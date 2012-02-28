/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
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
    ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
    ///                        Or the new value to be inserted/added if changeType==PutElement on array.
    ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
    /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
    ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
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
    ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
    ///                        Or the new value to be inserted/added if changeType==PutElement on array.
    ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
    /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
    ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
    public delegate void ChangingEdgeAttributeHandler(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue);

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
    /// <param name="oldEdge">The edge which will be redirectey.</param>
    public delegate void RedirectingEdgeHandler(IEdge edge);

    /// <summary>
    /// Delegate-type called shortly before elements are added to the graph, with the names of the elements added.
    /// </summary>
    public delegate void SettingAddedElementNamesHandler(String[] namesOfElementsAdded);

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
        /// If true (the default case), elements deleted during a rewrite
        /// may be reused in the same rewrite.
        /// As a result new elements may not be discriminable anymore from
        /// already deleted elements using object equality, hash maps, etc.
        /// In cases where this is needed this optimization should be disabled.
        /// </summary>
        bool ReuseOptimization { get; set; }


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
        /// Does graph-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        void Custom(params object[] args);

        /// <summary>
        /// Duplicates a graph.
        /// The new graph will use the same model and backend as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure as this graph.</returns>
        IGraph Clone(String newName);

        /// <summary>
        /// Creates an empty graph using the same model and backend as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new empty graph of the same model.</returns>
        IGraph CreateEmptyEquivalent(String newName);

        /// <summary>
        /// Returns whether this graph is isomorph to that graph (including the attribute values)
        /// Each graph must be either unanalyzed or unchanged since the last analyze,
        /// otherwise results will be wrong!
        /// </summary>
        /// <param name="that">The other graph we check for isomorphy against</param>
        /// <returns>true if that is isomorph (structure and attributes) to this, false otherwise</returns>
        bool IsIsomorph(IGraph that);

        /// <summary>
        /// Returns whether this graph is isomorph to that graph, neglecting the attribute values, only structurally
        /// Each graph must be either unanalyzed or unchanged since the last analyze,
        /// otherwise results will be wrong!
        /// </summary>
        /// <param name="that">The other graph we check for isomorphy against, neglecting attribute values</param>
        /// <returns>true if that is isomorph (regarding structure) to this, false otherwise</returns>
        bool HasSameStructure(IGraph that);

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
        /// <param name="node">The node whose attribute is changed.</param>
        /// <param name="attrType">The type of the attribute to be changed.</param>
        /// <param name="changeType">The type of the change which will be made.</param>
        /// <param name="newValue">The new value of the attribute, if changeType==Assign.
        ///                        Or the value to be inserted/removed if changeType==PutElement/RemoveElement on set.
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
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
        ///                        Or the new map pair value to be inserted if changeType==PutElement on map.
        ///                        Or the new value to be inserted/added if changeType==PutElement on array.
        ///                        Or the new value to be assigned to the given position if changeType==AssignElement on array.</param>
        /// <param name="keyValue">The map pair key to be inserted/removed if changeType==PutElement/RemoveElement on map.
        ///                        The array index to be removed/written to if changeType==RemoveElement/AssignElement on array.</param>
        void ChangingEdgeAttribute(IEdge edge, AttributeType attrType,
            AttributeChangeType changeType, Object newValue, Object keyValue);

        #endregion Events
    }
}
