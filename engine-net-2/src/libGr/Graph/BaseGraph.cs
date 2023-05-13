/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A partial implementation of the IGraph interface.
    /// Adding some methods implemented over the IGraph interface (some convenience stuff).
    /// </summary>
    public abstract class BaseGraph : IGraph
    {
        public abstract String Name { get; set; }
        public abstract int GraphId { get; }
        public abstract IGraphModel Model { get; }
        public abstract IIndexSet Indices { get; }
        public abstract IUniquenessHandler UniquenessHandler { get; }
        public abstract IGlobalVariables GlobalVariables { get; }

        public abstract bool ReuseOptimization { get; set; }
        public abstract long ChangesCounter{ get; }

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

        public abstract IGraphElement GetGraphElement(int unique);
        public abstract INode GetNode(int unique);
        public abstract IEdge GetEdge(int unique);

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

        public abstract IDictionary<String, String> CustomCommandsAndDescriptions { get; }
        public abstract void Custom(params object[] args);

        public abstract IGraph Clone(String newName);
        public abstract IGraph Clone(String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap);
        public abstract INamedGraph CloneAndAssignNames();
        public abstract IGraph CreateEmptyEquivalent(String newName);

        public abstract bool IsIsomorph(IGraph that);
        public abstract bool IsIsomorph(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);
        public abstract IGraph GetIsomorph(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);
        public abstract bool HasSameStructure(IGraph that);
        public abstract bool HasSameStructure(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);
        public abstract IGraph GetSameStructure(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);
        public abstract string Canonize();

        public bool Validate(ValidationMode mode, out List<ConnectionAssertionError> errors)
        {
            return GraphValidator.Validate(this, mode, out errors);
        }

        public abstract void Check();

        public abstract int AllocateVisitedFlag();
        public abstract void FreeVisitedFlag(int visitorID);
        public abstract void FreeVisitedFlagNonReset(int visitorID);
        public abstract void ResetVisitedFlag(int visitorID);
        public abstract void SetVisited(IGraphElement elem, int visitorID, bool visited);
        public abstract bool IsVisited(IGraphElement elem, int visitorID);
        public abstract List<int> GetAllocatedVisitedFlags();

        public abstract void SetInternallyVisited(IGraphElement elem, bool visited);
        public abstract bool IsInternallyVisited(IGraphElement elem);
        public abstract void SetInternallyVisited(IGraphElement elem, bool visited, int threadId);
        public abstract bool IsInternallyVisited(IGraphElement elem, int threadId);


        #region Events

        public event NodeAddedHandler OnNodeAdded;
        public event EdgeAddedHandler OnEdgeAdded;
        public event ObjectCreatedHandler OnObjectCreated;

        public event RemovingNodeHandler OnRemovingNode;
        public event RemovingEdgeHandler OnRemovingEdge;
        public event RemovingEdgesHandler OnRemovingEdges;

        public event ClearingGraphHandler OnClearingGraph;

        public event ChangingNodeAttributeHandler OnChangingNodeAttribute;
        public event ChangingEdgeAttributeHandler OnChangingEdgeAttribute;
        public event ChangingObjectAttributeHandler OnChangingObjectAttribute;
        public event ChangedNodeAttributeHandler OnChangedNodeAttribute;
        public event ChangedEdgeAttributeHandler OnChangedEdgeAttribute;

        public event RetypingNodeHandler OnRetypingNode;
        public event RetypingEdgeHandler OnRetypingEdge;

        public event RedirectingEdgeHandler OnRedirectingEdge;

        public event VisitedAllocHandler OnVisitedAlloc;
        public event VisitedFreeHandler OnVisitedFree;
        public event SettingVisitedHandler OnSettingVisited;

        public event SettingAddedElementNamesHandler OnSettingAddedNodeNames;
        public event SettingAddedElementNamesHandler OnSettingAddedEdgeNames;

        /// <summary>
        /// Fires an OnNodeAdded event.
        /// </summary>
        /// <param name="node">The added node.</param>
        public void NodeAdded(INode node)
        {
            if(OnNodeAdded != null)
                OnNodeAdded(node);
        }

        /// <summary>
        /// Fires an OnEdgeAdded event.
        /// </summary>
        /// <param name="edge">The added edge.</param>
        public void EdgeAdded(IEdge edge)
        {
            if(OnEdgeAdded != null)
                OnEdgeAdded(edge);
        }

        /// <summary>
        /// Fires an OnObjectCreated event.
        /// </summary>
        /// <param name="value">The created object value.</param>
        public void ObjectCreated(IObject value)
        {
            if(OnObjectCreated != null)
                OnObjectCreated(value);
        }

        /// <summary>
        /// Fires an OnRemovingNode event.
        /// </summary>
        /// <param name="node">The node to be removed.</param>
        public void RemovingNode(INode node)
        {
            if(OnRemovingNode != null)
                OnRemovingNode(node);
        }

        /// <summary>
        /// Fires an OnRemovingEdge event.
        /// </summary>
        /// <param name="edge">The edge to be removed.</param>
        public void RemovingEdge(IEdge edge)
        {
            if(OnRemovingEdge != null)
                OnRemovingEdge(edge);
        }

        /// <summary>
        /// Fires an OnRemovingEdges event.
        /// </summary>
        /// <param name="node">The node whose edges are to be removed.</param>
        public void RemovingEdges(INode node)
        {
            if(OnRemovingEdges != null)
                OnRemovingEdges(node);
        }

        /// <summary>
        /// Fires an OnClearingGraph event.
        /// </summary>
        public void ClearingGraph()
        {
            if(OnClearingGraph != null)
                OnClearingGraph(this);
        }

        public void ChangingNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(OnChangingNodeAttribute != null)
                OnChangingNodeAttribute(node, attrType, changeType, newValue, keyValue);
        }

        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(OnChangingEdgeAttribute != null)
                OnChangingEdgeAttribute(edge, attrType, changeType, newValue, keyValue);
        }

        public void ChangingObjectAttribute(IObject obj, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue)
        {
            if(OnChangingObjectAttribute != null)
                OnChangingObjectAttribute(obj, attrType, changeType, newValue, keyValue);
        }

        public void ChangedNodeAttribute(INode node, AttributeType attrType)
        {
            if(OnChangedNodeAttribute != null)
                OnChangedNodeAttribute(node, attrType);
        }

        public void ChangedEdgeAttribute(IEdge edge, AttributeType attrType)
        {
            if(OnChangedEdgeAttribute != null)
                OnChangedEdgeAttribute(edge, attrType);
        }

        /// <summary>
        /// Fires an OnRetypingNode event.
        /// </summary>
        /// <param name="oldNode">The node to be retyped.</param>
        /// <param name="newNode">The new node with the common attributes, but without any incident edges assigned, yet.</param>
        public void RetypingNode(INode oldNode, INode newNode)
        {
            if(OnRetypingNode != null)
                OnRetypingNode(oldNode, newNode);
        }

        /// <summary>
        /// Fires an OnRetypingEdge event.
        /// </summary>
        /// <param name="oldEdge">The edge to be retyped.</param>
        /// <param name="newEdge">The new edge with the common attributes, but not fully connected with the incident nodes, yet.</param>
        public void RetypingEdge(IEdge oldEdge, IEdge newEdge)
        {
            if(OnRetypingEdge != null)
                OnRetypingEdge(oldEdge, newEdge);
        }

        /// <summary>
        /// Fires an OnRedirectingEdge event.
        /// </summary>
        /// <param name="edge">The edge to be redirected.</param>
        public void RedirectingEdge(IEdge edge)
        {
            if(OnRedirectingEdge != null)
                OnRedirectingEdge(edge);
        }

        /// <summary>
        /// Fires an OnVisitedAlloc event.
        /// </summary>
        /// <param name="visitorID">The allocated visitorID.</param>
        public void VisitedAlloc(int visitorID)
        {
            if(OnVisitedAlloc != null)
                OnVisitedAlloc(visitorID);
        }

        /// <summary>
        /// Fires an OnVisitedFree event.
        /// </summary>
        /// <param name="visitorID">The freed visitorID.</param>
        public void VisitedFree(int visitorID)
        {
            if(OnVisitedFree != null)
                OnVisitedFree(visitorID);
        }

        /// <summary>
        /// Fires an OnSettingVisited event.
        /// </summary>
        /// <param name="elem">The graph element of which the specified flag is to be set.</param>
        /// <param name="visitorID">The id of the visited flag to be set.</param>
        /// <param name="newValue">The new value.</param>
        public void SettingVisited(IGraphElement elem, int visitorID, bool newValue)
        {
            if(OnSettingVisited != null)
                OnSettingVisited(elem, visitorID, newValue);
        }

        public void SettingAddedNodeNames(String[] addedNodeNames)
        {
            if(OnSettingAddedNodeNames != null)
                OnSettingAddedNodeNames(addedNodeNames);
        }

        public void SettingAddedEdgeNames(String[] addedEdgeNames)
        {
            if(OnSettingAddedEdgeNames != null)
                OnSettingAddedEdgeNames(addedEdgeNames);
        }


        // convenience helper function for firing the changing node/edge/object attribute event (contains type dispatching and fixes the change type to assignment)
        public static void ChangingAttributeAssign(IGraph graph, IAttributeBearer owner, AttributeType attrType, object value)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.Assign, value, null);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.Assign, value, null);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.Assign, value, null);
        }

        // convenience helper function for firing the changing node/edge/object attribute event (contains type dispatching and fixes the change type to (indexed) container element assignment)
        public static void ChangingAttributeAssignElement(IGraph graph, IAttributeBearer owner, AttributeType attrType, object value, object key)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.AssignElement, value, key);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.AssignElement, value, key);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.AssignElement, value, key);
        }

        // convenience helper function for firing the changing node/edge/object attribute event (for an attribute of array or deque type, contains type dispatching and fixes the change type to element addition)
        public static void ChangingAttributePutElement(IGraph graph, IAttributeBearer owner, AttributeType attrType, object value, object optionalIndex)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.PutElement, value, optionalIndex);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.PutElement, value, optionalIndex);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.PutElement, value, optionalIndex);
        }

        // convenience helper function for firing the changing node/edge/object attribute event (for an attribute of array or deque type, contains type dispatching and fixes the change type to element removal)
        public static void ChangingAttributeRemoveElement(IGraph graph, IAttributeBearer owner, AttributeType attrType, object indexToRemove)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.RemoveElement, null, indexToRemove);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.RemoveElement, null, indexToRemove);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.RemoveElement, null, indexToRemove);
        }

        // convenience helper function for firing the changing node/edge/object attribute event (for an attribute of set type, contains type dispatching and fixes the change type to element addition)
        public static void ChangingSetAttributePutElement(IGraph graph, IAttributeBearer owner, AttributeType attrType, object value)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.PutElement, value, null);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.PutElement, value, null);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.PutElement, value, null);
        }

        // convenience helper function for firing the changing node/edge/object attribute event (for an attribute of set type, contains type dispatching and fixes the change type to element removal)
        public static void ChangingSetAttributeRemoveElement(IGraph graph, IAttributeBearer owner, AttributeType attrType, object valueToRemove)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.RemoveElement, valueToRemove, null);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.RemoveElement, valueToRemove, null);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.RemoveElement, valueToRemove, null);
        }

        // convenience helper function for firing the changing node/edge/object attribute event (for an attribute of map type, contains type dispatching and fixes the change type to element addition)
        public static void ChangingMapAttributePutElement(IGraph graph, IAttributeBearer owner, AttributeType attrType, object key, object value)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.PutElement, value, key);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.PutElement, value, key);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.PutElement, value, key);
        }

        // convenience helper function for firing the changing node/edge/object attribute event (for an attribute of map type, contains type dispatching and fixes the change type to element removal)
        public static void ChangingMapAttributeRemoveElement(IGraph graph, IAttributeBearer owner, AttributeType attrType, object keyToRemove)
        {
            if(owner is INode)
                graph.ChangingNodeAttribute((INode)owner, attrType, AttributeChangeType.RemoveElement, null, keyToRemove);
            else if(owner is IEdge)
                graph.ChangingEdgeAttribute((IEdge)owner, attrType, AttributeChangeType.RemoveElement, null, keyToRemove);
            else if(owner is IObject)
                graph.ChangingObjectAttribute((IObject)owner, attrType, AttributeChangeType.RemoveElement, null, keyToRemove);
        }

        // convenience helper function for firing the changed node/edge attribute event (contains graph element type dispatching)
        public static void ChangedAttribute(IGraph graph, IGraphElement elem, AttributeType attrType)
        {
            if(elem is INode)
                graph.ChangedNodeAttribute((INode)elem, attrType);
            else
                graph.ChangedEdgeAttribute((IEdge)elem, attrType);
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
