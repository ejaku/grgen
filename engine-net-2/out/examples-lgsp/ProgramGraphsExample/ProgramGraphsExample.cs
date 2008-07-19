/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_ProgramGraphs;
using de.unika.ipd.grGen.Model_ProgramGraphs;

namespace ProgramGraphs
{
    class ProgramGraphsExample
    {
        LGSPGraph graph;
        ProgramGraphsActions actions;

        void DoIt()
        {
            graph = new LGSPGraph(new ProgramGraphsGraphModel());
            actions = new ProgramGraphsActions(graph);

            graph.PerformanceInfo = new PerformanceInfo();

            LGSPMatches matches;
            object[] returns;

            LGSPAction createProgramGraph = Action_createProgramGraphPullUp.Instance;
            matches = createProgramGraph.Match(graph, 0, null);
            returns = createProgramGraph.Modify(graph, matches.matchesList.First);
            IGraphElement[] param = new LGSPNode[2];
            param[0] = (Class)returns[0];
            param[1] = (MethodBody)returns[1];
            matches = actions.GetAction("pullUpMethod").Match(graph, 0, param);
            Console.WriteLine(matches.Count + " matches found.");
        }

        static void Main(string[] args)
        {
            ProgramGraphsExample pge = new ProgramGraphsExample();
            pge.DoIt();
        }
    }
}
