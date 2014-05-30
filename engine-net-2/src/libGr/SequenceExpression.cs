/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

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
        LazyOr, LazyAnd, StrictOr, StrictXor, StrictAnd,
        Not, Cast,
        Equal, NotEqual, Lower, LowerEqual, Greater, GreaterEqual, StructuralEqual,
        Plus, Minus, Mul, Div, Mod, // nice-to-have addition: all the other operators and functions/methods from the rule language expressions
        Constant, Variable, This,
        SetConstructor, MapConstructor, ArrayConstructor, DequeConstructor,
        Random,
        Def,
        IsVisited,
        InContainer, ContainerEmpty, ContainerSize, ContainerAccess, ContainerPeek,
        ElementFromGraph, NodeByName, EdgeByName, NodeByUnique, EdgeByUnique,
        Source, Target, Opposite,
        GraphElementAttribute,
        ElementOfMatch,
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
        Nameof, Uniqueof,
        ExistsFile, Import,
        Copy,
        Canonize,
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
        public SequenceExpressionType SequenceExpressionType;

        /// <summary>
        /// Initializes a new SequenceExpression object with the given sequence expression type.
        /// </summary>
        /// <param name="seqExprType">The sequence expression type.</param>
        public SequenceExpression(SequenceExpressionType seqExprType)
            : base(SequenceComputationType.Expression)
        {
            SequenceExpressionType = seqExprType;

            id = idSource;
            ++idSource;
        }

        /// <summary>
        /// Returns the type of the sequence expression
        /// default behaviour: returns "boolean"
        /// </summary>
        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
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

        public override IEnumerable<SequenceComputation> Children { get { foreach(SequenceExpression expr in ChildrenExpression) yield return expr; ; } }
        public override sealed bool ReturnsValue { get { return true; } }

        /// <summary>
        /// Enumerates all child sequence expression objects
        /// </summary>
        public abstract IEnumerable<SequenceExpression> ChildrenExpression { get; }
    }

    /// <summary>
    /// A sequence expression over a container object (resulting from a variable or a method call)
    /// </summary>
    public abstract class SequenceExpressionContainer : SequenceExpression
    {
        public SequenceExpression ContainerExpr;

        public SequenceExpressionContainer(SequenceExpressionType type, SequenceExpression containerExpr)
            : base(type)
        {
            ContainerExpr = containerExpr;
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

        public override string Type(SequenceCheckingEnvironment env)
        {
            return ContainerExpr.Type(env);
        }

        public string Name { get { return ContainerExpr.Symbol; } }
    }

    /// <summary>
    /// A sequence binary expression object with references to the left and right child sequence expressions.
    /// </summary>
    public abstract class SequenceBinaryExpression : SequenceExpression
    {
        public SequenceExpression Left;
        public SequenceExpression Right;

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
        public SequenceBinaryExpression(SequenceExpressionType seqExprType, 
            SequenceExpression left, SequenceExpression right)
            : base(seqExprType)
        {
            SequenceExpressionType = seqExprType;

            this.Left = left;
            this.Right = right;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            BalancedTypeStatic = SequenceExpressionHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
            if(BalancedTypeStatic == "-")
            {
                throw new SequenceParserException(Operator, LeftTypeStatic, RightTypeStatic, Symbol);
            }
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
        internal override sealed SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceBinaryExpression copy = (SequenceBinaryExpression)MemberwiseClone();
            copy.Left = Left.CopyExpression(originalToCopy, procEnv);
            copy.Right = Right.CopyExpression(originalToCopy, procEnv);
            return copy;
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
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Left; yield return Right; } }

        /// <summary>
        /// Returns this expression as string, in the form of left expression then operator then right expression;
        /// same for all binary expressions
        /// </summary>
        public override string Symbol { get { return Left.Symbol + Operator + Right.Symbol; } }

        /// <summary>
        /// Returns the operator of this binary expression (enclosed in spaces);
        /// abstract property, to be overwritten by concrete operator symbol
        /// </summary>
        public abstract string Operator { get; }
    }


    public class SequenceExpressionConditional : SequenceExpression
    {
        public SequenceExpression Condition;
        public SequenceExpression TrueCase;
        public SequenceExpression FalseCase;

        public SequenceExpressionConditional(SequenceExpression condition, 
            SequenceExpression trueCase, 
            SequenceExpression falseCase)
            : base(SequenceExpressionType.Conditional)
        {
            this.Condition = condition;
            this.TrueCase = trueCase;
            this.FalseCase = falseCase;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
            
            if(!TypesHelper.IsSameOrSubtype(Condition.Type(env), "boolean", env.Model))
            {
                throw new SequenceParserException(Symbol, "boolean", Condition.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return ""; // no constraints regarding the types of the expressions to choose from
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionConditional copy = (SequenceExpressionConditional)MemberwiseClone();
            copy.Condition = Condition.CopyExpression(originalToCopy, procEnv);
            copy.TrueCase = TrueCase.CopyExpression(originalToCopy, procEnv);
            copy.FalseCase = FalseCase.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Condition; yield return TrueCase; yield return FalseCase; } }
        public override int Precedence { get { return 1; } }
        public override string Symbol { get { return Condition.Symbol + " ? " + TrueCase.Symbol + " : " + FalseCase.Symbol; } }
    }

    public class SequenceExpressionLazyOr : SequenceBinaryExpression
    {
        public SequenceExpressionLazyOr(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.LazyOr, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) || (bool)Right.Evaluate(procEnv);
        }

        public override int Precedence { get { return 2; } }
        public override string Operator { get { return " || "; } }
    }

    public class SequenceExpressionLazyAnd : SequenceBinaryExpression
    {
        public SequenceExpressionLazyAnd(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.LazyAnd, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) && (bool)Right.Evaluate(procEnv);
        }

        public override int Precedence { get { return 3; } }
        public override string Operator { get { return " && "; } }
    }

    public class SequenceExpressionStrictOr : SequenceBinaryExpression
    {
        public SequenceExpressionStrictOr(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StrictOr, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) | (bool)Right.Evaluate(procEnv);
        }

        public override int Precedence { get { return 4; } }
        public override string Operator { get { return " | "; } }
    }

    public class SequenceExpressionStrictXor : SequenceBinaryExpression
    {
        public SequenceExpressionStrictXor(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StrictXor, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) ^ (bool)Right.Evaluate(procEnv);
        }

        public override int Precedence { get { return 5; } }
        public override string Operator { get { return " ^ "; } }
    }

    public class SequenceExpressionStrictAnd : SequenceBinaryExpression
    {
        public SequenceExpressionStrictAnd(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StrictAnd, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return (bool)Left.Evaluate(procEnv) & (bool)Right.Evaluate(procEnv);
        }

        public override int Precedence { get { return 6; } }
        public override string Operator { get { return " & "; } }
    }

    public class SequenceExpressionNot : SequenceExpression
    {
        public SequenceExpression Operand;

        public SequenceExpressionNot(SequenceExpression operand)
            : base(SequenceExpressionType.Not)
        {
            this.Operand = operand;
        }

        internal override sealed SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionNot copy = (SequenceExpressionNot)MemberwiseClone();
            copy.Operand = Operand.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Operand; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "!" + Operand.Symbol; } }
    }

    public class SequenceExpressionCast : SequenceExpression
    {
        public SequenceExpression Operand;
        public object TargetType;

        public SequenceExpressionCast(SequenceExpression operand, object targetType)
            : base(SequenceExpressionType.Cast)
        {
            this.Operand = operand;
            this.TargetType = targetType;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(TargetType is NodeType)
                return ((NodeType)TargetType).Name;
            if(TargetType is EdgeType)
                return ((EdgeType)TargetType).Name;
            return null; // TODO: handle the non-node and non-edge-types, too
        }

        internal override sealed SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCast copy = (SequenceExpressionCast)MemberwiseClone();
            copy.Operand = Operand.CopyExpression(originalToCopy, procEnv);
            copy.TargetType = TargetType;
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Operand; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return "(" + (TargetType is NodeType ? ((NodeType)TargetType).Name : ((EdgeType)TargetType).Name ) + ")" + Operand.Symbol; } }
    }


    public class SequenceExpressionEqual : SequenceBinaryExpression
    {
        public SequenceExpressionEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Equal, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.EqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " == "; } }
    }

    public class SequenceExpressionStructuralEqual : SequenceBinaryExpression
    {
        public SequenceExpressionStructuralEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.StructuralEqual, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            try
            {
                return SequenceExpressionHelper.StructuralEqualObjects(leftValue, rightValue);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " ~~ "; } }
    }

    public class SequenceExpressionNotEqual : SequenceBinaryExpression
    {
        public SequenceExpressionNotEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.NotEqual, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.NotEqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " != "; } }
    }

    public class SequenceExpressionLower : SequenceBinaryExpression
    {
        public SequenceExpressionLower(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Lower, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.LowerObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " < "; } }
    }

    public class SequenceExpressionLowerEqual : SequenceBinaryExpression
    {
        public SequenceExpressionLowerEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.LowerEqual, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.LowerEqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " <= "; } }
    }

    public class SequenceExpressionGreater : SequenceBinaryExpression
    {
        public SequenceExpressionGreater(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Greater, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.GreaterObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " > "; } }
    }

    public class SequenceExpressionGreaterEqual : SequenceBinaryExpression
    {
        public SequenceExpressionGreaterEqual(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.GreaterEqual, left, right)
        {
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.GreaterEqualObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " >= "; } }
    }


    public class SequenceExpressionPlus : SequenceBinaryExpression
    {
        public SequenceExpressionPlus(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Plus, left, right)
        {
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.PlusObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " + "; } }
    }

    public class SequenceExpressionMinus : SequenceBinaryExpression
    {
        public SequenceExpressionMinus(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Minus, left, right)
        {
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.MinusObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " - "; } }
    }

    public class SequenceExpressionMul : SequenceBinaryExpression
    {
        public SequenceExpressionMul(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Mul, left, right)
        {
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.MulObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " * "; } }
    }

    public class SequenceExpressionDiv : SequenceBinaryExpression
    {
        public SequenceExpressionDiv(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Div, left, right)
        {
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.DivObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " / "; } }
    }

    public class SequenceExpressionMod : SequenceBinaryExpression
    {
        public SequenceExpressionMod(SequenceExpression left, SequenceExpression right)
            : base(SequenceExpressionType.Mod, left, right)
        {
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            LeftTypeStatic = Left.Type(env);
            RightTypeStatic = Right.Type(env);
            return SequenceExpressionHelper.Balance(SequenceExpressionType, LeftTypeStatic, RightTypeStatic, env.Model);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object leftValue = Left.Evaluate(procEnv);
            object rightValue = Right.Evaluate(procEnv);

            string balancedType = BalancedTypeStatic;
            string leftType = LeftTypeStatic;
            string rightType = RightTypeStatic;
            if(balancedType == "")
            {
                leftType = TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model);
                rightType = TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model);
                balancedType = SequenceExpressionHelper.Balance(SequenceExpressionType, leftType, rightType, procEnv.Graph.Model);
                if(balancedType == "-")
                {
                    throw new SequenceParserException(Operator, leftType, rightType, Symbol);
                }
            }

            try
            {
                return SequenceExpressionHelper.ModObjects(leftValue, rightValue, balancedType, leftType, rightType, procEnv.Graph);
            }
            catch(Exception)
            {
                throw new SequenceParserException(Operator, TypesHelper.XgrsTypeOfConstant(leftValue, procEnv.Graph.Model), TypesHelper.XgrsTypeOfConstant(rightValue, procEnv.Graph.Model), Symbol);
            }
        }

        public override int Precedence { get { return -1; } }
        public override string Operator { get { return " % "; } }
    }


    public class SequenceExpressionConstant : SequenceExpression
    {
        public object Constant;

        public SequenceExpressionConstant(object constant)
            : base(SequenceExpressionType.Constant)
        {
            Constant = constant;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(Constant != null)
                return TypesHelper.XgrsTypeOfConstant(Constant, env.Model);
            else
                return "";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionConstant copy = (SequenceExpressionConstant)MemberwiseClone();
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return Constant;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol
        {
            get
            {
                if(Constant == null)
                    return "null";
                else
                    return Constant.ToString();
            }
        }
    }
    
    public class SequenceExpressionVariable : SequenceExpression
    {
        public SequenceVariable Variable;

        public SequenceExpressionVariable(SequenceVariable var)
            : base(SequenceExpressionType.Variable)
        {
            Variable = var;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return Variable.Type;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionVariable copy = (SequenceExpressionVariable)MemberwiseClone();
            copy.Variable = Variable.Copy(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Variable.Name; } }
    }

    public class SequenceExpressionThis : SequenceExpression
    {
        public SequenceExpressionThis()
            : base(SequenceExpressionType.This)
        {
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
                return "graph";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionThis copy = (SequenceExpressionThis)MemberwiseClone();
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "this"; } }
    }
    
    public abstract class SequenceExpressionContainerConstructor : SequenceExpression
    {
        public String ValueType;
        public SequenceExpression[] ContainerItems;

        public SequenceExpressionContainerConstructor(SequenceExpressionType seqExprType, String valueType, SequenceExpression[] containerItems)
            : base(seqExprType)
        {
            ValueType = valueType;
            ContainerItems = containerItems;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            foreach(SequenceExpression containerItem in ContainerItems)
            {
                if(!TypesHelper.IsSameOrSubtype(containerItem.Type(env), ValueType, env.Model))
                {
                    throw new SequenceParserException(Symbol, ValueType, containerItem.Type(env));
                }
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            foreach(SequenceExpression containerItem in ContainerItems)
                containerItem.GetLocalVariables(variables, containerConstructors);
            containerConstructors.Add(this);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { foreach(SequenceExpression containerItem in ContainerItems) yield return containerItem; } }
        public override int Precedence { get { return 8; } }
    }

    public class SequenceExpressionSetConstructor : SequenceExpressionContainerConstructor
    {
        public SequenceExpressionSetConstructor(String valueType, SequenceExpression[] setItems)
            : base(SequenceExpressionType.SetConstructor, valueType, setItems)
        {
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "set<" + ValueType + ">";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionSetConstructor copy = (SequenceExpressionSetConstructor)MemberwiseClone();
            copy.ContainerItems = new SequenceExpression[ContainerItems.Length];
            for(int i = 0; i < ContainerItems.Length; ++i)
                copy.ContainerItems[i] = ContainerItems[i].CopyExpression(originalToCopy, procEnv);
            return copy;
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
                for(int i = 0; i < ContainerItems.Length; ++i)
                {
                    sb.Append(ContainerItems[i].Symbol);
                    if(i != ContainerItems.Length - 1) sb.Append(",");
                }
                sb.Append("}");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionMapConstructor : SequenceExpressionContainerConstructor
    {
        public String KeyType;
        public SequenceExpression[] MapKeyItems;

        public SequenceExpressionMapConstructor(String keyType, String valueType,
            SequenceExpression[] mapKeyItems, SequenceExpression[] mapValueItems)
            : base(SequenceExpressionType.MapConstructor, valueType, mapValueItems)
        {
            KeyType = keyType;
            MapKeyItems = mapKeyItems;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            foreach(SequenceExpression keyItem in MapKeyItems)
            {
                if(!TypesHelper.IsSameOrSubtype(keyItem.Type(env), KeyType, env.Model))
                {
                    throw new SequenceParserException(Symbol, KeyType, keyItem.Type(env));
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "map<" + KeyType + "," + ValueType + ">";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionMapConstructor copy = (SequenceExpressionMapConstructor)MemberwiseClone();
            copy.MapKeyItems = new SequenceExpression[MapKeyItems.Length];
            for(int i = 0; i < MapKeyItems.Length; ++i)
                copy.MapKeyItems[i] = MapKeyItems[i].CopyExpression(originalToCopy, procEnv);
            copy.ContainerItems = new SequenceExpression[ContainerItems.Length];
            for(int i = 0; i < ContainerItems.Length; ++i)
                copy.ContainerItems[i] = ContainerItems[i].CopyExpression(originalToCopy, procEnv);
            return copy;
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
                mapKeyItem.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        { 
            get
            { 
                foreach(SequenceExpression mapKeyItem in MapKeyItems) yield return mapKeyItem;
                foreach(SequenceExpression mapValueItem in ContainerItems) yield return mapValueItem;
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
                    if(i != MapKeyItems.Length - 1) sb.Append(",");
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

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "array<" + ValueType + ">";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionArrayConstructor copy = (SequenceExpressionArrayConstructor)MemberwiseClone();
            copy.ContainerItems = new SequenceExpression[ContainerItems.Length];
            for(int i = 0; i < ContainerItems.Length; ++i)
                copy.ContainerItems[i] = ContainerItems[i].CopyExpression(originalToCopy, procEnv);
            return copy;
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
                for(int i = 0; i < ContainerItems.Length; ++i)
                {
                    sb.Append(ContainerItems[i].Symbol);
                    if(i != ContainerItems.Length - 1) sb.Append(",");
                }
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

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "deque<" + ValueType + ">";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionDequeConstructor copy = (SequenceExpressionDequeConstructor)MemberwiseClone();
            copy.ContainerItems = new SequenceExpression[ContainerItems.Length];
            for(int i = 0; i < ContainerItems.Length; ++i)
                copy.ContainerItems[i] = ContainerItems[i].CopyExpression(originalToCopy, procEnv);
            return copy;
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
                for(int i = 0; i < ContainerItems.Length; ++i)
                {
                    sb.Append(ContainerItems[i].Symbol);
                    if(i != ContainerItems.Length - 1) sb.Append(",");
                }
                sb.Append("[");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionRandom : SequenceExpression
    {
        public SequenceExpression UpperBound;

        public SequenceExpressionRandom(SequenceExpression upperBound)
            : base(SequenceExpressionType.Random)
        {
            UpperBound = upperBound; // might be null
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
                {
                    throw new SequenceParserException(Symbol, "int", UpperBound.Type(env));
                }
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionRandom copy = (SequenceExpressionRandom)MemberwiseClone();
            if(UpperBound != null)
                copy.UpperBound = UpperBound.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(UpperBound != null) yield return UpperBound; else yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "random(" + (UpperBound!=null? "..." : "") + ")"; } }
    }

    public class SequenceExpressionDef : SequenceExpression
    {
        public SequenceExpression[] DefVars;

        public SequenceExpressionDef(SequenceExpression[] defVars)
            : base(SequenceExpressionType.Def)
        {
            DefVars = defVars;
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

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionDef copy = (SequenceExpressionDef)MemberwiseClone();
            copy.DefVars = new SequenceExpression[DefVars.Length];
            for(int i = 0; i < DefVars.Length; ++i)
                copy.DefVars[i] = DefVars[i].CopyExpression(originalToCopy, procEnv);
            return copy;
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
                defVar.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { foreach(SequenceExpression defVar in DefVars) yield return defVar; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("def(");
                for(int i = 0; i < DefVars.Length; ++i)
                {
                    sb.Append(DefVars[i].Symbol);
                    if(i != DefVars.Length - 1) sb.Append(",");
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceExpressionIsVisited : SequenceExpression
    {
        public SequenceVariable GraphElementVar;
        public SequenceExpression VisitedFlagExpr;

        public SequenceExpressionIsVisited(SequenceVariable graphElementVar, SequenceExpression visitedFlagExpr)
            : base(SequenceExpressionType.IsVisited)
        {
            GraphElementVar = graphElementVar;
            VisitedFlagExpr = visitedFlagExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(GraphElementVar.Type, env.Model);
            if(GraphElementVar.Type != "" && nodeOrEdgeType == null)
            {
                throw new SequenceParserException(Symbol, "node or edge type", GraphElementVar.Type);
            }
            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpr.Type(env), "int", env.Model))
            {
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpr.Type(env));
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionIsVisited copy = (SequenceExpressionIsVisited)MemberwiseClone();
            copy.GraphElementVar = GraphElementVar.Copy(originalToCopy, procEnv);
            copy.VisitedFlagExpr = VisitedFlagExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(procEnv);
            int visitedFlag = (int)VisitedFlagExpr.Evaluate(procEnv);
            return procEnv.Graph.IsVisited(elem, visitedFlag);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagExpr.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return GraphElementVar.Name + ".visited[" + VisitedFlagExpr.Symbol + "]"; } }
    }

    public class SequenceExpressionInContainer : SequenceExpression
    {
        public SequenceExpression Expr;
        public SequenceExpression ContainerExpr;

        public SequenceExpressionInContainer(SequenceExpression expr, SequenceExpression container)
            : base(SequenceExpressionType.InContainer)
        {
            Expr = expr;
            ContainerExpr = container;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
            {
                throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
            }
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

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionInContainer copy = (SequenceExpressionInContainer)MemberwiseClone();
            copy.ContainerExpr = ContainerExpr.CopyExpression(originalToCopy, procEnv);
            copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Expr.Symbol + " in " + ContainerExpr.Symbol; } }
    }

    public class SequenceExpressionContainerSize : SequenceExpressionContainer
    {
        public SequenceExpressionContainerSize(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ContainerSize, containerExpr)
        {
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

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionContainerSize copy = (SequenceExpressionContainerSize)MemberwiseClone();
            copy.ContainerExpr = ContainerExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return ContainerExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Name + ".size()"; } }
    }

    public class SequenceExpressionContainerEmpty : SequenceExpressionContainer
    {
        public SequenceExpressionContainerEmpty(SequenceExpression containerExpr)
            : base(SequenceExpressionType.ContainerEmpty, containerExpr)
        {
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

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionContainerEmpty copy = (SequenceExpressionContainerEmpty)MemberwiseClone();
            copy.ContainerExpr = ContainerExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return ContainerExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Name + ".empty()"; } }
    }

    public class SequenceExpressionContainerAccess : SequenceExpression
    {
        public SequenceExpression ContainerExpr;
        public SequenceExpression KeyExpr;

        public SequenceExpressionContainerAccess(SequenceExpression containerExpr, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerAccess)
        {
            ContainerExpr = containerExpr;
            KeyExpr = keyExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check source and destination types if the container is untyped, only runtime-check possible

            if(TypesHelper.ExtractDst(ContainerType) == "SetValueType")
            {
                throw new SequenceParserException(Symbol, "map<S,T> or array<S> or deque<S>", ContainerType);
            }
            if(ContainerType.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpr.Type(env));
                }
            }
            else if(ContainerType.StartsWith("deque"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpr.Type(env));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), KeyExpr.Type(env));
                }
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

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionContainerAccess copy = (SequenceExpressionContainerAccess)MemberwiseClone();
            copy.ContainerExpr = ContainerExpr.CopyExpression(originalToCopy, procEnv);
            copy.KeyExpr = KeyExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return ContainerExpr.Symbol + "[" + KeyExpr.Symbol + "]"; } }
    }

    public class SequenceExpressionContainerPeek : SequenceExpressionContainer
    {
        public SequenceExpression KeyExpr;

        public SequenceExpressionContainerPeek(SequenceExpression containerExpr, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerPeek, containerExpr)
        {
            KeyExpr = keyExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string containerType = CheckAndReturnContainerType(env);

            if(KeyExpr != null)
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpr.Type(env));
                }
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

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionContainerPeek copy = (SequenceExpressionContainerPeek)MemberwiseClone();
            copy.ContainerExpr = ContainerExpr.CopyExpression(originalToCopy, procEnv);
            if(KeyExpr != null) copy.KeyExpr = KeyExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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
            if(KeyExpr != null) KeyExpr.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(KeyExpr != null) yield return KeyExpr; yield return ContainerExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Name + ".peek(" + (KeyExpr != null ? KeyExpr.Symbol : "") + ")"; } }
    }

    public class SequenceExpressionElementFromGraph : SequenceExpression
    {
        public String ElementName;
        public bool EmitProfiling;

        public SequenceExpressionElementFromGraph(String elemName)
            : base(SequenceExpressionType.ElementFromGraph)
        {
            ElementName = elemName;
            if(ElementName[0] == '\"') ElementName = ElementName.Substring(1, ElementName.Length - 2);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionElementFromGraph copy = (SequenceExpressionElementFromGraph)MemberwiseClone();
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "@(" + ElementName + ")"; } }
    }

    public class SequenceExpressionNodeByName : SequenceExpression
    {
        public SequenceExpression NodeName;
        public bool EmitProfiling;

        public SequenceExpressionNodeByName(SequenceExpression nodeName)
            : base(SequenceExpressionType.NodeByName)
        {
            NodeName = nodeName;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeName.Type(env) != "")
            {
                if(NodeName.Type(env) != "string")
                {
                    throw new SequenceParserException(Symbol + ", argument", "string", NodeName.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionNodeByName copy = (SequenceExpressionNodeByName)MemberwiseClone();
            copy.NodeName = NodeName.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph is INamedGraph))
                throw new InvalidOperationException("The nodeByName(.) call can only be used with named graphs!");
            INamedGraph namedGraph = (INamedGraph)procEnv.Graph;
            if(EmitProfiling)
                return GraphHelper.GetNode(namedGraph, (string)NodeName.Evaluate(procEnv), procEnv);
            else
                return GraphHelper.GetNode(namedGraph, (string)NodeName.Evaluate(procEnv));
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return NodeName; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "getNodeByName(" + NodeName.Symbol + ")"; } }
    }

    public class SequenceExpressionEdgeByName : SequenceExpression
    {
        public SequenceExpression EdgeName;
        public bool EmitProfiling;

        public SequenceExpressionEdgeByName(SequenceExpression edgeName)
            : base(SequenceExpressionType.EdgeByName)
        {
            EdgeName = edgeName;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeName.Type(env) != "")
            {
                if(EdgeName.Type(env) != "string")
                {
                    throw new SequenceParserException(Symbol + ", argument", "string", EdgeName.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Edge";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionEdgeByName copy = (SequenceExpressionEdgeByName)MemberwiseClone();
            copy.EdgeName = EdgeName.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph is INamedGraph))
                throw new InvalidOperationException("The edgeByName(.) call can only be used with named graphs!");
            INamedGraph namedGraph = (INamedGraph)procEnv.Graph;
            if(EmitProfiling)
                return GraphHelper.GetEdge(namedGraph, (string)EdgeName.Evaluate(procEnv), procEnv);
            else
                return GraphHelper.GetEdge(namedGraph, (string)EdgeName.Evaluate(procEnv));
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return EdgeName; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "getEdgeByName(" + EdgeName.Symbol + ")"; } }
    }

    public class SequenceExpressionNodeByUnique : SequenceExpression
    {
        public SequenceExpression NodeUniqueId;
        public bool EmitProfiling;

        public SequenceExpressionNodeByUnique(SequenceExpression nodeUniqueId)
            : base(SequenceExpressionType.NodeByUnique)
        {
            NodeUniqueId = nodeUniqueId;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeUniqueId.Type(env) != "")
            {
                if(NodeUniqueId.Type(env) != "int")
                {
                    throw new SequenceParserException(Symbol + ", argument", "int", NodeUniqueId.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionNodeByUnique copy = (SequenceExpressionNodeByUnique)MemberwiseClone();
            copy.NodeUniqueId = NodeUniqueId.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph.Model.GraphElementsAreAccessibleByUniqueId))
                throw new InvalidOperationException("The nodeByUnique(.) call can only be used on graphs with a node edge unique definition!");
            if(EmitProfiling)
                return GraphHelper.GetNode(procEnv.Graph, (int)NodeUniqueId.Evaluate(procEnv), procEnv);
            else
                return GraphHelper.GetNode(procEnv.Graph, (int)NodeUniqueId.Evaluate(procEnv));
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return NodeUniqueId; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "getNodeByUnique(" + NodeUniqueId.Symbol + ")"; } }
    }

    public class SequenceExpressionEdgeByUnique : SequenceExpression
    {
        public SequenceExpression EdgeUniqueId;
        public bool EmitProfiling;

        public SequenceExpressionEdgeByUnique(SequenceExpression edgeName)
            : base(SequenceExpressionType.EdgeByUnique)
        {
            EdgeUniqueId = edgeName;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeUniqueId.Type(env) != "")
            {
                if(EdgeUniqueId.Type(env) != "int")
                {
                    throw new SequenceParserException(Symbol + ", argument", "int", EdgeUniqueId.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Edge";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionEdgeByUnique copy = (SequenceExpressionEdgeByUnique)MemberwiseClone();
            copy.EdgeUniqueId = EdgeUniqueId.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(!(procEnv.Graph.Model.GraphElementsAreAccessibleByUniqueId))
                throw new InvalidOperationException("The edgeByUnique(.) call can only be used on graphs with a node edge unique definition!");
            IEdge edge = procEnv.Graph.GetEdge((int)EdgeUniqueId.Evaluate(procEnv));
            if(EmitProfiling)
                return GraphHelper.GetEdge(procEnv.Graph, (int)EdgeUniqueId.Evaluate(procEnv), procEnv);
            else
                return GraphHelper.GetEdge(procEnv.Graph, (int)EdgeUniqueId.Evaluate(procEnv));
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return EdgeUniqueId; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "getEdgeByUnique(" + EdgeUniqueId.Symbol + ")"; } }
    }

    public class SequenceExpressionSource : SequenceExpression
    {
        public SequenceExpression Edge;

        public SequenceExpressionSource(SequenceExpression edge)
            : base(SequenceExpressionType.Source)
        {
            Edge = edge;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionSource copy = (SequenceExpressionSource)MemberwiseClone();
            copy.Edge = Edge.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.Execute(procEnv)).Source;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Edge; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "source(" + Edge.Symbol + ")"; } }
    }

    public class SequenceExpressionTarget : SequenceExpression
    {
        public SequenceExpression Edge;

        public SequenceExpressionTarget(SequenceExpression edge)
            : base(SequenceExpressionType.Target)
        {
            Edge = edge;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionSource copy = (SequenceExpressionSource)MemberwiseClone();
            copy.Edge = Edge.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.Execute(procEnv)).Target;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Edge; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "target(" + Edge.Symbol + ")"; } }
    }

    public class SequenceExpressionOpposite : SequenceExpression
    {
        public SequenceExpression Edge;
        public SequenceExpression Node;

        public SequenceExpressionOpposite(SequenceExpression edge, SequenceExpression node)
            : base(SequenceExpressionType.Opposite)
        {
            Edge = edge;
            Node = node;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "Node";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionOpposite copy = (SequenceExpressionOpposite)MemberwiseClone();
            copy.Edge = Edge.CopyExpression(originalToCopy, procEnv);
            copy.Node = Node.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return ((IEdge)Edge.Execute(procEnv)).Opposite((INode)Node.Execute(procEnv));
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Edge; yield return Node; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "opposite(" + Edge.Symbol + "," + Node.Symbol + ")"; } }
    }

    public class SequenceExpressionAttributeAccess : SequenceExpression
    {
        public SequenceVariable SourceVar;
        public String AttributeName;

        public SequenceExpressionAttributeAccess(SequenceVariable sourceVar, String attributeName)
            : base(SequenceExpressionType.GraphElementAttribute)
        {
            SourceVar = sourceVar;
            AttributeName = attributeName;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            CheckAndReturnAttributeType(env);
        }

        public string CheckAndReturnAttributeType(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceVar.Type == "")
                return ""; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(SourceVar.Type, env.Model);
            if(nodeOrEdgeType == null)
            {
                throw new SequenceParserException(Symbol, "node or edge type", SourceVar.Type);
            }
            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(AttributeName);
            if(attributeType == null)
            {
                throw new SequenceParserException(AttributeName, SequenceParserError.UnknownAttribute);
            }

            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(SourceVar.Type == "")
                return ""; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            
            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(SourceVar.Type, env.Model);
            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(AttributeName);
            if(attributeType == null)
                return ""; // error, will be reported by Check, just ensure we don't crash here

            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionAttributeAccess copy = (SequenceExpressionAttributeAccess)MemberwiseClone();
            copy.SourceVar = SourceVar.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)SourceVar.GetVariableValue(procEnv);
            object value = elem.GetAttribute(AttributeName);
            value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                elem, AttributeName, value);
            return value;
        }

        public object ExecuteNoImplicitContainerCopy(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)SourceVar.GetVariableValue(procEnv);
            object value = elem.GetAttribute(AttributeName);
            return value;
        }

        public object ExecuteNoImplicitContainerCopy(IGraphProcessingEnvironment procEnv, out IGraphElement elem, out AttributeType attrType)
        {
            elem = (IGraphElement)SourceVar.GetVariableValue(procEnv);
            object value = elem.GetAttribute(AttributeName);
            attrType = elem.Type.GetAttributeType(AttributeName);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceVar.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return SourceVar.Name + "." + AttributeName; } }
    }

    public class SequenceExpressionMatchAccess : SequenceExpression
    {
        public SequenceVariable SourceVar;
        public String ElementName;

        public SequenceExpressionMatchAccess(SequenceVariable sourceVar, String elementName)
            : base(SequenceExpressionType.ElementOfMatch)
        {
            SourceVar = sourceVar;
            ElementName = elementName;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!SourceVar.Type.StartsWith("match<"))
                throw new Exception("SequenceExpression MatchAccess can only access a variable of type match<rulename>");

            string ruleName = TypesHelper.ExtractSrc(SourceVar.Type);

            // throws exceptions in case the rule does not exist, or it does not contain an element of the given name
            string elementType = env.TypeOfTopLevelEntityInRule(ruleName, ElementName);

            if(elementType == "")
                throw new Exception("Internal failure, static type of element in match type not known");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string ruleName = TypesHelper.ExtractSrc(SourceVar.Type);
            return env.TypeOfTopLevelEntityInRule(ruleName, ElementName);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionMatchAccess copy = (SequenceExpressionMatchAccess)MemberwiseClone();
            copy.SourceVar = SourceVar.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IMatch match = (IMatch)SourceVar.GetVariableValue(procEnv);
            object value = match.getNode(ElementName);
            if(value != null) return value;
            value = match.getEdge(ElementName);
            if(value != null) return value;
            value = match.getVariable(ElementName);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            SourceVar.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return SourceVar.Name + "." + ElementName; } }
    }

    public class SequenceExpressionNodes : SequenceExpression
    {
        public SequenceExpression NodeType;
        public bool EmitProfiling;

        public SequenceExpressionNodes(SequenceExpression nodeType)
            : base(SequenceExpressionType.Nodes)
        {
            NodeType = nodeType;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeType!=null && NodeType.Type(env)!="")
            {
                string typeString = null;
                if(NodeType.Type(env) == "string")
                {
                    if(NodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)NodeType).Constant;
                }
                else
                {
                    typeString = NodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "set<Node>";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionNodes copy = (SequenceExpressionNodes)MemberwiseClone();
            if(NodeType!=null)copy.NodeType = NodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = null;
            if(NodeType != null)
            {
                object tmp = NodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

            if(EmitProfiling)
                return GraphHelper.Nodes(procEnv.Graph, nodeType, procEnv);
            else
                return GraphHelper.Nodes(procEnv.Graph, nodeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(NodeType != null) NodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(NodeType != null) yield return NodeType; } }
        public override int Precedence { get { return 8; } }
        public string FunctionSymbol
        {
            get { return "nodes" ; }
        }
        public override string Symbol
        {
            get { return FunctionSymbol + "(" + (NodeType != null ? NodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionEdges : SequenceExpression
    {
        public SequenceExpression EdgeType;
        public bool EmitProfiling;

        public SequenceExpressionEdges(SequenceExpression edgeType)
            : base(SequenceExpressionType.Edges)
        {
            EdgeType = edgeType;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeType!=null && EdgeType.Type(env)!="")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", argument", "edge type or string denoting edge type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "set<Edge>";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionEdges copy = (SequenceExpressionEdges)MemberwiseClone();
            if(EdgeType!=null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }

            if(EmitProfiling)
                return GraphHelper.Edges(procEnv.Graph, edgeType, procEnv);
            else
                return GraphHelper.Edges(procEnv.Graph, edgeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(EdgeType != null) yield return EdgeType; } }
        public override int Precedence { get { return 8; } }
        public string FunctionSymbol
        {
            get { return "edges"; }
        }
        public override string Symbol
        {
            get { return FunctionSymbol + "(" + (EdgeType != null ? EdgeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountNodes : SequenceExpression
    {
        public SequenceExpression NodeType;
        public bool EmitProfiling;

        public SequenceExpressionCountNodes(SequenceExpression nodeType)
            : base(SequenceExpressionType.CountNodes)
        {
            NodeType = nodeType;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeType != null && NodeType.Type(env) != "")
            {
                string typeString = null;
                if(NodeType.Type(env) == "string")
                {
                    if(NodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)NodeType).Constant;
                }
                else
                {
                    typeString = NodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCountNodes copy = (SequenceExpressionCountNodes)MemberwiseClone();
            if(NodeType != null) copy.NodeType = NodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            NodeType nodeType = null;
            if(NodeType != null)
            {
                object tmp = NodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

            if(EmitProfiling)
                return GraphHelper.CountNodes(procEnv.Graph, nodeType, procEnv);
            else
                return GraphHelper.CountNodes(procEnv.Graph, nodeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(NodeType != null) NodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(NodeType != null) yield return NodeType; } }
        public override int Precedence { get { return 8; } }
        public string FunctionSymbol
        {
            get { return "countNodes"; }
        }
        public override string Symbol
        {
            get { return FunctionSymbol + "(" + (NodeType != null ? NodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountEdges : SequenceExpression
    {
        public SequenceExpression EdgeType;
        public bool EmitProfiling;

        public SequenceExpressionCountEdges(SequenceExpression edgeType)
            : base(SequenceExpressionType.CountEdges)
        {
            EdgeType = edgeType;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeType != null && EdgeType.Type(env) != "")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", argument", "edge type or string denoting edge type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCountEdges copy = (SequenceExpressionCountEdges)MemberwiseClone();
            if(EdgeType != null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }

            if(EmitProfiling)
                return GraphHelper.CountEdges(procEnv.Graph, edgeType, procEnv);
            else
                return GraphHelper.CountEdges(procEnv.Graph, edgeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(EdgeType != null) yield return EdgeType; } }
        public override int Precedence { get { return 8; } }
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

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionEmpty copy = (SequenceExpressionEmpty)MemberwiseClone();
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.NumNodes + procEnv.Graph.NumEdges == 0;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "empty()"; } }
    }

    public class SequenceExpressionNow : SequenceExpression
    {
        public SequenceExpressionNow()
            : base(SequenceExpressionType.Now)
        {
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "long";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionNow copy = (SequenceExpressionNow)MemberwiseClone();
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return DateTime.UtcNow.ToFileTime();
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "Time::now()"; } }
    }

    public class SequenceExpressionSize : SequenceExpression
    {
        public SequenceExpressionSize()
            : base(SequenceExpressionType.Size)
        {
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionSize copy = (SequenceExpressionSize)MemberwiseClone();
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.NumNodes + procEnv.Graph.NumEdges;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "size()"; } }
    }

    public class SequenceExpressionAdjacentIncident : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionAdjacentIncident(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.AdjacentNodes || type == SequenceExpressionType.AdjacentNodesViaIncoming || type == SequenceExpressionType.AdjacentNodesViaOutgoing)
                && !(type == SequenceExpressionType.IncidentEdges || type == SequenceExpressionType.IncomingEdges || type == SequenceExpressionType.OutgoingEdges))
                throw new Exception("Internal failure, adjacent/incident with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(EdgeType!=null && EdgeType.Type(env)!="")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", second argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "node type or string denoting node type", typeString);
                }
            }
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
            {
                return "set<Edge>";
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionAdjacentIncident copy = (SequenceExpressionAdjacentIncident)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            if(EdgeType!=null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountAdjacentIncident : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionCountAdjacentIncident(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.CountAdjacentNodes || type == SequenceExpressionType.CountAdjacentNodesViaIncoming || type == SequenceExpressionType.CountAdjacentNodesViaOutgoing)
                && !(type == SequenceExpressionType.CountIncidentEdges || type == SequenceExpressionType.CountIncomingEdges || type == SequenceExpressionType.CountOutgoingEdges))
                throw new Exception("Internal failure, count adjacent/incident with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(EdgeType!=null && EdgeType.Type(env)!="")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", second argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCountAdjacentIncident copy = (SequenceExpressionCountAdjacentIncident)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            if(EdgeType!=null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
                if(SequenceExpressionType == SequenceExpressionType.CountAdjacentNodes || SequenceExpressionType == SequenceExpressionType.CountAdjacentNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.CountAdjacentNodesViaOutgoing)
                    return FunctionSymbol + "graph, " + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")";
                else
                    return FunctionSymbol + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")";
            }
        }
    }

    public class SequenceExpressionReachable : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionReachable(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.ReachableNodes || type == SequenceExpressionType.ReachableNodesViaIncoming || type == SequenceExpressionType.ReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.ReachableEdges || type == SequenceExpressionType.ReachableEdgesViaIncoming || type == SequenceExpressionType.ReachableEdgesViaOutgoing))
                throw new Exception("Internal failure, reachable with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(EdgeType!=null && EdgeType.Type(env)!="")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", second argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "node type or string denoting node type", typeString);
                }
            }
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
            {
                return "set<Edge>";
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionReachable copy = (SequenceExpressionReachable)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            if(EdgeType!=null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountReachable : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionCountReachable(SequenceExpression sourceNode, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.CountReachableNodes || type == SequenceExpressionType.CountReachableNodesViaIncoming || type == SequenceExpressionType.CountReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.CountReachableEdges || type == SequenceExpressionType.CountReachableEdgesViaIncoming || type == SequenceExpressionType.CountReachableEdgesViaOutgoing))
                throw new Exception("Internal failure, count reachable with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(EdgeType!=null && EdgeType.Type(env)!="")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", second argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCountReachable copy = (SequenceExpressionCountReachable)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            if(EdgeType!=null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionBoundedReachable : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression Depth;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionBoundedReachable(SequenceExpression sourceNode, SequenceExpression depth, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            Depth = depth;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.BoundedReachableNodes || type == SequenceExpressionType.BoundedReachableNodesViaIncoming || type == SequenceExpressionType.BoundedReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.BoundedReachableEdges || type == SequenceExpressionType.BoundedReachableEdgesViaIncoming || type == SequenceExpressionType.BoundedReachableEdgesViaOutgoing))
                throw new Exception("Internal failure, bounded reachable with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                {
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
                }
            }
            if(EdgeType != null && EdgeType.Type(env) != "")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", fourth argument", "node type or string denoting node type", typeString);
                }
            }
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
            {
                return "set<Edge>";
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionBoundedReachable copy = (SequenceExpressionBoundedReachable)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            copy.Depth = Depth.CopyExpression(originalToCopy, procEnv);
            if(EdgeType != null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; yield return Depth; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionBoundedReachableWithRemainingDepth : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression Depth;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

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

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                {
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
                }
            }
            if(EdgeType != null && EdgeType.Type(env) != "")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", fourth argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "map<Node,int>";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionBoundedReachableWithRemainingDepth copy = (SequenceExpressionBoundedReachableWithRemainingDepth)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            copy.Depth = Depth.CopyExpression(originalToCopy, procEnv);
            if(EdgeType != null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; yield return Depth; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionCountBoundedReachable : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression Depth;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionCountBoundedReachable(SequenceExpression sourceNode, SequenceExpression depth, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            Depth = depth;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.CountBoundedReachableNodes || type == SequenceExpressionType.CountBoundedReachableNodesViaIncoming || type == SequenceExpressionType.CountBoundedReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.CountBoundedReachableEdges || type == SequenceExpressionType.CountBoundedReachableEdgesViaIncoming || type == SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing))
                throw new Exception("Internal failure, count bounded reachable with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                {
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
                }
            }
            if(EdgeType != null && EdgeType.Type(env) != "")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", fourth argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCountBoundedReachable copy = (SequenceExpressionCountBoundedReachable)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            copy.Depth = Depth.CopyExpression(originalToCopy, procEnv);
            if(EdgeType != null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; yield return Depth; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionIsBoundedReachable : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EndElement;
        public SequenceExpression Depth;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

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
                throw new Exception("Internal failure, is bounded reachable with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodes || SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaOutgoing)
                {
                    NodeType nodeType = TypesHelper.GetNodeType(EndElement.Type(env), env.Model);
                    if(nodeType == null)
                    {
                        throw new SequenceParserException(Symbol + ", second argument", "node type", EndElement.Type(env));
                    }
                }
                else
                {
                    EdgeType edgeType = TypesHelper.GetEdgeType(EndElement.Type(env), env.Model);
                    if(edgeType == null)
                    {
                        throw new SequenceParserException(Symbol + ", second argument", "edge type", EndElement.Type(env));
                    }
                }
            }
            if(Depth.Type(env) != "")
            {
                if(Depth.Type(env) != "int")
                {
                    throw new SequenceParserException(Symbol + ", second argument", "int type", Depth.Type(env));
                }
            }
            if(EdgeType != null && EdgeType.Type(env) != "")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", fourth argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionIsBoundedReachable copy = (SequenceExpressionIsBoundedReachable)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            copy.EndElement = EndElement.CopyExpression(originalToCopy, procEnv);
            copy.Depth = Depth.CopyExpression(originalToCopy, procEnv);
            if(EdgeType != null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            IGraphElement endElement = (IGraphElement)EndElement.Evaluate(procEnv);
            int depth = (int)Depth.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; yield return Depth; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + "," + EndElement.Symbol + "," + Depth.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionIsAdjacentIncident : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EndElement;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionIsAdjacentIncident(SequenceExpression sourceNode, SequenceExpression endElement, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EndElement = endElement;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.IsAdjacentNodes || type == SequenceExpressionType.IsAdjacentNodesViaIncoming || type == SequenceExpressionType.IsAdjacentNodesViaOutgoing)
                && !(type == SequenceExpressionType.IsIncidentEdges || type == SequenceExpressionType.IsIncomingEdges || type == SequenceExpressionType.IsOutgoingEdges))
                throw new Exception("Internal failure, isAdjacent/isIncident with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsAdjacentNodes || SequenceExpressionType == SequenceExpressionType.IsAdjacentNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsAdjacentNodesViaOutgoing)
                {
                    NodeType nodeType = TypesHelper.GetNodeType(EndElement.Type(env), env.Model);
                    if(nodeType == null)
                    {
                        throw new SequenceParserException(Symbol + ", second argument", "node type", EndElement.Type(env));
                    }
                }
                else
                {
                    EdgeType edgeType = TypesHelper.GetEdgeType(EndElement.Type(env), env.Model);
                    if(edgeType == null)
                    {
                        throw new SequenceParserException(Symbol + ", second argument", "edge type", EndElement.Type(env));
                    }
                }
            }
            if(EdgeType != null && EdgeType.Type(env) != "")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", fourth argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionIsAdjacentIncident copy = (SequenceExpressionIsAdjacentIncident)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            copy.EndElement = EndElement.CopyExpression(originalToCopy, procEnv);
            if(EdgeType != null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            IGraphElement endElement = (IGraphElement)EndElement.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; yield return EndElement; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + "," + EndElement.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionIsReachable : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EndElement;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;
        public bool EmitProfiling;

        public SequenceExpressionIsReachable(SequenceExpression sourceNode, SequenceExpression endElement, SequenceExpression edgeType, SequenceExpression oppositeNodeType, SequenceExpressionType type)
            : base(type)
        {
            SourceNode = sourceNode;
            EndElement = endElement;
            EdgeType = edgeType;
            OppositeNodeType = oppositeNodeType;
            if(!(type == SequenceExpressionType.IsReachableNodes || type == SequenceExpressionType.IsReachableNodesViaIncoming || type == SequenceExpressionType.IsReachableNodesViaOutgoing)
                && !(type == SequenceExpressionType.IsReachableEdges || type == SequenceExpressionType.IsReachableEdgesViaIncoming || type == SequenceExpressionType.IsReachableEdgesViaOutgoing))
                throw new Exception("Internal failure, reachable with wrong type");
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceNode.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                NodeType nodeType = TypesHelper.GetNodeType(SourceNode.Type(env), env.Model);
                if(nodeType == null)
                {
                    throw new SequenceParserException(Symbol+", first argument", "node type", SourceNode.Type(env));
                }
            }
            if(EndElement.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(SequenceExpressionType == SequenceExpressionType.IsReachableNodes || SequenceExpressionType == SequenceExpressionType.IsReachableNodesViaIncoming || SequenceExpressionType == SequenceExpressionType.IsReachableNodesViaOutgoing)
                {
                    NodeType nodeType = TypesHelper.GetNodeType(EndElement.Type(env), env.Model);
                    if(nodeType == null)
                    {
                        throw new SequenceParserException(Symbol + ", second argument", "node type", EndElement.Type(env));
                    }
                }
                else
                {
                    EdgeType edgeType = TypesHelper.GetEdgeType(EndElement.Type(env), env.Model);
                    if(edgeType == null)
                    {
                        throw new SequenceParserException(Symbol + ", second argument", "edge type", EndElement.Type(env));
                    }
                }
            }
            if(EdgeType != null && EdgeType.Type(env) != "")
            {
                string typeString = null;
                if(EdgeType.Type(env) == "string")
                {
                    if(EdgeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)EdgeType).Constant;
                }
                else
                {
                    typeString = EdgeType.Type(env);
                }
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", third argument", "edge type or string denoting edge type", typeString);
                }
            }
            if(OppositeNodeType!=null && OppositeNodeType.Type(env)!="")
            {
                string typeString = null;
                if(OppositeNodeType.Type(env) == "string")
                {
                    if(OppositeNodeType is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)OppositeNodeType).Constant;
                }
                else
                {
                    typeString = OppositeNodeType.Type(env);
                }
                NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                if(nodeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", fourth argument", "node type or string denoting node type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionIsAdjacentIncident copy = (SequenceExpressionIsAdjacentIncident)MemberwiseClone();
            copy.SourceNode = SourceNode.CopyExpression(originalToCopy, procEnv);
            copy.EndElement = EndElement.CopyExpression(originalToCopy, procEnv);
            if(EdgeType != null) copy.EdgeType = EdgeType.CopyExpression(originalToCopy, procEnv);
            if(OppositeNodeType!=null)copy.OppositeNodeType = OppositeNodeType.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            INode sourceNode = (INode)SourceNode.Evaluate(procEnv);
            IGraphElement endElement = (IGraphElement)EndElement.Evaluate(procEnv);
            EdgeType edgeType = null;
            if(EdgeType != null)
            {
                object tmp = EdgeType.Evaluate(procEnv);
                if(tmp is string) edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType) edgeType = (EdgeType)tmp;
                if(edgeType == null) throw new Exception("edge type argument to " + FunctionSymbol + " is not an edge type");
            }
            else
            {
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;
            }
            NodeType nodeType = null;
            if(OppositeNodeType != null)
            {
                object tmp = OppositeNodeType.Evaluate(procEnv);
                if(tmp is string) nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType) nodeType = (NodeType)tmp;
                if(nodeType == null) throw new Exception("node type argument to " + FunctionSymbol + " is not a node type");
            }
            else
            {
                nodeType = procEnv.Graph.Model.NodeModel.RootType;
            }

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
            if(EdgeType != null) EdgeType.GetLocalVariables(variables, containerConstructors);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables, containerConstructors);
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            EmitProfiling = profiling;
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return SourceNode; yield return EndElement; if(EdgeType != null) yield return EdgeType; if(OppositeNodeType != null) yield return OppositeNodeType; } }
        public override int Precedence { get { return 8; } }
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
            get { return FunctionSymbol + SourceNode.Symbol + "," + EndElement.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")"; }
        }
    }

    public class SequenceExpressionInducedSubgraph : SequenceExpression
    {
        public SequenceExpression NodeSet;

        public SequenceExpressionInducedSubgraph(SequenceExpression nodeSet)
            : base(SequenceExpressionType.InducedSubgraph)
        {
            NodeSet = nodeSet;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeSet.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!NodeSet.Type(env).StartsWith("set<"))
            {
                throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
            }
            if(TypesHelper.ExtractSrc(NodeSet.Type(env))!="Node")
            {
                throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionInducedSubgraph copy = (SequenceExpressionInducedSubgraph)MemberwiseClone();
            copy.NodeSet = NodeSet.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return NodeSet; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "inducedSubgraph(" + NodeSet.Symbol + ")"; } }
    }

    public class SequenceExpressionDefinedSubgraph : SequenceExpression
    {
        public SequenceExpression EdgeSet;

        public SequenceExpressionDefinedSubgraph(SequenceExpression edgeSet)
            : base(SequenceExpressionType.DefinedSubgraph)
        {
            EdgeSet = edgeSet;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeSet.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!EdgeSet.Type(env).StartsWith("set<"))
            {
                throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
            }
            if(TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "Edge")
            {
                throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionDefinedSubgraph copy = (SequenceExpressionDefinedSubgraph)MemberwiseClone();
            copy.EdgeSet = EdgeSet.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object edgeSet = EdgeSet.Evaluate(procEnv);
            return GraphHelper.DefinedSubgraph((IDictionary<IEdge, SetValueType>)edgeSet, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            EdgeSet.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return EdgeSet; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "definedSubgraph(" + EdgeSet.Symbol + ")"; } }
    }

    public class SequenceExpressionEqualsAny : SequenceExpression
    {
        public SequenceExpression Subgraph;
        public SequenceExpression SubgraphSet;
        public bool IncludingAttributes;

        public SequenceExpressionEqualsAny(SequenceExpression subgraph, SequenceExpression subgraphSet, bool includingAttributes)
            : base(SequenceExpressionType.EqualsAny)
        {
            Subgraph = subgraph;
            SubgraphSet = subgraphSet;
            IncludingAttributes = includingAttributes;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Subgraph.Type(env) != "")
            {
                if(Subgraph.Type(env) != "graph")
                {
                    throw new SequenceParserException(Symbol + ", first argument", "graph type", Subgraph.Type(env));
                }
            }

            if(SubgraphSet.Type(env) != "")
            {
                if(!SubgraphSet.Type(env).StartsWith("set<"))
                {
                    throw new SequenceParserException(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
                }
                if(TypesHelper.ExtractSrc(SubgraphSet.Type(env)) != "graph")
                {
                    throw new SequenceParserException(Symbol + ", second argument", "set<graph> type", SubgraphSet.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionEqualsAny copy = (SequenceExpressionEqualsAny)MemberwiseClone();
            copy.Subgraph = Subgraph.CopyExpression(originalToCopy, procEnv);
            copy.SubgraphSet = SubgraphSet.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Subgraph; yield return SubgraphSet; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { 
            if(IncludingAttributes) return "equalsAny(" + Subgraph.Symbol + ", " + SubgraphSet.Symbol + ")"; 
            else return "equalsAnyStructurally(" + Subgraph.Symbol + ", " + SubgraphSet.Symbol + ")"; 
        } }
    }

    public class SequenceExpressionCanonize : SequenceExpression
    {
        public SequenceExpression Graph;

        public SequenceExpressionCanonize(SequenceExpression graph)
            : base(SequenceExpressionType.Canonize)
        {
            Graph = graph;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Graph.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(Graph.Type(env) != "graph")
            {
                throw new SequenceParserException(Symbol, "graph type", Graph.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "string";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCanonize copy = (SequenceExpressionCanonize)MemberwiseClone();
            copy.Graph = Graph.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Graph; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "canonize(" + Graph.Symbol + ")"; } }
    }

    public class SequenceExpressionNameof : SequenceExpression
    {
        public SequenceExpression NamedEntity;

        public SequenceExpressionNameof(SequenceExpression namedEntity)
            : base(SequenceExpressionType.Nameof)
        {
            NamedEntity = namedEntity; // might be null
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
                    && !TypesHelper.IsSameOrSubtype(NamedEntity.Type(env), "Edge", env.Model)
                    && !TypesHelper.IsSameOrSubtype(NamedEntity.Type(env), "graph", env.Model))
                {
                    throw new SequenceParserException(Symbol, "node or edge or graph type", NamedEntity.Type(env));
                }
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionNameof copy = (SequenceExpressionNameof)MemberwiseClone();
            if(NamedEntity != null)
                copy.NamedEntity = NamedEntity.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(NamedEntity != null) yield return NamedEntity; else yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "nameof(" + (NamedEntity != null ? NamedEntity.Symbol : "") + ")"; } }
    }

    public class SequenceExpressionUniqueof : SequenceExpression
    {
        public SequenceExpression UniquelyIdentifiedEntity;

        public SequenceExpressionUniqueof(SequenceExpression uniquelyIdentifiedEntity)
            : base(SequenceExpressionType.Uniqueof)
        {
            UniquelyIdentifiedEntity = uniquelyIdentifiedEntity; // might be null
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
                    && !TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "Edge", env.Model)
                    && !TypesHelper.IsSameOrSubtype(UniquelyIdentifiedEntity.Type(env), "graph", env.Model))
                {
                    throw new SequenceParserException(Symbol, "node or edge or graph type", UniquelyIdentifiedEntity.Type(env));
                }
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionUniqueof copy = (SequenceExpressionUniqueof)MemberwiseClone();
            if(UniquelyIdentifiedEntity != null)
                copy.UniquelyIdentifiedEntity = UniquelyIdentifiedEntity.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(UniquelyIdentifiedEntity != null) yield return UniquelyIdentifiedEntity; else yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "uniqueof(" + (UniquelyIdentifiedEntity != null ? UniquelyIdentifiedEntity.Symbol : "") + ")"; } }
    }

    public class SequenceExpressionExistsFile : SequenceExpression
    {
        public SequenceExpression Path;

        public SequenceExpressionExistsFile(SequenceExpression path)
            : base(SequenceExpressionType.ExistsFile)
        {
            Path = path;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Path.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!TypesHelper.IsSameOrSubtype(Path.Type(env), "string", env.Model))
            {
                throw new SequenceParserException(Symbol, "string type", Path.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionExistsFile copy = (SequenceExpressionExistsFile)MemberwiseClone();
            copy.Path = Path.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Path; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "File::existsFile(" + Path.Symbol + ")"; } }
    }

    public class SequenceExpressionImport : SequenceExpression
    {
        public SequenceExpression Path;

        public SequenceExpressionImport(SequenceExpression path)
            : base(SequenceExpressionType.Import)
        {
            Path = path;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Path.Type(env) == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            if(!TypesHelper.IsSameOrSubtype(Path.Type(env), "string", env.Model))
            {
                throw new SequenceParserException(Symbol, "string type", Path.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionImport copy = (SequenceExpressionImport)MemberwiseClone();
            copy.Path = Path.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object path = Path.Evaluate(procEnv);
            return GraphHelper.Import(path, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Path.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Path; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "File::import(" + Path.Symbol + ")"; } }
    }

    public class SequenceExpressionCopy : SequenceExpression
    {
        public SequenceExpression ObjectToBeCopied;

        public SequenceExpressionCopy(SequenceExpression objectToBeCopied)
            : base(SequenceExpressionType.Copy)
        {
            ObjectToBeCopied = objectToBeCopied;
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

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionCopy copy = (SequenceExpressionCopy)MemberwiseClone();
            copy.ObjectToBeCopied = ObjectToBeCopied.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return ObjectToBeCopied; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "copy(" + ObjectToBeCopied.Symbol + ")"; } }
    }

    public class SequenceExpressionFunctionCall : SequenceExpression
    {
        public FunctionInvocationParameterBindings ParamBindings;

        public SequenceExpressionFunctionCall(FunctionInvocationParameterBindings paramBindings)
            : base(SequenceExpressionType.FunctionCall)
        {
            ParamBindings = paramBindings;
        }

        public SequenceExpressionFunctionCall(SequenceExpressionType seqExprType, FunctionInvocationParameterBindings paramBindings)
            : base(seqExprType)
        {
            ParamBindings = paramBindings;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckFunctionCall(this);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(ParamBindings.FunctionDef != null)
               return TypesHelper.DotNetTypeToXgrsType(ParamBindings.FunctionDef.Output);
            else // compiled sequence
               return ParamBindings.ReturnType;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionFunctionCall copy = (SequenceExpressionFunctionCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IFunctionDefinition funcDef = ParamBindings.FunctionDef;
            for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; i++)
            {
                if(ParamBindings.ArgumentExpressions[i] != null)
                    ParamBindings.Arguments[i] = ParamBindings.ArgumentExpressions[i].Evaluate(procEnv);
            }
            object res = funcDef.Apply(procEnv, procEnv.Graph, ParamBindings);
            return res;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ParamBindings.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { foreach(SequenceExpression argument in ParamBindings.ArgumentExpressions) yield return argument; }
        }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return ParamBindings.Name + "(...)"; } }
    }

    public class SequenceExpressionFunctionMethodCall : SequenceExpressionFunctionCall
    {
        public SequenceExpression TargetExpr;

        public SequenceExpressionFunctionMethodCall(SequenceExpression targetExpr, FunctionInvocationParameterBindings paramBindings)
            : base(SequenceExpressionType.FunctionMethodCall, paramBindings)
        {
            TargetExpr = targetExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckFunctionMethodCall(TargetExpr, this);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(ParamBindings.FunctionDef != null)
                return TypesHelper.DotNetTypeToXgrsType(ParamBindings.FunctionDef.Output);
            else // compiled sequence
                return ParamBindings.ReturnType;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionFunctionMethodCall copy = (SequenceExpressionFunctionMethodCall)MemberwiseClone();
            copy.TargetExpr = TargetExpr.CopyExpression(originalToCopy, procEnv);
            copy.ParamBindings = ParamBindings.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement owner = (IGraphElement)TargetExpr.Evaluate(procEnv);
            for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; i++)
            {
                if(ParamBindings.ArgumentExpressions[i] != null)
                    ParamBindings.Arguments[i] = ParamBindings.ArgumentExpressions[i].Evaluate(procEnv);
            }
            return owner.ApplyFunctionMethod(procEnv, procEnv.Graph, ParamBindings.Name, ParamBindings.Arguments);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            TargetExpr.GetLocalVariables(variables, containerConstructors);
            ParamBindings.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression
        {
            get { yield return TargetExpr; foreach(SequenceExpression argument in ParamBindings.ArgumentExpressions) yield return argument; }
        }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return TargetExpr.Symbol + "." + ParamBindings.Name + "(...)"; } }
    }
}
