/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        internal readonly bool fireDebugEvents;

        internal readonly SequenceExpression[] ArgumentExpressions;
        internal readonly String patternName;
        internal readonly String ruleName;
        internal readonly String matchType;
        internal readonly String matchesType;
        internal readonly String matchesName;
        

        public SequenceRuleCallMatcherGenerator(SequenceRuleCall seqRule, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.seqRule = seqRule;
            this.seqExprGen = seqExprGen;
            this.seqHelper = seqHelper;
            this.fireDebugEvents = fireDebugEvents;

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

        public void EmitToMatchListAdding(SourceBuilder source, String matchListName, String matchToConstructIndexName, int constructIndex)
        {
            source.AppendFront("if(" + matchesName + ".Count != 0) {\n");
            source.Indent();
            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IMatch match in {0})\n", matchesName);
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0}.Add(match);\n", matchListName);
            source.AppendFrontFormat("{0}[match] = {1};\n", matchToConstructIndexName, constructIndex);
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        public static void EmitBeginExecutionEventFiring(SourceBuilder source, string patternMatchingConstructVarName, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFrontFormat("procEnv.BeginExecution({0});\n", patternMatchingConstructVarName);
        }

        public static void EmitPreMatchEventFiring(SourceBuilder source, SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

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

        public static void EmitPreMatchEventFiring(SourceBuilder source, string matchesName, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFrontFormat("procEnv.MatchedBeforeFiltering({0});\n", matchesName);
        }

        public static void EmitMatchEventFiring(SourceBuilder source, SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators, bool matchClassFilterExisting, string matchList, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFront("procEnv.MatchedAfterFiltering(");

            if(matchClassFilterExisting)
            {
                source.AppendFormat("GRGEN_LIBGR.MatchListHelper.RemoveUnavailable({0}, ", matchList);
            }

            source.Append("new GRGEN_LIBGR.IMatches[" + ruleMatcherGenerators.Length + "] ");
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
            source.Append("}");

            if(matchClassFilterExisting)
            {
                source.Append(")");
            }

            source.Append(", new bool[" + ruleMatcherGenerators.Length + "] ");
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

        public static void EmitMatchEventFiring(SourceBuilder source, string matchesName, string specialValue, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFrontFormat("procEnv.MatchedAfterFiltering({0}, {1});\n", matchesName, specialValue);
        }

        public static void EmitFinishedEventFiring(SourceBuilder source, SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

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

        public static void EmitFinishedEventFiring(SourceBuilder source, string matchesName, string specialValue, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFrontFormat("procEnv.Finished({0}, {1});\n", matchesName, specialValue);
        }

        public static void EmitEndExecutionEventFiring(SourceBuilder source, String patternMatchingConstructVarName, String resultVarName, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFrontFormat("procEnv.EndExecution({0}, {1});\n", patternMatchingConstructVarName, resultVarName);
        }

        public static void EmitMatchSelectedEventFiring(SourceBuilder source, String matchVarName, String specialStr, String matchesVarName, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFront("procEnv.MatchSelected(" + matchVarName + ", " + specialStr + ", " + matchesVarName + ");\n");
        }

        public static void EmitRewritingSelectedMatchEventFiring(SourceBuilder source, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFront("procEnv.RewritingSelectedMatch();\n");
        }

        //public static void EmitSelectedMatchRewrittenEventFiring(SourceBuilder source) is not needed as the event firing code is only generated by the frontend (part of the rule modify)

        public static void EmitFinishedSelectedMatchEventFiring(SourceBuilder source, bool fireDebugEvents)
        {
            if(!fireDebugEvents)
                return;

            source.AppendFront("procEnv.FinishedSelectedMatch();\n");
        }
    }
}
