/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the actual subtype of a sequence expression.
    /// A new expression type -> you must add the corresponding class down below 
    /// and adapt the lgspSequenceGenerator.
    /// </summary>
    public enum SequenceExpressionType
    {
        Conditional,
        Except, LazyOr, LazyAnd, StrictOr, StrictXor, StrictAnd,
        Not, UnaryPlus, UnaryMinus, BitwiseComplement, Cast,
        Equal, NotEqual, Lower, LowerEqual, Greater, GreaterEqual, StructuralEqual,
        ShiftLeft, ShiftRight, ShiftRightUnsigned,
        Plus, Minus, Mul, Div, Mod, // nice-to-have addition: all the other operators and functions/methods from the rule language expressions
        Constant, Variable, This, New,
        MatchClassConstructor,
        SetConstructor, MapConstructor, ArrayConstructor, DequeConstructor,
        SetCopyConstructor, MapCopyConstructor, ArrayCopyConstructor, DequeCopyConstructor,
        ContainerAsArray,
        StringLength, StringStartsWith, StringEndsWith,
        StringSubstring, StringReplace, StringToLower, StringToUpper, StringAsArray,
        MapDomain, MapRange,
        Random,
        Def,
        IsVisited,
        InContainerOrString, ContainerEmpty, ContainerSize, ContainerAccess, ContainerPeek,
        ArrayOrDequeOrStringIndexOf, ArrayOrDequeOrStringLastIndexOf, ArrayIndexOfOrdered,
        ArraySum, ArrayProd, ArrayMin, ArrayMax, ArrayAvg, ArrayMed, ArrayMedUnordered, ArrayVar, ArrayDev, ArrayAnd, ArrayOr,
        ArrayOrDequeAsSet, ArrayAsMap, ArrayAsDeque, ArrayAsString,
        ArraySubarray, DequeSubdeque,
        ArrayOrderAscending, ArrayOrderDescending, ArrayGroup, ArrayKeepOneForEach, ArrayReverse, ArrayShuffle,
        ArrayExtract, ArrayOrderAscendingBy, ArrayOrderDescendingBy, ArrayGroupBy, ArrayKeepOneForEachBy,
        ArrayIndexOfBy, ArrayLastIndexOfBy, ArrayIndexOfOrderedBy,
        ArrayMap, ArrayRemoveIf, ArrayMapStartWithAccumulateBy,
        ElementFromGraph, NodeByName, EdgeByName, NodeByUnique, EdgeByUnique,
        Source, Target, Opposite,
        GraphElementAttributeOrElementOfMatch, GraphElementAttribute, ElementOfMatch,
        Nodes, Edges,
        CountNodes, CountEdges,
        Now,
        MathMin, MathMax, MathAbs, MathCeil, MathFloor, MathRound, MathTruncate,
        MathSqr, MathSqrt, MathPow, MathLog, MathSgn,
        MathSin, MathCos, MathTan, MathArcSin, MathArcCos, MathArcTan,
        MathPi, MathE,
        MathByteMin, MathByteMax, MathShortMin, MathShortMax, MathIntMin, MathIntMax, MathLongMin, MathLongMax,
        MathFloatMin, MathFloatMax, MathDoubleMin, MathDoubleMax,
        Empty, Size,
        AdjacentNodes, AdjacentNodesViaIncoming, AdjacentNodesViaOutgoing,
        IncidentEdges, IncomingEdges, OutgoingEdges,
        ReachableNodes, ReachableNodesViaIncoming, ReachableNodesViaOutgoing,
        ReachableEdges, ReachableEdgesViaIncoming, ReachableEdgesViaOutgoing,
        BoundedReachableNodes, BoundedReachableNodesViaIncoming, BoundedReachableNodesViaOutgoing,
        BoundedReachableEdges, BoundedReachableEdgesViaIncoming, BoundedReachableEdgesViaOutgoing,
        BoundedReachableNodesWithRemainingDepth, BoundedReachableNodesWithRemainingDepthViaIncoming, BoundedReachableNodesWithRemainingDepthViaOutgoing,
        CountAdjacentNodes, CountAdjacentNodesViaIncoming, CountAdjacentNodesViaOutgoing,
        CountIncidentEdges, CountIncomingEdges, CountOutgoingEdges,
        CountReachableNodes, CountReachableNodesViaIncoming, CountReachableNodesViaOutgoing,
        CountReachableEdges, CountReachableEdgesViaIncoming, CountReachableEdgesViaOutgoing,
        CountBoundedReachableNodes, CountBoundedReachableNodesViaIncoming, CountBoundedReachableNodesViaOutgoing,
        CountBoundedReachableEdges, CountBoundedReachableEdgesViaIncoming, CountBoundedReachableEdgesViaOutgoing,
        IsAdjacentNodes, IsAdjacentNodesViaIncoming, IsAdjacentNodesViaOutgoing,
        IsIncidentEdges, IsIncomingEdges, IsOutgoingEdges,
        IsReachableNodes, IsReachableNodesViaIncoming, IsReachableNodesViaOutgoing,
        IsReachableEdges, IsReachableEdgesViaIncoming, IsReachableEdgesViaOutgoing,
        IsBoundedReachableNodes, IsBoundedReachableNodesViaIncoming, IsBoundedReachableNodesViaOutgoing,
        IsBoundedReachableEdges, IsBoundedReachableEdgesViaIncoming, IsBoundedReachableEdgesViaOutgoing,
        InducedSubgraph, DefinedSubgraph,
        EqualsAny, GetEquivalent,
        Nameof, Uniqueof, Typeof,
        ExistsFile, Import,
        Copy,
        Canonize,
        RuleQuery, MultiRuleQuery,
        MappingClause,
        Scan, TryScan,
        FunctionCall, FunctionMethodCall
    }

    /// <summary>
    /// A sequence expression object with references to child sequence expressions.
    /// A sequence expression is a side effect free computation returning a value (a query).
    /// </summary>
    public abstract class SequenceExpression : SequenceComputation
    {
        /// <summary>
        /// The type of the sequence expression (e.g. Variable or IsVisited)
        /// </summary>
        public readonly SequenceExpressionType SequenceExpressionType;

        /// <summary>
        /// Initializes a new SequenceExpression object with the given sequence expression type.
        /// </summary>
        /// <param name="seqExprType">The sequence expression type.</param>
        protected SequenceExpression(SequenceExpressionType seqExprType)
            : base(SequenceComputationType.Expression)
        {
            SequenceExpressionType = seqExprType;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The sequence expression to be copied.</param>
        protected SequenceExpression(SequenceExpression that)
            : base(that)
        {
            SequenceExpressionType = that.SequenceExpressionType;
        }

        public override bool HasSequenceType(SequenceType sequenceType)
        {
            return false;
        }

        /// <summary>
        /// Copies the sequence computation deeply so that
        /// - the global Variables are kept
        /// - the local Variables are replaced by copies initialized to null
        /// Used for cloning defined sequences before executing them if needed.
        /// Needed if the defined sequence is currently executed to prevent state corruption.
        /// </summary>
        /// <param name="originalToCopy">A map used to ensure that every instance of a variable is mapped to the same copy</param>
        /// <param name="procEnv">The graph processing environment</param>
        /// <returns>The copy of the sequence computation</returns>
        internal override sealed SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return CopyExpression(originalToCopy, procEnv);
        }

        /// <summary>
        /// Copies the sequence expression deeply so that
        /// - the global Variables are kept
        /// - the local Variables are replaced by copies initialized to null
        /// Used for cloning defined sequences before executing them if needed.
        /// Needed if the defined sequence is currently executed to prevent state corruption.
        /// </summary>
        /// <param name="originalToCopy">A map used to ensure that every instance of a variable is mapped to the same copy</param>
        /// <param name="procEnv">The graph processing environment</param>
        /// <returns>The copy of the sequence expression</returns>
        internal abstract SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv);

        /// <summary>
        /// Returns the type of the sequence expression
        /// default behaviour: returns "boolean"
        /// </summary>
        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        /// <summary>
        /// Evaluates this sequence expression.
        /// Implemented by calling execute, every expression is a computation.
        /// </summary>
        /// <param name="procEnv">The graph processing environment on which this sequence expression is to be evaluated.
        ///     Contains especially the graph on which this sequence expression is to be evaluated.
        ///     And the user proxy queried when choices are due.</param>
        /// <returns>The value resulting from computing this sequence expression</returns>
        public object Evaluate(IGraphProcessingEnvironment procEnv)
        {
            return ExecuteImpl(procEnv);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                foreach(SequenceExpression expr in ChildrenExpression)
                {
                    yield return expr;
                }
            }
        }

        public override sealed bool ReturnsValue
        {
            get { return true; }
        }

        /// <summary>
        /// Enumerates all child sequence expression objects
        /// </summary>
        public abstract IEnumerable<SequenceExpression> ChildrenExpression { get; }

        protected String GetOperationType(String inputType)
        {
            if(inputType == "byte")
                return "int"; // byte input -> upcast to int operation
            else if(inputType == "short")
                return "int"; // short input -> upcast int operation
            else if(inputType == "float")
                return "double"; // float input -> upcast to double operation
            else
                return inputType;
        }

        protected bool IsNumericType(String inputType)
        {
            switch(inputType)
            {
            case "byte":
            case "short":
            case "int":
            case "long":
            case "float":
            case "double":
                return true;
            default:
                return false;
            }
        }
    }

    /// <summary>
    /// A sequence expression over a container object (resulting from a variable or a method call)
    /// </summary>
    public abstract class SequenceExpressionContainer : SequenceExpression
    {
        public readonly SequenceExpression ContainerExpr;

        protected SequenceExpressionContainer(SequenceExpressionType type, SequenceExpression containerExpr)
            : base(type)
        {
            ContainerExpr = containerExpr;
        }

        protected SequenceExpressionContainer(SequenceExpressionContainer that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            ContainerExpr = that.ContainerExpr.CopyExpression(originalToCopy, procEnv);
        }

        public string ContainerType(SequenceCheckingEnvironment env)
        {
            return ContainerExpr.Type(env);
        }

        public string CheckAndReturnContainerType(SequenceCheckingEnvironment env)
        {
            string ContainerType = ContainerExpr.Type(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        public object ContainerValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerExpr is SequenceExpressionAttributeAccess)
                return ((SequenceExpressionAttributeAccess)ContainerExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return ContainerExpr.Evaluate(procEnv);
        }

        public IDictionary SetValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerExpr is SequenceExpressionAttributeAccess)
                return (IDictionary)((SequenceExpressionAttributeAccess)ContainerExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return (IDictionary)ContainerExpr.Evaluate(procEnv);
        }

        public IDictionary MapValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerExpr is SequenceExpressionAttributeAccess)
                return (IDictionary)((SequenceExpressionAttributeAccess)ContainerExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return (IDictionary)ContainerExpr.Evaluate(procEnv);
        }

        public IList ArrayValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerExpr is SequenceExpressionAttributeAccess)
                return (IList)((SequenceExpressionAttributeAccess)ContainerExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return (IList)ContainerExpr.Evaluate(procEnv);
        }

        public IDeque DequeValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerExpr is SequenceExpressionAttributeAccess)
                return (IDeque)((SequenceExpressionAttributeAccess)ContainerExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return (IDeque)ContainerExpr.Evaluate(procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerExpr.Type(env);
        }

        public string Name
        {
            get { return ContainerExpr.Symbol; }
        }
    }

    /// <summary>
    /// A sequence binary expression object with references to the left and right child sequence expressions.
    /// </summary>
    public abstract class SequenceBinaryExpression : SequenceExpression
    {
        public readonly SequenceExpression Left;
        public readonly SequenceExpression Right;

        // statically known types of the binary expression
        public string LeftTypeStatic;
        public string RightTypeStatic;
        public string BalancedTypeStatic; // the type of the operator, i.e. the common type of both operands for the operation

        /// <summary>
        /// Initializes a new SequenceBinaryExpression object with the given sequence expression type.
        /// </summary>
        /// <param name="seqExprType">The sequence expression type.</param>
        /// <param name="left">The left sequence expression.</param>
        /// <param name="right">The right sequence expression.</param>
        protected SequenceBinaryExpression(SequenceExpressionType seqExprType, 
            SequenceExpression left, SequenceExpression right)
            : base(seqExprType)
        {
            this.Left = left;
            this.Right = right;
        }

        protected SequenceBinaryExpression(SequenceBinaryExpression that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Left = that.Left.CopyExpression(originalToCopy, procEnv);
            Right = that.Right.CopyExpression(originalToCopy, procEnv);
            LeftTypeStatic = that.LeftTypeStatic;
            RightTypeStatic = that.RightTypeStatic;
            BalancedTypeStatic = that.BalancedTypeStatic;
        }

        protected void BalanceIfStaticallyUnknown(IGraphProcessingEnvironment procEnv, 
            object leftValue, object rightValue, 
            ref string balancedType, ref string leftType, ref string rightType)
        {
            if(balancedType != "")
                return;

            leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
            rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
            balancedType = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);

            if(balancedType == "-")
                throw new SequenceParserExceptionOperatorNotFound(Operator, leftType, rightType, Symbol);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            BalancedTypeStatic = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
            if(BalancedTypeStatic == "-")
                throw new SequenceParserExceptionOperatorNotFound(Operator, LeftTypeStatic, RightTypeStatic, Symbol);
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Left.GetLocalVariables(variables, constructors);
            Right.GetLocalVariables(variables, constructors);
        }

        /// <summary>
        /// Enumerates all child sequence expression objects
        /// </summary>
        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Left;
                yield return Right;
            }
        }

        /// <summary>
        /// Returns this expression as string, in the form of left expression then operator then right expression;
        /// same for all binary expressions
        /// </summary>
        public override string Symbol
        {
            get { return Left.Symbol + Operator + Right.Symbol; }
        }

        /// <summary>
        /// Returns the operator of this binary expression (enclosed in spaces);
        /// abstract property, to be overwritten by concrete operator symbol
        /// </summary>
        public abstract string Operator { get; }
    }


    public class SequenceExpressionConditional : SequenceExpression
    {
        public readonly SequenceExpression Condition;
        public readonly SequenceExpression TrueCase;
        public readonly SequenceExpression FalseCase;

        public SequenceExpressionConditional(SequenceExpression condition, 
            SequenceExpression trueCase, 
            SequenceExpression falseCase)
            : base(SequenceExpressionType.Conditional)
        {
            this.Condition = condition;
            this.TrueCase = trueCase;
            this.FalseCase = falseCase;
        }

        protected SequenceExpressionConditional(SequenceExpressionConditional that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Condition = that.Condition.CopyExpression(originalToCopy, procEnv);
            TrueCase = that.TrueCase.CopyExpression(originalToCopy, procEnv);
            FalseCase = that.FalseCase.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionConditional(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
            
            if(!TypesHelper.IsSameOrSubtype(Condition.Type(env), "boolean", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "boolean", Condition.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ""; // no constraints regarding the types of the expressions to choose from
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Condition.Evaluate(procEnv) ? TrueCase.Evaluate(procEnv) : FalseCase.Evaluate(procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Condition.GetLocalVariables(variables, constructors);
            TrueCase.GetLocalVariables(variables, constructors);
            FalseCase.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Condition;
                yield return TrueCase;
                yield return FalseCase;
            }
        }

        public override int Precedence
        {
            get { return 1; }
        }

        public override string Symbol
        {
            get { return Condition.Symbol + " ? " + TrueCase.Symbol + " : " + FalseCase.Symbol; }
        }
    }

    public class SequenceExpressionExcept : SequenceBinaryExpression
    {
        public SequenceExpressionExcept(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Except, left, right)
        {
        }

        protected SequenceExpressionExcept(SequenceExpressionExcept that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionExcept(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.ExceptObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return 2; } // TODO: adapt all precedences
        }

        public override string Operator
        {
            get { return " \\ "; }
        }
    }

    public class SequenceExpressionLazyOr : SequenceBinaryExpression
    {
        public SequenceExpressionLazyOr(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.LazyOr, left, right)
        {
        }

        protected SequenceExpressionLazyOr(SequenceExpressionLazyOr that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionLazyOr(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) || (bool)Right.Evaluate(procEnv);
        }

        public override int Precedence
        {
            get { return 2; }
        }

        public override string Operator
        {
            get { return " || "; }
        }
    }

    public class SequenceExpressionLazyAnd : SequenceBinaryExpression
    {
        public SequenceExpressionLazyAnd(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.LazyAnd, left, right)
        {
        }

        protected SequenceExpressionLazyAnd(SequenceExpressionLazyAnd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionLazyAnd(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) && (bool)Right.Evaluate(procEnv);
        }

        public override int Precedence
        {
            get { return 3; }
        }

        public override string Operator
        {
            get { return " && "; }
        }
    }

    public class SequenceExpressionStrictOr : SequenceBinaryExpression
    {
        public SequenceExpressionStrictOr(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StrictOr, left, right)
        {
        }

        protected SequenceExpressionStrictOr(SequenceExpressionStrictOr that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStrictOr(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.OrObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return 4; }
        }

        public override string Operator
        {
            get { return " | "; }
        }
    }

    public class SequenceExpressionStrictXor : SequenceBinaryExpression
    {
        public SequenceExpressionStrictXor(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StrictXor, left, right)
        {
        }

        protected SequenceExpressionStrictXor(SequenceExpressionStrictXor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStrictXor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.XorObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return 5; }
        }

        public override string Operator
        {
            get { return " ^ "; }
        }
    }

    public class SequenceExpressionStrictAnd : SequenceBinaryExpression
    {
        public SequenceExpressionStrictAnd(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StrictAnd, left, right)
        {
        }

        protected SequenceExpressionStrictAnd(SequenceExpressionStrictAnd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStrictAnd(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.AndObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return 6; }
        }

        public override string Operator
        {
            get { return " & "; }
        }
    }

    public class SequenceExpressionNot : SequenceExpression
    {
        public readonly SequenceExpression Operand;

        public SequenceExpressionNot(SequenceExpression operand)
            : base(SequenceExpressionType.Not)
        {
            this.Operand = operand;
        }

        protected SequenceExpressionNot(SequenceExpressionNot that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Operand = that.Operand.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNot(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return !(bool)Operand.Evaluate(procEnv);
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Operand.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Operand; }
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return "!" + Operand.Symbol; }
        }
    }

    public class SequenceExpressionUnaryPlus : SequenceExpression
    {
        public readonly SequenceExpression Operand;

        // statically known types of the unary expression
        public string OperandTypeStatic;
        public string BalancedTypeStatic; // the type of the operator

        public SequenceExpressionUnaryPlus(SequenceExpression operand)
            : base(SequenceExpressionType.UnaryPlus)
        {
            this.Operand = operand;
        }

        protected SequenceExpressionUnaryPlus(SequenceExpressionUnaryPlus that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Operand = that.Operand.CopyExpression(originalToCopy, procEnv);
            OperandTypeStatic = that.OperandTypeStatic;
            BalancedTypeStatic = that.BalancedTypeStatic;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionUnaryPlus(this, originalToCopy, procEnv);
        }

        protected void BalanceIfStaticallyUnknown(IGraphProcessingEnvironment procEnv,
            object operandValue, ref string balancedType, ref string operandType)
        {
            if(balancedType != "")
                return;

            operandType = TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model);
            balancedType = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, operandType, procEnv.Graph.Model);

            if(balancedType == "-")
                throw new SequenceParserExceptionOperatorNotFound("+", operandType, operandType, Symbol);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            OperandTypeStatic = Operand.Type(env);
            BalancedTypeStatic = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
            if(BalancedTypeStatic == "-")
                throw new SequenceParserExceptionOperatorNotFound("+", OperandTypeStatic, OperandTypeStatic, Symbol);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            string OperandTypeStatic = Operand.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object operandValue = Operand.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string operandType = OperandTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, operandValue, ref balancedType, ref operandType);

            try
            {
                return SequenceExpressionExecutionHelper.UnaryPlusObjects(operandValue, balancedType, operandType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound("+", TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Operand.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Operand; }
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return "+" + Operand.Symbol; }
        }
    }

    public class SequenceExpressionUnaryMinus : SequenceExpression
    {
        public readonly SequenceExpression Operand;

        // statically known types of the unary expression
        public string OperandTypeStatic;
        public string BalancedTypeStatic; // the type of the operator

        public SequenceExpressionUnaryMinus(SequenceExpression operand)
            : base(SequenceExpressionType.UnaryMinus)
        {
            this.Operand = operand;
        }

        protected SequenceExpressionUnaryMinus(SequenceExpressionUnaryMinus that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Operand = that.Operand.CopyExpression(originalToCopy, procEnv);
            OperandTypeStatic = that.OperandTypeStatic;
            BalancedTypeStatic = that.BalancedTypeStatic;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionUnaryMinus(this, originalToCopy, procEnv);
        }

        protected void BalanceIfStaticallyUnknown(IGraphProcessingEnvironment procEnv,
            object operandValue, ref string balancedType, ref string operandType)
        {
            if(balancedType != "")
                return;

            operandType = TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model);
            balancedType = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, operandType, procEnv.Graph.Model);

            if(balancedType == "-")
                throw new SequenceParserExceptionOperatorNotFound("-", operandType, operandType, Symbol);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            OperandTypeStatic = Operand.Type(env);
            BalancedTypeStatic = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
            if(BalancedTypeStatic == "-")
                throw new SequenceParserExceptionOperatorNotFound("-", OperandTypeStatic, OperandTypeStatic, Symbol);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            string OperandTypeStatic = Operand.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object operandValue = Operand.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string operandType = OperandTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, operandValue, ref balancedType, ref operandType);

            try
            {
                return SequenceExpressionExecutionHelper.UnaryMinusObjects(operandValue, balancedType, operandType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound("-", TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Operand.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Operand; }
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return "-" + Operand.Symbol; }
        }
    }

    public class SequenceExpressionBitwiseComplement : SequenceExpression
    {
        public readonly SequenceExpression Operand;

        // statically known types of the unary expression
        public string OperandTypeStatic;
        public string BalancedTypeStatic; // the type of the operator

        public SequenceExpressionBitwiseComplement(SequenceExpression operand)
            : base(SequenceExpressionType.BitwiseComplement)
        {
            this.Operand = operand;
        }

        protected SequenceExpressionBitwiseComplement(SequenceExpressionBitwiseComplement that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Operand = that.Operand.CopyExpression(originalToCopy, procEnv);
            OperandTypeStatic = that.OperandTypeStatic;
            BalancedTypeStatic = that.BalancedTypeStatic;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionBitwiseComplement(this, originalToCopy, procEnv);
        }

        protected void BalanceIfStaticallyUnknown(IGraphProcessingEnvironment procEnv,
            object operandValue, ref string balancedType, ref string operandType)
        {
            if(balancedType != "")
                return;

            operandType = TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model);
            balancedType = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, operandType, procEnv.Graph.Model);

            if(balancedType == "-")
                throw new SequenceParserExceptionOperatorNotFound("~", operandType, operandType, Symbol);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            OperandTypeStatic = Operand.Type(env);
            BalancedTypeStatic = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
            if(BalancedTypeStatic == "-")
                throw new SequenceParserExceptionOperatorNotFound("~", OperandTypeStatic, OperandTypeStatic, Symbol);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            string OperandTypeStatic = Operand.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object operandValue = Operand.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string operandType = OperandTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, operandValue, ref balancedType, ref operandType);

            try
            {
                return SequenceExpressionExecutionHelper.UnaryComplement(operandValue, balancedType, operandType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound("~", TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Operand.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Operand; }
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return "~" + Operand.Symbol; }
        }
    }

    public class SequenceExpressionCast : SequenceExpression
    {
        public readonly SequenceExpression Operand;
        public readonly object TargetType;

        public SequenceExpressionCast(SequenceExpression operand, object targetType)
            : base(SequenceExpressionType.Cast)
        {
            this.Operand = operand;
            this.TargetType = targetType;
        }

        protected SequenceExpressionCast(SequenceExpressionCast that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Operand = that.Operand.CopyExpression(originalToCopy, procEnv);
            TargetType = that.TargetType;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCast(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(TargetType is NodeType)
                return ((NodeType)TargetType).Name;
            if(TargetType is EdgeType)
                return ((EdgeType)TargetType).Name;
            if(TargetType is ObjectType)
                return ((ObjectType)TargetType).Name;
            if(TargetType is TransientObjectType)
                return ((TransientObjectType)TargetType).Name;
            return null; // TODO: handle the non-node/edge/object/transient-object-types, too
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Operand.Evaluate(procEnv);
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Operand.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Operand; }
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return "(" + ((InheritanceType)TargetType).Name + ")" + Operand.Symbol; }
        }
    }


    public class SequenceExpressionEqual : SequenceBinaryExpression
    {
        public SequenceExpressionEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Equal, left, right)
        {
        }

        protected SequenceExpressionEqual(SequenceExpressionEqual that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionEqual(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.EqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " == "; }
        }
    }

    public class SequenceExpressionStructuralEqual : SequenceBinaryExpression
    {
        public SequenceExpressionStructuralEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StructuralEqual, left, right)
        {
        }

        protected SequenceExpressionStructuralEqual(SequenceExpressionStructuralEqual that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStructuralEqual(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.StructuralEqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " ~~ "; }
        }
    }

    public class SequenceExpressionNotEqual : SequenceBinaryExpression
    {
        public SequenceExpressionNotEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.NotEqual, left, right)
        {
        }

        protected SequenceExpressionNotEqual(SequenceExpressionNotEqual that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNotEqual(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.NotEqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " != "; }
        }
    }

    public class SequenceExpressionLower : SequenceBinaryExpression
    {
        public SequenceExpressionLower(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Lower, left, right)
        {
        }

        protected SequenceExpressionLower(SequenceExpressionLower that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionLower(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.LowerObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " < "; }
        }
    }

    public class SequenceExpressionLowerEqual : SequenceBinaryExpression
    {
        public SequenceExpressionLowerEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.LowerEqual, left, right)
        {
        }

        protected SequenceExpressionLowerEqual(SequenceExpressionLowerEqual that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionLowerEqual(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.LowerEqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " <= "; }
        }
    }

    public class SequenceExpressionGreater : SequenceBinaryExpression
    {
        public SequenceExpressionGreater(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Greater, left, right)
        {
        }

        protected SequenceExpressionGreater(SequenceExpressionGreater that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionGreater(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.GreaterObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " > "; }
        }
    }

    public class SequenceExpressionGreaterEqual : SequenceBinaryExpression
    {
        public SequenceExpressionGreaterEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.GreaterEqual, left, right)
        {
        }

        protected SequenceExpressionGreaterEqual(SequenceExpressionGreaterEqual that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionGreaterEqual(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.GreaterEqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " >= "; }
        }
    }


    public class SequenceExpressionShiftLeft : SequenceBinaryExpression
    {
        public SequenceExpressionShiftLeft(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.ShiftLeft, left, right)
        {
        }

        protected SequenceExpressionShiftLeft(SequenceExpressionShiftLeft that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionShiftLeft(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.ShiftLeft(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " << "; }
        }
    }

    public class SequenceExpressionShiftRight : SequenceBinaryExpression
    {
        public SequenceExpressionShiftRight(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.ShiftRight, left, right)
        {
        }

        protected SequenceExpressionShiftRight(SequenceExpressionShiftRight that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionShiftRight(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.ShiftRight(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " >> "; }
        }
    }

    public class SequenceExpressionShiftRightUnsigned : SequenceBinaryExpression
    {
        public SequenceExpressionShiftRightUnsigned(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.ShiftRightUnsigned, left, right)
        {
        }

        protected SequenceExpressionShiftRightUnsigned(SequenceExpressionShiftRightUnsigned that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionShiftRightUnsigned(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.ShiftRightUnsigned(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " >>> "; }
        }
    }


    public class SequenceExpressionPlus : SequenceBinaryExpression
    {
        public SequenceExpressionPlus(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Plus, left, right)
        {
        }

        protected SequenceExpressionPlus(SequenceExpressionPlus that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionPlus(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.PlusObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " + "; }
        }
    }

    public class SequenceExpressionMinus : SequenceBinaryExpression
    {
        public SequenceExpressionMinus(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Minus, left, right)
        {
        }

        protected SequenceExpressionMinus(SequenceExpressionMinus that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMinus(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.MinusObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " - "; }
        }
    }

    public class SequenceExpressionMul : SequenceBinaryExpression
    {
        public SequenceExpressionMul(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Mul, left, right)
        {
        }

        protected SequenceExpressionMul(SequenceExpressionMul that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMul(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.MulObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " * "; }
        }
    }

    public class SequenceExpressionDiv : SequenceBinaryExpression
    {
        public SequenceExpressionDiv(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Div, left, right)
        {
        }

        protected SequenceExpressionDiv(SequenceExpressionDiv that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionDiv(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.DivObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " / "; }
        }
    }

    public class SequenceExpressionMod : SequenceBinaryExpression
    {
        public SequenceExpressionMod(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Mod, left, right)
        {
        }

        protected SequenceExpressionMod(SequenceExpressionMod that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMod(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            BalanceIfStaticallyUnknown(procEnv, leftValue, rightValue, ref balancedType, ref leftType, ref rightType);

            try
            {
                return SequenceExpressionExecutionHelper.ModObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserExceptionOperatorNotFound(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence
        {
            get { return -1; }
        }

        public override string Operator
        {
            get { return " % "; }
        }
    }


    public class SequenceExpressionConstant : SequenceExpression
    {
        public object Constant;

        public SequenceExpressionConstant(object constant)
            : base(SequenceExpressionType.Constant)
        {
            Constant = constant;
        }

        protected SequenceExpressionConstant(SequenceExpressionConstant that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Constant = that.Constant;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionConstant(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(Constant != null)
                return TypesHelper.XgrsTypeOfConstant(Constant, env.Model);
            else
                return "";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Constant;
        }

        public static string ConstantAsString(object constant)
        {
            if(constant == null)
                return "null";
            else if(constant is string)
                return "\"" + constant.ToString() + "\"";
            else
                return constant.ToString();
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return ConstantAsString(Constant); }
        }
    }
    
    public class SequenceExpressionVariable : SequenceExpression
    {
        public readonly SequenceVariable Variable;

        public SequenceExpressionVariable(SequenceVariable var)
            : base(SequenceExpressionType.Variable)
        {
            Variable = var;
        }

        protected SequenceExpressionVariable(SequenceExpressionVariable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Variable = that.Variable.Copy(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionVariable(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return Variable.Type;
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Variable.GetVariableValue(procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Variable.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Variable.Name; }
        }
    }

    public class SequenceExpressionNew : SequenceExpressionConstructor
    {
        public readonly String ConstructedType;
        public readonly List<KeyValuePair<String, SequenceExpression>> AttributeInitializationList;

        public SequenceExpressionNew(String type)
            : base(SequenceExpressionType.New)
        {
            ConstructedType = type;
        }

        public SequenceExpressionNew(String type, List<KeyValuePair<String, SequenceExpression>> attributeInitializationList)
            : base(SequenceExpressionType.New)
        {
            ConstructedType = type;
            AttributeInitializationList = attributeInitializationList;
        }

        protected SequenceExpressionNew(SequenceExpressionNew that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ConstructedType = that.ConstructedType;
            if(that.AttributeInitializationList != null)
            {
                AttributeInitializationList = new List<KeyValuePair<string, SequenceExpression>>();
                foreach(KeyValuePair<string, SequenceExpression> attribute in that.AttributeInitializationList)
                {
                    AttributeInitializationList.Add(new KeyValuePair<string, SequenceExpression>(attribute.Key, attribute.Value.CopyExpression(originalToCopy, procEnv)));
                }
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNew(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            CheckBaseObjectTypeIsKnown(env, ConstructedType, ", constructor type/argument");
            if(AttributeInitializationList != null)
            {
                foreach(KeyValuePair<String, SequenceExpression> attributeInitialization in AttributeInitializationList)
                {
                    String attributeType = env.TypeOfMemberOrAttribute(ConstructedType, attributeInitialization.Key);
                    String valueType = attributeInitialization.Value.Type(env);
                    if(!TypesHelper.IsSameOrSubtype(valueType, attributeType, env.Model))
                        throw new SequenceParserExceptionTypeMismatch(Symbol, attributeType, valueType);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ConstructedType;
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            BaseObjectType objectType = procEnv.Graph.Model.ObjectModel.GetType(ConstructedType);
            if(objectType == null)
                objectType = procEnv.Graph.Model.TransientObjectModel.GetType(ConstructedType);
            IBaseObject obj;
            if(objectType is ObjectType)
                obj = ((ObjectType)objectType).CreateObject(procEnv.Graph, procEnv.Graph.GlobalVariables.FetchObjectUniqueId());
            else
                obj = ((TransientObjectType)objectType).CreateTransientObject();
            if(AttributeInitializationList != null)
            {
                foreach(KeyValuePair<string, SequenceExpression> attributeInitialization in AttributeInitializationList)
                {
                    obj.SetAttribute(attributeInitialization.Key, attributeInitialization.Value.Evaluate(procEnv));
                }
            }
            return obj;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            constructors.Add(this);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(AttributeInitializationList != null)
                {
                    foreach(KeyValuePair<String, SequenceExpression> attributeInitialization in AttributeInitializationList)
                    {
                        yield return attributeInitialization.Value;
                    }
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return ConstructedType; }
        }
    }

    public class SequenceExpressionThis : SequenceExpression
    {
        public readonly string RuleOfMatchThis;
        public readonly string TypeOfGraphElementThis;

        public SequenceExpressionThis(string ruleOfMatchThis, string typeOfGraphElementThis)
            : base(SequenceExpressionType.This)
        {
            RuleOfMatchThis = ruleOfMatchThis;
            TypeOfGraphElementThis = typeOfGraphElementThis;
        }

        protected SequenceExpressionThis(SequenceExpressionThis that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            RuleOfMatchThis = that.RuleOfMatchThis;
            TypeOfGraphElementThis = that.TypeOfGraphElementThis;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionThis(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(RuleOfMatchThis != null)
                return "match<" + RuleOfMatchThis + ">";
            else if(TypeOfGraphElementThis != null)
                return TypeOfGraphElementThis;
            else
                return "graph";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(RuleOfMatchThis != null)
                return (IMatch)procEnv.GetVariableValue("this"); // global variable "this" is filled at execution begin
            else if(TypeOfGraphElementThis != null)
                return (IGraphElement)procEnv.GetVariableValue("this"); // global variable "this" is filled at execution begin
            else
                return procEnv.Graph;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "this"; }
        }
    }

    public class SequenceExpressionMatchClassConstructor : SequenceExpression
    {
        public readonly String ConstructedType;

        public SequenceExpressionMatchClassConstructor(String constructedType)
            : base(SequenceExpressionType.MatchClassConstructor)
        {
            ConstructedType = constructedType;
        }

        protected SequenceExpressionMatchClassConstructor(SequenceExpressionMatchClassConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ConstructedType = that.ConstructedType;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMatchClassConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "match<class " + ConstructedType + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = TypesHelper.GetType(ConstructedType, procEnv.Graph.Model);
            MatchClassFilterer mcf = procEnv.Actions.GetMatchClass(ConstructedType);
            return mcf.info.Create();
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                return "match<class " + ConstructedType + ">()";
            }
        }
    }

    public abstract class SequenceExpressionConstructor : SequenceExpression
    {
        protected SequenceExpressionConstructor(SequenceExpressionType seqExprType)
            : base(seqExprType)
        {
        }

        protected SequenceExpressionConstructor(SequenceExpressionConstructor that)
           : base(that)
        {
        }
    }

    public abstract class SequenceExpressionContainerConstructor : SequenceExpressionConstructor
    {
        public readonly String ValueType;
        public readonly SequenceExpression[] ContainerItems;

        protected SequenceExpressionContainerConstructor(SequenceExpressionType seqExprType, String valueType, SequenceExpression[] containerItems)
            : base(seqExprType)
        {
            ValueType = valueType;
            ContainerItems = containerItems;
        }

        protected SequenceExpressionContainerConstructor(SequenceExpressionContainerConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ValueType = that.ValueType;
            ContainerItems = new SequenceExpression[that.ContainerItems.Length];
            for(int i = 0; i < that.ContainerItems.Length; ++i)
            {
                ContainerItems[i] = that.ContainerItems[i].CopyExpression(originalToCopy, procEnv);
            }
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            foreach(SequenceExpression containerItem in ContainerItems)
            {
                if(!TypesHelper.IsSameOrSubtype(containerItem.Type(env), ValueType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, ValueType, containerItem.Type(env));
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            foreach(SequenceExpression containerItem in ContainerItems)
            {
                containerItem.GetLocalVariables(variables, constructors);
            }
            constructors.Add(this);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                foreach(SequenceExpression containerItem in ContainerItems)
                {
                    yield return containerItem;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string ItemsString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < ContainerItems.Length; ++i)
                {
                    sb.Append(ContainerItems[i].Symbol);
                    if(i != ContainerItems.Length - 1)
                        sb.Append(",");
                }
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionSetConstructor : SequenceExpressionContainerConstructor
    {
        public SequenceExpressionSetConstructor(String valueType, SequenceExpression[] setItems)
            : base(SequenceExpressionType.SetConstructor, valueType, setItems)
        {
        }

        protected SequenceExpressionSetConstructor(SequenceExpressionSetConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionSetConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "set<" + ValueType + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type srcType = TypesHelper.GetType(ValueType, procEnv.Graph.Model);
            Type dstType = typeof(SetValueType);
            IDictionary set = ContainerHelper.NewDictionary(srcType, dstType);

            foreach(SequenceExpression setItem in ContainerItems)
            {
                set.Add(setItem.Evaluate(procEnv), null);
            }

            return set;
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("set<");
                sb.Append(ValueType);
                sb.Append(">{");
                sb.Append(ItemsString);
                sb.Append("}");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionMapConstructor : SequenceExpressionContainerConstructor
    {
        public readonly String KeyType;
        public readonly SequenceExpression[] MapKeyItems;

        public SequenceExpressionMapConstructor(String keyType, String valueType,
            SequenceExpression[] mapKeyItems, SequenceExpression[] mapValueItems)
            : base(SequenceExpressionType.MapConstructor, valueType, mapValueItems)
        {
            KeyType = keyType;
            MapKeyItems = mapKeyItems;
        }

        protected SequenceExpressionMapConstructor(SequenceExpressionMapConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            KeyType = that.KeyType;
            MapKeyItems = new SequenceExpression[that.MapKeyItems.Length];
            for(int i = 0; i < that.MapKeyItems.Length; ++i)
            {
                MapKeyItems[i] = that.MapKeyItems[i].CopyExpression(originalToCopy, procEnv);
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMapConstructor(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            foreach(SequenceExpression keyItem in MapKeyItems)
            {
                if(!TypesHelper.IsSameOrSubtype(keyItem.Type(env), KeyType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, KeyType, keyItem.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "map<" + KeyType + "," + ValueType + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type keyType = TypesHelper.GetType(KeyType, procEnv.Graph.Model);
            Type valueType = TypesHelper.GetType(ValueType, procEnv.Graph.Model);
            IDictionary map = ContainerHelper.NewDictionary(keyType, valueType);

            for(int i = 0; i < MapKeyItems.Length; ++i)
            {
                SequenceExpression mapKeyItem = MapKeyItems[i];
                SequenceExpression mapValueItem = ContainerItems[i];
                map.Add(mapKeyItem.Evaluate(procEnv), mapValueItem.Evaluate(procEnv));
            }

            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            base.GetLocalVariables(variables, constructors);

            foreach(SequenceExpression mapKeyItem in MapKeyItems)
            {
                mapKeyItem.GetLocalVariables(variables, constructors);
            }
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        { 
            get
            {
                foreach(SequenceExpression mapKeyItem in MapKeyItems)
                {
                    yield return mapKeyItem;
                }
                foreach(SequenceExpression mapValueItem in ContainerItems)
                {
                    yield return mapValueItem;
                }
            }
        }
        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("map<");
                sb.Append(KeyType);
                sb.Append(",");
                sb.Append(ValueType);
                sb.Append(">{");
                for(int i = 0; i < MapKeyItems.Length; ++i)
                {
                    sb.Append(MapKeyItems[i].Symbol);
                    sb.Append("->");
                    sb.Append(ContainerItems[i].Symbol);
                    if(i != MapKeyItems.Length - 1)
                        sb.Append(",");
                }
                sb.Append("}");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionArrayConstructor : SequenceExpressionContainerConstructor
    {
        public SequenceExpressionArrayConstructor(String valueType, SequenceExpression[] arrayItems)
            : base(SequenceExpressionType.ArrayConstructor, valueType, arrayItems)
        {
        }

        protected SequenceExpressionArrayConstructor(SequenceExpressionArrayConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<" + ValueType + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = TypesHelper.GetType(ValueType, procEnv);
            IList array = ContainerHelper.NewList(valueType);

            foreach(SequenceExpression arrayItem in ContainerItems)
            {
                array.Add(arrayItem.Evaluate(procEnv));
            }

            return array;
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("array<");
                sb.Append(ValueType);
                sb.Append(">[");
                sb.Append(ItemsString);
                sb.Append("]");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionDequeConstructor : SequenceExpressionContainerConstructor
    {
        public SequenceExpressionDequeConstructor(String valueType, SequenceExpression[] dequeItems)
            : base(SequenceExpressionType.DequeConstructor, valueType, dequeItems)
        {
        }

        protected SequenceExpressionDequeConstructor(SequenceExpressionDequeConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionDequeConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "deque<" + ValueType + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = TypesHelper.GetType(ValueType, procEnv);
            IDeque deque = ContainerHelper.NewDeque(valueType);

            foreach(SequenceExpression dequeItem in ContainerItems)
            {
                deque.Enqueue(dequeItem.Evaluate(procEnv));
            }

            return deque;
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("deque<");
                sb.Append(ValueType);
                sb.Append(">]");
                sb.Append(ItemsString);
                sb.Append("[");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionSetCopyConstructor : SequenceExpression
    {
        public readonly String ValueType;
        public readonly SequenceExpression SetToCopy;

        public SequenceExpressionSetCopyConstructor(String valueType, SequenceExpression setToCopy)
            : base(SequenceExpressionType.SetCopyConstructor)
        {
            ValueType = valueType;
            SetToCopy = setToCopy;
        }

        protected SequenceExpressionSetCopyConstructor(SequenceExpressionSetCopyConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ValueType = that.ValueType;
            SetToCopy = that.SetToCopy.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionSetCopyConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "set<" + ValueType + ">";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SetToCopy.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!SetToCopy.Type(env).StartsWith("set<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", argument", "set type", SetToCopy.Type(env));

                // TODO: check ValueType with compatibility with value type of SetToCopy
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type srcType = TypesHelper.GetType(ValueType, procEnv);
            Type dstType = typeof(SetValueType);
            IDictionary set = ContainerHelper.NewDictionary(srcType, dstType);
            ContainerHelper.FillSet(set, ValueType, SetToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return set;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            base.GetLocalVariables(variables, constructors);
            SetToCopy.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return SetToCopy; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("set<");
                sb.Append(ValueType);
                sb.Append(">(");
                sb.Append(SetToCopy.Symbol);
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionMapCopyConstructor : SequenceExpression
    {
        public readonly String KeyType;
        public readonly String ValueType;
        public readonly SequenceExpression MapToCopy;

        public SequenceExpressionMapCopyConstructor(String keyType, String valueType, SequenceExpression mapToCopy)
            : base(SequenceExpressionType.MapCopyConstructor)
        {
            KeyType = keyType;
            ValueType = valueType;
            MapToCopy = mapToCopy;
        }

        protected SequenceExpressionMapCopyConstructor(SequenceExpressionMapCopyConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            KeyType = that.KeyType;
            ValueType = that.ValueType;
            MapToCopy = that.MapToCopy.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMapCopyConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "map<" + KeyType + "," + ValueType + ">";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(MapToCopy.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!MapToCopy.Type(env).StartsWith("map<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", argument", "map type", MapToCopy.Type(env));

                // TODO: check KeyType/ValueType with compatibility with key type/value type of MapToCopy
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type srcType = TypesHelper.GetType(KeyType, procEnv);
            Type dstType = TypesHelper.GetType(ValueType, procEnv);
            IDictionary map = ContainerHelper.NewDictionary(srcType, dstType);
            ContainerHelper.FillMap(map, KeyType, ValueType, MapToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            base.GetLocalVariables(variables, constructors);
            MapToCopy.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return MapToCopy; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("map<");
                sb.Append(KeyType);
                sb.Append(",");
                sb.Append(ValueType);
                sb.Append(">(");
                sb.Append(MapToCopy.Symbol);
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionArrayCopyConstructor : SequenceExpression
    {
        public readonly String ValueType;
        public readonly SequenceExpression ArrayToCopy;

        public SequenceExpressionArrayCopyConstructor(String valueType, SequenceExpression arrayToCopy)
            : base(SequenceExpressionType.ArrayCopyConstructor)
        {
            ValueType = valueType;
            ArrayToCopy = arrayToCopy;
        }

        protected SequenceExpressionArrayCopyConstructor(SequenceExpressionArrayCopyConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ValueType = that.ValueType;
            ArrayToCopy = that.ArrayToCopy.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayCopyConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<" + ValueType + ">";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(ArrayToCopy.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!ArrayToCopy.Type(env).StartsWith("array<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", argument", "array type", ArrayToCopy.Type(env));

                // TODO: check ValueType with compatibility with value type of ArrayToCopy
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = TypesHelper.GetType(ValueType, procEnv.Graph.Model);
            IList array = ContainerHelper.NewList(valueType);
            ContainerHelper.FillArray(array, ValueType, ArrayToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return array;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            base.GetLocalVariables(variables, constructors);
            ArrayToCopy.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return ArrayToCopy; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("array<");
                sb.Append(ValueType);
                sb.Append(">[");
                sb.Append(ArrayToCopy.Symbol);
                sb.Append("]");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionDequeCopyConstructor : SequenceExpression
    {
        public readonly String ValueType;
        public readonly SequenceExpression DequeToCopy;

        public SequenceExpressionDequeCopyConstructor(String valueType, SequenceExpression dequeToCopy)
            : base(SequenceExpressionType.DequeCopyConstructor)
        {
            ValueType = valueType;
            DequeToCopy = dequeToCopy;
        }

        protected SequenceExpressionDequeCopyConstructor(SequenceExpressionDequeCopyConstructor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ValueType = that.ValueType;
            DequeToCopy = that.DequeToCopy.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionDequeCopyConstructor(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "deque<" + ValueType + ">";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(DequeToCopy.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!DequeToCopy.Type(env).StartsWith("deque<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", argument", "deque type", DequeToCopy.Type(env));

                // TODO: check ValueType with compatibility with value type of DequeToCopy
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = TypesHelper.GetType(ValueType, procEnv.Graph.Model);
            IDeque deque = ContainerHelper.NewDeque(valueType);
            ContainerHelper.FillDeque(deque, ValueType, DequeToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return deque;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            base.GetLocalVariables(variables, constructors);
            DequeToCopy.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return DequeToCopy; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("deque<");
                sb.Append(ValueType);
                sb.Append(">[");
                sb.Append(DequeToCopy.Symbol);
                sb.Append("]");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionContainerAsArray : SequenceExpressionContainer
    {
        public SequenceExpressionContainerAsArray(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ContainerAsArray, containerExpr)
        {
        }

        protected SequenceExpressionContainerAsArray(SequenceExpressionContainerAsArray that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionContainerAsArray(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "array<" + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.AsArray(ContainerValue(procEnv), procEnv.Graph.Model);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".asArray()"; }
        }
    }

    public class SequenceExpressionStringLength : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;

        public SequenceExpressionStringLength(SequenceExpression stringExpr)
            : base(SequenceExpressionType.StringLength)
        {
            StringExpr = stringExpr;
        }

        protected SequenceExpressionStringLength(SequenceExpressionStringLength that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringLength(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((string)StringExpr.Evaluate(procEnv)).Length;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".length()"; }
        }
    }

    public class SequenceExpressionStringStartsWith : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;
        public readonly SequenceExpression StringToSearchForExpr;

        public SequenceExpressionStringStartsWith(SequenceExpression stringExpr, SequenceExpression stringToSearchForExpr)
            : base(SequenceExpressionType.StringStartsWith)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
        }

        protected SequenceExpressionStringStartsWith(SequenceExpressionStringStartsWith that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
            StringToSearchForExpr = that.StringToSearchForExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringStartsWith(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));

            if(StringToSearchForExpr.Type(env) == "")
                return;

            if(StringToSearchForExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((string)StringExpr.Evaluate(procEnv)).StartsWith((string)StringToSearchForExpr.Evaluate(procEnv), StringComparison.InvariantCulture);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
            StringToSearchForExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
                yield return StringToSearchForExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".startsWith(" + StringToSearchForExpr.Symbol + ")"; }
        }
    }

    public class SequenceExpressionStringEndsWith : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;
        public readonly SequenceExpression StringToSearchForExpr;

        public SequenceExpressionStringEndsWith(SequenceExpression stringExpr, SequenceExpression stringToSearchForExpr)
            : base(SequenceExpressionType.StringEndsWith)
        {
            StringExpr = stringExpr;
            StringToSearchForExpr = stringToSearchForExpr;
        }

        protected SequenceExpressionStringEndsWith(SequenceExpressionStringEndsWith that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
            StringToSearchForExpr = that.StringToSearchForExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringEndsWith(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));

            if(StringToSearchForExpr.Type(env) == "")
                return;

            if(StringToSearchForExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((string)StringExpr.Evaluate(procEnv)).EndsWith((string)StringToSearchForExpr.Evaluate(procEnv), StringComparison.InvariantCulture);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
            StringToSearchForExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
                yield return StringToSearchForExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".endsWith(" + StringToSearchForExpr.Symbol + ")"; }
        }
    }

    public class SequenceExpressionStringSubstring : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;
        public readonly SequenceExpression StartIndexExpr;
        public readonly SequenceExpression LengthExpr;

        public SequenceExpressionStringSubstring(SequenceExpression stringExpr, SequenceExpression startIndexExpr, SequenceExpression lengthExpr)
            : base(SequenceExpressionType.StringSubstring)
        {
            StringExpr = stringExpr;
            StartIndexExpr = startIndexExpr;
            LengthExpr = lengthExpr;
        }

        public SequenceExpressionStringSubstring(SequenceExpression stringExpr, SequenceExpression startIndexExpr)
            : base(SequenceExpressionType.StringSubstring)
        {
            StringExpr = stringExpr;
            StartIndexExpr = startIndexExpr;
        }

        protected SequenceExpressionStringSubstring(SequenceExpressionStringSubstring that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
            StartIndexExpr = that.StartIndexExpr.CopyExpression(originalToCopy, procEnv);
            if(LengthExpr != null)
                LengthExpr = that.LengthExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringSubstring(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));

            if(StartIndexExpr.Type(env) != "" && StartIndexExpr.Type(env) != "int")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int type", Type(env));

            if(LengthExpr != null)
            {
                if(LengthExpr.Type(env) != "" && LengthExpr.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int type", Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(LengthExpr != null)
                return ((string)StringExpr.Evaluate(procEnv)).Substring((int)StartIndexExpr.Evaluate(procEnv), (int)LengthExpr.Evaluate(procEnv));
            else
                return ((string)StringExpr.Evaluate(procEnv)).Substring((int)StartIndexExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
            StartIndexExpr.GetLocalVariables(variables, constructors);
            if(LengthExpr != null)
                LengthExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
                yield return StartIndexExpr;
                if(LengthExpr != null)
                    yield return LengthExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".substring(" + StartIndexExpr.Symbol + (LengthExpr != null ? "," + LengthExpr.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionStringReplace : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;
        public readonly SequenceExpression StartIndexExpr;
        public readonly SequenceExpression LengthExpr;
        public readonly SequenceExpression ReplaceStringExpr;

        public SequenceExpressionStringReplace(SequenceExpression stringExpr, SequenceExpression startIndexExpr,
            SequenceExpression lengthExpr, SequenceExpression replaceStringExpr)
            : base(SequenceExpressionType.StringReplace)
        {
            StringExpr = stringExpr;
            StartIndexExpr = startIndexExpr;
            LengthExpr = lengthExpr;
            ReplaceStringExpr = replaceStringExpr;
        }

        protected SequenceExpressionStringReplace(SequenceExpressionStringReplace that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
            StartIndexExpr = that.StartIndexExpr.CopyExpression(originalToCopy, procEnv);
            LengthExpr = that.LengthExpr.CopyExpression(originalToCopy, procEnv);
            ReplaceStringExpr = that.ReplaceStringExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringReplace(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));

            if(StartIndexExpr.Type(env) != "" && StartIndexExpr.Type(env) != "int")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int type", Type(env));

            if(LengthExpr.Type(env) != "" && LengthExpr.Type(env) != "int")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int type", Type(env));

            if(ReplaceStringExpr.Type(env) != "" && ReplaceStringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            string target = (string)StringExpr.Evaluate(procEnv);
            int startIndex = (int)StartIndexExpr.Evaluate(procEnv);
            int length = (int)LengthExpr.Evaluate(procEnv);
            string replacement = (string)ReplaceStringExpr.Evaluate(procEnv);
            return target.Substring(0, startIndex) + replacement + target.Substring(startIndex + length);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
            StartIndexExpr.GetLocalVariables(variables, constructors);
            LengthExpr.GetLocalVariables(variables, constructors);
            ReplaceStringExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
                yield return StartIndexExpr;
                yield return LengthExpr;
                yield return ReplaceStringExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".replace(" + StartIndexExpr.Symbol + "," + LengthExpr.Symbol + "," + ReplaceStringExpr.Symbol + ")"; }
        }
    }

    public class SequenceExpressionStringToLower : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;

        public SequenceExpressionStringToLower(SequenceExpression stringExpr)
            : base(SequenceExpressionType.StringToLower)
        {
            StringExpr = stringExpr;
        }

        protected SequenceExpressionStringToLower(SequenceExpressionStringToLower that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringToLower(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((string)StringExpr.Evaluate(procEnv)).ToLowerInvariant();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".toLower()"; }
        }
    }

    public class SequenceExpressionStringToUpper : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;

        public SequenceExpressionStringToUpper(SequenceExpression stringExpr)
            : base(SequenceExpressionType.StringToUpper)
        {
            StringExpr = stringExpr;
        }

        protected SequenceExpressionStringToUpper(SequenceExpressionStringToUpper that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringToUpper(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((string)StringExpr.Evaluate(procEnv)).ToUpperInvariant();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".toUpper()"; }
        }
    }

    public class SequenceExpressionStringAsArray : SequenceExpression
    {
        public readonly SequenceExpression StringExpr;
        public readonly SequenceExpression SeparatorExpr;

        public SequenceExpressionStringAsArray(SequenceExpression stringExpr, SequenceExpression separatorExpr)
            : base(SequenceExpressionType.StringAsArray)
        {
            StringExpr = stringExpr;
            SeparatorExpr = separatorExpr;
        }

        protected SequenceExpressionStringAsArray(SequenceExpressionStringAsArray that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
            SeparatorExpr = that.SeparatorExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionStringAsArray(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) == "")
                return; // we can't check type if the variable is untyped, only runtime-check possible

            if(StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));

            if(SeparatorExpr.Type(env) == "")
                return;

            if(SeparatorExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<string>";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.StringAsArray((string)StringExpr.Evaluate(procEnv), (string)SeparatorExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
            SeparatorExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
                if(SeparatorExpr != null)
                    yield return SeparatorExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return StringExpr.Symbol + ".asArray(" + SeparatorExpr.Symbol + ")"; }
        }
    }

    public class SequenceExpressionRandom : SequenceExpression
    {
        public readonly SequenceExpression UpperBound;

        public SequenceExpressionRandom(SequenceExpression upperBound)
            : base(SequenceExpressionType.Random)
        {
            UpperBound = upperBound; // might be null
        }

        protected SequenceExpressionRandom(SequenceExpressionRandom that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            if(that.UpperBound != null)
                UpperBound = that.UpperBound.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionRandom(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(UpperBound != null)
                return "int";
            else
                return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(UpperBound != null)
            {
                base.Check(env); // check children

                if(!TypesHelper.IsSameOrSubtype(UpperBound.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", UpperBound.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(UpperBound != null)
                return Sequence.randomGenerator.Next((int)UpperBound.Evaluate(procEnv));
            else
                return Sequence.randomGenerator.NextDouble();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(UpperBound != null) 
                UpperBound.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(UpperBound != null)
                    yield return UpperBound;
                else
                    yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "random(" + (UpperBound!=null? UpperBound.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionDef : SequenceExpression
    {
        public readonly SequenceExpression[] DefVars;

        public SequenceExpressionDef(SequenceExpression[] defVars)
            : base(SequenceExpressionType.Def)
        {
            DefVars = defVars;
        }

        protected SequenceExpressionDef(SequenceExpressionDef that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            DefVars = new SequenceExpression[that.DefVars.Length];
            for(int i = 0; i < that.DefVars.Length; ++i)
            {
                DefVars[i] = that.DefVars[i].CopyExpression(originalToCopy, procEnv);
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionDef(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            foreach(SequenceExpression defVar in DefVars)
            {
                if(!(defVar is SequenceExpressionVariable))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "variable", "not a variable");
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            foreach(SequenceExpression defVar in DefVars)
            {
                if(defVar.Evaluate(procEnv) == null)
                    return false;
            }
            return true;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            foreach(SequenceExpression defVar in DefVars)
            {
                defVar.GetLocalVariables(variables, constructors);
            }
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                foreach(SequenceExpression defVar in DefVars)
                {
                    yield return defVar;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("def(");
                for(int i = 0; i < DefVars.Length; ++i)
                {
                    sb.Append(DefVars[i].Symbol);
                    if(i != DefVars.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionIsVisited : SequenceExpression
    {
        public readonly SequenceExpression GraphElementVarExpr;
        public readonly SequenceExpression VisitedFlagExpr;

        public SequenceExpressionIsVisited(SequenceExpression graphElementVarExpr, SequenceExpression visitedFlagExpr)
            : base(SequenceExpressionType.IsVisited)
        {
            GraphElementVarExpr = graphElementVarExpr;
            VisitedFlagExpr = visitedFlagExpr;
        }

        protected SequenceExpressionIsVisited(SequenceExpressionIsVisited that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            GraphElementVarExpr = that.GraphElementVarExpr.CopyExpression(originalToCopy, procEnv);
            if(VisitedFlagExpr != null)
                VisitedFlagExpr = that.VisitedFlagExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionIsVisited(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(GraphElementVarExpr.Type(env) != "" && TypesHelper.GetGraphElementType(GraphElementVarExpr.Type(env), env.Model) == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge type", GraphElementVarExpr.Type(env));
            if(VisitedFlagExpr != null)
            {
                if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpr.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", VisitedFlagExpr.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)GraphElementVarExpr.Evaluate(procEnv);
            int visitedFlag = VisitedFlagExpr != null ? (int)VisitedFlagExpr.Evaluate(procEnv) : 0;
            return procEnv.Graph.IsVisited(elem, visitedFlag);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            GraphElementVarExpr.GetLocalVariables(variables, constructors);
            if(VisitedFlagExpr != null)
                VisitedFlagExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return GraphElementVarExpr;
                if(VisitedFlagExpr != null) yield return VisitedFlagExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return GraphElementVarExpr.Symbol + ".visited" + (VisitedFlagExpr != null ? ("[" + VisitedFlagExpr.Symbol + "]") : ""); }
        }
    }

    public class SequenceExpressionInContainerOrString : SequenceExpression
    {
        public readonly SequenceExpression Expr;
        public readonly SequenceExpression ContainerOrStringExpr;

        public SequenceExpressionInContainerOrString(SequenceExpression expr, SequenceExpression containerOrString)
            : base(SequenceExpressionType.InContainerOrString)
        {
            Expr = expr;
            ContainerOrStringExpr = containerOrString;
        }

        protected SequenceExpressionInContainerOrString(SequenceExpressionInContainerOrString that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
            ContainerOrStringExpr = that.ContainerOrStringExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionInContainerOrString(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = ContainerOrStringExpr.Type(env);
            if(ContainerType == "")
                return; // we can't check container type if the variable is untyped, only runtime-check possible

            if(ContainerType != "string" && (TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);

            if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
        }

        public object ContainerOrStringValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerOrStringExpr is SequenceExpressionAttributeAccess)
                return ((SequenceExpressionAttributeAccess)ContainerOrStringExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return ContainerOrStringExpr.Evaluate(procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object containerOrString = ContainerOrStringValue(procEnv);
            
            if(containerOrString is string)
            {
                String str = (string)containerOrString;
                return str.Contains((string)Expr.Evaluate(procEnv));
            }
            if(containerOrString is IList)
            {
                IList array = (IList)containerOrString;
                return array.Contains(Expr.Evaluate(procEnv));
            }
            else if(containerOrString is IDeque)
            {
                IDeque deque = (IDeque)containerOrString;
                return deque.Contains(Expr.Evaluate(procEnv));
            }
            else
            {
                IDictionary setmap = (IDictionary)containerOrString;
                return setmap.Contains(Expr.Evaluate(procEnv));
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerOrStringExpr.GetLocalVariables(variables, constructors);
            Expr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Expr;
                yield return ContainerOrStringExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Expr.Symbol + " in " + ContainerOrStringExpr.Symbol; }
        }
    }

    public class SequenceExpressionContainerSize : SequenceExpressionContainer
    {
        public SequenceExpressionContainerSize(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ContainerSize, containerExpr)
        {
        }

        protected SequenceExpressionContainerSize(SequenceExpressionContainerSize that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionContainerSize(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            CheckAndReturnContainerType(env);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object container = ContainerValue(procEnv);
            if(container is IList)
            {
                IList array = (IList)container;
                return array.Count;
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                return deque.Count;
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap.Count;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return ContainerExpr; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".size()"; }
        }
    }

    public class SequenceExpressionContainerEmpty : SequenceExpressionContainer
    {
        public SequenceExpressionContainerEmpty(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ContainerEmpty, containerExpr)
        {
        }

        protected SequenceExpressionContainerEmpty(SequenceExpressionContainerEmpty that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionContainerEmpty(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            CheckAndReturnContainerType(env);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object container = ContainerValue(procEnv);
            if(container is IList)
            {
                IList array = (IList)container;
                return array.Count == 0;
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                return deque.Count == 0;
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap.Count == 0;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return ContainerExpr; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".empty()"; }
        }
    }

    public class SequenceExpressionContainerAccess : SequenceExpression
    {
        public readonly SequenceExpression ContainerExpr;
        public readonly SequenceExpression KeyExpr;

        public SequenceExpressionContainerAccess(SequenceExpression containerExpr, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerAccess)
        {
            ContainerExpr = containerExpr;
            KeyExpr = keyExpr;
        }

        protected SequenceExpressionContainerAccess(SequenceExpressionContainerAccess that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ContainerExpr = that.ContainerExpr.CopyExpression(originalToCopy, procEnv);
            KeyExpr = that.KeyExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionContainerAccess(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children -- todo: rethink (add children or remove)

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check source and destination types if the container is untyped, only runtime-check possible

            if(TypesHelper.ExtractDst(ContainerType) == "SetValueType")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "map<S,T> or array<S> or deque<S>", ContainerType);

            KeyExpr.Check(env);

            if(ContainerType.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", KeyExpr.Type(env));
            }
            else if(ContainerType.StartsWith("deque"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", KeyExpr.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType), KeyExpr.Type(env));
            }
        }

        public string CheckAndReturnContainerType(SequenceCheckingEnvironment env)
        {
            string ContainerType = ContainerExpr.Type(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string ContainerType;
            ContainerType = ContainerExpr.Type(env);
            if(ContainerType == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            if(ContainerType.StartsWith("array") || ContainerType.StartsWith("deque"))
                return TypesHelper.ExtractSrc(ContainerType);
            else
                return TypesHelper.ExtractDst(ContainerType);
        }

        public object ContainerValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerExpr is SequenceExpressionAttributeAccess)
                return ((SequenceExpressionAttributeAccess)ContainerExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return ContainerExpr.Evaluate(procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object container = ContainerValue(procEnv);

            if(container is IList)
            {
                IList array = (IList)container;
                int key = (int)KeyExpr.Evaluate(procEnv);
                return array[key];
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                int key = (int)KeyExpr.Evaluate(procEnv);
                return deque[key];
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                object key = KeyExpr.Evaluate(procEnv);
                return setmap[key];
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            KeyExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return KeyExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return ContainerExpr.Symbol + "[" + KeyExpr.Symbol + "]"; }
        }
    }

    public class SequenceExpressionContainerPeek : SequenceExpressionContainer
    {
        public readonly SequenceExpression KeyExpr;

        public SequenceExpressionContainerPeek(SequenceExpression containerExpr, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerPeek, containerExpr)
        {
            KeyExpr = keyExpr;
        }

        protected SequenceExpressionContainerPeek(SequenceExpressionContainerPeek that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            if(that.KeyExpr != null)
                KeyExpr = that.KeyExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionContainerPeek(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(KeyExpr != null)
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", KeyExpr.Type(env));
            }
            else
            {
                if(containerType.StartsWith("set<") || containerType.StartsWith("map<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<S> or deque<S> type", containerType);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return TypesHelper.ExtractSrc(ContainerType(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(KeyExpr != null)
                return ContainerHelper.Peek(ContainerValue(procEnv), (int)KeyExpr.Evaluate(procEnv)); 
            else
                return ContainerHelper.Peek(ContainerValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            if(KeyExpr != null)
                KeyExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(KeyExpr != null)
                    yield return KeyExpr;
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".peek(" + (KeyExpr != null ? KeyExpr.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionArrayOrDequeOrStringIndexOf : SequenceExpressionContainer
    {
        public readonly SequenceExpression ValueToSearchForExpr;
        public readonly SequenceExpression StartPositionExpr;

        public SequenceExpressionArrayOrDequeOrStringIndexOf(SequenceExpression containerExpr, SequenceExpression valueToSearchForExpr, SequenceExpression startPositionExpr)
            : base(SequenceExpressionType.ArrayOrDequeOrStringIndexOf, containerExpr)
        {
            ValueToSearchForExpr = valueToSearchForExpr;
            StartPositionExpr = startPositionExpr;
        }

        protected SequenceExpressionArrayOrDequeOrStringIndexOf(SequenceExpressionArrayOrDequeOrStringIndexOf that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrDequeOrStringIndexOf(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = ContainerExpr.Type(env);
            if(containerType == "")
                return; // we can't type check if the variable is untyped, only runtime-check possible

            if(!containerType.StartsWith("array<") && !containerType.StartsWith("deque<") && containerType != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> or deque<T> or string type", containerType);

            if(ValueToSearchForExpr.Type(env) != "")
            {
                if(containerType.StartsWith("array<") || containerType.StartsWith("deque<"))
                {
                    if(ValueToSearchForExpr.Type(env) != TypesHelper.ExtractSrc(ContainerType(env)))
                        throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType(env)), ValueToSearchForExpr.Type(env));
                }
                else
                {
                    if(ValueToSearchForExpr.Type(env) != "string")
                        throw new SequenceParserExceptionTypeMismatch(Symbol, "string", ValueToSearchForExpr.Type(env));
                }
            }

            if(StartPositionExpr != null && StartPositionExpr.Type(env) != "")
            {
                if(StartPositionExpr.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", ValueToSearchForExpr.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(StartPositionExpr != null)
                return ContainerHelper.IndexOf(ContainerValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv), (int)StartPositionExpr.Evaluate(procEnv));
            else
                return ContainerHelper.IndexOf(ContainerValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            ValueToSearchForExpr.GetLocalVariables(variables, constructors);
            if(StartPositionExpr != null)
                StartPositionExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return ValueToSearchForExpr;
                if(StartPositionExpr != null)
                    yield return StartPositionExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".indexOf(" + ValueToSearchForExpr.Symbol + (StartPositionExpr != null ? "," + StartPositionExpr.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionArrayOrDequeOrStringLastIndexOf : SequenceExpressionContainer
    {
        public readonly SequenceExpression ValueToSearchForExpr;
        public readonly SequenceExpression StartPositionExpr;

        public SequenceExpressionArrayOrDequeOrStringLastIndexOf(SequenceExpression containerExpr, SequenceExpression valueToSearchForExpr, SequenceExpression startPositionExpr)
            : base(SequenceExpressionType.ArrayOrDequeOrStringLastIndexOf, containerExpr)
        {
            ValueToSearchForExpr = valueToSearchForExpr;
            StartPositionExpr = startPositionExpr;
        }

        protected SequenceExpressionArrayOrDequeOrStringLastIndexOf(SequenceExpressionArrayOrDequeOrStringLastIndexOf that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrDequeOrStringLastIndexOf(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = ContainerExpr.Type(env);
            if(containerType == "")
                return; // we can't type check if the variable is untyped, only runtime-check possible

            if(!containerType.StartsWith("array<") && !containerType.StartsWith("deque<") && containerType != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> or deque<T> or string type", containerType);

            if(ValueToSearchForExpr.Type(env) != "")
            {
                if(containerType.StartsWith("array<") || containerType.StartsWith("deque<"))
                {
                    if(ValueToSearchForExpr.Type(env) != TypesHelper.ExtractSrc(ContainerType(env)))
                        throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType(env)), ValueToSearchForExpr.Type(env));
                }
                else
                {
                    if(ValueToSearchForExpr.Type(env) != "string")
                        throw new SequenceParserExceptionTypeMismatch(Symbol, "string", ValueToSearchForExpr.Type(env));
                }
            }

            if(StartPositionExpr != null && StartPositionExpr.Type(env) != "")
            {
                if(StartPositionExpr.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", ValueToSearchForExpr.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(StartPositionExpr != null)
                return ContainerHelper.LastIndexOf(ContainerValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv), (int)StartPositionExpr.Evaluate(procEnv));
            else
                return ContainerHelper.LastIndexOf(ContainerValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            ValueToSearchForExpr.GetLocalVariables(variables, constructors);
            if(StartPositionExpr != null)
                StartPositionExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return ValueToSearchForExpr;
                if(StartPositionExpr != null)
                    yield return StartPositionExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".lastIndexOf(" + ValueToSearchForExpr.Symbol + (StartPositionExpr != null ? "," + StartPositionExpr.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionArrayIndexOfOrdered : SequenceExpressionContainer
    {
        public readonly SequenceExpression ValueToSearchForExpr;

        public SequenceExpressionArrayIndexOfOrdered(SequenceExpression containerExpr, SequenceExpression valueToSearchForExpr)
            : base(SequenceExpressionType.ArrayIndexOfOrdered, containerExpr)
        {
            ValueToSearchForExpr = valueToSearchForExpr;
        }

        protected SequenceExpressionArrayIndexOfOrdered(SequenceExpressionArrayIndexOfOrdered that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayIndexOfOrdered(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType == "")
                return;

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(ValueToSearchForExpr.Type(env) != "")
            {
                if(ValueToSearchForExpr.Type(env) != TypesHelper.ExtractSrc(ContainerType(env)))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType(env)), ValueToSearchForExpr.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.IndexOfOrdered(ArrayValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            ValueToSearchForExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return ValueToSearchForExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".indexOfOrdered(" + ValueToSearchForExpr.Symbol + ")"; }
        }
    }

    public class SequenceExpressionArraySum : SequenceExpressionContainer
    {
        public SequenceExpressionArraySum(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArraySum, containerExpr)
        {
        }

        protected SequenceExpressionArraySum(SequenceExpressionArraySum that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArraySum(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Sum(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".sum()"; }
        }
    }

    public class SequenceExpressionArrayProd : SequenceExpressionContainer
    {
        public SequenceExpressionArrayProd(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayProd, containerExpr)
        {
        }

        protected SequenceExpressionArrayProd(SequenceExpressionArrayProd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayProd(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Prod(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".prod()"; }
        }
    }

    public class SequenceExpressionArrayMin : SequenceExpressionContainer
    {
        public SequenceExpressionArrayMin(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayMin, containerExpr)
        {
        }

        protected SequenceExpressionArrayMin(SequenceExpressionArrayMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayMin(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Min(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".min()"; }
        }
    }

    public class SequenceExpressionArrayMax : SequenceExpressionContainer
    {
        public SequenceExpressionArrayMax(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayMax, containerExpr)
        {
        }

        protected SequenceExpressionArrayMax(SequenceExpressionArrayMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayMax(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Max(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".max()"; }
        }
    }

    public class SequenceExpressionArrayAvg : SequenceExpressionContainer
    {
        public SequenceExpressionArrayAvg(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayAvg, containerExpr)
        {
        }

        protected SequenceExpressionArrayAvg(SequenceExpressionArrayAvg that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayAvg(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Avg(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".avg()"; }
        }
    }

    public class SequenceExpressionArrayMed : SequenceExpressionContainer
    {
        public SequenceExpressionArrayMed(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayMed, containerExpr)
        {
        }

        protected SequenceExpressionArrayMed(SequenceExpressionArrayMed that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayMed(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Med(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".med()"; }
        }
    }

    public class SequenceExpressionArrayMedUnordered : SequenceExpressionContainer
    {
        public SequenceExpressionArrayMedUnordered(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayMedUnordered, containerExpr)
        {
        }

        protected SequenceExpressionArrayMedUnordered(SequenceExpressionArrayMedUnordered that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayMedUnordered(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.MedUnordered(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".medUnordered()"; }
        }
    }

    public class SequenceExpressionArrayVar : SequenceExpressionContainer
    {
        public SequenceExpressionArrayVar(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayVar, containerExpr)
        {
        }

        protected SequenceExpressionArrayVar(SequenceExpressionArrayVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Var(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".var()"; }
        }
    }

    public class SequenceExpressionArrayDev : SequenceExpressionContainer
    {
        public SequenceExpressionArrayDev(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayDev, containerExpr)
        {
        }

        protected SequenceExpressionArrayDev(SequenceExpressionArrayDev that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayDev(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(!IsNumericType(TypesHelper.ExtractSrc(containerType)))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type where T=number", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Dev(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".dev()"; }
        }
    }

    public class SequenceExpressionArrayAnd : SequenceExpressionContainer
    {
        public SequenceExpressionArrayAnd(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayAnd, containerExpr)
        {
        }

        protected SequenceExpressionArrayAnd(SequenceExpressionArrayAnd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayAnd(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(TypesHelper.ExtractSrc(containerType) != "boolean")
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<boolean>", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.And(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".and()"; }
        }
    }

    public class SequenceExpressionArrayOr : SequenceExpressionContainer
    {
        public SequenceExpressionArrayOr(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayOr, containerExpr)
        {
        }

        protected SequenceExpressionArrayOr(SequenceExpressionArrayOr that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOr(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType.StartsWith("array<"))
            {
                if(TypesHelper.ExtractSrc(containerType) != "boolean")
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<boolean>", containerType);
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Or(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".or()"; }
        }
    }

    public class SequenceExpressionArrayOrDequeAsSet : SequenceExpressionContainer
    {
        public SequenceExpressionArrayOrDequeAsSet(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayOrDequeAsSet, containerExpr)
        {
        }

        protected SequenceExpressionArrayOrDequeAsSet(SequenceExpressionArrayOrDequeAsSet that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrDequeAsSet(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> or deque<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "set<" + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayOrDequeAsSet(ContainerValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".asSet()"; }
        }
    }

    public class SequenceExpressionMapDomain : SequenceExpressionContainer
    {
        public SequenceExpressionMapDomain(SequenceExpression containerExpr)
            : base(SequenceExpressionType.MapDomain, containerExpr)
        {
        }

        protected SequenceExpressionMapDomain(SequenceExpressionMapDomain that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMapDomain(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("array<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "map<S,T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "set<" + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Domain(MapValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".domain()"; }
        }
    }

    public class SequenceExpressionMapRange : SequenceExpressionContainer
    {
        public SequenceExpressionMapRange(SequenceExpression containerExpr)
            : base(SequenceExpressionType.MapRange, containerExpr)
        {
        }

        protected SequenceExpressionMapRange(SequenceExpressionMapRange that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMapRange(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("array<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "map<S,T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "set<" + TypesHelper.ExtractDst(ContainerType(env)) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Range(MapValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".range()"; }
        }
    }

    public class SequenceExpressionArrayAsMap : SequenceExpressionContainer
    {
        public SequenceExpressionArrayAsMap(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayAsMap, containerExpr)
        {
        }

        protected SequenceExpressionArrayAsMap(SequenceExpressionArrayAsMap that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayAsMap(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "map<int," + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayAsMap(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".asSet()"; }
        }
    }

    public class SequenceExpressionArrayAsDeque : SequenceExpressionContainer
    {
        public SequenceExpressionArrayAsDeque(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayAsDeque, containerExpr)
        {
        }

        protected SequenceExpressionArrayAsDeque(SequenceExpressionArrayAsDeque that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayAsDeque(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "deque<" + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayAsDeque(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".asDeque()"; }
        }
    }

    public class SequenceExpressionArrayAsString : SequenceExpressionContainer
    {
        public readonly SequenceExpression Separator;

        public SequenceExpressionArrayAsString(SequenceExpression containerExpr, SequenceExpression separator)
            : base(SequenceExpressionType.ArrayAsString, containerExpr)
        {
            Separator = separator;
        }

        protected SequenceExpressionArrayAsString(SequenceExpressionArrayAsString that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            Separator = that.Separator.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayAsString(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType != ("array<string>"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<string> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayAsString(ArrayValue(procEnv), (string)Separator.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return Separator;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".asString(" + Separator.Symbol + ")"; }
        }
    }

    public class SequenceExpressionArraySubarray : SequenceExpressionContainer
    {
        public readonly SequenceExpression Start;
        public readonly SequenceExpression Length;

        public SequenceExpressionArraySubarray(SequenceExpression containerExpr, SequenceExpression start, SequenceExpression length)
            : base(SequenceExpressionType.ArraySubarray, containerExpr)
        {
            Start = start;
            Length = length;
        }

        protected SequenceExpressionArraySubarray(SequenceExpressionArraySubarray that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            Start = that.Start.CopyExpression(originalToCopy, procEnv);
            Length = that.Length.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArraySubarray(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Subarray(ArrayValue(procEnv), (int)Start.Evaluate(procEnv), (int)Length.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return Start;
                yield return Length;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".subarray(" + Start.Symbol + "," + Length.Symbol + ")"; }
        }
    }

    public class SequenceExpressionDequeSubdeque : SequenceExpressionContainer
    {
        public readonly SequenceExpression Start;
        public readonly SequenceExpression Length;

        public SequenceExpressionDequeSubdeque(SequenceExpression containerExpr, SequenceExpression start, SequenceExpression length)
            : base(SequenceExpressionType.DequeSubdeque, containerExpr)
        {
            Start = start;
            Length = length;
        }

        protected SequenceExpressionDequeSubdeque(SequenceExpressionDequeSubdeque that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            Start = that.Start.CopyExpression(originalToCopy, procEnv);
            Length = that.Length.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionDequeSubdeque(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);
            if(containerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("array<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "deque<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Subdeque(DequeValue(procEnv), (int)Start.Evaluate(procEnv), (int)Length.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return Start;
                yield return Length;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".subdeque(" + Start.Symbol + "," + Length.Symbol + ")"; }
        }
    }

    public class SequenceExpressionArrayOrderAscending : SequenceExpressionContainer
    {
        public SequenceExpressionArrayOrderAscending(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayOrderAscending, containerExpr)
        {
        }

        protected SequenceExpressionArrayOrderAscending(SequenceExpressionArrayOrderAscending that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrderAscending(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayOrderAscending(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".orderAscending()"; }
        }
    }

    public class SequenceExpressionArrayOrderDescending : SequenceExpressionContainer
    {
        public SequenceExpressionArrayOrderDescending(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayOrderDescending, containerExpr)
        {
        }

        protected SequenceExpressionArrayOrderDescending(SequenceExpressionArrayOrderDescending that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrderDescending(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayOrderDescending(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".orderDescending()"; }
        }
    }

    public class SequenceExpressionArrayGroup : SequenceExpressionContainer
    {
        public SequenceExpressionArrayGroup(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayGroup, containerExpr)
        {
        }

        protected SequenceExpressionArrayGroup(SequenceExpressionArrayGroup that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayGroup(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayGroup(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".group()"; }
        }
    }

    public class SequenceExpressionArrayKeepOneForEach : SequenceExpressionContainer
    {
        public SequenceExpressionArrayKeepOneForEach(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayKeepOneForEach, containerExpr)
        {
        }

        protected SequenceExpressionArrayKeepOneForEach(SequenceExpressionArrayKeepOneForEach that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayKeepOneForEach(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayKeepOneForEach(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".keepOneForEach()"; }
        }
    }

    public class SequenceExpressionArrayReverse : SequenceExpressionContainer
    {
        public SequenceExpressionArrayReverse(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayReverse, containerExpr)
        {
        }

        protected SequenceExpressionArrayReverse(SequenceExpressionArrayReverse that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayReverse(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayReverse(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".reverse()"; }
        }
    }

    public class SequenceExpressionArrayShuffle : SequenceExpressionContainer
    {
        public SequenceExpressionArrayShuffle(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayShuffle, containerExpr)
        {
        }

        protected SequenceExpressionArrayShuffle(SequenceExpressionArrayShuffle that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayShuffle(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Shuffle(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".shuffle()"; }
        }
    }

    public class SequenceExpressionArrayExtract : SequenceExpressionArrayByAttributeAccess
    {
        public SequenceExpressionArrayExtract(SequenceExpression containerExpr, String memberOrAttributeName)
            : base(SequenceExpressionType.ArrayExtract, containerExpr, memberOrAttributeName)
        {
        }

        protected SequenceExpressionArrayExtract(SequenceExpressionArrayExtract that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayExtract(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));
            String memberOrAttributeType = env.TypeOfMemberOrAttribute(arrayValueType, memberOrAttributeName);
            return "array<" + memberOrAttributeType + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Extract(ArrayValue(procEnv), memberOrAttributeName, procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Name + ".extract<" + memberOrAttributeName + ">()"; }
        }
    }

    public class SequenceExpressionArrayMap : SequenceExpressionContainer
    {
        public string TypeName;
        public SequenceVariable ArrayAccess;
        public SequenceVariable Index;
        public SequenceVariable Var;
        public SequenceExpression MappingExpr;

        public SequenceExpressionArrayMap(SequenceExpression containerExpr, String typeName,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable var, SequenceExpression mappingExpr)
            : base(SequenceExpressionType.ArrayMap, containerExpr)
        {
            TypeName = typeName;
            ArrayAccess = arrayAccess;
            Index = index;
            Var = var;
            MappingExpr = mappingExpr;
        }

        protected SequenceExpressionArrayMap(SequenceExpressionArrayMap that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            TypeName = that.TypeName;
            if(that.ArrayAccess != null)
                ArrayAccess = that.ArrayAccess.Copy(originalToCopy, procEnv);
            if(that.Index != null)
                Index = that.Index.Copy(originalToCopy, procEnv);
            Var = that.Var.Copy(originalToCopy, procEnv);
            MappingExpr = that.MappingExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayMap(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(ArrayAccess != null)
            {
                if(ArrayAccess.Type.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", ArrayAccess.Type);

                if(!TypesHelper.IsSameOrSubtype(ArrayAccess.Type, containerType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, containerType, ArrayAccess.Type);
            }

            if(Index != null)
            {
                if(!TypesHelper.IsSameOrSubtype(Index.Type, "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", Index.Type);
            }

            base.Check(env); // check children

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));

            if(!TypesHelper.IsSameOrSubtype(MappingExpr.Type(env), TypeName, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, TypeName, MappingExpr.Type(env));

            if(containerType != "")
            {
                if(!TypesHelper.IsSameOrSubtype(arrayValueType, Var.Type, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, Var.Type, arrayValueType);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<" + TypeName + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IList target = ContainerHelper.NewList(TypesHelper.GetType(TypeName, procEnv.Graph.Model));
            IList source = ArrayValue(procEnv);

            if(ArrayAccess != null)
                ArrayAccess.SetVariableValue(source, procEnv);

            for(int index = 0; index < source.Count; ++index)
            {
                if(Index != null)
                    Index.SetVariableValue(index, procEnv);
                Var.SetVariableValue(source[index], procEnv);
                object result = MappingExpr.Evaluate(procEnv);
                target.Add(result);
            }

            return target;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            MappingExpr.GetLocalVariables(variables, constructors);
            if(ArrayAccess != null)
                variables.Remove(ArrayAccess);
            if(Index != null)
                variables.Remove(Index);
            variables.Remove(Var);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return MappingExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Name + ".map<" + TypeName + ">{");
                if(ArrayAccess != null)
                    sb.Append(ArrayAccess.Name + "; ");
                if(Index != null)
                    sb.Append(Index.Name + " -> ");
                sb.Append(Var.Name + " -> " + MappingExpr.Symbol + "}");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionArrayRemoveIf : SequenceExpressionContainer
    {
        public SequenceVariable ArrayAccess;
        public SequenceVariable Index;
        public SequenceVariable Var;
        public SequenceExpression ConditionExpr;

        public SequenceExpressionArrayRemoveIf(SequenceExpression containerExpr,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable var, SequenceExpression conditionExpr)
            : base(SequenceExpressionType.ArrayRemoveIf, containerExpr)
        {
            ArrayAccess = arrayAccess;
            Index = index;
            Var = var;
            ConditionExpr = conditionExpr;
        }

        protected SequenceExpressionArrayRemoveIf(SequenceExpressionArrayRemoveIf that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            if(that.ArrayAccess != null)
                ArrayAccess = that.ArrayAccess.Copy(originalToCopy, procEnv);
            if(that.Index != null)
                Index = that.Index.Copy(originalToCopy, procEnv);
            Var = that.Var.Copy(originalToCopy, procEnv);
            ConditionExpr = that.ConditionExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayRemoveIf(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(ArrayAccess != null)
            {
                if(ArrayAccess.Type.StartsWith("set<") || ArrayAccess.Type.StartsWith("map<") || ArrayAccess.Type.StartsWith("deque<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", ArrayAccess.Type);

                if(!TypesHelper.IsSameOrSubtype(ArrayAccess.Type, containerType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, containerType, ArrayAccess.Type);
            }

            if(Index != null)
            {
                if(!TypesHelper.IsSameOrSubtype(Index.Type, "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", Index.Type);
            }

            base.Check(env); // check children
 
            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));

            if(!TypesHelper.IsSameOrSubtype(ConditionExpr.Type(env), "boolean", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "boolean", ConditionExpr.Type(env));

            if(containerType != "")
            {
                if(!TypesHelper.IsSameOrSubtype(arrayValueType, Var.Type, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, Var.Type, arrayValueType);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));
            return "array<" + arrayValueType + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IList source = ArrayValue(procEnv);
            string arrayType = TypesHelper.DotNetTypeToXgrsType(source.GetType());
            String arrayValueType = TypesHelper.ExtractSrc(arrayType);

            if(ArrayAccess != null)
                ArrayAccess.SetVariableValue(source, procEnv);

            IList target = ContainerHelper.NewList(TypesHelper.GetType(arrayValueType, procEnv.Graph.Model));

            for(int index = 0; index < source.Count; ++index)
            {
                if(Index != null)
                    Index.SetVariableValue(index, procEnv);
                Var.SetVariableValue(source[index], procEnv);
                if(!(bool)ConditionExpr.Evaluate(procEnv))
                    target.Add(source[index]);
            }

            return target;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            ConditionExpr.GetLocalVariables(variables, constructors);
            if(ArrayAccess != null)
                variables.Remove(ArrayAccess);
            if(Index != null)
                variables.Remove(Index);
            variables.Remove(Var);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return ConditionExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Name + ".removeIf{");
                if(ArrayAccess != null)
                    sb.Append(ArrayAccess.Name + "; ");
                if(Index != null)
                    sb.Append(Index.Name + " -> ");
                sb.Append(Var.Name + " -> " + ConditionExpr.Symbol + "}");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionArrayMapStartWithAccumulateBy : SequenceExpressionContainer
    {
        public string TypeName;
        public SequenceVariable InitArrayAccess;
        public SequenceExpression InitExpr;
        public SequenceVariable ArrayAccess;
        public SequenceVariable PreviousAccumulationAccess;
        public SequenceVariable Index;
        public SequenceVariable Var;
        public SequenceExpression MappingExpr;

        public SequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpression containerExpr, String typeName,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess, SequenceVariable index, SequenceVariable var, SequenceExpression mappingExpr)
            : base(SequenceExpressionType.ArrayMapStartWithAccumulateBy, containerExpr)
        {
            TypeName = typeName;
            InitArrayAccess = initArrayAccess;
            InitExpr = initExpr;
            ArrayAccess = arrayAccess;
            PreviousAccumulationAccess = previousAccumulationAccess;
            Index = index;
            Var = var;
            MappingExpr = mappingExpr;
        }

        protected SequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpressionArrayMapStartWithAccumulateBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            TypeName = that.TypeName;
            if(that.InitArrayAccess != null)
                InitArrayAccess = that.InitArrayAccess.Copy(originalToCopy, procEnv);
            InitExpr = that.InitExpr.CopyExpression(originalToCopy, procEnv);
            if(that.ArrayAccess != null)
                ArrayAccess = that.ArrayAccess.Copy(originalToCopy, procEnv);
            PreviousAccumulationAccess = that.PreviousAccumulationAccess.Copy(originalToCopy, procEnv);
            if(that.Index != null)
                Index = that.Index.Copy(originalToCopy, procEnv);
            Var = that.Var.Copy(originalToCopy, procEnv);
            MappingExpr = that.MappingExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayMapStartWithAccumulateBy(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(InitArrayAccess != null)
            {
                if(InitArrayAccess.Type.StartsWith("set<") || InitArrayAccess.Type.StartsWith("map<") || InitArrayAccess.Type.StartsWith("deque<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", ArrayAccess.Type);

                if(!TypesHelper.IsSameOrSubtype(InitArrayAccess.Type, containerType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, containerType, InitArrayAccess.Type);
            }

            if(!TypesHelper.IsSameOrSubtype(InitExpr.Type(env), TypeName, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, TypeName, InitExpr.Type(env));

            if(ArrayAccess != null)
            {
                if(ArrayAccess.Type.StartsWith("set<") || ArrayAccess.Type.StartsWith("map<") || ArrayAccess.Type.StartsWith("deque<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", ArrayAccess.Type);

                if(!TypesHelper.IsSameOrSubtype(ArrayAccess.Type, containerType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, containerType, ArrayAccess.Type);
            }

            if(!TypesHelper.IsSameOrSubtype(PreviousAccumulationAccess.Type, TypeName, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, TypeName, PreviousAccumulationAccess.Type);

            if(Index != null)
            {
                if(!TypesHelper.IsSameOrSubtype(Index.Type, "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", Index.Type);
            }

            base.Check(env); // check children

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));

            if(!TypesHelper.IsSameOrSubtype(MappingExpr.Type(env), TypeName, env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, TypeName, MappingExpr.Type(env));

            if(containerType != "")
            {
                if(!TypesHelper.IsSameOrSubtype(arrayValueType, Var.Type, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, Var.Type, arrayValueType);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<" + TypeName + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IList target = ContainerHelper.NewList(TypesHelper.GetType(TypeName, procEnv.Graph.Model));
            IList source = ArrayValue(procEnv);

            if(InitArrayAccess != null)
                InitArrayAccess.SetVariableValue(source, procEnv);

            PreviousAccumulationAccess.SetVariableValue(InitExpr.Evaluate(procEnv), procEnv);

            if(ArrayAccess != null)
                ArrayAccess.SetVariableValue(source, procEnv);

            for(int index = 0; index < source.Count; ++index)
            {
                if(Index != null)
                    Index.SetVariableValue(index, procEnv);
                Var.SetVariableValue(source[index], procEnv);
                object result = MappingExpr.Evaluate(procEnv);
                target.Add(result);
                PreviousAccumulationAccess.SetVariableValue(result, procEnv);
            }

            return target;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            InitExpr.GetLocalVariables(variables, constructors);
            MappingExpr.GetLocalVariables(variables, constructors);
            if(InitArrayAccess != null)
                variables.Remove(InitArrayAccess);
            if(ArrayAccess != null)
                variables.Remove(ArrayAccess);
            variables.Remove(PreviousAccumulationAccess);
            if(Index != null)
                variables.Remove(Index);
            variables.Remove(Var);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return InitExpr;
                yield return MappingExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Name + ".map<" + TypeName + ">");
                sb.Append("StartWith");
                sb.Append("{");
                if(InitArrayAccess != null)
                    sb.Append(InitArrayAccess.Name + "; ");
                sb.Append(InitExpr.Symbol);
                sb.Append("}");
                sb.Append("AccumulateBy");
                sb.Append("{");
                if(ArrayAccess != null)
                    sb.Append(ArrayAccess.Name + "; ");
                sb.Append(PreviousAccumulationAccess.Name + ", ");
                if(Index != null)
                    sb.Append(Index.Name + " -> ");
                sb.Append(Var.Name + " -> " + MappingExpr.Symbol);
                sb.Append("}");
                return sb.ToString();
            }
        }
    }

    public abstract class SequenceExpressionArrayByAttributeAccess : SequenceExpressionContainer
    {
        public string memberOrAttributeName;

        public SequenceExpressionArrayByAttributeAccess(SequenceExpressionType type, SequenceExpression containerExpr, String memberOrAttributeName)
            : base(type, containerExpr)
        {
            this.memberOrAttributeName = memberOrAttributeName;
        }

        protected SequenceExpressionArrayByAttributeAccess(SequenceExpressionArrayByAttributeAccess that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "array<T> type", containerType);

            if(containerType == "")
                return; // not possible to check array value type if type is not known statically

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));

            // throws exceptions in case the match or graph element type does not exist, or it does not contain an element of the given name
            String memberOrAttributeType = env.TypeOfMemberOrAttribute(arrayValueType, memberOrAttributeName);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return ContainerType(env);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }
    }

    public class SequenceExpressionArrayOrderAscendingBy : SequenceExpressionArrayByAttributeAccess
    {
        public SequenceExpressionArrayOrderAscendingBy(SequenceExpression containerExpr, String memberOrAttributeName)
            : base(SequenceExpressionType.ArrayOrderAscendingBy, containerExpr, memberOrAttributeName)
        {
        }

        protected SequenceExpressionArrayOrderAscendingBy(SequenceExpressionArrayOrderAscendingBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrderAscendingBy(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.OrderAscendingBy(ArrayValue(procEnv), memberOrAttributeName, procEnv);
        }

        public override string Symbol
        {
            get { return Name + ".orderAscendingBy<" + memberOrAttributeName + ">()"; }
        }
    }

    public class SequenceExpressionArrayOrderDescendingBy : SequenceExpressionArrayByAttributeAccess
    {
        public SequenceExpressionArrayOrderDescendingBy(SequenceExpression containerExpr, String memberOrAttributeName)
            : base(SequenceExpressionType.ArrayOrderDescendingBy, containerExpr, memberOrAttributeName)
        {
        }

        protected SequenceExpressionArrayOrderDescendingBy(SequenceExpressionArrayOrderDescendingBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrderDescendingBy(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.OrderDescendingBy(ArrayValue(procEnv), memberOrAttributeName, procEnv);
        }

        public override string Symbol
        {
            get { return Name + ".orderDescendingBy<" + memberOrAttributeName + ">()"; }
        }
    }

    public class SequenceExpressionArrayGroupBy : SequenceExpressionArrayByAttributeAccess
    {
        public SequenceExpressionArrayGroupBy(SequenceExpression containerExpr, String memberOrAttributeName)
            : base(SequenceExpressionType.ArrayGroupBy, containerExpr, memberOrAttributeName)
        {
        }

        protected SequenceExpressionArrayGroupBy(SequenceExpressionArrayGroupBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayGroupBy(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.GroupBy(ArrayValue(procEnv), memberOrAttributeName, procEnv);
        }

        public override string Symbol
        {
            get { return Name + ".groupBy<" + memberOrAttributeName + ">()"; }
        }
    }

    public class SequenceExpressionArrayKeepOneForEachBy : SequenceExpressionArrayByAttributeAccess
    {
        public SequenceExpressionArrayKeepOneForEachBy(SequenceExpression containerExpr, String memberOrAttributeName)
            : base(SequenceExpressionType.ArrayKeepOneForEachBy, containerExpr, memberOrAttributeName)
        {
        }

        protected SequenceExpressionArrayKeepOneForEachBy(SequenceExpressionArrayKeepOneForEachBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayKeepOneForEachBy(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.KeepOneForEach(ArrayValue(procEnv), memberOrAttributeName, procEnv);
        }

        public override string Symbol
        {
            get { return Name + ".keepOneForEach<" + memberOrAttributeName + ">()"; }
        }
    }

    public abstract class SequenceExpressionArrayByAttributeAccessIndexOf : SequenceExpressionContainer
    {
        public string memberOrAttributeName;
        public SequenceExpression ValueToSearchForExpr;

        public SequenceExpressionArrayByAttributeAccessIndexOf(SequenceExpressionType type, SequenceExpression containerExpr,
            String memberOrAttributeName, SequenceExpression valueToSearchForExpr)
            : base(type, containerExpr)
        {
            this.memberOrAttributeName = memberOrAttributeName;
            this.ValueToSearchForExpr = valueToSearchForExpr;
        }

        protected SequenceExpressionArrayByAttributeAccessIndexOf(SequenceExpressionArrayByAttributeAccessIndexOf that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType == "")
                return; // not possible to check array value type if type is not known statically

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));

            // throws exceptions in case the match or graph element type does not exist, or it does not contain an element of the given name
            String memberOrAttributeType = env.TypeOfMemberOrAttribute(arrayValueType, memberOrAttributeName);

            if(ValueToSearchForExpr.Type(env) != "")
            {
                if(ValueToSearchForExpr.Type(env) != memberOrAttributeType)
                    throw new SequenceParserExceptionTypeMismatch(Symbol, memberOrAttributeType, ValueToSearchForExpr.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            ValueToSearchForExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return ValueToSearchForExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }
    }

    public class SequenceExpressionArrayIndexOfBy : SequenceExpressionArrayByAttributeAccessIndexOf
    {
        public SequenceExpression StartPositionExpr;

        public SequenceExpressionArrayIndexOfBy(SequenceExpression containerExpr, String memberOrAttributeName,
            SequenceExpression valueToSearchForExpr)
            : base(SequenceExpressionType.ArrayIndexOfBy, containerExpr, memberOrAttributeName, valueToSearchForExpr)
        {
        }

        public SequenceExpressionArrayIndexOfBy(SequenceExpression containerExpr, String memberOrAttributeName,
            SequenceExpression valueToSearchForExpr, SequenceExpression startIndexExpr)
            : base(SequenceExpressionType.ArrayIndexOfBy, containerExpr, memberOrAttributeName, valueToSearchForExpr)
        {
            this.StartPositionExpr = startIndexExpr;
        }

        protected SequenceExpressionArrayIndexOfBy(SequenceExpressionArrayIndexOfBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            if(StartPositionExpr != null)
                StartPositionExpr = that.StartPositionExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayIndexOfBy(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(StartPositionExpr != null && StartPositionExpr.Type(env) != "")
            {
                if(StartPositionExpr != null && StartPositionExpr.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", ValueToSearchForExpr.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(StartPositionExpr != null)
                return ContainerHelper.IndexOfBy(ArrayValue(procEnv), memberOrAttributeName, ValueToSearchForExpr.Evaluate(procEnv), (int)StartPositionExpr.Evaluate(procEnv), procEnv);
            else
                return ContainerHelper.IndexOfBy(ArrayValue(procEnv), memberOrAttributeName, ValueToSearchForExpr.Evaluate(procEnv), procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            ValueToSearchForExpr.GetLocalVariables(variables, constructors);
            if(StartPositionExpr != null)
                StartPositionExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return ValueToSearchForExpr;
                if(StartPositionExpr != null)
                    yield return StartPositionExpr;
            }
        }

        public override string Symbol
        {
            get { return Name + ".indexOfBy<" + memberOrAttributeName + ">(" + ValueToSearchForExpr.Symbol + (StartPositionExpr != null ? "," + StartPositionExpr.Symbol: "")+ ")"; }
        }
    }

    public class SequenceExpressionArrayLastIndexOfBy : SequenceExpressionArrayByAttributeAccessIndexOf
    {
        public SequenceExpression StartPositionExpr;

        public SequenceExpressionArrayLastIndexOfBy(SequenceExpression containerExpr, String memberOrAttributeName,
            SequenceExpression valueToSearchForExpr)
            : base(SequenceExpressionType.ArrayLastIndexOfBy, containerExpr, memberOrAttributeName, valueToSearchForExpr)
        {
        }

        public SequenceExpressionArrayLastIndexOfBy(SequenceExpression containerExpr, String memberOrAttributeName,
            SequenceExpression valueToSearchForExpr, SequenceExpression startIndexExpr)
            : base(SequenceExpressionType.ArrayLastIndexOfBy, containerExpr, memberOrAttributeName, valueToSearchForExpr)
        {
            this.StartPositionExpr = startIndexExpr;
        }

        protected SequenceExpressionArrayLastIndexOfBy(SequenceExpressionArrayLastIndexOfBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
            if(StartPositionExpr != null)
                StartPositionExpr = that.StartPositionExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayLastIndexOfBy(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(StartPositionExpr != null && StartPositionExpr.Type(env) != "")
            {
                if(StartPositionExpr != null && StartPositionExpr.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", ValueToSearchForExpr.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(StartPositionExpr != null)
                return ContainerHelper.LastIndexOfBy(ArrayValue(procEnv), memberOrAttributeName, ValueToSearchForExpr.Evaluate(procEnv), (int)StartPositionExpr.Evaluate(procEnv), procEnv);
            else
                return ContainerHelper.LastIndexOfBy(ArrayValue(procEnv), memberOrAttributeName, ValueToSearchForExpr.Evaluate(procEnv), procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ContainerExpr.GetLocalVariables(variables, constructors);
            ValueToSearchForExpr.GetLocalVariables(variables, constructors);
            if(StartPositionExpr != null)
                StartPositionExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return ContainerExpr;
                yield return ValueToSearchForExpr;
                if(StartPositionExpr != null)
                    yield return StartPositionExpr;
            }
        }

        public override string Symbol
        {
            get { return Name + ".lastIndexOfBy<" + memberOrAttributeName + ">(" + ValueToSearchForExpr.Symbol + (StartPositionExpr != null ? "," + StartPositionExpr.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionArrayIndexOfOrderedBy : SequenceExpressionArrayByAttributeAccessIndexOf
    {
        public SequenceExpressionArrayIndexOfOrderedBy(SequenceExpression containerExpr, String memberOrAttributeName,
            SequenceExpression valueToSearchForExpr)
            : base(SequenceExpressionType.ArrayIndexOfOrderedBy, containerExpr, memberOrAttributeName, valueToSearchForExpr)
        {
        }

        protected SequenceExpressionArrayIndexOfOrderedBy(SequenceExpressionArrayIndexOfOrderedBy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayIndexOfOrderedBy(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.IndexOfBy(ArrayValue(procEnv), memberOrAttributeName, ValueToSearchForExpr.Evaluate(procEnv), procEnv);
        }

        public override string Symbol
        {
            get { return Name + ".indexOfOrderedBy<" + memberOrAttributeName + ">(" + ValueToSearchForExpr.Symbol + ")"; }
        }
    }

    public class SequenceExpressionElementFromGraph : SequenceExpression
    {
        public readonly String ElementName;
        public bool EmitProfiling;

        public SequenceExpressionElementFromGraph(String elemName)
            : base(SequenceExpressionType.ElementFromGraph)
        {
            ElementName = elemName;
            if(ElementName[0] == '\"')
                ElementName = ElementName.Substring(1, ElementName.Length - 2);
        }

        protected SequenceExpressionElementFromGraph(SequenceExpressionElementFromGraph that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            ElementName = that.ElementName;
            EmitProfiling = that.EmitProfiling;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionElementFromGraph(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph is INamedGraph))
                throw new InvalidOperationException("The @-operator can only be used with named graphs!");
            INamedGraph namedGraph = (INamedGraph)procEnv.Graph;
            IGraphElement elem = namedGraph.GetGraphElement(ElementName);
            if(elem == null)
                throw new InvalidOperationException("Graph element does not exist: \"" + ElementName + "\"!");
            return elem;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "@(" + ElementName + ")"; }
        }
    }

    public class SequenceExpressionNodeByName : SequenceExpression
    {
        public readonly SequenceExpression NodeName;
        public readonly SequenceExpression NodeType;
        public bool EmitProfiling;

        public SequenceExpressionNodeByName(SequenceExpression nodeName, SequenceExpression nodeType)
            : base(SequenceExpressionType.NodeByName)
        {
            NodeName = nodeName;
            NodeType = nodeType;
        }

        protected SequenceExpressionNodeByName(SequenceExpressionNodeByName that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            NodeName = that.NodeName.CopyExpression(originalToCopy, procEnv);
            if(that.NodeType != null)
                NodeType = that.NodeType.CopyExpression(originalToCopy, procEnv);
            EmitProfiling = that.EmitProfiling;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNodeByName(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeName.Type(env) != "")
            {
                if(NodeName.Type(env) != "string")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", (first) argument", "string", NodeName.Type(env));
            }
            CheckNodeTypeIsKnown(env, NodeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return NodeType!=null ? NodeType.Type(env) : "Node";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph is INamedGraph))
                throw new InvalidOperationException("The nodeByName(.) call can only be used with named graphs!");
            INamedGraph namedGraph = (INamedGraph)procEnv.Graph;

            NodeType nodeType = GetNodeType(procEnv, NodeType, "nodeByName");

            if(EmitProfiling)
                return GraphHelper.GetNode(namedGraph, (string)NodeName.Evaluate(procEnv), nodeType, procEnv);
            else
                return GraphHelper.GetNode(namedGraph, (string)NodeName.Evaluate(procEnv), nodeType);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return NodeName;
                if(NodeType != null)
                    yield return NodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "nodeByName(" + NodeName.Symbol + (NodeType != null ? ", " + NodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionEdgeByName : SequenceExpression
    {
        public readonly SequenceExpression EdgeName;
        public readonly SequenceExpression EdgeType;
        public bool EmitProfiling;

        public SequenceExpressionEdgeByName(SequenceExpression edgeName, SequenceExpression edgeType)
            : base(SequenceExpressionType.EdgeByName)
        {
            EdgeName = edgeName;
            EdgeType = edgeType;
        }

        protected SequenceExpressionEdgeByName(SequenceExpressionEdgeByName that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            EdgeName = that.EdgeName.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            EmitProfiling = that.EmitProfiling;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionEdgeByName(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeName.Type(env) != "")
            {
                if(EdgeName.Type(env) != "string")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", (first) argument", "string", EdgeName.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return EdgeType != null ? EdgeType.Type(env) : "AEdge";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph is INamedGraph))
                throw new InvalidOperationException("The edgeByName(.) call can only be used with named graphs!");
            INamedGraph namedGraph = (INamedGraph)procEnv.Graph;

            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, "edgeByName");

            if(EmitProfiling)
                return GraphHelper.GetEdge(namedGraph, (string)EdgeName.Evaluate(procEnv), edgeType, procEnv);
            else
                return GraphHelper.GetEdge(namedGraph, (string)EdgeName.Evaluate(procEnv), edgeType);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return EdgeName;
                if(EdgeType != null)
                    yield return EdgeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "edgeByName(" + EdgeName.Symbol + (EdgeType != null ? ", " + EdgeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionNodeByUnique : SequenceExpression
    {
        public readonly SequenceExpression NodeUniqueId;
        public readonly SequenceExpression NodeType;
        public bool EmitProfiling;

        public SequenceExpressionNodeByUnique(SequenceExpression nodeUniqueId, SequenceExpression nodeType)
            : base(SequenceExpressionType.NodeByUnique)
        {
            NodeUniqueId = nodeUniqueId;
            NodeType = nodeType;
        }

        protected SequenceExpressionNodeByUnique(SequenceExpressionNodeByUnique that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            NodeUniqueId = that.NodeUniqueId.CopyExpression(originalToCopy, procEnv);
            if(that.NodeType != null)
                NodeType = that.NodeType.CopyExpression(originalToCopy, procEnv);
            EmitProfiling = that.EmitProfiling;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNodeByUnique(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeUniqueId.Type(env) != "")
            {
                if(NodeUniqueId.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", (first) argument", "int", NodeUniqueId.Type(env));
            }
            CheckNodeTypeIsKnown(env, NodeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return NodeType != null ? NodeType.Type(env) : "Node";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph.Model.GraphElementsAreAccessibleByUniqueId))
                throw new InvalidOperationException("The nodeByUnique(.) call can only be used on graphs with a node edge unique definition!");

            NodeType nodeType = GetNodeType(procEnv, NodeType, "nodeByUnique");

            if(EmitProfiling)
                return GraphHelper.GetNode(procEnv.Graph, (int)NodeUniqueId.Evaluate(procEnv), nodeType, procEnv);
            else
                return GraphHelper.GetNode(procEnv.Graph, (int)NodeUniqueId.Evaluate(procEnv), nodeType);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return NodeUniqueId;
                if(NodeType != null)
                    yield return NodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "nodeByUnique(" + NodeUniqueId.Symbol + (NodeType != null ? ", " + NodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionEdgeByUnique : SequenceExpression
    {
        public readonly SequenceExpression EdgeUniqueId;
        public readonly SequenceExpression EdgeType;
        public bool EmitProfiling;

        public SequenceExpressionEdgeByUnique(SequenceExpression edgeName, SequenceExpression edgeType)
            : base(SequenceExpressionType.EdgeByUnique)
        {
            EdgeUniqueId = edgeName;
            EdgeType = edgeType;
        }

        protected SequenceExpressionEdgeByUnique(SequenceExpressionEdgeByUnique that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            EdgeUniqueId = that.EdgeUniqueId.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            EmitProfiling = that.EmitProfiling;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionEdgeByUnique(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeUniqueId.Type(env) != "")
            {
                if(EdgeUniqueId.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", (first) argument", "int", EdgeUniqueId.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return EdgeType != null ? EdgeType.Type(env) : "AEdge";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph.Model.GraphElementsAreAccessibleByUniqueId))
                throw new InvalidOperationException("The edgeByUnique(.) call can only be used on graphs with a node edge unique definition!");

            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, "edgeByUnique");

            if(EmitProfiling)
                return GraphHelper.GetEdge(procEnv.Graph, (int)EdgeUniqueId.Evaluate(procEnv), edgeType, procEnv);
            else
                return GraphHelper.GetEdge(procEnv.Graph, (int)EdgeUniqueId.Evaluate(procEnv), edgeType);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return EdgeUniqueId;
                if(EdgeType != null)
                    yield return EdgeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "edgeByUnique(" + EdgeUniqueId.Symbol + (EdgeType != null ? ", " + EdgeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionSource : SequenceExpression
    {
        public readonly SequenceExpression Edge;

        public SequenceExpressionSource(SequenceExpression edge)
            : base(SequenceExpressionType.Source)
        {
            Edge = edge;
        }

        protected SequenceExpressionSource(SequenceExpressionSource that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Edge = that.Edge.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionSource(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.ExecuteImpl(procEnv)).Source;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Edge; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "source(" + Edge.Symbol + ")"; }
        }
    }

    public class SequenceExpressionTarget : SequenceExpression
    {
        public readonly SequenceExpression Edge;

        public SequenceExpressionTarget(SequenceExpression edge)
            : base(SequenceExpressionType.Target)
        {
            Edge = edge;
        }

        protected SequenceExpressionTarget(SequenceExpressionTarget that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Edge = that.Edge.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionTarget(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.ExecuteImpl(procEnv)).Target;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Edge; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "target(" + Edge.Symbol + ")"; }
        }
    }

    public class SequenceExpressionOpposite : SequenceExpression
    {
        public readonly SequenceExpression Edge;
        public readonly SequenceExpression Node;

        public SequenceExpressionOpposite(SequenceExpression edge, SequenceExpression node)
            : base(SequenceExpressionType.Opposite)
        {
            Edge = edge;
            Node = node;
        }

        protected SequenceExpressionOpposite(SequenceExpressionOpposite that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Edge = that.Edge.CopyExpression(originalToCopy, procEnv);
            Node = that.Node.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionOpposite(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.ExecuteImpl(procEnv)).Opposite((INode)Node.ExecuteImpl(procEnv));
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Edge;
                yield return Node;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "opposite(" + Edge.Symbol + "," + Node.Symbol + ")"; }
        }
    }

    public class SequenceExpressionAttributeAccess : SequenceExpression
    {
        public readonly SequenceExpression Source;
        public readonly String AttributeName;

        public SequenceExpressionAttributeAccess(SequenceExpression source, String attributeName)
            : base(SequenceExpressionType.GraphElementAttribute)
        {
            Source = source;
            AttributeName = attributeName;
        }

        protected SequenceExpressionAttributeAccess(SequenceExpressionAttributeAccess that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Source = that.Source.CopyExpression(originalToCopy, procEnv);
            AttributeName = that.AttributeName;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionAttributeAccess(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            CheckAndReturnAttributeType(env);
        }

        public string CheckAndReturnAttributeType(SequenceCheckingEnvironment env)
        {
            if(Source.Type(env) == "")
                return ""; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            InheritanceType inheritanceType = TypesHelper.GetInheritanceType(Source.Type(env), env.Model);
            if(inheritanceType == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge or object or transient object type (class)", Source.Type(env));
            AttributeType attributeType = inheritanceType.GetAttributeType(AttributeName);
            if(attributeType == null)
                throw new SequenceParserExceptionUnknownAttribute(AttributeName, inheritanceType.Name);

            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(Source.Type(env) == "")
                return ""; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            
            InheritanceType inheritanceType = TypesHelper.GetInheritanceType(Source.Type(env), env.Model);
            AttributeType attributeType = inheritanceType.GetAttributeType(AttributeName);
            if(attributeType == null)
                return ""; // error, will be reported by Check, just ensure we don't crash here

            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object source = Source.Evaluate(procEnv);
            if(source is IGraphElement)
                return Execute(procEnv, (IGraphElement)source, AttributeName);
            else if(source is IObject)
                return Execute(procEnv, (IObject)source, AttributeName);
            else //if(source is ITransientObject)
                return Execute(procEnv, (ITransientObject)source, AttributeName);
        }

        public static object Execute(IGraphProcessingEnvironment procEnv, IGraphElement elem, string attributeName)
        {
            object value = elem.GetAttribute(attributeName);
            value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                elem, attributeName, value);
            return value;
        }

        public static object Execute(IGraphProcessingEnvironment procEnv, IObject elem, string attributeName)
        {
            object value = elem.GetAttribute(attributeName);
            value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                elem, attributeName, value);
            return value;
        }

        public static object Execute(IGraphProcessingEnvironment procEnv, ITransientObject elem, string attributeName)
        {
            return elem.GetAttribute(attributeName);
        }

        public object ExecuteNoImplicitContainerCopy(IGraphProcessingEnvironment procEnv)
        {
            IAttributeBearer elem = (IAttributeBearer)Source.Evaluate(procEnv);
            object value = elem.GetAttribute(AttributeName);
            return value;
        }

        public object ExecuteNoImplicitContainerCopy(IGraphProcessingEnvironment procEnv, out IAttributeBearer elem, out AttributeType attrType)
        {
            elem = (IAttributeBearer)Source.Evaluate(procEnv);
            object value = elem.GetAttribute(AttributeName);
            attrType = elem.Type.GetAttributeType(AttributeName);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Source.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Source; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Source.Symbol + "." + AttributeName; }
        }
    }

    public class SequenceExpressionMatchAccess : SequenceExpression
    {
        public readonly SequenceExpression Source;
        public readonly String ElementName;

        public SequenceExpressionMatchAccess(SequenceExpression source, String elementName)
            : base(SequenceExpressionType.ElementOfMatch)
        {
            Source = source;
            ElementName = elementName;
        }

        protected SequenceExpressionMatchAccess(SequenceExpressionMatchAccess that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Source = that.Source.CopyExpression(originalToCopy, procEnv);
            ElementName = that.ElementName;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMatchAccess(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!Source.Type(env).StartsWith("match<"))
                throw new Exception("SequenceExpression MatchAccess can only access a variable of type match<rulename> (or match<class match-class-name>)");

            // throws exceptions in case the match type does not exist, or it does not contain an element of the given name
            string elementType = env.TypeOfMemberOrAttribute(Source.Type(env), ElementName);

            if(elementType == "")
                throw new Exception("Internal failure, static type of element in match type not known");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return env.TypeOfMemberOrAttribute(Source.Type(env), ElementName);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Execute(procEnv, (IMatch)Source.Evaluate(procEnv), ElementName);
        }

        public static object Execute(IGraphProcessingEnvironment procEnv, IMatch match, string elementName)
        {
            object value = match.getNode(elementName);
            if(value != null)
                return value;
            value = match.getEdge(elementName);
            if(value != null)
                return value;
            value = match.getVariable(elementName);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Source.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Source; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Source.Symbol + "." + ElementName; }
        }
    }

    public class SequenceExpressionAttributeOrMatchAccess : SequenceExpression
    {
        public readonly SequenceExpression Source;
        public readonly String AttributeOrElementName;

        public SequenceExpressionAttributeAccess AttributeAccess;
        public SequenceExpressionMatchAccess MatchAccess;

        public SequenceExpressionAttributeOrMatchAccess(SequenceExpression source, String attributeOrElementName)
            : base(SequenceExpressionType.GraphElementAttributeOrElementOfMatch)
        {
            Source = source;
            AttributeOrElementName = attributeOrElementName;
        }

        protected SequenceExpressionAttributeOrMatchAccess(SequenceExpressionAttributeOrMatchAccess that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Source = that.Source.CopyExpression(originalToCopy, procEnv);
            AttributeOrElementName = that.AttributeOrElementName;

            if(that.AttributeAccess != null)
                AttributeAccess = (SequenceExpressionAttributeAccess)that.AttributeAccess.CopyExpression(originalToCopy, procEnv);
            if(that.MatchAccess != null)
                MatchAccess = (SequenceExpressionMatchAccess)that.MatchAccess.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionAttributeOrMatchAccess(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            // if type is known statically, resolve to correct access class (otherwise runtime decision is required)
            if(Source.Type(env).StartsWith("match<"))
            {
                MatchAccess = new SequenceExpressionMatchAccess(Source, AttributeOrElementName);
                MatchAccess.Check(env);
            }
            else if(Source.Type(env)!="")
            {
                AttributeAccess = new SequenceExpressionAttributeAccess(Source, AttributeOrElementName);
                AttributeAccess.Check(env);
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(AttributeAccess != null)
                return AttributeAccess.Type(env);
            else if(MatchAccess != null)
                return MatchAccess.Type(env);
            else
                return "";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(AttributeAccess != null)
                return AttributeAccess.Evaluate(procEnv);
            else if(MatchAccess != null)
                return MatchAccess.Evaluate(procEnv);
            else
            {
                object source = Source.Evaluate(procEnv);
                if(source is IMatch)
                    return SequenceExpressionMatchAccess.Execute(procEnv, (IMatch)source, AttributeOrElementName);
                else if(source is IGraphElement)
                    return SequenceExpressionAttributeAccess.Execute(procEnv, (IGraphElement)source, AttributeOrElementName);
                else if(source is IObject)
                    return SequenceExpressionAttributeAccess.Execute(procEnv, (IObject)source, AttributeOrElementName);
                else //if(source is ITransientObject)
                    return SequenceExpressionAttributeAccess.Execute(procEnv, (ITransientObject)source, AttributeOrElementName);
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Source.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Source; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return Source.Symbol + "." + AttributeOrElementName; }
        }
    }

    public abstract class SequenceExpressionGraphQuery : SequenceExpression
    {
        public bool EmitProfiling;

        protected SequenceExpressionGraphQuery(SequenceExpressionType type)
            : base(type)
        {
        }

        protected SequenceExpressionGraphQuery(SequenceExpressionGraphQuery that)
          : base(that)
        {
            EmitProfiling = that.EmitProfiling;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public static string GetEdgeRootTypeWithDirection(SequenceExpression EdgeType, SequenceCheckingEnvironment env)
        {
            if(EdgeType == null)
                return "AEdge";
            if(EdgeType.Type(env) == "")
                return "AEdge";

            string typeString = null;
            if(EdgeType.Type(env) == "string")
            {
                if(EdgeType is SequenceExpressionConstant)
                    typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
            }
            else
                typeString = EdgeType.Type(env);
            EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
            if(edgeType == null)
                return "AEdge";

            if(edgeType.Directedness == Directedness.Directed)
                return "Edge";
            if(edgeType.Directedness == Directedness.Undirected)
                return "UEdge";

            return "AEdge";
        }
    }

    public class SequenceExpressionNodes : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression NodeType;

        public SequenceExpressionNodes(SequenceExpression nodeType)
            : base(SequenceExpressionType.Nodes)
        {
            NodeType = nodeType;
        }

        protected SequenceExpressionNodes(SequenceExpressionNodes that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            if(that.NodeType != null)
                NodeType = that.NodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNodes(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            CheckNodeTypeIsKnown(env, NodeType, ", argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "set<Node>";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = GetNodeType(procEnv, NodeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.Nodes(procEnv.Graph, nodeType, procEnv);
            else
                return GraphHelper.Nodes(procEnv.Graph, nodeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(NodeType != null)
                NodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(NodeType != null)
                    yield return NodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get { return "nodes" ; }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + (NodeType != null ? NodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionEdges : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression EdgeType;

        public SequenceExpressionEdges(SequenceExpression edgeType)
            : base(SequenceExpressionType.Edges)
        {
            EdgeType = edgeType;
        }

        protected SequenceExpressionEdges(SequenceExpressionEdges that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionEdges(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            CheckEdgeTypeIsKnown(env, EdgeType, ", argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "set<" + GetEdgeRootTypeWithDirection(EdgeType, env) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.Edges(procEnv.Graph, edgeType, procEnv);
            else
                return GraphHelper.Edges(procEnv.Graph, edgeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(EdgeType != null)
                    yield return EdgeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get { return "edges"; }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + (EdgeType != null ? EdgeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountNodes : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression NodeType;

        public SequenceExpressionCountNodes(SequenceExpression nodeType)
            : base(SequenceExpressionType.CountNodes)
        {
            NodeType = nodeType;
        }

        protected SequenceExpressionCountNodes(SequenceExpressionCountNodes that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            if(that.NodeType != null)
                NodeType = that.NodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCountNodes(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            CheckNodeTypeIsKnown(env, NodeType, ", argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = GetNodeType(procEnv, NodeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.CountNodes(procEnv.Graph, nodeType, procEnv);
            else
                return GraphHelper.CountNodes(procEnv.Graph, nodeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(NodeType != null)
                NodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(NodeType != null)
                    yield return NodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get { return "countNodes"; }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + (NodeType != null ? NodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountEdges : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression EdgeType;

        public SequenceExpressionCountEdges(SequenceExpression edgeType)
            : base(SequenceExpressionType.CountEdges)
        {
            EdgeType = edgeType;
        }

        protected SequenceExpressionCountEdges(SequenceExpressionCountEdges that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCountEdges(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            CheckEdgeTypeIsKnown(env, EdgeType, ", argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.CountEdges(procEnv.Graph, edgeType, procEnv);
            else
                return GraphHelper.CountEdges(procEnv.Graph, edgeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(EdgeType != null)
                    yield return EdgeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get { return "countEdges"; }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + (EdgeType != null ? EdgeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionEmpty : SequenceExpression
    {
        public SequenceExpressionEmpty()
            : base(SequenceExpressionType.Empty)
        {
        }

        protected SequenceExpressionEmpty(SequenceExpressionEmpty that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionEmpty(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.NumNodes + procEnv.Graph.NumEdges == 0;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "empty()"; }
        }
    }

    public class SequenceExpressionNow : SequenceExpression
    {
        public SequenceExpressionNow()
            : base(SequenceExpressionType.Now)
        {
        }

        protected SequenceExpressionNow(SequenceExpressionNow that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNow(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "long";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return DateTime.UtcNow.ToFileTime();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Time::now()"; }
        }
    }

    public class SequenceExpressionSize : SequenceExpression
    {
        public SequenceExpressionSize()
            : base(SequenceExpressionType.Size)
        {
        }

        protected SequenceExpressionSize(SequenceExpressionSize that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionSize(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.NumNodes + procEnv.Graph.NumEdges;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "size()"; }
        }
    }

    public class SequenceExpressionAdjacentIncident : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionAdjacentIncident(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.AdjacentNodes || type == SequenceExpressionType.AdjacentNodesViaIncoming || type == SequenceExpressionType.AdjacentNodesViaOutgoing)
                && !(type == SequenceExpressionType.IncidentEdges || type == SequenceExpressionType.IncomingEdges || type == SequenceExpressionType.OutgoingEdges))
            {
                throw new Exception("Internal failure, adjacent/incident with wrong type");
            }
        }

        protected SequenceExpressionAdjacentIncident(SequenceExpressionAdjacentIncident that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionAdjacentIncident(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", third argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(SequenceExpressionType == SequenceExpressionType.AdjacentNodes
                || SequenceExpressionType == SequenceExpressionType.AdjacentNodesViaIncoming
                || SequenceExpressionType == SequenceExpressionType.AdjacentNodesViaOutgoing)
            {
                return "set<Node>";
            }
            else // SequenceExpressionType.IncidentEdges || SequenceExpressionType.IncomingEdges || SequenceExpressionType.OutgoingEdges
                return "set<" + GetEdgeRootTypeWithDirection(EdgeType, env) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.AdjacentNodes:
                if(EmitProfiling)
                    return GraphHelper.Adjacent(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.Adjacent(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.AdjacentNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.AdjacentIncoming(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.AdjacentIncoming(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.AdjacentNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.AdjacentOutgoing(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.AdjacentOutgoing(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.IncidentEdges:
                if(EmitProfiling)
                    return GraphHelper.Incident(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.Incident(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.IncomingEdges:
                if(EmitProfiling)
                    return GraphHelper.Incoming(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.Incoming(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.OutgoingEdges:
                if(EmitProfiling)
                    return GraphHelper.Outgoing(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.Outgoing(sourceNode, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.AdjacentNodes: return "adjacent";
                case SequenceExpressionType.AdjacentNodesViaIncoming: return "adjacentIncoming";
                case SequenceExpressionType.AdjacentNodesViaOutgoing: return "adjacentOutgoing";
                case SequenceExpressionType.IncidentEdges: return "incident";
                case SequenceExpressionType.IncomingEdges: return "incoming";
                case SequenceExpressionType.OutgoingEdges: return "outgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountAdjacentIncident : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionCountAdjacentIncident(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.CountAdjacentNodes || type == SequenceExpressionType.CountAdjacentNodesViaIncoming || type == SequenceExpressionType.CountAdjacentNodesViaOutgoing)
                && !(type == SequenceExpressionType.CountIncidentEdges || type == SequenceExpressionType.CountIncomingEdges || type == SequenceExpressionType.CountOutgoingEdges))
            {
                throw new Exception("Internal failure, count adjacent/incident with wrong type");
            }
        }

        protected SequenceExpressionCountAdjacentIncident(SequenceExpressionCountAdjacentIncident that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCountAdjacentIncident(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", third argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.CountAdjacentNodes:
                if(EmitProfiling)
                    return GraphHelper.CountAdjacent(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountAdjacent(procEnv.Graph, sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountAdjacentNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.CountAdjacentIncoming(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountAdjacentIncoming(procEnv.Graph, sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.CountAdjacentOutgoing(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountAdjacentOutgoing(procEnv.Graph, sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountIncidentEdges:
                if(EmitProfiling)
                    return GraphHelper.CountIncident(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountIncident(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountIncomingEdges:
                if(EmitProfiling)
                    return GraphHelper.CountIncoming(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountIncoming(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountOutgoingEdges:
                if(EmitProfiling)
                    return GraphHelper.CountOutgoing(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountOutgoing(sourceNode, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.CountAdjacentNodes: return "countAdjacent";
                case SequenceExpressionType.CountAdjacentNodesViaIncoming: return "countAdjacentIncoming";
                case SequenceExpressionType.CountAdjacentNodesViaOutgoing: return "countAdjacentOutgoing";
                case SequenceExpressionType.CountIncidentEdges: return "countIncident";
                case SequenceExpressionType.CountIncomingEdges: return "countIncoming";
                case SequenceExpressionType.CountOutgoingEdges: return "countOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }

        public override string Symbol
        {
            get
            {
                return FunctionSymbol + "(" + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")";
            }
        }
    }

    public class SequenceExpressionReachable : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionReachable(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.ReachableNodes || type == SequenceExpressionType.ReachableNodesViaIncoming || type == SequenceExpressionType.ReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.ReachableEdges || type == SequenceExpressionType.ReachableEdgesViaIncoming || type == SequenceExpressionType.ReachableEdgesViaOutgoing))
            {
                throw new Exception("Internal failure, reachable with wrong type");
            }
        }

        protected SequenceExpressionReachable(SequenceExpressionReachable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionReachable(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", third argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(SequenceExpressionType == SequenceExpressionType.ReachableNodes
                || SequenceExpressionType == SequenceExpressionType.ReachableNodesViaIncoming
                || SequenceExpressionType == SequenceExpressionType.ReachableNodesViaOutgoing)
            {
                return "set<Node>";
            }
            else // SequenceExpressionType.ReachableEdges || SequenceExpressionType.ReachableEdgesViaIncoming || SequenceExpressionType.ReachableEdgesViaOutgoing
                return "set<" + GetEdgeRootTypeWithDirection(EdgeType, env) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.ReachableNodes:
                if(EmitProfiling)
                    return GraphHelper.Reachable(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.Reachable(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.ReachableNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.ReachableIncoming(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.ReachableIncoming(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.ReachableNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.ReachableOutgoing(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.ReachableOutgoing(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.ReachableEdges:
                if(EmitProfiling)
                    return GraphHelper.ReachableEdges(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.ReachableEdges(procEnv.Graph, sourceNode, edgeType, nodeType);
            case SequenceExpressionType.ReachableEdgesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.ReachableEdgesIncoming(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.ReachableEdgesIncoming(procEnv.Graph, sourceNode, edgeType, nodeType);
            case SequenceExpressionType.ReachableEdgesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.ReachableEdgesOutgoing(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.ReachableEdgesOutgoing(procEnv.Graph, sourceNode, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.ReachableNodes: return "reachable";
                case SequenceExpressionType.ReachableNodesViaIncoming: return "reachableIncoming";
                case SequenceExpressionType.ReachableNodesViaOutgoing: return "reachableOutgoing";
                case SequenceExpressionType.ReachableEdges: return "reachableEdges";
                case SequenceExpressionType.ReachableEdgesViaIncoming: return "reachableEdgesIncoming";
                case SequenceExpressionType.ReachableEdgesViaOutgoing: return "reachableEdgesOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }
        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountReachable : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionCountReachable(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.CountReachableNodes || type == SequenceExpressionType.CountReachableNodesViaIncoming || type == SequenceExpressionType.CountReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.CountReachableEdges || type == SequenceExpressionType.CountReachableEdgesViaIncoming || type == SequenceExpressionType.CountReachableEdgesViaOutgoing))
            {
                throw new Exception("Internal failure, count reachable with wrong type");
            }
        }

        protected SequenceExpressionCountReachable(SequenceExpressionCountReachable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCountReachable(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", third argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.CountReachableNodes:
                if(EmitProfiling)
                    return GraphHelper.CountReachable(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountReachable(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountReachableNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.CountReachableIncoming(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountReachableIncoming(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountReachableNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.CountReachableOutgoing(sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountReachableOutgoing(sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountReachableEdges:
                if(EmitProfiling)
                    return GraphHelper.CountReachableEdges(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountReachableEdges(procEnv.Graph, sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountReachableEdgesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.CountReachableEdgesIncoming(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountReachableEdgesIncoming(procEnv.Graph, sourceNode, edgeType, nodeType);
            case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.CountReachableEdgesOutgoing(procEnv.Graph, sourceNode, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountReachableEdgesOutgoing(procEnv.Graph, sourceNode, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.ReachableNodes: return "countReachable";
                case SequenceExpressionType.ReachableNodesViaIncoming: return "countReachableIncoming";
                case SequenceExpressionType.ReachableNodesViaOutgoing: return "countReachableOutgoing";
                case SequenceExpressionType.ReachableEdges: return "countReachableEdges";
                case SequenceExpressionType.ReachableEdgesViaIncoming: return "countReachableEdgesIncoming";
                case SequenceExpressionType.ReachableEdgesViaOutgoing: return "countReachableEdgesOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionBoundedReachable : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression Depth;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionBoundedReachable(SequenceExpression sourceNode, SequenceExpression depth, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            Depth = depth;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.BoundedReachableNodes || type == SequenceExpressionType.BoundedReachableNodesViaIncoming || type == SequenceExpressionType.BoundedReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.BoundedReachableEdges || type == SequenceExpressionType.BoundedReachableEdgesViaIncoming || type == SequenceExpressionType.BoundedReachableEdgesViaOutgoing))
            {
                throw new Exception("Internal failure, bounded reachable with wrong type");
            }
        }

        protected SequenceExpressionBoundedReachable(SequenceExpressionBoundedReachable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            Depth = that.Depth.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionBoundedReachable(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "int type", Depth.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(SequenceExpressionType == SequenceExpressionType.BoundedReachableNodes
                || SequenceExpressionType == SequenceExpressionType.BoundedReachableNodesViaIncoming
                || SequenceExpressionType == SequenceExpressionType.BoundedReachableNodesViaOutgoing)
            {
                return "set<Node>";
            }
            else // SequenceExpressionType.BoundedReachableEdges || SequenceExpressionType.BoundedReachableEdgesViaIncoming || SequenceExpressionType.BoundedReachableEdgesViaOutgoing
                return "set<" + GetEdgeRootTypeWithDirection(EdgeType, env) + ">";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.BoundedReachableNodes:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachable(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachable(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.BoundedReachableNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableIncoming(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableIncoming(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableOutgoing(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableOutgoing(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.BoundedReachableEdges:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableEdges(procEnv.Graph, sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableEdges(procEnv.Graph, sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableEdgesIncoming(procEnv.Graph, sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableEdgesIncoming(procEnv.Graph, sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableEdgesOutgoing(procEnv.Graph, sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableEdgesOutgoing(procEnv.Graph, sourceNode, depth, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            Depth.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                yield return Depth;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.BoundedReachableNodes: return "boundedReachable";
                case SequenceExpressionType.BoundedReachableNodesViaIncoming: return "boundedReachableIncoming";
                case SequenceExpressionType.BoundedReachableNodesViaOutgoing: return "boundedReachableOutgoing";
                case SequenceExpressionType.BoundedReachableEdges: return "boundedReachableEdges";
                case SequenceExpressionType.BoundedReachableEdgesViaIncoming: return "boundedReachableEdgesIncoming";
                case SequenceExpressionType.BoundedReachableEdgesViaOutgoing: return "boundedReachableEdgesOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }
        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionBoundedReachableWithRemainingDepth : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression Depth;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionBoundedReachableWithRemainingDepth(SequenceExpression sourceNode, SequenceExpression depth, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            Depth = depth;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.BoundedReachableNodesWithRemainingDepth || type == SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming || type == SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing))
                throw new Exception("Internal failure, bounded reachable with remaining depth with wrong type");
        }

        protected SequenceExpressionBoundedReachableWithRemainingDepth(SequenceExpressionBoundedReachableWithRemainingDepth that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            Depth = that.Depth.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionBoundedReachableWithRemainingDepth(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "int type", Depth.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "map<Node,int>";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableWithRemainingDepth(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableWithRemainingDepth(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableWithRemainingDepthIncoming(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableWithRemainingDepthIncoming(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.BoundedReachableWithRemainingDepthOutgoing(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.BoundedReachableWithRemainingDepthOutgoing(sourceNode, depth, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            Depth.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                yield return Depth;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.BoundedReachableNodes: return "boundedReachable";
                case SequenceExpressionType.BoundedReachableNodesViaIncoming: return "boundedReachableIncoming";
                case SequenceExpressionType.BoundedReachableNodesViaOutgoing: return "boundedReachableOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }
        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountBoundedReachable : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression Depth;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionCountBoundedReachable(SequenceExpression sourceNode, SequenceExpression depth, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            Depth = depth;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.CountBoundedReachableNodes || type == SequenceExpressionType.CountBoundedReachableNodesViaIncoming || type == SequenceExpressionType.CountBoundedReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.CountBoundedReachableEdges || type == SequenceExpressionType.CountBoundedReachableEdgesViaIncoming || type == SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing))
            {
                throw new Exception("Internal failure, count bounded reachable with wrong type");
            }
        }

        protected SequenceExpressionCountBoundedReachable(SequenceExpressionCountBoundedReachable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            Depth = that.Depth.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCountBoundedReachable(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "int type", Depth.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.CountBoundedReachableNodes:
                if(EmitProfiling)
                    return GraphHelper.CountBoundedReachable(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountBoundedReachable(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.CountBoundedReachableIncoming(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountBoundedReachableIncoming(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.CountBoundedReachableOutgoing(sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountBoundedReachableOutgoing(sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.CountBoundedReachableEdges:
                if(EmitProfiling)
                    return GraphHelper.CountBoundedReachableEdges(procEnv.Graph, sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountBoundedReachableEdges(procEnv.Graph, sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.CountBoundedReachableEdgesIncoming(procEnv.Graph, sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountBoundedReachableEdgesIncoming(procEnv.Graph, sourceNode, depth, edgeType, nodeType);
            case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.CountBoundedReachableEdgesOutgoing(procEnv.Graph, sourceNode, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.CountBoundedReachableEdgesOutgoing(procEnv.Graph, sourceNode, depth, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            Depth.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                yield return Depth;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.CountBoundedReachableNodes: return "countBoundedReachable";
                case SequenceExpressionType.CountBoundedReachableNodesViaIncoming: return "countBoundedReachableIncoming";
                case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing: return "countBoundedReachableOutgoing";
                case SequenceExpressionType.CountBoundedReachableEdges: return "countBoundedReachableEdges";
                case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming: return "countBoundedReachableEdgesIncoming";
                case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing: return "countBoundedReachableEdgesOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }
        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionIsBoundedReachable : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression EndElement;
        public readonly SequenceExpression Depth;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionIsBoundedReachable(SequenceExpression sourceNode, SequenceExpression endElement, SequenceExpression depth, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EndElement = endElement;
            Depth = depth;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.IsBoundedReachableNodes || type == SequenceExpressionType.IsBoundedReachableNodesViaIncoming || type == SequenceExpressionType.IsBoundedReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.IsBoundedReachableEdges || type == SequenceExpressionType.IsBoundedReachableEdgesViaIncoming || type == SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing))
            {
                throw new Exception("Internal failure, is bounded reachable with wrong type");
            }
        }

        protected SequenceExpressionIsBoundedReachable(SequenceExpressionIsBoundedReachable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            EndElement = that.EndElement.CopyExpression(originalToCopy, procEnv);
            Depth = that.Depth.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionIsBoundedReachable(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodes || SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaOutgoing)
                {
                    if(TypesHelper.GetNodeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "node type", EndElement.Type(env));
                }
                else
                {
                    if(TypesHelper.GetEdgeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "edge type", EndElement.Type(env));
                }
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "int type", Depth.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            IGraphElement endElement = (IGraphElement)EndElement.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.IsBoundedReachableNodes:
                if(EmitProfiling)
                    return GraphHelper.IsBoundedReachable(procEnv.Graph, sourceNode, (INode)endElement, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsBoundedReachable(procEnv.Graph, sourceNode, (INode)endElement, depth, edgeType, nodeType);
            case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.IsBoundedReachableIncoming(procEnv.Graph, sourceNode, (INode)endElement, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsBoundedReachableIncoming(procEnv.Graph, sourceNode, (INode)endElement, depth, edgeType, nodeType);
            case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.IsBoundedReachableOutgoing(procEnv.Graph, sourceNode, (INode)endElement, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsBoundedReachableOutgoing(procEnv.Graph, sourceNode, (INode)endElement, depth, edgeType, nodeType);
            case SequenceExpressionType.IsBoundedReachableEdges:
                if(EmitProfiling)
                    return GraphHelper.IsBoundedReachableEdges(procEnv.Graph, sourceNode, (IEdge)endElement, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsBoundedReachableEdges(procEnv.Graph, sourceNode, (IEdge)endElement, depth, edgeType, nodeType);
            case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.IsBoundedReachableEdgesIncoming(procEnv.Graph, sourceNode, (IEdge)endElement, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsBoundedReachableEdgesIncoming(procEnv.Graph, sourceNode, (IEdge)endElement, depth, edgeType, nodeType);
            case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.IsBoundedReachableEdgesOutgoing(procEnv.Graph, sourceNode, (IEdge)endElement, depth, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsBoundedReachableEdgesOutgoing(procEnv.Graph, sourceNode, (IEdge)endElement, depth, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            EndElement.GetLocalVariables(variables, constructors);
            Depth.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                yield return EndElement;
                yield return Depth;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.IsBoundedReachableNodes: return "isBoundedReachable";
                case SequenceExpressionType.IsBoundedReachableNodesViaIncoming: return "isBoundedReachableIncoming";
                case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing: return "isBoundedReachableOutgoing";
                case SequenceExpressionType.IsBoundedReachableEdges: return "isBoundedReachableEdges";
                case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming: return "isBoundedReachableEdgesIncoming";
                case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing: return "isBoundedReachableEdgesOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + "," + EndElement.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionIsAdjacentIncident : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression EndElement;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionIsAdjacentIncident(SequenceExpression sourceNode, SequenceExpression endElement, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EndElement = endElement;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.IsAdjacentNodes || type == SequenceExpressionType.IsAdjacentNodesViaIncoming || type == SequenceExpressionType.IsAdjacentNodesViaOutgoing)
                && !(type == SequenceExpressionType.IsIncidentEdges || type == SequenceExpressionType.IsIncomingEdges || type == SequenceExpressionType.IsOutgoingEdges))
            {
                throw new Exception("Internal failure, isAdjacent/isIncident with wrong type");
            }
        }

        protected SequenceExpressionIsAdjacentIncident(SequenceExpressionIsAdjacentIncident that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            EndElement = that.EndElement.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionIsAdjacentIncident(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsAdjacentNodes || SequenceExpressionType == SequenceExpressionType.IsAdjacentNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsAdjacentNodesViaOutgoing)
                {
                    if(TypesHelper.GetNodeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "node type", EndElement.Type(env));
                }
                else
                {
                    if(TypesHelper.GetEdgeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "edge type", EndElement.Type(env));
                }
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            IGraphElement endElement = (IGraphElement)EndElement.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.IsAdjacentNodes:
                if(EmitProfiling)
                    return GraphHelper.IsAdjacent(sourceNode, (INode)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsAdjacent(sourceNode, (INode)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsAdjacentNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.IsAdjacentIncoming(sourceNode, (INode)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsAdjacentIncoming(sourceNode, (INode)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.IsAdjacentOutgoing(sourceNode, (INode)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsAdjacentOutgoing(sourceNode, (INode)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsIncidentEdges:
                if(EmitProfiling)
                    return GraphHelper.IsIncident(sourceNode, (IEdge)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsIncident(sourceNode, (IEdge)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsIncomingEdges:
                if(EmitProfiling)
                    return GraphHelper.IsIncoming(sourceNode, (IEdge)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsIncoming(sourceNode, (IEdge)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsOutgoingEdges:
                if(EmitProfiling)
                    return GraphHelper.IsOutgoing(sourceNode, (IEdge)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsOutgoing(sourceNode, (IEdge)endElement, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            EndElement.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                yield return EndElement;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.IsAdjacentNodes: return "isAdjacent";
                case SequenceExpressionType.IsAdjacentNodesViaIncoming: return "isAdjacentIncoming";
                case SequenceExpressionType.IsAdjacentNodesViaOutgoing: return "isAdjacentOutgoing";
                case SequenceExpressionType.IsIncidentEdges: return "isIncident";
                case SequenceExpressionType.IsIncomingEdges: return "isIncoming";
                case SequenceExpressionType.IsOutgoingEdges: return "isOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + "," + EndElement.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionIsReachable : SequenceExpressionGraphQuery
    {
        public readonly SequenceExpression SourceNode;
        public readonly SequenceExpression EndElement;
        public readonly SequenceExpression EdgeType;
        public readonly SequenceExpression OppositeNodeType;

        public SequenceExpressionIsReachable(SequenceExpression sourceNode, SequenceExpression endElement, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EndElement = endElement;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.IsReachableNodes || type == SequenceExpressionType.IsReachableNodesViaIncoming || type == SequenceExpressionType.IsReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.IsReachableEdges || type == SequenceExpressionType.IsReachableEdgesViaIncoming || type == SequenceExpressionType.IsReachableEdgesViaOutgoing))
            {
                throw new Exception("Internal failure, reachable with wrong type");
            }
        }

        protected SequenceExpressionIsReachable(SequenceExpressionIsReachable that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            SourceNode = that.SourceNode.CopyExpression(originalToCopy, procEnv);
            EndElement = that.EndElement.CopyExpression(originalToCopy, procEnv);
            if(that.EdgeType != null)
                EdgeType = that.EdgeType.CopyExpression(originalToCopy, procEnv);
            if(that.OppositeNodeType != null)
                OppositeNodeType = that.OppositeNodeType.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionIsReachable(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(TypesHelper.GetNodeType(SourceNode.Type(env), env.Model) == null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsReachableNodes || SequenceExpressionType == SequenceExpressionType.IsReachableNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsReachableNodesViaOutgoing)
                {
                    if(TypesHelper.GetNodeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "node type", EndElement.Type(env));
                }
                else
                {
                    if(TypesHelper.GetEdgeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "edge type", EndElement.Type(env));
                }
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            IGraphElement endElement = (IGraphElement)EndElement.Evaluate(procEnv);
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);
            NodeType nodeType = GetNodeType(procEnv, OppositeNodeType, FunctionSymbol);

            switch(SequenceExpressionType)
            {
            case SequenceExpressionType.IsReachableNodes:
                if(EmitProfiling)
                    return GraphHelper.IsReachable(procEnv.Graph, sourceNode, (INode)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsReachable(procEnv.Graph, sourceNode, (INode)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsReachableNodesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.IsReachableIncoming(procEnv.Graph, sourceNode, (INode)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsReachableIncoming(procEnv.Graph, sourceNode, (INode)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsReachableNodesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.IsReachableOutgoing(procEnv.Graph, sourceNode, (INode)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsReachableOutgoing(procEnv.Graph, sourceNode, (INode)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsReachableEdges:
                if(EmitProfiling)
                    return GraphHelper.IsReachableEdges(procEnv.Graph, sourceNode, (IEdge)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsReachableEdges(procEnv.Graph, sourceNode, (IEdge)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsReachableEdgesViaIncoming:
                if(EmitProfiling)
                    return GraphHelper.IsReachableEdgesIncoming(procEnv.Graph, sourceNode, (IEdge)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsReachableEdgesIncoming(procEnv.Graph, sourceNode, (IEdge)endElement, edgeType, nodeType);
            case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                if(EmitProfiling)
                    return GraphHelper.IsReachableEdgesOutgoing(procEnv.Graph, sourceNode, (IEdge)endElement, edgeType, nodeType, procEnv);
                else
                    return GraphHelper.IsReachableEdgesOutgoing(procEnv.Graph, sourceNode, (IEdge)endElement, edgeType, nodeType);
            default:
                return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            SourceNode.GetLocalVariables(variables, constructors);
            EndElement.GetLocalVariables(variables, constructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, constructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return SourceNode;
                yield return EndElement;
                if(EdgeType != null)
                    yield return EdgeType;
                if(OppositeNodeType != null)
                    yield return OppositeNodeType;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string FunctionSymbol
        {
            get
            {
                switch(SequenceExpressionType)
                {
                case SequenceExpressionType.IsReachableNodes: return "isReachable";
                case SequenceExpressionType.IsReachableNodesViaIncoming: return "isReachableIncoming";
                case SequenceExpressionType.IsReachableNodesViaOutgoing: return "isReachableOutgoing";
                case SequenceExpressionType.IsReachableEdges: return "isReachableEdges";
                case SequenceExpressionType.IsReachableEdgesViaIncoming: return "isReachableEdgesIncoming";
                case SequenceExpressionType.IsReachableEdgesViaOutgoing: return "isReachableEdgesOutgoing";
                default: return "INTERNAL FAILURE!";
                }
            }
        }

        public override string Symbol
        {
            get { return FunctionSymbol + "(" + SourceNode.Symbol + "," + EndElement.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionInducedSubgraph : SequenceExpression
    {
        public readonly SequenceExpression NodeSet;

        public SequenceExpressionInducedSubgraph(SequenceExpression nodeSet)
            : base(SequenceExpressionType.InducedSubgraph)
        {
            NodeSet = nodeSet;
        }

        protected SequenceExpressionInducedSubgraph(SequenceExpressionInducedSubgraph that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            NodeSet = that.NodeSet.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionInducedSubgraph(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeSet.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!NodeSet.Type(env).StartsWith("set<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Node> type", NodeSet.Type(env));
            if(TypesHelper.ExtractSrc(NodeSet.Type(env))!="Node")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Node> type", NodeSet.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object nodeSet = NodeSet.Evaluate(procEnv);
            return GraphHelper.InducedSubgraph((IDictionary<INode, SetValueType>)nodeSet, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            NodeSet.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return NodeSet; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "inducedSubgraph(" + NodeSet.Symbol + ")"; }
        }
    }

    public class SequenceExpressionDefinedSubgraph : SequenceExpression
    {
        public readonly SequenceExpression EdgeSet;

        public SequenceExpressionDefinedSubgraph(SequenceExpression edgeSet)
            : base(SequenceExpressionType.DefinedSubgraph)
        {
            EdgeSet = edgeSet;
        }

        protected SequenceExpressionDefinedSubgraph(SequenceExpressionDefinedSubgraph that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            EdgeSet = that.EdgeSet.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionDefinedSubgraph(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeSet.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!EdgeSet.Type(env).StartsWith("set<"))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Edge> type", EdgeSet.Type(env));
            if(TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "AEdge"
                && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "Edge"
                && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "UEdge")
            {
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Edge> type", EdgeSet.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object edgeSet = EdgeSet.Evaluate(procEnv);
            return GraphHelper.DefinedSubgraph((IDictionary)edgeSet, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            EdgeSet.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return EdgeSet; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "definedSubgraph(" + EdgeSet.Symbol + ")"; }
        }
    }

    public class SequenceExpressionEqualsAny : SequenceExpression
    {
        public readonly SequenceExpression Subgraph;
        public readonly SequenceExpression SubgraphSet;
        public readonly bool IncludingAttributes;

        public SequenceExpressionEqualsAny(SequenceExpression subgraph, SequenceExpression subgraphSet, bool includingAttributes)
            : base(SequenceExpressionType.EqualsAny)
        {
            Subgraph = subgraph;
            SubgraphSet = subgraphSet;
            IncludingAttributes = includingAttributes;
        }

        protected SequenceExpressionEqualsAny(SequenceExpressionEqualsAny that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Subgraph = that.Subgraph.CopyExpression(originalToCopy, procEnv);
            SubgraphSet = that.SubgraphSet.CopyExpression(originalToCopy, procEnv);
            IncludingAttributes = that.IncludingAttributes;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionEqualsAny(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Subgraph.Type(env) != "")
            {
                if(Subgraph.Type(env) != "graph")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", first argument", "graph type", Subgraph.Type(env));
            }

            if(SubgraphSet.Type(env) != "")
            {
                if(!SubgraphSet.Type(env).StartsWith("set<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
                if(TypesHelper.ExtractSrc(SubgraphSet.Type(env)) != "graph")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object subgraph = Subgraph.Evaluate(procEnv);
            object subgraphSet = SubgraphSet.Evaluate(procEnv);
            return GraphHelper.EqualsAny((IGraph)subgraph, (IDictionary<IGraph, SetValueType>)subgraphSet, IncludingAttributes);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Subgraph.GetLocalVariables(variables, constructors);
            SubgraphSet.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Subgraph;
                yield return SubgraphSet;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            { 
                if(IncludingAttributes)
                    return "equalsAny(" + Subgraph.Symbol + ", " + SubgraphSet.Symbol + ")"; 
                else
                    return "equalsAnyStructurally(" + Subgraph.Symbol + ", " + SubgraphSet.Symbol + ")"; 
            }
        }
    }

    public class SequenceExpressionGetEquivalent : SequenceExpression
    {
        public readonly SequenceExpression Subgraph;
        public readonly SequenceExpression SubgraphSet;
        public readonly bool IncludingAttributes;

        public SequenceExpressionGetEquivalent(SequenceExpression subgraph, SequenceExpression subgraphSet, bool includingAttributes)
            : base(SequenceExpressionType.GetEquivalent)
        {
            Subgraph = subgraph;
            SubgraphSet = subgraphSet;
            IncludingAttributes = includingAttributes;
        }

        protected SequenceExpressionGetEquivalent(SequenceExpressionGetEquivalent that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Subgraph = that.Subgraph.CopyExpression(originalToCopy, procEnv);
            SubgraphSet = that.SubgraphSet.CopyExpression(originalToCopy, procEnv);
            IncludingAttributes = that.IncludingAttributes;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionGetEquivalent(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Subgraph.Type(env) != "")
            {
                if(Subgraph.Type(env) != "graph")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", first argument", "graph type", Subgraph.Type(env));
            }

            if(SubgraphSet.Type(env) != "")
            {
                if(!SubgraphSet.Type(env).StartsWith("set<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
                if(TypesHelper.ExtractSrc(SubgraphSet.Type(env)) != "graph")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object subgraph = Subgraph.Evaluate(procEnv);
            object subgraphSet = SubgraphSet.Evaluate(procEnv);
            return GraphHelper.GetEquivalent((IGraph)subgraph, (IDictionary<IGraph, SetValueType>)subgraphSet, IncludingAttributes);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Subgraph.GetLocalVariables(variables, constructors);
            SubgraphSet.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Subgraph;
                yield return SubgraphSet;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                if(IncludingAttributes)
                    return "getEquivalent(" + Subgraph.Symbol + ", " + SubgraphSet.Symbol + ")";
                else
                    return "getEquivalentStructurally(" + Subgraph.Symbol + ", " + SubgraphSet.Symbol + ")";
            }
        }
    }

    public class SequenceExpressionCanonize : SequenceExpression
    {
        public readonly SequenceExpression Graph;

        public SequenceExpressionCanonize(SequenceExpression graph)
            : base(SequenceExpressionType.Canonize)
        {
            Graph = graph;
        }

        protected SequenceExpressionCanonize(SequenceExpressionCanonize that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Graph = that.Graph.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCanonize(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Graph.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(Graph.Type(env) != "graph")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "graph type", Graph.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object graph = Graph.Evaluate(procEnv);
            return ((IGraph)graph).Canonize();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Graph.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Graph; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "canonize(" + Graph.Symbol + ")"; }
        }
    }

    public class SequenceExpressionNameof : SequenceExpression
    {
        public readonly SequenceExpression NamedEntity;

        public SequenceExpressionNameof(SequenceExpression namedEntity)
            : base(SequenceExpressionType.Nameof)
        {
            NamedEntity = namedEntity; // might be null
        }

        protected SequenceExpressionNameof(SequenceExpressionNameof that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            if(that.NamedEntity != null)
                NamedEntity = that.NamedEntity.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionNameof(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(NamedEntity != null)
            {
                base.Check(env); // check children

                if(!TypesHelper.IsSameOrSubtype(NamedEntity.Type(env), "Node", env.Model)
                    && !TypesHelper.IsSameOrSubtype(NamedEntity.Type(env), "AEdge", env.Model)
                    && !TypesHelper.IsSameOrSubtype(NamedEntity.Type(env), "graph", env.Model))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge or graph type", NamedEntity.Type(env));
                }
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(NamedEntity != null)
                return GraphHelper.Nameof(NamedEntity.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.Nameof(null, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(NamedEntity != null)
                NamedEntity.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(NamedEntity != null)
                    yield return NamedEntity;
                else yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "nameof(" + (NamedEntity != null ? NamedEntity.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionUniqueof : SequenceExpression
    {
        public readonly SequenceExpression UniquelyIdentifiedEntity;

        public SequenceExpressionUniqueof(SequenceExpression uniquelyIdentifiedEntity)
            : base(SequenceExpressionType.Uniqueof)
        {
            UniquelyIdentifiedEntity = uniquelyIdentifiedEntity; // might be null
        }

        protected SequenceExpressionUniqueof(SequenceExpressionUniqueof that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            if(that.UniquelyIdentifiedEntity != null)
                UniquelyIdentifiedEntity = that.UniquelyIdentifiedEntity.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionUniqueof(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(UniquelyIdentifiedEntity == null)
                return "int";
            if(UniquelyIdentifiedEntity.Type(env) == "")
                return "";
            if(TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "Object", env.Model))
                return "long";
            else
                return "int";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(UniquelyIdentifiedEntity != null)
            {
                base.Check(env); // check children

                if(!TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "Node", env.Model)
                    && !TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "AEdge", env.Model)
                    && !TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "graph", env.Model)
                    && !TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "Object", env.Model))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge or graph or internal object type", UniquelyIdentifiedEntity.Type(env));
                }
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(UniquelyIdentifiedEntity != null)
                return GraphHelper.Uniqueof(UniquelyIdentifiedEntity.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.Uniqueof(null, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(UniquelyIdentifiedEntity != null)
                UniquelyIdentifiedEntity.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(UniquelyIdentifiedEntity != null)
                    yield return UniquelyIdentifiedEntity;
                else yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "uniqueof(" + (UniquelyIdentifiedEntity != null ? UniquelyIdentifiedEntity.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionTypeof : SequenceExpression
    {
        public readonly SequenceExpression Entity;

        public SequenceExpressionTypeof(SequenceExpression entity)
            : base(SequenceExpressionType.Typeof)
        {
            Entity = entity;
        }

        protected SequenceExpressionTypeof(SequenceExpressionTypeof that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Entity = that.Entity.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionTypeof(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "string"; // typeof in the sequences returns the type name
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object entity = Entity.Evaluate(procEnv);
            return TypesHelper.XgrsTypeOfConstant(entity, procEnv.Graph.Model);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Entity.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Entity; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "typeof(" + Entity.Symbol + ")"; }
        }
    }

    public class SequenceExpressionExistsFile : SequenceExpression
    {
        public readonly SequenceExpression Path;

        public SequenceExpressionExistsFile(SequenceExpression path)
            : base(SequenceExpressionType.ExistsFile)
        {
            Path = path;
        }

        protected SequenceExpressionExistsFile(SequenceExpressionExistsFile that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Path = that.Path.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionExistsFile(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Path.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!TypesHelper.IsSameOrSubtype(Path.Type(env), "string", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Path.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object path = Path.Evaluate(procEnv);
            return File.Exists((string)path);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Path.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Path; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "File::existsFile(" + Path.Symbol + ")"; }
        }
    }

    public class SequenceExpressionImport : SequenceExpression
    {
        public readonly SequenceExpression Path;

        public SequenceExpressionImport(SequenceExpression path)
            : base(SequenceExpressionType.Import)
        {
            Path = path;
        }

        protected SequenceExpressionImport(SequenceExpressionImport that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Path = that.Path.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionImport(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Path.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!TypesHelper.IsSameOrSubtype(Path.Type(env), "string", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "string type", Path.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object path = Path.Evaluate(procEnv);
            return GraphHelper.Import(path, procEnv.Backend, procEnv.Graph.Model);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Path.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return Path; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "File::import(" + Path.Symbol + ")"; }
        }
    }

    public class SequenceExpressionCopy : SequenceExpression
    {
        public readonly SequenceExpression ObjectToBeCopied;
        public readonly bool Deep;


        public SequenceExpressionCopy(SequenceExpression objectToBeCopied, bool deep)
            : base(SequenceExpressionType.Copy)
        {
            ObjectToBeCopied = objectToBeCopied;
            Deep = deep;
        }

        protected SequenceExpressionCopy(SequenceExpressionCopy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            ObjectToBeCopied = that.ObjectToBeCopied.CopyExpression(originalToCopy, procEnv);
            Deep = that.Deep;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionCopy(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(ObjectToBeCopied.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(Deep)
            {
                if(!TypesHelper.IsSameOrSubtype(ObjectToBeCopied.Type(env), "graph", env.Model)
                    && (TypesHelper.ExtractSrc(ObjectToBeCopied.Type(env)) == null || TypesHelper.ExtractDst(ObjectToBeCopied.Type(env)) == null)
                    && env.Model.ObjectModel.GetType(ObjectToBeCopied.Type(env)) == null
                    && env.Model.TransientObjectModel.GetType(ObjectToBeCopied.Type(env)) == null)
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "graph type or container type or object class type or transient object class type", ObjectToBeCopied.Type(env));
                }
            }
            else
            {
                if(!ObjectToBeCopied.Type(env).StartsWith("match<")
                    && (TypesHelper.ExtractSrc(ObjectToBeCopied.Type(env)) == null || TypesHelper.ExtractDst(ObjectToBeCopied.Type(env)) == null)
                    && env.Model.ObjectModel.GetType(ObjectToBeCopied.Type(env)) == null
                    && env.Model.TransientObjectModel.GetType(ObjectToBeCopied.Type(env)) == null)
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "match type or container type or object class type or transient object class type", ObjectToBeCopied.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ObjectToBeCopied.Type(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(Deep)
            {
                object toBeCopied = ObjectToBeCopied.Evaluate(procEnv);
                if(toBeCopied is IGraph)
                    return GraphHelper.Copy((IGraph)toBeCopied);
                else if(toBeCopied is IObject)
                    return ((IObject)toBeCopied).Copy(procEnv.Graph, new Dictionary<object, object>());
                else if(toBeCopied is ITransientObject)
                    return ((ITransientObject)toBeCopied).Copy(procEnv.Graph, new Dictionary<object, object>());
                else
                    return ContainerHelper.Copy(toBeCopied, procEnv.Graph, new Dictionary<object, object>());
            }
            else
            {
                object toBeCloned = ObjectToBeCopied.Evaluate(procEnv);
                if(toBeCloned is IMatch)
                    return ((IMatch)toBeCloned).Clone();
                else if(toBeCloned is IObject)
                    return ((IObject)toBeCloned).Clone(procEnv.Graph);
                else if(toBeCloned is ITransientObject)
                    return ((ITransientObject)toBeCloned).Clone();
                else
                    return ContainerHelper.Clone(toBeCloned);
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ObjectToBeCopied.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return ObjectToBeCopied; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return (Deep ? "copy(" : "clone(") + ObjectToBeCopied.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathMin : SequenceExpression
    {
        public readonly SequenceExpression Left;
        public readonly SequenceExpression Right;

        public SequenceExpressionMathMin(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.MathMin)
        {
            Left = left;
            Right = right;
        }

        protected SequenceExpressionMathMin(SequenceExpressionMathMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Left = that.Left.CopyExpression(originalToCopy, procEnv);
            Right = that.Right.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathMin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return Left.Type(env);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Left.Type(env) != "")
            {
                if(!TypesHelper.IsNumericType(Left.Type(env)))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "numeric type", Left.Type(env));
            }

            if(Right.Type(env) != "")
            {
                if(!TypesHelper.IsNumericType(Right.Type(env)))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "numeric type", Right.Type(env));
            }

            if(Left.Type(env) != "" && Right.Type(env) != "")
            {
                if(Left.Type(env) != Right.Type(env))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "left and right must be the same numeric type", Left.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return MathHelper.Min(Left.Evaluate(procEnv), Right.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Left.GetLocalVariables(variables, constructors);
            Right.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Left;
                yield return Right;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::min(" + Left.Symbol + "," + Right.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathMax : SequenceExpression
    {
        public readonly SequenceExpression Left;
        public readonly SequenceExpression Right;

        public SequenceExpressionMathMax(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.MathMax)
        {
            Left = left;
            Right = right;
        }

        protected SequenceExpressionMathMax(SequenceExpressionMathMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Left = that.Left.CopyExpression(originalToCopy, procEnv);
            Right = that.Right.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathMax(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return Left.Type(env);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Left.Type(env) != "")
            {
                if(!TypesHelper.IsNumericType(Left.Type(env)))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "numeric type", Left.Type(env));
            }

            if(Right.Type(env) != "")
            {
                if(!TypesHelper.IsNumericType(Right.Type(env)))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "numeric type", Right.Type(env));
            }

            if(Left.Type(env) != "" && Right.Type(env) != "")
            {
                if(Left.Type(env) != Right.Type(env))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "left and right must be the same numeric type", Left.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return MathHelper.Max(Left.Evaluate(procEnv), Right.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Left.GetLocalVariables(variables, constructors);
            Right.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Left;
                yield return Right;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::max(" + Left.Symbol + "," + Right.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathAbs : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathAbs(SequenceExpression argument)
            : base(SequenceExpressionType.MathAbs)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathAbs(SequenceExpressionMathAbs that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathAbs(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return Argument.Type(env);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(!TypesHelper.IsNumericType(Argument.Type(env)))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "numeric type", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return MathHelper.Abs(Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::abs(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathCeil : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathCeil(SequenceExpression argument)
            : base(SequenceExpressionType.MathCeil)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathCeil(SequenceExpressionMathCeil that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathCeil(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Ceiling((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::ceil(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathFloor : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathFloor(SequenceExpression argument)
            : base(SequenceExpressionType.MathFloor)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathFloor(SequenceExpressionMathFloor that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathFloor(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Floor((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::floor(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathRound : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathRound(SequenceExpression argument)
            : base(SequenceExpressionType.MathRound)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathRound(SequenceExpressionMathRound that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathRound(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Round((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::round(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathTruncate : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathTruncate(SequenceExpression argument)
            : base(SequenceExpressionType.MathTruncate)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathTruncate(SequenceExpressionMathTruncate that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathTruncate(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Truncate((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::truncate(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathSqr : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathSqr(SequenceExpression argument)
            : base(SequenceExpressionType.MathSqr)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathSqr(SequenceExpressionMathSqr that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathSqr(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return MathHelper.Sqr((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::sqr(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathSqrt : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathSqrt(SequenceExpression argument)
            : base(SequenceExpressionType.MathSqrt)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathSqrt(SequenceExpressionMathSqrt that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathSqrt(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Sqrt((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::sqrt(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathPow : SequenceExpression
    {
        public readonly SequenceExpression Left;
        public readonly SequenceExpression Right;

        public SequenceExpressionMathPow(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.MathPow)
        {
            if(right == null)
            {
                Right = left;
            }
            else
            {
                Left = left;
                Right = right;
            }
        }

        protected SequenceExpressionMathPow(SequenceExpressionMathPow that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            if(Left != null)
                Left = that.Left.CopyExpression(originalToCopy, procEnv);
            Right = that.Right.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathPow(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Left != null)
            {
                if(Left.Type(env) != "")
                {
                    if(Left.Type(env) != "double")
                        throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Left.Type(env));
                }
            }

            if(Right.Type(env) != "")
            {
                if(Right.Type(env) != "double")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Right.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(Left != null)
                return Math.Pow((double)Left.Evaluate(procEnv), (double)Right.Evaluate(procEnv));
            else
                return Math.Exp((double)Right.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(Left != null)
                Left.GetLocalVariables(variables, constructors);
            Right.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                if(Left != null)
                    yield return Left;
                yield return Right;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::pow(" + (Left != null ? Left.Symbol + "," : "") + Right.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathLog : SequenceExpression
    {
        public readonly SequenceExpression Left;
        public readonly SequenceExpression Right;

        public SequenceExpressionMathLog(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.MathLog)
        {
            Left = left;
            Right = right;
        }

        protected SequenceExpressionMathLog(SequenceExpressionMathLog that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Left = that.Left.CopyExpression(originalToCopy, procEnv);
            if(Right != null)
                Right = that.Right.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathLog(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Left.Type(env) != "")
            {
                if(Left.Type(env) != "double")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Left.Type(env));
            }

            if(Right != null)
            {
                if(Right.Type(env) != "")
                {
                    if(Right.Type(env) != "double")
                        throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Right.Type(env));
                }
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(Right != null)
                return Math.Log((double)Left.Evaluate(procEnv), (double)Right.Evaluate(procEnv));
            else
                return Math.Log((double)Left.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Left.GetLocalVariables(variables, constructors);
            if(Right != null)
                Right.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Left;
                if(Right != null)
                    yield return Right;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::log(" + Left.Symbol + (Right != null ? "," + Right.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionMathSgn : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathSgn(SequenceExpression argument)
            : base(SequenceExpressionType.MathSgn)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathSgn(SequenceExpressionMathSgn that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathSgn(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Sign((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::sgn(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathSin : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathSin(SequenceExpression argument)
            : base(SequenceExpressionType.MathSin)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathSin(SequenceExpressionMathSin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathSin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Sin((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::sin(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathCos : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathCos(SequenceExpression argument)
            : base(SequenceExpressionType.MathCos)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathCos(SequenceExpressionMathCos that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathCos(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Cos((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::cos(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathTan : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathTan(SequenceExpression argument)
            : base(SequenceExpressionType.MathTan)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathTan(SequenceExpressionMathTan that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathTan(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Tan((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::tan(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathArcSin : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathArcSin(SequenceExpression argument)
            : base(SequenceExpressionType.MathArcSin)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathArcSin(SequenceExpressionMathArcSin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathArcSin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Asin((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::arcsin(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathArcCos : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathArcCos(SequenceExpression argument)
            : base(SequenceExpressionType.MathArcCos)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathArcCos(SequenceExpressionMathArcCos that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathArcCos(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Acos((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::arccos(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathArcTan : SequenceExpression
    {
        public readonly SequenceExpression Argument;

        public SequenceExpressionMathArcTan(SequenceExpression argument)
            : base(SequenceExpressionType.MathArcTan)
        {
            Argument = argument;
        }

        protected SequenceExpressionMathArcTan(SequenceExpressionMathArcTan that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Argument = that.Argument.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathArcTan(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Argument.Type(env) == "")
                return;

            if(Argument.Type(env) != "double")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "double", Argument.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.Atan((double)Argument.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Argument.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return Argument;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::arctan(" + Argument.Symbol + ")"; }
        }
    }

    public class SequenceExpressionMathPi : SequenceExpression
    {
        public SequenceExpressionMathPi()
            : base(SequenceExpressionType.MathPi)
        {
        }

        protected SequenceExpressionMathPi(SequenceExpressionMathPi that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathPi(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.PI;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::pi()"; }
        }
    }

    public class SequenceExpressionMathE : SequenceExpression
    {
        public SequenceExpressionMathE()
            : base(SequenceExpressionType.MathE)
        {
        }

        protected SequenceExpressionMathE(SequenceExpressionMathE that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathE(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Math.E;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::e()"; }
        }
    }

    public class SequenceExpressionMathByteMin : SequenceExpression
    {
        public SequenceExpressionMathByteMin()
            : base(SequenceExpressionType.MathByteMin)
        {
        }

        protected SequenceExpressionMathByteMin(SequenceExpressionMathByteMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathByteMin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "byte";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return SByte.MinValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::byteMin()"; }
        }
    }

    public class SequenceExpressionMathByteMax : SequenceExpression
    {
        public SequenceExpressionMathByteMax()
            : base(SequenceExpressionType.MathByteMax)
        {
        }

        protected SequenceExpressionMathByteMax(SequenceExpressionMathByteMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathByteMax(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "byte";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return SByte.MaxValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::byteMax()"; }
        }
    }

    public class SequenceExpressionMathShortMin : SequenceExpression
    {
        public SequenceExpressionMathShortMin()
            : base(SequenceExpressionType.MathShortMin)
        {
        }

        protected SequenceExpressionMathShortMin(SequenceExpressionMathShortMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathShortMin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "short";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Int16.MinValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::shortMin()"; }
        }
    }

    public class SequenceExpressionMathShortMax : SequenceExpression
    {
        public SequenceExpressionMathShortMax()
            : base(SequenceExpressionType.MathShortMax)
        {
        }

        protected SequenceExpressionMathShortMax(SequenceExpressionMathShortMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathShortMax(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "short";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Int16.MaxValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::shortMax()"; }
        }
    }

    public class SequenceExpressionMathIntMin : SequenceExpression
    {
        public SequenceExpressionMathIntMin()
            : base(SequenceExpressionType.MathIntMin)
        {
        }

        protected SequenceExpressionMathIntMin(SequenceExpressionMathIntMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathIntMin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Int32.MinValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::intMin()"; }
        }
    }

    public class SequenceExpressionMathIntMax : SequenceExpression
    {
        public SequenceExpressionMathIntMax()
            : base(SequenceExpressionType.MathIntMax)
        {
        }

        protected SequenceExpressionMathIntMax(SequenceExpressionMathIntMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathIntMax(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Int32.MaxValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::intMax()"; }
        }
    }

    public class SequenceExpressionMathLongMin : SequenceExpression
    {
        public SequenceExpressionMathLongMin()
            : base(SequenceExpressionType.MathLongMin)
        {
        }

        protected SequenceExpressionMathLongMin(SequenceExpressionMathLongMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathLongMin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "long";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Int64.MinValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::longMin()"; }
        }
    }

    public class SequenceExpressionMathLongMax : SequenceExpression
    {
        public SequenceExpressionMathLongMax()
            : base(SequenceExpressionType.MathLongMax)
        {
        }

        protected SequenceExpressionMathLongMax(SequenceExpressionMathLongMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathLongMax(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "long";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Int64.MaxValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::longMax()"; }
        }
    }

    public class SequenceExpressionMathFloatMin : SequenceExpression
    {
        public SequenceExpressionMathFloatMin()
            : base(SequenceExpressionType.MathFloatMin)
        {
        }

        protected SequenceExpressionMathFloatMin(SequenceExpressionMathFloatMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathFloatMin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "float";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Single.MinValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::floatMin()"; }
        }
    }

    public class SequenceExpressionMathFloatMax : SequenceExpression
    {
        public SequenceExpressionMathFloatMax()
            : base(SequenceExpressionType.MathFloatMax)
        {
        }

        protected SequenceExpressionMathFloatMax(SequenceExpressionMathFloatMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathFloatMax(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "float";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Single.MaxValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::floatMax()"; }
        }
    }

    public class SequenceExpressionMathDoubleMin : SequenceExpression
    {
        public SequenceExpressionMathDoubleMin()
            : base(SequenceExpressionType.MathDoubleMin)
        {
        }

        protected SequenceExpressionMathDoubleMin(SequenceExpressionMathDoubleMin that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathDoubleMin(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Double.MinValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::doubleMin()"; }
        }
    }

    public class SequenceExpressionMathDoubleMax : SequenceExpression
    {
        public SequenceExpressionMathDoubleMax()
            : base(SequenceExpressionType.MathDoubleMax)
        {
        }

        protected SequenceExpressionMathDoubleMax(SequenceExpressionMathDoubleMax that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMathDoubleMax(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return Double.MaxValue;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Math::doubleMax()"; }
        }
    }

    public class SequenceExpressionRuleQuery : SequenceExpression, RuleInvocation, IPatternMatchingConstruct, ISequenceSpecial
    {
        public readonly SequenceRuleAllCall RuleCall;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.RuleQuery; }
        }

        public String Name
        {
            get { return RuleCall.Name; }
        }

        public String Package
        {
            get { return RuleCall.Package; }
        }

        public String PackagePrefixedName
        {
            get { return RuleCall.PackagePrefixedName; }
        }

        public SequenceVariable Subgraph
        {
            get { return RuleCall.Subgraph; }
        }

        public bool Special
        {
            get { return RuleCall.Special; }
            set { RuleCall.Special = value; }
        }

        public SequenceExpressionRuleQuery(SequenceRuleAllCall ruleCall)
            : base(SequenceExpressionType.RuleQuery)
        {
            RuleCall = ruleCall;
        }

        protected SequenceExpressionRuleQuery(SequenceExpressionRuleQuery that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            RuleCall = that.RuleCall;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionRuleQuery(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            //children are arguments, those are checked in environment with the call

            env.CheckRuleCall(RuleCall);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<match<" + RuleCall.Name + ">>";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            SequenceRuleAllCallInterpreted ruleCallInterpreted = (SequenceRuleAllCallInterpreted)RuleCall;
            FireBeginExecutionEvent(procEnv);
            FireEnteringSequenceEvent(procEnv);

            IMatches matches;
            List<IMatch> matchesList = ruleCallInterpreted.MatchForQuery(procEnv, out matches);

            FireMatchedAfterFilteringEvent(procEnv, matches, ruleCallInterpreted.Special);
            FireFinishedEvent(procEnv, matches, ruleCallInterpreted.Special);

            FireExitingSequenceEvent(procEnv);
            FireEndExecutionEvent(procEnv, matchesList);
            return matchesList;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            RuleCall.GetLocalVariables(variables, constructors, null);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                foreach(SequenceExpression argument in RuleCall.ArgumentExpressions)
                {
                    yield return argument;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                return RuleCall.Symbol;
            }
        }
    }

    public class SequenceExpressionMultiRuleQuery : SequenceExpression, IPatternMatchingConstruct
    {
        public readonly SequenceMultiRuleAllCall MultiRuleCall;
        public readonly String MatchClass;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.MultiRuleQuery; }
        }

        public SequenceExpressionMultiRuleQuery(SequenceMultiRuleAllCall multiRuleCall, String matchClass)
            : base(SequenceExpressionType.MultiRuleQuery)
        {
            MultiRuleCall = multiRuleCall;
            MatchClass = matchClass;
        }

        protected SequenceExpressionMultiRuleQuery(SequenceExpressionMultiRuleQuery that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            MultiRuleCall = that.MultiRuleCall;
            MatchClass = that.MatchClass;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMultiRuleQuery(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            List<SequenceRuleCall> RuleCalls = new List<SequenceRuleCall>();
            foreach(Sequence seqChild in MultiRuleCall.Sequences)
            {
                seqChild.Check(env);
                if(seqChild is SequenceRuleAllCall)
                    throw new Exception("Sequence MultiRuleAllCall (e.g. [[r1,r2(x),(y)=r3]] can't contain a rule all call (e.g. [r4]");
                if(seqChild is SequenceRuleCountAllCall)
                    throw new Exception("Sequence MultiRuleAllCall (e.g. [[r1,r2(x),(y)=r3]] can't contain a rule count all call (e.g. count[r4] => ct");
                if(((SequenceRuleCall)seqChild).Subgraph != null)
                    throw new Exception("Sequence MultiRuleAllCall (e.g. [[r1,r2(x),(y)=r3]]  can't contain a call with subgraph prefix (e.g. sg.r4)");
                RuleCalls.Add((SequenceRuleCall)seqChild);
            }

            env.CheckMatchClassFilterCalls(MultiRuleCall.Filters, RuleCalls);
            foreach(SequenceRuleCall ruleCall in RuleCalls)
            {
                if(!env.IsRuleImplementingMatchClass(ruleCall.PackagePrefixedName, MatchClass))
                    throw new SequenceParserExceptionMatchClassNotImplemented(MatchClass, ruleCall.PackagePrefixedName);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<match<class " + MatchClass + ">>";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            FireBeginExecutionEvent(procEnv);
            FireEnteringSequenceEvent(procEnv);

            IMatches[] MatchesArray;
            List<IMatch> MatchList;
            Dictionary<IMatch, int> MatchToConstructIndex;
            MultiRuleCall.MatchAll(procEnv,
                out MatchesArray, out MatchList, out MatchToConstructIndex);
            // MatchAll clones single matches because of potentially multiple calls of same rule, overall list is created anew anyway
            // - without that cloning an additional clone would be needed here like for the single rule query, 
            // as the maches array must be available (at least) through continued sequence expression processing
            // (and a new call could be made during that time, example: [[?r]] + [[?r]])

            foreach(SequenceFilterCallBase filter in MultiRuleCall.Filters)
            {
                if(filter is SequenceFilterCallLambdaExpressionInterpreted)
                {
                    SequenceFilterCallLambdaExpressionInterpreted filterInterpreted = (SequenceFilterCallLambdaExpressionInterpreted)filter;
                    filterInterpreted.MatchClass.Filter(procEnv, MatchList, filterInterpreted.FilterCall);
                }
                else
                {
                    SequenceFilterCallInterpreted filterInterpreted = (SequenceFilterCallInterpreted)filter;
                    filterInterpreted.Execute(procEnv, MatchList);
                }
            }

            for(int i = 0; i < MultiRuleCall.Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)MultiRuleCall.Sequences[i];
                IMatches matches = MatchesArray[i];
                if(matches.Count == 0)
                    rule.executionState = SequenceExecutionState.Fail;
            }

            bool[] SpecialArray = new bool[MultiRuleCall.Sequences.Count];
            for(int i = 0; i < MultiRuleCall.Sequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)MultiRuleCall.Sequences[i];
                SpecialArray[i] = rule.Special;
            }
            MatchListHelper.RemoveUnavailable(MatchList, MatchesArray);
            FireMatchedAfterFilteringEvent(procEnv, MatchesArray, SpecialArray);

            FireFinishedEvent(procEnv, MatchesArray, SpecialArray);

            FireExitingSequenceEvent(procEnv);
            FireEndExecutionEvent(procEnv, MatchList);
            return MatchList;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            MultiRuleCall.GetLocalVariables(variables, constructors, null);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                foreach(Sequence rule in MultiRuleCall.Sequences)
                {
                    foreach(SequenceExpression argument in ((SequenceRuleCall)rule).ArgumentExpressions)
                    {
                        yield return argument;
                    }
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[?[");
                sb.Append(MultiRuleCall.CoreSymbolNoTestPrefix);
                sb.Append("]");
                sb.Append(MultiRuleCall.FilterSymbol);
                sb.Append("\\<class " + MatchClass + ">");
                sb.Append("]");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionMappingClause : SequenceExpression, IPatternMatchingConstruct
    {
        public readonly SequenceMultiRulePrefixedSequence MultiRulePrefixedSequence;

        public PatternMatchingConstructType ConstructType
        {
            get { return PatternMatchingConstructType.MappingClause; }
        }

        public SequenceExpressionMappingClause(SequenceMultiRulePrefixedSequence multiRulePrefixedSequence)
            : base(SequenceExpressionType.MappingClause)
        {
            MultiRulePrefixedSequence = multiRulePrefixedSequence;
        }

        protected SequenceExpressionMappingClause(SequenceExpressionMappingClause that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            MultiRulePrefixedSequence = (SequenceMultiRulePrefixedSequence)that.MultiRulePrefixedSequence.Copy(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionMappingClause(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            MultiRulePrefixedSequence.Check(env);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<graph>";
        }

        internal override void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            foreach(SequenceRulePrefixedSequence rulePrefixedSequence in MultiRulePrefixedSequence.RulePrefixedSequences)
            {
                rulePrefixedSequence.Rule.ReplaceSequenceDefinition(oldDef, newDef);
                rulePrefixedSequence.Sequence.ReplaceSequenceDefinition(oldDef, newDef);
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            List<IGraph> graphs = new List<IGraph>();

            // first get all matches of the rules
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Matching mapping multi rule prefixed sequence " + GetRuleCallString(procEnv));
#endif

            FireBeginExecutionEvent(procEnv);
            FireEnteringSequenceEvent(procEnv);

            IMatches[] MatchesArray;
            List<IMatch> MatchList;
            Dictionary<IMatch, int> MatchToConstructIndex;
            MultiRulePrefixedSequence.MatchAll(procEnv,
                out MatchesArray, out MatchList, out MatchToConstructIndex);

            foreach(SequenceFilterCallBase filter in MultiRulePrefixedSequence.Filters)
            {
                SequenceFilterCallInterpreted filterInterpreted = (SequenceFilterCallInterpreted)filter;
                filterInterpreted.Execute(procEnv, MatchList);
            }

            int matchesCount = MatchList.Count;
            if(matchesCount == 0)
            {
                FireEndExecutionEvent(procEnv, graphs);
                return graphs;
            }

#if LOG_SEQUENCE_EXECUTION
            for(int i = 0; i < matchesCount; ++i)
            {
                procEnv.Recorder.WriteLine("match " + i + ": " + MatchPrinter.ToString(MatchList[i], procEnv.Graph, ""));
            }
#endif

            // cloning already occurred to allow multiple calls of the same rule

            // apply the rule and its sequence for every match found
            int matchesTried = 0;

            for(int i = 0; i < MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule;
                IMatches matches = MatchesArray[i];
                if(matches.Count == 0)
                    rule.executionState = SequenceExecutionState.Fail;
            }

            bool[] SpecialArray = new bool[MultiRulePrefixedSequence.RulePrefixedSequences.Count];
            for(int i = 0; i < MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule;
                SpecialArray[i] = rule.Special;
            }
            MatchListHelper.RemoveUnavailable(MatchList, MatchesArray);
            FireMatchedAfterFilteringEvent(procEnv, MatchesArray, SpecialArray);

            foreach(IMatch match in MatchList)
            {
                ++matchesTried;
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Applying match " + matchesTried + "/" + matchesCount + " of " + rule.GetRuleCallString(procEnv));
                procEnv.Recorder.WriteLine("match: " + MatchPrinter.ToString(match, procEnv.Graph, ""));
#endif

                int constructIndex = MatchToConstructIndex[match];
                SequenceRuleCall rule = (SequenceRuleCall)MultiRulePrefixedSequence.RulePrefixedSequences[constructIndex].Rule;
                Sequence seq = MultiRulePrefixedSequence.RulePrefixedSequences[constructIndex].Sequence;
                IMatches matches = MatchesArray[constructIndex];

                for(int i = 0; i < MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
                {
                    SequenceRuleCall highlightingRule = (SequenceRuleCall)MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule;
                    if(i != constructIndex)
                        highlightingRule.executionState = SequenceExecutionState.Fail;
                    else
                        highlightingRule.executionState = SequenceExecutionState.Success;
                }

                IDictionary<IGraphElement, IGraphElement> oldToNewMap;
                IGraph graph = procEnv.Graph.Clone(procEnv.Graph.Name, out oldToNewMap);

                procEnv.SwitchToSubgraph(graph);

                IMatch mappedMatch = match.Clone(oldToNewMap);

#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("Before executing sequence " + rule.Id + ": " + rule.Symbol);
#endif
                rule.Rewrite(procEnv, matches, mappedMatch);
#if LOG_SEQUENCE_EXECUTION
                procEnv.Recorder.WriteLine("After executing sequence " + rule.Id + ": " + rule.Symbol + " result " + result);
#endif

                // rule applied, now execute its sequence
                bool result = seq.Apply(procEnv);

                if(matchesTried < matchesCount)
                {
                    procEnv.EndOfIteration(true, this);
                    rule.ResetExecutionState();
                    seq.ResetExecutionState();
                }
                else
                {
#if LOG_SEQUENCE_EXECUTION
                    procEnv.Recorder.WriteLine("Applying match exhausted " + rule.GetRuleCallString(procEnv));
#endif
                    procEnv.EndOfIteration(false, this);
                }

                procEnv.ReturnFromSubgraph();
                if(result)
                {
                    graphs.Add(graph);
                }
            }

            FireFinishedEvent(procEnv, MatchesArray, SpecialArray);
            FireExitingSequenceEvent(procEnv);
            FireEndExecutionEvent(procEnv, graphs);
            return graphs;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            MultiRulePrefixedSequence.GetLocalVariables(variables, constructors, null);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                foreach(SequenceRulePrefixedSequence rulePrefixedSequence in MultiRulePrefixedSequence.RulePrefixedSequences)
                {
                    foreach(SequenceExpression argument in rulePrefixedSequence.Rule.ChildrenBase)
                    {
                        yield return argument;
                    }
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get
            {
                return "[:" + MultiRulePrefixedSequence.CoreSymbol + MultiRulePrefixedSequence.FilterSymbol + ":]";
            }
        }
    }

    public class SequenceExpressionScan : SequenceExpression
    {
        public readonly String ResultType;
        public readonly SequenceExpression StringExpr;

        public SequenceExpressionScan(String resultType, SequenceExpression stringExpr)
            : base(SequenceExpressionType.Scan)
        {
            ResultType = resultType;
            StringExpr = stringExpr;
        }

        protected SequenceExpressionScan(SequenceExpressionScan that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            ResultType = that.ResultType;
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionScan(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) != "" && StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol + " string parameter", "string type", StringExpr.Type(env));

            if(ResultType == null)
                return;

            AttributeType attrType = TypesHelper.XgrsTypeToAttributeType(ResultType, env.Model);
            if(attrType.Kind == AttributeKind.InternalClassObjectAttr)
                throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type not denoting an object type", ResultType);

            if(attrType.Kind == AttributeKind.InternalClassTransientObjectAttr)
                throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type not denoting a transient object type", ResultType);

            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr || attrType.Kind == AttributeKind.DequeAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.InternalClassObjectAttr)
                    throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing an object type", ResultType);
                if(attrType.ValueType.Kind == AttributeKind.InternalClassTransientObjectAttr)
                    throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing a transient object type", ResultType);
                if(attrType.Kind == AttributeKind.MapAttr)
                {
                    if(attrType.KeyType.Kind == AttributeKind.InternalClassObjectAttr)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing an object type", ResultType);
                    if(attrType.KeyType.Kind == AttributeKind.InternalClassTransientObjectAttr)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing a transient object type", ResultType);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ResultType ?? "object";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            AttributeType attrType = TypesHelper.XgrsTypeToAttributeType(ResultType ?? "object", procEnv.Graph.Model);
            String unescaped = Unescape((string)StringExpr.Evaluate(procEnv));
            return GRSImport.Scan(attrType, unescaped, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "scan" + (ResultType != null ? "<" + ResultType + ">": "") + "(" + StringExpr.Symbol + ")"; }
        }
    }

    public class SequenceExpressionTryScan : SequenceExpression
    {
        public readonly String ResultType;
        public readonly SequenceExpression StringExpr;

        public SequenceExpressionTryScan(String resultType, SequenceExpression stringExpr)
            : base(SequenceExpressionType.TryScan)
        {
            ResultType = resultType;
            StringExpr = stringExpr;
        }

        protected SequenceExpressionTryScan(SequenceExpressionTryScan that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            ResultType = that.ResultType;
            StringExpr = that.StringExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionTryScan(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(StringExpr.Type(env) != "" && StringExpr.Type(env) != "string")
                throw new SequenceParserExceptionTypeMismatch(Symbol + " string parameter", "string type", StringExpr.Type(env));

            if(ResultType == null)
                return;

            AttributeType attrType = TypesHelper.XgrsTypeToAttributeType(ResultType, env.Model);
            if(attrType.Kind == AttributeKind.InternalClassObjectAttr)
                throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type not denoting an object type", ResultType);

            if(attrType.Kind == AttributeKind.InternalClassTransientObjectAttr)
                throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type not denoting a transient object type", ResultType);

            if(attrType.Kind == AttributeKind.SetAttr || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr || attrType.Kind == AttributeKind.DequeAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.InternalClassObjectAttr)
                    throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing an object type", ResultType);
                if(attrType.ValueType.Kind == AttributeKind.InternalClassTransientObjectAttr)
                    throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing a transient object type", ResultType);
                if(attrType.Kind == AttributeKind.MapAttr)
                {
                    if(attrType.KeyType.Kind == AttributeKind.InternalClassObjectAttr)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing an object type", ResultType);
                    if(attrType.KeyType.Kind == AttributeKind.InternalClassTransientObjectAttr)
                        throw new SequenceParserExceptionTypeMismatch(Symbol + " type parameter", "type, not containing a transient object type", ResultType);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            AttributeType attrType = TypesHelper.XgrsTypeToAttributeType(ResultType ?? "object", procEnv.Graph.Model);
            String unescaped = Unescape((string)StringExpr.Evaluate(procEnv));
            return GRSImport.TryScan(attrType, unescaped, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            StringExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return StringExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "tryscan" + (ResultType != null ? "<" + ResultType + ">" : "") + "(" + StringExpr.Symbol + ")"; }
        }
    }

    public abstract class SequenceExpressionFunctionCall : SequenceExpression, FunctionInvocation
    {
        /// <summary>
        /// An array of expressions used to compute the input arguments.
        /// </summary>
        public readonly SequenceExpression[] ArgumentExpressions;

        /// <summary>
        /// Buffer to store the argument values for the call; used to avoid unneccessary memory allocations.
        /// </summary>
        public readonly object[] Arguments;

        public abstract String Name { get; }
        public abstract String Package { get; }
        public abstract String PackagePrefixedName { get; }

        public abstract bool IsExternal { get; }

        protected SequenceExpressionFunctionCall(List<SequenceExpression> argExprs)
            : base(SequenceExpressionType.FunctionCall)
        {
            InitializeArgumentExpressionsAndArguments(argExprs, out ArgumentExpressions, out Arguments);
        }

        protected SequenceExpressionFunctionCall(SequenceExpressionType seqExprType, List<SequenceExpression> argExprs)
            : base(seqExprType)
        {
            InitializeArgumentExpressionsAndArguments(argExprs, out ArgumentExpressions, out Arguments);
        }

        protected SequenceExpressionFunctionCall(SequenceExpressionFunctionCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            CopyArgumentExpressionsAndArguments(originalToCopy, procEnv, that.ArgumentExpressions,
                out ArgumentExpressions, out Arguments);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            //children are arguments, those are checked in environment with the call

            env.CheckFunctionCall(this);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            GetLocalVariables(ArgumentExpressions, variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                foreach(SequenceExpression argument in ArgumentExpressions)
                {
                    yield return argument;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        protected virtual String GetFunctionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            if(ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ArgumentExpressions.Length; ++i)
                {
                    sb.Append(ArgumentExpressions[i].Symbol);
                    if(i != ArgumentExpressions.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

        public override string Symbol
        {
            get { return GetFunctionString(); }
        }
    }

    public class SequenceExpressionFunctionCallInterpreted : SequenceExpressionFunctionCall
    {
        /// <summary>
        /// The function to be used
        /// </summary>
        public readonly IFunctionDefinition FunctionDef;

        public override string Name
        {
            get { return FunctionDef.Name; }
        }

        public override String Package
        {
            get { return FunctionDef.Package; }
        }

        public override String PackagePrefixedName
        {
            get { return FunctionDef.Package != null ? FunctionDef.Package + "::" + FunctionDef.Name : FunctionDef.Name; }
        }

        public override bool IsExternal
        {
            get { return FunctionDef.IsExternal; }
        }

        public SequenceExpressionFunctionCallInterpreted(IFunctionDefinition FunctionDef, 
            List<SequenceExpression> argExprs)
            : base(argExprs)
        {
            this.FunctionDef = FunctionDef;
        }

        protected SequenceExpressionFunctionCallInterpreted(SequenceExpressionFunctionCallInterpreted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that, originalToCopy, procEnv)
        {
            FunctionDef = that.FunctionDef;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionFunctionCallInterpreted(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return TypesHelper.DotNetTypeToXgrsType(FunctionDef.Output);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IFunctionDefinition funcDef = FunctionDef;
            FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);
            object res = funcDef.Apply(procEnv, procEnv.Graph, Arguments);
            return res;
        }
    }

    public class SequenceExpressionFunctionCallCompiled : SequenceExpressionFunctionCall
    {
        public readonly String name;
        public readonly String package;
        public readonly String packagePrefixedName;
        public bool isExternal;

        /// <summary>
        /// The type returned
        /// </summary>
        public readonly string ReturnType;

        public override string Name
        {
            get { return name; }
        }

        public override String Package
        {
            get { return package; }
        }

        public override String PackagePrefixedName
        {
            get { return packagePrefixedName; }
        }

        public override bool IsExternal
        {
            get { return isExternal; }
        }

        public SequenceExpressionFunctionCallCompiled(String Name, String Package, String PackagePrefixedName,
            String ReturnType, List<SequenceExpression> argExprs, bool IsExternal)
            : base(argExprs)
        {
            this.name = Name;
            this.package = Package;
            this.packagePrefixedName = PackagePrefixedName;
            this.ReturnType = ReturnType;
            this.isExternal = IsExternal;
        }

        protected SequenceExpressionFunctionCallCompiled(SequenceExpressionFunctionCallCompiled that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that, originalToCopy, procEnv)
        {
            name = that.name;
            package = that.package;
            packagePrefixedName = that.packagePrefixedName;
            ReturnType = that.ReturnType;
            isExternal = that.isExternal;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionFunctionCallCompiled(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ReturnType;
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new NotImplementedException();
        }
    }

    public class SequenceExpressionFunctionMethodCall : SequenceExpressionFunctionCall, MethodFunctionInvocation
    {
        public readonly SequenceExpression TargetExpr;

        readonly String name;

        public override string Name
        {
            get { return name; }
        }

        public override String Package
        {
            get { return null; }
        }

        public override String PackagePrefixedName
        {
            get { return name; }
        }

        public override bool IsExternal
        {
            get { return false; }
        }

        public SequenceExpressionFunctionMethodCall(SequenceExpression targetExpr,
            String name,
            List<SequenceExpression> argExprs)
            : base(SequenceExpressionType.FunctionMethodCall, argExprs)
        {
            TargetExpr = targetExpr;
            this.name = name;
        }

        protected SequenceExpressionFunctionMethodCall(SequenceExpressionFunctionMethodCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that, originalToCopy, procEnv)
        {
            TargetExpr = that.TargetExpr.CopyExpression(originalToCopy, procEnv);
            name = that.name;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionFunctionMethodCall(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            //children are arguments, those are checked in environment with the call

            env.CheckFunctionMethodCall(TargetExpr, this);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            String targetExprType = TargetExpr.Type(env);
            if(targetExprType == "")
                return "";

            InheritanceType ownerType = TypesHelper.GetInheritanceType(targetExprType, env.Model);
            if(ownerType == null)
                throw new SequenceParserExceptionUserMethodsOnlyAvailableForInheritanceTypes(targetExprType, Name);

            if(ownerType.GetFunctionMethod(Name) == null)
                throw new SequenceParserExceptionCallIssue(this, CallIssueType.UnknownFunction);

            IFunctionDefinition funcDef = ownerType.GetFunctionMethod(Name);
            return TypesHelper.DotNetTypeToXgrsType(funcDef.Output);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement owner = (IGraphElement)TargetExpr.Evaluate(procEnv);
            FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);
            return owner.ApplyFunctionMethod(procEnv, procEnv.Graph, Name, Arguments);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            TargetExpr.GetLocalVariables(variables, constructors);
            GetLocalVariables(ArgumentExpressions, variables, constructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get
            {
                yield return TargetExpr;
                foreach(SequenceExpression argument in ArgumentExpressions)
                {
                    yield return argument;
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return TargetExpr.Symbol + "." + GetFunctionString(); }
        }
    }
}
