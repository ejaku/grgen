/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;
using COMP_HELPER = de.unika.ipd.grGen.lgsp.SequenceComputationGeneratorHelper;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceRulePrefixedSequenceGenerator
    {
        readonly SequenceRulePrefixedSequence seq;
        readonly SequenceExpressionGenerator seqExprGen;
        readonly SequenceGeneratorHelper seqHelper;
        readonly bool fireDebugEvents;

        readonly SequenceRuleCall seqRule;
        internal readonly SequenceRuleCallMatcherGenerator seqMatcherGen;

        readonly SequenceExpression[] ArgumentExpressions;
        readonly SequenceVariable[] ReturnVars;
        readonly String specialStr;
        readonly String matchingPatternClassName;
        readonly String patternName;
        readonly String ruleName;
        readonly String matchType;
        readonly String matchName;
        readonly String matchesType;
        readonly String matchesName;


        public SequenceRulePrefixedSequenceGenerator(SequenceRulePrefixedSequence seq, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.seq = seq;
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;
            this.fireDebugEvents = fireDebugEvents;

            seqRule = seq.Rule;
            seqMatcherGen = new SequenceRuleCallMatcherGenerator(seqRule, seqExprGen, seqHelper, fireDebugEvents);

            ArgumentExpressions = seqRule.ArgumentExpressions;
            ReturnVars = seqRule.ReturnVars;
            specialStr = seqRule.Special ? "true" : "false";
            matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seq.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        public void Emit(SourceBuilder source, SequenceGenerator seqGen)
        {
            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seq.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seq.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seq.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            String parameters = seqHelper.BuildParameters(seqRule, ArgumentExpressions, source);

            seqMatcherGen.EmitMatching(source, parameters, "procEnv.MaxMatches");
            SequenceRuleCallMatcherGenerator.EmitPreMatchEventFiring(source, matchesName, fireDebugEvents);
            seqMatcherGen.EmitFiltering(source);
            seqMatcherGen.EmitCloning(source);

            source.AppendFront("if(" + matchesName + ".Count == 0) {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seq, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seq, "false"));

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            seqHelper.BuildReturnParameters(seqRule, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, matchesName, specialStr, fireDebugEvents);

            // apply the rule and the following sequence for every match found
            String enumeratorName = "enum_" + seq.Id;
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");

            SequenceRuleCallMatcherGenerator.EmitMatchSelectedEventFiring(source, matchName, specialStr, matchesName, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitRewritingSelectedMatchEventFiring(source, fireDebugEvents);
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront(ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("++procEnv.PerformanceInfo.RewritesPerformed;\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedSelectedMatchEventFiring(source, fireDebugEvents);

            // rule applied, now execute the sequence
            seqGen.EmitSequence(seq.Sequence, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seq, COMP_HELPER.GetResultVar(seq) + "|" + COMP_HELPER.GetResultVar(seq.Sequence)));

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, matchesName, specialStr, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "null", fireDebugEvents);
        }
    }
}
