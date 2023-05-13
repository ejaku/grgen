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
    class SequenceMultiRulePrefixedSequenceRewritingGenerator
    {
        internal readonly SequenceMultiRulePrefixedSequence seqMulti;
        internal readonly SequenceRulePrefixedSequence seqRulePrefixedSequence;
        internal readonly SequenceExpressionGenerator seqExprGen;
        internal readonly SequenceGeneratorHelper seqHelper;
        internal readonly bool fireDebugEvents;

        internal readonly SequenceRuleCall seqRule;

        internal readonly String specialStr;
        internal readonly String matchingPatternClassName;
        internal readonly String patternName;
        internal readonly String plainRuleName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchName;
        internal readonly String matchesType;
        internal readonly String matchesName;

        internal readonly String returnParameterDeclarations;
        internal readonly String returnArguments;
        internal readonly String returnAssignments;
        internal readonly String returnParameterDeclarationsAllCall;
        internal readonly String intermediateReturnAssignmentsAllCall;
        internal readonly String returnAssignmentsAllCall;


        public SequenceMultiRulePrefixedSequenceRewritingGenerator(SequenceMultiRulePrefixedSequence seqMulti, SequenceRulePrefixedSequence seqRulePrefixedSequence,
            SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.seqMulti = seqMulti; // parent
            this.seqRulePrefixedSequence = seqRulePrefixedSequence;
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;
            this.fireDebugEvents = fireDebugEvents;

            seqRule = seqRulePrefixedSequence.Rule;

            specialStr = seqRule.Special ? "true" : "false";
            matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            plainRuleName = TypesHelper.PackagePrefixedNameDoubleColon(seqRule.Package, seqRule.Name);
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqRule.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;

            seqHelper.BuildReturnParameters(seqRule, seqRule.ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);
        }

        public void EmitRewriting(SourceBuilder source, SequenceGenerator seqGen, String matchListName, String enumeratorName, int constructIndex)
        {
            source.AppendFrontFormat("case {0}:\n", constructIndex);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(matchType + " " + matchName + " = (" + matchType + ")" + enumeratorName + ".Current;\n");
            SequenceRuleCallMatcherGenerator.EmitMatchSelectedEventFiring(source, matchName, specialStr, matchesName, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitRewritingSelectedMatchEventFiring(source, fireDebugEvents);
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("++procEnv.PerformanceInfo.RewritesPerformed;\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedSelectedMatchEventFiring(source, fireDebugEvents);

            seqGen.EmitSequence(seqRulePrefixedSequence.Sequence, source);

            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        // todo: support events, end of iteration event; currently events firing from expressions is reduced and fireDebugEvents = false
        // (and in general somewhat dubious here: dedicated match null but matches given)
        // maybe todo: pre/post part with mapping specific stuff only outside, would require mapped match input and result value output
        public void EmitRewritingMapping(SourceBuilder source, SequenceGenerator seqGen, String matchListName, String enumeratorName, int constructIndex)
        {
            source.AppendFrontFormat("case {0}:\n", constructIndex);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront("IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap;\n");
            source.AppendFront("GRGEN_LIBGR.IGraph graph = procEnv.Graph.Clone(procEnv.Graph.Name, out oldToNewMap);\n");

            source.AppendFront("procEnv.SwitchToSubgraph(graph);\n");

            source.AppendFront("GRGEN_LIBGR.IMatch mappedMatch = " + enumeratorName + ".Current.Clone(oldToNewMap);\n");

            source.AppendFront(matchType + " " + matchName + " = (" + matchType + ")mappedMatch;\n");
            SequenceRuleCallMatcherGenerator.EmitMatchSelectedEventFiring(source, matchName, specialStr, matchesName, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitRewritingSelectedMatchEventFiring(source, fireDebugEvents);
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");
            source.AppendFront(ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("++procEnv.PerformanceInfo.RewritesPerformed;\n");
            SequenceRuleCallMatcherGenerator.EmitFinishedSelectedMatchEventFiring(source, fireDebugEvents);

            seqGen.EmitSequence(seqRulePrefixedSequence.Sequence, source);

            source.AppendFront("procEnv.ReturnFromSubgraph();\n");
            source.AppendFrontFormat("if({0}) graphs.Add(graph);\n", COMP_HELPER.GetResultVar(seqRulePrefixedSequence.Sequence));

            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
