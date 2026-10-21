// by Claude Code with Edgar Jakumeit

using System;
using System.Windows.Forms;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    public partial class MainForm : Form
    {
        MSAGLExtClient msaglExtClient;
        GraphCommandReceiver receiver;

        readonly string startupVcgFile;
        readonly int serverPort;

        public MainForm(string vcgFile, int port)
        {
            startupVcgFile = vcgFile;
            serverPort = port;
            InitializeComponent();
            LoadWindowIcon();
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
            if(startupVcgFile != null)
            {
                ImportVcgFile(startupVcgFile);
            }
            else if(serverPort > 0)
            {
                receiver = new GraphCommandReceiver();
                string error = receiver.StartListening(serverPort);
                if(error != null)
                {
                    MessageBox.Show(error, "extMSAGLExt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    receiver = null;
                    return;
                }
                await receiver.RunAsync(msaglExtClient);
            }
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
                ImportVcgFile(dlg.FileName);
            }
        }

        // allocates a console window that receives the console output from then on
        // (it is normally printed in the parent console, or hidden when there is none)
        void MenuFileShowErrorLog_Click(object sender, EventArgs e)
        {
            de.unika.ipd.grGen.libConsoleAndOS.WorkaroundManager.Workaround.AllocTheConsole();
            Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Console.SetError(new System.IO.StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
            menuItemShowErrorLog.Enabled = false;
        }

        void MenuFileClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ---- vcg import ----

        void ReplaceGraphViewer()
        {
            Controls.Remove(msaglExtClient);
            msaglExtClient.Dispose();

            msaglExtClient = new MSAGLExtClient();
            msaglExtClient.Dock = DockStyle.Fill;
            msaglExtClient.Name = "msaglExtClient";
            msaglExtClient.TabIndex = 1;
            Controls.Add(msaglExtClient);
            Controls.SetChildIndex(msaglExtClient, 0); // the front-most control with Dock.Fill is laid out last, i.e. below the menu strip
        }

        void ImportVcgFile(string path)
        {
            // end a potentially running debugging session, then start with a pristine graph viewer
            // (a cleared graph would still contain the realizers registered so far)
            receiver?.Stop();
            receiver = null;
            ReplaceGraphViewer();

            string error = VcgImporter.Import(path, msaglExtClient);
            if(error != null)
                MessageBox.Show(error, "VCG Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
