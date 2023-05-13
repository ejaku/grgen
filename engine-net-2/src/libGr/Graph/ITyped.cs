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
    /// An interface to be implemented by classes whose objects are InheritanceType-typed
    /// </summary>
    public interface ITyped
    {
        /// <summary>
        /// The InheritanceType of the typed object
        /// </summary>
        InheritanceType Type { get; }

        /// <summary>
        /// Returns true, if the typed object is compatible to the given type
        /// </summary>
        bool InstanceOf(GrGenType type);
    }
}
