/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Text;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr.sequenceParser
{
    /// <summary>
    /// An environment class for the sequence parser, 
    /// gives it access to the entitites (and types) that can be referenced in the sequence, and works as a factory for call objects.
    /// Concrete subclass for compiled sequences.
    /// </summary>
    public class SequenceParserEnvironmentCompiled : SequenceParserEnvironment
    {
        /// <summary>
        /// The names of the different kinds of action used in the specification, set if parsing an xgrs to be compiled
        /// </summary>
        private readonly ActionNames actionNames;
        public ActionNames ActionNames
        {
            get { return actionNames; }
        }

        /// <summary>
        /// The name of the package the sequence is contained in (defining some context), null if it is not contained in a package.
        /// Also null in case of an interpreted sequence, only compiled sequences may appear within a package.
        /// </summary>
        private readonly String packageContext;
        public override String PackageContext
        {
            get { return packageContext; }
        }


        /// <summary>
        /// Creates the environment that sets the context for the sequence parser, containing the entitites and types that can be referenced.
        /// Used for the compiled xgrs.
        /// </summary>
        /// <param name="packageContext">The name of the package the sequence is contained in (defining some context), null if it is not contained in a package.</param>
        /// <param name="actionNames">Contains the names of the different kinds of actions used in the specification.</param>
        /// <param name="model">The model used in the specification.</param>
        public SequenceParserEnvironmentCompiled(String packageContext, ActionNames actionNames, IGraphModel model)
            : base(model)
        {
            this.actionNames = actionNames;
            this.packageContext = packageContext;
        }


        public override bool IsSequenceName(String ruleOrSequenceName, String package)
        {
            if(package != null)
            {
                if(package != "global")
                {
                    foreach(String sequenceName in actionNames.sequenceNames)
                    {
                        if(sequenceName == package + "::" + ruleOrSequenceName)
                            return true;
                    }
                    return false;
                }
                else
                {
                    foreach(String sequenceName in actionNames.sequenceNames)
                    {
                        if(sequenceName == ruleOrSequenceName)
                            return true;
                    }
                    return false;
                }
            }
            else
            {
                foreach(String sequenceName in actionNames.sequenceNames)
                {
                    if(packageContext != null && sequenceName == packageContext + "::" + ruleOrSequenceName)
                        return true;
                    if(sequenceName == ruleOrSequenceName)
                        return true;
                }
                return false;
            }
        }

        public override SequenceSequenceCall CreateSequenceSequenceCall(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
        {
            String package;
            String packagePrefixedName;
            ResolvePackage(sequenceName, packagePrefix, packageContext, actionNames.ContainsSequence(PackagePrefixedName(sequenceName, packageContext)),
                out package, out packagePrefixedName);
            if(!actionNames.ContainsSequence(packagePrefixedName))
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Sequence, CallIssueType.UnknownRuleOrSequence);

            SequenceSequenceCall seqSequenceCall = new SequenceSequenceCallCompiled(sequenceName, package, packagePrefixedName,
                argExprs, returnVars, subgraph,
                special);

            return seqSequenceCall;
        }


        public override bool IsRuleName(String ruleOrSequenceName, String package)
        {
            if(package != null)
            {
                if(package != "global")
                {
                    foreach(String ruleName in actionNames.ruleNames)
                    {
                        if(ruleName == package + "::" + ruleOrSequenceName)
                            return true;
                    }
                    return false;
                }
                else
                {
                    foreach(String ruleName in actionNames.ruleNames)
                    {
                        if(ruleName == ruleOrSequenceName)
                            return true;
                    }
                    return false;
                }
            }
            else
            {
                foreach(String ruleName in actionNames.ruleNames)
                {
                    if(packageContext != null && ruleName == packageContext + "::" + ruleOrSequenceName)
                        return true;
                    if(ruleName == ruleOrSequenceName)
                        return true;
                }
                return false;
            }
        }

        public override SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays)
        {
            String package;
            String packagePrefixedName;
            ResolvePackage(ruleName, packagePrefix, packageContext, actionNames.ContainsRule(PackagePrefixedName(ruleName, packageContext)), 
                out package, out packagePrefixedName);
            if(!actionNames.ContainsRule(packagePrefixedName))
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Action, CallIssueType.UnknownRuleOrSequence);

            SequenceRuleCall seqRuleCall = new SequenceRuleCallCompiled(ruleName, package, packagePrefixedName,
                argExprs, returnVars, subgraph,
                special, test, isRuleForMultiRuleAllCallReturningArrays);

            return seqRuleCall;
        }

        public override SequenceRuleAllCall CreateSequenceRuleAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice)
        {
            String package;
            String packagePrefixedName;
            ResolvePackage(ruleName, packagePrefix, packageContext, actionNames.ContainsRule(PackagePrefixedName(ruleName, packageContext)),
                out package, out packagePrefixedName);
            if(!actionNames.ContainsRule(packagePrefixedName))
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Action, CallIssueType.UnknownRuleOrSequence);

            SequenceRuleAllCall seqRuleAllCall = new SequenceRuleAllCallCompiled(ruleName, package, packagePrefixedName,
                argExprs, returnVars, subgraph,
                special, test,
                chooseRandom, varChooseRandom,
                chooseRandom2, varChooseRandom2, choice);

            return seqRuleAllCall;
        }

        public override SequenceRuleCountAllCall CreateSequenceRuleCountAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test)
        {
            String package;
            String packagePrefixedName;
            ResolvePackage(ruleName, packagePrefix, packageContext, actionNames.ContainsRule(PackagePrefixedName(ruleName, packageContext)),
                out package, out packagePrefixedName);
            if(!actionNames.ContainsRule(packagePrefixedName))
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Action, CallIssueType.UnknownRuleOrSequence);

            SequenceRuleCountAllCall seqRuleCountAllCall = new SequenceRuleCountAllCallCompiled(ruleName, package, packagePrefixedName,
                argExprs, returnVars, subgraph,
                special, test);

            return seqRuleCountAllCall;
        }

        public override SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String packagePrefixedRuleName = rulePackage != null ? rulePackage + "::" + ruleName : ruleName; // rulePackage already resolved, concatenation sufficient

            String filterName = GetFilterName(filterBase, entities);
            String packagePrefixedName;
            if(IsAutoSuppliedFilterName(filterBase))
                packagePrefixedName = filterBase;
            else if(IsAutoGeneratedFilter(filterBase, entities))
                packagePrefixedName = filterName;
            else
            {
                if(packagePrefix != null)
                {
                    if(packagePrefix != "global")
                        packagePrefixedName = packagePrefix + "::" + filterName;
                    else
                        packagePrefixedName = filterName;
                }
                else if(rulePackage != null && actionNames.GetFilterOfRule(packagePrefixedRuleName, rulePackage + "::" + filterName) != null)
                    packagePrefixedName = rulePackage + "::" + filterName;
                else if(packageContext != null && actionNames.GetFilterOfRule(packagePrefixedRuleName, packageContext + "::" + filterName) != null)
                    packagePrefixedName = packageContext + "::" + filterName;
                else
                    packagePrefixedName = filterName;
            }

            IFilter filter = actionNames.GetFilterOfRule(packagePrefixedRuleName, packagePrefixedName);
            if(filter == null)
                throw new SequenceParserExceptionFilterError(packagePrefixedRuleName, filterName);

            return new SequenceFilterCallCompiled(filter, argExprs.ToArray());
        }

        public override SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String packagePrefixedRuleName = rulePackage != null ? rulePackage + "::" + ruleName : ruleName; // rulePackage already resolved, concatenation sufficient

            String filterName = GetFilterName(filterBase, entities);
            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assign")
                return new SequenceFilterCallLambdaExpressionCompiled(filterBase, entities[0], arrayAccess, index, element, lambdaExpr);
            else if(entities.Count == 0 && filterBase == "removeIf")
                return new SequenceFilterCallLambdaExpressionCompiled(filterBase, null, arrayAccess, index, element, lambdaExpr);
            else
                throw new SequenceParserExceptionFilterError(packagePrefixedRuleName, filterName);
        }

        public override SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String packagePrefixedRuleName = rulePackage != null ? rulePackage + "::" + ruleName : ruleName; // rulePackage already resolved, concatenation sufficient

            String filterName = GetFilterName(filterBase, entities);
            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assignStartWithAccumulateBy")
            {
                return new SequenceFilterCallLambdaExpressionCompiled(filterBase, entities[0],
                    initArrayAccess, initExpr,
                    arrayAccess, previousAccumulationAccess, index, element, lambdaExpr);
            }
            else
                throw new SequenceParserExceptionFilterError(packagePrefixedRuleName, filterName);
        }

        public override string GetPackagePrefixedMatchClassName(String matchClassName, String matchClassPackage)
        {
            String resolvedMatchClassPackage; // match class not yet resolved (impossible before as only part of filter call), resolve it here
            String packagePrefixedMatchClassName;
            bool contextMatchClassNameExists = actionNames.matchClassesToFilters.ContainsKey(PackagePrefixedName(matchClassName, packageContext));
            ResolvePackage(matchClassName, matchClassPackage, packageContext, contextMatchClassNameExists,
                out resolvedMatchClassPackage, out packagePrefixedMatchClassName);
            if(!actionNames.ContainsMatchClass(packagePrefixedMatchClassName))
                throw new SequenceParserExceptionUnknownMatchClass(packagePrefixedMatchClassName, "\\<class " + packagePrefixedMatchClassName + ">");
            return packagePrefixedMatchClassName;
        }

        public override SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String resolvedMatchClassPackage; // match class not yet resolved (impossible before as only part of filter call), resolve it here
            String packagePrefixedMatchClassName;
            bool contextMatchClassNameExists = actionNames.matchClassesToFilters.ContainsKey(PackagePrefixedName(matchClassName, packageContext));
            ResolvePackage(matchClassName, matchClassPackage, packageContext, contextMatchClassNameExists,
                out resolvedMatchClassPackage, out packagePrefixedMatchClassName);
            if(!actionNames.ContainsMatchClass(packagePrefixedMatchClassName))
                throw new SequenceParserExceptionUnknownMatchClass(packagePrefixedMatchClassName, packagePrefixedMatchClassName + "." + filterBase);

            String filterName = GetFilterName(filterBase, entities);

            String packagePrefixedName;
            if(IsAutoSuppliedFilterName(filterBase))
                packagePrefixedName = filterBase;
            else if(IsAutoGeneratedFilter(filterBase, entities))
                packagePrefixedName = filterName;
            else
            {
                if(packagePrefix != null)
                {
                    if(packagePrefix != "global")
                        packagePrefixedName = packagePrefix + "::" + filterName;
                    else
                        packagePrefixedName = filterName;
                }
                else if(matchClassPackage != null && actionNames.GetFilterOfMatchClass(packagePrefixedMatchClassName, matchClassPackage + "::" + filterName) != null)
                    packagePrefixedName = matchClassPackage + "::" + filterName;
                else if(packageContext != null && actionNames.GetFilterOfMatchClass(packagePrefixedMatchClassName, packageContext + "::" + filterName) != null)
                    packagePrefixedName = packageContext + "::" + filterName;
                else 
                    packagePrefixedName = filterName;
            }

            IFilter filter = actionNames.GetFilterOfMatchClass(packagePrefixedMatchClassName, packagePrefixedName);
            if(filter == null)
                throw new SequenceParserExceptionFilterError(packagePrefixedMatchClassName, packagePrefixedName);

            return new SequenceFilterCallCompiled(matchClassName, resolvedMatchClassPackage, packagePrefixedMatchClassName,
                filter, argExprs.ToArray());
        }

        public override SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String resolvedMatchClassPackage; // match class not yet resolved (impossible before as only part of filter call), resolve it here
            String packagePrefixedMatchClassName;
            bool contextMatchClassNameExists = actionNames.matchClassesToFilters.ContainsKey(PackagePrefixedName(matchClassName, packageContext));
            ResolvePackage(matchClassName, matchClassPackage, packageContext, contextMatchClassNameExists,
                out resolvedMatchClassPackage, out packagePrefixedMatchClassName);
            if(!actionNames.ContainsMatchClass(packagePrefixedMatchClassName))
                throw new SequenceParserExceptionUnknownMatchClass(packagePrefixedMatchClassName, packagePrefixedMatchClassName + "." + filterBase);

            String filterName = GetFilterName(filterBase, entities);

            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assign")
            {
                return new SequenceFilterCallLambdaExpressionCompiled(matchClassName, resolvedMatchClassPackage, packagePrefixedMatchClassName,
                    filterBase, entities[0], arrayAccess, index, element, lambdaExpr);
            }
            else if(entities.Count == 0 && filterBase == "removeIf")
            {
                return new SequenceFilterCallLambdaExpressionCompiled(matchClassName, resolvedMatchClassPackage, packagePrefixedMatchClassName,
                    filterBase, null, arrayAccess, index, element, lambdaExpr);
            }
            else
                throw new SequenceParserExceptionFilterError(packagePrefixedMatchClassName, packagePrefixedName);
        }

        public override SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String resolvedMatchClassPackage; // match class not yet resolved (impossible before as only part of filter call), resolve it here
            String packagePrefixedMatchClassName;
            bool contextMatchClassNameExists = actionNames.matchClassesToFilters.ContainsKey(PackagePrefixedName(matchClassName, packageContext));
            ResolvePackage(matchClassName, matchClassPackage, packageContext, contextMatchClassNameExists,
                out resolvedMatchClassPackage, out packagePrefixedMatchClassName);
            if(!actionNames.ContainsMatchClass(packagePrefixedMatchClassName))
                throw new SequenceParserExceptionUnknownMatchClass(packagePrefixedMatchClassName, packagePrefixedMatchClassName + "." + filterBase);

            String filterName = GetFilterName(filterBase, entities);

            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assignStartWithAccumulateBy")
            {
                return new SequenceFilterCallLambdaExpressionCompiled(matchClassName, resolvedMatchClassPackage, packagePrefixedMatchClassName,
                    filterBase, entities[0],
                    initArrayAccess, initExpr, 
                    arrayAccess, previousAccumulationAccess, index, element, lambdaExpr);
            }
            else
                throw new SequenceParserExceptionFilterError(packagePrefixedMatchClassName, packagePrefixedName);
        }


        public override bool IsProcedureName(String procedureName, String package)
        {
            if(package != null)
            {
                if(package != "global")
                {
                    foreach(String procName in actionNames.procedureNames)
                    {
                        if(procName == package + "::" + procedureName)
                            return true;
                    }
                    return false;
                }
                else
                {
                    foreach(String procName in actionNames.procedureNames)
                    {
                        if(procName == procedureName)
                            return true;
                    }
                    return false;
                }
            }
            else
            {
                foreach(String procName in actionNames.procedureNames)
                {
                    if(packageContext != null && procName == packageContext + "::" + procedureName)
                        return true;
                    if(procName == procedureName)
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

        public override SequenceComputationProcedureCall CreateSequenceComputationProcedureCallUserProcedure(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            String package;
            String packagePrefixedName;
            ResolvePackage(procedureName, packagePrefix, packageContext, actionNames.ContainsProcedure(PackagePrefixedName(procedureName, packageContext)),
                out package, out packagePrefixedName);
            if(!actionNames.ContainsProcedure(packagePrefixedName))
                throw new SequenceParserExceptionCallIssue(procedureName, DefinitionType.Procedure, CallIssueType.UnknownProcedure);

            return new SequenceComputationProcedureCallCompiled(procedureName, package, packagePrefixedName,
                argExprs, returnVars, actionNames.IsExternal(packagePrefixedName));
        }


        public override bool IsFunctionName(String functionName, String package)
        {
            if(package != null)
            {
                if(package != "global")
                {
                    foreach(String funcName in actionNames.functionNames)
                    {
                        if(funcName == package + "::" + functionName)
                            return true;
                    }
                    return false;
                }
                else
                {
                    foreach(String funcName in actionNames.functionNames)
                    {
                        if(funcName == functionName)
                            return true;
                    }
                    return false;
                }
            }
            else
            {
                foreach(String funcName in actionNames.functionNames)
                {
                    if(packageContext != null && funcName == packageContext + "::" + functionName)
                        return true;
                    if(funcName == functionName)
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

        public override SequenceExpressionFunctionCall CreateSequenceExpressionFunctionCallUserFunction(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs)
        {
            String package;
            String packagePrefixedName;
            ResolvePackage(functionName, packagePrefix, packageContext, actionNames.ContainsFunction(PackagePrefixedName(functionName, packageContext)),
                out package, out packagePrefixedName);
            if(!actionNames.ContainsFunction(packagePrefixedName))
                throw new SequenceParserExceptionCallIssue(functionName, DefinitionType.Function, CallIssueType.UnknownFunction);

            String returnType = null;
            for(int i = 0; i < actionNames.functionNames.Length; ++i)
            {
                if(actionNames.functionNames[i] == functionName)
                    returnType = actionNames.functionOutputTypes[i];
            }

            return new SequenceExpressionFunctionCallCompiled(functionName, package, packagePrefixedName,
                returnType, argExprs, actionNames.IsExternal(packagePrefixedName));
        }

        // resolves names that are given without package context but do not reference global names
        // because they are used from a sequence that is contained in a package (only possible for compiled sequences from rule language)
        // (i.e. calls of entities from packages, without package prefix are changed to package calls (may occur for entities from the same package))
        private static void ResolvePackage(String Name, String PrePackage, String PrePackageContext, bool contextNameExists,
            out String Package, out String PackagePrefixedName)
        {
            if(PrePackage != null)
            {
                if(PrePackage != "global")
                {
                    Package = PrePackage;
                    PackagePrefixedName = PrePackage + "::" + Name;
                    return;
                }
                else
                {
                    Package = null;
                    PackagePrefixedName = Name;
                    return;
                }
            }

            if(PrePackageContext != null && contextNameExists)
            {
                Package = PrePackageContext;
                PackagePrefixedName = PrePackageContext + "::" + Name;
                return;
            }

            Package = null;
            PackagePrefixedName = Name;
        }
    }
}
