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
    /// interface to an object that allows to create basic graph viewer clients (internal use by the public GraphViewerClient)
    /// </summary>
    public interface IBasicGraphViewerClientCreator
    {
        // the host must be an object from the IHostCreator (this ensures it is a Form object from WindowsForms, while the static interface stays free of WindowsForms, saving us from this dependency -- you could use your own WindowsForms object implementing IBasicGraphViewerClientHost if you don't care about this dependency)
        // currently, only MSAGL is supported as graphViewerType
        IBasicGraphViewerClient Create(GraphViewerTypes graphViewerType, IBasicGraphViewerClientHost host);
    }
}
