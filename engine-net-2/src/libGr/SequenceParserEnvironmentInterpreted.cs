/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr.sequenceParser
{
    /// <summary>
    /// An evironment class for the sequence parser, gives it access to the entitites and types that can be referenced in the sequence.
    /// </summary>
    public class SequenceParserEnvironmentInterpreted : SequenceParserEnvironment
    {
        /// <summary>
        /// The rules and sequences used in the specification, set if parsing an xgrs to be interpreted
        /// </summary>
        private readonly IActions actions;
        public IActions Actions { get { return actions; } }


        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the interpreted xgrs.
        /// </summary>
        /// <param name="actions">The IActions object containing the known actions.</param>
        public SequenceParserEnvironmentInterpreted(IActions actions) : base(actions.Graph.Model)
        {
            this.actions = actions;
        }

        public override RuleInvocationParameterBindings CreateRuleInvocationParameterBindings(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph)
        {
            IAction action = null;
            if(packagePrefix != null) {
                action = actions.GetAction(packagePrefix + "::" + ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + packagePrefix + "::" + ruleName);
            } else {
                action = actions.GetAction(ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + ruleName);
            }

            RuleInvocationParameterBindings paramBindings = new RuleInvocationParameterBindings(action,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray(), subgraph);

            return paramBindings;
        }

        public override SequenceInvocationParameterBindings CreateSequenceInvocationParameterBindings(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph)
        {
            ISequenceDefinition sequenceDef = null;
            if(packagePrefix != null) {
                sequenceDef = actions.GetSequenceDefinition(packagePrefix + "::" + sequenceName);
                if(sequenceDef == null)
                    throw new Exception("Unknown sequence: " + packagePrefix + "::" + sequenceName);
            } else {
                sequenceDef = actions.GetSequenceDefinition(sequenceName);
                if(sequenceDef == null)
                    throw new Exception("Unknown sequence: " + sequenceName);
            }

            SequenceInvocationParameterBindings paramBindings = new SequenceInvocationParameterBindings(sequenceDef,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray(), subgraph);

            return paramBindings;
        }

        public override ProcedureInvocationParameterBindings CreateProcedureInvocationParameterBindings(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            IProcedureDefinition procedureDef = null;
            if(packagePrefix != null) {
                procedureDef = actions.GetProcedureDefinition(packagePrefix + "::" + procedureName);
            } else {
                procedureDef = actions.GetProcedureDefinition(procedureName);
            }

            ProcedureInvocationParameterBindings paramBindings = new ProcedureInvocationParameterBindings(procedureDef,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());
    
            return paramBindings;
        }

        public override FunctionInvocationParameterBindings CreateFunctionInvocationParameterBindings(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs)
        {
            IFunctionDefinition functionDef = null;
            if(packagePrefix != null) {
                functionDef = actions.GetFunctionDefinition(packagePrefix + "::" + functionName);
            } else {
                functionDef = actions.GetFunctionDefinition(functionName);
            }

            FunctionInvocationParameterBindings paramBindings = new FunctionInvocationParameterBindings(functionDef,
                argExprs.ToArray(), new object[argExprs.Count]);

            return paramBindings;
        }

        public override bool IsSequenceName(String ruleOrSequenceName, String package)
        {
            if(package != null) {
                return actions.GetSequenceDefinition(package + "::" + ruleOrSequenceName) != null;
            } else {
                ISequenceDefinition seqDef = actions.GetSequenceDefinition(ruleOrSequenceName);
                if(seqDef != null)
                    return true;
                return seqDef != null;
            }
        }

        public override bool IsFunctionName(String functionName, String package)
        {
            if(package != null) {
                return actions.GetFunctionDefinition(package + "::" + functionName) != null;
            } else {
                IFunctionDefinition funcInfo = actions.GetFunctionDefinition(functionName);
                return funcInfo != null;
            }
        }

        public override string GetFunctionNames()
        {
            return ((BaseActions)actions).FunctionNames;
        }

        public override bool IsProcedureName(String procedureName, String package)
        {
            if(package != null) {
                return actions.GetProcedureDefinition(package + "::" + procedureName) != null;
            } else {
                IProcedureDefinition procInfo = actions.GetProcedureDefinition(procedureName);
                return procInfo != null;
            }
        }

        public override string GetProcedureNames()
        {
            return ((BaseActions)actions).ProcedureNames;
        }

        public override bool IsFilterFunctionName(String filterFunctionName, String package, String ruleName, String actionPackage)
        {
            IAction action = null;
            if(actionPackage != null) {
                action = actions.GetAction(actionPackage + "::" + ruleName);
            } else {
                action = actions.GetAction(ruleName);
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
                }
                return false;
            }
        }
    }
}
