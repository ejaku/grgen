using de.unika.ipd.grGen.grShell;
using System;
using System.Windows.Forms;

namespace ShellExampleWindowsForms
{
    public partial class ShellForm : Form
    {
        public GrShellConfigurationAndControlState shellConfig;
        public GrShellComponents shellComponents;
        public GuiConsoleControlAsTextReader reader;
        public GuiConsoleControlAsTextWriter writer;

        public ShellForm()
        {
            InitializeComponent();
        }

        private void ShellForm_Shown(object sender, EventArgs e)
        {
            ExecuteShell();
        }

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

                    bool success = shellComponents.shell.ParseShellCommand(); // contains an Application.DoEvents(), causing this "main loop" to still support a reactive GUI
                                                                              // caveat: the internal loop on an include/replay comes without a DoEvents, but a dedicated DoEvents here wouldn't help - TODO

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

        private void ShellForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            console.Cancel();
        }
    }
}
