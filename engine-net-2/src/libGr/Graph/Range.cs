/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Describes a range with a minimum and a maximum value.
    /// </summary>
    public struct Range
    {
        /// <summary>
        /// Constant value representing positive infinity for a range.
        /// </summary>
        public const int Infinite = int.MaxValue;

        /// <summary>
        /// The lower bound of the range.
        /// </summary>
        public int Min;

        /// <summary>
        /// The upper bound of the range.
        /// </summary>
        public int Max;

        /// <summary>
        /// Constructs a Range object.
        /// </summary>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
