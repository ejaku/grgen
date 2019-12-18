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

        public override bool IsProcedureCallExternal(ProcedureInvocationParameterBindings paramBindings)
        {
            return paramBindings.ProcedureDef.IsExternal;
        }

        public override bool IsFunctionCallExternal(FunctionInvocationParameterBindings paramBindings)
        {
            return paramBindings.FunctionDef.IsExternal;
        }

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
}
