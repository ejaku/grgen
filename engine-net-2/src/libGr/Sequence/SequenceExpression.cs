/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        Not, UnaryMinus, Cast,
        Equal, NotEqual, Lower, LowerEqual, Greater, GreaterEqual, StructuralEqual,
        Plus, Minus, Mul, Div, Mod, // nice-to-have addition: all the other operators and functions/methods from the rule language expressions
        Constant, Variable, This,
        SetConstructor, MapConstructor, ArrayConstructor, DequeConstructor,
        SetCopyConstructor, MapCopyConstructor, ArrayCopyConstructor, DequeCopyConstructor,
        ContainerAsArray, StringAsArray,
        MapDomain, MapRange,
        Random,
        Def,
        IsVisited,
        InContainer, ContainerEmpty, ContainerSize, ContainerAccess, ContainerPeek,
        ArrayOrDequeIndexOf, ArrayOrDequeLastIndexOf, ArrayIndexOfOrdered,
        ArraySum, ArrayProd, ArrayMin, ArrayMax, ArrayAvg, ArrayMed, ArrayMedUnsorted, ArrayVar, ArrayDev,
        ArrayOrDequeAsSet, ArrayAsMap, ArrayAsDeque, ArrayAsString,
        ArraySubarray, DequeSubdeque,
        ArrayOrderAscending, ArrayOrderDescending, ArrayKeepOneForEach, ArrayReverse,
        ArrayExtract,
        ElementFromGraph, NodeByName, EdgeByName, NodeByUnique, EdgeByUnique,
        Source, Target, Opposite,
        GraphElementAttributeOrElementOfMatch, GraphElementAttribute, ElementOfMatch,
        Nodes, Edges,
        CountNodes, CountEdges,
        Now,
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
        EqualsAny,
        Nameof, Uniqueof, Typeof,
        ExistsFile, Import,
        Copy,
        Canonize,
        RuleQuery, MultiRuleQuery,
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

        /// <summary>
        /// Copies the sequence computation deeply so that
        /// - the global Variables are kept
        /// - the local Variables are replaced by copies initialized to null
        /// Used for cloning defined sequences before executing them if needed.
        /// Needed if the defined sequence is currently executed to prevent state corruption.
        /// </summary>
        /// <param name="originalToCopy">A map used to ensure that every instance of a variable is mapped to the same copy</param>
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
            return Execute(procEnv);
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
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
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
                throw new SequenceParserException(Operator, leftType, rightType, Symbol);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            BalancedTypeStatic = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
            if(BalancedTypeStatic == "-")
                throw new SequenceParserException(Operator, LeftTypeStatic, RightTypeStatic, Symbol);
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Left.GetLocalVariables(variables, containerConstructors);
            Right.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "boolean", Condition.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ""; // no constraints regarding the types of the expressions to choose from
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Condition.Evaluate(procEnv) ? TrueCase.Evaluate(procEnv) : FalseCase.Evaluate(procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Condition.GetLocalVariables(variables, containerConstructors);
            TrueCase.GetLocalVariables(variables, containerConstructors);
            FalseCase.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) ^ (bool)Right.Evaluate(procEnv);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return !(bool)Operand.Evaluate(procEnv);
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Operand.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException("-", operandType, operandType, Symbol);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            OperandTypeStatic = Operand.Type(env);
            BalancedTypeStatic = SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
            if(BalancedTypeStatic == "-")
                throw new SequenceParserException("-", OperandTypeStatic, OperandTypeStatic, Symbol);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            string OperandTypeStatic = Operand.Type(env);
            return SequenceExpressionTypeHelper.Balance(SequenceExpressionType, OperandTypeStatic, env.Model);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException("-", TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(operandValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Operand.GetLocalVariables(variables, containerConstructors);
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
            return null; // TODO: handle the non-node and non-edge-types, too
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return Operand.Evaluate(procEnv);
        }

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Operand.GetLocalVariables(variables, containerConstructors);
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
            get { return "(" + (TargetType is NodeType ? ((NodeType)TargetType).Name : ((EdgeType)TargetType).Name ) + ")" + Operand.Symbol; }
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            try
            {
                return SequenceExpressionExecutionHelper.StructuralEqualObjects(leftValue, rightValue);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return Variable.GetVariableValue(procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
    
    public abstract class SequenceExpressionContainerConstructor : SequenceExpression
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
                    throw new SequenceParserException(Symbol, ValueType, containerItem.Type(env));
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            foreach(SequenceExpression containerItem in ContainerItems)
            {
                containerItem.GetLocalVariables(variables, containerConstructors);
            }
            containerConstructors.Add(this);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type srcType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
            Type dstType = typeof(de.unika.ipd.grGen.libGr.SetValueType);
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
                    throw new SequenceParserException(Symbol, KeyType, keyItem.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "map<" + KeyType + "," + ValueType + ">";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type keyType = ContainerHelper.GetTypeFromNameForContainer(KeyType, procEnv.Graph.Model);
            Type valueType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            base.GetLocalVariables(variables, containerConstructors);

            foreach(SequenceExpression mapKeyItem in MapKeyItems)
            {
                mapKeyItem.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
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
                    throw new SequenceParserException(Symbol + ", argument", "set type", SetToCopy.Type(env));

                // TODO: check ValueType with compatibility with value type of SetToCopy
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type srcType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
            Type dstType = typeof(de.unika.ipd.grGen.libGr.SetValueType);
            IDictionary set = ContainerHelper.NewDictionary(srcType, dstType);
            ContainerHelper.FillSet(set, ValueType, SetToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return set;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            base.GetLocalVariables(variables, containerConstructors);
            SetToCopy.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + ", argument", "map type", MapToCopy.Type(env));

                // TODO: check KeyType/ValueType with compatibility with key type/value type of MapToCopy
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type srcType = ContainerHelper.GetTypeFromNameForContainer(KeyType, procEnv.Graph.Model);
            Type dstType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
            IDictionary map = ContainerHelper.NewDictionary(srcType, dstType);
            ContainerHelper.FillMap(map, KeyType, ValueType, MapToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            base.GetLocalVariables(variables, containerConstructors);
            MapToCopy.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + ", argument", "array type", ArrayToCopy.Type(env));

                // TODO: check ValueType with compatibility with value type of ArrayToCopy
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
            IList array = ContainerHelper.NewList(valueType);
            ContainerHelper.FillArray(array, ValueType, ArrayToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return array;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            base.GetLocalVariables(variables, containerConstructors);
            ArrayToCopy.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + ", argument", "deque type", DequeToCopy.Type(env));

                // TODO: check ValueType with compatibility with value type of DequeToCopy
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            Type valueType = ContainerHelper.GetTypeFromNameForContainer(ValueType, procEnv.Graph.Model);
            IDeque deque = ContainerHelper.NewDeque(valueType);
            ContainerHelper.FillDeque(deque, ValueType, DequeToCopy.Evaluate(procEnv), procEnv.Graph.Model);
            return deque;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            base.GetLocalVariables(variables, containerConstructors);
            DequeToCopy.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.AsArray(ContainerValue(procEnv), procEnv.Graph.Model);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "string type", Type(env));

            if(SeparatorExpr.Type(env) == "")
                return;

            if(SeparatorExpr.Type(env) != "string")
                throw new SequenceParserException(Symbol, "string type", Type(env));
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<string>";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.StringAsArray((string)StringExpr.Evaluate(procEnv), (string)SeparatorExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            StringExpr.GetLocalVariables(variables, containerConstructors);
            SeparatorExpr.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "int", UpperBound.Type(env));
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(UpperBound != null)
                return Sequence.randomGenerator.Next((int)UpperBound.Evaluate(procEnv));
            else
                return Sequence.randomGenerator.NextDouble();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(UpperBound != null) 
                UpperBound.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "variable", "not a variable");
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            foreach(SequenceExpression defVar in DefVars)
            {
                if(defVar.Evaluate(procEnv) == null)
                    return false;
            }
            return true;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            foreach(SequenceExpression defVar in DefVars)
            {
                defVar.GetLocalVariables(variables, containerConstructors);
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
            VisitedFlagExpr = that.VisitedFlagExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionIsVisited(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(GraphElementVarExpr.Type(env) != "" && TypesHelper.GetNodeOrEdgeType(GraphElementVarExpr.Type(env), env.Model) == null)
                throw new SequenceParserException(Symbol, "node or edge type", GraphElementVarExpr.Type(env));
            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpr.Type(env), "int", env.Model))
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpr.Type(env));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)GraphElementVarExpr.Evaluate(procEnv);
            int visitedFlag = (int)VisitedFlagExpr.Evaluate(procEnv);
            return procEnv.Graph.IsVisited(elem, visitedFlag);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            GraphElementVarExpr.GetLocalVariables(variables, containerConstructors);
            VisitedFlagExpr.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get{ yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return GraphElementVarExpr.Symbol + ".visited[" + VisitedFlagExpr.Symbol + "]"; }
        }
    }

    public class SequenceExpressionInContainer : SequenceExpression
    {
        public readonly SequenceExpression Expr;
        public readonly SequenceExpression ContainerExpr;

        public SequenceExpressionInContainer(SequenceExpression expr, SequenceExpression container)
            : base(SequenceExpressionType.InContainer)
        {
            Expr = expr;
            ContainerExpr = container;
        }

        protected SequenceExpressionInContainer(SequenceExpressionInContainer that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that)
        {
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
            ContainerExpr = that.ContainerExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionInContainer(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
        }

        public string CheckAndReturnContainerType(SequenceCheckingEnvironment env)
        {
            string ContainerType;
            ContainerType = ContainerExpr.Type(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        public object ContainerValue(IGraphProcessingEnvironment procEnv)
        {
            if(ContainerExpr is SequenceExpressionAttributeAccess)
                return ((SequenceExpressionAttributeAccess)ContainerExpr).ExecuteNoImplicitContainerCopy(procEnv);
            else
                return ContainerExpr.Evaluate(procEnv);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object container = ContainerValue(procEnv);
            
            if(container is IList)
            {
                IList array = (IList)container;
                return array.Contains(Expr.Evaluate(procEnv));
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                return deque.Contains(Expr.Evaluate(procEnv));
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap.Contains(Expr.Evaluate(procEnv));
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
            Expr.GetLocalVariables(variables, containerConstructors);
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
            get { return Expr.Symbol + " in " + ContainerExpr.Symbol; }
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check source and destination types if the container is untyped, only runtime-check possible

            if(TypesHelper.ExtractDst(ContainerType) == "SetValueType")
                throw new SequenceParserException(Symbol, "map<S,T> or array<S> or deque<S>", ContainerType);
            if(ContainerType.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                    throw new SequenceParserException(Symbol, "int", KeyExpr.Type(env));
            }
            else if(ContainerType.StartsWith("deque"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                    throw new SequenceParserException(Symbol, "int", KeyExpr.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), KeyExpr.Type(env));
            }
        }

        public string CheckAndReturnContainerType(SequenceCheckingEnvironment env)
        {
            string ContainerType = ContainerExpr.Type(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
            KeyExpr.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "int", KeyExpr.Type(env));
            }
            else
            {
                if(containerType.StartsWith("set<") || containerType.StartsWith("map<"))
                    throw new SequenceParserException(Symbol, "array<S> or deque<S> type", containerType);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return TypesHelper.ExtractSrc(ContainerType(env));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(KeyExpr != null)
                return ContainerHelper.Peek(ContainerValue(procEnv), (int)KeyExpr.Evaluate(procEnv)); 
            else
                return ContainerHelper.Peek(ContainerValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
            if(KeyExpr != null)
                KeyExpr.GetLocalVariables(variables, containerConstructors);
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

    public class SequenceExpressionArrayOrDequeIndexOf : SequenceExpressionContainer
    {
        public readonly SequenceExpression ValueToSearchForExpr;
        public readonly SequenceExpression StartPositionExpr;

        public SequenceExpressionArrayOrDequeIndexOf(SequenceExpression containerExpr, SequenceExpression valueToSearchForExpr, SequenceExpression startPositionExpr)
            : base(SequenceExpressionType.ArrayOrDequeIndexOf, containerExpr)
        {
            ValueToSearchForExpr = valueToSearchForExpr;
            StartPositionExpr = startPositionExpr;
        }

        protected SequenceExpressionArrayOrDequeIndexOf(SequenceExpressionArrayOrDequeIndexOf that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrDequeIndexOf(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType == "")
                return;

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<"))
                throw new SequenceParserException(Symbol, "array<T> or deque<T> type", containerType);

            if(ValueToSearchForExpr.Type(env) != "")
            {
                if(ValueToSearchForExpr.Type(env) != TypesHelper.ExtractSrc(ContainerType(env)))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType(env)), ValueToSearchForExpr.Type(env));
            }

            if(StartPositionExpr != null && StartPositionExpr.Type(env) != "")
            {
                if(StartPositionExpr != null && StartPositionExpr.Type(env) != "int")
                    throw new SequenceParserException(Symbol, "int", ValueToSearchForExpr.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(StartPositionExpr != null)
                return ContainerHelper.IndexOf(ContainerValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv), (int)StartPositionExpr.Evaluate(procEnv));
            else
                return ContainerHelper.IndexOf(ContainerValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
            ValueToSearchForExpr.GetLocalVariables(variables, containerConstructors);
            if(StartPositionExpr != null)
                StartPositionExpr.GetLocalVariables(variables, containerConstructors);
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

    public class SequenceExpressionArrayOrDequeLastIndexOf : SequenceExpressionContainer
    {
        public readonly SequenceExpression ValueToSearchForExpr;
        public readonly SequenceExpression StartPositionExpr;

        public SequenceExpressionArrayOrDequeLastIndexOf(SequenceExpression containerExpr, SequenceExpression valueToSearchForExpr, SequenceExpression startPositionExpr)
            : base(SequenceExpressionType.ArrayOrDequeLastIndexOf, containerExpr)
        {
            ValueToSearchForExpr = valueToSearchForExpr;
            StartPositionExpr = startPositionExpr;
        }

        protected SequenceExpressionArrayOrDequeLastIndexOf(SequenceExpressionArrayOrDequeLastIndexOf that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayOrDequeLastIndexOf(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType == "")
                return;

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<"))
                throw new SequenceParserException(Symbol, "array<T> or deque<T> type", containerType);

            if(ValueToSearchForExpr.Type(env) != "")
            {
                if(ValueToSearchForExpr.Type(env) != TypesHelper.ExtractSrc(ContainerType(env)))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType(env)), ValueToSearchForExpr.Type(env));
            }

            if(StartPositionExpr != null && StartPositionExpr.Type(env) != "")
            {
                if(containerType.StartsWith("deque<"))
                    throw new SequenceParserException(Symbol, "array<T> type", containerType);

                if(StartPositionExpr != null && StartPositionExpr.Type(env) != "int")
                    throw new SequenceParserException(Symbol, "int", ValueToSearchForExpr.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(StartPositionExpr != null)
                return ContainerHelper.LastIndexOf(ArrayValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv), (int)StartPositionExpr.Evaluate(procEnv));
            else
                return ContainerHelper.LastIndexOf(ContainerValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
            ValueToSearchForExpr.GetLocalVariables(variables, containerConstructors);
            if(StartPositionExpr != null)
                StartPositionExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);

            if(ValueToSearchForExpr.Type(env) != "")
            {
                if(ValueToSearchForExpr.Type(env) != TypesHelper.ExtractSrc(ContainerType(env)))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType(env)), ValueToSearchForExpr.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.IndexOfOrdered(ArrayValue(procEnv), ValueToSearchForExpr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
            ValueToSearchForExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Sum(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Prod(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Min(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return GetOperationType(TypesHelper.ExtractSrc(ContainerType(env)));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Max(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Avg(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Med(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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

    public class SequenceExpressionArrayMedUnsorted : SequenceExpressionContainer
    {
        public SequenceExpressionArrayMedUnsorted(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ArrayMedUnsorted, containerExpr)
        {
        }

        protected SequenceExpressionArrayMedUnsorted(SequenceExpressionArrayMedUnsorted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayMedUnsorted(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.MedUnsorted(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
            get { return Name + ".medUnsorted()"; }
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Var(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "double";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Dev(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> or deque<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "set<" + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayOrDequeAsSet(ContainerValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "map<S,T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "set<" + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Domain(MapValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "map<S,T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "set<" + TypesHelper.ExtractDst(ContainerType(env)) + ">";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Range(MapValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "map<int," + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayAsMap(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            return "deque<" + TypesHelper.ExtractSrc(ContainerType(env)) + ">";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayAsDeque(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);

            if(containerType != ("array<string>"))
                throw new SequenceParserException(Symbol, "array<string> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayAsString(ArrayValue(procEnv), (string)Separator.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Subarray(ArrayValue(procEnv), (int)Start.Evaluate(procEnv), (int)Length.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "deque<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Subdeque(DequeValue(procEnv), (int)Start.Evaluate(procEnv), (int)Length.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayOrderAscending(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayOrderDescending(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayKeepOneForEach(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "array<T> type", containerType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerType(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ArrayReverse(ArrayValue(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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

    public class SequenceExpressionArrayExtract : SequenceExpressionContainer
    {
        public string memberOrAttributeName;

        public SequenceExpressionArrayExtract(SequenceExpression containerExpr, String memberOrAttributeName)
            : base(SequenceExpressionType.ArrayExtract, containerExpr)
        {
            this.memberOrAttributeName = memberOrAttributeName;
        }

        protected SequenceExpressionArrayExtract(SequenceExpressionArrayExtract that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
           : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceExpressionArrayExtract(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(containerType.StartsWith("set<") || containerType.StartsWith("map<") || containerType.StartsWith("deque<"))
                throw new SequenceParserException(Symbol, "array<T> type", containerType);

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));

            // throws exceptions in case the rule does not exist, or it does not contain an element of the given name
            String memberOrAttributeType = env.TypeOfMemberOrAttribute(arrayValueType, memberOrAttributeName);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(ContainerType(env) == "")
                return "";

            String arrayValueType = TypesHelper.ExtractSrc(ContainerType(env));
            String memberOrAttributeType = env.TypeOfMemberOrAttribute(arrayValueType, memberOrAttributeName);
            return "array<" + memberOrAttributeType + ">";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.Extract(ArrayValue(procEnv), memberOrAttributeName, procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ContainerExpr.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                    throw new SequenceParserException(Symbol + ", (first) argument", "string", NodeName.Type(env));
            }
            CheckNodeTypeIsKnown(env, NodeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return NodeType!=null ? NodeType.Type(env) : "Node";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                    throw new SequenceParserException(Symbol + ", (first) argument", "string", EdgeName.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return EdgeType != null ? EdgeType.Type(env) : "AEdge";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                    throw new SequenceParserException(Symbol + ", (first) argument", "int", NodeUniqueId.Type(env));
            }
            CheckNodeTypeIsKnown(env, NodeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return NodeType != null ? NodeType.Type(env) : "Node";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                    throw new SequenceParserException(Symbol + ", (first) argument", "int", EdgeUniqueId.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return EdgeType != null ? EdgeType.Type(env) : "AEdge";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.Execute(procEnv)).Source;
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.Execute(procEnv)).Target;
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.Execute(procEnv)).Opposite((INode)Node.Execute(procEnv));
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

            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(Source.Type(env), env.Model);
            if(nodeOrEdgeType == null)
                throw new SequenceParserException(Symbol, "node or edge type", Source.Type(env));
            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(AttributeName);
            if(attributeType == null)
                throw new SequenceParserException(AttributeName, SequenceParserError.UnknownAttribute);

            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(Source.Type(env) == "")
                return ""; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            
            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(Source.Type(env), env.Model);
            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(AttributeName);
            if(attributeType == null)
                return ""; // error, will be reported by Check, just ensure we don't crash here

            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)Source.Evaluate(procEnv);
            object value = elem.GetAttribute(AttributeName);
            value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                elem, AttributeName, value);
            return value;
        }

        public static object Execute(IGraphProcessingEnvironment procEnv, IGraphElement elem, string attributeName)
        {
            object value = elem.GetAttribute(attributeName);
            value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                elem, attributeName, value);
            return value;
        }

        public object ExecuteNoImplicitContainerCopy(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)Source.Evaluate(procEnv);
            object value = elem.GetAttribute(AttributeName);
            return value;
        }

        public object ExecuteNoImplicitContainerCopy(IGraphProcessingEnvironment procEnv, out IGraphElement elem, out AttributeType attrType)
        {
            elem = (IGraphElement)Source.Evaluate(procEnv);
            object value = elem.GetAttribute(AttributeName);
            attrType = elem.Type.GetAttributeType(AttributeName);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Source.GetLocalVariables(variables, containerConstructors);
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
                throw new Exception("SequenceExpression MatchAccess can only access a variable of type match<rulename>");

            string ruleName = TypesHelper.ExtractSrc(Source.Type(env));

            // throws exceptions in case the rule does not exist, or it does not contain an element of the given name
            string elementType = env.TypeOfTopLevelEntityInRule(ruleName, ElementName);

            if(elementType == "")
                throw new Exception("Internal failure, static type of element in match type not known");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string ruleName = TypesHelper.ExtractSrc(Source.Type(env));
            return env.TypeOfTopLevelEntityInRule(ruleName, ElementName);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Source.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                else
                    return SequenceExpressionAttributeAccess.Execute(procEnv, (IGraphElement)source, AttributeOrElementName);
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Source.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = GetNodeType(procEnv, NodeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.Nodes(procEnv.Graph, nodeType, procEnv);
            else
                return GraphHelper.Nodes(procEnv.Graph, nodeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(NodeType != null)
                NodeType.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.Edges(procEnv.Graph, edgeType, procEnv);
            else
                return GraphHelper.Edges(procEnv.Graph, edgeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = GetNodeType(procEnv, NodeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.CountNodes(procEnv.Graph, nodeType, procEnv);
            else
                return GraphHelper.CountNodes(procEnv.Graph, nodeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(NodeType != null)
                NodeType.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = GetEdgeType(procEnv, EdgeType, FunctionSymbol);

            if(EmitProfiling)
                return GraphHelper.CountEdges(procEnv.Graph, edgeType, procEnv);
            else
                return GraphHelper.CountEdges(procEnv.Graph, edgeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.NumNodes + procEnv.Graph.NumEdges == 0;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return DateTime.UtcNow.ToFileTime();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.NumNodes + procEnv.Graph.NumEdges;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", third argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", second argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", third argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            Depth.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "map<Node,int>";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            Depth.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            Depth.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodes || SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaOutgoing)
                {
                    if(TypesHelper.GetNodeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserException(Symbol + ", second argument", "node type", EndElement.Type(env));
                }
                else
                {
                    if(TypesHelper.GetEdgeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserException(Symbol + ", second argument", "edge type", EndElement.Type(env));
                }
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            EndElement.GetLocalVariables(variables, containerConstructors);
            Depth.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsAdjacentNodes || SequenceExpressionType == SequenceExpressionType.IsAdjacentNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsAdjacentNodesViaOutgoing)
                {
                    if(TypesHelper.GetNodeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserException(Symbol + ", second argument", "node type", EndElement.Type(env));
                }
                else
                {
                    if(TypesHelper.GetEdgeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserException(Symbol + ", second argument", "edge type", EndElement.Type(env));
                }
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            EndElement.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsReachableNodes || SequenceExpressionType == SequenceExpressionType.IsReachableNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsReachableNodesViaOutgoing)
                {
                    if(TypesHelper.GetNodeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserException(Symbol + ", second argument", "node type", EndElement.Type(env));
                }
                else
                {
                    if(TypesHelper.GetEdgeType(EndElement.Type(env), env.Model) == null)
                        throw new SequenceParserException(Symbol + ", second argument", "edge type", EndElement.Type(env));
                }
            }
            CheckEdgeTypeIsKnown(env, EdgeType, ", third argument");
            CheckNodeTypeIsKnown(env, OppositeNodeType, ", fourth argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceNode.GetLocalVariables(variables, containerConstructors);
            EndElement.GetLocalVariables(variables, containerConstructors);
            if(EdgeType != null)
                EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null)
                OppositeNodeType.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
            if(TypesHelper.ExtractSrc(NodeSet.Type(env))!="Node")
                throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object nodeSet = NodeSet.Evaluate(procEnv);
            return GraphHelper.InducedSubgraph((IDictionary<INode, SetValueType>)nodeSet, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            NodeSet.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
            if(TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "AEdge"
                && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "Edge"
                && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "UEdge")
            {
                throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object edgeSet = EdgeSet.Evaluate(procEnv);
            return GraphHelper.DefinedSubgraph((IDictionary)edgeSet, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            EdgeSet.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + ", first argument", "graph type", Subgraph.Type(env));
            }

            if(SubgraphSet.Type(env) != "")
            {
                if(!SubgraphSet.Type(env).StartsWith("set<"))
                    throw new SequenceParserException(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
                if(TypesHelper.ExtractSrc(SubgraphSet.Type(env)) != "graph")
                    throw new SequenceParserException(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object subgraph = Subgraph.Evaluate(procEnv);
            object subgraphSet = SubgraphSet.Evaluate(procEnv);
            return GraphHelper.EqualsAny((IGraph)subgraph, (IDictionary<IGraph, SetValueType>)subgraphSet, IncludingAttributes);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Subgraph.GetLocalVariables(variables, containerConstructors);
            SubgraphSet.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "graph type", Graph.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object graph = Graph.Evaluate(procEnv);
            return ((IGraph)graph).Canonize();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Graph.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "node or edge or graph type", NamedEntity.Type(env));
                }
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(NamedEntity != null)
                return GraphHelper.Nameof(NamedEntity.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.Nameof(null, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(NamedEntity != null)
                NamedEntity.GetLocalVariables(variables, containerConstructors);
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
            return "int";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(UniquelyIdentifiedEntity != null)
            {
                base.Check(env); // check children

                if(!TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "Node", env.Model)
                    && !TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "AEdge", env.Model)
                    && !TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "graph", env.Model))
                {
                    throw new SequenceParserException(Symbol, "node or edge or graph type", UniquelyIdentifiedEntity.Type(env));
                }
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(UniquelyIdentifiedEntity != null)
                return GraphHelper.Uniqueof(UniquelyIdentifiedEntity.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.Uniqueof(null, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(UniquelyIdentifiedEntity != null)
                UniquelyIdentifiedEntity.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object entity = Entity.Evaluate(procEnv);
            return TypesHelper.XgrsTypeOfConstant(entity, procEnv.Graph.Model);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Entity.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "string type", Path.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object path = Path.Evaluate(procEnv);
            return File.Exists((string)path);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Path.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "string type", Path.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object path = Path.Evaluate(procEnv);
            return GraphHelper.Import(path, procEnv.Backend, procEnv.Graph.Model);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Path.GetLocalVariables(variables, containerConstructors);
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

        public SequenceExpressionCopy(SequenceExpression objectToBeCopied)
            : base(SequenceExpressionType.Copy)
        {
            ObjectToBeCopied = objectToBeCopied;
        }

        protected SequenceExpressionCopy(SequenceExpressionCopy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            ObjectToBeCopied = that.ObjectToBeCopied.CopyExpression(originalToCopy, procEnv);
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

            if(!TypesHelper.IsSameOrSubtype(ObjectToBeCopied.Type(env), "graph", env.Model)
                && !ObjectToBeCopied.Type(env).StartsWith("match<")
                && (TypesHelper.ExtractSrc(ObjectToBeCopied.Type(env)) == null || TypesHelper.ExtractDst(ObjectToBeCopied.Type(env)) == null))
            {
                throw new SequenceParserException(Symbol, "graph type or match type or container type", ObjectToBeCopied.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ObjectToBeCopied.Type(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object toBeCloned = ObjectToBeCopied.Evaluate(procEnv);
            if(toBeCloned is IGraph)
                return GraphHelper.Copy((IGraph)toBeCloned);
            else if(toBeCloned is IMatch)
                return ((IMatch)toBeCloned).Clone();
            else
                return ContainerHelper.Clone(toBeCloned);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ObjectToBeCopied.GetLocalVariables(variables, containerConstructors);
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
            get { return "copy(" + ObjectToBeCopied.Symbol + ")"; }
        }
    }

    public class SequenceExpressionRuleQuery : SequenceExpression, RuleInvocation
    {
        public readonly SequenceRuleAllCall RuleCall;

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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            SequenceRuleAllCallInterpreted ruleCallInterpreted = (SequenceRuleAllCallInterpreted)RuleCall;
            return ruleCallInterpreted.MatchForQuery(procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            RuleCall.GetLocalVariables(variables, containerConstructors, null);
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

    public class SequenceExpressionMultiRuleQuery : SequenceExpression
    {
        public readonly SequenceMultiRuleAllCall MultiRuleCall;
        public readonly String MatchClass;

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
                    throw new SequenceParserException(MatchClass, ruleCall.PackagePrefixedName, SequenceParserError.MatchClassNotImplementedError);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<match<class " + MatchClass + ">>";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            List<IMatches> MatchesList;
            List<IMatch> MatchList;
            MultiRuleCall.MatchAll(procEnv, out MatchesList, out MatchList);
            // MatchAll clones single matches because of potentially multiple calls of same rule, overall list is created anew anyway
            // - without that cloning an additional clone would be needed here like for the single rule query, 
            // as the maches array must be available (at least) through continued sequence expression processing
            // (and a new call could be made during that time, example: [[?r]] + [[?r]])
            return MatchList;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            MultiRuleCall.GetLocalVariables(variables, containerConstructors, null);
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
                bool first = true;
                foreach(Sequence rule in MultiRuleCall.Sequences)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(rule.Symbol);
                }
                sb.Append("]");
                sb.Append("\\<class " + MatchClass + ">");
                sb.Append("]");
                return sb.ToString();
            }
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            GetLocalVariables(ArgumentExpressions, variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            return "";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement owner = (IGraphElement)TargetExpr.Evaluate(procEnv);
            FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);
            return owner.ApplyFunctionMethod(procEnv, procEnv.Graph, Name, Arguments);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            TargetExpr.GetLocalVariables(variables, containerConstructors);
            GetLocalVariables(ArgumentExpressions, variables, containerConstructors);
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
