/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        readonly IDisplayer displayer;
        readonly Stack<SequenceBase> debugSequences = new Stack<SequenceBase>();

        UserChoiceMenu whichBreakpointToToggleMenu = new UserChoiceMenu(UserChoiceMenuNames.WhichBreakpointToToggleMenu, new string[] {
            "breakpointNumberToToggle", "breakpointEnterNumberToToggle", "breakpointChoicepointAbort" });

        UserChoiceMenu whichChoicepointToToggleMenu = new UserChoiceMenu(UserChoiceMenuNames.WhichChoicepointToToggleMenu, new string[] {
            "choicepointNumberToToggle", "choicepointEnterNumberToToggle", "breakpointChoicepointAbort" });

        public BreakpointAndChoicepointEditor(IDebuggerEnvironment env, IDisplayer displayer, Stack<SequenceBase> debugSequences)
        {
            this.env = env;
            this.displayer = displayer;
            this.debugSequences = debugSequences;
        }

        public void HandleToggleBreakpoints()
        {
            displayer.BeginOfDisplay("Available breakpoint positions:");
           
            Dictionary<int, int> sequenceIdToBreakpointPosMap = new Dictionary<int, int>();
            int bpPosCounter = 0; // A counter increased for every potential breakpoint position
            CollectBreakpointableSequences(debugSequences.Peek(), ref bpPosCounter, sequenceIdToBreakpointPosMap);

            DisplaySequenceContext contextBp = new DisplaySequenceContext();
            contextBp.sequenceIdToBreakpointPosMap = sequenceIdToBreakpointPosMap;
            displayer.DisplaySequenceBase(debugSequences.Peek(), contextBp, debugSequences.Count, "", "");

            HandleToggleBreakpoints(contextBp);

            env.WriteLine("Back from breakpoints to debugging.");
        }

        private void HandleToggleBreakpoints(DisplaySequenceContext contextBp)
        {
            if(contextBp.sequenceIdToBreakpointPosMap.Count == 0)
            {
                if(env.TwoPane)
                    env.PauseUntilAnyKeyPressedToContinueDialog("No breakpoint positions available, press any key to continue...");
                else
                    env.WriteLine("No breakpoint positions available!");
                return;
            }

            int pos = HandleTogglePoint(PointType.Breakpoint, contextBp.sequenceIdToBreakpointPosMap.Count);
            if(pos == -1)
                return;

            TogglePointInAllInstances(pos, false);
        }

        public void HandleToggleChoicepoints()
        {
            displayer.BeginOfDisplay("Available choicepoint positions:");

            Dictionary<int, int> sequenceIdToChoicepointPosMap = new Dictionary<int, int>();
            int cpPosCounter = 0; // A counter increased for every potential choice position
            CollectChoicepointableSequences(debugSequences.Peek(), ref cpPosCounter, sequenceIdToChoicepointPosMap);

            DisplaySequenceContext contextCp = new DisplaySequenceContext();
            contextCp.sequenceIdToChoicepointPosMap = sequenceIdToChoicepointPosMap;
            displayer.DisplaySequenceBase(debugSequences.Peek(), contextCp, debugSequences.Count, "", "");

            HandleToggleChoicepoints(contextCp);

            env.WriteLine("Back from choicepoints to debugging.");
        }

        private void HandleToggleChoicepoints(DisplaySequenceContext contextCp)
        {
            if(contextCp.sequenceIdToChoicepointPosMap.Count == 0)
            {
                if(env.TwoPane)
                    env.PauseUntilAnyKeyPressedToContinueDialog("No choicepoint positions available, press any key to continue...");
                else
                    env.WriteLine("No choicepoint positions available!");
                return;
            }

            int pos = HandleTogglePoint(PointType.Choicepoint, contextCp.sequenceIdToChoicepointPosMap.Count);
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
                char character = env.LetUserChoose(whichPointToToggleMenu);
                switch (character)
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
                    if(num >= numPositions)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (numPositions - 1) + "!");
                        break;
                    }
                    return num;
                case 'e':
                    num = env.ShowMsgAskForIntegerNumber("Enter number of " + (pointType == PointType.Breakpoint ? "breakpoint" : "choicepoint") + " to toggle (just enter for abort)", -1);
                    if(num < -1 || num >= numPositions)
                    {
                        env.WriteLine("You must specify a number between 0 and " + (numPositions - 1) + "!");
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

        private ISequenceSpecial GetSequenceAtBreakpointPosition(SequenceBase seq, int bpPos, ref int counter) // TODO: use new map instead of counter
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

        private void CollectBreakpointableSequences(SequenceBase seq, ref int counter, Dictionary<int, int> sequenceIdToBreakpointPosMap)
        {
            if(seq is ISequenceSpecial)
            {
                sequenceIdToBreakpointPosMap.Add(seq.Id, counter);
                counter++;
            }
            foreach(SequenceBase child in seq.ChildrenBase)
            {
                CollectBreakpointableSequences(child, ref counter, sequenceIdToBreakpointPosMap);
            }
        }

        private SequenceRandomChoice GetSequenceAtChoicepointPosition(SequenceBase seq, int cpPos, ref int counter) // TODO: use new map instead of counter
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

        private void CollectChoicepointableSequences(SequenceBase seq, ref int counter, Dictionary<int, int> sequenceIdToChoicepointPosMap)
        {
            if(seq is SequenceRandomChoice && ((SequenceRandomChoice)seq).Random)
            {
                sequenceIdToChoicepointPosMap.Add(seq.Id, counter);
                counter++;
            }
            foreach(SequenceBase child in seq.ChildrenBase)
            {
                CollectChoicepointableSequences(child, ref counter, sequenceIdToChoicepointPosMap);
            }
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
