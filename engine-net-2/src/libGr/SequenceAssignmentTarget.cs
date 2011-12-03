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
    /// Specifies the assignment target type (the lhs expression).
    /// </summary>
    public enum AssignmentTargetType
    {
        Var,
        YieldingToVar,
        IndexedVar,
        Attribute,
        Visited
    }

    /// <summary>
    /// An assignment target object with references to used sequence computations.
    /// </summary>
    public abstract class AssignmentTarget : SequenceComputation
    {
        /// <summary>
        /// The type of the assignment target (e.g. Variable or IndexedVariable)
        /// </summary>
        public AssignmentTargetType AssignmentTargetType;

        /// <summary>
        /// Initializes a new AssignmentTargetType object with the given assignment target type.
        /// </summary>
        /// <param name="seqCompType">The sequence computation type.</param>
        public AssignmentTarget(AssignmentTargetType assignTgtType)
            : base(SequenceComputationType.AssignmentTarget)
        {
            AssignmentTargetType = assignTgtType;
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
        internal override sealed SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            return CopyTarget(originalToCopy);
        }

        /// <summary>
        /// Copies the assignment target deeply so that
        /// - the global Variables are kept
        /// - the local Variables are replaced by copies initialized to null
        /// Used for cloning defined sequences before executing them if needed.
        /// Needed if the defined sequence is currently executed to prevent state corruption.
        /// </summary>
        /// <param name="originalToCopy">A map used to ensure that every instance of a variable is mapped to the same copy</param>
        /// <returns>The copy of the assignment target</returns>
        internal abstract AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy);

        /// <summary>
        /// Assigns a value to this assignment target.
        /// </summary>
        /// <param name="value">The value to assign.</param>
        /// <param name="graph">The graph on which this assignment is to be executed.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        public abstract void Assign(object value, IGraph graph, SequenceExecutionEnvironment env);

        public override sealed object Execute(IGraph graph, SequenceExecutionEnvironment env)
        {
            throw new Exception("Internal error! AssignmentTarget executed as SequenceComputation.");
        }

        public override sealed int Precedence { get { return 9; } } // irrelevant, always top prio
    }


    public class AssignmentTargetVar : AssignmentTarget
    {
        public SequenceVariable DestVar;

        public AssignmentTargetVar(SequenceVariable destVar)
            : base(AssignmentTargetType.Var)
        {
            DestVar = destVar;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return DestVar.Type;
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            AssignmentTargetVar copy = (AssignmentTargetVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            return copy;
        }

        public override void Assign(object value, IGraph graph, SequenceExecutionEnvironment env)
        {
            DestVar.SetVariableValue(value, graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
        }

        public override string Symbol { get { return DestVar.Name; } }
        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
    }

    public class AssignmentTargetYieldingVar : AssignmentTarget
    {
        public SequenceVariable DestVar;

        public AssignmentTargetYieldingVar(SequenceVariable destVar)
            : base(AssignmentTargetType.YieldingToVar)
        {
            DestVar = destVar;
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return DestVar.Type;
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            AssignmentTargetYieldingVar copy = (AssignmentTargetYieldingVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            return copy;
        }

        public override void Assign(object value, IGraph graph, SequenceExecutionEnvironment env)
        {
            throw new Exception("yield is only available in the compiled sequences (exec)");
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
        }

        public override string Symbol { get { return "yield " + DestVar.Name; } }
        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
    }

    public class AssignmentTargetIndexedVar : AssignmentTarget
    {
        public SequenceVariable DestVar;
        public SequenceExpression KeyExpression;

        public AssignmentTargetIndexedVar(SequenceVariable destVar, SequenceExpression keyExpr)
            : base(AssignmentTargetType.IndexedVar)
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
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), TypesHelper.ExtractSrc(DestVar.Type), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(DestVar.Type), KeyExpression.Type(env));
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(DestVar.Type.StartsWith("array"))
                return TypesHelper.ExtractSrc(DestVar.Type);
            else
                return TypesHelper.ExtractDst(DestVar.Type);
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            AssignmentTargetIndexedVar copy = (AssignmentTargetIndexedVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            copy.KeyExpression = KeyExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override void Assign(object value, IGraph graph, SequenceExecutionEnvironment env)
        {
            if(DestVar.GetVariableValue(graph) is IList)
            {
                IList array = (IList)DestVar.GetVariableValue(graph);
                int key = (int)KeyExpression.Evaluate(graph, env);
                array[key] = value;
            }
            else
            {
                IDictionary setmap = (IDictionary)DestVar.GetVariableValue(graph);
                object key = KeyExpression.Evaluate(graph, env);
                setmap[key] = value;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
            KeyExpression.GetLocalVariables(variables);
        }

        public override string Symbol { get { return DestVar.Name + "[" + KeyExpression.Symbol + "]"; } }
        public override IEnumerable<SequenceComputation> Children { get { yield return KeyExpression; } }
    }

    public class AssignmentTargetAttribute : AssignmentTarget
    {
        public SequenceVariable DestVar;
        public String AttributeName;

        public AssignmentTargetAttribute(SequenceVariable destVar, String attributeName)
            : base(AssignmentTargetType.Attribute)
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
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(DestVar.Type == "")
                return "";

            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(DestVar.Type, env.Model);
            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(AttributeName);
            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            AssignmentTargetAttribute copy = (AssignmentTargetAttribute)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy);
            return copy;
        }

        public override void Assign(object value, IGraph graph, SequenceExecutionEnvironment env)
        {
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
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            DestVar.GetLocalVariables(variables);
        }

        public override string Symbol { get { return DestVar.Name + "." + AttributeName; } }
        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
    }

    public class AssignmentTargetVisited : AssignmentTarget
    {
        public SequenceVariable GraphElementVar;
        public SequenceExpression VisitedFlagExpression;

        public AssignmentTargetVisited(SequenceVariable graphElementVar, SequenceExpression visitedFlagExpr)
            : base(AssignmentTargetType.Visited)
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
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            AssignmentTargetVisited copy = (AssignmentTargetVisited)MemberwiseClone();
            copy.GraphElementVar = GraphElementVar.Copy(originalToCopy);
            copy.VisitedFlagExpression = VisitedFlagExpression.CopyExpression(originalToCopy);
            return copy;
        }

        public override void Assign(object value, IGraph graph, SequenceExecutionEnvironment env)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(graph);
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(graph, env);
            graph.SetVisited(elem, visitedFlag, (bool)value);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagExpression.GetLocalVariables(variables);
        }

        public override string Symbol { get { return GraphElementVar.Name + ".visited[" + VisitedFlagExpression.Symbol + "]"; } }
        public override IEnumerable<SequenceComputation> Children { get { yield return VisitedFlagExpression; } }
    }
}
