/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

namespace de.unika.ipd.grGen.libGr
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public enum GrColor
    {
        Default = -1,
        Black, Blue, Green, Cyan, Red, Purple, Brown, Grey,
        LightGrey, LightBlue, LightGreen, LightCyan, LightRed, LightPurple, Yellow, White,
        DarkBlue, DarkRed, DarkGreen, DarkYellow, DarkMagenta, DarkCyan, Gold, Lilac,
        Turqouise, Aquamarine, Khaki, Pink, Orange, Orchid
    }

    public enum GrElemDumpType
    {
        Normal,
        SingleMatched,
        MultiMatched,
        VirtualMatch
    }

    public enum GrLineStyle
    {
        Default = -1,
        Solid, Dotted, Dashed, Invisible,
        MaxStyle = Invisible
    }

    public enum GrNodeShape
    {
        Default = -1,
        Box, Triangle, Circle, Ellipse, Rhomb, Hexagon, Trapeze, UpTrapeze, LParallelogram, RParallelogram
    }

    /// <summary>
    /// A dumper for output of graphs.
    /// </summary>
    public interface IDumper : IDisposable
    {
        /// <summary>
        /// Dump a node.
        /// </summary>
        /// <param name="node">The node to be dumped</param>
        /// <param name="label">The label to use for the node</param>
        /// <param name="attributes">An enumerable of attribute strings</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="nodeColor">The color of the node</param>
        /// <param name="borderColor">The color of the node border</param>
		/// <param name="nodeShape">The shape of the node</param>
        void DumpNode(INode node, String label, IEnumerable<String> attributes,
            GrColor textColor, GrColor nodeColor, GrColor borderColor, GrNodeShape nodeShape);

        /// <summary>
        /// Dump an edge
        /// </summary>
        /// <param name="srcNode">The source node of the edge</param>
        /// <param name="tgtNode">The target node of the edge</param>
        /// <param name="label">The label of the edge, may be null</param>
        /// <param name="attributes">An enumerable of attribute strings</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="edgeColor">The color of the edge</param>
        /// <param name="lineStyle">The linestyle of the edge</param>
        void DumpEdge(INode srcNode, INode tgtNode, String label, IEnumerable<String> attributes,
            GrColor textColor, GrColor edgeColor, GrLineStyle lineStyle);

        /// <summary>
        /// Creates a new sub-graph
        /// </summary>
        /// <param name="node">The node starting the new sub-graph</param>
        /// <param name="label">The label to use for the node</param>
        /// <param name="attributes">An enumerable of attribute strings</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="subgraphColor">The color of the subgraph node</param>
        void StartSubgraph(INode node, String label, IEnumerable<String> attributes, GrColor textColor,
            GrColor subgraphColor);

        /// <summary>
        /// Finishes a subgraph
        /// </summary>
        void FinishSubgraph();

        /// <summary>
        /// Finishes the dump and closes the file
        /// </summary>
        void FinishDump();
    }

    public delegate String ElementNameGetter(IGraphElement elem);
    public delegate void NodeTypeAppearanceChangedHandler(NodeType type);
    public delegate void EdgeTypeAppearanceChangedHandler(EdgeType type);
    public delegate void TypeInfotagsChangedHandler(GrGenType type);

    /// <summary>
    /// The supported group modes.
    /// </summary>
    [Flags]
    public enum GroupMode
    {
        /// <summary>
        /// Do not group any nodes.
        /// </summary>
        None = 0,

        /// <summary>
        /// Group only source nodes of incoming edges of the group node.
        /// </summary>
        GroupIncomingNodes = 1,

        /// <summary>
        /// Group only target nodes of outgoing edges of the group node.
        /// </summary>
        GroupOutgoingNodes = 2,

        /// <summary>
        /// Group all nodes connected to the group node.
        /// </summary>
        GroupAllNodes = 3,

        /// <summary>
        /// Hide the grouping edges in visualizations of the graph.
        /// </summary>
        Hidden = 4
    }

    /// <summary>
    /// Specifies which nodes are grouped by this group node type.
    /// </summary>
    public class GroupNodeType : IComparable<GroupNodeType>
    {
        /// <summary>
        /// The node type of this group node type.
        /// </summary>
        public NodeType NodeType;

        /// <summary>
        /// Groups with lower priorities can be grouped inside groups with higher priorities.
        /// For same priorities the behaviour is undefined.
        /// </summary>
        public int Priority;

        /// <summary>
        /// Initializes a GroupNodeType.
        /// </summary>
        /// <param name="nodeType">The node type of this group node type.</param>
        /// <param name="priority">The priority to be used, when two group node types apply to one edge.</param>
        public GroupNodeType(NodeType nodeType, int priority)
        {
            NodeType = nodeType;
            Priority = priority;
        }

        /// <summary>
        /// Gets the group mode for this group node with an edge of type edgeType connected to a node of type adjNodeType.
        /// </summary>
        /// <param name="edgeType">The type of an edge connected to the group node.</param>
        /// <param name="adjNodeType">The type of a node connected to the group node.</param>
        /// <returns>The group mode for this case. Default is GroupMode.None.</returns>
        public GroupMode GetEdgeGroupMode(EdgeType edgeType, NodeType adjNodeType)
        {
            Dictionary<NodeType, GroupMode> groupEdge;
            if(groupEdges.TryGetValue(edgeType, out groupEdge))
            {
                GroupMode groupMode;
                if(groupEdge.TryGetValue(adjNodeType, out groupMode))
                    return groupMode;
            }

            return GroupMode.None;
        }

        /// <summary>
        /// Sets the group mode for the given case. Inheritance is handled as specified by the parameters.
        /// </summary>
        /// <param name="edgeType">The type of an edge connected to the group node.</param>
        /// <param name="exactEdgeType">Specifies, whether only the exact given edge type is meant.</param>
        /// <param name="adjNodeType">The type of a node connected to the group node.</param>
        /// <param name="exactAdjNodeType">Specifies, whether only the exact given node type is meant.</param>
        /// <param name="groupMode">The group mode to be applied.</param>
        public void SetEdgeGroupMode(EdgeType edgeType, bool exactEdgeType, NodeType adjNodeType,
                bool exactAdjNodeType, GroupMode groupMode)
        {
            foreach(EdgeType subEdgeType in edgeType.subOrSameTypes)
            {
                Dictionary<NodeType, GroupMode> groupEdge;
                if(!groupEdges.TryGetValue(subEdgeType, out groupEdge))
                {
                    if(groupMode == GroupMode.None) return;
                    groupEdge = new Dictionary<NodeType, GroupMode>();
                    groupEdges[subEdgeType] = groupEdge;
                }

                foreach(NodeType subNodeType in adjNodeType.SubOrSameTypes)
                {
                    if(groupMode == GroupMode.None)
                        groupEdge.Remove(subNodeType);
                    else
                        groupEdge[subNodeType] = groupMode;

                    if(exactAdjNodeType) break;     // don't change group modes for subtypes of adjNodeType
                }

                if(exactEdgeType) break;        // don't change group modes for subtypes of edgeType
            }
        }

        /// <summary>
        /// Enumerates all definitions related to this group node type.
        /// </summary>
        public IEnumerable<KeyValuePair<EdgeType, Dictionary<NodeType, GroupMode>>> GroupEdges
        {
            get { return groupEdges; }
        }

        /// <summary>
        /// Compares this group node type to another given one.
        /// </summary>
        /// <param name="other">The other group node type.</param>
        /// <returns>Priority - other.Priority</returns>
        public int CompareTo(GroupNodeType other)
        {
            return Priority - other.Priority;
        }

        /// <summary>
        /// A map from EdgeTypes to NodeTypes to a GroupMode, specifying which connected nodes are grouped into this node.
        /// </summary>
        private Dictionary<EdgeType, Dictionary<NodeType, GroupMode>> groupEdges = new Dictionary<EdgeType, Dictionary<NodeType, GroupMode>>();
    }

    /// <summary>
    /// A description of how to dump a graph.
    /// </summary>
    public class DumpInfo
    {
        /// <summary>
        /// Used as priorities for GroupNodeType objects.
        /// </summary>
        int nextGroupID = 0x7fffffff;

        GrColor[] nodeColors = new GrColor[4];
        GrColor[] nodeBorderColors = new GrColor[4];
        GrColor[] edgeColors = new GrColor[4];
        GrColor[] nodeTextColors = new GrColor[4];
        GrColor[] edgeTextColors = new GrColor[4];

        Dictionary<NodeType, bool> excludedNodeTypes = new Dictionary<NodeType, bool>();
        Dictionary<EdgeType, bool> excludedEdgeTypes = new Dictionary<EdgeType, bool>();
        List<GroupNodeType> groupNodeTypes = new List<GroupNodeType>();
        Dictionary<NodeType, GroupNodeType> nodeTypeToGroupNodeType = new Dictionary<NodeType, GroupNodeType>();
        Dictionary<NodeType, GrColor> nodeTypeColors = new Dictionary<NodeType, GrColor>();
        Dictionary<NodeType, GrColor> nodeTypeBorderColors = new Dictionary<NodeType, GrColor>();
        Dictionary<NodeType, GrColor> nodeTypeTextColors = new Dictionary<NodeType, GrColor>();
        Dictionary<NodeType, GrNodeShape> nodeTypeShapes = new Dictionary<NodeType, GrNodeShape>();
        Dictionary<EdgeType, GrColor> edgeTypeColors = new Dictionary<EdgeType, GrColor>();
        Dictionary<EdgeType, GrColor> edgeTypeTextColors = new Dictionary<EdgeType, GrColor>();
        Dictionary<GrGenType, List<AttributeType>> infoTags = new Dictionary<GrGenType, List<AttributeType>>();

        public IEnumerable<NodeType> ExcludedNodeTypes { get { return excludedNodeTypes.Keys; } }
        public IEnumerable<EdgeType> ExcludedEdgeTypes { get { return excludedEdgeTypes.Keys; } }
        public IEnumerable<GroupNodeType> GroupNodeTypes { get { return groupNodeTypes; } }

        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeColors { get { return nodeTypeColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeBorderColors { get { return nodeTypeBorderColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeTextColors { get { return nodeTypeTextColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrNodeShape>> NodeTypeShapes { get { return nodeTypeShapes; } }
        public IEnumerable<KeyValuePair<EdgeType, GrColor>> EdgeTypeColors { get { return edgeTypeColors; } }
        public IEnumerable<KeyValuePair<EdgeType, GrColor>> EdgeTypeTextColors { get { return edgeTypeTextColors; } }

        public IEnumerable<KeyValuePair<GrGenType, List<AttributeType>>> InfoTags { get { return infoTags; } }

        private ElementNameGetter elementNameGetter;

        public event NodeTypeAppearanceChangedHandler OnNodeTypeAppearanceChanged;
        public event EdgeTypeAppearanceChangedHandler OnEdgeTypeAppearanceChanged;
        public event TypeInfotagsChangedHandler OnTypeInfotagsChanged;

        private void NodeTypeAppearanceChanged(NodeType type)
        {
            NodeTypeAppearanceChangedHandler handler = OnNodeTypeAppearanceChanged;
            if(handler != null) handler(type);
        }

        private void EdgeTypeAppearanceChanged(EdgeType type)
        {
            EdgeTypeAppearanceChangedHandler handler = OnEdgeTypeAppearanceChanged;
            if(handler != null) handler(type);
        }

        private void TypeInfotagsChanged(GrGenType type)
        {
            TypeInfotagsChangedHandler handler = OnTypeInfotagsChanged;
            if (handler != null) handler(type);
        }

        public DumpInfo(ElementNameGetter nameGetter)
        {
            elementNameGetter = nameGetter;
            InitDumpColors();
        }

        public void ExcludeNodeType(NodeType nodeType)
        {
            excludedNodeTypes.Add(nodeType, true);
        }

        public bool IsExcludedNodeType(NodeType nodeType)
        {
            return excludedNodeTypes.ContainsKey(nodeType);
        }

        public void ExcludeEdgeType(EdgeType edgeType)
        {
            excludedEdgeTypes.Add(edgeType, true);
        }

        public bool IsExcludedEdgeType(EdgeType edgeType)
        {
            return excludedEdgeTypes.ContainsKey(edgeType);
        }

        /// <summary>
        /// Adds or extends a GroupNodeType.
        /// All nodes connected via the given edge type and fulfilling the GroupType condition are placed inside a group
        /// corresponding to the according group node. The edges which lead to the grouping are not displayed.
        /// The group node types are ordered by the time of creation. Groups of group node types created later
        /// will be moved into groups of group node types created earlier.
        /// </summary>
        /// <param name="nodeType">The node type of the group node.</param>
        /// <param name="exactNodeType">True, if the node type must be exact, false, if also subtypes are allowed.</param>
        /// <param name="edgeType">An edge type along which nodes are grouped.</param>
        /// <param name="exactEdgeType">True, if the edge type must be exact, false, if also subtypes are allowed.</param>
        /// <param name="adjNodeType">The adjacent node type according to the edge.</param>
        /// <param name="exactAdjNodeType">True, if the adjacent node type must be exact, false, if also subtypes are allowed.</param>
        /// <param name="groupMode">Specifies how the edge is used for grouping.</param>
        public void AddOrExtendGroupNodeType(NodeType nodeType, bool exactNodeType, EdgeType edgeType, bool exactEdgeType,
                NodeType adjNodeType, bool exactAdjNodeType, GroupMode groupMode)
        {
            foreach(NodeType subType in nodeType.SubOrSameTypes)
            {
                GroupNodeType groupNodeType;
                if(!nodeTypeToGroupNodeType.TryGetValue(subType, out groupNodeType))
                {
                    groupNodeType = new GroupNodeType(subType, nextGroupID++);
                    nodeTypeToGroupNodeType[subType] = groupNodeType;
                    groupNodeTypes.Add(groupNodeType);
                }
                groupNodeType.SetEdgeGroupMode(edgeType, exactEdgeType, adjNodeType, exactAdjNodeType, groupMode);

                if(exactNodeType) break;  // don't change group modes for any subtypes
            }
        }

        /// <summary>
        /// Gets the GroupNodeType for a given node type.
        /// </summary>
        /// <param name="nodeType">The given node type.</param>
        /// <returns>The GroupNodeType of the given node type or null, if it is not a group node type.</returns>
        public GroupNodeType GetGroupNodeType(NodeType nodeType)
        {
            GroupNodeType groupNodeType;
            nodeTypeToGroupNodeType.TryGetValue(nodeType, out groupNodeType);
            return groupNodeType;
        }

/*        /// <summary>
        /// Gets the group mode for a given edge.
        /// Only the edge type and the types of the adjacent nodes are used to determine the group mode.
        /// </summary>
        /// <param name="edge">The edge to be inspected.</param>
        /// <returns>The group mode for the given edge.</returns>
        public GroupMode GetGroupMode(IEdge edge)
        {
            GroupNodeType srcGNT = GetGroupNodeType(edge.Source.Type);
            GroupNodeType tgtGNT = GetGroupNodeType(edge.Target.Type);
            if(srcGNT == null && tgtGNT == null) return GroupMode.None;

            if(srcGNT == null || srcGNT.Priority <= tgtGNT.Priority)
                return tgtGNT.GetEdgeGroupMode(edge.Type, edge.Source.Type);
            else
                return srcGNT.GetEdgeGroupMode(edge.Type, edge.Target.Type);
        }*/

        public GrColor GetNodeTypeColor(NodeType nodeType)
        {
            GrColor col;
            if(!nodeTypeColors.TryGetValue(nodeType, out col))
                return GetNodeDumpTypeColor(GrElemDumpType.Normal);
            return col;
        }

        public GrColor GetNodeTypeBorderColor(NodeType nodeType)
        {
            GrColor col;
            if(!nodeTypeBorderColors.TryGetValue(nodeType, out col))
                return GetNodeDumpTypeBorderColor(GrElemDumpType.Normal);
            return col;
        }

        public GrColor GetNodeTypeTextColor(NodeType nodeType)
        {
            GrColor col;
            if(!nodeTypeTextColors.TryGetValue(nodeType, out col))
                return GetNodeDumpTypeTextColor(GrElemDumpType.Normal);
            return col;
        }

        public GrNodeShape GetNodeTypeShape(NodeType nodeType)
        {
            GrNodeShape shape;
            if(!nodeTypeShapes.TryGetValue(nodeType, out shape))
                return GrNodeShape.Default;
            return shape;
        }

        public GrColor GetEdgeTypeColor(EdgeType edgeType)
        {
            GrColor col;
            if(!edgeTypeColors.TryGetValue(edgeType, out col))
                return GetEdgeDumpTypeColor(GrElemDumpType.Normal);
            return col;
        }

        public GrColor GetEdgeTypeTextColor(EdgeType edgeType)
        {
            GrColor col;
            if(!edgeTypeTextColors.TryGetValue(edgeType, out col))
                return GetEdgeDumpTypeTextColor(GrElemDumpType.Normal);
            return col;
        }

        public void SetNodeTypeColor(NodeType nodeType, GrColor color)
        {
            nodeTypeColors[nodeType] = color;               // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetNodeTypeBorderColor(NodeType nodeType, GrColor color)
        {
            nodeTypeBorderColors[nodeType] = color;         // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetNodeTypeTextColor(NodeType nodeType, GrColor color)
        {
            nodeTypeTextColors[nodeType] = color;           // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetNodeTypeShape(NodeType nodeType, GrNodeShape shape)
        {
            nodeTypeShapes[nodeType] = shape;               // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetEdgeTypeColor(EdgeType edgeType, GrColor color)
        {
            edgeTypeColors[edgeType] = color;               // overwrites existing mapping
            EdgeTypeAppearanceChanged(edgeType);
        }

        public void SetEdgeTypeTextColor(EdgeType edgeType, GrColor color)
        {
            edgeTypeTextColors[edgeType] = color;           // overwrites existing mapping
            EdgeTypeAppearanceChanged(edgeType);
        }

        public void SetNodeDumpTypeColor(GrElemDumpType type, GrColor color)
        {
            nodeColors[(int) type] = color;
        }

        public void SetEdgeDumpTypeColor(GrElemDumpType type, GrColor color)
        {
            edgeColors[(int) type] = color;
        }

        public void SetNodeDumpTypeBorderColor(GrElemDumpType type, GrColor color)
        {
            nodeBorderColors[(int) type] = color;
        }

        public void SetNodeDumpTypeTextColor(GrElemDumpType type, GrColor color)
        {
            nodeTextColors[(int) type] = color;
        }

        public void SetEdgeDumpTypeTextColor(GrElemDumpType type, GrColor color)
        {
            edgeTextColors[(int) type] = color;
        }

        public GrColor GetNodeDumpTypeColor(GrElemDumpType type)
        {
            return nodeColors[(int) type];
        }

        public GrColor GetEdgeDumpTypeColor(GrElemDumpType type)
        {
            return edgeColors[(int) type];
        }

        public GrColor GetNodeDumpTypeBorderColor(GrElemDumpType type)
        {
            return nodeBorderColors[(int) type];
        }

        public GrColor GetNodeDumpTypeTextColor(GrElemDumpType type)
        {
            return nodeTextColors[(int) type];
        }

        public GrColor GetEdgeDumpTypeTextColor(GrElemDumpType type)
        {
            return edgeTextColors[(int) type];
        }

        /// <summary>
        /// Returns the IAttributeType for the given IType holding the info tag, if any exists
        /// </summary>
        /// <param name="type">The IType to be examined</param>
        /// <returns>The IAttributeType of the info tag or NULL, if this IType has no registered info tag</returns>
        public List<AttributeType> GetTypeInfoTags(GrGenType type)
        {
            List<AttributeType> attrTypes;
            if(!infoTags.TryGetValue(type, out attrTypes)) return null;
            else return attrTypes;
        }

        /// <summary>
        /// Maps an AttributeType to a GrGenType or unmaps the GrGenType, if attrType is null
        /// </summary>
        /// <param name="type">The GrGenType to be mapped/unmapped</param>
        /// <param name="attrType">The AttributeType</param>
        public void AddTypeInfoTag(GrGenType type, AttributeType attrType)
        {
            List<AttributeType> attrTypes;
            if(!infoTags.TryGetValue(type, out attrTypes))
            {
                infoTags[type] = attrTypes = new List<AttributeType>();
            }
            attrTypes.Add(attrType);
            TypeInfotagsChanged(type);
        }

		/// <summary>
		/// Gets the element name of the given graph element according
		/// to the element name getter given to the constructor of DumpInfo.
		/// </summary>
		/// <param name="elem">The element.</param>
		/// <returns>The name of the element.</returns>
        public String GetElementName(IGraphElement elem)
        {
            return elementNameGetter(elem);
        }

        private void InitDumpColors()
        {
            nodeColors[0] = GrColor.Yellow;
            nodeColors[1] = GrColor.Khaki;
            nodeColors[2] = GrColor.Khaki;
            nodeColors[3] = GrColor.LightGrey;

            nodeBorderColors[0] = GrColor.DarkYellow;
            nodeBorderColors[1] = GrColor.DarkYellow;
            nodeBorderColors[2] = GrColor.LightRed;
            nodeBorderColors[3] = GrColor.Grey;

            edgeColors[0] = GrColor.DarkYellow;
            edgeColors[1] = GrColor.Khaki;
            edgeColors[2] = GrColor.LightRed;
            edgeColors[3] = GrColor.Grey;

            nodeTextColors[0] = GrColor.Default;
            nodeTextColors[1] = GrColor.Default;
            nodeTextColors[2] = GrColor.Default;
            nodeTextColors[3] = GrColor.Default;

            edgeTextColors[0] = GrColor.Default;
            edgeTextColors[1] = GrColor.Default;
            edgeTextColors[2] = GrColor.Default;
            edgeTextColors[3] = GrColor.Default;
        }

        public void Reset()
        {
            Dictionary<NodeType, bool> changedNodeTypes = new Dictionary<NodeType, bool>();

            excludedNodeTypes.Clear();
            excludedEdgeTypes.Clear();
            groupNodeTypes.Clear();
            nodeTypeToGroupNodeType.Clear();

            // Collect changed node types and clear property arrays
            foreach(NodeType type in nodeTypeColors.Keys)
                changedNodeTypes[type] = true;
            nodeTypeColors.Clear();
            foreach(NodeType type in nodeTypeBorderColors.Keys)
                changedNodeTypes[type] = true;
            nodeTypeBorderColors.Clear();
            foreach(NodeType type in nodeTypeTextColors.Keys)
                changedNodeTypes[type] = true;
            nodeTypeTextColors.Clear();
            foreach(NodeType type in nodeTypeShapes.Keys)
                changedNodeTypes[type] = true;
            nodeTypeShapes.Clear();

            // Announce changed node types
            foreach(NodeType type in nodeTypeShapes.Keys)
                NodeTypeAppearanceChanged(type);

            changedNodeTypes.Clear();

            Dictionary<EdgeType, bool> changedEdgeTypes = new Dictionary<EdgeType, bool>();

            // Collect changed edge types and clear property arrays
            foreach(EdgeType type in edgeTypeColors.Keys)
                changedEdgeTypes[type] = true;
            edgeTypeColors.Clear();
            foreach(EdgeType type in edgeTypeTextColors.Keys)
                changedEdgeTypes[type] = true;
            edgeTypeTextColors.Clear();

            // Announce changed edge types
            foreach(EdgeType type in changedEdgeTypes.Keys)
                EdgeTypeAppearanceChanged(type);

            foreach(EdgeType type in infoTags.Keys)
                TypeInfotagsChanged(type);
            infoTags.Clear();

            InitDumpColors();
        }
    }
}
