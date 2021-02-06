/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    class SequenceRuleCallMatcherGenerator
    {
        internal readonly SequenceRuleCall seqRule;
        internal readonly SequenceExpressionGenerator seqExprGen;
        internal readonly SequenceGeneratorHelper seqHelper;

        internal readonly SequenceExpression[] ArgumentExpressions;
        internal readonly String patternName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchesType;
        internal readonly String matchesName;
        

        public SequenceRuleCallMatcherGenerator(SequenceRuleCall seqRule, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper)
        {
            this.seqRule = seqRule;
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;

            ArgumentExpressions = seqRule.ArgumentExpressions;
            String matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Rule_" + seqRule.Name;
            patternName = seqRule.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seqRule.Id;
        }

        public void EmitMatchingAndCloning(SourceBuilder source, String maxMatches)
        {
            String parameters = seqHelper.BuildParameters(seqRule, ArgumentExpressions, source);
            EmitMatching(source, parameters, maxMatches);
            EmitCloning(source);
        }

        public void EmitMatching(SourceBuilder source, String parameters, String maxMatches)
        {
            source.AppendFront(matchesType + " " + matchesName + " = " + ruleName
                + ".Match(procEnv, " + maxMatches + parameters + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
        }

        public void EmitCloning(SourceBuilder source)
        {
            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitFiltering(SourceBuilder source)
        {
            for(int i = 0; i < seqRule.Filters.Count; ++i)
            {
                seqExprGen.EmitFilterCall(source, seqRule.Filters[i], patternName, matchesName, seqRule.PackagePrefixedName, false);
            }
        }

        public void EmitAddRange(SourceBuilder source, String matchListName)
        {
            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFrontFormat("{0}.AddRange({1});\n", matchListName, matchesName);
            source.Unindent();
            source.AppendFront("}\n");
        }

        public static void EmitPreMatchEventFiring(SourceBuilder source, SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators)
        {
            source.AppendFront("procEnv.PreMatched(");
            bool first = true;
            foreach(SequenceRuleCallMatcherGenerator ruleMatcherGenerator in ruleMatcherGenerators)
            {
                if(first)
                    first = false;
                else
                    source.Append(",");
                source.AppendFormat("{0}", ruleMatcherGenerator.matchesName);
            }
            source.Append(");\n");
        }

        public static void EmitPreMatchEventFiring(SourceBuilder source, string matchesName)
        {
            source.AppendFrontFormat("procEnv.PreMatched({0});\n", matchesName);
        }
    }
}
