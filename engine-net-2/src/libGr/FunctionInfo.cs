/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A description of a GrGen (attribute evaluation) function.
    /// </summary>
    public abstract class FunctionInfo : IFunctionDefinition
    {
        /// <summary>
        /// Constructs a FunctionInfo object.
        /// </summary>
        /// <param name="name">The name the function was defined with.</param>
        /// <param name="package">null if this is a global pattern graph, otherwise the package the pattern graph is contained in.</param>
        /// <param name="packagePrefixedName">The name of the pattern graph in case of a global type,
        /// the name of the pattern graph is prefixed by the name of the package otherwise (package "::" name).</param>
        /// <param name="inputNames">The names of the input parameters.</param>
        /// <param name="inputs">The types of the input parameters.</param>
        /// <param name="output">The type of the output parameter.</param>
        public FunctionInfo(String name, String package, String packagePrefixedName, 
            String[] inputNames, GrGenType[] inputs, GrGenType output)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
            this.inputNames = inputNames;
            this.inputs = inputs;
            this.output = output;

            this.annotations = new Dictionary<String, String>();
        }

        public string Name { get { return name; } }
        public IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
        public string Package { get { return package; } }
        public string PackagePrefixedName { get { return packagePrefixedName; } }
        public string[] InputNames { get { return inputNames; } }
        public GrGenType[] Inputs { get { return inputs; } }
        public GrGenType Output { get { return output; } }

        /// <summary>
        /// The name of the function.
        /// </summary>
        public string name;

        /// <summary>
        /// The annotations of the function
        /// </summary>
        public IDictionary<String, String> annotations;

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

        /// <summary>
        /// Applies this function with the given action environment on the given graph.
        /// Takes the parameters from paramBindings as inputs.
        /// Returns the one output value.
        /// </summary>
        public abstract object Apply(IActionExecutionEnvironment actionEnv, IGraph graph, FunctionInvocationParameterBindings paramBindings);
    }
}
