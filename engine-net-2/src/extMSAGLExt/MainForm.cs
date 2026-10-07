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
                await receiver.RunAsync(serverPort, msaglExtClient);
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

        void MenuFileClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ---- vcg import ----

        void ImportVcgFile(string path)
        {
            receiver?.Stop();
            receiver = null;
            msaglExtClient.ClearGraph();

            string error = VcgImporter.Import(path, msaglExtClient);
            if(error != null)
                MessageBox.Show(error, "VCG Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
