/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_edge1;
using de.unika.ipd.grGen.Model_Std;

namespace edge1
{
    class edge1Example
    {
        Std graph;
        edge1Actions actions;

        void DoEdge1()
        {
            graph = new Std();
            actions = new edge1Actions(graph);

            actions.PerformanceInfo = new PerformanceInfo();

            actions.ApplyGraphRewriteSequence("init3");

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(actions.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            actions.PerformanceInfo.Reset();

            LGSPMatches matches = actions.GetAction("findTripleCircle").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            edge1Example edge1 = new edge1Example();
            edge1.DoEdge1();
        }
    }
}
