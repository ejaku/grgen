/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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

            // use graph rewrite sequence
            actions.ApplyGraphRewriteSequence("createTNT");

			Console.WriteLine(graph.PerformanceInfo.MatchesFound + " matches found.");
			Console.WriteLine(graph.PerformanceInfo.RewritesPerformed + " rewrites performed.");
			graph.PerformanceInfo.Reset();

            // use old inexact interface
            IMatches matchesInexact = actions.GetAction("TNT").Match(graph, 0, null);
            Console.WriteLine(matchesInexact.Count + " matches found.");

            // use new 2.5 exact interface
            IMatchesExact<Rule_ToluolCore.IMatch_ToluolCore> matchesExact = actions.ToluolCore.Match(graph, 0);
            Console.WriteLine(matchesExact.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            TNTExample tnt = new TNTExample();
            tnt.DoTNT();
        }
    }
}
