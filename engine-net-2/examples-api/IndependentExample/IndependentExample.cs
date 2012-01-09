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
        LGSPGraphProcessingEnvironment procEnv;

        void DoIdpt()
        {
            graph = new LGSPGraph(new IndependentGraphModel());
            actions = new IndependentActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            procEnv.PerformanceInfo = new PerformanceInfo();

            procEnv.ApplyGraphRewriteSequence("create");

			Console.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
			Console.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
			procEnv.PerformanceInfo.Reset();

            IMatches matches = actions.GetAction("findIndependent").Match(procEnv, 0, null);
            Console.WriteLine(matches.Count + " matches found.");

            graph.Clear();

            procEnv.ApplyGraphRewriteSequence("createIterated");

            Console.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            IAction_createIterated createIterated = actions.createIterated;
            IMatchesExact<Rule_createIterated.IMatch_createIterated> matchesCreateIterated = 
                createIterated.Match(procEnv, 0);
            IintNode beg;
            INode end;
            createIterated.Modify(procEnv, matchesCreateIterated.FirstExact, out beg, out end);

            IMatchesExact<Rule_findChainPlusChainToIntIndependent.IMatch_findChainPlusChainToIntIndependent> matchesFindChain =
                actions.findChainPlusChainToIntIndependent.Match(procEnv, 0, beg, end);
            Console.WriteLine(matchesFindChain.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            IndependentExample idpt = new IndependentExample();
            idpt.DoIdpt();
        }
    }
}
