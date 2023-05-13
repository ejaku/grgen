/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A GrGen object (value of internal, non-node/edge class)
    /// </summary>
    public interface IObject : IBaseObject
    {
        /// <summary>
        /// The ObjectType (class) of the object
        /// </summary>
        new ObjectType Type { get; }

        /// <summary>
        /// Gets the unique id of the class object.
        /// </summary>
        /// <returns>The unique id of the class object.</returns>
        long GetUniqueId();

        /// <summary>
        /// Sets the unique id of the class object.
        /// You have to ensure consistency! (only meant for internal use.)
        /// </summary>
        void SetUniqueId(long uniqueId);

        /// <summary>
        /// Gets the name of the class object (which has the form "%" + uniqueId).
        /// </summary>
        /// <returns>The name of the class object.</returns>
        string GetObjectName();

        /// <summary>
        /// Creates a shallow clone of this object.
        /// All attributes will be transfered to the new object.
        /// A new name will be fetched from the graph.
        /// </summary>
        /// <param name="graph">The graph to fetch the names of the new objects from.</param>
        /// <returns>A copy of this object.</returns>
        IObject Clone(IGraph graph);

        /// <summary>
        /// Creates a deep copy of this object (i.e. (transient) class objects will be replicated).
        /// All attributes will be transfered to the new object.
        /// A new name will be fetched from the graph.
        /// </summary>
        /// <param name="graph">The graph to fetch the names of the new objects from.</param>
        /// <param name="oldToNewObjectMap">A dictionary mapping objects to their copies, to be supplied as empty dictionary.</param>
        /// <returns>A copy of this object.</returns>
        IObject Copy(IGraph graph, IDictionary<object, object> oldToNewObjectMap);
    }
}
