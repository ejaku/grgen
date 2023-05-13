/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Collections;

namespace de.unika.ipd.grGen.libGr
{
    public enum PatternMatchingConstructType
    {
        RuleCall,
        RuleAllCall,
        RuleCountAllCall,
        SomeFromSet,
        MultiRuleAllCall,
        RulePrefixedSequence,
        MultiRulePrefixedSequence,
        Backtrack,
        MultiBacktrack,
        MultiSequenceBacktrack,
        ForMatch,
        RuleQuery,
        MultiRuleQuery,
        MappingClause
    }

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

        PatternMatchingConstructType ConstructType { get; }
    }

    /// <summary>
    /// A Sequence Base with a Special flag.
    /// </summary>
    public interface ISequenceSpecial
    {
        /// <summary>
        /// The "Special" flag. Usage is implementation specific.
        /// GrShell uses this flag to indicate breakpoints when in debug mode and
        /// to dump matches when in normal mode.
        /// </summary>
        bool Special { get; set; }
    }

    /// <summary>
    /// A compiled sequence construct that allows to match patterns (most also allow to rewrite them).
    /// Utilized in begin and end events to tell about the construct that started or ended.
    /// </summary>
    public class PatternMatchingConstruct : IPatternMatchingConstruct
    {
        public PatternMatchingConstruct(String symbol, PatternMatchingConstructType constructType)
        {
            this.symbol = symbol;
            this.constructType = constructType;
        }

        public String Symbol { get { return symbol; } }
        public PatternMatchingConstructType ConstructType { get { return constructType; } }

        String symbol;
        PatternMatchingConstructType constructType;
    }

    /// <summary>
    /// States of executing sequence parts: not (yet) executed, execution underway, successful execution, fail execution
    /// </summary>
    public enum SequenceExecutionState
    {
        NotYet, Underway, Success, Fail
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
            executionState = SequenceExecutionState.NotYet;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="that">The sequence base to be copied.</param>
        protected SequenceBase(SequenceBase that)
        {
            id = that.id;
            executionState = SequenceExecutionState.NotYet;
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
        /// Iff true, no debug (mostly action) events are fired during sequence and rule execution (so debugging is not possible).
        /// </summary>
        public static bool noDebugEvents = false;

        /// <summary>
        /// Iff true, in addition to debug events, no attribute change events are fired during sequence and rule execution (thus also transaction/backtracking breaks, as well as recording/replaying).
        /// </summary>
        public static bool noEvents = false;

        public static bool FireDebugEvents { get { return !noDebugEvents && !noEvents; } }

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
        /// Returns whether the potentialChild is contained in this sequence (base).
        /// True if potentialChild is the same as this sequence (base) (so reflexive relation).
        /// </summary>
        /// <param name="potentialChild">The candidate to be checked for containment.</param>
        /// <returns>Returns whether the potentialChild is contained in this sequence (base).</returns>
        public bool Contains(SequenceBase potentialChild)
        {
            if(this == potentialChild)
                return true;
            foreach(SequenceBase child in ChildrenBase)
            {
                if(child.Contains(potentialChild))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// After a sequence definition was replaced by a new one, all references from then on will use the new one,
        /// but the old references are still there and must get replaced.
        /// </summary>
        /// <param name="oldDef">The old definition which is to be replaced</param>
        /// <param name="newDef">The new definition which replaces the old one</param>
        internal virtual void ReplaceSequenceDefinition(SequenceDefinition oldDef, SequenceDefinition newDef)
        {
            foreach(SequenceBase child in ChildrenBase)
            {
                child.ReplaceSequenceDefinition(oldDef, newDef);
            }
        }

        /// <summary>
        /// Returns the innermost sequence base beneath this as root
        /// which gets currently executed (for sequences on call stack this is the call).
        /// A path in the sequence tree gets executed, the innermost is the important one.
        /// </summary>
        /// <returns>The innermost sequence currently executed, or null if there is no such</returns>
        public virtual SequenceBase GetCurrentlyExecutedSequenceBase()
        {
            foreach(SequenceBase child in ChildrenBase)
            {
                if(child.GetCurrentlyExecutedSequenceBase() != null)
                    return child.GetCurrentlyExecutedSequenceBase();
            }
            if(executionState == SequenceExecutionState.Underway)
                return this;
            return null;
        }

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

        public static ISequenceDefinition GetSequence(SequenceInvocation invocation)
        {
            SequenceSequenceCallInterpreted sequenceSequenceCall = invocation as SequenceSequenceCallInterpreted;
            if(sequenceSequenceCall != null)
                return sequenceSequenceCall.SequenceDef;
            return null;
        }

        public static IProcedureDefinition GetProcedure(ProcedureInvocation invocation)
        {
            SequenceComputationProcedureCallInterpreted sequenceComputationProcedureCall = invocation as SequenceComputationProcedureCallInterpreted;
            if(sequenceComputationProcedureCall != null)
                return sequenceComputationProcedureCall.ProcedureDef;
            return null;
        }

        public static IFunctionDefinition GetFunction(FunctionInvocation invocation)
        {
            SequenceExpressionFunctionCallInterpreted sequenceExpressionFunctionCall = invocation as SequenceExpressionFunctionCallInterpreted;
            if(sequenceExpressionFunctionCall != null)
                return sequenceExpressionFunctionCall.FunctionDef;
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
                throw new SequenceParserExceptionTypeMismatch(Symbol + whichArgument, "node type or string denoting node type", typeString);
        }

        protected void CheckEdgeTypeIsKnown(SequenceCheckingEnvironment env, SequenceExpression typeExpr, String whichArgument)
        {
            if(typeExpr == null || typeExpr.Type(env) == "")
                return;

            string typeString = GetTypeString(env, typeExpr);

            if(TypesHelper.GetEdgeType(typeString, env.Model) == null && typeString != null)
                throw new SequenceParserExceptionTypeMismatch(Symbol + whichArgument, "edge type or string denoting edge type", typeString);
        }

        protected void CheckGraphElementTypeIsKnown(SequenceCheckingEnvironment env, SequenceExpression typeExpr, String whichArgument)
        {
            if(typeExpr == null || typeExpr.Type(env) == "")
                return;

            string typeString = GetTypeString(env, typeExpr);

            if(TypesHelper.GetNodeType(typeString, env.Model) == null && typeString != null)
            {
                if(TypesHelper.GetEdgeType(typeString, env.Model) == null && typeString != null)
                    throw new SequenceParserExceptionTypeMismatch(Symbol + whichArgument, "node or edge type or string denoting node or edge type", typeString);
            }
        }

        protected void CheckBaseObjectTypeIsKnown(SequenceCheckingEnvironment env, String baseObjectType, String whichArgument)
        {
            if(TypesHelper.GetObjectType(baseObjectType, env.Model) == null && TypesHelper.GetTransientObjectType(baseObjectType, env.Model) == null)
                throw new SequenceParserExceptionTypeMismatch(Symbol + whichArgument, "object type or transient object type", baseObjectType);
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

        #region event firing methods

        public void FireBeginExecutionEvent(IGraphProcessingEnvironment procEnv)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.BeginExecution((IPatternMatchingConstruct)this);
        }

        public void FireEnteringSequenceEvent(IGraphProcessingEnvironment procEnv)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.EnteringSequence(this);
        }

        public static void FireMatchedBeforeFilteringEvent(IGraphProcessingEnvironment procEnv, IMatches matches)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.MatchedBeforeFiltering(matches);
        }

        public static void FireMatchedBeforeFilteringEvent(IGraphProcessingEnvironment procEnv, IMatches[] matchesArray)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.MatchedBeforeFiltering(matchesArray);
        }

        public static void FireMatchedAfterFilteringEvent(IGraphProcessingEnvironment procEnv, IMatches matches, bool special)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.MatchedAfterFiltering(matches, special);
        }

        public static void FireMatchedAfterFilteringEvent(IGraphProcessingEnvironment procEnv, IMatches[] matchesArray, bool[] specialArray)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.MatchedAfterFiltering(matchesArray, specialArray);
        }

        public static void FireMatchSelectedEvent(IGraphProcessingEnvironment procEnv, IMatch match, bool special, IMatches matches)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.MatchSelected(match, special, matches);
        }

        public static void FireRewritingSelectedMatchEvent(IGraphProcessingEnvironment procEnv)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.RewritingSelectedMatch();
        }

        // void FireSelectedMatchRewrittenEvent(IGraphProcessingEnvironment procEnv) is not needed as the event firing code is only generated by the frontend (part of the rule modify)

        public static void FireFinishedSelectedMatchEvent(IGraphProcessingEnvironment procEnv)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.FinishedSelectedMatch();
        }

        public static void FireFinishedEvent(IGraphProcessingEnvironment procEnv, IMatches matches, bool special)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.Finished(matches, special);
        }

        public static void FireFinishedEvent(IGraphProcessingEnvironment procEnv, IMatches[] matchesArray, bool[] specialArray)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.Finished(matchesArray, specialArray);
        }

        public void FireExitingSequenceEvent(IGraphProcessingEnvironment procEnv)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.ExitingSequence(this);
        }

        public void FireEndExecutionEvent(IGraphProcessingEnvironment procEnv, object result)
        {
            if(noDebugEvents)
                return;
            if(noEvents)
                return;
            procEnv.EndExecution((IPatternMatchingConstruct)this, result);
        }

        #endregion event firing methods
    }
}
