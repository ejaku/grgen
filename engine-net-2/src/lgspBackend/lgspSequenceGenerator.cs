/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.IO;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A closure for an exec statement in an alternative, iterated or subpattern,
    /// containing the entities needed for the exec execution.
    /// These exec are executed at the end of the rule which directly or indirectly used them,
    /// long after the alternative/iterated/subpattern modification containing them has been applied.
    /// The real stuff depends on the xgrs and is generated, implementing this abstract class.
    /// </summary>
    public abstract class LGSPEmbeddedSequenceClosure
    {
        /// <summary>
        /// Executes the embedded sequence closure
        /// </summary>
        /// <param name="procEnv">the processing environment on which to apply the sequence, esp. containing the graph</param>
        /// <returns>the result of sequence execution</returns>
        public abstract bool exec(LGSPGraphProcessingEnvironment procEnv);
    }

    /// <summary>
    /// The C#-part responsible for compiling the XGRSs of the exec statements.
    /// </summary>
    public class LGSPSequenceGenerator
    {
        // the generator using us
        LGSPGrGen gen;

        // the model object of the .grg to compile
        IGraphModel model;

        // maps rule names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> rulesToOutputTypes;

        // maps rule names available in the .grg to compile to the list of the match filter names
        Dictionary<String, List<String>> rulesToFilters;
        
        // maps rule names available in the .grg to compile to the list of the top level entity names (nodes,edges,variables)
        Dictionary<String, List<String>> rulesToTopLevelEntities;
        // maps rule names available in the .grg to compile to the list of the top level entity types
        Dictionary<String, List<String>> rulesToTopLevelEntityTypes;

        // maps sequence names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> sequencesToInputTypes;
        // maps sequence names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> sequencesToOutputTypes;

        // maps procedure names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> proceduresToInputTypes;
        // maps procedure names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> proceduresToOutputTypes;
        
        // maps function names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> functionsToInputTypes;
        // maps function names available in the .grg to compile to the output typ name
        Dictionary<String, String> functionsToOutputType;
        
        // array containing the names of the rules available in the .grg to compile
        String[] ruleNames;
        // array containing the names of the sequences available in the .grg to compile
        String[] sequenceNames;
        // array containing the names of the procedures available in the .grg to compile
        String[] procedureNames;
        // array containing the names of the functions available in the .grg to compile
        String[] functionNames;
        // array containing the output types names of the functions available in the .grg to compile
        String[] functionOutputTypes;

        // environment for (type) checking the compiled sequences
        SequenceCheckingEnvironment env;

        // the used rules (so that a variable was created for easy acess to them)
		Dictionary<String, object> knownRules = new Dictionary<string, object>();

        // a counter for unique temporary variables needed as dummy variables
        // to receive the return/out values of rules/sequnces in case no assignment is given
        int tmpVarCtr;


        /// <summary>
        /// Constructs the sequence generator
        /// </summary>
        public LGSPSequenceGenerator(LGSPGrGen gen, IGraphModel model,
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes, Dictionary<String, List<String>> rulesToFilters,
            Dictionary<String, List<String>> rulesToTopLevelEntities, Dictionary<String, List<String>> rulesToTopLevelEntityTypes, 
            Dictionary<String, List<String>> sequencesToInputTypes, Dictionary<String, List<String>> sequencesToOutputTypes,
            Dictionary<String, List<String>> proceduresToInputTypes, Dictionary<String, List<String>> proceduresToOutputTypes,
            Dictionary<String, List<String>> functionsToInputTypes, Dictionary<String, String> functionsToOutputType)
        {
            this.gen = gen;
            this.model = model;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.rulesToFilters = rulesToFilters;
            this.rulesToTopLevelEntities = rulesToTopLevelEntities;
            this.rulesToTopLevelEntityTypes = rulesToTopLevelEntityTypes;
            this.sequencesToInputTypes = sequencesToInputTypes;
            this.sequencesToOutputTypes = sequencesToOutputTypes;
            this.proceduresToInputTypes = proceduresToInputTypes;
            this.proceduresToOutputTypes = proceduresToOutputTypes;
            this.functionsToInputTypes = functionsToInputTypes;
            this.functionsToOutputType = functionsToOutputType;

            // extract rule names from domain of rule names to input types map
            ruleNames = new String[rulesToInputTypes.Count];
            int i = 0;
            foreach(KeyValuePair<String, List<String>> ruleToInputTypes in rulesToInputTypes)
            {
                ruleNames[i] = ruleToInputTypes.Key;
                ++i;
         
            }
            // extract sequence names from domain of sequence names to input types map
            sequenceNames = new String[sequencesToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> sequenceToInputTypes in sequencesToInputTypes)
            {
                sequenceNames[i] = sequenceToInputTypes.Key;
                ++i;
            }
            // extract procedure names from domain of procedure names to input types map
            procedureNames = new String[proceduresToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> procedureToInputTypes in proceduresToInputTypes)
            {
                procedureNames[i] = procedureToInputTypes.Key;
                ++i;
            }
            // extract function names from domain of function names to input types map
            functionNames = new String[functionsToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> functionToInputTypes in functionsToInputTypes)
            {
                functionNames[i] = functionToInputTypes.Key;
                ++i;
            }
            // extract function output types from range of function names to output types map
            functionOutputTypes = new String[functionsToOutputType.Count];
            i = 0;
            foreach(KeyValuePair<String, String> functionToOutputType in functionsToOutputType)
            {
                functionOutputTypes[i] = functionToOutputType.Value;
                ++i;
            }

            // create the environment for (type) checking the compiled sequences after parsing
            env = new SequenceCheckingEnvironmentCompiled(
                ruleNames, sequenceNames, procedureNames, functionNames,
                rulesToInputTypes, rulesToOutputTypes, rulesToFilters, 
                rulesToTopLevelEntities, rulesToTopLevelEntityTypes,
                sequencesToInputTypes, sequencesToOutputTypes,
                proceduresToInputTypes, proceduresToOutputTypes,
                functionsToInputTypes, functionsToOutputType,
                model);
        }

        /// <summary>
        /// Returns string containing a C# expression to get the value of the sequence-local variable / graph-global variable given
        /// </summary>
        public string GetVar(SequenceVariable seqVar)
        {
            if(seqVar.Type == "")
            {
                return "procEnv.GetVariableValue(\"" + seqVar.PureName + "\")";
            }
            else
            {
                return "var_" + seqVar.Prefix + seqVar.PureName;
            }
        }

        /// <summary>
        /// Returns string containing a C# assignment to set the sequence-local variable / graph-global variable given
        /// to the value as computed by the C# expression in the string given
        /// </summary>
        public string SetVar(SequenceVariable seqVar, String valueToWrite)
        {
            if(seqVar.Type == "")
            {
                return "procEnv.SetVariableValue(\"" + seqVar.PureName + "\", " + valueToWrite + ");\n";
            }
            else
            {
                String cast = "(" + TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model) + ")";
                return "var_" + seqVar.Prefix + seqVar.PureName + " = " + cast + "(" + valueToWrite + ");\n";
            }
        }

        /// <summary>
        /// Returns string containing a C# declaration of the variable given
        /// </summary>
        public string DeclareVar(SequenceVariable seqVar)
        {
            if(seqVar.Type != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model));
                sb.Append(" ");
                sb.Append("var_" + seqVar.Prefix + seqVar.PureName);
                sb.Append(" = ");
                sb.Append(TypesHelper.DefaultValueString(seqVar.Type, model));
                sb.Append(";\n");
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Returns string containing a C# expression to get the value of the result variable of the sequence-like given
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public string GetResultVar(SequenceBase seq)
        {
            return "res_" + seq.Id;
        }

        /// <summary>
        /// Returns string containing a C# assignment to set the result variable of the sequence-like given
        /// to the value as computed by the C# expression in the string given
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public string SetResultVar(SequenceBase seq, String valueToWrite)
        {
            if(seq is Sequence)
                return "res_" + seq.Id + " = (bool)(" + valueToWrite + ");\n";
            else
                return "res_" + seq.Id + " = " + valueToWrite + ";\n";
        }

        /// <summary>
        /// Returns string containing C# declaration of the sequence-like result variable
        /// </summary>
        public string DeclareResultVar(SequenceBase seq)
        {
            if(seq is Sequence)
                return "bool res_" + seq.Id + ";\n";
            else
                return "object res_" + seq.Id + ";\n";
        }

        /// <summary>
        /// Emit variable declarations needed (only once for every variable)
        /// </summary>
        void EmitVarIfNew(SequenceVariable var, SourceBuilder source)
		{
			if(!var.Visited)
			{
                var.Visited = true;
                source.AppendFront(DeclareVar(var));
			}
		}

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits result variable declarations
        /// - emits sequence variable declarations (only once for every variable, declaration only possible at assignment targets)
        /// - collects used rules into knownRules, emit local rule declaration (only once for every rule)
        /// </summary>
		void EmitNeededVarAndRuleEntities(Sequence seq, SourceBuilder source)
		{
			source.AppendFront(DeclareResultVar(seq));

			switch(seq.SequenceType)
			{
                case SequenceType.AssignUserInputToVar:
                case SequenceType.AssignRandomIntToVar:
                case SequenceType.AssignRandomDoubleToVar:
                case SequenceType.DeclareVariable:
                case SequenceType.AssignConstToVar:
                case SequenceType.AssignContainerConstructorToVar:
                case SequenceType.AssignVarToVar:
                {
					SequenceAssignToVar toVar = (SequenceAssignToVar) seq;
                    EmitVarIfNew(toVar.DestVar, source);
					break;
				}

                case SequenceType.AssignSequenceResultToVar:
                case SequenceType.OrAssignSequenceResultToVar:
                case SequenceType.AndAssignSequenceResultToVar:
                {
					SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar) seq;
                    EmitVarIfNew(seqToVar.DestVar, source);
					EmitNeededVarAndRuleEntities(seqToVar.Seq, source);
					break;
				}

				case SequenceType.RuleCall:
				case SequenceType.RuleAllCall:
				{
					SequenceRuleCall seqRule = (SequenceRuleCall) seq;
					String ruleName = seqRule.ParamBindings.Name;
					if(!knownRules.ContainsKey(ruleName))
					{
                        knownRules.Add(ruleName, null);
                        source.AppendFront("Action_" + ruleName + " " + "rule_" + ruleName);
                        source.Append(" = Action_" + ruleName + ".Instance;\n");
                    }
                    // no handling for the input arguments seqRule.ParamBindings.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i=0; i<seqRule.ParamBindings.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqRule.ParamBindings.ReturnVars[i], source);
                    }
					break;
				}

                case SequenceType.SequenceCall:
                {
                    SequenceSequenceCall seqSeq = (SequenceSequenceCall)seq;
                    // no handling for the input arguments seqSeq.ParamBindings.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i = 0; i < seqSeq.ParamBindings.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqSeq.ParamBindings.ReturnVars[i], source);
                    }
                    break;
                }

                case SequenceType.ForContainer:
                {
                    SequenceForContainer seqFor = (SequenceForContainer)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    if (seqFor.VarDst != null) EmitVarIfNew(seqFor.VarDst, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    break;
                }

                case SequenceType.ForLookup:
                {
                    SequenceForLookup seqFor = (SequenceForLookup)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    break;
                }

                case SequenceType.ForAdjacentNodes:
                case SequenceType.ForAdjacentNodesViaIncoming:
                case SequenceType.ForAdjacentNodesViaOutgoing:
                case SequenceType.ForIncidentEdges:
                case SequenceType.ForIncomingEdges:
                case SequenceType.ForOutgoingEdges:
                {
                    SequenceForAdjacentIncident seqFor = (SequenceForAdjacentIncident)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    break;
                }

                case SequenceType.ForReachableNodes:
                case SequenceType.ForReachableNodesViaIncoming:
                case SequenceType.ForReachableNodesViaOutgoing:
                case SequenceType.ForReachableEdges:
                case SequenceType.ForReachableEdgesViaIncoming:
                case SequenceType.ForReachableEdgesViaOutgoing:
                {
                    SequenceForReachable seqFor = (SequenceForReachable)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    break;
                }

                case SequenceType.ForMatch:
                {
                    SequenceForMatch seqFor = (SequenceForMatch)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    EmitNeededVarAndRuleEntities(seqFor.Rule, source);
                    break;
                }

                case SequenceType.BooleanComputation:
                {
                    SequenceBooleanComputation seqBoolComp = (SequenceBooleanComputation)seq;
                    EmitNeededVarEntities(seqBoolComp.Computation, source);
                    break;
                }

				default:
					foreach(Sequence childSeq in seq.Children)
						EmitNeededVarAndRuleEntities(childSeq, source);
					break;
			}
		}

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits sequence variable declarations (only once for every variable, declaration only possible at assignment targets)
        /// </summary>
		void EmitNeededVarEntities(SequenceComputation seqComp, SourceBuilder source)
		{
            source.AppendFront(DeclareResultVar(seqComp));
            
            switch(seqComp.SequenceComputationType)
			{
                case SequenceComputationType.Assignment:
				{
					SequenceComputationAssignment assign = (SequenceComputationAssignment)seqComp;
                    EmitNeededVarEntities(assign.Target, source);
                    if(assign.SourceValueProvider is SequenceComputationAssignment)
                        EmitNeededVarEntities(assign.SourceValueProvider, source);
					break;
				}

                case SequenceComputationType.ProcedureCall:
                {
                    SequenceComputationProcedureCall seqProc = (SequenceComputationProcedureCall)seqComp;
                    // no handling for the input arguments seqProc.ParamBindings.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i = 0; i < seqProc.ParamBindings.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqProc.ParamBindings.ReturnVars[i], source);
                    }
                    break;
                }

                case SequenceComputationType.BuiltinProcedureCall:
                {
                    SequenceComputationBuiltinProcedureCall seqProc = (SequenceComputationBuiltinProcedureCall)seqComp;
                    // no handling for the input arguments seqProc.ParamBindings.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i = 0; i < seqProc.ReturnVars.Count; ++i)
                    {
                        EmitVarIfNew(seqProc.ReturnVars[i], source);
                    }
                    break;
                }

				default:
					foreach(SequenceComputation childSeqComp in seqComp.Children)
						EmitNeededVarEntities(childSeqComp, source);
					break;
			}
		}

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits sequence variable declarations (only once for every variable, declaration only possible at assignment targets)
        /// </summary>
		void EmitNeededVarEntities(AssignmentTarget tgt, SourceBuilder source)
		{
            source.AppendFront(DeclareResultVar(tgt));

            switch(tgt.AssignmentTargetType)
			{
				case AssignmentTargetType.Var:
				{
					AssignmentTargetVar var = (AssignmentTargetVar)tgt;
                    EmitVarIfNew(var.DestVar, source);
					break;
				}
                case AssignmentTargetType.YieldingToVar:
                {
                    AssignmentTargetYieldingVar var = (AssignmentTargetYieldingVar)tgt;
                    EmitVarIfNew(var.DestVar, source);
                    break;
                }

				default:
					break;
			}
		}

		void EmitLazyOp(SequenceBinary seq, SourceBuilder source, bool reversed)
		{
            Sequence seqLeft;
            Sequence seqRight;
            if(reversed) {
                Debug.Assert(seq.SequenceType != SequenceType.IfThen);
                seqLeft = seq.Right;
                seqRight = seq.Left;
            } else {
                seqLeft = seq.Left;
                seqRight = seq.Right;
            }

			EmitSequence(seqLeft, source);

            if(seq.SequenceType == SequenceType.LazyOr) {
                source.AppendFront("if(" + GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(SetResultVar(seq, "true"));
                source.Unindent();
            } else if(seq.SequenceType == SequenceType.LazyAnd) {
                source.AppendFront("if(!" + GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(SetResultVar(seq, "false"));
                source.Unindent();
            } else { //seq.SequenceType==SequenceType.IfThen -- lazy implication
                source.AppendFront("if(!" + GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(SetResultVar(seq, "true"));
                source.Unindent();
            }

			source.AppendFront("else\n");
			source.AppendFront("{\n");
			source.Indent();

            EmitSequence(seqRight, source);
            source.AppendFront(SetResultVar(seq, GetResultVar(seqRight)));

            source.Unindent();
			source.AppendFront("}\n");
		}

        void EmitRuleOrRuleAllCall(SequenceRuleCall seqRule, SourceBuilder source)
        {
            RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
            String specialStr = seqRule.Special ? "true" : "false";
            String parameters = BuildParameters(paramBindings);
            String matchingPatternClassName = "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seqRule.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seqRule.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.Name
                + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                + parameters + ");\n");
            if(seqRule.Filter != null)
            {
                if(seqRule.Filter == "auto")
                    source.AppendFrontFormat("MatchFilters.Filter_{0}_{1}(procEnv, {2});\n", patternName, seqRule.Filter, matchesName);
                else
                    source.AppendFrontFormat("MatchFilters.Filter_{0}(procEnv, {1});\n", seqRule.Filter, matchesName);
            }

            if(gen.FireEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
            if(seqRule is SequenceRuleAllCall
                && ((SequenceRuleAllCall)seqRule).ChooseRandom
                && ((SequenceRuleAllCall)seqRule).MinSpecified)
            {
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                source.AppendFrontFormat("int minmatchesvar_{0} = (int){1};\n", seqRuleAll.Id, GetVar(seqRuleAll.MinVarChooseRandom));
                source.AppendFrontFormat("if({0}.Count < minmatchesvar_{1}) {{\n", matchesName, seqRuleAll.Id);
            }
            else
            {
                source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            }
            source.Indent();
            source.AppendFront(SetResultVar(seqRule, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seqRule, "true"));
            if(gen.UsePerfInfo)
                source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(gen.FireEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            BuildReturnParameters(paramBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

            if(seqRule.SequenceType == SequenceType.RuleCall)
            {
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
            }
            else if(!((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
            {
                // iterate through matches, use Modify on each, fire the next match event after the first
                String enumeratorName = "enum_" + seqRule.Id;
                source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                source.AppendFront("if(" + matchName + "!=" + matchesName + ".FirstExact) procEnv.RewritingNextMatch();\n");
                if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
            {
                // as long as a further rewrite has to be selected: randomly choose next match, rewrite it and remove it from available matches; fire the next match event after the first
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                source.AppendFrontFormat("int numchooserandomvar_{0} = (int){1};\n", seqRuleAll.Id, seqRuleAll.MaxVarChooseRandom != null ? GetVar(seqRuleAll.MaxVarChooseRandom) : (seqRuleAll.MinSpecified ? "2147483647" : "1"));
                source.AppendFrontFormat("if({0}.Count < numchooserandomvar_{1}) numchooserandomvar_{1} = {0}.Count;\n", matchesName, seqRule.Id);
                source.AppendFrontFormat("for(int i = 0; i < numchooserandomvar_{0}; ++i)\n", seqRule.Id);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("if(i != 0) procEnv.RewritingNextMatch();\n");
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }

            if(gen.FireEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        void EmitSequenceCall(SequenceSequenceCall seqSeq, SourceBuilder source)
        {
            SequenceInvocationParameterBindings paramBindings = seqSeq.ParamBindings;
            String parameters = BuildParameters(paramBindings);
            String outParameterDeclarations;
            String outArguments;
            String outAssignments;
            BuildOutParameters(paramBindings, out outParameterDeclarations, out outArguments, out outAssignments);

            if(outParameterDeclarations.Length != 0)
                source.AppendFront(outParameterDeclarations + "\n");
            source.AppendFront("if(Sequence_"+paramBindings.Name+".ApplyXGRS_" + paramBindings.Name
                                + "(procEnv" + parameters + outArguments + ")) {\n");
            source.Indent();
            if(outAssignments.Length != 0)
                source.AppendFront(outAssignments + "\n");
            source.AppendFront(SetResultVar(seqSeq, "true"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seqSeq, "false"));
            source.Unindent();
            source.AppendFront("}\n");
        }

		void EmitSequence(Sequence seq, SourceBuilder source)
		{
			switch(seq.SequenceType)
			{
				case SequenceType.RuleCall:
                case SequenceType.RuleAllCall:
                    EmitRuleOrRuleAllCall((SequenceRuleCall)seq, source);
                    break;

                case SequenceType.SequenceCall:
                    EmitSequenceCall((SequenceSequenceCall)seq, source);
                    break;

				case SequenceType.Not:
				{
					SequenceNot seqNot = (SequenceNot) seq;
					EmitSequence(seqNot.Seq, source);
					source.AppendFront(SetResultVar(seqNot, "!"+GetResultVar(seqNot.Seq)));
					break;
				}

				case SequenceType.LazyOr:
				case SequenceType.LazyAnd:
                case SequenceType.IfThen:
				{
					SequenceBinary seqBin = (SequenceBinary) seq;
					if(seqBin.Random)
					{
                        Debug.Assert(seq.SequenceType != SequenceType.IfThen);

                        source.AppendFront("if(GRGEN_LIBGR.Sequence.randomGenerator.Next(2) == 1)\n");
						source.AppendFront("{\n");
						source.Indent();
                        EmitLazyOp(seqBin, source, true);
						source.Unindent();
						source.AppendFront("}\n");
						source.AppendFront("else\n");
						source.AppendFront("{\n");
                        source.Indent();
                        EmitLazyOp(seqBin, source, false);
						source.Unindent();
						source.AppendFront("}\n");
					}
					else
					{
                        EmitLazyOp(seqBin, source, false);
					}
					break;
				}

                case SequenceType.ThenLeft:
                case SequenceType.ThenRight:
				case SequenceType.StrictAnd:
				case SequenceType.StrictOr:
				case SequenceType.Xor:
				{
					SequenceBinary seqBin = (SequenceBinary) seq;
					if(seqBin.Random)
					{
                        source.AppendFront("if(GRGEN_LIBGR.Sequence.randomGenerator.Next(2) == 1)\n");
						source.AppendFront("{\n");
						source.Indent();
						EmitSequence(seqBin.Right, source);
						EmitSequence(seqBin.Left, source);
						source.Unindent();
						source.AppendFront("}\n");
						source.AppendFront("else\n");
						source.AppendFront("{\n");
                        source.Indent();
						EmitSequence(seqBin.Left, source);
						EmitSequence(seqBin.Right, source);
						source.Unindent();
						source.AppendFront("}\n");
					}
					else
					{
						EmitSequence(seqBin.Left, source);
						EmitSequence(seqBin.Right, source);
					}

                    if(seq.SequenceType==SequenceType.ThenLeft) {
                        source.AppendFront(SetResultVar(seq, GetResultVar(seqBin.Left)));
                        break;
                    } else if(seq.SequenceType==SequenceType.ThenRight) {
                        source.AppendFront(SetResultVar(seq, GetResultVar(seqBin.Right)));
                        break;
                    }

                    String op;
				    switch(seq.SequenceType)
				    {
					    case SequenceType.StrictAnd: op = "&"; break;
					    case SequenceType.StrictOr:  op = "|"; break;
					    case SequenceType.Xor:       op = "^"; break;
					    default: throw new Exception("Internal error in EmitSequence: Should not have reached this!");
				    }
				    source.AppendFront(SetResultVar(seq, GetResultVar(seqBin.Left) + " "+op+" " + GetResultVar(seqBin.Right)));
					break;
				}

                case SequenceType.IfThenElse:
                {
                    SequenceIfThenElse seqIf = (SequenceIfThenElse)seq;

                    EmitSequence(seqIf.Condition, source);

                    source.AppendFront("if(" + GetResultVar(seqIf.Condition) + ")");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.TrueCase, source);
                    source.AppendFront(SetResultVar(seqIf, GetResultVar(seqIf.TrueCase)));

                    source.Unindent();
                    source.AppendFront("}\n");
                    source.AppendFront("else\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.FalseCase, source);
                    source.AppendFront(SetResultVar(seqIf, GetResultVar(seqIf.FalseCase)));

                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForContainer:
                {
                    SequenceForContainer seqFor = (SequenceForContainer)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    if(seqFor.Container.Type == "")
                    {
                        // type not statically known? -> might be Dictionary or List or Deque dynamically, must decide at runtime
                        source.AppendFront("if(" + GetVar(seqFor.Container) + " is IList) {\n");
                        source.Indent();

                        source.AppendFront("IList entry_" + seqFor.Id + " = (IList) " + GetVar(seqFor.Container) + ";\n");
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else if(" + GetVar(seqFor.Container) + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        source.AppendFront("GRGEN_LIBGR.IDeque entry_" + seqFor.Id + " = (GRGEN_LIBGR.IDeque) " + GetVar(seqFor.Container) + ";\n");
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        source.AppendFront("foreach(DictionaryEntry entry_" + seqFor.Id + " in (IDictionary)" + GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqFor.Container.Type.StartsWith("array"))
                    {
                        String arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        source.AppendFrontFormat("List<{0}> entry_{1} = (List<{0}>) " + GetVar(seqFor.Container) + ";\n", arrayValueType, seqFor.Id);
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();

                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqFor.Container.Type.StartsWith("deque"))
                    {
                        String dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        source.AppendFrontFormat("GRGEN_LIBGR.Deque<{0}> entry_{1} = (GRGEN_LIBGR.Deque<{0}>) " + GetVar(seqFor.Container) + ";\n", dequeValueType, seqFor.Id);
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();

                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        String srcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        String dstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqFor.Container.Type), model);
                        source.AppendFront("foreach(KeyValuePair<" + srcType + "," + dstType + "> entry_" + seqFor.Id + " in " + GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();

                        source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        if(seqFor.VarDst != null)
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }

                    break;
                }

                case SequenceType.ForLookup:
                {
                    SequenceForLookup seqFor = (SequenceForLookup)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    NodeType nodeType = TypesHelper.GetNodeType(seqFor.Var.Type, model);
                    EdgeType edgeType = TypesHelper.GetEdgeType(seqFor.Var.Type, model);
                    if(nodeType != null)
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode elem_{0} in graph.GetCompatibleNodes(GRGEN_LIBGR.TypesHelper.GetNodeType(\"" + seqFor.Var.Type + "\", graph.Model)))\n", seqFor.Id);
                    else // further check/case is needed in case variables of dynamic type would be allowed
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge elem_{0} in graph.GetCompatibleEdges(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"" + seqFor.Var.Type + "\", graph.Model)))\n", seqFor.Id);
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(SetVar(seqFor.Var, "elem_" + seqFor.Id));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForAdjacentNodes:
                case SequenceType.ForAdjacentNodesViaIncoming:
                case SequenceType.ForAdjacentNodesViaOutgoing:
                case SequenceType.ForIncidentEdges:
                case SequenceType.ForIncomingEdges:
                case SequenceType.ForOutgoingEdges:
                {
                    SequenceForAdjacentIncident seqFor = (SequenceForAdjacentIncident)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = GetSequenceExpression(seqFor.Expr, source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);

                    string incidentEdgeTypeExpr = ExtractEdgeType(source, seqFor.IncidentEdgeType);
                    string adjacentNodeTypeExpr = ExtractNodeType(source, seqFor.AdjacentNodeType);
                    
                    string edgeMethod;
                    string theOther;
                    string iterationVariable;
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForAdjacentNodes:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = theOther;
                            break;
                        case SequenceType.ForAdjacentNodesViaIncoming:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = theOther;
                            break;
                        case SequenceType.ForAdjacentNodesViaOutgoing:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = theOther;
                            break;
                        case SequenceType.ForIncidentEdges:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = "edge_" + seqFor.Id;
                            break;
                        case SequenceType.ForIncomingEdges:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = "edge_" + seqFor.Id;
                            break;
                        case SequenceType.ForOutgoingEdges:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = "edge_" + seqFor.Id;
                            break;
                        default:
                            edgeMethod = theOther = iterationVariable = "INTERNAL ERROR";
                            break;
                    }

                    source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatible{1}({2}))\n",
                        seqFor.Id, edgeMethod, incidentEdgeTypeExpr);
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFrontFormat("if(!{0}.InstanceOf({1}))\n",
                        theOther, adjacentNodeTypeExpr);
                    source.AppendFront("\tcontinue;\n");

                    source.AppendFront(SetVar(seqFor.Var, iterationVariable));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForReachableNodes:
                case SequenceType.ForReachableNodesViaIncoming:
                case SequenceType.ForReachableNodesViaOutgoing:
                case SequenceType.ForReachableEdges:
                case SequenceType.ForReachableEdgesViaIncoming:
                case SequenceType.ForReachableEdgesViaOutgoing:
                {
                    SequenceForReachable seqFor = (SequenceForReachable)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = GetSequenceExpression(seqFor.Expr, source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);

                    string incidentEdgeTypeExpr = ExtractEdgeType(source, seqFor.IncidentEdgeType);
                    string adjacentNodeTypeExpr = ExtractNodeType(source, seqFor.AdjacentNodeType);

                    string reachableMethod;
                    string iterationVariable;
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForReachableNodes:
                            reachableMethod = "";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            break;
                        case SequenceType.ForReachableNodesViaIncoming:
                            reachableMethod = "Incoming";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            break;
                        case SequenceType.ForReachableNodesViaOutgoing:
                            reachableMethod = "Outgoing";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            break;
                        case SequenceType.ForReachableEdges:
                            reachableMethod = "Edges";
                            iterationVariable = "edge_" + seqFor.Id;
                            break;
                        case SequenceType.ForReachableEdgesViaIncoming:
                            reachableMethod = "EdgesIncoming";
                            iterationVariable = "edge_" + seqFor.Id;
                            break;
                        case SequenceType.ForReachableEdgesViaOutgoing:
                            reachableMethod = "EdgesOutgoing";
                            iterationVariable = "edge_" + seqFor.Id;
                            break;
                        default:
                            reachableMethod = iterationVariable = "INTERNAL ERROR";
                            break;
                    }

                    if(seqFor.SequenceType == SequenceType.ForReachableNodes || seqFor.SequenceType == SequenceType.ForReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableNodesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GraphHelper.Reachable{1}(node_{0}, ({2}), ({3}), graph))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GraphHelper.Reachable{1}(edge_{0}, ({2}), ({3}), graph))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront(SetVar(seqFor.Var, iterationVariable));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForMatch:
                {
                    SequenceForMatch seqFor = (SequenceForMatch)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    RuleInvocationParameterBindings paramBindings = seqFor.Rule.ParamBindings;
                    String specialStr = seqFor.Rule.Special ? "true" : "false";
                    String parameters = BuildParameters(paramBindings);
                    String matchingPatternClassName = "Rule_" + paramBindings.Name;
                    String patternName = paramBindings.Name;
                    String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                    String matchName = "match_" + seqFor.Id;
                    String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                    String matchesName = "matches_" + seqFor.Id;
                    source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.Name
                        + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
                    if(seqFor.Rule.Filter != null)
                    {
                        if(seqFor.Rule.Filter == "auto")
                            source.AppendFrontFormat("MatchFilters.Filter_{0}_{1}(procEnv, {2});\n", patternName, seqFor.Rule.Filter, matchesName);
                        else
                            source.AppendFrontFormat("MatchFilters.Filter_{0}(procEnv, {1});\n", seqFor.Rule.Filter, matchesName);
                    }

                    source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                    source.Indent();
                    source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
                    if(gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                    if(gen.FireEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

                    String returnParameterDeclarations;
                    String returnArguments;
                    String returnAssignments;
                    BuildReturnParameters(paramBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

                    // apply the sequence for every match found
                    String enumeratorName = "enum_" + seqFor.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    source.AppendFront(SetVar(seqFor.Var, matchName));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

				case SequenceType.IterationMin:
				{
                    SequenceIterationMin seqMin = (SequenceIterationMin)seq;
					source.AppendFront("long i_" + seqMin.Id + " = 0;\n");
					source.AppendFront("while(true)\n");
					source.AppendFront("{\n");
					source.Indent();
					EmitSequence(seqMin.Seq, source);
					source.AppendFront("if(!" + GetResultVar(seqMin.Seq) + ") break;\n");
					source.AppendFront("i_" + seqMin.Id + "++;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront(SetResultVar(seqMin, "i_" + seqMin.Id + " >= " + seqMin.Min));
					break;
				}

				case SequenceType.IterationMinMax:
				{
                    SequenceIterationMinMax seqMinMax = (SequenceIterationMinMax)seq;
					source.AppendFront("long i_" + seqMinMax.Id + " = 0;\n");
					source.AppendFront("for(; i_" + seqMinMax.Id + " < " + seqMinMax.Max + "; i_" + seqMinMax.Id + "++)\n");
					source.AppendFront("{\n");
					source.Indent();
					EmitSequence(seqMinMax.Seq, source);
                    source.AppendFront("if(!" + GetResultVar(seqMinMax.Seq) + ") break;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront(SetResultVar(seqMinMax, "i_" + seqMinMax.Id + " >= " + seqMinMax.Min));
					break;
				}

                case SequenceType.DeclareVariable:
                {
                    SequenceDeclareVariable seqDeclVar = (SequenceDeclareVariable)seq;
                    source.AppendFront(SetVar(seqDeclVar.DestVar, TypesHelper.DefaultValueString(seqDeclVar.DestVar.Type, env.Model)));
                    source.AppendFront(SetResultVar(seqDeclVar, "true"));
                    break;
                }

				case SequenceType.AssignConstToVar:
				{
					SequenceAssignConstToVar seqToVar = (SequenceAssignConstToVar) seq;
                    source.AppendFront(SetVar(seqToVar.DestVar, GetConstant(seqToVar.Constant)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
					break;
				}

                case SequenceType.AssignContainerConstructorToVar:
                {
                    SequenceAssignContainerConstructorToVar seqToVar = (SequenceAssignContainerConstructorToVar)seq;
                    source.AppendFront(SetVar(seqToVar.DestVar, GetSequenceExpression(seqToVar.Constructor, source)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignVarToVar:
                {
                    SequenceAssignVarToVar seqToVar = (SequenceAssignVarToVar)seq;
                    source.AppendFront(SetVar(seqToVar.DestVar, GetVar(seqToVar.Variable)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(SetVar(seqToVar.DestVar, GetResultVar(seqToVar.Seq)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.OrAssignSequenceResultToVar:
                {
                    SequenceOrAssignSequenceResultToVar seqToVar = (SequenceOrAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(SetVar(seqToVar.DestVar, GetResultVar(seqToVar.Seq) + "|| (bool)" + GetVar(seqToVar.DestVar)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AndAssignSequenceResultToVar:
                {
                    SequenceAndAssignSequenceResultToVar seqToVar = (SequenceAndAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(SetVar(seqToVar.DestVar, GetResultVar(seqToVar.Seq) + "&& (bool)" + GetVar(seqToVar.DestVar)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignUserInputToVar:
                {
                    throw new Exception("Internal Error: the AssignUserInputToVar is interpreted only (no Debugger available at lgsp level)");
                }

                case SequenceType.AssignRandomIntToVar:
                {
                    SequenceAssignRandomIntToVar seqRandomToVar = (SequenceAssignRandomIntToVar)seq;
                    source.AppendFront(SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.Next(" + seqRandomToVar.Number + ")"));
                    source.AppendFront(SetResultVar(seqRandomToVar, "true"));
                    break;
                }

                case SequenceType.AssignRandomDoubleToVar:
                {
                    SequenceAssignRandomDoubleToVar seqRandomToVar = (SequenceAssignRandomDoubleToVar)seq;
                    source.AppendFront(SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()"));
                    source.AppendFront(SetResultVar(seqRandomToVar, "true"));
                    break;
                }

                case SequenceType.LazyOrAll:
                {
                    SequenceLazyOrAll seqAll = (SequenceLazyOrAll)seq;
                    EmitSequenceAll(seqAll, true, true, source);
                    break;
                }

                case SequenceType.LazyAndAll:
                {
                    SequenceLazyAndAll seqAll = (SequenceLazyAndAll)seq;
                    EmitSequenceAll(seqAll, false, true, source);
                    break;
                }

                case SequenceType.StrictOrAll:
                {
                    SequenceStrictOrAll seqAll = (SequenceStrictOrAll)seq;
                    EmitSequenceAll(seqAll, true, false, source);
                    break;
                }

                case SequenceType.StrictAndAll:
                {
                    SequenceStrictAndAll seqAll = (SequenceStrictAndAll)seq;
                    EmitSequenceAll(seqAll, false, false, source);
                    break;
                }

                case SequenceType.WeightedOne:
                {
                    SequenceWeightedOne seqWeighted = (SequenceWeightedOne)seq;
                    EmitSequenceWeighted(seqWeighted, source);
                    break;
                }

                case SequenceType.SomeFromSet:
                {
                    SequenceSomeFromSet seqSome = (SequenceSomeFromSet)seq;
                    EmitSequenceSome(seqSome, source);
                    break;
                }

				case SequenceType.Transaction:
				{
					SequenceTransaction seqTrans = (SequenceTransaction) seq;
                    source.AppendFront("int transID_" + seqTrans.Id + " = procEnv.TransactionManager.Start();\n");
					EmitSequence(seqTrans.Seq, source);
                    source.AppendFront("if("+ GetResultVar(seqTrans.Seq) + ") procEnv.TransactionManager.Commit(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront("else procEnv.TransactionManager.Rollback(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront(SetResultVar(seqTrans, GetResultVar(seqTrans.Seq)));
					break;
				}

                case SequenceType.Backtrack:
                {
                    SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                    EmitSequenceBacktrack(seqBack, source);
                    break;
                }

                case SequenceType.Pause:
                {
                    SequencePause seqPause = (SequencePause)seq;
                    source.AppendFront("procEnv.TransactionManager.Pause();\n");
                    EmitSequence(seqPause.Seq, source);
                    source.AppendFront("procEnv.TransactionManager.Resume();\n");
                    source.AppendFront(SetResultVar(seqPause, GetResultVar(seqPause.Seq)));
                    break;
                }

                case SequenceType.BooleanComputation:
                {
                    SequenceBooleanComputation seqComp = (SequenceBooleanComputation)seq;
                    EmitSequenceComputation(seqComp.Computation, source);
                    if(seqComp.Computation.ReturnsValue)
                        source.AppendFront(SetResultVar(seqComp, "!GRGEN_LIBGR.TypesHelper.IsDefaultValue(" + GetResultVar(seqComp.Computation) + ")"));
                    else
                        source.AppendFront(SetResultVar(seqComp, "true"));
                    break;
                }

				default:
					throw new Exception("Unknown sequence type: " + seq.SequenceType);
			}
		}

        public void EmitSequenceBacktrack(SequenceBacktrack seq, SourceBuilder source)
        {
            RuleInvocationParameterBindings paramBindings = seq.Rule.ParamBindings;
            String specialStr = seq.Rule.Special ? "true" : "false";
            String parameters = BuildParameters(paramBindings);
            String matchingPatternClassName = "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seq.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seq.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.Name
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            if(seq.Rule.Filter != null)
            {
                if(seq.Rule.Filter == "auto")
                    source.AppendFrontFormat("MatchFilters.Filter_{0}_{1}(procEnv, {2});\n", patternName, seq.Rule.Filter, matchesName);
                else
                    source.AppendFrontFormat("MatchFilters.Filter_{0}(procEnv, {1});\n", seq.Rule.Filter, matchesName);
            }

            source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seq, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seq, "true")); // shut up compiler
            source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
            if(gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(gen.FireEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            BuildReturnParameters(paramBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

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
            source.AppendFront("int transID_" + seq.Id + " = procEnv.TransactionManager.Start();\n");
            source.AppendFront("int oldRewritesPerformed_" + seq.Id + " = -1;\n");
            source.AppendFront("if(procEnv.PerformanceInfo!=null) oldRewritesPerformed_"+seq.Id+" = procEnv.PerformanceInfo.RewritesPerformed;\n");
            if(gen.FireEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", " + matchName + ", " + specialStr + ");\n");
            if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
            if(gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
            if(gen.FireEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            // rule applied, now execute the sequence
            EmitSequence(seq.Seq, source);

            // if sequence execution failed, roll the changes back and try the next match of the rule
            source.AppendFront("if(!" + GetResultVar(seq.Seq) + ") {\n");
            source.Indent();
            source.AppendFront("procEnv.TransactionManager.Rollback(transID_" + seq.Id + ");\n");
            if(gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed_" + seq.Id + ";\n");

            source.AppendFront("if(" + matchesTriedName + " < " + matchesName + ".Count) {\n"); // further match available -> try it
            source.Indent();
            source.AppendFront("continue;\n");
            source.Unindent();
            source.AppendFront("} else {\n"); // all matches tried, all failed later on -> end in fail
            source.Indent();
            source.AppendFront(SetResultVar(seq, "false"));
            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution succeeded, commit the changes so far and succeed
            source.AppendFront("procEnv.TransactionManager.Commit(transID_" + seq.Id + ");\n");
            source.AppendFront(SetResultVar(seq, "true"));
            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitSequenceAll(SequenceNAry seqAll, bool disjunction, bool lazy, SourceBuilder source)
        {
            source.AppendFront(SetResultVar(seqAll, disjunction ? "false" : "true"));
            source.AppendFrontFormat("bool continue_{0} = true;\n", seqAll.Id);
            source.AppendFrontFormat("List<int> sequencestoexecutevar_{0} = new List<int>({1});\n", seqAll.Id, seqAll.Sequences.Count);
            source.AppendFrontFormat("for(int i = 0; i < {1}; ++i) sequencestoexecutevar_{0}.Add(i);\n", seqAll.Id, seqAll.Sequences.Count);
            source.AppendFrontFormat("while(sequencestoexecutevar_{0}.Count>0 && continue_{0})\n", seqAll.Id);
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("int positionofsequencetoexecute_{0} = GRGEN_LIBGR.Sequence.randomGenerator.Next(sequencestoexecutevar_{0}.Count);\n", seqAll.Id);
            source.AppendFrontFormat("switch(sequencestoexecutevar_{0}[positionofsequencetoexecute_{0}])\n", seqAll.Id);
            source.AppendFront("{\n");
            source.Indent();
            for(int i = 0; i < seqAll.Sequences.Count; ++i)
            {
                source.AppendFrontFormat("case {0}:\n", i);
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqAll.Sequences[i], source);
                source.AppendFrontFormat("sequencestoexecutevar_{0}.Remove({1});\n", seqAll.Id, i);
                source.AppendFront(SetResultVar(seqAll, GetResultVar(seqAll) + (disjunction ? " || " : " && ") + GetResultVar(seqAll.Sequences[i])));
                if(lazy)
                    source.AppendFrontFormat("if(" + (disjunction?"":"!") + GetResultVar(seqAll) + ") continue_{0} = false;\n", seqAll.Id);
                source.AppendFront("break;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitSequenceWeighted(SequenceWeightedOne seqWeighted, SourceBuilder source)
        {
            source.AppendFrontFormat("double pointtoexec_{0} = GRGEN_LIBGR.Sequence.randomGenerator.NextDouble() * {1};\n", seqWeighted.Id, seqWeighted.Numbers[seqWeighted.Numbers.Count - 1].ToString(System.Globalization.CultureInfo.InvariantCulture));
            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
            {
                if(i == 0)
                    source.AppendFrontFormat("if(pointtoexec_{0} <= {1})\n", seqWeighted.Id, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                else if(i == seqWeighted.Sequences.Count - 1)
                    source.AppendFrontFormat("else\n");
                else
                    source.AppendFrontFormat("else if(pointtoexec_{0} <= {1})\n", seqWeighted.Id, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqWeighted.Sequences[i], source);
                source.AppendFront(SetResultVar(seqWeighted, GetResultVar(seqWeighted.Sequences[i])));
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        void EmitSequenceSome(SequenceSomeFromSet seqSome, SourceBuilder source)
        {
            source.AppendFront(SetResultVar(seqSome, "false"));

            // emit code for matching all the contained rules
            for (int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
                String specialStr = seqRule.Special ? "true" : "false";
                String parameters = BuildParameters(paramBindings);
                String matchingPatternClassName = "Rule_" + paramBindings.Name;
                String patternName = paramBindings.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;
                source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.Name
                    + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                    + parameters + ");\n");
                if(seqRule.Filter != null)
                {
                    if(seqRule.Filter == "auto")
                        source.AppendFrontFormat("MatchFilters.Filter_{0}_{1}(procEnv, {2});\n", patternName, seqRule.Filter, matchesName);
                    else
                        source.AppendFrontFormat("MatchFilters.Filter_{0}(procEnv, {1});\n", seqRule.Filter, matchesName);
                }
                if (gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                source.Indent();
                source.AppendFront(SetResultVar(seqSome, "true"));
                source.Unindent();
                source.AppendFront("}\n");
            }

            // emit code for deciding on the match to rewrite
            String totalMatchToApply = "total_match_to_apply_" + seqSome.Id;
            String curTotalMatch = "cur_total_match_" + seqSome.Id;
            if (seqSome.Random)
            {
                source.AppendFront("int " + totalMatchToApply + " = 0;\n");
                for (int i = 0; i < seqSome.Sequences.Count; ++i)
                {
                    SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                    String matchesName = "matches_" + seqRule.Id;
                    if (seqRule.SequenceType == SequenceType.RuleCall)
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                    else if (!((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                        source.AppendFront("if(" + matchesName + ".Count>0) ++" + totalMatchToApply + ";\n");
                    else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                }
                source.AppendFront(totalMatchToApply + " = GRGEN_LIBGR.Sequence.randomGenerator.Next(" + totalMatchToApply + ");\n");
                source.AppendFront("int " + curTotalMatch + " = 0;\n");
            }

            // code to handle the rewrite next match
            String firstRewrite = "first_rewrite_" + seqSome.Id;
            source.AppendFront("bool " + firstRewrite + " = true;\n");

            // emit code for rewriting all the contained rules which got matched
            for (int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
                String specialStr = seqRule.Special ? "true" : "false";
                String matchingPatternClassName = "Rule_" + paramBindings.Name;
                String patternName = paramBindings.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchName = "match_" + seqRule.Id;
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;

                if(seqSome.Random)
                    source.AppendFront("if(" + matchesName + ".Count!=0 && " + curTotalMatch + "<=" + totalMatchToApply + ") {\n");
                else
                    source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                source.Indent();

                String returnParameterDeclarations;
                String returnArguments;
                String returnAssignments;
                BuildReturnParameters(paramBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

                if (seqRule.SequenceType == SequenceType.RuleCall)
                {
                    if (seqSome.Random) {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                    if (gen.FireEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (gen.FireEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    if (gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");

                    if (seqSome.Random) {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else if (!((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                {
                    if (seqSome.Random)
                    {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    // iterate through matches, use Modify on each, fire the next match event after the first
                    String enumeratorName = "enum_" + seqRule.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    if (gen.FireEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (gen.FireEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    if (gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");
                    source.Unindent();
                    source.AppendFront("}\n");

                    if (seqSome.Random)
                    {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                {
                    if (seqSome.Random)
                    {
                        // for the match selected: rewrite it
                        String enumeratorName = "enum_" + seqRule.Id;
                        source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                        source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                        source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                        if (gen.FireEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (gen.FireEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        if (gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        // randomly choose match, rewrite it and remove it from available matches
                        source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".GetMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                        if (gen.FireEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (gen.FireEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + paramBindings.Name + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        if (gen.UsePerfInfo) source.AppendFront("if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                    }
                }

                if (gen.FireEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
        }

  		void EmitSequenceComputation(SequenceComputation seqComp, SourceBuilder source)
		{
			switch(seqComp.SequenceComputationType)
			{
                case SequenceComputationType.Then:
                {
                    SequenceComputationThen seqThen = (SequenceComputationThen)seqComp;
                    EmitSequenceComputation(seqThen.left, source);
                    EmitSequenceComputation(seqThen.right, source);
                    source.AppendFront(SetResultVar(seqThen, GetResultVar(seqThen.right)));
                    break;
                }
                
                case SequenceComputationType.Assignment:
                {
                    SequenceComputationAssignment seqAssign = (SequenceComputationAssignment)seqComp;
                    if(seqAssign.SourceValueProvider is SequenceComputationAssignment)
                    {
                        EmitSequenceComputation(seqAssign.SourceValueProvider, source);
                        EmitAssignment(seqAssign.Target, GetResultVar(seqAssign.SourceValueProvider), source);
                        source.AppendFront(SetResultVar(seqAssign, GetResultVar(seqAssign.Target)));
                    }
                    else
                    {
                        string comp = GetSequenceExpression((SequenceExpression)seqAssign.SourceValueProvider, source);
                        EmitAssignment(seqAssign.Target, comp, source);
                        source.AppendFront(SetResultVar(seqAssign, GetResultVar(seqAssign.Target)));
                    }
                    break;
                }

                case SequenceComputationType.VariableDeclaration:
                {
                    SequenceComputationVariableDeclaration seqVarDecl = (SequenceComputationVariableDeclaration)seqComp;
                    source.AppendFront(SetVar(seqVarDecl.Target, TypesHelper.DefaultValueString(seqVarDecl.Target.Type, model)));
                    source.AppendFront(SetResultVar(seqVarDecl, GetVar(seqVarDecl.Target)));
                    break;
                }

                case SequenceComputationType.ContainerAdd:
                {
                    SequenceComputationContainerAdd seqAdd = (SequenceComputationContainerAdd)seqComp;

                    if(seqAdd.MethodCall != null)
                    {
                        EmitSequenceComputation(seqAdd.MethodCall, source);
                    }
                    string container = GetContainerValue(seqAdd);

                    if(seqAdd.ContainerType(env) == "")
                    {
                        if(seqAdd.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqAdd.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
                        }
                        string containerVar = "tmp_eval_once_" + seqAdd.Id;
                        source.AppendFront("object " + containerVar + " = " + container + ";\n");
                        string sourceValue = "srcval_" + seqAdd.Id;
                        source.AppendFront("object " + sourceValue + " = " + GetSequenceExpression(seqAdd.Expr, source) + ";\n");
                        string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
                        source.AppendFront("if(" + containerVar + " is IList) {\n");
                        source.Indent();

                        if(destinationValue != null && !TypesHelper.IsSameOrSubtype(seqAdd.ExprDst.Type(env), "int", model))
                            source.AppendFront("throw new Exception(\"Can't add non-int key to array\");\n");
                        else
                        {
                            string array = "((System.Collections.IList)" + containerVar + ")";
                            if(destinationValue != null)
                                source.AppendFront("int " + destinationValue + " = (int)" + GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                            if(seqAdd.Attribute != null)
                            {
                                if(destinationValue != null)
                                {
                                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                                }
                                else
                                {
                                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                                }
                            }
                            if(destinationValue == null)
                                source.AppendFront(array + ".Add(" + sourceValue + ");\n");
                            else
                                source.AppendFront(array + ".Insert(" + destinationValue + ", " + sourceValue + ");\n");
                        }

                        source.Unindent();
                        source.AppendFront("} else if(" + containerVar + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        if(destinationValue != null && !TypesHelper.IsSameOrSubtype(seqAdd.ExprDst.Type(env), "int", model))
                            source.AppendFront("throw new Exception(\"Can't add non-int key to deque\");\n");
                        else
                        {
                            string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                            if(destinationValue != null)
                                source.AppendFront("int " + destinationValue + " = (int)" + GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                            if(seqAdd.Attribute != null)
                            {
                                if(destinationValue != null)
                                {
                                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                                }
                                else
                                {
                                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                                }
                            }
                            if(destinationValue == null)
                                source.AppendFront(deque + ".Enqueue(" + sourceValue + ");\n");
                            else
                                source.AppendFront(deque + ".EnqueueAt(" + destinationValue + ", " + sourceValue + ");\n");
                        }

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        if(destinationValue != null)
                            source.AppendFront("object " + destinationValue + " = " + GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        if(seqAdd.Attribute != null)
                        {
                            if(seqAdd.ExprDst != null) // must be map
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                            }
                        }
                        if(destinationValue == null)
                            source.AppendFront(dictionary + "[" + sourceValue + "] = null;\n");
                        else
                            source.AppendFront(dictionary + "[" + sourceValue + "] = " + destinationValue + ";\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront(SetResultVar(seqAdd, containerVar));
                    }
                    else if(seqAdd.ContainerType(env).StartsWith("array"))
                    {
                        string array = container;
                        string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
                        string sourceValue = "srcval_" + seqAdd.Id;
                        source.AppendFront(arrayValueType + " " + sourceValue + " = (" + arrayValueType + ")" + GetSequenceExpression(seqAdd.Expr, source) + ";\n");
                        string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
                        if(destinationValue != null)
                            source.AppendFront("int " + destinationValue + " = (int)" + GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                        if(seqAdd.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqAdd.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
                            if(destinationValue != null)
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                            }
                        }
                        if(destinationValue == null)
                            source.AppendFront(array + ".Add(" + sourceValue + ");\n");
                        else
                            source.AppendFront(array + ".Insert(" + destinationValue + ", " + sourceValue + ");\n");
                        source.AppendFront(SetResultVar(seqAdd, container));
                    }
                    else if(seqAdd.ContainerType(env).StartsWith("deque"))
                    {
                        string deque = container;
                        string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
                        string sourceValue = "srcval_" + seqAdd.Id;
                        source.AppendFront(dequeValueType + " " + sourceValue + " = (" + dequeValueType + ")" + GetSequenceExpression(seqAdd.Expr, source) + ";\n");
                        string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
                        if(destinationValue != null)
                            source.AppendFront("int " + destinationValue + " = (int)" + GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                        if(seqAdd.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqAdd.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
                            if(destinationValue != null)
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                            }
                        }
                        if(destinationValue == null)
                            source.AppendFront(deque + ".Enqueue(" + sourceValue + ");\n");
                        else
                            source.AppendFront(deque + ".EnqueueAt(" + destinationValue + ", " + sourceValue + ");\n");
                        source.AppendFront(SetResultVar(seqAdd, container));
                    }
                    else
                    {
                        string dictionary = container;
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
                        string sourceValue = " srcval_" + seqAdd.Id;
                        source.AppendFront(dictSrcType + " " + sourceValue + " = (" + dictSrcType + ")" + GetSequenceExpression(seqAdd.Expr, source) + ";\n");
                        string dictDstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqAdd.ContainerType(env)), model);
                        string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
                        if(destinationValue != null)
                            source.AppendFront(dictDstType + " " + destinationValue + " = (" + dictDstType + ")" + GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                        if(seqAdd.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqAdd.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
                            if(destinationValue != null) // must be map
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                            }
                        }
                        if(destinationValue == null)
                            source.AppendFront(dictionary + "[" + sourceValue + "] = null;\n");
                        else
                            source.AppendFront(dictionary + "[" + sourceValue + "] = " + destinationValue + ";\n");
                        source.AppendFront(SetResultVar(seqAdd, container));
                    }
                    break;
                }

                case SequenceComputationType.ContainerRem:
                {
                    SequenceComputationContainerRem seqDel = (SequenceComputationContainerRem)seqComp;

                    if(seqDel.MethodCall != null)
                    {
                        EmitSequenceComputation(seqDel.MethodCall, source);
                    }
                    string container = GetContainerValue(seqDel);

                    if(seqDel.ContainerType(env) == "")
                    {
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqDel.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
                        }
                        string containerVar = "tmp_eval_once_" + seqDel.Id;
                        source.AppendFront("object " + containerVar + " = " + container + ";\n");
                        string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
                        source.AppendFront("if(" + containerVar + " is IList) {\n");
                        source.Indent();

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        if(sourceValue != null)
                            source.AppendFront("int " + sourceValue + " = (int)" + GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            if(sourceValue != null)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                            }
                        }
                        if(sourceValue == null)
                            source.AppendFront(array + ".RemoveAt(" + array + ".Count - 1);\n");
                        else
                        {
                            if(!TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                                source.AppendFront("throw new Exception(\"Can't remove non-int index from array\");\n");
                            else
                                source.AppendFront(array + ".RemoveAt(" + sourceValue + ");\n");
                        }

                        source.Unindent();
                        source.AppendFront("} else if(" + containerVar + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        if(sourceValue != null)
                            source.AppendFront("int " + sourceValue + " = (int)" + GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            if(sourceValue != null)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                            }
                        }
                        if(sourceValue == null)
                            source.AppendFront(deque + ".Dequeue();\n");
                        else
                        {
                            if(!TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                                source.AppendFront("throw new Exception(\"Can't remove non-int index from deque\");\n");
                            else
                                source.AppendFront(deque + ".DequeueAt(" + sourceValue + ");\n");
                        }

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        if(sourceValue != null)
                            source.AppendFront("object " + sourceValue + " = " + GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("if(GRGEN_LIBGR.TypesHelper.ExtractDst(GRGEN_LIBGR.TypesHelper.AttributeTypeToXgrsType(attrType_" + seqDel.Id + ")) == \"SetValueType\")\n");
                            source.AppendFront("{\n");
                            source.Indent();
                            source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                            source.AppendFront("else\n");
                            source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                            source.AppendFront("else\n");
                            source.AppendFront("{\n");
                            source.Indent();
                            source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                            source.AppendFront("else\n");
                            source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                        }
                        if(sourceValue == null)
                            source.AppendFront("throw new Exception(\""+seqDel.Container.PureName+".rem() only possible on array or deque!\");\n");
                        else
                            source.AppendFront(dictionary + ".Remove(" + sourceValue + ");\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront(SetResultVar(seqDel, containerVar));
                    }
                    else if(seqDel.ContainerType(env).StartsWith("array"))
                    {
                        string array = container;
                        string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
                        if(sourceValue != null)
                            source.AppendFront("int " + sourceValue + " = (int)" + GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqDel.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
                            if(sourceValue != null)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                            }
                        }
                        if(sourceValue == null)
                            source.AppendFront(array + ".RemoveAt(" + array + ".Count - 1);\n");
                        else
                            source.AppendFront(array + ".RemoveAt(" + sourceValue + ");\n");
                        source.AppendFront(SetResultVar(seqDel, container));
                    }
                    else if(seqDel.ContainerType(env).StartsWith("deque"))
                    {
                        string deque = container;
                        string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
                        if(sourceValue != null)
                            source.AppendFront("int " + sourceValue + " = (int)" + GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqDel.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
                            if(sourceValue != null)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                            }
                        }
                        if(sourceValue == null)
                            source.AppendFront(deque + ".Dequeue();\n");
                        else
                            source.AppendFront(deque + ".DequeueAt(" + sourceValue + ");\n");
                        source.AppendFront(SetResultVar(seqDel, container));
                    }
                    else
                    {
                        string dictionary = container;
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqDel.ContainerType(env)), model);
                        string sourceValue = "srcval_" + seqDel.Id;
                        source.AppendFront(dictSrcType + " " + sourceValue + " = (" + dictSrcType + ")" + GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqDel.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
                            if(TypesHelper.ExtractDst(seqDel.ContainerType(env)) == "SetValueType")
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                            }
                            else
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                            }
                        }
                        source.AppendFront(dictionary + ".Remove(" + sourceValue + ");\n");
                        source.AppendFront(SetResultVar(seqDel, container));
                    }
                    break;
                }

                case SequenceComputationType.ContainerClear:
                {
                    SequenceComputationContainerClear seqClear = (SequenceComputationContainerClear)seqComp;

                    if(seqClear.MethodCall != null)
                    {
                        EmitSequenceComputation(seqClear.MethodCall, source);
                    }
                    string container = GetContainerValue(seqClear);

                    if(seqClear.ContainerType(env) == "")
                    {
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqClear.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
                        }
                        string containerVar = "tmp_eval_once_" + seqClear.Id;
                        source.AppendFront("object " + containerVar + " = " + container + ";\n");

                        source.AppendFront("if(" + containerVar + " is IList) {\n");
                        source.Indent();

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("for(int i_" + seqClear.Id + " = " + array + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                            source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                            source.AppendFront("\telse\n");
                            source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                        }
                        source.AppendFront(array + ".Clear();\n");

                        source.Unindent();
                        source.AppendFront("} else if(" + containerVar + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("for(int i_" + seqClear.Id + " = " + deque + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                            source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                            source.AppendFront("\telse\n");
                            source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                        }
                        source.AppendFront(deque + ".Clear();\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("if(GRGEN_LIBGR.TypesHelper.ExtractDst(GRGEN_LIBGR.TypesHelper.AttributeTypeToXgrsType(attrType_" + seqClear.Id + ")) == \"SetValueType\")\n");
                            source.AppendFront("{\n");
                            source.Indent();
                            source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                            source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                            source.AppendFront("\telse\n");
                            source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                            source.AppendFront("else\n");
                            source.AppendFront("{\n");
                            source.Indent();
                            source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                            source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                            source.AppendFront("\telse\n");
                            source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                            source.Unindent();
                            source.AppendFront("}\n");

                        }
                        source.AppendFront(dictionary + ".Clear();\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront(SetResultVar(seqClear, containerVar));
                    }
                    else if(seqClear.ContainerType(env).StartsWith("array"))
                    {
                        string array = container;
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqClear.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
                            source.AppendFront("for(int i_" + seqClear.Id + " = " + array + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                            source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                            source.AppendFront("\telse\n");
                            source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                        }
                        source.AppendFront(array + ".Clear();\n");
                        source.AppendFront(SetResultVar(seqClear, container));
                    }
                    else if(seqClear.ContainerType(env).StartsWith("deque"))
                    {
                        string deque = container;
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqClear.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
                            source.AppendFront("for(int i_" + seqClear.Id + " = " + deque + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                            source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                            source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                            source.AppendFront("\telse\n");
                            source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                        }
                        source.AppendFront(deque + ".Clear();\n");
                        source.AppendFront(SetResultVar(seqClear, container));
                    }
                    else
                    {
                        string dictionary = container;
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqClear.Attribute.SourceVar) + ";\n");
                            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
                            if(TypesHelper.ExtractDst(seqClear.ContainerType(env)) == "SetValueType")
                            {
                                source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                                source.AppendFront("if(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                            }
                            else
                            {
                                source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                                source.AppendFront("if(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                            }
                        }
                        source.AppendFront(dictionary + ".Clear();\n");
                        source.AppendFront(SetResultVar(seqClear, container));
                    }
                    break;
                }

                case SequenceComputationType.VAlloc:
                    source.Append("graph.AllocateVisitedFlag()");
                    break;

                case SequenceComputationType.VFree:
                case SequenceComputationType.VFreeNonReset:
                {
                    SequenceComputationVFree seqVFree = (SequenceComputationVFree)seqComp;
                    if(seqVFree.Reset)
                        source.AppendFront("graph.FreeVisitedFlag((int)" + GetSequenceExpression(seqVFree.VisitedFlagExpression, source) + ");\n");
                    else
                        source.AppendFront("graph.FreeVisitedFlagNonReset((int)" + GetSequenceExpression(seqVFree.VisitedFlagExpression, source) + ");\n");
                    source.AppendFront(SetResultVar(seqVFree, "null"));
                    break;
                }

                case SequenceComputationType.VReset:
                {
                    SequenceComputationVReset seqVReset = (SequenceComputationVReset)seqComp;
                    source.AppendFront("graph.ResetVisitedFlag((int)" + GetSequenceExpression(seqVReset.VisitedFlagExpression, source) + ");\n");
                    source.AppendFront(SetResultVar(seqVReset, "null"));
                    break;
                }

                case SequenceComputationType.Emit:
                {
                    SequenceComputationEmit seqEmit = (SequenceComputationEmit)seqComp;
                    if(!(seqEmit.Expression is SequenceExpressionConstant))
                    {
                        string emitVal = "emitval_" + seqEmit.Id;
                        source.AppendFront("object " + emitVal + " = " + GetSequenceExpression(seqEmit.Expression, source) + ";\n");
                        if(seqEmit.Expression.Type(env) == "" 
                            || seqEmit.Expression.Type(env).StartsWith("set<") || seqEmit.Expression.Type(env).StartsWith("map<")
                            || seqEmit.Expression.Type(env).StartsWith("array<") || seqEmit.Expression.Type(env).StartsWith("deque<"))
                        {
                            source.AppendFront("if(" + emitVal + " is IDictionary)\n");
                            source.AppendFront("\tprocEnv.EmitWriter.Write(GRGEN_LIBGR.ContainerHelper.ToString((IDictionary)" + emitVal + ", graph));\n");
                            source.AppendFront("else if(" + emitVal + " is IList)\n");
                            source.AppendFront("\tprocEnv.EmitWriter.Write(GRGEN_LIBGR.ContainerHelper.ToString((IList)" + emitVal + ", graph));\n");
                            source.AppendFront("else if(" + emitVal + " is GRGEN_LIBGR.IDeque)\n");
                            source.AppendFront("\tprocEnv.EmitWriter.Write(GRGEN_LIBGR.ContainerHelper.ToString((GRGEN_LIBGR.IDeque)" + emitVal + ", graph));\n");
                            source.AppendFront("else\n\t");
                        }
                        source.AppendFront("procEnv.EmitWriter.Write(GRGEN_LIBGR.ContainerHelper.ToString(" + emitVal + ", graph));\n");
                    } else {
                        SequenceExpressionConstant constant = (SequenceExpressionConstant)seqEmit.Expression;
                        if(constant.Constant is string)
                        {
                            String text = (string)constant.Constant;
                            text = text.Replace("\n", "\\n");
                            text = text.Replace("\r", "\\r");
                            text = text.Replace("\t", "\\t");
                            source.AppendFront("procEnv.EmitWriter.Write(\"" + text + "\");\n");
                        }
                        else
                            source.AppendFront("procEnv.EmitWriter.Write(GRGEN_LIBGR.ContainerHelper.ToString(" + GetSequenceExpression(seqEmit.Expression, source) + ", graph));\n");
                    }
                    source.AppendFront(SetResultVar(seqEmit, "null"));
                    break;
                }

                case SequenceComputationType.Record:
                {
                    SequenceComputationRecord seqRec = (SequenceComputationRecord)seqComp;
                    if(!(seqRec.Expression is SequenceExpressionConstant))
                    {
                        string recVal = "recval_" + seqRec.Id;
                        source.AppendFront("object " + recVal + " = " + GetSequenceExpression(seqRec.Expression, source) + ";\n");
                        if(seqRec.Expression.Type(env) == "" 
                            || seqRec.Expression.Type(env).StartsWith("set<") || seqRec.Expression.Type(env).StartsWith("map<")
                            || seqRec.Expression.Type(env).StartsWith("array<") || seqRec.Expression.Type(env).StartsWith("deque<"))
                        {
                            source.AppendFront("if(" + recVal + " is IDictionary)\n");
                            source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.ContainerHelper.ToString((IDictionary)" + recVal + ", graph));\n");
                            source.AppendFront("else if(" + recVal + " is IList)\n");
                            source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.ContainerHelper.ToString((IList)" + recVal + ", graph));\n");
                            source.AppendFront("else if(" + recVal + " is GRGEN_LIBGR.IDeque)\n");
                            source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.ContainerHelper.ToString((GRGEN_LIBGR.IDeque)" + recVal + ", graph));\n");
                            source.AppendFront("else\n\t");
                        }
                        source.AppendFront("procEnv.Recorder.Write(GRGEN_LIBGR.ContainerHelper.ToString(" + recVal + ", graph));\n");
                    } else {
                        SequenceExpressionConstant constant = (SequenceExpressionConstant)seqRec.Expression;
                        if(constant.Constant is string)
                        {
                            String text = (string)constant.Constant;
                            text = text.Replace("\n", "\\n");
                            text = text.Replace("\r", "\\r");
                            text = text.Replace("\t", "\\t");
                            source.AppendFront("procEnv.Recorder.Write(\"" + text + "\");\n");
                        }
                        else
                            source.AppendFront("procEnv.Recorder.Write(GRGEN_LIBGR.ContainerHelper.ToString(" + GetSequenceExpression(seqRec.Expression, source) + ", graph));\n");
                    }
                    source.AppendFront(SetResultVar(seqRec, "null"));
                    break;
                }

                case SequenceComputationType.Export:
                {
                    SequenceComputationExport seqExp = (SequenceComputationExport)seqComp;
                    string expFileName = "expfilename_" + seqExp.Id;
                    source.AppendFront("object " + expFileName + " = " + GetSequenceExpression(seqExp.Name, source) + ";\n");
                    string expArguments = "exparguments_" + seqExp.Id;
                    source.AppendFront("List<string> " + expArguments + " = new List<string>();\n");
                    source.AppendFront(expArguments + ".Add(" + expFileName + ".ToString());\n");
                    string expGraph = "expgraph_" + seqExp.Id;
                    if(seqExp.Graph != null)
                        source.AppendFront("GRGEN_LIBGR.IGraph " + expGraph + " = (GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqExp.Graph, source) + ";\n");
                    else
                        source.AppendFront("GRGEN_LIBGR.IGraph " + expGraph + " = graph;\n");
                    source.AppendFront(expArguments + ".Add(" + expFileName + ".ToString());\n");
                    source.AppendFront("if(" + expGraph + " is GRGEN_LIBGR.INamedGraph)\n");
                    source.AppendFront("\tGRGEN_LIBGR.Porter.Export((GRGEN_LIBGR.INamedGraph)" + expGraph + ", " + expArguments + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tGRGEN_LIBGR.Porter.Export(" + expGraph + ", " + expArguments + ");\n");
                    source.AppendFront(SetResultVar(seqExp, "null"));
                    break;
                }

                case SequenceComputationType.GraphAdd:
                {
                    SequenceComputationGraphAdd seqAdd = (SequenceComputationGraphAdd)seqComp;
                    if(seqAdd.ExprSrc == null)
                    {
                        string typeExpr = GetSequenceExpression(seqAdd.Expr, source);
                        source.Append("GRGEN_LIBGR.GraphHelper.AddNodeOfType(" + typeExpr + ", graph)");
                    }
                    else
                    {
                        string typeExpr = GetSequenceExpression(seqAdd.Expr, source);
                        string srcExpr = GetSequenceExpression(seqAdd.ExprSrc, source);
                        string tgtExpr = GetSequenceExpression(seqAdd.ExprDst, source);
                        source.Append("GRGEN_LIBGR.GraphHelper.AddEdgeOfType(" + typeExpr + ", (GRGEN_LIBGR.INode)" + srcExpr + ", (GRGEN_LIBGR.INode)" + tgtExpr + ", graph)");
                    }
                    break;
                }
                
                case SequenceComputationType.GraphRem:
                {
                    SequenceComputationGraphRem seqRem = (SequenceComputationGraphRem)seqComp;
                    string remVal = "remval_" + seqRem.Id;
                    source.AppendFront("object " + remVal + " = " + GetSequenceExpression(seqRem.Expr, source) + ";\n");
                    source.AppendFront("if(" + remVal + " is GRGEN_LIBGR.IEdge)\n");
                    source.AppendFront("\tgraph.Remove((GRGEN_LIBGR.IEdge)" + remVal + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\t{graph.RemoveEdges((GRGEN_LIBGR.INode)" + remVal + "); graph.Remove((GRGEN_LIBGR.INode)" + remVal + ");}\n");
                    source.AppendFront(SetResultVar(seqRem, "null"));
                    break;
                }

                case SequenceComputationType.GraphClear:
                {
                    SequenceComputationGraphClear seqClr = (SequenceComputationGraphClear)seqComp;
                    source.AppendFront("graph.Clear();\n");
                    source.AppendFront(SetResultVar(seqClr, "null"));
                    break;
                }

                case SequenceComputationType.GraphRetype:
                {
                    SequenceComputationGraphRetype seqRetype = (SequenceComputationGraphRetype)seqComp;
                    string typeExpr = GetSequenceExpression(seqRetype.TypeExpr, source);
                    string elemExpr = GetSequenceExpression(seqRetype.ElemExpr, source);
                    source.Append("GRGEN_LIBGR.GraphHelper.RetypeGraphElement((GRGEN_LIBGR.IGraphElement)" + elemExpr + ", "  + typeExpr + ", graph)");
                    break;
                }

                case SequenceComputationType.InsertInduced:
                {
                    SequenceComputationInsertInduced seqInsInd = (SequenceComputationInsertInduced)seqComp;
                    source.Append("GRGEN_LIBGR.GraphHelper.InsertInduced((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInsInd.NodeSet, source) + ", (GRGEN_LIBGR.INode)" + GetSequenceExpression(seqInsInd.RootNode, source) + ", graph)");
                    break;
                }

                case SequenceComputationType.InsertDefined:
                {
                    SequenceComputationInsertDefined seqInsDef = (SequenceComputationInsertDefined)seqComp;
                    source.Append("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    break;
                }

                case SequenceComputationType.Expression:
                {
                    SequenceExpression seqExpr = (SequenceExpression)seqComp;
                    source.AppendFront(SetResultVar(seqExpr, GetSequenceExpression(seqExpr, source)));
                    break;
                }

                case SequenceComputationType.BuiltinProcedureCall:
                {
                    SequenceComputationBuiltinProcedureCall seqCall = (SequenceComputationBuiltinProcedureCall)seqComp;
                    SourceBuilder sb = new SourceBuilder();
                    EmitSequenceComputation(seqCall.BuiltinProcedure, sb);
                    if(seqCall.ReturnVars.Count > 0)
                    {
                        source.AppendFront(SetVar(seqCall.ReturnVars[0], sb.ToString()));
                        source.AppendFront(SetResultVar(seqCall, GetVar(seqCall.ReturnVars[0])));
                    }
                    else
                    {
                        source.AppendFront(sb.ToString() + ";\n");
                        source.AppendFront(SetResultVar(seqCall, "null"));
                    }
                    break;
                }

                case SequenceComputationType.ProcedureCall:
                {
                    SequenceComputationProcedureCall seqCall = (SequenceComputationProcedureCall)seqComp;

                    String returnParameterDeclarations;
                    String returnArguments;
                    String returnAssignments;
                    BuildReturnParameters(seqCall.ParamBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

                    if(returnParameterDeclarations.Length != 0)
                        source.AppendFront(returnParameterDeclarations + "\n");

                    source.AppendFront("Computations.");
                    source.Append(seqCall.ParamBindings.Name);
                    source.Append("(procEnv, graph");
                    source.Append(BuildParameters(seqCall.ParamBindings));
                    source.Append(returnArguments);
                    source.Append(");\n");

                    if(returnAssignments.Length != 0)
                        source.AppendFront(returnAssignments + "\n");

                    source.AppendFront(SetResultVar(seqCall, "null"));
                    break;
                }

				default:
					throw new Exception("Unknown sequence computation type: " + seqComp.SequenceComputationType);
			}
		}

   		void EmitAssignment(AssignmentTarget tgt, string sourceValueComputation, SourceBuilder source)
		{
			switch(tgt.AssignmentTargetType)
			{
                case AssignmentTargetType.YieldingToVar:
                {
                    AssignmentTargetYieldingVar tgtYield = (AssignmentTargetYieldingVar)tgt;
                    source.AppendFront(SetVar(tgtYield.DestVar, sourceValueComputation));
                    source.AppendFront(SetResultVar(tgtYield, GetVar(tgtYield.DestVar)));
                    break;
                }

                case AssignmentTargetType.Visited:
                {
                    AssignmentTargetVisited tgtVisitedFlag = (AssignmentTargetVisited)tgt;
                    source.AppendFront("bool visval_"+tgtVisitedFlag.Id+" = (bool)"+sourceValueComputation+";\n");
                    source.AppendFront("graph.SetVisited((GRGEN_LIBGR.IGraphElement)"+GetVar(tgtVisitedFlag.GraphElementVar)
                        + ", (int)" + GetSequenceExpression(tgtVisitedFlag.VisitedFlagExpression, source) + ", visval_" + tgtVisitedFlag.Id + ");\n");
                    source.AppendFront(SetResultVar(tgtVisitedFlag, "visval_"+tgtVisitedFlag.Id));
                    break;
                }

                case AssignmentTargetType.IndexedVar:
                {
                    AssignmentTargetIndexedVar tgtIndexedVar = (AssignmentTargetIndexedVar)tgt;
                    string container = "container_" + tgtIndexedVar.Id;
                    source.AppendFront("object " + container + " = " + GetVar(tgtIndexedVar.DestVar) + ";\n");
                    string key = "key_" + tgtIndexedVar.Id;
                    source.AppendFront("object " + key + " = " + GetSequenceExpression(tgtIndexedVar.KeyExpression, source) + ";\n");
                    source.AppendFront(SetResultVar(tgtIndexedVar, container)); // container is a reference, so we can assign it already here before the changes

                    if(tgtIndexedVar.DestVar.Type == "")
                    {
                        source.AppendFront("if(" + container + " is IList) {\n");
                        source.Indent();

                        string array = "((System.Collections.IList)" + container + ")";
                        if(!TypesHelper.IsSameOrSubtype(tgtIndexedVar.KeyExpression.Type(env), "int", model))
                        {
                            source.AppendFront("if(true) {\n");
                            source.Indent();
                            source.AppendFront("throw new Exception(\"Can't access non-int index in array\");\n");
                        }
                        else
                        {
                            source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                        }
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else if(" + container + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        string deque = "((GRGEN_LIBGR.IDeque)" + container + ")";
                        if(!TypesHelper.IsSameOrSubtype(tgtIndexedVar.KeyExpression.Type(env), "int", model))
                        {
                            source.AppendFront("if(true) {\n");
                            source.Indent();
                            source.AppendFront("throw new Exception(\"Can't access non-int index in deque\");\n");
                        }
                        else
                        {
                            source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                        }
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + container + ")";
                        source.AppendFront("if(" + dictionary + ".Contains(" + key + ")) {\n");
                        source.Indent();
                        source.AppendFront(dictionary + "[" + key + "] = " + sourceValueComputation + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(tgtIndexedVar.DestVar.Type.StartsWith("array"))
                    {
                        string array = GetVar(tgtIndexedVar.DestVar);
                        source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                        source.Indent();
                        source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(tgtIndexedVar.DestVar.Type.StartsWith("deque"))
                    {
                        string deque = GetVar(tgtIndexedVar.DestVar);
                        source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                        source.Indent();
                        source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        string dictionary = GetVar(tgtIndexedVar.DestVar);
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(tgtIndexedVar.DestVar.Type), model);
                        source.AppendFront("if(" + dictionary + ".ContainsKey((" + dictSrcType + ")" + key + ")) {\n");
                        source.Indent();
                        source.AppendFront(dictionary + "[(" + dictSrcType + ")" + key + "] = " + sourceValueComputation + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    break;
                }

                case AssignmentTargetType.Var:
				{
                    AssignmentTargetVar tgtVar = (AssignmentTargetVar)tgt;
                    source.AppendFront(SetVar(tgtVar.DestVar, sourceValueComputation));
                    source.AppendFront(SetResultVar(tgtVar, GetVar(tgtVar.DestVar)));
					break;
				}

                case AssignmentTargetType.Attribute:
                {
                    AssignmentTargetAttribute tgtAttr = (AssignmentTargetAttribute)tgt;
                    source.AppendFront("object value_" + tgtAttr.Id + " = " + sourceValueComputation + ";\n");
                    source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + tgtAttr.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(tgtAttr.DestVar) + ";\n");
                    source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + tgtAttr.Id + ";\n");
                    source.AppendFront("value_" + tgtAttr.Id + " = GRGEN_LIBGR.ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(elem_" + tgtAttr.Id + ", \"" + tgtAttr.AttributeName + "\", value_" + tgtAttr.Id + ", out attrType_" + tgtAttr.Id + ");\n");
                    source.AppendFront("if(elem_" + tgtAttr.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ", GRGEN_LIBGR.AttributeChangeType.Assign, value_" + tgtAttr.Id + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ", GRGEN_LIBGR.AttributeChangeType.Assign, value_" + tgtAttr.Id + ", null);\n");
                    source.AppendFront("elem_" + tgtAttr.Id + ".SetAttribute(\"" + tgtAttr.AttributeName + "\", value_" + tgtAttr.Id + ");\n");
                    source.AppendFront(SetResultVar(tgtAttr, "value_" + tgtAttr.Id));
                    break;
                }

                case AssignmentTargetType.AttributeIndexed:
                {
                    AssignmentTargetAttributeIndexed tgtAttrIndexedVar = (AssignmentTargetAttributeIndexed)tgt;
                    string value = "value_" + tgtAttrIndexedVar.Id;
                    source.AppendFront("object " + value + " = " + sourceValueComputation + ";\n");
                    source.AppendFront(SetResultVar(tgtAttrIndexedVar, "value_" + tgtAttrIndexedVar.Id));
                    source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + tgtAttrIndexedVar.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(tgtAttrIndexedVar.DestVar) + ";\n");
                    string container = "container_" + tgtAttrIndexedVar.Id;
                    source.AppendFront("object " + container + " = elem_" + tgtAttrIndexedVar.Id + ".GetAttribute(\"" + tgtAttrIndexedVar.AttributeName + "\");\n");
                    string key = "key_" + tgtAttrIndexedVar.Id;
                    source.AppendFront("object " + key + " = " + GetSequenceExpression(tgtAttrIndexedVar.KeyExpression, source) + ";\n");

                    source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + tgtAttrIndexedVar.Id + " = elem_" + tgtAttrIndexedVar.Id + ".Type.GetAttributeType(\"" + tgtAttrIndexedVar.AttributeName + "\");\n");
                    source.AppendFront("if(elem_" + tgtAttrIndexedVar.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ", GRGEN_LIBGR.AttributeChangeType.AssignElement, " + value + ", " + key + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ", GRGEN_LIBGR.AttributeChangeType.AssignElement, " + value + ", " + key + ");\n");

                    if(tgtAttrIndexedVar.DestVar.Type == "")
                    {
                        source.AppendFront("if(" + container + " is IList) {\n");
                        source.Indent();

                        string array = "((System.Collections.IList)" + container + ")";
                        if(!TypesHelper.IsSameOrSubtype(tgtAttrIndexedVar.KeyExpression.Type(env), "int", model))
                        {
                            source.AppendFront("if(true) {\n");
                            source.Indent();
                            source.AppendFront("throw new Exception(\"Can't access non-int index in array\");\n");
                        }
                        else
                        {
                            source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(array + "[(int)" + key + "] = " + value + ";\n");
                        }
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else if(" + container + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        string deque = "((GRGEN_LIBGR.IDeque)" + container + ")";
                        if(!TypesHelper.IsSameOrSubtype(tgtAttrIndexedVar.KeyExpression.Type(env), "int", model))
                        {
                            source.AppendFront("if(true) {\n");
                            source.Indent();
                            source.AppendFront("throw new Exception(\"Can't access non-int index in deque\");\n");
                        }
                        else
                        {
                            source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(deque + "[(int)" + key + "] = " + value + ";\n");
                        }
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + container + ")";
                        source.AppendFront("if(" + dictionary + ".Contains(" + key + ")) {\n");
                        source.Indent();
                        source.AppendFront(dictionary + "[" + key + "] = " + value + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(tgtAttrIndexedVar.DestVar.Type, env.Model);
                        AttributeType attributeType = nodeOrEdgeType.GetAttributeType(tgtAttrIndexedVar.AttributeName);
                        string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);

                        if(ContainerType.StartsWith("array"))
                        {
                            string array = GetVar(tgtAttrIndexedVar.DestVar);
                            source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                        }
                        else if(ContainerType.StartsWith("deque"))
                        {
                            string deque = GetVar(tgtAttrIndexedVar.DestVar);
                            source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                        }
                        else
                        {
                            string dictionary = GetVar(tgtAttrIndexedVar.DestVar);
                            string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(tgtAttrIndexedVar.DestVar.Type), model);
                            source.AppendFront("if(" + dictionary + ".ContainsKey((" + dictSrcType + ")" + key + ")) {\n");
                            source.Indent();
                            source.AppendFront(dictionary + "[(" + dictSrcType + ")" + key + "] = " + value + ";\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                        }
                    }
                    break;
                }

				default:
					throw new Exception("Unknown assignment target type: " + tgt.AssignmentTargetType);
			}
		}

        private String BuildParameters(InvocationParameterBindings paramBindings)
        {
            String parameters = "";
            for (int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                if (paramBindings.ArgumentExpressions[i] != null)
                {
                    String typeName;
                    if(rulesToInputTypes.ContainsKey(paramBindings.Name))
                        typeName = rulesToInputTypes[paramBindings.Name][i];
                    else if(sequencesToInputTypes.ContainsKey(paramBindings.Name))
                        typeName = sequencesToInputTypes[paramBindings.Name][i];
                    else if(proceduresToInputTypes.ContainsKey(paramBindings.Name))
                        typeName = proceduresToInputTypes[paramBindings.Name][i];
                    else
                        typeName = functionsToInputTypes[paramBindings.Name][i];
                    String cast = "(" + TypesHelper.XgrsTypeToCSharpType(typeName, model) + ")";
                    parameters += ", " + cast + GetSequenceExpression(paramBindings.ArgumentExpressions[i], null);
                }
                else
                {
                    // the sequence parser always emits all argument expressions, for interpreted and compiled
                    throw new Exception("Internal error: missing argument expressions");
                }
            }
            return parameters;
        }

        private void BuildOutParameters(SequenceInvocationParameterBindings paramBindings, out String outParameterDeclarations, out String outArguments, out String outAssignments)
        {
            outParameterDeclarations = "";
            outArguments = "";
            outAssignments = "";
            for(int i = 0; i < sequencesToOutputTypes[paramBindings.Name].Count; i++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0)
                    varName = tmpVarCtr.ToString() + paramBindings.ReturnVars[i].PureName;
                else
                    varName = tmpVarCtr.ToString();
                ++tmpVarCtr;
                String typeName = sequencesToOutputTypes[paramBindings.Name][i];
                outParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName
                    + " = " + TypesHelper.DefaultValueString(typeName, model) + ";";
                outArguments += ", ref tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    outAssignments += SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName);
            }
        }

        private void BuildReturnParameters(RuleInvocationParameterBindings paramBindings, out String returnParameterDeclarations, out String returnArguments, out String returnAssignments)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            returnParameterDeclarations = "";
            returnArguments = "";
            returnAssignments = "";
            for(int i = 0; i < rulesToOutputTypes[paramBindings.Name].Count; i++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0)
                    varName = tmpVarCtr.ToString() + paramBindings.ReturnVars[i].PureName;
                else
                    varName = tmpVarCtr.ToString();
                ++tmpVarCtr;
                String typeName = rulesToOutputTypes[paramBindings.Name][i];
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName + "; ";
                returnArguments += ", out tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    returnAssignments += SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName);
            }
        }

        private void BuildReturnParameters(ComputationInvocationParameterBindings paramBindings, out String returnParameterDeclarations, out String returnArguments, out String returnAssignments)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            returnParameterDeclarations = "";
            returnArguments = "";
            returnAssignments = "";
            for(int i = 0; i < proceduresToOutputTypes[paramBindings.Name].Count; i++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0)
                    varName = tmpVarCtr.ToString() + paramBindings.ReturnVars[i].PureName;
                else
                    varName = tmpVarCtr.ToString();
                ++tmpVarCtr;
                String typeName = proceduresToOutputTypes[paramBindings.Name][i];
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName + "; ";
                returnArguments += ", out tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    returnAssignments += SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName);
            }
        }


        string GetContainerValue(SequenceComputationContainer container)
        {
            if(container.MethodCall != null)
                return GetResultVar(container.MethodCall);
            else if(container.Container != null)
                return GetVar(container.Container);
            else
                return "((GRGEN_LIBGR.IGraphElement)" + GetVar(container.Attribute.SourceVar) + ")" + ".GetAttribute(\"" + container.Attribute.AttributeName + "\")";
        }

        // source is needed for a method call chain or expressions that require temporary variables, 
        // to emit the state changing computation methods or the temporary variable declarations (not the assignement, needs to be computed from inside the expression)
        // before returning the final expression method call ready to be emitted
        private string GetSequenceExpression(SequenceExpression expr, SourceBuilder source)
        {
            switch(expr.SequenceExpressionType)
            {
                case SequenceExpressionType.Conditional:
                {
                    SequenceExpressionConditional seqCond = (SequenceExpressionConditional)expr;
                    return "( (bool)" + GetSequenceExpression(seqCond.Condition, source)
                        + " ? (object)" + GetSequenceExpression(seqCond.TrueCase, source)
                        + " : (object)" + GetSequenceExpression(seqCond.FalseCase, source) + " )";
                }

                case SequenceExpressionType.LazyOr:
                {
                    SequenceExpressionLazyOr seq = (SequenceExpressionLazyOr)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " || (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }

                case SequenceExpressionType.LazyAnd:
                {
                    SequenceExpressionLazyAnd seq = (SequenceExpressionLazyAnd)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " && (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }
                
                case SequenceExpressionType.StrictOr:
                {
                    SequenceExpressionStrictOr seq = (SequenceExpressionStrictOr)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " | (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }
                
                case SequenceExpressionType.StrictXor:
                {
                    SequenceExpressionStrictXor seq = (SequenceExpressionStrictXor)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " ^ (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }
                
                case SequenceExpressionType.StrictAnd:
                {
                    SequenceExpressionStrictAnd seq = (SequenceExpressionStrictAnd)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " & (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }

                case SequenceExpressionType.Equal:
                {
                    SequenceExpressionEqual seq = (SequenceExpressionEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.EqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.EqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                            + leftType + ", " + rightType + ")";
                }

                case SequenceExpressionType.StructuralEqual:
                {
                    SequenceExpressionStructuralEqual seq = (SequenceExpressionStructuralEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    return SequenceExpressionHelper.StructuralEqualStatic(leftExpr, rightExpr);
                }

                case SequenceExpressionType.NotEqual:
                {
                    SequenceExpressionNotEqual seq = (SequenceExpressionNotEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.NotEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.NotEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.NotEqual, " + leftType + ", " + rightType + ", graph.Model), "
                            + leftType + ", " + rightType + ")";
                }

                case SequenceExpressionType.Lower:
                {
                    SequenceExpressionLower seq = (SequenceExpressionLower)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.LowerStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.LowerObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Lower, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ")";
                }

                case SequenceExpressionType.Greater:
                {
                    SequenceExpressionGreater seq = (SequenceExpressionGreater)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.GreaterStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.GreaterObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Greater, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ")";
                }

                case SequenceExpressionType.LowerEqual:
                {
                    SequenceExpressionLowerEqual seq = (SequenceExpressionLowerEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.LowerEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.LowerEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.LowerEqual, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ")";
                }

                case SequenceExpressionType.GreaterEqual:
                {
                    SequenceExpressionGreaterEqual seq = (SequenceExpressionGreaterEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.GreaterEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.GreaterEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.GreaterEqual, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ")";
                }

                case SequenceExpressionType.Plus:
                {
                    SequenceExpressionPlus seq = (SequenceExpressionPlus)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.PlusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.PlusObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Plus, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Minus:
                {
                    SequenceExpressionMinus seq = (SequenceExpressionMinus)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.MinusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.MinusObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Minus, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Not:
                {
                    SequenceExpressionNot seqNot = (SequenceExpressionNot)expr;
                    return "!" + "((bool)" + GetSequenceExpression(seqNot.Operand, source) + ")";
                }

                case SequenceExpressionType.Cast:
                {
                    SequenceExpressionCast seqCast = (SequenceExpressionCast)expr;
                    string targetType = "UNSUPPORTED TYPE CAST";
                    if(seqCast.TargetType is NodeType)
                        targetType = ((NodeType)seqCast.TargetType).NodeInterfaceName;
                    if(seqCast.TargetType is EdgeType)
                        targetType = ((EdgeType)seqCast.TargetType).EdgeInterfaceName;
                    // TODO: handle the non-node and non-edge-types, too
                    return "((" + targetType + ")" + GetSequenceExpression(seqCast.Operand, source) + ")";
                }

                case SequenceExpressionType.Def:
                {
                    SequenceExpressionDef seqDef = (SequenceExpressionDef)expr;
                    String condition = "(";
                    bool isFirst = true;
                    foreach(SequenceExpression var in seqDef.DefVars)
                    {
                        if(isFirst) isFirst = false;
                        else condition += " && ";
                        condition += GetSequenceExpression(var, source) + "!=null";
                    }
                    condition += ")";
                    return condition;
                }

                case SequenceExpressionType.InContainer:
                {
                    SequenceExpressionInContainer seqIn = (SequenceExpressionInContainer)expr;

                    string container;
                    string ContainerType;
                    if(seqIn.ContainerExpr is SequenceExpressionAttributeAccess)
                    {
                        SequenceExpressionAttributeAccess seqInAttribute = (SequenceExpressionAttributeAccess)(seqIn.ContainerExpr);
                        string element = "((GRGEN_LIBGR.IGraphElement)" + GetVar(seqInAttribute.SourceVar) + ")";
                        container = element + ".GetAttribute(\"" + seqInAttribute.AttributeName + "\")";
                        ContainerType = seqInAttribute.Type(env);
                    }
                    else
                    {
                        container = GetSequenceExpression(seqIn.ContainerExpr, source);
                        ContainerType = seqIn.ContainerExpr.Type(env);
                    }

                    if(ContainerType == "")
                    {
                        SourceBuilder sb = new SourceBuilder();

                        string sourceExpr = GetSequenceExpression(seqIn.Expr, source);
                        string containerVar = "tmp_eval_once_" + seqIn.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        sb.AppendFront(array + ".Contains(" + sourceExpr + ")");

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        sb.AppendFront(deque + ".Contains(" + sourceExpr + ")");

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + ".Contains(" + sourceExpr + ")");

                        sb.AppendFront(")");

                        return sb.ToString();
                    }
                    else if(ContainerType.StartsWith("array"))
                    {
                        string array = container;
                        string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
                        string sourceExpr = "((" + arrayValueType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                        return array + ".Contains(" + sourceExpr + ")";
                    }
                    else if(ContainerType.StartsWith("deque"))
                    {
                        string deque = container;
                        string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
                        string sourceExpr = "((" + dequeValueType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                        return deque + ".Contains(" + sourceExpr + ")";
                    }
                    else
                    {
                        string dictionary = container;
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
                        string sourceExpr = "((" + dictSrcType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                        return dictionary + ".ContainsKey(" + sourceExpr + ")";
                    }
                }

                case SequenceExpressionType.IsVisited:
                {
                    SequenceExpressionIsVisited seqIsVisited = (SequenceExpressionIsVisited)expr;
                    return "graph.IsVisited("
                        + "(GRGEN_LIBGR.IGraphElement)" + GetVar(seqIsVisited.GraphElementVar)
                        + ", (int)" + GetSequenceExpression(seqIsVisited.VisitedFlagExpr, source)
                        + ")";
                }

                case SequenceExpressionType.Nodes:
                {
                    SequenceExpressionNodes seqNodes = (SequenceExpressionNodes)expr;
                    string nodeType = ExtractNodeType(source, seqNodes.NodeType);
                    return "GRGEN_LIBGR.GraphHelper.Nodes(graph, (GRGEN_LIBGR.NodeType)" + nodeType + ")";
                }

                case SequenceExpressionType.Edges:
                {
                    SequenceExpressionEdges seqEdges = (SequenceExpressionEdges)expr;
                    string edgeType = ExtractEdgeType(source, seqEdges.EdgeType);
                    return "GRGEN_LIBGR.GraphHelper.Edges(graph, (GRGEN_LIBGR.EdgeType)" + edgeType + ")";
                }
                
                case SequenceExpressionType.AdjacentNodes:
                case SequenceExpressionType.AdjacentNodesViaIncoming:
                case SequenceExpressionType.AdjacentNodesViaOutgoing:
                case SequenceExpressionType.IncidentEdges:
                case SequenceExpressionType.IncomingEdges:
                case SequenceExpressionType.OutgoingEdges:
                {
                    SequenceExpressionAdjacentIncident seqAdjInc = (SequenceExpressionAdjacentIncident)expr;
                    string sourceNode = GetSequenceExpression(seqAdjInc.SourceNode, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqAdjInc.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqAdjInc.OppositeNodeType);
                    string function;
                    switch(seqAdjInc.SequenceExpressionType)
                    {
                        case SequenceExpressionType.AdjacentNodes:
                            function = "Adjacent"; break;
                        case SequenceExpressionType.AdjacentNodesViaIncoming:
                            function = "AdjacentIncoming"; break;
                        case SequenceExpressionType.AdjacentNodesViaOutgoing:
                            function = "AdjacentOutgoing"; break;
                        case SequenceExpressionType.IncidentEdges:
                            function = "Incident"; break;
                        case SequenceExpressionType.IncomingEdges:
                            function = "Incoming"; break;
                        case SequenceExpressionType.OutgoingEdges:
                            function = "Outgoing"; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                        + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + ")";
                }

                case SequenceExpressionType.ReachableNodes:
                case SequenceExpressionType.ReachableNodesViaIncoming:
                case SequenceExpressionType.ReachableNodesViaOutgoing:
                case SequenceExpressionType.ReachableEdges:
                case SequenceExpressionType.ReachableEdgesViaIncoming:
                case SequenceExpressionType.ReachableEdgesViaOutgoing:
                {
                    SequenceExpressionReachable seqReach = (SequenceExpressionReachable)expr;
                    string sourceNode = GetSequenceExpression(seqReach.SourceNode, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqReach.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqReach.OppositeNodeType);
                    string function;
                    switch(seqReach.SequenceExpressionType)
                    {
                        case SequenceExpressionType.ReachableNodes:
                            function = "Reachable"; break;
                        case SequenceExpressionType.ReachableNodesViaIncoming:
                            function = "ReachableIncoming"; break;
                        case SequenceExpressionType.ReachableNodesViaOutgoing:
                            function = "ReachableOutgoing"; break;
                        case SequenceExpressionType.ReachableEdges:
                            function = "ReachableEdges"; break;
                        case SequenceExpressionType.ReachableEdgesViaIncoming:
                            function = "ReachableEdgesIncoming"; break;
                        case SequenceExpressionType.ReachableEdgesViaOutgoing:
                            function = "ReachableEdgesOutgoing"; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    if(seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodes
                        || seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodesViaIncoming
                        || seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodesViaOutgoing)
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + ")";
                    }
                    else // SequenceExpressionType.ReachableEdges || SequenceExpressionType.ReachableEdgesViaIncoming || SequenceExpressionType.ReachableEdgesViaOutgoing
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + ")";
                    }
                }

                case SequenceExpressionType.IsAdjacentNodes:
                case SequenceExpressionType.IsAdjacentNodesViaIncoming:
                case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
                case SequenceExpressionType.IsIncidentEdges:
                case SequenceExpressionType.IsIncomingEdges:
                case SequenceExpressionType.IsOutgoingEdges:
                {
                    SequenceExpressionIsAdjacentIncident seqIsAdjInc = (SequenceExpressionIsAdjacentIncident)expr;
                    string sourceNode = GetSequenceExpression(seqIsAdjInc.SourceNode, source);
                    string endElement = GetSequenceExpression(seqIsAdjInc.EndElement, source);
                    string endElementType;
                    string incidentEdgeType = ExtractEdgeType(source, seqIsAdjInc.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqIsAdjInc.OppositeNodeType);
                    string function;
                    switch(seqIsAdjInc.SequenceExpressionType)
                    {
                        case SequenceExpressionType.IsAdjacentNodes:
                            function = "IsAdjacent";
                            endElementType = "(GRGEN_LIBGR.INode)";
                            break;
                        case SequenceExpressionType.IsAdjacentNodesViaIncoming:
                            function = "IsAdjacentIncoming";
                            endElementType = "(GRGEN_LIBGR.INode)";
                            break;
                        case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
                            function = "IsAdjacentOutgoing";
                            endElementType = "(GRGEN_LIBGR.INode)";
                            break;
                        case SequenceExpressionType.IsIncidentEdges:
                            function = "IsIncident";
                            endElementType = "(GRGEN_LIBGR.IEdge)";
                            break;
                        case SequenceExpressionType.IsIncomingEdges:
                            function = "IsIncoming";
                            endElementType = "(GRGEN_LIBGR.IEdge)";
                            break;
                        case SequenceExpressionType.IsOutgoingEdges:
                            function = "IsOutgoing";
                            endElementType = "(GRGEN_LIBGR.IEdge)";
                            break;
                        default:
                            function = "INTERNAL ERROR";
                            endElementType = "INTERNAL ERROR";
                            break;
                    }
                    return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", " + endElementType + " " + endElement
                        + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + ")";
                }

                case SequenceExpressionType.IsReachableNodes:
                case SequenceExpressionType.IsReachableNodesViaIncoming:
                case SequenceExpressionType.IsReachableNodesViaOutgoing:
                case SequenceExpressionType.IsReachableEdges:
                case SequenceExpressionType.IsReachableEdgesViaIncoming:
                case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                {
                    SequenceExpressionIsReachable seqIsReach = (SequenceExpressionIsReachable)expr;
                    string sourceNode = GetSequenceExpression(seqIsReach.SourceNode, source);
                    string endElement = GetSequenceExpression(seqIsReach.EndElement, source);
                    string endElementType;
                    string incidentEdgeType = ExtractEdgeType(source, seqIsReach.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqIsReach.OppositeNodeType);
                    string function;
                    switch(seqIsReach.SequenceExpressionType)
                    {
                        case SequenceExpressionType.IsReachableNodes:
                            function = "IsReachable";
                            endElementType = "(GRGEN_LIBGR.INode)";
                            break;
                        case SequenceExpressionType.IsReachableNodesViaIncoming:
                            function = "IsReachableIncoming";
                            endElementType = "(GRGEN_LIBGR.INode)";
                            break;
                        case SequenceExpressionType.IsReachableNodesViaOutgoing:
                            function = "IsReachableOutgoing";
                            endElementType = "(GRGEN_LIBGR.INode)";
                            break;
                        case SequenceExpressionType.IsReachableEdges:
                            function = "IsReachableEdges";
                            endElementType = "(GRGEN_LIBGR.IEdge)";
                            break;
                        case SequenceExpressionType.IsReachableEdgesViaIncoming:
                            function = "IsReachableEdgesIncoming";
                            endElementType = "(GRGEN_LIBGR.IEdge)";
                            break;
                        case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                            function = "IsReachableEdgesOutgoing";
                            endElementType = "(GRGEN_LIBGR.IEdge)";
                            break;
                        default:
                            function = "INTERNAL ERROR";
                            endElementType = "INTERNAL ERROR";
                            break;
                    }
                    return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", " + endElementType + " " + endElement
                        + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + ")";
                }

                case SequenceExpressionType.InducedSubgraph:
                {
                    SequenceExpressionInducedSubgraph seqInduced = (SequenceExpressionInducedSubgraph)expr;
                    return "GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInduced.NodeSet, source) + ", graph)";
                }

                case SequenceExpressionType.DefinedSubgraph:
                {
                    SequenceExpressionDefinedSubgraph seqDefined = (SequenceExpressionDefinedSubgraph)expr;
                    return "GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                }

                case SequenceExpressionType.Canonize:
                {
                    SequenceExpressionCanonize seqCanonize = (SequenceExpressionCanonize)expr;
                    return "((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqCanonize.Graph, source) + ").Canonize()";
                }

                case SequenceExpressionType.Random:
                {
                    SequenceExpressionRandom seqRandom = (SequenceExpressionRandom)expr;
                    if(seqRandom.UpperBound != null)
                        return "GRGEN_LIBGR.Sequence.randomGenerator.Next((int)" + GetSequenceExpression(seqRandom.UpperBound, source) + ")";
                    else
                        return "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()";
                }

                case SequenceExpressionType.ContainerSize:
                {
                    SequenceExpressionContainerSize seqContainerSize = (SequenceExpressionContainerSize)expr;

                    if(seqContainerSize.MethodCall != null)
                    {
                        EmitSequenceComputation(seqContainerSize.MethodCall, source);
                    }

                    string container = GetContainerValue(seqContainerSize, source);

                    if(seqContainerSize.ContainerType(env) == "")
                    {
                        SourceBuilder sb = new SourceBuilder();

                        string containerVar = "tmp_eval_once_" + seqContainerSize.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        sb.AppendFront(array + ".Count");

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        sb.AppendFront(deque + ".Count");

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + ".Count");

                        sb.AppendFront(")");

                        return sb.ToString();
                    }
                    else if(seqContainerSize.ContainerType(env).StartsWith("array"))
                    {
                        string array = container;
                        return array + ".Count";
                    }
                    else if(seqContainerSize.ContainerType(env).StartsWith("deque"))
                    {
                        string deque = container;
                        return deque + ".Count";
                    }
                    else
                    {
                        string dictionary = container;
                        return dictionary + ".Count";
                    }
                }

                case SequenceExpressionType.ContainerEmpty:
                {
                    SequenceExpressionContainerEmpty seqContainerEmpty = (SequenceExpressionContainerEmpty)expr;
                    
                    if(seqContainerEmpty.MethodCall != null)
                    {
                        EmitSequenceComputation(seqContainerEmpty.MethodCall, source);
                    }
                    string container = GetContainerValue(seqContainerEmpty, source);

                    if(seqContainerEmpty.ContainerType(env) == "")
                    {
                        SourceBuilder sb = new SourceBuilder();

                        string containerVar = "tmp_eval_once_" + seqContainerEmpty.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        sb.AppendFront(array + ".Count==0");

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        sb.AppendFront(deque + ".Count==0");

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + ".Count==0");

                        sb.AppendFront(")");

                        return sb.ToString();
                    }
                    else if(seqContainerEmpty.ContainerType(env).StartsWith("array"))
                    {
                        string array = container;
                        return "(" + array + ".Count==0)";
                    }
                    else if(seqContainerEmpty.ContainerType(env).StartsWith("deque"))
                    {
                        string deque = container;
                        return "(" + deque + ".Count==0)";
                    }
                    else
                    {
                        string dictionary = container;
                        return "(" + dictionary + ".Count==0)";
                    }
                }

                case SequenceExpressionType.ContainerAccess:
                {
                    SequenceExpressionContainerAccess seqContainerAccess = (SequenceExpressionContainerAccess)expr; // todo: dst type unknownTypesHelper.ExtractSrc(seqMapAccessToVar.Setmap.Type)
                    string container;
                    string ContainerType;
                    if(seqContainerAccess.ContainerExpr is SequenceExpressionAttributeAccess)
                    {
                        SequenceExpressionAttributeAccess seqContainerAttribute = (SequenceExpressionAttributeAccess)(seqContainerAccess.ContainerExpr);
                        string element = "((GRGEN_LIBGR.IGraphElement)" + GetVar(seqContainerAttribute.SourceVar) + ")";
                        container = element + ".GetAttribute(\"" + seqContainerAttribute.AttributeName + "\")";
                        if(seqContainerAttribute.SourceVar.Type == "")
                            ContainerType = "";
                        else
                        {
                            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(seqContainerAttribute.SourceVar.Type, env.Model);
                            AttributeType attributeType = nodeOrEdgeType.GetAttributeType(seqContainerAttribute.AttributeName);
                            ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);
                        }
                    }
                    else
                    {
                        container = GetSequenceExpression(seqContainerAccess.ContainerExpr, source);
                        ContainerType = seqContainerAccess.ContainerExpr.Type(env);
                    }

                    if(ContainerType == "")
                    {
                        SourceBuilder sb = new SourceBuilder();

                        string sourceExpr = GetSequenceExpression(seqContainerAccess.KeyExpr, source);
                        string containerVar = "tmp_eval_once_" + seqContainerAccess.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        if(!TypesHelper.IsSameOrSubtype(seqContainerAccess.KeyExpr.Type(env), "int", model))
                        {
                            sb.AppendFront(array + "[-1]");
                        }
                        else
                        {
                            sb.AppendFront(array + "[(int)" + sourceExpr + "]");
                        }

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        if(!TypesHelper.IsSameOrSubtype(seqContainerAccess.KeyExpr.Type(env), "int", model))
                        {
                            sb.AppendFront(deque + "[-1]");
                        }
                        else
                        {
                            sb.AppendFront(deque + "[(int)" + sourceExpr + "]");
                        }

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + "[" + sourceExpr + "]");

                        sb.AppendFront(")");

                        return sb.ToString();
                    }
                    else if(ContainerType.StartsWith("array"))
                    {
                        string array = container;
                        string sourceExpr = "((int)" + GetSequenceExpression(seqContainerAccess.KeyExpr, source) + ")";
                        return array + "[" + sourceExpr + "]";
                    }
                    else if(ContainerType.StartsWith("deque"))
                    {
                        string deque = container;
                        string sourceExpr = "((int)" + GetSequenceExpression(seqContainerAccess.KeyExpr, source) + ")";
                        return deque + "[" + sourceExpr + "]";
                    }
                    else
                    {
                        string dictionary = container;
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
                        string sourceExpr = "((" + dictSrcType + ")" + GetSequenceExpression(seqContainerAccess.KeyExpr, source) + ")";
                        return dictionary + "[" + sourceExpr + "]";
                    }
                }

                case SequenceExpressionType.ContainerPeek:
                {
                    SequenceExpressionContainerPeek seqContainerPeek = (SequenceExpressionContainerPeek)expr;

                    if(seqContainerPeek.MethodCall != null)
                    {
                        EmitSequenceComputation(seqContainerPeek.MethodCall, source);
                    }
                    string container = GetContainerValue(seqContainerPeek, source);

                    if(seqContainerPeek.KeyExpr != null)
                    {
                        if(seqContainerPeek.ContainerType(env) == "")
                        {
                            return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + ")";
                        }
                        else if(seqContainerPeek.ContainerType(env).StartsWith("array"))
                        {
                            return container + "[(int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + "]";
                        }
                        else if(seqContainerPeek.ContainerType(env).StartsWith("deque"))
                        {
                            return container + "[(int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + "]";
                        }
                        else // statically known set/map/deque
                        {
                            return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + ")";
                        }
                    }
                    else
                    {
                        if(seqContainerPeek.ContainerType(env).StartsWith("array"))
                        {
                            return container + "[" + container + ".Count - 1]";
                        }
                        else if(seqContainerPeek.ContainerType(env).StartsWith("deque"))
                        {
                            return container + "[0]";
                        }
                        else
                        {
                            return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ")";
                        }
                    }
                }

                case SequenceExpressionType.Constant:
                {
                    SequenceExpressionConstant seqConst = (SequenceExpressionConstant)expr;
                    return GetConstant(seqConst.Constant);
                }

                case SequenceExpressionType.SetConstructor:
                case SequenceExpressionType.ArrayConstructor:
                case SequenceExpressionType.DequeConstructor:
                {
                    SequenceExpressionContainerConstructor seqConstr = (SequenceExpressionContainerConstructor)expr;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("fillFromSequence_" + seqConstr.Id);
                    sb.Append("(");
                    for(int i = 0; i < seqConstr.ContainerItems.Length; ++i)
                    {
                        if(i > 0)
                            sb.Append(", ");
                        sb.Append("(");
                        sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
                        sb.Append(")");
                        sb.Append("(");
                        sb.Append(GetSequenceExpression(seqConstr.ContainerItems[i], source));
                        sb.Append(")");
                    }
                    sb.Append(")");
                    return sb.ToString();
                }

                case SequenceExpressionType.MapConstructor:
                {
                    SequenceExpressionMapConstructor seqConstr = (SequenceExpressionMapConstructor)expr;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("fillFromSequence_" + seqConstr.Id);
                    sb.Append("(");
                    for(int i = 0; i < seqConstr.ContainerItems.Length; ++i)
                    {
                        if(i > 0)
                            sb.Append(", ");
                        sb.Append("(");
                        sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.KeyType, model));
                        sb.Append(")");
                        sb.Append("(");
                        sb.Append(GetSequenceExpression(seqConstr.MapKeyItems[i], source));
                        sb.Append("), ");
                        sb.Append("(");
                        sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
                        sb.Append(")");
                        sb.Append("(");
                        sb.Append(GetSequenceExpression(seqConstr.ContainerItems[i], source));
                        sb.Append(")");
                    }
                    sb.Append(")");
                    return sb.ToString();
                }

                case SequenceExpressionType.GraphElementAttribute:
                {
                    SequenceExpressionAttributeAccess seqAttr = (SequenceExpressionAttributeAccess)expr;
                    string element = "((GRGEN_LIBGR.IGraphElement)" + GetVar(seqAttr.SourceVar) + ")";
                    string value = element + ".GetAttribute(\"" + seqAttr.AttributeName + "\")";
                    return "GRGEN_LIBGR.ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(" + element + ", \"" + seqAttr.AttributeName + "\", " + value + ")";
                }

                case SequenceExpressionType.ElementOfMatch:
                {
                    SequenceExpressionMatchAccess seqMA = (SequenceExpressionMatchAccess)expr;
                    String rulePatternClassName = "Rule_" + TypesHelper.ExtractSrc(seqMA.SourceVar.Type);
                    String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(TypesHelper.ExtractSrc(seqMA.SourceVar.Type));                     
                    string match = "((" + matchInterfaceName + ")" + GetVar(seqMA.SourceVar) + ")";
                    if(TypesHelper.GetNodeType(seqMA.Type(env), model) != null)
                        return match + ".node_" + seqMA.ElementName;
                    else if(TypesHelper.GetNodeType(seqMA.Type(env), model) != null)
                        return match + ".edge_" + seqMA.ElementName;
                    else
                        return match + ".var_" + seqMA.ElementName;
                }

                case SequenceExpressionType.ElementFromGraph:
                {
                    SequenceExpressionElementFromGraph seqFromGraph = (SequenceExpressionElementFromGraph)expr;
                    return "((GRGEN_LIBGR.INamedGraph)graph).GetGraphElement(\""+seqFromGraph.ElementName+"\")";
                }

                case SequenceExpressionType.Source:
                {
                    SequenceExpressionSource seqSrc = (SequenceExpressionSource)expr;
                    return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqSrc.Edge, source) + ").Source";
                }

                case SequenceExpressionType.Target:
                {
                    SequenceExpressionTarget seqTgt = (SequenceExpressionTarget)expr;
                    return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqTgt.Edge, source) + ").Target";
                }

                case SequenceExpressionType.Opposite:
                {
                    SequenceExpressionOpposite seqOpp = (SequenceExpressionOpposite)expr;
                    return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqOpp.Edge, source) + ").Opposite(" + GetSequenceExpression(seqOpp.Node, source) + ")";
                }

                case SequenceExpressionType.Variable:
                {
                    SequenceExpressionVariable seqVar = (SequenceExpressionVariable)expr;
                    return GetVar(seqVar.Variable);
                }

                case SequenceExpressionType.FunctionCall:
                {
                    SequenceExpressionFunctionCall seqFuncCall = (SequenceExpressionFunctionCall)expr;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Functions.");
                    sb.Append(seqFuncCall.ParamBindings.Name);
                    sb.Append("(procEnv, graph");
                    sb.Append(BuildParameters(seqFuncCall.ParamBindings));
                    sb.Append(")");
                    return sb.ToString();
                }

                default:
                    throw new Exception("Unknown sequence expression type: " + expr.SequenceExpressionType);
            }
        }

        private string ExtractNodeType(SourceBuilder source, SequenceExpression typeExpr)
        {
            string adjacentNodeType = "graph.Model.NodeModel.RootType";
            if(typeExpr != null)
            {
                if(typeExpr.Type(env) != "")
                {
                    if(typeExpr.Type(env) == "string")
                        adjacentNodeType = "graph.Model.NodeModel.GetType((string)" + GetSequenceExpression(typeExpr, source) + ")";
                    else
                        adjacentNodeType = "(GRGEN_LIBGR.NodeType)" + GetSequenceExpression(typeExpr, source);
                }
                else
                {
                    adjacentNodeType = GetSequenceExpression(typeExpr, source) + " is string ? "
                        + "graph.Model.NodeModel.GetType((string)" + GetSequenceExpression(typeExpr, source) + ")"
                        + " : " + "(GRGEN_LIBGR.NodeType)" + GetSequenceExpression(typeExpr, source);
                }
            }
            return "(" + adjacentNodeType + ")";
        }

        private string ExtractEdgeType(SourceBuilder source, SequenceExpression typeExpr)
        {
            string incidentEdgeType = "graph.Model.EdgeModel.RootType";
            if(typeExpr != null)
            {
                if(typeExpr.Type(env) != "")
                {
                    if(typeExpr.Type(env) == "string")
                        incidentEdgeType = "graph.Model.EdgeModel.GetType((string)" + GetSequenceExpression(typeExpr, source) + ")";
                    else
                        incidentEdgeType = "(GRGEN_LIBGR.EdgeType)" + GetSequenceExpression(typeExpr, source);
                }
                else
                {
                    incidentEdgeType = GetSequenceExpression(typeExpr, source) + " is string ? "
                        + "graph.Model.EdgeModel.GetType((string)" + GetSequenceExpression(typeExpr, source) + ")"
                        + " : " + "(GRGEN_LIBGR.EdgeType)" + GetSequenceExpression(typeExpr, source);
                }
            }
            return "(" + incidentEdgeType + ")";
        }

        string GetContainerValue(SequenceExpressionContainer container, SourceBuilder source)
        {
            if(container.MethodCall != null)
                return GetResultVar(container.MethodCall);
            else
            {
                if(container.ContainerExpr is SequenceExpressionAttributeAccess)
                {
                    SequenceExpressionAttributeAccess attribute = (SequenceExpressionAttributeAccess)container.ContainerExpr;
                    return "((GRGEN_LIBGR.IGraphElement)" + GetVar(attribute.SourceVar) + ")" + ".GetAttribute(\"" + attribute.AttributeName + "\")";
                }
                else
                    return GetSequenceExpression(container.ContainerExpr, source);
            }
        }

        private string GetConstant(object constant)
        {
            if(constant is bool)
            {
                return (bool)constant == true ? "true" : "false";
            }
            else if(constant is Enum)
            {
                Enum enumConst = (Enum)constant;
                return enumConst.GetType().ToString() + "." + enumConst.ToString();
            }
            else if(constant is IDictionary)
            {
                Type keyType;
                Type valueType;
                ContainerHelper.GetDictionaryTypes(constant, out keyType, out valueType);
                String srcType = "typeof(" + TypesHelper.PrefixedTypeFromType(keyType) + ")";
                String dstType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                return "GRGEN_LIBGR.ContainerHelper.NewDictionary(" + srcType + "," + dstType + ")";
            }
            else if(constant is IList)
            {
                Type valueType;
                ContainerHelper.GetListType(constant, out valueType);
                String dequeValueType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                return "GRGEN_LIBGR.ContainerHelper.NewList(" + dequeValueType + ")";
            }
            else if(constant is IDeque)
            {
                Type valueType;
                ContainerHelper.GetDequeType(constant, out valueType);
                String dequeValueType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                return "GRGEN_LIBGR.ContainerHelper.NewDeque(" + dequeValueType + ")";
            }
            else if(constant is string)
            {
                return "\"" + constant.ToString() + "\"";
            }
            else if(constant is float)
            {
                return ((float)constant).ToString(System.Globalization.CultureInfo.InvariantCulture) + "f";
            }
            else if(constant is double)
            {
                return ((double)constant).ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else if(constant is sbyte)
            {
                return "((sbyte)" + constant.ToString() + ")";
            }
            else if(constant is short)
            {
                return "((short)" + constant.ToString() + ")";
            }
            else if(constant is long)
            {
                return "((long)" + constant.ToString() + ")";
            }
            else if(constant is NodeType)
            {
                return "(GRGEN_LIBGR.TypesHelper.GetNodeType(\"" + constant + "\", graph.Model))";
            }
            else if(constant is EdgeType)
            {
                return "(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"" + constant + "\", graph.Model))";
            }
            else
            {
                if(constant == null)
                    return "null";
                else
                    return constant.ToString();
            }
        }

		public bool GenerateXGRSCode(string xgrsName, String xgrsStr,
            String[] paramNames, GrGenType[] paramTypes,
            String[] defToBeYieldedToNames, GrGenType[] defToBeYieldedToTypes,
            SourceBuilder source, int lineNr)
		{
			Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for (int i = 0; i < paramNames.Length; i++)
            {
                varDecls.Add(paramNames[i], TypesHelper.DotNetTypeToXgrsType(paramTypes[i]));
            }
            for(int i = 0; i < defToBeYieldedToNames.Length; i++)
            {
                varDecls.Add(defToBeYieldedToNames[i], TypesHelper.DotNetTypeToXgrsType(defToBeYieldedToTypes[i]));
            }

			Sequence seq;
            try
            {
                List<string> warnings = new List<string>();
                seq = SequenceParser.ParseSequence(xgrsStr, 
                    ruleNames, sequenceNames, procedureNames, functionNames,
                    functionOutputTypes, varDecls, model, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("The exec statement \"" + xgrsStr
                        + "\" given on line " + lineNr + " reported back:\n" + warning);
                }
                seq.Check(env);
            }
            catch(ParseException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" given on line " + lineNr + " caused the following error:\n" + ex.Message);
                return false;
            }
            catch(SequenceParserException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" given on line " + lineNr + " caused the following error:\n");
                HandleSequenceParserException(ex);
                return false;
            }

            source.Append("\n");
            source.AppendFront("public static bool ApplyXGRS_" + xgrsName + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
			for(int i = 0; i < paramNames.Length; i++)
			{
				source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(paramTypes[i]), model) + " var_");
				source.Append(paramNames[i]);
			}
            for(int i = 0; i < defToBeYieldedToTypes.Length; i++)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(defToBeYieldedToTypes[i]), model) + " var_");
                source.Append(defToBeYieldedToNames[i]);
            }
            source.Append(")\n");
			source.AppendFront("{\n");
			source.Indent();

            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            source.AppendFront("GRGEN_LGSP.LGSPActions actions = procEnv.curActions;\n");

			knownRules.Clear();

            EmitNeededVarAndRuleEntities(seq, source);

			EmitSequence(seq, source);

            source.AppendFront("return " + GetResultVar(seq) + ";\n");
			source.Unindent();
			source.AppendFront("}\n");

            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            seq.GetLocalVariables(variables, containerConstructors, null);
            foreach(SequenceExpressionContainerConstructor cc in containerConstructors)
            {
                GenerateContainerConstructor(cc, source);
            }

			return true;
		}

        public bool GenerateDefinedSequence(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for(int i = 0; i < sequence.Parameters.Length; i++)
            {
                varDecls.Add(sequence.Parameters[i], TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]));
            }
            for(int i = 0; i < sequence.OutParameters.Length; i++)
            {
                varDecls.Add(sequence.OutParameters[i], TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]));
            }

            Sequence seq;
            try
            {
                List<string> warnings = new List<string>();
                seq = SequenceParser.ParseSequence(sequence.XGRS, 
                    ruleNames, sequenceNames, procedureNames, functionNames, 
                    functionOutputTypes, varDecls, model, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("In the defined sequence " + sequence.Name
                        + " the exec part \"" + sequence.XGRS
                        + "\" reported back:\n" + warning);
                }
                seq.Check(env);
            }
            catch(ParseException ex)
            {
                Console.Error.WriteLine("In the defined sequence " + sequence.Name
                    + " the exec part \"" + sequence.XGRS
                    + "\" caused the following error:\n" + ex.Message);
                return false;
            }
            catch(SequenceParserException ex)
            {
                Console.Error.WriteLine("In the defined sequence " + sequence.Name
                    + " the exec part \"" + sequence.XGRS
                    + "\" caused the following error:\n");
                HandleSequenceParserException(ex);
                return false;
            }

            // exact sequence definition compiled class
            source.Append("\n");
            source.AppendFront("public class Sequence_" + sequence.Name + " : GRGEN_LIBGR.SequenceDefinitionCompiled\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateSequenceDefinedSingleton(source, sequence);

            source.Append("\n");
            GenerateInternalDefinedSequenceApplicationMethod(source, sequence, seq);

            source.Append("\n");
            GenerateExactExternalDefinedSequenceApplicationMethod(source, sequence);

            source.Append("\n");
            GenerateGenericExternalDefinedSequenceApplicationMethod(source, sequence);

            // end of exact sequence definition compiled class
            source.Unindent();
            source.AppendFront("}\n");

            return true;
        }

        public bool GenerateExternalDefinedSequence(SourceBuilder source, ExternalDefinedSequenceInfo sequence)
        {
            // exact sequence definition compiled class
            source.Append("\n");
            source.AppendFront("public partial class Sequence_" + sequence.Name + " : GRGEN_LIBGR.SequenceDefinitionCompiled\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateSequenceDefinedSingleton(source, sequence);

            source.Append("\n");
            GenerateExactExternalDefinedSequenceApplicationMethod(source, sequence);

            source.Append("\n");
            GenerateGenericExternalDefinedSequenceApplicationMethod(source, sequence);

            // end of exact sequence definition compiled class
            source.Unindent();
            source.AppendFront("}\n");

            return true;
        }

        public void GenerateExternalDefinedSequencePlaceholder(SourceBuilder source, ExternalDefinedSequenceInfo sequence, String externalActionsExtensionFilename)
        {
            source.Append("\n");
            source.AppendFront("public partial class Sequence_" + sequence.Name + "\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateInternalDefinedSequenceApplicationMethodStub(source, sequence, externalActionsExtensionFilename);

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateSequenceDefinedSingleton(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            String className = "Sequence_" + sequence.Name;
            source.AppendFront("private static "+className+" instance = null;\n");

            source.AppendFront("public static "+className+" Instance { get { if(instance==null) instance = new "+className+"(); return instance; } }\n");

            source.AppendFront("private "+className+"() : base(\""+sequence.Name+"\", SequenceInfo_"+sequence.Name+".Instance) { }\n");
        }

        private void GenerateInternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence, Sequence seq)
        {
            source.AppendFront("public static bool ApplyXGRS_" + sequence.Name + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
            }
            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            source.AppendFront("GRGEN_LGSP.LGSPActions actions = procEnv.curActions;\n");

            knownRules.Clear();

            EmitNeededVarAndRuleEntities(seq, source);

            EmitSequence(seq, source);

            source.AppendFront("return " + GetResultVar(seq) + ";\n");
            source.Unindent();
            source.AppendFront("}\n");

            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            seq.GetLocalVariables(variables, containerConstructors, null);
            foreach(SequenceExpressionContainerConstructor cc in containerConstructors)
            {
                GenerateContainerConstructor(cc, source);
            }
        }

        private void GenerateInternalDefinedSequenceApplicationMethodStub(SourceBuilder source, DefinedSequenceInfo sequence, String externalActionsExtensionFilename)
        {
            source.AppendFrontFormat("// You must implement the following function in the same partial class in ./{0}\n", externalActionsExtensionFilename);

            source.AppendFront("//public static bool ApplyXGRS_" + sequence.Name + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
            }
            source.Append(")\n");
        }

        private void GenerateExactExternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            source.AppendFront("public static bool Apply_" + sequence.Name + "(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
            }
            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model);
                source.AppendFront(typeName + " vari_" + sequence.OutParameters[i]);
                source.Append(" = " + TypesHelper.DefaultValueString(typeName, model) + ";\n");
            }
            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", var_" + sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref var_" + sequence.OutParameters[i]);
            }
            source.Append(");\n");
            if(sequence.OutParameters.Length > 0)
            {
                source.AppendFront("if(result) {\n");
                source.Indent();
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.AppendFront("var_" + sequence.OutParameters[i]);
                    source.Append(" = vari_" + sequence.OutParameters[i] + ";\n");
                }
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("return result;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateGenericExternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            source.AppendFront("public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv).graph;\n");

            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.Parameters[i]);
                source.Append(" = (" + typeName + ")sequenceInvocation.ArgumentExpressions[" + i + "].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);\n");
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.OutParameters[i]);
                source.Append(" = " + TypesHelper.DefaultValueString(typeName, model) + ";\n");
            }

            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", var_" + sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref var_" + sequence.OutParameters[i]);
            }
            source.Append(");\n");
            if(sequence.OutParameters.Length > 0)
            {
                source.AppendFront("if(result) {\n");
                source.Indent();
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.AppendFront("sequenceInvocation.ReturnVars[" + i + "].SetVariableValue(var_" + sequence.OutParameters[i] + ", procEnv);\n");
                }
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("return result;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void GenerateFilterStubs(SourceBuilder source, LGSPRulePattern rulePattern)
        {
            String rulePatternClassName = rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";

            foreach(string filterName in rulePattern.Filters)
            {
                if(filterName == "auto")
                    continue;

                source.AppendFrontFormat("//public static void Filter_{0}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {1} matches)\n", filterName, matchesListType);
            }
        }

        public void GenerateAutomorphyFilters(SourceBuilder source, LGSPRulePattern rulePattern)
        {
            String rulePatternClassName = rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";

            foreach(string filterName in rulePattern.Filters)
            {
                if(filterName != "auto")
                    continue;

                source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n", rulePattern.name, filterName, matchesListType);
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFront("if(matches.Count<2)\n");
                source.AppendFront("\treturn;\n");

                source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);
                source.AppendFront("for(int i = 0; i < matchesArray.Count; ++i)\n");
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFront("if(matchesArray[i] == null)\n");
                source.AppendFront("\tcontinue;\n");
                
                source.AppendFront("for(int j = i + 1; j < matchesArray.Count; ++j)\n");
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFront("if(matchesArray[j] == null)\n");
                source.AppendFront("\tcontinue;\n");

                source.AppendFront("if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(matchesArray[i], matchesArray[j], procEnv.graph))\n");
                source.AppendFront("\tmatchesArray[j] = null;\n");
                
                source.Unindent();
                source.AppendFront("}\n");

                source.Unindent();
                source.AppendFront("}\n");
                source.AppendFront("matches.FromList();\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        void GenerateContainerConstructor(SequenceExpressionContainerConstructor cc, SourceBuilder source)
        {
            string containerType = TypesHelper.XgrsTypeToCSharpType(GetContainerType(cc), model);
            string valueType = TypesHelper.XgrsTypeToCSharpType(cc.ValueType, model);
            string keyType = null;
            if(cc is SequenceExpressionMapConstructor)
                keyType = TypesHelper.XgrsTypeToCSharpType(((SequenceExpressionMapConstructor)cc).KeyType, model);

            source.Append("\n");
            source.AppendFront("public static ");
            source.Append(containerType);
            source.Append(" fillFromSequence_" + cc.Id);
            source.Append("(");
            for(int i = 0; i < cc.ContainerItems.Length; ++i)
            {
                if(i > 0)
                    source.Append(", ");
                if(keyType != null)
                    source.AppendFormat("{0} paramkey{1}, ", keyType, i);
                source.AppendFormat("{0} param{1}", valueType, i);
            }
            source.Append(")\n");
            
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} container = new {0}();\n", containerType);
            for(int i = 0; i < cc.ContainerItems.Length; ++i)
            {
                if(cc is SequenceExpressionSetConstructor)
                    source.AppendFrontFormat("container.Add(param{0}, null);\n", i);
                else if(cc is SequenceExpressionMapConstructor)
                    source.AppendFrontFormat("container.Add(paramkey{0}, param{0});\n", i);
                else if(cc is SequenceExpressionArrayConstructor)
                    source.AppendFrontFormat("container.Add(param{0});\n", i);
                else //if(cc is SequenceExpressionDequeConstructor)
                    source.AppendFrontFormat("container.Enqueue(param{0});\n", i);
            }
            source.AppendFront("return container;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        static string GetContainerType(SequenceExpressionContainerConstructor cc)
        {
            if(cc is SequenceExpressionSetConstructor)
                return "set<" + cc.ValueType + ">";
            else if(cc is SequenceExpressionMapConstructor)
                return "map<" + ((SequenceExpressionMapConstructor)cc).KeyType + "," + cc.ValueType + ">";
            else if(cc is SequenceExpressionArrayConstructor)
                return "array<" + cc.ValueType + ">";
            else //if(cc is SequenceExpressionDequeConstructor)
                return "deque<" + cc.ValueType + ">";
        }

        void HandleSequenceParserException(SequenceParserException ex)
        {
            if(ex.Name == null 
                && ex.Kind != SequenceParserError.TypeMismatch
                && ex.Kind != SequenceParserError.FilterError
                && ex.Kind != SequenceParserError.OperatorNotFound)
            {
                Console.Error.WriteLine("Unknown rule/sequence: \"{0}\"", ex.Name);
                return;
            }

            switch(ex.Kind)
            {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if(rulesToInputTypes[ex.Name].Count != ex.NumGivenInputs && rulesToOutputTypes[ex.Name].Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of parameters and return values for action/sequence \"" + ex.Name + "\"!");
                    else if(rulesToInputTypes[ex.Name].Count != ex.NumGivenInputs)
                        Console.Error.WriteLine("Wrong number of parameters for action/sequence \"" + ex.Name + "\"!");
                    else if(rulesToOutputTypes[ex.Name].Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of return values for action/sequence \"" + ex.Name + "\"!");
                    else
                        goto default;
                    break;

                case SequenceParserError.BadParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". parameter is not valid for action/sequence \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.BadReturnParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". return parameter is not valid for action/sequence \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.FilterError:
                    Console.Error.WriteLine("Can't apply filter " + ex.FilterName + " to rule!");
                    return;

                case SequenceParserError.OperatorNotFound:
                    Console.Error.WriteLine("Operator not found/arguments not of correct type: " + ex.Expression);
                    return;

                case SequenceParserError.RuleNameUsedByVariable:
                    Console.Error.WriteLine("The name of the variable conflicts with the name of action/sequence \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    Console.Error.WriteLine("The variable \"" + ex.Name + "\" may neither receive parameters nor return values!");
                    return;

                case SequenceParserError.UnknownAttribute:
                    Console.WriteLine("Unknown attribute \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.TypeMismatch:
                    Console.Error.WriteLine("The construct \"" + ex.VariableOrFunctionName + "\" expects:" + ex.ExpectedType + " but is / is given " + ex.GivenType + "!");
                    return;

                default:
                    throw new ArgumentException("Invalid error kind: " + ex.Kind);
            }

            // todo: Sequence
            Console.Error.Write("Prototype: {0}", ex.Name);
            if(rulesToInputTypes[ex.Name].Count != 0)
            {
                Console.Error.Write("(");
                bool first = true;
                foreach(String typeName in rulesToInputTypes[ex.Name])
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
            if(rulesToOutputTypes[ex.Name].Count != 0)
            {
                Console.Error.Write(" : (");
                bool first = true;
                foreach(String typeName in rulesToOutputTypes[ex.Name])
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
            Console.Error.WriteLine();
        }
    }
}
