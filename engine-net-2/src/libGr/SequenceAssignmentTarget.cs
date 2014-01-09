/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
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
    /// Specifies the assignment target type (the lhs expression).
    /// </summary>
    public enum AssignmentTargetType
    {
        Var,
        YieldingToVar,
        IndexedVar,
        Attribute,
        AttributeIndexed,
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
        internal override sealed SequenceComputation Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return CopyTarget(originalToCopy, procEnv);
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
        internal abstract AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv);

        /// <summary>
        /// Assigns a value to this assignment target.
        /// </summary>
        /// <param name="value">The value to assign.</param>
        /// <param name="procEnv">The graph processing environment on which this assignment is to be executed.
        ///     Containing especially the graph on which this assignment is to be executed.
        ///     And the user proxy queried when choices are due.</param>
        public abstract void Assign(object value, IGraphProcessingEnvironment procEnv);

        public override sealed object Execute(IGraphProcessingEnvironment procEnv)
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

        public override string Type(SequenceCheckingEnvironment env)
        {
            return DestVar.Type;
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            AssignmentTargetVar copy = (AssignmentTargetVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            DestVar.SetVariableValue(value, procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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

        public override string Type(SequenceCheckingEnvironment env)
        {
            return DestVar.Type;
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            AssignmentTargetYieldingVar copy = (AssignmentTargetYieldingVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            throw new Exception("yield is only available in the compiled sequences (exec)");
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
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

            if(DestVar.Type == "")
                return; // we can't check source and destination types if the variable is untyped, only runtime-check possible

            if(TypesHelper.ExtractSrc(DestVar.Type) == null || TypesHelper.ExtractDst(DestVar.Type) == null || TypesHelper.ExtractDst(DestVar.Type) == "SetValueType")
            {
                throw new SequenceParserException(Symbol, "map<S,T> or array<T> or deque<T>", DestVar.Type);
            }
            if(DestVar.Type.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpression.Type(env));
                }
            }
            else if(DestVar.Type.StartsWith("deque"))
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
            if(DestVar.Type.StartsWith("map"))
                return TypesHelper.ExtractDst(DestVar.Type) ?? "";
            else
                return TypesHelper.ExtractSrc(DestVar.Type) ?? "";
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            AssignmentTargetIndexedVar copy = (AssignmentTargetIndexedVar)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            copy.KeyExpression = KeyExpression.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            object container = DestVar.GetVariableValue(procEnv);
            object key = KeyExpression.Evaluate(procEnv);

            if(container is IList)
            {
                IList array = (IList)container;
                if(array.Count > (int)key)
                    array[(int)key] = value;
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                if(deque.Count > (int)key)
                    deque[(int)key] = value;
            }
            else
            {
                IDictionary map = (IDictionary)container;
                if(map.Contains(key))
                    map[key] = value;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            DestVar.GetLocalVariables(variables);
            KeyExpression.GetLocalVariables(variables, containerConstructors);
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

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            AssignmentTargetAttribute copy = (AssignmentTargetAttribute)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)DestVar.GetVariableValue(procEnv);
            AttributeType attrType;
            value = ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(
                elem, AttributeName, value, out attrType);
            AttributeChangeType changeType = AttributeChangeType.Assign;
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, null);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, null);
            elem.SetAttribute(AttributeName, value);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            DestVar.GetLocalVariables(variables);
        }

        public override string Symbol { get { return DestVar.Name + "." + AttributeName; } }
        public override IEnumerable<SequenceComputation> Children { get { yield break; } }
    }

    public class AssignmentTargetAttributeIndexed : AssignmentTarget
    {
        public SequenceVariable DestVar;
        public String AttributeName;
        public SequenceExpression KeyExpression;

        public AssignmentTargetAttributeIndexed(SequenceVariable destVar, String attributeName, SequenceExpression keyExpr)
            : base(AssignmentTargetType.AttributeIndexed)
        {
            DestVar = destVar;
            AttributeName = attributeName;
            KeyExpression = keyExpr;
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

            string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);
            if(TypesHelper.ExtractSrc(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == null || TypesHelper.ExtractDst(ContainerType) == "SetValueType")
            {
                throw new SequenceParserException(Symbol, "map<S,T> or array<T> or deque<T>", DestVar.Type);
            }
            if(ContainerType.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpression.Type(env));
                }
            }
            else if(ContainerType.StartsWith("deque"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                {
                    throw new SequenceParserException(Symbol, "int", KeyExpression.Type(env));
                }
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                {
                    throw new SequenceParserException(Symbol, TypesHelper.ExtractSrc(ContainerType), KeyExpression.Type(env));
                }
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(DestVar.Type == "")
                return "";

            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(DestVar.Type, env.Model);
            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(AttributeName);
            if(attributeType == null)
                return ""; // error, will be reported by Check, just ensure we don't crash here

            string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);

            if(DestVar.Type.StartsWith("map"))
                return TypesHelper.ExtractDst(DestVar.Type) ?? "";
            else
                return TypesHelper.ExtractSrc(DestVar.Type) ?? "";
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            AssignmentTargetAttributeIndexed copy = (AssignmentTargetAttributeIndexed)MemberwiseClone();
            copy.DestVar = DestVar.Copy(originalToCopy, procEnv);
            copy.KeyExpression = KeyExpression.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)DestVar.GetVariableValue(procEnv);
            object container = elem.GetAttribute(AttributeName);
            object key = KeyExpression.Evaluate(procEnv);

            AttributeType attrType = elem.Type.GetAttributeType(AttributeName);
            AttributeChangeType changeType = AttributeChangeType.AssignElement;
            if(elem is INode)
                procEnv.Graph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, key);
            else
                procEnv.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, key);

            if(container is IList)
            {
                IList array = (IList)container;
                array[(int)key] = value;
            }
            else if(container is IDeque)
            {
                IDeque deque = (IDeque)container;
                deque[(int)key] = value;
            }
            else
            {
                IDictionary map = (IDictionary)container;
                map[key] = value;
            }
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            DestVar.GetLocalVariables(variables);
            KeyExpression.GetLocalVariables(variables, containerConstructors);
        }

        public override string Symbol { get { return DestVar.Name + "." + AttributeName + "[" + KeyExpression.Symbol + "]"; } }
        public override IEnumerable<SequenceComputation> Children { get { yield return KeyExpression; } }
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

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            AssignmentTargetVisited copy = (AssignmentTargetVisited)MemberwiseClone();
            copy.GraphElementVar = GraphElementVar.Copy(originalToCopy, procEnv);
            copy.VisitedFlagExpression = VisitedFlagExpression.CopyExpression(originalToCopy, procEnv);
            return copy;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(procEnv);
            int visitedFlag = (int)VisitedFlagExpression.Evaluate(procEnv);
            procEnv.Graph.SetVisited(elem, visitedFlag, (bool)value);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            GraphElementVar.GetLocalVariables(variables);
            VisitedFlagExpression.GetLocalVariables(variables, containerConstructors);
        }

        public override string Symbol { get { return GraphElementVar.Name + ".visited[" + VisitedFlagExpression.Symbol + "]"; } }
        public override IEnumerable<SequenceComputation> Children { get { yield return VisitedFlagExpression; } }
    }
}
