using System;
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

/*            Node_Process p1 = Node_Process.CreateNode(graph);
            p1.name = "Siegfried";
            p1.val = 67;

            LGSPNode p2 = graph.AddNode(NodeType_Process.typeVar);
            p2.SetAttribute("name", "Dieter");
            if((int) p2.GetAttribute("val") == 0)
                p2.SetAttribute("val", 9);

//            INode_Process p1_attr = (INode_Process) p1.attributes;
//            p1_attr.name = "Siegfried";
//            p1_attr.val = 67;

            Edge_connection con = (Edge_connection) graph.AddEdge(EdgeType_connection.typeVar, p1, p2);
            con.bandwidth = 1000 + p1.val + p1.name.Length;*/

            Node_D231_4121 n1 = Node_D231_4121.CreateNode(graph);
            n1.a2 = 2;
            n1.a4 = 4;
            n1.a5 = 5;
            n1.b23 = 23;
            n1.b41 = 41;
            n1.b42 = 42;
            n1.d231_4121 = 231;

            Node_B21 n2 = Node_B21.CreateNode(graph);
            n2.a2 = 10002;
            n2.b21 = 10021;

            Node_D2211_2222_31 n3 = Node_D2211_2222_31.CreateNode(graph);
            n3.a2 = 20002;
            n3.a3 = 20003;
            n3.a4 = 20004;
            n3.b22 = 20022;
            n3.b41 = 20041;
            n3.c221 = 20221;
            n3.c222_411 = 20222;
            n3.d2211_2222_31 = 22221;

            Edge_Edge.CreateEdge(graph, n1, n2);
            Edge_Edge.CreateEdge(graph, n2, n3);

            Action_testRule.Instance.Apply(graph);

            using(VCGDumper dumper = new VCGDumper("test.vcg"))
                graph.Dump(dumper);
        }
    }
}
