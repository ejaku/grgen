/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing the shared elements from the patterns of several actions.
    /// (Match classes allow via their Filterer implementation part to filter the matches obtained from multiple actions (potentially employed by the multi rule all call or multi backtracking sequences)).
    /// </summary>
    public interface IMatchClass
    {
        /// <summary>
        /// The name of the match class
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The annotations of the match class
        /// </summary>
        Annotations Annotations { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        String Package { get; }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        String PackagePrefixedName { get; }

        // TODO: description of elements (of shared pattern part/the match class)

        /// <summary>
        /// An array of the names of the available filters
        /// </summary>
        IFilter[] Filters { get; }
    }

    public abstract class MatchClassInfo : IMatchClass
    {
        public MatchClassInfo(String name, String package, String packagePrefixedName, IFilter[] filters)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
            this.filters = filters;

            this.annotations = new Annotations();
        }

        public string Name { get { return name; } }
        public Annotations Annotations { get { return annotations; } }
        public string Package { get { return package; } }
        public string PackagePrefixedName { get { return packagePrefixedName; } }
        public IFilter[] Filters { get { return filters; } }

        /// <summary>
        /// The name of the match class.
        /// </summary>
        public readonly string name;

        /// <summary>
        /// The annotations of the match class
        /// </summary>
        public readonly Annotations annotations = new Annotations();

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        public readonly string package;

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        public readonly string packagePrefixedName;

        /// <summary>
        /// The filters of the match class
        /// </summary>
        public readonly IFilter[] filters;
    }

    /// <summary>
    /// An object that allows to filter the matches obtained from multiple actions (based on shared pattern elements contained in a match class),
    /// (potentially employed by the multi rule all call or multi backtracking sequences).
    /// </summary>
    public abstract class MatchClassFilterer
    {
        public MatchClassFilterer(MatchClassInfo info)
        {
            this.info = info;
        }

        /// <summary>
        /// The information object of the match class.
        /// </summary>
        public readonly MatchClassInfo info;

        /// <summary>
        /// Filters the matches of a multi rule all call or multi rule backtracking construct
        /// (i.e. matches obtained from different rules, that implement a match class).
        /// </summary>
        /// <param name="actionEnv">The action execution environment, required by the filter implementation.</param>
        /// <param name="matches">The combined list of all matches of all rules (implementing the same match class; to inspect and filter)</param>
        /// <param name="filter">The filter to apply</param>
        public abstract void Filter(IActionExecutionEnvironment actionEnv, IList<IMatch> matches, FilterCall filter);
    }
}
