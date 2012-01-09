/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */


//#define USE_SEQUENCE // use graph rewrite sequence or c# program for busy beaver control


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

        Turing3Graph graph;
        Turing3Actions actions;
        LGSPGraphProcessingEnvironment procEnv;

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
                procEnv.GetNodeVarValue(srcState), writeNode);
            graph.AddEdge(moveType, writeNode, procEnv.GetNodeVarValue(destState));
        }

        void DoBusyBeaver()
        {
            long startBytes = System.GC.GetTotalMemory(true);
            int startTime = Environment.TickCount;

            graph = new Turing3Graph();
            actions = new Turing3Actions(graph);
            procEnv = new LGSPGraphProcessingEnvironment(graph, actions);

            // Enable step counting
			procEnv.PerformanceInfo = new PerformanceInfo();

            // Initialize tape
            BandPosition bp = graph.CreateNodeBandPosition();

            // Initialize states
            State sA = graph.CreateNodeState();
            procEnv.SetVariableValue("sA", sA);
            State sB = graph.CreateNodeState();
            procEnv.SetVariableValue("sB", sB);
            State sC = graph.CreateNodeState();
            procEnv.SetVariableValue("sC", sC);
            State sD = graph.CreateNodeState();
            procEnv.SetVariableValue("sD", sD);
            State sE = graph.CreateNodeState();
            procEnv.SetVariableValue("sE", sE);
            State sH = graph.CreateNodeState();
            procEnv.SetVariableValue("sH", sH);

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
            procEnv.SetVariableValue("curState", sA);
            procEnv.SetVariableValue("curPos", bp);

            // A little warm up for the beaver
            // Using a graph rewrite sequence with the new and more expressive syntax
            procEnv.ApplyGraphRewriteSequence(
                  @"(
                       ((curValue:WriteValue)=readOneRule(curState, curPos)
                       || (curValue)=readZeroRule(curState,curPos))
                    && (ensureMoveLeftValidRule(curValue, curPos)
                       || ensureMoveRightValidRule(curValue, curPos)
                       || true)
                    && ((curState, curPos)=moveLeftRule(curValue, curPos)
                       || (curState, curPos)=moveRightRule(curValue, curPos))
                   )[100]");

			Console.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found.");

            // Reset counters of the PerformanceInfo object
			procEnv.PerformanceInfo.Reset();

            // Calculate search plans to optimize performance
            graph.AnalyzeGraph();
            actions.GenerateActions("readOneRule", "readZeroRule",
                "ensureMoveLeftValidRule", "ensureMoveRightValidRule", "moveLeftRule", "moveRightRule");

            // Go, beaver, go!
#if USE_SEQUENCE
            procEnv.ApplyGraphRewriteSequence(
                  @"(
                       ((curValue:WriteValue)=readOneRule(curState, curPos)
                       || (curValue)=readZeroRule(curState,curPos))
                    && (ensureMoveLeftValidRule(curValue, curPos)
                       || ensureMoveRightValidRule(curValue, curPos)
                       || true)
                    && ((curState, curPos)=moveLeftRule(curValue, curPos)
                       || (curState, curPos)=moveRightRule(curValue, curPos))
                   )*");
#else
            // the graph rewrite sequence from above formulated in C# with the API of GrGen.NET 2.5
            IState curState = (IState)procEnv.GetVariableValue("curState");
            IBandPosition curPos = (IBandPosition)procEnv.GetVariableValue("curPos");
            IWriteValue curValue = (IWriteValue)procEnv.GetVariableValue("curValue");
            bool expressionToIterateSucceeded;
            do
            {
                expressionToIterateSucceeded =
                    ( actions.readOneRule.Apply(procEnv, curState, curPos, ref curValue)
                    || actions.readZeroRule.Apply(procEnv, curState, curPos, ref curValue) )
                && ( actions.ensureMoveLeftValidRule.Apply(procEnv, curValue, curPos)
                    || actions.ensureMoveRightValidRule.Apply(procEnv, curValue, curPos)
                    || true )
                && ( actions.moveLeftRule.Apply(procEnv, curValue, curPos, ref curState, ref curPos)
                    || actions.moveRightRule.Apply(procEnv, curValue, curPos, ref curState, ref curPos) );
            }
            while(expressionToIterateSucceeded);
#endif

            int stopTime = Environment.TickCount;

            // Count "BandPosition" nodes with a "value" attribute being one
            int numOnes = 0;
            foreach(BandPosition bpNode in graph.GetExactNodes(BandPosition.TypeInstance))
                if(bpNode.value == 1)
                    numOnes++;

            int countTime = Environment.TickCount - stopTime;

			Console.WriteLine(procEnv.PerformanceInfo.MatchesFound + " matches found."
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
