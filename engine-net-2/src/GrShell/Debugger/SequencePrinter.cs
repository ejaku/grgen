/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Diagnostics;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    static class SequencePrinter
    {
        /// <summary>
        /// Prints the given root sequence base according to the print context.
        /// Switches in between printing a sequence and a sequence expression.
        /// </summary>
        /// <param name="seq">The sequence base to be printed</param>
        /// <param name="context">The print context</param>
        /// <param name="nestingLevel">The level the sequence is nested in</param>
        public static void PrintSequenceBase(SequenceBase seqBase, PrintSequenceContext context, int nestingLevel)
        {
            if(seqBase is Sequence)
                PrintSequence((Sequence)seqBase, context, nestingLevel);
            else
                PrintSequenceExpression((SequenceExpression)seqBase, context, nestingLevel);
        }

        /// <summary>
        /// Prints the given root sequence adding parentheses if needed according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="context">The print context</param>
        /// <param name="nestingLevel">The level the sequence is nested in</param>
        public static void PrintSequence(Sequence seq, PrintSequenceContext context, int nestingLevel)
        {
            WorkaroundManager.Workaround.PrintHighlighted(nestingLevel + ">", HighlightingMode.SequenceStart);
            PrintSequence(seq, null, HighlightingMode.None, context);
        }

        /// <summary>
        /// Prints the given root sequence expression according to the print context.
        /// </summary>
        /// <param name="seqExpr">The sequence expression to be printed</param>
        /// <param name="context">The print context</param>
        public static void PrintSequenceExpression(SequenceExpression seqExpr, PrintSequenceContext context, int nestingLevel)
        {
            WorkaroundManager.Workaround.PrintHighlighted(nestingLevel + ">", HighlightingMode.SequenceStart);
            PrintSequenceExpression(seqExpr, null, HighlightingMode.None, context);
        }

        /// <summary>
        /// Prints the given sequence adding parentheses if needed according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="parent">The parent of the sequence or null if the sequence is a root</param>
        /// <param name="context">The print context</param>
        private static void PrintSequence(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence)
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);

            switch(seq.SequenceType)
            {
            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
            case SequenceType.StrictAnd:
                PrintSequenceBinary((SequenceBinary)seq, parent, highlightingMode, context);
                break;
            case SequenceType.IfThen:
                PrintSequenceIfThen((SequenceIfThen)seq, parent, highlightingMode, context);
                break;
            case SequenceType.Not:
                PrintSequenceNot((SequenceNot)seq, parent, highlightingMode, context);
                break;
            case SequenceType.IterationMin:
                PrintSequenceIterationMin((SequenceIterationMin)seq, parent, highlightingMode, context);
                break;
            case SequenceType.IterationMinMax:
                PrintSequenceIterationMinMax((SequenceIterationMinMax)seq, parent, highlightingMode, context);
                break;
            case SequenceType.Transaction:
                PrintSequenceTransaction((SequenceTransaction)seq, parent, highlightingMode, context);
                break;
            case SequenceType.Backtrack:
                PrintSequenceBacktrack((SequenceBacktrack)seq, parent, highlightingMode, context);
                break;
            case SequenceType.MultiBacktrack:
                PrintSequenceMultiBacktrack((SequenceMultiBacktrack)seq, parent, highlightingMode, context);
                break;
            case SequenceType.MultiSequenceBacktrack:
                PrintSequenceMultiSequenceBacktrack((SequenceMultiSequenceBacktrack)seq, parent, highlightingMode, context);
                break;
            case SequenceType.Pause:
                PrintSequencePause((SequencePause)seq, parent, highlightingMode, context);
                break;
            case SequenceType.ForContainer:
                PrintSequenceForContainer((SequenceForContainer)seq, parent, highlightingMode, context);
                break;
            case SequenceType.ForIntegerRange:
                PrintSequenceForIntegerRange((SequenceForIntegerRange)seq, parent, highlightingMode, context);
                break;
            case SequenceType.ForIndexAccessEquality:
                PrintSequenceForIndexAccessEquality((SequenceForIndexAccessEquality)seq, parent, highlightingMode, context);
                break;
            case SequenceType.ForIndexAccessOrdering:
                PrintSequenceForIndexAccessOrdering((SequenceForIndexAccessOrdering)seq, parent, highlightingMode, context);
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
            case SequenceType.ForBoundedReachableNodes:
            case SequenceType.ForBoundedReachableNodesViaIncoming:
            case SequenceType.ForBoundedReachableNodesViaOutgoing:
            case SequenceType.ForBoundedReachableEdges:
            case SequenceType.ForBoundedReachableEdgesViaIncoming:
            case SequenceType.ForBoundedReachableEdgesViaOutgoing:
            case SequenceType.ForNodes:
            case SequenceType.ForEdges:
                PrintSequenceForFunction((SequenceForFunction)seq, parent, highlightingMode, context);
                break;
            case SequenceType.ForMatch:
                PrintSequenceForMatch((SequenceForMatch)seq, parent, highlightingMode, context);
                break;
            case SequenceType.ExecuteInSubgraph:
                PrintSequenceExecuteInSubgraph((SequenceExecuteInSubgraph)seq, parent, highlightingMode, context);
                break;
            case SequenceType.ParallelExecute:
                PrintSequenceParallelExecute((SequenceParallelExecute)seq, parent, highlightingMode, context);
                break;
            case SequenceType.IfThenElse:
                PrintSequenceIfThenElse((SequenceIfThenElse)seq, parent, highlightingMode, context);
                break;
            case SequenceType.LazyOrAll:
            case SequenceType.LazyAndAll:
            case SequenceType.StrictOrAll:
            case SequenceType.StrictAndAll:
                PrintSequenceNAry((SequenceNAry)seq, parent, highlightingMode, context);
                break;
            case SequenceType.WeightedOne:
                PrintSequenceWeightedOne((SequenceWeightedOne)seq, parent, highlightingMode, context);
                break;
            case SequenceType.SomeFromSet:
                PrintSequenceSomeFromSet((SequenceSomeFromSet)seq, parent, highlightingMode, context);
                break;
            case SequenceType.MultiRulePrefixedSequence:
                PrintSequenceMultiRulePrefixedSequence((SequenceMultiRulePrefixedSequence)seq, parent, highlightingMode, context);
                break;
            case SequenceType.MultiRuleAllCall:
                PrintSequenceMultiRuleAllCall((SequenceMultiRuleAllCall)seq, parent, highlightingMode, context);
                break;
            case SequenceType.RulePrefixedSequence:
                PrintSequenceRulePrefixedSequence((SequenceRulePrefixedSequence)seq, parent, highlightingMode, context);
                break;
            case SequenceType.SequenceCall:
            case SequenceType.RuleCall:
            case SequenceType.RuleAllCall:
            case SequenceType.RuleCountAllCall:
            case SequenceType.BooleanComputation:
                PrintSequenceBreakpointable((Sequence)seq, parent, highlightingMode, context);
                break;
            case SequenceType.AssignSequenceResultToVar:
            case SequenceType.OrAssignSequenceResultToVar:
            case SequenceType.AndAssignSequenceResultToVar:
                PrintSequenceAssignSequenceResultToVar((SequenceAssignSequenceResultToVar)seq, parent, highlightingMode, context);
                break;
            case SequenceType.AssignUserInputToVar:
            case SequenceType.AssignRandomIntToVar:
            case SequenceType.AssignRandomDoubleToVar:
                PrintSequenceAssignChoiceHighlightable((Sequence)seq, parent, highlightingMode, context);
                break;
            case SequenceType.SequenceDefinitionInterpreted:
                PrintSequenceDefinitionInterpreted((SequenceDefinitionInterpreted)seq, parent, highlightingMode, context);
                break;
            // Atoms (assignments)
            case SequenceType.AssignVarToVar:
            case SequenceType.AssignConstToVar:
            case SequenceType.DeclareVariable:
                WorkaroundManager.Workaround.PrintHighlighted(seq.Symbol, highlightingMode);
                break;
            case SequenceType.AssignContainerConstructorToVar:
                PrintSequenceAssignContainerConstructorToVar((SequenceAssignContainerConstructorToVar)seq, parent, highlightingMode, context);
                break;
            default:
                Debug.Assert(false);
                Console.Write("<UNKNOWN_SEQUENCE_TYPE>");
                break;
            }

            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence)
                WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceBinary(SequenceBinary seqBin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0 && seqBin.Random)
            {
                int cpPosCounter = context.cpPosCounter;
                ++context.cpPosCounter;
                PrintSequence(seqBin.Left, seqBin, highlightingMode, context);
                PrintChoice(seqBin, context);
                WorkaroundManager.Workaround.PrintHighlighted(seqBin.OperatorSymbol + " ", highlightingMode);
                PrintSequence(seqBin.Right, seqBin, highlightingMode, context);
                return;
            }

            if(seqBin == context.highlightSeq && context.choice)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(l)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Left, seqBin, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("(l) " + seqBin.OperatorSymbol + " (r)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Right, seqBin, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("(r)", HighlightingMode.Choicepoint);
                return;
            }

            PrintSequence(seqBin.Left, seqBin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(" " + seqBin.OperatorSymbol + " ", highlightingMode);
            PrintSequence(seqBin.Right, seqBin, highlightingMode, context);
        }

        private static void PrintSequenceIfThen(SequenceIfThen seqIfThen, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("if{", highlightingMode);
            PrintSequence(seqIfThen.Left, seqIfThen, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqIfThen.Right, seqIfThen, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceNot(SequenceNot seqNot, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqNot.OperatorSymbol, highlightingMode);
            PrintSequence(seqNot.Seq, seqNot, highlightingMode, context);
        }

        private static void PrintSequenceIterationMin(SequenceIterationMin seqMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequence(seqMin.Seq, seqMin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("[" + seqMin.Min + ":*]", highlightingMode);
        }

        private static void PrintSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequence(seqMinMax.Seq, seqMinMax, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("[" + seqMinMax.Min + ":" + seqMinMax.Max + "]", highlightingMode);
        }

        private static void PrintSequenceTransaction(SequenceTransaction seqTrans, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("<", highlightingMode);
            PrintSequence(seqTrans.Seq, seqTrans, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(">", highlightingMode);
        }

        private static void PrintSequenceBacktrack(SequenceBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("<<", highlightingModeLocal);
            PrintSequence(seqBack.Rule, seqBack, highlightingModeLocal, context);
            WorkaroundManager.Workaround.PrintHighlighted(";;", highlightingModeLocal);
            PrintSequence(seqBack.Seq, seqBack, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(">>", highlightingModeLocal);
        }

        private static void PrintSequenceMultiBacktrack(SequenceMultiBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("<<", highlightingModeLocal);
            PrintSequence(seqBack.Rules, seqBack, highlightingModeLocal, context);
            WorkaroundManager.Workaround.PrintHighlighted(";;", highlightingModeLocal);
            PrintSequence(seqBack.Seq, seqBack, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(">>", highlightingModeLocal);
        }

        private static void PrintSequenceMultiSequenceBacktrack(SequenceMultiSequenceBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("<<", highlightingModeLocal);
            WorkaroundManager.Workaround.PrintHighlighted("[[", highlightingModeLocal);

            bool first = true;
            foreach(SequenceRulePrefixedSequence seqRulePrefixedSequence in seqBack.MultiRulePrefixedSequence.RulePrefixedSequences)
            {
                if(first)
                    first = false;
                else
                    WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence, context);
                WorkaroundManager.Workaround.PrintHighlighted(";", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("}", highlightingModeRulePrefixedSequence);
            }

            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqBack.MultiRulePrefixedSequence.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqBack.MultiRulePrefixedSequence, highlightingModeLocal, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
            WorkaroundManager.Workaround.PrintHighlighted(">>", highlightingModeLocal);
        }

        private static void PrintSequencePause(SequencePause seqPause, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("/", highlightingMode);
            PrintSequence(seqPause.Seq, seqPause, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("/", highlightingMode);
        }

        private static void PrintSequenceForContainer(SequenceForContainer seqFor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            if(seqFor.VarDst != null)
                WorkaroundManager.Workaround.PrintHighlighted("->" + seqFor.VarDst.Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(" in " + seqFor.Container.Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceForIntegerRange(SequenceForIntegerRange seqFor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(" in [", highlightingMode);
            PrintSequenceExpression(seqFor.Left, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(":", highlightingMode);
            PrintSequenceExpression(seqFor.Right, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceForIndexAccessEquality(SequenceForIndexAccessEquality seqFor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(" in {", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.IndexName, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("==", highlightingMode);
            PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering seqFor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(" in {", highlightingMode);
            if(seqFor.Ascending)
                WorkaroundManager.Workaround.PrintHighlighted("ascending", highlightingMode);
            else
                WorkaroundManager.Workaround.PrintHighlighted("descending", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
            if(seqFor.From() != null && seqFor.To() != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.IndexName, highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.IndexName, highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction2), highlightingMode);
                PrintSequenceExpression(seqFor.Expr2, seqFor, highlightingMode, context);
            }
            else if(seqFor.From() != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.IndexName, highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode, context);
            }
            else if(seqFor.To() != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.IndexName, highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode, context);
            }
            else
            {
                WorkaroundManager.Workaround.PrintHighlighted(seqFor.IndexName, highlightingMode);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("}; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceForFunction(SequenceForFunction seqFor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(" in ", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.FunctionSymbol, highlightingMode);
            PrintArguments(seqFor.ArgExprs, parent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceForMatch(SequenceForMatch seqFor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqFor == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingModeLocal);
            WorkaroundManager.Workaround.PrintHighlighted(seqFor.Var.Name, highlightingModeLocal);
            WorkaroundManager.Workaround.PrintHighlighted(" in [?", highlightingModeLocal);
            PrintSequence(seqFor.Rule, seqFor, highlightingModeLocal, context);
            WorkaroundManager.Workaround.PrintHighlighted("]; ", highlightingModeLocal);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingModeLocal);
        }

        private static void PrintSequenceExecuteInSubgraph(SequenceExecuteInSubgraph seqExecInSub, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("in ", highlightingMode);
            PrintSequenceExpression(seqExecInSub.SubgraphExpr, seqExecInSub, highlightingMode, context);
            if(seqExecInSub.ValueExpr != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExecInSub.ValueExpr, seqExecInSub, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(" {", highlightingMode);
            PrintSequence(seqExecInSub.Seq, seqExecInSub, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceParallelExecute(SequenceParallelExecute seqParallelExec, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqParallelExec == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("parallel", highlightingModeLocal);

            for(int i = 0; i < seqParallelExec.InSubgraphExecutions.Count; ++i)
            {
                SequenceExecuteInSubgraph seqExecInSub = seqParallelExec.InSubgraphExecutions[i];
                WorkaroundManager.Workaround.PrintHighlighted(" ", highlightingModeLocal);
                if(context.sequences != null)
                {
                    if(seqExecInSub == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                    if(seqExecInSub == context.sequences[i])
                        WorkaroundManager.Workaround.PrintHighlighted("(" + i + ")", HighlightingMode.Choicepoint);
                }
                PrintSequenceExecuteInSubgraph(seqExecInSub, seqParallelExec, highlightingModeLocal, context);
                if(context.sequences != null)
                {
                    if(seqExecInSub == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                }
            }
        }

        private static void PrintSequenceIfThenElse(SequenceIfThenElse seqIf, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("if{", highlightingMode);
            PrintSequence(seqIf.Condition, seqIf, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqIf.TrueCase, seqIf, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqIf.FalseCase, seqIf, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceNAry(SequenceNAry seqN, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0)
            {
                PrintChoice(seqN, context);
                ++context.cpPosCounter;
                WorkaroundManager.Workaround.PrintHighlighted((seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(", highlightingMode);
                bool first = true;
                foreach(Sequence seqChild in seqN.Children)
                {
                    if(!first)
                        WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                    PrintSequence(seqChild, seqN, highlightingMode, context);
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
                return;
            }

            bool highlight = false;
            foreach(Sequence seqChild in seqN.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            if(highlight && context.choice)
            {
                WorkaroundManager.Workaround.PrintHighlighted("$%" + seqN.OperatorSymbol + "(", HighlightingMode.Choicepoint);
                bool first = true;
                foreach(Sequence seqChild in seqN.Children)
                {
                    if(!first)
                        WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                    if(seqChild == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                    if(context.sequences != null)
                    {
                        for(int i = 0; i < context.sequences.Count; ++i)
                        {
                            if(seqChild == context.sequences[i])
                                WorkaroundManager.Workaround.PrintHighlighted("(" + i + ")", HighlightingMode.Choicepoint);
                        }
                    }

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    PrintSequence(seqChild, seqN, highlightingMode, context);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqChild == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                return;
            }

            WorkaroundManager.Workaround.PrintHighlighted((seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(", highlightingMode);
            PrintChildren(seqN, highlightingMode, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceWeightedOne(SequenceWeightedOne seqWeighted, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0)
            {
                PrintChoice(seqWeighted, context);
                ++context.cpPosCounter;
                WorkaroundManager.Workaround.PrintHighlighted((seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(", highlightingMode);
                bool first = true;
                for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                {
                    if(first)
                        WorkaroundManager.Workaround.PrintHighlighted("0.00 ", highlightingMode);
                    else
                        WorkaroundManager.Workaround.PrintHighlighted(" ", highlightingMode);
                    PrintSequence(seqWeighted.Sequences[i], seqWeighted, highlightingMode, context);
                    WorkaroundManager.Workaround.PrintHighlighted(" ", highlightingMode);
                    WorkaroundManager.Workaround.PrintHighlighted(seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture), highlightingMode); // todo: format auf 2 nachkommastellen 
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
                return;
            }

            bool highlight = false;
            foreach(Sequence seqChild in seqWeighted.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            if(highlight && context.choice)
            {
                WorkaroundManager.Workaround.PrintHighlighted("$%" + seqWeighted.OperatorSymbol + "(", HighlightingMode.Choicepoint);
                bool first = true;
                for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                {
                    if(first)
                        WorkaroundManager.Workaround.PrintHighlighted("0.00 ", highlightingMode);
                    else
                        WorkaroundManager.Workaround.PrintHighlighted(" ", highlightingMode);
                    if(seqWeighted.Sequences[i] == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    PrintSequence(seqWeighted.Sequences[i], seqWeighted, highlightingMode, context);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqWeighted.Sequences[i] == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    WorkaroundManager.Workaround.PrintHighlighted(" ", highlightingMode);
                    WorkaroundManager.Workaround.PrintHighlighted(seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture), highlightingMode); // todo: format auf 2 nachkommastellen 
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                return;
            }

            WorkaroundManager.Workaround.PrintHighlighted((seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(", highlightingMode);
            bool ffs = true;
            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
            {
                if(ffs)
                    WorkaroundManager.Workaround.PrintHighlighted("0.00 ", highlightingMode);
                else
                    WorkaroundManager.Workaround.PrintHighlighted(" ", highlightingMode);
                PrintSequence(seqWeighted.Sequences[i], seqWeighted, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(" ", highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture), highlightingMode); // todo: format auf 2 nachkommastellen 
                ffs = false;
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceSomeFromSet(SequenceSomeFromSet seqSome, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0
                && seqSome.Random)
            {
                PrintChoice(seqSome, context);
                ++context.cpPosCounter;
                WorkaroundManager.Workaround.PrintHighlighted(seqSome.Choice ? "$%{<" : "${<", highlightingMode);
                bool first = true;
                foreach(Sequence seqChild in seqSome.Children)
                {
                    if(!first)
                        WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                    int cpPosCounterBackup = context.cpPosCounter;
                    context.cpPosCounter = -1; // rules within some-from-set are not choicepointable
                    PrintSequence(seqChild, seqSome, highlightingMode, context);
                    context.cpPosCounter = cpPosCounterBackup;
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(")}", highlightingMode);
                return;
            }

            bool highlight = false;
            foreach(Sequence seqChild in seqSome.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            if(highlight && context.choice)
            {
                WorkaroundManager.Workaround.PrintHighlighted("$%{<", HighlightingMode.Choicepoint);
                bool first = true;
                int numCurTotalMatch = 0;
                foreach(Sequence seqChild in seqSome.Children)
                {
                    if(!first)
                        WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                    if(seqChild == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                    if(context.sequences != null)
                    {
                        for(int i = 0; i < context.sequences.Count; ++i)
                        {
                            if(seqChild == context.sequences[i] && context.matches[i].Count > 0)
                            {
                                PrintListOfMatchesNumbers(context, ref numCurTotalMatch, seqSome.IsNonRandomRuleAllCall(i) ? 1 : context.matches[i].Count);
                            }
                        }
                    }

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    PrintSequence(seqChild, seqSome, highlightingMode, context);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqChild == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(">}", HighlightingMode.Choicepoint);
                return;
            }

            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqSome == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted(seqSome.Random ? (seqSome.Choice ? "$%{<" : "${<") : "{<", highlightingModeLocal);
            PrintChildren(seqSome, highlightingMode, highlightingModeLocal, context);
            WorkaroundManager.Workaround.PrintHighlighted(">}", highlightingModeLocal);
        }

        private static void PrintSequenceMultiRulePrefixedSequence(SequenceMultiRulePrefixedSequence seqMulti, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqMulti == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("[[", highlightingModeLocal);

            bool first = true;
            foreach(SequenceRulePrefixedSequence seqRulePrefixedSequence in seqMulti.RulePrefixedSequences)
            {
                if(first)
                    first = false;
                else
                    WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence, context);
                WorkaroundManager.Workaround.PrintHighlighted(";", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("}", highlightingModeRulePrefixedSequence);
            }

            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqMulti, highlightingModeLocal, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
        }

        private static void PrintSequenceMultiRuleAllCall(SequenceMultiRuleAllCall seqMulti, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqMulti == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("[[", highlightingModeLocal);
            PrintChildren(seqMulti, highlightingMode, highlightingModeLocal, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqMulti, highlightingModeLocal, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
        }

        private static void PrintSequenceRulePrefixedSequence(SequenceRulePrefixedSequence seqRulePrefixedSequence, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqRulePrefixedSequence == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            if(!(parent is SequenceMultiRulePrefixedSequence))
                WorkaroundManager.Workaround.PrintHighlighted("[", highlightingModeLocal);

            WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingModeLocal);
            PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeLocal, context);
            WorkaroundManager.Workaround.PrintHighlighted(";", highlightingModeLocal);
            PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingModeLocal);

            if(!(parent is SequenceMultiRulePrefixedSequence))
                WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
        }

        private static void PrintSequenceBreakpointable(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(context.bpPosCounter >= 0)
            {
                PrintBreak((SequenceSpecial)seq, context);
                ++context.bpPosCounter;
            }

            if(context.cpPosCounter >= 0
                && seq is SequenceRandomChoice
                && ((SequenceRandomChoice)seq).Random)
            {
                PrintChoice((SequenceRandomChoice)seq, context);
                ++context.cpPosCounter;
            }

            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seq == context.highlightSeq)
            {
                if(context.choice)
                    highlightingModeLocal |= HighlightingMode.Choicepoint;
                else if(context.success)
                    highlightingModeLocal |= HighlightingMode.FocusSucces;
                else
                    highlightingModeLocal |= HighlightingMode.Focus;
            }
            if(seq.ExecutionState == SequenceExecutionState.Success)
                highlightingModeLocal |= HighlightingMode.LastSuccess;
            if(seq.ExecutionState == SequenceExecutionState.Fail)
                highlightingModeLocal |= HighlightingMode.LastFail;
            if(context.sequences != null && context.sequences.Contains(seq))
            {
                if(context.matches != null && context.matches[context.sequences.IndexOf(seq)].Count > 0)
                    highlightingModeLocal |= HighlightingMode.FocusSucces;
            }

            if(seq.Contains(context.highlightSeq) && seq != context.highlightSeq)
                PrintSequenceAtom(seq, parent, highlightingMode, context);
            else
                PrintSequenceAtom(seq, parent, highlightingModeLocal, context);
        }

        private static void PrintSequenceAtom(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            switch(seq.SequenceType)
            {
            case SequenceType.SequenceCall:
                PrintSequenceSequenceCall((SequenceSequenceCallInterpreted)seq, parent, highlightingMode, context);
                break;
            case SequenceType.RuleCall:
                PrintSequenceRuleCall((SequenceRuleCall)seq, parent, highlightingMode, context);
                break;
            case SequenceType.RuleAllCall:
                PrintSequenceRuleAllCall((SequenceRuleAllCall)seq, parent, highlightingMode, context);
                break;
            case SequenceType.RuleCountAllCall:
                PrintSequenceRuleCountAllCall((SequenceRuleCountAllCall)seq, parent, highlightingMode, context);
                break;
            case SequenceType.BooleanComputation:
                PrintSequenceBooleanComputation((SequenceBooleanComputation)seq, parent, highlightingMode, context);
                break;
            default:
                Debug.Assert(false);
                Console.Write("<UNKNOWN_SEQUENCE_TYPE>");
                break;
            }
        }

        private static void PrintSequenceSequenceCall(SequenceSequenceCallInterpreted seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(seq.Special)
                WorkaroundManager.Workaround.PrintHighlighted("%", highlightingMode); // TODO: questionable position here and in sequence -- should appear before sequence name, not return assignment
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode, context);
            if(seq.subgraph != null)
                WorkaroundManager.Workaround.PrintHighlighted(seq.subgraph.Name + ".", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seq.SequenceDef.Name, highlightingMode);
            if(seq.ArgumentExpressions.Length > 0)
            {
                PrintArguments(seq.ArgumentExpressions, parent, highlightingMode, context);
            }
        }

        private static void PrintArguments(SequenceExpression[] arguments, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
            for(int i = 0; i < arguments.Length; ++i)
            {
                PrintSequenceExpression(arguments[i], parent, highlightingMode, context);
                if(i != arguments.Length - 1)
                    WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintArguments(IList<SequenceExpression> arguments, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
            for(int i = 0; i < arguments.Count; ++i)
            {
                PrintSequenceExpression(arguments[i], parent, highlightingMode, context);
                if(i != arguments.Count - 1)
                    WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceRuleCall(SequenceRuleCall seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(seq.TestDebugPrefix, highlightingMode);
            PrintRuleCallString(seq, parent, highlightingMode, context);
        }

        private static void PrintSequenceRuleAllCall(SequenceRuleAllCall seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(seq.RandomChoicePrefix, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("[", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seq.TestDebugPrefix, highlightingMode);
            PrintRuleCallString(seq, parent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
        }

        private static void PrintSequenceRuleCountAllCall(SequenceRuleCountAllCall seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("count[", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seq.TestDebugPrefix, highlightingMode);
            PrintRuleCallString(seq, parent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]" + "=>" + seq.CountResult.Name, highlightingMode);
        }

        private static void PrintReturnAssignments(SequenceVariable[] returnVars, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(returnVars.Length > 0)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < returnVars.Length; ++i)
                {
                    WorkaroundManager.Workaround.PrintHighlighted(returnVars[i].Name, highlightingMode);
                    if(i != returnVars.Length - 1)
                        WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted(")=", highlightingMode);
            }
        }

        private static void PrintRuleCallString(SequenceRuleCall seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(seq.subgraph != null)
                WorkaroundManager.Workaround.PrintHighlighted(seq.subgraph.Name + ".", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seq.Name, highlightingMode);
            if(seq.ArgumentExpressions.Length > 0)
            {
                PrintArguments(seq.ArgumentExpressions, parent, highlightingMode, context);
            }
            for(int i = 0; i < seq.Filters.Count; ++i)
            {
                PrintSequenceFilterCall(seq.Filters[i], seq, highlightingMode, context);
            }
        }

        private static void PrintSequenceFilterCall(SequenceFilterCallBase seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("\\", highlightingMode);
            if(seq is SequenceFilterCallInterpreted)
            {
                SequenceFilterCallInterpreted filterCall = (SequenceFilterCallInterpreted)seq;
                if(filterCall.MatchClass != null)
                    WorkaroundManager.Workaround.PrintHighlighted(filterCall.MatchClass.info.PackagePrefixedName + ".", highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(filterCall.PackagePrefixedName, highlightingMode);
                PrintArguments(filterCall.ArgumentExpressions, parent, highlightingMode, context);
            }
            else if(seq is SequenceFilterCallLambdaExpressionInterpreted)
            {
                SequenceFilterCallLambdaExpressionInterpreted filterCall = (SequenceFilterCallLambdaExpressionInterpreted)seq;
                if(filterCall.MatchClass != null)
                    WorkaroundManager.Workaround.PrintHighlighted(filterCall.MatchClass.info.PackagePrefixedName + ".", highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(filterCall.Name, highlightingMode);
                //if(filterCall.Entity != null)
                //    sb.Append("<" + filterCall.Entity + ">");
                if(filterCall.FilterCall.initExpression != null)
                {
                    WorkaroundManager.Workaround.PrintHighlighted("{", highlightingMode);
                    if(filterCall.FilterCall.initArrayAccess != null)
                        WorkaroundManager.Workaround.PrintHighlighted(filterCall.FilterCall.initArrayAccess.Name + "; ", highlightingMode);
                    PrintSequenceExpression(filterCall.FilterCall.initExpression, parent, highlightingMode, context);
                    WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted("{", highlightingMode);
                if(filterCall.FilterCall.arrayAccess != null)
                    WorkaroundManager.Workaround.PrintHighlighted(filterCall.FilterCall.arrayAccess.Name + "; ", highlightingMode);
                if(filterCall.FilterCall.previousAccumulationAccess != null)
                    WorkaroundManager.Workaround.PrintHighlighted(filterCall.FilterCall.previousAccumulationAccess + ", ", highlightingMode);
                if(filterCall.FilterCall.index != null)
                    WorkaroundManager.Workaround.PrintHighlighted(filterCall.FilterCall.index.Name + " -> ", highlightingMode);
                WorkaroundManager.Workaround.PrintHighlighted(filterCall.FilterCall.element.Name + " -> ", highlightingMode);
                PrintSequenceExpression(filterCall.FilterCall.lambdaExpression, parent, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
            }
            else
            {
                Debug.Assert(false);
            }
        }

        private static void PrintSequenceBooleanComputation(SequenceBooleanComputation seqComp, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceComputation(seqComp.Computation, seqComp, highlightingMode, context);
        }

        private static void PrintSequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar seqAss, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
            PrintSequence(seqAss.Seq, seqAss, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(seqAss.OperatorSymbol, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqAss.DestVar.Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        // Choice highlightable user assignments
        private static void PrintSequenceAssignChoiceHighlightable(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0
                && (seq is SequenceAssignRandomIntToVar || seq is SequenceAssignRandomDoubleToVar))
            {
                PrintChoice((SequenceRandomChoice)seq, context);
                WorkaroundManager.Workaround.PrintHighlighted(seq.Symbol, highlightingMode);
                ++context.cpPosCounter;
                return;
            }

            if(seq == context.highlightSeq && context.choice)
                WorkaroundManager.Workaround.PrintHighlighted(seq.Symbol, HighlightingMode.Choicepoint);
            else
                WorkaroundManager.Workaround.PrintHighlighted(seq.Symbol, highlightingMode);
        }

        private static void PrintSequenceDefinitionInterpreted(SequenceDefinitionInterpreted seqDef, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = HighlightingMode.None;
            if(seqDef.ExecutionState == SequenceExecutionState.Success)
                highlightingModeLocal = HighlightingMode.LastSuccess;
            if(seqDef.ExecutionState == SequenceExecutionState.Fail)
                highlightingModeLocal = HighlightingMode.LastFail;

            WorkaroundManager.Workaround.PrintHighlighted(seqDef.Symbol + ": ", highlightingModeLocal);
            PrintSequence(seqDef.Seq, seqDef.Seq, highlightingMode, context);
        }

        private static void PrintSequenceAssignContainerConstructorToVar(SequenceAssignContainerConstructorToVar seq, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seq.DestVar + "=", highlightingMode);
            PrintSequenceExpression(seq.Constructor, seq, highlightingMode, context);
        }

        private static void PrintChildren(Sequence seq, HighlightingMode highlightingModeChildren, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            bool first = true;
            foreach(Sequence seqChild in seq.Children)
            {
                if(first)
                    first = false;
                else
                    WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                PrintSequence(seqChild, seq, highlightingModeChildren, context);
            }
        }

        private static void PrintChoice(SequenceRandomChoice seq, PrintSequenceContext context)
        {
            if(seq.Choice)
                WorkaroundManager.Workaround.PrintHighlighted("-%" + context.cpPosCounter + "-:", HighlightingMode.Choicepoint);
            else
                WorkaroundManager.Workaround.PrintHighlighted("+%" + context.cpPosCounter + "+:", HighlightingMode.Choicepoint);
        }

        private static void PrintBreak(ISequenceSpecial seq, PrintSequenceContext context)
        {
            if(seq.Special)
                WorkaroundManager.Workaround.PrintHighlighted("-%" + context.bpPosCounter + "-:", HighlightingMode.Breakpoint);
            else
                WorkaroundManager.Workaround.PrintHighlighted("+%" + context.bpPosCounter + "+:", HighlightingMode.Breakpoint);
        }

        private static void PrintListOfMatchesNumbers(PrintSequenceContext context, ref int numCurTotalMatch, int numMatches)
        {
            WorkaroundManager.Workaround.PrintHighlighted("(", HighlightingMode.Choicepoint);
            bool first = true;
            for(int i = 0; i < numMatches; ++i)
            {
                if(!first)
                    WorkaroundManager.Workaround.PrintHighlighted(",", HighlightingMode.Choicepoint);
                WorkaroundManager.Workaround.PrintHighlighted(numCurTotalMatch.ToString(), HighlightingMode.Choicepoint);
                ++numCurTotalMatch;
                first = false;
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
        }

        /// <summary>
        /// Called from shell after an debugging abort highlighting the lastly executed rule
        /// </summary>
        public static void PrintSequence(Sequence seq, Sequence highlight)
        {
            PrintSequenceContext context = new PrintSequenceContext();
            context.highlightSeq = highlight;
            PrintSequence(seq, context, 0);
            // TODO: what to do if abort came within sequence called from top sequence?
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void PrintSequenceComputation(SequenceComputation seqComp, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            switch(seqComp.SequenceComputationType)
            {
            case SequenceComputationType.Then:
                PrintSequenceComputationThen((SequenceComputationThen)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.VAlloc:
                PrintSequenceComputationVAlloc((SequenceComputationVAlloc)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.VFree:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.VFreeNonReset:
            case SequenceComputationType.VReset:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.ContainerAdd:
                PrintSequenceComputationContainerAdd((SequenceComputationContainerAdd)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.ContainerRem:
                PrintSequenceComputationContainerRem((SequenceComputationContainerRem)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.ContainerClear:
                PrintSequenceComputationContainerClear((SequenceComputationContainerClear)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.Assignment:
                PrintSequenceComputationAssignment((SequenceComputationAssignment)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.VariableDeclaration:
                PrintSequenceComputationVariableDeclaration((SequenceComputationVariableDeclaration)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.Emit:
                PrintSequenceComputationEmit((SequenceComputationEmit)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.Record:
                PrintSequenceComputationRecord((SequenceComputationRecord)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.Export:
                PrintSequenceComputationExport((SequenceComputationExport)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.DeleteFile:
                PrintSequenceComputationDeleteFile((SequenceComputationDeleteFile)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphAdd:
                PrintSequenceComputationGraphAdd((SequenceComputationGraphAdd)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphRem:
                PrintSequenceComputationGraphRem((SequenceComputationGraphRem)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphClear:
                PrintSequenceComputationGraphClear((SequenceComputationGraphClear)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphRetype:
                PrintSequenceComputationGraphRetype((SequenceComputationGraphRetype)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphAddCopy:
                PrintSequenceComputationGraphAddCopy((SequenceComputationGraphAddCopy)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphMerge:
                PrintSequenceComputationGraphMerge((SequenceComputationGraphMerge)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphRedirectSource:
                PrintSequenceComputationGraphRedirectSource((SequenceComputationGraphRedirectSource)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphRedirectTarget:
                PrintSequenceComputationGraphRedirectTarget((SequenceComputationGraphRedirectTarget)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.GraphRedirectSourceAndTarget:
                PrintSequenceComputationGraphRedirectSourceAndTarget((SequenceComputationGraphRedirectSourceAndTarget)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.Insert:
                PrintSequenceComputationInsert((SequenceComputationInsert)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.InsertCopy:
                PrintSequenceComputationInsertCopy((SequenceComputationInsertCopy)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.InsertInduced:
                PrintSequenceComputationInsertInduced((SequenceComputationInsertInduced)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.InsertDefined:
                PrintSequenceComputationInsertDefined((SequenceComputationInsertDefined)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.ProcedureCall:
                PrintSequenceComputationProcedureCall((SequenceComputationProcedureCall)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.BuiltinProcedureCall:
                PrintSequenceComputationBuiltinProcedureCall((SequenceComputationBuiltinProcedureCall)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.ProcedureMethodCall:
                PrintSequenceComputationProcedureMethodCall((SequenceComputationProcedureMethodCall)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.DebugAdd:
            case SequenceComputationType.DebugRem:
            case SequenceComputationType.DebugEmit:
            case SequenceComputationType.DebugHalt:
            case SequenceComputationType.DebugHighlight:
                PrintSequenceComputationDebug((SequenceComputationDebug)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.AssignmentTarget: // every assignment target (lhs value) is a computation
                PrintSequenceAssignmentTarget((AssignmentTarget)seqComp, parent, highlightingMode, context);
                break;
            case SequenceComputationType.Expression: // every expression (rhs value) is a computation
                PrintSequenceExpression((SequenceExpression)seqComp, parent, highlightingMode, context);
                break;
            default:
                Debug.Assert(false);
                break;
            }
        }

        private static void PrintSequenceComputationThen(SequenceComputationThen seqCompThen, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceComputation(seqCompThen.left, seqCompThen, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("; ", highlightingMode);
            if(seqCompThen.right is SequenceExpression)
            {
                WorkaroundManager.Workaround.PrintHighlighted("{", highlightingMode);
                PrintSequenceExpression((SequenceExpression)seqCompThen.right, seqCompThen, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
            }
            else
            {
                PrintSequenceComputation(seqCompThen.right, seqCompThen, highlightingMode, context);
            }
        }

        private static void PrintSequenceComputationVAlloc(SequenceComputationVAlloc seqCompVAlloc, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("valloc()", highlightingMode);
        }

        private static void PrintSequenceComputationVFree(SequenceComputationVFree seqCompVFree, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted((seqCompVFree.Reset ? "vfree" : "vfreenonreset") + "(", highlightingMode);
            PrintSequenceExpression(seqCompVFree.VisitedFlagExpression, seqCompVFree, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationVReset(SequenceComputationVReset seqCompVReset, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("vreset(", highlightingMode);
            PrintSequenceExpression(seqCompVReset.VisitedFlagExpression, seqCompVReset, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationContainerAdd(SequenceComputationContainerAdd seqCompContainerAdd, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqCompContainerAdd.Name + ".add(", highlightingMode);
            PrintSequenceExpression(seqCompContainerAdd.Expr, seqCompContainerAdd, highlightingMode, context);
            if(seqCompContainerAdd.ExprDst != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompContainerAdd.ExprDst, seqCompContainerAdd, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationContainerRem(SequenceComputationContainerRem seqCompContainerRem, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqCompContainerRem.Name + ".rem(", highlightingMode);
            if(seqCompContainerRem.Expr != null)
            {
                PrintSequenceExpression(seqCompContainerRem.Expr, seqCompContainerRem, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationContainerClear(SequenceComputationContainerClear seqCompContainerClear, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqCompContainerClear.Name + ".clear()", highlightingMode);
        }

        private static void PrintSequenceComputationAssignment(SequenceComputationAssignment seqCompAssign, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceAssignmentTarget(seqCompAssign.Target, seqCompAssign, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("=", highlightingMode);
            PrintSequenceComputation(seqCompAssign.SourceValueProvider, seqCompAssign, highlightingMode, context);
        }

        private static void PrintSequenceComputationVariableDeclaration(SequenceComputationVariableDeclaration seqCompVarDecl, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqCompVarDecl.Target.Name, highlightingMode);
        }

        private static void PrintSequenceComputationDebug(SequenceComputationDebug seqCompDebug, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Debug::" + seqCompDebug.Name + "(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression seqExpr in seqCompDebug.ArgExprs)
            {
                if(!first)
                    WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                else
                    first = false;
                PrintSequenceExpression(seqExpr, seqCompDebug, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationEmit(SequenceComputationEmit seqCompEmit, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(seqCompEmit.IsDebug)
                WorkaroundManager.Workaround.PrintHighlighted("emitdebug(", highlightingMode);
            else
                WorkaroundManager.Workaround.PrintHighlighted("emit(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression expr in seqCompEmit.Expressions)
            {
                if(first)
                    first = false;
                else
                    WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                SequenceExpressionConstant exprConst = expr as SequenceExpressionConstant;
                if(exprConst != null && exprConst.Constant is string)
                    WorkaroundManager.Workaround.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
                else
                    PrintSequenceExpression(expr, seqCompEmit, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationRecord(SequenceComputationRecord seqCompRec, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("record(", highlightingMode);

            SequenceExpressionConstant exprConst = seqCompRec.Expression as SequenceExpressionConstant;
            if(exprConst != null && exprConst.Constant is string)
                WorkaroundManager.Workaround.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
            else
                PrintSequenceExpression(seqCompRec.Expression, seqCompRec, highlightingMode, context);

            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationExport(SequenceComputationExport seqCompExport, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("File::export(", highlightingMode);
            if(seqCompExport.Graph != null)
            {
                PrintSequenceExpression(seqCompExport.Graph, seqCompExport, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
            }
            PrintSequenceExpression(seqCompExport.Name, seqCompExport, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationDeleteFile(SequenceComputationDeleteFile seqCompDelFile, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("File::deleteFile(", highlightingMode);
            PrintSequenceExpression(seqCompDelFile.Name, seqCompDelFile, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphAdd(SequenceComputationGraphAdd seqCompGraphAdd, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("add(", highlightingMode);
            PrintSequenceExpression(seqCompGraphAdd.Expr, seqCompGraphAdd, highlightingMode, context);
            if(seqCompGraphAdd.ExprSrc != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAdd.ExprSrc, seqCompGraphAdd, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAdd.ExprDst, seqCompGraphAdd, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphRem(SequenceComputationGraphRem seqCompGraphRem, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("rem(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRem.Expr, seqCompGraphRem, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphClear(SequenceComputationGraphClear seqCompGraphClear, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("clear()", highlightingMode);
        }

        private static void PrintSequenceComputationGraphRetype(SequenceComputationGraphRetype seqCompGraphRetype, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("retype(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRetype.ElemExpr, seqCompGraphRetype, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRetype.TypeExpr, seqCompGraphRetype, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy seqCompGraphAddCopy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqCompGraphAddCopy.Deep ? "addCopy(" : "addClone(", highlightingMode);
            PrintSequenceExpression(seqCompGraphAddCopy.Expr, seqCompGraphAddCopy, highlightingMode, context);
            if(seqCompGraphAddCopy.ExprSrc != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAddCopy.ExprSrc, seqCompGraphAddCopy, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAddCopy.ExprDst, seqCompGraphAddCopy, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphMerge(SequenceComputationGraphMerge seqCompGraphMerge, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("merge(", highlightingMode);
            PrintSequenceExpression(seqCompGraphMerge.TargetNodeExpr, seqCompGraphMerge, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqCompGraphMerge.SourceNodeExpr, seqCompGraphMerge, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphRedirectSource(SequenceComputationGraphRedirectSource seqCompGraphRedirectSrc, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("redirectSource(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrc.EdgeExpr, seqCompGraphRedirectSrc, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrc.SourceNodeExpr, seqCompGraphRedirectSrc, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphRedirectTarget(SequenceComputationGraphRedirectTarget seqCompGraphRedirectTgt, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("redirectSourceAndTarget(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectTgt.EdgeExpr, seqCompGraphRedirectTgt, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectTgt.TargetNodeExpr, seqCompGraphRedirectTgt, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationGraphRedirectSourceAndTarget(SequenceComputationGraphRedirectSourceAndTarget seqCompGraphRedirectSrcTgt, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("redirectSourceAndTarget(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.EdgeExpr, seqCompGraphRedirectSrcTgt, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.SourceNodeExpr, seqCompGraphRedirectSrcTgt, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.TargetNodeExpr, seqCompGraphRedirectSrcTgt, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationInsert(SequenceComputationInsert seqCompInsert, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("insert(", highlightingMode);
            PrintSequenceExpression(seqCompInsert.Graph, seqCompInsert, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationInsertCopy(SequenceComputationInsertCopy seqCompInsertCopy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("insert(", highlightingMode);
            PrintSequenceExpression(seqCompInsertCopy.Graph, seqCompInsertCopy, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompInsertCopy.RootNode, seqCompInsertCopy, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationInsertInduced(SequenceComputationInsertInduced seqCompInsertInduced, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("insertInduced(", highlightingMode);
            PrintSequenceExpression(seqCompInsertInduced.NodeSet, seqCompInsertInduced, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompInsertInduced.RootNode, seqCompInsertInduced, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationInsertDefined(SequenceComputationInsertDefined seqCompInsertDefined, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("insertDefined(", highlightingMode);
            PrintSequenceExpression(seqCompInsertDefined.EdgeSet, seqCompInsertDefined, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompInsertDefined.RootEdge, seqCompInsertDefined, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceComputationBuiltinProcedureCall(SequenceComputationBuiltinProcedureCall seqCompBuiltinProcCall, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(seqCompBuiltinProcCall.ReturnVars.Count > 0)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompBuiltinProcCall.ReturnVars.Count; ++i)
                {
                    WorkaroundManager.Workaround.PrintHighlighted(seqCompBuiltinProcCall.ReturnVars[i].Name, highlightingMode);
                    if(i != seqCompBuiltinProcCall.ReturnVars.Count - 1)
                        WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted(")=", highlightingMode);
            }
            PrintSequenceComputation(seqCompBuiltinProcCall.BuiltinProcedure, seqCompBuiltinProcCall, highlightingMode, context);
        }

        private static void PrintSequenceComputationProcedureCall(SequenceComputationProcedureCall seqCompProcCall, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(seqCompProcCall.ReturnVars.Length > 0)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompProcCall.ReturnVars.Length; ++i)
                {
                    WorkaroundManager.Workaround.PrintHighlighted(seqCompProcCall.ReturnVars[i].Name, highlightingMode);
                    if(i != seqCompProcCall.ReturnVars.Length - 1)
                        WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted(")=", highlightingMode);
            }
            WorkaroundManager.Workaround.PrintHighlighted(seqCompProcCall.Name, highlightingMode);
            if(seqCompProcCall.ArgumentExpressions.Length > 0)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompProcCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(seqCompProcCall.ArgumentExpressions[i], seqCompProcCall, highlightingMode, context);
                    if(i != seqCompProcCall.ArgumentExpressions.Length - 1)
                        WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
            }
        }

        private static void PrintSequenceComputationProcedureMethodCall(SequenceComputationProcedureMethodCall sequenceComputationProcedureMethodCall, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(sequenceComputationProcedureMethodCall.ReturnVars.Length > 0)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ReturnVars.Length; ++i)
                {
                    WorkaroundManager.Workaround.PrintHighlighted(sequenceComputationProcedureMethodCall.ReturnVars[i].Name, highlightingMode);
                    if(i != sequenceComputationProcedureMethodCall.ReturnVars.Length - 1)
                        WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted(")=", highlightingMode);
            }
            if(sequenceComputationProcedureMethodCall.TargetExpr != null)
            {
                PrintSequenceExpression(sequenceComputationProcedureMethodCall.TargetExpr, sequenceComputationProcedureMethodCall, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(".", highlightingMode);
            }
            if(sequenceComputationProcedureMethodCall.TargetVar != null)
                WorkaroundManager.Workaround.PrintHighlighted(sequenceComputationProcedureMethodCall.TargetVar.ToString() + ".", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(sequenceComputationProcedureMethodCall.Name, highlightingMode);
            if(sequenceComputationProcedureMethodCall.ArgumentExpressions.Length > 0)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(sequenceComputationProcedureMethodCall.ArgumentExpressions[i], sequenceComputationProcedureMethodCall, highlightingMode, context);
                    if(i != sequenceComputationProcedureMethodCall.ArgumentExpressions.Length - 1)
                        WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void PrintSequenceAssignmentTarget(AssignmentTarget assTgt, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            switch(assTgt.AssignmentTargetType)
            {
            case AssignmentTargetType.Var:
                PrintSequenceAssignmentTargetVar((AssignmentTargetVar)assTgt, parent, highlightingMode, context);
                break;
            case AssignmentTargetType.YieldingToVar:
                PrintSequenceAssignmentTargetYieldingVar((AssignmentTargetYieldingVar)assTgt, parent, highlightingMode, context);
                break;
            case AssignmentTargetType.IndexedVar:
                PrintSequenceAssignmentTargetIndexedVar((AssignmentTargetIndexedVar)assTgt, parent, highlightingMode, context);
                break;
            case AssignmentTargetType.Attribute:
                PrintSequenceAssignmentTargetAttribute((AssignmentTargetAttribute)assTgt, parent, highlightingMode, context);
                break;
            case AssignmentTargetType.AttributeIndexed:
                PrintSequenceAssignmentTargetAttributeIndexed((AssignmentTargetAttributeIndexed)assTgt, parent, highlightingMode, context);
                break;
            case AssignmentTargetType.Visited:
                PrintSequenceAssignmentTargetVisited((AssignmentTargetVisited)assTgt, parent, highlightingMode, context);
                break;
            }
        }

        private static void PrintSequenceAssignmentTargetVar(AssignmentTargetVar assTgtVar, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(assTgtVar.DestVar.Name, highlightingMode);
        }

        private static void PrintSequenceAssignmentTargetYieldingVar(AssignmentTargetYieldingVar assTgtYieldingVar, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(assTgtYieldingVar.DestVar.Name, highlightingMode);
        }

        private static void PrintSequenceAssignmentTargetIndexedVar(AssignmentTargetIndexedVar assTgtIndexedVar, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(assTgtIndexedVar.DestVar.Name + "[", highlightingMode);
            PrintSequenceExpression(assTgtIndexedVar.KeyExpression, assTgtIndexedVar, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
        }

        private static void PrintSequenceAssignmentTargetAttribute(AssignmentTargetAttribute assTgtAttribute, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(assTgtAttribute.DestVar.Name + "." + assTgtAttribute.AttributeName, highlightingMode);
        }

        private static void PrintSequenceAssignmentTargetAttributeIndexed(AssignmentTargetAttributeIndexed assTgtAttributeIndexed, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(assTgtAttributeIndexed.DestVar.Name + "." + assTgtAttributeIndexed.AttributeName + "[", highlightingMode);
            PrintSequenceExpression(assTgtAttributeIndexed.KeyExpression, assTgtAttributeIndexed, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
        }

        private static void PrintSequenceAssignmentTargetVisited(AssignmentTargetVisited assTgtVisited, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(assTgtVisited.GraphElementVar.Name + ".visited", highlightingMode);
            if(assTgtVisited.VisitedFlagExpression != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted("[", highlightingMode);
                PrintSequenceExpression(assTgtVisited.VisitedFlagExpression, assTgtVisited, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void PrintSequenceExpression(SequenceExpression seqExpr, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            if(context.bpPosCounter >= 0
                && seqExpr is ISequenceSpecial)
            {
                PrintBreak((ISequenceSpecial)seqExpr, context);
                ++context.bpPosCounter;
                return;
            }

            switch(seqExpr.SequenceExpressionType)
            {
            case SequenceExpressionType.Conditional:
                PrintSequenceExpressionConditional((SequenceExpressionConditional)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Except:
            case SequenceExpressionType.LazyOr:
            case SequenceExpressionType.LazyAnd:
            case SequenceExpressionType.StrictOr:
            case SequenceExpressionType.StrictXor:
            case SequenceExpressionType.StrictAnd:
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Not:
                PrintSequenceExpressionNot((SequenceExpressionNot)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.UnaryPlus:
                PrintSequenceExpressionUnaryPlus((SequenceExpressionUnaryPlus)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.UnaryMinus:
                PrintSequenceExpressionUnaryMinus((SequenceExpressionUnaryMinus)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.BitwiseComplement:
                PrintSequenceExpressionBitwiseComplement((SequenceExpressionBitwiseComplement)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Cast:
                PrintSequenceExpressionCast((SequenceExpressionCast)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Equal:
            case SequenceExpressionType.NotEqual:
            case SequenceExpressionType.Lower:
            case SequenceExpressionType.LowerEqual:
            case SequenceExpressionType.Greater:
            case SequenceExpressionType.GreaterEqual:
            case SequenceExpressionType.StructuralEqual:
            case SequenceExpressionType.ShiftLeft:
            case SequenceExpressionType.ShiftRight:
            case SequenceExpressionType.ShiftRightUnsigned:
            case SequenceExpressionType.Plus:
            case SequenceExpressionType.Minus:
            case SequenceExpressionType.Mul:
            case SequenceExpressionType.Div:
            case SequenceExpressionType.Mod:
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Constant:
                PrintSequenceExpressionConstant((SequenceExpressionConstant)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Variable:
                PrintSequenceExpressionVariable((SequenceExpressionVariable)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.This:
                PrintSequenceExpressionThis((SequenceExpressionThis)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.New:
                PrintSequenceExpressionNew((SequenceExpressionNew)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MatchClassConstructor:
                PrintSequenceExpressionMatchClassConstructor((SequenceExpressionMatchClassConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.SetConstructor:
                PrintSequenceExpressionSetConstructor((SequenceExpressionSetConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MapConstructor:
                PrintSequenceExpressionMapConstructor((SequenceExpressionMapConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayConstructor:
                PrintSequenceExpressionArrayConstructor((SequenceExpressionArrayConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.DequeConstructor:
                PrintSequenceExpressionDequeConstructor((SequenceExpressionDequeConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.SetCopyConstructor:
                PrintSequenceExpressionSetCopyConstructor((SequenceExpressionSetCopyConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MapCopyConstructor:
                PrintSequenceExpressionMapCopyConstructor((SequenceExpressionMapCopyConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayCopyConstructor:
                PrintSequenceExpressionArrayCopyConstructor((SequenceExpressionArrayCopyConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.DequeCopyConstructor:
                PrintSequenceExpressionDequeCopyConstructor((SequenceExpressionDequeCopyConstructor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ContainerAsArray:
                PrintSequenceExpressionContainerAsArray((SequenceExpressionContainerAsArray)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringLength:
                PrintSequenceExpressionStringLength((SequenceExpressionStringLength)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringStartsWith:
                PrintSequenceExpressionStringStartsWith((SequenceExpressionStringStartsWith)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringEndsWith:
                PrintSequenceExpressionStringEndsWith((SequenceExpressionStringEndsWith)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringSubstring:
                PrintSequenceExpressionStringSubstring((SequenceExpressionStringSubstring)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringReplace:
                PrintSequenceExpressionStringReplace((SequenceExpressionStringReplace)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringToLower:
                PrintSequenceExpressionStringToLower((SequenceExpressionStringToLower)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringToUpper:
                PrintSequenceExpressionStringToUpper((SequenceExpressionStringToUpper)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.StringAsArray:
                PrintSequenceExpressionStringAsArray((SequenceExpressionStringAsArray)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MapDomain:
                PrintSequenceExpressionMapDomain((SequenceExpressionMapDomain)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MapRange:
                PrintSequenceExpressionMapRange((SequenceExpressionMapRange)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Random:
                PrintSequenceExpressionRandom((SequenceExpressionRandom)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Def:
                PrintSequenceExpressionDef((SequenceExpressionDef)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.IsVisited:
                PrintSequenceExpressionIsVisited((SequenceExpressionIsVisited)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.InContainerOrString:
                PrintSequenceExpressionInContainerOrString((SequenceExpressionInContainerOrString)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ContainerEmpty:
                PrintSequenceExpressionContainerEmpty((SequenceExpressionContainerEmpty)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ContainerSize:
                PrintSequenceExpressionContainerSize((SequenceExpressionContainerSize)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ContainerAccess:
                PrintSequenceExpressionContainerAccess((SequenceExpressionContainerAccess)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ContainerPeek:
                PrintSequenceExpressionContainerPeek((SequenceExpressionContainerPeek)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringIndexOf((SequenceExpressionArrayOrDequeOrStringIndexOf)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringLastIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf((SequenceExpressionArrayOrDequeOrStringLastIndexOf)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayIndexOfOrdered:
                PrintSequenceExpressionArrayIndexOfOrdered((SequenceExpressionArrayIndexOfOrdered)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArraySum:
                PrintSequenceExpressionArraySum((SequenceExpressionArraySum)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayProd:
                PrintSequenceExpressionArrayProd((SequenceExpressionArrayProd)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayMin:
                PrintSequenceExpressionArrayMin((SequenceExpressionArrayMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayMax:
                PrintSequenceExpressionArrayMax((SequenceExpressionArrayMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayAvg:
                PrintSequenceExpressionArrayAvg((SequenceExpressionArrayAvg)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayMed:
                PrintSequenceExpressionArrayMed((SequenceExpressionArrayMed)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayMedUnordered:
                PrintSequenceExpressionArrayMedUnordered((SequenceExpressionArrayMedUnordered)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayVar:
                PrintSequenceExpressionArrayVar((SequenceExpressionArrayVar)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayDev:
                PrintSequenceExpressionArrayDev((SequenceExpressionArrayDev)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayAnd:
                PrintSequenceExpressionArrayAnd((SequenceExpressionArrayAnd)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOr:
                PrintSequenceExpressionArrayOr((SequenceExpressionArrayOr)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOrDequeAsSet:
                PrintSequenceExpressionArrayOrDequeAsSet((SequenceExpressionArrayOrDequeAsSet)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayAsMap:
                PrintSequenceExpressionArrayAsMap((SequenceExpressionArrayAsMap)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayAsDeque:
                PrintSequenceExpressionArrayAsDeque((SequenceExpressionArrayAsDeque)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayAsString:
                PrintSequenceExpressionArrayAsString((SequenceExpressionArrayAsString)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArraySubarray:
                PrintSequenceExpressionArraySubarray((SequenceExpressionArraySubarray)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.DequeSubdeque:
                PrintSequenceExpressionDequeSubdeque((SequenceExpressionDequeSubdeque)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOrderAscending:
                PrintSequenceExpressionArrayOrderAscending((SequenceExpressionArrayOrderAscending)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOrderDescending:
                PrintSequenceExpressionArrayOrderDescending((SequenceExpressionArrayOrderDescending)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayGroup:
                PrintSequenceExpressionArrayGroup((SequenceExpressionArrayGroup)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayKeepOneForEach:
                PrintSequenceExpressionArrayKeepOneForEach((SequenceExpressionArrayKeepOneForEach)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayReverse:
                PrintSequenceExpressionArrayReverse((SequenceExpressionArrayReverse)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayShuffle:
                PrintSequenceExpressionArrayShuffle((SequenceExpressionArrayShuffle)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayExtract:
                PrintSequenceExpressionArrayExtract((SequenceExpressionArrayExtract)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOrderAscendingBy:
                PrintSequenceExpressionArrayOrderAscendingBy((SequenceExpressionArrayOrderAscendingBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayOrderDescendingBy:
                PrintSequenceExpressionArrayOrderDescendingBy((SequenceExpressionArrayOrderDescendingBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayGroupBy:
                PrintSequenceExpressionArrayGroupBy((SequenceExpressionArrayGroupBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayKeepOneForEachBy:
                PrintSequenceExpressionArrayKeepOneForEachBy((SequenceExpressionArrayKeepOneForEachBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayIndexOfBy:
                PrintSequenceExpressionArrayIndexOfBy((SequenceExpressionArrayIndexOfBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayLastIndexOfBy:
                PrintSequenceExpressionArrayLastIndexOfBy((SequenceExpressionArrayLastIndexOfBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayIndexOfOrderedBy:
                PrintSequenceExpressionArrayIndexOfOrderedBy((SequenceExpressionArrayIndexOfOrderedBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayMap:
                PrintSequenceExpressionArrayMap((SequenceExpressionArrayMap)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayRemoveIf:
                PrintSequenceExpressionArrayRemoveIf((SequenceExpressionArrayRemoveIf)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ArrayMapStartWithAccumulateBy:
                PrintSequenceExpressionArrayMapStartWithAccumulateBy((SequenceExpressionArrayMapStartWithAccumulateBy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ElementFromGraph:
                PrintSequenceExpressionElementFromGraph((SequenceExpressionElementFromGraph)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.NodeByName:
                PrintSequenceExpressionNodeByName((SequenceExpressionNodeByName)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.EdgeByName:
                PrintSequenceExpressionEdgeByName((SequenceExpressionEdgeByName)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.NodeByUnique:
                PrintSequenceExpressionNodeByUnique((SequenceExpressionNodeByUnique)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.EdgeByUnique:
                PrintSequenceExpressionEdgeByUnique((SequenceExpressionEdgeByUnique)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Source:
                PrintSequenceExpressionSource((SequenceExpressionSource)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Target:
                PrintSequenceExpressionTarget((SequenceExpressionTarget)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Opposite:
                PrintSequenceExpressionOpposite((SequenceExpressionOpposite)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.GraphElementAttributeOrElementOfMatch:
                PrintSequenceExpressionAttributeOrMatchAccess((SequenceExpressionAttributeOrMatchAccess)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.GraphElementAttribute:
                PrintSequenceExpressionAttributeAccess((SequenceExpressionAttributeAccess)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ElementOfMatch:
                PrintSequenceExpressionMatchAccess((SequenceExpressionMatchAccess)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Nodes:
                PrintSequenceExpressionNodes((SequenceExpressionNodes)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Edges:
                PrintSequenceExpressionEdges((SequenceExpressionEdges)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.CountNodes:
                PrintSequenceExpressionCountNodes((SequenceExpressionCountNodes)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.CountEdges:
                PrintSequenceExpressionCountEdges((SequenceExpressionCountEdges)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Now:
                PrintSequenceExpressionNow((SequenceExpressionNow)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathMin:
                PrintSequenceExpressionMathMin((SequenceExpressionMathMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathMax:
                PrintSequenceExpressionMathMax((SequenceExpressionMathMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathAbs:
                PrintSequenceExpressionMathAbs((SequenceExpressionMathAbs)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathCeil:
                PrintSequenceExpressionMathCeil((SequenceExpressionMathCeil)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathFloor:
                PrintSequenceExpressionMathFloor((SequenceExpressionMathFloor)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathRound:
                PrintSequenceExpressionMathRound((SequenceExpressionMathRound)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathTruncate:
                PrintSequenceExpressionMathTruncate((SequenceExpressionMathTruncate)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathSqr:
                PrintSequenceExpressionMathSqr((SequenceExpressionMathSqr)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathSqrt:
                PrintSequenceExpressionMathSqrt((SequenceExpressionMathSqrt)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathPow:
                PrintSequenceExpressionMathPow((SequenceExpressionMathPow)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathLog:
                PrintSequenceExpressionMathLog((SequenceExpressionMathLog)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathSgn:
                PrintSequenceExpressionMathSgn((SequenceExpressionMathSgn)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathSin:
                PrintSequenceExpressionMathSin((SequenceExpressionMathSin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathCos:
                PrintSequenceExpressionMathCos((SequenceExpressionMathCos)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathTan:
                PrintSequenceExpressionMathTan((SequenceExpressionMathTan)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathArcSin:
                PrintSequenceExpressionMathArcSin((SequenceExpressionMathArcSin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathArcCos:
                PrintSequenceExpressionMathArcCos((SequenceExpressionMathArcCos)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathArcTan:
                PrintSequenceExpressionMathArcTan((SequenceExpressionMathArcTan)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathPi:
                PrintSequenceExpressionMathPi((SequenceExpressionMathPi)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathE:
                PrintSequenceExpressionMathE((SequenceExpressionMathE)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathByteMin:
                PrintSequenceExpressionMathByteMin((SequenceExpressionMathByteMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathByteMax:
                PrintSequenceExpressionMathByteMax((SequenceExpressionMathByteMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathShortMin:
                PrintSequenceExpressionMathShortMin((SequenceExpressionMathShortMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathShortMax:
                PrintSequenceExpressionMathShortMax((SequenceExpressionMathShortMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathIntMin:
                PrintSequenceExpressionMathIntMin((SequenceExpressionMathIntMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathIntMax:
                PrintSequenceExpressionMathIntMax((SequenceExpressionMathIntMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathLongMin:
                PrintSequenceExpressionMathLongMin((SequenceExpressionMathLongMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathLongMax:
                PrintSequenceExpressionMathLongMax((SequenceExpressionMathLongMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathFloatMin:
                PrintSequenceExpressionMathFloatMin((SequenceExpressionMathFloatMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathFloatMax:
                PrintSequenceExpressionMathFloatMax((SequenceExpressionMathFloatMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathDoubleMin:
                PrintSequenceExpressionMathDoubleMin((SequenceExpressionMathDoubleMin)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MathDoubleMax:
                PrintSequenceExpressionMathDoubleMax((SequenceExpressionMathDoubleMax)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Empty:
                PrintSequenceExpressionEmpty((SequenceExpressionEmpty)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Size:
                PrintSequenceExpressionSize((SequenceExpressionSize)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.AdjacentNodes:
            case SequenceExpressionType.AdjacentNodesViaIncoming:
            case SequenceExpressionType.AdjacentNodesViaOutgoing:
            case SequenceExpressionType.IncidentEdges:
            case SequenceExpressionType.IncomingEdges:
            case SequenceExpressionType.OutgoingEdges:
                PrintSequenceExpressionAdjacentIncident((SequenceExpressionAdjacentIncident)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ReachableNodes:
            case SequenceExpressionType.ReachableNodesViaIncoming:
            case SequenceExpressionType.ReachableNodesViaOutgoing:
            case SequenceExpressionType.ReachableEdges:
            case SequenceExpressionType.ReachableEdgesViaIncoming:
            case SequenceExpressionType.ReachableEdgesViaOutgoing:
                PrintSequenceExpressionReachable((SequenceExpressionReachable)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.BoundedReachableNodes:
            case SequenceExpressionType.BoundedReachableNodesViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.BoundedReachableEdges:
            case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionBoundedReachable((SequenceExpressionBoundedReachable)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                PrintSequenceExpressionBoundedReachableWithRemainingDepth((SequenceExpressionBoundedReachableWithRemainingDepth)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.CountAdjacentNodes:
            case SequenceExpressionType.CountAdjacentNodesViaIncoming:
            case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
            case SequenceExpressionType.CountIncidentEdges:
            case SequenceExpressionType.CountIncomingEdges:
            case SequenceExpressionType.CountOutgoingEdges:
                PrintSequenceExpressionCountAdjacentIncident((SequenceExpressionCountAdjacentIncident)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.CountReachableNodes:
            case SequenceExpressionType.CountReachableNodesViaIncoming:
            case SequenceExpressionType.CountReachableNodesViaOutgoing:
            case SequenceExpressionType.CountReachableEdges:
            case SequenceExpressionType.CountReachableEdgesViaIncoming:
            case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountReachable((SequenceExpressionCountReachable)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.CountBoundedReachableNodes:
            case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.CountBoundedReachableEdges:
            case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountBoundedReachable((SequenceExpressionCountBoundedReachable)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.IsAdjacentNodes:
            case SequenceExpressionType.IsAdjacentNodesViaIncoming:
            case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
            case SequenceExpressionType.IsIncidentEdges:
            case SequenceExpressionType.IsIncomingEdges:
            case SequenceExpressionType.IsOutgoingEdges:
                PrintSequenceExpressionIsAdjacentIncident((SequenceExpressionIsAdjacentIncident)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.IsReachableNodes:
            case SequenceExpressionType.IsReachableNodesViaIncoming:
            case SequenceExpressionType.IsReachableNodesViaOutgoing:
            case SequenceExpressionType.IsReachableEdges:
            case SequenceExpressionType.IsReachableEdgesViaIncoming:
            case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsReachable((SequenceExpressionIsReachable)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.IsBoundedReachableNodes:
            case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.IsBoundedReachableEdges:
            case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsBoundedReachable((SequenceExpressionIsBoundedReachable)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.InducedSubgraph:
                PrintSequenceExpressionInducedSubgraph((SequenceExpressionInducedSubgraph)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.DefinedSubgraph:
                PrintSequenceExpressionDefinedSubgraph((SequenceExpressionDefinedSubgraph)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.EqualsAny:
                PrintSequenceExpressionEqualsAny((SequenceExpressionEqualsAny)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.GetEquivalent:
                PrintSequenceExpressionGetEquivalent((SequenceExpressionGetEquivalent)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Nameof:
                PrintSequenceExpressionNameof((SequenceExpressionNameof)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Uniqueof:
                PrintSequenceExpressionUniqueof((SequenceExpressionUniqueof)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Typeof:
                PrintSequenceExpressionTypeof((SequenceExpressionTypeof)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.ExistsFile:
                PrintSequenceExpressionExistsFile((SequenceExpressionExistsFile)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Import:
                PrintSequenceExpressionImport((SequenceExpressionImport)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Copy:
                PrintSequenceExpressionCopy((SequenceExpressionCopy)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Canonize:
                PrintSequenceExpressionCanonize((SequenceExpressionCanonize)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.RuleQuery:
                PrintSequenceExpressionRuleQuery((SequenceExpressionRuleQuery)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MultiRuleQuery:
                PrintSequenceExpressionMultiRuleQuery((SequenceExpressionMultiRuleQuery)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.MappingClause:
                PrintSequenceExpressionMappingClause((SequenceExpressionMappingClause)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.Scan:
                PrintSequenceExpressionScan((SequenceExpressionScan)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.TryScan:
                PrintSequenceExpressionTryScan((SequenceExpressionTryScan)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.FunctionCall:
                PrintSequenceExpressionFunctionCall((SequenceExpressionFunctionCall)seqExpr, parent, highlightingMode, context);
                break;
            case SequenceExpressionType.FunctionMethodCall:
                PrintSequenceExpressionFunctionMethodCall((SequenceExpressionFunctionMethodCall)seqExpr, parent, highlightingMode, context);
                break;
            }
        }

        private static void PrintSequenceExpressionConditional(SequenceExpressionConditional seqExprCond, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprCond.Condition, seqExprCond, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(" ? ", highlightingMode);
            PrintSequenceExpression(seqExprCond.TrueCase, seqExprCond, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(" : ", highlightingMode);
            PrintSequenceExpression(seqExprCond.FalseCase, seqExprCond, highlightingMode, context);
        }

        private static void PrintSequenceExpressionBinary(SequenceBinaryExpression seqExprBin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprBin.Left, seqExprBin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprBin.Operator, highlightingMode);
            PrintSequenceExpression(seqExprBin.Right, seqExprBin, highlightingMode, context);
        }

        private static void PrintSequenceExpressionNot(SequenceExpressionNot seqExprNot, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("!", highlightingMode);
            PrintSequenceExpression(seqExprNot.Operand, seqExprNot, highlightingMode, context);
        }

        private static void PrintSequenceExpressionUnaryPlus(SequenceExpressionUnaryPlus seqExprUnaryPlus, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("+", highlightingMode);
            PrintSequenceExpression(seqExprUnaryPlus.Operand, seqExprUnaryPlus, highlightingMode, context);
        }

        private static void PrintSequenceExpressionUnaryMinus(SequenceExpressionUnaryMinus seqExprUnaryMinus, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("-", highlightingMode);
            PrintSequenceExpression(seqExprUnaryMinus.Operand, seqExprUnaryMinus, highlightingMode, context);
        }

        private static void PrintSequenceExpressionBitwiseComplement(SequenceExpressionBitwiseComplement seqExprBitwiseComplement, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("~", highlightingMode);
            PrintSequenceExpression(seqExprBitwiseComplement.Operand, seqExprBitwiseComplement, highlightingMode, context);
        }

        private static void PrintSequenceExpressionCast(SequenceExpressionCast seqExprCast, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(((InheritanceType)seqExprCast.TargetType).Name, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
            PrintSequenceExpression(seqExprCast.Operand, seqExprCast, highlightingMode, context);
        }

        private static void PrintSequenceExpressionConstant(SequenceExpressionConstant seqExprConstant, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(seqExprConstant.Constant), highlightingMode);
        }

        private static void PrintSequenceExpressionVariable(SequenceExpressionVariable seqExprVariable, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprVariable.Variable.Name, highlightingMode);
        }

        private static void PrintSequenceExpressionNew(SequenceExpressionNew seqExprNew, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprNew.ConstructedType, highlightingMode); // TODO: check -- looks suspicious
        }

        private static void PrintSequenceExpressionThis(SequenceExpressionThis seqExprThis, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("this", highlightingMode);
        }

        private static void PrintSequenceExpressionMatchClassConstructor(SequenceExpressionMatchClassConstructor seqExprMatchClassConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("match<class " + seqExprMatchClassConstructor.ConstructedType + ">()", highlightingMode);
        }

        private static void PrintSequenceExpressionSetConstructor(SequenceExpressionSetConstructor seqExprSetConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("set<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprSetConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">{", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprSetConstructor, seqExprSetConstructor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceExpressionMapConstructor(SequenceExpressionMapConstructor seqExprMapConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("map<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprMapConstructor.KeyType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprMapConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">{", highlightingMode);
            for(int i = 0; i < seqExprMapConstructor.MapKeyItems.Length; ++i)
            {
                PrintSequenceExpression(seqExprMapConstructor.MapKeyItems[i], seqExprMapConstructor, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("->", highlightingMode);
                PrintSequenceExpression(seqExprMapConstructor.ContainerItems[i], seqExprMapConstructor, highlightingMode, context);
                if(i != seqExprMapConstructor.MapKeyItems.Length - 1)
                    WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            }
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayConstructor(SequenceExpressionArrayConstructor seqExprArrayConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("array<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprArrayConstructor, seqExprArrayConstructor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
        }

        private static void PrintSequenceExpressionDequeConstructor(SequenceExpressionDequeConstructor seqExprDequeConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("deque<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprDequeConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">]", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprDequeConstructor, seqExprDequeConstructor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("[", highlightingMode);
        }

        private static void PrintSequenceExpressionContainerConstructor(SequenceExpressionContainerConstructor seqExprContainerConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            for(int i = 0; i < seqExprContainerConstructor.ContainerItems.Length; ++i)
            {
                PrintSequenceExpression(seqExprContainerConstructor.ContainerItems[i], seqExprContainerConstructor, highlightingMode, context);
                if(i != seqExprContainerConstructor.ContainerItems.Length - 1)
                    WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            }
        }

        private static void PrintSequenceExpressionSetCopyConstructor(SequenceExpressionSetCopyConstructor seqExprSetCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("set<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprSetCopyConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">(", highlightingMode);
            PrintSequenceExpression(seqExprSetCopyConstructor.SetToCopy, seqExprSetCopyConstructor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMapCopyConstructor(SequenceExpressionMapCopyConstructor seqExprMapCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("map<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprMapCopyConstructor.KeyType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprMapCopyConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">(", highlightingMode);
            PrintSequenceExpression(seqExprMapCopyConstructor.MapToCopy, seqExprMapCopyConstructor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayCopyConstructor(SequenceExpressionArrayCopyConstructor seqExprArrayCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("array<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayCopyConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpression(seqExprArrayCopyConstructor.ArrayToCopy, seqExprArrayCopyConstructor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
        }

        private static void PrintSequenceExpressionDequeCopyConstructor(SequenceExpressionDequeCopyConstructor seqExprDequeCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("deque<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprDequeCopyConstructor.ValueType, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpression(seqExprDequeCopyConstructor.DequeToCopy, seqExprDequeCopyConstructor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
        }

        private static void PrintSequenceExpressionContainerAsArray(SequenceExpressionContainerAsArray seqExprContainerAsArray, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprContainerAsArray.Name + ".asArray()", highlightingMode);
        }

        private static void PrintSequenceExpressionStringLength(SequenceExpressionStringLength seqExprStringLength, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringLength.StringExpr, seqExprStringLength, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".length()", highlightingMode);
        }

        private static void PrintSequenceExpressionStringStartsWith(SequenceExpressionStringStartsWith seqExprStringStartsWith, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringStartsWith.StringExpr, seqExprStringStartsWith, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".startsWith(", highlightingMode);
            PrintSequenceExpression(seqExprStringStartsWith.StringToSearchForExpr, seqExprStringStartsWith, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionStringEndsWith(SequenceExpressionStringEndsWith seqExprStringEndsWith, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringEndsWith.StringExpr, seqExprStringEndsWith, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".endsWith(", highlightingMode);
            PrintSequenceExpression(seqExprStringEndsWith.StringToSearchForExpr, seqExprStringEndsWith, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionStringSubstring(SequenceExpressionStringSubstring seqExprStringSubstring, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringSubstring.StringExpr, seqExprStringSubstring, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".substring(", highlightingMode);
            PrintSequenceExpression(seqExprStringSubstring.StartIndexExpr, seqExprStringSubstring, highlightingMode, context);
            if(seqExprStringSubstring.LengthExpr != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprStringSubstring.LengthExpr, seqExprStringSubstring, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionStringReplace(SequenceExpressionStringReplace seqExprStringReplace, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringReplace.StringExpr, seqExprStringReplace, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".replace(", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.StartIndexExpr, seqExprStringReplace, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.LengthExpr, seqExprStringReplace, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.ReplaceStringExpr, seqExprStringReplace, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionStringToLower(SequenceExpressionStringToLower seqExprStringToLower, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringToLower.StringExpr, seqExprStringToLower, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".toLower()", highlightingMode);
        }

        private static void PrintSequenceExpressionStringToUpper(SequenceExpressionStringToUpper seqExprStringToUpper, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringToUpper.StringExpr, seqExprStringToUpper, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".toUpper()", highlightingMode);
        }

        private static void PrintSequenceExpressionStringAsArray(SequenceExpressionStringAsArray seqExprStringAsArray, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringAsArray.StringExpr, seqExprStringAsArray, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".asArray(", highlightingMode);
            PrintSequenceExpression(seqExprStringAsArray.SeparatorExpr, seqExprStringAsArray, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionRandom(SequenceExpressionRandom seqExprRandom, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("random(", highlightingMode);
            if(seqExprRandom.UpperBound != null)
                PrintSequenceExpression(seqExprRandom.UpperBound, seqExprRandom, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionDef(SequenceExpressionDef seqExprDef, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("def(", highlightingMode);
            for(int i = 0; i < seqExprDef.DefVars.Length; ++i)
            {
                PrintSequenceExpression(seqExprDef.DefVars[i], seqExprDef, highlightingMode, context);
                if(i != seqExprDef.DefVars.Length - 1)
                    WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionIsVisited(SequenceExpressionIsVisited seqExprIsVisited, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprIsVisited.GraphElementVarExpr, seqExprIsVisited, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".visited", highlightingMode);
            if(seqExprIsVisited.VisitedFlagExpr != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted("[", highlightingMode);
                PrintSequenceExpression(seqExprIsVisited.VisitedFlagExpr, seqExprIsVisited, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
            }
        }

        private static void PrintSequenceExpressionInContainerOrString(SequenceExpressionInContainerOrString seqExprInContainerOrString, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprInContainerOrString.Expr, seqExprInContainerOrString, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(" in ", highlightingMode);
            PrintSequenceExpression(seqExprInContainerOrString.ContainerOrStringExpr, seqExprInContainerOrString, highlightingMode, context);
        }

        private static void PrintSequenceExpressionContainerSize(SequenceExpressionContainerSize seqExprContainerSize, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerSize.ContainerExpr, seqExprContainerSize, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".size()", highlightingMode);
        }

        private static void PrintSequenceExpressionContainerEmpty(SequenceExpressionContainerEmpty seqExprContainerEmpty, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerEmpty.ContainerExpr, seqExprContainerEmpty, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".empty()", highlightingMode);
        }

        private static void PrintSequenceExpressionContainerAccess(SequenceExpressionContainerAccess seqExprContainerAccess, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerAccess.ContainerExpr, seqExprContainerAccess, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("[", highlightingMode);
            PrintSequenceExpression(seqExprContainerAccess.KeyExpr, seqExprContainerAccess, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingMode);
        }

        private static void PrintSequenceExpressionContainerPeek(SequenceExpressionContainerPeek seqExprContainerPeek, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerPeek.ContainerExpr, seqExprContainerPeek, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".peek(", highlightingMode);
            if(seqExprContainerPeek.KeyExpr != null)
                PrintSequenceExpression(seqExprContainerPeek.KeyExpr, seqExprContainerPeek, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOrDequeOrStringIndexOf(SequenceExpressionArrayOrDequeOrStringIndexOf seqExprArrayOrDequeOrStringIndexOf, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".indexOf(", highlightingMode);
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode, context);
            if(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf(SequenceExpressionArrayOrDequeOrStringLastIndexOf seqExprArrayOrDequeOrStringLastIndexOf, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".lastIndexOf(", highlightingMode);
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode, context);
            if(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayIndexOfOrdered(SequenceExpressionArrayIndexOfOrdered seqExprArrayIndexOfOrdered, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ContainerExpr, seqExprArrayIndexOfOrdered, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".indexOfOrdered(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ValueToSearchForExpr, seqExprArrayIndexOfOrdered, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArraySum(SequenceExpressionArraySum seqExprArraySum, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArraySum.ContainerExpr, seqExprArraySum, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".sum()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayProd(SequenceExpressionArrayProd seqExprArrayProd, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayProd.ContainerExpr, seqExprArrayProd, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".prod()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayMin(SequenceExpressionArrayMin seqExprArrayMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMin.ContainerExpr, seqExprArrayMin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".min()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayMax(SequenceExpressionArrayMax seqExprArrayMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMax.ContainerExpr, seqExprArrayMax, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".max()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayAvg(SequenceExpressionArrayAvg seqExprArrayAvg, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAvg.ContainerExpr, seqExprArrayAvg, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".avg()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayMed(SequenceExpressionArrayMed seqExprArrayMed, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMed.ContainerExpr, seqExprArrayMed, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".med()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayMedUnordered(SequenceExpressionArrayMedUnordered seqExprArrayMedUnordered, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMedUnordered.ContainerExpr, seqExprArrayMedUnordered, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".medUnordered()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayVar(SequenceExpressionArrayVar seqExprArrayVar, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayVar.ContainerExpr, seqExprArrayVar, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".var()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayDev(SequenceExpressionArrayDev seqExprArrayDev, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayDev.ContainerExpr, seqExprArrayDev, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".dev()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayAnd(SequenceExpressionArrayAnd seqExprArrayAnd, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAnd.ContainerExpr, seqExprArrayAnd, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".and()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOr(SequenceExpressionArrayOr seqExprArrayOr, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOr.ContainerExpr, seqExprArrayOr, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".or()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOrDequeAsSet(SequenceExpressionArrayOrDequeAsSet seqExprArrayOrDequeAsSet, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrDequeAsSet.ContainerExpr, seqExprArrayOrDequeAsSet, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".asSet()", highlightingMode);
        }

        private static void PrintSequenceExpressionMapDomain(SequenceExpressionMapDomain seqExprMapDomain, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprMapDomain.ContainerExpr, seqExprMapDomain, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".domain()", highlightingMode);
        }

        private static void PrintSequenceExpressionMapRange(SequenceExpressionMapRange seqExprMapRange, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprMapRange.ContainerExpr, seqExprMapRange, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".range()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayAsMap(SequenceExpressionArrayAsMap seqExprArrayAsMap, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAsMap.ContainerExpr, seqExprArrayAsMap, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".asSet()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayAsDeque(SequenceExpressionArrayAsDeque seqExprArrayAsDeque, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAsDeque.ContainerExpr, seqExprArrayAsDeque, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".asDeque()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayAsString(SequenceExpressionArrayAsString seqExprArrayAsString, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAsString.ContainerExpr, seqExprArrayAsString, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".asString(", highlightingMode);
            PrintSequenceExpression(seqExprArrayAsString.Separator, seqExprArrayAsString, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArraySubarray(SequenceExpressionArraySubarray seqExprArraySubarray, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArraySubarray.ContainerExpr, seqExprArraySubarray, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".subarray(", highlightingMode);
            PrintSequenceExpression(seqExprArraySubarray.Start, seqExprArraySubarray, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprArraySubarray.Length, seqExprArraySubarray, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionDequeSubdeque(SequenceExpressionDequeSubdeque seqExprDequeSubdeque, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprDequeSubdeque.ContainerExpr, seqExprDequeSubdeque, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".subdeque(", highlightingMode);
            PrintSequenceExpression(seqExprDequeSubdeque.Start, seqExprDequeSubdeque, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprDequeSubdeque.Length, seqExprDequeSubdeque, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOrderAscending(SequenceExpressionArrayOrderAscending seqExprArrayOrderAscending, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrderAscending.ContainerExpr, seqExprArrayOrderAscending, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".orderAscending()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOrderDescending(SequenceExpressionArrayOrderDescending seqExprArrayOrderDescending, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrderDescending.ContainerExpr, seqExprArrayOrderDescending, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".orderDescending()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayGroup(SequenceExpressionArrayGroup seqExprArrayGroup, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayGroup.ContainerExpr, seqExprArrayGroup, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".group()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayKeepOneForEach(SequenceExpressionArrayKeepOneForEach seqExprArrayKeepOneForEach, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayKeepOneForEach.ContainerExpr, seqExprArrayKeepOneForEach, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".keepOneForEach()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayReverse(SequenceExpressionArrayReverse seqExprArrayReverse, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayReverse.ContainerExpr, seqExprArrayReverse, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".reverse()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayShuffle(SequenceExpressionArrayShuffle seqExprArrayShuffle, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayShuffle.ContainerExpr, seqExprArrayShuffle, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".shuffle()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayExtract(SequenceExpressionArrayExtract seqExprArrayExtract, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayExtract.ContainerExpr, seqExprArrayExtract, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".extract<", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayExtract.memberOrAttributeName, highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(">()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayMap(SequenceExpressionArrayMap seqExprArrayMap, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMap.Name + ".map<" + seqExprArrayMap.TypeName + ">{", highlightingMode);
            if(seqExprArrayMap.ArrayAccess != null)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMap.ArrayAccess.Name + "; ", highlightingMode);
            if(seqExprArrayMap.Index != null)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMap.Index.Name + " -> ", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMap.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMap.MappingExpr, seqExprArrayMap, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayRemoveIf(SequenceExpressionArrayRemoveIf seqExprArrayRemoveIf, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayRemoveIf.Name + ".removeIf{", highlightingMode);
            if(seqExprArrayRemoveIf.ArrayAccess != null)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayRemoveIf.ArrayAccess.Name + "; ", highlightingMode);
            if(seqExprArrayRemoveIf.Index != null)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayRemoveIf.Index.Name + " -> ", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayRemoveIf.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayRemoveIf.ConditionExpr, seqExprArrayRemoveIf, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpressionArrayMapStartWithAccumulateBy seqExprArrayMapStartWithAccumulateBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Name + ".map<" + seqExprArrayMapStartWithAccumulateBy.TypeName + ">", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("StartWith", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("{", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess != null)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess.Name + "; ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.InitExpr, seqExprArrayMapStartWithAccumulateBy, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("AccumulateBy", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("{", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.ArrayAccess != null)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.ArrayAccess.Name + "; ", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.PreviousAccumulationAccess.Name + ", ", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.Index != null)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Index.Name + " -> ", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.MappingExpr, seqExprArrayMapStartWithAccumulateBy, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted("}", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOrderAscendingBy(SequenceExpressionArrayOrderAscendingBy seqExprArrayOrderAscendingBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayOrderAscendingBy.Name + ".orderAscendingBy<" + seqExprArrayOrderAscendingBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayOrderDescendingBy(SequenceExpressionArrayOrderDescendingBy seqExprArrayOrderDescendingBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayOrderDescendingBy.Name + ".orderDescendingBy<" + seqExprArrayOrderDescendingBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayGroupBy(SequenceExpressionArrayGroupBy seqExprArrayGroupBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayGroupBy.Name + ".groupBy<" + seqExprArrayGroupBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayKeepOneForEachBy(SequenceExpressionArrayKeepOneForEachBy seqExprArrayKeepOneForEachBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayKeepOneForEachBy.Name + ".keepOneForEach<" + seqExprArrayKeepOneForEachBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayIndexOfBy(SequenceExpressionArrayIndexOfBy seqExprArrayIndexOfBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayIndexOfBy.Name + ".indexOfBy<" + seqExprArrayIndexOfBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfBy.ValueToSearchForExpr, seqExprArrayIndexOfBy, highlightingMode, context);
            if(seqExprArrayIndexOfBy.StartPositionExpr != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayIndexOfBy.StartPositionExpr, seqExprArrayIndexOfBy, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayLastIndexOfBy(SequenceExpressionArrayLastIndexOfBy seqExprArrayLastIndexOfBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayLastIndexOfBy.Name + ".lastIndexOfBy<" + seqExprArrayLastIndexOfBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayLastIndexOfBy.ValueToSearchForExpr, seqExprArrayLastIndexOfBy, highlightingMode, context);
            if(seqExprArrayLastIndexOfBy.StartPositionExpr != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayLastIndexOfBy.StartPositionExpr, seqExprArrayLastIndexOfBy, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionArrayIndexOfOrderedBy(SequenceExpressionArrayIndexOfOrderedBy seqExprArrayIndexOfOrderedBy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprArrayIndexOfOrderedBy.Name + ".indexOfOrderedBy<" + seqExprArrayIndexOfOrderedBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfOrderedBy.ValueToSearchForExpr, seqExprArrayIndexOfOrderedBy, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionElementFromGraph(SequenceExpressionElementFromGraph seqExprElementFromGraph, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("@(" + seqExprElementFromGraph.ElementName + ")", highlightingMode);
        }

        private static void PrintSequenceExpressionNodeByName(SequenceExpressionNodeByName seqExprNodeByName, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("nodeByName(", highlightingMode);
            PrintSequenceExpression(seqExprNodeByName.NodeName, seqExprNodeByName, highlightingMode, context);
            if(seqExprNodeByName.NodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprNodeByName.NodeType, seqExprNodeByName, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionEdgeByName(SequenceExpressionEdgeByName seqExprEdgeByName, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("edgeByName(", highlightingMode);
            PrintSequenceExpression(seqExprEdgeByName.EdgeName, seqExprEdgeByName, highlightingMode, context);
            if(seqExprEdgeByName.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprEdgeByName.EdgeType, seqExprEdgeByName, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionNodeByUnique(SequenceExpressionNodeByUnique seqExprNodeByUnique, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("nodeByUnique(", highlightingMode);
            PrintSequenceExpression(seqExprNodeByUnique.NodeUniqueId, seqExprNodeByUnique, highlightingMode, context);
            if(seqExprNodeByUnique.NodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprNodeByUnique.NodeType, seqExprNodeByUnique, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionEdgeByUnique(SequenceExpressionEdgeByUnique seqExprEdgeByUnique, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("edgeByUnique(", highlightingMode);
            PrintSequenceExpression(seqExprEdgeByUnique.EdgeUniqueId, seqExprEdgeByUnique, highlightingMode, context);
            if(seqExprEdgeByUnique.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprEdgeByUnique.EdgeType, seqExprEdgeByUnique, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionSource(SequenceExpressionSource seqExprSource, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("source(", highlightingMode);
            PrintSequenceExpression(seqExprSource.Edge, seqExprSource, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionTarget(SequenceExpressionTarget seqExprTarget, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("target(", highlightingMode);
            PrintSequenceExpression(seqExprTarget.Edge, seqExprTarget, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionOpposite(SequenceExpressionOpposite seqExprOpposite, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("opposite(", highlightingMode);
            PrintSequenceExpression(seqExprOpposite.Edge, seqExprOpposite, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprOpposite.Node, seqExprOpposite, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionAttributeAccess(SequenceExpressionAttributeAccess seqExprAttributeAccess, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprAttributeAccess.Source, seqExprAttributeAccess, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprAttributeAccess.AttributeName, highlightingMode);
        }

        private static void PrintSequenceExpressionMatchAccess(SequenceExpressionMatchAccess seqExprMatchAccess, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprMatchAccess.Source, seqExprMatchAccess, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprMatchAccess.ElementName, highlightingMode);
        }

        private static void PrintSequenceExpressionAttributeOrMatchAccess(SequenceExpressionAttributeOrMatchAccess seqExprAttributeOrMatchAccess, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprAttributeOrMatchAccess.Source, seqExprAttributeOrMatchAccess, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted(seqExprAttributeOrMatchAccess.AttributeOrElementName, highlightingMode);
        }

        private static void PrintSequenceExpressionNodes(SequenceExpressionNodes seqExprNodes, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprNodes.FunctionSymbol + "(", highlightingMode);
            if(seqExprNodes.NodeType != null)
                PrintSequenceExpression(seqExprNodes.NodeType, seqExprNodes, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionEdges(SequenceExpressionEdges seqExprEdges, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprEdges.FunctionSymbol + "(", highlightingMode);
            if(seqExprEdges.EdgeType != null)
                PrintSequenceExpression(seqExprEdges.EdgeType, seqExprEdges, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionCountNodes(SequenceExpressionCountNodes seqExprCountNodes, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprCountNodes.FunctionSymbol + "(", highlightingMode);
            if(seqExprCountNodes.NodeType != null)
                PrintSequenceExpression(seqExprCountNodes.NodeType, seqExprCountNodes, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionCountEdges(SequenceExpressionCountEdges seqExprCountEdges, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprCountEdges.FunctionSymbol + "(", highlightingMode);
            if(seqExprCountEdges.EdgeType != null)
                PrintSequenceExpression(seqExprCountEdges.EdgeType, seqExprCountEdges, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionEmpty(SequenceExpressionEmpty seqExprEmpty, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("empty()", highlightingMode);
        }

        private static void PrintSequenceExpressionNow(SequenceExpressionNow seqExprNow, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Time::now()", highlightingMode);
        }

        private static void PrintSequenceExpressionSize(SequenceExpressionSize seqExprSize, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("size()", highlightingMode);
        }

        private static void PrintSequenceExpressionAdjacentIncident(SequenceExpressionAdjacentIncident seqExprAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprAdjacentIncident.SourceNode, seqExprAdjacentIncident, highlightingMode, context);
            if(seqExprAdjacentIncident.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprAdjacentIncident.EdgeType, seqExprAdjacentIncident, highlightingMode, context);
            }
            if(seqExprAdjacentIncident.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprAdjacentIncident.OppositeNodeType, seqExprAdjacentIncident, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionCountAdjacentIncident(SequenceExpressionCountAdjacentIncident seqExprCountAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprCountAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountAdjacentIncident.SourceNode, seqExprCountAdjacentIncident, highlightingMode, context);
            if(seqExprCountAdjacentIncident.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountAdjacentIncident.EdgeType, seqExprCountAdjacentIncident, highlightingMode, context);
            }
            if(seqExprCountAdjacentIncident.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountAdjacentIncident.OppositeNodeType, seqExprCountAdjacentIncident, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionReachable(SequenceExpressionReachable seqExprReachable, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprReachable.SourceNode, seqExprReachable, highlightingMode, context);
            if(seqExprReachable.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprReachable.EdgeType, seqExprReachable, highlightingMode, context);
            }
            if(seqExprReachable.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprReachable.OppositeNodeType, seqExprReachable, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionCountReachable(SequenceExpressionCountReachable seqExprCountReachable, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprCountReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountReachable.SourceNode, seqExprCountReachable, highlightingMode, context);
            if(seqExprCountReachable.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountReachable.EdgeType, seqExprCountReachable, highlightingMode, context);
            }
            if(seqExprCountReachable.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountReachable.OppositeNodeType, seqExprCountReachable, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionBoundedReachable(SequenceExpressionBoundedReachable seqExprBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachable.SourceNode, seqExprBoundedReachable, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachable.Depth, seqExprBoundedReachable, highlightingMode, context);
            if(seqExprBoundedReachable.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachable.EdgeType, seqExprBoundedReachable, highlightingMode, context);
            }
            if(seqExprBoundedReachable.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachable.OppositeNodeType, seqExprBoundedReachable, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionBoundedReachableWithRemainingDepth(SequenceExpressionBoundedReachableWithRemainingDepth seqExprBoundedReachableWithRemainingDepth, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprBoundedReachableWithRemainingDepth.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.SourceNode, seqExprBoundedReachableWithRemainingDepth, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.Depth, seqExprBoundedReachableWithRemainingDepth, highlightingMode, context);
            if(seqExprBoundedReachableWithRemainingDepth.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.EdgeType, seqExprBoundedReachableWithRemainingDepth, highlightingMode, context);
            }
            if(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType, seqExprBoundedReachableWithRemainingDepth, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionCountBoundedReachable(SequenceExpressionCountBoundedReachable seqExprCountBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprCountBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountBoundedReachable.SourceNode, seqExprCountBoundedReachable, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprCountBoundedReachable.Depth, seqExprCountBoundedReachable, highlightingMode, context);
            if(seqExprCountBoundedReachable.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountBoundedReachable.EdgeType, seqExprCountBoundedReachable, highlightingMode, context);
            }
            if(seqExprCountBoundedReachable.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountBoundedReachable.OppositeNodeType, seqExprCountBoundedReachable, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionIsBoundedReachable(SequenceExpressionIsBoundedReachable seqExprIsBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprIsBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.SourceNode, seqExprIsBoundedReachable, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.EndElement, seqExprIsBoundedReachable, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.Depth, seqExprIsBoundedReachable, highlightingMode, context);
            if(seqExprIsBoundedReachable.EdgeType != null)
            { 
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsBoundedReachable.EdgeType, seqExprIsBoundedReachable, highlightingMode, context);
            }
            if(seqExprIsBoundedReachable.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsBoundedReachable.OppositeNodeType, seqExprIsBoundedReachable, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionIsAdjacentIncident(SequenceExpressionIsAdjacentIncident seqExprIsAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprIsAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsAdjacentIncident.SourceNode, seqExprIsAdjacentIncident, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsAdjacentIncident.EndElement, seqExprIsAdjacentIncident, highlightingMode, context);
            if(seqExprIsAdjacentIncident.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsAdjacentIncident.EdgeType, seqExprIsAdjacentIncident, highlightingMode, context);
            }
            if(seqExprIsAdjacentIncident.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsAdjacentIncident.OppositeNodeType, seqExprIsAdjacentIncident, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionIsReachable(SequenceExpressionIsReachable seqExprIsReachable, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprIsReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsReachable.SourceNode, seqExprIsReachable, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsReachable.EndElement, seqExprIsReachable, highlightingMode, context);
            if(seqExprIsReachable.EdgeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsReachable.EdgeType, seqExprIsReachable, highlightingMode, context);
            }
            if(seqExprIsReachable.OppositeNodeType != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsReachable.OppositeNodeType, seqExprIsReachable, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionInducedSubgraph(SequenceExpressionInducedSubgraph seqExprInducedSubgraph, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("inducedSubgraph(", highlightingMode);
            PrintSequenceExpression(seqExprInducedSubgraph.NodeSet, seqExprInducedSubgraph, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionDefinedSubgraph(SequenceExpressionDefinedSubgraph seqExprDefinedSubgraph, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("definedSubgraph(", highlightingMode);
            PrintSequenceExpression(seqExprDefinedSubgraph.EdgeSet, seqExprDefinedSubgraph, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        // potential todo: change code structure in equals any sequence expression, too
        private static void PrintSequenceExpressionEqualsAny(SequenceExpressionEqualsAny seqExprEqualsAny, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprEqualsAny.IncludingAttributes ? "equalsAny(" : "equalsAnyStructurally(", highlightingMode);
            PrintSequenceExpression(seqExprEqualsAny.Subgraph, seqExprEqualsAny, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqExprEqualsAny.SubgraphSet, seqExprEqualsAny, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionGetEquivalent(SequenceExpressionGetEquivalent seqExprGetEquivalent, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprGetEquivalent.IncludingAttributes ? "getEquivalent(" : "getEquivalentStructurally(", highlightingMode);
            PrintSequenceExpression(seqExprGetEquivalent.Subgraph, seqExprGetEquivalent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqExprGetEquivalent.SubgraphSet, seqExprGetEquivalent, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionCanonize(SequenceExpressionCanonize seqExprCanonize, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("canonize(", highlightingMode);
            PrintSequenceExpression(seqExprCanonize.Graph, seqExprCanonize, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionNameof(SequenceExpressionNameof seqExprNameof, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("nameof(", highlightingMode);
            if(seqExprNameof.NamedEntity != null)
                PrintSequenceExpression(seqExprNameof.NamedEntity, seqExprNameof, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionUniqueof(SequenceExpressionUniqueof seqExprUniqueof, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("uniqueof(", highlightingMode);
            if(seqExprUniqueof.UniquelyIdentifiedEntity != null)
                PrintSequenceExpression(seqExprUniqueof.UniquelyIdentifiedEntity, seqExprUniqueof, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionTypeof(SequenceExpressionTypeof seqExprTypeof, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("typeof(", highlightingMode);
            PrintSequenceExpression(seqExprTypeof.Entity, seqExprTypeof, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionExistsFile(SequenceExpressionExistsFile seqExprExistsFile, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("File::existsFile(", highlightingMode);
            PrintSequenceExpression(seqExprExistsFile.Path, seqExprExistsFile, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionImport(SequenceExpressionImport seqExprImport, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("File::import(", highlightingMode);
            PrintSequenceExpression(seqExprImport.Path, seqExprImport, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionCopy(SequenceExpressionCopy seqExprCopy, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprCopy.Deep ? "copy(" : "clone(", highlightingMode);
            PrintSequenceExpression(seqExprCopy.ObjectToBeCopied, seqExprCopy, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathMin(SequenceExpressionMathMin seqExprMathMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::min(", highlightingMode);
            PrintSequenceExpression(seqExprMathMin.Left, seqExprMathMin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprMathMin.Right, seqExprMathMin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathMax(SequenceExpressionMathMax seqExprMathMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::max(", highlightingMode);
            PrintSequenceExpression(seqExprMathMax.Left, seqExprMathMax, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprMathMax.Right, seqExprMathMax, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathAbs(SequenceExpressionMathAbs seqExprMathAbs, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::abs(", highlightingMode);
            PrintSequenceExpression(seqExprMathAbs.Argument, seqExprMathAbs, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathCeil(SequenceExpressionMathCeil seqExprMathCeil, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::ceil(", highlightingMode);
            PrintSequenceExpression(seqExprMathCeil.Argument, seqExprMathCeil, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathFloor(SequenceExpressionMathFloor seqExprMathFloor, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::floor(", highlightingMode);
            PrintSequenceExpression(seqExprMathFloor.Argument, seqExprMathFloor, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathRound(SequenceExpressionMathRound seqExprMathRound, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::round(", highlightingMode);
            PrintSequenceExpression(seqExprMathRound.Argument, seqExprMathRound, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathTruncate(SequenceExpressionMathTruncate seqExprMathTruncate, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::truncate(", highlightingMode);
            PrintSequenceExpression(seqExprMathTruncate.Argument, seqExprMathTruncate, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathSqr(SequenceExpressionMathSqr seqExprMathSqr, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::sqr(", highlightingMode);
            PrintSequenceExpression(seqExprMathSqr.Argument, seqExprMathSqr, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathSqrt(SequenceExpressionMathSqrt seqExprMathSqrt, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::sqrt(", highlightingMode);
            PrintSequenceExpression(seqExprMathSqrt.Argument, seqExprMathSqrt, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathPow(SequenceExpressionMathPow seqExprPow, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::pow(", highlightingMode);
            if(seqExprPow.Left != null)
            {
                PrintSequenceExpression(seqExprPow.Left, seqExprPow, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
            }
            PrintSequenceExpression(seqExprPow.Right, seqExprPow, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathLog(SequenceExpressionMathLog seqExprMathLog, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::log(", highlightingMode);
            PrintSequenceExpression(seqExprMathLog.Left, seqExprMathLog, highlightingMode, context);
            if(seqExprMathLog.Right != null)
            {
                WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprMathLog.Right, seqExprMathLog, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathSgn(SequenceExpressionMathSgn seqExprMathSgn, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::sgn(", highlightingMode);
            PrintSequenceExpression(seqExprMathSgn.Argument, seqExprMathSgn, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathSin(SequenceExpressionMathSin seqExprMathSin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::sin(", highlightingMode);
            PrintSequenceExpression(seqExprMathSin.Argument, seqExprMathSin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathCos(SequenceExpressionMathCos seqExprMathCos, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::cos(", highlightingMode);
            PrintSequenceExpression(seqExprMathCos.Argument, seqExprMathCos, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathTan(SequenceExpressionMathTan seqExprMathTan, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::tan(", highlightingMode);
            PrintSequenceExpression(seqExprMathTan.Argument, seqExprMathTan, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathArcSin(SequenceExpressionMathArcSin seqExprMathArcSin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::arcsin(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcSin.Argument, seqExprMathArcSin, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathArcCos(SequenceExpressionMathArcCos seqExprMathArcCos, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::arccos(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcCos.Argument, seqExprMathArcCos, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathArcTan(SequenceExpressionMathArcTan seqExprMathArcTan, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::arctan(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcTan.Argument, seqExprMathArcTan, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionMathPi(SequenceExpressionMathPi seqExprMathPi, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::pi()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathE(SequenceExpressionMathE seqExprMathE, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::e()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathByteMin(SequenceExpressionMathByteMin seqExprMathByteMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::byteMin()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathByteMax(SequenceExpressionMathByteMax seqExprMathByteMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::byteMax()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathShortMin(SequenceExpressionMathShortMin seqExprMathShortMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::shortMin()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathShortMax(SequenceExpressionMathShortMax seqExprMathShortMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::shortMax()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathIntMin(SequenceExpressionMathIntMin seqExprMathIntMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::intMin()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathIntMax(SequenceExpressionMathIntMax seqExprMathIntMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::intMax()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathLongMin(SequenceExpressionMathLongMin seqExprMathLongMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::longMin()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathLongMax(SequenceExpressionMathLongMax seqExprMathLongMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::longMax()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathFloatMin(SequenceExpressionMathFloatMin seqExprMathFloatMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::floatMin()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathFloatMax(SequenceExpressionMathFloatMax seqExprMathFloatMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::floatMax()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathDoubleMin(SequenceExpressionMathDoubleMin seqExprMathDoubleMin, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::doubleMin()", highlightingMode);
        }

        private static void PrintSequenceExpressionMathDoubleMax(SequenceExpressionMathDoubleMax seqExprMathDoubleMax, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Math::doubleMax()", highlightingMode);
        }

        private static void PrintSequenceExpressionRuleQuery(SequenceExpressionRuleQuery seqExprRuleQuery, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprRuleQuery == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            PrintSequence(seqExprRuleQuery.RuleCall, seqExprRuleQuery, highlightingModeLocal, context); // rule all call with test flag, thus [?r]
        }

        private static void PrintSequenceExpressionMultiRuleQuery(SequenceExpressionMultiRuleQuery seqExprMultiRuleQuery, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprMultiRuleQuery == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("[?[", highlightingModeLocal);
            bool first = true;
            foreach(SequenceRuleCall rule in seqExprMultiRuleQuery.MultiRuleCall.Sequences)
            {
                if(first)
                    first = false;
                else
                    WorkaroundManager.Workaround.PrintHighlighted(",", highlightingModeLocal);

                PrintReturnAssignments(rule.ReturnVars, parent, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted(rule.DebugPrefix, highlightingMode);
                PrintRuleCallString(rule, parent, highlightingMode, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqExprMultiRuleQuery.MultiRuleCall.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqExprMultiRuleQuery.MultiRuleCall, highlightingModeLocal, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted("\\<class " + seqExprMultiRuleQuery.MatchClass + ">", highlightingModeLocal);
            WorkaroundManager.Workaround.PrintHighlighted("]", highlightingModeLocal);
        }

        private static void PrintSequenceExpressionMappingClause(SequenceExpressionMappingClause seqExprMappingClause, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprMappingClause == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            WorkaroundManager.Workaround.PrintHighlighted("[:", highlightingModeLocal);

            bool first = true;
            foreach(SequenceRulePrefixedSequence seqRulePrefixedSequence in seqExprMappingClause.MultiRulePrefixedSequence.RulePrefixedSequences)
            {
                if(first)
                    first = false;
                else
                    WorkaroundManager.Workaround.PrintHighlighted(", ", highlightingMode);

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                WorkaroundManager.Workaround.PrintHighlighted("for{", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence, context);
                WorkaroundManager.Workaround.PrintHighlighted(";", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode, context);
                WorkaroundManager.Workaround.PrintHighlighted("}", highlightingModeRulePrefixedSequence);
            }

            foreach(SequenceFilterCallBase filterCall in seqExprMappingClause.MultiRulePrefixedSequence.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqExprMappingClause.MultiRulePrefixedSequence, highlightingModeLocal, context);
            }
            WorkaroundManager.Workaround.PrintHighlighted(":]", highlightingModeLocal);
        }

        private static void PrintSequenceExpressionScan(SequenceExpressionScan seqExprScan, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("scan", highlightingMode);
            if(seqExprScan.ResultType != null)
                WorkaroundManager.Workaround.PrintHighlighted("<" + seqExprScan.ResultType + ">", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
            PrintSequenceExpression(seqExprScan.StringExpr, seqExprScan, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionTryScan(SequenceExpressionTryScan seqExprTryScan, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted("tryscan", highlightingMode);
            if(seqExprTryScan.ResultType != null)
                WorkaroundManager.Workaround.PrintHighlighted("<" + seqExprTryScan.ResultType + ">", highlightingMode);
            WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
            PrintSequenceExpression(seqExprTryScan.StringExpr, seqExprTryScan, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
        }

        private static void PrintSequenceExpressionFunctionCall(SequenceExpressionFunctionCall seqExprFunctionCall, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            WorkaroundManager.Workaround.PrintHighlighted(seqExprFunctionCall.Name, highlightingMode);
            if(seqExprFunctionCall.ArgumentExpressions.Length > 0)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqExprFunctionCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(seqExprFunctionCall.ArgumentExpressions[i], seqExprFunctionCall, highlightingMode, context);
                    if(i != seqExprFunctionCall.ArgumentExpressions.Length - 1)
                        WorkaroundManager.Workaround.PrintHighlighted(",", highlightingMode);
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", highlightingMode);
            }
        }

        private static void PrintSequenceExpressionFunctionMethodCall(SequenceExpressionFunctionMethodCall seqExprFunctionMethodCall, SequenceBase parent, HighlightingMode highlightingMode, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprFunctionMethodCall.TargetExpr, seqExprFunctionMethodCall, highlightingMode, context);
            WorkaroundManager.Workaround.PrintHighlighted(".", highlightingMode);
            PrintSequenceExpressionFunctionCall(seqExprFunctionMethodCall, parent, highlightingMode, context);
        }
    }
}
