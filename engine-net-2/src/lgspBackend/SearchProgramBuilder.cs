/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define NO_ADJUST_LIST_HEADS

using System;
using System.Collections.Generic;
using System.Text;
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
    /// holds environment variables for this build process
    /// </summary>
    class SearchProgramBuilder
    {
        /// <summary>
        /// Builds search program from scheduled search plan at given index in pattern graph of the action rule pattern
        /// </summary>
        public SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPRulePattern rulePattern,
            int index,
            string nameOfSearchProgram)
        {
            PatternGraph patternGraph = rulePattern.patternGraph;
            this.model = model;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            patternGraphWithNestingPatterns.Push(patternGraph);
            negLevelNeverAboveMaxNegLevel = patternGraphWithNestingPatterns.Peek().maxNegLevel <= (int)LGSPElemFlags.MAX_NEG_LEVEL;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(rulePattern.name, false);
            
            // filter out parameters which are implemented by lookup due to maybe null unfolding
            // and suffix matcher method name by missing parameters which get computed by lookup here
            String name;
            GetFilteredParametersAndSuffixedMatcherName(
                rulePattern, patternGraph, index,
                out parameterTypes, out parameterNames, out name);

            // this is the all presets available method (index 0) and there are presets which may be null?
            // -> collect data for missing preset calls
            List<String[]> paramTypesList = null;
            List<String[]> paramNamesList = null;
            List<String> suffixedMatcherNameList = null;
            if(patternGraph.schedules.Length>1 && index==0)
            {
                paramTypesList = new List<String[]>();
                paramNamesList = new List<String[]>();
                suffixedMatcherNameList = new List<String>();
                for(int i=0; i<patternGraph.schedules.Length; ++i)
                {
                    String[] paramTypes;
                    String[] paramNames;
                    String suffixedMatcherName;
                    GetFilteredParametersAndSuffixedMatcherName(
                        rulePattern, patternGraph, i,
                        out paramTypes, out paramNames, out suffixedMatcherName);
                    paramTypesList.Add(paramTypes);
                    paramNamesList.Add(paramNames);
                    suffixedMatcherNameList.Add(suffixedMatcherName);
                }
            }

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfAction(
                rulePatternClassName,
                patternGraph.name, parameterTypes, parameterNames, name,
                rulePattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                patternGraph.embeddedGraphs.Length > 0 || patternGraph.iterateds.Length > 0 || patternGraph.alternatives.Length > 0,
                patternGraph.maybeNullElementNames, suffixedMatcherNameList, paramNamesList);
 
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            insertionPoint = insertVariableDeclarations(insertionPoint, patternGraph);

            // start building with first operation in scheduled search plan
            indexOfSchedule = index;
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0, insertionPoint);

            patternGraphWithNestingPatterns.Pop();

            return searchProgram;
        }

        public void GetFilteredParametersAndSuffixedMatcherName(
            LGSPRulePattern rulePattern, PatternGraph patternGraph, int index,
            out String[] paramTypesArray, out String[] paramNamesArray, out String suffixedMatcherName)
        {
            List<String> paramTypes = new List<String>();
            List<String> paramNames = new List<String>();
            List<String> removedNames = new List<String>();
            for(int i = 0; i < rulePattern.Inputs.Length; ++i)
            {
                String inputName = rulePattern.InputNames[i];
                if(patternGraph.availabilityOfMaybeNullElements[index].ContainsKey(inputName)
                    && !patternGraph.availabilityOfMaybeNullElements[index][inputName])
                {
                    removedNames.Add(rulePattern.InputNames[i]);
                }
                else
                {
                    paramTypes.Add(TypesHelper.TypeName(rulePattern.Inputs[i]));
                    paramNames.Add(rulePattern.InputNames[i]);
                }
            }
            paramTypesArray = new String[paramTypes.Count];
            paramNamesArray = new String[paramNames.Count];
            for(int i = 0; i < paramTypes.Count; ++i)
            {
                paramTypesArray[i] = paramTypes[i];
                paramNamesArray[i] = paramNames[i];
            }
            suffixedMatcherName = "myMatch";
            foreach(String removedName in removedNames)
            {
                suffixedMatcherName += "_MissingPreset_"+removedName;
            }
        }

        /// <summary>
        /// Builds search program from scheduled search plan in pattern graph of the subpattern rule pattern
        /// </summary>
        public SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPMatchingPattern matchingPattern)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));

            PatternGraph patternGraph = matchingPattern.patternGraph;
            programType = SearchProgramType.Subpattern;
            this.model = model;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            patternGraphWithNestingPatterns.Push(patternGraph);
            negLevelNeverAboveMaxNegLevel = patternGraphWithNestingPatterns.Peek().maxNegLevel <= (int)LGSPElemFlags.MAX_NEG_LEVEL;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, true);

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfSubpattern(
                rulePatternClassName,
                matchingPattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                "myMatch");
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            insertionPoint = insertVariableDeclarations(insertionPoint, patternGraph);

            // initialize task/result-pushdown handling in subpattern matcher
            InitializeSubpatternMatching initialize = 
                new InitializeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Normal);
            insertionPoint = insertionPoint.Append(initialize);

            // start building with first operation in scheduled search plan
            indexOfSchedule = 0;
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);

            // finalize task/result-pushdown handling in subpattern matcher
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Normal);
            insertionPoint = insertionPoint.Append(finalize);

            patternGraphWithNestingPatterns.Pop();

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
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            this.alternative = alternative;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, !(matchingPattern is LGSPRulePattern));

            // build combined list of namesOfPatternGraphsOnPathToEnclosedPatternpath
            // from the namesOfPatternGraphsOnPathToEnclosedPatternpath of the alternative cases
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath = new List<string>();
            for (int i = 0; i < alternative.alternativeCases.Length; ++i)
            {
                PatternGraph altCase = alternative.alternativeCases[i];
                foreach (String name in altCase.patternGraphsOnPathToEnclosedPatternpath)
                {
                    if(!namesOfPatternGraphsOnPathToEnclosedPatternpath.Contains(name))
                        namesOfPatternGraphsOnPathToEnclosedPatternpath.Add(name);
                }
            }

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfAlternative(
                rulePatternClassName,
                namesOfPatternGraphsOnPathToEnclosedPatternpath,
                "myMatch");
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            // initialize task/result-pushdown handling in subpattern matcher
            InitializeSubpatternMatching initialize = 
                new InitializeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Normal);
            insertionPoint = insertionPoint.Append(initialize);

            // build alternative matching search programs, one per case
            for (int i=0; i<alternative.alternativeCases.Length; ++i)
            {
                PatternGraph altCase = alternative.alternativeCases[i];
                ScheduledSearchPlan scheduledSearchPlan = altCase.schedulesIncludingNegativesAndIndependents[0];

                GetPartialMatchOfAlternative matchAlternative = new GetPartialMatchOfAlternative(
                    scheduledSearchPlan.PatternGraph.pathPrefix, 
                    scheduledSearchPlan.PatternGraph.name,
                    rulePatternClassName);
                matchAlternative.OperationsList = new SearchProgramList(matchAlternative);
                SearchProgramOperation continuationPointAfterAltCase = insertionPoint.Append(matchAlternative);
                
                // at level of the current alt case
                insertionPoint = matchAlternative.OperationsList;
                insertionPoint = insertVariableDeclarations(insertionPoint, altCase);

                patternGraphWithNestingPatterns.Push(altCase);
                negLevelNeverAboveMaxNegLevel = patternGraphWithNestingPatterns.Peek().maxNegLevel <= (int)LGSPElemFlags.MAX_NEG_LEVEL;
                isNegative = false;
                isNestedInNegative = false;

                // start building with first operation in scheduled search plan

                indexOfSchedule = 0;
                BuildScheduledSearchPlanOperationIntoSearchProgram(
                    0,
                    insertionPoint);

                // back to level of alt cases
                insertionPoint = continuationPointAfterAltCase;

                // save matches found by alternative case to get clean start for matching next alternative case
                if(i<alternative.alternativeCases.Length-1)
                {
                    NewMatchesListForFollowingMatches newMatchesList =
                        new NewMatchesListForFollowingMatches(true);
                    insertionPoint = insertionPoint.Append(newMatchesList);
                }

                patternGraphWithNestingPatterns.Pop();
            }

            // finalize task/result-pushdown handling in subpattern matcher
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Normal);
            insertionPoint = insertionPoint.Append(finalize);

            return searchProgram;
        }

        /// <summary>
        /// Builds search program for iterated from scheduled search plan of iterated pattern graph
        /// </summary>
        public SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPMatchingPattern matchingPattern,
            PatternGraph iter)
        {
            programType = SearchProgramType.Iterated;
            this.model = model;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            patternGraphWithNestingPatterns.Push(iter);
            negLevelNeverAboveMaxNegLevel = patternGraphWithNestingPatterns.Peek().maxNegLevel <= (int)LGSPElemFlags.MAX_NEG_LEVEL;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, !(matchingPattern is LGSPRulePattern));

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfIterated(
                rulePatternClassName,
                matchingPattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                "myMatch");
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            insertionPoint = insertVariableDeclarations(insertionPoint, iter);

            // initialize task/result-pushdown handling in subpattern matcher for iteration
            InitializeSubpatternMatching initialize = 
                new InitializeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Iteration);
            insertionPoint = insertionPoint.Append(initialize);

            ReturnPreventingDummyIteration dummyIteration = new ReturnPreventingDummyIteration();
            SearchProgramOperation continuationPointAfterDummyIteration = insertionPoint.Append(dummyIteration);
            dummyIteration.NestedOperationsList = new SearchProgramList(dummyIteration);
            insertionPoint = dummyIteration.NestedOperationsList;

            // start building with first operation in scheduled search plan
            indexOfSchedule = 0;
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);

            insertionPoint = continuationPointAfterDummyIteration;

            // check whether iteration came to an end (pattern not found (again)) and handle it
            insertionPoint = insertEndOfIterationHandling(insertionPoint);

            // finalize task/result-pushdown handling in subpattern matcher for iteration
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Iteration);
            insertionPoint = insertionPoint.Append(finalize);

            patternGraphWithNestingPatterns.Pop();

            return searchProgram;
        }

        /// <summary>
        /// Inserts declarations for variables extracted from parameters and for def variables to be yielded to
        /// </summary>
        private SearchProgramOperation insertVariableDeclarations(SearchProgramOperation insertionPoint, PatternGraph patternGraph)
        {
            foreach(PatternVariable var in patternGraph.variables)
            {
                if(var.defToBeYieldedTo)
                {
                    String initializationExpression;
                    if(var.initialization != null)
                    {
                        SourceBuilder builder = new SourceBuilder();
                        var.initialization.Emit(builder);
                        initializationExpression = builder.ToString();
                    }
                    else
                    {
                        string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(var.Type), model);
                        initializationExpression = TypesHelper.DefaultValueString(typeName, model);
                    }
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Variable, TypesHelper.TypeName(var.Type), var.Name, 
                            initializationExpression)
                    );
                }
                else
                {
                    insertionPoint = insertionPoint.Append(
                        new ExtractVariable(TypesHelper.TypeName(var.Type), var.Name)
                    );
                }
            }

            foreach(PatternNode node in patternGraph.nodes)
            {
                if(node.defToBeYieldedTo)
                {
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Node, "GRGEN_LGSP.LGSPNode", node.Name, "null")
                    );
                }
            }

            foreach(PatternEdge edge in patternGraph.edges)
            {
                if(edge.defToBeYieldedTo)
                {
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Edge, "GRGEN_LGSP.LGSPEdge", edge.Name, "null")
                    );
                }
            }

            return insertionPoint;
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
        /// the pattern graph to build with its nesting patterns
        /// </summary>
        private Stack<PatternGraph> patternGraphWithNestingPatterns;

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
        /// types of the parameters of the action (null if not an action)
        /// </summary>
        private string[] parameterTypes;
       
        /// <summary>
        /// names of the parameters of the action (null if not an action)
        /// </summary>
        private string[] parameterNames;

        /// <summary>
        /// true if statically determined that the neg level of the pattern getting constructed 
        /// is always below the maximum neg level
        /// </summary>
        private bool negLevelNeverAboveMaxNegLevel;

        /// <summary>
        /// name says everything
        /// </summary>
        private const int MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID = 2;

        /// <summary>
        /// The index of the currently built schedule
        /// </summary>
        private int indexOfSchedule;

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
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            if (indexOfScheduledSearchPlanOperationToBuild >=
                patternGraph.schedulesIncludingNegativesAndIndependents[indexOfSchedule].Operations.Length)
            { // end of scheduled search plan reached, stop recursive iteration
                return buildMatchComplete(insertionPointWithinSearchProgram);
            }

            SearchOperation op = patternGraph.schedulesIncludingNegativesAndIndependents[indexOfSchedule].
                Operations[indexOfScheduledSearchPlanOperationToBuild];

            // for current scheduled search plan operation 
            // insert corresponding search program operations into search program
            switch (op.Type)
            {
                case SearchOperationType.Void:
                case SearchOperationType.DefToBeYieldedTo:
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

                case SearchOperationType.Lookup:
                    return buildLookup(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.PickFromStorage:
                    return buildPickFromStorage(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.Isomorphy);

                case SearchOperationType.PickFromStorageAttribute:
                    return buildPickFromStorageAttribute(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.StorageAttribute,
                        op.Isomorphy);

                case SearchOperationType.Cast:
                    return buildCast(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.MapWithStorage:
                    return buildMapWithStorage(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Storage,
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

                default:
                    Debug.Assert(false, "Unknown search operation");
                    return insertionPointWithinSearchProgram;
            }
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

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
                insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
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
            if (programType==SearchProgramType.Subpattern 
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
                            negLevelNeverAboveMaxNegLevel);
                    insertionPoint = insertionPoint.Append(checkIsomorphy);
                }
            }

            // check candidate for pattern path isomorphy
            if (patternGraphWithNestingPatterns.Peek().isPatternGraphOnPathFromEnclosingPatternpath)
            {
                CheckCandidateForIsomorphyPatternPath checkIsomorphy =
                    new CheckCandidateForIsomorphyPatternPath(
                        target.PatternElement.Name,
                        isNode,
                        patternGraphWithNestingPatterns.Peek().isPatternpathLocked,
                        getCurrentLastMatchAtPreviousNestingLevel());
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
        /// PickFromStorage search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPickFromStorage(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode target,
            PatternVariable storage,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = !TypesHelper.DotNetTypeToXgrsType(storage.Type).StartsWith("array");
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // iterate available storage elements
            string iterationType;
            if(isDict) iterationType = "System.Collections.Generic.KeyValuePair<" 
                + TypesHelper.GetStorageKeyTypeName(storage.Type) + ","
                + TypesHelper.GetStorageValueTypeName(storage.Type) + ">";
            else
                iterationType = TypesHelper.GetStorageKeyTypeName(storage.Type);
            GetCandidateByIteration elementsIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.StorageElements,
                    target.PatternElement.Name,
                    storage.Name,
                    iterationType,
                    isDict,
                    isNode);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, out continuationPointAfterConnectednessCheck);
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
                        negLevelNeverAboveMaxNegLevel);
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
                            negLevelNeverAboveMaxNegLevel);
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
                        getCurrentLastMatchAtPreviousNestingLevel());
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
            if(isomorphy.SetIsMatchedBit)
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
            // continue at the end of the list after storage iteration nesting level
            insertionPoint = continuationPoint;

            return insertionPoint;
        }

        /// <summary>
        /// Search program operations implementing the
        /// PickFromStorageAttribute search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildPickFromStorageAttribute(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            AttributeType storageAttribute,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = storageAttribute.Kind != AttributeKind.ArrayAttr;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // iterate available storage elements
            string iterationType;
            if(isDict)
                if(storageAttribute.Kind == AttributeKind.SetAttr)
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storageAttribute.ValueType.GetKindName(), model) + ","
                        + "de.unika.ipd.grGen.libGr.SetValueType" + ">";
                }
                else
                {
                    iterationType = "System.Collections.Generic.KeyValuePair<"
                        + TypesHelper.XgrsTypeToCSharpType(storageAttribute.KeyType.GetKindName(), model) + ","
                        + TypesHelper.XgrsTypeToCSharpType(storageAttribute.ValueType.GetKindName(), model) + ">";
                }
            else
                iterationType = TypesHelper.XgrsTypeToCSharpType(storageAttribute.ValueType.GetKindName(), model);
 
            GetCandidateByIteration elementsIteration =
                new GetCandidateByIteration(
                    GetCandidateByIterationType.StorageAttributeElements,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    source.PatternElement.typeName,
                    storageAttribute.Name,
                    iterationType,
                    isDict,
                    isNode);
            SearchProgramOperation continuationPoint =
                insertionPoint.Append(elementsIteration);
            elementsIteration.NestedOperationsList =
                new SearchProgramList(elementsIteration);
            insertionPoint = elementsIteration.NestedOperationsList;

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, out continuationPointAfterConnectednessCheck);
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
                        negLevelNeverAboveMaxNegLevel);
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
                            negLevelNeverAboveMaxNegLevel);
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
                        getCurrentLastMatchAtPreviousNestingLevel());
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
            if(isomorphy.SetIsMatchedBit)
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
            // continue at the end of the list after storage iteration nesting level
            insertionPoint = continuationPoint;

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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // get candidate from other element (the cast is simply the following type check)
            GetCandidateByDrawing fromOtherElementForCast =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromOtherElementForCast,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(fromOtherElementForCast);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, out continuationPointAfterConnectednessCheck);
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
                        negLevelNeverAboveMaxNegLevel);
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
                            negLevelNeverAboveMaxNegLevel);
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
                        getCurrentLastMatchAtPreviousNestingLevel());
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
            if(isomorphy.SetIsMatchedBit)
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
        /// MapWithStorage search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildMapWithStorage(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            SearchPlanNode source,
            SearchPlanNode target,
            PatternVariable storage,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // get candidate from storage-map, only creates variable to hold it, get is fused with check for map membership
            GetCandidateByDrawing elementFromStorage = 
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.MapWithStorage,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    storage.Name,
                    TypesHelper.GetStorageValueTypeName(storage.Type),
                    isNode);
            insertionPoint = insertionPoint.Append(elementFromStorage);

            // check existence of candidate in storage map 
            CheckCandidateMapWithStorage checkElementInStorage =
                new CheckCandidateMapWithStorage(
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    storage.Name,
                    TypesHelper.GetStorageKeyTypeName(storage.Type),
                    isNode);
            insertionPoint = insertionPoint.Append(checkElementInStorage);

            // check type of candidate
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointAfterConnectednessCheck;
            if(isNode)
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanNodeNode)target, out continuationPointAfterConnectednessCheck);
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
                    insertionPoint, (SearchPlanEdgeNode)target, out continuationPointAfterConnectednessCheck);
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
                        negLevelNeverAboveMaxNegLevel);
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
                            negLevelNeverAboveMaxNegLevel);
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
                        getCurrentLastMatchAtPreviousNestingLevel());
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
            if(isomorphy.SetIsMatchedBit)
            { // only if isomorphy information was previously written
                AbandonCandidate abandonCandidate =
                    new AbandonCandidate(
                        target.PatternElement.Name,
                        negativeIndependentNamePrefix,
                        isNode,
                        negLevelNeverAboveMaxNegLevel);
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            if (programType == SearchProgramType.Subpattern 
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
                            negLevelNeverAboveMaxNegLevel);
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
                        getCurrentLastMatchAtPreviousNestingLevel());
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            if (programType == SearchProgramType.Subpattern
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
                            negLevelNeverAboveMaxNegLevel);
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
                        getCurrentLastMatchAtPreviousNestingLevel());
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
            foreach (SearchOperation op in negativePatternGraph.schedulesIncludingNegativesAndIndependents[0].Operations)
            {
                if (op.Type == SearchOperationType.NegIdptPreset)
                {
                    ++numberOfNeededElements;
                }
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach(SearchOperation op in negativePatternGraph.schedulesIncludingNegativesAndIndependents[0].Operations)
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

            bool enclosingIsNegative = isNegative;
            bool enclosingIsNestedInNegative = isNestedInNegative;
            isNegative = true;
            isNestedInNegative = true;
            PatternGraph enclosingPatternGraph = patternGraphWithNestingPatterns.Peek();
            patternGraphWithNestingPatterns.Push(negativePatternGraph);
            negLevelNeverAboveMaxNegLevel = patternGraphWithNestingPatterns.Peek().maxNegLevel <= (int)LGSPElemFlags.MAX_NEG_LEVEL;

            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            bool negativeContainsSubpatterns = negativePatternGraph.EmbeddedGraphs.Length >= 1
                || negativePatternGraph.Alternatives.Length >= 1
                || negativePatternGraph.Iterateds.Length >= 1;
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
            patternGraphWithNestingPatterns.Pop();
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
            foreach(SearchOperation op in independentPatternGraph.schedulesIncludingNegativesAndIndependents[0].Operations)
            {
                if (op.Type == SearchOperationType.NegIdptPreset)
                {
                    ++numberOfNeededElements;
                }
            }
            string[] neededElements = new string[numberOfNeededElements];
            int i = 0;
            foreach(SearchOperation op in independentPatternGraph.schedulesIncludingNegativesAndIndependents[0].Operations)
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

            bool enclosingIsNegative = isNegative;
            isNegative = false;
            PatternGraph enclosingPatternGraph = patternGraphWithNestingPatterns.Peek();
            patternGraphWithNestingPatterns.Push(independentPatternGraph);
            negLevelNeverAboveMaxNegLevel = patternGraphWithNestingPatterns.Peek().maxNegLevel <= (int)LGSPElemFlags.MAX_NEG_LEVEL;

            string independentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
            bool independentContainsSubpatterns = independentPatternGraph.EmbeddedGraphs.Length >= 1
                || independentPatternGraph.Alternatives.Length >= 1
                || independentPatternGraph.Iterateds.Length >= 1;
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
                new CheckContinueMatchingOfIndependentFailed(checkIndependent, independentPatternGraph.isIterationBreaking);
            checkIndependent.CheckIndependentFailed = abortMatching;
            insertionPoint = insertionPoint.Append(abortMatching);

            patternGraphWithNestingPatterns.Pop();
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
                    getCurrentMatchOfNestingPattern()
                );
            insertionPoint = insertionPoint.Append(pushMatch);

            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Patternpath, false);

            insertionPoint = insertPatternpathAccept(insertionPoint, patternGraph);

            //---------------------------------------------------------------------------
            // build next operation
            insertionPoint = BuildScheduledSearchPlanOperationIntoSearchProgram(
                currentOperationIndex + 1,
                insertionPoint);
            //---------------------------------------------------------------------------

            insertionPoint = insertPatternpathAbandon(insertionPoint, patternGraph);

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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // is subpattern gives information about type of top level enclosing pattern (action vs. subpattern)
            // contains subpatterns gives informations about current pattern graph (might be negative, independent itself, too)
            bool isSubpattern = programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated;
            bool containsSubpatterns = patternGraph.embeddedGraphs.Length >= 1
                || patternGraph.alternatives.Length >= 1
                || patternGraph.iterateds.Length >= 1;

            // increase iterated matched counter if it is iterated
            if(programType==SearchProgramType.Iterated) {
                AcceptIterated acceptIterated = new AcceptIterated();
                insertionPoint = insertionPoint.Append(acceptIterated);
            }

            // push subpattern tasks (in case there are some)
            if (containsSubpatterns)
                insertionPoint = insertPushSubpatternTasks(insertionPoint);

            // if this is a subpattern without subpatterns
            // it may be the last to be matched - handle that case
            if (!containsSubpatterns && isSubpattern && programType!=SearchProgramType.Iterated && negativeIndependentNamePrefix=="")
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
                    insertionPoint = insertCheckForSubpatternsFound(insertionPoint, false);
                else // top-level-pattern of action without subpatterns
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

            // decrease iterated matched counter again if it is iterated
            if (programType == SearchProgramType.Iterated)
            {
                AbandonIterated abandonIterated = new AbandonIterated();
                insertionPoint = insertionPoint.Append(abandonIterated);
            }

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
                    "GRGEN_MODEL." + model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
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
                        "GRGEN_MODEL." + model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
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
                            "GRGEN_MODEL." + model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
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
                            "GRGEN_MODEL." + model.NodeModel.Types[currentNode.PatternElement.TypeID].Name,
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
        /// the types of match objects there are, to be filled by insertMatchObjectBuilding
        /// </summary>
        enum MatchObjectType
        {
            Normal,
            Independent,
            Patternpath
        }

        /// <summary>
        /// Inserts code to build the match object
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertMatchObjectBuilding(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            MatchObjectType matchObjectType,
            bool defElements)
        {
            String matchObjectName;
            if(matchObjectType==MatchObjectType.Independent) {
                matchObjectName = NamesOfEntities.MatchedIndependentVariable(patternGraph.pathPrefix + patternGraph.name);
            } else if(matchObjectType==MatchObjectType.Patternpath) {
                matchObjectName = NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
            } else { //if(matchObjectType==MatchObjectType.Normal)
                matchObjectName = "match";
            }
            String enumPrefix = patternGraph.pathPrefix + patternGraph.name + "_";

            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                // in defElements pass only def elements, in non defElements pass only non def elements
                if(defElements == patternGraph.nodes[i].defToBeYieldedTo)
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
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                // in defElements pass only def elements, in non defElements pass only non def elements
                if(defElements == patternGraph.edges[i].defToBeYieldedTo)
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
            }

            // only nodes and edges for patternpath matches
            if (matchObjectType == MatchObjectType.Patternpath) {
                return insertionPoint;
            }

            foreach (PatternVariable var in patternGraph.variables)
            {
                // in defElements pass only def elements, in non defElements pass only non def elements
                if(defElements == var.defToBeYieldedTo)
                {
                    BuildMatchObject buildMatch =
                        new BuildMatchObject(
                            BuildMatchObjectType.Variable,
                            var.Type.Type.FullName,
                            var.UnprefixedName,
                            var.Name,
                            rulePatternClassName,
                            enumPrefix,
                            matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }

            // only nodes, edges, variables in defElements pass
            if(defElements) {
                return insertionPoint;
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
            for (int i = 0; i < patternGraph.iterateds.Length; ++i)
            {
                BuildMatchObject buildMatch =
                    new BuildMatchObject(
                        BuildMatchObjectType.Iteration,
                        patternGraph.pathPrefix + patternGraph.name + "_" + patternGraph.iterateds[i].iteratedPattern.name,
                        patternGraph.iterateds[i].iteratedPattern.name,
                        patternGraph.iterateds[i].iteratedPattern.name,
                        rulePatternClassName,
                        enumPrefix,
                        matchObjectName,
                        patternGraph.embeddedGraphs.Length
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

            // we only have to take care of yielding in case of normal and independent matches 
            // and if we contain a def entity (and if we are not in the defElements pass)
            if(matchObjectType != MatchObjectType.Patternpath
                && patternGraph.isDefEntityExisting)
            {
                return insertYields(insertionPoint, patternGraph, matchObjectName);
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
            String matchObjectName)
        {
            // the match object is built now with our local elements
            // and the match objects of our children
            // now 1. copy all def entities we share with children to us

            foreach(PatternGraphEmbedding patternEmbedding in patternGraph.embeddedGraphs)
            {
                // if the pattern embedding does not contain a def entity it can't yield to us
                if(!((PatternGraph)patternEmbedding.EmbeddedGraph).isDefEntityExisting)
                    continue;

                LGSPMatchingPattern matchingPattern = patternEmbedding.matchingPatternOfEmbeddedGraph;
                String nestedMatchObjectName = matchObjectName + "._" + patternEmbedding.Name;
                Debug.Assert(matchingPattern.defNames.Length == patternEmbedding.yields.Length);
                for(int i = 0; i < patternEmbedding.yields.Length; ++i)
                {
                    BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                        toBubbleUpYieldAssignmentType(matchingPattern.defs[i]),
                        patternEmbedding.yields[i],
                        nestedMatchObjectName,
                        matchingPattern.defNames[i]
                        );
                    insertionPoint = insertionPoint.Append(bubble);
                }
            }

            foreach(Iterated iterated in patternGraph.iterateds)
            {
                // if the iterated does not contain a non local def entity it can't yield to us
                if(!iterated.iteratedPattern.isNonLocalDefEntityExisting)
                    continue;

                String nestedMatchObjectName = matchObjectName + "._" + iterated.iteratedPattern.Name;
                BubbleUpYieldIterated bubbleUpIterated = new BubbleUpYieldIterated(nestedMatchObjectName);
                bubbleUpIterated.NestedOperationsList = new SearchProgramList(bubbleUpIterated);
                SearchProgramOperation continuationPoint = insertionPoint.Append(bubbleUpIterated);
                insertionPoint = bubbleUpIterated.NestedOperationsList;

                insertYieldAssignments(insertionPoint,
                    patternGraph, nestedMatchObjectName+".Root", iterated.iteratedPattern);

                insertionPoint = continuationPoint;
            }
            foreach(Alternative alternative in patternGraph.alternatives)
            {
                bool first = true;
                foreach(PatternGraph alternativeCase in alternative.alternativeCases)
                {
                    // if the alternative case does not contain a non local def entity it can't yield to us
                    if(!alternativeCase.isNonLocalDefEntityExisting)
                        continue;

                    String nestedMatchObjectName = "_" + alternative.name;
                    BubbleUpYieldAlternativeCase bubbleUpAlternativeCase = 
                        new BubbleUpYieldAlternativeCase(
                            matchObjectName,
                            nestedMatchObjectName,
                            rulePatternClassName + ".Match_" + patternGraph.pathPrefix + patternGraph.name + "_" + alternative.name + "_" + alternativeCase.name,
                            "altCaseMatch",
                            first);
                    bubbleUpAlternativeCase.NestedOperationsList = new SearchProgramList(bubbleUpAlternativeCase);
                    SearchProgramOperation continuationPoint = insertionPoint.Append(bubbleUpAlternativeCase);
                    insertionPoint = bubbleUpAlternativeCase.NestedOperationsList;
                    first = false;

                    insertYieldAssignments(insertionPoint,
                        patternGraph, "altCaseMatch", alternativeCase);

                    insertionPoint = continuationPoint;
                }
            }

            foreach(PatternGraph independent in patternGraph.independentPatternGraphs)
            {
                // if the independent does not contain a non local def entity it can't yield to us
                if(!independent.isNonLocalDefEntityExisting)
                    continue;

                String nestedMatchObjectName = NamesOfEntities.MatchedIndependentVariable(independent.pathPrefix + independent.name); ;

                insertionPoint = insertYieldAssignments(insertionPoint,
                    patternGraph, nestedMatchObjectName, independent);
            }

            //////////////////////////////////////////////////////
            // then 2. compute all local yields
            foreach(PatternYielding yielding in patternGraph.Yieldings)
            {
                // iterated potentially matching more than once can't be bubbled up normally,
                // they need accumulation with a for loop into a variable of the nesting pattern, 
                // that's done in/with the yield statements of the parent
                if(yielding.YieldAssignment is IteratedAccumulationYield)
                {
                    IteratedAccumulationYield accumulationYield = (IteratedAccumulationYield)yielding.YieldAssignment;
                    foreach(Iterated iterated in patternGraph.iterateds)
                    {
                        // skip the iterateds we're not interested in
                        if(accumulationYield.Iterated != iterated.iteratedPattern.Name)
                            continue;

                        String nestedMatchObjectName = matchObjectName + "._" + iterated.iteratedPattern.Name;
                        AccumulateUpYieldIterated accumulateUpIterated = 
                            new AccumulateUpYieldIterated(
                                nestedMatchObjectName,
                                rulePatternClassName + ".Match_" + patternGraph.pathPrefix + patternGraph.name + "_" + iterated.iteratedPattern.name,
                                "iteratedMatch");
                        accumulateUpIterated.NestedOperationsList = new SearchProgramList(accumulateUpIterated);
                        SearchProgramOperation continuationPoint = insertionPoint.Append(accumulateUpIterated);
                        insertionPoint = accumulateUpIterated.NestedOperationsList;

                        accumulationYield.IteratedMatchVariable = "iteratedMatch._" + NamesOfEntities.Variable(accumulationYield.UnprefixedVariable);
                        accumulationYield.ReplaceVariableByIterationVariable(accumulationYield);

                        SourceBuilder yieldAssignmentSource = new SourceBuilder();
                        accumulationYield.Emit(yieldAssignmentSource);
                        LocalYieldAssignment yieldAssignment =
                            new LocalYieldAssignment(yieldAssignmentSource.ToString());
                        insertionPoint = insertionPoint.Append(yieldAssignment);

                        insertionPoint = continuationPoint;
                    }
                }
                else
                {
                    SourceBuilder yieldAssignmentSource = new SourceBuilder();
                    yielding.YieldAssignment.Emit(yieldAssignmentSource);
                    LocalYieldAssignment yieldAssignment =
                        new LocalYieldAssignment(yieldAssignmentSource.ToString());
                    insertionPoint = insertionPoint.Append(yieldAssignment);
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Inserts code for bubbling yield assignments
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertYieldAssignments(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            String nestedMatchObjectName,
            PatternGraph nestedPatternGraph)
        {
            foreach(PatternNode node in patternGraph.nodes)
            {
                if(!node.DefToBeYieldedTo)
                    continue;

                foreach(PatternNode nestedNode in nestedPatternGraph.nodes)
                {
                    if(nestedNode == node)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                            EntityType.Node,
                            node.Name,
                            nestedMatchObjectName,
                            nestedNode.UnprefixedName
                            );
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }

            foreach(PatternEdge edge in patternGraph.edges)
            {
                if(!edge.DefToBeYieldedTo)
                    continue;

                foreach(PatternEdge nestedEdge in nestedPatternGraph.edges)
                {
                    if(nestedEdge == edge)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                            EntityType.Edge,
                            edge.Name,
                            nestedMatchObjectName,
                            nestedEdge.UnprefixedName
                            );
                        insertionPoint = insertionPoint.Append(bubble);
                    }
                }
            }
            
            foreach(PatternVariable var in patternGraph.variables)
            {
                if(!var.DefToBeYieldedTo)
                    continue;

                foreach(PatternVariable nestedVar in nestedPatternGraph.variables)
                {
                    if(nestedVar == var)
                    {
                        BubbleUpYieldAssignment bubble = new BubbleUpYieldAssignment(
                            EntityType.Variable,
                            var.Name,
                            nestedMatchObjectName,
                            nestedVar.UnprefixedName
                            );
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
        private SearchProgramOperation insertPushSubpatternTasks(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            string searchPatternpath = "false";
            string matchOfNestingPattern = "null";
            string lastMatchAtPreviousNestingLevel = "null";

            if(patternGraph.patternGraphsOnPathToEnclosedPatternpath.Count > 0)
            {
                if(patternGraph.isPatternpathLocked) searchPatternpath = "true";
                if((programType == SearchProgramType.Subpattern || programType == SearchProgramType.AlternativeCase || programType == SearchProgramType.Iterated)
                    && patternGraphWithNestingPatterns.Count == 1)
                {
                    searchPatternpath = "searchPatternpath";
                }
                matchOfNestingPattern = NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
                lastMatchAtPreviousNestingLevel = getCurrentLastMatchAtPreviousNestingLevel();
            }

            // first alternatives, so that they get processed last
            // to handle subpatterns in linear order we've to push them in reverse order on the stack
            for (int i = patternGraph.alternatives.Length - 1; i >= 0; --i)
            {
                Alternative alternative = patternGraph.alternatives[i];

                Dictionary<string, bool> neededNodes = new Dictionary<string, bool>();
                Dictionary<string, bool> neededEdges = new Dictionary<string, bool>();
                Dictionary<string, GrGenType> neededVariables = new Dictionary<string, GrGenType>();
                foreach(PatternGraph altCase in alternative.alternativeCases)
                {
                    foreach (KeyValuePair<string, bool> neededNode in altCase.neededNodes)
                        neededNodes[neededNode.Key] = neededNode.Value;
                    foreach (KeyValuePair<string, bool> neededEdge in altCase.neededEdges)
                        neededEdges[neededEdge.Key] = neededEdge.Value;
                    foreach(KeyValuePair<string, GrGenType> neededVariable in altCase.neededVariables)
                        neededVariables[neededVariable.Key] = neededVariable.Value;
                }

                int numElements = neededNodes.Count + neededEdges.Count + neededVariables.Count;
                string[] connectionName = new string[numElements];
                string[] argumentExpressions = new string[numElements];
                int j = 0;
                foreach (KeyValuePair<string, bool> node in neededNodes)
                {
                    connectionName[j] = node.Key;
                    SourceBuilder argumentExpression = new SourceBuilder();
                    (new GraphEntityExpression(node.Key)).Emit(argumentExpression);
                    argumentExpressions[j] = argumentExpression.ToString();
                    ++j;
                }
                foreach (KeyValuePair<string, bool> edge in neededEdges)
                {
                    connectionName[j] = edge.Key;
                    SourceBuilder argumentExpression = new SourceBuilder();
                    (new GraphEntityExpression(edge.Key)).Emit(argumentExpression);
                    argumentExpressions[j] = argumentExpression.ToString();
                    ++j;
                }
                foreach(KeyValuePair<string, GrGenType> variable in neededVariables)
                {
                    connectionName[j] = variable.Key;
                    SourceBuilder argumentExpression = new SourceBuilder();
                    (new VariableExpression(variable.Key)).Emit(argumentExpression);
                    argumentExpressions[j] = argumentExpression.ToString();
                    ++j;
                }

                PushSubpatternTask pushTask =
                    new PushSubpatternTask(
                        PushAndPopSubpatternTaskTypes.Alternative,
                        alternative.pathPrefix,
                        alternative.name,
                        rulePatternClassName,
                        connectionName,
                        argumentExpressions,
                        negativeIndependentNamePrefix,
                        searchPatternpath,
                        matchOfNestingPattern,
                        lastMatchAtPreviousNestingLevel
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            // then iterated patterns of the pattern
            // to handle iterated in linear order we've to push them in reverse order on the stack
            for (int i = patternGraph.iterateds.Length - 1; i >= 0; --i)
            {
                PatternGraph iter = patternGraph.iterateds[i].iteratedPattern;

                int numElements = iter.neededNodes.Count + iter.neededEdges.Count + iter.neededVariables.Count;
                string[] connectionName = new string[numElements];
                string[] argumentExpressions = new string[numElements]; 
                int j = 0;
                foreach (KeyValuePair<string, bool> node in iter.neededNodes)
                {
                    connectionName[j] = node.Key;
                    SourceBuilder argumentExpression = new SourceBuilder();
                    (new GraphEntityExpression(node.Key)).Emit(argumentExpression);
                    argumentExpressions[j] = argumentExpression.ToString();
                    ++j;
                }
                foreach (KeyValuePair<string, bool> edge in iter.neededEdges)
                {
                    connectionName[j] = edge.Key;
                    SourceBuilder argumentExpression = new SourceBuilder();
                    (new GraphEntityExpression(edge.Key)).Emit(argumentExpression);
                    argumentExpressions[j] = argumentExpression.ToString();
                    ++j;
                }
                foreach(KeyValuePair<string, GrGenType> variable in iter.neededVariables)
                {
                    connectionName[j] = variable.Key;
                    SourceBuilder argumentExpression = new SourceBuilder();
                    (new VariableExpression(variable.Key)).Emit(argumentExpression);
                    argumentExpressions[j] = argumentExpression.ToString();
                    ++j;
                }

                PushSubpatternTask pushTask =
                    new PushSubpatternTask(
                        PushAndPopSubpatternTaskTypes.Iterated,
                        iter.pathPrefix,
                        iter.name,
                        rulePatternClassName,
                        connectionName,
                        argumentExpressions,
                        negativeIndependentNamePrefix,
                        searchPatternpath,
                        matchOfNestingPattern,
                        lastMatchAtPreviousNestingLevel
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            // and finally subpatterns of the pattern
            // to handle subpatterns in linear order we've to push them in reverse order on the stack
            for (int i = patternGraph.embeddedGraphs.Length - 1; i >= 0; --i)
            {
                PatternGraphEmbedding subpattern = patternGraph.embeddedGraphs[i];
                Debug.Assert(subpattern.matchingPatternOfEmbeddedGraph.inputNames.Length == subpattern.connections.Length);
                string[] connectionName = subpattern.matchingPatternOfEmbeddedGraph.inputNames;
                string[] argumentExpressions = new string[subpattern.connections.Length];
                for (int j = 0; j < subpattern.connections.Length; ++j)
                {
                    SourceBuilder argumentExpression = new SourceBuilder();
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
                        lastMatchAtPreviousNestingLevel
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
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

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

            foreach (Iterated iterated in patternGraph.iterateds)
            {
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Iterated,
                        iterated.iteratedPattern.name,
                        iterated.iteratedPattern.pathPrefix
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
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            CheckContinueMatchingTasksLeft tasksLeft =
                new CheckContinueMatchingTasksLeft();
            SearchProgramOperation continuationPointAfterTasksLeft =
                insertionPoint.Append(tasksLeft);
            tasksLeft.CheckFailedOperations =
                new SearchProgramList(tasksLeft);
            insertionPoint = tasksLeft.CheckFailedOperations;

            // ---- check failed, no tasks left, leaf subpattern was matched
            LeafSubpatternMatched leafMatched = new LeafSubpatternMatched(
                rulePatternClassName, patternGraph.pathPrefix+patternGraph.name, false);
            SearchProgramOperation continuationPointAfterLeafMatched =
                insertionPoint.Append(leafMatched);
            leafMatched.MatchBuildingOperations =
                new SearchProgramList(leafMatched);
            insertionPoint = leafMatched.MatchBuildingOperations;

            // ---- ---- fill the match object with the candidates 
            // ---- ---- which have passed all the checks for being a match
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, false);
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, true);

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
            bool isAction = programType == SearchProgramType.Action;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                if (patternGraph.nodes[i].PointOfDefinition == patternGraph
                    || patternGraph.nodes[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodes[i].defToBeYieldedTo && !patternGraph.totallyHomomorphicNodes[i])
                    {
                        AcceptCandidateGlobal acceptGlobal =
                            new AcceptCandidateGlobal(patternGraph.nodes[i].name,
                            negativeIndependentNamePrefix,
                            true,
                            negLevelNeverAboveMaxNegLevel);
                        insertionPoint = insertionPoint.Append(acceptGlobal);
                    }
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                if (patternGraph.edges[i].PointOfDefinition == patternGraph
                    || patternGraph.edges[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edges[i].defToBeYieldedTo && !patternGraph.totallyHomomorphicEdges[i])
                    {
                        AcceptCandidateGlobal acceptGlobal =
                            new AcceptCandidateGlobal(patternGraph.edges[i].name,
                            negativeIndependentNamePrefix,
                            false,
                            negLevelNeverAboveMaxNegLevel);
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
        private SearchProgramOperation insertPatternpathAccept(SearchProgramOperation insertionPoint,
            PatternGraph patternGraph)
        {
            bool isAction = programType == SearchProgramType.Action;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                if (patternGraph.nodes[i].PointOfDefinition == patternGraph
                    || patternGraph.nodes[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodes[i].defToBeYieldedTo)
                    {
                        AcceptCandidatePatternpath acceptPatternpath =
                            new AcceptCandidatePatternpath(patternGraph.nodes[i].name,
                            negativeIndependentNamePrefix,
                            true);
                        insertionPoint = insertionPoint.Append(acceptPatternpath);
                    }
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                if (patternGraph.edges[i].PointOfDefinition == patternGraph
                    || patternGraph.edges[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edges[i].defToBeYieldedTo)
                    {
                        AcceptCandidatePatternpath acceptPatternpath =
                            new AcceptCandidatePatternpath(patternGraph.edges[i].name,
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
        private SearchProgramOperation insertGlobalAbandon(SearchProgramOperation insertionPoint)
        {
            bool isAction = programType == SearchProgramType.Action;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // global abandon of all candidate elements (remove isomorphy information)
            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                if (patternGraph.nodes[i].PointOfDefinition == patternGraph
                    || patternGraph.nodes[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodes[i].defToBeYieldedTo && !patternGraph.totallyHomomorphicNodes[i])
                    {
                        AbandonCandidateGlobal abandonGlobal =
                            new AbandonCandidateGlobal(patternGraph.nodes[i].name,
                            negativeIndependentNamePrefix,
                            true,
                            negLevelNeverAboveMaxNegLevel);
                        insertionPoint = insertionPoint.Append(abandonGlobal);
                    }
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                if (patternGraph.edges[i].PointOfDefinition == patternGraph
                    || patternGraph.edges[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edges[i].defToBeYieldedTo && !patternGraph.totallyHomomorphicEdges[i])
                    {
                        AbandonCandidateGlobal abandonGlobal =
                            new AbandonCandidateGlobal(patternGraph.edges[i].name,
                            negativeIndependentNamePrefix,
                            false,
                            negLevelNeverAboveMaxNegLevel);
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
        private SearchProgramOperation insertPatternpathAbandon(SearchProgramOperation insertionPoint,
            PatternGraph patternGraph)
        {
            bool isAction = programType == SearchProgramType.Action;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // patternpath abandon of all candidate elements (remove isomorphy information)
            for (int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                if (patternGraph.nodes[i].PointOfDefinition == patternGraph
                    || patternGraph.nodes[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.nodes[i].defToBeYieldedTo)
                    {
                        AbandonCandidatePatternpath abandonPatternpath =
                            new AbandonCandidatePatternpath(patternGraph.nodes[i].name,
                            negativeIndependentNamePrefix,
                            true);
                        insertionPoint = insertionPoint.Append(abandonPatternpath);
                    }
                }
            }
            for (int i = 0; i < patternGraph.edges.Length; ++i)
            {
                if (patternGraph.edges[i].PointOfDefinition == patternGraph
                    || patternGraph.edges[i].PointOfDefinition == null && isAction)
                {
                    if(!patternGraph.edges[i].defToBeYieldedTo)
                    {
                        AbandonCandidatePatternpath abandonPatternpath =
                            new AbandonCandidatePatternpath(patternGraph.edges[i].name,
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
        private SearchProgramOperation insertCheckForSubpatternsFound(SearchProgramOperation insertionPoint, 
            bool isIteratedNullMatch)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // check whether there were no subpattern matches found
            CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                new CheckPartialMatchForSubpatternsFound(negativeIndependentNamePrefix, patternGraph.isIterationBreaking);
            SearchProgramOperation continuationPointAfterSubpatternsFound =
                   insertionPoint.Append(checkSubpatternsFound);
            checkSubpatternsFound.CheckFailedOperations =
                new SearchProgramList(checkSubpatternsFound);
            insertionPoint = checkSubpatternsFound.CheckFailedOperations;

            // ---- check failed, some subpattern matches found, pattern and subpatterns were matched
            PatternAndSubpatternsMatchedType type = PatternAndSubpatternsMatchedType.SubpatternOrAlternative;
            if (programType == SearchProgramType.Action) {
                type = PatternAndSubpatternsMatchedType.Action;
            } else if (programType == SearchProgramType.Iterated) {
                if (isIteratedNullMatch) type = PatternAndSubpatternsMatchedType.IteratedNullMatch;
                else type = PatternAndSubpatternsMatchedType.Iterated;
            }
            Debug.Assert(!isIteratedNullMatch || programType == SearchProgramType.Iterated);
            PatternAndSubpatternsMatched patternAndSubpatternsMatched = 
                new PatternAndSubpatternsMatched(rulePatternClassName, patternGraph.pathPrefix + patternGraph.name, type);
            SearchProgramOperation continuationPointAfterPatternAndSubpatternsMatched =
                insertionPoint.Append(patternAndSubpatternsMatched);
            patternAndSubpatternsMatched.MatchBuildingOperations =
                new SearchProgramList(patternAndSubpatternsMatched);
            insertionPoint = patternAndSubpatternsMatched.MatchBuildingOperations;

            // ---- ---- fill the match object with the candidates 
            // ---- ---- which have passed all the checks for being a match
            if (!isIteratedNullMatch)
            {
                insertionPoint = insertMatchObjectBuilding(insertionPoint,
                    patternGraph, MatchObjectType.Normal, false);
                insertionPoint = insertMatchObjectBuilding(insertionPoint,
                    patternGraph, MatchObjectType.Normal, true);
            }

            // ---- nesting level up
            insertionPoint = continuationPointAfterPatternAndSubpatternsMatched;

            // ---- create new matches list to search on or copy found matches into own matches list
            if (programType==SearchProgramType.Subpattern 
                || programType==SearchProgramType.AlternativeCase)
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
        private SearchProgramOperation insertCheckForSubpatternsFoundNegativeIndependent(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // check whether there were no subpattern matches found
            CheckPartialMatchForSubpatternsFound checkSubpatternsFound =
                new CheckPartialMatchForSubpatternsFound(negativeIndependentNamePrefix, patternGraph.isIterationBreaking);
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

                if (!isNestedInNegative) // no match object needed(/available) if independent is part of negative
                {
                    // ---- fill the match object with the candidates 
                    // ---- which have passed all the checks for being a match
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, false);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, true);
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
        private SearchProgramOperation insertPatternFound(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

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
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, false);
            insertionPoint = insertMatchObjectBuilding(insertionPoint,
                patternGraph, MatchObjectType.Normal, true);


            // ---- nesting level up
            insertionPoint = continuationPoint;

            // check whether to continue the matching process
            // or abort because the maximum desired number of maches was reached
            CheckContinueMatchingMaximumMatchesReached checkMaximumMatches =
#if NO_ADJUST_LIST_HEADS
                new CheckContinueMatchingMaximumMatchesReached(CheckMaximumMatchesType.Action, false);
#else
                new CheckContinueMatchingMaximumMatchesReached(CheckMaximumMatchesType.Action, true);
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
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            if (isNegative)
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

                if (!isNestedInNegative) // no match object needed(/available) if independent is part of negative
                {
                    // fill the match object with the candidates 
                    // which have passed all the checks for being a match
                    insertionPoint = insertMatchObjectBuilding(insertionPoint, 
                        patternGraph, MatchObjectType.Independent, false);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, true);
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
        private SearchProgramOperation insertEndOfIterationHandling(SearchProgramOperation insertionPoint)
        {
            Debug.Assert(NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek()) == "");

            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

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
            LeafSubpatternMatched leafMatched =
                new LeafSubpatternMatched(rulePatternClassName, patternGraph.pathPrefix + patternGraph.name, true);
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
                new ContinueOperation(ContinueOperationType.ByReturn, false);
            insertionPoint = insertionPoint.Append(leave);

            // ---- nesting level up
            insertionPoint = continuationPointAfterTasksLeft;

            // ---- we execute the open subpattern matching tasks
            MatchSubpatterns matchSubpatterns =
                new MatchSubpatterns("");
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
               new ContinueOperation(ContinueOperationType.ByReturn, false);
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
                            "GRGEN_MODEL." + typeModel.TypeTypes[target.PatternElement.TypeID].Name,
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
                            "GRGEN_MODEL." + typeModel.TypeTypes[target.PatternElement.TypeID].Name,
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
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfNodeFromLookupOrPickOrMap(
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
        private SearchProgramOperation decideOnAndInsertCheckConnectednessOfEdgeFromLookupOrPickOrMap(
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
        /// returns name prefix for candidate variables of the given pattern graph
        /// computed from current negative/independent pattern nesting
        /// </summary>
        private string NegativeIndependentNamePrefix(PatternGraph patternGraph)
        {
            string negativeIndependentNamePrefix = "";
            PatternGraph[] nestingPatterns = patternGraphWithNestingPatterns.ToArray(); // holy shit! no sets, no backward iterators, no direct access to stack ... c# data structures suck
            if (nestingPatterns[nestingPatterns.Length - 1] == patternGraph) return "";
            for (int i = nestingPatterns.Length - 2; i >= 0; --i) // skip first = top level pattern; stack dumped in reverse ^^
            {
                negativeIndependentNamePrefix += nestingPatterns[i].name;
                if (nestingPatterns[i] == patternGraph) break; ;
            }

            if (negativeIndependentNamePrefix != "") {
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

        /// <summary>
        /// Returns the variable which will evaluate at runtime to the match of the nesting pattern.
        /// Dependent on currently processed pattern graph of static nesting as given by nesting stack.
        /// </summary>
        private string getCurrentMatchOfNestingPattern()
        {
            if (patternGraphWithNestingPatterns.Count == 1)
            {
                if(programType == SearchProgramType.Subpattern || programType == SearchProgramType.AlternativeCase || programType == SearchProgramType.Iterated)
                {
                    return "matchOfNestingPattern";
                } else {
                    return "null";
                }
            }
            else // patternGraphWithNestingPatterns.Count > 1
            {
                int i = 0;
                foreach (PatternGraph patternGraph in patternGraphWithNestingPatterns)
                {
                    ++i;
                    if (i == 2) {
                        return NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
                    }
                }
                return "null"; // shut up warning
            }
        }

        /// <summary>
        /// Returns the variable which will evaluate at runtime to the last match at the previous nesting level.
        /// Dependent on currently processed pattern graph of static nesting as given by nesting stack.
        /// </summary>
        private string getCurrentLastMatchAtPreviousNestingLevel()
        {
            if (patternGraphWithNestingPatterns.Count == 1)
            {
                if(programType == SearchProgramType.Subpattern || programType == SearchProgramType.AlternativeCase || programType == SearchProgramType.Iterated)
                {
                    return "lastMatchAtPreviousNestingLevel";
                } else {
                    return "null";
                }
            }
            else // patternGraphWithNestingPatterns.Count > 1
            {
                int i = 0;
                foreach (PatternGraph patternGraph in patternGraphWithNestingPatterns)
                {
                    ++i;
                    if (i == 2) {
                        return NamesOfEntities.PatternpathMatch(patternGraph.pathPrefix + patternGraph.name);
                    }
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
    }
}

