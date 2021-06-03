/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Diagnostics;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    static class SequencePrinter
    {
        /// <summary>
        /// Prints the given root sequence adding parentheses if needed according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="context">The print context</param>
        /// <param name="nestingLevel">The level the sequence is nested in</param>
        public static void PrintSequence(Sequence seq, PrintSequenceContext context, int nestingLevel)
        {
            WorkaroundManager.Workaround.PrintHighlighted(nestingLevel + ">", HighlightingMode.SequenceStart);
            PrintSequence(seq, null, context);
        }

        /// <summary>
        /// Prints the given sequence adding parentheses if needed according to the print context.
        /// </summary>
        /// <param name="seq">The sequence to be printed</param>
        /// <param name="parent">The parent of the sequence or null if the sequence is a root</param>
        /// <param name="context">The print context</param>
        private static void PrintSequence(Sequence seq, SequenceBase parent, PrintSequenceContext context)
        {
            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence)
                Console.Write("(");

            switch(seq.SequenceType)
            {
            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
            case SequenceType.StrictAnd:
                PrintSequenceBinary((SequenceBinary)seq, parent, context);
                break;
            case SequenceType.IfThen:
                PrintSequenceIfThen((SequenceIfThen)seq, parent, context);
                break;
            case SequenceType.Not:
                PrintSequenceNot((SequenceNot)seq, parent, context);
                break;
            case SequenceType.IterationMin:
                PrintSequenceIterationMin((SequenceIterationMin)seq, parent, context);
                break;
            case SequenceType.IterationMinMax:
                PrintSequenceIterationMinMax((SequenceIterationMinMax)seq, parent, context);
                break;
            case SequenceType.Transaction:
                PrintSequenceTransaction((SequenceTransaction)seq, parent, context);
                break;
            case SequenceType.Backtrack:
                PrintSequenceBacktrack((SequenceBacktrack)seq, parent, context);
                break;
            case SequenceType.MultiBacktrack:
                PrintSequenceMultiBacktrack((SequenceMultiBacktrack)seq, parent, context);
                break;
            case SequenceType.MultiSequenceBacktrack:
                PrintSequenceMultiSequenceBacktrack((SequenceMultiSequenceBacktrack)seq, parent, context);
                break;
            case SequenceType.Pause:
                PrintSequencePause((SequencePause)seq, parent, context);
                break;
            case SequenceType.ForContainer:
                PrintSequenceForContainer((SequenceForContainer)seq, parent, context);
                break;
            case SequenceType.ForIntegerRange:
                PrintSequenceForIntegerRange((SequenceForIntegerRange)seq, parent, context);
                break;
            case SequenceType.ForIndexAccessEquality:
                PrintSequenceForIndexAccessEquality((SequenceForIndexAccessEquality)seq, parent, context);
                break;
            case SequenceType.ForIndexAccessOrdering:
                PrintSequenceForIndexAccessOrdering((SequenceForIndexAccessOrdering)seq, parent, context);
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
                PrintSequenceForFunction((SequenceForFunction)seq, parent, context);
                break;
            case SequenceType.ForMatch:
                PrintSequenceForMatch((SequenceForMatch)seq, parent, context);
                break;
            case SequenceType.ExecuteInSubgraph:
                PrintSequenceExecuteInSubgraph((SequenceExecuteInSubgraph)seq, parent, context);
                break;
            case SequenceType.IfThenElse:
                PrintSequenceIfThenElse((SequenceIfThenElse)seq, parent, context);
                break;
            case SequenceType.LazyOrAll:
            case SequenceType.LazyAndAll:
            case SequenceType.StrictOrAll:
            case SequenceType.StrictAndAll:
                PrintSequenceNAry((SequenceNAry)seq, parent, context);
                break;
            case SequenceType.WeightedOne:
                PrintSequenceWeightedOne((SequenceWeightedOne)seq, parent, context);
                break;
            case SequenceType.SomeFromSet:
                PrintSequenceSomeFromSet((SequenceSomeFromSet)seq, parent, context);
                break;
            case SequenceType.MultiRulePrefixedSequence:
                PrintSequenceMultiRulePrefixedSequence((SequenceMultiRulePrefixedSequence)seq, parent, context);
                break;
            case SequenceType.MultiRuleAllCall:
                PrintSequenceMultiRuleAllCall((SequenceMultiRuleAllCall)seq, parent, context);
                break;
            case SequenceType.RulePrefixedSequence:
                PrintSequenceRulePrefixedSequence((SequenceRulePrefixedSequence)seq, parent, context);
                break;
            case SequenceType.SequenceCall:
            case SequenceType.RuleCall:
            case SequenceType.RuleAllCall:
            case SequenceType.RuleCountAllCall:
            case SequenceType.BooleanComputation:
                PrintSequenceBreakpointable((Sequence)seq, parent, context);
                break;
            case SequenceType.AssignSequenceResultToVar:
            case SequenceType.OrAssignSequenceResultToVar:
            case SequenceType.AndAssignSequenceResultToVar:
                PrintSequenceAssignSequenceResultToVar((SequenceAssignSequenceResultToVar)seq, parent, context);
                break;
            case SequenceType.AssignUserInputToVar:
            case SequenceType.AssignRandomIntToVar:
            case SequenceType.AssignRandomDoubleToVar:
                PrintSequenceAssignChoiceHighlightable((Sequence)seq, parent, context);
                break;
            case SequenceType.SequenceDefinitionInterpreted:
                PrintSequenceDefinitionInterpreted((SequenceDefinitionInterpreted)seq, parent, context);
                break;
            // Atoms (assignments)
            case SequenceType.AssignVarToVar:
            case SequenceType.AssignConstToVar:
            case SequenceType.AssignContainerConstructorToVar:
            case SequenceType.DeclareVariable:
                Console.Write(seq.Symbol);
                break;
            default:
                Debug.Assert(false);
                Console.Write("<UNKNOWN_SEQUENCE_TYPE>");
                break;
            }

            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence)
                Console.Write(")");
        }

        private static void PrintSequenceBinary(SequenceBinary seqBin, SequenceBase parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0 && seqBin.Random)
            {
                int cpPosCounter = context.cpPosCounter;
                ++context.cpPosCounter;
                PrintSequence(seqBin.Left, seqBin, context);
                PrintChoice(seqBin, context);
                Console.Write(seqBin.OperatorSymbol + " ");
                PrintSequence(seqBin.Right, seqBin, context);
                return;
            }

            if(seqBin == context.highlightSeq && context.choice)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(l)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Left, seqBin, context);
                WorkaroundManager.Workaround.PrintHighlighted("(l) " + seqBin.OperatorSymbol + " (r)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Right, seqBin, context);
                WorkaroundManager.Workaround.PrintHighlighted("(r)", HighlightingMode.Choicepoint);
                return;
            }

            PrintSequence(seqBin.Left, seqBin, context);
            Console.Write(" " + seqBin.OperatorSymbol + " ");
            PrintSequence(seqBin.Right, seqBin, context);
        }

        private static void PrintSequenceIfThen(SequenceIfThen seqIfThen, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("if{");
            PrintSequence(seqIfThen.Left, seqIfThen, context);
            Console.Write(";");
            PrintSequence(seqIfThen.Right, seqIfThen, context);
            Console.Write("}");
        }

        private static void PrintSequenceNot(SequenceNot seqNot, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqNot.OperatorSymbol);
            PrintSequence(seqNot.Seq, seqNot, context);
        }

        private static void PrintSequenceIterationMin(SequenceIterationMin seqMin, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequence(seqMin.Seq, seqMin, context);
            Console.Write("[" + seqMin.Min + ":*]");
        }

        private static void PrintSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequence(seqMinMax.Seq, seqMinMax, context);
            Console.Write("[" + seqMinMax.Min + ":" + seqMinMax.Max + "]");
        }

        private static void PrintSequenceTransaction(SequenceTransaction seqTrans, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("<");
            PrintSequence(seqTrans.Seq, seqTrans, context);
            Console.Write(">");
        }

        private static void PrintSequenceBacktrack(SequenceBacktrack seqBack, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("<<");
            PrintSequence(seqBack.Rule, seqBack, context);
            Console.Write(";;");
            PrintSequence(seqBack.Seq, seqBack, context);
            Console.Write(">>");
        }

        private static void PrintSequenceMultiBacktrack(SequenceMultiBacktrack seqBack, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("<<");
            PrintSequence(seqBack.Rules, seqBack, context);
            Console.Write(";;");
            PrintSequence(seqBack.Seq, seqBack, context);
            Console.Write(">>");
        }

        private static void PrintSequenceMultiSequenceBacktrack(SequenceMultiSequenceBacktrack seqBack, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("<<");
            PrintSequence(seqBack.MultiRulePrefixedSequence, seqBack, context);
            Console.Write(">>");
        }

        private static void PrintSequencePause(SequencePause seqPause, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("/");
            PrintSequence(seqPause.Seq, seqPause, context);
            Console.Write("/");
        }

        private static void PrintSequenceForContainer(SequenceForContainer seqFor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            if(seqFor.VarDst != null)
                Console.Write("->" + seqFor.VarDst.Name);
            Console.Write(" in " + seqFor.Container.Name);
            Console.Write("; ");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceForIntegerRange(SequenceForIntegerRange seqFor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            Console.Write(" in [");
            Console.Write(seqFor.Left.Symbol);
            Console.Write(":");
            Console.Write(seqFor.Right.Symbol);
            Console.Write("]; ");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceForIndexAccessEquality(SequenceForIndexAccessEquality seqFor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            Console.Write(" in {");
            Console.Write(seqFor.IndexName);
            Console.Write("==");
            Console.Write(seqFor.Expr.Symbol);
            Console.Write("}; ");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering seqFor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            Console.Write(" in {");
            if(seqFor.Ascending)
                Console.Write("ascending");
            else
                Console.Write("descending");
            Console.Write("(");
            if(seqFor.From() != null && seqFor.To() != null)
            {
                Console.Write(seqFor.IndexName);
                Console.Write(seqFor.DirectionAsString(seqFor.Direction));
                Console.Write(seqFor.Expr.Symbol);
                Console.Write(",");
                Console.Write(seqFor.IndexName);
                Console.Write(seqFor.DirectionAsString(seqFor.Direction2));
                Console.Write(seqFor.Expr2.Symbol);
            }
            else if(seqFor.From() != null)
            {
                Console.Write(seqFor.IndexName);
                Console.Write(seqFor.DirectionAsString(seqFor.Direction));
                Console.Write(seqFor.Expr.Symbol);
            }
            else if(seqFor.To() != null)
            {
                Console.Write(seqFor.IndexName);
                Console.Write(seqFor.DirectionAsString(seqFor.Direction));
                Console.Write(seqFor.Expr.Symbol);
            }
            else
            {
                Console.Write(seqFor.IndexName);
            }
            Console.Write(")");
            Console.Write("}; ");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceForFunction(SequenceForFunction seqFor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            Console.Write(" in ");
            Console.Write(seqFor.FunctionSymbol + ";");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceForMatch(SequenceForMatch seqFor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            Console.Write(" in [?");
            PrintSequence(seqFor.Rule, seqFor, context);
            Console.Write("]; ");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceExecuteInSubgraph(SequenceExecuteInSubgraph seqExecInSub, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("in ");
            Console.Write(seqExecInSub.SubgraphVar.Name);
            if(seqExecInSub.AttributeName != null)
                Console.Write("." + seqExecInSub.AttributeName);
            Console.Write(" {");
            PrintSequence(seqExecInSub.Seq, seqExecInSub, context);
            Console.Write("}");
        }

        private static void PrintSequenceIfThenElse(SequenceIfThenElse seqIf, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("if{");
            PrintSequence(seqIf.Condition, seqIf, context);
            Console.Write(";");
            PrintSequence(seqIf.TrueCase, seqIf, context);
            Console.Write(";");
            PrintSequence(seqIf.FalseCase, seqIf, context);
            Console.Write("}");
        }

        private static void PrintSequenceNAry(SequenceNAry seqN, SequenceBase parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0)
            {
                PrintChoice(seqN, context);
                ++context.cpPosCounter;
                Console.Write((seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(");
                bool first = true;
                foreach(Sequence seqChild in seqN.Children)
                {
                    if(!first)
                        Console.Write(", ");
                    PrintSequence(seqChild, seqN, context);
                    first = false;
                }
                Console.Write(")");
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
                        Console.Write(", ");
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
                    PrintSequence(seqChild, seqN, context);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqChild == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                return;
            }

            Console.Write((seqN.Choice ? "$%" : "$") + seqN.OperatorSymbol + "(");
            PrintChildren(seqN, context);
            Console.Write(")");
        }

        private static void PrintSequenceWeightedOne(SequenceWeightedOne seqWeighted, SequenceBase parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0)
            {
                PrintChoice(seqWeighted, context);
                ++context.cpPosCounter;
                Console.Write((seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(");
                bool first = true;
                for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                {
                    if(first)
                        Console.Write("0.00 ");
                    else
                        Console.Write(" ");
                    PrintSequence(seqWeighted.Sequences[i], seqWeighted, context);
                    Console.Write(" ");
                    Console.Write(seqWeighted.Numbers[i]); // todo: format auf 2 nachkommastellen 
                    first = false;
                }
                Console.Write(")");
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
                        Console.Write("0.00 ");
                    else
                        Console.Write(" ");
                    if(seqWeighted.Sequences[i] == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);

                    SequenceBase highlightSeqBackup = context.highlightSeq;
                    context.highlightSeq = null; // we already highlighted here
                    PrintSequence(seqWeighted.Sequences[i], seqWeighted, context);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqWeighted.Sequences[i] == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    Console.Write(" ");
                    Console.Write(seqWeighted.Numbers[i]); // todo: format auf 2 nachkommastellen 
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                return;
            }

            Console.Write((seqWeighted.Choice ? "$%" : "$") + seqWeighted.OperatorSymbol + "(");
            bool ffs = true;
            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
            {
                if(ffs)
                    Console.Write("0.00 ");
                else
                    Console.Write(" ");
                PrintSequence(seqWeighted.Sequences[i], seqWeighted, context);
                Console.Write(" ");
                Console.Write(seqWeighted.Numbers[i]); // todo: format auf 2 nachkommastellen 
                ffs = false;
            }
            Console.Write(")");
        }

        private static void PrintSequenceSomeFromSet(SequenceSomeFromSet seqSome, SequenceBase parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0
                && seqSome.Random)
            {
                PrintChoice(seqSome, context);
                ++context.cpPosCounter;
                Console.Write(seqSome.Choice ? "$%{<" : "${<");
                bool first = true;
                foreach(Sequence seqChild in seqSome.Children)
                {
                    if(!first)
                        Console.Write(", ");
                    int cpPosCounterBackup = context.cpPosCounter;
                    context.cpPosCounter = -1; // rules within some-from-set are not choicepointable
                    PrintSequence(seqChild, seqSome, context);
                    context.cpPosCounter = cpPosCounterBackup;
                    first = false;
                }
                Console.Write(")}");
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
                        Console.Write(", ");
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
                    PrintSequence(seqChild, seqSome, context);
                    context.highlightSeq = highlightSeqBackup;

                    if(seqChild == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                    first = false;
                }
                WorkaroundManager.Workaround.PrintHighlighted(">}", HighlightingMode.Choicepoint);
                return;
            }

            bool succesBackup = context.success;
            if(highlight)
                context.success = true;
            Console.Write(seqSome.Random ? (seqSome.Choice ? "$%{<" : "${<") : "{<");
            PrintChildren(seqSome, context);
            Console.Write(">}");
            context.success = succesBackup;
        }

        private static void PrintSequenceMultiRulePrefixedSequence(SequenceMultiRulePrefixedSequence seqMulti, SequenceBase parent, PrintSequenceContext context)
        {
            bool highlight = false;
            foreach(Sequence seqChild in seqMulti.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            bool succesBackup = context.success;
            if(highlight)
                context.success = true;

            Console.Write("[[");
            PrintChildren(seqMulti, context);
            Console.Write("]");
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqMulti, context);
            }
            Console.Write("]");

            context.success = succesBackup;
        }

        private static void PrintSequenceMultiRuleAllCall(SequenceMultiRuleAllCall seqMulti, SequenceBase parent, PrintSequenceContext context)
        {
            bool highlight = false;
            foreach(Sequence seqChild in seqMulti.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            bool succesBackup = context.success;
            if(highlight)
                context.success = true;

            Console.Write("[[");
            PrintChildren(seqMulti, context);
            Console.Write("]");
            foreach(SequenceFilterCallBase filterCall in seqMulti.Filters)
            {
                PrintSequenceFilterCall(filterCall, seqMulti, context);
            }
            Console.Write("]");

            context.success = succesBackup;
        }

        private static void PrintSequenceRulePrefixedSequence(SequenceRulePrefixedSequence seqRulePrefixedSequence, SequenceBase parent, PrintSequenceContext context)
        {
            if(!(parent is SequenceMultiRulePrefixedSequence))
                Console.Write("[");

            Console.Write("for{");
            PrintSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, context);
            Console.Write(";");
            PrintSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, context);
            Console.Write("}");

            if(!(parent is SequenceMultiRulePrefixedSequence))
                Console.Write("]");
        }

        private static void PrintSequenceBreakpointable(Sequence seq, SequenceBase parent, PrintSequenceContext context)
        {
            if(context.bpPosCounter >= 0)
            {
                PrintBreak((SequenceSpecial)seq, context);
                Console.Write(seq.Symbol);
                ++context.bpPosCounter;
                return;
            }

            if(context.cpPosCounter >= 0 && seq is SequenceRandomChoice
                && ((SequenceRandomChoice)seq).Random)
            {
                PrintChoice((SequenceRandomChoice)seq, context);
                Console.Write(seq.Symbol);
                ++context.cpPosCounter;
                return;
            }

            HighlightingMode mode = HighlightingMode.None;
            if(seq == context.highlightSeq)
            {
                if(context.choice)
                    mode |= HighlightingMode.Choicepoint;
                else if(context.success)
                    mode |= HighlightingMode.FocusSucces;
                else
                    mode |= HighlightingMode.Focus;
            }
            if(seq.ExecutionState == SequenceExecutionState.Success)
                mode |= HighlightingMode.LastSuccess;
            if(seq.ExecutionState == SequenceExecutionState.Fail)
                mode |= HighlightingMode.LastFail;
            if(context.sequences != null && context.sequences.Contains(seq))
            {
                if(context.matches != null && context.matches[context.sequences.IndexOf(seq)].Count > 0)
                    mode |= HighlightingMode.FocusSucces;
            }

            if(seq.Contains(context.highlightSeq) && seq != context.highlightSeq)
                PrintSequenceAtom(seq, parent, context);
            else
                WorkaroundManager.Workaround.PrintHighlighted(seq.Symbol, mode);
        }

        private static void PrintSequenceAtom(Sequence seq, SequenceBase parent, PrintSequenceContext context)
        {
            switch(seq.SequenceType)
            {
            case SequenceType.SequenceCall:
                PrintSequenceSequenceCall((SequenceSequenceCallInterpreted)seq, parent, context);
                break;
            case SequenceType.RuleCall:
                PrintSequenceRuleCall((SequenceRuleCall)seq, parent, context);
                break;
            case SequenceType.RuleAllCall:
                PrintSequenceRuleAllCall((SequenceRuleAllCall)seq, parent, context);
                break;
            case SequenceType.RuleCountAllCall:
                PrintSequenceRuleCountAllCall((SequenceRuleCountAllCall)seq, parent, context);
                break;
            case SequenceType.BooleanComputation:
                PrintSequenceBooleanComputation((SequenceBooleanComputation)seq, parent, context);
                break;
            default:
                Debug.Assert(false);
                Console.Write("<UNKNOWN_SEQUENCE_TYPE>");
                break;
            }
        }

        private static void PrintSequenceSequenceCall(SequenceSequenceCallInterpreted seq, SequenceBase parent, PrintSequenceContext context)
        {
            if(seq.Special)
                Console.Write("%"); // TODO: questionable position here and in sequence -- should appear before sequence name, not return assignment
            PrintReturnAssignments(seq.ReturnVars, parent, context);
            if(seq.subgraph != null)
                Console.Write(seq.subgraph.Name + ".");
            Console.Write(seq.SequenceDef.Name);
            if(seq.ArgumentExpressions.Length > 0)
            {
                PrintArguments(seq.ArgumentExpressions, parent, context);
            }
        }

        private static void PrintArguments(SequenceExpression[] arguments, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("(");
            for(int i = 0; i < arguments.Length; ++i)
            {
                PrintSequenceExpression(arguments[i], parent, context);
                if(i != arguments.Length - 1)
                    Console.Write(",");
            }
            Console.Write(")");
        }

        private static void PrintSequenceRuleCall(SequenceRuleCall seq, SequenceBase parent, PrintSequenceContext context)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, context);
            Console.Write(seq.TestDebugPrefix);
            PrintRuleCallString(seq, parent, context);
        }

        private static void PrintSequenceRuleAllCall(SequenceRuleAllCall seq, SequenceBase parent, PrintSequenceContext context)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, context);
            Console.Write(seq.RandomChoicePrefix);
            Console.Write("[");
            Console.Write(seq.TestDebugPrefix);
            PrintRuleCallString(seq, parent, context);
            Console.Write("]");
        }

        private static void PrintSequenceRuleCountAllCall(SequenceRuleCountAllCall seq, SequenceBase parent, PrintSequenceContext context)
        {
            PrintReturnAssignments(seq.ReturnVars, parent, context);
            Console.Write("count[");
            Console.Write(seq.TestDebugPrefix);
            PrintRuleCallString(seq, parent, context);
            Console.Write("]" + "=>" + seq.CountResult.Name);
        }

        private static void PrintReturnAssignments(SequenceVariable[] returnVars, SequenceBase parent, PrintSequenceContext context)
        {
            if(returnVars.Length > 0)
            {
                Console.Write("(");
                for(int i = 0; i < returnVars.Length; ++i)
                {
                    Console.Write(returnVars[i].Name);
                    if(i != returnVars.Length - 1)
                        Console.Write(",");
                }
                Console.Write(")=");
            }
        }

        private static void PrintRuleCallString(SequenceRuleCall seq, SequenceBase parent, PrintSequenceContext context)
        {
            if(seq.subgraph != null)
                Console.Write(seq.subgraph.Name + ".");
            Console.Write(seq.Name);
            if(seq.ArgumentExpressions.Length > 0)
            {
                PrintArguments(seq.ArgumentExpressions, parent, context);
            }
            for(int i = 0; i < seq.Filters.Count; ++i)
            {
                Console.Write("\\");
                PrintSequenceFilterCall(seq.Filters[i], seq, context);
            }
        }

        private static void PrintSequenceFilterCall(SequenceFilterCallBase seq, SequenceBase parent, PrintSequenceContext context)
        {
            if(seq is SequenceFilterCallInterpreted)
            {
                SequenceFilterCallInterpreted filterCall = (SequenceFilterCallInterpreted)seq;
                if(filterCall.MatchClass != null)
                    Console.Write(filterCall.MatchClass.info.PackagePrefixedName + ".");
                Console.Write(filterCall.PackagePrefixedName);
                PrintArguments(filterCall.ArgumentExpressions, parent, context);
            }
            else if(seq is SequenceFilterCallLambdaExpressionInterpreted)
            {
                SequenceFilterCallLambdaExpressionInterpreted filterCall = (SequenceFilterCallLambdaExpressionInterpreted)seq;
                if(filterCall.MatchClass != null)
                    Console.Write(filterCall.MatchClass.info.PackagePrefixedName + ".");
                Console.Write(filterCall.Name);
                //if(filterCall.Entity != null)
                //    sb.Append("<" + filterCall.Entity + ">");
                if(filterCall.FilterCall.initExpression != null)
                {
                    Console.Write("{");
                    if(filterCall.FilterCall.initArrayAccess != null)
                        Console.Write(filterCall.FilterCall.initArrayAccess.Name + "; ");
                    PrintSequenceExpression(filterCall.FilterCall.initExpression, parent, context);
                    Console.Write("}");
                }
                Console.Write("{");
                if(filterCall.FilterCall.arrayAccess != null)
                    Console.Write(filterCall.FilterCall.arrayAccess.Name + "; ");
                if(filterCall.FilterCall.previousAccumulationAccess != null)
                    Console.Write(filterCall.FilterCall.previousAccumulationAccess + ", ");
                if(filterCall.FilterCall.index != null)
                    Console.Write(filterCall.FilterCall.index.Name + " -> ");
                Console.Write(filterCall.FilterCall.element.Name + " -> ");
                PrintSequenceExpression(filterCall.FilterCall.lambdaExpression, parent, context);
                Console.Write("}");
            }
            else
            {
                Debug.Assert(false);
            }
        }

        private static void PrintSequenceBooleanComputation(SequenceBooleanComputation seqComp, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceComputation(seqComp.Computation, seqComp, context);
        }

        private static void PrintSequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar seqAss, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("(");
            PrintSequence(seqAss.Seq, seqAss, context);
            Console.Write(seqAss.OperatorSymbol);
            Console.Write(seqAss.DestVar.Name);
            Console.Write(")");
        }

        // Choice highlightable user assignments
        private static void PrintSequenceAssignChoiceHighlightable(Sequence seq, SequenceBase parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0
                && (seq is SequenceAssignRandomIntToVar || seq is SequenceAssignRandomDoubleToVar))
            {
                PrintChoice((SequenceRandomChoice)seq, context);
                Console.Write(seq.Symbol);
                ++context.cpPosCounter;
                return;
            }

            if(seq == context.highlightSeq && context.choice)
                WorkaroundManager.Workaround.PrintHighlighted(seq.Symbol, HighlightingMode.Choicepoint);
            else
                Console.Write(seq.Symbol);
        }

        private static void PrintSequenceDefinitionInterpreted(SequenceDefinitionInterpreted seqDef, SequenceBase parent, PrintSequenceContext context)
        {
            HighlightingMode mode = HighlightingMode.None;
            if(seqDef.ExecutionState == SequenceExecutionState.Success)
                mode = HighlightingMode.LastSuccess;
            if(seqDef.ExecutionState == SequenceExecutionState.Fail)
                mode = HighlightingMode.LastFail;
            WorkaroundManager.Workaround.PrintHighlighted(seqDef.Symbol + ": ", mode);
            PrintSequence(seqDef.Seq, seqDef.Seq, context);
        }

        private static void PrintChildren(Sequence seq, PrintSequenceContext context)
        {
            bool first = true;
            foreach(Sequence seqChild in seq.Children)
            {
                if(first)
                    first = false;
                else
                    Console.Write(", ");
                PrintSequence(seqChild, seq, context);
            }
        }

        private static void PrintChoice(SequenceRandomChoice seq, PrintSequenceContext context)
        {
            if(seq.Choice)
                WorkaroundManager.Workaround.PrintHighlighted("-%" + context.cpPosCounter + "-:", HighlightingMode.Choicepoint);
            else
                WorkaroundManager.Workaround.PrintHighlighted("+%" + context.cpPosCounter + "+:", HighlightingMode.Choicepoint);
        }

        private static void PrintBreak(SequenceSpecial seq, PrintSequenceContext context)
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

        private static void PrintSequenceComputation(SequenceComputation seqComp, SequenceBase parent, PrintSequenceContext context)
        {
            switch(seqComp.SequenceComputationType)
            {
            case SequenceComputationType.Then:
                PrintSequenceComputationThen((SequenceComputationThen)seqComp, parent, context);
                break;
            case SequenceComputationType.VAlloc:
                PrintSequenceComputationVAlloc((SequenceComputationVAlloc)seqComp, parent, context);
                break;
            case SequenceComputationType.VFree:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, context);
                break;
            case SequenceComputationType.VFreeNonReset:
            case SequenceComputationType.VReset:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, context);
                break;
            case SequenceComputationType.ContainerAdd:
                PrintSequenceComputationContainerAdd((SequenceComputationContainerAdd)seqComp, parent, context);
                break;
            case SequenceComputationType.ContainerRem:
                PrintSequenceComputationContainerRem((SequenceComputationContainerRem)seqComp, parent, context);
                break;
            case SequenceComputationType.ContainerClear:
                PrintSequenceComputationContainerClear((SequenceComputationContainerClear)seqComp, parent, context);
                break;
            case SequenceComputationType.Assignment:
                PrintSequenceComputationAssignment((SequenceComputationAssignment)seqComp, parent, context);
                break;
            case SequenceComputationType.VariableDeclaration:
                PrintSequenceComputationVariableDeclaration((SequenceComputationVariableDeclaration)seqComp, parent, context);
                break;
            case SequenceComputationType.Emit:
                PrintSequenceComputationEmit((SequenceComputationEmit)seqComp, parent, context);
                break;
            case SequenceComputationType.Record:
                PrintSequenceComputationRecord((SequenceComputationRecord)seqComp, parent, context);
                break;
            case SequenceComputationType.Export:
                PrintSequenceComputationExport((SequenceComputationExport)seqComp, parent, context);
                break;
            case SequenceComputationType.DeleteFile:
                PrintSequenceComputationDeleteFile((SequenceComputationDeleteFile)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphAdd:
                PrintSequenceComputationGraphAdd((SequenceComputationGraphAdd)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphRem:
                PrintSequenceComputationGraphRem((SequenceComputationGraphRem)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphClear:
                PrintSequenceComputationGraphClear((SequenceComputationGraphClear)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphRetype:
                PrintSequenceComputationGraphRetype((SequenceComputationGraphRetype)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphAddCopy:
                PrintSequenceComputationGraphAddCopy((SequenceComputationGraphAddCopy)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphMerge:
                PrintSequenceComputationGraphMerge((SequenceComputationGraphMerge)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphRedirectSource:
                PrintSequenceComputationGraphRedirectSource((SequenceComputationGraphRedirectSource)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphRedirectTarget:
                PrintSequenceComputationGraphRedirectTarget((SequenceComputationGraphRedirectTarget)seqComp, parent, context);
                break;
            case SequenceComputationType.GraphRedirectSourceAndTarget:
                PrintSequenceComputationGraphRedirectSourceAndTarget((SequenceComputationGraphRedirectSourceAndTarget)seqComp, parent, context);
                break;
            case SequenceComputationType.Insert:
                PrintSequenceComputationInsert((SequenceComputationInsert)seqComp, parent, context);
                break;
            case SequenceComputationType.InsertCopy:
                PrintSequenceComputationInsertCopy((SequenceComputationInsertCopy)seqComp, parent, context);
                break;
            case SequenceComputationType.InsertInduced:
                PrintSequenceComputationInsertInduced((SequenceComputationInsertInduced)seqComp, parent, context);
                break;
            case SequenceComputationType.InsertDefined:
                PrintSequenceComputationInsertDefined((SequenceComputationInsertDefined)seqComp, parent, context);
                break;
            case SequenceComputationType.ProcedureCall:
                PrintSequenceComputationProcedureCall((SequenceComputationProcedureCall)seqComp, parent, context);
                break;
            case SequenceComputationType.BuiltinProcedureCall:
                PrintSequenceComputationBuiltinProcedureCall((SequenceComputationBuiltinProcedureCall)seqComp, parent, context);
                break;
            case SequenceComputationType.ProcedureMethodCall:
                PrintSequenceComputationProcedureMethodCall((SequenceComputationProcedureMethodCall)seqComp, parent, context);
                break;
            case SequenceComputationType.DebugAdd:
            case SequenceComputationType.DebugRem:
            case SequenceComputationType.DebugEmit:
            case SequenceComputationType.DebugHalt:
            case SequenceComputationType.DebugHighlight:
                PrintSequenceComputationDebug((SequenceComputationDebug)seqComp, parent, context);
                break;
            case SequenceComputationType.AssignmentTarget: // every assignment target (lhs value) is a computation
                PrintSequenceAssignmentTarget((AssignmentTarget)seqComp, parent, context);
                break;
            case SequenceComputationType.Expression: // every expression (rhs value) is a computation
                PrintSequenceExpression((SequenceExpression)seqComp, parent, context);
                break;
            default:
                Debug.Assert(false);
                break;
            }
        }

        private static void PrintSequenceComputationThen(SequenceComputationThen seqCompThen, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceComputation(seqCompThen.left, seqCompThen, context);
            Console.Write("; ");
            if(seqCompThen.right is SequenceExpression)
            {
                Console.Write("{");
                PrintSequenceExpression((SequenceExpression)seqCompThen.right, seqCompThen, context);
                Console.Write("}");
            }
            else
            {
                PrintSequenceComputation(seqCompThen.right, seqCompThen, context);
            }
        }

        private static void PrintSequenceComputationVAlloc(SequenceComputationVAlloc seqCompVAlloc, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("valloc()");
        }

        private static void PrintSequenceComputationVFree(SequenceComputationVFree seqCompVFree, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write((seqCompVFree.Reset ? "vfree" : "vfreenonreset") + "(");
            PrintSequenceExpression(seqCompVFree.VisitedFlagExpression, seqCompVFree, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationVReset(SequenceComputationVReset seqCompVReset, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("vreset(");
            PrintSequenceExpression(seqCompVReset.VisitedFlagExpression, seqCompVReset, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationContainerAdd(SequenceComputationContainerAdd seqCompContainerAdd, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqCompContainerAdd.Name + ".add(");
            PrintSequenceExpression(seqCompContainerAdd.Expr, seqCompContainerAdd, context);
            if(seqCompContainerAdd.ExprDst != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqCompContainerAdd.ExprDst, seqCompContainerAdd, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceComputationContainerRem(SequenceComputationContainerRem seqCompContainerRem, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqCompContainerRem.Name + ".rem(");
            if(seqCompContainerRem.Expr != null)
            {
                PrintSequenceExpression(seqCompContainerRem.Expr, seqCompContainerRem, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceComputationContainerClear(SequenceComputationContainerClear seqCompContainerClear, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqCompContainerClear.Name + ".clear()");
        }

        private static void PrintSequenceComputationAssignment(SequenceComputationAssignment seqCompAssign, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceAssignmentTarget(seqCompAssign.Target, seqCompAssign, context);
            Console.Write("=");
            PrintSequenceComputation(seqCompAssign.SourceValueProvider, seqCompAssign, context);
        }

        private static void PrintSequenceComputationVariableDeclaration(SequenceComputationVariableDeclaration seqCompVarDecl, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqCompVarDecl.Target.Name);
        }

        private static void PrintSequenceComputationDebug(SequenceComputationDebug seqCompDebug, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Debug::" + seqCompDebug.Name + "(");
            bool first = true;
            foreach(SequenceExpression seqExpr in seqCompDebug.ArgExprs)
            {
                if(!first)
                    Console.Write(", ");
                else
                    first = false;
                PrintSequenceExpression(seqExpr, seqCompDebug, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceComputationEmit(SequenceComputationEmit seqCompEmit, SequenceBase parent, PrintSequenceContext context)
        {
            if(seqCompEmit.IsDebug)
                Console.Write("emitdebug(");
            else
                Console.Write("emit(");
            bool first = true;
            foreach(SequenceExpression expr in seqCompEmit.Expressions)
            {
                if(first)
                    first = false;
                else
                    Console.Write(", ");
                SequenceExpressionConstant exprConst = expr as SequenceExpressionConstant;
                if(exprConst != null && exprConst.Constant is string)
                    Console.Write(SequenceExpressionConstant.ConstantAsString(exprConst.Constant));
                else
                    PrintSequenceExpression(expr, seqCompEmit, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceComputationRecord(SequenceComputationRecord seqCompRec, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("record(");

            SequenceExpressionConstant exprConst = seqCompRec.Expression as SequenceExpressionConstant;
            if(exprConst != null && exprConst.Constant is string)
                Console.Write(SequenceExpressionConstant.ConstantAsString(exprConst.Constant));
            else
                PrintSequenceExpression(seqCompRec.Expression, seqCompRec, context);

            Console.Write(")");
        }

        private static void PrintSequenceComputationExport(SequenceComputationExport seqCompExport, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("File::export(");
            if(seqCompExport.Graph != null)
            {
                PrintSequenceExpression(seqCompExport.Graph, seqCompExport, context);
                Console.Write(", ");
            }
            PrintSequenceExpression(seqCompExport.Name, seqCompExport, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationDeleteFile(SequenceComputationDeleteFile seqCompDelFile, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("File::deleteFile(");
            PrintSequenceExpression(seqCompDelFile.Name, seqCompDelFile, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphAdd(SequenceComputationGraphAdd seqCompGraphAdd, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("add(");
            PrintSequenceExpression(seqCompGraphAdd.Expr, seqCompGraphAdd, context);
            if(seqCompGraphAdd.ExprSrc != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqCompGraphAdd.ExprSrc, seqCompGraphAdd, context);
                Console.Write(",");
                PrintSequenceExpression(seqCompGraphAdd.ExprDst, seqCompGraphAdd, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphRem(SequenceComputationGraphRem seqCompGraphRem, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("rem(");
            PrintSequenceExpression(seqCompGraphRem.Expr, seqCompGraphRem, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphClear(SequenceComputationGraphClear seqCompGraphClear, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("clear()");
        }

        private static void PrintSequenceComputationGraphRetype(SequenceComputationGraphRetype seqCompGraphRetype, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("retype(");
            PrintSequenceExpression(seqCompGraphRetype.ElemExpr, seqCompGraphRetype, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompGraphRetype.TypeExpr, seqCompGraphRetype, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy seqCompGraphAddCopy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqCompGraphAddCopy.Deep ? "addCopy(" : "addClone(");
            PrintSequenceExpression(seqCompGraphAddCopy.Expr, seqCompGraphAddCopy, context);
            if(seqCompGraphAddCopy.ExprSrc != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqCompGraphAddCopy.ExprSrc, seqCompGraphAddCopy, context);
                Console.Write(",");
                PrintSequenceExpression(seqCompGraphAddCopy.ExprDst, seqCompGraphAddCopy, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphMerge(SequenceComputationGraphMerge seqCompGraphMerge, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("merge(");
            PrintSequenceExpression(seqCompGraphMerge.TargetNodeExpr, seqCompGraphMerge, context);
            Console.Write(", ");
            PrintSequenceExpression(seqCompGraphMerge.SourceNodeExpr, seqCompGraphMerge, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphRedirectSource(SequenceComputationGraphRedirectSource seqCompGraphRedirectSrc, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("redirectSource(");
            PrintSequenceExpression(seqCompGraphRedirectSrc.EdgeExpr, seqCompGraphRedirectSrc, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompGraphRedirectSrc.SourceNodeExpr, seqCompGraphRedirectSrc, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphRedirectTarget(SequenceComputationGraphRedirectTarget seqCompGraphRedirectTgt, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("redirectSourceAndTarget(");
            PrintSequenceExpression(seqCompGraphRedirectTgt.EdgeExpr, seqCompGraphRedirectTgt, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompGraphRedirectTgt.TargetNodeExpr, seqCompGraphRedirectTgt, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationGraphRedirectSourceAndTarget(SequenceComputationGraphRedirectSourceAndTarget seqCompGraphRedirectSrcTgt, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("redirectSourceAndTarget(");
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.EdgeExpr, seqCompGraphRedirectSrcTgt, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.SourceNodeExpr, seqCompGraphRedirectSrcTgt, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompGraphRedirectSrcTgt.TargetNodeExpr, seqCompGraphRedirectSrcTgt, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationInsert(SequenceComputationInsert seqCompInsert, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("insert(");
            PrintSequenceExpression(seqCompInsert.Graph, seqCompInsert, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationInsertCopy(SequenceComputationInsertCopy seqCompInsertCopy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("insert(");
            PrintSequenceExpression(seqCompInsertCopy.Graph, seqCompInsertCopy, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompInsertCopy.RootNode, seqCompInsertCopy, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationInsertInduced(SequenceComputationInsertInduced seqCompInsertInduced, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("insertInduced(");
            PrintSequenceExpression(seqCompInsertInduced.NodeSet, seqCompInsertInduced, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompInsertInduced.RootNode, seqCompInsertInduced, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationInsertDefined(SequenceComputationInsertDefined seqCompInsertDefined, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("insertDefined(");
            PrintSequenceExpression(seqCompInsertDefined.EdgeSet, seqCompInsertDefined, context);
            Console.Write(",");
            PrintSequenceExpression(seqCompInsertDefined.RootEdge, seqCompInsertDefined, context);
            Console.Write(")");
        }

        private static void PrintSequenceComputationBuiltinProcedureCall(SequenceComputationBuiltinProcedureCall seqCompBuiltinProcCall, SequenceBase parent, PrintSequenceContext context)
        {
            if(seqCompBuiltinProcCall.ReturnVars.Count > 0)
            {
                Console.Write("(");
                for(int i = 0; i < seqCompBuiltinProcCall.ReturnVars.Count; ++i)
                {
                    Console.Write(seqCompBuiltinProcCall.ReturnVars[i].Name);
                    if(i != seqCompBuiltinProcCall.ReturnVars.Count - 1)
                        Console.Write(",");
                }
                Console.Write(")=");
            }
            PrintSequenceComputation(seqCompBuiltinProcCall.BuiltinProcedure, seqCompBuiltinProcCall, context);
        }

        private static void PrintSequenceComputationProcedureCall(SequenceComputationProcedureCall seqCompProcCall, SequenceBase parent, PrintSequenceContext context)
        {
            if(seqCompProcCall.ReturnVars.Length > 0)
            {
                Console.Write("(");
                for(int i = 0; i < seqCompProcCall.ReturnVars.Length; ++i)
                {
                    Console.Write(seqCompProcCall.ReturnVars[i].Name);
                    if(i != seqCompProcCall.ReturnVars.Length - 1)
                        Console.Write(",");
                }
                Console.Write(")=");
            }
            Console.Write(seqCompProcCall.Name);
            if(seqCompProcCall.ArgumentExpressions.Length > 0)
            {
                Console.Write("(");
                for(int i = 0; i < seqCompProcCall.ArgumentExpressions.Length; ++i)
                {
                    Console.Write(seqCompProcCall.ArgumentExpressions[i].Symbol);
                    if(i != seqCompProcCall.ArgumentExpressions.Length - 1)
                        Console.Write(",");
                }
                Console.Write(")");
            }
        }

        private static void PrintSequenceComputationProcedureMethodCall(SequenceComputationProcedureMethodCall sequenceComputationProcedureMethodCall, SequenceBase parent, PrintSequenceContext context)
        {
            if(sequenceComputationProcedureMethodCall.ReturnVars.Length > 0)
            {
                Console.Write("(");
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ReturnVars.Length; ++i)
                {
                    Console.Write(sequenceComputationProcedureMethodCall.ReturnVars[i].Name);
                    if(i != sequenceComputationProcedureMethodCall.ReturnVars.Length - 1)
                        Console.Write(",");
                }
                Console.Write(")=");
            }
            if(sequenceComputationProcedureMethodCall.TargetExpr != null)
                Console.Write(sequenceComputationProcedureMethodCall.TargetExpr.Symbol + ".");
            if(sequenceComputationProcedureMethodCall.TargetVar != null)
                Console.Write(sequenceComputationProcedureMethodCall.TargetVar.ToString() + ".");
            Console.Write(sequenceComputationProcedureMethodCall.Name);
            if(sequenceComputationProcedureMethodCall.ArgumentExpressions.Length > 0)
            {
                Console.Write("(");
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ArgumentExpressions.Length; ++i)
                {
                    Console.Write(sequenceComputationProcedureMethodCall.ArgumentExpressions[i].Symbol);
                    if(i != sequenceComputationProcedureMethodCall.ArgumentExpressions.Length - 1)
                        Console.Write(",");
                }
                Console.Write(")");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void PrintSequenceAssignmentTarget(AssignmentTarget assTgt, SequenceBase parent, PrintSequenceContext context)
        {
            switch(assTgt.AssignmentTargetType)
            {
            case AssignmentTargetType.Var:
                PrintSequenceAssignmentTargetVar((AssignmentTargetVar)assTgt, parent, context);
                break;
            case AssignmentTargetType.YieldingToVar:
                PrintSequenceAssignmentTargetYieldingVar((AssignmentTargetYieldingVar)assTgt, parent, context);
                break;
            case AssignmentTargetType.IndexedVar:
                PrintSequenceAssignmentTargetIndexedVar((AssignmentTargetIndexedVar)assTgt, parent, context);
                break;
            case AssignmentTargetType.Attribute:
                PrintSequenceAssignmentTargetAttribute((AssignmentTargetAttribute)assTgt, parent, context);
                break;
            case AssignmentTargetType.AttributeIndexed:
                PrintSequenceAssignmentTargetAttributeIndexed((AssignmentTargetAttributeIndexed)assTgt, parent, context);
                break;
            case AssignmentTargetType.Visited:
                PrintSequenceAssignmentTargetVisited((AssignmentTargetVisited)assTgt, parent, context);
                break;
            }
        }

        private static void PrintSequenceAssignmentTargetVar(AssignmentTargetVar assTgtVar, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(assTgtVar.DestVar);
        }

        private static void PrintSequenceAssignmentTargetYieldingVar(AssignmentTargetYieldingVar assTgtYieldingVar, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(assTgtYieldingVar.DestVar);
        }

        private static void PrintSequenceAssignmentTargetIndexedVar(AssignmentTargetIndexedVar assTgtIndexedVar, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(assTgtIndexedVar.DestVar.Name + "[");
            PrintSequenceExpression(assTgtIndexedVar.KeyExpression, assTgtIndexedVar, context);
            Console.Write("]");
        }

        private static void PrintSequenceAssignmentTargetAttribute(AssignmentTargetAttribute assTgtAttribute, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(assTgtAttribute.DestVar.Name + "." + assTgtAttribute.AttributeName);
        }

        private static void PrintSequenceAssignmentTargetAttributeIndexed(AssignmentTargetAttributeIndexed assTgtAttributeIndexed, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(assTgtAttributeIndexed.DestVar.Name + "." + assTgtAttributeIndexed.AttributeName + "[");
            PrintSequenceExpression(assTgtAttributeIndexed.KeyExpression, assTgtAttributeIndexed, context);
            Console.Write("]");
        }

        private static void PrintSequenceAssignmentTargetVisited(AssignmentTargetVisited assTgtVisited, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(assTgtVisited.GraphElementVar.Name + ".visited");
            if(assTgtVisited.VisitedFlagExpression != null)
            {
                Console.Write("[");
                PrintSequenceExpression(assTgtVisited.VisitedFlagExpression, assTgtVisited, context);
                Console.Write("]");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void PrintSequenceExpression(SequenceExpression seqExpr, SequenceBase parent, PrintSequenceContext context)
        {
            switch(seqExpr.SequenceExpressionType)
            {
            case SequenceExpressionType.Conditional:
                PrintSequenceExpressionConditional((SequenceExpressionConditional)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Except:
            case SequenceExpressionType.LazyOr:
            case SequenceExpressionType.LazyAnd:
            case SequenceExpressionType.StrictOr:
            case SequenceExpressionType.StrictXor:
            case SequenceExpressionType.StrictAnd:
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Not:
                PrintSequenceExpressionNot((SequenceExpressionNot)seqExpr, parent, context);
                break;
            case SequenceExpressionType.UnaryPlus:
                PrintSequenceExpressionUnaryPlus((SequenceExpressionUnaryPlus)seqExpr, parent, context);
                break;
            case SequenceExpressionType.UnaryMinus:
                PrintSequenceExpressionUnaryMinus((SequenceExpressionUnaryMinus)seqExpr, parent, context);
                break;
            case SequenceExpressionType.BitwiseComplement:
                PrintSequenceExpressionBitwiseComplement((SequenceExpressionBitwiseComplement)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Cast:
                PrintSequenceExpressionCast((SequenceExpressionCast)seqExpr, parent, context);
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
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Constant:
                PrintSequenceExpressionConstant((SequenceExpressionConstant)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Variable:
                PrintSequenceExpressionVariable((SequenceExpressionVariable)seqExpr, parent, context);
                break;
            case SequenceExpressionType.This:
                PrintSequenceExpressionThis((SequenceExpressionThis)seqExpr, parent, context);
                break;
            case SequenceExpressionType.New:
                PrintSequenceExpressionNew((SequenceExpressionNew)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MatchClassConstructor:
                PrintSequenceExpressionMatchClassConstructor((SequenceExpressionMatchClassConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.SetConstructor:
                PrintSequenceExpressionSetConstructor((SequenceExpressionSetConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MapConstructor:
                PrintSequenceExpressionMapConstructor((SequenceExpressionMapConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayConstructor:
                PrintSequenceExpressionArrayConstructor((SequenceExpressionArrayConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.DequeConstructor:
                PrintSequenceExpressionDequeConstructor((SequenceExpressionDequeConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.SetCopyConstructor:
                PrintSequenceExpressionSetCopyConstructor((SequenceExpressionSetCopyConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MapCopyConstructor:
                PrintSequenceExpressionMapCopyConstructor((SequenceExpressionMapCopyConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayCopyConstructor:
                PrintSequenceExpressionArrayCopyConstructor((SequenceExpressionArrayCopyConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.DequeCopyConstructor:
                PrintSequenceExpressionDequeCopyConstructor((SequenceExpressionDequeCopyConstructor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ContainerAsArray:
                PrintSequenceExpressionContainerAsArray((SequenceExpressionContainerAsArray)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringLength:
                PrintSequenceExpressionStringLength((SequenceExpressionStringLength)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringStartsWith:
                PrintSequenceExpressionStringStartsWith((SequenceExpressionStringStartsWith)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringEndsWith:
                PrintSequenceExpressionStringEndsWith((SequenceExpressionStringEndsWith)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringSubstring:
                PrintSequenceExpressionStringSubstring((SequenceExpressionStringSubstring)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringReplace:
                PrintSequenceExpressionStringReplace((SequenceExpressionStringReplace)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringToLower:
                PrintSequenceExpressionStringToLower((SequenceExpressionStringToLower)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringToUpper:
                PrintSequenceExpressionStringToUpper((SequenceExpressionStringToUpper)seqExpr, parent, context);
                break;
            case SequenceExpressionType.StringAsArray:
                PrintSequenceExpressionStringAsArray((SequenceExpressionStringAsArray)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MapDomain:
                PrintSequenceExpressionMapDomain((SequenceExpressionMapDomain)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MapRange:
                PrintSequenceExpressionMapRange((SequenceExpressionMapRange)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Random:
                PrintSequenceExpressionRandom((SequenceExpressionRandom)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Def:
                PrintSequenceExpressionDef((SequenceExpressionDef)seqExpr, parent, context);
                break;
            case SequenceExpressionType.IsVisited:
                PrintSequenceExpressionIsVisited((SequenceExpressionIsVisited)seqExpr, parent, context);
                break;
            case SequenceExpressionType.InContainerOrString:
                PrintSequenceExpressionInContainerOrString((SequenceExpressionInContainerOrString)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ContainerEmpty:
                PrintSequenceExpressionContainerEmpty((SequenceExpressionContainerEmpty)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ContainerSize:
                PrintSequenceExpressionContainerSize((SequenceExpressionContainerSize)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ContainerAccess:
                PrintSequenceExpressionContainerAccess((SequenceExpressionContainerAccess)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ContainerPeek:
                PrintSequenceExpressionContainerPeek((SequenceExpressionContainerPeek)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringIndexOf((SequenceExpressionArrayOrDequeOrStringIndexOf)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringLastIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf((SequenceExpressionArrayOrDequeOrStringLastIndexOf)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayIndexOfOrdered:
                PrintSequenceExpressionArrayIndexOfOrdered((SequenceExpressionArrayIndexOfOrdered)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArraySum:
                PrintSequenceExpressionArraySum((SequenceExpressionArraySum)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayProd:
                PrintSequenceExpressionArrayProd((SequenceExpressionArrayProd)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayMin:
                PrintSequenceExpressionArrayMin((SequenceExpressionArrayMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayMax:
                PrintSequenceExpressionArrayMax((SequenceExpressionArrayMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayAvg:
                PrintSequenceExpressionArrayAvg((SequenceExpressionArrayAvg)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayMed:
                PrintSequenceExpressionArrayMed((SequenceExpressionArrayMed)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayMedUnordered:
                PrintSequenceExpressionArrayMedUnordered((SequenceExpressionArrayMedUnordered)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayVar:
                PrintSequenceExpressionArrayVar((SequenceExpressionArrayVar)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayDev:
                PrintSequenceExpressionArrayDev((SequenceExpressionArrayDev)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayAnd:
                PrintSequenceExpressionArrayAnd((SequenceExpressionArrayAnd)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOr:
                PrintSequenceExpressionArrayOr((SequenceExpressionArrayOr)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOrDequeAsSet:
                PrintSequenceExpressionArrayOrDequeAsSet((SequenceExpressionArrayOrDequeAsSet)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayAsMap:
                PrintSequenceExpressionArrayAsMap((SequenceExpressionArrayAsMap)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayAsDeque:
                PrintSequenceExpressionArrayAsDeque((SequenceExpressionArrayAsDeque)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayAsString:
                PrintSequenceExpressionArrayAsString((SequenceExpressionArrayAsString)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArraySubarray:
                PrintSequenceExpressionArraySubarray((SequenceExpressionArraySubarray)seqExpr, parent, context);
                break;
            case SequenceExpressionType.DequeSubdeque:
                PrintSequenceExpressionDequeSubdeque((SequenceExpressionDequeSubdeque)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOrderAscending:
                PrintSequenceExpressionArrayOrderAscending((SequenceExpressionArrayOrderAscending)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOrderDescending:
                PrintSequenceExpressionArrayOrderDescending((SequenceExpressionArrayOrderDescending)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayGroup:
                PrintSequenceExpressionArrayGroup((SequenceExpressionArrayGroup)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayKeepOneForEach:
                PrintSequenceExpressionArrayKeepOneForEach((SequenceExpressionArrayKeepOneForEach)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayReverse:
                PrintSequenceExpressionArrayReverse((SequenceExpressionArrayReverse)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayShuffle:
                PrintSequenceExpressionArrayShuffle((SequenceExpressionArrayShuffle)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayExtract:
                PrintSequenceExpressionArrayExtract((SequenceExpressionArrayExtract)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOrderAscendingBy:
                PrintSequenceExpressionArrayOrderAscendingBy((SequenceExpressionArrayOrderAscendingBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayOrderDescendingBy:
                PrintSequenceExpressionArrayOrderDescendingBy((SequenceExpressionArrayOrderDescendingBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayGroupBy:
                PrintSequenceExpressionArrayGroupBy((SequenceExpressionArrayGroupBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayKeepOneForEachBy:
                PrintSequenceExpressionArrayKeepOneForEachBy((SequenceExpressionArrayKeepOneForEachBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayIndexOfBy:
                PrintSequenceExpressionArrayIndexOfBy((SequenceExpressionArrayIndexOfBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayLastIndexOfBy:
                PrintSequenceExpressionArrayLastIndexOfBy((SequenceExpressionArrayLastIndexOfBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayIndexOfOrderedBy:
                PrintSequenceExpressionArrayIndexOfOrderedBy((SequenceExpressionArrayIndexOfOrderedBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayMap:
                PrintSequenceExpressionArrayMap((SequenceExpressionArrayMap)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayRemoveIf:
                PrintSequenceExpressionArrayRemoveIf((SequenceExpressionArrayRemoveIf)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ArrayMapStartWithAccumulateBy:
                PrintSequenceExpressionArrayMapStartWithAccumulateBy((SequenceExpressionArrayMapStartWithAccumulateBy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ElementFromGraph:
                PrintSequenceExpressionElementFromGraph((SequenceExpressionElementFromGraph)seqExpr, parent, context);
                break;
            case SequenceExpressionType.NodeByName:
                PrintSequenceExpressionNodeByName((SequenceExpressionNodeByName)seqExpr, parent, context);
                break;
            case SequenceExpressionType.EdgeByName:
                PrintSequenceExpressionEdgeByName((SequenceExpressionEdgeByName)seqExpr, parent, context);
                break;
            case SequenceExpressionType.NodeByUnique:
                PrintSequenceExpressionNodeByUnique((SequenceExpressionNodeByUnique)seqExpr, parent, context);
                break;
            case SequenceExpressionType.EdgeByUnique:
                PrintSequenceExpressionEdgeByUnique((SequenceExpressionEdgeByUnique)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Source:
                PrintSequenceExpressionSource((SequenceExpressionSource)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Target:
                PrintSequenceExpressionTarget((SequenceExpressionTarget)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Opposite:
                PrintSequenceExpressionOpposite((SequenceExpressionOpposite)seqExpr, parent, context);
                break;
            case SequenceExpressionType.GraphElementAttributeOrElementOfMatch:
                PrintSequenceExpressionAttributeOrMatchAccess((SequenceExpressionAttributeOrMatchAccess)seqExpr, parent, context);
                break;
            case SequenceExpressionType.GraphElementAttribute:
                PrintSequenceExpressionAttributeAccess((SequenceExpressionAttributeAccess)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ElementOfMatch:
                PrintSequenceExpressionMatchAccess((SequenceExpressionMatchAccess)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Nodes:
                PrintSequenceExpressionNodes((SequenceExpressionNodes)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Edges:
                PrintSequenceExpressionEdges((SequenceExpressionEdges)seqExpr, parent, context);
                break;
            case SequenceExpressionType.CountNodes:
                PrintSequenceExpressionCountNodes((SequenceExpressionCountNodes)seqExpr, parent, context);
                break;
            case SequenceExpressionType.CountEdges:
                PrintSequenceExpressionCountEdges((SequenceExpressionCountEdges)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Now:
                PrintSequenceExpressionNow((SequenceExpressionNow)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathMin:
                PrintSequenceExpressionMathMin((SequenceExpressionMathMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathMax:
                PrintSequenceExpressionMathMax((SequenceExpressionMathMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathAbs:
                PrintSequenceExpressionMathAbs((SequenceExpressionMathAbs)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathCeil:
                PrintSequenceExpressionMathCeil((SequenceExpressionMathCeil)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathFloor:
                PrintSequenceExpressionMathFloor((SequenceExpressionMathFloor)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathRound:
                PrintSequenceExpressionMathRound((SequenceExpressionMathRound)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathTruncate:
                PrintSequenceExpressionMathTruncate((SequenceExpressionMathTruncate)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathSqr:
                PrintSequenceExpressionMathSqr((SequenceExpressionMathSqr)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathSqrt:
                PrintSequenceExpressionMathSqrt((SequenceExpressionMathSqrt)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathPow:
                PrintSequenceExpressionMathPow((SequenceExpressionMathPow)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathLog:
                PrintSequenceExpressionMathLog((SequenceExpressionMathLog)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathSgn:
                PrintSequenceExpressionMathSgn((SequenceExpressionMathSgn)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathSin:
                PrintSequenceExpressionMathSin((SequenceExpressionMathSin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathCos:
                PrintSequenceExpressionMathCos((SequenceExpressionMathCos)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathTan:
                PrintSequenceExpressionMathTan((SequenceExpressionMathTan)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathArcSin:
                PrintSequenceExpressionMathArcSin((SequenceExpressionMathArcSin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathArcCos:
                PrintSequenceExpressionMathArcCos((SequenceExpressionMathArcCos)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathArcTan:
                PrintSequenceExpressionMathArcTan((SequenceExpressionMathArcTan)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathPi:
                PrintSequenceExpressionMathPi((SequenceExpressionMathPi)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathE:
                PrintSequenceExpressionMathE((SequenceExpressionMathE)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathByteMin:
                PrintSequenceExpressionMathByteMin((SequenceExpressionMathByteMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathByteMax:
                PrintSequenceExpressionMathByteMax((SequenceExpressionMathByteMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathShortMin:
                PrintSequenceExpressionMathShortMin((SequenceExpressionMathShortMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathShortMax:
                PrintSequenceExpressionMathShortMax((SequenceExpressionMathShortMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathIntMin:
                PrintSequenceExpressionMathIntMin((SequenceExpressionMathIntMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathIntMax:
                PrintSequenceExpressionMathIntMax((SequenceExpressionMathIntMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathLongMin:
                PrintSequenceExpressionMathLongMin((SequenceExpressionMathLongMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathLongMax:
                PrintSequenceExpressionMathLongMax((SequenceExpressionMathLongMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathFloatMin:
                PrintSequenceExpressionMathFloatMin((SequenceExpressionMathFloatMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathFloatMax:
                PrintSequenceExpressionMathFloatMax((SequenceExpressionMathFloatMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathDoubleMin:
                PrintSequenceExpressionMathDoubleMin((SequenceExpressionMathDoubleMin)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MathDoubleMax:
                PrintSequenceExpressionMathDoubleMax((SequenceExpressionMathDoubleMax)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Empty:
                PrintSequenceExpressionEmpty((SequenceExpressionEmpty)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Size:
                PrintSequenceExpressionSize((SequenceExpressionSize)seqExpr, parent, context);
                break;
            case SequenceExpressionType.AdjacentNodes:
            case SequenceExpressionType.AdjacentNodesViaIncoming:
            case SequenceExpressionType.AdjacentNodesViaOutgoing:
            case SequenceExpressionType.IncidentEdges:
            case SequenceExpressionType.IncomingEdges:
            case SequenceExpressionType.OutgoingEdges:
                PrintSequenceExpressionAdjacentIncident((SequenceExpressionAdjacentIncident)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ReachableNodes:
            case SequenceExpressionType.ReachableNodesViaIncoming:
            case SequenceExpressionType.ReachableNodesViaOutgoing:
            case SequenceExpressionType.ReachableEdges:
            case SequenceExpressionType.ReachableEdgesViaIncoming:
            case SequenceExpressionType.ReachableEdgesViaOutgoing:
                PrintSequenceExpressionReachable((SequenceExpressionReachable)seqExpr, parent, context);
                break;
            case SequenceExpressionType.BoundedReachableNodes:
            case SequenceExpressionType.BoundedReachableNodesViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.BoundedReachableEdges:
            case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionBoundedReachable((SequenceExpressionBoundedReachable)seqExpr, parent, context);
                break;
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                PrintSequenceExpressionBoundedReachableWithRemainingDepth((SequenceExpressionBoundedReachableWithRemainingDepth)seqExpr, parent, context);
                break;
            case SequenceExpressionType.CountAdjacentNodes:
            case SequenceExpressionType.CountAdjacentNodesViaIncoming:
            case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
            case SequenceExpressionType.CountIncidentEdges:
            case SequenceExpressionType.CountIncomingEdges:
            case SequenceExpressionType.CountOutgoingEdges:
                PrintSequenceExpressionCountAdjacentIncident((SequenceExpressionCountAdjacentIncident)seqExpr, parent, context);
                break;
            case SequenceExpressionType.CountReachableNodes:
            case SequenceExpressionType.CountReachableNodesViaIncoming:
            case SequenceExpressionType.CountReachableNodesViaOutgoing:
            case SequenceExpressionType.CountReachableEdges:
            case SequenceExpressionType.CountReachableEdgesViaIncoming:
            case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountReachable((SequenceExpressionCountReachable)seqExpr, parent, context);
                break;
            case SequenceExpressionType.CountBoundedReachableNodes:
            case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.CountBoundedReachableEdges:
            case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountBoundedReachable((SequenceExpressionCountBoundedReachable)seqExpr, parent, context);
                break;
            case SequenceExpressionType.IsAdjacentNodes:
            case SequenceExpressionType.IsAdjacentNodesViaIncoming:
            case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
            case SequenceExpressionType.IsIncidentEdges:
            case SequenceExpressionType.IsIncomingEdges:
            case SequenceExpressionType.IsOutgoingEdges:
                PrintSequenceExpressionIsAdjacentIncident((SequenceExpressionIsAdjacentIncident)seqExpr, parent, context);
                break;
            case SequenceExpressionType.IsReachableNodes:
            case SequenceExpressionType.IsReachableNodesViaIncoming:
            case SequenceExpressionType.IsReachableNodesViaOutgoing:
            case SequenceExpressionType.IsReachableEdges:
            case SequenceExpressionType.IsReachableEdgesViaIncoming:
            case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsReachable((SequenceExpressionIsReachable)seqExpr, parent, context);
                break;
            case SequenceExpressionType.IsBoundedReachableNodes:
            case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.IsBoundedReachableEdges:
            case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsBoundedReachable((SequenceExpressionIsBoundedReachable)seqExpr, parent, context);
                break;
            case SequenceExpressionType.InducedSubgraph:
                PrintSequenceExpressionInducedSubgraph((SequenceExpressionInducedSubgraph)seqExpr, parent, context);
                break;
            case SequenceExpressionType.DefinedSubgraph:
                PrintSequenceExpressionDefinedSubgraph((SequenceExpressionDefinedSubgraph)seqExpr, parent, context);
                break;
            case SequenceExpressionType.EqualsAny:
                PrintSequenceExpressionEqualsAny((SequenceExpressionEqualsAny)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Nameof:
                PrintSequenceExpressionNameof((SequenceExpressionNameof)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Uniqueof:
                PrintSequenceExpressionUniqueof((SequenceExpressionUniqueof)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Typeof:
                PrintSequenceExpressionTypeof((SequenceExpressionTypeof)seqExpr, parent, context);
                break;
            case SequenceExpressionType.ExistsFile:
                PrintSequenceExpressionExistsFile((SequenceExpressionExistsFile)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Import:
                PrintSequenceExpressionImport((SequenceExpressionImport)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Copy:
                PrintSequenceExpressionCopy((SequenceExpressionCopy)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Canonize:
                PrintSequenceExpressionCanonize((SequenceExpressionCanonize)seqExpr, parent, context);
                break;
            case SequenceExpressionType.RuleQuery:
                PrintSequenceExpressionRuleQuery((SequenceExpressionRuleQuery)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MultiRuleQuery:
                PrintSequenceExpressionMultiRuleQuery((SequenceExpressionMultiRuleQuery)seqExpr, parent, context);
                break;
            case SequenceExpressionType.MappingClause:
                PrintSequenceExpressionMappingClause((SequenceExpressionMappingClause)seqExpr, parent, context);
                break;
            case SequenceExpressionType.Scan:
                PrintSequenceExpressionScan((SequenceExpressionScan)seqExpr, parent, context);
                break;
            case SequenceExpressionType.TryScan:
                PrintSequenceExpressionTryScan((SequenceExpressionTryScan)seqExpr, parent, context);
                break;
            case SequenceExpressionType.FunctionCall:
                PrintSequenceExpressionFunctionCall((SequenceExpressionFunctionCall)seqExpr, parent, context);
                break;
            case SequenceExpressionType.FunctionMethodCall:
                PrintSequenceExpressionFunctionMethodCall((SequenceExpressionFunctionMethodCall)seqExpr, parent, context);
                break;
            }
        }

        private static void PrintSequenceExpressionConditional(SequenceExpressionConditional seqExprCond, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprCond.Condition, seqExprCond, context);
            Console.Write(" ? ");
            PrintSequenceExpression(seqExprCond.TrueCase, seqExprCond, context);
            Console.Write(" : ");
            PrintSequenceExpression(seqExprCond.FalseCase, seqExprCond, context);
        }

        private static void PrintSequenceExpressionBinary(SequenceBinaryExpression seqExprBin, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprBin.Left, seqExprBin, context);
            Console.Write(seqExprBin.Operator);
            PrintSequenceExpression(seqExprBin.Right, seqExprBin, context);
        }

        private static void PrintSequenceExpressionNot(SequenceExpressionNot seqExprNot, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("!");
            PrintSequenceExpression(seqExprNot.Operand, seqExprNot, context);
        }

        private static void PrintSequenceExpressionUnaryPlus(SequenceExpressionUnaryPlus seqExprUnaryPlus, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("+");
            PrintSequenceExpression(seqExprUnaryPlus.Operand, seqExprUnaryPlus, context);
        }

        private static void PrintSequenceExpressionUnaryMinus(SequenceExpressionUnaryMinus seqExprUnaryMinus, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("-");
            PrintSequenceExpression(seqExprUnaryMinus.Operand, seqExprUnaryMinus, context);
        }

        private static void PrintSequenceExpressionBitwiseComplement(SequenceExpressionBitwiseComplement seqExprBitwiseComplement, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("~");
            PrintSequenceExpression(seqExprBitwiseComplement.Operand, seqExprBitwiseComplement, context);
        }

        private static void PrintSequenceExpressionCast(SequenceExpressionCast seqExprCast, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("(");
            Console.Write(((InheritanceType)seqExprCast.TargetType).Name);
            Console.Write(")");
            PrintSequenceExpression(seqExprCast.Operand, seqExprCast, context);
        }

        private static void PrintSequenceExpressionConstant(SequenceExpressionConstant seqExprConstant, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(SequenceExpressionConstant.ConstantAsString(seqExprConstant.Constant));
        }

        private static void PrintSequenceExpressionVariable(SequenceExpressionVariable seqExprVariable, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprVariable.Variable.Name);
        }

        private static void PrintSequenceExpressionNew(SequenceExpressionNew seqExprNew, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprNew.ConstructedType); // TODO: check -- looks suspicious
        }

        private static void PrintSequenceExpressionThis(SequenceExpressionThis seqExprThis, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("this");
        }

        private static void PrintSequenceExpressionMatchClassConstructor(SequenceExpressionMatchClassConstructor seqExprMatchClassConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("match<class " + seqExprMatchClassConstructor.ConstructedType + ">()");
        }

        private static void PrintSequenceExpressionSetConstructor(SequenceExpressionSetConstructor seqExprSetConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("set<");
            Console.Write(seqExprSetConstructor.ValueType);
            Console.Write(">{");
            PrintSequenceExpressionContainerConstructor(seqExprSetConstructor, seqExprSetConstructor, context);
            Console.Write("}");
        }

        private static void PrintSequenceExpressionMapConstructor(SequenceExpressionMapConstructor seqExprMapConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("map<");
            Console.Write(seqExprMapConstructor.KeyType);
            Console.Write(",");
            Console.Write(seqExprMapConstructor.ValueType);
            Console.Write(">{");
            for(int i = 0; i < seqExprMapConstructor.MapKeyItems.Length; ++i)
            {
                Console.Write(seqExprMapConstructor.MapKeyItems[i].Symbol);
                Console.Write("->");
                Console.Write(seqExprMapConstructor.ContainerItems[i].Symbol);
                if(i != seqExprMapConstructor.MapKeyItems.Length - 1)
                    Console.Write(",");
            }
            Console.Write("}");
        }

        private static void PrintSequenceExpressionArrayConstructor(SequenceExpressionArrayConstructor seqExprArrayConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("array<");
            Console.Write(seqExprArrayConstructor.ValueType);
            Console.Write(">[");
            PrintSequenceExpressionContainerConstructor(seqExprArrayConstructor, seqExprArrayConstructor, context);
            Console.Write("]");
        }

        private static void PrintSequenceExpressionDequeConstructor(SequenceExpressionDequeConstructor seqExprDequeConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("deque<");
            Console.Write(seqExprDequeConstructor.ValueType);
            Console.Write(">]");
            PrintSequenceExpressionContainerConstructor(seqExprDequeConstructor, seqExprDequeConstructor, context);
            Console.Write("[");
        }

        private static void PrintSequenceExpressionContainerConstructor(SequenceExpressionContainerConstructor seqExprContainerConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            for(int i = 0; i < seqExprContainerConstructor.ContainerItems.Length; ++i)
            {
                Console.Write(seqExprContainerConstructor.ContainerItems[i].Symbol);
                if(i != seqExprContainerConstructor.ContainerItems.Length - 1)
                    Console.Write(",");
            }
        }

        private static void PrintSequenceExpressionSetCopyConstructor(SequenceExpressionSetCopyConstructor seqExprSetCopyConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("set<");
            Console.Write(seqExprSetCopyConstructor.ValueType);
            Console.Write(">(");
            Console.Write(seqExprSetCopyConstructor.SetToCopy);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMapCopyConstructor(SequenceExpressionMapCopyConstructor seqExprMapCopyConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("map<");
            Console.Write(seqExprMapCopyConstructor.KeyType);
            Console.Write(",");
            Console.Write(seqExprMapCopyConstructor.ValueType);
            Console.Write(">(");
            Console.Write(seqExprMapCopyConstructor.MapToCopy);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArrayCopyConstructor(SequenceExpressionArrayCopyConstructor seqExprArrayCopyConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("array<");
            Console.Write(seqExprArrayCopyConstructor.ValueType);
            Console.Write(">[");
            Console.Write(seqExprArrayCopyConstructor.ArrayToCopy);
            Console.Write("]");
        }

        private static void PrintSequenceExpressionDequeCopyConstructor(SequenceExpressionDequeCopyConstructor seqExprDequeCopyConstructor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("deque<");
            Console.Write(seqExprDequeCopyConstructor.ValueType);
            Console.Write(">[");
            Console.Write(seqExprDequeCopyConstructor.DequeToCopy);
            Console.Write("]");
        }

        private static void PrintSequenceExpressionContainerAsArray(SequenceExpressionContainerAsArray seqExprContainerAsArray, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprContainerAsArray.Name + ".asArray()");
        }

        private static void PrintSequenceExpressionStringLength(SequenceExpressionStringLength seqExprStringLength, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringLength.StringExpr, seqExprStringLength, context);
            Console.Write(".length()");
        }

        private static void PrintSequenceExpressionStringStartsWith(SequenceExpressionStringStartsWith seqExprStringStartsWith, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringStartsWith.StringExpr, seqExprStringStartsWith, context);
            Console.Write(".startsWith(");
            PrintSequenceExpression(seqExprStringStartsWith.StringToSearchForExpr, seqExprStringStartsWith, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionStringEndsWith(SequenceExpressionStringEndsWith seqExprStringEndsWith, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringEndsWith.StringExpr, seqExprStringEndsWith, context);
            Console.Write(".endsWith(");
            PrintSequenceExpression(seqExprStringEndsWith.StringToSearchForExpr, seqExprStringEndsWith, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionStringSubstring(SequenceExpressionStringSubstring seqExprStringSubstring, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringSubstring.StringExpr, seqExprStringSubstring, context);
            Console.Write(".substring(");
            PrintSequenceExpression(seqExprStringSubstring.StartIndexExpr, seqExprStringSubstring, context);
            if(seqExprStringSubstring.LengthExpr != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprStringSubstring.LengthExpr, seqExprStringSubstring, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionStringReplace(SequenceExpressionStringReplace seqExprStringReplace, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringReplace.StringExpr, seqExprStringReplace, context);
            Console.Write(".replace(");
            PrintSequenceExpression(seqExprStringReplace.StartIndexExpr, seqExprStringReplace, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprStringReplace.LengthExpr, seqExprStringReplace, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprStringReplace.ReplaceStringExpr, seqExprStringReplace, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionStringToLower(SequenceExpressionStringToLower seqExprStringToLower, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringToLower.StringExpr, seqExprStringToLower, context);
            Console.Write(".toLower()");
        }

        private static void PrintSequenceExpressionStringToUpper(SequenceExpressionStringToUpper seqExprStringToUpper, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringToUpper.StringExpr, seqExprStringToUpper, context);
            Console.Write(".toUpper()");
        }

        private static void PrintSequenceExpressionStringAsArray(SequenceExpressionStringAsArray seqExprStringAsArray, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprStringAsArray.StringExpr, seqExprStringAsArray, context);
            Console.Write(".asArray(");
            PrintSequenceExpression(seqExprStringAsArray.SeparatorExpr, seqExprStringAsArray, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionRandom(SequenceExpressionRandom seqExprRandom, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("random(");
            if(seqExprRandom.UpperBound != null)
                PrintSequenceExpression(seqExprRandom.UpperBound, seqExprRandom, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionDef(SequenceExpressionDef seqExprDef, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("def(");
            for(int i = 0; i < seqExprDef.DefVars.Length; ++i)
            {
                Console.Write(seqExprDef.DefVars[i].Symbol);
                if(i != seqExprDef.DefVars.Length - 1)
                    Console.Write(",");
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionIsVisited(SequenceExpressionIsVisited seqExprIsVisited, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprIsVisited.GraphElementVarExpr, seqExprIsVisited, context);
            Console.Write(".visited");
            if(seqExprIsVisited.VisitedFlagExpr != null)
            {
                Console.Write("[");
                PrintSequenceExpression(seqExprIsVisited.VisitedFlagExpr, seqExprIsVisited, context);
                Console.Write("]");
            }
        }

        private static void PrintSequenceExpressionInContainerOrString(SequenceExpressionInContainerOrString seqExprInContainerOrString, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprInContainerOrString.Expr, seqExprInContainerOrString, context);
            Console.Write(" in ");
            PrintSequenceExpression(seqExprInContainerOrString.ContainerOrStringExpr, seqExprInContainerOrString, context);
        }

        private static void PrintSequenceExpressionContainerSize(SequenceExpressionContainerSize seqExprContainerSize, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerSize.ContainerExpr, seqExprContainerSize, context);
            Console.Write(".size()");
        }

        private static void PrintSequenceExpressionContainerEmpty(SequenceExpressionContainerEmpty seqExprContainerEmpty, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerEmpty.ContainerExpr, seqExprContainerEmpty, context);
            Console.Write(".empty()");
        }

        private static void PrintSequenceExpressionContainerAccess(SequenceExpressionContainerAccess seqExprContainerAccess, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerAccess.ContainerExpr, seqExprContainerAccess, context);
            Console.Write("[");
            PrintSequenceExpression(seqExprContainerAccess.KeyExpr, seqExprContainerAccess, context);
            Console.Write("]");
        }

        private static void PrintSequenceExpressionContainerPeek(SequenceExpressionContainerPeek seqExprContainerPeek, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprContainerPeek.ContainerExpr, seqExprContainerPeek, context);
            Console.Write(".peek(");
            if(seqExprContainerPeek.KeyExpr != null)
                PrintSequenceExpression(seqExprContainerPeek.KeyExpr, seqExprContainerPeek, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArrayOrDequeOrStringIndexOf(SequenceExpressionArrayOrDequeOrStringIndexOf seqExprArrayOrDequeOrStringIndexOf, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringIndexOf, context);
            Console.Write(".indexOf(");
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringIndexOf, context);
            if(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringIndexOf, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf(SequenceExpressionArrayOrDequeOrStringLastIndexOf seqExprArrayOrDequeOrStringLastIndexOf, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringLastIndexOf, context);
            Console.Write(".lastIndexOf(");
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringLastIndexOf, context);
            if(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringLastIndexOf, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArrayIndexOfOrdered(SequenceExpressionArrayIndexOfOrdered seqExprArrayIndexOfOrdered, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ContainerExpr, seqExprArrayIndexOfOrdered, context);
            Console.Write(".indexOfOrdered(");
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ValueToSearchForExpr, seqExprArrayIndexOfOrdered, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArraySum(SequenceExpressionArraySum seqExprArraySum, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArraySum.ContainerExpr, seqExprArraySum, context);
            Console.Write(".sum()");
        }

        private static void PrintSequenceExpressionArrayProd(SequenceExpressionArrayProd seqExprArrayProd, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayProd.ContainerExpr, seqExprArrayProd, context);
            Console.Write(".prod()");
        }

        private static void PrintSequenceExpressionArrayMin(SequenceExpressionArrayMin seqExprArrayMin, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMin.ContainerExpr, seqExprArrayMin, context);
            Console.Write(".min()");
        }

        private static void PrintSequenceExpressionArrayMax(SequenceExpressionArrayMax seqExprArrayMax, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMax.ContainerExpr, seqExprArrayMax, context);
            Console.Write(".max()");
        }

        private static void PrintSequenceExpressionArrayAvg(SequenceExpressionArrayAvg seqExprArrayAvg, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAvg.ContainerExpr, seqExprArrayAvg, context);
            Console.Write(".avg()");
        }

        private static void PrintSequenceExpressionArrayMed(SequenceExpressionArrayMed seqExprArrayMed, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMed.ContainerExpr, seqExprArrayMed, context);
            Console.Write(".med()");
        }

        private static void PrintSequenceExpressionArrayMedUnordered(SequenceExpressionArrayMedUnordered seqExprArrayMedUnordered, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayMedUnordered.ContainerExpr, seqExprArrayMedUnordered, context);
            Console.Write(".medUnordered()");
        }

        private static void PrintSequenceExpressionArrayVar(SequenceExpressionArrayVar seqExprArrayVar, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayVar.ContainerExpr, seqExprArrayVar, context);
            Console.Write(".var()");
        }

        private static void PrintSequenceExpressionArrayDev(SequenceExpressionArrayDev seqExprArrayDev, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayDev.ContainerExpr, seqExprArrayDev, context);
            Console.Write(".dev()");
        }

        private static void PrintSequenceExpressionArrayAnd(SequenceExpressionArrayAnd seqExprArrayAnd, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAnd.ContainerExpr, seqExprArrayAnd, context);
            Console.Write(".and()");
        }

        private static void PrintSequenceExpressionArrayOr(SequenceExpressionArrayOr seqExprArrayOr, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOr.ContainerExpr, seqExprArrayOr, context);
            Console.Write(".or()");
        }

        private static void PrintSequenceExpressionArrayOrDequeAsSet(SequenceExpressionArrayOrDequeAsSet seqExprArrayOrDequeAsSet, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrDequeAsSet.ContainerExpr, seqExprArrayOrDequeAsSet, context);
            Console.Write(".asSet()");
        }

        private static void PrintSequenceExpressionMapDomain(SequenceExpressionMapDomain seqExprMapDomain, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprMapDomain.ContainerExpr, seqExprMapDomain, context);
            Console.Write(".domain()");
        }

        private static void PrintSequenceExpressionMapRange(SequenceExpressionMapRange seqExprMapRange, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprMapRange.ContainerExpr, seqExprMapRange, context);
            Console.Write(".range()");
        }

        private static void PrintSequenceExpressionArrayAsMap(SequenceExpressionArrayAsMap seqExprArrayAsMap, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAsMap.ContainerExpr, seqExprArrayAsMap, context);
            Console.Write(".asSet()");
        }

        private static void PrintSequenceExpressionArrayAsDeque(SequenceExpressionArrayAsDeque seqExprArrayAsDeque, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAsDeque.ContainerExpr, seqExprArrayAsDeque, context);
            Console.Write(".asDeque()");
        }

        private static void PrintSequenceExpressionArrayAsString(SequenceExpressionArrayAsString seqExprArrayAsString, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayAsString.ContainerExpr, seqExprArrayAsString, context);
            Console.Write(".asString(");
            PrintSequenceExpression(seqExprArrayAsString.Separator, seqExprArrayAsString, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArraySubarray(SequenceExpressionArraySubarray seqExprArraySubarray, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArraySubarray.ContainerExpr, seqExprArraySubarray, context);
            Console.Write(".subarray(");
            PrintSequenceExpression(seqExprArraySubarray.Start, seqExprArraySubarray, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprArraySubarray.Length, seqExprArraySubarray, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionDequeSubdeque(SequenceExpressionDequeSubdeque seqExprDequeSubdeque, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprDequeSubdeque.ContainerExpr, seqExprDequeSubdeque, context);
            Console.Write(".subdeque(");
            PrintSequenceExpression(seqExprDequeSubdeque.Start, seqExprDequeSubdeque, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprDequeSubdeque.Length, seqExprDequeSubdeque, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArrayOrderAscending(SequenceExpressionArrayOrderAscending seqExprArrayOrderAscending, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrderAscending.ContainerExpr, seqExprArrayOrderAscending, context);
            Console.Write(".orderAscending()");
        }

        private static void PrintSequenceExpressionArrayOrderDescending(SequenceExpressionArrayOrderDescending seqExprArrayOrderDescending, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayOrderDescending.ContainerExpr, seqExprArrayOrderDescending, context);
            Console.Write(".orderDescending()");
        }

        private static void PrintSequenceExpressionArrayGroup(SequenceExpressionArrayGroup seqExprArrayGroup, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayGroup.ContainerExpr, seqExprArrayGroup, context);
            Console.Write(".group()");
        }

        private static void PrintSequenceExpressionArrayKeepOneForEach(SequenceExpressionArrayKeepOneForEach seqExprArrayKeepOneForEach, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayKeepOneForEach.ContainerExpr, seqExprArrayKeepOneForEach, context);
            Console.Write(".keepOneForEach()");
        }

        private static void PrintSequenceExpressionArrayReverse(SequenceExpressionArrayReverse seqExprArrayReverse, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayReverse.ContainerExpr, seqExprArrayReverse, context);
            Console.Write(".reverse()");
        }

        private static void PrintSequenceExpressionArrayShuffle(SequenceExpressionArrayShuffle seqExprArrayShuffle, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayShuffle.ContainerExpr, seqExprArrayShuffle, context);
            Console.Write(".shuffle()");
        }

        private static void PrintSequenceExpressionArrayExtract(SequenceExpressionArrayExtract seqExprArrayExtract, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprArrayExtract.ContainerExpr, seqExprArrayExtract, context);
            Console.Write(".extract<");
            Console.Write(seqExprArrayExtract.memberOrAttributeName);
            Console.Write(">()");
        }

        private static void PrintSequenceExpressionArrayMap(SequenceExpressionArrayMap seqExprArrayMap, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayMap.Name + ".map<" + seqExprArrayMap.TypeName + ">{");
            if(seqExprArrayMap.ArrayAccess != null)
                Console.Write(seqExprArrayMap.ArrayAccess.Name + "; ");
            if(seqExprArrayMap.Index != null)
                Console.Write(seqExprArrayMap.Index.Name + " -> ");
            Console.Write(seqExprArrayMap.Var.Name + " -> ");
            PrintSequenceExpression(seqExprArrayMap.MappingExpr, seqExprArrayMap, context);
            Console.Write("}");
        }

        private static void PrintSequenceExpressionArrayRemoveIf(SequenceExpressionArrayRemoveIf seqExprArrayRemoveIf, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayRemoveIf.Name + ".removeIf{");
            if(seqExprArrayRemoveIf.ArrayAccess != null)
                Console.Write(seqExprArrayRemoveIf.ArrayAccess.Name + "; ");
            if(seqExprArrayRemoveIf.Index != null)
                Console.Write(seqExprArrayRemoveIf.Index.Name + " -> ");
            Console.Write(seqExprArrayRemoveIf.Var.Name + " -> ");
            PrintSequenceExpression(seqExprArrayRemoveIf.ConditionExpr, seqExprArrayRemoveIf, context);
            Console.Write("}");
        }

        private static void PrintSequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpressionArrayMapStartWithAccumulateBy seqExprArrayMapStartWithAccumulateBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayMapStartWithAccumulateBy.Name + ".map<" + seqExprArrayMapStartWithAccumulateBy.TypeName + ">");
            Console.Write("StartWith");
            Console.Write("{");
            if(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess != null)
                Console.Write(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess.Name + "; ");
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.InitExpr, seqExprArrayMapStartWithAccumulateBy, context);
            Console.Write("}");
            Console.Write("AccumulateBy");
            Console.Write("{");
            if(seqExprArrayMapStartWithAccumulateBy.ArrayAccess != null)
                Console.Write(seqExprArrayMapStartWithAccumulateBy.ArrayAccess.Name + "; ");
            Console.Write(seqExprArrayMapStartWithAccumulateBy.PreviousAccumulationAccess.Name + ", ");
            if(seqExprArrayMapStartWithAccumulateBy.Index != null)
                Console.Write(seqExprArrayMapStartWithAccumulateBy.Index.Name + " -> ");
            Console.Write(seqExprArrayMapStartWithAccumulateBy.Var.Name + " -> ");
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.MappingExpr, seqExprArrayMapStartWithAccumulateBy, context);
            Console.Write("}");
        }

        private static void PrintSequenceExpressionArrayOrderAscendingBy(SequenceExpressionArrayOrderAscendingBy seqExprArrayOrderAscendingBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayOrderAscendingBy.Name + ".orderAscendingBy<" + seqExprArrayOrderAscendingBy.memberOrAttributeName + ">()");
        }

        private static void PrintSequenceExpressionArrayOrderDescendingBy(SequenceExpressionArrayOrderDescendingBy seqExprArrayOrderDescendingBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayOrderDescendingBy.Name + ".orderDescendingBy<" + seqExprArrayOrderDescendingBy.memberOrAttributeName + ">()");
        }

        private static void PrintSequenceExpressionArrayGroupBy(SequenceExpressionArrayGroupBy seqExprArrayGroupBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayGroupBy.Name + ".groupBy<" + seqExprArrayGroupBy.memberOrAttributeName + ">()");
        }

        private static void PrintSequenceExpressionArrayKeepOneForEachBy(SequenceExpressionArrayKeepOneForEachBy seqExprArrayKeepOneForEachBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayKeepOneForEachBy.Name + ".keepOneForEach<" + seqExprArrayKeepOneForEachBy.memberOrAttributeName + ">()");
        }

        private static void PrintSequenceExpressionArrayIndexOfBy(SequenceExpressionArrayIndexOfBy seqExprArrayIndexOfBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayIndexOfBy.Name + ".indexOfBy<" + seqExprArrayIndexOfBy.memberOrAttributeName + ">(");
            PrintSequenceExpression(seqExprArrayIndexOfBy.ValueToSearchForExpr, seqExprArrayIndexOfBy, context);
            if(seqExprArrayIndexOfBy.StartPositionExpr != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprArrayIndexOfBy.StartPositionExpr, seqExprArrayIndexOfBy, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArrayLastIndexOfBy(SequenceExpressionArrayLastIndexOfBy seqExprArrayLastIndexOfBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayLastIndexOfBy.Name + ".lastIndexOfBy<" + seqExprArrayLastIndexOfBy.memberOrAttributeName + ">(");
            PrintSequenceExpression(seqExprArrayLastIndexOfBy.ValueToSearchForExpr, seqExprArrayLastIndexOfBy, context);
            if(seqExprArrayLastIndexOfBy.StartPositionExpr != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprArrayLastIndexOfBy.StartPositionExpr, seqExprArrayLastIndexOfBy, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionArrayIndexOfOrderedBy(SequenceExpressionArrayIndexOfOrderedBy seqExprArrayIndexOfOrderedBy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprArrayIndexOfOrderedBy.Name + ".indexOfOrderedBy<" + seqExprArrayIndexOfOrderedBy.memberOrAttributeName + ">(");
            PrintSequenceExpression(seqExprArrayIndexOfOrderedBy.ValueToSearchForExpr, seqExprArrayIndexOfOrderedBy, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionElementFromGraph(SequenceExpressionElementFromGraph seqExprElementFromGraph, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("@(" + seqExprElementFromGraph.ElementName + ")");
        }

        private static void PrintSequenceExpressionNodeByName(SequenceExpressionNodeByName seqExprNodeByName, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("nodeByName(");
            PrintSequenceExpression(seqExprNodeByName.NodeName, seqExprNodeByName, context);
            if(seqExprNodeByName.NodeType != null)
            {
                Console.Write(", ");
                PrintSequenceExpression(seqExprNodeByName.NodeType, seqExprNodeByName, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionEdgeByName(SequenceExpressionEdgeByName seqExprEdgeByName, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("edgeByName(");
            PrintSequenceExpression(seqExprEdgeByName.EdgeName, seqExprEdgeByName, context);
            if(seqExprEdgeByName.EdgeType != null)
            {
                Console.Write(", ");
                PrintSequenceExpression(seqExprEdgeByName.EdgeType, seqExprEdgeByName, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionNodeByUnique(SequenceExpressionNodeByUnique seqExprNodeByUnique, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("nodeByUnique(");
            PrintSequenceExpression(seqExprNodeByUnique.NodeUniqueId, seqExprNodeByUnique, context);
            if(seqExprNodeByUnique.NodeType != null)
            {
                Console.Write(", ");
                PrintSequenceExpression(seqExprNodeByUnique.NodeType, seqExprNodeByUnique, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionEdgeByUnique(SequenceExpressionEdgeByUnique seqExprEdgeByUnique, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("edgeByUnique(");
            PrintSequenceExpression(seqExprEdgeByUnique.EdgeUniqueId, seqExprEdgeByUnique, context);
            if(seqExprEdgeByUnique.EdgeType != null)
            {
                Console.Write(", ");
                PrintSequenceExpression(seqExprEdgeByUnique.EdgeType, seqExprEdgeByUnique, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionSource(SequenceExpressionSource seqExprSource, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("source(");
            PrintSequenceExpression(seqExprSource.Edge, seqExprSource, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionTarget(SequenceExpressionTarget seqExprTarget, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("target(");
            PrintSequenceExpression(seqExprTarget.Edge, seqExprTarget, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionOpposite(SequenceExpressionOpposite seqExprOpposite, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("opposite(");
            PrintSequenceExpression(seqExprOpposite.Edge, seqExprOpposite, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprOpposite.Node, seqExprOpposite, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionAttributeAccess(SequenceExpressionAttributeAccess seqExprAttributeAccess, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprAttributeAccess.Source, seqExprAttributeAccess, context);
            Console.Write(".");
            Console.Write(seqExprAttributeAccess.AttributeName);
        }

        private static void PrintSequenceExpressionMatchAccess(SequenceExpressionMatchAccess seqExprMatchAccess, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprMatchAccess.Source, seqExprMatchAccess, context);
            Console.Write(".");
            Console.Write(seqExprMatchAccess.ElementName);
        }

        private static void PrintSequenceExpressionAttributeOrMatchAccess(SequenceExpressionAttributeOrMatchAccess seqExprAttributeOrMatchAccess, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprAttributeOrMatchAccess.Source, seqExprAttributeOrMatchAccess, context);
            Console.Write(".");
            Console.Write(seqExprAttributeOrMatchAccess.AttributeOrElementName);
        }

        private static void PrintSequenceExpressionNodes(SequenceExpressionNodes seqExprNodes, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprNodes.FunctionSymbol + "(");
            if(seqExprNodes.NodeType != null)
                PrintSequenceExpression(seqExprNodes.NodeType, seqExprNodes, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionEdges(SequenceExpressionEdges seqExprEdges, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprEdges.FunctionSymbol + "(");
            if(seqExprEdges.EdgeType != null)
                PrintSequenceExpression(seqExprEdges.EdgeType, seqExprEdges, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionCountNodes(SequenceExpressionCountNodes seqExprCountNodes, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprCountNodes.FunctionSymbol + "(");
            if(seqExprCountNodes.NodeType != null)
                PrintSequenceExpression(seqExprCountNodes.NodeType, seqExprCountNodes, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionCountEdges(SequenceExpressionCountEdges seqExprCountEdges, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprCountEdges.FunctionSymbol + "(");
            if(seqExprCountEdges.EdgeType != null)
                PrintSequenceExpression(seqExprCountEdges.EdgeType, seqExprCountEdges, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionEmpty(SequenceExpressionEmpty seqExprEmpty, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("empty()");
        }

        private static void PrintSequenceExpressionNow(SequenceExpressionNow seqExprNow, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Time::now()");
        }

        private static void PrintSequenceExpressionSize(SequenceExpressionSize seqExprSize, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("size()");
        }

        private static void PrintSequenceExpressionAdjacentIncident(SequenceExpressionAdjacentIncident seqExprAdjacentIncident, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprAdjacentIncident.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprAdjacentIncident.SourceNode, seqExprAdjacentIncident, context);
            if(seqExprAdjacentIncident.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprAdjacentIncident.EdgeType, seqExprAdjacentIncident, context);
            }
            if(seqExprAdjacentIncident.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprAdjacentIncident.OppositeNodeType, seqExprAdjacentIncident, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionCountAdjacentIncident(SequenceExpressionCountAdjacentIncident seqExprCountAdjacentIncident, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprCountAdjacentIncident.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprCountAdjacentIncident.SourceNode, seqExprCountAdjacentIncident, context);
            if(seqExprCountAdjacentIncident.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprCountAdjacentIncident.EdgeType, seqExprCountAdjacentIncident, context);
            }
            if(seqExprCountAdjacentIncident.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprCountAdjacentIncident.OppositeNodeType, seqExprCountAdjacentIncident, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionReachable(SequenceExpressionReachable seqExprReachable, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprReachable.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprReachable.SourceNode, seqExprReachable, context);
            if(seqExprReachable.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprReachable.EdgeType, seqExprReachable, context);
            }
            if(seqExprReachable.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprReachable.OppositeNodeType, seqExprReachable, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionCountReachable(SequenceExpressionCountReachable seqExprCountReachable, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprCountReachable.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprCountReachable.SourceNode, seqExprCountReachable, context);
            if(seqExprCountReachable.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprCountReachable.EdgeType, seqExprCountReachable, context);
            }
            if(seqExprCountReachable.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprCountReachable.OppositeNodeType, seqExprCountReachable, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionBoundedReachable(SequenceExpressionBoundedReachable seqExprBoundedReachable, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprBoundedReachable.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprBoundedReachable.SourceNode, seqExprBoundedReachable, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprBoundedReachable.Depth, seqExprBoundedReachable, context);
            if(seqExprBoundedReachable.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprBoundedReachable.EdgeType, seqExprBoundedReachable, context);
            }
            if(seqExprBoundedReachable.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprBoundedReachable.OppositeNodeType, seqExprBoundedReachable, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionBoundedReachableWithRemainingDepth(SequenceExpressionBoundedReachableWithRemainingDepth seqExprBoundedReachableWithRemainingDepth, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprBoundedReachableWithRemainingDepth.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.SourceNode, seqExprBoundedReachableWithRemainingDepth, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.Depth, seqExprBoundedReachableWithRemainingDepth, context);
            if(seqExprBoundedReachableWithRemainingDepth.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.EdgeType, seqExprBoundedReachableWithRemainingDepth, context);
            }
            if(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType, seqExprBoundedReachableWithRemainingDepth, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionCountBoundedReachable(SequenceExpressionCountBoundedReachable seqExprCountBoundedReachable, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprCountBoundedReachable.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprCountBoundedReachable.SourceNode, seqExprCountBoundedReachable, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprCountBoundedReachable.Depth, seqExprCountBoundedReachable, context);
            if(seqExprCountBoundedReachable.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprCountBoundedReachable.EdgeType, seqExprCountBoundedReachable, context);
            }
            if(seqExprCountBoundedReachable.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprCountBoundedReachable.OppositeNodeType, seqExprCountBoundedReachable, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionIsBoundedReachable(SequenceExpressionIsBoundedReachable seqExprIsBoundedReachable, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprIsBoundedReachable.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprIsBoundedReachable.SourceNode, seqExprIsBoundedReachable, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprIsBoundedReachable.EndElement, seqExprIsBoundedReachable, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprIsBoundedReachable.Depth, seqExprIsBoundedReachable, context);
            if(seqExprIsBoundedReachable.EdgeType != null)
            { 
                Console.Write(",");
                PrintSequenceExpression(seqExprIsBoundedReachable.EdgeType, seqExprIsBoundedReachable, context);
            }
            if(seqExprIsBoundedReachable.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprIsBoundedReachable.OppositeNodeType, seqExprIsBoundedReachable, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionIsAdjacentIncident(SequenceExpressionIsAdjacentIncident seqExprIsAdjacentIncident, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprIsAdjacentIncident.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprIsAdjacentIncident.SourceNode, seqExprIsAdjacentIncident, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprIsAdjacentIncident.EndElement, seqExprIsAdjacentIncident, context);
            if(seqExprIsAdjacentIncident.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprIsAdjacentIncident.EdgeType, seqExprIsAdjacentIncident, context);
            }
            if(seqExprIsAdjacentIncident.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprIsAdjacentIncident.OppositeNodeType, seqExprIsAdjacentIncident, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionIsReachable(SequenceExpressionIsReachable seqExprIsReachable, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprIsReachable.FunctionSymbol + "(");
            PrintSequenceExpression(seqExprIsReachable.SourceNode, seqExprIsReachable, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprIsReachable.EndElement, seqExprIsReachable, context);
            if(seqExprIsReachable.EdgeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprIsReachable.EdgeType, seqExprIsReachable, context);
            }
            if(seqExprIsReachable.OppositeNodeType != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprIsReachable.OppositeNodeType, seqExprIsReachable, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionInducedSubgraph(SequenceExpressionInducedSubgraph seqExprInducedSubgraph, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("inducedSubgraph(");
            PrintSequenceExpression(seqExprInducedSubgraph.NodeSet, seqExprInducedSubgraph, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionDefinedSubgraph(SequenceExpressionDefinedSubgraph seqExprDefinedSubgraph, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("definedSubgraph(");
            PrintSequenceExpression(seqExprDefinedSubgraph.EdgeSet, seqExprDefinedSubgraph, context);
            Console.Write(")");
        }

        // potential todo: change code structure in equals any sequence expression, too
        private static void PrintSequenceExpressionEqualsAny(SequenceExpressionEqualsAny seqExprEqualsAny, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprEqualsAny.IncludingAttributes ? "equalsAny(" : "equalsAnyStructurally(");
            PrintSequenceExpression(seqExprEqualsAny.Subgraph, seqExprEqualsAny, context);
            Console.Write(", ");
            PrintSequenceExpression(seqExprEqualsAny.SubgraphSet, seqExprEqualsAny, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionCanonize(SequenceExpressionCanonize seqExprCanonize, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("canonize(");
            PrintSequenceExpression(seqExprCanonize.Graph, seqExprCanonize, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionNameof(SequenceExpressionNameof seqExprNameof, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("nameof(");
            if(seqExprNameof.NamedEntity != null)
                PrintSequenceExpression(seqExprNameof.NamedEntity, seqExprNameof, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionUniqueof(SequenceExpressionUniqueof seqExprUniqueof, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("uniqueof(");
            if(seqExprUniqueof.UniquelyIdentifiedEntity != null)
                PrintSequenceExpression(seqExprUniqueof.UniquelyIdentifiedEntity, seqExprUniqueof, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionTypeof(SequenceExpressionTypeof seqExprTypeof, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("typeof(");
            PrintSequenceExpression(seqExprTypeof.Entity, seqExprTypeof, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionExistsFile(SequenceExpressionExistsFile seqExprExistsFile, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("File::existsFile(");
            PrintSequenceExpression(seqExprExistsFile.Path, seqExprExistsFile, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionImport(SequenceExpressionImport seqExprImport, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("File::import(");
            PrintSequenceExpression(seqExprImport.Path, seqExprImport, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionCopy(SequenceExpressionCopy seqExprCopy, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprCopy.Deep ? "copy(" : "clone(");
            PrintSequenceExpression(seqExprCopy.ObjectToBeCopied, seqExprCopy, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathMin(SequenceExpressionMathMin seqExprMathMin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::min(");
            PrintSequenceExpression(seqExprMathMin.Left, seqExprMathMin, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprMathMin.Right, seqExprMathMin, context);
            Console.Write(")"); ;
        }

        private static void PrintSequenceExpressionMathMax(SequenceExpressionMathMax seqExprMathMax, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::max(");
            PrintSequenceExpression(seqExprMathMax.Left, seqExprMathMax, context);
            Console.Write(",");
            PrintSequenceExpression(seqExprMathMax.Right, seqExprMathMax, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathAbs(SequenceExpressionMathAbs seqExprMathAbs, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::abs(");
            PrintSequenceExpression(seqExprMathAbs.Argument, seqExprMathAbs, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathCeil(SequenceExpressionMathCeil seqExprMathCeil, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::ceil(");
            PrintSequenceExpression(seqExprMathCeil.Argument, seqExprMathCeil, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathFloor(SequenceExpressionMathFloor seqExprMathFloor, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::floor(");
            PrintSequenceExpression(seqExprMathFloor.Argument, seqExprMathFloor, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathRound(SequenceExpressionMathRound seqExprMathRound, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::round(");
            PrintSequenceExpression(seqExprMathRound.Argument, seqExprMathRound, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathTruncate(SequenceExpressionMathTruncate seqExprMathTruncate, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::truncate(");
            PrintSequenceExpression(seqExprMathTruncate.Argument, seqExprMathTruncate, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathSqr(SequenceExpressionMathSqr seqExprMathSqr, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::sqr(");
            PrintSequenceExpression(seqExprMathSqr.Argument, seqExprMathSqr, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathSqrt(SequenceExpressionMathSqrt seqExprMathSqrt, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::sqrt(");
            PrintSequenceExpression(seqExprMathSqrt.Argument, seqExprMathSqrt, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathPow(SequenceExpressionMathPow seqExprPow, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::pow(");
            if(seqExprPow.Left != null)
            {
                PrintSequenceExpression(seqExprPow.Left, seqExprPow, context);
                Console.Write(",");
            }
            Console.Write(seqExprPow.Right);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathLog(SequenceExpressionMathLog seqExprMathLog, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::log(");
            PrintSequenceExpression(seqExprMathLog.Left, seqExprMathLog, context);
            if(seqExprMathLog.Right != null)
            {
                Console.Write(",");
                PrintSequenceExpression(seqExprMathLog.Right, seqExprMathLog, context);
            }
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathSgn(SequenceExpressionMathSgn seqExprMathSgn, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::sgn(");
            PrintSequenceExpression(seqExprMathSgn.Argument, seqExprMathSgn, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathSin(SequenceExpressionMathSin seqExprMathSin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::sin(");
            PrintSequenceExpression(seqExprMathSin.Argument, seqExprMathSin, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathCos(SequenceExpressionMathCos seqExprMathCos, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::cos(");
            PrintSequenceExpression(seqExprMathCos.Argument, seqExprMathCos, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathTan(SequenceExpressionMathTan seqExprMathTan, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::tan(");
            PrintSequenceExpression(seqExprMathTan.Argument, seqExprMathTan, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathArcSin(SequenceExpressionMathArcSin seqExprMathArcSin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::arcsin(");
            PrintSequenceExpression(seqExprMathArcSin.Argument, seqExprMathArcSin, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathArcCos(SequenceExpressionMathArcCos seqExprMathArcCos, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::arccos(");
            PrintSequenceExpression(seqExprMathArcCos.Argument, seqExprMathArcCos, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathArcTan(SequenceExpressionMathArcTan seqExprMathArcTan, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::arctan(");
            PrintSequenceExpression(seqExprMathArcTan.Argument, seqExprMathArcTan, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionMathPi(SequenceExpressionMathPi seqExprMathPi, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::pi()");
        }

        private static void PrintSequenceExpressionMathE(SequenceExpressionMathE seqExprMathE, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::e()");
        }

        private static void PrintSequenceExpressionMathByteMin(SequenceExpressionMathByteMin seqExprMathByteMin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::byteMin()");
        }

        private static void PrintSequenceExpressionMathByteMax(SequenceExpressionMathByteMax seqExprMathByteMax, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::byteMax()");
        }

        private static void PrintSequenceExpressionMathShortMin(SequenceExpressionMathShortMin seqExprMathShortMin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::shortMin()");
        }

        private static void PrintSequenceExpressionMathShortMax(SequenceExpressionMathShortMax seqExprMathShortMax, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::shortMax()");
        }

        private static void PrintSequenceExpressionMathIntMin(SequenceExpressionMathIntMin seqExprMathIntMin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::intMin()");
        }

        private static void PrintSequenceExpressionMathIntMax(SequenceExpressionMathIntMax seqExprMathIntMax, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::intMax()");
        }

        private static void PrintSequenceExpressionMathLongMin(SequenceExpressionMathLongMin seqExprMathLongMin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::longMin()");
        }

        private static void PrintSequenceExpressionMathLongMax(SequenceExpressionMathLongMax seqExprMathLongMax, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::longMax()");
        }

        private static void PrintSequenceExpressionMathFloatMin(SequenceExpressionMathFloatMin seqExprMathFloatMin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::floatMin()");
        }

        private static void PrintSequenceExpressionMathFloatMax(SequenceExpressionMathFloatMax seqExprMathFloatMax, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::floatMax()");
        }

        private static void PrintSequenceExpressionMathDoubleMin(SequenceExpressionMathDoubleMin seqExprMathDoubleMin, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::doubleMin()");
        }

        private static void PrintSequenceExpressionMathDoubleMax(SequenceExpressionMathDoubleMax seqExprMathDoubleMax, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("Math::doubleMax()");
        }

        private static void PrintSequenceExpressionRuleQuery(SequenceExpressionRuleQuery seqExprRuleQuery, SequenceBase parent, PrintSequenceContext context)
        {
            if(seqExprRuleQuery == context.highlightSeq)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprRuleQuery.Symbol, HighlightingMode.Focus);
            else
                PrintSequence(seqExprRuleQuery.RuleCall, seqExprRuleQuery, context);
        }

        private static void PrintSequenceExpressionMultiRuleQuery(SequenceExpressionMultiRuleQuery seqExprMultiRuleQuery, SequenceBase parent, PrintSequenceContext context)
        {
            if(seqExprMultiRuleQuery == context.highlightSeq)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprMultiRuleQuery.Symbol, HighlightingMode.Focus);
            else
            {
                Console.Write("[?[");
                bool first = true;
                foreach(Sequence rule in seqExprMultiRuleQuery.MultiRuleCall.Sequences)
                {
                    if(first)
                        first = false;
                    else
                        Console.Write(",");
                    PrintSequence(rule, seqExprMultiRuleQuery, context);
                }
                Console.Write("]");
                foreach(SequenceFilterCallBase filterCall in seqExprMultiRuleQuery.MultiRuleCall.Filters)
                {
                    PrintSequenceFilterCall(filterCall, seqExprMultiRuleQuery.MultiRuleCall, context);
                }
                Console.Write("\\<class " + seqExprMultiRuleQuery.MatchClass + ">");
                Console.Write("]");
            }
        }

        private static void PrintSequenceExpressionMappingClause(SequenceExpressionMappingClause seqExprMappingClause, SequenceBase parent, PrintSequenceContext context)
        {
            bool highlight = false;
            foreach(Sequence seqChild in seqExprMappingClause.MultiRulePrefixedSequence.Children)
            {
                if(seqChild == context.highlightSeq)
                    highlight = true;
            }
            bool succesBackup = context.success;
            if(highlight)
                context.success = true;

            if(seqExprMappingClause == context.highlightSeq)
                WorkaroundManager.Workaround.PrintHighlighted(seqExprMappingClause.Symbol, HighlightingMode.Focus);
            else
            {
                Console.Write("[:");
                PrintChildren(seqExprMappingClause.MultiRulePrefixedSequence, context);
                foreach(SequenceFilterCallBase filterCall in seqExprMappingClause.MultiRulePrefixedSequence.Filters)
                {
                    PrintSequenceFilterCall(filterCall, seqExprMappingClause.MultiRulePrefixedSequence, context);
                }
                Console.Write(":]");
            }

            context.success = succesBackup;
        }

        private static void PrintSequenceExpressionScan(SequenceExpressionScan seqExprScan, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("scan");
            if(seqExprScan.ResultType != null)
                Console.Write("<" + seqExprScan.ResultType + ">");
            Console.Write("(");
            PrintSequenceExpression(seqExprScan.StringExpr, seqExprScan, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionTryScan(SequenceExpressionTryScan seqExprTryScan, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write("tryscan");
            if(seqExprTryScan.ResultType != null)
                Console.Write("<" + seqExprTryScan.ResultType + ">");
            Console.Write("(");
            PrintSequenceExpression(seqExprTryScan.StringExpr, seqExprTryScan, context);
            Console.Write(")");
        }

        private static void PrintSequenceExpressionFunctionCall(SequenceExpressionFunctionCall seqExprFunctionCall, SequenceBase parent, PrintSequenceContext context)
        {
            Console.Write(seqExprFunctionCall.Name);
            if(seqExprFunctionCall.ArgumentExpressions.Length > 0)
            {
                Console.Write("(");
                for(int i = 0; i < seqExprFunctionCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(seqExprFunctionCall.ArgumentExpressions[i], seqExprFunctionCall, context);
                    if(i != seqExprFunctionCall.ArgumentExpressions.Length - 1)
                        Console.Write(",");
                }
                Console.Write(")");
            }
        }

        private static void PrintSequenceExpressionFunctionMethodCall(SequenceExpressionFunctionMethodCall seqExprFunctionMethodCall, SequenceBase parent, PrintSequenceContext context)
        {
            PrintSequenceExpression(seqExprFunctionMethodCall.TargetExpr, seqExprFunctionMethodCall, context);
            Console.Write(".");
            PrintSequenceExpressionFunctionCall(seqExprFunctionMethodCall, parent, context);
        }
    }
}
