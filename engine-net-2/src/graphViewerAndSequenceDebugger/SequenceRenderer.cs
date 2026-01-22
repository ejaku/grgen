/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit, Claude Code with Edgar Jakumeit

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class SequenceRenderer : ISequenceDisplayer
    {
        readonly IDebuggerEnvironment env;
        DisplaySequenceContext context;
        string groupNodeName; // the name of the group node the sequence should be contained in (null in case no containment is required)
        SequenceComputationRenderer seqCompRenderer;
        SequenceExpressionRenderer seqExprRenderer;

        public SequenceRenderer(IDebuggerEnvironment env)
        {
            this.env = env;

            seqExprRenderer = new SequenceExpressionRenderer(env, this);
            seqCompRenderer = new SequenceComputationRenderer(env, seqExprRenderer);

            // GUI TODO: configure graph viewer here or at some other place?
            env.guiForDataRendering.graphViewer.SetLayout("SugiyamaScheme");
            RegisterRealizers();
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
                return DisplaySequence((Sequence)seqBase, context, nestingLevel, prefix, postfix, groupNodeName);
            else
                return DisplaySequenceExpression((SequenceExpression)seqBase, context, nestingLevel, prefix, postfix, groupNodeName);
        }

        public string DisplaySequence(Sequence seq, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            this.context = context;
            this.groupNodeName = groupNodeName;
            //env.PrintHighlighted(prefix + nestingLevel + ">", HighlightingMode.SequenceStart); // GUI TODO: prefix+nestingLevel/postfix rendering
            //env.guiForDataRendering.graphViewer.ClearGraph(); remove, not needed anymore since BeginOfDisplay

            string sequenceNode = RenderSequence(seq, null, HighlightingMode.None); // GUI TODO: rename

            //env.PrintHighlighted(postfix, HighlightingMode.SequenceStart);
            env.guiForDataRendering.graphViewer.Show();

            return sequenceNode;
        }

        public string DisplaySequenceExpression(SequenceExpression seqExpr, DisplaySequenceContext context, int nestingLevel, string prefix, string postfix, string groupNodeName)
        {
            this.context = context;
            this.groupNodeName = groupNodeName;
            //env.PrintHighlighted(prefix + nestingLevel + ">", HighlightingMode.SequenceStart); // GUI TODO: prefix+nestingLevel/postfix rendering
            //env.guiForDataRendering.graphViewer.ClearGraph(); remove, not needed anymore since BeginOfDisplay

            seqExprRenderer.PrintSequenceExpression(seqExpr, null, HighlightingMode.None);

            if(seqExpr.SequenceExpressionType != SequenceExpressionType.RuleQuery
                && seqExpr.SequenceExpressionType != SequenceExpressionType.MultiRuleQuery
                && seqExpr.SequenceExpressionType != SequenceExpressionType.MappingClause)
            {
                AddNode(seqExpr, HighlightingMode.None, seqExpr.Symbol); // also render top level sequence expression in case it was none of the rule based ones
            }

            //env.PrintHighlighted(postfix, HighlightingMode.SequenceStart);
            env.guiForDataRendering.graphViewer.Show();

            return null;
        }

        /// <summary>
        /// Renders the given sequence according to the display context into a graph.
        /// </summary>
        /// <param name="seq">The sequence to be rendered</param>
        /// <param name="parent">The parent of the sequence or null if the sequence is a root</param>
        /// <param name="highlightingMode">The highlighting mode to be used</param>
        /// <returns>The unique name of the node inserted for the rendered sequence</returns>
        internal string RenderSequence(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            // print parentheses, if neccessary
            //if(parent != null && seq.Precedence < parent.Precedence)
            //    env.PrintHighlighted("(", highlightingMode);

            switch(seq.SequenceType)
            {
            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
            case SequenceType.StrictAnd:
                return RenderSequenceBinary((SequenceBinary)seq, parent, highlightingMode);
            case SequenceType.IfThen:
                return RenderSequenceIfThen((SequenceIfThen)seq, parent, highlightingMode);
            case SequenceType.Not:
                return RenderSequenceNot((SequenceNot)seq, parent, highlightingMode);
            case SequenceType.IterationMin:
                return RenderSequenceIterationMin((SequenceIterationMin)seq, parent, highlightingMode);
            case SequenceType.IterationMinMax:
                return RenderSequenceIterationMinMax((SequenceIterationMinMax)seq, parent, highlightingMode);
            case SequenceType.Transaction:
                return RenderSequenceTransaction((SequenceTransaction)seq, parent, highlightingMode);
            case SequenceType.Backtrack:
                return RenderSequenceBacktrack((SequenceBacktrack)seq, parent, highlightingMode);
            case SequenceType.MultiBacktrack:
                return RenderSequenceMultiBacktrack((SequenceMultiBacktrack)seq, parent, highlightingMode);
            case SequenceType.MultiSequenceBacktrack:
                return RenderSequenceMultiSequenceBacktrack((SequenceMultiSequenceBacktrack)seq, parent, highlightingMode);
            case SequenceType.Pause:
                return RenderSequencePause((SequencePause)seq, parent, highlightingMode);
            case SequenceType.PersistenceProviderTransaction:
                return RenderSequencePersistenceProviderTransaction((SequencePersistenceProviderTransaction)seq, parent, highlightingMode);
            case SequenceType.CommitAndRestartPersistenceProviderTransaction:
                return RenderSequenceCommitAndRestartPersistenceProviderTransaction((SequenceCommitAndRestartPersistenceProviderTransaction)seq, parent, highlightingMode);
            case SequenceType.ForContainer:
                return RenderSequenceForContainer((SequenceForContainer)seq, parent, highlightingMode);
            case SequenceType.ForIntegerRange:
                return RenderSequenceForIntegerRange((SequenceForIntegerRange)seq, parent, highlightingMode);
            case SequenceType.ForIndexAccessEquality:
                return RenderSequenceForIndexAccessEquality((SequenceForIndexAccessEquality)seq, parent, highlightingMode);
            case SequenceType.ForIndexAccessOrdering:
                return RenderSequenceForIndexAccessOrdering((SequenceForIndexAccessOrdering)seq, parent, highlightingMode);
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
                return RenderSequenceForFunction((SequenceForFunction)seq, parent, highlightingMode);
            case SequenceType.ForMatch:
                return RenderSequenceForMatch((SequenceForMatch)seq, parent, highlightingMode);
            case SequenceType.ExecuteInSubgraph:
                return RenderSequenceExecuteInSubgraph((SequenceExecuteInSubgraph)seq, parent, highlightingMode, -1, highlightingMode);
            case SequenceType.ParallelExecute:
                return RenderSequenceParallelExecute((SequenceParallelExecute)seq, parent, highlightingMode);
            case SequenceType.ParallelArrayExecute:
                return RenderSequenceParallelArrayExecute((SequenceParallelArrayExecute)seq, parent, highlightingMode);
            case SequenceType.Lock:
                return RenderSequenceLock((SequenceLock)seq, parent, highlightingMode);
            case SequenceType.IfThenElse:
                return RenderSequenceIfThenElse((SequenceIfThenElse)seq, parent, highlightingMode);
            case SequenceType.LazyOrAll:
            case SequenceType.LazyAndAll:
            case SequenceType.StrictOrAll:
            case SequenceType.StrictAndAll:
                return RenderSequenceNAry((SequenceNAry)seq, parent, highlightingMode);
            case SequenceType.WeightedOne:
                return RenderSequenceWeightedOne((SequenceWeightedOne)seq, parent, highlightingMode);
            case SequenceType.SomeFromSet:
                return RenderSequenceSomeFromSet((SequenceSomeFromSet)seq, parent, highlightingMode);
            case SequenceType.MultiRulePrefixedSequence:
                return RenderSequenceMultiRulePrefixedSequence((SequenceMultiRulePrefixedSequence)seq, parent, highlightingMode);
            case SequenceType.MultiRuleAllCall:
                return RenderSequenceMultiRuleAllCall((SequenceMultiRuleAllCall)seq, parent, highlightingMode);
            case SequenceType.RulePrefixedSequence:
                return RenderSequenceRulePrefixedSequence((SequenceRulePrefixedSequence)seq, parent, highlightingMode);
            case SequenceType.SequenceCall:
            case SequenceType.RuleCall:
            case SequenceType.RuleAllCall:
            case SequenceType.RuleCountAllCall:
            case SequenceType.BooleanComputation:
                return RenderSequenceBreakpointable((Sequence)seq, parent, highlightingMode, "");
            case SequenceType.AssignSequenceResultToVar:
            case SequenceType.OrAssignSequenceResultToVar:
            case SequenceType.AndAssignSequenceResultToVar:
                return RenderSequenceAssignSequenceResultToVar((SequenceAssignSequenceResultToVar)seq, parent, highlightingMode);
            case SequenceType.AssignUserInputToVar:
            case SequenceType.AssignRandomIntToVar:
            case SequenceType.AssignRandomDoubleToVar:
                return RenderSequenceAssignChoiceHighlightable((Sequence)seq, parent, highlightingMode);
            case SequenceType.SequenceDefinitionInterpreted:
                return RenderSequenceDefinitionInterpreted((SequenceDefinitionInterpreted)seq, parent, highlightingMode);
            // Atoms (assignments)
            case SequenceType.AssignVarToVar:
            case SequenceType.AssignConstToVar:
            case SequenceType.DeclareVariable:
                return AddNode(seq, highlightingMode, seq.Symbol);
            case SequenceType.AssignContainerConstructorToVar:
                return RenderSequenceAssignContainerConstructorToVar((SequenceAssignContainerConstructorToVar)seq, parent, highlightingMode);
            default:
                Debug.Assert(false);
                return "<UNKNOWN_SEQUENCE_TYPE>";
            }

            // print parentheses, if neccessary
            //if(parent != null && seq.Precedence < parent.Precedence)
            //    env.PrintHighlighted(")", highlightingMode);
        }

        private string RenderSequenceBinary(SequenceBinary seqBin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null && seqBin.Random)
            {
                string choicePrefix = GetChoicePrefix(seqBin);
                String operatorNodeNameChoiceRun = AddNode(seqBin, HighlightingMode.Choicepoint, choicePrefix + seqBin.OperatorSymbol);
                String rightNodeNameChoiceRun = RenderSequence(seqBin.Right, seqBin, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, rightNodeNameChoiceRun), operatorNodeNameChoiceRun, rightNodeNameChoiceRun, GetEdgeRealizer(highlightingMode), "right");
                String leftNodeNameChoiceRun = RenderSequence(seqBin.Left, seqBin, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, leftNodeNameChoiceRun), operatorNodeNameChoiceRun, leftNodeNameChoiceRun, GetEdgeRealizer(highlightingMode), "left");
                return operatorNodeNameChoiceRun;
            }

            if(seqBin == context.highlightSeq && context.choice)
            {
                String operatorNodeNameChoiceRun = AddNode(seqBin, HighlightingMode.Choicepoint, seqBin.OperatorSymbol);
                String rightNodeNameChoiceRun = RenderSequence(seqBin.Right, seqBin, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, rightNodeNameChoiceRun), operatorNodeNameChoiceRun, rightNodeNameChoiceRun, GetEdgeRealizer(HighlightingMode.Choicepoint), "(r)ight");
                String leftNodeNameChoiceRun = RenderSequence(seqBin.Left, seqBin, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, leftNodeNameChoiceRun), operatorNodeNameChoiceRun, leftNodeNameChoiceRun, GetEdgeRealizer(HighlightingMode.Choicepoint), "(l)eft");
                return operatorNodeNameChoiceRun;
            }

            String operatorNodeName = AddNode(seqBin, highlightingMode, seqBin.OperatorSymbol);
            String rightNodeName = RenderSequence(seqBin.Right, seqBin, highlightingMode); // it seems the layout algorithm puts the first added child to the right and the last added child to the left
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, rightNodeName), operatorNodeName, rightNodeName, GetEdgeRealizer(highlightingMode), "right");
            String leftNodeName = RenderSequence(seqBin.Left, seqBin, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, leftNodeName), operatorNodeName, leftNodeName, GetEdgeRealizer(highlightingMode), "left");
            return operatorNodeName;
        }

        private string RenderSequenceIfThen(SequenceIfThen seqIfThen, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqIfThen, highlightingMode, "if{.;.}");
            String trueCaseNodeName = RenderSequence(seqIfThen.Right, seqIfThen, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, trueCaseNodeName), operatorNodeName, trueCaseNodeName, GetEdgeRealizer(highlightingMode), "trueCase");
            String conditionNodeName = RenderSequence(seqIfThen.Left, seqIfThen, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, conditionNodeName), operatorNodeName, conditionNodeName, GetEdgeRealizer(highlightingMode), "condition");
            return operatorNodeName;
        }

        private string RenderSequenceNot(SequenceNot seqNot, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqNot, highlightingMode, seqNot.OperatorSymbol);
            String subNodeName = RenderSequence(seqNot.Seq, seqNot, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "operand");
            return operatorNodeName;
        }

        private string RenderSequenceIterationMin(SequenceIterationMin seqMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqMin, highlightingMode, ".[" + seqMin.MinExpr.Symbol + ":*]");
            String subNodeName = RenderSequence(seqMin.Seq, seqMin, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            //String minExprNodeName = PrintSequenceExpression(seqMin.MinExpr, seqMin, highlightingMode);
            //env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, minExprNodeName), operatorNodeName, minExprNodeName, GetEdgeRealizer(highlightingMode), "minExpr");
            return operatorNodeName;
        }

        private string RenderSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqMinMax, highlightingMode, ".[" + seqMinMax.MinExpr.Symbol + ":" + seqMinMax.MaxExpr.Symbol + "]");
            String subNodeName = RenderSequence(seqMinMax.Seq, seqMinMax, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            //String minExprNodeName = PrintSequenceExpression(seqMinMax.MinExpr, seqMinMax, highlightingMode);
            //env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, minExprNodeName), operatorNodeName, minExprNodeName, GetEdgeRealizer(highlightingMode), "minExpr");
            //String maxExprNodeName = PrintSequenceExpression(seqMinMax.MaxExpr, seqMinMax, highlightingMode);
            //env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, maxExprNodeName), operatorNodeName, maxExprNodeName, GetEdgeRealizer(highlightingMode), "maxExpr");
            return operatorNodeName;
        }

        private string RenderSequenceTransaction(SequenceTransaction seqTrans, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqTrans, highlightingMode, "<.>");
            String subNodeName = RenderSequence(seqTrans.Seq, seqTrans, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
            return operatorNodeName;
        }

        private string RenderSequenceBacktrack(SequenceBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqBack, highlightingModeLocal, "<<.;;.>>");
            String subNodeName = RenderSequence(seqBack.Seq, seqBack, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
            String ruleNodeName = RenderSequence(seqBack.Rule, seqBack, highlightingModeLocal);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, ruleNodeName), operatorNodeName, ruleNodeName, GetEdgeRealizer(highlightingModeLocal), "rule");
            return operatorNodeName;
        }

        private string RenderSequenceMultiBacktrack(SequenceMultiBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqBack, highlightingModeLocal, "<<...;;.>>");
            String subNodeName = RenderSequence(seqBack.Seq, seqBack, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
            String ruleNodeName = RenderSequence(seqBack.Rules, seqBack, highlightingModeLocal);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, ruleNodeName), operatorNodeName, ruleNodeName, GetEdgeRealizer(highlightingModeLocal), "rules");
            return operatorNodeName;
        }

        private string RenderSequenceMultiSequenceBacktrack(SequenceMultiSequenceBacktrack seqBack, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqBack == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqBack, highlightingModeLocal, "<<[[...]]>>");

            for(int i = seqBack.MultiRulePrefixedSequence.RulePrefixedSequences.Count - 1; i >= 0; --i)
            {
                SequenceRulePrefixedSequence seqRulePrefixedSequence = seqBack.MultiRulePrefixedSequence.RulePrefixedSequences[i];

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                String subOperatorNodeName = AddNode(seqRulePrefixedSequence, highlightingModeRulePrefixedSequence, "for{.;.}");
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subOperatorNodeName), operatorNodeName, subOperatorNodeName, GetEdgeRealizer(highlightingMode), "sub" + i);

                String subNodeName = RenderSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(subOperatorNodeName, subNodeName), subOperatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
                String ruleNodeName = RenderSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(subOperatorNodeName, ruleNodeName), subOperatorNodeName, ruleNodeName, GetEdgeRealizer(highlightingModeRulePrefixedSequence), "rule");
            }

            StringBuilder sb = new StringBuilder();
            foreach(SequenceFilterCallBase filterCall in seqBack.MultiRulePrefixedSequence.Filters)
            {
                string filterCallAsString = null;
                PrintSequenceFilterCall(filterCall, seqBack.MultiRulePrefixedSequence, highlightingModeLocal, ref filterCallAsString); // highlightingModeLocal
                sb.Append(filterCallAsString);
            }
            env.guiForDataRendering.graphViewer.SetNodeLabel(operatorNodeName, "<<[[...]]>>" + sb.ToString());

            return operatorNodeName;
        }

        private string RenderSequencePause(SequencePause seqPause, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqPause, highlightingMode, "/./");
            String subNodeName = RenderSequence(seqPause.Seq, seqPause, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
            return operatorNodeName;
        }

        private string RenderSequencePersistenceProviderTransaction(SequencePersistenceProviderTransaction seqPPTrans, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqPPTrans, highlightingMode, "<:.:>");
            String subNodeName = RenderSequence(seqPPTrans.Seq, seqPPTrans, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
            return operatorNodeName;
        }

        private string RenderSequenceCommitAndRestartPersistenceProviderTransaction(SequenceCommitAndRestartPersistenceProviderTransaction seqCommitAndRestartPPTrans, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqCommitAndRestartPPTrans, highlightingMode, ">:<");
            return operatorNodeName;
        }

        private string RenderSequenceForContainer(SequenceForContainer seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("for{");
            sb.Append(seqFor.Var.Name);
            if(seqFor.VarDst != null)
                sb.Append("->" + seqFor.VarDst.Name);
            sb.Append(" in " + seqFor.Container.Name);
            sb.Append("; ");
            sb.Append(".");
            sb.Append("}");

            String operatorNodeName = AddNode(seqFor, highlightingMode, sb.ToString());
            String subNodeName = RenderSequence(seqFor.Seq, seqFor, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            return operatorNodeName;
        }

        private string RenderSequenceForIntegerRange(SequenceForIntegerRange seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqFor, highlightingMode, "for{" + seqFor.Var.Name + " in [" + seqFor.Left.Symbol + ":" + seqFor.Right.Symbol + "]; .}");
            //String minExprNodeName = PrintSequenceExpression(seqFor.Left, seqFor, highlightingMode);
            //env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, minExprNodeName), operatorNodeName, minExprNodeName, GetEdgeRealizer(highlightingMode), "minExpr");
            //String maxExprNodeName = PrintSequenceExpression(seqFor.Right, seqFor, highlightingMode);
            //env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, maxExprNodeName), operatorNodeName, maxExprNodeName, GetEdgeRealizer(highlightingMode), "maxExpr");
            String subNodeName = RenderSequence(seqFor.Seq, seqFor, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            return operatorNodeName;
        }

        private string RenderSequenceForIndexAccessEquality(SequenceForIndexAccessEquality seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("for{");
            sb.Append(seqFor.Var.Name);
            sb.Append(" in {");
            sb.Append(seqFor.IndexName);
            sb.Append("==");
            sb.Append(seqFor.Expr.Symbol);
            sb.Append("}; ");
            sb.Append(".");
            sb.Append("}");

            String operatorNodeName = AddNode(seqFor, highlightingMode, sb.ToString());
            //String minExprNodeName = PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
            //env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, minExprNodeName), operatorNodeName, minExprNodeName, GetEdgeRealizer(highlightingMode), "expr");
            String subNodeName = RenderSequence(seqFor.Seq, seqFor, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            return operatorNodeName;
        }

        private string RenderSequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("for{");
            sb.Append(seqFor.Var.Name);
            sb.Append(" in {");
            if(seqFor.Ascending)
                sb.Append("ascending");
            else
                sb.Append("descending");
            sb.Append("(");

            if(seqFor.From() != null && seqFor.To() != null)
            {
                sb.Append(seqFor.IndexName);
                sb.Append(seqFor.DirectionAsString(seqFor.Direction));
                sb.Append(seqFor.Expr.Symbol);
                sb.Append(",");
                sb.Append(seqFor.IndexName);
                sb.Append(seqFor.DirectionAsString(seqFor.Direction2));
                sb.Append(seqFor.Expr2.Symbol);
            }
            else if(seqFor.From() != null)
            {
                sb.Append(seqFor.IndexName);
                sb.Append(seqFor.DirectionAsString(seqFor.Direction));
                sb.Append(seqFor.Expr);
            }
            else if(seqFor.To() != null)
            {
                sb.Append(seqFor.IndexName);
                sb.Append(seqFor.DirectionAsString(seqFor.Direction));
                sb.Append(seqFor.Expr);
            }
            else
            {
                sb.Append(seqFor.IndexName);
            }
            sb.Append(")");
            sb.Append("}; ");
            sb.Append(".");
            sb.Append("}");

            String operatorNodeName = AddNode(seqFor, highlightingMode, sb.ToString());

            /*if(seqFor.From() != null && seqFor.To() != null)
            {
                String condExprNodeName = PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, condExprNodeName), operatorNodeName, condExprNodeName, GetEdgeRealizer(highlightingMode), "expr1");
                String cond2ExprNodeName = PrintSequenceExpression(seqFor.Expr2, seqFor, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, cond2ExprNodeName), operatorNodeName, cond2ExprNodeName, GetEdgeRealizer(highlightingMode), "expr2");
            }
            else if(seqFor.From() != null)
            {
                String condExprNodeName = PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, condExprNodeName), operatorNodeName, condExprNodeName, GetEdgeRealizer(highlightingMode), "expr1");
            }
            else if(seqFor.To() != null)
            {
                String condExprNodeName = PrintSequenceExpression(seqFor.Expr, seqFor, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, condExprNodeName), operatorNodeName, condExprNodeName, GetEdgeRealizer(highlightingMode), "expr1");
            }*/

            String subNodeName = RenderSequence(seqFor.Seq, seqFor, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            return operatorNodeName;
        }

        private string RenderSequenceForFunction(SequenceForFunction seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqFor, highlightingMode, "for{" + seqFor.Var.Name + " in " + seqFor.FunctionSymbol + "(); .}");

            string argumentsAsString = null;
            /*string[] argumentExprNodeNames = */PrintArguments(seqFor.ArgExprs, seqFor, highlightingMode, ref argumentsAsString); // TODO: PrintArguments is/was called with parent, strange, copy n paste omission or intended?
            /*for(int i = 0; i<argumentExprNodeNames.Length; ++i)
            {
                string argumentExprNodeName = argumentExprNodeNames[i];
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, argumentExprNodeName), operatorNodeName, argumentExprNodeName, GetEdgeRealizer(highlightingMode), "argument" + i);
            }*/
            env.guiForDataRendering.graphViewer.SetNodeLabel(operatorNodeName, "for{" + seqFor.Var.Name + " in " + seqFor.FunctionSymbol + argumentsAsString + "; .}");

            String subNodeName = RenderSequence(seqFor.Seq, seqFor, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            return operatorNodeName;
        }

        private string RenderSequenceForMatch(SequenceForMatch seqFor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqFor == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqFor, highlightingModeLocal, "for{" + seqFor.Var.Name + " in [?.]; .}");
            String subNodeName = RenderSequence(seqFor.Seq, seqFor, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "loopedBody");
            String ruleNodeName = RenderSequence(seqFor.Rule, seqFor, highlightingModeLocal);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, ruleNodeName), operatorNodeName, ruleNodeName, GetEdgeRealizer(highlightingModeLocal), "rule");
            return operatorNodeName;
        }

        private string RenderSequenceExecuteInSubgraph(SequenceExecuteInSubgraph seqExecInSub, SequenceBase parent, HighlightingMode highlightingMode, int indexInParallelExecute, HighlightingMode highlightingModeLocal)
        {
            StringBuilder sb = new StringBuilder();
            if(indexInParallelExecute != -1 && context.sequences != null)
            {
                if(seqExecInSub == context.highlightSeq)
                    sb.Append(">>");
                if(seqExecInSub == context.sequences[indexInParallelExecute])
                    sb.Append("(" + indexInParallelExecute + ")");
            }
            sb.Append("in " + seqExecInSub.SubgraphExpr.Symbol + (seqExecInSub.ValueExpr != null ? "," + seqExecInSub.ValueExpr.Symbol : "") + " {.}}");
            if(indexInParallelExecute != -1 && context.sequences != null)
            {
                if(seqExecInSub == context.highlightSeq)
                    sb.Append("<<");
            }

            HighlightingMode highlightingModeInContext = indexInParallelExecute != -1 ? highlightingModeLocal : highlightingMode;
            HighlightingMode finalHighlightingMode = highlightingModeInContext;
            if(indexInParallelExecute != -1 && context.sequences != null && (seqExecInSub == context.highlightSeq || seqExecInSub == context.sequences[indexInParallelExecute]))
                finalHighlightingMode = HighlightingMode.Choicepoint;
            String operatorNodeName = AddNode(seqExecInSub, finalHighlightingMode, sb.ToString());

            /*String subgraphExprNodeName = PrintSequenceExpression(seqExecInSub.SubgraphExpr, seqExecInSub, highlightingModeInContext);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subgraphExprNodeName), operatorNodeName, subgraphExprNodeName, GetEdgeRealizer(highlightingModeInContext), "subgraphExpr");
            if(seqExecInSub.ValueExpr != null)
            {
                String valueExprNodeName = PrintSequenceExpression(seqExecInSub.ValueExpr, seqExecInSub, highlightingModeInContext);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, valueExprNodeName), operatorNodeName, valueExprNodeName, GetEdgeRealizer(highlightingModeInContext), "valueExpr");
            }*/
            String subNodeName = RenderSequence(seqExecInSub.Seq, seqExecInSub, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingModeInContext), "sub");
            return operatorNodeName;
        }

        private string RenderSequenceParallelExecute(SequenceParallelExecute seqParallelExec, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqParallelExec == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqParallelExec, highlightingModeLocal, "parallel");

            for(int i = seqParallelExec.InSubgraphExecutions.Count - 1; i >= 0; --i)
            {
                SequenceExecuteInSubgraph seqExecInSub = seqParallelExec.InSubgraphExecutions[i];
                string executeInSubgraphNodeName = RenderSequenceExecuteInSubgraph(seqExecInSub, seqParallelExec, highlightingModeLocal, i, highlightingModeLocal);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, executeInSubgraphNodeName), operatorNodeName, executeInSubgraphNodeName, GetEdgeRealizer(highlightingModeLocal), "sub" + i);
            }

            return operatorNodeName;
        }

        private string RenderSequenceParallelArrayExecute(SequenceParallelArrayExecute seqParallelArrayExec, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqParallelArrayExec == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqParallelArrayExec, highlightingModeLocal, "parallel array");

            for(int i = seqParallelArrayExec.InSubgraphExecutions.Count - 1; i >= 0; --i)
            {
                SequenceExecuteInSubgraph seqExecInSub = seqParallelArrayExec.InSubgraphExecutions[i];
                string executeInSubgraphNodeName = RenderSequenceExecuteInSubgraph(seqExecInSub, seqParallelArrayExec, highlightingModeLocal, i, highlightingModeLocal);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, executeInSubgraphNodeName), operatorNodeName, executeInSubgraphNodeName, GetEdgeRealizer(highlightingModeLocal), "sub" + i);
            }

            return operatorNodeName;
        }

        private string RenderSequenceLock(SequenceLock seqLock, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqLock, highlightingMode, "lock(" + seqLock.LockObjectExpr + "){.}");
            String subNodeName = RenderSequence(seqLock.Seq, seqLock, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subNodeName), operatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
            //String lockObjectExprNodeName = PrintSequenceExpression(seqLock.LockObjectExpr, seqLock, highlightingMode);
            //env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, lockObjectExprNodeName), operatorNodeName, lockObjectExprNodeName, GetEdgeRealizer(highlightingMode), "minExpr");
            return operatorNodeName;
        }

        private string RenderSequenceIfThenElse(SequenceIfThenElse seqIf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String operatorNodeName = AddNode(seqIf, highlightingMode, "if{.;.;.}");
            String falseCaseNodeName = RenderSequence(seqIf.FalseCase, seqIf, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, falseCaseNodeName), operatorNodeName, falseCaseNodeName, GetEdgeRealizer(highlightingMode), "falseCase");
            String trueCaseNodeName = RenderSequence(seqIf.TrueCase, seqIf, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, trueCaseNodeName), operatorNodeName, trueCaseNodeName, GetEdgeRealizer(highlightingMode), "trueCase");
            String conditionNodeName = RenderSequence(seqIf.Condition, seqIf, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, conditionNodeName), operatorNodeName, conditionNodeName, GetEdgeRealizer(highlightingMode), "condition");
            return operatorNodeName;
        }

        private string RenderSequenceNAry(SequenceNAry seqN, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null)
            {
                string choicePrefix = GetChoicePrefix(seqN);
                String operatorNodeNameChoiceRun = AddNode(seqN, HighlightingMode.Choicepoint, choicePrefix + (seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(...)");
                for(int iChoiceRun = seqN.Sequences.Count - 1; iChoiceRun >= 0; --iChoiceRun)
                {
                    string childNodeNameChoiceRun = RenderSequence(seqN.Sequences[iChoiceRun], seqN, highlightingMode);
                    env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, childNodeNameChoiceRun), operatorNodeNameChoiceRun, childNodeNameChoiceRun, GetEdgeRealizer(highlightingMode), "child" + iChoiceRun);
                }
                return operatorNodeNameChoiceRun;
            }

            bool highlight = false;
            foreach(Sequence seqChild in seqN.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            if(highlight && context.choice)
            {
                String operatorNodeNameChoiceRun = AddNode(seqN, HighlightingMode.Choicepoint, (seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(...)");

                for(int iChoiceRun = seqN.Sequences.Count - 1; iChoiceRun >= 0; --iChoiceRun)
                {
                    Sequence seqChild = seqN.Sequences[iChoiceRun];

                    StringBuilder sb = new StringBuilder();
                    if(seqChild == context.highlightSeq)
                    {
                        sb.Append(">>");
                    }
                    if(context.sequences != null)
                    {
                        for(int i = 0; i < context.sequences.Count; ++i)
                        {
                            if(seqChild == context.sequences[i])
                                sb.Append("(" + i + ")");
                        }
                    }
                    sb.Append("child" + iChoiceRun);
                    if(seqChild == context.highlightSeq)
                    {
                        sb.Append("<<");
                    }

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    string childNodeNameChoiceRun = RenderSequence(seqChild, seqN, highlightingMode);
                    context.highlightSeq = highlightSeqBackup;

                    env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, childNodeNameChoiceRun), operatorNodeNameChoiceRun, childNodeNameChoiceRun, GetEdgeRealizer(HighlightingMode.Choicepoint), sb.ToString());
                }
                return operatorNodeNameChoiceRun;
            }

            String operatorNodeName = AddNode(seqN, HighlightingMode.Choicepoint, (seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(...)");
            RenderChildren(seqN, operatorNodeName, highlightingMode, highlightingMode);

            return operatorNodeName;
        }

        private string RenderSequenceWeightedOne(SequenceWeightedOne seqWeighted, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null)
            {
                string choicePrefix = GetChoicePrefix(seqWeighted);
                String operatorNodeNameChoiceRun = AddNode(seqWeighted, highlightingMode, choicePrefix + (seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(...)");
                for(int iChoiceRun = seqWeighted.Sequences.Count - 1; iChoiceRun >= 0; --iChoiceRun)
                {
                    string childNodeNameChoiceRun = RenderSequence(seqWeighted.Sequences[iChoiceRun], seqWeighted, highlightingMode);
                    string edgePrefixChoiceRun = (iChoiceRun == 0 ? "0.00" : seqWeighted.Numbers[iChoiceRun - 1].ToString(System.Globalization.CultureInfo.InvariantCulture)) + "-" + seqWeighted.Numbers[iChoiceRun].ToString(System.Globalization.CultureInfo.InvariantCulture) + " "; // todo: format auf 2 nachkommastellen 
                    env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, childNodeNameChoiceRun), operatorNodeNameChoiceRun, childNodeNameChoiceRun, GetEdgeRealizer(highlightingMode), edgePrefixChoiceRun + "child" + iChoiceRun);
                }
                return operatorNodeNameChoiceRun;
            }

            bool highlight = false;
            foreach(Sequence seqChild in seqWeighted.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            if(highlight && context.choice)
            {
                String operatorNodeNameChoiceRun = AddNode(seqWeighted, HighlightingMode.Choicepoint, (seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(...)");
                for(int iChoiceRun = seqWeighted.Sequences.Count - 1; iChoiceRun >= 0; --iChoiceRun)
                {
                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    string childNodeNameChoiceRun = RenderSequence(seqWeighted.Sequences[iChoiceRun], seqWeighted, highlightingMode);
                    context.highlightSeq = highlightSeqBackup;

                    HighlightingMode highlightingModeChoiceRun = highlightingMode;
                    StringBuilder sb = new StringBuilder();
                    string edgePrefixChoiceRun = (iChoiceRun == 0 ? "0.00" : seqWeighted.Numbers[iChoiceRun - 1].ToString(System.Globalization.CultureInfo.InvariantCulture)) + "-" + seqWeighted.Numbers[iChoiceRun].ToString(System.Globalization.CultureInfo.InvariantCulture) + " "; // todo: format auf 2 nachkommastellen 
                    sb.Append(edgePrefixChoiceRun);
                    if(seqWeighted.Sequences[iChoiceRun] == context.highlightSeq)
                    {
                        sb.Append(">>");
                        highlightingModeChoiceRun = HighlightingMode.Choicepoint;
                    }
                    sb.Append("child" + iChoiceRun);
                    if(seqWeighted.Sequences[iChoiceRun] == context.highlightSeq)
                        sb.Append("<<");

                    env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, childNodeNameChoiceRun), operatorNodeNameChoiceRun, childNodeNameChoiceRun, GetEdgeRealizer(highlightingModeChoiceRun), sb.ToString());
                }
                return operatorNodeNameChoiceRun;
            }

            String operatorNodeName = AddNode(seqWeighted, highlightingMode, (seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(...)");
            for(int i = seqWeighted.Sequences.Count - 1; i >= 0; --i)
            {
                string childNodeName = RenderSequence(seqWeighted.Sequences[i], seqWeighted, highlightingMode);
                string edgePrefix = (i == 0 ? "0.00" : seqWeighted.Numbers[i - 1].ToString(System.Globalization.CultureInfo.InvariantCulture)) + "-" + seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture) + " "; // todo: format auf 2 nachkommastellen 
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, childNodeName), operatorNodeName, childNodeName, GetEdgeRealizer(highlightingMode), edgePrefix + "child" + i);
            }

            return operatorNodeName;
        }

        private string RenderSequenceSomeFromSet(SequenceSomeFromSet seqSome, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(context.sequenceIdToChoicepointPosMap != null
                && seqSome.Random)
            {
                string choicePrefix = GetChoicePrefix(seqSome);
                String operatorNodeNameChoiceRun = AddNode(seqSome, highlightingMode, choicePrefix + (seqSome.Choice ? "$%" : "$") + "{<...>}");
                for(int iChoiceRun = seqSome.Sequences.Count - 1; iChoiceRun >= 0; --iChoiceRun)
                {
                    Sequence seqChildChoiceRun = seqSome.Sequences[iChoiceRun];
                    Dictionary<int, int> sequenceIdToChoicepointPosMapBackup = context.sequenceIdToChoicepointPosMap; // TODO: this works? choicepoint numbers maybe not displayed this way, but still assigned on the outside...
                    context.sequenceIdToChoicepointPosMap = null; // rules within some-from-set are not choicepointable
                    string childNodeNameChoiceRun = RenderSequence(seqChildChoiceRun, seqSome, highlightingMode);
                    context.sequenceIdToChoicepointPosMap = sequenceIdToChoicepointPosMapBackup;
                    env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, childNodeNameChoiceRun), operatorNodeNameChoiceRun, childNodeNameChoiceRun, GetEdgeRealizer(highlightingMode), "child" + iChoiceRun);
                }
                return operatorNodeNameChoiceRun;
            }

            bool highlight = false;
            foreach(Sequence seqChild in seqSome.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            if(highlight && context.choice)
            {
                String operatorNodeNameChoiceRun = AddNode(seqSome, HighlightingMode.Choicepoint, (seqSome.Choice ? "$%" : "$") + "{<...>}");
                int numCurTotalMatch = 0; // potential todo: pre-compute the match numbers in another loop, so there's no dependency in between the loops
                for(int i = 0; i < context.matches.Count; ++i)
                    numCurTotalMatch += seqSome.IsNonRandomRuleAllCall(i) ? context.matches[i].Count : 1;
                for(int iChoiceRun = seqSome.Sequences.Count - 1; iChoiceRun >= 0; --iChoiceRun)
                {
                    Sequence seqChildChoiceRun = seqSome.Sequences[iChoiceRun];
                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    string childNodeNameChoiceRun = RenderSequence(seqChildChoiceRun, seqSome, highlightingMode);
                    context.highlightSeq = highlightSeqBackup;

                    HighlightingMode highlightingModeChoiceRun = highlightingMode;
                    StringBuilder sb = new StringBuilder();
                    if(seqChildChoiceRun == context.highlightSeq)
                    {
                        sb.Append(">>");
                        highlightingModeChoiceRun = HighlightingMode.Choicepoint;
                    }
                    if(context.sequences != null)
                    {
                        for(int i = context.sequences.Count - 1; i >= 0; --i)
                        {
                            if(seqChildChoiceRun == context.sequences[i] && context.matches[i].Count > 0)
                            {
                                numCurTotalMatch -= seqSome.IsNonRandomRuleAllCall(i) ? context.matches[i].Count : 1;
                                sb.Append(GetListOfMatchesNumbers(numCurTotalMatch, seqSome.IsNonRandomRuleAllCall(i) ? context.matches[i].Count : 1));
                            }
                        }
                    }
                    sb.Append("child" + iChoiceRun);
                    if(seqChildChoiceRun == context.highlightSeq)
                        sb.Append("<<");

                    env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeNameChoiceRun, childNodeNameChoiceRun), operatorNodeNameChoiceRun, childNodeNameChoiceRun, GetEdgeRealizer(highlightingModeChoiceRun), sb.ToString());
                }
                return operatorNodeNameChoiceRun;
            }

            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqSome == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqSome, highlightingModeLocal, (seqSome.Random ? (seqSome.Choice ? "$%" : "$") : "") + "{<...>}");
            RenderChildren(seqSome, operatorNodeName, highlightingMode, highlightingModeLocal);

            return operatorNodeName;
        }

        private string RenderSequenceMultiRulePrefixedSequence(SequenceMultiRulePrefixedSequence seqMulti, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqMulti == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqMulti, highlightingModeLocal, "[[...]]");

            for(int i = seqMulti.RulePrefixedSequences.Count - 1; i >= 0; --i)
            {
                SequenceRulePrefixedSequence seqRulePrefixedSequence = seqMulti.RulePrefixedSequences[i];

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == context.highlightSeq)
                    highlightingModeRulePrefixedSequence = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                String subOperatorNodeName = AddNode(seqRulePrefixedSequence, highlightingModeRulePrefixedSequence, "for{.;.}");
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, subOperatorNodeName), operatorNodeName, subOperatorNodeName, GetEdgeRealizer(highlightingMode), "sub" + i);

                String subNodeName = RenderSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(subOperatorNodeName, subNodeName), subOperatorNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
                String ruleNodeName = RenderSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(subOperatorNodeName, ruleNodeName), subOperatorNodeName, ruleNodeName, GetEdgeRealizer(highlightingModeRulePrefixedSequence), "rule");
            }

            StringBuilder sb = new StringBuilder();
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                string filterCallAsString = null;
                PrintSequenceFilterCall(filterCall, seqMulti, highlightingModeLocal, ref filterCallAsString); //highlightingModeLocal
                sb.Append(filterCallAsString);
            }
            env.guiForDataRendering.graphViewer.SetNodeLabel(operatorNodeName, "[[...]]" + sb.ToString());

            return operatorNodeName;
        }

        private string RenderSequenceMultiRuleAllCall(SequenceMultiRuleAllCall seqMulti, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqMulti == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqMulti, highlightingModeLocal, "[[...]]");
            RenderChildren(seqMulti, operatorNodeName, highlightingMode, highlightingModeLocal);

            StringBuilder sb = new StringBuilder();
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                string filterCallAsString = null;
                PrintSequenceFilterCall(filterCall, seqMulti, highlightingModeLocal, ref filterCallAsString); //highlightingModeLocal
                sb.Append(filterCallAsString);
            }
            env.guiForDataRendering.graphViewer.SetNodeLabel(operatorNodeName, "[[...]]" + sb.ToString());

            return operatorNodeName;
        }

        private string RenderSequenceRulePrefixedSequence(SequenceRulePrefixedSequence seqRulePrefixedSequence, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqRulePrefixedSequence == context.highlightSeq)
                highlightingModeLocal = context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = AddNode(seqRulePrefixedSequence, highlightingModeLocal, "[for{.;.}]");
            String rightNodeName = RenderSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, rightNodeName), operatorNodeName, rightNodeName, GetEdgeRealizer(highlightingMode), "sub");
            String leftNodeName = RenderSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeLocal);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(operatorNodeName, leftNodeName), operatorNodeName, leftNodeName, GetEdgeRealizer(highlightingModeLocal), "rule");
            return operatorNodeName;
        }

        internal string RenderSequenceBreakpointable(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode, string prefixFromOuterConstruct)
        {
            HighlightingMode highlightingModeOverride = HighlightingMode.None;
            string prefix = "";
            if(context.sequenceIdToBreakpointPosMap != null)
            {
                prefix = GetBreakPrefix((SequenceSpecial)seq);
                highlightingModeOverride = HighlightingMode.Breakpoint;
            }

            if(context.sequenceIdToChoicepointPosMap != null
                && seq is SequenceRandomChoice
                && ((SequenceRandomChoice)seq).Random)
            {
                prefix = GetChoicePrefix((SequenceRandomChoice)seq);
                highlightingModeOverride = HighlightingMode.Choicepoint;
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
                return RenderSequenceAtom(seq, parent, highlightingMode, prefixFromOuterConstruct + prefix);
            else
                return RenderSequenceAtom(seq, parent, highlightingModeOverride != HighlightingMode.None ? highlightingModeOverride : highlightingModeLocal, prefixFromOuterConstruct + prefix); // GUI TODO: review override
        }

        private string RenderSequenceAtom(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode, string prefix)
        {
            switch(seq.SequenceType)
            {
            case SequenceType.SequenceCall:
                return RenderSequenceSequenceCall((SequenceSequenceCallInterpreted)seq, parent, highlightingMode, prefix);
            case SequenceType.RuleCall:
                return RenderSequenceRuleCall((SequenceRuleCall)seq, parent, highlightingMode, prefix);
            case SequenceType.RuleAllCall:
                return RenderSequenceRuleAllCall((SequenceRuleAllCall)seq, parent, highlightingMode, prefix);
            case SequenceType.RuleCountAllCall:
                return RenderSequenceRuleCountAllCall((SequenceRuleCountAllCall)seq, parent, highlightingMode, prefix);
            case SequenceType.BooleanComputation:
                return RenderSequenceBooleanComputation((SequenceBooleanComputation)seq, parent, highlightingMode, prefix);
            default:
                Debug.Assert(false);
                return "<UNKNOWN_SEQUENCE_TYPE>";
            }
        }

        private string RenderSequenceSequenceCall(SequenceSequenceCallInterpreted seq, SequenceBase parent, HighlightingMode highlightingMode, string prefix)
        {
            String callNodeName = AddNode(seq, highlightingMode, "");

            StringBuilder sb = new StringBuilder();
            sb.Append(prefix);
            if(seq.Special)
                sb.Append("%"); // TODO: questionable position here and in sequence -- should appear before sequence name, not return assignment
            string returnAssignmentsAsString = null;
            PrintReturnAssignments(callNodeName, seq.ReturnVars, parent, highlightingMode, ref returnAssignmentsAsString);
            sb.Append(returnAssignmentsAsString);
            if(seq.subgraph != null)
                sb.Append(seq.subgraph.Name + ".");
            sb.Append(seq.SequenceDef.Name);
            if(seq.ArgumentExpressions.Length > 0)
            {
                string argumentsAsString = null;
                PrintArguments(seq.ArgumentExpressions, parent, highlightingMode, ref argumentsAsString);
                sb.Append(argumentsAsString);
            }

            env.guiForDataRendering.graphViewer.SetNodeLabel(callNodeName, sb.ToString());
            return callNodeName;
        }

        private void PrintArguments(SequenceExpression[] arguments, SequenceBase parent, HighlightingMode highlightingMode, ref string argumentsAsString)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for(int i = 0; i < arguments.Length; ++i)
            {
                //PrintSequenceExpression(arguments[i], parent, highlightingMode);
                sb.Append(arguments[i].Symbol);
                if(i != arguments.Length - 1)
                    sb.Append(",");
            }
            sb.Append(")");
            argumentsAsString = sb.ToString();
        }

        private void PrintArguments(IList<SequenceExpression> arguments, SequenceBase parent, HighlightingMode highlightingMode, ref string argumentsAsString)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for(int i = 0; i < arguments.Count; ++i)
            {
                //PrintSequenceExpression(arguments[i], parent, highlightingMode);
                sb.Append(arguments[i].Symbol);
                if(i != arguments.Count - 1)
                    sb.Append(",");
            }
            sb.Append(")");
            argumentsAsString = sb.ToString();
        }

        private string RenderSequenceRuleCall(SequenceRuleCall seq, SequenceBase parent, HighlightingMode highlightingMode, string prefix)
        {
            String callNodeName = AddNode(seq, highlightingMode, "");

            StringBuilder sb = new StringBuilder();
            sb.Append(prefix);
            string returnAssignmentsAsString = null;
            PrintReturnAssignments(callNodeName, seq.ReturnVars, parent, highlightingMode, ref returnAssignmentsAsString);
            sb.Append(returnAssignmentsAsString);
            sb.Append(seq.TestDebugPrefix);
            string ruleCallAsString = null;
            PrintRuleCallString(callNodeName, seq, parent, highlightingMode, ref ruleCallAsString);
            sb.Append(ruleCallAsString);

            env.guiForDataRendering.graphViewer.SetNodeLabel(callNodeName, sb.ToString());
            return callNodeName;
        }

        private string RenderSequenceRuleAllCall(SequenceRuleAllCall seq, SequenceBase parent, HighlightingMode highlightingMode, string prefix)
        {
            String callNodeName = AddNode(seq, highlightingMode, "");

            StringBuilder sb = new StringBuilder();
            sb.Append(prefix);
            string returnAssignmentsAsString = null;
            PrintReturnAssignments(callNodeName, seq.ReturnVars, parent, highlightingMode, ref returnAssignmentsAsString);
            sb.Append(returnAssignmentsAsString);
            sb.Append(seq.RandomChoicePrefix);
            sb.Append("[");
            sb.Append(seq.TestDebugPrefix);
            string ruleCallAsString = null;
            PrintRuleCallString(callNodeName, seq, parent, highlightingMode, ref ruleCallAsString);
            sb.Append(ruleCallAsString);
            sb.Append("]");

            env.guiForDataRendering.graphViewer.SetNodeLabel(callNodeName, sb.ToString());
            return callNodeName;
        }

        private string RenderSequenceRuleCountAllCall(SequenceRuleCountAllCall seq, SequenceBase parent, HighlightingMode highlightingMode, string prefix)
        {
            String callNodeName = AddNode(seq, highlightingMode, "");

            StringBuilder sb = new StringBuilder();
            sb.Append(prefix);
            string returnAssignmentsAsString = null;
            PrintReturnAssignments(callNodeName, seq.ReturnVars, parent, highlightingMode, ref returnAssignmentsAsString);
            sb.Append(returnAssignmentsAsString);
            sb.Append("count[");
            sb.Append(seq.TestDebugPrefix);
            string ruleCallAsString = null;
            PrintRuleCallString(callNodeName, seq, parent, highlightingMode, ref ruleCallAsString);
            sb.Append(ruleCallAsString);
            sb.Append("]" + "=>" + seq.CountResult.Name);

            env.guiForDataRendering.graphViewer.SetNodeLabel(callNodeName, sb.ToString());
            return callNodeName;
        }

        internal void PrintReturnAssignments(String callNodeName, SequenceVariable[] returnVars, SequenceBase parent, HighlightingMode highlightingMode, ref string returnAssignmentsAsString)
        {
            StringBuilder sb = new StringBuilder();
            if(returnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < returnVars.Length; ++i)
                {
                    sb.Append(returnVars[i].Name);
                    if(i != returnVars.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")=");
            }
            returnAssignmentsAsString = sb.ToString();
        }

        internal void PrintRuleCallString(String callNodeName, SequenceRuleCall seq, SequenceBase parent, HighlightingMode highlightingMode, ref string ruleCallAsString)
        {
            StringBuilder sb = new StringBuilder();
            if(seq.subgraph != null)
                sb.Append(seq.subgraph.Name + ".");
            sb.Append(seq.Name);
            if(seq.ArgumentExpressions.Length > 0)
            {
                string argumentsAsString = null;
                PrintArguments(seq.ArgumentExpressions, parent, highlightingMode, ref argumentsAsString);
                sb.Append(argumentsAsString);
            }
            for(int i = 0; i < seq.Filters.Count; ++i)
            {
                string filterCallAsString = null;
                PrintSequenceFilterCall(seq.Filters[i], seq, highlightingMode, ref filterCallAsString);
                sb.Append(filterCallAsString);
            }
            ruleCallAsString = sb.ToString();
        }

        internal void PrintSequenceFilterCall(SequenceFilterCallBase seq, SequenceBase parent, HighlightingMode highlightingMode, ref string filterCallString)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\\");
            if(seq is SequenceFilterCallInterpreted)
            {
                SequenceFilterCallInterpreted filterCall = (SequenceFilterCallInterpreted)seq;
                if(filterCall.MatchClass != null)
                    sb.Append(filterCall.MatchClass.info.PackagePrefixedName + ".");
                sb.Append(filterCall.PackagePrefixedName);
                string argumentsAsString = null;
                PrintArguments(filterCall.ArgumentExpressions, parent, highlightingMode, ref argumentsAsString);
                sb.Append(argumentsAsString);
            }
            else if(seq is SequenceFilterCallLambdaExpressionInterpreted)
            {
                SequenceFilterCallLambdaExpressionInterpreted filterCall = (SequenceFilterCallLambdaExpressionInterpreted)seq;
                if(filterCall.MatchClass != null)
                    sb.Append(filterCall.MatchClass.info.PackagePrefixedName + ".");
                sb.Append(filterCall.Name);
                //if(filterCall.Entity != null)
                //    sb.Append("<" + filterCall.Entity + ">");
                if(filterCall.FilterCall.initExpression != null)
                {
                    sb.Append("{");
                    if(filterCall.FilterCall.initArrayAccess != null)
                        sb.Append(filterCall.FilterCall.initArrayAccess.Name + "; ");
                    //PrintSequenceExpression(filterCall.FilterCall.initExpression, parent, highlightingMode);
                    sb.Append(filterCall.FilterCall.initExpression.Symbol);
                    sb.Append("}");
                }
                sb.Append("{");
                if(filterCall.FilterCall.arrayAccess != null)
                    sb.Append(filterCall.FilterCall.arrayAccess.Name + "; ");
                if(filterCall.FilterCall.previousAccumulationAccess != null)
                    sb.Append(filterCall.FilterCall.previousAccumulationAccess + ", ");
                if(filterCall.FilterCall.index != null)
                    sb.Append(filterCall.FilterCall.index.Name + " -> ");
                sb.Append(filterCall.FilterCall.element.Name + " -> ");
                //PrintSequenceExpression(filterCall.FilterCall.lambdaExpression, parent, highlightingMode);
                sb.Append(filterCall.FilterCall.lambdaExpression.Symbol);
                sb.Append("}");
            }
            else
            {
                Debug.Assert(false);
            }
            filterCallString = sb.ToString();
        }

        private string RenderSequenceBooleanComputation(SequenceBooleanComputation seqComp, SequenceBase parent, HighlightingMode highlightingMode, string prefix)
        {
            String computationNodeName = AddNode(seqComp, highlightingMode, prefix + seqComp.Computation.Symbol);
            //PrintSequenceComputation(seqComp.Computation, seqComp, highlightingMode);
            return computationNodeName;
        }

        private string RenderSequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar seqAss, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String assignSequenceResultNodeName = AddNode(seqAss, highlightingMode, "(." + seqAss.OperatorSymbol + seqAss.DestVar.Name + ")");
            String subNodeName = RenderSequence(seqAss.Seq, seqAss, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(assignSequenceResultNodeName, subNodeName), assignSequenceResultNodeName, subNodeName, GetEdgeRealizer(highlightingMode), "sub");
            return assignSequenceResultNodeName;
        }

        // Choice highlightable user assignments
        private string RenderSequenceAssignChoiceHighlightable(Sequence seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            String assignChoiceNodeName;

            if(context.sequenceIdToChoicepointPosMap != null
                && (seq is SequenceAssignRandomIntToVar || seq is SequenceAssignRandomDoubleToVar))
            {
                assignChoiceNodeName = AddNode(seq, highlightingMode, GetChoicePrefix((SequenceRandomChoice)seq) + seq.Symbol);
                return assignChoiceNodeName;
            }

            if(seq == context.highlightSeq && context.choice)
                assignChoiceNodeName = AddNode(seq, HighlightingMode.Choicepoint, seq.Symbol);
            else
                assignChoiceNodeName = AddNode(seq, highlightingMode, seq.Symbol);
            return assignChoiceNodeName;
        }

        private string RenderSequenceDefinitionInterpreted(SequenceDefinitionInterpreted seqDef, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = HighlightingMode.None;
            if(seqDef.ExecutionState == SequenceExecutionState.Success)
                highlightingModeLocal = HighlightingMode.LastSuccess;
            if(seqDef.ExecutionState == SequenceExecutionState.Fail)
                highlightingModeLocal = HighlightingMode.LastFail;

            string sequenceDefNodeName = AddNode(seqDef, highlightingModeLocal, seqDef.Symbol + ": ");
            string seqName = RenderSequence(seqDef.Seq, seqDef.Seq, highlightingMode);
            env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(sequenceDefNodeName, seqName), sequenceDefNodeName, seqName, GetEdgeRealizer(highlightingMode), "body");

            return sequenceDefNodeName;
        }

        private string RenderSequenceAssignContainerConstructorToVar(SequenceAssignContainerConstructorToVar seq, SequenceBase parent, HighlightingMode highlightingMode)
        {
            string sequenceAssignNodeName = AddNode(seq, highlightingMode, seq.DestVar.Name + "=" + seq.Constructor.Symbol);
            //PrintSequenceExpression(seq.Constructor, seq, highlightingMode);
            return sequenceAssignNodeName;
        }

        private void RenderChildren(Sequence seq, string seqNodeName, HighlightingMode highlightingModeChildren, HighlightingMode highlightingMode)
        {
            List<Sequence> children = new List<Sequence>(seq.Children); // it seems the layout algorithm puts the first added child to the right and the last added child to the left, while we want it to be in reverse order
            for(int i = children.Count - 1; i >= 0; --i)
            {
                string childNodeName = RenderSequence(children[i], seq, highlightingModeChildren);
                env.guiForDataRendering.graphViewer.AddEdge(GetUniqueEdgeName(seqNodeName, childNodeName), seqNodeName, childNodeName, GetEdgeRealizer(highlightingMode), "child" + i);
            }
        }

        private string GetChoicePrefix(SequenceRandomChoice seq)
        {
            if(!context.sequenceIdToChoicepointPosMap.ContainsKey(((SequenceBase)seq).Id))
                return ""; // tests/rules in sequence expressions are not choicepointable (at the moment)
            if(seq.Choice)
                return "-%" + context.sequenceIdToChoicepointPosMap[((SequenceBase)seq).Id] + "-:"; // HighlightingMode.Choicepoint
            else
                return "+%" + context.sequenceIdToChoicepointPosMap[((SequenceBase)seq).Id] + "+:"; // HighlightingMode.Choicepoint
        }

        internal string GetBreakPrefix(ISequenceSpecial seq)
        {
            if(!context.sequenceIdToBreakpointPosMap.ContainsKey(((SequenceBase)seq).Id))
                return ""; // tests/rules in sequence expressions are not breakpointable (at the moment)
            if(seq.Special)
                return "-%" + context.sequenceIdToBreakpointPosMap[((SequenceBase)seq).Id] + "-:"; // HighlightingMode.Breakpoint
            else
                return "+%" + context.sequenceIdToBreakpointPosMap[((SequenceBase)seq).Id] + "+:"; // HighlightingMode.Breakpoint
        }

        private string GetListOfMatchesNumbers(int numCurTotalMatch, int numMatches)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            bool first = true;
            for(int i = 0; i < numMatches; ++i)
            {
                if(!first)
                    sb.Append(",");
                sb.Append(numCurTotalMatch.ToString());
                ++numCurTotalMatch;
                first = false;
            }
            sb.Append(")");
            return sb.ToString();
        }


        private void RegisterRealizers()
        {
            // GUI TODO: configure graph viewer here or at some other place?
            ITwinConsoleUIDataRenderingGUI debuggerGUIForDataRendering = env.guiForDataRendering;
            IBasicGraphViewerClient graphViewer = debuggerGUIForDataRendering.graphViewer;

            // GUI TODO: implement realizers for Breakpoint/Choicepoint and Focus/FocusSuccess combined, maybe also LastSuccess/LastFail...
            // GUI TODO: distinguish Breakpoint/Choicepoint set from Breakpoint/Choicepoint hit
            graphViewer.AddNodeRealizer("nr0", GrColor.Black, GrColor.White, GrColor.Black, GrNodeShape.Box); // HighlightingMode.None
            graphViewer.AddNodeRealizer("nr1", GrColor.Black, GrColor.Yellow, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Focus
            graphViewer.AddNodeRealizer("nr2", GrColor.Black, GrColor.LightGreen, GrColor.Black, GrNodeShape.Box); // HighlightingMode.FocusSucces
            graphViewer.AddNodeRealizer("nr4", GrColor.Black, GrColor.DarkGreen, GrColor.Black, GrNodeShape.Box); // HighlightingMode.LastSuccess
            graphViewer.AddNodeRealizer("nr8", GrColor.Black, GrColor.DarkRed, GrColor.Black, GrNodeShape.Box); // HighlightingMode.LastFail
            graphViewer.AddNodeRealizer("nr16", GrColor.Red, GrColor.White, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Breakpoint
            graphViewer.AddNodeRealizer("nr32", GrColor.LightPurple, GrColor.White, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Choicepoint
            //graphViewer.AddNodeRealizer("nr64", GrColor.Black, GrColor.Blue, GrColor.Black, GrNodeShape.Box); // HighlightingMode.SequenceStart
            //graphViewer.AddNodeRealizer("nr17", GrColor.Red, GrColor.Yellow, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Breakpoint | HighlightingMode.Focus
            //graphViewer.AddNodeRealizer("nr33", GrColor.LightPurple, GrColor.Yellow, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Choicepoint | HighlightingMode.Focus
            //graphViewer.AddNodeRealizer("nr18", GrColor.Red, GrColor.LightGreen, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Breakpoint | HighlightingMode.FocusSuccess
            //graphViewer.AddNodeRealizer("nr34", GrColor.LightPurple, GrColor.LightGreen, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Choicepoint | HighlightingMode.FocusSuccess

            graphViewer.AddEdgeRealizer("er0", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous); // HighlightingMode.None
            graphViewer.AddEdgeRealizer("er1", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous); // HighlightingMode.Focus
            graphViewer.AddEdgeRealizer("er2", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous); // HighlightingMode.FocusSucces
            graphViewer.AddEdgeRealizer("er4", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous); // HighlightingMode.LastSuccess
            graphViewer.AddEdgeRealizer("er8", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous); // HighlightingMode.LastFail
            graphViewer.AddEdgeRealizer("er16", GrColor.Red, GrColor.Red, 1, GrLineStyle.Continuous); // HighlightingMode.Breakpoint
            graphViewer.AddEdgeRealizer("er32", GrColor.LightPurple, GrColor.LightPurple, 1, GrLineStyle.Continuous); // HighlightingMode.Choicepoint
            //graphViewer.AddEdgeRealizer("er64", GrColor.Black, GrColor.Black, 1, GrLineStyle.Continuous); // HighlightingMode.SequenceStart
            //graphViewer.AddNodeRealizer("er17", GrColor.Red, GrColor.Yellow, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Breakpoint | HighlightingMode.Focus
            //graphViewer.AddNodeRealizer("er33", GrColor.LightPurple, GrColor.Yellow, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Choicepoint | HighlightingMode.Focus
            //graphViewer.AddNodeRealizer("er18", GrColor.Red, GrColor.LightGreen, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Breakpoint | HighlightingMode.FocusSuccess
            //graphViewer.AddNodeRealizer("er34", GrColor.LightPurple, GrColor.LightGreen, GrColor.Black, GrNodeShape.Box); // HighlightingMode.Choicepoint | HighlightingMode.FocusSuccess
        }

        internal string GetUniqueEdgeName(String fromNodeName, String toNodeName)
        {
            return fromNodeName + "->" + toNodeName;
        }

        private string GetNodeRealizer(HighlightingMode highlightingMode)
        {
            // when node is focussed, the state of last execution is ignored during rendering
            if((highlightingMode & HighlightingMode.Focus) == HighlightingMode.Focus || (highlightingMode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces)
                highlightingMode &= ~(HighlightingMode.LastFail | HighlightingMode.LastSuccess);
            return "nr" + ((int)highlightingMode).ToString();
        }

        internal string GetEdgeRealizer(HighlightingMode highlightingMode)
        {
            // when edge is focussed, the state of last execution is ignored during rendering
            if((highlightingMode & HighlightingMode.Focus) == HighlightingMode.Focus || (highlightingMode & HighlightingMode.FocusSucces) == HighlightingMode.FocusSucces)
                highlightingMode &= ~(HighlightingMode.LastFail | HighlightingMode.LastSuccess);
            return "er" + ((int)highlightingMode).ToString();
        }

        internal string AddNode(SequenceBase seqBase, HighlightingMode highlightingMode, string label)
        {
            String nodeName = seqBase.Id.ToString();
            env.guiForDataRendering.graphViewer.AddNode(nodeName, GetNodeRealizer(highlightingMode), label);
            if(groupNodeName != null)
                env.guiForDataRendering.graphViewer.MoveNode(nodeName, groupNodeName);
            return nodeName;
        }
    }
}
