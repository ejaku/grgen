/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr.sequenceParser
{
    /// <summary>
    /// An evironment class for the sequence parser, gives it access to the entitites and types that can be referenced in the sequence.
    /// Special version that appears in the context of the debugger, when a debug event occurred, 
    /// and it is checked with a sequence expression whether execution is to be halted (/not halted).
    /// </summary>
    public class SequenceParserEnvironmentInterpretedDebugEventCondition : SequenceParserEnvironmentInterpreted
    {
        /// <summary>
        /// Gives the rule of the match this stands for in the if clause of the debug match event.
        /// </summary>
        private readonly string ruleOfMatchThis;
        public override string RuleOfMatchThis
        {
            get { return ruleOfMatchThis; }
        }

        /// <summary>
        /// Gives the graph element type of the graph element this stands for in the if clause of the debug new/delete/retype/set-attributes event.
        /// </summary>
        private readonly string typeOfGraphElementThis;
        public override string TypeOfGraphElementThis
        {
            get { return typeOfGraphElementThis; }
        }


        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the interpreted if clauses for conditional watchpoint debugging.
        /// </summary>
        /// <param name="actions">The IActions object containing the known actions.</param>
        /// <param name="ruleOfMatchThis">Gives the rule of the match this stands for in the if clause of the debug match event.</param>
        /// <param name="typeOfGraphElementThis">Gives the graph element type of the graph element this stands for in the if clause of the debug new/delete/retype/set-attributes event.</param>
        public SequenceParserEnvironmentInterpretedDebugEventCondition(IActions actions, string ruleOfMatchThis, string typeOfGraphElementThis) : base(actions)
        {
            this.ruleOfMatchThis = ruleOfMatchThis;
            this.typeOfGraphElementThis = typeOfGraphElementThis;
        }
    }
}
