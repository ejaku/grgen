/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Gives access to the graph containing the graph element
    /// </summary>
    public interface IContained
    {
        /// <summary>
        /// Gets the containing graph of the graph element.
        /// Only available if graph containment tracking for nodes and edges was declared in the model.
        /// </summary>
        /// <returns>The containing graph (of the graph element).</returns>
        IGraph GetContainingGraph();
    }

    /// <summary>
    /// Allows to write the graph field defining the containing graph - for internal use only (don't complain if you use it and mess up the internal state).
    /// </summary>
    public interface ISetableContained
    {
        /// <summary>
        /// Sets the containing graph of the graph element.
        /// Only available if graph containment tracking for nodes and edges was declared in the model.
        /// </summary>
        /// <param name="graph">The new containing graph.</param>
        /// <returns>The old containing graph (of the graph element).</returns>
        IGraph SetContainingGraph(IGraph graph);
    }
}
