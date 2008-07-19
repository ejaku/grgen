/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
            SearchProgramOperation enclosingSearchProgram,
            GetPartialMatchOfAlternative enclosingAlternative,
            CheckPartialMatchByNegative enclosingCheckNegative)
        {
            // mainly dispatching and iteration method, traverses search program
            // real completion done in MoveOutwardsAppendingRemoveIsomorphyAndJump
            // outermost operation for that function is computed here, regarding negative patterns
            // program nesting structure: search program - [alternative] - [negative]*

            // complete check operations by inserting failure code
            // find them in depth first search of search program
            while (currentOperation != null)
            {
                //////////////////////////////////////////////////////////
                if (currentOperation is CheckCandidate)
                //////////////////////////////////////////////////////////
                {
                    if (currentOperation is CheckCandidateForPreset)
                    {
                        Debug.Assert(enclosingAlternative == null);
                        Debug.Assert(enclosingCheckNegative == null);

                        // when the call to the preset handling returns
                        // it might be because it found all available matches from the given parameters on
                        //   then we want to continue the matching
                        // or it might be because it found the required number of matches
                        //   then we want to abort the matching process
                        // so we have to add an additional maximum matches check to the check preset
                        CheckCandidateForPreset checkPreset =
                            (CheckCandidateForPreset)currentOperation;
                        checkPreset.CheckFailedOperations =
                            new SearchProgramList(checkPreset);
                        CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                            new CheckContinueMatchingMaximumMatchesReached(false, false);
                        checkPreset.CheckFailedOperations.Append(checkMaximumMatches);

                        string[] neededElementsForCheckOperation = new string[1];
                        neededElementsForCheckOperation[0] = checkPreset.PatternElementName;
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkPreset,
                            neededElementsForCheckOperation,
                            enclosingSearchProgram);

                        // check preset has a further check maximum matches nested within check failed code
                        // give it its special bit of attention here
                        CompleteCheckOperations(
                            checkPreset.CheckFailedOperations,
                            enclosingSearchProgram,
                            enclosingAlternative,
                            enclosingCheckNegative);
                    }
                    else
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
                            enclosingCheckNegative ?? (enclosingAlternative ?? enclosingSearchProgram));
                    }
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
                            enclosingCheckNegative ?? (enclosingAlternative ?? enclosingSearchProgram));
                    }
                    else if (currentOperation is CheckPartialMatchByNegative)
                    {
                        CheckPartialMatchByNegative checkNegative =
                            (CheckPartialMatchByNegative)currentOperation;

                        // ByNegative is handled in CheckContinueMatchingFailed 
                        // of the negative case - enter negative case
                        CompleteCheckOperations(
                            checkNegative.NestedOperationsList,
                            enclosingCheckNegative ?? enclosingSearchProgram,
                            enclosingCheckNegative!=null ? null : enclosingAlternative,
                            checkNegative);
                    }
                    else if (currentOperation is CheckPartialMatchForSubpatternsFound)
                    {
                        CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                            (CheckPartialMatchForSubpatternsFound)currentOperation;

                        if (enclosingCheckNegative == null)
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
                            bool atSubpatternLevel = enclosingSearchProgram is SearchProgramOfSubpattern
                                || enclosingSearchProgram is SearchProgramOfAlternative;
                            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
                                new CheckContinueMatchingMaximumMatchesReached(atSubpatternLevel, false);
                            insertionPoint.Append(checkMaximumMatches);

                            MoveOutwardsAppendingRemoveIsomorphyAndJump(
                                checkSubpatternsFound,
                                null,
                                enclosingCheckNegative ?? (enclosingAlternative ?? enclosingSearchProgram));
                        }

                        // check subpatterns found has a further check maximum matches 
                        // or check continue matching of negative failed nested within check failed code
                        // give it its special bit of attention here
                        CompleteCheckOperations(
                            checkSubpatternsFound.CheckFailedOperations,
                            enclosingSearchProgram,
                            enclosingAlternative,
                            enclosingCheckNegative);
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
                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            checkFailed,
                            enclosingCheckNegative.NeededElements,
                            enclosingAlternative ?? enclosingSearchProgram);
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
                            new CheckContinueMatchingMaximumMatchesReached(true, false);
                        insertionPoint.Append(checkMaximumMatches);

                        MoveOutwardsAppendingRemoveIsomorphyAndJump(
                            tasksLeft,
                            null,
                            enclosingCheckNegative ?? (enclosingAlternative ?? enclosingSearchProgram));

                        // check tasks left has a further check maximum matches nested within check failed code
                        // give it its special bit of attention here
                        CompleteCheckOperations(
                            tasksLeft.CheckFailedOperations,
                            enclosingSearchProgram,
                            enclosingAlternative,
                            enclosingCheckNegative);
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
                        enclosingCheckNegative);
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
                    else //candidateByIteration.Type==GetCandidateByIterationType.IncidentEdges
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
        /// and final jump to operation to continue
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
            // currently focused operation on our way outwards
            SearchProgramOperation op = checkOperation;
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
                            writeIsomorphy.NegativeNamePrefix,
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
                            writeIsomorphy.NegativeNamePrefix,
                            writeIsomorphy.IsNode);
                    insertionPoint = insertionPoint.Append(removeIsomorphy);
                }
                // insert code to undo subpattern matching initialization if we leave the subpattern matching method
                if (op is InitializeSubpatternMatching)
                {
                    InitializeSubpatternMatching initialize =
                        op as InitializeSubpatternMatching;
                    FinalizeSubpatternMatching finalize =
                        new FinalizeSubpatternMatching();
                    insertionPoint = insertionPoint.Append(finalize);
                }
                // insert code to undo negative matching initialization if we leave the negative matching method
                if (op is InitializeNegativeMatching)
                {
                    InitializeNegativeMatching initialize =
                        op as InitializeNegativeMatching;
                    FinalizeNegativeMatching finalize =
                        new FinalizeNegativeMatching(initialize.NeverAboveMaxNegLevel);
                    insertionPoint = insertionPoint.Append(finalize);
                }

                // determine operation to continue at
                // found by looking at the graph elements 
                // the check operation depends on / is dominated by
                // its the first element iteration on our way outwards the search program
                // after or at the point of a get element operation 
                // of some dominating element the check depends on
                // (or the outermost operation if no iteration is found until it is reached)
                if (op is GetCandidate)
                {
                    GetCandidate getCandidate = op as GetCandidate;
                    if (creationPointOfDominatingElementFound == false)
                    {
                        if (neededElementsForCheckOperation != null)
                        {
                            foreach (string dominating in neededElementsForCheckOperation)
                            {
                                if (getCandidate.PatternElementName == dominating)
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
                    if (op is GetCandidateByIteration)
                    {
                        iterationReached = true;
                    }
                }
            }
            while (!(creationPointOfDominatingElementFound && iterationReached)
                    && op != outermostOperation);
            SearchProgramOperation continuationPoint = op;

            // decide on type of continuing operation, then insert it

            // continue at top nesting level -> return
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
                // (check negative is enclosing, but not a loop, thus continue wouldn't work)
                && !(directlyEnclosingOperation is CheckPartialMatchByNegative))
            {
                ContinueOperation continueByContinue =
                    new ContinueOperation(ContinueOperationType.ByContinue);
                insertionPoint.Append(continueByContinue);

                return;
            }

            // otherwise -> goto label
            GotoLabel gotoLabel;

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
                gotoLabel = new GotoLabel();
                op.Append(gotoLabel);
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
                gotoLabel = new GotoLabel();
                op.Append(gotoLabel);
            }
            // otherwise our continuation point is a check negative operation
            // -> insert label directly after the check negative operation
            else
            {
                CheckPartialMatchByNegative checkNegative =
                    continuationPoint as CheckPartialMatchByNegative;
                gotoLabel = new GotoLabel();
                checkNegative.Insert(gotoLabel);
            }

            ContinueOperation continueByGoto =
                new ContinueOperation(
                    ContinueOperationType.ByGoto,
                    gotoLabel.LabelName); // ByGoto due to parameters
            insertionPoint.Append(continueByGoto);
        }
    }
}

