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
    /// interface to the host object that contains the gui and esp. the console(s) to be used by the gui console debugger
    /// </summary>
    public interface IGuiDebuggerHost : IGuiConsoleDebuggerHost
    {
        IDebuggerGUIForDataRendering MainWorkObjectGuiGraphRenderer { get; }
        IDebuggerConsoleUICombined MainWorkObjectGuiConsoleControl { get; }
        IDebuggerConsoleUICombined InputOutputAndLogGuiConsoleControl { get; }
    }
}
