/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Action_TNT;
using de.unika.ipd.grGen.Model_TNT;

namespace TNT
{
    class TNTExample
    {
        LGSPGraph graph;
        TNTActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        void DoTNT()
        {
            graph = new LGSPGraph(new TNTGraphModel(), new LGSPGlobalVariables());
            actions = new TNTActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            // use graph rewrite sequence
            procEnv.ApplyGraphRewriteSequence("createTNT");

            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            // use old inexact interface
            IMatches matchesInexact = actions.GetAction("TNT").Match(procEnv, 0, null);
            ConsoleUI.outWriter.WriteLine(matchesInexact.Count + " matches found.");

            // use new 2.5 exact interface
            IMatchesExact<Rule_ToluolCore.IMatch_ToluolCore> matchesExact = actions.ToluolCore.Match(procEnv, 0);
            ConsoleUI.outWriter.WriteLine(matchesExact.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            TNTExample tnt = new TNTExample();
            tnt.DoTNT();
        }
    }
}
