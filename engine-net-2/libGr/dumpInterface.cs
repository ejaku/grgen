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
    /// Deprecated...
    /// </summary>
    public interface IDumperFactory
    {
        /// <summary>
        /// Creates a new IDumper instance using the given name for the output file.
        /// The filename should be created out of this name by optionally prepanding path information
        /// and appending an extension.
        /// </summary>
        IDumper CreateDumper(String name);

        DumpInfo DumpInfo { get; set; }
    }

    /// <summary>
    /// A dumper for output of graphs.
    /// </summary>
    public interface IDumper : IDisposable
    {
        /// <summary>
        /// Dump a node
        /// </summary>
        /// <param name="node">The node to be dumped</param>
        /// <param name="label">The label to use for the node</param>
        /// <param name="attributes">An enumerable of attribute strings</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="nodeColor">The color of the node</param>
        /// <param name="borderColor">The color of the node border</param>
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
    public delegate void TypeAppearanceChangedHandler(GrGenType type);
    public delegate void TypeInfotagsChangedHandler(GrGenType type);

    /// <summary>
    /// A description of how to dump a graph.
    /// </summary>
    public class DumpInfo
    {
        GrColor[] nodeColors = new GrColor[4];
        GrColor[] nodeBorderColors = new GrColor[4];
        GrColor[] edgeColors = new GrColor[4];
        GrColor[] nodeTextColors = new GrColor[4];
        GrColor[] edgeTextColors = new GrColor[4];

        Dictionary<NodeType, bool> excludedNodeTypes = new Dictionary<NodeType, bool>();
        Dictionary<EdgeType, bool> excludedEdgeTypes = new Dictionary<EdgeType, bool>();
        List<NodeType> groupNodeTypes = new List<NodeType>();
        Dictionary<NodeType, GrColor> nodeTypeColors = new Dictionary<NodeType, GrColor>();
        Dictionary<NodeType, GrColor> nodeTypeBorderColors = new Dictionary<NodeType, GrColor>();
        Dictionary<NodeType, GrColor> nodeTypeTextColors = new Dictionary<NodeType, GrColor>();
        Dictionary<NodeType, GrNodeShape> nodeTypeShapes = new Dictionary<NodeType, GrNodeShape>();
        Dictionary<EdgeType, GrColor> edgeTypeColors = new Dictionary<EdgeType, GrColor>();
        Dictionary<EdgeType, GrColor> edgeTypeTextColors = new Dictionary<EdgeType, GrColor>();
        Dictionary<GrGenType, List<AttributeType>> infoTags = new Dictionary<GrGenType, List<AttributeType>>();

        public IEnumerable<NodeType> ExcludedNodeTypes { get { return excludedNodeTypes.Keys; } }
        public IEnumerable<EdgeType> ExcludedEdgeTypes { get { return excludedEdgeTypes.Keys; } }
        public List<NodeType> GroupNodeTypes { get { return groupNodeTypes; } }

        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeColors { get { return nodeTypeColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeBorderColors { get { return nodeTypeBorderColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrColor>> NodeTypeTextColors { get { return nodeTypeTextColors; } }
        public IEnumerable<KeyValuePair<NodeType, GrNodeShape>> NodeTypeShapes { get { return nodeTypeShapes; } }
        public IEnumerable<KeyValuePair<EdgeType, GrColor>> EdgeTypeColors { get { return edgeTypeColors; } }
        public IEnumerable<KeyValuePair<EdgeType, GrColor>> EdgeTypeTextColors { get { return edgeTypeTextColors; } }

        public IEnumerable<KeyValuePair<GrGenType, List<AttributeType>>> InfoTags { get { return infoTags; } }

        private ElementNameGetter elementNameGetter;

        public event TypeAppearanceChangedHandler OnNodeTypeAppearanceChanged;
        public event TypeAppearanceChangedHandler OnEdgeTypeAppearanceChanged;
        public event TypeInfotagsChangedHandler OnTypeInfotagsChanged;

        private void NodeTypeAppearanceChanged(NodeType type)
        {
            TypeAppearanceChangedHandler handler = OnNodeTypeAppearanceChanged;
            if(handler != null) handler(type);
        }

        private void EdgeTypeAppearanceChanged(EdgeType type)
        {
            TypeAppearanceChangedHandler handler = OnEdgeTypeAppearanceChanged;
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

        public void GroupNodes(NodeType nodeType)
        {
            GroupNodeTypes.Add(nodeType);
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
        /// Maps an IAttributeType to an IType or unmaps the IType, if attrType is null
        /// </summary>
        /// <param name="nodeType">The IType to be mapped/unmapped</param>
        /// <param name="attrType">The IAttributeType</param>
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
            GroupNodeTypes.Clear();

            // Collect changed node types and clear property arrays
            foreach(NodeType type in nodeTypeColors.Keys)
                changedTypes[type] = true;
            nodeTypeColors.Clear();
            foreach(NodeType type in nodeTypeBorderColors.Keys)
                changedTypes[type] = true;
            nodeTypeBorderColors.Clear();
            foreach(NodeType type in nodeTypeTextColors.Keys)
                changedTypes[type] = true;
            nodeTypeTextColors.Clear();
            foreach(NodeType type in nodeTypeShapes.Keys)
                changedTypes[type] = true;
            nodeTypeShapes.Clear();

            // Announce changed node types
            foreach(NodeType type in changedTypes.Keys)
                NodeTypeAppearanceChanged(type);

            changedTypes.Clear();

            Dictionary<EdgeType, bool> changedEdgeTypes = new Dictionary<EdgeType, bool>();

            // Collect changed edge types and clear property arrays
            foreach(EdgeType type in edgeTypeColors.Keys)
                changedTypes[type] = true;
            edgeTypeColors.Clear();
            foreach(EdgeType type in edgeTypeTextColors.Keys)
                changedTypes[type] = true;
            edgeTypeTextColors.Clear();

            // Announce changed edge types
            foreach(EdgeType type in changedTypes.Keys)
                EdgeTypeAppearanceChanged(type);

            foreach(EdgeType type in infoTags.Keys)
                TypeInfotagsChanged(type);
            infoTags.Clear();

            InitDumpColors();
        }
    }
}