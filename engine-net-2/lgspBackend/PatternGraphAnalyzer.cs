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
        /// Computes for each matching pattern (of rule/subpattern)
        /// all directly or indirectly used matching patterns (of used subpatterns).
        /// </summary>
        private void ComputeSubpatternsUsed()
        {
            // all directly used subpatterns
            foreach (LGSPMatchingPattern matchingPattern in matchingPatterns)
            {
                ComputeSubpatternsUsed(matchingPattern);
            }
        }

        /// <summary>
        /// Computes for given matching pattern (of rule/subpattern)
        /// all directly or indirectly used matching patterns (of used subpatterns).
        /// </summary>
        private void ComputeSubpatternsUsed(LGSPMatchingPattern matchingPattern)
        {
            // TODO: subpatterns used in negatives, independents, alternatives!!!
            // todo: use more efficient dynamic programming algorithm

            matchingPattern.patternGraph.usedSubpatterns = 
                new Dictionary<LGSPMatchingPattern, LGSPMatchingPattern>();

            // all directly used subpatterns
            PatternGraphEmbedding[] embeddedGraphs = 
                ((PatternGraphEmbedding[]) matchingPattern.patternGraph.EmbeddedGraphs);
            for (int i = 0; i < embeddedGraphs.Length; ++i)
            {
                matchingPattern.patternGraph.usedSubpatterns[embeddedGraphs[i].matchingPatternOfEmbeddedGraph] = null;
            }

            // transitive closure
            bool setChanged = matchingPattern.patternGraph.usedSubpatterns.Count != 0;
            while (setChanged)
            {
                setChanged = false;
                foreach (KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPattern in matchingPattern.patternGraph.usedSubpatterns)
                {
                    PatternGraphEmbedding[] embedded = (PatternGraphEmbedding[])subpatternMatchingPattern.Key.PatternGraph.EmbeddedGraphs;
                    for (int i = 0; i < embedded.Length; ++i)
                    {
                        if (!matchingPattern.patternGraph.usedSubpatterns.ContainsKey(embedded[i].matchingPatternOfEmbeddedGraph))
                        {
                            matchingPattern.patternGraph.usedSubpatterns.Add(embedded[i].matchingPatternOfEmbeddedGraph, null);
                            setChanged = true;
                        }
                    }

                    if (setChanged) break;
                }
            }
        }

        private List<LGSPMatchingPattern> matchingPatterns;
    }
}
