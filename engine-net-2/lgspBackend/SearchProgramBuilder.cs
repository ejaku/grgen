using System.Collections.Generic;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// class for building search program data structure from scheduled search plan
    /// holds environment variables for this build process
    /// </summary>
    class SearchProgramBuilder
    {
        /// <summary>
        /// Builds search program from scheduled search plan
        /// </summary>
        public SearchProgram BuildSearchProgram(
            ScheduledSearchPlan scheduledSearchPlan_,
            string nameOfSearchProgram,
            List<string> parametersList,
            List<bool> parameterIsNodeList,
            LGSPRulePattern rulePattern_,
            IGraphModel model_)
        {
            string[] parameters = null;
            bool[] parameterIsNode = null;
            if (parametersList != null)
            {
                parameters = new string[parametersList.Count];
                parameterIsNode = new bool[parameterIsNodeList.Count];
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
            }

            SearchProgram searchProgram = new SearchProgram(
                nameOfSearchProgram, parameters, parameterIsNode,
                rulePattern_.patternGraph.embeddedGraphs.Length>0 && !rulePattern_.isSubpattern,
                rulePattern_.isSubpattern);
            searchProgram.OperationsList = new SearchProgramList(searchProgram);

            scheduledSearchPlan = scheduledSearchPlan_;
            nameOfRulePatternType = rulePattern_.GetType().Name;
            enclosingPositiveOperation = null;
            rulePattern = rulePattern_;
            model = model_;

            // start building with first operation in scheduled search plan
            BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                searchProgram.OperationsList);

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
                    case SearchOperationType.ImplicitSource:
                    case SearchOperationType.ImplicitTarget:
                        // don't search
                        op.Type = SearchOperationType.Void;
                        // make parameter in remainder program
                        SearchPlanNode element = (SearchPlanNode)op.Element;
                        neededElementsInRemainderProgram.Add(element.PatternElement.Name);
                        neededElementInRemainderProgramIsNode.Add(element.NodeType == PlanNodeType.Node);
                        break;
                    // check operations were already executed in the calling program 
                    // they don't need any treatement in the remainder search program
                    case SearchOperationType.NegativePattern:
                    case SearchOperationType.Condition:
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
                                scheduledSearchPlan,
                                NamesOfEntities.MissingPresetHandlingMethod(element.PatternElement.Name),
                                neededElementsInRemainderProgram,
                                neededElementInRemainderProgramIsNode,
                                rulePattern,
                                model);
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
                    case SearchOperationType.NegPreset:
                    case SearchOperationType.PatPreset:
                    default:
                        Debug.Assert(false, "At this pass/position not allowed search operation");
                        break;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the scheduled search plan to build
        /// </summary>
        private ScheduledSearchPlan scheduledSearchPlan;

        /// <summary>
        /// name of the rule pattern type of the rule pattern that is built
        /// </summary>
        private string nameOfRulePatternType;

        /// <summary>
        /// the innermost enclosing positive candidate iteration operation 
        /// at the current insertion point of the nested negative pattern
        /// not null if negative pattern is currently built, null otherwise
        /// </summary>
        private SearchProgramOperation enclosingPositiveOperation;

        /// <summary>
        /// the rule pattern that is built 
        /// (it's pattern graph is needed for match object building)
        /// </summary>
        private LGSPRulePattern rulePattern;

        /// <summary>
        /// The model for which the matcher functions shall be generated.
        /// </summary>
        private IGraphModel model;

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
                scheduledSearchPlan.Operations.Length)
            { // end of scheduled search plan reached, stop recursive iteration
                return buildMatchComplete(insertionPointWithinSearchProgram);
            }

            SearchOperation op = scheduledSearchPlan.
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

                case SearchOperationType.NegPreset:
                    return buildNegPreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.PatPreset:
                    return buildPatPreset(insertionPointWithinSearchProgram,
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
                        true);

                case SearchOperationType.ImplicitTarget:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        false);

                case SearchOperationType.Incoming:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        true);

                case SearchOperationType.Outgoing:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        false);

                case SearchOperationType.NegativePattern:
                    return buildNegative(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (ScheduledSearchPlan)op.Element);

                case SearchOperationType.Condition:
                    return buildCondition(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (Condition)op.Element);

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
            bool positive = enclosingPositiveOperation == null;
            Debug.Assert(positive, "Positive maybe preset in negative search plan");

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
        /// NegPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = enclosingPositiveOperation == null;
            Debug.Assert(!positive, "Negative preset in positive search plan");

            // check candidate for isomorphy 
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
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

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// PatPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPatPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool positive = enclosingPositiveOperation == null;
            Debug.Assert(positive, "Positive subpattern preset in negative search plan");

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
            bool positive = enclosingPositiveOperation == null;

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
            if (isNode)
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNode(
                    insertionPoint, (SearchPlanNodeNode)target, null, null);
            }
            else
            {
                bool dontcare = true || false;
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdge(
                    insertionPoint, (SearchPlanEdgeNode)target, null, dontcare);
            }

            // check candidate for isomorphy 
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
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

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        isNode);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
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
        /// Implicit source|target search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildImplicit(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanEdgeNode source,
            SearchPlanNodeNode target,
            IsomorphyInformation isomorphy,
            bool getSource)
        {
            // get candidate = demanded node from edge
            GetCandidateByDrawing nodeFromEdge =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.NodeFromEdge,
                    target.PatternElement.Name,
                    model.NodeModel.Types[target.PatternElement.TypeID].Name,
                    source.PatternElement.Name,
                    getSource);
            insertionPoint = insertionPoint.Append(nodeFromEdge);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfNode(
                insertionPoint, target, source,
                getSource ? source.PatternEdgeTarget : source.PatternEdgeSource);

            // check candidate for isomorphy 
            bool positive = enclosingPositiveOperation == null;
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(), 
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
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

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        true);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Extend Incoming|Outgoing search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildIncident(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNodeNode source,
            SearchPlanEdgeNode target,
            IsomorphyInformation isomorphy,
            bool getIncoming)
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
            GetCandidateByIteration incidentIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.IncidentEdges,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    getIncoming);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(incidentIteration);
            incidentIteration.NestedOperationsList =
                new SearchProgramList(incidentIteration);
            insertionPoint = incidentIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfEdge(
                insertionPoint, target, source, getIncoming);

            // check candidate for isomorphy 
            bool positive = enclosingPositiveOperation == null;
            if (isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate and write candidate isomorphy (until withdrawn)
            if (isomorphy.SetIsMatchedBit)
            {
                AcceptIntoPartialMatchWriteIsomorphy writeIsomorphy =
                    new AcceptIntoPartialMatchWriteIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(writeIsomorphy);
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

            // withdraw candidate and remove candidate isomorphy
            if (isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                WithdrawFromPartialMatchRemoveIsomorphy removeIsomorphy =
                    new WithdrawFromPartialMatchRemoveIsomorphy(
                        target.PatternElement.Name,
                        positive,
                        false);
                insertionPoint = insertionPoint.Append(removeIsomorphy);
            }

            // everything nested within incident iteration built by now -
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Negative search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildNegative(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            ScheduledSearchPlan negativeScheduledSearchPlan)
        {
            bool positive = enclosingPositiveOperation == null;
            Debug.Assert(positive, "Nested negative");

            // fill needed elements array for CheckPartialMatchByNegative
            int numberOfNeededElements = 0;
            foreach (SearchOperation op in negativeScheduledSearchPlan.Operations)
            {
                if (op.Type == SearchOperationType.NegPreset)
                {
                    ++numberOfNeededElements;
                }
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach (SearchOperation op in negativeScheduledSearchPlan.Operations)
            {
                if (op.Type == SearchOperationType.NegPreset)
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

            enclosingPositiveOperation = checkNegative.GetEnclosingOperation();
            ScheduledSearchPlan positiveScheduledSearchPlan = scheduledSearchPlan;
            scheduledSearchPlan = negativeScheduledSearchPlan;

            //---------------------------------------------------------------------------
            // build negative pattern
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);
            //---------------------------------------------------------------------------

            // negative pattern built by now
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;
            enclosingPositiveOperation = null;
            scheduledSearchPlan = positiveScheduledSearchPlan;

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
            Condition condition)
        {
            // check condition with current partial match
            CheckPartialMatchByCondition checkCondition =
                new CheckPartialMatchByCondition(condition.ID.ToString(),
                    nameOfRulePatternType,
                    condition.NeededNodes,
                    condition.NeededEdges);
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
        /// after all pattern elements have been found 
        /// are created and inserted into the program
        /// </summary>
        private SearchProgramOperation buildMatchComplete(
            SearchProgramOperation insertionPoint)
        {
            bool positive = enclosingPositiveOperation == null;

            if (positive)
            {
                // build the match complete operation signaling pattern was found
                PartialMatchCompletePositive matchComplete =
                    new PartialMatchCompletePositive();
                SearchProgramOperation continuationPoint =
                    insertionPoint.Append(matchComplete);
                matchComplete.MatchBuildingOperations =
                    new SearchProgramList(matchComplete);
                insertionPoint = matchComplete.MatchBuildingOperations;

                // fill the match object with the candidates 
                // which have passed all the checks for being a match
                PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;
                for (int i = 0; i < patternGraph.nodes.Length; i++)
                {
                    PartialMatchCompleteBuildMatchObject buildMatch =
                        new PartialMatchCompleteBuildMatchObject(
                            patternGraph.nodes[i].Name,
                            i.ToString(),
                            true);
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
                for (int i = 0; i < patternGraph.edges.Length; i++)
                {
                    PartialMatchCompleteBuildMatchObject buildMatch =
                        new PartialMatchCompleteBuildMatchObject(
                            patternGraph.edges[i].Name,
                            i.ToString(),
                            false);
                    insertionPoint = insertionPoint.Append(buildMatch);
                }

                // check wheter to continue the matching process
                // or abort because the maximum desired number of maches was reached
                CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
#if NO_ADJUST_LIST_HEADS
                    new CheckContinueMatchingMaximumMatchesReached(false);
#else
                    new CheckContinueMatchingMaximumMatchesReached(true);
#endif
                insertionPoint = continuationPoint.Append(checkMaximumMatches);

                return insertionPoint;
            }
            else
            {
                // build the match complete operation signaling negative pattern was found
                PartialMatchCompleteNegative matchComplete =
                    new PartialMatchCompleteNegative();
                insertionPoint = insertionPoint.Append(matchComplete);

                // abort the matching process
                CheckContinueMatchingFailed abortMatching =
                    new CheckContinueMatchingFailed();
                insertionPoint = insertionPoint.Append(abortMatching);

                return insertionPoint;
            }
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
                            nameOfRulePatternType,
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
                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByIsAllowedType,
                        target.PatternElement.Name,
                        nameOfRulePatternType,
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);

                return insertionPoint;
            }

            if (target.PatternElement.AllowedTypes == null)
            { // the pattern element type and all subtypes are allowed

                if (typeModel.Types[target.PatternElement.TypeID] == typeModel.RootType)
                { // every type matches the root type == element type -> no check needed
                    return insertionPoint;
                }

                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByIsMyType,
                        target.PatternElement.Name,
                        typeModel.TypeTypes[target.PatternElement.TypeID].Name,
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);

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
                CheckCandidateForType checkType =
                    new CheckCandidateForType(
                        CheckCandidateForTypeType.ByAllowedTypes,
                        target.PatternElement.Name,
                        target.PatternElement.AllowedTypes[0].TypeID.ToString(),
                        isNode);
                insertionPoint = insertionPoint.Append(checkType);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfNode(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode node,
            SearchPlanEdgeNode originatingEdge,
            SearchPlanNodeNode otherNodeOfOriginatingEdge)
        {
            // check whether incoming/outgoing-edges of the candidate node 
            // are the same as the already found edges to which the node must be incident
            // only for the edges required by the pattern to be incident with the node
            // only if edge is already matched by now (signaled by visited)
            // only if the node was not taken from the given originating edge
            //   with the exception of reflexive edges, as these won't get checked thereafter
            foreach (SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if (edge.Visited)
                {
                    if (edge != originatingEdge || node==otherNodeOfOriginatingEdge)
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                node.PatternElement.Name,
                                node.PatternElement.Name,
                                edge.PatternElement.Name,
                                true);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }
            foreach (SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if (edge.Visited)
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                node.PatternElement.Name,
                                node.PatternElement.Name,
                                edge.PatternElement.Name,
                                false);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge
        /// and inserts them into the search program
        /// </summary>
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdge(
            SearchProgramOperation insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode originatingNode,
            bool edgeIncomingAtOriginatingNode)
        {
            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            // don't need to, if the edge is not required by the pattern 
            //  to be incident to some given node
            // or that node is not matched by now (signaled by visited)
            // or if the edge was taken from the given originating node
            if (edge.PatternEdgeSource != null)
            {
                if (edge.PatternEdgeSource.Visited)
                {
                    if (!(!edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeSource == originatingNode))
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                edge.PatternElement.Name,
                                edge.PatternEdgeSource.PatternElement.Name,
                                edge.PatternElement.Name,
                                true);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
            }

            if (edge.PatternEdgeTarget != null)
            {
                if (edge.PatternEdgeTarget.Visited)
                {
                    if (!(edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeTarget == originatingNode))
                    {
                        CheckCandidateForConnectedness checkConnectedness =
                            new CheckCandidateForConnectedness(
                                edge.PatternElement.Name,
                                edge.PatternEdgeTarget.PatternElement.Name,
                                edge.PatternElement.Name,
                                false);
                        insertionPoint = insertionPoint.Append(checkConnectedness);
                    }
                }
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
            /// Iterate all search programs 
            /// search for calls to missing preset handling methods 
            /// within check preset opertions which are the only ones utilizing them
            /// and complete them with the arguments given
            do
            {
                CompleteCallsToMissingPresetHandlingMethod(
                    searchProgram.GetNestedOperationsList(),
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
                else if (searchProgramOperation.IsNestingOperation())
                { // depth first
                    CompleteCallsToMissingPresetHandlingMethod(
                        searchProgramOperation.GetNestedOperationsList(),
                        nameOfMissingPresetHandlingMethod,
                        arguments,
                        argumentIsNode);
                }

                // breadth
                searchProgramOperation = searchProgramOperation.Next;
            }
        }
    }
}

