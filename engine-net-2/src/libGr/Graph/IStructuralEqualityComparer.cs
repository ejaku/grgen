/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An interface to be implemented by classes whose objects can be compared for structural equality
    /// </summary>
    public interface IStructuralEqualityComparer
    {
        /// <summary>
        /// Returns whether this and that are structurally equal,
        /// which means the scalar attributes are equal, the container attributes are memberwise structurally equal, and object attributes are deeply structurally equal.
        /// (If types are unequal the result is false.)
        /// </summary>
        bool IsStructurallyEqual(IStructuralEqualityComparer that, IDictionary<object, object> visitedObjects);
    }
}
