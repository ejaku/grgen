/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the kind of sequence parser error.
    /// </summary>
    public enum SequenceParserError
    {
        /// <summary>
        /// The rule is unknown.
        /// </summary>
        UnknownRule,

        /// <summary>
        /// The number of parameters and/or return parameters does not match the action.
        /// </summary>
        BadNumberOfParametersOrReturnParameters,

        /// <summary>
        /// The type of a parameter does not match the signature of the action.
        /// </summary>
        BadParameter,

        /// <summary>
        /// A variable has been declared with the name of an action.
        /// </summary>
        RuleNameUsedByVariable,

        /// <summary>
        /// A variable has been used with parameters and/or return parameters.
        /// </summary>
        VariableUsedWithParametersOrReturnParameters,

        /// <summary>
        /// A non-boolean variable has been used as a predicate.
        /// </summary>
        InvalidUseOfVariable
    }

    /// <summary>
    /// An exception thrown by SequenceParser describing,
    /// which rule caused the problem and how it was used
    /// </summary>
    public class SequenceParserRuleException : Exception
    {
        /// <summary>
        /// The kind of error.
        /// </summary>
        public SequenceParserError Kind;

        /// <summary>
        /// The name of the rule.
        /// </summary>
        public String RuleName;

        /// <summary>
        /// The associated action instance. If it is null, there was no rule with the name specified in RuleName.
        /// </summary>
        public IAction Action;

        /// <summary>
        /// The number of inputs given to the rule.
        /// </summary>
        public int NumGivenInputs;

        /// <summary>
        /// The number of outputs given to the rule.
        /// </summary>
        public int NumGivenOutputs;

        /// <summary>
        /// The index of a bad parameter or -1 if another error occurred.
        /// </summary>
        public int BadParamIndex;

        /// <summary>
        /// Creates an instance of a SequenceParserRuleException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="ruleName">The name of the rule.</param>
        /// <param name="action">The associated action instance.
        /// If it is null, there was no rule with the name specified in RuleName.</param>
        /// <param name="numGivenInputs">The number of inputs given to the rule.</param>
        /// <param name="numGivenOutputs">The number of outputs given to the rule.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserRuleException(String ruleName, IAction action, int numGivenInputs, int numGivenOutputs, int badParamIndex)
        {
            RuleName = ruleName;
            Action = action;
            NumGivenInputs = numGivenInputs;
            NumGivenOutputs = numGivenOutputs;
            BadParamIndex = badParamIndex;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserRuleException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="ruleName">Name of the rule or variable.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserRuleException(String ruleName, SequenceParserError errorKind)
        {
            RuleName = ruleName;
            Kind = errorKind; 
        }

        /// <summary>
        /// Creates an instance of a SequenceParserRuleException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserRuleException(RuleInvocationParameterBindings paramBindings, SequenceParserError errorKind)
            : this(paramBindings, errorKind, -1)
        {
        }

        /// <summary>
        /// Creates an instance of a SequenceParserRuleException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserRuleException(RuleInvocationParameterBindings paramBindings, SequenceParserError errorKind, int badParamIndex)
        {
            Kind = errorKind;
            RuleName = paramBindings.RuleName;
            Action = paramBindings.Action;
            NumGivenInputs = paramBindings.Parameters.Length;
            NumGivenOutputs = paramBindings.ReturnVars.Length;
            BadParamIndex = badParamIndex;
        }
    }
}
