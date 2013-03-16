/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;

// this is not related in any way to IGraphHelpers.cs

namespace de.unika.ipd.grGen.libGr
{
    public class GraphHelper
    {
        /// <summary>
        /// Returns the nodes in the graph of the type given, as set
        /// </summary>
        public static IDictionary<INode, SetValueType> Nodes(IGraph graph, NodeType nodeType)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in graph.GetCompatibleNodes(nodeType))
            {
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        /// <summary>
        /// Returns the edges in the graph of the type given, as set
        /// </summary>
        public static IDictionary<IEdge, SetValueType> Edges(IGraph graph, EdgeType edgeType)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of nodes adjacent to the start node, under the type constraints given
        /// </summary>
        public static IDictionary<INode, SetValueType> Adjacent(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode,SetValueType>();
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
            }
            return adjacentNodesSet;
        }

        /// <summary>
        /// Returns set of nodes adjacent to the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static IDictionary<INode, SetValueType> AdjacentOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                targetNodesSet[adjacentNode] = null;
            }
            return targetNodesSet;
        }

        /// <summary>
        /// Returns set of nodes adjacent to the start node via incoming edges, under the type constraints given
        /// </summary>
        public static IDictionary<INode, SetValueType> AdjacentIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                sourceNodesSet[adjacentNode] = null;
            }
            return sourceNodesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges incident to the start node, under the type constraints given
        /// </summary>
        public static IDictionary<IEdge, SetValueType> Incident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of edges outgoing from the start node, under the type constraints given
        /// </summary>
        public static IDictionary<IEdge, SetValueType> Outgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of edges incoming to the start node, under the type constraints given
        /// </summary>
        public static IDictionary<IEdge, SetValueType> Incoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
            }
            return incomingEdgesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of nodes reachable from the start node, under the type constraints given
        /// </summary>
        public static IDictionary<INode, SetValueType> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet);
            return adjacentNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, SetValueType> adjacentNodesSet)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static IDictionary<INode, SetValueType> ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet);
            return targetNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node via outgoing edges, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, SetValueType> targetNodesSet)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(targetNodesSet.ContainsKey(adjacentNode))
                    continue;
                targetNodesSet[adjacentNode] = null;
                ReachableOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, targetNodesSet);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node via incoming edges, under the type constraints given
        /// </summary>
        public static IDictionary<INode, SetValueType> ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet);
            return sourceNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node via incoming edges, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, SetValueType> sourceNodesSet)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(sourceNodesSet.ContainsKey(adjacentNode))
                    continue;
                sourceNodesSet[adjacentNode] = null;
                ReachableIncoming(adjacentNode, incomingEdgeType, sourceNodeType, sourceNodesSet);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges reachable from the start node, under the type constraints given
        /// </summary>
        public static IDictionary<IEdge, SetValueType> ReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, flag);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetVisited(edge.Source, flag, false);
                graph.SetVisited(edge.Target, flag, false);
            }
            graph.FreeVisitedFlagNonReset(flag);
            return incidentEdgesSet;
        }

        /// <summary>
        /// Fills set of edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, IGraph graph, int flag)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                incidentEdgesSet[edge] = null;
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, flag);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                incidentEdgesSet[edge] = null;
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, flag);
            }
        }

        /// <summary>
        /// Returns set of outgoing edges reachable from the start node, under the type constraints given
        /// </summary>
        public static IDictionary<IEdge, SetValueType> ReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, flag);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetVisited(edge.Source, flag, false);
                graph.SetVisited(edge.Target, flag, false);
            }
            graph.FreeVisitedFlagNonReset(flag);
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Fills set of outgoing edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, IGraph graph, int flag)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                outgoingEdgesSet[edge] = null;
                ReachableEdgesOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, flag);
            }
        }

        /// <summary>
        /// Returns set of incoming edges reachable from the start node, under the type constraints given
        /// </summary>
        public static IDictionary<IEdge, SetValueType> ReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, flag);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetVisited(edge.Source, flag, false);
                graph.SetVisited(edge.Target, flag, false);
            }
            graph.FreeVisitedFlagNonReset(flag);
            return incomingEdgesSet;
        }

        /// <summary>
        /// Fills set of incoming edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, IGraph graph, int flag)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                incomingEdgesSet[edge] = null;
                ReachableEdgesIncoming(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, flag);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end node is adajcent to the start node, under the type constraints given
        /// </summary>
        public static bool IsAdjacent(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the end node is adajcent to the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsAdjacentOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the end node is adajcent to the start node via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsAdjacentIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end edge is incident to the start node, under the type constraints given
        /// </summary>
        public static bool IsIncident(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the end edge is incident to the start node as outgoing edge, under the type constraints given
        /// </summary>
        public static bool IsOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the end edge is incident to the start node as outgoing edge, under the type constraints given
        /// </summary>
        public static bool IsIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end node is reachable from the start node, under the type constraints given
        /// </summary>
        public static bool IsReachable(IGraph graph, INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachable(startNode, endNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetVisited(visitedNodes[i], flag, false);
            graph.FreeVisitedFlagNonReset(flag);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, under the type constraints given
        /// </summary>
        private static bool IsReachable(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                if(edge.Target == endNode)
                    return true;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes);
                if(result == true)
                    break;
            }

            if(!result)
            {
                foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
                {
                    INode adjacentNode = edge.Source;
                    if(!adjacentNode.InstanceOf(adjacentNodeType))
                        continue;
                    if(graph.IsVisited(adjacentNode, flag))
                        continue;
                    if(edge.Source == endNode)
                        return true;
                    graph.SetVisited(adjacentNode, flag, true);
                    visitedNodes.Add(adjacentNode);
                    result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsReachableOutgoing(IGraph graph, INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableOutgoing(startNode, endNode, outgoingEdgeType, targetNodeType, graph, flag, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetVisited(visitedNodes[i], flag, false);
            graph.FreeVisitedFlagNonReset(flag);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsReachableOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                if(edge.Target == endNode)
                    return true;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableOutgoing(adjacentNode, endNode, outgoingEdgeType, targetNodeType, graph, flag, visitedNodes);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsReachableIncoming(IGraph graph, INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableIncoming(startNode, endNode, incomingEdgeType, sourceNodeType, graph, flag, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetVisited(visitedNodes[i], flag, false);
            graph.FreeVisitedFlagNonReset(flag);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsReachableIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                if(edge.Source == endNode)
                    return true;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableIncoming(adjacentNode, endNode, incomingEdgeType, sourceNodeType, graph, flag, visitedNodes);
                if(result == true)
                    break;
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, under the type constraints given
        /// </summary>
        public static bool IsReachableEdges(IGraph graph, INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdges(startNode, endEdge, incidentEdgeType, adjacentNodeType, graph, flag, visitedElems);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetVisited(visitedElems[i], flag, false);
            graph.FreeVisitedFlagNonReset(flag);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, under the type constraints given
        /// </summary>
        private static bool IsReachableEdges(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<IGraphElement> visitedElems)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(edge, flag))
                    continue;
                graph.SetVisited(edge, flag, true);
                visitedElems.Add(edge);
                if(edge.Target == endEdge)
                    return true;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, flag, visitedElems);
                if(result == true)
                    break;
            }

            if(!result)
            {
                foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
                {
                    INode adjacentNode = edge.Source;
                    if(!adjacentNode.InstanceOf(adjacentNodeType))
                        continue;
                    if(graph.IsVisited(edge, flag))
                        continue;
                    graph.SetVisited(edge, flag, true);
                    visitedElems.Add(edge);
                    if(edge.Source == endEdge)
                        return true;

                    if(graph.IsVisited(adjacentNode, flag))
                        continue;
                    graph.SetVisited(adjacentNode, flag, true);
                    visitedElems.Add(adjacentNode);
                    result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, flag, visitedElems);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesOutgoing(startNode, endEdge, outgoingEdgeType, targetNodeType, graph, flag, visitedElems);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetVisited(visitedElems[i], flag, false);
            graph.FreeVisitedFlagNonReset(flag);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsReachableEdgesOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, int flag, List<IGraphElement> visitedElems)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsVisited(edge, flag))
                    continue;
                graph.SetVisited(edge, flag, true);
                visitedElems.Add(edge);
                if(edge.Target == endEdge)
                    return true;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdgesOutgoing(adjacentNode, endEdge, outgoingEdgeType, targetNodeType, graph, flag, visitedElems);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            int flag = graph.AllocateVisitedFlag();
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesIncoming(startNode, endEdge, incomingEdgeType, sourceNodeType, graph, flag, visitedElems);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetVisited(visitedElems[i], flag, false);
            graph.FreeVisitedFlagNonReset(flag);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsReachableEdgesIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, int flag, List<IGraphElement> visitedElems)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsVisited(edge, flag))
                    continue;
                graph.SetVisited(edge, flag, true);
                visitedElems.Add(edge);
                if(edge.Source == endEdge)
                    return true;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdgesIncoming(adjacentNode, endEdge, incomingEdgeType, sourceNodeType, graph, flag, visitedElems);
                if(result == true)
                    break;
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the induced subgraph of the given node set
        /// </summary>
        public static IGraph InducedSubgraph(IDictionary<INode, SetValueType> nodeSet, IGraph graph)
        {
            IGraph inducedGraph = graph.CreateEmptyEquivalent("induced_from_" + graph.Name);
            IDictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(nodeSet.Count);
            foreach(KeyValuePair<INode, SetValueType> nodeEntry in nodeSet)
            {
                INode node = nodeEntry.Key;
                INode clone = node.Clone();
                nodeToCloned.Add(node, clone);
                inducedGraph.AddNode(clone);
            }
            //graph.Check();
            //inducedGraph.Check();

            foreach(KeyValuePair<INode, SetValueType> nodeEntry in nodeSet)
            {
                INode node = nodeEntry.Key;
                foreach(IEdge edge in node.Outgoing)
                {
                    if(nodeToCloned.ContainsKey(edge.Target))
                    {
                        IEdge clone = edge.Clone(nodeToCloned[node], nodeToCloned[edge.Target]);
                        inducedGraph.AddEdge(clone);
                    }
                }
            }
            //graph.Check();
            //inducedGraph.Check();
            
            return inducedGraph;
        }

        /// <summary>
        /// Returns the edge induced/defined subgraph of the given edge set
        /// </summary>
        public static IGraph DefinedSubgraph(IDictionary<IEdge, SetValueType> edgeSet, IGraph graph)
        {
            IGraph definedGraph = graph.CreateEmptyEquivalent("defined_from_" + graph.Name);
            IDictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count*2);
            foreach(KeyValuePair<IEdge, SetValueType> edgeEntry in edgeSet)
            {
                IEdge edge = edgeEntry.Key;
                if(!nodeToCloned.ContainsKey(edge.Source))
                {
                    INode clone = edge.Source.Clone();
                    nodeToCloned.Add(edge.Source, clone);
                    definedGraph.AddNode(clone);

                }
                if(!nodeToCloned.ContainsKey(edge.Target))
                {
                    INode clone = edge.Target.Clone();
                    nodeToCloned.Add(edge.Target, clone);
                    definedGraph.AddNode(clone);
                }
            }
            //graph.Check();
            //definedGraph.Check();

            foreach(KeyValuePair<IEdge, SetValueType> edgeEntry in edgeSet)
            {
                IEdge edge = edgeEntry.Key;
                IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                definedGraph.AddEdge(clone);
            }
            //graph.Check();
            //definedGraph.Check();

            return definedGraph;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Inserts a copy of the induced subgraph of the given node set to the graph
        /// returns the copy of the dedicated root node
        /// the root node is processed as if it was in the given node set even if it isn't
        /// </summary>
        public static INode InsertInduced(IDictionary<INode, SetValueType> nodeSet, INode rootNode, IGraph graph)
        {
            IDictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(nodeSet.Count+1);
            foreach(KeyValuePair<INode, SetValueType> nodeEntry in nodeSet)
            {
                INode node = nodeEntry.Key;
                INode clone = node.Clone();
                nodeToCloned.Add(node, clone);
                graph.AddNode(clone);
            }
            if(!nodeSet.ContainsKey(rootNode))
            {
                INode clone = rootNode.Clone();
                nodeToCloned.Add(rootNode, clone);
                graph.AddNode(clone);
            }
            //graph.Check();

            foreach(KeyValuePair<INode, SetValueType> nodeEntry in nodeSet)
            {
                INode node = nodeEntry.Key;
                foreach(IEdge edge in node.Outgoing)
                {
                    if(nodeToCloned.ContainsKey(edge.Target))
                    {
                        IEdge clone = edge.Clone(nodeToCloned[node], nodeToCloned[edge.Target]);
                        graph.AddEdge(clone);
                    }
                }
            }
            if(!nodeSet.ContainsKey(rootNode))
            {
                foreach(IEdge edge in rootNode.Outgoing)
                {
                    if(nodeToCloned.ContainsKey(edge.Target))
                    {
                        IEdge clone = edge.Clone(nodeToCloned[rootNode], nodeToCloned[edge.Target]);
                        graph.AddEdge(clone);
                    }
                }
                foreach(IEdge edge in rootNode.Incoming)
                {
                    if(nodeToCloned.ContainsKey(edge.Source) && edge.Source!=rootNode)
                    {
                        IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[rootNode]);
                        graph.AddEdge(clone);
                    }
                }
            }
            //graph.Check();

            return nodeToCloned[rootNode];
        }

        /// <summary>
        /// Inserts a copy of the edge induced/defined subgraph of the given edge set to the graph
        /// returns the copy of the dedicated root edge
        /// the root edge is processed as if it was in the given edge set even if it isn't
        /// </summary>
        public static IEdge InsertDefined(IDictionary<IEdge, SetValueType> edgeSet, IEdge rootEdge, IGraph graph)
        {
            IDictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count*2 + 1);
            foreach(KeyValuePair<IEdge, SetValueType> edgeEntry in edgeSet)
            {
                IEdge edge = edgeEntry.Key;
                if(!nodeToCloned.ContainsKey(edge.Source))
                {
                    INode clone = edge.Source.Clone();
                    nodeToCloned.Add(edge.Source, clone);
                    graph.AddNode(clone);

                }
                if(!nodeToCloned.ContainsKey(edge.Target))
                {
                    INode clone = edge.Target.Clone();
                    nodeToCloned.Add(edge.Target, clone);
                    graph.AddNode(clone);
                }
            }
            if(!edgeSet.ContainsKey(rootEdge))
            {
                if(!nodeToCloned.ContainsKey(rootEdge.Source))
                {
                    INode clone = rootEdge.Source.Clone();
                    nodeToCloned.Add(rootEdge.Source, clone);
                    graph.AddNode(clone);

                }
                if(!nodeToCloned.ContainsKey(rootEdge.Target))
                {
                    INode clone = rootEdge.Target.Clone();
                    nodeToCloned.Add(rootEdge.Target, clone);
                    graph.AddNode(clone);
                }
            }
            //graph.Check();

            IEdge clonedEdge = null;
            if(edgeSet.ContainsKey(rootEdge))
            {
                foreach(KeyValuePair<IEdge, SetValueType> edgeEntry in edgeSet)
                {
                    IEdge edge = edgeEntry.Key;
                    IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                    graph.AddEdge(clone);
                    if(edge == rootEdge)
                        clonedEdge = clone;
                }
            }
            else
            {
                foreach(KeyValuePair<IEdge, SetValueType> edgeEntry in edgeSet)
                {
                    IEdge edge = edgeEntry.Key;
                    IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                    graph.AddEdge(clone);
                }

                IEdge rootClone = rootEdge.Clone(nodeToCloned[rootEdge.Source], nodeToCloned[rootEdge.Target]);
                graph.AddEdge(rootClone);
                clonedEdge = rootClone;
            }
            //graph.Check();

            return clonedEdge;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// creates a node of given type and adds it to the graph, returns it
        /// type might be a string denoting a NodeType or a NodeType
        /// </summary>
        public static INode AddNodeOfType(object type, IGraph graph)
        {
            return graph.AddNode(type is string ? graph.Model.NodeModel.GetType((string)type) : (NodeType)type);
        }

        /// <summary>
        /// creates an edge of given type and adds it to the graph between from and to, returns it
        /// type might be a string denoting an EdgeType or an EdgeType
        /// </summary>
        public static IEdge AddEdgeOfType(object type, INode src, INode tgt, IGraph graph)
        {
            return graph.AddEdge(type is string ? graph.Model.EdgeModel.GetType((string)type) : (EdgeType)type, src, tgt);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<INode> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(INode node in ReachableRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return node;
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                    graph.SetVisited(visitedNodes[i], flag, false);
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<INode> ReachableRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;
                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return node;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;

                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return node;
            }
        }

        public static IEnumerable<INode> ReachableIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(INode node in ReachableIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return node;
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                    graph.SetVisited(visitedNodes[i], flag, false);
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<INode> ReachableIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;

                foreach(INode node in ReachableIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return node;
            }
        }

        public static IEnumerable<INode> ReachableOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(INode node in ReachableOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return node;
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                    graph.SetVisited(visitedNodes[i], flag, false);
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<INode> ReachableOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;

                foreach(INode node in ReachableOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return node;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<IEdge> ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(IEdge edge in ReachableEdgesRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return edge;
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                    graph.SetVisited(visitedNodes[i], flag, false);
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return reachableEdge;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return reachableEdge;
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(IEdge edge in ReachableEdgesIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return edge;
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                    graph.SetVisited(visitedNodes[i], flag, false);
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return reachableEdge;
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(IEdge edge in ReachableEdgesOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return edge;
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                    graph.SetVisited(visitedNodes[i], flag, false);
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                    yield return reachableEdge;
            }
        }
    }
}
