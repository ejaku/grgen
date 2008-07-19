/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_TNT;
using de.unika.ipd.grGen.Model_TNT;

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

            graph.PerformanceInfo = new PerformanceInfo();

            actions.ApplyGraphRewriteSequence("createTNT");

			Console.WriteLine(graph.PerformanceInfo.MatchesFound + " matches found.");
			Console.WriteLine(graph.PerformanceInfo.RewritesPerformed + " rewrites performed.");
			graph.PerformanceInfo.Reset();

            LGSPMatches matches = actions.GetAction("TNT").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " matches found.");

            matches = actions.GetAction("ToluolCore").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            TNTExample tnt = new TNTExample();
            tnt.DoTNT();
        }
    }
}
