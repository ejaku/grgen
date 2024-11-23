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
    /// interface to an object that allows to create hosts, for one a host for a (basic) graph viewer (client), for the other a host for a (gui) console debugger (which is an extension of a basic graph viewer client)
    /// you may use GraphViewerClient.GetGuiConsoleDebuggerHostCreator() to obtain a creator object (from graphViewerAndSequenceDebuggerWindowsForms.dll)
    /// </summary>
    public interface IHostCreator
    {
        IGuiConsoleDebuggerHost CreateGuiConsoleDebuggerHost(bool twoPane);
        IBasicGraphViewerClientHost CreateBasicGraphViewerClientHost();
    }
}
