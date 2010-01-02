/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_Recursive;
using de.unika.ipd.grGen.Model_Std;

namespace Recursive
{
    class RecursiveExample
    {
        Std graph;
        RecursiveActions actions;

        void DoAlt()
        {
            graph = new Std();
            actions = new RecursiveActions(graph);

            IMatches matches;
            Object[] returns;

            Action_createChain createChain = Action_createChain.Instance;
            matches = createChain.Match(graph, 0);
            returns = createChain.Modify(graph, matches.First);
            Node[] param = new Node[2];
            param[0] = (Node)returns[0];
            param[1] = (Node)returns[1];
            matches = actions.GetAction("chainFromToReverseToCommon").Match(graph, 0, param);
            Console.WriteLine(matches.Count + " matches found.");

            Action_createBlowball createBlowball = Action_createBlowball.Instance;
            matches = createBlowball.Match(graph, 0);
            returns = createBlowball.Modify(graph, matches.First);
            matches = actions.GetAction("blowball").Match(graph, 0, returns);
            Console.WriteLine(matches.Count + " matches found.");

            graph.Clear();

            graph.PerformanceInfo = new PerformanceInfo();
            matches = createChain.Match(graph, 0);
            returns = createChain.Modify(graph, matches.First);
            param[0] = (Node)returns[0];

            Console.WriteLine(graph.PerformanceInfo.MatchesFound + " matches found.");
            Console.WriteLine(graph.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            graph.PerformanceInfo.Reset();

            IAction chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards = 
                actions.GetAction("chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards");
            matches = chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match(graph, 0, param);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            RecursiveExample rec = new RecursiveExample();
            rec.DoAlt();
        }
    }
}
