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
    class UserChoiceMenu
    {
        readonly IDebuggerEnvironment env;

        public UserChoiceMenu(IDebuggerEnvironment env)
        {
            this.env = env;
        }

        public int ChooseDirection(int direction, Sequence seq)
        {
            env.consoleOut.PrintHighlighted("Please choose: Which branch to execute first?", HighlightingMode.Choicepoint);
            env.outWriter.Write(" (l)eft or (r)ight or (s)/(n)/(d) to continue with random choice.  (Random has chosen " + (direction == 0 ? "(l)eft" : "(r)ight") + ") ");

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'l':
                    env.outWriter.WriteLine();
                    return 0;
                case 'r':
                    env.outWriter.WriteLine();
                    return 1;
                case 's':
                case 'n':
                case 'd':
                    env.outWriter.WriteLine();
                    return direction;
                default:
                    env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (l)eft branch, (r)ight branch, (s)/(n)/(d) to continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public void ChooseSequencePrintHeader(int seqToExecute)
        {
            env.consoleOut.PrintHighlighted("Please choose: Which sequence to execute?", HighlightingMode.Choicepoint);
            env.outWriter.WriteLine(" Pre-selecting sequence " + seqToExecute + " chosen by random.");
            env.outWriter.WriteLine("Press (0)...(9) to pre-select the corresponding sequence or (e) to enter the number of the sequence to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected sequence and continue."
                                + " Pressing (u) or (o) works like (s)/(n)/(d) but does not ask for the remaining contained sequences.");
        }

        public bool ChooseSequence(ref int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
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
                        env.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                        break;
                    }
                    seqToExecute = num;
                    return false;
                case 'e':
                    env.outWriter.Write("Enter number of sequence to show: ");
                    String numStr = env.inReader.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= sequences.Count)
                        {
                            env.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        return false;
                    }
                    env.outWriter.WriteLine("You must enter a valid integer number!");
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
                    env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue, (u)/(o) to commit and skip remaining choices allowed! ");
                    break;
                }
            }
            while(true);
        }

        public void ChooseSequenceParallelPrintHeader(int seqToExecute)
        {
            env.consoleOut.PrintHighlighted("Please choose: Which sequence to execute in the debugger (others are executed in parallel outside)?", HighlightingMode.Choicepoint);
            env.outWriter.WriteLine(" Pre-selecting first sequence " + seqToExecute + ".");
            env.outWriter.WriteLine("Press (0)...(9) to pre-select the corresponding sequence or (e) to enter the number of the sequence to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected sequence and continue.");
        }

        public bool ChooseSequence(ref int seqToExecute, List<Sequence> sequences, SequenceParallel seq)
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
                            env.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        return false;
                    case 'e':
                        env.outWriter.Write("Enter number of sequence to show: ");
                        String numStr = env.inReader.ReadLine();
                        if(int.TryParse(numStr, out num))
                        {
                            if(num < 0 || num >= sequences.Count)
                            {
                                env.outWriter.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                                break;
                            }
                            seqToExecute = num;
                            return false;
                        }
                        env.outWriter.WriteLine("You must enter a valid integer number!");
                        break;
                    case 's':
                    case 'n':
                    case 'd':
                        return true;
                    default:
                        env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                            + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue allowed! ");
                        break;
                }
            }
            while(true);
        }

        public void ChoosePointPrintHeader(double pointToExecute)
        {
            env.consoleOut.PrintHighlighted("Please choose: Which point in the interval series (corresponding to a sequence) to execute?", HighlightingMode.Choicepoint);
            env.outWriter.WriteLine(" Pre-selecting point " + pointToExecute + " chosen by random.");
            env.outWriter.WriteLine("Press (e) to enter a point in the interval series of the sequence to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected sequence and continue.");
        }

        public bool ChoosePoint(ref double pointToExecute, SequenceWeightedOne seq)
        {
            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'e':
                    double num;
                    env.outWriter.Write("Enter point in interval series of sequence to show: ");
                    String numStr = env.inReader.ReadLine();
                    if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out num))
                    {
                        if(num < 0.0 || num > seq.Numbers[seq.Numbers.Count - 1])
                        {
                            env.outWriter.WriteLine("You must specify a floating point number between 0.0 and " + seq.Numbers[seq.Numbers.Count - 1] + "!");
                            break;
                        }
                        pointToExecute = num;
                        return false;
                    }
                    env.outWriter.WriteLine("You must enter a valid floating point number!");
                    break;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (e)nter number and (s)/(n)/(d) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public void ChooseMatchSomeFromSetPrintHeader(int totalMatchToExecute)
        {
            env.consoleOut.PrintHighlighted("Please choose: Which match to execute?", HighlightingMode.Choicepoint);
            env.outWriter.WriteLine(" Pre-selecting match " + totalMatchToExecute + " chosen by random.");
            env.outWriter.WriteLine("Press (0)...(9) to pre-select the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) or (d) to commit to the pre-selected match and continue.");
        }

        public bool ChooseMatch(ref int totalMatchToExecute, SequenceSomeFromSet seq)
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
                        env.outWriter.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                        break;
                    }
                    totalMatchToExecute = num;
                    return false;
                case 'e':
                    env.outWriter.Write("Enter number of rule to show: ");
                    String numStr = env.inReader.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= seq.NumTotalMatches)
                        {
                            env.outWriter.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                            break;
                        }
                        totalMatchToExecute = num;
                        return false;
                    }
                    env.outWriter.WriteLine("You must enter a valid integer number!");
                    break;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public void ChooseMatchPrintHeader(int numFurtherMatchesToApply)
        {
            env.consoleOut.PrintHighlighted("Please choose: Which match to apply?", HighlightingMode.Choicepoint);
            env.outWriter.WriteLine(" Showing the match chosen by random. (" + numFurtherMatchesToApply + " following)");
            env.outWriter.WriteLine("Press (0)...(9) to show the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) or (d) to commit to the currently shown match and continue.");
        }

        public bool ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq, out int newMatchToRewrite)
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
                        env.outWriter.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                        break;
                    }
                    newMatchToRewrite = num;
                    return false;
                case 'e':
                    env.outWriter.Write("Enter number of match to show: ");
                    String numStr = env.inReader.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= matches.Count)
                        {
                            env.outWriter.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                            break;
                        }
                        newMatchToRewrite = num;
                        return false;
                    }
                    env.outWriter.WriteLine("You must enter a valid integer number!");
                    return false;
                case 's':
                case 'n':
                case 'd':
                    return true;
                default:
                    env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n)/(d) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            env.outWriter.Write("Enter number in range [0.." + upperBound + "[ or press enter to use " + randomNumber + ": ");

            do
            {
                String numStr = env.inReader.ReadLine();
                if(numStr == "")
                    return randomNumber;
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < 0 || num >= upperBound)
                    {
                        env.outWriter.WriteLine("You must specify a number between 0 and " + (upperBound - 1) + "!");
                        continue;
                    }
                    return num;
                }
                env.outWriter.WriteLine("You must enter a valid integer number!");
            }
            while(true);
        }

        public double ChooseRandomNumber(double randomNumber, Sequence seq)
        {
            env.outWriter.Write("Enter number in range [0.0 .. 1.0[ or press enter to use " + randomNumber + ": ");

            do
            {
                String numStr = env.inReader.ReadLine();
                if(numStr == "")
                    return randomNumber;
                double num;
                if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out num))
                {
                    if(num < 0.0 || num >= 1.0)
                    {
                        env.outWriter.WriteLine("You must specify a number between 0.0 and 1.0 exclusive !");
                        continue;
                    }
                    return num;
                }
                env.outWriter.WriteLine("You must enter a valid double number!");
            }
            while(true);
        }

        public object ChooseValue(string type, Sequence seq, INamedGraph graph)
        {
            object value = env.Askfor(type, graph);

            while(value == null) // bad input case
            {
                env.outWriter.Write("How to proceed? (a)bort user choice (-> value null) or (r)etry:");

read_again:
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'a':
                    env.outWriter.WriteLine();
                    return null;
                case 'r':
                    env.outWriter.WriteLine();
                    value = env.Askfor(type, graph);
                    break;
                default:
                    env.outWriter.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (a)bort user choice or (r)etry allowed! ");
                    goto read_again;
                }
            }

            return value;
        }
    }
}
