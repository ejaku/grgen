/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a filter call (of an action filter or a match class filter).
    /// To be built by the user and used at API level to carry out filter call.
    /// </summary>
    public class FilterCallBase
    {
        public FilterCallBase(String fullName, int argumentCount)
        {
            FullName = fullName;
            Arguments = new object[argumentCount];
        }

        /// <summary>
        /// Name including entities; with package prefix for filter functions as needed.
        /// (A package prefix for auto-generated filters is implementation-only, does not appear here.)
        /// </summary>
        public readonly String FullName;

        /// <summary>
        /// Buffer to store the argument values for the filter function call (or auto-supplied filter call).
        /// </summary>
        public readonly object[] Arguments;
    }
}
