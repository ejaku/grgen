/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An unique id bearer
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets the unique id of the graph element.
        /// Only available if unique ids for nodes and edges were declared in the model
        /// (or implicitely switched on by parallelization or the declaration of some index).
        /// </summary>
        /// <returns>The unique id (of the graph element).</returns>
        int GetUniqueId();
    }
}
