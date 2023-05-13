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
    /// Class representing "match the pattern of the alternative case" operation
    /// </summary>
    class AlternativeCaseMatching : SearchProgramOperation
    {
        public AlternativeCaseMatching(string pathPrefix, string caseName, 
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
            builder.AppendFrontFormat("AlternativeCaseMatching {0}{1}\n", PathPrefix, CaseName);

            // then nested content
            if(OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Alternative case {0}{1} \n", PathPrefix, CaseName);

            sourceCode.AppendFront("do {\n");
            sourceCode.Indent();
            string whichCase = RulePatternClassName + "." + PathPrefix + "CaseNums.@" + CaseName;
            sourceCode.AppendFrontFormat("patternGraph = patternGraphs[(int){0}];\n", whichCase);

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PathPrefix + CaseName),
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
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PathPrefix + CaseName),
                    NamesOfEntities.FoundMatchesForFilteringVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

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

        public readonly string PathPrefix;
        public readonly string CaseName;
        public readonly string RulePatternClassName;
        public readonly bool WasIndependentInlined;

        public SearchProgramList OperationsList;
    }

    /// <summary>
    /// Class representing nesting operation which executes the body once;
    /// needed for iterated, to prevent a return out of the matcher program
    /// circumventing the maxMatchesIterReached code which must get called if matching fails
    /// </summary>
    class IteratedMatchingDummyLoop : SearchProgramOperation
    {
        public IteratedMatchingDummyLoop()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("IteratedMatchingDummyLoopPreventingReturn \n");

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
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// dummy loop for iterated matching return prevention\n");

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

        public readonly bool SetupSubpatternMatching;
        public readonly string NegativeIndependentNamePrefix;
        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
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

        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
    }
}
