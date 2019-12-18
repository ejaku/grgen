/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Environment for sequence (esp. type) checking giving access to model and action signatures.
    /// Abstract base class, there are two concrete subclasses, one for interpreted, one for compiled sequences.
    /// The compiled version in addition resolves names that are given without package context but do not reference global names
    /// because they are used from a sequence that is contained in a package (only possible for compiled sequences from rule language).
    /// </summary>
    public abstract class SequenceCheckingEnvironment
    {
        /// <summary>
        /// The model giving access to graph element types for checking.
        /// </summary>
        public abstract IGraphModel Model { get; }

        /// <summary>
        /// Helper for checking rule calls, rule all calls, and sequence calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence to check, must be a rule call, a rule all call, or a sequence call</param>
        public void CheckCall(Sequence seq, bool isRuleAllCall)
        {
            InvocationParameterBindingsWithReturns paramBindings = ExtractParameterBindings(seq);

            // check the name against the available names, "resolves" them as needed (pre- and context packages to packages)
            if(!IsCalledEntityExisting(paramBindings, null))
                throw new SequenceParserException(paramBindings, SequenceParserError.UnknownRuleOrSequence);

            // Check whether number of parameters and return parameters match
            if(NumInputParameters(paramBindings, null) != paramBindings.ArgumentExpressions.Length
                    || paramBindings.ReturnVars.Length != 0 && NumOutputParameters(paramBindings, null) != paramBindings.ReturnVars.Length)
                throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

            // Check parameter types
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                paramBindings.ArgumentExpressions[i].Check(this);

                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    if(!TypesHelper.IsSameOrSubtype(paramBindings.ArgumentExpressions[i].Type(this), InputParameterType(i, paramBindings, null), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
                else
                {
                    if(paramBindings.Arguments[i]!=null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(paramBindings.Arguments[i], Model), InputParameterType(i, paramBindings, null), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
            }

            // Check return types
            for(int i = 0; i < paramBindings.ReturnVars.Length; ++i)
            {
                if(isRuleAllCall)
                {
                    if(paramBindings.ReturnVars[i].Type != "")
                    {
                        if(!paramBindings.ReturnVars[i].Type.StartsWith("array<"))
                        {
                            Console.Error.WriteLine("An all call expects all return parameters T in an array<T>");
                            throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
                        }
                        if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, paramBindings, null), paramBindings.ReturnVars[i].Type.Substring(6, paramBindings.ReturnVars[i].Type.Length - 7), Model))
                        {
                            Console.Error.WriteLine("The arrays of the all call are inconsemurable in their value types");
                            throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
                        }
                    }
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, paramBindings, null), paramBindings.ReturnVars[i].Type, Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
                }
            }

            // Check filter calls
            if(seq is SequenceRuleCall)
            {
                SequenceRuleCall seqRuleCall = (SequenceRuleCall)seq;
                foreach(FilterCall filterCall in seqRuleCall.Filters)
                {
                    if(!IsFilterExisting(filterCall, seqRuleCall))
                        throw new SequenceParserException(paramBindings.PackagePrefixedName ?? paramBindings.Name, filterCall.PackagePrefixedName ?? filterCall.Name, SequenceParserError.FilterError);

                    // Check whether number of filter parameters match
                    if(NumFilterFunctionParameters(filterCall, seqRuleCall) != filterCall.ArgumentExpressions.Length)
                        throw new SequenceParserException(paramBindings.Name, filterCall.Name, SequenceParserError.FilterParameterError);

                    // Check parameter types
                    for(int i = 0; i < filterCall.ArgumentExpressions.Length; i++)
                    {
                        filterCall.ArgumentExpressions[i].Check(this);

                        if(filterCall.ArgumentExpressions[i] != null)
                        {
                            if(!TypesHelper.IsSameOrSubtype(filterCall.ArgumentExpressions[i].Type(this), FilterFunctionParameterType(i, filterCall, seqRuleCall), Model))
                                throw new SequenceParserException(paramBindings.Name, filterCall.Name, SequenceParserError.FilterParameterError);
                        }
                        else
                        {
                            if(filterCall.Arguments[i] != null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(filterCall.Arguments[i], Model), FilterFunctionParameterType(i, filterCall, seqRuleCall), Model))
                                throw new SequenceParserException(paramBindings.Name, filterCall.Name, SequenceParserError.FilterParameterError);
                        }
                    }
                }
            }

            SequenceVariable subgraph;
            if(paramBindings is RuleInvocationParameterBindings)
                subgraph = ((RuleInvocationParameterBindings)paramBindings).Subgraph;
            else
                subgraph = ((SequenceInvocationParameterBindings)paramBindings).Subgraph;
            if(subgraph!=null && !TypesHelper.IsSameOrSubtype("graph", subgraph.Type, Model))
                throw new SequenceParserException(paramBindings.Name, subgraph.Type, SequenceParserError.SubgraphTypeError);
    
            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence computation to check, must be a procedure call</param>
        /// <param name="ownerType">Gives the owner type of the procedure method call, in case this is a method call, otherwise null</param>
        private void CheckProcedureCallBase(SequenceComputation seq, GrGenType ownerType)
        {
            InvocationParameterBindingsWithReturns paramBindings = (seq as SequenceComputationProcedureCall).ParamBindings;

            // check the name against the available names
            if(!IsCalledEntityExisting(paramBindings, ownerType))
                throw new SequenceParserException(paramBindings, SequenceParserError.UnknownProcedure);

            // Check whether number of parameters and return parameters match
            if(NumInputParameters(paramBindings, ownerType) != paramBindings.ArgumentExpressions.Length
                    || paramBindings.ReturnVars.Length != 0 && NumOutputParameters(paramBindings, ownerType) != paramBindings.ReturnVars.Length)
                throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

            // Check parameter types
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                paramBindings.ArgumentExpressions[i].Check(this);

                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    if(!TypesHelper.IsSameOrSubtype(paramBindings.ArgumentExpressions[i].Type(this), InputParameterType(i, paramBindings, ownerType), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
                else
                {
                    if(paramBindings.Arguments[i] != null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(paramBindings.Arguments[i], Model), InputParameterType(i, paramBindings, ownerType), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
            }

            // Check return types
            for(int i = 0; i < paramBindings.ReturnVars.Length; ++i)
            {
                if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, paramBindings, ownerType), paramBindings.ReturnVars[i].Type, Model))
                    throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
            }

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking function calls.
        /// Checks whether called entity exists, and type checks the input.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence expression to check, must be a function call</param>
        /// <param name="ownerType">Gives the owner type of the function method call, in case this is a method call, otherwise null</param>
        private void CheckFunctionCallBase(SequenceExpression seq, GrGenType ownerType)
        {
            InvocationParameterBindings paramBindings = (seq as SequenceExpressionFunctionCall).ParamBindings;

            // check the name against the available names
            if(!IsCalledEntityExisting(paramBindings, ownerType))
                throw new SequenceParserException(paramBindings, SequenceParserError.UnknownFunction);

            // Check whether number of parameters and return parameters match
            if(NumInputParameters(paramBindings, ownerType) != paramBindings.ArgumentExpressions.Length)
                throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

            // Check parameter types
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                paramBindings.ArgumentExpressions[i].Check(this);

                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    if(!TypesHelper.IsSameOrSubtype(paramBindings.ArgumentExpressions[i].Type(this), InputParameterType(i, paramBindings, ownerType), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
                else
                {
                    if(paramBindings.Arguments[i] != null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(paramBindings.Arguments[i], Model), InputParameterType(i, paramBindings, ownerType), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
            }

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence computation to check, must be a procedure call</param>
        public void CheckProcedureCall(SequenceComputation seq)
        {
            CheckProcedureCallBase(seq, null);
        }

        public abstract bool IsProcedureCallExternal(ProcedureInvocationParameterBindings paramBindings);

        /// <summary>
        /// Helper for checking procedure method calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence computation to check, must be a procedure call</param>
        /// <param name="targetExpr">The target of the procedure method call</param>
        public void CheckProcedureMethodCall(SequenceExpression targetExpr, SequenceComputation seq)
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
            
            CheckProcedureCallBase(seq, ownerType);
        }

        /// <summary>
        /// Helper for checking procedure method calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence computation to check, must be a procedure call</param>
        /// <param name="targetVar">The target of the procedure method call</param>
        public void CheckProcedureMethodCall(SequenceVariable targetVar, SequenceComputation seq)
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

            CheckProcedureCallBase(seq, ownerType);
        }

        /// <summary>
        /// Helper for checking function calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence expression to check, must be a function call</param>
        public void CheckFunctionCall(SequenceExpression seq)
        {
            CheckFunctionCallBase(seq, null);
        }

        public abstract bool IsFunctionCallExternal(FunctionInvocationParameterBindings paramBindings);

        /// <summary>
        /// Helper for checking function method calls.
        /// Checks whether called entity exists, and type checks the input.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence expression to check, must be a function call</param>
        /// <param name="targetExpr">The target of the procedure function call</param>
        public void CheckFunctionMethodCall(SequenceExpression targetExpr, SequenceExpression seq)
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

            CheckFunctionCallBase(seq, ownerType);
        }

        /// <summary>
        /// Helper which returns the type of the given top level entity of the given rule.
        /// Throws an exception in case the rule of the given name does not exist 
        /// or in case it does not contain an entity of the given name.
        /// </summary>
        public abstract string TypeOfTopLevelEntityInRule(string ruleName, string entityName);

        private InvocationParameterBindingsWithReturns ExtractParameterBindings(SequenceBase seq)
        {
            if(seq is SequenceRuleCall) // hint: a rule all call is a rule call, too
                return (seq as SequenceRuleCall).ParamBindings;
            else
                return (seq as SequenceSequenceCall).ParamBindings;
        }

        protected abstract bool IsCalledEntityExisting(InvocationParameterBindings paramBindings, GrGenType ownerType);
        protected abstract int NumInputParameters(InvocationParameterBindings paramBindings, GrGenType ownerType);
        protected abstract int NumOutputParameters(InvocationParameterBindings paramBindings, GrGenType ownerType);
        protected abstract string InputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType);
        protected abstract string OutputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType);
        protected abstract bool IsFilterExisting(FilterCall filterCall, SequenceRuleCall seq);
        protected abstract int NumFilterFunctionParameters(FilterCall filterCall, SequenceRuleCall seq);
        protected abstract string FilterFunctionParameterType(int i, FilterCall filterCall, SequenceRuleCall seq);
    }
}
