/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

        void DoIter()
        {
            graph = new LGSPGraph(new StdGraphModel());
            actions = new spanningTreeActions(graph);

            IMatches matches;
            object[] returns;

            LGSPAction init = Action_initUndirected.Instance;
            matches = init.Match(graph, 0, null);
            returns = init.Modify(graph, matches.First);

            IGraphElement[] param = new LGSPNode[1];
            param[0] = (Node)returns[0];
            matches = actions.GetAction("spanningTree").Match(graph, 0, param);
            returns = actions.GetAction("spanningTree").Modify(graph, matches.First);

            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            IteratedExample iter = new IteratedExample();
            iter.DoIter();
        }
    }
}
