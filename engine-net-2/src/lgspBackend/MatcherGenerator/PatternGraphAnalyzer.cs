/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.expression;


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
        /// Analyze the nesting structure of the pattern graph
        /// </summary>
        /// <param name="patternGraph">The pattern graph</param>
        /// <param name="inlined">Tells whether subpattern usages were inlined (analyze run before and after inlining)</param>
        public void AnalyzeNestingOfPatternGraph(PatternGraph patternGraph, bool inlined)
        {
            SetDefEntityExistanceAndNonLocalDefEntityExistance(patternGraph, inlined);

            CalculateNeededElements(patternGraph, inlined);

            AnnotateIndependentsAtNestingPattern(patternGraph, inlined, false, false);

            if(!inlined) // no inlining will occur in case the pattern is on a patternpath, so nothing to adapt to in the after-inline run
                ComputePatternGraphsOnPathToEnclosedPatternpath(patternGraph);

            ComputeMaxIsoSpace(patternGraph, inlined);
        }

        /// <summary>
        /// Remember matching pattern for computing of inter pattern relations later on
        /// </summary>
        /// <param name="matchingPattern"></param>
        public void RememberMatchingPattern(LGSPMatchingPattern matchingPattern)
        {
            matchingPatterns.Add(matchingPattern);
        }

        /// <summary>
        /// Whole world known by now, computer relationships in between matching patterns
        /// </summary>
        public void ComputeInterPatternRelations(bool inlined)
        {
            // compute for every rule/subpattern all directly or indirectly used subpatterns
            ComputeSubpatternsUsed(inlined);

            if(!inlined) // no inlining will occur in case the pattern is on a patternpath, so nothing to adapt to in the after-inline run
            {
                // fix point iteration in order to compute the pattern graphs on a path from an enclosing patternpath
                bool onPathFromEnclosingChanged;
                do
                {
                    onPathFromEnclosingChanged = false;
                    foreach(LGSPMatchingPattern matchingPattern in matchingPatterns)
                    {
                        onPathFromEnclosingChanged |= ComputePatternGraphsOnPathFromEnclosingPatternpath(matchingPattern.patternGraph, false);
                    }
                } // until nothing changes because transitive closure was found
                while(onPathFromEnclosingChanged);
            }

            // fix point iteration in order to compute the max iso space number
            bool maxIsoSpaceChanged;
            do
            {
                maxIsoSpaceChanged = false;
                foreach(LGSPMatchingPattern matchingPattern in matchingPatterns)
                {
                    maxIsoSpaceChanged |= ComputeMaxIsoSpace(matchingPattern.patternGraph, inlined);
                }
            } // until nothing changes because transitive closure was found
            while(maxIsoSpaceChanged);

            // parallelization occurs only after inlining
            if(inlined)
            {
                foreach(LGSPMatchingPattern matchingPattern in matchingPatterns)
                {
                    SetNeedForParallelizedVersion(matchingPattern);
                }
            }
        }

        /// <summary>
        /// Analyze the pattern further on, know that the inter pattern relations are known
        /// </summary>
        public void AnalyzeWithInterPatternRelationsKnown(PatternGraph patternGraph)
        {
            AddSubpatternInformationToPatternpathInformation(patternGraph);
        }

        ///////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Insert names of independents nested within the pattern graph
        /// to the matcher generation skeleton data structure pattern graph
        /// </summary>
        public void AnnotateIndependentsAtNestingPattern(
            PatternGraph patternGraph, bool inlined, bool nestedInNegative, bool nestedInIteratedWithPotentiallyMoreThanOneMatch)
        {
            patternGraph.nestedIndependents = null; // at inlined pass reset values from pass before (nop when used first)
            
            PatternGraph[] independentPatternGraphs = inlined ? patternGraph.independentPatternGraphsPlusInlined : patternGraph.independentPatternGraphs;
            foreach(PatternGraph idpt in independentPatternGraphs)
            {
                if(!nestedInNegative)
                {
                    // annotate path prefix and name
                    if(patternGraph.nestedIndependents == null)
                        patternGraph.nestedIndependents = new Dictionary<PatternGraph, bool>();
                    patternGraph.nestedIndependents[idpt] = nestedInIteratedWithPotentiallyMoreThanOneMatch;
                }

                // handle nested independents
                AnnotateIndependentsAtNestingPattern(idpt, inlined, nestedInNegative, nestedInIteratedWithPotentiallyMoreThanOneMatch);
            }

            PatternGraph[] negativePatternGraphs = inlined ? patternGraph.negativePatternGraphsPlusInlined : patternGraph.negativePatternGraphs;
            foreach(PatternGraph neg in negativePatternGraphs)
            {
                // handle nested independents
                AnnotateIndependentsAtNestingPattern(neg, inlined, true, nestedInIteratedWithPotentiallyMoreThanOneMatch);
            }

            // alternative cases represent new annotation point
            Alternative[] alternatives = inlined ? patternGraph.alternativesPlusInlined : patternGraph.alternatives;
            foreach(Alternative alt in alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    AnnotateIndependentsAtNestingPattern(altCase, inlined, false, false);
                }
            }

            // iterateds represent new annotation point
            Iterated[] iterateds = inlined ? patternGraph.iteratedsPlusInlined : patternGraph.iterateds;
            foreach(Iterated iter in iterateds)
            {
                AnnotateIndependentsAtNestingPattern(iter.iteratedPattern, inlined, false, iter.maxMatches==0 || iter.maxMatches>1);
            }
        }

        public void SetDefEntityExistanceAndNonLocalDefEntityExistance(PatternGraph patternGraph, bool inlined)
        {
            if(inlined)
            {
                foreach(PatternNode node in patternGraph.nodesPlusInlined)
                {
                    if(node.DefToBeYieldedTo)
                    {
                        patternGraph.isDefEntityExistingPlusInlined = true;
                        if(node.pointOfDefinition != patternGraph)
                            patternGraph.isNonLocalDefEntityExistingPlusInlined = true;
                    }
                }
                foreach(PatternEdge edge in patternGraph.edgesPlusInlined)
                {
                    if(edge.DefToBeYieldedTo)
                    {
                        patternGraph.isDefEntityExistingPlusInlined = true;
                        if(edge.pointOfDefinition != patternGraph)
                            patternGraph.isNonLocalDefEntityExistingPlusInlined = true;
                    }
                }
                foreach(PatternVariable var in patternGraph.variablesPlusInlined)
                {
                    if(var.DefToBeYieldedTo)
                    {
                        patternGraph.isDefEntityExistingPlusInlined = true;
                        if(var.pointOfDefinition != patternGraph)
                            patternGraph.isNonLocalDefEntityExistingPlusInlined = true;
                    }
                }
                foreach(PatternYielding patternYielding in patternGraph.YieldingsPlusInlined)
                {
                    foreach(Yielding yielding in patternYielding.ElementaryYieldings)
                    {
                        if(yielding is IteratedFiltering)
                            patternGraph.isIteratedFilteringExistingPlusInlined = true;
                    }
                }
                foreach(PatternYielding patternYielding in patternGraph.YieldingsPlusInlined)
                {
                    foreach(Yielding yielding in patternYielding.ElementaryYieldings)
                    {
                        foreach(ExpressionOrYielding child in yielding)
                        {
                            if(child is EmitStatement)
                                patternGraph.isEmitOrAssertExistingPlusInlined = true;
                            else if(child is AssertStatement)
                                patternGraph.isEmitOrAssertExistingPlusInlined = true;
                        }
                    }
                }

                foreach(Alternative alternative in patternGraph.alternativesPlusInlined)
                {
                    foreach(PatternGraph alternativeCase in alternative.alternativeCases)
                    {
                        SetDefEntityExistanceAndNonLocalDefEntityExistance(alternativeCase, true);
                    }
                }
                foreach(Iterated iterated in patternGraph.iteratedsPlusInlined)
                {
                    SetDefEntityExistanceAndNonLocalDefEntityExistance(iterated.iteratedPattern, true);
                }
                foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
                {
                    SetDefEntityExistanceAndNonLocalDefEntityExistance(independent, true);
                }
            }
            else
            {
                foreach(PatternNode node in patternGraph.nodes)
                {
                    if(node.DefToBeYieldedTo)
                    {
                        patternGraph.isDefEntityExisting = true;
                        if(node.pointOfDefinition != patternGraph)
                            patternGraph.isNonLocalDefEntityExisting = true;
                    }
                }
                foreach(PatternEdge edge in patternGraph.edges)
                {
                    if(edge.DefToBeYieldedTo)
                    {
                        patternGraph.isDefEntityExisting = true;
                        if(edge.pointOfDefinition != patternGraph)
                            patternGraph.isNonLocalDefEntityExisting = true;
                    }
                }
                foreach(PatternVariable var in patternGraph.variables)
                {
                    if(var.DefToBeYieldedTo)
                    {
                        patternGraph.isDefEntityExisting = true;
                        if(var.pointOfDefinition != patternGraph)
                            patternGraph.isNonLocalDefEntityExisting = true;
                    }
                }
                foreach(PatternYielding patternYielding in patternGraph.Yieldings)
                {
                    foreach(Yielding yielding in patternYielding.ElementaryYieldings)
                    {
                        if(yielding is IteratedFiltering)
                            patternGraph.isIteratedFilteringExisting = true;
                    }
                }
                foreach(PatternYielding patternYielding in patternGraph.Yieldings)
                {
                    foreach(Yielding yielding in patternYielding.ElementaryYieldings)
                    {
                        foreach(ExpressionOrYielding child in yielding)
                        {
                            if(child is EmitStatement)
                                patternGraph.isEmitOrAssertExisting = true;
                            else if(child is AssertStatement)
                                patternGraph.isEmitOrAssertExisting = true;
                        }
                    }
                }

                foreach(Alternative alternative in patternGraph.alternatives)
                {
                    foreach(PatternGraph alternativeCase in alternative.alternativeCases)
                    {
                        SetDefEntityExistanceAndNonLocalDefEntityExistance(alternativeCase, false);
                    }
                }
                foreach(Iterated iterated in patternGraph.iterateds)
                {
                    SetDefEntityExistanceAndNonLocalDefEntityExistance(iterated.iteratedPattern, false);
                }
                foreach(PatternGraph independent in patternGraph.independentPatternGraphs)
                {
                    SetDefEntityExistanceAndNonLocalDefEntityExistance(independent, false);
                }
            }
        }

        /// <summary>
        /// Calculates the elements the given pattern graph and it's nested pattern graphs don't compute locally
        /// but expect to be preset from outwards; for pattern graph and all nested graphs
        /// </summary>
        private static void CalculateNeededElements(PatternGraph patternGraph, bool inlined)
        {
            // algorithm descends top down to the nested patterns,
            // computes within each leaf pattern the locally needed elements
            PatternGraph[] negativePatternGraphs = inlined ? patternGraph.negativePatternGraphsPlusInlined : patternGraph.negativePatternGraphs;
            foreach(PatternGraph neg in negativePatternGraphs)
            {
                CalculateNeededElements(neg, inlined);
            }
            PatternGraph[] independentPatternGraphs = inlined ? patternGraph.independentPatternGraphsPlusInlined : patternGraph.independentPatternGraphs;
            foreach(PatternGraph idpt in independentPatternGraphs)
            {
                CalculateNeededElements(idpt, inlined);
            }
            Alternative[] alternatives = inlined ? patternGraph.alternativesPlusInlined : patternGraph.alternatives;
            foreach(Alternative alt in alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    CalculateNeededElements(altCase, inlined);
                }
            }
            Iterated[] iterateds = inlined ? patternGraph.iteratedsPlusInlined : patternGraph.iterateds;
            foreach(Iterated iter in iterateds)
            {
                CalculateNeededElements(iter.iteratedPattern, inlined);
            }

            // and on ascending bottom up
            // a) it creates the local needed element sets
            patternGraph.neededNodes = new Dictionary<String, PatternNode>();
            patternGraph.neededEdges = new Dictionary<String, PatternEdge>();
            patternGraph.neededVariables = new Dictionary<String, PatternVariable>();

            // b) it adds the needed elements of the nested patterns (just computed)
            foreach(PatternGraph neg in negativePatternGraphs)
            {
                foreach(KeyValuePair<string, PatternNode> neededNode in neg.neededNodes)
                {
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                }
                foreach(KeyValuePair<string, PatternEdge> neededEdge in neg.neededEdges)
                {
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                }
                foreach(KeyValuePair<string, PatternVariable> neededVariable in neg.neededVariables)
                {
                    patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
                }
            }
            foreach(PatternGraph idpt in independentPatternGraphs)
            {
                foreach(KeyValuePair<string, PatternNode> neededNode in idpt.neededNodes)
                {
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                }
                foreach(KeyValuePair<string, PatternEdge> neededEdge in idpt.neededEdges)
                {
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                }
                foreach(KeyValuePair<string, PatternVariable> neededVariable in idpt.neededVariables)
                {
                    patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
                }
            }
            foreach(Alternative alt in alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    foreach(KeyValuePair<string, PatternNode> neededNode in altCase.neededNodes)
                    {
                        patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                    }
                    foreach(KeyValuePair<string, PatternEdge> neededEdge in altCase.neededEdges)
                    {
                        patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                    }
                    foreach(KeyValuePair<string, PatternVariable> neededVariable in altCase.neededVariables)
                    {
                        patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
                    }
                }
            }
            foreach(Iterated iter in iterateds)
            {
                foreach(KeyValuePair<string, PatternNode> neededNode in iter.iteratedPattern.neededNodes)
                {
                    patternGraph.neededNodes[neededNode.Key] = neededNode.Value;
                }
                foreach(KeyValuePair<string, PatternEdge> neededEdge in iter.iteratedPattern.neededEdges)
                {
                    patternGraph.neededEdges[neededEdge.Key] = neededEdge.Value;
                }
                foreach(KeyValuePair<string, PatternVariable> neededVariable in iter.iteratedPattern.neededVariables)
                {
                    patternGraph.neededVariables[neededVariable.Key] = neededVariable.Value;
                }
            }

            // c) it adds it's own locally needed elements
            //    - in conditions
            PatternCondition[] Conditions = inlined ? patternGraph.ConditionsPlusInlined : patternGraph.Conditions;
            foreach(PatternCondition cond in Conditions)
            {
                for(int i = 0; i < cond.NeededNodeNames.Length; ++i)
                {
                    String neededNodeName = cond.NeededNodeNames[i];
                    PatternNode neededNode = cond.NeededNodes[i];
                    patternGraph.neededNodes[neededNodeName] = neededNode;
                }
                for(int i = 0; i < cond.NeededEdgeNames.Length; ++i)
                {
                    String neededEdgeName = cond.NeededEdgeNames[i];
                    PatternEdge neededEdge = cond.NeededEdges[i];
                    patternGraph.neededEdges[neededEdgeName] = neededEdge;
                }
                for(int i = 0; i < cond.NeededVariableNames.Length; ++i)
                {
                    String neededVariableName = cond.NeededVariableNames[i];
                    PatternVariable neededVariable = cond.NeededVariables[i];
                    patternGraph.neededVariables[neededVariableName] = neededVariable;
                }
            }
            //    - in the pattern (def to be yielded are needed top down because of their initialization - even if besides that only bottom-up accumulation)
            PatternNode[] nodes = inlined ? patternGraph.nodesPlusInlined : patternGraph.nodes;
            foreach(PatternNode node in nodes)
            {
                if(node.PointOfDefinition != patternGraph)
                {
                    if(!node.DefToBeYieldedTo)
                        patternGraph.neededNodes[node.name] = node;
                    else
                        patternGraph.neededNodes[NamesOfEntities.CandidateVariable(node.name)] = node;
                }
            }
            PatternEdge[] edges = inlined ? patternGraph.edgesPlusInlined : patternGraph.edges;
            foreach(PatternEdge edge in edges)
            {
                if(edge.PointOfDefinition != patternGraph)
                {
                    if(!edge.DefToBeYieldedTo)
                        patternGraph.neededEdges[edge.name] = edge;
                    else
                        patternGraph.neededEdges[NamesOfEntities.CandidateVariable(edge.name)] = edge;
                }
            }
            PatternVariable[] variables = inlined ? patternGraph.variablesPlusInlined : patternGraph.variables;
            foreach(PatternVariable variable in variables)
            {
                if(variable.PointOfDefinition != patternGraph)
                {
                    if(!variable.DefToBeYieldedTo)
                        patternGraph.neededVariables[variable.name] = variable;
                    else
                        patternGraph.neededVariables[NamesOfEntities.Variable(variable.name)] = variable;
                }
            }
            //    - as subpattern connections
            PatternGraphEmbedding[] embeddedGraphs = inlined ? patternGraph.embeddedGraphsPlusInlined : patternGraph.embeddedGraphs;
            foreach(PatternGraphEmbedding sub in embeddedGraphs)
            {
                if(sub.inlined) // skip inlined embeddings
                    continue;
                for(int i = 0; i < sub.neededNodeNames.Length; ++i)
                {
                    String neededNodeName = sub.neededNodeNames[i];
                    PatternNode neededNode = sub.neededNodes[i];
                    patternGraph.neededNodes[neededNodeName] = neededNode;
                }
                for(int i = 0; i < sub.neededEdgeNames.Length; ++i)
                {
                    String neededEdgeName = sub.neededEdgeNames[i];
                    PatternEdge neededEdge = sub.neededEdges[i];
                    patternGraph.neededEdges[neededEdgeName] = neededEdge;
                }
                for(int i = 0; i < sub.neededVariableNames.Length; ++i)
                {
                    String neededVariableName = sub.neededVariableNames[i];
                    PatternVariable neededVariable = sub.neededVariables[i];
                    patternGraph.neededVariables[neededVariableName] = neededVariable;
                }
            }

            // d) it filters out the elements needed (by the nested patterns) which are defined locally
            foreach(PatternNode node in nodes)
            {
                if(node.PointOfDefinition == patternGraph)
                {
                    if(!node.DefToBeYieldedTo)
                        patternGraph.neededNodes.Remove(node.name);
                    else
                        patternGraph.neededNodes.Remove(NamesOfEntities.CandidateVariable(node.name));
                }
            }
            foreach(PatternEdge edge in edges)
            {
                if(edge.PointOfDefinition == patternGraph)
                {
                    if(!edge.DefToBeYieldedTo)
                        patternGraph.neededEdges.Remove(edge.name);
                    else
                        patternGraph.neededEdges.Remove(NamesOfEntities.CandidateVariable(edge.name));
                }
            }
            foreach(PatternVariable variable in variables)
            {
                if(variable.PointOfDefinition == patternGraph)
                {
                    if(!variable.DefToBeYieldedTo)
                        patternGraph.neededVariables.Remove(variable.name);
                    else
                        patternGraph.neededVariables.Remove(NamesOfEntities.Variable(variable.name));
                }
            }
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
                foreach(PatternGraphEmbedding embedding in patternGraph.embeddedGraphs)
                {
                    PatternGraph embeddedPatternGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
                    if(!embeddedPatternGraph.isPatternGraphOnPathFromEnclosingPatternpath)
                    {
                        embeddedPatternGraph.isPatternGraphOnPathFromEnclosingPatternpath = true;
                        changed = true;
                    }
                }
            }

            foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                changed |= ComputePatternGraphsOnPathFromEnclosingPatternpath(neg, isOnPathFromEnclosingPatternpath);
            }
            foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                changed |= ComputePatternGraphsOnPathFromEnclosingPatternpath(idpt, isOnPathFromEnclosingPatternpath);
            }

            foreach(Alternative alt in patternGraph.alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    changed |= ComputePatternGraphsOnPathFromEnclosingPatternpath(altCase, isOnPathFromEnclosingPatternpath);
                }
            }
            foreach(Iterated iter in patternGraph.iterateds)
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

            foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                ComputePatternGraphsOnPathToEnclosedPatternpath(neg);
                patternGraph.patternGraphsOnPathToEnclosedPatternpath.AddRange(neg.patternGraphsOnPathToEnclosedPatternpath);
            }
            foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                ComputePatternGraphsOnPathToEnclosedPatternpath(idpt);
                patternGraph.patternGraphsOnPathToEnclosedPatternpath.AddRange(idpt.patternGraphsOnPathToEnclosedPatternpath);
            }

            foreach(Alternative alt in patternGraph.alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    ComputePatternGraphsOnPathToEnclosedPatternpath(altCase);
                    AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                        patternGraph.pathPrefix + patternGraph.name,
                        altCase.patternGraphsOnPathToEnclosedPatternpath);
                }
            }
            foreach(Iterated iter in patternGraph.iterateds)
            {
                ComputePatternGraphsOnPathToEnclosedPatternpath(iter.iteratedPattern);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name,
                    iter.iteratedPattern.patternGraphsOnPathToEnclosedPatternpath);
            }

            // one of the nested patterns was found to be on a path
            // to a pattern with patternpath modifier -> so we are/may be too
            // or we are locally because we contain a patternpath modifier
            if(patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count != 0
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

            foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                AddSubpatternInformationToPatternpathInformation(neg);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath, neg.patternGraphsOnPathToEnclosedPatternpath);
            }
            foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                AddSubpatternInformationToPatternpathInformation(idpt);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath, idpt.patternGraphsOnPathToEnclosedPatternpath);
            }

            foreach(Alternative alt in patternGraph.alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    AddSubpatternInformationToPatternpathInformation(altCase);
                    AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                        patternGraph.pathPrefix + patternGraph.name,
                        altCase.patternGraphsOnPathToEnclosedPatternpath);
                }
            }
            foreach(Iterated iter in patternGraph.iterateds)
            {
                AddSubpatternInformationToPatternpathInformation(iter.iteratedPattern);
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name,
                    iter.iteratedPattern.patternGraphsOnPathToEnclosedPatternpath);
            }

            foreach(PatternGraphEmbedding embedding in patternGraph.embeddedGraphs)
            {
                PatternGraph embeddedPatternGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
                AddNotContained(patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    patternGraph.pathPrefix + patternGraph.name,
                    embeddedPatternGraph.patternGraphsOnPathToEnclosedPatternpath);

                foreach(LGSPMatchingPattern calledMatchingPattern in embeddedPatternGraph.usedSubpatterns.Keys)
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
            if(patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count != 0)
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
        private void ComputeSubpatternsUsed(bool inlined)
        {
            // step 1 intra pattern
            // initialize used subpatterns in pattern graph with all locally used subpatterns - top level or nested
            foreach(LGSPMatchingPattern matchingPattern in matchingPatterns)
            {
                matchingPattern.patternGraph.usedSubpatterns =
                    new Dictionary<LGSPMatchingPattern, LGSPMatchingPattern>();
                ComputeSubpatternsUsedLocally(matchingPattern.patternGraph, matchingPattern, inlined);
            }

            // step 2 inter pattern
            // fixed point iteration in order to get the globally / indirectly used subpatterns
            bool subpatternsUsedChanged;
            do
            {
                // for every subpattern used, add all the subpatterns used by these ones to current one
                subpatternsUsedChanged = false;
                foreach(LGSPMatchingPattern matchingPattern in matchingPatterns)
                {
                    subpatternsUsedChanged |= AddSubpatternsOfSubpatternsUsed(matchingPattern);
                }
            } // until nothing changes because transitive closure is found
            while(subpatternsUsedChanged);
        }

        /// <summary>
        /// Computes for given pattern graph all locally used subpatterns;
        /// none of the globally used ones, but all of the nested ones.
        /// Writes them to the used subpatterns member of the pattern graph of the given top level matching pattern.
        /// </summary>
        private void ComputeSubpatternsUsedLocally(PatternGraph patternGraph, LGSPMatchingPattern topLevelMatchingPattern,
            bool inlined)
        {
            // all directly used subpatterns
            PatternGraphEmbedding[] embeddedGraphs = inlined ? patternGraph.embeddedGraphsPlusInlined : patternGraph.embeddedGraphs;
            for(int i = 0; i < embeddedGraphs.Length; ++i)
            {
                if(embeddedGraphs[i].inlined) // skip inlined embeddings
                    continue;
                topLevelMatchingPattern.patternGraph.usedSubpatterns[embeddedGraphs[i].matchingPatternOfEmbeddedGraph] = null;
            }

            // all nested subpattern usages from nested negatives, independents, alternatives, iterateds
            PatternGraph[] negativePatternGraphs = inlined ? patternGraph.negativePatternGraphsPlusInlined : patternGraph.negativePatternGraphs;
            foreach(PatternGraph neg in negativePatternGraphs)
            {
                ComputeSubpatternsUsedLocally(neg, topLevelMatchingPattern, inlined);
            }
            PatternGraph[] independentPatternGraphs = inlined ? patternGraph.independentPatternGraphsPlusInlined : patternGraph.independentPatternGraphs;
            foreach(PatternGraph idpt in independentPatternGraphs)
            {
                ComputeSubpatternsUsedLocally(idpt, topLevelMatchingPattern, inlined);
            }
            Alternative[] alternatives = inlined ? patternGraph.alternativesPlusInlined : patternGraph.alternatives;
            foreach(Alternative alt in alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    ComputeSubpatternsUsedLocally(altCase, topLevelMatchingPattern, inlined);
                }
            }
            Iterated[] iterateds = inlined ? patternGraph.iteratedsPlusInlined : patternGraph.iterateds;
            foreach(Iterated iter in iterateds)
            {
                ComputeSubpatternsUsedLocally(iter.iteratedPattern, topLevelMatchingPattern, inlined);
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
            foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpattern
                in matchingPattern.patternGraph.usedSubpatterns)
            {
                foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpatternOfUsedSubpattern
                    in usedSubpattern.Key.patternGraph.usedSubpatterns)
                {
                    if(!matchingPattern.patternGraph.usedSubpatterns.ContainsKey(usedSubpatternOfUsedSubpattern.Key))
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
        /// Computes the maximum isoSpace number of the pattern graph reached by negative/independent nesting,
        /// clipped by LGSPElemFlags.MAX_ISO_SPACE/LGSPElemFlagsParallel.MAX_ISO_SPACE which is the critical point of interest,
        /// this might happen by heavy nesting or by a subpattern call path with
        /// direct or indirect recursion on it including a negative/independent which gets passed.
        /// Returns true if the max isoSpace of a subpattern called was increased, causing a further run.
        /// Note: If you want to use a higher MAX_ISO_SPACE in either parallel or non-parallel matching, you must split the maxIsoSpace field and this computation into two parts.
        /// Currently the lower one of both is used, which causes insertion of superfluous runtime checks for the higher one. 
        /// (This is not an issue at the time of writing, because both values are set to the same value.)
        /// </summary>
        private bool ComputeMaxIsoSpace(PatternGraph patternGraph, bool inlined)
        {
            PatternGraph[] negativePatternGraphs = inlined ? patternGraph.negativePatternGraphsPlusInlined : patternGraph.negativePatternGraphs;
            foreach(PatternGraph neg in negativePatternGraphs)
            {
                neg.maxIsoSpace = patternGraph.maxIsoSpace + 1;
                ComputeMaxIsoSpace(neg, inlined);
            }
            PatternGraph[] independentPatternGraphs = inlined ? patternGraph.independentPatternGraphsPlusInlined : patternGraph.independentPatternGraphs;
            foreach(PatternGraph idpt in independentPatternGraphs)
            {
                idpt.maxIsoSpace = patternGraph.maxIsoSpace + 1;
                ComputeMaxIsoSpace(idpt, inlined);
            }

            Alternative[] alternatives = inlined ? patternGraph.alternativesPlusInlined : patternGraph.alternatives;
            foreach(Alternative alt in alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    altCase.maxIsoSpace = patternGraph.maxIsoSpace;
                    ComputeMaxIsoSpace(altCase, inlined);
                }
            }
            Iterated[] iterateds = inlined ? patternGraph.iteratedsPlusInlined : patternGraph.iterateds;
            foreach(Iterated iter in patternGraph.iterateds)
            {
                iter.iteratedPattern.maxIsoSpace = patternGraph.maxIsoSpace;
                ComputeMaxIsoSpace(iter.iteratedPattern, inlined);
            }

            bool changed = false;
            PatternGraphEmbedding[] embeddedGraphs = inlined ? patternGraph.embeddedGraphsPlusInlined : patternGraph.embeddedGraphs;
            for(int i = 0; i < embeddedGraphs.Length; ++i)
            {
                if(embeddedGraphs[i].inlined) // skip inlined embeddings
                    continue;
                PatternGraph embeddedPatternGraph = embeddedGraphs[i].matchingPatternOfEmbeddedGraph.patternGraph;
                if(embeddedPatternGraph.maxIsoSpace <= Math.Min((int)LGSPElemFlags.MAX_ISO_SPACE, (int)LGSPElemFlagsParallel.MAX_ISO_SPACE))
                {
                    int oldMaxIsoSpace = embeddedPatternGraph.maxIsoSpace;
                    embeddedPatternGraph.maxIsoSpace = Math.Max(patternGraph.maxIsoSpace, embeddedPatternGraph.maxIsoSpace);
                    if(embeddedPatternGraph.maxIsoSpace > oldMaxIsoSpace)
                        changed = true;
                }
            }

            return changed;
        }

        /// <summary>
        /// Sets branchingFactor to >1 for actions with a parallelize annotation,
        /// but especially for subpatterns used by them, and the nested patterns of both,
        /// they require an additional version with non- is-matched-flag-based isomorphy checking 
        /// the action needs furtheron a very different setup with a work distributing head and a parallelized body
        /// </summary>
        public void SetNeedForParallelizedVersion(LGSPMatchingPattern matchingPattern)
        {
            int branchingFactor = 1;
            foreach(KeyValuePair<string, string> annotation in matchingPattern.Annotations)
            {
                if(annotation.Key != "parallelize")
                    continue;

                if(branchingFactor > 1)
                    ConsoleUI.errorOutWriter.WriteLine("Warning: Further parallelize annotation at " + matchingPattern.patternGraph.Name + " found.");
                
                int value;
                if(!Int32.TryParse(annotation.Value, out value))
                {
                    ConsoleUI.errorOutWriter.WriteLine("Warning: Branching factor at " + matchingPattern.patternGraph.Name + " of parallelize annotation is not a valid integer.");
                    continue;
                }
                if(value < 2)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Warning: Branching factor at " + matchingPattern.patternGraph.Name + " of parallelize annotation is below 2.");
                    continue;
                }
                branchingFactor = value;
            }

            // user wants this action to be parallelized
            if(branchingFactor > 1)
            {
                if(branchingFactor > 64)
                {
                    branchingFactor = 64;
                    ConsoleUI.errorOutWriter.WriteLine("Warning: Branching factor at " + matchingPattern.patternGraph.Name + " of parallelize annotation reduced to the supported maximum of 64.");
                }

                if(ContainsMaybeNullElement(matchingPattern.patternGraph))
                {
                    ConsoleUI.errorOutWriter.WriteLine("Warning: " + matchingPattern.patternGraph.Name + " not parallelized because of the maybe null parameter(s), actions using them cannot be parallelized.");
                    branchingFactor = 1;
                    return;
                }

                // checks passed, this action is to be parallelized
                SetNeedForParallelizedVersion(matchingPattern.patternGraph, branchingFactor);
                // used subpatterns are to be parallelized, too
                foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpattern in matchingPattern.patternGraph.usedSubpatterns)
                {
                    SetNeedForParallelizedVersion(usedSubpattern.Key.patternGraph, branchingFactor);
                }
            }
        }

        public static void SetNeedForParallelizedVersion(PatternGraph patternGraph, int branchingFactor)
        {
            patternGraph.branchingFactor = branchingFactor;

            foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                SetNeedForParallelizedVersion(idpt, branchingFactor);
            }
            foreach(PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
            {
                SetNeedForParallelizedVersion(neg, branchingFactor);
            }

            foreach(Alternative alt in patternGraph.alternativesPlusInlined)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    SetNeedForParallelizedVersion(altCase, branchingFactor);
                }
            }
            foreach(Iterated iter in patternGraph.iteratedsPlusInlined)
            {
                SetNeedForParallelizedVersion(iter.iteratedPattern, branchingFactor);
            }
        }

        public static bool ContainsMaybeNullElement(PatternGraph patternGraph)
        {
            foreach(PatternNode node in patternGraph.nodes)
            {
                if(node.MaybeNull)
                    return true;
            }
            foreach(PatternEdge edge in patternGraph.edges)
            {
                if(edge.MaybeNull)
                    return true;
            }
            return false;
        }

        public static void PrepareInline(PatternGraph patternGraph)
        {
            // fill the inlined fields with the content from the non-inlined fields,
            // so we can just use the inlined fields for the rest of processing (outside of the user interface)
            patternGraph.PrepareInline();

            // walk all patterns used in the current pattern and prepare inline there, too
            foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                PrepareInline(neg);
            }
            foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                PrepareInline(idpt);
            }

            foreach(Alternative alt in patternGraph.alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    PrepareInline(altCase);
                }
            }
            foreach(Iterated iter in patternGraph.iterateds)
            {
                PrepareInline(iter.iteratedPattern);
            }
        }


        const int PATTERN_SIZE_BEYOND_WHICH_TO_INLINE = 7;
        const int OVERALL_PATTERN_SIZE_GROWTH_TO_ACCEPT = 19;

        public void InlineSubpatternUsages(PatternGraph patternGraph)
        {
            // we do one step of inlining only, with the embedding being always replaced by the non-inlined original
            // multiple step inlining would be nice but would require some generalizations in the current code, following chains of original elements instead of one step only
            // unbounded depth inlining would require taking care of direct or indirect recursion, would be a dubious approach

            // pattern path locked patterns would be too much programming effort to inline
            // we shy away from anything in this regard
            if(patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count > 0)
                return;
            foreach(PatternGraphEmbedding embedding in patternGraph.embeddedGraphs)
            {
                if(embedding.matchingPatternOfEmbeddedGraph.patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count > 0)
                    return;
            }

            // for each subpattern used/embedded decide whether to inline or not
            // for the ones to inline do the inlining by adding the original content of the subpattern 
            // directly to the using/embedding pattern
            foreach(PatternGraphEmbedding embedding in patternGraph.embeddedGraphs)
            {
                LGSPMatchingPattern embeddedMatchingPattern = embedding.matchingPatternOfEmbeddedGraph;

                // primary cause for inlining: connectedness, major performance gain if pattern gets connected
                // if pattern is disconnected we ruthlessly inline, maybe it gets connected, if not we still gain because of early pruning
                // secondary cause for inlining: save subpattern matching setup cost, small impact to be balanced against code bloat cost
                if(!IsConnected(patternGraph) 
                    || IsOfLowSelectiveness(patternGraph) // hosting pattern would benefit?
                    || PatternCost(embeddedMatchingPattern.patternGraph) <= PATTERN_SIZE_BEYOND_WHICH_TO_INLINE // small patterns are inlined irrespective of their number of occurances
                    || OverallPatternInliningCost(embeddedMatchingPattern) <= OVERALL_PATTERN_SIZE_GROWTH_TO_ACCEPT) // pattern mul num occurances must be acceptable for the rest
                {
                    // rewrite pattern graph to include the content of the embedded graph
                    // modulo name changes to avoid conflicts
                    InlineSubpatternUsage(patternGraph, embedding);
                }
            }

            // walk all patterns used in the current pattern and inline there, too
            foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                InlineSubpatternUsages(neg);
            }
            foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                InlineSubpatternUsages(idpt);
            }

            foreach(Alternative alt in patternGraph.alternatives)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    InlineSubpatternUsages(altCase);
                }
            }
            foreach(Iterated iter in patternGraph.iterateds)
            {
                InlineSubpatternUsages(iter.iteratedPattern);
            }
        }

        bool IsConnected(PatternGraph patternGraph)
        {
            Dictionary<PatternNode, PatternNode> nodes = new Dictionary<PatternNode, PatternNode>();
            PatternGraph cur = patternGraph;
            do
            {
                foreach(PatternNode node in cur.nodes)
                {
                    nodes[node] = node;
                }
                cur = cur.embeddingGraph;
            }
            while(cur != null);

            Dictionary<PatternNode, List<PatternEdge>> nodeToEdges = new Dictionary<PatternNode, List<PatternEdge>>();
            foreach(KeyValuePair<PatternNode, PatternNode> node in nodes)
            {
                nodeToEdges.Add(node.Key, new List<PatternEdge>());
            }

            Dictionary<PatternEdge, PatternEdge> edges = new Dictionary<PatternEdge, PatternEdge>();
            cur = patternGraph;
            do
            {
                foreach(PatternEdge edge in cur.edges)
                {
                    edges[edge] = edge;

                    PatternNode source = cur.GetSource(edge);
                    if(source!=null && !nodeToEdges[source].Contains(edge))
                        nodeToEdges[source].Add(edge);
                    PatternNode target = cur.GetTarget(edge);
                    if(target != null && !nodeToEdges[target].Contains(edge))
                        nodeToEdges[target].Add(edge);
                }
                cur = cur.embeddingGraph;
            }
            while(cur != null);

            Dictionary<PatternEdge, List<PatternNode>> edgeToNodes = new Dictionary<PatternEdge, List<PatternNode>>();
            foreach(KeyValuePair<PatternEdge,PatternEdge> edge in edges)
            {
                edgeToNodes.Add(edge.Key, new List<PatternNode>());
            }

            cur = patternGraph;
            do
            {
                foreach(PatternEdge edge in cur.edges)
                {
                    PatternNode source = cur.GetSource(edge);
                    if(source!=null && !edgeToNodes[edge].Contains(source))
                        edgeToNodes[edge].Add(source);
                    PatternNode target = cur.GetTarget(edge);
                    if(target != null && !edgeToNodes[edge].Contains(target))
                        edgeToNodes[edge].Add(target);
                }
                cur = cur.embeddingGraph;
            }
            while(cur != null);

            if(nodes.Count==0)
                return edges.Count<=1;

            Dictionary<PatternNode, PatternNode>.Enumerator enumerator = nodes.GetEnumerator();
            enumerator.MoveNext();
            PatternNode root = enumerator.Current.Key;
            root.visited = true;
            Visit(root, nodeToEdges, edgeToNodes);

            bool connected = true;
            foreach(KeyValuePair<PatternNode, PatternNode> node in nodes)
            {
                connected &= node.Key.visited;
            }
            foreach(KeyValuePair<PatternEdge, PatternEdge> edge in edges)
            {
                connected &= edge.Key.visited;
            }

            foreach(KeyValuePair<PatternNode, PatternNode> node in nodes)
            {
                node.Key.visited = false;
            }
            foreach(KeyValuePair<PatternEdge, PatternEdge> edge in edges)
            {
                edge.Key.visited = false;
            }

            return connected;
        }

        void Visit(PatternNode parent, 
            Dictionary<PatternNode, List<PatternEdge>> nodeToEdges, 
            Dictionary<PatternEdge, List<PatternNode>> edgeToNodes)
        {
            foreach(PatternEdge edge in nodeToEdges[parent])
            {
                foreach(PatternNode node in edgeToNodes[edge])
                {
                    if(node == parent)
                        continue;
                    if(node.visited)
                        continue;
                    node.visited = true;
                    Visit(node, nodeToEdges, edgeToNodes);
                }
            }
        }

        const int SELECTIVENESS_THRESHOLD = 5;

        bool IsOfLowSelectiveness(PatternGraph patternGraph)
        {
            // crude approximation: low selectiveness if nothing handed in and pattern is small

            int numHandedIn = 0;
            foreach(PatternNode node in patternGraph.nodes)
            {
                if(node.ParameterIndex != -1 || node.pointOfDefinition != patternGraph)
                    ++numHandedIn;
            }
            foreach(PatternEdge edge in patternGraph.edges)
            {
                if(edge.ParameterIndex != -1 || edge.pointOfDefinition != patternGraph)
                    ++numHandedIn;
            }

            if(numHandedIn == patternGraph.nodes.Length + patternGraph.edges.Length)
                return false; // all handed in? -> nothing to search locally, no selectivity problem
            else if(numHandedIn == 0)
                // we must search the entire graph, a tiny pattern would benefit from growing
                return patternGraph.nodes.Length + patternGraph.edges.Length <= SELECTIVENESS_THRESHOLD;
            else
                return false; // one element handed in -> we can search from that element on
        }

        private int OverallPatternInliningCost(LGSPMatchingPattern embeddedMatchingPattern)
        {
            // compute size of pattern graph and count all the occurences            
            return PatternCost(embeddedMatchingPattern.patternGraph) * embeddedMatchingPattern.uses;
        }

        private int PatternCost(PatternGraph patternGraph)
        {
            int cost = patternGraph.nodes.Length + patternGraph.edges.Length;

            foreach(PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                cost += 1 + PatternCost(neg);
            }
            foreach(PatternGraph idpt in patternGraph.independentPatternGraphs)
            {
                cost += 1 + PatternCost(idpt);
            }

            foreach(Alternative alt in patternGraph.alternatives)
            {
                int maxSize = 0;
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    maxSize = Math.Max(PatternCost(altCase), maxSize);
                }
                cost += 1 + maxSize;
            }
            foreach(Iterated iter in patternGraph.iterateds)
            {
                cost += 1 + PatternCost(iter.iteratedPattern);
            }

            for(int i = 0; i < patternGraph.embeddedGraphs.Length; ++i)
            {
                PatternGraph embeddedPatternGraph = patternGraph.embeddedGraphs[i].matchingPatternOfEmbeddedGraph.patternGraph;
                cost += 1 + embeddedPatternGraph.nodes.Length + embeddedPatternGraph.edges.Length 
                    + embeddedPatternGraph.negativePatternGraphs.Length + embeddedPatternGraph.independentPatternGraphs.Length
                    + embeddedPatternGraph.alternatives.Length + embeddedPatternGraph.iterateds.Length
                    + embeddedPatternGraph.embeddedGraphs.Length;
            }

            return cost;
        }

        void InlineSubpatternUsage(PatternGraph patternGraph, PatternGraphEmbedding embedding)
        {
            PatternGraph embeddedGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
            string renameSuffix = "_inlined_" + embedding.Name + "_" + (renameId++);

            Dictionary<PatternNode, PatternNode> nodeToCopy = new Dictionary<PatternNode,PatternNode>();
            Dictionary<PatternEdge, PatternEdge> edgeToCopy = new Dictionary<PatternEdge,PatternEdge>();
            Dictionary<PatternVariable, PatternVariable> variableToCopy = new Dictionary<PatternVariable,PatternVariable>();
            CopyNodesEdgesVariablesOfSubpattern(patternGraph, embedding, renameSuffix, 
                nodeToCopy, edgeToCopy, variableToCopy);

            patternGraph.PatchUsersOfCopiedElements(renameSuffix, nodeToCopy, edgeToCopy, variableToCopy);

            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndSource in embeddedGraph.edgeToSourceNode)
            {
                patternGraph.edgeToSourceNodePlusInlined.Add(edgeToCopy[edgeAndSource.Key], nodeToCopy[edgeAndSource.Value]);
            }
            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndTarget in embeddedGraph.edgeToTargetNode)
            {
                patternGraph.edgeToTargetNodePlusInlined.Add(edgeToCopy[edgeAndTarget.Key], nodeToCopy[edgeAndTarget.Value]);
            }

            if(embeddedGraph.nodes.Length > 0)
            {
                patternGraph.homomorphicNodes = ExtendHomMatrix(patternGraph.homomorphicNodes, embeddedGraph.homomorphicNodes, embeddedGraph.nodes.Length);
                patternGraph.homomorphicNodesGlobal = ExtendHomMatrix(patternGraph.homomorphicNodesGlobal, embeddedGraph.homomorphicNodesGlobal, embeddedGraph.nodes.Length);
                patternGraph.totallyHomomorphicNodes = ExtendTotallyHomArray(patternGraph.totallyHomomorphicNodes, embeddedGraph.totallyHomomorphicNodes, embeddedGraph.nodes.Length);

                // make arguments/connections of original pattern graph hom to parameters of embedded graph
                for(int i = 0; i < embedding.connections.Length; ++i)
                {
                    PatternNode nodeArgument = getBoundNode(embedding, i, patternGraph.nodesPlusInlined);
                    if(nodeArgument == null)
                        continue;
                    PatternNode nodeParameter = getParameterNode(embeddedGraph.nodes, i);
                    PatternNode nodeParameterCopy = nodeToCopy[nodeParameter];
                    int indexOfArgument = Array.IndexOf(patternGraph.nodesPlusInlined, nodeArgument);
                    int indexOfParameterCopy = Array.IndexOf(patternGraph.nodesPlusInlined, nodeParameterCopy);
                    patternGraph.homomorphicNodes[indexOfArgument, indexOfParameterCopy] = true;
                    patternGraph.homomorphicNodes[indexOfParameterCopy, indexOfArgument] = true;
                }
            }

            if(embeddedGraph.edges.Length > 0)
            {
                patternGraph.homomorphicEdges = ExtendHomMatrix(patternGraph.homomorphicEdges, embeddedGraph.homomorphicEdges, embeddedGraph.edges.Length);
                patternGraph.homomorphicEdgesGlobal = ExtendHomMatrix(patternGraph.homomorphicEdgesGlobal, embeddedGraph.homomorphicEdgesGlobal, embeddedGraph.edges.Length);
                patternGraph.totallyHomomorphicEdges = ExtendTotallyHomArray(patternGraph.totallyHomomorphicEdges, embeddedGraph.totallyHomomorphicEdges, embeddedGraph.edges.Length);

                // make arguments/connections of original pattern graph hom to parameters of embedded graph
                for(int i = 0; i < embedding.connections.Length; ++i)
                {
                    PatternEdge edgeArgument = getBoundEdge(embedding, i, patternGraph.edgesPlusInlined);
                    if(edgeArgument == null)
                        continue;
                    PatternEdge edgeParameter = getParameterEdge(embeddedGraph.edges, i);
                    PatternEdge edgeParameterCopy = edgeToCopy[edgeParameter];
                    int indexOfArgument = Array.IndexOf(patternGraph.edgesPlusInlined, edgeArgument);
                    int indexOfParameterCopy = Array.IndexOf(patternGraph.edgesPlusInlined, edgeParameterCopy);
                    patternGraph.homomorphicEdges[indexOfArgument, indexOfParameterCopy] = true;
                    patternGraph.homomorphicEdges[indexOfParameterCopy, indexOfArgument] = true;
                }
            }

            CopyConditionsYieldingsOfSubpattern(patternGraph, embedding, renameSuffix,
                nodeToCopy, edgeToCopy, variableToCopy);

            CopyNegativesIndependentsOfSubpattern(patternGraph, embedding, renameSuffix,
                nodeToCopy, edgeToCopy, variableToCopy);

            CopyAlternativesIteratedsOfSubpattern(patternGraph, embedding, renameSuffix,
                nodeToCopy, edgeToCopy, variableToCopy);

            CopySubpatternUsagesOfSubpattern(patternGraph, embedding, embeddedGraph, renameSuffix, 
                nodeToCopy, edgeToCopy, variableToCopy);

            // TODO: das zeugs das vom analyzer berechnet wird, das bei der konstruktion berechnet wird
            // TODO: einfach für alles nicht-konstante PlusInlined einrichten, damit das schön getrennt ist, keine Probleme auftreten können; das hom extended ist unsauber
            // TODO: die copy-konstruktoren prüfen, über die das rekursive kopieren erfolgt

            embedding.inlined = true;
        }

        private static void CopyNodesEdgesVariablesOfSubpattern(PatternGraph patternGraph, 
            PatternGraphEmbedding embedding, string renameSuffix, 
            Dictionary<PatternNode, PatternNode> nodeToCopy, 
            Dictionary<PatternEdge, PatternEdge> edgeToCopy, 
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            PatternGraph embeddedGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
            if(embeddedGraph.nodes.Length > 0)
            {
                PatternNode[] newNodes = new PatternNode[patternGraph.nodesPlusInlined.Length + embeddedGraph.nodes.Length];
                patternGraph.nodesPlusInlined.CopyTo(newNodes, 0);
                for(int i = 0; i < embeddedGraph.nodes.Length; ++i)
                {
                    PatternNode node = embeddedGraph.nodes[i];
                    PatternNode newNode = new PatternNode(node, embedding, patternGraph, renameSuffix);
                    newNodes[patternGraph.nodesPlusInlined.Length + i] = newNode;
                    nodeToCopy[node] = newNode;

                    if(node.pointOfDefinition==null)
                    {
                        newNode.AssignmentSource = getBoundNode(embedding, node.ParameterIndex,
                            patternGraph.nodesPlusInlined);
                    }
                }
                patternGraph.nodesPlusInlined = newNodes;
            }
            if(embeddedGraph.edges.Length > 0)
            {
                PatternEdge[] newEdges = new PatternEdge[patternGraph.edgesPlusInlined.Length + embeddedGraph.edges.Length];
                patternGraph.edgesPlusInlined.CopyTo(newEdges, 0);
                for(int i = 0; i < embeddedGraph.edges.Length; ++i)
                {
                    PatternEdge edge = embeddedGraph.edges[i];
                    PatternEdge newEdge = new PatternEdge(edge, embedding, patternGraph, renameSuffix);
                    newEdges[patternGraph.edgesPlusInlined.Length + i] = newEdge;
                    edgeToCopy[edge] = newEdge;

                    if(edge.pointOfDefinition==null)
                    {
                        newEdge.AssignmentSource = getBoundEdge(embedding, edge.ParameterIndex,
                            patternGraph.edgesPlusInlined);
                    }
                }
                patternGraph.edgesPlusInlined = newEdges;
            }
            if(embeddedGraph.variables.Length > 0)
            {
                PatternVariable[] newVariables = new PatternVariable[patternGraph.variablesPlusInlined.Length + embeddedGraph.variables.Length];
                patternGraph.variablesPlusInlined.CopyTo(newVariables, 0);
                for(int i = 0; i < embeddedGraph.variables.Length; ++i)
                {
                    PatternVariable variable = embeddedGraph.variables[i];
                    PatternVariable newVariable = new PatternVariable(variable, embedding, patternGraph, renameSuffix);
                    newVariables[patternGraph.variablesPlusInlined.Length + i] = newVariable;
                    variableToCopy[variable] = newVariable;

                    if(variable.pointOfDefinition==null)
                    {
                        if(!variable.defToBeYieldedTo)
                        {
                            newVariable.AssignmentSource = embedding.connections[variable.ParameterIndex];
                            newVariable.AssignmentDependencies = embedding;
                        }
                    }
                }
                patternGraph.variablesPlusInlined = newVariables;
            }
        }

        private static void CopyConditionsYieldingsOfSubpattern(PatternGraph patternGraph,
            PatternGraphEmbedding embedding, string renameSuffix,
            Dictionary<PatternNode, PatternNode> nodeToCopy,
            Dictionary<PatternEdge, PatternEdge> edgeToCopy,
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            PatternGraph embeddedGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
            if(embeddedGraph.Conditions.Length > 0)
            {
                PatternCondition[] newConditions = new PatternCondition[patternGraph.ConditionsPlusInlined.Length + embeddedGraph.Conditions.Length];
                patternGraph.ConditionsPlusInlined.CopyTo(newConditions, 0);
                for(int i = 0; i < embeddedGraph.Conditions.Length; ++i)
                {
                    PatternCondition cond = embeddedGraph.Conditions[i];
                    PatternCondition newCond = new PatternCondition(cond, embedding, renameSuffix);
                    newConditions[patternGraph.ConditionsPlusInlined.Length + i] = newCond;
                }
                patternGraph.ConditionsPlusInlined = newConditions;
            }
            if(embeddedGraph.Yieldings.Length > 0)
            {
                PatternYielding[] newYieldings = new PatternYielding[patternGraph.YieldingsPlusInlined.Length + embeddedGraph.Yieldings.Length];
                patternGraph.YieldingsPlusInlined.CopyTo(newYieldings, 0);
                for(int i = 0; i < embeddedGraph.Yieldings.Length; ++i)
                {
                    PatternYielding yield = embeddedGraph.Yieldings[i];
                    PatternYielding newYield = new PatternYielding(yield, embedding, renameSuffix);
                    newYieldings[patternGraph.YieldingsPlusInlined.Length + i] = newYield;
                }
                patternGraph.YieldingsPlusInlined = newYieldings;
            }
        }

        private static void CopyNegativesIndependentsOfSubpattern(PatternGraph patternGraph, 
            PatternGraphEmbedding embedding, string renameSuffix, 
            Dictionary<PatternNode, PatternNode> nodeToCopy, 
            Dictionary<PatternEdge, PatternEdge> edgeToCopy, 
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            PatternGraph embeddedGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
            if(embeddedGraph.negativePatternGraphs.Length > 0)
            {
                PatternGraph[] newNegativePatternGraphs = new PatternGraph[patternGraph.negativePatternGraphsPlusInlined.Length + embeddedGraph.negativePatternGraphs.Length];
                patternGraph.negativePatternGraphsPlusInlined.CopyTo(newNegativePatternGraphs, 0);
                for(int i = 0; i < embeddedGraph.negativePatternGraphs.Length; ++i)
                {
                    PatternGraph neg = embeddedGraph.negativePatternGraphs[i];
                    PatternGraph newNeg = new PatternGraph(neg, embedding, patternGraph, renameSuffix,
                        nodeToCopy, edgeToCopy, variableToCopy);
                    newNegativePatternGraphs[patternGraph.negativePatternGraphsPlusInlined.Length + i] = newNeg;
                }
                patternGraph.negativePatternGraphsPlusInlined = newNegativePatternGraphs;
            }
            if(embeddedGraph.independentPatternGraphs.Length > 0)
            {
                PatternGraph[] newIndependentPatternGraphs = new PatternGraph[patternGraph.independentPatternGraphsPlusInlined.Length + embeddedGraph.independentPatternGraphs.Length];
                patternGraph.independentPatternGraphsPlusInlined.CopyTo(newIndependentPatternGraphs, 0);
                for(int i = 0; i < embeddedGraph.independentPatternGraphs.Length; ++i)
                {
                    PatternGraph idpt = embeddedGraph.independentPatternGraphs[i];
                    PatternGraph newIdpt = new PatternGraph(idpt, embedding, patternGraph, renameSuffix,
                        nodeToCopy, edgeToCopy, variableToCopy);
                    newIndependentPatternGraphs[patternGraph.independentPatternGraphsPlusInlined.Length + i] = newIdpt;
                }
                patternGraph.independentPatternGraphsPlusInlined = newIndependentPatternGraphs;
            }
        }

        private static void CopyAlternativesIteratedsOfSubpattern(PatternGraph patternGraph, 
            PatternGraphEmbedding embedding, string renameSuffix, 
            Dictionary<PatternNode, PatternNode> nodeToCopy, 
            Dictionary<PatternEdge, PatternEdge> edgeToCopy, 
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            PatternGraph embeddedGraph = embedding.matchingPatternOfEmbeddedGraph.patternGraph;
            if(embeddedGraph.alternatives.Length > 0)
            {
                Alternative[] newAlternatives = new Alternative[patternGraph.alternativesPlusInlined.Length + embeddedGraph.alternatives.Length];
                patternGraph.alternativesPlusInlined.CopyTo(newAlternatives, 0);
                for(int i = 0; i < embeddedGraph.alternatives.Length; ++i)
                {
                    Alternative alt = embeddedGraph.alternatives[i];
                    Alternative newAlt = new Alternative(alt, embedding, patternGraph, renameSuffix, patternGraph.pathPrefix + patternGraph.name + "_",
                        nodeToCopy, edgeToCopy, variableToCopy);
                    newAlternatives[patternGraph.alternativesPlusInlined.Length + i] = newAlt;
                }
                patternGraph.alternativesPlusInlined = newAlternatives;
            }
            if(embeddedGraph.iterateds.Length > 0)
            {
                Iterated[] newIterateds = new Iterated[patternGraph.iteratedsPlusInlined.Length + embeddedGraph.iterateds.Length];
                patternGraph.iteratedsPlusInlined.CopyTo(newIterateds, 0);
                for(int i = 0; i < embeddedGraph.iterateds.Length; ++i)
                {
                    Iterated iter = embeddedGraph.iterateds[i];
                    Iterated newIter = new Iterated(iter, embedding, patternGraph, renameSuffix,
                        nodeToCopy, edgeToCopy, variableToCopy);
                    newIterateds[patternGraph.iteratedsPlusInlined.Length + i] = newIter;
                }
                patternGraph.iteratedsPlusInlined = newIterateds;
            }
        }

        private static void CopySubpatternUsagesOfSubpattern(PatternGraph patternGraph, PatternGraphEmbedding embedding, 
            PatternGraph embeddedGraph, string renameSuffix, 
            Dictionary<PatternNode, PatternNode> nodeToCopy, 
            Dictionary<PatternEdge, PatternEdge> edgeToCopy, 
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            if(embeddedGraph.embeddedGraphs.Length > 0)
            {
                PatternGraphEmbedding[] newEmbeddings = new PatternGraphEmbedding[patternGraph.embeddedGraphsPlusInlined.Length + embeddedGraph.embeddedGraphs.Length];
                patternGraph.embeddedGraphsPlusInlined.CopyTo(newEmbeddings, 0);
                for(int i = 0; i < embeddedGraph.embeddedGraphs.Length; ++i)
                {
                    PatternGraphEmbedding sub = embeddedGraph.embeddedGraphs[i];
                    PatternGraphEmbedding newSub = new PatternGraphEmbedding(sub, embedding, patternGraph, renameSuffix);
                    newEmbeddings[patternGraph.embeddedGraphsPlusInlined.Length + i] = newSub;
                }
                patternGraph.embeddedGraphsPlusInlined = newEmbeddings;
            }
        }

        private static PatternNode getBoundNode(PatternGraphEmbedding embedding, int parameterIndex,
            PatternNode[] nodes)
        {
            Expression exp = embedding.connections[parameterIndex];
            if(!(exp is GraphEntityExpression))
                return null;
            GraphEntityExpression elem = (GraphEntityExpression)exp;
            foreach(PatternNode node in nodes)
            {
                if(node.name == elem.Entity)
                    return node;
            }
            return null;
        }

        private static PatternEdge getBoundEdge(PatternGraphEmbedding embedding, int parameterIndex,
            PatternEdge[] edges)
        {
            Expression exp = embedding.connections[parameterIndex];
            if(!(exp is GraphEntityExpression))
                return null;
            GraphEntityExpression elem = (GraphEntityExpression)exp;
            foreach(PatternEdge edge in edges)
            {
                if(edge.name == elem.Entity)
                    return edge;
            }
            return null;
        }

        private static PatternNode getParameterNode(PatternNode[] nodes, int parameterIndex)
        {
            foreach(PatternNode node in nodes)
            {
                if(node.ParameterIndex == parameterIndex)
                    return node;
            }

            return null;
        }

        private static PatternEdge getParameterEdge(PatternEdge[] edges, int parameterIndex)
        {
            foreach(PatternEdge edge in edges)
            {
                if(edge.ParameterIndex == parameterIndex)
                    return edge;
            }

            return null;
        }

        // extensionLength prevents us from copying the hom-information of already inlined stuff if the pattern to be inlined was inlined before
        bool[,] ExtendHomMatrix(bool[,] homOriginal, bool[,] homExtension, int extensionLength)
        {
            int numOld = homOriginal.GetLength(0);
            int numNew = extensionLength;
            int numTotal = numOld + numNew;

            bool[,] newHom = new bool[numTotal, numTotal];

            // copy-write left-upper part from original matrix
            for(int i = 0; i < numOld; ++i)
            {
                for(int j = 0; j < numOld; ++j)
                {
                    newHom[i, j] = homOriginal[i, j];
                }
            }

            // copy-write right-lower part from extension matrix
            for(int i = 0; i < numNew; ++i)
            {
                for(int j = 0; j < numNew; ++j)
                {
                    newHom[numOld + i, numOld + j] = homExtension[i, j];
                }
            }

            // fill-write left-lower part with false, so old and new elments are iso 
            for(int i = 0; i < numOld; ++i)
            {
                for(int j = 0; j < numNew; ++j)
                {
                    newHom[i, numOld + j] = false;
                }
            }

            // fill-write right-upper part with false, so old and new elements are iso
            for(int i = 0; i < numNew; ++i)
            {
                for(int j = 0; j < numOld; ++j)
                {
                    newHom[numOld + i, j] = false;
                }
            }

            return newHom;
        }

        // extensionLength prevents us from copying the hom-information of already inlined stuff if the pattern to be inlined was inlined before
        bool[] ExtendTotallyHomArray(bool[] totallyHomOriginal, bool[] totallyHomExtension, int extensionLength)
        {
            bool[] newTotallyHom = new bool[totallyHomOriginal.Length + extensionLength];
            totallyHomOriginal.CopyTo(newTotallyHom, 0);
            for(int i = 0; i < extensionLength; ++i)
            {
                newTotallyHom[totallyHomOriginal.Length + i] = totallyHomExtension[i];
            }
            return newTotallyHom;
        }

        // ----------------------------------------------------------------

        private readonly List<LGSPMatchingPattern> matchingPatterns;
        private int renameId = 0; // an id to ensure that inlined elements of a subpattern are named differently for multiple instances of the inlined pattern
    }
}
