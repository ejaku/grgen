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
        private static void PrintSequence(Sequence seq, Sequence parent, PrintSequenceContext context)
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

        private static void PrintSequenceBinary(SequenceBinary seqBin, Sequence parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0 && seqBin.Random)
            {
                int cpPosCounter = context.cpPosCounter;
                ++context.cpPosCounter;
                PrintSequence(seqBin.Left, seqBin, context);
                PrintChoice(seqBin, context);
                Console.Write(seqBin.Symbol + " ");
                PrintSequence(seqBin.Right, seqBin, context);
                return;
            }

            if(seqBin == context.highlightSeq && context.choice)
            {
                WorkaroundManager.Workaround.PrintHighlighted("(l)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Left, seqBin, context);
                WorkaroundManager.Workaround.PrintHighlighted("(l) " + seqBin.Symbol + " (r)", HighlightingMode.Choicepoint);
                PrintSequence(seqBin.Right, seqBin, context);
                WorkaroundManager.Workaround.PrintHighlighted("(r)", HighlightingMode.Choicepoint);
                return;
            }

            PrintSequence(seqBin.Left, seqBin, context);
            Console.Write(" " + seqBin.Symbol + " ");
            PrintSequence(seqBin.Right, seqBin, context);
        }

        private static void PrintSequenceIfThen(SequenceIfThen seqIfThen, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("if{");
            PrintSequence(seqIfThen.Left, seqIfThen, context);
            Console.Write(";");
            PrintSequence(seqIfThen.Right, seqIfThen, context);
            Console.Write("}");
        }

        private static void PrintSequenceNot(SequenceNot seqNot, Sequence parent, PrintSequenceContext context)
        {
            Console.Write(seqNot.Symbol);
            PrintSequence(seqNot.Seq, seqNot, context);
        }

        private static void PrintSequenceIterationMin(SequenceIterationMin seqMin, Sequence parent, PrintSequenceContext context)
        {
            PrintSequence(seqMin.Seq, seqMin, context);
            Console.Write("[" + seqMin.Min + ":*]");
        }

        private static void PrintSequenceIterationMinMax(SequenceIterationMinMax seqMinMax, Sequence parent, PrintSequenceContext context)
        {
            PrintSequence(seqMinMax.Seq, seqMinMax, context);
            Console.Write("[" + seqMinMax.Min + ":" + seqMinMax.Max + "]");
        }

        private static void PrintSequenceTransaction(SequenceTransaction seqTrans, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("<");
            PrintSequence(seqTrans.Seq, seqTrans, context);
            Console.Write(">");
        }

        private static void PrintSequenceBacktrack(SequenceBacktrack seqBack, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("<<");
            PrintSequence(seqBack.Rule, seqBack, context);
            Console.Write(";;");
            PrintSequence(seqBack.Seq, seqBack, context);
            Console.Write(">>");
        }

        private static void PrintSequencePause(SequencePause seqPause, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("/");
            PrintSequence(seqPause.Seq, seqPause, context);
            Console.Write("/");
        }

        private static void PrintSequenceForContainer(SequenceForContainer seqFor, Sequence parent, PrintSequenceContext context)
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

        private static void PrintSequenceForIntegerRange(SequenceForIntegerRange seqFor, Sequence parent, PrintSequenceContext context)
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

        private static void PrintSequenceForIndexAccessEquality(SequenceForIndexAccessEquality seqFor, Sequence parent, PrintSequenceContext context)
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

        private static void PrintSequenceForIndexAccessOrdering(SequenceForIndexAccessOrdering seqFor, Sequence parent, PrintSequenceContext context)
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

        private static void PrintSequenceForFunction(SequenceForFunction seqFor, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            Console.Write(" in ");
            Console.Write(seqFor.FunctionSymbol + ";");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceForMatch(SequenceForMatch seqFor, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("for{");
            Console.Write(seqFor.Var.Name);
            Console.Write(" in [?");
            PrintSequence(seqFor.Rule, seqFor, context);
            Console.Write("]; ");
            PrintSequence(seqFor.Seq, seqFor, context);
            Console.Write("}");
        }

        private static void PrintSequenceExecuteInSubgraph(SequenceExecuteInSubgraph seqExecInSub, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("in ");
            Console.Write(seqExecInSub.SubgraphVar.Name);
            if(seqExecInSub.AttributeName != null)
                Console.Write("." + seqExecInSub.AttributeName);
            Console.Write(" {");
            PrintSequence(seqExecInSub.Seq, seqExecInSub, context);
            Console.Write("}");
        }

        private static void PrintSequenceIfThenElse(SequenceIfThenElse seqIf, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("if{");
            PrintSequence(seqIf.Condition, seqIf, context);
            Console.Write(";");
            PrintSequence(seqIf.TrueCase, seqIf, context);
            Console.Write(";");
            PrintSequence(seqIf.FalseCase, seqIf, context);
            Console.Write("}");
        }

        private static void PrintSequenceNAry(SequenceNAry seqN, Sequence parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0)
            {
                PrintChoice(seqN, context);
                ++context.cpPosCounter;
                Console.Write((seqN.Choice ? "$%" : "$") + seqN.Symbol + "(");
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
                WorkaroundManager.Workaround.PrintHighlighted("$%" + seqN.Symbol + "(", HighlightingMode.Choicepoint);
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

                    Sequence highlightSeqBackup = context.highlightSeq;
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

            Console.Write((seqN.Choice ? "$%" : "$") + seqN.Symbol + "(");
            PrintChildren(seqN, context);
            Console.Write(")");
        }

        private static void PrintSequenceWeightedOne(SequenceWeightedOne seqWeighted, Sequence parent, PrintSequenceContext context)
        {
            if(context.cpPosCounter >= 0)
            {
                PrintChoice(seqWeighted, context);
                ++context.cpPosCounter;
                Console.Write((seqWeighted.Choice ? "$%" : "$") + seqWeighted.Symbol + "(");
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
                WorkaroundManager.Workaround.PrintHighlighted("$%" + seqWeighted.Symbol + "(", HighlightingMode.Choicepoint);
                bool first = true;
                for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                {
                    if(first)
                        Console.Write("0.00 ");
                    else
                        Console.Write(" ");
                    if(seqWeighted.Sequences[i] == context.highlightSeq)
                        WorkaroundManager.Workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);

                    Sequence highlightSeqBackup = context.highlightSeq;
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

            Console.Write((seqWeighted.Choice ? "$%" : "$") + seqWeighted.Symbol + "(");
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

        private static void PrintSequenceSomeFromSet(SequenceSomeFromSet seqSome, Sequence parent, PrintSequenceContext context)
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

                    Sequence highlightSeqBackup = context.highlightSeq;
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

        private static void PrintSequenceBreakpointable(Sequence seq, Sequence parent, PrintSequenceContext context)
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
            WorkaroundManager.Workaround.PrintHighlighted(seq.Symbol, mode);
        }

        private static void PrintSequenceAssignSequenceResultToVar(SequenceAssignSequenceResultToVar seqAss, Sequence parent, PrintSequenceContext context)
        {
            Console.Write("(");
            PrintSequence(seqAss.Seq, seqAss, context);
            if(seqAss.SequenceType == SequenceType.OrAssignSequenceResultToVar)
                Console.Write("|>");
            else if(seqAss.SequenceType == SequenceType.AndAssignSequenceResultToVar)
                Console.Write("&>");
            else //if(seqAss.SequenceType==SequenceType.AssignSequenceResultToVar)
                Console.Write("=>");
            Console.Write(seqAss.DestVar.Name);
            Console.Write(")");
        }

        // Choice highlightable user assignments
        private static void PrintSequenceAssignChoiceHighlightable(Sequence seq, Sequence parent, PrintSequenceContext context)
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

        private static void PrintSequenceDefinitionInterpreted(SequenceDefinitionInterpreted seqDef, Sequence parent, PrintSequenceContext context)
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
                if(!first)
                    Console.Write(", ");
                PrintSequence(seqChild, seq, context);
                first = false;
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
    }
}
