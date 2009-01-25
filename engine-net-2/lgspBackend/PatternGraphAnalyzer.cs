/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
            AnnotateIndependentsAtNestingTopLevelOrAlternativeCasePattern(matchingPattern.patternGraph);
            ComputePatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative(matchingPattern.patternGraph);
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
        public void AnnotateIndependentsAtNestingTopLevelOrAlternativeCasePattern(
            PatternGraph patternGraph)
        {
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                // annotate path prefix and name
                if (patternGraph.pathPrefixesAndNamesOfNestedIndependents == null)
                    patternGraph.pathPrefixesAndNamesOfNestedIndependents = new List<Pair<String, String>>();
                patternGraph.pathPrefixesAndNamesOfNestedIndependents.Add(new Pair<String, String>(idpt.pathPrefix, idpt.name));
                // handle nested independents
                AnnotateIndependentsAtNestingTopLevelOrAlternativeCasePattern(idpt);
            }

            // alternative cases represent new annotation point
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    AnnotateIndependentsAtNestingTopLevelOrAlternativeCasePattern(altCase);
                }
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

            // and on ascending bottom up
            // a) it creates the local needed element sets
            patternGraph.neededNodes = new Dictionary<String, bool>();
            patternGraph.neededEdges = new Dictionary<String, bool>();

            // b) it adds the needed elements of the nested patterns (just computed)
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                foreach (KeyValuePair<string, bool> neededNode in neg.neededNodes)
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                foreach (KeyValuePair<string, bool> neededEdge in neg.neededEdges)
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                foreach (KeyValuePair<string, bool> neededNode in idpt.neededNodes)
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                foreach (KeyValuePair<string, bool> neededEdge in idpt.neededEdges)
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;

            }
            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    foreach (KeyValuePair<string, bool> neededNode in altCase.neededNodes)
                        patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                    foreach (KeyValuePair<string, bool> neededEdge in altCase.neededEdges)
                        patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                }
            }

            // c) it adds it's own locally needed elements
            //    - in conditions
            foreach (PatternCondition cond in patternGraph.Conditions)
            {
                foreach (String neededNode in cond.NeededNodes)
                    patternGraph.neededNodes[neededNode] = true;
                foreach (String neededEdge in cond.NeededEdges)
                    patternGraph.neededEdges[neededEdge] = true;
            }
            //    - in the pattern
            foreach (PatternNode node in patternGraph.nodes)
                if (node.PointOfDefinition != patternGraph)
                    patternGraph.neededNodes[node.name] = true;
            foreach (PatternEdge edge in patternGraph.edges)
                if (edge.PointOfDefinition != patternGraph)
                    patternGraph.neededEdges[edge.name] = true;
            //    - as subpattern connections
            foreach (PatternGraphEmbedding sub in patternGraph.embeddedGraphs)
            {
                foreach (PatternElement element in sub.connections)
                {
                    if (element.PointOfDefinition!=patternGraph)
                    {
                        if (element is PatternNode)
                            patternGraph.neededNodes[element.name] = true;
                        else // element is PatternEdge
                            patternGraph.neededEdges[element.name] = true;
                    }
                }
            }

            // d) it filters out the elements needed (by the nested patterns) which are defined locally
            foreach (PatternNode node in patternGraph.nodes)
                if (node.PointOfDefinition == patternGraph)
                    patternGraph.neededNodes.Remove(node.name);
            foreach (PatternEdge edge in patternGraph.edges)
                if (edge.PointOfDefinition == patternGraph)
                    patternGraph.neededEdges.Remove(edge.name);
        }

        /// <summary>
        /// Computes the pattern graphs which are on a path to some enclosed subpattern usage or alternative
        /// Adds them the pattern graph of the top level pattern
        /// </summary>
        private void ComputePatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative(PatternGraph topLevelPatternGraph)
        {
            topLevelPatternGraph.namesOfPatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative = new List<string>();
            ComputePatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative(topLevelPatternGraph, topLevelPatternGraph);
        }

        /// <summary>
        /// Computes the pattern graphs which are on a path to some enclosed subpattern usage or alternative
        /// Adds them the pattern graph of the top level pattern; alternative case is new top level pattern
        /// </summary>
        private bool ComputePatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative(
            PatternGraph patternGraph, PatternGraph topLevelPatternGraph)
        {
            // algorithm descends top down to the nested patterns, computes within each leaf pattern 
            // wheteher there are subpattern usages or alternatives, and ascends bottom up
            // current pattern graph has subpattern usages or alternatives enclosed
            // if they are contained locally or one of the nested patterns has them enclosed
            bool isSubpatternUsageOrAlternativeEnclosed = false;
            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                isSubpatternUsageOrAlternativeEnclosed |= ComputePatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative(
                    neg, topLevelPatternGraph);
            }
            foreach (PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                isSubpatternUsageOrAlternativeEnclosed |= ComputePatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative(
                    idpt, topLevelPatternGraph);
            }
            foreach (Alternative alt in patternGraph.alternatives)
            {
                isSubpatternUsageOrAlternativeEnclosed = true;
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    altCase.namesOfPatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative = new List<string>();
                    ComputePatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative(altCase, altCase);
                }
            }
            if (patternGraph.embeddedGraphs.Length > 0)
            {
                isSubpatternUsageOrAlternativeEnclosed = true;
            }

            // in this case add the current pattern graph to the list in the top level pattern graph
            if (isSubpatternUsageOrAlternativeEnclosed)
            {
                topLevelPatternGraph.namesOfPatternGraphsOnPathToEnclosedSubpatternUsageOrAlternative.Add(
                    patternGraph.pathPrefix+patternGraph.name);
            }

            return isSubpatternUsageOrAlternativeEnclosed;
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

            // all nested subpattern usages from nested negatives, independents, alternatives
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
