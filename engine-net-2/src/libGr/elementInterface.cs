/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A GrGen graph element
    /// </summary>
    public interface IGraphElement
    {
        /// <summary>
        /// The GrGenType of the graph element
        /// </summary>
        GrGenType Type { get; }

        /// <summary>
        /// This is true, if the element is a valid graph element, i.e. it is part of a graph.
        /// </summary>
        bool Valid { get; }

		/// <summary>
		/// The element which replaced this element (Valid is false in this case)
		/// or null, if this element has not been replaced or is still a valid member of a graph.
		/// </summary>
		IGraphElement ReplacedByElement { get; }

        /// <summary>
        /// Returns true, if the graph element is compatible to the given type
        /// </summary>
        bool InstanceOf(GrGenType type);

        /// <summary>
        /// Gets the unique id of the graph element.
        /// Only available if unique ids for nodes and edges were declared in the model
        /// (or implicitely switched on by parallelization or the declaration of some index).
        /// </summary>
        /// <returns>The unique id of the graph element (an arbitrary number in case uniqueness was not requested).</returns>
        int GetUniqueId();

        /// <summary>
        /// Returns the graph element attribute with the given attribute name.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        object GetAttribute(String attrName);

        /// <summary>
        /// Sets the graph element attribute with the given attribute name to the given value.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        void SetAttribute(String attrName, object value);

        /// <summary>
        /// Resets all graph element attributes to their initial values.
        /// </summary>
        void ResetAllAttributes();

        /// <summary>
        /// Returns whether the attributes of this element and that are equal.
        /// If types are unequal the result is false, otherwise the conjunction of equality comparison of the attributes.
        /// </summary>
        bool AreAttributesEqual(IGraphElement that);

        /// <summary>
        /// Executes the function method given by its name.
        /// Throws an exception if the method does not exists or the parameters are of wrong types.
        /// </summary>
        /// <param name="actionEnv">The current action execution environment.</param>
        /// <param name="graph">The current graph.</param>
        /// <param name="name">The name of the function method to apply.</param>
        /// <param name="arguments">An array with the arguments to the method.</param>
        /// <returns>The return value of function application.</returns>
        object ApplyFunctionMethod(IActionExecutionEnvironment actionEnv, IGraph graph, string name, object[] arguments);

        /// <summary>
        /// Executes the procedure method given by its name.
        /// Throws an exception if the method does not exists or the parameters are of wrong types.
        /// </summary>
        /// <param name="actionEnv">The current action execution environment.</param>
        /// <param name="graph">The current graph.</param>
        /// <param name="name">The name of the procedure method to apply.</param>
        /// <param name="arguments">An array with the arguments to the method.</param>
        /// <returns>An array with the return values of procedure application. Only valid until the next call of this method.</returns>
        object[] ApplyProcedureMethod(IActionExecutionEnvironment actionEnv, IGraph graph, string name, object[] arguments);
    }

    /// <summary>
    /// A GrGen node
    /// </summary>
    public interface INode : IGraphElement
    {
        /// <summary>
        /// The NodeType of the node
        /// </summary>
        new NodeType Type { get; }

        /// <summary>
        /// The node which replaced this node (Valid is false in this case)
        /// or null, if this node has not been replaced or is still a valid member of a graph.
        /// </summary>
        INode ReplacedByNode { get; }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges
        /// </summary>
        IEnumerable<IEdge> Outgoing { get; }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with the same type or a subtype of the given type
        /// </summary>
        IEnumerable<IEdge> GetCompatibleOutgoing(EdgeType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with exactly the given type
        /// </summary>
        IEnumerable<IEdge> GetExactOutgoing(EdgeType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges
        /// </summary>
        IEnumerable<IEdge> Incoming { get; }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with the same type or a subtype of the given type
        /// </summary>
        IEnumerable<IEdge> GetCompatibleIncoming(EdgeType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with exactly the given type
        /// </summary>
        IEnumerable<IEdge> GetExactIncoming(EdgeType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incident edges
        /// </summary>
        IEnumerable<IEdge> Incident { get; }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incident edges with the same type or a subtype of the given type
        /// </summary>
        IEnumerable<IEdge> GetCompatibleIncident(EdgeType edgeType);

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incident edges with exactly the given type
        /// </summary>
        IEnumerable<IEdge> GetExactIncident(EdgeType edgeType);

        /// <summary>
        /// Creates a copy of this node.
        /// All attributes will be transfered to the new node.
        /// The node will not be associated to a graph, yet.
        /// So it will not have any incident edges nor any assigned variables.
        /// </summary>
        /// <returns>A copy of this node.</returns>
        INode Clone();
    }

    /// <summary>
    /// A GrGen edge (arbitrary direction)
    /// </summary>
    public interface IEdge : IGraphElement
    {
        /// <summary>
        /// Returns the EdgeType of the edge
        /// </summary>
        new EdgeType Type { get; }

		/// <summary>
		/// The edge which replaced this edge (Valid is false in this case)
		/// or null, if this edge has not been replaced or is still a valid member of a graph.
		/// </summary>
        IEdge ReplacedByEdge { get; }

        /// <summary>
        /// The source node of the edge.
        /// </summary>
        INode Source { get; }

        /// <summary>
        /// The target node of the edge.
        /// </summary>
        INode Target { get; }

        /// <summary>
        /// Retrieves the other incident node of this edge.
        /// </summary>
        /// <remarks>If the given node is not the source, the source will be returned.</remarks>
        /// <param name="sourceOrTarget">One node of this edge.</param>
        /// <returns>The other node of this edge.</returns>
        INode Opposite(INode sourceOrTarget);

        /// <summary>
        /// Creates a copy of this edge.
        /// All attributes will be transfered to the new edge.
        /// The edge will not be associated to a graph, yet.
        /// So it will not have any assigned variables.
        /// </summary>
        /// <param name="newSource">The new source node for the new edge.</param>
        /// <param name="newTarget">The new target node for the new edge.</param>
        /// <returns>A copy of this edge.</returns>
        IEdge Clone(INode newSource, INode newTarget);
    }

    /// <summary>
    /// A directed GrGen edge
    /// </summary>
    public interface IDEdge : IEdge
    {
    }

    /// <summary>
    /// An undirected GrGen edge
    /// </summary>
    public interface IUEdge : IEdge
    {
    }
}
