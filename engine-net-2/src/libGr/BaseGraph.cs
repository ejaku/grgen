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
    /// Adding some methods implemented over the IGraph interface (some convenience stuff, graph validation, and graph dumping).
    /// </summary>
    public abstract class BaseGraph : IGraph
    {
        #region Abstract and virtual members

        public abstract String Name { get; }
        public abstract IGraphModel Model { get; }
        public abstract bool ReuseOptimization { get; set; }
        public abstract void DestroyGraph();

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

        public abstract int AllocateVisitedFlag();
        public abstract void FreeVisitedFlag(int visitorID);
        public abstract void ResetVisitedFlag(int visitorID);
        public abstract void SetVisited(IGraphElement elem, int visitorID, bool visited);
        public abstract bool IsVisited(IGraphElement elem, int visitorID);

        #endregion Abstract and virtual members


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


        #region Graph validation

        public bool Validate(ValidationMode mode, out List<ConnectionAssertionError> errors)
        {
            bool result = true;
            Dictionary<IEdge, bool> checkedOutEdges = new Dictionary<IEdge, bool>(2 * NumEdges);
            Dictionary<IEdge, bool> checkedInEdges = new Dictionary<IEdge, bool>(2 * NumEdges);
            errors = new List<ConnectionAssertionError>();

            int numConnectionAssertions = 0;
            foreach(ValidateInfo valInfo in Model.ValidateInfo)
            {
                // Check outgoing count on nodes of source type
                foreach(INode node in GetCompatibleNodes(valInfo.SourceType))
                {
                    result &= ValidateSource(node, valInfo, errors, checkedOutEdges, checkedInEdges);
                }
                // Check incoming count on nodes of target type
                foreach(INode node in GetCompatibleNodes(valInfo.TargetType))
                {
                    result &= ValidateTarget(node, valInfo, errors, checkedOutEdges, checkedInEdges);
                }

                ++numConnectionAssertions;
            }

            if(mode == ValidationMode.StrictOnlySpecified)
            {
                Dictionary<EdgeType, bool> strictnessCheckedEdgeTypes = new Dictionary<EdgeType, bool>(2 * numConnectionAssertions);
                foreach(ValidateInfo valInfo in Model.ValidateInfo)
                {
                    if(strictnessCheckedEdgeTypes.ContainsKey(valInfo.EdgeType))
                        continue;

                    foreach(IEdge edge in GetExactEdges(valInfo.EdgeType))
                    {
                        // Some edges with connection assertions specified are not covered; strict only specified validation prohibits that!
                        if(!checkedOutEdges.ContainsKey(edge) || !checkedInEdges.ContainsKey(edge))
                        {
                            errors.Add(new ConnectionAssertionError(CAEType.EdgeNotSpecified, edge, 0, null));
                            result = false;
                        }
                    }
                    strictnessCheckedEdgeTypes.Add(valInfo.EdgeType, true);
                }
            }

            if(mode == ValidationMode.Strict
                && (NumEdges != checkedOutEdges.Count || NumEdges != checkedInEdges.Count))
            {
                // Some edges are not covered; strict validation prohibits that!
                foreach(IEdge edge in Edges)
                {
                    if(!checkedOutEdges.ContainsKey(edge) || !checkedInEdges.ContainsKey(edge))
                    {
                        errors.Add(new ConnectionAssertionError(CAEType.EdgeNotSpecified, edge, 0, null));
                        result = false;
                    }
                }
            }

            if(result) errors = null;
            return result;
        }

        bool ValidateSource(INode node, ValidateInfo valInfo, List<ConnectionAssertionError> errors,
            Dictionary<IEdge, bool> checkedOutEdges, Dictionary<IEdge, bool> checkedInEdges)
        {
            bool result = true;

            // Check outgoing edges
            long num = CountOutgoing(node, valInfo.EdgeType, valInfo.TargetType, checkedOutEdges);
            if(valInfo.BothDirections)
            {
                long incoming = CountIncoming(node, valInfo.EdgeType, valInfo.TargetType, checkedInEdges);
                num -= CountReflexive(node, valInfo.EdgeType, valInfo.TargetType, num, incoming);
                num += incoming;
            }

            if(num < valInfo.SourceLower)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooFewSources, node, num, valInfo));
                result = false;
            }
            else if(num > valInfo.SourceUpper)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooManySources, node, num, valInfo));
                result = false;
            }

            return result;
        }

        bool ValidateTarget(INode node, ValidateInfo valInfo, List<ConnectionAssertionError> errors,
            Dictionary<IEdge, bool> checkedOutEdges, Dictionary<IEdge, bool> checkedInEdges)
        {
            bool result = true;

            // Check incoming edges
            long num = CountIncoming(node, valInfo.EdgeType, valInfo.SourceType, checkedInEdges);
            if(valInfo.BothDirections)
            {
                long outgoing = CountOutgoing(node, valInfo.EdgeType, valInfo.SourceType, checkedOutEdges);
                num -= CountReflexive(node, valInfo.EdgeType, valInfo.SourceType, outgoing, num);
                num += outgoing;
            }

            if(num < valInfo.TargetLower)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooFewTargets, node, num, valInfo));
                result = false;
            }
            else if(num > valInfo.TargetUpper)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooManyTargets, node, num, valInfo));
                result = false;
            }

            return result;
        }

        long CountOutgoing(INode node, EdgeType edgeType, NodeType targetNodeType,
            Dictionary<IEdge, bool> checkedOutEdges)
        {
            long num = 0;
            foreach(IEdge outEdge in node.GetExactOutgoing(edgeType))
            {
                if(!outEdge.Target.Type.IsA(targetNodeType)) continue;
                checkedOutEdges[outEdge] = true;
                ++num;
            }
            return num;
        }

        long CountIncoming(INode node, EdgeType edgeType, NodeType sourceNodeType,
            Dictionary<IEdge, bool> checkedInEdges)
        {
            long num = 0;
            foreach(IEdge inEdge in node.GetExactIncoming(edgeType))
            {
                if(!inEdge.Source.Type.IsA(sourceNodeType)) continue;
                checkedInEdges[inEdge] = true;
                ++num;
            }
            return num;
        }

        long CountReflexive(INode node, EdgeType edgeType, NodeType oppositeNodeType,
            long outgoing, long incoming)
        {
            long num = 0;
            if(outgoing <= incoming)
            {
                foreach(IEdge outEdge in node.GetExactOutgoing(edgeType))
                {
                    if(!outEdge.Target.Type.IsA(oppositeNodeType)) continue;
                    if(outEdge.Target != node) continue;
                    ++num;
                }
            }
            else
            {
                foreach(IEdge inEdge in node.GetExactIncoming(edgeType))
                {
                    if(!inEdge.Source.Type.IsA(oppositeNodeType)) continue;
                    if(inEdge.Source != node) continue;
                    ++num;
                }
            }
            return num;
        }

        #endregion Graph validation


        /// <summary>
        /// Returns the outgoing edges of given type from the given node, with a target node of given type.
        /// </summary>
        public IDictionary Outgoing(INode node, EdgeType edgeType, NodeType targetNodeType)
        {
            IDictionary set = DictionaryListHelper.NewDictionary(DictionaryListHelper.GetTypeFromNameForDictionaryOrList(edgeType.Name, this), typeof(SetValueType));
            foreach(IEdge outEdge in node.GetCompatibleOutgoing(edgeType))
            {
                if(!outEdge.Target.Type.IsA(targetNodeType)) continue;
                set.Add(outEdge, null);
            }
            return set;
        }

        /// <summary>
        /// Returns the incoming edges of given type to the given node, with a source node of given type.
        /// </summary>
        public IDictionary Incoming(INode node, EdgeType edgeType, NodeType sourceNodeType)
        {
            IDictionary set = DictionaryListHelper.NewDictionary(DictionaryListHelper.GetTypeFromNameForDictionaryOrList(edgeType.Name, this), typeof(SetValueType));
            foreach(IEdge inEdge in node.GetCompatibleIncoming(edgeType))
            {
                if(!inEdge.Source.Type.IsA(sourceNodeType)) continue;
                set.Add(inEdge, null);
            }
            return set;
        }


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


        public abstract bool IsIsomorph(IGraph that);
    }
}
