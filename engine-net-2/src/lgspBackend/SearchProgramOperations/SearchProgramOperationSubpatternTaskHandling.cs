/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
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
            if(Type == PushAndPopSubpatternTaskTypes.Subpattern)
                builder.AppendFront("PushSubpatternTask Subpattern ");
            else if(Type == PushAndPopSubpatternTaskTypes.Alternative)
                builder.AppendFront("PushSubpatternTask Alternative ");
            else // Type==PushAndPopSubpatternTaskTypes.Iterated
                builder.AppendFront("PushSubpatternTask Iterated ");

            if(Type == PushAndPopSubpatternTaskTypes.Subpattern)
                builder.AppendFormat("{0} of {1} ", SubpatternElementName, SubpatternName);
            else
                builder.AppendFormat("{0}/{1} ", PathPrefix, AlternativeOrIteratedName);

            if(Parallel)
                builder.Append("Parallel ");

            builder.Append("with ");
            for(int i = 0; i < ConnectionName.Length; ++i)
            {
                builder.AppendFormat("{0} <- {1} ",
                    ConnectionName[i], ArgumentExpressions[i]);
            }
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode) {
                if(Type == PushAndPopSubpatternTaskTypes.Subpattern)
                    sourceCode.AppendFrontFormat("// Push subpattern matching task for {0}\n", SubpatternElementName);
                else if(Type == PushAndPopSubpatternTaskTypes.Alternative)
                    sourceCode.AppendFrontFormat("// Push alternative matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                else // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                    sourceCode.AppendFrontFormat("// Push iterated matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
            }

            string variableContainingTask;
            string parallelizationThreadId = Parallel ? ", threadId" : "";
            if(Type == PushAndPopSubpatternTaskTypes.Subpattern)
            {
                // create matching task for subpattern
                variableContainingTask = NamesOfEntities.TaskVariable(SubpatternElementName, NegativeIndependentNamePrefix);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(SubpatternName, false, false);
                sourceCode.AppendFrontFormat("{0} {1} = {0}.getNewTask(actionEnv, {2}openTasks{3});\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativeIndependentNamePrefix, parallelizationThreadId);
            }
            else if(Type == PushAndPopSubpatternTaskTypes.Alternative)
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
            for(int i = 0; i < ConnectionName.Length; ++i)
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

        public readonly PushAndPopSubpatternTaskTypes Type;
        public readonly string SubpatternName; // only valid if Type==Subpattern
        public readonly string SubpatternElementName; // only valid if Type==Subpattern
        readonly string PathPrefix; // only valid if Type==Alternative|Iterated
        readonly string AlternativeOrIteratedName; // only valid if Type==Alternative|Iterated
        readonly string RulePatternClassName; // only valid if Type==Alternative|Iterated
        readonly string PathPrefixInRulePatternClass; // only valid if Type==Alternative
        readonly string AlternativeNameInRulePatternClass; // only valid if Type==Alternative
        public readonly string[] ConnectionName;
        public readonly string[] ArgumentExpressions;
        public readonly string NegativeIndependentNamePrefix;
        public readonly string SearchPatternpath;
        public readonly string MatchOfNestingPattern;
        public readonly string LastMatchAtPreviousNestingLevel;
        public readonly bool Parallel;
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
            if(type == PushAndPopSubpatternTaskTypes.Subpattern)
            {
                SubpatternName = subpatternOrAlternativeOrIteratedName;
                SubpatternElementName = subpatternElementNameOrPathPrefix;
            }
            else if(type == PushAndPopSubpatternTaskTypes.Alternative)
            {
                AlternativeOrIteratedName = subpatternOrAlternativeOrIteratedName;
                PathPrefix = subpatternElementNameOrPathPrefix;
            }
            else // if(type == PushAndPopSubpatternTaskTypes.Iterated)
            {
                AlternativeOrIteratedName = subpatternOrAlternativeOrIteratedName;
                PathPrefix = subpatternElementNameOrPathPrefix;
            }
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            if(Type == PushAndPopSubpatternTaskTypes.Subpattern)
            {
                builder.AppendFrontFormat("PopSubpatternTask Subpattern {0} {1}\n",
                    SubpatternElementName, Parallel ? "Parallel" : "");
            }
            else if(Type == PushAndPopSubpatternTaskTypes.Alternative)
            {
                builder.AppendFrontFormat("PopSubpatternTask Alternative {0} {1}\n",
                    AlternativeOrIteratedName, Parallel ? "Parallel" : "");
            }
            else // Type==PushAndPopSubpatternTaskTypes.Iterated
            {
                builder.AppendFrontFormat("PopSubpatternTask Iterated {0} {1}\n",
                    AlternativeOrIteratedName, Parallel ? "Parallel" : "");
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode) {
                if(Type == PushAndPopSubpatternTaskTypes.Subpattern)
                    sourceCode.AppendFrontFormat("// Pop subpattern matching task for {0}\n", SubpatternElementName);
                else if(Type == PushAndPopSubpatternTaskTypes.Alternative)
                    sourceCode.AppendFrontFormat("// Pop alternative matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                else // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                    sourceCode.AppendFrontFormat("// Pop iterated matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
            }

            sourceCode.AppendFrontFormat("{0}openTasks.Pop();\n", NegativeIndependentNamePrefix);

            string variableContainingTask;
            string typeOfVariableContainingTask;
            if(Type == PushAndPopSubpatternTaskTypes.Subpattern)
            {
                variableContainingTask = NamesOfEntities.TaskVariable(SubpatternElementName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(SubpatternName, false, false);
            }
            else if(Type == PushAndPopSubpatternTaskTypes.Alternative)
            {
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, true, false);
            }
            else // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
            {
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, false, true);
            }
            string parallelizationThreadId = Parallel ? ", threadId" : "";
            sourceCode.AppendFrontFormat("{0}.releaseTask({1}{2});\n", 
                typeOfVariableContainingTask, variableContainingTask, parallelizationThreadId);
        }

        public readonly PushAndPopSubpatternTaskTypes Type;
        public readonly string SubpatternName; // only valid if Type==Subpattern
        public readonly string SubpatternElementName; // only valid if Type==Subpattern
        public readonly string PathPrefix; // only valid if Type==Alternative|Iterated
        public readonly string AlternativeOrIteratedName; // only valid if Type==Alternative|Iterated
        public readonly string NegativeIndependentNamePrefix;
        public readonly bool Parallel;
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
            if(sourceCode.CommentSourceCode)
            {
                sourceCode.AppendFrontFormat("// Match subpatterns {0}\n",
                    NegativeIndependentNamePrefix != "" ? "of " + NegativeIndependentNamePrefix : "");
            }

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

        public readonly string NegativeIndependentNamePrefix;
        public readonly bool Parallelized;
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
            switch(Type)
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
            if(Type != InitializeFinalizeSubpatternMatchingType.Iteration)
                sourceCode.AppendFront("openTasks.Pop();\n");
            if(Type != InitializeFinalizeSubpatternMatchingType.EndOfIteration)
            {
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;\n");
                sourceCode.AppendFront("if(matchesList.Count!=0) throw new ApplicationException(); //debug assert\n");

                if(Type == InitializeFinalizeSubpatternMatchingType.Iteration)
                {
                    sourceCode.AppendFront("// if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object\n");
                    sourceCode.AppendFront("if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;\n");
                }
            }
        }

        public readonly InitializeFinalizeSubpatternMatchingType Type;
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
            switch(Type)
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
            if(Type != InitializeFinalizeSubpatternMatchingType.Iteration)
                sourceCode.AppendFrontFormat("openTasks.Push(this);\n");
        }

        readonly InitializeFinalizeSubpatternMatchingType Type;
    }
}
