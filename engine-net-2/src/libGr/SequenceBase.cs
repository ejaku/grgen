/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
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
    /// A proxy querying or simulating a user for choices during sequence execution
    /// TODO: general user proxy, not just for sequence execution
    /// </summary>
    public interface IUserProxyForSequenceExecution
    {
        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen directions is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        int ChooseDirection(int direction, Sequence seq);

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq);

        /// <summary>
        /// returns the maybe user altered point within the interval series, denoting the sequence to execute next
        /// the randomly chosen point is supplied; the sequence with the intervals and their corresponding sequences is supplied
        /// </summary>
        double ChoosePoint(double pointToExecute, SequenceWeightedOne seq);

        /// <summary>
        /// returns the maybe user altered match to execute next for the sequence given
        /// the randomly chosen total match is supplied; the sequence with the rules and matches is supplied
        /// </summary>
        int ChooseMatch(int totalMatchExecute, SequenceSomeFromSet seq);

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq);

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq);

        /// <summary>
        /// returns the maybe user altered random number in the range 0.0 - 1.0 exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        double ChooseRandomNumber(double randomNumber, Sequence seq);

        /// <summary>
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        object ChooseValue(string type, Sequence seq);
    }


    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
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
        public void CheckCall(Sequence seq)
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
                if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, paramBindings, null), paramBindings.ReturnVars[i].Type, Model))
                    throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
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

    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
    /// Concrete subclass for interpreted sequences.
    /// </summary>
    public class SequenceCheckingEnvironmentInterpreted : SequenceCheckingEnvironment
    {
        // constructor for interpreted sequences
        public SequenceCheckingEnvironmentInterpreted(IActions actions)
        {
            this.actions = actions;
        }

        // the information available if this is an interpreted sequence 

        private IActions actions;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        public override IGraphModel Model { get { return actions.Graph.Model; } }

        public override string TypeOfTopLevelEntityInRule(string ruleName, string entityName)
        {
            IAction rule = actions.GetAction(ruleName);
            if(rule==null)
                throw new SequenceParserException(ruleName, SequenceParserError.UnknownRule);

            foreach(IPatternNode node in rule.RulePattern.PatternGraph.Nodes)
                if(node.UnprefixedName==entityName)
                    return TypesHelper.DotNetTypeToXgrsType(node.Type);

            foreach(IPatternEdge edge in rule.RulePattern.PatternGraph.Edges)
                if(edge.UnprefixedName==entityName)
                    return TypesHelper.DotNetTypeToXgrsType(edge.Type);

            foreach(IPatternVariable var in rule.RulePattern.PatternGraph.Variables)
                if(var.UnprefixedName==entityName)
                    return TypesHelper.DotNetTypeToXgrsType(var.Type);

            throw new SequenceParserException(ruleName, entityName, SequenceParserError.UnknownPatternElement);
        }

        protected override bool IsCalledEntityExisting(InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action != null;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return seqParamBindings.SequenceDef != null;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procParamBindings.Name) != null;
                else
                    return procParamBindings.ProcedureDef != null;
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcParamBindings.Name) != null;
                else
                    return funcParamBindings.FunctionDef != null;
            }
            throw new Exception("Internal error");
        }

        protected override int NumInputParameters(InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action.RulePattern.Inputs.Length;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.InputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return seqDef.SeqInfo.ParameterTypes.Length;
                }
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procParamBindings.Name).Inputs.Length;
                else
                    return procParamBindings.ProcedureDef.Inputs.Length;
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcParamBindings.Name).Inputs.Length;
                else
                    return funcParamBindings.FunctionDef.Inputs.Length;
            }
            throw new Exception("Internal error");
        }

        protected override int NumOutputParameters(InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action.RulePattern.Outputs.Length;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.OutputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return seqDef.SeqInfo.OutParameterTypes.Length;
                }
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procParamBindings.Name).Outputs.Length;
                else
                    return procParamBindings.ProcedureDef.Outputs.Length;
            }
            throw new Exception("Internal error");
        }

        protected override string InputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(ruleParamBindings.Action.RulePattern.Inputs[i]);
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.InputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.ParameterTypes[i]);
                }
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procParamBindings.Name).Inputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(procParamBindings.ProcedureDef.Inputs[i]);
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetFunctionMethod(funcParamBindings.Name).Inputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(funcParamBindings.FunctionDef.Inputs[i]);
            }
            throw new Exception("Internal error");
        }

        protected override string OutputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(ruleParamBindings.Action.RulePattern.Outputs[i]);
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.OutputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.OutParameterTypes[i]);
                }
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procParamBindings.Name).Outputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(procParamBindings.ProcedureDef.Outputs[i]);
            }
            throw new Exception("Internal error");
        }

        protected override bool IsFilterExisting(FilterCall filterCall, SequenceRuleCall seq)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                filterCall.Package = null;
                filterCall.PackagePrefixedName = filterCall.Name;
                return true;
            }
            filterCall.Package = filterCall.PrePackage;
            filterCall.PackagePrefixedName = filterCall.Package != null ? filterCall.Package + "::" + filterCall.Name : filterCall.Name;
            if(filterCall.IsContainedIn(seq.ParamBindings.Action.RulePattern.Filters))
                return true;
            if(filterCall.IsAutoGenerated && seq.ParamBindings.Package != null)
            {
                filterCall.Package = seq.ParamBindings.Package;
                filterCall.PackagePrefixedName = seq.ParamBindings.Package + "::" + filterCall.Name;
                return filterCall.IsContainedIn(seq.ParamBindings.Action.RulePattern.Filters);
            }
            return false;
        }

        protected override int NumFilterFunctionParameters(FilterCall filterCall, SequenceRuleCall seq)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                return 1;
            }
            foreach(IFilter filter in seq.ParamBindings.Action.RulePattern.Filters)
            {
                if(filter is IFilterFunction)
                {
                    IFilterFunction filterFunction = (IFilterFunction)filter;
                    if(filterCall.PackagePrefixedName == filterFunction.PackagePrefixedName)
                        return filterFunction.Inputs.Length;
                }
            }
            return 0; // auto-generated
        }

        protected override string FilterFunctionParameterType(int i, FilterCall filterCall, SequenceRuleCall seq)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst")
                return "int";
            if(filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction")
                return "double";
            if(filterCall.Name == "keepLast" || filterCall.Name == "removeLast")
                return "int";
            if(filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
                return "double";
            foreach(IFilter filter in seq.ParamBindings.Action.RulePattern.Filters)
            {
                if(filter is IFilterFunction)
                {
                    IFilterFunction filterFunction = (IFilterFunction)filter;
                    if(filterCall.PackagePrefixedName == filterFunction.PackagePrefixedName)
                        return TypesHelper.DotNetTypeToXgrsType(filterFunction.Inputs[i]);
                }
            }
            throw new Exception("Internal error");
        }
    }

    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
    /// Concrete subclass for compiled sequences.
    /// This environment in addition resolves names that are given without package context but do not reference global names
    /// because they are used from a sequence that is contained in a package (only possible for compiled sequences from rule language).
    /// </summary>
    public class SequenceCheckingEnvironmentCompiled : SequenceCheckingEnvironment
    {
        // constructor for compiled sequences
        public SequenceCheckingEnvironmentCompiled(String[] ruleNames, String[] sequenceNames, String[] procedureNames, String[] functionNames,
            Dictionary<String, List<IFilter>> rulesToFilters, Dictionary<String, List<String>> filterFunctionsToInputTypes,
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes,
            Dictionary<String, List<String>> rulesToTopLevelEntities, Dictionary<String, List<String>> rulesToTopLevelEntityTypes, 
            Dictionary<String, List<String>> sequencesToInputTypes, Dictionary<String, List<String>> sequencesToOutputTypes,
            Dictionary<String, List<String>> proceduresToInputTypes, Dictionary<String, List<String>> proceduresToOutputTypes,
            Dictionary<String, List<String>> functionsToInputTypes, Dictionary<String, String> functionsToOutputType,
            IGraphModel model)
        {
            this.ruleNames = ruleNames;
            this.sequenceNames = sequenceNames;
            this.procedureNames = procedureNames;
            this.functionNames = functionNames;
            this.rulesToFilters = rulesToFilters;
            this.filterFunctionsToInputTypes = filterFunctionsToInputTypes;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.rulesToTopLevelEntities = rulesToTopLevelEntities;
            this.rulesToTopLevelEntityTypes = rulesToTopLevelEntityTypes;
            this.sequencesToInputTypes = sequencesToInputTypes;
            this.sequencesToOutputTypes = sequencesToOutputTypes;
            this.proceduresToInputTypes = proceduresToInputTypes;
            this.proceduresToOutputTypes = proceduresToOutputTypes;
            this.functionsToInputTypes = functionsToInputTypes;
            this.functionsToOutputType = functionsToOutputType;
            this.model = model;
        }

        // the information available if this is a compiled sequence 

        // the rule names available in the .grg to compile
        private String[] ruleNames;

        // the sequence names available in the .grg to compile
        private String[] sequenceNames;

        // the procedure names available in the .grg to compile
        private String[] procedureNames;

        // the function names available in the .grg to compile
        private String[] functionNames;

        // maps rule names available in the .grg to compile to the list of the match filters
        private Dictionary<String, List<IFilter>> rulesToFilters;
        // maps filter function names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> filterFunctionsToInputTypes;

        // maps rule names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> rulesToOutputTypes;

        // maps rule names available in the .grg to compile to the list of the top level entity names (nodes,edges,variables)
        private Dictionary<String, List<String>> rulesToTopLevelEntities;
        // maps rule names available in the .grg to compile to the list of the top level entity types
        private Dictionary<String, List<String>> rulesToTopLevelEntityTypes;

        // maps sequence names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> sequencesToInputTypes;
        // maps sequence names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> sequencesToOutputTypes;

        // maps procedure names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> proceduresToInputTypes;
        // maps procedure names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> proceduresToOutputTypes;

        // maps function names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> functionsToInputTypes;
        // maps function names available in the .grg to compile to the list of the output typ name
        private Dictionary<String, String> functionsToOutputType;

        // returns rule or sequence name to input types dictionary depending on argument
        private Dictionary<String, List<String>> toInputTypes(bool rule) { return rule ? rulesToInputTypes : sequencesToInputTypes; }

        // returns rule or sequence name to output types dictionary depending on argument
        private Dictionary<String, List<String>> toOutputTypes(bool rule) { return rule ? rulesToOutputTypes : sequencesToOutputTypes; }

        // the model object of the .grg to compile
        private IGraphModel model;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the model giving access to graph element types for checking
        /// </summary>
        public override IGraphModel Model { get { return model; } }

        public override string TypeOfTopLevelEntityInRule(string ruleName, string entityName)
        {
            if(!rulesToTopLevelEntities.ContainsKey(ruleName))
                throw new SequenceParserException(ruleName, SequenceParserError.UnknownRule);

            if(!rulesToTopLevelEntities[ruleName].Contains(entityName))
                throw new SequenceParserException(ruleName, entityName, SequenceParserError.UnknownPatternElement);

            int indexOfEntity = rulesToTopLevelEntities[ruleName].IndexOf(entityName);
            return rulesToTopLevelEntityTypes[ruleName][indexOfEntity];
        }

        protected override bool IsCalledEntityExisting(InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            // besides determining whether the called entity exists, this function
            // resolves pre- and context packages to packages, i.e. calls of entities from packages 
            // without package prefix are changed to package calls (may occur for entities from the same package)

            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                if(ruleParamBindings.PrePackage != null)
                {
                    ruleParamBindings.Package = ruleParamBindings.PrePackage;
                    ruleParamBindings.PackagePrefixedName = ruleParamBindings.PrePackage + "::" + ruleParamBindings.Name;
                    return Array.IndexOf(ruleNames, ruleParamBindings.PrePackage + "::" + ruleParamBindings.Name) != -1;
                }
                else
                {
                    if(Array.IndexOf(ruleNames, ruleParamBindings.Name) != -1)
                    {
                        ruleParamBindings.Package = null;
                        ruleParamBindings.PackagePrefixedName = ruleParamBindings.Name;
                        return true;
                    }
                    if(ruleParamBindings.PrePackageContext != null)
                    {
                        ruleParamBindings.Package = ruleParamBindings.PrePackageContext;
                        ruleParamBindings.PackagePrefixedName = ruleParamBindings.PrePackageContext + "::" + ruleParamBindings.Name;
                        return Array.IndexOf(ruleNames, ruleParamBindings.PrePackageContext + "::" + ruleParamBindings.Name) != -1;
                    }
                    return false;
                }
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.PrePackage != null)
                {
                    seqParamBindings.Package = seqParamBindings.PrePackage;
                    seqParamBindings.PackagePrefixedName = seqParamBindings.PrePackage + "::" + seqParamBindings.Name;
                    return Array.IndexOf(sequenceNames, seqParamBindings.PrePackage + "::" + seqParamBindings.Name) != -1;
                }
                else
                {
                    if(Array.IndexOf(sequenceNames, seqParamBindings.Name) != -1)
                    {
                        seqParamBindings.Package = null;
                        seqParamBindings.PackagePrefixedName = seqParamBindings.Name;
                        return true;
                    }
                    if(seqParamBindings.PrePackageContext != null)
                    {
                        seqParamBindings.Package = seqParamBindings.PrePackageContext;
                        seqParamBindings.PackagePrefixedName = seqParamBindings.PrePackageContext + "::" + seqParamBindings.Name;
                        return Array.IndexOf(sequenceNames, seqParamBindings.PrePackageContext + "::" + seqParamBindings.Name) != -1;
                    }
                    return false;
                }
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                {
                    return ownerType.GetProcedureMethod(procParamBindings.Name) != null;
                }
                else
                {
                    if(procParamBindings.PrePackage != null)
                    {
                        procParamBindings.Package = procParamBindings.PrePackage;
                        procParamBindings.PackagePrefixedName = procParamBindings.PrePackage + "::" + procParamBindings.Name;
                        return Array.IndexOf(procedureNames, procParamBindings.PrePackage + "::" + procParamBindings.Name) != -1;
                    }
                    else
                    {
                        if(Array.IndexOf(procedureNames, procParamBindings.Name) != -1)
                        {
                            procParamBindings.Package = null;
                            procParamBindings.PackagePrefixedName = procParamBindings.Name;
                            return true;
                        }
                        if(procParamBindings.PrePackageContext != null)
                        {
                            procParamBindings.Package = procParamBindings.PrePackageContext;
                            procParamBindings.PackagePrefixedName = procParamBindings.PrePackageContext + "::" + procParamBindings.Name;
                            return Array.IndexOf(procedureNames, procParamBindings.PrePackageContext + "::" + procParamBindings.Name) != -1;
                        }
                        return false;
                    }
                }
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                {
                    return ownerType.GetFunctionMethod(funcParamBindings.Name) != null;
                }
                else
                {
                    if(funcParamBindings.PrePackage != null)
                    {
                        funcParamBindings.Package = funcParamBindings.PrePackage;
                        funcParamBindings.PackagePrefixedName = funcParamBindings.PrePackage + "::" + funcParamBindings.Name;
                        return Array.IndexOf(functionNames, funcParamBindings.PrePackage + "::" + funcParamBindings.Name) != -1;
                    }
                    else
                    {
                        if(Array.IndexOf(functionNames, funcParamBindings.Name) != -1)
                        {
                            funcParamBindings.Package = null;
                            funcParamBindings.PackagePrefixedName = funcParamBindings.Name;
                            return true;
                        }
                        if(funcParamBindings.PrePackageContext != null)
                        {
                            funcParamBindings.Package = funcParamBindings.PrePackageContext;
                            funcParamBindings.PackagePrefixedName = funcParamBindings.PrePackageContext + "::" + funcParamBindings.Name;
                            return Array.IndexOf(functionNames, funcParamBindings.PrePackageContext + "::" + funcParamBindings.Name) != -1;
                        }
                        return false;
                    }
                }
            }
            throw new Exception("Internal error");
        }

        protected override int NumInputParameters(InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToInputTypes[ruleParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToInputTypes[seqParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procParamBindings.Name).Inputs.Length;
                else
                    return proceduresToInputTypes[procParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcParamBindings.Name).Inputs.Length;
                else
                    return functionsToInputTypes[funcParamBindings.PackagePrefixedName].Count;
            }
            throw new Exception("Internal error");
        }

        protected override int NumOutputParameters(InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToOutputTypes[ruleParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToOutputTypes[seqParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procParamBindings.Name).Outputs.Length;
                else
                    return proceduresToOutputTypes[procParamBindings.PackagePrefixedName].Count;
            }
            throw new Exception("Internal error");
        }

        protected override string InputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToInputTypes[ruleParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToInputTypes[seqParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procParamBindings.Name).Inputs[i]);
                else
                    return proceduresToInputTypes[procParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetFunctionMethod(funcParamBindings.Name).Inputs[i]);
                else
                    return functionsToInputTypes[funcParamBindings.PackagePrefixedName][i];
            }
            throw new Exception("Internal error");
        }

        protected override string OutputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToOutputTypes[ruleParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToOutputTypes[seqParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procParamBindings.Name).Outputs[i]);
                else
                    return proceduresToOutputTypes[procParamBindings.PackagePrefixedName][i];
            }
            throw new Exception("Internal error");
        }

        protected override bool IsFilterExisting(FilterCall filterCall, SequenceRuleCall seq)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                filterCall.Package = null;
                filterCall.PackagePrefixedName = filterCall.Name;
                return true;
            }

            if(filterCall.PrePackage != null)
            {
                filterCall.Package = filterCall.PrePackage;
                filterCall.PackagePrefixedName = filterCall.PrePackage + "::" + filterCall.Name;
                return filterCall.IsContainedIn(rulesToFilters[seq.ParamBindings.PackagePrefixedName]);
            }
            else
            {
                filterCall.Package = null;
                filterCall.PackagePrefixedName = filterCall.Name;
                if(filterCall.IsContainedIn(rulesToFilters[seq.ParamBindings.PackagePrefixedName]))
                    return true;
                if(filterCall.PrePackageContext != null)
                {
                    filterCall.Package = filterCall.PrePackageContext;
                    filterCall.PackagePrefixedName = filterCall.PrePackageContext + "::" + filterCall.Name;
                    if(filterCall.IsContainedIn(rulesToFilters[seq.ParamBindings.PackagePrefixedName]))
                        return true;
                }
                if(filterCall.IsAutoGenerated && seq.ParamBindings.Package != null)
                {
                    filterCall.Package = seq.ParamBindings.Package;
                    filterCall.PackagePrefixedName = seq.ParamBindings.Package + "::" + filterCall.Name;
                    return filterCall.IsContainedIn(rulesToFilters[seq.ParamBindings.PackagePrefixedName]);
                }
                return false;
            }
        }

        protected override int NumFilterFunctionParameters(FilterCall filterCall, SequenceRuleCall seq)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                return 1;
            }
            if(filterFunctionsToInputTypes.ContainsKey(filterCall.PackagePrefixedName))
                return filterFunctionsToInputTypes[filterCall.PackagePrefixedName].Count;
            else
                return 0; // auto-supplied
        }

        protected override string FilterFunctionParameterType(int i, FilterCall filterCall, SequenceRuleCall seq)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst")
                return "int";
            if(filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction")
                return "double";
            if(filterCall.Name == "keepLast" || filterCall.Name == "removeLast")
                return "int";
            if(filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
                return "double";
            return filterFunctionsToInputTypes[filterCall.PackagePrefixedName][i];
        }
    }


    /// <summary>
    /// The common base of sequence, sequence computation, and sequence expression objects,
    /// with some common infrastructure.
    /// </summary>
    public abstract class SequenceBase
    {
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
        protected int id;

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
    }
}
