/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An interface to be implemented by classes whose objects can be compared for deep attribute value equality
    /// This excludes structures with shortcuts and cycles (acyclic and cyclic graphs),
    /// only classes without further nesting, or lists, or trees are supported.
    /// TODO: extend to structures including shortcuts and cycles, but excluding aliasing.
    /// Was(/is) of minor importance because internal class objects are meant to enrich the graph with memory-efficient extensions, lists and trees (not with hand-written graphs supplying only inefficient pattern-matching and manipulation).
    /// TODO: rename to sth like IDeeplyEqualityComparable
    /// </summary>
    public interface IDeepEqualityComparer
    {
        /// <summary>
        /// Returns whether this and that are deeply equal,
        /// which means the scalar attributes are equal, the container attributes are memberwise deeply equal, and object attributes are deeply equal.
        /// (If types are unequal the result is false.)
        /// Visited objects are/have to be stored in the visited objects dictionary in order to detect shortcuts and cycles.
        /// TODO: extend to structures including shortcuts and cycles, but excluding aliasing, with a visited objects map instead of a set.
        /// </summary>
        bool IsDeeplyEqual(IDeepEqualityComparer that, IDictionary<object, object> visitedObjects);
    }
}
