/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class DebuggerGraphProcessingEnvironment
    {
        public readonly IGraphProcessingEnvironment ProcEnv;

        public readonly DumpInfo DumpInfo;
        public readonly SubruleDebuggingConfiguration SubruleDebugConfig;
        public VCGFlags VcgFlags = VCGFlags.OrientTopToBottom | VCGFlags.EdgeLabels;

        public readonly Dictionary<string, INamedGraph> NameToSubgraph = new Dictionary<string, INamedGraph>(); // maps subgraph name to subgraph

        public readonly Dictionary<string, IObject> NameToClassObject = new Dictionary<string, IObject>(); // maps "transient" name to class object


        // creates a temporary internal named graph required by the debugger
        public DebuggerGraphProcessingEnvironment(IGraph graph)
        {
            LGSPNamedGraph Graph = new LGSPNamedGraph((LGSPGraph)graph);
            DumpInfo = new DumpInfo(Graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            ProcEnv = new LGSPGraphProcessingEnvironment(Graph, null);
            NameToSubgraph.Add(Graph.Name, Graph);
        }

        public DebuggerGraphProcessingEnvironment(INamedGraph graph)
        {
            LGSPNamedGraph Graph = (LGSPNamedGraph)graph;
            DumpInfo = new DumpInfo(Graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            ProcEnv = new LGSPGraphProcessingEnvironment((LGSPNamedGraph)graph, null);
            NameToSubgraph.Add(Graph.Name, Graph);
        }

        public DebuggerGraphProcessingEnvironment(INamedGraph graph, IGraphProcessingEnvironment procEnv)
        {
            LGSPNamedGraph Graph = (LGSPNamedGraph)graph;
            DumpInfo = new DumpInfo(Graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            ProcEnv = procEnv;
            NameToSubgraph.Add(Graph.Name, Graph);
        }
    }
}
