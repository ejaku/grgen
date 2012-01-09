/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define DUMP_COMMANDS_TO_YCOMP

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace de.unika.ipd.grGen.grShell
{
    public delegate void ConnectionLostHandler();

    /// <summary>
    /// The stream over which the client communicates with yComp
    /// </summary>
    class YCompStream
    {
        NetworkStream stream;
        byte[] readBuffer = new byte[4096];
        bool closing = false;

        public event ConnectionLostHandler OnConnectionLost;

#if DUMP_COMMANDS_TO_YCOMP
        StreamWriter dumpWriter;
#endif

        public YCompStream(TcpClient client)
        {
            stream = client.GetStream();

#if DUMP_COMMANDS_TO_YCOMP
            dumpWriter = new StreamWriter("ycomp_dump.txt");
#endif
        }

        public void Write(String message)
        {
            try
            {
#if DUMP_COMMANDS_TO_YCOMP
                dumpWriter.Write(message);
                dumpWriter.Flush();
#endif
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch(Exception)
            {
                stream = null;
                if(closing) return;
#if DUMP_COMMANDS_TO_YCOMP
                dumpWriter.Write("connection lost!\n");
                dumpWriter.Flush();
#endif
                ConnectionLostHandler handler = OnConnectionLost;
                if(handler != null) handler();
            }
        }

#if DUMP_COMMANDS_TO_YCOMP
        public void Dump(String message)
        {
            dumpWriter.Write(message);
            dumpWriter.Flush();
        }
#endif

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
                if(stream == null)
                {
                    ConnectionLostHandler handler = OnConnectionLost;
                    if(handler != null) handler();
                    return false;
                }

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
    /// Class communicating with yComp over a socket via the GrGen-yComp protocol,
    /// mainly telling yComp what should be displayed (and how)
    /// </summary>
    public class YCompClient
    {
        TcpClient ycompClient;
        internal YCompStream ycompStream;

        INamedGraph graph;
        
        DumpInfo dumpInfo;

        bool isDirty = false;
        bool isLayoutDirty = false;

        Dictionary<IEdge, bool> hiddenEdges = new Dictionary<IEdge,bool>();

        String nodeRealizerOverride = null;
        String edgeRealizerOverride = null;

        private ElementRealizers realizers;

        private static Dictionary<String, bool> availableLayouts;


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

        /// <summary>
        /// Creates a new YCompClient instance and connects to the local YComp server.
        /// If it is not available a SocketException is thrown
        /// </summary>
        public YCompClient(INamedGraph graph, String layoutModule, int connectionTimeout, int port, DumpInfo dumpInfo, ElementRealizers realizers)
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

            SetLayout(layoutModule);

            dumpInfo.OnNodeTypeAppearanceChanged += new NodeTypeAppearanceChangedHandler(OnNodeTypeAppearanceChanged);
            dumpInfo.OnEdgeTypeAppearanceChanged += new EdgeTypeAppearanceChangedHandler(OnEdgeTypeAppearanceChanged);
            dumpInfo.OnTypeInfotagsChanged += new TypeInfotagsChangedHandler(OnTypeInfotagsChanged);

            this.realizers = realizers;
            realizers.RegisterYComp(this);

            // TODO: Add group related events
        }


        public static IEnumerable<String> AvailableLayouts
        {
            get { return availableLayouts.Keys; }
        }

        public static bool IsValidLayout(String layoutName)     // TODO: allow case insensitive layout name
        {
            return availableLayouts.ContainsKey(layoutName);
        }

        /// <summary>
        /// If non-null, overrides the type dependend node realizer (setter used from debugger for added nodes, other realizers are given directly at methods called by debugger)
        /// </summary>
        public String NodeRealizerOverride { get { return nodeRealizerOverride; } set { nodeRealizerOverride = value; } }

        /// <summary>
        /// If non-null, overrides the type dependend edge realizer (setter used from debugger for added edges, other realizers are given directly at methods called by debugger)
        /// </summary>
        public String EdgeRealizerOverride { get { return edgeRealizerOverride; } set { edgeRealizerOverride = value; } }

        public INamedGraph Graph { get { return graph; } }

        public event ConnectionLostHandler OnConnectionLost
        { add { ycompStream.OnConnectionLost += value; } remove { ycompStream.OnConnectionLost -= value; } }

        public bool CommandAvailable { get { return ycompStream.Ready; } }
        public bool ConnectionLost { get { return !ycompStream.IsStreamOpen; } }

        public String ReadCommand()
        {
            return ycompStream.Read();
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
        public bool Sync()
        {
            ycompStream.Write("sync\n");
            return ycompStream.Read() == "sync\n";
        }

        public void AddNode(INode node)
        {
            if(IsNodeExcluded(node)) return;

            String nrName = nodeRealizerOverride ?? realizers.GetNodeRealizer(node.Type, dumpInfo);

            String name = graph.GetElementName(node);
            if(dumpInfo.GetGroupNodeType(node.Type) != null)
                ycompStream.Write("addSubgraphNode \"-1\" \"n" + name + "\" \"" + nrName + "\" \"" + GetElemLabel(node) + "\"\n");
            else
                ycompStream.Write("addNode \"-1\" \"n" + name + "\" \"" + nrName + "\" \"" + GetElemLabel(node) + "\"\n");
            foreach(AttributeType attrType in node.Type.AttributeTypes)
            {
                string attrTypeString;
                string attrValueString;
                EncodeAttr(attrType, node, out attrTypeString, out attrValueString);
                ycompStream.Write("changeNodeAttr \"n" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + attrTypeString + "\" \"" + attrValueString + "\"\n");
            }
            isDirty = true;
            isLayoutDirty = true;
        }

        public void AddEdge(IEdge edge)
        {
            if(IsEdgeExcluded(edge)) return;

            String edgeRealizerName = edgeRealizerOverride ?? realizers.GetEdgeRealizer(edge.Type, dumpInfo);

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
                    groupedNode = TryGroupNode(groupNodeFirst, edge, srcName, tgtName, srcGroupNodeType, tgtGroupNodeType, ref grpMode);
                    if(!groupedNode && groupNodeSecond != null)
                        groupedNode = TryGroupNode(groupNodeSecond, edge, srcName, tgtName, srcGroupNodeType, tgtGroupNodeType, ref grpMode);
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
                + "\" \"" + edgeRealizerName + "\" \"" + GetElemLabel(edge) + "\"\n");
            foreach(AttributeType attrType in edge.Type.AttributeTypes)
            {
                string attrTypeString;
                string attrValueString;
                EncodeAttr(attrType, edge, out attrTypeString, out attrValueString);
                ycompStream.Write("changeEdgeAttr \"e" + edgeName + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + attrTypeString + "\" \"" + attrValueString + "\"\n");
            }
            isDirty = true;
            isLayoutDirty = true;
        }

        /// <summary>
        /// Annotates the given element with the given string in double angle brackets
        /// </summary>
        /// <param name="elem">The element to be annotated</param>
        /// <param name="annotation">The annotation string or null, if the annotation is to be removed</param>
        public void AnnotateElement(IGraphElement elem, String annotation)
        {
            if (elem is INode)
            {
                INode node = (INode)elem;
                if (IsNodeExcluded(node)) return;
                ycompStream.Write("setNodeLabel \"n" + graph.GetElementName(elem) + "\" \""
                    + (annotation == null ? "" : "<<" + annotation + ">>\\n") + GetElemLabel(elem) + "\"\n");
            }
            else
            {
                IEdge edge = (IEdge)elem;
                if (IsEdgeExcluded(edge)) return;
                ycompStream.Write("setEdgeLabel \"e" + graph.GetElementName(elem) + "\" \""
                    + (annotation == null ? "" : "<<" + annotation + ">>\\n") + GetElemLabel(elem) + "\"\n");
            }
            isDirty = true;
        }

        /// <summary>
        /// Sets the node realizer of the given node.
        /// If realizer is null, the realizer for the type of the node is used.
        /// </summary>
        public void ChangeNode(INode node, String realizer)
        {
            if(IsNodeExcluded(node)) return;

            if(realizer == null) realizer = realizers.GetNodeRealizer(node.Type, dumpInfo);
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
            if(IsEdgeExcluded(edge)) return;
            if(hiddenEdges.ContainsKey(edge)) return;

            if(realizer == null) realizer = realizers.GetEdgeRealizer(edge.Type, dumpInfo);
            String name = graph.GetElementName(edge);
            ycompStream.Write("changeEdge \"e" + name + "\" \"" + realizer + "\"\n");
            isDirty = true;
        }

        public void ChangeNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            if (IsNodeExcluded(node)) return;

            String attrTypeString;
            String attrValueString;
            EncodeAttr(attrType, node, changeType, newValue, keyValue, out attrTypeString, out attrValueString);

            String name = graph.GetElementName(node);
            ycompStream.Write("changeNodeAttr \"n" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + attrTypeString + "\" \"" + attrValueString + "\"\n");
            if(dumpInfo.GetTypeInfoTag(node.Type, attrType) != null)
                ycompStream.Write("setNodeLabel \"n" + name + "\" \""
                    + GetElemLabelWithChangedAttr(node, attrType, attrValueString) + "\"\n");
            isDirty = true;
        }

        public void ChangeEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            if (IsEdgeExcluded(edge)) return;
            if (hiddenEdges.ContainsKey(edge)) return;

            String attrTypeString;
            String attrValueString;
            EncodeAttr(attrType, edge, changeType, newValue, keyValue, out attrTypeString, out attrValueString);

            String name = graph.GetElementName(edge);
            ycompStream.Write("changeEdgeAttr \"e" + name + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + attrTypeString + "\" \"" + attrValueString + "\"\n");
            List<InfoTag> infotags = dumpInfo.GetTypeInfoTags(edge.Type);
            if (dumpInfo.GetTypeInfoTag(edge.Type, attrType) != null)
                ycompStream.Write("setEdgeLabel \"e" + name + "\" \""
                    + GetElemLabelWithChangedAttr(edge, attrType, attrValueString) + "\"\n");
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
                INode oldNode = (INode) oldElem;
                if(IsNodeExcluded(oldNode)) return;
            }
            else
            {
                IEdge oldEdge = (IEdge) oldElem;
                if(IsEdgeExcluded(oldEdge)) return;
                if(hiddenEdges.ContainsKey(oldEdge)) return;       // TODO: Update group relation
            }

            String elemKind = isNode ? "Node" : "Edge";
            String elemNamePrefix = isNode ? "n" : "e";
            String oldName = elemNamePrefix + graph.GetElementName(oldElem);

            ycompStream.Write("set" + elemKind + "Label \"" + oldName + "\" \"" + GetElemLabel(newElem) + "\"\n");

            // remove the old attributes
            foreach(AttributeType attrType in oldType.AttributeTypes)
            {
                String attrTypeString;
                String attrValueString;
                EncodeAttr(attrType, oldElem, out attrTypeString, out attrValueString);
                ycompStream.Write("clear" + elemKind + "Attr \"" + oldName + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + attrTypeString + "\"\n");
            }
            // set the new attributes
            foreach(AttributeType attrType in newType.AttributeTypes)
            {
                String attrTypeString;
                String attrValueString;
                EncodeAttr(attrType, newElem, out attrTypeString, out attrValueString);
                ycompStream.Write("change" + elemKind + "Attr \"" + oldName + "\" \"" + attrType.OwnerType.Name + "::" + attrType.Name + " : "
                    + attrTypeString + "\" \"" + attrValueString + "\"\n");
            }

            if(isNode)
            {
                String oldNr = realizers.GetNodeRealizer((NodeType)oldType, dumpInfo);
                String newNr = realizers.GetNodeRealizer((NodeType)newType, dumpInfo);
                if(oldNr != newNr)
                    ChangeNode((INode) oldElem, newNr);
            }
            else
            {
                String oldEr = realizers.GetEdgeRealizer((EdgeType)oldType, dumpInfo);
                String newEr = realizers.GetEdgeRealizer((EdgeType)newType, dumpInfo);
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
            if(IsNodeExcluded(node)) return;

            DeleteNode(graph.GetElementName(node));
        }

        public void DeleteEdge(String edgeName)
        {
            // TODO: Update group relation

            ycompStream.Write("deleteEdge \"e" + edgeName + "\"\n");
            isDirty = true;
            isLayoutDirty = true;
        }

        public void DeleteEdge(IEdge edge)
        {
            // TODO: Update group relation

            if(hiddenEdges.ContainsKey(edge))
                hiddenEdges.Remove(edge);
            if(IsEdgeExcluded(edge)) return;

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

        public void WaitForElement(bool val)
        {
            ycompStream.Write("waitForElement " + (val ? "true" : "false") + "\n");
        }

        public void Close()
        {
            realizers.UnregisterYComp();
            
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

        void OnNodeTypeAppearanceChanged(NodeType type)
        {
            if(dumpInfo.IsExcludedNodeType(type)) return;

            String nr = realizers.GetNodeRealizer(type, dumpInfo);
            foreach(INode node in graph.GetExactNodes(type))
                ChangeNode(node, nr);
            isDirty = true;
        }

        void OnEdgeTypeAppearanceChanged(EdgeType type)
        {
            if(dumpInfo.IsExcludedEdgeType(type)) return;

            String er = realizers.GetEdgeRealizer(type, dumpInfo);
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
                    if(IsEdgeExcluded(edge)) return; // additionally checks incident nodes

                    ycompStream.Write("setEdgeLabel \"e" + dumpInfo.GetElementName(edge) + "\" \"" + GetElemLabel(edge) + "\"\n");
                }
            }
            isDirty = true;
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

        private String GetElemLabel(IGraphElement elem)
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
                    string attrTypeString;
                    string attrValueString;
                    EncodeAttr(infoTag.AttributeType, elem, out attrTypeString, out attrValueString);

                    if(!first) label += "\\n";
                    else first = false;

                    if(!infoTag.ShortInfoTag)
                        label += infoTag.AttributeType.Name + " = ";
                    label += attrValueString;
                }
            }

            return label;
        }

        private String GetElemLabelWithChangedAttr(IGraphElement elem, AttributeType changedAttrType, String newValue)
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
                    string attrValueString;

                    if (infoTag.AttributeType == changedAttrType) {
                        attrValueString = newValue;
                    } else {
                        string attrTypeString;
                        EncodeAttr(infoTag.AttributeType, elem, out attrTypeString, out attrValueString);
                    }

                    if(!first) label += "\\n";
                    else first = false;

                    if(!infoTag.ShortInfoTag)
                        label += infoTag.AttributeType.Name + " = ";
                    label += attrValueString;
                }
            }

            return label;
        }

        private void EncodeAttr(AttributeType attrType, IGraphElement elem, out String attrTypeString, out String attrValueString)
        {
            if (attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
            {
                DictionaryListHelper.ToString((IDictionary)elem.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph);
                attrValueString = Encode(attrValueString);
            }
            else if(attrType.Kind == AttributeKind.ArrayAttr)
            {
                DictionaryListHelper.ToString((IList)elem.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph);
                attrValueString = Encode(attrValueString);
            }
            else
            {
                DictionaryListHelper.ToString(elem.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph);
                attrValueString = Encode(attrValueString);
            }
        }

        private void EncodeAttr(AttributeType attrType, IGraphElement elem, AttributeChangeType changeType, Object newValue, Object keyValue,
            out String attrTypeString, out String attrValueString)
        {
            if (attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
            {
                DictionaryListHelper.ToString((IDictionary)elem.GetAttribute(attrType.Name), changeType, newValue, keyValue, out attrTypeString, out attrValueString, attrType, graph);
                attrValueString = Encode(attrValueString);
            }
            else if(attrType.Kind == AttributeKind.ArrayAttr)
            {
                DictionaryListHelper.ToString((IList)elem.GetAttribute(attrType.Name), changeType, newValue, keyValue, out attrTypeString, out attrValueString, attrType, graph);
                attrValueString = Encode(attrValueString);
            }
            else
            {
                DictionaryListHelper.ToString(newValue, out attrTypeString, out attrValueString, attrType, graph);
                attrValueString = Encode(attrValueString);
            }
        }

        private String Encode(String str)
        {
            if(str == null) return "";

            StringBuilder sb = new StringBuilder(str);
            sb.Replace("  ", " &nbsp;");
            sb.Replace("\n", "\\n");
            sb.Replace("\"", "&quot;");
            return sb.ToString();
        }

        bool IsEdgeExcluded(IEdge edge)
        {
            return dumpInfo.IsExcludedEdgeType(edge.Type)
                || dumpInfo.IsExcludedNodeType(edge.Source.Type)
                || dumpInfo.IsExcludedNodeType(edge.Target.Type);
        }

        bool IsNodeExcluded(INode node)
        {
            return dumpInfo.IsExcludedNodeType(node.Type);
        }
    }
}
