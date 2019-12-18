/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

//#define USE_HIGHPERFORMANCE_COUNTER

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing an executable function.
    /// </summary>
    public interface IFunctionDefinition
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The annotations of the function
        /// </summary>
        Annotations Annotations { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        string Package { get; }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        string PackagePrefixedName { get; }

        /// <summary>
        /// Names of the function parameters.
        /// </summary>
        string[] InputNames { get; }

        /// <summary>
        /// The GrGen types of the function parameters.
        /// </summary>
        GrGenType[] Inputs { get; }

        /// <summary>
        /// The GrGen type of the function return value.
        /// </summary>
        GrGenType Output { get; }

        /// <summary>
        // Tells whether the function is an externally defined one or an internal one
        /// </summary>
        bool IsExternal { get; }

        /// <summary>
        /// Applies this function with the given action environment on the given graph.
        /// Takes the parameters from paramBindings as inputs.
        /// Returns the one output value.
        /// </summary>
        object Apply(IActionExecutionEnvironment actionEnv, IGraph graph, FunctionInvocationParameterBindings paramBindings);
    }
}
