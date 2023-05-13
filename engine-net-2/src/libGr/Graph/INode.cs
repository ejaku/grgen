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
        /// Creates a shallow clone of this node.
        /// All attributes will be transfered to the new node.
        /// The node will not be associated to a graph, yet.
        /// So it will not have any incident edges nor any assigned variables.
        /// </summary>
        /// <returns>A copy of this node.</returns>
        INode Clone();

        /// <summary>
        /// Creates a deep copy of this node (i.e. (transient) class objects will be replicated).
        /// All attributes will be transfered to the new node.
        /// The node will not be associated to a graph, yet.
        /// So it will not have any incident edges nor any assigned variables.
        /// </summary>
        /// <param name="graph">The graph to fetch the names of the new (non-transient) objects from.</param>
        /// <param name="oldToNewObjectMap">A dictionary mapping objects to their copies, to be supplied as empty dictionary.</param>
        /// <returns>A copy of this node.</returns>
        INode Copy(IGraph graph, IDictionary<object, object> oldToNewObjectMap);
    }
}
