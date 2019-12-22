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
    public class SequenceParserEnvironmentCompiled : SequenceParserEnvironment
    {
        /// <summary>
        /// The names of the different kinds of action used in the specification, set if parsing an xgrs to be compiled
        /// </summary>
        private readonly ActionNames actionNames;
        public ActionNames ActionNames { get { return actionNames; } }

        /// <summary>
        /// The name of the package the sequence is contained in (defining some context), null if it is not contained in a package.
        /// Also null in case of an interpreted sequence, only compiled sequences may appear within a package.
        /// </summary>
        private readonly String packageContext;
        public override String PackageContext { get { return packageContext; } }


        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the compiled xgrs.
        /// </summary>
        /// <param name="packageContext">The name of the package the sequence is contained in (defining some context), null if it is not contained in a package.</param>
        /// <param name="actionNames">Contains the names of the different kinds of actions used in the specification.</param>
        /// <param name="model">The model used in the specification.</param>
        public SequenceParserEnvironmentCompiled(String packageContext, ActionNames actionNames, IGraphModel model) : base(model)
        {
            this.actionNames = actionNames;
            this.packageContext = packageContext;
        }


        public override bool IsSequenceName(String ruleOrSequenceName, String package)
        {
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

        public override SequenceSequenceCall CreateSequenceSequenceCall(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
        {
            SequenceSequenceCall seqSequenceCall = new SequenceSequenceCallCompiled(sequenceName, packagePrefix, packageContext, 
                Array.IndexOf(actionNames.sequenceNames, sequenceName) != -1,
                argExprs, returnVars, subgraph,
                special);

            return seqSequenceCall;
        }


        public override SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, List<FilterCall> filters)
        {
            SequenceRuleCall seqRuleCall = new SequenceRuleCallCompiled(ruleName, packagePrefix, packageContext,
                Array.IndexOf(actionNames.ruleNames, ruleName) != -1,
                argExprs, returnVars, subgraph,
                special, test, filters);

            return seqRuleCall;
        }

        public override SequenceRuleAllCall CreateSequenceRuleAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice, List<FilterCall> filters)
        {
            SequenceRuleAllCall seqRuleAllCall = new SequenceRuleAllCallCompiled(ruleName, packagePrefix, packageContext,
                Array.IndexOf(actionNames.ruleNames, ruleName) != -1,
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
            SequenceRuleCountAllCall seqRuleCountAllCall = new SequenceRuleCountAllCallCompiled(ruleName, packagePrefix, packageContext,
                Array.IndexOf(actionNames.ruleNames, ruleName) != -1,
                argExprs, returnVars, subgraph,
                special, test, filters,
                countResult);

            return seqRuleCountAllCall;
        }

        public override bool IsFilterFunctionName(String filterFunctionName, String package, String ruleName, String actionPackage)
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


        public override bool IsProcedureName(String procedureName, String package)
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

        public override string GetProcedureNames()
        {
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

        public override SequenceComputationProcedureCall CreateSequenceComputationProcedureCall(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {    
            return new SequenceComputationProcedureCallCompiled(procedureName, packagePrefix, packageContext,
                Array.IndexOf(actionNames.procedureNames, procedureName) != -1,
                argExprs, returnVars);
        }


        public override bool IsFunctionName(String functionName, String package)
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

        public override string GetFunctionNames()
        {
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

        public override SequenceExpressionFunctionCall CreateSequenceExpressionFunctionCall(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs)
        {
            String returnType = null;
            for(int i=0; i<actionNames.functionNames.Length; ++i)
                if(actionNames.functionNames[i] == functionName)
                    returnType = actionNames.functionOutputTypes[i];

            return new SequenceExpressionFunctionCallCompiled(functionName, packagePrefix, packageContext,
                Array.IndexOf(actionNames.functionNames, functionName) != -1,
                returnType,
                argExprs);
        }
    }
}
