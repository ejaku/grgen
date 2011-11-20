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
    /// Specifies the actual subtype of a sequence computation.
    /// A new expression type -> you must add the corresponding class down below 
    /// and adapt the lgspSequenceGenerator.
    /// </summary>
    public enum SequenceComputationType
    {
        VFree, VReset,
        ContainerAdd, ContainerRem, ContainerClear,
        AssignExprToVar, YieldingAssignExprToVar, AssignExprToIndexedVar, AssignExprToAttribute, AssignExprToVisited,
        Emit, Record,
        Expression // every expression is a computation
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
        /// The type of the sequence computatoin (e.g. Assignment or MethodCall)
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
        internal abstract SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy);

        /// <summary>
        /// Executes this sequence computation.
        /// </summary>
        /// <param name="graph">The graph on which this sequence computation is to be evaluated.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>The value resulting from computing this sequence computation, 
        ///          null if there is no result value</returns>
        public abstract object Execute(IGraph graph, SequenceExecutionEnvironment env);

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
    /// A sequence computation which assigns an expression.
    /// </summary>
    public abstract class SequenceComputationAssignExpr : SequenceComputation
    {
        public SequenceExpression SourceExpression;

        /// <summary>
        /// Initializes a new SequenceComputation object with the given sequence computation type.
        /// </summary>
        /// <param name="seqCompType">The sequence computation type.</param>
        public SequenceComputationAssignExpr(SequenceExpression sourceExpr, SequenceComputationType seqCompType)
            : base(seqCompType)
        {
            SourceExpression = sourceExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            SourceExpression.Check(env);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } } // no children, will change with assignment operator instead of statement
        public override int Precedence { get { return 8; } } // always a top prio assignment factor
    }

    public class SequenceComputationVFree : SequenceComputation
    {
        public SequenceExpression VisitedFlagExpression;

        public SequenceComputationVFree(SequenceExpression visitedFlagExpr)
            : base(SequenceComputationType.VFree)
        {
            VisitedFlagExpression = visitedFlagExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            VisitedFlagExpression.Check(env);

            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
            {
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationVFree copy = (SequenceComputationVFree)MemberwiseClone();
            copy.VisitedFlagExpression = VisitedFlagExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(graph, env);
            graph.FreeVisitedFlag(visitedFlag);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            VisitedFlagExpression.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "vfree(" + VisitedFlagExpression.Symbol + ")"; } }
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
            VisitedFlagExpression.Check(env);

            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
            {
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationVReset copy = (SequenceComputationVReset)MemberwiseClone();
            copy.VisitedFlagExpression = VisitedFlagExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(graph, env);
            graph.ResetVisitedFlag(visitedFlag);
            return null;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            VisitedFlagExpression.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "vreset(" + VisitedFlagExpression.Symbol + ")"; } }
    }

    public class SequenceComputationContainerAdd : SequenceComputation
    {
        public SequenceVariable Container;
        public SequenceExpression Expr;
        public SequenceExpression ExprDst;

        public SequenceComputationContainerAdd(SequenceVariable container, SequenceExpression expr, SequenceExpression exprDst)
            : base(SequenceComputationType.ContainerAdd)
        {
            Container = container;
            Expr = expr;
            ExprDst = exprDst;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            Expr.Check(env);
            if(ExprDst != null)
                ExprDst.Check(env);

            if(Container.Type == "")
                return; // we can't check further types if the variable is untyped, only runtime-check possible

            if(!Container.Type.StartsWith("set<") && !Container.Type.StartsWith("map<") && !Container.Type.StartsWith("array<"))
            {
                throw new SequenceParserException(Symbol, ExprDst == null ? "set or array type" : "map or array type", Container.Type);
            }
            if(ExprDst != null && TypesHelper.ExtractDst(Container.Type) == "SetValueType")
            {
                throw new SequenceParserException(Symbol, "map type or array", Container.Type);
            }
            if(Container.Type.StartsWith("array<"))
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(Container.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(Container.Type), Expr.Type(env));
                }
                if(ExprDst != null && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(Container.Type), ExprDst.Type(env));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(Container.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(Container.Type), Expr.Type(env));
                }
                if(TypesHelper.ExtractDst(Container.Type) != "SetValueType"
                    && !TypesHelper.IsSameOrSubtype(ExprDst.Type(env), TypesHelper.ExtractDst(Container.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractDst(Container.Type), ExprDst.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationContainerAdd copy = (SequenceComputationContainerAdd)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.Expr = Expr.CopyExpression(originalToCopy);
            if(ExprDst != null)
                copy.ExprDst = ExprDst.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                if(ExprDst == null)
                    array.Add(Expr.Evaluate(graph, env));
                else
                    array.Insert((int)ExprDst.Evaluate(graph, env), Expr.Evaluate(graph, env));
                return array;
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                if(setmap.Contains(Expr.Evaluate(graph, env)))
                    setmap[Expr.Evaluate(graph, env)] = (ExprDst == null ? null : ExprDst.Evaluate(graph, env));
                else
                    setmap.Add(Expr.Evaluate(graph, env), (ExprDst == null ? null : ExprDst.Evaluate(graph, env)));
                return setmap;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
            Expr.GetLocalVariables(variables);
            if(ExprDst != null)
                ExprDst.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name + ".add(" + Expr.Symbol + (ExprDst != null ? "," + ExprDst.Symbol : "") + ")"; } }
    }

    public class SequenceComputationContainerRem : SequenceComputation
    {
        public SequenceVariable Container;
        public SequenceExpression Expr;

        public SequenceComputationContainerRem(SequenceVariable container, SequenceExpression expr)
            : base(SequenceComputationType.ContainerRem)
        {
            Container = container;
            Expr = expr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Expr != null)
                Expr.Check(env);

            if(Container.Type == "")
                return; // we can't check further types if the variable is untyped, only runtime-check possible

            if(!Container.Type.StartsWith("set<") && !Container.Type.StartsWith("map<") && !Container.Type.StartsWith("array<"))
            {
                throw new SequenceParserException(Symbol, "set or map or array type", Container.Type);
            }
            if(Container.Type.StartsWith("array<"))
            {
                if(Expr != null && !TypesHelper.IsSameOrSubtype(Expr.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", Expr.Type(env));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(Container.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(Container.Type), Expr.Type(env));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationContainerRem copy = (SequenceComputationContainerRem)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            if(Expr != null)
                copy.Expr = Expr.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                if(Expr == null)
                    array.RemoveAt(array.Count - 1);
                else
                    array.RemoveAt((int)Expr.Evaluate(graph, env));
                return array;
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                setmap.Remove(Expr.Evaluate(graph, env));
                return setmap;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
            if(Expr != null)
                Expr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name + ".rem(" + (Expr != null ? Expr.Symbol : "") + ")"; } }
    }

    public class SequenceComputationContainerClear : SequenceComputation
    {
        public SequenceVariable Container;

        public SequenceComputationContainerClear(SequenceVariable container)
            : base(SequenceComputationType.ContainerClear)
        {
            Container = container;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Container.Type == "")
                return; // we can't check further types if the variable is untyped, only runtime-check possible

            if(!Container.Type.StartsWith("set<") && !Container.Type.StartsWith("map<") && !Container.Type.StartsWith("array<"))
            {
                throw new SequenceParserException(Symbol, "set or map or array type", Container.Type);
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationContainerClear copy = (SequenceComputationContainerClear)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                array.Clear();
                return array;
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                setmap.Clear();
                return setmap;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name + ".clear()"; } }
    }

    public class SequenceComputationAssignExprToVar : SequenceComputationAssignExpr
    {
        public SequenceVariable DestVar;

        public SequenceComputationAssignExprToVar(SequenceVariable destVar, SequenceExpression srcExpr)
            : base(srcExpr, SequenceComputationType.AssignExprToVar)
        {
            DestVar = destVar;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            // the assignment of an untyped variable to a typed variable is ok, cause we want access to persistency
            // which is only offered by the untyped variables; it is checked at runtime / causes an invalid cast exception
            if(!TypesHelper.IsSameOrSubtype(SourceExpression.Type(env), DestVar.Type, env.Model))
            {
                throw new SequenceParserException(Symbol, DestVar.Type, SourceExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationAssignExprToVar copy = (SequenceComputationAssignExprToVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.SourceExpression = SourceExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            object value = SourceExpression.Evaluate(graph, env);
            DestVar.SetVariableValue(value, graph);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
            SourceExpression.GetLocalVariables(variables);
        }

        public override string Symbol { get { return DestVar.Name + "=" + SourceExpression.Symbol; } }
    }

    public class SequenceComputationYieldingAssignExprToVar : SequenceComputationAssignExpr
    {
        public SequenceVariable DestVar;

        public SequenceComputationYieldingAssignExprToVar(SequenceVariable destVar, SequenceExpression srcExpr)
            : base(srcExpr, SequenceComputationType.YieldingAssignExprToVar)
        {
            DestVar = destVar;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            // the assignment of an untyped variable to a typed variable is ok, cause we want access to persistency
            // which is only offered by the untyped variables; it is checked at runtime / causes an invalid cast exception
            if(!TypesHelper.IsSameOrSubtype(SourceExpression.Type(env), DestVar.Type, env.Model))
            {
                throw new SequenceParserException(Symbol, DestVar.Type, SourceExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationYieldingAssignExprToVar copy = (SequenceComputationYieldingAssignExprToVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.SourceExpression = SourceExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            throw new Exception("yield is only available in the compiled sequences (exec)");
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
            SourceExpression.GetLocalVariables(variables);
        }

        public override string Symbol { get { return "yield " + DestVar.Name + "=" + SourceExpression.Symbol; } }
    }

    public class SequenceComputationAssignExprToIndexedVar : SequenceComputationAssignExpr
    {
        public SequenceVariable DestVar;
        public SequenceExpression KeyExpression;

        public SequenceComputationAssignExprToIndexedVar(SequenceVariable destVar, SequenceExpression keyExpr, SequenceExpression srcExpr)
            : base(srcExpr, SequenceComputationType.AssignExprToIndexedVar)
        {
            DestVar = destVar;
            KeyExpression = keyExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);
            KeyExpression.Check(env);

            if(DestVar.Type == "")
                return; // we can't check source and destination types if the variable is untyped, only runtime-check possible

            if(TypesHelper.ExtractSrc(DestVar.Type) == null || TypesHelper.ExtractDst(DestVar.Type) == null || TypesHelper.ExtractDst(DestVar.Type) == "SetValueType")
            {
                throw new SequenceParserException(Symbol, "map<S,T> or array<T>", DestVar.Type);
            }
            if(DestVar.Type.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpression.Type(env));
                }
                if(!TypesHelper.IsSameOrSubtype(SourceExpression.Type(env), TypesHelper.ExtractSrc(DestVar.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, SourceExpression.Type(env), TypesHelper.ExtractSrc(DestVar.Type));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), TypesHelper.ExtractSrc(DestVar.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(DestVar.Type), KeyExpression.Type(env));
                }
                if(!TypesHelper.IsSameOrSubtype(SourceExpression.Type(env), TypesHelper.ExtractDst(DestVar.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, SourceExpression.Type(env), TypesHelper.ExtractDst(DestVar.Type));
                }
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationAssignExprToIndexedVar copy = (SequenceComputationAssignExprToIndexedVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.KeyExpression = KeyExpression.CopyExpression(originalToCopy);
            copy.SourceExpression = SourceExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(DestVar.GetVariableValue(graph) is IList)
            {
                IList array = (IList)DestVar.GetVariableValue(graph);
                int key = (int)KeyExpression.Evaluate(graph, env);
                object value = SourceExpression.Evaluate(graph, env);
                array[key] = value;
                return value;
            }
            else
            {
                IDictionary setmap = (IDictionary)DestVar.GetVariableValue(graph);
                object key = KeyExpression.Evaluate(graph, env);
                object value = SourceExpression.Evaluate(graph, env);
                setmap[key] = value;
                return value;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
            KeyExpression.GetLocalVariables(variables);
            SourceExpression.GetLocalVariables(variables);
        }

        public override string Symbol { get { return DestVar.Name + "[" + KeyExpression.Symbol + "] = " + SourceExpression.Symbol; } }
    }

    public class SequenceComputationAssignExprToAttribute : SequenceComputationAssignExpr
    {
        public SequenceVariable DestVar;
        public String AttributeName;

        public SequenceComputationAssignExprToAttribute(SequenceVariable destVar, String attributeName, SequenceExpression srcExpr)
            : base(srcExpr, SequenceComputationType.AssignExprToAttribute)
        {
            DestVar = destVar;
            AttributeName = attributeName;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(DestVar.Type == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(DestVar.Type, env.Model);
            if(nodeOrEdgeType == null)
            {
                throw new SequenceParserException(Symbol, "node or edge type", DestVar.Type);
            }
            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(AttributeName);
            if(attributeType == null)
            {
                throw new SequenceParserException(AttributeName, SequenceParserError.UnknownAttribute);
            }
            if(!TypesHelper.IsSameOrSubtype(SourceExpression.Type(env), TypesHelper.AttributeTypeToXgrsType(attributeType), env.Model))
            {
                throw new SequenceParserException(Symbol, TypesHelper.AttributeTypeToXgrsType(attributeType), SourceExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationAssignExprToAttribute copy = (SequenceComputationAssignExprToAttribute)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.SourceExpression = SourceExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            object value = SourceExpression.Evaluate(graph, env);
            IGraphElement elem = (IGraphElement)DestVar.GetVariableValue(graph);
            AttributeType attrType;
            value = DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                elem, AttributeName, value, out attrType);
            AttributeChangeType changeType = AttributeChangeType.Assign;
            if(elem is INode)
                graph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, null);
            else
                graph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, null);
            elem.SetAttribute(AttributeName, value);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
            SourceExpression.GetLocalVariables(variables);
        }

        public override string Symbol { get { return DestVar.Name + "." + AttributeName + "=" + SourceExpression.Symbol; } }
    }

    public class SequenceComputationAssignExprToVisited : SequenceComputationAssignExpr
    {
        public SequenceVariable GraphElementVar;
        public SequenceExpression VisitedFlagExpression;

        public SequenceComputationAssignExprToVisited(SequenceVariable graphElementVar, SequenceExpression visitedFlagExpr, SequenceExpression srcExpr)
            : base(srcExpr, SequenceComputationType.AssignExprToVisited)
        {
            GraphElementVar = graphElementVar;
            VisitedFlagExpression = visitedFlagExpr;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);
            VisitedFlagExpression.Check(env);

            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(GraphElementVar.Type, env.Model);
            if(GraphElementVar.Type != "" && nodeOrEdgeType == null)
            {
                throw new SequenceParserException(Symbol, "node or edge type", GraphElementVar.Type);
            }
            if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
            {
                throw new SequenceParserException(Symbol, "int", VisitedFlagExpression.Type(env));
            }
            if(!TypesHelper.IsSameOrSubtype(SourceExpression.Type(env), "boolean", env.Model))
            {
                throw new SequenceParserException(Symbol, "boolean", SourceExpression.Type(env));
            }
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationAssignExprToVisited copy = (SequenceComputationAssignExprToVisited)MemberwiseClone();
            copy.GraphElementVar = GraphElementVar.Copy(originalToCopy);
            copy.VisitedFlagExpression = VisitedFlagExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(graph);
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(graph, env);
            object value = SourceExpression.Evaluate(graph, env);
            graph.SetVisited(elem, visitedFlag, (bool)value);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagExpression.GetLocalVariables(variables);
            SourceExpression.GetLocalVariables(variables);
        }

        public override string Symbol { get { return GraphElementVar.Name + ".visited[" + VisitedFlagExpression.Symbol + "]=" + SourceExpression.Symbol; } }
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            Expression.Check(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationEmit copy = (SequenceComputationEmit)MemberwiseClone();
            copy.Expression = Expression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            object value = Expression.Evaluate(graph, env);
            if(value != null)
            {
                if(value is IDictionary)
                    graph.EmitWriter.Write(DictionaryListHelper.ToString((IDictionary)value, env != null ? env.GetNamedGraph() : graph));
                else if(value is IList)
                    graph.EmitWriter.Write(DictionaryListHelper.ToString((IList)value, env != null ? env.GetNamedGraph() : graph));
                else
                    graph.EmitWriter.Write(DictionaryListHelper.ToString(value, env != null ? env.GetNamedGraph() : graph));
            }
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Expression.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
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

        public override void Check(SequenceCheckingEnvironment env)
        {
            Expression.Check(env);
        }

        internal override SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceComputationRecord copy = (SequenceComputationRecord)MemberwiseClone();
            copy.Expression = Expression.CopyExpression(originalToCopy);
            return copy;
        }

        public override object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            object value = Expression.Evaluate(graph, env);
            if(value != null)
            {
                if(value is IDictionary)
                    graph.Recorder.Write(DictionaryListHelper.ToString((IDictionary)value, env != null ? env.GetNamedGraph() : graph));
                else if(value is IList)
                    graph.Recorder.Write(DictionaryListHelper.ToString((IList)value, env != null ? env.GetNamedGraph() : graph));
                else
                    graph.Recorder.Write(DictionaryListHelper.ToString(value, env != null ? env.GetNamedGraph() : graph));
            }
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Expression.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "record(" + Expression.Symbol + ")"; } }
    }
}
