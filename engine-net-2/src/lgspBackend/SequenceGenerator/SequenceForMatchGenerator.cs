/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;
using COMP_HELPER = de.unika.ipd.grGen.lgsp.SequenceComputationGeneratorHelper;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceForMatchGenerator
    {
        internal readonly SequenceForMatch seqFor;
        internal readonly SequenceExpressionGenerator seqExprGen;
        internal readonly SequenceGeneratorHelper seqHelper;

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


        public SequenceForMatchGenerator(SequenceForMatch seqFor, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper)
        {
            this.seqFor = seqFor;
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;

            seqRule = seqFor.Rule;
            seqMatcherGen = new SequenceRuleCallMatcherGenerator(seqRule, seqExprGen, seqHelper);

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

        public void Emit(SourceBuilder source, SequenceGenerator seqGen, bool fireDebugEvents)
        {
            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqFor.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\");\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqFor.Symbol));
            source.AppendFrontFormat("procEnv.BeginExecution({0});\n", patternMatchingConstructVarName);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            seqMatcherGen.EmitMatchingAndCloning(source, "procEnv.MaxMatches");
            SequenceRuleCallMatcherGenerator.EmitPreMatchEventFiring(source, matchesName);
            seqMatcherGen.EmitFiltering(source);

            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            seqHelper.BuildReturnParameters(seqRule, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            if(fireDebugEvents)
                SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, matchesName, specialStr);

            // apply the sequence for every match found
            String enumeratorName = "enum_" + seqFor.Id;
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            source.AppendFront(seqHelper.SetVar(seqFor.Var, matchName));

            if(fireDebugEvents)
                source.AppendFront("procEnv.MatchSelected(" + matchName + ", " + specialStr + ", " + matchesName + ");\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.FinishedSelectedMatch();\n");

            seqGen.EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");

            if(fireDebugEvents)
                SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, matchesName, specialStr);

            source.Unindent();
            source.AppendFront("}\n");

            source.AppendFrontFormat("procEnv.EndExecution({0}, null);\n", patternMatchingConstructVarName);
        }
    }
}
