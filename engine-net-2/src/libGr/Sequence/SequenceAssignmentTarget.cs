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
        public readonly AssignmentTargetType AssignmentTargetType;

        /// <summary>
        /// Initializes a new AssignmentTargetType object with the given assignment target type.
        /// </summary>
        /// <param name="assignTgtType">The assignment target type.</param>
        protected AssignmentTarget(AssignmentTargetType assignTgtType)
            : base(SequenceComputationType.AssignmentTarget)
        {
            AssignmentTargetType = assignTgtType;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The assignment target to be copied.</param>
        protected AssignmentTarget(AssignmentTarget that)
            : base(that)
        {
            AssignmentTargetType = that.AssignmentTargetType;
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
        /// <param name="procEnv">The graph processing environment</param>
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

        public override sealed object ExecuteImpl(IGraphProcessingEnvironment procEnv)
        {
            throw new Exception("Internal error! AssignmentTarget executed as SequenceComputation.");
        }

        public override sealed int Precedence
        {
            get { return 9; } // irrelevant, always top prio
        }
    }


    public class AssignmentTargetVar : AssignmentTarget
    {
        public readonly SequenceVariable DestVar;

        public AssignmentTargetVar(SequenceVariable destVar)
            : base(AssignmentTargetType.Var)
        {
            DestVar = destVar;
        }

        protected AssignmentTargetVar(AssignmentTargetVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            DestVar = that.DestVar.Copy(originalToCopy, procEnv);
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new AssignmentTargetVar(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return DestVar.Type;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            DestVar.SetVariableValue(value, procEnv);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            DestVar.GetLocalVariables(variables);
        }

        public override string Symbol
        {
            get { return DestVar.Name; }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }
    }

    public class AssignmentTargetYieldingVar : AssignmentTarget
    {
        public readonly SequenceVariable DestVar;

        public AssignmentTargetYieldingVar(SequenceVariable destVar)
            : base(AssignmentTargetType.YieldingToVar)
        {
            DestVar = destVar;
        }

        protected AssignmentTargetYieldingVar(AssignmentTargetYieldingVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            DestVar = that.DestVar.Copy(originalToCopy, procEnv);
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new AssignmentTargetYieldingVar(this, originalToCopy, procEnv);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return DestVar.Type;
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            throw new Exception("yield is only available in the compiled sequences (exec)");
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            DestVar.GetLocalVariables(variables);
        }

        public override string Symbol
        {
            get { return "yield " + DestVar.Name; }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }
    }

    public class AssignmentTargetIndexedVar : AssignmentTarget
    {
        public readonly SequenceVariable DestVar;
        public readonly SequenceExpression KeyExpression;

        public AssignmentTargetIndexedVar(SequenceVariable destVar, SequenceExpression keyExpr)
            : base(AssignmentTargetType.IndexedVar)
        {
            DestVar = destVar;
            KeyExpression = keyExpr;
        }

        protected AssignmentTargetIndexedVar(AssignmentTargetIndexedVar that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            DestVar = that.DestVar.Copy(originalToCopy, procEnv);
            KeyExpression = that.KeyExpression.CopyExpression(originalToCopy, procEnv);
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new AssignmentTargetIndexedVar(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(DestVar.Type == "")
                return; // we can't check source and destination types if the variable is untyped, only runtime-check possible

            if(TypesHelper.ExtractSrc(DestVar.Type) == null 
                || TypesHelper.ExtractDst(DestVar.Type) == null 
                || TypesHelper.ExtractDst(DestVar.Type) == "SetValueType")
            {
                throw new SequenceParserExceptionTypeMismatch(Symbol, "map<S,T> or array<T> or deque<T>", DestVar.Type);
            }
            if(DestVar.Type.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", KeyExpression.Type(env));
            }
            else if(DestVar.Type.StartsWith("deque"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", KeyExpression.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), TypesHelper.ExtractSrc(DestVar.Type), env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(DestVar.Type), KeyExpression.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(DestVar.Type.StartsWith("map"))
                return TypesHelper.ExtractDst(DestVar.Type) ?? "";
            else
                return TypesHelper.ExtractSrc(DestVar.Type) ?? "";
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
            List<SequenceExpressionConstructor> constructors)
        {
            DestVar.GetLocalVariables(variables);
            KeyExpression.GetLocalVariables(variables, constructors);
        }

        public override string Symbol
        {
            get { return DestVar.Name + "[" + KeyExpression.Symbol + "]"; }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield return KeyExpression; }
        }
    }

    public class AssignmentTargetAttribute : AssignmentTarget
    {
        public readonly SequenceVariable DestVar;
        public readonly String AttributeName;

        public AssignmentTargetAttribute(SequenceVariable destVar, String attributeName)
            : base(AssignmentTargetType.Attribute)
        {
            DestVar = destVar;
            AttributeName = attributeName;
        }

        protected AssignmentTargetAttribute(AssignmentTargetAttribute that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            DestVar = that.DestVar.Copy(originalToCopy, procEnv);
            AttributeName = that.AttributeName;
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new AssignmentTargetAttribute(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(DestVar.Type == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            InheritanceType inheritanceType = TypesHelper.GetInheritanceType(DestVar.Type, env.Model);
            if(inheritanceType == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge or object or transient object type (class)", DestVar.Type);
            AttributeType attributeType = inheritanceType.GetAttributeType(AttributeName);
            if(attributeType == null)
                throw new SequenceParserExceptionUnknownAttribute(AttributeName, inheritanceType.Name);
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(DestVar.Type == "")
                return "";

            InheritanceType inheritanceType = TypesHelper.GetInheritanceType(DestVar.Type, env.Model);
            AttributeType attributeType = inheritanceType.GetAttributeType(AttributeName);
            return TypesHelper.AttributeTypeToXgrsType(attributeType);
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            ContainerHelper.AssignAttribute(DestVar.GetVariableValue(procEnv), value, AttributeName, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            DestVar.GetLocalVariables(variables);
        }

        public override string Symbol
        {
            get { return DestVar.Name + "." + AttributeName; }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield break; }
        }
    }

    public class AssignmentTargetAttributeIndexed : AssignmentTarget
    {
        public readonly SequenceVariable DestVar;
        public readonly String AttributeName;
        public readonly SequenceExpression KeyExpression;

        public AssignmentTargetAttributeIndexed(SequenceVariable destVar, String attributeName, SequenceExpression keyExpr)
            : base(AssignmentTargetType.AttributeIndexed)
        {
            DestVar = destVar;
            AttributeName = attributeName;
            KeyExpression = keyExpr;
        }

        protected AssignmentTargetAttributeIndexed(AssignmentTargetAttributeIndexed that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            DestVar = that.DestVar.Copy(originalToCopy, procEnv);
            AttributeName = that.AttributeName;
            KeyExpression = that.KeyExpression.CopyExpression(originalToCopy, procEnv);
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new AssignmentTargetAttributeIndexed(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            if(DestVar.Type == "")
                return; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible

            InheritanceType inheritanceType = TypesHelper.GetInheritanceType(DestVar.Type, env.Model);
            if(inheritanceType == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge or object or transient object type (class)", DestVar.Type);
            AttributeType attributeType = inheritanceType.GetAttributeType(AttributeName);
            if(attributeType == null)
                throw new SequenceParserExceptionUnknownAttribute(AttributeName, inheritanceType.Name);

            string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);
            if(TypesHelper.ExtractSrc(ContainerType) == null
                || TypesHelper.ExtractDst(ContainerType) == null
                || TypesHelper.ExtractDst(ContainerType) == "SetValueType")
            {
                throw new SequenceParserExceptionTypeMismatch(Symbol, "map<S,T> or array<T> or deque<T>", DestVar.Type);
            }
            if(ContainerType.StartsWith("array"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", KeyExpression.Type(env));
            }
            else if(ContainerType.StartsWith("deque"))
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", KeyExpression.Type(env));
            }
            else
            {
                if(!TypesHelper.IsSameOrSubtype(KeyExpression.Type(env), TypesHelper.ExtractSrc(ContainerType), env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, TypesHelper.ExtractSrc(ContainerType), KeyExpression.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            if(DestVar.Type == "")
                return "";

            InheritanceType inheritanceType = TypesHelper.GetInheritanceType(DestVar.Type, env.Model);
            AttributeType attributeType = inheritanceType.GetAttributeType(AttributeName);
            if(attributeType == null)
                return ""; // error, will be reported by Check, just ensure we don't crash here

            string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);

            if(DestVar.Type.StartsWith("map"))
                return TypesHelper.ExtractDst(DestVar.Type) ?? "";
            else
                return TypesHelper.ExtractSrc(DestVar.Type) ?? "";
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            ContainerHelper.AssignAttributeIndexed(DestVar.GetVariableValue(procEnv), KeyExpression.Evaluate(procEnv),
                value, AttributeName, procEnv.Graph);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            DestVar.GetLocalVariables(variables);
            KeyExpression.GetLocalVariables(variables, constructors);
        }

        public override string Symbol
        {
            get { return DestVar.Name + "." + AttributeName + "[" + KeyExpression.Symbol + "]"; }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { yield return KeyExpression; }
        }
    }

    public class AssignmentTargetVisited : AssignmentTarget
    {
        public readonly SequenceVariable GraphElementVar;
        public readonly SequenceExpression VisitedFlagExpression;

        public AssignmentTargetVisited(SequenceVariable graphElementVar, SequenceExpression visitedFlagExpr)
            : base(AssignmentTargetType.Visited)
        {
            GraphElementVar = graphElementVar;
            VisitedFlagExpression = visitedFlagExpr;
        }

        protected AssignmentTargetVisited(AssignmentTargetVisited that, Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
            : base(that)
        {
            GraphElementVar = that.GraphElementVar.Copy(originalToCopy, procEnv);
            if(VisitedFlagExpression != null)
                VisitedFlagExpression = that.VisitedFlagExpression.CopyExpression(originalToCopy, procEnv);
        }

        internal override AssignmentTarget CopyTarget(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            return new AssignmentTargetVisited(this, originalToCopy, procEnv);
        }

        public override void Check(SequenceCheckingEnvironment env)
        {
            base.Check(env);

            GraphElementType graphElementType = TypesHelper.GetGraphElementType(GraphElementVar.Type, env.Model);
            if(GraphElementVar.Type != "" && graphElementType == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol, "node or edge type", GraphElementVar.Type);
            if(VisitedFlagExpression != null)
            {
                if(!TypesHelper.IsSameOrSubtype(VisitedFlagExpression.Type(env), "int", env.Model))
                    throw new SequenceParserExceptionTypeMismatch(Symbol, "int", VisitedFlagExpression.Type(env));
            }
        }

        public override string Type(SequenceCheckingEnvironment env)
        {
            return "boolean";
        }

        public override void Assign(object value, IGraphProcessingEnvironment procEnv)
        {
            IGraphElement elem = (IGraphElement)GraphElementVar.GetVariableValue(procEnv);
            int visitedFlag = VisitedFlagExpression != null ? (int)VisitedFlagExpression.Evaluate(procEnv) : 0;
            procEnv.Graph.SetVisited(elem, visitedFlag, (bool)value);
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors)
        {
            GraphElementVar.GetLocalVariables(variables);
            if(VisitedFlagExpression != null)
                VisitedFlagExpression.GetLocalVariables(variables, constructors);
        }

        public override string Symbol
        {
            get { return GraphElementVar.Name + ".visited" + (VisitedFlagExpression != null ? ("[" + VisitedFlagExpression.Symbol + "]") : ""); }
        }

        public override IEnumerable<SequenceComputation> Children
        {
            get { if(VisitedFlagExpression != null) yield return VisitedFlagExpression; }
        }
    }
}
