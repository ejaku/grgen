/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    static class UserChoiceMenu
    {
        public static int ChooseDirection(PrintSequenceContext context, IDebuggerEnvironment env, int direction, Sequence seq)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Please choose: Which branch to execute first?", HighlightingMode.Choicepoint);
            Console.Write(" (l)eft or (r)ight or (s)/(n) to continue with random choice?  (Random has chosen " + (direction == 0 ? "(l)eft" : "(r)ight") + ") ");

            do
            {
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'l':
                    Console.WriteLine();
                    return 0;
                case 'r':
                    Console.WriteLine();
                    return 1;
                case 's':
                case 'n':
                    Console.WriteLine();
                    return direction;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (l)eft branch, (r)ight branch, (s)/(n) to continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChooseSequencePrintHeader(PrintSequenceContext context, int seqToExecute)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Please choose: Which sequence to execute?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Pre-selecting sequence " + seqToExecute + " chosen by random.");
            Console.WriteLine("Press (0)...(9) to pre-select the corresponding sequence or (e) to enter the number of the sequence to show."
                                + " Press (s) or (n) to commit to the pre-selected sequence and continue."
                                + " Pressing (u) or (o) works like (s)/(n) but does not ask for the remaining contained sequences.");
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
                        Console.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                        break;
                    }
                    seqToExecute = num;
                    return false;
                case 'e':
                    Console.Write("Enter number of sequence to show: ");
                    String numStr = Console.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= sequences.Count)
                        {
                            Console.WriteLine("You must specify a number between 0 and " + (sequences.Count - 1) + "!");
                            break;
                        }
                        seqToExecute = num;
                        return false;
                    }
                    Console.WriteLine("You must enter a valid integer number!");
                    break;
                case 's':
                case 'n':
                    return true;
                case 'u':
                case 'o':
                    seq.Skip = true; // skip remaining rules (reset after exection of seq)
                    return true;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n) to commit and continue, (u)/(o) to commit and skip remaining choices allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChoosePointPrintHeader(PrintSequenceContext context, double pointToExecute)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Please choose: Which point in the interval series (corresponding to a sequence) to execute?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Pre-selecting point " + pointToExecute + " chosen by random.");
            Console.WriteLine("Press (e) to enter a point in the interval series of the sequence to show."
                                + " Press (s) or (n) to commit to the pre-selected sequence and continue.");
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
                    Console.Write("Enter point in interval series of sequence to show: ");
                    String numStr = Console.ReadLine();
                    if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out num))
                    {
                        if(num < 0.0 || num > seq.Numbers[seq.Numbers.Count - 1])
                        {
                            Console.WriteLine("You must specify a floating point number between 0.0 and " + seq.Numbers[seq.Numbers.Count - 1] + "!");
                            break;
                        }
                        pointToExecute = num;
                        return false;
                    }
                    Console.WriteLine("You must enter a valid floating point number!");
                    break;
                case 's':
                case 'n':
                    return true;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (e)nter number and (s)/(n) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChooseMatchSomeFromSetPrintHeader(PrintSequenceContext context, int totalMatchToExecute)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Please choose: Which match to execute?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Pre-selecting match " + totalMatchToExecute + " chosen by random.");
            Console.WriteLine("Press (0)...(9) to pre-select the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) to commit to the pre-selected match and continue.");
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
                        Console.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                        break;
                    }
                    totalMatchToExecute = num;
                    return false;
                case 'e':
                    Console.Write("Enter number of rule to show: ");
                    String numStr = Console.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= seq.NumTotalMatches)
                        {
                            Console.WriteLine("You must specify a number between 0 and " + (seq.NumTotalMatches - 1) + "!");
                            break;
                        }
                        totalMatchToExecute = num;
                        return false;
                    }
                    Console.WriteLine("You must enter a valid integer number!");
                    break;
                case 's':
                case 'n':
                    return true;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static void ChooseMatchPrintHeader(PrintSequenceContext context, int numFurtherMatchesToApply)
        {
            WorkaroundManager.Workaround.PrintHighlighted("Please choose: Which match to apply?", HighlightingMode.Choicepoint);
            Console.WriteLine(" Showing the match chosen by random. (" + numFurtherMatchesToApply + " following)");
            Console.WriteLine("Press (0)...(9) to show the corresponding match or (e) to enter the number of the match to show."
                                + " Press (s) or (n) to commit to the currently shown match and continue.");
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
                        Console.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                        break;
                    }
                    newMatchToRewrite = num;
                    return false;
                case 'e':
                    Console.Write("Enter number of match to show: ");
                    String numStr = Console.ReadLine();
                    if(int.TryParse(numStr, out num))
                    {
                        if(num < 0 || num >= matches.Count)
                        {
                            Console.WriteLine("You must specify a number between 0 and " + (matches.Count - 1) + "!");
                            break;
                        }
                        newMatchToRewrite = num;
                        return false;
                    }
                    Console.WriteLine("You must enter a valid integer number!");
                    return false;
                case 's':
                case 'n':
                    return true;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (0)...(9), (e)nter number, (s)/(n) to commit and continue allowed! ");
                    break;
                }
            }
            while(true);
        }

        public static int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            Console.Write("Enter number in range [0.." + upperBound + "[ or press enter to use " + randomNumber + ": ");

            do
            {
                String numStr = Console.ReadLine();
                if(numStr == "")
                    return randomNumber;
                int num;
                if(int.TryParse(numStr, out num))
                {
                    if(num < 0 || num >= upperBound)
                    {
                        Console.WriteLine("You must specify a number between 0 and " + (upperBound - 1) + "!");
                        continue;
                    }
                    return num;
                }
                Console.WriteLine("You must enter a valid integer number!");
            }
            while(true);
        }

        public static double ChooseRandomNumber(double randomNumber, Sequence seq)
        {
            Console.Write("Enter number in range [0.0 .. 1.0[ or press enter to use " + randomNumber + ": ");

            do
            {
                String numStr = Console.ReadLine();
                if(numStr == "")
                    return randomNumber;
                double num;
                if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out num))
                {
                    if(num < 0.0 || num >= 1.0)
                    {
                        Console.WriteLine("You must specify a number between 0.0 and 1.0 exclusive !");
                        continue;
                    }
                    return num;
                }
                Console.WriteLine("You must enter a valid double number!");
            }
            while(true);
        }

        public static object ChooseValue(IDebuggerEnvironment env, string type, Sequence seq)
        {
            object value = env.Askfor(type);

            while(value == null) // bad input case
            {
                Console.Write("How to proceed? (a)bort user choice (-> value null) or (r)etry:");

read_again:
                ConsoleKeyInfo key = env.ReadKeyWithCancel();
                switch(key.KeyChar)
                {
                case 'a':
                    Console.WriteLine();
                    return null;
                case 'r':
                    Console.WriteLine();
                    value = env.Askfor(type);
                    break;
                default:
                    Console.WriteLine("Illegal choice (Key = " + key.Key
                        + ")! Only (a)bort user choice or (r)etry allowed! ");
                    goto read_again;
                }
            }

            return value;
        }
    }
}
