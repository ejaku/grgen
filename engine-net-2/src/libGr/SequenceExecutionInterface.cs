/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
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
        /// <param name="outParameterTypes">The types of the graph elements returned to the containing action.</param>
        /// <param name="xgrs">The XGRS string.</param>
        public EmbeddedSequenceInfo(String[] parameters, GrGenType[] parameterTypes,
            GrGenType[] outParameterTypes, String xgrs)
        {
            Parameters = parameters;
            ParameterTypes = parameterTypes;
            OutParameterTypes = outParameterTypes;
            XGRS = xgrs;
        }

        /// <summary>
        /// The names of the needed graph elements of the containing action.
        /// </summary>
        public String[] Parameters;

        /// <summary>
        /// The types of the needed graph elements of the containing action.
        /// </summary>
        public GrGenType[] ParameterTypes;

        /// <summary>
        /// The types of the graph elements returned to the containing action.
        /// </summary>
        public GrGenType[] OutParameterTypes;

        /// <summary>
        /// The XGRS string.
        /// </summary>
        public String XGRS;
    }

    /// <summary>
    /// Represents a sequence definition.
    /// </summary>
    public class DefinedSequenceInfo : EmbeddedSequenceInfo
    {
        /// <summary>
        /// Constructs an DefinedSequenceInfo object.
        /// </summary>
        /// <param name="parameters">The names of the needed graph elements of the containing action.</param>
        /// <param name="parameterTypes">The types of the needed graph elements of the containing action.</param>
        /// <param name="outParameters">The names of the needed graph elements returned to the containing action.</param>
        /// <param name="outParameterTypes">The types of the graph elements returned to the containing action.</param>
        /// <param name="name">The name the sequence was defined with.</param>
        /// <param name="xgrs">The XGRS string.</param>
        public DefinedSequenceInfo(String[] parameters, GrGenType[] parameterTypes,
            String[] outParameters, GrGenType[] outParameterTypes,
            String name, String xgrs)
            : base(parameters, parameterTypes, outParameterTypes, xgrs)
        {
            OutParameters = outParameters;
            Name = name;
        }

        /// <summary>
        /// The name the sequence was defined with
        /// </summary>
        public string Name;

        /// <summary>
        /// The names of the graph elements returned to the containing action.
        /// </summary>
        public String[] OutParameters;
    }
}
