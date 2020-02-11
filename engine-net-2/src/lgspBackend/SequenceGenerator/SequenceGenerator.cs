/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence generator contains code to generate sequences, the sequence trees to be more precise,
    /// it is used by the lgsp sequence generator driver, emitting sequence heads and surrounding code.
    /// </summary>
    public class SequenceGenerator
    {
        readonly IGraphModel model;

        readonly SequenceCheckingEnvironmentCompiled env;

        readonly SequenceComputationGenerator compGen;

        readonly SequenceExpressionGenerator exprGen;

        readonly SequenceGeneratorHelper helper;

        readonly bool fireDebugEvents;
        readonly bool emitProfiling;


        public SequenceGenerator(IGraphModel model, SequenceCheckingEnvironmentCompiled env,
            SequenceComputationGenerator compGen, SequenceExpressionGenerator exprGen, SequenceGeneratorHelper helper,
            bool fireDebugEvents, bool emitProfiling)
        {
            this.model = model;

            this.env = env;

            this.compGen = compGen;

            this.exprGen = exprGen;

            this.helper = helper;

            this.fireDebugEvents = fireDebugEvents;
            this.emitProfiling = emitProfiling;
        }

        public void EmitSequence(Sequence seq, SourceBuilder source)
        {
            switch(seq.SequenceType)
            {
            case SequenceType.RuleCall:
            case SequenceType.RuleAllCall:
            case SequenceType.RuleCountAllCall:
                new SequenceRuleOrRuleAllCallGenerator((SequenceRuleCall)seq, helper).Emit(source, this, compGen, fireDebugEvents);
                break;

            case SequenceType.SequenceCall:
                EmitSequenceCall((SequenceSequenceCall)seq, source);
                break;

            case SequenceType.Not:
                EmitSequenceNot((SequenceNot)seq, source);
                break;

            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
            case SequenceType.IfThen:
                EmitSequenceBinaryLazy((SequenceBinary)seq, source);
                break;

            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.StrictAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
                EmitSequenceBinary((SequenceBinary)seq, source);
                break;

            case SequenceType.IfThenElse:
                EmitSequenceIfThenElse((SequenceIfThenElse)seq, source);
                break;

            case SequenceType.ForContainer:
                EmitSequenceForContainer((SequenceForContainer)seq, source);
                break;

            case SequenceType.ForIntegerRange:
                EmitSequenceForIntegerRange((SequenceForIntegerRange)seq, source);
                break;

            case SequenceType.ForIndexAccessEquality:
                EmitSequenceForIndexAccessEquality((SequenceForIndexAccessEquality)seq, source);
                break;

            case SequenceType.ForIndexAccessOrdering:
                EmitSequenceForIndexAccessOrdering((SequenceForIndexAccessOrdering)seq, source);
                break;

            case SequenceType.ForAdjacentNodes:
            case SequenceType.ForAdjacentNodesViaIncoming:
            case SequenceType.ForAdjacentNodesViaOutgoing:
            case SequenceType.ForIncidentEdges:
            case SequenceType.ForIncomingEdges:
            case SequenceType.ForOutgoingEdges:
            case SequenceType.ForReachableNodes:
            case SequenceType.ForReachableNodesViaIncoming:
            case SequenceType.ForReachableNodesViaOutgoing:
            case SequenceType.ForReachableEdges:
            case SequenceType.ForReachableEdgesViaIncoming:
            case SequenceType.ForReachableEdgesViaOutgoing:
                EmitSequenceForFunction((SequenceForFunction)seq, source);
                break;

            case SequenceType.ForBoundedReachableNodes:
            case SequenceType.ForBoundedReachableNodesViaIncoming:
            case SequenceType.ForBoundedReachableNodesViaOutgoing:
            case SequenceType.ForBoundedReachableEdges:
            case SequenceType.ForBoundedReachableEdgesViaIncoming:
            case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                EmitSequenceForBoundedReachable((SequenceForFunction)seq, source);
                break;

            case SequenceType.ForNodes:
            case SequenceType.ForEdges:
                EmitSequenceForNodesEdges((SequenceForFunction)seq, source);
                break;

            case SequenceType.ForMatch:
                new SequenceForMatchGenerator((SequenceForMatch)seq, helper).Emit(source, this, compGen, fireDebugEvents);
                break;

            case SequenceType.IterationMin:
                EmitSequenceIterationMin((SequenceIterationMin)seq, source);
                break;

            case SequenceType.IterationMinMax:
                EmitSequenceIterationMinMax((SequenceIterationMinMax)seq, source);
                break;

            case SequenceType.DeclareVariable:
                EmitSequenceDeclareVariable((SequenceDeclareVariable)seq, source);
                break;

            case SequenceType.AssignConstToVar:
                EmitSequenceAssignConstToVar((SequenceAssignConstToVar)seq, source);
                break;

            case SequenceType.AssignContainerConstructorToVar:
                EmitSequenceAssignContainerConstructorToVar((SequenceAssignContainerConstructorToVar)seq, source);
                break;

            case SequenceType.AssignVarToVar:
                EmitSequenceAssignVarToVar((SequenceAssignVarToVar)seq, source);
                break;

            case SequenceType.AssignSequenceResultToVar:
                EmitSequenceAssignSequenceResultToVar((SequenceAssignSequenceResultToVar)seq, source);
                break;

            case SequenceType.OrAssignSequenceResultToVar:
                EmitSequenceOrAssignSequenceResultToVar((SequenceOrAssignSequenceResultToVar)seq, source);
                break;

            case SequenceType.AndAssignSequenceResultToVar:
                EmitSequenceAndAssignSequenceResultToVar((SequenceAndAssignSequenceResultToVar)seq, source);
                break;

            case SequenceType.AssignUserInputToVar:
                throw new Exception("Internal Error: the AssignUserInputToVar is interpreted only (no Debugger available at lgsp level)");

            case SequenceType.AssignRandomIntToVar:
                EmitSequenceAssignRandomIntToVar((SequenceAssignRandomIntToVar)seq, source);
                break;

            case SequenceType.AssignRandomDoubleToVar:
                EmitSequenceAssignRandomDoubleToVar((SequenceAssignRandomDoubleToVar)seq, source);
                break;

            case SequenceType.LazyOrAll:
                EmitSequenceAll((SequenceLazyOrAll)seq, true, true, source);
                break;

            case SequenceType.LazyAndAll:
                EmitSequenceAll((SequenceLazyAndAll)seq, false, true, source);
                break;

            case SequenceType.StrictOrAll:
                EmitSequenceAll((SequenceStrictOrAll)seq, true, false, source);
                break;

            case SequenceType.StrictAndAll:
                EmitSequenceAll((SequenceStrictAndAll)seq, false, false, source);
                break;

            case SequenceType.WeightedOne:
                EmitSequenceWeighted((SequenceWeightedOne)seq, source);
                break;

            case SequenceType.SomeFromSet:
                EmitSequenceSome((SequenceSomeFromSet)seq, source);
                break;

            case SequenceType.Transaction:
                EmitSequenceTransaction((SequenceTransaction)seq, source);
                break;

            case SequenceType.Backtrack:
                new SequenceBacktrackGenerator((SequenceBacktrack)seq, helper).Emit(source, this, compGen, fireDebugEvents);
                break;

            case SequenceType.Pause:
                EmitSequencePause((SequencePause)seq, source);
                break;

            case SequenceType.ExecuteInSubgraph:
                EmitSequenceExecuteInSubgraph((SequenceExecuteInSubgraph)seq, source);
                break;

            case SequenceType.BooleanComputation:
                EmitSequenceBooleanComputation((SequenceBooleanComputation)seq, source);
                break;

            default:
                throw new Exception("Unknown sequence type: " + seq.SequenceType);
            }
        }

        private void EmitSequenceCall(SequenceSequenceCall seqSeq, SourceBuilder source)
        {
            SequenceInvocation sequenceInvocation = seqSeq.SequenceInvocation;
            SequenceExpression[] ArgumentExpressions = seqSeq.ArgumentExpressions;
            SequenceVariable[] ReturnVars = seqSeq.ReturnVars;
            String parameterDeclarations = null;
            String parameters = null;
            if(sequenceInvocation.Subgraph != null)
                parameters = helper.BuildParametersInDeclarations(sequenceInvocation, ArgumentExpressions, out parameterDeclarations);
            else
                parameters = helper.BuildParameters(sequenceInvocation, ArgumentExpressions);
            String outParameterDeclarations;
            String outArguments;
            String outAssignments;
            helper.BuildOutParameters(sequenceInvocation, ReturnVars, out outParameterDeclarations, out outArguments, out outAssignments);

            if(sequenceInvocation.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + helper.GetVar(sequenceInvocation.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            if(outParameterDeclarations.Length != 0)
                source.AppendFront(outParameterDeclarations + "\n");
            source.AppendFront("if(" + TypesHelper.GetPackagePrefixDot(sequenceInvocation.Package) + "Sequence_" + sequenceInvocation.Name + ".ApplyXGRS_" + sequenceInvocation.Name
                                + "(procEnv" + parameters + outArguments + ")) {\n");
            source.Indent();
            if(outAssignments.Length != 0)
                source.AppendFront(outAssignments + "\n");
            source.AppendFront(compGen.SetResultVar(seqSeq, "true"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqSeq, "false"));
            source.Unindent();
            source.AppendFront("}\n");

            if(sequenceInvocation.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

        private void EmitSequenceNot(SequenceNot seqNot, SourceBuilder source)
        {
            EmitSequence(seqNot.Seq, source);
            source.AppendFront(compGen.SetResultVar(seqNot, "!" + compGen.GetResultVar(seqNot.Seq)));
        }

        private void EmitSequenceBinaryLazy(SequenceBinary seqBin, SourceBuilder source)
        {
            if(seqBin.Random)
            {
                Debug.Assert(seqBin.SequenceType != SequenceType.IfThen);

                source.AppendFront("if(GRGEN_LIBGR.Sequence.randomGenerator.Next(2) == 1)\n");
                source.AppendFront("{\n");
                source.Indent();
                EmitLazyOp(seqBin, source, true);
                source.Unindent();
                source.AppendFront("}\n");
                source.AppendFront("else\n");
                source.AppendFront("{\n");
                source.Indent();
                EmitLazyOp(seqBin, source, false);
                source.Unindent();
                source.AppendFront("}\n");
            }
            else
                EmitLazyOp(seqBin, source, false);
        }

        private void EmitLazyOp(SequenceBinary seq, SourceBuilder source, bool reversed)
        {
            Sequence seqLeft;
            Sequence seqRight;
            if(reversed)
            {
                Debug.Assert(seq.SequenceType != SequenceType.IfThen);
                seqLeft = seq.Right;
                seqRight = seq.Left;
            }
            else
            {
                seqLeft = seq.Left;
                seqRight = seq.Right;
            }

            EmitSequence(seqLeft, source);

            if(seq.SequenceType == SequenceType.LazyOr)
            {
                source.AppendFront("if(" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "true"));
                source.Unindent();
            }
            else if(seq.SequenceType == SequenceType.LazyAnd)
            {
                source.AppendFront("if(!" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "false"));
                source.Unindent();
            }
            else
            { //seq.SequenceType==SequenceType.IfThen -- lazy implication
                source.AppendFront("if(!" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "true"));
                source.Unindent();
            }

            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqRight, source);
            source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqRight)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceBinary(SequenceBinary seqBin, SourceBuilder source)
        {
            if(seqBin.Random)
            {
                source.AppendFront("if(GRGEN_LIBGR.Sequence.randomGenerator.Next(2) == 1)\n");
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqBin.Right, source);
                EmitSequence(seqBin.Left, source);
                source.Unindent();
                source.AppendFront("}\n");
                source.AppendFront("else\n");
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqBin.Left, source);
                EmitSequence(seqBin.Right, source);
                source.Unindent();
                source.AppendFront("}\n");
            }
            else
            {
                EmitSequence(seqBin.Left, source);
                EmitSequence(seqBin.Right, source);
            }

            if(seqBin.SequenceType == SequenceType.ThenLeft)
            {
                source.AppendFront(compGen.SetResultVar(seqBin, compGen.GetResultVar(seqBin.Left)));
                return;
            }
            else if(seqBin.SequenceType == SequenceType.ThenRight)
            {
                source.AppendFront(compGen.SetResultVar(seqBin, compGen.GetResultVar(seqBin.Right)));
                return;
            }

            String op;
            switch(seqBin.SequenceType)
            {
            case SequenceType.StrictAnd:
                op = "&"; break;
            case SequenceType.StrictOr:
                op = "|"; break;
            case SequenceType.Xor:
                op = "^"; break;
            default:
                throw new Exception("Internal error in EmitSequence: Should not have reached this!");
            }
            source.AppendFront(compGen.SetResultVar(seqBin, compGen.GetResultVar(seqBin.Left) + " " + op + " " + compGen.GetResultVar(seqBin.Right)));
        }

        private void EmitSequenceIfThenElse(SequenceIfThenElse seqIf, SourceBuilder source)
        {
            EmitSequence(seqIf.Condition, source);

            source.AppendFront("if(" + compGen.GetResultVar(seqIf.Condition) + ")");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqIf.TrueCase, source);
            source.AppendFront(compGen.SetResultVar(seqIf, compGen.GetResultVar(seqIf.TrueCase)));

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqIf.FalseCase, source);
            source.AppendFront(compGen.SetResultVar(seqIf, compGen.GetResultVar(seqIf.FalseCase)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForContainer(SequenceForContainer seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            if(seqFor.Container.Type == "")
            {
                // type not statically known? -> might be Dictionary or List or Deque dynamically, must decide at runtime
                source.AppendFront("if(" + helper.GetVar(seqFor.Container) + " is IList) {\n");
                source.Indent();

                source.AppendFront("IList entry_" + seqFor.Id + " = (IList) " + helper.GetVar(seqFor.Container) + ";\n");
                source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                source.AppendFront("{\n");
                source.Indent();
                if(seqFor.VarDst != null)
                {
                    source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                    source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                }
                else
                    source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));

                EmitSequence(seqFor.Seq, source);
                source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                source.Unindent();
                source.AppendFront("}\n");

                source.Unindent();
                source.AppendFront("} else if(" + helper.GetVar(seqFor.Container) + " is GRGEN_LIBGR.IDeque) {\n");
                source.Indent();

                source.AppendFront("GRGEN_LIBGR.IDeque entry_" + seqFor.Id + " = (GRGEN_LIBGR.IDeque) " + helper.GetVar(seqFor.Container) + ";\n");
                source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                source.AppendFront("{\n");
                source.Indent();
                if(seqFor.VarDst != null)
                {
                    source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                    source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                }
                else
                    source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));

                EmitSequence(seqFor.Seq, source);
                source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                source.Unindent();
                source.AppendFront("}\n");

                source.Unindent();
                source.AppendFront("} else {\n");
                source.Indent();

                source.AppendFront("foreach(DictionaryEntry entry_" + seqFor.Id + " in (IDictionary)" + helper.GetVar(seqFor.Container) + ")\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                if(seqFor.VarDst != null)
                    source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));

                EmitSequence(seqFor.Seq, source);
                source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                source.Unindent();
                source.AppendFront("}\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
            else if(seqFor.Container.Type.StartsWith("array"))
            {
                String arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                source.AppendFrontFormat("List<{0}> entry_{1} = (List<{0}>) " + helper.GetVar(seqFor.Container) + ";\n", arrayValueType, seqFor.Id);
                source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                source.AppendFront("{\n");
                source.Indent();

                if(seqFor.VarDst != null)
                {
                    source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                    source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                }
                else
                    source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));

                EmitSequence(seqFor.Seq, source);

                source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                source.Unindent();
                source.AppendFront("}\n");
            }
            else if(seqFor.Container.Type.StartsWith("deque"))
            {
                String dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                source.AppendFrontFormat("GRGEN_LIBGR.Deque<{0}> entry_{1} = (GRGEN_LIBGR.Deque<{0}>) " + helper.GetVar(seqFor.Container) + ";\n", dequeValueType, seqFor.Id);
                source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                source.AppendFront("{\n");
                source.Indent();

                if(seqFor.VarDst != null)
                {
                    source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                    source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                }
                else
                    source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));

                EmitSequence(seqFor.Seq, source);

                source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                source.Unindent();
                source.AppendFront("}\n");
            }
            else
            {
                String srcTypeXgrs = TypesHelper.ExtractSrc(seqFor.Container.Type);
                String srcType = TypesHelper.XgrsTypeToCSharpType(srcTypeXgrs, model);
                String dstTypeXgrs = TypesHelper.ExtractDst(seqFor.Container.Type);
                String dstType = TypesHelper.XgrsTypeToCSharpType(dstTypeXgrs, model);
                source.AppendFront("foreach(KeyValuePair<" + srcType + "," + dstType + "> entry_" + seqFor.Id + " in " + helper.GetVar(seqFor.Container) + ")\n");
                source.AppendFront("{\n");
                source.Indent();

                if(dstTypeXgrs == "SetValueType")
                    source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                else
                    source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));

                if(seqFor.VarDst != null)
                    source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));

                EmitSequence(seqFor.Seq, source);

                source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        private void EmitSequenceForIntegerRange(SequenceForIntegerRange seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            String ascendingVar = "ascending_" + seqFor.Id;
            String entryVar = "entry_" + seqFor.Id;
            String limitVar = "limit_" + seqFor.Id;
            source.AppendFrontFormat("int {0} = (int)({1});\n", entryVar, exprGen.GetSequenceExpression(seqFor.Left, source));
            source.AppendFrontFormat("int {0} = (int)({1});\n", limitVar, exprGen.GetSequenceExpression(seqFor.Right, source));
            source.AppendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");

            source.AppendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");

            source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForIndexAccessEquality(SequenceForIndexAccessEquality seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            String indexVar = "index_" + seqFor.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
            String entryVar = "entry_" + seqFor.Id;
            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                entryVar, indexVar);
            source.Append("(");
            source.Append(exprGen.GetSequenceExpression(seqFor.Expr, source));
            source.Append("))\n");
            source.AppendFront("{\n");
            source.Indent();

            if(emitProfiling)
                source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
            source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

            EmitSequence(seqFor.Seq, source);

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            String indexVar = "index_" + seqFor.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
            String entryVar = "entry_" + seqFor.Id;
            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                entryVar, indexVar);

            if(seqFor.Ascending)
                source.Append("Ascending");
            else
                source.Append("Descending");
            if(seqFor.From() != null && seqFor.To() != null)
            {
                source.Append("From");
                if(seqFor.IncludingFrom())
                    source.Append("Inclusive");
                else
                    source.Append("Exclusive");
                source.Append("To");
                if(seqFor.IncludingTo())
                    source.Append("Inclusive");
                else
                    source.Append("Exclusive");
                source.Append("(");
                source.Append(exprGen.GetSequenceExpression(seqFor.From(), source));
                source.Append(", ");
                source.Append(exprGen.GetSequenceExpression(seqFor.To(), source));
            }
            else if(seqFor.From() != null)
            {
                source.Append("From");
                if(seqFor.IncludingFrom())
                    source.Append("Inclusive");
                else
                    source.Append("Exclusive");
                source.Append("(");
                source.Append(exprGen.GetSequenceExpression(seqFor.From(), source));
            }
            else if(seqFor.To() != null)
            {
                source.Append("To");
                if(seqFor.IncludingTo())
                    source.Append("Inclusive");
                else
                    source.Append("Exclusive");
                source.Append("(");
                source.Append(exprGen.GetSequenceExpression(seqFor.To(), source));
            }
            else
            {
                source.Append("(");
            }

            source.Append("))\n");
            source.AppendFront("{\n");
            source.Indent();

            if(emitProfiling)
                source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
            source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

            EmitSequence(seqFor.Seq, source);

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForFunction(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            string sourceNodeName = "node_" + seqFor.Id;
            string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
            source.AppendFrontFormat("GRGEN_LIBGR.INode {0} = (GRGEN_LIBGR.INode)({1});\n", sourceNodeName, sourceNodeExpr);

            SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 2 ? seqFor.ArgExprs[1] : null;
            string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
            SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
            string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);

            string iterationVariable = "edge_" + seqFor.Id;
            string targetVariableOfIteration;
            string edgeMethod;
            string theOther;
            switch(seqFor.SequenceType)
            {
            case SequenceType.ForAdjacentNodes:
                edgeMethod = "Incident";
                theOther = iterationVariable + ".Opposite(node_" + seqFor.Id + ")";
                targetVariableOfIteration = theOther;
                break;
            case SequenceType.ForAdjacentNodesViaIncoming:
                edgeMethod = "Incoming";
                theOther = iterationVariable + ".Source";
                targetVariableOfIteration = theOther;
                break;
            case SequenceType.ForAdjacentNodesViaOutgoing:
                edgeMethod = "Outgoing";
                theOther = iterationVariable + ".Target";
                targetVariableOfIteration = theOther;
                break;
            case SequenceType.ForIncidentEdges:
                edgeMethod = "Incident";
                theOther = iterationVariable + ".Opposite(node_" + seqFor.Id + ")";
                targetVariableOfIteration = iterationVariable;
                break;
            case SequenceType.ForIncomingEdges:
                edgeMethod = "Incoming";
                theOther = iterationVariable + ".Source";
                targetVariableOfIteration = iterationVariable;
                break;
            case SequenceType.ForOutgoingEdges:
                edgeMethod = "Outgoing";
                theOther = iterationVariable + ".Target";
                targetVariableOfIteration = iterationVariable;
                break;
            default:
                edgeMethod = theOther = targetVariableOfIteration = "INTERNAL ERROR";
                break;
            }

            string profilingArgument = emitProfiling ? ", procEnv" : "";
            if(emitProfiling)
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in {1}.{2})\n",
                    iterationVariable, sourceNodeName, edgeMethod);
            }
            else
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in {1}.GetCompatible{2}({3}))\n",
                    iterationVariable, sourceNodeName, edgeMethod, incidentEdgeTypeExpr);
            }
            source.AppendFront("{\n");
            source.Indent();

            if(emitProfiling)
            {
                source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                source.AppendFrontFormat("if(!{0}.InstanceOf(", iterationVariable);
                source.Append(incidentEdgeTypeExpr);
                source.Append("))\n");
                source.AppendFront("\tcontinue;\n");
            }

            // incident/adjacent needs a check for adjacent node, cause only incident edge can be type constrained in the loop
            source.AppendFrontFormat("if(!{0}.InstanceOf({1}))\n",
                theOther, adjacentNodeTypeExpr);
            source.AppendFront("\tcontinue;\n");

            source.AppendFront(helper.SetVar(seqFor.Var, targetVariableOfIteration));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForReachable(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            string sourceNodeName = "node_" + seqFor.Id;
            string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
            source.AppendFrontFormat("GRGEN_LIBGR.INode {0} = (GRGEN_LIBGR.INode)({1});\n", sourceNodeName, sourceNodeExpr);

            SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 2 ? seqFor.ArgExprs[1] : null;
            string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
            SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
            string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);

            string iterationVariable = "iter_" + seqFor.Id;
            string iterationType;
            string reachableMethod;
            switch(seqFor.SequenceType)
            {
            case SequenceType.ForReachableNodes:
                reachableMethod = "";
                iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                break;
            case SequenceType.ForReachableNodesViaIncoming:
                reachableMethod = "Incoming";
                iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                break;
            case SequenceType.ForReachableNodesViaOutgoing:
                reachableMethod = "Outgoing";
                iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                break;
            case SequenceType.ForReachableEdges:
                reachableMethod = "Edges";
                iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                break;
            case SequenceType.ForReachableEdgesViaIncoming:
                reachableMethod = "EdgesIncoming";
                iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                break;
            case SequenceType.ForReachableEdgesViaOutgoing:
                reachableMethod = "EdgesOutgoing";
                iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                break;
            default:
                reachableMethod = iterationVariable = iterationType = "INTERNAL ERROR";
                break;
            }

            string profilingArgument = emitProfiling ? ", procEnv" : "";
            if(seqFor.SequenceType == SequenceType.ForReachableNodes || seqFor.SequenceType == SequenceType.ForReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableNodesViaOutgoing)
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode {0} in GraphHelper.Reachable{1}({2}, ({3}), ({4}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            else //if(seqFor.SequenceType == SequenceType.ForReachableEdges || seqFor.SequenceType == SequenceType.ForReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableEdgesViaOutgoing)
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in GraphHelper.Reachable{1}({2}, ({3}), ({4}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(helper.SetVar(seqFor.Var, iterationVariable));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForBoundedReachable(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            string sourceNodeName = "node_" + seqFor.Id;
            string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
            source.AppendFrontFormat("GRGEN_LIBGR.INode {0} = (GRGEN_LIBGR.INode)({1});\n", sourceNodeName, sourceNodeExpr);
            string depthVarName = "depth_" + seqFor.Id;
            string depthExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[1], source);
            source.AppendFrontFormat("int {0} = (int)({1});\n", depthVarName, depthExpr);

            SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
            string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
            SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 4 ? seqFor.ArgExprs[3] : null;
            string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);

            string iterationVariable = "iter_" + seqFor.Id;
            string iterationType;
            string reachableMethod = null;
            switch(seqFor.SequenceType)
            {
            case SequenceType.ForBoundedReachableNodes:
                reachableMethod = "";
                iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                break;
            case SequenceType.ForBoundedReachableNodesViaIncoming:
                reachableMethod = "Incoming";
                iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                break;
            case SequenceType.ForBoundedReachableNodesViaOutgoing:
                reachableMethod = "Outgoing";
                iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                break;
            case SequenceType.ForBoundedReachableEdges:
                reachableMethod = "Edges";
                iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                break;
            case SequenceType.ForBoundedReachableEdgesViaIncoming:
                reachableMethod = "EdgesIncoming";
                iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                break;
            case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                reachableMethod = "EdgesOutgoing";
                iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                break;
            default:
                iterationVariable = iterationType = "INTERNAL ERROR";
                break;
            }

            string profilingArgument = emitProfiling ? ", procEnv" : "";
            if(seqFor.SequenceType == SequenceType.ForBoundedReachableNodes || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing)
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode {0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}({2}, {3}, ({4}), ({5}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, depthVarName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            else //if(seqFor.SequenceType == SequenceType.ForBoundedReachableEdges || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}({2}, {3}, ({4}), ({5}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, depthVarName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(helper.SetVar(seqFor.Var, iterationVariable));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");

        }

        private void EmitSequenceForNodesEdges(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            String iterationVariableName = "elem_" + seqFor.Id;
            if(seqFor.SequenceType == SequenceType.ForNodes)
            {
                SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode {0} in graph.GetCompatibleNodes({1}))\n", 
                    iterationVariableName, adjacentNodeTypeExpr);
            }
            else
            {
                SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in graph.GetCompatibleEdges({1}))\n", 
                    iterationVariableName, incidentEdgeTypeExpr);
            }
            source.AppendFront("{\n");
            source.Indent();

            if(emitProfiling)
                source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
            source.AppendFront(helper.SetVar(seqFor.Var, iterationVariableName));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceIterationMin(SequenceIterationMin seqMin, SourceBuilder source)
        {
            String iterationVariableName = "i_" + seqMin.Id;
            source.AppendFrontFormat("long {0} = 0;\n", iterationVariableName);
            source.AppendFront("while(true)\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqMin.Seq, source);

            source.AppendFront("if(!" + compGen.GetResultVar(seqMin.Seq) + ")\n");
            source.AppendFront("\tbreak;\n");
            source.AppendFrontFormat("++{0};\n", iterationVariableName);
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(compGen.SetResultVar(seqMin, iterationVariableName + " >= " + seqMin.Min));
        }

        private void EmitSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, SourceBuilder source)
        {
            String iterationVariableName = "i_" + seqMinMax.Id;
            source.AppendFrontFormat("long {0} = 0;\n", iterationVariableName);
            source.AppendFrontFormat("for(; {0} < {1}; ++{0})\n", iterationVariableName, seqMinMax.Max);
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqMinMax.Seq, source);

            source.AppendFront("if(!" + compGen.GetResultVar(seqMinMax.Seq) + ")\n");
            source.AppendFront("\tbreak;\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(compGen.SetResultVar(seqMinMax, iterationVariableName + " >= " + seqMinMax.Min));
        }

        private void EmitSequenceDeclareVariable(SequenceDeclareVariable seqDeclVar, SourceBuilder source)
        {
            source.AppendFront(helper.SetVar(seqDeclVar.DestVar, TypesHelper.DefaultValueString(seqDeclVar.DestVar.Type, env.Model)));
            source.AppendFront(compGen.SetResultVar(seqDeclVar, "true"));
        }

        private void EmitSequenceAssignConstToVar(SequenceAssignConstToVar seqToVar, SourceBuilder source)
        {
            source.AppendFront(helper.SetVar(seqToVar.DestVar, helper.GetConstant(seqToVar.Constant)));
            source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignContainerConstructorToVar(SequenceAssignContainerConstructorToVar seqToVar, SourceBuilder source)
        {
            source.AppendFront(helper.SetVar(seqToVar.DestVar, exprGen.GetSequenceExpression(seqToVar.Constructor, source)));
            source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignVarToVar(SequenceAssignVarToVar seqToVar, SourceBuilder source)
        {
            source.AppendFront(helper.SetVar(seqToVar.DestVar, helper.GetVar(seqToVar.Variable)));
            source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar seqToVar, SourceBuilder source)
        {
            EmitSequence(seqToVar.Seq, source);
            source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq)));
            source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceOrAssignSequenceResultToVar(SequenceOrAssignSequenceResultToVar seqToVar, SourceBuilder source)
        {
            EmitSequence(seqToVar.Seq, source);
            source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq) + "|| (bool)" + helper.GetVar(seqToVar.DestVar)));
            source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAndAssignSequenceResultToVar(SequenceAndAssignSequenceResultToVar seqToVar, SourceBuilder source)
        {
            EmitSequence(seqToVar.Seq, source);
            source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq) + "&& (bool)" + helper.GetVar(seqToVar.DestVar)));
            source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignRandomIntToVar(SequenceAssignRandomIntToVar seqRandomToVar, SourceBuilder source)
        {
            source.AppendFront(helper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.Next(" + seqRandomToVar.Number + ")"));
            source.AppendFront(compGen.SetResultVar(seqRandomToVar, "true"));
        }

        private void EmitSequenceAssignRandomDoubleToVar(SequenceAssignRandomDoubleToVar seqRandomToVar, SourceBuilder source)
        {
            source.AppendFront(helper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()"));
            source.AppendFront(compGen.SetResultVar(seqRandomToVar, "true"));
        }

        private void EmitSequenceAll(SequenceNAry seqAll, bool disjunction, bool lazy, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqAll, disjunction ? "false" : "true"));
            String continueDecisionName = "continue_" + seqAll.Id;
            source.AppendFrontFormat("bool {0} = true;\n", continueDecisionName);
            String sequencesToExecuteVarName = "sequencestoexecutevar_" + seqAll.Id;
            source.AppendFrontFormat("List<int> {0} = new List<int>({1});\n", sequencesToExecuteVarName, seqAll.Sequences.Count);
            source.AppendFrontFormat("for(int i = 0; i < {0}; ++i)\n", seqAll.Sequences.Count);
            source.AppendFrontFormat("\t{0}.Add(i);\n", sequencesToExecuteVarName);
            source.AppendFrontFormat("while({0}.Count > 0 && {1})\n", sequencesToExecuteVarName, continueDecisionName);
            source.AppendFront("{\n");
            source.Indent();
            String positionOfSequenceToExecuteName = "positionofsequencetoexecute_" + seqAll.Id;
            source.AppendFrontFormat("int {0} = GRGEN_LIBGR.Sequence.randomGenerator.Next({1}.Count);\n", 
                positionOfSequenceToExecuteName, sequencesToExecuteVarName);
            source.AppendFrontFormat("switch({0}[{1}])\n", sequencesToExecuteVarName, positionOfSequenceToExecuteName);
            source.AppendFront("{\n");
            source.Indent();
            for(int i = 0; i < seqAll.Sequences.Count; ++i)
            {
                source.AppendFrontFormat("case {0}:\n", i);
                source.AppendFront("{\n");
                source.Indent();

                EmitSequence(seqAll.Sequences[i], source);

                source.AppendFrontFormat("{0}.Remove({1});\n", sequencesToExecuteVarName, i);
                String sequenceResult = compGen.GetResultVar(seqAll) + (disjunction ? " || " : " && ") + compGen.GetResultVar(seqAll.Sequences[i]);
                source.AppendFront(compGen.SetResultVar(seqAll, sequenceResult));
                if(lazy)
                {
                    source.AppendFrontFormat("if({0}" + compGen.GetResultVar(seqAll) + ")\n", disjunction ? "" : "!");
                    source.AppendFrontFormat("\t{0} = false;\n", continueDecisionName);
                }
                source.AppendFront("break;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceWeighted(SequenceWeightedOne seqWeighted, SourceBuilder source)
        {
            String pointToExecName = "pointtoexec_" + seqWeighted.Id;
            source.AppendFrontFormat("double {0} = GRGEN_LIBGR.Sequence.randomGenerator.NextDouble() * {1};\n", 
                pointToExecName, seqWeighted.Numbers[seqWeighted.Numbers.Count - 1].ToString(System.Globalization.CultureInfo.InvariantCulture));
            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
            {
                if(i == 0)
                    source.AppendFrontFormat("if({0} <= {1})\n", pointToExecName, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                else if(i == seqWeighted.Sequences.Count - 1)
                    source.AppendFrontFormat("else\n");
                else
                    source.AppendFrontFormat("else if({0} <= {1})\n", pointToExecName, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                source.AppendFront("{\n");
                source.Indent();

                EmitSequence(seqWeighted.Sequences[i], source);

                source.AppendFront(compGen.SetResultVar(seqWeighted, compGen.GetResultVar(seqWeighted.Sequences[i])));
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        private void EmitSequenceSome(SequenceSomeFromSet seqSome, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqSome, "false"));

            // emit code for matching all the contained rules
            for(int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                new SequenceSomeRuleCallGenerator(seqSome, (SequenceRuleCall)seqSome.Sequences[i], helper)
                    .EmitMatching(source, this, compGen);
            }

            // emit code for deciding on the match to rewrite
            String totalMatchToApply = "total_match_to_apply_" + seqSome.Id;
            String curTotalMatch = "cur_total_match_" + seqSome.Id;
            if(seqSome.Random)
            {
                source.AppendFront("int " + totalMatchToApply + " = 0;\n");
                for(int i = 0; i < seqSome.Sequences.Count; ++i)
                {
                    SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                    String matchesName = "matches_" + seqRule.Id;
                    if(seqRule.SequenceType == SequenceType.RuleCall)
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                    else if(seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                        source.AppendFront("if(" + matchesName + ".Count>0) ++" + totalMatchToApply + ";\n");
                    else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                }
                source.AppendFront(totalMatchToApply + " = GRGEN_LIBGR.Sequence.randomGenerator.Next(" + totalMatchToApply + ");\n");
                source.AppendFront("int " + curTotalMatch + " = 0;\n");
            }

            // code to handle the rewrite next match
            String firstRewrite = "first_rewrite_" + seqSome.Id;
            source.AppendFront("bool " + firstRewrite + " = true;\n");

            // emit code for rewriting all the contained rules which got matched
            for(int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                new SequenceSomeRuleCallGenerator(seqSome, (SequenceRuleCall)seqSome.Sequences[i], helper)
                    .EmitRewriting(source, this, compGen, totalMatchToApply, curTotalMatch, firstRewrite, fireDebugEvents);
            }
        }

        private void EmitSequenceTransaction(SequenceTransaction seqTrans, SourceBuilder source)
        {
            String transactionId = "transID_" + seqTrans.Id;
            source.AppendFront("int " + transactionId + " = procEnv.TransactionManager.Start();\n");
            EmitSequence(seqTrans.Seq, source);
            source.AppendFront("if(" + compGen.GetResultVar(seqTrans.Seq) + ")\n");
            source.AppendFront("\tprocEnv.TransactionManager.Commit(" + transactionId + ");\n");
            source.AppendFront("else\n");
            source.AppendFront("\tprocEnv.TransactionManager.Rollback(" + transactionId + ");\n");
            source.AppendFront(compGen.SetResultVar(seqTrans, compGen.GetResultVar(seqTrans.Seq)));
        }

        private void EmitSequencePause(SequencePause seqPause, SourceBuilder source)
        {
            source.AppendFront("procEnv.TransactionManager.Pause();\n");
            EmitSequence(seqPause.Seq, source);
            source.AppendFront("procEnv.TransactionManager.Resume();\n");
            source.AppendFront(compGen.SetResultVar(seqPause, compGen.GetResultVar(seqPause.Seq)));
        }

        private void EmitSequenceExecuteInSubgraph(SequenceExecuteInSubgraph seqExecInSub, SourceBuilder source)
        {
            string subgraph;
            if(seqExecInSub.AttributeName == null)
                subgraph = helper.GetVar(seqExecInSub.SubgraphVar);
            else
            {
                string element = "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqExecInSub.SubgraphVar) + ")";
                subgraph = element + ".GetAttribute(\"" + seqExecInSub.AttributeName + "\")";
            }
            source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + subgraph + ");\n");
            source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            EmitSequence(seqExecInSub.Seq, source);
            source.AppendFront("procEnv.ReturnFromSubgraph();\n");
            source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            source.AppendFront(compGen.SetResultVar(seqExecInSub, compGen.GetResultVar(seqExecInSub.Seq)));
        }

        private void EmitSequenceBooleanComputation(SequenceBooleanComputation seqComp, SourceBuilder source)
        {
            compGen.EmitSequenceComputation(seqComp.Computation, source);
            if(seqComp.Computation.ReturnsValue)
                source.AppendFront(compGen.SetResultVar(seqComp, "!GRGEN_LIBGR.TypesHelper.IsDefaultValue(" + compGen.GetResultVar(seqComp.Computation) + ")"));
            else
                source.AppendFront(compGen.SetResultVar(seqComp, "true"));
        }

        public String GetSequenceResult(Sequence seq)
        {
            return compGen.GetResultVar(seq);
        }

        internal void EmitFilterCall(SourceBuilder source, FilterCall filterCall, string patternName, string matchesName)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                switch(filterCall.Name)
                {
                case "keepFirst":
                    source.AppendFrontFormat("{0}.FilterKeepFirst((int)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                case "keepLast":
                    source.AppendFrontFormat("{0}.FilterKeepLast((int)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                case "keepFirstFraction":
                    source.AppendFrontFormat("{0}.FilterKeepFirstFraction((double)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                case "keepLastFraction":
                    source.AppendFrontFormat("{0}.FilterKeepLastFraction((double)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                case "removeFirst":
                    source.AppendFrontFormat("{0}.FilterRemoveFirst((int)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                case "removeLast":
                    source.AppendFrontFormat("{0}.FilterRemoveLast((int)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                case "removeFirstFraction":
                    source.AppendFrontFormat("{0}.FilterRemoveFirstFraction((double)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                case "removeLastFraction":
                    source.AppendFrontFormat("{0}.FilterRemoveLastFraction((double)({1}));\n",
                        matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                    break;
                }
            }
            else
            {
                if(filterCall.IsAutoGenerated && filterCall.Name == "auto")
                {
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}(procEnv, {3});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, matchesName);
                }
                else if(filterCall.IsAutoGenerated)
                {
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}_{3}(procEnv, {4});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, filterCall.EntitySuffixForName, matchesName);
                }
                else
                {
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}(procEnv, {2}",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), filterCall.Name, matchesName);
                    for(int i = 0; i < filterCall.ArgumentExpressions.Length; ++i)
                    {
                        source.AppendFormat(", ({0})({1})",
                            TypesHelper.XgrsTypeToCSharpType(helper.actionsTypeInformation.filterFunctionsToInputTypes[filterCall.Name][i], model),
                            exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[i], source));
                    }
                    source.Append(");\n");
                }
            }
        }
    }
}
