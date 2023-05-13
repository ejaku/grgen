/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// The different graph validation modes
    /// </summary>
    public enum ValidationMode
    {
        OnlyMultiplicitiesOfMatchingTypes, // check the multiplicities of the incoming/outgoing edges which match the types specified
        StrictOnlySpecified, // as first and additionally check that edges with connections assertions specified are covered by at least on connection assertion
        Strict // as first and additionally check that all edges are covered by at least one connection assertion
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
        /// Inserting a value into some set or a key value pair into some map or a value into some array.
        /// </summary>
        PutElement,

        /// <summary>
        /// Removing a value from some set or a key value pair from some map or a key/index from some array.
        /// </summary>
        RemoveElement,

        /// <summary>
        /// Assignment of a value to a key/index position in an array, overwriting old element at that position.
        /// </summary>
        AssignElement
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
    /// Represents a method called, when an object has been created.
    /// </summary>
    /// <param name="value">The created object.</param>
    public delegate void ObjectCreatedHandler(IObject value);

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
    /// <param name="graph">The graph to be cleared.</param>
    public delegate void ClearingGraphHandler(IGraph graph);

    /// <summary>
    /// Represents a method called just before a node attribute is changed,
    /// with exact information about the change to occur,
    /// to allow rollback of changes, in case a transaction is underway.
    /// </summary>
    /// <param name="node">The node whose attribute is to be changed.</param>
    /// <param name="attrType">The type of the attribute to be changed.</param>
    /// <param name="changeType">The type of the change which will be made.</param>
    /// <param name="newValue">The new value of the attribute, if changeType==Assign.
    ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
    ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
    ///                        Or the new value to be inserted/added if changeType==PutElement on array.
    ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
    /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
    ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
    public delegate void ChangingNodeAttributeHandler(INode node, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue);

    /// <summary>
    /// Represents a method called just before an edge attribute is changed,
    /// with exact information about the change to occur,
    /// to allow rollback of changes, in case a transaction is underway.
    /// </summary>
    /// <param name="edge">The edge whose attribute is to be changed.</param>
    /// <param name="attrType">The type of the attribute to be changed.</param>
    /// <param name="changeType">The type of the change which will be made.</param>
    /// <param name="newValue">The new value of the attribute, if changeType==Assign.
    ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
    ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
    ///                        Or the new value to be inserted/added if changeType==PutElement on array.
    ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
    /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
    ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
    public delegate void ChangingEdgeAttributeHandler(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue);

    /// <summary>
    /// Represents a method called just before an object attribute is changed,
    /// with exact information about the change to occur,
    /// to allow rollback of changes, in case a transaction is underway.
    /// </summary>
    /// <param name="obj">The object whose attribute is to be changed.</param>
    /// <param name="attrType">The type of the attribute to be changed.</param>
    /// <param name="changeType">The type of the change which will be made.</param>
    /// <param name="newValue">The new value of the attribute, if changeType==Assign.
    ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
    ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
    ///                        Or the new value to be inserted/added if changeType==PutElement on array.
    ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
    /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
    ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
    public delegate void ChangingObjectAttributeHandler(IObject obj, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue);

    /// <summary>
    /// Represents a method called after a node attribute was changed (for debugging, omitted in case of nodebugevents).
    /// </summary>
    /// <param name="node">The node whose attribute was changed.</param>
    /// <param name="attrType">The type of the attribute changed.</param>
    public delegate void ChangedNodeAttributeHandler(INode node, AttributeType attrType);

    /// <summary>
    /// Represents a method called after an edge attribute was changed (for debugging, omitted in case of nodebugevents).
    /// </summary>
    /// <param name="edge">The edge whose attribute was changed.</param>
    /// <param name="attrType">The type of the attribute changed.</param>
    public delegate void ChangedEdgeAttributeHandler(IEdge edge, AttributeType attrType);

    /// <summary>
    /// Represents a method called before a node is retyped.
    /// </summary>
    /// <param name="oldNode">The node to be retyped.</param>
    /// <param name="newNode">The new node with the common attributes, but without any incident edges assigned, yet.</param>
    public delegate void RetypingNodeHandler(INode oldNode, INode newNode);

    /// <summary>
    /// Represents a method called before an edge is retyped.
    /// </summary>
    /// <param name="oldEdge">The edge to be retyped.</param>
    /// <param name="newEdge">The new edge with the common attributes, but not fully connected with the incident nodes, yet.</param>
    public delegate void RetypingEdgeHandler(IEdge oldEdge, IEdge newEdge);

    /// <summary>
    /// Represents a method called before an edge is redirected (i.e. will be removed soon and added again immediately thereafter).
    /// </summary>
    /// <param name="edge">The edge which will be redirected.</param>
    public delegate void RedirectingEdgeHandler(IEdge edge);

    /// <summary>
    /// Delegate-type called shortly before elements are added to the graph, with the names of the elements added.
    /// </summary>
    public delegate void SettingAddedElementNamesHandler(String[] namesOfElementsAdded);

    /// <summary>
    /// Represents a method called, when a visited flag was allocated.
    /// </summary>
    /// <param name="visitorID">The id of the visited flag that was allocated.</param>
    public delegate void VisitedAllocHandler(int visitorID);

    /// <summary>
    /// Represents a method called, when a visited flag was freed.
    /// </summary>
    /// <param name="visitorID">The id of the visited flag that was freed.</param>
    public delegate void VisitedFreeHandler(int visitorID);

    /// <summary>
    /// Represents a method called before a visited flag is set to a new value.
    /// </summary>
    /// <param name="elem">The graph element of which the specified flag is to be set.</param>
    /// <param name="visitorID">The id of the visited flag to be set.</param>
    /// <param name="newValue">The new value.</param>
    public delegate void SettingVisitedHandler(IGraphElement elem, int visitorID, bool newValue);

    #endregion GraphDelegates


    // the graph implements an abstract concept IAttributeBearerContainer:
    // it contains attribute bearers, and fires change events for them (realizes a change event space)
    // concrete attribute bearers are nodes, edges, class objects, their remove as well as add behavior differs
    // abstract methods/events of the abstract concept
    // IEnumerator containedAttributeBearers; // allows to iterate the contained attribute bearer objects
    // add(AttributeBearer value); // adds an attribute bearer value
    // event AttributeBearerAddedHandler OnAdded; fired when an attribute bearer is added, with parameter attribute bearer
    // event ChangingAttributeHandler OnChangingAttribute; fired when an attribute of an attribute bearer is assigned to/changed
    //     with parameters attribute bearer, attribute, value / change type


    /// <summary>
    /// An attributed, typed and directed multigraph with multiple inheritance on node and edge types.
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// A name associated with the graph.
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// A unique id associated with the graph
        /// </summary>
        int GraphId { get; }

        /// <summary>
        /// The model associated with the graph.
        /// </summary>
        IGraphModel Model { get; }

        /// <summary>
        /// The indices associated with the graph.
        /// </summary>
        IIndexSet Indices { get; }

        /// <summary>
        /// The uniqueness handler associated with the graph.
        /// </summary>
        IUniquenessHandler UniquenessHandler { get; }

        /// <summary>
        /// The global variables of the graph rewrite system; convenience access to save parameter passing.
        /// </summary>
        IGlobalVariables GlobalVariables { get; }

        /// <summary>
        /// If true (the default case), elements deleted during a rewrite
        /// may be reused in the same rewrite.
        /// As a result new elements may not be discriminable anymore from
        /// already deleted elements using object equality, hash maps, etc.
        /// In cases where this is needed this optimization should be disabled.
        /// </summary>
        bool ReuseOptimization { get; set; }

        /// <summary>
        /// Returns a counter of the number of changes that occured since the graph was created.
        /// If it's different since last time you visited, the graph has changed (but it may be back again in the original state).
        /// Only graph structure changes are counted, attribute changes are not included.
        /// </summary>
        long ChangesCounter { get; }


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
        /// Gets the graph element for the given unique id.
        /// Only available if the unique index was declared in the model.
        /// </summary>
        /// <param name="unique">The unique id of a graph element.</param>
        /// <returns>The graph element for the given unique id or null, if there is no graph element with this unique id.</returns>
        IGraphElement GetGraphElement(int unique);

        /// <summary>
        /// Gets the node for the given unique id.
        /// Only available if the unique index was declared in the model.
        /// </summary>
        /// <param name="unique">The unique id of a node.</param>
        /// <returns>The node for the given unique id or null, if there is no node with this unique id.</returns>
        INode GetNode(int unique);

        /// <summary>
        /// Gets the edge for the given id.
        /// Only available if the unique index was declared in the model.
        /// </summary>
        /// <param name="unique">The unique id of a edge.</param>
        /// <returns>The edge for the given unique id or null, if there is no edge with this unique id.</returns>
        IEdge GetEdge(int unique);


        /// <summary>
        /// Adds an existing INode object to the graph.
        /// The node must not be part of any graph, yet!
        /// The node may not be connected to any other elements!
        /// </summary>
        /// <param name="node">The node to be added.</param>
        void AddNode(INode node);

        /// <summary>
        /// Adds a new node to the graph.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <returns>The newly created node.</returns>
        INode AddNode(NodeType nodeType);

        /// <summary>
        /// Adds an existing IEdge object to the graph.
        /// The edge must not be part of any graph, yet!
        /// Source and target of the edge must already be part of the graph.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        void AddEdge(IEdge edge);

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
        /// There must be no edges left incident to the node (you may use RemoveEdges to ensure this).
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
        /// Removes all nodes and edges from the graph (so any variables pointing to them start dangling).
        /// Also resets the class object unique id source.
        /// </summary>
        void Clear();


        /// <summary>
        /// Retypes a node by creating a new node of the given type.
        /// All incident edges as well as all attributes from common super classes are kept.
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
        /// Merges the source node into the target node,
        /// i.e. all edges incident to the source node are redirected to the target node, then the source node is deleted.
        /// </summary>
        /// <param name="target">The node which remains after the merge.</param>
        /// <param name="source">The node to be merged.</param>
        /// <param name="sourceName">The name of the node to be merged (used for debug display of redirected edges).</param>
        void Merge(INode target, INode source, string sourceName);

        /// <summary>
        /// Changes the source node of the edge from the old source to the given new source.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        /// <param name="oldSourceName">The name of the old source node (used for debug display of the new edge).</param>
        void RedirectSource(IEdge edge, INode newSource, string oldSourceName);

        /// <summary>
        /// Changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        /// <param name="oldTargetName">The name of the old target node (used for debug display of the new edge).</param>
        void RedirectTarget(IEdge edge, INode newTarget, string oldTargetName);

        /// <summary>
        /// Changes the source of the edge from the old source to the given new source,
        /// and changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        /// <param name="oldSourceName">The name of the old source node (used for debug display of the new edge).</param>
        /// <param name="oldTargetName">The name of the old target node (used for debug display of the new edge).</param>
        void RedirectSourceAndTarget(IEdge edge, INode newSource, INode newTarget, string oldSourceName, string oldTargetName);


        /// <summary>
        /// The graph-backend dependent commands that are available, and a description of each command.
        /// </summary>
        IDictionary<String, String> CustomCommandsAndDescriptions { get; }

        /// <summary>
        /// Does graph-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do; first parameter has to be the command</param>
        void Custom(params object[] args);


        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        IGraph Clone(String newName);

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <param name="oldToNewMap">A map of the old elements to the corresponding new elements after cloning.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        IGraph Clone(String newName, out IDictionary<IGraphElement, IGraphElement> oldToNewMap);

        /// <summary>
        /// Duplicates a graph, assigning names. (Don't use this on a named graph.)
        /// The new graph will use the same model and backend as the other.
        /// </summary>
        /// <returns>A new named graph with the same structure as this graph.</returns>
        INamedGraph CloneAndAssignNames();

        /// <summary>
        /// Creates an empty graph using the same model as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new empty graph of the same model.</returns>
        IGraph CreateEmptyEquivalent(String newName);

        
        /// <summary>
        /// Returns whether this graph is isomorphic to that graph (including the attribute values)
        /// If a graph changed only in attribute values since the last comparison, results will be wrong!
        /// (Do a fake node insert and removal to ensure the graph is recognized as having changed.)
        /// </summary>
        /// <param name="that">The other graph we check for isomorphy against</param>
        /// <returns>true if that is isomorphic (structure and attributes) to this, false otherwise</returns>
        bool IsIsomorph(IGraph that);

        /// <summary>
        /// Returns whether this graph is isomorphic to any of the set of graphs given (including the attribute values)
        /// If a graph changed only in attribute values since the last comparison, results will be wrong!
        /// (Do a fake node insert and removal to ensure the graph is recognized as having changed.)
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graph we check for isomorphy against</param>
        /// <returns>true if any of the graphs given is isomorphic to this, false otherwise</returns>
        bool IsIsomorph(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);

        /// <summary>
        /// Returns the graph from the set of graphs given that is isomorphic to this graph (including the attribute values), or null if no such graph exists
        /// If a graph changed only in attribute values since the last comparison, results will be wrong!
        /// (Do a fake node insert and removal to ensure the graph is recognized as having changed.)
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graph we check for isomorphy against</param>
        /// <returns>The isomorphic graph from graphsToCheckAgainst, null if no such graph exists</returns>
        IGraph GetIsomorph(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);

        /// <summary>
        /// Returns whether this graph is isomorphic to that graph, neglecting the attribute values, only structurally
        /// </summary>
        /// <param name="that">The other graph we check for isomorphy against, neglecting attribute values</param>
        /// <returns>true if that is isomorphic (regarding structure) to this, false otherwise</returns>
        bool HasSameStructure(IGraph that);

        /// <summary>
        /// Returns whether this graph is isomorphic to any of the set of graphs given, neglecting the attribute values, only structurally
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graphs we check for isomorphy against, neglecting attribute values</param>
        /// <returns>true if any of the graphs given is isomorphic (regarding structure) to this, false otherwise</returns>
        bool HasSameStructure(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);

        /// <summary>
        /// Returns the graph from the set of graphs given that is isomorphic to this graph (neglecting the attribute values, only structurally), or null if no such graph exists
        /// Don't call from a parallelized matcher!
        /// </summary>
        /// <param name="graphsToCheckAgainst">The other graphs we check for isomorphy against, neglecting attribute values</param>
        /// <returns>The isomorphic graph from graphsToCheckAgainst (regarding structure), null if no such graph exists</returns>
        IGraph GetSameStructure(IDictionary<IGraph, SetValueType> graphsToCheckAgainst);

        /// <summary>
        /// Returns a canonical representation of the graph as a string
        /// </summary>
        /// <returns>a canonical representation of the graph as a string</returns>
        string Canonize();


        /// <summary>
        /// Checks whether a graph meets the connection assertions.
        /// </summary>
        /// <param name="mode">The validation mode to apply.</param>
        /// <param name="errors">If the graph is not valid, this refers to a List of ConnectionAssertionError objects, otherwise it is null.</param>
        /// <returns>True, if the graph is valid.</returns>
        bool Validate(ValidationMode mode, out List<ConnectionAssertionError> errors);

        /// <summary>
        /// Checks whether the internal data structures are ok (will throw an exception if they are not).
        /// This is for debugging the underlying implementation from positions outside the implementation.
        /// </summary>
        void Check();


        #region Visited flags management

        /// <summary>
        /// Allocates a visited flag on the graph elements.
        /// </summary>
        /// <returns>A visitor ID to be used in
        /// visited conditions in patterns ("if { !elem.visited[id]; }"),
        /// visited expressions in evals ("elem.visited[id] = true; b.flag = elem.visited[id] || c.flag; "}
        /// and calls to other visitor functions.</returns>
        int AllocateVisitedFlag();

        /// <summary>
        /// Frees a visited flag.
        /// This is a safe but O(n) operation, as it resets the visited flag in the graph.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        void FreeVisitedFlag(int visitorID);

        /// <summary>
        /// Frees a clean visited flag.
        /// This is an O(1) but potentially unsafe operation.
        /// Attention! A marked element stays marked, so a later allocation hands out a dirty visited flag! 
        /// Use only if you can ensure that all elements of that flag are unmarked before calling.
        /// </summary>
        /// <param name="visitorID">The ID of the visited flag to be freed.</param>
        void FreeVisitedFlagNonReset(int visitorID);

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

        /// <summary>
        /// Returns the ids of the allocated visited flags.
        /// </summary>
        /// <returns>A dynamic array of the visitor ids allocated.</returns>
        List<int> GetAllocatedVisitedFlags();

        #endregion Visited flags management


        #region Visited flag for internal use management

        /// <summary>
        /// Sets the internal-use visited flag of the given graph element.
        /// (Used for computing reachability.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        void SetInternallyVisited(IGraphElement elem, bool visited);

        /// <summary>
        /// Returns whether the given graph element has been internally visited.
        /// (Used for computing reachability.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be retrieved.</param>
        /// <returns>True for visited, false for not visited.</returns>
        bool IsInternallyVisited(IGraphElement elem);

        /// <summary>
        /// Sets the internal-use visited flag of the given graph element.
        /// (Used for computing reachability when employed from a parallelized matcher executed by the thread pool.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be set.</param>
        /// <param name="visited">True for visited, false for not visited.</param>
        /// <param name="threadId">The id of the thread which marks the graph element.</param>
        void SetInternallyVisited(IGraphElement elem, bool visited, int threadId);

        /// <summary>
        /// Returns whether the given graph element has been internally visited.
        /// (Used for computing reachability when employed from a parallelized matcher executed by the thread pool.)
        /// </summary>
        /// <param name="elem">The graph element whose flag is to be retrieved.</param>
        /// <param name="threadId">The id of the thread which queries the marking of the graph element.</param>
        /// <returns>True for visited, false for not visited.</returns>
        bool IsInternallyVisited(IGraphElement elem, int threadId);

        #endregion Visited flag for internal use management


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
        /// Fired after an object has been created (there is no event when it gets destroyed)
        /// </summary>
        event ObjectCreatedHandler OnObjectCreated;

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
        /// Fired before an attribute of an object is changed.
        /// Note for LGSPBackend:
        /// Because objects (of the LGSPBackend) don't know their graph a call to
        /// LGSPObject.SetAttribute will not fire this event. If you use this function
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangingObjectAttributes.
        /// </summary>
        event ChangingObjectAttributeHandler OnChangingObjectAttribute;

        /// <summary>
        /// Fired after an attribute of a node is changed; for debugging purpose.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangedNodeAttributes.
        /// </summary>
        event ChangedNodeAttributeHandler OnChangedNodeAttribute;

        /// <summary>
        /// Fired after an attribute of an edge is changed; for debugging purpose.
        /// Note for LGSPBackend:
        /// Because graph elements of the LGSPBackend don't know their graph a call to
        /// LGSPGraphElement.SetAttribute will not fire this event. If you use this function
        /// and want the event to be fired, you have to fire it yourself
        /// using ChangedEdgeAttributes.
        /// </summary>
        event ChangedEdgeAttributeHandler OnChangedEdgeAttribute;

        /// <summary>
        /// Fired before the type of a node is changed.
        /// Old and new nodes are provided to the handler.
        /// </summary>
        event RetypingNodeHandler OnRetypingNode;

        /// <summary>
        /// Fired before the type of an edge is changed.
        /// Old and new edges are provided to the handler.
        /// </summary>
        event RetypingEdgeHandler OnRetypingEdge;

        /// <summary>
        /// Fired before an edge is redirected (causing removal then adding again).
        /// The edge to be redirected is provided to the handler.
        /// </summary>
        event RedirectingEdgeHandler OnRedirectingEdge;

        /// <summary>
        /// Fired after a visited flag was allocated.
        /// </summary>
        event VisitedAllocHandler OnVisitedAlloc;

        /// <summary>
        /// Fired after a visited flag was freed.
        /// </summary>
        event VisitedFreeHandler OnVisitedFree;

        /// <summary>
        /// Fired before a visited flag is set.
        /// </summary>
        event SettingVisitedHandler OnSettingVisited;

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
        /// Fires an OnChangingNodeAttribute event.
        /// To be called before changing an attribute of a node,
        /// with exact information about the change to occur,
        /// to allow rollback of changes, in case a transaction is underway.
        /// </summary>
        /// <param name="node">The node whose attribute is to be changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array/deque.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array/deque.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The index to be removed/written to if changeType==RemoveElement/AssignElement on array/deque.</param>
        void ChangingNodeAttribute(INode node, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue);

        /// <summary>
        /// Fires an OnChangingEdgeAttribute event.
        /// To be called before changing an attribute of an edge,
        /// with exact information about the change to occur,
        /// to allow rollback of changes, in case a transaction is underway.
        /// </summary>
        /// <param name="edge">The edge whose attribute is to be changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array/deque.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array/deque.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The index to be removed/written to if changeType==RemoveElement/AssignElement on array/deque.</param>
        void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue);

        /// <summary>
        /// Fires an OnChangingObjectAttribute event.
        /// To be called before changing an attribute of an internal object,
        /// with exact information about the change to occur,
        /// to allow rollback of changes, in case a transaction is underway.
        /// </summary>
        /// <param name="obj">The object whose attribute is to be changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array/deque.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array/deque.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The index to be removed/written to if changeType==RemoveElement/AssignElement on array/deque.</param>
        void ChangingObjectAttribute(IObject obj, AttributeType attrType,
            AttributeChangeType changeType, object newValue, object keyValue);

        /// <summary>
        /// Fires an OnChangedNodeAttribute event.
        /// For debugging, won't be automatically called in case of -nodebugevents, attribute change rollback is based on the pre-events.
        /// </summary>
        /// <param name="node">The node whose attribute was changed.</param>
        /// <param name="attrType">The type of the attribute changed.</param>
        void ChangedNodeAttribute(INode node, AttributeType attrType);

        /// <summary>
        /// Fires an OnChangedEdgeAttribute event.
        /// For debugging, won't be automatically called in case of -nodebugevents, attribute change rollback is based on the pre-events.
        /// </summary>
        /// <param name="edge">The edge whose attribute was changed.</param>
        /// <param name="attrType">The type of the attribute changed.</param>
        void ChangedEdgeAttribute(IEdge edge, AttributeType attrType);

        #endregion Events
    }
}
