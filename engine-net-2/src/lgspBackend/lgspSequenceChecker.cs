/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class for some extended type checking of the sequence for which some code is to be generated.
    /// </summary>
    public class LGSPSequenceChecker
    {
        // the rule names available in the .grg to compile
        String[] ruleNames;

        // maps rule names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> rulesToOutputTypes;

        // the model object of the .grg to compile
        IGraphModel model;


        public LGSPSequenceChecker(String[] ruleNames, Dictionary<String, List<String>> rulesToInputTypes, 
            Dictionary<String, List<String>> rulesToOutputTypes, IGraphModel model)
        {
            this.ruleNames = ruleNames;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.model = model;
        }

        /// <summary>
        /// Checks the given sequence for type errors
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

            case SequenceType.RuleAll:
            case SequenceType.Rule:
                {
                    SequenceRule ruleSeq = (SequenceRule)seq;
                    RuleInvocationParameterBindings paramBindings = ruleSeq.ParamBindings;

                    // processing of a compiled xgrs without BaseActions but array of rule names,
                    // check the rule name against the available rule names
                    if(Array.IndexOf(ruleNames, paramBindings.RuleName) == -1)
                        throw new SequenceParserRuleException(paramBindings, SequenceParserError.UnknownRule);

                    // ok, this is a rule invocation
                    break;
                }

            case SequenceType.AssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar assignSeq = (SequenceAssignSequenceResultToVar)seq;
                    Check(assignSeq.Seq);
                    break;
                }

            case SequenceType.Def:
            case SequenceType.True:
            case SequenceType.False:
            case SequenceType.VarPredicate:
            case SequenceType.AssignVarToVar:
            case SequenceType.AssignConstToVar:
            case SequenceType.AssignAttributeToVar:
            case SequenceType.AssignVarToAttribute:
            case SequenceType.AssignElemToVar:
            case SequenceType.AssignVAllocToVar:
            case SequenceType.AssignSetmapSizeToVar:
            case SequenceType.AssignSetmapEmptyToVar:
            case SequenceType.AssignMapAccessToVar:
            case SequenceType.AssignSetCreationToVar:
            case SequenceType.AssignMapCreationToVar:
            case SequenceType.IsVisited:
            case SequenceType.SetVisited:
            case SequenceType.VFree:
            case SequenceType.VReset:
            case SequenceType.Emit:
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
