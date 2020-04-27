/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using COMP_HELPER = de.unika.ipd.grGen.lgsp.SequenceComputationGeneratorHelper;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class for emitting the needed entities of a sequence, in a pre-run before emitting the real code (by the SequenceGenerator).
    /// </summary>
    public class NeededEntitiesEmitter
    {
        readonly SequenceGeneratorHelper seqHelper;

        // set of the used rules (if contained, a variable was created for easy access to them, once)
        readonly Dictionary<String, object> knownRules = new Dictionary<string, object>();


        public NeededEntitiesEmitter(SequenceGeneratorHelper seqHelper)
        {
            this.seqHelper = seqHelper;
        }

        public void Reset()
        {
            knownRules.Clear();
        }

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits result variable declarations
        /// - emits sequence variable declarations (only once for every variable, declaration only possible at assignment targets)
        /// - collects used rules into knownRules, emit local rule declaration (only once for every rule)
        /// </summary>
		public void EmitNeededVarAndRuleEntities(Sequence seq, SourceBuilder source)
		{
			source.AppendFront(COMP_HELPER.DeclareResultVar(seq));

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
					String ruleName = seqRule.PackagePrefixedName;
					if(!knownRules.ContainsKey(ruleName))
					{
                        knownRules.Add(ruleName, null);
                        source.AppendFront("GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Action_" + seqRule.Name + " " + "rule_" + TypesHelper.PackagePrefixedNameUnderscore(seqRule.Package, seqRule.Name));
                        source.Append(" = " + "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqRule.Package) + "Action_" + seqRule.Name + ".Instance;\n");
                    }
                    // no handling for the input arguments seqRule.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i=0; i<seqRule.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqRule.ReturnVars[i], source);
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
                    // no handling for the input arguments seqSeq.ArgumentExpressions or the optional Subgraph needed 
                    // because there can only be variable uses
                    for(int i = 0; i < seqSeq.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqSeq.ReturnVars[i], source);
                    }
                    break;
                }

            case SequenceType.ForContainer:
                {
                    SequenceForContainer seqFor = (SequenceForContainer)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    if(seqFor.VarDst != null)
                        EmitVarIfNew(seqFor.VarDst, source);
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
				{
					EmitNeededVarAndRuleEntities(childSeq, source);
				}
				break;
			}
		}

        /// <summary>
        /// Emit variable declarations needed (only once for every variable)
        /// </summary>
        private void EmitVarIfNew(SequenceVariable var, SourceBuilder source)
        {
            if(!var.Visited)
            {
                var.Visited = true;
                source.AppendFront(seqHelper.DeclareVar(var));
            }
        }

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits sequence variable declarations (only once for every variable, declaration only possible at assignment targets)
        /// </summary>
		private void EmitNeededVarEntities(SequenceComputation seqComp, SourceBuilder source)
		{
            source.AppendFront(COMP_HELPER.DeclareResultVar(seqComp));
            
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
                    // no handling for the input arguments seqProc.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i = 0; i < seqProc.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqProc.ReturnVars[i], source);
                    }
                    break;
                }

            case SequenceComputationType.BuiltinProcedureCall:
                {
                    SequenceComputationBuiltinProcedureCall seqProc = (SequenceComputationBuiltinProcedureCall)seqComp;
                    // no handling for the input arguments seqProc.ArgumentExpressions needed 
                    // because there can only be variable uses
                    for(int i = 0; i < seqProc.ReturnVars.Count; ++i)
                    {
                        EmitVarIfNew(seqProc.ReturnVars[i], source);
                    }
                    break;
                }

			default:
				foreach(SequenceComputation childSeqComp in seqComp.Children)
				{
					EmitNeededVarEntities(childSeqComp, source);
				}
				break;
			}
		}

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits sequence variable declarations (only once for every variable, declaration only possible at assignment targets)
        /// </summary>
		private void EmitNeededVarEntities(AssignmentTarget tgt, SourceBuilder source)
		{
            source.AppendFront(COMP_HELPER.DeclareResultVar(tgt));

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
    }
}
