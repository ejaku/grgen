/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Interface implemented by the compiled graph matchers
    /// </summary>
    public interface GraphComparisonMatcher
    {
        /// <summary>
        /// Returns whether the graph which resulted in thisPattern is isomorph to the graph given.        
        /// </summary>
        bool IsIsomorph(PatternGraph thisPattern, LGSPGraph graph, bool includingAttributes);

        /// <summary>
        /// Returns whether the graph which resulted in thisPattern is isomorph to the graph given, using the parallel is matched flags array of the given thread id.        
        /// </summary>
        bool IsIsomorph(PatternGraph thisPattern, LGSPGraph graph, bool includingAttributes, int threadId);

        /// <summary>
        /// Returns the name of the compiled matcher, same as the name of the interpretation plan.
        /// </summary>
        string Name { get; }
    }
}

