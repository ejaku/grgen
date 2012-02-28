/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A partial implementation of the IGraph interface.
    /// Adding some methods implemented over the IGraph interface (some convenience stuff).
    /// </summary>
    public abstract class BaseGraph : IGraph
    {
        public abstract String Name { get; }
        public abstract IGraphModel Model { get; }
        public abstract bool ReuseOptimization { get; set; }

        public int NumNodes
        {
            get { return GetNumCompatibleNodes(Model.NodeModel.RootType); }
        }
        
        public int NumEdges
        {
            get { return GetNumCompatibleEdges(Model.EdgeModel.RootType); }
        }
        
        public IEnumerable<INode> Nodes
        {
            get { return GetCompatibleNodes(Model.NodeModel.RootType); }
        }
        
        public IEnumerable<IEdge> Edges
        {
            get { return GetCompatibleEdges(Model.EdgeModel.RootType); }
        }

        public abstract int GetNumExactNodes(NodeType nodeType);
        public abstract int GetNumExactEdges(EdgeType edgeType);
        public abstract IEnumerable<INode> GetExactNodes(NodeType nodeType);
        public abstract IEnumerable<IEdge> GetExactEdges(EdgeType edgeType);

        public abstract int GetNumCompatibleNodes(NodeType nodeType);
        public abstract int GetNumCompatibleEdges(EdgeType edgeType);
        public abstract IEnumerable<INode> GetCompatibleNodes(NodeType nodeType);
        public abstract IEnumerable<IEdge> GetCompatibleEdges(EdgeType edgeType);

        public abstract void AddNode(INode node);
        public abstract INode AddNode(NodeType nodeType);

        public abstract void AddEdge(IEdge edge);
        public abstract IEdge AddEdge(EdgeType edgeType, INode source, INode target);

        public abstract void Remove(INode node);
        public abstract void Remove(IEdge edge);
        public abstract void RemoveEdges(INode node);

        public abstract void Clear();

        public abstract INode Retype(INode node, NodeType newNodeType);
        public abstract IEdge Retype(IEdge edge, EdgeType newEdgeType);

        public abstract void Merge(INode target, INode source, string sourceName);

        public abstract void RedirectSource(IEdge edge, INode newSource, string oldSourceName);
        public abstract void RedirectTarget(IEdge edge, INode newTarget, string oldTargetName);
        public abstract void RedirectSourceAndTarget(IEdge edge, INode newSource, INode newTarget, string oldSourceName, string oldTargetName);

        public abstract void Custom(params object[] args);

        public abstract IGraph Clone(String newName);
        public abstract IGraph CreateEmptyEquivalent(String newName);

        public abstract bool IsIsomorph(IGraph that);
        public abstract bool HasSameStructure(IGraph that);

        public bool Validate(ValidationMode mode, out List<ConnectionAssertionError> errors)
        {
            return GraphValidator.Validate(this, mode, out errors);
        }

        public abstract void Check();

        public abstract int AllocateVisitedFlag();
        public abstract void FreeVisitedFlag(int visitorID);
        public abstract void ResetVisitedFlag(int visitorID);
        public abstract void SetVisited(IGraphElement elem, int visitorID, bool visited);
        public abstract bool IsVisited(IGraphElement elem, int visitorID);


        #region Events

        public event NodeAddedHandler OnNodeAdded;
        public event EdgeAddedHandler OnEdgeAdded;

        public event RemovingNodeHandler OnRemovingNode;
        public event RemovingEdgeHandler OnRemovingEdge;
        public event RemovingEdgesHandler OnRemovingEdges;

        public event ClearingGraphHandler OnClearingGraph;

        public event ChangingNodeAttributeHandler OnChangingNodeAttribute;
        public event ChangingEdgeAttributeHandler OnChangingEdgeAttribute;

        public event RetypingNodeHandler OnRetypingNode;
        public event RetypingEdgeHandler OnRetypingEdge;

        public event RedirectingEdgeHandler OnRedirectingEdge;

        public event SettingAddedElementNamesHandler OnSettingAddedNodeNames;
        public event SettingAddedElementNamesHandler OnSettingAddedEdgeNames;

        /// <summary>
        /// Fires an OnNodeAdded event.
        /// </summary>
        /// <param name="node">The added node.</param>
        public void NodeAdded(INode node)
        {
            NodeAddedHandler nodeAdded = OnNodeAdded;
            if(nodeAdded != null) nodeAdded(node);
        }

        /// <summary>
        /// Fires an OnEdgeAdded event.
        /// </summary>
        /// <param name="edge">The added edge.</param>
        public void EdgeAdded(IEdge edge)
        {
            EdgeAddedHandler edgeAdded = OnEdgeAdded;
            if(edgeAdded != null) edgeAdded(edge);
        }

        /// <summary>
        /// Fires an OnRemovingNode event.
        /// </summary>
        /// <param name="node">The node to be removed.</param>
        public void RemovingNode(INode node)
        {
            RemovingNodeHandler removingNode = OnRemovingNode;
            if(removingNode != null) removingNode(node);
        }

        /// <summary>
        /// Fires an OnRemovingEdge event.
        /// </summary>
        /// <param name="edge">The edge to be removed.</param>
        public void RemovingEdge(IEdge edge)
        {
            RemovingEdgeHandler removingEdge = OnRemovingEdge;
            if(removingEdge != null) removingEdge(edge);
        }

        /// <summary>
        /// Fires an OnRemovingEdges event.
        /// </summary>
        /// <param name="node">The node whose edges are to be removed.</param>
        public void RemovingEdges(INode node)
        {
            RemovingEdgesHandler removingEdges = OnRemovingEdges;
            if(removingEdges != null) removingEdges(node);
        }

        /// <summary>
        /// Fires an OnClearingGraph event.
        /// </summary>
        public void ClearingGraph()
        {
            ClearingGraphHandler clearingGraph = OnClearingGraph;
            if(clearingGraph != null) clearingGraph();
        }

        public void ChangingNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            ChangingNodeAttributeHandler changingElemAttr = OnChangingNodeAttribute;
            if(changingElemAttr != null)
                changingElemAttr(node, attrType, changeType, newValue, keyValue);
        }

        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            ChangingEdgeAttributeHandler changingElemAttr = OnChangingEdgeAttribute;
            if(changingElemAttr != null)
                changingElemAttr(edge, attrType, changeType, newValue, keyValue);
        }

        /// <summary>
        /// Fires an OnRetypingNode event.
        /// </summary>
        /// <param name="oldNode">The node to be retyped.</param>
        /// <param name="newNode">The new node with the common attributes, but without any incident edges assigned, yet.</param>
        public void RetypingNode(INode oldNode, INode newNode)
        {
            RetypingNodeHandler retypingNode = OnRetypingNode;
            if(retypingNode != null) retypingNode(oldNode, newNode);
        }

        /// <summary>
        /// Fires an OnRetypingEdge event.
        /// </summary>
        /// <param name="oldEdge">The edge to be retyped.</param>
        /// <param name="newEdge">The new edge with the common attributes, but not fully connected with the incident nodes, yet.</param>
        public void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            RetypingEdgeHandler retypingEdge = OnRetypingEdge;
            if(retypingEdge != null) retypingEdge(oldEdge, newEdge);
        }

        /// <summary>
        /// Fires an OnRedirectingEdge event.
        /// </summary>
        /// <param name="oldEdge">The edge to be retyped.</param>
        /// <param name="newEdge">The new edge with the common attributes, but not fully connected with the incident nodes, yet.</param>
        public void RedirectingEdge(IEdge edge)
        {
            RedirectingEdgeHandler redirectingEdge = OnRedirectingEdge;
            if(redirectingEdge != null) redirectingEdge(edge);
        }

        public void SettingAddedNodeNames(String[] addedNodeNames)
        {
            SettingAddedElementNamesHandler handler = OnSettingAddedNodeNames;
            if(handler != null) handler(addedNodeNames);
        }

        public void SettingAddedEdgeNames(String[] addedEdgeNames)
        {
            SettingAddedElementNamesHandler handler = OnSettingAddedEdgeNames;
            if(handler != null) handler(addedEdgeNames);
        }

        #endregion Events


        /// <summary>
        /// Returns the node type with the given name.
        /// </summary>
        /// <param name="typeName">The name of a node type.</param>
        /// <returns>The node type with the given name or null, if it does not exist.</returns>
        public NodeType GetNodeType(String typeName)
        {
            return Model.NodeModel.GetType(typeName);
        }

        /// <summary>
        /// Returns the edge type with the given name.
        /// </summary>
        /// <param name="typeName">The name of a edge type.</param>
        /// <returns>The edge type with the given name or null, if it does not exist.</returns>
        public EdgeType GetEdgeType(String typeName)
        {
            return Model.EdgeModel.GetType(typeName);
        }
    }
}
