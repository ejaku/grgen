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
    class SequenceMultiBacktrackRuleGenerator
    {
        readonly SequenceMultiBacktrack seqMulti;
        readonly SequenceRuleCall seqRule;
        readonly SequenceExpressionGenerator seqExprGen;
        readonly SequenceGeneratorHelper seqHelper;

        readonly SequenceExpression[] ArgumentExpressions;
        readonly SequenceVariable[] ReturnVars;
        readonly String specialStr;
        readonly String parameters;
        readonly String matchingPatternClassName;
        readonly String patternName;
        readonly String plainRuleName;
        readonly String ruleName;
        readonly String matchType;
        readonly String matchName;
        readonly String matchesType;
        readonly String matchesName;


        public SequenceMultiBacktrackRuleGenerator(SequenceMultiBacktrack seqMulti, SequenceRuleCall seqRule, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper)
        {
            this.seqMulti = seqMulti;
            this.seqRule = seqRule;
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;

            ArgumentExpressions = seqRule.ArgumentExpressions;
            ReturnVars = seqRule.ReturnVars;
            specialStr = seqRule.Special ? "true" : "false";
            parameters = seqHelper.BuildParameters(seqRule, ArgumentExpressions);
            matchingPatternClassName = TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            plainRuleName = TypesHelper.PackagePrefixedNameDoubleColon(seqRule.Package, seqRule.Name);
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seqRule.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        public void EmitMatching(SourceBuilder source, SequenceGenerator seqGen, String matchListName)
        {
            source.AppendFront(matchesType + " " + matchesName + " = " + ruleName
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            for(int i = 0; i < seqRule.Filters.Count; ++i)
            {
                seqExprGen.EmitFilterCall(source, (SequenceFilterCallCompiled)seqRule.Filters[i], patternName, matchesName, seqRule.PackagePrefixedName, false);
            }

            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFrontFormat("{0}.AddRange({1});\n", matchListName, matchesName);
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitCloning(SourceBuilder source, SequenceGenerator seqGen, String matchListName, String originalToCloneName)
        {
            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFrontFormat("{0} = ({1}){0}.Clone({2});\n", matchesName, matchesType, originalToCloneName);
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitRewriting(SourceBuilder source, SequenceGenerator seqGen, String matchListName, String enumeratorName, 
            bool fireDebugEvents)
        {
            source.AppendFrontFormat("case \"{0}\":\n", plainRuleName);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront(matchType + " " + matchName + " = (" + matchType + ")" + enumeratorName + ".Current;\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            seqHelper.BuildReturnParameters(seqRule, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            // start a transaction
            if(fireDebugEvents)
                source.AppendFront("procEnv.Matched(" + matchesName + ", " + matchName + ", " + specialStr + ");\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront(ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("++procEnv.PerformanceInfo.RewritesPerformed;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
