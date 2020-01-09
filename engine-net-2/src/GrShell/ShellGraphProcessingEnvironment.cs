/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.grShell
{
    public class ShellGraphProcessingEnvironment
    {
        public IGraphProcessingEnvironment ProcEnv;

        public DumpInfo DumpInfo;
        public SubruleDebuggingConfiguration SubruleDebugConfig;
        public VCGFlags VcgFlags = VCGFlags.OrientTopToBottom | VCGFlags.EdgeLabels;

        public String BackendFilename;
        public String[] BackendParameters;
        public String ModelFilename;
        public String ActionsFilename = null;

        public Dictionary<string, INamedGraph> NameToSubgraph = new Dictionary<string, INamedGraph>(); // maps subgraph name to subgraph


        public ShellGraphProcessingEnvironment(IGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
        {
            LGSPNamedGraph Graph = new LGSPNamedGraph((LGSPGraph)graph);
            DumpInfo = new DumpInfo(Graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
            ProcEnv = new LGSPGraphProcessingEnvironment(Graph, null);
            NameToSubgraph.Add(Graph.Name, Graph);
        }

        public ShellGraphProcessingEnvironment(INamedGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
        {
            LGSPNamedGraph Graph = (LGSPNamedGraph)graph;
            DumpInfo = new DumpInfo(Graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
            ProcEnv = new LGSPGraphProcessingEnvironment(Graph, null);
            NameToSubgraph.Add(Graph.Name, Graph);
        }

        public ShellGraphProcessingEnvironment Clone(string name)
        {
            string realname = (name == null) ? ProcEnv.Graph.Name + "-clone" : name;
            ShellGraphProcessingEnvironment result = new ShellGraphProcessingEnvironment(ProcEnv.Graph.Clone(realname),
                BackendFilename, BackendParameters, ModelFilename);
            result.ProcEnv.Actions = this.ProcEnv.Actions;
            result.DumpInfo = this.DumpInfo;
            result.SubruleDebugConfig = this.SubruleDebugConfig;
            result.VcgFlags = this.VcgFlags;
            result.ActionsFilename = this.ActionsFilename;
            result.ProcEnv.EmitWriter = this.ProcEnv.EmitWriter;
            return result;
        }
    }
}
