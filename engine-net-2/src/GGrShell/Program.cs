using System;
using System.Windows.Forms;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.grShell;

namespace GGrShell
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GGrShellForm shell = new GGrShellForm();
            GuiConsoleControlAsTextReader inReader = new GuiConsoleControlAsTextReader(shell.console);
            GuiConsoleControlAsTextWriter outWriter = new GuiConsoleControlAsTextWriter(shell.console);
            ConsoleUI.inReader = inReader;
            ConsoleUI.outWriter = outWriter;
            ConsoleUI.errorOutWriter = outWriter;
            ConsoleUI.consoleIn = inReader;
            ConsoleUI.consoleOut = outWriter;

            int errorCode = GrShellMainHelper.ConstructShell(args, out shell.shellConfig, out shell.shellComponents);
            if(errorCode != 0)
                throw new Exception("Error during construction of shell"); // TODO: show error dialog?

            shell.reader = inReader;
            shell.writer = outWriter;

            Application.Run(shell);
        }
    }
}
