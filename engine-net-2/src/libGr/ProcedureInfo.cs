/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
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
    /// A description of a GrGen procedure.
    /// </summary>
    public abstract class ProcedureInfo
    {
        /// <summary>
        /// Constructs a ProcedureInfo object.
        /// </summary>
        /// <param name="name">The name the procedure was defined with.</param>
        /// <param name="package">null if this is a global pattern graph, otherwise the package the pattern graph is contained in.</param>
        /// <param name="packagePrefixedName">The name of the pattern graph in case of a global type,
        /// the name of the pattern graph is prefixed by the name of the package otherwise (package "::" name).</param>
        /// <param name="inputNames">The names of the input parameters.</param>
        /// <param name="inputs">The types of the input parameters.</param>
        /// <param name="outputs">The types of the output parameters.</param>
        public ProcedureInfo(String name, String package, String packagePrefixedName, 
            String[] inputNames, GrGenType[] inputs, GrGenType[] outputs)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
            this.inputNames = inputNames;
            this.inputs = inputs;
            this.outputs = outputs;

            this.ReturnArray = new object[outputs.Length];
        }

        /// <summary>
        /// Applies this procedure with the given action environment on the given graph.
        /// Takes the parameters from paramBindings as inputs.
        /// Returns an array of output values.
        /// Attention: at the next call of Apply, the array returned from previous call is overwritten with the new return values.
        /// </summary>
        public abstract object[] Apply(IActionExecutionEnvironment actionEnv, IGraph graph, ProcedureInvocationParameterBindings paramBindings);

        /// <summary>
        /// The name of the procedure.
        /// </summary>
        public string name;

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        public string package;

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        public string packagePrefixedName;

        /// <summary>
        /// Names of the procedure parameters.
        /// </summary>
        public string[] inputNames;

        /// <summary>
        /// The GrGen types of the procedure parameters.
        /// </summary>
        public GrGenType[] inputs;

        /// <summary>
        /// The GrGen types of the procedure return values.
        /// </summary>
        public GrGenType[] outputs;

        /// <summary>
        /// Performance optimization: saves us usage of new in implementing the Apply method for returning an array.
        /// </summary>
        protected object[] ReturnArray;
    }
}
