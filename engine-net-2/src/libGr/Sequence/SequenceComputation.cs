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
using System.Threading;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies the actual subtype of a sequence computation.
    /// A new expression type -> you must add the corresponding class down below 
    /// and adapt the lgspSequenceGenerator.
    /// </summary>
    public enum SequenceComputationType
    {
        Then,
        VAlloc, VFree, VFreeNonReset, VReset,
        ContainerAdd, ContainerRem, ContainerClear, ContainerAddAll,
        Assignment,
        VariableDeclaration,
        Emit, Record, Export, DeleteFile,
        GraphAdd, GraphRem, GraphClear, GraphRetype,
        GraphAddCopy, GraphMerge, GraphRedirectSource, GraphRedirectTarget, GraphRedirectSourceAndTarget,
        Insert, InsertCopy,
        InsertInduced, InsertDefined,
        ProcedureCall, BuiltinProcedureCall, ProcedureMethodCall,
        DebugAdd, DebugRem, DebugEmit, DebugHalt, DebugHighlight,
        Assert,
        SynchronizationEnter, SynchronizationTryEnter, SynchronizationExit,
        GetEquivalentOrAdd,
        AssignmentTarget, // every assignment target (lhs value) is a computation
        Expression // every expression (rhs value) is a computation
    }

    /// <summary>
    /// A sequence computation object with references to child sequence computations.
    /// The computations are basically: visited flags management, container manipulation,
    /// assignments and special functions; they may or may not return values.
    /// They do change things, in contrast to the side-effect free sequence expressions.
    /// </summary>
    public abstract class SequenceComputation : SequenceBase
    {
        /// <summary>
        /// The type of the sequence computation (e.g. Assignment or MethodCall)
        /// </summary>
        public readonly SequenceComputationType SequenceComputationType;

        /// <summary>
        /// Initializes a new SequenceComputation object with the given sequence computation type.
        /// </summary>
        /// <param name="seqCompType">The sequence computation type.</param>
        protected SequenceComputation(SequenceComputationType seqCompType)
            : base()
        {
            SequenceComputationType = seqCompType;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The sequence computation to be copied.</param>
        protected SequenceComputation(SequenceComputation that)
            : base(that)
        {
            SequenceComputationType = that.SequenceComputationType;
        }

        public override bool HasSequenceType(SequenceType sequenceType)
        {
            return false;
        }

        /// <summary>
        /// Checks the sequence computation for errors utilizing the given checking environment
        /// reports them by exception
        /// default behavior: check all the children 
        /// </summary>
        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(SequenceComputation childSeq in Children)
            {
                childSeq.Check(env);
            }
        }

        /// <summary>
        /// Returns the type of the sequence
        /// default behaviour: returns "void"
        /// </summary>
        public override string Type(SequenceCheckingEnvironment env)
        {
            return "void";
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
        internal abstract SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv);

        /// <summary>
        /// Executes this sequence computation.
        /// </summary>
        /// <param name="procEnv">The graph processing environment on which this sequence computation is to be evaluated.
        ///     Contains especially the graph on which this sequence computation is to be evaluated.
        ///     And the user proxy queried when choices are due.</param>
        /// <returns>The value resulting from computing this sequence computation, 
        ///          null if there is no result value</returns>
        public object Execute(IGraphProcessingEnvironment procEnv)
        {
            FireEnteringSequenceEvent(procEnv);
            executionState = SequenceExecutionState.Underway;
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("Before executing sequence " + Id + ": " + Symbol);
#endif
            object res = ExecuteImpl(procEnv);
#if LOG_SEQUENCE_EXECUTION
            procEnv.Recorder.WriteLine("After executing sequence " + Id + ": " + Symbol + " result " + res);
#endif
            executionState = SequenceExecutionState.Success; // maybe todo: dedicated value SequenceExecutionState.Executed?
            FireExitingSequenceEvent(procEnv);
            return res;
        }

        /// <summary>
        /// Executes this sequence computation. This function represents the actual implementation of the sequence.
        /// </summary>
        /// <param name="procEnv">The graph processing environment on which this sequence computation is to be evaluated.
        ///     Contains especially the graph on which this sequence computation is to be evaluated.
        ///     And the user proxy queried when choices are due.</param>
        /// <returns>The value resulting from computing this sequence computation, 
        ///          null if there is no result value</returns>
        public abstract object ExecuteImpl(IGraphProcessingEnvironment procEnv);

        /// <summary>
        /// Collects all variables of the sequence expression tree into the variables dictionary,
        /// and all container and internal object constructors used into the constructors array.
        /// </summary>
        /// <param name="variables">Contains the variables found</param>
        /// <param name="constructors">Contains the constructors found</param>
        public virtual void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        /// <summary>
        /// Enumerates all child sequence computation objects
        /// </summary>
        public abstract IEnumerable<SequenceComputation> Children { get; }

        public override IEnumerable<SequenceBase> ChildrenBase
        {
            get
            {
                foreach(SequenceBase child in Children)
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// Tells whether Execute returns a value to be used as a result determining value for a boolean computation sequence.
        /// Only expressions do so, the values returned by plain computations don't bubble up to sequence level, are computation internal only.
        /// </summary>
        public virtual bool ReturnsValue
        {
            get { return false; }
        }

        protected static string Escape(String str)
        {
            return str.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t").Replace("#", "\\#").Replace("\"", "\\\"");
        }

        protected static string Unescape(String str)
        {
            return str.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t").Replace("\\#", "#").Replace("\\\"", "\"");
        }
    }

    /// <summary>
    /// A sequence computation on a container object (resulting from a variable or a method call; yielding a container object again)
    /// </summary>
    public abstract class SequenceComputationContainer : SequenceComputation
    {
        public readonly SequenceVariable Container;
        public readonly SequenceExpressionAttributeAccess Attribute;

        protected SequenceComputationContainer(SequenceComputationType type, SequenceVariable container)
            : base(type)
        {
            Container = container;
        }

        protected SequenceComputationContainer(SequenceComputationType type, SequenceExpressionAttributeAccess attribute)
            : base(type)
        {
            Attribute = attribute;
        }

        protected SequenceComputationContainer(SequenceComputationContainer that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            if(that.Container != null)
                Container = that.Container.Copy(originalToCopy, procEnv);
            else
                Attribute = (SequenceExpressionAttributeAccess)that.Attribute.CopyExpression(originalToCopy, procEnv);
        }

        public string ContainerType(SequenceCheckingEnvironment env)
        {
            if(Container != null)
                return Container.Type;
            else
                return Attribute.Type(env);
        }

        protected string CheckAndReturnContainerType(SequenceCheckingEnvironment env)
        {
            string ContainerType;
            if(Container != null)
                ContainerType = Container.Type;
            else
                ContainerType = Attribute.CheckAndReturnAttributeType(env);
            if(ContainerType == "")
                return ""; // we can't check container type if the variable is untyped, only runtime-check possible
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        protected object ContainerValue(IGraphProcessingEnvironment procEnv, out IAttributeBearer elem, out AttributeType attrType)
        {
            if(Container != null)
            {
                elem = null;
                attrType = null;
                return Container.GetVariableValue(procEnv);
            }
            else
                return Attribute.ExecuteNoImplicitContainerCopy(procEnv, out elem, out attrType);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(Container != null)
                return Container.Type;
            else 
                return Attribute.Type(env);
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string Name
        {
            get
            {
                if(Container != null)
                    return Container.Name;
                else
                    return Attribute.Symbol;
            }
        }
    }


    public class SequenceComputationThen : SequenceComputation
    {
        public readonly SequenceComputation left;
        public readonly SequenceComputation right;

        public SequenceComputationThen(SequenceComputation left, SequenceComputation right)
            : base(SequenceComputationType.Then)
        {
            this.left = left;
            this.right = right;
        }

        protected SequenceComputationThen(SequenceComputationThen that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            left = that.left.Copy(originalToCopy, procEnv);
            right = that.right.Copy(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationThen(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            left.ExecuteImpl(procEnv);
            return right.ExecuteImpl(procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            left.GetLocalVariables(variables, constructors);
            right.GetLocalVariables(variables, constructors);
        }

        public override bool ReturnsValue { get { return right.ReturnsValue; } }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return left;
                yield return right;
            }
        }

        public override int Precedence
        {
            get { return 7; }
        }

        public override string Symbol
        {
            get { return left.Symbol + "; " + (right is SequenceExpression ? "{" + right.Symbol + "}" : right.Symbol); }
        }
    }


    public class SequenceComputationVAlloc : SequenceComputation
    {
        public SequenceComputationVAlloc()
            : base(SequenceComputationType.VAlloc)
        {
        }

        protected SequenceComputationVAlloc(SequenceComputationVAlloc that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationVAlloc(this, originalToCopy, procEnv);
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.AllocateVisitedFlag();
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "valloc()"; }
        }
    }

    public class SequenceComputationVFree : SequenceComputation
    {
        public readonly SequenceExpression VisitedFlagExpression;
        public readonly bool Reset;

        public SequenceComputationVFree(SequenceExpression visitedFlagExpr, bool reset)
            : base(reset ? SequenceComputationType.VFree : SequenceComputationType.VFreeNonReset)
        {
            VisitedFlagExpression = visitedFlagExpr;
            Reset = reset;
        }

        protected SequenceComputationVFree(SequenceComputationVFree that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            VisitedFlagExpression = that.VisitedFlagExpression.CopyExpression(originalToCopy, procEnv);
            Reset = that.Reset;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationVFree(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int", VisitedFlagExpression.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(procEnv);
            if(Reset)
                procEnv.Graph.FreeVisitedFlag(visitedFlag);
            else
                procEnv.Graph.FreeVisitedFlagNonReset(visitedFlag);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            VisitedFlagExpression.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return VisitedFlagExpression;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return (Reset ? "vfree" : "vfreenonreset") + "(" + VisitedFlagExpression.Symbol + ")"; }
        }
    }

    public class SequenceComputationVReset : SequenceComputation
    {
        public readonly SequenceExpression VisitedFlagExpression;

        public SequenceComputationVReset(SequenceExpression visitedFlagExpr)
            : base(SequenceComputationType.VReset)
        {
            VisitedFlagExpression = visitedFlagExpr;
        }

        protected SequenceComputationVReset(SequenceComputationVReset that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            VisitedFlagExpression = that.VisitedFlagExpression.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationVReset(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "int", VisitedFlagExpression.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(procEnv);
            procEnv.Graph.ResetVisitedFlag(visitedFlag);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            VisitedFlagExpression.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield return VisitedFlagExpression; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "vreset(" + VisitedFlagExpression.Symbol + ")"; }
        }
    }

    public class SequenceComputationContainerAdd : SequenceComputationContainer
    {
        public readonly SequenceExpression Expr;
        public readonly SequenceExpression ExprDst;

        public SequenceComputationContainerAdd(SequenceVariable container, SequenceExpression expr, SequenceExpression exprDst)
            : base(SequenceComputationType.ContainerAdd, container)
        {
            Expr = expr;
            ExprDst = exprDst;
        }

        public SequenceComputationContainerAdd(SequenceExpressionAttributeAccess attribute, SequenceExpression expr, SequenceExpression exprDst)
            : base(SequenceComputationType.ContainerAdd, attribute)
        {
            Expr = expr;
            ExprDst = exprDst;
        }

        protected SequenceComputationContainerAdd(SequenceComputationContainerAdd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
            if(that.ExprDst != null)
                ExprDst = that.ExprDst.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationContainerAdd(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(ExprDst != null && TypesHelper.ExtractDst(ContainerType) == "SetValueType")
                throw new SequenceParserExceptionTypeMismatch(Symbol, "map or array or deque", ContainerType);

            if(ContainerType.StartsWith("array<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                if(ExprDst != null && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
            }
            else if(ContainerType.StartsWith("deque<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                if(ExprDst != null && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                if(TypesHelper.ExtractDst(ContainerType) != "SetValueType"
                    && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), TypesHelper.ExtractDst(ContainerType), env.Model))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
                }
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IAttributeBearer elem;
            AttributeType attrType;
            object container = ContainerValue(procEnv, out elem, out attrType);
            if(container == null)
                throw new NullReferenceException();
            object firstValue = Expr.Evaluate(procEnv);
            object optionalSecondValue = null;
            if(ExprDst != null)
                optionalSecondValue = ExprDst.Evaluate(procEnv);

            if(container is IList)
                return ExecuteArray(procEnv, elem, attrType, (IList)container, firstValue, optionalSecondValue);
            else if(container is IDeque)
                return ExecuteDeque(procEnv, elem, attrType, (IDeque)container, firstValue, optionalSecondValue);
            else if(ExprDst == null)
                return ExecuteSet(procEnv, elem, attrType, (IDictionary)container, firstValue, optionalSecondValue);
            else
                return ExecuteMap(procEnv, elem, attrType, (IDictionary)container, firstValue, optionalSecondValue);
        }

        private object ExecuteArray(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IList array, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                BaseGraph.ChangingAttributePutElement(procEnv.Graph, elem, attrType, firstValue, optionalSecondValue);

            if(ExprDst == null)
                array.Add(firstValue);
            else
                array.Insert((int)optionalSecondValue, firstValue);

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return array;
        }

        private object ExecuteDeque(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDeque deque, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                BaseGraph.ChangingAttributePutElement(procEnv.Graph, elem, attrType, firstValue, optionalSecondValue);

            if(ExprDst == null)
                deque.Enqueue(firstValue);
            else
                deque.EnqueueAt((int)optionalSecondValue, firstValue);

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return deque;
        }

        private object ExecuteSet(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDictionary set, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                BaseGraph.ChangingSetAttributePutElement(procEnv.Graph, elem, attrType, firstValue);

            set[firstValue] = optionalSecondValue;

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return set;
        }

        private object ExecuteMap(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDictionary map, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                BaseGraph.ChangingMapAttributePutElement(procEnv.Graph, elem, attrType, firstValue, optionalSecondValue);

            map[firstValue] = optionalSecondValue;

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(Container != null)
                Container.GetLocalVariables(variables);
            if(Attribute != null)
                Attribute.GetLocalVariables(variables, constructors);
            Expr.GetLocalVariables(variables, constructors);
            if(ExprDst != null)
                ExprDst.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Expr;
                if(ExprDst != null)
                    yield return ExprDst;
            }
        }

        public override string Symbol
        {
            get { return Name + ".add(" + Expr.Symbol + (ExprDst != null ? "," + ExprDst.Symbol : "") + ")"; }
        }
    }

    public class SequenceComputationContainerRem : SequenceComputationContainer
    {
        public readonly SequenceExpression Expr;

        public SequenceComputationContainerRem(SequenceVariable container, SequenceExpression expr)
            : base(SequenceComputationType.ContainerRem, container)
        {
            Expr = expr;
        }

        public SequenceComputationContainerRem(SequenceExpressionAttributeAccess attribute, SequenceExpression expr)
            : base(SequenceComputationType.ContainerRem, attribute)
        {
            Expr = expr;
        }

        protected SequenceComputationContainerRem(SequenceComputationContainerRem that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            if(Expr != null)
                Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationContainerRem(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(ContainerType.StartsWith("array<"))
            {
                if(Expr != null && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", Expr.Type(env));
            }
            else if(ContainerType.StartsWith("deque<"))
            {
                if(Expr != null && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", Expr.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IAttributeBearer elem;
            AttributeType attrType;
            object container = ContainerValue(procEnv, out elem, out attrType);
            if(container == null)
                throw new NullReferenceException();
            object valueOrKeyOrIndexToRemove = null;
            if(Expr != null)
                valueOrKeyOrIndexToRemove = Expr.Evaluate(procEnv);

            if(container is IList)
                return ExecuteArray(procEnv, elem, attrType, (IList)container, valueOrKeyOrIndexToRemove);
            else if(container is IDeque)
                return ExecuteDeque(procEnv, elem, attrType, (IDeque)container, valueOrKeyOrIndexToRemove);
            else if(TypesHelper.ExtractDst(TypesHelper.XgrsTypeOfConstant(container, procEnv.Graph.Model)) == "SetValueType")
                return ExecuteSet(procEnv, elem, attrType, (IDictionary)container, valueOrKeyOrIndexToRemove);
            else
                return ExecuteMap(procEnv, elem, attrType, (IDictionary)container, valueOrKeyOrIndexToRemove);
        }

        private object ExecuteArray(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IList array, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                BaseGraph.ChangingAttributeRemoveElement(procEnv.Graph, elem, attrType, valueOrKeyOrIndexToRemove);

            if(Expr == null)
                array.RemoveAt(array.Count - 1);
            else
                array.RemoveAt((int)valueOrKeyOrIndexToRemove);

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return array;
        }

        private object ExecuteDeque(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDeque deque, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                BaseGraph.ChangingAttributeRemoveElement(procEnv.Graph, elem, attrType, valueOrKeyOrIndexToRemove);

            if(Expr == null)
                deque.Dequeue();
            else
                deque.DequeueAt((int)valueOrKeyOrIndexToRemove);

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return deque;
        }

        private static object ExecuteSet(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDictionary set, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                BaseGraph.ChangingSetAttributeRemoveElement(procEnv.Graph, elem, attrType, valueOrKeyOrIndexToRemove);

            set.Remove(valueOrKeyOrIndexToRemove);

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return set;
        }

        private static object ExecuteMap(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDictionary map, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                BaseGraph.ChangingMapAttributeRemoveElement(procEnv.Graph, elem, attrType, valueOrKeyOrIndexToRemove);

            map.Remove(valueOrKeyOrIndexToRemove);

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(Container != null)
                Container.GetLocalVariables(variables);
            if(Attribute != null)
                Attribute.GetLocalVariables(variables, constructors);
            if(Expr != null)
                Expr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                if(Expr != null)
                    yield return Expr;
                yield break;
            }
        }

        public override string Symbol
        {
            get { return Name + ".rem(" + (Expr != null ? Expr.Symbol : "") + ")"; }
        }
    }

    public class SequenceComputationContainerClear : SequenceComputationContainer
    {
        public SequenceComputationContainerClear(SequenceVariable container)
            : base(SequenceComputationType.ContainerClear, container)
        {
        }

        public SequenceComputationContainerClear(SequenceExpressionAttributeAccess attribute)
            : base(SequenceComputationType.ContainerClear, attribute)
        {
        }

        protected SequenceComputationContainerClear(SequenceComputationContainerClear that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationContainerClear(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IAttributeBearer elem;
            AttributeType attrType;
            object container = ContainerValue(procEnv, out elem, out attrType);
            if(container == null)
                throw new NullReferenceException();

            if(container is IList)
                return ExecuteList(procEnv, elem, attrType, (IList)container);
            else if(container is IDeque)
                return ExecuteDeque(procEnv, elem, attrType, (IDeque)container);
            else if(TypesHelper.ExtractDst(TypesHelper.XgrsTypeOfConstant(container, procEnv.Graph.Model)) == "SetValueType")
                return ExecuteSet(procEnv, elem, attrType, (IDictionary)container);
            else
                return ExecuteMap(procEnv, elem, attrType, (IDictionary)container);
        }

        private static object ExecuteList(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IList array)
        {
            if(elem != null)
            {
                for(int i = array.Count; i >= 0; --i)
                {
                    BaseGraph.ChangingAttributeRemoveElement(procEnv.Graph, elem, attrType, i);
                }
            }

            array.Clear();

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return array;
        }

        private static object ExecuteDeque(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDeque deque)
        {
            if(elem != null)
            {
                for(int i = deque.Count; i >= 0; --i)
                {
                    BaseGraph.ChangingAttributeRemoveElement(procEnv.Graph, elem, attrType, i);
                }
            }

            deque.Clear();

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return deque;
        }

        private static object ExecuteSet(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDictionary set)
        {
            if(elem != null)
            {
                foreach(DictionaryEntry kvp in set)
                {
                    BaseGraph.ChangingSetAttributeRemoveElement(procEnv.Graph, elem, attrType, kvp.Key);
                }
            }

            set.Clear();

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return set;
        }

        private static object ExecuteMap(IGraphProcessingEnvironment procEnv, IAttributeBearer elem, AttributeType attrType, IDictionary map)
        {
            if(elem != null)
            {
                foreach(DictionaryEntry kvp in map)
                {
                    BaseGraph.ChangingMapAttributeRemoveElement(procEnv.Graph, elem, attrType, kvp.Key);
                }
            }

            map.Clear();

            if(elem != null && elem is IGraphElement)
                BaseGraph.ChangedAttribute(procEnv.Graph, (IGraphElement)elem, attrType);

            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(Container != null)
                Container.GetLocalVariables(variables);
            if(Attribute != null)
                Attribute.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override string Symbol
        {
            get { return Name + ".clear()"; }
        }
    }

    public class SequenceComputationContainerAddAll : SequenceComputationContainer
    {
        public readonly SequenceExpression Expr;

        public SequenceComputationContainerAddAll(SequenceVariable container, SequenceExpression expr)
            : base(SequenceComputationType.ContainerAddAll, container)
        {
            Expr = expr;
        }

        protected SequenceComputationContainerAddAll(SequenceComputationContainerAddAll that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationContainerAddAll(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(ContainerType.StartsWith("array<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), ContainerType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, ContainerType, Expr.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), ContainerType, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, ContainerType, Expr.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return ContainerHelper.ContainerAddAll(procEnv, Container.GetVariableValue(procEnv), Expr.Evaluate(procEnv));
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Container.GetLocalVariables(variables);
            Expr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Expr;
            }
        }

        public override string Symbol
        {
            get { return Name + ".addAll(" + Expr.Symbol + ")"; }
        }
    }

    public class SequenceComputationAssignment : SequenceComputation
    {
        public readonly AssignmentTarget Target;
        public readonly SequenceComputation SourceValueProvider;

        public SequenceComputationAssignment(AssignmentTarget tgt, SequenceComputation srcValueProvider)
            : base(SequenceComputationType.Assignment)
        {
            Target = tgt;
            SourceValueProvider = srcValueProvider;
        }

        protected SequenceComputationAssignment(SequenceComputationAssignment that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Target = that.Target.CopyTarget(originalToCopy, procEnv);
            SourceValueProvider = that.SourceValueProvider.Copy(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationAssignment(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsSameOrSubtype(SourceValueProvider.Type(env), Target.Type(env), env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, Target.Type(env), SourceValueProvider.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object value = SourceValueProvider.ExecuteImpl(procEnv);
            Target.Assign(value, procEnv);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Target.GetLocalVariables(variables, constructors);
            SourceValueProvider.GetLocalVariables(variables, constructors);
        }

        public override string Symbol
        {
            get { return Target.Symbol + "=" + SourceValueProvider.Symbol; }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Target;
                yield return SourceValueProvider;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        } // always a top prio assignment factor
    }

    public class SequenceComputationVariableDeclaration : SequenceComputation
    {
        public readonly SequenceVariable Target;

        public SequenceComputationVariableDeclaration(SequenceVariable tgt)
            : base(SequenceComputationType.VariableDeclaration)
        {
            Target = tgt;
        }

        protected SequenceComputationVariableDeclaration(SequenceComputationVariableDeclaration that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Target = that.Target.Copy(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationVariableDeclaration(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object value = TypesHelper.DefaultValue(Target.Type, procEnv.Graph.Model);
            Target.SetVariableValue(value, procEnv);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Target.GetLocalVariables(variables);
        }

        public override string Symbol
        {
            get { return Target.Name; }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; } // always a top prio assignment factor
        }
    }

    public abstract class SequenceComputationDebug : SequenceComputation
    {
        public readonly List<SequenceExpression> ArgExprs;

        protected SequenceComputationDebug(SequenceComputationType seqCompType, List<SequenceExpression> argExprs)
            : base(seqCompType)
        {
            ArgExprs = argExprs;
        }

        protected SequenceComputationDebug(SequenceComputationDebug that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in that.ArgExprs)
            {
                ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            }
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(ArgExprs.Count == 0)
                throw new Exception("Debug::(add,rem,emit,halt,highlight) expects at least one parameter (the message at computation entry)");

            if(!TypesHelper.IsSameOrSubtype(ArgExprs[0].Type(env), "string", env.Model))
                throw new SequenceParserExceptionTypeMismatch("The 0 parameter of " + Symbol, "string type", ArgExprs[0].Type(env));

            base.Check(env);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                for(int i = 0; i < ArgExprs.Count; ++i)
                {
                    yield return ArgExprs[i];
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public abstract string Name { get; }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Debug::" + Name + "(");
                bool first = true;
                foreach(SequenceExpression seqExpr in ArgExprs)
                {
                    if(!first)
                        sb.Append(", ");
                    else
                        first = false;
                    sb.Append(seqExpr.Symbol);
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceComputationDebugAdd : SequenceComputationDebug
    {
        readonly object[] values;

        public SequenceComputationDebugAdd(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugAdd, argExprs)
        {
            values = new object[ArgExprs.Count - 1];
        }

        protected SequenceComputationDebugAdd(SequenceComputationDebugAdd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
             values = new object[ArgExprs.Count - 1];
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationDebugAdd(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!FireDebugEvents)
                return null;

            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugEntering((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Name
        {
            get { return "add"; }
        }
    }

    public class SequenceComputationDebugRem : SequenceComputationDebug
    {
        readonly object[] values;

        public SequenceComputationDebugRem(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugRem, argExprs)
        {
            values = new object[ArgExprs.Count - 1];
        }

        protected SequenceComputationDebugRem(SequenceComputationDebugRem that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            values = new object[ArgExprs.Count - 1];
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationDebugRem(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!FireDebugEvents)
                return null;

            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugExiting((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Name
        {
            get { return "rem"; }
        }
    }

    public class SequenceComputationDebugEmit : SequenceComputationDebug
    {
        readonly object[] values;

        public SequenceComputationDebugEmit(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugEmit, argExprs)
        {
            values = new object[ArgExprs.Count - 1];
        }

        protected SequenceComputationDebugEmit(SequenceComputationDebugEmit that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            values = new object[ArgExprs.Count - 1];
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationDebugEmit(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!FireDebugEvents)
                return null;

            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugEmitting((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Name
        {
            get { return "emit"; }
        }
    }

    public class SequenceComputationDebugHalt : SequenceComputationDebug
    {
        readonly object[] values;

        public SequenceComputationDebugHalt(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugHalt, argExprs)
        {
            values = new object[ArgExprs.Count - 1];
        }

        protected SequenceComputationDebugHalt(SequenceComputationDebugHalt that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            values = new object[ArgExprs.Count - 1];
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationDebugHalt(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!FireDebugEvents)
                return null;

            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugHalting((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Name
        {
            get { return "halt"; }
        }
    }

    public class SequenceComputationDebugHighlight : SequenceComputationDebug
    {
        public SequenceComputationDebugHighlight(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugHighlight, argExprs)
        {
        }

        protected SequenceComputationDebugHighlight(SequenceComputationDebugHighlight that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationDebugHighlight(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(ArgExprs.Count % 2 == 0)
                throw new Exception("Debug::highlight expects an odd number of parameters (message, then series of alternating value to highlight followed by annotation to be displayed)");

            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                if(i % 2 == 0 && !TypesHelper.IsSameOrSubtype(ArgExprs[i].Type(env), "string", env.Model))
                    throw new SequenceParserExceptionTypeMismatch("The " + i + " parameter of " + Symbol, "string type", ArgExprs[i].Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(!FireDebugEvents)
                return null;

            List<object> values = new List<object>();
            List<string> annotations = new List<string>();
            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                if(i % 2 == 1)
                    values.Add(ArgExprs[i].Evaluate(procEnv));
                else
                    annotations.Add((string)ArgExprs[i].Evaluate(procEnv));
            }
            procEnv.DebugHighlighting((string)ArgExprs[0].Evaluate(procEnv), values, annotations);
            return null;
        }

        public override string Name
        {
            get { return "highlight"; }
        }
    }

    public class SequenceComputationAssert : SequenceComputation
    {
        public readonly List<SequenceExpression> ArgExprs;
        public readonly bool IsAlways; // !IsAlways is only processed in case of procEnv.EnableAssertions

        public SequenceComputationAssert(List<SequenceExpression> argExprs, bool isAlways)
            : base(SequenceComputationType.Assert)
        {
            ArgExprs = argExprs;
            IsAlways = isAlways;
        }

        protected SequenceComputationAssert(SequenceComputationAssert that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in that.ArgExprs)
            {
                ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            }
            IsAlways = that.IsAlways;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationAssert(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(ArgExprs.Count == 0)
                throw new Exception(Name + " expects at least one parameter (the condition to assert on)");

            if(!TypesHelper.IsSameOrSubtype(ArgExprs[0].Type(env), "boolean", env.Model))
                throw new SequenceParserExceptionTypeMismatch("The first parameter of " + Symbol, "boolean type", ArgExprs[0].Type(env));

            if(ArgExprs.Count >= 2)
            if(!TypesHelper.IsSameOrSubtype(ArgExprs[1].Type(env), "string", env.Model))
                throw new SequenceParserExceptionTypeMismatch("The second parameter of " + Symbol, "string type", ArgExprs[1].Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            Func<bool> assertion = () => (bool)ArgExprs[0].Evaluate(procEnv);
            Func<string> message = () => "";
            if(ArgExprs.Count >= 2)
                message = () => (string)ArgExprs[1].Evaluate(procEnv);
            List<Func<object>> values = new List<Func<object>>();
            for(int i = 2; i < ArgExprs.Count; ++i)
            {
                values.Add(() => ArgExprs[i].Evaluate(procEnv));
            }
            procEnv.UserProxy.HandleAssert(IsAlways, assertion, message, values.ToArray());
            return null;
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                for(int i = 0; i < ArgExprs.Count; ++i)
                {
                    yield return ArgExprs[i];
                }
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public string Name
        {
            get { return IsAlways ? "assertAlways" : "assert"; }
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Name);
                sb.Append("(");
                bool first = true;
                foreach(SequenceExpression seqExpr in ArgExprs)
                {
                    if(!first)
                        sb.Append(", ");
                    else
                        first = false;
                    sb.Append(seqExpr.Symbol);
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceComputationEmit : SequenceComputation
    {
        public readonly List<SequenceExpression> Expressions;
        public readonly bool IsDebug;

        public SequenceComputationEmit(List<SequenceExpression> exprs, bool isDebug)
            : base(SequenceComputationType.Emit)
        {
            Expressions = exprs;
            IsDebug = isDebug;
        }

        protected SequenceComputationEmit(SequenceComputationEmit that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Expressions = new List<SequenceExpression>(that.Expressions.Count);
            for(int i = 0; i < that.Expressions.Count; ++i)
            {
                Expressions.Add(that.Expressions[i].CopyExpression(originalToCopy, procEnv));
            }
            IsDebug = that.IsDebug;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationEmit(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object value = null;
            for(int i = 0; i < Expressions.Count; ++i)
            {
                value = Expressions[i].Evaluate(procEnv);

                if(value is string)
                    value = Unescape((string)value);

                if(IsDebug)
                {
                    if(value != null)
                        procEnv.EmitWriterDebug.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph, false, null, null));
                }
                else
                {
                    if(value != null)
                        procEnv.EmitWriter.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph, false, null, null));
                }
            }
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            for(int i = 0; i < Expressions.Count; ++i)
            {
                Expressions[i].GetLocalVariables(variables, constructors);
            }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                for(int i = 0; i < Expressions.Count; ++i)
                {
                    yield return Expressions[i];
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
                if(IsDebug)
                    sb.Append("emitdebug(");
                else
                    sb.Append("emit(");
                bool first = true;
                foreach(SequenceExpression expr in Expressions)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(", ");
                    SequenceExpressionConstant exprConst = expr as SequenceExpressionConstant;
                    if(exprConst != null && exprConst.Constant is string)
                        sb.Append(SequenceExpressionConstant.ConstantAsString(exprConst.Constant));
                    else
                        sb.Append(expr.Symbol);
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceComputationRecord : SequenceComputation
    {
        public readonly SequenceExpression Expression;

        public SequenceComputationRecord(SequenceExpression expr)
            : base(SequenceComputationType.Record)
        {
            Expression = expr;
        }

        protected SequenceComputationRecord(SequenceComputationRecord that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Expression = that.Expression.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationRecord(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object value = Expression.Evaluate(procEnv);
            if(value is string)
                value = Unescape((string)value);
            if(value != null)
                procEnv.Recorder.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph, false, null, null));
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Expression.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield return Expression; }
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
                sb.Append("record(");

                SequenceExpressionConstant exprConst = Expression as SequenceExpressionConstant;
                if(exprConst != null && exprConst.Constant is string)
                    sb.Append(SequenceExpressionConstant.ConstantAsString(exprConst.Constant));
                else
                    sb.Append(Expression.Symbol);

                sb.Append(")");
                return sb.ToString();
            } 
        }
    }

    public class SequenceComputationExport : SequenceComputation
    {
        public readonly SequenceExpression Name;
        public readonly SequenceExpression Graph;

        public SequenceComputationExport(SequenceExpression expr1, SequenceExpression expr2)
            : base(SequenceComputationType.Export)
        {
            if(expr2 != null)
            {
                Graph = expr1;
                Name = expr2;
            }
            else
            {
                Name = expr1;
            }
        }

        protected SequenceComputationExport(SequenceComputationExport that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Name = that.Name.CopyExpression(originalToCopy, procEnv);
            if(that.Graph != null)
                Graph = that.Graph.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationExport(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object path = Name.Evaluate(procEnv);
            IGraph graph = Graph != null ? (IGraph)Graph.Evaluate(procEnv) : procEnv.Graph;
            GraphHelper.Export(path, graph);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Name.GetLocalVariables(variables, constructors);
            if(Graph != null)
                Graph.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Name;
                if(Graph != null)
                    yield return Graph;
                else yield break;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "File::export(" + (Graph!=null ? Graph.Symbol + ", " : "") + Name.Symbol + ")"; }
        }
    }

    public class SequenceComputationDeleteFile : SequenceComputation
    {
        public readonly SequenceExpression Name;

        public SequenceComputationDeleteFile(SequenceExpression expr1)
            : base(SequenceComputationType.DeleteFile)
        {
            Name = expr1;
        }

        protected SequenceComputationDeleteFile(SequenceComputationDeleteFile that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Name = that.Name.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationDeleteFile(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object path = Name.Evaluate(procEnv);
            File.Delete((string)path);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Name.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield return Name; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "File::deleteFile(" + Name.Symbol + ")"; }
        }
    }


    public class SequenceComputationGraphAdd : SequenceComputation
    {
        public readonly SequenceExpression Expr;
        public readonly SequenceExpression ExprSrc;
        public readonly SequenceExpression ExprDst;

        public SequenceComputationGraphAdd(SequenceExpression expr, SequenceExpression exprSrc, SequenceExpression exprDst)
            : base(SequenceComputationType.GraphAdd)
        {
            Expr = expr;
            ExprSrc = exprSrc;
            ExprDst = exprDst;
        }

        protected SequenceComputationGraphAdd(SequenceComputationGraphAdd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
            if(that.ExprSrc != null)
                ExprSrc = that.ExprSrc.CopyExpression(originalToCopy, procEnv);
            if(that.ExprDst != null)
                ExprDst = that.ExprDst.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphAdd(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Expr.Type(env) != "")
            {
                if(ExprSrc != null)
                    CheckEdgeTypeIsKnown(env, Expr, ", first argument");
                else
                    CheckNodeTypeIsKnown(env, Expr, ", first argument");
            }
            if(ExprSrc != null && ExprSrc.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ExprSrc.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "second argument", "node", ExprSrc.Type(env));
            }
            if(ExprDst != null && ExprDst.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "third argument", "node", ExprDst.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string typeString = "";

            if(Expr.Type(env) != "")
                typeString = GetTypeString(env, Expr);

            return typeString;
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(ExprSrc == null)
                return GraphHelper.AddNodeOfType(Expr.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.AddEdgeOfType(Expr.Evaluate(procEnv), (INode)ExprSrc.Evaluate(procEnv), (INode)ExprDst.Evaluate(procEnv), procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Expr.GetLocalVariables(variables, constructors);
            if(ExprSrc != null)
                ExprSrc.GetLocalVariables(variables, constructors);
            if(ExprDst != null)
                ExprDst.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Expr;
                if(ExprSrc != null)
                    yield return ExprSrc;
                if(ExprDst != null)
                    yield return ExprDst;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "add(" + Expr.Symbol + (ExprSrc != null ? "," + ExprSrc.Symbol + "," + ExprDst.Symbol : "") + ")"; }
        }
    }

    public class SequenceComputationGraphRem : SequenceComputation
    {
        public readonly SequenceExpression Expr;

        public SequenceComputationGraphRem(SequenceExpression expr)
            : base(SequenceComputationType.GraphRem)
        {
            Expr = expr;
        }

        protected SequenceComputationGraphRem(SequenceComputationGraphRem that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphRem(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
            
            if(Expr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "Node", env.Model)
                    && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "AEdge", env.Model))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge", Expr.Type(env));
                }
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object delCandidate = Expr.Evaluate(procEnv);
            if(delCandidate is IEdge)
                procEnv.Graph.Remove((IEdge)delCandidate);
            else
            {
                procEnv.Graph.RemoveEdges((INode)delCandidate);
                procEnv.Graph.Remove((INode)delCandidate);
            }
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Expr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield return Expr; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "rem(" + Expr.Symbol + ")"; }
        }
    }

    public class SequenceComputationGraphClear : SequenceComputation
    {
        public SequenceComputationGraphClear()
            : base(SequenceComputationType.GraphClear)
        {
        }

        protected SequenceComputationGraphClear(SequenceComputationGraphClear that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphClear(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.Clear();
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "clear()"; }
        }
    }

    public class SequenceComputationGraphRetype : SequenceComputation
    {
        public readonly SequenceExpression ElemExpr;
        public readonly SequenceExpression TypeExpr;

        public SequenceComputationGraphRetype(SequenceExpression elemExpr, SequenceExpression typeExpr)
            : base(SequenceComputationType.GraphRetype)
        {
            ElemExpr = elemExpr;
            TypeExpr = typeExpr;
        }

        protected SequenceComputationGraphRetype(SequenceComputationGraphRetype that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            ElemExpr = that.ElemExpr.CopyExpression(originalToCopy, procEnv);
            TypeExpr = that.TypeExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphRetype(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(ElemExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ElemExpr.Type(env), "Node", env.Model)
                    && !TypesHelper.IsSameOrSubtype(ElemExpr.Type(env), "AEdge", env.Model))
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", first argument", "node or edge", ElemExpr.Type(env));
                }
            }

            CheckGraphElementTypeIsKnown(env, TypeExpr, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string typeString = "";

            if(TypeExpr.Type(env) != "")
                typeString = GetTypeString(env, TypeExpr);

            return typeString;
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            return GraphHelper.RetypeGraphElement((IGraphElement)ElemExpr.Evaluate(procEnv), TypeExpr.Evaluate(procEnv), procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            ElemExpr.GetLocalVariables(variables, constructors);
            TypeExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return ElemExpr;
                yield return TypeExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "retype(" + ElemExpr.Symbol + "," + TypeExpr.Symbol + ")"; }
        }
    }

    public class SequenceComputationGraphAddCopy : SequenceComputation
    {
        public readonly SequenceExpression Expr;
        public readonly SequenceExpression ExprSrc;
        public readonly SequenceExpression ExprDst;
        public readonly bool Deep;

        public SequenceComputationGraphAddCopy(SequenceExpression expr, SequenceExpression exprSrc, SequenceExpression exprDst, bool deep)
            : base(SequenceComputationType.GraphAddCopy)
        {
            Expr = expr;
            ExprSrc = exprSrc;
            ExprDst = exprDst;
            Deep = deep;
        }

        protected SequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Expr = that.Expr.CopyExpression(originalToCopy, procEnv);
            if(that.ExprSrc != null)
                ExprSrc = that.ExprSrc.CopyExpression(originalToCopy, procEnv);
            if(that.ExprDst != null)
                ExprDst = that.ExprDst.CopyExpression(originalToCopy, procEnv);
            Deep = that.Deep;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphAddCopy(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Expr.Type(env) != "")
            {
                if(ExprSrc != null)
                {
                    if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "AEdge", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(Symbol + "first argument", "edge", Expr.Type(env));
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "Node", env.Model))
                        throw new SequenceParserExceptionTypeMismatch(Symbol + "first argument", "node", Expr.Type(env));
                }
            }
            if(ExprSrc != null && ExprSrc.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ExprSrc.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "second argument", "node", ExprSrc.Type(env));
            }
            if(ExprDst != null && ExprDst.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "third argument", "node", ExprDst.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return Expr.Type(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(ExprSrc == null)
            {
                if(Deep)
                    return GraphHelper.AddCopyOfNode(Expr.Evaluate(procEnv), procEnv.Graph);
                else
                    return GraphHelper.AddCloneOfNode(Expr.Evaluate(procEnv), procEnv.Graph);
            }
            else
            {
                if(Deep)
                    return GraphHelper.AddCopyOfEdge(Expr.Evaluate(procEnv), (INode)ExprSrc.Evaluate(procEnv), (INode)ExprDst.Evaluate(procEnv), procEnv.Graph);
                else
                    return GraphHelper.AddCloneOfEdge(Expr.Evaluate(procEnv), (INode)ExprSrc.Evaluate(procEnv), (INode)ExprDst.Evaluate(procEnv), procEnv.Graph);
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Expr.GetLocalVariables(variables, constructors);
            if(ExprSrc != null)
                ExprSrc.GetLocalVariables(variables, constructors);
            if(ExprDst != null)
                ExprDst.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Expr;
                if(ExprSrc != null)
                    yield return ExprSrc;
                if(ExprDst != null)
                    yield return ExprDst;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return (Deep ? "addCopy(" : "addClone(") + Expr.Symbol + (ExprSrc != null ? "," + ExprSrc.Symbol + "," + ExprDst.Symbol : "") + ")"; }
        }
    }

    public class SequenceComputationGraphMerge : SequenceComputation
    {
        public readonly SequenceExpression TargetNodeExpr;
        public readonly SequenceExpression SourceNodeExpr;

        public SequenceComputationGraphMerge(SequenceExpression targetNodeExpr, SequenceExpression sourceNodeExpr)
            : base(SequenceComputationType.GraphMerge)
        {
            TargetNodeExpr = targetNodeExpr;
            SourceNodeExpr = sourceNodeExpr;
        }

        protected SequenceComputationGraphMerge(SequenceComputationGraphMerge that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            TargetNodeExpr = that.TargetNodeExpr.CopyExpression(originalToCopy, procEnv);
            SourceNodeExpr = that.SourceNodeExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphMerge(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "node", TargetNodeExpr.Type(env));
            }

            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "node", SourceNodeExpr.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object targetNode = TargetNodeExpr.Evaluate(procEnv);
            object sourceNode = SourceNodeExpr.Evaluate(procEnv);
            procEnv.Graph.Merge((INode)targetNode, (INode)sourceNode, "merge");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            TargetNodeExpr.GetLocalVariables(variables, constructors);
            SourceNodeExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return TargetNodeExpr;
                yield return SourceNodeExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "merge(" + TargetNodeExpr.Symbol + ", " + SourceNodeExpr.Symbol + ")"; }
        }
    }

    public class SequenceComputationGraphRedirectSource : SequenceComputation
    {
        public readonly SequenceExpression EdgeExpr;
        public readonly SequenceExpression SourceNodeExpr;

        public SequenceComputationGraphRedirectSource(SequenceExpression edgeExpr, SequenceExpression sourceNodeExpr)
            : base(SequenceComputationType.GraphRedirectSource)
        {
            EdgeExpr = edgeExpr;
            SourceNodeExpr = sourceNodeExpr;
        }

        protected SequenceComputationGraphRedirectSource(SequenceComputationGraphRedirectSource that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            EdgeExpr = that.EdgeExpr.CopyExpression(originalToCopy, procEnv);
            SourceNodeExpr = that.SourceNodeExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphRedirectSource(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(EdgeExpr.Type(env), "AEdge", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "first argument", "edge", EdgeExpr.Type(env));
            }
            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "second argument", "node", SourceNodeExpr.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.RedirectSource((IEdge)EdgeExpr.Evaluate(procEnv), (INode)SourceNodeExpr.Evaluate(procEnv), "old source");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            EdgeExpr.GetLocalVariables(variables, constructors);
            SourceNodeExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return EdgeExpr;
                yield return SourceNodeExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "redirectSource(" + EdgeExpr.Symbol + "," + SourceNodeExpr.Symbol + ")"; }
        }
    }

    public class SequenceComputationGraphRedirectTarget : SequenceComputation
    {
        public readonly SequenceExpression EdgeExpr;
        public readonly SequenceExpression TargetNodeExpr;

        public SequenceComputationGraphRedirectTarget(SequenceExpression edgeExpr, SequenceExpression targetNodeExpr)
            : base(SequenceComputationType.GraphRedirectTarget)
        {
            EdgeExpr = edgeExpr;
            TargetNodeExpr = targetNodeExpr;
        }

        protected SequenceComputationGraphRedirectTarget(SequenceComputationGraphRedirectTarget that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            EdgeExpr = that.EdgeExpr.CopyExpression(originalToCopy, procEnv);
            TargetNodeExpr = that.TargetNodeExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphRedirectTarget(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(EdgeExpr.Type(env), "AEdge", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "first argument", "edge", EdgeExpr.Type(env));
            }
            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "second argument", "node", TargetNodeExpr.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.RedirectTarget((IEdge)EdgeExpr.Evaluate(procEnv), (INode)TargetNodeExpr.Evaluate(procEnv), "old target");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            EdgeExpr.GetLocalVariables(variables, constructors);
            TargetNodeExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return EdgeExpr;
                yield return TargetNodeExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "redirectSourceAndTarget(" + EdgeExpr.Symbol + "," + TargetNodeExpr.Symbol + ")"; }
        }
    }
    
    public class SequenceComputationGraphRedirectSourceAndTarget : SequenceComputation
    {
        public readonly SequenceExpression EdgeExpr;
        public readonly SequenceExpression SourceNodeExpr;
        public readonly SequenceExpression TargetNodeExpr;

        public SequenceComputationGraphRedirectSourceAndTarget(SequenceExpression edgeExpr, SequenceExpression sourceNodeExpr, SequenceExpression targetNodeExpr)
            : base(SequenceComputationType.GraphRedirectSourceAndTarget)
        {
            EdgeExpr = edgeExpr;
            SourceNodeExpr = sourceNodeExpr;
            TargetNodeExpr = targetNodeExpr;
        }

        protected SequenceComputationGraphRedirectSourceAndTarget(SequenceComputationGraphRedirectSourceAndTarget that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            EdgeExpr = that.EdgeExpr.CopyExpression(originalToCopy, procEnv);
            SourceNodeExpr = that.SourceNodeExpr.CopyExpression(originalToCopy, procEnv);
            TargetNodeExpr = that.TargetNodeExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGraphRedirectSourceAndTarget(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(EdgeExpr.Type(env), "AEdge", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "first argument", "edge", EdgeExpr.Type(env));
            }
            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "second argument", "node", SourceNodeExpr.Type(env));
            }
            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + "third argument", "node", TargetNodeExpr.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.RedirectSourceAndTarget((IEdge)EdgeExpr.Evaluate(procEnv), (INode)SourceNodeExpr.Evaluate(procEnv), (INode)TargetNodeExpr.Evaluate(procEnv), "old source", "old target");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            EdgeExpr.GetLocalVariables(variables, constructors);
            SourceNodeExpr.GetLocalVariables(variables, constructors);
            TargetNodeExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return EdgeExpr;
                yield return SourceNodeExpr;
                yield return TargetNodeExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "redirectSourceAndTarget(" + EdgeExpr.Symbol + "," + SourceNodeExpr.Symbol + "," + TargetNodeExpr.Symbol + ")"; }
        }
    }


    public class SequenceComputationInsert : SequenceComputation
    {
        public readonly SequenceExpression Graph;

        public SequenceComputationInsert(SequenceExpression graph)
            : base(SequenceComputationType.Insert)
        {
            Graph = graph;
        }

        protected SequenceComputationInsert(SequenceComputationInsert that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Graph = that.Graph.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationInsert(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Graph.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!TypesHelper.IsSameOrSubtype(Graph.Type(env), "graph", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "graph", Graph.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object graph = Graph.Evaluate(procEnv);
            GraphHelper.Insert((IGraph)graph, procEnv.Graph);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Graph.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield return Graph; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "insert(" + Graph.Symbol + ")"; }
        }
    }

    public class SequenceComputationInsertCopy : SequenceComputation
    {
        public readonly SequenceExpression Graph;
        public readonly SequenceExpression RootNode;

        public SequenceComputationInsertCopy(SequenceExpression graph, SequenceExpression rootNode)
            : base(SequenceComputationType.InsertCopy)
        {
            Graph = graph;
            RootNode = rootNode;
        }

        protected SequenceComputationInsertCopy(SequenceComputationInsertCopy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            Graph = that.Graph.CopyExpression(originalToCopy, procEnv);
            RootNode = that.RootNode.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationInsertCopy(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Graph.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!TypesHelper.IsSameOrSubtype(Graph.Type(env), "graph", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "graph", Graph.Type(env));
            }

            if(RootNode.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(RootNode.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "Node", RootNode.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootNode.Type(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object graph = Graph.Evaluate(procEnv);
            object rootNode = RootNode.Evaluate(procEnv);
            return GraphHelper.InsertCopy((IGraph)graph, (INode)rootNode, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Graph.GetLocalVariables(variables, constructors);
            RootNode.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Graph;
                yield return RootNode;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "insert(" + Graph.Symbol + "," + RootNode.Symbol + ")"; }
        }
    }

    public class SequenceComputationInsertInduced : SequenceComputation
    {
        public readonly SequenceExpression NodeSet;
        public readonly SequenceExpression RootNode;

        public SequenceComputationInsertInduced(SequenceExpression nodeSet, SequenceExpression rootNode)
            : base(SequenceComputationType.InsertInduced)
        {
            NodeSet = nodeSet;
            RootNode = rootNode;
        }

        protected SequenceComputationInsertInduced(SequenceComputationInsertInduced that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            NodeSet = that.NodeSet.CopyExpression(originalToCopy, procEnv);
            RootNode = that.RootNode.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationInsertInduced(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(NodeSet.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!NodeSet.Type(env).StartsWith("set<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Node> type", NodeSet.Type(env));
                if(TypesHelper.ExtractSrc(NodeSet.Type(env)) != "Node")
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Node> type", NodeSet.Type(env));
            }

            if(RootNode.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(RootNode.Type(env), "Node", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "Node", RootNode.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootNode.Type(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object nodeSet = NodeSet.Evaluate(procEnv);
            object rootNode = RootNode.Evaluate(procEnv);
            return GraphHelper.InsertInduced((IDictionary<INode, SetValueType>)nodeSet, (INode)rootNode, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            NodeSet.GetLocalVariables(variables, constructors);
            RootNode.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return NodeSet;
                yield return RootNode;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "insertInduced(" + NodeSet.Symbol + "," + RootNode.Symbol + ")"; }
        }
    }

    public class SequenceComputationInsertDefined : SequenceComputation
    {
        public readonly SequenceExpression EdgeSet;
        public readonly SequenceExpression RootEdge;

        public SequenceComputationInsertDefined(SequenceExpression edgeSet, SequenceExpression rootEdge)
            : base(SequenceComputationType.InsertDefined)
        {
            EdgeSet = edgeSet;
            RootEdge = rootEdge;
        }

        protected SequenceComputationInsertDefined(SequenceComputationInsertDefined that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            EdgeSet = that.EdgeSet.CopyExpression(originalToCopy, procEnv);
            RootEdge = that.RootEdge.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationInsertDefined(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeSet.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!EdgeSet.Type(env).StartsWith("set<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Edge> type", EdgeSet.Type(env));
                if(TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "AEdge"
                    && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "Edge"
                    && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "UEdge")
                {
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "set<Edge> type", EdgeSet.Type(env));
                }
            }

            if(RootEdge.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(RootEdge.Type(env), "AEdge", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "Edge", RootEdge.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootEdge.Type(env);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object edgeSet = EdgeSet.Evaluate(procEnv);
            object rootEdge = RootEdge.Evaluate(procEnv);
            return GraphHelper.InsertDefined((IDictionary)edgeSet, (IEdge)rootEdge, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            EdgeSet.GetLocalVariables(variables, constructors);
            RootEdge.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return EdgeSet;
                yield return RootEdge;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "insertDefined(" + EdgeSet.Symbol + "," + RootEdge.Symbol + ")"; }
        }
    }

    public class SequenceComputationSynchronizationEnter : SequenceComputation
    {
        public readonly SequenceExpression LockObjectExpr;

        public SequenceComputationSynchronizationEnter(SequenceExpression lockObjectExpr)
            : base(SequenceComputationType.SynchronizationEnter)
        {
            LockObjectExpr = lockObjectExpr;
        }

        protected SequenceComputationSynchronizationEnter(SequenceComputationSynchronizationEnter that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            LockObjectExpr = that.LockObjectExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationSynchronizationEnter(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsLockableType(LockObjectExpr.Type(env), env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "lockable type (value types are not lockable)", LockObjectExpr.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object lockObject = LockObjectExpr.Evaluate(procEnv);
            Monitor.Enter(lockObject);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            LockObjectExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return LockObjectExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Synchronization::enter(" + LockObjectExpr.Symbol + ")"; }
        }
    }

    public class SequenceComputationSynchronizationTryEnter : SequenceComputation
    {
        public readonly SequenceExpression LockObjectExpr;

        public SequenceComputationSynchronizationTryEnter(SequenceExpression lockObjectExpr)
            : base(SequenceComputationType.SynchronizationTryEnter)
        {
            LockObjectExpr = lockObjectExpr;
        }

        protected SequenceComputationSynchronizationTryEnter(SequenceComputationSynchronizationTryEnter that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            LockObjectExpr = that.LockObjectExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationSynchronizationTryEnter(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsLockableType(LockObjectExpr.Type(env), env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "lockable type (value types are not lockable)", LockObjectExpr.Type(env));
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object lockObject = LockObjectExpr.Evaluate(procEnv);
            return Monitor.TryEnter(lockObject);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            LockObjectExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return LockObjectExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Synchronization::tryenter(" + LockObjectExpr.Symbol + ")"; }
        }
    }

    public class SequenceComputationSynchronizationExit : SequenceComputation
    {
        public readonly SequenceExpression LockObjectExpr;

        public SequenceComputationSynchronizationExit(SequenceExpression lockObjectExpr)
            : base(SequenceComputationType.SynchronizationExit)
        {
            LockObjectExpr = lockObjectExpr;
        }

        protected SequenceComputationSynchronizationExit(SequenceComputationSynchronizationExit that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            LockObjectExpr = that.LockObjectExpr.CopyExpression(originalToCopy, procEnv);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationSynchronizationExit(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsLockableType(LockObjectExpr.Type(env), env.Model))
                throw new SequenceParserExceptionTypeMismatch(Symbol, "lockable type (value types are not lockable)", LockObjectExpr.Type(env));
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object lockObject = LockObjectExpr.Evaluate(procEnv);
            Monitor.Exit(lockObject);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            LockObjectExpr.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return LockObjectExpr;
            }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        public override string Symbol
        {
            get { return "Synchronization::tryenter(" + LockObjectExpr.Symbol + ")"; }
        }
    }

    public class SequenceComputationGetEquivalentOrAdd : SequenceComputation
    {
        public readonly SequenceExpression Subgraph;
        public readonly SequenceExpression SubgraphArray;
        public readonly bool IncludingAttributes;

        public SequenceComputationGetEquivalentOrAdd(SequenceExpression subgraph, SequenceExpression subgraphArray, bool includingAttributes)
            : base(SequenceComputationType.GetEquivalentOrAdd)
        {
            Subgraph = subgraph;
            SubgraphArray = subgraphArray;
            IncludingAttributes = includingAttributes;
        }

        protected SequenceComputationGetEquivalentOrAdd(SequenceComputationGetEquivalentOrAdd that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
          : base(that)
        {
            Subgraph = that.Subgraph.CopyExpression(originalToCopy, procEnv);
            SubgraphArray = that.SubgraphArray.CopyExpression(originalToCopy, procEnv);
            IncludingAttributes = that.IncludingAttributes;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationGetEquivalentOrAdd(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Subgraph.Type(env) != "")
            {
                if(Subgraph.Type(env) != "graph")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", first argument", "graph type", Subgraph.Type(env));
            }

            if(SubgraphArray.Type(env) != "")
            {
                if(!SubgraphArray.Type(env).StartsWith("array<"))
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "array<graph> type", SubgraphArray.Type(env));
                if(TypesHelper.ExtractSrc(SubgraphArray.Type(env)) != "graph")
                    throw new SequenceParserExceptionTypeMismatch(Symbol + ", second argument", "array<graph> type", SubgraphArray.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "graph";
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            object subgraph = Subgraph.Evaluate(procEnv);
            object subgraphArray = SubgraphArray.Evaluate(procEnv);
            return GraphHelper.GetEquivalentOrAdd((IGraph)subgraph, (IList<IGraph>)subgraphArray, IncludingAttributes);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            Subgraph.GetLocalVariables(variables, constructors);
            SubgraphArray.GetLocalVariables(variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get
            {
                yield return Subgraph;
                yield return SubgraphArray;
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
                    return "getEquivalentOrAdd(" + Subgraph.Symbol + ", " + SubgraphArray.Symbol + ")";
                else
                    return "getEquivalentStructurallyOrAdd(" + Subgraph.Symbol + ", " + SubgraphArray.Symbol + ")";
            }
        }
    }

    public class SequenceComputationBuiltinProcedureCall : SequenceComputation
    {
        public readonly SequenceComputation BuiltinProcedure;
        public readonly List<SequenceVariable> ReturnVars;

        public SequenceComputationBuiltinProcedureCall(SequenceComputation builtinProcedure, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.BuiltinProcedureCall)
        {
            BuiltinProcedure = builtinProcedure;
            ReturnVars = returnVars;
        }

        protected SequenceComputationBuiltinProcedureCall(SequenceComputationBuiltinProcedureCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            BuiltinProcedure = that.BuiltinProcedure.Copy(originalToCopy, procEnv);
            ReturnVars = new List<SequenceVariable>();
            for(int i = 0; i < that.ReturnVars.Count; ++i)
            {
                ReturnVars.Add(that.ReturnVars[i].Copy(originalToCopy, procEnv));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationBuiltinProcedureCall(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(!(BuiltinProcedure is SequenceComputationVAlloc) 
                && !(BuiltinProcedure is SequenceComputationGraphAdd)
                && !(BuiltinProcedure is SequenceComputationGraphRetype)
                && !(BuiltinProcedure is SequenceComputationInsertInduced)
                && !(BuiltinProcedure is SequenceComputationInsertDefined)
                && !(BuiltinProcedure is SequenceComputationSynchronizationTryEnter)
                && !(BuiltinProcedure is SequenceComputationGetEquivalentOrAdd))
            {
                throw new Exception("Procedure call of builtin unknown procedure");
            }
            if(ReturnVars.Count > 1)
                throw new Exception("Procedure returns 1 output value, can't assign more");
            BuiltinProcedure.Check(env);
            // 0 Return Vars, i.e. throwing away the value returned is ok
            if(ReturnVars.Count == 0)
                return;

            if(BuiltinProcedure is SequenceComputationVAlloc
                || BuiltinProcedure is SequenceComputationGraphAdd
                || BuiltinProcedure is SequenceComputationGraphRetype
                || BuiltinProcedure is SequenceComputationInsertInduced
                || BuiltinProcedure is SequenceComputationInsertDefined
                || BuiltinProcedure is SequenceComputationSynchronizationTryEnter
                || BuiltinProcedure is SequenceComputationGetEquivalentOrAdd
                ) 
            {
                if(!TypesHelper.IsSameOrSubtype(BuiltinProcedure.Type(env), ReturnVars[0].Type, env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, ReturnVars[0].Type, BuiltinProcedure.Type(env));
            }
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            if(ReturnVars.Count > 0)
                ReturnVars[0].SetVariableValue(BuiltinProcedure.ExecuteImpl(procEnv), procEnv);
            else
            {
                if(BuiltinProcedure is SequenceComputationGraphAdd)
                {
                    SequenceComputationGraphAdd add = (SequenceComputationGraphAdd)BuiltinProcedure;
                }
                BuiltinProcedure.ExecuteImpl(procEnv);
            }
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            BuiltinProcedure.GetLocalVariables(variables, constructors);
            for(int i = 0; i < ReturnVars.Count; ++i)
            {
                ReturnVars[i].GetLocalVariables(variables);
            }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        protected String GetProcedureString()
        {
            StringBuilder sb = new StringBuilder();
            if(ReturnVars.Count > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ReturnVars.Count; ++i)
                {
                    sb.Append(ReturnVars[i].Name);
                    if(i != ReturnVars.Count - 1)
                        sb.Append(",");
                }
                sb.Append(")=");
            }
            sb.Append(BuiltinProcedure.Symbol);
            return sb.ToString();
        }

        public override string Symbol
        {
            get { return GetProcedureString(); }
        }
    }
    
    public abstract class SequenceComputationProcedureCall : SequenceComputation, ProcedureInvocation
    {
        /// <summary>
        /// An array of expressions used to compute the input arguments.
        /// </summary>
        public readonly SequenceExpression[] ArgumentExpressions;

        /// <summary>
        /// Buffer to store the argument values for the call; used to avoid unneccessary memory allocations.
        /// </summary>
        public readonly object[] Arguments;

        /// <summary>
        /// An array of variables used for the return values. Might be empty if the caller is not interested in available returns values.
        /// </summary>
        public readonly SequenceVariable[] ReturnVars;

        public abstract String Name { get; }
        public abstract String Package { get; }
        public abstract String PackagePrefixedName { get; }

        public abstract bool IsExternal { get; }

        protected SequenceComputationProcedureCall(List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.ProcedureCall)
        {
            InitializeArgumentExpressionsAndArguments(argExprs, out ArgumentExpressions, out Arguments);
            InitializeReturnVariables(returnVars, out ReturnVars);
        }

        protected SequenceComputationProcedureCall(SequenceComputationType seqCompType, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(seqCompType)
        {
            InitializeArgumentExpressionsAndArguments(argExprs, out ArgumentExpressions, out Arguments);
            InitializeReturnVariables(returnVars, out ReturnVars);
        }

        protected SequenceComputationProcedureCall(SequenceComputationProcedureCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            CopyArgumentExpressionsAndArguments(originalToCopy, procEnv, that.ArgumentExpressions,
                out ArgumentExpressions, out Arguments);
            CopyVars(originalToCopy, procEnv, that.ReturnVars,
                out ReturnVars);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckProcedureCall(this);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            GetLocalVariables(ArgumentExpressions, variables, constructors);
            GetLocalVariables(ReturnVars, variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        protected virtual String GetProcedureString()
        {
            StringBuilder sb = new StringBuilder();
            if(ReturnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    sb.Append(ReturnVars[i].Name);
                    if(i != ReturnVars.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")=");
            }
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
            get { return GetProcedureString(); }
        }
    }

    public class SequenceComputationProcedureCallInterpreted : SequenceComputationProcedureCall
    {
        /// <summary>
        /// The procedure to be used
        /// </summary>
        public readonly IProcedureDefinition ProcedureDef;

        public override string Name
        {
            get { return ProcedureDef.Name; }
        }

        public override String Package
        {
            get { return ProcedureDef.Package; }
        }

        public override String PackagePrefixedName
        {
            get { return ProcedureDef.Package != null ? ProcedureDef.Package + "::" + ProcedureDef.Name : ProcedureDef.Name; }
        }

        public override bool IsExternal
        {
            get { return ProcedureDef.IsExternal; }
        }

        public SequenceComputationProcedureCallInterpreted(IProcedureDefinition ProcedureDef,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(argExprs, returnVars)
        {
            this.ProcedureDef = ProcedureDef;
        }

        protected SequenceComputationProcedureCallInterpreted(SequenceComputationProcedureCallInterpreted that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            ProcedureDef = that.ProcedureDef;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationProcedureCallInterpreted(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IProcedureDefinition procedureDef = ProcedureDef;
            FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);
            object[] resultValues = procedureDef.Apply(procEnv, procEnv.Graph, Arguments);
            if(ReturnVars.Length > 0)
                FillReturnVariablesFromValues(ReturnVars, procEnv, resultValues);
            return null;
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }
    }

    public class SequenceComputationProcedureCallCompiled : SequenceComputationProcedureCall
    {
        public readonly String name;
        public readonly String package;
        public readonly String packagePrefixedName;
        public bool isExternal;

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

        public SequenceComputationProcedureCallCompiled(String Name, String Package, String PackagePrefixedName,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, bool IsExternalProcedureCalled)
            : base(argExprs, returnVars)
        {
            this.name = Name;
            this.package = Package;
            this.packagePrefixedName = PackagePrefixedName;
            this.isExternal = IsExternalProcedureCalled;
        }

        protected SequenceComputationProcedureCallCompiled(SequenceComputationProcedureCallCompiled that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            name = that.name;
            package = that.package;
            packagePrefixedName = that.packagePrefixedName;
            isExternal = that.isExternal;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationProcedureCallCompiled(this, originalToCopy, procEnv);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }
    }

    public class SequenceComputationProcedureMethodCall : SequenceComputationProcedureCall, MethodProcedureInvocation
    {
        public readonly SequenceExpression TargetExpr;
        public readonly SequenceVariable TargetVar;

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

        public SequenceComputationProcedureMethodCall(SequenceExpression targetExpr, 
            String name, 
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.ProcedureMethodCall, argExprs, returnVars)
        {
            TargetExpr = targetExpr;
            this.name = name;
        }

        public SequenceComputationProcedureMethodCall(SequenceVariable targetVar, 
            String name, 
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.ProcedureMethodCall, argExprs, returnVars)
        {
            TargetVar = targetVar;
            this.name = name;
        }

        protected SequenceComputationProcedureMethodCall(SequenceComputationProcedureMethodCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            if(TargetExpr != null)
                TargetExpr = that.TargetExpr.CopyExpression(originalToCopy, procEnv);
            if(TargetVar != null)
                TargetVar = that.TargetVar.Copy(originalToCopy, procEnv);
            name = that.name;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationProcedureMethodCall(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(TargetExpr != null)
                env.CheckProcedureMethodCall(TargetExpr, this);
            else
                env.CheckProcedureMethodCall(TargetVar, this);
        }

        public override object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement owner;
            if(TargetExpr != null)
                owner = (IGraphElement)TargetExpr.Evaluate(procEnv);
            else
                owner = (IGraphElement)TargetVar.GetVariableValue(procEnv);
            FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);
            object[] resultValues = owner.ApplyProcedureMethod(procEnv, procEnv.Graph, Name, Arguments);
            if(ReturnVars.Length > 0)
                FillReturnVariablesFromValues(ReturnVars, procEnv, resultValues);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            if(TargetExpr != null)
                TargetExpr.GetLocalVariables(variables, constructors);
            if(TargetVar != null)
                TargetVar.GetLocalVariables(variables);
            GetLocalVariables(ArgumentExpressions, variables, constructors);
            GetLocalVariables(ReturnVars, variables, constructors);
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }

        public override int Precedence
        {
            get { return 8; }
        }

        protected override String GetProcedureString()
        {
            StringBuilder sb = new StringBuilder();
            if(ReturnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    sb.Append(ReturnVars[i].Name);
                    if(i != ReturnVars.Length - 1)
                        sb.Append(",");
                }
                sb.Append(")=");
            }
            if(TargetExpr != null)
                sb.Append(TargetExpr.Symbol + ".");
            if(TargetVar != null)
                sb.Append(TargetVar.ToString() + ".");
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
    }
}
