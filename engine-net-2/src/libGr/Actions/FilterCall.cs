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
    /// To be built by the user and used at API level to carry out filter calls.
    /// </summary>
    public class FilterCall
    {
        public FilterCall(String packagePrefixedName, int argumentCount)
        {
            PackagePrefixedName = packagePrefixedName;
            Arguments = new object[argumentCount];
        }

        /// <summary>
        /// Name of the filter to call.
        /// (For auto-generated filters, it includes the entities; for filter functions, it includes the package.)
        /// Examples: <![CDATA[keepFirst, orderAscendingBy<i,j>, filterFunctionName, packageName::filterFunctionName]]>
        /// </summary>
        public readonly String PackagePrefixedName;

        /// <summary>
        /// Buffer to store the argument values for the filter function or auto-supplied filter call.
        /// </summary>
        public readonly object[] Arguments;
    }
}
