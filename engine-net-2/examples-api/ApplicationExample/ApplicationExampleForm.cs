using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using de.unika.ipd.grGen.grShell;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;

namespace ApplicationExample
{
    // you must copy the content of engine-net-2\bin to bin\Debug and bin\Release before executing this example
    // this is due to implementation issues, the GrGen-implementation in its current state assumes that 
    // the current executing assembly lies within engine-net-2/bin (this holds for the GrShell application and the GrGen compiler)

    public partial class ApplicationExampleForm : Form
    {
        // import Win32 API console functionality
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        public ApplicationExampleForm()
        {
            InitializeComponent();
        }

        String scriptFilename;
        TextReader reader;
        bool showPrompt;
        bool readFromConsole;
        GrShell shell;
        GrShellImpl shellImpl;
        IGrShellImplForDriver impl;
        GrShellDriver driver;

        private void buttonOpenShell_Click(object sender, EventArgs e)
        {
            SetUp(null);

            textBoxCommandLine.Enabled = true;
            buttonSendToConsole.Enabled = true;
        }

        private void buttonExecuteMutexInShell_Click(object sender, EventArgs e)
        {
            int errorCode = SetUp("ApplicationExampleMutex10.grs");
            if(errorCode != 0)
            {
                ShowErrorDialog(errorCode);
                return;
            }

            ExecuteCommandsFromFile();
        }

        private int SetUp(String scriptFilename)
        {
            int errorCode = DetermineAndOpenInputSource(scriptFilename);
            if(errorCode != 0)
            {
                ShowErrorDialog(errorCode);
                return errorCode;
            }

            AllocateConsole();

            shell = new GrShell(reader);
            shellImpl = new GrShellImpl();
            impl = shellImpl;
            driver = new GrShellDriver(shell, impl);
            shell.SetImpl(shellImpl);
            shell.SetDriver(driver);
            driver.tokenSources.Push(shell.token_source);
            driver.showIncludes = false;
            impl.nonDebugNonGuiExitOnError = false;

            this.AcceptButton = buttonSendToConsole;

            if(showPrompt)
                ConsoleUI.outWriter.WriteLine("Please enter the shell commands in the dialog (only debug commands/keys are available here) (suggestion: paste line-by-line the ApplicationExampleMutex10.grs from the local bin/Debug folder).");

            buttonOpenShell.Enabled = false;
            buttonExecuteMutexInShell.Enabled = false;
            buttonExecuteMutexInDebugger.Enabled = false;

            return 0;
        }

        private int DetermineAndOpenInputSource(String scriptFilename)
        {
            this.scriptFilename = scriptFilename;

            if(scriptFilename != null)
            {
                try
                {
                    reader = new StreamReader(scriptFilename);
                }
                catch(Exception e)
                {
                    ConsoleUI.outWriter.WriteLine("Unable to read file \"" + scriptFilename + "\": " + e.Message);
                    reader = null;
                    showPrompt = false;
                    readFromConsole = false;
                    return -1;
                }
                showPrompt = false;
                readFromConsole = false;
            }
            else
            {
                reader = new StringReader("");
                showPrompt = true;
                readFromConsole = true;
            }

            return 0;
        }

