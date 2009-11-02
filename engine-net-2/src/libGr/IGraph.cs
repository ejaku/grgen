/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A named variable.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The value pointed to by the variable.
        /// </summary>
        public object Value;

        /// <summary>
        /// Initializes a Variable instance.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value pointed to by the variable.</param>
        public Variable(String name, object value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// The changes which might occur to graph element attributes.
    /// </summary>
    public enum AttributeChangeType
    {
        /// <summary>
        /// Assignment of a value to some attribute.
        /// Value semantics, even if assigned attribute is a set or a map, not a primitive type.
        /// </summary>
        Assign,

        /// <summary>
        /// Inserting a value into some set or a key value pair into some map.
        /// </summary>
        PutElement,

        /// <summary>
        /// Removing a value from some set or a key value pair from some map.
        /// </summary>
        RemoveElement
    }

    /// <summary>
    /// An interface for managing graph transactions.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Starts a transaction
        /// </summary>
        /// <returns>A transaction ID to be used with Commit or Rollback</returns>
        int StartTransaction();

        /// <summary>
        /// Removes the rollback data and stops this transaction
        /// </summary>
        /// <param name="transactionID">Transaction ID returned by a StartTransaction call</param>
        void Commit(int transactionID);

        /// <summary>
        /// Undoes all changes during a transaction
        /// </summary>
        /// <param name="transactionID">The ID of the transaction to be rollbacked</param>
        void Rollback(int transactionID);

        /// <summary>
        /// Event handler for IGraph.OnNodeAdded and IGraph.OnEdgeAdded.
        /// </summary>
        /// <param name="elem">The added element.</param>
        void ElementAdded(IGraphElement elem);

        /// <summary>
        /// Event handler for IGraph.OnRemovingNode and IGraph.OnRemovingEdge.
        /// </summary>
        /// <param name="elem">The element to be deleted.</param>
        void RemovingElement(IGraphElement elem);

        /// <summary>
        /// Event handler for IGraph.OnChangingNodeAttribute and IGraph.OnChangingEdgeAttribute.
        /// </summary>
        /// <param name="elem">The element whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.</param>
        void ChangingElementAttribute(IGraphElement elem, AttributeType attrType,
                AttributeChangeType changeType, Object newValue, Object keyValue);

        /// <summary>
        /// Event handler for IGraph.OnRetypingNode and IGraph.OnRetypingEdge.
        /// </summary>
        /// <param name="oldElem">The element to be retyped.</param>
        /// <param name="newElem">The new element with the common attributes, but without the correct connections, yet.</param>
        void RetypingElement(IGraphElement oldElem, IGraphElement newElem);

        /// <summary>
        /// Indicates, whether a transaction is currently active.
        /// </summary>
        bool TransactionActive { get; }

//        ITransactionManager Clone(Dictionary<IGraphElement, IGraphElement> oldToNewMap);
    }

    #region GraphDelegates

    /// <summary>
    /// Represents a method called, when a node has been added.
    /// </summary>
    /// <param name="node">The added node.</param>
    public delegate void NodeAddedHandler(INode node);

    /// <summary>
    /// Represents a method called, when an edge has been added.
    /// </summary>
    /// <param name="edge">The added edge.</param>
    public delegate void EdgeAddedHandler(IEdge edge);

    /// <summary>
    /// Represents a method called before a node is removed.
    /// </summary>
    /// <param name="node">The node to be removed.</param>
    public delegate void RemovingNodeHandler(INode node);

    /// <summary>
    /// Represents a method called before a edge is removed.
    /// </summary>
    /// <param name="edge">The edge to be removed.</param>
    public delegate void RemovingEdgeHandler(IEdge edge);

    /// <summary>
    /// Represents a method called before all edges of a node are removed.
    /// </summary>
    /// <param name="node">The node whose edges are to be removed.</param>
    public delegate void RemovingEdgesHandler(INode node);

    /// <summary>
    /// Represents a method called before a graph is cleared.
    /// </summary>
    public delegate void ClearingGraphHandler();

    /// <summary>
    /// Represents a method called just before a node attribute is changed,
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
    public delegate void ChangingNodeAttributeHandler(INode node, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue);

    /// <summary>
    /// Represents a method called just before an edge attribute is changed,
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
    public delegate void ChangingEdgeAttributeHandler(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue);

    /// <summary>
    /// Represents a method called before a node is retyped.
    /// </summary>
    /// <param name="oldNode">The node to be retyped.</param>
    /// <param name="newNode">The new node with the common attributes, but without any adjacent edges assigned, yet.</param>
    public delegate void RetypingNodeHandler(INode oldNode, INode newNode);

    /// <summary>
    /// Represents a method called before a edge is retyped.
    /// </summary>
    /// <param name="oldEdge">The edge to be retyped.</param>
    /// <param name="newEdge">The new edge with the common attributes, but not fully connected with the adjacent nodes, yet.</param>
    public delegate void RetypingEdgeHandler(IEdge oldEdge, IEdge newEdge);

    /// <summary>
    /// Delegate-type called shortly before elements are added to the graph, with the names of the elements added.
    /// </summary>
    public delegate void SettingAddedElementNamesHandler(String[] namesOfElementsAdded);

    /// <summary>
    /// Represents a method called after all requested matches of an action have been matched.
    /// </summary>
    /// <param name="matches">The matches found.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void AfterMatchHandler(IMatches matches, bool special);

    /// <summary>
    /// Represents a method called before the rewrite step of an action, when at least one match has been found.
    /// </summary>
    /// <param name="matches">The matches found.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void BeforeFinishHandler(IMatches matches, bool special);

    /// <summary>
    /// Represents a method called during rewriting a set of matches before the next match is rewritten.
    /// It is not fired before rewriting the first match.
    /// </summary>
    public delegate void RewriteNextMatchHandler();

    /// <summary>
    /// Represents a method called after the rewrite step of a rule.
    /// </summary>
    /// <param name="matches">The matches found.
    /// This may contain invalid entries, because parts of the matches may have been deleted.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void AfterFinishHandler(IMatches matches, bool special);

    /// <summary>
    /// Represents a method called directly after a sequence has been entered.
    /// </summary>
    /// <param name="seq">The current sequence object.</param>
    public delegate void EnterSequenceHandler(Sequence seq);

    /// <summary>
    /// Represents a method called before a sequence is left.
    /// </summary>
    /// <param name="seq">The current sequence object.</param>
    public delegate void ExitSequenceHandler(Sequence seq);

    #endregion GraphDelegates

    /// <summary>
    /// An attributed, typed and directed multigraph with multiple inheritance on node and edge types.
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// A name associated with the graph.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The model associated with the graph.
        /// </summary>
        IGraphModel Model { get; }

        /// <summary>
        /// A currently associated actions object.
        /// </summary>
        BaseActions Actions { get; set; }

        /// <summary>
        /// Returns the graph's transaction manager.
        /// For attribute changes using the transaction manager is the only way to include such changes in the transaction history!
        /// Don't forget to call Commit after a transaction is finished!
        /// </summary>
        ITransactionManager TransactionManager { get; }

        /// <summary>
        /// If PerformanceInfo is non-null, this object is used to accumulate information about time, found matches and applied rewrites.
        /// By default it should be null.
        /// The user is responsible for resetting the PerformanceInfo object.
        /// </summary>
        PerformanceInfo PerformanceInfo { get; set; }

        /// <summary>
        /// The writer used by emit statements. By default this is Console.Out.
        /// </summary>
        TextWriter EmitWriter { get; set; }

        /// <summary>
        /// The maximum number of matches to be returned for a RuleAll sequence element.
        /// If it is zero or less, the number of matches is unlimited.
        /// </summary>
        int MaxMatches { get; set; }

        /// <summary>
        /// If true (the default case), elements deleted during a rewrite
        /// may be reused in the same rewrite.
        /// As a result new elements may not be discriminable anymore from
        /// already deleted elements using object equality, hash maps, etc.
        /// In cases where this is needed this optimization should be disabled.
        /// </summary>
        bool ReuseOptimization { get; set; }

        /// <summary>
        /// For persistent backends permanently destroys the graph.
        /// </summary>
        void DestroyGraph();

        /// <summary>
        /// Loads a BaseActions instance from the given file.
        /// If the file is a ".cs" file it will be compiled first.
        /// </summary>
        BaseActions LoadActions(String actionFilename);


        /// <summary>
        /// The total number of nodes in the graph.
        /// </summary>
        int NumNodes { get; }

        /// <summary>
        /// The total number of edges in the graph.
        /// </summary>
        int NumEdges { get; }

        /// <summary>
        /// Enumerates all nodes in the graph.
        /// </summary>
        IEnumerable<INode> Nodes { get; }

        /// <summary>
        /// Enumerates all edges in the graph.
        /// </summary>
        IEnumerable<IEdge> Edges { get; }


        /// <summary>
        /// Returns the number of nodes with the exact given node type.
        /// </summary>
        int GetNumExactNodes(NodeType nodeType);

        /// <summary>
        /// Returns the number of edges with the exact given edge type.
        /// </summary>
        int GetNumExactEdges(EdgeType edgeType);

        /// <summary>
        /// Enumerates all nodes with the exact given node type.
        /// </summary>
        IEnumerable<INode> GetExactNodes(NodeType nodeType);

        /// <summary>
        /// Enumerates all edges with the exact given edge type.
        /// </summary>
        IEnumerable<IEdge> GetExactEdges(EdgeType edgeType);

        /// <summary>
        /// Returns the number of nodes compatible to the given node type.
        /// </summary>
        int GetNumCompatibleNodes(NodeType nodeType);

        /// <summary>
        /// Returns the number of edges compatible to the given edge type.
        /// </summary>
        int GetNumCompatibleEdges(EdgeType edgeType);

        /// <summary>
        /// Enumerates all nodes compatible to the given node type.
        /// </summary>
        IEnumerable<INode> GetCompatibleNodes(NodeType nodeType);

        /// <summary>
        /// Enumerates all edges compatible to the given edge type.
        /// </summary>
        IEnumerable<IEdge> GetCompatibleEdges(EdgeType edgeType);

        /// <summary>
        /// Adds an existing INode object to the graph and assigns it to the given variable.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        void AddNode(INode node, String varName);

        /// <summary>
        /// Adds an existing INode object to the graph.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        void AddNode(INode node);

        /// <summary>
        /// Adds a new node to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created node.</returns>
        INode AddNode(NodeType nodeType, String varName);

        /// <summary>
        /// Adds a new node to the graph.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <returns>The newly created node.</returns>
        INode AddNode(NodeType nodeType);

        /// <summary>
        /// Adds an existing IEdge object to the graph and assigns it to the given variable.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="varName">The name of the variable.</param>
        void AddEdge(IEdge edge, String varName);

        /// <summary>
        /// Adds an existing IEdge object to the graph.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        void AddEdge(IEdge edge);

        /// <summary>
        /// Adds a new edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="varName">The name of the variable.</param>
        /// <returns>The newly created edge.</returns>
        IEdge AddEdge(EdgeType edgeType, INode source, INode target, string varName);

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <returns>The newly created edge.</returns>
        IEdge AddEdge(EdgeType edgeType, INode source, INode target);

        /// <summary>
        /// Removes the given node from the graph.
        /// </summary>
        void Remove(INode node);

        /// <summary>
        /// Removes the given edge from the graph.
        /// </summary>
        void Remove(IEdge edge);

        /// <summary>
        /// Removes all edges from the given node.
        /// </summary>
        void RemoveEdges(INode node);

        /// <summary>
        /// Removes all nodes and edges (including any variables pointing to them) from the graph.
        /// </summary>
        void Clear();

        /// <summary>
        /// Retypes a node by creating a new node of the given type.
        /// All adjacent edges as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="node">The node to be retyped.</param>
        /// <param name="newNodeType">The new type for the node.</param>
        /// <returns>The new node object representing the retyped node.</returns>
        INode Retype(INode node, NodeType newNodeType);

        /// <summary>
        /// Retypes an edge by creating a new edge of the given type.
        /// Source and target node as well as all attributes from common super classes are kept.
        /// </summary>
        /// <param name="edge">The edge to be retyped.</param>
        /// <param name="newEdgeType">The new type for the edge.</param>
        /// <returns>The new edge object representing the retyped edge.</returns>
        IEdge Retype(IEdge edge, EdgeType newEdgeType);

        /// <summary>
        /// Mature a graph.
        /// This method should be invoked after adding all nodes and edges to the graph.
        /// The backend may implement analyses on the graph to speed up matching etc.
        /// The graph may not be modified by this function.
        /// </summary>
        void Mature();

        /// <summary>
        /// Does graph-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        void Custom(params object[] args);

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model and backend as the other.
        /// The open transactions will NOT be cloned.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        IGraph Clone(String newName);

        #region Visited flags management

        /// <summary>
        /// Allocates a clean visited flag on the graph elements.
        /// If needed the flag is cleared on all graph elements, so this is an O(n) operation.
        /// </summary>
        /// <returns>A visitor ID to be used in
        /// visited conditions in patterns ("if { !visited(elem, id); }"),
        /// visited expressions in evals ("visited(elem, id) = true; b.flag = visited(elem, id) || c.flag; "}
        /// and calls to other visitor functions.</returns>
        int AllocateVisitedFlag();

        /// <summary>
        /// Frees a visited flag.
        /// This is an O(1) operation.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        void FreeVisitedFlag(int visitorID);

        /// <summary>
        /// Resets the visited flag with the given ID on all graph elements, if necessary.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag.</param>
        void ResetVisitedFlag(int visitorID);

        /// <summary>
        /// Sets the visited flag of the given graph element.
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        void SetVisited(IGraphElement elem, int visitorID, bool visited);

        /// <summary>
        /// Returns whether the given graph element has been visited.
        /// </summary>
        /// <param name="elem">The graph element to be examined.</param>
        /// <param name="visitorID">The ID of the visited flag.</param>
        /// <returns>True for visited, false for not visited.</returns>
        bool IsVisited(IGraphElement elem, int visitorID);

        #endregion Visited flags management

        #region Variables management

        /// <summary>
        /// Returns the first variable name for the given element it finds (if any).
        /// </summary>
        /// <param name="elem">Element which name is to be found</param>
        /// <returns>A name which can be used in GetVariableValue to get this element</returns>
        string GetElementName(IGraphElement elem);

        /// <summary>
        /// Returns a linked list of variables mapped to the given graph element
        /// or null, if no variable points to this element
        /// </summary>
        LinkedList<Variable> GetElementVariables(IGraphElement elem);

        /// <summary>
        /// Retrieves the object for a variable name or null, if the variable isn't set yet or anymore
        /// </summary>
        /// <param name="varName">The variable name to lookup</param>
        /// <returns>The according object or null</returns>
        object GetVariableValue(string varName);

        /// <summary>
        /// Retrieves the INode for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an INode object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        INode GetNodeVarValue(string varName);

        /// <summary>
        /// Retrieves the IEdge for a variable name or null, if the variable isn't set yet or anymore.
        /// A InvalidCastException is thrown, if the variable is set and does not point to an IEdge object.
        /// </summary>
        /// <param name="varName">The variable name to lookup.</param>
        /// <returns>The according INode or null.</returns>
        IEdge GetEdgeVarValue(string varName);

        /// <summary>
        /// Sets the value of the given variable to the given value.
        /// If the variable name is null, this function does nothing.
        /// If elem is null, the variable is unset.
        /// </summary>
        /// <param name="varName">The name of the variable</param>
        /// <param name="val">The new value of the variable</param>
        void SetVariableValue(string varName, object val);

        #endregion Variables management

        #region Graph rewriting

        /// <summary>
        /// Retrieves the newest version of an IAction object currently available for this graph.
        /// This may be the given object.
        /// </summary>
        /// <param name="action">The IAction object.</param>
        /// <returns>The newest version of the given action.</returns>
        IAction GetNewestActionVersion(IAction action);

        /// <summary>
        /// Sets the newest action version for a static action.
        /// </summary>
        /// <param name="staticAction">The original action generated by GrGen.exe.</param>
        /// <param name="newAction">A new action instance.</param>
        void SetNewestActionVersion(IAction staticAction, IAction newAction);

        /// <summary>
        /// Executes the modifications of the according rule to the given match/matches.
        /// Fires OnRewritingNextMatch events before each rewrite except for the first one.
        /// </summary>
        /// <param name="matches">The matches object returned by a previous matcher call.</param>
        /// <param name="which">The index of the match in the matches object to be applied,
        /// or -1, if all matches are to be applied.</param>
        /// <returns>A possibly empty array of objects returned by the last applied rewrite.</returns>
        object[] Replace(IMatches matches, int which);

        /// <summary>
        /// Apply a rewrite rule.
        /// </summary>
        /// <param name="ruleObject">RuleObject to be applied</param>
        /// <param name="which">The index of the match to be rewritten or -1 to rewrite all matches</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on MaxMatches)</param>
        /// <param name="special">Specifies whether the %-modifier has been used for this rule, which may have a special meaning for
        /// the application</param>
        /// <param name="test">If true, no rewrite step is performed.</param>
        /// <returns>The number of matches found</returns>
        int ApplyRewrite(RuleObject ruleObject, int which, int localMaxMatches, bool special, bool test);

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <returns>The result of the sequence.</returns>
        bool ApplyGraphRewriteSequence(Sequence sequence);

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        bool ValidateWithSequence(Sequence seq);

        #endregion Graph rewriting

        #region Events

        /// <summary>
        /// Fired after a node has been added
        /// </summary>
        event NodeAddedHandler OnNodeAdded;

        /// <summary>
        /// Fired after an edge has been added
        /// </summary>
        event EdgeAddedHandler OnEdgeAdded;

        /// <summary>
        /// Fired before a node is deleted
        /// </summary>
        event RemovingNodeHandler OnRemovingNode;

        /// <summary>
        /// Fired before an edge is deleted
        /// </summary>
        event RemovingEdgeHandler OnRemovingEdge;

        /// <summary>
        /// Fired before all edges of a node are deleted
        /// </summary>
        event RemovingEdgesHandler OnRemovingEdges;

        /// <summary>
        /// Fired before the whole graph is cleared
        /// </summary>
        event ClearingGraphHandler OnClearingGraph;

        /// <summary>
        /// Fired before an attribute of a node is changed.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function 
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangingNodeAttributes.
        /// </summary>
        event ChangingNodeAttributeHandler OnChangingNodeAttribute;

        /// <summary>
        /// Fired before an attribute of an edge is changed.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function 
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangingEdgeAttributes.
        /// </summary>
        event ChangingEdgeAttributeHandler OnChangingEdgeAttribute;

        /// <summary>
        /// Fired before the type of a node is changed.
        /// Old and new type and attributes are provided to the handler.
        /// </summary>
        event RetypingNodeHandler OnRetypingNode;

        /// <summary>
        /// Fired before the type of an edge is changed.
        /// Old and new type and attributes are provided to the handler.
        /// </summary>
        event RetypingEdgeHandler OnRetypingEdge;

        /// <summary>
        /// Fired before each rewrite step (also rewrite steps of subpatterns) to indicate the names
        /// of the nodes added in this rewrite step in order of addition.
        /// </summary>
        event SettingAddedElementNamesHandler OnSettingAddedNodeNames;

        /// <summary>
        /// Fired before each rewrite step (also rewrite steps of subpatterns) to indicate the names
        /// of the edges added in this rewrite step in order of addition.
        /// </summary>
        event SettingAddedElementNamesHandler OnSettingAddedEdgeNames;

        /// <summary>
        /// Fired after all requested matches of a rule have been matched.
        /// </summary>
        event AfterMatchHandler OnMatched;

        /// <summary>
        /// Fired before the rewrite step of a rule, when at least one match has been found.
        /// </summary>
        event BeforeFinishHandler OnFinishing;

        /// <summary>
        /// Fired before the next match is rewritten. It is not fired before rewriting the first match.
        /// </summary>
        event RewriteNextMatchHandler OnRewritingNextMatch;

        /// <summary>
        /// Fired after the rewrite step of a rule.
        /// Note, that the given matches object may contain invalid entries,
        /// as parts of the match may have been deleted!
        /// </summary>
        event AfterFinishHandler OnFinished;

        /// <summary>
        /// Fired when a sequence is entered.
        /// </summary>
        event EnterSequenceHandler OnEntereringSequence;

        /// <summary>
        /// Fired when a sequence is left.
        /// </summary>
        event ExitSequenceHandler OnExitingSequence;

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
        void ChangingNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue);

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
        void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue);

        /// <summary>
        /// Fires an OnMatched event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Matched(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnFinishing event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Finishing(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnRewritingNextMatch event.
        /// </summary>
        void RewritingNextMatch();

        /// <summary>
        /// Fires an OnFinished event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher. The elements may be invalid.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Finished(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnEnteringSequence event.
        /// </summary>
        /// <param name="seq">The sequence to be entered.</param>
        void EnteringSequence(Sequence seq);

        /// <summary>
        /// Fires an OnExitingSequence event.
        /// </summary>
        /// <param name="seq">The sequence to be exited.</param>
        void ExitingSequence(Sequence seq);

        #endregion Events

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
        bool Validate(bool strict, out List<ConnectionAssertionError> errors);

        /// <summary>
        /// Dumps one or more matches with a given graph dumper.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        /// <param name="dumpInfo">Specifies how the graph shall be dumped.</param>
        /// <param name="matches">An IMatches object containing the matches.</param>
        /// <param name="which">Which match to dump, or AllMatches for dumping all matches
        /// adding connections between them, or OnlyMatches to dump the matches only</param>
        void DumpMatch(IDumper dumper, DumpInfo dumpInfo, IMatches matches, DumpMatchSpecial which);

        /// <summary>
        /// Dumps the graph with a given graph dumper.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        /// <param name="dumpInfo">Specifies how the graph shall be dumped.</param>
        void Dump(IDumper dumper, DumpInfo dumpInfo);

        /// <summary>
        /// Dumps the graph with a given graph dumper and default dump style.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        void Dump(IDumper dumper);
    }
}
