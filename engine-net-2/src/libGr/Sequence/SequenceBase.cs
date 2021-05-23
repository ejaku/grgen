/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A sequence construct that allows to match patterns (most also allow to rewrite them).
    /// Utilized in begin and end events to tell about the construct that started or ended;
    /// to be type checked against the concrete construct as needed,
    /// e.g. SequenceExpressionRuleQuery or SequenceMultiRuleAllCall from interpreted sequences,
    /// or PatternMatchingConstruct from compiled sequences.
    /// </summary>
    public interface IPatternMatchingConstruct
    {
        /// <summary>
        /// A string symbol representing this sequence /expression kind.
        /// </summary>
        String Symbol { get; }
    }

    /// <summary>
    /// A compiled sequence construct that allows to match patterns (most also allow to rewrite them).
    /// Utilized in begin and end events to tell about the construct that started or ended.
    /// </summary>
    public class PatternMatchingConstruct : IPatternMatchingConstruct
    {
        public PatternMatchingConstruct(String symbol)
        {
            this.symbol = symbol;
        }

        public String Symbol { get { return symbol; } }

        String symbol;
    }

    /// <summary>
    /// The common base of sequence, sequence computation, and sequence expression objects,
    /// with some common infrastructure.
    /// </summary>
    public abstract class SequenceBase
    {
        /// <summary>
        /// Initializes a new SequenceBase object (sets the id).
        /// </summary>
        protected SequenceBase()
        {
            id = idSource;
            ++idSource;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The sequence base to be copied.</param>
        protected SequenceBase(SequenceBase that)
        {
            id = that.id;
        }

        public abstract bool HasSequenceType(SequenceType sequenceType);

        /// <summary>
        /// Checks the sequence /expression for errors utilizing the given checking environment
        /// reports them by exception
        /// </summary>s
        public abstract void Check(SequenceCheckingEnvironment env);

        /// <summary>
        /// Returns the type of the sequence /expression (for sequences always "boolean")
        /// </summary>
        public abstract string Type(SequenceCheckingEnvironment env);
        
        /// <summary>
        /// A common random number generator for all sequence /expression objects.
        /// It uses a time-dependent seed.
        /// </summary>
        public static Random randomGenerator = new Random();

        /// <summary>
        /// The precedence of this operator. Zero is the highest priority, int.MaxValue the lowest.
        /// Used to add needed parentheses for printing sequences /expressions
        /// TODO: WTF? das ist im Parser genau umgekehrt implementiert!
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// A string symbol representing this sequence /expression kind.
        /// </summary>
        public abstract String Symbol { get; }

        /// <summary>
        /// returns the sequence /expresion id - every sequence /expression is assigned a unique id used in xgrs code generation
        /// for copies the old id is just taken over, does not cause problems as code is only generated once per defined sequence
        /// </summary>
        public int Id { get { return id; } }

        /// <summary>
        /// stores the sequence /expression unique id
        /// </summary>
        protected readonly int id;

        /// <summary>
        /// the static member used to assign the unique ids to the sequence /expression instances
        /// </summary>
        protected static int idSource = 0;

        /// <summary>
        /// Enumerates all child sequence computation objects
        /// </summary>
        public abstract IEnumerable<SequenceBase> ChildrenBase { get; }

        /// <summary>
        /// sets for the very node the profiling flag (does not recurse)
        /// </summary>
        public virtual void SetNeedForProfiling(bool profiling)
        {
            // NOP, sufficient for most sequences / sequence computations / sequence expressions,
            // only the node/edge/incident/adjacent/reachable/isX-constructs need to call a special version
            // counting up the search steps with each visited element/graph element accessed (but not the implicit operations)
        }

        /// <summary>
        /// sets for the node and all children, i.e. the entire tree the profiling flag
        /// </summary>
        public void SetNeedForProfilingRecursive(bool profiling)
        {
            SetNeedForProfiling(true);
            foreach(SequenceBase child in ChildrenBase)
            {
                child.SetNeedForProfilingRecursive(profiling);
            }
        }

        /// <summary>
        /// the state of executing this sequence base
        /// (a sequence base comprises sequences, sequence computations, sequence expressions)
        /// </summary>
        public SequenceExecutionState ExecutionState
        {
            get { return executionState; }
        }

        /// <summary>
        /// the state of executing this sequence base, implementation
        /// </summary>
        internal SequenceExecutionState executionState;

        /// <summary>
        /// Walks the sequence tree from this on to the given target sequence base (inclusive),
        /// collecting all variables found on the way into the variables dictionary,
        /// and all container and object type constructors used into the constructors array.
        /// </summary>
        /// <param name="variables">Contains the variables found</param>
        /// <param name="constructors">Contains the constructors walked by</param>
        /// <param name="target">The target sequence base up to which to walk</param>
        /// <returns>Returns whether the target was hit, so the parent can abort walking</returns>
        public virtual bool GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionConstructor> constructors, SequenceBase target)
        {
            return this == target;
        }

        #region helper methods

        public static IAction GetAction(RuleInvocation invocation)
        {
            SequenceRuleCallInterpreted sequenceRuleCall = invocation as SequenceRuleCallInterpreted;
            if(sequenceRuleCall != null)
                return sequenceRuleCall.Action;
            SequenceRuleAllCallInterpreted sequenceRuleAllCall = invocation as SequenceRuleAllCallInterpreted;
            if(sequenceRuleAllCall != null)
                return sequenceRuleAllCall.Action;
            SequenceRuleCountAllCallInterpreted sequenceRuleCountAllCall = invocation as SequenceRuleCountAllCallInterpreted;
            if(sequenceRuleCountAllCall != null)
                return sequenceRuleCountAllCall.Action;
            return null;
        }

        protected static NodeType GetNodeType(IGraphProcessingEnvironment procEnv, SequenceExpression nodeTypeExpr, string functionName)
        {
            NodeType nodeType = null;

            if(nodeTypeExpr != null) // often adjacent node
            {
                object tmp = nodeTypeExpr.Evaluate(procEnv);
                if(tmp is string)
                    nodeType = procEnv.Graph.Model.NodeModel.GetType((string)tmp);
                else if(tmp is NodeType)
                    nodeType = (NodeType)tmp;
                if(nodeType == null)
                    throw new Exception("node type argument to " + functionName + " is not a node type");
            }
            else
                nodeType = procEnv.Graph.Model.NodeModel.RootType;

            return nodeType;
        }

        protected static EdgeType GetEdgeType(IGraphProcessingEnvironment procEnv, SequenceExpression edgeTypeExpr, string functionName)
        {
            EdgeType edgeType = null;

            if(edgeTypeExpr != null) // often incident edge
            {
                object tmp = edgeTypeExpr.Evaluate(procEnv);
                if(tmp is string)
                    edgeType = procEnv.Graph.Model.EdgeModel.GetType((string)tmp);
                else if(tmp is EdgeType)
                    edgeType = (EdgeType)tmp;
                if(edgeType == null)
                    throw new Exception("edge type argument to " + functionName + " is not an edge type");
            }
            else
                edgeType = procEnv.Graph.Model.EdgeModel.RootType;

            return edgeType;
        }

        protected void CheckNodeTypeIsKnown(SequenceCheckingEnvironment env, SequenceExpression typeExpr, String whichArgument)
        {
            if(typeExpr == null || typeExpr.Type(env) == "")
                return;

            string typeString = GetTypeString(env, typeExpr);

            if(TypesHelper.GetNodeType(typeString, env.Model) == null && typeString != null)
                throw new SequenceParserException(Symbol + whichArgument, "node type or string denoting node type", typeString);
        }

        protected void CheckEdgeTypeIsKnown(SequenceCheckingEnvironment env, SequenceExpression typeExpr, String whichArgument)
        {
            if(typeExpr == null || typeExpr.Type(env) == "")
                return;

            string typeString = GetTypeString(env, typeExpr);

            if(TypesHelper.GetEdgeType(typeString, env.Model) == null && typeString != null)
                throw new SequenceParserException(Symbol + whichArgument, "edge type or string denoting edge type", typeString);
        }

        protected void CheckGraphElementTypeIsKnown(SequenceCheckingEnvironment env, SequenceExpression typeExpr, String whichArgument)
        {
            if(typeExpr == null || typeExpr.Type(env) == "")
                return;

            string typeString = GetTypeString(env, typeExpr);

            if(TypesHelper.GetNodeType(typeString, env.Model) == null && typeString != null)
            {
                if(TypesHelper.GetEdgeType(typeString, env.Model) == null && typeString != null)
                    throw new SequenceParserException(Symbol + whichArgument, "node or edge type or string denoting node or edge type", typeString);
            }
        }

        protected void CheckBaseObjectTypeIsKnown(SequenceCheckingEnvironment env, String baseObjectType, String whichArgument)
        {
            if(TypesHelper.GetObjectType(baseObjectType, env.Model) == null && TypesHelper.GetTransientObjectType(baseObjectType, env.Model) == null)
                throw new SequenceParserException(Symbol + whichArgument, "object type or transient object type", baseObjectType);
        }

        protected string GetTypeString(SequenceCheckingEnvironment env, SequenceExpression typeExpr)
        {
            if(typeExpr.Type(env) == "string")
            {
                if(typeExpr is SequenceExpressionConstant)
                    return (string)((SequenceExpressionConstant)typeExpr).Constant;
            }
            else
                return typeExpr.Type(env);

            return null;
        }

        protected static void FillArgumentsFromArgumentExpressions(SequenceExpression[] ArgumentExpressions, object[] Arguments, IGraphProcessingEnvironment procEnv)
        {
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
            {
                Arguments[i] = ArgumentExpressions[i].Evaluate(procEnv);
            }
        }

        protected static void FillReturnVariablesFromValues(SequenceVariable[] ReturnVars, IAction Action, IGraphProcessingEnvironment procEnv, List<object[]> retElemsList, int which)
        {
            if(which == -1)
            {
                FillReturnVariablesFromValues(ReturnVars, Action, procEnv, retElemsList);
            }
            else
            {
                object[] retElems = retElemsList[0];
                FillReturnVariablesFromValues(ReturnVars, procEnv, retElems);
            }
        }

        protected static void FillReturnVariablesFromValues(SequenceVariable[] ReturnVars, IAction Action, IGraphProcessingEnvironment procEnv, List<object[]> retElemsList)
        {
            IList[] returnVars = null;
            if(ReturnVars.Length > 0)
            {
                returnVars = new IList[ReturnVars.Length];
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    returnVars[i] = ReturnVars[i].GetVariableValue(procEnv) as IList;
                    if(returnVars[i] == null)
                    {
                        string returnType = TypesHelper.DotNetTypeToXgrsType(Action.RulePattern.Outputs[i]);
                        Type valueType = TypesHelper.GetType(returnType, procEnv.Graph.Model);
                        returnVars[i] = ContainerHelper.NewList(valueType);
                        ReturnVars[i].SetVariableValue(returnVars[i], procEnv);
                    }
                    else
                        returnVars[i].Clear();
                }
            }
            for(int curRetElemNum = 0; curRetElemNum < retElemsList.Count; ++curRetElemNum)
            {
                object[] retElems = retElemsList[curRetElemNum];
                for(int i = 0; i < ReturnVars.Length; ++i)
                {
                    returnVars[i].Add(retElems[i]);
                }
            }
        }

        protected static void FillReturnVariablesFromValues(SequenceVariable[] ReturnVars, IGraphProcessingEnvironment procEnv, object[] retElems)
        {
            for(int i = 0; i < ReturnVars.Length; ++i)
            {
                ReturnVars[i].SetVariableValue(retElems[i], procEnv);
            }
        }

        protected static void InitializeArgumentExpressionsAndArguments(List<SequenceExpression> argExprs,
            out SequenceExpression[] ArgumentExpressions, out object[] Arguments)
        {
            foreach(SequenceExpression argExpr in argExprs)
            {
                if(argExpr == null)
                    throw new Exception("Null entry in argument expressions");
            }
            ArgumentExpressions = argExprs.ToArray();
            Arguments = new object[ArgumentExpressions.Length];
        }

        protected static void InitializeReturnVariables(List<SequenceVariable> returnVars,
            out SequenceVariable[] ReturnVars)
        {
            foreach(SequenceVariable returnVar in returnVars)
            {
                if(returnVar == null)
                    throw new Exception("Null entry in return variables");
            }
            ReturnVars = returnVars.ToArray();
        }

        protected static void CopyArgumentExpressionsAndArguments(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv,
            SequenceExpression[] sourceArgumentExpressions, 
            out SequenceExpression[] targetArgumentExpressions, out object[] targetArguments)
        {
            targetArgumentExpressions = new SequenceExpression[sourceArgumentExpressions.Length];
            for(int i = 0; i < sourceArgumentExpressions.Length; ++i)
            {
                targetArgumentExpressions[i] = sourceArgumentExpressions[i].CopyExpression(originalToCopy, procEnv);
            }
            targetArguments = new object[targetArgumentExpressions.Length];
        }

        // typically used for ReturnVars
        protected static void CopyVars(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv,
            SequenceVariable[] sourceVars,
            out SequenceVariable[] targetVars)
        {
            targetVars = new SequenceVariable[sourceVars.Length];
            for(int i = 0; i < sourceVars.Length; ++i)
            {
                targetVars[i] = sourceVars[i].Copy(originalToCopy, procEnv);
            }
        }

        protected static List<SequenceVariable> CopyVars(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv,
            List<SequenceVariable> sourceVars)
        {
            List<SequenceVariable> targetVars = new List<SequenceVariable>(sourceVars.Count);
            foreach(SequenceVariable sourceVar in sourceVars)
            {
                targetVars.Add(sourceVar.Copy(originalToCopy, procEnv));
            }
            return targetVars;
        }

        protected static void GetLocalVariables(SequenceExpression[] ArgumentExpressions, 
            Dictionary<SequenceVariable, SetValueType> variables, List<SequenceExpressionConstructor> constructors)
        {
            foreach(SequenceExpression seqExpr in ArgumentExpressions)
            {
                seqExpr.GetLocalVariables(variables, constructors);
            }
        }

        protected static void GetLocalVariables(SequenceVariable[] ReturnVars, 
            Dictionary<SequenceVariable, SetValueType> variables, List<SequenceExpressionConstructor> constructors)
        {
            foreach(SequenceVariable seqVar in ReturnVars)
            {
                seqVar.GetLocalVariables(variables);
            }
        }

        protected static void GetLocalVariables(List<SequenceFilterCallBase> filterCalls,
            Dictionary<SequenceVariable, SetValueType> variables, List<SequenceExpressionConstructor> constructors)
        {
            foreach(SequenceFilterCallBase filterCall in filterCalls)
            {
                if(filterCall is SequenceFilterCall)
                    GetLocalVariables((SequenceFilterCall)filterCall, variables, constructors);
                else if(filterCall is SequenceFilterCallLambdaExpression)
                    GetLocalVariables((SequenceFilterCallLambdaExpression)filterCall, variables, constructors);
            }
        }

        protected static void GetLocalVariables(SequenceFilterCall filterCall,
            Dictionary<SequenceVariable, SetValueType> variables, List<SequenceExpressionConstructor> constructors)
        {
            foreach(SequenceExpression expr in filterCall.ArgumentExpressions)
            {
                expr.GetLocalVariables(variables, constructors);
            }
        }

        protected static void GetLocalVariables(SequenceFilterCallLambdaExpression filterCall,
            Dictionary<SequenceVariable, SetValueType> variables, List<SequenceExpressionConstructor> constructors)
        {
            if(filterCall.FilterCall.initExpression != null)
                filterCall.FilterCall.initExpression.GetLocalVariables(variables, constructors);
            filterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors);
            if(filterCall.FilterCall.previousAccumulationAccess != null)
                variables.Remove(filterCall.FilterCall.previousAccumulationAccess);
            if(filterCall.FilterCall.arrayAccess != null)
                variables.Remove(filterCall.FilterCall.arrayAccess);
            if(filterCall.FilterCall.index != null)
                variables.Remove(filterCall.FilterCall.index);
            variables.Remove(filterCall.FilterCall.element);
        }

        protected static void RemoveVariablesFallingOutOfScope(Dictionary<SequenceVariable, SetValueType> variables, List<SequenceVariable> variablesFallingOutOfScope)
        {
            foreach(SequenceVariable seqVar in variablesFallingOutOfScope)
            {
                variables.Remove(seqVar);
            }
        }

        #endregion helper methods
    }
}
