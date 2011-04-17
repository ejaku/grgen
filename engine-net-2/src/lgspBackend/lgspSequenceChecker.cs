/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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

        // the sequence names available in the .grg to compile
        String[] sequenceNames;

        // maps rule names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> rulesToOutputTypes;

        // maps sequence names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> sequencesToInputTypes;
        // maps sequence names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> sequencesToOutputTypes;

        // returns rule or sequence name to input types dictionary depending on argument
        Dictionary<String, List<String>> toInputTypes(bool rule) { return rule ? rulesToInputTypes : sequencesToInputTypes; }

        // returns rule or sequence name to output types dictionary depending on argument
        Dictionary<String, List<String>> toOutputTypes(bool rule) { return rule ? rulesToOutputTypes : sequencesToOutputTypes; } 

        // the model object of the .grg to compile
        IGraphModel model;


        public LGSPSequenceChecker(String[] ruleNames, String[] sequenceNames, 
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes,
            Dictionary<String, List<String>> sequencesToInputTypes, Dictionary<String, List<String>> sequencesToOutputTypes,
            IGraphModel model)
        {
            this.ruleNames = ruleNames;
            this.sequenceNames = sequenceNames;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.sequencesToInputTypes = sequencesToInputTypes;
            this.sequencesToOutputTypes = sequencesToOutputTypes;
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
                foreach(Sequence seqChild in seq.Children)
                {
                    Check(seqChild);
                    if(seqChild is SequenceRuleAllCall 
                        && ((SequenceRuleAllCall)seqChild).MinVarChooseRandom!=null
                        && ((SequenceRuleAllCall)seqChild).MaxVarChooseRandom!=null)
                        throw new Exception("Sequence SomeFromSet (e.g. {r1,[r2],$[r3]}) can't contain a select with variable from all construct (e.g. $v[r4], e.g. $v1,v2[r4])");
                }
                break;
            }

            case SequenceType.RuleAllCall:
            case SequenceType.RuleCall:
            case SequenceType.SequenceCall:
            {
                SequenceRuleCall ruleSeq = seq as SequenceRuleCall;
                SequenceSequenceCall seqSeq = seq as SequenceSequenceCall;
                InvocationParameterBindings paramBindings = ruleSeq!=null ? (InvocationParameterBindings)ruleSeq.ParamBindings : (InvocationParameterBindings)seqSeq.ParamBindings;

                // processing of a compiled xgrs without BaseActions but array of rule names,
                // check the rule name against the available rule names
                if(Array.IndexOf(ruleNames, paramBindings.Name) == -1
                    && Array.IndexOf(sequenceNames, paramBindings.Name) == -1)
                    throw new SequenceParserException(paramBindings, SequenceParserError.UnknownRuleOrSequence);

                // Check whether number of parameters and return parameters match
                if(toInputTypes(ruleSeq!=null)[paramBindings.Name].Count != paramBindings.ParamVars.Length
                        || paramBindings.ReturnVars.Length != 0 && toOutputTypes(ruleSeq!=null)[paramBindings.Name].Count != paramBindings.ReturnVars.Length)
                    throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

                // Check parameter types
                for(int i = 0; i < paramBindings.ParamVars.Length; i++)
                {
                    if(paramBindings.ParamVars[i] != null
                        && !TypesHelper.IsSameOrSubtype(paramBindings.ParamVars[i].Type, toInputTypes(ruleSeq!=null)[paramBindings.Name][i], model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }

                // Check return types
                for(int i = 0; i < paramBindings.ReturnVars.Length; ++i)
                {
                    if(!TypesHelper.IsSameOrSubtype(toOutputTypes(ruleSeq!=null)[paramBindings.Name][i], paramBindings.ReturnVars[i].Type, model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
                }

                // ok, this is a well-formed rule invocation
                break;
            }

            // case SequenceType.SequenceDefinition: not supported in compiled sequence
            case SequenceType.SequenceDefinitionCompiled:
            {
                // todo: something to check?
                break;
            }

            case SequenceType.AssignSequenceResultToVar:
            case SequenceType.OrAssignSequenceResultToVar:
            case SequenceType.AndAssignSequenceResultToVar:
            {
                SequenceAssignSequenceResultToVar assignSeq = (SequenceAssignSequenceResultToVar)seq;
                Check(assignSeq.Seq);
                if(!TypesHelper.IsSameOrSubtype(assignSeq.DestVar.Type, "boolean", model))
                {
                    if(seq.SequenceType == SequenceType.OrAssignSequenceResultToVar)
                        throw new SequenceParserException("sequence |> " + assignSeq.DestVar.Name, "boolean", assignSeq.DestVar.Type);
                    else if(seq.SequenceType == SequenceType.AndAssignSequenceResultToVar)
                        throw new SequenceParserException("sequence &> " + assignSeq.DestVar.Name, "boolean", assignSeq.DestVar.Type);
                    else //if(seq.SequenceType==SequenceType.AssignSequenceResultToVar)
                        throw new SequenceParserException("sequence => " + assignSeq.DestVar.Name, "boolean", assignSeq.DestVar.Type);
                }
                break;
            }

            case SequenceType.Backtrack:
            {
                SequenceBacktrack backSeq = (SequenceBacktrack)seq;
                Check(backSeq.Rule);
                Check(backSeq.Seq);
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

            case SequenceType.AssignContainerSizeToVar:
            {
                SequenceAssignContainerSizeToVar assignSeq = (SequenceAssignContainerSizeToVar)seq;
                if(assignSeq.Container.Type != "" && (TypesHelper.ExtractSrc(assignSeq.Container.Type) == null || TypesHelper.ExtractDst(assignSeq.Container.Type) == null))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + ".size()", "set<S> or map<S,T> or array<S> type", assignSeq.Container.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(assignSeq.DestVar.Type, "int", model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + ".size()", "int", assignSeq.DestVar.Type);
                }
                break;
            }

            case SequenceType.AssignContainerEmptyToVar:
            {
                SequenceAssignContainerEmptyToVar assignSeq = (SequenceAssignContainerEmptyToVar)seq;
                if(assignSeq.Container.Type!="" && (TypesHelper.ExtractSrc(assignSeq.Container.Type)==null || TypesHelper.ExtractDst(assignSeq.Container.Type)==null))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + ".empty()", "set<S> or map<S,T> or array<S> type", assignSeq.Container.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(assignSeq.DestVar.Type, "boolean", model))
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + ".empty()", "boolean", assignSeq.DestVar.Type);
                }
                break;
            }

            case SequenceType.AssignContainerAccessToVar:
            {
                SequenceAssignContainerAccessToVar assignSeq = (SequenceAssignContainerAccessToVar)seq;
                if(assignSeq.Container.Type == "") break; // we can't check source and destination types if the variable is untyped, only runtime-check possible
                if(TypesHelper.ExtractSrc(assignSeq.Container.Type)==null || TypesHelper.ExtractDst(assignSeq.Container.Type)==null || TypesHelper.ExtractDst(assignSeq.Container.Type)=="SetValueType")
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + "[" + assignSeq.KeyVar.Name + "]", "map<S,T> or array<S>", assignSeq.Container.Type);
                }
                if(assignSeq.Container.Type.StartsWith("array"))
                {
                    if(!TypesHelper.IsSameOrSubtype(assignSeq.KeyVar.Type, "int", model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + "[" + assignSeq.KeyVar.Name + "]", "int", assignSeq.KeyVar.Type);
                    }
                    if(!TypesHelper.IsSameOrSubtype(TypesHelper.ExtractSrc(assignSeq.Container.Type), assignSeq.DestVar.Type, model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + "[" + assignSeq.KeyVar.Name + "]", assignSeq.DestVar.Type, TypesHelper.ExtractSrc(assignSeq.Container.Type));
                    }
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(assignSeq.KeyVar.Type, TypesHelper.ExtractSrc(assignSeq.Container.Type), model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + "[" + assignSeq.KeyVar.Name + "]", TypesHelper.ExtractSrc(assignSeq.Container.Type), assignSeq.KeyVar.Type);
                    }
                    if(!TypesHelper.IsSameOrSubtype(TypesHelper.ExtractDst(assignSeq.Container.Type), assignSeq.DestVar.Type, model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "=" + assignSeq.Container.Name + "[" + assignSeq.KeyVar.Name + "]", assignSeq.DestVar.Type, TypesHelper.ExtractDst(assignSeq.Container.Type));
                    }
                }
                break;
            }

            case SequenceType.AssignVarToIndexedVar:
            {
                SequenceAssignVarToIndexedVar assignSeq = (SequenceAssignVarToIndexedVar)seq;
                if(assignSeq.DestVar.Type == "") break; // we can't check source and destination types if the variable is untyped, only runtime-check possible
                if(TypesHelper.ExtractSrc(assignSeq.DestVar.Type) == null || TypesHelper.ExtractDst(assignSeq.DestVar.Type) == null || TypesHelper.ExtractDst(assignSeq.DestVar.Type) == "SetValueType")
                {
                    throw new SequenceParserException(assignSeq.DestVar.Name + "[" + assignSeq.KeyVar.Name + "[" + assignSeq.KeyVar.Name + "] = " + assignSeq.Var.Name, "map<S,T> or array<T>", assignSeq.DestVar.Type);
                }
                if(assignSeq.DestVar.Type.StartsWith("array"))
                {
                    if(!TypesHelper.IsSameOrSubtype(assignSeq.KeyVar.Type, "int", model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "[" + assignSeq.KeyVar.Name + "] = " + assignSeq.Var.Name, "int", assignSeq.KeyVar.Type);
                    }
                    if(!TypesHelper.IsSameOrSubtype(assignSeq.Var.Type, TypesHelper.ExtractSrc(assignSeq.DestVar.Type), model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "[" + assignSeq.KeyVar.Name + "] = " + assignSeq.Var.Name, assignSeq.Var.Type, TypesHelper.ExtractSrc(assignSeq.DestVar.Type));
                    }
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(assignSeq.KeyVar.Type, TypesHelper.ExtractSrc(assignSeq.DestVar.Type), model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "[" + assignSeq.DestVar.Name + "] = " + assignSeq.Var.Name, TypesHelper.ExtractSrc(assignSeq.DestVar.Type), assignSeq.KeyVar.Type);
                    }
                    if(!TypesHelper.IsSameOrSubtype(assignSeq.Var.Type, TypesHelper.ExtractDst(assignSeq.DestVar.Type), model))
                    {
                        throw new SequenceParserException(assignSeq.DestVar.Name + "[" + assignSeq.DestVar.Name + "] = " + assignSeq.Var.Name, assignSeq.Var.Type, TypesHelper.ExtractDst(assignSeq.DestVar.Type));
                    }
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
            
            case SequenceType.ContainerAdd:
            {
                SequenceContainerAdd addSeq = (SequenceContainerAdd)seq;
                if(addSeq.Container.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
                if(!addSeq.Container.Type.StartsWith("set<") && !addSeq.Container.Type.StartsWith("map<") && !addSeq.Container.Type.StartsWith("array<"))
                {
                    throw new SequenceParserException(addSeq.Container.Name, addSeq.VarDst==null ? "set or array type" : "map or array type", addSeq.Container.Type);
                }
                if(addSeq.VarDst!=null && TypesHelper.ExtractDst(addSeq.Container.Type)=="SetValueType")
                {
                    throw new SequenceParserException(addSeq.Container.Name, "map type or array", addSeq.Container.Type);
                }
                if(addSeq.Container.Type.StartsWith("array<"))
                {
                    if(!TypesHelper.IsSameOrSubtype(addSeq.Var.Type, TypesHelper.ExtractSrc(addSeq.Container.Type), model))
                    {
                        if(addSeq.VarDst == null) throw new SequenceParserException(addSeq.Container.Name + ".Add(" + addSeq.Var.Name + ")", TypesHelper.ExtractSrc(addSeq.Container.Type), addSeq.Var.Type);
                        else throw new SequenceParserException(addSeq.Container.Name + ".Add(" + addSeq.Var.Name + "," + addSeq.VarDst.Name + ")", TypesHelper.ExtractSrc(addSeq.Container.Type), addSeq.Var.Type);
                    }
                    if(addSeq.VarDst!=null && !TypesHelper.IsSameOrSubtype(addSeq.VarDst.Type, "int", model))
                    {
                        throw new SequenceParserException(addSeq.Container.Name + ".Add(.," + addSeq.VarDst.Name + ")", TypesHelper.ExtractDst(addSeq.Container.Type), addSeq.VarDst.Type);
                    }
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(addSeq.Var.Type, TypesHelper.ExtractSrc(addSeq.Container.Type), model))
                    {
                        if(addSeq.VarDst==null) throw new SequenceParserException(addSeq.Container.Name+".Add("+addSeq.Var.Name+")", TypesHelper.ExtractSrc(addSeq.Container.Type), addSeq.Var.Type);
                        else throw new SequenceParserException(addSeq.Container.Name + ".Add(" + addSeq.Var.Name + "," + addSeq.VarDst.Name + ")", TypesHelper.ExtractSrc(addSeq.Container.Type), addSeq.Var.Type);
                    }
                    if(TypesHelper.ExtractDst(addSeq.Container.Type) != "SetValueType"
                        && !TypesHelper.IsSameOrSubtype(addSeq.VarDst.Type, TypesHelper.ExtractDst(addSeq.Container.Type), model))
                    {
                        throw new SequenceParserException(addSeq.Container.Name+".Add(.,"+addSeq.VarDst.Name+")", TypesHelper.ExtractDst(addSeq.Container.Type), addSeq.VarDst.Type);
                    }
                }
                break;
            }

            case SequenceType.ContainerRem:
            {
                SequenceContainerRem remSeq = (SequenceContainerRem)seq;
                if(remSeq.Container.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
                if(!remSeq.Container.Type.StartsWith("set<") && !remSeq.Container.Type.StartsWith("map<") && !remSeq.Container.Type.StartsWith("array<"))
                {
                    throw new SequenceParserException(remSeq.Container.Name, "set or map or array type", remSeq.Container.Type);
                }
                if(remSeq.Container.Type.StartsWith("array<"))
                {
                    if(remSeq.Var!=null && !TypesHelper.IsSameOrSubtype(remSeq.Var.Type, "int", model))
                    {
                        throw new SequenceParserException(remSeq.Container.Name + ".Rem(" + remSeq.Var.Name + ")", "int", remSeq.Var.Type);
                    }
                }
                else
                {
                    if(!TypesHelper.IsSameOrSubtype(remSeq.Var.Type, TypesHelper.ExtractSrc(remSeq.Container.Type), model))
                    {
                        throw new SequenceParserException(remSeq.Container.Name + ".Rem(" + remSeq.Var.Name + ")", TypesHelper.ExtractSrc(remSeq.Container.Type), remSeq.Var.Type);
                    }
                }
                break;
            }

            case SequenceType.ContainerClear:
            {
                SequenceContainerClear clrSeq = (SequenceContainerClear)seq;
                if(clrSeq.Container.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
                if(!clrSeq.Container.Type.StartsWith("set<") && !clrSeq.Container.Type.StartsWith("map<") && !clrSeq.Container.Type.StartsWith("array<"))
                {
                    throw new SequenceParserException(clrSeq.Container.Name, "set or map or array type", clrSeq.Container.Type);
                }
                break;
            }

            case SequenceType.InContainer:
            {
                SequenceIn inSeq = (SequenceIn)seq;
                if(inSeq.Container.Type == "") break; // we can't check further types if the variable is untyped, only runtime-check possible
                if(!inSeq.Container.Type.StartsWith("set<") && !inSeq.Container.Type.StartsWith("map<") && !inSeq.Container.Type.StartsWith("array<"))
                {
                    throw new SequenceParserException(inSeq.Container.Name, "set or map or array type", inSeq.Container.Type);
                }
                if(!TypesHelper.IsSameOrSubtype(inSeq.Var.Type, TypesHelper.ExtractSrc(inSeq.Container.Type), model))
                {
                    throw new SequenceParserException(inSeq.Var.Name+" in "+inSeq.Container.Name , TypesHelper.ExtractSrc(inSeq.Container.Type), inSeq.Var.Type);
                }
                break;
            }

            case SequenceType.Def:
            case SequenceType.True:
            case SequenceType.False:
            case SequenceType.Emit:
            case SequenceType.Record:
            // Nothing to be done here
                break;

            case SequenceType.Yield:
            {
                // the assignment of an untyped variable to a typed variable is ok, cause we want access to persistency
                // which is only offered by the untyped variables; it is checked at runtime / causes an invalid cast exception
                SequenceYield seqYield = (SequenceYield)seq;
                if(!TypesHelper.IsSameOrSubtype(seqYield.FromVar.Type, seqYield.ToVar.Type, model))
                {
                    throw new SequenceParserException("yield " + seqYield.ToVar.Name + "=" + seqYield.FromVar.Name, seqYield.ToVar.Type, seqYield.FromVar.Type);
                }
                break;
            }

            default: // esp. AssignElemToVar
                throw new Exception("Unknown/unsupported sequence type: " + seq.SequenceType);
            }
        }
    }
}
