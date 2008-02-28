using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An attributed, typed and directed multigraph with multiple inheritance on node and edge types
    /// and uniquely named elements.
    /// </summary>
    public class NamedGraph : IGraph
    {
        private IGraph graph;
        private Dictionary<String, IGraphElement> NameToElem = new Dictionary<String, IGraphElement>();
        private Dictionary<IGraphElement, String> ElemToName = new Dictionary<IGraphElement, String>();
        private NodeAddedHandler onNodeAdded;
        private EdgeAddedHandler onEdgeAdded;
        private bool skipNextEvent = false;

        int nextID = 0;

        private String GetNextName()
        {
            return String.Format("${0,00000000:X}", nextID++);
        }

        /// <summary>
        /// Initializes the name maps with anonymous names in the form "$" + GetHashCode()
        /// </summary>
        /// <param name="somegraph">The graph to be used named</param>
        public NamedGraph(IGraph somegraph)
        {
            graph = somegraph;

            foreach(INode node in graph.Nodes)
            {
                String name = GetNextName();
                NameToElem[name] = node;
                ElemToName[node] = name;
            }

            foreach(IEdge edge in graph.Edges)
            {
                String name = GetNextName();
                NameToElem[name] = edge;
                ElemToName[edge] = name;
            }
        }

        /// <summary>
        /// Initializes the name maps with the names provided in a given attribute each graph element must have
        /// </summary>
        /// <param name="somegraph">The graph to be used named</param>
        /// <param name="nameAttributeName">The name of the attribute</param>
        public NamedGraph(IGraph somegraph, String nameAttributeName)
        {
            graph = somegraph;

            foreach(INode node in graph.Nodes)
            {
                AttributeType attrType = node.Type.GetAttributeType(nameAttributeName);
                if(attrType == null)
                    throw new ArgumentException(String.Format(
                        "Illegal name attribute for node of type {0}!", node.Type.Name));
                if(attrType.Kind != AttributeKind.StringAttr)
                    throw new ArgumentException("Name attribute is not a string attribute in type {0}!", node.Type.Name);

                String name = (String) node.GetAttribute(nameAttributeName);
                if(NameToElem.ContainsKey(name))
                    throw new ArgumentException(String.Format(
                        "The name attributes do not contain unique names (\"{0}\" found twice)!",name));
                NameToElem[name] = node;
                ElemToName[node] = name;
            }

            foreach(IEdge edge in graph.Edges)
            {
                AttributeType attrType = edge.Type.GetAttributeType(nameAttributeName);
                if(attrType == null)
                    throw new ArgumentException(String.Format(
                        "Illegal name attribute for edge of type {0}!", edge.Type.Name));
                if(attrType.Kind != AttributeKind.StringAttr)
                    throw new ArgumentException("Name attribute is not a string attribute in edge type {0}!",
                        edge.Type.Name);

                String name = (String) edge.GetAttribute(nameAttributeName);
                if(NameToElem.ContainsKey(name))
                    throw new ArgumentException(String.Format(
                        "The name attributes do not contain unique names (\"{0}\" found twice)!", name));
                NameToElem[name] = edge;
                ElemToName[edge] = name;
            }
        }

        /// <summary>
        /// Sets the name for a graph element. Any previous name will be overwritten.
        /// </summary>
        /// <param name="elem">The graph element to be named.</param>
        /// <param name="name">The new name for the graph element.</param>
        public void SetElementName(IGraphElement elem, String name)
        {
            if(NameToElem.ContainsKey(name))
                throw new Exception("The name \"" + name + "\" is already in use!");
            String oldName;
            if(ElemToName.TryGetValue(elem, out oldName))
                NameToElem.Remove(oldName);
            NameToElem[name] = elem;
            ElemToName[elem] = name;
        }

        /// <summary>
        /// Gets the name for a graph element. It automatically generates a new name, if the
        /// element does not have a name, yet.
        /// </summary>
        /// <param name="elem">The graph element.</param>
        /// <returns>The name of the graph element.</returns>
        public String GetElementName(IGraphElement elem)
        {
            String name;
            if(!ElemToName.TryGetValue(elem, out name))
            {
                // element has been generated within a rule execution, so give it a name
                name = GetNextName();
                NameToElem[name] = elem;
                ElemToName[elem] = name;
                //                throw new Exception((elem is INode ? "Node" : "Edge") + " of type \"" + elem.Type.Name + "\" has no name!");
            }
            return name;
        }

        // TODO: What happens, if a named element has been removed by a rule and is asked for with GetGraphElement??

        /// <summary>
        /// Gets the graph element for a given name.
        /// </summary>
        /// <param name="name">The name of a graph element.</param>
        /// <returns>The graph element for the given name or null, if there is no graph element with this name.</returns>
        public IGraphElement GetGraphElement(String name)
        {
            IGraphElement elem;
            if(!NameToElem.TryGetValue(name, out elem)) return null;
            return elem;
        }

        /// <summary>
        /// Tries to set the name of an element.
        /// If the name is already used by another element, the element is removed from the graph and
        /// an ArgumentException is thrown.
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="name"></param>
        private void AddElemName(IGraphElement elem, String name)
        {
            if(NameToElem.ContainsKey(name))
            {
                if(elem is INode)
                    graph.Remove((INode) elem);
                else
                    graph.Remove((IEdge) elem);
                throw new ArgumentException(String.Format("The name \"{0}\" is already used!", name));
            }
            NameToElem[name] = elem;
            ElemToName[elem] = name;
        }

        /// <summary>
        /// Adds a new named node to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the new node.</param>
        /// <returns>The newly created node.</returns>
        public INode AddNode(NodeType nodeType, String varName, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            skipNextEvent = true;
            INode node = graph.AddNode(nodeType, varName);
            skipNextEvent = false;

            if(elemName == null)
                elemName = GetNextName();

            NameToElem[elemName] = node;
            ElemToName[node] = elemName;

            NodeAddedHandler nodeAdded = onNodeAdded;
            if(nodeAdded != null) nodeAdded(node);

            return node;
        }

        /// <summary>
        /// Adds a new node to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        public INode AddNode(NodeType nodeType, String varName)
        {
            return AddNode(nodeType, varName, null);
        }

        /// <summary>
        /// Adds a new node to the graph.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <returns>The newly created node.</returns>
        public INode AddNode(NodeType nodeType)
        {
            return AddNode(nodeType, null, null);
        }

        /// <summary>
        /// Adds a new named edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the edge.</param>
        /// <returns>The newly created edge.</returns>
        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            skipNextEvent = true;
            IEdge edge = graph.AddEdge(edgeType, source, target, varName);
            skipNextEvent = false;

            if(elemName == null)
                elemName = GetNextName();

            NameToElem[elemName] = edge;
            ElemToName[edge] = elemName;

            EdgeAddedHandler edgeAdded = onEdgeAdded;
            if(edgeAdded != null) edgeAdded(edge);

            return edge;
        }

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, string varName)
        {
            return AddEdge(edgeType, source, target, varName, null);
        }

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The newly created edge.</returns>
        public IEdge AddEdge(EdgeType edgeType, INode source, INode target)
        {
            return AddEdge(edgeType, source, target, null, null);
        }

        private void RemoveName(IGraphElement elem)
        {
            String name;
            if(ElemToName.TryGetValue(elem, out name))  // has a name been assigned to the element?
            {
                ElemToName.Remove(elem);
                NameToElem.Remove(name);
            }
        }

        /// <summary>
        /// Removes the given node from the graph.
        /// </summary>
        public void Remove(INode node)
        {
            graph.Remove(node);
            RemoveName(node);
        }

        /// <summary>
        /// Removes the given edge from the graph.
        /// </summary>
        public void Remove(IEdge edge)
        {
            graph.Remove(edge);
            RemoveName(edge);
        }

        /// <summary>
        /// Removes all edges from the given node.
        /// </summary>
        public void RemoveEdges(INode node)
        {
            foreach(IEdge edge in node.Incoming) Remove(edge);
            foreach(IEdge edge in node.Outgoing) Remove(edge);
        }

        /// <summary>
        /// Removes all nodes and edges (including any variables pointing to them) from the graph.
        /// </summary>
        public void Clear()
        {
            ElemToName.Clear();
            NameToElem.Clear();
            graph.Clear();
        }

        private void NodeAddedHandler(INode node)
        {
            if(skipNextEvent)
            {
                skipNextEvent = false;
                return;
            }
            NodeAddedHandler nodeAdded = onNodeAdded;
            if(nodeAdded != null) nodeAdded(node);
        }

        private void EdgeAddedHandler(IEdge edge)
        {
            if(skipNextEvent)
            {
                skipNextEvent = false;
                return;
            }
            EdgeAddedHandler edgeAdded = onEdgeAdded;
            if(edgeAdded != null) edgeAdded(edge);
        }

        /// <summary>
        /// Fired after a node has been added
        /// </summary>
        public event NodeAddedHandler OnNodeAdded
        {
            add
            {
                if(onNodeAdded == null)
                    graph.OnNodeAdded += NodeAddedHandler;
                onNodeAdded += value;
            }
            remove
            {
                onNodeAdded -= value;
                if(onNodeAdded == null)
                    graph.OnNodeAdded -= NodeAddedHandler;
            }
        }

        /// <summary>
        /// Fired after an edge has been added
        /// </summary>
        public event EdgeAddedHandler OnEdgeAdded
        {
            add
            {
                if(onEdgeAdded == null)
                    graph.OnEdgeAdded += EdgeAddedHandler;
                onEdgeAdded += value;
            }
            remove
            {
                onEdgeAdded -= value;
                if(onEdgeAdded == null)
                    graph.OnEdgeAdded -= EdgeAddedHandler;
            }
        }

        #region Simply wrapped functions
        /// <summary>
        /// A name associated with the graph.
        /// </summary>
        public String Name { get { return graph.Name; } }

        /// <summary>
        /// The model associated with the graph.
        /// </summary>
        public IGraphModel Model { get { return graph.Model; } }

        /// <summary>
        /// Returns the graph's transaction manager.
        /// For attribute changes using the transaction manager is the only way to include such changes in the transaction history!
        /// Don't forget to call Commit after a transaction is finished!
        /// </summary>
        public ITransactionManager TransactionManager { get { return graph.TransactionManager; } }

        /// <summary>
        /// The writer used by emit statements. By default this is Console.Out.
        /// </summary>
        public TextWriter EmitWriter { get { return graph.EmitWriter; } set { graph.EmitWriter = value; } }

        /// <summary>
        /// If true (the default case), elements deleted during a rewrite
        /// may be reused in the same rewrite.
        /// As a result new elements may not be discriminable anymore from
        /// already deleted elements using object equality, hash maps, etc.
        /// In cases where this is needed this optimization should be disabled.
        /// </summary>
        public bool ReuseOptimization
        {
            get { return graph.ReuseOptimization; }
            set { graph.ReuseOptimization = value; }
        }

        /// <summary>
        /// For persistent backends permanently destroys the graph
        /// </summary>
        public void DestroyGraph() { graph.DestroyGraph(); }

        /// <summary>
        /// Loads a BaseActions instance from the given file, which becomes initialized with the given dumpInfo.
        /// If the file is a ".cs" file it will be compiled first.
        /// If dumpInfo is null, a standard dumpInfo will be used.
        /// </summary>
        public BaseActions LoadActions(String actionFilename, DumpInfo dumpInfo) { return graph.LoadActions(actionFilename, dumpInfo); }


        /// <summary>
        /// The total number of nodes in the graph.
        /// </summary>
        public int NumNodes { get { return graph.NumNodes; } }

        /// <summary>
        /// The total number of edges in the graph.
        /// </summary>
        public int NumEdges { get { return graph.NumEdges; } }

        /// <summary>
        /// Enumerates all nodes in the graph.
        /// </summary>
        public IEnumerable<INode> Nodes { get { return graph.Nodes; } }

        /// <summary>
        /// Enumerates all edges in the graph.
        /// </summary>
        public IEnumerable<IEdge> Edges { get { return graph.Edges; } }


        /// <summary>
        /// Returns the number of nodes with the exact given node type.
        /// </summary>
        public int GetNumExactNodes(NodeType nodeType) { return graph.GetNumExactNodes(nodeType); }

        /// <summary>
        /// Returns the number of edges with the exact given edge type.
        /// </summary>
        public int GetNumExactEdges(EdgeType edgeType) { return graph.GetNumExactEdges(edgeType); }

        /// <summary>
        /// Enumerates all nodes with the exact given node type.
        /// </summary>
        public IEnumerable<INode> GetExactNodes(NodeType nodeType) { return graph.GetExactNodes(nodeType); }

        /// <summary>
        /// Enumerates all edges with the exact given edge type.
        /// </summary>
        public IEnumerable<IEdge> GetExactEdges(EdgeType edgeType) { return graph.GetExactEdges(edgeType); }

        /// <summary>
        /// Returns the number of nodes compatible to the given node type.
        /// </summary>
        public int GetNumCompatibleNodes(NodeType nodeType) { return graph.GetNumCompatibleNodes(nodeType); }

        /// <summary>
        /// Returns the number of edges compatible to the given edge type.
        /// </summary>
        public int GetNumCompatibleEdges(EdgeType edgeType) { return graph.GetNumCompatibleEdges(edgeType); }

        /// <summary>
        /// Enumerates all nodes compatible to the given node type.
        /// </summary>
        public IEnumerable<INode> GetCompatibleNodes(NodeType nodeType) { return graph.GetCompatibleNodes(nodeType); }

        /// <summary>
        /// Enumerates all edges compatible to the given edge type.
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleEdges(EdgeType edgeType) { return graph.GetCompatibleEdges(edgeType); }

        /// <summary>
        /// Retypes a node by creating a new node of the given type.
        /// All adjacent edges as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="node">The node to be retyped.</param>
        /// <param name="newNodeType">The new type for the node.</param>
        /// <returns>The new node object representing the retyped node.</returns>
        public INode Retype(INode node, NodeType newNodeType)
        {
            INode newNode = graph.Retype(node, newNodeType);
            String name = ElemToName[node];
            ElemToName.Remove(node);
            ElemToName[newNode] = name;
            NameToElem[name] = newNode;
            return newNode;
        }

        /// <summary>
        /// Retypes an edge by creating a new edge of the given type.
        /// Source and target node as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="edge">The edge to be retyped.</param>
        /// <param name="newEdgeType">The new type for the edge.</param>
        /// <returns>The new edge object representing the retyped edge.</returns>
        public IEdge Retype(IEdge edge, EdgeType newEdgeType)
        {
            IEdge newEdge = graph.Retype(edge, newEdgeType);
            String name = ElemToName[edge];
            ElemToName.Remove(edge);
            ElemToName[newEdge] = name;
            NameToElem[name] = newEdge;
            return newEdge;
        }

        /// <summary>
        /// Mature a graph.
        /// This method should be invoked after adding all nodes and edges to the graph.
        /// The backend may implement analyses on the graph to speed up matching etc.
        /// The graph may not be modified by this function.
        /// </summary>
        public void Mature() { graph.Mature(); }

        /// <summary>
        /// Does graph-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        public void Custom(params object[] args) { graph.Custom(args); }

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model and backend as the other.
        /// The open transactions will NOT be cloned.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        public IGraph Clone(String newName) { return graph.Clone(newName); }

        /// <summary>
        /// Returns a linked list of variables mapped to the given graph element
        /// or null, if no variable points to this element
        /// </summary>
        public LinkedList<Variable> GetElementVariables(IGraphElement elem) { return graph.GetElementVariables(elem); }

        /// <summary>
        /// Retrieves the IGraphElement for a variable name or null, if the variable isn't set yet or anymore
        /// </summary>
        /// <param name="varName">The variable name to lookup</param>
        /// <returns>The according IGraphElement or null</returns>
        public IGraphElement GetVariableValue(string varName) { return graph.GetVariableValue(varName); }

        /// <summary>
        /// Retrieves the INode for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an INode object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        public INode GetNodeVarValue(string varName)
        {
            return (INode) graph.GetVariableValue(varName);
        }

        /// <summary>
        /// Retrieves the IEdge for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an IEdge object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        public IEdge GetEdgeVarValue(string varName)
        {
            return (IEdge) graph.GetVariableValue(varName);
        }

        /// <summary>
        /// Sets the value of the given variable to the given IGraphElement
        /// If the variable name is null, this function does nothing
        /// If elem is null, the variable is unset
        /// </summary>
        /// <param name="varName">The name of the variable</param>
        /// <param name="element">The new value of the variable</param>
        public void SetVariableValue(string varName, IGraphElement element) { graph.SetVariableValue(varName, element); }

        /// <summary>
        /// Fired before a node is deleted
        /// </summary>
        public event RemovingNodeHandler OnRemovingNode { add { graph.OnRemovingNode += value; } remove { graph.OnRemovingNode -= value; } }

        /// <summary>
        /// Fired before an edge is deleted
        /// </summary>
        public event RemovingEdgeHandler OnRemovingEdge { add { graph.OnRemovingEdge += value; } remove { graph.OnRemovingEdge -= value; } }

        /// <summary>
        /// Fired before all edges of a node are deleted
        /// </summary>
        public event RemovingEdgesHandler OnRemovingEdges { add { graph.OnRemovingEdges += value; } remove { graph.OnRemovingEdges -= value; } }

        /// <summary>
        /// Fired before the whole graph is cleared
        /// </summary>
        public event ClearingGraphHandler OnClearingGraph { add { graph.OnClearingGraph += value; } remove { graph.OnClearingGraph -= value; } }

        /// <summary>
        /// Fired before an attribute of a node is changed.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function 
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangingNodeAttributes.
        /// </summary>
        public event ChangingNodeAttributeHandler OnChangingNodeAttribute
        { add { graph.OnChangingNodeAttribute += value; } remove { graph.OnChangingNodeAttribute -= value; } }

        /// <summary>
        /// Fired before an attribute of an edge is changed.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function 
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangingEdgeAttributes.
        /// </summary>
        public event ChangingEdgeAttributeHandler OnChangingEdgeAttribute
        { add { graph.OnChangingEdgeAttribute += value; } remove { graph.OnChangingEdgeAttribute -= value; } }

        /// <summary>
        /// Fired before the type of a node is changed.
        /// Old and new type and attributes are provided to the handler.
        /// </summary>
        public event RetypingNodeHandler OnRetypingNode { add { graph.OnRetypingNode += value; } remove { graph.OnRetypingNode -= value; } }

        /// <summary>
        /// Fired before the type of an edge is changed.
        /// Old and new type and attributes are provided to the handler.
        /// </summary>
        public event RetypingEdgeHandler OnRetypingEdge { add { graph.OnRetypingEdge += value; } remove { graph.OnRetypingEdge -= value; } }


        /// <summary>
        /// Fires an OnChangingNodeAttribute event. This should be called before an attribute of a node is changed.
        /// </summary>
        /// <param name="node">The node whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="oldValue">The old value of the attribute.</param>
        /// <param name="newValue">The new value of the attribute.</param>
        public void ChangingNodeAttribute(INode node, AttributeType attrType, Object oldValue, Object newValue)
        { graph.ChangingNodeAttribute(node, attrType, oldValue, newValue); }

        /// <summary>
        /// Fires an OnChangingEdgeAttribute event. This should be called before an attribute of a edge is changed.
        /// </summary>
        /// <param name="edge">The edge whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="oldValue">The old value of the attribute.</param>
        /// <param name="newValue">The new value of the attribute.</param>
        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType, Object oldValue, Object newValue)
        { graph.ChangingEdgeAttribute(edge, attrType, oldValue, newValue); }

        /// <summary>
        /// Checks whether a graph meets the connection assertions.
        /// In strict mode all occuring connections must be specified
        /// by a connection assertion.
        /// </summary>
        /// <param name="strict">If false, only check for specified assertions,
        /// otherwise it isn an error, if an edge connects nodes without a
        /// specified connection assertion.</param>
        /// <param name="errors">If the graph is not valid, this refers to a List of ConnectionAssertionError objects, otherwise it is null.</param>
        /// <returns>True, if the graph is valid.</returns>
        public bool Validate(bool strict, out List<ConnectionAssertionError> errors) { return graph.Validate(strict, out errors); }

        /// <summary>
        /// Dumps one or more matches with a given graph dumper.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        /// <param name="dumpInfo">Specifies how the graph shall be dumped.</param>
        /// <param name="matches">An IMatches object containing the matches.</param>
        /// <param name="which">Which match to dump, or AllMatches for dumping all matches
        /// adding connections between them, or OnlyMatches to dump the matches only</param>
        public void DumpMatch(IDumper dumper, DumpInfo dumpInfo, IMatches matches, DumpMatchSpecial which) { graph.DumpMatch(dumper, dumpInfo, matches, which); }

        /// <summary>
        /// Dumps the graph with a given graph dumper.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        /// <param name="dumpInfo">Specifies how the graph shall be dumped.</param>
        public void Dump(IDumper dumper, DumpInfo dumpInfo) { graph.Dump(dumper, dumpInfo); }

        /// <summary>
        /// Dumps the graph with a given graph dumper and default dump style.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        public void Dump(IDumper dumper) { graph.Dump(dumper); }

        #endregion Simply wrapped functions
   }
}