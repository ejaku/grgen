/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
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

            // use new exact 2.5 interface
            IClass cls = null; 
            IMethodBody mb = null;
            bool success = actions.createProgramGraphPullUp.Apply(graph, ref cls, ref mb);
            IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matchesExact =
                actions.pullUpMethod.Match(graph, 0, cls, mb);
            Console.WriteLine(matchesExact.Count + " matches found.");
            Console.WriteLine(matchesExact.FirstExact.node_m5.ToString());

            graph.Clear();
            graph.PerformanceInfo = new PerformanceInfo();
            
            // and again with old inexact interface
            IMatches matchesInexact;
            object[] returns;

            Action_createProgramGraphPullUp createProgramGraph = Action_createProgramGraphPullUp.Instance;
            matchesInexact = createProgramGraph.Match(graph, 0);
            returns = createProgramGraph.Modify(graph, matchesInexact.First);
            IGraphElement[] param = new LGSPNode[2];
            param[0] = (Class)returns[0];
            param[1] = (MethodBody)returns[1];
            matchesInexact = actions.GetAction("pullUpMethod").Match(graph, 0, param);
            Console.WriteLine(matchesInexact.Count + " matches found.");
            Console.WriteLine(matchesInexact.First.getNodeAt((int)Rule_pullUpMethod.pullUpMethod_NodeNums.m5).ToString());
        }

        static void Main(string[] args)
        {
            ProgramGraphsExample pge = new ProgramGraphsExample();
            pge.DoIt();
        }
    }
}
