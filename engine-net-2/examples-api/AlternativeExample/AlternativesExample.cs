/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

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

			graph.PerformanceInfo = new PerformanceInfo();

            // use graph rewrite sequence
            actions.ApplyGraphRewriteSequence("createComplex");

			Console.WriteLine(graph.PerformanceInfo.MatchesFound + " matches found.");
			Console.WriteLine(graph.PerformanceInfo.RewritesPerformed + " rewrites performed.");
			graph.PerformanceInfo.Reset();

            // use old inexact interface
            IMatches matches = actions.GetAction("Complex").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " Complex matches found.");

            // use new 2.5 exact interface
            IMatchesExact<Rule_ComplexMax.IMatch_ComplexMax> matchesExact = actions.ComplexMax.Match(graph, 0);
            Console.WriteLine(matchesExact.Count + " ComplexMax matches found.");
        }

        static void Main(string[] args)
        {
            AlternativeExample alt = new AlternativeExample();
            alt.DoAlt();
        }
    }
}
