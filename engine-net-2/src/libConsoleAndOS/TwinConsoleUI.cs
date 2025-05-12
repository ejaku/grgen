/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    // (twin) ConsoleUI for user dialog
    public interface ITwinConsoleUIInputOutputConsole
    {
        // outWriter (errorOutWriter not used in interactive debugger)
        void Write(string value);
        void Write(string format, params object[] arg);
        void WriteLine(string value);
        void WriteLine(string format, params object[] arg);
        void WriteLine();

        // consoleOut for user dialog
        void PrintHighlightedUserDialog(String text, HighlightingMode mode);

        // inReader
        string ReadLine();

        // consoleIn
        ConsoleKeyInfo ReadKey(bool intercept);
        ConsoleKeyInfo ReadKeyWithControlCAsInput();
        bool KeyAvailable { get; }
    }

    public interface ITwinConsoleUIDataRenderingBase // TODO: really worthwhile?
    {
        // base version of outWriter for data rendering, TODO: also implemented by the graph GUI version, resulting in a list of lines
        void WriteLineDataRendering(string value);
        void WriteLineDataRendering(string format, params object[] arg);
        void WriteLineDataRendering();

        void Clear();

        // flicker prevention/performance optimization
        void SuspendImmediateExecution();
        void RestartImmediateExecution();
    }

    // (twin) ConsoleUI for data rendering (of the main work object)
    public interface ITwinConsoleUIDataRenderingConsole : ITwinConsoleUIDataRenderingBase
    {
        // outWriter for data rendering (errorOutWriter not used in interactive debugger and even less in data rendering)
        void WriteDataRendering(string value);
        void WriteDataRendering(string format, params object[] arg);
        //void WriteLineDataRendering(string value);
        //void WriteLineDataRendering(string format, params object[] arg);
        //void WriteLineDataRendering();

        // consoleOut
        void PrintHighlighted(String text, HighlightingMode mode);

        //void Clear();

        // inReader and consoleIn are not used in data rendering

        // flicker prevention/performance optimization
        //void SuspendImmediateExecution();
        //void RestartImmediateExecution();
    }

    public interface ITwinConsoleUICombinedConsole : ITwinConsoleUIInputOutputConsole, ITwinConsoleUIDataRenderingConsole
    {
    }
}
