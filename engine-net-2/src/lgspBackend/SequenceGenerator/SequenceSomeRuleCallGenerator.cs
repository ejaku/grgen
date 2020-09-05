/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;
using COMP_HELPER = de.unika.ipd.grGen.lgsp.SequenceComputationGeneratorHelper;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceSomeRuleCallGenerator
    {
        internal readonly SequenceSomeFromSet seqSome;
        internal readonly SequenceExpressionGenerator seqExprGen;
        internal readonly SequenceGeneratorHelper seqHelper;

        internal readonly SequenceRuleCall seqRule;
        internal readonly SequenceRuleCallMatcherGenerator seqMatcherGen;

        internal readonly String specialStr;
        internal readonly String matchingPatternClassName;
        internal readonly String patternName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchName;
        internal readonly String matchesType;
        internal readonly String matchesName;


        public SequenceSomeRuleCallGenerator(SequenceSomeFromSet seqSome, SequenceRuleCall seqRule, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper)
        {
            this.seqSome = seqSome; // parent
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;

            this.seqRule = seqRule;
            seqMatcherGen = new SequenceRuleCallMatcherGenerator(seqRule, seqExprGen, seqHelper);

            specialStr = seqRule.Special ? "true" : "false";
            matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqRule.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        public void EmitMatching(SourceBuilder source, SequenceGenerator seqGen)
        {
            seqMatcherGen.EmitMatchingAndCloning(source,
                (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches"));
            seqMatcherGen.EmitFiltering(source);

            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqSome, "true"));
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitRewriting(SourceBuilder source, SequenceGenerator seqGen,
            String totalMatchToApply, String curTotalMatch, String firstRewrite, bool fireDebugEvents)
        {
            if(seqSome.Random)
                source.AppendFront("if(" + matchesName + ".Count != 0 && " + curTotalMatch + " <= " + totalMatchToApply + ") {\n");
            else
                source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();

            SequenceSomeRuleCallRewritingGenerator rewritingGen = new SequenceSomeRuleCallRewritingGenerator(this, totalMatchToApply, curTotalMatch);

            if(seqRule.SequenceType == SequenceType.RuleCall)
            {
                if(seqSome.Random)
                {
                    source.AppendFront("if(" + curTotalMatch + " == " + totalMatchToApply + ") {\n");
                    source.Indent();
                }

                rewritingGen.EmitRewritingRuleCall(source, firstRewrite, fireDebugEvents);

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

                rewritingGen.EmitRewritingRuleCountAllCallOrRuleAllCallNonRandom(source, firstRewrite, fireDebugEvents);

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
                    rewritingGen.EmitRewritingRuleAllCallRandomSequenceRandom(source, firstRewrite, fireDebugEvents);
                else
                    rewritingGen.EmitRewritingRuleAllCallRandomSequenceNonRandom(source, firstRewrite, fireDebugEvents);
            }

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
