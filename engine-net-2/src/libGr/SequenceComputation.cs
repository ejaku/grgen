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
        public SequenceComputationType SequenceComputationType;

        /// <summary>
        /// Initializes a new SequenceComputation object with the given sequence computation type.
        /// </summary>
        /// <param name="seqCompType">The sequence computation type.</param>
        public SequenceComputation(SequenceComputationType seqCompType)
        {
            SequenceComputationType = seqCompType;

            id = idSource;
            ++idSource;
        }

        /// <summary>
        /// Checks the sequence computation for errors utilizing the given checking environment
        /// reports them by exception
        /// default behavior: check all the children 
        /// </summary>
        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(SequenceComputation childSeq in Children)
                childSeq.Check(env);
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

        public override IEnumerable<SequenceBase> ChildrenBase { get { foreach(SequenceBase child in Children) yield return child; } }

        /// <summary>
        /// Tells whether Execute returns a value to be used as a result determining value for a boolean computation sequence.
        /// Only expressions do so, the values returned by plain computations don't bubble up to sequence level, are computation internal only.
        /// </summary>
        public virtual bool ReturnsValue { get { return false; } }
    }

    /// <summary>
    /// A sequence computation on a container object (resulting from a variable or a method call; yielding a container object again)
    /// </summary>
    public abstract class SequenceComputationContainer : SequenceComputation
    {
        public SequenceVariable Container;
        public SequenceExpressionAttributeAccess Attribute;

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

        public string ContainerType(SequenceCheckingEnvironment env)
        {
            if(Container != null) return Container.Type;
            else return Attribute.Type(env);
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

        public object ContainerValue(IGraphProcessingEnvironment procEnv, out IGraphElement elem, out AttributeType attrType)
        {
            if(Container != null)
            {
                elem = null;
                attrType = null;
                return Container.GetVariableValue(procEnv);
            }
            else
            {
                return Attribute.ExecuteNoImplicitContainerCopy(procEnv, out elem, out attrType);
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(Container != null)
                return Container.Type;
            else 
                return Attribute.Type(env);
        }

        public override int Precedence { get { return 8; } }
        public string Name { get { if(Container != null) return Container.Name; else return Attribute.Symbol; } }
    }


    public class SequenceComputationThen : SequenceComputation
    {
        public SequenceComputation left;
        public SequenceComputation right;

        public SequenceComputationThen(SequenceComputation left, SequenceComputation right)
            : base(SequenceComputationType.Then)
        {
            this.left = left;
            this.right = right;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationThen copy = (SequenceComputationThen)MemberwiseClone();
            copy.left = left.Copy(originalToCopy, procEnv);
            copy.right = right.Copy(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return left; yield return right; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return left.Symbol + "; " + (right is SequenceExpression ? "{" + right.Symbol + "}" : right.Symbol); } }
    }


    public class SequenceComputationVAlloc : SequenceComputation
    {
        public SequenceComputationVAlloc()
            : base(SequenceComputationType.VAlloc)
        {
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationVAlloc copy = (SequenceComputationVAlloc)MemberwiseClone();
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            return procEnv.Graph.AllocateVisitedFlag();
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "valloc()"; } }
    }

    public class SequenceComputationVFree : SequenceComputation
    {
        public SequenceExpression VisitedFlagExpression;
        public bool Reset;

        public SequenceComputationVFree(SequenceExpression visitedFlagExpr, bool reset)
            : base(reset ? SequenceComputationType.VFree : SequenceComputationType.VFreeNonReset)
        {
            VisitedFlagExpression = visitedFlagExpr;
            Reset = reset;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
            {
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationVFree copy = (SequenceComputationVFree)MemberwiseClone();
            copy.VisitedFlagExpression = VisitedFlagExpression.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return VisitedFlagExpression; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return (Reset ? "vfree" : "vfreenonreset") + "(" + VisitedFlagExpression.Symbol + ")"; } }
    }

    public class SequenceComputationVReset : SequenceComputation
    {
        public SequenceExpression VisitedFlagExpression;

        public SequenceComputationVReset(SequenceExpression visitedFlagExpr)
            : base(SequenceComputationType.VReset)
        {
            VisitedFlagExpression = visitedFlagExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
            {
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationVReset copy = (SequenceComputationVReset)MemberwiseClone();
            copy.VisitedFlagExpression = VisitedFlagExpression.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return VisitedFlagExpression; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "vreset(" + VisitedFlagExpression.Symbol + ")"; } }
    }

    public class SequenceComputationContainerAdd : SequenceComputationContainer
    {
        public SequenceExpression Expr;
        public SequenceExpression ExprDst;

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

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(ExprDst != null && TypesHelper.ExtractDst(ContainerType) == "SetValueType")
            {
                throw new SequenceParserException(Symbol, "map or array or deque", ContainerType);
            }
            if(ContainerType.StartsWith("array<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                }
                if(ExprDst != null && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
                }
            }
            else if(ContainerType.StartsWith("deque<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                }
                if(ExprDst != null && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                }
                if(TypesHelper.ExtractDst(ContainerType) != "SetValueType"
                    && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), TypesHelper.ExtractDst(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(ContainerType), ExprDst.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationContainerAdd copy = (SequenceComputationContainerAdd)MemberwiseClone();
            if(Container != null) copy.Container = Container.Copy(originalToCopy, procEnv);
            if(Attribute != null) copy.Attribute = (SequenceExpressionAttributeAccess)Attribute.Copy(originalToCopy, procEnv);
            copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            if(ExprDst != null) copy.ExprDst = ExprDst.CopyExpression(originalToCopy, procEnv);
            return copy; 
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
            {
                if(elem != null)
                {
                    if(elem is INode)
                        procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.PutElement, firstValue, optionalSecondValue);
                    else
                        procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.PutElement, firstValue, optionalSecondValue);
                }
                IList array = (IList)container;
                if(ExprDst == null)
                    array.Add(firstValue);
                else
                    array.Insert((int)optionalSecondValue, firstValue);
                return array;
            }
            else if(container is IDeque)
            {
                if(elem != null)
                {
                    if(elem is INode)
                        procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.PutElement, firstValue, optionalSecondValue);
                    else
                        procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.PutElement, firstValue, optionalSecondValue);
                }
                IDeque deque = (IDeque)container;
                if(ExprDst == null)
                    deque.Enqueue(firstValue);
                else
                    deque.EnqueueAt((int)optionalSecondValue, firstValue);
                return deque;
            }
            else
            {
                if(elem != null)
                {
                    if(ExprDst != null) // must be map
                    {
                        if(elem is INode)
                            procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.PutElement, optionalSecondValue, firstValue);
                        else
                            procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.PutElement, optionalSecondValue, firstValue);
                    }
                    else
                    {
                        if(elem is INode)
                            procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.PutElement, firstValue, null);
                        else
                            procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.PutElement, firstValue, null);
                    }
                }
                IDictionary setmap = (IDictionary)container;
                setmap[firstValue] = optionalSecondValue;
                return setmap;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables, containerConstructors);
            Expr.GetLocalVariables(variables, containerConstructors);
            if(ExprDst != null) ExprDst.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Expr; if(ExprDst != null) yield return ExprDst; } }
        public override string Symbol { get { return Name + ".add(" + Expr.Symbol + (ExprDst != null ? "," + ExprDst.Symbol : "") + ")"; } }
    }

    public class SequenceComputationContainerRem : SequenceComputationContainer
    {
        public SequenceExpression Expr;

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

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible

            if(ContainerType.StartsWith("array<"))
            {
                if(Expr != null && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", Expr.Type(env));
                }
            }
            else if(ContainerType.StartsWith("deque<"))
            {
                if(Expr != null && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", Expr.Type(env));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), Expr.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationContainerRem copy = (SequenceComputationContainerRem)MemberwiseClone();
            if(Container != null) copy.Container = Container.Copy(originalToCopy, procEnv);
            if(Attribute != null) copy.Attribute = (SequenceExpressionAttributeAccess)Attribute.Copy(originalToCopy, procEnv);
            if(Expr != null) copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            return copy;
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
            {
                if(elem != null)
                {
                    if(elem is INode)
                        procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
                    else
                        procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
                }
                IList array = (IList)container;
                if(Expr == null)
                    array.RemoveAt(array.Count - 1);
                else
                    array.RemoveAt((int)valueOrKeyOrIndexToRemove);
                return array;
            }
            else if(container is IDeque)
            {
                if(elem != null)
                {
                    if(elem is INode)
                        procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
                    else
                        procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
                }
                IDeque deque = (IDeque)container;
                if(Expr == null)
                    deque.Dequeue();
                else
                    deque.DequeueAt((int)valueOrKeyOrIndexToRemove);
                return deque;
            }
            else
            {
                if(elem != null)
                {
                    if(TypesHelper.ExtractDst(TypesHelper.AttributeTypeToXgrsType(attrType)) == "SetValueType")
                    {
                        if(elem is INode)
                            procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, valueOrKeyOrIndexToRemove, null);
                        else
                            procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, valueOrKeyOrIndexToRemove, null);
                    }
                    else
                    {
                        if(elem is INode)
                            procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
                        else
                            procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, valueOrKeyOrIndexToRemove);
                    }
                }
                IDictionary setmap = (IDictionary)container;
                setmap.Remove(valueOrKeyOrIndexToRemove);
                return setmap;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables, containerConstructors);
            if(Expr != null) Expr.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { if(Expr != null) yield return Expr; yield break; } }
        public override string Symbol { get { return Name + ".rem(" + (Expr != null ? Expr.Symbol : "") + ")"; } }
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            string ContainerType = CheckAndReturnContainerType(env);
            if(ContainerType == "")
                return; // we can't check further types if the container is untyped, only runtime-check possible
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationContainerClear copy = (SequenceComputationContainerClear)MemberwiseClone();
            if(Container != null) copy.Container = Container.Copy(originalToCopy, procEnv);
            if(Attribute != null) copy.Attribute = (SequenceExpressionAttributeAccess)Attribute.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem;
            AttributeType attrType;
            object container = ContainerValue(procEnv, out elem, out attrType);
            if(container is IList)
            {
                IList array = (IList)container;
                if(elem != null)
                {
				    for(int i = array.Count; i >= 0; --i)
                    {
                        if(elem is INode)
                            procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, i);
                        else
                            procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, i);
                    }
                }
                array.Clear();
                return array;
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                if(elem != null)
                {
                    for(int i = deque.Count; i >= 0; --i)
                    {
                        if(elem is INode)
                            procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, i);
                        else
                            procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, i);
                    }
                }
                deque.Clear();
                return deque;
            }
            else
            {
                IDictionary setmap = (IDictionary)container;
                if(elem != null)
                {
                    if(TypesHelper.ExtractDst(TypesHelper.AttributeTypeToXgrsType(attrType)) == "SetValueType")
                    {
				        foreach(DictionaryEntry kvp in setmap)
                        {
                            if(elem is INode)
                                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, kvp.Key, null);
                            else
                                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, kvp.Key, null);
                        }
                    }
                    else
                    {
                        foreach(DictionaryEntry kvp in setmap)
                        {
                            if(elem is INode)
                                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, AttributeChangeType.RemoveElement, null, kvp.Key);
                            else
                                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, AttributeChangeType.RemoveElement, null, kvp.Key);
                        }
                    }
                }
                setmap.Clear();
                return setmap;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override string Symbol { get { return Name + ".clear()"; } }
    }

    public class SequenceComputationAssignment : SequenceComputation
    {
        public AssignmentTarget Target;
        public SequenceComputation SourceValueProvider;

        public SequenceComputationAssignment(AssignmentTarget tgt, SequenceComputation srcValueProvider)
            : base(SequenceComputationType.Assignment)
        {
            Target = tgt;
            SourceValueProvider = srcValueProvider;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            // the assignment of an untyped variable to a typed variable is ok, cause we want access to persistency
            // which is only offered by the untyped variables; it is checked at runtime / causes an invalid cast exception
            if(!TypesHelper.IsSameOrSubtype(SourceValueProvider.Type(env), Target.Type(env), env.Model))
            {
                throw new SequenceParserException(Symbol, Target.Type(env), SourceValueProvider.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationAssignment copy = (SequenceComputationAssignment)MemberwiseClone();
            copy.Target = Target.CopyTarget(originalToCopy, procEnv);
            copy.SourceValueProvider = SourceValueProvider.Copy(originalToCopy, procEnv);
            return copy;
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

        public override string Symbol { get { return Target.Symbol + "=" + SourceValueProvider.Symbol; } }
        public override IEnumerable<SequenceComputation> Children { get { yield return Target; yield return SourceValueProvider; } }
        public override int Precedence { get { return 8; } } // always a top prio assignment factor
    }

    public class SequenceComputationVariableDeclaration : SequenceComputation
    {
        public SequenceVariable Target;

        public SequenceComputationVariableDeclaration(SequenceVariable tgt)
            : base(SequenceComputationType.VariableDeclaration)
        {
            Target = tgt;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationVariableDeclaration copy = (SequenceComputationVariableDeclaration)MemberwiseClone();
            copy.Target = Target.Copy(originalToCopy, procEnv);
            return copy;
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

        public override string Symbol { get { return Target.Name; } }
        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } } // always a top prio assignment factor
    }

    public class SequenceComputationDebugAdd : SequenceComputation
    {
        public List<SequenceExpression> ArgExprs;

        public SequenceComputationDebugAdd(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugAdd)
        {
            ArgExprs = argExprs;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(ArgExprs.Count == 0)
                throw new Exception("Debug::add expects at least one parameter (the message at computation entry)");

            if(!TypesHelper.IsSameOrSubtype(ArgExprs[0].Type(env), "string", env.Model))
                throw new SequenceParserException("The 0 parameter of " + Symbol, "string type", ArgExprs[0].Type(env));

            base.Check(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationDebugAdd copy = (SequenceComputationDebugAdd)MemberwiseClone();
            copy.ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in ArgExprs)
                copy.ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object[] values = new object[ArgExprs.Count-1];
            for(int i = 1; i < ArgExprs.Count; ++i)
            {
                values[i-1] = ArgExprs[i].Evaluate(procEnv);
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
        public override IEnumerable<SequenceComputation> Children { get { for(int i = 0; i < ArgExprs.Count; ++i) yield return ArgExprs[i]; } }
        public override int Precedence { get { return 8; } }
    }

    public class SequenceComputationDebugRem : SequenceComputation
    {
        public List<SequenceExpression> ArgExprs;

        public SequenceComputationDebugRem(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugRem)
        {
            ArgExprs = argExprs;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(ArgExprs.Count == 0)
                throw new Exception("Debug::rem expects at least one parameter (the message at computation exit, must be the same as the message at computation entry)");

            if(!TypesHelper.IsSameOrSubtype(ArgExprs[0].Type(env), "string", env.Model))
                throw new SequenceParserException("The 0 parameter of " + Symbol, "string type", ArgExprs[0].Type(env));

            base.Check(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationDebugRem copy = (SequenceComputationDebugRem)MemberwiseClone();
            copy.ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in ArgExprs)
                copy.ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object[] values = new object[ArgExprs.Count - 1];
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
        public override IEnumerable<SequenceComputation> Children { get { for(int i = 0; i < ArgExprs.Count; ++i) yield return ArgExprs[i]; } }
        public override int Precedence { get { return 8; } }
    }

    public class SequenceComputationDebugEmit : SequenceComputation
    {
        public List<SequenceExpression> ArgExprs;

        public SequenceComputationDebugEmit(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugEmit)
        {
            ArgExprs = argExprs;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(ArgExprs.Count == 0)
                throw new Exception("Debug::emit expects at least one parameter (the message)");

            if(!TypesHelper.IsSameOrSubtype(ArgExprs[0].Type(env), "string", env.Model))
                throw new SequenceParserException("The 0 parameter of " + Symbol, "string type", ArgExprs[0].Type(env));

            base.Check(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationDebugEmit copy = (SequenceComputationDebugEmit)MemberwiseClone();
            copy.ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in ArgExprs)
                copy.ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object[] values = new object[ArgExprs.Count - 1];
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
        public override IEnumerable<SequenceComputation> Children { get { for(int i = 0; i < ArgExprs.Count; ++i) yield return ArgExprs[i]; } }
        public override int Precedence { get { return 8; } }
    }

    public class SequenceComputationDebugHalt : SequenceComputation
    {
        public List<SequenceExpression> ArgExprs;

        public SequenceComputationDebugHalt(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugHalt)
        {
            ArgExprs = argExprs;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(ArgExprs.Count == 0)
                throw new Exception("Debug::halt expects at least one parameter (the message)");

            if(!TypesHelper.IsSameOrSubtype(ArgExprs[0].Type(env), "string", env.Model))
                throw new SequenceParserException("The 0 parameter of " + Symbol, "string type", ArgExprs[0].Type(env));

            base.Check(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationDebugHalt copy = (SequenceComputationDebugHalt)MemberwiseClone();
            copy.ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in ArgExprs)
                copy.ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object[] values = new object[ArgExprs.Count - 1];
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
        public override IEnumerable<SequenceComputation> Children { get { for(int i = 0; i < ArgExprs.Count; ++i) yield return ArgExprs[i]; } }
        public override int Precedence { get { return 8; } }
    }

    public class SequenceComputationDebugHighlight : SequenceComputation
    {
        public List<SequenceExpression> ArgExprs;

        public SequenceComputationDebugHighlight(List<SequenceExpression> argExprs)
            : base(SequenceComputationType.DebugHighlight)
        {
            ArgExprs = argExprs;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(ArgExprs.Count == 0 || ArgExprs.Count % 2 == 0)
                throw new Exception("Debug::highlight expects an odd number of parameters (message, then series of alternating value to highlight followed by annotation to be displayed)");

            for(int i = 0; i < ArgExprs.Count; ++i)
            {
                if(i % 2 == 0 && !TypesHelper.IsSameOrSubtype(ArgExprs[i].Type(env), "string", env.Model))
                    throw new SequenceParserException("The " + i + " parameter of " + Symbol, "string type", ArgExprs[i].Type(env));
            }

            base.Check(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationDebugHighlight copy = (SequenceComputationDebugHighlight)MemberwiseClone();
            copy.ArgExprs = new List<SequenceExpression>();
            foreach(SequenceExpression seqExpr in ArgExprs)
                copy.ArgExprs.Add(seqExpr.CopyExpression(originalToCopy, procEnv));
            return copy;
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
        public override IEnumerable<SequenceComputation> Children { get { for(int i = 0; i < ArgExprs.Count; ++i) yield return ArgExprs[i]; } }
        public override int Precedence { get { return 8; } }
    }

    public class SequenceComputationEmit : SequenceComputation
    {
        public List<SequenceExpression> Expressions;

        public SequenceComputationEmit(List<SequenceExpression> exprs)
            : base(SequenceComputationType.Emit)
        {
            Expressions = exprs;
            for(int i = 0; i < Expressions.Count; ++i)
            {
                SequenceExpression expr = Expressions[i];
                if(expr is SequenceExpressionConstant)
                {
                    SequenceExpressionConstant constant = (SequenceExpressionConstant)Expressions[i];
                    if(constant.Constant is string)
                    {
                        constant.Constant = ((string)constant.Constant).Replace("\\n", "\n");
                        constant.Constant = ((string)constant.Constant).Replace("\\r", "\r");
                        constant.Constant = ((string)constant.Constant).Replace("\\t", "\t");
                        constant.Constant = ((string)constant.Constant).Replace("\\#", "#");
                    }
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationEmit copy = (SequenceComputationEmit)MemberwiseClone();
            copy.Expressions = new List<SequenceExpression>(Expressions.Count);
            for(int i = 0; i < Expressions.Count; ++i)
            {
                copy.Expressions[i] = Expressions[i].CopyExpression(originalToCopy, procEnv);
            }
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object value = null;
            for(int i = 0; i < Expressions.Count; ++i)
            {
                value = Expressions[i].Evaluate(procEnv);
                if(value != null)
                    procEnv.EmitWriter.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph));
            }
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            for(int i = 0; i < Expressions.Count; ++i)
                Expressions[i].GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { for(int i = 0; i < Expressions.Count; ++i) yield return Expressions[i]; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("emit(");
                for(int i = 0; i < Expressions.Count; ++i)
                {
                    if(i != 0)
                        sb.Append(", ");
                    String oldString = null;
                    if(Expressions[i] is SequenceExpressionConstant)
                    {
                        SequenceExpressionConstant constant = (SequenceExpressionConstant)Expressions[i];
                        if(constant.Constant is string)
                        {
                            oldString = (string)constant.Constant;
                            constant.Constant = ((string)constant.Constant).Replace("\n", "\\n");
                            constant.Constant = ((string)constant.Constant).Replace("\r", "\\r");
                            constant.Constant = ((string)constant.Constant).Replace("\t", "\\t");
                            constant.Constant = ((string)constant.Constant).Replace("#", "\\#");
                        }
                    }

                    if(oldString != null)
                        sb.Append("\"");
                    sb.Append(Expressions[i].Symbol);
                    if(oldString != null)
                        sb.Append("\"");

                    if(oldString != null)
                        ((SequenceExpressionConstant)Expressions[i]).Constant = oldString;
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }

    public class SequenceComputationRecord : SequenceComputation
    {
        public SequenceExpression Expression;

        public SequenceComputationRecord(SequenceExpression expr)
            : base(SequenceComputationType.Record)
        {
            Expression = expr;
            if(Expression is SequenceExpressionConstant)
            {
                SequenceExpressionConstant constant = (SequenceExpressionConstant)Expression;
                if(constant.Constant is string)
                {
                    constant.Constant = ((string)constant.Constant).Replace("\\n", "\n");
                    constant.Constant = ((string)constant.Constant).Replace("\\r", "\r");
                    constant.Constant = ((string)constant.Constant).Replace("\\t", "\t");
                    constant.Constant = ((string)constant.Constant).Replace("\\#", "#");
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationRecord copy = (SequenceComputationRecord)MemberwiseClone();
            copy.Expression = Expression.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object value = Expression.Evaluate(procEnv);
            if(value != null)
                procEnv.Recorder.Write(EmitHelper.ToStringNonNull(value, procEnv.Graph));
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Expression.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Expression; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol 
        { 
            get 
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("record(");
                
                String oldString = null;
                if(Expression is SequenceExpressionConstant)
                {
                    SequenceExpressionConstant constant = (SequenceExpressionConstant)Expression;
                    if(constant.Constant is string)
                    {
                        oldString = (string)constant.Constant;
                        constant.Constant = ((string)constant.Constant).Replace("\n", "\\n");
                        constant.Constant = ((string)constant.Constant).Replace("\r", "\\r");
                        constant.Constant = ((string)constant.Constant).Replace("\t", "\\t");
                        constant.Constant = ((string)constant.Constant).Replace("#", "\\#");
                    }
                }

                if(oldString != null)
                    sb.Append("\"");
                sb.Append(Expression.Symbol);
                if(oldString != null)
                    sb.Append("\"");

                if(oldString != null)
                    ((SequenceExpressionConstant)Expression).Constant = oldString;

                sb.Append(")");
                return sb.ToString();
            } 
        }
    }

    public class SequenceComputationExport : SequenceComputation
    {
        public SequenceExpression Name;
        public SequenceExpression Graph;

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

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationExport copy = (SequenceComputationExport)MemberwiseClone();
            copy.Name = Name.CopyExpression(originalToCopy, procEnv);
            if(Graph!=null)
                copy.Graph = Graph.CopyExpression(originalToCopy, procEnv);
            return copy;
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
            if(Graph!=null)
                Graph.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Name; if(Graph != null) yield return Graph; else yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "File::export(" + (Graph!=null ? Graph.Symbol + ", " : "") + Name.Symbol + ")"; } }
    }

    public class SequenceComputationDeleteFile : SequenceComputation
    {
        public SequenceExpression Name;

        public SequenceComputationDeleteFile(SequenceExpression expr1)
            : base(SequenceComputationType.DeleteFile)
        {
            Name = expr1;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationDeleteFile copy = (SequenceComputationDeleteFile)MemberwiseClone();
            copy.Name = Name.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return Name; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "File::deleteFile(" + Name.Symbol + ")"; } }
    }


    public class SequenceComputationGraphAdd : SequenceComputation
    {
        public SequenceExpression Expr;
        public SequenceExpression ExprSrc;
        public SequenceExpression ExprDst;

        public SequenceComputationGraphAdd(SequenceExpression expr, SequenceExpression exprSrc, SequenceExpression exprDst)
            : base(SequenceComputationType.GraphAdd)
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

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphAdd copy = (SequenceComputationGraphAdd)MemberwiseClone();
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            Expr.GetLocalVariables(variables, containerConstructors);
            if(ExprSrc != null) ExprSrc.GetLocalVariables(variables, containerConstructors);
            if(ExprDst != null) ExprDst.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Expr; if(ExprSrc != null) yield return ExprSrc; if(ExprDst != null) yield return ExprDst; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "add(" + Expr.Symbol + (ExprSrc != null ? "," + ExprSrc.Symbol + "," + ExprDst.Symbol : "") + ")"; } }
    }

    public class SequenceComputationGraphRem : SequenceComputation
    {
        public SequenceExpression Expr;

        public SequenceComputationGraphRem(SequenceExpression expr)
            : base(SequenceComputationType.GraphRem)
        {
            Expr = expr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children
            
            if(Expr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "Node", env.Model)
                    && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "Edge", env.Model))
                {
                    throw new SequenceParserException(Symbol, "node or edge", Expr.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphRem copy = (SequenceComputationGraphRem)MemberwiseClone();
            copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object delCandidate = Expr.Evaluate(procEnv);
            if(delCandidate is IEdge)
            {
                procEnv.Graph.Remove((IEdge)delCandidate);
            }
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

        public override IEnumerable<SequenceComputation> Children { get { yield return Expr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "rem(" + Expr.Symbol + ")"; } }
    }

    public class SequenceComputationGraphClear : SequenceComputation
    {
        public SequenceComputationGraphClear()
            : base(SequenceComputationType.GraphClear)
        {
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphClear copy = (SequenceComputationGraphClear)MemberwiseClone();
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "clear()"; } }
    }

    public class SequenceComputationGraphRetype : SequenceComputation
    {
        public SequenceExpression TypeExpr;
        public SequenceExpression ElemExpr;

        public SequenceComputationGraphRetype(SequenceExpression elemExpr, SequenceExpression typeExpr)
            : base(SequenceComputationType.GraphRetype)
        {
            ElemExpr = elemExpr;
            TypeExpr = typeExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(ElemExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(ElemExpr.Type(env), "Node", env.Model)
                    && !TypesHelper.IsSameOrSubtype(ElemExpr.Type(env), "Edge", env.Model))
                {
                    throw new SequenceParserException(Symbol + ", first argument", "node or edge", ElemExpr.Type(env));
                }
            }

            string typeString = null;
            if(TypeExpr.Type(env) == "string")
            {
                if(TypeExpr is SequenceExpressionConstant)
                    typeString = (string)((SequenceExpressionConstant)TypeExpr).Constant;
            }
            else
            {
                typeString = TypeExpr.Type(env);
            }
            NodeType nodeType = TypesHelper.GetNodeType(typeString, env.Model);
            if(nodeType == null && typeString != null)
            {
                EdgeType edgeType = TypesHelper.GetEdgeType(typeString, env.Model);
                if(edgeType == null && typeString != null)
                {
                    throw new SequenceParserException(Symbol + ", second argument", "node or edge type or string denoting node or edge type", typeString);
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            string typeString = "";

            if(TypeExpr.Type(env) != "")
            {
                if(TypeExpr.Type(env) == "string")
                {
                    if(TypeExpr is SequenceExpressionConstant)
                        typeString = (string)((SequenceExpressionConstant)TypeExpr).Constant;
                }
                else
                {
                    typeString = TypeExpr.Type(env);
                }
            }

            return typeString;
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphRetype copy = (SequenceComputationGraphRetype)MemberwiseClone();
            copy.ElemExpr = ElemExpr.CopyExpression(originalToCopy, procEnv);
            copy.TypeExpr = TypeExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return ElemExpr; yield return TypeExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "retype(" + ElemExpr.Symbol + "," + TypeExpr.Symbol + ")"; } }
    }

    public class SequenceComputationGraphAddCopy : SequenceComputation
    {
        public SequenceExpression Expr;
        public SequenceExpression ExprSrc;
        public SequenceExpression ExprDst;

        public SequenceComputationGraphAddCopy(SequenceExpression expr, SequenceExpression exprSrc, SequenceExpression exprDst)
            : base(SequenceComputationType.GraphAddCopy)
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
                    if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "Edge", env.Model))
                    {
                        throw new SequenceParserException(Symbol + "first argument", "edge", Expr.Type(env));
                    }
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), "Node", env.Model))
                    {
                        throw new SequenceParserException(Symbol + "first argument", "node", Expr.Type(env));
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
            return Expr.Type(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphAddCopy copy = (SequenceComputationGraphAddCopy)MemberwiseClone();
            copy.Expr = Expr.CopyExpression(originalToCopy, procEnv);
            if(ExprSrc != null) copy.ExprSrc = ExprSrc.CopyExpression(originalToCopy, procEnv);
            if(ExprDst != null) copy.ExprDst = ExprDst.CopyExpression(originalToCopy, procEnv);
            return copy;
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
            if(ExprSrc != null) ExprSrc.GetLocalVariables(variables, containerConstructors);
            if(ExprDst != null) ExprDst.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Expr; if(ExprSrc != null) yield return ExprSrc; if(ExprDst != null) yield return ExprDst; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "addCopy(" + Expr.Symbol + (ExprSrc != null ? "," + ExprSrc.Symbol + "," + ExprDst.Symbol : "") + ")"; } }
    }

    public class SequenceComputationGraphMerge : SequenceComputation
    {
        public SequenceExpression TargetNodeExpr;
        public SequenceExpression SourceNodeExpr;

        public SequenceComputationGraphMerge(SequenceExpression targetNodeExpr, SequenceExpression sourceNodeExpr)
            : base(SequenceComputationType.GraphMerge)
        {
            TargetNodeExpr = targetNodeExpr;
            SourceNodeExpr = sourceNodeExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                {
                    throw new SequenceParserException(Symbol, "node", TargetNodeExpr.Type(env));
                }
            }

            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                {
                    throw new SequenceParserException(Symbol, "node", SourceNodeExpr.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphMerge copy = (SequenceComputationGraphMerge)MemberwiseClone();
            copy.TargetNodeExpr = TargetNodeExpr.CopyExpression(originalToCopy, procEnv);
            copy.SourceNodeExpr = SourceNodeExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return TargetNodeExpr; yield return SourceNodeExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "merge(" + TargetNodeExpr.Symbol + ", " + SourceNodeExpr.Symbol + ")"; } }
    }

    public class SequenceComputationGraphRedirectSource : SequenceComputation
    {
        public SequenceExpression EdgeExpr;
        public SequenceExpression SourceNodeExpr;

        public SequenceComputationGraphRedirectSource(SequenceExpression edgeExpr, SequenceExpression sourceNodeExpr)
            : base(SequenceComputationType.GraphRedirectSource)
        {
            EdgeExpr = edgeExpr;
            SourceNodeExpr = sourceNodeExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(EdgeExpr.Type(env), "Edge", env.Model))
                {
                    throw new SequenceParserException(Symbol + "first argument", "edge", EdgeExpr.Type(env));
                }
            }
            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                {
                    throw new SequenceParserException(Symbol + "second argument", "node", SourceNodeExpr.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphRedirectSource copy = (SequenceComputationGraphRedirectSource)MemberwiseClone();
            copy.EdgeExpr = EdgeExpr.CopyExpression(originalToCopy, procEnv);
            copy.SourceNodeExpr = SourceNodeExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return EdgeExpr; yield return SourceNodeExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "redirectSource(" + EdgeExpr.Symbol + "," + SourceNodeExpr.Symbol + ")"; } }
    }

    public class SequenceComputationGraphRedirectTarget : SequenceComputation
    {
        public SequenceExpression EdgeExpr;
        public SequenceExpression TargetNodeExpr;

        public SequenceComputationGraphRedirectTarget(SequenceExpression edgeExpr, SequenceExpression targetNodeExpr)
            : base(SequenceComputationType.GraphRedirectTarget)
        {
            EdgeExpr = edgeExpr;
            TargetNodeExpr = targetNodeExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(EdgeExpr.Type(env), "Edge", env.Model))
                {
                    throw new SequenceParserException(Symbol + "first argument", "edge", EdgeExpr.Type(env));
                }
            }
            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                {
                    throw new SequenceParserException(Symbol + "second argument", "node", TargetNodeExpr.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphRedirectTarget copy = (SequenceComputationGraphRedirectTarget)MemberwiseClone();
            copy.EdgeExpr = EdgeExpr.CopyExpression(originalToCopy, procEnv);
            copy.TargetNodeExpr = TargetNodeExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return EdgeExpr; yield return TargetNodeExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "redirectSourceAndTarget(" + EdgeExpr.Symbol + "," + TargetNodeExpr.Symbol + ")"; } }
    }
    
    public class SequenceComputationGraphRedirectSourceAndTarget : SequenceComputation
    {
        public SequenceExpression EdgeExpr;
        public SequenceExpression SourceNodeExpr;
        public SequenceExpression TargetNodeExpr;

        public SequenceComputationGraphRedirectSourceAndTarget(SequenceExpression edgeExpr, SequenceExpression sourceNodeExpr, SequenceExpression targetNodeExpr)
            : base(SequenceComputationType.GraphRedirectSourceAndTarget)
        {
            EdgeExpr = edgeExpr;
            SourceNodeExpr = sourceNodeExpr;
            TargetNodeExpr = targetNodeExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(EdgeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(EdgeExpr.Type(env), "Edge", env.Model))
                {
                    throw new SequenceParserException(Symbol + "first argument", "edge", EdgeExpr.Type(env));
                }
            }
            if(SourceNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(SourceNodeExpr.Type(env), "Node", env.Model))
                {
                    throw new SequenceParserException(Symbol + "second argument", "node", SourceNodeExpr.Type(env));
                }
            }
            if(TargetNodeExpr.Type(env) != "")
            {
                if(!TypesHelper.IsSameOrSubtype(TargetNodeExpr.Type(env), "Node", env.Model))
                {
                    throw new SequenceParserException(Symbol + "third argument", "node", TargetNodeExpr.Type(env));
                }
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "void";
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationGraphRedirectSourceAndTarget copy = (SequenceComputationGraphRedirectSourceAndTarget)MemberwiseClone();
            copy.EdgeExpr = EdgeExpr.CopyExpression(originalToCopy, procEnv);
            copy.SourceNodeExpr = SourceNodeExpr.CopyExpression(originalToCopy, procEnv);
            copy.TargetNodeExpr = TargetNodeExpr.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return EdgeExpr; yield return SourceNodeExpr; yield return TargetNodeExpr; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "redirectSourceAndTarget(" + EdgeExpr.Symbol + "," + SourceNodeExpr.Symbol + "," + TargetNodeExpr.Symbol + ")"; } }
    }


    public class SequenceComputationInsert : SequenceComputation
    {
        public SequenceExpression Graph;

        public SequenceComputationInsert(SequenceExpression graph)
            : base(SequenceComputationType.Insert)
        {
            Graph = graph;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Graph.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!TypesHelper.IsSameOrSubtype(Graph.Type(env), "graph", env.Model))
                {
                    throw new SequenceParserException(Symbol, "graph", Graph.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationInsert copy = (SequenceComputationInsert)MemberwiseClone();
            copy.Graph = Graph.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return Graph; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "insert(" + Graph.Symbol + ")"; } }
    }

    public class SequenceComputationInsertCopy : SequenceComputation
    {
        public SequenceExpression Graph;
        public SequenceExpression RootNode;

        public SequenceComputationInsertCopy(SequenceExpression graph, SequenceExpression rootNode)
            : base(SequenceComputationType.InsertCopy)
        {
            Graph = graph;
            RootNode = rootNode;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env); // check children

            if(Graph.Type(env) != "") // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
            {
                if(!TypesHelper.IsSameOrSubtype(Graph.Type(env), "graph", env.Model))
                {
                    throw new SequenceParserException(Symbol, "graph", Graph.Type(env));
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

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationInsertCopy copy = (SequenceComputationInsertCopy)MemberwiseClone();
            copy.Graph = Graph.CopyExpression(originalToCopy, procEnv);
            copy.RootNode = RootNode.CopyExpression(originalToCopy, procEnv);
            return copy;
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

        public override IEnumerable<SequenceComputation> Children { get { yield return Graph; yield return RootNode; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "insert(" + Graph.Symbol + "," + RootNode.Symbol + ")"; } }
    }

    public class SequenceComputationInsertInduced : SequenceComputation
    {
        public SequenceExpression NodeSet;
        public SequenceExpression RootNode;

        public SequenceComputationInsertInduced(SequenceExpression nodeSet, SequenceExpression rootNode)
            : base(SequenceComputationType.InsertInduced)
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

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationInsertInduced copy = (SequenceComputationInsertInduced)MemberwiseClone();
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            NodeSet.GetLocalVariables(variables, containerConstructors);
            RootNode.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return NodeSet; yield return RootNode; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "insertInduced(" + NodeSet.Symbol + "," + RootNode.Symbol + ")"; } }
    }

    public class SequenceComputationInsertDefined : SequenceComputation
    {
        public SequenceExpression EdgeSet;
        public SequenceExpression RootEdge;

        public SequenceComputationInsertDefined(SequenceExpression edgeSet, SequenceExpression rootEdge)
            : base(SequenceComputationType.InsertDefined)
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

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationInsertDefined copy = (SequenceComputationInsertDefined)MemberwiseClone();
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            EdgeSet.GetLocalVariables(variables, containerConstructors);
            RootEdge.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return EdgeSet; yield return RootEdge; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "insertDefined(" + EdgeSet.Symbol + "," + RootEdge.Symbol + ")"; } }
    }


    public class SequenceComputationBuiltinProcedureCall : SequenceComputation
    {
        public SequenceComputation BuiltinProcedure;
        public List<SequenceVariable> ReturnVars;

        public SequenceComputationBuiltinProcedureCall(SequenceComputation builtinProcedure, List<SequenceVariable> returnVars)
            : base(SequenceComputationType.BuiltinProcedureCall)
        {
            BuiltinProcedure = builtinProcedure;
            ReturnVars = returnVars;
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
                {
                    throw new SequenceParserException(Symbol, ReturnVars[0].Type, BuiltinProcedure.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationBuiltinProcedureCall copy = (SequenceComputationBuiltinProcedureCall)MemberwiseClone();
            copy.BuiltinProcedure = BuiltinProcedure.Copy(originalToCopy, procEnv);
            copy.ReturnVars = new List<SequenceVariable>();
            for(int i = 0; i < ReturnVars.Count; ++i)
                copy.ReturnVars.Add(ReturnVars[i].Copy(originalToCopy, procEnv));
            return copy;
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
                ReturnVars[i].GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }

        protected String GetProcedureString()
        {
            StringBuilder sb = new StringBuilder();
            if(ReturnVars.Count > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ReturnVars.Count; ++i)
                {
                    sb.Append(ReturnVars[i].Name);
                    if(i != ReturnVars.Count - 1) sb.Append(",");
                }
                sb.Append(")=");
            }
            sb.Append(BuiltinProcedure.Symbol);
            return sb.ToString();
        }

        public override string Symbol { get { return GetProcedureString(); } }
    }
    
    public class SequenceComputationProcedureCall : SequenceComputation
    {
        public ProcedureInvocationParameterBindings ParamBindings;

        public SequenceComputationProcedureCall(ProcedureInvocationParameterBindings paramBindings)
            : base(SequenceComputationType.ProcedureCall)
        {
            ParamBindings = paramBindings;
        }

        public SequenceComputationProcedureCall(SequenceComputationType seqCompType, ProcedureInvocationParameterBindings paramBindings)
            : base(seqCompType)
        {
            ParamBindings = paramBindings;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            env.CheckProcedureCall(this);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationProcedureCall copy = (SequenceComputationProcedureCall)MemberwiseClone();
            copy.ParamBindings = ParamBindings.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IProcedureDefinition procedureDef = ParamBindings.ProcedureDef;
            for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; i++)
            {
                if(ParamBindings.ArgumentExpressions[i] != null)
                    ParamBindings.Arguments[i] = ParamBindings.ArgumentExpressions[i].Evaluate(procEnv);
            }
            object[] resultVars = procedureDef.Apply(procEnv, procEnv.Graph, ParamBindings);
            if(ParamBindings.ReturnVars.Length>0)
            {
                for(int i = 0; i < ParamBindings.ReturnVars.Length; ++i)
                    ParamBindings.ReturnVars[i].SetVariableValue(resultVars[i], procEnv);
            }
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            ParamBindings.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }

        protected virtual String GetProcedureString()
        {
            StringBuilder sb = new StringBuilder();
            if(ParamBindings.ReturnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ReturnVars.Length; ++i)
                {
                    sb.Append(ParamBindings.ReturnVars[i].Name);
                    if(i != ParamBindings.ReturnVars.Length - 1) sb.Append(",");
                }
                sb.Append(")=");
            }
            sb.Append(ParamBindings.ProcedureDef.Name);
            if(ParamBindings.ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; ++i)
                {
                    if(ParamBindings.ArgumentExpressions[i] != null)
                        sb.Append(ParamBindings.ArgumentExpressions[i].Symbol);
                    else
                        sb.Append(ParamBindings.Arguments[i] != null ? ParamBindings.Arguments[i] : "null");
                    if(i != ParamBindings.ArgumentExpressions.Length - 1) sb.Append(",");
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

        public override string Symbol { get { return GetProcedureString(); } }
    }

    public class SequenceComputationProcedureMethodCall : SequenceComputationProcedureCall
    {
        public SequenceExpression TargetExpr;
        public SequenceVariable TargetVar;

        public SequenceComputationProcedureMethodCall(SequenceExpression targetExpr, ProcedureInvocationParameterBindings paramBindings)
            : base(SequenceComputationType.ProcedureMethodCall, paramBindings)
        {
            TargetExpr = targetExpr;
        }

        public SequenceComputationProcedureMethodCall(SequenceVariable targetVar, ProcedureInvocationParameterBindings paramBindings)
            : base(SequenceComputationType.ProcedureMethodCall, paramBindings)
        {
            TargetVar = targetVar;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(TargetExpr != null)
                env.CheckProcedureMethodCall(TargetExpr, this);
            else
                env.CheckProcedureMethodCall(TargetVar, this);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceComputationProcedureMethodCall copy = (SequenceComputationProcedureMethodCall)MemberwiseClone();
            if(TargetExpr!=null) copy.TargetExpr = TargetExpr.CopyExpression(originalToCopy, procEnv);
            if(TargetVar != null) copy.TargetVar = TargetVar.Copy(originalToCopy, procEnv);
            copy.ParamBindings = ParamBindings.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            IGraphElement owner;
            if(TargetExpr != null)
                owner = (IGraphElement)TargetExpr.Evaluate(procEnv);
            else
                owner = (IGraphElement)TargetVar.GetVariableValue(procEnv);
            for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; i++)
            {
                if(ParamBindings.ArgumentExpressions[i] != null)
                    ParamBindings.Arguments[i] = ParamBindings.ArgumentExpressions[i].Evaluate(procEnv);
            }
            object[] resultVars = owner.ApplyProcedureMethod(procEnv, procEnv.Graph, ParamBindings.Name, ParamBindings.Arguments);
            if(ParamBindings.ReturnVars.Length > 0)
            {
                for(int i = 0; i < ParamBindings.ReturnVars.Length; ++i)
                    ParamBindings.ReturnVars[i].SetVariableValue(resultVars[i], procEnv);
            }
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            if(TargetExpr != null) TargetExpr.GetLocalVariables(variables, containerConstructors);
            if(TargetVar != null) TargetVar.GetLocalVariables(variables);
            ParamBindings.GetLocalVariables(variables, containerConstructors);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }

        protected override String GetProcedureString()
        {
            StringBuilder sb = new StringBuilder();
            if(ParamBindings.ReturnVars.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ReturnVars.Length; ++i)
                {
                    sb.Append(ParamBindings.ReturnVars[i].Name);
                    if(i != ParamBindings.ReturnVars.Length - 1) sb.Append(",");
                }
                sb.Append(")=");
            }
            if(TargetExpr != null) sb.Append(TargetExpr.Symbol + ".");
            if(TargetVar != null) sb.Append(TargetVar.ToString() + ".");
            sb.Append(ParamBindings.ProcedureDef.Name);
            if(ParamBindings.ArgumentExpressions.Length > 0)
            {
                sb.Append("(");
                for(int i = 0; i < ParamBindings.ArgumentExpressions.Length; ++i)
                {
                    if(ParamBindings.ArgumentExpressions[i] != null)
                        sb.Append(ParamBindings.ArgumentExpressions[i].Symbol);
                    else
                        sb.Append(ParamBindings.Arguments[i] != null ? ParamBindings.Arguments[i] : "null");
                    if(i != ParamBindings.ArgumentExpressions.Length - 1) sb.Append(",");
                }
                sb.Append(")");
            }
            return sb.ToString();
        }
    }
}
