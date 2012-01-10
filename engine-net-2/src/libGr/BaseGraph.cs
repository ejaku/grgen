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
    /// Specifies how an IMatches object should be dumped.
    /// </summary>
    public enum DumpMatchSpecial
    {
        /// <summary>
        /// Insert virtual match nodes and connect the matches
        /// </summary>
        AllMatches = -1,

        /// <summary>
        /// Show only the matches
        /// </summary>
        OnlyMatches = -2
    }

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

        public int NumNodes { get { return GetNumCompatibleNodes(Model.NodeModel.RootType); } }
        public int NumEdges { get { return GetNumCompatibleEdges(Model.EdgeModel.RootType); } }
        public IEnumerable<INode> Nodes { get { return GetCompatibleNodes(Model.NodeModel.RootType); } }
        public IEnumerable<IEdge> Edges { get { return GetCompatibleEdges(Model.EdgeModel.RootType); } }

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


        #region Convenience methods

        /// <summary>
        /// Returns the node type with the given name.
        /// </summary>
        /// <param name="typeName">The name of a node type.</param>
        /// <returns>The node type with the given name or null, if it does not exist.</returns>
        public NodeType GetNodeType(String typeName) { return Model.NodeModel.GetType(typeName); }

        /// <summary>
        /// Returns the edge type with the given name.
        /// </summary>
        /// <param name="typeName">The name of a edge type.</param>
        /// <returns>The edge type with the given name or null, if it does not exist.</returns>
        public EdgeType GetEdgeType(String typeName) { return Model.EdgeModel.GetType(typeName); }

        #endregion Convenience methods

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

        #endregion Graph validation

        #region Graph dumping stuff

        // If the containment specified does not lead to a tree the results are unspecified,
        // the nodes and incident edges will be placed within several groups and dumped this way;
        // it is up to yComp to decide which nesting to use/where to locate the node (you'll see one node with duplicate edges in this case).
        // One could think of using the most deeply nested unambiguous node as the most sensible conflict resolution strategy in this case
        // but this would require quite some additional code plus a loop detection.
        // But that has the feeling of a workaround, it would only apply to dumping, not debugging,
        // and as it's quite easy for the user to get it right -> it is not worth the effort.
        // Simply require the user to fix his nesting model.

        /// <summary>
        /// Trivial IType implementation for virtual nodes
        /// </summary>
        internal class VirtualNodeType : NodeType
        {
            public static VirtualNodeType Instance = new VirtualNodeType();

            public VirtualNodeType()
                : base(0)
            {
                subOrSameGrGenTypes = superOrSameGrGenTypes = subOrSameTypes = superOrSameTypes
                    = new NodeType[] { this };
            }

            public override string Name { get { return "__VirtualType__"; } }
            public override String NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
            public override String NodeClassName { get { return "de.unika.ipd.grGen.libGr.VirtualNode"; } }
            public override bool IsA(GrGenType other) { return other is VirtualNodeType; }
            public override bool IsAbstract { get { return true; } }
            public override bool IsConst { get { return true; } }
            public override int NumAttributes { get { return 0; } }
            public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
            public override AttributeType GetAttributeType(String name) { return null; }

            public override INode CreateNode()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public override INode CreateNodeWithCopyCommons(INode oldNode)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return new Dictionary<string, string>(); } }
        }

        /// <summary>
        /// Trivial INode implementation for virtual nodes
        /// </summary>
        internal class VirtualNode : INode
        {
            int id;

            public VirtualNode(int newID)
            {
                id = newID;
            }

            public int ID { get { return id; } }
            public NodeType Type { get { return VirtualNodeType.Instance; } }
            GrGenType IGraphElement.Type { get { return VirtualNodeType.Instance; } }
            public bool InstanceOf(GrGenType type) { return type is VirtualNodeType; }

            public object GetAttribute(String attrName)
            { throw new NotSupportedException("Get attribute not supported on virtual node!"); }
            public void SetAttribute(String attrName, object value)
            { throw new NotSupportedException("Set attribute not supported on virtual node!"); }

            // TODO: Do we need to support this for other dumpers???
            public IEnumerable<IEdge> Outgoing { get { yield break; } }
            public IEnumerable<IEdge> Incoming { get { yield break; } }
            public IEnumerable<IEdge> Incident { get { yield break; } }
            public IEnumerable<IEdge> GetCompatibleOutgoing(EdgeType edgeType) { yield break; }
            public IEnumerable<IEdge> GetCompatibleIncoming(EdgeType edgeType) { yield break; }
            public IEnumerable<IEdge> GetCompatibleIncident(EdgeType edgeType) { yield break; }
            public IEnumerable<IEdge> GetExactOutgoing(EdgeType edgeType) { yield break; }
            public IEnumerable<IEdge> GetExactIncoming(EdgeType edgeType) { yield break; }
            public IEnumerable<IEdge> GetExactIncident(EdgeType edgeType) { yield break; }

            public INode Clone()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public void ResetAllAttributes()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public INode ReplacedByNode
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public bool Valid
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

			public IGraphElement ReplacedByElement
			{
				get { throw new Exception("The method or operation is not implemented."); }
			}
		}

        /// <summary>
        /// Dumps all attributes in the form "kind owner::name = value" into a String List
        /// </summary>
        /// <param name="elem">IGraphElement which attributes are to be dumped</param>
        /// <returns>A String List containing the dumped attributes </returns>
        private List<String> DumpAttributes(IGraphElement elem)
        {
            List<String> attribs = new List<String>();
            foreach(AttributeType attrType in elem.Type.AttributeTypes)
            {
                object attr = elem.GetAttribute(attrType.Name);
                String attrString = (attr != null) ? attr.ToString() : "<Not initialized>";
                attribs.Add(String.Format("{0}::{1} : {2} = {3}",
                    attrType.OwnerType.Name, attrType.Name, attrType.GetKindName(), attrString));
            }
            return attribs;
        }

        private String GetElemLabel(IGraphElement elem, DumpInfo dumpInfo)
        {
            List<InfoTag> infoTagTypes = dumpInfo.GetTypeInfoTags(elem.Type);
            String label = dumpInfo.GetElemTypeLabel(elem.Type);
            bool first = true;

            if(label == null)
            {
                label = dumpInfo.GetElementName(elem) + ":" + elem.Type.Name;
                first = false;
            }

            if(infoTagTypes != null)
            {
                foreach(InfoTag infoTag in infoTagTypes)
                {
                    object attr = elem.GetAttribute(infoTag.AttributeType.Name);
                    if(attr == null) continue;

                    if(!first) label += "\n";
                    else first = false;

                    if(infoTag.ShortInfoTag)
                        label += attr.ToString();
                    else
                        label += infoTag.AttributeType.Name + " = " + attr.ToString();
                }
            }

            return label;
        }

        internal class DumpContext
        {
            public IDumper Dumper;
            public DumpInfo DumpInfo;
            public Set<INode> MatchedNodes;
            public Set<INode> MultiMatchedNodes;
            public Set<IEdge> MatchedEdges;
            public Set<IEdge> MultiMatchedEdges;
            public Set<INode> InitialNodes = null;
            public Set<INode> Nodes = new Set<INode>();
            public Set<IEdge> ExcludedEdges = new Set<IEdge>();

            public DumpContext(IDumper dumper, DumpInfo dumpInfo, Set<INode> matchedNodes, Set<INode> multiMatchedNodes,
                Set<IEdge> matchedEdges, Set<IEdge> multiMatchedEdges)
            {
                Dumper = dumper;
                DumpInfo = dumpInfo;
                MatchedNodes = matchedNodes;
                MultiMatchedNodes = multiMatchedNodes;
                MatchedEdges = matchedEdges;
                MultiMatchedEdges = multiMatchedEdges;
            }
        }

        private void DumpNode(INode node, GrColor textColor, GrColor color, GrColor borderColor,
            GrNodeShape shape, IDumper dumper, DumpInfo dumpInfo)
        {
            dumper.DumpNode(node, GetElemLabel(node, dumpInfo), DumpAttributes(node), textColor,
                color, borderColor, shape);
        }

        private void DumpEdge(IEdge edge, GrColor textColor, GrColor color, GrLineStyle style,
            int thickness, IDumper dumper, DumpInfo dumpInfo)
        {
            dumper.DumpEdge(edge.Source, edge.Target, GetElemLabel(edge, dumpInfo), DumpAttributes(edge),
                textColor, color, style, thickness);
        }

        /// <summary>
        /// Dumps the given matches.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        /// <param name="dumpInfo">Specifies how the graph shall be dumped.</param>
        /// <param name="matches">An IMatches object containing the matches.</param>
        /// <param name="which">Which match to dump, or AllMatches for dumping all matches
        /// adding connections between them, or OnlyMatches to dump the matches only</param>
        public void DumpMatchOnly(IDumper dumper, DumpInfo dumpInfo, IMatches matches, DumpMatchSpecial which,
            ref Set<INode> matchedNodes, ref Set<INode> multiMatchedNodes, ref Set<IEdge> matchedEdges, ref Set<IEdge> multiMatchedEdges)
        {
            matchedNodes = new Set<INode>();
            matchedEdges = new Set<IEdge>();

            if((int)which >= 0 && (int)which < matches.Count)
            {
                // Show exactly one match

                IMatch match = matches.GetMatch((int)which);
                matchedNodes.Add(match.Nodes);
                matchedEdges.Add(match.Edges);
            }
            else
            {
                GrColor vnodeColor = dumpInfo.GetNodeDumpTypeColor(GrElemDumpType.VirtualMatch);
                GrColor vedgeColor = dumpInfo.GetEdgeDumpTypeColor(GrElemDumpType.VirtualMatch);
                GrColor vnodeBorderColor = dumpInfo.GetNodeDumpTypeBorderColor(GrElemDumpType.VirtualMatch);
                GrColor vnodeTextColor = dumpInfo.GetNodeDumpTypeTextColor(GrElemDumpType.VirtualMatch);
                GrColor vedgeTextColor = dumpInfo.GetEdgeDumpTypeTextColor(GrElemDumpType.VirtualMatch);
                GrNodeShape vnodeShape = dumpInfo.GetNodeDumpTypeShape(GrElemDumpType.VirtualMatch);
                GrLineStyle vedgeLineStyle = dumpInfo.GetEdgeDumpTypeLineStyle(GrElemDumpType.VirtualMatch);
                int vedgeThickness = dumpInfo.GetEdgeDumpTypeThickness(GrElemDumpType.VirtualMatch);

                multiMatchedNodes = new Set<INode>();
                multiMatchedEdges = new Set<IEdge>();

                // TODO: May edges to nodes be dumped before those nodes exist??
                // TODO: Should indices in strings start at 0 or 1? (original: 0)

                // Dump all matches with virtual nodes
                int i = 0;
                foreach(IMatch match in matches)
                {
                    VirtualNode virtNode = new VirtualNode(-i - 1);
                    dumper.DumpNode(virtNode, String.Format("{0}. match of {1}", i + 1, matches.Producer.Name),
                        null, vnodeTextColor, vnodeColor, vnodeBorderColor, vnodeShape);
                    int j = 1;
                    foreach(INode node in match.Nodes)
                    {
                        dumper.DumpEdge(virtNode, node, String.Format("node {0}", j++), null, 
                            vedgeTextColor, vedgeColor, vedgeLineStyle, vedgeThickness);

                        if(matchedNodes.Contains(node)) multiMatchedNodes.Add(node);
                        else matchedNodes.Add(node);
                    }

                    // Collect matched edges
                    foreach(IEdge edge in match.Edges)
                    {
                        if(matchedEdges.Contains(edge)) multiMatchedEdges.Add(edge);
                        else matchedEdges.Add(edge);
                    }
                    i++;
                }

                if(which == DumpMatchSpecial.OnlyMatches)
                {
                    // Dump the matches only
                    // First dump the matched nodes

                    foreach(INode node in matchedNodes)
                    {
                        GrElemDumpType dumpType;
                        if(multiMatchedNodes.Contains(node))
                            dumpType = GrElemDumpType.MultiMatched;
                        else
                            dumpType = GrElemDumpType.SingleMatched;

                        DumpNode(node, dumpInfo.GetNodeDumpTypeTextColor(dumpType),
                            dumpInfo.GetNodeDumpTypeColor(dumpType),
                            dumpInfo.GetNodeDumpTypeBorderColor(dumpType),
                            dumpInfo.GetNodeDumpTypeShape(dumpType),
                            dumper, dumpInfo);
                    }

                    // Now add the matched edges (possibly including "Not matched" nodes)

                    foreach(IEdge edge in matchedEdges)
                    {
                        if(!matchedNodes.Contains(edge.Source))
                            DumpNode(edge.Source,
                                dumpInfo.GetNodeTypeTextColor(edge.Source.Type),
                                dumpInfo.GetNodeTypeColor(edge.Source.Type),
                                dumpInfo.GetNodeTypeBorderColor(edge.Source.Type),
                                dumpInfo.GetNodeTypeShape(edge.Source.Type),
                                dumper, dumpInfo);

                        if(!matchedNodes.Contains(edge.Target))
                            DumpNode(edge.Target,
                                dumpInfo.GetNodeTypeTextColor(edge.Target.Type),
                                dumpInfo.GetNodeTypeColor(edge.Target.Type),
                                dumpInfo.GetNodeTypeBorderColor(edge.Target.Type),
                                dumpInfo.GetNodeTypeShape(edge.Target.Type),
                                dumper, dumpInfo);

                        GrElemDumpType dumpType;
                        if(multiMatchedEdges.Contains(edge))
                            dumpType = GrElemDumpType.MultiMatched;
                        else
                            dumpType = GrElemDumpType.SingleMatched;

                        DumpEdge(edge, dumpInfo.GetEdgeDumpTypeTextColor(dumpType),
                            dumpInfo.GetEdgeDumpTypeColor(dumpType),
                            dumpInfo.GetEdgeDumpTypeLineStyle(dumpType),
                            dumpInfo.GetEdgeDumpTypeThickness(dumpType),
                            dumper, dumpInfo);
                    }
                    return;
                }
            }
        }

        private void DumpEdgesFromNode(INode node, DumpContext dc)
        {
            // dumping only outgoing ensures every edge is dumped only once
            foreach(IEdge edge in node.Outgoing)        // TODO: This is probably wrong for group nodes grouped by outgoing edges
            {
                if(dc.DumpInfo.IsExcludedEdgeType(edge.Type)) continue;
                if(dc.ExcludedEdges.Contains(edge)) continue;
                if(!dc.InitialNodes.Contains(edge.Target)) continue;

                GrColor color;
                GrColor textColor;
                GrLineStyle style;
                int thickness;
                if(dc.MatchedEdges != null && dc.MatchedEdges.Contains(edge))
                {
                    GrElemDumpType dumpType;
                    if(dc.MultiMatchedEdges != null && dc.MultiMatchedEdges.Contains(edge))
                        dumpType = GrElemDumpType.MultiMatched;
                    else
                        dumpType = GrElemDumpType.SingleMatched;
                    color = dc.DumpInfo.GetEdgeDumpTypeColor(dumpType);
                    textColor = dc.DumpInfo.GetEdgeDumpTypeTextColor(dumpType);
                    style = dc.DumpInfo.GetEdgeDumpTypeLineStyle(dumpType);
                    thickness = dc.DumpInfo.GetEdgeDumpTypeThickness(dumpType);
                }
                else
                {
                    color = dc.DumpInfo.GetEdgeTypeColor(edge.Type);
                    textColor = dc.DumpInfo.GetEdgeTypeTextColor(edge.Type);
                    style = dc.DumpInfo.GetEdgeTypeLineStyle(edge.Type);
                    thickness = dc.DumpInfo.GetEdgeTypeThickness(edge.Type);
                }

                DumpEdge(edge, textColor, color, style, thickness, dc.Dumper, dc.DumpInfo);
            }
        }

        internal void DumpNodeAndEdges(INode node, DumpContext dc)
        {
            GrElemDumpType dumpType = GrElemDumpType.Normal;
            GrColor color, borderColor, textColor;
            GrNodeShape shape;
            if(dc.MatchedNodes != null && dc.MatchedNodes.Contains(node))
            {
                if(dc.MultiMatchedNodes != null && dc.MultiMatchedNodes.Contains(node))
                    dumpType = GrElemDumpType.MultiMatched;
                else
                    dumpType = GrElemDumpType.SingleMatched;
                color = dc.DumpInfo.GetNodeDumpTypeColor(dumpType);
                borderColor = dc.DumpInfo.GetNodeDumpTypeBorderColor(dumpType);
                textColor = dc.DumpInfo.GetNodeDumpTypeTextColor(dumpType);
                shape = dc.DumpInfo.GetNodeDumpTypeShape(dumpType);
            }
            else
            {
                color = dc.DumpInfo.GetNodeTypeColor(node.Type);
                borderColor = dc.DumpInfo.GetNodeTypeBorderColor(node.Type);
                textColor = dc.DumpInfo.GetNodeTypeTextColor(node.Type);
                shape = dc.DumpInfo.GetNodeTypeShape(node.Type);
            }

            DumpNode(node, textColor, color, borderColor, shape, dc.Dumper, dc.DumpInfo);

            DumpEdgesFromNode(node, dc);
        }

        internal class DumpGroupNode
        {
            public DumpGroupNode()
            {
                groupedNodes = new Set<INode>();
            }

            public Set<INode> groupedNodes;
        }

        internal void DumpGroupTree(INode root, Dictionary<INode, DumpGroupNode> groupNodes, DumpContext dc)
        {
            GrElemDumpType dumpType = GrElemDumpType.Normal;
            if(dc.MatchedNodes != null && dc.MatchedNodes.Contains(root))
            {
                if(dc.MultiMatchedNodes != null && dc.MultiMatchedNodes.Contains(root))
                    dumpType = GrElemDumpType.MultiMatched;
                else
                    dumpType = GrElemDumpType.SingleMatched;
            }

            dc.Dumper.StartSubgraph(root, GetElemLabel(root, dc.DumpInfo), DumpAttributes(root),
                dc.DumpInfo.GetNodeDumpTypeTextColor(dumpType), dc.DumpInfo.GetNodeTypeColor(root.Type)); // TODO: Check coloring...

            // Dump the elements nested inside this subgraph
            foreach(INode node in groupNodes[root].groupedNodes)
            {
                if(groupNodes.ContainsKey(node))
                {
                    DumpGroupTree(node, groupNodes, dc);
                    DumpEdgesFromNode(node, dc);
                }
                else
                {
                    DumpNodeAndEdges(node, dc);
                }
            }

            dc.Dumper.FinishSubgraph();
        }

        private void DumpGroups(Set<INode> nodes, DumpContext dc)
        {
            // Compute the nesting hierarchy (groups)
            Dictionary<INode, DumpGroupNode> groupNodes = new Dictionary<INode, DumpGroupNode>();
            Dictionary<INode, INode> containedIn = new Dictionary<INode, INode>();
            Set<INode> groupedNodes = new Set<INode>();

                // (by iterating the group node types in order of dump declaration and removing the iterated nodes from the available nodes,
                //  the conflict resolution priorities of debug enable are taken care of)
            foreach(GroupNodeType groupNodeType in dc.DumpInfo.GroupNodeTypes)
            {
                foreach(INode node in GetCompatibleNodes(groupNodeType.NodeType))
                {
                    if(nodes.Contains(node))
                    {
                        if(!groupNodes.ContainsKey(node)) groupNodes.Add(node, new DumpGroupNode()); // todo: is the if needed?
                        nodes.Remove(node);
                    }

                    if(dc.DumpInfo.IsExcludedNodeType(node.Type)) continue;

                    foreach(IEdge edge in node.Incoming)
                    {
                        GroupMode grpMode = groupNodeType.GetEdgeGroupMode(edge.Type, edge.Source.Type);
                        if((grpMode & GroupMode.GroupIncomingNodes) == 0) continue;
                        if(!dc.Nodes.Contains(edge.Source)) continue;
                        groupNodes[node].groupedNodes.Add(edge.Source);
                        if(!containedIn.ContainsKey(edge.Source)) containedIn.Add(edge.Source, node); // crashes without if in case of multiple containment due to dump misspecification by user
                        groupedNodes.Add(edge.Source);
                        if((grpMode & GroupMode.Hidden) != 0) dc.ExcludedEdges.Add(edge);
                    }
                    foreach(IEdge edge in node.Outgoing)
                    {
                        GroupMode grpMode = groupNodeType.GetEdgeGroupMode(edge.Type, edge.Target.Type);
                        if((grpMode & GroupMode.GroupOutgoingNodes) == 0) continue;
                        if(!dc.Nodes.Contains(edge.Target)) continue;
                        groupNodes[node].groupedNodes.Add(edge.Target);
                        if(!containedIn.ContainsKey(edge.Target)) containedIn.Add(edge.Target, node); // crashes without if in case of multiple containment due to dump misspecification by user
                        groupedNodes.Add(edge.Target);
                        if((grpMode & GroupMode.Hidden) != 0) dc.ExcludedEdges.Add(edge);
                    }
                }
            }

            // Dump the groups (begin at the roots of the group trees)
            foreach(KeyValuePair<INode, DumpGroupNode> groupNode in groupNodes)
            {
                if(!containedIn.ContainsKey(groupNode.Key))
                {
                    DumpGroupTree(groupNode.Key, groupNodes, dc);
                    DumpEdgesFromNode(groupNode.Key, dc);
                }
            }

            // Dump the rest, which has not been grouped
            nodes.Remove(groupedNodes);

            foreach(INode node in nodes)
            {
                DumpNodeAndEdges(node, dc);
            }
        }

        public void DumpMatch(IDumper dumper, DumpInfo dumpInfo, IMatches matches, DumpMatchSpecial which)
        {
            Set<INode> matchedNodes = null;
            Set<INode> multiMatchedNodes = null;
            Set<IEdge> matchedEdges = null;
            Set<IEdge> multiMatchedEdges = null;

            if(matches != null)
            {
                DumpMatchOnly(dumper, dumpInfo, matches, which,
                    ref matchedNodes, ref multiMatchedNodes, ref matchedEdges, ref multiMatchedEdges);
            }

            // Dump the graph, but color the matches if any exist

            DumpContext dc = new DumpContext(dumper, dumpInfo,
                matchedNodes, multiMatchedNodes, matchedEdges, multiMatchedEdges);

            foreach(NodeType nodeType in Model.NodeModel.Types)
            {
                if(dumpInfo.IsExcludedNodeType(nodeType)) continue;
                dc.Nodes.Add(GetExactNodes(nodeType));
            }

            dc.InitialNodes = new Set<INode>(dc.Nodes);
            Set<INode> nodes = new Set<INode>(dc.Nodes);
            DumpGroups(nodes, dc);
        }

        public void Dump(IDumper dumper, DumpInfo dumpInfo)
        {
            DumpMatch(dumper, dumpInfo, null, 0);
        }

        #endregion Graph dumping stuff


        public abstract bool IsIsomorph(IGraph that);
    }
}
