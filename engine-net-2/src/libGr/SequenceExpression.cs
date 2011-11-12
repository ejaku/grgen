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
        True, False, Constant,
        Variable,
        Def,
        IsVisited,
        VAlloc,
        InContainer, ContainerEmpty, ContainerSize, ContainerAccess,
        ElementFromGraph,
        GraphElementAttribute
    }

    /// <summary>
    /// A sequence expression object with references to child sequence expressions.
    /// A language construct is an expression and not a sequence if: 
    /// - it may return non-boolean values (primary)
    /// - it is a side effect free query (secondary)
    /// </summary>
    public abstract class SequenceExpression : SequenceBase
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
        {
            SequenceExpressionType = seqExprType;

            id = idSource;
            ++idSource;
        }

        /// <summary>
        /// Checks the sequence expression for errors utilizing the given checking environment
        /// reports them by exception
        /// default behavior: check all the children 
        /// </summary>
        public override void Check(SequenceCheckingEnvironment env)
        {
            foreach(SequenceExpression childSeq in Children)
                childSeq.Check(env);
        }

        /// <summary>
        /// Returns the type of the sequence
        /// default behaviour: returns "boolean"
        /// </summary>
        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        /// <summary>
        /// Copies the sequence expression deeply so that
        /// - the global Variables are kept
        /// - the local Variables are replaced by copies initialized to null
        /// Used for cloning defined sequences before executing them if needed.
        /// Needed if the defined sequence is currently executed to prevent state corruption.
        /// </summary>
        /// <param name="originalToCopy">A map used to ensure that every instance of a variable is mapped to the same copy</param>
        /// <returns>The copy of the sequence</returns>
        internal abstract SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy);

        /// <summary>
        /// Evaluates this sequence expression.
        /// </summary>
        /// <param name="graph">The graph on which this sequence expression is to be evaluated.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>The value resulting from computing this sequence expression</returns>
        public abstract object Evaluate(IGraph graph, SequenceExecutionEnvironment env);

        /// <summary>
        /// Collects all variables of the sequence expression tree into the variables dictionary.
        /// </summary>
        /// <param name="variables">Contains the variables found</param>
        public virtual void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
        }

        /// <summary>
        /// Enumerates all child sequence expression objects
        /// </summary>
        public abstract IEnumerable<SequenceExpression> Children { get; }

        /// <summary>
        /// The type of this sequence expression as string ("" if statically unknown).
        /// </summary>
    }


    public class SequenceExpressionTrue : SequenceExpression
    {
        public SequenceExpressionTrue()
            : base(SequenceExpressionType.True)
        {
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionTrue copy = (SequenceExpressionTrue)MemberwiseClone();
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env) { return true; }
        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "true"; } }
    }

    public class SequenceExpressionFalse : SequenceExpression
    {
        public SequenceExpressionFalse()
            : base(SequenceExpressionType.False)
        {
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionFalse copy = (SequenceExpressionFalse)MemberwiseClone();
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env) { return false; }
        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "false"; } }
    }

    public class SequenceExpressionVariable : SequenceExpression
    {
        public SequenceVariable PredicateVar;

        public SequenceExpressionVariable(SequenceVariable var)
            : base(SequenceExpressionType.Variable)
        {
            PredicateVar = var;
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return PredicateVar.Type;
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionVariable copy = (SequenceExpressionVariable)MemberwiseClone();
            copy.PredicateVar = PredicateVar.Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            return PredicateVar.GetVariableValue(graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            PredicateVar.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return PredicateVar.Name; } }
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
            foreach(SequenceExpression defVar in DefVars)
            {
                if(!(defVar is SequenceExpressionVariable))
                    throw new SequenceParserException(Symbol, "variable", "not a variable");
            }
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionDef copy = (SequenceExpressionDef)MemberwiseClone();
            copy.DefVars = new SequenceExpression[DefVars.Length];
            for(int i = 0; i < DefVars.Length; ++i)
                copy.DefVars[i] = DefVars[i].Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            foreach(SequenceExpression defVar in DefVars)
            {
                if(defVar.Evaluate(graph, env) == null)
                    return false;
            }
            return true;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            foreach(SequenceExpression defVar in DefVars)
                defVar.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { foreach(SequenceExpression defVar in DefVars) yield return defVar; } }
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

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionIsVisited copy = (SequenceExpressionIsVisited)MemberwiseClone();
            copy.GraphElementVar = GraphElementVar.Copy(originalToCopy);
            copy.VisitedFlagExpr = VisitedFlagExpr.Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(graph);
            int visitedFlag = (int)VisitedFlagExpr.Evaluate(graph, env);
            return graph.IsVisited(elem, visitedFlag);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagExpr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return GraphElementVar.Name + ".visited[" + VisitedFlagExpr.Symbol + "]"; } }
    }

    public class SequenceExpressionInContainer : SequenceExpression
    {
        public SequenceExpression Expr;
        public SequenceVariable Container;

        public SequenceExpressionInContainer(SequenceExpression expr, SequenceVariable container)
            : base(SequenceExpressionType.InContainer)
        {
            Expr = expr;
            Container = container;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Container.Type == "") 
                return; // we can't check further types if the variable is untyped, only runtime-check possible

            if(!Container.Type.StartsWith("set<") && !Container.Type.StartsWith("map<") && !Container.Type.StartsWith("array<"))
            {
                throw new SequenceParserException(Container.Name, "set or map or array type", Container.Type);
            }
            if(!TypesHelper.IsSameOrSubtype(Expr.Type(env), TypesHelper.ExtractSrc(Container.Type), env.Model))
            {
                throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(Container.Type), Expr.Type(env));
            }
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionInContainer copy = (SequenceExpressionInContainer)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.Expr = Expr.Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                return array.Contains(Expr.Evaluate(graph, env));
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                return setmap.Contains(Expr.Evaluate(graph, env));
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
            Expr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Expr.Symbol + " in " + Container.Name; } }
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

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionVAlloc copy = (SequenceExpressionVAlloc)MemberwiseClone();
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            return graph.AllocateVisitedFlag();
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "valloc()"; } }
    }

    public class SequenceExpressionContainerSize : SequenceExpression
    {
        public SequenceVariable Container;

        public SequenceExpressionContainerSize(SequenceVariable container)
            : base(SequenceExpressionType.ContainerSize)
        {
            Container = container;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Container.Type != "" && (TypesHelper.ExtractSrc(Container.Type) == null || TypesHelper.ExtractDst(Container.Type) == null))
            {
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> type", Container.Type);
            }
        }

        public override String Type(SequenceCheckingEnvironment env)
        {
            return "int";
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionContainerSize copy = (SequenceExpressionContainerSize)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                return array.Count;
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                return setmap.Count;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name + ".size()"; } }
    }

    public class SequenceExpressionContainerEmpty : SequenceExpression
    {
        public SequenceVariable Container;

        public SequenceExpressionContainerEmpty(SequenceVariable container)
            : base(SequenceExpressionType.ContainerEmpty)
        {
            Container = container;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            if(Container.Type != "" && (TypesHelper.ExtractSrc(Container.Type) == null || TypesHelper.ExtractDst(Container.Type) == null))
            {
                throw new SequenceParserException(Symbol, "set<S> or map<S,T> or array<S> type", Container.Type);
            }
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionContainerEmpty copy = (SequenceExpressionContainerEmpty)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                return array.Count == 0;
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                return setmap.Count == 0;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return Container.Name + ".empty()"; } }
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

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionContainerAccess copy = (SequenceExpressionContainerAccess)MemberwiseClone();
            copy.Container = Container.Copy(originalToCopy);
            copy.KeyExpr = KeyExpr.Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(Container.GetVariableValue(graph) is IList)
            {
                IList array = (IList)Container.GetVariableValue(graph);
                int key = (int)KeyExpr.Evaluate(graph, env);
                return array[key];
            }
            else
            {
                IDictionary setmap = (IDictionary)Container.GetVariableValue(graph);
                object key = KeyExpr.Evaluate(graph, env);
                return setmap[key];
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            Container.GetLocalVariables(variables);
            KeyExpr.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
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

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionElementFromGraph copy = (SequenceExpressionElementFromGraph)MemberwiseClone();
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            if(env == null && !(graph is NamedGraph))
                throw new InvalidOperationException("The @-operator can only be used with NamedGraphs!");
            NamedGraph namedGraph = null;
            if(env != null) namedGraph = env.GetNamedGraph();
            if(env == null) namedGraph = (NamedGraph)graph;
            IGraphElement elem = namedGraph.GetGraphElement(ElementName);
            if(elem == null)
                throw new InvalidOperationException("Graph element does not exist: \"" + ElementName + "\"!");
            return elem;
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return "@(" + ElementName + ")"; } }
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

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionConstant copy = (SequenceExpressionConstant)MemberwiseClone();
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            return Constant;
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
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
            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        internal override SequenceExpression Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceExpressionAttribute copy = (SequenceExpressionAttribute)MemberwiseClone();
            copy.SourceVar = SourceVar.Copy(originalToCopy);
            return copy;
        }

        public override object Evaluate(IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)SourceVar.GetVariableValue(graph);
            object value = elem.GetAttribute(AttributeName);
            value = DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(
                elem, AttributeName, value);
            return value;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            SourceVar.GetLocalVariables(variables);
        }

        public override IEnumerable<SequenceExpression> Children { get { yield break; } }
        public override int Precedence { get { return 8; } }
        public override string Symbol { get { return SourceVar.Name + "." + AttributeName; } }
    }
}
