/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// interface to an object that allows to create hosts, 
    /// for one a host for a (basic) graph viewer (client) (that is required for a pure graph viewer, and a part required for a full debugger based on one of the following debuggers), 
    /// for the other a host for a gui console debugger,
    /// or a host for a gui debugger (which is an extension of a gui console debugger)
    /// you may use GraphViewerClient.GetGuiConsoleDebuggerHostCreator() to obtain a creator object (from graphViewerAndSequenceDebuggerWindowsForms.dll)
    /// </summary>
    public interface IHostCreator
    {
        IBasicGraphViewerClientHost CreateBasicGraphViewerClientHost();
        IGuiConsoleDebuggerHost CreateGuiConsoleDebuggerHost(bool twoPane);
        IGuiDebuggerHost CreateGuiDebuggerHost();
    }
}
