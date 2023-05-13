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
        /// Creates a shallow clone of this edge.
        /// All attributes will be transfered to the new edge.
        /// The edge will not be associated to a graph, yet.
        /// So it will not have any assigned variables.
        /// </summary>
        /// <param name="newSource">The new source node for the new edge.</param>
        /// <param name="newTarget">The new target node for the new edge.</param>
        /// <returns>A copy of this edge.</returns>
        IEdge Clone(INode newSource, INode newTarget);

        /// <summary>
        /// Creates a deep copy of this edge (i.e. (transient) class objects will be replicated).
        /// All attributes will be transfered to the new edge.
        /// The edge will not be associated to a graph, yet.
        /// So it will not have any assigned variables.
        /// </summary>
        /// <param name="newSource">The new source node for the new edge.</param>
        /// <param name="newTarget">The new target node for the new edge.</param>
        /// <param name="graph">The graph to fetch the names of the new (non-transient) objects from.</param>
        /// <param name="oldToNewObjectMap">A dictionary mapping objects to their copies, to be supplied as empty dictionary.</param>
        /// <returns>A copy of this edge.</returns>
        IEdge Copy(INode newSource, INode newTarget, IGraph graph, IDictionary<object, object> oldToNewObjectMap);
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
