/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// class for building search program data structure from scheduled search plan
    /// entry point for the different kinds of pattern matchers, delegates to search program body builder for doing real work
    /// </summary>
    static class SearchProgramBuilder
    {
        /// <summary>
        /// Builds search program from scheduled search plan at given index in pattern graph of the action rule pattern
        /// </summary>
        public static SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPRulePattern rulePattern,
            int index,
            string nameOfSearchProgram,
            bool parallelized,
            bool emitProfiling)
        {
            PatternGraph patternGraph = rulePattern.patternGraph;
            String rulePatternClassName = NamesOfEntities.RulePatternClassName(rulePattern.name, rulePattern.PatternGraph.Package, false);

            // filter out parameters which are implemented by lookup due to maybe null unfolding
            // and suffix matcher method name by missing parameters which get computed by lookup here
            string[] parameterTypes;
            string[] parameterNames;
            String name;
            GetFilteredParametersAndSuffixedMatcherName(
                rulePattern, patternGraph, parallelized ? 0 : index,
                out parameterTypes, out parameterNames, out name);

            SearchProgramBodyBuilder builder = new SearchProgramBodyBuilder(
                SearchProgramType.Action,
                model,
                rulePatternClassName,
                rulePattern.patternGraph.Package != null ? rulePattern.patternGraph.Package + "::" + rulePattern.name : rulePattern.name,
                parameterNames,
                parameterTypes,
                patternGraph,
                emitProfiling,
                parallelized,
                index
            );

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
                        containsSubpatterns, builder.wasIndependentInlined(patternGraph, index), 
                        matchingPatternClassTypeNames, nestedIndependents,
                        emitProfiling, patternGraph.PackagePrefixedName);
                }
                else // index == 0
                {
                    searchProgram = new SearchProgramOfActionParallelizationHead(
                        rulePatternClassName,
                        patternGraph.name, parameterTypes, parameterNames, name + "_parallelized",
                        emitProfiling, patternGraph.PackagePrefixedName, rulePattern.Outputs.Length);
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
                    containsSubpatterns, builder.wasIndependentInlined(patternGraph, 0),
                    matchingPatternClassTypeNames, nestedIndependents,
                    emitProfiling, patternGraph.PackagePrefixedName, rulePattern.Outputs.Length,
                    patternGraph.maybeNullElementNames, suffixedMatcherNameList, paramNamesList);
            } 
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            if(!parallelized || index == 0)
                insertionPoint = insertVariableDeclarations(insertionPoint, patternGraph);

            // start building with first operation in scheduled search plan
            insertionPoint = builder.BuildScheduledSearchPlanOperationIntoSearchProgram(
                0, insertionPoint);

            searchProgram.ArrayPerElementMethods = builder.arrayPerElementMethodBuilder.ToString();

            return searchProgram;
        }

        private static void GetFilteredParametersAndSuffixedMatcherName(
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
        public static SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPMatchingPattern matchingPattern,
            bool parallelized,
            bool emitProfiling)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));

            PatternGraph patternGraph = matchingPattern.patternGraph;
            String rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, matchingPattern.PatternGraph.Package, true);

            SearchProgramBodyBuilder builder = new SearchProgramBodyBuilder(
                SearchProgramType.Subpattern,
                model,
                rulePatternClassName,
                null,
                null,
                null,
                patternGraph,
                emitProfiling,
                parallelized,
                0
            );

            List<String> matchingPatternClassTypeNames = new List<String>();
            List<Dictionary<PatternGraph, bool>> nestedIndependents = new List<Dictionary<PatternGraph, bool>>();
            ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, matchingPattern, patternGraph);

            // build outermost search program operation, create the list anchor starting its program
            SearchProgram searchProgram = new SearchProgramOfSubpattern(
                rulePatternClassName,
                patternGraph.Name,
                matchingPattern.patternGraph.patternGraphsOnPathToEnclosedPatternpath,
                "myMatch",
                builder.wasIndependentInlined(patternGraph, 0),
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
            insertionPoint = builder.BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);

            // finalize task/result-pushdown handling in subpattern matcher
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Normal);
            insertionPoint = insertionPoint.Append(finalize);

            searchProgram.ArrayPerElementMethods = builder.arrayPerElementMethodBuilder.ToString();

            return searchProgram;
        }

        /// <summary>
        /// Builds search program for alternative from scheduled search plans of the alternative cases
        /// </summary>
        public static SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPMatchingPattern matchingPattern,
            Alternative alternative,
            bool parallelized,
            bool emitProfiling)
        {
            String rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, matchingPattern.PatternGraph.Package, !(matchingPattern is LGSPRulePattern));

            // build combined list of namesOfPatternGraphsOnPathToEnclosedPatternpath
            // from the namesOfPatternGraphsOnPathToEnclosedPatternpath of the alternative cases
            // also build combined lists of matching pattern class type names and nested independents
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath = new List<string>();
            List<string> matchingPatternClassTypeNames = new List<string>();
            List<Dictionary<PatternGraph, bool>> nestedIndependents = new List<Dictionary<PatternGraph, bool>>();
            for(int i = 0; i < alternative.alternativeCases.Length; ++i)
            {
                PatternGraph altCase = alternative.alternativeCases[i];

                foreach(String name in altCase.patternGraphsOnPathToEnclosedPatternpath)
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
            for(int i=0; i<alternative.alternativeCases.Length; ++i)
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

                SearchProgramBodyBuilder builder = new SearchProgramBodyBuilder(
                    SearchProgramType.AlternativeCase,
                    model,
                    rulePatternClassName,
                    null,
                    null,
                    null,
                    altCase,
                    emitProfiling,
                    parallelized,
                    0
                );

                AlternativeCaseMatching alternativeCaseMatching = new AlternativeCaseMatching(
                    pathPrefixInInlinedPatternClass, 
                    unprefixedNameInInlinedPatternClass,
                    inlinedPatternClassName,
                    builder.wasIndependentInlined(altCase, 0));
                alternativeCaseMatching.OperationsList = new SearchProgramList(alternativeCaseMatching);
                SearchProgramOperation continuationPointAfterAltCase = insertionPoint.Append(alternativeCaseMatching);
                
                // at level of the current alt case
                insertionPoint = alternativeCaseMatching.OperationsList;
                insertionPoint = insertVariableDeclarations(insertionPoint, altCase);

                // start building with first operation in scheduled search plan

                builder.BuildScheduledSearchPlanOperationIntoSearchProgram(
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

                searchProgram.ArrayPerElementMethods += builder.arrayPerElementMethodBuilder.ToString();
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
        public static SearchProgram BuildSearchProgram(
            IGraphModel model,
            LGSPMatchingPattern matchingPattern,
            PatternGraph iter,
            bool parallelized,
            bool emitProfiling)
        {
            String rulePatternClassName = NamesOfEntities.RulePatternClassName(matchingPattern.name, matchingPattern.PatternGraph.Package, !(matchingPattern is LGSPRulePattern));

            SearchProgramBodyBuilder builder = new SearchProgramBodyBuilder(
                SearchProgramType.Iterated,
                model,
                rulePatternClassName,
                null,
                null,
                null,
                iter,
                emitProfiling,
                parallelized,
                0
            );

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
                builder.wasIndependentInlined(iter, 0), 
                matchingPatternClassTypeNames, nestedIndependents,
                parallelized);
            searchProgram.OperationsList = new SearchProgramList(searchProgram);
            SearchProgramOperation insertionPoint = searchProgram.OperationsList;

            insertionPoint = insertVariableDeclarations(insertionPoint, iter);

            // initialize task/result-pushdown handling in subpattern matcher for iteration
            InitializeSubpatternMatching initialize = 
                new InitializeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Iteration);
            insertionPoint = insertionPoint.Append(initialize);

            IteratedMatchingDummyLoop iteratedMatchingDummyLoop = new IteratedMatchingDummyLoop();
            SearchProgramOperation continuationPointAfterIteratedMatching = insertionPoint.Append(iteratedMatchingDummyLoop);
            iteratedMatchingDummyLoop.NestedOperationsList = new SearchProgramList(iteratedMatchingDummyLoop);
            insertionPoint = iteratedMatchingDummyLoop.NestedOperationsList;

            // start building with first operation in scheduled search plan
            insertionPoint = builder.BuildScheduledSearchPlanOperationIntoSearchProgram(
                0,
                insertionPoint);

            insertionPoint = continuationPointAfterIteratedMatching;

            // check whether iteration came to an end (pattern not found (again)) and handle it
            insertionPoint = builder.insertEndOfIterationHandling(insertionPoint);

            // finalize task/result-pushdown handling in subpattern matcher for iteration
            FinalizeSubpatternMatching finalize =
                new FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType.Iteration);
            insertionPoint = insertionPoint.Append(finalize);

            searchProgram.ArrayPerElementMethods = builder.arrayPerElementMethodBuilder.ToString();

            return searchProgram;
        }

        /// <summary>
        /// Inserts declarations for variables extracted from parameters
        /// </summary>
        private static SearchProgramOperation insertVariableDeclarations(SearchProgramOperation insertionPoint, PatternGraph patternGraph)
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

        private static void ExtractNestedIndependents(List<String> matchingPatternClassTypeNames, List<Dictionary<PatternGraph, bool>> nestedIndependents,
            LGSPMatchingPattern matchingPattern, PatternGraph patternGraph)
        {
            matchingPatternClassTypeNames.Add(matchingPattern.GetType().Name);
            nestedIndependents.Add(patternGraph.nestedIndependents);

            foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                ExtractNestedIndependents(matchingPatternClassTypeNames, nestedIndependents, matchingPattern, idpt);
            }
        }
    }
}

