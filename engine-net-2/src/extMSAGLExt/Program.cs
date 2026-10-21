// by Claude Code with Edgar Jakumeit

using System;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    static class Program
    {
        const string Usage = "Usage:\n  extMSAGLExt [-console] [<vcg-file>]\n  extMSAGLExt [-console] -p <port>\n\n-console: show the error log console from the start";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string vcgFile = null;
            int port = -1;
            bool showErrorLogConsole = false;

            for(int i = 0; i < args.Length; ++i)
            {
                if(args[i] == "-console")
                {
                    showErrorLogConsole = true;
                }
                else if(args[i] == "-p" && port == -1 && vcgFile == null && i + 1 < args.Length)
                {
                    ++i;
                    if(!int.TryParse(args[i], out port) || port < 4242 || port > 4251)
                    {
                        MessageBox.Show("Invalid port number: " + args[i] + " (must be in the range 4242-4251)\n" + Usage,
                            "extMSAGLExt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if(!args[i].StartsWith("-") && vcgFile == null && port == -1)
                {
                    vcgFile = args[i];
                }
                else
                {
                    MessageBox.Show(Usage, "extMSAGLExt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Application.Run(new MainForm(vcgFile, port, showErrorLogConsole));
        }
    }
}
