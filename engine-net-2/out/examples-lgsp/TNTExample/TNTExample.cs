using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.actions.TNT;
using de.unika.ipd.grGen.models.TNT;

namespace TNT
{
    class TNTExample
    {
        LGSPGraph graph;
        TNTActions actions;

        void DoTNT()
        {
            graph = new LGSPGraph(new TNTGraphModel());
            actions = new TNTActions(graph);

            actions.PerformanceInfo = new PerformanceInfo();

            actions.ApplyGraphRewriteSequence("createTNT");

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(actions.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            actions.PerformanceInfo.Reset();

            LGSPMatches matches = actions.GetAction("TNT").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            TNTExample tnt = new TNTExample();
            tnt.DoTNT();
        }
    }
}
