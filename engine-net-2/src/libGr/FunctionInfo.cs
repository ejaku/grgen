/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A description of a GrGen attribute evaluation function.
    /// </summary>
    public abstract class FunctionInfo
    {
        /// <summary>
        /// Constructs a FunctionInfo object.
        /// </summary>
        /// <param name="name">The name the function was defined with.</param>
        /// <param name="inputNames">The names of the input parameters.</param>
        /// <param name="inputs">The types of the input parameters.</param>
        /// <param name="output">The types of the output parameters.</param>
        public FunctionInfo(String name, String[] inputNames, GrGenType[] inputs, GrGenType output)
        {
            this.name = name;
            this.inputNames = inputNames;
            this.inputs = inputs;
            this.output = output;
        }

        public abstract object Apply(IActionExecutionEnvironment actionEnv, IGraph graph, FunctionInvocationParameterBindings paramBindings);

        /// <summary>
        /// The name of the function.
        /// </summary>
        public string name;

        /// <summary>
        /// Names of the function parameters.
        /// </summary>
        public string[] inputNames;

        /// <summary>
        /// The GrGen types of the function parameters.
        /// </summary>
        public GrGenType[] inputs;

        /// <summary>
        /// The GrGen type of the function return value.
        /// </summary>
        public GrGenType output;
    }
}
