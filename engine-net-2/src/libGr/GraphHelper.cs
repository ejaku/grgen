/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
        public static IDictionary<INode, SetValueType> Adjacent(INode sourceNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode,SetValueType>();
            foreach(IEdge edge in sourceNode.Outgoing)
            {
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet.Add(adjacentNode, null);
            }
            foreach(IEdge edge in sourceNode.Incoming)
            {
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet.Add(adjacentNode, null);
            }
            return adjacentNodesSet;
        }
        
        public static IDictionary<INode, SetValueType> AdjacentOutgoing(INode sourceNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in sourceNode.Outgoing)
            {
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Target;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet.Add(adjacentNode, null);
            }
            return adjacentNodesSet;
        }

        public static IDictionary<INode, SetValueType> AdjacentIncoming(INode sourceNode, EdgeType incidentEdgeType, NodeType adjacentNodeType)
        {
            Dictionary<INode, SetValueType> adjacentNodesSet = new Dictionary<INode, SetValueType>();
            foreach(IEdge edge in sourceNode.Incoming)
            {
                if(!edge.InstanceOf(incidentEdgeType))
                    continue;
                INode adjacentNode = edge.Source;
                if(!adjacentNode.InstanceOf(adjacentNodeType))
                    continue;
                adjacentNodesSet.Add(adjacentNode, null);
            }
            return adjacentNodesSet;
        }

        public static IGraph Induced(IDictionary<INode, SetValueType> nodeSet, IGraph graph)
        {
            IGraph inducedGraph = graph.CreateEmptyEquivalent("induced_from_" + graph.Name);
            IDictionary<INode, INode> nodeToCloned = new Dictionary<INode, INode>(nodeSet.Count);
            foreach(INode node in nodeSet.Keys)
            {
                INode clone = node.Clone();
                nodeToCloned.Add(node, clone);
                inducedGraph.AddNode(clone);
            }
            //graph.Check();
            //inducedGraph.Check();

            foreach(INode node in nodeSet.Keys)
            {
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
    }
}
