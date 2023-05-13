/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

namespace de.unika.ipd.grGen.lgsp
{
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
            bool emitProfiling, string packagePrefixedActionName, bool emitFirstLoopProfiling)
        {
            Type = type;
            ListHeadAdjustment = listHeadAdjustment;
            InParallelizedBody = inParallelizedBody;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public CheckContinueMatchingMaximumMatchesReached(
            CheckMaximumMatchesType type, bool listHeadAdjustment, bool inParallelizedBody)
        {
            Type = type;
            ListHeadAdjustment = listHeadAdjustment;
            InParallelizedBody = inParallelizedBody;
            EmitProfiling = false;
            PackagePrefixedActionName = null;
            EmitFirstLoopProfiling = false;
        }


        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            if(Type == CheckMaximumMatchesType.Action)
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached at Action level ");
            else if(Type == CheckMaximumMatchesType.Subpattern)
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached at Subpattern level ");
            else if(Type == CheckMaximumMatchesType.Iterated)
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached if Iterated ");
            if(ListHeadAdjustment)
                builder.Append("ListHeadAdjustment ");
            if(InParallelizedBody)
                builder.Append("InParallelizedBody ");
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
                sourceCode.AppendFront("// if enough matches were found, we leave\n");

            if(Type == CheckMaximumMatchesType.Action)
            {
                if(InParallelizedBody)
                    sourceCode.AppendFront("if(maxMatches > 0 && parallelTaskMatches[threadId].Count >= maxMatches)\n");
                else
                    sourceCode.AppendFront("if(maxMatches > 0 && matches.Count >= maxMatches)\n");
            }
            else if(Type == CheckMaximumMatchesType.Subpattern)
                sourceCode.AppendFront("if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)\n");
            else if(Type == CheckMaximumMatchesType.Iterated)
                sourceCode.AppendFront("if(true) // as soon as there's a match, it's enough for iterated\n");
    
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(Type == CheckMaximumMatchesType.Action && InParallelizedBody)
                sourceCode.AppendFront("maxMatchesFound = true;\n");

            if(EmitProfiling)
            {
                if(InParallelizedBody)
                {
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId]);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsSingle.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].loopStepsMultiple.Add(actionEnv.PerformanceInfo.LoopStepsPerThread[threadId]);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                }
                else
                {
                    sourceCode.AppendFrontFormat("++actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].callsTotal;\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].loopStepsTotal += loopSteps;\n", PackagePrefixedActionName);

                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsTotal += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin;\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsTotal += loopSteps;\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBegin);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsSingle.Add(loopSteps);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].loopStepsMultiple.Add(loopSteps);\n", PackagePrefixedActionName);
                    
                    if(EmitFirstLoopProfiling)
                    {
                        sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                        sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                    }
                }
            }

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly CheckMaximumMatchesType Type;
        public readonly bool ListHeadAdjustment;
        public readonly bool InParallelizedBody;
        public readonly bool EmitProfiling;
        public readonly string PackagePrefixedActionName;
        public readonly bool EmitFirstLoopProfiling;
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
            if(CheckFailedOperations != null)
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

        public readonly bool IsIterationBreaking;
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
            if(CheckFailedOperations != null)
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
        public readonly CheckPartialMatchByIndependent CheckIndependent;
        public readonly bool IsIterationBreaking;
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
            if(CheckFailedOperations != null)
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

        public readonly bool IsIterationBreaking;
    }
}
