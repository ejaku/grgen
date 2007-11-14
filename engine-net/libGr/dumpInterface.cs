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
    public delegate void TypeAppearanceChangedHandler(IType type);
    public delegate void TypeInfotagsChangedHandler(IType type);

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

        Dictionary<IType, bool> excludedNodeTypes = new Dictionary<IType, bool>();
        Dictionary<IType, bool> excludedEdgeTypes = new Dictionary<IType, bool>();
        List<IType> groupNodeTypes = new List<IType>();
        Dictionary<IType, GrColor> nodeTypeColors = new Dictionary<IType, GrColor>();
        Dictionary<IType, GrColor> nodeTypeBorderColors = new Dictionary<IType, GrColor>();
        Dictionary<IType, GrColor> nodeTypeTextColors = new Dictionary<IType, GrColor>();
        Dictionary<IType, GrNodeShape> nodeTypeShapes = new Dictionary<IType, GrNodeShape>();
        Dictionary<IType, GrColor> edgeTypeColors = new Dictionary<IType, GrColor>();
        Dictionary<IType, GrColor> edgeTypeTextColors = new Dictionary<IType, GrColor>();
        Dictionary<IType, List<AttributeType>> infoTags = new Dictionary<IType, List<AttributeType>>();

        public IEnumerable<IType> ExcludedNodeTypes { get { return excludedNodeTypes.Keys; } }
        public IEnumerable<IType> ExcludedEdgeTypes { get { return excludedEdgeTypes.Keys; } }
        public List<IType> GroupNodeTypes { get { return groupNodeTypes; } }

        public IEnumerable<KeyValuePair<IType, GrColor>> NodeTypeColors { get { return nodeTypeColors; } }
        public IEnumerable<KeyValuePair<IType, GrColor>> NodeTypeBorderColors { get { return nodeTypeBorderColors; } }
        public IEnumerable<KeyValuePair<IType, GrColor>> NodeTypeTextColors { get { return nodeTypeTextColors; } }
        public IEnumerable<KeyValuePair<IType, GrNodeShape>> NodeTypeShapes { get { return nodeTypeShapes; } }
        public IEnumerable<KeyValuePair<IType, GrColor>> EdgeTypeColors { get { return edgeTypeColors; } }
        public IEnumerable<KeyValuePair<IType, GrColor>> EdgeTypeTextColors { get { return edgeTypeTextColors; } }

        public IEnumerable<KeyValuePair<IType, List<AttributeType>>> InfoTags { get { return infoTags; } }

        private ElementNameGetter elementNameGetter;

        public event TypeAppearanceChangedHandler OnNodeTypeAppearanceChanged;
        public event TypeAppearanceChangedHandler OnEdgeTypeAppearanceChanged;
        public event TypeInfotagsChangedHandler OnTypeInfotagsChanged;

        private void NodeTypeAppearanceChanged(IType type)
        {
            TypeAppearanceChangedHandler handler = OnNodeTypeAppearanceChanged;
            if(handler != null) handler(type);
        }

        private void EdgeTypeAppearanceChanged(IType type)
        {
            TypeAppearanceChangedHandler handler = OnEdgeTypeAppearanceChanged;
            if(handler != null) handler(type);
        }

        private void TypeInfotagsChanged(IType type)
        {
            TypeInfotagsChangedHandler handler = OnTypeInfotagsChanged;
            if (handler != null) handler(type);
        }

        public DumpInfo(ElementNameGetter nameGetter)
        {
            elementNameGetter = nameGetter;
            InitDumpColors();
        }

        public void ExcludeNodeType(IType nodeType)
        {
            excludedNodeTypes.Add(nodeType, true);
        }

        public bool IsExcludedNodeType(IType nodeType)
        {
            return excludedNodeTypes.ContainsKey(nodeType);
        }

        public void ExcludeEdgeType(IType edgeType)
        {
            excludedEdgeTypes.Add(edgeType, true);
        }

        public bool IsExcludedEdgeType(IType edgeType)
        {
            return excludedEdgeTypes.ContainsKey(edgeType);
        }

        public void GroupNodes(IType nodeType)
        {
            GroupNodeTypes.Add(nodeType);
        }

        public GrColor GetNodeTypeColor(IType nodeType)
        {
            GrColor col;
            if(!nodeTypeColors.TryGetValue(nodeType, out col))
                return GetNodeDumpTypeColor(GrElemDumpType.Normal);
            return col;
        }

        public GrColor GetNodeTypeBorderColor(IType nodeType)
        {
            GrColor col;
            if(!nodeTypeBorderColors.TryGetValue(nodeType, out col))
                return GetNodeDumpTypeBorderColor(GrElemDumpType.Normal);
            return col;
        }

        public GrColor GetNodeTypeTextColor(IType nodeType)
        {
            GrColor col;
            if(!nodeTypeTextColors.TryGetValue(nodeType, out col))
                return GetNodeDumpTypeTextColor(GrElemDumpType.Normal);
            return col;
        }

        public GrNodeShape GetNodeTypeShape(IType nodeType)
        {
            GrNodeShape shape;
            if(!nodeTypeShapes.TryGetValue(nodeType, out shape))
                return GrNodeShape.Default;
            return shape;
        }

        public GrColor GetEdgeTypeColor(IType edgeType)
        {
            GrColor col;
            if(!edgeTypeColors.TryGetValue(edgeType, out col))
                return GetEdgeDumpTypeColor(GrElemDumpType.Normal);
            return col;
        }

        public GrColor GetEdgeTypeTextColor(IType edgeType)
        {
            GrColor col;
            if(!edgeTypeTextColors.TryGetValue(edgeType, out col))
                return GetEdgeDumpTypeTextColor(GrElemDumpType.Normal);
            return col;
        }

        public void SetNodeTypeColor(IType nodeType, GrColor color)
        {
            nodeTypeColors[nodeType] = color;               // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetNodeTypeBorderColor(IType nodeType, GrColor color)
        {
            nodeTypeBorderColors[nodeType] = color;         // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetNodeTypeTextColor(IType nodeType, GrColor color)
        {
            nodeTypeTextColors[nodeType] = color;           // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetNodeTypeShape(IType nodeType, GrNodeShape shape)
        {
            nodeTypeShapes[nodeType] = shape;               // overwrites existing mapping
            NodeTypeAppearanceChanged(nodeType);
        }

        public void SetEdgeTypeColor(IType edgeType, GrColor color)
        {
            edgeTypeColors[edgeType] = color;               // overwrites existing mapping
            EdgeTypeAppearanceChanged(edgeType);
        }

        public void SetEdgeTypeTextColor(IType edgeType, GrColor color)
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
        public List<AttributeType> GetTypeInfoTags(IType type)
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
        public void AddTypeInfoTag(IType type, AttributeType attrType)
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
            Dictionary<IType, bool> changedTypes = new Dictionary<IType, bool>();

            excludedNodeTypes.Clear();
            excludedEdgeTypes.Clear();
            GroupNodeTypes.Clear();

            // Collect changed node types and clear property arrays
            foreach(IType type in nodeTypeColors.Keys)
                changedTypes[type] = true;
            nodeTypeColors.Clear();
            foreach(IType type in nodeTypeBorderColors.Keys)
                changedTypes[type] = true;
            nodeTypeBorderColors.Clear();
            foreach(IType type in nodeTypeTextColors.Keys)
                changedTypes[type] = true;
            nodeTypeTextColors.Clear();
            foreach(IType type in nodeTypeShapes.Keys)
                changedTypes[type] = true;
            nodeTypeShapes.Clear();

            // Announce changed node types
            foreach(IType type in changedTypes.Keys)
                NodeTypeAppearanceChanged(type);

            changedTypes.Clear();

            // Collect changed edge types and clear property arrays
            foreach(IType type in edgeTypeColors.Keys)
                changedTypes[type] = true;
            edgeTypeColors.Clear();
            foreach(IType type in edgeTypeTextColors.Keys)
                changedTypes[type] = true;
            edgeTypeTextColors.Clear();

            // Announce changed edge types
            foreach(IType type in changedTypes.Keys)
                EdgeTypeAppearanceChanged(type);

            foreach(IType type in infoTags.Keys)
                TypeInfotagsChanged(type);
            infoTags.Clear();

            InitDumpColors();
        }
    }
}