/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A description of a GrGen matching pattern, that's a subpattern/subrule or the base for some rule.
    /// </summary>
    public interface IMatchingPattern
    {
        /// <summary>
        /// The main pattern graph.
        /// </summary>
        IPatternGraph PatternGraph { get; }

        /// <summary>
        /// An array of GrGen types corresponding to rule parameters.
        /// </summary>
        GrGenType[] Inputs { get; }

        /// <summary>
        /// An array of the names corresponding to rule parameters.
        /// </summary>
        String[] InputNames { get; }

        /// <summary>
        /// An array of the names of the def elements yielded out of this pattern.
        /// </summary>
        String[] DefNames { get; }

        /// <summary>
        /// The annotations of the matching pattern (test/rule/subpattern)
        /// </summary>
        Annotations Annotations { get; }
    }

    // TODO: split ISubpatternPattern out of IMatching pattern, def elements are a subpattern only thing 
    // -> IMatchingPattern as parent element for IRulePattern and ISubpatternPattern

    /// <summary>
    /// A description of a GrGen rule.
    /// </summary>
    public interface IRulePattern : IMatchingPattern
    {
        /// <summary>
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        GrGenType[] Outputs { get; }

        /// <summary>
        /// An array of the names of the available filters (external extensions)
        /// </summary>
        IFilter[] Filters { get; }

        /// <summary>
        /// Returns the (package prefixed) filter, if it is available, otherwise null
        /// </summary>
        IFilter GetFilter(string name);

        /// <summary>
        /// An array of the implemented match classes
        /// </summary>
        IMatchClass[] ImplementedMatchClasses { get; }

        /// <summary>
        /// Returns the implemented match class, if it is available, otherwise null
        /// </summary>
        IMatchClass GetImplementedMatchClass(string name);

        /// <summary>
        /// The name of the real .NET interface type of the match of the rule/action (fully qualified).
        /// </summary>
        string MatchInterfaceName { get; }

        /// <summary>
        /// The name of the real .NET class type of the match of the rule/action (fully qualified).
        /// </summary>
        string MatchClassName { get; }
    }
}

