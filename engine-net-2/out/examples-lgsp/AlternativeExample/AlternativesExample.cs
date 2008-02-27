using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_Alternatives;
using de.unika.ipd.grGen.Model_Alternatives;

namespace Alternatives
{
    class AlternativeExample
    {
        LGSPGraph graph;
        AlternativesActions actions;

        void DoAlt()
        {
            graph = new LGSPGraph(new AlternativesGraphModel());
            actions = new AlternativesActions(graph);

            actions.PerformanceInfo = new PerformanceInfo();

            actions.ApplyGraphRewriteSequence("createComplex");

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(actions.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            actions.PerformanceInfo.Reset();

            LGSPMatches matches = actions.GetAction("Complex").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            AlternativeExample alt = new AlternativeExample();
            alt.DoAlt();
        }
    }
}
