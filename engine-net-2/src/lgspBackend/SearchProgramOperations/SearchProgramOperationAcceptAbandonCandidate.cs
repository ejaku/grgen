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

        public readonly string PatternElementName;
        public readonly string NegativeIndependentNamePrefix; // "" if top-level
        public readonly bool IsNode; // node|edge
        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
        public readonly bool LockForAllThreads;
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

        public readonly string PatternElementName;
        public readonly string NegativeIndependentNamePrefix; // "" if top-level
        public readonly bool IsNode; // node|edge
        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
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

        public readonly string PatternElementName;
        public readonly string NegativeIndependentNamePrefix; // "" if top-level
        public readonly bool IsNode; // node|edge
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

        public readonly string PatternElementName;
        public readonly string NegativeIndependentNamePrefix; // "" if top-level
        public readonly bool IsNode; // node|edge
        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
        public readonly bool LockForAllThreads;
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

        public readonly string PatternElementName;
        public readonly string NegativeIndependentNamePrefix; // "" if positive
        public readonly bool IsNode; // node|edge
        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
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

        public readonly string PatternElementName;
        public readonly string NegativeIndependentNamePrefix; // "" if positive
        public readonly bool IsNode; // node|edge
    }
}
