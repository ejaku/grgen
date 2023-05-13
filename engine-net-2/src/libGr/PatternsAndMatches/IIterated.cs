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
    /// An iterated is a pattern graph element containing the subpattern to be matched iteratively
    /// and the information how much matches are needed for success and how much matches to obtain at most
    /// </summary>
    public interface IIterated
    {
        /// <summary>
        ///The iterated pattern to be matched as often as possible within specified bounds.
        /// </summary>
        IPatternGraph IteratedPattern { get; }

        /// <summary>
        /// How many matches to find so the iterated succeeds.
        /// </summary>
        int MinMatches { get; }

        /// <summary>
        /// The upper bound to stop matching at, 0 means unlimited/as often as possible.
        /// </summary>
        int MaxMatches { get; }

        /// <summary>
        /// An array of the available filters
        /// </summary>
        IFilter[] Filters { get; }
    }
}

