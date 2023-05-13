/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the kind of call issue.
    /// </summary>
    public enum CallIssueType
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
        /// The rule is unknown (only rule name available, originating from match type)
        /// </summary>
        UnknownRule,

        /// <summary>
        /// Used in case of a call parameter issue (when the callable entity is known)
        /// </summary>
        Unspecified
    }

    /// <summary>
    /// Specifies the kind of call parameter issue.
    /// </summary>
    public enum CallParameterIssueType
    {
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
    }

    /// <summary>
    /// Specifies the kind of the name definition.
    /// </summary>
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
    public abstract class SequenceParserException : Exception
    {
        /// <summary>
        /// Typically the name of the primary erroneous entity (may be also a compound construct)
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// A second entity giving some further error information, if available (i.e. not null)
        /// </summary>
        public virtual String Context { get { return null; } }

        /// <summary>
        /// The error message of the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                return "Unspecific error (with " + Name + ")" + (Context != null ? "(and with " + Context + ")" : "");
            }
        }
    }

    public class SequenceParserExceptionUnknownAttribute : SequenceParserException
    {
        public override String Name { get { return UnknownAttribute; } }
        public readonly String UnknownAttribute;
        public override String Context { get { return AttributeBearerType; } }
        public readonly String AttributeBearerType;


        /// <summary>
        /// Creates an instance with the name of the unknown attribute and its type.
        /// </summary>
        public SequenceParserExceptionUnknownAttribute(String unknownAttribute, String attributeBearerType)
        {
            UnknownAttribute = unknownAttribute;
            AttributeBearerType = attributeBearerType;
        }

        public override string Message
        {
            get
            {
                return "Unknown attribute \"" + UnknownAttribute + "\" (of type \"" + AttributeBearerType + "\")!";
            }
        }
    }

    public class SequenceParserExceptionUnknownMatchMember : SequenceParserException
    {
        public override String Name { get { return UnknownMatchMember; } }
        public readonly String UnknownMatchMember;
        public override String Context { get { return MatchType; } }
        public readonly String MatchType;


        /// <summary>
        /// Creates an instance with the name of the unknown match member and its type.
        /// </summary>
        public SequenceParserExceptionUnknownMatchMember(String unknownMatchMember, String matchType)
        {
            UnknownMatchMember = unknownMatchMember;
            MatchType = matchType;
        }

        public override string Message
        {
            get
            {
                return "Unknown member \"" + UnknownMatchMember + "\" (of type \"" + MatchType + "\")!";
            }
        }
    }

    public class SequenceParserExceptionSubgraphError : SequenceParserException
    {
        public override String Name { get { return Construct; } }
        public readonly String Construct;
        public override String Context { get { return SubgraphPrefix; } }
        public readonly String SubgraphPrefix;


        /// <summary>
        /// Creates an instance with the construct(/variable) that comes with a subgraph prefix (but shouldn't) and the subgraph prefix.
        /// </summary>
        public SequenceParserExceptionSubgraphError(String construct, String subgraphPrefix)
        {
            Construct = construct;
            SubgraphPrefix = subgraphPrefix;
        }

        public override string Message
        {
            get
            {
                return "The construct(/variable) \"" + Construct + "\" does not support a subgraph prefix (given \"" + SubgraphPrefix + ".\")!";
            }
        }
    }

    public class SequenceParserExceptionUserMethodsOnlyAvailableForInheritanceTypes : SequenceParserException
    {
        public override String Name { get { return Type; } }
        public readonly String Type;
        public override String Context { get { return MethodCalled; } }
        public readonly String MethodCalled;


        /// <summary>
        /// Creates an instance with the type a method is called on (not supporting user methods; and the method).
        /// </summary>
        public SequenceParserExceptionUserMethodsOnlyAvailableForInheritanceTypes(String type, String methodCalled)
        {
            Type = type;
            MethodCalled = methodCalled;
        }

        public override string Message
        {
            get
            {
                return "The type \"" + Type + "\" does not support user methods (given is \"" + MethodCalled + "\")";
            }
        }
    }

    public abstract class SequenceParserExceptionIndexIssue : SequenceParserException
    {
        public override string Message
        {
            get
            {
                return "Issue with index \"" + Name + "\"";
            }
        }
    }

    public class SequenceParserExceptionIndexUnknownAccessDirection : SequenceParserExceptionIndexIssue
    {
        public override String Name { get { return IndexAccessDirection; } }
        public readonly String IndexAccessDirection;
        public override String Context { get { return IndexName; } }
        public readonly String IndexName;


        /// <summary>
        /// Creates an instance with the erroneous index access direction (and the name of the index).
        /// </summary>
        public SequenceParserExceptionIndexUnknownAccessDirection(String indexAccessDirection, String indexName)
        {
            IndexAccessDirection = indexAccessDirection;
            IndexName = indexName;
        }

        public override string Message
        {
            get
            {
                return "The index access direction \"" + IndexAccessDirection + "\" is not supported, must be ascending or descending (accessing index \"" + IndexName + "\")";
            }
        }
    }

    public class SequenceParserExceptionIndexConflictingNames : SequenceParserExceptionIndexIssue
    {
        public override String Name { get { return IndexName; } }
        public readonly String IndexName;
        public override String Context { get { return IndexName2; } }
        public readonly String IndexName2;


        /// <summary>
        /// Creates an instance with the name of the index and the conflicting one.
        /// </summary>
        public SequenceParserExceptionIndexConflictingNames(String indexName, String indexName2)
        {
            IndexName = indexName;
            IndexName2 = indexName2;
        }

        public override string Message
        {
            get
            {
                return "The index \"" + IndexName + "\" is constrained with a second bound on the different (thus conflicting) index name \"" + IndexName2 + "\"";
            }
        }
    }

    public class SequenceParserExceptionIndexTwoLowerBounds : SequenceParserExceptionIndexIssue
    {
        public override String Name { get { return IndexName; } }
        public readonly String IndexName;


        /// <summary>
        /// Creates an instance with the name of the index.
        /// </summary>
        public SequenceParserExceptionIndexTwoLowerBounds(String indexName)
        {
            IndexName = indexName;
        }

        public override string Message
        {
            get
            {
                return "Two lower bounds specified in accessing index \"" + IndexName + "\"";
            }
        }
    }

    public class SequenceParserExceptionIndexTwoUpperBounds : SequenceParserExceptionIndexIssue
    {
        public override String Name { get { return IndexName; } }
        public readonly String IndexName;


        /// <summary>
        /// Creates an instance with the name of the index.
        /// </summary>
        public SequenceParserExceptionIndexTwoUpperBounds(String indexName)
        {
            IndexName = indexName;
        }

        public override string Message
        {
            get
            {
                return "Two upper bounds specified in accessing index \"" + IndexName + "\"";
            }
        }
    }

    public class SequenceParserExceptionCallIssue : SequenceParserException
    {
        public override String Name { get { return NameCalled; } }
        public readonly String NameCalled;

        /// <summary>
        /// The kind of the call issue.
        /// </summary>
        public readonly CallIssueType CallIssue;

        /// <summary>
        /// The type of the definition that caused the error, Unknown if no definition was involved.
        /// </summary>
        public readonly DefinitionType DefType = DefinitionType.Unknown;


        /// <summary>
        /// Creates an instance with the name called that does not exist.
        /// </summary>
        public SequenceParserExceptionCallIssue(String nameCalled, DefinitionType defType, CallIssueType callIssue)
        {
            NameCalled = nameCalled;
            CallIssue = callIssue;
            DefType = defType;
        }

        /// <summary>
        /// Creates an instance when input or output parameters do not match.
        /// </summary>
        /// <param name="invocation">The rule/sequence/procedure/function invocation.</param>
        /// <param name="numGiven">The number of inputs or outputs given to the rule.</param>
        /// <param name="callIssue">The kind of call issue.</param>
        public SequenceParserExceptionCallIssue(Invocation invocation, CallIssueType callIssue)
            : this(invocation.Name, ClassifyDefinitionType(invocation), callIssue)
        {
        }

        /// <summary>
        /// The error message of the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                switch(this.CallIssue)
                {
                    case CallIssueType.UnknownRuleOrSequence:
                        return "Unknown rule/sequence: \"" + NameCalled + "\"";

                    case CallIssueType.UnknownProcedure: // as of now only procedure methods, unknown procedures yield a ParseException
                        return "Unknown procedure \"" + NameCalled + "\"!";

                    case CallIssueType.UnknownFunction: // as of now only function methods, unknown functions yield a ParseException
                        return "Unknown function \"" + NameCalled + "\"!";

                    case CallIssueType.UnknownRule:
                        return "Unknown rule \"" + NameCalled + "\" (in match<" + NameCalled + ">)!";

                    default:
                        return base.Message;
                }
            }
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

        protected static DefinitionType ClassifyDefinitionType(Invocation invocation)
        {
            if(invocation is RuleInvocation)
                return DefinitionType.Action;
            else if(invocation is SequenceInvocation)
                return DefinitionType.Sequence;
            else if(invocation is ProcedureInvocation)
                return DefinitionType.Procedure;
            else if(invocation is FunctionInvocation)
                return DefinitionType.Function;
            else
                return DefinitionType.Unknown;
        }
    }

    public class SequenceParserExceptionCallParameterIssue : SequenceParserExceptionCallIssue
    {
        CallParameterIssueType CallParameterIssue;

        /// <summary>
        /// The associated action/sequence/procedure/function instance (in case of interpreted sequences).
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
        /// The index of the bad parameter or the number of expected parameters.
        /// </summary>
        public readonly int BadParamIndexOrNumExpected;

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
        /// Creates an instance with the number of input or output parameters that do not match for the invocation,
        /// or the input or output parameter that does not match for the invocation.
        /// </summary>
        /// <param name="invocation">The rule/sequence/procedure/function invocation.</param>
        /// <param name="numGiven">The number of inputs or outputs given.</param>
        /// <param name="callParameterIssue">The type of call parameter issue.</param>
        /// <param name="badParamIndexOrNumExpected">The index of a bad parameter, or the number of expected parameters.</param>
        public SequenceParserExceptionCallParameterIssue(Invocation invocation, int numGiven, CallParameterIssueType callParameterIssue, int badParamIndexOrNumExpected)
            : base(invocation.Name, ClassifyDefinitionType(invocation), CallIssueType.Unspecified)
        {
            CallParameterIssue = callParameterIssue;
            if(invocation is RuleInvocation)
                Action = SequenceBase.GetAction((RuleInvocation)invocation);
            else if(invocation is SequenceInvocation)
                Sequence = SequenceBase.GetSequence((SequenceInvocation)invocation);
            else if(invocation is ProcedureInvocation)
                Procedure = SequenceBase.GetProcedure((ProcedureInvocation)invocation);
            else if(invocation is FunctionInvocation)
                Function = SequenceBase.GetFunction((FunctionInvocation)invocation);
            NumGiven = numGiven;
            BadParamIndexOrNumExpected = badParamIndexOrNumExpected;
        }

        /// <summary>
        /// The error message of the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                switch(this.CallParameterIssue)
                {
                    case CallParameterIssueType.BadNumberOfParameters:
                        return "Wrong number of input parameters for " + DefinitionTypeName + " \"" + NameCalled + "\" (given: " + NumGiven + " expected: " + BadParamIndexOrNumExpected + ")!";

                    case CallParameterIssueType.BadNumberOfReturnParameters:
                        return "Wrong number of output parameters for " + DefinitionTypeName + " \"" + NameCalled + "\" (given: " + NumGiven + " expected: " + BadParamIndexOrNumExpected + ")!";

                    case CallParameterIssueType.BadParameter:
                        return "The " + (BadParamIndexOrNumExpected + 1) + ". parameter is not valid for " + DefinitionTypeName + " \"" + NameCalled + "\"!";

                    case CallParameterIssueType.BadReturnParameter:
                        return "The " + (BadParamIndexOrNumExpected + 1) + ". return parameter is not valid for " + DefinitionTypeName + " \"" + NameCalled + "\"!";

                    default:
                        return base.Message;
                }
            }
        }
    }

    public class SequenceParserExceptionFilterError : SequenceParserException
    {
        public override String Name { get { return FilterName; } }
        public readonly String FilterName;
        public override String Context { get { return RuleName; } }
        public readonly String RuleName;


        /// <summary>
        /// Creates an instance with the filter that can't be applied to the rule.
        /// </summary>
        public SequenceParserExceptionFilterError(String ruleNameOrMatchClassName, String filterName)
        {
            RuleName = ruleNameOrMatchClassName;
            FilterName = filterName;
        }

        public override string Message
        {
            get
            {
                return "The filter \"" + FilterName + "\" can't be applied to \"" + RuleName + "\"! ";
            }
        }
    }

    public class SequenceParserExceptionFilterParameterError : SequenceParserException
    {
        public override String Name { get { return FilterName; } }
        public readonly String FilterName;
        public override String Context { get { return RuleName; } }
        public readonly String RuleName;


        /// <summary>
        /// Creates an instance with the filter that can't be applied to the rule
        /// because the number of filter arguments does not match the number of filter parameters, or they mismatch in type.
        /// </summary>
        public SequenceParserExceptionFilterParameterError(String ruleNameOrMatchClassName, String filterName)
        {
            RuleName = ruleNameOrMatchClassName;
            FilterName = filterName;
        }

        public override string Message
        {
            get
            {
                return "Filter parameter mismatch for filter \"" + FilterName + "\" applied to \"" + RuleName + "\"!";
            }
        }
    }

    public class SequenceParserExceptionFilterLambdaExpressionError : SequenceParserException
    {
        public override String Name { get { return FilterName; } }
        public readonly String FilterName;
        public override String Context { get { return RuleName; } }
        public readonly String RuleName;
        public readonly String VariableName;


        /// <summary>
        /// Creates an instance with the filter that can't be applied to the rule
        /// because a lambda expression variable mismatches in type.
        /// </summary>
        public SequenceParserExceptionFilterLambdaExpressionError(String ruleNameOrMatchClassName, String filterName, String variableName)
        {
            RuleName = ruleNameOrMatchClassName;
            FilterName = filterName;
            VariableName = variableName;
        }

        public override string Message
        {
            get
            {
                return "Lambda expression variable type mismatch for variable \"" + VariableName + "\" in filter \"" + FilterName + "\" applied to \"" + RuleName + "\"!";
            }
        }
    }

    public class SequenceParserExceptionUnknownMatchClass : SequenceParserException
    {
        public override String Name { get { return MatchClassName; } }
        public readonly String MatchClassName;
        public override String Context { get { return FilterName; } }
        public readonly String FilterName;


        /// <summary>
        /// Creates an instance when the match class is unknown (in a call of the given filter).
        /// </summary>
        public SequenceParserExceptionUnknownMatchClass(String matchClassName, String filterName)
        {
            MatchClassName = matchClassName;
            FilterName = filterName;
        }

        public override string Message
        {
            get
            {
                return "Unknown match class \"" + MatchClassName + "\" (in filter call \"" + FilterName + "\")!";
            }
        }
    }

    public class SequenceParserExceptionMatchClassNotImplemented : SequenceParserException
    {
        public override String Name { get { return MatchClassName; } }
        public readonly String MatchClassName;
        public override String Context { get { return RuleName; } }
        public readonly String RuleName;


        /// <summary>
        /// Creates an instance when match class with the given name is not implemented by the rule with the given name.
        /// </summary>
        public SequenceParserExceptionMatchClassNotImplemented(String matchClassName, String ruleName)
        {
            MatchClassName = matchClassName;
            RuleName = ruleName;
        }

        public override string Message
        {
            get
            {
                return "Match class \"" + MatchClassName + "\" is not implemented by rule \"" + RuleName + "\"!";
            }
        }
    }

    public class SequenceParserExceptionUnknownPatternElement : SequenceParserException
    {
        public override String Name { get { return EntityName; } }
        public readonly String EntityName;
        public override String Context { get { return RuleName; } }
        public readonly String RuleName;


        /// <summary>
        /// Creates an instance when the entity of the given name does not exist in the pattern of the rule of the given name.
        /// </summary>
        public SequenceParserExceptionUnknownPatternElement(String ruleNameOrMatchClassName, String entityName)
        {
            RuleName = ruleNameOrMatchClassName;
            EntityName = entityName;
        }

        public override string Message
        {
            get
            {
                return "The rule \"" + RuleName + "\" (so its type match<" + RuleName + ">) does not contain a (top-level) element \"" + EntityName + "\"!";
            }
        }
    }

    public class SequenceParserExceptionTypeMismatch : SequenceParserException
    {
        public override String Name { get { return Construct; } }
        public readonly String Construct;

        /// <summary>
        /// The expected type or types
        /// </summary>
        public readonly String ExpectedType;

        /// <summary>
        /// The given type
        /// </summary>
        public readonly String GivenType;


        /// <summary>
        /// Creates an instance when the expected type does not match the given type of the variable or function.
        /// This includes parameter (type) mismatches of builtin functions and of subgraph prefixes to rule/sequence calls.
        /// </summary>
        public SequenceParserExceptionTypeMismatch(String name, String expectedType, String givenType)
        {
            Construct = name;
            ExpectedType = expectedType;
            GivenType = givenType;
        }

        public override string Message
        {
            get
            {
                return "The construct \"" + Construct + "\" expects:" + ExpectedType + " but is /given " + GivenType + "!";
            }
        }
    }

    public class SequenceParserExceptionOperatorNotFound : SequenceParserException
    {
        public override String Name { get { return OperatorName; } }
        public readonly String OperatorName;

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
        /// Creates an instance when an operator is not available for the supplied types.
        /// </summary>
        public SequenceParserExceptionOperatorNotFound(String name, String leftType, String rightType, String expression)
        {
            OperatorName = name;
            LeftType = leftType;
            RightType = rightType;
            Expression = expression;
        }

        public override string Message
        {
            get
            {
                return "No operator " + LeftType + Name + RightType + " available (for \"" + Expression + "\")! Or a division-by-zero/runtime error occured.";
            }
        }
    }
}
