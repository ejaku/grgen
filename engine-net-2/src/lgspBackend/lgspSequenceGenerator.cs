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
        /// <param name="ruleNames">The names of the available rules.</param>
        /// <param name="parameters">The names of the needed graph elements of the containing action.</param>
        /// <param name="parameterTypes">The types of the needed graph elements of the containing action.</param>
        /// <param name="xgrs">The XGRS string.</param>
		public LGSPXGRSInfo(String[] ruleNames, String[] parameters, String[] parameterTypes, String xgrs)
		{
            RuleNames = ruleNames;
			Parameters = parameters;
            ParameterTypes = parameterTypes;
			XGRS = xgrs;
		}

        /// <summary>
        /// The names of the available rules.
        /// </summary>
        public String[] RuleNames;

        /// <summary>
        /// The names of the needed graph elements of the containing action.
        /// </summary>
		public String[] Parameters;

        /// <summary>
        /// The types of the needed graph elements of the containing action.
        /// </summary>
        public String[] ParameterTypes;

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
        IGraphModel model;
        Dictionary<String, List<String>> rulesToInputTypes;
        Dictionary<String, List<String>> rulesToOutputTypes;

		Dictionary<String, object> knownRules = new Dictionary<string, object>();

        public LGSPSequenceGenerator(LGSPGrGen gen, IGraphModel model,
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes)
        {
            this.gen = gen;
            this.model = model;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
        }

        /// <summary>
        /// Returns string containing a C# expression to get the value of the sequence-local variable / graph-global variable given
        /// </summary>
        public string GetVar(SequenceVariable seqVar)
        {
            if(seqVar.Type == "")
            {
                return "graph.GetVariableValue(" + seqVar.Name + ")";
            }
            else
            {
                return "var_" + seqVar.Prefix + seqVar.Name;
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
                return "graph.SetVariableValue(" + seqVar.Name + ", " + valueToWrite + ");\n";
            }
            else
            {
                return "var_" + seqVar.Prefix + seqVar.Name + " = " + valueToWrite + ";\n";
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
                sb.Append("var_" + seqVar.Prefix + seqVar.Name);
                sb.Append(" = ");
                sb.Append(TypesHelper.DefaultValue(seqVar.Type, model));
                sb.Append(";\n");
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Returns string containing a C# expression to get the value of the result variable of the sequence given
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public string GetResultVar(Sequence seq)
        {
            return "res_" + seq.Id;
        }

        /// <summary>
        /// Returns string containing a C# assignment to set the result variable of the sequence given
        /// to the value as computed by the C# expression in the string given 
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public string SetResultVar(Sequence seq, String valueToWrite)
        {
            return "res_" + seq.Id + " = " + valueToWrite + ";\n";
        }

        /// <summary>
        /// Returns string containing C# declaration of the sequence result variable
        /// </summary>
        public string DeclareResultVar(Sequence seq)
        {
            return "bool res_" + seq.Id + ";\n";
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

        /*void EmitSetmapVarIfNew(String varName, String type, String typeKey, String typeValue, SourceBuilder source)
        {
            if(!vars.ContainsKey(varName))
            {
                vars.Add(varName, "IDictionary");
                // todo: use exact types in compiled xgrs
                //typeKey = DictionaryHelper.GetQualifiedTypeName(typeKey, model);
                //typeValue = DictionaryHelper.GetQualifiedTypeName(typeValue, model);
                //source.AppendFront("IDictionary<"+typeKey+","+typeValue+"> var_" + varName + " = null;\n");
                source.AppendFront("System.Collections.IDictionary var_" + varName + " = null;\n");
            }
        }*/ // TODO remove

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits result variable declarations
        /// - emits xgrs variable declarations (only once for every variable)
        /// - collects used rules into knownRules, emit local rule declaration (only once for every rule)
        /// </summary>
		void EmitNeededVarAndRuleEntities(Sequence seq, SourceBuilder source)
		{
			source.AppendFront(DeclareResultVar(seq));

			switch(seq.SequenceType)
			{
				case SequenceType.AssignElemToVar:
				{
					SequenceAssignElemToVar elemToVar = (SequenceAssignElemToVar) seq;
					EmitVarIfNew(elemToVar.DestVar, source);
					break;
				}
				case SequenceType.AssignVarToVar:		// TODO: Load from external vars?
				{
					SequenceAssignVarToVar varToVar = (SequenceAssignVarToVar) seq;
                    EmitVarIfNew(varToVar.DestVar, source);
					break;
				}
                case SequenceType.AssignVAllocToVar:
                {
                    SequenceAssignVAllocToVar vallocToVar = (SequenceAssignVAllocToVar) seq;
                    EmitVarIfNew(vallocToVar.DestVar, source);
                    break;
                }
                case SequenceType.AssignSetmapSizeToVar:
                {
                    SequenceAssignSetmapSizeToVar seqSetmapSizeToVar = (SequenceAssignSetmapSizeToVar)seq;
                    EmitVarIfNew(seqSetmapSizeToVar.DestVar, source); 
                    break;
                }
                case SequenceType.AssignSetmapEmptyToVar:
                {
                    SequenceAssignSetmapEmptyToVar seqSetmapEmptyToVar = (SequenceAssignSetmapEmptyToVar)seq;
                    EmitVarIfNew(seqSetmapEmptyToVar.DestVar, source); 
                    break;
                }
                case SequenceType.AssignMapAccessToVar:
                {
                    SequenceAssignMapAccessToVar seqMapAccessToVar = (SequenceAssignMapAccessToVar)seq;
                    EmitVarIfNew(seqMapAccessToVar.DestVar, source); 
                    break;
                }
                case SequenceType.AssignSetCreationToVar:
                {
                    SequenceAssignSetCreationToVar seqToVar = (SequenceAssignSetCreationToVar)seq;
                    EmitVarIfNew(seqToVar.DestVar, source);
                    break;
                }
                case SequenceType.AssignMapCreationToVar:
                {
                    SequenceAssignMapCreationToVar seqToVar = (SequenceAssignMapCreationToVar)seq;
                    EmitVarIfNew(seqToVar.DestVar, source);
                    break;
                }
				case SequenceType.AssignSequenceResultToVar:
				{
					SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar) seq;
                    EmitVarIfNew(seqToVar.DestVar, source);
					EmitNeededVarAndRuleEntities(seqToVar.Seq, source);
					break;
				}
                case SequenceType.AssignConstToVar:
                {
                    SequenceAssignConstToVar constToVar = (SequenceAssignConstToVar)seq;
                    EmitVarIfNew(constToVar.DestVar, source);
                    break;
                }

				case SequenceType.Rule:
				case SequenceType.RuleAll:
				{
					SequenceRule seqRule = (SequenceRule) seq;
					String ruleName = seqRule.ParamBindings.RuleName;
					if(!knownRules.ContainsKey(ruleName))
					{
                        knownRules.Add(ruleName, null);
                        source.AppendFront("Action_" + ruleName + " " + "rule_" + ruleName);
                        source.Append(" = Action_" + ruleName + ".Instance;\n");
                    }
                    for(int i=0; i<seqRule.ParamBindings.ParamVars.Length; ++i)
                    {
                        SequenceVariable paramVar = seqRule.ParamBindings.ParamVars[i];
                        if(paramVar != null)
                            EmitVarIfNew(paramVar, source);
                    }
                    for(int i=0; i<seqRule.ParamBindings.ReturnVars.Length; ++i)
                    {
                        EmitVarIfNew(seqRule.ParamBindings.ReturnVars[i], source);
                    }
					break;
				}

                case SequenceType.For:
                {
                    SequenceFor seqFor = (SequenceFor)seq;
                    EmitVarIfNew(seqFor.Var, source);
                    if (seqFor.VarDst != null) EmitVarIfNew(seqFor.VarDst, source);
                    EmitNeededVarAndRuleEntities(seqFor.Seq, source);
                    break;
                }

				default:
					foreach(Sequence childSeq in seq.Children)
						EmitNeededVarAndRuleEntities(childSeq, source);
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

        void EmitRuleOrRuleAll(SequenceRule seqRule, SourceBuilder source)
        {
            RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
            String specialStr = seqRule.Special ? "true" : "false";
            int paramLen = paramBindings.ParamVars.Length;
            String parameters = "";
            for(int i = 0; i < paramLen; i++)
            {
                if(paramBindings.ParamVars[i] != null)
                    parameters += ", " + GetVar(paramBindings.ParamVars[i]);
                else
                {
                    object arg = paramBindings.Parameters[i];
                    if(arg is bool)
                        parameters += ", " + ((bool)arg ? "true" : "false");
                    else if(arg is string)
                        parameters += ", \"" + (string)arg + "\"";
                    else if(arg is float)
                        parameters += "," + ((float)arg).ToString(System.Globalization.CultureInfo.InvariantCulture) + "f";
                    else if(arg is double)
                        parameters += "," + ((double)arg).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    else // e.g. int
                        parameters += "," + arg.ToString();
                    // TODO: abolish constants as parameters or extend to set/map?
                }
            }
            String matchingPatternClassName = "Rule_" + paramBindings.RuleName;
            String patternName = paramBindings.RuleName;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seqRule.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seqRule.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.RuleName
                + ".Match(graph, " + (seqRule.SequenceType == SequenceType.Rule ? "1" : "graph.MaxMatches")
                + parameters + ");\n");
            if(gen.FireEvents) source.AppendFront("graph.Matched(" + matchesName + ", " + specialStr + ");\n");
            source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seqRule, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seqRule, "true"));
            if(gen.UsePerfInfo)
                source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(gen.FireEvents) source.AppendFront("graph.Finishing(" + matchesName + ", " + specialStr + ");\n");

            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values, 
            // and finally we assign these temporary variables to the real xgrs variables
            String returnParameterDeclarations = "";
            String returnArguments = "";
            String returnAssignments = "";
            for(int i = 0; i < paramBindings.ReturnVars.Length; i++)
            {
                IEnumerator<int> a;
                String varName = paramBindings.ReturnVars[i].Prefix + paramBindings.ReturnVars[i].Name;
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(rulesToOutputTypes[paramBindings.RuleName][i], model) + " tmpvar_" + varName + "; ";
                returnArguments += ", out tmpvar_" + varName;
                returnAssignments += "var_" + varName + " = tmpvar_" + varName + "; ";
            }

            if(seqRule.SequenceType == SequenceType.Rule)
            {
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.RuleName + ".Modify(graph, " + matchName + returnArguments + ");\n");
                source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo)
                    source.AppendFront("if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;\n");
            }
            else // seq.SequenceType == SequenceType.RuleAll
            {
                String enumeratorName = "enum_" + seqRule.Id;
                if(((SequenceRuleAll)seqRule).NumChooseRandom <= 0)
                {
                    // iterate through matches, use Modify on each, fire the next match event after the first
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.RuleName + ".Modify(graph, " + matchName + returnArguments + ");\n");
                    source.AppendFront(returnAssignments + "\n");
                    source.AppendFront("if(" + matchName + "!=" + matchesName + ".FirstExact) graph.RewritingNextMatch();");
                    if(gen.UsePerfInfo)
                        source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                }
                else
                {
                    // todo: bisher gibt es eine exception wenn zu wenig matches für die random-auswahl vorhanden, auch bei interpretiert
                    // -- das so ändern, dass geclippt nach vorhandenen matches - das andere ist für die nutzer scheisse (wohl noch nie vorgekommen, weil immer nur ein match gewählt wurde)

                    // compute indices to select, to reach same behaviour as interpreted version, using an available indices and a selected indices list
                    String matchesToSelect = ((SequenceRuleAll)seqRule).NumChooseRandom.ToString();
                    source.AppendFront("List<int> availableIndices = new List<int>(" + matchesName + ".Count);\n");
                    source.AppendFront("for(int i=0; i<" + matchesName + ".Count; ++i) availableIndices.Add(i);\n");
                    source.AppendFront("List<int> selectedIndices = new List<int>(" + matchesToSelect + ");\n");
                    source.AppendFront("for(int i=0; i<" + matchesToSelect + "; ++i) {\n");
                    source.Indent();
                    source.AppendFront("int index = GRGEN_LIBGR.Sequence.randomGenerator.Next(availableIndices.Count);\n");
                    source.AppendFront("selectedIndices.Add(availableIndices[index]);\n");
                    source.AppendFront("availableIndices.RemoveAt(index);\n");
                    source.Unindent();
                    source.AppendFront("}\n");

                    // iterate through matches, use Modify on matches of selected indices, fire the next match event after the first
                    String matchCounter = "matchCounter_" + seqRule.Id;
                    source.AppendFront("int " + matchCounter + " = 0;\n");
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    source.AppendFront("if(" + matchCounter + "==selectedIndices[0])\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.RuleName + ".Modify(graph, " + matchName + returnArguments + ");\n");
                    source.AppendFront(returnAssignments + "\n");
                    source.AppendFront("if(selectedIndices.Count!=" + matchesToSelect + ") graph.RewritingNextMatch();");
                    if(gen.UsePerfInfo)
                        source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront("selectedIndices.RemoveAt(0);");
                    source.AppendFront("if(selectedIndices.Count==0) break;");
                    source.Unindent();
                    source.AppendFront("}\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                }
            }

            if(gen.FireEvents) source.AppendFront("graph.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

		void EmitSequence(Sequence seq, SourceBuilder source)
		{
			switch(seq.SequenceType)
			{
				case SequenceType.Rule:
                case SequenceType.RuleAll:
                    EmitRuleOrRuleAll((SequenceRule)seq, source);
                    break;

				case SequenceType.VarPredicate:
				{
					SequenceVarPredicate seqPred = (SequenceVarPredicate) seq;
					source.AppendFront(SetResultVar(seqPred, GetVar(seqPred.PredicateVar)));
					break;
				}

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
					if(seqBin.Randomize)
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

                case SequenceType.For:
                {
                    SequenceFor seqFor = (SequenceFor)seq;

                    String srcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Setmap.Type), model);
                    String dstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqFor.Setmap.Type), model);
                    source.AppendFront(SetResultVar(seqFor, "true"));
                    source.AppendFront("foreach(KeyValuePair<"+srcType+","+dstType+"> entry_"+seqFor.Id+" in "+GetVar(seqFor.Setmap)+")\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront(SetVar(seqFor.Var, "entry_"+seqFor.Id+".Key"));
                    if(seqFor.VarDst != null)
                    {
                        source.AppendFront(SetVar(seqFor.VarDst, "entry_"+seqFor.Id+".Value"));
                    }

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
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

				case SequenceType.Def:
				{
					SequenceDef seqDef = (SequenceDef) seq;
                    String condition = "";
					bool isFirst = true;
					foreach(SequenceVariable var in seqDef.DefVars)
					{
						if(isFirst) isFirst = false;
						else condition += " && ";
						condition += GetVar(var) + "!=null";
					}
					source.AppendFront(SetResultVar(seqDef, condition));
					break;
				}

				case SequenceType.True:
				case SequenceType.False:
					source.AppendFront(SetResultVar(seq, (seq.SequenceType == SequenceType.True ? "true" : "false")));
					break;

                case SequenceType.SetmapAdd:
                {
                    SequenceSetmapAdd seqAdd = (SequenceSetmapAdd)seq;

                    source.AppendFront("if("+GetVar(seqAdd.Setmap)+".Contains("+GetVar(seqAdd.Var)+"))\n");
					source.AppendFront("{\n");
					source.Indent();
                    if(seqAdd.VarDst==null) {
                        source.AppendFront(GetVar(seqAdd.Setmap)+"["+GetVar(seqAdd.Var)+"] = null;\n");
                    } else {
                        source.AppendFront(GetVar(seqAdd.Setmap)+"["+GetVar(seqAdd.Var)+"] = "+GetVar(seqAdd.VarDst)+";\n");
                    }
					source.Unindent();
					source.AppendFront("}\n");
                    source.AppendFront("else\n");
					source.AppendFront("{\n");
					source.Indent();
                    if(seqAdd.VarDst==null) {
                        source.AppendFront(GetVar(seqAdd.Setmap)+".Add("+GetVar(seqAdd.Var)+", null);\n");
                    } else {
                        source.AppendFront(GetVar(seqAdd.Setmap)+".Add("+GetVar(seqAdd.Var)+", "+GetVar(seqAdd.VarDst)+");\n");
                    }
					source.Unindent();
					source.AppendFront("}\n");
                    source.AppendFront(SetResultVar(seqAdd, "true"));
                    
                    break;
                }

                case SequenceType.SetmapRem:
                {
                    SequenceSetmapRem seqDel = (SequenceSetmapRem)seq;
                    source.AppendFront(GetVar(seqDel.Setmap)+".Remove("+GetVar(seqDel.Var)+");\n");
                    source.AppendFront(SetResultVar(seqDel, "true"));
                    break;
                }

                case SequenceType.SetmapClear:
                {
                    SequenceSetmapClear seqClear = (SequenceSetmapClear)seq;
                    source.AppendFront(GetVar(seqClear.Setmap)+".Clear();\n");
                    source.AppendFront(SetResultVar(seqClear, "true"));
                    break;
                }

                case SequenceType.InSetmap:
                {
                    SequenceIn seqIn = (SequenceIn)seq;
                    source.AppendFront(SetResultVar(seqIn, GetVar(seqIn.Setmap)+".Contains("+GetVar(seqIn.Var)+")"));
                    break;
                }

                case SequenceType.IsVisited:
                {
                    SequenceIsVisited seqIsVisited = (SequenceIsVisited)seq;
                    source.AppendFront(SetResultVar(seqIsVisited, "graph.IsVisited("
                        + "(GRGEN_LIBGR.IGraphElement)"+GetVar(seqIsVisited.GraphElementVar)
                        +", (int)"+GetVar(seqIsVisited.VisitedFlagVar)
                        +")"));
                    break;
                }

                case SequenceType.SetVisited:
                {
                    SequenceSetVisited seqSetVisited = (SequenceSetVisited)seq;
                    if(seqSetVisited.Var!=null) {
                        source.AppendFront("graph.SetVisited((GRGEN_LIBGR.IGraphElement)"+GetVar(seqSetVisited.GraphElementVar)
                            +", (int)"+GetVar(seqSetVisited.VisitedFlagVar)+", "+GetVar(seqSetVisited.Var)+");\n");
                    } else {
                        source.AppendFront("graph.SetVisited((GRGEN_LIBGR.IGraphElement)"+GetVar(seqSetVisited.GraphElementVar)
                            +", (int)"+GetVar(seqSetVisited.VisitedFlagVar)+", "+(seqSetVisited.Val?"true":"false")+");\n");
                    } 
                    source.AppendFront(SetResultVar(seqSetVisited, "true"));
                    break;
                }

                case SequenceType.VFree:
                {
                    SequenceVFree seqVFree = (SequenceVFree)seq;
                    source.AppendFront("graph.FreeVisitedFlag((int)"+GetVar(seqVFree.VisitedFlagVar)+");\n");
                    source.AppendFront(SetResultVar(seqVFree, "true"));
                    break;
                }

                case SequenceType.VReset:
                {
                    SequenceVReset seqVReset = (SequenceVReset)seq;
                    source.AppendFront("graph.ResetVisitedFlag((int)"+GetVar(seqVReset.VisitedFlagVar)+");\n");
                    source.AppendFront(SetResultVar(seqVReset, "true"));
                    break;
                }

                case SequenceType.Emit:
                {
                    SequenceEmit seqEmit = (SequenceEmit)seq;
                    if(seqEmit.Variable!=null) {
                        source.AppendFront("graph.EmitWriter.Write("+GetVar(seqEmit.Variable)+".ToString());\n");
                    } else {
                        String text = seqEmit.Text.Replace("\n", "\\n");
                        text = text.Replace("\r", "\\r");
                        text = text.Replace("\t", "\\t");
                        source.AppendFront("graph.EmitWriter.Write(\""+text+"\");\n");
                    }
                    source.AppendFront(SetResultVar(seqEmit, "true"));
                    break;
                }

                case SequenceType.AssignVAllocToVar:
                {
                    SequenceAssignVAllocToVar seqVAllocToVar = (SequenceAssignVAllocToVar)seq;
                    source.AppendFront(SetVar(seqVAllocToVar.DestVar, "graph.AllocateVisitedFlag()"));
                    source.AppendFront(SetResultVar(seqVAllocToVar, "true"));
                    break;
                }

                case SequenceType.AssignSetmapSizeToVar:
                {
                    SequenceAssignSetmapSizeToVar seqSetmapSizeToVar = (SequenceAssignSetmapSizeToVar)seq;
                    source.AppendFront(SetVar(seqSetmapSizeToVar.DestVar, GetVar(seqSetmapSizeToVar.Setmap)+".Count")); 
                    source.AppendFront(SetResultVar(seqSetmapSizeToVar, "true"));
                    break;
                }

                case SequenceType.AssignSetmapEmptyToVar:
                {
                    SequenceAssignSetmapEmptyToVar seqSetmapEmptyToVar = (SequenceAssignSetmapEmptyToVar)seq;
                    source.AppendFront(SetVar(seqSetmapEmptyToVar.DestVar, GetVar(seqSetmapEmptyToVar.Setmap)+".Count==0"));
                    source.AppendFront(SetResultVar(seqSetmapEmptyToVar, "true"));
                    break;
                }

                case SequenceType.AssignMapAccessToVar:
                {
                    SequenceAssignMapAccessToVar seqMapAccessToVar = (SequenceAssignMapAccessToVar)seq;
                    source.AppendFront(SetResultVar(seqMapAccessToVar, "false"));
                    source.AppendFront("if("+GetVar(seqMapAccessToVar.Setmap)+".Contains("+GetVar(seqMapAccessToVar.KeyVar)+")) {\n");
                    source.Indent();
                    source.AppendFront(SetVar(seqMapAccessToVar.DestVar, GetVar(seqMapAccessToVar.Setmap)+"["+GetVar(seqMapAccessToVar.KeyVar)+"]"));
                    source.AppendFront(SetResultVar(seqMapAccessToVar, "true"));
                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.AssignSetCreationToVar:
                {
                    SequenceAssignSetCreationToVar seqSetCreationToVar = (SequenceAssignSetCreationToVar)seq;
                    String srcType = "GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+seqSetCreationToVar.TypeName+"\", graph)";
                    String dstType = "typeof(de.unika.ipd.grGen.libGr.SetValueType)";
                    source.AppendFront(SetVar(seqSetCreationToVar.DestVar, "GRGEN_LIBGR.DictionaryHelper.NewDictionary("+srcType+", "+dstType+")"));
                    source.AppendFront(SetResultVar(seqSetCreationToVar, "true"));
                    break;
                }

                case SequenceType.AssignMapCreationToVar:
                {
                    SequenceAssignMapCreationToVar seqMapCreationToVar = (SequenceAssignMapCreationToVar)seq;
                    String srcType = "GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+seqMapCreationToVar.TypeName+"\", graph)";
                    String dstType = "GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+seqMapCreationToVar.TypeNameDst+"\", graph)";
                    source.AppendFront(SetVar(seqMapCreationToVar.DestVar, "GRGEN_LIBGR.DictionaryHelper.NewDictionary("+srcType+", "+dstType+")"));
                    source.AppendFront(SetResultVar(seqMapCreationToVar, "true"));
                    break;
                }

				case SequenceType.AssignVarToVar:
				{
					SequenceAssignVarToVar seqVarToVar = (SequenceAssignVarToVar) seq;
					source.AppendFront(SetVar(seqVarToVar.DestVar, GetVar(seqVarToVar.SourceVar)));
					source.AppendFront(SetResultVar(seqVarToVar, "true"));
					break;
				}

                case SequenceType.AssignElemToVar:
                {
                    SequenceAssignElemToVar seqElemToVar = (SequenceAssignElemToVar)seq;
                    source.AppendFront("if(!(graph is NamedGraph)) throw new InvalidOperationException(\"The @-operator can only be used with NamedGraphs!\");\n");
                    source.AppendFront("IGraphElement elem_"+seqElemToVar.Id+" = ((NamedGraph)namedGraph).GetGraphElement(\"" + seqElemToVar.ElementName + "\");");
                    source.AppendFront("if(elem_"+seqElemToVar.Id+" == null) throw new InvalidOperationException(\"Graph element does not exist: \"" + seqElemToVar.ElementName + "\"!\");");
                    source.AppendFront(SetVar(seqElemToVar.DestVar, "elem_"+seqElemToVar.Id));
                    source.AppendFront(SetResultVar(seqElemToVar, "true"));
                    break;
                }

                case SequenceType.AssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(SetVar(seqToVar.DestVar, GetResultVar(seqToVar.Seq)));
                    source.AppendFront(SetResultVar(seqToVar, GetVar(seqToVar.DestVar)));
                    break;
                }

                case SequenceType.AssignConstToVar:
                {
                    SequenceAssignConstToVar seqConstToVar = (SequenceAssignConstToVar)seq;
                    source.AppendFront(SetVar(seqConstToVar.DestVar, seqConstToVar.Constant.ToString()));
                    source.AppendFront(SetResultVar(seqConstToVar, "true"));
                    break;
                }

                case SequenceType.AssignAttributeToVar:
                {
                    SequenceAssignAttributeToVar seqAttrToVar = (SequenceAssignAttributeToVar)seq;
                    source.AppendFront("object value_"+seqAttrToVar.Id+" = " + GetVar(seqAttrToVar.SourceVar) + ";\n");
                    source.AppendFront("IGraphElement elem_"+seqAttrToVar.Id+" = (IGraphElement)" + GetVar(seqAttrToVar.DestVar) + ";\n");
                    source.AppendFront("AttributeType attrType_"+seqAttrToVar.Id+";\n");
                    source.AppendFront("value_"+seqAttrToVar.Id+" = DictionaryHelper.IfAttributeOfElementIsDictionaryThenCloneDictionaryValue(elem_"+seqAttrToVar.Id+", \"" + seqAttrToVar.AttributeName + "\", value_"+seqAttrToVar.Id+", out attrType"+seqAttrToVar.Id+");\n");
                    source.AppendFront("AttributeChangeType changeType_"+seqAttrToVar.Id+" = AttributeChangeType.Assign;\n");
                    source.AppendFront("if(elem_"+seqAttrToVar.Id+" is INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((INode)elem_"+seqAttrToVar.Id+", attrType_"+seqAttrToVar.Id+", changeType_"+seqAttrToVar.Id+", value_"+seqAttrToVar.Id+", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((IEdge)elem_"+seqAttrToVar.Id+", attrType_"+seqAttrToVar.Id+", changeType_"+seqAttrToVar.Id+", value_"+seqAttrToVar.Id+", null);\n");
                    source.AppendFront("elem_"+seqAttrToVar.Id+".SetAttribute(\"" + seqAttrToVar.AttributeName + "\", value_"+seqAttrToVar.Id+");\n");
                    source.AppendFront(SetResultVar(seqAttrToVar, "true"));
                    break;
                }

                case SequenceType.AssignVarToAttribute:
                {
                    SequenceAssignVarToAttribute seqVarToAttr = (SequenceAssignVarToAttribute)seq;
                    source.AppendFront("IGraphElement elem_"+seqVarToAttr.Id+" = (IGraphElement)" + GetVar(seqVarToAttr.SourceVar) + ";\n");
                    source.AppendFront("object value_"+seqVarToAttr.Id+" = elem_"+seqVarToAttr.Id+".GetAttribute(\""+seqVarToAttr.AttributeName+"\");\n");
                    source.AppendFront("AttributeType attrType_"+seqVarToAttr.Id+";\n");
                    source.AppendFront("value_"+seqVarToAttr.Id+" = DictionaryHelper.IfAttributeOfElementIsDictionaryThenCloneDictionaryValue(elem_"+seqVarToAttr.Id+", \"" + seqVarToAttr.AttributeName + "\", value_"+seqVarToAttr.Id+", out attrType_"+seqVarToAttr.Id+");\n");
                    source.AppendFront(SetVar(seqVarToAttr.DestVar, "value_"+seqVarToAttr.Id));
                    source.AppendFront(SetResultVar(seqVarToAttr, "true"));
                    break;
                }

				case SequenceType.Transaction:
				{
					SequenceTransaction seqTrans = (SequenceTransaction) seq;
                    source.AppendFront("int transID_" + seqTrans.Id + " = graph.TransactionManager.StartTransaction();\n");
					EmitSequence(seqTrans.Seq, source);
                    source.AppendFront("if("+ GetResultVar(seqTrans.Seq) + ") graph.TransactionManager.Commit(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront("else graph.TransactionManager.Rollback(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront(SetResultVar(seqTrans, GetResultVar(seqTrans.Seq)));
					break;
				}

				default:
					throw new Exception("Unknown sequence type: " + seq.SequenceType);
			}
		}

		public bool GenerateXGRSCode(int xgrsID, String xgrsStr, String[] ruleNames, String[] paramNames, String[] paramTypes, 
                                     SourceBuilder source)
		{
			Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for (int i = 0; i < paramNames.Length; i++)
            {
                varDecls.Add(paramNames[i], paramTypes[i]);
            }

			Sequence seq;
			try
			{
				seq = SequenceParser.ParseSequence(xgrsStr, ruleNames, varDecls);
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
				source.Append(", " + paramTypes[i] + " var_");
				source.Append(paramNames[i]);
			}
			source.Append(")\n");
			source.AppendFront("{\n");
			source.Indent();

            source.AppendFront("GRGEN_LGSP.LGSPActions actions = graph.curActions;\n");

			knownRules.Clear();

  			EmitNeededVarAndRuleEntities(seq, source);

			EmitSequence(seq, source);

            source.AppendFront("return " + GetResultVar(seq) + ";\n");
			source.Unindent();
			source.AppendFront("}\n");

			return true;
		}
    }
}