        private void AllocateConsole()
        {
            // allocate Windows Console and reopen stdout (note: VisualStudio is carring out some redirections in the debugger, 
            // see how to handle these, besides starting outside debugger and attaching after this code was executed (TODO))
            bool result = AllocConsole();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true }); // ConsoleUI is forwarded to the normal Console unless explicitly redefined
        }

        private void buttonSendToConsole_Click(object sender, EventArgs e)
        {
            InitReader(textBoxCommandLine.Text + "\n");

            ConsoleUI.outWriter.WriteLine("> " + textBoxCommandLine.Text);

            ExecuteSingleCommand();
        }

        private void ExecuteSingleCommand()
        {
            try
            {
                driver.conditionalEvaluationResults.Push(true);

                shell.ParseShellCommand();

                driver.conditionalEvaluationResults.Pop();
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("exit due to " + ex.Message);
                int errorCode = -2;
                ShowErrorDialog(errorCode);
            }
            finally
            {
                impl.Cleanup();
            }
        }

        private void ExecuteCommandsFromFile()
        {
            try
            {
                driver.conditionalEvaluationResults.Push(true);

                while(!driver.Quitting && !driver.Eof)
                {
                    bool success = shell.ParseShellCommand();
                    BecomeInteractiveShellInCaseOfEofOrError(success);
                }

                driver.conditionalEvaluationResults.Pop();
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("exit due to " + ex.Message);
                ShowErrorDialog(-2);
            }
            finally
            {
                impl.Cleanup();
            }
        }

        private void BecomeInteractiveShellInCaseOfEofOrError(bool success)
        {
            if(readFromConsole || (!driver.Eof && success))
                return;

            InitReader("");

            scriptFilename = null; // become an interactive shell (in case of a script file executing shell, otherwise stay an interactive shell, just re-init)
            showPrompt = true;
            readFromConsole = true;

            textBoxCommandLine.Enabled = true;
            buttonSendToConsole.Enabled = true;
        }

        private void InitReader(String text)
        {
            TextReader newReader = new StringReader(text);
            shell.ReInit(newReader);
            driver.tokenSources.Pop();
            driver.tokenSources.Push(shell.token_source);
            reader.Close();
            driver.Eof = false;
            reader = newReader;
        }

        private void ShowErrorDialog(int errorCode)
        {
            string message = "Error code " + errorCode;
            string caption = "Error occured";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, caption, buttons);
        }

        private void buttonExecuteMutexInDebugger_Click(object sender, EventArgs e)
        {
            buttonOpenShell.Enabled = false;
            buttonExecuteMutexInShell.Enabled = false;

            AllocateConsole(); // opens console window

            LGSPNamedGraph graph;
            LGSPGraphProcessingEnvironment procEnv;
            InitMutexViaAPI(out graph, out procEnv);

            ConsoleDebugger debugger = OpenDebugger(graph, procEnv, GraphViewerTypes.YComp); // Let yComp observe any changes to the graph, and execute the sequence step by step

            PrintAndWait("Initial 2-process ring constructed. Press key to debug sequence.", debugger);

            Sequence sequence = procEnv.ParseSequence("newRule[8] && mountRule && requestRule[10] | (takeRule && releaseRule && giveRule)[10]");
            debugger.InitNewRewriteSequence(sequence, true);
            procEnv.ApplyGraphRewriteSequence(sequence);
            sequence.ResetExecutionState();

            MessageBox.Show("Done executing ApplicationExampleMutex example in debugger", "Execution in Debugger completed", MessageBoxButtons.OK);
        }

        private void buttonExecuteMutexInMSAGLDebugger_Click(object sender, EventArgs e)
        {
            buttonOpenShell.Enabled = false;
            buttonExecuteMutexInShell.Enabled = false;

            LGSPNamedGraph graph;
            LGSPGraphProcessingEnvironment procEnv;
            InitMutexViaAPI(out graph, out procEnv);

            // opens a windows forms debugger console
            ConsoleDebugger debugger = OpenDebugger(graph, procEnv, GraphViewerTypes.MSAGL); // Let MSAGL observe any changes to the graph, and execute the sequence step by step

            PrintAndWait("Initial 2-process ring constructed. Press key to debug sequence.", debugger);

            Sequence sequence = procEnv.ParseSequence("newRule[8] && mountRule && requestRule[10] | (takeRule && releaseRule && giveRule)[10]");
            debugger.InitNewRewriteSequence(sequence, true);
            procEnv.ApplyGraphRewriteSequence(sequence);
            sequence.ResetExecutionState();

            MessageBox.Show("Done executing ApplicationExampleMutex example in msagl debugger", "Execution in Debugger completed", MessageBoxButtons.OK);
        }

        private void InitMutexViaAPI(out LGSPNamedGraph graph, out LGSPGraphProcessingEnvironment procEnv)
        {
            LGSPGlobalVariables globalVars = new LGSPGlobalVariables();
            LGSPActions actions;

            try
            {
                LGSPBackend.Instance.CreateNamedFromSpec("ApplicationExampleMutex.grg", globalVars, null, 0, out graph, out actions);
                procEnv = new LGSPGraphProcessingEnvironment(graph, actions);
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to create graph from specification: " + ex.Message);
                graph = null;
                procEnv = null;
                return;
            }

            NodeType processType = graph.GetNodeType("Process");
            EdgeType nextType = graph.GetEdgeType("next");

            LGSPNode p1 = graph.AddLGSPNode(processType);
            LGSPNode p2 = graph.AddLGSPNode(processType);
            graph.AddEdge(nextType, p1, p2);
            graph.AddEdge(nextType, p2, p1);
        }

        private static ConsoleDebugger OpenDebugger(INamedGraph graph, IGraphProcessingEnvironment procEnv, GraphViewerTypes graphViewerType)
        {
            Dictionary<String, String> optMap = new Dictionary<String, String>();
            DebuggerGraphProcessingEnvironment debuggerProcEnv = new DebuggerGraphProcessingEnvironment(graph, procEnv);

            DebuggerEnvironment debuggerEnv = null;
            ConsoleDebugger debugger = null;
            if(graphViewerType == GraphViewerTypes.YComp)
            {
                debuggerEnv = new DebuggerEnvironment(DebuggerConsoleUI.Instance);
                debugger = new ConsoleDebugger(debuggerEnv, debuggerProcEnv, new ElementRealizers(),
                    graphViewerType, "Organic", optMap, null);
            }
            else
            {
                GuiConsoleDebuggerHost host = new GuiConsoleDebuggerHost();
                debuggerEnv = new DebuggerEnvironment(host.GuiConsoleControl);
                debugger = new ConsoleDebugger(debuggerEnv, debuggerProcEnv, new ElementRealizers(),
                    graphViewerType, "SugiyamaScheme", optMap, host);
            }

            debugger.DetailedModeShowPreMatches = true;
            debugger.DetailedModeShowPostMatches = true;
            debuggerEnv.Debugger = debugger;
            return debugger;
        }

        private static void PrintAndWait(String text, ConsoleDebugger debugger)
        {
            if(debugger != null && debugger.GraphViewerClient != null)
                debugger.GraphViewerClient.UpdateDisplay();
            if(debugger != null && debugger.GraphViewerClient != null)
                debugger.GraphViewerClient.Sync();
            debugger.env.WriteLine(text);
            debugger.env.PauseUntilAnyKeyPressed("Press any key to continue...");
        }

        private void buttonExecuteMutex_Click(object sender, EventArgs e)
        {
            LGSPNamedGraph graph;
            LGSPGraphProcessingEnvironment procEnv;
            InitMutexViaAPI(out graph, out procEnv);

            Sequence sequence = procEnv.ParseSequence("newRule[8] && mountRule && requestRule[10] | (takeRule && releaseRule && giveRule)[10]");
            procEnv.ApplyGraphRewriteSequence(sequence);
            sequence.ResetExecutionState();

            MessageBox.Show("Done executing ApplicationExampleMutex example", "Execution completed", MessageBoxButtons.OK);
        }
    }
}
