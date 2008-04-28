/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

            actions.PerformanceInfo = new PerformanceInfo();

            LGSPMatches matches;
            Object[] returns;

            LGSPAction createChain = Action_createChain.Instance;
            matches = createChain.Match(graph, 0, null);
            returns = createChain.Modify(graph, matches.matchesList.First);
            Node[] param = new Node[2];
            param[0] = (Node)returns[0];
            param[1] = (Node)returns[1];
            matches = actions.GetAction("chainFromToReverseToCommon").Match(graph, 0, param);
            Console.WriteLine(matches.Count + " matches found.");

            LGSPAction createBlowball = Action_createBlowball.Instance;
            matches = createBlowball.Match(graph, 0, null);
            returns = createBlowball.Modify(graph, matches.matchesList.First);
            matches = actions.GetAction("blowball").Match(graph, 0, returns);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            RecursiveExample rec = new RecursiveExample();
            rec.DoAlt();
        }
    }
}
