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
using de.unika.ipd.grGen.Action_ProgramGraphsOriginal;
using de.unika.ipd.grGen.Model_ProgramGraphsOriginal;

namespace ProgramGraphs
{
    class ProgramGraphsExample
    {
        LGSPGraph graph;
        ProgramGraphsOriginalActions actions;
        LGSPGraphProcessingEnvironment procEnv;

        void DoIt()
        {
            graph = new LGSPGraph(new ProgramGraphsOriginalGraphModel(), new LGSPGlobalVariables());
            actions = new ProgramGraphsOriginalActions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            // use new exact 2.5 interface
            de.unika.ipd.grGen.Model_ProgramGraphsOriginal.IClass cls = null; 
            IMethodBody mb = null;
            bool success = actions.createProgramGraphPullUp.Apply(procEnv, ref cls, ref mb);
            IMatchesExact<Rule_pullUpMethod.IMatch_pullUpMethod> matchesExact =
                actions.pullUpMethod.Match(procEnv, 0, cls, mb);
            ConsoleUI.outWriter.WriteLine(matchesExact.Count + " matches found.");
            ConsoleUI.outWriter.WriteLine(matchesExact.FirstExact.node_m5.ToString());

            graph.Clear();
            
            // and again with old inexact interface
            IMatches matchesInexact;
            object[] returns;

            Action_createProgramGraphPullUp createProgramGraph = Action_createProgramGraphPullUp.Instance;
            matchesInexact = createProgramGraph.Match(procEnv, 0);
            returns = createProgramGraph.Modify(procEnv, matchesInexact.First);
            IGraphElement[] param = new LGSPNode[2];
            param[0] = (Class)returns[0];
            param[1] = (MethodBody)returns[1];
            matchesInexact = actions.GetAction("pullUpMethod").Match(procEnv, 0, param);
            ConsoleUI.outWriter.WriteLine(matchesInexact.Count + " matches found.");
            ConsoleUI.outWriter.WriteLine(matchesInexact.First.getNodeAt((int)Rule_pullUpMethod.pullUpMethod_NodeNums.m5).ToString());
        }

        static void Main(string[] args)
        {
            ProgramGraphsExample pge = new ProgramGraphsExample();
            pge.DoIt();
        }
    }
}
