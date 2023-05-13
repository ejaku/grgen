/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using de.unika.ipd.grGen.libGr;
using COMP_HELPER = de.unika.ipd.grGen.lgsp.SequenceComputationGeneratorHelper;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceRuleOrRuleAllCallGenerator
    {
        internal readonly SequenceExpressionGenerator seqExprGen;
        internal readonly SequenceGeneratorHelper seqHelper;
        internal readonly bool fireDebugEvents;

        internal readonly SequenceRuleCall seqRule;
        internal readonly SequenceRuleCallMatcherGenerator seqMatcherGen;

        internal readonly SequenceExpression[] ArgumentExpressions;
        internal readonly SequenceVariable[] ReturnVars;
        internal readonly String specialStr;
        internal readonly String matchingPatternClassName;
        internal readonly String patternName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchName;
        internal readonly String matchesType;
        internal readonly String matchesName;


        public SequenceRuleOrRuleAllCallGenerator(SequenceRuleCall seqRule, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;
            this.fireDebugEvents = fireDebugEvents;

            this.seqRule = seqRule;
            seqMatcherGen = new SequenceRuleCallMatcherGenerator(seqRule, seqExprGen, seqHelper, fireDebugEvents);

            ArgumentExpressions = seqRule.ArgumentExpressions;
            ReturnVars = seqRule.ReturnVars;
            specialStr = seqRule.Special ? "true" : "false";
            matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqRule.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        public void Emit(SourceBuilder source, SequenceGenerator seqGen)
        {
            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqRule.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqRule.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seqRule.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            String parameterDeclarations = null;
            String parameters = null;
            if(seqRule.Subgraph != null)
                parameters = seqHelper.BuildParametersInDeclarations(seqRule, ArgumentExpressions, source, out parameterDeclarations);
            else
                parameters = seqHelper.BuildParameters(seqRule, ArgumentExpressions, source);

            if(seqRule.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + seqHelper.GetVar(seqRule.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            seqMatcherGen.EmitMatching(source, parameters,
                (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches"));
            SequenceRuleCallMatcherGenerator.EmitPreMatchEventFiring(source, matchesName, fireDebugEvents);
            seqMatcherGen.EmitFiltering(source);

            if(seqRule is SequenceRuleCountAllCall)
            {
                SequenceRuleCountAllCall seqRuleCountAll = (SequenceRuleCountAllCall)seqRule;
                source.AppendFront(seqHelper.SetVar(seqRuleCountAll.CountResult, matchesName + ".Count"));
            }

            String insufficientMatchesCondition;
            if(seqRule is SequenceRuleAllCall
                && ((SequenceRuleAllCall)seqRule).ChooseRandom
                && ((SequenceRuleAllCall)seqRule).MinSpecified)
            {
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                String minMatchesVarName = "minmatchesvar_" + seqRuleAll.Id;
                source.AppendFrontFormat("int {0} = (int){1};\n", minMatchesVarName, seqHelper.GetVar(seqRuleAll.MinVarChooseRandom));
                insufficientMatchesCondition = matchesName + ".Count < " + minMatchesVarName;
            }
            else
                insufficientMatchesCondition = matchesName + ".Count == 0";

            source.AppendFrontFormat("if({0}) {{\n", insufficientMatchesCondition);
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqRule, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqRule, "true"));

            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, matchesName, specialStr, fireDebugEvents);

            SequenceRuleOrRuleAllCallRewritingGenerator ruleRewritingGenerator = new SequenceRuleOrRuleAllCallRewritingGenerator(this, fireDebugEvents);
            if(seqRule.SequenceType == SequenceType.RuleCall)
                ruleRewritingGenerator.EmitRewritingRuleCall(source);
            else if(seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                ruleRewritingGenerator.EmitRewritingRuleCountAllCallOrRuleAllCallNonRandom(source);
            else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                ruleRewritingGenerator.EmitRewritingRuleAllCallRandom(source);

            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, matchesName, specialStr, fireDebugEvents);

            source.Unindent();
            source.AppendFront("}\n");

            if(seqRule.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "null", fireDebugEvents);
        }
    }
}
