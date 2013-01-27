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
    /// Specifies the actual subtype of a sequence computation.
    /// A new expression type -> you must add the corresponding class down below 
    /// and adapt the lgspSequenceGenerator.
    /// </summary>
    public enum SequenceComputationType
    {
        Then,
        VFree, VFreeNonReset, VReset,
        ContainerAdd, ContainerRem, ContainerClear,
        Assignment,
        VariableDeclaration,
        Emit, Record, Export, GraphRem, GraphClear,
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
        /// Collects all variables of the sequence expression tree into the variables dictionary.
        /// </summary>
        /// <param name="variables">Contains the variables found</param>
        public virtual void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
        }

        /// <summary>
        /// Enumerates all child sequence computation objects
        /// </summary>
        public abstract IEnumerable<SequenceComputation> Children { get; }

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
        public SequenceComputation MethodCall;
        public SequenceExpressionAttributeAccess Attribute;

        public SequenceComputationContainer(SequenceComputationType type, SequenceVariable container, SequenceComputation methodCall)
            : base(type)
        {
            Container = container;
            MethodCall = methodCall;
        }

        public SequenceComputationContainer(SequenceComputationType type, SequenceExpressionAttributeAccess attribute)
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

        public object ContainerValue(IGraphProcessingEnvironment procEnv, out IGraphElement elem, out AttributeType attrType)
        {
            if(Container != null)
            {
                elem = null;
                attrType = null;
                return Container.GetVariableValue(procEnv);
            }
            else if(MethodCall != null)
            {
                elem = null;
                attrType = null;
                return MethodCall.Execute(procEnv);
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
            else if(MethodCall != null)
                return MethodCall.Type(env);
            else 
                return Attribute.Type(env);
        }

        public override int Precedence { get { return 8; } }
        public string Name { get { if(Container != null) return Container.Name; else if(MethodCall != null) return MethodCall.Symbol; else return Attribute.Symbol; } }
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            left.GetLocalVariables(variables);
            right.GetLocalVariables(variables);
        }

        public override bool ReturnsValue { get { return right.ReturnsValue; } }

        public override IEnumerable<SequenceComputation> Children { get { yield return left; yield return right; } }
        public override int Precedence { get { return 7; } }
        public override string Symbol { get { return left.Symbol + "; " + right.Symbol; } }
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            VisitedFlagExpression.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            VisitedFlagExpression.GetLocalVariables(variables);
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
            : base(SequenceComputationType.ContainerAdd, container, null)
        {
            Expr = expr;
            ExprDst = exprDst;
        }

        public SequenceComputationContainerAdd(SequenceComputation methodCall, SequenceExpression expr, SequenceExpression exprDst)
            : base(SequenceComputationType.ContainerAdd, null, methodCall)
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
            if(MethodCall != null) copy.MethodCall = MethodCall.Copy(originalToCopy, procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(MethodCall != null) MethodCall.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables);
            Expr.GetLocalVariables(variables);
            if(ExprDst != null) ExprDst.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { if(MethodCall != null) yield return MethodCall; yield return Expr; if(ExprDst != null) yield return ExprDst; } }
        public override string Symbol { get { return Name + ".add(" + Expr.Symbol + (ExprDst != null ? "," + ExprDst.Symbol : "") + ")"; } }
    }

    public class SequenceComputationContainerRem : SequenceComputationContainer
    {
        public SequenceExpression Expr;

        public SequenceComputationContainerRem(SequenceVariable container, SequenceExpression expr)
            : base(SequenceComputationType.ContainerRem, container, null)
        {
            Expr = expr;
        }

        public SequenceComputationContainerRem(SequenceComputation methodCall, SequenceExpression expr)
            : base(SequenceComputationType.ContainerRem, null, methodCall)
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
            if(MethodCall != null) copy.MethodCall = MethodCall.Copy(originalToCopy, procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(MethodCall != null) MethodCall.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables);
            if(Expr != null) Expr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { if(MethodCall != null) yield return MethodCall; if(Expr != null) yield return Expr; yield break; } }
        public override string Symbol { get { return Name + ".rem(" + (Expr != null ? Expr.Symbol : "") + ")"; } }
    }

    public class SequenceComputationContainerClear : SequenceComputationContainer
    {
        public SequenceComputationContainerClear(SequenceVariable container)
            : base(SequenceComputationType.ContainerClear, container, null)
        {
        }

        public SequenceComputationContainerClear(SequenceComputation methodCall)
            : base(SequenceComputationType.ContainerClear, null, methodCall)
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
            if(MethodCall != null) copy.MethodCall = MethodCall.Copy(originalToCopy, procEnv);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Container != null) Container.GetLocalVariables(variables);
            if(MethodCall != null) MethodCall.GetLocalVariables(variables);
            if(Attribute != null) Attribute.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { if(MethodCall == null) yield break; else yield return MethodCall; } }
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Target.GetLocalVariables(variables);
            SourceValueProvider.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Target.GetLocalVariables(variables);
        }

        public override string Symbol { get { return Target.Name; } }
        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } } // always a top prio assignment factor
    }

    public class SequenceComputationEmit : SequenceComputation
    {
        public SequenceExpression Expression;

        public SequenceComputationEmit(SequenceExpression expr)
            : base(SequenceComputationType.Emit)
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
            SequenceComputationEmit copy = (SequenceComputationEmit)MemberwiseClone();
            copy.Expression = Expression.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override object Execute(IGraphProcessingEnvironment procEnv)
        {
            object value = Expression.Evaluate(procEnv);
            if(value != null)
            {
                if(value is IDictionary)
                    procEnv.EmitWriter.Write(ContainerHelper.ToString((IDictionary)value, procEnv.Graph));
                else if(value is IList)
                    procEnv.EmitWriter.Write(ContainerHelper.ToString((IList)value, procEnv.Graph));
                else if(value is IDeque)
                    procEnv.EmitWriter.Write(ContainerHelper.ToString((IDeque)value, procEnv.Graph));
                else
                    procEnv.EmitWriter.Write(ContainerHelper.ToString(value, procEnv.Graph));
            }
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Expression.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Expression; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "emit(" + Expression.Symbol + ")"; } }
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
            {
                if(value is IDictionary)
                    procEnv.Recorder.Write(ContainerHelper.ToString((IDictionary)value, procEnv.Graph));
                else if(value is IList)
                    procEnv.Recorder.Write(ContainerHelper.ToString((IList)value, procEnv.Graph));
                else if(value is IDeque)
                    procEnv.Recorder.Write(ContainerHelper.ToString((IDeque)value, procEnv.Graph));
                else
                    procEnv.Recorder.Write(ContainerHelper.ToString(value, procEnv.Graph));
            }
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Expression.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Expression; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "record(" + Expression.Symbol + ")"; } }
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
            object value = Name.Evaluate(procEnv);
            List<string> arguments = new List<string>();
            arguments.Add(value.ToString());
            IGraph graph = Graph != null ? (IGraph)Graph.Evaluate(procEnv) : procEnv.Graph;
            if(graph is INamedGraph)
                Porter.Export((INamedGraph)graph, arguments);
            else
                Porter.Export(graph, arguments);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Name.GetLocalVariables(variables);
            if(Graph!=null)
                Graph.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield return Name; if(Graph != null) yield return Graph; else yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "export(" + (Graph!=null ? Graph.Symbol + ", " : "") + Name.Symbol + ")"; } }
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Expr.GetLocalVariables(variables);
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

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "clear()"; } }
    }
}
