/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    class UserProxyChoiceMenu
    {
        readonly IDebuggerEnvironment env;

        UserChoiceMenu chooseDirectionMenu = new UserChoiceMenu(UserChoiceMenuNames.ChooseDirectionMenu, new string[] {
            "chooseLeft", "chooseRight", "choosePreselectedBranch" });

        UserChoiceMenu chooseSequenceMenu = new UserChoiceMenu(UserChoiceMenuNames.ChooseSequenceMenu, new string[] {
            "chooseNumberOfSequence", "chooseEnterNumberOfSequence", "choosePreselected", "choosePreselectedAndSkipRemaining" });

        UserChoiceMenu chooseSequenceParallelMenu = new UserChoiceMenu(UserChoiceMenuNames.ChooseSequenceParallelMenu, new string[] {
            "chooseNumberOfSequence", "chooseEnterNumberOfSequence", "choosePreselected" });

        UserChoiceMenu choosePointMenu = new UserChoiceMenu(UserChoiceMenuNames.ChoosePointMenu, new string[] {
            "chooseEnterPoint", "choosePreselected" });

        UserChoiceMenu chooseMatchSomeFromSetMenu = new UserChoiceMenu(UserChoiceMenuNames.ChooseMatchSomeFromSetMenu, new string[] {
            "chooseNumberOfMatch", "chooseEnterNumberOfMatch", "choosePreselectedMatch" });

        UserChoiceMenu chooseMatchMenu = new UserChoiceMenu(UserChoiceMenuNames.ChooseMatchMenu, new string[] {
            "chooseNumberOfMatch", "chooseEnterNumberOfMatch", "choosePreselectedMatch" });

        UserChoiceMenu chooseValueMenu = new UserChoiceMenu(UserChoiceMenuNames.ChooseValueMenu, new string[] {
            "chooseValueAbort", "chooseValueRetry" });

        public UserProxyChoiceMenu(IDebuggerEnvironment env)
        {
            this.env = env;
        }

        public int ChooseDirection(int direction, Sequence seq)
        {
            env.PrintHighlightedUserDialog("Please choose: Which branch to execute first?", HighlightingMode.Choicepoint);
            env.Write(" Random has chosen " + (direction == 0 ? "(l)eft" : "(r)ight") + ".");
            env.PrintInstructions(chooseDirectionMenu, "Press ", ".");

            do
            {
                switch(env.LetUserChoose(chooseDirectionMenu))
                {
                case 'l':
                    env.WriteLine();
                    return 0;
                case 'r':
                    env.WriteLine();
                    return 1;
                case 's':
                case 'n':
                case 'd':
                    env.WriteLine();
                    return direction;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        public void ChooseSequencePrintHeader(int seqToExecute)
        {
            env.PrintHighlightedUserDialog("Please choose: Which sequence to execute?", HighlightingMode.Choicepoint);
            env.WriteLine(" Pre-selecting sequence " + seqToExecute + " chosen by random.");
            env.PrintInstructions(chooseSequenceMenu, "Press ", ".");
        }

        public bool ChooseSequence(ref int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            do
            {
                char character = env.LetUserChoose(chooseSequenceMenu);
                switch(character)
                {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    int num = character - '0';
                    if(num >= sequences.Count)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                        break;
                    }
                    seqToExecute = num;
                    return false;
                case 'e':
                    num = env.ShowMsgAskForIntegerNumber("Enter number of sequence to show");
                    if(num < 0 || num >= sequences.Count)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                        break;
                    }
                    seqToExecute = num;
                    return false;
                case 's':
                case 'n':
                case 'd':
                    return true;
                case 'u':
                case 'o':
                    seq.Skip = true; // skip remaining rules (reset after exection of seq)
                    return true;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        public void ChooseSequenceParallelPrintHeader(int seqToExecute)
        {
            env.PrintHighlightedUserDialog("Please choose: Which sequence to execute in the debugger (others are executed in parallel outside)?", HighlightingMode.Choicepoint);
            env.WriteLine(" Pre-selecting first sequence " + seqToExecute + ".");
            env.PrintInstructions(chooseSequenceParallelMenu, "Press ", ".");
        }

        public bool ChooseSequence(ref int seqToExecute, List<Sequence> sequences, SequenceParallel seq)
        {
            do
            {
                char character = env.LetUserChoose(chooseSequenceParallelMenu);
                switch(character)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        int num = character - '0';
                        if(num >= sequences.Count)
                        {
                            env.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        return false;
                    case 'e':
                        num = env.ShowMsgAskForIntegerNumber("Enter number of sequence to show");
                        if(num < 0 || num >= sequences.Count)
                        {
                            env.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        return false;
                    case 's':
                    case 'n':
                    case 'd':
                        return true;
                    default:
                        throw new Exception("Internal error");
                }
            }
            while(true);
        }

        public void ChoosePointPrintHeader(double pointToExecute)
        {
            env.PrintHighlightedUserDialog("Please choose: Which point in the interval series (corresponding to a sequence) to show?", HighlightingMode.Choicepoint);
            env.WriteLine(" Pre-selecting point " + pointToExecute + " chosen by random.");
            env.PrintInstructions(choosePointMenu, "Press ", ".");
        }

        public bool ChoosePoint(ref double pointToExecute, SequenceWeightedOne seq)
        {
            do
            {
                char character = env.LetUserChoose(choosePointMenu);
                switch(character)
                {
                case 'e':
                    double num = env.ShowMsgAskForFloatingPointNumber("Enter point in interval series of sequence to show");
                    if(num < 0.0 || num > seq.Numbers[seq.Numbers.Count - 1])
                    {
                        env.WriteLine("You must specify a floating point number between 0.0 and " + seq.Numbers[seq.Numbers.Count - 1] + "!");
                        break;
                    }
                    pointToExecute = num;
                    return false;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        public void ChooseMatchSomeFromSetPrintHeader(int totalMatchToExecute)
        {
            env.PrintHighlightedUserDialog("Please choose: Which match to execute?", HighlightingMode.Choicepoint);
            env.WriteLine(" Pre-selecting match " + totalMatchToExecute + " chosen by random.");
            env.PrintInstructions(chooseMatchSomeFromSetMenu, " Press ", ".");
        }

        public bool ChooseMatch(ref int totalMatchToExecute, SequenceSomeFromSet seq)
        {
            do
            {
                char character = env.LetUserChoose(chooseMatchSomeFromSetMenu);
                switch(character)
                {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    int num = character - '0';
                    if(num >= seq.NumTotalMatches)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                        break;
                    }
                    totalMatchToExecute = num;
                    return false;
                case 'e':
                    num = env.ShowMsgAskForIntegerNumber("Enter number of rule to show");
                    if(num < 0 || num >= seq.NumTotalMatches)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                        break;
                    }
                    totalMatchToExecute = num;
                    return false;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        public void ChooseMatchPrintHeader(int numFurtherMatchesToApply)
        {
            env.PrintHighlightedUserDialog("Please choose: Which match to apply?", HighlightingMode.Choicepoint);
            env.WriteLine(" Showing the match chosen by random (" + numFurtherMatchesToApply + " following).");
            env.PrintInstructions(chooseMatchMenu, "Press ", ".");
        }

        public bool ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq, out int newMatchToRewrite)
        {
            newMatchToRewrite = matchToApply;

            do
            {
                char character = env.LetUserChoose(chooseMatchMenu);
                switch(character)
                {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    int num = character - '0';
                    if(num >= matches.Count)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                        break;
                    }
                    newMatchToRewrite = num;
                    return false;
                case 'e':
                    num = env.ShowMsgAskForIntegerNumber("Enter number of match to show");
                    if(num < 0 || num >= matches.Count)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                        break;
                    }
                    newMatchToRewrite = num;
                    return false;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    throw new Exception("Internal error");
                }
            }
            while(true);
        }

        public int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            do
            {
                int num = env.ShowMsgAskForIntegerNumber("Enter number in range [0.." + upperBound + "[ or press enter to use " + randomNumber, randomNumber);
                if(num < 0 || num >= upperBound)
                {
                    env.WriteLine("You must specify a number between 0 and " + (upperBound - 1) + "!");
                    continue;
                }
                return num;
            }
            while(true);
        }

        public double ChooseRandomNumber(double randomNumber, Sequence seq)
        {
            do
            {
                double num = env.ShowMsgAskForFloatingPointNumber("Enter number in range [0.0 .. 1.0[ or press enter to use " + randomNumber, randomNumber);
                if(num < 0.0 || num >= 1.0)
                {
                    env.WriteLine("You must specify a number between 0.0 and 1.0 exclusive !");
                    continue;
                }
                return num;
            }
            while(true);
        }

        public object ChooseValue(string type, Sequence seq, INamedGraph graph)
        {
            object value = env.Askfor(type, graph);

            while(value == null) // bad input case
            {
                env.PrintInstructions(chooseValueMenu, "How to proceed? ", ": ");

                switch(env.LetUserChoose(chooseValueMenu))
                {
                case 'a':
                    env.WriteLine();
                    return null;
                case 'r':
                    env.WriteLine();
                    value = env.Askfor(type, graph);
                    break;
                default:
                    throw new Exception("Internal error");
                }
            }

            return value;
        }
    }
}
