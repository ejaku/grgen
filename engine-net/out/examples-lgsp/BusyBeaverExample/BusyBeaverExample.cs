using System;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.actions.Turing3;
using de.unika.ipd.grGen.models.Turing3;

namespace BusyBeaver
{
    class BusyBeaverExample
    {
        const int L = -1;
        const int R = 1;

        LGSPGraph graph;
        Turing3Actions actions;

        void GenStateTransition(String srcState, int input, String destState, int output, int move)
        {
            IType moveType;
            switch(move)
            {
                case L: moveType = EdgeType_moveLeft.typeVar; break;
                case R: moveType = EdgeType_moveRight.typeVar; break;
                default: throw new ArgumentException("Illegal move value: " + move);
            }

            LGSPNode writeNode = graph.AddNode(NodeType_WriteValue.typeVar);

            // Set the "value" attribute of the "WriteValue" node to the integer value output
            ((Node_WriteValue) writeNode.attributes).value = output;

            graph.AddEdge(input == 0 ? (ITypeFramework) EdgeType_readZero.typeVar : (ITypeFramework) EdgeType_readOne.typeVar,
                (LGSPNode) graph.GetVariableValue(srcState), writeNode);
            graph.AddEdge(moveType, writeNode, (LGSPNode) graph.GetVariableValue(destState));
        }

        void DoBusyBeaver()
        {
            long startBytes = System.GC.GetTotalMemory(true);
            int startTime = Environment.TickCount;

            graph = new LGSPGraph(new Turing3GraphModel());
            actions = new Turing3Actions(graph);

            // Enable step counting
            actions.PerformanceInfo = new PerformanceInfo();

            // Initialize tape
            LGSPNode bp = graph.AddNode(NodeType_BandPosition.typeVar);

            // Initialize states
            LGSPNode sA = (LGSPNode) graph.AddNode(NodeType_State.typeVar, "sA");
            graph.AddNode(NodeType_State.typeVar, "sB");
            graph.AddNode(NodeType_State.typeVar, "sC");
            graph.AddNode(NodeType_State.typeVar, "sD");
            graph.AddNode(NodeType_State.typeVar, "sE");
            graph.AddNode(NodeType_State.typeVar, "sH");

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
            actions.GenerateSearchPlans("readOneRule", "readZeroRule",
                "ensureMoveLeftValidRule", "ensureMoveRightValidRule", "moveLeftRule", "moveRightRule");

            // Go, beaver, go!

            actions.ApplyGraphRewriteSequence(
                "(((curValue)=readOneRule(curState, curPos) || (curValue)=readZeroRule(curState,curPos))"
                + " && (ensureMoveLeftValidRule(curValue, curPos) || ensureMoveRightValidRule(curValue, curPos) || true)"
                + " && ((curState, curPos)=moveLeftRule(curValue, curPos) || (curState, curPos)=moveRightRule(curValue, curPos)))*");

            int stopTime = Environment.TickCount;

            // Count the number of "BandPosition" nodes with a "value" attribute being one
            int numOnes = 0;
            foreach(LGSPNode valueNode in graph.GetExactNodes(NodeType_BandPosition.typeVar))
                if(((INode_BandPosition) valueNode.attributes).value == 1)
                    numOnes++;

            int countTime = Environment.TickCount - stopTime;

            Console.WriteLine(actions.PerformanceInfo.MatchesFound + " matches found."
                + "\nNumber of ones written: " + numOnes
                + "\nTime needed for counting ones: " + countTime + " ms");

            long endBytes = System.GC.GetTotalMemory(true);

            int numNodes = graph.GetNumCompatibleNodes(NodeType_Node.typeVar);
            int numEdges = graph.GetNumCompatibleEdges(EdgeType_Edge.typeVar);

            Console.WriteLine("Time: " + (stopTime - startTime) + " ms"
                + "\nMemory usage: " + (endBytes - startBytes) + " bytes"
                + "\nNum nodes: " + numNodes
                + "\nNum edges: " + numEdges);
        }

        static void Main(string[] args)
        {
            BusyBeaverExample beaver = new BusyBeaverExample();
            beaver.DoBusyBeaver();
        }
    }
}
