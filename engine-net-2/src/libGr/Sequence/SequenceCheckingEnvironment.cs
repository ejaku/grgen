/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Environment for sequence (esp. type) checking giving access to model and action signatures.
    /// Abstract base class, there are two concrete subclasses, one for interpreted, one for compiled sequences.
    /// </summary>
    public abstract class SequenceCheckingEnvironment
    {
        /// <summary>
        /// The model giving access to graph element types for checking.
        /// </summary>
        public abstract IGraphModel Model { get; }

        /// <summary>
        /// Helper for checking rule calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqRuleCall">The rule call to check</param>
        public void CheckRuleCall(SequenceRuleCall seqRuleCall)
        {
            RuleInvocation ruleInvocation = seqRuleCall.RuleInvocation;

            // check the name against the available names, "resolves" them as needed (pre- and context packages to packages)
            if(!IsCalledEntityExisting(ruleInvocation, null))
                throw new SequenceParserException(ruleInvocation, -1, SequenceParserError.UnknownRuleOrSequence);

            CheckInputParameters(ruleInvocation, seqRuleCall.ArgumentExpressions, null);
            if(seqRuleCall.IsRuleForMultiRuleAllCallReturningArrays)
                CheckOutputParametersRuleAll(ruleInvocation, seqRuleCall.ReturnVars);
            else
                CheckOutputParameters(ruleInvocation, seqRuleCall.ReturnVars, null);

            CheckFilterCalls(seqRuleCall, ruleInvocation);

            CheckSubgraph(ruleInvocation);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking rule all calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqRuleAllCall">The rule all call to check</param>
        public void CheckRuleAllCall(SequenceRuleAllCall seqRuleAllCall)
        {
            RuleInvocation ruleInvocation = seqRuleAllCall.RuleInvocation;

            // check the name against the available names, "resolves" them as needed (pre- and context packages to packages)
            if(!IsCalledEntityExisting(ruleInvocation, null))
                throw new SequenceParserException(ruleInvocation, -1, SequenceParserError.UnknownRuleOrSequence);

            CheckInputParameters(ruleInvocation, seqRuleAllCall.ArgumentExpressions, null);
            CheckOutputParametersRuleAll(ruleInvocation, seqRuleAllCall.ReturnVars);

            CheckFilterCalls(seqRuleAllCall, ruleInvocation);

            CheckSubgraph(ruleInvocation);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking sequence calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqSeqCall">The sequence call to check</param>
        public void CheckSequenceCall(SequenceSequenceCall seqSeqCall)
        {
            SequenceInvocation sequenceInvocation = seqSeqCall.SequenceInvocation;

            // check the name against the available names, "resolves" them as needed (pre- and context packages to packages)
            if(!IsCalledEntityExisting(sequenceInvocation, null))
                throw new SequenceParserException(sequenceInvocation, -1, SequenceParserError.UnknownRuleOrSequence);

            CheckInputParameters(sequenceInvocation, seqSeqCall.ArgumentExpressions, null);
            CheckOutputParameters(sequenceInvocation, seqSeqCall.ReturnVars, null);

            CheckSubgraph(sequenceInvocation);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqCompProcCall">The procedure call to check</param>
        public void CheckProcedureCall(SequenceComputationProcedureCall seqCompProcCall)
        {
            CheckProcedureCallBase(seqCompProcCall, null);
        }

        /// <summary>
        /// Helper for checking procedure method calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqCompProcMethodCall">The procedure method call to check</param>
        /// <param name="targetExpr">The target of the procedure method call</param>
        public void CheckProcedureMethodCall(SequenceExpression targetExpr, SequenceComputationProcedureMethodCall seqCompProcMethodCall)
        {
            if(targetExpr.Type(this) == "")
            {
                // only runtime checks possible (we could check whether the called procedure signature exists in at least one of the model types, if not it's a type error, can't work at runtime, but that kind of negative check is not worth the effort)
                return;
            }

            GrGenType ownerType = TypesHelper.GetNodeOrEdgeType(targetExpr.Type(this), Model);
            if(ownerType == null)
            {
                // error, must be node or edge type
                throw new SequenceParserException(targetExpr.Type(this), SequenceParserError.UserMethodsOnlyAvailableForGraphElements);
            }
            
            CheckProcedureCallBase(seqCompProcMethodCall, ownerType);
        }

        /// <summary>
        /// Helper for checking procedure method calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqCompProcMethodCall">The procedure method call to check</param>
        /// <param name="targetVar">The target of the procedure method call</param>
        public void CheckProcedureMethodCall(SequenceVariable targetVar, SequenceComputationProcedureMethodCall seqCompProcMethodCall)
        {
            if(targetVar.Type == "")
            {
                // only runtime checks possible (we could check whether the called procedure signature exists in at least one of the model types, if not it's a type error, can't work at runtime, but that kind of negative check is not worth the effort)
                return;
            }

            GrGenType ownerType = TypesHelper.GetNodeOrEdgeType(targetVar.Type, Model);
            if(ownerType == null)
            {
                // error, must be node or edge type
                throw new SequenceParserException(targetVar.Type, SequenceParserError.UserMethodsOnlyAvailableForGraphElements);
            }

            CheckProcedureCallBase(seqCompProcMethodCall, ownerType);
        }

        /// <summary>
        /// Helper for checking function calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The function call to check</param>
        public void CheckFunctionCall(SequenceExpressionFunctionCall seq)
        {
            CheckFunctionCallBase(seq, null);
        }

        /// <summary>
        /// Helper for checking function method calls.
        /// Checks whether called entity exists, and type checks the input.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqExprFuncMethodCall">The function method call to check</param>
        /// <param name="targetExpr">The target of the procedure function call</param>
        public void CheckFunctionMethodCall(SequenceExpression targetExpr, SequenceExpressionFunctionMethodCall seqExprFuncMethodCall)
        {
            if(targetExpr.Type(this) == "")
            {
                // only runtime checks possible (we could check whether the called procedure signature exists in at least one of the model types, if not it's a type error, can't work at runtime, but that kind of negative check is not worth the effort)
                return;
            }

            GrGenType ownerType = TypesHelper.GetNodeOrEdgeType(targetExpr.Type(this), Model);
            if(ownerType == null)
            {
                // error, must be node or edge type
                throw new SequenceParserException(targetExpr.Type(this), SequenceParserError.UserMethodsOnlyAvailableForGraphElements);
            }

            CheckFunctionCallBase(seqExprFuncMethodCall, ownerType);
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqCompProcCall">The procedure call to check</param>
        /// <param name="ownerType">Gives the owner type of the procedure method call, in case this is a method call, otherwise null</param>
        private void CheckProcedureCallBase(SequenceComputationProcedureCall seqCompProcCall, GrGenType ownerType)
        {
            ProcedureInvocation procedureInvocation = seqCompProcCall.ProcedureInvocation;

            // check the name against the available names
            if(!IsCalledEntityExisting(procedureInvocation, ownerType))
                throw new SequenceParserException(procedureInvocation, -1, SequenceParserError.UnknownProcedure);

            CheckInputParameters(procedureInvocation, seqCompProcCall.ArgumentExpressions, ownerType);
            CheckOutputParameters(procedureInvocation, seqCompProcCall.ReturnVars, ownerType);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking function calls.
        /// Checks whether called entity exists, and type checks the input.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqExprFuncCall">The function call to check</param>
        /// <param name="ownerType">Gives the owner type of the function method call, in case this is a method call, otherwise null</param>
        private void CheckFunctionCallBase(SequenceExpressionFunctionCall seqExprFuncCall, GrGenType ownerType)
        {
            FunctionInvocation functionInvocation = seqExprFuncCall.FunctionInvocation;

            // check the name against the available names
            if(!IsCalledEntityExisting(functionInvocation, ownerType))
                throw new SequenceParserException(functionInvocation, -1, SequenceParserError.UnknownFunction);

            CheckInputParameters(functionInvocation, seqExprFuncCall.ArgumentExpressions, ownerType);

            // ok, this is a well-formed invocation
        }

        private void CheckInputParameters(Invocation invocation, SequenceExpression[] ArgumentExpressions, GrGenType ownerType)
        {
            // Check whether number of parameters and return parameters match
            if(NumInputParameters(invocation, ownerType) != ArgumentExpressions.Length)
                throw new SequenceParserException(invocation, ArgumentExpressions.Length, SequenceParserError.BadNumberOfParameters);

            // Check parameter types
            for(int i = 0; i < ArgumentExpressions.Length; i++)
            {
                ArgumentExpressions[i].Check(this);

                if(!TypesHelper.IsSameOrSubtype(ArgumentExpressions[i].Type(this), InputParameterType(i, invocation, ownerType), Model))
                    throw new SequenceParserException(invocation, -1, SequenceParserError.BadParameter, i);
            }
        }

        private void CheckOutputParameters(Invocation invocation, SequenceVariable[] ReturnVars, GrGenType ownerType)
        {
            // Check whether number of parameters and return parameters match
            if(ReturnVars.Length != 0 && NumOutputParameters(invocation, ownerType) != ReturnVars.Length)
                throw new SequenceParserException(invocation, ReturnVars.Length, SequenceParserError.BadNumberOfReturnParameters);

            // Check return types
            for(int i = 0; i < ReturnVars.Length; ++i)
            {
                if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, invocation, ownerType), ReturnVars[i].Type, Model))
                    throw new SequenceParserException(invocation, -1, SequenceParserError.BadReturnParameter, i);
            }
        }

        private void CheckOutputParametersRuleAll(Invocation invocation, SequenceVariable[] ReturnVars)
        {
            // Check whether number of parameters and return parameters match
            if(ReturnVars.Length != 0 && NumOutputParameters(invocation, null) != ReturnVars.Length)
                throw new SequenceParserException(invocation, ReturnVars.Length, SequenceParserError.BadNumberOfReturnParameters);

            // Check return types
            for(int i = 0; i < ReturnVars.Length; ++i)
            {
                if(ReturnVars[i].Type != "")
                {
                    if(!ReturnVars[i].Type.StartsWith("array<"))
                    {
                        Console.Error.WriteLine("An all call expects all return parameters T in an array<T>");
                        throw new SequenceParserException(invocation, -1, SequenceParserError.BadReturnParameter, i);
                    }
                    if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, invocation, null), ReturnVars[i].Type.Substring(6, ReturnVars[i].Type.Length - 7), Model))
                    {
                        Console.Error.WriteLine("The arrays of the all call are inconsemurable in their value types");
                        throw new SequenceParserException(invocation, -1, SequenceParserError.BadReturnParameter, i);
                    }
                }
            }
        }

        private void CheckFilterCalls(SequenceRuleCall seqRuleCall, Invocation invocation)
        {
            foreach(FilterCall filterCall in seqRuleCall.Filters)
            {
                if(!IsFilterExisting(filterCall, seqRuleCall))
                    throw new SequenceParserException(invocation.PackagePrefixedName ?? invocation.Name, filterCall.PackagePrefixedName ?? filterCall.Name, SequenceParserError.FilterError);

                // Check whether number of filter parameters match
                if(NumFilterFunctionParameters(filterCall, seqRuleCall) != filterCall.ArgumentExpressions.Length)
                    throw new SequenceParserException(invocation.Name, filterCall.Name, SequenceParserError.FilterParameterError);

                // Check parameter types
                for(int i = 0; i < filterCall.ArgumentExpressions.Length; i++)
                {
                    filterCall.ArgumentExpressions[i].Check(this);

                    if(filterCall.ArgumentExpressions[i] != null)
                    {
                        if(!TypesHelper.IsSameOrSubtype(filterCall.ArgumentExpressions[i].Type(this), FilterFunctionParameterType(i, filterCall, seqRuleCall), Model))
                            throw new SequenceParserException(invocation.Name, filterCall.Name, SequenceParserError.FilterParameterError);
                    }
                    else
                    {
                        if(filterCall.Arguments[i] != null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(filterCall.Arguments[i], Model), FilterFunctionParameterType(i, filterCall, seqRuleCall), Model))
                            throw new SequenceParserException(invocation.Name, filterCall.Name, SequenceParserError.FilterParameterError);
                    }
                }
            }
        }

        public void CheckFilterCalls(SequenceMultiRuleAllCall seqMultiRuleAllCall)
        {
            foreach(FilterCall filterCall in seqMultiRuleAllCall.Filters)
            {
                if(filterCall.MatchClassName == null || !IsMatchClassExisting(filterCall))
                    throw new SequenceParserException(seqMultiRuleAllCall.Symbol, filterCall.PackagePrefixedName ?? filterCall.Name, SequenceParserError.MatchClassError);

                String suggestion;
                if(!IsFilterExisting(filterCall, seqMultiRuleAllCall, out suggestion))
                    throw new SequenceParserException(filterCall.MatchClassPackagePrefixedName ?? filterCall.MatchClassName, filterCall.PackagePrefixedName ?? filterCall.Name, SequenceParserError.FilterError, suggestion);

                // Check whether number of filter parameters match
                if(NumFilterFunctionParameters(filterCall, seqMultiRuleAllCall) != filterCall.ArgumentExpressions.Length)
                    throw new SequenceParserException(filterCall.MatchClassName, filterCall.Name, SequenceParserError.FilterParameterError);

                // Check parameter types
                for(int i = 0; i < filterCall.ArgumentExpressions.Length; i++)
                {
                    filterCall.ArgumentExpressions[i].Check(this);

                    if(filterCall.ArgumentExpressions[i] != null)
                    {
                        if(!TypesHelper.IsSameOrSubtype(filterCall.ArgumentExpressions[i].Type(this), FilterFunctionParameterType(i, filterCall, seqMultiRuleAllCall), Model))
                            throw new SequenceParserException(filterCall.MatchClassName, filterCall.Name, SequenceParserError.FilterParameterError);
                    }
                    else
                    {
                        if(filterCall.Arguments[i] != null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(filterCall.Arguments[i], Model), FilterFunctionParameterType(i, filterCall, seqMultiRuleAllCall), Model))
                            throw new SequenceParserException(filterCall.MatchClassName, filterCall.Name, SequenceParserError.FilterParameterError);
                    }
                }
            }
        }

        private void CheckSubgraph(Invocation invocation)
        {
            SequenceVariable subgraph;
            if(invocation is RuleInvocation)
                subgraph = ((RuleInvocation)invocation).Subgraph;
            else
                subgraph = ((SequenceInvocation)invocation).Subgraph;
            if(subgraph != null && !TypesHelper.IsSameOrSubtype("graph", subgraph.Type, Model))
                throw new SequenceParserException(invocation.Name, subgraph.Type, SequenceParserError.SubgraphTypeError);
        }

        public abstract bool IsFunctionCallExternal(FunctionInvocation functionInvocation);
        public abstract bool IsProcedureCallExternal(ProcedureInvocation procedureInvocation);

        /// <summary>
        /// Helper which returns the type of the given top level entity of the given rule.
        /// Throws an exception in case the rule of the given name does not exist 
        /// or in case it does not contain an entity of the given name.
        /// </summary>
        public abstract string TypeOfTopLevelEntityInRule(string ruleName, string entityName);

        protected abstract bool IsCalledEntityExisting(Invocation invocation, GrGenType ownerType);
        protected abstract int NumInputParameters(Invocation invocation, GrGenType ownerType);
        protected abstract int NumOutputParameters(Invocation invocation, GrGenType ownerType);
        protected abstract string InputParameterType(int i, Invocation invocation, GrGenType ownerType);
        protected abstract string OutputParameterType(int i, Invocation invocation, GrGenType ownerType);
        protected abstract bool IsFilterExisting(FilterCall filterCall, SequenceRuleCall seq);
        protected abstract int NumFilterFunctionParameters(FilterCall filterCall, SequenceRuleCall seq);
        protected abstract string FilterFunctionParameterType(int i, FilterCall filterCall, SequenceRuleCall seq);
        protected abstract bool IsMatchClassExisting(FilterCall filterCall);
        protected abstract bool IsFilterExisting(FilterCall filterCall, SequenceMultiRuleAllCall seq, out string suggestion);
        protected abstract int NumFilterFunctionParameters(FilterCall filterCall, SequenceMultiRuleAllCall seq);
        protected abstract string FilterFunctionParameterType(int i, FilterCall filterCall, SequenceMultiRuleAllCall seq);
    }
}
