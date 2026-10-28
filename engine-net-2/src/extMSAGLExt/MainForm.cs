// by Claude Code with Edgar Jakumeit

using System;
using System.IO;
using System.Windows.Forms;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    public partial class MainForm : Form
    {
        // the server mode can only be entered at startup, once left it cannot be re-entered
        enum ApplicationState
        {
            Idle,
            ShowingFile, // watching or not watching the file (watcher != null)
            ServerWaiting,
            ServerConnected,
            ServerEnded // the graph stays open for inspection
        }

        ApplicationState state = ApplicationState.Idle;

        MSAGLExtClient msaglExtClient; // null in state Idle (the idleLabel is shown instead)
        GraphCommandReceiver receiver; // only non-null while the server mode is underway

        string currentFilePath; // full path of the file shown in state ShowingFile
        FileSystemWatcher watcher; // only non-null while the current file is watched
        int reloadRetries;
        const int MaxReloadRetries = 10; // the file may still be locked by its writer

        readonly string startupVcgFile;
        readonly bool watchStartupVcgFile;
        readonly int serverPort;
        readonly bool showErrorLogConsoleAtStartup;

        public MainForm(string vcgFile, bool watchVcgFile, int port, bool showErrorLogConsole)
        {
            startupVcgFile = vcgFile;
            watchStartupVcgFile = watchVcgFile;
            serverPort = port;
            showErrorLogConsoleAtStartup = showErrorLogConsole;
            InitializeComponent();
            LoadWindowIcon();
            UpdateStateDisplay();
        }

        // ApplicationIcon only sets the icon of the executable file; the window (title bar) icon must be set explicitly
        void LoadWindowIcon()
        {
            using(System.IO.Stream iconStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("grgen.ico"))
            {
                if(iconStream != null)
                    Icon = new System.Drawing.Icon(iconStream);
            }
        }

        // async void: the form load event handler starts the receiver coroutine on the UI thread.
        // All await continuations in RunAsync run via the WinForms SynchronizationContext --
        // no Invoke needed, and ConfigureAwait(false) must NOT be used anywhere in the receiver.
        async void MainForm_Load(object sender, EventArgs e)
        {
            if(showErrorLogConsoleAtStartup)
                ShowErrorLogConsole();

            if(startupVcgFile != null)
            {
                OpenFile(startupVcgFile);
                if(watchStartupVcgFile && state == ApplicationState.ShowingFile)
                    StartWatching();
            }
            else if(serverPort > 0)
            {
                GraphCommandReceiver serverReceiver = new GraphCommandReceiver();
                string error = serverReceiver.StartListening(serverPort);
                if(error != null)
                {
                    MessageBox.Show(error, "extMSAGLExt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                receiver = serverReceiver;
                ShowGraphViewer(new MSAGLExtClient());
                EnterState(ApplicationState.ServerWaiting);
                receiver.ClientConnected += () => EnterState(ApplicationState.ServerConnected);

                bool exitReceived = await receiver.RunAsync(msaglExtClient);

                if(receiver == serverReceiver) // otherwise the server mode was already left by opening a file
                {
                    receiver = null;
                    if(exitReceived) // the client ended the debugging session regularly, so the application is closed
                        Close();
                    else // the connection broke, the graph stays open for inspection
                        EnterState(ApplicationState.ServerEnded);
                }
            }
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopWatching();
            receiver?.Stop();
        }

        // ---- application state and its display ----

        void EnterState(ApplicationState newState)
        {
            state = newState;
            UpdateStateDisplay();
        }

        void UpdateStateDisplay()
        {
            switch(state)
            {
                case ApplicationState.Idle:
                    statusLabelState.Text = "Idle";
                    break;
                case ApplicationState.ShowingFile:
                    if(watcher != null)
                        statusLabelState.Text = "Showing file " + currentFilePath + ", watching it in order to reload it on changes";
                    else
                        statusLabelState.Text = "Showing file " + currentFilePath;
                    break;
                case ApplicationState.ServerWaiting:
                    statusLabelState.Text = "Server mode, listening on port " + serverPort + ", waiting for the client";
                    break;
                case ApplicationState.ServerConnected:
                    statusLabelState.Text = "Server mode, client connected, debugging underway";
                    break;
                case ApplicationState.ServerEnded:
                    statusLabelState.Text = "Server mode ended, client disconnected";
                    break;
            }

            menuItemReloadOnChanges.Enabled = state == ApplicationState.ShowingFile;
            menuItemReloadOnChanges.Checked = watcher != null;
            menuItemClose.Enabled = state == ApplicationState.ShowingFile;
        }

        // the action is shown while the UI thread is blocked by it, so the status bar is repainted immediately
        void ShowAction(string action)
        {
            statusLabelAction.Text = action ?? "";
            statusLabelAction.Visible = action != null;
            statusStrip.Refresh();
        }

        // ---- menu handlers ----

        void MenuFileOpen_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open VCG File";
                dlg.Filter = "VCG files (*.vcg)|*.vcg|All files (*.*)|*.*";
                dlg.DefaultExt = "vcg";
                if(dlg.ShowDialog(this) != DialogResult.OK) return;
                OpenFile(dlg.FileName);
            }
        }

        void MenuFileReloadOnChanges_Click(object sender, EventArgs e)
        {
            if(watcher != null)
                StopWatching();
            else
                StartWatching();
            UpdateStateDisplay();
        }

        void MenuFileClose_Click(object sender, EventArgs e)
        {
            Console.Out.WriteLine("VCG close, lastly rendered file was \"" + currentFilePath + "\".");
            StopWatching();
            ShowGraphViewer(null);
            currentFilePath = null;
            EnterState(ApplicationState.Idle);
        }

        // allocates a console window that receives the console output from then on
        // (it is normally printed in the parent console, or hidden when there is none)
        void MenuFileShowErrorLog_Click(object sender, EventArgs e)
        {
            ShowErrorLogConsole();
        }

        void ShowErrorLogConsole()
        {
            de.unika.ipd.grGen.libConsoleAndOS.WorkaroundManager.Workaround.AllocTheConsole();
            Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Console.SetError(new System.IO.StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
            menuItemShowErrorLog.Enabled = false;
        }

        void MenuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ---- drag'n'drop of a vcg file onto the idle placeholder ----

        void IdleLabel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = GetDroppedVcgFile(e) != null ? DragDropEffects.Copy : DragDropEffects.None;
        }

        void IdleLabel_DragDrop(object sender, DragEventArgs e)
        {
            string path = GetDroppedVcgFile(e);
            if(path != null)
                OpenFile(path);
        }

        // returns the path of the dropped file if exactly one file with extension .vcg is dropped, otherwise null
        static string GetDroppedVcgFile(DragEventArgs e)
        {
            if(!e.Data.GetDataPresent(DataFormats.FileDrop))
                return null;
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if(files == null || files.Length != 1)
                return null;
            if(!files[0].EndsWith(".vcg", StringComparison.OrdinalIgnoreCase))
                return null;
            return files[0];
        }

        // ---- vcg import ----

        // replaces the graph viewer control by the given one (a cleared one would still contain the realizers registered so far),
        // or shows the idle placeholder in case of null
        void ShowGraphViewer(MSAGLExtClient newMsaglExtClient)
        {
            if(msaglExtClient != null)
            {
                Controls.Remove(msaglExtClient);
                msaglExtClient.Dispose();
            }

            msaglExtClient = newMsaglExtClient;
            if(msaglExtClient != null)
            {
                msaglExtClient.Dock = DockStyle.Fill;
                msaglExtClient.Name = "msaglExtClient";
                msaglExtClient.TabIndex = 1;
                Controls.Add(msaglExtClient);
                Controls.SetChildIndex(msaglExtClient, 0); // the front-most control with Dock.Fill is laid out last, i.e. inside the menu and status strips
            }
            idleLabel.Visible = msaglExtClient == null;
        }

        void OpenFile(string path)
        {
            // end a potentially running debugging session and a potentially underway watching
            StopWatching();
            receiver?.Stop();
            receiver = null;

            string fullPath = Path.GetFullPath(path);
            ShowAction("Opening file " + fullPath);
            MSAGLExtClient newMsaglExtClient = new MSAGLExtClient();
            string error = VcgImporter.Import(fullPath, newMsaglExtClient);
            ShowAction(null);

            if(error != null)
            {
                newMsaglExtClient.Dispose();
                ShowGraphViewer(null);
                currentFilePath = null;
                EnterState(ApplicationState.Idle);
                MessageBox.Show(error, "VCG Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ShowGraphViewer(newMsaglExtClient);
            currentFilePath = fullPath;
            EnterState(ApplicationState.ShowingFile);
        }

        // ---- watching of the current file ----

        // only existing files can be watched (watching stays off otherwise)
        void StartWatching()
        {
            if(!File.Exists(currentFilePath))
            {
                Console.Error.WriteLine("Cannot watch \"" + currentFilePath + "\", the file does not exist (anymore).");
                return;
            }

            try
            {
                watcher = new FileSystemWatcher(Path.GetDirectoryName(currentFilePath), Path.GetFileName(currentFilePath));
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.CreationTime;
                watcher.Changed += new FileSystemEventHandler(Watcher_FileChanged);
                watcher.Created += new FileSystemEventHandler(Watcher_FileChanged);
                watcher.Deleted += new FileSystemEventHandler(Watcher_FileChanged);
                watcher.Renamed += new RenamedEventHandler(Watcher_FileChanged);
                watcher.EnableRaisingEvents = true;
                Console.Out.WriteLine("Watching \"" + currentFilePath + "\".");
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Cannot watch \"" + currentFilePath + "\": " + ex.Message);
                StopWatching();
            }
        }

        void StopWatching()
        {
            reloadTimer.Stop();
            reloadRetries = 0;
            if(watcher == null)
                return;
            Console.Out.WriteLine("Stop watching \"" + currentFilePath + "\".");
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
            watcher = null;
        }

        // called on a thread pool thread; all kinds of changes (incl. deletion and renaming) are handled after the reload delay
        void Watcher_FileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                if(!IsDisposed)
                    BeginInvoke(new Action(RestartReloadTimer));
            }
            catch(InvalidOperationException) // the window handle was already destroyed
            {
            }
        }

        // a change notification (re)starts the timer, so the reload happens only after the file modifications ended
        void RestartReloadTimer()
        {
            if(watcher == null) // notification arrived after watching was stopped
                return;
            reloadTimer.Stop();
            reloadTimer.Start();
        }

        void ReloadTimer_Tick(object sender, EventArgs e)
        {
            reloadTimer.Stop();
            if(watcher == null)
                return;

            if(!File.Exists(currentFilePath))
            {
                Console.Error.WriteLine("The watched file \"" + currentFilePath + "\" is not available anymore, stopped watching it.");
                StopWatching();
                UpdateStateDisplay();
                return;
            }

            string text;
            try
            {
                string fileSizeError = VcgImporter.CheckFileSize(currentFilePath);
                if(fileSizeError != null)
                {
                    Console.Error.WriteLine("Reload refused, keeping the previous graph: " + fileSizeError);
                    return;
                }
                text = File.ReadAllText(currentFilePath);
            }
            catch(IOException ex) // e.g. still locked by the writer, or deleted in between -- retry later
            {
                if(++reloadRetries <= MaxReloadRetries)
                    reloadTimer.Start();
                else
                {
                    Console.Error.WriteLine("Cannot reload \"" + currentFilePath + "\": " + ex.Message);
                    reloadRetries = 0;
                }
                return;
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine("Cannot reload \"" + currentFilePath + "\": " + ex.Message);
                return;
            }
            reloadRetries = 0;

            // the new graph is built in a fresh graph viewer, the previous graph stays open in case of an error
            Console.Out.WriteLine("VCG reimport of \"" + currentFilePath + "\".");
            ShowAction("Reopening changed file");
            MSAGLExtClient newMsaglExtClient = new MSAGLExtClient();
            string error = VcgImporter.ImportFromText(text, newMsaglExtClient);
            ShowAction(null);

            if(error != null)
            {
                newMsaglExtClient.Dispose();
                Console.Error.WriteLine("Reload of \"" + currentFilePath + "\" failed, keeping the previous graph: " + error);
                return;
            }

            ShowGraphViewer(newMsaglExtClient);
        }
    }
}
