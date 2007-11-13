using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.test;
using de.unika.ipd.grGen.actions.test;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            LGSPGraph graph = new LGSPGraph(new testGraphModel());
            graph.ReuseOptimization = false;
            LGSPActions actions = new testActions(graph);

            Node_Process p1 = Node_Process.CreateNode(graph);
            p1.name = "Siegfried";
            p1.val = 67;

            LGSPNode p2 = graph.AddNode(NodeType_Process.typeVar);
            p2.SetAttribute("name", "Dieter");
            if((int) p2.GetAttribute("val") == 0)
                p2.SetAttribute("val", 9);

            Edge_connection con = (Edge_connection) graph.AddEdge(EdgeType_connection.typeVar, p1, p2);
            con.bandwidth = 1000 + p1.val + p1.name.Length;

            Action_testRule.Instance.Apply(graph);

            using(VCGDumper dumper = new VCGDumper("test.vcg"))
                graph.Dump(dumper);
        }
    }
}
