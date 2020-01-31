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
    /// Specifies the actual subtype of a sequence computation.
    /// A new expression type -> you must add the corresponding class down below 
    /// and adapt the lgspSequenceGenerator.
    /// </summary>
    public enum SequenceComputationType
    {
        Then,
        VAlloc, VFree, VFreeNonReset, VReset,
        ContainerAdd, ContainerRem, ContainerClear,
        Assignment,
        VariableDeclaration,
        Emit, Record, Export, DeleteFile,
        GraphAdd, GraphRem, GraphClear, GraphRetype,
        GraphAddCopy, GraphMerge, GraphRedirectSource, GraphRedirectTarget, GraphRedirectSourceAndTarget,
        Insert, InsertCopy,
        InsertInduced, InsertDefined,
        ProcedureCall, BuiltinProcedureCall, ProcedureMethodCall,
        DebugAdd, DebugRem, DebugEmit, DebugHalt, DebugHighlight,
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
        public SequenceComputation(SequenceComputationType seqCompType)
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
        public abstract object Execute(IGraphProcessingEnvironment procEnv);

        /// <summary>
        /// Collects all variables of the sequence expression tree into the variables dictionary,
        /// and all container constructors used into the constructors array.
        /// </summary>
        /// <param name="variables">Contains the variables found</param>
        public virtual void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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
            return str.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t").Replace("#", "\\#");
        }

        protected static string Unescape(String str)
        {
            return str.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t").Replace("\\#", "#");
        }
    }

    /// <summary>
    /// A sequence computation on a container object (resulting from a variable or a method call; yielding a container object again)
    /// </summary>
    public abstract class SequenceComputationContainer : SequenceComputation
    {
        public readonly SequenceVariable Container;
        public readonly SequenceExpressionAttributeAccess Attribute;

        public SequenceComputationContainer(SequenceComputationType type, SequenceVariable container)
            : base(type)
        {
            Container = container;
        }

        public SequenceComputationContainer(SequenceComputationType type, SequenceExpressionAttributeAccess attribute)
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
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> or deque<S> type", ContainerType);
            return ContainerType;
        }

        protected object ContainerValue(IGraphProcessingEnvironment procEnv, out IGraphElement elem, out AttributeType attrType)
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

        #region helper methods

        protected static void ChangingAttributePutElement(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, object firstValue, object optionalSecondValue)
        {
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.PutElement, firstValue, optionalSecondValue);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.PutElement, firstValue, optionalSecondValue);
        }

        protected static void ChangingAttributeRemoveElement(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, object valueOrKeyOrIndexToRemove)
        {
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
        }

        protected static void ChangingSetAttributePutElement(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, object firstValue)
        {
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.PutElement, firstValue, null);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.PutElement, firstValue, null);
        }

        protected static void ChangingSetAttributeRemoveElement(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, object valueOrKeyOrIndexToRemove)
        {
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, valueOrKeyOrIndexToRemove, null);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, valueOrKeyOrIndexToRemove, null);
        }

        protected static void ChangingMapAttributePutElement(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, object firstValue, object optionalSecondValue)
        {
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.PutElement, optionalSecondValue, firstValue);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.PutElement, optionalSecondValue, firstValue);
        }

        protected static void ChangingMapAttributeRemoveElement(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, object valueOrKeyOrIndexToRemove)
        {
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
        }

        #endregion helper methods
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            left.Execute(procEnv);
            return right.Execute(procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            left.GetLocalVariables(variables, containerConstructors);
            right.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpression.Type(env));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(procEnv);
            if(Reset)
                procEnv.Graph.FreeVisitedFlag(visitedFlag);
            else
                procEnv.Graph.FreeVisitedFlagNonReset(visitedFlag);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            VisitedFlagExpression.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpression.Type(env));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(procEnv);
            procEnv.Graph.ResetVisitedFlag(visitedFlag);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            VisitedFlagExpression.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, "map or array or deque", ContainerType);

            if(ContainerType.StartsWith("array<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                if(ExprDst != null && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "int", env.Model))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
            }
            else if(ContainerType.StartsWith("deque<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                if(ExprDst != null && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "int", env.Model))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                if(TypesHelper.ExtractDst(ContainerType) != "SetValueType"
                    && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), TypesHelper.ExtractDst(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
                }
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem;
            AttributeType attrType;
            object container = ContainerValue(procEnv, out elem, out attrType);
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

        private object ExecuteArray(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IList array, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                ChangingAttributePutElement(procEnv, elem, attrType, firstValue, optionalSecondValue);

            if(ExprDst == null)
                array.Add(firstValue);
            else
                array.Insert((int)optionalSecondValue, firstValue);

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return array;
        }

        private object ExecuteDeque(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDeque deque, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                ChangingAttributePutElement(procEnv, elem, attrType, firstValue, optionalSecondValue);

            if(ExprDst == null)
                deque.Enqueue(firstValue);
            else
                deque.EnqueueAt((int)optionalSecondValue, firstValue);

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return deque;
        }

        private object ExecuteSet(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDictionary set, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                ChangingSetAttributePutElement(procEnv, elem, attrType, firstValue);

            set[firstValue] = optionalSecondValue;

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return set;
        }

        private object ExecuteMap(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDictionary map, object firstValue, object optionalSecondValue)
        {
            if(elem != null)
                ChangingMapAttributePutElement(procEnv, elem, attrType, firstValue, optionalSecondValue);

            map[firstValue] = optionalSecondValue;

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(Container != null)
                Container.GetLocalVariables(variables);
            if(Attribute != null)
                Attribute.GetLocalVariables(variables, containerConstructors);
            Expr.GetLocalVariables(variables, containerConstructors);
            if(ExprDst != null)
                ExprDst.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "int", Expr.Type(env));
            }
            else if(ContainerType.StartsWith("deque<"))
            {
                if(Expr != null && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "int", env.Model))
                    throw new SequenceParserException(Symbol, "int", Expr.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem;
            AttributeType attrType;
            object container = ContainerValue(procEnv, out elem, out attrType);
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

        private object ExecuteArray(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IList array, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                ChangingAttributeRemoveElement(procEnv, elem, attrType, valueOrKeyOrIndexToRemove);

            if(Expr == null)
                array.RemoveAt(array.Count - 1);
            else
                array.RemoveAt((int)valueOrKeyOrIndexToRemove);

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return array;
        }

        private object ExecuteDeque(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDeque deque, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                ChangingAttributeRemoveElement(procEnv, elem, attrType, valueOrKeyOrIndexToRemove);

            if(Expr == null)
                deque.Dequeue();
            else
                deque.DequeueAt((int)valueOrKeyOrIndexToRemove);

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return deque;
        }

        private static object ExecuteSet(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDictionary set, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                ChangingSetAttributeRemoveElement(procEnv, elem, attrType, valueOrKeyOrIndexToRemove);

            set.Remove(valueOrKeyOrIndexToRemove);

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return set;
        }

        private static object ExecuteMap(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDictionary map, object valueOrKeyOrIndexToRemove)
        {
            if(elem != null)
                ChangingMapAttributeRemoveElement(procEnv, elem, attrType, valueOrKeyOrIndexToRemove);

            map.Remove(valueOrKeyOrIndexToRemove);

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(Container != null)
                Container.GetLocalVariables(variables);
            if(Attribute != null)
                Attribute.GetLocalVariables(variables, containerConstructors);
            if(Expr != null)
                Expr.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem;
            AttributeType attrType;
            object container = ContainerValue(procEnv, out elem, out attrType);

            if(container is IList)
                return ExecuteList(procEnv, elem, attrType, (IList)container);
            else if(container is IDeque)
                return ExecuteDeque(procEnv, elem, attrType, (IDeque)container);
            else if(TypesHelper.ExtractDst(TypesHelper.XgrsTypeOfConstant(container, procEnv.Graph.Model)) == "SetValueType")
                return ExecuteSet(procEnv, elem, attrType, (IDictionary)container);
            else
                return ExecuteMap(procEnv, elem, attrType, (IDictionary)container);
        }

        private static object ExecuteList(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IList array)
        {
            if(elem != null)
            {
                for(int i = array.Count; i >= 0; --i)
                {
                    ChangingAttributeRemoveElement(procEnv, elem, attrType, i);
                }
            }

            array.Clear();

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return array;
        }

        private static object ExecuteDeque(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDeque deque)
        {
            if(elem != null)
            {
                for(int i = deque.Count; i >= 0; --i)
                {
                    ChangingAttributeRemoveElement(procEnv, elem, attrType, i);
                }
            }

            deque.Clear();

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return deque;
        }

        private static object ExecuteSet(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDictionary set)
        {
            if(elem != null)
            {
                foreach(DictionaryEntry kvp in set)
                {
                    ChangingSetAttributeRemoveElement(procEnv, elem, attrType, kvp.Key);
                }
            }

            set.Clear();

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return set;
        }

        private static object ExecuteMap(IGraphProcessingEnvironment procEnv, IGraphElement elem, AttributeType attrType, IDictionary map)
        {
            if(elem != null)
            {
                foreach(DictionaryEntry kvp in map)
                {
                    ChangingMapAttributeRemoveElement(procEnv, elem, attrType, kvp.Key);
                }
            }

            map.Clear();

            if(elem != null)
                ChangedAttribute(procEnv, elem, attrType);

            return map;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(Container != null)
                Container.GetLocalVariables(variables);
            if(Attribute != null)
                Attribute.GetLocalVariables(variables, containerConstructors);
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
                throw new SequenceParserException(Symbol, Target.Type(env), SourceValueProvider.Type(env));
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object value = SourceValueProvider.Execute(procEnv);
            Target.Assign(value, procEnv);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Target.GetLocalVariables(variables, containerConstructors);
            SourceValueProvider.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object value = TypesHelper.DefaultValue(Target.Type, procEnv.Graph.Model);
            Target.SetVariableValue(value, procEnv);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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
                throw new SequenceParserException("The 0 parameter of " + Symbol, "string type", ArgExprs[0].Type(env));

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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugEntering((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Debug::add(");
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugExiting((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Debug::rem(");
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugEmitting((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Debug::emit(");
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i - 1] = ArgExprs[i].Evaluate(procEnv);
            }
            procEnv.DebugHalting((string)ArgExprs[0].Evaluate(procEnv), values);
            return null;
        }

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Debug::halt(");
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
                    throw new SequenceParserException("The " + i + " parameter of " + Symbol, "string type", ArgExprs[i].Type(env));
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
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

        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Debug::highlight(");
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
                        procEnv.EmitWriterDebug.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph));
                }
                else
                {
                    if(value != null)
                        procEnv.EmitWriter.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph));
                }
            }
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            for(int i = 0; i < Expressions.Count; ++i)
            {
                Expressions[i].GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object value = Expression.Evaluate(procEnv);
            if(value is string)
                value = Unescape((string)value);
            if(value != null)
                procEnv.Recorder.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph));
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Expression.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object path = Name.Evaluate(procEnv);
            IGraph graph = Graph != null ? (IGraph)Graph.Evaluate(procEnv) : procEnv.Graph;
            GraphHelper.Export(path, graph);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Name.GetLocalVariables(variables, containerConstructors);
            if(Graph != null)
                Graph.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object path = Name.Evaluate(procEnv);
            File.Delete((string)path);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Name.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + "second argument", "node", ExprSrc.Type(env));
            }
            if(ExprDst != null && ExprDst.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol + "third argument", "node", ExprDst.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string typeString = "";

            if(Expr.Type(env) != "")
                typeString = GetTypeString(env, Expr);

            return typeString;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(ExprSrc == null)
                return GraphHelper.AddNodeOfType(Expr.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.AddEdgeOfType(Expr.Evaluate(procEnv), (INode)ExprSrc.Evaluate(procEnv), (INode)ExprDst.Evaluate(procEnv), procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Expr.GetLocalVariables(variables, containerConstructors);
            if(ExprSrc != null)
                ExprSrc.GetLocalVariables(variables, containerConstructors);
            if(ExprDst != null)
                ExprDst.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "node or edge", Expr.Type(env));
                }
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Expr.GetLocalVariables(variables, containerConstructors);
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.Clear();
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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
                    throw new SequenceParserException(Symbol + ", first argument", "node or edge", ElemExpr.Type(env));
                }
            }

            CheckNodeOrEdgeTypeIsKnown(env, TypeExpr, ", second argument");
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string typeString = "";

            if(TypeExpr.Type(env) != "")
                typeString = GetTypeString(env, TypeExpr);

            return typeString;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return GraphHelper.RetypeGraphElement((IGraphElement)ElemExpr.Evaluate(procEnv), TypeExpr.Evaluate(procEnv), procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ElemExpr.GetLocalVariables(variables, containerConstructors);
            TypeExpr.GetLocalVariables(variables, containerConstructors);
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

        public SequenceComputationGraphAddCopy(SequenceExpression expr, SequenceExpression exprSrc, SequenceExpression exprDst)
            : base(SequenceComputationType.GraphAddCopy)
        {
            Expr = expr;
            ExprSrc = exprSrc;
            ExprDst = exprDst;
        }

        protected SequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
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
                        throw new SequenceParserException(Symbol + "first argument", "edge", Expr.Type(env));
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "Node", env.Model))
                        throw new SequenceParserException(Symbol + "first argument", "node", Expr.Type(env));
                }
            }
            if(ExprSrc != null && ExprSrc.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ExprSrc.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol + "second argument", "node", ExprSrc.Type(env));
            }
            if(ExprDst != null && ExprDst.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol + "third argument", "node", ExprDst.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return Expr.Type(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(ExprSrc == null)
                return GraphHelper.AddCopyOfNode(Expr.Evaluate(procEnv), procEnv.Graph);
            else
                return GraphHelper.AddCopyOfEdge(Expr.Evaluate(procEnv), (INode)ExprSrc.Evaluate(procEnv), (INode)ExprDst.Evaluate(procEnv), procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Expr.GetLocalVariables(variables, containerConstructors);
            if(ExprSrc != null)
                ExprSrc.GetLocalVariables(variables, containerConstructors);
            if(ExprDst != null)
                ExprDst.GetLocalVariables(variables, containerConstructors);
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
            get { return "addCopy(" + Expr.Symbol + (ExprSrc != null ? "," + ExprSrc.Symbol + "," + ExprDst.Symbol : "") + ")"; }
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
                    throw new SequenceParserException(Symbol, "node", TargetNodeExpr.Type(env));
            }

            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol, "node", SourceNodeExpr.Type(env));
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object targetNode = TargetNodeExpr.Evaluate(procEnv);
            object sourceNode = SourceNodeExpr.Evaluate(procEnv);
            procEnv.Graph.Merge((INode)targetNode, (INode)sourceNode, "merge");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            TargetNodeExpr.GetLocalVariables(variables, containerConstructors);
            SourceNodeExpr.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + "first argument", "edge", EdgeExpr.Type(env));
            }
            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol + "second argument", "node", SourceNodeExpr.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.RedirectSource((IEdge)EdgeExpr.Evaluate(procEnv), (INode)SourceNodeExpr.Evaluate(procEnv), "old source");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            EdgeExpr.GetLocalVariables(variables, containerConstructors);
            SourceNodeExpr.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + "first argument", "edge", EdgeExpr.Type(env));
            }
            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol + "second argument", "node", TargetNodeExpr.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.RedirectTarget((IEdge)EdgeExpr.Evaluate(procEnv), (INode)TargetNodeExpr.Evaluate(procEnv), "old target");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            EdgeExpr.GetLocalVariables(variables, containerConstructors);
            TargetNodeExpr.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol + "first argument", "edge", EdgeExpr.Type(env));
            }
            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol + "second argument", "node", SourceNodeExpr.Type(env));
            }
            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol + "third argument", "node", TargetNodeExpr.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            procEnv.Graph.RedirectSourceAndTarget((IEdge)EdgeExpr.Evaluate(procEnv), (INode)SourceNodeExpr.Evaluate(procEnv), (INode)TargetNodeExpr.Evaluate(procEnv), "old source", "old target");
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            EdgeExpr.GetLocalVariables(variables, containerConstructors);
            SourceNodeExpr.GetLocalVariables(variables, containerConstructors);
            TargetNodeExpr.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "graph", Graph.Type(env));
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object graph = Graph.Evaluate(procEnv);
            GraphHelper.Insert((IGraph)graph, procEnv.Graph);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Graph.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "graph", Graph.Type(env));
            }

            if(RootNode.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(RootNode.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol, "Node", RootNode.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootNode.Type(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object graph = Graph.Evaluate(procEnv);
            object rootNode = RootNode.Evaluate(procEnv);
            return GraphHelper.InsertCopy((IGraph)graph, (INode)rootNode, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Graph.GetLocalVariables(variables, containerConstructors);
            RootNode.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
                if(TypesHelper.ExtractSrc(NodeSet.Type(env)) != "Node")
                    throw new SequenceParserException(Symbol, "set<Node> type", NodeSet.Type(env));
            }

            if(RootNode.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(RootNode.Type(env), "Node", env.Model))
                    throw new SequenceParserException(Symbol, "Node", RootNode.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootNode.Type(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object nodeSet = NodeSet.Evaluate(procEnv);
            object rootNode = RootNode.Evaluate(procEnv);
            return GraphHelper.InsertInduced((IDictionary<INode, SetValueType>)nodeSet, (INode)rootNode, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            NodeSet.GetLocalVariables(variables, containerConstructors);
            RootNode.GetLocalVariables(variables, containerConstructors);
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
                    throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
                if(TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "AEdge"
                    && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "Edge"
                    && TypesHelper.ExtractSrc(EdgeSet.Type(env)) != "UEdge")
                {
                    throw new SequenceParserException(Symbol, "set<Edge> type", EdgeSet.Type(env));
                }
            }

            if(RootEdge.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(RootEdge.Type(env), "AEdge", env.Model))
                    throw new SequenceParserException(Symbol, "Edge", RootEdge.Type(env));
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return RootEdge.Type(env);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object edgeSet = EdgeSet.Evaluate(procEnv);
            object rootEdge = RootEdge.Evaluate(procEnv);
            return GraphHelper.InsertDefined((IDictionary)edgeSet, (IEdge)rootEdge, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            EdgeSet.GetLocalVariables(variables, containerConstructors);
            RootEdge.GetLocalVariables(variables, containerConstructors);
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
                && !(BuiltinProcedure is SequenceComputationInsertDefined))
            {
                throw new Exception("Procedure call of builtin unknown procedure");
            }
            if(ReturnVars.Count > 1)
                throw new Exception("Procedure returns 1 output value, can't assign more");
            // 0 Return Vars, i.e. throwing away the value returned is ok
            if(ReturnVars.Count == 0)
                return;

            if(BuiltinProcedure is SequenceComputationVAlloc
                || BuiltinProcedure is SequenceComputationGraphAdd
                || BuiltinProcedure is SequenceComputationGraphRetype
                || BuiltinProcedure is SequenceComputationInsertInduced
                || BuiltinProcedure is SequenceComputationInsertDefined
                ) 
            {
                if(!TypesHelper.IsSameOrSubtype(BuiltinProcedure.Type(env), ReturnVars[0].Type, env.Model))
                    throw new SequenceParserException(Symbol, ReturnVars[0].Type, BuiltinProcedure.Type(env));
            }
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            if(ReturnVars.Count > 0)
                ReturnVars[0].SetVariableValue(BuiltinProcedure.Execute(procEnv), procEnv);
            else
                BuiltinProcedure.Execute(procEnv);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            BuiltinProcedure.GetLocalVariables(variables, containerConstructors);
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
    
    public abstract class SequenceComputationProcedureCall : SequenceComputation
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

        public abstract ProcedureInvocation ProcedureInvocation { get; }
        public abstract String NameForProcedureString { get; }

        public bool IsExternalProcedureCalled;

        public SequenceComputationProcedureCall(List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.ProcedureCall)
        {
            InitializeArgumentExpressionsAndArguments(argExprs, out ArgumentExpressions, out Arguments);
            InitializeReturnVariables(returnVars, out ReturnVars);
        }

        public SequenceComputationProcedureCall(SequenceComputationType seqCompType, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
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
            IsExternalProcedureCalled = that.IsExternalProcedureCalled;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckProcedureCall(this);
            IsExternalProcedureCalled = env.IsProcedureCallExternal(ProcedureInvocation);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            GetLocalVariables(ArgumentExpressions, variables, containerConstructors);
            GetLocalVariables(ReturnVars, variables, containerConstructors);
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
            sb.Append(NameForProcedureString);
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

        public override ProcedureInvocation ProcedureInvocation
        {
            get { return new ProcedureInvocationInterpreted(ProcedureDef); }
        }

        public override string NameForProcedureString
        {
            get { return ProcedureDef.Name; }
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
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
        /// <summary>
        /// The name of the procedure.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// null if this is a call of a global procedure, otherwise the package the call target is contained in.
        /// </summary>
        public readonly String Package;

        /// <summary>
        /// The name of the procedure, prefixed by the package it is contained in (separated by a double colon), if it is contained in a package.
        /// </summary>
        public readonly String PackagePrefixedName;

        public override ProcedureInvocation ProcedureInvocation
        {
            get { return new ProcedureInvocationCompiled(Name, Package, PackagePrefixedName); }
        }

        public override string NameForProcedureString
        {
            get { return Name; }
        }

        public SequenceComputationProcedureCallCompiled(String Name, String PrePackage, String PrePackageContext, bool unprefixedProcedureNameExists,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(argExprs, returnVars)
        {
            this.Name = Name;

            ResolvePackage(Name, PrePackage, PrePackageContext, unprefixedProcedureNameExists,
                        out Package, out PackagePrefixedName);
        }

        protected SequenceComputationProcedureCallCompiled(SequenceComputationProcedureCallCompiled that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            Name = that.Name;
            Package = that.Package;
            PackagePrefixedName = that.PackagePrefixedName;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new SequenceComputationProcedureCallCompiled(this, originalToCopy, procEnv);
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
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

    public class SequenceComputationProcedureMethodCall : SequenceComputationProcedureCall
    {
        public readonly SequenceExpression TargetExpr;
        public readonly SequenceVariable TargetVar;

        readonly String Name;

        public override ProcedureInvocation ProcedureInvocation
        {
            get { return new MethodProcedureInvocation(Name); }
        }

        public override string NameForProcedureString
        {
            get { return Name; }
        }

        public SequenceComputationProcedureMethodCall(SequenceExpression targetExpr, 
            String name, 
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.ProcedureMethodCall, argExprs, returnVars)
        {
            TargetExpr = targetExpr;
            Name = name;
        }

        public SequenceComputationProcedureMethodCall(SequenceVariable targetVar, 
            String name, 
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.ProcedureMethodCall, argExprs, returnVars)
        {
            TargetVar = targetVar;
            Name = name;
        }

        protected SequenceComputationProcedureMethodCall(SequenceComputationProcedureMethodCall that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that, originalToCopy, procEnv)
        {
            if(TargetExpr != null)
                TargetExpr = that.TargetExpr.CopyExpression(originalToCopy, procEnv);
            if(TargetVar != null)
                TargetVar = that.TargetVar.Copy(originalToCopy, procEnv);
            Name = that.Name;
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

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement owner;
            if(TargetExpr != null)
                owner = (IGraphElement)TargetExpr.Evaluate(procEnv);
            else
                owner = (IGraphElement)TargetVar.GetVariableValue(procEnv);
            FillArgumentsFromArgumentExpressions(ArgumentExpressions, Arguments, procEnv);
            object[] resultValues = owner.ApplyProcedureMethod(procEnv, procEnv.Graph, NameForProcedureString, Arguments);
            if(ReturnVars.Length > 0)
                FillReturnVariablesFromValues(ReturnVars, procEnv, resultValues);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(TargetExpr != null)
                TargetExpr.GetLocalVariables(variables, containerConstructors);
            if(TargetVar != null)
                TargetVar.GetLocalVariables(variables);
            GetLocalVariables(ArgumentExpressions, variables, containerConstructors);
            GetLocalVariables(ReturnVars, variables, containerConstructors);
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
            sb.Append(NameForProcedureString);
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
