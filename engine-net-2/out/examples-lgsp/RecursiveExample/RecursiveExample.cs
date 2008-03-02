using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_Recursive;
using de.unika.ipd.grGen.Model_Recursive;

namespace Recursive
{
    class RecursiveExample
    {
        LGSPGraph graph;
        RecursiveActions actions;

        void DoAlt()
        {
            graph = new LGSPGraph(new RecursiveGraphModel());
            actions = new RecursiveActions(graph);

            actions.PerformanceInfo = new PerformanceInfo();

            LGSPMatches matches;
            IGraphElement[] returns;

            LGSPAction createChain = Action_createChain.Instance;
            matches = createChain.Match(graph, 0, null);
            returns = createChain.Modify(graph, matches.matchesList.First);
            Node_Node[] head = new Node_Node[1];
            head[0] = (Node_Node)returns[0];
            matches = actions.GetAction("chainFrom").Match(graph, 0, head);
            Console.WriteLine(matches.Count + " matches found.");

            LGSPAction createBlowball = Action_createBlowball.Instance;
            matches = createBlowball.Match(graph, 0, null);
            returns = createBlowball.Modify(graph, matches.matchesList.First);
            matches = actions.GetAction("blowball").Match(graph, 0, returns);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            RecursiveExample rec = new RecursiveExample();
            rec.DoAlt();
        }
    }
}
