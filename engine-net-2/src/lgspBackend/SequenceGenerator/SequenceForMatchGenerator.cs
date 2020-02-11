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
    class SequenceForMatchGenerator
    {
        readonly SequenceForMatch seqFor;
        readonly SequenceGeneratorHelper helper;

        readonly RuleInvocation ruleInvocation;
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


        public SequenceForMatchGenerator(SequenceForMatch seqFor, SequenceGeneratorHelper helper)
        {
            this.seqFor = seqFor;
            this.helper = helper;

            ruleInvocation = seqFor.Rule.RuleInvocation;
            ArgumentExpressions = seqFor.Rule.ArgumentExpressions;
            ReturnVars = seqFor.Rule.ReturnVars;
            specialStr = seqFor.Rule.Special ? "true" : "false";
            parameters = helper.BuildParameters(ruleInvocation, ArgumentExpressions);
            matchingPatternClassName = TypesHelper.GetPackagePrefixDot(ruleInvocation.Package) + "Rule_" + ruleInvocation.Name;
            patternName = ruleInvocation.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqFor.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqFor.Id;
        }

        public void Emit(SourceBuilder source, SequenceGenerator seqGen, SequenceComputationGenerator compGen, bool fireDebugEvents)
        {
            source.AppendFront(compGen.SetResultVar(seqFor, "true"));

            source.AppendFront(matchesType + " " + matchesName + " = " + ruleName
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            for(int i = 0; i < seqFor.Rule.Filters.Count; ++i)
            {
                seqGen.EmitFilterCall(source, seqFor.Rule.Filters[i], patternName, matchesName);
            }

            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            helper.BuildReturnParameters(ruleInvocation, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            // apply the sequence for every match found
            String enumeratorName = "enum_" + seqFor.Id;
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            source.AppendFront(helper.SetVar(seqFor.Var, matchName));

            seqGen.EmitSequence(seqFor.Seq, source);

            source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
