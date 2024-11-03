/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    class BreakpointAndChoicepointEditor
    {
        private enum PointType
        {
            Breakpoint, Choicepoint
        }

        readonly IDebuggerEnvironment env;

        readonly Stack<SequenceBase> debugSequences = new Stack<SequenceBase>();

        UserChoiceMenu whichBreakpointToToggleMenu = new UserChoiceMenu(UserChoiceMenuNames.WhichBreakpointToToggleMenu, new string[] {
            "(0-9) to toggle the corresponding breakpoint", "(e)nter the number of the breakpoint to toggle", "(a)bort" });

        UserChoiceMenu whichChoicepointToToggleMenu = new UserChoiceMenu(UserChoiceMenuNames.WhichChoicepointToToggleMenu, new string[] {
            "(0-9) to toggle the corresponding choicepoint", "(e)nter the number of the choicepoint to toggle", "(a)bort" });

        public BreakpointAndChoicepointEditor(IDebuggerEnvironment env, Stack<SequenceBase> debugSequences)
        {
            this.env = env;
            this.debugSequences = debugSequences;
        }

        public void HandleToggleBreakpoints()
        {
            env.Clear();
            env.WriteDataRendering("Available breakpoint positions:\n  ");

            PrintSequenceContext contextBp = new PrintSequenceContext();
            contextBp.bpPosCounter = 0;
            new SequencePrinter(env).PrintSequenceBase(debugSequences.Peek(), contextBp, debugSequences.Count);
            env.WriteLine();

            if(contextBp.bpPosCounter == 0)
            {
                env.WriteLineDataRendering("No breakpoint positions available!");
                return;
            }

            int pos = HandleTogglePoint(PointType.Breakpoint, contextBp.bpPosCounter);
            if(pos == -1)
                return;

            TogglePointInAllInstances(pos, false);
        }

        public void HandleToggleChoicepoints()
        {
            env.Clear();
            env.WriteDataRendering("Available choicepoint positions:\n  ");

            PrintSequenceContext contextCp = new PrintSequenceContext();
            contextCp.cpPosCounter = 0;
            new SequencePrinter(env).PrintSequenceBase(debugSequences.Peek(), contextCp, debugSequences.Count);
            env.WriteLine();

            if(contextCp.cpPosCounter == 0)
            {
                env.WriteLineDataRendering("No choicepoint positions available!");
                return;
            }

            int pos = HandleTogglePoint(PointType.Choicepoint, contextCp.cpPosCounter);
            if(pos == -1)
                return;

            TogglePointInAllInstances(pos, true);
        }

        private int HandleTogglePoint(PointType pointType, int numPositions)
        {
            UserChoiceMenu whichPointToToggleMenu;
            if(pointType == PointType.Breakpoint)
            {
                env.WriteLine("Which breakpoint to toggle (toggling on is shown by +, off by -)?");
                whichPointToToggleMenu = whichBreakpointToToggleMenu;
            }
            else // "choicepoint"
            {
                env.WriteLine("Which choicepoint to toggle (toggling on is shown by +, off by -)?");
                whichPointToToggleMenu = whichChoicepointToToggleMenu;
            }
            env.PrintInstructions(whichPointToToggleMenu, "Press ", ".");

            do
            {
                char key = env.LetUserChoose(whichPointToToggleMenu);
                switch (key)
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
                    int num = key - '0';
                    if(num >= numPositions)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (numPositions - 1) + "!");
                        break;
                    }
                    return num;
                case 'e':
                    num = env.ShowMsgAskForIntegerNumber("Enter number of " + (pointType == PointType.Breakpoint ? "breakpoint" : "choicepoint") + " to toggle (-1 for abort)");
                    if(num < -1 || num >= numPositions)
                    {
                        env.WriteLine("You must specify a number between -1 and " + (numPositions - 1) + "!");
                        break;
                    }
                    return num;
                case 'a':
                    return -1;
                default:
                    throw new Exception("Internal error");
                }
            } while(true);
        }

        private void ToggleChoicepoint(SequenceBase seq, int cpPos)
        {
            int cpCounter = 0; // dummy
            SequenceRandomChoice cpSeq = GetSequenceAtChoicepointPosition(seq, cpPos, ref cpCounter);
            cpSeq.Choice = !cpSeq.Choice;
        }

        private void ToggleBreakpoint(SequenceBase seq, int bpPos)
        {
            int bpCounter = 0; // dummy
            ISequenceSpecial bpSeq = GetSequenceAtBreakpointPosition(seq, bpPos, ref bpCounter);
            bpSeq.Special = !bpSeq.Special;
        }

        private ISequenceSpecial GetSequenceAtBreakpointPosition(SequenceBase seq, int bpPos, ref int counter)
        {
            if(seq is ISequenceSpecial)
            {
                if(counter == bpPos)
                    return (ISequenceSpecial)seq;
                counter++;
            }
            foreach (SequenceBase child in seq.ChildrenBase)
            {
                ISequenceSpecial res = GetSequenceAtBreakpointPosition(child, bpPos, ref counter);
                if(res != null)
                    return res;
            }
            return null;
        }

        private SequenceRandomChoice GetSequenceAtChoicepointPosition(SequenceBase seq, int cpPos, ref int counter)
        {
            if(seq is SequenceRandomChoice && ((SequenceRandomChoice)seq).Random)
            {
                if(counter == cpPos)
                    return (SequenceRandomChoice)seq;
                counter++;
            }
            foreach (SequenceBase child in seq.ChildrenBase)
            {
                SequenceRandomChoice res = GetSequenceAtChoicepointPosition(child, cpPos, ref counter);
                if(res != null)
                    return res;
            }
            return null;
        }
        
        private void TogglePointInAllInstances(int pos, bool choice)
        {
            if(debugSequences.Count > 1)
            {
                SequenceDefinitionInterpreted top = (SequenceDefinitionInterpreted)debugSequences.Peek();
                SequenceBase[] callStack = debugSequences.ToArray();
                for(int i = 0; i <= callStack.Length - 2; ++i) // non definition bottom excluded
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)callStack[i];
                    if(seqDef.SequenceName == top.SequenceName)
                    {
                        if(choice)
                            ToggleChoicepoint(seqDef, pos);
                        else
                            ToggleBreakpoint(seqDef, pos);
                    }
                }

                // additionally handle the internally cached sequences
                foreach(SequenceDefinitionInterpreted seqDef in top.CachedSequenceCopies)
                {
                    if(choice)
                        ToggleChoicepoint(seqDef, pos);
                    else
                        ToggleBreakpoint(seqDef, pos);
                }
            }
            else
            {
                if(choice)
                    ToggleChoicepoint(debugSequences.Peek(), pos);
                else
                    ToggleBreakpoint(debugSequences.Peek(), pos);
            }
        }
    }
}
