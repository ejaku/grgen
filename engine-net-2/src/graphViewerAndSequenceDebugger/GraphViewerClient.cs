/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    // potentially available graph viewer client types (debugger types)
    public enum GraphViewerTypes
    {
        YComp, MSAGL
    }

    public delegate void ConnectionLostHandler();

    /// <summary>
    /// Class communicating with yComp or MSAGL, over a simple live graph viewer protocol,
    /// some higher-level shared functionality regarding graph presentation state handling is implemented here.
    /// </summary>
    public class GraphViewerClient
    {
        YCompServerProxy yCompServerProxy; // not null in case the basicClient is a YCompClient
        internal IBasicGraphViewerClient basicClient; // either the traditional YCompClient or a MSAGLClient

        INamedGraph graph;

        public DumpInfo dumpInfo;

        bool isDirty = false;
        bool isLayoutDirty = false;

        Dictionary<IEdge, bool> hiddenEdges = new Dictionary<IEdge, bool>();

        Dictionary<INode, bool> nodesIncludedWhileGraphExcluded = new Dictionary<INode, bool>();
        Dictionary<IEdge, bool> edgesIncludedWhileGraphExcluded = new Dictionary<IEdge, bool>();

        String nodeRealizerOverride = null;
        String edgeRealizerOverride = null;

        ElementRealizers realizers;

        ObjectNamerAndIndexer objectNamerAndIndexer;
        TransientObjectNamerAndIndexer transientObjectNamerAndIndexer;

        static Dictionary<String, bool> availableMSAGLLayouts;


        static GraphViewerClient()
        {
            availableMSAGLLayouts = new Dictionary<string, bool>();
            availableMSAGLLayouts.Add("SugiyamaScheme", true);
            availableMSAGLLayouts.Add("MDS", true);
            availableMSAGLLayouts.Add("Ranking", true);
            availableMSAGLLayouts.Add("IcrementalLayout", true);
        }

        /// <summary>
        /// Creates a new GraphViewerClient instance.
        /// internally, it creates a YCompClient and connects to the local YComp server,
        /// or creates a MSAGLClient, inside the basicGraphViewerClientHost (which may be a GuiConsoleDebuggerHost) in case one is supplied,
        /// depending on the graph viewer type that is requested (the layout is expected to be one of the valid layouts of the corresponding graph viewer client).
        /// </summary>
        public GraphViewerClient(INamedGraph graph, GraphViewerTypes graphViewerType, String layoutModule,
            DumpInfo dumpInfo, ElementRealizers realizers,
            ObjectNamerAndIndexer objectNamerAndIndexer, TransientObjectNamerAndIndexer transientObjectNamerAndIndexer,
            IBasicGraphViewerClientHost basicGraphViewerClientHost)
        {
            this.graph = graph;
            this.dumpInfo = dumpInfo;

            if(graphViewerType == GraphViewerTypes.MSAGL)
            {
                IHostCreator guiConsoleDebuggerHostCreator = GetGuiConsoleDebuggerHostCreator();
                IBasicGraphViewerClientCreator basicGraphViewerClientCreator = GetBasicGraphViewerClientCreator();
                IBasicGraphViewerClientHost host = basicGraphViewerClientHost;
                if(host == null)
                    host = guiConsoleDebuggerHostCreator.CreateBasicGraphViewerClientHost();
                basicClient = basicGraphViewerClientCreator.Create(graphViewerType, host);
            }
            else // default is yCompClient
            {
                yCompServerProxy = new YCompServerProxy(YCompServerProxy.GetFreeTCPPort());
                int connectionTimeout = 20000;
                int port = yCompServerProxy.port;
                basicClient = new YCompClient(connectionTimeout, port);
            }

            SetLayout(layoutModule);

            dumpInfo.OnNodeTypeAppearanceChanged += new NodeTypeAppearanceChangedHandler(OnNodeTypeAppearanceChanged);
            dumpInfo.OnEdgeTypeAppearanceChanged += new EdgeTypeAppearanceChangedHandler(OnEdgeTypeAppearanceChanged);
            dumpInfo.OnTypeInfotagsChanged += new TypeInfotagsChangedHandler(OnTypeInfotagsChanged);

            this.realizers = realizers;
            realizers.RegisterGraphViewerClient(this);

            this.objectNamerAndIndexer = objectNamerAndIndexer;
            this.transientObjectNamerAndIndexer = transientObjectNamerAndIndexer;
            // TODO: Add group related events
        }

        /// <summary>
        /// returns a host creator from graphViewerAndSequenceDebuggerWindowsForms.dll
        /// </summary>
        public static IHostCreator GetGuiConsoleDebuggerHostCreator()
        {
            Type guiConsoleDebuggerHostCreatorType = GetSingleImplementationOfInterfaceFromAssembly("graphViewerAndSequenceDebuggerWindowsForms.dll", "IHostCreator");
            IHostCreator guiConsoleDebuggerHostCreator = (IHostCreator)Activator.CreateInstance(guiConsoleDebuggerHostCreatorType);
            return guiConsoleDebuggerHostCreator;
        }

        private static IBasicGraphViewerClientCreator GetBasicGraphViewerClientCreator()
        {
            Type basicGraphViewerClientCreatorType = GetSingleImplementationOfInterfaceFromAssembly("graphViewerAndSequenceDebuggerWindowsForms.dll", "IBasicGraphViewerClientCreator");
            IBasicGraphViewerClientCreator basicGraphViewerClientCreator = (IBasicGraphViewerClientCreator)Activator.CreateInstance(basicGraphViewerClientCreatorType);
            return basicGraphViewerClientCreator;
        }

        private static Type GetSingleImplementationOfInterfaceFromAssembly(string assemblyName, string interfaceName)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            string pathPrefix = System.IO.Path.GetDirectoryName(callingAssembly.Location);
            Assembly assembly = Assembly.LoadFrom(pathPrefix + System.IO.Path.DirectorySeparatorChar + assemblyName); // maybe todo: search path instead of same path as calling graphViewerAndSequenceDebugger.dll?

            Type typeImplementingInterface = null;
            try
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic)
                        continue;
                    if(type.GetInterface(interfaceName) != null)
                    {
                        if(typeImplementingInterface != null)
                        {
                            throw new ArgumentException(
                                "The assembly " + assemblyName + " contains more than one " + interfaceName + " implementation!");
                        }
                        typeImplementingInterface = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                String errorMsg = "";
                foreach(Exception ex in e.LoaderExceptions)
                {
                    errorMsg += "- " + ex.Message + Environment.NewLine;
                }
                if(errorMsg.Length == 0)
                    errorMsg = e.Message;
                throw new ArgumentException(errorMsg);
            }
            if(typeImplementingInterface == null)
                throw new ArgumentException("The assembly " + assemblyName + " does not contain an " + interfaceName + " implementation!");

            return typeImplementingInterface;
        }

        public void Close()
        {
            realizers.UnregisterGraphViewerClient();

            basicClient.Close();
            basicClient = null;

            if(yCompServerProxy != null)
                yCompServerProxy.Close();
            yCompServerProxy = null;

            dumpInfo.OnNodeTypeAppearanceChanged -= new NodeTypeAppearanceChangedHandler(OnNodeTypeAppearanceChanged);
            dumpInfo.OnEdgeTypeAppearanceChanged -= new EdgeTypeAppearanceChangedHandler(OnEdgeTypeAppearanceChanged);
            dumpInfo.OnTypeInfotagsChanged -= new TypeInfotagsChangedHandler(OnTypeInfotagsChanged);
        }

        public static IEnumerable<String> AvailableLayouts(GraphViewerTypes type)
        {
            if(type == GraphViewerTypes.YComp)
                return YCompClient.AvailableLayouts;
            else if(type == GraphViewerTypes.MSAGL)
            {
                // better to be handled by the corresponding graph viewer client that has that knowledge,
                // but this would require to create an instance of a dll we don't want to instantiate for technology reasons unless really requested
                // this function is also used for general error checking/printing without prior request, though
                return availableMSAGLLayouts.Keys;
            }
            else
                return null;
        }

        public static bool IsValidLayout(GraphViewerTypes type, String layoutName)     // TODO: allow case insensitive layout name
        {
            if(type == GraphViewerTypes.YComp)
                return YCompClient.IsValidLayout(layoutName);
            else if(type == GraphViewerTypes.MSAGL)
            {
                // better to be handled by the corresponding graph viewer client that has that knowledge,
                // but this would require to create an instance of a dll we don't want to instantiate for technology reasons unless really requested
                // this function is also used for general error checking/printing without prior request, though
                return availableMSAGLLayouts.ContainsKey(layoutName);
            }
            else
                return false;
        }

        /// <summary>
        /// If non-null, overrides the type dependent node realizer (setter used from debugger for added nodes, other realizers are given directly at methods called by debugger)
        /// </summary>
        public String NodeRealizerOverride
        {
            get { return nodeRealizerOverride; }
            set { nodeRealizerOverride = value; }
        }

        /// <summary>
        /// If non-null, overrides the type dependent edge realizer (setter used from debugger for added edges, other realizers are given directly at methods called by debugger)
        /// </summary>
        public String EdgeRealizerOverride
        {
            get { return edgeRealizerOverride; }
            set { edgeRealizerOverride = value; }
        }

        public INamedGraph Graph 
        { 
            get { return graph; }
            set
            { 
                if(isDirty || isLayoutDirty || hiddenEdges.Count > 0) 
                    throw new Exception("Internal error: first clear the graph before you switch to a new one!");
                graph = value;
                dumpInfo.ElementNameGetter = graph.GetElementName;
            }
        }

        public event ConnectionLostHandler OnConnectionLost
        {
            add { basicClient.OnConnectionLost += value; }
            remove { basicClient.OnConnectionLost -= value; }
        }

        public bool CommandAvailable
        {
            get { return basicClient.CommandAvailable; }
        }

        public bool ConnectionLost
        {
            get { return basicClient.ConnectionLost; }
        }

        public String ReadCommand()
        {
            return basicClient.ReadCommand();
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
            basicClient.SetLayout(moduleName);
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
            return basicClient.GetLayoutOptions();
        }

        /// <summary>
        /// Sets a layout option of the current layouter of yComp.
        /// </summary>
        /// <param name="optionName">The name of the option.</param>
        /// <param name="optionValue">The new value.</param>
        /// <returns>Null, or a error message, if setting the option failed.</returns>
        public String SetLayoutOption(String optionName, String optionValue)
        {
            String msg = basicClient.SetLayoutOption(optionName, optionValue);
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
            basicClient.ForceLayout();
        }

        /// <summary>
        /// Relayouts the graph if needed.
        /// </summary>
        public void UpdateDisplay()
        {
            if(isLayoutDirty)
                ForceLayout();
            else if(isDirty)
            {
                basicClient.Show();
                isDirty = false;
            }
        }

        /// <summary>
        /// Sends a "sync" request and waits for a "sync" answer
        /// </summary>
        public bool Sync()
        {
            return basicClient.Sync();
        }

        public void AddNode(INode node)
        {
            if(IsNodeExcluded(node))
                return;

            String nrName = nodeRealizerOverride ?? realizers.GetNodeRealizer(node.Type, dumpInfo);

            String name = graph.GetElementName(node);
            if(dumpInfo.GetGroupNodeType(node.Type) != null)
                basicClient.AddSubgraphNode(name, nrName, GetElemLabel(node));
            else
                basicClient.AddNode(name, nrName, GetElemLabel(node));
            foreach(AttributeType attrType in node.Type.AttributeTypes)
            {
                string attrTypeString;
                string attrValueString;
                EncodeAttr(attrType, node, out attrTypeString, out attrValueString);
                basicClient.SetNodeAttribute(name, attrType.OwnerType.Name, attrType.Name, attrTypeString, attrValueString);
            }
            isDirty = true;
            isLayoutDirty = true;
        }

        public void AddNodeEvenIfGraphExcluded(INode node)
        {
            if(!nodesIncludedWhileGraphExcluded.ContainsKey(node))
            {
                AddNode(node);
                nodesIncludedWhileGraphExcluded.Add(node, true);
            }
        }

        public void AddEdge(IEdge edge)
        {
            if(IsEdgeExcluded(edge))
                return;

            String edgeRealizerName = edgeRealizerOverride ?? realizers.GetEdgeRealizer(edge.Type, dumpInfo);

            String edgeName = graph.GetElementName(edge);
            String srcName = graph.GetElementName(edge.Source);
            String tgtName = graph.GetElementName(edge.Target);

            if(edge.Source != edge.Target)
            {
                GroupNodeType srcGroupNodeType = dumpInfo.GetGroupNodeType(edge.Source.Type);
                GroupNodeType tgtGroupNodeType = dumpInfo.GetGroupNodeType(edge.Target.Type);
                INode groupNodeFirst = null, groupNodeSecond = null;
                if(tgtGroupNodeType != null)
                    groupNodeFirst = edge.Target;
                if(srcGroupNodeType != null)
                {
                    if(groupNodeFirst == null)
                        groupNodeFirst = edge.Source;
                    else if(srcGroupNodeType.Priority > tgtGroupNodeType.Priority)
                    {
                        groupNodeSecond = groupNodeFirst;
                        groupNodeFirst = edge.Source;
                    }
                    else
                        groupNodeSecond = edge.Source;
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

            basicClient.AddEdge(edgeName, srcName, tgtName, edgeRealizerName, GetElemLabel(edge));
            foreach(AttributeType attrType in edge.Type.AttributeTypes)
            {
                string attrTypeString;
                string attrValueString;
                EncodeAttr(attrType, edge, out attrTypeString, out attrValueString);
                basicClient.SetEdgeAttribute(edgeName, attrType.OwnerType.Name, attrType.Name, attrTypeString, attrValueString);
            }
            isDirty = true;
            isLayoutDirty = true;
        }

        public void AddEdgeEvenIfGraphExcluded(IEdge edge)
        {
            if(!edgesIncludedWhileGraphExcluded.ContainsKey(edge))
            {
                AddEdge(edge);
                edgesIncludedWhileGraphExcluded.Add(edge, true);
            }
        }

        /// <summary>
        /// Adding of helper edge used in debugging, for visualization of map content
        /// </summary>
        public void AddEdge(string edgeName, string edgeLabel, INode source, INode target)
        {
            String srcName = graph.GetElementName(source);
            String tgtName = graph.GetElementName(target);

            basicClient.AddEdge(edgeName, srcName, tgtName, realizers.MatchedEdgeRealizer, edgeLabel);
            isDirty = true;
            isLayoutDirty = true;
        }

        public void AddNeighboursAndParentsOfNeededGraphElements()
        {
            // add all neighbours of elements to graph and excludedGraphElementsIncluded (1-level direct context by default, maybe overriden by user)
            Set<INode> nodesIncluded = new Set<INode>(); // second variable needed to prevent disturbing iteration
            foreach(INode node in nodesIncludedWhileGraphExcluded.Keys)
            {
                nodesIncluded.Add(node);
            }
            for(int i = 0; i < dumpInfo.GetExcludeGraphContextDepth(); ++i)
            {
                AddDirectNeighboursOfNeededGraphElements(nodesIncluded);
            }

            // add all parents of elements to graph and excludedGraphElementsIncluded (n-level nesting)
            AddParentsOfNeededGraphElements(nodesIncluded);
        }

        private void AddDirectNeighboursOfNeededGraphElements(Set<INode> nodesIncluded)
        {
            foreach(INode node in nodesIncluded)
            {
                foreach(IEdge edge in node.Incident)
                {
                    AddNodeEvenIfGraphExcluded(edge.Opposite(node));
                    AddEdgeEvenIfGraphExcluded(edge);
                }
            }
            foreach(INode node in nodesIncludedWhileGraphExcluded.Keys)
            {
                if(!nodesIncluded.Contains(node))
                    nodesIncluded.Add(node);
            }
        }

        private void AddParentsOfNeededGraphElements(Set<INode> latelyAddedNodes)
        {
            Set<INode> newlyAddedNodes = new Set<INode>();

            // wavefront algorithm, in the following step all nodes added by the previous step are inspected,
            // until the wave collapses cause no not already added node is added any more
            while(latelyAddedNodes.Count > 0)
            {
                foreach(INode node in latelyAddedNodes)
                {
                    bool parentFound = false;
                    foreach(GroupNodeType groupNodeType in dumpInfo.GroupNodeTypes)
                    {
                        foreach(IEdge edge in node.Incoming)
                        {
                            INode parent = edge.Source;
                            if(!groupNodeType.NodeType.IsMyType(parent.Type.TypeID))
                                continue;
                            GroupMode grpMode = groupNodeType.GetEdgeGroupMode(edge.Type, node.Type);
                            if((grpMode & GroupMode.GroupOutgoingNodes) == 0)
                                continue;
                            if(!nodesIncludedWhileGraphExcluded.ContainsKey(parent))
                            {
                                newlyAddedNodes.Add(parent);
                                AddNode(parent);
                                nodesIncludedWhileGraphExcluded.Add(parent, true);
                                AddEdge(edge);
                                if(!edgesIncludedWhileGraphExcluded.ContainsKey(edge))
                                    edgesIncludedWhileGraphExcluded.Add(edge, true);
                            }
                            parentFound = true;
                        }
                        if(parentFound)
                            break;
                        foreach(IEdge edge in node.Outgoing)
                        {
                            INode parent = edge.Target;
                            if(!groupNodeType.NodeType.IsMyType(parent.Type.TypeID))
                                continue;
                            GroupMode grpMode = groupNodeType.GetEdgeGroupMode(edge.Type, node.Type);
                            if((grpMode & GroupMode.GroupIncomingNodes) == 0)
                                continue;
                            if(!nodesIncludedWhileGraphExcluded.ContainsKey(parent))
                            {
                                newlyAddedNodes.Add(parent);
                                AddNode(parent);
                                nodesIncludedWhileGraphExcluded.Add(parent, true);
                                AddEdge(edge);
                                if(!edgesIncludedWhileGraphExcluded.ContainsKey(edge))
                                    edgesIncludedWhileGraphExcluded.Add(edge, true);
                            }
                            parentFound = true;
                        }
                        if(parentFound)
                            break;
                    }
                }
                Set<INode> tmp = latelyAddedNodes;
                latelyAddedNodes = newlyAddedNodes;
                newlyAddedNodes = tmp;
                newlyAddedNodes.Clear();
            }
        }

        /// <summary>
        /// Annotates the given element with the given string in double angle brackets
        /// </summary>
        /// <param name="elem">The element to be annotated</param>
        /// <param name="annotation">The annotation string or null, if the annotation is to be removed</param>
        public void AnnotateElement(IGraphElement elem, String annotation)
        {
            if(elem is INode)
            {
                INode node = (INode)elem;
                if(IsNodeExcluded(node))
                    return;
                basicClient.SetNodeLabel(graph.GetElementName(elem), (annotation == null ? "" : "<<" + annotation + ">>\\n") + GetElemLabel(elem));
            }
            else
            {
                IEdge edge = (IEdge)elem;
                if(IsEdgeExcluded(edge))
                    return;
                basicClient.SetEdgeLabel(graph.GetElementName(elem), (annotation == null ? "" : "<<" + annotation + ">>\\n") + GetElemLabel(elem));
            }
            isDirty = true;
        }

        /// <summary>
        /// Sets the node realizer of the given node.
        /// If realizer is null, the realizer for the type of the node is used.
        /// </summary>
        public void ChangeNode(INode node, String realizer)
        {
            if(IsNodeExcluded(node))
                return;

            if(realizer == null)
                realizer = realizers.GetNodeRealizer(node.Type, dumpInfo);
            String name = graph.GetElementName(node);
            basicClient.ChangeNode(name, realizer);
            isDirty = true;
        }

        /// <summary>
        /// Sets the edge realizer of the given edge.
        /// If realizer is null, the realizer for the type of the edge is used.
        /// </summary>
        public void ChangeEdge(IEdge edge, String realizer)
        {
            if(IsEdgeExcluded(edge))
                return;
            if(hiddenEdges.ContainsKey(edge))
                return;

            if(realizer == null)
                realizer = realizers.GetEdgeRealizer(edge.Type, dumpInfo);
            String name = graph.GetElementName(edge);
            basicClient.ChangeEdge(name, realizer);
            isDirty = true;
        }

        public void ChangeNodeAttribute(INode node, AttributeType attrType)
        {
            if(IsNodeExcluded(node))
                return;

            String attrTypeString;
            String attrValueString;
            EncodeAttr(attrType, node, out attrTypeString, out attrValueString);

            String name = graph.GetElementName(node);
            basicClient.SetNodeAttribute(name, attrType.OwnerType.Name, attrType.Name, attrTypeString, attrValueString);
            if(dumpInfo.GetTypeInfoTag(node.Type, attrType) != null)
            {
                basicClient.SetNodeLabel(name, GetElemLabelWithChangedAttr(node, attrType, attrValueString));
            }
            isDirty = true;
        }

        public void ChangeEdgeAttribute(IEdge edge, AttributeType attrType)
        {
            if(IsEdgeExcluded(edge))
                return;
            if(hiddenEdges.ContainsKey(edge))
                return;

            String attrTypeString;
            String attrValueString;
            EncodeAttr(attrType, edge, out attrTypeString, out attrValueString);

            String name = graph.GetElementName(edge);
            basicClient.SetEdgeAttribute(name, attrType.OwnerType.Name, attrType.Name, attrTypeString, attrValueString);
            List<InfoTag> infotags = dumpInfo.GetTypeInfoTags(edge.Type);
            if(dumpInfo.GetTypeInfoTag(edge.Type, attrType) != null)
                basicClient.SetEdgeLabel(name, GetElemLabelWithChangedAttr(edge, attrType, attrValueString));
            isDirty = true;
        }

        public void RetypingElement(IGraphElement oldElem, IGraphElement newElem)
        {
            bool isNode = oldElem is INode;
            GraphElementType oldType = oldElem.Type;
            GraphElementType newType = newElem.Type;

            // TODO: Add element, if old element was excluded, but new element is not
            if(isNode)
            {
                INode oldNode = (INode)oldElem;
                if(IsNodeExcluded(oldNode))
                    return;
            }
            else
            {
                IEdge oldEdge = (IEdge)oldElem;
                if(IsEdgeExcluded(oldEdge))
                    return;
                if(hiddenEdges.ContainsKey(oldEdge))
                    return;       // TODO: Update group relation
            }

            String oldName = graph.GetElementName(oldElem);

            if(isNode)
                basicClient.SetNodeLabel(oldName, GetElemLabel(newElem));
            else
                basicClient.SetEdgeLabel(oldName, GetElemLabel(newElem));

            // remove the old attributes
            foreach(AttributeType attrType in oldType.AttributeTypes)
            {
                String attrTypeString;
                String attrValueString;
                EncodeAttr(attrType, oldElem, out attrTypeString, out attrValueString);
                if(isNode)
                    basicClient.ClearNodeAttribute(oldName, attrType.OwnerType.Name, attrType.Name, attrTypeString);
                else
                    basicClient.ClearEdgeAttribute(oldName, attrType.OwnerType.Name, attrType.Name, attrTypeString);
            }
            // set the new attributes
            foreach(AttributeType attrType in newType.AttributeTypes)
            {
                String attrTypeString;
                String attrValueString;
                EncodeAttr(attrType, newElem, out attrTypeString, out attrValueString);
                if(isNode)
                    basicClient.SetNodeAttribute(oldName, attrType.OwnerType.Name, attrType.Name, attrTypeString, attrValueString);
                else
                    basicClient.SetEdgeAttribute(oldName, attrType.OwnerType.Name, attrType.Name, attrTypeString, attrValueString);
            }

            if(isNode)
            {
                String oldNr = realizers.GetNodeRealizer((NodeType)oldType, dumpInfo);
                String newNr = realizers.GetNodeRealizer((NodeType)newType, dumpInfo);
                if(oldNr != newNr)
                    ChangeNode((INode)oldElem, newNr);
            }
            else
            {
                String oldEr = realizers.GetEdgeRealizer((EdgeType)oldType, dumpInfo);
                String newEr = realizers.GetEdgeRealizer((EdgeType)newType, dumpInfo);
                if(oldEr != newEr)
                    ChangeEdge((IEdge)oldElem, newEr);
            }

            String newName = graph.GetElementName(newElem);
            if(isNode)
                basicClient.RenameNode(oldName, newName);
            else
                basicClient.RenameEdge(oldName, newName);

            isDirty = true;
        }

        public void DeleteNode(String nodeName, String oldNodeName)
        {
            basicClient.DeleteNode(nodeName, oldNodeName);
            isDirty = true;
            isLayoutDirty = true;
        }

        public void DeleteNode(INode node)
        {
            if(IsNodeExcluded(node))
                return;

            DeleteNode(graph.GetElementName(node), null);
        }

        public void DeleteEdge(String edgeName, String oldEdgeName)
        {
            // TODO: Update group relation

            basicClient.DeleteEdge(edgeName, oldEdgeName);
            isDirty = true;
            isLayoutDirty = true;
        }

        public void DeleteEdge(IEdge edge)
        {
            // TODO: Update group relation

            if(hiddenEdges.ContainsKey(edge))
                hiddenEdges.Remove(edge);
            if(IsEdgeExcluded(edge))
                return;

            DeleteEdge(graph.GetElementName(edge), null);
        }

        public void RenameNode(String oldName, String newName)
        {
            basicClient.RenameNode(oldName, newName);
        }

        public void RenameEdge(String oldName, String newName)
        {
            basicClient.RenameEdge(oldName, newName);
        }

        /// <summary>
        /// Uploads the graph to YComp, updates the display and makes a synchronisation.
        /// Does not change the stored graph, even though this is required for naming.
        /// </summary>
        public void UploadGraph()
        {
            foreach(INode node in graph.Nodes)
            {
                AddNode(node);
            }
            foreach(IEdge edge in graph.Edges)
            {
                AddEdge(edge);
            }
            UpdateDisplay();
            Sync();
        }

        public void ClearGraph()
        {
            basicClient.ClearGraph();
            isDirty = false;
            isLayoutDirty = false;
            hiddenEdges.Clear();
            nodesIncludedWhileGraphExcluded.Clear();
            edgesIncludedWhileGraphExcluded.Clear();
        }

        public void WaitForElement(bool val)
        {
            basicClient.WaitForElement(val);
        }

        void OnNodeTypeAppearanceChanged(NodeType type)
        {
            if(dumpInfo.IsExcludedNodeType(type))
                return;

            String nr = realizers.GetNodeRealizer(type, dumpInfo);
            foreach(INode node in graph.GetExactNodes(type))
            {
                ChangeNode(node, nr);
            }
            isDirty = true;
        }

        void OnEdgeTypeAppearanceChanged(EdgeType type)
        {
            if(dumpInfo.IsExcludedEdgeType(type))
                return;

            String er = realizers.GetEdgeRealizer(type, dumpInfo);
            foreach(IEdge edge in graph.GetExactEdges(type))
            {
                ChangeEdge(edge, er);
            }
            isDirty = true;
        }

        void OnTypeInfotagsChanged(GraphElementType type)
        {
            if(type.IsNodeType)
            {
                if(dumpInfo.IsExcludedNodeType((NodeType) type))
                    return;

                foreach(INode node in graph.GetExactNodes((NodeType)type))
                {
                    basicClient.SetNodeLabel(dumpInfo.GetElementName(node), GetElemLabel(node));
                }
            }
            else
            {
                if(dumpInfo.IsExcludedEdgeType((EdgeType) type))
                    return;

                foreach(IEdge edge in graph.GetExactEdges((EdgeType) type))
                {
                    if(IsEdgeExcluded(edge))
                        return; // additionally checks incident nodes

                    basicClient.SetEdgeLabel(dumpInfo.GetElementName(edge), GetElemLabel(edge));
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
                    basicClient.MoveNode(srcName, tgtName);
                    return true;
                }
            }
            else if(groupNode == edge.Source)
            {
                grpMode = srcGroupNodeType.GetEdgeGroupMode(edge.Type, edge.Target.Type);
                if((grpMode & GroupMode.GroupOutgoingNodes) != 0)
                {
                    basicClient.MoveNode(tgtName, srcName);
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

                    if(!first)
                        label += "\\n";
                    else
                        first = false;

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

                    if(infoTag.AttributeType == changedAttrType)
                        attrValueString = newValue;
                    else
                    {
                        string attrTypeString;
                        EncodeAttr(infoTag.AttributeType, elem, out attrTypeString, out attrValueString);
                    }

                    if(!first)
                        label += "\\n";
                    else
                        first = false;

                    if(!infoTag.ShortInfoTag)
                        label += infoTag.AttributeType.Name + " = ";
                    label += attrValueString;
                }
            }

            return label;
        }

        private void EncodeAttr(AttributeType attrType, IGraphElement elem, out String attrTypeString, out String attrValueString)
        {
            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr)
            {
                EmitHelper.ToString((IDictionary)elem.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
                attrValueString = basicClient.Encode(attrValueString);
            }
            else if(attrType.Kind == AttributeKind.ArrayAttr)
            {
                EmitHelper.ToString((IList)elem.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
                attrValueString = basicClient.Encode(attrValueString);
            }
            else if(attrType.Kind == AttributeKind.DequeAttr)
            {
                EmitHelper.ToString((IDeque)elem.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
                attrValueString = basicClient.Encode(attrValueString);
            }
            else
            {
                EmitHelper.ToString(elem.GetAttribute(attrType.Name), out attrTypeString, out attrValueString, attrType, graph, false, objectNamerAndIndexer, transientObjectNamerAndIndexer, null);
                attrValueString = basicClient.Encode(attrValueString);
            }
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
