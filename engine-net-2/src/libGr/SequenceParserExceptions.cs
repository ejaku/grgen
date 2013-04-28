/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        /// The rule or sequence is unknown.
        /// </summary>
        UnknownRuleOrSequence,

        /// <summary>
        /// The procedure is unknown.
        /// </summary>
        UnknownProcedure,

        /// <summary>
        /// The function is unknown.
        /// </summary>
        UnknownFunction,

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
        TypeMismatch,

        /// <summary>
        /// The operator is not available for the given types
        /// </summary>
        OperatorNotFound,

        /// <summary>
        /// The given filter can't be applied to the given rule 
        /// </summary>
        FilterError,

        /// <summary>
        /// The given subgraph is of wrong type
        /// </summary>
        SubgraphTypeError,

        /// <summary>
        /// The construct does not accept a subgraph
        /// </summary>
        SubgraphError,

        /// <summary>
        /// The element is not contained in the rule pattern (thus in the match of the rule)
        /// </summary>
        UnknownPatternElement,

        /// <summary>
        /// The rule is unknown (only rule name available, originating from match type)
        /// </summary>
        UnknownRule
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
        /// The name of the rule/sequence.
        /// </summary>
        public String Name;

        /// <summary>
        /// The name of the filter which was mis-applied.
        /// </summary>
        public String FilterName;

        /// <summary>
        /// The name of the entity which does not exist in the pattern of the rule.
        /// </summary>
        public String EntityName;

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
        /// The variable which caused the type error or the function/operator which caused the type error
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
        /// The left type given for the operator
        /// </summary>
        public String LeftType;

        /// <summary>
        /// The right type given for the operator
        /// </summary>
        public String RightType;

        /// <summary>
        /// The sub-expression as string for which the operator given in VariableOrFunctionName 
        /// was not defined for the types given in LeftType and RightType
        /// </summary>
        public String Expression;


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
            Name = ruleName;
            Action = action;
            NumGivenInputs = numGivenInputs;
            NumGivenOutputs = numGivenOutputs;
            BadParamIndex = badParamIndex;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="ruleName">Name of the rule or sequence or variable.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(String name, SequenceParserError errorKind)
        {
            Name = name;
            Kind = errorKind;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule or sequence
        /// with the given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule/sequence invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(InvocationParameterBindingsWithReturns paramBindings, SequenceParserError errorKind)
            : this(paramBindings, errorKind, -1)
        {
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule or sequence 
        /// with the given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the rule/sequence invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserException(InvocationParameterBindingsWithReturns paramBindings, SequenceParserError errorKind, int badParamIndex)
        {
            Kind = errorKind;
            Name = paramBindings.Name;
            if(paramBindings is RuleInvocationParameterBindings)
                Action = ((RuleInvocationParameterBindings)paramBindings).Action;
            NumGivenInputs = paramBindings.Arguments.Length;
            NumGivenOutputs = paramBindings.ReturnVars.Length;
            BadParamIndex = badParamIndex;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the function
        /// with the given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the function invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(InvocationParameterBindings paramBindings, SequenceParserError errorKind)
            : this(paramBindings, errorKind, -1)
        {
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the function 
        /// with the given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="paramBindings">The parameter bindings of the function invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserException(InvocationParameterBindings paramBindings, SequenceParserError errorKind, int badParamIndex)
        {
            Kind = errorKind;
            Name = paramBindings.Name;
            NumGivenInputs = paramBindings.Arguments.Length;
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
        /// Creates an instance of a SequenceParserException used by the SequenceParser,
        /// when an operator is not available for the supplied types.
        /// </summary>
        public SequenceParserException(String varOrFuncName, String leftType, String rightType, String expression)
        {
            VariableOrFunctionName = varOrFuncName;
            LeftType = leftType;
            RightType = rightType;
            Expression = expression;
            Kind = SequenceParserError.OperatorNotFound;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, 
        /// when the filter with the given name can't be applied to the rule of the given name
        /// or when the pattern of the rule of the given name does not contain an entity of the given name.
        /// </summary>
        /// <param name="ruleName">Name of the rule.</param>
        /// <param name="filterNameOrEntityName">Name of the filter which was mis-applied or name of the entity which is not conained in the rule.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(String ruleName, String filterNameOrEntityName, SequenceParserError errorKind)
        {
            if(errorKind == SequenceParserError.FilterError)
                FilterName = filterNameOrEntityName;
            else
                FilterName = filterNameOrEntityName;
            Name = ruleName;
            Kind = errorKind;
        }

        /// <summary>
        /// The error message of the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                // TODO: function

                if (this.Action == null && this.Kind != SequenceParserError.TypeMismatch && this.Kind != SequenceParserError.FilterError) {
                    return "Unknown rule/sequence: \"" + this.Name + "\"";
                }

                switch (this.Kind)
                {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if (this.Action.RulePattern.Inputs.Length != this.NumGivenInputs &&
                        this.Action.RulePattern.Outputs.Length != this.NumGivenOutputs)
                    {
                        return "Wrong number of parameters and return values for action \"" + this.Name + "\"!";
                    } else if (this.Action.RulePattern.Inputs.Length != this.NumGivenInputs) {
                        return "Wrong number of parameters for action \"" + this.Name + "\"!";
                    } else if (this.Action.RulePattern.Outputs.Length != this.NumGivenOutputs) {
                        return "Wrong number of return values for action \"" + this.Name + "\"!";
                    } else {
                        goto default;
                    }
                    // TODO: sequence

                case SequenceParserError.BadParameter:
                    return "The " + (this.BadParamIndex + 1) + ". parameter is not valid for action/sequence \"" + this.Name + "\"!";

                case SequenceParserError.BadReturnParameter:
                    return "The " + (this.BadParamIndex + 1) + ". return parameter is not valid for action/sequence \"" + this.Name + "\"!";

                case SequenceParserError.RuleNameUsedByVariable:
                    return "The name of the variable conflicts with the name of action/sequence \"" + this.Name + "\"!";

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    return "The variable \"" + this.Name + "\" may neither receive parameters nor return values!";

                case SequenceParserError.UnknownAttribute:
                    return "Unknown attribute \"" + this.Name + "\"!";

                case SequenceParserError.TypeMismatch:
                    return "The construct \"" + this.VariableOrFunctionName + "\" expects:" + this.ExpectedType + " but is /given " + this.GivenType + "!";

                case SequenceParserError.OperatorNotFound:
                    return "No operator " + this.LeftType + this.VariableOrFunctionName + this.RightType + " available (for \"" + this.Expression + "\")!";

                case SequenceParserError.FilterError:
                    return "The filter \"" + this.FilterName + "\" can't be applied to \"" + this.Name + "\"!";

                case SequenceParserError.SubgraphError:
                    return "The construct \"" + this.VariableOrFunctionName  + "\" does not support subgraph prefixes!";

                case SequenceParserError.SubgraphTypeError:
                    return "The subgraph prefix \"" + this.VariableOrFunctionName + "\" must be of type:" + this.ExpectedType + " but is /given " + this.GivenType + "!";

                case SequenceParserError.UnknownRule:
                    return "Unknown rule \"" + this.Name + "\" (in match<" + this.Name + ">)!";

                case SequenceParserError.UnknownPatternElement:
                    return "The rule \"" + this.Name + "\" does not contain a (top-level) element \"" + this.EntityName + "\" (so type match<" + this.Name + "> does not)!";

                default:
                    return "Invalid error kind: " + this.Kind;
                }
            }
        }
    }
}
