/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Base class for all search program operations, containing concatenation fields,
    /// so that search program operations can form a linked search program list
    /// - double linked list; next points to the following list element or null;
    /// previous points to the preceding list element 
    /// or the enclosing search program operation within the list anchor element
    /// </summary>
    abstract class SearchProgramOperation
    {
        public SearchProgramOperation Next;
        public SearchProgramOperation Previous;

        /// <summary>
        /// dumps search program operation (as string) into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Dump(SourceBuilder builder);

        /// <summary>
        /// emits c# code implementing search program operation into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Emit(SourceBuilder sourceCode);

        /// <summary>
        /// Appends the given element to the search program operations list
        /// whose closing element until now was this element.
        /// Returns the new closing element - the given element.
        /// </summary>
        public SearchProgramOperation Append(SearchProgramOperation newElement)
        {
            Debug.Assert(Next == null, "Append only at end of list");
            Debug.Assert(newElement.Previous == null, "Append only of element without predecessor");
            Next = newElement;
            newElement.Previous = this;
            return newElement;
        }

        /// <summary>
        /// Insert the given element into the search program operations list
        /// between this and the succeeding element.
        /// Returns the element after this - the given element.
        /// </summary>
        public SearchProgramOperation Insert(SearchProgramOperation newElement)
        {
            Debug.Assert(newElement.Previous == null, "Insert only of single unconnected element (previous)");
            Debug.Assert(newElement.Next == null, "Insert only of single unconnected element (next)");
            
            if (Next == null)
            {
                return Append(newElement);
            }

            SearchProgramOperation Successor = Next;
            Next = newElement;
            newElement.Next = Successor;
            Successor.Previous = newElement;
            newElement.Previous = this;           
            return newElement;
        }

        /// <summary>
        /// returns whether operation is a search nesting operation 
        /// containing other elements within some list inside
        /// bearing the search nesting/iteration structure.
        /// default: false (cause only few operations are search nesting operations)
        /// </summary>
        public virtual bool IsSearchNestingOperation()
        {
            return false;
        }

        /// <summary>
        /// returns the nested search operations list anchor
        /// null if list not created or IsSearchNestingOperation == false.
        /// default: null (cause only few search operations are nesting operations)
        /// </summary>
        public virtual SearchProgramOperation GetNestedSearchOperationsList()
        {
            return null;
        }

        /// <summary>
        /// returns operation enclosing this operation
        /// </summary>
        public SearchProgramOperation GetEnclosingSearchOperation()
        {
            SearchProgramOperation potentiallyNestingOperation = this;
            SearchProgramOperation nestedOperation;

            // iterate list leftwards, leftmost list element is list anchor element,
            // which contains uplink to enclosing search operation in it's previous member
            // step over search nesting operations we're not nested in 
            do
            {
                nestedOperation = potentiallyNestingOperation;
                potentiallyNestingOperation = nestedOperation.Previous;
            }
            while (!potentiallyNestingOperation.IsSearchNestingOperation() 
                || potentiallyNestingOperation.GetNestedSearchOperationsList()!=nestedOperation);

            return potentiallyNestingOperation;
        }
    }

    /// <summary>
    /// Search program list anchor element,
    /// containing first list element within inherited Next member
    /// Inherited to be able to access the first element via Next
    /// Previous points to enclosing search program operation
    /// (starts list, but doesn't contain one)
    /// </summary>
    class SearchProgramList : SearchProgramOperation
    {
        public SearchProgramList(SearchProgramOperation enclosingOperation)
        {
            Previous = enclosingOperation;
        }

        public override void Dump(SourceBuilder builder)
        {
            SearchProgramOperation currentOperation = Next;

            // depth first walk over nested search program lists
            // walk current list here, recursive descent within local dump-methods
            while (currentOperation != null)
            {
                currentOperation.Dump(builder);
                currentOperation = currentOperation.Next;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            SearchProgramOperation currentOperation = Next;

            // depth first walk over nested search program lists
            // walk current list here, recursive descent within local Emit-methods
            while (currentOperation != null)
            {
                currentOperation.Emit(sourceCode);
                currentOperation = currentOperation.Next;
            }
        }
    }

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
        public override bool IsSearchNestingOperation()
        {
            return true; // contains complete nested search program
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return OperationsList;
        }

        protected string RulePatternClassName;
        protected List<string> NamesOfPatternGraphsOnPathToEnclosedPatternpath;
        public string Name;
        public bool Parallel;

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
            bool containsSubpatterns, bool wasIndependentInlined, bool emitProfiling,
            string[] dispatchConditions, List<string> suffixedMatcherNames, List<string[]> arguments)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;
            Parallel = false;

            PatternName = patternName;
            Parameters = "";
            for (int i = 0; i < parameterTypes.Length; ++i)
            {
                Parameters += ", " + parameterTypes[i] + " " + parameterNames[i];
            }
            SetupSubpatternMatching = containsSubpatterns;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;

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
            if (OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }

            // then next missing preset search subprogram
            if (Next != null)
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
            sourceCode.AppendFront("matches.Clear();\n");
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

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName), NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            if(EmitProfiling)
            {
                sourceCode.AppendFront("long searchStepsAtBegin = actionEnv.PerformanceInfo.SearchSteps;\n");
                sourceCode.AppendFront("int loopSteps = 0;\n");
            }

            OperationsList.Emit(sourceCode);

            if(EmitProfiling)
            {
                sourceCode.AppendFrontFormat("++actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].callsTotal;\n", PatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", PatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].loopStepsTotal += loopSteps;\n", PatternName);

                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", PatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsTotal += loopSteps;\n", PatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", PatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", PatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsSingle.Add(loopSteps);\n", PatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsMultiple.Add(loopSteps);\n", PatternName);
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
                    foreach(string argument in Arguments[emittedCounter]) {
                        sourceCode.AppendFormat(", {0}", argument);                        
                    }
                    sourceCode.Append(");\n");
                }
                ++emittedCounter;
            }
            return emittedCounter;
        }

        public string PatternName;
        public string Parameters;
        public bool SetupSubpatternMatching;
        public bool WasIndependentInlined;
        public bool EmitProfiling;

        string[] DispatchConditions;
        List<string> SuffixedMatcherNames; // for maybe null dispatcher
        List<string[]> Arguments; // for maybe null dispatcher
    }

    /// <summary>
    /// Class representing the search program of the head of a parallelized matching action, i.e. some test or rule
    /// </summary>
    class SearchProgramOfActionParallelizationHead : SearchProgram
    {
        public SearchProgramOfActionParallelizationHead(string rulePatternClassName,
            string patternName, string[] parameterTypes, string[] parameterNames, string name,
            bool emitProfiling)
        {
            RulePatternClassName = rulePatternClassName;
            Name = name;
            Parallel = true;

            PatternName = patternName;
            Parameters = "";
            for(int i = 0; i < parameterTypes.Length; ++i)
            {
                Parameters += ", " + parameterTypes[i] + " " + parameterNames[i];
            }
            EmitProfiling = emitProfiling;
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
            sourceCode.AppendFront("\n");
            sourceCode.AppendFrontFormat("public {0} {1}("
                    + "GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches{2})\n", matchesType, Name, Parameters);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            //sourceCode.AppendFrontFormat("Console.WriteLine(\"called matcher for {0}\");\n", PatternName);
            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            sourceCode.AppendFront("matches.Clear();\n");
            sourceCode.AppendFront("int isoSpace = 0;\n");
            sourceCode.AppendFront("actionEnvParallel = actionEnv;\n");
            sourceCode.AppendFront("maxMatchesParallel = maxMatches;\n");
            sourceCode.AppendFront("graph.EnsureSufficientIsomorphySpacesForParallelizedMatchingAreAvailable(numWorkerThreads);\n");
            sourceCode.AppendFront("List<ushort> flagsPerElement0 = graph.flagsPerThreadPerElement[0];\n");

            if(EmitProfiling)
            {
                sourceCode.AppendFront("actionEnv.PerformanceInfo.ResetStepsPerThread(numWorkerThreads);\n");
                sourceCode.AppendFront("bool parallelMatcherUsed = false;\n");
            }

            OperationsList.Emit(sourceCode);

            if(EmitProfiling)
            {
                sourceCode.AppendFront("if(parallelMatcherUsed)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("++actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].callsTotal;\n", PatternName);
                sourceCode.AppendFront("for(int i=0; i<numThreadsSignaled; ++i)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[i].searchStepsTotal += actionEnv.PerformanceInfo.SearchStepsPerThread[i];\n", PatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].searchStepsTotal += actionEnv.PerformanceInfo.SearchStepsPerThread[i];\n", PatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.SearchSteps += actionEnv.PerformanceInfo.SearchStepsPerThread[i];\n", PatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[i].loopStepsTotal += actionEnv.PerformanceInfo.LoopStepsPerThread[i];\n", PatternName);
                sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].loopStepsTotal += actionEnv.PerformanceInfo.LoopStepsPerThread[i];\n", PatternName);
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
            {
                Next.Emit(sourceCode);
            }
        }

        public string PatternName;
        public string Parameters;
        public bool EmitProfiling;
    }

    /// <summary>
    /// Class representing the search program of the body of a parallelized matching action, i.e. some test or rule
    /// </summary>
    class SearchProgramOfActionParallelizationBody : SearchProgram
    {
        public SearchProgramOfActionParallelizationBody(string rulePatternClassName,
            string patternName, string name,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            bool containsSubpatterns, bool wasIndependentInlined, bool emitProfiling)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;
            Parallel = true;

            PatternName = patternName;
            SetupSubpatternMatching = containsSubpatterns;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
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
            string matchType = RulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(PatternName);
            string matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            sourceCode.AppendFront("\n");
            sourceCode.AppendFrontFormat("private void {0}()\n", Name);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("threadId = GRGEN_LGSP.WorkerPool.ThreadId;\n");
            //sourceCode.AppendFrontFormat("Console.WriteLine(\"start work for {0} at threadId \" + threadId);\n", PatternName);
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

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName), NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            OperationsList.Emit(sourceCode);

            //sourceCode.AppendFrontFormat("Console.WriteLine(\"work done for {0} at threadId \" + threadId);\n", PatternName);
            if(EmitProfiling)
            {
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", PatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", PatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsSingle.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", PatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsMultiple.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", PatternName);
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PatternName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PatternName);
            }

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            Debug.Assert(Next == null);
        }

        public string PatternName;
        public bool SetupSubpatternMatching;
        public bool WasIndependentInlined;
        public bool EmitProfiling;
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
            bool wasIndependentInlined, bool parallel)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;
            WasIndependentInlined = wasIndependentInlined;
            Parallel = parallel;
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
            if (OperationsList != null)
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

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName), NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string PatternName;
        public bool WasIndependentInlined;
    }

    /// <summary>
    /// Class representing the search program of an alternative
    /// </summary>
    class SearchProgramOfAlternative : SearchProgram
    {
        public SearchProgramOfAlternative(string rulePatternClassName,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name,
            bool parallel)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;
            Parallel = parallel;
        }

        /// <summary>
        /// Dumps search program
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of alternative case\n", Parallel ? Name + "_parallelized" : Name);

            // then nested content
            if (OperationsList != null)
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
            bool wasIndependentInlined, bool parallel)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            IterPathPrefix = iterPathPrefix;
            IterPatternName = iterPatternName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;
            WasIndependentInlined = wasIndependentInlined;
            Parallel = parallel;
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
            if (OperationsList != null)
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

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(IterPathPrefix + IterPatternName), NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string PatternName;
        public string IterPatternName;
        public string IterPathPrefix;
        public bool WasIndependentInlined;
    }

    /// <summary>
    /// Class representing "match the pattern of the alternative case" operation
    /// </summary>
    class GetPartialMatchOfAlternative : SearchProgramOperation
    {
        public GetPartialMatchOfAlternative(string pathPrefix, string caseName, 
            string rulePatternClassName, bool wasIndependentInlined)
        {
            PathPrefix = pathPrefix;
            CaseName = caseName;
            RulePatternClassName = rulePatternClassName;
            WasIndependentInlined = wasIndependentInlined;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("GetPartialMatchOfAlternative case {0}{1}\n", PathPrefix, CaseName);

            // then nested content
            if (OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Alternative case {0}{1} \n", PathPrefix, CaseName);

            sourceCode.AppendFront("do {\n");
            sourceCode.Indent();
            string whichCase = RulePatternClassName + "." + PathPrefix + "CaseNums.@" + CaseName;
            sourceCode.AppendFrontFormat("patternGraph = patternGraphs[(int){0}];\n", whichCase);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PathPrefix + CaseName), NamesOfEntities.FoundMatchesForFilteringVariable());
            }

            OperationsList.Emit(sourceCode);
            
            sourceCode.Unindent();
            sourceCode.AppendFront("} while(false);\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true; // contains complete nested search program of alternative case
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return OperationsList;
        }

        public string PathPrefix;
        public string CaseName;
        public string RulePatternClassName;
        public bool WasIndependentInlined;

        public SearchProgramList OperationsList;
    }

    /// <summary>
    /// Class representing "draw variable from input parameters array" operation
    /// </summary>
    class ExtractVariable : SearchProgramOperation
    {
        public ExtractVariable(string varType, string varName)
        {
            VarType = varType;
            VarName = varName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("ExtractVariable " + VarName + ":" + VarType + "\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront(VarType + " " + NamesOfEntities.Variable(VarName) + " = (" + VarType + ")" + VarName + ";\n");
        }

        public string VarType;
        public string VarName;
    }

    /// <summary>
    /// Available entity types 
    /// </summary>
    enum EntityType
    {
        Node,
        Edge,
        Variable
    }

    /// <summary>
    /// Class representing "declare a def to be yielded to variable" operation
    /// </summary>
    class DeclareDefElement : SearchProgramOperation
    {
        public DeclareDefElement(EntityType entityType, string typeOfEntity, string nameOfEntity, string initialization)
        {
            Type = entityType;
            TypeOfEntity = typeOfEntity;
            NameOfEntity = nameOfEntity;
            Initialization = initialization;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("Declare def " + NamesOfEntities.ToString(Type) + " " + NameOfEntity + ":" + TypeOfEntity +  " = " + Initialization + "\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == EntityType.Node)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.CandidateVariable(NameOfEntity) + " = " + Initialization + ";\n");
            else if(Type == EntityType.Edge)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.CandidateVariable(NameOfEntity) + " = " + Initialization + ";\n");
            else //if(Type == EntityType.Variable)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.Variable(NameOfEntity) + " = " + Initialization + ";\n");
        }

        public EntityType Type;
        public string TypeOfEntity;
        public string NameOfEntity;
        public string Initialization; // only valid if Variable, only not null if initialization given
    }

    /// <summary>
    /// Base class for search program check operations
    /// contains list anchor for operations to execute when check failed
    /// (check is not a search operation, thus the check failed operations are not search nested operations)
    /// </summary>
    abstract class CheckOperation : SearchProgramOperation
    {
        // (nested) operations to execute when check failed
        public SearchProgramList CheckFailedOperations;
    }

    /// <summary>
    /// Base class for search program type determining operations,
    /// setting current type for following get candidate operation
    /// </summary>
    abstract class GetType : SearchProgramOperation
    {
    }

    /// <summary>
    /// Available types of GetTypeByIteration operations
    /// </summary>
    enum GetTypeByIterationType
    {
        ExplicitelyGiven, // iterate the explicitely given types
        AllCompatible // iterate all compatible types of the pattern element type
    }

    /// <summary>
    /// Class representing "iterate over the allowed types" operation,
    /// setting type id to use in the following get candidate by element iteration
    /// </summary>
    class GetTypeByIteration : GetType
    {
        public GetTypeByIteration(
            GetTypeByIterationType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeName,
            bool isNode)
        {
            Type = type;
            PatternElementName = patternElementName;
            if (type == GetTypeByIterationType.ExplicitelyGiven) {
                TypeName = rulePatternTypeNameOrTypeName;
            } else { // type == GetTypeByIterationType.AllCompatible
                RulePatternTypeName = rulePatternTypeNameOrTypeName;
            }
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetType ByIteration ");
            if(Type==GetTypeByIterationType.ExplicitelyGiven) {
                builder.Append("ExplicitelyGiven ");
                builder.AppendFormat("on {0} in {1} node:{2}\n", 
                    PatternElementName, TypeName, IsNode);
            } else { // Type==GetTypeByIterationType.AllCompatible
                builder.Append("AllCompatible ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, RulePatternTypeName, IsNode);
            }
            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            // todo: randomisierte auswahl des typen wenn RANDOM_LOOKUP_LIST_START ?

            // emit type iteration loop header
            string typeOfVariableContainingType = NamesOfEntities.TypeOfVariableContainingType(IsNode);
            string variableContainingTypeForCandidate = 
                NamesOfEntities.TypeForCandidateVariable(PatternElementName);
            string containerWithAvailableTypes;
            if (Type == GetTypeByIterationType.ExplicitelyGiven)
            {
                containerWithAvailableTypes = TypeName
                    + "." + PatternElementName + "_AllowedTypes";
            }
            else //(Type == GetTypeByIterationType.AllCompatible)
            {
                containerWithAvailableTypes = RulePatternTypeName
                    + ".typeVar.SubOrSameTypes";
            }
            
            sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                typeOfVariableContainingType, variableContainingTypeForCandidate,
                containerWithAvailableTypes);

            // open loop
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit type id setting and loop body 
            string variableContainingTypeIDForCandidate = 
                NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
            sourceCode.AppendFrontFormat("int {0} = {1}.TypeID;\n",
                variableContainingTypeIDForCandidate, variableContainingTypeForCandidate);

            NestedOperationsList.Emit(sourceCode);

            // close loop
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public GetTypeByIterationType Type;
        public string PatternElementName;
        public string RulePatternTypeName; // only valid if ExplicitelyGiven
        public string TypeName; // only valid if AllCompatible
        public bool IsNode; // node|edge

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "get the allowed type" operation,
    /// setting type id to use in the following get candidate by element iteration
    /// </summary>
    class GetTypeByDrawing : GetType
    {
        public GetTypeByDrawing(
            string patternElementName,
            string typeID,
            bool isNode)
        {
            PatternElementName = patternElementName;
            TypeID = typeID;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GetType GetTypeByDrawing ");
            builder.AppendFormat("on {0} id:{1} node:{2}\n",
                PatternElementName, TypeID, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            string variableContainingTypeIDForCandidate = 
                NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
            sourceCode.AppendFrontFormat("int {0} = {1};\n",
                variableContainingTypeIDForCandidate, TypeID);
        }

        public string PatternElementName;
        public string TypeID;
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Base class for search program candidate determining operations,
    /// setting current candidate for following check candidate operation
    /// </summary>
    abstract class GetCandidate : SearchProgramOperation
    {
        public string PatternElementName;
    }

    /// <summary>
    /// Available types of GetCandidateByIteration operations
    /// </summary>
    enum GetCandidateByIterationType
    {
        GraphElements, // available graph elements
        IncidentEdges, // incident edges
        StorageElements, // available elements of the storage variable
        StorageAttributeElements, // available elements of the storage attribute
        IndexElements, // available elements of the index
        //GraphElementAttribute, // available graph element of the attribute
        //GlobalVariable, // available element of the global non-storage variable
        //GlobalVariableStorageElements // available elements of the storage global variable
    } // TODO: letzte 3

    /// <summary>
    /// The different possibilites an edge might be incident to some node
    /// incoming; outgoing; incoming or outgoing if arbitrary directed, undirected, arbitrary
    /// </summary>
    enum IncidentEdgeType
    {
        Incoming,
        Outgoing,
        IncomingOrOutgoing
    }

    /// <summary>
    /// The different possibilties an index might be accessed
    /// </summary>
    enum IndexAccessType
    {
        Equality,
        Ascending,
        Descending
    }

    /// <summary>
    /// Class representing "get candidate by iteration" operations,
    /// setting current candidate for following check candidate operation
    /// </summary>
    class GetCandidateByIteration : GetCandidate
    {
        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageName = storageName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageOwnerName,
            string storageOwnerTypeName,
            string storageAttributeName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageAttributeElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageOwnerName = storageOwnerName;
            StorageOwnerTypeName = storageOwnerTypeName;
            StorageAttributeName = storageAttributeName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string equality,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            PatternElementName = patternElementName;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Equality);
            IndexAccessType = indexAccessType;
            IndexEqual = equality;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string from,
            bool fromIncluded,
            string to,
            bool toIncluded,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            PatternElementName = patternElementName;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Ascending || indexAccessType == IndexAccessType.Descending);
            IndexAccessType = indexAccessType;
            IndexFrom = from;
            IndexFromIncluded = fromIncluded;
            IndexTo = to;
            IndexToIncluded = toIncluded;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType edgeType,
            bool parallel,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            EdgeType = edgeType;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetCandidate ByIteration ");
            if (Type == GetCandidateByIterationType.GraphElements) {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else if(Type == GetCandidateByIterationType.StorageElements) {
                builder.Append("StorageElements ");
                builder.AppendFormat("on {0} from {1} node:{2} {3}\n",
                    PatternElementName, StorageName, IsNode, IsDict?"Dictionary":"List/Deque");
            } else if(Type == GetCandidateByIterationType.StorageAttributeElements) {
                builder.Append("StorageAttributeElements ");
                builder.AppendFormat("on {0} from {1}.{2} node:{3} {4}\n",
                    PatternElementName, StorageOwnerName, StorageAttributeName, IsNode, IsDict?"Dictionary":"List/Deque");
            } else if(Type == GetCandidateByIterationType.IndexElements) {
                builder.Append("IndexElements ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, IndexName, IsNode);
            } else { //Type==GetCandidateByIterationType.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} edge type:{2}\n",
                    PatternElementName, StartingPointNodeName, EdgeType.ToString());
            }
            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == GetCandidateByIterationType.GraphElements)
            {
                // code comments: lookup comment was already emitted with type iteration/drawing

                // open loop header 
                sourceCode.AppendFrontFormat("for(");
                // emit declaration of variable containing graph elements list head
                string typeOfVariableContainingListHead = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingListHead =
                    NamesOfEntities.CandidateIterationListHead(PatternElementName);
                sourceCode.AppendFormat("{0} {1}",
                    typeOfVariableContainingListHead, variableContainingListHead);
                // emit initialization of variable containing graph elements list head
                string graphMemberContainingListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                string variableContainingTypeIDForCandidate =
                    NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
                sourceCode.AppendFormat(" = graph.{0}[{1}], ",
                    graphMemberContainingListHeadByType, variableContainingTypeIDForCandidate);
                // emit declaration and initialization of variable containing candidates
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFormat("{0} = {1}.lgspTypeNext; ",
                    variableContainingCandidate, variableContainingListHead);
                // emit loop condition: check for head reached again 
                sourceCode.AppendFormat("{0} != {1}; ",
                    variableContainingCandidate, variableContainingListHead);
                // emit loop increment: switch to next element of same type
                sourceCode.AppendFormat("{0} = {0}.lgspTypeNext",
                    variableContainingCandidate);
                // close loop header
                sourceCode.Append(")\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict?"Dictionary":"List/Deque", StorageName);
                }

                // emit loop header with variable containing container entry
                string variableContainingStorage =
                    NamesOfEntities.Variable(StorageName);
                string storageIterationVariable = 
                    NamesOfEntities.CandidateIterationContainerEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                    IterationType, storageIterationVariable, variableContainingStorage);
                
                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, 
                    storageIterationVariable, IsDict?".Key":"");

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}.{4}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict?"Dictionary":"List/Deque", StorageOwnerName, StorageAttributeName);
                }

                // emit loop header with variable containing container entry
                string variableContainingStorage =
                    "((" + StorageOwnerTypeName + ")" + NamesOfEntities.CandidateVariable(StorageOwnerName) + ")." + StorageAttributeName;
                string storageIterationVariable =
                    NamesOfEntities.CandidateIterationContainerEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                    IterationType, storageIterationVariable, variableContainingStorage);

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, 
                    storageIterationVariable, IsDict?".Key":"");

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from index {2}\n",
                        IsNode ? "node" : "edge", PatternElementName, IndexName);
                }

                // emit loop header with variable containing container entry
                string indexIterationVariable =
                    NamesOfEntities.CandidateIterationIndexEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in (({2})graph.indices).{3}.",
                        IterationType, indexIterationVariable, IndexSetType, IndexName);
                if(IndexAccessType==IndexAccessType.Equality)
                {
                    sourceCode.AppendFormat("Lookup({0}))\n", IndexEqual);
                }
                else
                {
                    String accessType = IndexAccessType == IndexAccessType.Ascending ? "Ascending" : "Descending";
                    String indexFromIncluded = IndexFromIncluded ? "Inclusive" : "Exclusive";
                    String indexToIncluded = IndexToIncluded ? "Inclusive" : "Exclusive";
                    if(IndexFrom != null && IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}To{2}({3}, {4}))\n",
                            accessType, indexFromIncluded, indexToIncluded, IndexFrom, IndexTo);
                    else if(IndexFrom != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}({2}))\n",
                            accessType, indexFromIncluded, IndexFrom);
                    else if(IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}To{1}({2}))\n",
                            accessType, indexToIncluded, IndexTo);
                    else
                        sourceCode.AppendFormat("Lookup{0}())\n",
                            accessType);
                }

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    indexIterationVariable);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if (sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Extend {0} {1} from {2} \n",
                            EdgeType.ToString(), PatternElementName, StartingPointNodeName);
                }

                if (EdgeType != IncidentEdgeType.IncomingOrOutgoing)
                {
                    // emit declaration of variable containing incident edges list head
                    string variableContainingListHead =
                        NamesOfEntities.CandidateIterationListHead(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0}", variableContainingListHead);
                    // emit initialization of variable containing incident edges list head
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    string memberOfNodeContainingListHead =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInhead" : "lgspOuthead";
                    sourceCode.AppendFormat(" = {0}.{1};\n",
                        variableContainingStartingPointNode, memberOfNodeContainingListHead);

                    // emit execute the following code only if head != null
                    // todo: replace by check == null and continue
                    sourceCode.AppendFrontFormat("if({0} != null)\n", variableContainingListHead);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates
                    string typeOfVariableContainingCandidate = "GRGEN_LGSP.LGSPEdge";
                    string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                        typeOfVariableContainingCandidate, variableContainingCandidate,
                        variableContainingListHead);
                    // open loop
                    sourceCode.AppendFront("do\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    EmitProfilingAsNeededPre(sourceCode);

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // emit loop tail
                    // - emit switch to next edge in list within assignment expression
                    string memberOfEdgeContainingNextEdge =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInNext" : "lgspOutNext";
                    sourceCode.AppendFrontFormat("while( ({0} = {0}.{1})",
                        variableContainingCandidate, memberOfEdgeContainingNextEdge);
                    // - check condition that head has been reached again (compare with assignment value)
                    sourceCode.AppendFormat(" != {0} );\n", variableContainingListHead);

                    // close the head != null check
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
                else //EdgeType == IncidentEdgeType.IncomingOrOutgoing
                {
                    // we've to search both lists, we do so by first searching incoming, then outgoing
                    string directionRunCounter = NamesOfEntities.DirectionRunCounterVariable(PatternElementName);
                    // emit declaration of variable containing incident edges list head
                    string variableContainingListHead =
                        NamesOfEntities.CandidateIterationListHead(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0}", variableContainingListHead);
                    // emit initialization of variable containing incident edges list head
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    sourceCode.AppendFormat(" = {0}==0 ? {1}.lgspInhead : {1}.lgspOuthead;\n",
                        directionRunCounter, variableContainingStartingPointNode);

                    // emit execute the following code only if head != null
                    // todo: replace by check == null and continue
                    sourceCode.AppendFrontFormat("if({0} != null)\n", variableContainingListHead);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates
                    string typeOfVariableContainingCandidate = "GRGEN_LGSP.LGSPEdge";
                    string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                        typeOfVariableContainingCandidate, variableContainingCandidate,
                        variableContainingListHead);
                    // open loop
                    sourceCode.AppendFront("do\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    EmitProfilingAsNeededPre(sourceCode);
                    
                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // emit loop tail
                    // - emit switch to next edge in list within assignment expression
                    sourceCode.AppendFrontFormat("while( ({0}==0 ? {1} = {1}.lgspInNext : {1} = {1}.lgspOutNext)",
                        directionRunCounter, variableContainingCandidate);
                    // - check condition that head has been reached again (compare with assignment value)
                    sourceCode.AppendFormat(" != {0} );\n", variableContainingListHead);

                    // close the head != null check
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                } //EdgeType 
            } //Type
        }

        private void EmitProfilingAsNeededPre(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(ActionName != null && EmitFirstLoopProfiling)
            {
                if(Parallel)
                {
                    sourceCode.AppendFront("++actionEnv.PerformanceInfo.LoopStepsPerThread[threadId];\n");
                    sourceCode.AppendFront("long searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                }
                else
                {
                    sourceCode.AppendFront("++loopSteps;\n");
                    sourceCode.AppendFront("long searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchSteps;\n");
                }
            }
            if(Parallel)
                sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
            else
                sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
        }

        private void EmitProfilingAsNeededPost(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(ActionName != null && EmitFirstLoopProfiling)
            {
                if(Parallel)
                {
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", ActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", ActionName);
                }
                else
                {
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", ActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", ActionName);
                }
            }
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public GetCandidateByIterationType Type;
        public bool IsNode; // node|edge - only available if GraphElements|StorageElements|StorageAttributeElements|IndexElements
        public bool IsDict; // Dictionary(set/map)|List/Deque(array/deque) - only available if StorageElements|StorageAttributeElements
        public string StorageName; // only available if StorageElements
        public string StorageOwnerName; // only available if StorageAttributeElements
        public string StorageOwnerTypeName; // only available if StorageAttributeElements
        public string StorageAttributeName; // only available if StorageAttributeElements
        public string IterationType; // only available if StorageElements|StorageAttributeElements|IndexElements
        public string IndexName; // only available if IndexElements
        public string IndexSetType; // only available if IndexElements
        public IndexAccessType IndexAccessType; // only available if IndexElements
        public string IndexEqual; // only available if IndexElements
        public string IndexFrom; // only available if IndexElements
        public bool IndexFromIncluded; // only available if IndexElements
        public string IndexTo; // only available if IndexElements
        public bool IndexToIncluded; // only available if IndexElements
        public string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public IncidentEdgeType EdgeType; // only available if IncidentEdges
        public bool Parallel;
        public bool EmitProfiling;
        public string ActionName;
        public bool EmitFirstLoopProfiling;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing parallelized "get candidate by iteration" operations,
    /// setting current candidate for following check candidate operation
    /// </summary>
    class GetCandidateByIterationParallel : GetCandidate
    {
        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            bool isNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageName = storageName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageOwnerName,
            string storageOwnerTypeName,
            string storageAttributeName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageAttributeElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageOwnerName = storageOwnerName;
            StorageOwnerTypeName = storageOwnerTypeName;
            StorageAttributeName = storageAttributeName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string equality,
            bool isNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            PatternElementName = patternElementName;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Equality);
            IndexAccessType = indexAccessType;
            IndexEqual = equality;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string from,
            bool fromIncluded,
            string to,
            bool toIncluded,
            bool isNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            PatternElementName = patternElementName;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Ascending || indexAccessType == IndexAccessType.Descending);
            IndexAccessType = indexAccessType;
            IndexFrom = from;
            IndexFromIncluded = fromIncluded;
            IndexTo = to;
            IndexToIncluded = toIncluded;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType edgeType,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            EdgeType = edgeType;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetCandidate ByIteration Parallel");
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                builder.Append("StorageElements ");
                builder.AppendFormat("on {0} from {1} node:{2} {3}\n",
                    PatternElementName, StorageName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                builder.Append("StorageAttributeElements ");
                builder.AppendFormat("on {0} from {1}.{2} node:{3} {4}\n",
                    PatternElementName, StorageOwnerName, StorageAttributeName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                builder.Append("IndexElements ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, IndexName, IsNode);
            }
            else
            { //Type==GetCandidateByIterationType.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} edge type:{2}\n",
                    PatternElementName, StartingPointNodeName, EdgeType.ToString());
            }
            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Parallelized Lookup {0} \n", PatternElementName);

                if(EmitProfiling)
                    sourceCode.AppendFront("long searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                string variableContainingParallelizedCandidate =
                    NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                string variableContainingParallelizedListHead =
                    NamesOfEntities.IterationParallelizationListHead(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for head reached again 
                sourceCode.AppendFormat("{0} != {1}",
                    variableContainingParallelizedCandidate, variableContainingParallelizedListHead);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit declaration and initialization of variable containing candidates from parallelized next candidate
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, variableContainingParallelizedCandidate);
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");

                // emit loop increment: switch to next element of same type
                sourceCode.AppendFrontFormat("{0} = {0}.lgspTypeNext;\n",
                    variableContainingParallelizedCandidate);
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Pick {0} {1} from {2} {3}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("long searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                // variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for iteration end 
                sourceCode.AppendFormat("{0}.MoveNext()",
                    variableContainingParallelizedIterator);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.Current{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    variableContainingParallelizedIterator, IsDict ? ".Key" : "");
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Pick {0} {1} from {2} {3}.{4}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageOwnerName, StorageAttributeName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("long searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                // variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for iteration end 
                sourceCode.AppendFormat("{0}.MoveNext()",
                    variableContainingParallelizedIterator);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.Current{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    variableContainingParallelizedIterator, IsDict ? ".Key" : "");
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Pick {0} {1} from index {2}\n",
                        IsNode ? "node" : "edge", PatternElementName, IndexName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("long searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                // variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for iteration end 
                sourceCode.AppendFormat("{0}.MoveNext()",
                    variableContainingParallelizedIterator);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.Current;\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    variableContainingParallelizedIterator);
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Extend {0} {1} from {2} \n",
                            EdgeType.ToString(), PatternElementName, StartingPointNodeName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("long searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                if(EdgeType != IncidentEdgeType.IncomingOrOutgoing)
                {
                    EmitIterationParallelizationLockAquire(sourceCode);

                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);

                    // open loop header, and emit early out condition: another thread already found the required amount of matches 
                    sourceCode.AppendFront("while(!maxMatchesFound && (");
                    // emit loop condition: check whether head has been reached again 
                    sourceCode.AppendFormat("{0} != {1} || iterationNumber==0",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);
                    // close loop header
                    sourceCode.Append("))\n");

                    // open loop
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates from parallelized next candidate
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);
                    sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");

                    // emit loop increment: switch to next edge in list
                    string memberOfEdgeContainingNextEdge =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInNext" : "lgspOutNext";
                    sourceCode.AppendFrontFormat("{0} = {0}.{1};\n",
                        variableContainingParallelizedCandidate, memberOfEdgeContainingNextEdge);
                    sourceCode.AppendFront("++iterationNumber;\n");

                    EmitIterationParallelizationLockRelease(sourceCode);

                    EmitProfilingAsNeededPre(sourceCode);

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    sourceCode.Append("\n");
                    EmitIterationParallelizationLockAquire(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    EmitIterationParallelizationLockRelease(sourceCode);
                }
                else //EdgeType == IncidentEdgeType.IncomingOrOutgoing
                {
                    EmitIterationParallelizationLockAquire(sourceCode);

                    string directionRunCounterParallelized =
                        NamesOfEntities.IterationParallelizationDirectionRunCounterVariable(PatternElementName);
                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);

                    // open loop header, and emit early out condition: another thread already found the required amount of matches 
                    sourceCode.AppendFront("while(!maxMatchesFound && (");
                    // emit loop condition: check whether head has been reached again 
                    sourceCode.AppendFormat("{0} != {1} || iterationNumber==0",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);
                    // close loop header
                    sourceCode.Append("))\n");

                    // open loop
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates from parallelized next candidate
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);
                    sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");

                    // emit loop increment: switch to next edge in list
                    sourceCode.AppendFrontFormat("{0} = {1}==0 ? {0}.lgspInNext : {0}.lgspOutNext;\n",
                        variableContainingParallelizedCandidate, directionRunCounterParallelized);
                    sourceCode.AppendFront("++iterationNumber;\n");
                    
                    EmitIterationParallelizationLockRelease(sourceCode);

                    EmitProfilingAsNeededPre(sourceCode);

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // get iteration parallelization lock
                    sourceCode.Append("\n");
                    EmitIterationParallelizationLockAquire(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    EmitIterationParallelizationLockRelease(sourceCode);
                } //EdgeType 
            } //Type
        }

        private void EmitProfilingAsNeededPre(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(ActionName != null && EmitFirstLoopProfiling)
            {
                sourceCode.AppendFront("++actionEnv.PerformanceInfo.LoopStepsPerThread[threadId];\n");
                sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
            }
            sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
        }

        private void EmitProfilingAsNeededPost(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(ActionName != null && EmitFirstLoopProfiling)
            {
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", ActionName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", ActionName);
            }
        }

        private void EmitIterationParallelizationLockAquire(SourceBuilder sourceCode)
        {
            //sourceCode.AppendFront("Monitor.Enter(this);//lock parallel matching enumeration with action object\n");
            sourceCode.AppendFront("while(Interlocked.CompareExchange(ref iterationLock, 1, 0) != 0) Thread.SpinWait(10);//lock parallel matching enumeration with iteration lock\n");
        }

        private void EmitIterationParallelizationLockRelease(SourceBuilder sourceCode)
        {
            //sourceCode.AppendFront("Monitor.Exit(this);//unlock parallel matching enumeration with action object\n\n");
            sourceCode.AppendFront("Interlocked.Exchange(ref iterationLock, 0);//unlock parallel matching enumeration with iteration lock\n\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public GetCandidateByIterationType Type;
        public bool IsNode; // node|edge - only available if GraphElements|StorageElements|StorageAttributeElements
        public bool IsDict; // Dictionary(set/map)|List/Deque(array/deque) - only available if StorageElements|StorageAttributeElements
        public string StorageName; // only available if StorageElements
        public string StorageOwnerName; // only available if StorageAttributeElements
        public string StorageOwnerTypeName; // only available if StorageAttributeElements
        public string StorageAttributeName; // only available if StorageAttributeElements
        public string IterationType; // only available if StorageElements|StorageAttributeElements
        public string IndexName; // only available if IndexElements
        public string IndexSetType; // only available if IndexElements
        public IndexAccessType IndexAccessType; // only available if IndexElements
        public string IndexEqual; // only available if IndexElements
        public string IndexFrom; // only available if IndexElements
        public bool IndexFromIncluded; // only available if IndexElements
        public string IndexTo; // only available if IndexElements
        public bool IndexToIncluded; // only available if IndexElements
        public string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public IncidentEdgeType EdgeType; // only available if IncidentEdges
        public bool EmitProfiling;
        public string ActionName;
        public bool EmitFirstLoopProfiling;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing setup of parallelized "get candidate by iteration" operations,
    /// distributing work to worker threads, collecting their results
    /// </summary>
    class GetCandidateByIterationParallelSetup : GetCandidate
    {
        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool enclosingLoop,
            bool wasIndependentInlined,
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            EnclosingLoop = enclosingLoop;
            WasIndependentInlined = wasIndependentInlined;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageName = storageName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageOwnerName,
            string storageOwnerTypeName,
            string storageAttributeName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageAttributeElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageOwnerName = storageOwnerName;
            StorageOwnerTypeName = storageOwnerTypeName;
            StorageAttributeName = storageAttributeName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string equality,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            PatternElementName = patternElementName;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Equality);
            IndexAccessType = indexAccessType;
            IndexEqual = equality;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string from,
            bool fromIncluded,
            string to,
            bool toIncluded,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            PatternElementName = patternElementName;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Ascending || indexAccessType == IndexAccessType.Descending);
            IndexAccessType = indexAccessType;
            IndexFrom = from;
            IndexFromIncluded = fromIncluded;
            IndexTo = to;
            IndexToIncluded = toIncluded;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType edgeType,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool enclosingLoop,
            bool wasIndependentInlined,
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool emitProfiling,
            string actionName,
            bool emitFirstLoopProfiling)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            EdgeType = edgeType;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            EnclosingLoop = enclosingLoop;
            WasIndependentInlined = wasIndependentInlined;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public override void Dump(SourceBuilder builder)
        {
            // dump local content
            builder.AppendFront("GetCandidate ByIteration ParallelSetup");
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                builder.Append("StorageElements ");
                builder.AppendFormat("on {0} from {1} node:{2} {3}\n",
                    PatternElementName, StorageName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                builder.Append("StorageAttributeElements ");
                builder.AppendFormat("on {0} from {1}.{2} node:{3} {4}\n",
                    PatternElementName, StorageOwnerName, StorageAttributeName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                builder.Append("IndexElements ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, IndexName, IsNode);
            }
            else
            { //Type==GetCandidateByIterationType.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} edge type:{2}\n",
                    PatternElementName, StartingPointNodeName, EdgeType.ToString());
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("iterationNumber = 0;\n");
            sourceCode.AppendFront("int numThreadsSignaled = 0;\n");

            if(Type == GetCandidateByIterationType.GraphElements)
            {
                // code comments: lookup comment was already emitted with type iteration/drawing

                // emit initialization of variable containing graph elements list head
                string variableContainingParallelizedListHead =
                    NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                string graphMemberContainingListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                string variableContainingTypeIDForCandidate =
                    NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} = graph.{1}[{2}];\n",
                    variableContainingParallelizedListHead, graphMemberContainingListHeadByType, variableContainingTypeIDForCandidate);
                // emit initialization of variable containing candidates
                string variableContainingParallelizedCandidate =
                    NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                sourceCode.AppendFrontFormat("{0} = {1}.lgspTypeNext;\n",
                    variableContainingParallelizedCandidate, variableContainingParallelizedListHead);

                // emit declaration and initialization of variable containing candidates from next candidate, for pre-run
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, variableContainingParallelizedCandidate);
 
                // emit prerun determining the number of threads to wake up
                sourceCode.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("if({0} == {1})\n",
                    variableContainingCandidate, variableContainingParallelizedListHead);
                sourceCode.AppendFront("\tbreak;\n");
                sourceCode.AppendFrontFormat("{0} = {0}.lgspTypeNext;\n",
                    variableContainingCandidate);
                sourceCode.AppendFront("++numThreadsSignaled;\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("\n");
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageName);
                }

                // initialize variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);
                string variableContainingStorage =
                    NamesOfEntities.Variable(StorageName);
                sourceCode.AppendFrontFormat("{0} = {1}.GetEnumerator();\n",
                    variableContainingParallelizedIterator, variableContainingStorage);

                // emit prerun determining the number of threads to wake up
                sourceCode.AppendFrontFormat("numThreadsSignaled = Math.Min(numWorkerThreads, {0}.Count);\n",
                    variableContainingStorage);
                sourceCode.AppendFront("\n");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}.{4}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageOwnerName, StorageAttributeName);
                }

                // initialize variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);
                string variableContainingStorage =
                    "((" + StorageOwnerTypeName + ")" + NamesOfEntities.CandidateVariable(StorageOwnerName) + ")." + StorageAttributeName;
                sourceCode.AppendFrontFormat("{0} = {1}.GetEnumerator();\n",
                    variableContainingParallelizedIterator, variableContainingStorage);

                // emit prerun determining the number of threads to wake up
                sourceCode.AppendFrontFormat("numThreadsSignaled = Math.Min(numWorkerThreads, {0}.Count);\n",
                    variableContainingStorage);
                sourceCode.AppendFront("\n");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from index {2}\n",
                        IsNode ? "node" : "edge", PatternElementName, IndexName);
                }

                // initialize variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);
                sourceCode.AppendFrontFormat("{0} = (({1})graph.indices).{2}.",
                    variableContainingParallelizedIterator, IndexSetType, IndexName);

                if(IndexAccessType == IndexAccessType.Equality)
                {
                    sourceCode.AppendFormat("Lookup({0}).GetEnumerator();\n", IndexEqual);
                }
                else
                {
                    String accessType = IndexAccessType == IndexAccessType.Ascending ? "Ascending" : "Descending";
                    String indexFromIncluded = IndexFromIncluded ? "Inclusive" : "Exclusive";
                    String indexToIncluded = IndexToIncluded ? "Inclusive" : "Exclusive";
                    if(IndexFrom != null && IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}To{2}({3}, {4}).GetEnumerator();\n",
                            accessType, indexFromIncluded, indexToIncluded, IndexFrom, IndexTo);
                    else if(IndexFrom != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}({2}).GetEnumerator();\n",
                            accessType, indexFromIncluded, IndexFrom);
                    else if(IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}To{1}({2}).GetEnumerator();\n",
                            accessType, indexToIncluded, IndexTo);
                    else
                        sourceCode.AppendFormat("Lookup{0}().GetEnumerator();\n",
                            accessType);
                }

                // emit prerun determining the number of threads to wake up             
                sourceCode.AppendFrontFormat("foreach({0} indexPreIteration in (({1})graph.indices).{2}.",
                        IterationType, IndexSetType, IndexName);
                if(IndexAccessType == IndexAccessType.Equality)
                {
                    sourceCode.AppendFormat("Lookup({0}))\n", IndexEqual);
                }
                else
                {
                    String accessType = IndexAccessType == IndexAccessType.Ascending ? "Ascending" : "Descending";
                    String indexFromIncluded = IndexFromIncluded ? "Inclusive" : "Exclusive";
                    String indexToIncluded = IndexToIncluded ? "Inclusive" : "Exclusive";
                    if(IndexFrom != null && IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}To{2}({3}, {4}))\n",
                            accessType, indexFromIncluded, indexToIncluded, IndexFrom, IndexTo);
                    else if(IndexFrom != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}({2}))\n",
                            accessType, indexFromIncluded, IndexFrom);
                    else if(IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}To{1}({2}))\n",
                            accessType, indexToIncluded, IndexTo);
                    else
                        sourceCode.AppendFormat("Lookup{0}())\n",
                            accessType);
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFront("++numThreadsSignaled;\n");
                sourceCode.AppendFront("if(numThreadsSignaled >= numWorkerThreads)\n");
                sourceCode.AppendFront("\tbreak;\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("\n");
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Extend {0} {1} from {2} \n",
                            EdgeType.ToString(), PatternElementName, StartingPointNodeName);
                }

                if(EdgeType != IncidentEdgeType.IncomingOrOutgoing)
                {
                    // emit initialization of variable containing incident edges list head
                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    string memberOfNodeContainingListHead =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInhead" : "lgspOuthead";
                    sourceCode.AppendFrontFormat("{0} = {1}.{2};\n",
                        variableContainingParallelizedListHead, variableContainingStartingPointNode, memberOfNodeContainingListHead);
                    // emit initialization of variable containing incident edges
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} = {1};\n",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);

                    // emit declaration and initialization of variable containing candidates from next candidate, for pre-run
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);
                    string memberOfEdgeContainingNextEdge =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInNext" : "lgspOutNext";

                    // emit prerun determining the number of threads to wake up
                    sourceCode.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat("if({0} == null || ({0} == {1} && numThreadsSignaled>0))\n",
                        variableContainingCandidate, variableContainingParallelizedListHead);
                    sourceCode.AppendFront("\tbreak;\n");
                    sourceCode.AppendFrontFormat("{0} = {0}.{1};\n",
                        variableContainingCandidate, memberOfEdgeContainingNextEdge);
                    sourceCode.AppendFront("++numThreadsSignaled;\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                    sourceCode.AppendFront("\n");
                }
                else //EdgeType == IncidentEdgeType.IncomingOrOutgoing
                {
                    // emit initialization of direction run counter, to search first incoming, then outgoing
                    string directionRunCounter = NamesOfEntities.DirectionRunCounterVariable(PatternElementName);
                    // emit initialization of variable containing incident edges list head
                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    sourceCode.AppendFrontFormat("{0} = {1}==0 ? {2}.lgspInhead : {2}.lgspOuthead;\n",
                        variableContainingParallelizedListHead, directionRunCounter, variableContainingStartingPointNode);
                    // emit initialization of variable containing incident edges
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} = {1};\n",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);

                    // emit declaration and initialization of variable containing candidates from next candidate, for pre-run
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);

                    // emit prerun determining the number of threads to wake up
                    sourceCode.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat("if({0} == null || ({0} == {1} && numThreadsSignaled>0))\n",
                        variableContainingCandidate, variableContainingParallelizedListHead);
                    sourceCode.AppendFront("\tbreak;\n");
                    sourceCode.AppendFrontFormat("{0} = {1}==0 ? {0}.lgspInNext : {0}.lgspOutNext;\n",
                        variableContainingCandidate, directionRunCounter);
                    sourceCode.AppendFront("++numThreadsSignaled;\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                    sourceCode.AppendFront("\n");
                } //EdgeType 
            } //Type

            // only use parallelized body if there are at least one or two iterations available
            if(EnclosingLoop)
                sourceCode.AppendFront("if(numThreadsSignaled>0)\n"); // if no iteration is available we don't use the parallelized body
            else
                sourceCode.AppendFront("if(numThreadsSignaled>1)\n"); // if one iteration is available we use the normal matcher
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit matcher assignment, worker signaling, and waiting for completion
            //sourceCode.AppendFrontFormat("Console.WriteLine(\"signaling of parallel matchers for {0}\");\n", RulePatternClassName);
            sourceCode.AppendFront("maxMatchesFound = false;\n");
            sourceCode.AppendFront("GRGEN_LGSP.WorkerPool.Task = myMatch_parallelized_body;\n");
            sourceCode.AppendFront("GRGEN_LGSP.WorkerPool.StartWork(numThreadsSignaled);\n");
            //sourceCode.AppendFrontFormat("Console.WriteLine(\"awaiting of parallel matchers for {0}\");\n", RulePatternClassName);
            sourceCode.AppendFront("GRGEN_LGSP.WorkerPool.WaitForWorkDone();\n");
            
            // emit matches list building from matches lists of the matcher threads (obeying order of sequential iteration)
            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName), NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFrontFormat("if(maxMatches != 1) {0} = new Dictionary<int, {1}>();\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(), RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName));
            }

            sourceCode.AppendFront("int threadOfLastlyChosenMatch = 0;\n");
            sourceCode.AppendFront("while(matches.Count<maxMatches || maxMatches<=0)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("int minIterationValue = Int32.MaxValue;\n");
            sourceCode.AppendFront("int minIterationValueIndex = Int32.MaxValue;\n");
            sourceCode.AppendFront("for(int i=0; i<numThreadsSignaled; ++i)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("if(parallelTaskMatches[i].Count == 0)\n");
            sourceCode.AppendFront("\tcontinue;\n");
            sourceCode.AppendFront("if(parallelTaskMatches[i].First.IterationNumber < minIterationValue)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("minIterationValue = parallelTaskMatches[i].First.IterationNumber;\n");
            sourceCode.AppendFront("minIterationValueIndex = i;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("if(minIterationValueIndex<Int32.MaxValue)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(WasIndependentInlined)
            {
                // check whether matches filtering is needed
                sourceCode.AppendFrontFormat("if({0} != null)\n", NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // check whether the hash of the current match is already contained in the overall matches hash map
                sourceCode.AppendFrontFormat("if({0}.ContainsKey(parallelTaskMatches[minIterationValueIndex].FirstImplementation.{1}))\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // check whether one of the matches with the same hash in the overall hash map is equal to the current match
                sourceCode.AppendFrontFormat("{0} {1} = {2}[parallelTaskMatches[minIterationValueIndex].FirstImplementation.{3}];\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.DuplicateMatchCandidateVariable(),
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFront("do {\n");
                sourceCode.Indent();

                // check for same elements
                sourceCode.AppendFront("if(");
                for(int i = 0; i < NeededElements.Length; ++i)
                {
                    if(i != 0)
                        sourceCode.Append(" && ");
                    sourceCode.AppendFormat("{0}._{1} == parallelTaskMatches[minIterationValueIndex].FirstImplementation{2}.{1}",
                        NamesOfEntities.DuplicateMatchCandidateVariable() + MatchObjectPaths[i],
                        NamesOfEntities.MatchName(NeededElementsUnprefixedName[i], NeededElementsIsNode[i] ? BuildMatchObjectType.Node : BuildMatchObjectType.Edge),
                        MatchObjectPaths[i]);
                }
                sourceCode.Append(")\n");

                // the current local match is equivalent to one of the already found ones, a duplicate
                // so emit code to remove it and continue
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFront("parallelTaskMatches[minIterationValueIndex].RemoveFirst();\n");
                sourceCode.AppendFrontFormat("goto {0};\n", "continue_after_duplicate_match_removal_" + PatternName);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // close "check whether one of the matches with same hash in the overall hash map is equal to the current match"
                // switching to next match with the same hash, if available
                sourceCode.Unindent();
                sourceCode.AppendFront("} ");
                sourceCode.AppendFormat("while(({0} = {0}.nextWithSameHash) != null);\n",
                    NamesOfEntities.DuplicateMatchCandidateVariable());

                // close "check whether hash of current match is already contained in overall matches hash map (if matches filtering is needed)"
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // result when this points is reached: no equal hash is contained in the overall hash map (but a match with same hash may exist)

                // check whether hash is contained in found matches hash map
                sourceCode.AppendFrontFormat("if(!{0}.ContainsKey(parallelTaskMatches[minIterationValueIndex].FirstImplementation.{1}))\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // if not, just insert it
                sourceCode.AppendFrontFormat("{0}[parallelTaskMatches[minIterationValueIndex].FirstImplementation.{1}] = parallelTaskMatches[minIterationValueIndex].FirstImplementation;\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("else\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // otherwise loop till end of collision list of the hash set, insert there
                // other this code completed, the match will be processed like normal, as if there was no matches filtering
                sourceCode.AppendFrontFormat("{0} {1} = {2}[parallelTaskMatches[minIterationValueIndex].FirstImplementation.{3}];\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.DuplicateMatchCandidateVariable(),
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFrontFormat("while({0}.nextWithSameHash != null) {0} = {0}.nextWithSameHash;\n",
                    NamesOfEntities.DuplicateMatchCandidateVariable());

                sourceCode.AppendFrontFormat("{0}.nextWithSameHash = parallelTaskMatches[minIterationValueIndex].FirstImplementation;\n",
                     NamesOfEntities.DuplicateMatchCandidateVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // close "check whether matches filtering is needed"
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            string matchType = RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName);
            sourceCode.AppendFrontFormat("{0} match = matches.GetNextUnfilledPosition();\n", matchType);
            sourceCode.AppendFrontFormat("match.CopyMatchContent(({0})parallelTaskMatches[minIterationValueIndex].First);\n", matchType);
            sourceCode.AppendFront("parallelTaskMatches[minIterationValueIndex].RemoveFirst();\n");
            sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");
            sourceCode.AppendFront("threadOfLastlyChosenMatch = minIterationValueIndex;\n");
            sourceCode.AppendFront("continue;\n");
            sourceCode.Unindent();

            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("else\n");
            sourceCode.AppendFront("\tbreak;\n"); // break iteration, minIterationValueIndex == Int32.MaxValue
            sourceCode.Unindent();
            sourceCode.Append("continue_after_duplicate_match_removal_" + PatternName + ": ;\n");
            sourceCode.AppendFront("}\n");

            // emit adjust list heads
            sourceCode.AppendFront("for(int i=0; i<moveHeadAfterNodes[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFront("\tgraph.MoveHeadAfter(moveHeadAfterNodes[threadOfLastlyChosenMatch][i]);\n");
            sourceCode.AppendFront("for(int i=0; i<moveHeadAfterEdges[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFront("\tgraph.MoveHeadAfter(moveHeadAfterEdges[threadOfLastlyChosenMatch][i]);\n");
            sourceCode.AppendFront("for(int i=0; i<moveOutHeadAfter[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFront("\tmoveOutHeadAfter[threadOfLastlyChosenMatch][i].Key.MoveOutHeadAfter(moveOutHeadAfter[threadOfLastlyChosenMatch][i].Value);\n");
            sourceCode.AppendFront("for(int i=0; i<moveInHeadAfter[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFront("\tmoveInHeadAfter[threadOfLastlyChosenMatch][i].Key.MoveInHeadAfter(moveInHeadAfter[threadOfLastlyChosenMatch][i].Value);\n");

            // emit cleaning of variables that must be empty for next run
            sourceCode.AppendFront("for(int i=0; i<numThreadsSignaled; ++i)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("parallelTaskMatches[i].Clear();\n");
            sourceCode.AppendFront("moveHeadAfterNodes[i].Clear();\n");
            sourceCode.AppendFront("moveHeadAfterEdges[i].Clear();\n");
            sourceCode.AppendFront("moveOutHeadAfter[i].Clear();\n");
            sourceCode.AppendFront("moveInHeadAfter[i].Clear();\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            if(EmitProfiling)
                sourceCode.AppendFront("parallelMatcherUsed = true;\n");

            // use normal matcher if there is only one iteration available and no enclosing loop around us
            // this re-executes the head stuff before the first iteration,
            // but that's most of the times cheaper than starting the parallel workers
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            if(!EnclosingLoop)
            {
                sourceCode.AppendFront("else if(numThreadsSignaled==1)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFront("myMatch(actionEnv, maxMatches");
                foreach(string parameterName in ParameterNames)
                {
                    sourceCode.Append(", ");
                    sourceCode.Append(parameterName);
                }
                sourceCode.Append(");\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            // just continue matching/with cleaning code if there were zero iterations
        }

        public override bool IsSearchNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return null;
        }

        public GetCandidateByIterationType Type;
        public bool IsNode; // node|edge - only available if GraphElements|StorageElements|StorageAttributeElements
        public bool IsDict; // Dictionary(set/map)|List/Deque(array/deque) - only available if StorageElements|StorageAttributeElements
        public string StorageName; // only available if StorageElements
        public string StorageOwnerName; // only available if StorageAttributeElements
        public string StorageOwnerTypeName; // only available if StorageAttributeElements
        public string StorageAttributeName; // only available if StorageAttributeElements
        public string IterationType; // only available if StorageElements|StorageAttributeElements
        public string IndexName; // only available if IndexElements
        public string IndexSetType; // only available if IndexElements
        public IndexAccessType IndexAccessType; // only available if IndexElements
        public string IndexEqual; // only available if IndexElements
        public string IndexFrom; // only available if IndexElements
        public bool IndexFromIncluded; // only available if IndexElements
        public string IndexTo; // only available if IndexElements
        public bool IndexToIncluded; // only available if IndexElements
        public string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public IncidentEdgeType EdgeType; // only available if IncidentEdges
        public string RulePatternClassName;
        public string PatternName;
        public string[] ParameterNames; // the parameters to forward to the normal matcher in case that is to be used because there's only a single iteration
        public bool EnclosingLoop; // in case of an enclosing loop we can't forward to the normal matcher
        public bool WasIndependentInlined;
        public string[] NeededElements; // needed in case of WasIndependentInlined
        public string[] MatchObjectPaths; // needed in case of WasIndependentInlined
        public string[] NeededElementsUnprefixedName; // needed in case of WasIndependentInlined
        public bool[] NeededElementsIsNode; // needed in case of WasIndependentInlined
        public bool EmitProfiling;
        public string ActionName;
        public bool EmitFirstLoopProfiling;
    }

    /// <summary>
    /// Available types of GetCandidateByDrawing operations
    /// </summary>
    enum GetCandidateByDrawingType
    {
        NodeFromEdge, // draw node from given edge
        MapWithStorage, // map element by storage map lookup
        MapByName, // map element by name map lookup
        MapByUnique, // map element by unique map lookup
        FromInputs, // draw element from action inputs
        FromSubpatternConnections, // draw element from subpattern connections
        FromParallelizationTask, // draw element from parallelization preset
        FromParallelizationTaskVar, // draw variable from parallelization preset
        FromOtherElementForCast, // draw element from other element for type cast
        FromOtherElementForAssign, // draw element from other element for assignment
    }

    /// <summary>
    /// The different possibilites of drawing an implicit node from an edge
    /// if directed edge: source, target
    /// if arbitrary directed, undirected, arbitrary: source-or-target for first node, the-other for second node
    /// </summary>
    enum ImplicitNodeType
    {
        Source,
        Target,
        SourceOrTarget,
        TheOther
    }

    /// <summary>
    /// Class representing "get node (or edge) by drawing" operation,
    /// setting current candidate for following check candidate operations
    /// </summary>
    class GetCandidateByDrawing : GetCandidate
    {
        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string patternElementTypeName,
            string startingPointEdgeName,
            ImplicitNodeType nodeType)
        {
            Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
            Debug.Assert(nodeType != ImplicitNodeType.TheOther);

            Type = type;
            PatternElementName = patternElementName;
            PatternElementTypeName = patternElementTypeName;
            StartingPointEdgeName = startingPointEdgeName;
            NodeType = nodeType;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string patternElementTypeName,
            string startingPointEdgeName,
            string theOtherPatternElementName,
            ImplicitNodeType nodeType)
        {
            Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
            Debug.Assert(nodeType == ImplicitNodeType.TheOther);

            Type = type;
            PatternElementName = patternElementName;
            PatternElementTypeName = patternElementTypeName;
            StartingPointEdgeName = startingPointEdgeName;
            TheOtherPatternElementName = theOtherPatternElementName;
            NodeType = nodeType;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromInputs 
                || type == GetCandidateByDrawingType.FromSubpatternConnections
                || type == GetCandidateByDrawingType.FromParallelizationTask);
            
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string typeName)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromParallelizationTaskVar);

            Type = type;
            PatternElementName = patternElementName;
            TypeName = typeName;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string sourcePatternElementName,
            string storageName,
            string storageValueTypeName,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.MapWithStorage);

            Type = type;
            PatternElementName = patternElementName;
            SourcePatternElementName = sourcePatternElementName;
            StorageName = storageName;
            StorageValueTypeName = storageValueTypeName;
            IsNode = isNode;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string source,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromOtherElementForCast 
                || type == GetCandidateByDrawingType.FromOtherElementForAssign
                || type == GetCandidateByDrawingType.MapByName
                || type == GetCandidateByDrawingType.MapByUnique);

            Type = type;
            PatternElementName = patternElementName;
            if(type == GetCandidateByDrawingType.MapByName || type == GetCandidateByDrawingType.MapByUnique)
                SourceExpression = source;
            else
                SourcePatternElementName = source;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GetCandidate ByDrawing ");
            if(Type==GetCandidateByDrawingType.NodeFromEdge) {
                builder.Append("NodeFromEdge ");
                builder.AppendFormat("on {0} of {1} from {2} implicit node type:{3}\n",
                    PatternElementName, PatternElementTypeName, 
                    StartingPointEdgeName, NodeType.ToString());
            } else if(Type==GetCandidateByDrawingType.MapWithStorage) {
                builder.Append("MapWithStorage ");
                builder.AppendFormat("on {0} by {1} from {2} node:{3}\n",
                    PatternElementName, SourcePatternElementName, 
                    StorageName, IsNode);
            } else if(Type==GetCandidateByDrawingType.MapByName) {
                builder.Append("MapByName ");
                builder.AppendFormat("on {0} from name map by {1} node:{2}\n",
                    PatternElementName, SourceExpression, IsNode);
            } else if(Type==GetCandidateByDrawingType.MapByUnique) {
                builder.Append("MapByUnique ");
                builder.AppendFormat("on {0} from unique map by {1} node:{2}\n",
                    PatternElementName, SourceExpression, IsNode);
            } else if(Type==GetCandidateByDrawingType.FromInputs) {
                builder.Append("FromInputs ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else if(Type==GetCandidateByDrawingType.FromSubpatternConnections) {
                builder.Append("FromSubpatternConnections ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else if(Type == GetCandidateByDrawingType.FromParallelizationTask) {
                builder.Append("FromParallelizationTask ");
                builder.AppendFormat("on {0} node:{1} \n",
                    PatternElementName, IsNode);
            } else if(Type == GetCandidateByDrawingType.FromParallelizationTaskVar) {
                builder.Append("FromParallelizationTaskVar ");
                builder.AppendFormat("on {0} \n",
                    PatternElementName);
            } else if(Type == GetCandidateByDrawingType.FromOtherElementForCast) {
                builder.Append("FromOtherElementForCast ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, SourcePatternElementName, IsNode);
            } else { //if(Type==GetCandidateByDrawingType.FromOtherElementForAssign)
                builder.Append("FromOtherElementForAssign ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, SourcePatternElementName, IsNode);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type==GetCandidateByDrawingType.NodeFromEdge)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Implicit {0} {1} from {2} \n",
                            NodeType.ToString(), PatternElementName, StartingPointEdgeName);

                if (NodeType == ImplicitNodeType.Source || NodeType == ImplicitNodeType.Target)
                {
                    // emit declaration of variable containing candidate node
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0}", variableContainingCandidate);
                    // emit initialization with demanded node from variable containing edge
                    string variableContainingStartingPointEdge =
                        NamesOfEntities.CandidateVariable(StartingPointEdgeName);
                    string whichImplicitNode =
                        NodeType==ImplicitNodeType.Source ? "lgspSource" : "lgspTarget";
                    sourceCode.AppendFormat(" = {0}.{1};\n",
                        variableContainingStartingPointEdge, whichImplicitNode);
                }
                else if (NodeType == ImplicitNodeType.SourceOrTarget)
                {
                    // we've to look at both nodes, we do so by first handling source, then target
                    string directionRunCounter = 
                        NamesOfEntities.DirectionRunCounterVariable(StartingPointEdgeName);
                    // emit declaration of variable containing candidate node
                    string variableContainingCandidate = 
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0}", variableContainingCandidate);
                    // emit initialization with demanded node from variable containing edge
                    string variableContainingStartingPointEdge =
                        NamesOfEntities.CandidateVariable(StartingPointEdgeName);
                    sourceCode.AppendFormat(" = {0}==0 ? {1}.lgspSource : {1}.lgspTarget;\n",
                        directionRunCounter, variableContainingStartingPointEdge);
                }
                else // NodeType == ImplicitNodeType.TheOther
                {
                    // emit declaration of variable containing candidate node
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0}", variableContainingCandidate);
                    // emit initialization with other node from edge
                    string variableContainingStartingPointEdge =
                        NamesOfEntities.CandidateVariable(StartingPointEdgeName);
                    sourceCode.AppendFormat(" = {0}=={1}.lgspSource ? {1}.lgspTarget : {1}.lgspSource;\n",
                        NamesOfEntities.CandidateVariable(TheOtherPatternElementName),
                        variableContainingStartingPointEdge);
                }
            }
            else if(Type == GetCandidateByDrawingType.MapWithStorage)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Map {0} by {1}[{2}] \n",
                        PatternElementName, StorageName, SourcePatternElementName);

                // emit declaration of variable to hold element mapped from storage
                string typeOfTempVariableForMapResult = StorageValueTypeName;
                string tempVariableForMapResult = NamesOfEntities.MapWithStorageTemporary(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfTempVariableForMapResult, tempVariableForMapResult);
                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
            }
            else if(Type == GetCandidateByDrawingType.MapByName)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Map {0} by name map with {1} \n",
                        PatternElementName, SourceExpression);

                // emit declaration of variable to hold element mapped by name
                // emit initialization with element from name map lookup
                string typeOfTempVariableForMapResult = "GRGEN_LIBGR.IGraphElement";
                string tempVariableForMapResult = NamesOfEntities.MapByNameTemporary(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ((GRGEN_LGSP.LGSPNamedGraph)graph).GetGraphElement(({2}).ToString());\n",
                    typeOfTempVariableForMapResult, tempVariableForMapResult, SourceExpression);
                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
            }
            else if(Type == GetCandidateByDrawingType.MapByUnique)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Map {0} by unique index with {1} \n",
                        PatternElementName, SourceExpression);

                // emit declaration of variable to hold element mapped by unique id
                // emit initialization with element from unique map lookup
                string typeOfTempVariableForMapResult = "GRGEN_LIBGR.IGraphElement";
                string tempVariableForMapResult = NamesOfEntities.MapByUniqueTemporary(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = graph.GetGraphElement((int)({2}));\n",
                    typeOfTempVariableForMapResult, tempVariableForMapResult, SourceExpression);
                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
            }
            else if(Type == GetCandidateByDrawingType.FromInputs)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Preset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with element from input parameters array
                sourceCode.AppendFormat(" = ({0}){1};\n",
                    typeOfVariableContainingCandidate, PatternElementName);
            }
            else if(Type==GetCandidateByDrawingType.FromSubpatternConnections)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// SubPreset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node
                // and initialization with element from subpattern connections
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, PatternElementName);
            }
            else if(Type == GetCandidateByDrawingType.FromParallelizationTask)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// ParallelizationPreset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node or edge
                // and initialization with element from parallelization task
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, variableContainingParallelPreset);
            }
            else if(Type == GetCandidateByDrawingType.FromParallelizationTaskVar)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// ParallelizationPresetVar {0} \n", PatternElementName);

                // emit declaration of variable containing parameter forwarded
                // and initialization with element from parallelization task
                string variableContainingVariable = NamesOfEntities.Variable(PatternElementName);
                string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    TypeName, variableContainingVariable, variableContainingParallelPreset);
            }
            else if(Type == GetCandidateByDrawingType.FromOtherElementForCast)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Element {0} as type cast from other element {1} \n", 
                        PatternElementName, SourcePatternElementName);

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with other element candidate variable
                string variableContainingSource = NamesOfEntities.CandidateVariable(SourcePatternElementName);
                sourceCode.AppendFormat(" = {0};\n", variableContainingSource);
            }
            else //if(Type == GetCandidateByDrawingType.FromOtherElementForAssign)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Element {0} assigned from other element {1} \n", 
                        PatternElementName, SourcePatternElementName);

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with other element candidate variable
                string variableContainingSource = NamesOfEntities.CandidateVariable(SourcePatternElementName);
                sourceCode.AppendFormat(" = {0};\n", variableContainingSource);
            }
        }

        public GetCandidateByDrawingType Type;
        public string PatternElementTypeName; // only valid if NodeFromEdge
        public string TheOtherPatternElementName; // only valid if NodeFromEdge and TheOther
        public string StartingPointEdgeName; // from pattern - only valid if NodeFromEdge
        ImplicitNodeType NodeType; // only valid if NodeFromEdge
        public bool IsNode; // node|edge
        public string SourcePatternElementName; // only valid if MapWithStorage|FromOtherElementForCast|FromOtherElementForAssign
        public string SourceExpression; // only valid if MapByName
        public string StorageName; // only valid if MapWithStorage
        public string StorageValueTypeName; // only valid if MapWithStorage
        public string TypeName; // only valid if FromParallelizationTaskVar
    }

    /// <summary>
    /// Class representing "get node or edge by drawing" operation,
    /// setting current candidate for following check candidate operations
    /// </summary>
    class WriteParallelPreset : GetCandidate
    {
        public WriteParallelPreset(
            string patternElementName,
            bool isNode)
        {
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("WriteParallelPreset ");
            builder.AppendFormat("on {0} node:{1} \n",
                PatternElementName, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// WriteParallelizationPreset {0} \n", PatternElementName);

            // initialize parallelization task with variable containing candidate node
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
            sourceCode.AppendFrontFormat("{0} = {1};\n",
                variableContainingParallelPreset, variableContainingCandidate);
        }

        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing "get variable by drawing" operation,
    /// setting current candidate for following check candidate operations
    /// </summary>
    class WriteParallelPresetVar : GetCandidate
    {
        public WriteParallelPresetVar(
            string varName,
            string varType)
        {
            PatternElementName = varName;
            VarType = varType;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("WriteParallelPreset ");
            builder.AppendFormat("on {0} of type:{1}\n",
                PatternElementName, VarType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// WriteParallelizationPresetVar {0} \n", PatternElementName);

            string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
            string variableContainingVariable = NamesOfEntities.Variable(PatternElementName);
            sourceCode.AppendFrontFormat("{0} = {1};\n",
                variableContainingParallelPreset, variableContainingVariable);
        }

        public string VarType;
    }

    /// <summary>
    /// Class representing operation iterating both directions of an edge of unfixed direction 
    /// </summary>
    class BothDirectionsIteration : SearchProgramOperation
    {
        public BothDirectionsIteration(string patternElementName)
        {
            PatternElementName = patternElementName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BothDirectionsIteration on {0}\n", PatternElementName);

            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// both directions of {0}\n", PatternElementName);

            // emit loop header of loop iterating both directions of an edge of not fixed direction
            string directionRunCounter = NamesOfEntities.DirectionRunCounterVariable(PatternElementName);
            sourceCode.AppendFrontFormat("for(");
            sourceCode.AppendFormat("int {0} = 0; ", directionRunCounter);
            sourceCode.AppendFormat("{0} < 2; ", directionRunCounter);
            sourceCode.AppendFormat("++{0}", directionRunCounter);
            sourceCode.Append(")\n");

            // open loop
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit loop body
            NestedOperationsList.Emit(sourceCode);

            // close loop
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public string PatternElementName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing nesting operation which executes the body once;
    /// needed for iterated, to prevent a return out of the matcher program
    /// circumventing the maxMatchesIterReached code which must get called if matching fails
    /// </summary>
    class ReturnPreventingDummyIteration : SearchProgramOperation
    {
        public ReturnPreventingDummyIteration()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("ReturnPreventingDummyIteration \n");

            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// dummy iteration for iterated return prevention\n");

            // open loop
            sourceCode.AppendFront("do\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit loop body
            NestedOperationsList.Emit(sourceCode);

            // close loop
            sourceCode.Unindent();
            sourceCode.AppendFront("} while(false);\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Base class for search program candidate filtering operations
    /// </summary>
    abstract class CheckCandidate : CheckOperation
    {
        public string PatternElementName;
    }

    /// <summary>
    /// Available types of CheckCandidateForType operations
    /// </summary>
    enum CheckCandidateForTypeType
    {
        ByIsAllowedType, //check by inspecting the IsAllowedType array of the rule pattern
        ByIsMyType,      // check by inspecting the IsMyType array of the graph element's type model
        ByTypeID         // check by comparing against a given type ID
    }

    /// <summary>
    /// Class representing "check whether candidate is of allowed type" operation
    /// </summary>
    class CheckCandidateForType : CheckCandidate
    {
        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeName,
            bool isNode)
        {
            Debug.Assert(type == CheckCandidateForTypeType.ByIsMyType);
            Type = type;
            PatternElementName = patternElementName;
            TypeName = rulePatternTypeNameOrTypeName;
            IsNode = isNode;
        }

        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeName,
            string isAllowedArrayName,
            bool isNode)
        {
            Debug.Assert(type == CheckCandidateForTypeType.ByIsAllowedType);
            Type = type;
            PatternElementName = patternElementName;
            RulePatternTypeName = rulePatternTypeNameOrTypeName;
            IsAllowedArrayName = isAllowedArrayName;
            IsNode = isNode;
        }

        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string[] typeIDs,
            bool isNode)
        {
            Debug.Assert(type == CheckCandidateForTypeType.ByTypeID);
            Type = type;
            PatternElementName = patternElementName;
            TypeIDs = (string[])typeIDs.Clone();
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForType ");
            if (Type == CheckCandidateForTypeType.ByIsAllowedType) {
                builder.Append("ByIsAllowedType ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, RulePatternTypeName, IsNode);
            } else if (Type == CheckCandidateForTypeType.ByIsMyType) {
                builder.Append("ByIsMyType ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, TypeName, IsNode);
            } else { // Type == CheckCandidateForTypeType.ByTypeID
                builder.Append("ByTypeID ");
                builder.AppendFormat("on {0} ids:{1} node:{2}\n",
                    PatternElementName, string.Join(",", TypeIDs), IsNode);
            }
            
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check decision
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            if (Type == CheckCandidateForTypeType.ByIsAllowedType)
            {
                string isAllowedTypeArrayMemberOfRulePattern =
                    IsAllowedArrayName + "_IsAllowedType";
                sourceCode.AppendFrontFormat("if(!{0}.{1}[{2}.lgspType.TypeID]) ",
                    RulePatternTypeName, isAllowedTypeArrayMemberOfRulePattern,
                    variableContainingCandidate);
            }
            else if (Type == CheckCandidateForTypeType.ByIsMyType)
            {
                sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.lgspType.TypeID]) ",
                    TypeName, variableContainingCandidate);
            }
            else // Type == CheckCandidateForTypeType.ByTypeID)
            {
                sourceCode.AppendFront("if(");
                bool first = true;
                foreach (string typeID in TypeIDs)
                {
                    if (first) first = false;
                    else sourceCode.Append(" && ");

                    sourceCode.AppendFormat("{0}.lgspType.TypeID!={1}",
                        variableContainingCandidate, typeID);
                }
                sourceCode.Append(") ");
            }
            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public CheckCandidateForTypeType Type;

        public string RulePatternTypeName; // only valid if ByIsAllowedType
        public string IsAllowedArrayName; // only valid if ByIsAllowedType
        public string TypeName; // only valid if ByIsMyType
        public string[] TypeIDs; // only valid if ByTypeID

        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing "check whether candidate is identical to another element" operation
    /// </summary>
    class CheckCandidateForIdentity : CheckCandidate
    {
        public CheckCandidateForIdentity(
            string patternElementName,
            string otherPatternElementName)
        {
            PatternElementName = patternElementName;
            OtherPatternElementName = otherPatternElementName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.Append("CheckCandidate ForIdentity ");
            builder.AppendFormat("by {0} == {1}\n", PatternElementName, OtherPatternElementName);
            
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check decision
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingOtherElement = NamesOfEntities.CandidateVariable(OtherPatternElementName);
            sourceCode.AppendFrontFormat("if({0}!={1}) ", 
                variableContainingCandidate, variableContainingOtherElement);
            
            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string OtherPatternElementName;
    }

    /// <summary>
    /// Class representing some check candidate operation,
    /// which was determined at generation time to always fail 
    /// </summary>
    class CheckCandidateFailed : CheckCandidate
    {
        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate Failed \n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check failed code
            CheckFailedOperations.Emit(sourceCode);
        }
    }

    /// <summary>
    /// The different positions of some edge to check the candidate node against
    /// if directed edge: source, target
    /// if arbitrary directed, undirected, arbitrary edge: source-or-target for first node, the-other for second node
    /// </summary>
    enum CheckCandidateForConnectednessType
    {
        Source,
        Target,
        SourceOrTarget,
        TheOther
    }

    /// <summary>
    /// Class representing "check whether candidate is connected to the elements
    ///   it should be connected to, according to the pattern" operation
    /// </summary>
    class CheckCandidateForConnectedness : CheckCandidate
    {
        public CheckCandidateForConnectedness(
            string patternElementName, 
            string patternNodeName,
            string patternEdgeName,
            CheckCandidateForConnectednessType connectednessType)
        {
            Debug.Assert(connectednessType != CheckCandidateForConnectednessType.TheOther);

            // pattern element is the candidate to check, either node or edge
            PatternElementName = patternElementName;
            PatternNodeName = patternNodeName;
            PatternEdgeName = patternEdgeName;
            ConnectednessType = connectednessType;
        }

        public CheckCandidateForConnectedness(
            string patternElementName,
            string patternNodeName,
            string patternEdgeName,
            string theOtherPatternNodeName,
            CheckCandidateForConnectednessType connectednessType)
        {
            Debug.Assert(connectednessType == CheckCandidateForConnectednessType.TheOther);

            // pattern element is the candidate to check, either node or edge
            PatternElementName = patternElementName;
            PatternNodeName = patternNodeName;
            PatternEdgeName = patternEdgeName;
            TheOtherPatternNodeName = theOtherPatternNodeName;
            ConnectednessType = connectednessType;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForConnectedness ");
            builder.AppendFormat("{0}=={1}.{2}\n",
                PatternNodeName, PatternEdgeName, ConnectednessType.ToString());
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (ConnectednessType == CheckCandidateForConnectednessType.Source)
            {
                // emit check decision for is candidate connected to already found partial match, i.e. edge source equals node
                sourceCode.AppendFrontFormat("if({0}.lgspSource != {1}) ",
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }
            else if (ConnectednessType == CheckCandidateForConnectednessType.Target)
            {
                // emit check decision for is candidate connected to already found partial match, i.e. edge target equals node
                sourceCode.AppendFrontFormat("if({0}.lgspTarget != {1}) ",
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }
            else if(ConnectednessType == CheckCandidateForConnectednessType.SourceOrTarget)
            {
                // we've to check both node positions of the edge, we do so by checking source or target dependent on the direction run
                sourceCode.AppendFrontFormat("if( ({0}==0 ? {1}.lgspSource : {1}.lgspTarget) != {2}) ",
                    NamesOfEntities.DirectionRunCounterVariable(PatternEdgeName), 
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }
            else //ConnectednessType == CheckCandidateForConnectednessType.TheOther
            {
                // we've to check the node position of the edge the first node is not assigned to
                sourceCode.AppendFrontFormat("if( ({0}=={1}.lgspSource ? {1}.lgspTarget : {1}.lgspSource) != {2}) ",
                    NamesOfEntities.CandidateVariable(TheOtherPatternNodeName),
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string PatternNodeName;
        public string PatternEdgeName;
        public string TheOtherPatternNodeName; // only valid if ConnectednessType==TheOther
        public CheckCandidateForConnectednessType ConnectednessType;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other local pattern element within this isomorphy space, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written/removed by AcceptCandidate/AbandonCandidate
    /// </summary>
    class CheckCandidateForIsomorphy : CheckCandidate
    {
        public CheckCandidateForIsomorphy(
            string patternElementName,
            List<string> namesOfPatternElementsToCheckAgainst,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel,
            bool lockForAllThreads)
        {
            PatternElementName = patternElementName;
            NamesOfPatternElementsToCheckAgainst = namesOfPatternElementsToCheckAgainst;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
            LockForAllThreads = lockForAllThreads;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphy ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2} ",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
            if (NamesOfPatternElementsToCheckAgainst != null)
            {
                builder.Append("against ");
                foreach (string name in NamesOfPatternElementsToCheckAgainst)
                {
                    builder.AppendFormat("{0} ", name);
                }
            }
            builder.AppendFormat("parallel:{0} first for all:{1} ",
                Parallel, LockForAllThreads);
            builder.Append("\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // open decision whether to fail
            sourceCode.AppendFront("if(");

            // fail if graph element contained within candidate was already matched
            // (to another pattern element)
            // as this would cause a homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE ? ");
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED << isoSpace";
                if(LockForAllThreads)
                    sourceCode.AppendFormat("( flagsPerElement0[{0}.uniqueId] & {1} ) != 0",
                        variableContainingCandidate, isMatchedBit);
                else
                    sourceCode.AppendFormat("( flagsPerElement[{0}.uniqueId] & {1} ) != 0",
                        variableContainingCandidate, isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.perThreadInIsoSpaceMatchedElements[{0}][isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE]"
                        + ".ContainsKey({1}))", 
                        LockForAllThreads ? "0" : "threadId",
                        variableContainingCandidate);
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE ? ");
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace";
                sourceCode.AppendFormat("({0}.lgspFlags & {1}) != 0", variableContainingCandidate, isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.inIsoSpaceMatchedElements[isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE]"
                        + ".ContainsKey({0}))", variableContainingCandidate);
                }
            }

            // but only if isomorphy is demanded (NamesOfPatternElementsToCheckAgainst empty)
            // otherwise homomorphy to certain elements is allowed, 
            // so we only fail if the graph element is matched to one of the not allowed elements,
            // given in NamesOfPatternElementsToCheckAgainst 
            if (NamesOfPatternElementsToCheckAgainst != null)
            {
                Debug.Assert(NamesOfPatternElementsToCheckAgainst.Count > 0);

                sourceCode.Append("\n");
                sourceCode.Indent();

                if (NamesOfPatternElementsToCheckAgainst.Count == 1)
                {
                    string name = NamesOfPatternElementsToCheckAgainst[0];
                    sourceCode.AppendFrontFormat("&& {0}=={1}\n", variableContainingCandidate,
                        NamesOfEntities.CandidateVariable(name));
                }
                else
                {
                    bool first = true;
                    foreach (string name in NamesOfPatternElementsToCheckAgainst)
                    {
                        if (first)
                        {
                            sourceCode.AppendFrontFormat("&& ({0}=={1}\n", variableContainingCandidate,
                                NamesOfEntities.CandidateVariable(name));
                            sourceCode.Indent();
                            first = false;
                        }
                        else
                        {
                            sourceCode.AppendFrontFormat("|| {0}=={1}\n", variableContainingCandidate,
                               NamesOfEntities.CandidateVariable(name));
                        }
                    }
                    sourceCode.AppendFront(")\n");
                    sourceCode.Unindent();
                }

                // close decision
                sourceCode.AppendFront(")\n");
                sourceCode.Unindent();
            }
            else
            {
                // close decision
                sourceCode.Append(")\n");
            }

            // emit check failed code
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public List<string> NamesOfPatternElementsToCheckAgainst;
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
        public bool LockForAllThreads;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other non-local pattern element within this isomorphy space, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written by AcceptCandidateGlobal/AbandonCandidateGlobal
    /// </summary>
    class CheckCandidateForIsomorphyGlobal : CheckCandidate
    {
        public CheckCandidateForIsomorphyGlobal(
            string patternElementName,
            List<string> globallyHomomorphElements,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel)
        {
            PatternElementName = patternElementName;
            GloballyHomomorphElements = globallyHomomorphElements;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphyGlobal ");
            builder.AppendFormat("on {0} node:{1} ",
                PatternElementName, IsNode);
            if (GloballyHomomorphElements != null)
            {
                builder.Append("but accept if ");
                foreach (string name in GloballyHomomorphElements)
                {
                    builder.AppendFormat("{0} ", name);
                }
            }
            builder.Append("\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // open decision whether to fail
            sourceCode.AppendFront("if(");

            // fail if graph element contained within candidate was already matched
            // (in another subpattern to another pattern element)
            // as this would cause a inter-pattern-homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE ? ");
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFormat("( flagsPerElementGlobal[{0}.uniqueId] & {1} ) == {1}",
                    variableContainingCandidate,
                    isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.perThreadInIsoSpaceMatchedElementsGlobal[threadId][isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE]"
                        + ".ContainsKey({0}))", variableContainingCandidate);
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE ? ");
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFormat("({0}.lgspFlags & {1})=={1}",
                    variableContainingCandidate, isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.inIsoSpaceMatchedElementsGlobal[isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE]"
                        + ".ContainsKey({0}))", variableContainingCandidate);
                }
            }

            if (GloballyHomomorphElements != null)
            {
                // don't fail if candidate was globally matched by an element
                // it is allowed to be globally homomorph to 
                // (element from alternative case declared to be non-isomorph to element from enclosing pattern)
                foreach (string name in GloballyHomomorphElements)
                {
                    sourceCode.AppendFormat(" && {0}!={1}",
                        variableContainingCandidate, NamesOfEntities.CandidateVariable(name));
                }
            }
            sourceCode.Append(")\n");

            // emit check failed code
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public List<string> GloballyHomomorphElements;
        public bool IsNode; // node|edge
        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other pattern element on the pattern derivation path, to ensure required isomorphy" operation
    /// </summary>
    class CheckCandidateForIsomorphyPatternPath : CheckCandidate
    {
        public CheckCandidateForIsomorphyPatternPath(
            string patternElementName,
            bool isNode,
            bool always,
            string lastMatchAtPreviousNestingLevel)
        {
            PatternElementName = patternElementName;
            IsNode = isNode;
            Always = always;
            LastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphyPatternPath ");
            builder.AppendFormat("on {0} node:{1} last match at previous nesting level in:{2}",
                PatternElementName, IsNode, LastMatchAtPreviousNestingLevel);
            builder.Append("\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // open decision whether to fail
            sourceCode.AppendFront("if(");

            // fail if graph element contained within candidate was already matched
            // (previously on the pattern derivation path to another pattern element)
            // as this would cause a inter-pattern-homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBySomeBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN";

            if (!Always) {
                sourceCode.Append("searchPatternpath && ");
            }

            sourceCode.AppendFormat("({0}.lgspFlags & {1})=={1} && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched({0}, {2})",
                variableContainingCandidate, isMatchedBySomeBit, LastMatchAtPreviousNestingLevel);

            sourceCode.Append(")\n");

            // emit check failed code
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public bool IsNode; // node|edge
        public bool Always; // have a look at searchPatternpath or search always
        string LastMatchAtPreviousNestingLevel;
    }

    /// <summary>
    /// Class representing "check whether candidate is contained in the storage map" operation
    /// </summary>
    class CheckCandidateMapWithStorage : CheckCandidate
    {
        public CheckCandidateMapWithStorage(
            string patternElementName,
            string sourcePatternElementName,
            string storageName,
            string storageKeyTypeName,
            bool isNode)
        {
            PatternElementName = patternElementName;
            SourcePatternElementName = sourcePatternElementName;
            StorageName = storageName;
            StorageKeyTypeName = storageKeyTypeName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate MapWithStorage ");
            builder.AppendFormat("on {0} by {1} from {2} node:{3}\n",
                PatternElementName, SourcePatternElementName, 
                StorageName, IsNode);
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit initialization with element mapped from storage
            string variableContainingStorage = NamesOfEntities.Variable(StorageName);
            string variableContainingSourceElement = NamesOfEntities.CandidateVariable(SourcePatternElementName);
            string tempVariableForMapResult = NamesOfEntities.MapWithStorageTemporary(PatternElementName);
            sourceCode.AppendFrontFormat("if(!{0}.TryGetValue(({1}){2}, out {3})) ",
                variableContainingStorage, StorageKeyTypeName, 
                variableContainingSourceElement, tempVariableForMapResult);

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // assign the value to the candidate variable, cast it to the variable type
            string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                
            sourceCode.AppendFrontFormat("{0} = ({1}){2};\n",
                variableContainingCandidate, typeOfVariableContainingCandidate, tempVariableForMapResult);
        }

        public string SourcePatternElementName;
        public string StorageName;
        public string StorageKeyTypeName;
        bool IsNode;
    }

    /// <summary>
    /// Class representing "check whether candidate is contained in the name map" operation
    /// </summary>
    class CheckCandidateMapByName : CheckCandidate
    {
        public CheckCandidateMapByName(
            string patternElementName,
            bool isNode)
        {
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate MapByName ");
            builder.AppendFormat("on {0} node:{1}\n",
                PatternElementName, IsNode);
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit initialization with element mapped from name map
            string tempVariableForMapResult = NamesOfEntities.MapByNameTemporary(PatternElementName);
            sourceCode.AppendFrontFormat("if({0}==null || !({0} is {1}))", 
                tempVariableForMapResult, IsNode ? "GRGEN_LIBGR.INode" : "GRGEN_LIBGR.IEdge");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // assign the value to the candidate variable, cast it to the variable type
            string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("{0} = ({1}){2};\n",
                variableContainingCandidate, typeOfVariableContainingCandidate, tempVariableForMapResult);
        }

        bool IsNode;
    }

    /// <summary>
    /// Class representing "check whether candidate is contained in the unique index" operation
    /// </summary>
    class CheckCandidateMapByUnique : CheckCandidate
    {
        public CheckCandidateMapByUnique(
            string patternElementName,
            bool isNode)
        {
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate MapByUnique ");
            builder.AppendFormat("on {0} node:{1}\n",
                PatternElementName, IsNode);
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit initialization with element mapped from unique index
            string tempVariableForUniqueResult = NamesOfEntities.MapByUniqueTemporary(PatternElementName);
            sourceCode.AppendFrontFormat("if({0}==null || !({0} is {1}))",
                tempVariableForUniqueResult, IsNode ? "GRGEN_LIBGR.INode" : "GRGEN_LIBGR.IEdge");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // assign the value to the candidate variable, cast it to the variable type
            string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("{0} = ({1}){2};\n",
                variableContainingCandidate, typeOfVariableContainingCandidate, tempVariableForUniqueResult);
        }

        bool IsNode;
    }

    /// <summary>
    /// Base class for search program operations
    /// filtering partial match
    /// (of the pattern part under construction)
    /// </summary>
    abstract class CheckPartialMatch : CheckOperation
    {
    }

    /// <summary>
    /// Base class for search program operations
    /// filtering partial match by searching further patterns based on found one
    /// i.e. by negative or independent patterns (nac/pac)
    /// </summary>
    abstract class CheckPartialMatchByNegativeOrIndependent : CheckPartialMatch
    {
        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public string[] NeededElements;

        // search program of the negative/independent pattern
        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "check whether the negative pattern applies" operation
    /// </summary>
    class CheckPartialMatchByNegative : CheckPartialMatchByNegativeOrIndependent
    {
        public CheckPartialMatchByNegative(string[] neededElements)
        {
            NeededElements = neededElements;
        }

        public override void Dump(SourceBuilder builder)
        {
            Debug.Assert(CheckFailedOperations == null, "check negative without direct check failed code");
            // first dump local content
            builder.AppendFront("CheckPartialMatch ByNegative with ");
            foreach (string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// NegativePattern \n");
            // currently needed because of multiple negMapped backup variables with same name
            // todo: assign names to negatives, mangle that name in, then remove block again
            // todo: remove (neg)mapped backup variables altogether, then remove block again
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            //if(sourceCode.CommentSourceCode) reinsert when block is removed
            //    sourceCode.AppendFront("// NegativePattern end\n");
        }
    }

    /// <summary>
    /// Class representing "check whether the independent pattern applies" operation
    /// </summary>
    class CheckPartialMatchByIndependent : CheckPartialMatchByNegativeOrIndependent
    {
        public CheckPartialMatchByIndependent(string[] neededElements)
        {
            NeededElements = neededElements;
        }

        public override void Dump(SourceBuilder builder)
        {
            Debug.Assert(CheckFailedOperations == null, "check independent without direct check failed code");
            // first dump local content
            builder.AppendFront("CheckPartialMatch ByIndependent with ");
            foreach (string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// IndependentPattern \n");
            // currently needed because of multiple idptMapped backup variables with same name
            // todo: assign names to independents, mangle that name in, then remove block again
            // todo: remove (idpt)mapped backup variables altogether, then remove block again
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            //if(sourceCode.CommentSourceCode) reinsert when block is removed
            //    sourceCode.AppendFront("// IndependentPattern end\n");
        }

        // the corresponding check failed operation located after this one (in contrast to the check succeeded operation nested inside this one)
        public CheckContinueMatchingOfIndependentFailed CheckIndependentFailed;
    }

    /// <summary>
    /// Class representing "check whether the condition applies" operation
    /// </summary>
    class CheckPartialMatchByCondition : CheckPartialMatch
    {
        public CheckPartialMatchByCondition(
            string conditionExpression,
            string[] neededNodes,
            string[] neededEdges,
            string[] neededVariables)
        {
            ConditionExpression = conditionExpression;
            NeededVariables = neededVariables;
            
            int i = 0;
            NeededElements = new string[neededNodes.Length + neededEdges.Length];
            NeededElementIsNode = new bool[neededNodes.Length + neededEdges.Length];
            foreach (string neededNode in neededNodes)
            {
                NeededElements[i] = neededNode;
                NeededElementIsNode[i] = true;
                ++i;
            }
            foreach (string neededEdge in neededEdges)
            {
                NeededElements[i] = neededEdge;
                NeededElementIsNode[i] = false;
                ++i;
            }
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckPartialMatch ByCondition ");
            builder.AppendFormat("{0} with ", ConditionExpression);
            foreach(string neededElement in NeededElements)
            {
                builder.Append(neededElement);
                builder.Append(" ");
            }
            foreach(string neededVar in NeededVariables)
            {
                builder.Append(neededVar);
                builder.Append(" ");
            }
            builder.Append("\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Condition \n");

            // open decision
            sourceCode.AppendFront("if(");
            // emit condition expression
            sourceCode.AppendFormat("!({0})", ConditionExpression);
            // close decision
            sourceCode.Append(") ");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string ConditionExpression;
        public string[] NeededElements;
        public bool[] NeededElementIsNode;
        public string[] NeededVariables;
    }

    /// <summary>
    /// Class representing "check whether the subpatterns of the pattern were found" operation
    /// </summary>
    class CheckPartialMatchForSubpatternsFound : CheckPartialMatch
    {
        public CheckPartialMatchForSubpatternsFound(
            string negativeIndependentNamePrefix)
        {
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckPartialMatch ForSubpatternsFound\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether subpatterns were found \n");

            // emit decision
            sourceCode.AppendFrontFormat("if({0}matchesList.Count>0) ", NegativeIndependentNamePrefix);

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class representing "check whether the current partial match is a duplicate" operation
    /// </summary>
    class CheckPartialMatchForDuplicate : CheckPartialMatch
    {
        public CheckPartialMatchForDuplicate(
            string rulePatternClassName, 
            string patternName, 
            string[] neededElements,
            string[] matchObjectPaths,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            NeededElements = neededElements;
            MatchObjectPaths = matchObjectPaths;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("CheckPartialMatch ForDuplicate with ");
            foreach(string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether a duplicate match to be purged was found \n");

            // emit hash variable declaration
            sourceCode.AppendFrontFormat("int {0} = 0;\n", NamesOfEntities.DuplicateMatchHashVariable());

            // only do the rest if more than one match is requested
            sourceCode.AppendFront("if(maxMatches!=1) {\n");
            sourceCode.Indent();

            // emit found matches hash map initialization as needed
            sourceCode.AppendFrontFormat("if({0}==null) {0} = new Dictionary<int, {1}>();\n",
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName));

            // emit hash variable initialization with result of hash computation 
            sourceCode.AppendFrontFormat("{0} = unchecked(", 
                NamesOfEntities.DuplicateMatchHashVariable());
            EmitHashComputation(sourceCode, NeededElements.Length - 1);
            sourceCode.Append(");\n");

            // emit check whether hash is contained in found matches hash map
            sourceCode.AppendFrontFormat("if({0}.ContainsKey({1}))\n", 
                NamesOfEntities.FoundMatchesForFilteringVariable(), 
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit check whether one of the matches in the hash map with same hash is equal to the locally matched elements
            sourceCode.AppendFrontFormat("{0} {1} = {2}[{3}];\n", 
                RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                NamesOfEntities.DuplicateMatchCandidateVariable(),
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFront("do {\n");
            sourceCode.Indent();

            // emit check for same elements
            sourceCode.AppendFront("if(");
            for(int i = 0; i < NeededElements.Length; ++i)
            {
                if(i != 0)
                    sourceCode.Append(" && ");
                sourceCode.AppendFormat("{0}._{1} == {2}",
                    NamesOfEntities.DuplicateMatchCandidateVariable() + MatchObjectPaths[i],
                    NamesOfEntities.MatchName(NeededElementsUnprefixedName[i], NeededElementsIsNode[i] ? BuildMatchObjectType.Node : BuildMatchObjectType.Edge), 
                    NamesOfEntities.CandidateVariable(NeededElements[i]));
            }
            sourceCode.Append(")\n");

            // emit check failed code, i.e. the current local match is equivalent to one of the already found ones, a duplicate
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // close "emit check whether one of the matches in the hash map with same hash is equal to the locally matched elements"
            // switching to next match with the same hash, if available
            sourceCode.Unindent();
            sourceCode.AppendFront("} ");
            sourceCode.AppendFormat("while(({0} = {0}.nextWithSameHash) != null);\n", 
                NamesOfEntities.DuplicateMatchCandidateVariable());

            // close "emit check whether hash is contained in found matches hash map"
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // close "only do the rest if more than one match is requested"
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        void EmitHashComputation(SourceBuilder sourceCode, int i)
        {
            if(i == 0)
                sourceCode.Append("23");
            else
            {
                sourceCode.Append("(");
                EmitHashComputation(sourceCode, i - 1);
                sourceCode.AppendFormat("*17 + {0}.GetHashCode()",
                    NamesOfEntities.CandidateVariable(NeededElements[i]));
                sourceCode.Append(")");
            }
        }

        public string RulePatternClassName;
        public string PatternName;
        public string[] NeededElements;
        public string[] MatchObjectPaths;
        public string[] NeededElementsUnprefixedName;
        public bool[] NeededElementsIsNode;
    }

    /// <summary>
    /// Class representing a fill operation of the current partial match into a set to prevent duplicates
    /// </summary>
    class FillPartialMatchForDuplicateChecking : SearchProgramOperation
    {
        public FillPartialMatchForDuplicateChecking(
            string rulePatternClassName,
            string patternName,
            string[] neededElements,
            string[] neededElementsUnprefixedName,
            bool[] neededElementsIsNode,
            bool parallelizedAction)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            NeededElements = neededElements;
            NeededElementsUnprefixedName = neededElementsUnprefixedName;
            NeededElementsIsNode = neededElementsIsNode;
            ParallelizedAction = parallelizedAction;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("FillPartialMatchForDuplicateChecking with ");
            foreach(string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("if({0} != null)\n", NamesOfEntities.FoundMatchesForFilteringVariable());
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit check whether hash is contained in found matches hash map
            sourceCode.AppendFrontFormat("if(!{0}.ContainsKey({1}))\n",
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // if not, just insert it
            sourceCode.AppendFrontFormat("{0}[{1}] = match;\n",
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("else\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // otherwise loop till end of collision list of the hash set, insert there
            sourceCode.AppendFrontFormat("{0} {1} = {2}[{3}];\n",
                RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                NamesOfEntities.DuplicateMatchCandidateVariable(),
                NamesOfEntities.FoundMatchesForFilteringVariable(),
                NamesOfEntities.DuplicateMatchHashVariable());
            sourceCode.AppendFrontFormat("while({0}.nextWithSameHash != null) {0} = {0}.nextWithSameHash;\n",
                NamesOfEntities.DuplicateMatchCandidateVariable());

            sourceCode.AppendFrontFormat("{0}.nextWithSameHash = match;\n",
                 NamesOfEntities.DuplicateMatchCandidateVariable());

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            if(ParallelizedAction)
            {
                sourceCode.AppendFrontFormat("match.{0} = {0};\n",
                    NamesOfEntities.DuplicateMatchHashVariable());
            }

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string RulePatternClassName;
        public string PatternName;
        public string[] NeededElements;
        public string[] NeededElementsUnprefixedName;
        public bool[] NeededElementsIsNode;
        public bool ParallelizedAction;
    }

    /// <summary>
    /// Class representing operations to execute upon candidate checking succeded;
    /// writing isomorphy information to graph, for isomorphy checking later on
    /// (mapping graph element to pattern element)
    /// </summary>
    class AcceptCandidate : SearchProgramOperation
    {
        public AcceptCandidate(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel,
            bool lockForAllThreads)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
            LockForAllThreads = lockForAllThreads;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidate ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
            builder.AppendFormat("parallel:{0} all:{1} ",
                Parallel, LockForAllThreads);
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate =
                NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("uint {0};\n", variableContainingBackupOfMappedMember);

            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED << isoSpace";
                if(LockForAllThreads)
                {
                    sourceCode.AppendFrontFormat("{0} = flagsPerElement0[{1}.uniqueId] & {2};\n",
                        variableContainingBackupOfMappedMember,
                        variableContainingCandidate,
                        isMatchedBit);
                    sourceCode.AppendFrontFormat("for(int i=0; i<numWorkerThreads; ++i) graph.flagsPerThreadPerElement[i][{0}.uniqueId] |= (ushort)({1});\n",
                        variableContainingCandidate,
                        isMatchedBit);
                }
                else
                {
                    sourceCode.AppendFrontFormat("{0} = flagsPerElement[{1}.uniqueId] & {2};\n",
                        variableContainingBackupOfMappedMember,
                        variableContainingCandidate,
                        isMatchedBit);
                    sourceCode.AppendFrontFormat("flagsPerElement[{0}.uniqueId] |= (ushort)({1});\n",
                        variableContainingCandidate,
                        isMatchedBit);
                }

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else {\n");
                    sourceCode.Indent();

                    if(LockForAllThreads)
                    {
                        sourceCode.AppendFrontFormat("{0} = graph.perThreadInIsoSpaceMatchedElements[0][isoSpace - (int)"
                            + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].ContainsKey({1}) ? 1U : 0U;\n",
                            variableContainingBackupOfMappedMember, variableContainingCandidate);
                        sourceCode.AppendFrontFormat("if({0} == 0) for(int i=0; i<numWorkerThreads; ++i) graph.perThreadInIsoSpaceMatchedElements[i][isoSpace - (int)"
                            + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].Add({1},{1});\n",
                            variableContainingBackupOfMappedMember, variableContainingCandidate);
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("{0} = graph.perThreadInIsoSpaceMatchedElements[threadId][isoSpace - (int)"
                            + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].ContainsKey({1}) ? 1U : 0U;\n",
                            variableContainingBackupOfMappedMember, variableContainingCandidate);
                        sourceCode.AppendFrontFormat("if({0} == 0) graph.perThreadInIsoSpaceMatchedElements[threadId][isoSpace - (int)"
                            + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].Add({1},{1});\n",
                            variableContainingBackupOfMappedMember, variableContainingCandidate);
                    }

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace";
                sourceCode.AppendFrontFormat("{0} = {1}.lgspFlags & {2};\n",
                    variableContainingBackupOfMappedMember, variableContainingCandidate, isMatchedBit);
                sourceCode.AppendFrontFormat("{0}.lgspFlags |= {1};\n",
                    variableContainingCandidate, isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else {\n");
                    sourceCode.Indent();

                    sourceCode.AppendFrontFormat("{0} = graph.inIsoSpaceMatchedElements[isoSpace - (int) "
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE].ContainsKey({1}) ? 1U : 0U;\n",
                        variableContainingBackupOfMappedMember, variableContainingCandidate);
                    sourceCode.AppendFrontFormat("if({0} == 0) graph.inIsoSpaceMatchedElements[isoSpace - (int) "
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE].Add({1},{1});\n",
                        variableContainingBackupOfMappedMember, variableContainingCandidate);

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
        public bool LockForAllThreads;
    }

    /// <summary>
    /// Class representing operations to execute upon candidate gets accepted 
    /// into a complete match of its subpattern, locking candidate for other subpatterns
    /// </summary>
    class AcceptCandidateGlobal : SearchProgramOperation
    {
        public AcceptCandidateGlobal(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidateGlobal ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember = NamesOfEntities.VariableWithBackupOfIsMatchedGlobalBit(
                PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("uint {0};\n", variableContainingBackupOfMappedMember);

            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFrontFormat("{0} = flagsPerElementGlobal[{1}.uniqueId] & {2};\n",
                    variableContainingBackupOfMappedMember,
                    variableContainingCandidate,
                    isMatchedBit);
                sourceCode.AppendFrontFormat("flagsPerElementGlobal[{0}.uniqueId] |= (ushort)({1});\n",
                    variableContainingCandidate,
                    isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else {\n");
                    sourceCode.Indent();

                    sourceCode.AppendFrontFormat("{0} = graph.perThreadInIsoSpaceMatchedElementsGlobal[threadId][isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].ContainsKey({1}) ? 1U : 0U;\n",
                        variableContainingBackupOfMappedMember, variableContainingCandidate);
                    sourceCode.AppendFrontFormat("if({0} == 0) graph.perThreadInIsoSpaceMatchedElementsGlobal[threadId][isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].Add({1},{1});\n",
                        variableContainingBackupOfMappedMember, variableContainingCandidate);

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFrontFormat("{0} = {1}.lgspFlags & {2};\n",
                    variableContainingBackupOfMappedMember, variableContainingCandidate, isMatchedBit);
                sourceCode.AppendFrontFormat("{0}.lgspFlags |= {1};\n",
                    variableContainingCandidate, isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else {\n");
                    sourceCode.Indent();

                    sourceCode.AppendFrontFormat("{0} = graph.inIsoSpaceMatchedElementsGlobal[isoSpace - (int) "
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE].ContainsKey({1}) ? 1U : 0U;\n",
                        variableContainingBackupOfMappedMember, variableContainingCandidate);
                    sourceCode.AppendFrontFormat("if({0} == 0) graph.inIsoSpaceMatchedElementsGlobal[isoSpace - (int) "
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE].Add({1},{1});\n",
                        variableContainingBackupOfMappedMember, variableContainingCandidate);

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing operations to execute upon candidate gets accepted 
    /// into a complete match of its subpattern, locking candidate for patternpath checks later on
    /// </summary>
    class AcceptCandidatePatternpath : SearchProgramOperation
    {
        public AcceptCandidatePatternpath(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidatePatternPath ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingBackupOfMappedMemberGlobalSome =
                NamesOfEntities.VariableWithBackupOfIsMatchedGlobalInSomePatternBit(PatternElementName, NegativeIndependentNamePrefix);
            sourceCode.AppendFrontFormat("uint {0};\n", variableContainingBackupOfMappedMemberGlobalSome);
            string isMatchedInSomePatternBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN";
            sourceCode.AppendFrontFormat("{0} = {1}.lgspFlags & {2};\n",
                variableContainingBackupOfMappedMemberGlobalSome, variableContainingCandidate, isMatchedInSomePatternBit);
            sourceCode.AppendFrontFormat("{0}.lgspFlags |= {1};\n",
                variableContainingCandidate, isMatchedInSomePatternBit);
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing operations to execute upon iterated pattern was accepted (increase counter)
    /// </summary>
    class AcceptIterated : SearchProgramOperation
    {
        public AcceptIterated()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptIterated \n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("// accept iterated instance match\n");
            sourceCode.AppendFront("++numMatchesIter;\n");
        }
    }

    /// <summary>
    /// Class representing operations undoing effects of candidate acceptance 
    /// when performing the backtracking step;
    /// (currently only) restoring isomorphy information in graph, as not needed any more
    /// (mapping graph element to pattern element)
    /// </summary>
    class AbandonCandidate : SearchProgramOperation
    {
        public AbandonCandidate(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel,
            bool lockForAllThreads)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
            LockForAllThreads = lockForAllThreads;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidate ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
            builder.AppendFormat("parallel:{0} all:{1} ",
                Parallel, LockForAllThreads);
            builder.AppendFormat("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate =
                NamesOfEntities.CandidateVariable(PatternElementName);

            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED << isoSpace";
                if(LockForAllThreads)
                {
                    sourceCode.AppendFrontFormat("for(int i=0; i<numWorkerThreads; ++i) graph.flagsPerThreadPerElement[i][{0}.uniqueId] &= (ushort)(~({1}) | {2});\n",
                        variableContainingCandidate,
                        isMatchedBit,
                        variableContainingBackupOfMappedMember);
                }
                else
                {
                    sourceCode.AppendFrontFormat("flagsPerElement[{0}.uniqueId] &= (ushort)(~({1}) | {2});\n",
                        variableContainingCandidate,
                        isMatchedBit, 
                        variableContainingBackupOfMappedMember);
                }

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else { \n");
                    sourceCode.Indent();

                    if(LockForAllThreads)
                    {
                        sourceCode.AppendFrontFormat("if({0} == 0) {{\n", variableContainingBackupOfMappedMember);
                        sourceCode.Indent();
                        sourceCode.AppendFrontFormat(
                            "for(int i=0; i<numWorkerThreads; ++i) graph.perThreadInIsoSpaceMatchedElements[i][isoSpace - (int)GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE]"
                                + ".Remove({0});\n", variableContainingCandidate);
                        sourceCode.Unindent();
                        sourceCode.AppendFront("}\n");
                    }
                    else
                    {
                        sourceCode.AppendFrontFormat("if({0} == 0) {{\n", variableContainingBackupOfMappedMember);
                        sourceCode.Indent();
                        sourceCode.AppendFrontFormat(
                            "graph.perThreadInIsoSpaceMatchedElements[threadId][isoSpace - (int)GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE]"
                                + ".Remove({0});\n", variableContainingCandidate);
                        sourceCode.Unindent();
                        sourceCode.AppendFront("}\n");
                    }

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace";
                sourceCode.AppendFrontFormat("{0}.lgspFlags = {0}.lgspFlags & ~({1}) | {2};\n",
                    variableContainingCandidate, isMatchedBit, variableContainingBackupOfMappedMember);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else { \n");
                    sourceCode.Indent();

                    sourceCode.AppendFrontFormat("if({0} == 0) {{\n", variableContainingBackupOfMappedMember);
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat(
                        "graph.inIsoSpaceMatchedElements[isoSpace - (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE]"
                            + ".Remove({0});\n", variableContainingCandidate);
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
        public bool LockForAllThreads;
    }

    /// <summary>
    /// Class representing operations undoing effects of candidate acceptance 
    /// into complete match of it's subpattern when performing the backtracking step (unlocks candidate)
    /// </summary>
    class AbandonCandidateGlobal : SearchProgramOperation
    {
        public AbandonCandidateGlobal(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidateGlobal ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember = NamesOfEntities.VariableWithBackupOfIsMatchedGlobalBit(
                PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFrontFormat("flagsPerElementGlobal[{0}.uniqueId] &= (ushort)(~({1}) | {2});\n",
                    variableContainingCandidate,
                    isMatchedBit,
                    variableContainingBackupOfMappedMember);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else { \n");
                    sourceCode.Indent();

                    sourceCode.AppendFrontFormat("if({0} == 0) {{\n", variableContainingBackupOfMappedMember);
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat(
                        "graph.perThreadInIsoSpaceMatchedElementsGlobal[threadId][isoSpace - (int)GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE]"
                            + ".Remove({0});\n", variableContainingCandidate);
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                }

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFrontFormat("{0}.lgspFlags = {0}.lgspFlags & ~({1}) | {2};\n",
                    variableContainingCandidate, isMatchedBit, variableContainingBackupOfMappedMember);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else { \n");
                    sourceCode.Indent();

                    sourceCode.AppendFrontFormat("if({0} == 0) {{\n", variableContainingBackupOfMappedMember);
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat(
                        "graph.inIsoSpaceMatchedElementsGlobal[isoSpace - (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE]"
                            + ".Remove({0});\n", variableContainingCandidate);
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if positive
        public bool IsNode; // node|edge
        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing operations undoing effects of patternpath candidate acceptance 
    /// into complete match of it's subpattern when performing the backtracking step (unlocks candidate)
    /// </summary>
    class AbandonCandidatePatternpath : SearchProgramOperation
    {
        public AbandonCandidatePatternpath(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidatePatternpath");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingBackupOfMappedMemberGlobalSome =
                NamesOfEntities.VariableWithBackupOfIsMatchedGlobalInSomePatternBit(PatternElementName, NegativeIndependentNamePrefix);
            string isMatchedInSomePatternBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN";
            sourceCode.AppendFrontFormat("{0}.lgspFlags = {0}.lgspFlags & ~({1}) | {2};\n",
                variableContainingCandidate, isMatchedInSomePatternBit, variableContainingBackupOfMappedMemberGlobalSome);
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if positive
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing operations to execute upon iterated pattern was abandoned (decrease counter)
    /// </summary>
    class AbandonIterated : SearchProgramOperation
    {
        public AbandonIterated()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonIterated \n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("--numMatchesIter;\n");
        }
    }

    /// <summary>
    /// Class representing create inlined subpattern matches operations
    /// </summary>
    class CreateInlinedSubpatternMatch : SearchProgramOperation
    {
        public CreateInlinedSubpatternMatch(
            string rulePatternClassName,
            string patternName,
            string matchName,
            string matchOfEnclosingPatternName)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            MatchName = matchName;
            MatchOfEnclosingPatternName = matchOfEnclosingPatternName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("CreateInlinedSubpatternMatch \n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}.{1} match_{2} = new {0}.{1}();\n",
                RulePatternClassName, NamesOfEntities.MatchClassName(PatternName), MatchName);
            sourceCode.AppendFrontFormat("match_{0}.SetMatchOfEnclosingPattern({1});\n",
                MatchName, MatchOfEnclosingPatternName);
        }

        string RulePatternClassName;
        string PatternName;
        string MatchName;
        string MatchOfEnclosingPatternName;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern without contained subpatterns was matched
    /// </summary>
    class PositivePatternWithoutSubpatternsMatched : SearchProgramOperation
    {
        public PositivePatternWithoutSubpatternsMatched(
            string rulePatternClassName,
            string patternName,
            bool inParallelizedBody)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            InParallelizedBody = inParallelizedBody;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PositivePatternMatched \n");
            if(InParallelizedBody)
                builder.AppendFront("InParallelizedBody \n");

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(InParallelizedBody)
                sourceCode.AppendFrontFormat("{0}.{1} match = parallelTaskMatches[threadId].GetNextUnfilledPosition();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
            else
                sourceCode.AppendFrontFormat("{0}.{1} match = matches.GetNextUnfilledPosition();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));

            // emit match building operations
            MatchBuildingOperations.Emit(sourceCode);

            if(InParallelizedBody)
            {
                sourceCode.AppendFront("match.IterationNumber = currentIterationNumber;\n");
                sourceCode.AppendFront("parallelTaskMatches[threadId].PositionWasFilledFixIt();\n");
            }
            else
                sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");
        }

        string RulePatternClassName;
        string PatternName;
        bool InParallelizedBody;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a subpattern without contained subpatterns was matched (as the last element of the search)
    /// </summary>
    class LeafSubpatternMatched : SearchProgramOperation
    {
        public LeafSubpatternMatched(
            string rulePatternClassName, 
            string patternName,
            bool isIteratedNullMatch)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            IsIteratedNullMatch = isIteratedNullMatch;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("LeafSubpatternMatched {0}\n", IsIteratedNullMatch ? "IteratedNullMatch " : "");

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();\n");
            sourceCode.AppendFront("foundPartialMatches.Add(currentFoundPartialMatch);\n");

            sourceCode.AppendFrontFormat("{0}.{1} match = new {0}.{1}();\n",
                RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
            if (IsIteratedNullMatch) {
                sourceCode.AppendFront("match._isNullMatch = true; // null match of iterated pattern\n");
            } else {
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
            }
            sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");
        }

        string RulePatternClassName;
        string PatternName;
        bool IsIteratedNullMatch;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Available types of PatternAndSubpatternsMatched operations
    /// </summary>
    enum PatternAndSubpatternsMatchedType
    {
        Action,
        Iterated,
        IteratedNullMatch,
        SubpatternOrAlternative
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern was matched and all of it's subpatterns were matched at least once
    /// </summary>
    class PatternAndSubpatternsMatched : SearchProgramOperation
    {
        public PatternAndSubpatternsMatched(
            string rulePatternClassName,
            string patternName,
            bool inParallelizedBody,
            PatternAndSubpatternsMatchedType type)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            InParallelizedBody = inParallelizedBody;
            Type = type;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PatternAndSubpatternsMatched ");
            if(InParallelizedBody)
                builder.Append("InParallelizedBody ");
            switch (Type)
            {
                case PatternAndSubpatternsMatchedType.Action: builder.Append("Action \n"); break;
                case PatternAndSubpatternsMatchedType.Iterated: builder.Append("Iterated \n"); break;
                case PatternAndSubpatternsMatchedType.IteratedNullMatch: builder.Append("IteratedNullMatch \n"); break;
                case PatternAndSubpatternsMatchedType.SubpatternOrAlternative: builder.Append("SubpatternOrAlternative \n"); break;
                default: builder.Append("INTERNAL ERROR\n"); break;
            }

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type==PatternAndSubpatternsMatchedType.Iterated)
            {
                sourceCode.AppendFront("patternFound = true;\n");
            }

            if (Type!=PatternAndSubpatternsMatchedType.Action)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object\n");
                sourceCode.AppendFront("foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0}.{1} match = new {0}.{1}();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                if (Type == PatternAndSubpatternsMatchedType.IteratedNullMatch) {
                    sourceCode.AppendFront("match._isNullMatch = true; // null match of iterated pattern\n");
                }

                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else // top-level pattern with subpatterns/alternatives
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it\n");
                sourceCode.AppendFront("foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                if(InParallelizedBody)
                    sourceCode.AppendFrontFormat("{0}.{1} match = parallelTaskMatches[threadId].GetNextUnfilledPosition();\n",
                        RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                else
                    sourceCode.AppendFrontFormat("{0}.{1} match = matches.GetNextUnfilledPosition();\n",
                        RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                if(InParallelizedBody)
                {
                    sourceCode.AppendFront("match.IterationNumber = currentIterationNumber;\n");
                    sourceCode.AppendFront("parallelTaskMatches[threadId].PositionWasFilledFixIt();\n");
                }
                else
                    sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");
                
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("matchesList.Clear();\n");
            }
        }

        string RulePatternClassName;
        string PatternName;
        bool InParallelizedBody; 
        PatternAndSubpatternsMatchedType Type;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Available types of NegativeIndependentPatternMatched operations
    /// </summary>
    enum NegativeIndependentPatternMatchedType
    {
        WithoutSubpatterns,
        ContainingSubpatterns
    };

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a negative pattern was matched
    /// </summary>
    class NegativePatternMatched : SearchProgramOperation
    {
        public NegativePatternMatched(
            NegativeIndependentPatternMatchedType type,
            string negativeIndependentNamePrefix)
        {
            Type = type;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("NegativePatternMatched ");
            builder.Append(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns ?
                "WithoutSubpatterns\n" : "ContainingSubpatterns\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern found\n");
            }
            else
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern with contained subpatterns found\n");

                sourceCode.AppendFrontFormat("{0}matchesList.Clear();\n", NegativeIndependentNamePrefix);
            }
        }

        public NegativeIndependentPatternMatchedType Type;
        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a independent pattern was matched
    /// </summary>
    class IndependentPatternMatched : SearchProgramOperation
    {
        public IndependentPatternMatched(
            NegativeIndependentPatternMatchedType type,
            string negativeIndependentNamePrefix)
        {
            Type = type;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("IndependentPatternMatched ");
            builder.Append(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns ?
                "WithoutSubpatterns\n" : "ContainingSubpatterns\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// independent pattern found\n");
            }
            else
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// independent pattern with contained subpatterns found\n");
                sourceCode.AppendFrontFormat("Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = {0}matchesList[0];\n",
                    NegativeIndependentNamePrefix);
                sourceCode.AppendFrontFormat("{0}matchesList.Clear();\n", NegativeIndependentNamePrefix);
            }
        }

        public NegativeIndependentPatternMatchedType Type;
        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Available types of BuildMatchObject operations
    /// </summary>
    enum BuildMatchObjectType
    {
        Node,        // build match object with match for node 
        Edge,        // build match object with match for edge
        Variable,    // build match object with match for variable
        Subpattern,  // build match object with match for subpattern
        InlinedSubpattern, // build match object with match of an inlined subpattern
        Iteration,   // build match object with match for iteration
        Alternative, // build match object with match for alternative
        Independent  // build match object with match for independent
    }

    /// <summary>
    /// Class representing "pattern was matched, now build match object" operation
    /// </summary>
    class BuildMatchObject : SearchProgramOperation
    {
        public BuildMatchObject(
            BuildMatchObjectType type,
            string patternElementType,
            string patternElementUnprefixedName,
            string patternElementName,
            string rulePatternClassName,
            string pathPrefixForEnum,
            string matchObjectName,
            int numSubpatterns)
        {
            Debug.Assert(type != BuildMatchObjectType.Independent);
            Type = type;
            PatternElementType = patternElementType;
            PatternElementUnprefixedName = patternElementUnprefixedName;
            PatternElementName = patternElementName;
            RulePatternClassName = rulePatternClassName;
            PathPrefixForEnum = pathPrefixForEnum;
            MatchObjectName = matchObjectName;
            NumSubpatterns = numSubpatterns; // only valid if type == Alternative
        }

        public BuildMatchObject(
            BuildMatchObjectType type,
            string patternElementUnprefixedName,
            string patternElementName,
            string rulePatternClassName,
            string matchObjectName,
            string matchClassName)
        {
            Debug.Assert(type == BuildMatchObjectType.Independent);
            Type = type;
            PatternElementUnprefixedName = patternElementUnprefixedName;
            PatternElementName = patternElementName;
            RulePatternClassName = rulePatternClassName;
            MatchObjectName = matchObjectName;
            MatchClassName = matchClassName;
        }

        public override void Dump(SourceBuilder builder)
        {
            string typeDescr;
            switch(Type)
            {
                case BuildMatchObjectType.Node:              typeDescr = "Node";        break;
                case BuildMatchObjectType.Edge:              typeDescr = "Edge";        break;
                case BuildMatchObjectType.Variable:          typeDescr = "Variable";    break;
                case BuildMatchObjectType.Subpattern:        typeDescr = "Subpattern";  break;
                case BuildMatchObjectType.InlinedSubpattern: typeDescr = "InlinedSubpattern"; break;
                case BuildMatchObjectType.Iteration:         typeDescr = "Iteration"; break;
                case BuildMatchObjectType.Alternative:       typeDescr = "Alternative"; break;
                case BuildMatchObjectType.Independent:       typeDescr = "Independent"; break;
                default:                                     typeDescr = ">>UNKNOWN<<"; break;
            }

            builder.AppendFrontFormat("BuildMatchObject {0} name {1} with {2} within {3}\n",
                typeDescr, MatchObjectName, PatternElementName, RulePatternClassName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string matchName = NamesOfEntities.MatchName(PatternElementUnprefixedName, Type);
            if (Type == BuildMatchObjectType.Node || Type == BuildMatchObjectType.Edge)
            {
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, variableContainingCandidate);
            }
            else if(Type == BuildMatchObjectType.Variable)
            {
                string variableName = NamesOfEntities.Variable(PatternElementName);
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, variableName);
            }
            else if(Type == BuildMatchObjectType.Subpattern)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = (@{2})currentFoundPartialMatch.Pop();\n",
                    MatchObjectName, matchName, PatternElementType);
                sourceCode.AppendFrontFormat("{0}._{1}._matchOfEnclosingPattern = {0};\n",
                    MatchObjectName, matchName);
            }
            else if(Type == BuildMatchObjectType.InlinedSubpattern)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = match_{2};\n",
                    MatchObjectName, matchName, PatternElementName);
            }
            else if(Type == BuildMatchObjectType.Iteration)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = new GRGEN_LGSP.LGSPMatchesList<{2}.{3}, {2}.{4}>(null);\n",
                    MatchObjectName, matchName,
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternElementType), NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.AppendFrontFormat("while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is {0}.{1}) ", 
                    RulePatternClassName, NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.Append("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("{0}.{1} cfpm = ({0}.{1})currentFoundPartialMatch.Pop();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternElementType));
                sourceCode.AppendFront("if(cfpm.IsNullMatch) break;\n");
                sourceCode.AppendFrontFormat("cfpm.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName);
                sourceCode.AppendFrontFormat("{0}._{1}.Add(cfpm);\n",
                    MatchObjectName, matchName);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == BuildMatchObjectType.Alternative)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = ({2}.{3})currentFoundPartialMatch.Pop();\n",
                    MatchObjectName, matchName, RulePatternClassName, NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.AppendFrontFormat("{0}._{1}.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName, matchName);
            }
            else //if (Type == BuildMatchObjectType.Independent)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, NamesOfEntities.MatchedIndependentVariable(PatternElementName));
                sourceCode.AppendFrontFormat("{0} = new {1}({0});\n",
                    NamesOfEntities.MatchedIndependentVariable(PatternElementName),
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(MatchClassName));
                sourceCode.AppendFrontFormat("{0}._{1}.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName, matchName);
            }
        }

        public BuildMatchObjectType Type;
        public string PatternElementType;
        public string PatternElementUnprefixedName;
        public string PatternElementName;
        public string RulePatternClassName;
        public string MatchClassName;
        public string PathPrefixForEnum;
        public string MatchObjectName;
        public int NumSubpatterns;
    }

    /// <summary>
    /// Class representing implicit yield assignment operations,
    /// to bubble up values from nested patterns and subpatterns to the containing pattern
    /// </summary>
    class BubbleUpYieldAssignment : SearchProgramOperation
    {
        public BubbleUpYieldAssignment(
            EntityType type,
            string targetPatternElementName,
            string nestedMatchObjectName,
            string sourcePatternElementUnprefixedName
            )
        {
            Type = type;
            TargetPatternElementName = targetPatternElementName;
            NestedMatchObjectName = nestedMatchObjectName;
            SourcePatternElementUnprefixedName = sourcePatternElementUnprefixedName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("BubbleUpYieldAssignemt {0} {1} = {2}.{3}\n",
                NamesOfEntities.ToString(Type), TargetPatternElementName, NestedMatchObjectName, SourcePatternElementUnprefixedName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string targetPatternElement = Type==EntityType.Variable ? NamesOfEntities.Variable(TargetPatternElementName) : NamesOfEntities.CandidateVariable(TargetPatternElementName);
            string sourcePatternElement = NamesOfEntities.MatchName(SourcePatternElementUnprefixedName, Type);
            sourceCode.AppendFrontFormat("{0} = {1}._{2}; // bubble up\n",
                targetPatternElement, NestedMatchObjectName, sourcePatternElement);
        }

        EntityType Type;
        string TargetPatternElementName;
        string NestedMatchObjectName;
        string SourcePatternElementUnprefixedName;
    }

    /// <summary>
    /// Class representing a block around implicit yield bubble up assignments for nested iterateds
    /// </summary>
    class BubbleUpYieldIterated : SearchProgramOperation
    {
        public BubbleUpYieldIterated(string nestedMatchObjectName)
        {
            NestedMatchObjectName = nestedMatchObjectName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldIterated on {0}\n", NestedMatchObjectName);

            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("if({0}.Count>0) {{\n", NestedMatchObjectName);
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        string NestedMatchObjectName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing a block around explicit yield accumulate up assignments for nested iterateds
    /// </summary>
    class AccumulateUpYieldIterated : SearchProgramOperation
    {
        public AccumulateUpYieldIterated(string nestedMatchObjectName, string iteratedMatchTypeName, string helperMatchName)
        {
            NestedMatchObjectName = nestedMatchObjectName;
            IteratedMatchTypeName = iteratedMatchTypeName;
            HelperMatchName = helperMatchName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldIterated on {0} type {1}\n", 
                NestedMatchObjectName, IteratedMatchTypeName);

            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("foreach({0} {1} in {2}) {{\n", 
                IteratedMatchTypeName, HelperMatchName, NestedMatchObjectName);
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        string NestedMatchObjectName;
        string IteratedMatchTypeName;
        string HelperMatchName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing a block around implicit yield bubble up assignments for nested alternatives
    /// </summary>
    class BubbleUpYieldAlternativeCase : SearchProgramOperation
    {
        public BubbleUpYieldAlternativeCase(string matchObjectName, string nestedMatchObjectName,
            string alternativeCaseMatchTypeName, string helperMatchName, bool first)
        {
            MatchObjectName = matchObjectName;
            NestedMatchObjectName = nestedMatchObjectName;
            AlternativeCaseMatchTypeName = alternativeCaseMatchTypeName;
            HelperMatchName = helperMatchName;
            First = first;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldAlternativeCase on {0}.{1} case match type {2} {3}\n",
                MatchObjectName, NestedMatchObjectName, AlternativeCaseMatchTypeName, First ? "first" : "");

            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}if({1}.{2} is {3}) {{\n",
                First ? "" : "else ", MatchObjectName, NestedMatchObjectName, AlternativeCaseMatchTypeName);
            sourceCode.Indent();
            sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.{3};\n",
                AlternativeCaseMatchTypeName, HelperMatchName, MatchObjectName, NestedMatchObjectName);

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        string MatchObjectName;
        string NestedMatchObjectName;
        string AlternativeCaseMatchTypeName;
        string HelperMatchName;
        bool First;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing (explicit) local yielding operations
    /// </summary>
    class LocalYielding : SearchProgramOperation
    {
        public LocalYielding(string yielding)
        {
            Yielding = yielding;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("LocalYielding {0}\n", Yielding);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0}", Yielding);
        }

        string Yielding;
    }

    /// <summary>
    /// Class representing a yielding block
    /// </summary>
    class YieldingBlock : SearchProgramOperation
    {
        public YieldingBlock(string name)
        {
            Name = name;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("YieldingBlock {0}\n", Name);
            
            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("{ // " + Name + "\n");

            sourceCode.Indent();
            NestedOperationsList.Emit(sourceCode);
            sourceCode.Unindent();
            
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        string Name;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Available types of AdjustListHeads operations
    /// </summary>
    enum AdjustListHeadsTypes
    {
        GraphElements,
        IncidentEdges
    }

    /// <summary>
    /// Class representing "adjust list heads" operation ("listentrick")
    /// </summary>
    class AdjustListHeads : SearchProgramOperation
    {
        public AdjustListHeads(
            AdjustListHeadsTypes type,
            string patternElementName,
            bool isNode,
            bool parallel)
        {
            Debug.Assert(type == AdjustListHeadsTypes.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
            Parallel = parallel;
        }

        public AdjustListHeads(
            AdjustListHeadsTypes type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType incidentType,
            bool parallel)
        {
            Debug.Assert(type == AdjustListHeadsTypes.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            IncidentType = incidentType;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AdjustListHeads ");
            if(Type==AdjustListHeadsTypes.GraphElements) {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1} {2}\n",
                    PatternElementName, IsNode, Parallel ? "Parallel " : "");
            } else { // Type==AdjustListHeadsTypes.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from:{1} incident type:{2} {3}\n",
                    PatternElementName, StartingPointNodeName, IncidentType.ToString(), Parallel ? "Parallel " : "");
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == AdjustListHeadsTypes.GraphElements)
            {
                if(Parallel)
                {
                    if(IsNode)
                        sourceCode.AppendFrontFormat("moveHeadAfterNodes[threadId].Add({0});\n",
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    else
                        sourceCode.AppendFrontFormat("moveHeadAfterEdges[threadId].Add({0});\n",
                            NamesOfEntities.CandidateVariable(PatternElementName));
                }
                else
                    sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                         NamesOfEntities.CandidateVariable(PatternElementName));
            }
            else //Type == AdjustListHeadsTypes.IncidentEdges
            {
                if (IncidentType == IncidentEdgeType.Incoming)
                {
                    if(Parallel)
                        sourceCode.AppendFrontFormat("moveInHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    else
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                }
                else if (IncidentType == IncidentEdgeType.Outgoing)
                {
                    if(Parallel)
                        sourceCode.AppendFrontFormat("moveOutHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    else
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                }
                else // IncidentType == IncidentEdgeType.IncomingOrOutgoing
                {
                    sourceCode.AppendFrontFormat("if({0}==0)",
                        NamesOfEntities.DirectionRunCounterVariable(PatternElementName));
                    sourceCode.Append(" {\n");
                    sourceCode.Indent();
                    if(Parallel)
                        sourceCode.AppendFrontFormat("moveInHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    else
                        sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else {\n");
                    sourceCode.Indent();
                    if(Parallel)
                        sourceCode.AppendFrontFormat("moveOutHeadAfter[threadId].Add(new KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>({0}, {1}));\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    else
                        sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                            NamesOfEntities.CandidateVariable(StartingPointNodeName),
                            NamesOfEntities.CandidateVariable(PatternElementName));
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
        }

        public AdjustListHeadsTypes Type;
        public string PatternElementName;
        public bool IsNode; // node|edge - only valid if GraphElements
        public string StartingPointNodeName; // only valid if IncidentEdges
        public IncidentEdgeType IncidentType; // only valid if IncidentEdges
        public bool Parallel;
    }

    /// <summary>
    /// Base class for search program operations
    /// to check whether to continue the matching process 
    /// (of the pattern part under construction)
    /// </summary>
    abstract class CheckContinueMatching : CheckOperation
    {
    }

    /// <summary>
    /// Class representing "check if matching process is to be aborted because
    /// there are no tasks to execute left" operation
    /// </summary>
    class CheckContinueMatchingTasksLeft : CheckContinueMatching
    {
        public CheckContinueMatchingTasksLeft()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching TasksLeft\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether there are subpattern matching tasks left to execute\n");

            sourceCode.AppendFront("if(openTasks.Count==0)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }
    }

    /// <summary>
    /// available types of check continue matching maximum matches reached operations (at which level/where nested inside does the check occur?)
    /// </summary>
    enum CheckMaximumMatchesType
    {
        Action,
        Subpattern,
        Iterated
    }

    /// <summary>
    /// Class representing "check if matching process is to be aborted because
    /// the maximum number of matches has been reached" operation
    /// listHeadAdjustment==false prevents listentrick
    /// </summary>
    class CheckContinueMatchingMaximumMatchesReached : CheckContinueMatching
    {
        public CheckContinueMatchingMaximumMatchesReached(
            CheckMaximumMatchesType type, bool listHeadAdjustment, bool inParallelizedBody,
            bool emitProfiling, string actionName, bool emitFirstLoopProfiling)
        {
            Type = type;
            ListHeadAdjustment = listHeadAdjustment;
            InParallelizedBody = inParallelizedBody;
            EmitProfiling = emitProfiling;
            ActionName = actionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public CheckContinueMatchingMaximumMatchesReached(
            CheckMaximumMatchesType type, bool listHeadAdjustment, bool inParallelizedBody)
        {
            Type = type;
            ListHeadAdjustment = listHeadAdjustment;
            InParallelizedBody = inParallelizedBody;
            EmitProfiling = false;
            ActionName = null;
            EmitFirstLoopProfiling = false;
        }


        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            if (Type == CheckMaximumMatchesType.Action) {
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached at Action level ");
            } else if (Type == CheckMaximumMatchesType.Subpattern) {
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached at Subpattern level ");
            } else if (Type == CheckMaximumMatchesType.Iterated) {
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached if Iterated ");
            }
            if (ListHeadAdjustment) builder.Append("ListHeadAdjustment ");
            if (InParallelizedBody) builder.Append("InParallelizedBody ");
            builder.Append("\n");

            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// if enough matches were found, we leave\n");

            if (Type == CheckMaximumMatchesType.Action) {
                if(InParallelizedBody)
                    sourceCode.AppendFront("if(maxMatches > 0 && parallelTaskMatches[threadId].Count >= maxMatches)\n");
                else
                    sourceCode.AppendFront("if(maxMatches > 0 && matches.Count >= maxMatches)\n");
            } else if (Type == CheckMaximumMatchesType.Subpattern) {
                sourceCode.AppendFront("if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)\n");
            } else if (Type == CheckMaximumMatchesType.Iterated) {
                sourceCode.AppendFront("if(true) // as soon as there's a match, it's enough for iterated\n");
            }
    
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(Type == CheckMaximumMatchesType.Action && InParallelizedBody)
                sourceCode.AppendFront("maxMatchesFound = true;\n");

            if(EmitProfiling)
            {
                if(InParallelizedBody)
                {
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", ActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", ActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsSingle.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", ActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsMultiple.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", ActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", ActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", ActionName);
                }
                else
                {
                    sourceCode.AppendFrontFormat("++actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].callsTotal;\n", ActionName);
                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", ActionName);
                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].loopStepsTotal += loopSteps;\n", ActionName);

                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", ActionName);
                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsTotal += loopSteps;\n", ActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", ActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", ActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsSingle.Add(loopSteps);\n", ActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsMultiple.Add(loopSteps);\n", ActionName);
                    
                    if(EmitFirstLoopProfiling)
                    {
                        sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", ActionName);
                        sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", ActionName);
                    }
                }
            }

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public CheckMaximumMatchesType Type;
        public bool ListHeadAdjustment;
        public bool InParallelizedBody;
        public bool EmitProfiling;
        public string ActionName;
        public bool EmitFirstLoopProfiling;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always succeed.
    /// Check of abort negative matching process always succeeds
    /// </summary>
    class CheckContinueMatchingOfNegativeFailed : CheckContinueMatching
    {
        public CheckContinueMatchingOfNegativeFailed(bool isIterationBreaking)
        {
            IsIterationBreaking = isIterationBreaking;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching OfNegativeFailed ");
            builder.Append(IsIterationBreaking ? "IterationBreaking\n" : "\n");

            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(IsIterationBreaking)
                sourceCode.AppendFront("breakIteration = true;\n");

            CheckFailedOperations.Emit(sourceCode);
        }

        public bool IsIterationBreaking;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always succeed.
    /// Check of abort independent matching process always succeeds
    /// </summary>
    class CheckContinueMatchingOfIndependentFailed : CheckContinueMatching
    {
        public CheckContinueMatchingOfIndependentFailed(CheckPartialMatchByIndependent checkIndependent, bool isIterationBreaking)
        {
            CheckIndependent = checkIndependent;
            IsIterationBreaking = isIterationBreaking;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching OfIndependentFailed ");
            builder.Append(IsIterationBreaking ? "IterationBreaking\n" : "\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(IsIterationBreaking)
                sourceCode.AppendFront("breakIteration = true;\n");

            CheckFailedOperations.Emit(sourceCode);
        }

        // the independent which failed
        public CheckPartialMatchByIndependent CheckIndependent;
        public bool IsIterationBreaking;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always fail.
    /// Check of abort independent matching process always fails
    /// </summary>
    class CheckContinueMatchingOfIndependentSucceeded : CheckContinueMatching
    {
        public CheckContinueMatchingOfIndependentSucceeded()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching OfIndependentSucceeded \n");
            // then operations for case check failed .. wording a bit rotten
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // nothing locally, just emit check failed code
            CheckFailedOperations.Emit(sourceCode);
        }
    }

    /// <summary>
    /// Class representing "check if matching process is to be continued with iterated pattern null match" operation
    /// </summary>
    class CheckContinueMatchingIteratedPatternNonNullMatchFound : CheckContinueMatching
    {
        public CheckContinueMatchingIteratedPatternNonNullMatchFound(bool isIterationBreaking)
        {
            IsIterationBreaking = isIterationBreaking;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFrontFormat("CheckContinueMatching IteratedPatternFound {0}\n",
                IsIterationBreaking ? "IterationBreaking" : "");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether the iterated pattern null match was found\n");

            sourceCode.Append("maxMatchesIterReached:\n");
            if(IsIterationBreaking)
                sourceCode.AppendFront("if(!patternFound && numMatchesIter>=minMatchesIter && !breakIteration)\n");
            else
                sourceCode.AppendFront("if(!patternFound && numMatchesIter>=minMatchesIter)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public bool IsIterationBreaking;
    }

    /// <summary>
    /// Available types of ContinueOperation operations
    /// </summary>
    enum ContinueOperationType
    {
        ByReturn,
        ByContinue,
        ByGoto
    }

    /// <summary>
    /// Class representing "continue matching there" control flow operations
    /// </summary>
    class ContinueOperation : SearchProgramOperation
    {
        public ContinueOperation(ContinueOperationType type,
            bool returnMatches, 
            bool inParallelizedBody)
        {
            Debug.Assert(type == ContinueOperationType.ByReturn);
            Type = type;
            ReturnMatches = returnMatches;
            InParallelizedBody = inParallelizedBody;
        }

        public ContinueOperation(ContinueOperationType type,
            bool continueAtParllelizedLoop)
        {
            Debug.Assert(type == ContinueOperationType.ByContinue);
            Type = type;
            ContinueAtParallelizedLoop = continueAtParllelizedLoop;
        }

        public ContinueOperation(ContinueOperationType type,
            string labelName)
        {
            Debug.Assert(type == ContinueOperationType.ByGoto);
            Type = type;
            LabelName = labelName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("ContinueOperation ");
            if(Type==ContinueOperationType.ByReturn) {
                builder.Append("ByReturn ");
                if(InParallelizedBody)
                builder.Append("InParallelizedBody ");
                builder.AppendFormat("return matches:{0}\n", ReturnMatches);
            } else if(Type==ContinueOperationType.ByContinue) {
                builder.AppendFormat("ByContinue {0}\n", ContinueAtParallelizedLoop ? "AtParallelizedLoop" : "");
            } else { // Type==ContinueOperationType.ByGoto
                builder.Append("ByGoto ");
                builder.AppendFormat("{0}\n", LabelName);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == ContinueOperationType.ByReturn)
            {
                if(InParallelizedBody)
                    sourceCode.AppendFrontFormat("return;\n");
                else
                {
                    if(ReturnMatches)
                    {
#if ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION
                        sourceCode.AppendFront("graph.CheckEmptyFlags();\n");
#endif
                        sourceCode.AppendFront("return matches;\n");
                    }
                    else
                    {
                        sourceCode.AppendFront("return;\n");
                    }
                }
            }
            else if(Type == ContinueOperationType.ByContinue)
            {
                if(ContinueAtParallelizedLoop) { //re-aquire parallel matching enumeration lock before entering loop header
                    //sourceCode.AppendFront("Monitor.Enter(this);\n");
                    sourceCode.AppendFront("while(Interlocked.CompareExchange(ref iterationLock, 1, 0) != 0) Thread.SpinWait(10);//lock parallel matching enumeration with iteration lock\n");
                }
                sourceCode.AppendFront("continue;\n");
            }
            else //Type == ContinueOperationType.ByGoto
            {
                sourceCode.AppendFrontFormat("goto {0};\n", LabelName);
            }
        }

        public ContinueOperationType Type;
        public bool ReturnMatches; // only valid if ByReturn
        public bool InParallelizedBody; // only valid if ByReturn
        public string LabelName; // only valid if ByGoto
        public bool ContinueAtParallelizedLoop; // only valid if ByContinue
    }

    /// <summary>
    /// Class representing location within code named with label,
    /// potential target of goto operation
    /// </summary>
    class GotoLabel : SearchProgramOperation
    {
        public GotoLabel()
        {
            LabelName = "label" + labelId.ToString();
            ++labelId;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GotoLabel ");
            builder.AppendFormat("{0}\n", LabelName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("{0}: ;\n", LabelName);
        }

        public string LabelName;
        private static int labelId = 0;
    }

    /// <summary>
    /// Available types of RandomizeListHeads operations
    /// </summary>
    enum RandomizeListHeadsTypes
    {
        GraphElements,
        IncidentEdges
    }

    /// <summary>
    /// Class representing "adjust list heads" operation ("listentrick")
    /// </summary>
    class RandomizeListHeads : SearchProgramOperation
    {
        public RandomizeListHeads(
            RandomizeListHeadsTypes type,
            string patternElementName,
            bool isNode)
        {
            Debug.Assert(type == RandomizeListHeadsTypes.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public RandomizeListHeads(
            RandomizeListHeadsTypes type,
            string patternElementName,
            string startingPointNodeName,
            bool isIncoming)
        {
            Debug.Assert(type == RandomizeListHeadsTypes.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            IsIncoming = isIncoming;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("RandomizeListHeads ");
            if (Type == RandomizeListHeadsTypes.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else
            { // Type==RandomizeListHeadsTypes.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from:{1} incoming:{2}\n",
                    PatternElementName, StartingPointNodeName, IsIncoming);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // --- move list head from current position to random position ---

            if (Type == RandomizeListHeadsTypes.GraphElements)
            {
                // emit declaration of variable containing random position to move list head to
                string variableContainingRandomPosition =
                    "random_position_" + PatternElementName;
                sourceCode.AppendFormat("int {0}", variableContainingRandomPosition);
                // emit initialization with ramdom position
                string graphMemberContainingElementListCountsByType =
                    IsNode ? "nodesByTypeCounts" : "edgesByTypeCounts";
                string variableContainingTypeIDForCandidate = 
                    NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
                sourceCode.AppendFormat(" = random.Next(graph.{0}[{1}]);\n",
                    graphMemberContainingElementListCountsByType,
                    variableContainingTypeIDForCandidate);
                // emit declaration of variable containing element at random position
                string typeOfVariableContainingElementAtRandomPosition = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingElementAtRandomPosition =
                    "random_element_" + PatternElementName;
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingElementAtRandomPosition,
                    variableContainingElementAtRandomPosition);
                // emit initialization with element list head
                string graphMemberContainingElementListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                sourceCode.AppendFormat(" = graph.{0}[{1}];\n",
                    graphMemberContainingElementListHeadByType, variableContainingTypeIDForCandidate);
                // emit iteration to get element at random position
                sourceCode.AppendFrontFormat(
                    "for(int i = 0; i < {0}; ++i) {1} = {1}.Next;\n",
                    variableContainingRandomPosition, variableContainingElementAtRandomPosition);
                // iteration left, element is the one at the requested random position
                // move list head after element at random position, 
                sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                    variableContainingElementAtRandomPosition);
                // effect is new random starting point for following iteration
            }
            else //Type == RandomizeListHeadsTypes.IncidentEdges
            {
                // emit "randomization only if list is not empty"
                string variableContainingStartingPointNode =
                    NamesOfEntities.CandidateVariable(StartingPointNodeName);
                string memberOfNodeContainingListHead =
                    IsIncoming ? "lgspInhead" : "lgspOuthead";
                sourceCode.AppendFrontFormat("if({0}.{1}!=null)\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit declaration of variable containing random position to move list head to, initialize it to 0 
                string variableContainingRandomPosition =
                    "random_position_" + PatternElementName;
                sourceCode.AppendFrontFormat("int {0} = 0;", variableContainingRandomPosition);
                // misuse variable to store length of list which is computed within the follwing iteration
                string memberOfEdgeContainingNextEdge =
                    IsIncoming ? "lgspInNext" : "lgspOutNext";
                sourceCode.AppendFrontFormat("for(GRGEN_LGSP.LGSPEdge edge = {0}.{1}; edge!={0}.{1}; edge=edge.{2}) ++{3};\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead,
                    memberOfEdgeContainingNextEdge, variableContainingRandomPosition);
                // emit initialization of variable containing ramdom position
                // now that the necessary length of the list is known after the iteration
                // given in the variable itself
                sourceCode.AppendFrontFormat("{0} = random.Next({0});\n",
                    variableContainingRandomPosition);
                // emit declaration of variable containing edge at random position
                string variableContainingEdgeAtRandomPosition =
                    "random_element_" + PatternElementName;
                sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0}",
                    variableContainingEdgeAtRandomPosition);
                // emit initialization with edge list head
                sourceCode.AppendFormat(" = {0}.{1};\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead);
                // emit iteration to get edge at random position
                sourceCode.AppendFrontFormat(
                    "for(int i = 0; i < {0}; ++i) {1} = {1}.{2};\n",
                    variableContainingRandomPosition,
                    variableContainingEdgeAtRandomPosition,
                    memberOfEdgeContainingNextEdge);
                // iteration left, edge is the one at the requested random position
                // move list head after edge at random position, 
                if (IsIncoming)
                {
                    sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                        variableContainingStartingPointNode,
                        variableContainingEdgeAtRandomPosition);
                }
                else
                {
                    sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                        variableContainingStartingPointNode,
                        variableContainingEdgeAtRandomPosition);
                }

                // close list is not empty check
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // effect is new random starting point for following iteration
            }
        }

        public RandomizeListHeadsTypes Type;
        public string PatternElementName;
        public bool IsNode; // node|edge - only valid if GraphElements
        public string StartingPointNodeName; // only valid if IncidentEdges
        public bool IsIncoming; // only valid if IncidentEdges
    }

    /// <summary>
    /// Available types of PushSubpatternTask and PopSubpatternTask operations
    /// </summary>
    enum PushAndPopSubpatternTaskTypes
    {
        Subpattern,
        Alternative,
        Iterated
    }

    /// <summary>
    /// Class representing "push a subpattern tasks to the open tasks stack" operation
    /// </summary>
    class PushSubpatternTask : SearchProgramOperation
    {
        public PushSubpatternTask(
            PushAndPopSubpatternTaskTypes type,
            string subpatternName,
            string subpatternElementName,
            string[] connectionName,
            string[] argumentExpressions,
            string negativeIndependentNamePrefix,
            string searchPatternpath,
            string matchOfNestingPattern,
            string lastMatchAtPreviousNestingLevel,
            bool parallel)
        {
            Debug.Assert(connectionName.Length == argumentExpressions.Length);
            Debug.Assert(type == PushAndPopSubpatternTaskTypes.Subpattern);
            Type = type;
            SubpatternName = subpatternName;
            SubpatternElementName = subpatternElementName;

            ConnectionName = connectionName;
            ArgumentExpressions = argumentExpressions;

            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;

            SearchPatternpath = searchPatternpath;
            MatchOfNestingPattern = matchOfNestingPattern;
            LastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;

            Parallel = parallel;
        }

        public PushSubpatternTask(
            PushAndPopSubpatternTaskTypes type,
            string pathPrefix,
            string alternativeOrIteratedName,
            string rulePatternClassName,
            string pathPrefixInRulePatternClass,
            string alternativeNameInRulePatternClass,
            string[] connectionName,
            string[] argumentExpressions,
            string negativeIndependentNamePrefix,
            string searchPatternpath,
            string matchOfNestingPattern,
            string lastMatchAtPreviousNestingLevel,
            bool parallel)
        {
            Debug.Assert(connectionName.Length == argumentExpressions.Length);
            Debug.Assert(type == PushAndPopSubpatternTaskTypes.Alternative
                || type == PushAndPopSubpatternTaskTypes.Iterated);
            Type = type;
            PathPrefix = pathPrefix;
            AlternativeOrIteratedName = alternativeOrIteratedName;
            RulePatternClassName = rulePatternClassName;
            PathPrefixInRulePatternClass = pathPrefixInRulePatternClass;
            AlternativeNameInRulePatternClass = alternativeNameInRulePatternClass;

            ConnectionName = connectionName;
            ArgumentExpressions = argumentExpressions;

            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;

            SearchPatternpath = searchPatternpath;
            MatchOfNestingPattern = matchOfNestingPattern;
            LastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;

            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            if(Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                builder.AppendFront("PushSubpatternTask Subpattern ");
            } else if(Type == PushAndPopSubpatternTaskTypes.Alternative) {
                builder.AppendFront("PushSubpatternTask Alternative ");
            } else { // Type==PushAndPopSubpatternTaskTypes.Iterated
                builder.AppendFront("PushSubpatternTask Iterated ");
            }

            if(Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                builder.AppendFormat("{0} of {1} ", SubpatternElementName, SubpatternName);
            } else {
                builder.AppendFormat("{0}/{1} ", PathPrefix, AlternativeOrIteratedName);
            }

            if(Parallel)
                builder.Append("Parallel ");

            builder.Append("with ");
            for (int i = 0; i < ConnectionName.Length; ++i)
            {
                builder.AppendFormat("{0} <- {1} ",
                    ConnectionName[i], ArgumentExpressions[i]);
            }
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode) {
                if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                    sourceCode.AppendFrontFormat("// Push subpattern matching task for {0}\n", SubpatternElementName);
                } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                    sourceCode.AppendFrontFormat("// Push alternative matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                } else { // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                    sourceCode.AppendFrontFormat("// Push iterated matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                }
            }

            string variableContainingTask;
            string parallelizationThreadId = Parallel ? ", threadId" : "";
            if (Type == PushAndPopSubpatternTaskTypes.Subpattern)
            {
                // create matching task for subpattern
                variableContainingTask = NamesOfEntities.TaskVariable(SubpatternElementName, NegativeIndependentNamePrefix);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(SubpatternName, false, false);
                sourceCode.AppendFrontFormat("{0} {1} = {0}.getNewTask(actionEnv, {2}openTasks{3});\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativeIndependentNamePrefix, parallelizationThreadId);
            }
            else if (Type == PushAndPopSubpatternTaskTypes.Alternative)
            {
                // create matching task for alternative
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, true, false);
                string patternGraphPath = RulePatternClassName + ".Instance.";
                if(RulePatternClassName.Substring(RulePatternClassName.IndexOf('_')+1) == PathPrefixInRulePatternClass.Substring(0, PathPrefixInRulePatternClass.Length-1))
                    patternGraphPath += "patternGraph";
                else
                    patternGraphPath += PathPrefixInRulePatternClass.Substring(0, PathPrefixInRulePatternClass.Length - 1);
                string alternativeCases = patternGraphPath + ".alternatives[(int)" + RulePatternClassName + "."
                    + PathPrefixInRulePatternClass + "AltNums.@" + AlternativeNameInRulePatternClass + "].alternativeCases";
                sourceCode.AppendFrontFormat("{0} {1} = {0}.getNewTask(actionEnv, {2}openTasks, {3}{4});\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativeIndependentNamePrefix, alternativeCases, parallelizationThreadId);
            }
            else // if(Type == PushAndPopSubpatternTaskTypes.Iterated)
            {
                // create matching task for iterated
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, false, true);
                sourceCode.AppendFrontFormat("{0} {1} = {0}.getNewTask(actionEnv, {2}openTasks{3});\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativeIndependentNamePrefix, parallelizationThreadId);
            }
            
            // fill in connections
            for (int i = 0; i < ConnectionName.Length; ++i)
            {
                sourceCode.AppendFrontFormat("{0}.{1} = {2};\n",
                    variableContainingTask, ConnectionName[i], ArgumentExpressions[i]);
            }

            // fill in values needed for patternpath handling
            sourceCode.AppendFrontFormat("{0}.searchPatternpath = {1};\n",
                variableContainingTask, SearchPatternpath);
            sourceCode.AppendFrontFormat("{0}.matchOfNestingPattern = {1};\n", 
                variableContainingTask, MatchOfNestingPattern);
            sourceCode.AppendFrontFormat("{0}.lastMatchAtPreviousNestingLevel = {1};\n",
                variableContainingTask, LastMatchAtPreviousNestingLevel);

            // push matching task to open tasks stack
            sourceCode.AppendFrontFormat("{0}openTasks.Push({1});\n", NegativeIndependentNamePrefix, variableContainingTask);
        }

        public PushAndPopSubpatternTaskTypes Type;
        public string SubpatternName; // only valid if Type==Subpattern
        public string SubpatternElementName; // only valid if Type==Subpattern
        string PathPrefix; // only valid if Type==Alternative|Iterated
        string AlternativeOrIteratedName; // only valid if Type==Alternative|Iterated
        string RulePatternClassName; // only valid if Type==Alternative|Iterated
        string PathPrefixInRulePatternClass; // only valid if Type==Alternative
        string AlternativeNameInRulePatternClass; // only valid if Type==Alternative
        public string[] ConnectionName;
        public string[] ArgumentExpressions;
        public string NegativeIndependentNamePrefix;
        public string SearchPatternpath;
        public string MatchOfNestingPattern;
        public string LastMatchAtPreviousNestingLevel;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing "pop a subpattern tasks from the open tasks stack" operation
    /// </summary>
    class PopSubpatternTask : SearchProgramOperation
    {
        public PopSubpatternTask(string negativeIndependentNamePrefix,
            PushAndPopSubpatternTaskTypes type,
            string subpatternOrAlternativeOrIteratedName, 
            string subpatternElementNameOrPathPrefix,
            bool parallel)
        {
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            Type = type;
            if (type == PushAndPopSubpatternTaskTypes.Subpattern) {
                SubpatternName = subpatternOrAlternativeOrIteratedName;
                SubpatternElementName = subpatternElementNameOrPathPrefix;
            } else if (type == PushAndPopSubpatternTaskTypes.Alternative) {
                AlternativeOrIteratedName = subpatternOrAlternativeOrIteratedName;
                PathPrefix = subpatternElementNameOrPathPrefix;
            } else { // if (type == PushAndPopSubpatternTaskTypes.Iterated)
                AlternativeOrIteratedName = subpatternOrAlternativeOrIteratedName;
                PathPrefix = subpatternElementNameOrPathPrefix;
            }
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                builder.AppendFrontFormat("PopSubpatternTask Subpattern {0} {1}\n",
                    SubpatternElementName, Parallel ? "Parallel" : "");
            } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                builder.AppendFrontFormat("PopSubpatternTask Alternative {0} {1}\n",
                    AlternativeOrIteratedName, Parallel ? "Parallel" : "");
            } else { // Type==PushAndPopSubpatternTaskTypes.Iterated
                builder.AppendFrontFormat("PopSubpatternTask Iterated {0} {1}\n",
                    AlternativeOrIteratedName, Parallel ? "Parallel" : "");
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode) {
                if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                    sourceCode.AppendFrontFormat("// Pop subpattern matching task for {0}\n", SubpatternElementName);
                } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                    sourceCode.AppendFrontFormat("// Pop alternative matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                } else { // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                    sourceCode.AppendFrontFormat("// Pop iterated matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                }
            }

            sourceCode.AppendFrontFormat("{0}openTasks.Pop();\n", NegativeIndependentNamePrefix);

            string variableContainingTask;
            string typeOfVariableContainingTask;
            if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                variableContainingTask = NamesOfEntities.TaskVariable(SubpatternElementName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(SubpatternName, false, false);
            } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, true, false);
            } else { // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, false, true);
            }
            string parallelizationThreadId = Parallel ? ", threadId" : "";
            sourceCode.AppendFrontFormat("{0}.releaseTask({1}{2});\n", 
                typeOfVariableContainingTask, variableContainingTask, parallelizationThreadId);
        }

        public PushAndPopSubpatternTaskTypes Type;
        public string SubpatternName; // only valid if Type==Subpattern
        public string SubpatternElementName; // only valid if Type==Subpattern
        public string PathPrefix; // only valid if Type==Alternative|Iterated
        public string AlternativeOrIteratedName; // only valid if Type==Alternative|Iterated
        public string NegativeIndependentNamePrefix;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing "execute open subpattern matching tasks" operation
    /// </summary>
    class MatchSubpatterns : SearchProgramOperation
    {
        public MatchSubpatterns(string negativeIndependentNamePrefix, bool parallelized)
        {
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            Parallelized = parallelized;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("MatchSubpatterns {0}\n", NegativeIndependentNamePrefix!="" ? "of "+NegativeIndependentNamePrefix : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Match subpatterns {0}\n", 
                    NegativeIndependentNamePrefix!="" ? "of "+NegativeIndependentNamePrefix : "");

            if(Parallelized)
            {
                sourceCode.AppendFrontFormat("{0}openTasks.Peek().myMatch_parallelized({0}matchesList, {1}, isoSpace, threadId);\n",
                    NegativeIndependentNamePrefix,
                    NegativeIndependentNamePrefix == "" ? "maxMatches - foundPartialMatches.Count" : "1");
            }
            else
            {
                sourceCode.AppendFrontFormat("{0}openTasks.Peek().myMatch({0}matchesList, {1}, isoSpace);\n",
                    NegativeIndependentNamePrefix,
                    NegativeIndependentNamePrefix == "" ? "maxMatches - foundPartialMatches.Count" : "1");
            }
        }

        public string NegativeIndependentNamePrefix;
        public bool Parallelized;
    }

    /// <summary>
    /// Class representing "create new matches list for following matches" operation
    /// </summary>
    class NewMatchesListForFollowingMatches : SearchProgramOperation
    {
        public NewMatchesListForFollowingMatches(bool onlyIfMatchWasFound)
        {
            OnlyIfMatchWasFound = onlyIfMatchWasFound;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("NewMatchesListForFollowingMatches {0}\n",
                OnlyIfMatchWasFound ? "if match was found" : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (OnlyIfMatchWasFound)
            {
                sourceCode.AppendFront("if(matchesList.Count>0) {\n");
                sourceCode.Indent();
            }

            sourceCode.AppendFront("if(matchesList==foundPartialMatches) {\n");
            sourceCode.AppendFront("    matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();\n");
            sourceCode.AppendFront("} else {\n");
            sourceCode.AppendFront("    foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {\n");
            sourceCode.AppendFront("        foundPartialMatches.Add(match);\n");
            sourceCode.AppendFront("    }\n");
            sourceCode.AppendFront("    matchesList.Clear();\n");
            sourceCode.AppendFront("}\n");

            if (OnlyIfMatchWasFound)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public bool OnlyIfMatchWasFound;
    }

    /// <summary>
    /// Available types of InitializeSubpatternMatching and the corresponding FinalizeSubpatternMatching operations
    /// </summary>
    enum InitializeFinalizeSubpatternMatchingType
    {
        Normal, // normal subpattern/alternative, whose tasks get removed when pattern gets matched
        Iteration, // iteration whose task stays on the tasks stack
        EndOfIteration // end of iteration, now the task gets removed from the tasks stack
    }

    /// <summary>
    /// Class representing "initialize subpattern matching" operation
    /// </summary>
    class InitializeSubpatternMatching : SearchProgramOperation
    {
        public InitializeSubpatternMatching(InitializeFinalizeSubpatternMatchingType type)
        {
            Type = type;
        }

        public override void Dump(SourceBuilder builder)
        {
            switch (Type)
            {
            case InitializeFinalizeSubpatternMatchingType.Normal:
                builder.AppendFront("InitializeSubpatternMatching Normal\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.Iteration:
                builder.AppendFront("InitializeSubpatternMatching Iteration\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.EndOfIteration:
                builder.AppendFront("InitializeSubpatternMatching EndOfIteration\n");
                break;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type != InitializeFinalizeSubpatternMatchingType.Iteration)
            {
                sourceCode.AppendFront("openTasks.Pop();\n");
            }
            if (Type != InitializeFinalizeSubpatternMatchingType.EndOfIteration)
            {
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;\n");
                sourceCode.AppendFront("if(matchesList.Count!=0) throw new ApplicationException(); //debug assert\n");

                if (Type == InitializeFinalizeSubpatternMatchingType.Iteration)
                {
                    sourceCode.AppendFront("// if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object\n");
                    sourceCode.AppendFront("if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;\n");
                }
            }
        }

        public InitializeFinalizeSubpatternMatchingType Type;
    }

    /// <summary>
    /// Class representing "finalize subpattern matching" operation
    /// </summary>
    class FinalizeSubpatternMatching : SearchProgramOperation
    {
        public FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType type)
        {
            Type = type;
        }

        public override void Dump(SourceBuilder builder)
        {
            switch (Type)
            {
            case InitializeFinalizeSubpatternMatchingType.Normal:
                builder.AppendFront("FinalizeSubpatternMatching Normal\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.Iteration:
                builder.AppendFront("FinalizeSubpatternMatching Iteration\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.EndOfIteration:
                builder.AppendFront("FinalizeSubpatternMatching EndOfIteration\n");
                break;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type != InitializeFinalizeSubpatternMatchingType.Iteration)
            {
                sourceCode.AppendFrontFormat("openTasks.Push(this);\n");
            }
        }

        InitializeFinalizeSubpatternMatchingType Type;
    }

    /// <summary>
    /// Class representing "initialize negative/independent matching" operation
    /// it opens an isomorphy space at the next isoSpace number, finalizeXXX will close it
    /// </summary>
    class InitializeNegativeIndependentMatching : SearchProgramOperation
    {
        public InitializeNegativeIndependentMatching(
            bool containsSubpatterns,
            string negativeIndependentNamePrefix,
            bool neverAboveMaxIsoSpace,
            bool parallel)
        {
            SetupSubpatternMatching = containsSubpatterns;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("InitializeNegativeIndependentMatching "); 
            if(SetupSubpatternMatching)
                builder.AppendFront("SetupSubpatternMatching ");
            if(Parallel)
                builder.AppendFront("Parallel ");
            builder.Append("\n"); 
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("++isoSpace;\n");
            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace >= (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE && "+
                        "isoSpace - (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE + 1 > graph.perThreadInIsoSpaceMatchedElements[threadId].Count) {\n");
                    sourceCode.Indent();
                    sourceCode.AppendFront("graph.AllocateFurtherIsomorphySpaceNestingLevelForParallelizedMatching(threadId);\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace >= (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE && "
                        + "isoSpace - (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE + 1 > graph.inIsoSpaceMatchedElements.Count) {\n");
                    sourceCode.Indent();
                    sourceCode.AppendFront("graph.inIsoSpaceMatchedElements.Add(new Dictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement>());\n");
                    sourceCode.AppendFront("graph.inIsoSpaceMatchedElementsGlobal.Add(new Dictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement>());\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }

            if(SetupSubpatternMatching)
            {
                sourceCode.AppendFrontFormat("Stack<GRGEN_LGSP.LGSPSubpatternAction> {0}openTasks ="
                    + " new Stack<GRGEN_LGSP.LGSPSubpatternAction>();\n", NegativeIndependentNamePrefix);
                sourceCode.AppendFrontFormat("List<Stack<GRGEN_LIBGR.IMatch>> {0}foundPartialMatches ="
                    + " new List<Stack<GRGEN_LIBGR.IMatch>>();\n", NegativeIndependentNamePrefix);
                sourceCode.AppendFrontFormat("List<Stack<GRGEN_LIBGR.IMatch>> {0}matchesList ="
                    + " {0}foundPartialMatches;\n", NegativeIndependentNamePrefix);
            }
        }

        public bool SetupSubpatternMatching;
        public string NegativeIndependentNamePrefix;
        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing "finalize negative/independent matching" operation
    /// it closes an isomorphy space opened by initializeXXX, returning to the previous isoSpace
    /// </summary>
    class FinalizeNegativeIndependentMatching : SearchProgramOperation
    {
        public FinalizeNegativeIndependentMatching(bool neverAboveMaxIsoSpace, bool parallel)
        {
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("FinalizeNegativeIndependentMatching ");
            if(Parallel)
                builder.AppendFront("Parallel ");
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // TODO: why is the clear needed? is it needed at all? the set being empty must be ensured at this point.
            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace >= (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                    sourceCode.AppendFront("graph.perThreadInIsoSpaceMatchedElements[threadId][isoSpace - "
                        + "(int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].Clear();\n");
                    sourceCode.AppendFront("graph.perThreadInIsoSpaceMatchedElementsGlobal[threadId][isoSpace - "
                        + "(int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE].Clear();\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.AppendFront("if(isoSpace >= (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE) {\n");
                    sourceCode.Indent();
                    sourceCode.AppendFront("graph.inIsoSpaceMatchedElements[isoSpace - "
                        + "(int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE].Clear();\n");
                    sourceCode.AppendFront("graph.inIsoSpaceMatchedElementsGlobal[isoSpace - "
                        + "(int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE].Clear();\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
            sourceCode.AppendFront("--isoSpace;\n");
        }

        public bool NeverAboveMaxIsoSpace;
        public bool Parallel;
    }

    /// <summary>
    /// Class representing "push match for patternpath" operation
    /// push match to the match objects stack used for patternpath checking
    /// </summary>
    class PushMatchForPatternpath : SearchProgramOperation
    {
        public PushMatchForPatternpath(
            string rulePatternClassName,
            string patternGraphName,
            string matchOfNestingPattern)
        {
            RulePatternClassName = rulePatternClassName;
            PatternGraphName = patternGraphName;
            MatchOfNestingPattern = matchOfNestingPattern;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("PushMatchOfNestingPattern of {0} nestingPattern in:{1}\n",
                PatternGraphName, MatchOfNestingPattern);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("// build match of {0} for patternpath checks\n", 
                PatternGraphName);
            sourceCode.AppendFrontFormat("if({0}==null) {0} = new {1}.{2}();\n", 
                NamesOfEntities.PatternpathMatch(PatternGraphName),
                RulePatternClassName,
                NamesOfEntities.MatchClassName(PatternGraphName));
            sourceCode.AppendFrontFormat("{0}._matchOfEnclosingPattern = {1};\n",
                NamesOfEntities.PatternpathMatch(PatternGraphName), MatchOfNestingPattern);
        }

        string RulePatternClassName;
        string PatternGraphName;
        string MatchOfNestingPattern;
    }

    /// <summary>
    /// Class representing "assign variable from expression" operation,
    /// </summary>
    class AssignVariableFromExpression : SearchProgramOperation
    {
        public AssignVariableFromExpression(
            string variableName,
            string variableType,
            string sourceExpression)
        {
            VariableName = variableName;
            VariableType = variableType;
            SourceExpression = sourceExpression;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("AssignVariableFromExpression {0} := {1}\n",
                VariableName, SourceExpression);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Variable {0} assigned from expression {1} \n",
                    VariableName, SourceExpression);
            
            // emit declaration of variable initialized with expression
            sourceCode.AppendFrontFormat("{0} {1} = ({0}){2};\n",
                VariableType, NamesOfEntities.Variable(VariableName), SourceExpression);
        }

        public string VariableName;
        public string VariableType;
        public string SourceExpression;
    }
}
