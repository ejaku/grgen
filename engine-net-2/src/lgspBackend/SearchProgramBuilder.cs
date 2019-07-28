/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

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
            string nameOfSearchProgram,
            bool parallelized,
            bool emitProfiling)
        {
            PatternGraph patternGraph = rulePattern.patternGraph;
            this.model = model;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            patternGraphWithNestingPatterns.Push(patternGraph);
            this.parallelized = parallelized;
            isoSpaceNeverAboveMaxIsoSpace = patternGraphWithNestingPatterns.Peek().maxIsoSpace < (int)LGSPElemFlags.MAX_ISO_SPACE;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(rulePattern.name, rulePattern.PatternGraph.Package, false);
            this.emitProfiling = emitProfiling;
            packagePrefixedActionName = rulePattern.patternGraph.Package != null ? rulePattern.patternGraph.Package + "::" + rulePattern.name : rulePattern.name;
            firstLoopPassed = false;
            
            // filter out parameters which are implemented by lookup due to maybe null unfolding
            // and suffix matcher method name by missing parameters which get computed by lookup here
            String name;
            GetFilteredParametersAndSuffixedMatcherName(
                rulePattern, patternGraph, parallelized ? 0 : index,
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
            bool containsSubpatterns = patternGraph.embeddedGraphsPlusInlined.Length > 0
                || patternGraph.iteratedsPlusInlined.Length > 0
                || patternGraph.alternativesPlusInlined.Length > 0;
            SearchProgram searchProgram;
            if(parallelized)
            {
                if(index == 1)
                {
                    List<String> matchingPatternClassTypeNames = new List<String>();
                    List<Dictionary<PatternGraph, bool>> nestedIndependents = new List<Dictionary<PatternGraph, bool>>();
                    ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, rulePattern, patternGraph);

                    searchProgram = new SearchProgramOfActionParallelizationBody(
                        rulePatternClassName,
                        patternGraph.name, name + "_parallelized_body",
                        rulePattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                        containsSubpatterns, wasIndependentInlined(patternGraph, index), 
                        matchingPatternClassTypeNames, nestedIndependents,
                        emitProfiling, patternGraph.PackagePrefixedName);
                }
                else // index == 0
                {
                    searchProgram = new SearchProgramOfActionParallelizationHead(
                        rulePatternClassName,
                        patternGraph.name, parameterTypes, parameterNames, name + "_parallelized",
                        emitProfiling, patternGraph.PackagePrefixedName);
                }
            }
            else
            {
                List<String> matchingPatternClassTypeNames = new List<String>();
                List<Dictionary<PatternGraph, bool>> nestedIndependents = new List<Dictionary<PatternGraph, bool>>();
                ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, rulePattern, patternGraph);

                searchProgram = new SearchProgramOfAction(
                    rulePatternClassName,
                    patternGraph.name, parameterTypes, parameterNames, name,
                    rulePattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                    containsSubpatterns, wasIndependentInlined(patternGraph, indexOfSchedule),
                    matchingPatternClassTypeNames, nestedIndependents,
                    emitProfiling, patternGraph.PackagePrefixedName,
                    patternGraph.maybeNullElementNames, suffixedMatcherNameList, paramNamesList);
            } 
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            if(!parallelized || index == 0)
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
            LGSPMatchingPattern matchingPattern,
            bool parallelized,
            bool emitProfiling)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));

            PatternGraph patternGraph = matchingPattern.patternGraph;
            programType = SearchProgramType.Subpattern;
            this.model = model;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            patternGraphWithNestingPatterns.Push(patternGraph);
            this.parallelized = parallelized;
            isoSpaceNeverAboveMaxIsoSpace = patternGraphWithNestingPatterns.Peek().maxIsoSpace < (int)LGSPElemFlags.MAX_ISO_SPACE;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, matchingPattern.PatternGraph.Package, true);
            this.emitProfiling = emitProfiling;
            packagePrefixedActionName = null;
            firstLoopPassed = false;

            List<String> matchingPatternClassTypeNames = new List<String>();
            List<Dictionary<PatternGraph, bool>> nestedIndependents = new List<Dictionary<PatternGraph, bool>>();
            ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, matchingPattern, patternGraph);

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfSubpattern(
                rulePatternClassName,
                patternGraph.Name,
                matchingPattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                "myMatch",
                wasIndependentInlined(patternGraph, indexOfSchedule),
                matchingPatternClassTypeNames, nestedIndependents,
                parallelized);
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
            Alternative alternative,
            bool parallelized,
            bool emitProfiling)
        {
            programType = SearchProgramType.AlternativeCase;
            this.model = model;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            this.parallelized = parallelized;
            this.alternative = alternative;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, matchingPattern.PatternGraph.Package, !(matchingPattern is LGSPRulePattern));
            this.emitProfiling = emitProfiling;
            packagePrefixedActionName = null;
            firstLoopPassed = false;

            // build combined list of namesOfPatternGraphsOnPathToEnclosedPatternpath
            // from the namesOfPatternGraphsOnPathToEnclosedPatternpath of the alternative cases
            // also build combined lists of matching pattern class type names and nested independents
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath = new List<string>();
            List<string> matchingPatternClassTypeNames = new List<string>();
            List<Dictionary<PatternGraph, bool>> nestedIndependents = new List<Dictionary<PatternGraph, bool>>();
            for (int i = 0; i < alternative.alternativeCases.Length; ++i)
            {
                PatternGraph altCase = alternative.alternativeCases[i];

                foreach (String name in altCase.patternGraphsOnPathToEnclosedPatternpath)
                {
                    if(!namesOfPatternGraphsOnPathToEnclosedPatternpath.Contains(name))
                        namesOfPatternGraphsOnPathToEnclosedPatternpath.Add(name);
                }

                ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, matchingPattern, altCase);
            }

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfAlternative(
                rulePatternClassName,
                namesOfPatternGraphsOnPathToEnclosedPatternpath,
                "myMatch",
                matchingPatternClassTypeNames, nestedIndependents,
                parallelized);
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

                string inlinedPatternClassName = rulePatternClassName;
                string pathPrefixInInlinedPatternClass = scheduledSearchPlan.PatternGraph.pathPrefix;
                string unprefixedNameInInlinedPatternClass = scheduledSearchPlan.PatternGraph.name;
                if(alternative.originalAlternative != null)
                {
                    inlinedPatternClassName = alternative.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name;
                    pathPrefixInInlinedPatternClass = alternative.originalAlternative.pathPrefix + alternative.originalAlternative.name + "_";
                    unprefixedNameInInlinedPatternClass = alternative.originalAlternative.alternativeCases[i].name;
                }

                GetPartialMatchOfAlternative matchAlternative = new GetPartialMatchOfAlternative(
                    pathPrefixInInlinedPatternClass, 
                    unprefixedNameInInlinedPatternClass,
                    inlinedPatternClassName,
                    wasIndependentInlined(altCase, indexOfSchedule));
                matchAlternative.OperationsList = new SearchProgramList(matchAlternative);
                SearchProgramOperation continuationPointAfterAltCase = insertionPoint.Append(matchAlternative);
                
                // at level of the current alt case
                insertionPoint = matchAlternative.OperationsList;
                insertionPoint = insertVariableDeclarations(insertionPoint, altCase);

                patternGraphWithNestingPatterns.Push(altCase);
                isoSpaceNeverAboveMaxIsoSpace = patternGraphWithNestingPatterns.Peek().maxIsoSpace < (int)LGSPElemFlags.MAX_ISO_SPACE;
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
            PatternGraph iter,
            bool parallelized,
            bool emitProfiling)
        {
            programType = SearchProgramType.Iterated;
            this.model = model;
            patternGraphWithNestingPatterns = new Stack<PatternGraph>();
            patternGraphWithNestingPatterns.Push(iter);
            this.parallelized = parallelized;
            isoSpaceNeverAboveMaxIsoSpace = patternGraphWithNestingPatterns.Peek().maxIsoSpace < (int)LGSPElemFlags.MAX_ISO_SPACE;
            isNegative = false;
            isNestedInNegative = false;
            rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, matchingPattern.PatternGraph.Package, !(matchingPattern is LGSPRulePattern));
            this.emitProfiling = emitProfiling;
            packagePrefixedActionName = null;
            firstLoopPassed = false;

            List<String> matchingPatternClassTypeNames = new List<String>();
            List<Dictionary<PatternGraph, bool>> nestedIndependents = new List<Dictionary<PatternGraph, bool>>();
            ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, matchingPattern, iter);

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfIterated(
                rulePatternClassName,
                matchingPattern.patternGraph.Name,
                iter.Name,
                iter.pathPrefix,
                matchingPattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                "myMatch",
                wasIndependentInlined(iter, indexOfSchedule), 
                matchingPatternClassTypeNames, nestedIndependents,
                parallelized);
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
        /// Inserts declarations for variables extracted from parameters
        /// </summary>
        private SearchProgramOperation insertVariableDeclarations(SearchProgramOperation insertionPoint, PatternGraph patternGraph)
        {
            foreach(PatternVariable var in patternGraph.variablesPlusInlined)
            {
                if(!var.defToBeYieldedTo) // def variables are handled with the schedule, must come after the presets
                {
                    // inlined variables are handled later, cause they may depend on elements matched
                    if(var.originalVariable == null || !patternGraph.WasInlinedHere(var.originalSubpatternEmbedding))
                    {
                        insertionPoint = insertionPoint.Append(
                            new ExtractVariable(TypesHelper.TypeName(var.type), var.Name)
                        );
                    }
                }
            }

            return insertionPoint;
        }

        private void ExtractNestedIndependents(List<String> matchingPatternClassTypeNames, List<Dictionary<PatternGraph, bool>> nestedIndependents,
            LGSPMatchingPattern matchingPattern, PatternGraph patternGraph)
        {
            matchingPatternClassTypeNames.Add(matchingPattern.GetType().Name);
            nestedIndependents.Add(patternGraph.nestedIndependents);

            foreach (PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, matchingPattern, idpt);
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
        /// true if statically determined that the iso space number of the pattern getting constructed 
        /// is always below the maximum iso space number (the maximum nesting level of the isomorphy spaces)
        /// </summary>
        private bool isoSpaceNeverAboveMaxIsoSpace;

        /// <summary>
        /// name says everything
        /// </summary>
        private const int MAXIMUM_NUMBER_OF_TYPES_TO_CHECK_BY_TYPE_ID = 2;

        /// <summary>
        /// The index of the currently built schedule
        /// </summary>
        private int indexOfSchedule;

        /// <summary>
        /// whether to build the parallelized matcher from the parallelized schedule
        /// </summary>
        private bool parallelized;

        /// <summary>
        /// whether to emit code for gathering profiling information (about search steps executed)
        /// </summary>
        private bool emitProfiling;

        /// <summary>
        /// the package prefixed name of the action in case we're building a rule/test, otherwise null
        /// </summary>
        private string packagePrefixedActionName;

        /// <summary>
        /// tells whether the first loop of the search programm was built, or not yet
        /// needed for the profile that does special statistics for the first loop,
        /// because this is the one that will get parallelized in case of action parallelization
        /// only of relevance if programType == SearchProgramType.Action, otherwise the type pinns it to true
        /// </summary>
        private bool firstLoopPassed;

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
                        op.Element);

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

                case SearchOperationType.PickFromStorageDependent:
                    return buildPickFromStorageDependent(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Storage,
                        op.Isomorphy);

                case SearchOperationType.PickFromIndex:
                    return buildPickFromIndex(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess,
                        op.Isomorphy);

                case SearchOperationType.PickFromIndexDependent:
                    return buildPickFromIndex(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.IndexAccess,
                        op.Isomorphy);

                case SearchOperationType.PickByName:
                    return buildPickByName(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.NameLookup,
                        op.Isomorphy);

                case SearchOperationType.PickByNameDependent:
                    return buildPickByName(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.NameLookup,
                        op.Isomorphy);

                case SearchOperationType.PickByUnique:
                    return buildPickByUnique(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        (SearchPlanNode)op.Element,
                        op.UniqueLookup,
                        op.Isomorphy);

                case SearchOperationType.PickByUniqueDependent:
                    return buildPickByUnique(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        //op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.UniqueLookup,
                        op.Isomorphy);

                case SearchOperationType.Cast:
                    return buildCast(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

                case SearchOperationType.Assign:
                    return buildAssign(insertionPointWithinSearchProgram,
                        indexOfScheduledSearchPlanOperationToBuild,
                        op.SourceSPNode,
                        (SearchPlanNode)op.Element,
                        op.Isomorphy);

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
                        op.Isomorphy);

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
                        op.Isomorphy);

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
                        op.Isomorphy);

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
                        op.Isomorphy);

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
                        op.Isomorphy);

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
                        IncidentEdgeType.IncomingOrOutgoing);

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
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if (isomorphy.SetIsMatchedBit)
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            if (isomorphy.CheckIsMatchedBit)
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
            if (isomorphy.SetIsMatchedBit)
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
            if (isomorphy.SetIsMatchedBit)
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
        /// DefElementToBeYieldedTo search plan operation
        /// are created and inserted into search program
        /// </summary>
        private SearchProgramOperation buildDefToBeYieldedTo(
            SearchProgramOperation insertionPoint,
            int currentOperationIndex,
            object target)
        {
            if(target is PatternVariable)
            {
                PatternVariable var = (PatternVariable)target;

                String initializationExpression;
                if(var.initialization != null)
                {
                    SourceBuilder builder = new SourceBuilder();
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
            else
            {
                if(((SearchPlanNode)target).PatternElement is PatternNode)
                {
                    PatternNode node = (PatternNode)((SearchPlanNode)target).PatternElement;

                    String initializationExpression;
                    if(node.initialization != null)
                    {
                        SourceBuilder builder = new SourceBuilder();
                        node.initialization.Emit(builder);
                        initializationExpression = builder.ToString();
                    }
                    else
                    {
                        initializationExpression = "null";
                    }
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Node, "GRGEN_LGSP.LGSPNode", node.Name, initializationExpression)
                    );
                }
                else
                {
                    PatternEdge edge = (PatternEdge)((SearchPlanNode)target).PatternElement;

                    String initializationExpression;
                    if(edge.initialization != null)
                    {
                        SourceBuilder builder = new SourceBuilder();
                        edge.initialization.Emit(builder);
                        initializationExpression = builder.ToString();
                    }
                    else
                    {
                        initializationExpression = "null";
                    }
                    insertionPoint = insertionPoint.Append(
                        new DeclareDefElement(EntityType.Edge, "GRGEN_LGSP.LGSPEdge", edge.Name, initializationExpression)
                    );
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
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
                            isoSpaceNeverAboveMaxIsoSpace,
                            isomorphy.Parallel);
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
            if (isomorphy.SetIsMatchedBit)
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
            StorageAccess storage,
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("set") || TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("map");
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // iterate available storage elements
            string iterationType;
            if(isDict) iterationType = "System.Collections.Generic.KeyValuePair<"
                + TypesHelper.GetStorageKeyTypeName(storage.Variable.type) + ","
                + TypesHelper.GetStorageValueTypeName(storage.Variable.type) + ">";
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

            if(storage.Variable != null) ; // alt, siehe oben
            if(storage.GlobalVariable != null) ; // neu -- wenn es ein container-typ ist iterieren, wenn es ein elementarer typ ist eine einfache zuweisung -- und kein != null handling
            if(storage.Attribute != null) ; // das kann hier nicht auftreten

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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = storage.Attribute.Attribute.Kind == AttributeKind.SetAttr || storage.Attribute.Attribute.Kind == AttributeKind.MapAttr;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // iterate available storage elements
            string iterationType;
            if(isDict)
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

            if(storage.Variable != null) ; // das kann hier nicht auftreten
            if(storage.GlobalVariable != null) ; // das kann hier nicht auftreten
            if(storage.Attribute != null) ; // alt, siehe oben

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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
                if(indexAscending.From!=null)
                    indexAscending.From.Emit(fromExpression);
                SourceBuilder toExpression = new SourceBuilder();
                if(indexAscending.To!=null)
                    indexAscending.To.Emit(toExpression);
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
                    indexDescending.From.Emit(fromExpression);
                SourceBuilder toExpression = new SourceBuilder();
                if(indexDescending.To != null)
                    indexDescending.To.Emit(toExpression);
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            SourceBuilder expression = new SourceBuilder();
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            SourceBuilder expression = new SourceBuilder();
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // get candidate from other element (the cast is simply the following type check)
            GetCandidateByDrawing fromOtherElementForAssign =
                new GetCandidateByDrawing(
                    GetCandidateByDrawingType.FromOtherElementForAssign,
                    target.PatternElement.Name,
                    source.PatternElement.Name,
                    isNode);
            insertionPoint = insertionPoint.Append(fromOtherElementForAssign);

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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // storage muss ein container typ nach graph element sein, index muss ein elementarer typ sein

            if(storage.Variable != null) ; // neu
            if(storage.GlobalVariable != null) ; // neu
            if(storage.Attribute != null) ; // das kann hier nicht auftreten
            if(index.Variable != null) ; // neu
            if(index.GlobalVariable != null) ; // neu
            if(index.Attribute != null) ; // das kann hier nicht auftreten
            if(index.GraphElement != null) ; // das kann hier nicht auftreten
            
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());

            // storage muss ein container typ nach graph element sein, index muss ein elementarer typ sein

            if(storage.Variable != null && index.Variable != null) ;// das kann hier nicht auftreten; 
            if(storage.Variable != null && index.GlobalVariable != null) ;// das kann hier nicht auftreten; 
            if(storage.Variable != null && index.Attribute != null) ;// neu
            if(storage.Variable != null && index.GraphElement != null) ;// alt, siehe unten

            if(storage.GlobalVariable != null && index.Variable != null) ;// das kann hier nicht auftreten; 
            if(storage.GlobalVariable != null && index.GlobalVariable != null) ;// das kann hier nicht auftreten; 
            if(storage.GlobalVariable != null && index.Attribute != null) ;// neu
            if(storage.GlobalVariable != null && index.GraphElement != null) ;// neu

            if(storage.Attribute != null && index.Variable != null) ;// neu
            if(storage.Attribute != null && index.GlobalVariable != null) ;// neu
            if(storage.Attribute != null && index.Attribute != null) ;// kann nicht auftreten, 2 abh�ngigkeiten
            if(storage.Attribute != null && index.GraphElement != null) ;// kann nicht auftreten, 2 anh�ngigkeiten
            
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
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if (isomorphy.SetIsMatchedBit)
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
                        isoSpaceNeverAboveMaxIsoSpace,
                        isomorphy.Parallel,
                        isomorphy.LockForAllThreads);
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
            if (isomorphy.SetIsMatchedBit)
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
            if(parallelized) indexOfSchedule = 0; // the neg of a parallelized body at index 1 is still only at index 0

            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            if(parallelized) indexOfSchedule = 0; // the idpt of a parallelized body at index 1 is still only at index 0

            string independentNamePrefix = NegativeIndependentNamePrefix(patternGraphWithNestingPatterns.Peek());
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
            GetMatchElementsForDuplicateCheck(patternGraph,
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
                {
                    ++numberOfNeededElements;
                }
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
            string negativeIndependentNamePrefix = "";
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            // decide on and insert operation determining type of candidate
            SearchProgramOperation continuationPointAfterTypeIteration;
            SearchProgramOperation insertionPointAfterTypeIteration =
                decideOnAndInsertGetType(insertionPoint, target,
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
                    wasIndependentInlined(patternGraph, indexOfSchedule),
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = "";

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
            string negativeIndependentNamePrefix = "";
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            // iterate available storage elements
            string iterationType;
            if(isDict) iterationType = "System.Collections.Generic.KeyValuePair<"
                + TypesHelper.GetStorageKeyTypeName(storage.Variable.type) + ","
                + TypesHelper.GetStorageValueTypeName(storage.Variable.type) + ">";
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
                    wasIndependentInlined(patternGraph, indexOfSchedule),
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("set") || TypesHelper.DotNetTypeToXgrsType(storage.Variable.type).StartsWith("map");
            string negativeIndependentNamePrefix = "";

            // iterate available storage elements
            string iterationType;
            if(isDict) iterationType = "System.Collections.Generic.KeyValuePair<"
                + TypesHelper.GetStorageKeyTypeName(storage.Variable.type) + ","
                + TypesHelper.GetStorageValueTypeName(storage.Variable.type) + ">";
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
            string negativeIndependentNamePrefix = "";
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            // iterate available storage elements
            string iterationType;
            if(isDict)
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
                    wasIndependentInlined(patternGraph, indexOfSchedule),
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            bool isDict = storage.Attribute.Attribute.Kind == AttributeKind.SetAttr || storage.Attribute.Attribute.Kind == AttributeKind.MapAttr;
            string negativeIndependentNamePrefix = "";

            // iterate available storage elements
            string iterationType;
            if(isDict)
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
            string negativeIndependentNamePrefix = "";
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
                        wasIndependentInlined(patternGraph, indexOfSchedule),
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else if(index is IndexAccessAscending)
            {
                IndexAccessAscending indexAscending = (IndexAccessAscending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexAscending.From != null)
                    indexAscending.From.Emit(fromExpression);
                SourceBuilder toExpression = new SourceBuilder();
                if(indexAscending.To != null)
                    indexAscending.To.Emit(toExpression);
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
                        wasIndependentInlined(patternGraph, indexOfSchedule),
                        emitProfiling,
                        packagePrefixedActionName,
                        !firstLoopPassed);
            }
            else //if(index is IndexAccessDescending)
            {
                IndexAccessDescending indexDescending = (IndexAccessDescending)index;
                SourceBuilder fromExpression = new SourceBuilder();
                if(indexDescending.From != null)
                    indexDescending.From.Emit(fromExpression);
                SourceBuilder toExpression = new SourceBuilder();
                if(indexDescending.To != null)
                    indexDescending.To.Emit(toExpression);
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
                        wasIndependentInlined(patternGraph, indexOfSchedule),
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
            IsomorphyInformation isomorphy)
        {
            bool isNode = target.NodeType == PlanNodeType.Node;
            string negativeIndependentNamePrefix = "";
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
                    indexAscending.From.Emit(fromExpression);
                SourceBuilder toExpression = new SourceBuilder();
                if(indexAscending.To != null)
                    indexAscending.To.Emit(toExpression);
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
                    indexDescending.From.Emit(fromExpression);
                SourceBuilder toExpression = new SourceBuilder();
                if(indexDescending.To != null)
                    indexDescending.To.Emit(toExpression);
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
                    wasIndependentInlined(patternGraph, indexOfSchedule),
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
                        wasIndependentInlined(patternGraph, indexOfSchedule),
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
                        wasIndependentInlined(patternGraph, indexOfSchedule),
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
            insertionPoint = decideOnAndInsertCheckType(insertionPoint, target);

            // check connectedness of candidate
            SearchProgramOperation continuationPointOfConnectednessCheck;
            insertionPoint = decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(
                insertionPoint, target, source, edgeType == IncidentEdgeType.Incoming,
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
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // is subpattern gives information about type of top level enclosing pattern (action vs. subpattern)
            // contains subpatterns gives informations about current pattern graph (might be negative, independent itself, too)
            bool isSubpattern = programType == SearchProgramType.Subpattern
                || programType == SearchProgramType.AlternativeCase
                || programType == SearchProgramType.Iterated;
            bool existsNonInlinedSubpattern = false;
            foreach(PatternGraphEmbedding sub in patternGraph.embeddedGraphsPlusInlined)
                if(!sub.inlined)
                    existsNonInlinedSubpattern = true;
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
                insertionPoint = insertPushSubpatternTasks(insertionPoint);

            // if this is a subpattern without subpatterns
            // it may be the last to be matched - handle that case
            if(!containsSubpatterns && isSubpattern && programType!=SearchProgramType.Iterated && negativeIndependentNamePrefix=="")
                insertionPoint = insertCheckForTasksLeft(insertionPoint);

            // if this is a subpattern or a top-level pattern which contains subpatterns
            if(containsSubpatterns || isSubpattern && negativeIndependentNamePrefix=="")
            {
                // we do the global accept of all candidate elements (write isomorphy information)
                insertionPoint = insertGlobalAccept(insertionPoint);

                // and execute the open subpattern matching tasks
                MatchSubpatterns matchSubpatterns = new MatchSubpatterns(negativeIndependentNamePrefix, parallelized);
                insertionPoint = insertionPoint.Append(matchSubpatterns);
            }

            // pop subpattern tasks (in case there are/were some)
            if(containsSubpatterns)
                insertionPoint = insertPopSubpatternTasks(insertionPoint);

            // handle matched or not matched subpatterns, matched local pattern
            if(negativeIndependentNamePrefix == "")
            {
                // subpattern or top-level pattern which contains subpatterns
                if(containsSubpatterns || isSubpattern)
                    insertionPoint = insertCheckForSubpatternsFound(insertionPoint, false);
                else // top-level-pattern of action without subpatterns
                    insertionPoint = insertPatternFound(insertionPoint);
            }
            else
            {
                // negative/independent contains subpatterns
                if(containsSubpatterns)
                    insertionPoint = insertCheckForSubpatternsFoundNegativeIndependent(insertionPoint);
                else
                    insertionPoint = insertPatternFoundNegativeIndependent(insertionPoint);
            }

            // global abandon of all accepted candidate elements (remove isomorphy information)
            if(containsSubpatterns || isSubpattern && negativeIndependentNamePrefix=="")
                insertionPoint = insertGlobalAbandon(insertionPoint);

            // decrease iterated matched counter again if it is iterated
            if(programType == SearchProgramType.Iterated)
            {
                AbandonIterated abandonIterated = new AbandonIterated();
                insertionPoint = insertionPoint.Append(abandonIterated);
            }

            return insertionPoint;
        }

        private bool wasIndependentInlined(PatternGraph patternGraph, int index)
        {
            SearchOperation[] operations;
            if(parallelized)
                operations = patternGraph.parallelizedSchedule[index].Operations;
            else
                operations = patternGraph.schedulesIncludingNegativesAndIndependents[indexOfSchedule].Operations;

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
            if(parallelized)
                operations = patternGraph.parallelizedSchedule[indexOfSchedule].Operations;
            else
                operations = patternGraph.schedulesIncludingNegativesAndIndependents[indexOfSchedule].Operations;

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
                    TypesHelper.XgrsTypeToCSharpTypeNodeEdge(model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName),
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
                        TypesHelper.XgrsTypeToCSharpTypeNodeEdge(model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName),
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
                            TypesHelper.XgrsTypeToCSharpTypeNodeEdge(model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName),
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
                            TypesHelper.XgrsTypeToCSharpTypeNodeEdge(model.NodeModel.Types[currentNode.PatternElement.TypeID].PackagePrefixedName),
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
        /// Inserts code to create the match objects of the inlined subpatterns
        /// at the given position, returns position after inserted operations
        /// </summary>
        private SearchProgramOperation insertInlinedMatchObjectCreation(
            SearchProgramOperation insertionPoint,
            PatternGraph patternGraph,
            MatchObjectType matchObjectType)
        {
            String matchOfEnclosingPatternName;
            if(matchObjectType == MatchObjectType.Independent) {
                matchOfEnclosingPatternName = NamesOfEntities.MatchedIndependentVariable(patternGraph.pathPrefix + patternGraph.name);
            } else { //if(matchObjectType==MatchObjectType.Normal)
                matchOfEnclosingPatternName = "match";
            }

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

            for (int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
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
                            rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }
            for (int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
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
                            rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }

            // only nodes and edges for patternpath matches
            if (matchObjectType == MatchObjectType.Patternpath) {
                return insertionPoint;
            }

            foreach (PatternVariable var in patternGraph.variablesPlusInlined)
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
                            rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }

            // only nodes, edges, variables in defElements pass
            if(defElements) {
                return insertionPoint;
            }

            for (int i = 0; i < patternGraph.embeddedGraphsPlusInlined.Length; ++i)
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
                            rulePatternClassName,
                            enumPrefix,
                            inlinedMatchObjectName ?? matchObjectName,
                            -1
                        );
                    insertionPoint = insertionPoint.Append(buildMatch);
                }
            }
            for (int i = 0; i < patternGraph.iteratedsPlusInlined.Length; ++i)
            {
                string inlinedMatchObjectName = null;
                string unprefixedName = patternGraph.iteratedsPlusInlined[i].iteratedPattern.name;
                string inlinedPatternClassName = rulePatternClassName;
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
            for (int i = 0; i < patternGraph.alternativesPlusInlined.Length; ++i)
            {
                string inlinedMatchObjectName = null;
                string unprefixedName = patternGraph.alternativesPlusInlined[i].name;
                string inlinedPatternClassName = rulePatternClassName;
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
            if (patternGraph.nestedIndependents != null)
            {
                foreach (KeyValuePair<PatternGraph, bool> nestedIndependent in patternGraph.nestedIndependents)
                {
                    string inlinedMatchObjectName = null;
                    string unprefixedName = nestedIndependent.Key.name;
                    string inlinedPatternClassName = rulePatternClassName;
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
                && (patternGraph.isDefEntityExistingPlusInlined || patternGraph.IsRefEntityExisting()))
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

                    insertYieldAssignments(insertionPoint,
                        patternGraph, nestedMatchObjectName + ".Root", iterated.iteratedPattern);

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
                        string inlinedPatternClassName = rulePatternClassName;
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

                        insertYieldAssignments(insertionPoint,
                            patternGraph, "altCaseMatch", alternativeCase);

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
                    {
                        inlinedMatchObjectName = "match_" + independent.originalSubpatternEmbedding.Name;
                    }

                    String nestedMatchObjectName = NamesOfEntities.MatchedIndependentVariable(independent.pathPrefix + independent.name); ;

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
                                        rulePatternClassName + ".Match_" + patternGraph.pathPrefix + patternGraph.name + "_" + iterated.iteratedPattern.name,
                                        "iteratedMatch");
                                accumulateUpIterated.NestedOperationsList = new SearchProgramList(accumulateUpIterated);
                                SearchProgramOperation continuationPoint = insertionPoint.Append(accumulateUpIterated);
                                insertionPoint = accumulateUpIterated.NestedOperationsList;

                                accumulationYield.IteratedMatchVariable = "iteratedMatch._" + NamesOfEntities.Variable(accumulationYield.UnprefixedVariable);
                                accumulationYield.ReplaceVariableByIterationVariable(accumulationYield);

                                SourceBuilder yieldAssignmentSource = new SourceBuilder();
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
                            patternEmbedding.matchingPatternOfEmbeddedGraph.defs[i] is VarType ? TypesHelper.TypeName(patternEmbedding.matchingPatternOfEmbeddedGraph.defs[i]) : patternEmbedding.matchingPatternOfEmbeddedGraph.defs[i].IsNodeType ? "GRGEN_LGSP.LGSPNode" : "GRGEN_LGSP.LGSPEdge",
                            isVariable ? (Expression)new VariableExpression(elem.Name) : (Expression)new GraphEntityExpression(elem.Name)
                        );

                        SourceBuilder yieldAssignmentSource = new SourceBuilder();
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
        /// Inserts code for bubbling yield assignments
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
            for (int i = patternGraph.alternativesPlusInlined.Length - 1; i >= 0; --i)
            {
                Alternative alternative = patternGraph.alternativesPlusInlined[i];

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

                string inlinedPatternClassName = rulePatternClassName;
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
                        parallelized
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            // then iterated patterns of the pattern
            // to handle iterated in linear order we've to push them in reverse order on the stack
            for (int i = patternGraph.iteratedsPlusInlined.Length - 1; i >= 0; --i)
            {
                PatternGraph iter = patternGraph.iteratedsPlusInlined[i].iteratedPattern;

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
                        "",
                        "",
                        connectionName,
                        argumentExpressions,
                        negativeIndependentNamePrefix,
                        searchPatternpath,
                        matchOfNestingPattern,
                        lastMatchAtPreviousNestingLevel,
                        parallelized
                    );
                insertionPoint = insertionPoint.Append(pushTask);
            }

            // and finally subpatterns of the pattern
            // to handle subpatterns in linear order we've to push them in reverse order on the stack
            for (int i = patternGraph.embeddedGraphsPlusInlined.Length - 1; i >= 0; --i)
            {
                PatternGraphEmbedding subpattern = patternGraph.embeddedGraphsPlusInlined[i];
                if(subpattern.inlined)
                    continue;
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
                        lastMatchAtPreviousNestingLevel,
                        parallelized
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

            foreach (PatternGraphEmbedding subpattern in patternGraph.embeddedGraphsPlusInlined)
            {
                if(subpattern.inlined)
                    continue;
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Subpattern,
                        subpattern.matchingPatternOfEmbeddedGraph.name,
                        subpattern.name,
                        parallelized
                    );
                insertionPoint = insertionPoint.Append(popTask);
            }

            foreach (Iterated iterated in patternGraph.iteratedsPlusInlined)
            {
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Iterated,
                        iterated.iteratedPattern.name,
                        iterated.iteratedPattern.pathPrefix,
                        parallelized
                    );
                insertionPoint = insertionPoint.Append(popTask);
            }

            foreach (Alternative alternative in patternGraph.alternativesPlusInlined)
            {
                PopSubpatternTask popTask =
                    new PopSubpatternTask(
                        negativeIndependentNamePrefix,
                        PushAndPopSubpatternTaskTypes.Alternative,
                        alternative.name,
                        alternative.pathPrefix,
                        parallelized
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
            string inlinedPatternClassName = rulePatternClassName;
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
            if(wasIndependentInlined(patternGraph, indexOfSchedule))
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
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

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
                new FillPartialMatchForDuplicateChecking(rulePatternClassName,
                    patternGraph.pathPrefix + patternGraph.Name,
                    namesOfPatternElements.ToArray(),
                    unprefixedNamesOfPatternElements.ToArray(),
                    patternElementIsNode.ToArray(),
                    parallelized && programType == SearchProgramType.Action);
            insertionPoint = insertionPoint.Append(checkDuplicateMatch);

            return insertionPoint;
        }

        private void GetMatchElementsForDuplicateCheck(PatternGraph patternGraph, 
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
        private SearchProgramOperation insertGlobalAccept(SearchProgramOperation insertionPoint)
        {
            bool isAction = programType == SearchProgramType.Action;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            for (int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if (patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
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
                            isoSpaceNeverAboveMaxIsoSpace,
                            parallelized);
                        insertionPoint = insertionPoint.Append(acceptGlobal);
                    }
                }
            }
            for (int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if (patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
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
                            isoSpaceNeverAboveMaxIsoSpace,
                            parallelized);
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

            for (int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if (patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
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
            for (int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if (patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
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
        private SearchProgramOperation insertGlobalAbandon(SearchProgramOperation insertionPoint)
        {
            bool isAction = programType == SearchProgramType.Action;
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
            string negativeIndependentNamePrefix = NegativeIndependentNamePrefix(patternGraph);

            // global abandon of all candidate elements (remove isomorphy information)
            for (int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if (patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
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
                            isoSpaceNeverAboveMaxIsoSpace,
                            parallelized);
                        insertionPoint = insertionPoint.Append(abandonGlobal);
                    }
                }
            }
            for (int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if (patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
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
                            isoSpaceNeverAboveMaxIsoSpace,
                            parallelized);
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
            for (int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                if (patternGraph.nodesPlusInlined[i].PointOfDefinition == patternGraph
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
            for (int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                if (patternGraph.edgesPlusInlined[i].PointOfDefinition == patternGraph
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
        private SearchProgramOperation insertCheckForSubpatternsFound(SearchProgramOperation insertionPoint, 
            bool isIteratedNullMatch)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();
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
            if (programType == SearchProgramType.Action) {
                type = PatternAndSubpatternsMatchedType.Action;
            } else if (programType == SearchProgramType.Iterated) {
                if (isIteratedNullMatch) type = PatternAndSubpatternsMatchedType.IteratedNullMatch;
                else type = PatternAndSubpatternsMatchedType.Iterated;
            }
            Debug.Assert(!isIteratedNullMatch || programType == SearchProgramType.Iterated);
            string inlinedPatternClassName = rulePatternClassName;
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
                parallelized && indexOfSchedule == 1, type);
            SearchProgramOperation continuationPointAfterPatternAndSubpatternsMatched =
                insertionPoint.Append(patternAndSubpatternsMatched);
            patternAndSubpatternsMatched.MatchBuildingOperations =
                new SearchProgramList(patternAndSubpatternsMatched);
            insertionPoint = patternAndSubpatternsMatched.MatchBuildingOperations;

            // ---- ---- fill the match object with the candidates 
            // ---- ---- which have passed all the checks for being a match
            if (!isIteratedNullMatch)
            {
                insertionPoint = insertInlinedMatchObjectCreation(insertionPoint, 
                    patternGraph, MatchObjectType.Normal);
                insertionPoint = insertMatchObjectBuilding(insertionPoint,
                    patternGraph, MatchObjectType.Normal, false);
                insertionPoint = insertMatchObjectBuilding(insertionPoint,
                    patternGraph, MatchObjectType.Normal, true);

                // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
                if(wasIndependentInlined(patternGraph, indexOfSchedule))
                    insertionPoint = insertFillForDuplicateMatchChecking(insertionPoint);
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
                    insertionPoint = insertInlinedMatchObjectCreation(insertionPoint, 
                        patternGraph, MatchObjectType.Independent);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, false);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, true);

                    // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
                    if(wasIndependentInlined(patternGraph, indexOfSchedule))
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
        private SearchProgramOperation insertPatternFound(SearchProgramOperation insertionPoint)
        {
            PatternGraph patternGraph = patternGraphWithNestingPatterns.Peek();

            // build the pattern was matched operation
            PositivePatternWithoutSubpatternsMatched patternMatched =
                new PositivePatternWithoutSubpatternsMatched(rulePatternClassName, 
                    patternGraph.name, 
                    parallelized && indexOfSchedule == 1);
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
            if(wasIndependentInlined(patternGraph, indexOfSchedule))
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
                    CheckMaximumMatchesType.Action, true, parallelized && indexOfSchedule == 1,
                    emitProfiling, packagePrefixedActionName, firstLoopPassed);
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
                    insertionPoint = insertInlinedMatchObjectCreation(insertionPoint,
                        patternGraph, MatchObjectType.Independent);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint, 
                        patternGraph, MatchObjectType.Independent, false);
                    insertionPoint = insertMatchObjectBuilding(insertionPoint,
                        patternGraph, MatchObjectType.Independent, true);

                    // if an independent was inlined, we have to insert the local match into a set used for duplicate checking
                    if(wasIndependentInlined(patternGraph, indexOfSchedule))
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
            string inlinedPatternClassName = rulePatternClassName;
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
                new ContinueOperation(ContinueOperationType.ByReturn, false, parallelized && indexOfSchedule == 1);
            insertionPoint = insertionPoint.Append(leave);

            // ---- nesting level up
            insertionPoint = continuationPointAfterTasksLeft;

            // ---- we execute the open subpattern matching tasks
            MatchSubpatterns matchSubpatterns =
                new MatchSubpatterns("", parallelized);
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
                new ContinueOperation(ContinueOperationType.ByReturn, false, parallelized && indexOfSchedule == 1);
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
                            inlinedPatternClassName ?? rulePatternClassName,
                            inlinedPatternElementName ?? target.PatternElement.Name,
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
                            TypesHelper.PrefixedTypeFromType(typeModel.TypeTypes[target.PatternElement.TypeID]),
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

