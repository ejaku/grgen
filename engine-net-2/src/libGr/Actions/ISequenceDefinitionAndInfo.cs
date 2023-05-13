/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing an executable sequence.
    /// Implemented by SequenceDefinitionInterpreted inheriting from SequenceDefinition, in the Sequences file, for sequences registered at runtime,
    /// and by the generated class inheriting from SequenceDefinitionCompiled inheriting from SequenceDefinition, for sequences already available at compile time.
    /// Embedded execs are employed directly by their rules, have only an EmbeddedSequenceInfo as representation 
    /// (without capability of getting called, this occurs only directly in the generated code).
    /// </summary>
    public interface ISequenceDefinition
    {
        /// <summary>
        /// The name of the sequence
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The annotations of the sequence
        /// </summary>
        Annotations Annotations { get; }

        /// <summary>
        /// Applies this sequence.
        /// </summary>
        /// <param name="procEnv">The graph processing environment on which this sequence is to be applied.
        ///     Contains especially the graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <param name="arguments">Input arguments</param>
        /// <param name="returnValues">Output return values</param>
        /// <returns>True, iff the sequence succeeded</returns>
        bool Apply(IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues);
    }

    /// <summary>
    /// Represents an XGRS used in an exec statement.
    /// </summary>
    public class EmbeddedSequenceInfo
    {
        /// <summary>
        /// Constructs an EmbeddedSequenceInfo object.
        /// </summary>
        /// <param name="parameters">The names of the needed graph elements of the containing action.</param>
        /// <param name="parameterTypes">The types of the needed graph elements of the containing action.</param>
        /// <param name="outParameters">The names of the graph elements of the containing action yielded to.</param>
        /// <param name="outParameterTypes">The types of the graph elements of the containing action yielded to.</param>
        /// <param name="package">null if this is a global embedded sequence, otherwise the package the embedded sequence is contained in.</param>
        /// <param name="xgrs">The XGRS string.</param>
        /// <param name="lineNr">The line number the sequence appears on in the source.</param>
        public EmbeddedSequenceInfo(String[] parameters, GrGenType[] parameterTypes,
            String[] outParameters, GrGenType[] outParameterTypes,
            String package, String xgrs, int lineNr)
        {
            Parameters = parameters;
            ParameterTypes = parameterTypes;
            OutParameters = outParameters;
            OutParameterTypes = outParameterTypes;
            this.package = package;
            XGRS = xgrs;
            LineNr = lineNr;
        }

        /// <summary>
        /// The names of the needed graph elements of the containing action.
        /// Or the names of the graph elements needed from the calling action in case of a defined sequence.
        /// </summary>
        public readonly String[] Parameters;

        /// <summary>
        /// The types of the needed graph elements of the containing action.
        /// Or the types of the graph elements needed from the calling action in case of a definted sequence.
        /// </summary>
        public readonly GrGenType[] ParameterTypes;

        /// <summary>
        /// The names of the graph elements of the containing action yielded to.
        /// Or the names of the graph elements returned to the calling action in case of a defined sequence.
        /// </summary>
        public readonly String[] OutParameters;

        /// <summary>
        /// The types of the graph elements of the containing action yielded to.
        /// Or the types of the graph elements returned to the calling action in case of a defined sequence.
        /// </summary>
        public readonly GrGenType[] OutParameterTypes;

        /// <summary>
        /// null if this is a global embedded sequence, otherwise the package the embedded sequence is contained in.
        /// </summary>
        public string Package
        {
            get { return package; }
        }

        public readonly string package;

        /// <summary>
        /// The XGRS string.
        /// </summary>
        public readonly String XGRS;

        /// <summary>
        /// The line in the source code on which this sequence appears, printed in case of an error.
        /// </summary>
        public readonly int LineNr;
    }

    /// <summary>
    /// Represents a sequence definition.
    /// </summary>
    public class DefinedSequenceInfo : EmbeddedSequenceInfo, INamed
    {
        /// <summary>
        /// Constructs an DefinedSequenceInfo object.
        /// </summary>
        /// <param name="parameters">The names of the graph elements needed from the calling action.</param>
        /// <param name="parameterTypes">The types of the graph elements needed from the calling action.</param>
        /// <param name="outParameters">The names of the graph elements returned to the calling action.</param>
        /// <param name="outParameterTypes">The types of the graph elements returned to the calling action.</param>
        /// <param name="name">The name the sequence was defined with.</param>
        /// <param name="package">null if this is a global sequence, otherwise the package the sequence is contained in.</param>
        /// <param name="packagePrefixedName">The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.</param>
        /// <param name="xgrs">The XGRS string.</param>
        /// <param name="lineNr">The line number the sequence appears on in the source.</param>
        public DefinedSequenceInfo(String[] parameters, GrGenType[] parameterTypes,
            String[] outParameters, GrGenType[] outParameterTypes,
            String name, String package, String packagePrefixedName,
            String xgrs, int lineNr)
            : base(parameters, parameterTypes, outParameters, outParameterTypes, package, xgrs, lineNr)
        {
            this.name = name;
            this.packagePrefixedName = packagePrefixedName;

            annotations = new Annotations();
        }

        /// <summary>
        /// The name the sequence was defined with
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// null if this is a global sequence, otherwise the package the sequence is contained in.
        /// </summary>
        new public string Package
        {
            get { return package; }
        }

        /// <summary>
        /// The name of the sequence in case of a global sequence,
        /// the name of the sequence prefixed by the name of the package otherwise.
        /// </summary>
        public string PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public readonly string name;
        public readonly string packagePrefixedName;

        /// <summary>
        /// The annotations of the sequence definition
        /// </summary>
        public readonly Annotations annotations;
    }

    /// <summary>
    /// Represents a sequence definition implemented externally.
    /// </summary>
    public class ExternalDefinedSequenceInfo : DefinedSequenceInfo
    {
        /// <summary>
        /// Constructs an ExternalDefinedSequenceInfo object.
        /// </summary>
        /// <param name="parameters">The names of the graph elements needed from the calling action.</param>
        /// <param name="parameterTypes">The types of the graph elements needed from the calling action.</param>
        /// <param name="outParameters">The names of the graph elements returned to the calling action.</param>
        /// <param name="outParameterTypes">The types of the graph elements returned to the calling action.</param>
        /// <param name="name">The name the sequence was defined with.</param>
        /// <param name="lineNr">The line number the sequence appears on in the source.</param>
        public ExternalDefinedSequenceInfo(String[] parameters, GrGenType[] parameterTypes,
            String[] outParameters, GrGenType[] outParameterTypes,
            String name, int lineNr)
            : base(parameters, parameterTypes, outParameters, outParameterTypes, name, null, name, "", lineNr)
        {
        }
    }
}
