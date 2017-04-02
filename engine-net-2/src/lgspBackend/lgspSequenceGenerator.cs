/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

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

        // maps rule names available in the .grg to compile to the list of the match filters
        Dictionary<String, List<IFilter>> rulesToFilters;

        // maps filter function names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> filterFunctionsToInputTypes;

        // maps rule names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> rulesToOutputTypes;
        
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
        // tells for a procedure given by its name whether it is external
        Dictionary<String, bool> proceduresToIsExternal;
        
        // maps function names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> functionsToInputTypes;
        // maps function names available in the .grg to compile to the output typ name
        Dictionary<String, String> functionsToOutputType;
        // tells for a function given by its name whether it is external
        Dictionary<String, bool> functionsToIsExternal;

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
        // array containing the names of the filter functions available in the .grg to compile
        String[] filterFunctionNames;

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
            Dictionary<String, List<IFilter>> rulesToFilters, Dictionary<String, List<String>> filterFunctionsToInputTypes,
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes,
            Dictionary<String, List<String>> rulesToTopLevelEntities, Dictionary<String, List<String>> rulesToTopLevelEntityTypes, 
            Dictionary<String, List<String>> sequencesToInputTypes, Dictionary<String, List<String>> sequencesToOutputTypes,
            Dictionary<String, List<String>> proceduresToInputTypes, Dictionary<String, List<String>> proceduresToOutputTypes, Dictionary<String, bool> proceduresToIsExternal,
            Dictionary<String, List<String>> functionsToInputTypes, Dictionary<String, String> functionsToOutputType, Dictionary<String, bool> functionsToIsExternal)
        {
            this.gen = gen;
            this.model = model;
            this.rulesToFilters = rulesToFilters;
            this.filterFunctionsToInputTypes = filterFunctionsToInputTypes;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.rulesToTopLevelEntities = rulesToTopLevelEntities;
            this.rulesToTopLevelEntityTypes = rulesToTopLevelEntityTypes;
            this.sequencesToInputTypes = sequencesToInputTypes;
            this.sequencesToOutputTypes = sequencesToOutputTypes;
            this.proceduresToInputTypes = proceduresToInputTypes;
            this.proceduresToOutputTypes = proceduresToOutputTypes;
            this.proceduresToIsExternal = proceduresToIsExternal;
            this.functionsToInputTypes = functionsToInputTypes;
            this.functionsToOutputType = functionsToOutputType;
            this.functionsToIsExternal = functionsToIsExternal;

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
            // extract filter function names from domain of filter functions to input types
            filterFunctionNames = new String[filterFunctionsToInputTypes.Count];
            i = 0;
            foreach(KeyValuePair<String, List<String>> filterFunctionToInputType in filterFunctionsToInputTypes)
            {
                filterFunctionNames[i] = filterFunctionToInputType.Key;
                ++i;
            }

            // create the environment for (type) checking the compiled sequences after parsing
            env = new SequenceCheckingEnvironmentCompiled(
                ruleNames, sequenceNames, procedureNames, functionNames,
                rulesToFilters, filterFunctionsToInputTypes,
                rulesToInputTypes, rulesToOutputTypes, 
                rulesToTopLevelEntities, rulesToTopLevelEntityTypes,
                sequencesToInputTypes, sequencesToOutputTypes,
                proceduresToInputTypes, proceduresToOutputTypes, proceduresToIsExternal,
                functionsToInputTypes, functionsToOutputType, functionsToIsExternal,
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
                case SequenceType.RuleCountAllCall:
                {
					SequenceRuleCall seqRule = (SequenceRuleCall) seq;
					String ruleName = seqRule.ParamBindings.PackagePrefixedName;
					if(!knownRules.ContainsKey(ruleName))
					{
                        knownRules.Add(ruleName, null);
                        source.AppendFront(TypesHelper.GetPackagePrefixDot(seqRule.ParamBindings.Package) + "Action_" + seqRule.ParamBindings.Name + " " + "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.ParamBindings.Package, seqRule.ParamBindings.Name));
                        source.Append(" = " + TypesHelper.GetPackagePrefixDot(seqRule.ParamBindings.Package) + "Action_" + seqRule.ParamBindings.Name + ".Instance;\n");
                    }
                    // no handling for the input arguments seqRule.ParamBindings.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i=0; i<seqRule.ParamBindings.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqRule.ParamBindings.ReturnVars[i], source);
                    }
                    if(seq.SequenceType == SequenceType.RuleCountAllCall)
                    {
                        SequenceRuleCountAllCall seqCountRuleAll = (SequenceRuleCountAllCall)seqRule;
                        EmitVarIfNew(seqCountRuleAll.CountResult, source);
                    }
					break;
				}

                case SequenceType.SequenceCall:
                {
                    SequenceSequenceCall seqSeq = (SequenceSequenceCall)seq;
                    // no handling for the input arguments seqSeq.ParamBindings.ArgumentExpressions or the optional Subgraph needed 
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

                case SequenceType.ForIntegerRange:
                {
                    SequenceForIntegerRange seqFor = (SequenceForIntegerRange)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    break;
                }

                case SequenceType.ForIndexAccessEquality:
                {
                    SequenceForIndexAccessEquality seqFor = (SequenceForIndexAccessEquality)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    break;
                }

                case SequenceType.ForIndexAccessOrdering:
                {
                    SequenceForIndexAccessOrdering seqFor = (SequenceForIndexAccessOrdering)seq;
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
                case SequenceType.ForReachableNodes:
                case SequenceType.ForReachableNodesViaIncoming:
                case SequenceType.ForReachableNodesViaOutgoing:
                case SequenceType.ForReachableEdges:
                case SequenceType.ForReachableEdgesViaIncoming:
                case SequenceType.ForReachableEdgesViaOutgoing:
                case SequenceType.ForBoundedReachableNodes:
                case SequenceType.ForBoundedReachableNodesViaIncoming:
                case SequenceType.ForBoundedReachableNodesViaOutgoing:
                case SequenceType.ForBoundedReachableEdges:
                case SequenceType.ForBoundedReachableEdgesViaIncoming:
                case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                case SequenceType.ForNodes:
                case SequenceType.ForEdges:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;
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
            String parameterDeclarations = null;
            String parameters = null;
            if(paramBindings.Subgraph != null)
                parameters = BuildParametersInDeclarations(paramBindings, out parameterDeclarations);
            else
                parameters = BuildParameters(paramBindings);
            String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seqRule.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seqRule.Id;

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + GetVar(paramBindings.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                + parameters + ");\n");
            for(int i = 0; i < seqRule.Filters.Count; ++i)
            {
                EmitFilterCall(source, seqRule.Filters[i], patternName, matchesName);
            }

            if(gen.FireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
            if(seqRule is SequenceRuleCountAllCall)
            {
                SequenceRuleCountAllCall seqRuleCountAll = (SequenceRuleCountAllCall)seqRule;
                source.AppendFront(SetVar(seqRuleCountAll.CountResult, matchesName + ".Count"));
            }

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
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(gen.FireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            BuildReturnParameters(paramBindings,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            if(seqRule.SequenceType == SequenceType.RuleCall)
            {
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            }
            else if(seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
            {
                // iterate through matches, use Modify on each, fire the next match event after the first
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                String enumeratorName = "enum_" + seqRule.Id;
                source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                source.AppendFront("if(" + matchName + "!=" + matchesName + ".FirstExact) procEnv.RewritingNextMatch();\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
            }
            else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
            {
                // as long as a further rewrite has to be selected: randomly choose next match, rewrite it and remove it from available matches; fire the next match event after the first
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                source.AppendFrontFormat("int numchooserandomvar_{0} = (int){1};\n", seqRuleAll.Id, seqRuleAll.MaxVarChooseRandom != null ? GetVar(seqRuleAll.MaxVarChooseRandom) : (seqRuleAll.MinSpecified ? "2147483647" : "1"));
                source.AppendFrontFormat("if({0}.Count < numchooserandomvar_{1}) numchooserandomvar_{1} = {0}.Count;\n", matchesName, seqRule.Id);
                source.AppendFrontFormat("for(int i = 0; i < numchooserandomvar_{0}; ++i)\n", seqRule.Id);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("if(i != 0) procEnv.RewritingNextMatch();\n");
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
            }

            if(gen.FireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.Unindent();
            source.AppendFront("}\n");

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

        void EmitSequenceCall(SequenceSequenceCall seqSeq, SourceBuilder source)
        {
            SequenceInvocationParameterBindings paramBindings = seqSeq.ParamBindings;
            String parameterDeclarations = null;
            String parameters = null;
            if(paramBindings.Subgraph != null)
                parameters = BuildParametersInDeclarations(paramBindings, out parameterDeclarations);
            else
                parameters = BuildParameters(paramBindings);
            String outParameterDeclarations;
            String outArguments;
            String outAssignments;
            BuildOutParameters(paramBindings, out outParameterDeclarations, out outArguments, out outAssignments);

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + GetVar(paramBindings.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            if(outParameterDeclarations.Length != 0)
                source.AppendFront(outParameterDeclarations + "\n");
            source.AppendFront("if(" + TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Sequence_" + paramBindings.Name + ".ApplyXGRS_" + paramBindings.Name
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

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

		void EmitSequence(Sequence seq, SourceBuilder source)
		{
			switch(seq.SequenceType)
			{
				case SequenceType.RuleCall:
                case SequenceType.RuleAllCall:
                case SequenceType.RuleCountAllCall:
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
                        String srcTypeXgrs = TypesHelper.ExtractSrc(seqFor.Container.Type);
                        String srcType = TypesHelper.XgrsTypeToCSharpType(srcTypeXgrs, model);
                        String dstTypeXgrs = TypesHelper.ExtractDst(seqFor.Container.Type);
                        String dstType = TypesHelper.XgrsTypeToCSharpType(dstTypeXgrs, model);
                        source.AppendFront("foreach(KeyValuePair<" + srcType + "," + dstType + "> entry_" + seqFor.Id + " in " + GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();

                        if(dstTypeXgrs== "SetValueType")
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        else
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));

                        if (seqFor.VarDst != null)
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }

                    break;
                }

                case SequenceType.ForIntegerRange:
                {
                    SequenceForIntegerRange seqFor = (SequenceForIntegerRange)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    String ascendingVar = "ascending_" + seqFor.Id;
                    String entryVar = "entry_" + seqFor.Id;
                    String limitVar = "limit_" + seqFor.Id;
                    source.AppendFrontFormat("int {0} = (int)({1});\n", entryVar, GetSequenceExpression(seqFor.Left, source));
                    source.AppendFrontFormat("int {0} = (int)({1});\n", limitVar, GetSequenceExpression(seqFor.Right, source));
                    source.AppendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");

                    source.AppendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront(SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");

                    source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));

                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.ForIndexAccessEquality:
                {
                    SequenceForIndexAccessEquality seqFor = (SequenceForIndexAccessEquality)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    String indexVar = "index_" + seqFor.Id;
                    source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
                    String entryVar = "entry_" + seqFor.Id;
                    source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                        entryVar, indexVar);
                    source.Append("(");
                    source.Append(GetSequenceExpression(seqFor.Expr, source));
                    source.Append("))\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(gen.EmitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.ForIndexAccessOrdering:
                {
                    SequenceForIndexAccessOrdering seqFor = (SequenceForIndexAccessOrdering)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    String indexVar = "index_" + seqFor.Id;
                    source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
                    String entryVar = "entry_" + seqFor.Id;
                    source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                        entryVar, indexVar);

                    if(seqFor.Ascending)
                        source.Append("Ascending");
                    else
                        source.Append("Descending");
                    if(seqFor.From() != null && seqFor.To() != null)
                    {
                        source.Append("From");
                        if(seqFor.IncludingFrom())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("To");
                        if(seqFor.IncludingTo())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(GetSequenceExpression(seqFor.From(), source));
                        source.Append(", ");
                        source.Append(GetSequenceExpression(seqFor.To(), source));
                    }
                    else if(seqFor.From() != null)
                    {
                        source.Append("From");
                        if(seqFor.IncludingFrom())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(GetSequenceExpression(seqFor.From(), source));
                    }
                    else if(seqFor.To() != null)
                    {
                        source.Append("To");
                        if(seqFor.IncludingTo())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(GetSequenceExpression(seqFor.To(), source));
                    }
                    else
                    {
                        source.Append("(");
                    }

                    source.Append("))\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(gen.EmitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

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
                case SequenceType.ForReachableNodes:
                case SequenceType.ForReachableNodesViaIncoming:
                case SequenceType.ForReachableNodesViaOutgoing:
                case SequenceType.ForReachableEdges:
                case SequenceType.ForReachableEdgesViaIncoming:
                case SequenceType.ForReachableEdgesViaOutgoing:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = GetSequenceExpression(seqFor.ArgExprs[0], source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);

                    SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 2 ? seqFor.ArgExprs[1] : null;
                    string incidentEdgeTypeExpr = ExtractEdgeType(source, IncidentEdgeType);
                    SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
                    string adjacentNodeTypeExpr = ExtractNodeType(source, AdjacentNodeType);

                    string iterationVariable; // valid for incident/adjacent and reachable
                    string iterationType;
                    string edgeMethod = null; // only valid for incident/adajcent
                    string theOther = null; // only valid for incident/adjacent
                    string reachableMethod = null; // only valid for reachable
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForAdjacentNodes:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForAdjacentNodesViaIncoming:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForAdjacentNodesViaOutgoing:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForIncidentEdges:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForIncomingEdges:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForOutgoingEdges:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableNodes:
                            reachableMethod = "";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableNodesViaIncoming:
                            reachableMethod = "Incoming";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableNodesViaOutgoing:
                            reachableMethod = "Outgoing";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableEdges:
                            reachableMethod = "Edges";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableEdgesViaIncoming:
                            reachableMethod = "EdgesIncoming";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableEdgesViaOutgoing:
                            reachableMethod = "EdgesOutgoing";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        default:
                            edgeMethod = theOther = iterationVariable = iterationType = "INTERNAL ERROR";
                            break;
                    }

                    string profilingArgument = gen.EmitProfiling ? ", procEnv" : "";
                    if(seqFor.SequenceType == SequenceType.ForReachableNodes || seqFor.SequenceType == SequenceType.ForReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableNodesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GraphHelper.Reachable{1}(node_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else if(seqFor.SequenceType == SequenceType.ForReachableEdges || seqFor.SequenceType == SequenceType.ForReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableEdgesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GraphHelper.Reachable{1}(node_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else
                    {
                        if(gen.EmitProfiling)
                            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.{1})\n",
                                seqFor.Id, edgeMethod);
                        else
                            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatible{1}({2}))\n",
                                seqFor.Id, edgeMethod, incidentEdgeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();

                    if(seqFor.SequenceType != SequenceType.ForReachableNodes && seqFor.SequenceType != SequenceType.ForReachableNodesViaIncoming && seqFor.SequenceType != SequenceType.ForReachableNodesViaOutgoing
                        && seqFor.SequenceType != SequenceType.ForReachableEdges && seqFor.SequenceType != SequenceType.ForReachableEdgesViaIncoming || seqFor.SequenceType != SequenceType.ForReachableEdgesViaOutgoing)
                    {
                        if(gen.EmitProfiling)
                        {
                            source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                            source.AppendFrontFormat("if(!edge_{0}.InstanceOf(", seqFor.Id);
                            source.Append(incidentEdgeTypeExpr);
                            source.Append("))\n");
                            source.AppendFront("\tcontinue;\n");
                        }

                        // incident/adjacent needs a check for adjacent node, cause only incident edge can be type constrained in the loop
                        // reachable already allows to iterate exactly the edges of interest
                        source.AppendFrontFormat("if(!{0}.InstanceOf({1}))\n",
                            theOther, adjacentNodeTypeExpr);
                        source.AppendFront("\tcontinue;\n");
                    }

                    source.AppendFront(SetVar(seqFor.Var, iterationVariable));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForBoundedReachableNodes:
                case SequenceType.ForBoundedReachableNodesViaIncoming:
                case SequenceType.ForBoundedReachableNodesViaOutgoing:
                case SequenceType.ForBoundedReachableEdges:
                case SequenceType.ForBoundedReachableEdgesViaIncoming:
                case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = GetSequenceExpression(seqFor.ArgExprs[0], source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);
                    string depthExpr = GetSequenceExpression(seqFor.ArgExprs[1], source);
                    source.AppendFrontFormat("int depth_{0} = (int)({1});\n", seqFor.Id, depthExpr);

                    SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
                    string incidentEdgeTypeExpr = ExtractEdgeType(source, IncidentEdgeType);
                    SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 4 ? seqFor.ArgExprs[3] : null;
                    string adjacentNodeTypeExpr = ExtractNodeType(source, AdjacentNodeType);

                    string iterationVariable; // valid for incident/adjacent and reachable
                    string iterationType;
                    string edgeMethod = null; // only valid for incident/adajcent
                    string theOther = null; // only valid for incident/adjacent
                    string reachableMethod = null; // only valid for reachable
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForBoundedReachableNodes:
                            reachableMethod = "";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableNodesViaIncoming:
                            reachableMethod = "Incoming";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableNodesViaOutgoing:
                            reachableMethod = "Outgoing";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableEdges:
                            reachableMethod = "Edges";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForBoundedReachableEdgesViaIncoming:
                            reachableMethod = "EdgesIncoming";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                            reachableMethod = "EdgesOutgoing";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        default:
                            edgeMethod = theOther = iterationVariable = iterationType = "INTERNAL ERROR";
                            break;
                    }

                    string profilingArgument = gen.EmitProfiling ? ", procEnv" : "";
                    if(seqFor.SequenceType == SequenceType.ForBoundedReachableNodes || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}(node_{0}, depth_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else if(seqFor.SequenceType == SequenceType.ForBoundedReachableEdges || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}(node_{0}, depth_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
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

                case SequenceType.ForNodes:
                case SequenceType.ForEdges:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    if (seqFor.SequenceType == SequenceType.ForNodes)
                    {
                        SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                        string adjacentNodeTypeExpr = ExtractNodeType(source, AdjacentNodeType);
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode elem_{0} in graph.GetCompatibleNodes({1}))\n", seqFor.Id, adjacentNodeTypeExpr);
                    }
                    else
                    {
                        SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                        string incidentEdgeTypeExpr = ExtractEdgeType(source, IncidentEdgeType);
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge elem_{0} in graph.GetCompatibleEdges({1}))\n", seqFor.Id, incidentEdgeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();
                    
                    if(gen.EmitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(SetVar(seqFor.Var, "elem_" + seqFor.Id));

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
                    String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
                    String patternName = paramBindings.Name;
                    String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                    String matchName = "match_" + seqFor.Id;
                    String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                    String matchesName = "matches_" + seqFor.Id;
                    source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                        + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
                    for(int i=0; i<seqFor.Rule.Filters.Count; ++i)
                    {
                        EmitFilterCall(source, seqFor.Rule.Filters[i], patternName, matchesName);
                    }

                    source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                    source.Indent();
                    source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
                    source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                    if(gen.FireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

                    String returnParameterDeclarations;
                    String returnArguments;
                    String returnAssignments;
                    String returnParameterDeclarationsAllCall;
                    String intermediateReturnAssignmentsAllCall;
                    String returnAssignmentsAllCall;
                    BuildReturnParameters(paramBindings,
                        out returnParameterDeclarations, out returnArguments, out returnAssignments,
                        out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

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

                case SequenceType.ExecuteInSubgraph:
                {
                    SequenceExecuteInSubgraph seqExecInSub = (SequenceExecuteInSubgraph)seq;
                    string subgraph;
                    if(seqExecInSub.AttributeName == null)
                        subgraph = GetVar(seqExecInSub.SubgraphVar);
                    else
                    {
                        string element = "((GRGEN_LIBGR.IGraphElement)" + GetVar(seqExecInSub.SubgraphVar) + ")";
                        subgraph = element + ".GetAttribute(\"" + seqExecInSub.AttributeName + "\")";
                    }
                    source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + subgraph + ");\n");
                    source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
                    EmitSequence(seqExecInSub.Seq, source);
                    source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                    source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
                    source.AppendFront(SetResultVar(seqExecInSub, GetResultVar(seqExecInSub.Seq)));
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
            String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seq.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seq.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            for(int i=0; i<seq.Rule.Filters.Count; ++i)
            {
                EmitFilterCall(source, seq.Rule.Filters[i], patternName, matchesName);
            }

            source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seq, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seq, "true")); // shut up compiler
            source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(gen.FireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            BuildReturnParameters(paramBindings,
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
            source.AppendFront("int transID_" + seq.Id + " = procEnv.TransactionManager.Start();\n");
            source.AppendFront("int oldRewritesPerformed_" + seq.Id + " = procEnv.PerformanceInfo.RewritesPerformed;\n");
            if(gen.FireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", " + matchName + ", " + specialStr + ");\n");
            if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            if(gen.FireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            // rule applied, now execute the sequence
            EmitSequence(seq.Seq, source);

            // if sequence execution failed, roll the changes back and try the next match of the rule
            source.AppendFront("if(!" + GetResultVar(seq.Seq) + ") {\n");
            source.Indent();
            source.AppendFront("procEnv.TransactionManager.Rollback(transID_" + seq.Id + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed_" + seq.Id + ";\n");

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
                String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
                String patternName = paramBindings.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;
                source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                    + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                    + parameters + ");\n");
                for(int j=0; j<seqRule.Filters.Count; ++j)
                {
                    EmitFilterCall(source, seqRule.Filters[j], patternName, matchesName);
                }
                source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
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
                    else if (seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
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
                String returnParameterDeclarationsAllCall;
                String intermediateReturnAssignmentsAllCall;
                String returnAssignmentsAllCall;
                BuildReturnParameters(paramBindings,
                    out returnParameterDeclarations, out returnArguments, out returnAssignments,
                    out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

                if (seqRule.SequenceType == SequenceType.RuleCall)
                {
                    if (seqSome.Random) {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                    if (gen.FireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (gen.FireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");

                    if (seqSome.Random) {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else if (seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                {
                    if (seqSome.Random)
                    {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    // iterate through matches, use Modify on each, fire the next match event after the first
                    if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                    String enumeratorName = "enum_" + seqRule.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    if (gen.FireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (gen.FireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                    source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                    if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
                    if(seqRule.SequenceType == SequenceType.RuleCountAllCall)
                    {
                        SequenceRuleCountAllCall ruleCountAll = (SequenceRuleCountAllCall)seqRule;
                        source.AppendFront(SetVar(ruleCountAll.CountResult, matchesName + ".Count"));
                    }

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
                        if (gen.FireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (gen.FireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
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
                        if (gen.FireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (gen.FireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                    }
                }

                if (gen.FireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        void EmitFilterCall(SourceBuilder source, FilterCall filterCall, string patternName, string matchesName)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                switch(filterCall.Name)
                {
                    case "keepFirst":
                        source.AppendFrontFormat("{0}.FilterKeepFirst((int)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepLast":
                        source.AppendFrontFormat("{0}.FilterKeepLast((int)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepFirstFraction":
                        source.AppendFrontFormat("{0}.FilterKeepFirstFraction((double)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepLastFraction":
                        source.AppendFrontFormat("{0}.FilterKeepLastFraction((double)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeFirst":
                        source.AppendFrontFormat("{0}.FilterRemoveFirst((int)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeLast":
                        source.AppendFrontFormat("{0}.FilterRemoveLast((int)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeFirstFraction":
                        source.AppendFrontFormat("{0}.FilterRemoveFirstFraction((double)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeLastFraction":
                        source.AppendFrontFormat("{0}.FilterRemoveLastFraction((double)({1}));\n",
                            matchesName, GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                }
            }
            else
            {
                if(filterCall.IsAutoGenerated && filterCall.Name == "auto")
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}(procEnv, {3});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, matchesName);
                else if(filterCall.IsAutoGenerated)
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}_{3}(procEnv, {4});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, filterCall.Entity, matchesName);
                else
                {
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}(procEnv, {2}",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), filterCall.Name, matchesName);
                    for(int i = 0; i < filterCall.ArgumentExpressions.Length; ++i)
                    {
                        source.AppendFormat(", ({0})({1})",
                            TypesHelper.XgrsTypeToCSharpType(filterFunctionsToInputTypes[filterCall.Name][i], model),
                            GetSequenceExpression(filterCall.ArgumentExpressions[i], source));
                    } 
                    source.Append(");\n");
                }
            }
        }

  		void EmitSequenceComputation(SequenceComputation seqComp, SourceBuilder source)
		{
            // take care that the operations returning a value are emitted similarily to expressions,
            // whereas the operations returning no value are emitted as statements
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
                            if(seqAdd.Attribute != null)
                            {
                                if(gen.FireDebugEvents)
                                {
                                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                }
                            }
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
                            if(seqAdd.Attribute != null)
                            {
                                if(gen.FireDebugEvents)
                                {
                                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                }
                            }
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
                        if(seqAdd.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                            }
                        }

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
                        if(seqAdd.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                            }
                        }
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
                        if(seqAdd.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                            }
                        }
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
                        if(seqAdd.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                            }
                        }
                        source.AppendFront(SetResultVar(seqAdd, container));
                    }
                    break;
                }

                case SequenceComputationType.ContainerRem:
                {
                    SequenceComputationContainerRem seqDel = (SequenceComputationContainerRem)seqComp;

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

                        if(sourceValue != null && !TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                            source.AppendFront("throw new Exception(\"Can't remove non-int index from array\");\n");
                        else
                        {
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
                                source.AppendFront(array + ".RemoveAt(" + sourceValue + ");\n");
                            if(seqDel.Attribute != null)
                            {
                                if(gen.FireDebugEvents)
                                {
                                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                }
                            }
                        }

                        source.Unindent();
                        source.AppendFront("} else if(" + containerVar + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        if(sourceValue != null && !TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                            source.AppendFront("throw new Exception(\"Can't remove non-int index from deque\");\n");
                        else
                        {
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
                                source.AppendFront(deque + ".DequeueAt(" + sourceValue + ");\n");
                            if(seqDel.Attribute != null)
                            {
                                if(gen.FireDebugEvents)
                                {
                                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                    source.AppendFront("else\n");
                                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                }
                            }
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
                        if(seqDel.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                            }
                        }

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
                        if(seqDel.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                            }
                        }
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
                        if(seqDel.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                            }
                        }
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
                        if(seqDel.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                            }
                        }
                        source.AppendFront(SetResultVar(seqDel, container));
                    }
                    break;
                }

                case SequenceComputationType.ContainerClear:
                {
                    SequenceComputationContainerClear seqClear = (SequenceComputationContainerClear)seqComp;

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
                        if(seqClear.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                                source.AppendFront("\telse\n");
                                source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                            }
                        }

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
                        if(seqClear.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                                source.AppendFront("\telse\n");
                                source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                            }
                        }

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
                        if(seqClear.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                                source.AppendFront("\telse\n");
                                source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                            }
                        }

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
                        if(seqClear.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                                source.AppendFront("\telse\n");
                                source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                            }
                        }
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
                        if(seqClear.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                                source.AppendFront("\telse\n");
                                source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                            }
                        }
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
                        if(seqClear.Attribute != null)
                        {
                            if(gen.FireDebugEvents)
                            {
                                source.AppendFront("if(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                                source.AppendFront("else\n");
                                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                            }
                        }
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

                case SequenceComputationType.DebugAdd:
                {
                    SequenceComputationDebugAdd seqDebug = (SequenceComputationDebugAdd)seqComp;
                    source.AppendFront("procEnv.DebugEntering(");
                    for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
                    {
                        if(i == 0)
                            source.Append("(string)");
                        else
                            source.Append(", ");
                        source.Append(GetSequenceExpression(seqDebug.ArgExprs[i], source));
                    }
                    source.Append(");\n");
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugRem:
                {
                    SequenceComputationDebugRem seqDebug = (SequenceComputationDebugRem)seqComp;
                    source.AppendFront("procEnv.DebugExiting(");
                    for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
                    {
                        if(i == 0)
                            source.Append("(string)");
                        else
                            source.Append(", ");
                        source.Append(GetSequenceExpression(seqDebug.ArgExprs[i], source));
                    }
                    source.Append(");\n");
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugEmit:
                {
                    SequenceComputationDebugEmit seqDebug = (SequenceComputationDebugEmit)seqComp;
                    source.AppendFront("procEnv.DebugEmitting(");
                    for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
                    {
                        if(i == 0)
                            source.Append("(string)");
                        else
                            source.Append(", ");
                        source.Append(GetSequenceExpression(seqDebug.ArgExprs[i], source));
                    }
                    source.Append(");\n");
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugHalt:
                {
                    SequenceComputationDebugHalt seqDebug = (SequenceComputationDebugHalt)seqComp;
                    source.AppendFront("procEnv.DebugHalting(");
                    for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
                    {
                        if(i == 0)
                            source.Append("(string)");
                        else
                            source.Append(", ");
                        source.Append(GetSequenceExpression(seqDebug.ArgExprs[i], source));
                    }
                    source.Append(");\n");
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugHighlight:
                {
                    SequenceComputationDebugHighlight seqDebug = (SequenceComputationDebugHighlight)seqComp;
                    source.AppendFront("List<object> values = new List<object>();\n");
                    source.AppendFront("List<string> annotations = new List<string>();\n");
                    for(int i = 1; i < seqDebug.ArgExprs.Count; ++i)
                    {
                        if(i % 2 == 1)
                            source.AppendFront("values.Add(" + GetSequenceExpression(seqDebug.ArgExprs[i], source) + ");\n");
                        else
                            source.AppendFront("annotations.Add((string)" + GetSequenceExpression(seqDebug.ArgExprs[i], source) + ");\n");
                    }
                    source.AppendFront("procEnv.DebugHighlighting(" + GetSequenceExpression(seqDebug.ArgExprs[0], source) + ", values, annotations);\n");
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.Emit:
                {
                    SequenceComputationEmit seqEmit = (SequenceComputationEmit)seqComp;
                    bool declarationEmitted = false;
                    for(int i = 0; i < seqEmit.Expressions.Count; ++i)
                    {
                        if(!(seqEmit.Expressions[i] is SequenceExpressionConstant))
                        {
                            string emitVal = "emitval_" + seqEmit.Id;
                            if(!declarationEmitted) {
                                source.AppendFront("object " + emitVal + ";\n");
                                declarationEmitted = true;
                            }
                            source.AppendFront(emitVal + " = " + GetSequenceExpression(seqEmit.Expressions[i], source) + ";\n");
                            if(seqEmit.Expressions[i].Type(env) == ""
                                || seqEmit.Expressions[i].Type(env).StartsWith("set<") || seqEmit.Expressions[i].Type(env).StartsWith("map<")
                                || seqEmit.Expressions[i].Type(env).StartsWith("array<") || seqEmit.Expressions[i].Type(env).StartsWith("deque<"))
                            {
                                source.AppendFront("if(" + emitVal + " is IDictionary)\n");
                                source.AppendFront("\tprocEnv.EmitWriter.Write(GRGEN_LIBGR.EmitHelper.ToString((IDictionary)" + emitVal + ", graph));\n");
                                source.AppendFront("else if(" + emitVal + " is IList)\n");
                                source.AppendFront("\tprocEnv.EmitWriter.Write(GRGEN_LIBGR.EmitHelper.ToString((IList)" + emitVal + ", graph));\n");
                                source.AppendFront("else if(" + emitVal + " is GRGEN_LIBGR.IDeque)\n");
                                source.AppendFront("\tprocEnv.EmitWriter.Write(GRGEN_LIBGR.EmitHelper.ToString((GRGEN_LIBGR.IDeque)" + emitVal + ", graph));\n");
                                source.AppendFront("else\n\t");
                            }
                            source.AppendFront("procEnv.EmitWriter.Write(GRGEN_LIBGR.EmitHelper.ToString(" + emitVal + ", graph));\n");
                        }
                        else
                        {
                            SequenceExpressionConstant constant = (SequenceExpressionConstant)seqEmit.Expressions[i];
                            if(constant.Constant is string)
                            {
                                String text = (string)constant.Constant;
                                text = text.Replace("\n", "\\n");
                                text = text.Replace("\r", "\\r");
                                text = text.Replace("\t", "\\t");
                                source.AppendFront("procEnv.EmitWriter.Write(\"" + text + "\");\n");
                            }
                            else
                                source.AppendFront("procEnv.EmitWriter.Write(GRGEN_LIBGR.EmitHelper.ToString(" + GetSequenceExpression(seqEmit.Expressions[i], source) + ", graph));\n");
                        }
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
                            source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((IDictionary)" + recVal + ", graph));\n");
                            source.AppendFront("else if(" + recVal + " is IList)\n");
                            source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((IList)" + recVal + ", graph));\n");
                            source.AppendFront("else if(" + recVal + " is GRGEN_LIBGR.IDeque)\n");
                            source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((GRGEN_LIBGR.IDeque)" + recVal + ", graph));\n");
                            source.AppendFront("else\n\t");
                        }
                        source.AppendFront("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString(" + recVal + ", graph));\n");
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
                            source.AppendFront("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString(" + GetSequenceExpression(seqRec.Expression, source) + ", graph));\n");
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

                case SequenceComputationType.DeleteFile:
                {
                    SequenceComputationDeleteFile seqDelFile = (SequenceComputationDeleteFile)seqComp;
                    string delFileName = "delfilename_" + seqDelFile.Id;
                    source.AppendFront("object " + delFileName + " = " + GetSequenceExpression(seqDelFile.Name, source) + ";\n");
                    source.AppendFront("\tSystem.IO.File.Delete((string)" + delFileName + ");\n");
                    source.AppendFront(SetResultVar(seqDelFile, "null"));
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
                    string seqRemExpr = GetSequenceExpression(seqRem.Expr, source);
                    if(seqRem.Expr.Type(env) == "")
                    {
                        source.AppendFront("GRGEN_LIBGR.IGraphElement " + remVal + " = (GRGEN_LIBGR.IGraphElement)" + seqRemExpr + ";\n");
                        source.AppendFront("if(" + remVal + " is GRGEN_LIBGR.IEdge)\n");
                        source.AppendFront("\tgraph.Remove((GRGEN_LIBGR.IEdge)" + remVal + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\t{graph.RemoveEdges((GRGEN_LIBGR.INode)" + remVal + "); graph.Remove((GRGEN_LIBGR.INode)" + remVal + ");}\n");
                    }
                    else
                    {
                        if(TypesHelper.IsSameOrSubtype(seqRem.Expr.Type(env), "Node", model))
                        {
                            source.AppendFront("GRGEN_LIBGR.INode " + remVal + " = (GRGEN_LIBGR.INode)" + seqRemExpr + ";\n");
                            source.AppendFront("graph.RemoveEdges(" + remVal + "); graph.Remove(" + remVal + ");\n");
                        }
                        else if(TypesHelper.IsSameOrSubtype(seqRem.Expr.Type(env), "AEdge", model))
                        {
                            source.AppendFront("GRGEN_LIBGR.IEdge " + remVal + " = (GRGEN_LIBGR.IEdge)" + seqRemExpr + ";\n");
                            source.AppendFront("\tgraph.Remove(" + remVal + ");\n");
                        }
                        else
                            source.AppendFront("throw new Exception(\"rem() on non-node/edge\");\n");
                    }
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

                case SequenceComputationType.GraphAddCopy:
                {
                    SequenceComputationGraphAddCopy seqAddCopy = (SequenceComputationGraphAddCopy)seqComp;
                    if(seqAddCopy.ExprSrc == null)
                    {
                        string nodeExpr = GetSequenceExpression(seqAddCopy.Expr, source);
                        source.Append("GRGEN_LIBGR.GraphHelper.AddCopyOfNode(" + nodeExpr + ", graph)");
                    }
                    else
                    {
                        string edgeExpr = GetSequenceExpression(seqAddCopy.Expr, source);
                        string srcExpr = GetSequenceExpression(seqAddCopy.ExprSrc, source);
                        string tgtExpr = GetSequenceExpression(seqAddCopy.ExprDst, source);
                        source.Append("GRGEN_LIBGR.GraphHelper.AddCopyOfEdge(" + edgeExpr + ", (GRGEN_LIBGR.INode)" + srcExpr + ", (GRGEN_LIBGR.INode)" + tgtExpr + ", graph)");
                    }
                    break;
                }

                case SequenceComputationType.GraphMerge:
                {
                    SequenceComputationGraphMerge seqMrg = (SequenceComputationGraphMerge)seqComp;
                    string tgtNodeExpr = GetSequenceExpression(seqMrg.TargetNodeExpr, source);
                    string srcNodeExpr = GetSequenceExpression(seqMrg.SourceNodeExpr, source);
                    source.AppendFrontFormat("graph.Merge((GRGEN_LIBGR.INode){0}, (GRGEN_LIBGR.INode){1}, \"merge\");\n", tgtNodeExpr, srcNodeExpr);
                    source.AppendFront(SetResultVar(seqMrg, "null"));
                    break;
                }
                
                case SequenceComputationType.GraphRedirectSource:
                {
                    SequenceComputationGraphRedirectSource seqRedir = (SequenceComputationGraphRedirectSource)seqComp;
                    string edgeExpr = GetSequenceExpression(seqRedir.EdgeExpr, source);
                    string srcNodeExpr = GetSequenceExpression(seqRedir.SourceNodeExpr, source);
                    source.AppendFrontFormat("graph.RedirectSource((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old source\");\n", edgeExpr, srcNodeExpr);
                    source.AppendFront(SetResultVar(seqRedir, "null"));
                    break;
                }

                case SequenceComputationType.GraphRedirectTarget:
                {
                    SequenceComputationGraphRedirectTarget seqRedir = (SequenceComputationGraphRedirectTarget)seqComp;
                    string edgeExpr = GetSequenceExpression(seqRedir.EdgeExpr, source);
                    string tgtNodeExpr = GetSequenceExpression(seqRedir.TargetNodeExpr, source);
                    source.AppendFrontFormat("graph.RedirectTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old target\");\n", edgeExpr, tgtNodeExpr);
                    source.AppendFront(SetResultVar(seqRedir, "null"));
                    break;
                }

                case SequenceComputationType.GraphRedirectSourceAndTarget:
                {
                    SequenceComputationGraphRedirectSourceAndTarget seqRedir = (SequenceComputationGraphRedirectSourceAndTarget)seqComp;
                    string edgeExpr = GetSequenceExpression(seqRedir.EdgeExpr, source);
                    string srcNodeExpr = GetSequenceExpression(seqRedir.SourceNodeExpr, source);
                    string tgtNodeExpr = GetSequenceExpression(seqRedir.TargetNodeExpr, source);
                    source.AppendFrontFormat("graph.RedirectSourceAndTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, (GRGEN_LIBGR.INode){2}, \"old source\", \"old target\");\n", edgeExpr, srcNodeExpr, tgtNodeExpr);
                    source.AppendFront(SetResultVar(seqRedir, "null"));
                    break;
                }

                case SequenceComputationType.Insert:
                {
                    SequenceComputationInsert seqIns = (SequenceComputationInsert)seqComp;
                    string graphExpr = GetSequenceExpression(seqIns.Graph, source);
                    source.AppendFrontFormat("GRGEN_LIBGR.GraphHelper.Insert((GRGEN_LIBGR.IGraph){0}, graph);\n", graphExpr);
                    source.AppendFront(SetResultVar(seqIns, "null"));
                    break;
                }

                case SequenceComputationType.InsertCopy:
                {
                    SequenceComputationInsertCopy seqInsCopy = (SequenceComputationInsertCopy)seqComp;
                    string graphExpr = GetSequenceExpression(seqInsCopy.Graph, source);
                    string rootNodeExpr = GetSequenceExpression(seqInsCopy.RootNode, source);
                    source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertCopy((GRGEN_LIBGR.IGraph){0}, (GRGEN_LIBGR.INode){1}, graph)", graphExpr, rootNodeExpr);
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
                    if(seqInsDef.EdgeSet.Type(env)=="set<Edge>")
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefinedDirected((IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IDEdge)" + GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    else if (seqInsDef.EdgeSet.Type(env) == "set<UEdge>")
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefinedUndirected((IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IUEdge)" + GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    else if (seqInsDef.EdgeSet.Type(env) == "set<AEdge>")
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    else
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary)" + GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
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

                    if(seqCall.IsExternalProcedureCalled)
                        source.AppendFront("GRGEN_EXPR.ExternalProcedures.");
                    else
                        source.AppendFrontFormat("GRGEN_ACTIONS.{0}Procedures.", TypesHelper.GetPackagePrefixDot(seqCall.ParamBindings.Package));
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

                case SequenceComputationType.ProcedureMethodCall:
                {
                    SequenceComputationProcedureMethodCall seqCall = (SequenceComputationProcedureMethodCall)seqComp;
                    String type = seqCall.TargetExpr != null ? seqCall.TargetExpr.Type(env) : seqCall.TargetVar.Type;
                    if(type == "")
                    {
                        string tmpVarName = "tmpvar_" + tmpVarCtr.ToString();
                        ++tmpVarCtr;
                        source.AppendFront("object[] " + tmpVarName + " = ");
                        source.Append("((GRGEN_LIBGR.IGraphElement)");
                        if(seqCall.TargetExpr != null)
                            source.Append(GetSequenceExpression(seqCall.TargetExpr, source));
                        else
                            source.Append(GetVar(seqCall.TargetVar));
                        source.Append(").ApplyProcedureMethod(procEnv, graph, ");
                        source.Append("\"" + seqCall.ParamBindings.Name + "\"");
                        source.Append(BuildParametersInObject(seqCall.ParamBindings));
                        source.Append(");\n");
                        for(int i = 0; i < seqCall.ParamBindings.ReturnVars.Length; i++)
                            source.Append(SetVar(seqCall.ParamBindings.ReturnVars[i], tmpVarName));
                    }
                    else
                    {
                        String returnParameterDeclarations;
                        String returnArguments;
                        String returnAssignments;
                        BuildReturnParameters(seqCall.ParamBindings, TypesHelper.GetNodeOrEdgeType(type, model), out returnParameterDeclarations, out returnArguments, out returnAssignments);

                        if(returnParameterDeclarations.Length != 0)
                            source.AppendFront(returnParameterDeclarations + "\n");

                        source.AppendFront("((");
                        source.Append(TypesHelper.XgrsTypeToCSharpType(type, model));
                        source.Append(")");
                        if(seqCall.TargetExpr != null)
                            source.Append(GetSequenceExpression(seqCall.TargetExpr, source));
                        else
                            source.Append(GetVar(seqCall.TargetVar));
                        source.Append(").");
                        source.Append(seqCall.ParamBindings.Name);
                        source.Append("(procEnv, graph");
                        source.Append(BuildParameters(seqCall.ParamBindings, TypesHelper.GetNodeOrEdgeType(type, model).GetProcedureMethod(seqCall.ParamBindings.Name)));
                        source.Append(returnArguments);
                        source.Append(");\n");
                    }
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
                    if(gen.FireDebugEvents)
                    {
                        source.AppendFront("if(elem_" + tgtAttr.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ");\n");
                    }
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
                    if(gen.FireDebugEvents)
                    {
                        source.AppendFront("if(elem_" + tgtAttrIndexedVar.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ");\n");
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
                    if(rulesToInputTypes.ContainsKey(paramBindings.PackagePrefixedName))
                        typeName = rulesToInputTypes[paramBindings.PackagePrefixedName][i];
                    else if(sequencesToInputTypes.ContainsKey(paramBindings.PackagePrefixedName))
                        typeName = sequencesToInputTypes[paramBindings.PackagePrefixedName][i];
                    else if(proceduresToInputTypes.ContainsKey(paramBindings.PackagePrefixedName))
                        typeName = proceduresToInputTypes[paramBindings.PackagePrefixedName][i];
                    else
                        typeName = functionsToInputTypes[paramBindings.PackagePrefixedName][i];
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

        private String BuildParameters(InvocationParameterBindings paramBindings, IFunctionDefinition functionMethod)
        {
            String parameters = "";
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    String typeName = TypesHelper.DotNetTypeToXgrsType(functionMethod.Inputs[i]);
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

        private String BuildParameters(InvocationParameterBindings paramBindings, IProcedureDefinition procedureMethod)
        {
            String parameters = "";
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    String typeName = TypesHelper.DotNetTypeToXgrsType(procedureMethod.Inputs[i]);
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

        private String BuildParametersInObject(InvocationParameterBindings paramBindings)
        {
            String parameters = ", new object[] { ";
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    parameters += ", " + GetSequenceExpression(paramBindings.ArgumentExpressions[i], null);
                }
                else
                {
                    // the sequence parser always emits all argument expressions, for interpreted and compiled
                    throw new Exception("Internal error: missing argument expressions");
                }
            }
            return parameters + " }";
        }

        private String BuildParametersInDeclarations(InvocationParameterBindings paramBindings, out String declarations)
        {
            String parameters = "";
            declarations = "";
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    String typeName;
                    if(rulesToInputTypes.ContainsKey(paramBindings.PackagePrefixedName))
                        typeName = rulesToInputTypes[paramBindings.PackagePrefixedName][i];
                    else 
                        typeName = sequencesToInputTypes[paramBindings.PackagePrefixedName][i];
                    String type = TypesHelper.XgrsTypeToCSharpType(typeName, model);
                    String name = "tmpvar_" + tmpVarCtr.ToString();
                    ++tmpVarCtr;
                    declarations += type + " " + name + " = " + "(" + type + ")" + GetSequenceExpression(paramBindings.ArgumentExpressions[i], null) + ";";
                    parameters += ", " + name;
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
            for(int i = 0; i < sequencesToOutputTypes[paramBindings.PackagePrefixedName].Count; i++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0)
                    varName = tmpVarCtr.ToString() + paramBindings.ReturnVars[i].PureName;
                else
                    varName = tmpVarCtr.ToString();
                ++tmpVarCtr;
                String typeName = sequencesToOutputTypes[paramBindings.PackagePrefixedName][i];
                outParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName
                    + " = " + TypesHelper.DefaultValueString(typeName, model) + ";";
                outArguments += ", ref tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    outAssignments += SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName);
            }
        }

        private void BuildReturnParameters(RuleInvocationParameterBindings paramBindings, 
            out String returnParameterDeclarations, out String returnArguments, out String returnAssignments,
            out String returnParameterDeclarationsAllCall, out String intermediateReturnAssignmentsAllCall, out String returnAssignmentsAllCall)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            StringBuilder sbReturnParameterDeclarations = new StringBuilder();
            StringBuilder sbReturnArguments = new StringBuilder();
            StringBuilder sbReturnAssignments = new StringBuilder();
            StringBuilder sbReturnParameterDeclarationsAllCall = new StringBuilder();
            StringBuilder sbIntermediateReturnAssignmentsAllCall = new StringBuilder();
            StringBuilder sbReturnAssignmentsAllCall = new StringBuilder();

            for(int i = 0; i < rulesToOutputTypes[paramBindings.PackagePrefixedName].Count; i++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0)
                    varName = tmpVarCtr.ToString() + paramBindings.ReturnVars[i].PureName;
                else
                    varName = tmpVarCtr.ToString();
                ++tmpVarCtr;
                String typeName = rulesToOutputTypes[paramBindings.PackagePrefixedName][i];
                
                sbReturnParameterDeclarations.Append(TypesHelper.XgrsTypeToCSharpType(typeName, model));
                sbReturnParameterDeclarations.Append(" tmpvar_");
                sbReturnParameterDeclarations.Append(varName);
                sbReturnParameterDeclarations.Append("; ");

                String returnListValueVarType = typeName;
                if(paramBindings.ReturnVars.Length != 0 && paramBindings.ReturnVars[i].Type != "" && paramBindings.ReturnVars[i].Type.StartsWith("array<"))
                    returnListValueVarType = TypesHelper.ExtractSrc(paramBindings.ReturnVars[i].Type);
                if(paramBindings.ReturnVars.Length != 0)
                {
                    sbReturnParameterDeclarationsAllCall.Append("List<");
                    sbReturnParameterDeclarationsAllCall.Append(TypesHelper.XgrsTypeToCSharpType(returnListValueVarType, model));
                    sbReturnParameterDeclarationsAllCall.Append("> tmpvarlist_");
                    sbReturnParameterDeclarationsAllCall.Append(varName);
                    sbReturnParameterDeclarationsAllCall.Append(" = new List<");
                    sbReturnParameterDeclarationsAllCall.Append(TypesHelper.XgrsTypeToCSharpType(returnListValueVarType, model));
                    sbReturnParameterDeclarationsAllCall.Append(">(); ");
                }

                sbReturnArguments.Append(", out tmpvar_");
                sbReturnArguments.Append(varName);

                if(paramBindings.ReturnVars.Length != 0)
                {
                    sbReturnAssignments.Append(SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName));

                    sbIntermediateReturnAssignmentsAllCall.Append("tmpvarlist_");
                    sbIntermediateReturnAssignmentsAllCall.Append(varName);
                    sbIntermediateReturnAssignmentsAllCall.Append(".Add((");
                    sbIntermediateReturnAssignmentsAllCall.Append(TypesHelper.XgrsTypeToCSharpType(returnListValueVarType, model));
                    sbIntermediateReturnAssignmentsAllCall.Append(")tmpvar_");
                    sbIntermediateReturnAssignmentsAllCall.Append(varName);
                    sbIntermediateReturnAssignmentsAllCall.Append("); ");
                    
                    sbReturnAssignmentsAllCall.Append(SetVar(paramBindings.ReturnVars[i], "tmpvarlist_" + varName));
                }
            }

            returnParameterDeclarations = sbReturnParameterDeclarations.ToString();
            returnArguments = sbReturnArguments.ToString();
            returnAssignments = sbReturnAssignments.ToString();
            returnParameterDeclarationsAllCall = sbReturnParameterDeclarationsAllCall.ToString();
            intermediateReturnAssignmentsAllCall = sbIntermediateReturnAssignmentsAllCall.ToString();
            returnAssignmentsAllCall = sbReturnAssignmentsAllCall.ToString();
        }

        private void BuildReturnParameters(ProcedureInvocationParameterBindings paramBindings, out String returnParameterDeclarations, out String returnArguments, out String returnAssignments)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            returnParameterDeclarations = "";
            returnArguments = "";
            returnAssignments = "";
            for(int i = 0; i < proceduresToOutputTypes[paramBindings.PackagePrefixedName].Count; i++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0)
                    varName = tmpVarCtr.ToString() + paramBindings.ReturnVars[i].PureName;
                else
                    varName = tmpVarCtr.ToString();
                ++tmpVarCtr;
                String typeName = proceduresToOutputTypes[paramBindings.PackagePrefixedName][i];
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName + "; ";
                returnArguments += ", out tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    returnAssignments += SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName);
            }
        }

        private void BuildReturnParameters(ProcedureInvocationParameterBindings paramBindings, GrGenType ownerType, out String returnParameterDeclarations, out String returnArguments, out String returnAssignments)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            returnParameterDeclarations = "";
            returnArguments = "";
            returnAssignments = "";
            for(int i = 0; i < ownerType.GetProcedureMethod(paramBindings.Name).Outputs.Length; i++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0)
                    varName = tmpVarCtr.ToString() + paramBindings.ReturnVars[i].PureName;
                else
                    varName = tmpVarCtr.ToString();
                ++tmpVarCtr;
                String typeName = TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(paramBindings.Name).Outputs[i]);
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName + "; ";
                returnArguments += ", out tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    returnAssignments += SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName);
            }
        }


        string GetContainerValue(SequenceComputationContainer container)
        {
            if(container.Container != null)
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
                        return SequenceExpressionHelper.EqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.EqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                            + leftType + ", " + rightType + ", graph)";
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
                        return SequenceExpressionHelper.NotEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.NotEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.NotEqual, " + leftType + ", " + rightType + ", graph.Model), "
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Lower:
                {
                    SequenceExpressionLower seq = (SequenceExpressionLower)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.LowerStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.LowerObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Lower, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Greater:
                {
                    SequenceExpressionGreater seq = (SequenceExpressionGreater)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.GreaterStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.GreaterObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Greater, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.LowerEqual:
                {
                    SequenceExpressionLowerEqual seq = (SequenceExpressionLowerEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.LowerEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.LowerEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.LowerEqual, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.GreaterEqual:
                {
                    SequenceExpressionGreaterEqual seq = (SequenceExpressionGreaterEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.GreaterEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.GreaterEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.GreaterEqual, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Plus:
                {
                    SequenceExpressionPlus seq = (SequenceExpressionPlus)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.PlusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
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
                        return SequenceExpressionHelper.MinusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.MinusObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Minus, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Mul:
                {
                    SequenceExpressionMul seq = (SequenceExpressionMul)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.MulStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.MulObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Mul, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Div:
                {
                    SequenceExpressionDiv seq = (SequenceExpressionDiv)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.DivStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.DivObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Div, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Mod:
                {
                    SequenceExpressionMod seq = (SequenceExpressionMod)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelper.ModStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.ModObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Mod, " + leftType + ", " + rightType + ", graph.Model),"
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
                    string profilingArgument = seqNodes.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.Nodes(graph, (GRGEN_LIBGR.NodeType)" + nodeType + profilingArgument + ")";
                }

                case SequenceExpressionType.Edges:
                {
                    SequenceExpressionEdges seqEdges = (SequenceExpressionEdges)expr;
                    string edgeType = ExtractEdgeType(source, seqEdges.EdgeType);
                    string edgeRootType = SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqEdges.EdgeType, env);
                    string directedness = GetDirectedness(edgeRootType);
                    string profilingArgument = seqEdges.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.Edges" + directedness + "(graph, (GRGEN_LIBGR.EdgeType)" + edgeType + profilingArgument + ")";
                }

                case SequenceExpressionType.CountNodes:
                {
                    SequenceExpressionCountNodes seqNodes = (SequenceExpressionCountNodes)expr;
                    string nodeType = ExtractNodeType(source, seqNodes.NodeType);
                    string profilingArgument = seqNodes.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.CountNodes(graph, (GRGEN_LIBGR.NodeType)" + nodeType + profilingArgument + ")";
                }

                case SequenceExpressionType.CountEdges:
                {
                    SequenceExpressionCountEdges seqEdges = (SequenceExpressionCountEdges)expr;
                    string edgeType = ExtractEdgeType(source, seqEdges.EdgeType);
                    string profilingArgument = seqEdges.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.CountEdges(graph, (GRGEN_LIBGR.EdgeType)" + edgeType + profilingArgument + ")";
                }

                case SequenceExpressionType.Now:
                {
                    SequenceExpressionNow seqNow = (SequenceExpressionNow)expr;
                    return "DateTime.UtcNow.ToFileTime()";
                }

                case SequenceExpressionType.Empty:
                {
                    SequenceExpressionEmpty seqEmpty = (SequenceExpressionEmpty)expr;
                    return "(graph.NumNodes+graph.NumEdges==0)";
                }

                case SequenceExpressionType.Size:
                {
                    SequenceExpressionSize seqSize = (SequenceExpressionSize)expr;
                    return "(graph.NumNodes+graph.NumEdges)";
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
                    string directedness = GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqAdjInc.EdgeType, env));
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
                            function = "Incident" + directedness; break;
                        case SequenceExpressionType.IncomingEdges:
                            function = "Incoming" + directedness; break;
                        case SequenceExpressionType.OutgoingEdges:
                            function = "Outgoing" + directedness; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqAdjInc.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                        + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                }

                case SequenceExpressionType.CountAdjacentNodes:
                case SequenceExpressionType.CountAdjacentNodesViaIncoming:
                case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
                case SequenceExpressionType.CountIncidentEdges:
                case SequenceExpressionType.CountIncomingEdges:
                case SequenceExpressionType.CountOutgoingEdges:
                {
                    SequenceExpressionCountAdjacentIncident seqCntAdjInc = (SequenceExpressionCountAdjacentIncident)expr;
                    string sourceNode = GetSequenceExpression(seqCntAdjInc.SourceNode, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqCntAdjInc.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqCntAdjInc.OppositeNodeType);
                    string function;
                    switch(seqCntAdjInc.SequenceExpressionType)
                    {
                        case SequenceExpressionType.CountAdjacentNodes:
                            function = "CountAdjacent"; break;
                        case SequenceExpressionType.CountAdjacentNodesViaIncoming:
                            function = "CountAdjacentIncoming"; break;
                        case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
                            function = "CountAdjacentOutgoing"; break;
                        case SequenceExpressionType.CountIncidentEdges:
                            function = "CountIncident"; break;
                        case SequenceExpressionType.CountIncomingEdges:
                            function = "CountIncoming"; break;
                        case SequenceExpressionType.CountOutgoingEdges:
                            function = "CountOutgoing"; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqCntAdjInc.EmitProfiling ? ", procEnv" : "";
                    if(seqCntAdjInc.SequenceExpressionType == SequenceExpressionType.CountAdjacentNodes
                        || seqCntAdjInc.SequenceExpressionType == SequenceExpressionType.CountAdjacentNodesViaIncoming
                        || seqCntAdjInc.SequenceExpressionType == SequenceExpressionType.CountAdjacentNodesViaOutgoing)
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                    else // SequenceExpressionType.CountIncidentEdges || SequenceExpressionType.CountIncomingEdges || SequenceExpressionType.CountOutgoingEdges
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
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
                    string directedness = GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqReach.EdgeType, env));
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
                            function = "ReachableEdges" + directedness; break;
                        case SequenceExpressionType.ReachableEdgesViaIncoming:
                            function = "ReachableEdgesIncoming" + directedness; break;
                        case SequenceExpressionType.ReachableEdgesViaOutgoing:
                            function = "ReachableEdgesOutgoing" + directedness; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqReach.EmitProfiling ? ", procEnv" : "";
                    if(seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodes
                        || seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodesViaIncoming
                        || seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodesViaOutgoing)
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                    else // SequenceExpressionType.ReachableEdges || SequenceExpressionType.ReachableEdgesViaIncoming || SequenceExpressionType.ReachableEdgesViaOutgoing
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                }

                case SequenceExpressionType.CountReachableNodes:
                case SequenceExpressionType.CountReachableNodesViaIncoming:
                case SequenceExpressionType.CountReachableNodesViaOutgoing:
                case SequenceExpressionType.CountReachableEdges:
                case SequenceExpressionType.CountReachableEdgesViaIncoming:
                case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                {
                    SequenceExpressionCountReachable seqCntReach = (SequenceExpressionCountReachable)expr;
                    string sourceNode = GetSequenceExpression(seqCntReach.SourceNode, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqCntReach.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqCntReach.OppositeNodeType);
                    string function;
                    switch(seqCntReach.SequenceExpressionType)
                    {
                        case SequenceExpressionType.CountReachableNodes:
                            function = "CountReachable"; break;
                        case SequenceExpressionType.CountReachableNodesViaIncoming:
                            function = "CountReachableIncoming"; break;
                        case SequenceExpressionType.CountReachableNodesViaOutgoing:
                            function = "CountReachableOutgoing"; break;
                        case SequenceExpressionType.CountReachableEdges:
                            function = "CountReachableEdges"; break;
                        case SequenceExpressionType.CountReachableEdgesViaIncoming:
                            function = "CountReachableEdgesIncoming"; break;
                        case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                            function = "CountReachableEdgesOutgoing"; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqCntReach.EmitProfiling ? ", procEnv" : "";
                    if(seqCntReach.SequenceExpressionType == SequenceExpressionType.CountReachableNodes
                        || seqCntReach.SequenceExpressionType == SequenceExpressionType.CountReachableNodesViaIncoming
                        || seqCntReach.SequenceExpressionType == SequenceExpressionType.CountReachableNodesViaOutgoing)
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                    else // SequenceExpressionType.CountReachableEdges || SequenceExpressionType.CountReachableEdgesViaIncoming || SequenceExpressionType.CountReachableEdgesViaOutgoing
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                }

                case SequenceExpressionType.BoundedReachableNodes:
                case SequenceExpressionType.BoundedReachableNodesViaIncoming:
                case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
                case SequenceExpressionType.BoundedReachableEdges:
                case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
                case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                {
                    SequenceExpressionBoundedReachable seqBoundReach = (SequenceExpressionBoundedReachable)expr;
                    string sourceNode = GetSequenceExpression(seqBoundReach.SourceNode, source);
                    string depth = GetSequenceExpression(seqBoundReach.Depth, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqBoundReach.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqBoundReach.OppositeNodeType);
                    string directedness = GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqBoundReach.EdgeType, env));
                    string function;
                    switch(seqBoundReach.SequenceExpressionType)
                    {
                        case SequenceExpressionType.BoundedReachableNodes:
                            function = "BoundedReachable"; break;
                        case SequenceExpressionType.BoundedReachableNodesViaIncoming:
                            function = "BoundedReachableIncoming"; break;
                        case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
                            function = "BoundedReachableOutgoing"; break;
                        case SequenceExpressionType.BoundedReachableEdges:
                            function = "BoundedReachableEdges" + directedness; break;
                        case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
                            function = "BoundedReachableEdgesIncoming" + directedness; break;
                        case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                            function = "BoundedReachableEdgesOutgoing" + directedness; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqBoundReach.EmitProfiling ? ", procEnv" : "";
                    if(seqBoundReach.SequenceExpressionType == SequenceExpressionType.BoundedReachableNodes
                        || seqBoundReach.SequenceExpressionType == SequenceExpressionType.BoundedReachableNodesViaIncoming
                        || seqBoundReach.SequenceExpressionType == SequenceExpressionType.BoundedReachableNodesViaOutgoing)
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                    else // SequenceExpressionType.BoundedReachableEdges || SequenceExpressionType.BoundedReachableEdgesViaIncoming || SequenceExpressionType.BoundedReachableEdgesViaOutgoing
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                }

                case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
                case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
                case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                {
                    SequenceExpressionBoundedReachableWithRemainingDepth seqBoundReach = (SequenceExpressionBoundedReachableWithRemainingDepth)expr;
                    string sourceNode = GetSequenceExpression(seqBoundReach.SourceNode, source);
                    string depth = GetSequenceExpression(seqBoundReach.Depth, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqBoundReach.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqBoundReach.OppositeNodeType);
                    string function;
                    switch(seqBoundReach.SequenceExpressionType)
                    {
                        case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
                            function = "BoundedReachableWithRemainingDepth"; break;
                        case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
                            function = "BoundedReachableWithRemainingDepthIncoming"; break;
                        case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                            function = "BoundedReachableWithRemainingDepthOutgoing"; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqBoundReach.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                        + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                }

                case SequenceExpressionType.CountBoundedReachableNodes:
                case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
                case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
                case SequenceExpressionType.CountBoundedReachableEdges:
                case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
                case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                {
                    SequenceExpressionCountBoundedReachable seqCntBoundReach = (SequenceExpressionCountBoundedReachable)expr;
                    string sourceNode = GetSequenceExpression(seqCntBoundReach.SourceNode, source);
                    string depth = GetSequenceExpression(seqCntBoundReach.Depth, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqCntBoundReach.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqCntBoundReach.OppositeNodeType);
                    string function;
                    switch(seqCntBoundReach.SequenceExpressionType)
                    {
                        case SequenceExpressionType.CountBoundedReachableNodes:
                            function = "CountBoundedReachable"; break;
                        case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
                            function = "CountBoundedReachableIncoming"; break;
                        case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
                            function = "CountBoundedReachableOutgoing"; break;
                        case SequenceExpressionType.CountBoundedReachableEdges:
                            function = "CountBoundedReachableEdges"; break;
                        case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
                            function = "CountBoundedReachableEdgesIncoming"; break;
                        case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                            function = "CountBoundedReachableEdgesOutgoing"; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqCntBoundReach.EmitProfiling ? ", procEnv" : "";
                    if(seqCntBoundReach.SequenceExpressionType == SequenceExpressionType.CountBoundedReachableNodes
                        || seqCntBoundReach.SequenceExpressionType == SequenceExpressionType.CountBoundedReachableNodesViaIncoming
                        || seqCntBoundReach.SequenceExpressionType == SequenceExpressionType.CountBoundedReachableNodesViaOutgoing)
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                    else // SequenceExpressionType.CountBoundedReachableEdges || SequenceExpressionType.CountBoundedReachableEdgesViaIncoming || SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
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
                    string profilingArgument = seqIsAdjInc.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", " + endElementType + " " + endElement
                        + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
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
                    string profilingArgument = seqIsReach.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", " + endElementType + " " + endElement
                        + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                }

                case SequenceExpressionType.IsBoundedReachableNodes:
                case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
                case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
                case SequenceExpressionType.IsBoundedReachableEdges:
                case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
                case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                {
                    SequenceExpressionIsBoundedReachable seqIsBoundReach = (SequenceExpressionIsBoundedReachable)expr;
                    string sourceNode = GetSequenceExpression(seqIsBoundReach.SourceNode, source);
                    string endElement = GetSequenceExpression(seqIsBoundReach.EndElement, source);
                    string depth = GetSequenceExpression(seqIsBoundReach.Depth, source);
                    string incidentEdgeType = ExtractEdgeType(source, seqIsBoundReach.EdgeType);
                    string adjacentNodeType = ExtractNodeType(source, seqIsBoundReach.OppositeNodeType);
                    string function;
                    switch(seqIsBoundReach.SequenceExpressionType)
                    {
                        case SequenceExpressionType.IsBoundedReachableNodes:
                            function = "IsBoundedReachable"; break;
                        case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
                            function = "IsBoundedReachableIncoming"; break;
                        case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
                            function = "IsBoundedReachableOutgoing"; break;
                        case SequenceExpressionType.IsBoundedReachableEdges:
                            function = "IsBoundedReachableEdges"; break;
                        case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
                            function = "IsBoundedReachableEdgesIncoming"; break;
                        case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                            function = "IsBoundedReachableEdgesOutgoing"; break;
                        default:
                            function = "INTERNAL ERROR"; break;
                    }
                    string profilingArgument = seqIsBoundReach.EmitProfiling ? ", procEnv" : "";
                    if(seqIsBoundReach.SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodes
                        || seqIsBoundReach.SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaIncoming
                        || seqIsBoundReach.SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaOutgoing)
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (GRGEN_LIBGR.INode)" + endElement + ", (int)" + depth
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                    else // SequenceExpressionType.IsBoundedReachableEdges || SequenceExpressionType.IsBoundedReachableEdgesViaIncoming || SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing
                    {
                        return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (GRGEN_LIBGR.IEdge)" + endElement + ", (int)" + depth
                            + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
                    }
                }

                case SequenceExpressionType.InducedSubgraph:
                {
                    SequenceExpressionInducedSubgraph seqInduced = (SequenceExpressionInducedSubgraph)expr;
                    return "GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInduced.NodeSet, source) + ", graph)";
                }

                case SequenceExpressionType.DefinedSubgraph:
                {
                    SequenceExpressionDefinedSubgraph seqDefined = (SequenceExpressionDefinedSubgraph)expr;
                    if (seqDefined.EdgeSet.Type(env) == "set<Edge>")
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraphDirected((IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                    else if (seqDefined.EdgeSet.Type(env) == "set<UEdge>")
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraphUndirected((IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                    else if (seqDefined.EdgeSet.Type(env) == "set<AEdge>")
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                }

                case SequenceExpressionType.EqualsAny:
                {
                    SequenceExpressionEqualsAny seqEqualsAny = (SequenceExpressionEqualsAny)expr;
                    if(seqEqualsAny.IncludingAttributes)
                        return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", true)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", false)";
                }

                case SequenceExpressionType.Nameof:
                {
                    SequenceExpressionNameof seqNameof = (SequenceExpressionNameof)expr;
                    if(seqNameof.NamedEntity != null)
                        return "GRGEN_LIBGR.GraphHelper.Nameof(" + GetSequenceExpression(seqNameof.NamedEntity, source) + ", graph)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.Nameof(null, graph)";
                }

                case SequenceExpressionType.Uniqueof:
                {
                    SequenceExpressionUniqueof seqUniqueof = (SequenceExpressionUniqueof)expr;
                    if(seqUniqueof.UniquelyIdentifiedEntity != null)
                        return "GRGEN_LIBGR.GraphHelper.Uniqueof(" + GetSequenceExpression(seqUniqueof.UniquelyIdentifiedEntity, source) + ", graph)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.Uniqueof(null, graph)";
                }

                case SequenceExpressionType.Typeof:
                {
                    SequenceExpressionTypeof seqTypeof = (SequenceExpressionTypeof)expr;
                    return "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + GetSequenceExpression(seqTypeof.Entity, source) + ", graph.Model)";
                }

                case SequenceExpressionType.ExistsFile:
                {
                    SequenceExpressionExistsFile seqExistsFile = (SequenceExpressionExistsFile)expr;
                    return "System.IO.File.Exists((string)" + GetSequenceExpression(seqExistsFile.Path, source) + ")";
                }

                case SequenceExpressionType.Import:
                {
                    SequenceExpressionImport seqImport = (SequenceExpressionImport)expr;
                    return "GRGEN_LIBGR.GraphHelper.Import(" + GetSequenceExpression(seqImport.Path, source) + ", graph)";
                }

                case SequenceExpressionType.Copy:
                {
                    SequenceExpressionCopy seqCopy = (SequenceExpressionCopy)expr;
                    if(seqCopy.ObjectToBeCopied.Type(env)=="graph")
                        return "GRGEN_LIBGR.GraphHelper.Copy((GRGEN_LIBGR.IGraph)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("set<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) 
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("map<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) 
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("array<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) 
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("deque<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model)
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("match<")) {
                        string rulePatternClassName = "Rule_" + TypesHelper.ExtractSrc(seqCopy.ObjectToBeCopied.Type(env));
                        string matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(TypesHelper.ExtractSrc(seqCopy.ObjectToBeCopied.Type(env)));                     
                        return "((" + matchInterfaceName + ")(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ").Clone())";
                    }
                    else //if(seqCopy.ObjectToBeCopied.Type(env) == "")
                        return "GRGEN_LIBGR.TypesHelper.Clone(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ")";
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

                case SequenceExpressionType.This:
                {
                    SequenceExpressionThis seqThis = (SequenceExpressionThis)expr;
                    return "graph";
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
                    string type = seqAttr.Type(env);
                    if (type == ""
                            || type.StartsWith("set<") || type.StartsWith("map<")
                            || type.StartsWith("array<") || type.StartsWith("deque<"))
                    {
                        return "GRGEN_LIBGR.ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(" + element + ", \"" + seqAttr.AttributeName + "\", " + value + ")";
                    }
                    else
                    {
                        return "(" + TypesHelper.XgrsTypeToCSharpType(type, env.Model) + ")(" + value + ")";
                    }
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
                    string profilingArgument = seqFromGraph.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.GetGraphElement((GRGEN_LIBGR.INamedGraph)graph, \"" + seqFromGraph.ElementName + "\"" + profilingArgument + ")";
                }

                case SequenceExpressionType.NodeByName:
                {
                    SequenceExpressionNodeByName seqNodeByName = (SequenceExpressionNodeByName)expr;
                    string profilingArgument = seqNodeByName.EmitProfiling ? ", procEnv" : "";
                    string nodeType = seqNodeByName.NodeType!=null ? ExtractNodeType(source, seqNodeByName.NodeType) : null;
                    if(nodeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqNodeByName.NodeName, source) + ", " + nodeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqNodeByName.NodeName, source) + profilingArgument + ")";
                }

                case SequenceExpressionType.EdgeByName:
                {
                    SequenceExpressionEdgeByName seqEdgeByName = (SequenceExpressionEdgeByName)expr;
                    string profilingArgument = seqEdgeByName.EmitProfiling ? ", procEnv" : "";
                    string edgeType = seqEdgeByName.EdgeType != null ? ExtractEdgeType(source, seqEdgeByName.EdgeType) : null;
                    if (edgeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqEdgeByName.EdgeName, source) + ", " + edgeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqEdgeByName.EdgeName, source) + profilingArgument + ")";
                }

                case SequenceExpressionType.NodeByUnique:
                {
                    SequenceExpressionNodeByUnique seqNodeByUnique = (SequenceExpressionNodeByUnique)expr;
                    string profilingArgument = seqNodeByUnique.EmitProfiling ? ", procEnv" : "";
                    string nodeType = seqNodeByUnique.NodeType != null ? ExtractNodeType(source, seqNodeByUnique.NodeType) : null;
                    if (nodeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetNode(graph, (int)" + GetSequenceExpression(seqNodeByUnique.NodeUniqueId, source) + ", " + nodeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetNode(graph, (int)" + GetSequenceExpression(seqNodeByUnique.NodeUniqueId, source) + profilingArgument + ")";
                }

                case SequenceExpressionType.EdgeByUnique:
                {
                    SequenceExpressionEdgeByUnique seqEdgeByUnique = (SequenceExpressionEdgeByUnique)expr;
                    string profilingArgument = seqEdgeByUnique.EmitProfiling ? ", procEnv" : "";
                    string edgeType = seqEdgeByUnique.EdgeType != null ? ExtractEdgeType(source, seqEdgeByUnique.EdgeType) : null;
                    if (edgeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetEdge(graph, (int)" + GetSequenceExpression(seqEdgeByUnique.EdgeUniqueId, source) + ", " + edgeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetEdge(graph, (int)" + GetSequenceExpression(seqEdgeByUnique.EdgeUniqueId, source) + profilingArgument + ")";
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
                    return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqOpp.Edge, source) + ").Opposite((GRGEN_LIBGR.INode)(" + GetSequenceExpression(seqOpp.Node, source) + "))";
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
                    if(seqFuncCall.IsExternalFunctionCalled)
                        sb.Append("GRGEN_EXPR.ExternalFunctions.");
                    else
                        sb.AppendFormat("GRGEN_ACTIONS.{0}Functions.", TypesHelper.GetPackagePrefixDot(seqFuncCall.ParamBindings.Package));
                    sb.Append(seqFuncCall.ParamBindings.Name);
                    sb.Append("(procEnv, graph");
                    sb.Append(BuildParameters(seqFuncCall.ParamBindings));
                    sb.Append(")");
                    return sb.ToString();
                }

                case SequenceExpressionType.FunctionMethodCall:
                {
                    SequenceExpressionFunctionMethodCall seqFuncCall = (SequenceExpressionFunctionMethodCall)expr;
                    StringBuilder sb = new StringBuilder();
                    if(seqFuncCall.TargetExpr.Type(env) == "")
                    {
                        sb.Append("((GRGEN_LIBGR.IGraphElement)");
                        sb.Append(GetSequenceExpression(seqFuncCall.TargetExpr, source));
                        sb.Append(").ApplyFunctionMethod(procEnv, graph, ");
                        sb.Append("\"" + seqFuncCall.ParamBindings.Name+ "\"");
                        sb.Append(BuildParametersInObject(seqFuncCall.ParamBindings));
                        sb.Append(")");
                    }
                    else
                    {
                        sb.Append("((");
                        sb.Append(TypesHelper.XgrsTypeToCSharpType(seqFuncCall.TargetExpr.Type(env), model));
                        sb.Append(")");
                        sb.Append(GetSequenceExpression(seqFuncCall.TargetExpr, source));
                        sb.Append(").");
                        sb.Append(seqFuncCall.ParamBindings.Name);
                        sb.Append("(procEnv, graph");
                        sb.Append(BuildParameters(seqFuncCall.ParamBindings, TypesHelper.GetNodeOrEdgeType(seqFuncCall.TargetExpr.Type(env), model).GetFunctionMethod(seqFuncCall.ParamBindings.Name)));
                        sb.Append(")");
                    }
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

        private string GetDirectedness(String edgeRootType)
        {
            if(edgeRootType == "Edge")
                return "Directed";
            else if(edgeRootType == "UEdge")
                return "Undirected";
            else
                return "";
        }

        string GetContainerValue(SequenceExpressionContainer container, SourceBuilder source)
        {
            if(container.ContainerExpr is SequenceExpressionAttributeAccess)
            {
                SequenceExpressionAttributeAccess attribute = (SequenceExpressionAttributeAccess)container.ContainerExpr;
                return "((GRGEN_LIBGR.IGraphElement)" + GetVar(attribute.SourceVar) + ")" + ".GetAttribute(\"" + attribute.AttributeName + "\")";
            }
            else
                return GetSequenceExpression(container.ContainerExpr, source);
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
                return "((double)" + ((double)constant).ToString(System.Globalization.CultureInfo.InvariantCulture) + ")";
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

		public bool GenerateXGRSCode(string xgrsName, String package, String xgrsStr,
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
                seq = SequenceParser.ParseSequence(xgrsStr, package,
                    ruleNames, sequenceNames, procedureNames, functionNames,
                    functionOutputTypes, filterFunctionNames, varDecls, model, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("The exec statement \"" + xgrsStr
                        + "\" given on line " + lineNr + " reported back:\n" + warning);
                }
                seq.Check(env);
                seq.SetNeedForProfilingRecursive(gen.EmitProfiling);
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

            if(gen.FireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugEntering(\"{0}\", \"{1}\");\n", InjectExec(xgrsName), xgrsStr.Replace("\\", "\\\\").Replace("\"", "\\\""));
            }

            EmitNeededVarAndRuleEntities(seq, source);

			EmitSequence(seq, source);

            if(gen.FireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugExiting(\"{0}\");\n", InjectExec(xgrsName));
            }

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

        private string InjectExec(string execName)
        {
            string rulePart = execName.Substring(0, execName.LastIndexOf('_'));
            string execNumberPart = execName.Substring(execName.LastIndexOf('_'));
            return rulePart + ".exec" + execNumberPart;
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
                seq = SequenceParser.ParseSequence(sequence.XGRS, sequence.Package,
                    ruleNames, sequenceNames, procedureNames, functionNames, 
                    functionOutputTypes, filterFunctionNames, varDecls, model, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("In the defined sequence " + sequence.Name
                        + " the exec part \"" + sequence.XGRS
                        + "\" reported back:\n" + warning);
                }
                seq.Check(env);
                seq.SetNeedForProfilingRecursive(gen.EmitProfiling);
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

            if(sequence.Package != null)
            {
                source.AppendFrontFormat("namespace {0}\n", sequence.Package);
                source.AppendFront("{\n");
                source.Indent();
            }

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

            if(sequence.Package != null)
            {
                source.Unindent();
                source.AppendFront("}\n");
            }

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

            source.AppendFront("private "+className+"() : base(\""+sequence.PackagePrefixedName+"\", SequenceInfo_"+sequence.Name+".Instance) { }\n");
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

            if(gen.FireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugEntering(\"{0}\"", sequence.Name);
                for(int i = 0; i < sequence.Parameters.Length; ++i)
                {
                    source.Append(", var_");
                    source.Append(sequence.Parameters[i]);
                }
                source.Append(");\n");
            }

            EmitNeededVarAndRuleEntities(seq, source);

            EmitSequence(seq, source);

            if(gen.FireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugExiting(\"{0}\"", sequence.Name);
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.Append(", var_");
                    source.Append(sequence.OutParameters[i]);
                }
                source.Append(");\n");
            }

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
            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");

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

            source.AppendFront("if(sequenceInvocation.Subgraph!=null)\n");
            source.AppendFront("\t{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }\n");

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

            source.AppendFront("if(sequenceInvocation.Subgraph!=null)\n");
            source.AppendFront("\t{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }\n");

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

            foreach(IFilter filter in rulePattern.Filters)
            {
                if(filter is IFilterAutoGenerated)
                    continue;

                IFilterFunction filterFunction = (IFilterFunction)filter;
                if(!filterFunction.IsExternal)
                    continue;

                if(filter.Package != null)
                {
                    source.AppendFrontFormat("namespace {0}\n", filter.Package);
                    source.AppendFront("{\n");
                    source.Indent();
                }
                source.AppendFront("public partial class MatchFilters\n");
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFrontFormat("//public static void Filter_{0}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {1} matches", filter.Name, matchesListType);
                for(int i = 0; i < filterFunction.Inputs.Length; ++i)
                {
                    source.AppendFormat(", {0} {1}", TypesHelper.TypeName(filterFunction.Inputs[i]), filterFunction.InputNames[i]);
                }
                source.Append(")\n");

                source.Unindent();
                source.AppendFront("}\n");
                if(filter.Package != null)
                {
                    source.Unindent();
                    source.AppendFront("}\n");
                }
            }
        }

        public void GenerateFilters(SourceBuilder source, LGSPRulePattern rulePattern)
        {
            foreach(IFilter f in rulePattern.Filters)
            {
                if(f is IFilterAutoGenerated)
                {
                    IFilterAutoGenerated filter = (IFilterAutoGenerated)f;

                    if(filter.Package != null)
                    {
                        source.AppendFrontFormat("namespace {0}\n", filter.Package);
                        source.AppendFront("{\n");
                        source.Indent();
                    }
                    source.AppendFront("public partial class MatchFilters\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(filter.Name == "auto")
                        GenerateAutomorphyFilter(source, rulePattern);
                    else
                    {
                        if(filter.Name == "orderAscendingBy")
                            GenerateOrderByFilter(source, rulePattern, filter.Entity, true);
                        if(filter.Name == "orderDescendingBy")
                            GenerateOrderByFilter(source, rulePattern, filter.Entity, false);
                        if(filter.Name == "groupBy")
                            GenerateGroupByFilter(source, rulePattern, filter.Entity);
                        if(filter.Name == "keepSameAsFirst")
                            GenerateKeepSameFilter(source, rulePattern, filter.Entity, true);
                        if(filter.Name == "keepSameAsLast")
                            GenerateKeepSameFilter(source, rulePattern, filter.Entity, false);
                        if(filter.Name == "keepOneForEach")
                            GenerateKeepOneForEachFilter(source, rulePattern, filter.Entity);
                    }

                    source.Unindent();
                    source.AppendFront("}\n");
                    if(filter.Package != null)
                    {
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                }
            }
        }

        private static void GenerateAutomorphyFilter(SourceBuilder source, LGSPRulePattern rulePattern)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchClassName = rulePatternClassName + "." + NamesOfEntities.MatchClassName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = "auto";
            
            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n", 
                rulePattern.name, filterName, matchesListType);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront("if(matches.Count < 2)\n");
            source.AppendFront("\treturn;\n");
            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);

            source.AppendFrontFormat("if(matches.Count < 5 || {0}.Instance.patternGraph.nodes.Length + {0}.Instance.patternGraph.edges.Length < 1)\n", rulePatternClassName);
            source.AppendFront("{\n");
            source.Indent();

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

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("Dictionary<int, {0}> foundMatchesOfSameMainPatternHash = new Dictionary<int, {0}>();\n", 
                matchClassName);
            source.AppendFront("for(int i = 0; i < matchesArray.Count; ++i)\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} match = ({0})matchesArray[i];\n", matchClassName);
            source.AppendFront("int duplicateMatchHash = 0;\n");
            source.AppendFront("for(int j = 0; j < match.NumberOfNodes; ++j) duplicateMatchHash ^= match.getNodeAt(j).GetHashCode();\n");
            source.AppendFront("for(int j = 0; j < match.NumberOfEdges; ++j) duplicateMatchHash ^= match.getEdgeAt(j).GetHashCode();\n");
            source.AppendFront("bool contained = foundMatchesOfSameMainPatternHash.ContainsKey(duplicateMatchHash);\n");
            source.AppendFront("if(contained)\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];\n", matchClassName);
            source.AppendFront("do\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(match, duplicateMatchCandidate, procEnv.graph))\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("matchesArray[i] = null;\n");
            source.AppendFrontFormat("goto label_auto_{0};\n", rulePatternClassName.Replace('.', '_'));
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("while((duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash) != null);\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("if(!contained)\n");
            source.AppendFront("\tfoundMatchesOfSameMainPatternHash[duplicateMatchHash] = match;\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];\n", matchClassName);
            source.AppendFront("while(duplicateMatchCandidate.nextWithSameHash != null) duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash;\n");
            source.AppendFront("duplicateMatchCandidate.nextWithSameHash = match;\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFormat("label_auto_{0}: ;\n", rulePatternClassName.Replace('.', '_'));
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFrontFormat("foreach({0} toClean in foundMatchesOfSameMainPatternHash.Values) toClean.CleanNextWithSameHash();\n", matchClassName);
            
            source.Unindent();
            source.AppendFront("}\n");

            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private static void GenerateOrderByFilter(SourceBuilder source, LGSPRulePattern rulePattern, String filterVariable, bool ascending)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = ascending ? "orderAscendingBy_" + filterVariable : "orderDescendingBy_" + filterVariable;

            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n", 
                rulePattern.name, filterName, matchesListType);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);
            source.AppendFrontFormat("matchesArray.Sort(new Comparer_{0}_{1}());\n", rulePattern.name, filterName);
            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.AppendFrontFormat("class Comparer_{0}_{1} : Comparer<{2}>\n", rulePattern.name, filterName, matchInterfaceName);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("public override int Compare({0} left, {0} right)\n", matchInterfaceName);
            source.AppendFront("{\n");
            source.Indent();
            if(ascending)
                source.AppendFrontFormat("return left.{0}.CompareTo(right.{0});\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
            else
                source.AppendFrontFormat("return -left.{0}.CompareTo(right.{0});\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        void GenerateGroupByFilter(SourceBuilder source, LGSPRulePattern rulePattern, String filterVariable)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = "groupBy_" + filterVariable;

            if(true) // does the type of the variable to group-by support ordering? then order, is more efficient than equality comparisons
            {
                source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n",
                    rulePattern.name, filterName, matchesListType);
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);
                source.AppendFrontFormat("matchesArray.Sort(new Comparer_{0}_{1}());\n", rulePattern.name, filterName);
                source.AppendFront("matches.FromList();\n");

                source.Unindent();
                source.AppendFront("}\n");

                source.AppendFrontFormat("class Comparer_{0}_{1} : Comparer<{2}>\n", rulePattern.name, filterName, matchInterfaceName);
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFrontFormat("public override int Compare({0} left, {0} right)\n", matchInterfaceName);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("return left.{0}.CompareTo(right.{0});\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                source.Unindent();
                source.AppendFront("}\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
            else
            {
                // ensure that if two elements are equal, they are neighbours or only separated by equal elements
                // not needed yet, as of now we only support numerical types and string, that support ordering in addition to equality comparison
                /*List<T> matchesArray;
                for(int i = 0; i < matchesArray.Count - 1; ++i)
                {
                    for(int j = i + 1; j < matchesArray.Count; ++j)
                    {
                        if(matchesArray[i] == matchesArray[j])
                        {
                            T tmp = matchesArray[i + 1];
                            matchesArray[i + 1] = matchesArray[j];
                            matchesArray[j] = tmp;
                            break;
                        }
                    }
                }*/
            }
        }

        void GenerateKeepSameFilter(SourceBuilder source, LGSPRulePattern rulePattern, String filterVariable, bool sameAsFirst)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = sameAsFirst ? "keepSameAsFirst_" + filterVariable : "keepSameAsLast_" + filterVariable;

            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n", 
                rulePattern.name, filterName, matchesListType);

            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);

            if(sameAsFirst)
            {
                source.AppendFront("int pos = 0 + 1;\n");
                source.AppendFrontFormat("while(pos < matchesArray.Count && matchesArray[pos].{0} == matchesArray[0].{0})\n", 
                    NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                source.AppendFront("{\n");
                source.AppendFront("\t++pos;\n");
                source.AppendFront("}\n");
                source.AppendFront("for(; pos < matchesArray.Count; ++pos)\n");
                source.AppendFront("{\n");
                source.AppendFront("\tmatchesArray[pos] = null;\n");
                source.AppendFront("}\n");
            }
            else
            {
                source.AppendFront("int pos = matchesArray.Count-1 - 1;\n");
                source.AppendFrontFormat("while(pos >= 0 && matchesArray[pos].{0} == matchesArray[matchesArray.Count-1].{0})\n",
                    NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                source.AppendFront("{\n");
                source.AppendFront("\t--pos;\n");
                source.AppendFront("}\n");
                source.AppendFront("for(; pos >= 0; --pos)\n");
                source.AppendFront("{\n");
                source.AppendFront("\tmatchesArray[pos] = null;\n");
                source.AppendFront("}\n");
            }

            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        void GenerateKeepOneForEachFilter(SourceBuilder source, LGSPRulePattern rulePattern, String filterVariable)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = "keepOneForEach_" + filterVariable;

            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n",
                 rulePattern.name, filterName, matchesListType);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);

            source.AppendFront("int cmpPos = 0;\n");
            source.AppendFront("int pos = 0 + 1;\n");
            source.AppendFront("for(; pos < matchesArray.Count; ++pos)\n");
            source.AppendFront("{\n");
            source.AppendFrontFormat("\tif(matchesArray[pos].{0} == matchesArray[cmpPos].{0})\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
            source.AppendFront("\t\tmatchesArray[pos] = null;\n");
            source.AppendFront("\telse\n");
            source.AppendFront("\t\tcmpPos = pos;\n");
            source.AppendFront("}\n");

            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");
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
                && ex.Kind != SequenceParserError.FilterParameterError
                && ex.Kind != SequenceParserError.OperatorNotFound)
            {
                Console.Error.WriteLine("Unknown " + ex.DefinitionTypeName + ": \"{0}\"", ex.Name);
                return;
            }

            switch(ex.Kind)
            {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if(InputTypes(ex.Name).Count != ex.NumGivenInputs && OutputTypes(ex.Name).Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of parameters and return values for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else if(InputTypes(ex.Name).Count != ex.NumGivenInputs)
                        Console.Error.WriteLine("Wrong number of parameters for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else if(OutputTypes(ex.Name).Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of return values for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else
                        goto default;
                    break;

                case SequenceParserError.BadParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". parameter is not valid for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.BadReturnParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". return parameter is not valid for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.FilterError:
                    Console.Error.WriteLine("Can't apply filter " + ex.FilterName + " to rule " + ex.Name + "!");
                    return;

                case SequenceParserError.FilterParameterError:
                    Console.Error.WriteLine("Filter parameter mismatch for filter \"" + ex.FilterName + "\" applied to \"" + ex.Name + "\"!");
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

                case SequenceParserError.UnknownProcedure:
                    Console.WriteLine("Unknown procedure \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.UnknownFunction:
                    Console.WriteLine("Unknown function \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.TypeMismatch:
                    Console.Error.WriteLine("The construct \"" + ex.VariableOrFunctionName + "\" expects:" + ex.ExpectedType + " but is / is given " + ex.GivenType + "!");
                    return;

                default:
                    throw new ArgumentException("Invalid error kind: " + ex.Kind);
            }

            if(rulesToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of rule/test: {0}", ex.Name);
                PrintInputParams(rulesToInputTypes[ex.Name]);
                PrintOutputParams(rulesToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(sequencesToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of sequence: {0}", ex.Name);
                PrintInputParams(sequencesToInputTypes[ex.Name]);
                PrintOutputParams(sequencesToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(proceduresToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature procedure: {0}", ex.Name);
                PrintInputParams(proceduresToInputTypes[ex.Name]);
                PrintOutputParams(proceduresToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(functionsToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of function: {0}", ex.Name);
                PrintInputParams(functionsToInputTypes[ex.Name]);
                Console.Error.Write(" : ");
                Console.Error.Write(functionsToOutputType[ex.Name]);
                Console.Error.WriteLine();
            }
        }

        List<String> InputTypes(string actionName)
        {
            if(rulesToInputTypes.ContainsKey(actionName))
            {
                return rulesToInputTypes[actionName];
            }
            else if(sequencesToInputTypes.ContainsKey(actionName))
            {
                return sequencesToInputTypes[actionName];
            }
            else if(proceduresToInputTypes.ContainsKey(actionName))
            {
                return proceduresToInputTypes[actionName];
            }
            else if(functionsToInputTypes.ContainsKey(actionName))
            {
                return functionsToInputTypes[actionName];
            }
            return null;
        }

        List<String> OutputTypes(string actionName)
        {
            if(rulesToOutputTypes.ContainsKey(actionName))
            {
                return rulesToOutputTypes[actionName];
            }
            else if(sequencesToOutputTypes.ContainsKey(actionName))
            {
                return sequencesToOutputTypes[actionName];
            }
            else if(proceduresToOutputTypes.ContainsKey(actionName))
            {
                return proceduresToOutputTypes[actionName];
            }
            else if(functionsToOutputType.ContainsKey(actionName))
            {
                List<String> ret = new List<String>();
                ret.Add(functionsToOutputType[actionName]);
                return ret;
            }
            return null;
        }

        void PrintInputParams(List<String> nameToInputTypes)
        {
            if(nameToInputTypes.Count != 0)
            {
                Console.Error.Write("(");
                bool first = true;
                foreach(String typeName in nameToInputTypes)
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
        }

        void PrintOutputParams(List<String> nameToOutputTypes)
        {
            if(nameToOutputTypes.Count != 0)
            {
                Console.Error.Write(" : (");
                bool first = true;
                foreach(String typeName in nameToOutputTypes)
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
        }
    }
}
