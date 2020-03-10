/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    /// An evironment class for the sequence parser, gives it access to the entitites and types that can be referenced in the sequence.
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
            String package;
            String packagePrefixedName;
            ResolvePackage(sequenceName, packagePrefix, packageContext, actionNames.ContainsSequence(sequenceName),
                out package, out packagePrefixedName);

            SequenceSequenceCall seqSequenceCall = new SequenceSequenceCallCompiled(sequenceName, package, packagePrefixedName,
                argExprs, returnVars, subgraph,
                special);

            return seqSequenceCall;
        }


        public override SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays)
        {
            String package;
            String packagePrefixedName;
            ResolvePackage(ruleName, packagePrefix, packageContext, actionNames.ContainsRule(ruleName),
                out package, out packagePrefixedName);

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
            ResolvePackage(ruleName, packagePrefix, packageContext, actionNames.ContainsRule(ruleName),
                out package, out packagePrefixedName);

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
            ResolvePackage(ruleName, packagePrefix, packageContext, actionNames.ContainsRule(ruleName),
                out package, out packagePrefixedName);

            SequenceRuleCountAllCall seqRuleCountAllCall = new SequenceRuleCountAllCallCompiled(ruleName, package, packagePrefixedName,
                argExprs, returnVars, subgraph,
                special, test);

            return seqRuleCountAllCall;
        }

        public override SequenceFilterCall CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String packagePrefixedRuleName = rulePackage != null ? rulePackage + "::" + ruleName : ruleName; // rulePackage already resolved, concatenation sufficient

            String filterName = GetFilterName(filterBase, entities);

            String Package;
            String PackagePrefixedName;
            String ImplementationPackageAutoGenerated = null;
            if(IsAutoSuppliedFilterName(filterBase))
            {
                Package = null;
                PackagePrefixedName = filterBase;
            }
            else if(IsAutoGeneratedFilter(filterBase, entities))
            {
                Package = null; // implementation only for auto-generated filters, does not appear in calls
                PackagePrefixedName = filterBase;
                ImplementationPackageAutoGenerated = rulePackage;
            }
            else
            {
                if(packagePrefix != null)
                {
                    Package = packagePrefix;
                    PackagePrefixedName = Package + "::" + filterBase;
                }
                else if(actionNames.RuleContainsFilter(packagePrefixedRuleName, filterName))
                {
                    Package = null;
                    PackagePrefixedName = filterBase;
                }
                else if(packageContext != null && actionNames.RuleContainsFilter(packagePrefixedRuleName, packageContext + "::" + filterName))
                {
                    Package = packageContext;
                    PackagePrefixedName = Package + "::" + filterBase;
                }
                else if(rulePackage != null)
                {
                    Package = rulePackage;
                    PackagePrefixedName = Package + "::" + filterBase;
                }
                else
                {
                    // should not occur, (to be) handled in SequenceCheckingEnvironment
                    Package = null;
                    PackagePrefixedName = filterBase;
                }
            }

            SequenceFilterCall seqFilterCall = new SequenceFilterCallCompiled(filterBase, entities,
                Package, PackagePrefixedName, ImplementationPackageAutoGenerated,
                IsAutoSuppliedFilterName(filterBase), IsAutoGeneratedFilter(filterBase, entities),
                argExprs.ToArray());

            return seqFilterCall;
        }

        public override SequenceFilterCall CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String resolvedMatchClassPackage; // match class not yet resolved (impossible before as only part of filter call), resolve it here
            String packagePrefixedMatchClassName;
            bool unprefixedMatchClassNameExists = actionNames.matchClassesToFilters.ContainsKey(matchClassName);
            ResolvePackage(matchClassName, matchClassPackage, packageContext, unprefixedMatchClassNameExists,
                out resolvedMatchClassPackage, out packagePrefixedMatchClassName);

            String filterName = GetFilterName(filterBase, entities);

            String Package;
            String PackagePrefixedName;
            String ImplementationPackageAutoGenerated = null;
            if(IsAutoSuppliedFilterName(filterBase))
            {
                Package = null;
                PackagePrefixedName = filterBase;
            }
            else if(IsAutoGeneratedFilter(filterBase, entities))
            {
                Package = null; // implementation only for auto-generated filters, does not appear in calls
                PackagePrefixedName = filterBase;
                ImplementationPackageAutoGenerated = resolvedMatchClassPackage;
            }
            else
            {
                if(packagePrefix != null)
                {
                    Package = packagePrefix;
                    PackagePrefixedName = Package + "::" + filterBase;
                }
                else if(actionNames.MatchClassContainsFilter(packagePrefixedMatchClassName, filterName))
                {
                    Package = null;
                    PackagePrefixedName = filterBase;
                }
                else if(packageContext != null && actionNames.MatchClassContainsFilter(packagePrefixedMatchClassName, packageContext + "::" + filterName))
                {
                    Package = packageContext;
                    PackagePrefixedName = Package + "::" + filterBase;
                }
                else if(matchClassPackage != null)
                {
                    Package = matchClassPackage;
                    PackagePrefixedName = Package + "::" + filterBase;
                }
                else
                {
                    // should not occur, (to be) handled in SequenceCheckingEnvironment
                    Package = null;
                    PackagePrefixedName = filterBase;
                }
            }

            SequenceFilterCall seqFilterCall = new SequenceFilterCallCompiled(filterBase, entities,
                Package, PackagePrefixedName, ImplementationPackageAutoGenerated,
                matchClassName, resolvedMatchClassPackage, packagePrefixedMatchClassName, 
                IsAutoSuppliedFilterName(filterBase), IsAutoGeneratedFilter(filterBase, entities),
                argExprs.ToArray());

            return seqFilterCall;
        }

        // todo: not used, remove
        public override bool IsFilterFunctionName(String filterFunctionName, String package, String ruleName, String actionPackage)
        {
            if(package != null)
            {
                foreach(String funcName in actionNames.filterFunctionNames)
                {
                    if(funcName == package + "::" + filterFunctionName)
                        return true;
                    if(funcName == filterFunctionName)
                        return true;
                }
                return false;
            }
            else
            {
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
            if(package != null)
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
            String package;
            String packagePrefixedName;
            ResolvePackage(procedureName, packagePrefix, packageContext, actionNames.ContainsProcedure(procedureName),
                out package, out packagePrefixedName);

            return new SequenceComputationProcedureCallCompiled(procedureName, package, packagePrefixedName,
                argExprs, returnVars);
        }


        public override bool IsFunctionName(String functionName, String package)
        {
            if(package != null)
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
            String package;
            String packagePrefixedName;
            ResolvePackage(functionName, packagePrefix, packageContext, actionNames.ContainsFunction(functionName),
                out package, out packagePrefixedName);

            String returnType = null;
            for(int i = 0; i < actionNames.functionNames.Length; ++i)
            {
                if(actionNames.functionNames[i] == functionName)
                    returnType = actionNames.functionOutputTypes[i];
            }

            return new SequenceExpressionFunctionCallCompiled(functionName, package, packagePrefixedName,
                returnType,
                argExprs);
        }

        // resolves names that are given without package context but do not reference global names
        // because they are used from a sequence that is contained in a package (only possible for compiled sequences from rule language)
        // (i.e. calls of entities from packages, without package prefix are changed to package calls (may occur for entities from the same package))
        private static void ResolvePackage(String Name, String PrePackage, String PrePackageContext, bool unprefixedNameExists,
            out String Package, out String PackagePrefixedName)
        {
            if(PrePackage != null)
            {
                Package = PrePackage;
                PackagePrefixedName = PrePackage + "::" + Name;
                return;
            }

            if(unprefixedNameExists)
            {
                Package = null;
                PackagePrefixedName = Name;
                return;
            }

            if(PrePackageContext != null)
            {
                Package = PrePackageContext;
                PackagePrefixedName = PrePackageContext + "::" + Name;
                return;
            }

            // should not occur, (to be) handled in SequenceCheckingEnvironment
            Package = null;
            PackagePrefixedName = Name;
        }
    }
}
