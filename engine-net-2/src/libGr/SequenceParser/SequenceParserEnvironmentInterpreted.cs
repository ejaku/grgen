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
            String packagePrefixedName = package != null ? package + "::" + ruleOrSequenceName : ruleOrSequenceName;
            return actions.GetSequenceDefinition(packagePrefixedName) != null;
        }

        public override SequenceSequenceCall CreateSequenceSequenceCall(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special)
        {
            String packagePrefixedName = packagePrefix != null ? packagePrefix + "::" + sequenceName : sequenceName;
            ISequenceDefinition sequenceDef = actions.GetSequenceDefinition(packagePrefixedName);
            if(sequenceDef == null)
                throw new SequenceParserException(packagePrefixedName, DefinitionType.Sequence, SequenceParserError.UnknownRuleOrSequence);

            return new SequenceSequenceCallInterpreted(sequenceDef,
                argExprs, returnVars, subgraph,
                special);
        }


        public override SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, bool isRuleForMultiRuleAllCallReturningArrays)
        {
            String packagePrefixedName = packagePrefix != null ? packagePrefix + "::" + ruleName : ruleName;
            IAction action = actions.GetAction(packagePrefixedName);
            if(action == null)
                throw new SequenceParserException(packagePrefixedName, DefinitionType.Action, SequenceParserError.UnknownRuleOrSequence);

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
            String packagePrefixedName = packagePrefix != null ? packagePrefix + "::" + ruleName : ruleName;
            IAction action = actions.GetAction(packagePrefixedName);
            if(action == null)
                throw new SequenceParserException(packagePrefixedName, DefinitionType.Action, SequenceParserError.UnknownRuleOrSequence);

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
            String packagePrefixedName = packagePrefix != null ? packagePrefix + "::" + ruleName : ruleName;
            IAction action = actions.GetAction(packagePrefixedName);
            if(action == null)
                throw new SequenceParserException(packagePrefixedName, DefinitionType.Action, SequenceParserError.UnknownRuleOrSequence);

            return new SequenceRuleCountAllCallInterpreted(action,
                argExprs, returnVars, subgraph,
                special, test);
        }

        public override SequenceFilterCall CreateSequenceFilterCall(String ruleName, String rulePackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String packagePrefixedRuleName = rulePackage != null ? rulePackage + "::" + ruleName : ruleName; // no (further) resolving of rules in interpreted sequences cause there is no package context existing
            IAction action = actions.GetAction(packagePrefixedRuleName); // must be not null due to preceeding checks of rule call resolving result

            String filterName = GetFilterName(filterBase, entities);
            String PackagePrefixedName;
            if(IsAutoSuppliedFilterName(filterBase)) 
                PackagePrefixedName = filterBase;
            else if(IsAutoGeneratedFilter(filterBase, entities))
                PackagePrefixedName = filterName;
            else
            {
                if(packagePrefix != null)
                    PackagePrefixedName = packagePrefix + "::" + filterName;
                else if(action.RulePattern.GetFilter(filterName) != null)
                    PackagePrefixedName = filterName;
                else if(rulePackage != null)
                    PackagePrefixedName = rulePackage + "::" + filterName;
                else
                    PackagePrefixedName = filterBase; // should not occur
            }

            IFilter filter = action.RulePattern.GetFilter(PackagePrefixedName);
            if(filter == null)
                throw new SequenceParserException(action.PackagePrefixedName, PackagePrefixedName, SequenceParserError.FilterError);

            return new SequenceFilterCallInterpreted(/*action, */filter, argExprs.ToArray());
        }

        public override SequenceFilterCall CreateSequenceMatchClassFilterCall(String matchClassName, String matchClassPackage,
            String packagePrefix, String filterBase, List<String> entities, List<SequenceExpression> argExprs)
        {
            String packagePrefixedMatchClassName = matchClassPackage != null ? matchClassPackage + "::" + matchClassName : matchClassName; // no (further) resolving of match classes in interpreted sequences cause there is no package context existing
            MatchClassFilterer matchClass = actions.GetMatchClass(packagePrefixedMatchClassName); // may be null, match class is part of filter call, was not checked before
            if(matchClass == null)
                throw new SequenceParserException(packagePrefixedMatchClassName, packagePrefixedMatchClassName + "." + filterBase, SequenceParserError.MatchClassError);

            String filterName = GetFilterName(filterBase, entities);
            String PackagePrefixedName;
            if(IsAutoSuppliedFilterName(filterBase))
                PackagePrefixedName = filterBase;
            else if(IsAutoGeneratedFilter(filterBase, entities))
                PackagePrefixedName = filterName;
            else
            {
                if(packagePrefix != null)
                    PackagePrefixedName = packagePrefix + "::" + filterName;
                else if(matchClass.info.GetFilter(filterName) != null)
                    PackagePrefixedName = filterName;
                else if(matchClassPackage != null)
                    PackagePrefixedName = matchClassPackage + "::" + filterName;
                else
                    PackagePrefixedName = filterBase; // should not occur
            }

            IFilter filter = matchClass.info.GetFilter(PackagePrefixedName);
            if(filter == null)
                throw new SequenceParserException(packagePrefixedMatchClassName, PackagePrefixedName, SequenceParserError.FilterError);

            return new SequenceFilterCallInterpreted(matchClass, filter, argExprs.ToArray());
        }


        public override bool IsProcedureName(String procedureName, String package)
        {
            String packagePrefixedProcedureName = package != null ? package + "::" + procedureName : procedureName;
            return actions.GetProcedureDefinition(packagePrefixedProcedureName) != null;
        }

        public override string GetProcedureNames()
        {
            return ((BaseActions)actions).ProcedureNames;
        }

        public override SequenceComputationProcedureCall CreateSequenceComputationProcedureCallUserProcedure(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            String packagePrefixedProcedureName = packagePrefix != null ? packagePrefix + "::" + procedureName : procedureName;
            IProcedureDefinition procedureDef = actions.GetProcedureDefinition(packagePrefixedProcedureName);
            if(procedureDef == null)
                throw new SequenceParserException(packagePrefixedProcedureName, DefinitionType.Procedure, SequenceParserError.UnknownProcedure);

            return new SequenceComputationProcedureCallInterpreted(procedureDef, argExprs, returnVars);
        }


        public override bool IsFunctionName(String functionName, String package)
        {
            String packagePrefixedFunctionName = package != null ? package + "::" + functionName : functionName;
            return actions.GetFunctionDefinition(packagePrefixedFunctionName) != null;
        }

        public override string GetFunctionNames()
        {
            return ((BaseActions)actions).FunctionNames;
        }

        public override SequenceExpressionFunctionCall CreateSequenceExpressionFunctionCallUserFunction(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs)
        {
            String packagePrefixedFunctionName = packagePrefix != null ? packagePrefix + "::" + functionName : functionName;
            IFunctionDefinition functionDef = actions.GetFunctionDefinition(packagePrefixedFunctionName);
            if(functionDef == null)
                throw new SequenceParserException(packagePrefixedFunctionName, DefinitionType.Function, SequenceParserError.UnknownFunction);

            return new SequenceExpressionFunctionCallInterpreted(functionDef, argExprs);
        }
    }
}
