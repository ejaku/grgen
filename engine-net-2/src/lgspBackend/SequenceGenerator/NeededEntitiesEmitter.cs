/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

        readonly SequenceExpressionGenerator exprGen;

        // set of the used rules (if contained, a variable was created for easy access to them, once)
        Dictionary<String, object> knownRules = new Dictionary<string, object>();


        public NeededEntitiesEmitter(SequenceGeneratorHelper seqHelper, SequenceExpressionGenerator exprGen)
        {
            this.seqHelper = seqHelper;
            this.exprGen = exprGen;
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

        /// <summary>
        /// pre-run for emitting the needed mapping clauses and (multi) rule queries before emitting the sequences
        /// </summary>
        public void EmitNeededMappingClausesAndRuleQueries(Sequence seq, SequenceGenerator seqGen, SourceBuilder source)
        {
            switch(seq.SequenceType)
            {
            case SequenceType.BooleanComputation:
                {
                    SequenceBooleanComputation seqBoolComp = (SequenceBooleanComputation)seq;
                    if(seqBoolComp.Computation is SequenceExpression)
                        EmitNeededMappingClausesAndRuleQueries((SequenceExpression)seqBoolComp.Computation, seqGen, source);
                    else
                        EmitNeededMappingClausesAndRuleQueries(seqBoolComp.Computation, seqGen, source);
                    break;
                }

            case SequenceType.RuleCall:
            case SequenceType.RuleAllCall:
            case SequenceType.RuleCountAllCall:
                {
                    SequenceRuleCall seqRuleCall = (SequenceRuleCall)seq;
                    foreach(SequenceBase seqBase in seqRuleCall.ChildrenBase)
                    {
                        if(seqBase is SequenceExpression)
                            EmitNeededMappingClausesAndRuleQueries((SequenceExpression)seqBase, seqGen, source);
                    }
                    break;
                }

            case SequenceType.RulePrefixedSequence:
                {
                    SequenceRulePrefixedSequence seqRulePrefixedSequence = (SequenceRulePrefixedSequence)seq;
                    EmitNeededMappingClausesAndRuleQueries(seqRulePrefixedSequence.Rule, seqGen, source);
                    EmitNeededMappingClausesAndRuleQueries(seqRulePrefixedSequence.Sequence, seqGen, source);
                    break;
                }

            case SequenceType.MultiRuleAllCall:
                {
                    SequenceMultiRuleAllCall seqMultiRuleAllCall = (SequenceMultiRuleAllCall)seq;
                    foreach(SequenceRuleCall seqRuleCall in seqMultiRuleAllCall.Sequences)
                    {
                        EmitNeededMappingClausesAndRuleQueries(seqRuleCall, seqGen, source);
                    }
                    foreach(SequenceFilterCallBase seqFilterCall in seqMultiRuleAllCall.Filters)
                    {
                        EmitNeededMappingClausesAndRuleQueries(seqFilterCall, seqGen, source);
                    }
                    break;
                }

            case SequenceType.MultiRulePrefixedSequence:
                {
                    SequenceMultiRulePrefixedSequence seqMultiRulePrefixedSequence = (SequenceMultiRulePrefixedSequence)seq;
                    foreach(SequenceRulePrefixedSequence seqRulePrefixedSequence in seqMultiRulePrefixedSequence.RulePrefixedSequences)
                    {
                        EmitNeededMappingClausesAndRuleQueries(seqRulePrefixedSequence, seqGen, source);
                    }
                    foreach(SequenceFilterCallBase seqFilterCall in seqMultiRulePrefixedSequence.Filters)
                    {
                        EmitNeededMappingClausesAndRuleQueries(seqFilterCall, seqGen, source);
                    }
                    break;
                }

            case SequenceType.SomeFromSet:
                {
                    SequenceSomeFromSet seqSomeFromSet = (SequenceSomeFromSet)seq;
                    foreach(SequenceRuleCall seqRuleCall in seqSomeFromSet.Sequences)
                    {
                        EmitNeededMappingClausesAndRuleQueries(seqRuleCall, seqGen, source);
                    }
                    break;
                }

            case SequenceType.ForMatch:
                {
                    SequenceForMatch seqForMatch = (SequenceForMatch)seq;
                    EmitNeededMappingClausesAndRuleQueries(seqForMatch.Rule, seqGen, source);
                    EmitNeededMappingClausesAndRuleQueries(seqForMatch.Seq, seqGen, source);
                    break;
                }

            case SequenceType.Backtrack:
                {
                    SequenceBacktrack seqBacktrack = (SequenceBacktrack)seq;
                    EmitNeededMappingClausesAndRuleQueries(seqBacktrack.Rule, seqGen, source);
                    EmitNeededMappingClausesAndRuleQueries(seqBacktrack.Seq, seqGen, source);
                    break;
                }

            case SequenceType.MultiBacktrack:
                {
                    SequenceMultiBacktrack seqMultiBacktrack = (SequenceMultiBacktrack)seq;
                    EmitNeededMappingClausesAndRuleQueries(seqMultiBacktrack.Rules, seqGen, source);
                    EmitNeededMappingClausesAndRuleQueries(seqMultiBacktrack.Seq, seqGen, source);
                    break;
                }

            case SequenceType.MultiSequenceBacktrack:
                {
                    SequenceMultiSequenceBacktrack seqMultiSequenceBacktrack = (SequenceMultiSequenceBacktrack)seq;
                    EmitNeededMappingClausesAndRuleQueries(seqMultiSequenceBacktrack.MultiRulePrefixedSequence, seqGen, source);
                    break;
                }

            default:
                foreach(Sequence childSeq in seq.Children)
                {
                    EmitNeededMappingClausesAndRuleQueries(childSeq, seqGen, source);
                }
                break;
            }
        }

        private void EmitNeededMappingClausesAndRuleQueries(SequenceFilterCallBase seqFilterCall, SequenceGenerator seqGen, SourceBuilder source)
        {
            if(seqFilterCall is SequenceFilterCall)
                EmitNeededMappingClausesAndRuleQueries((SequenceFilterCall)seqFilterCall, seqGen, source);
            else if(seqFilterCall is SequenceFilterCallLambdaExpression)
                EmitNeededMappingClausesAndRuleQueries((SequenceFilterCallLambdaExpression)seqFilterCall, seqGen, source);
        }

        private void EmitNeededMappingClausesAndRuleQueries(SequenceFilterCall seqFilterCall, SequenceGenerator seqGen, SourceBuilder source)
        {
            foreach(SequenceExpression seqExpr in seqFilterCall.ArgumentExpressions)
            {
                EmitNeededMappingClausesAndRuleQueries(seqExpr, seqGen, source);
            }
        }

        private void EmitNeededMappingClausesAndRuleQueries(SequenceFilterCallLambdaExpression seqFilterCall, SequenceGenerator seqGen, SourceBuilder source)
        {
            if(seqFilterCall.FilterCall.initExpression != null)
                EmitNeededMappingClausesAndRuleQueries(seqFilterCall.FilterCall.initExpression, seqGen, source);
            EmitNeededMappingClausesAndRuleQueries(seqFilterCall.FilterCall.lambdaExpression, seqGen, source);
        }

        private void EmitNeededMappingClausesAndRuleQueries(SequenceComputation seqComp, SequenceGenerator seqGen, SourceBuilder source)
        {
            foreach(SequenceComputation childSeqComp in seqComp.Children)
            {
                if(childSeqComp is SequenceExpression)
                    EmitNeededMappingClausesAndRuleQueries((SequenceExpression)childSeqComp, seqGen, source);
                else
                    EmitNeededMappingClausesAndRuleQueries(childSeqComp, seqGen, source);
            }
        }

        private void EmitNeededMappingClausesAndRuleQueries(SequenceExpression seqExpr, SequenceGenerator seqGen, SourceBuilder source)
        {
            switch(seqExpr.SequenceExpressionType)
            {
            case SequenceExpressionType.MappingClause:
                {
                    SequenceExpressionMappingClause seqMapping = (SequenceExpressionMappingClause)seqExpr;
                    Dictionary<String, object> knownRulesBackup = knownRules;
                    knownRules = new Dictionary<string, object>();
                    exprGen.EmitSequenceExpressionMappingClauseImplementation(seqMapping, seqGen, this, source);
                    knownRules = knownRulesBackup;
                    EmitNeededMappingClausesAndRuleQueries(seqMapping.MultiRulePrefixedSequence, seqGen, source);
                    break;
                }
            case SequenceExpressionType.RuleQuery:
                {
                    SequenceExpressionRuleQuery seqRuleQuery = (SequenceExpressionRuleQuery)seqExpr;
                    Dictionary<String, object> knownRulesBackup = knownRules;
                    knownRules = new Dictionary<string, object>();
                    exprGen.EmitSequenceExpressionRuleQueryImplementation(seqRuleQuery, seqGen, this, source);
                    knownRules = knownRulesBackup;
                    EmitNeededMappingClausesAndRuleQueries(seqRuleQuery.RuleCall, seqGen, source);
                    break;
                }
            case SequenceExpressionType.MultiRuleQuery:
                {
                    SequenceExpressionMultiRuleQuery seqMultiRuleQuery = (SequenceExpressionMultiRuleQuery)seqExpr;
                    Dictionary<String, object> knownRulesBackup = knownRules;
                    knownRules = new Dictionary<string, object>();
                    exprGen.EmitSequenceExpressionMultiRuleQueryImplementation(seqMultiRuleQuery, seqGen, this, source);
                    knownRules = knownRulesBackup;
                    EmitNeededMappingClausesAndRuleQueries(seqMultiRuleQuery.MultiRuleCall, seqGen, source);
                    break;
                }
            default:
                foreach(SequenceExpression childSeqComp in seqExpr.ChildrenExpression)
                {
                    EmitNeededMappingClausesAndRuleQueries(childSeqComp, seqGen, source);
                }
                break;
            }
        }
    }
}
