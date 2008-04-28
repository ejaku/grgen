/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.Action_Turing3;
using de.unika.ipd.grGen.Model_Turing3;

namespace BusyBeaver
{
    class BusyBeaverExample
    {
        const int L = -1;
        const int R = 1;

        Turing3 graph;
        Turing3Actions actions;

        void GenStateTransition(String srcState, int input, String destState, int output, int move)
        {
            EdgeType moveType;
            switch(move)
            {
                case L: moveType = moveLeft.TypeInstance; break;
                case R: moveType = moveRight.TypeInstance; break;
                default: throw new ArgumentException("Illegal move value: " + move);
            }

            WriteValue writeNode = graph.CreateNodeWriteValue();
            writeNode.value = output;

            graph.AddEdge(input == 0 ? (EdgeType) readZero.TypeInstance : (EdgeType) readOne.TypeInstance,
                graph.GetNodeVarValue(srcState), writeNode);
            graph.AddEdge(moveType, writeNode, graph.GetNodeVarValue(destState));
        }

        void DoBusyBeaver()
        {
            long startBytes = System.GC.GetTotalMemory(true);
            int startTime = Environment.TickCount;

            graph = new Turing3();
            actions = new Turing3Actions(graph);

            // Enable step counting
            actions.PerformanceInfo = new PerformanceInfo();

            // Initialize tape
            BandPosition bp = graph.CreateNodeBandPosition();

            // Initialize states
            State sA = graph.CreateNodeState("sA");
			graph.CreateNodeState("sB");
			graph.CreateNodeState("sC");
			graph.CreateNodeState("sD");
			graph.CreateNodeState("sE");
			graph.CreateNodeState("sH");

            // Create state transitions
            GenStateTransition("sA", 0, "sB", 1, L);
            GenStateTransition("sA", 1, "sD", 1, L);
            GenStateTransition("sB", 0, "sC", 1, R);
            GenStateTransition("sB", 1, "sE", 0, R);
            GenStateTransition("sC", 0, "sA", 0, L);
            GenStateTransition("sC", 1, "sB", 0, R);
            GenStateTransition("sD", 0, "sE", 1, L);
            GenStateTransition("sD", 1, "sH", 1, L);
            GenStateTransition("sE", 0, "sC", 1, R);
            GenStateTransition("sE", 1, "sC", 1, L);

            // Initialize head
            graph.SetVariableValue("curState", sA);
            graph.SetVariableValue("curPos", bp);

            // A little warm up for the beaver
            // Using a graph rewrite sequence with the new and more expressive syntax
            actions.ApplyGraphRewriteSequence(
                "(((curValue)=readOneRule(curState, curPos) || (curValue)=readZeroRule(curState,curPos))"
                + " && (ensureMoveLeftValidRule(curValue, curPos) || ensureMoveRightValidRule(curValue, curPos) || true)"
                + " && ((curState, curPos)=moveLeftRule(curValue, curPos) || (curState, curPos)=moveRightRule(curValue, curPos)))[100]");

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found.");

            // Reset counters of the PerformanceInfo object
            actions.PerformanceInfo.Reset();

            // Calculate search plans to optimize performance
            graph.AnalyzeGraph();
            actions.GenerateActions("readOneRule", "readZeroRule",
                "ensureMoveLeftValidRule", "ensureMoveRightValidRule", "moveLeftRule", "moveRightRule");

            // Go, beaver, go!

            actions.ApplyGraphRewriteSequence(
                "(((curValue)=readOneRule(curState, curPos) || (curValue)=readZeroRule(curState,curPos))"
                + " && (ensureMoveLeftValidRule(curValue, curPos) || ensureMoveRightValidRule(curValue, curPos) || true)"
                + " && ((curState, curPos)=moveLeftRule(curValue, curPos) || (curState, curPos)=moveRightRule(curValue, curPos)))*");

            int stopTime = Environment.TickCount;

            // Count the number of "BandPosition" nodes with a "value" attribute being one
            int numOnes = 0;
            foreach(BandPosition valueNode in graph.GetExactNodes(BandPosition.TypeInstance))
                if(valueNode.value == 1)
                    numOnes++;

            int countTime = Environment.TickCount - stopTime;

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found."
                + "\nNumber of ones written: " + numOnes
                + "\nTime needed for counting ones: " + countTime + " ms");

            long endBytes = System.GC.GetTotalMemory(true);

            Console.WriteLine("Time: " + (stopTime - startTime) + " ms"
                + "\nMemory usage: " + (endBytes - startBytes) + " bytes"
                + "\nNum nodes: " + graph.NumNodes
                + "\nNum edges: " + graph.NumEdges);
        }

        static void Main(string[] args)
        {
            BusyBeaverExample beaver = new BusyBeaverExample();
            beaver.DoBusyBeaver();
        }
    }
}
