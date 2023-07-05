/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

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

            CheckFilterCalls(seqRuleCall.Name, seqRuleCall.Filters);

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

            CheckFilterCalls(seqRuleAllCall.Name, seqRuleAllCall.Filters);

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
            String targetExprType = targetExpr.Type(this);
            if(targetExprType == "")
            {
                // only runtime checks possible (we could check whether the called procedure signature exists in at least one of the model types, if not it's a type error, can't work at runtime, but that kind of negative check is not worth the effort)
                return;
            }

            InheritanceType ownerType = TypesHelper.GetInheritanceType(targetExprType, Model);
            if(ownerType == null)
            {
                // error, must be node or edge type
                throw new SequenceParserExceptionUserMethodsOnlyAvailableForInheritanceTypes(targetExprType, seqCompProcMethodCall.Name);
            }

            if(ownerType.GetProcedureMethod(seqCompProcMethodCall.Name) == null)
                throw new SequenceParserExceptionCallIssue(seqCompProcMethodCall, CallIssueType.UnknownProcedure);

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

            InheritanceType ownerType = TypesHelper.GetInheritanceType(targetVar.Type, Model);
            if(ownerType == null)
            {
                // error, must be node or edge type
                throw new SequenceParserExceptionUserMethodsOnlyAvailableForInheritanceTypes(targetVar.Type, seqCompProcMethodCall.Name);
            }

            // check whether called procedure method exists
            if(ownerType.GetProcedureMethod(seqCompProcMethodCall.Name) == null)
                throw new SequenceParserExceptionCallIssue(seqCompProcMethodCall, CallIssueType.UnknownProcedure);

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
            String targetExprType = targetExpr.Type(this);
            if(targetExprType == "")
            {
                // only runtime checks possible (we could check whether the called procedure signature exists in at least one of the model types, if not it's a type error, can't work at runtime, but that kind of negative check is not worth the effort)
                return;
            }

            InheritanceType ownerType = TypesHelper.GetInheritanceType(targetExprType, Model);
            if(ownerType == null)
            {
                // error, must be node or edge type
                throw new SequenceParserExceptionUserMethodsOnlyAvailableForInheritanceTypes(targetExprType, seqExprFuncMethodCall.Name);
            }

            // check whether called function method exists
            if(ownerType.GetFunctionMethod(seqExprFuncMethodCall.Name) == null)
                throw new SequenceParserExceptionCallIssue(seqExprFuncMethodCall, CallIssueType.UnknownFunction);

            CheckFunctionCallBase(seqExprFuncMethodCall, ownerType);
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seqCompProcCall">The procedure call to check</param>
        /// <param name="ownerType">Gives the owner type of the procedure method call, in case this is a method call, otherwise null</param>
        private void CheckProcedureCallBase(SequenceComputationProcedureCall seqCompProcCall, InheritanceType ownerType)
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
        private void CheckFunctionCallBase(SequenceExpressionFunctionCall seqExprFuncCall, InheritanceType ownerType)
        {
            CheckInputParameters(seqExprFuncCall, seqExprFuncCall.ArgumentExpressions, ownerType);

            // ok, this is a well-formed invocation
        }

        private void CheckInputParameters(Invocation invocation, SequenceExpression[] ArgumentExpressions, InheritanceType ownerType)
        {
            // Check whether number of parameters and return parameters match
            if(NumInputParameters(invocation, ownerType) != ArgumentExpressions.Length)
                throw new SequenceParserExceptionCallParameterIssue(invocation, ArgumentExpressions.Length, CallParameterIssueType.BadNumberOfParameters, NumInputParameters(invocation, ownerType));

            // Check parameter types
            for(int i = 0; i < ArgumentExpressions.Length; i++)
            {
                ArgumentExpressions[i].Check(this);

                String argumentType = ArgumentExpressions[i].Type(this);
                String paramterType = InputParameterType(i, invocation, ownerType);
                if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                    throw new SequenceParserExceptionCallParameterIssue(invocation, -1, CallParameterIssueType.BadParameter, i);
            }
        }

        private void CheckOutputParameters(Invocation invocation, SequenceVariable[] ReturnVars, InheritanceType ownerType)
        {
            // Check whether number of parameters and return parameters match
            if(ReturnVars.Length != 0 && NumOutputParameters(invocation, ownerType) != ReturnVars.Length)
                throw new SequenceParserExceptionCallParameterIssue(invocation, ReturnVars.Length, CallParameterIssueType.BadNumberOfReturnParameters, NumOutputParameters(invocation, ownerType));

            // Check return types
            for(int i = 0; i < ReturnVars.Length; ++i)
            {
                String argumentType = ReturnVars[i].Type;
                String paramterType = OutputParameterType(i, invocation, ownerType);
                if(!TypesHelper.IsSameOrSubtype(paramterType, argumentType, Model))
                    throw new SequenceParserExceptionCallParameterIssue(invocation, -1, CallParameterIssueType.BadReturnParameter, i);
            }
        }

        private void CheckOutputParametersRuleAll(Invocation invocation, SequenceVariable[] ReturnVars)
        {
            // Check whether number of parameters and return parameters match
            if(ReturnVars.Length != 0 && NumOutputParameters(invocation, null) != ReturnVars.Length)
                throw new SequenceParserExceptionCallParameterIssue(invocation, ReturnVars.Length, CallParameterIssueType.BadNumberOfReturnParameters, NumOutputParameters(invocation, null));

            // Check return types
            for(int i = 0; i < ReturnVars.Length; ++i)
            {
                String argumentType = ReturnVars[i].Type;
                String paramterType = OutputParameterType(i, invocation, null);
                if(argumentType == "")
                    continue; // only runtime checks possible

                if(!argumentType.StartsWith("array<"))
                {
                    ConsoleUI.errorOutWriter.WriteLine("An all call expects all return parameters T in an array<T>");
                    throw new SequenceParserExceptionCallParameterIssue(invocation, -1, CallParameterIssueType.BadReturnParameter, i);
                }
                if(!TypesHelper.IsSameOrSubtype(paramterType, argumentType.Substring(6, ReturnVars[i].Type.Length - 7), Model))
                {
                    ConsoleUI.errorOutWriter.WriteLine("The arrays of the all call are inconsemurable in their value types");
                    throw new SequenceParserExceptionCallParameterIssue(invocation, -1, CallParameterIssueType.BadReturnParameter, i);
                }
            }
        }

        /// <summary>
        /// Checks whether called filter exists, and type checks the inputs.
        /// </summary>
        private void CheckFilterCalls(String ruleName, List<SequenceFilterCallBase> sequenceFilterCalls)
        {
            foreach(SequenceFilterCallBase sequenceFilterCallBase in sequenceFilterCalls)
            {
                if(sequenceFilterCallBase is SequenceFilterCall)
                {
                    SequenceFilterCall sequenceFilterCall = (SequenceFilterCall)sequenceFilterCallBase;

                    String filterCallName = GetFilterCallName(sequenceFilterCall);

                    // Check whether number of filter parameters match
                    if(NumFilterFunctionParameters(sequenceFilterCall) != sequenceFilterCall.ArgumentExpressions.Length)
                        throw new SequenceParserExceptionFilterParameterError(ruleName, filterCallName);

                    // Check parameter types
                    for(int i = 0; i < sequenceFilterCall.ArgumentExpressions.Length; i++)
                    {
                        sequenceFilterCall.ArgumentExpressions[i].Check(this);

                        String argumentType = sequenceFilterCall.ArgumentExpressions[i].Type(this);
                        String paramterType = FilterFunctionParameterType(i, sequenceFilterCall);
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterParameterError(ruleName, filterCallName);
                    }
                }
                else
                {
                    SequenceFilterCallLambdaExpression sequenceFilterCallLambdaExpression = (SequenceFilterCallLambdaExpression)sequenceFilterCallBase;

                    String filterCallName = GetFilterCallName(sequenceFilterCallLambdaExpression);

                    FilterCallWithLambdaExpression filterCall = sequenceFilterCallLambdaExpression.FilterCall;

                    if(filterCall.initArrayAccess != null)
                    {
                        String argumentType = filterCall.initArrayAccess.Type;
                        String paramterType = "array<match<" + ruleName + ">>";
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(ruleName, filterCallName, filterCall.initArrayAccess.Name);
                    }

                    if(filterCall.initExpression != null)
                        filterCall.initExpression.Check(this);

                    if(filterCall.arrayAccess != null)
                    {
                        String argumentType = filterCall.arrayAccess.Type;
                        String paramterType = "array<match<" + ruleName + ">>";
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(ruleName, filterCallName, filterCall.arrayAccess.Name);
                    }
                    if(filterCall.previousAccumulationAccess != null)
                    {
                        String argumentType = filterCall.previousAccumulationAccess.Type;
                        String paramterType = TypeOfTopLevelEntityInRule(ruleName, filterCall.Entity);
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(ruleName, filterCallName, filterCall.previousAccumulationAccess.Name);
                    }
                    if(filterCall.index != null)
                    {
                        String argumentType = filterCall.index.Type;
                        String paramterType = "int";
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(ruleName, filterCallName, filterCall.index.Name);
                    }
                    String elementArgumentType = filterCall.element.Type;
                    String elementParamterType = "match<" + ruleName + ">";
                    if(!TypesHelper.IsSameOrSubtype(elementArgumentType, elementParamterType, Model))
                        throw new SequenceParserExceptionFilterLambdaExpressionError(ruleName, filterCallName, filterCall.element.Name);

                    filterCall.lambdaExpression.Check(this);
                }
            }
        }

        /// <summary>
        /// Checks whether called match class filter exists, and type checks the inputs.
        /// </summary>
        public void CheckMatchClassFilterCalls(List<SequenceFilterCallBase> sequenceFilterCalls, List<SequenceRuleCall> ruleCalls)
        {
            foreach(SequenceFilterCallBase sequenceFilterCallBase in sequenceFilterCalls)
            {
                String matchClassName = GetMatchClassName(sequenceFilterCallBase);
                foreach(SequenceRuleCall ruleCall in ruleCalls)
                {
                    if(!IsRuleImplementingMatchClass(ruleCall.PackagePrefixedName, matchClassName))
                        throw new SequenceParserExceptionMatchClassNotImplemented(matchClassName, ruleCall.PackagePrefixedName);
                }

                if(sequenceFilterCallBase is SequenceFilterCall)
                {
                    SequenceFilterCall sequenceFilterCall = (SequenceFilterCall)sequenceFilterCallBase;

                    String filterCallName = GetFilterCallName(sequenceFilterCall);

                    // Check whether number of filter parameters match
                    if(NumFilterFunctionParameters(sequenceFilterCall) != sequenceFilterCall.ArgumentExpressions.Length)
                        throw new SequenceParserExceptionFilterParameterError(matchClassName, filterCallName);

                    // Check parameter types
                    for(int i = 0; i < sequenceFilterCall.ArgumentExpressions.Length; i++)
                    {
                        sequenceFilterCall.ArgumentExpressions[i].Check(this);

                        String argumentType = sequenceFilterCall.ArgumentExpressions[i].Type(this);
                        String paramterType = FilterFunctionParameterType(i, sequenceFilterCall);
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterParameterError(matchClassName, filterCallName);
                    }
                }
                else
                {
                    SequenceFilterCallLambdaExpression sequenceFilterCallLambdaExpression = (SequenceFilterCallLambdaExpression)sequenceFilterCallBase;

                    String filterCallName = GetFilterCallName(sequenceFilterCallLambdaExpression);

                    FilterCallWithLambdaExpression filterCall = sequenceFilterCallLambdaExpression.FilterCall;

                    if(filterCall.initArrayAccess != null)
                    {
                        String argumentType = filterCall.initArrayAccess.Type;
                        String paramterType = "array<match<class " + matchClassName + ">>";
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(matchClassName, filterCallName, filterCall.initArrayAccess.Name);
                    }

                    if(filterCall.initExpression != null)
                        filterCall.initExpression.Check(this);

                    if(filterCall.arrayAccess != null)
                    {
                        String argumentType = filterCall.arrayAccess.Type;
                        String paramterType = "array<match<class " + matchClassName + ">>";
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(matchClassName, filterCallName, filterCall.arrayAccess.Name);
                    }
                    if(filterCall.previousAccumulationAccess != null)
                    {
                        String argumentType = filterCall.previousAccumulationAccess.Type;
                        String paramterType = TypeOfMemberOrAttribute("match<class " + matchClassName + ">", filterCall.Entity);
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(matchClassName, filterCallName, filterCall.previousAccumulationAccess.Name);
                    }
                    if(filterCall.index != null)
                    {
                        String argumentType = filterCall.index.Type;
                        String paramterType = "int";
                        if(!TypesHelper.IsSameOrSubtype(argumentType, paramterType, Model))
                            throw new SequenceParserExceptionFilterLambdaExpressionError(matchClassName, filterCallName, filterCall.index.Name);
                    }
                    String elementArgumentType = filterCall.element.Type;
                    String elementParamterType = "match<class " + matchClassName + ">";
                    if(!TypesHelper.IsSameOrSubtype(elementArgumentType, elementParamterType, Model))
                        throw new SequenceParserExceptionFilterLambdaExpressionError(matchClassName, filterCallName, filterCall.element.Name);

                    filterCall.lambdaExpression.Check(this);
                }
            }
        }

        private int NumFilterFunctionParameters(SequenceFilterCall sequenceFilterCall)
        {
            if(sequenceFilterCall.Filter is IFilterAutoSupplied)
            {
                IFilterAutoSupplied filterAutoSupplied = (IFilterAutoSupplied)sequenceFilterCall.Filter;
                return filterAutoSupplied.Inputs.Length;
            }
            if(sequenceFilterCall.Filter is IFilterFunction)
            {
                IFilterFunction filterFunction = (IFilterFunction)sequenceFilterCall.Filter;
                return filterFunction.Inputs.Length;
            }
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

        private string GetFilterCallName(SequenceFilterCallBase sequenceFilterCall)
        {
            if(sequenceFilterCall is SequenceFilterCallLambdaExpression)
                return ((SequenceFilterCallLambdaExpression)sequenceFilterCall).PackagePrefixedName;
            return ((SequenceFilterCall)sequenceFilterCall).Filter.PackagePrefixedName;
        }

        private void CheckSubgraph(Invocation invocation)
        {
            SequenceVariable subgraph;
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                subgraph = ruleInvocation.Subgraph;
            }
            else
            {
                SequenceInvocation sequenceInvocation = (SequenceInvocation)invocation;
                subgraph = sequenceInvocation.Subgraph;
            }
            if(subgraph != null && !TypesHelper.IsSameOrSubtype("graph", subgraph.Type, Model))
                throw new SequenceParserExceptionTypeMismatch(subgraph.Name + "." + invocation.Name, "graph", subgraph.Type);
        }

        /// <summary>
        /// Helper which returns the type of the given top level entity of the given rule.
        /// Throws an exception in case the rule of the given name does not exist 
        /// or in case it does not contain an entity of the given name.
        /// </summary>
        public abstract string TypeOfTopLevelEntityInRule(string ruleName, string entityName);

        /// <summary>
        /// Helper which returns the type of 
        /// - the given member in the given match or match class type or
        /// - the given attribute in the given node or edge or internal object type
        /// </summary>
        public abstract string TypeOfMemberOrAttribute(string matchOrGraphElementType, string memberOrAttribute);

        protected abstract int NumInputParameters(Invocation invocation, InheritanceType ownerType);
        protected abstract int NumOutputParameters(Invocation invocation, InheritanceType ownerType);
        protected abstract string InputParameterType(int i, Invocation invocation, InheritanceType ownerType);
        protected abstract string OutputParameterType(int i, Invocation invocation, InheritanceType ownerType);
        protected abstract bool IsMatchClassExisting(SequenceFilterCallBase sequenceFilterCall);
        protected abstract string GetMatchClassName(SequenceFilterCallBase sequenceFilterCall);
        public abstract bool IsRuleImplementingMatchClass(string rulePackagePrefixedName, string matchClassPackagePrefixedName);
    }
}
