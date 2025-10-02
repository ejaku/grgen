/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

using System;
using System.Windows.Forms;
using de.unika.ipd.grGen.grShell;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace ShellExampleWindowsForms
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

            ShellForm shell = new ShellForm();
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
