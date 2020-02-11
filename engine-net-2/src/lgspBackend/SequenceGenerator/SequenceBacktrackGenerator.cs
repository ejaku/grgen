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
    class SequenceBacktrackGenerator
    {
        readonly SequenceBacktrack seq;
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


        public SequenceBacktrackGenerator(SequenceBacktrack seq, SequenceGeneratorHelper helper)
        {
            this.seq = seq;
            this.helper = helper;

            ruleInvocation = seq.Rule.RuleInvocation;
            ArgumentExpressions = seq.Rule.ArgumentExpressions;
            ReturnVars = seq.Rule.ReturnVars;
            specialStr = seq.Rule.Special ? "true" : "false";
            parameters = helper.BuildParameters(ruleInvocation, ArgumentExpressions);
            matchingPatternClassName = TypesHelper.GetPackagePrefixDot(ruleInvocation.Package) + "Rule_" + ruleInvocation.Name;
            patternName = ruleInvocation.Name;
            ruleName = "rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name);
            matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            matchName = "match_" + seq.Id;
            matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            matchesName = "matches_" + seq.Id;
        }

        public void Emit(SourceBuilder source, SequenceGenerator seqGen, SequenceComputationGenerator compGen, bool fireDebugEvents)
        {
            source.AppendFront(matchesType + " " + matchesName + " = " + ruleName
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            for(int i = 0; i < seq.Rule.Filters.Count; ++i)
            {
                seqGen.EmitFilterCall(source, seq.Rule.Filters[i], patternName, matchesName);
            }

            source.AppendFront("if(" + matchesName + ".Count == 0) {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "true")); // shut up compiler
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

            // apply the rule and the following sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            String enumeratorName = "enum_" + seq.Id;
            String matchesTriedName = "matchesTried_" + seq.Id;
            source.AppendFront("int " + matchesTriedName + " = 0;\n");
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            source.AppendFront("++" + matchesTriedName + ";\n");

            // start a transaction
            String transactionIdName = "transID_" + seq.Id;
            source.AppendFront("int " + transactionIdName + " = procEnv.TransactionManager.Start();\n");
            String oldRewritesPerformedName = "oldRewritesPerformed_" + seq.Id;
            source.AppendFront("int " + oldRewritesPerformedName + " = procEnv.PerformanceInfo.RewritesPerformed;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Matched(" + matchesName + ", " + matchName + ", " + specialStr + ");\n");
            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront(ruleName + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");
            source.AppendFront("++procEnv.PerformanceInfo.RewritesPerformed;\n");
            if(fireDebugEvents)
                source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            // rule applied, now execute the sequence
            seqGen.EmitSequence(seq.Seq, source);

            // if sequence execution failed, roll the changes back and try the next match of the rule
            source.AppendFront("if(!" + compGen.GetResultVar(seq.Seq) + ") {\n");
            source.Indent();
            source.AppendFront("procEnv.TransactionManager.Rollback(" + transactionIdName + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed = " + oldRewritesPerformedName + ";\n");

            source.AppendFront("if(" + matchesTriedName + " < " + matchesName + ".Count) {\n"); // further match available -> try it
            source.Indent();
            source.AppendFront("continue;\n");
            source.Unindent();
            source.AppendFront("} else {\n"); // all matches tried, all failed later on -> end in fail
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "false"));
            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution succeeded, commit the changes so far and succeed
            source.AppendFront("procEnv.TransactionManager.Commit(" + transactionIdName + ");\n");
            source.AppendFront(compGen.SetResultVar(seq, "true"));
            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }
    }
}
