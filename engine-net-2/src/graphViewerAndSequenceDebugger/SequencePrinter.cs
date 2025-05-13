/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Diagnostics;
using System.Collections.Generic;

using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class SequencePrinter : ISequenceDisplayer
    {
        readonly IDebuggerEnvironment env;
        DisplaySequenceContext context;

        public SequencePrinter(IDebuggerEnvironment env)
        {
            this.env = env;
        }

        public void DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            DisplaySequenceBase(seqBase, context, nestingLevel, prefix, postfix, null);
        }

        public void DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            DisplaySequence(seq, context, nestingLevel, prefix, postfix, null);
        }

        public void DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix)
        {
            DisplaySequenceExpression(seqExpr, context, nestingLevel, prefix, postfix, null);
        }

        public string DisplaySequenceBase(SequenceBase seqBase, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            if(seqBase is Sequence)
                DisplaySequence((Sequence)seqBase, context, nestingLevel, prefix, postfix, groupNodeName);
            else
                DisplaySequenceExpression((SequenceExpression)seqBase, context, nestingLevel, prefix, postfix, groupNodeName);
            return null;
        }

        public string DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            this.context = context;
            env.PrintHighlighted(prefix + nestingLevel + ">", HighlightingMode.SequenceStart);
            PrintSequence(seq, null, HighlightingMode.None);
            env.PrintHighlighted(postfix, HighlightingMode.SequenceStart);
            env.WriteLineDataRendering();
            return null;
        }

        public string DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            this.context = context;
            env.PrintHighlighted(prefix + nestingLevel + ">", HighlightingMode.SequenceStart);
            PrintSequenceExpression(seqExpr, null, HighlightingMode.None);
            env.PrintHighlighted(postfix, HighlightingMode.SequenceStart);
            env.WriteLineDataRendering();
            return null;
        }

        /// <summary>
        /// Prints the given sequence (adding parentheses if needed) according to the display context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="parent">The parent of the sequence or null if the sequence is a root</param>
        /// <param name="context">The print context</param>
        private void PrintSequence(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence)
                env.PrintHighlighted("(", highlightingMode);

            switch(seq.SequenceType)
            {
            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
            case SequenceType.StrictAnd:
                PrintSequenceBinary((SequenceBinary)seq, parent, highlightingMode);
                break;
            case SequenceType.IfThen:
                PrintSequenceIfThen((SequenceIfThen)seq, parent, highlightingMode);
                break;
            case SequenceType.Not:
                PrintSequenceNot((SequenceNot)seq, parent, highlightingMode);
                break;
            case SequenceType.IterationMin:
                PrintSequenceIterationMin((SequenceIterationMin)seq, parent, highlightingMode);
                break;
            case SequenceType.IterationMinMax:
                PrintSequenceIterationMinMax((SequenceIterationMinMax)seq, parent, highlightingMode);
                break;
            case SequenceType.Transaction:
                PrintSequenceTransaction((SequenceTransaction)seq, parent, highlightingMode);
                break;
            case SequenceType.Backtrack:
                PrintSequenceBacktrack((SequenceBacktrack)seq, parent, highlightingMode);
                break;
            case SequenceType.MultiBacktrack:
                PrintSequenceMultiBacktrack((SequenceMultiBacktrack)seq, parent, highlightingMode);
                break;
            case SequenceType.MultiSequenceBacktrack:
                PrintSequenceMultiSequenceBacktrack((SequenceMultiSequenceBacktrack)seq, parent, highlightingMode);
                break;
            case SequenceType.Pause:
                PrintSequencePause((SequencePause)seq, parent, highlightingMode);
                break;
            case SequenceType.ForContainer:
                PrintSequenceForContainer((SequenceForContainer)seq, parent, highlightingMode);
                break;
            case SequenceType.ForIntegerRange:
                PrintSequenceForIntegerRange((SequenceForIntegerRange)seq, parent, highlightingMode);
                break;
            case SequenceType.ForIndexAccessEquality:
                PrintSequenceForIndexAccessEquality((SequenceForIndexAccessEquality)seq, parent, highlightingMode);
                break;
            case SequenceType.ForIndexAccessOrdering:
                PrintSequenceForIndexAccessOrdering((SequenceForIndexAccessOrdering)seq, parent, highlightingMode);
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
                PrintSequenceForFunction((SequenceForFunction)seq, parent, highlightingMode);
                break;
            case SequenceType.ForMatch:
                PrintSequenceForMatch((SequenceForMatch)seq, parent, highlightingMode);
                break;
            case SequenceType.ExecuteInSubgraph:
                PrintSequenceExecuteInSubgraph((SequenceExecuteInSubgraph)seq, parent, highlightingMode);
                break;
            case SequenceType.ParallelExecute:
                PrintSequenceParallelExecute((SequenceParallelExecute)seq, parent, highlightingMode);
                break;
            case SequenceType.ParallelArrayExecute:
                PrintSequenceParallelArrayExecute((SequenceParallelArrayExecute)seq, parent, highlightingMode);
                break;
            case SequenceType.Lock:
                PrintSequenceLock((SequenceLock)seq, parent, highlightingMode);
                break;
            case SequenceType.IfThenElse:
                PrintSequenceIfThenElse((SequenceIfThenElse)seq, parent, highlightingMode);
                break;
            case SequenceType.LazyOrAll:
            case SequenceType.LazyAndAll:
            case SequenceType.StrictOrAll:
            case SequenceType.StrictAndAll:
                PrintSequenceNAry((SequenceNAry)seq, parent, highlightingMode);
                break;
            case SequenceType.WeightedOne:
                PrintSequenceWeightedOne((SequenceWeightedOne)seq, parent, highlightingMode);
                break;
            case SequenceType.SomeFromSet:
                PrintSequenceSomeFromSet((SequenceSomeFromSet)seq, parent, highlightingMode);
                break;
            case SequenceType.MultiRulePrefixedSequence:
                PrintSequenceMultiRulePrefixedSequence((SequenceMultiRulePrefixedSequence)seq, parent, highlightingMode);
                break;
            case SequenceType.MultiRuleAllCall:
                PrintSequenceMultiRuleAllCall((SequenceMultiRuleAllCall)seq, parent, highlightingMode);
                break;
            case SequenceType.RulePrefixedSequence:
                PrintSequenceRulePrefixedSequence((SequenceRulePrefixedSequence)seq, parent, highlightingMode);
                break;
            case SequenceType.SequenceCall:
            case SequenceType.RuleCall:
            case SequenceType.RuleAllCall:
            case SequenceType.RuleCountAllCall:
            case SequenceType.BooleanComputation:
                PrintSequenceBreakpointable((Sequence)seq, parent, highlightingMode);
                break;
            case SequenceType.AssignSequenceResultToVar:
            case SequenceType.OrAssignSequenceResultToVar:
            case SequenceType.AndAssignSequenceResultToVar:
                PrintSequenceAssignSequenceResultToVar((SequenceAssignSequenceResultToVar)seq, parent, highlightingMode);
                break;
            case SequenceType.AssignUserInputToVar:
            case SequenceType.AssignRandomIntToVar:
            case SequenceType.AssignRandomDoubleToVar:
                PrintSequenceAssignChoiceHighlightable((Sequence)seq, parent, highlightingMode);
                break;
            case SequenceType.SequenceDefinitionInterpreted:
                PrintSequenceDefinitionInterpreted((SequenceDefinitionInterpreted)seq, parent, highlightingMode);
                break;
            // Atoms (assignments)
            case SequenceType.AssignVarToVar:
            case SequenceType.AssignConstToVar:
            case SequenceType.DeclareVariable:
                env.PrintHighlighted(seq.Symbol, highlightingMode);
                break;
            case SequenceType.AssignContainerConstructorToVar:
                PrintSequenceAssignContainerConstructorToVar((SequenceAssignContainerConstructorToVar)seq, parent, highlightingMode);
                break;
            default:
                Debug.Assert(false);
                env.Write("<UNKNOWN_SEQUENCE_TYPE>");
                break;
            }

            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence)
                env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceBinary(SequenceBinary seqBin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null && seqBin.Random)
            {
                PrintSequence(seqBin.Left, seqBin, highlightingMode);
                PrintChoice(seqBin);
                env.PrintHighlighted(seqBin.OperatorSymbol + " ", highlightingMode);
                PrintSequence(seqBin.Right, seqBin, highlightingMode);
                return;
            }

            if(seqBin == context.highlightSeq && context.choice)
            {
                env.PrintHighlighted("(l)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Left, seqBin, highlightingMode);
                env.PrintHighlighted("(l) " + seqBin.OperatorSymbol + " (r)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Right, seqBin, highlightingMode);
                env.PrintHighlighted("(r)", HighlightingMode.Choicepoint);
                return;
            }

            PrintSequence(seqBin.Left, seqBin, highlightingMode);
            env.PrintHighlighted(" " + seqBin.OperatorSymbol + " ", highlightingMode);
            PrintSequence(seqBin.Right, seqBin, highlightingMode);
        }

        private void PrintSequenceIfThen(SequenceIfThen seqIfThen, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("if{", highlightingMode);
            PrintSequence(seqIfThen.Left, seqIfThen, highlightingMode);
            env.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqIfThen.Right, seqIfThen, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceNot(SequenceNot seqNot, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqNot.OperatorSymbol, highlightingMode);
            PrintSequence(seqNot.Seq, seqNot, highlightingMode);
        }

        private void PrintSequenceIterationMin(SequenceIterationMin seqMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequence(seqMin.Seq, seqMin, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
            PrintSequenceExpression(seqMin.MinExpr, seqMin, highlightingMode);
            env.PrintHighlighted(":*]", highlightingMode);
        }

        private void PrintSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequence(seqMinMax.Seq, seqMinMax, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
            PrintSequenceExpression(seqMinMax.MinExpr, seqMinMax, highlightingMode);
            env.PrintHighlighted(":", highlightingMode);
            PrintSequenceExpression(seqMinMax.MaxExpr, seqMinMax, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceTransaction(SequenceTransaction seqTrans, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("<", highlightingMode);
            PrintSequence(seqTrans.Seq, seqTrans, highlightingMode);
            env.PrintHighlighted(">", highlightingMode);
        }

        private void PrintSequenceBacktrack(SequenceBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("<<", highlightingModeLocal);
            PrintSequence(seqBack.Rule, seqBack, highlightingModeLocal);
            env.PrintHighlighted(";;", highlightingModeLocal);
            PrintSequence(seqBack.Seq, seqBack, highlightingMode);
            env.PrintHighlighted(">>", highlightingModeLocal);
        }

        private void PrintSequenceMultiBacktrack(SequenceMultiBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("<<", highlightingModeLocal);
            PrintSequence(seqBack.Rules, seqBack, highlightingModeLocal);
            env.PrintHighlighted(";;", highlightingModeLocal);
            PrintSequence(seqBack.Seq, seqBack, highlightingMode);
            env.PrintHighlighted(">>", highlightingModeLocal);
        }

        private void PrintSequenceMultiSequenceBacktrack(SequenceMultiSequenceBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("<<", highlightingModeLocal);
            env.PrintHighlighted("[[", highlightingModeLocal);

            bool first = true;
            foreach(SequenceRulePrefixedSequence seqRulePrefixedSequence in seqBack.MultiRulePrefixedSequence.RulePrefixedSequences)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                env.PrintHighlighted("for{", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence);
                env.PrintHighlighted(";", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
                env.PrintHighlighted("}", highlightingModeRulePrefixedSequence);
            }

            env.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqBack.MultiRulePrefixedSequence.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqBack.MultiRulePrefixedSequence, highlightingModeLocal);
            }
            env.PrintHighlighted("]", highlightingModeLocal);
            env.PrintHighlighted(">>", highlightingModeLocal);
        }

        private void PrintSequencePause(SequencePause seqPause, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("/", highlightingMode);
            PrintSequence(seqPause.Seq, seqPause, highlightingMode);
            env.PrintHighlighted("/", highlightingMode);
        }

        private void PrintSequenceForContainer(SequenceForContainer seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("for{", highlightingMode);
            env.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            if(seqFor.VarDst != null)
                env.PrintHighlighted("->" + seqFor.VarDst.Name, highlightingMode);
            env.PrintHighlighted(" in " + seqFor.Container.Name, highlightingMode);
            env.PrintHighlighted("; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceForIntegerRange(SequenceForIntegerRange seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("for{", highlightingMode);
            env.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            env.PrintHighlighted(" in [", highlightingMode);
            PrintSequenceExpression(seqFor.Left, seqFor, highlightingMode);
            env.PrintHighlighted(":", highlightingMode);
            PrintSequenceExpression(seqFor.Right, seqFor, highlightingMode);
            env.PrintHighlighted("]; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceForIndexAccessEquality(SequenceForIndexAccessEquality seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("for{", highlightingMode);
            env.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            env.PrintHighlighted(" in {", highlightingMode);
            env.PrintHighlighted(seqFor.IndexName, highlightingMode);
            env.PrintHighlighted("==", highlightingMode);
            PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
            env.PrintHighlighted("}; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("for{", highlightingMode);
            env.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            env.PrintHighlighted(" in {", highlightingMode);
            if(seqFor.Ascending)
                env.PrintHighlighted("ascending", highlightingMode);
            else
                env.PrintHighlighted("descending", highlightingMode);
            env.PrintHighlighted("(", highlightingMode);
            if(seqFor.From() != null && seqFor.To() != null)
            {
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
                env.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
                env.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction2), highlightingMode);
                PrintSequenceExpression(seqFor.Expr2, seqFor, highlightingMode);
            }
            else if(seqFor.From() != null)
            {
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
                env.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
            }
            else if(seqFor.To() != null)
            {
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
                env.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
            }
            else
            {
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
            env.PrintHighlighted("}; ", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceForFunction(SequenceForFunction seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("for{", highlightingMode);
            env.PrintHighlighted(seqFor.Var.Name, highlightingMode);
            env.PrintHighlighted(" in ", highlightingMode);
            env.PrintHighlighted(seqFor.FunctionSymbol, highlightingMode);
            PrintArguments(seqFor.ArgExprs, parent, highlightingMode);
            env.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceForMatch(SequenceForMatch seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqFor == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("for{", highlightingModeLocal);
            env.PrintHighlighted(seqFor.Var.Name, highlightingModeLocal);
            env.PrintHighlighted(" in [?", highlightingModeLocal);
            PrintSequence(seqFor.Rule, seqFor, highlightingModeLocal);
            env.PrintHighlighted("]; ", highlightingModeLocal);
            PrintSequence(seqFor.Seq, seqFor, highlightingMode);
            env.PrintHighlighted("}", highlightingModeLocal);
        }

        private void PrintSequenceExecuteInSubgraph(SequenceExecuteInSubgraph seqExecInSub, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("in ", highlightingMode);
            PrintSequenceExpression(seqExecInSub.SubgraphExpr, seqExecInSub, highlightingMode);
            if(seqExecInSub.ValueExpr != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExecInSub.ValueExpr, seqExecInSub, highlightingMode);
            }
            env.PrintHighlighted(" {", highlightingMode);
            PrintSequence(seqExecInSub.Seq, seqExecInSub, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceParallelExecute(SequenceParallelExecute seqParallelExec, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqParallelExec == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("parallel", highlightingModeLocal);

            for(int i = 0; i < seqParallelExec.InSubgraphExecutions.Count; ++i)
            {
                SequenceExecuteInSubgraph seqExecInSub = seqParallelExec.InSubgraphExecutions[i];
                env.PrintHighlighted(" ", highlightingModeLocal);
                if(context.sequences != null)
                {
                    if(seqExecInSub == context.highlightSeq)
                        env.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                    if(seqExecInSub == context.sequences[i])
                        env.PrintHighlighted("(" + i + ")", HighlightingMode.Choicepoint);
                }
                PrintSequenceExecuteInSubgraph(seqExecInSub, seqParallelExec, highlightingModeLocal);
                if(context.sequences != null)
                {
                    if(seqExecInSub == context.highlightSeq)
                        env.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                }
            }
        }

        private void PrintSequenceParallelArrayExecute(SequenceParallelArrayExecute seqParallelArrayExec, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqParallelArrayExec == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("parallel array", highlightingModeLocal);

            for(int i = 0; i < seqParallelArrayExec.InSubgraphExecutions.Count; ++i)
            {
                SequenceExecuteInSubgraph seqExecInSub = seqParallelArrayExec.InSubgraphExecutions[i];
                env.PrintHighlighted(" ", highlightingModeLocal);
                if(context.sequences != null)
                {
                    if(seqExecInSub == context.highlightSeq)
                        env.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                    if(seqExecInSub == context.sequences[i])
                        env.PrintHighlighted("(" + i + ")", HighlightingMode.Choicepoint);
                }
                PrintSequenceExecuteInSubgraph(seqExecInSub, seqParallelArrayExec, highlightingModeLocal);
                if(context.sequences != null)
                {
                    if(seqExecInSub == context.highlightSeq)
                        env.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                }
            }
        }

        private void PrintSequenceLock(SequenceLock seqLock, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("lock(", highlightingMode);
            PrintSequenceExpression(seqLock.LockObjectExpr, seqLock, highlightingMode);
            env.PrintHighlighted("){", highlightingMode);
            PrintSequence(seqLock.Seq, seqLock, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceIfThenElse(SequenceIfThenElse seqIf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("if{", highlightingMode);
            PrintSequence(seqIf.Condition, seqIf, highlightingMode);
            env.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqIf.TrueCase, seqIf, highlightingMode);
            env.PrintHighlighted(";", highlightingMode);
            PrintSequence(seqIf.FalseCase, seqIf, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceNAry(SequenceNAry seqN, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null)
            {
                PrintChoice(seqN);
                env.PrintHighlighted((seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(", highlightingMode);
                bool first = true;
                foreach(Sequence seqChild in seqN.Children)
                {
                    if(!first)
                        env.PrintHighlighted(", ", highlightingMode);
                    PrintSequence(seqChild, seqN, highlightingMode);
                    first = false;
                }
                env.PrintHighlighted(")", highlightingMode);
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
                env.PrintHighlighted("$%" + seqN.OperatorSymbol + "(", HighlightingMode.Choicepoint);
                bool first = true;
                foreach(Sequence seqChild in seqN.Children)
                {
                    if(!first)
                        env.PrintHighlighted(", ", highlightingMode);
                    if(seqChild == context.highlightSeq)
                        env.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                    if(context.sequences != null)
                    {
                        for(int i = 0; i < context.sequences.Count; ++i)
                        {
                            if(seqChild == context.sequences[i])
                                env.PrintHighlighted("(" + i + ")", HighlightingMode.Choicepoint);
                        }
                    }

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    PrintSequence(seqChild, seqN, highlightingMode);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqChild == context.highlightSeq)
                        env.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    first = false;
                }
                env.PrintHighlighted(")", HighlightingMode.Choicepoint);
                return;
            }

            env.PrintHighlighted((seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(", highlightingMode);
            PrintChildren(seqN, highlightingMode, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceWeightedOne(SequenceWeightedOne seqWeighted, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null)
            {
                PrintChoice(seqWeighted);
                env.PrintHighlighted((seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(", highlightingMode);
                bool first = true;
                for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                {
                    if(first)
                        env.PrintHighlighted("0.00 ", highlightingMode);
                    else
                        env.PrintHighlighted(" ", highlightingMode);
                    PrintSequence(seqWeighted.Sequences[i], seqWeighted, highlightingMode);
                    env.PrintHighlighted(" ", highlightingMode);
                    env.PrintHighlighted(seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture), highlightingMode); // todo: format auf 2 nachkommastellen 
                    first = false;
                }
                env.PrintHighlighted(")", highlightingMode);
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
                env.PrintHighlighted("$%" + seqWeighted.OperatorSymbol + "(", HighlightingMode.Choicepoint);
                bool first = true;
                for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                {
                    if(first)
                        env.PrintHighlighted("0.00 ", highlightingMode);
                    else
                        env.PrintHighlighted(" ", highlightingMode);
                    if(seqWeighted.Sequences[i] == context.highlightSeq)
                        env.PrintHighlighted(">>", HighlightingMode.Choicepoint);

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    PrintSequence(seqWeighted.Sequences[i], seqWeighted, highlightingMode);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqWeighted.Sequences[i] == context.highlightSeq)
                        env.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    env.PrintHighlighted(" ", highlightingMode);
                    env.PrintHighlighted(seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture), highlightingMode); // todo: format auf 2 nachkommastellen 
                    first = false;
                }
                env.PrintHighlighted(")", HighlightingMode.Choicepoint);
                return;
            }

            env.PrintHighlighted((seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(", highlightingMode);
            bool ffs = true;
            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
            {
                if(ffs)
                    env.PrintHighlighted("0.00 ", highlightingMode);
                else
                    env.PrintHighlighted(" ", highlightingMode);
                PrintSequence(seqWeighted.Sequences[i], seqWeighted, highlightingMode);
                env.PrintHighlighted(" ", highlightingMode);
                env.PrintHighlighted(seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture), highlightingMode); // todo: format auf 2 nachkommastellen 
                ffs = false;
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceSomeFromSet(SequenceSomeFromSet seqSome, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null
                && seqSome.Random)
            {
                PrintChoice(seqSome);
                env.PrintHighlighted((seqSome.Choice ? "$%" : "$") + "{<", highlightingMode);
                bool first = true;
                foreach(Sequence seqChild in seqSome.Children)
                {
                    if(!first)
                        env.PrintHighlighted(", ", highlightingMode);
                    Dictionary<int, int> sequenceIdToChoicepointPosMapBackup = context.sequenceIdToChoicepointPosMap; // TODO: this works? choicepoint numbers maybe not displayed this way, but still assigned on the outside...
                    context.sequenceIdToChoicepointPosMap = null; // rules within some-from-set are not choicepointable
                    PrintSequence(seqChild, seqSome, highlightingMode);
                    context.sequenceIdToChoicepointPosMap = sequenceIdToChoicepointPosMapBackup;
                    first = false;
                }
                env.PrintHighlighted(">}", highlightingMode);
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
                env.PrintHighlighted("$%" + "{<", HighlightingMode.Choicepoint);
                bool first = true;
                int numCurTotalMatch = 0; // potential todo: pre-compute the match numbers in another loop, so there's no dependency in between the loops
                foreach(Sequence seqChild in seqSome.Children)
                {
                    if(!first)
                        env.PrintHighlighted(", ", highlightingMode);
                    if(seqChild == context.highlightSeq)
                        env.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                    if(context.sequences != null)
                    {
                        for(int i = 0; i < context.sequences.Count; ++i)
                        {
                            if(seqChild == context.sequences[i] && context.matches[i].Count > 0)
                            {
                                PrintListOfMatchesNumbers(ref numCurTotalMatch, seqSome.IsNonRandomRuleAllCall(i) ? context.matches[i].Count : 1);
                            }
                        }
                    }

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    PrintSequence(seqChild, seqSome, highlightingMode);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqChild == context.highlightSeq)
                        env.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    first = false;
                }
                env.PrintHighlighted(">}", HighlightingMode.Choicepoint);
                return;
            }

            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqSome == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted(seqSome.Random ? ((seqSome.Choice ? "$%" : "$") + "{<") : "{<", highlightingModeLocal);
            PrintChildren(seqSome, highlightingMode, highlightingModeLocal);
            env.PrintHighlighted(">}", highlightingModeLocal);
        }

        private void PrintSequenceMultiRulePrefixedSequence(SequenceMultiRulePrefixedSequence seqMulti, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqMulti == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("[[", highlightingModeLocal);

            bool first = true;
            foreach(SequenceRulePrefixedSequence seqRulePrefixedSequence in seqMulti.RulePrefixedSequences)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                env.PrintHighlighted("for{", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence);
                env.PrintHighlighted(";", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
                env.PrintHighlighted("}", highlightingModeRulePrefixedSequence);
            }

            env.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqMulti, highlightingModeLocal);
            }
            env.PrintHighlighted("]", highlightingModeLocal);
        }

        private void PrintSequenceMultiRuleAllCall(SequenceMultiRuleAllCall seqMulti, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqMulti == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("[[", highlightingModeLocal);
            PrintChildren(seqMulti, highlightingMode, highlightingModeLocal);
            env.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqMulti, highlightingModeLocal);
            }
            env.PrintHighlighted("]", highlightingModeLocal);
        }

        private void PrintSequenceRulePrefixedSequence(SequenceRulePrefixedSequence seqRulePrefixedSequence, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqRulePrefixedSequence == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            if(!(parent is SequenceMultiRulePrefixedSequence))
                env.PrintHighlighted("[", highlightingModeLocal);

            env.PrintHighlighted("for{", highlightingModeLocal);
            PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeLocal);
            env.PrintHighlighted(";", highlightingModeLocal);
            PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
            env.PrintHighlighted("}", highlightingModeLocal);

            if(!(parent is SequenceMultiRulePrefixedSequence))
                env.PrintHighlighted("]", highlightingModeLocal);
        }

        private void PrintSequenceBreakpointable(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToBreakpointPosMap != null)
            {
                PrintBreak((SequenceSpecial)seq);
            }

            if(context.sequenceIdToChoicepointPosMap != null
                && seq is SequenceRandomChoice
                && ((SequenceRandomChoice)seq).Random)
            {
                PrintChoice((SequenceRandomChoice)seq);
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
                PrintSequenceAtom(seq, parent, highlightingMode);
            else
                PrintSequenceAtom(seq, parent, highlightingModeLocal);
        }

        private void PrintSequenceAtom(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            switch(seq.SequenceType)
            {
            case SequenceType.SequenceCall:
                PrintSequenceSequenceCall((SequenceSequenceCallInterpreted)seq, parent, highlightingMode);
                break;
            case SequenceType.RuleCall:
                PrintSequenceRuleCall((SequenceRuleCall)seq, parent, highlightingMode);
                break;
            case SequenceType.RuleAllCall:
                PrintSequenceRuleAllCall((SequenceRuleAllCall)seq, parent, highlightingMode);
                break;
            case SequenceType.RuleCountAllCall:
                PrintSequenceRuleCountAllCall((SequenceRuleCountAllCall)seq, parent, highlightingMode);
                break;
            case SequenceType.BooleanComputation:
                PrintSequenceBooleanComputation((SequenceBooleanComputation)seq, parent, highlightingMode);
                break;
            default:
                Debug.Assert(false);
                env.Write("<UNKNOWN_SEQUENCE_TYPE>");
                break;
            }
        }

        private void PrintSequenceSequenceCall(SequenceSequenceCallInterpreted seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seq.Special)
                env.PrintHighlighted("%", highlightingMode); // TODO: questionable position here and in sequence -- should appear before sequence name, not return assignment
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode);
            if(seq.subgraph != null)
                env.PrintHighlighted(seq.subgraph.Name + ".", highlightingMode);
            env.PrintHighlighted(seq.SequenceDef.Name, highlightingMode);
            if(seq.ArgumentExpressions.Length > 0)
            {
                PrintArguments(seq.ArgumentExpressions, parent, highlightingMode);
            }
        }

        private void PrintArguments(SequenceExpression[] arguments, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("(", highlightingMode);
            for(int i = 0; i < arguments.Length; ++i)
            {
                PrintSequenceExpression(arguments[i], parent, highlightingMode);
                if(i != arguments.Length - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintArguments(IList<SequenceExpression> arguments, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("(", highlightingMode);
            for(int i = 0; i < arguments.Count; ++i)
            {
                PrintSequenceExpression(arguments[i], parent, highlightingMode);
                if(i != arguments.Count - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceRuleCall(SequenceRuleCall seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode);
            env.PrintHighlighted(seq.TestDebugPrefix, highlightingMode);
            PrintRuleCallString(seq, parent, highlightingMode);
        }

        private void PrintSequenceRuleAllCall(SequenceRuleAllCall seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode);
            env.PrintHighlighted(seq.RandomChoicePrefix, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
            env.PrintHighlighted(seq.TestDebugPrefix, highlightingMode);
            PrintRuleCallString(seq, parent, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceRuleCountAllCall(SequenceRuleCountAllCall seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, highlightingMode);
            env.PrintHighlighted("count[", highlightingMode);
            env.PrintHighlighted(seq.TestDebugPrefix, highlightingMode);
            PrintRuleCallString(seq, parent, highlightingMode);
            env.PrintHighlighted("]" + "=>" + seq.CountResult.Name, highlightingMode);
        }

        private void PrintReturnAssignments(SequenceVariable[] returnVars, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(returnVars.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < returnVars.Length; ++i)
                {
                    env.PrintHighlighted(returnVars[i].Name, highlightingMode);
                    if(i != returnVars.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")=", highlightingMode);
            }
        }

        private void PrintRuleCallString(SequenceRuleCall seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seq.subgraph != null)
                env.PrintHighlighted(seq.subgraph.Name + ".", highlightingMode);
            env.PrintHighlighted(seq.Name, highlightingMode);
            if(seq.ArgumentExpressions.Length > 0)
            {
                PrintArguments(seq.ArgumentExpressions, parent, highlightingMode);
            }
            for(int i = 0; i < seq.Filters.Count; ++i)
            {
                PrintSequenceFilterCall(seq.Filters[i], seq, highlightingMode);
            }
        }

        private void PrintSequenceFilterCall(SequenceFilterCallBase seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("\\", highlightingMode);
            if(seq is SequenceFilterCallInterpreted)
            {
                SequenceFilterCallInterpreted filterCall = (SequenceFilterCallInterpreted)seq;
                if(filterCall.MatchClass != null)
                    env.PrintHighlighted(filterCall.MatchClass.info.PackagePrefixedName + ".", highlightingMode);
                env.PrintHighlighted(filterCall.PackagePrefixedName, highlightingMode);
                PrintArguments(filterCall.ArgumentExpressions, parent, highlightingMode);
            }
            else if(seq is SequenceFilterCallLambdaExpressionInterpreted)
            {
                SequenceFilterCallLambdaExpressionInterpreted filterCall = (SequenceFilterCallLambdaExpressionInterpreted)seq;
                if(filterCall.MatchClass != null)
                    env.PrintHighlighted(filterCall.MatchClass.info.PackagePrefixedName + ".", highlightingMode);
                env.PrintHighlighted(filterCall.Name, highlightingMode);
                //if(filterCall.Entity != null)
                //    sb.Append("<" + filterCall.Entity + ">");
                if(filterCall.FilterCall.initExpression != null)
                {
                    env.PrintHighlighted("{", highlightingMode);
                    if(filterCall.FilterCall.initArrayAccess != null)
                        env.PrintHighlighted(filterCall.FilterCall.initArrayAccess.Name + "; ", highlightingMode);
                    PrintSequenceExpression(filterCall.FilterCall.initExpression, parent, highlightingMode);
                    env.PrintHighlighted("}", highlightingMode);
                }
                env.PrintHighlighted("{", highlightingMode);
                if(filterCall.FilterCall.arrayAccess != null)
                    env.PrintHighlighted(filterCall.FilterCall.arrayAccess.Name + "; ", highlightingMode);
                if(filterCall.FilterCall.previousAccumulationAccess != null)
                    env.PrintHighlighted(filterCall.FilterCall.previousAccumulationAccess + ", ", highlightingMode);
                if(filterCall.FilterCall.index != null)
                    env.PrintHighlighted(filterCall.FilterCall.index.Name + " -> ", highlightingMode);
                env.PrintHighlighted(filterCall.FilterCall.element.Name + " -> ", highlightingMode);
                PrintSequenceExpression(filterCall.FilterCall.lambdaExpression, parent, highlightingMode);
                env.PrintHighlighted("}", highlightingMode);
            }
            else
            {
                Debug.Assert(false);
            }
        }

        private void PrintSequenceBooleanComputation(SequenceBooleanComputation seqComp, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceComputation(seqComp.Computation, seqComp, highlightingMode);
        }

        private void PrintSequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar seqAss, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("(", highlightingMode);
            PrintSequence(seqAss.Seq, seqAss, highlightingMode);
            env.PrintHighlighted(seqAss.OperatorSymbol, highlightingMode);
            env.PrintHighlighted(seqAss.DestVar.Name, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        // Choice highlightable user assignments
        private void PrintSequenceAssignChoiceHighlightable(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null
                && (seq is SequenceAssignRandomIntToVar || seq is SequenceAssignRandomDoubleToVar))
            {
                PrintChoice((SequenceRandomChoice)seq);
                env.PrintHighlighted(seq.Symbol, highlightingMode);
                return;
            }

            if(seq == context.highlightSeq && context.choice)
                env.PrintHighlighted(seq.Symbol, HighlightingMode.Choicepoint);
            else
                env.PrintHighlighted(seq.Symbol, highlightingMode);
        }

        private void PrintSequenceDefinitionInterpreted(SequenceDefinitionInterpreted seqDef, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = HighlightingMode.None;
            if(seqDef.ExecutionState == SequenceExecutionState.Success)
                highlightingModeLocal = HighlightingMode.LastSuccess;
            if(seqDef.ExecutionState == SequenceExecutionState.Fail)
                highlightingModeLocal = HighlightingMode.LastFail;

            env.PrintHighlighted(seqDef.Symbol + ": ", highlightingModeLocal);
            PrintSequence(seqDef.Seq, seqDef.Seq, highlightingMode);
        }

        private void PrintSequenceAssignContainerConstructorToVar(SequenceAssignContainerConstructorToVar seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seq.DestVar.Name + "=", highlightingMode);
            PrintSequenceExpression(seq.Constructor, seq, highlightingMode);
        }

        private void PrintChildren(Sequence seq, HighlightingMode highlightingModeChildren, HighlightingMode highlightingMode)
        {
            bool first = true;
            foreach(Sequence seqChild in seq.Children)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);
                PrintSequence(seqChild, seq, highlightingModeChildren);
            }
        }

        private void PrintChoice(SequenceRandomChoice seq)
        {
            if(!context.sequenceIdToChoicepointPosMap.ContainsKey(((SequenceBase)seq).Id))
                return; // tests/rules in sequence expressions are not choicepointable (at the moment)
            if(seq.Choice)
                env.PrintHighlighted("-%" + context.sequenceIdToChoicepointPosMap[((SequenceBase)seq).Id] + "-:", HighlightingMode.Choicepoint);
            else
                env.PrintHighlighted("+%" + context.sequenceIdToChoicepointPosMap[((SequenceBase)seq).Id] + "+:", HighlightingMode.Choicepoint);
        }

        private void PrintBreak(ISequenceSpecial seq)
        {
            if(!context.sequenceIdToBreakpointPosMap.ContainsKey(((SequenceBase)seq).Id))
                return; // tests/rules in sequence expressions are not breakpointable (at the moment)
            if(seq.Special)
                env.PrintHighlighted("-%" + context.sequenceIdToBreakpointPosMap[((SequenceBase)seq).Id] + "-:", HighlightingMode.Breakpoint);
            else
                env.PrintHighlighted("+%" + context.sequenceIdToBreakpointPosMap[((SequenceBase)seq).Id] + "+:", HighlightingMode.Breakpoint);
        }

        private void PrintListOfMatchesNumbers(ref int numCurTotalMatch, int numMatches)
        {
            env.PrintHighlighted("(", HighlightingMode.Choicepoint);
            bool first = true;
            for(int i = 0; i < numMatches; ++i)
            {
                if(!first)
                    env.PrintHighlighted(",", HighlightingMode.Choicepoint);
                env.PrintHighlighted(numCurTotalMatch.ToString(), HighlightingMode.Choicepoint);
                ++numCurTotalMatch;
                first = false;
            }
            env.PrintHighlighted(")", HighlightingMode.Choicepoint);
        }

        /// <summary>
        /// Called from shell after an debugging abort highlighting the lastly executed rule
        /// </summary>
        public static void PrintSequence(Sequence seq, Sequence highlight, IDebuggerEnvironment env)
        {
            DisplaySequenceContext context = new DisplaySequenceContext();
            context.highlightSeq = highlight;
            new SequencePrinter(env).DisplaySequence(seq, context, 0, "", "");
            // TODO: what to do if abort came within sequence called from top sequence?
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PrintSequenceComputation(SequenceComputation seqComp, SequenceBase parent, HighlightingMode highlightingMode)
        {
            switch(seqComp.SequenceComputationType)
            {
            case SequenceComputationType.Then:
                PrintSequenceComputationThen((SequenceComputationThen)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VAlloc:
                PrintSequenceComputationVAlloc((SequenceComputationVAlloc)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VFree:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VFreeNonReset:
            case SequenceComputationType.VReset:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ContainerAdd:
                PrintSequenceComputationContainerAdd((SequenceComputationContainerAdd)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ContainerRem:
                PrintSequenceComputationContainerRem((SequenceComputationContainerRem)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ContainerClear:
                PrintSequenceComputationContainerClear((SequenceComputationContainerClear)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Assignment:
                PrintSequenceComputationAssignment((SequenceComputationAssignment)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VariableDeclaration:
                PrintSequenceComputationVariableDeclaration((SequenceComputationVariableDeclaration)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Emit:
                PrintSequenceComputationEmit((SequenceComputationEmit)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Record:
                PrintSequenceComputationRecord((SequenceComputationRecord)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Export:
                PrintSequenceComputationExport((SequenceComputationExport)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.DeleteFile:
                PrintSequenceComputationDeleteFile((SequenceComputationDeleteFile)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphAdd:
                PrintSequenceComputationGraphAdd((SequenceComputationGraphAdd)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRem:
                PrintSequenceComputationGraphRem((SequenceComputationGraphRem)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphClear:
                PrintSequenceComputationGraphClear((SequenceComputationGraphClear)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRetype:
                PrintSequenceComputationGraphRetype((SequenceComputationGraphRetype)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphAddCopy:
                PrintSequenceComputationGraphAddCopy((SequenceComputationGraphAddCopy)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphMerge:
                PrintSequenceComputationGraphMerge((SequenceComputationGraphMerge)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRedirectSource:
                PrintSequenceComputationGraphRedirectSource((SequenceComputationGraphRedirectSource)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRedirectTarget:
                PrintSequenceComputationGraphRedirectTarget((SequenceComputationGraphRedirectTarget)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRedirectSourceAndTarget:
                PrintSequenceComputationGraphRedirectSourceAndTarget((SequenceComputationGraphRedirectSourceAndTarget)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Insert:
                PrintSequenceComputationInsert((SequenceComputationInsert)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.InsertCopy:
                PrintSequenceComputationInsertCopy((SequenceComputationInsertCopy)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.InsertInduced:
                PrintSequenceComputationInsertInduced((SequenceComputationInsertInduced)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.InsertDefined:
                PrintSequenceComputationInsertDefined((SequenceComputationInsertDefined)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ProcedureCall:
                PrintSequenceComputationProcedureCall((SequenceComputationProcedureCall)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.BuiltinProcedureCall:
                PrintSequenceComputationBuiltinProcedureCall((SequenceComputationBuiltinProcedureCall)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ProcedureMethodCall:
                PrintSequenceComputationProcedureMethodCall((SequenceComputationProcedureMethodCall)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.DebugAdd:
            case SequenceComputationType.DebugRem:
            case SequenceComputationType.DebugEmit:
            case SequenceComputationType.DebugHalt:
            case SequenceComputationType.DebugHighlight:
                PrintSequenceComputationDebug((SequenceComputationDebug)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Assert:
                PrintSequenceComputationAssert((SequenceComputationAssert)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.SynchronizationEnter:
                PrintSequenceComputationSynchronizationEnter((SequenceComputationSynchronizationEnter)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.SynchronizationTryEnter:
                PrintSequenceComputationSynchronizationTryEnter((SequenceComputationSynchronizationTryEnter)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.SynchronizationExit:
                PrintSequenceComputationSynchronizationExit((SequenceComputationSynchronizationExit)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GetEquivalentOrAdd:
                PrintSequenceComputationGetEquivalentOrAdd((SequenceComputationGetEquivalentOrAdd)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.AssignmentTarget: // every assignment target (lhs value) is a computation
                PrintSequenceAssignmentTarget((AssignmentTarget)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Expression: // every expression (rhs value) is a computation
                PrintSequenceExpression((SequenceExpression)seqComp, parent, highlightingMode);
                break;
            default:
                Debug.Assert(false);
                break;
            }
        }

        private void PrintSequenceComputationThen(SequenceComputationThen seqCompThen, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceComputation(seqCompThen.left, seqCompThen, highlightingMode);
            env.PrintHighlighted("; ", highlightingMode);
            if(seqCompThen.right is SequenceExpression)
            {
                env.PrintHighlighted("{", highlightingMode);
                PrintSequenceExpression((SequenceExpression)seqCompThen.right, seqCompThen, highlightingMode);
                env.PrintHighlighted("}", highlightingMode);
            }
            else
            {
                PrintSequenceComputation(seqCompThen.right, seqCompThen, highlightingMode);
            }
        }

        private void PrintSequenceComputationVAlloc(SequenceComputationVAlloc seqCompVAlloc, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("valloc()", highlightingMode);
        }

        private void PrintSequenceComputationVFree(SequenceComputationVFree seqCompVFree, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted((seqCompVFree.Reset ? "vfree" : "vfreenonreset") + "(", highlightingMode);
            PrintSequenceExpression(seqCompVFree.VisitedFlagExpression, seqCompVFree, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationVReset(SequenceComputationVReset seqCompVReset, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("vreset(", highlightingMode);
            PrintSequenceExpression(seqCompVReset.VisitedFlagExpression, seqCompVReset, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationContainerAdd(SequenceComputationContainerAdd seqCompContainerAdd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompContainerAdd.Name + ".add(", highlightingMode);
            PrintSequenceExpression(seqCompContainerAdd.Expr, seqCompContainerAdd, highlightingMode);
            if(seqCompContainerAdd.ExprDst != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompContainerAdd.ExprDst, seqCompContainerAdd, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationContainerRem(SequenceComputationContainerRem seqCompContainerRem, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompContainerRem.Name + ".rem(", highlightingMode);
            if(seqCompContainerRem.Expr != null)
            {
                PrintSequenceExpression(seqCompContainerRem.Expr, seqCompContainerRem, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationContainerClear(SequenceComputationContainerClear seqCompContainerClear, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompContainerClear.Name + ".clear()", highlightingMode);
        }

        private void PrintSequenceComputationAssignment(SequenceComputationAssignment seqCompAssign, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceAssignmentTarget(seqCompAssign.Target, seqCompAssign, highlightingMode);
            env.PrintHighlighted("=", highlightingMode);
            PrintSequenceComputation(seqCompAssign.SourceValueProvider, seqCompAssign, highlightingMode);
        }

        private void PrintSequenceComputationVariableDeclaration(SequenceComputationVariableDeclaration seqCompVarDecl, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompVarDecl.Target.Name, highlightingMode);
        }

        private void PrintSequenceComputationDebug(SequenceComputationDebug seqCompDebug, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Debug::" + seqCompDebug.Name + "(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression seqExpr in seqCompDebug.ArgExprs)
            {
                if(!first)
                    env.PrintHighlighted(", ", highlightingMode);
                else
                    first = false;
                PrintSequenceExpression(seqExpr, seqCompDebug, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationAssert(SequenceComputationAssert seqCompAssert, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompAssert.IsAlways)
                env.PrintHighlighted("assertAlways(", highlightingMode);
            else
                env.PrintHighlighted("assert(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression expr in seqCompAssert.ArgExprs)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);
                SequenceExpressionConstant exprConst = expr as SequenceExpressionConstant;
                if(exprConst != null && exprConst.Constant is string)
                    env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
                else
                    PrintSequenceExpression(expr, seqCompAssert, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationSynchronizationEnter(SequenceComputationSynchronizationEnter seqCompEnter, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Synchronization::enter(", highlightingMode);
            PrintSequenceExpression(seqCompEnter.LockObjectExpr, seqCompEnter, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationSynchronizationTryEnter(SequenceComputationSynchronizationTryEnter seqCompTryEnter, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Synchronization::tryenter(", highlightingMode);
            PrintSequenceExpression(seqCompTryEnter.LockObjectExpr, seqCompTryEnter, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationSynchronizationExit(SequenceComputationSynchronizationExit seqCompExit, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Synchronization::exit(", highlightingMode);
            PrintSequenceExpression(seqCompExit.LockObjectExpr, seqCompExit, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGetEquivalentOrAdd(SequenceComputationGetEquivalentOrAdd seqCompGetEquivalentOrAdd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("getEquivalentOrAdd(", highlightingMode);
            PrintSequenceExpression(seqCompGetEquivalentOrAdd.Subgraph, seqCompGetEquivalentOrAdd, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGetEquivalentOrAdd.SubgraphArray, seqCompGetEquivalentOrAdd, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationEmit(SequenceComputationEmit seqCompEmit, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompEmit.IsDebug)
                env.PrintHighlighted("emitdebug(", highlightingMode);
            else
                env.PrintHighlighted("emit(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression expr in seqCompEmit.Expressions)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);
                SequenceExpressionConstant exprConst = expr as SequenceExpressionConstant;
                if(exprConst != null && exprConst.Constant is string)
                    env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
                else
                    PrintSequenceExpression(expr, seqCompEmit, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationRecord(SequenceComputationRecord seqCompRec, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("record(", highlightingMode);

            SequenceExpressionConstant exprConst = seqCompRec.Expression as SequenceExpressionConstant;
            if(exprConst != null && exprConst.Constant is string)
                env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
            else
                PrintSequenceExpression(seqCompRec.Expression, seqCompRec, highlightingMode);

            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationExport(SequenceComputationExport seqCompExport, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::export(", highlightingMode);
            if(seqCompExport.Graph != null)
            {
                PrintSequenceExpression(seqCompExport.Graph, seqCompExport, highlightingMode);
                env.PrintHighlighted(", ", highlightingMode);
            }
            PrintSequenceExpression(seqCompExport.Name, seqCompExport, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationDeleteFile(SequenceComputationDeleteFile seqCompDelFile, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::deleteFile(", highlightingMode);
            PrintSequenceExpression(seqCompDelFile.Name, seqCompDelFile, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphAdd(SequenceComputationGraphAdd seqCompGraphAdd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("add(", highlightingMode);
            PrintSequenceExpression(seqCompGraphAdd.Expr, seqCompGraphAdd, highlightingMode);
            if(seqCompGraphAdd.ExprSrc != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAdd.ExprSrc, seqCompGraphAdd, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAdd.ExprDst, seqCompGraphAdd, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRem(SequenceComputationGraphRem seqCompGraphRem, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("rem(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRem.Expr, seqCompGraphRem, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphClear(SequenceComputationGraphClear seqCompGraphClear, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("clear()", highlightingMode);
        }

        private void PrintSequenceComputationGraphRetype(SequenceComputationGraphRetype seqCompGraphRetype, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("retype(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRetype.ElemExpr, seqCompGraphRetype, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRetype.TypeExpr, seqCompGraphRetype, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy seqCompGraphAddCopy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompGraphAddCopy.Deep ? "addCopy(" : "addClone(", highlightingMode);
            PrintSequenceExpression(seqCompGraphAddCopy.Expr, seqCompGraphAddCopy, highlightingMode);
            if(seqCompGraphAddCopy.ExprSrc != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAddCopy.ExprSrc, seqCompGraphAddCopy, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqCompGraphAddCopy.ExprDst, seqCompGraphAddCopy, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphMerge(SequenceComputationGraphMerge seqCompGraphMerge, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("merge(", highlightingMode);
            PrintSequenceExpression(seqCompGraphMerge.TargetNodeExpr, seqCompGraphMerge, highlightingMode);
            env.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqCompGraphMerge.SourceNodeExpr, seqCompGraphMerge, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRedirectSource(SequenceComputationGraphRedirectSource seqCompGraphRedirectSrc, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("redirectSource(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrc.EdgeExpr, seqCompGraphRedirectSrc, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrc.SourceNodeExpr, seqCompGraphRedirectSrc, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRedirectTarget(SequenceComputationGraphRedirectTarget seqCompGraphRedirectTgt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("redirectSourceAndTarget(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectTgt.EdgeExpr, seqCompGraphRedirectTgt, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectTgt.TargetNodeExpr, seqCompGraphRedirectTgt, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRedirectSourceAndTarget(SequenceComputationGraphRedirectSourceAndTarget seqCompGraphRedirectSrcTgt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("redirectSourceAndTarget(", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.EdgeExpr, seqCompGraphRedirectSrcTgt, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.SourceNodeExpr, seqCompGraphRedirectSrcTgt, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.TargetNodeExpr, seqCompGraphRedirectSrcTgt, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsert(SequenceComputationInsert seqCompInsert, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insert(", highlightingMode);
            PrintSequenceExpression(seqCompInsert.Graph, seqCompInsert, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsertCopy(SequenceComputationInsertCopy seqCompInsertCopy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insert(", highlightingMode);
            PrintSequenceExpression(seqCompInsertCopy.Graph, seqCompInsertCopy, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompInsertCopy.RootNode, seqCompInsertCopy, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsertInduced(SequenceComputationInsertInduced seqCompInsertInduced, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insertInduced(", highlightingMode);
            PrintSequenceExpression(seqCompInsertInduced.NodeSet, seqCompInsertInduced, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompInsertInduced.RootNode, seqCompInsertInduced, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsertDefined(SequenceComputationInsertDefined seqCompInsertDefined, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insertDefined(", highlightingMode);
            PrintSequenceExpression(seqCompInsertDefined.EdgeSet, seqCompInsertDefined, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqCompInsertDefined.RootEdge, seqCompInsertDefined, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationBuiltinProcedureCall(SequenceComputationBuiltinProcedureCall seqCompBuiltinProcCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompBuiltinProcCall.ReturnVars.Count > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompBuiltinProcCall.ReturnVars.Count; ++i)
                {
                    env.PrintHighlighted(seqCompBuiltinProcCall.ReturnVars[i].Name, highlightingMode);
                    if(i != seqCompBuiltinProcCall.ReturnVars.Count - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")=", highlightingMode);
            }
            PrintSequenceComputation(seqCompBuiltinProcCall.BuiltinProcedure, seqCompBuiltinProcCall, highlightingMode);
        }

        private void PrintSequenceComputationProcedureCall(SequenceComputationProcedureCall seqCompProcCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompProcCall.ReturnVars.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompProcCall.ReturnVars.Length; ++i)
                {
                    env.PrintHighlighted(seqCompProcCall.ReturnVars[i].Name, highlightingMode);
                    if(i != seqCompProcCall.ReturnVars.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")=", highlightingMode);
            }
            env.PrintHighlighted(seqCompProcCall.Name, highlightingMode);
            if(seqCompProcCall.ArgumentExpressions.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompProcCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(seqCompProcCall.ArgumentExpressions[i], seqCompProcCall, highlightingMode);
                    if(i != seqCompProcCall.ArgumentExpressions.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")", highlightingMode);
            }
        }

        private void PrintSequenceComputationProcedureMethodCall(SequenceComputationProcedureMethodCall sequenceComputationProcedureMethodCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(sequenceComputationProcedureMethodCall.ReturnVars.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ReturnVars.Length; ++i)
                {
                    env.PrintHighlighted(sequenceComputationProcedureMethodCall.ReturnVars[i].Name, highlightingMode);
                    if(i != sequenceComputationProcedureMethodCall.ReturnVars.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")=", highlightingMode);
            }
            if(sequenceComputationProcedureMethodCall.TargetExpr != null)
            {
                PrintSequenceExpression(sequenceComputationProcedureMethodCall.TargetExpr, sequenceComputationProcedureMethodCall, highlightingMode);
                env.PrintHighlighted(".", highlightingMode);
            }
            if(sequenceComputationProcedureMethodCall.TargetVar != null)
                env.PrintHighlighted(sequenceComputationProcedureMethodCall.TargetVar.ToString() + ".", highlightingMode);
            env.PrintHighlighted(sequenceComputationProcedureMethodCall.Name, highlightingMode);
            if(sequenceComputationProcedureMethodCall.ArgumentExpressions.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(sequenceComputationProcedureMethodCall.ArgumentExpressions[i], sequenceComputationProcedureMethodCall, highlightingMode);
                    if(i != sequenceComputationProcedureMethodCall.ArgumentExpressions.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")", highlightingMode);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PrintSequenceAssignmentTarget(AssignmentTarget assTgt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            switch(assTgt.AssignmentTargetType)
            {
            case AssignmentTargetType.Var:
                PrintSequenceAssignmentTargetVar((AssignmentTargetVar)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.YieldingToVar:
                PrintSequenceAssignmentTargetYieldingVar((AssignmentTargetYieldingVar)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.IndexedVar:
                PrintSequenceAssignmentTargetIndexedVar((AssignmentTargetIndexedVar)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.Attribute:
                PrintSequenceAssignmentTargetAttribute((AssignmentTargetAttribute)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.AttributeIndexed:
                PrintSequenceAssignmentTargetAttributeIndexed((AssignmentTargetAttributeIndexed)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.Visited:
                PrintSequenceAssignmentTargetVisited((AssignmentTargetVisited)assTgt, parent, highlightingMode);
                break;
            }
        }

        private void PrintSequenceAssignmentTargetVar(AssignmentTargetVar assTgtVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtVar.DestVar.Name, highlightingMode);
        }

        private void PrintSequenceAssignmentTargetYieldingVar(AssignmentTargetYieldingVar assTgtYieldingVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtYieldingVar.DestVar.Name, highlightingMode);
        }

        private void PrintSequenceAssignmentTargetIndexedVar(AssignmentTargetIndexedVar assTgtIndexedVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtIndexedVar.DestVar.Name + "[", highlightingMode);
            PrintSequenceExpression(assTgtIndexedVar.KeyExpression, assTgtIndexedVar, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceAssignmentTargetAttribute(AssignmentTargetAttribute assTgtAttribute, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtAttribute.DestVar.Name + "." + assTgtAttribute.AttributeName, highlightingMode);
        }

        private void PrintSequenceAssignmentTargetAttributeIndexed(AssignmentTargetAttributeIndexed assTgtAttributeIndexed, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtAttributeIndexed.DestVar.Name + "." + assTgtAttributeIndexed.AttributeName + "[", highlightingMode);
            PrintSequenceExpression(assTgtAttributeIndexed.KeyExpression, assTgtAttributeIndexed, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceAssignmentTargetVisited(AssignmentTargetVisited assTgtVisited, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtVisited.GraphElementVar.Name + ".visited", highlightingMode);
            if(assTgtVisited.VisitedFlagExpression != null)
            {
                env.PrintHighlighted("[", highlightingMode);
                PrintSequenceExpression(assTgtVisited.VisitedFlagExpression, assTgtVisited, highlightingMode);
                env.PrintHighlighted("]", highlightingMode);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PrintSequenceExpression(SequenceExpression seqExpr, SequenceBase parent, HighlightingMode highlightingMode)
        {
            switch(seqExpr.SequenceExpressionType)
            {
            case SequenceExpressionType.Conditional:
                PrintSequenceExpressionConditional((SequenceExpressionConditional)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Except:
            case SequenceExpressionType.LazyOr:
            case SequenceExpressionType.LazyAnd:
            case SequenceExpressionType.StrictOr:
            case SequenceExpressionType.StrictXor:
            case SequenceExpressionType.StrictAnd:
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Not:
                PrintSequenceExpressionNot((SequenceExpressionNot)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.UnaryPlus:
                PrintSequenceExpressionUnaryPlus((SequenceExpressionUnaryPlus)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.UnaryMinus:
                PrintSequenceExpressionUnaryMinus((SequenceExpressionUnaryMinus)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.BitwiseComplement:
                PrintSequenceExpressionBitwiseComplement((SequenceExpressionBitwiseComplement)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Cast:
                PrintSequenceExpressionCast((SequenceExpressionCast)seqExpr, parent, highlightingMode);
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
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Constant:
                PrintSequenceExpressionConstant((SequenceExpressionConstant)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Variable:
                PrintSequenceExpressionVariable((SequenceExpressionVariable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.This:
                PrintSequenceExpressionThis((SequenceExpressionThis)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.New:
                PrintSequenceExpressionNew((SequenceExpressionNew)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MatchClassConstructor:
                PrintSequenceExpressionMatchClassConstructor((SequenceExpressionMatchClassConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.SetConstructor:
                PrintSequenceExpressionSetConstructor((SequenceExpressionSetConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapConstructor:
                PrintSequenceExpressionMapConstructor((SequenceExpressionMapConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayConstructor:
                PrintSequenceExpressionArrayConstructor((SequenceExpressionArrayConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DequeConstructor:
                PrintSequenceExpressionDequeConstructor((SequenceExpressionDequeConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.SetCopyConstructor:
                PrintSequenceExpressionSetCopyConstructor((SequenceExpressionSetCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapCopyConstructor:
                PrintSequenceExpressionMapCopyConstructor((SequenceExpressionMapCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayCopyConstructor:
                PrintSequenceExpressionArrayCopyConstructor((SequenceExpressionArrayCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DequeCopyConstructor:
                PrintSequenceExpressionDequeCopyConstructor((SequenceExpressionDequeCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerAsArray:
                PrintSequenceExpressionContainerAsArray((SequenceExpressionContainerAsArray)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringLength:
                PrintSequenceExpressionStringLength((SequenceExpressionStringLength)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringStartsWith:
                PrintSequenceExpressionStringStartsWith((SequenceExpressionStringStartsWith)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringEndsWith:
                PrintSequenceExpressionStringEndsWith((SequenceExpressionStringEndsWith)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringSubstring:
                PrintSequenceExpressionStringSubstring((SequenceExpressionStringSubstring)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringReplace:
                PrintSequenceExpressionStringReplace((SequenceExpressionStringReplace)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringToLower:
                PrintSequenceExpressionStringToLower((SequenceExpressionStringToLower)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringToUpper:
                PrintSequenceExpressionStringToUpper((SequenceExpressionStringToUpper)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringAsArray:
                PrintSequenceExpressionStringAsArray((SequenceExpressionStringAsArray)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapDomain:
                PrintSequenceExpressionMapDomain((SequenceExpressionMapDomain)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapRange:
                PrintSequenceExpressionMapRange((SequenceExpressionMapRange)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Random:
                PrintSequenceExpressionRandom((SequenceExpressionRandom)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Def:
                PrintSequenceExpressionDef((SequenceExpressionDef)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsVisited:
                PrintSequenceExpressionIsVisited((SequenceExpressionIsVisited)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.InContainerOrString:
                PrintSequenceExpressionInContainerOrString((SequenceExpressionInContainerOrString)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerEmpty:
                PrintSequenceExpressionContainerEmpty((SequenceExpressionContainerEmpty)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerSize:
                PrintSequenceExpressionContainerSize((SequenceExpressionContainerSize)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerAccess:
                PrintSequenceExpressionContainerAccess((SequenceExpressionContainerAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerPeek:
                PrintSequenceExpressionContainerPeek((SequenceExpressionContainerPeek)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringIndexOf((SequenceExpressionArrayOrDequeOrStringIndexOf)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringLastIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf((SequenceExpressionArrayOrDequeOrStringLastIndexOf)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayIndexOfOrdered:
                PrintSequenceExpressionArrayIndexOfOrdered((SequenceExpressionArrayIndexOfOrdered)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArraySum:
                PrintSequenceExpressionArraySum((SequenceExpressionArraySum)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayProd:
                PrintSequenceExpressionArrayProd((SequenceExpressionArrayProd)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrSetMin:
                PrintSequenceExpressionArrayOrSetMin((SequenceExpressionArrayOrSetMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrSetMax:
                PrintSequenceExpressionArrayOrSetMax((SequenceExpressionArrayOrSetMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAvg:
                PrintSequenceExpressionArrayAvg((SequenceExpressionArrayAvg)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMed:
                PrintSequenceExpressionArrayMed((SequenceExpressionArrayMed)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMedUnordered:
                PrintSequenceExpressionArrayMedUnordered((SequenceExpressionArrayMedUnordered)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayVar:
                PrintSequenceExpressionArrayVar((SequenceExpressionArrayVar)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayDev:
                PrintSequenceExpressionArrayDev((SequenceExpressionArrayDev)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAnd:
                PrintSequenceExpressionArrayAnd((SequenceExpressionArrayAnd)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOr:
                PrintSequenceExpressionArrayOr((SequenceExpressionArrayOr)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrDequeAsSet:
                PrintSequenceExpressionArrayOrDequeAsSet((SequenceExpressionArrayOrDequeAsSet)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAsMap:
                PrintSequenceExpressionArrayAsMap((SequenceExpressionArrayAsMap)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAsDeque:
                PrintSequenceExpressionArrayAsDeque((SequenceExpressionArrayAsDeque)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAsString:
                PrintSequenceExpressionArrayAsString((SequenceExpressionArrayAsString)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArraySubarray:
                PrintSequenceExpressionArraySubarray((SequenceExpressionArraySubarray)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DequeSubdeque:
                PrintSequenceExpressionDequeSubdeque((SequenceExpressionDequeSubdeque)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderAscending:
                PrintSequenceExpressionArrayOrderAscending((SequenceExpressionArrayOrderAscending)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderDescending:
                PrintSequenceExpressionArrayOrderDescending((SequenceExpressionArrayOrderDescending)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayGroup:
                PrintSequenceExpressionArrayGroup((SequenceExpressionArrayGroup)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayKeepOneForEach:
                PrintSequenceExpressionArrayKeepOneForEach((SequenceExpressionArrayKeepOneForEach)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayReverse:
                PrintSequenceExpressionArrayReverse((SequenceExpressionArrayReverse)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayShuffle:
                PrintSequenceExpressionArrayShuffle((SequenceExpressionArrayShuffle)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayExtract:
                PrintSequenceExpressionArrayExtract((SequenceExpressionArrayExtract)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderAscendingBy:
                PrintSequenceExpressionArrayOrderAscendingBy((SequenceExpressionArrayOrderAscendingBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderDescendingBy:
                PrintSequenceExpressionArrayOrderDescendingBy((SequenceExpressionArrayOrderDescendingBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayGroupBy:
                PrintSequenceExpressionArrayGroupBy((SequenceExpressionArrayGroupBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayKeepOneForEachBy:
                PrintSequenceExpressionArrayKeepOneForEachBy((SequenceExpressionArrayKeepOneForEachBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayIndexOfBy:
                PrintSequenceExpressionArrayIndexOfBy((SequenceExpressionArrayIndexOfBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayLastIndexOfBy:
                PrintSequenceExpressionArrayLastIndexOfBy((SequenceExpressionArrayLastIndexOfBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayIndexOfOrderedBy:
                PrintSequenceExpressionArrayIndexOfOrderedBy((SequenceExpressionArrayIndexOfOrderedBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMap:
                PrintSequenceExpressionArrayMap((SequenceExpressionArrayMap)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayRemoveIf:
                PrintSequenceExpressionArrayRemoveIf((SequenceExpressionArrayRemoveIf)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMapStartWithAccumulateBy:
                PrintSequenceExpressionArrayMapStartWithAccumulateBy((SequenceExpressionArrayMapStartWithAccumulateBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ElementFromGraph:
                PrintSequenceExpressionElementFromGraph((SequenceExpressionElementFromGraph)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.NodeByName:
                PrintSequenceExpressionNodeByName((SequenceExpressionNodeByName)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.EdgeByName:
                PrintSequenceExpressionEdgeByName((SequenceExpressionEdgeByName)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.NodeByUnique:
                PrintSequenceExpressionNodeByUnique((SequenceExpressionNodeByUnique)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.EdgeByUnique:
                PrintSequenceExpressionEdgeByUnique((SequenceExpressionEdgeByUnique)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Source:
                PrintSequenceExpressionSource((SequenceExpressionSource)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Target:
                PrintSequenceExpressionTarget((SequenceExpressionTarget)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Opposite:
                PrintSequenceExpressionOpposite((SequenceExpressionOpposite)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.GraphElementAttributeOrElementOfMatch:
                PrintSequenceExpressionAttributeOrMatchAccess((SequenceExpressionAttributeOrMatchAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.GraphElementAttribute:
                PrintSequenceExpressionAttributeAccess((SequenceExpressionAttributeAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ElementOfMatch:
                PrintSequenceExpressionMatchAccess((SequenceExpressionMatchAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Nodes:
                PrintSequenceExpressionNodes((SequenceExpressionNodes)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Edges:
                PrintSequenceExpressionEdges((SequenceExpressionEdges)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountNodes:
                PrintSequenceExpressionCountNodes((SequenceExpressionCountNodes)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountEdges:
                PrintSequenceExpressionCountEdges((SequenceExpressionCountEdges)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Now:
                PrintSequenceExpressionNow((SequenceExpressionNow)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathMin:
                PrintSequenceExpressionMathMin((SequenceExpressionMathMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathMax:
                PrintSequenceExpressionMathMax((SequenceExpressionMathMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathAbs:
                PrintSequenceExpressionMathAbs((SequenceExpressionMathAbs)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathCeil:
                PrintSequenceExpressionMathCeil((SequenceExpressionMathCeil)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathFloor:
                PrintSequenceExpressionMathFloor((SequenceExpressionMathFloor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathRound:
                PrintSequenceExpressionMathRound((SequenceExpressionMathRound)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathTruncate:
                PrintSequenceExpressionMathTruncate((SequenceExpressionMathTruncate)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSqr:
                PrintSequenceExpressionMathSqr((SequenceExpressionMathSqr)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSqrt:
                PrintSequenceExpressionMathSqrt((SequenceExpressionMathSqrt)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathPow:
                PrintSequenceExpressionMathPow((SequenceExpressionMathPow)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathLog:
                PrintSequenceExpressionMathLog((SequenceExpressionMathLog)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSgn:
                PrintSequenceExpressionMathSgn((SequenceExpressionMathSgn)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSin:
                PrintSequenceExpressionMathSin((SequenceExpressionMathSin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathCos:
                PrintSequenceExpressionMathCos((SequenceExpressionMathCos)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathTan:
                PrintSequenceExpressionMathTan((SequenceExpressionMathTan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathArcSin:
                PrintSequenceExpressionMathArcSin((SequenceExpressionMathArcSin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathArcCos:
                PrintSequenceExpressionMathArcCos((SequenceExpressionMathArcCos)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathArcTan:
                PrintSequenceExpressionMathArcTan((SequenceExpressionMathArcTan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathPi:
                PrintSequenceExpressionMathPi((SequenceExpressionMathPi)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathE:
                PrintSequenceExpressionMathE((SequenceExpressionMathE)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathByteMin:
                PrintSequenceExpressionMathByteMin((SequenceExpressionMathByteMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathByteMax:
                PrintSequenceExpressionMathByteMax((SequenceExpressionMathByteMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathShortMin:
                PrintSequenceExpressionMathShortMin((SequenceExpressionMathShortMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathShortMax:
                PrintSequenceExpressionMathShortMax((SequenceExpressionMathShortMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathIntMin:
                PrintSequenceExpressionMathIntMin((SequenceExpressionMathIntMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathIntMax:
                PrintSequenceExpressionMathIntMax((SequenceExpressionMathIntMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathLongMin:
                PrintSequenceExpressionMathLongMin((SequenceExpressionMathLongMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathLongMax:
                PrintSequenceExpressionMathLongMax((SequenceExpressionMathLongMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathFloatMin:
                PrintSequenceExpressionMathFloatMin((SequenceExpressionMathFloatMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathFloatMax:
                PrintSequenceExpressionMathFloatMax((SequenceExpressionMathFloatMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathDoubleMin:
                PrintSequenceExpressionMathDoubleMin((SequenceExpressionMathDoubleMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathDoubleMax:
                PrintSequenceExpressionMathDoubleMax((SequenceExpressionMathDoubleMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Empty:
                PrintSequenceExpressionEmpty((SequenceExpressionEmpty)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Size:
                PrintSequenceExpressionSize((SequenceExpressionSize)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.AdjacentNodes:
            case SequenceExpressionType.AdjacentNodesViaIncoming:
            case SequenceExpressionType.AdjacentNodesViaOutgoing:
            case SequenceExpressionType.IncidentEdges:
            case SequenceExpressionType.IncomingEdges:
            case SequenceExpressionType.OutgoingEdges:
                PrintSequenceExpressionAdjacentIncident((SequenceExpressionAdjacentIncident)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ReachableNodes:
            case SequenceExpressionType.ReachableNodesViaIncoming:
            case SequenceExpressionType.ReachableNodesViaOutgoing:
            case SequenceExpressionType.ReachableEdges:
            case SequenceExpressionType.ReachableEdgesViaIncoming:
            case SequenceExpressionType.ReachableEdgesViaOutgoing:
                PrintSequenceExpressionReachable((SequenceExpressionReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.BoundedReachableNodes:
            case SequenceExpressionType.BoundedReachableNodesViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.BoundedReachableEdges:
            case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionBoundedReachable((SequenceExpressionBoundedReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                PrintSequenceExpressionBoundedReachableWithRemainingDepth((SequenceExpressionBoundedReachableWithRemainingDepth)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountAdjacentNodes:
            case SequenceExpressionType.CountAdjacentNodesViaIncoming:
            case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
            case SequenceExpressionType.CountIncidentEdges:
            case SequenceExpressionType.CountIncomingEdges:
            case SequenceExpressionType.CountOutgoingEdges:
                PrintSequenceExpressionCountAdjacentIncident((SequenceExpressionCountAdjacentIncident)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountReachableNodes:
            case SequenceExpressionType.CountReachableNodesViaIncoming:
            case SequenceExpressionType.CountReachableNodesViaOutgoing:
            case SequenceExpressionType.CountReachableEdges:
            case SequenceExpressionType.CountReachableEdgesViaIncoming:
            case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountReachable((SequenceExpressionCountReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountBoundedReachableNodes:
            case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.CountBoundedReachableEdges:
            case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountBoundedReachable((SequenceExpressionCountBoundedReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsAdjacentNodes:
            case SequenceExpressionType.IsAdjacentNodesViaIncoming:
            case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
            case SequenceExpressionType.IsIncidentEdges:
            case SequenceExpressionType.IsIncomingEdges:
            case SequenceExpressionType.IsOutgoingEdges:
                PrintSequenceExpressionIsAdjacentIncident((SequenceExpressionIsAdjacentIncident)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsReachableNodes:
            case SequenceExpressionType.IsReachableNodesViaIncoming:
            case SequenceExpressionType.IsReachableNodesViaOutgoing:
            case SequenceExpressionType.IsReachableEdges:
            case SequenceExpressionType.IsReachableEdgesViaIncoming:
            case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsReachable((SequenceExpressionIsReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsBoundedReachableNodes:
            case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.IsBoundedReachableEdges:
            case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsBoundedReachable((SequenceExpressionIsBoundedReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.InducedSubgraph:
                PrintSequenceExpressionInducedSubgraph((SequenceExpressionInducedSubgraph)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DefinedSubgraph:
                PrintSequenceExpressionDefinedSubgraph((SequenceExpressionDefinedSubgraph)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.EqualsAny:
                PrintSequenceExpressionEqualsAny((SequenceExpressionEqualsAny)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.GetEquivalent:
                PrintSequenceExpressionGetEquivalent((SequenceExpressionGetEquivalent)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Nameof:
                PrintSequenceExpressionNameof((SequenceExpressionNameof)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Uniqueof:
                PrintSequenceExpressionUniqueof((SequenceExpressionUniqueof)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Typeof:
                PrintSequenceExpressionTypeof((SequenceExpressionTypeof)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ExistsFile:
                PrintSequenceExpressionExistsFile((SequenceExpressionExistsFile)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Import:
                PrintSequenceExpressionImport((SequenceExpressionImport)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Copy:
                PrintSequenceExpressionCopy((SequenceExpressionCopy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Canonize:
                PrintSequenceExpressionCanonize((SequenceExpressionCanonize)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.RuleQuery:
                PrintSequenceExpressionRuleQuery((SequenceExpressionRuleQuery)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MultiRuleQuery:
                PrintSequenceExpressionMultiRuleQuery((SequenceExpressionMultiRuleQuery)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MappingClause:
                PrintSequenceExpressionMappingClause((SequenceExpressionMappingClause)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Scan:
                PrintSequenceExpressionScan((SequenceExpressionScan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.TryScan:
                PrintSequenceExpressionTryScan((SequenceExpressionTryScan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.FunctionCall:
                PrintSequenceExpressionFunctionCall((SequenceExpressionFunctionCall)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.FunctionMethodCall:
                PrintSequenceExpressionFunctionMethodCall((SequenceExpressionFunctionMethodCall)seqExpr, parent, highlightingMode);
                break;
            }
        }

        private void PrintSequenceExpressionConditional(SequenceExpressionConditional seqExprCond, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprCond.Condition, seqExprCond, highlightingMode);
            env.PrintHighlighted(" ? ", highlightingMode);
            PrintSequenceExpression(seqExprCond.TrueCase, seqExprCond, highlightingMode);
            env.PrintHighlighted(" : ", highlightingMode);
            PrintSequenceExpression(seqExprCond.FalseCase, seqExprCond, highlightingMode);
        }

        private void PrintSequenceExpressionBinary(SequenceBinaryExpression seqExprBin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprBin.Left, seqExprBin, highlightingMode);
            env.PrintHighlighted(seqExprBin.Operator, highlightingMode);
            PrintSequenceExpression(seqExprBin.Right, seqExprBin, highlightingMode);
        }

        private void PrintSequenceExpressionNot(SequenceExpressionNot seqExprNot, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("!", highlightingMode);
            PrintSequenceExpression(seqExprNot.Operand, seqExprNot, highlightingMode);
        }

        private void PrintSequenceExpressionUnaryPlus(SequenceExpressionUnaryPlus seqExprUnaryPlus, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("+", highlightingMode);
            PrintSequenceExpression(seqExprUnaryPlus.Operand, seqExprUnaryPlus, highlightingMode);
        }

        private void PrintSequenceExpressionUnaryMinus(SequenceExpressionUnaryMinus seqExprUnaryMinus, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("-", highlightingMode);
            PrintSequenceExpression(seqExprUnaryMinus.Operand, seqExprUnaryMinus, highlightingMode);
        }

        private void PrintSequenceExpressionBitwiseComplement(SequenceExpressionBitwiseComplement seqExprBitwiseComplement, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("~", highlightingMode);
            PrintSequenceExpression(seqExprBitwiseComplement.Operand, seqExprBitwiseComplement, highlightingMode);
        }

        private void PrintSequenceExpressionCast(SequenceExpressionCast seqExprCast, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("(", highlightingMode);
            env.PrintHighlighted(((InheritanceType)seqExprCast.TargetType).Name, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
            PrintSequenceExpression(seqExprCast.Operand, seqExprCast, highlightingMode);
        }

        private void PrintSequenceExpressionConstant(SequenceExpressionConstant seqExprConstant, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(seqExprConstant.Constant), highlightingMode);
        }

        private void PrintSequenceExpressionVariable(SequenceExpressionVariable seqExprVariable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprVariable.Variable.Name, highlightingMode);
        }

        private void PrintSequenceExpressionNew(SequenceExpressionNew seqExprNew, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprNew.ConstructedType, highlightingMode); // TODO: check -- looks suspicious
        }

        private void PrintSequenceExpressionThis(SequenceExpressionThis seqExprThis, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("this", highlightingMode);
        }

        private void PrintSequenceExpressionMatchClassConstructor(SequenceExpressionMatchClassConstructor seqExprMatchClassConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("match<class " + seqExprMatchClassConstructor.ConstructedType + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionSetConstructor(SequenceExpressionSetConstructor seqExprSetConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("set<", highlightingMode);
            env.PrintHighlighted(seqExprSetConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">{", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprSetConstructor, seqExprSetConstructor, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionMapConstructor(SequenceExpressionMapConstructor seqExprMapConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("map<", highlightingMode);
            env.PrintHighlighted(seqExprMapConstructor.KeyType, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            env.PrintHighlighted(seqExprMapConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">{", highlightingMode);
            for(int i = 0; i < seqExprMapConstructor.MapKeyItems.Length; ++i)
            {
                PrintSequenceExpression(seqExprMapConstructor.MapKeyItems[i], seqExprMapConstructor, highlightingMode);
                env.PrintHighlighted("->", highlightingMode);
                PrintSequenceExpression(seqExprMapConstructor.ContainerItems[i], seqExprMapConstructor, highlightingMode);
                if(i != seqExprMapConstructor.MapKeyItems.Length - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayConstructor(SequenceExpressionArrayConstructor seqExprArrayConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("array<", highlightingMode);
            env.PrintHighlighted(seqExprArrayConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprArrayConstructor, seqExprArrayConstructor, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionDequeConstructor(SequenceExpressionDequeConstructor seqExprDequeConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("deque<", highlightingMode);
            env.PrintHighlighted(seqExprDequeConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">]", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprDequeConstructor, seqExprDequeConstructor, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
        }

        private void PrintSequenceExpressionContainerConstructor(SequenceExpressionContainerConstructor seqExprContainerConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            for(int i = 0; i < seqExprContainerConstructor.ContainerItems.Length; ++i)
            {
                PrintSequenceExpression(seqExprContainerConstructor.ContainerItems[i], seqExprContainerConstructor, highlightingMode);
                if(i != seqExprContainerConstructor.ContainerItems.Length - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
        }

        private void PrintSequenceExpressionSetCopyConstructor(SequenceExpressionSetCopyConstructor seqExprSetCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("set<", highlightingMode);
            env.PrintHighlighted(seqExprSetCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">(", highlightingMode);
            PrintSequenceExpression(seqExprSetCopyConstructor.SetToCopy, seqExprSetCopyConstructor, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMapCopyConstructor(SequenceExpressionMapCopyConstructor seqExprMapCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("map<", highlightingMode);
            env.PrintHighlighted(seqExprMapCopyConstructor.KeyType, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            env.PrintHighlighted(seqExprMapCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">(", highlightingMode);
            PrintSequenceExpression(seqExprMapCopyConstructor.MapToCopy, seqExprMapCopyConstructor, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayCopyConstructor(SequenceExpressionArrayCopyConstructor seqExprArrayCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("array<", highlightingMode);
            env.PrintHighlighted(seqExprArrayCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpression(seqExprArrayCopyConstructor.ArrayToCopy, seqExprArrayCopyConstructor, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionDequeCopyConstructor(SequenceExpressionDequeCopyConstructor seqExprDequeCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("deque<", highlightingMode);
            env.PrintHighlighted(seqExprDequeCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpression(seqExprDequeCopyConstructor.DequeToCopy, seqExprDequeCopyConstructor, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionContainerAsArray(SequenceExpressionContainerAsArray seqExprContainerAsArray, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprContainerAsArray.Name + ".asArray()", highlightingMode);
        }

        private void PrintSequenceExpressionStringLength(SequenceExpressionStringLength seqExprStringLength, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringLength.StringExpr, seqExprStringLength, highlightingMode);
            env.PrintHighlighted(".length()", highlightingMode);
        }

        private void PrintSequenceExpressionStringStartsWith(SequenceExpressionStringStartsWith seqExprStringStartsWith, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringStartsWith.StringExpr, seqExprStringStartsWith, highlightingMode);
            env.PrintHighlighted(".startsWith(", highlightingMode);
            PrintSequenceExpression(seqExprStringStartsWith.StringToSearchForExpr, seqExprStringStartsWith, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringEndsWith(SequenceExpressionStringEndsWith seqExprStringEndsWith, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringEndsWith.StringExpr, seqExprStringEndsWith, highlightingMode);
            env.PrintHighlighted(".endsWith(", highlightingMode);
            PrintSequenceExpression(seqExprStringEndsWith.StringToSearchForExpr, seqExprStringEndsWith, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringSubstring(SequenceExpressionStringSubstring seqExprStringSubstring, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringSubstring.StringExpr, seqExprStringSubstring, highlightingMode);
            env.PrintHighlighted(".substring(", highlightingMode);
            PrintSequenceExpression(seqExprStringSubstring.StartIndexExpr, seqExprStringSubstring, highlightingMode);
            if(seqExprStringSubstring.LengthExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprStringSubstring.LengthExpr, seqExprStringSubstring, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringReplace(SequenceExpressionStringReplace seqExprStringReplace, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringReplace.StringExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(".replace(", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.StartIndexExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.LengthExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.ReplaceStringExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringToLower(SequenceExpressionStringToLower seqExprStringToLower, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringToLower.StringExpr, seqExprStringToLower, highlightingMode);
            env.PrintHighlighted(".toLower()", highlightingMode);
        }

        private void PrintSequenceExpressionStringToUpper(SequenceExpressionStringToUpper seqExprStringToUpper, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringToUpper.StringExpr, seqExprStringToUpper, highlightingMode);
            env.PrintHighlighted(".toUpper()", highlightingMode);
        }

        private void PrintSequenceExpressionStringAsArray(SequenceExpressionStringAsArray seqExprStringAsArray, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringAsArray.StringExpr, seqExprStringAsArray, highlightingMode);
            env.PrintHighlighted(".asArray(", highlightingMode);
            PrintSequenceExpression(seqExprStringAsArray.SeparatorExpr, seqExprStringAsArray, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionRandom(SequenceExpressionRandom seqExprRandom, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("random(", highlightingMode);
            if(seqExprRandom.UpperBound != null)
                PrintSequenceExpression(seqExprRandom.UpperBound, seqExprRandom, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionDef(SequenceExpressionDef seqExprDef, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("def(", highlightingMode);
            for(int i = 0; i < seqExprDef.DefVars.Length; ++i)
            {
                PrintSequenceExpression(seqExprDef.DefVars[i], seqExprDef, highlightingMode);
                if(i != seqExprDef.DefVars.Length - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsVisited(SequenceExpressionIsVisited seqExprIsVisited, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprIsVisited.GraphElementVarExpr, seqExprIsVisited, highlightingMode);
            env.PrintHighlighted(".visited", highlightingMode);
            if(seqExprIsVisited.VisitedFlagExpr != null)
            {
                env.PrintHighlighted("[", highlightingMode);
                PrintSequenceExpression(seqExprIsVisited.VisitedFlagExpr, seqExprIsVisited, highlightingMode);
                env.PrintHighlighted("]", highlightingMode);
            }
        }

        private void PrintSequenceExpressionInContainerOrString(SequenceExpressionInContainerOrString seqExprInContainerOrString, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprInContainerOrString.Expr, seqExprInContainerOrString, highlightingMode);
            env.PrintHighlighted(" in ", highlightingMode);
            PrintSequenceExpression(seqExprInContainerOrString.ContainerOrStringExpr, seqExprInContainerOrString, highlightingMode);
        }

        private void PrintSequenceExpressionContainerSize(SequenceExpressionContainerSize seqExprContainerSize, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerSize.ContainerExpr, seqExprContainerSize, highlightingMode);
            env.PrintHighlighted(".size()", highlightingMode);
        }

        private void PrintSequenceExpressionContainerEmpty(SequenceExpressionContainerEmpty seqExprContainerEmpty, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerEmpty.ContainerExpr, seqExprContainerEmpty, highlightingMode);
            env.PrintHighlighted(".empty()", highlightingMode);
        }

        private void PrintSequenceExpressionContainerAccess(SequenceExpressionContainerAccess seqExprContainerAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerAccess.ContainerExpr, seqExprContainerAccess, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
            PrintSequenceExpression(seqExprContainerAccess.KeyExpr, seqExprContainerAccess, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionContainerPeek(SequenceExpressionContainerPeek seqExprContainerPeek, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerPeek.ContainerExpr, seqExprContainerPeek, highlightingMode);
            env.PrintHighlighted(".peek(", highlightingMode);
            if(seqExprContainerPeek.KeyExpr != null)
                PrintSequenceExpression(seqExprContainerPeek.KeyExpr, seqExprContainerPeek, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrDequeOrStringIndexOf(SequenceExpressionArrayOrDequeOrStringIndexOf seqExprArrayOrDequeOrStringIndexOf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode);
            env.PrintHighlighted(".indexOf(", highlightingMode);
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode);
            if(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf(SequenceExpressionArrayOrDequeOrStringLastIndexOf seqExprArrayOrDequeOrStringLastIndexOf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode);
            env.PrintHighlighted(".lastIndexOf(", highlightingMode);
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode);
            if(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayIndexOfOrdered(SequenceExpressionArrayIndexOfOrdered seqExprArrayIndexOfOrdered, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ContainerExpr, seqExprArrayIndexOfOrdered, highlightingMode);
            env.PrintHighlighted(".indexOfOrdered(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ValueToSearchForExpr, seqExprArrayIndexOfOrdered, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArraySum(SequenceExpressionArraySum seqExprArraySum, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArraySum.ContainerExpr, seqExprArraySum, highlightingMode);
            env.PrintHighlighted(".sum()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayProd(SequenceExpressionArrayProd seqExprArrayProd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayProd.ContainerExpr, seqExprArrayProd, highlightingMode);
            env.PrintHighlighted(".prod()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrSetMin(SequenceExpressionArrayOrSetMin seqExprArrayMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMin.ContainerExpr, seqExprArrayMin, highlightingMode);
            env.PrintHighlighted(".min()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrSetMax(SequenceExpressionArrayOrSetMax seqExprArrayMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMax.ContainerExpr, seqExprArrayMax, highlightingMode);
            env.PrintHighlighted(".max()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAvg(SequenceExpressionArrayAvg seqExprArrayAvg, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAvg.ContainerExpr, seqExprArrayAvg, highlightingMode);
            env.PrintHighlighted(".avg()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMed(SequenceExpressionArrayMed seqExprArrayMed, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMed.ContainerExpr, seqExprArrayMed, highlightingMode);
            env.PrintHighlighted(".med()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMedUnordered(SequenceExpressionArrayMedUnordered seqExprArrayMedUnordered, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMedUnordered.ContainerExpr, seqExprArrayMedUnordered, highlightingMode);
            env.PrintHighlighted(".medUnordered()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayVar(SequenceExpressionArrayVar seqExprArrayVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayVar.ContainerExpr, seqExprArrayVar, highlightingMode);
            env.PrintHighlighted(".var()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayDev(SequenceExpressionArrayDev seqExprArrayDev, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayDev.ContainerExpr, seqExprArrayDev, highlightingMode);
            env.PrintHighlighted(".dev()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAnd(SequenceExpressionArrayAnd seqExprArrayAnd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAnd.ContainerExpr, seqExprArrayAnd, highlightingMode);
            env.PrintHighlighted(".and()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOr(SequenceExpressionArrayOr seqExprArrayOr, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOr.ContainerExpr, seqExprArrayOr, highlightingMode);
            env.PrintHighlighted(".or()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrDequeAsSet(SequenceExpressionArrayOrDequeAsSet seqExprArrayOrDequeAsSet, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrDequeAsSet.ContainerExpr, seqExprArrayOrDequeAsSet, highlightingMode);
            env.PrintHighlighted(".asSet()", highlightingMode);
        }

        private void PrintSequenceExpressionMapDomain(SequenceExpressionMapDomain seqExprMapDomain, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprMapDomain.ContainerExpr, seqExprMapDomain, highlightingMode);
            env.PrintHighlighted(".domain()", highlightingMode);
        }

        private void PrintSequenceExpressionMapRange(SequenceExpressionMapRange seqExprMapRange, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprMapRange.ContainerExpr, seqExprMapRange, highlightingMode);
            env.PrintHighlighted(".range()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAsMap(SequenceExpressionArrayAsMap seqExprArrayAsMap, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAsMap.ContainerExpr, seqExprArrayAsMap, highlightingMode);
            env.PrintHighlighted(".asSet()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAsDeque(SequenceExpressionArrayAsDeque seqExprArrayAsDeque, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAsDeque.ContainerExpr, seqExprArrayAsDeque, highlightingMode);
            env.PrintHighlighted(".asDeque()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAsString(SequenceExpressionArrayAsString seqExprArrayAsString, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAsString.ContainerExpr, seqExprArrayAsString, highlightingMode);
            env.PrintHighlighted(".asString(", highlightingMode);
            PrintSequenceExpression(seqExprArrayAsString.Separator, seqExprArrayAsString, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArraySubarray(SequenceExpressionArraySubarray seqExprArraySubarray, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArraySubarray.ContainerExpr, seqExprArraySubarray, highlightingMode);
            env.PrintHighlighted(".subarray(", highlightingMode);
            PrintSequenceExpression(seqExprArraySubarray.Start, seqExprArraySubarray, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprArraySubarray.Length, seqExprArraySubarray, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionDequeSubdeque(SequenceExpressionDequeSubdeque seqExprDequeSubdeque, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprDequeSubdeque.ContainerExpr, seqExprDequeSubdeque, highlightingMode);
            env.PrintHighlighted(".subdeque(", highlightingMode);
            PrintSequenceExpression(seqExprDequeSubdeque.Start, seqExprDequeSubdeque, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprDequeSubdeque.Length, seqExprDequeSubdeque, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderAscending(SequenceExpressionArrayOrderAscending seqExprArrayOrderAscending, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrderAscending.ContainerExpr, seqExprArrayOrderAscending, highlightingMode);
            env.PrintHighlighted(".orderAscending()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderDescending(SequenceExpressionArrayOrderDescending seqExprArrayOrderDescending, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrderDescending.ContainerExpr, seqExprArrayOrderDescending, highlightingMode);
            env.PrintHighlighted(".orderDescending()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayGroup(SequenceExpressionArrayGroup seqExprArrayGroup, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayGroup.ContainerExpr, seqExprArrayGroup, highlightingMode);
            env.PrintHighlighted(".group()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayKeepOneForEach(SequenceExpressionArrayKeepOneForEach seqExprArrayKeepOneForEach, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayKeepOneForEach.ContainerExpr, seqExprArrayKeepOneForEach, highlightingMode);
            env.PrintHighlighted(".keepOneForEach()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayReverse(SequenceExpressionArrayReverse seqExprArrayReverse, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayReverse.ContainerExpr, seqExprArrayReverse, highlightingMode);
            env.PrintHighlighted(".reverse()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayShuffle(SequenceExpressionArrayShuffle seqExprArrayShuffle, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayShuffle.ContainerExpr, seqExprArrayShuffle, highlightingMode);
            env.PrintHighlighted(".shuffle()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayExtract(SequenceExpressionArrayExtract seqExprArrayExtract, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayExtract.ContainerExpr, seqExprArrayExtract, highlightingMode);
            env.PrintHighlighted(".extract<", highlightingMode);
            env.PrintHighlighted(seqExprArrayExtract.memberOrAttributeName, highlightingMode);
            env.PrintHighlighted(">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMap(SequenceExpressionArrayMap seqExprArrayMap, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayMap.Name + ".map<" + seqExprArrayMap.TypeName + ">{", highlightingMode);
            if(seqExprArrayMap.ArrayAccess != null)
                env.PrintHighlighted(seqExprArrayMap.ArrayAccess.Name + "; ", highlightingMode);
            if(seqExprArrayMap.Index != null)
                env.PrintHighlighted(seqExprArrayMap.Index.Name + " -> ", highlightingMode);
            env.PrintHighlighted(seqExprArrayMap.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMap.MappingExpr, seqExprArrayMap, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayRemoveIf(SequenceExpressionArrayRemoveIf seqExprArrayRemoveIf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayRemoveIf.Name + ".removeIf{", highlightingMode);
            if(seqExprArrayRemoveIf.ArrayAccess != null)
                env.PrintHighlighted(seqExprArrayRemoveIf.ArrayAccess.Name + "; ", highlightingMode);
            if(seqExprArrayRemoveIf.Index != null)
                env.PrintHighlighted(seqExprArrayRemoveIf.Index.Name + " -> ", highlightingMode);
            env.PrintHighlighted(seqExprArrayRemoveIf.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayRemoveIf.ConditionExpr, seqExprArrayRemoveIf, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpressionArrayMapStartWithAccumulateBy seqExprArrayMapStartWithAccumulateBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Name + ".map<" + seqExprArrayMapStartWithAccumulateBy.TypeName + ">", highlightingMode);
            env.PrintHighlighted("StartWith", highlightingMode);
            env.PrintHighlighted("{", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess != null)
                env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess.Name + "; ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.InitExpr, seqExprArrayMapStartWithAccumulateBy, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
            env.PrintHighlighted("AccumulateBy", highlightingMode);
            env.PrintHighlighted("{", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.ArrayAccess != null)
                env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.ArrayAccess.Name + "; ", highlightingMode);
            env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.PreviousAccumulationAccess.Name + ", ", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.Index != null)
                env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Index.Name + " -> ", highlightingMode);
            env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.MappingExpr, seqExprArrayMapStartWithAccumulateBy, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderAscendingBy(SequenceExpressionArrayOrderAscendingBy seqExprArrayOrderAscendingBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayOrderAscendingBy.Name + ".orderAscendingBy<" + seqExprArrayOrderAscendingBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderDescendingBy(SequenceExpressionArrayOrderDescendingBy seqExprArrayOrderDescendingBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayOrderDescendingBy.Name + ".orderDescendingBy<" + seqExprArrayOrderDescendingBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayGroupBy(SequenceExpressionArrayGroupBy seqExprArrayGroupBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayGroupBy.Name + ".groupBy<" + seqExprArrayGroupBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayKeepOneForEachBy(SequenceExpressionArrayKeepOneForEachBy seqExprArrayKeepOneForEachBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayKeepOneForEachBy.Name + ".keepOneForEach<" + seqExprArrayKeepOneForEachBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayIndexOfBy(SequenceExpressionArrayIndexOfBy seqExprArrayIndexOfBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayIndexOfBy.Name + ".indexOfBy<" + seqExprArrayIndexOfBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfBy.ValueToSearchForExpr, seqExprArrayIndexOfBy, highlightingMode);
            if(seqExprArrayIndexOfBy.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayIndexOfBy.StartPositionExpr, seqExprArrayIndexOfBy, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayLastIndexOfBy(SequenceExpressionArrayLastIndexOfBy seqExprArrayLastIndexOfBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayLastIndexOfBy.Name + ".lastIndexOfBy<" + seqExprArrayLastIndexOfBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayLastIndexOfBy.ValueToSearchForExpr, seqExprArrayLastIndexOfBy, highlightingMode);
            if(seqExprArrayLastIndexOfBy.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayLastIndexOfBy.StartPositionExpr, seqExprArrayLastIndexOfBy, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayIndexOfOrderedBy(SequenceExpressionArrayIndexOfOrderedBy seqExprArrayIndexOfOrderedBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayIndexOfOrderedBy.Name + ".indexOfOrderedBy<" + seqExprArrayIndexOfOrderedBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfOrderedBy.ValueToSearchForExpr, seqExprArrayIndexOfOrderedBy, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionElementFromGraph(SequenceExpressionElementFromGraph seqExprElementFromGraph, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("@(" + seqExprElementFromGraph.ElementName + ")", highlightingMode);
        }

        private void PrintSequenceExpressionNodeByName(SequenceExpressionNodeByName seqExprNodeByName, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("nodeByName(", highlightingMode);
            PrintSequenceExpression(seqExprNodeByName.NodeName, seqExprNodeByName, highlightingMode);
            if(seqExprNodeByName.NodeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprNodeByName.NodeType, seqExprNodeByName, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEdgeByName(SequenceExpressionEdgeByName seqExprEdgeByName, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("edgeByName(", highlightingMode);
            PrintSequenceExpression(seqExprEdgeByName.EdgeName, seqExprEdgeByName, highlightingMode);
            if(seqExprEdgeByName.EdgeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprEdgeByName.EdgeType, seqExprEdgeByName, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionNodeByUnique(SequenceExpressionNodeByUnique seqExprNodeByUnique, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("nodeByUnique(", highlightingMode);
            PrintSequenceExpression(seqExprNodeByUnique.NodeUniqueId, seqExprNodeByUnique, highlightingMode);
            if(seqExprNodeByUnique.NodeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprNodeByUnique.NodeType, seqExprNodeByUnique, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEdgeByUnique(SequenceExpressionEdgeByUnique seqExprEdgeByUnique, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("edgeByUnique(", highlightingMode);
            PrintSequenceExpression(seqExprEdgeByUnique.EdgeUniqueId, seqExprEdgeByUnique, highlightingMode);
            if(seqExprEdgeByUnique.EdgeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprEdgeByUnique.EdgeType, seqExprEdgeByUnique, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionSource(SequenceExpressionSource seqExprSource, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("source(", highlightingMode);
            PrintSequenceExpression(seqExprSource.Edge, seqExprSource, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionTarget(SequenceExpressionTarget seqExprTarget, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("target(", highlightingMode);
            PrintSequenceExpression(seqExprTarget.Edge, seqExprTarget, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionOpposite(SequenceExpressionOpposite seqExprOpposite, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("opposite(", highlightingMode);
            PrintSequenceExpression(seqExprOpposite.Edge, seqExprOpposite, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprOpposite.Node, seqExprOpposite, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionAttributeAccess(SequenceExpressionAttributeAccess seqExprAttributeAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprAttributeAccess.Source, seqExprAttributeAccess, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            env.PrintHighlighted(seqExprAttributeAccess.AttributeName, highlightingMode);
        }

        private void PrintSequenceExpressionMatchAccess(SequenceExpressionMatchAccess seqExprMatchAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprMatchAccess.Source, seqExprMatchAccess, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            env.PrintHighlighted(seqExprMatchAccess.ElementName, highlightingMode);
        }

        private void PrintSequenceExpressionAttributeOrMatchAccess(SequenceExpressionAttributeOrMatchAccess seqExprAttributeOrMatchAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprAttributeOrMatchAccess.Source, seqExprAttributeOrMatchAccess, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            env.PrintHighlighted(seqExprAttributeOrMatchAccess.AttributeOrElementName, highlightingMode);
        }

        private void PrintSequenceExpressionNodes(SequenceExpressionNodes seqExprNodes, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprNodes.FunctionSymbol + "(", highlightingMode);
            if(seqExprNodes.NodeType != null)
                PrintSequenceExpression(seqExprNodes.NodeType, seqExprNodes, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEdges(SequenceExpressionEdges seqExprEdges, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprEdges.FunctionSymbol + "(", highlightingMode);
            if(seqExprEdges.EdgeType != null)
                PrintSequenceExpression(seqExprEdges.EdgeType, seqExprEdges, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountNodes(SequenceExpressionCountNodes seqExprCountNodes, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountNodes.FunctionSymbol + "(", highlightingMode);
            if(seqExprCountNodes.NodeType != null)
                PrintSequenceExpression(seqExprCountNodes.NodeType, seqExprCountNodes, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountEdges(SequenceExpressionCountEdges seqExprCountEdges, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountEdges.FunctionSymbol + "(", highlightingMode);
            if(seqExprCountEdges.EdgeType != null)
                PrintSequenceExpression(seqExprCountEdges.EdgeType, seqExprCountEdges, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEmpty(SequenceExpressionEmpty seqExprEmpty, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("empty()", highlightingMode);
        }

        private void PrintSequenceExpressionNow(SequenceExpressionNow seqExprNow, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Time::now()", highlightingMode);
        }

        private void PrintSequenceExpressionSize(SequenceExpressionSize seqExprSize, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("size()", highlightingMode);
        }

        private void PrintSequenceExpressionAdjacentIncident(SequenceExpressionAdjacentIncident seqExprAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprAdjacentIncident.SourceNode, seqExprAdjacentIncident, highlightingMode);
            if(seqExprAdjacentIncident.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprAdjacentIncident.EdgeType, seqExprAdjacentIncident, highlightingMode);
            }
            if(seqExprAdjacentIncident.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprAdjacentIncident.OppositeNodeType, seqExprAdjacentIncident, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountAdjacentIncident(SequenceExpressionCountAdjacentIncident seqExprCountAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountAdjacentIncident.SourceNode, seqExprCountAdjacentIncident, highlightingMode);
            if(seqExprCountAdjacentIncident.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountAdjacentIncident.EdgeType, seqExprCountAdjacentIncident, highlightingMode);
            }
            if(seqExprCountAdjacentIncident.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountAdjacentIncident.OppositeNodeType, seqExprCountAdjacentIncident, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionReachable(SequenceExpressionReachable seqExprReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprReachable.SourceNode, seqExprReachable, highlightingMode);
            if(seqExprReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprReachable.EdgeType, seqExprReachable, highlightingMode);
            }
            if(seqExprReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprReachable.OppositeNodeType, seqExprReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountReachable(SequenceExpressionCountReachable seqExprCountReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountReachable.SourceNode, seqExprCountReachable, highlightingMode);
            if(seqExprCountReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountReachable.EdgeType, seqExprCountReachable, highlightingMode);
            }
            if(seqExprCountReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountReachable.OppositeNodeType, seqExprCountReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionBoundedReachable(SequenceExpressionBoundedReachable seqExprBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachable.SourceNode, seqExprBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachable.Depth, seqExprBoundedReachable, highlightingMode);
            if(seqExprBoundedReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachable.EdgeType, seqExprBoundedReachable, highlightingMode);
            }
            if(seqExprBoundedReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachable.OppositeNodeType, seqExprBoundedReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionBoundedReachableWithRemainingDepth(SequenceExpressionBoundedReachableWithRemainingDepth seqExprBoundedReachableWithRemainingDepth, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprBoundedReachableWithRemainingDepth.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.SourceNode, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.Depth, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            if(seqExprBoundedReachableWithRemainingDepth.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.EdgeType, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            }
            if(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountBoundedReachable(SequenceExpressionCountBoundedReachable seqExprCountBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountBoundedReachable.SourceNode, seqExprCountBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprCountBoundedReachable.Depth, seqExprCountBoundedReachable, highlightingMode);
            if(seqExprCountBoundedReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountBoundedReachable.EdgeType, seqExprCountBoundedReachable, highlightingMode);
            }
            if(seqExprCountBoundedReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountBoundedReachable.OppositeNodeType, seqExprCountBoundedReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsBoundedReachable(SequenceExpressionIsBoundedReachable seqExprIsBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprIsBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.SourceNode, seqExprIsBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.EndElement, seqExprIsBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.Depth, seqExprIsBoundedReachable, highlightingMode);
            if(seqExprIsBoundedReachable.EdgeType != null)
            { 
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsBoundedReachable.EdgeType, seqExprIsBoundedReachable, highlightingMode);
            }
            if(seqExprIsBoundedReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsBoundedReachable.OppositeNodeType, seqExprIsBoundedReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsAdjacentIncident(SequenceExpressionIsAdjacentIncident seqExprIsAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprIsAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsAdjacentIncident.SourceNode, seqExprIsAdjacentIncident, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsAdjacentIncident.EndElement, seqExprIsAdjacentIncident, highlightingMode);
            if(seqExprIsAdjacentIncident.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsAdjacentIncident.EdgeType, seqExprIsAdjacentIncident, highlightingMode);
            }
            if(seqExprIsAdjacentIncident.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsAdjacentIncident.OppositeNodeType, seqExprIsAdjacentIncident, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsReachable(SequenceExpressionIsReachable seqExprIsReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprIsReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsReachable.SourceNode, seqExprIsReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsReachable.EndElement, seqExprIsReachable, highlightingMode);
            if(seqExprIsReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsReachable.EdgeType, seqExprIsReachable, highlightingMode);
            }
            if(seqExprIsReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsReachable.OppositeNodeType, seqExprIsReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionInducedSubgraph(SequenceExpressionInducedSubgraph seqExprInducedSubgraph, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("inducedSubgraph(", highlightingMode);
            PrintSequenceExpression(seqExprInducedSubgraph.NodeSet, seqExprInducedSubgraph, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionDefinedSubgraph(SequenceExpressionDefinedSubgraph seqExprDefinedSubgraph, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("definedSubgraph(", highlightingMode);
            PrintSequenceExpression(seqExprDefinedSubgraph.EdgeSet, seqExprDefinedSubgraph, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        // potential todo: change code structure in equals any sequence expression, too
        private void PrintSequenceExpressionEqualsAny(SequenceExpressionEqualsAny seqExprEqualsAny, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprEqualsAny.IncludingAttributes ? "equalsAny(" : "equalsAnyStructurally(", highlightingMode);
            PrintSequenceExpression(seqExprEqualsAny.Subgraph, seqExprEqualsAny, highlightingMode);
            env.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqExprEqualsAny.SubgraphSet, seqExprEqualsAny, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionGetEquivalent(SequenceExpressionGetEquivalent seqExprGetEquivalent, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprGetEquivalent.IncludingAttributes ? "getEquivalent(" : "getEquivalentStructurally(", highlightingMode);
            PrintSequenceExpression(seqExprGetEquivalent.Subgraph, seqExprGetEquivalent, highlightingMode);
            env.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqExprGetEquivalent.SubgraphSet, seqExprGetEquivalent, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCanonize(SequenceExpressionCanonize seqExprCanonize, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("canonize(", highlightingMode);
            PrintSequenceExpression(seqExprCanonize.Graph, seqExprCanonize, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionNameof(SequenceExpressionNameof seqExprNameof, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("nameof(", highlightingMode);
            if(seqExprNameof.NamedEntity != null)
                PrintSequenceExpression(seqExprNameof.NamedEntity, seqExprNameof, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionUniqueof(SequenceExpressionUniqueof seqExprUniqueof, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("uniqueof(", highlightingMode);
            if(seqExprUniqueof.UniquelyIdentifiedEntity != null)
                PrintSequenceExpression(seqExprUniqueof.UniquelyIdentifiedEntity, seqExprUniqueof, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionTypeof(SequenceExpressionTypeof seqExprTypeof, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("typeof(", highlightingMode);
            PrintSequenceExpression(seqExprTypeof.Entity, seqExprTypeof, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionExistsFile(SequenceExpressionExistsFile seqExprExistsFile, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::existsFile(", highlightingMode);
            PrintSequenceExpression(seqExprExistsFile.Path, seqExprExistsFile, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionImport(SequenceExpressionImport seqExprImport, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::import(", highlightingMode);
            PrintSequenceExpression(seqExprImport.Path, seqExprImport, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCopy(SequenceExpressionCopy seqExprCopy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCopy.Deep ? "copy(" : "clone(", highlightingMode);
            PrintSequenceExpression(seqExprCopy.ObjectToBeCopied, seqExprCopy, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathMin(SequenceExpressionMathMin seqExprMathMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::min(", highlightingMode);
            PrintSequenceExpression(seqExprMathMin.Left, seqExprMathMin, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprMathMin.Right, seqExprMathMin, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathMax(SequenceExpressionMathMax seqExprMathMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::max(", highlightingMode);
            PrintSequenceExpression(seqExprMathMax.Left, seqExprMathMax, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprMathMax.Right, seqExprMathMax, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathAbs(SequenceExpressionMathAbs seqExprMathAbs, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::abs(", highlightingMode);
            PrintSequenceExpression(seqExprMathAbs.Argument, seqExprMathAbs, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathCeil(SequenceExpressionMathCeil seqExprMathCeil, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::ceil(", highlightingMode);
            PrintSequenceExpression(seqExprMathCeil.Argument, seqExprMathCeil, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathFloor(SequenceExpressionMathFloor seqExprMathFloor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::floor(", highlightingMode);
            PrintSequenceExpression(seqExprMathFloor.Argument, seqExprMathFloor, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathRound(SequenceExpressionMathRound seqExprMathRound, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::round(", highlightingMode);
            PrintSequenceExpression(seqExprMathRound.Argument, seqExprMathRound, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathTruncate(SequenceExpressionMathTruncate seqExprMathTruncate, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::truncate(", highlightingMode);
            PrintSequenceExpression(seqExprMathTruncate.Argument, seqExprMathTruncate, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSqr(SequenceExpressionMathSqr seqExprMathSqr, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sqr(", highlightingMode);
            PrintSequenceExpression(seqExprMathSqr.Argument, seqExprMathSqr, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSqrt(SequenceExpressionMathSqrt seqExprMathSqrt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sqrt(", highlightingMode);
            PrintSequenceExpression(seqExprMathSqrt.Argument, seqExprMathSqrt, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathPow(SequenceExpressionMathPow seqExprPow, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::pow(", highlightingMode);
            if(seqExprPow.Left != null)
            {
                PrintSequenceExpression(seqExprPow.Left, seqExprPow, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
            }
            PrintSequenceExpression(seqExprPow.Right, seqExprPow, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathLog(SequenceExpressionMathLog seqExprMathLog, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::log(", highlightingMode);
            PrintSequenceExpression(seqExprMathLog.Left, seqExprMathLog, highlightingMode);
            if(seqExprMathLog.Right != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprMathLog.Right, seqExprMathLog, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSgn(SequenceExpressionMathSgn seqExprMathSgn, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sgn(", highlightingMode);
            PrintSequenceExpression(seqExprMathSgn.Argument, seqExprMathSgn, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSin(SequenceExpressionMathSin seqExprMathSin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sin(", highlightingMode);
            PrintSequenceExpression(seqExprMathSin.Argument, seqExprMathSin, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathCos(SequenceExpressionMathCos seqExprMathCos, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::cos(", highlightingMode);
            PrintSequenceExpression(seqExprMathCos.Argument, seqExprMathCos, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathTan(SequenceExpressionMathTan seqExprMathTan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::tan(", highlightingMode);
            PrintSequenceExpression(seqExprMathTan.Argument, seqExprMathTan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathArcSin(SequenceExpressionMathArcSin seqExprMathArcSin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::arcsin(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcSin.Argument, seqExprMathArcSin, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathArcCos(SequenceExpressionMathArcCos seqExprMathArcCos, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::arccos(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcCos.Argument, seqExprMathArcCos, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathArcTan(SequenceExpressionMathArcTan seqExprMathArcTan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::arctan(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcTan.Argument, seqExprMathArcTan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathPi(SequenceExpressionMathPi seqExprMathPi, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::pi()", highlightingMode);
        }

        private void PrintSequenceExpressionMathE(SequenceExpressionMathE seqExprMathE, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::e()", highlightingMode);
        }

        private void PrintSequenceExpressionMathByteMin(SequenceExpressionMathByteMin seqExprMathByteMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::byteMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathByteMax(SequenceExpressionMathByteMax seqExprMathByteMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::byteMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathShortMin(SequenceExpressionMathShortMin seqExprMathShortMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::shortMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathShortMax(SequenceExpressionMathShortMax seqExprMathShortMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::shortMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathIntMin(SequenceExpressionMathIntMin seqExprMathIntMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::intMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathIntMax(SequenceExpressionMathIntMax seqExprMathIntMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::intMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathLongMin(SequenceExpressionMathLongMin seqExprMathLongMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::longMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathLongMax(SequenceExpressionMathLongMax seqExprMathLongMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::longMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathFloatMin(SequenceExpressionMathFloatMin seqExprMathFloatMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::floatMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathFloatMax(SequenceExpressionMathFloatMax seqExprMathFloatMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::floatMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathDoubleMin(SequenceExpressionMathDoubleMin seqExprMathDoubleMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::doubleMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathDoubleMax(SequenceExpressionMathDoubleMax seqExprMathDoubleMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::doubleMax()", highlightingMode);
        }

        private void PrintSequenceExpressionRuleQuery(SequenceExpressionRuleQuery seqExprRuleQuery, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToBreakpointPosMap != null)
            {
                PrintBreak((ISequenceSpecial)seqExprRuleQuery);
            }

            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprRuleQuery == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            PrintSequence(seqExprRuleQuery.RuleCall, seqExprRuleQuery, highlightingModeLocal); // rule all call with test flag, thus [?r]
        }

        private void PrintSequenceExpressionMultiRuleQuery(SequenceExpressionMultiRuleQuery seqExprMultiRuleQuery, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprMultiRuleQuery == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("[?[", highlightingModeLocal);
            bool first = true;
            foreach(SequenceRuleCall rule in seqExprMultiRuleQuery.MultiRuleCall.Sequences)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(",", highlightingModeLocal);

                PrintReturnAssignments(rule.ReturnVars, parent, highlightingMode);
                env.PrintHighlighted(rule.DebugPrefix, highlightingMode);
                PrintRuleCallString(rule, parent, highlightingMode);
            }
            env.PrintHighlighted("]", highlightingModeLocal);
            foreach(SequenceFilterCallBase filterCall in seqExprMultiRuleQuery.MultiRuleCall.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqExprMultiRuleQuery.MultiRuleCall, highlightingModeLocal);
            }
            env.PrintHighlighted("\\<class " + seqExprMultiRuleQuery.MatchClass + ">", highlightingModeLocal);
            env.PrintHighlighted("]", highlightingModeLocal);
        }

        private void PrintSequenceExpressionMappingClause(SequenceExpressionMappingClause seqExprMappingClause, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprMappingClause == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            env.PrintHighlighted("[:", highlightingModeLocal);

            bool first = true;
            foreach(SequenceRulePrefixedSequence seqRulePrefixedSequence in seqExprMappingClause.MultiRulePrefixedSequence.RulePrefixedSequences)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                env.PrintHighlighted("for{", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence);
                env.PrintHighlighted(";", highlightingModeRulePrefixedSequence);
                PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
                env.PrintHighlighted("}", highlightingModeRulePrefixedSequence);
            }

            foreach(SequenceFilterCallBase filterCall in seqExprMappingClause.MultiRulePrefixedSequence.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqExprMappingClause.MultiRulePrefixedSequence, highlightingModeLocal);
            }
            env.PrintHighlighted(":]", highlightingModeLocal);
        }

        private void PrintSequenceExpressionScan(SequenceExpressionScan seqExprScan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("scan", highlightingMode);
            if(seqExprScan.ResultType != null)
                env.PrintHighlighted("<" + seqExprScan.ResultType + ">", highlightingMode);
            env.PrintHighlighted("(", highlightingMode);
            PrintSequenceExpression(seqExprScan.StringExpr, seqExprScan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionTryScan(SequenceExpressionTryScan seqExprTryScan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("tryscan", highlightingMode);
            if(seqExprTryScan.ResultType != null)
                env.PrintHighlighted("<" + seqExprTryScan.ResultType + ">", highlightingMode);
            env.PrintHighlighted("(", highlightingMode);
            PrintSequenceExpression(seqExprTryScan.StringExpr, seqExprTryScan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionFunctionCall(SequenceExpressionFunctionCall seqExprFunctionCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprFunctionCall.Name, highlightingMode);
            if(seqExprFunctionCall.ArgumentExpressions.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqExprFunctionCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(seqExprFunctionCall.ArgumentExpressions[i], seqExprFunctionCall, highlightingMode);
                    if(i != seqExprFunctionCall.ArgumentExpressions.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")", highlightingMode);
            }
        }

        private void PrintSequenceExpressionFunctionMethodCall(SequenceExpressionFunctionMethodCall seqExprFunctionMethodCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprFunctionMethodCall.TargetExpr, seqExprFunctionMethodCall, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            PrintSequenceExpressionFunctionCall(seqExprFunctionMethodCall, parent, highlightingMode);
        }
    }
}
