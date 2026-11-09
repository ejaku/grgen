// by Claude Code with Edgar Jakumeit
// this file is part of GrGen.NET

using System;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    static class Program
    {
        const string Usage = "Usage:\n"
            + "  extMSAGLExt [--console] [[--autoreload] <vcg-file>]\n"
            + "  extMSAGLExt [--console] -p <port>\n"
            + "  extMSAGLExt --help\n\n"
            + "--console: show the error log console from the start\n"
            + "--autoreload: watch the file, reload it when it changed\n"
            + "-p <port>: listen on the given port (4242-4251) for the commands of a debugging session\n"
            + "--help: print the available options to standard out";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string vcgFile = null;
            int port = -1;
            bool showErrorLogConsole = false;
            bool watchVcgFile = false;

            for(int i = 0; i < args.Length; ++i)
            {
                if(args[i] == "--help")
                {
                    Console.Out.WriteLine(Usage);
                    return;
                }
                else if(args[i] == "--console")
                {
                    showErrorLogConsole = true;
                }
                else if(args[i] == "--autoreload")
                {
                    watchVcgFile = true;
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

            if(watchVcgFile && vcgFile == null) // --autoreload requires a file (so it is also rejected in server mode)
            {
                MessageBox.Show("--autoreload requires a vcg file\n" + Usage, "extMSAGLExt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm(vcgFile, watchVcgFile, port, showErrorLogConsole));
        }
    }
}
