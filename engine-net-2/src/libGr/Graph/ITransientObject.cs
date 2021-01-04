/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

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
        /// Creates a copy of this transient object.
        /// All attributes will be transferred to the new object.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        new ITransientObject Clone();
    }
}
