/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
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
    class SequenceMultiSequenceBacktrackGenerator
    {
        readonly SequenceMultiSequenceBacktrack seqMulti;
        readonly SequenceGeneratorHelper seqHelper;


        public SequenceMultiSequenceBacktrackGenerator(SequenceMultiSequenceBacktrack seqMulti, SequenceGeneratorHelper seqHelper)
        {
            this.seqMulti = seqMulti;
            this.seqHelper = seqHelper;
        }

        public void Emit(SourceBuilder source, SequenceGenerator seqGen, bool fireDebugEvents)
        {
            String matchListName = "MatchList_" + seqMulti.Id;
            source.AppendFrontFormat("List<GRGEN_LIBGR.IMatch> {0} = new List<GRGEN_LIBGR.IMatch>();\n", matchListName);

            // emit code for matching all the contained rules
            for(int i = 0; i < seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
            {
                new SequenceMultiSequenceBacktrackRuleGenerator(seqMulti, 
                    seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule,
                    seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences[i].Sequence,
                    seqHelper)
                    .EmitMatching(source, seqGen, matchListName);
            }

            // emit code for match class (non-rule-based) filtering
            foreach(SequenceFilterCall sequenceFilterCall in seqMulti.MultiRulePrefixedSequence.Filters)
            {
                seqGen.EmitMatchClassFilterCall(source, (SequenceFilterCallCompiled)sequenceFilterCall, matchListName);
            }

            source.AppendFront("if(" + matchListName + ".Count == 0) {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqMulti, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(COMP_HELPER.SetResultVar(seqMulti, "true")); // shut up compiler

            String originalToCloneName = "originalToClone_" + seqMulti.Id;
            source.AppendFrontFormat("Dictionary<GRGEN_LIBGR.IMatch, GRGEN_LIBGR.IMatch> {0} = new Dictionary<GRGEN_LIBGR.IMatch, GRGEN_LIBGR.IMatch>();\n", originalToCloneName);

            // emit code for cloning the matches objects of the rules
            for(int i = 0; i < seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
            {
                new SequenceMultiSequenceBacktrackRuleGenerator(seqMulti,
                    seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule,
                    seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences[i].Sequence,
                    seqHelper)
                    .EmitCloning(source, seqGen, matchListName, originalToCloneName);
            }

            String originalMatchList = "originalMatchList_" + seqMulti.Id;
            source.AppendFrontFormat("List<GRGEN_LIBGR.IMatch> {0} = new List<GRGEN_LIBGR.IMatch>({1});\n", originalMatchList, matchListName);
            source.AppendFrontFormat("{0}.Clear();\n", matchListName);

            String originalMatch = "originalMatch_" + seqMulti.Id;
            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IMatch {0} in {1})\n", originalMatch, originalMatchList);
            source.AppendFrontFormat("\t{0}.Add({1}[{2}]);", matchListName, originalToCloneName, originalMatch);

            // apply the rules and the following sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            String enumeratorName = "enum_" + seqMulti.Id;
            String matchesTriedName = "matchesTried_" + seqMulti.Id;
            source.AppendFront("int " + matchesTriedName + " = 0;\n");
            source.AppendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + enumeratorName + " = " + matchListName + ".GetEnumerator();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("++" + matchesTriedName + ";\n");

            String transactionIdName = "transID_" + seqMulti.Id;
            source.AppendFront("int " + transactionIdName + " = procEnv.TransactionManager.Start();\n");
            String oldRewritesPerformedName = "oldRewritesPerformed_" + seqMulti.Id;
            source.AppendFront("int " + oldRewritesPerformedName + " = procEnv.PerformanceInfo.RewritesPerformed;\n");

            source.AppendFront("switch(" + enumeratorName + ".Current.Pattern.PackagePrefixedName)\n");
            source.AppendFront("{\n");
            source.Indent();

            // emit code for rewriting the current match (for each rule, rule fitting to the match is selected by rule name)
            // and code for executing the corresponding sequence
            for(int i = 0; i < seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences.Count; ++i)
            {
                new SequenceMultiSequenceBacktrackRuleGenerator(seqMulti,
                    seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences[i].Rule,
                    seqMulti.MultiRulePrefixedSequence.RulePrefixedSequences[i].Sequence,
                    seqHelper)
                    .EmitRewriting(source, seqGen, matchListName, enumeratorName, fireDebugEvents);
            }

            source.AppendFrontFormat("default: throw new Exception(\"Unknown pattern \" + {0}.Current.Pattern.PackagePrefixedName + \" in match!\");", enumeratorName);
            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution failed, roll the changes back and try the next match
            source.AppendFront("if(!" + COMP_HELPER.GetResultVar(seqMulti) + ") {\n");
            source.Indent();
            source.AppendFront("procEnv.TransactionManager.Rollback(" + transactionIdName + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed = " + oldRewritesPerformedName + ";\n");

            source.AppendFront("if(" + matchesTriedName + " < " + matchListName + ".Count) {\n"); // further match available -> try it
            source.Indent();
            source.AppendFront("continue;\n");
            source.Unindent();
            source.AppendFront("} else {\n"); // all matches tried, all failed later on -> end in fail
            source.Indent();
            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution succeeded, commit the changes so far and succeed
            source.AppendFront("procEnv.TransactionManager.Commit(" + transactionIdName + ");\n");
            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
