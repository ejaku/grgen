/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr.sequenceParser
{
    /// <summary>
    /// An environment class for the sequence parser, 
    /// gives it access to the entitites (and types) that can be referenced in the sequence, and works as a factory for call objects.
    /// Concrete subclass for interpreted sequences.
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
            String packagePrefixedName =  !PackageIsNullOrGlobal(package) ? package + "::" + ruleOrSequenceName : ruleOrSequenceName;
            return actions.GetSequenceDefinition(packagePrefixedName) != null;
        }

        public override SequenceSequenceCall CreateSequenceSequenceCall(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
        {
            String packagePrefixedName = !PackageIsNullOrGlobal(packagePrefix) ? packagePrefix + "::" + sequenceName : sequenceName;
            ISequenceDefinition sequenceDef = actions.GetSequenceDefinition(packagePrefixedName);
            if(sequenceDef == null)
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Sequence, CallIssueType.UnknownRuleOrSequence);

            return new SequenceSequenceCallInterpreted(sequenceDef,
                argExprs, returnVars, subgraph,
                special);
        }


        public override bool IsRuleName(String ruleOrSequenceName, String package)
        {
            String packagePrefixedName = !PackageIsNullOrGlobal(package) ? package + "::" + ruleOrSequenceName : ruleOrSequenceName;
            return actions.GetAction(packagePrefixedName) != null;
        }

        public override SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays)
        {
            String packagePrefixedName = !PackageIsNullOrGlobal(packagePrefix) ? packagePrefix + "::" + ruleName : ruleName;
            IAction action = actions.GetAction(packagePrefixedName);
            if(action == null)
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Action, CallIssueType.UnknownRuleOrSequence);

            return new SequenceRuleCallInterpreted(action,
                argExprs, returnVars, subgraph,
                special, test, isRuleForMultiRuleAllCallReturningArrays);
        }

        public override SequenceRuleAllCall CreateSequenceRuleAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice)
        {
            String packagePrefixedName = !PackageIsNullOrGlobal(packagePrefix) ? packagePrefix + "::" + ruleName : ruleName;
            IAction action = actions.GetAction(packagePrefixedName);
            if(action == null)
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Action, CallIssueType.UnknownRuleOrSequence);

            return new SequenceRuleAllCallInterpreted(action,
                argExprs, returnVars, subgraph,
                special, test,
                chooseRandom, varChooseRandom,
                chooseRandom2, varChooseRandom2, choice);
        }

        public override SequenceRuleCountAllCall CreateSequenceRuleCountAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test)
        {
            String packagePrefixedName = !PackageIsNullOrGlobal(packagePrefix) ? packagePrefix + "::" + ruleName : ruleName;
            IAction action = actions.GetAction(packagePrefixedName);
            if(action == null)
                throw new SequenceParserExceptionCallIssue(packagePrefixedName, DefinitionType.Action, CallIssueType.UnknownRuleOrSequence);

            return new SequenceRuleCountAllCallInterpreted(action,
                argExprs, returnVars, subgraph,
                special, test);
        }

        public override SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String packagePrefixedRuleName = !PackageIsNullOrGlobal(rulePackage) ? rulePackage + "::" + ruleName : ruleName; // no (further) resolving of rules in interpreted sequences cause there is no package context existing
            IAction action = actions.GetAction(packagePrefixedRuleName); // must be not null due to preceeding checks of rule call resolving result

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
                else if(rulePackage != null && action.RulePattern.GetFilter(rulePackage + "::" + filterName) != null)
                    packagePrefixedName = rulePackage + "::" + filterName;
                else
                    packagePrefixedName = filterName;
            }

            IFilter filter = action.RulePattern.GetFilter(packagePrefixedName);
            if(filter == null)
                throw new SequenceParserExceptionFilterError(action.PackagePrefixedName, packagePrefixedName);

            return new SequenceFilterCallInterpreted(/*action, */filter, argExprs.ToArray());
        }

        public override SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String packagePrefixedRuleName = !PackageIsNullOrGlobal(rulePackage) ? rulePackage + "::" + ruleName : ruleName; // no (further) resolving of rules in interpreted sequences cause there is no package context existing
            IAction action = actions.GetAction(packagePrefixedRuleName); // must be not null due to preceeding checks of rule call resolving result

            String filterName = GetFilterName(filterBase, entities);
            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assign")
                return new SequenceFilterCallLambdaExpressionInterpreted(/*action, */filterBase, entities[0], arrayAccess, index, element, lambdaExpr);
            else if(entities.Count == 0 && filterBase == "removeIf")
                return new SequenceFilterCallLambdaExpressionInterpreted(/*action, */filterBase, null, arrayAccess, index, element, lambdaExpr);
            else
                throw new SequenceParserExceptionFilterError(action.PackagePrefixedName, packagePrefixedName);
        }

        public override SequenceFilterCallBase CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String packagePrefixedRuleName = !PackageIsNullOrGlobal(rulePackage) ? rulePackage + "::" + ruleName : ruleName; // no (further) resolving of rules in interpreted sequences cause there is no package context existing
            IAction action = actions.GetAction(packagePrefixedRuleName); // must be not null due to preceeding checks of rule call resolving result

            String filterName = GetFilterName(filterBase, entities);
            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assignStartWithAccumulateBy")
            {
                return new SequenceFilterCallLambdaExpressionInterpreted(/*action, */filterBase, entities[0],
                    initArrayAccess, initExpr,
                    arrayAccess, previousAccumulationAccess, index, element, lambdaExpr);
            }
            else
                throw new SequenceParserExceptionFilterError(action.PackagePrefixedName, packagePrefixedName);
        }

        public override string GetPackagePrefixedMatchClassName(String matchClassName, String matchClassPackage)
        {
            String packagePrefixedMatchClassName = !PackageIsNullOrGlobal(matchClassPackage) ? matchClassPackage + "::" + matchClassName : matchClassName; // no (further) resolving of match classes in interpreted sequences cause there is no package context existing
            MatchClassFilterer matchClass = actions.GetMatchClass(packagePrefixedMatchClassName); // may be null, match class is part of filter call, was not checked before
            if(matchClass == null)
                throw new SequenceParserExceptionUnknownMatchClass(packagePrefixedMatchClassName, "\\<class " + packagePrefixedMatchClassName + ">");
            return matchClass.info.PackagePrefixedName;
        }

        public override SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String packagePrefixedMatchClassName = matchClassPackage != null ? matchClassPackage + "::" + matchClassName : matchClassName; // no (further) resolving of match classes in interpreted sequences cause there is no package context existing
            MatchClassFilterer matchClass = actions.GetMatchClass(packagePrefixedMatchClassName); // may be null, match class is part of filter call, was not checked before
            if(matchClass == null)
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
                else if(matchClassPackage != null && matchClass.info.GetFilter(matchClassPackage + "::" + filterName) != null)
                    packagePrefixedName = matchClassPackage + "::" + filterName;
                else
                    packagePrefixedName = filterName;
            }

            IFilter filter = matchClass.info.GetFilter(packagePrefixedName);
            if(filter == null)
                throw new SequenceParserExceptionFilterError(packagePrefixedMatchClassName, packagePrefixedName);

            return new SequenceFilterCallInterpreted(matchClass, filter, argExprs.ToArray());
        }

        public override SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable arrayAccess, SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String packagePrefixedMatchClassName = matchClassPackage != null ? matchClassPackage + "::" + matchClassName : matchClassName; // no (further) resolving of match classes in interpreted sequences cause there is no package context existing
            MatchClassFilterer matchClass = actions.GetMatchClass(packagePrefixedMatchClassName); // may be null, match class is part of filter call, was not checked before
            if(matchClass == null)
                throw new SequenceParserExceptionUnknownMatchClass(packagePrefixedMatchClassName, packagePrefixedMatchClassName + "." + filterBase);

            String filterName = GetFilterName(filterBase, entities);
            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assign")
                return new SequenceFilterCallLambdaExpressionInterpreted(matchClass, filterBase, entities[0], arrayAccess, index, element, lambdaExpr);
            else if(entities.Count == 0 && filterBase == "removeIf")
                return new SequenceFilterCallLambdaExpressionInterpreted(matchClass, filterBase, null, arrayAccess, index, element, lambdaExpr);
            else
                throw new SequenceParserExceptionFilterError(packagePrefixedMatchClassName, packagePrefixedName);
        }

        public override SequenceFilterCallBase CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities,
            SequenceVariable initArrayAccess, SequenceExpression initExpr,
            SequenceVariable arrayAccess, SequenceVariable previousAccumulationAccess,
            SequenceVariable index, SequenceVariable element, SequenceExpression lambdaExpr)
        {
            String packagePrefixedMatchClassName = matchClassPackage != null ? matchClassPackage + "::" + matchClassName : matchClassName; // no (further) resolving of match classes in interpreted sequences cause there is no package context existing
            MatchClassFilterer matchClass = actions.GetMatchClass(packagePrefixedMatchClassName); // may be null, match class is part of filter call, was not checked before
            if(matchClass == null)
                throw new SequenceParserExceptionUnknownMatchClass(packagePrefixedMatchClassName, packagePrefixedMatchClassName + "." + filterBase);

            String filterName = GetFilterName(filterBase, entities);
            String packagePrefixedName = filterName;

            if(entities.Count == 1 && filterBase == "assignStartWithAccumulateBy")
            {
                return new SequenceFilterCallLambdaExpressionInterpreted(matchClass, filterBase, entities[0],
                    initArrayAccess, initExpr,
                    arrayAccess, previousAccumulationAccess, index, element, lambdaExpr);
            }
            else
                throw new SequenceParserExceptionFilterError(packagePrefixedMatchClassName, packagePrefixedName);
        }


        public override bool IsProcedureName(String procedureName, String package)
        {
            String packagePrefixedProcedureName = !PackageIsNullOrGlobal(package) ? package + "::" + procedureName : procedureName;
            return actions.GetProcedureDefinition(packagePrefixedProcedureName) != null;
        }

        public override string GetProcedureNames()
        {
            return ((BaseActions)actions).ProcedureNames;
        }

        public override SequenceComputationProcedureCall CreateSequenceComputationProcedureCallUserProcedure(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            String packagePrefixedProcedureName = !PackageIsNullOrGlobal(packagePrefix) ? packagePrefix + "::" + procedureName : procedureName;
            IProcedureDefinition procedureDef = actions.GetProcedureDefinition(packagePrefixedProcedureName);
            if(procedureDef == null)
                throw new SequenceParserExceptionCallIssue(packagePrefixedProcedureName, DefinitionType.Procedure, CallIssueType.UnknownProcedure);

            return new SequenceComputationProcedureCallInterpreted(procedureDef, argExprs, returnVars);
        }


        public override bool IsFunctionName(String functionName, String package)
        {
            String packagePrefixedFunctionName = !PackageIsNullOrGlobal(package) ? package + "::" + functionName : functionName;
            return actions.GetFunctionDefinition(packagePrefixedFunctionName) != null;
        }

        public override string GetFunctionNames()
        {
            return ((BaseActions)actions).FunctionNames;
        }

        public override SequenceExpressionFunctionCall CreateSequenceExpressionFunctionCallUserFunction(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs)
        {
            String packagePrefixedFunctionName = !PackageIsNullOrGlobal(packagePrefix) ? packagePrefix + "::" + functionName : functionName;
            IFunctionDefinition functionDef = actions.GetFunctionDefinition(packagePrefixedFunctionName);
            if(functionDef == null)
                throw new SequenceParserExceptionCallIssue(packagePrefixedFunctionName, DefinitionType.Function, CallIssueType.UnknownFunction);

            return new SequenceExpressionFunctionCallInterpreted(functionDef, argExprs);
        }
    }
}
