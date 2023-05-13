/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using COMP_HELPER = de.unika.ipd.grGen.lgsp.SequenceComputationGeneratorHelper;

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

        public readonly SequenceExpressionGenerator exprGen;

        readonly SequenceGeneratorHelper seqHelper;

        readonly bool fireDebugEvents;
        readonly bool emitProfiling;


        public SequenceGenerator(IGraphModel model, SequenceCheckingEnvironmentCompiled env,
            SequenceComputationGenerator compGen, SequenceExpressionGenerator exprGen, SequenceGeneratorHelper seqHelper,
            bool fireDebugEvents, bool emitProfiling)
        {
            this.model = model;

            this.env = env;

            this.compGen = compGen;

            this.exprGen = exprGen;

            this.seqHelper = seqHelper;

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
                new SequenceRuleOrRuleAllCallGenerator((SequenceRuleCall)seq, exprGen, seqHelper, fireDebugEvents)
                    .Emit(source, this);
                break;

            case SequenceType.RulePrefixedSequence:
                new SequenceRulePrefixedSequenceGenerator((SequenceRulePrefixedSequence)seq, exprGen, seqHelper, fireDebugEvents)
                    .Emit(source, this);
                break;

            case SequenceType.SequenceCall:
                EmitSequenceCall((SequenceSequenceCall)seq, source);
                break;

            case SequenceType.Not:
                EmitSequenceNot((SequenceNot)seq, source);
                break;

            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
                EmitSequenceBinaryLazy((SequenceBinary)seq, source);
                break;

            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.StrictAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
                EmitSequenceBinary((SequenceBinary)seq, source);
                break;

            case SequenceType.IfThen:
                EmitSequenceIfThen((SequenceIfThen)seq, source);
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
                EmitSequenceForFunction((SequenceForFunction)seq, source);
                break;

            case SequenceType.ForReachableNodes:
            case SequenceType.ForReachableNodesViaIncoming:
            case SequenceType.ForReachableNodesViaOutgoing:
            case SequenceType.ForReachableEdges:
            case SequenceType.ForReachableEdgesViaIncoming:
            case SequenceType.ForReachableEdgesViaOutgoing:
                EmitSequenceForReachable((SequenceForFunction)seq, source);
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
                new SequenceForMatchGenerator((SequenceForMatch)seq, exprGen, seqHelper, fireDebugEvents)
                    .Emit(source, this);
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

            case SequenceType.AssignObjectConstructorToVar:
                EmitSequenceAssignObjectConstructorToVar((SequenceAssignObjectConstructorToVar)seq, source);
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
                EmitSequenceAll((SequenceNAry)seq, true, true, source);
                break;

            case SequenceType.LazyAndAll:
                EmitSequenceAll((SequenceNAry)seq, false, true, source);
                break;

            case SequenceType.StrictOrAll:
                EmitSequenceAll((SequenceNAry)seq, true, false, source);
                break;

            case SequenceType.StrictAndAll:
                EmitSequenceAll((SequenceNAry)seq, false, false, source);
                break;

            case SequenceType.WeightedOne:
                EmitSequenceWeighted((SequenceWeightedOne)seq, source);
                break;

            case SequenceType.SomeFromSet:
                EmitSequenceSome((SequenceSomeFromSet)seq, source);
                break;

            case SequenceType.MultiRuleAllCall:
                EmitSequenceMultiRuleAllCall((SequenceMultiRuleAllCall)seq, source);
                break;

            case SequenceType.MultiRulePrefixedSequence:
                EmitSequenceMultiRulePrefixedSequence((SequenceMultiRulePrefixedSequence)seq, source);
                break;

            case SequenceType.Transaction:
                EmitSequenceTransaction((SequenceTransaction)seq, source);
                break;

            case SequenceType.Backtrack:
                new SequenceBacktrackGenerator((SequenceBacktrack)seq, exprGen, seqHelper, fireDebugEvents)
                    .Emit(source, this);
                break;

            case SequenceType.MultiBacktrack:
                new SequenceMultiBacktrackGenerator((SequenceMultiBacktrack)seq, exprGen, seqHelper, fireDebugEvents)
                    .Emit(source, this);
                break;

            case SequenceType.MultiSequenceBacktrack:
                new SequenceMultiSequenceBacktrackGenerator((SequenceMultiSequenceBacktrack)seq, exprGen, seqHelper, fireDebugEvents)
                    .Emit(source, this);
                break;

            case SequenceType.Pause:
                EmitSequencePause((SequencePause)seq, source);
                break;

            case SequenceType.ExecuteInSubgraph:
                EmitSequenceExecuteInSubgraph((SequenceExecuteInSubgraph)seq, source);
                break;

            case SequenceType.Lock:
                EmitSequenceLock((SequenceLock)seq, source);
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
            SequenceExpression[] ArgumentExpressions = seqSeq.ArgumentExpressions;
            SequenceVariable[] ReturnVars = seqSeq.ReturnVars;
            String parameterDeclarations = null;
            String parameters = null;
            if(seqSeq.Subgraph != null)
                parameters = seqHelper.BuildParametersInDeclarations(seqSeq, ArgumentExpressions, source, out parameterDeclarations);
            else
                parameters = seqHelper.BuildParameters(seqSeq, ArgumentExpressions, source);
            String outParameterDeclarations;
            String outArguments;
            String outAssignments;
            seqHelper.BuildOutParameters(seqSeq, ReturnVars, out outParameterDeclarations, out outArguments, out outAssignments);

            if(seqSeq.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + seqHelper.GetVar(seqSeq.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            if(outParameterDeclarations.Length != 0)
                source.AppendFront(outParameterDeclarations + "\n");
            source.AppendFront("if(" + "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqSeq.Package) + "Sequence_" + seqSeq.Name + ".ApplyXGRS_" + seqSeq.Name
                                + "(procEnv" + parameters + outArguments + ")) {\n");
            source.Indent();
            if(outAssignments.Length != 0)
                source.AppendFront(outAssignments + "\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqSeq, "true"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqSeq, "false"));
            source.Unindent();
            source.AppendFront("}\n");

            if(seqSeq.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

        private void EmitSequenceNot(SequenceNot seqNot, SourceBuilder source)
        {
            EmitSequence(seqNot.Seq, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqNot, "!" + COMP_HELPER.GetResultVar(seqNot.Seq)));
        }

        private void EmitSequenceBinaryLazy(SequenceBinary seqBin, SourceBuilder source)
        {
            if(seqBin.Random)
            {
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
                source.AppendFront("if(" + COMP_HELPER.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(COMP_HELPER.SetResultVar(seq, "true"));
                source.Unindent();
            }
            else //if(seq.SequenceType == SequenceType.LazyAnd)
            {
                source.AppendFront("if(!" + COMP_HELPER.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(COMP_HELPER.SetResultVar(seq, "false"));
                source.Unindent();
            }

            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqRight, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seq, COMP_HELPER.GetResultVar(seqRight)));

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
                source.AppendFront(COMP_HELPER.SetResultVar(seqBin, COMP_HELPER.GetResultVar(seqBin.Left)));
                return;
            }
            else if(seqBin.SequenceType == SequenceType.ThenRight)
            {
                source.AppendFront(COMP_HELPER.SetResultVar(seqBin, COMP_HELPER.GetResultVar(seqBin.Right)));
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
            source.AppendFront(COMP_HELPER.SetResultVar(seqBin, COMP_HELPER.GetResultVar(seqBin.Left) + " " + op + " " + COMP_HELPER.GetResultVar(seqBin.Right)));
        }

        private void EmitSequenceIfThen(SequenceIfThen seqIfThen, SourceBuilder source)
        {
            EmitSequence(seqIfThen.Left, source);

            source.AppendFront("if(!" + COMP_HELPER.GetResultVar(seqIfThen.Left) + ")\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqIfThen, "true")); // lazy implication
            source.Unindent();

            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqIfThen.Right, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqIfThen, COMP_HELPER.GetResultVar(seqIfThen.Right)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceIfThenElse(SequenceIfThenElse seqIf, SourceBuilder source)
        {
            EmitSequence(seqIf.Condition, source);

            source.AppendFront("if(" + COMP_HELPER.GetResultVar(seqIf.Condition) + ")");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqIf.TrueCase, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqIf, COMP_HELPER.GetResultVar(seqIf.TrueCase)));

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqIf.FalseCase, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqIf, COMP_HELPER.GetResultVar(seqIf.FalseCase)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForContainer(SequenceForContainer seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            if(seqFor.Container.Type == "")
                EmitSequenceForContainerUnknownType(seqFor, source);
            else if(seqFor.Container.Type.StartsWith("array"))
                EmitSequenceForContainerArrayType(seqFor, source);
            else if(seqFor.Container.Type.StartsWith("deque"))
                EmitSequenceForContainerDequeType(seqFor, source);
            else
                EmitSequenceForContainerSetMapType(seqFor, source);
        }

        private void EmitSequenceForContainerUnknownType(SequenceForContainer seqFor, SourceBuilder source)
        {
            String entryVar = "entry_" + seqFor.Id;
            String indexVar = "index_" + seqFor.Id;

            // type not statically known? -> might be Dictionary or List or Deque dynamically, must decide at runtime
            source.AppendFront("if(" + seqHelper.GetVar(seqFor.Container) + " is IList) {\n");
            source.Indent();

            source.AppendFront("IList " + entryVar + " = (IList) " + seqHelper.GetVar(seqFor.Container) + ";\n");
            source.AppendFrontFormat("for(int {0}=0; {0} < {1}.Count; ++{0})\n", indexVar, entryVar);
            source.AppendFront("{\n");
            source.Indent();
            if(seqFor.VarDst != null)
            {
                source.AppendFront(seqHelper.SetVar(seqFor.Var, indexVar));
                source.AppendFront(seqHelper.SetVar(seqFor.VarDst, entryVar + "[" + indexVar + "]"));
            }
            else
                source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar + "[" + indexVar + "]"));

            EmitSequence(seqFor.Seq, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("} else if(" + seqHelper.GetVar(seqFor.Container) + " is GRGEN_LIBGR.IDeque) {\n");
            source.Indent();

            source.AppendFront("GRGEN_LIBGR.IDeque " + entryVar + " = (GRGEN_LIBGR.IDeque) " + seqHelper.GetVar(seqFor.Container) + ";\n");
            source.AppendFrontFormat("for(int {0}=0; {0} < {1}.Count; ++{0})\n", indexVar, entryVar);
            source.AppendFront("{\n");
            source.Indent();
            if(seqFor.VarDst != null)
            {
                source.AppendFront(seqHelper.SetVar(seqFor.Var, indexVar));
                source.AppendFront(seqHelper.SetVar(seqFor.VarDst, entryVar + "[" + indexVar + "]"));
            }
            else
                source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar + "[" + indexVar + "]"));

            EmitSequence(seqFor.Seq, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();

            source.AppendFront("foreach(DictionaryEntry " + entryVar + " in (IDictionary)" + seqHelper.GetVar(seqFor.Container) + ")\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar + ".Key"));
            if(seqFor.VarDst != null)
                source.AppendFront(seqHelper.SetVar(seqFor.VarDst, entryVar + ".Value"));

            EmitSequence(seqFor.Seq, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForContainerArrayType(SequenceForContainer seqFor, SourceBuilder source)
        {
            String arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
            String entryVar = "entry_" + seqFor.Id;
            String indexVar = "index_" + seqFor.Id;
            source.AppendFrontFormat("List<{0}> {1} = (List<{0}>) " + seqHelper.GetVar(seqFor.Container) + ";\n", arrayValueType, entryVar);
            source.AppendFrontFormat("for(int {0}=0; {0}<{1}.Count; ++{0})\n", indexVar, entryVar);
            source.AppendFront("{\n");
            source.Indent();

            if(seqFor.VarDst != null)
            {
                source.AppendFront(seqHelper.SetVar(seqFor.Var, indexVar));
                source.AppendFront(seqHelper.SetVar(seqFor.VarDst, entryVar + "[" + indexVar + "]"));
            }
            else
                source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar + "[" + indexVar + "]"));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForContainerDequeType(SequenceForContainer seqFor, SourceBuilder source)
        {
            String dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
            String entryVar = "entry_" + seqFor.Id;
            String indexVar = "index_" + seqFor.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.Deque<{0}> {1} = (GRGEN_LIBGR.Deque<{0}>) " + seqHelper.GetVar(seqFor.Container) + ";\n", dequeValueType, entryVar);
            source.AppendFrontFormat("for(int {0}=0; {0}<{1}.Count; ++{0})\n", indexVar, entryVar);
            source.AppendFront("{\n");
            source.Indent();

            if(seqFor.VarDst != null)
            {
                source.AppendFront(seqHelper.SetVar(seqFor.Var, indexVar));
                source.AppendFront(seqHelper.SetVar(seqFor.VarDst, entryVar + "[" + indexVar + "]"));
            }
            else
                source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar + "[" + indexVar + "]"));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForContainerSetMapType(SequenceForContainer seqFor, SourceBuilder source)
        {
            String srcTypeXgrs = TypesHelper.ExtractSrc(seqFor.Container.Type);
            String srcType = TypesHelper.XgrsTypeToCSharpType(srcTypeXgrs, model);
            String dstTypeXgrs = TypesHelper.ExtractDst(seqFor.Container.Type);
            String dstType = TypesHelper.XgrsTypeToCSharpType(dstTypeXgrs, model);
            String entryVar = "entry_" + seqFor.Id;
            source.AppendFront("foreach(KeyValuePair<" + srcType + "," + dstType + "> " + entryVar + " in " + seqHelper.GetVar(seqFor.Container) + ")\n");
            source.AppendFront("{\n");
            source.Indent();

            if(dstTypeXgrs == "SetValueType")
                source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar + ".Key"));
            else
                source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar + ".Key"));

            if(seqFor.VarDst != null)
                source.AppendFront(seqHelper.SetVar(seqFor.VarDst, entryVar + ".Value"));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForIntegerRange(SequenceForIntegerRange seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            String ascendingVar = "ascending_" + seqFor.Id;
            String entryVar = "entry_" + seqFor.Id;
            String limitVar = "limit_" + seqFor.Id;
            source.AppendFrontFormat("int {0} = (int)({1});\n", entryVar, exprGen.GetSequenceExpression(seqFor.Left, source));
            source.AppendFrontFormat("int {0} = (int)({1});\n", limitVar, exprGen.GetSequenceExpression(seqFor.Right, source));
            source.AppendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");

            source.AppendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForIndexAccessEquality(SequenceForIndexAccessEquality seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

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
            source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar));

            EmitSequence(seqFor.Seq, source);

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

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
            source.AppendFront(seqHelper.SetVar(seqFor.Var, entryVar));

            EmitSequence(seqFor.Seq, source);

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForFunction(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            string sourceNodeName = "node_" + seqFor.Id;
            string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
            source.AppendFrontFormat("GRGEN_LIBGR.INode {0} = (GRGEN_LIBGR.INode)({1});\n", sourceNodeName, sourceNodeExpr);

            SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 2 ? seqFor.ArgExprs[1] : null;
            string incidentEdgeTypeExpr = seqHelper.ExtractEdgeType(source, IncidentEdgeType);
            SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
            string adjacentNodeTypeExpr = seqHelper.ExtractNodeType(source, AdjacentNodeType);

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
                source.AppendFrontIndented("continue;\n");
            }

            // incident/adjacent needs a check for adjacent node, cause only incident edge can be type constrained in the loop
            source.AppendFrontFormat("if(!{0}.InstanceOf({1}))\n",
                theOther, adjacentNodeTypeExpr);
            source.AppendFrontIndented("continue;\n");

            source.AppendFront(seqHelper.SetVar(seqFor.Var, targetVariableOfIteration));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForReachable(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            string sourceNodeName = "node_" + seqFor.Id;
            string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
            source.AppendFrontFormat("GRGEN_LIBGR.INode {0} = (GRGEN_LIBGR.INode)({1});\n", sourceNodeName, sourceNodeExpr);

            SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 2 ? seqFor.ArgExprs[1] : null;
            string incidentEdgeTypeExpr = seqHelper.ExtractEdgeType(source, IncidentEdgeType);
            SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
            string adjacentNodeTypeExpr = seqHelper.ExtractNodeType(source, AdjacentNodeType);

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
            if(IteratesNodes(seqFor.SequenceType))
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode {0} in GRGEN_LIBGR.GraphHelper.Reachable{1}({2}, ({3}), ({4}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            else
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in GRGEN_LIBGR.GraphHelper.Reachable{1}({2}, ({3}), ({4}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(seqHelper.SetVar(seqFor.Var, iterationVariable));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForBoundedReachable(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            string sourceNodeName = "node_" + seqFor.Id;
            string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
            source.AppendFrontFormat("GRGEN_LIBGR.INode {0} = (GRGEN_LIBGR.INode)({1});\n", sourceNodeName, sourceNodeExpr);
            string depthVarName = "depth_" + seqFor.Id;
            string depthExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[1], source);
            source.AppendFrontFormat("int {0} = (int)({1});\n", depthVarName, depthExpr);

            SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
            string incidentEdgeTypeExpr = seqHelper.ExtractEdgeType(source, IncidentEdgeType);
            SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 4 ? seqFor.ArgExprs[3] : null;
            string adjacentNodeTypeExpr = seqHelper.ExtractNodeType(source, AdjacentNodeType);

            string iterationVariable = "iter_" + seqFor.Id;
            string iterationType;
            string reachableMethod;
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
                reachableMethod = iterationVariable = iterationType = "INTERNAL ERROR";
                break;
            }

            string profilingArgument = emitProfiling ? ", procEnv" : "";
            if(IteratesNodes(seqFor.SequenceType))
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode {0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}({2}, {3}, ({4}), ({5}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, depthVarName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            else
            {
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}({2}, {3}, ({4}), ({5}), graph" + profilingArgument + "))\n",
                    iterationVariable, reachableMethod, sourceNodeName, depthVarName, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
            }
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(seqHelper.SetVar(seqFor.Var, iterationVariable));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceForNodesEdges(SequenceForFunction seqFor, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            String iterationVariableName = "elem_" + seqFor.Id;
            if(IteratesNodes(seqFor.SequenceType))
            {
                SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                string adjacentNodeTypeExpr = seqHelper.ExtractNodeType(source, AdjacentNodeType);
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode {0} in graph.GetCompatibleNodes({1}))\n", 
                    iterationVariableName, adjacentNodeTypeExpr);
            }
            else
            {
                SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                string incidentEdgeTypeExpr = seqHelper.ExtractEdgeType(source, IncidentEdgeType);
                source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge {0} in graph.GetCompatibleEdges({1}))\n", 
                    iterationVariableName, incidentEdgeTypeExpr);
            }
            source.AppendFront("{\n");
            source.Indent();

            if(emitProfiling)
                source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
            source.AppendFront(seqHelper.SetVar(seqFor.Var, iterationVariableName));

            EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private bool IteratesNodes(SequenceType seqType)
        {
            switch(seqType)
            {
            case SequenceType.ForAdjacentNodes:
            case SequenceType.ForAdjacentNodesViaIncoming:
            case SequenceType.ForAdjacentNodesViaOutgoing:
            case SequenceType.ForReachableNodes:
            case SequenceType.ForReachableNodesViaIncoming:
            case SequenceType.ForReachableNodesViaOutgoing:
            case SequenceType.ForBoundedReachableNodes:
            case SequenceType.ForBoundedReachableNodesViaIncoming:
            case SequenceType.ForBoundedReachableNodesViaOutgoing:
            case SequenceType.ForNodes:
                return true;
            default:
                return false;
            }
        }

        private void EmitSequenceIterationMin(SequenceIterationMin seqMin, SourceBuilder source)
        {
            String iterationMinLimitVariableName = "iminl_" + seqMin.Id;
            source.AppendFrontFormat("int {0} = (int)({1});\n", iterationMinLimitVariableName, exprGen.GetSequenceExpression(seqMin.MinExpr, source));
            String iterationCountVariableName = "i_" + seqMin.Id;
            source.AppendFrontFormat("int {0} = 0;\n", iterationCountVariableName);
            source.AppendFront("while(true)\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqMin.Seq, source);

            source.AppendFront("if(!" + COMP_HELPER.GetResultVar(seqMin.Seq) + ")\n");
            source.AppendFrontIndented("break;\n");
            source.AppendFrontFormat("++{0};\n", iterationCountVariableName);
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqMin, iterationCountVariableName + " >= " + iterationMinLimitVariableName));
        }

        private void EmitSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, SourceBuilder source)
        {
            String iterationMinLimitVariableName = "iminl_" + seqMinMax.Id;
            source.AppendFrontFormat("int {0} = (int)({1});\n", iterationMinLimitVariableName, exprGen.GetSequenceExpression(seqMinMax.MinExpr, source));
            String iterationMaxLimitVariableName = "imaxl_" + seqMinMax.Id;
            source.AppendFrontFormat("int {0} = (int)({1});\n", iterationMaxLimitVariableName, exprGen.GetSequenceExpression(seqMinMax.MaxExpr, source));
            String iterationCountVariableName = "i_" + seqMinMax.Id;
            source.AppendFrontFormat("int {0} = 0;\n", iterationCountVariableName);
            source.AppendFrontFormat("for(; {0} < {1}; ++{0})\n", iterationCountVariableName, iterationMaxLimitVariableName);
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqMinMax.Seq, source);

            source.AppendFront("if(!" + COMP_HELPER.GetResultVar(seqMinMax.Seq) + ")\n");
            source.AppendFrontIndented("break;\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqMinMax, iterationCountVariableName + " >= " + iterationMinLimitVariableName));
        }

        private void EmitSequenceDeclareVariable(SequenceDeclareVariable seqDeclVar, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqDeclVar.DestVar, TypesHelper.DefaultValueString(seqDeclVar.DestVar.Type, env.Model)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqDeclVar, "true"));
        }

        private void EmitSequenceAssignConstToVar(SequenceAssignConstToVar seqToVar, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqToVar.DestVar, seqHelper.GetConstant(seqToVar.Constant)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignContainerConstructorToVar(SequenceAssignContainerConstructorToVar seqToVar, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqToVar.DestVar, exprGen.GetSequenceExpression(seqToVar.Constructor, source)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignObjectConstructorToVar(SequenceAssignObjectConstructorToVar seqToVar, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqToVar.DestVar, exprGen.GetSequenceExpression(seqToVar.Constructor, source)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignVarToVar(SequenceAssignVarToVar seqToVar, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqToVar.DestVar, seqHelper.GetVar(seqToVar.Variable)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar seqToVar, SourceBuilder source)
        {
            EmitSequence(seqToVar.Seq, source);
            source.AppendFront(seqHelper.SetVar(seqToVar.DestVar, COMP_HELPER.GetResultVar(seqToVar.Seq)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceOrAssignSequenceResultToVar(SequenceOrAssignSequenceResultToVar seqToVar, SourceBuilder source)
        {
            EmitSequence(seqToVar.Seq, source);
            source.AppendFront(seqHelper.SetVar(seqToVar.DestVar, COMP_HELPER.GetResultVar(seqToVar.Seq) + "|| (bool)" + seqHelper.GetVar(seqToVar.DestVar)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAndAssignSequenceResultToVar(SequenceAndAssignSequenceResultToVar seqToVar, SourceBuilder source)
        {
            EmitSequence(seqToVar.Seq, source);
            source.AppendFront(seqHelper.SetVar(seqToVar.DestVar, COMP_HELPER.GetResultVar(seqToVar.Seq) + "&& (bool)" + seqHelper.GetVar(seqToVar.DestVar)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqToVar, "true"));
        }

        private void EmitSequenceAssignRandomIntToVar(SequenceAssignRandomIntToVar seqRandomToVar, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.Next(" + seqRandomToVar.Number + ")"));
            source.AppendFront(COMP_HELPER.SetResultVar(seqRandomToVar, "true"));
        }

        private void EmitSequenceAssignRandomDoubleToVar(SequenceAssignRandomDoubleToVar seqRandomToVar, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()"));
            source.AppendFront(COMP_HELPER.SetResultVar(seqRandomToVar, "true"));
        }

        private void EmitSequenceAll(SequenceNAry seqAll, bool disjunction, bool lazy, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqAll, disjunction ? "false" : "true"));
            String continueDecisionName = "continue_" + seqAll.Id;
            source.AppendFrontFormat("bool {0} = true;\n", continueDecisionName);
            String sequencesToExecuteVarName = "sequencestoexecutevar_" + seqAll.Id;
            source.AppendFrontFormat("List<int> {0} = new List<int>({1});\n", sequencesToExecuteVarName, seqAll.Sequences.Count);
            String index = "i";
            source.AppendFrontFormat("for(int {0} = 0; {0} < {1}; ++{0})\n", index, seqAll.Sequences.Count);
            source.AppendFrontIndentedFormat("{0}.Add({1});\n", sequencesToExecuteVarName, index);
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
                String sequenceResult = COMP_HELPER.GetResultVar(seqAll) + (disjunction ? " || " : " && ") + COMP_HELPER.GetResultVar(seqAll.Sequences[i]);
                source.AppendFront(COMP_HELPER.SetResultVar(seqAll, sequenceResult));
                if(lazy)
                {
                    source.AppendFrontFormat("if({0}" + COMP_HELPER.GetResultVar(seqAll) + ")\n", disjunction ? "" : "!");
                    source.AppendFrontIndentedFormat("{0} = false;\n", continueDecisionName);
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

                source.AppendFront(COMP_HELPER.SetResultVar(seqWeighted, COMP_HELPER.GetResultVar(seqWeighted.Sequences[i])));
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        private void EmitSequenceSome(SequenceSomeFromSet seqSome, SourceBuilder source)
        {
            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqSome.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqSome.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seqSome.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            source.AppendFront(COMP_HELPER.SetResultVar(seqSome, "false"));

            // emit code for matching all the contained rules
            SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators = new SequenceRuleCallMatcherGenerator[seqSome.Sequences.Count];
            for(int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                ruleMatcherGenerators[i] = new SequenceRuleCallMatcherGenerator(seqRule, exprGen, seqHelper, fireDebugEvents);

                ruleMatcherGenerators[i].EmitMatchingAndCloning(source,
                    (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches"));
            }

            SequenceRuleCallMatcherGenerator.EmitPreMatchEventFiring(source, ruleMatcherGenerators, fireDebugEvents);

            for(int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                ruleMatcherGenerators[i].EmitFiltering(source);

                String matchesName = "matches_" + seqRule.Id;
                source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
                source.Indent();
                source.AppendFront(COMP_HELPER.SetResultVar(seqSome, "true"));
                source.AppendFront(COMP_HELPER.SetResultVar(seqRule, "true"));
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("if(" + COMP_HELPER.GetResultVar(seqSome) + ") {\n");
            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, ruleMatcherGenerators, false, "", fireDebugEvents);
            source.AppendFront("}\n");

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
            // emit code for rewriting all the contained rules which got matched
            for(int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceSomeRuleCallRewritingGenerator ruleGenerator = new SequenceSomeRuleCallRewritingGenerator(
                    seqSome, (SequenceRuleCall)seqSome.Sequences[i], exprGen, seqHelper, fireDebugEvents);
                ruleGenerator.EmitRewriting(source, totalMatchToApply, curTotalMatch);
            }

            source.AppendFront("if(" + COMP_HELPER.GetResultVar(seqSome) + ") {\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, ruleMatcherGenerators, fireDebugEvents);
            source.AppendFront("}\n");
            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "null", fireDebugEvents);
        }

        private void EmitSequenceMultiRuleAllCall(SequenceMultiRuleAllCall seqMulti, SourceBuilder source)
        {
            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqMulti.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqMulti.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seqMulti.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            source.AppendFront(COMP_HELPER.SetResultVar(seqMulti, "false"));

            String matchListName = "MatchList_" + seqMulti.Id;
            String matchToConstructIndexName = "MatchToConstructIndex_" + seqMulti.Id;
            source.AppendFrontFormat("List<GRGEN_LIBGR.IMatch> {0} = new List<GRGEN_LIBGR.IMatch>();\n", matchListName);
            source.AppendFrontFormat("Dictionary<GRGEN_LIBGR.IMatch, int> {0} = new Dictionary<GRGEN_LIBGR.IMatch, int>();\n", matchToConstructIndexName);

            // emit code for matching all the contained rules
            SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators = new SequenceRuleCallMatcherGenerator[seqMulti.Sequences.Count];
            for(int i = 0; i < seqMulti.Sequences.Count; ++i)
            {
                ruleMatcherGenerators[i] = new SequenceRuleCallMatcherGenerator((SequenceRuleCall)seqMulti.Sequences[i], exprGen, seqHelper, fireDebugEvents);
                ruleMatcherGenerators[i].EmitMatchingAndCloning(source, "procEnv.MaxMatches");
            }

            SequenceRuleCallMatcherGenerator.EmitPreMatchEventFiring(source, ruleMatcherGenerators, fireDebugEvents);

            // emit code for rule-based filtering
            for(int i = 0; i < seqMulti.Sequences.Count; ++i)
            {
                ruleMatcherGenerators[i].EmitFiltering(source);
                ruleMatcherGenerators[i].EmitToMatchListAdding(source, matchListName, matchToConstructIndexName, i);
                SequenceRuleCall seqRule = (SequenceRuleCall)seqMulti.Sequences[i];
                source.AppendFront(COMP_HELPER.SetResultVar(seqRule, "matches_" + seqRule.Id + ".Count != 0"));
            }

            // emit code for match class (non-rule-based) filtering
            foreach(SequenceFilterCallBase sequenceFilterCall in seqMulti.Filters)
            {
                exprGen.EmitMatchClassFilterCall(source, sequenceFilterCall, matchListName, false);
            }

            SequenceMultiRuleAllCallRewritingGenerator[] ruleRewritingGenerators = new SequenceMultiRuleAllCallRewritingGenerator[seqMulti.Sequences.Count];
            for(int i = 0; i < seqMulti.Sequences.Count; ++i)
            {
                ruleRewritingGenerators[i] = new SequenceMultiRuleAllCallRewritingGenerator(
                    seqMulti, (SequenceRuleCall)seqMulti.Sequences[i], exprGen, seqHelper, fireDebugEvents);
                if(ruleRewritingGenerators[i].returnParameterDeclarationsAllCall.Length != 0)
                    source.AppendFront(ruleRewritingGenerators[i].returnParameterDeclarationsAllCall + "\n");
            }

            // code to handle the rewrite next match
            source.AppendFront("if(" + matchListName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqMulti, "true"));

            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, ruleMatcherGenerators, seqMulti.Filters.Count > 0, matchListName, fireDebugEvents);

            // iterate through matches, use Modify on each, fire the next match event after the first
            String enumeratorName = "enum_" + seqMulti.Id;
            source.AppendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + enumeratorName + " = " + matchListName + ".GetEnumerator();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("switch({0}[" + enumeratorName + ".Current])\n", matchToConstructIndexName);
            source.AppendFront("{\n");
            source.Indent();

            // emit code for rewriting the current match (for each rule, rule fitting to the match is selected by rule name)
            for(int i = 0; i < seqMulti.Sequences.Count; ++i)
            {
                ruleRewritingGenerators[i].EmitRewriting(source, this, matchListName, enumeratorName, i);
            }

            source.AppendFrontFormat("default: throw new Exception(\"Unknown construct index of pattern \" + {0}.Current.Pattern.PackagePrefixedName + \" in match!\");", enumeratorName);
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            for(int i = 0; i < seqMulti.Sequences.Count; ++i)
            {
                if(ruleRewritingGenerators[i].returnAssignmentsAllCall.Length != 0)
                    source.AppendFront(ruleRewritingGenerators[i].returnAssignmentsAllCall + "\n");
            }

            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, ruleMatcherGenerators, fireDebugEvents);

            source.Unindent();
            source.AppendFront("}\n");

            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "null", fireDebugEvents);
        }

        private void EmitSequenceMultiRulePrefixedSequence(SequenceMultiRulePrefixedSequence seqMulti, SourceBuilder source)
        {
            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqMulti.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqMulti.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seqMulti.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            source.AppendFront(COMP_HELPER.SetResultVar(seqMulti, "false"));

            String matchListName = "MatchList_" + seqMulti.Id;
            String matchToConstructIndexName = "MatchToConstructIndex_" + seqMulti.Id;
            source.AppendFrontFormat("List<GRGEN_LIBGR.IMatch> {0} = new List<GRGEN_LIBGR.IMatch>();\n", matchListName);
            source.AppendFrontFormat("Dictionary<GRGEN_LIBGR.IMatch, int> {0} = new Dictionary<GRGEN_LIBGR.IMatch, int>();\n", matchToConstructIndexName);

            // emit code for matching all the contained rules
            SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators = new SequenceRuleCallMatcherGenerator[seqMulti.RulePrefixedSequences.Count];
            for(int i = 0; i < seqMulti.RulePrefixedSequences.Count; ++i)
            {
                SequenceRulePrefixedSequence seqRulePrefixedSequence = (SequenceRulePrefixedSequence)seqMulti.RulePrefixedSequences[i];
                ruleMatcherGenerators[i] = new SequenceRuleCallMatcherGenerator(seqRulePrefixedSequence.Rule, exprGen, seqHelper, fireDebugEvents);
                ruleMatcherGenerators[i].EmitMatchingAndCloning(source, "procEnv.MaxMatches");
            }

            SequenceRuleCallMatcherGenerator.EmitPreMatchEventFiring(source, ruleMatcherGenerators, fireDebugEvents);

            // emit code for rule-based filtering
            for(int i = 0; i < seqMulti.RulePrefixedSequences.Count; ++i)
            {
                ruleMatcherGenerators[i].EmitFiltering(source);
                ruleMatcherGenerators[i].EmitToMatchListAdding(source, matchListName, matchToConstructIndexName, i);
            }

            // emit code for match class (non-rule-based) filtering
            foreach(SequenceFilterCallBase sequenceFilterCall in seqMulti.Filters)
            {
                exprGen.EmitMatchClassFilterCall(source, sequenceFilterCall, matchListName, false);
            }

            // code to handle the rewrite next match
            source.AppendFront("if(" + matchListName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqMulti, "true"));

            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, ruleMatcherGenerators, seqMulti.Filters.Count > 0, matchListName, fireDebugEvents);

            // iterate through matches, use Modify on each, fire the next match event after the first
            String enumeratorName = "enum_" + seqMulti.Id;
            source.AppendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + enumeratorName + " = " + matchListName + ".GetEnumerator();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("switch({0}[" + enumeratorName + ".Current])\n", matchToConstructIndexName);
            source.AppendFront("{\n");
            source.Indent();

            // emit code for rewriting the current match (for each rule, rule fitting to the match is selected by rule name)
            for(int i = 0; i < seqMulti.RulePrefixedSequences.Count; ++i)
            {
                SequenceMultiRulePrefixedSequenceRewritingGenerator ruleRewritingGenerator = new SequenceMultiRulePrefixedSequenceRewritingGenerator(
                    seqMulti, (SequenceRulePrefixedSequence)seqMulti.RulePrefixedSequences[i], exprGen, seqHelper, fireDebugEvents);
                ruleRewritingGenerator.EmitRewriting(source, this, matchListName, enumeratorName, i);
            }

            source.AppendFrontFormat("default: throw new Exception(\"Unknown construct index of pattern \" + {0}.Current.Pattern.PackagePrefixedName + \" in match!\");", enumeratorName);
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, ruleMatcherGenerators, fireDebugEvents);

            source.Unindent();
            source.AppendFront("}\n");

            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "null", fireDebugEvents);
        }

        private void EmitSequenceTransaction(SequenceTransaction seqTrans, SourceBuilder source)
        {
            String transactionId = "transID_" + seqTrans.Id;
            source.AppendFront("int " + transactionId + " = procEnv.TransactionManager.Start();\n");
            EmitSequence(seqTrans.Seq, source);
            source.AppendFront("if(" + COMP_HELPER.GetResultVar(seqTrans.Seq) + ")\n");
            source.AppendFrontIndented("procEnv.TransactionManager.Commit(" + transactionId + ");\n");
            source.AppendFront("else\n");
            source.AppendFrontIndented("procEnv.TransactionManager.Rollback(" + transactionId + ");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqTrans, COMP_HELPER.GetResultVar(seqTrans.Seq)));
        }

        private void EmitSequencePause(SequencePause seqPause, SourceBuilder source)
        {
            source.AppendFront("procEnv.TransactionManager.Pause();\n");
            EmitSequence(seqPause.Seq, source);
            source.AppendFront("procEnv.TransactionManager.Resume();\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqPause, COMP_HELPER.GetResultVar(seqPause.Seq)));
        }

        private void EmitSequenceExecuteInSubgraph(SequenceExecuteInSubgraph seqExecInSub, SourceBuilder source)
        {
            string subgraph = exprGen.GetSequenceExpression(seqExecInSub.SubgraphExpr, source);
            source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + subgraph + ");\n");
            source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            EmitSequence(seqExecInSub.Seq, source);
            source.AppendFront("procEnv.ReturnFromSubgraph();\n");
            source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqExecInSub, COMP_HELPER.GetResultVar(seqExecInSub.Seq)));
        }

        private void EmitSequenceLock(SequenceLock seqLock, SourceBuilder source)
        {
            string lockObject = exprGen.GetSequenceExpression(seqLock.LockObjectExpr, source);
            source.AppendFront("lock(" + lockObject + ")\n");
            source.AppendFront("{\n");
            EmitSequence(seqLock.Seq, source);
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqLock, COMP_HELPER.GetResultVar(seqLock.Seq)));
        }

        private void EmitSequenceBooleanComputation(SequenceBooleanComputation seqComp, SourceBuilder source)
        {
            compGen.EmitSequenceComputation(seqComp.Computation, source);
            if(seqComp.Computation.ReturnsValue)
                source.AppendFront(COMP_HELPER.SetResultVar(seqComp, "!GRGEN_LIBGR.TypesHelper.IsDefaultValue(" + COMP_HELPER.GetResultVar(seqComp.Computation) + ")"));
            else
                source.AppendFront(COMP_HELPER.SetResultVar(seqComp, "true"));
        }

        public String GetSequenceResult(Sequence seq)
        {
            return COMP_HELPER.GetResultVar(seq);
        }
    }
}
