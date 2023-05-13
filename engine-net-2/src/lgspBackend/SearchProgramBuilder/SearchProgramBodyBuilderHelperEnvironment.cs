/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// environment class for body builder helper, contains the (read-only) information from the body builder that is of relevance
    /// </summary>
    class SearchProgramBodyBuilderHelperEnvironment
    {
        public SearchProgramBodyBuilderHelperEnvironment(SearchProgramBodyBuilder bodyBuilder)
        {
            this.bodyBuilder = bodyBuilder;
        }

        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        readonly SearchProgramBodyBuilder bodyBuilder;

        /// <summary>
        /// type of the program which gets currently built
        /// </summary>
        public SearchProgramType programType
        {
            get { return bodyBuilder.programType; }
        }

        /// <summary>
        /// The model for which the matcher functions shall be generated.
        /// </summary>
        public IGraphModel model
        {
            get { return bodyBuilder.model; }
        }

        /// <summary>
        /// the pattern graph to build with its nesting patterns
        /// </summary>
        public Stack<PatternGraph> patternGraphWithNestingPatterns
        {
            get { return bodyBuilder.patternGraphWithNestingPatterns; }
        }

        /// <summary>
        /// is the pattern graph a negative pattern graph?
        /// </summary>
        public bool isNegative
        {
            get { return bodyBuilder.isNegative; }
        }

        /// <summary>
        /// is the current pattern graph nested within a negative pattern graph?
        /// </summary>
        public bool isNestedInNegative
        {
            get { return bodyBuilder.isNestedInNegative; }
        }

        /// <summary>
        /// name of the rule pattern class of the pattern graph
        /// </summary>
        public string rulePatternClassName
        {
            get { return bodyBuilder.rulePatternClassName; }
        }

        /// <summary>
        /// true if statically determined that the iso space number of the pattern getting constructed 
        /// is always below the maximum iso space number (the maximum nesting level of the isomorphy spaces)
        /// </summary>
        public bool isoSpaceNeverAboveMaxIsoSpace
        {
            get { return bodyBuilder.isoSpaceNeverAboveMaxIsoSpace; }
        }

        /// <summary>
        /// The index of the currently built schedule
        /// </summary>
        public int indexOfSchedule
        {
            get { return bodyBuilder.indexOfSchedule; }
        }

        /// <summary>
        /// whether to build the parallelized matcher from the parallelized schedule
        /// </summary>
        public bool parallelized
        {
            get { return bodyBuilder.parallelized; }
        }

        /// <summary>
        /// whether to emit code for gathering profiling information (about search steps executed)
        /// </summary>
        public bool emitProfiling
        {
            get { return bodyBuilder.emitProfiling; }
        }

        /// <summary>
        /// the package prefixed name of the action in case we're building a rule/test, otherwise null
        /// </summary>
        public string packagePrefixedActionName
        {
            get { return bodyBuilder.packagePrefixedActionName; }
        }

        /// <summary>
        /// tells whether the first loop of the search programm was built, or not yet
        /// needed for the profile that does special statistics for the first loop,
        /// because this is the one that will get parallelized in case of action parallelization
        /// only of relevance if programType == SearchProgramType.Action, otherwise the type pinns it to true
        /// </summary>
        public bool firstLoopPassed
        {
            get { return bodyBuilder.firstLoopPassed; }
        }

        public SourceBuilder arrayPerElementMethodBuilder
        {
            get { return bodyBuilder.arrayPerElementMethodBuilder; }
        }
    }
}
