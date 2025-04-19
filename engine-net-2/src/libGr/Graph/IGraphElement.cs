/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    }
}
