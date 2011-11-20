/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
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
        /// <param name="outParameters">The names of the graph elements of the containing action yielded to.</param>
        /// <param name="outParameterTypes">The types of the graph elements of the containing action yielded to.</param>
        /// <param name="xgrs">The XGRS string.</param>
        /// <param name="lineNr">The line number the sequence appears on in the source.</param>
        public EmbeddedSequenceInfo(String[] parameters, GrGenType[] parameterTypes,
            String[] outParameters, GrGenType[] outParameterTypes, String xgrs, int lineNr)
        {
            Parameters = parameters;
            ParameterTypes = parameterTypes;
            OutParameters = outParameters;
            OutParameterTypes = outParameterTypes;
            XGRS = xgrs;
            LineNr = lineNr;
        }

        /// <summary>
        /// The names of the needed graph elements of the containing action.
        /// Or the names of the graph elements needed from the calling action in case of a defined sequence.
        /// </summary>
        public String[] Parameters;

        /// <summary>
        /// The types of the needed graph elements of the containing action.
        /// Or the types of the graph elements needed from the calling action in case of a definted sequence.
        /// </summary>
        public GrGenType[] ParameterTypes;

        /// <summary>
        /// The names of the graph elements of the containing action yielded to.
        /// Or the names of the graph elements returned to the calling action in case of a defined sequence.
        /// </summary>
        public String[] OutParameters;

        /// <summary>
        /// The types of the graph elements of the containing action yielded to.
        /// Or the types of the graph elements returned to the calling action in case of a defined sequence.
        /// </summary>
        public GrGenType[] OutParameterTypes;

        /// <summary>
        /// The XGRS string.
        /// </summary>
        public String XGRS;

        /// <summary>
        /// The line in the source code on which this sequence appears, printed in case of an error.
        /// </summary>
        public int LineNr;
    }

    /// <summary>
    /// Represents a sequence definition.
    /// </summary>
    public class DefinedSequenceInfo : EmbeddedSequenceInfo
    {
        /// <summary>
        /// Constructs an DefinedSequenceInfo object.
        /// </summary>
        /// <param name="parameters">The names of the graph elements needed from the calling action.</param>
        /// <param name="parameterTypes">The types of the graph elements needed from the calling action.</param>
        /// <param name="outParameters">The names of the graph elements returned to the calling action.</param>
        /// <param name="outParameterTypes">The types of the graph elements returned to the calling action.</param>
        /// <param name="name">The name the sequence was defined with.</param>
        /// <param name="xgrs">The XGRS string.</param>
        /// <param name="lineNr">The line number the sequence appears on in the source.</param>
        public DefinedSequenceInfo(String[] parameters, GrGenType[] parameterTypes,
            String[] outParameters, GrGenType[] outParameterTypes,
            String name, String xgrs, int lineNr)
            : base(parameters, parameterTypes, outParameters, outParameterTypes, xgrs, lineNr)
        {
            Name = name;
        }

        /// <summary>
        /// The name the sequence was defined with
        /// </summary>
        public string Name;
    }
}
