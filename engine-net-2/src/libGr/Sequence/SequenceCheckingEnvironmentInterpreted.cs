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

        public override bool IsProcedureCallExternal(ProcedureInvocation invocation)
        {
            return invocation.ProcedureDef.IsExternal;
        }

        public override bool IsFunctionCallExternal(FunctionInvocation invocation)
        {
            return invocation.FunctionDef.IsExternal;
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

        protected override bool IsCalledEntityExisting(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return ruleInvocation.Action != null;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                return seqInvocation.SequenceDef != null;
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name) != null;
                else
                    return procInvocation.ProcedureDef != null;
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcInvocation.Name) != null;
                else
                    return funcInvocation.FunctionDef != null;
            }
            throw new Exception("Internal error");
        }

        protected override int NumInputParameters(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return ruleInvocation.Action.RulePattern.Inputs.Length;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.InputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return seqDef.SeqInfo.ParameterTypes.Length;
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name).Inputs.Length;
                else
                    return procInvocation.ProcedureDef.Inputs.Length;
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcInvocation.Name).Inputs.Length;
                else
                    return funcInvocation.FunctionDef.Inputs.Length;
            }
            throw new Exception("Internal error");
        }

        protected override int NumOutputParameters(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return ruleInvocation.Action.RulePattern.Outputs.Length;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.OutputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return seqDef.SeqInfo.OutParameterTypes.Length;
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name).Outputs.Length;
                else
                    return procInvocation.ProcedureDef.Outputs.Length;
            }
            throw new Exception("Internal error");
        }

        protected override string InputParameterType(int i, Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return TypesHelper.DotNetTypeToXgrsType(ruleInvocation.Action.RulePattern.Inputs[i]);
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.InputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.ParameterTypes[i]);
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procInvocation.Name).Inputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(procInvocation.ProcedureDef.Inputs[i]);
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetFunctionMethod(funcInvocation.Name).Inputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(funcInvocation.FunctionDef.Inputs[i]);
            }
            throw new Exception("Internal error");
        }

        protected override string OutputParameterType(int i, Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return TypesHelper.DotNetTypeToXgrsType(ruleInvocation.Action.RulePattern.Outputs[i]);
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.OutputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.OutParameterTypes[i]);
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procInvocation.Name).Outputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(procInvocation.ProcedureDef.Outputs[i]);
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
            if(filterCall.IsContainedIn(seq.RuleInvocation.Action.RulePattern.Filters))
                return true;
            if(filterCall.IsAutoGenerated && seq.RuleInvocation.Package != null)
            {
                filterCall.Package = seq.RuleInvocation.Package;
                filterCall.PackagePrefixedName = seq.RuleInvocation.Package + "::" + filterCall.Name;
                return filterCall.IsContainedIn(seq.RuleInvocation.Action.RulePattern.Filters);
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
            foreach(IFilter filter in seq.RuleInvocation.Action.RulePattern.Filters)
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
            foreach(IFilter filter in seq.RuleInvocation.Action.RulePattern.Filters)
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
