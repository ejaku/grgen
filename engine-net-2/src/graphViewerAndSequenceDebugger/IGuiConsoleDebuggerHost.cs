/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// interface to the host object that contains the console(s) to be used by the gui console debugger
    /// </summary>
    public interface IGuiConsoleDebuggerHost
    {
        IDebuggerConsoleUICombined GuiConsoleControl { get; }
        IDebuggerConsoleUICombined OptionalGuiConsoleControl { get; }
        bool TwoPane { get; set; }
        void Show();
        void Close();
        IDebugger Debugger { get; set; } // allows to set the debugger the implementation of this interface is intended to host
    }
}
