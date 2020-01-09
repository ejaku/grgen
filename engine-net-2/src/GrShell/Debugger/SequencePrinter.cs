/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;

using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;
using System.Text;

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
            context.workaround.PrintHighlighted(nestingLevel + ">", HighlightingMode.SequenceStart);
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
            // Binary
            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
            case SequenceType.StrictAnd:
                {
                    SequenceBinary seqBin = (SequenceBinary)seq;

                    if(context.cpPosCounter >= 0 && seqBin.Random)
                    {
                        int cpPosCounter = context.cpPosCounter;
                        ++context.cpPosCounter;
                        PrintSequence(seqBin.Left, seq, context);
                        PrintChoice(seqBin, context);
                        Console.Write(seq.Symbol + " ");
                        PrintSequence(seqBin.Right, seq, context);
                        break;
                    }

                    if(seqBin == context.highlightSeq && context.choice)
                    {
                        context.workaround.PrintHighlighted("(l)", HighlightingMode.Choicepoint);
                        PrintSequence(seqBin.Left, seq, context);
                        context.workaround.PrintHighlighted("(l) " + seq.Symbol + " (r)", HighlightingMode.Choicepoint);
                        PrintSequence(seqBin.Right, seq, context);
                        context.workaround.PrintHighlighted("(r)", HighlightingMode.Choicepoint);
                        break;
                    }

                    PrintSequence(seqBin.Left, seq, context);
                    Console.Write(" " + seq.Symbol + " ");
                    PrintSequence(seqBin.Right, seq, context);
                    break;
                }
            case SequenceType.IfThen:
                {
                    SequenceIfThen seqIfThen = (SequenceIfThen)seq;
                    Console.Write("if{");
                    PrintSequence(seqIfThen.Left, seq, context);
                    Console.Write(";");
                    PrintSequence(seqIfThen.Right, seq, context);
                    Console.Write("}");
                    break;
                }

            // Unary
            case SequenceType.Not:
                {
                    SequenceNot seqNot = (SequenceNot)seq;
                    Console.Write(seq.Symbol);
                    PrintSequence(seqNot.Seq, seq, context);
                    break;
                }
            case SequenceType.IterationMin:
                {
                    SequenceIterationMin seqMin = (SequenceIterationMin)seq;
                    PrintSequence(seqMin.Seq, seq, context);
                    Console.Write("[" + seqMin.Min + ":*]");
                    break;
                }
            case SequenceType.IterationMinMax:
                {
                    SequenceIterationMinMax seqMinMax = (SequenceIterationMinMax)seq;
                    PrintSequence(seqMinMax.Seq, seq, context);
                    Console.Write("[" + seqMinMax.Min + ":" + seqMinMax.Max + "]");
                    break;
                }
            case SequenceType.Transaction:
                {
                    SequenceTransaction seqTrans = (SequenceTransaction)seq;
                    Console.Write("<");
                    PrintSequence(seqTrans.Seq, seq, context);
                    Console.Write(">");
                    break;
                }
            case SequenceType.Backtrack:
                {
                    SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                    Console.Write("<<");
                    PrintSequence(seqBack.Rule, seq, context);
                    Console.Write(";;");
                    PrintSequence(seqBack.Seq, seq, context);
                    Console.Write(">>");
                    break;
                }
            case SequenceType.Pause:
                {
                    SequencePause seqPause = (SequencePause)seq;
                    Console.Write("/");
                    PrintSequence(seqPause.Seq, seq, context);
                    Console.Write("/");
                    break;
                }
            case SequenceType.ForContainer:
                {
                    SequenceForContainer seqFor = (SequenceForContainer)seq;
                    Console.Write("for{");
                    Console.Write(seqFor.Var.Name);
                    if(seqFor.VarDst != null)
                        Console.Write("->" + seqFor.VarDst.Name);
                    Console.Write(" in " + seqFor.Container.Name);
                    Console.Write("; ");
                    PrintSequence(seqFor.Seq, seq, context);
                    Console.Write("}");
                    break;
                }
            case SequenceType.ForIntegerRange:
                {
                    SequenceForIntegerRange seqFor = (SequenceForIntegerRange)seq;
                    Console.Write("for{");
                    Console.Write(seqFor.Var.Name);
                    Console.Write(" in [");
                    Console.Write(seqFor.Left.Symbol);
                    Console.Write(":");
                    Console.Write(seqFor.Right.Symbol);
                    Console.Write("]; ");
                    PrintSequence(seqFor.Seq, seq, context);
                    Console.Write("}");
                    break;
                }
            case SequenceType.ForIndexAccessEquality:
                {
                    SequenceForIndexAccessEquality seqFor = (SequenceForIndexAccessEquality)seq;
                    Console.Write("for{");
                    Console.Write(seqFor.Var.Name);
                    Console.Write(" in {");
                    Console.Write(seqFor.IndexName);
                    Console.Write("==");
                    Console.Write(seqFor.Expr.Symbol);
                    Console.Write("}; ");
                    PrintSequence(seqFor.Seq, seq, context);
                    Console.Write("}");
                    break;
                }
            case SequenceType.ForIndexAccessOrdering:
                {
                    SequenceForIndexAccessOrdering seqFor = (SequenceForIndexAccessOrdering)seq;
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
                    PrintSequence(seqFor.Seq, seq, context);
                    Console.Write("}");
                    break;
                }
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
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;
                    Console.Write("for{");
                    Console.Write(seqFor.Var.Name);
                    Console.Write(" in ");
                    Console.Write(seqFor.FunctionSymbol + ";");
                    PrintSequence(seqFor.Seq, seq, context);
                    Console.Write("}");
                    break;
                }
            case SequenceType.ForMatch:
                {
                    SequenceForMatch seqFor = (SequenceForMatch)seq;
                    Console.Write("for{");
                    Console.Write(seqFor.Var.Name);
                    Console.Write(" in [?");
                    PrintSequence(seqFor.Rule, seq, context);
                    Console.Write("]; ");
                    PrintSequence(seqFor.Seq, seq, context);
                    Console.Write("}");
                    break;
                }
            case SequenceType.ExecuteInSubgraph:
                {
                    SequenceExecuteInSubgraph seqExecInSub = (SequenceExecuteInSubgraph)seq;
                    Console.Write("in ");
                    Console.Write(seqExecInSub.SubgraphVar.Name);
                    if(seqExecInSub.AttributeName != null)
                        Console.Write("." + seqExecInSub.AttributeName);
                    Console.Write(" {");
                    PrintSequence(seqExecInSub.Seq, seq, context);
                    Console.Write("}");
                    break;
                }

            // Ternary
            case SequenceType.IfThenElse:
                {
                    SequenceIfThenElse seqIf = (SequenceIfThenElse)seq;
                    Console.Write("if{");
                    PrintSequence(seqIf.Condition, seq, context);
                    Console.Write(";");
                    PrintSequence(seqIf.TrueCase, seq, context);
                    Console.Write(";");
                    PrintSequence(seqIf.FalseCase, seq, context);
                    Console.Write("}");
                    break;
                }

            // n-ary
            case SequenceType.LazyOrAll:
            case SequenceType.LazyAndAll:
            case SequenceType.StrictOrAll:
            case SequenceType.StrictAndAll:
                {
                    SequenceNAry seqN = (SequenceNAry)seq;

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
                        break;
                    }

                    bool highlight = false;
                    foreach(Sequence seqChild in seqN.Children)
                        if(seqChild == context.highlightSeq)
                            highlight = true;
                    if(highlight && context.choice)
                    {
                        context.workaround.PrintHighlighted("$%" + seqN.Symbol + "(", HighlightingMode.Choicepoint);
                        bool first = true;
                        foreach(Sequence seqChild in seqN.Children)
                        {
                            if(!first)
                                Console.Write(", ");
                            if(seqChild == context.highlightSeq)
                                context.workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);
                            if(context.sequences != null)
                            {
                                for(int i = 0; i < context.sequences.Count; ++i)
                                {
                                    if(seqChild == context.sequences[i])
                                        context.workaround.PrintHighlighted("(" + i + ")", HighlightingMode.Choicepoint);
                                }
                            }

                            Sequence highlightSeqBackup = context.highlightSeq;
                            context.highlightSeq = null; // we already highlighted here
                            PrintSequence(seqChild, seqN, context);
                            context.highlightSeq = highlightSeqBackup;

                            if(seqChild == context.highlightSeq)
                                context.workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                            first = false;
                        }
                        context.workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                        break;
                    }

                    Console.Write((seqN.Choice ? "$%" : "$") + seqN.Symbol + "(");
                    PrintChildren(seqN, context);
                    Console.Write(")");
                    break;
                }

            case SequenceType.WeightedOne:
                {
                    SequenceWeightedOne seqWeighted = (SequenceWeightedOne)seq;

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
                        break;
                    }

                    bool highlight = false;
                    foreach(Sequence seqChild in seqWeighted.Children)
                        if(seqChild == context.highlightSeq)
                            highlight = true;
                    if(highlight && context.choice)
                    {
                        context.workaround.PrintHighlighted("$%" + seqWeighted.Symbol + "(", HighlightingMode.Choicepoint);
                        bool first = true;
                        for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
                        {
                            if(first)
                                Console.Write("0.00 ");
                            else
                                Console.Write(" ");
                            if(seqWeighted.Sequences[i] == context.highlightSeq)
                                context.workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);

                            Sequence highlightSeqBackup = context.highlightSeq;
                            context.highlightSeq = null; // we already highlighted here
                            PrintSequence(seqWeighted.Sequences[i], seqWeighted, context);
                            context.highlightSeq = highlightSeqBackup;

                            if(seqWeighted.Sequences[i] == context.highlightSeq)
                                context.workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                            Console.Write(" ");
                            Console.Write(seqWeighted.Numbers[i]); // todo: format auf 2 nachkommastellen 
                            first = false;
                        }
                        context.workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
                        break;
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
                    break;
                }

            case SequenceType.SomeFromSet:
                {
                    SequenceSomeFromSet seqSome = (SequenceSomeFromSet)seq;

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
                        break;
                    }

                    bool highlight = false;
                    foreach(Sequence seqChild in seqSome.Children)
                        if(seqChild == context.highlightSeq)
                            highlight = true;

                    if(highlight && context.choice)
                    {
                        context.workaround.PrintHighlighted("$%{<", HighlightingMode.Choicepoint);
                        bool first = true;
                        int numCurTotalMatch = 0;
                        foreach(Sequence seqChild in seqSome.Children)
                        {
                            if(!first)
                                Console.Write(", ");
                            if(seqChild == context.highlightSeq)
                                context.workaround.PrintHighlighted(">>", HighlightingMode.Choicepoint);
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
                                context.workaround.PrintHighlighted("<<", HighlightingMode.Choicepoint);
                            first = false;
                        }
                        context.workaround.PrintHighlighted(">}", HighlightingMode.Choicepoint);
                        break;
                    }

                    bool succesBackup = context.success;
                    if(highlight)
                        context.success = true;
                    Console.Write(seqSome.Random ? (seqSome.Choice ? "$%{<" : "${<") : "{<");
                    PrintChildren(seqSome, context);
                    Console.Write(">}");
                    context.success = succesBackup;
                    break;
                }

            // Breakpointable atoms
            case SequenceType.SequenceCall:
            case SequenceType.RuleCall:
            case SequenceType.RuleAllCall:
            case SequenceType.RuleCountAllCall:
            case SequenceType.BooleanComputation:
                {
                    if(context.bpPosCounter >= 0)
                    {
                        PrintBreak((SequenceSpecial)seq, context);
                        Console.Write(seq.Symbol);
                        ++context.bpPosCounter;
                        break;
                    }

                    if(context.cpPosCounter >= 0 && seq is SequenceRandomChoice
                        && ((SequenceRandomChoice)seq).Random)
                    {
                        PrintChoice((SequenceRandomChoice)seq, context);
                        Console.Write(seq.Symbol);
                        ++context.cpPosCounter;
                        break;
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
                    context.workaround.PrintHighlighted(seq.Symbol, mode);
                    break;
                }

            // Unary assignment
            case SequenceType.AssignSequenceResultToVar:
            case SequenceType.OrAssignSequenceResultToVar:
            case SequenceType.AndAssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar seqAss = (SequenceAssignSequenceResultToVar)seq;
                    Console.Write("(");
                    PrintSequence(seqAss.Seq, seq, context);
                    if(seq.SequenceType == SequenceType.OrAssignSequenceResultToVar)
                        Console.Write("|>");
                    else if(seq.SequenceType == SequenceType.AndAssignSequenceResultToVar)
                        Console.Write("&>");
                    else //if(seq.SequenceType==SequenceType.AssignSequenceResultToVar)
                        Console.Write("=>");
                    Console.Write(seqAss.DestVar.Name);
                    Console.Write(")");
                    break;
                }

            // Choice highlightable user assignments
            case SequenceType.AssignUserInputToVar:
            case SequenceType.AssignRandomIntToVar:
            case SequenceType.AssignRandomDoubleToVar:
                {
                    if(context.cpPosCounter >= 0 
                        && (seq is SequenceAssignRandomIntToVar || seq is SequenceAssignRandomDoubleToVar))
                    {
                        PrintChoice((SequenceRandomChoice)seq, context);
                        Console.Write(seq.Symbol);
                        ++context.cpPosCounter;
                        break;
                    }

                    if(seq == context.highlightSeq && context.choice)
                        context.workaround.PrintHighlighted(seq.Symbol, HighlightingMode.Choicepoint);
                    else
                        Console.Write(seq.Symbol);
                    break;
                }

            case SequenceType.SequenceDefinitionInterpreted:
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seq;
                    HighlightingMode mode = HighlightingMode.None;
                    if(seqDef.ExecutionState == SequenceExecutionState.Success)
                        mode = HighlightingMode.LastSuccess;
                    if(seqDef.ExecutionState == SequenceExecutionState.Fail)
                        mode = HighlightingMode.LastFail;
                    context.workaround.PrintHighlighted(seqDef.Symbol + ": ", mode);
                    PrintSequence(seqDef.Seq, seqDef.Seq, context);
                    break;
                }

            // Atoms (assignments)
            case SequenceType.AssignVarToVar:
            case SequenceType.AssignConstToVar:
            case SequenceType.AssignContainerConstructorToVar:
            case SequenceType.DeclareVariable:
                {
                    Console.Write(seq.Symbol);
                    break;
                }

            default:
                {
                    Debug.Assert(false);
                    Console.Write("<UNKNOWN_SEQUENCE_TYPE>");
                    break;
                }
            }

            // print parentheses, if neccessary
            if(parent != null && seq.Precedence < parent.Precedence)
                Console.Write(")");
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
                context.workaround.PrintHighlighted("-%" + context.cpPosCounter + "-:", HighlightingMode.Choicepoint);
            else
                context.workaround.PrintHighlighted("+%" + context.cpPosCounter + "+:", HighlightingMode.Choicepoint);
        }

        private static void PrintBreak(SequenceSpecial seq, PrintSequenceContext context)
        {
            if(seq.Special)
                context.workaround.PrintHighlighted("-%" + context.bpPosCounter + "-:", HighlightingMode.Breakpoint);
            else
                context.workaround.PrintHighlighted("+%" + context.bpPosCounter + "+:", HighlightingMode.Breakpoint);
        }

        private static void PrintListOfMatchesNumbers(PrintSequenceContext context, ref int numCurTotalMatch, int numMatches)
        {
            context.workaround.PrintHighlighted("(", HighlightingMode.Choicepoint);
            bool first = true;
            for(int i = 0; i < numMatches; ++i)
            {
                if(!first)
                    context.workaround.PrintHighlighted(",", HighlightingMode.Choicepoint);
                context.workaround.PrintHighlighted(numCurTotalMatch.ToString(), HighlightingMode.Choicepoint);
                ++numCurTotalMatch;
                first = false;
            }
            context.workaround.PrintHighlighted(")", HighlightingMode.Choicepoint);
        }

        /// <summary>
        /// Called from shell after an debugging abort highlighting the lastly executed rule
        /// </summary>
        public static void PrintSequence(Sequence seq, Sequence highlight, IWorkaround workaround)
        {
            PrintSequenceContext context = new PrintSequenceContext(workaround);
            context.highlightSeq = highlight;
            PrintSequence(seq, context, 0);
            // TODO: what to do if abort came within sequence called from top sequence?
        }
    }
}
