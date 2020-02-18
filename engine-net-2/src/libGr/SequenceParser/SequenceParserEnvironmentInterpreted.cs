/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        public IActions Actions
        {
            get { return actions; }
        }


        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the interpreted xgrs.
        /// </summary>
        /// <param name="actions">The IActions object containing the known actions.</param>
        public SequenceParserEnvironmentInterpreted(IActions actions)
            : base(actions.Graph.Model)
        {
            this.actions = actions;
        }


        public override bool IsSequenceName(String ruleOrSequenceName, String package)
        {
            if(package != null)
            {
                return actions.GetSequenceDefinition(package + "::" + ruleOrSequenceName) != null;
            }
            else
            {
                ISequenceDefinition seqDef = actions.GetSequenceDefinition(ruleOrSequenceName);
                if(seqDef != null)
                    return true;
                return seqDef != null;
            }
        }

        public override SequenceSequenceCall CreateSequenceSequenceCall(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
        {
            ISequenceDefinition sequenceDef = null;
            if(packagePrefix != null)
            {
                sequenceDef = actions.GetSequenceDefinition(packagePrefix + "::" + sequenceName);
                if(sequenceDef == null)
                    throw new Exception("Unknown sequence: " + packagePrefix + "::" + sequenceName);
            }
            else
            {
                sequenceDef = actions.GetSequenceDefinition(sequenceName);
                if(sequenceDef == null)
                    throw new Exception("Unknown sequence: " + sequenceName);
            }

            SequenceSequenceCall seqSequenceCall = new SequenceSequenceCallInterpreted(sequenceDef,
                argExprs, returnVars, subgraph,
                special);

            return seqSequenceCall;
        }


        public override SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, List<FilterCall> filters, bool isRuleForMultiRuleAllCallReturningArrays)
        {
            IAction action = null;
            if(packagePrefix != null)
            {
                action = actions.GetAction(packagePrefix + "::" + ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + packagePrefix + "::" + ruleName);
            }
            else
            {
                action = actions.GetAction(ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + ruleName);
            }

            SequenceRuleCall seqRuleCall = new SequenceRuleCallInterpreted(action,
                argExprs, returnVars, subgraph,
                special, test, filters, isRuleForMultiRuleAllCallReturningArrays);

            return seqRuleCall;
        }

        public override SequenceRuleAllCall CreateSequenceRuleAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice, List<FilterCall> filters)
        {
            IAction action = null;
            if(packagePrefix != null)
            {
                action = actions.GetAction(packagePrefix + "::" + ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + packagePrefix + "::" + ruleName);
            }
            else
            {
                action = actions.GetAction(ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + ruleName);
            }

            SequenceRuleAllCall seqRuleAllCall = new SequenceRuleAllCallInterpreted(action,
                argExprs, returnVars, subgraph,
                special, test, filters,
                chooseRandom, varChooseRandom,
                chooseRandom2, varChooseRandom2, choice);

            return seqRuleAllCall;
        }

        public override SequenceRuleCountAllCall CreateSequenceRuleCountAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, SequenceVariable countResult, List<FilterCall> filters)
        {
            IAction action = null;
            if(packagePrefix != null)
            {
                action = actions.GetAction(packagePrefix + "::" + ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + packagePrefix + "::" + ruleName);
            }
            else
            {
                action = actions.GetAction(ruleName);
                if(action == null)
                    throw new Exception("Unknown rule: " + ruleName);
            }

            SequenceRuleCountAllCall seqRuleCountAllCall = new SequenceRuleCountAllCallInterpreted(action,
                argExprs, returnVars, subgraph,
                special, test, filters,
                countResult);

            return seqRuleCountAllCall;
        }

        public override bool IsFilterFunctionName(String filterFunctionName, String package, String ruleName, String actionPackage)
        {
            IAction action = null;
            if(actionPackage != null)
                action = actions.GetAction(actionPackage + "::" + ruleName);
            else
                action = actions.GetAction(ruleName);

            if(package != null)
            {
                foreach(IFilter filterFunc in action.RulePattern.Filters)
                {
                    if(filterFunc.PackagePrefixedName == filterFunctionName)
                        return true;
                    if(filterFunc.PackagePrefixedName == package + "::" + filterFunctionName)
                        return true;
                }
                return false;
            }
            else
            {
                foreach(IFilter filterFunc in action.RulePattern.Filters)
                {
                    if(filterFunc.PackagePrefixedName == filterFunctionName)
                        return true;
                }
                return false;
            }
        }


        public override bool IsProcedureName(String procedureName, String package)
        {
            if(package != null)
                return actions.GetProcedureDefinition(package + "::" + procedureName) != null;
            else
            {
                IProcedureDefinition procInfo = actions.GetProcedureDefinition(procedureName);
                return procInfo != null;
            }
        }

        public override string GetProcedureNames()
        {
            return ((BaseActions)actions).ProcedureNames;
        }

        public override SequenceComputationProcedureCall CreateSequenceComputationProcedureCall(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            IProcedureDefinition procedureDef = null;
            if(packagePrefix != null)
                procedureDef = actions.GetProcedureDefinition(packagePrefix + "::" + procedureName);
            else
                procedureDef = actions.GetProcedureDefinition(procedureName);
    
            return new SequenceComputationProcedureCallInterpreted(procedureDef, 
                argExprs, returnVars);
        }


        public override bool IsFunctionName(String functionName, String package)
        {
            if(package != null)
                return actions.GetFunctionDefinition(package + "::" + functionName) != null;
            else
            {
                IFunctionDefinition funcInfo = actions.GetFunctionDefinition(functionName);
                return funcInfo != null;
            }
        }

        public override string GetFunctionNames()
        {
            return ((BaseActions)actions).FunctionNames;
        }

        public override SequenceExpressionFunctionCall CreateSequenceExpressionFunctionCall(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs)
        {
            IFunctionDefinition functionDef = null;
            if(packagePrefix != null)
                functionDef = actions.GetFunctionDefinition(packagePrefix + "::" + functionName);
            else
                functionDef = actions.GetFunctionDefinition(functionName);

            return new SequenceExpressionFunctionCallInterpreted(functionDef, argExprs);
        }
    }
}
