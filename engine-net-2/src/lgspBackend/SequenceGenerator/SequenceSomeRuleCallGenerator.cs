/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceSomeRuleCallGenerator
    {
        internal readonly SequenceSomeFromSet seqSome;
        internal readonly SequenceRuleCall seqRule;
        internal readonly SequenceGeneratorHelper helper;

        internal readonly RuleInvocation ruleInvocation;
        internal readonly SequenceExpression[] ArgumentExpressions;
        internal readonly String specialStr;
        internal readonly String parameters;
        internal readonly String matchingPatternClassName;
        internal readonly String patternName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchName;
        internal readonly String matchesType;
        internal readonly String matchesName;


        public SequenceSomeRuleCallGenerator(SequenceSomeFromSet seqSome, SequenceRuleCall seqRule, SequenceGeneratorHelper helper)
        {
            this.seqSome = seqSome; // parent
            this.seqRule = seqRule;
            this.helper = helper;

            ruleInvocation = seqRule.RuleInvocation;
            ArgumentExpressions = seqRule.ArgumentExpressions;
            specialStr = seqRule.Special ? "true" : "false";
            parameters = helper.BuildParameters(ruleInvocation, ArgumentExpressions);
            matchingPatternClassName = TypesHelper.GetPackagePrefixDot(ruleInvocation.Package) + "Rule_" + ruleInvocation.Name;
            patternName = ruleInvocation.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqRule.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        private void Emit(SourceBuilder source, SequenceGenerator seqGen, SequenceComputationGenerator compGen, bool fireDebugEvents)
        {
            source.AppendFront(compGen.SetResultVar(seqSome, "false"));

            // emit code for matching all the contained rules
            for(int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                new SequenceSomeRuleCallGenerator(seqSome, (SequenceRuleCall)seqSome.Sequences[i], helper)
                    .EmitMatching(source, seqGen, compGen);
            }

            // emit code for deciding on the match to rewrite
            String totalMatchToApply = "total_match_to_apply_" + seqSome.Id;
            String curTotalMatch = "cur_total_match_" + seqSome.Id;
            if(seqSome.Random)
            {
                source.AppendFront("int " + totalMatchToApply + " = 0;\n");
                for(int i = 0; i < seqSome.Sequences.Count; ++i)
                {
                    SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                    String matchesName = "matches_" + seqRule.Id;
                    if(seqRule.SequenceType == SequenceType.RuleCall)
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                    else if(seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                        source.AppendFront("if(" + matchesName + ".Count > 0) ++" + totalMatchToApply + ";\n");
                    else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                }
                source.AppendFront(totalMatchToApply + " = GRGEN_LIBGR.Sequence.randomGenerator.Next(" + totalMatchToApply + ");\n");
                source.AppendFront("int " + curTotalMatch + " = 0;\n");
            }

            // code to handle the rewrite next match
            String firstRewrite = "first_rewrite_" + seqSome.Id;
            source.AppendFront("bool " + firstRewrite + " = true;\n");

            // emit code for rewriting all the contained rules which got matched
            for(int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                new SequenceSomeRuleCallGenerator(seqSome, (SequenceRuleCall)seqSome.Sequences[i], helper)
                    .EmitRewriting(source, seqGen, compGen, totalMatchToApply, curTotalMatch, firstRewrite, fireDebugEvents);
            }
        }

        public void EmitMatching(SourceBuilder source, SequenceGenerator seqGen, SequenceComputationGenerator compGen)
        {
            source.AppendFront(matchesType + " " + matchesName + " = " + ruleName
                + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                + parameters + ");\n");
            for(int i = 0; i < seqRule.Filters.Count; ++i)
            {
                seqGen.EmitFilterCall(source, seqRule.Filters[i], patternName, matchesName);
            }

            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqSome, "true"));
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitRewriting(SourceBuilder source, SequenceGenerator seqGen, SequenceComputationGenerator compGen,
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

                rewritingGen.EmitRewritingRuleCall(source,
                    seqGen, compGen, firstRewrite, fireDebugEvents);

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

                rewritingGen.EmitRewritingRuleCountAllCallOrRuleAllCallNonRandom(source,
                    seqGen, compGen, firstRewrite, fireDebugEvents);

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
                {
                    rewritingGen.EmitRewritingRuleAllCallRandomSequenceRandom(source,
                        seqGen, compGen, firstRewrite, fireDebugEvents);
                }
                else
                {
                    rewritingGen.EmitRewritingRuleAllCallRandomSequenceNonRandom(source,
                        seqGen, compGen, firstRewrite, fireDebugEvents);
                }
            }

            if(fireDebugEvents)
                source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
