/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

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
        /// Creates a copy of this node.
        /// All attributes will be transfered to the new node.
        /// The node will not be associated to a graph, yet.
        /// So it will not have any adjacent edges nor any assigned variables.
        /// </summary>
        /// <returns>A copy of this node.</returns>
        INode Clone();
    }

    /// <summary>
    /// A GrGen edge
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
}
