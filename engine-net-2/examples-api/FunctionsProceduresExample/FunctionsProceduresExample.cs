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
using de.unika.ipd.grGen.Action_FunctionsProceduresExample;
using de.unika.ipd.grGen.Model_FunctionsProceduresExample;

namespace FPE
{
    class FPEExample
    {
        FunctionsProceduresExampleGraph graph;
        FunctionsProceduresExampleActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        void DoFPE()
        {
            graph = new FunctionsProceduresExampleGraph(new LGSPGlobalVariables());
            actions = new FunctionsProceduresExampleActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            // use graph rewrite sequence
            procEnv.ApplyGraphRewriteSequence("init");

            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            // use new 2.5 exact interface
            IMatchesExact<Rule_r.IMatch_r> matchesExact = actions.r.Match(procEnv, 0);
            ConsoleUI.outWriter.WriteLine(matchesExact.Count + " matches found.");
            actions.r.Modify(procEnv, matchesExact.FirstExact);
        }

        static void Main(string[] args)
        {
            FPEExample eae = new FPEExample();
            eae.DoFPE();
        }
    }
}
