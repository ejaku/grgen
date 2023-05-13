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
    /// A GrGen transient object (value of internal, non-node/edge class)
    /// </summary>
    public interface ITransientObject : IBaseObject
    {
        /// <summary>
        /// The TransientObjectType (class) of the object
        /// </summary>
        new TransientObjectType Type { get; }

        /// <summary>
        /// Creates a shallow clone of this transient object.
        /// All attributes will be transferred to the new object.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        ITransientObject Clone();

        /// <summary>
        /// Creates a deep copy of this transient object (i.e. (transient) class objects will be replicated).
        /// All attributes will be transferred to the new transient object.
        /// </summary>
        /// <param name="graph">The graph to fetch the names of the new (non-transient) objects from.</param>
        /// <param name="oldToNewObjectMap">A dictionary mapping objects to their copies, to be supplied as empty dictionary.</param>
        /// <returns>A copy of this object.</returns>
        ITransientObject Copy(IGraph graph, IDictionary<object, object> oldToNewObjectMap);
    }
}
