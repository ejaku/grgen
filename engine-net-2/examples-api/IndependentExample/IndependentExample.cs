/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_Independent;
using de.unika.ipd.grGen.Model_Independent;

namespace Independent
{
    class IndependentExample
    {
        LGSPGraph graph;
        IndependentActions actions;

        void DoIdpt()
        {
            graph = new LGSPGraph(new IndependentGraphModel());
            actions = new IndependentActions(graph);

            graph.PerformanceInfo = new PerformanceInfo();

            actions.ApplyGraphRewriteSequence("create");

			Console.WriteLine(graph.PerformanceInfo.MatchesFound + " matches found.");
			Console.WriteLine(graph.PerformanceInfo.RewritesPerformed + " rewrites performed.");
			graph.PerformanceInfo.Reset();

            IMatches matches = actions.GetAction("findIndependent").Match(graph, 0, null);
            Console.WriteLine(matches.Count + " matches found.");

            graph.Clear();

            actions.ApplyGraphRewriteSequence("createIterated");

            Console.WriteLine(graph.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(graph.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            graph.PerformanceInfo.Reset();

            IAction_createIterated createIterated = actions.createIterated;
            IMatchesExact<Rule_createIterated.IMatch_createIterated> matchesCreateIterated = 
                createIterated.Match(graph, 0);
            IintNode beg;
            INode end;
            createIterated.Modify(graph, matchesCreateIterated.FirstExact, out beg, out end);

            IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matchesFindChain =
                actions.findChainPlusChainToIntIndependent.Match(graph, 0, beg, end);
            Console.WriteLine(matchesFindChain.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            IndependentExample idpt = new IndependentExample();
            idpt.DoIdpt();
        }
    }
}
