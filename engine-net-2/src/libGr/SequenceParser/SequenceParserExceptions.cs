/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        /// The attribute is not known
        /// </summary>
        UnknownAttribute,

        /// <summary>
        /// The member (of match type) is not known
        /// </summary>
        UnknownMatchMember,

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
        /// The match class is not implemented by the rule
        /// </summary>
        MatchClassNotImplementedError,

        /// <summary>
        /// The given filter can't be applied to the given rule or match class
        /// </summary>
        FilterError,

        /// <summary>
        /// The parameters of the given filter applied to the given rule don't fit to the declaration
        /// </summary>
        FilterParameterError,

        /// <summary>
        /// The type of the lambda expression variables doesn't fit to the (matches) array
        /// </summary>
        FilterLambdaExpressionError,

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
        /// The primary erroneous item (esp. name of the rule/test/sequence/procedure/function/procedure method/function method definition).
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
        /// The type of the definition that caused the error, Unknown if no definition was involved.
        /// </summary>
        public readonly DefinitionType DefType = DefinitionType.Unknown;

        /// <summary>
        /// The associated action/sequence/procedure/function instance.
        /// </summary>
        public readonly IAction Action;
        public readonly ISequenceDefinition Sequence;
        public readonly IProcedureDefinition Procedure;
        public readonly IFunctionDefinition Function;

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
        /// The sub-expression as string for which the operator given in Name 
        /// was not defined for the types given in LeftType and RightType
        /// </summary>
        public readonly String Expression;

        /// <summary>
        /// A suggestion to the user hinting at a possible fix.
        /// </summary>
        public readonly String Suggestion;



        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser,.
        /// with the name of the offending entity, and the error kind.
        /// </summary>
        /// <param name="name">Name of the entity.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(String name, SequenceParserError errorKind)
        {
            Name = name;
            Kind = errorKind;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when input or output parameters do not match
        /// for the rule/sequence/procedure/function.
        /// </summary>
        /// <param name="invocation">The rule/sequence/procedure/function invocation.</param>
        /// <param name="numGiven">The number of inputs or outputs given to the rule.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(Invocation invocation, int numGiven, SequenceParserError errorKind)
            : this(invocation, numGiven, errorKind, -1)
        {
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule/sequence/procedure/function
        /// with the given name does not exist.
        /// </summary>
        public SequenceParserException(String name, DefinitionType defType, SequenceParserError errorKind)
        {
            Name = name;
            DefType = defType;
            Kind = errorKind;
            NumGiven = -1;
            BadParamIndex = -1;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser, when the rule/sequence/procedure/function 
        /// with the given name does not exist or input or output parameters do not match.
        /// </summary>
        /// <param name="invocation">The rule/sequence/procedure/function invocation.</param>
        /// <param name="numGiven">The number of inputs or outputs given to the rule.</param>
        /// <param name="errorKind">The kind of error.</param>
        /// <param name="badParamIndex">The index of a bad parameter or -1 if another error occurred.</param>
        public SequenceParserException(Invocation invocation, int numGiven, SequenceParserError errorKind, int badParamIndex)
        {
            Kind = errorKind;
            Name = invocation.Name;
            if(invocation is RuleInvocation)
                Action = SequenceBase.GetAction((RuleInvocation)invocation);
            else if(invocation is SequenceInvocation)
                Sequence = SequenceBase.GetSequence((SequenceInvocation)invocation);
            else if(invocation is ProcedureInvocation)
                Procedure = SequenceBase.GetProcedure((ProcedureInvocation)invocation);
            else if(invocation is FunctionInvocation)
                Function = SequenceBase.GetFunction((FunctionInvocation)invocation);
            NumGiven = numGiven;
            BadParamIndex = badParamIndex;
            ClassifyDefinitionType(invocation, out DefType);
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser,
        /// when the expected type does not match the given type of the variable of function.
        /// </summary>
        public SequenceParserException(String name, String expectedType, String givenType)
            : this(name, expectedType, givenType, SequenceParserError.TypeMismatch)
        {
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser,
        /// when the expected type does not match the given type of the variable of function (TypeMismatch),
        /// or in case of SubgraphError/SubgraphTypeError.
        /// </summary>
        public SequenceParserException(String name, String expectedType, String givenType, SequenceParserError errorKind)
        {
            Name = name;
            ExpectedType = expectedType;
            GivenType = givenType;
            Kind = errorKind;
        }

        /// <summary>
        /// Creates an instance of a SequenceParserException used by the SequenceParser,
        /// when an operator is not available for the supplied types.
        /// </summary>
        public SequenceParserException(String name, String leftType, String rightType, String expression)
        {
            Name = name;
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
        /// <param name="ruleNameOrMatchClassName">Name of the rule or match class.</param>
        /// <param name="filterNameOrEntityName">Name of the filter which was mis-applied or name of the entity which is not conained in the rule.</param>
        /// <param name="errorKind">The kind of error.</param>
        public SequenceParserException(String ruleNameOrMatchClassName, String filterNameOrEntityName, SequenceParserError errorKind)
        {
            if(errorKind == SequenceParserError.FilterError
                || errorKind == SequenceParserError.MatchClassError
                || errorKind == SequenceParserError.MatchClassNotImplementedError
                || errorKind == SequenceParserError.FilterParameterError
                || errorKind == SequenceParserError.FilterLambdaExpressionError)
                FilterName = filterNameOrEntityName;
            else
                EntityName = filterNameOrEntityName;
            Name = ruleNameOrMatchClassName;
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
                switch (this.Kind)
                {
                case SequenceParserError.UnknownRuleOrSequence:
                    return "Unknown rule/sequence: \"" + this.Name + "\"";

                case SequenceParserError.BadNumberOfParameters:
                    return "Wrong number of input parameters for " + DefinitionTypeName + " \"" + this.Name + "\"!";
 
                case SequenceParserError.BadNumberOfReturnParameters:
                    return "Wrong number of output parameters for " + DefinitionTypeName + " \"" + this.Name + "\"!";

                case SequenceParserError.BadParameter:
                    return "The " + (this.BadParamIndex + 1) + ". parameter is not valid for " + DefinitionTypeName + " \"" + this.Name + "\"!";

                case SequenceParserError.BadReturnParameter:
                    return "The " + (this.BadParamIndex + 1) + ". return parameter is not valid for " + DefinitionTypeName + " \"" + this.Name + "\"!";

                case SequenceParserError.UnknownAttribute:
                    return "Unknown attribute \"" + this.Name + "\"!";

                case SequenceParserError.UnknownMatchMember:
                    return "Unknown member (of match type) \"" + this.Name + "\"!";

                case SequenceParserError.UnknownProcedure: // as of now only procedure methods, unknown procedures yield a ParseException
                    return "Unknown procedure \"" + this.Name + "\"!";

                case SequenceParserError.UnknownFunction: // as of now only function methods, unknown functions yield a ParseException
                    return "Unknown function \"" + this.Name + "\"!";

                case SequenceParserError.TypeMismatch:
                    return "The construct \"" + this.Name + "\" expects:" + this.ExpectedType + " but is /given " + this.GivenType + "!";

                case SequenceParserError.OperatorNotFound:
                    return "No operator " + this.LeftType + this.Name + this.RightType + " available (for \"" + this.Expression + "\")! Or a division-by-zero/runtime error occured.";

                case SequenceParserError.MatchClassError:
                    return "Unknown match class \"" + this.Name + "\" in filter call \"" + this.FilterName + "\"!";

                case SequenceParserError.MatchClassNotImplementedError:
                    return "Match class \"" + this.Name + "\" is not implemented by rule \"" + this.FilterName + "\"!";

                case SequenceParserError.FilterError:
                    return "The filter \"" + this.FilterName + "\" can't be applied to \"" + this.Name + "\"! "
                        + (Suggestion!=null ? "\n" + Suggestion : "");

                case SequenceParserError.FilterParameterError:
                    return "Filter parameter mismatch for filter \"" + this.FilterName + "\" applied to \"" + this.Name + "\"!";

                case SequenceParserError.FilterLambdaExpressionError:
                    return "Lambda expression variable type mismatch for filter \"" + this.FilterName + "\" applied to \"" + this.Name + "\"!";

                case SequenceParserError.SubgraphError:
                    return "The construct \"" + this.Name  + "\" does not support subgraph prefixes!";

                case SequenceParserError.SubgraphTypeError:
                    return "The construct \"" + this.Name + "\" expects a subgraph prefix of type:" + this.ExpectedType + " but is /given " + this.GivenType + "!";

                case SequenceParserError.UnknownRule:
                    return "Unknown rule \"" + this.Name + "\" (in match<" + this.Name + ">)!";

                case SequenceParserError.UnknownPatternElement:
                    return "The rule \"" + this.Name + "\" (so its type match<" + this.Name + ">) does not contain a (top-level) element \"" + this.EntityName + "\"!";

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
