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
    class SequenceForMatchGenerator
    {
        readonly SequenceForMatch seqFor;
        readonly SequenceGeneratorHelper seqHelper;

        readonly SequenceRuleCall seqRule;
        readonly SequenceExpression[] ArgumentExpressions;
        readonly SequenceVariable[] ReturnVars;
        readonly String specialStr;
        readonly String parameters;
        readonly String matchingPatternClassName;
        readonly String patternName;
        readonly String ruleName;
        readonly String matchType;
        readonly String matchName;
        readonly String matchesType;
        readonly String matchesName;


        public SequenceForMatchGenerator(SequenceForMatch seqFor, SequenceGeneratorHelper seqHelper)
        {
            this.seqFor = seqFor;
            this.seqHelper = seqHelper;

            seqRule = seqFor.Rule;
            ArgumentExpressions = seqRule.ArgumentExpressions;
            ReturnVars = seqRule.ReturnVars;
            specialStr = seqRule.Special ? "true" : "false";
            parameters = seqHelper.BuildParameters(seqRule, ArgumentExpressions);
            matchingPatternClassName = TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqFor.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqFor.Id;
        }

        public void Emit(SourceBuilder source, SequenceGenerator seqGen, bool fireDebugEvents)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, "true"));

            source.AppendFront(matchesType + " " + matchesName + " = " + ruleName
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            for(int i = 0; i < seqRule.Filters.Count; ++i)
            {
                seqGen.EmitFilterCall(source, (SequenceFilterCallCompiled)seqRule.Filters[i], patternName, matchesName, seqRule.PackagePrefixedName);
            }

            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            seqHelper.BuildReturnParameters(seqRule, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            // apply the sequence for every match found
            String enumeratorName = "enum_" + seqFor.Id;
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            source.AppendFront(seqHelper.SetVar(seqFor.Var, matchName));

            seqGen.EmitSequence(seqFor.Seq, source);

            source.AppendFront(COMP_HELPER.SetResultVar(seqFor, COMP_HELPER.GetResultVar(seqFor) + " & " + COMP_HELPER.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
