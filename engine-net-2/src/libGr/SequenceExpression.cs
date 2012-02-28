/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
        Not,
        Equal, NotEqual, Lower, LowerEqual, Greater, GreaterEqual, StructuralEqual,
        Plus, // todo: all the other operators and functions/methods from the expressions - as time allows
        Constant, Variable,
        Def,
        IsVisited,
        InContainer, ContainerEmpty, ContainerSize, ContainerAccess,
        ElementFromGraph,
        Source, Target,
        GraphElementAttribute,
        AdjacentNodes, AdjacentNodesViaIncoming, AdjacentNodesViaOutgoing,
        IncidentEdges, IncomingEdges, OutgoingEdges,
        InducedSubgraph, DefinedSubgraph,
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

        public SequenceExpressionContainer(SequenceExpressionType type, SequenceVariable container, SequenceComputation methodCall)
            : base(type)
        {
            Container = container;
            MethodCall = methodCall;
        }

        public string ContainerType(SequenceCheckingEnvironment env)
        {
            if(Container != null) return Container.Type;
            else return MethodCall.Type(env);
        }

        public object ContainerValue(IGraphProcessingEnvironment procEnv)
        {
            if(Container != null) return Container.GetVariableValue(procEnv);
            else return MethodCall.Execute(procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(Container != null)
                return Container.Type;
            else
                return MethodCall.Type(env);
        }

        public string Name { get { if(Container != null) return Container.Name; else return MethodCall.Symbol; } }
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

        public SequenceExpressionInContainer(SequenceExpression expr, SequenceExpression container)
            : base(SequenceExpressionType.InContainer)
        {
            Expr = expr;
            Container = container;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Container.Type(env) == "") 
                return; // we can't check further types if the variable is untyped, only runtime-check possible

            if(!Container.Type(env).StartsWith("set<") && !Container.Type(env).StartsWith("map<") && !Container.Type(env).StartsWith("array<"))
            {
                throw new SequenceParserException(Container.Symbol, "set or map or array type", Container.Type(env));
            }
            if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(Container.Type(env)), env.Model))
            {
                throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(Container.Type(env)), Expr.Type(env));
            }
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionInContainer copy = (SequenceExpressionInContainer)MemberwiseClone();
            copy.Container = Container.CopyExpression(originalToCopy, procEnv);
            copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object container = Container.Evaluate(procEnv);
            if(container is IList)
            {
                IList array = (IList)container;
                return array.Contains(Expr.Evaluate(procEnv));
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                return setmap.Contains(Expr.Evaluate(procEnv));
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
            Expr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Expr.Symbol + " in " + Container.Symbol; } }
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(ContainerType(env) != "" && (TypesHelper.ExtractSrc(ContainerType(env)) == null || TypesHelper.ExtractDst(ContainerType(env)) == null))
            {
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> type", ContainerType(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionContainerSize copy = (SequenceExpressionContainerSize)MemberwiseClone();
            if(Container != null) copy.Container = Container.Copy(originalToCopy, procEnv);
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(ContainerType(env) != "" && (TypesHelper.ExtractSrc(ContainerType(env)) == null || TypesHelper.ExtractDst(ContainerType(env)) == null))
            {
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> type", ContainerType(env));
            }
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
        }

        public override IEnumerable<SequenceComputation> Children { get { if(MethodCall == null) yield break; else yield return MethodCall; } }
        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Name + ".empty()"; } }
    }

    public class SequenceExpressionContainerAccess : SequenceExpression
    {
        public SequenceVariable Container;
        public SequenceExpression KeyExpr;

        public SequenceExpressionContainerAccess(SequenceVariable container, SequenceExpression keyExpr)
            : base(SequenceExpressionType.ContainerAccess)
        {
            Container = container;
            KeyExpr = keyExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Container.Type == "")
                return; // we can't check source and destination types if the variable is untyped, only runtime-check possible
            
            if(TypesHelper.ExtractSrc(Container.Type) == null || TypesHelper.ExtractDst(Container.Type) == null || TypesHelper.ExtractDst(Container.Type) == "SetValueType")
            {
                throw new SequenceParserException(Symbol, "map<S,T> or array<S>", Container.Type);
            }
            if(Container.Type.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpr.Type(env));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpr.Type(env), TypesHelper.ExtractSrc(Container.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(Container.Type), KeyExpr.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            if(Container.Type == "")
                return ""; // we can't gain access to the container destination type if the variable is untyped, only runtime-check possible

            if(Container.Type.StartsWith("array"))
                return TypesHelper.ExtractSrc(Container.Type);
            else
                return TypesHelper.ExtractDst(Container.Type);
        }

        internal override SequenceExpression CopyExpression(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceExpressionContainerAccess copy = (SequenceExpressionContainerAccess)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy, procEnv);
            copy.KeyExpr = KeyExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(Container.GetVariableValue(procEnv) is IList)
            {
                IList array = (IList)Container.GetVariableValue(procEnv);
                int key = (int)KeyExpr.Evaluate(procEnv);
                return array[key];
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(procEnv);
                object key = KeyExpr.Evaluate(procEnv);
                return setmap[key];
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
            KeyExpr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name + "[" + KeyExpr.Symbol + "]"; } }
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

    public class SequenceExpressionAttribute : SequenceExpression
    {
        public SequenceVariable SourceVar;
        public String AttributeName;

        public SequenceExpressionAttribute(SequenceVariable sourceVar, String attributeName)
            : base(SequenceExpressionType.GraphElementAttribute)
        {
            SourceVar = sourceVar;
            AttributeName = attributeName;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(SourceVar.Type == "") 
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

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
            SequenceExpressionAttribute copy = (SequenceExpressionAttribute)MemberwiseClone();
            copy.SourceVar = SourceVar.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)SourceVar.GetVariableValue(procEnv);
            object value = elem.GetAttribute(AttributeName);
            value = DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                elem, AttributeName, value);
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
                if(edgeType == null) throw new Exception("edge argument to adjacent is not an edge type");
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
                if(nodeType == null) throw new Exception("node argument to adjacent is not a node type");
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
        public override string Symbol { get 
        {
            string name;
            switch(SequenceExpressionType)
            {
                case SequenceExpressionType.AdjacentNodes: name = "adjacent("; break;
                case SequenceExpressionType.AdjacentNodesViaIncoming: name = "adjacentIncoming("; break;
                case SequenceExpressionType.AdjacentNodesViaOutgoing: name = "adjacentOutgoing("; break;
                case SequenceExpressionType.IncidentEdges: name = "incident("; break;
                case SequenceExpressionType.IncomingEdges: name = "incoming("; break;
                case SequenceExpressionType.OutgoingEdges: name = "outgoing("; break;
                default: name = "INTERNAL FAILURE!"; break;
            }
            return name + SourceNode.Symbol + (EdgeType != null ? "," + EdgeType.Symbol : "") + (OppositeNodeType != null ? "," + OppositeNodeType.Symbol : "") + ")";
        } }
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
            return ExprSrc!=null ? "Edge" : "Node";
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
            {
                object nodeTypeCandidate = Expr.Evaluate(procEnv);
                NodeType nodeType = nodeTypeCandidate is string
                    ? procEnv.Graph.Model.NodeModel.GetType((string)nodeTypeCandidate)
                    : (NodeType)nodeTypeCandidate;
                return procEnv.Graph.AddNode(nodeType);
            }
            else
            {
                object edgeTypeCandidate = Expr.Evaluate(procEnv);
                EdgeType edgeType = edgeTypeCandidate is string
                    ? procEnv.Graph.Model.EdgeModel.GetType((string)edgeTypeCandidate)
                    : (EdgeType)edgeTypeCandidate;
                return procEnv.Graph.AddEdge(edgeType, (INode)ExprSrc.Evaluate(procEnv), (INode)ExprDst.Evaluate(procEnv));
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Expr.GetLocalVariables(variables);
            if(ExprSrc != null) ExprSrc.GetLocalVariables(variables);
            if(ExprDst != null) ExprDst.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> ChildrenExpression { get { yield return Expr; yield return ExprSrc; yield return ExprDst; } }
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
}
