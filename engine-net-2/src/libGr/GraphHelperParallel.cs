/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;

// this is not related in any way to IGraphHelpers.cs
// this is for the reachable functions that normally mark elements in the graph
//  -- this is not possible in case of a parallelized version because of multiple threads mapping to one mark
// (not all functions need to be here as of now, some are handled with the same code in the non-parallel version by now,
//   but I want to allow implementing the parallel and non-parallel version differently (distinguished by threadId in the interface))
// don't forget GraphHelper.cs for the non-parallelized versions

namespace de.unika.ipd.grGen.libGr
{
    public partial class GraphHelper
    {
        /// <summary>
        /// Returns set of nodes reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, threadId);
            return adjacentNodesSet;
        }

        public static Dictionary<INode, SetValueType> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv, threadId);
            return adjacentNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, SetValueType> adjacentNodesSet, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, threadId);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, threadId);
            }
        }

        private static void Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, SetValueType> adjacentNodesSet, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv, threadId);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet, threadId);
            return targetNodesSet;
        }

        public static Dictionary<INode, SetValueType> ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet, actionEnv, threadId);
            return targetNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node via outgoing edges, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, SetValueType> targetNodesSet, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(targetNodesSet.ContainsKey(adjacentNode))
                    continue;
                targetNodesSet[adjacentNode] = null;
                ReachableOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, targetNodesSet, threadId);
            }
        }

        private static void ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, SetValueType> targetNodesSet, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(targetNodesSet.ContainsKey(adjacentNode))
                    continue;
                targetNodesSet[adjacentNode] = null;
                ReachableOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, targetNodesSet, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node via incoming edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet, threadId);
            return sourceNodesSet;
        }

        public static Dictionary<INode, SetValueType> ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet, actionEnv, threadId);
            return sourceNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node via incoming edges, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, SetValueType> sourceNodesSet, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(sourceNodesSet.ContainsKey(adjacentNode))
                    continue;
                sourceNodesSet[adjacentNode] = null;
                ReachableIncoming(adjacentNode, incomingEdgeType, sourceNodeType, sourceNodesSet, threadId);
            }
        }

        private static void ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, SetValueType> sourceNodesSet, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(sourceNodesSet.ContainsKey(adjacentNode))
                    continue;
                sourceNodesSet[adjacentNode] = null;
                ReachableIncoming(adjacentNode, incomingEdgeType, sourceNodeType, sourceNodesSet, actionEnv, threadId);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> ReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> ReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Fills set of edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, IGraph graph, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                incidentEdgesSet[edge] = null;
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                incidentEdgesSet[edge] = null;
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                incidentEdgesSet[edge] = null;
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                incidentEdgesSet[edge] = null;
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of outgoing edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> ReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> ReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Fills set of outgoing edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, IGraph graph, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                outgoingEdgesSet[edge] = null;
                ReachableEdgesOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                outgoingEdgesSet[edge] = null;
                ReachableEdgesOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of incoming edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> ReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> ReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incomingEdgesSet;
        }

        /// <summary>
        /// Fills set of incoming edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, IGraph graph, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                incomingEdgesSet[edge] = null;
                ReachableEdgesIncoming(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                incomingEdgesSet[edge] = null;
                ReachableEdgesIncoming(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end node is reachable from the start node, under the type constraints given
        /// </summary>
        public static bool IsReachable(IGraph graph, INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachable(startNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            return result;
        }

        public static bool IsReachable(IGraph graph, INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachable(startNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, under the type constraints given
        /// </summary>
        private static bool IsReachable(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<INode> visitedNodes, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                if(edge.Target == endNode)
                    return true;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedNodes.Add(adjacentNode);
                result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId);
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
                    if(graph.IsInternallyVisited(adjacentNode, threadId))
                        continue;
                    if(edge.Source == endNode)
                        return true;
                    graph.SetInternallyVisited(adjacentNode, true, threadId);
                    visitedNodes.Add(adjacentNode);
                    result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        private static bool IsReachable(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                if(edge.Target == endNode)
                    return true;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedNodes.Add(adjacentNode);
                result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId);
                if(result == true)
                    break;
            }

            if(!result)
            {
                foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
                {
                    ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                    INode adjacentNode = edge.Source;
                    if(!adjacentNode.InstanceOf(adjacentNodeType))
                        continue;
                    if(graph.IsInternallyVisited(adjacentNode, threadId))
                        continue;
                    if(edge.Source == endNode)
                        return true;
                    graph.SetInternallyVisited(adjacentNode, true, threadId);
                    visitedNodes.Add(adjacentNode);
                    result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsReachableOutgoing(IGraph graph, INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableOutgoing(startNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            return result;
        }

        public static bool IsReachableOutgoing(IGraph graph, INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableOutgoing(startNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes, actionEnv, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsReachableOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<INode> visitedNodes, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                if(edge.Target == endNode)
                    return true;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedNodes.Add(adjacentNode);
                result = IsReachableOutgoing(adjacentNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                if(edge.Target == endNode)
                    return true;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedNodes.Add(adjacentNode);
                result = IsReachableOutgoing(adjacentNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes, actionEnv, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsReachableIncoming(IGraph graph, INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableIncoming(startNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            return result;
        }

        public static bool IsReachableIncoming(IGraph graph, INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableIncoming(startNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes, actionEnv, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsReachableIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<INode> visitedNodes, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                if(edge.Source == endNode)
                    return true;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedNodes.Add(adjacentNode);
                result = IsReachableIncoming(adjacentNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                if(edge.Source == endNode)
                    return true;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedNodes.Add(adjacentNode);
                result = IsReachableIncoming(adjacentNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes, actionEnv, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, under the type constraints given
        /// </summary>
        public static bool IsReachableEdges(IGraph graph, INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdges(startNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            return result;
        }

        public static bool IsReachableEdges(IGraph graph, INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdges(startNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, actionEnv, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, under the type constraints given
        /// </summary>
        private static bool IsReachableEdges(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<IGraphElement> visitedElems, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(edge, threadId))
                    continue;
                graph.SetInternallyVisited(edge, true, threadId);
                visitedElems.Add(edge);
                if(edge.Target == endEdge)
                    return true;

                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, threadId);
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
                    if(graph.IsInternallyVisited(edge, threadId))
                        continue;
                    graph.SetInternallyVisited(edge, true, threadId);
                    visitedElems.Add(edge);
                    if(edge.Source == endEdge)
                        return true;

                    if(graph.IsInternallyVisited(adjacentNode, threadId))
                        continue;
                    graph.SetInternallyVisited(adjacentNode, true, threadId);
                    visitedElems.Add(adjacentNode);
                    result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, threadId);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        private static bool IsReachableEdges(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<IGraphElement> visitedElems, IActionExecutionEnvironment actionEnv, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(edge, threadId))
                    continue;
                graph.SetInternallyVisited(edge, true, threadId);
                visitedElems.Add(edge);
                if(edge.Target == endEdge)
                    return true;

                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, actionEnv, threadId);
                if(result == true)
                    break;
            }

            if(!result)
            {
                foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
                {
                    ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                    INode adjacentNode = edge.Source;
                    if(!adjacentNode.InstanceOf(adjacentNodeType))
                        continue;
                    if(graph.IsInternallyVisited(edge, threadId))
                        continue;
                    graph.SetInternallyVisited(edge, true, threadId);
                    visitedElems.Add(edge);
                    if(edge.Source == endEdge)
                        return true;

                    if(graph.IsInternallyVisited(adjacentNode, threadId))
                        continue;
                    graph.SetInternallyVisited(adjacentNode, true, threadId);
                    visitedElems.Add(adjacentNode);
                    result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, actionEnv, threadId);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesOutgoing(startNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedElems, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            return result;
        }

        public static bool IsReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesOutgoing(startNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedElems, actionEnv, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsReachableEdgesOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<IGraphElement> visitedElems, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsInternallyVisited(edge, threadId))
                    continue;
                graph.SetInternallyVisited(edge, true, threadId);
                visitedElems.Add(edge);
                if(edge.Target == endEdge)
                    return true;

                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdgesOutgoing(adjacentNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedElems, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableEdgesOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<IGraphElement> visitedElems, IActionExecutionEnvironment actionEnv, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(graph.IsInternallyVisited(edge, threadId))
                    continue;
                graph.SetInternallyVisited(edge, true, threadId);
                visitedElems.Add(edge);
                if(edge.Target == endEdge)
                    return true;

                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdgesOutgoing(adjacentNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedElems, actionEnv, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesIncoming(startNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedElems, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            return result;
        }

        public static bool IsReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesIncoming(startNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedElems, actionEnv, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsReachableEdgesIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<IGraphElement> visitedElems, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsInternallyVisited(edge, threadId))
                    continue;
                graph.SetInternallyVisited(edge, true, threadId);
                visitedElems.Add(edge);
                if(edge.Source == endEdge)
                    return true;

                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdgesIncoming(adjacentNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedElems, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableEdgesIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<IGraphElement> visitedElems, IActionExecutionEnvironment actionEnv, int threadId)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(graph.IsInternallyVisited(edge, threadId))
                    continue;
                graph.SetInternallyVisited(edge, true, threadId);
                visitedElems.Add(edge);
                if(edge.Source == endEdge)
                    return true;

                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                visitedElems.Add(adjacentNode);
                result = IsReachableEdgesIncoming(adjacentNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedElems, actionEnv, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<INode> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                yield return node;
        }

        private static IEnumerable<INode> ReachableRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return node;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return node;
            }
        }

        public static IEnumerable<INode> ReachableIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                yield return node;
        }

        private static IEnumerable<INode> ReachableIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return node;
            }
        }

        public static IEnumerable<INode> ReachableOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                yield return node;
        }

        private static IEnumerable<INode> ReachableOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return node;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<IEdge> ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(IEdge edge in ReachableEdgesRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                yield return edge;
        }

        private static IEnumerable<IEdge> ReachableEdgesRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return reachableEdge;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return reachableEdge;
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(IEdge edge in ReachableEdgesIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                yield return edge;
        }

        private static IEnumerable<IEdge> ReachableEdgesIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return reachableEdge;
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(IEdge edge in ReachableEdgesOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                yield return edge;
        }

        private static IEnumerable<IEdge> ReachableEdgesOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
                    yield return reachableEdge;
            }
        }
    }
}
