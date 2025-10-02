/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

using System;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Action_Recursive;
using de.unika.ipd.grGen.Model_Std;

namespace Recursive
{
    class RecursiveExample
    {
        StdGraph graph;
        RecursiveActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        void DoAlt()
        {
            graph = new StdGraph(new LGSPGlobalVariables());
            actions = new RecursiveActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            IMatches matches;
            object[] returns;

            Action_createChain createChain = Action_createChain.Instance;
            matches = createChain.Match(procEnv, 0);
            returns = createChain.Modify(procEnv, matches.First);
            Node[] param = new Node[2];
            param[0] = (Node)returns[0];
            param[1] = (Node)returns[1];
            matches = actions.GetAction("chainFromToReverseToCommon").Match(procEnv, 0, param);
            ConsoleUI.outWriter.WriteLine(matches.Count + " matches found.");

            Action_createBlowball createBlowball = Action_createBlowball.Instance;
            matches = createBlowball.Match(procEnv, 0);
            returns = createBlowball.Modify(procEnv, matches.First);
            matches = actions.GetAction("blowball").Match(procEnv, 0, returns);
            ConsoleUI.outWriter.WriteLine(matches.Count + " matches found.");

            graph.Clear();

            matches = createChain.Match(procEnv, 0);
            returns = createChain.Modify(procEnv, matches.First);
            param[0] = (Node)returns[0];

            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");
            ConsoleUI.outWriter.WriteLine(procEnv.PerformanceInfo.RewritesPerformed + " rewrites performed.");
            procEnv.PerformanceInfo.Reset();

            IAction chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards = 
                actions.GetAction("chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards");
            matches = chainFromCompleteArbitraryBaseAlwaysFailesByGoingBackwards.Match(procEnv, 0, param);
            ConsoleUI.outWriter.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            RecursiveExample rec = new RecursiveExample();
            rec.DoAlt();
        }
    }
}
