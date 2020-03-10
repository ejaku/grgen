/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

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
        /// The number of parameters does not match the signature of the definition.
        /// </summary>
        BadNumberOfParameters,

        /// <summary>
        /// The number of return parameters does not match the signature of the definition.
        /// </summary>
        BadNumberOfReturnParameters,

        /// <summary>
        /// The type of a parameter does not match the signature of the definition.
        /// </summary>
        BadParameter,

        /// <summary>
        /// The type of a return parameter does not match the signature of the definition.
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
        /// The match class employed by the filter is not known
        /// </summary>
        MatchClassError,
        
        /// <summary>
        /// The given filter can't be applied to the given rule or match class
        /// </summary>
        FilterError,

        /// <summary>
        /// The parameters of the given filter applied to the given rule don't fit to the declaration
        /// </summary>
        FilterParameterError,

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
        UnknownRule,

        /// <summary>
        /// Method call notations of a not-builtin-method was used on a non-graph-type
        /// </summary>
        UserMethodsOnlyAvailableForGraphElements,

        /// <summary>
        /// The index access direction is unknown (must be ascending or descending)
        /// </summary>
        UnknownIndexAccessDirection,

        /// <summary>
        /// Two different index names are given for an index access, must be a single one
        /// </summary>
        TwoDifferentIndexNames,

        /// <summary>
        /// Two lower bounds are given
        /// </summary>
        TwoLowerBounds,

        /// <summary>
        /// Two upper bounds are given
        /// </summary>
        TwoUpperBounds
    }

    public enum DefinitionType
    {
        Unknown,
        Action,
        Sequence,
        Procedure,
        Function
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
        public readonly SequenceParserError Kind;

        /// <summary>
        /// The type of the definition that caused the error, Unknown if no definition was involved.
        /// </summary>
        public readonly DefinitionType DefType = DefinitionType.Unknown;

        /// <summary>
        /// The name of the definition (rule/test/sequence/procedure/function/procedure method/function method).
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The name of the filter which was mis-applied.
        /// </summary>
        public readonly String FilterName;

        /// <summary>
        /// The name of the entity which does not exist in the pattern of the rule.
        /// </summary>
        public readonly String EntityName;

        /// <summary>
        /// The associated action instance. If it is null, there was no rule with the name specified in RuleName.
        /// </summary>
        public readonly IAction Action;

        /// <summary>
        /// The number of inputs or outputs given to the rule.
        /// </summary>
        public readonly int NumGiven;

        /// <summary>
        /// The index of a bad parameter or -1 if another error occurred.
        /// </summary>
        public readonly int BadParamIndex;

        // the members for a type mismatch error

        /// <summary>
        /// The variable which caused the type error or the function/operator which caused the type error
        /// </summary>
        public readonly String VariableOrFunctionName;

        /// <summary>
        /// The expected type or types
        /// </summary>
        public readonly String ExpectedType;

        /// <summary>
        /// The given type
        /// </summary>
        public readonly String GivenType;

        /// <summary>
        /// The left type given for the operator
        /// </summary>
        public readonly String LeftType;

        /// <summary>
        /// The right type given for the operator
        /// </summary>
        public readonly String RightType;

        /// <summary>
        /// The sub-expression as string for which the operator given in VariableOrFunctionName 
        /// was not defined for the types given in LeftType and RightType
        /// </summary>
        public readonly String Expression;

        /// <summary>
        /// A suggestion to the user hinting at a possible fix.
        /// </summary>
        public readonly String Suggestion;


        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="ruleName">The name of the rule.</param>
        /// <param name="action">The associated action instance.
        /// If it is null, there was no rule with the name specified in RuleName.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserException(String ruleName, IAction action, int numGiven, int badParamIndex)
        {
            Name = ruleName;
            Action = action;
            NumGiven = numGiven;
            BadParamIndex = badParamIndex;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule with the
        /// given name does not exist or input or output parameters do not match, or a method was called on a type not supporting this.
        /// </summary>
        /// <param name="ruleName">Name of the rule or sequence or variable.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(String name, SequenceParserError errorKind)
        {
            Name = name;
            Kind = errorKind;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule/sequence/procedure/function
        /// with the given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="invocation">The rule/sequence/procedure/function invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(Invocation invocation, int numGiven, SequenceParserError errorKind)
            : this(invocation, numGiven, errorKind, -1)
        {
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule/sequence/procedure/function 
        /// with the given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="invocation">The rule/sequence/procedure/function invocation.</param>
        /// <param name="errorKind">The kind of error.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserException(Invocation invocation, int numGiven, SequenceParserError errorKind, int badParamIndex)
        {
            Kind = errorKind;
            Name = invocation.Name;
            if(invocation is RuleInvocationInterpreted)
                Action = ((RuleInvocationInterpreted)invocation).Action;
            NumGiven = numGiven;
            BadParamIndex = badParamIndex;
            ClassifyDefinitionType(invocation, out DefType);
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
            if(errorKind == SequenceParserError.MatchClassError)
                FilterName = filterNameOrEntityName;
            else if(errorKind == SequenceParserError.FilterParameterError)
                FilterName = filterNameOrEntityName;
            else
                EntityName = filterNameOrEntityName;
            Name = ruleName;
            Kind = errorKind;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, 
        /// when the filter with the given name can't be applied to the rule of the given name
        /// or when the pattern of the rule of the given name does not contain an entity of the given name.
        /// Allows to give a suggestion string hinting the user at a possible solution.
        /// </summary>
        /// <param name="ruleName">Name of the rule.</param>
        /// <param name="filterNameOrEntityName">Name of the filter which was mis-applied or name of the entity which is not conained in the rule.</param>
        /// <param name="errorKind">The kind of error.</param>
        /// <param name="suggestion">A suggestion to display to the user.</param>
        public SequenceParserException(String ruleName, String filterNameOrEntityName, SequenceParserError errorKind, String suggestion)
            : this(ruleName, filterNameOrEntityName, errorKind)
        {
            Suggestion = suggestion;
        }

        /// <summary>
        /// The error message of the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                // TODO: function

                switch (this.Kind)
                {
                case SequenceParserError.UnknownRuleOrSequence:
                    return "Unknown rule/sequence: \"" + this.Name + "\"";

                case SequenceParserError.BadNumberOfParameters:
                    if(this.Action == null) {
                        return "Wrong number of input parameters for " + DefinitionTypeName + " \"" + this.Name + "\"";
                    } else if(this.Action.RulePattern.Inputs.Length != this.NumGiven) {
                        return "Wrong number of input parameters for " + DefinitionTypeName + " \"" + this.Name + "\"!";
                    } else {
                        goto default;
                    }

                case SequenceParserError.BadNumberOfReturnParameters:
                    if(this.Action == null) {
                        return "Wrong number of output parameters for " + DefinitionTypeName + " \"" + this.Name + "\"";
                    } else if(this.Action.RulePattern.Outputs.Length != this.NumGiven) {
                        return "Wrong number of output parameters for " + DefinitionTypeName + " \"" + this.Name + "\"!";
                    } else {
                        goto default;
                    }

                case SequenceParserError.BadParameter:
                    return "The " + (this.BadParamIndex + 1) + ". parameter is not valid for " + DefinitionTypeName + " \"" + this.Name + "\"!";

                case SequenceParserError.BadReturnParameter:
                    return "The " + (this.BadParamIndex + 1) + ". return parameter is not valid for " + DefinitionTypeName + " \"" + this.Name + "\"!";

                case SequenceParserError.RuleNameUsedByVariable:
                    return "The name of the variable conflicts with the name of " + DefinitionTypeName + " \"" + this.Name + "\"!";

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    return "The variable \"" + this.Name + "\" may neither receive parameters nor return values!";

                case SequenceParserError.UnknownAttribute:
                    return "Unknown attribute \"" + this.Name + "\"!";

                case SequenceParserError.UnknownProcedure:
                    return "Unknown procedure \"" + this.Name + "\")!";

                case SequenceParserError.UnknownFunction:
                    return "Unknown function \"" + this.Name + "\")!";

                case SequenceParserError.TypeMismatch:
                    return "The construct \"" + this.VariableOrFunctionName + "\" expects:" + this.ExpectedType + " but is /given " + this.GivenType + "!";

                case SequenceParserError.OperatorNotFound:
                    return "No operator " + this.LeftType + this.VariableOrFunctionName + this.RightType + " available (for \"" + this.Expression + "\")! Or a division-by-zero/runtime error occured.";

                case SequenceParserError.MatchClassError:
                    return "Unknown match class \"" + this.Name + "\" in filter call \"" + this.FilterName + "\"!";

                case SequenceParserError.FilterError:
                    return "The filter \"" + this.FilterName + "\" can't be applied to \"" + this.Name + "\"! "
                        + (Suggestion!=null ? "\n" + Suggestion : "");

                case SequenceParserError.FilterParameterError:
                    return "Filter parameter mismatch for filter \"" + this.FilterName + "\" applied to \"" + this.Name + "\"!";

                case SequenceParserError.SubgraphError:
                    return "The construct \"" + this.VariableOrFunctionName  + "\" does not support subgraph prefixes!";

                case SequenceParserError.SubgraphTypeError:
                    return "The subgraph prefix \"" + this.VariableOrFunctionName + "\" must be of type:" + this.ExpectedType + " but is /given " + this.GivenType + "!";

                case SequenceParserError.UnknownRule:
                    return "Unknown rule \"" + this.Name + "\" (in match<" + this.Name + ">)!";

                case SequenceParserError.UnknownPatternElement:
                    return "The rule \"" + this.Name + "\" does not contain a (top-level) element \"" + this.EntityName + "\" (so type match<" + this.Name + "> does not)!";

                case SequenceParserError.UserMethodsOnlyAvailableForGraphElements:
                    return "The type \"" + this.Name + "\" does not support user methods";

                case SequenceParserError.UnknownIndexAccessDirection:
                    return "The index access direction \"" + this.Name + "\" is not supported, must be ascending or descending";

                case SequenceParserError.TwoDifferentIndexNames:
                    return "A different index name than \"" + this.Name + "\" is given for the second bound";

                case SequenceParserError.TwoLowerBounds:
                    return "Two lower bounds specified in accessing index \"" + this.Name + "\"";

                case SequenceParserError.TwoUpperBounds:
                    return "Two upper bounds specified in accessing index \"" + this.Name + "\"";

                default:
                    return "Invalid error kind: " + this.Kind;
                }
            }
        }

        void ClassifyDefinitionType(Invocation invocation, out DefinitionType DefType)
        {
            if(invocation is RuleInvocation)
                DefType = DefinitionType.Action;
            else if(invocation is SequenceInvocation)
                DefType = DefinitionType.Sequence;
            else if(invocation is ProcedureInvocation)
                DefType = DefinitionType.Procedure;
            else if(invocation is FunctionInvocation)
                DefType = DefinitionType.Function;
            else
                DefType = DefinitionType.Unknown;
        }

        public string DefinitionTypeName
        {
            get
            {
                switch(DefType)
                {
                    case DefinitionType.Action:
                        return "rule/test";
                    case DefinitionType.Sequence:
                        return "sequence";
                    case DefinitionType.Procedure:
                        return "procedure";
                    case DefinitionType.Function:
                        return "function";
                    default:
                        return "definition";
                }
            }
        }
    }
}
