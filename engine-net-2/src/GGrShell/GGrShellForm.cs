/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Windows.Forms;
using System.Collections.Generic;

using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.grShell;
using System.Diagnostics;

namespace GGrShell
{
    public partial class GGrShellForm : Form
    {
        public GrShellConfigurationAndControlState shellConfig;
        public GrShellComponents shellComponents;
        public GuiConsoleControlAsTextReader reader;
        public GuiConsoleControlAsTextWriter writer;
        private ClipboardLineSource clipboardLineSource;

        public GGrShellForm()
        {
            InitializeComponent();
        }

        private void GGrShellForm_Shown(object sender, EventArgs e)
        {
            ExecuteShell();
        }

        // maybe TODO: libGGrShell with common code that is then used in the ShellExampleWindowsForms (but a minimal console window only example could be helpful, while this app is to be extended with GUI gizmos)
        private void ExecuteShell()
        {
            try
            {
                shellComponents.driver.conditionalEvaluationResults.Push(true);

                // first process file input if available (stemming from arguments given when the shell was started)...
                while(!shellConfig.readFromConsole)
                {
                    bool success = shellComponents.shell.ParseShellCommand();

                    int errorCode = GrShellMainHelper.HandleEofOrErrorIfNonConsoleShell(success, shellConfig, shellComponents);
                    if(errorCode != 0)
                        return;
                }

                // ...then console input (which may cause an internal file processing upon include/replay)
                while(!shellComponents.driver.Quitting && !shellComponents.driver.Eof)
                {
                    GrShellMainHelper.ShowPromptAsNeeded(shellConfig.showPrompt);

                    // simulates a line-by-line entering from the clipboard
                    if(clipboardLineSource != null)
                        PasteNextLineFromClipboard();

                    bool success = shellComponents.shell.ParseShellCommand(); // contains an Application.DoEvents(), causing this "main loop" to still support a reactive GUI
                                                                              // caveat: the internal loop on an include/replay comes without a DoEvents, but a dedicated DoEvents here wouldn't help - TODO

                    // stop line-by-line entering from the clipboard upon an execution error
                    if(!success)
                        clipboardLineSource = null;

                    int errorCode = GrShellMainHelper.HandleEofOrErrorIfNonConsoleShell(success, shellConfig, shellComponents);
                    if(errorCode != 0)
                        return;
                }

                shellComponents.driver.conditionalEvaluationResults.Pop();
            }
            catch(Exception ex)
            {
                writer.WriteLine("exit due to " + ex.Message);
                writer.WriteLine(ex.StackTrace);
                writer.WriteLine(ex.Source);
            }
            finally
            {
                shellComponents.impl.Cleanup();
                Close();
            }
        }

        private void GGrShellForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            console.Cancel();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clipboardLineSource = new ClipboardLineSource();

            PasteNextLineFromClipboard();

            /* treat clipboard content like a file, issue: not printed, only execution results shown
            TextReader newReader = new StringReader(Clipboard.GetText());
            shellComponents.shell.ReInit(newReader);
            shellComponents.driver.tokenSources.Pop();
            shellComponents.driver.tokenSources.Push(shellComponents.shell.token_source);
            shellConfig.showPrompt = false;
            shellConfig.readFromConsole = false;
            shellComponents.reader.Close();
            shellComponents.reader = newReader; */
        }

        private void PasteNextLineFromClipboard()
        {
            Debug.Assert(!console.LineEntered); // previous line was not yet fully processed - this should not happen, it could if a command is not ended with a newline in the grammar of the shell
            string line = clipboardLineSource.GetNextLine();
            console.EnterLine(line);
            if(clipboardLineSource.IsEmpty)
                clipboardLineSource = null;
        }
    }
}
