/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_Std;
using de.unika.ipd.grGen.libGr;

namespace VisitedExample
{
    enum WalkerMode
    {
        Outgoing,
        Incoming,
        Incident
    }

    enum WalkerResult
    {
        Proceed,
        Skip,
        Abort
    }

    delegate WalkerResult WalkerHandler(INode node);

    class DFSWalker
    {
        public IGraph Graph;
        public int VisitorID;
        public WalkerHandler PreHandler;
        public WalkerHandler PostHandler;
        public WalkerMode Mode;

        public DFSWalker(IGraph graph, WalkerHandler pre, WalkerHandler post, int visitorID)
        {
            Graph = graph;
            PreHandler = pre;
            PostHandler = post;
            VisitorID = visitorID;
        }

        public WalkerResult DoDFS(INode node)
        {
            if(Graph.IsVisited(node, VisitorID)) return WalkerResult.Proceed;
            Graph.SetVisited(node, VisitorID, true);

            if(PreHandler != null)
            {
                WalkerResult preRes = PreHandler(node);
                if(preRes == WalkerResult.Abort) return WalkerResult.Abort;
                else if(preRes == WalkerResult.Skip) return WalkerResult.Proceed;

                // Return and proceed with parent, if current node has been deleted by pre handler
                if(!node.Valid) return WalkerResult.Proceed;
            }

            IEnumerable<IEdge> edgesToNext;
            if(Mode == WalkerMode.Outgoing) edgesToNext = node.Outgoing;
            else if(Mode == WalkerMode.Incoming) edgesToNext = node.Incoming;
            else if(Mode == WalkerMode.Incident) edgesToNext = node.Incident;
            else throw new InvalidOperationException("Invalid walker mode!");

            foreach(IEdge edge in edgesToNext)
            {
                INode next = edge.Opposite(node);

                WalkerResult res = DoDFS(next);
                if(res == WalkerResult.Abort) return WalkerResult.Abort;
                else if(res == WalkerResult.Skip) return WalkerResult.Proceed;

                // Return and proceed with parent, if current node has been deleted while walking children
                if(!node.Valid) return WalkerResult.Proceed;
            }

            if(PostHandler != null)
            {
                WalkerResult postRes = PostHandler(node);
                if(postRes == WalkerResult.Abort) return WalkerResult.Abort;
            }
            return WalkerResult.Proceed;
        }
    }

    class BFSWalker
    {
        public IGraph Graph;
        public int VisitorID;
        public WalkerHandler Handler;
        public WalkerMode Mode;

        public BFSWalker(IGraph graph, WalkerHandler handler, int visitorID)
        {
            Graph = graph;
            Handler = handler;
            VisitorID = visitorID;
        }

        public WalkerResult DoBFS(INode startNode)
        {
            LinkedList<INode> workList = new LinkedList<INode>();
            workList.AddLast(startNode);

            do
            {
                INode curNode = workList.First.Value;
                workList.RemoveFirst();

                if(Graph.IsVisited(curNode, VisitorID)) continue;
                Graph.SetVisited(curNode, VisitorID, true);

                if(Handler != null)
                {
                    WalkerResult preRes = Handler(curNode);
                    if(preRes == WalkerResult.Abort) return WalkerResult.Abort;
                    else if(preRes == WalkerResult.Skip) continue;

                    // Proceed with next node, if current node has been deleted by handler
                    if(!curNode.Valid) continue;
                }

                IEnumerable<IEdge> edgesToNext;
                if(Mode == WalkerMode.Outgoing) edgesToNext = curNode.Outgoing;
                else if(Mode == WalkerMode.Incoming) edgesToNext = curNode.Incoming;
                else if(Mode == WalkerMode.Incident) edgesToNext = curNode.Incident;
                else throw new InvalidOperationException("Invalid walker mode!");

                foreach(IEdge edge in edgesToNext)
                    workList.AddLast(edge.Opposite(curNode));
            }
            while(workList.Count != 0);

            return WalkerResult.Proceed;
        }
    }

    class VisitedExample
    {
        StdNamedGraph graph;

        int countedNodesPre = 0;
        int countedNodesPost = 0;

        private WalkerResult PreWalker(INode node)
        {
            Console.WriteLine("Pre: " + graph.GetElementName(node));
            countedNodesPre++;
            return WalkerResult.Proceed;
        }

        private WalkerResult PostWalker(INode node)
        {
            Console.WriteLine("Post: " + graph.GetElementName(node));
            countedNodesPost++;
            return WalkerResult.Proceed;
        }

        private void Run()
        {
            graph = new StdNamedGraph();

            int numNodes = 10;
            int numEdges = 20;

            List<Node> nodes = new List<Node>(numNodes);
            for(int i = 0; i < numNodes; i++)
                nodes.Add(Node.CreateNode(graph));

            Random rnd = new Random(4);
            for(int i = 0; i < numEdges; i++)
                Edge.CreateEdge(graph, nodes[rnd.Next(numNodes)], nodes[rnd.Next(numNodes)]);

            using(VCGDumper dumper = new VCGDumper("test.vcg"))
                GraphDumper.Dump(graph, dumper);

            int visitorID = graph.AllocateVisitedFlag();
            DFSWalker dfs = new DFSWalker(graph, PreWalker, PostWalker, visitorID);
            dfs.DoDFS(nodes[0]);
            Console.WriteLine("Visited nodes DFS: pre=" + countedNodesPre + " post=" + countedNodesPost);

            graph.ResetVisitedFlag(visitorID);
            countedNodesPre = 0;
            BFSWalker bfs = new BFSWalker(graph, PreWalker, visitorID);
            bfs.Mode = WalkerMode.Incident;
            bfs.DoBFS(nodes[0]);

            Console.WriteLine("Visited nodes BFS: " + countedNodesPre);

            graph.FreeVisitedFlag(visitorID);
        }

        static void Main(string[] args)
        {
            new VisitedExample().Run();
        }
    }
}
