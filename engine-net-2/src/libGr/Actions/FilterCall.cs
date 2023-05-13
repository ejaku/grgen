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
    /// Base class for objects representing a filter call (of an action filter or a match class filter),
    /// distinguished into filter calls with arguments and lambda expression filter calls.
    /// </summary>
    public abstract class FilterCall
    {
        public FilterCall(String packagePrefixedName)
        {
            PackagePrefixedName = packagePrefixedName;
        }

        /// <summary>
        /// Name of the filter to call.
        /// (For auto-generated filters, it includes the entities; for filter functions, it includes the package.
        /// For lambda expression filters, it may include the optional entity.)
        /// Examples: <![CDATA[keepFirst, orderAscendingBy<i,j>, filterFunctionName, packageName::filterFunctionName]]>
        /// </summary>
        public readonly String PackagePrefixedName;
    }
}
