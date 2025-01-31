/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public partial class GuiDebuggerHost : Form, IGuiDebuggerHost, IDebuggerGUIForDataRendering // TODO: introduce own object or really use this one to implement IDebuggerGUIForDataRendering?
    {
        UserChoiceMenu currentUserChoiceMenu;
        UserChoiceMenu currentAdditionalGuiUserChoiceMenu;
        Dictionary<string, ToolStripItem[]> optionNameToControls; // map of menu option names aka commands from application logic to GUI controls implementing these commands/options
        MSAGLClient msaglClient; //private Microsoft.Msagl.GraphViewerGdi.GViewer gv = null; -- gets created by the msagl client, a behavior needed if it is used without debugger as a plain graph viewer
        IDebugger debugger;

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
            Fill("commandContinueDebuggingAnyKey", continueToolStripMenuItem, continueToolStripButton);
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
            Fill("commandToggleBreakpoints", breakpointsToolStripMenuItem);
            Fill("breakpointEnterNumberToToggle", toggleBreakpointToolStripMenuItem);
            Fill("breakpointChoicepointAbort", backAbortToolStripButton); // maybe split
            Fill("commandToggleChoicepoints", choicepointsToolStripMenuItem);
            Fill("choicepointEnterNumberToToggle", toggleChoicepointToolStripMenuItem);
            Fill("commandToggleLazyChoice", toggleLazyChoiceToolStripMenuItem);
            Fill("commandUpFromCurrentEntry", stepUpToolStripMenuItem, stepUpToolStripButton);
            Fill("commandWatchpoints", watchpointsToolStripMenuItem);
            Fill("watchpointEdit", editWatchpointToolStripMenuItem);
            Fill("watchpointToggle", toggleWatchpointToolStripMenuItem);
            Fill("watchpointDelete", deleteWatchpointToolStripMenuItem);
            Fill("watchpointInsert", insertWatchpointToolStripMenuItem);
            Fill("watchpointAppend", appendWatchpointToolStripMenuItem);
            Fill("watchpointAbortReturn", backAbortToolStripButton);
            Fill("watchpointAbort", backAbortToolStripButton);
            Fill("choosePreselectedBranch", continueToolStripButton);
            Fill("choosePreselected", continueToolStripButton);
            Fill("choosePreselectedMatch", continueToolStripButton);
            Fill("chooseValueAbort", backAbortToolStripButton);
            Fill("viewSwitch", switchViewToolStripMenuItem);
            Fill("viewRefresh", refreshViewToolStripMenuItem);
            Fill("enterLineCancel", backAbortToolStripButton);
            Fill("pauseContinueDialogAnyKey", continueDialogToolStripButton);
        }

        private void Fill(string optionName, params ToolStripItem[] controls)
        {
            optionNameToControls.Add(optionName, controls);
        }

        // IDebuggerGUIForDataRendering ------------------------------

        public void SetContext(UserChoiceMenu userChoiceMenu, UserChoiceMenu additionalGuiUserChoiceMenu)
        {
            if(currentUserChoiceMenu != null)
                EnableDisableControls(currentUserChoiceMenu, false);
            if(currentAdditionalGuiUserChoiceMenu != null)
                EnableDisableControls(currentAdditionalGuiUserChoiceMenu, false);

            EnableDisableControls(userChoiceMenu, true);
            if(additionalGuiUserChoiceMenu != null)
                EnableDisableControls(additionalGuiUserChoiceMenu, true);

            currentUserChoiceMenu = userChoiceMenu;
            currentAdditionalGuiUserChoiceMenu = additionalGuiUserChoiceMenu;
        }

        public IBasicGraphViewerClient graphViewer
        {
            get { return msaglClient; }
        }

        // IGuiDebuggerHost ------------------------------

        public IDebuggerGUIForDataRendering MainWorkObjectGuiGraphRenderer
        {
            get { return this; }
        }
        public IDebuggerConsoleUICombined MainWorkObjectGuiConsoleControl
        {
            get { return mainWorkObjectGuiConsoleControl; }
        }
        public IDebuggerConsoleUICombined InputOutputAndLogGuiConsoleControl
        {
            get { return inputOutputAndLogGuiConsoleControl; }
        }

        // IGuiConsoleDebuggerHost ------------------------------

        public IDebuggerConsoleUICombined GuiConsoleControl
        {
            get { return inputOutputAndLogGuiConsoleControl; }
        }
        public IDebuggerConsoleUICombined OptionalGuiConsoleControl
        {
            get { return mainWorkObjectGuiConsoleControl; }
        }

        public bool TwoPane
        {
            get { return true; }
            set {; } // todo: maybe throw not implemented exception instead of silent ignoring
        }

        //public void Show(); by the Form
        //public void Close(); by the Form

        public IDebugger Debugger
        {
            get { return debugger; }
            set { debugger = value; }
        }

        // ------------------------------

        // outWriter for data rendering, base version, also implemented by the graph GUI version, resulting in a list of lines
        public void WriteLineDataRendering(string value)
        {
            // GUI TODO: write to graph viewer
        }

        public void WriteLineDataRendering(string format, params object[] arg)
        {
            // GUI TODO: write to graph viewer
        }

        public void WriteLineDataRendering()
        {
            // GUI TODO: write to graph viewer
        }

        public void Clear()
        {
            // GUI TODO: forward to graph viewer, as of now ClearGraph() is called manually (not targeted any more: soft clear, clears console but not the graph viewer, as often the graph viewer content is only modified - this is the implementation of the graph viewer render)
        }

        public void SuspendImmediateExecution()
        {
            SuspendLayout();
        }

        public void RestartImmediateExecution()
        {
            ResumeLayout();
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

        private KeyValuePair<char, ConsoleKey> GetKey(ToolStripItem clicked)
        {
            string command = GetCommand(clicked);
            KeyValuePair<char, ConsoleKey> key = currentUserChoiceMenu.GetKey(command);
            if(key.Key != '\0')
                return key;
            if(currentAdditionalGuiUserChoiceMenu != null)
                key = currentAdditionalGuiUserChoiceMenu.GetKey(command);
            if(key.Key != '\0')
                return key;
            return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.NoName);
        }

        private string GetCommand(ToolStripItem clicked) // reverse search should be ok performance wise, no premature optimization
        {
            foreach(KeyValuePair<string, ToolStripItem[]> keyValuePair in optionNameToControls)
            {
                foreach(ToolStripItem item in keyValuePair.Value)
                {
                    if(item == clicked)
                    {
                        // first match might be not the right one, filter to the dynamically enabled command instead of the statically first matching
                        // e.g. the continue button may be enabled by many different commands, with different accelerator keys (but only one at a time, from the current user choice menu, or the additional one)
                        string command = keyValuePair.Key;
                        if(currentUserChoiceMenu.IsCurrentlyAvailable(command) || (currentAdditionalGuiUserChoiceMenu != null ? currentAdditionalGuiUserChoiceMenu.IsCurrentlyAvailable(command) : false))
                        {
                            return command;
                        }
                    }
                }
            }
            throw new Exception("Internal error - no command found");
        }

        // ------------------------------

        private void nextMatchToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void detailedStepToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void stepToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void stepUpToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void stepOutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void runToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void abortToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void continueToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void skipSingleMatchesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void debugAtSourceCodeLevelToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void toggleBreakpointsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void toggleBreakpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void toggleChoicepointsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void toggleChoicepointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void toggleLazyChoiceToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if(toggleLazyChoiceToolStripMenuItem.Checked)
            {
                toggleLazyChoiceToolStripMenuItem.Checked = false;
            }
            else
            {
                toggleLazyChoiceToolStripMenuItem.Checked = true;
            }
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void watchpointsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void editWatchpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void toggleWatchpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void deleteWatchpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void insertWatchpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void appendWatchpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void showVariablesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void showClassObjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void printStacktraceToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void printFullStateToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void highlightToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void dumpGraphToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void asGraphToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void switchViewToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if(switchViewToolStripMenuItem.Checked)
            {
                msaglClient.Close();
                msaglClient = null;
                switchViewToolStripMenuItem.Checked = false;
            }
            else
            {
                msaglClient = new MSAGLClient(theSplitContainer.Panel1);
                // GUI todo: configuring of graph layout of sequences is done in the SequenceRenderer ... keep it?
                // GUI todo: maybe configure viewer, toolbar on top looks a bit ugly
                switchViewToolStripMenuItem.Checked = true;
            }
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            KeyValuePair<char, ConsoleKey> nextKey = GetKey(refreshViewToolStripMenuItem); // key of an immediately following refresh so the new view is displayed without user intervention
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value, nextKey.Key, nextKey.Value);
        }

        private void refreshViewToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void continueToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void nextMatchToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void detailedStepToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void stepToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void stepUpToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void stepOutToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void runToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void abortToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void skipSingleMatchesToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void backAbortToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void continueDialogToolStripButton_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<char, ConsoleKey> key = GetKey((ToolStripItem)sender);
            inputOutputAndLogGuiConsoleControl.EnterKey(key.Key, key.Value);
        }

        private void GuiDebuggerHost_FormClosed(object sender, FormClosedEventArgs e)
        {
            inputOutputAndLogGuiConsoleControl.Cancel();
        }
    }
}
