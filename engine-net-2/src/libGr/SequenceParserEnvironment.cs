/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Text;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr.sequenceParser
{
    /// <summary>
    /// An evironment class for the sequence parser, gives it access to the entitites and types that can be referenced in the sequence.
    /// </summary>
    public class SequenceParserEnvironment
    {
        /// <summary>
        /// The rules and sequences used in the specification, set if parsing an xgrs to be interpreted
        /// </summary>
        internal readonly IActions actions;

        /// <summary>
        /// The names of the different kinds of action used in the specification, set if parsing an xgrs to be compiled
        /// </summary>
        internal readonly ActionNames actionNames;
        
        /// <summary>
        /// The model used in the specification
        /// </summary>
        internal readonly IGraphModel model;

        /// <summary>
        /// The name of the package the sequence is contained in (defining some context), null if it is not contained in a package. (Applies only to compiled sequences.)
        /// </summary>
        internal readonly String packageContext;

        /// <summary>
        /// Gives the rule of the match this stands for in the if clause of the debug match event.
        /// </summary>
        internal readonly string ruleOfMatchThis;

        /// <summary>
        /// Gives the graph element type of the graph element this stands for in the if clause of the debug new/delete/retype/set-attributes event.
        /// </summary>
        internal readonly string typeOfGraphElementThis;

        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the interpreted xgrs.
        /// </summary>
        /// <param name="actions">The IActions object containing the known actions.</param>
        public SequenceParserEnvironment(IActions actions)
        {
            this.actions = actions;
            this.actionNames = null;
            this.model = actions.Graph.Model;
        }

        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the interpreted if clauses for conditional watchpoint debugging.
        /// </summary>
        /// <param name="actions">The IActions object containing the known actions.</param>
        /// <param name="ruleOfMatchThis">Gives the rule of the match this stands for in the if clause of the debug match event.</param>
        /// <param name="typeOfGraphElementThis">Gives the graph element type of the graph element this stands for in the if clause of the debug new/delete/retype/set-attributes event.</param>
        public SequenceParserEnvironment(IActions actions, string ruleOfMatchThis, string typeOfGraphElementThis)
        {
            this.actions = actions;
            this.actionNames = null;
            this.model = actions.Graph.Model;
            this.ruleOfMatchThis = ruleOfMatchThis;
            this.typeOfGraphElementThis = typeOfGraphElementThis;
        }

        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the compiled xgrs.
        /// </summary>
        /// <param name="packageContext">The name of the package the sequence is contained in (defining some context), null if it is not contained in a package.</param>
        /// <param name="actionNames">Contains the names of the different kinds of actions used in the specification.</param>
        /// <param name="model">The model used in the specification.</param>
        public SequenceParserEnvironment(String packageContext, ActionNames actionNames, IGraphModel model)
        {
            this.actions = null;
            this.actionNames = actionNames;
            this.model = model;
            this.packageContext = packageContext;
        }

        internal RuleInvocationParameterBindings CreateRuleInvocationParameterBindings(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph)
        {
            IAction action = null;
            if(actions != null)
            {
                if(packagePrefix != null) {
                    action = actions.GetAction(packagePrefix + "::" + ruleName);
                    if(action == null)
                        throw new Exception("Unknown rule: " + packagePrefix + "::" + ruleName);
                } else {
                    action = actions.GetAction(ruleName);
                    if(action == null && packageContext != null)
                        action = actions.GetAction(packageContext + "::" + ruleName);
                    if(action == null)
                        throw new Exception("Unknown rule: " + ruleName);
                }
            }

            RuleInvocationParameterBindings paramBindings = new RuleInvocationParameterBindings(action,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray(), subgraph);

            if(action == null)
            {
                paramBindings.Name = ruleName;
                paramBindings.PrePackage = packagePrefix;
                paramBindings.PrePackageContext = packageContext;
            }

            return paramBindings;
        }

        internal SequenceInvocationParameterBindings CreateSequenceInvocationParameterBindings(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph)
        {
            ISequenceDefinition sequenceDef = null;
            if(actions != null)
            {
                if(packagePrefix != null) {
                    sequenceDef = actions.GetSequenceDefinition(packagePrefix + "::" + sequenceName);
                    if(sequenceDef == null)
                        throw new Exception("Unknown sequence: " + packagePrefix + "::" + sequenceName);
                } else {
                    sequenceDef = actions.GetSequenceDefinition(sequenceName);
                    if(sequenceDef == null && packageContext != null)
                        sequenceDef = actions.GetSequenceDefinition(packageContext + "::" + sequenceName);
                    if(sequenceDef == null)
                        throw new Exception("Unknown sequence: " + sequenceName);
                }
            }

            SequenceInvocationParameterBindings paramBindings = new SequenceInvocationParameterBindings(sequenceDef,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray(), subgraph);

            if(sequenceDef == null)
            {
                paramBindings.Name = sequenceName;
                paramBindings.PrePackage = packagePrefix;
                paramBindings.PrePackageContext = packageContext;
            }

            return paramBindings;
        }

        internal ProcedureInvocationParameterBindings CreateProcedureInvocationParameterBindings(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            IProcedureDefinition procedureDef = null;
            if(actions != null)
            {
                if(packagePrefix != null) {
                    procedureDef = actions.GetProcedureDefinition(packagePrefix + "::" + procedureName);
                } else {
                    procedureDef = actions.GetProcedureDefinition(procedureName);
                    if(procedureDef == null && packageContext != null)
                        procedureDef = actions.GetProcedureDefinition(packageContext + "::" + procedureName);
                }
            }

            ProcedureInvocationParameterBindings paramBindings = new ProcedureInvocationParameterBindings(procedureDef,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());

            if(procedureDef == null)
            {
                paramBindings.Name = procedureName;
                paramBindings.PrePackage = packagePrefix;
                paramBindings.PrePackageContext = packageContext;
            }
    
            return paramBindings;
        }

        internal ProcedureInvocationParameterBindings CreateProcedureMethodInvocationParameterBindings(String procedureName,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            ProcedureInvocationParameterBindings paramBindings = new ProcedureInvocationParameterBindings(null,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());

            paramBindings.Name = procedureName;

            return paramBindings;
        }

        internal FunctionInvocationParameterBindings CreateFunctionInvocationParameterBindings(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs)
        {
            IFunctionDefinition functionDef = null;
            if(actions != null)
            {
                if(packagePrefix != null) {
                    functionDef = actions.GetFunctionDefinition(packagePrefix + "::" + functionName);
                } else {
                    functionDef = actions.GetFunctionDefinition(functionName);
                    if(functionDef == null && packageContext != null)
                        functionDef = actions.GetFunctionDefinition(packageContext + "::" + functionName);
                }
            }

            FunctionInvocationParameterBindings paramBindings = new FunctionInvocationParameterBindings(functionDef,
                argExprs.ToArray(), new object[argExprs.Count]);

            if(functionDef == null) {
                paramBindings.Name = functionName;
                paramBindings.PrePackage = packagePrefix;
                paramBindings.PrePackageContext = packageContext;
        
                for(int i=0; i<actionNames.functionNames.Length; ++i)
                    if(actionNames.functionNames[i] == functionName)
                        paramBindings.ReturnType = actionNames.functionOutputTypes[i];
            }

            return paramBindings;
        }

        internal FunctionInvocationParameterBindings CreateFunctionMethodInvocationParameterBindings(String functionMethodName,
            List<SequenceExpression> argExprs)
        {
            FunctionInvocationParameterBindings paramBindings = new FunctionInvocationParameterBindings(null,
                argExprs.ToArray(), new object[argExprs.Count]);

            paramBindings.Name = functionMethodName;
            paramBindings.ReturnType = "";

            return paramBindings;
        }

        internal bool IsSequenceName(String ruleOrSequenceName, String package)
        {
            if(actions != null) {
                if(package != null) {
                    return actions.GetSequenceDefinition(package + "::" + ruleOrSequenceName) != null;
                } else {
                    ISequenceDefinition seqDef = actions.GetSequenceDefinition(ruleOrSequenceName);
                    if(seqDef != null)
                        return true;
                    if(packageContext != null)
                        seqDef = actions.GetSequenceDefinition(packageContext + "::" + ruleOrSequenceName);
                    return seqDef != null;
                }
            } else {
                if(package != null) {
                    foreach(String sequenceName in actionNames.sequenceNames)
                    {
                        if(sequenceName == package + "::" + ruleOrSequenceName)
                            return true;
                    }
                    return false;
                } else {
                    foreach(String sequenceName in actionNames.sequenceNames)
                    {
                        if(sequenceName == ruleOrSequenceName)
                            return true;
                        if(packageContext != null && sequenceName == packageContext + "::" + ruleOrSequenceName)
                            return true;
                    }
                    return false;
                }
            }
        }

        internal bool IsFunctionName(String functionName, String package)
        {
            if(actions != null)
            {
                if(package != null) {
                    return actions.GetFunctionDefinition(package + "::" + functionName) != null;
                } else {
                    IFunctionDefinition funcInfo = actions.GetFunctionDefinition(functionName);
                    if(funcInfo != null)
                        return true;
                    if(packageContext != null)
                        funcInfo = actions.GetFunctionDefinition(packageContext + "::" + functionName);
                    return funcInfo != null;
                }
            }
            else
            {
                if(package != null) {
                    foreach(String funcName in actionNames.functionNames)
                    {
                        if(funcName == package + "::" + functionName)
                            return true;
                    }
                    return false;
                } else {
                    foreach(String funcName in actionNames.functionNames)
                    {
                        if(funcName == functionName)
                            return true;
                        if(packageContext != null && funcName == packageContext + "::" + functionName)
                            return true;
                    }
                    return false;
                }
            }
        }

        internal string GetFunctionNames()
        {
            if(actions != null) {
                return ((BaseActions)actions).FunctionNames;
            } else {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach(String funcName in actionNames.functionNames)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(funcName);
                }
                return sb.ToString();
            }
        }

        internal bool IsProcedureName(String procedureName, String package)
        {
            if(actions != null)
            {
                if(package != null) {
                    return actions.GetProcedureDefinition(package + "::" + procedureName) != null;
                } else {
                    IProcedureDefinition procInfo = actions.GetProcedureDefinition(procedureName);
                    if(procInfo != null)
                        return true;
                    if(packageContext != null)
                        procInfo = actions.GetProcedureDefinition(packageContext + "::" + procedureName);
                    return procInfo != null;
                }
            }
            else
            {
                if(package != null) {
                    foreach(String procName in actionNames.procedureNames)
                    {
                        if(procName == package + "::" + procedureName)
                            return true;
                    }
                    return false;
                } else {
                    foreach(String procName in actionNames.procedureNames)
                    {
                        if(procName == procedureName)
                            return true;
                        if(packageContext != null && procName == packageContext + "::" + procedureName)
                            return true;
                    }
                    return false;
                }
            }
        }

        internal string GetProcedureNames()
        {
            if(actions != null) {
                return ((BaseActions)actions).ProcedureNames;
            } else {
                StringBuilder sb = new StringBuilder();
                bool first = true;
                foreach(String procName in actionNames.procedureNames)
                {
                    if(first)
                        first = false;
                    else
                        sb.Append(",");
                    sb.Append(procName);
                }
                return sb.ToString();
            }
        }

        internal bool IsFilterFunctionName(String filterFunctionName, String package, String ruleName, String actionPackage)
        {
            if(actions != null)
            {
                IAction action = null;
                if(actionPackage != null) {
                    action = actions.GetAction(actionPackage + "::" + ruleName);
                } else {
                    action = actions.GetAction(ruleName);
                    if(action == null && packageContext != null)
                        action = actions.GetAction(packageContext + "::" + ruleName);
                }

                if(package != null) {
                    foreach(IFilter filterFunc in action.RulePattern.Filters)
                    {
                        if(filterFunc.PackagePrefixedName == filterFunctionName)
                            return true;
                        if(filterFunc.PackagePrefixedName == package + "::" + filterFunctionName)
                            return true;
                    }
                    return false;
                } else {
                    foreach(IFilter filterFunc in action.RulePattern.Filters)
                    {
                        if(filterFunc.PackagePrefixedName == filterFunctionName)
                            return true;
                        if(packageContext != null && filterFunc.PackagePrefixedName == packageContext + "::" + filterFunctionName)
                            return true;
                    }
                    return false;
                }
            }
            else
            {
                if(package != null) {
                    foreach(String funcName in actionNames.filterFunctionNames)
                    {
                        if(funcName == filterFunctionName)
                            return true;
                        if(funcName == package + "::" + filterFunctionName)
                            return true;
                    }
                    return false;
                } else {
                    foreach(String funcName in actionNames.filterFunctionNames)
                    {
                        if(funcName == filterFunctionName)
                            return true;
                        if(packageContext != null && funcName == packageContext + "::" + filterFunctionName)
                            return true;
                    }
                    return false;
                }
            }
        }
    }
}
