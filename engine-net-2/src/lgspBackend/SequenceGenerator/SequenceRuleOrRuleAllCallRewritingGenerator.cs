/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceRuleOrRuleAllCallRewritingGenerator
    {
        readonly SequenceRuleOrRuleAllCallGenerator ruleCallGen;
        readonly bool fireDebugEvents;

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

        readonly String returnParameterDeclarations;
        readonly String returnArguments;
        readonly String returnAssignments;
        readonly String returnParameterDeclarationsAllCall;
        readonly String intermediateReturnAssignmentsAllCall;
        readonly String returnAssignmentsAllCall;

        public SequenceRuleOrRuleAllCallRewritingGenerator(SequenceRuleOrRuleAllCallGenerator ruleCallGen, bool fireDebugEvents)
        {
            this.ruleCallGen = ruleCallGen;
            this.fireDebugEvents = fireDebugEvents;

            ruleCallGen.seqHelper.BuildReturnParameters(ruleCallGen.seqRule, ruleCallGen.ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);
        }

        public void EmitRewritingRuleCall(SourceBuilder source)
        {
            source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            SequenceRuleCallMatcherGenerator.EmitMatchSelectedEventFiring(source, matchName, ruleCallGen.specialStr, matchesName, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitRewritingSelectedMatchEventFiring(source, fireDebugEvents);
            source.AppendFront(ruleCallGen.ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedSelectedMatchEventFiring(source, fireDebugEvents);
        }

        public void EmitRewritingRuleCountAllCallOrRuleAllCallNonRandom(SourceBuilder source)
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
            SequenceRuleCallMatcherGenerator.EmitMatchSelectedEventFiring(source, matchName, ruleCallGen.specialStr, matchesName, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitRewritingSelectedMatchEventFiring(source, fireDebugEvents);
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleCallGen.ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedSelectedMatchEventFiring(source, fireDebugEvents);
            source.Unindent();
            source.AppendFront("}\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignmentsAllCall + "\n");
        }

        public void EmitRewritingRuleAllCallRandom(SourceBuilder source)
        {
            // as long as a further rewrite has to be selected: randomly choose next match, rewrite it and remove it from available matches; fire the next match event after the first
            SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)ruleCallGen.seqRule;
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarationsAllCall + "\n");
            String matchesToChoose = "numchooserandomvar_" + seqRuleAll.Id; // variable storing number of matches to choose randomly
            source.AppendFrontFormat("int {0} = (int){1};\n", matchesToChoose, 
                seqRuleAll.MaxVarChooseRandom != null ? ruleCallGen.seqHelper.GetVar(seqRuleAll.MaxVarChooseRandom) : (seqRuleAll.MinSpecified ? "2147483647" : "1"));
            source.AppendFrontFormat("if({0}.Count < {1}) {1} = {0}.Count;\n", matchesName, matchesToChoose);
            source.AppendFrontFormat("for(int i = 0; i < {0}; ++i)\n", matchesToChoose);
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
            SequenceRuleCallMatcherGenerator.EmitMatchSelectedEventFiring(source, matchName, ruleCallGen.specialStr, matchesName, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitRewritingSelectedMatchEventFiring(source, fireDebugEvents);
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleCallGen.ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedSelectedMatchEventFiring(source, fireDebugEvents);
            source.Unindent();
            source.AppendFront("}\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignmentsAllCall + "\n");
        }
    }
}
