/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_spanningTree;
using de.unika.ipd.grGen.Model_Std;

namespace Iterated
{
    class IteratedExample
    {
        LGSPGraph graph;
        spanningTreeActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        void DoIter()
        {
            graph = new LGSPGraph(new StdGraphModel());
            actions = new spanningTreeActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            IAction_initUndirected init = actions.initUndirected;
            IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matchesInitUndirected = init.Match(procEnv, 0);
            INode root;
            init.Modify(procEnv, matchesInitUndirected.FirstExact, out root);

            IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matchesSpanningTree = actions.spanningTree.Match(procEnv, 0, root);
            actions.spanningTree.Modify(procEnv, matchesSpanningTree.FirstExact);
            Console.WriteLine(matchesSpanningTree.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            IteratedExample iter = new IteratedExample();
            iter.DoIter();
        }
    }
}
