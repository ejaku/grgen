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
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Represents an XGRS used in an exec statement.
    /// </summary>
	public class LGSPXGRSInfo
	{
        /// <summary>
        /// Constructs an LGSPXGRSInfo object.
        /// </summary>
        /// <param name="parameters">The names of the needed graph elements of the containing action.</param>
        /// <param name="xgrs">The XGRS string.</param>
		public LGSPXGRSInfo(String[] parameters, String xgrs)
		{
			Parameters = parameters;
			XGRS = xgrs;
		}

        /// <summary>
        /// The names of the needed graph elements of the containing action.
        /// </summary>
		public String[] Parameters;

        /// <summary>
        /// The XGRS string.
        /// </summary>
		public String XGRS;
	}

    /// <summary>
    /// The C#-part responsible for compiling the XGRSs of the exec statements.
    /// </summary>
    public class LGSPSequenceGenerator
    {
        LGSPGrGen gen;

        int xgrsNextSequenceID = 0;
		Dictionary<Sequence, int> xgrsSequenceIDs = new Dictionary<Sequence, int>();
		Dictionary<String, object> xgrsVars = new Dictionary<string, object>();
		Dictionary<String, object> xgrsRules = new Dictionary<string, object>();
        Dictionary<int, object> xgrsParamArrays = new Dictionary<int, object>();

        public LGSPSequenceGenerator(LGSPGrGen gen)
        {
            this.gen = gen;
        }

		void EmitElementVarIfNew(String varName, SourceBuilder source)
		{
			if(!xgrsVars.ContainsKey(varName))
			{
				xgrsVars.Add(varName, null);
				source.AppendFront("object var_" + varName + " = null;\n");
			}
		}

        void EmitSetmapVarIfNew(String varName, String typeKey, String typeValue, SourceBuilder source, IGraphModel model)
        {
            if(!xgrsVars.ContainsKey(varName))
            {
                xgrsVars.Add(varName, null);
                // todo: use exact types in compiled xgrs
                //typeKey = DictionaryHelper.GetQualifiedTypeName(typeKey, model);
                //typeValue = DictionaryHelper.GetQualifiedTypeName(typeValue, model);
                //source.AppendFront("IDictionary<"+typeKey+","+typeValue+"> var_" + varName + " = null;\n");
                source.AppendFront("System.Collections.IDictionary var_" + varName + " = null;\n");
            }
        }

		void EmitBoolVarIfNew(String varName, SourceBuilder source)
		{
			if(!xgrsVars.ContainsKey(varName))
			{
				xgrsVars.Add(varName, null);
				source.AppendFront("bool varbool_" + varName + " = false;\n");
			}
		}

        // pre-run for emitting the needed entities before emitting the real code
        // - assigns ids to sequences, stores the mapping in xgrsSequenceIDs, needed for naming of result variables
        // - collects used variables into xgrsVars, only on first occurence some code for the entity gets emitted
        // - collects used rules into xgrsRules, only on first occurence some code for the entitiy gets emitted
		void AssignIdsAndEmitNeededVarAndRuleEntities(Sequence seq, SourceBuilder source, IGraphModel model)
		{
			source.AppendFront("bool res_" + xgrsNextSequenceID + ";\n");
			xgrsSequenceIDs.Add(seq, xgrsNextSequenceID++);

			switch(seq.SequenceType)
			{
				case SequenceType.AssignElemToVar:
				{
					SequenceAssignElemToVar seqToVar = (SequenceAssignElemToVar) seq;
					EmitElementVarIfNew(seqToVar.DestVar, source);
					break;
				}
				case SequenceType.AssignVarToVar:		// TODO: Load from external vars?
				{
					SequenceAssignVarToVar seqToVar = (SequenceAssignVarToVar) seq;
					EmitElementVarIfNew(seqToVar.DestVar, source);
					break;
				}
                case SequenceType.AssignVAllocToVar:
                {
                    SequenceAssignVAllocToVar vallocToVar = (SequenceAssignVAllocToVar) seq;
                    EmitElementVarIfNew(vallocToVar.DestVar, source);
                    break;
                }
                case SequenceType.AssignSetmapSizeToVar:
                {
                    SequenceAssignSetmapSizeToVar seqSetmapSizeToVar = (SequenceAssignSetmapSizeToVar)seq;
                    EmitElementVarIfNew(seqSetmapSizeToVar.DestVar, source); 
                    break;
                }
                case SequenceType.AssignSetmapEmptyToVar:
                {
                    SequenceAssignSetmapEmptyToVar seqSetmapEmptyToVar = (SequenceAssignSetmapEmptyToVar)seq;
                    EmitElementVarIfNew(seqSetmapEmptyToVar.DestVar, source); 
                    break;
                }
                case SequenceType.AssignMapAccessToVar:
                {
                    SequenceAssignMapAccessToVar seqMapAccessToVar = (SequenceAssignMapAccessToVar)seq;
                    EmitElementVarIfNew(seqMapAccessToVar.DestVar, source); 
                    break;
                }
                case SequenceType.AssignSetCreationToVar:
                {
                    SequenceAssignSetCreationToVar seqToVar = (SequenceAssignSetCreationToVar)seq;
                    EmitSetmapVarIfNew(seqToVar.DestVar, seqToVar.TypeName, "de.unika.ipd.grGen.libGr.SetValueType", source, model);
                    break;
                }
                case SequenceType.AssignMapCreationToVar:
                {
                    SequenceAssignMapCreationToVar seqToVar = (SequenceAssignMapCreationToVar)seq;
                    EmitSetmapVarIfNew(seqToVar.DestVar,seqToVar.TypeName, seqToVar.TypeNameDst, source, model);
                    break;
                }
				case SequenceType.AssignSequenceResultToVar:
				{
					SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar) seq;
					EmitBoolVarIfNew(seqToVar.DestVar, source);
					AssignIdsAndEmitNeededVarAndRuleEntities(seqToVar.Seq, source, model);
					break;
				}
                case SequenceType.AssignConstToVar:
                {
                    SequenceAssignConstToVar constToVar = (SequenceAssignConstToVar)seq;
                    EmitBoolVarIfNew(constToVar.DestVar, source);
                    break;
                }


				case SequenceType.Rule:
				case SequenceType.RuleAll:
				{
					SequenceRule seqRule = (SequenceRule) seq;
					String ruleName = seqRule.RuleObj.RuleName;
					if(!xgrsRules.ContainsKey(ruleName))
					{
						xgrsRules.Add(ruleName, null);
                        source.AppendFrontFormat("Action_{0} rule_{0} = Action_{0}.Instance;\n", ruleName);
					}
                    foreach(String varName in seqRule.RuleObj.ParamVars)
                    {
                        if(varName != null)
                            EmitElementVarIfNew(varName, source);
                    }
					foreach(String varName in seqRule.RuleObj.ReturnVars)
						EmitElementVarIfNew(varName, source);
					break;
				}

                case SequenceType.For:
                {
                    SequenceFor seqFor = (SequenceFor)seq;
                    EmitElementVarIfNew(seqFor.Var, source);
                    if(seqFor.VarDst!=null) EmitElementVarIfNew(seqFor.VarDst, source);
                    AssignIdsAndEmitNeededVarAndRuleEntities(seqFor.Seq, source, model);
                    break;
                }

				default:
					foreach(Sequence childSeq in seq.Children)
						AssignIdsAndEmitNeededVarAndRuleEntities(childSeq, source, model);
					break;
			}
		}

		void EmitLazyOp(Sequence left, Sequence right, SequenceType seqType, int seqID, SourceBuilder source)
		{
			EmitSequence(left, source);

            if(seqType == SequenceType.LazyOr) {
                source.AppendFront("if(res_" + xgrsSequenceIDs[left] + ")");
                source.Indent();
                source.AppendFront("res_" + seqID + " = true;\n");
                source.Unindent();
            } else if(seqType == SequenceType.LazyAnd) {
                source.AppendFront("if(!res_" + xgrsSequenceIDs[left] + ")");
                source.Indent();
                source.AppendFront("res_" + seqID + " = false;\n");
                source.Unindent();
            } else { //seqType==SequenceType.IfThen -- lazy implication
                source.AppendFront("if(!res_" + xgrsSequenceIDs[left] + ")");
                source.Indent();
                source.AppendFront("res_" + seqID + " = true;\n");
                source.Unindent();
            }

			source.AppendFront("else\n");
			source.AppendFront("{\n");
			source.Indent();
			
            EmitSequence(right, source);
            source.AppendFront("res_" + seqID + " = res_" + xgrsSequenceIDs[right] + ";\n");

            source.Unindent();
			source.AppendFront("}\n");
		}

		void EmitSequence(Sequence seq, SourceBuilder source)
		{
			int seqID = xgrsSequenceIDs[seq];

			switch(seq.SequenceType)
			{
				case SequenceType.Rule:
                case SequenceType.RuleAll:
                {
					SequenceRule seqRule = (SequenceRule) seq;
					RuleObject ruleObj = seqRule.RuleObj;
                    String specialStr = seqRule.Special ? "true" : "false";
                    int paramLen = ruleObj.ParamVars.Length;
                    if(paramLen != 0)
                    {
                        for(int i = 0; i < paramLen; i++)
                        {
                            source.AppendFront("__xgrs_paramarray_" + paramLen + "[" + i + "] = ");
                            if(ruleObj.ParamVars[i] != null)
                                source.Append("var_" + ruleObj.ParamVars[i]);
                            else
                            {
                                object arg = ruleObj.Parameters[i];
                                if(arg is bool)
                                    source.Append((bool) arg ? "true" : "false");
                                else if(arg is string)
                                    source.Append("\"" + (string) arg + "\"");
                                else if(arg is float)
                                    source.Append(((float) arg).ToString(System.Globalization.CultureInfo.InvariantCulture) + "f");
                                else if(arg is double)
                                    source.Append(((double) arg).ToString(System.Globalization.CultureInfo.InvariantCulture));
                                else
                                    source.Append(arg.ToString());
                            }
                            source.Append(";\n");
                        }
                    }
                    source.AppendFront("GRGEN_LIBGR.IMatches mat_" + seqID + " = rule_" + ruleObj.RuleName
                        + ".Match(graph, " + (seq.SequenceType == SequenceType.Rule ? "1" : "graph.MaxMatches")
                        + (paramLen == 0 ? ", null);\n" : ", __xgrs_paramarray_" + paramLen + ");\n"));
                    if(gen.FireEvents) source.AppendFront("graph.Matched(mat_" + seqID + ", " + specialStr + ");\n");
                    source.AppendFront("if(mat_" + seqID + ".Count == 0)\n");
                    source.AppendFront("\tres_" + seqID + " = false;\n");
                    source.AppendFront("else\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    if(gen.UsePerfInfo)
                        source.AppendFront("if(graph.PerformanceInfo != null) graph.PerformanceInfo.MatchesFound += mat_" + seqID + ".Count;\n");
                    if(gen.FireEvents) source.AppendFront("graph.Finishing(mat_" + seqID + ", " + specialStr + ");\n");
                    source.AppendFront("object[] ret_" + seqID + " = ");
                    if(seq.SequenceType == SequenceType.Rule)
                    {
                        source.Append("rule_" + ruleObj.RuleName + ".Modify(graph, mat_" + seqID + ".First);\n");
                        if(gen.UsePerfInfo)
                            source.AppendFront("if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;\n");
                    }
                    else if(((SequenceRuleAll) seq).NumChooseRandom <= 0)
                        source.Append("graph.Replace(mat_" + seqID + ", -1);\n");
                    else
                    {
                        source.Append("null;\n");
                        source.AppendFront("for(int repi_" + seqID + " = 0; repi_" + seqID + " < "
                            + ((SequenceRuleAll) seq).NumChooseRandom + "; repi_" + seqID + "++)\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        if(gen.FireEvents) source.AppendFront("if(repi_" + seqID + " != 0) graph.RewritingNextMatch();\n");
                        source.AppendFront("GRGEN_LIBGR.IMatch curmat_" + seqID + " = mat_" + seqID
                            + ".RemoveMatch(GRGEN_LIBGR.Sequence.randomGenerator.Next(mat_" + seqID + ".Count));\n");
                        source.AppendFront("ret_" + seqID + " = mat_" + seqID + ".Producer.Modify(graph, curmat_" + seqID + ");\n");
                        if(gen.UsePerfInfo)
                            source.AppendFront("if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }

                    if(ruleObj.ReturnVars.Length != 0)
                    {
                        source.AppendFront("if(ret_" + seqID + " != null)\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        for(int i = 0; i < ruleObj.ReturnVars.Length; i++)
                            source.AppendFront("var_" + ruleObj.ReturnVars[i] + " = ret_" + seqID + "[" + i + "];\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }

                    if(gen.FireEvents) source.AppendFront("graph.Finished(mat_" + seqID + ", " + specialStr + ");\n");

                    source.AppendFront("res_" + seqID + " = ret_" + seqID + " != null;\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

				case SequenceType.VarPredicate:
				{
					SequenceVarPredicate seqPred = (SequenceVarPredicate) seq;
					source.AppendFront("res_" + seqID + " = varbool_" + seqPred.PredicateVar + ";\n");
					break;
				}

				case SequenceType.Not:
				{
					SequenceNot seqNot = (SequenceNot) seq;
					EmitSequence(seqNot.Seq, source);
					source.AppendFront("res_" + seqID + " = !res_" + xgrsSequenceIDs[seqNot.Seq] + ";\n");
					break;
				}

				case SequenceType.LazyOr:
				case SequenceType.LazyAnd:
                case SequenceType.IfThen:
				{
					SequenceBinary seqBin = (SequenceBinary) seq;
					if(seqBin.Randomize)
					{
                        if(seq.SequenceType==SequenceType.IfThen) throw new Exception("Internal error in EmitSequence: Randomized ifthen not possible!");
                        source.AppendFront("if(GRGEN_LIBGR.Sequence.randomGenerator.Next(2) == 1)\n");
						source.AppendFront("{\n");
						source.Indent();
                        EmitLazyOp(seqBin.Right, seqBin.Left, seq.SequenceType, seqID, source);
						source.Unindent();
						source.AppendFront("}\n");
						source.AppendFront("else\n");
						source.AppendFront("{\n");
                        source.Indent();
                        EmitLazyOp(seqBin.Left, seqBin.Right, seq.SequenceType, seqID, source);
						source.Unindent();
						source.AppendFront("}\n");
					}
					else
					{
                        EmitLazyOp(seqBin.Left, seqBin.Right, seq.SequenceType, seqID, source);
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
					if(seqBin.Randomize)
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
                        source.AppendFront("res_" + seqID + " = res_" + xgrsSequenceIDs[seqBin.Left] + ";\n");
                        break;
                    } else if(seq.SequenceType==SequenceType.ThenRight) {
                        source.AppendFront("res_" + seqID + " = res_" + xgrsSequenceIDs[seqBin.Right] + ";\n");
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
				    source.AppendFront("res_" + seqID + " = res_" + xgrsSequenceIDs[seqBin.Left] + " "
					    + op + " res_" + xgrsSequenceIDs[seqBin.Right] + ";\n");
					break;
				}

                case SequenceType.IfThenElse:
                {
                    SequenceIfThenElse seqIf = (SequenceIfThenElse)seq;

                    EmitSequence(seqIf.Condition, source);

                    source.AppendFront("if(res_" + xgrsSequenceIDs[seqIf.Condition] + ")");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.TrueCase, source);
                    source.AppendFront("res_" + seqID + " = res_" + xgrsSequenceIDs[seqIf.TrueCase] + ";\n");

                    source.Unindent();
                    source.AppendFront("}\n");                 
                    source.AppendFront("else\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.FalseCase, source);
                    source.AppendFront("res_" + seqID + " = res_" + xgrsSequenceIDs[seqIf.FalseCase] + ";\n");

                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.For:
                {
                    SequenceFor seqFor = (SequenceFor)seq;

                    source.AppendFront("res_"+seqID+" = true;\n");
                    source.AppendFront("foreach(System.Collections.DictionaryEntry entry_"+seqID+" in var_"+seqFor.Setmap+")\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront("var_"+seqFor.Var+" = entry_"+seqID+".Key;\n");
                    if(seqFor.VarDst != null)
                    {
                        source.AppendFront("var_"+seqFor.VarDst+" = entry_"+seqID+".Value;\n");
                    }

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront("res_"+seqID+" &= res_"+xgrsSequenceIDs[seqFor.Seq]+";\n");
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

				case SequenceType.Min:
				{
					SequenceMin seqMin = (SequenceMin) seq;
					int seqMinSubID = xgrsSequenceIDs[seqMin.Seq];
					source.AppendFront("long i_" + seqID + " = 0;\n");
					source.AppendFront("while(true)\n");
					source.AppendFront("{\n");
					source.Indent();
					EmitSequence(seqMin.Seq, source);
					source.AppendFront("if(!res_" + seqMinSubID + ") break;\n");
					source.AppendFront("i_" + seqID + "++;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront("res_" + seqID + " = i_" + seqID + " >= " + seqMin.Min + ";\n");
					break;
				}

				case SequenceType.MinMax:
				{
					SequenceMinMax seqMinMax = (SequenceMinMax) seq;
					int seqMinMaxSubID = xgrsSequenceIDs[seqMinMax.Seq];
					source.AppendFront("long i_" + seqID + " = 0;\n");
					source.AppendFront("for(; i_" + seqID + " < " + seqMinMax.Max + "; i_" + seqID + "++)\n");
					source.AppendFront("{\n");
					source.Indent();
					EmitSequence(seqMinMax.Seq, source);
					source.AppendFront("if(!res_" + seqMinMaxSubID + ") break;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront("res_" + seqID + " = i_" + seqID + " >= " + seqMinMax.Min + ";\n");
					break;
				}

				case SequenceType.Def:
				{
					SequenceDef seqDef = (SequenceDef) seq;
					source.AppendFront("res_" + seqID + " = ");
					bool isFirst = true;
					foreach(String varName in seqDef.DefVars)
					{
						if(isFirst) isFirst = false;
						else source.Append(" && ");
						source.Append("var_" + varName + " != null");
					}
					source.Append(";\n");
					break;
				}

				case SequenceType.True:
				case SequenceType.False:
					source.AppendFront("res_" + seqID + " = " + (seq.SequenceType == SequenceType.True ? "true;\n" : "false;\n"));
					break;

                case SequenceType.SetmapAdd:
                {
                    SequenceSetmapAdd seqAdd = (SequenceSetmapAdd)seq;

                    source.AppendFront("if(var_"+seqAdd.Setmap+".Contains(var_"+seqAdd.Var+"))\n");
					source.AppendFront("{\n");
					source.Indent();
                    if(seqAdd.VarDst==null) {
                        source.AppendFront("var_"+seqAdd.Setmap+"[var_"+seqAdd.Var+"] = null;\n");
                    } else {
                        source.AppendFront("var_"+seqAdd.Setmap+"[var_"+seqAdd.Var+"] = var_"+seqAdd.VarDst+";\n");
                    }
					source.Unindent();
					source.AppendFront("}\n");
                    source.AppendFront("else\n");
					source.AppendFront("{\n");
					source.Indent();
                    if(seqAdd.VarDst==null) {
                        source.AppendFront("var_"+seqAdd.Setmap+".Add(var_"+seqAdd.Var+", null);\n");
                    } else {
                        source.AppendFront("var_"+seqAdd.Setmap+".Add(var_"+seqAdd.Var+", var_"+seqAdd.VarDst+");\n");
                    }
					source.Unindent();
					source.AppendFront("}\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    
                    break;
                }

                case SequenceType.SetmapRem:
                {
                    SequenceSetmapRem seqDel = (SequenceSetmapRem)seq;
                    source.AppendFront("var_"+seqDel.Setmap+".Remove(var_"+seqDel.Var+");\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.SetmapClear:
                {
                    SequenceSetmapClear seqClear = (SequenceSetmapClear)seq;
                    source.AppendFront("var_"+seqClear.Setmap+".Clear();\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.InSetmap:
                {
                    SequenceIn seqIn = (SequenceIn)seq;
                    source.AppendFront("res_"+seqID+" = var_"+seqIn.Setmap+".Contains(var_"+seqIn.Var+");\n");
                    break;
                }

                case SequenceType.IsVisited:
                {
                    SequenceIsVisited seqIsVisited = (SequenceIsVisited)seq;
                    source.AppendFront("res_"+seqID+" = graph.IsVisited((GRGEN_LIBGR.IGraphElement)var_"+seqIsVisited.GraphElementVar+", (int)var_"+seqIsVisited.VisitedFlagVar+");\n");
                    break;
                }

                case SequenceType.SetVisited:
                {
                    SequenceSetVisited seqSetVisited = (SequenceSetVisited)seq;
                    if(seqSetVisited.Var!=null) {
                        source.AppendFront("graph.SetVisited((GRGEN_LIBGR.IGraphElement)var_"+seqSetVisited.GraphElementVar
                            +", (int)var_"+seqSetVisited.VisitedFlagVar+", varbool_"+seqSetVisited.Var+");\n");
                    } else {
                        source.AppendFront("graph.SetVisited((GRGEN_LIBGR.IGraphElement)var_"+seqSetVisited.GraphElementVar
                            +", (int)var_"+seqSetVisited.VisitedFlagVar+", "+(seqSetVisited.Val?"true":"false")+");\n");
                    } 
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.VFree:
                {
                    SequenceVFree seqVFree = (SequenceVFree)seq;
                    source.AppendFront("graph.FreeVisitedFlag((int)var_"+seqVFree.VisitedFlagVar+");\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.VReset:
                {
                    SequenceVReset seqVReset = (SequenceVReset)seq;
                    source.AppendFront("graph.ResetVisitedFlag((int)var_"+seqVReset.VisitedFlagVar+");\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.Emit:
                {
                    SequenceEmit seqEmit = (SequenceEmit)seq;
                    if(seqEmit.IsVariable) {
                        source.AppendFront("graph.EmitWriter.Write(var_"+seqEmit.Text+".ToString());\n");
                    } else {
                        String text = seqEmit.Text.Replace("\n", "\\n");
                        text = text.Replace("\r", "\\r");
                        text = text.Replace("\t", "\\t");
                        source.AppendFront("graph.EmitWriter.Write(\""+text+"\");\n");
                    }
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.AssignVAllocToVar:
                {
                    SequenceAssignVAllocToVar seqVAllocToVar = (SequenceAssignVAllocToVar)seq;
                    source.AppendFront("var_"+seqVAllocToVar.DestVar+" = graph.AllocateVisitedFlag();\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.AssignSetmapSizeToVar:
                {
                    SequenceAssignSetmapSizeToVar seqSetmapSizeToVar = (SequenceAssignSetmapSizeToVar)seq;
                    source.AppendFront("var_"+seqSetmapSizeToVar.DestVar+" = var_"+seqSetmapSizeToVar.Setmap+".Count;\n"); 
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.AssignSetmapEmptyToVar:
                {
                    SequenceAssignSetmapEmptyToVar seqSetmapEmptyToVar = (SequenceAssignSetmapEmptyToVar)seq;
                    source.AppendFront("var_"+seqSetmapEmptyToVar.DestVar+" = var_"+seqSetmapEmptyToVar.Setmap+".Count==0;\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.AssignMapAccessToVar:
                {
                    SequenceAssignMapAccessToVar seqMapAccessToVar = (SequenceAssignMapAccessToVar)seq;
                    source.AppendFront("res_"+seqID+" = false;\n");
                    source.AppendFront("if(var_"+seqMapAccessToVar.Setmap+".Contains(var_"+seqMapAccessToVar.KeyVar+")) {\n");
                    source.Indent();
                    source.AppendFront("var_"+seqMapAccessToVar.DestVar+" = var_"+seqMapAccessToVar.Setmap+"[ var_"+seqMapAccessToVar.KeyVar+" ];\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.AssignSetCreationToVar:
                {
                    SequenceAssignSetCreationToVar seqSetCreationToVar = (SequenceAssignSetCreationToVar)seq;
                    source.AppendFront("var_"+seqSetCreationToVar.DestVar+" = GRGEN_LIBGR.DictionaryHelper.NewDictionary(\n");
                    source.AppendFront("  GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+seqSetCreationToVar.TypeName+"\", graph), \n");
                    source.AppendFront("  typeof(de.unika.ipd.grGen.libGr.SetValueType));\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

                case SequenceType.AssignMapCreationToVar:
                {
                    SequenceAssignMapCreationToVar seqMapCreationToVar = (SequenceAssignMapCreationToVar)seq;
                    source.AppendFront("var_"+seqMapCreationToVar.DestVar+" = de.unika.ipd.grGen.libGr.DictionaryHelper.NewDictionary(\n");
                    source.AppendFront("  GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+seqMapCreationToVar.TypeName+"\", graph), \n");
                    source.AppendFront("  GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+seqMapCreationToVar.TypeNameDst+"\", graph));\n");
                    source.AppendFront("res_"+seqID+" = true;\n");
                    break;
                }

				case SequenceType.AssignVarToVar:
				{
					SequenceAssignVarToVar seqVarToVar = (SequenceAssignVarToVar) seq;
					source.AppendFront("var_" + seqVarToVar.DestVar + " = var_" + seqVarToVar.SourceVar + ";\n");
					source.AppendFront("res_" + seqID + " = true;\n");
					break;
				}

                case SequenceType.AssignElemToVar:
                    throw new Exception("AssignElemToVar not supported, yet");

                case SequenceType.AssignSequenceResultToVar:
                    {
                        SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar)seq;
                        int seqSubID = xgrsSequenceIDs[seqToVar.Seq];
                        EmitSequence(seqToVar.Seq, source);
                        source.AppendFront("res_" + seqID + " = varbool_" + seqToVar.DestVar
						+ " = res_" + seqSubID + ";\n");
                        break;
                    }

                case SequenceType.AssignConstToVar:
                {
                    // currently only boolean values supported, compiled xgrs don't support more at the moment
                    SequenceAssignConstToVar seqConstToVar = (SequenceAssignConstToVar)seq;
                    source.AppendFront("varbool_" + seqConstToVar.DestVar + " = " + (((bool)seqConstToVar.Constant)?"true":"false") + ";\n");
                    source.AppendFront("res_" + seqID + " = true;\n");
                    break;
                }

                case SequenceType.AssignAttributeToVar:
                    throw new Exception("AssignAttributeToVar not supported, yet");

                case SequenceType.AssignVarToAttribute:
                    throw new Exception("AssignVarToAttribute not supported, yet");

				case SequenceType.Transaction:
				{
					SequenceTransaction seqTrans = (SequenceTransaction) seq;
					int seqTransSubID = xgrsSequenceIDs[seqTrans.Seq];
                    source.AppendFront("int transID_" + seqID + " = graph.TransactionManager.StartTransaction();\n");
					EmitSequence(seqTrans.Seq, source);
                    source.AppendFront("if(res_" + seqTransSubID + ") graph.TransactionManager.Commit(transID_" + seqID + ");\n");
                    source.AppendFront("else graph.TransactionManager.Rollback(transID_" + seqID + ");\n");
                    source.AppendFront("res_" + seqID + " = res_" + seqTransSubID + ";\n");
					break;
				}

				default:
					throw new Exception("Unknown sequence type: " + seq.SequenceType);
			}
		}

        // emits matcher class static arrays for parameter transmission into rules
        // Must be called after EmitNeededVars as sequence IDs are needed.
        void EmitStaticVars(Sequence seq, SourceBuilder source)
        {
            switch(seq.SequenceType)
            {
                case SequenceType.Rule:
                case SequenceType.RuleAll:
                    {
                        SequenceRule seqRule = (SequenceRule) seq;
                        int paramLen = seqRule.RuleObj.ParamVars.Length;
                        if(!xgrsParamArrays.ContainsKey(paramLen))
                        {
                            xgrsParamArrays[paramLen] = null;
                            source.AppendFront("private static object[] __xgrs_paramarray_" + paramLen
                                + " = new object[" + paramLen + "];\n");
                        }
                        break;
                    }

                default:
                    foreach(Sequence childSeq in seq.Children)
                        EmitStaticVars(childSeq, source);
                    break;
            }
        }

		public bool GenerateXGRSCode(int xgrsID, String xgrsStr, String[] paramNames, SourceBuilder source, IGraphModel model)
		{
			Dictionary<String, String> varDecls = new Dictionary<String, String>();

			Sequence seq;
			try
			{
				seq = SequenceParser.ParseSequence(xgrsStr, null, varDecls);
			}
			catch(ParseException ex)
			{
				Console.Error.WriteLine("The exec statement \"" + xgrsStr
					+ "\" caused the following error:\n" + ex.Message);
				return false;
			}

            source.AppendFront("public static bool ApplyXGRS_" + xgrsID + "(GRGEN_LGSP.LGSPGraph graph");
			for(int i = 0; i < paramNames.Length; i++)
			{
				source.Append(", object var_");
				source.Append(paramNames[i]);
			}
			source.Append(")\n");
			source.AppendFront("{\n");
			source.Indent();

            source.AppendFront("GRGEN_LGSP.LGSPActions actions = graph.curActions;\n");

			xgrsVars.Clear();
			xgrsNextSequenceID = 0;
			xgrsSequenceIDs.Clear();
			xgrsRules.Clear();
            if(xgrsID == 0)                     // First XGRS in this rule?
                xgrsParamArrays.Clear();        // No param arrays created, yet

			foreach(String param in paramNames)
				xgrsVars.Add(param, null);
			AssignIdsAndEmitNeededVarAndRuleEntities(seq, source, model);

			EmitSequence(seq, source);
			source.AppendFront("return res_" + xgrsSequenceIDs[seq] + ";\n");
			source.Unindent();
			source.AppendFront("}\n");

            EmitStaticVars(seq, source);

			return true;
		}
    }
}
