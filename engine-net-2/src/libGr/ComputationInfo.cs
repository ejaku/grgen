/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
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
    /// A description of a GrGen attribute evaluation computation.
    /// </summary>
    public abstract class ComputationInfo
    {
        /// <summary>
        /// Constructs a ComputationInfo object.
        /// </summary>
        /// <param name="name">The name the computation was defined with.</param>
        /// <param name="inputNames">The names of the input parameters.</param>
        /// <param name="inputs">The types of the input parameters.</param>
        /// <param name="output">The types of the output parameters.</param>
        public ComputationInfo(String name, String[] inputNames, GrGenType[] inputs, GrGenType output)
        {
            this.name = name;
            this.inputNames = inputNames;
            this.inputs = inputs;
            this.output = output;
        }

        public abstract object Apply(IActionExecutionEnvironment actionEnv, IGraph graph, ComputationInvocationParameterBindings paramBindings);

        /// <summary>
        /// The name of the computation.
        /// </summary>
        public string name;

        /// <summary>
        /// Names of the computation parameters.
        /// </summary>
        public string[] inputNames;

        /// <summary>
        /// The GrGen types of the computation parameters.
        /// </summary>
        public GrGenType[] inputs;

        /// <summary>
        /// The GrGen type of the computation return value.
        /// </summary>
        public GrGenType output;
    }
}
