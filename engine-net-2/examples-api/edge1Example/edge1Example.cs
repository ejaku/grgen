/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
        StdGraph graph;
        edge1Actions actions;
        LGSPGraphProcessingEnvironment procEnv;

        void DoEdge1()
        {
            graph = new StdGraph(new LGSPGlobalVariables());
            actions = new edge1Actions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            // use graph rewrite sequence
            procEnv.ApplyGraphRewriteSequence("init3");

            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            // use old inexact interface
            IMatches matches = actions.GetAction("findTripleCircle").Match(procEnv, 0, null);
            ConsoleUI.outWriter.WriteLine(matches.Count + " matches found.");

            // use new exact interface
            IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matchesExact =
                actions.findTripleCircle.Match(procEnv, 0);
            ConsoleUI.outWriter.WriteLine(matchesExact.Count + " matches found.");
            actions.findTripleCircle.Modify(procEnv, matchesExact.FirstExact); // rewrite first match (largely nop, as findTripleCircle is a test)
        }

        static void Main(string[] args)
        {
            edge1Example edge1 = new edge1Example();
            edge1.DoEdge1();
        }
    }
}
