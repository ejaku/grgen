/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        Plus, Minus, // todo: all the other operators and functions/methods from the expressions - as time allows
        Constant, Variable,
        Random,
        Def,
        IsVisited,
        InContainer, ContainerEmpty, ContainerSize, ContainerAccess, ContainerPeek,
        ElementFromGraph,
        Source, Target,
        GraphElementAttribute,
        ElementOfMatch,
        Nodes, Edges,
        AdjacentNodes, AdjacentNodesViaIncoming, AdjacentNodesViaOutgoing,
        IncidentEdges, IncomingEdges, OutgoingEdges,
        ReachableNodes, ReachableNodesViaIncoming, ReachableNodesViaOutgoing,
        ReachableEdges, ReachableEdgesViaIncoming, ReachableEdgesViaOutgoing,
        InducedSubgraph, DefinedSubgraph,
        Canonize,
        VAlloc, GraphAdd, InsertInduced, InsertDefined // has side effects, but parser accepts it only in assignments
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
        public SequenceVariable Container;
        public SequenceComputation MethodCall;
        public SequenceExpressionAttributeAccess Attribute;

        public SequenceExpressionContainer(SequenceExpressionType type, SequenceVariable container, SequenceComputation methodCall)
            : base(type)
        {
            Container = container;
            MethodCall = methodCall;
        }

        public SequenceExpressionContainer(SequenceExpressionType type, SequenceExpressionAttributeAccess attribute)
            : base(type)
        {
            Attribute = attribute;
        }

        public string ContainerType(SequenceCheckingEnvironment env)
        {
            if(Container != null) return Container.Type;
            else if(MethodCall != null) return MethodCall.Type(env);
            else return Attribute.Type(env);
        }

        public string CheckAndReturnContainerType(SequenceCheckingEnvironment env)
        {
            string ContainerType;
            if(Container != null)
                ContainerType = Container.Type;
            else if(MethodCall != null)
                ContainerType = MethodCall.Type(env);
            else
                ContainerType = Attribute.CheckAndReturnAttributeType(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        public object ContainerValue(IGraphProcessingEnvironment procEnv)
        {
            if(Container != null) return Container.GetVariableValue(procEnv);
            else if(MethodCall != null) return MethodCall.Execute(procEnv);
            else return Attribute.ExecuteNoImplicitContainerCopy(procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(Container != null)
                return Container.Type;
            else if(MethodCall != null)
                return MethodCall.Type(env);
            else
                return Attribute.Type(env);
        }

        public string Name { get { if(Container != null) return Container.Name; 
                                   else if(MethodCall != null) return MethodCall.Symbol; 
                                   else return Attribute.Symbol; } }
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

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Left.GetLocalVariables(variables);
            Right.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Condition.GetLocalVariables(variables);
            TrueCase.GetLocalVariables(variables);
            FalseCase.GetLocalVariables(variables);
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

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Operand.GetLocalVariables(variables);
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

        public override sealed void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Operand.GetLocalVariables(variables);
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
                return SequenceExpressionHelper.EqualObjects(leftValue, rightValue, balancedType, leftType, rightType);
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
                return SequenceExpressionHelper.NotEqualObjects(leftValue, rightValue, balancedType, leftType, rightType);
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
                return SequenceExpressionHelper.LowerObjects(leftValue, rightValue, balancedType, leftType, rightType);
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
                return SequenceExpressionHelper.LowerEqualObjects(leftValue, rightValue, balancedType, leftType, rightType);
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
                return SequenceExpressionHelper.GreaterObjects(leftValue, rightValue, balancedType, leftType, rightType);
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
                return SequenceExpressionHelper.GreaterEqualObjects(leftValue, rightValue, balancedType, leftType, rightType);
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
                else if(Constant.GetType().Name == "Dictionary`2")
                    return "{}"; // only empty set/map assignment possible as of now
                else if(Constant.GetType().Name == "List`1")
                    return "[]"; // only empty array assignment possible as of now
                else if(Constant.GetType().Name == "Deque`1")
                    return "]["; // only empty deque assignment possible as of now
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Variable.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Variable.Name; } }
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(UpperBound != null) 
                UpperBound.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            foreach(SequenceExpression defVar in DefVars)
                defVar.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagExpr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return GraphElementVar.Name + ".visited[" + VisitedFlagExpr.Symbol + "]"; } }
    }

    public class SequenceExpressionInContainer : SequenceExpression
    {
        public SequenceExpression Expr;

        public SequenceExpression Container;
        public SequenceExpressionAttributeAccess Attribute;

        public SequenceExpressionInContainer(SequenceExpression expr, SequenceExpression container)
            : base(SequenceExpressionType.InContainer)
        {
            Expr = expr;
            Container = container;
        }

        public SequenceExpressionInContainer(SequenceExpression expr, SequenceExpressionAttributeAccess attribute)
            : base(SequenceExpressionType.InContainer)
        {
            Expr = expr;
            Attribute = attribute;
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
            if(Container != null)
                ContainerType = Container.Type(env);
            else
                ContainerType = Attribute.CheckAndReturnAttributeType(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionInContainer copy = (SequenceExpressionInContainer)MemberwiseClone();
            if(Container != null) copy.Container = Container.CopyExpression(originalToCopy, procEnv);
            if(Attribute != null) copy.Attribute = (SequenceExpressionAttributeAccess)Attribute.CopyExpression(originalToCopy, procEnv);
            copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public object ContainerValue(IGraphProcessingEnvironment procEnv)
        {
            if(Container != null) return Container.Evaluate(procEnv);
            else return Attribute.ExecuteNoImplicitContainerCopy(procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables);
            Expr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Expr.Symbol + " in " + (Container!=null ? Container.Symbol : Attribute.Symbol); } }
    }

    public class SequenceExpressionContainerSize : SequenceExpressionContainer
    {
        public SequenceExpressionContainerSize(SequenceVariable container)
            : base(SequenceExpressionType.ContainerSize, container, null)
        {
        }

        public SequenceExpressionContainerSize(SequenceComputation methodCall)
            : base(SequenceExpressionType.ContainerSize, null, methodCall)
        {
        }

        public SequenceExpressionContainerSize(SequenceExpressionAttributeAccess attribute)
            : base(SequenceExpressionType.ContainerSize, attribute)
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
            if(Container != null) copy.Container = Container.Copy(originalToCopy, procEnv);
            if(Attribute != null) copy.Attribute = (SequenceExpressionAttributeAccess)Attribute.Copy(originalToCopy, procEnv);
            if(MethodCall != null) copy.MethodCall = MethodCall.Copy(originalToCopy, procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(MethodCall != null) MethodCall.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { if(MethodCall==null) yield break; else yield return MethodCall; } }
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Name + ".size()"; } }
    }

    public class SequenceExpressionContainerEmpty : SequenceExpressionContainer
    {
        public SequenceExpressionContainerEmpty(SequenceVariable container)
            : base(SequenceExpressionType.ContainerEmpty, container, null)
        {
        }

        public SequenceExpressionContainerEmpty(SequenceComputation methodCall)
            : base(SequenceExpressionType.ContainerEmpty, null, methodCall)
        {
        }

        public SequenceExpressionContainerEmpty(SequenceExpressionAttributeAccess attribute)
            : base(SequenceExpressionType.ContainerEmpty, attribute)
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
            if(Container != null) copy.Container = Container.Copy(originalToCopy, procEnv);
            if(MethodCall != null) copy.MethodCall = MethodCall.Copy(originalToCopy, procEnv);
            if(Attribute != null) copy.Attribute = (SequenceExpressionAttributeAccess)Attribute.Copy(originalToCopy, procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(MethodCall != null) MethodCall.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { if(MethodCall == null) yield break; else yield return MethodCall; } }
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Name + ".empty()"; } }
    }

    public class SequenceExpressionContainerAccess : SequenceExpression
    {
        public SequenceVariable Container;
        public SequenceExpressionAttributeAccess Attribute;
        
        public SequenceExpression KeyExpr;

        public SequenceExpressionContainerAccess(SequenceVariable container, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerAccess)
        {
            Container = container;
            KeyExpr = keyExpr;
        }

        public SequenceExpressionContainerAccess(SequenceExpressionAttributeAccess attribute, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerAccess)
        {
            Attribute = attribute;
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
            string ContainerType;
            if(Container != null)
                ContainerType = Container.Type;
            else
                ContainerType = Attribute.CheckAndReturnAttributeType(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string ContainerType;
            if(Container != null)
                ContainerType = Container.Type;
            else
                ContainerType = Attribute.Type(env);
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
            copy.Container = Container!=null ? Container.Copy(originalToCopy, procEnv) : null;
            copy.Attribute = Attribute!=null ? (SequenceExpressionAttributeAccess)Attribute.Copy(originalToCopy, procEnv) : null;
            copy.KeyExpr = KeyExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public object ContainerValue(IGraphProcessingEnvironment procEnv)
        {
            if(Container != null) return Container.GetVariableValue(procEnv);
            else return Attribute.ExecuteNoImplicitContainerCopy(procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container!=null) Container.GetLocalVariables(variables);
            if(Attribute!=null) Attribute.GetLocalVariables(variables);
            KeyExpr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container != null ? (Container.Name + "[" + KeyExpr.Symbol + "]") : (Attribute.SourceVar.Name + "." + Attribute.AttributeName + "[" + KeyExpr.Symbol + "]"); } }
    }

    public class SequenceExpressionContainerPeek : SequenceExpressionContainer
    {
        public SequenceExpression KeyExpr;

        public SequenceExpressionContainerPeek(SequenceVariable container, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerPeek, container, null)
        {
            KeyExpr = keyExpr;
        }

        public SequenceExpressionContainerPeek(SequenceComputation methodCall, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerPeek, null, methodCall)
        {
            KeyExpr = keyExpr;
        }

        public SequenceExpressionContainerPeek(SequenceExpressionAttributeAccess attribute, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerPeek, attribute)
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
            if(Container.Type == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            return TypesHelper.ExtractSrc(Container.Type);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionContainerPeek copy = (SequenceExpressionContainerPeek)MemberwiseClone();
            if(Container != null) copy.Container = Container.Copy(originalToCopy, procEnv);
            if(MethodCall != null) copy.MethodCall = MethodCall.Copy(originalToCopy, procEnv);
            if(Attribute != null) copy.Attribute = (SequenceExpressionAttributeAccess)Attribute.Copy(originalToCopy, procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(MethodCall != null) MethodCall.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables);
            if(KeyExpr != null) KeyExpr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { if(MethodCall == null) yield break; else yield return MethodCall; } }
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { if(KeyExpr != null) yield return KeyExpr; else yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Name + ".peek(" + (KeyExpr != null ? KeyExpr.Symbol : "") + ")"; } }
    }

    public class SequenceExpressionElementFromGraph : SequenceExpression
    {
        public String ElementName;

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

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "@(" + ElementName + ")"; } }
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
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
            SequenceExpressionAdjacentIncident copy = (SequenceExpressionAdjacentIncident)MemberwiseClone();
            if(NodeType!=null)copy.OppositeNodeType = NodeType.CopyExpression(originalToCopy, procEnv);
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

            return GraphHelper.Nodes(procEnv.Graph, nodeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(NodeType != null) NodeType.GetLocalVariables(variables);
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
            SequenceExpressionAdjacentIncident copy = (SequenceExpressionAdjacentIncident)MemberwiseClone();
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

            return GraphHelper.Edges(procEnv.Graph, edgeType);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(EdgeType != null) EdgeType.GetLocalVariables(variables);
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

    public class SequenceExpressionAdjacentIncident : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;

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
                    return GraphHelper.Adjacent(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.AdjacentNodesViaIncoming:
                    return GraphHelper.AdjacentIncoming(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.AdjacentNodesViaOutgoing:
                    return GraphHelper.AdjacentOutgoing(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.IncidentEdges:
                    return GraphHelper.Incident(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.IncomingEdges:
                    return GraphHelper.Incoming(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.OutgoingEdges:
                    return GraphHelper.Outgoing(sourceNode, edgeType, nodeType);
                default:
                    return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            SourceNode.GetLocalVariables(variables);
            if(EdgeType != null) EdgeType.GetLocalVariables(variables);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables);
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

    public class SequenceExpressionReachable : SequenceExpression
    {
        public SequenceExpression SourceNode;
        public SequenceExpression EdgeType;
        public SequenceExpression OppositeNodeType;

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
                case SequenceExpressionType.ReachableNodes:
                    return GraphHelper.Reachable(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.ReachableNodesViaIncoming:
                    return GraphHelper.ReachableIncoming(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.ReachableNodesViaOutgoing:
                    return GraphHelper.ReachableOutgoing(sourceNode, edgeType, nodeType);
                case SequenceExpressionType.ReachableEdges:
                    return GraphHelper.ReachableEdges(procEnv.Graph, sourceNode, edgeType, nodeType);
                case SequenceExpressionType.ReachableEdgesViaIncoming:
                    return GraphHelper.ReachableEdgesIncoming(procEnv.Graph, sourceNode, edgeType, nodeType);
                case SequenceExpressionType.ReachableEdgesViaOutgoing:
                    return GraphHelper.ReachableEdgesOutgoing(procEnv.Graph, sourceNode, edgeType, nodeType);
                default:
                    return null; // internal failure
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            SourceNode.GetLocalVariables(variables);
            if(EdgeType != null) EdgeType.GetLocalVariables(variables);
            if(OppositeNodeType != null) OppositeNodeType.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            NodeSet.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            EdgeSet.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return EdgeSet; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "definedSubgraph(" + EdgeSet.Symbol + ")"; } }
    }

    public class SequenceExpressionVAlloc : SequenceExpression
    {
        public SequenceExpressionVAlloc()
            : base(SequenceExpressionType.VAlloc)
        {
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionVAlloc copy = (SequenceExpressionVAlloc)MemberwiseClone();
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.AllocateVisitedFlag();
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "valloc()"; } }
    }

    public class SequenceExpressionGraphAdd : SequenceExpression
    {
        public SequenceExpression Expr;
        public SequenceExpression ExprSrc;
        public SequenceExpression ExprDst;

        public SequenceExpressionGraphAdd(SequenceExpression expr, SequenceExpression exprSrc, SequenceExpression exprDst)
            : base(SequenceExpressionType.GraphAdd)
        {
            Expr = expr;
            ExprSrc = exprSrc;
            ExprDst = exprDst;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Expr.Type(env) != "")
            {
                if(ExprSrc != null)
                {
                    string typeString = null;
                    if(Expr.Type(env) == "string")
                    {
                        if(Expr is SequenceExpressionConstant)
                            typeString = (string)((SequenceExpressionConstant)Expr).Constant;
                    }
                    else
                    {
                        typeString = Expr.Type(env);
                    }
                    EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                    if(edgeType == null && typeString != null)
                    {
                        throw new SequenceParserException(Symbol + ", first argument", "edge type or string denoting edge type", typeString);
                    }
                }
                else
                {
                    string typeString = null;
                    if(Expr.Type(env) == "string")
                    {
                        if(Expr is SequenceExpressionConstant)
                            typeString = (string)((SequenceExpressionConstant)Expr).Constant;
                    }
                    else
                    {
                        typeString = Expr.Type(env);
                    }
                    NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
                    if(nodeType == null && typeString != null)
                    {
                        throw new SequenceParserException(Symbol + ", first argument", "node type or string denoting node type", typeString);
                    }
                }
            }
            if(ExprSrc != null)
            {
                if(ExprSrc.Type(env) != "")
                {
                    if(!TypesHelper.IsSameOrSubtype(ExprSrc.Type(env), "Node", env.Model))
                    {
                        throw new SequenceParserException(Symbol + "second argument", "node", ExprSrc.Type(env));
                    }
                }
            }
            if(ExprDst != null)
            {
                if(ExprDst.Type(env) != "")
                {
                    if(!TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "Node", env.Model))
                    {
                        throw new SequenceParserException(Symbol + "third argument", "node", ExprDst.Type(env));
                    }
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string typeString = "";
            
            if(Expr.Type(env) != "")
            {
                if(ExprSrc != null)
                {
                    if(Expr.Type(env) == "string")
                    {
                        if(Expr is SequenceExpressionConstant)
                            typeString = (string)((SequenceExpressionConstant)Expr).Constant;
                    }
                    else
                    {
                        typeString = Expr.Type(env);
                    }
                }
                else
                {
                    if(Expr.Type(env) == "string")
                    {
                        if(Expr is SequenceExpressionConstant)
                            typeString = (string)((SequenceExpressionConstant)Expr).Constant;
                    }
                    else
                    {
                        typeString = Expr.Type(env);
                    }
                }
            }

            return typeString;
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionGraphAdd copy = (SequenceExpressionGraphAdd)MemberwiseClone();
            copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            if(ExprSrc != null) copy.ExprSrc = ExprSrc.CopyExpression(originalToCopy, procEnv);
            if(ExprDst != null) copy.ExprDst = ExprDst.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(ExprSrc == null)
                return GraphHelper.AddNodeOfType(Expr.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.AddEdgeOfType(Expr.Evaluate(procEnv), (INode)ExprSrc.Evaluate(procEnv), (INode)ExprDst.Evaluate(procEnv), procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Expr.GetLocalVariables(variables);
            if(ExprSrc != null) ExprSrc.GetLocalVariables(variables);
            if(ExprDst != null) ExprDst.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Expr; if(ExprSrc!=null) yield return ExprSrc; if(ExprDst!=null) yield return ExprDst; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "add(" + Expr.Symbol + (ExprSrc != null ? "," + ExprSrc.Symbol + "," + ExprDst.Symbol : "") + ")"; } }
    }

    public class SequenceExpressionInsertInduced : SequenceExpression
    {
        public SequenceExpression NodeSet;
        public SequenceExpression RootNode;

        public SequenceExpressionInsertInduced(SequenceExpression nodeSet, SequenceExpression rootNode)
            : base(SequenceExpressionType.InsertInduced)
        {
            NodeSet = nodeSet;
            RootNode = rootNode;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeSet.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!NodeSet.Type(env).StartsWith("set<"))
                {
                    throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
                }
                if(TypesHelper.ExtractSrc(NodeSet.Type(env)) != "Node")
                {
                    throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
                }
            }

            if(RootNode.Type(env) != "") 
            {
                if(!TypesHelper.IsSameOrSubtype(RootNode.Type(env), "Node", env.Model))
                {
                    throw new SequenceParserException(Symbol, "Node", RootNode.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootNode.Type(env);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionInsertInduced copy = (SequenceExpressionInsertInduced)MemberwiseClone();
            copy.NodeSet = NodeSet.CopyExpression(originalToCopy, procEnv);
            copy.RootNode = RootNode.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object nodeSet = NodeSet.Evaluate(procEnv);
            object rootNode = RootNode.Evaluate(procEnv);
            return GraphHelper.InsertInduced((IDictionary<INode, SetValueType>)nodeSet, (INode)rootNode, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            NodeSet.GetLocalVariables(variables);
            RootNode.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return NodeSet; yield return RootNode; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "insertInduced(" + NodeSet.Symbol + "," + RootNode.Symbol + ")"; } }
    }

    public class SequenceExpressionInsertDefined : SequenceExpression
    {
        public SequenceExpression EdgeSet;
        public SequenceExpression RootEdge;

        public SequenceExpressionInsertDefined(SequenceExpression edgeSet, SequenceExpression rootEdge)
            : base(SequenceExpressionType.InsertDefined)
        {
            EdgeSet = edgeSet;
            RootEdge = rootEdge;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeSet.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!EdgeSet.Type(env).StartsWith("set<"))
                {
                    throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
                }
                if(TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "Edge")
                {
                    throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
                }
            }

            if(RootEdge.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(RootEdge.Type(env), "Edge", env.Model))
                {
                    throw new SequenceParserException(Symbol, "Edge", RootEdge.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootEdge.Type(env);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionInsertDefined copy = (SequenceExpressionInsertDefined)MemberwiseClone();
            copy.EdgeSet = EdgeSet.CopyExpression(originalToCopy, procEnv);
            copy.RootEdge = RootEdge.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object edgeSet = EdgeSet.Evaluate(procEnv);
            object rootEdge = RootEdge.Evaluate(procEnv);
            return GraphHelper.InsertDefined((IDictionary<IEdge, SetValueType>)edgeSet, (IEdge)rootEdge, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            EdgeSet.GetLocalVariables(variables);
            RootEdge.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return EdgeSet; yield return RootEdge; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "insertDefined(" + EdgeSet.Symbol + "," + RootEdge.Symbol + ")"; } }
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Graph.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Graph; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "canonize(" + Graph.Symbol + ")"; } }
    }
}
