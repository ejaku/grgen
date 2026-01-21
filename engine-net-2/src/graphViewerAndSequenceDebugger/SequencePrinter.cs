/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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
        SequenceComputationPrinter seqCompPrinter;
        SequenceExpressionPrinter seqExprPrinter;

        public SequencePrinter(IDebuggerEnvironment env)
        {
            this.env = env;

            seqExprPrinter = new SequenceExpressionPrinter(env, this);
            seqCompPrinter = new SequenceComputationPrinter(env, seqExprPrinter);
        }

        internal DisplaySequenceContext Context { get { return context; } }

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
            seqExprPrinter.PrintSequenceExpression(seqExpr, null, HighlightingMode.None);
            env.PrintHighlighted(postfix, HighlightingMode.SequenceStart);
            env.WriteLineDataRendering();
            return null;
        }

        /// <summary>
        /// Prints the given sequence (adding parentheses if needed) according to the display context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="parent">The parent of the sequence or null if the sequence is a root</param>
        /// <param name="highlightingMode">The highlighting mode defining how the sequence is to be printed</param>
        internal void PrintSequence(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode)
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
            case SequenceType.PersistenceProviderTransaction:
                PrintSequencePersistenceProviderTransaction((SequencePersistenceProviderTransaction)seq, parent, highlightingMode);
                break;
            case SequenceType.CommitAndRestartPersistenceProviderTransaction:
                PrintSequenceCommitAndRestartPersistenceProviderTransaction((SequenceCommitAndRestartPersistenceProviderTransaction)seq, parent, highlightingMode);
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
            seqExprPrinter.PrintSequenceExpression(seqMin.MinExpr, seqMin, highlightingMode);
            env.PrintHighlighted(":*]", highlightingMode);
        }

        private void PrintSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequence(seqMinMax.Seq, seqMinMax, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
            seqExprPrinter.PrintSequenceExpression(seqMinMax.MinExpr, seqMinMax, highlightingMode);
            env.PrintHighlighted(":", highlightingMode);
            seqExprPrinter.PrintSequenceExpression(seqMinMax.MaxExpr, seqMinMax, highlightingMode);
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

        private void PrintSequencePersistenceProviderTransaction(SequencePersistenceProviderTransaction seqPPTrans, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("<:", highlightingMode);
            PrintSequence(seqPPTrans.Seq, seqPPTrans, highlightingMode);
            env.PrintHighlighted(":>", highlightingMode);
        }

        private void PrintSequenceCommitAndRestartPersistenceProviderTransaction(SequenceCommitAndRestartPersistenceProviderTransaction seqCommitAndRestartPPTrans, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(">:<", highlightingMode);
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
            seqExprPrinter.PrintSequenceExpression(seqFor.Left, seqFor, highlightingMode);
            env.PrintHighlighted(":", highlightingMode);
            seqExprPrinter.PrintSequenceExpression(seqFor.Right, seqFor, highlightingMode);
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
            seqExprPrinter.PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
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
                seqExprPrinter.PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
                env.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction2), highlightingMode);
                seqExprPrinter.PrintSequenceExpression(seqFor.Expr2, seqFor, highlightingMode);
            }
            else if(seqFor.From() != null)
            {
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
                env.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                seqExprPrinter.PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
            }
            else if(seqFor.To() != null)
            {
                env.PrintHighlighted(seqFor.IndexName, highlightingMode);
                env.PrintHighlighted(seqFor.DirectionAsString(seqFor.Direction), highlightingMode);
                seqExprPrinter.PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
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
            seqExprPrinter.PrintSequenceExpression(seqExecInSub.SubgraphExpr, seqExecInSub, highlightingMode);
            if(seqExecInSub.ValueExpr != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                seqExprPrinter.PrintSequenceExpression(seqExecInSub.ValueExpr, seqExecInSub, highlightingMode);
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
            seqExprPrinter.PrintSequenceExpression(seqLock.LockObjectExpr, seqLock, highlightingMode);
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
                seqExprPrinter.PrintSequenceExpression(arguments[i], parent, highlightingMode);
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
                seqExprPrinter.PrintSequenceExpression(arguments[i], parent, highlightingMode);
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

        internal void PrintReturnAssignments(SequenceVariable[] returnVars, SequenceBase parent, HighlightingMode highlightingMode)
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

        internal void PrintRuleCallString(SequenceRuleCall seq, SequenceBase parent, HighlightingMode highlightingMode)
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

        internal void PrintSequenceFilterCall(SequenceFilterCallBase seq, SequenceBase parent, HighlightingMode highlightingMode)
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
                    seqExprPrinter.PrintSequenceExpression(filterCall.FilterCall.initExpression, parent, highlightingMode);
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
                seqExprPrinter.PrintSequenceExpression(filterCall.FilterCall.lambdaExpression, parent, highlightingMode);
                env.PrintHighlighted("}", highlightingMode);
            }
            else
            {
                Debug.Assert(false);
            }
        }

        private void PrintSequenceBooleanComputation(SequenceBooleanComputation seqComp, SequenceBase parent, HighlightingMode highlightingMode)
        {
            seqCompPrinter.PrintSequenceComputation(seqComp.Computation, seqComp, highlightingMode);
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
            seqExprPrinter.PrintSequenceExpression(seq.Constructor, seq, highlightingMode);
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

        internal void PrintBreak(ISequenceSpecial seq)
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
    }
}
