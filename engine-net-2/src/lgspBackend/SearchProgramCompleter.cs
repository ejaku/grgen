/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class completing search programs
    /// </summary>
    class SearchProgramCompleter
    {
        /// <summary>
        /// Iterate all search programs to complete check operations within each one
        /// </summary>
        public void CompleteCheckOperationsInAllSearchPrograms(
            SearchProgram searchProgram)
        {
            do
            {
                CompleteCheckOperations(
                    searchProgram.GetNestedSearchOperationsList(),
                    searchProgram,
                    null,
                    null);
                searchProgram = searchProgram.Next as SearchProgram;
            }
            while (searchProgram != null);
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Completes check operations in search program from given currentOperation on
        /// (taking borderlines set by enclosing search program and check negative into account)
        /// Completion:
        /// - determine continuation point
        /// - insert remove isomorphy opertions needed for continuing there
        /// - insert continuing operation itself
        /// </summary>
        private void CompleteCheckOperations(
            SearchProgramOperation currentOperation,
            SearchProgramOperation enclosingSearchProgram, // might be a negative/independent in case these are nested
            GetPartialMatchOfAlternative enclosingAlternative,
            CheckPartialMatchByNegativeOrIndependent enclosingCheckNegativeOrIndependent)
        {
            // mainly dispatching and iteration method, traverses search program
            // real completion done in MoveOutwardsAppendingRemoveIsomorphyAndJump
            // outermost operation for that function is computed here, regarding negative patterns
            // program nesting structure: search program - [alternative] - [negative|independent]*

            // complete check operations by inserting failure code
            // find them in depth first search of search program
            while (currentOperation != null)
            {
                //////////////////////////////////////////////////////////
                if (currentOperation is CheckCandidate)
                //////////////////////////////////////////////////////////
                {
                    CheckCandidate checkCandidate =
                        (CheckCandidate)currentOperation;
                    checkCandidate.CheckFailedOperations =
                        new SearchProgramList(checkCandidate);
                    string[] neededElementsForCheckOperation = new string[1];
                    neededElementsForCheckOperation[0] = checkCandidate.PatternElementName;
                    MoveOutwardsAppendingRemoveIsomorphyAndJump(
                        checkCandidate,
                        neededElementsForCheckOperation,
                        enclosingCheckNegativeOrIndependent ?? (enclosingAlternative ?? enclosingSearchProgram));
                }
                //////////////////////////////////////////////////////////
                else if (currentOperation is CheckPartialMatch)
                //////////////////////////////////////////////////////////
                {
                    if (currentOperation is CheckPartialMatchByCondition)
                    {
                        CheckPartialMatchByCondition checkCondition =
                            (CheckPartialMatchByCondition)currentOperation;
                        checkCondition.CheckFailedOperations =
                            new SearchProgramList(checkCondition);
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkCondition,
                            checkCondition.NeededElements,
                            enclosingCheckNegativeOrIndependent ?? (enclosingAlternative ?? enclosingSearchProgram));
                    }
                    else if (currentOperation is CheckPartialMatchByNegativeOrIndependent)
                    {
                        CheckPartialMatchByNegativeOrIndependent checkNegativeOrIndependent =
                            (CheckPartialMatchByNegativeOrIndependent)currentOperation;

                        // ByNegative/ByIndependent is handled in CheckContinueMatchingFailed
                        // of the negative/independent case - enter negative/independent case
                        CompleteCheckOperations(
                            checkNegativeOrIndependent.NestedOperationsList,
                            enclosingCheckNegativeOrIndependent ?? enclosingSearchProgram,
                            enclosingCheckNegativeOrIndependent!=null ? null : enclosingAlternative,
                            checkNegativeOrIndependent);
                    }
                    else if (currentOperation is CheckPartialMatchForSubpatternsFound)
                    {
                        CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                            (CheckPartialMatchForSubpatternsFound)currentOperation;

                        if (enclosingCheckNegativeOrIndependent == null)
                        {
                            // determine insertion point within check failed operations
                            // to append the nested check maximum matches
                            SearchProgramOperation insertionPoint =
                                checkSubpatternsFound.CheckFailedOperations;
                            while (insertionPoint.Next != null)
                            {
                                insertionPoint = insertionPoint.Next;
                            }

                            // append nested check maximum matches
                            CheckMaximumMatchesType checkMaxMatchesType = CheckMaximumMatchesType.Action;
                            if(enclosingSearchProgram is SearchProgramOfSubpattern
                                || enclosingSearchProgram is SearchProgramOfAlternative) {
                                checkMaxMatchesType = CheckMaximumMatchesType.Subpattern;
                            } else if(enclosingSearchProgram is SearchProgramOfIterated) {
                                checkMaxMatchesType = CheckMaximumMatchesType.Iterated;
                            }
                            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                                new CheckContinueMatchingMaximumMatchesReached(checkMaxMatchesType, false);
                            insertionPoint.Append(checkMaximumMatches);

                            MoveOutwardsAppendingRemoveIsomorphyAndJump(
                                checkSubpatternsFound,
                                null,
                                enclosingCheckNegativeOrIndependent ?? (enclosingAlternative ?? enclosingSearchProgram));
                        }

                        // check subpatterns found has a further check maximum matches
                        // or check continue matching of negative failed nested within check failed code
                        // give it its special bit of attention here
                        CompleteCheckOperations(
                            checkSubpatternsFound.CheckFailedOperations,
                            enclosingSearchProgram,
                            enclosingAlternative,
                            enclosingCheckNegativeOrIndependent);
                    }
                    else
                    {
                        Debug.Assert(false, "unknown check partial match operation");
                    }

                }
                //////////////////////////////////////////////////////////
                else if (currentOperation is CheckContinueMatching)
                //////////////////////////////////////////////////////////
                {
                    if (currentOperation is CheckContinueMatchingMaximumMatchesReached)
                    {
                        CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                            (CheckContinueMatchingMaximumMatchesReached)currentOperation;
                        checkMaximumMatches.CheckFailedOperations =
                            new SearchProgramList(checkMaximumMatches);

                        if (checkMaximumMatches.ListHeadAdjustment)
                        {
                            MoveOutwardsAppendingListHeadAdjustment(checkMaximumMatches);
                        }

                        string[] neededElementsForCheckOperation = new string[0];
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkMaximumMatches,
                            neededElementsForCheckOperation,
                            enclosingSearchProgram);
                    }
                    else if (currentOperation is CheckContinueMatchingOfNegativeFailed)
                    {
                        CheckContinueMatchingOfNegativeFailed checkFailed =
                            (CheckContinueMatchingOfNegativeFailed)currentOperation;
                        checkFailed.CheckFailedOperations =
                            new SearchProgramList(checkFailed);
                        if(checkFailed.IsIterationBreaking)
                        {
                            string[] neededElementsForCheckOperation = new string[0];
                            MoveOutwardsAppendingRemoveIsomorphyAndJump(
                                checkFailed,
                                neededElementsForCheckOperation,
                                enclosingSearchProgram);
                        }
                        else
                        {
                            MoveOutwardsAppendingRemoveIsomorphyAndJump(
                                checkFailed,
                                enclosingCheckNegativeOrIndependent.NeededElements,
                                enclosingAlternative ?? enclosingSearchProgram);
                        }
                    }
                    else if (currentOperation is CheckContinueMatchingOfIndependentSucceeded)
                    {
                        CheckContinueMatchingOfIndependentSucceeded checkSucceeded =
                            (CheckContinueMatchingOfIndependentSucceeded)currentOperation;
                        checkSucceeded.CheckFailedOperations = // yep, rotten wording
                            new SearchProgramList(checkSucceeded);
                        MoveRightAfterCorrespondingIndependentFailedAppendingRemoveIsomorphyAndJump(
                            checkSucceeded,
                            (CheckPartialMatchByIndependent)enclosingCheckNegativeOrIndependent);
                    }
                    else if (currentOperation is CheckContinueMatchingOfIndependentFailed)
                    {
                        CheckContinueMatchingOfIndependentFailed checkFailed =
                            (CheckContinueMatchingOfIndependentFailed)currentOperation;
                        checkFailed.CheckFailedOperations =
                            new SearchProgramList(checkFailed);
                        if(checkFailed.IsIterationBreaking)
                        {
                            string[] neededElementsForCheckOperation = new string[0];
                            MoveOutwardsAppendingRemoveIsomorphyAndJump(
                                checkFailed,
                                neededElementsForCheckOperation,
                                enclosingSearchProgram);
                        }
                        else
                        {
                            MoveOutwardsAppendingRemoveIsomorphyAndJump(
                                checkFailed,
                                checkFailed.CheckIndependent.NeededElements,
                                enclosingAlternative ?? enclosingSearchProgram);
                        }
                    }
                    else if (currentOperation is CheckContinueMatchingTasksLeft)
                    {
                        CheckContinueMatchingTasksLeft tasksLeft =
                            (CheckContinueMatchingTasksLeft)currentOperation;

                        // determine insertion point within check failed operations
                        // to append the nested check maximum matches
                        SearchProgramOperation insertionPoint =
                            tasksLeft.CheckFailedOperations;
                        while (insertionPoint.Next != null)
                        {
                            insertionPoint = insertionPoint.Next;
                        }

                        // append nested check maximum matches
                        CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                            new CheckContinueMatchingMaximumMatchesReached(
                                enclosingSearchProgram is SearchProgramOfIterated ? CheckMaximumMatchesType.Iterated : CheckMaximumMatchesType.Subpattern,
                                false);
                        insertionPoint.Append(checkMaximumMatches);

                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            tasksLeft,
                            null,
                            enclosingCheckNegativeOrIndependent ?? (enclosingAlternative ?? enclosingSearchProgram));

                        // check tasks left has a further check maximum matches nested within check failed code
                        // give it its special bit of attention here
                        CompleteCheckOperations(
                            tasksLeft.CheckFailedOperations,
                            enclosingSearchProgram,
                            enclosingAlternative,
                            enclosingCheckNegativeOrIndependent);
                    }
                    else if (currentOperation is CheckContinueMatchingIteratedPatternNonNullMatchFound)
                    {
                        // was built completely, nothing to complete
                    }
                    else
                    {
                        Debug.Assert(false, "unknown check abort matching operation");
                    }
                }
                //////////////////////////////////////////////////////////
                else if (currentOperation is GetPartialMatchOfAlternative)
                //////////////////////////////////////////////////////////
                {
                    // depth first
                    CompleteCheckOperations(
                        currentOperation.GetNestedSearchOperationsList(),
                        enclosingSearchProgram,
                        (GetPartialMatchOfAlternative)currentOperation,
                        null);
                }
                //////////////////////////////////////////////////////////
                else if (currentOperation.IsSearchNestingOperation())
                //////////////////////////////////////////////////////////
                {
                    // depth first
                    CompleteCheckOperations(
                        currentOperation.GetNestedSearchOperationsList(),
                        enclosingSearchProgram,
                        enclosingAlternative,
                        enclosingCheckNegativeOrIndependent);
                }

                // breadth
                currentOperation = currentOperation.Next;
            }
        }

        /// <summary>
        /// "listentrick": append search program operations to adjust list heads
        /// i.e. set list entry point to element after last found,
        /// so that next searching starts there - preformance optimization
        /// (leave graph in the state of our last visit (searching it))
        /// </summary>
        private void MoveOutwardsAppendingListHeadAdjustment(
            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches)
        {
            // todo: avoid adjusting list heads twice for lookups of same type

            // insertion point for candidate failed operations
            SearchProgramOperation insertionPoint =
                checkMaximumMatches.CheckFailedOperations;
            SearchProgramOperation op = checkMaximumMatches;
            do
            {
                if (op is GetCandidateByIteration)
                {
                    GetCandidateByIteration candidateByIteration =
                        op as GetCandidateByIteration;
                    if (candidateByIteration.Type == GetCandidateByIterationType.GraphElements)
                    {
                        AdjustListHeads adjustElements =
                            new AdjustListHeads(
                                AdjustListHeadsTypes.GraphElements,
                                candidateByIteration.PatternElementName,
                                candidateByIteration.IsNode);
                        insertionPoint = insertionPoint.Append(adjustElements);
                    }
                    else if(candidateByIteration.Type==GetCandidateByIterationType.IncidentEdges)
                    {
                        AdjustListHeads adjustIncident =
                            new AdjustListHeads(
                                AdjustListHeadsTypes.IncidentEdges,
                                candidateByIteration.PatternElementName,
                                candidateByIteration.StartingPointNodeName,
                                candidateByIteration.EdgeType);
                        insertionPoint = insertionPoint.Append(adjustIncident);
                    }
                }

                op = op.Previous;
            }
            while (op != null);
        }

        /// <summary>
        /// move outwards from check operation until operation to continue at is found
        /// appending restore isomorphy for isomorphy written on the way
        /// and final jump to operation to continue at
        /// </summary>
        private void MoveOutwardsAppendingRemoveIsomorphyAndJump(
            CheckOperation checkOperation,
            string[] neededElementsForCheckOperation,
            SearchProgramOperation outermostOperation)
        {
            // insertion point for candidate failed operations
            SearchProgramOperation insertionPoint =
                checkOperation.CheckFailedOperations;
            while (insertionPoint.Next != null)
            {
                insertionPoint = insertionPoint.Next;
            }

            // set outermost to iterated dummy iteration if iterated
            if (outermostOperation is SearchProgramOfIterated)
            {
                SearchProgramOperation cur = checkOperation;
                while (!(cur is ReturnPreventingDummyIteration))
                {
                    cur = cur.Previous;
                }
                outermostOperation = cur;
            }

            SearchProgramOperation continuationPoint = MoveOutwardsAppendingRemoveIsomorphy(
                checkOperation, ref insertionPoint, neededElementsForCheckOperation, outermostOperation);

            // decide on type of continuing operation, then insert it

            // continue at top nesting level -> return
            SearchProgramOperation op = continuationPoint;
            while (!(op is SearchProgram))
            {
                op = op.Previous;
            }
            SearchProgram searchProgramRoot = op as SearchProgram;
            if (continuationPoint == searchProgramRoot)
            {
                ContinueOperation continueByReturn =
                    new ContinueOperation(
                        ContinueOperationType.ByReturn,
                        searchProgramRoot is SearchProgramOfAction
                        );
                insertionPoint.Append(continueByReturn);

                return;
            }

            // continue at directly enclosing nesting level -> continue
            op = checkOperation;
            do
            {
                op = op.Previous;
            }
            while (!op.IsSearchNestingOperation());
            SearchProgramOperation directlyEnclosingOperation = op;
            if (continuationPoint == directlyEnclosingOperation
                // (check negative/independent is enclosing, but not a loop, thus continue wouldn't work)
                && !(directlyEnclosingOperation is CheckPartialMatchByNegative)
                && !(directlyEnclosingOperation is CheckPartialMatchByIndependent))
            {
                ContinueOperation continueByContinue =
                    new ContinueOperation(ContinueOperationType.ByContinue);
                insertionPoint.Append(continueByContinue);

                return;
            }

            // otherwise -> goto label
            string gotoLabelName;

            // if our continuation point is a candidate iteration
            // -> append label at the end of the loop body of the candidate iteration loop
            if (continuationPoint is GetCandidateByIteration)
            {
                GetCandidateByIteration candidateIteration =
                    continuationPoint as GetCandidateByIteration;
                op = candidateIteration.NestedOperationsList;
                while (op.Next != null)
                {
                    op = op.Next;
                }
                GotoLabel gotoLabel = new GotoLabel();
                op.Append(gotoLabel);
                gotoLabelName = gotoLabel.LabelName;
            }
            // if our continuation point is a both directions iteration
            // -> append label at the end of the loop body of the both directions iteration loop
            else if (continuationPoint is BothDirectionsIteration)
            {
                BothDirectionsIteration bothDirections =
                    continuationPoint as BothDirectionsIteration;
                op = bothDirections.NestedOperationsList;
                while (op.Next != null)
                {
                    op = op.Next;
                }
                GotoLabel gotoLabel = new GotoLabel();
                op.Append(gotoLabel);
                gotoLabelName = gotoLabel.LabelName;
            }
            // if our continuation point is an alternative
            // -> append label at the end of the alternative operations
            else if (continuationPoint is GetPartialMatchOfAlternative)
            {
                GetPartialMatchOfAlternative getAlternative =
                    continuationPoint as GetPartialMatchOfAlternative;
                op = getAlternative.OperationsList;
                while (op.Next != null)
                {
                    op = op.Next;
                }
                GotoLabel gotoLabel = new GotoLabel();
                op.Append(gotoLabel);
                gotoLabelName = gotoLabel.LabelName;
            }
            // if our continuation point is the dummy loop of an iterated operation
            // -> we just jump to the label maxMatchesIterReached directly after the loop
            else if (continuationPoint is ReturnPreventingDummyIteration)
            {
                gotoLabelName = "maxMatchesIterReached";
            }
            // otherwise our continuation point is a check negative/independent operation
            // -> insert label directly after the check negative/independent operation
            else
            {
                CheckPartialMatchByNegativeOrIndependent checkNegativeIndependent =
                    continuationPoint as CheckPartialMatchByNegativeOrIndependent;
                GotoLabel gotoLabel = new GotoLabel();
                checkNegativeIndependent.Insert(gotoLabel);
                gotoLabelName = gotoLabel.LabelName;
            }

            ContinueOperation continueByGoto =
                new ContinueOperation(
                    ContinueOperationType.ByGoto,
                    gotoLabelName); // ByGoto due to parameters
            insertionPoint.Append(continueByGoto);
        }

        /// <summary>
        /// move outwards from check succeeded operation until check partial match by independent is found
        /// appending restore isomorphy for isomorphy written on the way
        /// and final jump to operation right after the independent failed operation of the check partial match by independent
        /// </summary>
        private void MoveRightAfterCorrespondingIndependentFailedAppendingRemoveIsomorphyAndJump(
            CheckContinueMatchingOfIndependentSucceeded checkSucceeded,
            CheckPartialMatchByIndependent enclosingIndependent)
        {
            // insertion point for candidate failed operations
            SearchProgramOperation insertionPoint =
                checkSucceeded.CheckFailedOperations;
            while (insertionPoint.Next != null) // needed?
            {
                insertionPoint = insertionPoint.Next;
            }

            // move outwards, append remove isomorphy
            string[] neededElements = new string[0];
            SearchProgramOperation continuationPoint = MoveOutwardsAppendingRemoveIsomorphy(
                checkSucceeded, ref insertionPoint, neededElements, enclosingIndependent);

            // move to check failed operation of check independent operation
            while (!(continuationPoint is CheckContinueMatchingOfIndependentFailed))
            {
                continuationPoint = continuationPoint.Next;
            }
            CheckContinueMatchingOfIndependentFailed checkFailed =
                (CheckContinueMatchingOfIndependentFailed)continuationPoint;

            // insert label right thereafter, append jump there at insertion point
            GotoLabel gotoLabel = new GotoLabel();
            checkFailed.Insert(gotoLabel);

            ContinueOperation continueByGoto =
                new ContinueOperation(
                    ContinueOperationType.ByGoto,
                    gotoLabel.LabelName);
            insertionPoint.Append(continueByGoto);
        }

        /// <summary>
        /// move outwards from starting point on until operation to continue at is found
        /// appending restore isomorphy at insertion point for isomorphy written on the way
        /// returns operation to continue at
        /// </summary>
        private SearchProgramOperation MoveOutwardsAppendingRemoveIsomorphy(
            SearchProgramOperation startingPoint,
            ref SearchProgramOperation insertionPoint,
            string[] neededElementsForCheckOperation,
            SearchProgramOperation outermostOperation)
        {
            // currently focused operation on our way outwards
            SearchProgramOperation op = startingPoint;
            // move outwards until operation to continue at is found
            bool creationPointOfDominatingElementFound = false;
            bool iterationReached = false;
            do
            {
                op = op.Previous;

                // insert code to clean up isomorphy information written by candidate acceptance
                // in between the operation to continue and the check operation
                if (op is AcceptCandidate)
                {
                    AcceptCandidate writeIsomorphy =
                        op as AcceptCandidate;
                    AbandonCandidate restoreIsomorphy =
                        new AbandonCandidate(
                            writeIsomorphy.PatternElementName,
                            writeIsomorphy.NegativeIndependentNamePrefix,
                            writeIsomorphy.IsNode,
                            writeIsomorphy.NeverAboveMaxNegLevel);
                    insertionPoint = insertionPoint.Append(restoreIsomorphy);
                }
                // insert code to clean up isomorphy information written by global candidate acceptance
                // in between the operation to continue and the check operation
                if (op is AcceptCandidateGlobal)
                {
                    AcceptCandidateGlobal writeIsomorphy =
                        op as AcceptCandidateGlobal;
                    AbandonCandidateGlobal removeIsomorphy =
                        new AbandonCandidateGlobal(
                            writeIsomorphy.PatternElementName,
                            writeIsomorphy.NegativeIndependentNamePrefix,
                            writeIsomorphy.IsNode,
                            writeIsomorphy.NeverAboveMaxNegLevel);
                    insertionPoint = insertionPoint.Append(removeIsomorphy);
                }
                // insert code to clean up isomorphy information written by patternpath candidate acceptance
                // in between the operation to continue and the check operation
                if (op is AcceptCandidatePatternpath)
                {
                    AcceptCandidatePatternpath writeIsomorphy =
                        op as AcceptCandidatePatternpath;
                    AbandonCandidatePatternpath removeIsomorphy =
                        new AbandonCandidatePatternpath(
                            writeIsomorphy.PatternElementName,
                            writeIsomorphy.NegativeIndependentNamePrefix,
                            writeIsomorphy.IsNode);
                    insertionPoint = insertionPoint.Append(removeIsomorphy);
                }
                // insert code to remove iterated pattern acceptance
                if (op is AcceptIterated)
                {
                    AcceptIterated acceptIterated =
                        op as AcceptIterated;
                    AbandonIterated abandonIterated =
                        new AbandonIterated();
                    insertionPoint = insertionPoint.Append(abandonIterated);
                }
                // insert code to undo subpattern matching initialization if we leave the subpattern matching method
                if (op is InitializeSubpatternMatching)
                {
                    InitializeSubpatternMatching initialize =
                        op as InitializeSubpatternMatching;
                    FinalizeSubpatternMatching finalize =
                        new FinalizeSubpatternMatching(initialize.Type);
                    insertionPoint = insertionPoint.Append(finalize);
                }
                // insert code to undo negative/independent matching initialization if we leave the negative/independent matching method
                if (op is InitializeNegativeIndependentMatching)
                {
                    InitializeNegativeIndependentMatching initialize =
                        op as InitializeNegativeIndependentMatching;
                    FinalizeNegativeIndependentMatching finalize =
                        new FinalizeNegativeIndependentMatching(initialize.NeverAboveMaxNegLevel);
                    insertionPoint = insertionPoint.Append(finalize);
                }

                // determine operation to continue at
                // found by looking at the graph elements
                // the check operation depends on / is dominated by
                // its the first element iteration on our way outwards the search program
                // after or at the point of a get element operation
                // of some dominating element the check depends on
                // (or the outermost operation if no iteration is found until it is reached)
                if (op is GetCandidate || op is BothDirectionsIteration)
                {
                    if (creationPointOfDominatingElementFound == false)
                    {
                        if (neededElementsForCheckOperation != null)
                        {
                            foreach (string dominating in neededElementsForCheckOperation)
                            {
                                GetCandidate getCandidate = op as GetCandidate;
                                BothDirectionsIteration bothDirections = op as BothDirectionsIteration;
                                if (getCandidate!=null && getCandidate.PatternElementName == dominating
                                    || bothDirections!=null && bothDirections.PatternElementName == dominating)
                                {
                                    creationPointOfDominatingElementFound = true;
                                    iterationReached = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // needed elements == null means everything fits,
                            // take first element iteration on our way outwards the search program
                            // (or the outermost operation if no iteration is found until it is reached)
                            creationPointOfDominatingElementFound = true;
                            iterationReached = false;
                        }
                    }
                    if (op is GetCandidateByIteration || op is BothDirectionsIteration)
                    {
                        iterationReached = true;
                    }
                }
            }
            while (!(creationPointOfDominatingElementFound && iterationReached)
                    && op != outermostOperation);

            return op;
        }
    }
}

