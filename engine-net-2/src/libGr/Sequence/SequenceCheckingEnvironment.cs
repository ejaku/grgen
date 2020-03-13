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
    /// Environment for sequence type checking (with/giving access to model and action signatures).
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
        /// Type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqRuleCall">The rule call to check</param>
        public void CheckRuleCall(SequenceRuleCall seqRuleCall)
        {
            CheckInputParameters(seqRuleCall, seqRuleCall.ArgumentExpressions, null);
            if(seqRuleCall.IsRuleForMultiRuleAllCallReturningArrays)
                CheckOutputParametersRuleAll(seqRuleCall, seqRuleCall.ReturnVars);
            else
                CheckOutputParameters(seqRuleCall, seqRuleCall.ReturnVars, null);

            CheckFilterCalls(seqRuleCall, seqRuleCall);

            CheckSubgraph(seqRuleCall);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking rule all calls.
        /// Type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqRuleAllCall">The rule all call to check</param>
        public void CheckRuleAllCall(SequenceRuleAllCall seqRuleAllCall)
        {
            CheckInputParameters(seqRuleAllCall, seqRuleAllCall.ArgumentExpressions, null);
            CheckOutputParametersRuleAll(seqRuleAllCall, seqRuleAllCall.ReturnVars);

            CheckFilterCalls(seqRuleAllCall, seqRuleAllCall);

            CheckSubgraph(seqRuleAllCall);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking sequence calls.
        /// Type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqSeqCall">The sequence call to check</param>
        public void CheckSequenceCall(SequenceSequenceCall seqSeqCall)
        {
            CheckInputParameters(seqSeqCall, seqSeqCall.ArgumentExpressions, null);
            CheckOutputParameters(seqSeqCall, seqSeqCall.ReturnVars, null);

            CheckSubgraph(seqSeqCall);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Type checks the input, type checks the output.
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

            if(ownerType.GetProcedureMethod(seqCompProcMethodCall.Name) == null)
                throw new SequenceParserException(seqCompProcMethodCall, -1, SequenceParserError.UnknownProcedure);

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

            // check whether called procedure method exists
            if(ownerType.GetProcedureMethod(seqCompProcMethodCall.Name) == null)
                throw new SequenceParserException(seqCompProcMethodCall, -1, SequenceParserError.UnknownProcedure);

            CheckProcedureCallBase(seqCompProcMethodCall, ownerType);
        }

        /// <summary>
        /// Helper for checking function calls.
        /// Type checks the input, type checks the output.
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

            // check whether called function method exists
            if(ownerType.GetFunctionMethod(seqExprFuncMethodCall.Name) == null)
                throw new SequenceParserException(seqExprFuncMethodCall, -1, SequenceParserError.UnknownProcedure);

            CheckFunctionCallBase(seqExprFuncMethodCall, ownerType);
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqCompProcCall">The procedure call to check</param>
        /// <param name="ownerType">Gives the owner type of the procedure method call, in case this is a method call, otherwise null</param>
        private void CheckProcedureCallBase(SequenceComputationProcedureCall seqCompProcCall, GrGenType ownerType)
        {
            CheckInputParameters(seqCompProcCall, seqCompProcCall.ArgumentExpressions, ownerType);
            CheckOutputParameters(seqCompProcCall, seqCompProcCall.ReturnVars, ownerType);

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking function calls.
        /// Type checks the input.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqExprFuncCall">The function call to check</param>
        /// <param name="ownerType">Gives the owner type of the function method call, in case this is a method call, otherwise null</param>
        private void CheckFunctionCallBase(SequenceExpressionFunctionCall seqExprFuncCall, GrGenType ownerType)
        {
            CheckInputParameters(seqExprFuncCall, seqExprFuncCall.ArgumentExpressions, ownerType);

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

        /// <summary>
        /// Checks whether called filter exists, and type checks the inputs.
        /// </summary>
        private void CheckFilterCalls(SequenceRuleCall seqRuleCall, Invocation invocation)
        {
            foreach(SequenceFilterCall sequenceFilterCall in seqRuleCall.Filters)
            {
                String filterCallName = GetFilterCallName(sequenceFilterCall);

                if(!IsFilterExisting(sequenceFilterCall))
                    throw new SequenceParserException(invocation.PackagePrefixedName ?? invocation.Name, filterCallName, SequenceParserError.FilterError);

                // Check whether number of filter parameters match
                if(NumFilterFunctionParameters(sequenceFilterCall) != sequenceFilterCall.ArgumentExpressions.Length)
                    throw new SequenceParserException(invocation.Name, filterCallName, SequenceParserError.FilterParameterError);

                // Check parameter types
                for(int i = 0; i < sequenceFilterCall.ArgumentExpressions.Length; i++)
                {
                    sequenceFilterCall.ArgumentExpressions[i].Check(this);

                    if(!TypesHelper.IsSameOrSubtype(sequenceFilterCall.ArgumentExpressions[i].Type(this), FilterFunctionParameterType(i, sequenceFilterCall), Model))
                        throw new SequenceParserException(invocation.Name, filterCallName, SequenceParserError.FilterParameterError);
                }
            }
        }

        /// <summary>
        /// Checks whether called match class filter exists, and type checks the inputs.
        /// </summary>
        public void CheckFilterCalls(SequenceMultiRuleAllCall seqMultiRuleAllCall)
        {
            foreach(SequenceFilterCall sequenceFilterCall in seqMultiRuleAllCall.Filters)
            {
                String matchClassName = GetMatchClassName(sequenceFilterCall);
                String filterCallName = GetFilterCallName(sequenceFilterCall);

                if(matchClassName == null || !IsMatchClassExisting(sequenceFilterCall))
                    throw new SequenceParserException(matchClassName, sequenceFilterCall.ToString(), SequenceParserError.MatchClassError);

                if(!IsFilterExisting(sequenceFilterCall))
                    throw new SequenceParserException(matchClassName, filterCallName, SequenceParserError.FilterError);

                // Check whether number of filter parameters match
                if(NumFilterFunctionParameters(sequenceFilterCall) != sequenceFilterCall.ArgumentExpressions.Length)
                    throw new SequenceParserException(matchClassName, filterCallName, SequenceParserError.FilterParameterError);

                // Check parameter types
                for(int i = 0; i < sequenceFilterCall.ArgumentExpressions.Length; i++)
                {
                    sequenceFilterCall.ArgumentExpressions[i].Check(this);

                    if(!TypesHelper.IsSameOrSubtype(sequenceFilterCall.ArgumentExpressions[i].Type(this), FilterFunctionParameterType(i, sequenceFilterCall), Model))
                        throw new SequenceParserException(matchClassName, filterCallName, SequenceParserError.FilterParameterError);
                }
            }
        }

        private bool IsFilterExisting(SequenceFilterCall sequenceFilterCall)
        {
            return sequenceFilterCall.Filter != null;
        }

        private int NumFilterFunctionParameters(SequenceFilterCall sequenceFilterCall)
        {
            if(sequenceFilterCall.Filter is IFilterAutoSupplied)
                return ((IFilterAutoSupplied)sequenceFilterCall.Filter).Inputs.Length;
            if(sequenceFilterCall.Filter is IFilterFunction)
                return ((IFilterFunction)sequenceFilterCall.Filter).Inputs.Length;
            return 0; // auto-generated
        }

        private string FilterFunctionParameterType(int i, SequenceFilterCall sequenceFilterCall)
        {
            if(sequenceFilterCall.Filter is IFilterAutoSupplied)
            {
                IFilterAutoSupplied filterAutoSupplied = (IFilterAutoSupplied)sequenceFilterCall.Filter;
                return TypesHelper.DotNetTypeToXgrsType(filterAutoSupplied.Inputs[i]);
            }
            if(sequenceFilterCall.Filter is IFilterFunction)
            {
                IFilterFunction filterFunction = (IFilterFunction)sequenceFilterCall.Filter;
                return TypesHelper.DotNetTypeToXgrsType(filterFunction.Inputs[i]);
            }
            throw new Exception("Internal error"); // auto-generated
        }

        private string GetFilterCallName(SequenceFilterCall sequenceFilterCall)
        {
            return sequenceFilterCall.Filter.PackagePrefixedName;
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

        /// <summary>
        /// Helper which returns the type of the given top level entity of the given rule.
        /// Throws an exception in case the rule of the given name does not exist 
        /// or in case it does not contain an entity of the given name.
        /// </summary>
        public abstract string TypeOfTopLevelEntityInRule(string ruleName, string entityName);

        protected abstract int NumInputParameters(Invocation invocation, GrGenType ownerType);
        protected abstract int NumOutputParameters(Invocation invocation, GrGenType ownerType);
        protected abstract string InputParameterType(int i, Invocation invocation, GrGenType ownerType);
        protected abstract string OutputParameterType(int i, Invocation invocation, GrGenType ownerType);
        protected abstract bool IsMatchClassExisting(SequenceFilterCall sequenceFilterCall);
        protected abstract string GetMatchClassName(SequenceFilterCall sequenceFilterCall);
    }
}
