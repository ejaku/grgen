/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;

class HelloMutex
{
    static void Main(string[] args)
    {
        LGSPNamedGraph graph;
        LGSPActions actions;

        new LGSPBackend().CreateNamedFromSpec("Mutex.grg", null, 0, out graph, out actions);

        LGSPGraphProcessingEnvironment procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

        NodeType processType = graph.GetNodeType("Process");
        EdgeType nextType = graph.GetEdgeType("next");

        INode p1 = graph.AddNode(processType);
        INode p2 = graph.AddNode(processType);
        graph.AddEdge(nextType, p1, p2);
        graph.AddEdge(nextType, p2, p1);

        procEnv.ApplyGraphRewriteSequence("newRule[3] && mountRule && requestRule[5] "
            + "&& (takeRule && releaseRule && giveRule)*");

        using(VCGDumper dumper = new VCGDumper("HelloMutex.vcg"))
            GraphDumper.Dump(graph, dumper);
    }
}
