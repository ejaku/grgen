/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

//#define NO_ADJUST_LIST_HEADS

using System;
using System.Collections.Generic;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using System.Text;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// says what kind of search program to build
    /// </summary>
    enum SearchProgramType
    {
        Action, // action pattern matcher
        MissingPreset, // missing preset matcher
        Subpattern, // subpattern matcher
        AlternativeCase // alternative case matcher
    }

    /// <summary>
    /// class for building search program data structure from scheduled search plan
    /// holds environment variables for this build process
    /// </summary>
    class SearchProgramBuilder
    {
        /// <summary>
        /// Builds search program from scheduled search plan in pattern graph of the action rule pattern
        /// </summary>
        public SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPRulePattern rulePattern,
            string nameOfSearchProgram,
            List<string> parametersList,
            List<bool> parameterIsNodeList)
        {
            Debug.Assert(nameOfSearchProgram == null && parametersList == null && parameterIsNodeList == null
                || nameOfSearchProgram != null && parametersList != null && parameterIsNodeList != null);

            programType = nameOfSearchProgram != null ? SearchProgramType.MissingPreset : SearchProgramType.Action;
            this.model = model;
            patternGraph = rulePattern.patternGraph;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(rulePattern.name, false);
            negativeIndependentNames = new List<string>();
            negLevelNeverAboveMaxNegLevel = computeMaxNegLevel(rulePattern.patternGraph) <= (int) LGSPElemFlags.MAX_NEG_LEVEL;

            SearchProgram searchProgram;
            if (parametersList != null)
            {
                string[] parameters = new string[parametersList.Count];
                bool[] parameterIsNode = new bool[parameterIsNodeList.Count];
                int i = 0;
                foreach (string p in parametersList)
                {
                    parameters[i] = p;
                    ++i;
                }
                i = 0;
                foreach (bool pis in parameterIsNodeList)
                {
                    parameterIsNode[i] = pis;
                    ++i;
                }

                // build outermost search program operation, create the list anchor starting it's program
                searchProgram = new SearchProgramOfMissingPreset(
                    nameOfSearchProgram,
                    patternGraph.embeddedGraphs.Length > 0 || patternGraph.alternatives.Length > 0,
                    parameters, parameterIsNode);
            }
            else
            {
                // build outermost search program operation, create the list anchor starting it's program
                searchProgram = new SearchProgramOfAction(
                    "myMatch",
                    patternGraph.embeddedGraphs.Length > 0 || patternGraph.alternatives.Length > 0);
            }
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            for(int i = 0; i < patternGraph.variables.Length; i++)
            {
                PatternVariable var = patternGraph.variables[i];
                Type varType = var.Type.Type;
                String varTypeName;
                if(varType.IsGenericType)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(varType.Name.Substring(0, varType.Name.IndexOf('`')));
                    sb.Append('<');
                    bool first = true;
                    foreach(Type typeArg in varType.GetGenericArguments())
                    {
                        if(first) first = false;
                        else sb.Append(", ");
                        sb.Append(typeArg.FullName);
                    }
                    sb.Append('>');
                    varTypeName = sb.ToString();
                }
                else varTypeName = varType.Name;
                insertionPoint = insertionPoint.Append(new ExtractVariable(varTypeName, var.Name, var.ParameterIndex));
            }

            // start building with first operation in scheduled search plan
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);

            return searchProgram;
        }

        /// <summary>
        /// Builds search program from scheduled search plan in pattern graph of the subpattern rule pattern
        /// </summary>
        public SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPMatchingPattern matchingPattern)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));

            programType = SearchProgramType.Subpattern;
            this.model = model;
            patternGraph = matchingPattern.patternGraph;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, true);
            negativeIndependentNames = new List<string>();
            negLevelNeverAboveMaxNegLevel = false;

            // build outermost search program operation, create the list anchor starting it's program
            SearchProgram searchProgram = new SearchProgramOfSubpattern("myMatch");
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            // initialize task/result-pushdown handling in subpattern matcher
            InitializeSubpatternMatching initialize = new InitializeSubpatternMatching();
            insertionPoint = insertionPoint.Append(initialize);

            // start building with first operation in scheduled search plan
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);

            // finalize task/result-pushdown handling in subpattern matcher
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching();
            insertionPoint = insertionPoint.Append(finalize);

            return searchProgram;
        }

        /// <summary>
        /// Builds search program for alternative from scheduled search plans of the alternative cases
        /// </summary>
        public SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPMatchingPattern matchingPattern,
            Alternative alternative)
        {
            programType = SearchProgramType.AlternativeCase;
            this.model = model;
            this.alternative = alternative;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, !(matchingPattern is LGSPRulePattern));
            negativeIndependentNames = new List<string>();
            negLevelNeverAboveMaxNegLevel = false;

            // build outermost search program operation, create the list anchor starting it's program
            SearchProgram searchProgram = new SearchProgramOfAlternative("myMatch");
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            // initialize task/result-pushdown handling in subpattern matcher
            InitializeSubpatternMatching initialize = new InitializeSubpatternMatching();
            insertionPoint = insertionPoint.Append(initialize);

            // build alternative matching search programs, one per case
            for (int i=0; i<alternative.alternativeCases.Length; ++i)
            {
                PatternGraph altCase = alternative.alternativeCases[i];
                ScheduledSearchPlan scheduledSearchPlan = altCase.scheduleIncludingNegativesAndIndependents;

                GetPartialMatchOfAlternative matchAlternative = new GetPartialMatchOfAlternative(
                    scheduledSearchPlan.PatternGraph.pathPrefix, 
                    scheduledSearchPlan.PatternGraph.name,
                    rulePatternClassName);
                matchAlternative.OperationsList = new SearchProgramList(matchAlternative);
                insertionPoint = insertionPoint.Append(matchAlternative);

                this.patternGraph = altCase;
                isNegative = false;

                // start building with first operation in scheduled search plan
                BuildScheduledSearchPlanOperationIntoSearchProgram(
                    0,
                    matchAlternative.OperationsList);

                // save matches found by alternative case to get clean start for matching next alternative case
                if(i<alternative.alternativeCases.Length-1)
                {
                    NewMatchesListForFollowingMatches newMatchesList =
                        new NewMatchesListForFollowingMatches(true);
                    insertionPoint = insertionPoint.Append(newMatchesList);
                }
            }
            this.patternGraph = null;

            // finalize task/result-pushdown handling in subpattern matcher
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching();
            insertionPoint = insertionPoint.Append(finalize);

            return searchProgram;
        }

        /// <summary>
        /// Create an extra search subprogram per MaybePreset operation.
        /// Created search programs are added to search program next field, forming list.
        /// Destroys the scheduled search program.
        /// </summary>
        public void BuildAddionalSearchSubprograms(ScheduledSearchPlan scheduledSearchPlan,
            SearchProgram searchProgram, LGSPRulePattern rulePattern)
        {
            // insertion point for next search subprogram
            SearchProgramOperation insertionPoint = searchProgram;
            // for stepwise buildup of parameter lists
            List<string> neededElementsInRemainderProgram = new List<string>();
            List<bool> neededElementInRemainderProgramIsNode = new List<bool>();

            foreach (SearchOperation op in scheduledSearchPlan.Operations)
            {
                switch (op.Type)
                {
                    // candidates bound in the program up to the maybe preset operation
                    // are handed in to the "lookup missing element and continue"
                    // remainder search program as parameters 
                    // - so they don't need to be searched any more
                    case SearchOperationType.Lookup:
                    case SearchOperationType.Outgoing:
                    case SearchOperationType.Incoming:
                    case SearchOperationType.Incident:
                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                    case SearchOperationType.Implicit:
                        // don't search
                        op.Type = SearchOperationType.Void;
                        // make parameter in remainder program
                        SearchPlanNode element = (SearchPlanNode)op.Element;
                        neededElementsInRemainderProgram.Add(element.PatternElement.Name);
                        neededElementInRemainderProgramIsNode.Add(element.NodeType == PlanNodeType.Node);
                        break;
                    // check operations were already executed in the calling program 
                    // they don't need any treatement in the remainder search program
                    case SearchOperationType.Condition:
                    case SearchOperationType.NegativePattern:
                    case SearchOperationType.IndependentPattern:
                        op.Type = SearchOperationType.Void;
                        break;
                    // the maybe preset operation splitting off an extra search program
                    // to look the missing element up and continue within that 
                    // remainder search program until the end (or the next split)
                    // replace the maybe preset by a lookup within that program
                    // and build the extra program (with the rest unmodified (yet))
                    // insert the search program into the search program list
                    case SearchOperationType.MaybePreset:
                        // change maybe preset on element which was not handed in  
                        // into lookup on element, then build search program with lookup
                        op.Type = SearchOperationType.Lookup;
                        element = (SearchPlanNode)op.Element;
                        SearchProgramOperation searchSubprogram =
                            BuildSearchProgram(
                                model,
                                rulePattern,
                                NamesOfEntities.MissingPresetHandlingMethod(element.PatternElement.Name),
                                neededElementsInRemainderProgram,
                                neededElementInRemainderProgramIsNode
                                );
                        insertionPoint = insertionPoint.Append(searchSubprogram);
                        // search calls to this new search program and complete arguments in
                        CompleteCallsToMissingPresetHandlingMethodInAllSearchPrograms(
                            searchProgram,
                            NamesOfEntities.MissingPresetHandlingMethod(element.PatternElement.Name),
                            neededElementsInRemainderProgram,
                            neededElementInRemainderProgramIsNode);
                        // make parameter in remainder program
                        neededElementsInRemainderProgram.Add(element.PatternElement.Name);
                        neededElementInRemainderProgramIsNode.Add(element.NodeType == PlanNodeType.Node);
                        // handled, now do same as with other candidate binding operations
                        op.Type = SearchOperationType.Void;
                        break;
                    // operations which are not allowed in a positive scheduled search plan
                    // after ssp creation and/or the buildup pass
                    case SearchOperationType.Void:
                    case SearchOperationType.NegIdptPreset:
                    case SearchOperationType.SubPreset:
                    default:
                        Debug.Assert(false, "At this pass/position not allowed search operation");
                        break;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// type of the program which gets currently built
        /// </summary>
        private SearchProgramType programType;

        /// <summary>
        /// The model for which the matcher functions shall be generated.
        /// </summary>
        private IGraphModel model;

        /// <summary>
        /// the pattern graph to build (of the rule pattern or the subpattern)
        /// </summary>
        private PatternGraph patternGraph;

        /// <summary>
        /// is the pattern graph a negative pattern graph?
        /// </summary>
        private bool isNegative;

        /// <summary>
        /// is the current pattern graph nested within a negative pattern graph?
        /// </summary>
        private bool isNestedInNegative;

        /// <summary>
        /// the alternative to build
        /// non-null if the builder constructs an alternative
        /// </summary>
        private Alternative alternative;

        /// <summary>
        /// name of the rule pattern class of the pattern graph
        /// </summary>
        string rulePatternClassName;

        /// <summary>
        /// stack of negative/independent names representing the nesting of negative/independent patterns (nac/pac)
        /// the top of stack is the name of the negative/independent pattern whose buildup is currently underway
        /// </summary>
        private List<string> negativeIndependentNames;

        /// <summary>
        /// true if statically determined that the neg level of the pattern getting constructed 
        /// is always below the maximum neg level
        /// </summary>
        private bool negLevelNeverAboveMaxNegLevel;

        /// <summary>
        /// name says everything
        /// </summary>
        private const int MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID = 2;

        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Builds search program operations from scheduled search plan operation.
        /// Decides which specialized build procedure is to be called.
        /// The specialized build procedure then calls this procedure again, 
        /// in order to process the next search plan operation.
        /// </summary>
        private SearchProgramOperation BuildScheduledSearchPlanOperationIntoSearchProgram(
            int indexOfScheduledSearchPlanOperationToBuild,
            SearchProgramOperation insertionPointWithinSearchProgram)
        {
            if (indexOfScheduledSearchPlanOperationToBuild >=
                patternGraph.scheduleIncludingNegativesAndIndependents.Operations.Length)
            { // end of scheduled search plan reached, stop recursive iteration
                return buildMatchComplete(insertionPointWithinSearchProgram);
            }

            SearchOperation op = patternGraph.scheduleIncludingNegativesAndIndependents.
                Operations[indexOfScheduledSearchPlanOperationToBuild];

            // for current scheduled search plan operation 
            // insert corresponding search program operations into search program
            switch (op.Type)
            {
                case SearchOperationType.Void:
                    return BuildScheduledSearchPlanOperationIntoSearchProgram(
                        indexOfScheduledSearchPlanOperationToBuild + 1,
                        insertionPointWithinSearchProgram);

                case SearchOperationType.MaybePreset:
                    return buildMaybePreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.NegIdptPreset:
                    return buildNegIdptPreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.SubPreset:
                    return buildSubPreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.Lookup:
                    return buildLookup(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.ImplicitSource:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        ImplicitNodeType.Source);

                case SearchOperationType.ImplicitTarget:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        ImplicitNodeType.Target);

                case SearchOperationType.Implicit:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        ImplicitNodeType.SourceOrTarget);

                case SearchOperationType.Incoming:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        IncidentEdgeType.Incoming);

                case SearchOperationType.Outgoing:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        IncidentEdgeType.Outgoing);

                case SearchOperationType.Incident:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        IncidentEdgeType.IncomingOrOutgoing);

                case SearchOperationType.NegativePattern:
                    return buildNegative(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        ((ScheduledSearchPlan)op.Element).PatternGraph);

                case SearchOperationType.IndependentPattern:
                    return buildIndependent(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        ((ScheduledSearchPlan)op.Element).PatternGraph);

                case SearchOperationType.Condition:
                    return buildCondition(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (PatternCondition)op.Element);

                default:
                    Debug.Assert(false, "Unknown search operation");
                    return insertionPointWithinSearchProgram;
            }
        }

        /// <summary>
        /// Search program operations implementing the
        /// MaybePreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildMaybePreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();
            Debug.Assert(negativeIndependentNamePrefix == "", "Top-level maybe preset in negative/independent search plan");
            Debug.Assert(programType != SearchProgramType.Subpattern, "Maybe preset in subpattern");
            Debug.Assert(programType != SearchProgramType.AlternativeCase, "Maybe preset in alternative");

            // get candidate from inputs
            GetCandidateByDrawing fromInputs =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromInputs,
                    target.PatternElement.Name,
                    target.PatternElement.ParameterIndex.ToString(),
                    isNode);
            insertionPoint = insertionPoint.Append(fromInputs);

            // check whether candidate was preset (not null)
            // continues with missing preset searching and continuing search program on fail
            CheckCandidateForPreset checkPreset = new CheckCandidateForPreset(
                target.PatternElement.Name,
                isNode);
            insertionPoint = insertionPoint.Append(checkPreset);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check candidate for isomorphy 
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(acceptCandidate);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // abandon candidate (restore isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// NegIdptPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegIdptPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();
            Debug.Assert(negativeIndependentNamePrefix != "", "Negative/Independent preset in top-level search plan");

            // check candidate for isomorphy 
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(acceptCandidate);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // abandon candidate (restore isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// SubPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildSubPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();
            Debug.Assert(negativeIndependentNamePrefix == "", "Top-level subpattern preset in negative/independent search plan");

            // get candidate from inputs
            GetCandidateByDrawing fromInputs =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromSubpatternConnections,
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(fromInputs);

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Lookup search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildLookup(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            // decide on and insert operation determining type of candidate
            SearchProgramOperation continuationPointAfterTypeIteration;
            SearchProgramOperation insertionPointAfterTypeIteration =
                decideOnAndInsertGetType(insertionPoint, target,
                out continuationPointAfterTypeIteration);
            insertionPoint = insertionPointAfterTypeIteration;

#if RANDOM_LOOKUP_LIST_START
            // insert list heads randomization, thus randomized lookup
            RandomizeListHeads randomizeListHeads =
                new RandomizeListHeads(
                    RandomizeListHeadsTypes.GraphElements,
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(randomizeListHeads);
#endif

            // iterate available graph elements
            GetCandidateByIteration elementsIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.GraphElements,
                    target.PatternElement.Name,
                    isNode);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if (isNode)
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFromLookup(
                    insertionPoint, (SearchPlanNodeNode)target, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFromLookup(
                    insertionPoint, (SearchPlanEdgeNode)target, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if (programType==SearchProgramType.Subpattern || programType==SearchProgramType.AlternativeCase)
            {
                CheckCandidateForIsomorphyGlobal checkIsomorphy =
                    new CheckCandidateForIsomorphyGlobal(
                        target.PatternElement.Name,
                        isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                        isNode);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(acceptCandidate);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // abandon candidate (restore isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list at type iteration nesting level
            insertionPoint = continuationPoint;

            // everything nested within type iteration built by now
            // continue at the end of the list handed in
            if (insertionPointAfterTypeIteration != continuationPointAfterTypeIteration)
            {
                // if type was drawn then the if is not entered (insertion point==continuation point)
                // thus we continue at the continuation point of the candidate iteration
                // otherwise if type was iterated
                // we continue at the continuation point of the type iteration
                insertionPoint = continuationPointAfterTypeIteration;
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Implicit Source|Target|SourceOrTarget search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildImplicit(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanEdgeNode source,
            SearchPlanNodeNode target,
            IsomorphyInformation isomorphy,
            ImplicitNodeType nodeType)
        {
            // get candidate = demanded node from edge
            SearchProgramOperation continuationPoint;
            insertionPoint = insertImplicitNodeFromEdge(insertionPoint, source, target, nodeType,
                out continuationPoint);
            if (continuationPoint == insertionPoint)
                continuationPoint = null;

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            SearchPlanNodeNode otherNodeOfOriginatingEdge = null;
            if (nodeType == ImplicitNodeType.Source) otherNodeOfOriginatingEdge = source.PatternEdgeTarget;
            if (nodeType == ImplicitNodeType.Target) otherNodeOfOriginatingEdge = source.PatternEdgeSource;
            if (source.PatternEdgeTarget == source.PatternEdgeSource) // reflexive sign needed in unfixed direction case, too
                otherNodeOfOriginatingEdge = source.PatternEdgeSource;
            insertionPoint = decideOnAndInsertCheckConnectednessOfImplicitNodeFromEdge(
                insertionPoint, target, source, otherNodeOfOriginatingEdge, out continuationPointAfterConnectednessCheck);
            if (continuationPoint == null && continuationPointAfterConnectednessCheck != insertionPoint)
                continuationPoint = continuationPointAfterConnectednessCheck;

            // check candidate for isomorphy 
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        true,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if (programType == SearchProgramType.Subpattern || programType == SearchProgramType.AlternativeCase)
            {
                CheckCandidateForIsomorphyGlobal checkIsomorphy =
                    new CheckCandidateForIsomorphyGlobal(
                        target.PatternElement.Name,
                        isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                        true);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        true,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(acceptCandidate);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // abandon candidate (restore isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        true,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            if (continuationPoint != null)
                insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Extend Incoming|Outgoing|IncomingOrOutgoing search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildIncident(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNodeNode source,
            SearchPlanEdgeNode target,
            IsomorphyInformation isomorphy,
            IncidentEdgeType edgeType)
        {
#if RANDOM_LOOKUP_LIST_START
            // insert list heads randomization, thus randomized extend
            RandomizeListHeads randomizeListHeads =
                new RandomizeListHeads(
                    RandomizeListHeadsTypes.IncidentEdges,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    getIncoming);
            insertionPoint = insertionPoint.Append(randomizeListHeads);
#endif

            // iterate available incident edges
            SearchProgramOperation continuationPoint;
            insertionPoint = insertIncidentEdgeFromNode(insertionPoint, source, target, edgeType,
                out continuationPoint);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointOfConnectednessCheck;
            insertionPoint = decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(
                insertionPoint, target, source, edgeType==IncidentEdgeType.Incoming, 
                out continuationPointOfConnectednessCheck);

            // check candidate for isomorphy 
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        false,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if (programType == SearchProgramType.Subpattern || programType == SearchProgramType.AlternativeCase)
            {
                CheckCandidateForIsomorphyGlobal checkIsomorphy =
                    new CheckCandidateForIsomorphyGlobal(
                        target.PatternElement.Name,
                        isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                        false);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        false,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(acceptCandidate);
            }

            // mark element as visited
            target.Visited = true;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            // unmark element for possibly following run
            target.Visited = false;

            // abandon candidate (restore isomorphy information)
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        false,
                        negLevelNeverAboveMaxNegLevel);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within incident iteration built by now -
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Negative search plan operation (searching of negative application condition)
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegative(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            PatternGraph negativePatternGraph)
        {
            // fill needed elements array for CheckPartialMatchByNegative
            int numberOfNeededElements = 0;
            foreach (SearchOperation op in negativePatternGraph.scheduleIncludingNegativesAndIndependents.Operations)
            {
                if (op.Type == SearchOperationType.NegIdptPreset)
                {
                    ++numberOfNeededElements;
                }
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach (SearchOperation op in negativePatternGraph.scheduleIncludingNegativesAndIndependents.Operations)
            {
                if (op.Type == SearchOperationType.NegIdptPreset)
                {
                    SearchPlanNode element = ((SearchPlanNode)op.Element);
                    neededElements[i] = element.PatternElement.Name;
                    ++i;
                }
            }

            CheckPartialMatchByNegative checkNegative =
               new CheckPartialMatchByNegative(neededElements);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(checkNegative);
            checkNegative.NestedOperationsList =
                new SearchProgramList(checkNegative);
            insertionPoint = checkNegative.NestedOperationsList;

            PatternGraph enclosingPatternGraph = patternGraph;
            bool enclosingIsNegative = isNegative;
            bool enclosingIsNestedInNegative = isNestedInNegative;
            patternGraph = negativePatternGraph;
            isNegative = true;
            isNestedInNegative = true; 
            negativeIndependentNames.Add(negativePatternGraph.name);

            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();
            bool negativeContainsSubpatterns = negativePatternGraph.EmbeddedGraphs.Length >= 1
                || negativePatternGraph.Alternatives.Length >= 1;
            InitializeNegativeIndependentMatching initNeg = new InitializeNegativeIndependentMatching(
                negativeContainsSubpatterns, 
                negativeIndependentNamePrefix, 
                negLevelNeverAboveMaxNegLevel);
            insertionPoint = insertionPoint.Append(initNeg);


            //---------------------------------------------------------------------------
            // build negative pattern
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);
            //---------------------------------------------------------------------------

            FinalizeNegativeIndependentMatching finalize = new FinalizeNegativeIndependentMatching(negLevelNeverAboveMaxNegLevel);
            insertionPoint = insertionPoint.Append(finalize);

            // negative pattern built by now
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;
            negativeIndependentNames.RemoveAt(negativeIndependentNames.Count - 1);
            patternGraph = enclosingPatternGraph;
            isNegative = enclosingIsNegative;
            isNestedInNegative = enclosingIsNestedInNegative;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Independent search plan operation (searching of positive application condition)
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildIndependent(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            PatternGraph independentPatternGraph)
        {
            // fill needed elements array for CheckPartialMatchByIndependent
            int numberOfNeededElements = 0;
            foreach (SearchOperation op in independentPatternGraph.scheduleIncludingNegativesAndIndependents.Operations)
            {
                if (op.Type == SearchOperationType.NegIdptPreset)
                {
                    ++numberOfNeededElements;
                }
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach (SearchOperation op in independentPatternGraph.scheduleIncludingNegativesAndIndependents.Operations)
            {
                if (op.Type == SearchOperationType.NegIdptPreset)
                {
                    SearchPlanNode element = ((SearchPlanNode)op.Element);
                    neededElements[i] = element.PatternElement.Name;
                    ++i;
                }
            }

            CheckPartialMatchByIndependent checkIndependent =
               new CheckPartialMatchByIndependent(neededElements);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(checkIndependent);
            checkIndependent.NestedOperationsList =
                new SearchProgramList(checkIndependent);
            insertionPoint = checkIndependent.NestedOperationsList;

            PatternGraph enclosingPatternGraph = patternGraph;
            bool enclosingIsNegative = isNegative;
            patternGraph = independentPatternGraph;
            isNegative = false;
            negativeIndependentNames.Add(independentPatternGraph.name);

            string independentNamePrefix = NegativeIndependentNamePrefix();
            bool independentContainsSubpatterns = independentPatternGraph.EmbeddedGraphs.Length >= 1
                || independentPatternGraph.Alternatives.Length >= 1;
            InitializeNegativeIndependentMatching initIdpt = new InitializeNegativeIndependentMatching(
                independentContainsSubpatterns,
                independentNamePrefix,
                negLevelNeverAboveMaxNegLevel);
            insertionPoint = insertionPoint.Append(initIdpt);


            //---------------------------------------------------------------------------
            // build independent pattern
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);
            //---------------------------------------------------------------------------

            FinalizeNegativeIndependentMatching finalize = new FinalizeNegativeIndependentMatching(negLevelNeverAboveMaxNegLevel);
            insertionPoint = insertionPoint.Append(finalize);

            // independent pattern built by now
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;

            // the matcher program of independent didn't find a match,
            // we fell through the loops and reached this point -> abort the matching process / try next candidate
            CheckContinueMatchingOfIndependentFailed abortMatching =
                new CheckContinueMatchingOfIndependentFailed(checkIndependent);
            checkIndependent.CheckIndependentFailed = abortMatching;
            insertionPoint = insertionPoint.Append(abortMatching);

            negativeIndependentNames.RemoveAt(negativeIndependentNames.Count - 1);
            patternGraph = enclosingPatternGraph;
            isNegative = enclosingIsNegative;

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Condition search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildCondition(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            PatternCondition condition)
        {
            // generate c#-code-string out of condition expression ast
            SourceBuilder conditionExpression = new SourceBuilder();
            condition.ConditionExpression.Emit(conditionExpression);

            // check condition with current partial match
            CheckPartialMatchByCondition checkCondition =
                new CheckPartialMatchByCondition(conditionExpression.ToString(),
                    condition.NeededNodes,
                    condition.NeededEdges,
                    condition.NeededVariables);
            insertionPoint = insertionPoint.Append(checkCondition);

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations completing the matching process
        /// after all pattern elements have been found are created and inserted into the program
        /// </summary>
        private SearchProgramOperation buildMatchComplete(
            SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            // may be a top-level-pattern with/-out subpatterns, may be a subpattern with/-out subpatterns
            bool isSubpattern = programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase;
            bool containsSubpatterns = patternGraph.embeddedGraphs.Length >= 1
                || patternGraph.alternatives.Length >= 1;

            // push subpattern tasks (in case there are some)
            if (containsSubpatterns)
                insertionPoint = insertPushSubpatternTasks(insertionPoint);

            // if this is a subpattern without subpatterns
            // it may be the last to be matched - handle that case
            if (!containsSubpatterns && isSubpattern && negativeIndependentNamePrefix=="")
                insertionPoint = insertCheckForTasksLeft(insertionPoint);

            // if this is a subpattern or a top-level pattern which contains subpatterns
            if (containsSubpatterns || isSubpattern && negativeIndependentNamePrefix=="")
            {
                // we do the global accept of all candidate elements (write isomorphy information)
                insertionPoint = insertGlobalAccept(insertionPoint);

                // and execute the open subpattern matching tasks
                MatchSubpatterns matchSubpatterns = new MatchSubpatterns(negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(matchSubpatterns);
            }

            // pop subpattern tasks (in case there are/were some)
            if (containsSubpatterns)
                insertionPoint = insertPopSubpatternTasks(insertionPoint);

            // handle matched or not matched subpatterns, matched local pattern
            if(negativeIndependentNamePrefix == "")
            {
                // subpattern or top-level pattern which contains subpatterns
                if (containsSubpatterns || isSubpattern)
                    insertionPoint = insertCheckForSubpatternsFound(insertionPoint);
                else // top-level-pattern without subpatterns
                    insertionPoint = insertPatternFound(insertionPoint);
            }
            else
            {
                // negative/independent contains subpatterns
                if (containsSubpatterns)
                    insertionPoint = insertCheckForSubpatternsFoundNegativeIndependent(insertionPoint);
                else
                    insertionPoint = insertPatternFoundNegativeIndependent(insertionPoint);
            }

            // global abandon of all accepted candidate elements (remove isomorphy information)
            if (containsSubpatterns || isSubpattern && negativeIndependentNamePrefix=="")
                insertionPoint = insertGlobalAbandon(insertionPoint);

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to get an implicit node from an edge
        /// </summary>
        private SearchProgramOperation insertImplicitNodeFromEdge(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode currentNode,
            ImplicitNodeType nodeType,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            GetCandidateByDrawing nodeFromEdge;
            if (nodeType == ImplicitNodeType.Source || nodeType == ImplicitNodeType.Target)
            {
                nodeFromEdge = new GetCandidateByDrawing(
                    GetCandidateByDrawingType.NodeFromEdge,
                    currentNode.PatternElement.Name,
                    model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
                    edge.PatternElement.Name,
                    nodeType);
                insertionPoint = insertionPoint.Append(nodeFromEdge);
            }
            else
            {
                Debug.Assert(nodeType != ImplicitNodeType.TheOther);

                if (currentNodeIsSecondIncidentNodeOfEdge(currentNode, edge))
                {
                    nodeFromEdge = new GetCandidateByDrawing(
                        GetCandidateByDrawingType.NodeFromEdge,
                        currentNode.PatternElement.Name,
                        model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
                        edge.PatternElement.Name,
                        edge.PatternEdgeSource == currentNode ? edge.PatternEdgeTarget.PatternElement.Name
                            : edge.PatternEdgeSource.PatternElement.Name,
                        ImplicitNodeType.TheOther);
                    insertionPoint = insertionPoint.Append(nodeFromEdge);
                }
                else // edge connects to first incident node
                {
                    if (edge.PatternEdgeSource == edge.PatternEdgeTarget)
                    {
                        // reflexive edge without direction iteration as we don't want 2 matches 
                        nodeFromEdge = new GetCandidateByDrawing(
                            GetCandidateByDrawingType.NodeFromEdge,
                            currentNode.PatternElement.Name,
                            model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
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
                            model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
                            edge.PatternElement.Name,
                            ImplicitNodeType.SourceOrTarget);
                        insertionPoint = insertionPoint.Append(nodeFromEdge);
                    }
                }
            }

            if (continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to get an incident edge from some node
        /// </summary>
        private SearchProgramOperation insertIncidentEdgeFromNode(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            SearchPlanEdgeNode currentEdge,
            IncidentEdgeType incidentType,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            GetCandidateByIteration incidentIteration;
            if (incidentType == IncidentEdgeType.Incoming || incidentType == IncidentEdgeType.Outgoing)
            {
                incidentIteration = new GetCandidateByIteration(
                    GetCandidateByIterationType.IncidentEdges,
                    currentEdge.PatternElement.Name,
                    node.PatternElement.Name,
                    incidentType);
                incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                continuationPoint = insertionPoint.Append(incidentIteration);
                insertionPoint = incidentIteration.NestedOperationsList;
            }
            else // IncidentEdgeType.IncomingOrOutgoing
            {
                if (currentEdge.PatternEdgeSource == currentEdge.PatternEdgeTarget)
                {
                    // reflexive edge without direction iteration as we don't want 2 matches 
                    incidentIteration = new GetCandidateByIteration(
                        GetCandidateByIterationType.IncidentEdges,
                        currentEdge.PatternElement.Name,
                        node.PatternElement.Name,
                        IncidentEdgeType.Incoming);
                    incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                    continuationPoint = insertionPoint.Append(incidentIteration);
                    insertionPoint = incidentIteration.NestedOperationsList;
                }
                else
                {
                    BothDirectionsIteration directionsIteration =
                        new BothDirectionsIteration(currentEdge.PatternElement.Name);
                    directionsIteration.NestedOperationsList = new SearchProgramList(directionsIteration);
                    continuationPoint = insertionPoint.Append(directionsIteration);
                    insertionPoint = directionsIteration.NestedOperationsList;

                    incidentIteration = new GetCandidateByIteration(
                        GetCandidateByIterationType.IncidentEdges,
                        currentEdge.PatternElement.Name,
                        node.PatternElement.Name,
                        IncidentEdgeType.IncomingOrOutgoing);
                    incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                    insertionPoint = insertionPoint.Append(incidentIteration);
                    insertionPoint = incidentIteration.NestedOperationsList;
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to build the match object
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertMatchObjectBuilding(
            SearchProgramOperation insertionPoint,
            bool isIndependent)
        {
            String matchObjectName = isIndependent ? NamesOfEntities.MatchedIndependentVariable(patternGraph.pathPrefix + patternGraph.name)
                : "match";
            String enumPrefix = patternGraph.pathPrefix + patternGraph.name + "_";
            foreach(PatternVariable var in patternGraph.variables)
            {
                BuildMatchObject buildMatch = 
                    new BuildMatchObject(
                        BuildMatchObjectType.Variable,
                        var.Type.Name,
                        var.UnprefixedName,
                        var.Name,
                        rulePatternClassName, 
                        enumPrefix, 
                        matchObjectName,
                        -1
                    );
                insertionPoint = insertionPoint.Append(buildMatch);
            }
            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Node,
                        patternGraph.nodes[i].typeName,
                        patternGraph.nodes[i].UnprefixedName,
                        patternGraph.nodes[i].Name,
                        rulePatternClassName,
                        enumPrefix,
                        matchObjectName,
                        -1
                    );
                insertionPoint = insertionPoint.Append(buildMatch);
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Edge,
                        patternGraph.edges[i].typeName,
                        patternGraph.edges[i].UnprefixedName,
                        patternGraph.edges[i].Name,
                        rulePatternClassName,
                        enumPrefix,
                        matchObjectName,
                        -1
                    );
                insertionPoint = insertionPoint.Append(buildMatch);
            }
            for (int i = 0; i < patternGraph.embeddedGraphs.Length; ++i)
            {
                string subpatternContainingType = NamesOfEntities.RulePatternClassName(patternGraph.embeddedGraphs[i].EmbeddedGraph.Name, true);
                string subpatternType = NamesOfEntities.MatchClassName(patternGraph.embeddedGraphs[i].EmbeddedGraph.Name);
                BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Subpattern,
                        subpatternContainingType+"."+subpatternType,
                        patternGraph.embeddedGraphs[i].name,
                        patternGraph.embeddedGraphs[i].name,
                        rulePatternClassName,
                        enumPrefix,
                        matchObjectName,
                        -1
                    );
                insertionPoint = insertionPoint.Append(buildMatch);
            }
            for (int i = 0; i < patternGraph.alternatives.Length; ++i)
            {
                BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Alternative,
                        patternGraph.pathPrefix+patternGraph.name+"_"+patternGraph.alternatives[i].name,
                        patternGraph.alternatives[i].name,
                        patternGraph.alternatives[i].name,
                        rulePatternClassName,
                        enumPrefix,
                        matchObjectName,
                        patternGraph.embeddedGraphs.Length
                    );
                insertionPoint = insertionPoint.Append(buildMatch);
            }
            if (patternGraph.pathPrefixesAndNamesOfNestedIndependents != null)
            {
                foreach (Pair<String,String> pathPrefixAndName in patternGraph.pathPrefixesAndNamesOfNestedIndependents)
                {
                    BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Independent,
                        pathPrefixAndName.snd,
                        pathPrefixAndName.fst + pathPrefixAndName.snd,
                        rulePatternClassName,
                        matchObjectName
                    );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to push the subpattern tasks to the open tasks stack 
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertPushSubpatternTasks(SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            // first alternatives, so that they get processed last
            // to handle subpatterns in linear order we've to push them in reverse order on the stack
            for (int i = patternGraph.alternatives.Length - 1; i >= 0; --i)
            {
                Alternative alternative = patternGraph.alternatives[i];

                Dictionary<string, bool> neededNodes = new Dictionary<string, bool>();
                Dictionary<string, bool> neededEdges = new Dictionary<string, bool>();
                foreach (PatternGraph altCase in alternative.alternativeCases)
                {
                    foreach (KeyValuePair<string, bool> neededNode in altCase.neededNodes)
                        neededNodes[neededNode.Key] = neededNode.Value;
                    foreach (KeyValuePair<string, bool> neededEdge in altCase.neededEdges)
                        neededEdges[neededEdge.Key] = neededEdge.Value;
                }

                int numElements = neededNodes.Count + neededEdges.Count;
                string[] connectionName = new string[numElements];
                string[] patternElementBoundToConnectionName = new string[numElements];
                bool[] patternElementBoundToConnectionIsNode = new bool[numElements];
                int j = 0;
                foreach (KeyValuePair<string, bool> node in neededNodes)
                {
                    connectionName[j] = node.Key;
                    patternElementBoundToConnectionName[j] = node.Key;
                    patternElementBoundToConnectionIsNode[j] = true;
                    ++j;
                }
                foreach (KeyValuePair<string, bool> edge in neededEdges)
                {
                    connectionName[j] = edge.Key;
                    patternElementBoundToConnectionName[j] = edge.Key;
                    patternElementBoundToConnectionIsNode[j] = false;
                    ++j;
                }

                PushSubpatternTask pushTask =
                    new PushSubpatternTask(
                        alternative.pathPrefix,
                        alternative.name,
                        rulePatternClassName,
                        connectionName,
                        patternElementBoundToConnectionName,
                        patternElementBoundToConnectionIsNode,
                        negativeIndependentNamePrefix
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }
            
            // then subpatterns of the pattern
            // to handle subpatterns in linear order we've to push them in reverse order on the stack
            for (int i = patternGraph.embeddedGraphs.Length - 1; i >= 0; --i)
            {
                PatternGraphEmbedding subpattern = patternGraph.embeddedGraphs[i];
                Debug.Assert(subpattern.matchingPatternOfEmbeddedGraph.inputNames.Length == subpattern.connections.Length);
                string[] connectionName = subpattern.matchingPatternOfEmbeddedGraph.inputNames;
                string[] patternElementBoundToConnectionName = new string[subpattern.connections.Length];
                bool[] patternElementBoundToConnectionIsNode = new bool[subpattern.connections.Length];
                for (int j = 0; j < subpattern.connections.Length; ++j)
                {
                    patternElementBoundToConnectionName[j] = subpattern.connections[j].name;
                    patternElementBoundToConnectionIsNode[j] = subpattern.connections[j] is PatternNode;
                }

                PushSubpatternTask pushTask =
                    new PushSubpatternTask(
                        subpattern.matchingPatternOfEmbeddedGraph.name,
                        subpattern.name,
                        connectionName,
                        patternElementBoundToConnectionName,
                        patternElementBoundToConnectionIsNode,
                        negativeIndependentNamePrefix
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to pop the subpattern tasks from the open tasks stack
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertPopSubpatternTasks(SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            foreach (PatternGraphEmbedding subpattern in patternGraph.embeddedGraphs)
            {
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Subpattern,
                        subpattern.matchingPatternOfEmbeddedGraph.name,
                        subpattern.name
                    );
                insertionPoint = insertionPoint.Append(popTask);
            }

            foreach (Alternative alternative in patternGraph.alternatives)
            {
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Alternative,
                        alternative.name,
                        alternative.pathPrefix
                    );
                insertionPoint = insertionPoint.Append(popTask);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to check whether there are open tasks to handle left and code for case there are none
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertCheckForTasksLeft(SearchProgramOperation insertionPoint)
        {
            CheckContinueMatchingTasksLeft tasksLeft =
                new CheckContinueMatchingTasksLeft();
            SearchProgramOperation continuationPointAfterTasksLeft =
                insertionPoint.Append(tasksLeft);
            tasksLeft.CheckFailedOperations =
                new SearchProgramList(tasksLeft);
            insertionPoint = tasksLeft.CheckFailedOperations;

            // ---- check failed, no tasks left, leaf subpattern was matched
            LeafSubpatternMatched leafMatched = new LeafSubpatternMatched(
                rulePatternClassName, patternGraph.pathPrefix+patternGraph.name);
            SearchProgramOperation continuationPointAfterLeafMatched =
                insertionPoint.Append(leafMatched);
            leafMatched.MatchBuildingOperations =
                new SearchProgramList(leafMatched);
            insertionPoint = leafMatched.MatchBuildingOperations;

            // ---- ---- fill the match object with the candidates 
            // ---- ---- which have passed all the checks for being a match
            insertionPoint = insertMatchObjectBuilding(insertionPoint, false);

            // ---- nesting level up
            insertionPoint = continuationPointAfterLeafMatched;

            // ---- check max matches reached will be inserted here by completion pass

            // nesting level up
            insertionPoint = continuationPointAfterTasksLeft;

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to accept the matched elements globally
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertGlobalAccept(SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            bool isAction = programType == SearchProgramType.Action
                    || programType == SearchProgramType.MissingPreset;

            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                if (patternGraph.nodes[i].PointOfDefinition == patternGraph
                    || patternGraph.nodes[i].PointOfDefinition == null && isAction)
                {
                    AcceptCandidateGlobal acceptGlobal =
                        new AcceptCandidateGlobal(patternGraph.nodes[i].name, negativeIndependentNamePrefix, true);
                    insertionPoint = insertionPoint.Append(acceptGlobal);
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                if (patternGraph.edges[i].PointOfDefinition == patternGraph
                    || patternGraph.edges[i].PointOfDefinition == null && isAction)
                {
                    AcceptCandidateGlobal acceptGlobal =
                        new AcceptCandidateGlobal(patternGraph.edges[i].name, negativeIndependentNamePrefix, false);
                    insertionPoint = insertionPoint.Append(acceptGlobal);
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to abandon the matched elements globally
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertGlobalAbandon(SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            bool isAction = programType == SearchProgramType.Action
                    || programType == SearchProgramType.MissingPreset;

            // global abandon of all candidate elements (remove isomorphy information)
            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                if (patternGraph.nodes[i].PointOfDefinition == patternGraph
                    || patternGraph.nodes[i].PointOfDefinition == null && isAction)
                {
                    AbandonCandidateGlobal abandonGlobal =
                        new AbandonCandidateGlobal(patternGraph.nodes[i].name, negativeIndependentNamePrefix, true);
                    insertionPoint = insertionPoint.Append(abandonGlobal);
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                if (patternGraph.edges[i].PointOfDefinition == patternGraph
                    || patternGraph.edges[i].PointOfDefinition == null && isAction)
                {
                    AbandonCandidateGlobal abandonGlobal =
                        new AbandonCandidateGlobal(patternGraph.edges[i].name, negativeIndependentNamePrefix, false);
                    insertionPoint = insertionPoint.Append(abandonGlobal);
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to check whether the subpatterns were found and code for case there were some
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertCheckForSubpatternsFound(SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            // check whether there were no subpattern matches found
            CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                new CheckPartialMatchForSubpatternsFound(negativeIndependentNamePrefix);
            SearchProgramOperation continuationPointAfterSubpatternsFound =
                   insertionPoint.Append(checkSubpatternsFound);
            checkSubpatternsFound.CheckFailedOperations =
                new SearchProgramList(checkSubpatternsFound);
            insertionPoint = checkSubpatternsFound.CheckFailedOperations;

            // ---- check failed, some subpattern matches found, pattern and subpatterns were matched
            PatternAndSubpatternsMatched patternAndSubpatternsMatched;
            if (programType == SearchProgramType.Action || programType == SearchProgramType.MissingPreset)
            {
                patternAndSubpatternsMatched = new PatternAndSubpatternsMatched(
                    rulePatternClassName, patternGraph.pathPrefix+patternGraph.name, false);
            }
            else
            {
                patternAndSubpatternsMatched = new PatternAndSubpatternsMatched(
                    rulePatternClassName, patternGraph.pathPrefix+patternGraph.name, true);
            }
            SearchProgramOperation continuationPointAfterPatternAndSubpatternsMatched =
                insertionPoint.Append(patternAndSubpatternsMatched);
            patternAndSubpatternsMatched.MatchBuildingOperations =
                new SearchProgramList(patternAndSubpatternsMatched);
            insertionPoint = patternAndSubpatternsMatched.MatchBuildingOperations;

            // ---- ---- fill the match object with the candidates 
            // ---- ---- which have passed all the checks for being a match
            insertionPoint = insertMatchObjectBuilding(insertionPoint, false);

            // ---- nesting level up
            insertionPoint = continuationPointAfterPatternAndSubpatternsMatched;

            // ---- create new matches list to search on or copy found matches into own matches list
            if (programType == SearchProgramType.Subpattern || programType == SearchProgramType.AlternativeCase)
            {
                NewMatchesListForFollowingMatches newMatchesList =
                    new NewMatchesListForFollowingMatches(false);
                insertionPoint = insertionPoint.Append(newMatchesList);
            }

            // ---- check max matches reached will be inserted here by completion pass

            // nesting level up
            insertionPoint = continuationPointAfterSubpatternsFound;

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to check whether the subpatterns were found and code for case there were some
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertCheckForSubpatternsFoundNegativeIndependent(SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            // check whether there were no subpattern matches found
            CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                new CheckPartialMatchForSubpatternsFound(negativeIndependentNamePrefix);
            SearchProgramOperation continuationPointAfterSubpatternsFound =
                   insertionPoint.Append(checkSubpatternsFound);
            checkSubpatternsFound.CheckFailedOperations =
                new SearchProgramList(checkSubpatternsFound);
            insertionPoint = checkSubpatternsFound.CheckFailedOperations;

            if (isNegative)
            {
                // ---- check failed, some subpattern matches found, negative pattern and subpatterns were matched
                // build the negative pattern was matched operation
                NegativePatternMatched patternMatched = new NegativePatternMatched(
                    NegativeIndependentPatternMatchedType.ContainingSubpatterns, negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(patternMatched);

                // ---- abort the matching process
                CheckContinueMatchingOfNegativeFailed abortMatching =
                    new CheckContinueMatchingOfNegativeFailed();
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

                if (!isNestedInNegative) // no match object needed(/available) if independent is part of negative
                {
                    // ---- fill the match object with the candidates 
                    // ---- which have passed all the checks for being a match
                    insertionPoint = insertMatchObjectBuilding(insertionPoint, true);
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
        /// Inserts code to handle case pattern was found
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertPatternFound(SearchProgramOperation insertionPoint)
        {
            // build the pattern was matched operation
            PositivePatternWithoutSubpatternsMatched patternMatched =
                new PositivePatternWithoutSubpatternsMatched(rulePatternClassName, patternGraph.name);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(patternMatched);
            patternMatched.MatchBuildingOperations =
                new SearchProgramList(patternMatched);
            insertionPoint = patternMatched.MatchBuildingOperations;

            // ---- fill the match object with the candidates 
            // ---- which have passed all the checks for being a match
            insertionPoint = insertMatchObjectBuilding(insertionPoint, false);

            // ---- nesting level up
            insertionPoint = continuationPoint;

            // check whether to continue the matching process
            // or abort because the maximum desired number of maches was reached
            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
#if NO_ADJUST_LIST_HEADS
                new CheckContinueMatchingMaximumMatchesReached(false, false);
#else
                new CheckContinueMatchingMaximumMatchesReached(false, true);
#endif
            insertionPoint = insertionPoint.Append(checkMaximumMatches);

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to handle case negative/independent pattern was found
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertPatternFoundNegativeIndependent(SearchProgramOperation insertionPoint)
        {
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix();

            if (isNegative)
            {
                // build the negative pattern was matched operation
                NegativePatternMatched patternMatched = new NegativePatternMatched(
                    NegativeIndependentPatternMatchedType.WithoutSubpatterns, negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(patternMatched);

                // abort the matching process
                CheckContinueMatchingOfNegativeFailed abortMatching = 
                    new CheckContinueMatchingOfNegativeFailed();
                insertionPoint = insertionPoint.Append(abortMatching);
            }
            else
            {
                // build the independent pattern was matched operation
                IndependentPatternMatched patternMatched = new IndependentPatternMatched(
                    NegativeIndependentPatternMatchedType.WithoutSubpatterns,
                    negativeIndependentNamePrefix);
                insertionPoint = insertionPoint.Append(patternMatched);

                if (!isNestedInNegative) // no match object needed(/available) if independent is part of negative
                {
                    // fill the match object with the candidates 
                    // which have passed all the checks for being a match
                    insertionPoint = insertMatchObjectBuilding(insertionPoint, true);
                }

                // continue the matching process outside
                CheckContinueMatchingOfIndependentSucceeded continueMatching =
                    new CheckContinueMatchingOfIndependentSucceeded();
                insertionPoint = insertionPoint.Append(continueMatching);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which get type operation to use and inserts it
        /// returns new insertion point and continuation point
        ///  for continuing buildup after the stuff nested within type iteration was built
        /// if type drawing was sufficient, insertion point == continuation point
        /// </summary>
        private SearchProgramOperation decideOnAndInsertGetType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target,
            out SearchProgramOperation continuationPoint)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? (ITypeModel)model.NodeModel : (ITypeModel)model.EdgeModel;

            if (target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed
                if (typeModel.Types[target.PatternElement.TypeID].HasSubTypes)
                { // more than the type itself -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.AllCompatible,
                            target.PatternElement.Name,
                            typeModel.TypeTypes[target.PatternElement.TypeID].Name,
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
                if (target.PatternElement.AllowedTypes.Length != 1)
                { // more than one allowed type -> we've to iterate them
                    GetTypeByIteration typeIteration =
                        new GetTypeByIteration(
                            GetTypeByIterationType.ExplicitelyGiven,
                            target.PatternElement.Name,
                            rulePatternClassName,
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
        private SearchProgramOperation decideOnAndInsertCheckType(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            ITypeModel typeModel = isNode ? (ITypeModel)model.NodeModel : (ITypeModel)model.EdgeModel;

            if (target.PatternElement.IsAllowedType != null)
            { // the allowed types are given by an array for checking against them
                Debug.Assert(target.PatternElement.AllowedTypes != null);

                if (target.PatternElement.AllowedTypes.Length <= MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID)
                {
                    string[] typeIDs = new string[target.PatternElement.AllowedTypes.Length];
                    for (int i = 0; i < target.PatternElement.AllowedTypes.Length; ++i)
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
                    CheckCandidateForType checkType =
                        new CheckCandidateForType(
                            CheckCandidateForTypeType.ByIsAllowedType,
                            target.PatternElement.Name,
                            rulePatternClassName,
                            isNode);
                    insertionPoint = insertionPoint.Append(checkType);
                }

                return insertionPoint;
            }

            if (target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed
                GrGenType targetType = typeModel.Types[target.PatternElement.TypeID];
                if (targetType == typeModel.RootType)
                { // every type matches the root type == element type -> no check needed
                    return insertionPoint;
                }

                if (targetType.subOrSameGrGenTypes.Length <= MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID)
                { // the target type has no sub types, it must be exactly this type
                    string[] typeIDs = new string[targetType.subOrSameGrGenTypes.Length];
                    for (int i = 0; i < targetType.subOrSameGrGenTypes.Length; ++i)
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
                            typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                            isNode);
                    insertionPoint = insertionPoint.Append(checkType);
                }

                return insertionPoint;
            }

            // the allowed types are given by allowed types array
            // if there are multiple ones, is allowed types must have been not null before
            Debug.Assert(target.PatternElement.AllowedTypes.Length > 1, "More than one allowed type");

            if (target.PatternElement.AllowedTypes.Length == 0)
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
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfNodeFromLookup(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;
            SearchProgramOperation localContinuationPoint;

            // check for edges required by the pattern to be incident to the given node
            foreach (SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                        insertionPoint, node, edge, CheckCandidateForConnectednessType.Source);
                }
                else
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                        insertionPoint, node, edge, out localContinuationPoint);
                    if (localContinuationPoint != insertionPoint && continuationPoint == null)
                        continuationPoint = localContinuationPoint;
                }
            }
            foreach (SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                        insertionPoint, node, edge, CheckCandidateForConnectednessType.Target);
                }
                else
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                        insertionPoint, node, edge, out localContinuationPoint);
                    if (localContinuationPoint != insertionPoint && continuationPoint == null)
                        continuationPoint = localContinuationPoint;
                }
            }

            if (continuationPoint == null)
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
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfImplicitNodeFromEdge(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            SearchPlanEdgeNode originatingEdge,
            SearchPlanNodeNode otherNodeOfOriginatingEdge,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;
            SearchProgramOperation localContinuationPoint;

            // check for edges required by the pattern to be incident to the given node
            // only if the node was not taken from the given originating edge
            //   with the exception of reflexive edges, as these won't get checked thereafter
            foreach (SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                            insertionPoint, node, edge, CheckCandidateForConnectednessType.Source);
                    }
                }
                else
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                            insertionPoint, node, edge, out localContinuationPoint);
                        if (localContinuationPoint != insertionPoint && continuationPoint == null)
                            continuationPoint = localContinuationPoint;
                    }
                }
            }
            foreach (SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
                            insertionPoint, node, edge, CheckCandidateForConnectednessType.Target);
                    }
                }
                else
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(
                            insertionPoint, node, edge, out localContinuationPoint);
                        if (localContinuationPoint != insertionPoint && continuationPoint == null)
                            continuationPoint = localContinuationPoint;
                    }
                }
            }

            if (continuationPoint == null)
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
            CheckCandidateForConnectednessType connectednessType)
        {
            Debug.Assert(connectednessType == CheckCandidateForConnectednessType.Source || connectednessType == CheckCandidateForConnectednessType.Target);

            // check whether the pattern edges which must be incident to the candidate node (according to the pattern)
            // are really incident to it
            // only if edge is already matched by now (signaled by visited)
            if (edge.Visited)
            {
                CheckCandidateForConnectedness checkConnectedness =
                    new CheckCandidateForConnectedness(
                        currentNode.PatternElement.Name,
                        currentNode.PatternElement.Name,
                        edge.PatternElement.Name,
                        connectednessType);
                insertionPoint = insertionPoint.Append(checkConnectedness);
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
            out SearchProgramOperation continuationPoint)
        {
            Debug.Assert(edge.PatternEdgeSource != edge.PatternEdgeTarget);

            continuationPoint = null;

            // check whether the pattern edges which must be incident to the candidate node (according to the pattern)
            // are really incident to it
            if (currentNodeIsFirstIncidentNodeOfEdge(currentNode, edge))
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
            }
            if (currentNodeIsSecondIncidentNodeOfEdge(currentNode, edge))
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
            }

            if (continuationPoint == null)
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
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdgeFromLookup(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            if (((PatternEdge)edge.PatternElement).fixedDirection)
            {
                // don't need to check if the edge is not required by the pattern to be incident to some given node
                if (edge.PatternEdgeSource != null)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                        insertionPoint, edge, CheckCandidateForConnectednessType.Source);
                }
                if (edge.PatternEdgeTarget != null)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                        insertionPoint, edge, CheckCandidateForConnectednessType.Target);
                }
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeBothDirections(
                    insertionPoint, edge, false, out continuationPoint);
            }

            if (continuationPoint == null)
                continuationPoint = insertionPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge determined from incident node
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode originatingNode,
            bool edgeIncomingAtOriginatingNode,
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            if (((PatternEdge)edge.PatternElement).fixedDirection)
            {
                // don't need to check if the edge is not required by the pattern to be incident to some given node
                // or if the edge was taken from the given originating node
                if (edge.PatternEdgeSource != null)
                {
                    if (!(!edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeSource == originatingNode))
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                            insertionPoint, edge, CheckCandidateForConnectednessType.Source);
                    }
                }
                if (edge.PatternEdgeTarget != null)
                {
                    if (!(edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeTarget == originatingNode))
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
                            insertionPoint, edge, CheckCandidateForConnectednessType.Target);
                    }
                }
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeBothDirections(
                    insertionPoint, edge, true, out continuationPoint);
            }

            if (continuationPoint == null)
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
            CheckCandidateForConnectednessType connectednessType)
        {
            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            // don't need to, if that node is not matched by now (signaled by visited)
            SearchPlanNodeNode nodeRequiringCheck = 
                connectednessType == CheckCandidateForConnectednessType.Source ?
                    edge.PatternEdgeSource : edge.PatternEdgeTarget;
            if (nodeRequiringCheck.Visited)
            {
                CheckCandidateForConnectedness checkConnectedness =
                    new CheckCandidateForConnectedness(
                        edge.PatternElement.Name,
                        nodeRequiringCheck.PatternElement.Name,
                        edge.PatternElement.Name,
                        connectednessType);
                insertionPoint = insertionPoint.Append(checkConnectedness);
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
            out SearchProgramOperation continuationPoint)
        {
            continuationPoint = null;

            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            if (!edgeDeterminationContainsFirstNodeLoop && currentEdgeConnectsToFirstIncidentNode(edge))
            {
                // due to currentEdgeConnectsToFirstIncidentNode: at least on incident node available
                if (edge.PatternEdgeSource == edge.PatternEdgeTarget)
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
                }
            }
            if (currentEdgeConnectsToSecondIncidentNode(edge))
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
            }

            return insertionPoint;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Completes calls to missing preset handling methods in all search programs
        ///   (available at the time the functions is called)
        /// </summary>
        private void CompleteCallsToMissingPresetHandlingMethodInAllSearchPrograms(
            SearchProgram searchProgram,
            string nameOfMissingPresetHandlingMethod,
            List<string> arguments,
            List<bool> argumentIsNode)
        {
            // Iterate all search programs 
            // search for calls to missing preset handling methods 
            // within check preset opertions which are the only ones utilizing them
            // and complete them with the arguments given
            do
            {
                CompleteCallsToMissingPresetHandlingMethod(
                    searchProgram.GetNestedSearchOperationsList(),
                    nameOfMissingPresetHandlingMethod,
                    arguments,
                    argumentIsNode);
                searchProgram = searchProgram.Next as SearchProgram;
            }
            while (searchProgram != null);
        }

        /// <summary>
        /// Completes calls to missing preset handling methods 
        /// by inserting the initially not given arguments
        /// </summary>
        private void CompleteCallsToMissingPresetHandlingMethod(
            SearchProgramOperation searchProgramOperation,
            string nameOfMissingPresetHandlingMethod,
            List<string> arguments,
            List<bool> argumentIsNode)
        {
            // complete calls with arguments 
            // find them in depth first search of search program
            while (searchProgramOperation != null)
            {
                if (searchProgramOperation is CheckCandidateForPreset)
                {
                    CheckCandidateForPreset checkPreset =
                        (CheckCandidateForPreset)searchProgramOperation;
                    if (NamesOfEntities.MissingPresetHandlingMethod(checkPreset.PatternElementName)
                        == nameOfMissingPresetHandlingMethod)
                    {
                        checkPreset.CompleteWithArguments(arguments, argumentIsNode);
                    }
                }
                else if (searchProgramOperation.IsSearchNestingOperation())
                { // depth first
                    CompleteCallsToMissingPresetHandlingMethod(
                        searchProgramOperation.GetNestedSearchOperationsList(),
                        nameOfMissingPresetHandlingMethod,
                        arguments,
                        argumentIsNode);
                }

                // breadth
                searchProgramOperation = searchProgramOperation.Next;
            }
        }

        /// <summary>
        /// returns name prefix for candidate variables computed from current negative/independent pattern nesting
        /// </summary>
        private string NegativeIndependentNamePrefix()
        {
            string negativeIndependentNamePrefix = "";
            foreach (string negativeIndependentName in negativeIndependentNames)
            {
                negativeIndependentNamePrefix += negativeIndependentName;
            }

            if (negativeIndependentNamePrefix != "") {
                negativeIndependentNamePrefix += "_";
            }

            return negativeIndependentNamePrefix;
        }

        /// <summary>
        /// computes maximum neg level of the given positive pattern graph 
        /// if it can be easily determined statically
        /// </summary>
        private int computeMaxNegLevel(PatternGraph patternGraph)
        {
            int maxNegLevel = 0;

            if(patternGraph.alternatives.Length > 0) return (int) LGSPElemFlags.MAX_NEG_LEVEL + 1;
            if(patternGraph.embeddedGraphs.Length > 0) return (int) LGSPElemFlags.MAX_NEG_LEVEL + 1;

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                int level = computeMaxNegLevelNegative(neg);
                if(level > maxNegLevel)
                {
                    maxNegLevel = level;
                }
            }

            return maxNegLevel;
        }

        /// <summary>
        /// computes maximum neg level of the given negative pattern graph 
        /// if it can be easily determined statically
        /// </summary>
        private int computeMaxNegLevelNegative(PatternGraph patternGraph)
        {
            int maxNegLevel = 1;

            if(patternGraph.alternatives.Length > 0) return (int) LGSPElemFlags.MAX_NEG_LEVEL + 1;
            if(patternGraph.embeddedGraphs.Length > 0) return (int) LGSPElemFlags.MAX_NEG_LEVEL + 1;

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                int level = 1 + computeMaxNegLevel(neg);
                if (level > maxNegLevel)
                {
                    maxNegLevel = level;
                }
            }

            return maxNegLevel;
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

            if (!edge.Visited) return false;

            if (currentNode == edge.PatternEdgeSource)
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

            if (!edge.Visited) return false;

            if (edge.PatternEdgeSource == null || edge.PatternEdgeTarget == null)
                return false;

            if (currentNode == edge.PatternEdgeSource)
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

            if (currentEdge.PatternEdgeSource != null && currentEdge.PatternEdgeSource.Visited
                && (currentEdge.PatternEdgeTarget == null || !currentEdge.PatternEdgeTarget.Visited))
            {
                return true;
            }
            if (currentEdge.PatternEdgeTarget != null && currentEdge.PatternEdgeTarget.Visited
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

            if (currentEdge.PatternEdgeSource != null && currentEdge.PatternEdgeSource.Visited)
                return true;
            if (currentEdge.PatternEdgeTarget != null && currentEdge.PatternEdgeTarget.Visited)
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

            if (currentEdge.PatternEdgeSource == null || currentEdge.PatternEdgeTarget == null)
                return false;

            if (currentEdge.PatternEdgeSource.Visited && currentEdge.PatternEdgeTarget.Visited)
                return true;

            return false;
        }
    }
}

