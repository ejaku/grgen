/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace de.unika.ipd.grGen.grShell
{
    public delegate void ConnectionLostHandler();

    class NodeRealizer : IEquatable<NodeRealizer>
    {
        public String Name;
        public GrColor Color;
        public GrColor BorderColor;
        public GrColor TextColor;
        public GrNodeShape Shape;

        public NodeRealizer(String name, GrColor color, GrColor borderColor, GrColor textColor, GrNodeShape shape)
        {
            Name = name;
            Color = color;
            BorderColor = borderColor;
            TextColor = textColor;
            Shape = shape;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode() ^ BorderColor.GetHashCode() ^ Shape.GetHashCode() ^ TextColor.GetHashCode();
        }

        public bool Equals(NodeRealizer other)
        {
            return Color == other.Color && BorderColor == other.BorderColor && Shape == other.Shape && TextColor == other.TextColor;
        }
    }

    class EdgeRealizer : IEquatable<EdgeRealizer>
    {
        public String Name;
        public GrColor Color;
        public GrColor TextColor;
        public GrLineStyle LineStyle;
        public int LineWidth;

        public EdgeRealizer(String name, GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            Name = name;
            Color = color;
            TextColor = textColor;
            LineWidth = lineWidth;
            LineStyle = lineStyle;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode() ^ TextColor.GetHashCode() ^ LineStyle.GetHashCode() ^ LineWidth.GetHashCode();
        }

        public bool Equals(EdgeRealizer other)
        {
            return Color == other.Color && TextColor == other.TextColor && LineStyle == other.LineStyle && LineWidth == other.LineWidth;
        }
    }

    public class YCompClient
    {
        TcpClient ycompClient;
        YCompStream ycompStream;
        IGraph graph;
        String nodeRealizer = null;
        String edgeRealizer = null;
        DumpInfo dumpInfo;

        bool isDirty = false;
        bool isLayoutDirty = false;

        Dictionary<IEdge, bool> hiddenEdges = new Dictionary<IEdge,bool>();

        Dictionary<NodeRealizer, NodeRealizer> nodeRealizers = new Dictionary<NodeRealizer, NodeRealizer>();
        int nextNodeRealizerID = 5;

        Dictionary<EdgeRealizer, EdgeRealizer> edgeRealizers = new Dictionary<EdgeRealizer, EdgeRealizer>();
        int nextEdgeRealizerID = 5;

        public readonly String NormalNodeRealizer, MatchedNodeRealizer, NewNodeRealizer, DeletedNodeRealizer, RetypedNodeRealizer;
        public readonly String NormalEdgeRealizer, MatchedEdgeRealizer, NewEdgeRealizer, DeletedEdgeRealizer, RetypedEdgeRealizer;

        private static Dictionary<String, bool> availableLayouts;

        public static IEnumerable<String> AvailableLayouts
        {
            get { return availableLayouts.Keys; }
        }

        public static bool IsValidLayout(String layoutName)     // TODO: allow case insensitive layout name
        {
            return availableLayouts.ContainsKey(layoutName);
        }

        static YCompClient()
        {
            availableLayouts = new Dictionary<string, bool>();
            availableLayouts.Add("Random", true);
            availableLayouts.Add("Hierarchic", true);
            availableLayouts.Add("Organic", true);
            availableLayouts.Add("Orthogonal", true);
            availableLayouts.Add("Circular", true);
            availableLayouts.Add("Tree", true);
            availableLayouts.Add("Diagonal", true);
            availableLayouts.Add("Incremental Hierarchic", true);
            availableLayouts.Add("Compilergraph", true);
        }

        public event ConnectionLostHandler OnConnectionLost
        { add { ycompStream.OnConnectionLost += value; } remove { ycompStream.OnConnectionLost -= value; } }
            

        /// <summary>
        /// If non-null, overrides the type dependend node realizer
        /// </summary>
        public String NodeRealizer { get { return nodeRealizer; } set { nodeRealizer = value; } }

        /// <summary>
        /// If non-null, overrides the type dependend edge realizer
        /// </summary>
        public String EdgeRealizer { get { return edgeRealizer; } set { edgeRealizer = value; } }

        public IGraph Graph { get { return graph; } }

        class YCompStream
        {
            NetworkStream stream;
            byte[] readBuffer = new byte[4096];
            bool closing = false;

            public event ConnectionLostHandler OnConnectionLost;

            public YCompStream(TcpClient client)
            {
                stream = client.GetStream();
            }

            public void Write(String message)
            {
                try
                {
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
                catch(Exception)
                {
                    stream = null;
                    if(closing) return;
                    ConnectionLostHandler handler = OnConnectionLost;
                    if(handler != null) handler();
                }
            }

            /// <summary>
            /// Reads up to 4096 bytes from the stream
            /// </summary>
            /// <returns>The read bytes converted to a String using ASCII encoding</returns>
            public String Read()
            {
                try
                {
                    int bytesRead = stream.Read(readBuffer, 0, 4096);
                    return Encoding.ASCII.GetString(readBuffer, 0, bytesRead);
                }
                catch(Exception)
                {
                    stream = null;
                    ConnectionLostHandler handler = OnConnectionLost;
                    if(handler != null) handler();
                    return null;
                }
            }

            public bool Ready
            {
                get
                {
                    try
                    {
                        return stream.DataAvailable;
                    }
                    catch(Exception)
                    {
                        stream = null;
                        ConnectionLostHandler handler = OnConnectionLost;
                        if(handler != null) handler();
                        return false;
                    }
                }
            }

            public bool IsStreamOpen { get { return stream != null; } }
            public bool Closing { get { return closing; } set { closing = value; } }
        }

        /// <summary>
        /// Creates a new YCompClient instance and connects to the local YComp server.
        /// If it is not available a SocketException is thrown
        /// </summary>
        public YCompClient(IGraph graph, String layoutModule, int connectionTimeout, int port, DumpInfo dumpInfo)
        {
            this.graph = graph;
            this.dumpInfo = dumpInfo;

            int startTime = Environment.TickCount;

            do
            {
                try
                {
                    ycompClient = new TcpClient("localhost", port);
                }
                catch(SocketException)
                {
                    ycompClient = null;
                    Thread.Sleep(1000);
                }
            } while(ycompClient == null && Environment.TickCount - startTime < connectionTimeout);

            if(ycompClient == null)
                throw new Exception("Connection timeout!");

            ycompStream = new YCompStream(ycompClient);
            ycompStream.Write("identifyClient \"" + GrShellImpl.VersionString + "\"\n");

            SetLayout(layoutModule);

            NormalNodeRealizer = GetNodeRealizer(GrColor.Yellow, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            MatchedNodeRealizer = GetNodeRealizer(GrColor.Khaki, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            NewNodeRealizer = GetNodeRealizer(GrColor.LightRed, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            DeletedNodeRealizer = GetNodeRealizer(GrColor.LightGrey, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            RetypedNodeRealizer = GetNodeRealizer(GrColor.Cyan, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);

            NormalEdgeRealizer = GetEdgeRealizer(GrColor.DarkYellow, GrColor.Black, 1, GrLineStyle.Solid);
            MatchedEdgeRealizer = GetEdgeRealizer(GrColor.DarkYellow, GrColor.Black, 2, GrLineStyle.Solid);
            NewEdgeRealizer = GetEdgeRealizer(GrColor.LightRed, GrColor.Black, 2, GrLineStyle.Solid);
            DeletedEdgeRealizer = GetEdgeRealizer(GrColor.LightGrey, GrColor.Black, 2, GrLineStyle.Solid);
            RetypedEdgeRealizer = GetEdgeRealizer(GrColor.Cyan, GrColor.Black, 2, GrLineStyle.Solid);

            dumpInfo.OnNodeTypeAppearanceChanged += new NodeTypeAppearanceChangedHandler(OnNodeTypeAppearanceChanged);
            dumpInfo.OnEdgeTypeAppearanceChanged += new EdgeTypeAppearanceChangedHandler(OnEdgeTypeAppearanceChanged);
            dumpInfo.OnTypeInfotagsChanged += new TypeInfotagsChangedHandler(OnTypeInfotagsChanged);

            // TODO: Add group related events
        }

        /// <summary>
        /// Creates a new YCompClient instance and connects to the local YComp server.
        /// If it is not available a SocketException is thrown
        /// </summary>
        public YCompClient(IGraph graph, String layoutModule, int connectionTimeout, int port)
            : this(graph, layoutModule, connectionTimeout, port, new DumpInfo(graph.GetElementName)) { }


        public bool CommandAvailable { get { return ycompStream.Ready; } }

        public String ReadCommand()
        {
            return ycompStream.Read();
        }

        String GetNodeRealizer(GrColor nodeColor, GrColor borderColor, GrColor textColor, GrNodeShape shape)
        {
            NodeRealizer newNr = new NodeRealizer("nr" + nextNodeRealizerID, nodeColor, borderColor, textColor, shape);

            NodeRealizer nr;
            if (!nodeRealizers.TryGetValue(newNr, out nr))
            {
                ycompStream.Write("addNodeRealizer \"" + newNr.Name + "\" \""
                    + VCGDumper.GetColor(borderColor) + "\" \""
                    + VCGDumper.GetColor(nodeColor) + "\" \""
                    + VCGDumper.GetColor(textColor) + "\" \""
                    + VCGDumper.GetNodeShape(shape) + "\"\n");
                nodeRealizers.Add(newNr, newNr);
                nextNodeRealizerID++;
                nr = newNr;
            }
            return nr.Name;
        }

        String GetNodeRealizer(NodeType type)
        {
            return GetNodeRealizer(dumpInfo.GetNodeTypeColor(type), dumpInfo.GetNodeTypeBorderColor(type),
                dumpInfo.GetNodeTypeTextColor(type), dumpInfo.GetNodeTypeShape(type));
        }

        String GetEdgeRealizer(GrColor edgeColor, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            EdgeRealizer newEr = new EdgeRealizer("er" + nextEdgeRealizerID, edgeColor, textColor, lineWidth, lineStyle);

            EdgeRealizer er;
            if(!edgeRealizers.TryGetValue(newEr, out er))
            {
                ycompStream.Write("addEdgeRealizer \"" + newEr.Name + "\" \""
                    + VCGDumper.GetColor(newEr.Color) + "\" \""
                    + VCGDumper.GetColor(newEr.TextColor) + "\" \""
                    + lineWidth + "\" \"continuous\"\n");
                edgeRealizers.Add(newEr, newEr);
                nextEdgeRealizerID++;
                er = newEr;
            }
            return er.Name;
        }

        String GetEdgeRealizer(EdgeType type)
        {
            return GetEdgeRealizer(dumpInfo.GetEdgeTypeColor(type), dumpInfo.GetEdgeTypeTextColor(type), 1, GrLineStyle.Solid);
        }

        void OnNodeTypeAppearanceChanged(NodeType type)
        {
            if(dumpInfo.IsExcludedNodeType(type)) return;

            String nr = GetNodeRealizer(type);
            foreach(INode node in graph.GetExactNodes(type))
                ChangeNode(node, nr);
            isDirty = true;
        }

        void OnEdgeTypeAppearanceChanged(EdgeType type)
        {
            if(dumpInfo.IsExcludedEdgeType(type)) return;

            String er = GetEdgeRealizer(type);
            foreach(IEdge edge in graph.GetExactEdges(type))
                ChangeEdge(edge, er);
            isDirty = true;
        }

        void OnTypeInfotagsChanged(GrGenType type)
        {
            if(type.IsNodeType)
            {
                if(dumpInfo.IsExcludedNodeType((NodeType) type)) return;

                foreach(INode node in graph.GetExactNodes((NodeType) type))
                    ycompStream.Write("setNodeLabel \"n" + dumpInfo.GetElementName(node) + "\" \"" + GetElemLabel(node) + "\"\n");
            }
            else
            {
                if(dumpInfo.IsExcludedEdgeType((EdgeType) type)) return;

                foreach(IEdge edge in graph.GetExactEdges((EdgeType) type))
                {
                    if(dumpInfo.IsExcludedEdgeType(edge.Type)
                        || dumpInfo.IsExcludedNodeType(edge.Source.Type)
                        || dumpInfo.IsExcludedNodeType(edge.Target.Type)) return;

                    ycompStream.Write("setEdgeLabel \"e" + dumpInfo.GetElementName(edge) + "\" \"" + GetElemLabel(edge) + "\"\n");
                }
            }
            isDirty = true;
        }

        /// <summary>
        /// Sets the current layouter of yComp
        /// </summary>
        /// <param name="moduleName">The name of the layouter.
        ///     Can be one of:
        ///     - Random
        ///     - Hierarchic
        ///     - Organic
        ///     - Orthogonal
        ///     - Circular
        ///     - Tree
        ///     - Diagonal
        ///     - Incremental Hierarchic
        ///     - Compilergraph
        /// </param>
        public void SetLayout(String moduleName)
        {
            ycompStream.Write("setLayout \"" + moduleName + "\"\n");
            isDirty = true;
            isLayoutDirty = true;
        }

        /// <summary>
        /// Retrieves the available options of the current layouter of yComp and the current values.
        /// </summary>
        /// <returns>A description of the available options of the current layouter of yComp
        /// and the current values.</returns>
        public String GetLayoutOptions()
        {
            ycompStream.Write("getLayoutOptions\n");
            String msg = "";
            do
            {
                msg += ycompStream.Read();
            }
            while(!msg.EndsWith("endoptions\n"));
            return msg.Substring(0, msg.Length - 11);       // remove "endoptions\n" from message
        }

        /// <summary>
        /// Sets a layout option of the current layouter of yComp.
        /// </summary>
        /// <param name="optionName">The name of the option.</param>
        /// <param name="optionValue">The new value.</param>
        /// <returns>Null, or a error message, if setting the option failed.</returns>
        public String SetLayoutOption(String optionName, String optionValue)
        {
            ycompStream.Write("setLayoutOption \"" + optionName + "\" \"" + optionValue + "\"\n");
            String msg = ycompStream.Read();
            if(msg == "optionset\n")
            {
                isDirty = true;
                isLayoutDirty = true;
                return null;
            }
            return msg;
        }

        /// <summary>
        /// Forces yComp to relayout the graph.
        /// </summary>
        public void ForceLayout()
        {
            ycompStream.Write("layout\n");
            isLayoutDirty = false;
            isDirty = false;
        }

        /// <summary>
        /// Relayouts the graph if needed.
        /// </summary>
        public void UpdateDisplay()
        {
            if(isLayoutDirty) ForceLayout();
            else if(isDirty)
            {
                ycompStream.Write("show\n");
                isDirty = false;
            }
        }

        /// <summary>
        /// Sends a "sync" request and waits for a "sync" answer
        /// </summary>
        public void Sync()
        {
            ycompStream.Write("sync\n");
            String answer = ycompStream.Read();
            if(answer != "sync\n")
                Console.WriteLine("Wrong sync answer received: \"" + answer + "\"!");
        }

        private String GetElemLabel(IGraphElement elem)
        {
            List<AttributeType> infoTagTypes = dumpInfo.GetTypeInfoTags(elem.Type);
            String infoTag = "";
            if(infoTagTypes != null)
            {
                foreach(AttributeType attrType in infoTagTypes)
                {
                    object attr = elem.GetAttribute(attrType.Name);
                    if(attr == null) continue;
                    infoTag += "\\n" + attrType.Name + " = " + attr.ToString();
                }
            }

            return dumpInfo.GetElementName(elem) + ":" + elem.Type.Name + infoTag;
        }

        private String GetElemLabelWithChangedAttr(IGraphElement elem, AttributeType changedAttrType, object newValue)
        {
            List<AttributeType> infoTagTypes = dumpInfo.GetTypeInfoTags(elem.Type);
            String infoTag = "";
            if(infoTagTypes != null)
            {
                foreach(AttributeType attrType in infoTagTypes)
                {
                    object attr;
                    if(attrType == changedAttrType) attr = newValue;
                    else attr = elem.GetAttribute(attrType.Name);
                    if(attr == null) continue;
                    infoTag += "\\n" + attrType.Name + " = " + attr.ToString();
                }
            }

            return dumpInfo.GetElementName(elem) + ":" + elem.Type.Name + infoTag;
        }


        public void AddNode(INode node)
        {
            if(dumpInfo.IsExcludedNodeType(node.Type)) return;

            String nrName = nodeRealizer ?? GetNodeRealizer(node.Type);

            String name = graph.GetElementName(node);
            if(dumpInfo.GetGroupNodeType(node.Type) != null)
                ycompStream.Write("addSubgraphNode \"-1\" \"n" + name + "\" \"" + nrName + "\" \"" + GetElemLabel(node) + "\"\n");
            else
                ycompStream.Write("addNode \"-1\" \"n" + name + "\" \"" + nrName + "\" \"" + GetElemLabel(node) + "\"\n");
            foreach(AttributeType attrType in node.Type.AttributeTypes)
            {
                object attr = node.GetAttribute(attrType.Name);
                String attrString = (attr != null) ? attr.ToString() : "<Not initialized>";
                ycompStream.Write("changeNodeAttr \"n" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + GetKindName(attrType) + "\" \"" + attrString + "\"\n");
            }
            isDirty = true;
            isLayoutDirty = true;
        }

        public void AddEdge(IEdge edge)
        {
            if(dumpInfo.IsExcludedEdgeType(edge.Type)
                || dumpInfo.IsExcludedNodeType(edge.Source.Type)
                || dumpInfo.IsExcludedNodeType(edge.Target.Type)) return;

            String erName = edgeRealizer ?? GetEdgeRealizer(edge.Type);

            String edgeName = graph.GetElementName(edge);
            String srcName = graph.GetElementName(edge.Source);
            String tgtName = graph.GetElementName(edge.Target);

            if(edge.Source != edge.Target)
            {
                GroupNodeType srcGroupNodeType = dumpInfo.GetGroupNodeType(edge.Source.Type);
                GroupNodeType tgtGroupNodeType = dumpInfo.GetGroupNodeType(edge.Target.Type);
                INode groupNodeFirst = null, groupNodeSecond = null;
                if(tgtGroupNodeType != null) groupNodeFirst = edge.Target;
                if(srcGroupNodeType != null)
                {
                    if(groupNodeFirst == null) groupNodeFirst = edge.Source;
                    else if(srcGroupNodeType.Priority > tgtGroupNodeType.Priority)
                    {
                        groupNodeSecond = groupNodeFirst;
                        groupNodeFirst = edge.Source;
                    }
                    else groupNodeSecond = edge.Source;
                }

                GroupMode grpMode = GroupMode.None;
                bool groupedNode = false;
                if(groupNodeFirst != null)
                {
                    groupedNode = TryGroupNode(groupNodeFirst, edge, srcName, tgtName, srcGroupNodeType,
                            tgtGroupNodeType, ref grpMode);
                    if(!groupedNode && groupNodeSecond != null)
                        groupedNode = TryGroupNode(groupNodeSecond, edge, srcName, tgtName, srcGroupNodeType,
                                tgtGroupNodeType, ref grpMode);
                }

                // If no grouping rule applies, grpMode is GroupMode.None (= 0)
                if((grpMode & GroupMode.Hidden) != 0)
                {
                    hiddenEdges[edge] = true;
                    isDirty = true;
                    isLayoutDirty = true;
                    return;
                }
            }

            ycompStream.Write("addEdge \"e" + edgeName + "\" \"n" + srcName + "\" \"n" + tgtName
                + "\" \"" + erName + "\" \"" + GetElemLabel(edge) + "\"\n");
            foreach(AttributeType attrType in edge.Type.AttributeTypes)
            {
                object attr = edge.GetAttribute(attrType.Name);
                String attrString = (attr != null) ? attr.ToString() : "<Not initialized>";
                ycompStream.Write("changeEdgeAttr \"e" + edgeName + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + GetKindName(attrType) + "\" \"" + attrString + "\"\n");
            }
            isDirty = true;
            isLayoutDirty = true;
        }

        private bool TryGroupNode(INode groupNode, IEdge edge, String srcName, String tgtName,
            GroupNodeType srcGroupNodeType, GroupNodeType tgtGroupNodeType, ref GroupMode grpMode)
        {
            if(groupNode == edge.Target)
            {
                grpMode = tgtGroupNodeType.GetEdgeGroupMode(edge.Type, edge.Source.Type);
                if((grpMode & GroupMode.GroupIncomingNodes) != 0)
                {
                    ycompStream.Write("moveNode \"n" + srcName + "\" \"n" + tgtName + "\"\n");
                    return true;
                }
            }
            else if(groupNode == edge.Source)
            {
                grpMode = srcGroupNodeType.GetEdgeGroupMode(edge.Type, edge.Target.Type);
                if((grpMode & GroupMode.GroupOutgoingNodes) != 0)
                {
                    ycompStream.Write("moveNode \"n" + tgtName + "\" \"n" + srcName + "\"\n");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Annotates the given element with the given string in double angle brackets
        /// </summary>
        /// <param name="elem">The element to be annotated</param>
        /// <param name="annotation">The annotation string or null, if the annotation is to be removed</param>
        public void AnnotateElement(IGraphElement elem, String annotation)
        {
            bool isNode = elem is INode;
            if(isNode)
            {
                if(dumpInfo.IsExcludedNodeType((NodeType) elem.Type)) return;
            }
            else
            {
                if(dumpInfo.IsExcludedEdgeType((EdgeType) elem.Type)) return;
            }
            

            String name = graph.GetElementName(elem);
            String elemKind = isNode ? "Node" : "Edge";
            String elemNamePrefix = isNode ? "n" : "e";
            ycompStream.Write("set" + elemKind + "Label \"" + elemNamePrefix + name + "\" \""
                + (annotation == null ? "" : "<<" + annotation + ">>\\n") + GetElemLabel(elem) + "\"\n");
            isDirty = true;
        }

        /// <summary>
        /// Sets the node realizer of the given node.
        /// If realizer is null, the realizer for the type of the node is used.
        /// </summary>
        public void ChangeNode(INode node, String realizer)
        {
            if(dumpInfo.IsExcludedNodeType(node.Type)) return;

            if(realizer == null) realizer = GetNodeRealizer(node.Type);
            String name = graph.GetElementName(node);
            ycompStream.Write("changeNode \"n" + name + "\" \"" + realizer + "\"\n");
            isDirty = true;
        }

        /// <summary>
        /// Sets the edge realizer of the given edge.
        /// If realizer is null, the realizer for the type of the edge is used.
        /// </summary>
        public void ChangeEdge(IEdge edge, String realizer)
        {
            if(dumpInfo.IsExcludedEdgeType(edge.Type)
                || dumpInfo.IsExcludedNodeType(edge.Source.Type)
                || dumpInfo.IsExcludedNodeType(edge.Target.Type)) return;
            if(hiddenEdges.ContainsKey(edge)) return;

            if(realizer == null) realizer = GetEdgeRealizer(edge.Type);
            String name = graph.GetElementName(edge);
            ycompStream.Write("changeEdge \"e" + name + "\" \"" + realizer + "\"\n");
            isDirty = true;
        }

        public void ChangeNodeAttribute(INode node, AttributeType attrType, String attrValue)
        {
            if(dumpInfo.IsExcludedNodeType(node.Type)) return;

            String name = graph.GetElementName(node);
            ycompStream.Write("changeNodeAttr \"n" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + GetKindName(attrType) + "\" \"" + attrValue + "\"\n");
            List<AttributeType> infotags = dumpInfo.GetTypeInfoTags(node.Type);
            if(infotags != null && infotags.Contains(attrType))
                ycompStream.Write("setNodeLabel \"n" + name + "\" \""
                    + GetElemLabelWithChangedAttr(node, attrType, attrValue) + "\"\n");
            isDirty = true;
        }

        public void ChangeEdgeAttribute(IEdge edge, AttributeType attrType, String attrValue)
        {
            if(dumpInfo.IsExcludedEdgeType(edge.Type)
                || dumpInfo.IsExcludedNodeType(edge.Source.Type)
                || dumpInfo.IsExcludedNodeType(edge.Target.Type)) return;
            if(hiddenEdges.ContainsKey(edge)) return;

            String name = graph.GetElementName(edge);
            ycompStream.Write("changeEdgeAttr \"e" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + GetKindName(attrType) + "\" \"" + attrValue + "\"\n");
            List<AttributeType> infotags = dumpInfo.GetTypeInfoTags(edge.Type);
            if(infotags != null && infotags.Contains(attrType))
                ycompStream.Write("setEdgeLabel \"e" + name + "\" \""
                    + GetElemLabelWithChangedAttr(edge, attrType, attrValue) + "\"\n");
            isDirty = true;
        }

        public void ClearNodeAttribute(INode node, AttributeType attrType)
        {
            if(dumpInfo.IsExcludedNodeType(node.Type)) return;

            String name = graph.GetElementName(node);
            ycompStream.Write("clearNodeAttr \"e" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + GetKindName(attrType) + "\"\n");
            List<AttributeType> infotags = dumpInfo.GetTypeInfoTags(node.Type);
            if(infotags != null && infotags.Contains(attrType))
                ycompStream.Write("setNodeLabel \"n" + name + "\" \"" + GetElemLabel(node) + "\"\n");
            isDirty = true;
        }

        public void ClearEdgeAttribute(IEdge edge, AttributeType attrType)
        {
            if(dumpInfo.IsExcludedEdgeType(edge.Type)
                || dumpInfo.IsExcludedNodeType(edge.Source.Type)
                || dumpInfo.IsExcludedNodeType(edge.Target.Type)) return;
            if(hiddenEdges.ContainsKey(edge)) return;

            String name = graph.GetElementName(edge);
            ycompStream.Write("clearEdgeAttr \"n" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + GetKindName(attrType) + "\"\n");
            List<AttributeType> infotags = dumpInfo.GetTypeInfoTags(edge.Type);
            if(infotags != null && infotags.Contains(attrType))
                ycompStream.Write("setEdgeLabel \"e" + name + "\" \"" + GetElemLabel(edge) + "\"\n");
            isDirty = true;
        }

        public void RetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
            bool isNode = oldElem is INode;
            GrGenType oldType = oldElem.Type;
            GrGenType newType = newElem.Type;

            // TODO: Add element, if old element was excluded, but new element is not
            if(isNode)
            {
                if(dumpInfo.IsExcludedNodeType((NodeType) oldType)) return;
            }
            else
            {
                IEdge oldEdge = (IEdge) oldElem;
                if(dumpInfo.IsExcludedEdgeType(oldEdge.Type)
                    || dumpInfo.IsExcludedNodeType(oldEdge.Source.Type)
                    || dumpInfo.IsExcludedNodeType(oldEdge.Target.Type)) return;
                if(hiddenEdges.ContainsKey(oldEdge)) return;       // TODO: Update group relation
            }

            String elemKind = isNode ? "Node" : "Edge";
            String elemNamePrefix = isNode ? "n" : "e";
            String oldName = elemNamePrefix + graph.GetElementName(oldElem);

            ycompStream.Write("set" + elemKind + "Label \"" + oldName + "\" \"" + GetElemLabel(newElem) + "\"\n");

            // remove the old attributes
            foreach(AttributeType attrType in oldType.AttributeTypes)
            {
                ycompStream.Write("clear" + elemKind + "Attr \"" + oldName + "\" \"" + attrType.OwnerType.Name + "::"
                    + attrType.Name + " : " + GetKindName(attrType) + "\"\n");
            }
            // set the new attributes
            foreach(AttributeType attrType in newType.AttributeTypes)
            {
                object attr = newElem.GetAttribute(attrType.Name);
                String attrString = (attr != null) ? attr.ToString() : "<Not initialized>";
                ycompStream.Write("change" + elemKind + "Attr \"" + oldName + "\" \"" + attrType.OwnerType.Name + "::"
                    + attrType.Name + " : " + GetKindName(attrType) + "\" \"" + attrString + "\"\n");
            }

            if(isNode)
            {
                String oldNr = GetNodeRealizer((NodeType) oldType);
                String newNr = GetNodeRealizer((NodeType) newType);
                if(oldNr != newNr)
                    ChangeNode((INode) oldElem, newNr);
            }
            else
            {
                String oldEr = GetEdgeRealizer((EdgeType) oldType);
                String newEr = GetEdgeRealizer((EdgeType) newType);
                if(oldEr != newEr)
                    ChangeEdge((IEdge) oldElem, newEr);
            }

            String newName = elemNamePrefix + graph.GetElementName(newElem);
            ycompStream.Write("rename" + elemKind + " \"" + oldName + "\" \"" + newName + "\"\n");

            isDirty = true;
        }

        public void DeleteNode(String nodeName)
        {
            ycompStream.Write("deleteNode \"n" + nodeName + "\"\n");
            isDirty = true;
            isLayoutDirty = true;
        }

        public void DeleteNode(INode node)
        {
            if(dumpInfo.IsExcludedNodeType(node.Type)) return;

            DeleteNode(graph.GetElementName(node));
        }

        public void DeleteEdge(String edgeName)
        {
            // TODO: Update group relation...

            ycompStream.Write("deleteEdge \"e" + edgeName + "\"\n");
            isDirty = true;
            isLayoutDirty = true;
        }

        public void DeleteEdge(IEdge edge)
        {
            if(hiddenEdges.ContainsKey(edge))
            {
                // TODO: Update group relation
                hiddenEdges.Remove(edge);
            }
            if(dumpInfo.IsExcludedEdgeType(edge.Type)
                || dumpInfo.IsExcludedNodeType(edge.Source.Type)
                || dumpInfo.IsExcludedNodeType(edge.Target.Type)) return;

            DeleteEdge(graph.GetElementName(edge));
        }

        public void RenameNode(String oldName, String newName)
        {
            ycompStream.Write("renameNode \"n" + oldName + "\" \"n" + newName + "\"\n");
        }

        public void RenameEdge(String oldName, String newName)
        {
            ycompStream.Write("renameEdge \"e" + oldName + "\" \"e" + newName + "\"\n");
        }

        public void ClearGraph()
        {
            ycompStream.Write("deleteGraph\n");
            isDirty = false;
            isLayoutDirty = false;
            hiddenEdges.Clear();
        }

        public void Close()
        {
            if(ycompStream.IsStreamOpen)
            {
                ycompStream.Closing = true;     // don't care if exit doesn't work
                ycompStream.Write("exit\n");
            }
            ycompClient.Close();
            ycompClient = null;

            dumpInfo.OnNodeTypeAppearanceChanged -= new NodeTypeAppearanceChangedHandler(OnNodeTypeAppearanceChanged);
            dumpInfo.OnEdgeTypeAppearanceChanged -= new EdgeTypeAppearanceChangedHandler(OnEdgeTypeAppearanceChanged);
        }

        /// <summary>
        /// Returns the name of the kind of the given attribute
        /// </summary>
        /// <param name="attrType">The IAttributeType</param>
        /// <returns>The name of the kind of the attribute</returns>
        private String GetKindName(AttributeType attrType)
        {
            switch(attrType.Kind)
            {
                case AttributeKind.IntegerAttr: return "int";
                case AttributeKind.BooleanAttr: return "boolean";
                case AttributeKind.StringAttr: return "string";
                case AttributeKind.EnumAttr: return attrType.EnumType.Name;
                case AttributeKind.FloatAttr: return "float";
                case AttributeKind.DoubleAttr: return "double";
                case AttributeKind.ObjectAttr: return "object";
            }
            return "<INVALID>";
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
                attribs.Add(String.Format("{0} {1}::{2} = {3}", GetKindName(attrType),
                    attrType.OwnerType.Name, attrType.Name, attrString));
            }
            return attribs;
        }
    }
}
