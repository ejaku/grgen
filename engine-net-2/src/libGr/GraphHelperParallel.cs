/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

// this is not related in any way to IGraphHelpers.cs
// this is for the reachable (and related) functions that normally mark elements in the graph (the marks are stored in the graph elements)
//  -- this is not possible in case of a parallelized version because of multiple threads mapping to one marking (thus destroying each other)
// (some functions are handled with the same code as in the non-parallel version as of now,
//   but I want to allow implementing the parallel and non-parallel version differently (distinguished by threadId in the interface))
// don't forget GraphHelper.cs for the non-parallelized versions

namespace de.unika.ipd.grGen.libGr
{
    public static partial class GraphHelper
    {
        /// <summary>
        /// Returns the nodes in the graph of the type given, as set
        /// </summary>
        public static Dictionary<INode, SetValueType> Nodes(IGraph graph, NodeType nodeType, int threadId)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in graph.GetCompatibleNodes(nodeType))
            {
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        public static Dictionary<INode, SetValueType> Nodes(IGraph graph, NodeType nodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in graph.GetCompatibleNodes(nodeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        /// <summary>
        /// Returns the edges in the graph of the type given, as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> Edges(IGraph graph, EdgeType edgeType, int threadId)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IEdge, SetValueType> Edges(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        /// <summary>
        /// Returns the directed edges in the graph of the type given, as set
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> EdgesDirected(IGraph graph, EdgeType edgeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> edgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> EdgesDirected(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> edgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        /// <summary>
        /// Returns the undirected edges in the graph of the type given, as set
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> EdgesUndirected(IGraph graph, EdgeType edgeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> edgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> EdgesUndirected(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> edgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the nodes in the graph of the type given
        /// </summary>
        public static int CountNodes(IGraph graph, NodeType nodeType, int threadId)
        {
            return graph.GetNumCompatibleNodes(nodeType);
        }

        public static int CountNodes(IGraph graph, NodeType nodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            return graph.GetNumCompatibleNodes(nodeType);
        }

        /// <summary>
        /// Returns the count of the edges in the graph of the type given
        /// </summary>
        public static int CountEdges(IGraph graph, EdgeType edgeType, int threadId)
        {
            return graph.GetNumCompatibleEdges(edgeType);
        }

        public static int CountEdges(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            return graph.GetNumCompatibleEdges(edgeType);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IGraphElement GetGraphElement(INamedGraph graph, string name, int threadId)
        {
            return graph.GetGraphElement(name);
        }

        public static IGraphElement GetGraphElement(INamedGraph graph, string name, IActionExecutionEnvironment actionEnv, int threadId)
        {
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return graph.GetGraphElement(name);
        }

        public static INode GetNode(INamedGraph graph, string name, int threadId)
        {
            return graph.GetNode(name);
        }

        public static INode GetNode(INamedGraph graph, string name, IActionExecutionEnvironment actionEnv, int threadId)
        {
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return graph.GetNode(name);
        }

        public static IEdge GetEdge(INamedGraph graph, string name, int threadId)
        {
            return graph.GetEdge(name);
        }

        public static IEdge GetEdge(INamedGraph graph, string name, IActionExecutionEnvironment actionEnv, int threadId)
        {
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return graph.GetEdge(name);
        }

        public static INode GetNode(INamedGraph graph, string name, NodeType nodeType, int threadId)
        {
            INode node = graph.GetNode(name);
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static INode GetNode(INamedGraph graph, string name, NodeType nodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            INode node = graph.GetNode(name);
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static IEdge GetEdge(INamedGraph graph, string name, EdgeType edgeType, int threadId)
        {
            IEdge edge = graph.GetEdge(name);
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        public static IEdge GetEdge(INamedGraph graph, string name, EdgeType edgeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            IEdge edge = graph.GetEdge(name);
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static INode GetNode(IGraph graph, int uniqueId, int threadId)
        {
            return graph.GetNode(uniqueId);
        }

        public static INode GetNode(IGraph graph, int uniqueId, IActionExecutionEnvironment actionEnv, int threadId)
        {
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return graph.GetNode(uniqueId);
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId, int threadId)
        {
            return graph.GetEdge(uniqueId);
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId, IActionExecutionEnvironment actionEnv, int threadId)
        {
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return graph.GetEdge(uniqueId);
        }

        public static INode GetNode(IGraph graph, int uniqueId, NodeType nodeType, int threadId)
        {
            INode node = graph.GetNode(uniqueId);
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static INode GetNode(IGraph graph, int uniqueId, NodeType nodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            INode node = graph.GetNode(uniqueId);
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId, EdgeType edgeType, int threadId)
        {
            IEdge edge = graph.GetEdge(uniqueId);
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId, EdgeType edgeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            IEdge edge = graph.GetEdge(uniqueId);
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of nodes adjacent to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> Adjacent(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
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

        public static Dictionary<INode, SetValueType> Adjacent(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
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
        public static Dictionary<INode, SetValueType> AdjacentOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
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

        public static Dictionary<INode, SetValueType> AdjacentOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
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
        public static Dictionary<INode, SetValueType> AdjacentIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
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

        public static Dictionary<INode, SetValueType> AdjacentIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                sourceNodesSet[adjacentNode] = null;
            }
            return sourceNodesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the nodes adjacent to the start node, under the type constraints given
        /// </summary>
        public static int CountAdjacent(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            return count;
        }

        public static int CountAdjacent(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the nodes adjacent to the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static int CountAdjacentOutgoing(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            return count;
        }

        public static int CountAdjacentOutgoing(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the nodes adjacent to the start node via incoming edges, under the type constraints given
        /// </summary>
        public static int CountAdjacentIncoming(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            return count;
        }

        public static int CountAdjacentIncoming(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false, threadId);
            }
            return count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges incident to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> Incident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
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

        public static Dictionary<IEdge, SetValueType> Incident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of directed edges incident to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> IncidentDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> IncidentDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of undirected edges incident to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> IncidentUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> IncidentUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
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
        public static Dictionary<IEdge, SetValueType> Outgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
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

        public static Dictionary<IEdge, SetValueType> Outgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of directed edges outgoing from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> OutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> OutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of undirected edges outgoing from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> OutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> OutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
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
        public static Dictionary<IEdge, SetValueType> Incoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
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

        public static Dictionary<IEdge, SetValueType> Incoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
            }
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of directed edges incoming to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> IncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> IncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
            }
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of undirected edges incoming to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> IncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> IncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
            }
            return incomingEdgesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns count of the edges incident to the start node, under the type constraints given
        /// </summary>
        public static int CountIncident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNode == startNode)
                    continue; // count reflexive edge only once
                ++count;
            }
            return count;
        }

        public static int CountIncident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                ++count;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNode == startNode)
                    continue; // count reflexive edge only once
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns count of the edges outgoing from the start node, under the type constraints given
        /// </summary>
        public static int CountOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                ++count;
            }
            return count;
        }

        public static int CountOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns count of the edges incoming to the start node, under the type constraints given
        /// </summary>
        public static int CountIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                ++count;
            }
            return count;
        }

        public static int CountIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                ++count;
            }
            return count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of nodes reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet); // call normal version, is thread safe
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
        private static void Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, SetValueType> adjacentNodesSet, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv, threadId);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
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
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet); // call normal version, is thread safe
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
        private static void ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, SetValueType> targetNodesSet, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
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
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet); // call normal version, is thread safe
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
        private static void ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, SetValueType> sourceNodesSet, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
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
        /// Returns the count of the nodes reachable from the start node, under the type constraints given
        /// </summary>
        public static int CountReachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet); // call normal version, is thread safe
            return adjacentNodesSet.Count;
        }

        public static int CountReachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv, threadId);
            return adjacentNodesSet.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static int CountReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet); // call normal version, is thread safe
            return targetNodesSet.Count;
        }

        public static int CountReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet, actionEnv, threadId);
            return targetNodesSet.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node via incoming edges, under the type constraints given
        /// </summary>
        public static int CountReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet); // call normal version, is thread safe
            return sourceNodesSet.Count;
        }

        public static int CountReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet, actionEnv, threadId);
            return sourceNodesSet.Count;
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
        /// Returns set of directed edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> ReachableEdgesDirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesDirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> ReachableEdgesDirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesDirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of undirected edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> ReachableEdgesUndirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesUndirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> ReachableEdgesUndirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesUndirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IUEdge edge = kvp.Key;
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
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of directed edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IDEdge, SetValueType> incidentEdgesSet, IGraph graph, int threadId)
        {
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IDEdge, SetValueType> incidentEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            }
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of undirectd edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IUEdge, SetValueType> incidentEdgesSet, IGraph graph, int threadId)
        {
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IUEdge, SetValueType> incidentEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            }
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
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
        /// Returns set of outgoing directed edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> ReachableEdgesOutgoingDirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesOutgoingDirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> ReachableEdgesOutgoingDirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesOutgoingDirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of outgoing undirected edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> ReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesOutgoingUndirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> ReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesOutgoingUndirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IUEdge edge = kvp.Key;
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
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of outgoing directed edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesOutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IDEdge, SetValueType> outgoingEdgesSet, IGraph graph, int threadId)
        {
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesOutgoingDirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesOutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IDEdge, SetValueType> outgoingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesOutgoingDirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of outgoing undirected edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesOutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IUEdge, SetValueType> outgoingEdgesSet, IGraph graph, int threadId)
        {
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesOutgoingUndirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesOutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IUEdge, SetValueType> outgoingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesOutgoingUndirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
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
        /// Returns set of incoming directed edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> ReachableEdgesIncomingDirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesIncomingDirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> ReachableEdgesIncomingDirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesIncomingDirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of incoming undirected edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> ReachableEdgesIncomingUndirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesIncomingUndirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false, threadId);
                graph.SetInternallyVisited(edge.Target, false, threadId);
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> ReachableEdgesIncomingUndirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesIncomingUndirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IUEdge edge = kvp.Key;
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
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesIncoming(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesIncoming(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of incoming directed edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesIncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IDEdge, SetValueType> incomingEdgesSet, IGraph graph, int threadId)
        {
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesIncomingDirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesIncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IDEdge, SetValueType> incomingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesIncomingDirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of incoming undirected edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesIncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IUEdge, SetValueType> incomingEdgesSet, IGraph graph, int threadId)
        {
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesIncomingUndirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            }
        }

        private static void ReachableEdgesIncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IUEdge, SetValueType> incomingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode, threadId))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true, threadId);
                ReachableEdgesIncomingUndirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the edges reachable from the start node, under the type constraints given
        /// </summary>
        public static int CountReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet.Count;
        }

        public static int CountReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet.Count;
        }

        /// <summary>
        /// Returns the count of the outgoing edges reachable from the start node, under the type constraints given
        /// </summary>
        public static int CountReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet.Count;
        }

        public static int CountReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet.Count;
        }

        /// <summary>
        /// Returns the count of the incoming edges reachable from the start node, under the type constraints given
        /// </summary>
        public static int CountReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet.Count;
        }

        public static int CountReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv, threadId);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet.Count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth); // call normal version, is thread safe 
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>(adjacentNodesToMinDepth.Count);
            foreach(INode node in adjacentNodesToMinDepth.Keys)
            {
                adjacentNodesSet.Add(node, null);
            }
            return adjacentNodesSet;
        }

        public static Dictionary<INode, SetValueType> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv, threadId);
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>(adjacentNodesToMinDepth.Count);
            foreach(INode node in adjacentNodesToMinDepth.Keys)
            {
                adjacentNodesSet.Add(node, null);
            }
            return adjacentNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachable(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv, threadId);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachable(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth via outgoing edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> BoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth); // call normal version, is thread safe
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>(targetNodesToMinDepth.Count);
            foreach(INode node in targetNodesToMinDepth.Keys)
            {
                targetNodesSet.Add(node, null);
            }
            return targetNodesSet;
        }

        public static Dictionary<INode, SetValueType> BoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth, actionEnv, threadId);
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>(targetNodesToMinDepth.Count);
            foreach(INode node in targetNodesToMinDepth.Keys)
            {
                targetNodesSet.Add(node, null);
            }
            return targetNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node within the given depth via outgoing edges, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableOutgoing(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, adjacentNodesToMinDepth, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth via incoming edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> BoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth); // call normal version, is thread safe
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>(sourceNodesToMinDepth.Count);
            foreach(INode node in sourceNodesToMinDepth.Keys)
            {
                sourceNodesSet.Add(node, null);
            }
            return sourceNodesSet;
        }

        public static Dictionary<INode, SetValueType> BoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth, actionEnv, threadId);
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>(sourceNodesToMinDepth.Count);
            foreach(INode node in sourceNodesToMinDepth.Keys)
            {
                sourceNodesSet.Add(node, null);
            }
            return sourceNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node within the given depth via incoming edges, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableIncoming(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, adjacentNodesToMinDepth, actionEnv, threadId);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns map of nodes to remaining depth reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<INode, int> BoundedReachableWithRemainingDepth(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth); // call normal version, is thread safe 
            return adjacentNodesToMinDepth;
        }

        public static Dictionary<INode, int> BoundedReachableWithRemainingDepth(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv, threadId);
            return adjacentNodesToMinDepth;
        }

        /// <summary>
        /// Returns map of nodes to remaining depth reachable from the start node within the given depth via outgoing edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth); // call normal version, is thread safe
            return targetNodesToMinDepth;
        }

        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth, actionEnv, threadId);
            return targetNodesToMinDepth;
        }

        /// <summary>
        /// Returns map of nodes to remaining depth reachable from the start node within the given depth via incoming edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth); // call normal version, is thread safe
            return sourceNodesToMinDepth;
        }

        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth, actionEnv, threadId);
            return sourceNodesToMinDepth;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the nodes reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth); // call normal version, is thread safe
            return adjacentNodesToMinDepth.Count;
        }

        public static int CountBoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv, threadId);
            return adjacentNodesToMinDepth.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node within the given depth via outgoing edges, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth); // call normal version, is thread safe
            return targetNodesToMinDepth.Count;
        }

        public static int CountBoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth, actionEnv, threadId);
            return targetNodesToMinDepth.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node within the given depth via incoming edges, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth); // call normal version, is thread safe
            return sourceNodesToMinDepth.Count;
        }

        public static int CountBoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth, actionEnv, threadId);
            return sourceNodesToMinDepth.Count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> BoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incidentEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> BoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of directed edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesDirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesDirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incidentEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesDirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesDirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of undirected edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesUndirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesUndirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incidentEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesUndirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesUndirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incidentEdgesSet;
        }

        /// <summary>
        /// Fills set of edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdges(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdges(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdges(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of directed edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesDirected(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IDEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesDirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesDirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of undirected edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesUndirected(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IUEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesUndirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesUndirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of outgoing edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return outgoingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of outgoing directed edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesOutgoingDirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingDirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return outgoingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesOutgoingDirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingDirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of outgoing undirected edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingUndirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return outgoingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingUndirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Fills set of outgoing edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesOutgoing(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of outgoing directed edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesOutgoingDirected(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IDEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesOutgoingDirected(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of outgoing undirected edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesOutgoingUndirected(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IUEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesOutgoingUndirected(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Returns set of incoming edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incomingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of incoming directed edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesIncomingDirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingDirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incomingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesIncomingDirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingDirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of incoming undirected edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesIncomingUndirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingUndirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incomingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesIncomingUndirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingUndirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incomingEdgesSet;
        }

        /// <summary>
        /// Fills set of incoming directed edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesIncomingDirected(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IDEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesIncomingDirected(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of incoming undirected edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesIncomingUndirected(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IUEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesIncomingUndirected(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        /// <summary>
        /// Fills set of incoming edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesIncoming(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incidentEdgesSet.Count;
        }

        public static int CountBoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incidentEdgesSet.Count;
        }

        /// <summary>
        /// Returns the count of the outgoing edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return outgoingEdgesSet.Count;
        }

        public static int CountBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return outgoingEdgesSet.Count;
        }

        /// <summary>
        /// Returns the count of the incoming edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph); // call normal version, is thread safe
            return incomingEdgesSet.Count;
        }

        public static int CountBoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv, threadId);
            return incomingEdgesSet.Count;
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
            {
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            }
            return result;
        }

        public static bool IsReachable(IGraph graph, INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachable(startNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            }
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

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
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
                foreach(IEdge edge in startNode.Incoming)
                {
                    ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                    if(!edge.InstanceOf(incidentEdgeType))
                        continue;
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
            {
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            }
            return result;
        }

        public static bool IsReachableOutgoing(IGraph graph, INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableOutgoing(startNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes, actionEnv, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            }
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

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
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
            {
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            }
            return result;
        }

        public static bool IsReachableIncoming(IGraph graph, INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableIncoming(startNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes, actionEnv, threadId);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false, threadId);
            }
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

            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
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

        public static bool IsAdjacent(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
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

        public static bool IsAdjacent(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            return false;
        }

        public static bool IsAdjacentOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
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

        public static bool IsAdjacentOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            return false;
        }

        public static bool IsAdjacentIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
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

        public static bool IsAdjacentIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(adjacentNode == endNode)
                    return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static bool IsIncident(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
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

        public static bool IsIncident(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
            }
            return false;
        }

        public static bool IsOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
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

        public static bool IsOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
            }
            return false;
        }

        public static bool IsIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
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

        public static bool IsIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
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
        /// Returns whether the end edge is reachable from the start node, under the type constraints given
        /// </summary>
        public static bool IsReachableEdges(IGraph graph, INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdges(startNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
            {
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            }
            return result;
        }

        public static bool IsReachableEdges(IGraph graph, INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdges(startNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedElems, actionEnv, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
            {
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            }
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

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
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
                foreach(IEdge edge in startNode.Incoming)
                {
                    ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                    if(!edge.InstanceOf(incidentEdgeType))
                        continue;
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
            {
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            }
            return result;
        }

        public static bool IsReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesOutgoing(startNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedElems, actionEnv, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
            {
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            }
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

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
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
            {
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            }
            return result;
        }

        public static bool IsReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IGraphElement> visitedElems = new List<IGraphElement>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesIncoming(startNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedElems, actionEnv, threadId);
            for(int i = 0; i < visitedElems.Count; ++i)
            {
                graph.SetInternallyVisited(visitedElems[i], false, threadId);
            }
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

            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
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

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachable(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachable(startNode, endNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth); // call normal version, is thread safe
            return result;
        }

        public static bool IsBoundedReachable(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachable(startNode, endNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachable(INode startNode, INode endNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge.Target == endNode)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachable(adjacentNode, endNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
                if(result == true)
                    break;
            }

            if(!result)
            {
                foreach(IEdge edge in startNode.Incoming)
                {
                    ++actionEnv.PerformanceInfo.SearchSteps;
                    if(!edge.InstanceOf(incidentEdgeType))
                        continue;
                    INode adjacentNode = edge.Source;
                    if(!adjacentNode.InstanceOf(adjacentNodeType))
                        continue;
                    if(edge.Source == endNode)
                        return true;
                    int nodeDepth;
                    if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                        continue;
                    adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                    result = IsBoundedReachable(adjacentNode, endNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableOutgoing(IGraph graph, INode startNode, INode endNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableOutgoing(startNode, endNode, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth); // call normal version, is thread safe
            return result;
        }

        public static bool IsBoundedReachableOutgoing(IGraph graph, INode startNode, INode endNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableOutgoing(startNode, endNode, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth, actionEnv, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableOutgoing(INode startNode, INode endNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge.Target == endNode)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableOutgoing(adjacentNode, endNode, depth - 1, outgoingEdgeType, targetNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableIncoming(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableIncoming(startNode, endNode, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth); // call normal version, is thread safe
            return result;
        }

        public static bool IsBoundedReachableIncoming(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableIncoming(startNode, endNode, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth, actionEnv, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableIncoming(INode startNode, INode endNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(edge.Source == endNode)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableIncoming(adjacentNode, endNode, depth - 1, incomingEdgeType, sourceNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableEdges(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdges(startNode, endEdge, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth); // call normal version, is thread safe
            return result;
        }

        public static bool IsBoundedReachableEdges(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdges(startNode, endEdge, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableEdges(INode startNode, IEdge endEdge, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableEdges(adjacentNode, endEdge, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
                if(result == true)
                    break;
            }

            if(!result)
            {
                foreach(IEdge edge in startNode.Incoming)
                {
                    ++actionEnv.PerformanceInfo.SearchSteps;
                    if(!edge.InstanceOf(incidentEdgeType))
                        continue;
                    INode adjacentNode = edge.Source;
                    if(!adjacentNode.InstanceOf(adjacentNodeType))
                        continue;
                    if(edge == endEdge)
                        return true;
                    int nodeDepth;
                    if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                        continue;
                    adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                    result = IsBoundedReachableEdges(adjacentNode, endEdge, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesOutgoing(startNode, endEdge, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth); // call normal version, is thread safe
            return result;
        }

        public static bool IsBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesOutgoing(startNode, endEdge, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth, actionEnv, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableEdgesOutgoing(INode startNode, IEdge endEdge, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableEdgesOutgoing(adjacentNode, endEdge, depth - 1, outgoingEdgeType, targetNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesIncoming(startNode, endEdge, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth); // call normal version, is thread safe
            return result;
        }

        public static bool IsBoundedReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesIncoming(startNode, endEdge, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth, actionEnv, threadId);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableEdgesIncoming(INode startNode, IEdge endEdge, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableEdgesIncoming(adjacentNode, endEdge, depth - 1, incomingEdgeType, sourceNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId);
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
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
            {
                yield return node;
            }
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
                {
                    yield return node;
                }
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
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> ReachableRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return node;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return node;
                }
            }
        }

        public static IEnumerable<INode> ReachableIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> ReachableIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
            {
                yield return node;
            }
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
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> ReachableIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return node;
                }
            }
        }

        public static IEnumerable<INode> ReachableOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> ReachableOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            foreach(INode node in ReachableOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
            {
                yield return node;
            }
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
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> ReachableOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                yield return adjacentNode;

                foreach(INode node in ReachableOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return node;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<IEdge> ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            visitedNodes.Add(startNode, null);
            foreach(IEdge edge in ReachableEdgesRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
            {
                yield return edge;
            }
        }

        public static IEnumerable<IEdge> ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            visitedNodes.Add(startNode, null);
            foreach(IEdge edge in ReachableEdgesRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
            {
                yield return edge;
            }
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
                {
                    yield return reachableEdge;
                }
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
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            visitedNodes.Add(startNode, null);
            foreach(IEdge edge in ReachableEdgesIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
            {
                yield return edge;
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            visitedNodes.Add(startNode, null);
            foreach(IEdge edge in ReachableEdgesIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
            {
                yield return edge;
            }
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
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            visitedNodes.Add(startNode, null);
            foreach(IEdge edge in ReachableEdgesOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, threadId))
            {
                yield return edge;
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> visitedNodes = new Dictionary<INode, SetValueType>((int)Math.Sqrt(graph.NumNodes));
            visitedNodes.Add(startNode, null);
            foreach(IEdge edge in ReachableEdgesOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
            {
                yield return edge;
            }
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
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, SetValueType> visitedNodes, IActionExecutionEnvironment actionEnv, int threadId)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(visitedNodes.ContainsKey(adjacentNode))
                    continue;
                visitedNodes.Add(adjacentNode, null);
                foreach(IEdge reachableEdge in ReachableEdgesOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<INode> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth)) // call normal version, is thread safe
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId))
            {
                yield return node;
            }
        }

        private static IEnumerable<INode> BoundedReachableRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId))
                {
                    yield return node;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv, threadId))
                {
                    yield return node;
                }
            }
        }

        public static IEnumerable<INode> BoundedReachableIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, sourceNodesToMinDepth)) // call normal version, is thread safe
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> BoundedReachableIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, sourceNodesToMinDepth, actionEnv, threadId))
            {
                yield return node;
            }
        }

        private static IEnumerable<INode> BoundedReachableIncomingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> sourceNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                sourceNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableIncomingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, sourceNodesToMinDepth, actionEnv, threadId))
                {
                    yield return node;
                }
            }
        }

        public static IEnumerable<INode> BoundedReachableOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, targetNodesToMinDepth)) // call normal version, is thread safe
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> BoundedReachableOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, targetNodesToMinDepth, actionEnv, threadId))
            {
                yield return node;
            }
        }

        private static IEnumerable<INode> BoundedReachableOutgoingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> targetNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                targetNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableOutgoingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, targetNodesToMinDepth, actionEnv, threadId))
                {
                    yield return node;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<IEdge> BoundedReachableEdges(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<IEdge, SetValueType> visitedEdges = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            foreach(IEdge edge in BoundedReachableEdgesRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, visitedEdges, adjacentNodesToMinDepth, threadId))
            {
                yield return edge;
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdges(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> visitedEdges = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            foreach(IEdge edge in BoundedReachableEdgesRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, visitedEdges, adjacentNodesToMinDepth, actionEnv, threadId))
            {
                yield return edge;
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<IEdge, SetValueType> visitedEdges, Dictionary<INode, int> adjacentNodesToMinDepth, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, adjacentNodesToMinDepth, threadId))
                {
                    yield return reachableEdge;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, adjacentNodesToMinDepth, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<IEdge, SetValueType> visitedEdges, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, adjacentNodesToMinDepth, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, adjacentNodesToMinDepth, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<IEdge, SetValueType> visitedEdges = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            foreach(IEdge edge in BoundedReachableEdgesIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, visitedEdges, sourceNodesToMinDepth, threadId))
            {
                yield return edge;
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> visitedEdges = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            foreach(IEdge edge in BoundedReachableEdgesIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, visitedEdges, sourceNodesToMinDepth, actionEnv, threadId))
            {
                yield return edge;
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesIncomingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<IEdge, SetValueType> visitedEdges, Dictionary<INode, int> sourceNodesToMinDepth, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                sourceNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesIncomingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, sourceNodesToMinDepth, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesIncomingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<IEdge, SetValueType> visitedEdges, Dictionary<INode, int> sourceNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                sourceNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesIncomingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, sourceNodesToMinDepth, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int threadId)
        {
            Dictionary<IEdge, SetValueType> visitedEdges = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            foreach(IEdge edge in BoundedReachableEdgesOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, visitedEdges, targetNodesToMinDepth, threadId))
            {
                yield return edge;
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> visitedEdges = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            foreach(IEdge edge in BoundedReachableEdgesOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, visitedEdges, targetNodesToMinDepth, actionEnv, threadId))
            {
                yield return edge;
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesOutgoingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<IEdge, SetValueType> visitedEdges, Dictionary<INode, int> targetNodesToMinDepth, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                targetNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesOutgoingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, targetNodesToMinDepth, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesOutgoingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<IEdge, SetValueType> visitedEdges, Dictionary<INode, int> targetNodesToMinDepth, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!visitedEdges.ContainsKey(edge))
                {
                    visitedEdges[edge] = null;
                    yield return edge;
                }
                int nodeDepth;
                if(targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                targetNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesOutgoingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, visitedEdges, targetNodesToMinDepth, actionEnv, threadId))
                {
                    yield return reachableEdge;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static bool EqualsAny(IGraph candidate, IDictionary<IGraph, SetValueType> graphsToCheckAgainst, bool includingAttributes, int threadId)
        {
            if(candidate == null)
                return false;
            if(graphsToCheckAgainst == null)
                return false;

            // we're called from a parallel matcher, use non-parallel version of comparison
            if(includingAttributes)
            {
                foreach(IGraph graphToCheckAgainst in graphsToCheckAgainst.Keys)
                {
                    if(candidate.IsIsomorph(graphToCheckAgainst))
                        return true;
                }
            }
            else
            {
                foreach(IGraph graphToCheckAgainst in graphsToCheckAgainst.Keys)
                {
                    if(candidate.HasSameStructure(graphToCheckAgainst))
                        return true;
                }
            }
            return false;
        }

        public static IGraph GetEquivalent(IGraph candidate, IDictionary<IGraph, SetValueType> graphsToCheckAgainst, bool includingAttributes, int threadId)
        {
            if(candidate == null)
                return null;
            if(graphsToCheckAgainst == null)
                return null;

            // we're called from a parallel matcher, use non-parallel version of comparison
            if(includingAttributes)
            {
                foreach(IGraph graphToCheckAgainst in graphsToCheckAgainst.Keys)
                {
                    if(candidate.IsIsomorph(graphToCheckAgainst))
                        return graphToCheckAgainst;
                }
            }
            else
            {
                foreach(IGraph graphToCheckAgainst in graphsToCheckAgainst.Keys)
                {
                    if(candidate.HasSameStructure(graphToCheckAgainst))
                        return graphToCheckAgainst;
                }
            }
            return null;
        }
    }
}
