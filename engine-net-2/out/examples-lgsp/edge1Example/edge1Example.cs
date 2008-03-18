using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_edge1;
using de.unika.ipd.grGen.Model_edge1;

namespace edge1
{
    class edge1Example
    {
        LGSPGraph graph;
        edge1Actions actions;

        void DoEdge1()
        {
            graph = new LGSPGraph(new edge1GraphModel());
            actions = new edge1Actions(graph);

            actions.PerformanceInfo = new PerformanceInfo();

            actions.ApplyGraphRewriteSequence("init");

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(actions.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            actions.PerformanceInfo.Reset();

            LGSPMatches matches = actions.GetAction("findArbitraryDirectedEdge").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            edge1Example edge1 = new edge1Example();
            edge1.DoEdge1();
        }
    }
}
