/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

//#define NO_ADJUST_LIST_HEADS

using System;
using System.Collections.Generic;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.expression;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// says what kind of search program to build
    /// </summary>
    enum SearchProgramType
    {
        Action, // action pattern matcher
        Subpattern, // subpattern matcher
        AlternativeCase, // alternative case matcher
        Iterated // iterated matcher
    }

    /// <summary>
    /// class for building search program data structure from scheduled search plan
    /// for all kinds of matchers, called by search program builder, 
    /// holds environment variables for this build process
    /// </summary>
    class SearchProgramBodyBuilder
    {
        public SearchProgramBodyBuilder(
            SearchProgramType programType,
            IGraphModel model,
            String rulePatternClassName,
            String packagePrefixedActionName,
            string[] parameterNames,
            string[] parameterTypes,
            PatternGraph patternGraph,
            bool emitProfiling,
            bool parallelized,
            int indexOfSchedule
        )
        {
            this.programType = programType;
            this.model = model;
            this.rulePatternClassName = rulePatternClassName;
            this.packagePrefixedActionName = packagePrefixedActionName;
            this.parameterNames = parameterNames;
            this.parameterTypes = parameterTypes;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            patternGraphWithNestingPatterns.Push(patternGraph);
            isoSpaceNeverAboveMaxIsoSpace = patternGraphWithNestingPatterns.Peek().maxIsoSpace < (int)LGSPElemFlags.MAX_ISO_SPACE;
            this.emitProfiling = emitProfiling;
            this.parallelized = parallelized;
            this.indexOfSchedule = indexOfSchedule;

            isNegative = false;
            isNestedInNegative = false;
            firstLoopPassed = false;

            arrayPerElementMethodBuilder = new SourceBuilder();
            arrayPerElementMethodBuilder.Indent();
            arrayPerElementMethodBuilder.Indent();

            helper = new SearchProgramBodyBuilderHelper(new SearchProgramBodyBuilderHelperEnvironment(this));
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        // all members here are internal instead of private to allow SearchProgramBodyBuilderEnvironment 
        // (and thus SearchProgramBodyBuilder in a limited way) to access it

        /// <summary>
        /// type of the program which gets currently built
        /// </summary>
        internal readonly SearchProgramType programType;

        /// <summary>
        /// The model for which the matcher functions shall be generated.
        /// </summary>
        internal readonly IGraphModel model;

        /// <summary>
        /// the pattern graph to build with its nesting patterns
        /// </summary>
        internal Stack<PatternGraph> patternGraphWithNestingPatterns;

        /// <summary>
        /// is the pattern graph a negative pattern graph?
        /// </summary>
        internal bool isNegative;

        /// <summary>
        /// is the current pattern graph nested within a negative pattern graph?
        /// </summary>
        internal bool isNestedInNegative;

        /// <summary>
        /// name of the rule pattern class of the pattern graph
        /// </summary>
        internal readonly string rulePatternClassName;

        /// <summary>
        /// types of the parameters of the action (null if not an action)
        /// </summary>
        internal readonly string[] parameterTypes;

        /// <summary>
        /// names of the parameters of the action (null if not an action)
        /// </summary>
        internal readonly string[] parameterNames;

        /// <summary>
        /// true if statically determined that the iso space number of the pattern getting constructed 
        /// is always below the maximum iso space number (the maximum nesting level of the isomorphy spaces)
        /// </summary>
        internal bool isoSpaceNeverAboveMaxIsoSpace;

        /// <summary>
        /// The index of the currently built schedule
        /// </summary>
        internal int indexOfSchedule;

        /// <summary>
        /// whether to build the parallelized matcher from the parallelized schedule
        /// </summary>
        internal bool parallelized;

        /// <summary>
        /// whether to emit code for gathering profiling information (about search steps executed)
        /// </summary>
        internal readonly bool emitProfiling;

        /// <summary>
        /// the package prefixed name of the action in case we're building a rule/test, otherwise null
        /// </summary>
        internal readonly string packagePrefixedActionName;

        /// <summary>
        /// tells whether the first loop of the search programm was built, or not yet
        /// needed for the profile that does special statistics for the first loop,
        /// because this is the one that will get parallelized in case of action parallelization
        /// only of relevance if programType == SearchProgramType.Action, otherwise the type pinns it to true
        /// </summary>
        internal bool firstLoopPassed;

        /// <summary>
        /// source builder that receives the array per element methods
        /// </summary>
        internal readonly SourceBuilder arrayPerElementMethodBuilder;

        /// <summary>
        /// helper class to execute queries
        /// </summary>
        readonly SearchProgramBodyBuilderHelper helper;

        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Builds search program operations from scheduled search plan operation.
        /// Decides which specialized build procedure is to be called.
        /// The specialized build procedure then calls this procedure again, 
        /// in order to process the next search plan operation.
        /// </summary>
        public SearchProgramOperation BuildScheduledSearchPlanOperationIntoSearchProgram(
            int indexOfScheduledSearchPlanOperationToBuild,
            SearchProgramOperation insertionPointWithinSearchProgram)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            SearchOperation op;
            if(parallelized)
            {
                if(indexOfScheduledSearchPlanOperationToBuild >=
                    patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length)
                { // end of scheduled search plan reached, stop recursive iteration (of body, the head ends with a parallel setup operation)
                    return buildMatchComplete(insertionPointWithinSearchProgram);
                }
                op = patternGraph.parallelizedSchedule[indexOfSchedule].
                    Operations[indexOfScheduledSearchPlanOperationToBuild];
            }
            else
            {
                if(indexOfScheduledSearchPlanOperationToBuild >=
                    patternGraph.schedulesIncludingNegativesAndIndependents[indexOfSchedule].Operations.Length)
                { // end of scheduled search plan reached, stop recursive iteration
                    return buildMatchComplete(insertionPointWithinSearchProgram);
                }
                op = patternGraph.schedulesIncludingNegativesAndIndependents[indexOfSchedule].
                    Operations[indexOfScheduledSearchPlanOperationToBuild];
            }

            // for current scheduled search plan operation 
            // insert corresponding search program operations into search program
            switch(op.Type)
            {
                case SearchOperationType.Void:
                    return BuildScheduledSearchPlanOperationIntoSearchProgram(
                        indexOfScheduledSearchPlanOperationToBuild + 1,
                        insertionPointWithinSearchProgram);

                case SearchOperationType.ActionPreset:
                    return buildActionPreset(insertionPointWithinSearchProgram,
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

                case SearchOperationType.DefToBeYieldedTo:
                    return buildDefToBeYieldedTo(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.Element,
                        patternGraph);

                case SearchOperationType.Lookup:
                    return buildLookup(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickFromStorage:
                    return buildPickFromStorage(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickFromStorageDependent:
                    return buildPickFromStorageDependent(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickFromIndex:
                    return buildPickFromIndex(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickFromIndexDependent:
                    return buildPickFromIndex(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickByName:
                    return buildPickByName(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.NameLookup,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickByNameDependent:
                    return buildPickByName(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.NameLookup,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickByUnique:
                    return buildPickByUnique(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.UniqueLookup,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.PickByUniqueDependent:
                    return buildPickByUnique(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.UniqueLookup,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.Cast:
                    return buildCast(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.Assign:
                    return buildAssign(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.Identity:
                    return buildIdentity(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.AssignVar:
                    return buildAssignVar(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (PatternVariable)op.Element,
                        op.Expression);

                case SearchOperationType.MapWithStorage:
                    return buildMapWithStorage(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.StorageIndex,
                        op.Isomorphy);

                case SearchOperationType.MapWithStorageDependent:
                    return buildMapWithStorageDependent(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.StorageIndex,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.ImplicitSource:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        ImplicitNodeType.Source);

                case SearchOperationType.ImplicitTarget:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        ImplicitNodeType.Target);

                case SearchOperationType.Implicit:
                    return buildImplicit(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        ImplicitNodeType.SourceOrTarget);

                case SearchOperationType.Incoming:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        IncidentEdgeType.Incoming);

                case SearchOperationType.Outgoing:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        IncidentEdgeType.Outgoing);

                case SearchOperationType.Incident:
                    return buildIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        IncidentEdgeType.IncomingOrOutgoing);

                case SearchOperationType.Condition:
                    return buildCondition(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (PatternCondition)op.Element);

                case SearchOperationType.LockLocalElementsForPatternpath:
                    return buildLockLocalElementsForPatternpath(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild);

                case SearchOperationType.NegativePattern:
                    return buildNegative(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        ((ScheduledSearchPlan)op.Element).PatternGraph);

                case SearchOperationType.IndependentPattern:
                    return buildIndependent(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        ((ScheduledSearchPlan)op.Element).PatternGraph);

                case SearchOperationType.InlinedIndependentCheckForDuplicateMatch:
                    return buildInlinedIndependentCheckForDuplicateMatch(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild);

                case SearchOperationType.WriteParallelPreset:
                    return buildWriteParallelPreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element);

                case SearchOperationType.WriteParallelPresetVar:
                    return buildWriteParallelPresetVar(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (PatternVariable)op.Element);

                case SearchOperationType.ParallelPreset:
                    return buildParallelPreset(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element);

                case SearchOperationType.ParallelPresetVar:
                    return buildParallelPresetVar(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (PatternVariable)op.Element);

                case SearchOperationType.SetupParallelLookup:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelLookupSetup(insertionPointWithinSearchProgram,
                        (SearchPlanNode)op.Element);

                case SearchOperationType.ParallelLookup:
                    return buildParallelLookup(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.SetupParallelPickFromStorage:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelPickFromStorageSetup(insertionPointWithinSearchProgram,
                        (SearchPlanNode)op.Element,
                        op.Storage);

                case SearchOperationType.ParallelPickFromStorage:
                    return buildParallelPickFromStorage(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.SetupParallelPickFromStorageDependent:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelPickFromStorageDependentSetup(insertionPointWithinSearchProgram,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Storage);

                case SearchOperationType.ParallelPickFromStorageDependent:
                    return buildParallelPickFromStorageDependent(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.SetupParallelPickFromIndex:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelPickFromIndexSetup(insertionPointWithinSearchProgram,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess);

                case SearchOperationType.ParallelPickFromIndex:
                    return buildParallelPickFromIndex(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.SetupParallelPickFromIndexDependent:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelPickFromIndexSetup(insertionPointWithinSearchProgram,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess);

                case SearchOperationType.ParallelPickFromIndexDependent:
                    return buildParallelPickFromIndex(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess,
                        op.Isomorphy,
                        op.ConnectednessCheck);

                case SearchOperationType.SetupParallelOutgoing:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelIncidentSetup(insertionPointWithinSearchProgram,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        IncidentEdgeType.Outgoing);

                case SearchOperationType.ParallelOutgoing:
                    return buildParallelIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        IncidentEdgeType.Outgoing);

                case SearchOperationType.SetupParallelIncoming:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelIncidentSetup(insertionPointWithinSearchProgram,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        IncidentEdgeType.Incoming);

                case SearchOperationType.ParallelIncoming:
                    return buildParallelIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        IncidentEdgeType.Incoming);

                case SearchOperationType.SetupParallelIncident:
                    Debug.Assert(indexOfScheduledSearchPlanOperationToBuild >= patternGraph.parallelizedSchedule[indexOfSchedule].Operations.Length - 1, "Setup parallel must be last operation in schedule");
                    return buildParallelIncidentSetup(insertionPointWithinSearchProgram,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        IncidentEdgeType.IncomingOrOutgoing);

                case SearchOperationType.ParallelIncident:
                    return buildParallelIncident(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        op.Isomorphy,
                        op.ConnectednessCheck,
                        IncidentEdgeType.IncomingOrOutgoing);

                default:
                    Debug.Assert(false, "Unknown search operation");
                    return insertionPointWithinSearchProgram;
            }
        }

        public bool wasIndependentInlined(PatternGraph patternGraph, int index)
        {
            return helper.wasIndependentInlined(patternGraph, index);
        }

        /// <summary>
        /// Inserts code to check whether iteration came to an end (pattern not found (again))
        /// and code to handle that case 
        /// </summary>
        public SearchProgramOperation insertEndOfIterationHandling(SearchProgramOperation insertionPoint)
        {
            return helper.insertEndOfIterationHandling(insertionPoint);
        }

        /// <summary>
        /// Search program operations implementing the
        /// ActionPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildActionPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            Debug.Assert(negativeIndependentNamePrefix == "", "Top-level maybe preset in negative/independent search plan");
            Debug.Assert(programType != SearchProgramType.Subpattern, "Maybe preset in subpattern");
            Debug.Assert(programType != SearchProgramType.AlternativeCase, "Maybe preset in alternative");
            Debug.Assert(programType != SearchProgramType.Iterated, "Maybe preset in iterated");

            // get candidate from inputs
            GetCandidateByDrawing fromInputs =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromInputs,
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(fromInputs);

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            Debug.Assert(negativeIndependentNamePrefix != "", "Negative/Independent preset in top-level search plan");

            // an inlined independet preset is differently named, must be assigned first
            if(target.PatternElement.PresetBecauseOfIndependentInlining)
            {
                // get candidate from other element (the cast is simply the following type check)
                GetCandidateByDrawing inlinedIndependentPreset =
                    new GetCandidateByDrawing(
                        GetCandidateByDrawingType.FromOtherElementForAssign,
                        target.PatternElement.Name,
                        target.PatternElement.Name + "_inlined_" + patternGraphWithNestingPatterns.Peek().Name,
                        isNode);
                insertionPoint = insertionPoint.Append(inlinedIndependentPreset);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
        /// DefElementToBeYieldedTo search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildDefToBeYieldedTo(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            object target,
            PatternGraph patternGraph)
        {
            if(target is PatternVariable)
            {
                PatternVariable var = (PatternVariable)target;

                // only variables of this pattern required declaration and initialization, variables from nesting patterns are handed in via the tasks object
                if(var.pointOfDefinition == patternGraph)
                {
                    String initializationExpression;
                    if(var.initialization != null)
                    {
                        SourceBuilder builder = new SourceBuilder();
                        var.initialization.Emit(builder);
                        var.initialization.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                        initializationExpression = builder.ToString();
                    }
                    else
                    {
                        string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(var.type), model);
                        initializationExpression = TypesHelper.DefaultValueString(typeName, model);
                    }
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Variable, TypesHelper.TypeName(var.type), var.Name,
                            initializationExpression)
                    );
                }
            }
            else
            {
                if(((SearchPlanNode)target).PatternElement is PatternNode)
                {
                    PatternNode node = (PatternNode)((SearchPlanNode)target).PatternElement;

                    // only nodes of this pattern required declaration and initialization, nodes from nesting patterns are handed in via the tasks object
                    if(node.pointOfDefinition == patternGraph)
                    {
                        String initializationExpression;
                        if(node.initialization != null)
                        {
                            SourceBuilder builder = new SourceBuilder();
                            node.initialization.Emit(builder);
                            node.initialization.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                            initializationExpression = builder.ToString();
                        }
                        else
                            initializationExpression = "null";
                        insertionPoint = insertionPoint.Append(
                            new DeclareDefElement(EntityType.Node, "GRGEN_LGSP.LGSPNode", node.Name, initializationExpression)
                        );
                    }
                }
                else
                {
                    PatternEdge edge = (PatternEdge)((SearchPlanNode)target).PatternElement;

                    // only edges of this pattern required declaration and initialization, edges from nesting patterns are handed in via the tasks object
                    if(edge.pointOfDefinition == patternGraph)
                    {
                        String initializationExpression;
                        if(edge.initialization != null)
                        {
                            SourceBuilder builder = new SourceBuilder();
                            edge.initialization.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                            edge.initialization.Emit(builder);
                            initializationExpression = builder.ToString();
                        }
                        else
                            initializationExpression = "null";
                        insertionPoint = insertionPoint.Append(
                            new DeclareDefElement(EntityType.Edge, "GRGEN_LGSP.LGSPEdge", edge.Name, initializationExpression)
                        );
                    }
                }
            }

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
        /// Lookup search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildLookup(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // decide on and insert operation determining type of candidate
            SearchProgramOperation continuationPointAfterTypeIteration;
            SearchProgramOperation insertionPointAfterTypeIteration =
                helper.decideOnAndInsertGetType(insertionPoint, target,
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
                    isNode,
                    parallelized,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            firstLoopPassed = true;
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType==SearchProgramType.Subpattern 
                || programType==SearchProgramType.AlternativeCase
                || programType==SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list at type iteration nesting level
            insertionPoint = continuationPoint;

            // everything nested within type iteration built by now
            // continue at the end of the list handed in
            if(insertionPointAfterTypeIteration != continuationPointAfterTypeIteration)
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
        /// PickFromStorage search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPickFromStorage(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            StorageAccess storage,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("set") || TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("map");
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // iterate available storage elements
            string iterationType;
            if(isDict)
            {
                iterationType = "System.Collections.Generic.KeyValuePair<"
                    + TypesHelper.GetStorageKeyTypeName(storage.Variable.type) + ","
                    + TypesHelper.GetStorageValueTypeName(storage.Variable.type) + ">";
            }
            else
                iterationType = TypesHelper.GetStorageKeyTypeName(storage.Variable.type);
            GetCandidateByIteration elementsIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.StorageElements,
                    target.PatternElement.Name,
                    storage.Variable.Name,
                    iterationType,
                    isDict,
                    isNode,
                    parallelized,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            firstLoopPassed = true;

            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list after storage iteration nesting level
            insertionPoint = continuationPoint;

            //storage.Variable != null - alt, siehe oben
            //storage.GlobalVariable != null - neu -- wenn es ein container-typ ist iterieren, wenn es ein elementarer typ ist eine einfache zuweisung -- und kein != null handling
            //storage.Attribute != null - das kann hier nicht auftreten

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// PickFromStorageDependent search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPickFromStorageDependent(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            StorageAccess storage,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = storage.Attribute.Attribute.Kind == AttributeKind.SetAttr || storage.Attribute.Attribute.Kind == AttributeKind.MapAttr;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // iterate available storage elements
            string iterationType;
            if(isDict)
            {
                if(storage.Attribute.Attribute.Kind == AttributeKind.SetAttr)
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model) + ","
                        + "de.unika.ipd.grGen.libGr.SetValueType" + ">";
                }
                else
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.KeyType.GetKindName(), model) + ","
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model) + ">";
                }
            }
            else
                iterationType = TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model);
 
            GetCandidateByIteration elementsIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.StorageAttributeElements,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    source.PatternElement.typeName,
                    storage.Attribute.Attribute.Name,
                    iterationType,
                    isDict,
                    isNode,
                    parallelized,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            firstLoopPassed = true;
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list after storage iteration nesting level
            insertionPoint = continuationPoint;

            //storage.Variable != null - das kann hier nicht auftreten
            //storage.GlobalVariable != null - das kann hier nicht auftreten
            //storage.Attribute != null - alt, siehe oben

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// PickFromIndex search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPickFromIndex(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IndexAccess index,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            string iterationType = TypesHelper.TypeName(index.Index is AttributeIndexDescription ?
                ((AttributeIndexDescription)index.Index).GraphElementType :
                ((IncidenceCountIndexDescription)index.Index).StartNodeType);
            string indexSetType = NamesOfEntities.IndexSetType(model.ModelName);

            // iterate available index elements
            GetCandidateByIteration elementsIteration;
            if(index is IndexAccessEquality)
            {
                IndexAccessEquality indexEquality = (IndexAccessEquality)index;
                SourceBuilder equalityExpression = new SourceBuilder();
                indexEquality.Expr.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                indexEquality.Expr.Emit(equalityExpression);
                elementsIteration =
                    new GetCandidateByIteration(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Equality,
                        equalityExpression.ToString(),
                        isNode,
                        parallelized,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else if(index is IndexAccessAscending)
            {
                IndexAccessAscending indexAscending = (IndexAccessAscending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexAscending.From != null)
                {
                    indexAscending.From.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexAscending.From.Emit(fromExpression);
                }
                SourceBuilder toExpression = new SourceBuilder();
                if(indexAscending.To != null)
                {
                    indexAscending.To.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexAscending.To.Emit(toExpression);
                }
                elementsIteration =
                    new GetCandidateByIteration(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Ascending,
                        indexAscending.From != null ? fromExpression.ToString() : null,
                        indexAscending.IncludingFrom,
                        indexAscending.To != null ? toExpression.ToString() : null,
                        indexAscending.IncludingTo,
                        isNode,
                        parallelized,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else //if(index is IndexAccessDescending)
            {
                IndexAccessDescending indexDescending = (IndexAccessDescending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexDescending.From != null)
                {
                    indexDescending.From.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexDescending.From.Emit(fromExpression);
                }
                SourceBuilder toExpression = new SourceBuilder();
                if(indexDescending.To != null)
                {
                    indexDescending.To.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexDescending.To.Emit(toExpression);
                }
                elementsIteration =
                    new GetCandidateByIteration(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Descending,
                        indexDescending.From != null ? fromExpression.ToString() : null,
                        indexDescending.IncludingFrom,
                        indexDescending.To != null ? toExpression.ToString() : null,
                        indexDescending.IncludingTo,
                        isNode,
                        parallelized,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            firstLoopPassed = true;

            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list after storage iteration nesting level
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// PickByName or PickByNameDependent search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPickByName(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            NameLookup nameLookup,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            SourceBuilder expression = new SourceBuilder();
            nameLookup.Expr.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
            nameLookup.Expr.Emit(expression);

            // get candidate from name map, only creates variable to hold it, get is fused with check for map membership
            GetCandidateByDrawing elementByName =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.MapByName,
                    target.PatternElement.Name,
                    expression.ToString(),
                    isNode);
            insertionPoint = insertionPoint.Append(elementByName);

            // check existence of candidate in name map 
            CheckCandidateMapByName checkElementInNameMap =
                new CheckCandidateMapByName(
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(checkElementInNameMap);

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            if(continuationPointAfterConnectednessCheck == insertionPoint)
                continuationPointAfterConnectednessCheck = null;

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            if(continuationPointAfterConnectednessCheck != null)
                insertionPoint = continuationPointAfterConnectednessCheck;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// PickByUnique or PickByUniqueDependent search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPickByUnique(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            UniqueLookup uniqueLookup,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            SourceBuilder expression = new SourceBuilder();
            uniqueLookup.Expr.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
            uniqueLookup.Expr.Emit(expression);

            // get candidate from unique index, only creates variable to hold it, get is fused with check for index membership
            GetCandidateByDrawing elementByUnique =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.MapByUnique,
                    target.PatternElement.Name,
                    expression.ToString(),
                    isNode);
            insertionPoint = insertionPoint.Append(elementByUnique);

            // check existence of candidate in unique map 
            CheckCandidateMapByUnique checkElementInUniqueMap =
                new CheckCandidateMapByUnique(
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(checkElementInUniqueMap);

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            if(continuationPointAfterConnectednessCheck == insertionPoint)
                continuationPointAfterConnectednessCheck = null;

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            if(continuationPointAfterConnectednessCheck != null)
                insertionPoint = continuationPointAfterConnectednessCheck;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Cast search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildCast(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // get candidate from other element (the cast is simply the following type check)
            GetCandidateByDrawing fromOtherElementForCast =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromOtherElementForCast,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(fromOtherElementForCast);

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Assign search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildAssign(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // get candidate from other element (the cast is simply the following type check)
            GetCandidateByDrawing fromOtherElementForAssign =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromOtherElementForAssign,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(fromOtherElementForAssign);

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
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

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// Identity search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildIdentity(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            IsomorphyInformation isomorphy)
        {
            // check candidate is identical to other element
            CheckCandidateForIdentity checkIdentical = new CheckCandidateForIdentity(
                target.PatternElement.Name,
                source.PatternElement.Name);
            insertionPoint = insertionPoint.Append(checkIdentical);

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
        /// AssignVar search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildAssignVar(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            PatternVariable variable,
            Expression expression)
        {
            // generate c#-code-string out of condition expression ast
            SourceBuilder assignmentExpression = new SourceBuilder();
            expression.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
            expression.Emit(assignmentExpression);

            // get candidate from other element (the cast is simply the following type check)
            AssignVariableFromExpression assignVar =
                new AssignVariableFromExpression(
                    variable.Name,
                    TypesHelper.TypeName(variable.type),
                    assignmentExpression.ToString());
            insertionPoint = insertionPoint.Append(assignVar);

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
        /// MapWithStorage search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildMapWithStorage(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            StorageAccess storage,
            StorageAccessIndex index,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // storage muss ein container typ nach graph element sein, index muss ein elementarer typ sein

            //storage.Variable != null - neu
            //storage.GlobalVariable != null - neu
            //storage.Attribute != null - das kann hier nicht auftreten
            //index.Variable != null - neu
            //index.GlobalVariable != null - neu
            //index.Attribute != null - das kann hier nicht auftreten
            //index.GraphElement != null - das kann hier nicht auftreten
            
            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// MapWithStorageDependent search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildMapWithStorageDependent(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            StorageAccess storage,
            StorageAccessIndex index,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // storage muss ein container typ nach graph element sein, index muss ein elementarer typ sein

            //storage.Variable != null && index.Variable != null - das kann hier nicht auftreten; 
            //storage.Variable != null && index.GlobalVariable != null - das kann hier nicht auftreten; 
            //storage.Variable != null && index.Attribute != null - neu
            //storage.Variable != null && index.GraphElement != null - alt, siehe unten

            //storage.GlobalVariable != null && index.Variable != null - das kann hier nicht auftreten; 
            //storage.GlobalVariable != null && index.GlobalVariable != null - das kann hier nicht auftreten; 
            //storage.GlobalVariable != null && index.Attribute != null - neu
            //storage.GlobalVariable != null && index.GraphElement != null - neu

            //storage.Attribute != null && index.Variable != null - neu
            //storage.Attribute != null && index.GlobalVariable != null - neu
            //storage.Attribute != null && index.Attribute != null - kann nicht auftreten, 2 abhängigkeiten
            //storage.Attribute != null && index.GraphElement != null - kann nicht auftreten, 2 anhängigkeiten
            
            // get candidate from storage-map, only creates variable to hold it, get is fused with check for map membership
            GetCandidateByDrawing elementFromStorage =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.MapWithStorage,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    storage.Variable.Name,
                    TypesHelper.GetStorageValueTypeName(storage.Variable.type),
                    isNode);
            insertionPoint = insertionPoint.Append(elementFromStorage);

            // check existence of candidate in storage map 
            CheckCandidateMapWithStorage checkElementInStorage =
                new CheckCandidateMapWithStorage(
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    storage.Variable.Name,
                    TypesHelper.GetStorageKeyTypeName(storage.Variable.type),
                    isNode);
            insertionPoint = insertionPoint.Append(checkElementInStorage);

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            if(continuationPointAfterConnectednessCheck == insertionPoint)
                continuationPointAfterConnectednessCheck = null;

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            isNode,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            if(continuationPointAfterConnectednessCheck != null)
                insertionPoint = continuationPointAfterConnectednessCheck;

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
            ConnectednessCheck connectednessCheck,
            ImplicitNodeType nodeType)
        {
            // get candidate = demanded node from edge
            SearchProgramOperation continuationPoint;
            insertionPoint = helper.insertImplicitNodeFromEdge(insertionPoint, source, target, nodeType,
                out continuationPoint);
            if(continuationPoint == insertionPoint)
                continuationPoint = null;

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            SearchPlanNodeNode otherNodeOfOriginatingEdge = null;
            if(nodeType == ImplicitNodeType.Source)
                otherNodeOfOriginatingEdge = source.PatternEdgeTarget;
            if(nodeType == ImplicitNodeType.Target)
                otherNodeOfOriginatingEdge = source.PatternEdgeSource;
            if(source.PatternEdgeTarget == source.PatternEdgeSource) // reflexive sign needed in unfixed direction case, too
                otherNodeOfOriginatingEdge = source.PatternEdgeSource;
            insertionPoint = helper.decideOnAndInsertCheckConnectednessOfImplicitNodeFromEdge(
                insertionPoint, target, source, otherNodeOfOriginatingEdge, connectednessCheck, out continuationPointAfterConnectednessCheck);
            if(continuationPoint == null && continuationPointAfterConnectednessCheck != insertionPoint)
                continuationPoint = continuationPointAfterConnectednessCheck;

            // check candidate for isomorphy 
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        true,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern 
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            true,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        true,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        true,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        true,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            if(continuationPoint != null)
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
            ConnectednessCheck connectednessCheck,
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
            SearchPlanNodeNode node = source;
            SearchPlanEdgeNode currentEdge = target;
            IncidentEdgeType incidentType = edgeType;
            GetCandidateByIteration incidentIteration;
            SearchProgramOperation continuationPoint;
            if(incidentType == IncidentEdgeType.Incoming || incidentType == IncidentEdgeType.Outgoing)
            {
                incidentIteration = new GetCandidateByIteration(
                    GetCandidateByIterationType.IncidentEdges,
                    currentEdge.PatternElement.Name,
                    node.PatternElement.Name,
                    incidentType,
                    parallelized,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
                firstLoopPassed = true;
                incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                continuationPoint = insertionPoint.Append(incidentIteration);
                insertionPoint = incidentIteration.NestedOperationsList;
            }
            else // IncidentEdgeType.IncomingOrOutgoing
            {
                if(currentEdge.PatternEdgeSource == currentEdge.PatternEdgeTarget)
                {
                    // reflexive edge without direction iteration as we don't want 2 matches 
                    incidentIteration = new GetCandidateByIteration(
                        GetCandidateByIterationType.IncidentEdges,
                        currentEdge.PatternElement.Name,
                        node.PatternElement.Name,
                        IncidentEdgeType.Incoming,
                        parallelized,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
                    firstLoopPassed = true;
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
                        IncidentEdgeType.IncomingOrOutgoing,
                        parallelized,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
                    firstLoopPassed = true;
                    incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                    insertionPoint = insertionPoint.Append(incidentIteration);
                    insertionPoint = incidentIteration.NestedOperationsList;
                }
            }

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointOfConnectednessCheck;
            insertionPoint = helper.decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(
                insertionPoint, target, source, edgeType==IncidentEdgeType.Incoming, connectednessCheck,
                out continuationPointOfConnectednessCheck);

            // check candidate for isomorphy 
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        false,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // check candidate for global isomorphy 
            if(programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated)
            {
                if(!isomorphy.TotallyHomomorph)
                {
                    CheckCandidateForIsomorphyGlobal checkIsomorphy =
                        new CheckCandidateForIsomorphyGlobal(
                            target.PatternElement.Name,
                            isomorphy.GloballyHomomorphPatternElementsAsListOfStrings(),
                            false,
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if(patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        false,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        helper.getCurrentLastMatchAtPreviousNestingLevel());
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        false,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        false,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            string[] neededElements = ComputeNeededElements(negativePatternGraph);

            CheckPartialMatchByNegative checkNegative =
               new CheckPartialMatchByNegative(neededElements);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(checkNegative);
            checkNegative.NestedOperationsList =
                new SearchProgramList(checkNegative);
            insertionPoint = checkNegative.NestedOperationsList;

            bool enclosingIsNegative = isNegative;
            bool enclosingIsNestedInNegative = isNestedInNegative;
            isNegative = true;
            isNestedInNegative = true;
            PatternGraph enclosingPatternGraph = patternGraphWithNestingPatterns.Peek();
            patternGraphWithNestingPatterns.Push(negativePatternGraph);
            isoSpaceNeverAboveMaxIsoSpace = patternGraphWithNestingPatterns.Peek().maxIsoSpace < (int)LGSPElemFlags.MAX_ISO_SPACE;
            bool parallelizedBak = parallelized;
            parallelized &= patternGraphWithNestingPatterns.Peek().parallelizedSchedule != null; // the neg within a parallelized head may be non-parallelized
            int indexOfScheduleBak = indexOfSchedule;
            if(parallelized)
                indexOfSchedule = 0; // the neg of a parallelized body at index 1 is still only at index 0

            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            bool negativeContainsSubpatterns = negativePatternGraph.embeddedGraphsPlusInlined.Length >= 1
                || negativePatternGraph.alternativesPlusInlined.Length >= 1
                || negativePatternGraph.iteratedsPlusInlined.Length >= 1;
            InitializeNegativeIndependentMatching initNeg = new InitializeNegativeIndependentMatching(
                negativeContainsSubpatterns, 
                negativeIndependentNamePrefix, 
                isoSpaceNeverAboveMaxIsoSpace,
                parallelized);
            insertionPoint = insertionPoint.Append(initNeg);
            insertionPoint = insertVariableDeclarationsNegIdpt(insertionPoint, negativePatternGraph);

            //---------------------------------------------------------------------------
            // build negative pattern
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);
            //---------------------------------------------------------------------------

            FinalizeNegativeIndependentMatching finalize = new FinalizeNegativeIndependentMatching(
                isoSpaceNeverAboveMaxIsoSpace, parallelized);
            insertionPoint = insertionPoint.Append(finalize);

            // negative pattern built by now
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;
            patternGraphWithNestingPatterns.Pop();
            parallelized = parallelizedBak;
            indexOfSchedule = indexOfScheduleBak;
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
            string[] neededElements = ComputeNeededElements(independentPatternGraph);

            CheckPartialMatchByIndependent checkIndependent =
               new CheckPartialMatchByIndependent(neededElements);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(checkIndependent);
            checkIndependent.NestedOperationsList =
                new SearchProgramList(checkIndependent);
            insertionPoint = checkIndependent.NestedOperationsList;

            bool enclosingIsNegative = isNegative;
            isNegative = false;
            PatternGraph enclosingPatternGraph = patternGraphWithNestingPatterns.Peek();
            patternGraphWithNestingPatterns.Push(independentPatternGraph);
            isoSpaceNeverAboveMaxIsoSpace = patternGraphWithNestingPatterns.Peek().maxIsoSpace < (int)LGSPElemFlags.MAX_ISO_SPACE;
            bool parallelizedBak = parallelized;
            parallelized &= patternGraphWithNestingPatterns.Peek().parallelizedSchedule != null; // the idpt within a parallelized head may be non-parallelized
            int indexOfScheduleBak = indexOfSchedule;
            if(parallelized)
                indexOfSchedule = 0; // the idpt of a parallelized body at index 1 is still only at index 0

            string independentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            bool independentContainsSubpatterns = independentPatternGraph.embeddedGraphsPlusInlined.Length >= 1
                || independentPatternGraph.alternativesPlusInlined.Length >= 1
                || independentPatternGraph.iteratedsPlusInlined.Length >= 1;
            InitializeNegativeIndependentMatching initIdpt = new InitializeNegativeIndependentMatching(
                independentContainsSubpatterns,
                independentNamePrefix,
                isoSpaceNeverAboveMaxIsoSpace,
                parallelized);
            insertionPoint = insertionPoint.Append(initIdpt);
            insertionPoint = insertVariableDeclarationsNegIdpt(insertionPoint, independentPatternGraph);

            //---------------------------------------------------------------------------
            // build independent pattern
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);
            //---------------------------------------------------------------------------

            FinalizeNegativeIndependentMatching finalize = new FinalizeNegativeIndependentMatching(
                isoSpaceNeverAboveMaxIsoSpace, parallelized);
            insertionPoint = insertionPoint.Append(finalize);

            // independent pattern built by now
            // continue at the end of the list handed in
            insertionPoint = continuationPoint;

            // the matcher program of independent didn't find a match,
            // we fell through the loops and reached this point -> abort the matching process / try next candidate
            CheckContinueMatchingOfIndependentFailed abortMatching =
                new CheckContinueMatchingOfIndependentFailed(checkIndependent, independentPatternGraph.isIterationBreaking);
            checkIndependent.CheckIndependentFailed = abortMatching;
            insertionPoint = insertionPoint.Append(abortMatching);

            patternGraphWithNestingPatterns.Pop();
            parallelized = parallelizedBak;
            indexOfSchedule = indexOfScheduleBak;
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
        /// check for duplicate match search plan operation (needed in case an independent was inlined)
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildInlinedIndependentCheckForDuplicateMatch(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            List<String> namesOfPatternElements;
            List<String> matchObjectPaths;
            List<String> unprefixedNamesOfPatternElements;
            List<bool> patternElementIsNode;
            helper.GetMatchElementsForDuplicateCheck(patternGraph,
                out namesOfPatternElements,
                out matchObjectPaths,
                out unprefixedNamesOfPatternElements,
                out patternElementIsNode);

            // if an independent was inlined, we may have to throw away duplicate matches
            // (duplicates in the sense that the non-inlined part was already matched with exactly the same elements)
            CheckPartialMatchForDuplicate checkDuplicateMatch =
                new CheckPartialMatchForDuplicate(rulePatternClassName,
                    patternGraph.pathPrefix + patternGraph.Name,
                    namesOfPatternElements.ToArray(),
                    matchObjectPaths.ToArray(),
                    unprefixedNamesOfPatternElements.ToArray(),
                    patternElementIsNode.ToArray());
            insertionPoint = insertionPoint.Append(checkDuplicateMatch);

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            return insertionPoint;
        }

        private static string[] ComputeNeededElements(PatternGraph negativePatternGraph)
        {
            int numberOfNeededElements = 0;
            foreach(SearchOperation op in negativePatternGraph.schedulesIncludingNegativesAndIndependents[0].Operations)
            {
                if(op.Type == SearchOperationType.NegIdptPreset)
                    ++numberOfNeededElements;
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach(SearchOperation op in negativePatternGraph.schedulesIncludingNegativesAndIndependents[0].Operations)
            {
                if(op.Type == SearchOperationType.NegIdptPreset)
                {
                    SearchPlanNode element = ((SearchPlanNode)op.Element);
                    neededElements[i] = element.PatternElement.Name;
                    ++i;
                }
            }
            return neededElements;
        }

        private SearchProgramOperation insertVariableDeclarationsNegIdpt(SearchProgramOperation insertionPoint, PatternGraph patternGraph)
        {
            foreach(PatternNode node in patternGraph.nodesPlusInlined)
            {
                if(node.defToBeYieldedTo && patternGraph.WasInlinedHere(node.originalSubpatternEmbedding))
                {
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Node, "GRGEN_LGSP.LGSPNode", node.Name, "null")
                    );
                }
            }
            foreach(PatternEdge edge in patternGraph.edgesPlusInlined)
            {
                if(edge.defToBeYieldedTo && patternGraph.WasInlinedHere(edge.originalSubpatternEmbedding))
                {
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Edge, "GRGEN_LGSP.LGSPEdge", edge.Name, "null")
                    );
                }
            }
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(var.defToBeYieldedTo && patternGraph.WasInlinedHere(var.originalSubpatternEmbedding))
                {
                    String initializationExpression;
                    if(var.initialization != null)
                    {
                        SourceBuilder builder = new SourceBuilder();
                        var.initialization.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                        var.initialization.Emit(builder);
                        initializationExpression = builder.ToString();
                    }
                    else
                    {
                        string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(var.type), model);
                        initializationExpression = TypesHelper.DefaultValueString(typeName, model);
                    }
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Variable, TypesHelper.TypeName(var.type), var.Name,
                            initializationExpression)
                    );
                }
            }

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
            condition.ConditionExpression.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
            condition.ConditionExpression.Emit(conditionExpression);

            // check condition with current partial match
            CheckPartialMatchByCondition checkCondition =
                new CheckPartialMatchByCondition(conditionExpression.ToString(),
                    condition.NeededNodeNames,
                    condition.NeededEdgeNames,
                    condition.NeededVariableNames);
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
        /// Search program operations implementing the
        /// LockLocalElementsForPatternpath search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildLockLocalElementsForPatternpath(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex)
        {
            bool isSubpattern = programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            PushMatchForPatternpath pushMatch =
                new PushMatchForPatternpath(
                    rulePatternClassName,
                    patternGraph.pathPrefix + patternGraph.name,
                    helper.getCurrentMatchOfNestingPattern()
                );
            insertionPoint = insertionPoint.Append(pushMatch);

            insertionPoint = helper.insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Patternpath, false);

            insertionPoint = helper.insertPatternpathAccept(insertionPoint, patternGraph);

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            insertionPoint = helper.insertPatternpathAbandon(insertionPoint, patternGraph);

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// WriteParallelPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildWriteParallelPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;

            // write parallel preset
            WriteParallelPreset writePreset =
                new WriteParallelPreset(
                    target.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(writePreset);

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
        /// WriteParallelPresetVar search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildWriteParallelPresetVar(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            PatternVariable target)
        {
            // write parallel preset var
            WriteParallelPresetVar writePresetVar =
                new WriteParallelPresetVar(
                    target.Name,
                    TypesHelper.TypeName(target.Type));
            insertionPoint = insertionPoint.Append(writePresetVar);

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
        /// ParallelPreset search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPreset(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;

            // get candidate from parallelization preset
            GetCandidateByDrawing fromInputs =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromParallelizationTask,
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
        /// ParallelPresetVar search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPresetVar(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            PatternVariable target)
        {
            // get candidate from parallelization preset
            GetCandidateByDrawing fromInputs =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromParallelizationTaskVar,
                    target.Name,
                    TypesHelper.TypeName(target.Type));
            insertionPoint = insertionPoint.Append(fromInputs);

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
        /// setup parallelized Lookup search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelLookupSetup(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            // decide on and insert operation determining type of candidate
            SearchProgramOperation continuationPointAfterTypeIteration;
            SearchProgramOperation insertionPointAfterTypeIteration =
                helper.decideOnAndInsertGetType(insertionPoint, target,
                out continuationPointAfterTypeIteration);
            insertionPoint = insertionPointAfterTypeIteration;

            // iterate available graph elements
            GetCandidateByIterationParallelSetup elementsIteration =
                new GetCandidateByIterationParallelSetup(
                    GetCandidateByIterationType.GraphElements,
                    target.PatternElement.Name,
                    isNode,
                    rulePatternClassName,
                    patternGraph.name,
                    parameterNames,
                    insertionPointAfterTypeIteration != continuationPointAfterTypeIteration,
                    helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            return insertionPoint.Append(elementsIteration);
        }

        /// <summary>
        /// Search program operations implementing the
        /// parallelized Lookup search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelLookup(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = ""; // parallel operations only in main pattern, in nested negatives/independents not supported

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
            GetCandidateByIterationParallel elementsIteration =
                new GetCandidateByIterationParallel(
                    GetCandidateByIterationType.GraphElements,
                    target.PatternElement.Name,
                    isNode,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            firstLoopPassed = true;
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list after candidate iteration
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// setup parallelized PickFromStorage search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPickFromStorageSetup(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target,
            StorageAccess storage)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("set") || TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("map");
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            // iterate available storage elements
            string iterationType;
            if(isDict)
            {
                iterationType = "System.Collections.Generic.KeyValuePair<"
                  + TypesHelper.GetStorageKeyTypeName(storage.Variable.type) + ","
                  + TypesHelper.GetStorageValueTypeName(storage.Variable.type) + ">";
            }
            else
                iterationType = TypesHelper.GetStorageKeyTypeName(storage.Variable.type);

            GetCandidateByIterationParallelSetup elementsIteration =
                new GetCandidateByIterationParallelSetup(
                    GetCandidateByIterationType.StorageElements,
                    target.PatternElement.Name,
                    storage.Variable.Name,
                    iterationType,
                    isDict,
                    isNode,
                    rulePatternClassName,
                    patternGraph.Name,
                    parameterNames,
                    helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            return insertionPoint.Append(elementsIteration);
        }

        /// <summary>
        /// Search program operations implementing the
        /// parallelized PickFromStorage search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPickFromStorage(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            StorageAccess storage,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("set") || TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("map");
            string negativeIndependentNamePrefix = ""; // parallel operations only in main pattern, in nested negatives/independents not supported

            // iterate available storage elements
            string iterationType;
            if(isDict)
            {
                iterationType = "System.Collections.Generic.KeyValuePair<"
                    + TypesHelper.GetStorageKeyTypeName(storage.Variable.type) + ","
                    + TypesHelper.GetStorageValueTypeName(storage.Variable.type) + ">";
            }
            else
                iterationType = TypesHelper.GetStorageKeyTypeName(storage.Variable.type);
            GetCandidateByIterationParallel elementsIteration =
                new GetCandidateByIterationParallel(
                    GetCandidateByIterationType.StorageElements,
                    target.PatternElement.Name,
                    storage.Variable.Name,
                    iterationType,
                    isDict,
                    isNode,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            firstLoopPassed = true;

            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list after storage iteration
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// setup parallelized PickFromStorageDependent search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPickFromStorageDependentSetup(
            SearchProgramOperation insertionPoint,
            SearchPlanNode source,
            SearchPlanNode target,
            StorageAccess storage)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = storage.Attribute.Attribute.Kind == AttributeKind.SetAttr || storage.Attribute.Attribute.Kind == AttributeKind.MapAttr;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            // iterate available storage elements
            string iterationType;
            if(isDict)
            {
                if(storage.Attribute.Attribute.Kind == AttributeKind.SetAttr)
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model) + ","
                        + "de.unika.ipd.grGen.libGr.SetValueType" + ">";
                }
                else
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.KeyType.GetKindName(), model) + ","
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model) + ">";
                }
            }
            else
                iterationType = TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model);

            GetCandidateByIterationParallelSetup elementsIteration =
                new GetCandidateByIterationParallelSetup(
                    GetCandidateByIterationType.StorageAttributeElements,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    source.PatternElement.typeName,
                    storage.Attribute.Attribute.Name,
                    iterationType,
                    isDict,
                    isNode,
                    rulePatternClassName,
                    patternGraph.name,
                    parameterNames,
                    helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            return insertionPoint.Append(elementsIteration);
        }

        /// <summary>
        /// Search program operations implementing the
        /// parallelized PickFromStorageDependent search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPickFromStorageDependent(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            StorageAccess storage,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = storage.Attribute.Attribute.Kind == AttributeKind.SetAttr || storage.Attribute.Attribute.Kind == AttributeKind.MapAttr;
            string negativeIndependentNamePrefix = ""; // parallel operations only in main pattern, in nested negatives/independents not supported

            // iterate available storage elements
            string iterationType;
            if(isDict)
            {
                if(storage.Attribute.Attribute.Kind == AttributeKind.SetAttr)
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model) + ","
                        + "de.unika.ipd.grGen.libGr.SetValueType" + ">";
                }
                else
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.KeyType.GetKindName(), model) + ","
                        + TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model) + ">";
                }
            }
            else
                iterationType = TypesHelper.XgrsTypeToCSharpType(storage.Attribute.Attribute.ValueType.GetKindName(), model);

            GetCandidateByIterationParallel elementsIteration =
                new GetCandidateByIterationParallel(
                    GetCandidateByIterationType.StorageAttributeElements,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    source.PatternElement.typeName,
                    storage.Attribute.Attribute.Name,
                    iterationType,
                    isDict,
                    isNode,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
            firstLoopPassed = true;
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list after storage iteration nesting level
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// setup parallelized PickFromIndex search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPickFromIndexSetup(
            SearchProgramOperation insertionPoint,
            SearchPlanNode target,
            IndexAccess index)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string iterationType = TypesHelper.TypeName(index.Index is AttributeIndexDescription ?
                ((AttributeIndexDescription)index.Index).GraphElementType :
                ((IncidenceCountIndexDescription)index.Index).StartNodeType);
            string indexSetType = NamesOfEntities.IndexSetType(model.ModelName);

            // iterate available index elements
            GetCandidateByIterationParallelSetup elementsIteration;
            if(index is IndexAccessEquality)
            {
                IndexAccessEquality indexEquality = (IndexAccessEquality)index;
                SourceBuilder equalityExpression = new SourceBuilder();
                indexEquality.Expr.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                indexEquality.Expr.Emit(equalityExpression);
                elementsIteration =
                    new GetCandidateByIterationParallelSetup(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Equality,
                        equalityExpression.ToString(),
                        isNode,
                        rulePatternClassName,
                        patternGraph.name,
                        parameterNames,
                        helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else if(index is IndexAccessAscending)
            {
                IndexAccessAscending indexAscending = (IndexAccessAscending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexAscending.From != null)
                {
                    indexAscending.From.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexAscending.From.Emit(fromExpression);
                }
                SourceBuilder toExpression = new SourceBuilder();
                if(indexAscending.To != null)
                {
                    indexAscending.To.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexAscending.To.Emit(toExpression);
                }
                elementsIteration =
                    new GetCandidateByIterationParallelSetup(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Ascending,
                        indexAscending.From != null ? fromExpression.ToString() : null,
                        indexAscending.IncludingFrom,
                        indexAscending.To != null ? toExpression.ToString() : null,
                        indexAscending.IncludingTo,
                        isNode,
                        rulePatternClassName,
                        patternGraph.name,
                        parameterNames,
                        helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else //if(index is IndexAccessDescending)
            {
                IndexAccessDescending indexDescending = (IndexAccessDescending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexDescending.From != null)
                {
                    indexDescending.From.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexDescending.From.Emit(fromExpression);
                }
                SourceBuilder toExpression = new SourceBuilder();
                if(indexDescending.To != null)
                {
                    indexDescending.To.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexDescending.To.Emit(toExpression);
                }
                elementsIteration =
                    new GetCandidateByIterationParallelSetup(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Descending,
                        indexDescending.From != null ? fromExpression.ToString() : null,
                        indexDescending.IncludingFrom,
                        indexDescending.To != null ? toExpression.ToString() : null,
                        indexDescending.IncludingTo,
                        isNode,
                        rulePatternClassName,
                        patternGraph.name,
                        parameterNames,
                        helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            return insertionPoint.Append(elementsIteration);
        }

        /// <summary>
        /// Search program operations implementing the
        /// parallelized PickFromIndex search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelPickFromIndex(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            IndexAccess index,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = ""; // parallel operations only in main pattern, in nested negatives/independents not supported
            string iterationType = TypesHelper.TypeName(index.Index is AttributeIndexDescription ?
                ((AttributeIndexDescription)index.Index).GraphElementType :
                ((IncidenceCountIndexDescription)index.Index).StartNodeType);
            string indexSetType = NamesOfEntities.IndexSetType(model.ModelName);

            // iterate available index elements
            GetCandidateByIterationParallel elementsIteration;
            if(index is IndexAccessEquality)
            {
                IndexAccessEquality indexEquality = (IndexAccessEquality)index;
                SourceBuilder equalityExpression = new SourceBuilder();
                indexEquality.Expr.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                indexEquality.Expr.Emit(equalityExpression);
                elementsIteration =
                    new GetCandidateByIterationParallel(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Equality,
                        equalityExpression.ToString(),
                        isNode,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else if(index is IndexAccessAscending)
            {
                IndexAccessAscending indexAscending = (IndexAccessAscending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexAscending.From != null)
                {
                    indexAscending.From.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexAscending.From.Emit(fromExpression);
                }
                SourceBuilder toExpression = new SourceBuilder();
                if(indexAscending.To != null)
                {
                    indexAscending.To.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexAscending.To.Emit(toExpression);
                }
                elementsIteration =
                    new GetCandidateByIterationParallel(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Ascending,
                        indexAscending.From != null ? fromExpression.ToString() : null,
                        indexAscending.IncludingFrom,
                        indexAscending.To != null ? toExpression.ToString() : null,
                        indexAscending.IncludingTo,
                        isNode,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else //if(index is IndexAccessDescending)
            {
                IndexAccessDescending indexDescending = (IndexAccessDescending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexDescending.From != null)
                {
                    indexDescending.From.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexDescending.From.Emit(fromExpression);
                }
                SourceBuilder toExpression = new SourceBuilder();
                if(indexDescending.To != null)
                {
                    indexDescending.To.EmitLambdaExpressionImplementationMethods(arrayPerElementMethodBuilder);
                    indexDescending.To.Emit(toExpression);
                }
                elementsIteration =
                    new GetCandidateByIterationParallel(
                        GetCandidateByIterationType.IndexElements,
                        target.PatternElement.Name,
                        index.Index.Name,
                        iterationType,
                        indexSetType,
                        IndexAccessType.Descending,
                        indexDescending.From != null ? fromExpression.ToString() : null,
                        indexDescending.IncludingFrom,
                        indexDescending.To != null ? toExpression.ToString() : null,
                        indexDescending.IncludingTo,
                        isNode,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            firstLoopPassed = true;

            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = helper.decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, connectednessCheck, out continuationPointAfterConnectednessCheck);
            }

            // check candidate for isomorphy 
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within candidate iteration built by now -
            // continue at the end of the list after storage iteration
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// setup parallelized Extend Incoming|Outgoing|IncomingOrOutgoing search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelIncidentSetup(
            SearchProgramOperation insertionPoint,
            SearchPlanNodeNode source,
            SearchPlanEdgeNode target,
            IncidentEdgeType edgeType)
        {
            // iterate available incident edges
            SearchPlanNodeNode node = source;
            SearchPlanEdgeNode currentEdge = target;
            IncidentEdgeType incidentType = edgeType;
            GetCandidateByIterationParallelSetup incidentIteration;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            if(incidentType == IncidentEdgeType.Incoming || incidentType == IncidentEdgeType.Outgoing)
            {
                incidentIteration = new GetCandidateByIterationParallelSetup(
                    GetCandidateByIterationType.IncidentEdges,
                    currentEdge.PatternElement.Name,
                    node.PatternElement.Name,
                    incidentType,
                    rulePatternClassName,
                    patternGraph.name,
                    parameterNames,
                    false,
                    helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
                return insertionPoint.Append(incidentIteration);
            }
            else // IncidentEdgeType.IncomingOrOutgoing
            {
                if(currentEdge.PatternEdgeSource == currentEdge.PatternEdgeTarget)
                {
                    // reflexive edge without direction iteration as we don't want 2 matches 
                    incidentIteration = new GetCandidateByIterationParallelSetup(
                        GetCandidateByIterationType.IncidentEdges,
                        currentEdge.PatternElement.Name,
                        node.PatternElement.Name,
                        IncidentEdgeType.Incoming,
                        rulePatternClassName,
                        patternGraph.name,
                        parameterNames,
                        false,
                        helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
                    return insertionPoint.Append(incidentIteration);
                }
                else
                {
                    BothDirectionsIteration directionsIteration =
                        new BothDirectionsIteration(currentEdge.PatternElement.Name);
                    directionsIteration.NestedOperationsList = new SearchProgramList(directionsIteration);
                    SearchProgramOperation continuationPoint = insertionPoint.Append(directionsIteration);
                    insertionPoint = directionsIteration.NestedOperationsList;

                    incidentIteration = new GetCandidateByIterationParallelSetup(
                        GetCandidateByIterationType.IncidentEdges,
                        currentEdge.PatternElement.Name,
                        node.PatternElement.Name,
                        IncidentEdgeType.IncomingOrOutgoing,
                        rulePatternClassName,
                        patternGraph.name,
                        parameterNames,
                        true,
                        helper.wasIndependentInlined(patternGraph, indexOfSchedule),
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
                    return insertionPoint.Append(incidentIteration);
                }
            }
        }

        /// <summary>
        /// Search program operations implementing the
        /// parallelized Extend Incoming|Outgoing|IncomingOrOutgoing search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildParallelIncident(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNodeNode source,
            SearchPlanEdgeNode target,
            IsomorphyInformation isomorphy,
            ConnectednessCheck connectednessCheck,
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
            SearchPlanNodeNode node = source;
            SearchPlanEdgeNode currentEdge = target;
            IncidentEdgeType incidentType = edgeType;
            SearchProgramOperation continuationPoint;
            GetCandidateByIterationParallel incidentIteration;
            if(incidentType == IncidentEdgeType.Incoming || incidentType == IncidentEdgeType.Outgoing)
            {
                incidentIteration = new GetCandidateByIterationParallel(
                    GetCandidateByIterationType.IncidentEdges,
                    currentEdge.PatternElement.Name,
                    node.PatternElement.Name,
                    incidentType,
                    emitProfiling,
                    packagePrefixedActionName,
                    !firstLoopPassed);
                firstLoopPassed = true;
                incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                continuationPoint = insertionPoint.Append(incidentIteration);
                insertionPoint = incidentIteration.NestedOperationsList;
            }
            else // IncidentEdgeType.IncomingOrOutgoing
            {
                if(currentEdge.PatternEdgeSource == currentEdge.PatternEdgeTarget)
                {
                    // reflexive edge without direction iteration as we don't want 2 matches 
                    incidentIteration = new GetCandidateByIterationParallel(
                        GetCandidateByIterationType.IncidentEdges,
                        currentEdge.PatternElement.Name,
                        node.PatternElement.Name,
                        IncidentEdgeType.Incoming,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
                    firstLoopPassed = true;
                    incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                    continuationPoint = insertionPoint.Append(incidentIteration);
                    insertionPoint = incidentIteration.NestedOperationsList;
                }
                else
                {
                    incidentIteration = new GetCandidateByIterationParallel(
                        GetCandidateByIterationType.IncidentEdges,
                        currentEdge.PatternElement.Name,
                        node.PatternElement.Name,
                        IncidentEdgeType.IncomingOrOutgoing,
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
                    firstLoopPassed = true;
                    incidentIteration.NestedOperationsList = new SearchProgramList(incidentIteration);
                    continuationPoint = insertionPoint.Append(incidentIteration);
                    insertionPoint = incidentIteration.NestedOperationsList;
                }
            }

            // check type of candidate
            insertionPoint = helper.decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointOfConnectednessCheck;
            insertionPoint = helper.decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(
                insertionPoint, target, source, edgeType == IncidentEdgeType.Incoming, connectednessCheck,
                out continuationPointOfConnectednessCheck);

            // check candidate for isomorphy 
            string negativeIndependentNamePrefix = "";
            if(isomorphy.CheckIsMatchedBit)
            {
                CheckCandidateForIsomorphy checkIsomorphy =
                    new CheckCandidateForIsomorphy(
                        target.PatternElement.Name,
                        isomorphy.PatternElementsToCheckAgainstAsListOfStrings(),
                        negativeIndependentNamePrefix,
                        false,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(checkIsomorphy);
            }

            // accept candidate (write isomorphy information)
            if(isomorphy.SetIsMatchedBit)
            {
                AcceptCandidate acceptCandidate =
                    new AcceptCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        false,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        false,
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
                insertionPoint = insertionPoint.Append(abandonCandidate);
            }

            // everything nested within incident iteration built by now -
            // continue at the end of the list after incident edges iteration
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations completing the matching process
        /// after all pattern elements have been found are created and inserted into the program
        /// </summary>
        private SearchProgramOperation buildMatchComplete(
            SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = helper.NegativeIndependentNamePrefix(patternGraph);

            // is subpattern gives information about type of top level enclosing pattern (action vs. subpattern)
            // contains subpatterns gives informations about current pattern graph (might be negative, independent itself, too)
            bool isSubpattern = programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated;
            bool existsNonInlinedSubpattern = false;
            foreach(PatternGraphEmbedding sub in patternGraph.embeddedGraphsPlusInlined)
            {
                if(!sub.inlined)
                    existsNonInlinedSubpattern = true;
            }
            bool containsSubpatterns = existsNonInlinedSubpattern
                || patternGraph.alternativesPlusInlined.Length >= 1
                || patternGraph.iteratedsPlusInlined.Length >= 1;

            // increase iterated matched counter if it is iterated
            if(programType==SearchProgramType.Iterated) {
                AcceptIterated acceptIterated = new AcceptIterated();
                insertionPoint = insertionPoint.Append(acceptIterated);
            }

            // push subpattern tasks (in case there are some)
            if(containsSubpatterns)
                insertionPoint = helper.insertPushSubpatternTasks(insertionPoint);

            // if this is a subpattern without subpatterns
            // it may be the last to be matched - handle that case
            if(!containsSubpatterns && isSubpattern && programType!=SearchProgramType.Iterated && negativeIndependentNamePrefix=="")
                insertionPoint = helper.insertCheckForTasksLeft(insertionPoint);

            // if this is a subpattern or a top-level pattern which contains subpatterns
            if(containsSubpatterns || isSubpattern && negativeIndependentNamePrefix=="")
            {
                // we do the global accept of all candidate elements (write isomorphy information)
                insertionPoint = helper.insertGlobalAccept(insertionPoint);

                // and execute the open subpattern matching tasks
                MatchSubpatterns matchSubpatterns = new MatchSubpatterns(negativeIndependentNamePrefix, parallelized);
                insertionPoint = insertionPoint.Append(matchSubpatterns);
            }

            // pop subpattern tasks (in case there are/were some)
            if(containsSubpatterns)
                insertionPoint = helper.insertPopSubpatternTasks(insertionPoint);

            // handle matched or not matched subpatterns, matched local pattern
            if(negativeIndependentNamePrefix == "")
            {
                // subpattern or top-level pattern which contains subpatterns
                if(containsSubpatterns || isSubpattern)
                    insertionPoint = helper.insertCheckForSubpatternsFound(insertionPoint, false);
                else // top-level-pattern of action without subpatterns
                    insertionPoint = helper.insertPatternFound(insertionPoint);
            }
            else
            {
                // negative/independent contains subpatterns
                if(containsSubpatterns)
                    insertionPoint = helper.insertCheckForSubpatternsFoundNegativeIndependent(insertionPoint);
                else
                    insertionPoint = helper.insertPatternFoundNegativeIndependent(insertionPoint);
            }

            // global abandon of all accepted candidate elements (remove isomorphy information)
            if(containsSubpatterns || isSubpattern && negativeIndependentNamePrefix=="")
                insertionPoint = helper.insertGlobalAbandon(insertionPoint);

            // decrease iterated matched counter again if it is iterated
            if(programType == SearchProgramType.Iterated)
            {
                AbandonIterated abandonIterated = new AbandonIterated();
                insertionPoint = insertionPoint.Append(abandonIterated);
            }

            return insertionPoint;
        }
    }
}

