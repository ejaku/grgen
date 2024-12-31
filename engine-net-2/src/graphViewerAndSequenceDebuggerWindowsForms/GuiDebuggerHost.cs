/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Collections.Generic;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public partial class GuiDebuggerHost : Form, IGuiDebuggerHost, IDebuggerGUIForDataRendering // TODO: introduce own object or really use this one to implement IDebuggerGUIForDataRendering?
    {
        UserChoiceMenu currentUserChoiceMenu;
        Dictionary<string, ToolStripItem[]> optionNameToControls; // map of menu option names aka commands from application logic to GUI controls implementing these commands/options

        public GuiDebuggerHost()
        {
            InitializeComponent();
            mainWorkObjectGuiConsoleControl.EnableClear = true;
            inputOutputAndLogGuiConsoleControl.Select(); // ensure it receives keyboard input

            FillMappingOfOptionNamesToToolStripItems();
        }

        private void FillMappingOfOptionNamesToToolStripItems()
        {
            optionNameToControls = new Dictionary<string, ToolStripItem[]>(); // maybe todo: only one click handler, registered to all controls in the range of this map (implementation is the same generic one for all of them)

            Fill("commandAbort", abortToolStripMenuItem, abortToolStripButton);
            Fill("commandAsGraph", asGraphToolStripMenuItem);
            Fill("commandContinue", continueToolStripMenuItem, continueToolStripButton); // assert - maybe dedicated menu, then also dedicated commandAbort for assert separate from the regular commandAbort
            Fill("commandContinueAnyKey", continueToolStripMenuItem, continueToolStripButton);
            Fill("commandContinueApplyRewrite", continueToolStripMenuItem, continueToolStripButton);
            Fill("commandContinueDebuggingAsBefore", continueToolStripMenuItem, continueToolStripButton);
            Fill("commandContinueDetailedDebugging", continueToolStripMenuItem, continueToolStripButton);
            Fill("commandContinueShowSingleMatchesAndApplyRewrite", continueToolStripMenuItem, continueToolStripButton);
            Fill("commandDebugAtSourceCodeLevel", debugAtSourceCodeLevelToolStripMenuItem);
            Fill("commandDetailedStep", detailedStepToolStripMenuItem, detailedStepToolStripButton);
            Fill("commandDumpGraph", dumpGraphToolStripMenuItem);
            Fill("commandFullState", printFullStateToolStripMenuItem);
            Fill("commandHighlight", highlightToolStripMenuItem);
            Fill("commandNextMatch", nextMatchToolStripMenuItem, nextMatchToolStripButton);
            Fill("commandOutOfDetailedDebuggingEntry", stepOutToolStripMenuItem);
            Fill("commandPrintStacktrace", printStacktraceToolStripMenuItem);
            Fill("commandPrintSubruleStacktrace", printStacktraceToolStripMenuItem);
            Fill("commandRun", runToolStripMenuItem, runToolStripButton);
            Fill("commandRunUntilEndOfDetailedDebugging", runToolStripMenuItem, runToolStripButton);
            Fill("commandShowClassObject", showClassObjectToolStripMenuItem);
            Fill("commandShowVariables", showVariablesToolStripMenuItem);
            Fill("commandSkipSingleMatches", skipSingleMatchesToolStripMenuItem, skipSingleMatchesToolStripButton);
            Fill("commandStep", stepToolStripMenuItem, stepToolStripButton);
            Fill("commandStepMode", stepToolStripMenuItem, stepToolStripButton);
            Fill("commandStepOut", stepOutToolStripMenuItem, stepOutToolStripButton);
            Fill("commandStepUp", stepUpToolStripMenuItem, stepUpToolStripButton);
            Fill("commandToggleBreakpoints", toggleBreakpointsToolStripMenuItem);
            Fill("commandToggleChoicepoints", toggleChoicepointsToolStripMenuItem);
            Fill("commandToggleLazyChoice", toggleLazyChoiceToolStripMenuItem);
            Fill("commandUpFromCurrentEntry", stepUpToolStripMenuItem, stepUpToolStripButton);
            Fill("commandWatchpoints", watchpointsToolStripMenuItem);
        }

        private void Fill(string optionName, params ToolStripItem[] controls)
        {
            optionNameToControls.Add(optionName, controls);
        }

        public void SetContext(UserChoiceMenu userChoiceMenu)
        {
            if(currentUserChoiceMenu != null)
                EnableDisableControls(currentUserChoiceMenu, false);
            EnableDisableControls(userChoiceMenu, true);
            currentUserChoiceMenu = userChoiceMenu;
        }

        private void EnableDisableControls(UserChoiceMenu userChoiceMenu, bool enable)
        {
            foreach(string optionName in userChoiceMenu.optionNames)
            {
                ToolStripItem[] controls;
                if(optionNameToControls.TryGetValue(optionName, out controls))
                {
                    foreach(ToolStripItem control in controls)
                    {
                        control.Enabled = enable;
                    }
                }
            }
        }

        private char GetKey(ToolStripItem clicked)
        {
            string command = GetCommand(clicked);
            for(int i=0; i < currentUserChoiceMenu.optionNames.Length; ++i)
            {
                if(currentUserChoiceMenu.optionNames[i] == command)
                {
                    return GetKey(currentUserChoiceMenu.options[i]);
                }
            }
            return ' ';
        }

        private string GetCommand(ToolStripItem clicked) // reverse search should be ok performance wise, no premature optimization
        {
            foreach(KeyValuePair<string, ToolStripItem[]> keyValuePair in optionNameToControls)
            {
                foreach(ToolStripItem item in keyValuePair.Value)
                {
                    if(item == clicked)
                    {
                        return keyValuePair.Key;
                    }
                }
            }
            return "";
        }

        private char GetKey(string commandOption)
        {
            int indexOfOpeningParenthesis = -1;
            int indexOfClosingParenthesis = 0;

            while(true)
            {
                indexOfOpeningParenthesis = commandOption.IndexOf('(', indexOfOpeningParenthesis + 1);
                indexOfClosingParenthesis = commandOption.IndexOf(')', indexOfOpeningParenthesis);
                if(indexOfOpeningParenthesis == -1 || indexOfClosingParenthesis == -1)
                    break; // only placeholder keys that are skipped are contained in the command (or none at all, but that would be illegal)
                if(commandOption[indexOfOpeningParenthesis + 1] == '(' // skip escaped parenthesis in the form of (())
                    || indexOfClosingParenthesis > indexOfOpeningParenthesis + 2) // skip number special (0-9), also skipping (any key)
                    indexOfOpeningParenthesis = indexOfClosingParenthesis;
                else
                    return commandOption[indexOfOpeningParenthesis + 1]; // return first matching key
            }

            return ' '; // return space in case no key was found, this is commonly the case if the option contains only (any key)
        }

        private void nextMatchToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void detailedStepToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void stepToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void stepUpToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void stepOutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void runToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void abortToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void continueToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void skipSingleMatchesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void debugAtSourceCodeLevelToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void toggleBreakpointsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void toggleChoicepointsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void toggleLazyChoiceToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void watchpointsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void showVariablesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void showClassObjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void printStacktraceToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void printFullStateToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void highlightToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void dumpGraphToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void asGraphToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void continueToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void nextMatchToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void detailedStepToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void stepToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void stepUpToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void stepOutToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void runToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void abortToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }

        private void skipSingleMatchesToolStripButton_Click(object sender, System.EventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.EnterKey(GetKey((ToolStripItem)sender));
        }
    }
}
