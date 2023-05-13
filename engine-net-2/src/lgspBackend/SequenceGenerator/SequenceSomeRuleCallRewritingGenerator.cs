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
    class SequenceSomeRuleCallRewritingGenerator
    {
        internal readonly SequenceSomeFromSet seqSome;
        internal readonly SequenceExpressionGenerator seqExprGen;
        internal readonly SequenceGeneratorHelper seqHelper;
        internal readonly bool fireDebugEvents;

        internal readonly SequenceRuleCall seqRule;

        internal readonly String specialStr;
        internal readonly String matchingPatternClassName;
        internal readonly String patternName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchName;
        internal readonly String matchesType;
        internal readonly String matchesName;


        public SequenceSomeRuleCallRewritingGenerator(SequenceSomeFromSet seqSome, SequenceRuleCall seqRule,
            SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.seqSome = seqSome; // parent
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;
            this.fireDebugEvents = fireDebugEvents;

            this.seqRule = seqRule;

            specialStr = seqRule.Special ? "true" : "false";
            matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqRule.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        public void EmitRewriting(SourceBuilder source,
            String totalMatchToApply, String curTotalMatch)
        {
            if(seqSome.Random)
                source.AppendFront("if(" + matchesName + ".Count != 0 && " + curTotalMatch + " <= " + totalMatchToApply + ") {\n");
            else
                source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();

            SequenceSomeRuleCallRewritingGeneratorHelper ruleRewritingGeneratorHelper = new SequenceSomeRuleCallRewritingGeneratorHelper(
                this, totalMatchToApply, curTotalMatch, fireDebugEvents);

            if(seqRule.SequenceType == SequenceType.RuleCall)
            {
                if(seqSome.Random)
                {
                    source.AppendFront("if(" + curTotalMatch + " == " + totalMatchToApply + ") {\n");
                    source.Indent();
                }

                ruleRewritingGeneratorHelper.EmitRewritingRuleCall(source);

                if(seqSome.Random)
                {
                    source.Unindent();
                    source.AppendFront("}\n");
                    source.AppendFront("++" + curTotalMatch + ";\n");
                }
            }
            else if(seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
            {
                if(seqSome.Random)
                {
                    source.AppendFront("if(" + curTotalMatch + " == " + totalMatchToApply + ") {\n");
                    source.Indent();
                }

                ruleRewritingGeneratorHelper.EmitRewritingRuleCountAllCallOrRuleAllCallNonRandom(source);

                if(seqSome.Random)
                {
                    source.Unindent();
                    source.AppendFront("}\n");
                    source.AppendFront("++" + curTotalMatch + ";\n");
                }
            }
            else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
            {
                if(seqSome.Random)
                    ruleRewritingGeneratorHelper.EmitRewritingRuleAllCallRandomSequenceRandom(source);
                else
                    ruleRewritingGeneratorHelper.EmitRewritingRuleAllCallRandomSequenceNonRandom(source);
            }

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
