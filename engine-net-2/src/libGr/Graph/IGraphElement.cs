/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A GrGen graph element
    /// </summary>
    public interface IGraphElement : IAttributeBearer
    {
        /// <summary>
        /// The GraphElementType of the graph element
        /// </summary>
        new GraphElementType Type { get; }

        /// <summary>
        /// This is true, if the element is a valid graph element, i.e. it is part of a graph.
        /// </summary>
        bool Valid { get; }

        /// <summary>
        /// The element which replaced this element (Valid is false in this case)
        /// or null, if this element has not been replaced or is still a valid member of a graph.
        /// </summary>
        IGraphElement ReplacedByElement { get; }

        /// <summary>
        /// Gets the unique id of the graph element.
        /// Only available if unique ids for nodes and edges were declared in the model
        /// (or implicitely switched on by parallelization or the declaration of some index).
        /// </summary>
        /// <returns>The unique id of the graph element (an arbitrary number in case uniqueness was not requested).</returns>
        int GetUniqueId();
    }
}
