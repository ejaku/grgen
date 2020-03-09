/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
    /// Concrete subclass for compiled sequences.
    /// </summary>
    public class SequenceCheckingEnvironmentCompiled : SequenceCheckingEnvironment
    {
        // constructor for compiled sequences
        public SequenceCheckingEnvironmentCompiled(ActionNames actionNames, ActionsTypeInformation actionsTypeInformation, IGraphModel model)
        {
            this.actionNames = actionNames;
            this.actionsTypeInformation = actionsTypeInformation;
            this.model = model;
        }

        // the information available if this is a compiled sequence 

        readonly ActionNames actionNames;

        readonly ActionsTypeInformation actionsTypeInformation;

        // returns rule or sequence name to input types dictionary depending on argument
        private Dictionary<String, List<String>> toInputTypes(bool rule) { return rule ? actionsTypeInformation.rulesToInputTypes : actionsTypeInformation.sequencesToInputTypes; }

        // returns rule or sequence name to output types dictionary depending on argument
        private Dictionary<String, List<String>> toOutputTypes(bool rule) { return rule ? actionsTypeInformation.rulesToOutputTypes : actionsTypeInformation.sequencesToOutputTypes; }

        // the model object of the .grg to compile
        private readonly IGraphModel model;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the model giving access to graph element types for checking
        /// </summary>
        public override IGraphModel Model { get { return model; } }

        public override bool IsProcedureCallExternal(ProcedureInvocation procedureInvocation)
        {
            return actionsTypeInformation.proceduresToIsExternal[procedureInvocation.PackagePrefixedName];
        }

        public override bool IsFunctionCallExternal(FunctionInvocation functionInvocation)
        {
            return actionsTypeInformation.functionsToIsExternal[functionInvocation.PackagePrefixedName];
        }

        public override string TypeOfTopLevelEntityInRule(string ruleName, string entityName)
        {
            if(!actionsTypeInformation.rulesToTopLevelEntities.ContainsKey(ruleName))
                throw new SequenceParserException(ruleName, SequenceParserError.UnknownRule);

            if(!actionsTypeInformation.rulesToTopLevelEntities[ruleName].Contains(entityName))
                throw new SequenceParserException(ruleName, entityName, SequenceParserError.UnknownPatternElement);

            int indexOfEntity = actionsTypeInformation.rulesToTopLevelEntities[ruleName].IndexOf(entityName);
            return actionsTypeInformation.rulesToTopLevelEntityTypes[ruleName][indexOfEntity];
        }

        protected override bool IsCalledEntityExisting(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return Array.IndexOf(actionNames.ruleNames, ruleInvocation.PackagePrefixedName) != -1;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                return Array.IndexOf(actionNames.sequenceNames, seqInvocation.PackagePrefixedName) != -1;
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name) != null;
                else
                    return Array.IndexOf(actionNames.procedureNames, procInvocation.PackagePrefixedName) != -1;
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcInvocation.Name) != null;
                else
                    return Array.IndexOf(actionNames.functionNames, funcInvocation.PackagePrefixedName) != -1;
            }
            throw new Exception("Internal error");
        }

        protected override int NumInputParameters(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return actionsTypeInformation.rulesToInputTypes[ruleInvocation.PackagePrefixedName].Count;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                return actionsTypeInformation.sequencesToInputTypes[seqInvocation.PackagePrefixedName].Count;
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name).Inputs.Length;
                else
                    return actionsTypeInformation.proceduresToInputTypes[procInvocation.PackagePrefixedName].Count;
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcInvocation.Name).Inputs.Length;
                else
                    return actionsTypeInformation.functionsToInputTypes[funcInvocation.PackagePrefixedName].Count;
            }
            throw new Exception("Internal error");
        }

        protected override int NumOutputParameters(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return actionsTypeInformation.rulesToOutputTypes[ruleInvocation.PackagePrefixedName].Count;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                return actionsTypeInformation.sequencesToOutputTypes[seqInvocation.PackagePrefixedName].Count;
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name).Outputs.Length;
                else
                    return actionsTypeInformation.proceduresToOutputTypes[procInvocation.PackagePrefixedName].Count;
            }
            throw new Exception("Internal error");
        }

        protected override string InputParameterType(int i, Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return actionsTypeInformation.rulesToInputTypes[ruleInvocation.PackagePrefixedName][i];
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                return actionsTypeInformation.sequencesToInputTypes[seqInvocation.PackagePrefixedName][i];
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procInvocation.Name).Inputs[i]);
                else
                    return actionsTypeInformation.proceduresToInputTypes[procInvocation.PackagePrefixedName][i];
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetFunctionMethod(funcInvocation.Name).Inputs[i]);
                else
                    return actionsTypeInformation.functionsToInputTypes[funcInvocation.PackagePrefixedName][i];
            }
            throw new Exception("Internal error");
        }

        protected override string OutputParameterType(int i, Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return actionsTypeInformation.rulesToOutputTypes[ruleInvocation.PackagePrefixedName][i];
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceInvocation seqInvocation = (SequenceInvocation)invocation;
                return actionsTypeInformation.sequencesToOutputTypes[seqInvocation.PackagePrefixedName][i];
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procInvocation.Name).Outputs[i]);
                else
                    return actionsTypeInformation.proceduresToOutputTypes[procInvocation.PackagePrefixedName][i];
            }
            throw new Exception("Internal error");
        }

        protected override bool IsFilterExisting(SequenceFilterCall sequenceFilterCall, SequenceRuleCall seq)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;

            if(filterCall.IsAutoSupplied)
                return true;

            String ruleName = seq.RuleInvocation.PackagePrefixedName;

            if(filterCall.IsAutoGenerated)
                return actionsTypeInformation.RuleContainsFilter(ruleName, filterCall.FullName);

            return actionsTypeInformation.RuleContainsFilter(ruleName, filterCall.PackagePrefixedName);
        }

        protected override int NumFilterFunctionParameters(SequenceFilterCall sequenceFilterCall, SequenceRuleCall seq)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;
            if(filterCall.IsAutoSupplied)
                return 1;
            if(actionsTypeInformation.filterFunctionsToInputTypes.ContainsKey(filterCall.PackagePrefixedName))
                return actionsTypeInformation.filterFunctionsToInputTypes[filterCall.PackagePrefixedName].Count;
            else
                return 0; // auto-supplied
        }

        protected override string FilterFunctionParameterType(int i, SequenceFilterCall sequenceFilterCall, SequenceRuleCall seq)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst")
                return "int";
            if(filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction")
                return "double";
            if(filterCall.Name == "keepLast" || filterCall.Name == "removeLast")
                return "int";
            if(filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
                return "double";
            return actionsTypeInformation.filterFunctionsToInputTypes[filterCall.PackagePrefixedName][i];
        }

        protected override bool IsMatchClassExisting(SequenceFilterCall sequenceFilterCall)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;
            return Array.IndexOf(actionNames.matchClassNames, filterCall.MatchClassPackagePrefixedName) != -1;
        }

        protected override bool IsFilterExisting(SequenceFilterCall sequenceFilterCall, SequenceMultiRuleAllCall seq)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;

            if(filterCall.IsAutoSupplied)
                return true;

            String matchClassName = filterCall.MatchClassPackagePrefixedName;

            if(filterCall.IsAutoGenerated)
                return actionsTypeInformation.MatchClassContainsFilter(matchClassName, filterCall.FullName);

            return actionsTypeInformation.MatchClassContainsFilter(matchClassName, filterCall.PackagePrefixedName);
        }

        protected override int NumFilterFunctionParameters(SequenceFilterCall sequenceFilterCall, SequenceMultiRuleAllCall seq)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;
            if(filterCall.IsAutoSupplied)
                return 1;
            if(actionsTypeInformation.filterFunctionsToInputTypes.ContainsKey(filterCall.PackagePrefixedName))
                return actionsTypeInformation.filterFunctionsToInputTypes[filterCall.PackagePrefixedName].Count;
            else
                return 0; // auto-supplied
        }

        protected override string FilterFunctionParameterType(int i, SequenceFilterCall sequenceFilterCall, SequenceMultiRuleAllCall seq)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst")
                return "int";
            if(filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction")
                return "double";
            if(filterCall.Name == "keepLast" || filterCall.Name == "removeLast")
                return "int";
            if(filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
                return "double";
            return actionsTypeInformation.filterFunctionsToInputTypes[filterCall.PackagePrefixedName][i];
        }

        protected override string GetMatchClassName(SequenceFilterCall sequenceFilterCall)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;
            return filterCall.MatchClassPackagePrefixedName;
        }

        protected override string GetFilterCallName(SequenceFilterCall sequenceFilterCall)
        {
            FilterCall filterCall = ((SequenceFilterCallCompiled)sequenceFilterCall).FilterCall;
            return filterCall.PackagePrefixedName;
        }
    }
}
