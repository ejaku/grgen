/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Collections;

// this is not related in any way to IGraphHelpers.cs
// don't forget GraphHelperParallel.cs for the parallelized versions

namespace de.unika.ipd.grGen.libGr
{
    public static partial class GraphHelper
    {
        /// <summary>
        /// Returns the nodes in the graph of the type given, as set
        /// </summary>
        public static Dictionary<INode, SetValueType> Nodes(IGraph graph, NodeType nodeType)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in graph.GetCompatibleNodes(nodeType))
            {
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        public static Dictionary<INode, SetValueType> Nodes(IGraph graph, NodeType nodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in graph.GetCompatibleNodes(nodeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        /// <summary>
        /// Returns the edges in the graph of the type given, as set of IEdge
        /// </summary>
        public static Dictionary<IEdge, SetValueType> Edges(IGraph graph, EdgeType edgeType)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IEdge, SetValueType> Edges(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        /// <summary>
        /// Returns the directed edges in the graph of the type given, as set of IDEdge
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> EdgesDirected(IGraph graph, EdgeType edgeType)
        {
            Dictionary<IDEdge, SetValueType> edgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> EdgesDirected(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> edgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        /// <summary>
        /// Returns the undirected edges in the graph of the type given, as set of IUEdge
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> EdgesUndirected(IGraph graph, EdgeType edgeType)
        {
            Dictionary<IUEdge, SetValueType> edgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> EdgesUndirected(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> edgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in graph.GetCompatibleEdges(edgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the nodes in the graph of the type given
        /// </summary>
        public static int CountNodes(IGraph graph, NodeType nodeType)
        {
            return graph.GetNumCompatibleNodes(nodeType);
        }

        public static int CountNodes(IGraph graph, NodeType nodeType, IActionExecutionEnvironment actionEnv)
        {
            return graph.GetNumCompatibleNodes(nodeType);
        }

        /// <summary>
        /// Returns the count of the edges in the graph of the type given
        /// </summary>
        public static int CountEdges(IGraph graph, EdgeType edgeType)
        {
            return graph.GetNumCompatibleEdges(edgeType);
        }

        public static int CountEdges(IGraph graph, EdgeType edgeType, IActionExecutionEnvironment actionEnv)
        {
            return graph.GetNumCompatibleEdges(edgeType);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IGraphElement GetGraphElement(INamedGraph graph, string name)
        {
            return graph.GetGraphElement(name);
        }

        public static IGraphElement GetGraphElement(INamedGraph graph, string name, IActionExecutionEnvironment actionEnv)
        {
            ++actionEnv.PerformanceInfo.SearchSteps;
            return graph.GetGraphElement(name);
        }

        public static INode GetNode(INamedGraph graph, string name)
        {
            return graph.GetNode(name);
        }

        public static INode GetNode(INamedGraph graph, string name, IActionExecutionEnvironment actionEnv)
        {
            ++actionEnv.PerformanceInfo.SearchSteps;
            return graph.GetNode(name);
        }

        public static IEdge GetEdge(INamedGraph graph, string name)
        {
            return graph.GetEdge(name);
        }

        public static IEdge GetEdge(INamedGraph graph, string name, IActionExecutionEnvironment actionEnv)
        {
            ++actionEnv.PerformanceInfo.SearchSteps;
            return graph.GetEdge(name);
        }

        public static INode GetNode(INamedGraph graph, string name, NodeType nodeType)
        {
            INode node = graph.GetNode(name);
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static INode GetNode(INamedGraph graph, string name, NodeType nodeType, IActionExecutionEnvironment actionEnv)
        {
            INode node = graph.GetNode(name);
            ++actionEnv.PerformanceInfo.SearchSteps;
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static IEdge GetEdge(INamedGraph graph, string name, EdgeType edgeType)
        {
            IEdge edge = graph.GetEdge(name);
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        public static IEdge GetEdge(INamedGraph graph, string name, EdgeType edgeType, IActionExecutionEnvironment actionEnv)
        {
            IEdge edge = graph.GetEdge(name);
            ++actionEnv.PerformanceInfo.SearchSteps;
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static INode GetNode(IGraph graph, int uniqueId)
        {
            return graph.GetNode(uniqueId);
        }

        public static INode GetNode(IGraph graph, int uniqueId, IActionExecutionEnvironment actionEnv)
        {
            ++actionEnv.PerformanceInfo.SearchSteps;
            return graph.GetNode(uniqueId);
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId)
        {
            return graph.GetEdge(uniqueId);
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId, IActionExecutionEnvironment actionEnv)
        {
            ++actionEnv.PerformanceInfo.SearchSteps;
            return graph.GetEdge(uniqueId);
        }

        public static INode GetNode(IGraph graph, int uniqueId, NodeType nodeType)
        {
            INode node = graph.GetNode(uniqueId);
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static INode GetNode(IGraph graph, int uniqueId, NodeType nodeType, IActionExecutionEnvironment actionEnv)
        {
            INode node = graph.GetNode(uniqueId);
            ++actionEnv.PerformanceInfo.SearchSteps;
            if(node == null)
                return null;
            return node.InstanceOf(nodeType) ? node : null;
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId, EdgeType edgeType)
        {
            IEdge edge = graph.GetEdge(uniqueId);
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        public static IEdge GetEdge(IGraph graph, int uniqueId, EdgeType edgeType, IActionExecutionEnvironment actionEnv)
        {
            IEdge edge = graph.GetEdge(uniqueId);
            ++actionEnv.PerformanceInfo.SearchSteps;
            if(edge == null)
                return null;
            return edge.InstanceOf(edgeType) ? edge : null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of nodes adjacent to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> Adjacent(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
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

        public static Dictionary<INode, SetValueType> Adjacent(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<INode, SetValueType> AdjacentOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
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

        public static Dictionary<INode, SetValueType> AdjacentOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<INode, SetValueType> AdjacentIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
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

        public static Dictionary<INode, SetValueType> AdjacentIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static int CountAdjacent(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            return count;
        }

        public static int CountAdjacent(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the nodes adjacent to the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static int CountAdjacentOutgoing(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            return count;
        }

        public static int CountAdjacentOutgoing(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the nodes adjacent to the start node via incoming edges, under the type constraints given
        /// </summary>
        public static int CountAdjacentIncoming(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            int count = 0;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            return count;
        }

        public static int CountAdjacentIncoming(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ++count;
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                graph.SetInternallyVisited(adjacentNode, false);
            }
            return count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges incident to the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> Incident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
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

        public static Dictionary<IEdge, SetValueType> Incident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IDEdge, SetValueType> IncidentDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
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

        public static Dictionary<IDEdge, SetValueType> IncidentDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IUEdge, SetValueType> IncidentUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
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

        public static Dictionary<IUEdge, SetValueType> IncidentUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
            }
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IEdge, SetValueType> Outgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
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

        public static Dictionary<IEdge, SetValueType> Outgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IDEdge, SetValueType> OutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
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

        public static Dictionary<IDEdge, SetValueType> OutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IUEdge, SetValueType> OutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
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

        public static Dictionary<IUEdge, SetValueType> OutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IEdge, SetValueType> Incoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
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

        public static Dictionary<IEdge, SetValueType> Incoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IDEdge, SetValueType> IncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
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

        public static Dictionary<IDEdge, SetValueType> IncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<IUEdge, SetValueType> IncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
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

        public static Dictionary<IUEdge, SetValueType> IncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static int CountIncident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
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

        public static int CountIncident(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                ++count;
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static int CountOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
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

        public static int CountOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static int CountIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
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

        public static int CountIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
        public static Dictionary<INode, SetValueType> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet);
            return adjacentNodesSet;
        }

        public static Dictionary<INode, SetValueType> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv);
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

        private static void Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, SetValueType> adjacentNodesSet, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(adjacentNodesSet.ContainsKey(adjacentNode))
                    continue;
                adjacentNodesSet[adjacentNode] = null;
                Reachable(adjacentNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet);
            return targetNodesSet;
        }

        public static Dictionary<INode, SetValueType> ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet, actionEnv);
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

        private static void ReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, SetValueType> targetNodesSet, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(targetNodesSet.ContainsKey(adjacentNode))
                    continue;
                targetNodesSet[adjacentNode] = null;
                ReachableOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, targetNodesSet, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node via incoming edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet);
            return sourceNodesSet;
        }

        public static Dictionary<INode, SetValueType> ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet, actionEnv);
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

        private static void ReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, SetValueType> sourceNodesSet, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(sourceNodesSet.ContainsKey(adjacentNode))
                    continue;
                sourceNodesSet[adjacentNode] = null;
                ReachableIncoming(adjacentNode, incomingEdgeType, sourceNodeType, sourceNodesSet, actionEnv);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the nodes reachable from the start node, under the type constraints given
        /// </summary>
        public static int CountReachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet);
            return adjacentNodesSet.Count;
        }

        public static int CountReachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            Reachable(startNode, incidentEdgeType, adjacentNodeType, adjacentNodesSet, actionEnv);
            return adjacentNodesSet.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node via outgoing edges, under the type constraints given
        /// </summary>
        public static int CountReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet);
            return targetNodesSet.Count;
        }

        public static int CountReachableOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>();
            ReachableOutgoing(startNode, outgoingEdgeType, targetNodeType, targetNodesSet, actionEnv);
            return targetNodesSet.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node via incoming edges, under the type constraints given
        /// </summary>
        public static int CountReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet);
            return sourceNodesSet.Count;
        }

        public static int CountReachableIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>();
            ReachableIncoming(startNode, incomingEdgeType, sourceNodeType, sourceNodesSet, actionEnv);
            return sourceNodesSet.Count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> ReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> ReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> ReachableEdgesDirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesDirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> ReachableEdgesDirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesDirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> ReachableEdgesUndirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesUndirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> ReachableEdgesUndirected(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesUndirected(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet;
        }

        /// <summary>
        /// Fills set of edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, IGraph graph)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            }
        }

        private static void ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdges(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of directed edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IDEdge, SetValueType> incidentEdgesSet, IGraph graph)
        {
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            }
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            }
        }

        private static void ReachableEdgesDirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IDEdge, SetValueType> incidentEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            }
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesDirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of undirected edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IUEdge, SetValueType> incidentEdgesSet, IGraph graph)
        {
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            }
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            }
        }

        private static void ReachableEdgesUndirected(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IUEdge, SetValueType> incidentEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            }
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesUndirected(adjacentNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of outgoing edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> ReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> ReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of outgoing directed edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> ReachableEdgesOutgoingDirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesOutgoingDirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> ReachableEdgesOutgoingDirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesOutgoingDirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of outgoing undirected edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> ReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesOutgoingUndirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> ReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesOutgoingUndirected(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Fills set of outgoing edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, IGraph graph)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph);
            }
        }

        private static void ReachableEdgesOutgoing(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesOutgoing(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of outgoing directed edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesOutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IDEdge, SetValueType> outgoingEdgesSet, IGraph graph)
        {
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesOutgoingDirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph);
            }
        }

        private static void ReachableEdgesOutgoingDirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IDEdge, SetValueType> outgoingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesOutgoingDirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of outgoing undirected edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesOutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IUEdge, SetValueType> outgoingEdgesSet, IGraph graph)
        {
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesOutgoingUndirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph);
            }
        }

        private static void ReachableEdgesOutgoingUndirected(INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IUEdge, SetValueType> outgoingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesOutgoingUndirected(adjacentNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of incoming edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> ReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> ReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of incoming edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> ReachableEdgesIncomingDirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesIncomingDirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> ReachableEdgesIncomingDirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            ReachableEdgesIncomingDirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IDEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IDEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of incoming edges reachable from the start node, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> ReachableEdgesIncomingUndirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesIncomingUndirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> ReachableEdgesIncomingUndirected(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            ReachableEdgesIncomingUndirected(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv);
            foreach(KeyValuePair<IUEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IUEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet;
        }

        /// <summary>
        /// Fills set of incoming edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, IGraph graph)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesIncoming(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph);
            }
        }

        private static void ReachableEdgesIncoming(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesIncoming(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of incoming directed edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesIncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IDEdge, SetValueType> incomingEdgesSet, IGraph graph)
        {
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesIncomingDirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph);
            }
        }

        private static void ReachableEdgesIncomingDirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IDEdge, SetValueType> incomingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesIncomingDirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of incoming undirected edges reachable from the start node, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void ReachableEdgesIncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IUEdge, SetValueType> incomingEdgesSet, IGraph graph)
        {
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesIncomingUndirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph);
            }
        }

        private static void ReachableEdgesIncomingUndirected(INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IUEdge, SetValueType> incomingEdgesSet, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                ReachableEdgesIncomingUndirected(adjacentNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the edges reachable from the start node, under the type constraints given
        /// </summary>
        public static int CountReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incidentEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incidentEdgesSet.Count;
        }

        public static int CountReachableEdges(IGraph graph, INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdges(startNode, incidentEdgeType, adjacentNodeType, incidentEdgesSet, graph, actionEnv);
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
        public static int CountReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in outgoingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return outgoingEdgesSet.Count;
        }

        public static int CountReachableEdgesOutgoing(IGraph graph, INode startNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesOutgoing(startNode, outgoingEdgeType, targetNodeType, outgoingEdgesSet, graph, actionEnv);
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
        public static int CountReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph);
            foreach(KeyValuePair<IEdge, SetValueType> kvp in incomingEdgesSet)
            {
                IEdge edge = kvp.Key;
                graph.SetInternallyVisited(edge.Source, false);
                graph.SetInternallyVisited(edge.Target, false);
            }
            return incomingEdgesSet.Count;
        }

        public static int CountReachableEdgesIncoming(IGraph graph, INode startNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            ReachableEdgesIncoming(startNode, incomingEdgeType, sourceNodeType, incomingEdgesSet, graph, actionEnv);
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
        public static Dictionary<INode, SetValueType> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth);
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>(adjacentNodesToMinDepth.Count);
            foreach(INode node in adjacentNodesToMinDepth.Keys)
                adjacentNodesSet.Add(node, null);
            return adjacentNodesSet;
        }

        public static Dictionary<INode, SetValueType> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv);
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>(adjacentNodesToMinDepth.Count);
            foreach(INode node in adjacentNodesToMinDepth.Keys)
                adjacentNodesSet.Add(node, null);
            return adjacentNodesSet;
        }

        /// <summary>
        /// Fills set of nodes reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachable(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachable(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth);
            }
        }

        private static void BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachable(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachable(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth via outgoing edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> BoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth);
            Dictionary<INode, SetValueType> targetNodesSet = new Dictionary<INode, SetValueType>(targetNodesToMinDepth.Count);
            foreach(INode node in targetNodesToMinDepth.Keys)
            {
                targetNodesSet.Add(node, null);
            }
            return targetNodesSet;
        }

        public static Dictionary<INode, SetValueType> BoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth, actionEnv);
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
        private static void BoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableOutgoing(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, adjacentNodesToMinDepth);
            }
        }

        private static void BoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IDictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(outgoingEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableOutgoing(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, adjacentNodesToMinDepth, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth via incoming edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, SetValueType> BoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth);
            Dictionary<INode, SetValueType> sourceNodesSet = new Dictionary<INode, SetValueType>(sourceNodesToMinDepth.Count);
            foreach(INode node in sourceNodesToMinDepth.Keys)
            {
                sourceNodesSet.Add(node, null);
            }
            return sourceNodesSet;
        }

        public static Dictionary<INode, SetValueType> BoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth, actionEnv);
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
        private static void BoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableIncoming(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, adjacentNodesToMinDepth);
            }
        }

        private static void BoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(!edge.InstanceOf(incomingEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableIncoming(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, adjacentNodesToMinDepth, actionEnv);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<INode, int> BoundedReachableWithRemainingDepth(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth);
            return adjacentNodesToMinDepth;
        }

        public static Dictionary<INode, int> BoundedReachableWithRemainingDepth(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv);
            return adjacentNodesToMinDepth;
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth via outgoing edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth);
            return targetNodesToMinDepth;
        }

        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth, actionEnv);
            return targetNodesToMinDepth;
        }

        /// <summary>
        /// Returns set of nodes reachable from the start node within the given depth via incoming edges, under the type constraints given
        /// </summary>
        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth);
            return sourceNodesToMinDepth;
        }

        public static Dictionary<INode, int> BoundedReachableWithRemainingDepthIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth, actionEnv);
            return sourceNodesToMinDepth;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the nodes reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth);
            return adjacentNodesToMinDepth.Count;
        }

        public static int CountBoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachable(startNode, depth, incidentEdgeType, adjacentNodeType, adjacentNodesToMinDepth, actionEnv);
            return adjacentNodesToMinDepth.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node within the given depth via outgoing edges, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth);
            return targetNodesToMinDepth.Count;
        }

        public static int CountBoundedReachableOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, targetNodesToMinDepth, actionEnv);
            return targetNodesToMinDepth.Count;
        }

        /// <summary>
        /// Returns the count of the nodes reachable from the start node within the given depth via incoming edges, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth);
            return sourceNodesToMinDepth.Count;
        }

        public static int CountBoundedReachableIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableIncoming(startNode, depth, incomingEdgeType, sourceNodeType, sourceNodesToMinDepth, actionEnv);
            return sourceNodesToMinDepth.Count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns set of edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> BoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            return incidentEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> BoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of directed edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesDirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesDirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            return incidentEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesDirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> incidentEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesDirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incidentEdgesSet;
        }

        /// <summary>
        /// Returns set of undirected edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesUndirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesUndirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            return incidentEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesUndirected(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> incidentEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesUndirected(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incidentEdgesSet;
        }

        /// <summary>
        /// Fills set of edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdges(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdges(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdges(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdges(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdges(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdges(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of directed edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesDirected(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IDEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesDirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            }
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesDirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesDirected(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IDEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesDirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesDirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of undirected edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesUndirected(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IUEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesUndirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            }
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                incidentEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesUndirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesUndirected(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, Dictionary<IUEdge, SetValueType> incidentEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesUndirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesUndirected(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of outgoing edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph);
            return outgoingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Returns set of outgoing directed edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesOutgoingDirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingDirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph);
            return outgoingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesOutgoingDirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> outgoingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingDirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return outgoingEdgesSet;
        }
        /// <summary>
        /// Returns set of outgoing undirected edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingUndirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph);
            return outgoingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesOutgoingUndirected(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> outgoingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoingUndirected(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return outgoingEdgesSet;
        }

        /// <summary>
        /// Fills set of outgoing edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesOutgoing(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesOutgoing(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesOutgoing(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of outgoing directed edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesOutgoingDirected(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IDEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesOutgoingDirected(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesOutgoingDirected(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IDEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesOutgoingDirected(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of outgoing undirected edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesOutgoingUndirected(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IUEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                outgoingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesOutgoingUndirected(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesOutgoingUndirected(INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, Dictionary<IUEdge, SetValueType> outgoingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesOutgoingUndirected(adjacentNode, depth - 1, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Returns set of incoming edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph);
            return incomingEdgesSet;
        }

        public static Dictionary<IEdge, SetValueType> BoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of incoming directed edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesIncomingDirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingDirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph);
            return incomingEdgesSet;
        }

        public static Dictionary<IDEdge, SetValueType> BoundedReachableEdgesIncomingDirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IDEdge, SetValueType> incomingEdgesSet = new Dictionary<IDEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingDirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incomingEdgesSet;
        }

        /// <summary>
        /// Returns set of incoming undirected edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesIncomingUndirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingUndirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph);
            return incomingEdgesSet;
        }

        public static Dictionary<IUEdge, SetValueType> BoundedReachableEdgesIncomingUndirected(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IUEdge, SetValueType> incomingEdgesSet = new Dictionary<IUEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncomingUndirected(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incomingEdgesSet;
        }

        /// <summary>
        /// Fills set of incoming edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesIncoming(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesIncoming(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesIncoming(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of incoming directed edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesIncomingDirected(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IDEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesIncomingDirected(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesIncomingDirected(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IDEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IDEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesIncomingDirected(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        /// <summary>
        /// Fills set of incoming undirected edges reachable from the start node within the given depth, under the type constraints given, in a depth-first walk
        /// </summary>
        private static void BoundedReachableEdgesIncomingUndirected(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IUEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                incomingEdgesSet[edge] = null;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                BoundedReachableEdgesIncomingUndirected(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph);
            }
        }

        private static void BoundedReachableEdgesIncomingUndirected(INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, Dictionary<IUEdge, SetValueType> incomingEdgesSet, Dictionary<INode, int> adjacentNodesToMinDepth, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                return;
            foreach(IUEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                BoundedReachableEdgesIncomingUndirected(adjacentNode, depth - 1, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph);
            return incidentEdgesSet.Count;
        }

        public static int CountBoundedReachableEdges(IGraph graph, INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incidentEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdges(startNode, depth, incidentEdgeType, adjacentNodeType, incidentEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incidentEdgesSet.Count;
        }

        /// <summary>
        /// Returns the count of the outgoing edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph);
            return outgoingEdgesSet.Count;
        }

        public static int CountBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> outgoingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesOutgoing(startNode, depth, outgoingEdgeType, targetNodeType, outgoingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return outgoingEdgesSet.Count;
        }

        /// <summary>
        /// Returns the count of the incoming edges reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static int CountBoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph);
            return incomingEdgesSet.Count;
        }

        public static int CountBoundedReachableEdgesIncoming(IGraph graph, INode startNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            // todo: more performant implementation with internally visited used for marking and list for unmarking instead of hash set
            Dictionary<IEdge, SetValueType> incomingEdgesSet = new Dictionary<IEdge, SetValueType>();
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            BoundedReachableEdgesIncoming(startNode, depth, incomingEdgeType, sourceNodeType, incomingEdgesSet, adjacentNodesToMinDepth, graph, actionEnv);
            return incomingEdgesSet.Count;
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

        public static bool IsAdjacent(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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
                ++actionEnv.PerformanceInfo.SearchSteps;
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

        public static bool IsAdjacentOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Outgoing)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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

        public static bool IsAdjacentIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.Incoming)
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
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

        public static bool IsIncident(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
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
            }
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

        public static bool IsOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
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

        public static bool IsIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
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
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end node is reachable from the start node, under the type constraints given
        /// </summary>
        public static bool IsReachable(IGraph graph, INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachable(startNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        public static bool IsReachable(IGraph graph, INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachable(startNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, under the type constraints given
        /// </summary>
        private static bool IsReachable(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge.Target == endNode)
                    return true;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes);
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
                    if(edge.Source == endNode)
                        return true;
                    if(graph.IsInternallyVisited(adjacentNode))
                        continue;
                    graph.SetInternallyVisited(adjacentNode, true);
                    visitedNodes.Add(adjacentNode);
                    result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        private static bool IsReachable(INode startNode, INode endNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
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
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv);
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
                    if(graph.IsInternallyVisited(adjacentNode))
                        continue;
                    graph.SetInternallyVisited(adjacentNode, true);
                    visitedNodes.Add(adjacentNode);
                    result = IsReachable(adjacentNode, endNode, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv);
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
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableOutgoing(startNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        public static bool IsReachableOutgoing(IGraph graph, INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableOutgoing(startNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes, actionEnv);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsReachableOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge.Target == endNode)
                    return true;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableOutgoing(adjacentNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableOutgoing(INode startNode, INode endNode, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
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
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableOutgoing(adjacentNode, endNode, outgoingEdgeType, targetNodeType, graph, visitedNodes, actionEnv);
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
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableIncoming(startNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        public static bool IsReachableIncoming(IGraph graph, INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableIncoming(startNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes, actionEnv);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsReachableIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(edge.Source == endNode)
                    return true;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableIncoming(adjacentNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableIncoming(INode startNode, INode endNode, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
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
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableIncoming(adjacentNode, endNode, incomingEdgeType, sourceNodeType, graph, visitedNodes, actionEnv);
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
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdges(startNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        public static bool IsReachableEdges(IGraph graph, INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdges(startNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, under the type constraints given
        /// </summary>
        private static bool IsReachableEdges(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedNodes);
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
                    if(edge == endEdge)
                        return true;
                    if(graph.IsInternallyVisited(adjacentNode))
                        continue;
                    graph.SetInternallyVisited(adjacentNode, true);
                    visitedNodes.Add(adjacentNode);
                    result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedNodes);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        private static bool IsReachableEdges(INode startNode, IEdge endEdge, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
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
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv);
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
                    if(graph.IsInternallyVisited(adjacentNode))
                        continue;
                    graph.SetInternallyVisited(adjacentNode, true);
                    visitedNodes.Add(adjacentNode);
                    result = IsReachableEdges(adjacentNode, endEdge, incidentEdgeType, adjacentNodeType, graph, visitedNodes, actionEnv);
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
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesOutgoing(startNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        public static bool IsReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesOutgoing(startNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedNodes, actionEnv);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsReachableEdgesOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableEdgesOutgoing(adjacentNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedNodes);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableEdgesOutgoing(INode startNode, IEdge endEdge, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
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
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableEdgesOutgoing(adjacentNode, endEdge, outgoingEdgeType, targetNodeType, graph, visitedNodes, actionEnv);
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
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesIncoming(startNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedNodes);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        public static bool IsReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            List<INode> visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
            bool result = IsReachableEdgesIncoming(startNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedNodes, actionEnv);
            for(int i = 0; i < visitedNodes.Count; ++i)
            {
                graph.SetInternallyVisited(visitedNodes[i], false);
            }
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsReachableEdgesIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<INode> visitedNodes)
        {
            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableEdgesIncoming(adjacentNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedNodes);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsReachableEdgesIncoming(INode startNode, IEdge endEdge, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
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
                if(graph.IsInternallyVisited(adjacentNode))
                    continue;
                graph.SetInternallyVisited(adjacentNode, true);
                visitedNodes.Add(adjacentNode);
                result = IsReachableEdgesIncoming(adjacentNode, endEdge, incomingEdgeType, sourceNodeType, graph, visitedNodes, actionEnv);
                if(result == true)
                    break;
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachable(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachable(startNode, endNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth);
            return result;
        }

        public static bool IsBoundedReachable(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachable(startNode, endNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachable(INode startNode, INode endNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge.Target == endNode)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachable(adjacentNode, endNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth);
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
                    if(edge.Source == endNode)
                        return true;
                    int nodeDepth;
                    if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                        continue;
                    adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                    result = IsBoundedReachable(adjacentNode, endNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        private static bool IsBoundedReachable(INode startNode, INode endNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
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
                result = IsBoundedReachable(adjacentNode, endNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv);
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
                    result = IsBoundedReachable(adjacentNode, endNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableOutgoing(IGraph graph, INode startNode, INode endNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableOutgoing(startNode, endNode, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth);
            return result;
        }

        public static bool IsBoundedReachableOutgoing(IGraph graph, INode startNode, INode endNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableOutgoing(startNode, endNode, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth, actionEnv);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableOutgoing(INode startNode, INode endNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge.Target == endNode)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableOutgoing(adjacentNode, endNode, depth - 1, outgoingEdgeType, targetNodeType, graph, adjacentNodesToMinDepth);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsBoundedReachableOutgoing(INode startNode, INode endNode, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
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
                result = IsBoundedReachableOutgoing(adjacentNode, endNode, depth - 1, outgoingEdgeType, targetNodeType, graph, adjacentNodesToMinDepth, actionEnv);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableIncoming(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableIncoming(startNode, endNode, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth);
            return result;
        }

        public static bool IsBoundedReachableIncoming(IGraph graph, INode startNode, INode endNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableIncoming(startNode, endNode, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth, actionEnv);
            return result;
        }

        /// <summary>
        /// Returns whether the end node is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableIncoming(INode startNode, INode endNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(edge.Source == endNode)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableIncoming(adjacentNode, endNode, depth - 1, incomingEdgeType, sourceNodeType, graph, adjacentNodesToMinDepth);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsBoundedReachableIncoming(INode startNode, INode endNode, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
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
                result = IsBoundedReachableIncoming(adjacentNode, endNode, depth - 1, incomingEdgeType, sourceNodeType, graph, adjacentNodesToMinDepth, actionEnv);
                if(result == true)
                    break;
            }

            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableEdges(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdges(startNode, endEdge, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth);
            return result;
        }

        public static bool IsBoundedReachableEdges(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdges(startNode, endEdge, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableEdges(INode startNode, IEdge endEdge, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableEdges(adjacentNode, endEdge, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth);
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
                    if(edge == endEdge)
                        return true;
                    int nodeDepth;
                    if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                        continue;
                    adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                    result = IsBoundedReachableEdges(adjacentNode, endEdge, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        private static bool IsBoundedReachableEdges(INode startNode, IEdge endEdge, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
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
                result = IsBoundedReachableEdges(adjacentNode, endEdge, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv);
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
                    result = IsBoundedReachableEdges(adjacentNode, endEdge, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv);
                    if(result == true)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesOutgoing(startNode, endEdge, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth);
            return result;
        }

        public static bool IsBoundedReachableEdgesOutgoing(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesOutgoing(startNode, endEdge, depth, outgoingEdgeType, targetNodeType, graph, targetNodesToMinDepth, actionEnv);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via outgoing edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableEdgesOutgoing(INode startNode, IEdge endEdge, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleOutgoing(outgoingEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(targetNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableEdgesOutgoing(adjacentNode, endEdge, depth - 1, outgoingEdgeType, targetNodeType, graph, adjacentNodesToMinDepth);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsBoundedReachableEdgesOutgoing(INode startNode, IEdge endEdge, int depth, EdgeType outgoingEdgeType, NodeType targetNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
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
                result = IsBoundedReachableEdgesOutgoing(adjacentNode, endEdge, depth - 1, outgoingEdgeType, targetNodeType, graph, adjacentNodesToMinDepth, actionEnv);
                if(result == true)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        public static bool IsBoundedReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesIncoming(startNode, endEdge, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth);
            return result;
        }

        public static bool IsBoundedReachableEdgesIncoming(IGraph graph, INode startNode, IEdge endEdge, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            bool result = IsBoundedReachableEdgesIncoming(startNode, endEdge, depth, incomingEdgeType, sourceNodeType, graph, sourceNodesToMinDepth, actionEnv);
            return result;
        }

        /// <summary>
        /// Returns whether the end edge is reachable from the start node within the given depth, via incoming edges, under the type constraints given
        /// </summary>
        private static bool IsBoundedReachableEdgesIncoming(INode startNode, IEdge endEdge, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                return false;

            bool result = false;

            foreach(IEdge edge in startNode.GetCompatibleIncoming(incomingEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(sourceNodeType))
                    continue;
                if(edge == endEdge)
                    return true;
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                result = IsBoundedReachableEdgesIncoming(adjacentNode, endEdge, depth - 1, incomingEdgeType, sourceNodeType, graph, adjacentNodesToMinDepth);
                if(result == true)
                    break;
            }

            return result;
        }

        private static bool IsBoundedReachableEdgesIncoming(INode startNode, IEdge endEdge, int depth, EdgeType incomingEdgeType, NodeType sourceNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
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
                result = IsBoundedReachableEdgesIncoming(adjacentNode, endEdge, depth - 1, incomingEdgeType, sourceNodeType, graph, adjacentNodesToMinDepth, actionEnv);
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
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(nodeSet.Count);
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
        /// Returns the edge induced/defined subgraph of the given edge set of unknown direction
        /// </summary>
        public static IGraph DefinedSubgraph(IDictionary edgeSet, IGraph graph)
        {
            if(edgeSet is IDictionary<IDEdge, SetValueType>)
                return DefinedSubgraphDirected((IDictionary<IDEdge, SetValueType>)edgeSet, graph);
            else if(edgeSet is IDictionary<IUEdge, SetValueType>)
                return DefinedSubgraphUndirected((IDictionary<IUEdge, SetValueType>)edgeSet, graph);
            else
                return DefinedSubgraph((IDictionary<IEdge, SetValueType>)edgeSet, graph);
        }

        /// <summary>
        /// Returns the edge induced/defined subgraph of the given edge set
        /// </summary>
        public static IGraph DefinedSubgraph(IDictionary<IEdge, SetValueType> edgeSet, IGraph graph)
        {
            IGraph definedGraph = graph.CreateEmptyEquivalent("defined_from_" + graph.Name);
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count*2);
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

        /// <summary>
        /// Returns the edge induced/defined subgraph of the given directed edge set
        /// </summary>
        public static IGraph DefinedSubgraphDirected(IDictionary<IDEdge, SetValueType> edgeSet, IGraph graph)
        {
            IGraph definedGraph = graph.CreateEmptyEquivalent("defined_from_" + graph.Name);
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count * 2);
            foreach(KeyValuePair<IDEdge, SetValueType> edgeEntry in edgeSet)
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

            foreach(KeyValuePair<IDEdge, SetValueType> edgeEntry in edgeSet)
            {
                IEdge edge = edgeEntry.Key;
                IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                definedGraph.AddEdge(clone);
            }
            //graph.Check();
            //definedGraph.Check();

            return definedGraph;
        }

        /// <summary>
        /// Returns the edge induced/defined subgraph of the given undirected edge set
        /// </summary>
        public static IGraph DefinedSubgraphUndirected(IDictionary<IUEdge, SetValueType> edgeSet, IGraph graph)
        {
            IGraph definedGraph = graph.CreateEmptyEquivalent("defined_from_" + graph.Name);
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count * 2);
            foreach(KeyValuePair<IUEdge, SetValueType> edgeEntry in edgeSet)
            {
                IUEdge edge = edgeEntry.Key;
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

            foreach(KeyValuePair<IUEdge, SetValueType> edgeEntry in edgeSet)
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
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(nodeSet.Count+1);
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
        public static IEdge InsertDefined(IDictionary edgeSet, IEdge rootEdge, IGraph graph)
        {
            if(edgeSet is Dictionary<IDEdge, SetValueType>)
                return InsertDefinedDirected((IDictionary<IDEdge, SetValueType>)edgeSet, (IDEdge)rootEdge, graph);
            else if(edgeSet is Dictionary<IUEdge, SetValueType>)
                return InsertDefinedUndirected((IDictionary<IUEdge, SetValueType>)edgeSet, (IUEdge)rootEdge, graph);
            else
                return InsertDefined((IDictionary<IEdge, SetValueType>)edgeSet, rootEdge, graph);
        }

        /// <summary>
        /// Inserts a copy of the edge induced/defined subgraph of the given edge set to the graph
        /// returns the copy of the dedicated root edge
        /// the root edge is processed as if it was in the given edge set even if it isn't
        /// </summary>
        public static IEdge InsertDefined(IDictionary<IEdge, SetValueType> edgeSet, IEdge rootEdge, IGraph graph)
        {
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count*2 + 1);
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

        /// <summary>
        /// Inserts a copy of the edge induced/defined subgraph of the given directed edge set to the graph
        /// returns the copy of the dedicated directed root edge
        /// the root edge is processed as if it was in the given edge set even if it isn't
        /// </summary>
        public static IDEdge InsertDefinedDirected(IDictionary<IDEdge, SetValueType> edgeSet, IDEdge rootEdge, IGraph graph)
        {
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count * 2 + 1);
            foreach(KeyValuePair<IDEdge, SetValueType> edgeEntry in edgeSet)
            {
                IDEdge edge = edgeEntry.Key;
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

            IDEdge clonedEdge = null;
            if(edgeSet.ContainsKey(rootEdge))
            {
                foreach(KeyValuePair<IDEdge, SetValueType> edgeEntry in edgeSet)
                {
                    IEdge edge = edgeEntry.Key;
                    IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                    graph.AddEdge(clone);
                    if(edge == rootEdge)
                        clonedEdge = (IDEdge)clone;
                }
            }
            else
            {
                foreach(KeyValuePair<IDEdge, SetValueType> edgeEntry in edgeSet)
                {
                    IEdge edge = edgeEntry.Key;
                    IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                    graph.AddEdge(clone);
                }

                IEdge rootClone = rootEdge.Clone(nodeToCloned[rootEdge.Source], nodeToCloned[rootEdge.Target]);
                graph.AddEdge(rootClone);
                clonedEdge = (IDEdge)rootClone;
            }
            //graph.Check();

            return clonedEdge;
        }

        /// <summary>
        /// Inserts a copy of the edge induced/defined subgraph of the given undirected edge set to the graph
        /// returns the copy of the dedicated undirected root edge
        /// the root edge is processed as if it was in the given edge set even if it isn't
        /// </summary>
        public static IUEdge InsertDefinedUndirected(IDictionary<IUEdge, SetValueType> edgeSet, IUEdge rootEdge, IGraph graph)
        {
            Dictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(edgeSet.Count * 2 + 1);
            foreach(KeyValuePair<IUEdge, SetValueType> edgeEntry in edgeSet)
            {
                IUEdge edge = edgeEntry.Key;
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

            IUEdge clonedEdge = null;
            if(edgeSet.ContainsKey(rootEdge))
            {
                foreach(KeyValuePair<IUEdge, SetValueType> edgeEntry in edgeSet)
                {
                    IEdge edge = edgeEntry.Key;
                    IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                    graph.AddEdge(clone);
                    if(edge == rootEdge)
                        clonedEdge = (IUEdge)clone;
                }
            }
            else
            {
                foreach(KeyValuePair<IUEdge, SetValueType> edgeEntry in edgeSet)
                {
                    IEdge edge = edgeEntry.Key;
                    IEdge clone = edge.Clone(nodeToCloned[edge.Source], nodeToCloned[edge.Target]);
                    graph.AddEdge(clone);
                }

                IEdge rootClone = rootEdge.Clone(nodeToCloned[rootEdge.Source], nodeToCloned[rootEdge.Target]);
                graph.AddEdge(rootClone);
                clonedEdge = (IUEdge)rootClone;
            }
            //graph.Check();

            return clonedEdge;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Copies the given graph, returns the copy
        /// </summary>
        public static IGraph Copy(IGraph sourceGraph)
        {
            return sourceGraph.Clone(sourceGraph.Name);
        }

        /*private static INode CopyNamed(INamedGraph sourceGraph, INamedGraph targetGraph, INode rootNode)
        {
            Dictionary<INode, INode> originalToCopy = new Dictionary<INode, INode>();
            foreach(INode node in sourceGraph.Nodes)
            {
                INode copy = node.Clone();
                originalToCopy.Add(node, copy);
                targetGraph.AddNode(copy, sourceGraph.GetElementName(node));
            }
            foreach(IEdge edge in sourceGraph.Edges)
            {
                IEdge copy = edge.Clone(originalToCopy[edge.Source], originalToCopy[edge.Target]);
                targetGraph.AddEdge(copy, sourceGraph.GetElementName(edge));
            }
            return rootNode != null ? originalToCopy[rootNode] : null;
        }*/

        private static INode CopyUnnamed(IGraph sourceGraph, IGraph targetGraph, INode rootNode)
        {
            Dictionary<INode, INode> originalToCopy = new Dictionary<INode, INode>();
            foreach(INode node in sourceGraph.Nodes)
            {
                INode copy = node.Clone();
                originalToCopy.Add(node, copy);
                targetGraph.AddNode(copy);
            }
            foreach(IEdge edge in sourceGraph.Edges)
            {
                IEdge copy = edge.Clone(originalToCopy[edge.Source], originalToCopy[edge.Target]);
                targetGraph.AddEdge(copy);
            }
            return rootNode != null ? originalToCopy[rootNode] : null;
        }

        /// <summary>
        /// Inserts a copy of the given subgraph to the graph (disjoint union).
        /// Returns the copy of the dedicated root node.
        /// </summary>
        public static INode InsertCopy(IGraph sourceGraph, INode rootNode, IGraph targetGraph)
        {
            return CopyUnnamed(sourceGraph, targetGraph, rootNode);
        }

        /// <summary>
        /// Inserts the given subgraph to the graph, destroying the source (destructive disjoint union).
        /// The elements keep their identity (though not their name).
        /// </summary>
        public static void Insert(IGraph sourceGraph, IGraph targetGraph)
        {
            sourceGraph.ReuseOptimization = false; // we want to own the elements we remove from the source

            List<IEdge> edges = new List<IEdge>(sourceGraph.NumEdges);
            foreach(IEdge edge in sourceGraph.Edges)
            {
                edges.Add(edge);
            }
            foreach(IEdge edge in edges)
            {
                sourceGraph.Remove(edge);
            }

            List<INode> nodes = new List<INode>(sourceGraph.NumNodes);
            foreach(INode node in sourceGraph.Nodes)
            {
                nodes.Add(node);
            }
            foreach(INode node in nodes)
            {
                sourceGraph.Remove(node);
                targetGraph.AddNode(node);
            }

            foreach(IEdge edge in edges)
            {
                targetGraph.AddEdge(edge); // edge still has source and target set, and they are the same in the new graph
            }

            sourceGraph.Name = "ZOMBIE GRAPH - DON'T USE - LET HIM DIE!";
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the name of the given entity (which might be a node, an edge, or a graph).
        /// If the entity is null, the name of the graph is returned.
        /// </summary>
        public static string Nameof(object entity, IGraph graph)
        {
            if(entity is IGraphElement)
                return ((INamedGraph)graph).GetElementName((IGraphElement)entity);
            else if(entity is IGraph)
                return ((IGraph)entity).Name;
            else
                return graph.Name;
        }

        /// <summary>
        /// Returns the unique if of the given entity (which might be a node, an edge, or a graph).
        /// If the entity is null, the unique id of the graph is returned.
        /// </summary>
        public static long Uniqueof(object entity, IGraph graph)
        {
            if(entity is IGraphElement)
                return ((IGraphElement)entity).GetUniqueId();
            else if(entity is IObject)
                return ((IObject)entity).GetUniqueId();
            else if(entity is IGraph)
                return ((IGraph)entity).GraphId;
            else
                return graph.GraphId;
        }

        /// <summary>
        /// Imports and returns the graph within the file specified by its path (model as specified).
        /// </summary>
        public static IGraph Import(object path, IBackend backend, IGraphModel model)
        {
            IActions actions;
            return Porter.Import((string)path, backend, model, out actions);
        }

        /// <summary>
        /// Exports the graph to the file specified by its path.
        /// </summary>
        public static void Export(object path, IGraph graph)
        {
            List<string> arguments = new List<string>();
            arguments.Add(path.ToString());
            if(graph is INamedGraph)
                Porter.Export((INamedGraph)graph, arguments);
            else
                Porter.Export(graph, arguments);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// creates a deep copy of the given node and adds it to the graph, returns it
        /// </summary>
        public static INode AddCopyOfNode(object node, IGraph graph)
        {
            IDictionary<object, object> oldToNewObjectMap = new Dictionary<object, object>();
            INode copy = ((INode)node).Copy(graph, oldToNewObjectMap);
            graph.AddNode(copy);
            return copy;
        }

        /// <summary>
        /// creates a deep copy of the given edge and adds it to the graph between from and to, returns it
        /// </summary>
        public static IEdge AddCopyOfEdge(object edge, INode src, INode tgt, IGraph graph)
        {
            IDictionary<object, object> oldToNewObjectMap = new Dictionary<object, object>();
            IEdge copy = ((IEdge)edge).Copy(src, tgt, graph, oldToNewObjectMap);
            graph.AddEdge(copy);
            return copy;
        }

        /// <summary>
        /// creates a shallow clone of the given node and adds it to the graph, returns it
        /// </summary>
        public static INode AddCloneOfNode(object node, IGraph graph)
        {
            INode copy = ((INode)node).Clone();
            graph.AddNode(copy);
            return copy;
        }

        /// <summary>
        /// creates a shallow clone of the given edge and adds it to the graph between from and to, returns it
        /// </summary>
        public static IEdge AddCloneOfEdge(object edge, INode src, INode tgt, IGraph graph)
        {
            IEdge copy = ((IEdge)edge).Clone(src, tgt);
            graph.AddEdge(copy);
            return copy;
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

        /// <summary>
        /// retypes a node to the given type, returns it
        /// type might be a string denoting a NodeType or a NodeType
        /// </summary>
        public static INode RetypeNode(INode node, object type, IGraph graph)
        {
            return graph.Retype(node, type is string ? graph.Model.NodeModel.GetType((string)type) : (NodeType)type);
        }

        /// <summary>
        /// retypes an edge to the given type, returns it
        /// type might be a string denoting an EdgeType or an EdgeType
        /// </summary>
        public static IEdge RetypeEdge(IEdge edge, object type, IGraph graph)
        {
            return graph.Retype(edge, type is string ? graph.Model.EdgeModel.GetType((string)type) : (EdgeType)type);
        }

        /// <summary>
        /// retypes a graph element to the given type, returns it
        /// type might be a string denoting a NodeType or EdgeType, or a NodeType or EdgeType
        /// </summary>
        public static IGraphElement RetypeGraphElement(IGraphElement elem, object type, IGraph graph)
        {
            if(elem is INode)
                return RetypeNode((INode)elem, type, graph);
            else
                return RetypeEdge((IEdge)elem, type, graph);
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
                {
                    yield return node;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        public static IEnumerable<INode> Reachable(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(INode node in ReachableRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return node;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
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
                {
                    yield return node;
                }
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
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> ReachableRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;

                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return node;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;

                foreach(INode node in ReachableRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return node;
                }
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
                {
                    yield return node;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        public static IEnumerable<INode> ReachableIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(INode node in ReachableIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return node;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
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
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> ReachableIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;

                foreach(INode node in ReachableIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return node;
                }
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
                {
                    yield return node;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }


        public static IEnumerable<INode> ReachableOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                foreach(INode node in ReachableOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return node;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
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
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> ReachableOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                yield return adjacentNode;

                foreach(INode node in ReachableOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return node;
                }
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
                graph.SetVisited(startNode, flag, true);
                visitedNodes.Add(startNode);
                foreach(IEdge edge in ReachableEdgesRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        public static IEnumerable<IEdge> ReachableEdges(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                graph.SetVisited(startNode, flag, true);
                visitedNodes.Add(startNode);
                foreach(IEdge edge in ReachableEdgesRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
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

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return reachableEdge;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return reachableEdge;
                }
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
                graph.SetVisited(startNode, flag, true);
                visitedNodes.Add(startNode);
                foreach(IEdge edge in ReachableEdgesIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        public static IEnumerable<IEdge> ReachableEdgesIncoming(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                graph.SetVisited(startNode, flag, true);
                visitedNodes.Add(startNode);
                foreach(IEdge edge in ReachableEdgesIncomingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
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
                {
                    yield return reachableEdge;
                }
            }
        }


        private static IEnumerable<IEdge> ReachableEdgesIncomingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesIncomingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return reachableEdge;
                }
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
                graph.SetVisited(startNode, flag, true);
                visitedNodes.Add(startNode);
                foreach(IEdge edge in ReachableEdgesOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }


        public static IEnumerable<IEdge> ReachableEdgesOutgoing(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<INode> visitedNodes = null;
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedNodes = new List<INode>((int)Math.Sqrt(graph.NumNodes));
                graph.SetVisited(startNode, flag, true);
                visitedNodes.Add(startNode);
                foreach(IEdge edge in ReachableEdgesOutgoingRec(startNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedNodes.Count; ++i)
                {
                    graph.SetVisited(visitedNodes[i], flag, false);
                }
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
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> ReachableEdgesOutgoingRec(INode startNode, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<INode> visitedNodes, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                yield return edge;

                if(graph.IsVisited(adjacentNode, flag))
                    continue;
                graph.SetVisited(adjacentNode, flag, true);
                visitedNodes.Add(adjacentNode);
                foreach(IEdge reachableEdge in ReachableEdgesOutgoingRec(adjacentNode, incidentEdgeType, adjacentNodeType, graph, flag, visitedNodes, actionEnv))
                {
                    yield return reachableEdge;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<INode> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth))
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> BoundedReachable(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv))
            {
                yield return node;
            }
        }

        private static IEnumerable<INode> BoundedReachableRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth))
                {
                    yield return node;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth))
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> BoundedReachableRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv))
                {
                    yield return node;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, adjacentNodesToMinDepth, actionEnv))
                {
                    yield return node;
                }
            }
        }

        public static IEnumerable<INode> BoundedReachableIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, sourceNodesToMinDepth))
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> BoundedReachableIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, sourceNodesToMinDepth, actionEnv))
            {
                yield return node;
            }
        }

        private static IEnumerable<INode> BoundedReachableIncomingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> sourceNodesToMinDepth)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                sourceNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableIncomingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, sourceNodesToMinDepth))
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> BoundedReachableIncomingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> sourceNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                sourceNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableIncomingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, sourceNodesToMinDepth, actionEnv))
                {
                    yield return node;
                }
            }
        }

        public static IEnumerable<INode> BoundedReachableOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, targetNodesToMinDepth))
            {
                yield return node;
            }
        }

        public static IEnumerable<INode> BoundedReachableOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            foreach(INode node in BoundedReachableOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, targetNodesToMinDepth, actionEnv))
            {
                yield return node;
            }
        }

        private static IEnumerable<INode> BoundedReachableOutgoingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> targetNodesToMinDepth)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                targetNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableOutgoingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, targetNodesToMinDepth))
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> BoundedReachableOutgoingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, Dictionary<INode, int> targetNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                int nodeDepth;
                if(!targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth))
                    yield return adjacentNode;
                if(targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                targetNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(INode node in BoundedReachableOutgoingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, targetNodesToMinDepth, actionEnv))
                {
                    yield return node;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static IEnumerable<IEdge> BoundedReachableEdges(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<IEdge> visitedEdges = null;
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedEdges = new List<IEdge>((int)Math.Sqrt(graph.NumEdges));
                foreach(IEdge edge in BoundedReachableEdgesRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, adjacentNodesToMinDepth))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedEdges.Count; ++i)
                {
                    graph.SetVisited(visitedEdges[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdges(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<IEdge> visitedEdges = null;
            Dictionary<INode, int> adjacentNodesToMinDepth = new Dictionary<INode, int>();
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedEdges = new List<IEdge>((int)Math.Sqrt(graph.NumEdges));
                foreach(IEdge edge in BoundedReachableEdgesRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, adjacentNodesToMinDepth, actionEnv))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedEdges.Count; ++i)
                {
                    graph.SetVisited(visitedEdges[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<IEdge> visitedEdges, Dictionary<INode, int> adjacentNodesToMinDepth)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, adjacentNodesToMinDepth))
                {
                    yield return reachableEdge;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, adjacentNodesToMinDepth))
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<IEdge> visitedEdges, Dictionary<INode, int> adjacentNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, adjacentNodesToMinDepth, actionEnv))
                {
                    yield return reachableEdge;
                }
            }
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(adjacentNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                adjacentNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, adjacentNodesToMinDepth, actionEnv))
                {
                    yield return reachableEdge;
                }
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<IEdge> visitedEdges = null;
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedEdges = new List<IEdge>((int)Math.Sqrt(graph.NumEdges));
                foreach(IEdge edge in BoundedReachableEdgesIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, sourceNodesToMinDepth))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedEdges.Count; ++i)
                {
                    graph.SetVisited(visitedEdges[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesIncoming(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<IEdge> visitedEdges = null;
            Dictionary<INode, int> sourceNodesToMinDepth = new Dictionary<INode, int>();
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedEdges = new List<IEdge>((int)Math.Sqrt(graph.NumEdges));
                foreach(IEdge edge in BoundedReachableEdgesIncomingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, sourceNodesToMinDepth, actionEnv))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedEdges.Count; ++i)
                {
                    graph.SetVisited(visitedEdges[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesIncomingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<IEdge> visitedEdges, Dictionary<INode, int> sourceNodesToMinDepth)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                sourceNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesIncomingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, sourceNodesToMinDepth))
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesIncomingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<IEdge> visitedEdges, Dictionary<INode, int> sourceNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleIncoming(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(sourceNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                sourceNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesIncomingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, sourceNodesToMinDepth, actionEnv))
                {
                    yield return reachableEdge;
                }
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph)
        {
            int flag = -1;
            List<IEdge> visitedEdges = null;
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedEdges = new List<IEdge>((int)Math.Sqrt(graph.NumEdges));
                foreach(IEdge edge in BoundedReachableEdgesOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, targetNodesToMinDepth))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedEdges.Count; ++i)
                {
                    graph.SetVisited(visitedEdges[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        public static IEnumerable<IEdge> BoundedReachableEdgesOutgoing(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            int flag = -1;
            List<IEdge> visitedEdges = null;
            Dictionary<INode, int> targetNodesToMinDepth = new Dictionary<INode, int>();
            try
            {
                flag = graph.AllocateVisitedFlag();
                visitedEdges = new List<IEdge>((int)Math.Sqrt(graph.NumEdges));
                foreach(IEdge edge in BoundedReachableEdgesOutgoingRec(startNode, depth, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, targetNodesToMinDepth, actionEnv))
                {
                    yield return edge;
                }
            }
            finally
            {
                for(int i = 0; i < visitedEdges.Count; ++i)
                {
                    graph.SetVisited(visitedEdges[i], flag, false);
                }
                graph.FreeVisitedFlagNonReset(flag);
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesOutgoingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<IEdge> visitedEdges, Dictionary<INode, int> targetNodesToMinDepth)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                targetNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesOutgoingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, targetNodesToMinDepth))
                {
                    yield return reachableEdge;
                }
            }
        }

        private static IEnumerable<IEdge> BoundedReachableEdgesOutgoingRec(INode startNode, int depth, EdgeType incidentEdgeType, NodeType adjacentNodeType, IGraph graph, int flag, List<IEdge> visitedEdges, Dictionary<INode, int> targetNodesToMinDepth, IActionExecutionEnvironment actionEnv)
        {
            if(depth <= 0)
                yield break;
            foreach(IEdge edge in startNode.GetCompatibleOutgoing(incidentEdgeType))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                if(!graph.IsVisited(edge, flag))
                {
                    graph.SetVisited(edge, flag, true);
                    visitedEdges.Add(edge);
                    yield return edge;
                }
                int nodeDepth;
                if(targetNodesToMinDepth.TryGetValue(adjacentNode, out nodeDepth) && nodeDepth >= depth - 1)
                    continue;
                targetNodesToMinDepth[adjacentNode] = depth - 1;
                foreach(IEdge reachableEdge in BoundedReachableEdgesOutgoingRec(adjacentNode, depth - 1, incidentEdgeType, adjacentNodeType, graph, flag, visitedEdges, targetNodesToMinDepth, actionEnv))
                {
                    yield return reachableEdge;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////
        
        public static bool Equal(IGraph this_, IGraph that)
        {
            if(this_ == null && that == null)
                return true;
            if(this_ == null || that == null)
                return false;
            return this_.IsIsomorph(that);
        }

        public static bool HasSameStructure(IGraph this_, IGraph that)
        {
            if(this_ == null && that == null)
                return true;
            if(this_ == null || that == null)
                return false;
            return this_.HasSameStructure(that);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the candidate (sub)graph equals any of the graphs in the given set
        /// </summary>
        public static bool EqualsAny(IGraph candidate, IDictionary<IGraph, SetValueType> graphsToCheckAgainst, bool includingAttributes)
        {
            if(candidate == null)
                return false;
            if(graphsToCheckAgainst == null)
                return false;

            // we're called from a non-parallel matcher, use parallel version (if available) of comparison
            if(includingAttributes)
                return candidate.IsIsomorph(graphsToCheckAgainst);
            else
                return candidate.HasSameStructure(graphsToCheckAgainst);
        }

        /// <summary>
        /// Returns one graph from the given set of graphs that is equivalent to the candidate (sub)graph 
        /// </summary>
        public static IGraph GetEquivalent(IGraph candidate, IDictionary<IGraph, SetValueType> graphsToCheckAgainst, bool includingAttributes)
        {
            if(candidate == null)
                return null;
            if(graphsToCheckAgainst == null)
                return null;

            // we're called from a non-parallel matcher, use parallel version (if available) of comparison
            if(includingAttributes)
                return candidate.GetIsomorph(graphsToCheckAgainst);
            else
                return candidate.GetSameStructure(graphsToCheckAgainst);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns one graph from the given array of graphs that is equivalent to the candidate (sub)graph, 
        /// or null if none exists, in that case the graph is added to the array of graphs.
        /// This method is (intended to be) thread-safe (it locks the array as needed, still allowing for a large amount of parallel equivalence checking if called by multiple threads).
        /// </summary>
        public static IGraph GetEquivalentOrAdd(IGraph candidate, IList<IGraph> graphsToCheckAgainst, bool includingAttributes)
        {
            if(candidate == null)
                return null;
            if(graphsToCheckAgainst == null)
                return null;

            int countOfGraphsToCheckAgainst = graphsToCheckAgainst.Count;
            for(int i=0; i < countOfGraphsToCheckAgainst; ++i)
            {
                IGraph graphToCheckAgainst = graphsToCheckAgainst[i];

                if(includingAttributes)
                {
                    if(candidate.IsIsomorph(graphToCheckAgainst))
                        return graphToCheckAgainst;
                }
                else
                {
                    if(candidate.HasSameStructure(graphToCheckAgainst))
                        return graphToCheckAgainst;
                }
            }

            lock(graphsToCheckAgainst)
            {
                // count may have incread concurrently, now check the added graphs not checked yet
                for(int i = countOfGraphsToCheckAgainst; i < graphsToCheckAgainst.Count; ++i)
                {
                    IGraph graphToCheckAgainst = graphsToCheckAgainst[i];

                    if(includingAttributes)
                    {
                        if(candidate.IsIsomorph(graphToCheckAgainst))
                            return graphToCheckAgainst;
                    }
                    else
                    {
                        if(candidate.HasSameStructure(graphToCheckAgainst))
                            return graphToCheckAgainst;
                    }
                }

                graphsToCheckAgainst.Add(candidate);

                // ensure that all changes are visible to other threads (outside lock), this esp. includes the case that the internal array was replaced because its capacity was exceeded
                System.Threading.Thread.MemoryBarrier();
            }

            return null;
        }
    }
}
