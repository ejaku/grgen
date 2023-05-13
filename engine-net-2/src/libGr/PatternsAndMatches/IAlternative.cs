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
    /// An alternative is a pattern graph element containing subpatterns
    /// of which one must get successfully matched so that the entire pattern gets matched successfully
    /// </summary>
    public interface IAlternative
    {
        /// <summary>
        /// Array with the alternative cases
        /// </summary>
        IPatternGraph[] AlternativeCases { get; }
    }
}

