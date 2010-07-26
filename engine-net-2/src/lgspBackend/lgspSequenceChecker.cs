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

        // expected sequence yield output types
        String[] expectedYieldTypes;


        public LGSPSequenceChecker(String[] ruleNames, Dictionary<String, List<String>> rulesToInputTypes, 
            Dictionary<String, List<String>> rulesToOutputTypes, IGraphModel model, GrGenType[] expectedYieldTypes)
        {
            this.ruleNames = ruleNames;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.model = model;
            this.expectedYieldTypes = new String[expectedYieldTypes.Length];
            for(int i=0; i<expectedYieldTypes.Length; ++i)
            {
                this.expectedYieldTypes[i] = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(expectedYieldTypes[i]), model);
            }
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

            case SequenceType.LazyOrAll:
            case SequenceType.LazyAndAll:
            case SequenceType.StrictOrAll:
            case SequenceType.StrictAndAll:
            {
                foreach(Sequence seqChild in seq.Children)
                    Check(seqChild);
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
                if(!TypesHelper.IsSameOrSubtype(assignSeq.DestVar.Type, "boolean", model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=sequence", "boolean", assignSeq.DestVar.Type);
                }
                break;
            }

            case SequenceType.VarPredicate:
            {
                SequenceVarPredicate varPredSeq = (SequenceVarPredicate)seq;
                if(!TypesHelper.IsSameOrSubtype(varPredSeq.PredicateVar.Type, "boolean", model))
                {
                    throw new SequenceParserException(varPredSeq.PredicateVar.Name, "boolean", varPredSeq.PredicateVar.Type);
                }
                break;
            }

            case SequenceType.AssignVarToVar:
            {
                // the assignment of an untyped variable to a typed variable is ok, cause we want access to persistency
                // which is only offered by the untyped variables; it is checked at runtime / causes an invalid cast exception
                SequenceAssignVarToVar assignSeq = (SequenceAssignVarToVar)seq;
                if(!TypesHelper.IsSameOrSubtype(assignSeq.SourceVar.Type, assignSeq.DestVar.Type, model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.SourceVar.Name, assignSeq.DestVar.Type, assignSeq.SourceVar.Type);
                }
                break;
            }

            case SequenceType.AssignUserInputToVar:
            {
                SequenceAssignUserInputToVar assignUI = (SequenceAssignUserInputToVar)seq;
                if(!TypesHelper.IsSameOrSubtype(assignUI.Type, assignUI.DestVar.Type, model))
                {
                    throw new SequenceParserException(assignUI.DestVar.Name + "=$%("+assignUI.Type+")", assignUI.DestVar.Type, assignUI.Type);
                }
                break;
            }

            case SequenceType.AssignRandomToVar:
            {
                SequenceAssignRandomToVar assignRandom = (SequenceAssignRandomToVar)seq;
                if(!TypesHelper.IsSameOrSubtype(assignRandom.DestVar.Type, "int", model))
                {
                    throw new SequenceParserException(assignRandom.DestVar.Name + "=$("+assignRandom.Number+")", "int", assignRandom.DestVar.Type);
                }
                break;
            }

            case SequenceType.AssignConstToVar:
            {
                SequenceAssignConstToVar assignSeq = (SequenceAssignConstToVar)seq;
                if(!TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(assignSeq.Constant, model), assignSeq.DestVar.Type, model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Constant.ToString(), assignSeq.DestVar.Type, TypesHelper.XgrsTypeOfConstant(assignSeq.Constant, model));
                }
                break;
            }

            case SequenceType.AssignAttributeToVar:
            {
                SequenceAssignAttributeToVar assignSeq = (SequenceAssignAttributeToVar)seq;
                if(assignSeq.SourceVar.Type=="") break; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
                GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(assignSeq.SourceVar.Type, model);
                if(nodeOrEdgeType==null)
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.SourceVar.Name + "." + assignSeq.AttributeName, "node or edge type", assignSeq.SourceVar.Type);
                } 
                AttributeType attributeType = nodeOrEdgeType.GetAttributeType(assignSeq.AttributeName);
                if(attributeType==null)
                {
                    throw new SequenceParserException(assignSeq.AttributeName, SequenceParserError.UnknownAttribute);
                } 
                if(!TypesHelper.IsSameOrSubtype(TypesHelper.AttributeTypeToXgrsType(attributeType), assignSeq.DestVar.Type, model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.SourceVar.Name + "." + assignSeq.AttributeName, assignSeq.DestVar.Type, TypesHelper.AttributeTypeToXgrsType(attributeType));
                }
                break;
            }

            case SequenceType.AssignVarToAttribute:
            {
                SequenceAssignVarToAttribute assignSeq = (SequenceAssignVarToAttribute)seq;
                if(assignSeq.DestVar.Type == "") break; // we can't gain access to an attribute type if the variable is untyped, only runtime-check possible
                GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(assignSeq.DestVar.Type, model);
                if(nodeOrEdgeType == null)
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "." + assignSeq.AttributeName + "=" + assignSeq.SourceVar.Name, "node or edge type", assignSeq.DestVar.Type);
                }
                AttributeType attributeType = nodeOrEdgeType.GetAttributeType(assignSeq.AttributeName);
                if(attributeType == null)
                {
                    throw new SequenceParserException(assignSeq.AttributeName, SequenceParserError.UnknownAttribute);
                }
                if(!TypesHelper.IsSameOrSubtype(assignSeq.SourceVar.Type, TypesHelper.AttributeTypeToXgrsType(attributeType), model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "." + assignSeq.AttributeName + "=" + assignSeq.SourceVar.Name, TypesHelper.AttributeTypeToXgrsType(attributeType), assignSeq.SourceVar.Type);
                }
                break;
            }

            case SequenceType.AssignElemToVar:
            {
                SequenceAssignElemToVar assignSeq = (SequenceAssignElemToVar)seq;
                GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(assignSeq.DestVar.Type, model);
                if(nodeOrEdgeType == null)
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=@(" + assignSeq.ElementName + ")", "node or edge type", assignSeq.DestVar.Type);
                } 
                break;
            }

            case SequenceType.AssignVAllocToVar:
            {
                SequenceAssignVAllocToVar assignSeq = (SequenceAssignVAllocToVar)seq;
                if(!TypesHelper.IsSameOrSubtype(assignSeq.DestVar.Type, "int", model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=valloc()", "int", assignSeq.DestVar.Type);
                }
                break;
            }

            case SequenceType.AssignSetmapSizeToVar:
            {
                SequenceAssignSetmapSizeToVar assignSeq = (SequenceAssignSetmapSizeToVar)seq;
                if(assignSeq.Setmap.Type != "" && (TypesHelper.ExtractSrc(assignSeq.Setmap.Type) == null || TypesHelper.ExtractDst(assignSeq.Setmap.Type) == null))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Setmap.Name + ".size()", "set<S> or map<S,T> type", assignSeq.Setmap.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(assignSeq.DestVar.Type, "int", model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Setmap.Name + ".size()", "int", assignSeq.DestVar.Type);
                }
                break;
            }

            case SequenceType.AssignSetmapEmptyToVar:
            {
                SequenceAssignSetmapEmptyToVar assignSeq = (SequenceAssignSetmapEmptyToVar)seq;
                if(assignSeq.Setmap.Type!="" && (TypesHelper.ExtractSrc(assignSeq.Setmap.Type)==null || TypesHelper.ExtractDst(assignSeq.Setmap.Type)==null))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Setmap.Name + ".empty()", "set<S> or map<S,T> type", assignSeq.Setmap.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(assignSeq.DestVar.Type, "boolean", model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Setmap.Name + ".empty()", "boolean", assignSeq.DestVar.Type);
                }
                break;
            }

            case SequenceType.AssignMapAccessToVar:
            {
                SequenceAssignMapAccessToVar assignSeq = (SequenceAssignMapAccessToVar)seq;
                if(assignSeq.Setmap.Type == "") break; // we can't check source and destination types if the variable is untyped, only runtime-check possible
                if(TypesHelper.ExtractSrc(assignSeq.Setmap.Type)==null || TypesHelper.ExtractDst(assignSeq.Setmap.Type)==null || TypesHelper.ExtractDst(assignSeq.Setmap.Type)=="SetValueType")
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Setmap.Name + "[" + assignSeq.KeyVar.Name + "]", "map<S,T>", assignSeq.Setmap.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(assignSeq.KeyVar.Type, TypesHelper.ExtractSrc(assignSeq.Setmap.Type), model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Setmap.Name + "[" + assignSeq.KeyVar.Name + "]", TypesHelper.ExtractSrc(assignSeq.Setmap.Type), assignSeq.KeyVar.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(TypesHelper.ExtractDst(assignSeq.Setmap.Type), assignSeq.DestVar.Type, model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Setmap.Name + "[" + assignSeq.KeyVar.Name + "]", assignSeq.DestVar.Type, TypesHelper.ExtractDst(assignSeq.Setmap.Type));
                }
                break;
            }

            case SequenceType.IsVisited:
            {
                SequenceIsVisited isVisSeq = (SequenceIsVisited)seq;
                GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(isVisSeq.GraphElementVar.Type, model);
                if(isVisSeq.GraphElementVar.Type!="" && nodeOrEdgeType==null)
                {
                    throw new SequenceParserException(isVisSeq.GraphElementVar.Name + ".visited[" + isVisSeq.VisitedFlagVar.Name + "]", "node or edge type", isVisSeq.GraphElementVar.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(isVisSeq.VisitedFlagVar.Type, "int", model))
                {
                    throw new SequenceParserException(isVisSeq.GraphElementVar.Name + ".visited[" + isVisSeq.VisitedFlagVar.Name + "]", "int", isVisSeq.VisitedFlagVar.Type);
                }
                break;
            }

            case SequenceType.SetVisited:
            {
                SequenceSetVisited setVisSeq = (SequenceSetVisited)seq;
                String varVal = setVisSeq.Var != null ? setVisSeq.Var.Name : setVisSeq.Val.ToString();
                GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(setVisSeq.GraphElementVar.Type, model);
                if(setVisSeq.GraphElementVar.Type != "" && nodeOrEdgeType == null)
                {
                    throw new SequenceParserException(setVisSeq.GraphElementVar.Name + ".visited[" + setVisSeq.VisitedFlagVar.Name + "]=" + varVal, "node or edge type", setVisSeq.GraphElementVar.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(setVisSeq.VisitedFlagVar.Type, "int", model))
                {
                    throw new SequenceParserException(setVisSeq.GraphElementVar.Name + ".visited[" + setVisSeq.VisitedFlagVar.Name + "]=" + varVal, "int", setVisSeq.VisitedFlagVar.Type);
                }
                if(setVisSeq.Var!=null && !TypesHelper.IsSameOrSubtype(setVisSeq.Var.Type, "boolean", model))
                {
                    throw new SequenceParserException(setVisSeq.GraphElementVar.Name + ".visited[" + setVisSeq.VisitedFlagVar.Name + "]=" + varVal, "boolean", setVisSeq.Var.Type);
                }
                break;
            }

            case SequenceType.VFree:
            {
                SequenceVFree vFreeSeq = (SequenceVFree)seq;
                if(!TypesHelper.IsSameOrSubtype(vFreeSeq.VisitedFlagVar.Type, "int", model))
                {
                    throw new SequenceParserException("vfree(" + vFreeSeq.VisitedFlagVar.Name + ")", "int", vFreeSeq.VisitedFlagVar.Type);
                }
                break;
            }
            
            case SequenceType.VReset:
            {
                SequenceVReset vResetSeq = (SequenceVReset)seq;
                if(!TypesHelper.IsSameOrSubtype(vResetSeq.VisitedFlagVar.Type, "int", model))
                {
                    throw new SequenceParserException("vfree(" + vResetSeq.VisitedFlagVar.Name + ")", "int", vResetSeq.VisitedFlagVar.Type);
                }
                break;
            }
            
            case SequenceType.SetmapAdd:
            {
                SequenceSetmapAdd addSeq = (SequenceSetmapAdd)seq;
                if(addSeq.Setmap.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
                if(!addSeq.Setmap.Type.StartsWith("set<") && !addSeq.Setmap.Type.StartsWith("map<"))
                {
                    throw new SequenceParserException(addSeq.Setmap.Name, addSeq.VarDst==null ? "set type" : "map type", addSeq.Setmap.Type);
                }
                if(addSeq.VarDst!=null && TypesHelper.ExtractDst(addSeq.Setmap.Type)=="SetValueType")
                {
                    throw new SequenceParserException(addSeq.Setmap.Name, "map type", addSeq.Setmap.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(addSeq.Var.Type, TypesHelper.ExtractSrc(addSeq.Setmap.Type), model))
                {
                    if(addSeq.VarDst==null) throw new SequenceParserException(addSeq.Setmap.Name+".Add("+addSeq.Var.Name+")", TypesHelper.ExtractSrc(addSeq.Setmap.Type), addSeq.Var.Type);
                    else throw new SequenceParserException(addSeq.Setmap.Name + ".Add(" + addSeq.Var.Name + "," + addSeq.VarDst.Name + ")", TypesHelper.ExtractSrc(addSeq.Setmap.Type), addSeq.Var.Type);
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
                if(remSeq.Setmap.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
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
                if(clrSeq.Setmap.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
                if(!clrSeq.Setmap.Type.StartsWith("set<") && !clrSeq.Setmap.Type.StartsWith("map<"))
                {
                    throw new SequenceParserException(clrSeq.Setmap.Name, "set or map type", clrSeq.Setmap.Type);
                }
                break;
            }

            case SequenceType.InSetmap:
            {
                SequenceIn inSeq = (SequenceIn)seq;
                if(inSeq.Setmap.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
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

            case SequenceType.Yield:
            {
                // transmit expected yield types from xgrs interface to the contained yields where they are needed
                SequenceYield seqYield = (SequenceYield)seq;
                seqYield.SetExpectedYieldType(expectedYieldTypes);
                break;
            }

            default: // esp. AssignElemToVar
                throw new Exception("Unknown sequence type: " + seq.SequenceType);
            }
        }
    }
}
