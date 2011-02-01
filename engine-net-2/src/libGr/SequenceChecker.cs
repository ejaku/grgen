/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Class for some basic checking of the sequence which is to be interpreted.
    /// </summary>
    public class SequenceChecker
    {
        BaseActions actions;

        public SequenceChecker(BaseActions actions)
        {
            this.actions = actions;
        }

        /// <summary>
        /// Checks the given sequence for errors
        /// reports them by exception
        /// </summary>
        public void Check(Sequence seq)
        {
            switch(seq.SequenceType)
            {
            case SequenceType.ThenLeft:
            case SequenceType.ThenRight:
            case SequenceType.LazyOr:
            case SequenceType.LazyAnd:
            case SequenceType.StrictOr:
            case SequenceType.Xor:
            case SequenceType.StrictAnd:
            case SequenceType.IfThen: // lazy implication
            {
                SequenceBinary binSeq = (SequenceBinary)seq;
                Check(binSeq.Left);
                Check(binSeq.Right);
                break;
            }

            case SequenceType.Not:
            case SequenceType.IterationMin:
            case SequenceType.IterationMinMax:
            case SequenceType.Transaction:
            case SequenceType.For:
            {
                SequenceUnary unSeq = (SequenceUnary)seq;
                Check(unSeq.Seq);
                break;
            }

            case SequenceType.IfThenElse:
            {
                SequenceIfThenElse seqIf = (SequenceIfThenElse)seq;
                Check(seqIf.Condition);
                Check(seqIf.TrueCase);
                Check(seqIf.FalseCase);
                break;
            }

            case SequenceType.LazyOrAll:
            case SequenceType.LazyAndAll:
            case SequenceType.StrictOrAll:
            case SequenceType.StrictAndAll:
            {
                foreach(Sequence seqChild in seq.Children)
                    Check(seqChild);
                break;
            }

            case SequenceType.SomeFromSet:
            {
                foreach (Sequence seqChild in seq.Children)
                {
                    Check(seqChild);
                    if (seqChild is SequenceRuleAllCall
                        && ((SequenceRuleAllCall)seqChild).MinVarChooseRandom != null
                        && ((SequenceRuleAllCall)seqChild).MaxVarChooseRandom != null)
                        throw new Exception("Sequence SomeFromSet (e.g. {r1,[r2],$[r3]}) can't contain a select with variable from all construct (e.g. $v[r4], e.g. $v1,v2[r4])");
                }
                break;
            }

            case SequenceType.SequenceDefinition:
            {
                SequenceDefinition seqDef = (SequenceDefinition)seq;
                Check(seqDef.Seq);
                break;
            }

            case SequenceType.SequenceCall:
                // Nothing to be done here?
                break;

            case SequenceType.RuleAllCall:
            case SequenceType.RuleCall:
            {
                SequenceRuleCall ruleSeq = (SequenceRuleCall)seq;
                RuleInvocationParameterBindings paramBindings = ruleSeq.ParamBindings;

                // We found the rule?
                if(paramBindings.Action == null)
                {
                    throw new SequenceParserException(paramBindings, SequenceParserError.UnknownRule);
                }
                
                // yes -> this is a rule call; now check it
                IAction action = paramBindings.Action;

                // Check whether number of parameters and return parameters match
                if(action.RulePattern.Inputs.Length != paramBindings.ParamVars.Length
                        || paramBindings.ReturnVars.Length != 0 && action.RulePattern.Outputs.Length != paramBindings.ReturnVars.Length)
                    throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

                // Check parameter types
                for(int i = 0; i < paramBindings.ParamVars.Length; i++)
                {
                    // CSharpCC does not support as-expressions, yet...
                    VarType inputType = (VarType)(action.RulePattern.Inputs[i] is VarType ? action.RulePattern.Inputs[i] : null);

                    // If input type is not a VarType, a variable must be specified.
                    // Otherwise, if a constant is specified, the VarType must match the type of the constant
                    if(inputType == null && paramBindings.ParamVars[i] == null
                            || inputType != null && paramBindings.Parameters[i] != null && inputType.Type != paramBindings.Parameters[i].GetType())
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }

                if(seq.SequenceType != SequenceType.RuleAllCall)
                {
                    // When no return parameters were specified for a rule with returns, create an according array with null entries
                    if(paramBindings.ReturnVars.Length == 0 && action.RulePattern.Outputs.Length > 0)
                        paramBindings.ReturnVars = new SequenceVariable[action.RulePattern.Outputs.Length];
                }

                // ok, this is a well-formed rule invocation
                break;
            }

            case SequenceType.AssignSequenceResultToVar:
            case SequenceType.OrAssignSequenceResultToVar:
            case SequenceType.AndAssignSequenceResultToVar:
            {
                SequenceAssignSequenceResultToVar assignSeq = (SequenceAssignSequenceResultToVar)seq;
                Check(assignSeq.Seq);
                break;
            }

            case SequenceType.Backtrack:
            {
                SequenceBacktrack backSeq = (SequenceBacktrack)seq;
                Check(backSeq.Rule);
                Check(backSeq.Seq);
                break;
            }

            case SequenceType.Def:
            case SequenceType.True:
            case SequenceType.False:
            case SequenceType.VarPredicate:
            case SequenceType.AssignVarToVar:
            case SequenceType.AssignUserInputToVar:
            case SequenceType.AssignRandomToVar:
            case SequenceType.AssignConstToVar:
            case SequenceType.AssignAttributeToVar:
            case SequenceType.AssignVarToAttribute:
            case SequenceType.AssignElemToVar:
            case SequenceType.AssignVAllocToVar:
            case SequenceType.AssignSetmapSizeToVar:
            case SequenceType.AssignSetmapEmptyToVar:
            case SequenceType.AssignMapAccessToVar:
            case SequenceType.IsVisited:
            case SequenceType.SetVisited:
            case SequenceType.VFree:
            case SequenceType.VReset:
            case SequenceType.Emit:
            case SequenceType.Record:
            case SequenceType.SetmapAdd:
            case SequenceType.SetmapRem:
            case SequenceType.SetmapClear:
            case SequenceType.InSetmap:
                // Nothing to be done here
                break;

            default:
                throw new Exception("Unknown sequence type: " + seq.SequenceType);
            }
        }
    }
}
