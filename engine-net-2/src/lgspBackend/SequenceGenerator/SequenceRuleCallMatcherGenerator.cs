/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
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
            source.AppendFront("procEnv.MatchedBeforeFiltering(new GRGEN_LIBGR.IMatches[" + ruleMatcherGenerators.Length + "] {");
            bool first = true;
            foreach(SequenceRuleCallMatcherGenerator ruleMatcherGenerator in ruleMatcherGenerators)
            {
                if(first)
                    first = false;
                else
                    source.Append(",");
                source.AppendFormat("{0}", ruleMatcherGenerator.matchesName);
            }
            source.Append("});\n");
        }

        public static void EmitPreMatchEventFiring(SourceBuilder source, string matchesName)
        {
            source.AppendFrontFormat("procEnv.MatchedBeforeFiltering({0});\n", matchesName);
        }

        public static void EmitMatchEventFiring(SourceBuilder source, SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators, bool matchClassFilterExisting, int constructId, string matchList)
        {
            string matchesArray = "matchesArray_" + constructId;
            source.AppendFrontFormat("GRGEN_LIBGR.IMatches[] {0} = new GRGEN_LIBGR.IMatches[" + ruleMatcherGenerators.Length + "] ", matchesArray);
            source.Append("{");
            bool first = true;
            foreach(SequenceRuleCallMatcherGenerator ruleMatcherGenerator in ruleMatcherGenerators)
            {
                if(first)
                    first = false;
                else
                    source.Append(",");
                source.AppendFormat("{0}", ruleMatcherGenerator.matchesName);
            }
            source.Append("};\n");

            if(matchClassFilterExisting)
            {
                source.AppendFrontFormat("GRGEN_LIBGR.MatchListHelper.RemoveUnavailable({0}, {1});\n", matchList, matchesArray);
            }

            source.AppendFrontFormat("procEnv.MatchedAfterFiltering({0}, ", matchesArray);
            source.Append("new bool[" + ruleMatcherGenerators.Length + "] ");
            source.Append("{");
            first = true;
            foreach(SequenceRuleCallMatcherGenerator ruleMatcherGenerator in ruleMatcherGenerators)
            {
                if(first)
                    first = false;
                else
                    source.Append(",");
                source.AppendFormat("{0}", ruleMatcherGenerator.seqRule.Special ? "true" : "false");
            }
            source.Append("});\n");
        }

        public static void EmitMatchEventFiring(SourceBuilder source, string matchesName, string specialValue)
        {
            source.AppendFrontFormat("procEnv.MatchedAfterFiltering({0}, {1});\n", matchesName, specialValue);
        }

        public static void EmitFinishedEventFiring(SourceBuilder source, SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators)
        {
            source.AppendFront("procEnv.Finished(new GRGEN_LIBGR.IMatches[" + ruleMatcherGenerators.Length + "] ");
            source.Append("{");
            bool first = true;
            foreach(SequenceRuleCallMatcherGenerator ruleMatcherGenerator in ruleMatcherGenerators)
            {
                if(first)
                    first = false;
                else
                    source.Append(",");
                source.AppendFormat("{0}", ruleMatcherGenerator.matchesName);
            }
            source.Append("}, ");
            source.Append("new bool[" + ruleMatcherGenerators.Length + "] ");
            source.Append("{");
            first = true;
            foreach(SequenceRuleCallMatcherGenerator ruleMatcherGenerator in ruleMatcherGenerators)
            {
                if(first)
                    first = false;
                else
                    source.Append(",");
                source.AppendFormat("{0}", ruleMatcherGenerator.seqRule.Special ? "true" : "false");
            }
            source.Append("});\n");
        }

        public static void EmitFinishedEventFiring(SourceBuilder source, string matchesName, string specialValue)
        {
            source.AppendFrontFormat("procEnv.Finished({0}, {1});\n", matchesName, specialValue);
        }
    }
}
