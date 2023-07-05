/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

//note the same define in SearchProgramOperationMiscellaneous.cs
//#define ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION

using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Abstract base class for search programs.
    /// A search program is a list of search program operations,
    ///   some search program operations contain nested search program operations,
    ///   yielding a search program operation tree in fact
    /// represents/assembling a backtracking search program,
    /// for finding a homomorphic mapping of the pattern graph within the host graph.
    /// A search program is itself the outermost enclosing operation.
    /// </summary>
    abstract class SearchProgram : SearchProgramOperation
    {
        protected SearchProgram(string rulePatternClassName,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name,
            bool parallel,
            List<string> matchingPatternClassTypeName,
            List<Dictionary<PatternGraph, bool>> nestedIndependents)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath = namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;
            Parallel = parallel;
            MatchingPatternClassTypeName = matchingPatternClassTypeName;
            NestedIndependents = nestedIndependents;
        }

        public override bool IsSearchNestingOperation()
        {
            return true; // contains complete nested search program
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return OperationsList;
        }

        /// <summary>
        /// Generates match objects of independents
        /// </summary>
        protected void GenerateIndependentsMatchObjects(SourceBuilder sourceCode)
        {
            for(int i=0; i<MatchingPatternClassTypeName.Count; ++i)
            {
                if(NestedIndependents[i] == null)
                    continue;

                foreach(KeyValuePair<PatternGraph, bool> nestedIndependent in NestedIndependents[i])
                {
                    if(nestedIndependent.Value == false)
                        continue; // if independent is not nested in iterated with potentially more than one match then matcher-class-based match variables are sufficient, only one match is living at a time

                    if(nestedIndependent.Key.originalPatternGraph != null)
                    {
                        sourceCode.AppendFrontFormat("{0} {1} = new {0}();\n",
                            nestedIndependent.Key.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name + "." + NamesOfEntities.MatchClassName(nestedIndependent.Key.originalPatternGraph.pathPrefix + nestedIndependent.Key.originalPatternGraph.name),
                            NamesOfEntities.MatchedIndependentVariable(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name));
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0} {1} = new {0}();\n",
                            MatchingPatternClassTypeName[i] + "." + NamesOfEntities.MatchClassName(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name),
                            NamesOfEntities.MatchedIndependentVariable(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name));
                    }
                }
            }
        }

        protected readonly string RulePatternClassName;
        protected readonly List<string> NamesOfPatternGraphsOnPathToEnclosedPatternpath;
        public readonly string Name;
        public readonly bool Parallel;

        public string ArrayPerElementMethods;

        // List because of potentially nested independents, flattened, and also alternative cases combined in alternative
        protected readonly List<string> MatchingPatternClassTypeName;
        protected readonly List<Dictionary<PatternGraph, bool>> NestedIndependents;

        public SearchProgramList OperationsList;
    }

    /// <summary>
    /// Class representing the search program of a matching action, i.e. some test or rule
    /// The list forming concatenation field is used for adding missing preset search subprograms.
    /// </summary>
    class SearchProgramOfAction : SearchProgram
    {
        public SearchProgramOfAction(string rulePatternClassName,
            string patternName, string[] parameterTypes, string[] parameterNames, string name,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            bool containsSubpatterns, bool wasIndependentInlined,
            List<string> matchingPatternClassTypeName,
            List<Dictionary<PatternGraph, bool>> nestedIndependents,
            bool emitProfiling, string packagePrefixedPatternName, int numReturns,
            string[] dispatchConditions, List<string> suffixedMatcherNames, List<string[]> arguments)
        : base(rulePatternClassName, namesOfPatternGraphsOnPathToEnclosedPatternpath,
            name, false, matchingPatternClassTypeName, nestedIndependents)
        {
            PatternName = patternName;
            Parameters = "";
            for(int i = 0; i < parameterTypes.Length; ++i)
            {
                Parameters += ", " + parameterTypes[i] + " " + parameterNames[i];
            }
            SetupSubpatternMatching = containsSubpatterns;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedPatternName = packagePrefixedPatternName;
            NumReturns = numReturns;

            DispatchConditions = dispatchConditions;
            SuffixedMatcherNames = suffixedMatcherNames;
            Arguments = arguments;
        }

        /// <summary>
        /// Dumps search program followed by missing preset search subprograms
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of action {1}",
                Name, SetupSubpatternMatching ? "with subpattern matching setup\n" : "\n");

            // then nested content
            if(OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }

            // then next missing preset search subprogram
            if(Next != null)
            {
                Next.Dump(builder);
            }
        }

        /// <summary>
        /// Emits the matcher source code for all search programs
        /// first head of matching function of the current search program
        /// then the search program operations list in depth first walk over search program operations list
        /// then tail of matching function of the current search program
        /// and finally continues in missing preset search program list by emitting following search program
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif
            string matchType = RulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(PatternName);
            string matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            sourceCode.AppendFrontFormat("public {0} {1}("
                    + "GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches{2})\n", matchesType, Name, Parameters);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(Arguments!=null)
            {
                sourceCode.AppendFront("// maybe null dispatching\n");
                EmitMaybeNullDispatching(sourceCode, 0, 0);
            }

            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            string matchClassName = RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName);
            sourceCode.AppendFront("if(matches == null)\n");
            sourceCode.AppendFrontIndentedFormat("matches = new GRGEN_LGSP.LGSPMatchesList<" + matchClassName + ", " + matchType + ">(this);\n");
            sourceCode.AppendFront("matches.Clear();\n");
            sourceCode.AppendFront("if(ReturnArray == null)\n");
            sourceCode.AppendFrontIndentedFormat("ReturnArray = new object[{0}];\n", NumReturns);
            sourceCode.AppendFront("int isoSpace = 0;\n");
            
            if(NamesOfPatternGraphsOnPathToEnclosedPatternpath.Count > 0)
                sourceCode.AppendFront("bool searchPatternpath = false;\n");
            foreach(string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

            if(SetupSubpatternMatching)
            {
                sourceCode.AppendFront("Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<"
                        + "GRGEN_LGSP.LGSPSubpatternAction>();\n");
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<"
                        + "GRGEN_LIBGR.IMatch>>();\n");
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;\n");
            }

            GenerateIndependentsMatchObjects(sourceCode);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            if(EmitProfiling)
            {
                sourceCode.AppendFront("long searchStepsAtBegin = actionEnv.PerformanceInfo.SearchSteps;\n");
                sourceCode.AppendFront("long searchStepsAtLoopStepBegin = searchStepsAtBegin;\n");
                sourceCode.AppendFront("int loopSteps = 0;\n");
            }

            OperationsList.Emit(sourceCode);

            if(EmitProfiling)
            {
                sourceCode.AppendFrontFormat("++actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].callsTotal;\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].loopStepsTotal += loopSteps;\n", PackagePrefixedPatternName);

                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsTotal += loopSteps;\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsSingle.Add(loopSteps);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsMultiple.Add(loopSteps);\n", PackagePrefixedPatternName);
            }

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("if({0} != null)\n", 
                    NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("foreach({0} toClean in {1}.Values) toClean.CleanNextWithSameHash();\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

#if ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION
            sourceCode.AppendFront("graph.CheckEmptyFlags();\n");
#endif
            sourceCode.AppendFront("return matches;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // emit search subprograms
            if(Next != null)
            {
                Next.Emit(sourceCode);
            }
        }

        private int EmitMaybeNullDispatching(SourceBuilder sourceCode, int conditionLevel, int emittedCounter)
        {
            if(conditionLevel<DispatchConditions.Length)
            {
                sourceCode.AppendFrontFormat("if({0}!=null) {{\n", DispatchConditions[conditionLevel]);
                sourceCode.Indent();
                emittedCounter = EmitMaybeNullDispatching(sourceCode, conditionLevel+1, emittedCounter);
                sourceCode.Unindent();
                sourceCode.AppendFront("} else {\n");
                sourceCode.Indent();
                emittedCounter = EmitMaybeNullDispatching(sourceCode, conditionLevel+1, emittedCounter);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else
            {
                if(emittedCounter>0) // first entry are we ourselves, don't call, just nop
                {
                    sourceCode.AppendFrontFormat("return {0}(actionEnv, maxMatches", SuffixedMatcherNames[emittedCounter]);
                    foreach(string argument in Arguments[emittedCounter])
                    {
                        sourceCode.AppendFormat(", {0}", argument);                        
                    }
                    sourceCode.Append(");\n");
                }
                ++emittedCounter;
            }
            return emittedCounter;
        }

        public readonly string PatternName;
        public readonly string PackagePrefixedPatternName;
        public readonly string Parameters;
        public readonly bool SetupSubpatternMatching;
        public readonly bool WasIndependentInlined;
        public readonly bool EmitProfiling;
        public readonly int NumReturns;

        readonly string[] DispatchConditions;
        readonly List<string> SuffixedMatcherNames; // for maybe null dispatcher
        readonly List<string[]> Arguments; // for maybe null dispatcher
    }

    /// <summary>
    /// Class representing the search program of the head of a parallelized matching action, i.e. some test or rule
    /// </summary>
    class SearchProgramOfActionParallelizationHead : SearchProgram
    {
        public SearchProgramOfActionParallelizationHead(string rulePatternClassName,
            string patternName, string[] parameterTypes, string[] parameterNames, string name,
            bool emitProfiling, string packagePrefixedPatternName, int numReturns)
        : base(rulePatternClassName, null,
            name, true, null, null)
        {
            PatternName = patternName;
            Parameters = "";
            for(int i = 0; i < parameterTypes.Length; ++i)
            {
                Parameters += ", " + parameterTypes[i] + " " + parameterNames[i];
            }
            EmitProfiling = emitProfiling;
            PackagePrefixedPatternName = packagePrefixedPatternName;
            NumReturns = numReturns;
        }

        /// <summary>
        /// Dumps search program followed by missing preset search subprograms
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of action \n", Name);

            // then nested content
            if(OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }

            // then next missing preset search subprogram
            if(Next != null)
                Next.Dump(builder);
        }

        /// <summary>
        /// Emits the matcher source code for all search programs
        /// first head of matching function of the current search program
        /// then the search program operations list in depth first walk over search program operations list
        /// then tail of matching function of the current search program
        /// and finally continues in missing preset search program list by emitting following search program
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif
            string matchType = RulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(PatternName);
            string matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            sourceCode.AppendFront("\n");
            sourceCode.AppendFrontFormat("public {0} {1}("
                    + "GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches{2})\n", matchesType, Name, Parameters);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            //sourceCode.AppendFrontFormat("GRGEN_LIBGR.ConsoleUI.outWriter.WriteLine(\"called matcher for {0}\");\n", PatternName);
            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            sourceCode.AppendFront("if(matches == null)\n");
            string matchClassName = RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName);
            sourceCode.AppendFrontIndentedFormat("matches = new GRGEN_LGSP.LGSPMatchesList<" + matchClassName + ", " + matchType + ">(this);\n");
            sourceCode.AppendFront("matches.Clear();\n");
            sourceCode.AppendFront("if(ReturnArray == null)\n");
            sourceCode.AppendFrontIndentedFormat("ReturnArray = new object[{0}];\n", NumReturns);
            sourceCode.AppendFront("int isoSpace = 0;\n");
            sourceCode.AppendFront("actionEnvParallel = actionEnv;\n");
            sourceCode.AppendFront("maxMatchesParallel = maxMatches;\n");
            sourceCode.AppendFront("graph.EnsureSufficientIsomorphySpacesForParallelizedMatchingAreAvailable(numWorkerThreads);\n");
            sourceCode.AppendFront("List<ushort> flagsPerElement0 = graph.flagsPerThreadPerElement[0];\n");
            sourceCode.AppendFront("int numThreadsSignaled = 0;\n");

            if(EmitProfiling)
            {
                sourceCode.AppendFront("actionEnv.PerformanceInfo.ResetStepsPerThread(numWorkerThreads);\n");
                sourceCode.AppendFront("bool parallelMatcherUsed = false;\n");

                sourceCode.AppendFront("long searchStepsAtBegin = actionEnv.PerformanceInfo.SearchSteps;\n");
                sourceCode.AppendFront("long searchStepsAtLoopStepBegin = searchStepsAtBegin;\n");
                sourceCode.AppendFront("int loopSteps = 0;\n");
            }

            OperationsList.Emit(sourceCode);

            if(EmitProfiling)
            {
                sourceCode.AppendFront("if(parallelMatcherUsed)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("++actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].callsTotal;\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].loopStepsTotal += loopSteps;\n", PackagePrefixedPatternName);

                sourceCode.AppendFront("for(int i=0; i<numThreadsSignaled; ++i)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[i].searchStepsTotal += actionEnv.PerformanceInfo.SearchStepsPerThread[i];\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].searchStepsTotal += actionEnv.PerformanceInfo.SearchStepsPerThread[i];\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.SearchSteps += actionEnv.PerformanceInfo.SearchStepsPerThread[i];\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[i].loopStepsTotal += actionEnv.PerformanceInfo.LoopStepsPerThread[i];\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].loopStepsTotal += actionEnv.PerformanceInfo.LoopStepsPerThread[i];\n", PackagePrefixedPatternName);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

#if ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION
            sourceCode.AppendFront("graph.CheckEmptyFlags();\n");
#endif
            sourceCode.AppendFront("return matches;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // emit search subprograms
            if(Next != null)
                Next.Emit(sourceCode);
        }

        public readonly string PatternName;
        public readonly string Parameters;
        public readonly bool EmitProfiling;
        public readonly string PackagePrefixedPatternName;
        public readonly int NumReturns;
    }

    /// <summary>
    /// Class representing the search program of the body of a parallelized matching action, i.e. some test or rule
    /// </summary>
    class SearchProgramOfActionParallelizationBody : SearchProgram
    {
        public SearchProgramOfActionParallelizationBody(string rulePatternClassName,
            string patternName, string name,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            bool containsSubpatterns, bool wasIndependentInlined,
            List<string> matchingPatternClassTypeName,
            List<Dictionary<PatternGraph, bool>> nestedIndependents,
            bool emitProfiling, string packagePrefixedPatternName)
        : base(rulePatternClassName, namesOfPatternGraphsOnPathToEnclosedPatternpath,
            name, true, matchingPatternClassTypeName, nestedIndependents)
        {
            PatternName = patternName;
            SetupSubpatternMatching = containsSubpatterns;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedPatternName = packagePrefixedPatternName;
        }

        /// <summary>
        /// Dumps search program followed by missing preset search subprograms
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of action {1}",
                Name, SetupSubpatternMatching ? "with subpattern matching setup\n" : "\n");

            // then nested content
            if(OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }

            // then next missing preset search subprogram
            if(Next != null)
                Next.Dump(builder);
        }

        /// <summary>
        /// Emits the matcher source code for all search programs
        /// first head of matching function of the current search program
        /// then the search program operations list in depth first walk over search program operations list
        /// then tail of matching function of the current search program
        /// and finally continues in missing preset search program list by emitting following search program
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            string matchType = RulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(PatternName);
            string matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            sourceCode.AppendFront("\n");
            sourceCode.AppendFrontFormat("private void {0}()\n", Name);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("threadId = GRGEN_LGSP.WorkerPool.ThreadId;\n");
            //sourceCode.AppendFrontFormat("GRGEN_LIBGR.ConsoleUI.outWriter.WriteLine(\"start work for {0} at threadId \" + threadId);\n", PatternName);
            sourceCode.AppendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = actionEnvParallel;\n");
            sourceCode.AppendFront("int maxMatches = maxMatchesParallel;\n");
            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            sourceCode.AppendFront("List<ushort> flagsPerElement = graph.flagsPerThreadPerElement[threadId];\n");
            sourceCode.AppendFront("List<ushort> flagsPerElement0 = graph.flagsPerThreadPerElement[0];\n");
            sourceCode.AppendFront("List<ushort> flagsPerElementGlobal = graph.flagsPerThreadPerElement[threadId];\n");
            sourceCode.AppendFront("int isoSpace = 0;\n");

            if(NamesOfPatternGraphsOnPathToEnclosedPatternpath.Count > 0)
                sourceCode.AppendFront("bool searchPatternpath = false;\n");
            foreach(string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

            if(SetupSubpatternMatching)
            {
                sourceCode.AppendFront("Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<"
                        + "GRGEN_LGSP.LGSPSubpatternAction>();\n");
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<"
                        + "GRGEN_LIBGR.IMatch>>();\n");
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;\n");
            }

            GenerateIndependentsMatchObjects(sourceCode);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            if(EmitProfiling)
                sourceCode.AppendFront("long searchStepsAtLoopStepBegin;\n");

            OperationsList.Emit(sourceCode);

            //sourceCode.AppendFrontFormat("GRGEN_LIBGR.ConsoleUI.outWriter.WriteLine(\"work done for {0} at threadId \" + threadId);\n", PatternName);
            if(EmitProfiling)
            {
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsSingle.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsMultiple.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedPatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedPatternName);
            }

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("if({0} != null)\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("foreach({0} toClean in {1}.Values) toClean.CleanNextWithSameHash();\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            Debug.Assert(Next == null);
        }

        public readonly string PatternName;
        public readonly bool SetupSubpatternMatching;
        public readonly bool WasIndependentInlined;
        public readonly bool EmitProfiling;
        public readonly string PackagePrefixedPatternName;
    }

    /// <summary>
    /// Class representing the search program of a subpattern
    /// </summary>
    class SearchProgramOfSubpattern : SearchProgram
    {
        public SearchProgramOfSubpattern(string rulePatternClassName,
            string patternName,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name,
            bool wasIndependentInlined,
            List<string> matchingPatternClassTypeName,
            List<Dictionary<PatternGraph, bool>> nestedIndependents,
            bool parallel)
        : base(rulePatternClassName, namesOfPatternGraphsOnPathToEnclosedPatternpath,
            name, parallel, matchingPatternClassTypeName, nestedIndependents)
        {
            PatternName = patternName;
            WasIndependentInlined = wasIndependentInlined;
        }

        /// <summary>
        /// Dumps search program 
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of subpattern\n", Parallel ? Name + "_parallelized" : Name);
            builder.Append("\n");

            // then nested content
            if(OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits the matcher source code for the search program
        /// head, search program operations list in depth first walk over search program operations list, tail
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

            if(Parallel)
            {
                sourceCode.AppendFront("public override void " + Name + "_parallelized"
                    + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                    + "int maxMatches, int isoSpace, int threadId)\n");
            }
            else
            {
                sourceCode.AppendFront("public override void " + Name
                    + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                    + "int maxMatches, int isoSpace)\n");
            }
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            if(Parallel)
            {
                sourceCode.AppendFront("List<ushort> flagsPerElement = graph.flagsPerThreadPerElement[threadId];\n");
                sourceCode.AppendFront("List<ushort> flagsPerElement0 = graph.flagsPerThreadPerElement[0];\n");
                sourceCode.AppendFront("List<ushort> flagsPerElementGlobal = graph.flagsPerThreadPerElement[threadId];\n");
            }

            foreach(string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

            GenerateIndependentsMatchObjects(sourceCode);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            OperationsList.Emit(sourceCode);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("if({0} != null)\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("foreach({0} toClean in {1}.Values) toClean.CleanNextWithSameHash();\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly string PatternName;
        public readonly bool WasIndependentInlined;
    }

    /// <summary>
    /// Class representing the search program of an alternative
    /// </summary>
    class SearchProgramOfAlternative : SearchProgram
    {
        public SearchProgramOfAlternative(string rulePatternClassName,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name,
            List<string> matchingPatternClassTypeName,
            List<Dictionary<PatternGraph, bool>> nestedIndependents,
            bool parallel)
        : base(rulePatternClassName, namesOfPatternGraphsOnPathToEnclosedPatternpath,
            name, parallel, matchingPatternClassTypeName, nestedIndependents)
        {
        }

        /// <summary>
        /// Dumps search program
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of alternative case\n", Parallel ? Name + "_parallelized" : Name);

            // then nested content
            if(OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits the matcher source code for the search program
        /// head, search program operations list in depth first walk over search program operations list, tail
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

            if(Parallel)
            {
                sourceCode.AppendFront("public override void " + Name + "_parallelized"
                    + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                    + "int maxMatches, int isoSpace, int threadId)\n");
            }
            else
            {
                sourceCode.AppendFront("public override void " + Name
                    + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                    + "int maxMatches, int isoSpace)\n");
            }
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            if(Parallel)
            {
                sourceCode.AppendFront("List<ushort> flagsPerElement = graph.flagsPerThreadPerElement[threadId];\n");
                sourceCode.AppendFront("List<ushort> flagsPerElement0 = graph.flagsPerThreadPerElement[0];\n");
                sourceCode.AppendFront("List<ushort> flagsPerElementGlobal = graph.flagsPerThreadPerElement[threadId];\n");
            }

            foreach(string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

            GenerateIndependentsMatchObjects(sourceCode);

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }
    }

    /// <summary>
    /// Class representing the search program of an iterated pattern 
    /// </summary>
    class SearchProgramOfIterated : SearchProgram
    {
        public SearchProgramOfIterated(string rulePatternClassName,
            string patternName,
            string iterPatternName,
            string iterPathPrefix,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name,
            bool wasIndependentInlined,
            List<string> matchingPatternClassTypeName,
            List<Dictionary<PatternGraph, bool>> nestedIndependents,
            bool parallel)
        : base(rulePatternClassName, namesOfPatternGraphsOnPathToEnclosedPatternpath,
            name, parallel, matchingPatternClassTypeName, nestedIndependents)
        {
            PatternName = patternName;
            IterPathPrefix = iterPathPrefix;
            IterPatternName = iterPatternName;
            WasIndependentInlined = wasIndependentInlined;
        }

        /// <summary>
        /// Dumps search program 
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of iterated\n", Parallel ? Name + "_parallelized" : Name);
            builder.Append("\n");

            // then nested content
            if(OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits the matcher source code for the search program
        /// head, search program operations list in depth first walk over search program operations list, tail
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

            if(Parallel)
            {
                sourceCode.AppendFront("public override void " + Name + "_parallelized"
                    + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                    + "int maxMatches, int isoSpace, int threadId)\n");
            }
            else
            {
                sourceCode.AppendFront("public override void " + Name
                    + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                    + "int maxMatches, int isoSpace)\n");
            }
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("bool patternFound = false;\n");
            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            if(Parallel)
            {
                sourceCode.AppendFront("List<ushort> flagsPerElement = graph.flagsPerThreadPerElement[threadId];\n");
                sourceCode.AppendFront("List<ushort> flagsPerElement0 = graph.flagsPerThreadPerElement[0];\n");
                sourceCode.AppendFront("List<ushort> flagsPerElementGlobal = graph.flagsPerThreadPerElement[threadId];\n");
            }

            foreach(string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

            GenerateIndependentsMatchObjects(sourceCode);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(IterPathPrefix + IterPatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            OperationsList.Emit(sourceCode);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("if({0} != null)\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("foreach({0} toClean in {1}.Values) toClean.CleanNextWithSameHash();\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(IterPathPrefix + IterPatternName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly string PatternName;
        public readonly string IterPatternName;
        public readonly string IterPathPrefix;
        public readonly bool WasIndependentInlined;
    }
}
