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
    class SequenceBacktrackGenerator
    {
        internal readonly SequenceBacktrack seq;
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


        public SequenceBacktrackGenerator(SequenceBacktrack seq, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
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
            matchName = "match_" + seqRule.Id;
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
            source.AppendFront(COMP_HELPER.SetResultVar(seqRule, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seq, "true")); // shut up compiler
            source.AppendFront(COMP_HELPER.SetResultVar(seqRule, "true")); // shut up compiler
            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, matchesName, specialStr, fireDebugEvents);

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            seqHelper.BuildReturnParameters(seqRule, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            // apply the rule and the following sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            String enumeratorName = "enum_" + seq.Id;
            String matchesTriedName = "matchesTried_" + seq.Id;
            source.AppendFront("int " + matchesTriedName + " = 0;\n");
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            source.AppendFront("++" + matchesTriedName + ";\n");

            // start a transaction
            String transactionIdName = "transID_" + seq.Id;
            source.AppendFront("int " + transactionIdName + " = procEnv.TransactionManager.Start();\n");
            String oldRewritesPerformedName = "oldRewritesPerformed_" + seq.Id;
            source.AppendFront("int " + oldRewritesPerformedName + " = procEnv.PerformanceInfo.RewritesPerformed;\n");
            SequenceRuleCallMatcherGenerator.EmitMatchSelectedEventFiring(source, matchName, specialStr, matchesName, fireDebugEvents);
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            SequenceRuleCallMatcherGenerator.EmitRewritingSelectedMatchEventFiring(source, fireDebugEvents);

            source.AppendFront(ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("++procEnv.PerformanceInfo.RewritesPerformed;\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedSelectedMatchEventFiring(source, fireDebugEvents);

            // rule applied, now execute the sequence
            seqGen.EmitSequence(seq.Seq, source);

            // if sequence execution failed, roll the changes back and try the next match of the rule
            source.AppendFront("if(!" + COMP_HELPER.GetResultVar(seq.Seq) + ") {\n");
            source.Indent();
            source.AppendFront("procEnv.TransactionManager.Rollback(" + transactionIdName + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed = " + oldRewritesPerformedName + ";\n");

            source.AppendFront("if(" + matchesTriedName + " < " + matchesName + ".Count) {\n"); // further match available -> try it
            source.Indent();
            source.AppendFront("continue;\n");
            source.Unindent();
            source.AppendFront("} else {\n"); // all matches tried, all failed later on -> end in fail
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seq, "false"));
            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution succeeded, commit the changes so far and succeed
            source.AppendFront("procEnv.TransactionManager.Commit(" + transactionIdName + ");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seq, "true"));
            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, matchesName, specialStr, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "null", fireDebugEvents);
        }
    }
}
