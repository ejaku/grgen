/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

            LGSPAction createIterated = Action_createIterated.Instance;
            matches = createIterated.Match(graph, 0, null);
            object[] returns = createIterated.Modify(graph, matches.First);

            IGraphElement[] param = new LGSPNode[2];
            param[0] = (IGraphElement)returns[0];
            param[1] = (IGraphElement)returns[1];

            matches = actions.GetAction("findChainPlusChainToIntIndependent").Match(graph, 0, param);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            IndependentExample idpt = new IndependentExample();
            idpt.DoIdpt();
        }
    }
}
