/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

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
    /// A class for recording changes (and their causes) applied to a graph into a file,
    /// so that they can get replayed.
    /// </summary>
    public class Recorder : IRecorder
    {
        /// <summary>
        /// Create a recorder
        /// </summary>
        /// <param name="graph">The graph whose changes are to be recorded; 
        /// should be a NamedGraph for things to run smoothly (same holds for Porter methods)</param>
        public Recorder(IGraph graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Creates a file which initially gets filled with a .grs export of the graph.
        /// Afterwards the changes applied to the graph are recorded into the file,
        /// in the order they occur.
        /// You can start multiple recordings into differently named files.
        /// </summary>
        /// <param name="filename">The name of the file to record to</param>
        public void StartRecording(string filename)
        {
            if(!recordings.ContainsKey(filename))
            {
                if(recordings.Count == 0)
                    SubscribeEvents();

                StreamWriter writer = null;
                if(filename.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase)) {
                    FileStream filewriter = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(new GZipStream(filewriter, CompressionMode.Compress));
                } else {
                    writer = new StreamWriter(filename);
                }

                String pathPrefix = "";
                if(filename.LastIndexOf("/")!=-1 || filename.LastIndexOf("\\")!=-1)
                {
                    int lastIndex = filename.LastIndexOf("/");
                    if(lastIndex==-1) lastIndex = filename.LastIndexOf("\\");
                    pathPrefix = filename.Substring(0, lastIndex+1);
                }
                GRSExport.ExportYouMustCloseStreamWriter(graph, writer, false, pathPrefix);

                recordings.Add(new KeyValuePair<string, StreamWriter>(filename, writer));
            }
        }

        /// <summary>
        /// Stops recording of the changes applied to the graph to the given file.
        /// </summary>
        /// <param name="filename">The name of the file to stop recording to</param>
        public void StopRecording(string filename)
        {
            if(recordings.ContainsKey(filename))
            {
                recordings[filename].Close();
                recordings.Remove(filename);

                if(recordings.Count == 0)
                    UnsubscribeEvents();
            }
        }

        /// <summary>
        /// Returns whether the graph changes get currently recorded into the given file.
        /// </summary>
        /// <param name="filename">The name of the file whose recording status gets queried</param>
        /// <returns>The recording status of the file queried</returns>
        public bool IsRecording(string filename)
        {
            return recordings.ContainsKey(filename);
        }

        /// <summary>
        /// Writes the given string to the currently ongoing recordings
        /// </summary>
        /// <param name="value">The string to write to the recordings</param>
        public void Write(string value)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.Write(value);
        }

        IGraph graph = null;
        private IDictionary<string, StreamWriter> recordings = new Dictionary<string, StreamWriter>();

        private void SubscribeEvents()
        {
            graph.OnNodeAdded += new NodeAddedHandler(NodeAdded);
            graph.OnEdgeAdded += new EdgeAddedHandler(EdgeAdded);
            graph.OnRemovingNode += new RemovingNodeHandler(RemovingNode);
            graph.OnRemovingEdge += new RemovingEdgeHandler(RemovingEdge);
            graph.OnChangingNodeAttribute += new ChangingNodeAttributeHandler(ChangingAttribute);
            graph.OnChangingEdgeAttribute += new ChangingEdgeAttributeHandler(ChangingAttribute);
            graph.OnRetypingNode += new RetypingNodeHandler(RetypingNode);
            graph.OnRetypingEdge += new RetypingEdgeHandler(RetypingEdge);
            graph.OnFinishing += new BeforeFinishHandler(BeforeFinish);
            graph.OnRewritingNextMatch += new RewriteNextMatchHandler(RewriteNextMatch);
            graph.OnFinished += new AfterFinishHandler(AfterFinish);
        }

        private void UnsubscribeEvents()
        {
            graph.OnNodeAdded -= new NodeAddedHandler(NodeAdded);
            graph.OnEdgeAdded -= new EdgeAddedHandler(EdgeAdded);
            graph.OnRemovingNode -= new RemovingNodeHandler(RemovingNode);
            graph.OnRemovingEdge -= new RemovingEdgeHandler(RemovingEdge);
            graph.OnChangingNodeAttribute -= new ChangingNodeAttributeHandler(ChangingAttribute);
            graph.OnChangingEdgeAttribute -= new ChangingEdgeAttributeHandler(ChangingAttribute);
            graph.OnRetypingNode -= new RetypingNodeHandler(RetypingNode);
            graph.OnRetypingEdge -= new RetypingEdgeHandler(RetypingEdge);
            graph.OnFinishing -= new BeforeFinishHandler(BeforeFinish);
            graph.OnRewritingNextMatch += new RewriteNextMatchHandler(RewriteNextMatch);
            graph.OnFinished -= new AfterFinishHandler(AfterFinish);
        }

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Event handler for IGraph.OnNodeAdded.
        /// </summary>
        /// <param name="node">The added node.</param>
        void NodeAdded(INode node)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("new :" + node.Type.Name + "($=\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnEdgeAdded.
        /// </summary>
        /// <param name="edge">The added edge.</param>
        void EdgeAdded(IEdge edge)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("new @(\"" + graph.GetElementName(edge.Source)
                    + "\") -:" + edge.Type.Name + "($=\"" + graph.GetElementName(edge) + "\")-> @(\"" 
                    + graph.GetElementName(edge.Target) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingNode.
        /// </summary>
        /// <param name="node">The node to be deleted.</param>
        void RemovingNode(INode node)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("delete node @(\"" + graph.GetElementName(node) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnRemovingEdge.
        /// </summary>
        /// <param name="edge">The edge to be deleted.</param>
        void RemovingEdge(IEdge edge)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("delete edge @(\"" + graph.GetElementName(edge) + "\")");
        }

        /// <summary>
        /// Event handler for IGraph.OnChangingNodeAttribute and IGraph.OnChangingEdgeAttribute.
        /// </summary>
        /// <param name="element">The node or edge whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.</param>
        void ChangingAttribute(IGraphElement element, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue)
        {
            foreach(StreamWriter writer in recordings.Values)
                switch(changeType)
                {
                case AttributeChangeType.Assign:
                    writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name + " = ");
                    GRSExport.EmitAttribute(attrType, newValue, writer);
                    writer.WriteLine();
                    break;
                case AttributeChangeType.PutElement:
                    writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        writer.Write(".add(");
                        writer.Write(GRSExport.ToString(newValue, attrType.ValueType));
                        writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        writer.Write(".add(");
                        writer.Write(GRSExport.ToString(keyValue, attrType.KeyType));
                        writer.Write(", ");
                        writer.Write(GRSExport.ToString(newValue, attrType.ValueType));
                        writer.WriteLine(")");
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                case AttributeChangeType.RemoveElement:
                    writer.Write("@(\"" + graph.GetElementName(element) + "\")." + attrType.Name);
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        writer.Write(".rem(");
                        writer.Write(GRSExport.ToString(newValue, attrType.ValueType));
                        writer.WriteLine(")");
                        break;
                    case AttributeKind.MapAttr:
                        writer.Write(".rem(");
                        writer.Write(GRSExport.ToString(keyValue, attrType.KeyType));
                        writer.WriteLine(")");
                        break;
                    default:
                         throw new Exception("Wrong attribute type for attribute change type");
                    }
                    break;
                default:
                    throw new Exception("Unknown attribute change type");
                }
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingNode.
        /// </summary>
        /// <param name="oldNode">The node to be retyped.</param>
        /// <param name="newNode">The new node with the common attributes, but without the correct connections, yet.</param>
        void RetypingNode(INode oldNode, INode newNode)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("retype @(\"" + graph.GetElementName(oldNode) + "\")<" + newNode.Type.Name + ">");
        }

        /// <summary>
        /// Event handler for IGraph.OnRetypingEdge.
        /// </summary>
        /// <param name="oldEdge">The edge to be retyped.</param>
        /// <param name="newEdge">The new edge with the common attributes, but without the correct connections, yet.</param>
        void RetypingEdge(IEdge oldEdge, IEdge  newEdge)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("retype -@(\"" + graph.GetElementName(oldEdge) + "\")<" + newEdge.Type.Name + ">->");
        }

        ////////////////////////////////////////////////////////////////////////

        void BeforeFinish(IMatches matches, bool special)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# rewriting " + matches.Producer.Name + "..");
        }

        void RewriteNextMatch()
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# rewriting next match");
        }

        void AfterFinish(IMatches matches, bool special)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# ..rewritten " + matches.Producer.Name);
        }

        ////////////////////////////////////////////////////////////////////////

        public void TransactionStart(int transactionID)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# begin transaction " + transactionID);
        }

        public void TransactionCommit(int transactionID)
        {
            foreach(StreamWriter writer in recordings.Values)
                writer.WriteLine("# commit transaction " + transactionID);
        }

        public void TransactionRollback(int transactionID, bool start)
        {
            if(start)
                foreach(StreamWriter writer in recordings.Values)
                    writer.WriteLine("# rolling back transaction " + transactionID + "..");
            else
                foreach(StreamWriter writer in recordings.Values)
                    writer.WriteLine("# ..rolled back transaction " + transactionID);
        }
    }

    /// <summary>
    /// A partial implementation of the IGraph interface.
    /// </summary>
    public abstract class BaseGraph : IGraph
    {
        #region Abstract and virtual members

        /// <summary>
        /// A name associated with the graph.
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// The model associated with the graph.
        /// </summary>
        public abstract IGraphModel Model { get; }

        /// <summary>
        /// A currently associated actions object.
        /// </summary>
        public abstract BaseActions Actions { get; set; }

        /// <summary>
        /// Returns the graph's transaction manager.
        /// For attribute changes using the transaction manager is the only way to include such changes in the transaction history!
        /// Don't forget to call Commit after a transaction is finished!
        /// </summary>
        public abstract ITransactionManager TransactionManager { get; }

        /// <summary>
        /// If true (the default case), elements deleted during a rewrite
        /// may be reused in the same rewrite.
        /// As a result new elements may not be discriminable anymore from
        /// already deleted elements using object equality, hash maps, etc.
        /// In cases where this is needed this optimization should be disabled.
        /// </summary>
        public abstract bool ReuseOptimization { get; set; }

        /// <summary>
        /// For persistent backends permanently destroys the graph
        /// </summary>
        public abstract void DestroyGraph();

        /// <summary>
        /// Loads a BaseActions instance from the given file.
        /// If the file is a ".cs" file it will be compiled first.
        /// </summary>
        public abstract BaseActions LoadActions(String actionFilename);

        /// <summary>
        /// Returns the number of nodes with the exact given node type.
        /// </summary>
        public abstract int GetNumExactNodes(NodeType nodeType);

        /// <summary>
        /// Returns the number of edges with the exact given edge type.
        /// </summary>
        public abstract int GetNumExactEdges(EdgeType edgeType);

        /// <summary>
        /// Enumerates all nodes with the exact given node type.
        /// </summary>
        public abstract IEnumerable<INode> GetExactNodes(NodeType nodeType);

        /// <summary>
        /// Enumerates all edges with the exact given edge type.
        /// </summary>
        public abstract IEnumerable<IEdge> GetExactEdges(EdgeType edgeType);

        /// <summary>
        /// Returns the number of nodes compatible to the given node type.
        /// </summary>
        public abstract int GetNumCompatibleNodes(NodeType nodeType);

        /// <summary>
        /// Returns the number of edges compatible to the given edge type.
        /// </summary>
        public abstract int GetNumCompatibleEdges(EdgeType edgeType);

        /// <summary>
        /// Enumerates all nodes compatible to the given node type.
        /// </summary>
        public abstract IEnumerable<INode> GetCompatibleNodes(NodeType nodeType);

        /// <summary>
        /// Enumerates all edges compatible to the given edge type.
        /// </summary>
        public abstract IEnumerable<IEdge> GetCompatibleEdges(EdgeType edgeType);

        /// <summary>
        /// Adds an existing INode object to the graph and assigns it to the given variable.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public abstract void AddNode(INode node, String varName);

        /// <summary>
        /// Adds an existing INode object to the graph.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        public abstract void AddNode(INode node);

        /// <summary>
        /// Adds a new node to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        protected abstract INode AddINode(NodeType nodeType, String varName);

        /// <summary>
        /// Adds a new node to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        public INode AddNode(NodeType nodeType, String varName)
        {
            return AddINode(nodeType, varName);
        }

        /// <summary>
        /// Adds a new node to the graph.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <returns>The newly created node.</returns>
        protected abstract INode AddINode(NodeType nodeType);

        /// <summary>
        /// Adds a new node to the graph.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <returns>The newly created node.</returns>
        public INode AddNode(NodeType nodeType)
        {
            return AddINode(nodeType);
        }

        /// <summary>
        /// Adds an existing IEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        public abstract void AddEdge(IEdge edge, String varName);

        /// <summary>
        /// Adds an existing IEdge object to the graph.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        public abstract void AddEdge(IEdge edge);

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        public abstract IEdge AddEdge(EdgeType edgeType, INode source, INode target, string varName);

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The newly created edge.</returns>
        public abstract IEdge AddEdge(EdgeType edgeType, INode source, INode target);

        /// <summary>
        /// Removes the given node from the graph.
        /// </summary>
        public abstract void Remove(INode node);

        /// <summary>
        /// Removes the given edge from the graph.
        /// </summary>
        public abstract void Remove(IEdge edge);

        /// <summary>
        /// Removes all edges from the given node.
        /// </summary>
        public abstract void RemoveEdges(INode node);

        /// <summary>
        /// Removes all nodes and edges (including any variables pointing to them) from the graph.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Retypes a node by creating a new node of the given type.
        /// All incident edges as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="node">The node to be retyped.</param>
        /// <param name="newNodeType">The new type for the node.</param>
        /// <returns>The new node object representing the retyped node.</returns>
        public abstract INode Retype(INode node, NodeType newNodeType);

        /// <summary>
        /// Retypes an edge by creating a new edge of the given type.
        /// Source and target node as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="edge">The edge to be retyped.</param>
        /// <param name="newEdgeType">The new type for the edge.</param>
        /// <returns>The new edge object representing the retyped edge.</returns>
        public abstract IEdge Retype(IEdge edge, EdgeType newEdgeType);

        /// <summary>
        /// Mature a graph.
        /// This method should be invoked after adding all nodes and edges to the graph.
        /// The backend may implement analyses on the graph to speed up matching etc.
        /// The graph may not be modified by this function.
        /// </summary>
        public abstract void Mature();

        /// <summary>
        /// Does graph-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        public abstract void Custom(params object[] args);

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model and backend as the other
        /// The open transactions will NOT be cloned.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        public abstract IGraph Clone(String newName);

        /// <summary>
        /// Allocates a clean visited flag on the graph elements.
        /// If needed the flag is cleared on all graph elements, so this is an O(n) operation.
        /// </summary>
        /// <returns>A visitor ID to be used in
        /// visited conditions in patterns ("if { !visited(elem, id); }"),
        /// visited expressions in evals ("visited(elem, id) = true; b.flag = visited(elem, id) || c.flag; "}
        /// and calls to other visitor functions.</returns>
        public abstract int AllocateVisitedFlag();

        /// <summary>
        /// Frees a visited flag.
        /// This is an O(1) operation.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        public abstract void FreeVisitedFlag(int visitorID);

        /// <summary>
        /// Resets the visited flag with the given ID on all graph elements, if necessary.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag.</param>
        public abstract void ResetVisitedFlag(int visitorID);

        /// <summary>
        /// Sets the visited flag of the given graph element.
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        public abstract void SetVisited(IGraphElement elem, int visitorID, bool visited);

        /// <summary>
        /// Returns whether the given graph element has been visited.
        /// </summary>
        /// <param name="elem">The graph element to be examined.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <returns>True for visited, false for not visited.</returns>
        public abstract bool IsVisited(IGraphElement elem, int visitorID);

        #endregion Abstract and virtual members

        #region Names and Variables management

        /// <summary>
        /// Returns the name for the given element,
        /// i.e. the name defined by the named graph if a named graph is available,
        /// or a hash value string if only a lgpsGraph is available.
        /// </summary>
        /// <param name="elem">Element of which the name is to be found</param>
        /// <returns>The name of the given element</returns>
        public abstract String GetElementName(IGraphElement elem);

        /// <summary>
        /// Returns a linked list of variables mapped to the given graph element
        /// or null, if no variable points to this element
        /// </summary>
        public abstract LinkedList<Variable> GetElementVariables(IGraphElement elem);

        /// <summary>
        /// Retrieves the object for a variable name or null, if the variable isn't set yet or anymore
        /// </summary>
        /// <param name="varName">The variable name to lookup</param>
        /// <returns>The according object or null</returns>
        public abstract object GetVariableValue(String varName);

        /// <summary>
        /// Retrieves the INode for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an INode object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        public INode GetNodeVarValue(string varName)
        {
            return (INode) GetVariableValue(varName);
        }

        /// <summary>
        /// Retrieves the IEdge for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an IEdge object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        public IEdge GetEdgeVarValue(string varName)
        {
            return (IEdge) GetVariableValue(varName);
        }

        /// <summary>
        /// Sets the value of the given variable to the given object.
        /// If the variable name is null, this function does nothing
        /// If elem is null, the variable is unset
        /// </summary>
        /// <param name="varName">The name of the variable</param>
        /// <param name="val">The new value of the variable</param>
        public abstract void SetVariableValue(String varName, object val);

        #endregion Variables management

        #region Graph rewriting

        /// <summary>
        /// A singleton object array used when no elements are returned.
        /// </summary>
        public static readonly object[] NoElems = new object[] { };

        private PerformanceInfo perfInfo = null;
        private int maxMatches = 0;
        private Dictionary<IAction, IAction> actionMapStaticToNewest = new Dictionary<IAction, IAction>();

        /// <summary>
        /// If PerformanceInfo is non-null, this object is used to accumulate information about time, found matches and applied rewrites.
        /// The user is responsible for resetting the PerformanceInfo object.
        /// </summary>
        public PerformanceInfo PerformanceInfo
        {
            get { return perfInfo; }
            set { perfInfo = value; }
        }

        /// <summary>
        /// The maximum number of matches to be returned for a RuleAll sequence element.
        /// If it is zero or less, the number of matches is unlimited.
        /// </summary>
        public int MaxMatches
        {
            get { return maxMatches; }
            set { maxMatches = value; }
        }

        /// <summary>
        /// Retrieves the newest version of an IAction object currently available for this graph.
        /// This may be the given object.
        /// </summary>
        /// <param name="action">The IAction object.</param>
        /// <returns>The newest version of the given action.</returns>
        public IAction GetNewestActionVersion(IAction action)
        {
            IAction newest;
            if(!actionMapStaticToNewest.TryGetValue(action, out newest))
                return action;
            return newest;
        }

        /// <summary>
        /// Sets the newest action version for a static action.
        /// </summary>
        /// <param name="staticAction">The original action generated by GrGen.exe.</param>
        /// <param name="newAction">A new action instance.</param>
        public void SetNewestActionVersion(IAction staticAction, IAction newAction)
        {
            actionMapStaticToNewest[staticAction] = newAction;
        }

        /// <summary>
        /// Executes the modifications of the according rule to the given match/matches.
        /// Fires OnRewritingNextMatch events before each rewrite except for the first one.
        /// </summary>
        /// <param name="matches">The matches object returned by a previous matcher call.</param>
        /// <param name="which">The index of the match in the matches object to be applied,
        /// or -1, if all matches are to be applied.</param>
        /// <returns>A possibly empty array of objects returned by the last applied rewrite.</returns>
        public object[] Replace(IMatches matches, int which)
        {
            object[] retElems = null;
            if(which != -1)
            {
                if(which < 0 || which >= matches.Count)
                    throw new ArgumentOutOfRangeException("\"which\" is out of range!");

                retElems = matches.Producer.Modify(this, matches.GetMatch(which));
                if(PerformanceInfo != null) PerformanceInfo.RewritesPerformed++;
            }
            else
            {
                bool first = true;
                foreach(IMatch match in matches)
                {
                    if(first) first = false;
                    else if(OnRewritingNextMatch != null) OnRewritingNextMatch();
                    retElems = matches.Producer.Modify(this, match);
                    if(PerformanceInfo != null) PerformanceInfo.RewritesPerformed++;
                }
                if(retElems == null) retElems = NoElems;
            }
            return retElems;
        }

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
            int curMaxMatches = (localMaxMatches > 0) ? localMaxMatches : MaxMatches;

            object[] parameters;
            if(paramBindings.ParamVars.Length > 0)
            {
                parameters = paramBindings.Parameters;
                for(int i = 0; i < paramBindings.ParamVars.Length; i++)
                {
                    // If this parameter is not constant, the according ParamVars entry holds the
                    // name of a variable to be used for the parameter.
                    // Otherwise the parameters entry remains unchanged (it already contains the constant)
                    if(paramBindings.ParamVars[i] != null)
                        parameters[i] = paramBindings.ParamVars[i].GetVariableValue(this);
                }
            }
            else parameters = null;

            if(PerformanceInfo != null) PerformanceInfo.StartLocal();
            IMatches matches = paramBindings.Action.Match(this, curMaxMatches, parameters);
            if(PerformanceInfo != null) PerformanceInfo.StopMatch();

            if(OnMatched != null) OnMatched(matches, special);
            if(matches.Count == 0) return 0;

            if(PerformanceInfo != null) PerformanceInfo.MatchesFound += matches.Count;

            if(test) return matches.Count;

            if(OnFinishing != null) OnFinishing(matches, special);

            if(PerformanceInfo != null) PerformanceInfo.StartLocal();
            object[] retElems = Replace(matches, which);
            if(paramBindings.ReturnVars.Length > 0 && paramBindings.ReturnVars[0] != null)
                for(int i = 0; i < paramBindings.ReturnVars.Length; i++)
                    paramBindings.ReturnVars[i].SetVariableValue(retElems[i], this);
            if(PerformanceInfo != null) PerformanceInfo.StopRewrite();

            if(OnFinished != null) OnFinished(matches, special);

            return matches.Count;
        }

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(Sequence sequence)
        {
            if(PerformanceInfo != null) PerformanceInfo.Start();

            bool res = sequence.Apply(this, null);

            if(PerformanceInfo != null) PerformanceInfo.Stop();
            return res;
        }

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(Sequence sequence, SequenceExecutionEnvironment env)
        {
            if (PerformanceInfo != null) PerformanceInfo.Start();

            bool res = sequence.Apply(this, env);

            if (PerformanceInfo != null) PerformanceInfo.Stop();
            return res;
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(Sequence seq)
        {
            return seq.Apply(Clone("clonedGraph"), null);
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(Sequence seq, SequenceExecutionEnvironment env)
        {
            return seq.Apply(Clone("clonedGraph"), env);
        }

        #endregion Graph rewriting

        #region Events

        /// <summary>
        /// Fired after a node has been added
        /// </summary>
        public event NodeAddedHandler OnNodeAdded;

        /// <summary>
        /// Fired after an edge has been added
        /// </summary>
        public event EdgeAddedHandler OnEdgeAdded;

        /// <summary>
        /// Fired before a node is deleted
        /// </summary>
        public event RemovingNodeHandler OnRemovingNode;

        /// <summary>
        /// Fired before an edge is deleted
        /// </summary>
        public event RemovingEdgeHandler OnRemovingEdge;

        /// <summary>
        /// Fired before all edges of a node are deleted
        /// </summary>
        public event RemovingEdgesHandler OnRemovingEdges;

        /// <summary>
        /// Fired before the whole graph is cleared
        /// </summary>
        public event ClearingGraphHandler OnClearingGraph;

        /// <summary>
        /// Fired before an attribute of a node is changed.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function 
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangingNodeAttributes.
        /// </summary>
        public event ChangingNodeAttributeHandler OnChangingNodeAttribute;

        /// <summary>
        /// Fired before an attribute of an edge is changed.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function 
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangingEdgeAttributes.
        /// </summary>
        public event ChangingEdgeAttributeHandler OnChangingEdgeAttribute;

        /// <summary>
        /// Fired before a node is retyped.
        /// Old and new node are provided to the handler.
        /// </summary>
        public event RetypingNodeHandler OnRetypingNode;

        /// <summary>
        /// Fired before an edge is retyped.
        /// Old and new edge are provided to the handler.
        /// </summary>
        public event RetypingEdgeHandler OnRetypingEdge;

        /// <summary>
        /// Fired before each rewrite step (also rewrite steps of subpatterns) to indicate the names
        /// of the nodes added in this rewrite step in order of addition.
        /// </summary>
        public event SettingAddedElementNamesHandler OnSettingAddedNodeNames;

        /// <summary>
        /// Fired before each rewrite step (also rewrite steps of subpatterns) to indicate the names
        /// of the edges added in this rewrite step in order of addition.
        /// </summary>
        public event SettingAddedElementNamesHandler OnSettingAddedEdgeNames;

        /// <summary>
        /// Fired after all requested matches of a rule have been matched.
        /// </summary>
        public event AfterMatchHandler OnMatched;

        /// <summary>
        /// Fired before the rewrite step of a rule, when at least one match has been found.
        /// </summary>
        public event BeforeFinishHandler OnFinishing;

        /// <summary>
        /// Fired before the next match is rewritten. It is not fired before rewriting the first match.
        /// </summary>
        public event RewriteNextMatchHandler OnRewritingNextMatch;

        /// <summary>
        /// Fired after the rewrite step of a rule.
        /// Note, that the given matches object may contain invalid entries,
        /// as parts of the match may have been deleted!
        /// </summary>
        public event AfterFinishHandler OnFinished;

        /// <summary>
        /// Fired when a sequence is entered.
        /// </summary>
        public event EnterSequenceHandler OnEntereringSequence;

        /// <summary>
        /// Fired when a sequence is left.
        /// </summary>
        public event ExitSequenceHandler OnExitingSequence;

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

        /// <summary>
        /// Fires an OnSettingAddedNodeNames event.
        /// </summary>
        public void SettingAddedNodeNames(String[] addedNodeNames)
        {
            SettingAddedElementNamesHandler handler = OnSettingAddedNodeNames;
            if(handler != null) handler(addedNodeNames);
        }

        /// <summary>
        /// Fires an OnSettingAddedEdgeNames event.
        /// </summary>
        public void SettingAddedEdgeNames(String[] addedEdgeNames)
        {
            SettingAddedElementNamesHandler handler = OnSettingAddedEdgeNames;
            if(handler != null) handler(addedEdgeNames);
        }

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
            ChangingNodeAttributeHandler changingElemAttr = OnChangingNodeAttribute;
            if(changingElemAttr != null) changingElemAttr(node, attrType, changeType, newValue, keyValue);
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
            ChangingEdgeAttributeHandler changingElemAttr = OnChangingEdgeAttribute;
            if(changingElemAttr != null) changingElemAttr(edge, attrType, changeType, newValue, keyValue);
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
        /// Fires an OnMatched event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Matched(IMatches matches, bool special)
        {
            AfterMatchHandler handler = OnMatched;
            if(handler != null) handler(matches, special);
        }

        /// <summary>
        /// Fires an OnFinishing event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Finishing(IMatches matches, bool special)
        {
            BeforeFinishHandler handler = OnFinishing;
            if(handler != null) handler(matches, special);
        }

        /// <summary>
        /// Fires an OnRewritingNextMatch event.
        /// </summary>
        public void RewritingNextMatch()
        {
            RewriteNextMatchHandler handler = OnRewritingNextMatch;
            if(handler != null) handler();
        }

        /// <summary>
        /// Fires an OnFinished event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Finished(IMatches matches, bool special)
        {
            AfterFinishHandler handler = OnFinished;
            if(handler != null) handler(matches, special);
        }

        /// <summary>
        /// Fires an OnEnteringSequence event.
        /// </summary>
        /// <param name="seq">The sequence which is entered.</param>
        public void EnteringSequence(Sequence seq)
        {
            EnterSequenceHandler handler = OnEntereringSequence;
            if(handler != null) handler(seq);
        }

        /// <summary>
        /// Fires an OnExitingSequence event.
        /// </summary>
        /// <param name="seq">The sequence which is exited.</param>
        public void ExitingSequence(Sequence seq)
        {
            ExitSequenceHandler handler = OnExitingSequence;
            if(handler != null) handler(seq);
        }

        #endregion Events

        /// <summary>
        /// The total number of nodes in the graph.
        /// </summary>
        public int NumNodes { get { return GetNumCompatibleNodes(Model.NodeModel.RootType); } }

        /// <summary>
        /// The total number of edges in the graph.
        /// </summary>
        public int NumEdges { get { return GetNumCompatibleEdges(Model.EdgeModel.RootType); } }

        /// <summary>
        /// Enumerates all nodes in the graph.
        /// </summary>
        public IEnumerable<INode> Nodes { get { return GetCompatibleNodes(Model.NodeModel.RootType); } }

        /// <summary>
        /// Enumerates all edges in the graph.
        /// </summary>
        public IEnumerable<IEdge> Edges { get { return GetCompatibleEdges(Model.EdgeModel.RootType); } }

        /// <summary>
        /// The writer used by emit statements. By default this is Console.Out.
        /// </summary>
        private TextWriter emitWriter = Console.Out;

        /// <summary>
        /// The writer used by emit statements. By default this is Console.Out.
        /// </summary>
        public TextWriter EmitWriter
        {
            get { return emitWriter; }
            set { emitWriter = value; }
        }

        /// <summary>
        /// The recorder writing the changes applied to the graph to a file
        /// </summary>
        private IRecorder recorder;

        /// <summary>
        /// The recorder writing the changes applied to the graph to a file
        /// </summary>
        public IRecorder Recorder
        {
            get { return recorder; }
            set { recorder = value; }
        }

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

        /// <summary>
        /// Checks whether a graph meets the connection assertions.
        /// </summary>
        /// <param name="mode">The validation mode to apply.</param>
        /// <param name="errors">If the graph is not valid, this refers to a List of ConnectionAssertionError objects, otherwise it is null.</param>
        /// <returns>True, if the graph is valid.</returns>
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
                    result &= ValidateSource(node, valInfo, ref errors, ref checkedOutEdges, ref checkedInEdges);
                }
                // Check incoming count on nodes of target type
                foreach(INode node in GetCompatibleNodes(valInfo.TargetType))
                {
                    result &= ValidateTarget(node, valInfo, ref errors, ref checkedOutEdges, ref checkedInEdges);
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

        bool ValidateSource(INode node, ValidateInfo valInfo, ref List<ConnectionAssertionError> errors,
            ref Dictionary<IEdge, bool> checkedOutEdges, ref Dictionary<IEdge, bool> checkedInEdges)
        {
            bool result = true;

            // Check outgoing edges
            long num = CountOutgoing(node, valInfo.EdgeType, valInfo.TargetType, ref checkedOutEdges);
            if(valInfo.BothDirections)
            {
                long incoming = CountIncoming(node, valInfo.EdgeType, valInfo.TargetType, ref checkedInEdges);
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

        bool ValidateTarget(INode node, ValidateInfo valInfo, ref List<ConnectionAssertionError> errors,
            ref Dictionary<IEdge, bool> checkedOutEdges, ref Dictionary<IEdge, bool> checkedInEdges)
        {
            bool result = true;

            // Check incoming edges
            long num = CountIncoming(node, valInfo.EdgeType, valInfo.SourceType, ref checkedInEdges);
            if(valInfo.BothDirections)
            {
                long outgoing = CountOutgoing(node, valInfo.EdgeType, valInfo.SourceType, ref checkedOutEdges);
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
            ref Dictionary<IEdge, bool> checkedOutEdges)
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
            ref Dictionary<IEdge, bool> checkedInEdges)
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

        private void DumpEdge(IEdge edge, GrColor textColor, GrColor color, IDumper dumper, DumpInfo dumpInfo)
        {
            dumper.DumpEdge(edge.Source, edge.Target, GetElemLabel(edge, dumpInfo), DumpAttributes(edge),
                textColor, color, GrLineStyle.Default);
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
                        null, vnodeTextColor, vnodeColor, vnodeBorderColor, GrNodeShape.Default);
                    int j = 1;
                    foreach(INode node in match.Nodes)
                    {
                        dumper.DumpEdge(virtNode, node, String.Format("node {0}", j++), null, vedgeTextColor, vedgeColor,
                            GrLineStyle.Default);

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
                            GrNodeShape.Default, dumper, dumpInfo);
                    }

                    // Now add the matched edges (possibly including "Not matched" nodes)

                    foreach(IEdge edge in matchedEdges)
                    {
                        if(!matchedNodes.Contains(edge.Source))
                            DumpNode(edge.Source, dumpInfo.GetNodeTypeTextColor(edge.Source.Type),
                                dumpInfo.GetNodeTypeColor(edge.Source.Type),
                                dumpInfo.GetNodeTypeBorderColor(edge.Source.Type),
                                dumpInfo.GetNodeTypeShape(edge.Source.Type), dumper, dumpInfo);

                        if(!matchedNodes.Contains(edge.Target))
                            DumpNode(edge.Target, dumpInfo.GetNodeTypeTextColor(edge.Target.Type),
                                dumpInfo.GetNodeTypeColor(edge.Target.Type),
                                dumpInfo.GetNodeTypeBorderColor(edge.Target.Type),
                                dumpInfo.GetNodeTypeShape(edge.Target.Type), dumper, dumpInfo);

                        GrElemDumpType dumpType;
                        if(multiMatchedEdges.Contains(edge))
                            dumpType = GrElemDumpType.MultiMatched;
                        else
                            dumpType = GrElemDumpType.SingleMatched;

                        DumpEdge(edge, dumpInfo.GetEdgeDumpTypeTextColor(dumpType),
                            dumpInfo.GetEdgeDumpTypeColor(dumpType), dumper, dumpInfo);
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
                if(dc.MatchedEdges != null && dc.MatchedEdges.Contains(edge))
                {
                    GrElemDumpType dumpType;
                    if(dc.MultiMatchedEdges != null && dc.MultiMatchedEdges.Contains(edge))
                        dumpType = GrElemDumpType.MultiMatched;
                    else
                        dumpType = GrElemDumpType.SingleMatched;
                    color = dc.DumpInfo.GetEdgeDumpTypeColor(dumpType);
                    textColor = dc.DumpInfo.GetEdgeDumpTypeTextColor(dumpType);
                }
                else
                {
                    color = dc.DumpInfo.GetEdgeTypeColor(edge.Type);
                    textColor = dc.DumpInfo.GetEdgeTypeTextColor(edge.Type);
                }

                DumpEdge(edge, textColor, color, dc.Dumper, dc.DumpInfo);
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
                shape = GrNodeShape.Default;
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

        /// <summary>
        /// Dumps the current graph and highlights any given matches.
        /// If no match is given, the whole graph is dumped without any changes.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        /// <param name="dumpInfo">Specifies how the graph shall be dumped.</param>
        /// <param name="matches">An IMatches object containing the matches or null, if the graph is to be dumped normally.</param>
        /// <param name="which">Which match to dump, or AllMatches for dumping all matches
        /// adding connections between them, or OnlyMatches to dump the matches only</param>
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

        /// <summary>
        /// Dumps the graph with a given graph dumper.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        /// <param name="dumpInfo">Specifies how the graph shall be dumped.</param>
        public void Dump(IDumper dumper, DumpInfo dumpInfo)
        {
            DumpMatch(dumper, dumpInfo, null, 0);
        }

        /// <summary>
        /// Dumps the graph with a given graph dumper and default dump style.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        public void Dump(IDumper dumper)
        {
            DumpMatch(dumper, new DumpInfo(GetElementName), null, 0);
        }

        #endregion Graph dumping stuff
    }
}
