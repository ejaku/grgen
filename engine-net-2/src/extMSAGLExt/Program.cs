// by Claude Code with Edgar Jakumeit

using System;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string vcgFile = null;
            int port = -1;

            if(args.Length == 1)
            {
                vcgFile = args[0];
            }
            else if(args.Length == 2 && args[0] == "-p")
            {
                if(!int.TryParse(args[1], out port) || port <= 0 || port > 65535)
                {
                    MessageBox.Show("Invalid port number: " + args[1] + "\nUsage: extMSAGLExt -p <port>",
                        "extMSAGLExt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if(args.Length != 0)
            {
                MessageBox.Show("Usage:\n  extMSAGLExt <vcg-file>\n  extMSAGLExt -p <port>",
                    "extMSAGLExt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.Run(new MainForm(vcgFile, port));
        }
    }
}
