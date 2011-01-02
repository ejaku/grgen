/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
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
            CalculateNeededElements(matchingPattern.patternGraph);
            AnnotateIndependentsAtNestingTopLevelOrAlternativeCaseOrIteratedPattern(matchingPattern.patternGraph);
            ComputePatternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath(matchingPattern.patternGraph);
            matchingPatterns.Add(matchingPattern);
        }

        /// <summary>
        /// Whole world known by now, computer relationships in between matching patterns
        /// </summary>
        public void ComputeInterPatternRelations()
        {
            ComputeSubpatternsUsed();
        }

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
            foreach (PatternGraph iter in patternGraph.iterateds)
            {
                AnnotateIndependentsAtNestingTopLevelOrAlternativeCaseOrIteratedPattern(iter);
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
            foreach (PatternGraph iter in patternGraph.iterateds)
            {
                CalculateNeededElements(iter);
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
            foreach (PatternGraph iter in patternGraph.iterateds)
            {
                foreach (KeyValuePair<string, bool> neededNode in iter.neededNodes)
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                foreach (KeyValuePair<string, bool> neededEdge in iter.neededEdges)
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                foreach (KeyValuePair<string, GrGenType> neededVariable in iter.neededVariables)
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
            //    - in the pattern
            foreach (PatternNode node in patternGraph.nodes)
                if (node.PointOfDefinition != patternGraph)
                    patternGraph.neededNodes[node.name] = true;
            foreach (PatternEdge edge in patternGraph.edges)
                if (edge.PointOfDefinition != patternGraph)
                    patternGraph.neededEdges[edge.name] = true;
            foreach (PatternVariable variable in patternGraph.variables)
                if (variable.PointOfDefinition != patternGraph)
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
        /// Computes the pattern graphs which are on a path to some enclosed subpattern usage/alternative/iterated
        /// or negative/independent with a patternpath modifier; stores information to the pattern graph and it's children.
        /// </summary>
        private void ComputePatternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath(
            PatternGraph patternGraph)
        {
            // Algorithm descends top down to the nested patterns, 
            // computes within each leaf pattern whether there are subpattern usages/alternatives/iterateds,
            // or whether there is a patternpath modifier, stores this information locally,
            // and ascends bottom up, computing/storing the same information, adding the results of the nested patterns.
            patternGraph.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath = new List<string>();

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                ComputePatternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath(neg);
                patternGraph.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath
                    .AddRange(neg.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath);
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                ComputePatternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath(idpt);
                patternGraph.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath
                    .AddRange(idpt.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath);
            }
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    ComputePatternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath(altCase);
                    patternGraph.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath
                        .AddRange(altCase.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath);
                }
            }
            foreach (PatternGraph iter in patternGraph.iterateds)
            {
                ComputePatternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath(iter);
                patternGraph.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath
                    .AddRange(iter.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath);
            }

            // one of the nested patterns was found to be on the path -> so we are too
            // or we are locally on the path due to subpattern usages, alternatives, iterateds, patternpath modifier
            if (patternGraph.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath.Count != 0
                || patternGraph.embeddedGraphs.Length > 0
                || patternGraph.alternatives.Length > 0
                || patternGraph.iterateds.Length > 0
                || patternGraph.isPatternpathLocked)
            {
                // add the current pattern graph to the list in the top level pattern graph
                patternGraph.patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath
                    .Add(patternGraph.pathPrefix + patternGraph.name);
            }
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
            foreach (PatternGraph iter in patternGraph.iterateds)
            {
                ComputeSubpatternsUsedLocally(iter, topLevelMatchingPattern);
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

        // ----------------------------------------------------------------

        private List<LGSPMatchingPattern> matchingPatterns;
    }
}
