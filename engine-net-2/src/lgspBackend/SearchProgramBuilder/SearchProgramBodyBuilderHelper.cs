/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

//#define NO_ADJUST_LIST_HEADS

using System;
using System.Collections.Generic;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.expression;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// the types of match objects there are, to be filled by insertMatchObjectBuilding
    /// </summary>
    public enum MatchObjectType
    {
        Normal,
        Independent,
        Patternpath
    }

    /// <summary>
    /// class for building search program data structure from scheduled search plan
    /// for all kinds of matchers, called by search program builder, 
    /// holds environment variables for this build process
    /// </summary>
    class SearchProgramBodyBuilderHelper
    {
        public SearchProgramBodyBuilderHelper(SearchProgramBodyBuilderHelperEnvironment env)
        {
            this.env = env;
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// type of the program which gets currently built
        /// </summary>
        private readonly SearchProgramBodyBuilderHelperEnvironment env;

        /// <summary>
        /// name says everything
        /// </summary>
        private const int MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID = 2;

        ///////////////////////////////////////////////////////////////////////////////////

        public bool wasIndependentInlined(PatternGraph patternGraph, int index)
        {
            SearchOperation[] operations;
            if(env.parallelized)
                operations = patternGraph.parallelizedSchedule[index].Operations;
            else
                operations = patternGraph.schedulesIncludingNegativesAndIndependents[env.indexOfSchedule].Operations;

            for(int i = 0; i < operations.Length; ++i)
            {
                if(operations[i].Element is SearchPlanNode
                    && ((SearchPlanNode)operations[i].Element).PatternElement.OriginalIndependentElement != null)
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<PatternElement> patternElementsMatchedThere(PatternGraph patternGraph)
        {
            SearchOperation[] operations;
            if(env.parallelized)
                operations = patternGraph.parallelizedSchedule[env.indexOfSchedule].Operations;
            else
                operations = patternGraph.schedulesIncludingNegativesAndIndependents[env.indexOfSchedule].Operations;

            for(int i = 0; i < operations.Length; ++i)
            {
                if(operations[i].Element is SearchPlanNode)
                {
                    SearchPlanNode spn = ((SearchPlanNode)operations[i].Element);
                    if(spn.PatternElement.PointOfDefinition == patternGraph && !spn.PatternElement.defToBeYieldedTo)
                    {
                        yield return spn.PatternElement;
                    }
                }
            }
        }

        /// <summary>
        /// Inserts code to get an implicit node from an edge
        /// </summary>
        public SearchProgramOperation insertImplicitNodeFromEdge(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode currentNode,
            ImplicitNodeType nodeType,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            GetCandidateByDrawing nodeFromEdge;
            if(nodeType == ImplicitNodeType.Source || nodeType == ImplicitNodeType.Target)
            {
                nodeFromEdge = new GetCandidateByDrawing(
                    GetCandidateByDrawingType.NodeFromEdge,
                    currentNode.PatternElement.Name,
                    TypesHelper.XgrsTypeToCSharpType(env.model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName, env.model),
                    edge.PatternElement.Name,
                    nodeType);
                insertionPoint = insertionPoint.Append(nodeFromEdge);
            }
            else
            {
                Debug.Assert(nodeType != ImplicitNodeType.TheOther);

                if(currentNodeIsSecondIncidentNodeOfEdge(currentNode, edge))
                {
                    nodeFromEdge = new GetCandidateByDrawing(
                        GetCandidateByDrawingType.NodeFromEdge,
                        currentNode.PatternElement.Name,
                        TypesHelper.XgrsTypeToCSharpType(env.model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName, env.model),
                        edge.PatternElement.Name,
                        edge.PatternEdgeSource == currentNode ? edge.PatternEdgeTarget.PatternElement.Name
                            : edge.PatternEdgeSource.PatternElement.Name,
                        ImplicitNodeType.TheOther);
                    insertionPoint = insertionPoint.Append(nodeFromEdge);
                }
                else // edge connects to first incident node
                {
                    if(edge.PatternEdgeSource == edge.PatternEdgeTarget)
                    {
                        // reflexive edge without direction iteration as we don't want 2 matches 
                        nodeFromEdge = new GetCandidateByDrawing(
                            GetCandidateByDrawingType.NodeFromEdge,
                            currentNode.PatternElement.Name,
                            TypesHelper.XgrsTypeToCSharpType(env.model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName, env.model),
                            edge.PatternElement.Name,
                            ImplicitNodeType.Source);
                        insertionPoint = insertionPoint.Append(nodeFromEdge);
                    }
                    else
                    {
                        BothDirectionsIteration directionsIteration =
                            new BothDirectionsIteration(edge.PatternElement.Name);
                        directionsIteration.NestedOperationsList = new SearchProgramList(directionsIteration);
                        continuationPoint = insertionPoint.Append(directionsIteration);
                        insertionPoint = directionsIteration.NestedOperationsList;

                        nodeFromEdge = new GetCandidateByDrawing(
                            GetCandidateByDrawingType.NodeFromEdge,
                            currentNode.PatternElement.Name,
                            TypesHelper.XgrsTypeToCSharpType(env.model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName, env.model),
                            edge.PatternElement.Name,
                            ImplicitNodeType.SourceOrTarget);
                        insertionPoint = insertionPoint.Append(nodeFromEdge);
                    }
                }
            }

            if(continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to create the match objects of the inlined subpatterns
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertInlinedMatchObjectCreation(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            MatchObjectType matchObjectType)
        {
            String matchOfEnclosingPatternName;
            if(matchObjectType == MatchObjectType.Independent)
                matchOfEnclosingPatternName = NamesOfEntities.MatchedIndependentVariable(patternGraph.pathPrefix + patternGraph.name);
            else //if(matchObjectType==MatchObjectType.Normal)
                matchOfEnclosingPatternName = "match";

            for(int i = 0; i < patternGraph.embeddedGraphsPlusInlined.Length; ++i)
            {
                if(patternGraph.embeddedGraphsPlusInlined[i].inlined)
                {
                    LGSPMatchingPattern matchingPattern = patternGraph.embeddedGraphsPlusInlined[i].matchingPatternOfEmbeddedGraph;
                    string inlinedPatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, matchingPattern.PatternGraph.Package, true);
                    insertionPoint = insertionPoint.Append(
                        new CreateInlinedSubpatternMatch(
                            inlinedPatternClassName,
                            matchingPattern.patternGraph.pathPrefix + matchingPattern.patternGraph.name,
                            patternGraph.embeddedGraphsPlusInlined[i].Name,
                            matchOfEnclosingPatternName)
                    );
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to build the match object
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertMatchObjectBuilding(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            MatchObjectType matchObjectType,
            bool defElements)
        {
            String matchObjectName;
            if(matchObjectType==MatchObjectType.Independent)
                matchObjectName = NamesOfEntities.MatchedIndependentVariable(patternGraph.pathPrefix + patternGraph.name);
            else if(matchObjectType==MatchObjectType.Patternpath)
                matchObjectName = NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
            else //if(matchObjectType==MatchObjectType.Normal)
                matchObjectName = "match";
            String enumPrefix = patternGraph.pathPrefix + patternGraph.name + "_";

            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                // in defElements pass only def elements, in non defElements pass only non def elements
                if(defElements == patternGraph.nodesPlusInlined[i].defToBeYieldedTo)
                {
                    string inlinedMatchObjectName = null;
                    string unprefixedName = patternGraph.nodesPlusInlined[i].UnprefixedName;
                    if(patternGraph.nodesPlusInlined[i].originalNode != null)
                    {
                        if(patternGraph.WasInlinedHere(patternGraph.nodesPlusInlined[i].originalSubpatternEmbedding))
                            inlinedMatchObjectName = "match_" + patternGraph.nodesPlusInlined[i].originalSubpatternEmbedding.Name;
                        unprefixedName = patternGraph.nodesPlusInlined[i].originalElement.UnprefixedName;
                    }
                    BuildMatchObject buildMatch =
                        new BuildMatchObject(
                            BuildMatchObjectType.Node,
                            patternGraph.nodesPlusInlined[i].typeName,
                            unprefixedName,
                            patternGraph.nodesPlusInlined[i].Name,
                            env.rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                // in defElements pass only def elements, in non defElements pass only non def elements
                if(defElements == patternGraph.edgesPlusInlined[i].defToBeYieldedTo)
                {
                    string inlinedMatchObjectName = null;
                    string unprefixedName = patternGraph.edgesPlusInlined[i].UnprefixedName;
                    if(patternGraph.edgesPlusInlined[i].originalEdge != null)
                    {
                        if(patternGraph.WasInlinedHere(patternGraph.edgesPlusInlined[i].originalSubpatternEmbedding))
                            inlinedMatchObjectName = "match_" + patternGraph.edgesPlusInlined[i].originalSubpatternEmbedding.Name;
                        unprefixedName = patternGraph.edgesPlusInlined[i].originalElement.UnprefixedName;
                    }
                    BuildMatchObject buildMatch =
                        new BuildMatchObject(
                            BuildMatchObjectType.Edge,
                            patternGraph.edgesPlusInlined[i].typeName,
                            unprefixedName,
                            patternGraph.edgesPlusInlined[i].Name,
                            env.rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }

            // only nodes and edges for patternpath matches
            if(matchObjectType == MatchObjectType.Patternpath)
                return insertionPoint;

            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                // in defElements pass only def elements, in non defElements pass only non def elements
                if(defElements == var.defToBeYieldedTo)
                {
                    string inlinedMatchObjectName = null;
                    string unprefixedName = var.UnprefixedName;
                    if(var.originalVariable != null)
                    {
                        if(patternGraph.WasInlinedHere(var.originalSubpatternEmbedding))
                            inlinedMatchObjectName = "match_" + var.originalSubpatternEmbedding.Name;
                        unprefixedName = var.originalVariable.UnprefixedName;
                    }
                    BuildMatchObject buildMatch =
                        new BuildMatchObject(
                            BuildMatchObjectType.Variable,
                            TypesHelper.TypeName(var.type),
                            unprefixedName,
                            var.Name,
                            env.rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }

            // only nodes, edges, variables in defElements pass
            if(defElements)
                return insertionPoint;

            for(int i = 0; i < patternGraph.embeddedGraphsPlusInlined.Length; ++i)
            {
                if(patternGraph.embeddedGraphsPlusInlined[i].inlined)
                {
                    BuildMatchObject buildMatch =
                        new BuildMatchObject(
                            BuildMatchObjectType.InlinedSubpattern,
                            "",
                            patternGraph.embeddedGraphsPlusInlined[i].name,
                            patternGraph.embeddedGraphsPlusInlined[i].name,
                            "",
                            "",
                            matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
                else
                {
                    string inlinedMatchObjectName = null;
                    string unprefixedName = patternGraph.embeddedGraphsPlusInlined[i].name;
                    if(patternGraph.embeddedGraphsPlusInlined[i].originalEmbedding != null)
                    {
                        if(patternGraph.WasInlinedHere(patternGraph.embeddedGraphsPlusInlined[i].originalSubpatternEmbedding))
                            inlinedMatchObjectName = "match_" + patternGraph.embeddedGraphsPlusInlined[i].originalSubpatternEmbedding.Name;
                        unprefixedName = patternGraph.embeddedGraphsPlusInlined[i].originalEmbedding.name;
                    }
                    string subpatternContainingType = NamesOfEntities.RulePatternClassName(patternGraph.embeddedGraphsPlusInlined[i].EmbeddedGraph.Name, patternGraph.embeddedGraphsPlusInlined[i].EmbeddedGraph.Package, true);
                    string subpatternType = NamesOfEntities.MatchClassName(patternGraph.embeddedGraphsPlusInlined[i].EmbeddedGraph.Name);
                    BuildMatchObject buildMatch =
                        new BuildMatchObject(
                            BuildMatchObjectType.Subpattern,
                            subpatternContainingType + "." + subpatternType,
                            unprefixedName,
                            patternGraph.embeddedGraphsPlusInlined[i].name,
                            env.rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }
            for(int i = 0; i < patternGraph.iteratedsPlusInlined.Length; ++i)
            {
                string inlinedMatchObjectName = null;
                string unprefixedName = patternGraph.iteratedsPlusInlined[i].iteratedPattern.name;
                string inlinedPatternClassName = env.rulePatternClassName;
                string patternElementType = patternGraph.pathPrefix + patternGraph.name + "_" + patternGraph.iteratedsPlusInlined[i].iteratedPattern.name; 
                if(patternGraph.iteratedsPlusInlined[i].originalIterated != null)
                {
                    if(patternGraph.WasInlinedHere(patternGraph.iteratedsPlusInlined[i].originalSubpatternEmbedding))
                        inlinedMatchObjectName = "match_" + patternGraph.iteratedsPlusInlined[i].originalSubpatternEmbedding.Name;
                    unprefixedName = patternGraph.iteratedsPlusInlined[i].originalIterated.iteratedPattern.Name;
                    inlinedPatternClassName = patternGraph.iteratedsPlusInlined[i].originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                    patternElementType = patternGraph.iteratedsPlusInlined[i].originalIterated.iteratedPattern.pathPrefix + unprefixedName;
                }
                BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Iteration,
                        patternElementType,
                        unprefixedName,
                        patternGraph.iteratedsPlusInlined[i].iteratedPattern.name,
                        inlinedPatternClassName,
                        enumPrefix,
                        inlinedMatchObjectName ?? matchObjectName,
                        patternGraph.embeddedGraphsPlusInlined.Length
                    );
                insertionPoint = insertionPoint.Append(buildMatch);
            }
            for(int i = 0; i < patternGraph.alternativesPlusInlined.Length; ++i)
            {
                string inlinedMatchObjectName = null;
                string unprefixedName = patternGraph.alternativesPlusInlined[i].name;
                string inlinedPatternClassName = env.rulePatternClassName;
                string patternElementType = patternGraph.pathPrefix+patternGraph.name+"_"+patternGraph.alternativesPlusInlined[i].name;
                if(patternGraph.alternativesPlusInlined[i].originalAlternative != null)
                {
                    if(patternGraph.WasInlinedHere(patternGraph.alternativesPlusInlined[i].originalSubpatternEmbedding))
                        inlinedMatchObjectName = "match_" + patternGraph.alternativesPlusInlined[i].originalSubpatternEmbedding.Name;
                    unprefixedName = patternGraph.alternativesPlusInlined[i].originalAlternative.name;
                    inlinedPatternClassName = patternGraph.alternativesPlusInlined[i].originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                    patternElementType = patternGraph.alternativesPlusInlined[i].originalAlternative.pathPrefix + unprefixedName;
                }
                BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Alternative,
                        patternElementType,
                        unprefixedName,
                        patternGraph.alternativesPlusInlined[i].name,
                        inlinedPatternClassName,
                        enumPrefix,
                        inlinedMatchObjectName ?? matchObjectName,
                        patternGraph.embeddedGraphsPlusInlined.Length
                    );
                insertionPoint = insertionPoint.Append(buildMatch);
            }
            if(patternGraph.nestedIndependents != null)
            {
                foreach(KeyValuePair<PatternGraph, bool> nestedIndependent in patternGraph.nestedIndependents)
                {
                    string inlinedMatchObjectName = null;
                    string unprefixedName = nestedIndependent.Key.name;
                    string inlinedPatternClassName = env.rulePatternClassName;
                    string matchClassName = nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name;
                    if(nestedIndependent.Key.originalPatternGraph != null)
                    {
                        if(patternGraph.WasInlinedHere(nestedIndependent.Key.originalSubpatternEmbedding))
                            inlinedMatchObjectName = "match_" + nestedIndependent.Key.originalSubpatternEmbedding.Name;
                        unprefixedName = nestedIndependent.Key.originalPatternGraph.Name;
                        inlinedPatternClassName = nestedIndependent.Key.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                        matchClassName = nestedIndependent.Key.pathPrefix + unprefixedName;
                    }
                    BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Independent,
                        unprefixedName,
                        nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name,
                        inlinedPatternClassName,
                        inlinedMatchObjectName ?? matchObjectName,
                        matchClassName
                    );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }

            // we only have to take care of yielding in case of normal and independent matches 
            // and if we contain a def entity (and if we are not in the defElements pass), or if we contain a ref entity (that may be yielded to instead)
            if(matchObjectType != MatchObjectType.Patternpath
                && (patternGraph.isDefEntityExistingPlusInlined || patternGraph.IsRefEntityExisting()
                    || patternGraph.isIteratedFilteringExistingPlusInlined || patternGraph.isEmitOrAssertExistingPlusInlined))
            {
                // first compute the yieldings which were inlined from subpatterns to us
                insertionPoint = insertYields(insertionPoint, patternGraph, matchObjectName, true);
                
                // then the yieldings of the current pattern
                return insertYields(insertionPoint, patternGraph, matchObjectName, false);

                // in the def-elements pass the yieldings computed into local variables are written to the match
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to yield, bubbling effects of nested yields upwards and computing local yields 
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertYields(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            String matchObjectName,
            bool inlined)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // the match object is built now with our local elements
            // and the match objects of our children
            // now 1. copy all def entities we share with children to our local variables

            foreach(PatternGraphEmbedding patternEmbedding in patternGraph.embeddedGraphsPlusInlined)
            {
                // in the inlined pass only the elements which were inlined into this pattern, in the non-inlined pass the original elements
                bool wasInlined = patternEmbedding.originalEmbedding != null;
                if(inlined == wasInlined)
                {
                    // if the pattern embedding does not contain a def entity it can't yield to us
                    if(!((PatternGraph)patternEmbedding.EmbeddedGraph).isDefEntityExistingPlusInlined)
                        continue;
                    // skip inlined embeddings (will be handled in 3.)
                    if(patternEmbedding.inlined)
                        continue;

                    // in inlined pass bubble up from the components of the inlined patterns to the elements resulting from of the inlined pattern 
                    // (read from submatches to local variables, compute in local variables, writing will happen later from local variables to matches)
                    string inlinedMatchObjectName = null;
                    string patternEmbeddingName = patternEmbedding.Name;
                    if(patternEmbedding.originalEmbedding != null)
                    {
                        if(patternGraph.WasInlinedHere(patternEmbedding.originalSubpatternEmbedding))
                            inlinedMatchObjectName = "match_" + patternEmbedding.originalSubpatternEmbedding.Name;
                        patternEmbeddingName = patternEmbedding.originalEmbedding.name;
                    }

                    LGSPMatchingPattern matchingPattern = patternEmbedding.matchingPatternOfEmbeddedGraph;
                    String nestedMatchObjectName = (inlinedMatchObjectName ?? matchObjectName) + "._" + patternEmbeddingName;
                    Debug.Assert(matchingPattern.defNames.Length == patternEmbedding.yields.Length);
                    for(int i = 0; i < patternEmbedding.yields.Length; ++i)
                    {
                        // nesting pattern def elements used in the subpattern (explicitly via paramter passing, no "direct access") are bubbled up from the match object
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                            toBubbleUpYieldAssignmentType(matchingPattern.defs[i]),
                            patternEmbedding.yields[i],
                            nestedMatchObjectName,
                            matchingPattern.defNames[i]
                            );
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }

            foreach(Iterated iterated in patternGraph.iteratedsPlusInlined)
            {
                // in the inlined pass only the elements which were inlined into this pattern, in the non-inlined pass the original elements
                bool wasInlined = iterated.originalIterated != null;
                if(inlined == wasInlined)
                {
                    // if the iterated does not contain a non local def entity it can't yield to us
                    if(!iterated.iteratedPattern.isNonLocalDefEntityExistingPlusInlined)
                        continue;

                    // in inlined pass bubble up from the components of the inlined patterns to the elements resulting from of the inlined pattern 
                    // (read from submatches to local variables, compute in local variables, writing will happen later from local variables to matches)
                    string inlinedMatchObjectName = null;
                    string iteratedName = iterated.iteratedPattern.Name;
                    if(iterated.originalIterated != null)
                    {
                        if(patternGraph.WasInlinedHere(iterated.originalSubpatternEmbedding))
                            inlinedMatchObjectName = "match_" + iterated.originalSubpatternEmbedding.Name;
                        iteratedName = iterated.originalIterated.iteratedPattern.name;
                    }

                    String nestedMatchObjectName = (inlinedMatchObjectName ?? matchObjectName) + "._" + iteratedName;
                    BubbleUpYieldIterated bubbleUpIterated = new BubbleUpYieldIterated(nestedMatchObjectName);
                    bubbleUpIterated.NestedOperationsList = new SearchProgramList(bubbleUpIterated);
                    SearchProgramOperation continuationPoint = insertionPoint.Append(bubbleUpIterated);
                    insertionPoint = bubbleUpIterated.NestedOperationsList;

                    // nesting pattern def elements used in the iterated are bubbled up from the tasks object
                    string taskVar = NamesOfEntities.TaskVariable(iterated.iteratedPattern.name, negativeIndependentNamePrefix);
                    insertYieldAssignmentsFromTask(insertionPoint,
                        patternGraph, taskVar, iterated.iteratedPattern);

                    insertionPoint = continuationPoint;
                }
            }
            foreach(Alternative alternative in patternGraph.alternativesPlusInlined)
            {
                // in the inlined pass only the elements which were inlined into this pattern, in the non-inlined pass the original elements
                bool wasInlined = alternative.originalAlternative != null;
                if(inlined == wasInlined)
                {
                    bool first = true;
                    foreach(PatternGraph alternativeCase in alternative.alternativeCases)
                    {
                        // if the alternative case does not contain a non local def entity it can't yield to us
                        if(!alternativeCase.isNonLocalDefEntityExistingPlusInlined)
                            continue;

                        // in inlined pass bubble up from the components of the inlined patterns to the elements resulting from of the inlined pattern 
                        // (read from submatches to local variables, compute in local variables, writing will happen later from local variables to matches)
                        string inlinedMatchObjectName = matchObjectName;
                        string inlinedNestedMatchObjectName = "_" + alternative.name;
                        string inlinedPatternClassName = env.rulePatternClassName;
                        string patternElementType = patternGraph.pathPrefix + patternGraph.name + "_" + alternative.name + "_" + alternativeCase.name;
                        if(alternative.originalAlternative != null)
                        {
                            if(patternGraph.WasInlinedHere(alternative.originalSubpatternEmbedding))
                                inlinedMatchObjectName = "match_" + alternative.originalSubpatternEmbedding.name;
                            inlinedNestedMatchObjectName = "_" + alternative.originalAlternative.name;
                            inlinedPatternClassName = alternative.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                            patternElementType = alternative.originalAlternative.pathPrefix + alternative.originalAlternative.name + "_" + alternativeCase.originalPatternGraph.name;
                        }
                        
                        BubbleUpYieldAlternativeCase bubbleUpAlternativeCase =
                            new BubbleUpYieldAlternativeCase(
                                inlinedMatchObjectName,
                                inlinedNestedMatchObjectName,
                                inlinedPatternClassName + ".Match_" + patternElementType,
                                "altCaseMatch",
                                first);
                        bubbleUpAlternativeCase.NestedOperationsList = new SearchProgramList(bubbleUpAlternativeCase);
                        SearchProgramOperation continuationPoint = insertionPoint.Append(bubbleUpAlternativeCase);
                        insertionPoint = bubbleUpAlternativeCase.NestedOperationsList;
                        first = false;

                        // nesting pattern def elements used in the alternative are bubbled up from the tasks object
                        string taskVar = NamesOfEntities.TaskVariable(alternative.name, negativeIndependentNamePrefix);
                        insertYieldAssignmentsFromTask(insertionPoint,
                            patternGraph, taskVar, alternativeCase);

                        insertionPoint = continuationPoint;
                    }
                }
            }

            foreach(PatternGraph independent in patternGraph.independentPatternGraphsPlusInlined)
            {
                // in the inlined pass only the elements which were inlined into this pattern, in the non-inlined pass the original elements
                bool wasInlined = independent.originalPatternGraph != null;
                if(inlined == wasInlined)
                {
                    // if the independent does not contain a non local def entity it can't yield to us
                    if(!independent.isNonLocalDefEntityExistingPlusInlined)
                        continue;

                    // in inlined pass bubble up from the components of the inlined patterns to the elements resulting from of the inlined pattern 
                    // (read from submatches to local variables, compute in local variables, writing will happen later from local variables to matches)
                    string inlinedMatchObjectName = null;
                    if(independent.originalPatternGraph != null && patternGraph.WasInlinedHere(independent.originalSubpatternEmbedding))
                        inlinedMatchObjectName = "match_" + independent.originalSubpatternEmbedding.Name;

                    String nestedMatchObjectName = NamesOfEntities.MatchedIndependentVariable(independent.pathPrefix + independent.name); ;

                    // nesting pattern def variables used in the independent are bubbled up from the match object
                    insertionPoint = insertYieldAssignments(insertionPoint,
                        patternGraph, inlinedMatchObjectName ?? nestedMatchObjectName, independent);
                }
            }

            //////////////////////////////////////////////////////
            // then 2. compute all local yields
            PatternYielding[] patternYieldings;
            if(patternGraph.parallelizedSchedule != null) // in case of parallelization we've to use the parallelized yieldings
                patternYieldings = patternGraph.parallelizedYieldings;
            else
                patternYieldings = patternGraph.YieldingsPlusInlined;
            foreach(PatternYielding patternYielding in patternYieldings)
            {
                // in the inlined pass only the elements which were inlined into this pattern, in the non-inlined pass the original elements
                bool wasInlined = patternYielding.originalYielding != null;
                if(inlined == wasInlined)
                {
                    YieldingBlock yieldingBlock = new YieldingBlock(patternYielding.Name);
                    yieldingBlock.NestedOperationsList = new SearchProgramList(yieldingBlock);
                    SearchProgramOperation continuationPointOuter = insertionPoint.Append(yieldingBlock);
                    insertionPoint = yieldingBlock.NestedOperationsList;
                    
                    foreach(Yielding yielding in patternYielding.ElementaryYieldings)
                    {
                        // iterated potentially matching more than once can't be bubbled up normally,
                        // they need accumulation with a for loop into a variable of the nesting pattern, 
                        // that's done in/with the yield statements of the parent
                        if(yielding is IteratedAccumulationYield)
                        {
                            IteratedAccumulationYield accumulationYield = (IteratedAccumulationYield)yielding;
                            foreach(Iterated iterated in patternGraph.iteratedsPlusInlined)
                            {
                                // skip the iterateds we're not interested in
                                if(accumulationYield.Iterated != iterated.iteratedPattern.Name)
                                    continue;

                                // in inlined pass bubble up from the components of the inlined patterns to the elements resulting from of the inlined pattern 
                                // (read from submatches to local variables, compute in local variables, writing will happen later from local variables to matches)
                                string inlinedMatchObjectName = null;
                                if(iterated.originalIterated != null)
                                {
                                    inlinedMatchObjectName = "match_" + iterated.originalSubpatternEmbedding.Name;
                                }

                                String nestedMatchObjectName = (inlinedMatchObjectName ?? matchObjectName) + "._" + iterated.iteratedPattern.Name;
                                AccumulateUpYieldIterated accumulateUpIterated =
                                    new AccumulateUpYieldIterated(
                                        nestedMatchObjectName,
                                        env.rulePatternClassName + ".Match_" + patternGraph.pathPrefix + patternGraph.name + "_" + iterated.iteratedPattern.name,
                                        "iteratedMatch");
                                accumulateUpIterated.NestedOperationsList = new SearchProgramList(accumulateUpIterated);
                                SearchProgramOperation continuationPoint = insertionPoint.Append(accumulateUpIterated);
                                insertionPoint = accumulateUpIterated.NestedOperationsList;

                                accumulationYield.IteratedMatchVariable = "iteratedMatch._" + NamesOfEntities.Variable(accumulationYield.UnprefixedVariable);
                                accumulationYield.ReplaceVariableByIterationVariable(accumulationYield);

                                SourceBuilder yieldAssignmentSource = new SourceBuilder();
                                accumulationYield.EmitLambdaExpressionImplementationMethods(env.arrayPerElementMethodBuilder);
                                accumulationYield.Emit(yieldAssignmentSource);
                                LocalYielding yieldAssignment =
                                    new LocalYielding(yieldAssignmentSource.ToString());
                                insertionPoint = insertionPoint.Append(yieldAssignment);

                                insertionPoint = continuationPoint;
                            }
                        }
                        else
                        {
                            SourceBuilder yieldAssignmentSource = new SourceBuilder();
                            yielding.EmitLambdaExpressionImplementationMethods(env.arrayPerElementMethodBuilder);
                            yielding.Emit(yieldAssignmentSource);
                            LocalYielding yieldAssignment =
                                new LocalYielding(yieldAssignmentSource.ToString());
                            insertionPoint = insertionPoint.Append(yieldAssignment);
                        }
                    }

                    insertionPoint = continuationPointOuter;
                }
            }

            //////////////////////////////////////////////////////
            // 3. at the end of the inlined pass:
            // assign the def parameters from a subpattern which was inlined to the arguments yielded to
            // we can't read from the match objects of the inlined subpatterns cause they are not written yet, but from the corresponding local variables
            if(inlined)
            {
                foreach(PatternGraphEmbedding patternEmbedding in patternGraph.embeddedGraphsPlusInlined)
                {
                    // only inlined embeddings
                    if(!patternEmbedding.inlined)
                        continue;

                    for(int i = 0; i < patternEmbedding.yields.Length; ++i)
                    {
                        String targetName = patternEmbedding.yields[i];
                        String sourceName = patternEmbedding.matchingPatternOfEmbeddedGraph.defNames[i];
                        IPatternElement elem = getInlinedElementByOriginalUnprefixedName(patternGraph, patternEmbedding, sourceName);
                        
                        // find the one which was originating from inlining patternEmbedding, because of same originator
                        bool isVariable = patternEmbedding.matchingPatternOfEmbeddedGraph.defs[i] is VarType;
                        YieldAssignment assignment = new YieldAssignment(
                            targetName,
                            isVariable,
                            patternEmbedding.matchingPatternOfEmbeddedGraph.defs[i] is VarType ?
                                TypesHelper.TypeName(patternEmbedding.matchingPatternOfEmbeddedGraph.defs[i]) :
                                ((GraphElementType)patternEmbedding.matchingPatternOfEmbeddedGraph.defs[i]).IsNodeType ? "GRGEN_LGSP.LGSPNode" : "GRGEN_LGSP.LGSPEdge",
                            isVariable ? (Expression)new VariableExpression(elem.Name) : (Expression)new GraphEntityExpression(elem.Name)
                        );

                        SourceBuilder yieldAssignmentSource = new SourceBuilder();
                        assignment.EmitLambdaExpressionImplementationMethods(env.arrayPerElementMethodBuilder);
                        assignment.Emit(yieldAssignmentSource);
                        LocalYielding yieldAssignment =
                            new LocalYielding(yieldAssignmentSource.ToString());
                        insertionPoint = insertionPoint.Append(yieldAssignment);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code for bubbling yield assignments from matches object
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertYieldAssignments(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            String nestedMatchObjectName,
            PatternGraph nestedPatternGraph)
        {
            foreach(PatternNode node in patternGraph.nodesPlusInlined)
            {
                if(!node.DefToBeYieldedTo)
                    continue;

                foreach(PatternNode nestedNode in nestedPatternGraph.nodesPlusInlined)
                {
                    if(nestedNode == node)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                            EntityType.Node,
                            node.Name,
                            nestedMatchObjectName,
                            nestedNode.originalNode!=null && patternGraph.WasInlinedHere(nestedNode.originalSubpatternEmbedding) ? nestedNode.originalNode.UnprefixedName : nestedNode.UnprefixedName
                            );
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }

            foreach(PatternEdge edge in patternGraph.edgesPlusInlined)
            {
                if(!edge.DefToBeYieldedTo)
                    continue;

                foreach(PatternEdge nestedEdge in nestedPatternGraph.edgesPlusInlined)
                {
                    if(nestedEdge == edge)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                            EntityType.Edge,
                            edge.Name,
                            nestedMatchObjectName,
                            nestedEdge.originalElement!=null ? nestedEdge.originalEdge.UnprefixedName : nestedEdge.UnprefixedName
                            );
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }
            
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(!var.DefToBeYieldedTo)
                    continue;

                foreach(PatternVariable nestedVar in nestedPatternGraph.variablesPlusInlined)
                {
                    if(nestedVar == var)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                            EntityType.Variable,
                            var.Name,
                            nestedMatchObjectName,
                            nestedVar.originalVariable!=null ? nestedVar.originalVariable.UnprefixedName : nestedVar.UnprefixedName
                            );
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code for bubbling yield assignments from tasks object
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertYieldAssignmentsFromTask(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            String taskObjectName,
            PatternGraph nestedPatternGraph)
        {
            foreach(PatternNode node in patternGraph.nodesPlusInlined)
            {
                if(!node.DefToBeYieldedTo)
                    continue;

                foreach(PatternNode nestedNode in nestedPatternGraph.nodesPlusInlined)
                {
                    if(nestedNode == node)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                                EntityType.Node,
                                node.Name,
                                taskObjectName);
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }

            foreach(PatternEdge edge in patternGraph.edgesPlusInlined)
            {
                if(!edge.DefToBeYieldedTo)
                    continue;

                foreach(PatternEdge nestedEdge in nestedPatternGraph.edgesPlusInlined)
                {
                    if(nestedEdge == edge)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                                EntityType.Edge,
                                edge.Name,
                                taskObjectName);
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }

            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(!var.DefToBeYieldedTo)
                    continue;

                foreach(PatternVariable nestedVar in nestedPatternGraph.variablesPlusInlined)
                {
                    if(nestedVar == var)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                                EntityType.Variable,
                                var.Name,
                                taskObjectName);
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to push the subpattern tasks to the open tasks stack 
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertPushSubpatternTasks(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            string searchPatternpath = "false";
            string matchOfNestingPattern = "null";
            string lastMatchAtPreviousNestingLevel = "null";

            if(patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count > 0)
            {
                if(patternGraph.isPatternpathLocked) searchPatternpath = "true";
                if((env.programType == SearchProgramType.Subpattern || env.programType == SearchProgramType.AlternativeCase || env.programType == SearchProgramType.Iterated)
                    && env.patternGraphWithNestingPatterns.Count == 1)
                {
                    searchPatternpath = "searchPatternpath";
                }
                matchOfNestingPattern = NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
                lastMatchAtPreviousNestingLevel = getCurrentLastMatchAtPreviousNestingLevel();
            }

            // first alternatives, so that they get processed last
            // to handle subpatterns in linear order we've to push them in reverse order on the stack
            for(int i = patternGraph.alternativesPlusInlined.Length - 1; i >= 0; --i)
            {
                Alternative alternative = patternGraph.alternativesPlusInlined[i];

                Dictionary<string, PatternNode> neededNodes = new Dictionary<string, PatternNode>();
                Dictionary<string, PatternEdge> neededEdges = new Dictionary<string, PatternEdge>();
                Dictionary<string, PatternVariable> neededVariables = new Dictionary<string, PatternVariable>();
                foreach(PatternGraph altCase in alternative.alternativeCases)
                {
                    foreach(KeyValuePair<string, PatternNode> neededNode in altCase.neededNodes)
                    {
                        neededNodes[neededNode.Key] = neededNode.Value;
                    }
                    foreach(KeyValuePair<string, PatternEdge> neededEdge in altCase.neededEdges)
                    {
                        neededEdges[neededEdge.Key] = neededEdge.Value;
                    }
                    foreach(KeyValuePair<string, PatternVariable> neededVariable in altCase.neededVariables)
                    {
                        neededVariables[neededVariable.Key] = neededVariable.Value;
                    }
                }

                int numElements = neededNodes.Count + neededEdges.Count + neededVariables.Count;
                string[] connectionName = new string[numElements];
                string[] argumentExpressions = new string[numElements];
                int j = 0;
                foreach(KeyValuePair<string, PatternNode> neededNode in neededNodes)
                {
                    connectionName[j] = neededNode.Key;
                    if(neededNode.Value.DefToBeYieldedTo)
                        argumentExpressions[j] = neededNode.Key; // already stored under full name, entity contained in tasks
                    else
                    {
                        SourceBuilder argumentExpression = new SourceBuilder();
                        (new GraphEntityExpression(neededNode.Key)).Emit(argumentExpression);
                        argumentExpressions[j] = argumentExpression.ToString();
                    }
                    ++j;
                }
                foreach(KeyValuePair<string, PatternEdge> neededEdge in neededEdges)
                {
                    connectionName[j] = neededEdge.Key;
                    if(neededEdge.Value.DefToBeYieldedTo)
                        argumentExpressions[j] = neededEdge.Key; // already stored under full name, entity contained in tasks
                    else
                    {
                        SourceBuilder argumentExpression = new SourceBuilder();
                        (new GraphEntityExpression(neededEdge.Key)).Emit(argumentExpression);
                        argumentExpressions[j] = argumentExpression.ToString();
                    }
                    ++j;
                }
                foreach(KeyValuePair<string, PatternVariable> neededVariable in neededVariables)
                {
                    connectionName[j] = neededVariable.Key;
                    if(neededVariable.Value.DefToBeYieldedTo)
                        argumentExpressions[j] = neededVariable.Key; // already stored under full name, entity contained in tasks
                    else
                    {
                        SourceBuilder argumentExpression = new SourceBuilder();
                        (new VariableExpression(neededVariable.Key)).Emit(argumentExpression);
                        argumentExpressions[j] = argumentExpression.ToString();
                    }
                    ++j;
                }

                string inlinedPatternClassName = env.rulePatternClassName;
                string pathPrefixInInlinedPatternClass = alternative.pathPrefix;
                string unprefixedNameInInlinedPatternClass = alternative.name;
                if(alternative.originalAlternative != null)
                {
                    inlinedPatternClassName = alternative.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                    pathPrefixInInlinedPatternClass = alternative.originalAlternative.pathPrefix;
                    unprefixedNameInInlinedPatternClass = alternative.originalAlternative.name;
                }

                PushSubpatternTask pushTask =
                    new PushSubpatternTask(
                        PushAndPopSubpatternTaskTypes.Alternative,
                        alternative.pathPrefix,
                        alternative.name,
                        inlinedPatternClassName,
                        pathPrefixInInlinedPatternClass,
                        unprefixedNameInInlinedPatternClass,
                        connectionName,
                        argumentExpressions,
                        negativeIndependentNamePrefix,
                        searchPatternpath,
                        matchOfNestingPattern,
                        lastMatchAtPreviousNestingLevel,
                        env.parallelized
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            // then iterated patterns of the pattern
            // to handle iterated in linear order we've to push them in reverse order on the stack
            for(int i = patternGraph.iteratedsPlusInlined.Length - 1; i >= 0; --i)
            {
                PatternGraph iter = patternGraph.iteratedsPlusInlined[i].iteratedPattern;

                int numElements = iter.neededNodes.Count + iter.neededEdges.Count + iter.neededVariables.Count;
                string[] connectionName = new string[numElements];
                string[] argumentExpressions = new string[numElements]; 
                int j = 0;
                foreach(KeyValuePair<string, PatternNode> node in iter.neededNodes)
                {
                    connectionName[j] = node.Key;
                    if(node.Value.DefToBeYieldedTo)
                        argumentExpressions[j] = node.Key; // already stored under full name, entity contained in task
                    else
                    {
                        SourceBuilder argumentExpression = new SourceBuilder();
                        (new GraphEntityExpression(node.Key)).Emit(argumentExpression);
                        argumentExpressions[j] = argumentExpression.ToString();
                    }
                    ++j;
                }
                foreach(KeyValuePair<string, PatternEdge> edge in iter.neededEdges)
                {
                    connectionName[j] = edge.Key;
                    if(edge.Value.DefToBeYieldedTo)
                        argumentExpressions[j] = edge.Key; // already stored under full name, entity contained in task
                    else
                    {
                        SourceBuilder argumentExpression = new SourceBuilder();
                        (new GraphEntityExpression(edge.Key)).Emit(argumentExpression);
                        argumentExpressions[j] = argumentExpression.ToString();
                    }
                    ++j;
                }
                foreach(KeyValuePair<string, PatternVariable> variable in iter.neededVariables)
                {
                    connectionName[j] = variable.Key;
                    if(variable.Value.DefToBeYieldedTo)
                        argumentExpressions[j] = variable.Key; // already stored under full name, entity contained in task
                    else
                    {
                        SourceBuilder argumentExpression = new SourceBuilder();
                        (new VariableExpression(variable.Key)).Emit(argumentExpression);
                        argumentExpressions[j] = argumentExpression.ToString();
                    }
                    ++j;
                }

                PushSubpatternTask pushTask =
                    new PushSubpatternTask(
                        PushAndPopSubpatternTaskTypes.Iterated,
                        iter.pathPrefix,
                        iter.name,
                        env.rulePatternClassName,
                        "",
                        "",
                        connectionName,
                        argumentExpressions,
                        negativeIndependentNamePrefix,
                        searchPatternpath,
                        matchOfNestingPattern,
                        lastMatchAtPreviousNestingLevel,
                        env.parallelized
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            // and finally subpatterns of the pattern
            // to handle subpatterns in linear order we've to push them in reverse order on the stack
            for(int i = patternGraph.embeddedGraphsPlusInlined.Length - 1; i >= 0; --i)
            {
                PatternGraphEmbedding subpattern = patternGraph.embeddedGraphsPlusInlined[i];
                if(subpattern.inlined)
                    continue;
                Debug.Assert(subpattern.matchingPatternOfEmbeddedGraph.inputNames.Length == subpattern.connections.Length);
                string[] connectionName = subpattern.matchingPatternOfEmbeddedGraph.inputNames;
                string[] argumentExpressions = new string[subpattern.connections.Length];
                for(int j = 0; j < subpattern.connections.Length; ++j)
                {
                    // no def entites needed as for alternative/iterated as explicit parameter passing is used, instead of implicit access of entity from nesting pattern
                    SourceBuilder argumentExpression = new SourceBuilder();
                    subpattern.connections[j].EmitLambdaExpressionImplementationMethods(env.arrayPerElementMethodBuilder);
                    subpattern.connections[j].Emit(argumentExpression);
                    argumentExpressions[j] = argumentExpression.ToString();
                }

                PushSubpatternTask pushTask =
                    new PushSubpatternTask(
                        PushAndPopSubpatternTaskTypes.Subpattern,
                        subpattern.matchingPatternOfEmbeddedGraph.name,
                        subpattern.name,
                        connectionName,
                        argumentExpressions,
                        negativeIndependentNamePrefix,
                        searchPatternpath,
                        matchOfNestingPattern,
                        lastMatchAtPreviousNestingLevel,
                        env.parallelized
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to pop the subpattern tasks from the open tasks stack
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertPopSubpatternTasks(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            foreach(PatternGraphEmbedding subpattern in patternGraph.embeddedGraphsPlusInlined)
            {
                if(subpattern.inlined)
                    continue;
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Subpattern,
                        subpattern.matchingPatternOfEmbeddedGraph.name,
                        subpattern.name,
                        env.parallelized
                    );
                insertionPoint = insertionPoint.Append(popTask);
            }

            foreach(Iterated iterated in patternGraph.iteratedsPlusInlined)
            {
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Iterated,
                        iterated.iteratedPattern.name,
                        iterated.iteratedPattern.pathPrefix,
                        env.parallelized
                    );
                insertionPoint = insertionPoint.Append(popTask);
            }

            foreach(Alternative alternative in patternGraph.alternativesPlusInlined)
            {
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Alternative,
                        alternative.name,
                        alternative.pathPrefix,
                        env.parallelized
                    );
                insertionPoint = insertionPoint.Append(popTask);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to check whether there are open tasks to handle left and code for case there are none
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertCheckForTasksLeft(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();

            CheckContinueMatchingTasksLeft tasksLeft =
                new CheckContinueMatchingTasksLeft();
            SearchProgramOperation continuationPointAfterTasksLeft =
                insertionPoint.Append(tasksLeft);
            tasksLeft.CheckFailedOperations =
                new SearchProgramList(tasksLeft);
            insertionPoint = tasksLeft.CheckFailedOperations;

            // ---- check failed, no tasks left, leaf subpattern was matched
            string inlinedPatternClassName = env.rulePatternClassName;
            string pathPrefixInInlinedPatternClass = patternGraph.pathPrefix;
            string unprefixedNameInInlinedPatternClass = patternGraph.name;
            if(patternGraph.originalPatternGraph != null)
            {
                inlinedPatternClassName = patternGraph.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                pathPrefixInInlinedPatternClass = patternGraph.originalPatternGraph.pathPrefix;
                unprefixedNameInInlinedPatternClass = patternGraph.originalPatternGraph.name;
            }
            LeafSubpatternMatched leafMatched = new LeafSubpatternMatched(
                inlinedPatternClassName, pathPrefixInInlinedPatternClass+unprefixedNameInInlinedPatternClass, false);
            SearchProgramOperation continuationPointAfterLeafMatched =
                insertionPoint.Append(leafMatched);
            leafMatched.MatchBuildingOperations =
                new SearchProgramList(leafMatched);
            insertionPoint = leafMatched.MatchBuildingOperations;

            // ---- ---- fill the match object with the candidates 
            // ---- ---- which have passed all the checks for being a match
            insertionPoint = insertInlinedMatchObjectCreation(insertionPoint, 
                patternGraph, MatchObjectType.Normal);
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, false);
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, true);

            // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
            if(wasIndependentInlined(patternGraph, env.indexOfSchedule))
                insertionPoint = insertFillForDuplicateMatchChecking(insertionPoint);

            // ---- nesting level up
            insertionPoint = continuationPointAfterLeafMatched;

            // ---- check max matches reached will be inserted here by completion pass

            // nesting level up
            insertionPoint = continuationPointAfterTasksLeft;

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to insert the local match into a set used for duplicate checking
        /// at the given position, returns position after inserted operations.
        /// Duplicates may arise after independent inlining, when the non-inlined part was already matched with exactly the same elements.
        /// </summary>
        private SearchProgramOperation insertFillForDuplicateMatchChecking(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();

            List<String> namesOfPatternElements = new List<String>();
            List<String> unprefixedNamesOfPatternElements = new List<String>();
            List<bool> patternElementIsNode = new List<bool>();
            foreach(PatternElement elementFromInlinedIndependent in patternElementsMatchedThere(patternGraph))
            {
                namesOfPatternElements.Add(elementFromInlinedIndependent.Name);
                unprefixedNamesOfPatternElements.Add(elementFromInlinedIndependent.UnprefixedName);
                patternElementIsNode.Add(elementFromInlinedIndependent is PatternNode);
            }

            FillPartialMatchForDuplicateChecking checkDuplicateMatch =
                new FillPartialMatchForDuplicateChecking(env.rulePatternClassName,
                    patternGraph.pathPrefix + patternGraph.Name,
                    namesOfPatternElements.ToArray(),
                    unprefixedNamesOfPatternElements.ToArray(),
                    patternElementIsNode.ToArray(),
                    env.parallelized && env.programType == SearchProgramType.Action);
            insertionPoint = insertionPoint.Append(checkDuplicateMatch);

            return insertionPoint;
        }

        public void GetMatchElementsForDuplicateCheck(PatternGraph patternGraph, 
            out List<String> namesOfPatternElements, 
            out List<String> matchObjectPaths, 
            out List<String> unprefixedNamesOfPatternElements, 
            out List<bool> patternElementIsNode)
        {
            namesOfPatternElements = new List<String>();
            matchObjectPaths = new List<string>();
            unprefixedNamesOfPatternElements = new List<String>();
            patternElementIsNode = new List<bool>();
            foreach(PatternElement elementFromInlinedIndependent in patternElementsMatchedThere(patternGraph))
            {
                namesOfPatternElements.Add(elementFromInlinedIndependent.Name);

                string inlinedMatchObjectPath = "";
                string unprefixedName = elementFromInlinedIndependent.UnprefixedName;
                if(elementFromInlinedIndependent.originalElement != null)
                {
                    if(patternGraph.WasInlinedHere(elementFromInlinedIndependent.originalSubpatternEmbedding))
                        inlinedMatchObjectPath = "." + elementFromInlinedIndependent.originalSubpatternEmbedding.Name;
                    unprefixedName = elementFromInlinedIndependent.originalElement.UnprefixedName;
                }
                matchObjectPaths.Add(inlinedMatchObjectPath);
                unprefixedNamesOfPatternElements.Add(unprefixedName);

                patternElementIsNode.Add(elementFromInlinedIndependent is PatternNode);
            }
        }

        /// <summary>
        /// Inserts code to accept the matched elements globally
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertGlobalAccept(SearchProgramOperation insertionPoint)
        {
            bool isAction = env.programType == SearchProgramType.Action;
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if(patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.nodesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodesPlusInlined[i].defToBeYieldedTo
                        && !patternGraph.totallyHomomorphicNodes[i]
                        && patternGraph.nodesPlusInlined[i].AssignmentSource==null)
                    {
                        AcceptCandidateGlobal acceptGlobal =
                            new AcceptCandidateGlobal(patternGraph.nodesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            true,
                            env.isoSpaceNeverAboveMaxIsoSpace,
                            env.parallelized);
                        insertionPoint = insertionPoint.Append(acceptGlobal);
                    }
                }
            }
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if(patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.edgesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edgesPlusInlined[i].defToBeYieldedTo 
                        && !patternGraph.totallyHomomorphicEdges[i]
                        && patternGraph.edgesPlusInlined[i].AssignmentSource==null)
                    {
                        AcceptCandidateGlobal acceptGlobal =
                            new AcceptCandidateGlobal(patternGraph.edgesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            false,
                            env.isoSpaceNeverAboveMaxIsoSpace,
                            env.parallelized);
                        insertionPoint = insertionPoint.Append(acceptGlobal);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to accept the matched elements for patternpath checks
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertPatternpathAccept(SearchProgramOperation insertionPoint,
            PatternGraph patternGraph)
        {
            bool isAction = env.programType == SearchProgramType.Action;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if(patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.nodesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodesPlusInlined[i].defToBeYieldedTo)
                    {
                        AcceptCandidatePatternpath acceptPatternpath =
                            new AcceptCandidatePatternpath(patternGraph.nodesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            true);
                        insertionPoint = insertionPoint.Append(acceptPatternpath);
                    }
                }
            }
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if(patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.edgesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edgesPlusInlined[i].defToBeYieldedTo)
                    {
                        AcceptCandidatePatternpath acceptPatternpath =
                            new AcceptCandidatePatternpath(patternGraph.edgesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            false);
                        insertionPoint = insertionPoint.Append(acceptPatternpath);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to abandon the matched elements globally
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertGlobalAbandon(SearchProgramOperation insertionPoint)
        {
            bool isAction = env.programType == SearchProgramType.Action;
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // global abandon of all candidate elements (remove isomorphy information)
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if(patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.nodesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodesPlusInlined[i].defToBeYieldedTo
                        && !patternGraph.totallyHomomorphicNodes[i]
                        && patternGraph.nodesPlusInlined[i].AssignmentSource==null)
                    {
                        AbandonCandidateGlobal abandonGlobal =
                            new AbandonCandidateGlobal(patternGraph.nodesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            true,
                            env.isoSpaceNeverAboveMaxIsoSpace,
                            env.parallelized);
                        insertionPoint = insertionPoint.Append(abandonGlobal);
                    }
                }
            }
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if(patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.edgesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edgesPlusInlined[i].defToBeYieldedTo 
                        && !patternGraph.totallyHomomorphicEdges[i]
                        && patternGraph.edgesPlusInlined[i].AssignmentSource==null)
                    {
                        AbandonCandidateGlobal abandonGlobal =
                            new AbandonCandidateGlobal(patternGraph.edgesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            false,
                            env.isoSpaceNeverAboveMaxIsoSpace,
                            env.parallelized);
                        insertionPoint = insertionPoint.Append(abandonGlobal);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to abandon the matched elements for patternpath check
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertPatternpathAbandon(SearchProgramOperation insertionPoint,
            PatternGraph patternGraph)
        {
            bool isAction = env.programType == SearchProgramType.Action;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // patternpath abandon of all candidate elements (remove isomorphy information)
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if(patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.nodesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodesPlusInlined[i].defToBeYieldedTo)
                    {
                        AbandonCandidatePatternpath abandonPatternpath =
                            new AbandonCandidatePatternpath(patternGraph.nodesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            true);
                        insertionPoint = insertionPoint.Append(abandonPatternpath);
                    }
                }
            }
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if(patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
                    || patternGraph.edgesPlusInlined[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edgesPlusInlined[i].defToBeYieldedTo)
                    {
                        AbandonCandidatePatternpath abandonPatternpath =
                            new AbandonCandidatePatternpath(patternGraph.edgesPlusInlined[i].name,
                            negativeIndependentNamePrefix,
                            false);
                        insertionPoint = insertionPoint.Append(abandonPatternpath);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to check whether the subpatterns were found and code for case there were some
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertCheckForSubpatternsFound(SearchProgramOperation insertionPoint, 
            bool isIteratedNullMatch)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // check whether there were no subpattern matches found
            CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                new CheckPartialMatchForSubpatternsFound(negativeIndependentNamePrefix);
            SearchProgramOperation continuationPointAfterSubpatternsFound =
                   insertionPoint.Append(checkSubpatternsFound);
            checkSubpatternsFound.CheckFailedOperations =
                new SearchProgramList(checkSubpatternsFound);
            insertionPoint = checkSubpatternsFound.CheckFailedOperations;

            // ---- check failed, some subpattern matches found, pattern and subpatterns were matched
            PatternAndSubpatternsMatchedType type = PatternAndSubpatternsMatchedType.SubpatternOrAlternative;
            if(env.programType == SearchProgramType.Action)
                type = PatternAndSubpatternsMatchedType.Action;
            else if(env.programType == SearchProgramType.Iterated)
            {
                if(isIteratedNullMatch)
                    type = PatternAndSubpatternsMatchedType.IteratedNullMatch;
                else
                    type = PatternAndSubpatternsMatchedType.Iterated;
            }
            Debug.Assert(!isIteratedNullMatch || env.programType == SearchProgramType.Iterated);
            string inlinedPatternClassName = env.rulePatternClassName;
            string pathPrefixInInlinedPatternClass = patternGraph.pathPrefix;
            string unprefixedNameInInlinedPatternClass = patternGraph.name;
            if(patternGraph.originalPatternGraph != null)
            {
                inlinedPatternClassName = patternGraph.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                pathPrefixInInlinedPatternClass = patternGraph.originalPatternGraph.pathPrefix;
                unprefixedNameInInlinedPatternClass = patternGraph.originalPatternGraph.name;
            }
            PatternAndSubpatternsMatched patternAndSubpatternsMatched = new PatternAndSubpatternsMatched(
                inlinedPatternClassName, pathPrefixInInlinedPatternClass + unprefixedNameInInlinedPatternClass,
                env.parallelized && env.indexOfSchedule == 1, type);
            SearchProgramOperation continuationPointAfterPatternAndSubpatternsMatched =
                insertionPoint.Append(patternAndSubpatternsMatched);
            patternAndSubpatternsMatched.MatchBuildingOperations =
                new SearchProgramList(patternAndSubpatternsMatched);
            insertionPoint = patternAndSubpatternsMatched.MatchBuildingOperations;

            // ---- ---- fill the match object with the candidates 
            // ---- ---- which have passed all the checks for being a match
            if(!isIteratedNullMatch)
            {
                insertionPoint = insertInlinedMatchObjectCreation(insertionPoint, 
                    patternGraph, MatchObjectType.Normal);
                insertionPoint = insertMatchObjectBuilding(insertionPoint,
                    patternGraph, MatchObjectType.Normal, false);
                insertionPoint = insertMatchObjectBuilding(insertionPoint,
                    patternGraph, MatchObjectType.Normal, true);

                // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
                if(wasIndependentInlined(patternGraph, env.indexOfSchedule))
                    insertionPoint = insertFillForDuplicateMatchChecking(insertionPoint);
            }

            // ---- nesting level up
            insertionPoint = continuationPointAfterPatternAndSubpatternsMatched;

            // ---- create new matches list to search on or copy found matches into own matches list
            if(env.programType==SearchProgramType.Subpattern 
                || env.programType==SearchProgramType.AlternativeCase)
            { // not needed for iterated, because if match was found, that's it
                NewMatchesListForFollowingMatches newMatchesList =
                    new NewMatchesListForFollowingMatches(false);
                insertionPoint = insertionPoint.Append(newMatchesList);
            }

            // ---- check max matches reached will be inserted here by completion pass
            // (not for iterated, but not needed in that case, too)

            // nesting level up
            insertionPoint = continuationPointAfterSubpatternsFound;

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to check whether the subpatterns were found and code for case there were some
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertCheckForSubpatternsFoundNegativeIndependent(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // check whether there were no subpattern matches found
            CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                new CheckPartialMatchForSubpatternsFound(negativeIndependentNamePrefix);
            SearchProgramOperation continuationPointAfterSubpatternsFound =
                   insertionPoint.Append(checkSubpatternsFound);
            checkSubpatternsFound.CheckFailedOperations =
                new SearchProgramList(checkSubpatternsFound);
            insertionPoint = checkSubpatternsFound.CheckFailedOperations;

            if(env.isNegative)
            {
                // ---- check failed, some subpattern matches found, negative pattern and subpatterns were matched
                // build the negative pattern was matched operation
                NegativePatternMatched patternMatched = new NegativePatternMatched(
                    NegativeIndependentPatternMatchedType.ContainingSubpatterns, negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(patternMatched);

                // ---- abort the matching process
                CheckContinueMatchingOfNegativeFailed abortMatching =
                    new CheckContinueMatchingOfNegativeFailed(patternGraph.isIterationBreaking);
                insertionPoint = insertionPoint.Append(abortMatching);
            }
            else
            {
                // ---- check failed, some subpattern matches found, independent pattern and subpatterns were matched
                // build the independent pattern was matched operation
                IndependentPatternMatched patternMatched = new IndependentPatternMatched(
                    NegativeIndependentPatternMatchedType.ContainingSubpatterns,
                    negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(patternMatched);

                if(!env.isNestedInNegative) // no match object needed(/available) if independent is part of negative
                {
                    // ---- fill the match object with the candidates 
                    // ---- which have passed all the checks for being a match
                    insertionPoint = insertInlinedMatchObjectCreation(insertionPoint, 
                        patternGraph, MatchObjectType.Independent);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, false);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, true);

                    // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
                    if(wasIndependentInlined(patternGraph, env.indexOfSchedule))
                        insertionPoint = insertFillForDuplicateMatchChecking(insertionPoint);
                }

                // ---- continue the matching process outside
                CheckContinueMatchingOfIndependentSucceeded continueMatching =
                    new CheckContinueMatchingOfIndependentSucceeded();
                insertionPoint = insertionPoint.Append(continueMatching);
            }

            // nesting level up
            insertionPoint = continuationPointAfterSubpatternsFound;

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to handle case top level pattern of action was found
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertPatternFound(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();

            // build the pattern was matched operation
            PositivePatternWithoutSubpatternsMatched patternMatched =
                new PositivePatternWithoutSubpatternsMatched(env.rulePatternClassName, 
                    patternGraph.name,
                    env.parallelized && env.indexOfSchedule == 1);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(patternMatched);
            patternMatched.MatchBuildingOperations =
                new SearchProgramList(patternMatched);
            insertionPoint = patternMatched.MatchBuildingOperations;

            // ---- fill the match object with the candidates 
            // ---- which have passed all the checks for being a match
            insertionPoint = insertInlinedMatchObjectCreation(insertionPoint, 
                patternGraph, MatchObjectType.Normal);
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, false);
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, true);

            // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
            if(wasIndependentInlined(patternGraph, env.indexOfSchedule))
                insertionPoint = insertFillForDuplicateMatchChecking(insertionPoint);

            // ---- nesting level up
            insertionPoint = continuationPoint;

            // check whether to continue the matching process
            // or abort because the maximum desired number of maches was reached
            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
#if NO_ADJUST_LIST_HEADS
                new CheckContinueMatchingMaximumMatchesReached(
                    CheckMaximumMatchesType.Action, false, parallelized && indexOfSchedule == 1,
                    emitProfiling, actionName, firstLoopPassed);
#else
                new CheckContinueMatchingMaximumMatchesReached(
                    CheckMaximumMatchesType.Action, true, env.parallelized && env.indexOfSchedule == 1,
                    env.emitProfiling, env.packagePrefixedActionName, env.firstLoopPassed);
#endif
            insertionPoint = insertionPoint.Append(checkMaximumMatches);

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to handle case negative/independent pattern was found
        /// at the given position, returns position after inserted operations
        /// </summary>
        public SearchProgramOperation insertPatternFoundNegativeIndependent(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            if(env.isNegative)
            {
                // build the negative pattern was matched operation
                NegativePatternMatched patternMatched = new NegativePatternMatched(
                    NegativeIndependentPatternMatchedType.WithoutSubpatterns, negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(patternMatched);

                // abort the matching process
                CheckContinueMatchingOfNegativeFailed abortMatching =
                    new CheckContinueMatchingOfNegativeFailed(patternGraph.isIterationBreaking);
                insertionPoint = insertionPoint.Append(abortMatching);
            }
            else
            {
                // build the independent pattern was matched operation
                IndependentPatternMatched patternMatched = new IndependentPatternMatched(
                    NegativeIndependentPatternMatchedType.WithoutSubpatterns,
                    negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(patternMatched);

                if(!env.isNestedInNegative) // no match object needed(/available) if independent is part of negative
                {
                    // fill the match object with the candidates 
                    // which have passed all the checks for being a match
                    insertionPoint = insertInlinedMatchObjectCreation(insertionPoint,
                        patternGraph, MatchObjectType.Independent);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint, 
                        patternGraph, MatchObjectType.Independent, false);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, true);

                    // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
                    if(wasIndependentInlined(patternGraph, env.indexOfSchedule))
                        insertionPoint = insertFillForDuplicateMatchChecking(insertionPoint);
                }

                // continue the matching process outside
                CheckContinueMatchingOfIndependentSucceeded continueMatching =
                    new CheckContinueMatchingOfIndependentSucceeded();
                insertionPoint = insertionPoint.Append(continueMatching);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to check whether iteration came to an end (pattern not found (again))
        /// and code to handle that case 
        /// </summary>
        public SearchProgramOperation insertEndOfIterationHandling(SearchProgramOperation insertionPoint)
        {
            Debug.Assert(NegativeIndependentNamePrefix(env.patternGraphWithNestingPatterns.Peek()) == "");

            PatternGraph patternGraph = env.patternGraphWithNestingPatterns.Peek();

            // check whether the pattern was not found / the null match was found
            // if yes the iteration came to an end, handle that case
            CheckContinueMatchingIteratedPatternNonNullMatchFound iteratedPatternFound =
                new CheckContinueMatchingIteratedPatternNonNullMatchFound(patternGraph.isIterationBreaking);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(iteratedPatternFound);
            iteratedPatternFound.CheckFailedOperations = new SearchProgramList(iteratedPatternFound);
            insertionPoint = iteratedPatternFound.CheckFailedOperations;

            // ---- initialize task/result-pushdown handling in subpattern matcher for end of iteration
            InitializeSubpatternMatching initialize =
                new InitializeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.EndOfIteration);
            insertionPoint = insertionPoint.Append(initialize);

            // ---- ---- iteration pattern may be the last to be matched - handle that case
            CheckContinueMatchingTasksLeft tasksLeft =
                new CheckContinueMatchingTasksLeft();
            SearchProgramOperation continuationPointAfterTasksLeft =
                insertionPoint.Append(tasksLeft);
            tasksLeft.CheckFailedOperations = new SearchProgramList(tasksLeft);
            insertionPoint = tasksLeft.CheckFailedOperations;

            // ---- ---- check failed, no tasks left, leaf subpattern was matched
            string inlinedPatternClassName = env.rulePatternClassName;
            string pathPrefixInInlinedPatternClass = patternGraph.pathPrefix;
            string unprefixedNameInInlinedPatternClass = patternGraph.name;
            if(patternGraph.originalPatternGraph != null)
            {
                inlinedPatternClassName = patternGraph.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                pathPrefixInInlinedPatternClass = patternGraph.originalPatternGraph.pathPrefix;
                unprefixedNameInInlinedPatternClass = patternGraph.originalPatternGraph.name;
            }
            LeafSubpatternMatched leafMatched = new LeafSubpatternMatched(
                inlinedPatternClassName, pathPrefixInInlinedPatternClass + unprefixedNameInInlinedPatternClass, true);
            insertionPoint = insertionPoint.Append(leafMatched);
            leafMatched.MatchBuildingOperations = new SearchProgramList(leafMatched); // empty, no match object

            // ---- ---- finalize task/result-pushdown handling in subpattern matcher for end of iteration
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.EndOfIteration);
            insertionPoint = insertionPoint.Append(finalize);
            FinalizeSubpatternMatching finalizeIteration =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Iteration);
            insertionPoint = insertionPoint.Append(finalizeIteration);
            ContinueOperation leave =
                new ContinueOperation(ContinueOperationType.ByReturn, false, env.parallelized && env.indexOfSchedule == 1);
            insertionPoint = insertionPoint.Append(leave);

            // ---- nesting level up
            insertionPoint = continuationPointAfterTasksLeft;

            // ---- we execute the open subpattern matching tasks
            MatchSubpatterns matchSubpatterns =
                new MatchSubpatterns("", env.parallelized);
            insertionPoint = insertionPoint.Append(matchSubpatterns);

            // ---- check whether the open subpattern matching task succeeded, with null match building
            insertionPoint = insertCheckForSubpatternsFound(insertionPoint, true);

            // ---- finalize task/result-pushdown handling in subpattern matcher for end of iteration
            finalize =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.EndOfIteration);
            insertionPoint = insertionPoint.Append(finalize);
            finalizeIteration =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Iteration);
            insertionPoint = insertionPoint.Append(finalizeIteration);
            leave =
                new ContinueOperation(ContinueOperationType.ByReturn, false, env.parallelized && env.indexOfSchedule == 1);
            insertionPoint = insertionPoint.Append(leave);

            // ---- nesting level up
            insertionPoint = continuationPoint;
            
            return insertionPoint;
        }

        /// <summary>
        /// Decides which get type operation to use and inserts it
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within type iteration was built
        /// if type drawing was sufficient, insertion point == continuation point
        /// </summary>
        public SearchProgramOperation decideOnAndInsertGetType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target,
            out SearchProgramOperation continuationPoint)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? (ITypeModel)env.model.NodeModel : (ITypeModel)env.model.EdgeModel;

            if(target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed
                if(typeModel.Types[target.PatternElement.TypeID].HasSubTypes)
                { // more than the type itself -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.AllCompatible,
                            target.PatternElement.Name,
                            TypesHelper.PrefixedTypeFromType(typeModel.TypeTypes[target.PatternElement.TypeID]),
                            isNode);
                    continuationPoint = insertionPoint.Append(typeIteration);

                    typeIteration.NestedOperationsList =
                        new SearchProgramList(typeIteration);
                    insertionPoint = typeIteration.NestedOperationsList;
                }
                else
                { // only the type itself -> no iteration needed
                    GetTypeByDrawing typeDrawing =
                        new GetTypeByDrawing(
                            target.PatternElement.Name,
                            target.PatternElement.TypeID.ToString(),
                            isNode);
                    insertionPoint = insertionPoint.Append(typeDrawing);
                    continuationPoint = insertionPoint;
                }
            }
            else //(target.PatternElement.AllowedTypes != null)
            { // the allowed types are given explicitely
                if(target.PatternElement.AllowedTypes.Length != 1)
                { // more than one allowed type -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.ExplicitelyGiven,
                            target.PatternElement.Name,
                            env.rulePatternClassName,
                            isNode);
                    continuationPoint = insertionPoint.Append(typeIteration);

                    typeIteration.NestedOperationsList =
                        new SearchProgramList(typeIteration);
                    insertionPoint = typeIteration.NestedOperationsList;
                }
                else
                { // only one allowed type -> no iteration needed
                    GetTypeByDrawing typeDrawing =
                        new GetTypeByDrawing(
                            target.PatternElement.Name,
                            target.PatternElement.AllowedTypes[0].TypeID.ToString(),
                            isNode);
                    insertionPoint = insertionPoint.Append(typeDrawing);
                    continuationPoint = insertionPoint;
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check type operation to build and inserts it into search program
        /// </summary>
        public SearchProgramOperation decideOnAndInsertCheckType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? (ITypeModel)env.model.NodeModel : (ITypeModel)env.model.EdgeModel;

            if(target.PatternElement.IsAllowedType != null)
            { // the allowed types are given by an array for checking against them
                Debug.Assert(target.PatternElement.AllowedTypes != null);

                if(target.PatternElement.AllowedTypes.Length <= MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID)
                {
                    string[] typeIDs = new string[target.PatternElement.AllowedTypes.Length];
                    for(int i = 0; i < target.PatternElement.AllowedTypes.Length; ++i)
                    {
                        typeIDs[i] = target.PatternElement.AllowedTypes[i].TypeID.ToString();
                    }

                    CheckCandidateForType checkType =
                        new CheckCandidateForType(
                            CheckCandidateForTypeType.ByTypeID,
                            target.PatternElement.Name,
                            typeIDs,
                            isNode);
                    insertionPoint = insertionPoint.Append(checkType);
                }
                else
                {
                    string inlinedPatternClassName = null;
                    string inlinedPatternElementName = null;
                    if(target.PatternElement.originalElement != null)
                    {
                        inlinedPatternClassName = target.PatternElement.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                        inlinedPatternElementName = target.PatternElement.originalElement.name;
                    }

                    CheckCandidateForType checkType =
                        new CheckCandidateForType(
                            CheckCandidateForTypeType.ByIsAllowedType,
                            target.PatternElement.Name,
                            inlinedPatternClassName ?? env.rulePatternClassName,
                            inlinedPatternElementName ?? target.PatternElement.Name,
                            isNode);
                    insertionPoint = insertionPoint.Append(checkType);
                }

                return insertionPoint;
            }

            if(target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed
                GraphElementType targetType = ((IGraphElementTypeModel)typeModel).Types[target.PatternElement.TypeID];
                if(targetType == typeModel.RootType)
                { // every type matches the root type == element type -> no check needed
                    return insertionPoint;
                }

                if(targetType.subOrSameGrGenTypes.Length <= MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID)
                { // the target type has no sub types, it must be exactly this type
                    string[] typeIDs = new string[targetType.subOrSameGrGenTypes.Length];
                    for(int i = 0; i < targetType.subOrSameGrGenTypes.Length; ++i)
                    {
                        typeIDs[i] = targetType.subOrSameGrGenTypes[i].TypeID.ToString();
                    }

                    CheckCandidateForType checkType =
                        new CheckCandidateForType(
                            CheckCandidateForTypeType.ByTypeID,
                            target.PatternElement.Name,
                            typeIDs,
                            isNode);
                    insertionPoint = insertionPoint.Append(checkType);
                }
                else
                {
                    CheckCandidateForType checkType =
                        new CheckCandidateForType(
                            CheckCandidateForTypeType.ByIsMyType,
                            target.PatternElement.Name,
                            TypesHelper.PrefixedTypeFromType(typeModel.TypeTypes[target.PatternElement.TypeID]),
                            isNode);
                    insertionPoint = insertionPoint.Append(checkType);
                }

                return insertionPoint;
            }

            // the allowed types are given by allowed types array
            // if there are multiple ones, is allowed types must have been not null before
            Debug.Assert(target.PatternElement.AllowedTypes.Length > 1, "More than one allowed type");

            if(target.PatternElement.AllowedTypes.Length == 0)
            { // no type allowed
                CheckCandidateFailed checkFailed = new CheckCandidateFailed();
                insertionPoint = insertionPoint.Append(checkFailed);
            }
            else // (target.PatternElement.AllowedTypes.Length == 1)
            { // only one type allowed
                string[] typeIDs = new string[1];
                typeIDs[0] = target.PatternElement.AllowedTypes[0].TypeID.ToString();

                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByTypeID,
                        target.PatternElement.Name,
                        typeIDs,
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node just determined by lookup
        /// and inserts them into the search program
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within both directions iteration was built
        /// if no direction iteration was needed, insertion point == continuation point
        /// </summary>
        public SearchProgramOperation decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            ConnectednessCheck connectednessCheck,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;
            SearchProgramOperation localContinuationPoint;

            // check for edges required by the pattern to be incident to the given node
            foreach(SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if(((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                        insertionPoint, node, edge, CheckCandidateForConnectednessType.Source, connectednessCheck);
                }
                else
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                        insertionPoint, node, edge, connectednessCheck, out localContinuationPoint);
                    if(localContinuationPoint != insertionPoint && continuationPoint == null)
                        continuationPoint = localContinuationPoint;
                }
            }
            foreach(SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if(((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                        insertionPoint, node, edge, CheckCandidateForConnectednessType.Target, connectednessCheck);
                }
                else
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                        insertionPoint, node, edge, connectednessCheck, out localContinuationPoint);
                    if(localContinuationPoint != insertionPoint && continuationPoint == null)
                        continuationPoint = localContinuationPoint;
                }
            }

            if(continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node just drawn from edge
        /// and inserts them into the search program
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within both directions iteration was built
        /// if no direction iteration was needed, insertion point == continuation point
        /// </summary>
        public SearchProgramOperation decideOnAndInsertCheckConnectednessOfImplicitNodeFromEdge(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            SearchPlanEdgeNode originatingEdge,
            SearchPlanNodeNode otherNodeOfOriginatingEdge,
            ConnectednessCheck connectednessCheck,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;
            SearchProgramOperation localContinuationPoint;

            // check for edges required by the pattern to be incident to the given node
            // only if the node was not taken from the given originating edge
            //   with the exception of reflexive edges, as these won't get checked thereafter
            foreach(SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if(((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    if(edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                            insertionPoint, node, edge, CheckCandidateForConnectednessType.Source, connectednessCheck);
                    }
                }
                else
                {
                    if(edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                            insertionPoint, node, edge, connectednessCheck, out localContinuationPoint);
                        if(localContinuationPoint != insertionPoint && continuationPoint == null)
                            continuationPoint = localContinuationPoint;
                    }
                }
            }
            foreach(SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if(((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    if(edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                            insertionPoint, node, edge, CheckCandidateForConnectednessType.Target, connectednessCheck);
                    }
                }
                else
                {
                    if(edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                            insertionPoint, node, edge, connectednessCheck, out localContinuationPoint);
                        if(localContinuationPoint != insertionPoint && continuationPoint == null)
                            continuationPoint = localContinuationPoint;
                    }
                }
            }

            if(continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node and edge of fixed direction
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode currentNode,
            SearchPlanEdgeNode edge,
            CheckCandidateForConnectednessType connectednessType,
            ConnectednessCheck connectednessCheck)
        {
            Debug.Assert(connectednessType == CheckCandidateForConnectednessType.Source || connectednessType == CheckCandidateForConnectednessType.Target);

            // check whether the pattern edges which must be incident to the candidate node (according to the pattern)
            // are really incident to it
            // only if edge is already matched by now (signaled by visited)
            if(edge.Visited)
            {
                CheckCandidateForConnectedness checkConnectedness =
                    new CheckCandidateForConnectedness(
                        currentNode.PatternElement.Name,
                        currentNode.PatternElement.Name,
                        edge.PatternElement.Name,
                        connectednessType);
                insertionPoint = insertionPoint.Append(checkConnectedness);

                connectednessCheck.PatternElementName = currentNode.PatternElement.UnprefixedName;
                connectednessCheck.PatternNodeName = currentNode.PatternElement.UnprefixedName;
                connectednessCheck.PatternEdgeName = edge.PatternElement.UnprefixedName;
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node and edge in both directions
        /// and inserts them into the search program
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within both directions iteration was built
        /// if no direction iteration was needed, insertion point == continuation point
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfNodeBothDirections(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode currentNode,
            SearchPlanEdgeNode edge,
            ConnectednessCheck connectednessCheck,
            out SearchProgramOperation continuationPoint)
        {
            Debug.Assert(edge.PatternEdgeSource != edge.PatternEdgeTarget);

            continuationPoint = null;

            // check whether the pattern edges which must be incident to the candidate node (according to the pattern)
            // are really incident to it
            if(currentNodeIsFirstIncidentNodeOfEdge(currentNode, edge))
            {
                BothDirectionsIteration directionsIteration =
                    new BothDirectionsIteration(edge.PatternElement.Name);
                directionsIteration.NestedOperationsList = new SearchProgramList(directionsIteration);
                continuationPoint = insertionPoint.Append(directionsIteration);

                CheckCandidateForConnectedness checkConnectedness =
                    new CheckCandidateForConnectedness(
                        currentNode.PatternElement.Name,
                        currentNode.PatternElement.Name,
                        edge.PatternElement.Name,
                        CheckCandidateForConnectednessType.SourceOrTarget);
                insertionPoint = directionsIteration.NestedOperationsList.Append(checkConnectedness);

                connectednessCheck.PatternElementName = currentNode.PatternElement.UnprefixedName;
                connectednessCheck.PatternNodeName = currentNode.PatternElement.UnprefixedName;
                connectednessCheck.PatternEdgeName = edge.PatternElement.UnprefixedName;
            }
            if(currentNodeIsSecondIncidentNodeOfEdge(currentNode, edge))
            {
                CheckCandidateForConnectedness checkConnectedness =
                    new CheckCandidateForConnectedness(
                        currentNode.PatternElement.Name,
                        currentNode.PatternElement.Name,
                        edge.PatternElement.Name,
                        edge.PatternEdgeSource == currentNode ? edge.PatternEdgeTarget.PatternElement.Name
                            : edge.PatternEdgeSource.PatternElement.Name,
                        CheckCandidateForConnectednessType.TheOther);
                insertionPoint = insertionPoint.Append(checkConnectedness);

                connectednessCheck.PatternElementName = currentNode.PatternElement.UnprefixedName;
                connectednessCheck.PatternNodeName = currentNode.PatternElement.UnprefixedName;
                connectednessCheck.PatternEdgeName = edge.PatternElement.UnprefixedName;
            }

            if(continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge determined by lookup
        /// and inserts them into the search program
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within both directions iteration was built
        /// if no direction iteration was needed, insertion point == continuation point
        /// </summary>
        public SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            ConnectednessCheck connectednessCheck,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            if(((PatternEdge)edge.PatternElement).fixedDirection)
            {
                // don't need to check if the edge is not required by the pattern to be incident to some given node
                if(edge.PatternEdgeSource != null)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                        insertionPoint, edge, CheckCandidateForConnectednessType.Source, connectednessCheck);
                }
                if(edge.PatternEdgeTarget != null)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                        insertionPoint, edge, CheckCandidateForConnectednessType.Target, connectednessCheck);
                }
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeBothDirections(
                    insertionPoint, edge, false, connectednessCheck, out continuationPoint);
            }

            if(continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge determined from incident node
        /// and inserts them into the search program
        /// </summary>
        public SearchProgramOperation decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode originatingNode,
            bool edgeIncomingAtOriginatingNode,
            ConnectednessCheck connectednessCheck,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            if(((PatternEdge)edge.PatternElement).fixedDirection)
            {
                // don't need to check if the edge is not required by the pattern to be incident to some given node
                // or if the edge was taken from the given originating node
                if(edge.PatternEdgeSource != null)
                {
                    if(!(!edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeSource == originatingNode))
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                            insertionPoint, edge, CheckCandidateForConnectednessType.Source, connectednessCheck);
                    }
                }
                if(edge.PatternEdgeTarget != null)
                {
                    if(!(edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeTarget == originatingNode))
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                            insertionPoint, edge, CheckCandidateForConnectednessType.Target, connectednessCheck);
                    }
                }
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeBothDirections(
                    insertionPoint, edge, true, connectednessCheck, out continuationPoint);
            }

            if(continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge of fixed direction
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            CheckCandidateForConnectednessType connectednessType,
            ConnectednessCheck connectednessCheck)
        {
            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            // don't need to, if that node is not matched by now (signaled by visited)
            SearchPlanNodeNode nodeRequiringCheck = 
                connectednessType == CheckCandidateForConnectednessType.Source ?
                    edge.PatternEdgeSource : edge.PatternEdgeTarget;
            if(nodeRequiringCheck.Visited)
            {
                CheckCandidateForConnectedness checkConnectedness =
                    new CheckCandidateForConnectedness(
                        edge.PatternElement.Name,
                        nodeRequiringCheck.PatternElement.Name,
                        edge.PatternElement.Name,
                        connectednessType);
                insertionPoint = insertionPoint.Append(checkConnectedness);

                connectednessCheck.PatternElementName = edge.PatternElement.UnprefixedName;
                connectednessCheck.PatternNodeName = nodeRequiringCheck.PatternElement.UnprefixedName;
                connectednessCheck.PatternEdgeName = edge.PatternElement.UnprefixedName;
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge in both directions
        /// and inserts them into the search program
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within both directions iteration was built
        /// todo: if no direction iteration was needed, insertion point == continuation point ?
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdgeBothDirections(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            bool edgeDeterminationContainsFirstNodeLoop,
            ConnectednessCheck connectednessCheck,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            if(!edgeDeterminationContainsFirstNodeLoop && currentEdgeConnectsToFirstIncidentNode(edge))
            {
                // due to currentEdgeConnectsToFirstIncidentNode: at least on incident node available
                if(edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    // reflexive edge without direction iteration as we don't want 2 matches 
                    SearchPlanNodeNode nodeRequiringFirstNodeLoop = edge.PatternEdgeSource != null ?
                        edge.PatternEdgeSource : edge.PatternEdgeTarget;
                    CheckCandidateForConnectedness checkConnectedness =
                        new CheckCandidateForConnectedness(
                            edge.PatternElement.Name,
                            nodeRequiringFirstNodeLoop.PatternElement.Name,
                            edge.PatternElement.Name,
                            CheckCandidateForConnectednessType.Source); // might be Target as well
                    insertionPoint = insertionPoint.Append(checkConnectedness);

                    connectednessCheck.PatternElementName = edge.PatternElement.UnprefixedName;
                    connectednessCheck.PatternNodeName = nodeRequiringFirstNodeLoop.PatternElement.UnprefixedName;
                    connectednessCheck.PatternEdgeName = edge.PatternElement.UnprefixedName;
                }
                else
                {
                    BothDirectionsIteration directionsIteration =
                       new BothDirectionsIteration(edge.PatternElement.Name);
                    directionsIteration.NestedOperationsList = new SearchProgramList(directionsIteration);
                    continuationPoint = insertionPoint.Append(directionsIteration);
                    insertionPoint = directionsIteration.NestedOperationsList;

                    SearchPlanNodeNode nodeRequiringFirstNodeLoop = edge.PatternEdgeSource != null ?
                        edge.PatternEdgeSource : edge.PatternEdgeTarget;
                    CheckCandidateForConnectedness checkConnectedness =
                        new CheckCandidateForConnectedness(
                            edge.PatternElement.Name,
                            nodeRequiringFirstNodeLoop.PatternElement.Name,
                            edge.PatternElement.Name,
                            CheckCandidateForConnectednessType.SourceOrTarget);
                    insertionPoint = insertionPoint.Append(checkConnectedness);

                    connectednessCheck.PatternElementName = edge.PatternElement.UnprefixedName;
                    connectednessCheck.PatternNodeName = nodeRequiringFirstNodeLoop.PatternElement.UnprefixedName;
                    connectednessCheck.PatternEdgeName = edge.PatternElement.UnprefixedName;
                }
            }
            if(currentEdgeConnectsToSecondIncidentNode(edge))
            {
                // due to currentEdgeConnectsToSecondIncidentNode: both incident node available
                CheckCandidateForConnectedness checkConnectedness =
                    new CheckCandidateForConnectedness(
                        edge.PatternElement.Name,
                        edge.PatternEdgeTarget.PatternElement.Name,
                        edge.PatternElement.Name,
                        edge.PatternEdgeSource.PatternElement.Name,
                        CheckCandidateForConnectednessType.TheOther);
                insertionPoint = insertionPoint.Append(checkConnectedness);

                connectednessCheck.PatternElementName = edge.PatternElement.UnprefixedName;
                connectednessCheck.PatternNodeName = edge.PatternEdgeTarget.PatternElement.UnprefixedName;
                connectednessCheck.PatternEdgeName = edge.PatternEdgeSource.PatternElement.UnprefixedName;
                connectednessCheck.TheOtherPatternNodeName = edge.PatternEdgeSource.PatternElement.UnprefixedName;
            }

            return insertionPoint;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// returns name prefix for candidate variables of the given pattern graph
        /// computed from current negative/independent pattern nesting
        /// </summary>
        public string NegativeIndependentNamePrefix(PatternGraph patternGraph)
        {
            string negativeIndependentNamePrefix = "";
            PatternGraph[] nestingPatterns = env.patternGraphWithNestingPatterns.ToArray(); // holy shit! no sets, no backward iterators, no direct access to stack ... c# data structures suck
            if(nestingPatterns[nestingPatterns.Length - 1] == patternGraph)
                return "";
            for(int i = nestingPatterns.Length - 2; i >= 0; --i) // skip first = top level pattern; stack dumped in reverse ^^
            {
                negativeIndependentNamePrefix += nestingPatterns[i].name;
                if(nestingPatterns[i] == patternGraph)
                    break; ;
            }

            if(negativeIndependentNamePrefix != "") {
                negativeIndependentNamePrefix += "_";
            }

            return negativeIndependentNamePrefix;
        }

        /// <summary>
        /// returns true if the node which gets currently determined in the schedule
        /// is the first incident node of the edge which gets connected to it
        /// only of interest for edges of unfixed direction
        /// </summary>
        private bool currentNodeIsFirstIncidentNodeOfEdge(
            SearchPlanNodeNode currentNode, SearchPlanEdgeNode edge)
        {
            //Debug.Assert(!currentNode.Visited);
            Debug.Assert(!((PatternEdge)edge.PatternElement).fixedDirection);

            if(!edge.Visited)
                return false;

            if(currentNode == edge.PatternEdgeSource)
                return edge.PatternEdgeTarget==null || !edge.PatternEdgeTarget.Visited;
            else
                return edge.PatternEdgeSource==null || !edge.PatternEdgeSource.Visited;
        }

        /// <summary>
        /// returns true if the node which gets currently determined in the schedule
        /// is the second incident node of the edge which gets connected to it
        /// only of interest for edges of unfixed direction
        /// </summary>
        private bool currentNodeIsSecondIncidentNodeOfEdge(
            SearchPlanNodeNode currentNode, SearchPlanEdgeNode edge)
        {
            //Debug.Assert(!currentNode.Visited);
            Debug.Assert(!((PatternEdge)edge.PatternElement).fixedDirection);

            if(!edge.Visited)
                return false;

            if(edge.PatternEdgeSource == null || edge.PatternEdgeTarget == null)
                return false;

            if(currentNode == edge.PatternEdgeSource)
                return edge.PatternEdgeTarget.Visited;
            else
                return edge.PatternEdgeSource.Visited;
        }

        /// <summary>
        /// returns true if only one incident node of the edge which gets currently determined in the schedule
        /// was already computed; only of interest for edges of unfixed direction
        /// </summary>
        private bool currentEdgeConnectsOnlyToFirstIncidentNode(SearchPlanEdgeNode currentEdge)
        {
            //Debug.Assert(!currentEdge.Visited);
            Debug.Assert(!((PatternEdge)currentEdge.PatternElement).fixedDirection);

            if(currentEdge.PatternEdgeSource != null && currentEdge.PatternEdgeSource.Visited
                && (currentEdge.PatternEdgeTarget == null || !currentEdge.PatternEdgeTarget.Visited))
            {
                return true;
            }
            if(currentEdge.PatternEdgeTarget != null && currentEdge.PatternEdgeTarget.Visited
                && (currentEdge.PatternEdgeSource == null || !currentEdge.PatternEdgeSource.Visited))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// returns true if at least one incident node of the edge which gets currently determined in the schedule
        /// was already computed; only of interest for edges of unfixed direction
        /// </summary>
        private bool currentEdgeConnectsToFirstIncidentNode(SearchPlanEdgeNode currentEdge)
        {
            //Debug.Assert(!currentEdge.Visited);
            Debug.Assert(!((PatternEdge)currentEdge.PatternElement).fixedDirection);

            if(currentEdge.PatternEdgeSource != null && currentEdge.PatternEdgeSource.Visited)
                return true;
            if(currentEdge.PatternEdgeTarget != null && currentEdge.PatternEdgeTarget.Visited)
                return true;

            return false;
        }

        /// <summary>
        /// returns true if both incident nodes of the edge which gets currently determined in the schedule
        /// were already computed; only of interest for edges of unfixed direction
        /// </summary>
        private bool currentEdgeConnectsToSecondIncidentNode(SearchPlanEdgeNode currentEdge)
        {
            //Debug.Assert(!currentEdge.Visited);
            Debug.Assert(!((PatternEdge)currentEdge.PatternElement).fixedDirection);

            if(currentEdge.PatternEdgeSource == null || currentEdge.PatternEdgeTarget == null)
                return false;

            if(currentEdge.PatternEdgeSource.Visited && currentEdge.PatternEdgeTarget.Visited)
                return true;

            return false;
        }

        /// <summary>
        /// Returns the variable which will evaluate at runtime to the match of the nesting pattern.
        /// Dependent on currently processed pattern graph of static nesting as given by nesting stack.
        /// </summary>
        public string getCurrentMatchOfNestingPattern()
        {
            if(env.patternGraphWithNestingPatterns.Count == 1)
            {
                if(env.programType == SearchProgramType.Subpattern || env.programType == SearchProgramType.AlternativeCase || env.programType == SearchProgramType.Iterated)
                    return "matchOfNestingPattern";
                else
                    return "null";
            }
            else // patternGraphWithNestingPatterns.Count > 1
            {
                int i = 0;
                foreach(PatternGraph patternGraph in env.patternGraphWithNestingPatterns)
                {
                    ++i;
                    if(i == 2)
                        return NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
                }
                return "null"; // shut up warning
            }
        }

        /// <summary>
        /// Returns the variable which will evaluate at runtime to the last match at the previous nesting level.
        /// Dependent on currently processed pattern graph of static nesting as given by nesting stack.
        /// </summary>
        public string getCurrentLastMatchAtPreviousNestingLevel()
        {
            if(env.patternGraphWithNestingPatterns.Count == 1)
            {
                if(env.programType == SearchProgramType.Subpattern || env.programType == SearchProgramType.AlternativeCase || env.programType == SearchProgramType.Iterated)
                    return "lastMatchAtPreviousNestingLevel";
                else
                    return "null";
            }
            else // patternGraphWithNestingPatterns.Count > 1
            {
                int i = 0;
                foreach(PatternGraph patternGraph in env.patternGraphWithNestingPatterns)
                {
                    ++i;
                    if(i == 2)
                        return NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
                }
                return "null"; // shut up warning
            }
        }

        private EntityType toBubbleUpYieldAssignmentType(GrGenType type)
        {
            if(type is VarType)
                return EntityType.Variable;
            else if(type is NodeType)
                return EntityType.Node;
            else //if(type is EdgeType)
                return EntityType.Edge;
        }

        /// <summary>
        /// Returns the element from the pattern graph which was inserted by inlining the patternEmbedding
        /// and which originally bears the unprefixed name given
        /// </summary>
        private IPatternElement getInlinedElementByOriginalUnprefixedName(
            PatternGraph patternGraph, PatternGraphEmbedding patternEmbedding, string name)
        {
            foreach(PatternNode node in patternGraph.nodesPlusInlined)
            {
                if(node.originalNode != null)
                {
                    foreach(PatternNode embeddedNode in patternEmbedding.matchingPatternOfEmbeddedGraph.patternGraph.nodes)
                    {
                        if(node.originalNode == embeddedNode
                            && embeddedNode.unprefixedName == name)
                        {
                            return node;
                        }
                    }
                }
            }
            foreach(PatternEdge edge in patternGraph.edgesPlusInlined)
            {
                if(edge.originalEdge != null)
                {
                    foreach(PatternEdge embeddedEdge in patternEmbedding.matchingPatternOfEmbeddedGraph.patternGraph.edges)
                    {
                        if(edge.originalEdge == embeddedEdge
                            && embeddedEdge.unprefixedName == name)
                        {
                            return edge;
                        }
                    }
                }
            }
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(var.originalVariable != null)
                {
                    foreach(PatternVariable embeddedVar in patternEmbedding.matchingPatternOfEmbeddedGraph.patternGraph.variables)
                    {
                        if(var.originalVariable == embeddedVar
                            && embeddedVar.unprefixedName == name)
                        {
                            return var;
                        }
                    }
                }
            }
            return null;
        }
    }
}

