/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Embedding of a subpattern into it's containing pattern
    /// </summary>
    public interface IPatternGraphEmbedding
    {
        /// <summary>
        /// The name of the usage of the subpattern.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The embedded subpattern
        /// </summary>
        IPatternGraph EmbeddedGraph { get; }

        /// <summary>
        /// The annotations of the pattern element
        /// </summary>
        Annotations Annotations { get; }
    }
}

