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
    /// Concrete subclass for compiled sequences.
    /// This environment in addition resolves names that are given without package context but do not reference global names
    /// because they are used from a sequence that is contained in a package (only possible for compiled sequences from rule language).
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

        ActionNames actionNames;

        ActionsTypeInformation actionsTypeInformation;

        // returns rule or sequence name to input types dictionary depending on argument
        private Dictionary<String, List<String>> toInputTypes(bool rule) { return rule ? actionsTypeInformation.rulesToInputTypes : actionsTypeInformation.sequencesToInputTypes; }

        // returns rule or sequence name to output types dictionary depending on argument
        private Dictionary<String, List<String>> toOutputTypes(bool rule) { return rule ? actionsTypeInformation.rulesToOutputTypes : actionsTypeInformation.sequencesToOutputTypes; }

        // the model object of the .grg to compile
        private IGraphModel model;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the model giving access to graph element types for checking
        /// </summary>
        public override IGraphModel Model { get { return model; } }

        public override bool IsProcedureCallExternal(ProcedureInvocationParameterBindings paramBindings)
        {
            return actionsTypeInformation.proceduresToIsExternal[paramBindings.PackagePrefixedName];
        }

        public override bool IsFunctionCallExternal(FunctionInvocationParameterBindings paramBindings)
        {
            return actionsTypeInformation.functionsToIsExternal[paramBindings.PackagePrefixedName];
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
                    return Array.IndexOf(actionNames.ruleNames, ruleParamBindings.PrePackage + "::" + ruleParamBindings.Name) != -1;
                }
                else
                {
                    if(Array.IndexOf(actionNames.ruleNames, ruleParamBindings.Name) != -1)
                    {
                        ruleParamBindings.Package = null;
                        ruleParamBindings.PackagePrefixedName = ruleParamBindings.Name;
                        return true;
                    }
                    if(ruleParamBindings.PrePackageContext != null)
                    {
                        ruleParamBindings.Package = ruleParamBindings.PrePackageContext;
                        ruleParamBindings.PackagePrefixedName = ruleParamBindings.PrePackageContext + "::" + ruleParamBindings.Name;
                        return Array.IndexOf(actionNames.ruleNames, ruleParamBindings.PrePackageContext + "::" + ruleParamBindings.Name) != -1;
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
                    return Array.IndexOf(actionNames.sequenceNames, seqParamBindings.PrePackage + "::" + seqParamBindings.Name) != -1;
                }
                else
                {
                    if(Array.IndexOf(actionNames.sequenceNames, seqParamBindings.Name) != -1)
                    {
                        seqParamBindings.Package = null;
                        seqParamBindings.PackagePrefixedName = seqParamBindings.Name;
                        return true;
                    }
                    if(seqParamBindings.PrePackageContext != null)
                    {
                        seqParamBindings.Package = seqParamBindings.PrePackageContext;
                        seqParamBindings.PackagePrefixedName = seqParamBindings.PrePackageContext + "::" + seqParamBindings.Name;
                        return Array.IndexOf(actionNames.sequenceNames, seqParamBindings.PrePackageContext + "::" + seqParamBindings.Name) != -1;
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
                        return Array.IndexOf(actionNames.procedureNames, procParamBindings.PrePackage + "::" + procParamBindings.Name) != -1;
                    }
                    else
                    {
                        if(Array.IndexOf(actionNames.procedureNames, procParamBindings.Name) != -1)
                        {
                            procParamBindings.Package = null;
                            procParamBindings.PackagePrefixedName = procParamBindings.Name;
                            return true;
                        }
                        if(procParamBindings.PrePackageContext != null)
                        {
                            procParamBindings.Package = procParamBindings.PrePackageContext;
                            procParamBindings.PackagePrefixedName = procParamBindings.PrePackageContext + "::" + procParamBindings.Name;
                            return Array.IndexOf(actionNames.procedureNames, procParamBindings.PrePackageContext + "::" + procParamBindings.Name) != -1;
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
                        return Array.IndexOf(actionNames.functionNames, funcParamBindings.PrePackage + "::" + funcParamBindings.Name) != -1;
                    }
                    else
                    {
                        if(Array.IndexOf(actionNames.functionNames, funcParamBindings.Name) != -1)
                        {
                            funcParamBindings.Package = null;
                            funcParamBindings.PackagePrefixedName = funcParamBindings.Name;
                            return true;
                        }
                        if(funcParamBindings.PrePackageContext != null)
                        {
                            funcParamBindings.Package = funcParamBindings.PrePackageContext;
                            funcParamBindings.PackagePrefixedName = funcParamBindings.PrePackageContext + "::" + funcParamBindings.Name;
                            return Array.IndexOf(actionNames.functionNames, funcParamBindings.PrePackageContext + "::" + funcParamBindings.Name) != -1;
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
                return actionsTypeInformation.rulesToInputTypes[ruleParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return actionsTypeInformation.sequencesToInputTypes[seqParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procParamBindings.Name).Inputs.Length;
                else
                    return actionsTypeInformation.proceduresToInputTypes[procParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcParamBindings.Name).Inputs.Length;
                else
                    return actionsTypeInformation.functionsToInputTypes[funcParamBindings.PackagePrefixedName].Count;
            }
            throw new Exception("Internal error");
        }

        protected override int NumOutputParameters(InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return actionsTypeInformation.rulesToOutputTypes[ruleParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return actionsTypeInformation.sequencesToOutputTypes[seqParamBindings.PackagePrefixedName].Count;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procParamBindings.Name).Outputs.Length;
                else
                    return actionsTypeInformation.proceduresToOutputTypes[procParamBindings.PackagePrefixedName].Count;
            }
            throw new Exception("Internal error");
        }

        protected override string InputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return actionsTypeInformation.rulesToInputTypes[ruleParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return actionsTypeInformation.sequencesToInputTypes[seqParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procParamBindings.Name).Inputs[i]);
                else
                    return actionsTypeInformation.proceduresToInputTypes[procParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is FunctionInvocationParameterBindings)
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetFunctionMethod(funcParamBindings.Name).Inputs[i]);
                else
                    return actionsTypeInformation.functionsToInputTypes[funcParamBindings.PackagePrefixedName][i];
            }
            throw new Exception("Internal error");
        }

        protected override string OutputParameterType(int i, InvocationParameterBindings paramBindings, GrGenType ownerType)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return actionsTypeInformation.rulesToOutputTypes[ruleParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return actionsTypeInformation.sequencesToOutputTypes[seqParamBindings.PackagePrefixedName][i];
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procParamBindings.Name).Outputs[i]);
                else
                    return actionsTypeInformation.proceduresToOutputTypes[procParamBindings.PackagePrefixedName][i];
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
                return filterCall.IsContainedIn(actionsTypeInformation.rulesToFilters[seq.ParamBindings.PackagePrefixedName]);
            }
            else
            {
                filterCall.Package = null;
                filterCall.PackagePrefixedName = filterCall.Name;
                if(filterCall.IsContainedIn(actionsTypeInformation.rulesToFilters[seq.ParamBindings.PackagePrefixedName]))
                    return true;
                if(filterCall.PrePackageContext != null)
                {
                    filterCall.Package = filterCall.PrePackageContext;
                    filterCall.PackagePrefixedName = filterCall.PrePackageContext + "::" + filterCall.Name;
                    if(filterCall.IsContainedIn(actionsTypeInformation.rulesToFilters[seq.ParamBindings.PackagePrefixedName]))
                        return true;
                }
                if(filterCall.IsAutoGenerated && seq.ParamBindings.Package != null)
                {
                    filterCall.Package = seq.ParamBindings.Package;
                    filterCall.PackagePrefixedName = seq.ParamBindings.Package + "::" + filterCall.Name;
                    return filterCall.IsContainedIn(actionsTypeInformation.rulesToFilters[seq.ParamBindings.PackagePrefixedName]);
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
            if(actionsTypeInformation.filterFunctionsToInputTypes.ContainsKey(filterCall.PackagePrefixedName))
                return actionsTypeInformation.filterFunctionsToInputTypes[filterCall.PackagePrefixedName].Count;
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
            return actionsTypeInformation.filterFunctionsToInputTypes[filterCall.PackagePrefixedName][i];
        }
    }
}
