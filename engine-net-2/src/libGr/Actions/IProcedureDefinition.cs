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
    /// An object representing an executable procedure.
    /// </summary>
    public interface IProcedureDefinition
    {
        /// <summary>
        /// The name of the procedure
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The annotations of the procedure
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
        /// Names of the procedure parameters.
        /// </summary>
        string[] InputNames { get; }

        /// <summary>
        /// The GrGen types of the procedure parameters.
        /// </summary>
        GrGenType[] Inputs { get; }

        /// <summary>
        /// The GrGen types of the procedure return values.
        /// </summary>
        GrGenType[] Outputs { get; }

        /// <summary>
        // Tells whether the procedure is an externally defined one or an internal one
        /// </summary>
        bool IsExternal { get; }

        /// <summary>
        /// Applies this procedure with the given action environment on the given graph.
        /// Takes the parameters from paramBindings as inputs.
        /// Returns an array of output values.
        /// Attention: at the next call of Apply, the array returned from previous call is overwritten with the new return values.
        /// </summary>
        object[] Apply(IActionExecutionEnvironment actionEnv, IGraph graph, ProcedureInvocationParameterBindings paramBindings);
    }
}
