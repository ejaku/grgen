/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Environment for sequence exection giving access to graph element names, with null user interface
    /// </summary>
    public class SequenceExecutionEnvironmentNamedGraphOnly : SequenceExecutionEnvironment
    {
        NamedGraph namedGraph;

        public SequenceExecutionEnvironmentNamedGraphOnly(NamedGraph namedGraph)
        {
            this.namedGraph = namedGraph;
        }

        /// <summary>
        /// returns the named graph on which the sequence is to be executed, containing the names
        /// </summary>
        public NamedGraph GetNamedGraph()
        {
            return namedGraph;
        }

        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen direction is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        public int ChooseDirection(int direction, Sequence seq)
        {
            return direction;
        }

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        public int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            return seqToExecute;
        }

        /// <summary>
        /// returns the maybe user altered match to execute next for the sequence given
        /// the randomly chosen total match is supplied; the sequence with the rules and matches is supplied
        /// </summary>
        public int ChooseMatch(int totalMatchExecute, SequenceSomeFromSet seq)
        {
            return totalMatchExecute;
        }

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        public int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq)
        {
            return matchToApply;
        }

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        public int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            return randomNumber;
        }

        /// <summary>
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        public object ChooseValue(string type, Sequence seq)
        {
            return null;
        }

        /// <summary>
        /// informs debugger about the end of a loop iteration, so it can display the state at the end of the iteration
        /// </summary>
        public void EndOfIteration(bool continueLoop, Sequence seq)
        {
        }
    }

    /// <summary>
    /// An attributed, typed and directed multigraph with multiple inheritance on node and edge types
    /// and uniquely named elements. This class is a wrapper for an unnamed graph adding names.
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
            String nameCandidate;
            do
            {
                nameCandidate = String.Format("${0,00000000:X}", nextID++);
            }
            while(NameToElem.ContainsKey(nameCandidate));
            return nameCandidate;
        }

        /// <summary>
        /// Initializes the name maps with anonymous names in the form "$" + GetNextName()
        /// </summary>
        /// <param name="somegraph">The graph to be used named</param>
        public NamedGraph(IGraph somegraph)
        {
            graph = somegraph;
            graph.Recorder = new Recorder(this);

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
            graph.Recorder = new Recorder(this);

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
        /// returns the (lgsp) graph this named graph is wrapping
        /// </summary>
        public IGraph WrappedGraph { get { return graph; } }

        /// <summary>
        /// Sets the name for a graph element. Any previous name will be overwritten.
        /// </summary>
        /// <param name="elem">The graph element to be named.</param>
        /// <param name="name">The new name for the graph element.</param>
        public void SetElementName(IGraphElement elem, String name)
        {
            if(DifferentElementWithName(elem, name))
                throw new Exception("The name \"" + name + "\" is already in use!");
            String oldName;
            if(ElemToName.TryGetValue(elem, out oldName))
                NameToElem.Remove(oldName);
            NameToElem[name] = elem;
            ElemToName[elem] = name;
        }

        /// <summary>
        /// Sets a name of the form prefix + number for the graph element,
        /// with number being the first number from 0 on yielding an element name not already available in the graph
        /// </summary>
        public void SetElementPrefixName(IGraphElement element, String prefix)
        {
            Console.WriteLine("Set node prefix name {0}, {1}", element, prefix);
            String name = prefix;
            int curr = 0;
            while (DifferentElementWithName(element, name))
            {
                ++curr;
                name = prefix + curr;
            }
            SetElementName(element, name);
        }

        /// <summary>
        /// returns whether another element than the one given already bears the name
        /// </summary>
        protected bool DifferentElementWithName(IGraphElement elem, String name)
        {
            return (NameToElem.ContainsKey(name)) && (NameToElem[name] != elem);
        }

        /// <summary>
        /// Returns the name for the given element,
        /// i.e. the name defined by the named graph if a named graph is available,
        /// or a hash value string if only a lgpsGraph is available.
        /// </summary>
        /// <param name="elem">Element of which the name is to be found</param>
        /// <returns>The name of the given element</returns>
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
        /// Adds an existing node to the graph, names it, and assigns it to the given variable.
        /// </summary>
        /// <param name="node">The existing node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the new node or null if it is to be auto-generated.</param>
        public void AddNode(INode node, String varName, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            skipNextEvent = true;
            graph.AddNode(node, varName);
            skipNextEvent = false;

            NameToElem[elemName] = node;
            ElemToName[node] = elemName;

            NodeAddedHandler nodeAdded = onNodeAdded;
            if(nodeAdded != null) nodeAdded(node);
        }

        /// <summary>
        /// Adds a new named node to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the new node or null if it is to be auto-generated.</param>
        /// <returns>The newly created node.</returns>
        public INode AddNode(NodeType nodeType, String varName, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            skipNextEvent = true;
            INode node = graph.AddNode(nodeType, varName);
            skipNextEvent = false;

            NameToElem[elemName] = node;
            ElemToName[node] = elemName;

            NodeAddedHandler nodeAdded = onNodeAdded;
            if(nodeAdded != null) nodeAdded(node);

            return node;
        }

        /// <summary>
        /// Adds an existing INode object to the graph and assigns it to the given variable.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddNode(INode node, String varName)
        {
            AddNode(node, varName, null);
        }

        /// <summary>
        /// Adds an existing INode object to the graph.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        public void AddNode(INode node)
        {
            AddNode(node, null, null);
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
        /// Adds an existing edge to the graph, names it, and assigns it to the given variable.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the edge or null if it is to be auto-generated.</param>
        /// <returns>The newly created edge.</returns>
        public void AddEdge(IEdge edge, String varName, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            skipNextEvent = true;
            graph.AddEdge(edge, varName);
            skipNextEvent = false;

            NameToElem[elemName] = edge;
            ElemToName[edge] = elemName;

            EdgeAddedHandler edgeAdded = onEdgeAdded;
            if(edgeAdded != null) edgeAdded(edge);
        }

        /// <summary>
        /// Adds a new named edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="elemName">The name for the edge or null if it is to be auto-generated.</param>
        /// <returns>The newly created edge.</returns>
        public IEdge AddEdge(EdgeType edgeType, INode source, INode target, String varName, String elemName)
        {
            if(elemName != null && NameToElem.ContainsKey(elemName))
                throw new ArgumentException("The name \"" + elemName + "\" is already used!");

            if(elemName == null)
                elemName = GetNextName();

            skipNextEvent = true;
            IEdge edge = graph.AddEdge(edgeType, source, target, varName);
            skipNextEvent = false;

            NameToElem[elemName] = edge;
            ElemToName[edge] = elemName;

            EdgeAddedHandler edgeAdded = onEdgeAdded;
            if(edgeAdded != null) edgeAdded(edge);

            return edge;
        }

        /// <summary>
        /// Adds an existing IEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public void AddEdge(IEdge edge, String varName)
        {
            AddEdge(edge, varName, null);
        }

        /// <summary>
        /// Adds an existing IEdge object to the graph.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        public void AddEdge(IEdge edge)
        {
            AddEdge(edge, null, null);
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
        /// A currently associated actions object.
        /// </summary>
        public BaseActions Actions { get { return graph.Actions; } set { graph.Actions = value; } }

        /// <summary>
        /// Returns the transaction manager of the graph.
        /// For attribute changes using the transaction manager is the only way to include such changes in the transaction history!
        /// Don't forget to call Commit after a transaction is finished!
        /// </summary>
        public ITransactionManager TransactionManager { get { return graph.TransactionManager; } }

        /// <summary>
        /// The recorder writing the changes applied to the graph to a file
        /// </summary>
        public IRecorder Recorder { get { return graph.Recorder; } set { graph.Recorder = value; } }

        /// <summary>
        /// If PerformanceInfo is non-null, this object is used to accumulate information about time, found matches and applied rewrites.
        /// The user is responsible for resetting the PerformanceInfo object.
        /// </summary>
        public PerformanceInfo PerformanceInfo { get { return graph.PerformanceInfo; } set { graph.PerformanceInfo = value; } }

        /// <summary>
        /// The writer used by emit statements. By default this is Console.Out.
        /// </summary>
        public TextWriter EmitWriter { get { return graph.EmitWriter; } set { graph.EmitWriter = value; } }

        /// <summary>
        /// The maximum number of matches to be returned for a RuleAll sequence element.
        /// If it is zero or less, the number of matches is unlimited.
        /// </summary>
        public int MaxMatches { get { return graph.MaxMatches; } set { graph.MaxMatches = value; } }

        /// <summary>
        /// If true (the default case), elements deleted during a rewrite
        /// may be reused in the same rewrite.
        /// As a result new elements may not be discriminable anymore from
        /// already deleted elements using object equality, hash maps, etc.
        /// In cases where this is needed this optimization should be disabled.
        /// </summary>
        public bool ReuseOptimization { get { return graph.ReuseOptimization; } set { graph.ReuseOptimization = value; } }

        /// <summary>
        /// For persistent backends permanently destroys the graph
        /// </summary>
        public void DestroyGraph() { graph.DestroyGraph(); }

        /// <summary>
        /// Loads a BaseActions instance from the given file.
        /// If the file is a ".cs" file it will be compiled first.
        /// </summary>
        public BaseActions LoadActions(String actionFilename) { return graph.LoadActions(actionFilename); }


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
        /// All incident edges as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="node">The node to be retyped.</param>
        /// <param name="newNodeType">The new type for the node.</param>
        /// <returns>The new node object representing the retyped node.</returns>
        public INode Retype(INode node, NodeType newNodeType)
        {
            return graph.Retype(node, newNodeType);
        }

        /// <summary>
        /// An element gets retyped, i.e. a new element created from an old element;
        /// give the new element the name of the old element, step 1.
        /// (high level retyping is low level delete and create, reconnecting incident stuff and copying attributes)
        /// </summary>
        /// <param name="oldElem">The old element, which gets retyped.</param>
        /// <param name="newElem">The new element, the result of retyping.</param>
        public void Retyping(IGraphElement oldElem, IGraphElement newElem)
        {
            String name;
            if(ElemToName.TryGetValue(oldElem, out name))  // has a name been assigned to the element?
            {
                ElemToName[newElem] = name;
            }
        }

        /// <summary>
        /// An element was retyped, i.e. a new element created from an old element;
        /// give the new element the name of the old element, step 2.
        /// (high level retyping is low level delete and create, reconnecting incident stuff and copying attributes)
        /// </summary>
        /// <param name="oldElem">The old element, which was retyped.</param>
        /// <param name="newElem">The new element, the result of retyping.</param>
        public void Retyped(IGraphElement oldElem, IGraphElement newElem)
        {
            String name;
            if(ElemToName.TryGetValue(oldElem, out name))  // has a name been assigned to the element?
            {
                ElemToName.Remove(oldElem);
                NameToElem[name] = newElem;
            }
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
            return graph.Retype(edge, newEdgeType);
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
        /// Allocates a clean visited flag on the graph elements.
        /// If needed the flag is cleared on all graph elements, so this is an O(n) operation.
        /// </summary>
        /// <returns>A visitor ID to be used in
        /// visited conditions in patterns ("if { !visited(elem, id); }"),
        /// visited expressions in evals ("visited(elem, id) = true; b.flag = visited(elem, id) || c.flag; "}
        /// and calls to other visitor functions.</returns>
        public int AllocateVisitedFlag() { return graph.AllocateVisitedFlag(); }

        /// <summary>
        /// Frees a visited flag.
        /// This is an O(1) operation.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        public void FreeVisitedFlag(int visitorID) { graph.FreeVisitedFlag(visitorID); }

        /// <summary>
        /// Resets the visited flag with the given ID on all graph elements, if necessary.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag.</param>
        public void ResetVisitedFlag(int visitorID) { graph.ResetVisitedFlag(visitorID); }

        /// <summary>
        /// Sets the visited flag of the given graph element.
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        public void SetVisited(IGraphElement elem, int visitorID, bool visited)
        { graph.SetVisited(elem, visitorID, visited); }

        /// <summary>
        /// Returns whether the given graph element has been visited.
        /// </summary>
        /// <param name="elem">The graph element to be examined.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <returns>True for visited, false for not visited.</returns>
        public bool IsVisited(IGraphElement elem, int visitorID) { return graph.IsVisited(elem, visitorID); }

        /// <summary>
        /// Returns a linked list of variables mapped to the given graph element
        /// or null, if no variable points to this element
        /// </summary>
        public LinkedList<Variable> GetElementVariables(IGraphElement elem) { return graph.GetElementVariables(elem); }

        /// <summary>
        /// Retrieves the object for a variable name or null, if the variable isn't set yet or anymore
        /// </summary>
        /// <param name="varName">The variable name to lookup</param>
        /// <returns>The according object or null</returns>
        public object GetVariableValue(string varName) { return graph.GetVariableValue(varName); }

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
        /// Sets the value of the given variable to the given object.
        /// If the variable name is null, this function does nothing
        /// If elem is null, the variable is unset
        /// </summary>
        /// <param name="varName">The name of the variable</param>
        /// <param name="val">The new value of the variable</param>
        public void SetVariableValue(string varName, object val) { graph.SetVariableValue(varName, val); }

        /// <summary>
        /// Executes the modifications of the according rule to the given match/matches.
        /// Fires OnRewritingNextMatch events before each rewrite except for the first one.
        /// </summary>
        /// <param name="matches">The matches object returned by a previous matcher call.</param>
        /// <param name="which">The index of the match in the matches object to be applied,
        /// or -1, if all matches are to be applied.</param>
        /// <returns>A possibly empty array of objects returned by the last applied rewrite.</returns>
        public object[] Replace(IMatches matches, int which) { return graph.Replace(matches, which); }

        /// <summary>
        /// Apply a rewrite rule.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule invocation</param>
        /// <param name="which">The index of the match to be rewritten or -1 to rewrite all matches</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on MaxMatches)</param>
        /// <param name="special">Specifies whether the %-modifier has been used for this rule, which may have a special meaning for
        /// the application</param>
        /// <param name="test">If true, no rewrite step is performed.</param>
        /// <returns>The number of matches found</returns>
        public int ApplyRewrite(RuleInvocationParameterBindings paramBindings, int which, int localMaxMatches, bool special, bool test)
        {
            return graph.ApplyRewrite(paramBindings, which, localMaxMatches, special, test);
        }

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(Sequence sequence)
        {
            SequenceExecutionEnvironment env = new SequenceExecutionEnvironmentNamedGraphOnly(this);
            return graph.ApplyGraphRewriteSequence(sequence, env);
        }

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <param name="env">The execution environment giving access to the names and user interface</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(Sequence sequence, SequenceExecutionEnvironment env)
        {
            return graph.ApplyGraphRewriteSequence(sequence, env);
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(Sequence seq)
        {
            SequenceExecutionEnvironment env = new SequenceExecutionEnvironmentNamedGraphOnly(this);
            return graph.ValidateWithSequence(seq, env);
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <param name="env">The execution environment giving access to the names and user interface</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(Sequence seq, SequenceExecutionEnvironment env)
        {
            return graph.ValidateWithSequence(seq, env);
        }

        /// <summary>
        /// Retrieves the newest version of an IAction object currently available for this graph.
        /// This may be the given object.
        /// </summary>
        /// <param name="action">The IAction object.</param>
        /// <returns>The newest version of the given action.</returns>
        public IAction GetNewestActionVersion(IAction action)
        {
            return graph.GetNewestActionVersion(action);
        }

        /// <summary>
        /// Sets the newest action version for a static action.
        /// </summary>
        /// <param name="staticAction">The original action generated by GrGen.exe.</param>
        /// <param name="newAction">A new action instance.</param>
        public void SetNewestActionVersion(IAction staticAction, IAction newAction)
        {
            graph.SetNewestActionVersion(staticAction, newAction);
        }


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
        /// Fired before each rewrite step (also rewrite steps of subpatterns) to indicate the names
        /// of the nodes added in this rewrite step in order of addition.
        /// </summary>
        public event SettingAddedElementNamesHandler OnSettingAddedNodeNames
        { add { graph.OnSettingAddedNodeNames += value; } remove { graph.OnSettingAddedNodeNames -= value; } }

        /// <summary>
        /// Fired before each rewrite step (also rewrite steps of subpatterns) to indicate the names
        /// of the edges added in this rewrite step in order of addition.
        /// </summary>
        public event SettingAddedElementNamesHandler OnSettingAddedEdgeNames
        { add { graph.OnSettingAddedEdgeNames += value; } remove { graph.OnSettingAddedEdgeNames -= value; } }

        /// <summary>
        /// Fired after all requested matches of a rule have been matched.
        /// </summary>
        public event AfterMatchHandler OnMatched { add { graph.OnMatched += value; } remove { graph.OnMatched -= value; } }

        /// <summary>
        /// Fired before the rewrite step of a rule, when at least one match has been found.
        /// </summary>
        public event BeforeFinishHandler OnFinishing { add { graph.OnFinishing += value; } remove { graph.OnFinishing -= value; } }

        /// <summary>
        /// Fired before the next match is rewritten. It is not fired before rewriting the first match.
        /// </summary>
        public event RewriteNextMatchHandler OnRewritingNextMatch { add { graph.OnRewritingNextMatch += value; } remove { graph.OnRewritingNextMatch -= value; } }

        /// <summary>
        /// Fired after the rewrite step of a rule.
        /// Note, that the given matches object may contain invalid entries,
        /// as parts of the match may have been deleted!
        /// </summary>
        public event AfterFinishHandler OnFinished { add { graph.OnFinished += value; } remove { graph.OnFinished -= value; } }

        /// <summary>
        /// Fired when a sequence is entered.
        /// </summary>
        public event EnterSequenceHandler OnEntereringSequence { add { graph.OnEntereringSequence += value; } remove { graph.OnEntereringSequence -= value; } }

        /// <summary>
        /// Fired when a sequence is left.
        /// </summary>
        public event ExitSequenceHandler OnExitingSequence { add { graph.OnExitingSequence += value; } remove { graph.OnExitingSequence -= value; } }

        /// <summary>
        /// Fires an OnChangingNodeAttribute event.
        /// To be called before changing an attribute of a node,
        /// with exact information about the change to occur,
        /// to allow rollback of changes, in case a transaction is underway.
        /// </summary>
        /// <param name="node">The node whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.</param>
        public void ChangingNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            graph.ChangingNodeAttribute(node, attrType, changeType, newValue, keyValue);
        }

        /// <summary>
        /// Fires an OnChangingEdgeAttribute event.
        /// To be called before changing an attribute of an edge,
        /// with exact information about the change to occur,
        /// to allow rollback of changes, in case a transaction is underway.
        /// </summary>
        /// <param name="edge">The edge whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.</param>
        public void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            graph.ChangingEdgeAttribute(edge, attrType, changeType, newValue, keyValue);
        }

        /// <summary>
        /// Fires an OnMatched event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Matched(IMatches matches, bool special)
        { graph.Matched(matches, special); }

        /// <summary>
        /// Fires an OnFinishing event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Finishing(IMatches matches, bool special)
        { graph.Finishing(matches, special); }

        /// <summary>
        /// Fires an OnRewritingNextMatch event.
        /// </summary>
        public void RewritingNextMatch()
        { graph.RewritingNextMatch(); }

        /// <summary>
        /// Fires an OnFinished event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Finished(IMatches matches, bool special)
        { graph.Finished(matches, special); }

        /// <summary>
        /// Fires an OnEnteringSequence event.
        /// </summary>
        /// <param name="seq">The sequence which is entered.</param>
        public void EnteringSequence(Sequence seq)
        { graph.EnteringSequence(seq); }

        /// <summary>
        /// Fires an OnExitingSequence event.
        /// </summary>
        /// <param name="seq">The sequence which is exited.</param>
        public void ExitingSequence(Sequence seq)
        { graph.ExitingSequence(seq); }

        /// <summary>
        /// Checks whether a graph meets the connection assertions.
        /// </summary>
        /// <param name="mode">The validation mode to apply.</param>
        /// <param name="errors">If the graph is not valid, this refers to a List of ConnectionAssertionError objects, otherwise it is null.</param>
        /// <returns>True, if the graph is valid.</returns>
        public bool Validate(ValidationMode mode, out List<ConnectionAssertionError> errors) { return graph.Validate(mode, out errors); }

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
