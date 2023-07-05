/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    static class UserChoiceMenu
    {
        public static int ChooseDirection(PrintSequenceContext context, IDebuggerEnvironment env, int direction, Sequence seq)
        {
            ConsoleUI.consoleOut.PrintHighlighted("Please choose: Which branch to execute first?", HighlightingMode.Choicepoint);
            ConsoleUI.outWriter.Write(" (l)eft or (r)ight or (s)/(n)/(d) to continue with random choice.  (Random has chosen " + (direction == 0 ? "(l)eft" : "(r)ight") + ") ");

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'l':
                    ConsoleUI.outWriter.WriteLine();
                    return 0;
                case 'r':
                    ConsoleUI.outWriter.WriteLine();
                    return 1;
                case 's':
                case 'n':
                case 'd':
                    ConsoleUI.outWriter.WriteLine();
                    return direction;
                default:
                    ConsoleUI.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (l)eft branch, (r)ight branch, (s)/(n)/(d) to continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChooseSequencePrintHeader(PrintSequenceContext context, int seqToExecute)
        {
            ConsoleUI.consoleOut.PrintHighlighted("Please choose: Which sequence to execute?", HighlightingMode.Choicepoint);
            ConsoleUI.outWriter.WriteLine(" Pre-selecting sequence " + seqToExecute + " chosen by random.");
            ConsoleUI.outWriter.WriteLine("Press (0)...(9) to pre-select the corresponding sequence or (e) to enter the number of the sequence to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected sequence and continue."
                                + " Pressing (u) or (o) works like (s)/(n)/(d) but does not ask for the remaining contained sequences.");
        }

        public static bool ChooseSequence(IDebuggerEnvironment env, ref int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
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
                    int num = key.KeyChar - '0';
                    if(num >= sequences.Count)
                    {
                        ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                        break;
                    }
                    seqToExecute = num;
                    return false;
                case 'e':
                    ConsoleUI.outWriter.Write("Enter number of sequence to show: ");
                    String numStr = ConsoleUI.inReader.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= sequences.Count)
                        {
                            ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        return false;
                    }
                    ConsoleUI.outWriter.WriteLine("You must enter a valid integer number!");
                    break;
                case 's':
                case 'n':
                case 'd':
                    return true;
                case 'u':
                case 'o':
                    seq.Skip = true; // skip remaining rules (reset after exection of seq)
                    return true;
                default:
                    ConsoleUI.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue, (u)/(o) to commit and skip remaining choices allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChooseSequenceParallelPrintHeader(PrintSequenceContext context, int seqToExecute)
        {
            ConsoleUI.consoleOut.PrintHighlighted("Please choose: Which sequence to execute in the debugger (others are executed in parallel outside)?", HighlightingMode.Choicepoint);
            ConsoleUI.outWriter.WriteLine(" Pre-selecting first sequence " + seqToExecute + ".");
            ConsoleUI.outWriter.WriteLine("Press (0)...(9) to pre-select the corresponding sequence or (e) to enter the number of the sequence to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected sequence and continue.");
        }

        public static bool ChooseSequence(IDebuggerEnvironment env, ref int seqToExecute, List<Sequence> sequences, SequenceParallel seq)
        {
            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
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
                        int num = key.KeyChar - '0';
                        if(num >= sequences.Count)
                        {
                            ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        return false;
                    case 'e':
                        ConsoleUI.outWriter.Write("Enter number of sequence to show: ");
                        String numStr = ConsoleUI.inReader.ReadLine();
                        if(int.TryParse(numStr, out num))
                        {
                            if(num < 0 || num >= sequences.Count)
                            {
                                ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                                break;
                            }
                            seqToExecute = num;
                            return false;
                        }
                        ConsoleUI.outWriter.WriteLine("You must enter a valid integer number!");
                        break;
                    case 's':
                    case 'n':
                    case 'd':
                        return true;
                    default:
                        ConsoleUI.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue allowed! ");
                        break;
                }
            }
            while(true);
        }

        public static void ChoosePointPrintHeader(PrintSequenceContext context, double pointToExecute)
        {
            ConsoleUI.consoleOut.PrintHighlighted("Please choose: Which point in the interval series (corresponding to a sequence) to execute?", HighlightingMode.Choicepoint);
            ConsoleUI.outWriter.WriteLine(" Pre-selecting point " + pointToExecute + " chosen by random.");
            ConsoleUI.outWriter.WriteLine("Press (e) to enter a point in the interval series of the sequence to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected sequence and continue.");
        }

        public static bool ChoosePoint(IDebuggerEnvironment env, ref double pointToExecute, SequenceWeightedOne seq)
        {
            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'e':
                    double num;
                    ConsoleUI.outWriter.Write("Enter point in interval series of sequence to show: ");
                    String numStr = ConsoleUI.inReader.ReadLine();
                    if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out num))
                    {
                        if(num < 0.0 || num > seq.Numbers[seq.Numbers.Count - 1])
                        {
                            ConsoleUI.outWriter.WriteLine("You must specify a floating point number between 0.0 and " + seq.Numbers[seq.Numbers.Count - 1] + "!");
                            break;
                        }
                        pointToExecute = num;
                        return false;
                    }
                    ConsoleUI.outWriter.WriteLine("You must enter a valid floating point number!");
                    break;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    ConsoleUI.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (e)nter number and (s)/(n)/(d) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChooseMatchSomeFromSetPrintHeader(PrintSequenceContext context, int totalMatchToExecute)
        {
            ConsoleUI.consoleOut.PrintHighlighted("Please choose: Which match to execute?", HighlightingMode.Choicepoint);
            ConsoleUI.outWriter.WriteLine(" Pre-selecting match " + totalMatchToExecute + " chosen by random.");
            ConsoleUI.outWriter.WriteLine("Press (0)...(9) to pre-select the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected match and continue.");
        }

        public static bool ChooseMatch(IDebuggerEnvironment env, ref int totalMatchToExecute, SequenceSomeFromSet seq)
        {
            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
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
                    int num = key.KeyChar - '0';
                    if(num >= seq.NumTotalMatches)
                    {
                        ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                        break;
                    }
                    totalMatchToExecute = num;
                    return false;
                case 'e':
                    ConsoleUI.outWriter.Write("Enter number of rule to show: ");
                    String numStr = ConsoleUI.inReader.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= seq.NumTotalMatches)
                        {
                            ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                            break;
                        }
                        totalMatchToExecute = num;
                        return false;
                    }
                    ConsoleUI.outWriter.WriteLine("You must enter a valid integer number!");
                    break;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    ConsoleUI.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChooseMatchPrintHeader(PrintSequenceContext context, int numFurtherMatchesToApply)
        {
            ConsoleUI.consoleOut.PrintHighlighted("Please choose: Which match to apply?", HighlightingMode.Choicepoint);
            ConsoleUI.outWriter.WriteLine(" Showing the match chosen by random. (" + numFurtherMatchesToApply + " following)");
            ConsoleUI.outWriter.WriteLine("Press (0)...(9) to show the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) or (d) to commit to the currently shown match and continue.");
        }

        public static bool ChooseMatch(IDebuggerEnvironment env, int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq, out int newMatchToRewrite)
        {
            newMatchToRewrite = matchToApply;

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
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
                    int num = key.KeyChar - '0';
                    if(num >= matches.Count)
                    {
                        ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                        break;
                    }
                    newMatchToRewrite = num;
                    return false;
                case 'e':
                    ConsoleUI.outWriter.Write("Enter number of match to show: ");
                    String numStr = ConsoleUI.inReader.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= matches.Count)
                        {
                            ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                            break;
                        }
                        newMatchToRewrite = num;
                        return false;
                    }
                    ConsoleUI.outWriter.WriteLine("You must enter a valid integer number!");
                    return false;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    ConsoleUI.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            ConsoleUI.outWriter.Write("Enter number in range [0.." + upperBound + "[ or press enter to use " + randomNumber + ": ");

            do
            {
                String numStr = ConsoleUI.inReader.ReadLine();
                if(numStr == "")
                    return randomNumber;
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < 0 || num >= upperBound)
                    {
                        ConsoleUI.outWriter.WriteLine("You must specify a number between 0 and " + (upperBound - 1) + "!");
                        continue;
                    }
                    return num;
                }
                ConsoleUI.outWriter.WriteLine("You must enter a valid integer number!");
            }
            while(true);
        }

        public static double ChooseRandomNumber(double randomNumber, Sequence seq)
        {
            ConsoleUI.outWriter.Write("Enter number in range [0.0 .. 1.0[ or press enter to use " + randomNumber + ": ");

            do
            {
                String numStr = ConsoleUI.inReader.ReadLine();
                if(numStr == "")
                    return randomNumber;
                double num;
                if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out num))
                {
                    if(num < 0.0 || num >= 1.0)
                    {
                        ConsoleUI.outWriter.WriteLine("You must specify a number between 0.0 and 1.0 exclusive !");
                        continue;
                    }
                    return num;
                }
                ConsoleUI.outWriter.WriteLine("You must enter a valid double number!");
            }
            while(true);
        }

        public static object ChooseValue(IDebuggerEnvironment env, string type, Sequence seq, INamedGraph graph)
        {
            object value = env.Askfor(type, graph);

            while(value == null) // bad input case
            {
                ConsoleUI.outWriter.Write("How to proceed? (a)bort user choice (-> value null) or (r)etry:");

read_again:
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'a':
                    ConsoleUI.outWriter.WriteLine();
                    return null;
                case 'r':
                    ConsoleUI.outWriter.WriteLine();
                    value = env.Askfor(type, graph);
                    break;
                default:
                    ConsoleUI.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (a)bort user choice or (r)etry allowed! ");
                    goto read_again;
                }
            }

            return value;
        }
    }
}
