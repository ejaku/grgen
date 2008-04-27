/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
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

            actions.PerformanceInfo = new PerformanceInfo();

            actions.ApplyGraphRewriteSequence("createTNT");

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(actions.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            actions.PerformanceInfo.Reset();

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
