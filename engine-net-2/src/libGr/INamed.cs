/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Represents a named entity/type that may be contained in a package.
    /// (Packages were added late during development, with the constraint to stay compatible,
    /// and intentionally as 2nd class citizens, confined to one nesting level, and without explicit object representing them.)
    /// ("Overrides" by "new" in inherited interfaces are for comment refinement only.)
    /// </summary>
    public interface INamed
    {
        /// <summary>
        /// The name of the entity/type
        /// </summary>
        string Name { get; }

        /// <summary>
        /// null if this is a global entity/type, otherwise the package the entity/type is contained in.
        /// </summary>
        string Package { get; }

        /// <summary>
        /// The name of the entity/type in case of a global type,
        /// the name of the entity/type prefixed by the name of the package otherwise (separated by a double colon).
        /// </summary>
        string PackagePrefixedName { get; }
    }
}
