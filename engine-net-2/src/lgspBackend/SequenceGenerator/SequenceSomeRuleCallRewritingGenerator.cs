/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceSomeRuleCallRewritingGenerator
    {
        readonly SequenceSomeRuleCallGenerator ruleCallGen;

        String matchType
        {
            get { return ruleCallGen.matchType; }
        }
        String matchName
        {
            get { return ruleCallGen.matchName; }
        }
        String matchesType
        {
            get { return ruleCallGen.matchesType; }
        }
        String matchesName
        {
            get { return ruleCallGen.matchesName; }
        }

        String specialStr
        {
            get { return ruleCallGen.specialStr; }
        }

        readonly String totalMatchToApply;
        readonly String curTotalMatch;

        readonly String returnParameterDeclarations;
        readonly String returnArguments;
        readonly String returnAssignments;
        readonly String returnParameterDeclarationsAllCall;
        readonly String intermediateReturnAssignmentsAllCall;
        readonly String returnAssignmentsAllCall;


        public SequenceSomeRuleCallRewritingGenerator(SequenceSomeRuleCallGenerator ruleCallGen, String totalMatchToApply, String curTotalMatch)
        {
            this.ruleCallGen = ruleCallGen;
            this.totalMatchToApply = totalMatchToApply;
            this.curTotalMatch = curTotalMatch;

            ruleCallGen.seqHelper.BuildReturnParameters(ruleCallGen.seqRule, ruleCallGen.seqRule.ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);
        }

        public void EmitRewritingRuleCall(SourceBuilder source, String firstRewrite, bool fireDebugEvents)
        {
            source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + ruleCallGen.specialStr + ");\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finishing(" + matchesName + ", " + ruleCallGen.specialStr + ");\n");
            source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleCallGen.ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            source.AppendFront(firstRewrite + " = false;\n");
        }

        public void EmitRewritingRuleCountAllCallOrRuleAllCallNonRandom(SourceBuilder source, String firstRewrite, bool fireDebugEvents)
        {
            // iterate through matches, use Modify on each, fire the next match event after the first
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarationsAllCall + "\n");
            String enumeratorName = "enum_" + ruleCallGen.seqRule.Id;
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
            source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleCallGen.ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            source.AppendFront(firstRewrite + " = false;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");
            source.Unindent();
            source.AppendFront("}\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignmentsAllCall + "\n");
            if(ruleCallGen.seqRule.SequenceType == SequenceType.RuleCountAllCall)
            {
                SequenceRuleCountAllCall ruleCountAll = (SequenceRuleCountAllCall)ruleCallGen.seqRule;
                source.AppendFront(ruleCallGen.seqHelper.SetVar(ruleCountAll.CountResult, matchesName + ".Count"));
            }
        }

        public void EmitRewritingRuleAllCallRandomSequenceRandom(SourceBuilder source, String firstRewrite, bool fireDebugEvents)
        {
            // for the match selected: rewrite it
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarationsAllCall + "\n");
            String enumeratorName = "enum_" + ruleCallGen.seqRule.Id;
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
            source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleCallGen.ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            source.AppendFront(firstRewrite + " = false;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");
            source.Unindent();
            source.AppendFront("}\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignmentsAllCall + "\n");
            source.AppendFront("++" + curTotalMatch + ";\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitRewritingRuleAllCallRandomSequenceNonRandom(SourceBuilder source, String firstRewrite, bool fireDebugEvents)
        {
            // randomly choose match, rewrite it and remove it from available matches
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarationsAllCall + "\n");
            source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".GetMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
            source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleCallGen.ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            source.AppendFront(firstRewrite + " = false;\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignmentsAllCall + "\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");
        }
    }
}
