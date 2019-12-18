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
    /// An object representing an executable sequence.
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
        /// <param name="sequenceInvocation">Sequence invocation object for this sequence application,
        ///     containing the input parameter sources and output parameter targets</param>
        /// <param name="procEnv">The graph processing environment on which this sequence is to be applied.
        ///     Contains especially the graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeded</returns>
        bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraphProcessingEnvironment procEnv);
    }
}
