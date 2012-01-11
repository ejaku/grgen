/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
        Turquoise, Aquamarine, Khaki, Pink, Orange, Orchid, LightYellow, YellowGreen
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
        Continuous, Dotted, Dashed, Invisible,
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
        /// <param name="thickness">The thickness of the edge</param>
        void DumpEdge(INode srcNode, INode tgtNode, String label, IEnumerable<String> attributes,
            GrColor textColor, GrColor edgeColor, GrLineStyle lineStyle, int thickness);

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
    /// Represents an info tag.
    /// </summary>
    public class InfoTag
    {
        /// <summary>
        /// The attribute to be shown.
        /// </summary>
        public AttributeType AttributeType;

        /// <summary>
        /// Whether this is a short info tag (no attribute name is shown).
        /// </summary>
        public bool ShortInfoTag;

        /// <summary>
        /// Initializes an info tag.
        /// </summary>
        /// <param name="attrType">The attribute to be shown.</param>
        /// <param name="shortInfoTag">Whether this is a short info tag (no attribute name is shown).</param>
        public InfoTag(AttributeType attrType, bool shortInfoTag)
        {
            AttributeType = attrType;
            ShortInfoTag = shortInfoTag;
        }
    }


    /// <summary>
    /// A description of how to dump a graph.
    /// </summary>
    public class DumpInfo
    {
        /// <summary>
        /// Used as priorities for GroupNodeType objects.
        /// </summary>
        int nextGroupID = 0;

        GrColor[] nodeColors = new GrColor[4];
        GrColor[] nodeBorderColors = new GrColor[4];
        GrColor[] nodeTextColors = new GrColor[4];
        GrNodeShape[] nodeShapes = new GrNodeShape[4];
        GrColor[] edgeColors = new GrColor[4];
        GrColor[] edgeTextColors = new GrColor[4];
        GrLineStyle[] edgeLineStyles = new GrLineStyle[4];
        int[] edgeThicknesses = new int[4];

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
        Dictionary<EdgeType, GrLineStyle> edgeTypeLineStyles = new Dictionary<EdgeType, GrLineStyle>();
        Dictionary<EdgeType, int> edgeTypeThicknesses = new Dictionary<EdgeType, int>();
        Dictionary<GrGenType, String> elemTypeLabel = new Dictionary<GrGenType, String>();
        Dictionary<GrGenType, List<InfoTag>> infoTags = new Dictionary<GrGenType, List<InfoTag>>();

        public IEnumerable<NodeType> ExcludedNodeTypes { get { return excludedNodeTypes.Keys; } }
        public IEnumerable<EdgeType> ExcludedEdgeTypes { get { return excludedEdgeTypes.Keys; } }
        public IEnumerable<GroupNodeType> GroupNodeTypes { get { return groupNodeTypes; } }

        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeColors { get { return nodeTypeColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeBorderColors { get { return nodeTypeBorderColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeTextColors { get { return nodeTypeTextColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrNodeShape>> NodeTypeShapes { get { return nodeTypeShapes; } }
        public IEnumerable<KeyValuePair<EdgeType, GrColor>> EdgeTypeColors { get { return edgeTypeColors; } }
        public IEnumerable<KeyValuePair<EdgeType, GrColor>> EdgeTypeTextColors { get { return edgeTypeTextColors; } }
        public IEnumerable<KeyValuePair<EdgeType, GrLineStyle>> EdgeTypeLineStyles { get { return edgeTypeLineStyles; } }
        public IEnumerable<KeyValuePair<EdgeType, int>> EdgeTypeThicknesses { get { return edgeTypeThicknesses; } }

        public IEnumerable<KeyValuePair<GrGenType, List<InfoTag>>> InfoTags { get { return infoTags; } }

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
        /// Sets the labels of the given element type.
        /// null is the default case, which is "&lt;elemname&gt;:&lt;type&gt;".
        /// </summary>
        /// <param name="type">The element type.</param>
        /// <param name="label">The label or null for the default case.</param>
        public void SetElemTypeLabel(GrGenType type, String label)
        {
            elemTypeLabel[type] = label;
        }

        /// <summary>
        /// Returns the label of the given element type or null for the default case.
        /// </summary>
        /// <param name="type">The element type.</param>
        /// <returns>The label or null.</returns>
        public String GetElemTypeLabel(GrGenType type)
        {
            String res;
            if(!elemTypeLabel.TryGetValue(type, out res))
                return null;
            return res;
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
        /// <param name="incNodeType">The incident node type according to the edge.</param>
        /// <param name="exactIncNodeType">True, if the incident node type must be exact, false, if also subtypes are allowed.</param>
        /// <param name="groupMode">Specifies how the edge is used for grouping.</param>
        public void AddOrExtendGroupNodeType(NodeType nodeType, bool exactNodeType, EdgeType edgeType, bool exactEdgeType,
                NodeType incNodeType, bool exactIncNodeType, GroupMode groupMode)
        {
            foreach(NodeType subType in nodeType.SubOrSameTypes)
            {
                GroupNodeType groupNodeType;
                if(!nodeTypeToGroupNodeType.TryGetValue(subType, out groupNodeType))
                {
                    groupNodeType = new GroupNodeType(subType, nextGroupID--);
                    nodeTypeToGroupNodeType[subType] = groupNodeType;
                    groupNodeTypes.Add(groupNodeType);
                }
                groupNodeType.SetEdgeGroupMode(edgeType, exactEdgeType, incNodeType, exactIncNodeType, groupMode);

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

        public GrLineStyle GetEdgeTypeLineStyle(EdgeType edgeType)
        {
            GrLineStyle style;
            if(!edgeTypeLineStyles.TryGetValue(edgeType, out style))
                return GetEdgeDumpTypeLineStyle(GrElemDumpType.Normal);
            return style;
        }

        public int GetEdgeTypeThickness(EdgeType edgeType)
        {
            int thickness;
            if(!edgeTypeThicknesses.TryGetValue(edgeType, out thickness))
                return GetEdgeDumpTypeThickness(GrElemDumpType.Normal);
            return thickness;
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

        public void SetEdgeTypeLineStyle(EdgeType edgeType, GrLineStyle style)
        {
            edgeTypeLineStyles[edgeType] = style;           // overwrites existing mapping
            EdgeTypeAppearanceChanged(edgeType);
        }

        public void SetEdgeTypeThickness(EdgeType edgeType, int thickness)
        {
            edgeTypeThicknesses[edgeType] = thickness;      // overwrites existing mapping
            EdgeTypeAppearanceChanged(edgeType);
        }

        public void SetNodeDumpTypeColor(GrElemDumpType type, GrColor color)
        {
            nodeColors[(int) type] = color;
        }

        public void SetNodeDumpTypeShape(GrElemDumpType type, GrNodeShape shape)
        {
            nodeShapes[(int) type] = shape;
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

        public void SetEdgeDumpTypeLineStyle(GrElemDumpType type, GrLineStyle style)
        {
            edgeLineStyles[(int) type] = style;
        }

        public void SetEdgeDumpTypeThickness(GrElemDumpType type, int thickness)
        {
            edgeThicknesses[(int) type] = thickness;
        }

        public GrColor GetNodeDumpTypeColor(GrElemDumpType type)
        {
            return nodeColors[(int) type];
        }

        public GrNodeShape GetNodeDumpTypeShape(GrElemDumpType type)
        {
            return nodeShapes[(int) type];
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

        public GrLineStyle GetEdgeDumpTypeLineStyle(GrElemDumpType type)
        {
            return edgeLineStyles[(int) type];
        }

        public int GetEdgeDumpTypeThickness(GrElemDumpType type)
        {
            return edgeThicknesses[(int) type];
        }

        /// <summary>
        /// Returns a list of InfoTag objects for the given GrGenType or null.
        /// </summary>
        /// <param name="type">The GrGenType to be examined.</param>
        /// <returns>A list of associated InfoTag objects or null.</returns>
        public List<InfoTag> GetTypeInfoTags(GrGenType type)
        {
            List<InfoTag> typeInfoTags;
            if(!infoTags.TryGetValue(type, out typeInfoTags)) return null;
            else return typeInfoTags;
        }

        /// <summary>
        /// Associates an InfoTag to a GrGenType.
        /// </summary>
        /// <param name="type">The GrGenType to given an InfoTag</param>
        /// <param name="infotag">The InfoTag</param>
        public void AddTypeInfoTag(GrGenType type, InfoTag infoTag)
        {
            List<InfoTag> typeInfoTags;
            if(!infoTags.TryGetValue(type, out typeInfoTags))
            {
                infoTags[type] = typeInfoTags = new List<InfoTag>();
            }
            typeInfoTags.Add(infoTag);
            TypeInfotagsChanged(type);
        }

        /// <summary>
        /// Returns an info tag with the given AttributeType registered for the given element type or null.
        /// </summary>
        /// <param name="type">The element type.</param>
        /// <param name="attrType">The attribute type.</param>
        /// <returns>The info tag or null.</returns>
        public InfoTag GetTypeInfoTag(GrGenType type, AttributeType attrType)
        {
            List<InfoTag> typeInfoTags;
            if(infoTags.TryGetValue(type, out typeInfoTags))
            {
                foreach(InfoTag infotag in typeInfoTags)
                {
                    if(infotag.AttributeType == attrType) return infotag;
                }
            }
            return null;
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

            edgeLineStyles[0] = GrLineStyle.Default;
            edgeLineStyles[1] = GrLineStyle.Default;
            edgeLineStyles[2] = GrLineStyle.Default;
            edgeLineStyles[3] = GrLineStyle.Default;

            edgeThicknesses[0] = 1;
            edgeThicknesses[1] = 1;
            edgeThicknesses[2] = 1;
            edgeThicknesses[3] = 1;

            nodeShapes[0] = GrNodeShape.Default;
            nodeShapes[1] = GrNodeShape.Default;
            nodeShapes[2] = GrNodeShape.Default;
            nodeShapes[3] = GrNodeShape.Default;
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
            foreach(EdgeType type in edgeTypeLineStyles.Keys)
                changedEdgeTypes[type] = true;
            edgeTypeLineStyles.Clear();
            foreach(EdgeType type in edgeTypeThicknesses.Keys)
                changedEdgeTypes[type] = true;
            edgeTypeThicknesses.Clear();

            // Announce changed edge types
            foreach(EdgeType type in changedEdgeTypes.Keys)
                EdgeTypeAppearanceChanged(type);

            foreach(GrGenType type in infoTags.Keys)
                TypeInfotagsChanged(type);
            infoTags.Clear();

            InitDumpColors();
        }
    }
}
