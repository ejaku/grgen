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
    class SequenceMultiSequenceBacktrackRuleRewritingGenerator
    {
        internal readonly SequenceMultiSequenceBacktrack seqMulti;
        internal readonly Sequence seqSeq;
        internal readonly SequenceExpressionGenerator seqExprGen;
        internal readonly SequenceGeneratorHelper seqHelper;
        internal readonly bool fireDebugEvents;

        internal readonly SequenceRuleCall seqRule;

        internal readonly SequenceVariable[] ReturnVars;
        internal readonly String specialStr;
        internal readonly String matchingPatternClassName;
        internal readonly String patternName;
        internal readonly String plainRuleName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchName;
        internal readonly String matchesType;
        internal readonly String matchesName;


        public SequenceMultiSequenceBacktrackRuleRewritingGenerator(SequenceMultiSequenceBacktrack seqMulti, SequenceRuleCall seqRule, Sequence seqSeq,
            SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.seqMulti = seqMulti;
            this.seqSeq = seqSeq;
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;
            this.fireDebugEvents = fireDebugEvents;

            this.seqRule = seqRule;

            ReturnVars = seqRule.ReturnVars;
            specialStr = seqRule.Special ? "true" : "false";
            matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            plainRuleName = TypesHelper.PackagePrefixedNameDoubleColon(seqRule.Package, seqRule.Name);
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqRule.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        public void EmitRewriting(SourceBuilder source, SequenceGenerator seqGen, String matchListName, String enumeratorName, int constructIndex)
        {
            source.AppendFrontFormat("case {0}:\n", constructIndex);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(matchType + " " + matchName + " = (" + matchType + ")" + enumeratorName + ".Current;\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            seqHelper.BuildReturnParameters(seqRule, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            // start a transaction
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
            seqGen.EmitSequence(seqSeq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqMulti, COMP_HELPER.GetResultVar(seqSeq)));

            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
