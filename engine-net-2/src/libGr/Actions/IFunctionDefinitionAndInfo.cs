/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        /// Takes the arguments as inputs.
        /// Returns the one output value.
        /// </summary>
        object Apply(IActionExecutionEnvironment actionEnv, IGraph graph, object[] arguments);
    }

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
        /// <param name="isExternal">Tells whether the function is an externally defined one or an internal one.</param>
        /// <param name="inputNames">The names of the input parameters.</param>
        /// <param name="inputs">The types of the input parameters.</param>
        /// <param name="output">The type of the output parameter.</param>
        public FunctionInfo(String name, String package, String packagePrefixedName, bool isExternal,
            String[] inputNames, GrGenType[] inputs, GrGenType output)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
            this.isExternal = isExternal;
            this.inputNames = inputNames;
            this.inputs = inputs;
            this.output = output;

            this.annotations = new Annotations();
        }

        public string Name { get { return name; } }
        public Annotations Annotations { get { return annotations; } }
        public string Package { get { return package; } }
        public string PackagePrefixedName { get { return packagePrefixedName; } }
        public string[] InputNames { get { return inputNames; } }
        public GrGenType[] Inputs { get { return inputs; } }
        public GrGenType Output { get { return output; } }
        public bool IsExternal { get { return isExternal; } }

        /// <summary>
        /// The name of the function.
        /// </summary>
        public string name;

        /// <summary>
        /// The annotations of the function
        /// </summary>
        public Annotations annotations = new Annotations();

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
        /// Tells whether the function is an externally defined one or an internal one
        /// </summary>
        public bool isExternal;

        /// <summary>
        /// Applies this function with the given action environment on the given graph.
        /// Takes the arguments as inputs.
        /// Returns the one output value.
        /// </summary>
        public abstract object Apply(IActionExecutionEnvironment actionEnv, IGraph graph, object[] arguments);
    }
}
