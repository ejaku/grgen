/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */


using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;


namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class analyzing the pattern graphs of the matching patterns to generate code for,
    /// storing computed nesting and inter-pattern-relationships locally in the pattern graphs,
    /// ready to be used by the (local intra-pattern) code generator
    /// (to generate code more easily, to generate better code).
    /// </summary>
    public class PatternGraphAnalyzer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PatternGraphAnalyzer()
        {
            matchingPatterns = new List<LGSPMatchingPattern>();
        }

        /// <summary>
        /// Analyze the nesting structure of the pattern
        /// and remember matching pattern for computing of inter pattern relations later on
        /// </summary>
        /// <param name="matchingPattern"></param>
        public void AnalyzeNestingOfAndRemember(LGSPMatchingPattern matchingPattern)
        {
            matchingPattern.patternGraph.SetDefEntityExistanceAndNonLocalDefEntityExistance();

            CalculateNeededElements(matchingPattern.patternGraph);

            AnnotateIndependentsAtNestingTopLevelOrAlternativeCaseOrIteratedPattern(matchingPattern.patternGraph);

            ComputePatternGraphsOnPathToEnclosedPatternpath(matchingPattern.patternGraph);

            ComputeMaxNegLevel(matchingPattern.patternGraph);

            matchingPatterns.Add(matchingPattern);
        }

        /// <summary>
        /// Whole world known by now, computer relationships in between matching patterns
        /// </summary>
        public void ComputeInterPatternRelations()
        {
            // compute for every rule/subpattern all directly or indirectly used subpatterns
            ComputeSubpatternsUsed();

            // fix point iteration in order to compute the pattern graphs on a path from an enclosing patternpath
            bool onPathFromEnclosingChanged;
            do
            {
                onPathFromEnclosingChanged = false;
                foreach (LGSPMatchingPattern matchingPattern in matchingPatterns)
                {
                    onPathFromEnclosingChanged |= ComputePatternGraphsOnPathFromEnclosingPatternpath(matchingPattern.patternGraph, false);
                }
            } // until nothing changes because transitive closure was found
            while (onPathFromEnclosingChanged);

            // fix point iteration in order to compute the max neg level
            bool maxNegLevelChanged;
            do
            {
                maxNegLevelChanged = false;
                foreach(LGSPMatchingPattern matchingPattern in matchingPatterns)
                {
                    maxNegLevelChanged |= ComputeMaxNegLevel(matchingPattern.patternGraph);
                }
            } // until nothing changes because transitive closure was found
            while(maxNegLevelChanged);
        }

        /// <summary>
        /// Analyze the pattern further on, know that the inter pattern relations are known
        /// </summary>
        /// <param name="matchingPattern"></param>
        public void AnalyzeWithInterPatternRelationsKnown(LGSPMatchingPattern matchingPattern)
        {
            AddSubpatternInformationToPatternpathInformation(matchingPattern.patternGraph);
        }

        ///////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Insert names of independents nested within the pattern graph
        /// to the matcher generation skeleton data structure pattern graph
        /// </summary>
        public void AnnotateIndependentsAtNestingTopLevelOrAlternativeCaseOrIteratedPattern(
            PatternGraph patternGraph)
        {
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                // annotate path prefix and name
                if (patternGraph.pathPrefixesAndNamesOfNestedIndependents == null)
                    patternGraph.pathPrefixesAndNamesOfNestedIndependents = new List<Pair<String, String>>();
                patternGraph.pathPrefixesAndNamesOfNestedIndependents.Add(new Pair<String, String>(idpt.pathPrefix, idpt.name));
                // handle nested independents
                AnnotateIndependentsAtNestingTopLevelOrAlternativeCaseOrIteratedPattern(idpt);
            }

            // alternative cases represent new annotation point
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    AnnotateIndependentsAtNestingTopLevelOrAlternativeCaseOrIteratedPattern(altCase);
                }
            }

            // iterateds represent new annotation point
            foreach (Iterated iter in patternGraph.iterateds)
            {
                AnnotateIndependentsAtNestingTopLevelOrAlternativeCaseOrIteratedPattern(iter.iteratedPattern);
            }
        }

        /// <summary>
        /// Calculates the elements the given pattern graph and it's nested pattern graphs don't compute locally
        /// but expect to be preset from outwards; for pattern graph and all nested graphs
        /// </summary>
        private static void CalculateNeededElements(PatternGraph patternGraph)
        {
            // algorithm descends top down to the nested patterns,
            // computes within each leaf pattern the locally needed elements
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                CalculateNeededElements(neg);
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                CalculateNeededElements(idpt);
            }
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    CalculateNeededElements(altCase);
                }
            }
            foreach (Iterated iter in patternGraph.iterateds)
            {
                CalculateNeededElements(iter.iteratedPattern);
            }

            // and on ascending bottom up
            // a) it creates the local needed element sets
            patternGraph.neededNodes = new Dictionary<String, bool>();
            patternGraph.neededEdges = new Dictionary<String, bool>();
            patternGraph.neededVariables = new Dictionary<String, GrGenType>();

            // b) it adds the needed elements of the nested patterns (just computed)
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                foreach (KeyValuePair<string, bool> neededNode in neg.neededNodes)
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                foreach (KeyValuePair<string, bool> neededEdge in neg.neededEdges)
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                foreach (KeyValuePair<string, GrGenType> neededVariable in neg.neededVariables)
                    patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                foreach (KeyValuePair<string, bool> neededNode in idpt.neededNodes)
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                foreach (KeyValuePair<string, bool> neededEdge in idpt.neededEdges)
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                foreach (KeyValuePair<string, GrGenType> neededVariable in idpt.neededVariables)
                    patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
            }
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    foreach (KeyValuePair<string, bool> neededNode in altCase.neededNodes)
                        patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                    foreach (KeyValuePair<string, bool> neededEdge in altCase.neededEdges)
                        patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                    foreach (KeyValuePair<string, GrGenType> neededVariable in altCase.neededVariables)
                        patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
                }
            }
            foreach (Iterated iter in patternGraph.iterateds)
            {
                foreach (KeyValuePair<string, bool> neededNode in iter.iteratedPattern.neededNodes)
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                foreach (KeyValuePair<string, bool> neededEdge in iter.iteratedPattern.neededEdges)
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                foreach (KeyValuePair<string, GrGenType> neededVariable in iter.iteratedPattern.neededVariables)
                    patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
            }

            // c) it adds it's own locally needed elements
            //    - in conditions
            foreach (PatternCondition cond in patternGraph.Conditions)
            {
                foreach (String neededNode in cond.NeededNodes)
                    patternGraph.neededNodes[neededNode] = true;
                foreach (String neededEdge in cond.NeededEdges)
                    patternGraph.neededEdges[neededEdge] = true;
                for (int i = 0; i < cond.NeededVariables.Length; ++i) {
                    patternGraph.neededVariables[cond.NeededVariables[i]] = cond.NeededVariableTypes[i];
                }
            }
            //    - in the pattern (if not def to be yielded, they are not needed top down)
            foreach (PatternNode node in patternGraph.nodes)
                if (node.PointOfDefinition != patternGraph)
                    if (!node.DefToBeYieldedTo)
                        patternGraph.neededNodes[node.name] = true;
            foreach (PatternEdge edge in patternGraph.edges)
                if (edge.PointOfDefinition != patternGraph)
                    if(!edge.DefToBeYieldedTo)
                        patternGraph.neededEdges[edge.name] = true;
            foreach (PatternVariable variable in patternGraph.variables)
                if (variable.PointOfDefinition != patternGraph)
                    if(!variable.DefToBeYieldedTo)
                        patternGraph.neededVariables[variable.name] = variable.Type;
            //    - as subpattern connections
            foreach (PatternGraphEmbedding sub in patternGraph.embeddedGraphs)
            {
                foreach (String neededNode in sub.neededNodes)
                    patternGraph.neededNodes[neededNode] = true;
                foreach (String neededEdge in sub.neededEdges)
                    patternGraph.neededEdges[neededEdge] = true;
                for (int i = 0; i < sub.neededVariables.Length; ++i)
                    patternGraph.neededVariables[sub.neededVariables[i]] = sub.neededVariableTypes[i];
            }

            // d) it filters out the elements needed (by the nested patterns) which are defined locally
            foreach (PatternNode node in patternGraph.nodes)
                if (node.PointOfDefinition == patternGraph)
                    patternGraph.neededNodes.Remove(node.name);
            foreach (PatternEdge edge in patternGraph.edges)
                if (edge.PointOfDefinition == patternGraph)
                    patternGraph.neededEdges.Remove(edge.name);
            foreach (PatternVariable variable in patternGraph.variables)
                if (variable.PointOfDefinition == patternGraph)
                    patternGraph.neededVariables.Remove(variable.name);
        }

        /// <summary>
        /// Computes whether the pattern graphs are on a path from some enclosing
        /// negative/independent with a patternpath modifier.
        /// They need to check the patternpath stack filled with the already matched entities
        /// on the subpattern usage/derivation path to this pattern.
        /// It stores information to the pattern graph and its children.
        /// Returns whether a change occured, to be used for a fixpoint iteration.
        /// </summary>
        private bool ComputePatternGraphsOnPathFromEnclosingPatternpath(
            PatternGraph patternGraph, bool isOnPathFromEnclosingPatternpath)
        {
            // Algorithm descends top down to the nested patterns,
            // on descending the am-i-on-path-from-enclosing-patternpath information
            // is computed locally and propagated downwards
            bool changed = false;

            // we are patternpath locked? -> so we and our nested patterns are on a path from an enclosing patternpath
            if(patternGraph.isPatternpathLocked)
                isOnPathFromEnclosingPatternpath = true;
            if(isOnPathFromEnclosingPatternpath && !patternGraph.isPatternGraphOnPathFromEnclosingPatternpath)
            {
                patternGraph.isPatternGraphOnPathFromEnclosingPatternpath = true;
                changed = true;
            }

            // we're on a path from an enclosing patternpath? -> the subpatterns we call are too
            if(patternGraph.isPatternGraphOnPathFromEnclosingPatternpath)
            {
                foreach (PatternGraphEmbedding embedding in patternGraph.embeddedGraphs)
                {
                    PatternGraph embeddedPatternGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
                    if(!embeddedPatternGraph.isPatternGraphOnPathFromEnclosingPatternpath)
                    {
                        embeddedPatternGraph.isPatternGraphOnPathFromEnclosingPatternpath = true;
                        changed = true;
                    }
                }
            }

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                changed |= ComputePatternGraphsOnPathFromEnclosingPatternpath(neg, isOnPathFromEnclosingPatternpath);
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                changed |= ComputePatternGraphsOnPathFromEnclosingPatternpath(idpt, isOnPathFromEnclosingPatternpath);
            }

            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    changed |= ComputePatternGraphsOnPathFromEnclosingPatternpath(altCase, isOnPathFromEnclosingPatternpath);
                }
            }
            foreach (Iterated iter in patternGraph.iterateds)
            {
                changed |= ComputePatternGraphsOnPathFromEnclosingPatternpath(iter.iteratedPattern, isOnPathFromEnclosingPatternpath);
            }

            return changed;
        }

        /// <summary>
        /// Computes the pattern graphs which are on a path to some enclosed negative/independent
        /// with a patternpath modifier. They need to fill the patternpath check stack.
        /// It stores information to the pattern graph and its children.
        /// First pass, computes local information neglecting subpattern usage.
        /// </summary>
        private void ComputePatternGraphsOnPathToEnclosedPatternpath(PatternGraph patternGraph)
        {
            // Algorithm descends top down to the nested patterns and ascends bottom up again,
            // on ascending the who-is-on-path-to-enclosed-patternpath information
            // is computed locally and propagated upwards
            patternGraph.patternGraphsOnPathToEnclosedPatternpath = new List<string>();

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                ComputePatternGraphsOnPathToEnclosedPatternpath(neg);
                patternGraph.patternGraphsOnPathToEnclosedPatternpath.AddRange(neg.patternGraphsOnPathToEnclosedPatternpath);
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                ComputePatternGraphsOnPathToEnclosedPatternpath(idpt);
                patternGraph.patternGraphsOnPathToEnclosedPatternpath.AddRange(idpt.patternGraphsOnPathToEnclosedPatternpath);
            }

            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    ComputePatternGraphsOnPathToEnclosedPatternpath(altCase);
                    AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                        patternGraph.pathPrefix + patternGraph.name,
                        altCase.patternGraphsOnPathToEnclosedPatternpath);
                }
            }
            foreach (Iterated iter in patternGraph.iterateds)
            {
                ComputePatternGraphsOnPathToEnclosedPatternpath(iter.iteratedPattern);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name,
                    iter.iteratedPattern.patternGraphsOnPathToEnclosedPatternpath);
            }

            // one of the nested patterns was found to be on a path
            // to a pattern with patternpath modifier -> so we are/may be too
            // or we are locally because we contain a patternpath modifier
            if (patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count != 0
                || patternGraph.isPatternpathLocked)
            {
                // add the current pattern graph to the list in the top level pattern graph
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name);
            }
        }

        /// <summary>
        /// Computes the pattern graphs which are on a path to some enclosed negative/independent
        /// with a patternpath modifier; stores information to the pattern graph and its children.
        /// Second pass, adds global information from subpattern usage.
        /// </summary>
        private void AddSubpatternInformationToPatternpathInformation(PatternGraph patternGraph)
        {
            // Algorithm descends top down to the nested patterns and ascends bottom up again,
            // on ascending the who-is-on-path-to-enclosed-patternpath information from subpattern usage
            // is addded locally and propagated upwards

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                AddSubpatternInformationToPatternpathInformation(neg);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath, neg.patternGraphsOnPathToEnclosedPatternpath);
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                AddSubpatternInformationToPatternpathInformation(idpt);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath, idpt.patternGraphsOnPathToEnclosedPatternpath);
            }

            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    AddSubpatternInformationToPatternpathInformation(altCase);
                    AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                        patternGraph.pathPrefix + patternGraph.name,
                        altCase.patternGraphsOnPathToEnclosedPatternpath);
                }
            }
            foreach (Iterated iter in patternGraph.iterateds)
            {
                AddSubpatternInformationToPatternpathInformation(iter.iteratedPattern);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name,
                    iter.iteratedPattern.patternGraphsOnPathToEnclosedPatternpath);
            }

            foreach (PatternGraphEmbedding embedding in patternGraph.embeddedGraphs)
            {
                PatternGraph embeddedPatternGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name,
                    embeddedPatternGraph.patternGraphsOnPathToEnclosedPatternpath);

                foreach (LGSPMatchingPattern calledMatchingPattern in embeddedPatternGraph.usedSubpatterns.Keys)
                {
                    if(calledMatchingPattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count > 0)
                    {
                        AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                            patternGraph.pathPrefix + patternGraph.name,
                            calledMatchingPattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath);
                    }
                }
            }

            // one of the used subpatterns was found to be on a path
            // to a pattern with patternpath modifier -> so we are/may be too
            if (patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count != 0)
            {
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name);
            }
        }

        /// <summary>
        /// Adds the elements from the list to be added to the target list in case they are not already contained there
        /// </summary>
        private void AddNotContained<T>(List<T> target, List<T> listToBeAdded)
        {
            foreach(T toBeAdded in listToBeAdded)
            {
                AddNotContained(target, toBeAdded);
            }
        }

        /// <summary>
        /// Adds the element to be added to the target list in case it is not already contained and the condition list is not empty
        /// </summary>
        private void AddNotContained<T>(List<T> target, T toBeAdded, List<T> listCondition)
        {
            if(listCondition.Count > 0)
                AddNotContained(target, toBeAdded);
        }

        /// <summary>
        /// Adds the element to be added to the target list in case it is not already contained there
        /// </summary>
        private void AddNotContained<T>(List<T> target, T toBeAdded)
        {
            if(!target.Contains(toBeAdded))
                target.Add(toBeAdded);
        }

        /// <summary>
        /// Computes for each matching pattern (of rule/subpattern)
        /// all directly/locally and indirectly/globally used matching patterns (of used subpatterns).
        /// </summary>
        private void ComputeSubpatternsUsed()
        {
            // step 1 intra pattern
            // initialize used subpatterns in pattern graph with all locally used subpatterns - top level or nested
            foreach (LGSPMatchingPattern matchingPattern in matchingPatterns)
            {
                matchingPattern.patternGraph.usedSubpatterns =
                    new Dictionary<LGSPMatchingPattern, LGSPMatchingPattern>();
                ComputeSubpatternsUsedLocally(matchingPattern.patternGraph, matchingPattern);
            }

            // step 2 inter pattern
            // fixed point iteration in order to get the globally / indirectly used subpatterns
            bool subpatternsUsedChanged;
            do
            {
                // for every subpattern used, add all the subpatterns used by these ones to current one
                subpatternsUsedChanged = false;
                foreach (LGSPMatchingPattern matchingPattern in matchingPatterns)
                {
                    subpatternsUsedChanged |= AddSubpatternsOfSubpatternsUsed(matchingPattern);
                }
            } // until nothing changes because transitive closure is found
            while (subpatternsUsedChanged);
        }

        /// <summary>
        /// Computes for given pattern graph all locally used subpatterns;
        /// none of the globally used ones, but all of the nested ones.
        /// Writes them to the used subpatterns member of the pattern graph of the given top level matching pattern.
        /// </summary>
        private void ComputeSubpatternsUsedLocally(PatternGraph patternGraph, LGSPMatchingPattern topLevelMatchingPattern)
        {
            // all directly used subpatterns
            PatternGraphEmbedding[] embeddedGraphs = patternGraph.embeddedGraphs;
            for (int i = 0; i < embeddedGraphs.Length; ++i)
            {
                topLevelMatchingPattern.patternGraph.usedSubpatterns[embeddedGraphs[i].matchingPatternOfEmbeddedGraph] = null;
            }

            // all nested subpattern usages from nested negatives, independents, alternatives, iterateds
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                ComputeSubpatternsUsedLocally(neg, topLevelMatchingPattern);
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                ComputeSubpatternsUsedLocally(idpt, topLevelMatchingPattern);
            }
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    ComputeSubpatternsUsedLocally(altCase, topLevelMatchingPattern);
                }
            }
            foreach (Iterated iter in patternGraph.iterateds)
            {
                ComputeSubpatternsUsedLocally(iter.iteratedPattern, topLevelMatchingPattern);
            }
        }

        /// <summary>
        /// Adds all of the subpatterns used by one of the subpatterns used by the given matching pattern,
        /// returns whether the set of subpatterns of the given matching pattern changed thereby.
        /// Consider worklist algorithm in case of performance problems
        /// </summary>
        private bool AddSubpatternsOfSubpatternsUsed(LGSPMatchingPattern matchingPattern)
        {
            bool usedSubpatternsChanged = false;

        restart:
            foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpattern
                in matchingPattern.patternGraph.usedSubpatterns)
            {
                foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpatternOfUsedSubpattern
                    in usedSubpattern.Key.patternGraph.usedSubpatterns)
                {
                    if (!matchingPattern.patternGraph.usedSubpatterns.ContainsKey(usedSubpatternOfUsedSubpattern.Key))
                    {
                        matchingPattern.patternGraph.usedSubpatterns.Add(
                            usedSubpatternOfUsedSubpattern.Key, usedSubpatternOfUsedSubpattern.Value);
                        usedSubpatternsChanged = true;
                        goto restart; // adding invalidates enumerator
                    }
                }
            }

            return usedSubpatternsChanged;
        }

        /// <summary>
        /// Computes the maximum negLevel of the pattern graph reached by negative/independent nesting,
        /// clipped by LGSPElemFlags.MAX_NEG_LEVEL+1 which is the critical point of interest,
        /// this might happen by heavy nesting or by a subpattern call path with
        /// direct or indirect recursion on it including a negative/independent which gets passed.
        /// Returns if the max negLevel of a subpattern called was increased, causing a further run.
        /// </summary>
        private bool ComputeMaxNegLevel(PatternGraph patternGraph)
        {
            foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                neg.maxNegLevel = patternGraph.maxNegLevel + 1;
                ComputeMaxNegLevel(neg);
            }
            foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                idpt.maxNegLevel = patternGraph.maxNegLevel + 1;
                ComputeMaxNegLevel(idpt);
            }

            foreach(Alternative alt in patternGraph.alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    altCase.maxNegLevel = patternGraph.maxNegLevel;
                    ComputeMaxNegLevel(altCase);
                }
            }
            foreach(Iterated iter in patternGraph.iterateds)
            {
                iter.iteratedPattern.maxNegLevel = patternGraph.maxNegLevel;
                ComputeMaxNegLevel(iter.iteratedPattern);
            }

            bool changed = false;
            PatternGraphEmbedding[] embeddedGraphs = patternGraph.embeddedGraphs;
            for(int i = 0; i < embeddedGraphs.Length; ++i)
            {
                PatternGraph embeddedPatternGraph = embeddedGraphs[i].matchingPatternOfEmbeddedGraph.patternGraph;
                if(embeddedPatternGraph.maxNegLevel <= (int)LGSPElemFlags.MAX_NEG_LEVEL)
                {
                    int oldMaxNegLevel = embeddedPatternGraph.maxNegLevel;
                    embeddedPatternGraph.maxNegLevel = Math.Max(patternGraph.maxNegLevel, embeddedPatternGraph.maxNegLevel);
                    if(embeddedPatternGraph.maxNegLevel > oldMaxNegLevel)
                        changed = true;
                }
            }

            return changed;
        }

        // ----------------------------------------------------------------

        private List<LGSPMatchingPattern> matchingPatterns;
    }
}
