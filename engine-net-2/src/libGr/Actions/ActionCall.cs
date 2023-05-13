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
    /// An object representing an action call.
    /// To be built by the user and used at API level to carry out multi action calls.
    /// </summary>
    public class ActionCall
    {
        /// <summary>
        /// Creates a new action call object, allocating an arguments buffer that allows to store argumentCount arguments.
        /// </summary>
        public ActionCall(IAction action, int maxMatches, int argumentCount)
        {
            Action = action;
            MaxMatches = maxMatches;
            Arguments = new object[argumentCount];
        }

        /// <summary>
        /// Creates a new action call object, taking over the given arguments (buffer) as arguments buffer.
        /// </summary>
        public ActionCall(IAction action, int maxMatches, params object[] arguments)
        {
            Action = action;
            MaxMatches = maxMatches;
            Arguments = arguments;
        }

        /// <summary>
        /// The action to call.
        /// </summary>
        public readonly IAction Action;

        /// <summary>
        /// The maximum amount of matches to search for.
        /// </summary>
        public readonly int MaxMatches;

        /// <summary>
        /// Buffer to store the argument values for the action.
        /// </summary>
        public readonly object[] Arguments;
    }
}
