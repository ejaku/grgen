/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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

        // the debugger shows internal class objects to the user including a unique name, and offers to access the objects by name
        // (in addition the shell has commands to define objects with name/uniqueId and access them by name/uniqueId)
        // - so we create object names in case the model does not supply them
        // - so we request the name to object index
        // (in the constructor calls below) (note that some objects are simply not visited by the debugger, so only some objects may receive a name)
        public readonly ObjectNamerAndIndexer objectNamerAndIndexer; // maps name to class object and the other way round
        public readonly TransientObjectNamerAndIndexer transientObjectNamerAndIndexer; // maps name to transient class object and the other way round

        // the graph handed in is only used as source to create a named graph from (which will be operated on thereafter)
        // the debugger (/the shell) supports only named graphs
        public DebuggerGraphProcessingEnvironment(IGraph unnamedGraph)
        {
            INamedGraph graph = LGSPBackend.Instance.CreateNamedGraph(unnamedGraph);
            DumpInfo = new DumpInfo(graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            ProcEnv = LGSPBackend.Instance.CreateGraphProcessingEnvironment(graph, null);
            NameToSubgraph.Add(graph.Name, graph);
            objectNamerAndIndexer = new ObjectNamerAndIndexer(!graph.Model.ObjectUniquenessIsEnsured, true, graph.Model.ObjectUniquenessIsEnsured);
            transientObjectNamerAndIndexer = new TransientObjectNamerAndIndexer();
        }

        public DebuggerGraphProcessingEnvironment(INamedGraph graph)
        {
            DumpInfo = new DumpInfo(graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            ProcEnv = LGSPBackend.Instance.CreateGraphProcessingEnvironment(graph, null);
            NameToSubgraph.Add(graph.Name, graph);
            objectNamerAndIndexer = new ObjectNamerAndIndexer(!graph.Model.ObjectUniquenessIsEnsured, true, graph.Model.ObjectUniquenessIsEnsured);
            transientObjectNamerAndIndexer = new TransientObjectNamerAndIndexer();
        }

        public DebuggerGraphProcessingEnvironment(INamedGraph graph, IGraphProcessingEnvironment procEnv)
        {
            DumpInfo = new DumpInfo(graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            ProcEnv = procEnv;
            NameToSubgraph.Add(graph.Name, graph);
            objectNamerAndIndexer = new ObjectNamerAndIndexer(!graph.Model.ObjectUniquenessIsEnsured, true, graph.Model.ObjectUniquenessIsEnsured);
            transientObjectNamerAndIndexer = new TransientObjectNamerAndIndexer();
        }
    }
}
