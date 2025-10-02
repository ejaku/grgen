/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.grShell
{
    public class ShellGraphProcessingEnvironment : DebuggerGraphProcessingEnvironment
    {
        public readonly String BackendFilename;
        public readonly String[] BackendParameters;
        public readonly String ModelFilename;
        public String ActionsFilename = null;


        public ShellGraphProcessingEnvironment(IGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
            : base(graph)
        {
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
        }

        public ShellGraphProcessingEnvironment(INamedGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
            : base(graph)
        {
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
        }
    }
}
