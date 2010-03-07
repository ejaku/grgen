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
                    throw new SequenceParserException(paramBindings, SequenceParserError.UnknownRule);

                // Check whether number of parameters and return parameters match
                if(rulesToInputTypes[paramBindings.RuleName].Count != paramBindings.ParamVars.Length
                        || paramBindings.ReturnVars.Length != 0 && rulesToOutputTypes[paramBindings.RuleName].Count != paramBindings.ReturnVars.Length)
                    throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

                // Check parameter types
                for(int i = 0; i < paramBindings.ParamVars.Length; i++)
                {
                    if(paramBindings.ParamVars[i] != null 
                        && !TypesHelper.IsSameOrSubtype(paramBindings.ParamVars[i].Type, rulesToInputTypes[paramBindings.RuleName][i], model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }

                // Check return types
                for(int i = 0; i < paramBindings.ReturnVars.Length; ++i)
                {
                    if(!TypesHelper.IsSameOrSubtype(rulesToOutputTypes[paramBindings.RuleName][i], paramBindings.ReturnVars[i].Type, model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
                }

                // ok, this is a well-formed rule invocation
                break;
            }

            case SequenceType.AssignSequenceResultToVar:
            {
                SequenceAssignSequenceResultToVar assignSeq = (SequenceAssignSequenceResultToVar)seq;
                Check(assignSeq.Seq);
                break;
            }

            case SequenceType.VarPredicate:
            {
                SequenceVarPredicate varPredSeq = (SequenceVarPredicate)seq;
                // TODO
                break;
            }

            case SequenceType.AssignVarToVar:
            {
                SequenceAssignVarToVar assignSeq = (SequenceAssignVarToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignConstToVar:
            {
                SequenceAssignConstToVar assignSeq = (SequenceAssignConstToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignAttributeToVar:
            {
                SequenceAssignAttributeToVar assignSeq = (SequenceAssignAttributeToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignVarToAttribute:
            {
                SequenceAssignVarToAttribute assignSeq = (SequenceAssignVarToAttribute)seq;
                // TODO
                break;
            }

            case SequenceType.AssignElemToVar:
            {
                SequenceAssignElemToVar assignSeq = (SequenceAssignElemToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignVAllocToVar:
            {
                SequenceAssignVAllocToVar assignSeq = (SequenceAssignVAllocToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignSetmapSizeToVar:
            {
                SequenceAssignSetmapSizeToVar assignSeq = (SequenceAssignSetmapSizeToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignSetmapEmptyToVar:
            {
                SequenceAssignSetmapEmptyToVar assignSeq = (SequenceAssignSetmapEmptyToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignMapAccessToVar:
            {
                SequenceAssignMapAccessToVar assignSeq = (SequenceAssignMapAccessToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignSetCreationToVar:
            {
                SequenceAssignSetCreationToVar assignSeq = (SequenceAssignSetCreationToVar)seq;
                // TODO
                break;
            }

            case SequenceType.AssignMapCreationToVar:
            {
                SequenceAssignMapCreationToVar assignSeq = (SequenceAssignMapCreationToVar)seq;
                // TODO
                break;
            }

            case SequenceType.IsVisited:
            {
                SequenceIsVisited isVisSeq = (SequenceIsVisited)seq;
                // TODO
                break;
            }

            case SequenceType.SetVisited:
            {
                SequenceSetVisited setVisSeq = (SequenceSetVisited)seq;
                // TODO
                break;
            }

            case SequenceType.VFree:
            {
                SequenceVFree vFreeSeq = (SequenceVFree)seq;
                // TODO
                break;
            }
            
            case SequenceType.VReset:
            {
                SequenceVReset vResetSeq = (SequenceVReset)seq;
                // TODO
                break;
            }
            
            case SequenceType.SetmapAdd:
            {
                SequenceSetmapAdd addSeq = (SequenceSetmapAdd)seq;
                if(!addSeq.Setmap.Type.StartsWith("set<") && !addSeq.Setmap.Type.StartsWith("map<"))
                {
                    throw new SequenceParserException(addSeq.Setmap.Name, "set or map type", addSeq.Setmap.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(addSeq.Var.Type, TypesHelper.ExtractSrc(addSeq.Setmap.Type), model))
                {
                    throw new SequenceParserException(addSeq.Setmap.Name+".Add("+addSeq.Var.Name+")", TypesHelper.ExtractSrc(addSeq.Setmap.Type), addSeq.Var.Type);
                }
                if(TypesHelper.ExtractDst(addSeq.Setmap.Type) != "SetValueType"
                    && !TypesHelper.IsSameOrSubtype(addSeq.VarDst.Type, TypesHelper.ExtractDst(addSeq.Setmap.Type), model))
                {
                    throw new SequenceParserException(addSeq.Setmap.Name+".Add(.,"+addSeq.VarDst.Name+")", TypesHelper.ExtractDst(addSeq.Setmap.Type), addSeq.VarDst.Type);
                }
                break;
            }

            case SequenceType.SetmapRem:
            {
                SequenceSetmapRem remSeq = (SequenceSetmapRem)seq;
                if(!remSeq.Setmap.Type.StartsWith("set<") && !remSeq.Setmap.Type.StartsWith("map<"))
                {
                    throw new SequenceParserException(remSeq.Setmap.Name, "set or map type", remSeq.Setmap.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(remSeq.Var.Type, TypesHelper.ExtractSrc(remSeq.Setmap.Type), model))
                {
                    throw new SequenceParserException(remSeq.Setmap.Name+".Rem("+remSeq.Var.Name+")", TypesHelper.ExtractSrc(remSeq.Setmap.Type), remSeq.Var.Type);
                }
                break;
            }

            case SequenceType.SetmapClear:
            {
                SequenceSetmapClear clrSeq = (SequenceSetmapClear)seq;
                if(!clrSeq.Setmap.Type.StartsWith("set<") && !clrSeq.Setmap.Type.StartsWith("map<"))
                {
                    throw new SequenceParserException(clrSeq.Setmap.Name, "set or map type", clrSeq.Setmap.Type);
                }
                break;
            }

            case SequenceType.InSetmap:
            {
                SequenceIn inSeq = (SequenceIn)seq;
                if(!inSeq.Setmap.Type.StartsWith("set<") && !inSeq.Setmap.Type.StartsWith("map<"))
                {
                    throw new SequenceParserException(inSeq.Setmap.Name, "set or map type", inSeq.Setmap.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(inSeq.Var.Type, TypesHelper.ExtractSrc(inSeq.Setmap.Type), model))
                {
                    throw new SequenceParserException(inSeq.Var.Name+" in "+inSeq.Setmap.Name , TypesHelper.ExtractSrc(inSeq.Setmap.Type), inSeq.Var.Type);
                }
                break;
            }

            case SequenceType.Def:
            case SequenceType.True:
            case SequenceType.False:
            case SequenceType.Emit:
                // Nothing to be done here
                break;

            default:
                throw new Exception("Unknown sequence type: " + seq.SequenceType);
            }
        }
    }
}
