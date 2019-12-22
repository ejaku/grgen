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
        /// Takes the arguments as inputs.
        /// Returns an array of output values.
        /// Attention: at the next call of Apply, the array returned from previous call is overwritten with the new return values.
        /// </summary>
        object[] Apply(IActionExecutionEnvironment actionEnv, IGraph graph, object[] argument);
    }

    /// <summary>
    /// A description of a GrGen (attribute evaluation) procedure.
    /// </summary>
    public abstract class ProcedureInfo : IProcedureDefinition
    {
        /// <summary>
        /// Constructs a ProcedureInfo object.
        /// </summary>
        /// <param name="name">The name the procedure was defined with.</param>
        /// <param name="package">null if this is a global pattern graph, otherwise the package the pattern graph is contained in.</param>
        /// <param name="packagePrefixedName">The name of the pattern graph in case of a global type,
        /// the name of the pattern graph is prefixed by the name of the package otherwise (package "::" name).</param>
        /// <param name="isExternal">Tells whether the procedure is an externally defined one or an internal one.</param>
        /// <param name="inputNames">The names of the input parameters.</param>
        /// <param name="inputs">The types of the input parameters.</param>
        /// <param name="outputs">The types of the output parameters.</param>
        public ProcedureInfo(String name, String package, String packagePrefixedName, bool isExternal,
            String[] inputNames, GrGenType[] inputs, GrGenType[] outputs)
        {
            this.name = name;
            this.package = package;
            this.packagePrefixedName = packagePrefixedName;
            this.isExternal = isExternal;
            this.inputNames = inputNames;
            this.inputs = inputs;
            this.outputs = outputs;

            this.annotations = new Annotations();

            this.ReturnArray = new object[outputs.Length];
        }

        public string Name { get { return name; } }
        public Annotations Annotations { get { return annotations; } }
        public string Package { get { return package; } }
        public string PackagePrefixedName { get { return packagePrefixedName; } }
        public string[] InputNames { get { return inputNames; } }
        public GrGenType[] Inputs { get { return inputs; } }
        public GrGenType[] Outputs { get { return outputs; } }
        public bool IsExternal { get { return isExternal; } }

        /// <summary>
        /// The name of the procedure.
        /// </summary>
        public string name;

        /// <summary>
        /// The annotations of the procedure
        /// </summary>
        public Annotations annotations;

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

        /// <summary>
        /// Tells whether the procedure is an externally defined one or an internal one
        /// </summary>
        public bool isExternal;

        /// <summary>
        /// Applies this procedure with the given action environment on the given graph.
        /// Takes the arguments as inputs.
        /// Returns an array of output values.
        /// Attention: at the next call of Apply, the array returned from previous call is overwritten with the new return values.
        /// </summary>
        public abstract object[] Apply(IActionExecutionEnvironment actionEnv, IGraph graph, object[] argument);
    }
}
