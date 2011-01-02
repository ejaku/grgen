/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
        /// The type of a return parameter does not match the signature of the action.
        /// </summary>
        BadReturnParameter,

        /// <summary>
        /// A variable has been declared with the name of an action.
        /// </summary>
        RuleNameUsedByVariable,

        /// <summary>
        /// A variable has been used with parameters and/or return parameters.
        /// </summary>
        VariableUsedWithParametersOrReturnParameters,

        /// <summary>
        /// The attribute is not known
        /// </summary>
        UnknownAttribute,

        /// <summary>
        /// Type check error
        /// </summary>
        TypeMismatch
    }

    /// <summary>
    /// An exception thrown by SequenceParser,
    /// describing the error, e.g. which rule caused the problem and how it was used
    /// </summary>
    public class SequenceParserException : Exception
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

        // the members for a type mismatch error

        /// <summary>
        /// The variable which caused the type error or the function which caused the type error
        /// </summary>
        public String VariableOrFunctionName;

        /// <summary>
        /// The expected type or types
        /// </summary>
        public String ExpectedType;

        /// <summary>
        /// The given type
        /// </summary>
        public String GivenType;


        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="ruleName">The name of the rule.</param>
        /// <param name="action">The associated action instance.
        /// If it is null, there was no rule with the name specified in RuleName.</param>
        /// <param name="numGivenInputs">The number of inputs given to the rule.</param>
        /// <param name="numGivenOutputs">The number of outputs given to the rule.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserException(String ruleName, IAction action, int numGivenInputs, int numGivenOutputs, int badParamIndex)
        {
            RuleName = ruleName;
            Action = action;
            NumGivenInputs = numGivenInputs;
            NumGivenOutputs = numGivenOutputs;
            BadParamIndex = badParamIndex;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="ruleName">Name of the rule or variable.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(String ruleName, SequenceParserError errorKind)
        {
            RuleName = ruleName;
            Kind = errorKind; 
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(RuleInvocationParameterBindings paramBindings, SequenceParserError errorKind)
            : this(paramBindings, errorKind, -1)
        {
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserException(RuleInvocationParameterBindings paramBindings, SequenceParserError errorKind, int badParamIndex)
        {
            Kind = errorKind;
            RuleName = paramBindings.RuleName;
            Action = paramBindings.Action;
            NumGivenInputs = paramBindings.Parameters.Length;
            NumGivenOutputs = paramBindings.ReturnVars.Length;
            BadParamIndex = badParamIndex;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser,
        /// when the expected type does not match the given type of the variable of function.
        /// </summary>
        public SequenceParserException(String varOrFuncName, String expectedType, String givenType)
        {
            VariableOrFunctionName = varOrFuncName;
            ExpectedType = expectedType;
            GivenType = givenType;
            Kind = SequenceParserError.TypeMismatch;
        }

        /// <summary>
        /// The error message of the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                if (this.Action == null && this.Kind != SequenceParserError.TypeMismatch) {
                    return "Unknown rule: \"" + this.RuleName + "\"";
                }

                switch (this.Kind)
                {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if (this.Action.RulePattern.Inputs.Length != this.NumGivenInputs &&
                        this.Action.RulePattern.Outputs.Length != this.NumGivenOutputs)
                    {
                        return "Wrong number of parameters and return values for action \"" + this.RuleName + "\"!";
                    } else if (this.Action.RulePattern.Inputs.Length != this.NumGivenInputs) {
                        return "Wrong number of parameters for action \"" + this.RuleName + "\"!";
                    } else if (this.Action.RulePattern.Outputs.Length != this.NumGivenOutputs) {
                        return "Wrong number of return values for action \"" + this.RuleName + "\"!";
                    } else {
                        goto default;
                    }

                case SequenceParserError.BadParameter:
                    return "The " + (this.BadParamIndex + 1) + ". parameter is not valid for action \"" + this.RuleName + "\"!";

                case SequenceParserError.BadReturnParameter:
                    return "The " + (this.BadParamIndex + 1) + ". return parameter is not valid for action \"" + this.RuleName + "\"!";

                case SequenceParserError.RuleNameUsedByVariable:
                    return "The name of the variable conflicts with the name of action \"" + this.RuleName + "\"!";

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    return "The variable \"" + this.RuleName + "\" may neither receive parameters nor return values!";

                case SequenceParserError.UnknownAttribute:
                    return "Unknown attribute \"" + this.RuleName + "\"!";

                case SequenceParserError.TypeMismatch:
                    return "The construct \"" + this.VariableOrFunctionName + "\" expects:" + this.ExpectedType + " but is /given " + this.GivenType + "!";

                default:
                    return "Invalid error kind: " + this.Kind;
                }
            }
        }
    }
}
