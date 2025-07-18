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
using de.unika.ipd.grGen.Action_Alternatives;
using de.unika.ipd.grGen.Model_Alternatives;

namespace Alternatives
{
    class AlternativeExample
    {
        LGSPGraph graph;
        AlternativesActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        void DoAlt()
        {
            graph = new LGSPGraph(new AlternativesGraphModel(), new LGSPGlobalVariables());
            actions = new AlternativesActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            // use graph rewrite sequence
            procEnv.ApplyGraphRewriteSequence("createComplex");

            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            // use old inexact interface
            IMatches matches = actions.GetAction("Complex").Match(procEnv, 0, null);
            ConsoleUI.outWriter.WriteLine(matches.Count + " Complex matches found.");

            // use new 2.5 exact interface
            IMatchesExact<Rule_ComplexMax.IMatch_ComplexMax> matchesExact = actions.ComplexMax.Match(procEnv, 0);
            ConsoleUI.outWriter.WriteLine(matchesExact.Count + " ComplexMax matches found.");
        }

        static void Main(string[] args)
        {
            AlternativeExample alt = new AlternativeExample();
            alt.DoAlt();
        }
    }
}
