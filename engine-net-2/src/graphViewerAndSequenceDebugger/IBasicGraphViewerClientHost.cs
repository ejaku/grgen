/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// interface to the host object that will contain the basic graph viewer client (also visually)
    /// </summary>
    public interface IBasicGraphViewerClientHost
    {
        // the dynamic type must be additionally a WindowsForms form
        void Close();

        IBasicGraphViewerClient BasicGraphViewerClient { get; set; } // allows to set the basic graph viewer client the implementation of this interface is intended to host
    }
}
