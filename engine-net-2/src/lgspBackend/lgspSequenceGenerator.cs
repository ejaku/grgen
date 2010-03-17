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
        /// <param name="parameters">The names of the needed graph elements of the containing action.</param>
        /// <param name="parameterTypes">The types of the needed graph elements of the containing action.</param>
        /// <param name="xgrs">The XGRS string.</param>
		public LGSPXGRSInfo(String[] parameters, GrGenType[] parameterTypes, String xgrs)
		{
			Parameters = parameters;
            ParameterTypes = parameterTypes;
			XGRS = xgrs;
		}

        /// <summary>
        /// The names of the needed graph elements of the containing action.
        /// </summary>
		public String[] Parameters;

        /// <summary>
        /// The types of the needed graph elements of the containing action.
        /// </summary>
        public GrGenType[] ParameterTypes;

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
        // the generator using us
        LGSPGrGen gen;

        // the model object of the .grg to compile
        IGraphModel model;

        // maps rule names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> rulesToOutputTypes;

        // the used rules (so that a variable was created for easy acess to them)
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
                return "graph.GetVariableValue(\"" + seqVar.Name + "\")";
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
                return "graph.SetVariableValue(\"" + seqVar.Name + "\", " + valueToWrite + ");\n";
            }
            else
            {
                String cast = "(" + TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model) + ")";
                return "var_" + seqVar.Prefix + seqVar.Name + " = " + cast + "(" + valueToWrite + ");\n";
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
            return "res_" + seq.Id + " = (bool)(" + valueToWrite + ");\n";
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
                case SequenceType.AssignAttributeToVar:
                {
                    SequenceAssignAttributeToVar attrToVar = (SequenceAssignAttributeToVar)seq;
                    EmitVarIfNew(attrToVar.DestVar, source);
                    break;
                }
                case SequenceType.AssignVarToAttribute:
                {
                    SequenceAssignVarToAttribute varToAttr = (SequenceAssignVarToAttribute)seq;
                    EmitVarIfNew(varToAttr.DestVar, source);
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
                {
                    String cast = "(" + TypesHelper.XgrsTypeToCSharpType(rulesToInputTypes[seqRule.ParamBindings.RuleName][i], model) + ")";
                    parameters += ", " + cast + GetVar(paramBindings.ParamVars[i]);
                }
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
                returnAssignments += SetVar(paramBindings.ReturnVars[i], "tmpvar_" + varName);
            }

            if(seqRule.SequenceType == SequenceType.Rule)
            {
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.RuleName + ".Modify(graph, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo)
                    source.AppendFront("if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;\n");
            }
            else // seq.SequenceType == SequenceType.RuleAll
            {
                if(((SequenceRuleAll)seqRule).NumChooseRandom <= 0)
                {
                    // iterate through matches, use Modify on each, fire the next match event after the first
                    String enumeratorName = "enum_" + seqRule.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.RuleName + ".Modify(graph, " + matchName + returnArguments + ");\n");
                    if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    source.AppendFront("if(" + matchName + "!=" + matchesName + ".FirstExact) graph.RewritingNextMatch();\n");
                    if(gen.UsePerfInfo)
                        source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                }
                else
                {
                    // todo: bisher gibt es eine exception wenn zu wenig matches für die random-auswahl vorhanden, auch bei interpretiert
                    // -- das so ändern, dass geclippt nach vorhandenen matches - das andere ist für die nutzer scheisse (wohl noch nie vorgekommen, weil immer nur ein match gewählt wurde)

                    // as long as a further rewrite has to be selected: randomly choose next match, rewrite it and remove it from available matches; fire the next match event after the first
                    String matchesToSelect = ((SequenceRuleAll)seqRule).NumChooseRandom.ToString();
                    source.AppendFront("for(int i = 0; i < " + matchesToSelect + "; ++i)\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront("if(i != 0) graph.RewritingNextMatch();\n");
                    source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                    if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.RuleName + ".Modify(graph, " + matchName + returnArguments + ");\n");
                    if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    if(gen.UsePerfInfo)
                        source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
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
                    string dictionary;
                    string contains;
                    string sourceValue;
                    string destinationValue = null;
                    if(seqAdd.Setmap.Type == "") {
                        dictionary = "((System.Collections.IDictionary)" + GetVar(seqAdd.Setmap) + ")";
                        contains = "Contains";
                        sourceValue = GetVar(seqAdd.Var);
                        if(seqAdd.VarDst!=null) destinationValue = GetVar(seqAdd.VarDst);
                    } else {
                        dictionary = GetVar(seqAdd.Setmap);
                        contains = "ContainsKey";
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.Setmap.Type), model);
                        sourceValue = "((" + dictSrcType + ")" + GetVar(seqAdd.Var) +")";
                        string dictDstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqAdd.Setmap.Type), model);
                        if(seqAdd.VarDst!=null) destinationValue = "((" + dictDstType + ")" + GetVar(seqAdd.VarDst) + ")";
                    }

                    source.AppendFront("if("+dictionary+"."+contains+"("+sourceValue+"))\n");
					source.AppendFront("{\n");
					source.Indent();
                    if(seqAdd.VarDst==null) {
                        source.AppendFront(dictionary+"["+sourceValue+"] = null;\n");
                    } else {
                        source.AppendFront(dictionary+"["+sourceValue+"] = "+destinationValue+";\n");
                    }
					source.Unindent();
					source.AppendFront("}\n");
                    source.AppendFront("else\n");
					source.AppendFront("{\n");
					source.Indent();
                    if(seqAdd.VarDst==null) {
                        source.AppendFront(dictionary+".Add("+sourceValue+", null);\n");
                    } else {
                        source.AppendFront(dictionary+".Add("+sourceValue+", "+destinationValue+");\n");
                    }
					source.Unindent();
					source.AppendFront("}\n");
                    source.AppendFront(SetResultVar(seqAdd, "true"));
                    
                    break;
                }

                case SequenceType.SetmapRem:
                {
                    SequenceSetmapRem seqDel = (SequenceSetmapRem)seq;
                    string dictionary;
                    string sourceValue;
                    if(seqDel.Setmap.Type == "") {
                        dictionary = "((System.Collections.IDictionary)" + GetVar(seqDel.Setmap) + ")";
                        sourceValue = GetVar(seqDel.Var);
                    } else {
                        dictionary = GetVar(seqDel.Setmap);
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqDel.Setmap.Type), model);
                        sourceValue = "((" + dictSrcType + ")" + GetVar(seqDel.Var) +")";
                    }
                    source.AppendFront(dictionary+".Remove("+sourceValue+");\n");
                    source.AppendFront(SetResultVar(seqDel, "true"));
                    break;
                }

                case SequenceType.SetmapClear:
                {
                    SequenceSetmapClear seqClear = (SequenceSetmapClear)seq;
                    string dictionary;
                    if(seqClear.Setmap.Type == "") {
                        dictionary = "((System.Collections.IDictionary)" + GetVar(seqClear.Setmap) + ")";
                    } else {
                        dictionary = GetVar(seqClear.Setmap);
                    }
                    source.AppendFront(dictionary+".Clear();\n");
                    source.AppendFront(SetResultVar(seqClear, "true"));
                    break;
                }

                case SequenceType.InSetmap:
                {
                    SequenceIn seqIn = (SequenceIn)seq;
                    string dictionary;
                    string contains;
                    string sourceValue;
                    if(seqIn.Setmap.Type == "") {
                        dictionary = "((System.Collections.IDictionary)" + GetVar(seqIn.Setmap) + ")";
                        contains = "Contains";
                        sourceValue = GetVar(seqIn.Var);
                    } else {
                        dictionary = GetVar(seqIn.Setmap);
                        contains = "ContainsKey";
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqIn.Setmap.Type), model);
                        sourceValue = "((" + dictSrcType + ")" + GetVar(seqIn.Var) +")";
                    }
                    source.AppendFront(SetResultVar(seqIn, dictionary+"."+contains+"("+sourceValue+")"));
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
                            +", (int)"+GetVar(seqSetVisited.VisitedFlagVar)+", (bool)"+GetVar(seqSetVisited.Var)+");\n");
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
                    String dictionary;
                    if(seqSetmapSizeToVar.Setmap.Type == "") {
                        dictionary = "((System.Collections.IDictionary)" + GetVar(seqSetmapSizeToVar.Setmap) + ")";
                    } else {
                        dictionary = GetVar(seqSetmapSizeToVar.Setmap);
                    }
                    source.AppendFront(SetVar(seqSetmapSizeToVar.DestVar, dictionary+".Count")); 
                    source.AppendFront(SetResultVar(seqSetmapSizeToVar, "true"));
                    break;
                }

                case SequenceType.AssignSetmapEmptyToVar:
                {
                    SequenceAssignSetmapEmptyToVar seqSetmapEmptyToVar = (SequenceAssignSetmapEmptyToVar)seq;
                    String dictionary;
                    if(seqSetmapEmptyToVar.Setmap.Type == "") {
                        dictionary = "((System.Collections.IDictionary)" + GetVar(seqSetmapEmptyToVar.Setmap) + ")";
                    } else {
                        dictionary = GetVar(seqSetmapEmptyToVar.Setmap);
                    }
                    source.AppendFront(SetVar(seqSetmapEmptyToVar.DestVar, dictionary +".Count==0"));
                    source.AppendFront(SetResultVar(seqSetmapEmptyToVar, "true"));
                    break;
                }

                case SequenceType.AssignMapAccessToVar:
                {
                    SequenceAssignMapAccessToVar seqMapAccessToVar = (SequenceAssignMapAccessToVar)seq; // todo: dst type unknownTypesHelper.ExtractSrc(seqMapAccessToVar.Setmap.Type)
                    source.AppendFront(SetResultVar(seqMapAccessToVar, "false"));
                    string dictionary;
                    string contains;
                    string sourceValue;
                    if(seqMapAccessToVar.Setmap.Type == "") {
                        dictionary = "((System.Collections.IDictionary)" + GetVar(seqMapAccessToVar.Setmap) + ")";
                        contains = "Contains";
                        sourceValue = GetVar(seqMapAccessToVar.KeyVar);
                    } else {
                        dictionary = GetVar(seqMapAccessToVar.Setmap);
                        contains = "ContainsKey";
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqMapAccessToVar.Setmap.Type), model);
                        sourceValue = "((" + dictSrcType + ")" + GetVar(seqMapAccessToVar.KeyVar) +")";
                    }
                    source.AppendFront("if(" + dictionary + "." + contains + "("+ sourceValue +")) {\n");
                    source.Indent();
                    source.AppendFront(SetVar(seqMapAccessToVar.DestVar, dictionary + "[" + sourceValue + "]"));
                    source.AppendFront(SetResultVar(seqMapAccessToVar, "true"));
                    source.Unindent();
                    source.AppendFront("}\n");
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
                    throw new Exception("Internal Error: the AssignElemToVar is interpreted only (no NamedGraph available at lgsp level)");
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
                    if(seqConstToVar.Constant is bool)
                    {
                        source.AppendFront(SetVar(seqConstToVar.DestVar, (bool)seqConstToVar.Constant==true ? "true" : "false"));
                    }
                    else if(seqConstToVar.Constant is string && ((string)seqConstToVar.Constant).Contains("::"))
                    {
                        string strConst = (string)seqConstToVar.Constant;
                        int separationPos = strConst.IndexOf("::");
                        string type = strConst.Substring(0, separationPos);
                        string value = strConst.Substring(separationPos + 2);
                        source.AppendFront(SetVar(seqConstToVar.DestVar, "GRGEN_MODEL.ENUM_" + type + "." + value));
                    }
                    else if(seqConstToVar.Constant is string && ((string)seqConstToVar.Constant).StartsWith("set<") && ((string)seqConstToVar.Constant).EndsWith(">"))
                    {
                        String srcType = "GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+TypesHelper.ExtractSrc((string)seqConstToVar.Constant)+"\", graph)";
                        String dstType = "typeof(de.unika.ipd.grGen.libGr.SetValueType)";
                        source.AppendFront(SetVar(seqConstToVar.DestVar, "GRGEN_LIBGR.DictionaryHelper.NewDictionary("+srcType+", "+dstType+")"));
                        source.AppendFront(SetResultVar(seqConstToVar, "true"));
                    }
                    else if(seqConstToVar.Constant is string && ((string)seqConstToVar.Constant).StartsWith("map<") && ((string)seqConstToVar.Constant).EndsWith(">"))
                    {
                        String srcType = "GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+TypesHelper.ExtractSrc((string)seqConstToVar.Constant)+"\", graph)";
                        String dstType = "GRGEN_LIBGR.DictionaryHelper.GetTypeFromNameForDictionary(\""+TypesHelper.ExtractDst((string)seqConstToVar.Constant)+"\", graph)";
                        source.AppendFront(SetVar(seqConstToVar.DestVar, "GRGEN_LIBGR.DictionaryHelper.NewDictionary("+srcType+", "+dstType+")"));
                        source.AppendFront(SetResultVar(seqConstToVar, "true"));
                    }
                    else if(seqConstToVar.Constant is string)
                    {
                        source.AppendFront(SetVar(seqConstToVar.DestVar, "\"" + seqConstToVar.Constant.ToString() + "\""));
                    }
                    else 
                    {
                        source.AppendFront(SetVar(seqConstToVar.DestVar, seqConstToVar.Constant.ToString()));
                    }
                    source.AppendFront(SetResultVar(seqConstToVar, "true"));
                    break;
                }

                case SequenceType.AssignAttributeToVar:
                {
                    SequenceAssignAttributeToVar seqAttrToVar = (SequenceAssignAttributeToVar)seq;
                    source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAttrToVar.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqAttrToVar.SourceVar) + ";\n");
                    source.AppendFront("object value_" + seqAttrToVar.Id + " = elem_" + seqAttrToVar.Id + ".GetAttribute(\"" + seqAttrToVar.AttributeName + "\");\n");
                    source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAttrToVar.Id + ";\n");
                    source.AppendFront("value_" + seqAttrToVar.Id + " = GRGEN_LIBGR.DictionaryHelper.IfAttributeOfElementIsDictionaryThenCloneDictionaryValue(elem_" + seqAttrToVar.Id + ", \"" + seqAttrToVar.AttributeName + "\", value_" + seqAttrToVar.Id + ", out attrType_" + seqAttrToVar.Id + ");\n");
                    source.AppendFront(SetVar(seqAttrToVar.DestVar, "value_" + seqAttrToVar.Id));
                    source.AppendFront(SetResultVar(seqAttrToVar, "true"));
                    break;
                }

                case SequenceType.AssignVarToAttribute:
                {
                    SequenceAssignVarToAttribute seqVarToAttr = (SequenceAssignVarToAttribute)seq;
                    source.AppendFront("object value_" + seqVarToAttr.Id + " = " + GetVar(seqVarToAttr.SourceVar) + ";\n");
                    source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqVarToAttr.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqVarToAttr.DestVar) + ";\n");
                    source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqVarToAttr.Id + ";\n");
                    source.AppendFront("value_" + seqVarToAttr.Id + " = GRGEN_LIBGR.DictionaryHelper.IfAttributeOfElementIsDictionaryThenCloneDictionaryValue(elem_" + seqVarToAttr.Id + ", \"" + seqVarToAttr.AttributeName + "\", value_" + seqVarToAttr.Id + ", out attrType_" + seqVarToAttr.Id + ");\n");
                    source.AppendFront("GRGEN_LIBGR.AttributeChangeType changeType_" + seqVarToAttr.Id + " = GRGEN_LIBGR.AttributeChangeType.Assign;\n");
                    source.AppendFront("if(elem_" + seqVarToAttr.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqVarToAttr.Id + ", attrType_" + seqVarToAttr.Id + ", changeType_" + seqVarToAttr.Id + ", value_" + seqVarToAttr.Id + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqVarToAttr.Id + ", attrType_" + seqVarToAttr.Id + ", changeType_" + seqVarToAttr.Id + ", value_" + seqVarToAttr.Id + ", null);\n");
                    source.AppendFront("elem_" + seqVarToAttr.Id + ".SetAttribute(\"" + seqVarToAttr.AttributeName + "\", value_" + seqVarToAttr.Id + ");\n");
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

		public bool GenerateXGRSCode(int xgrsID, String xgrsStr, String[] paramNames, GrGenType[] paramTypes, SourceBuilder source)
		{
			Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for (int i = 0; i < paramNames.Length; i++)
            {
                varDecls.Add(paramNames[i], TypesHelper.DotNetTypeToXgrsType(paramTypes[i]));
            }
            String[] ruleNames = new String[rulesToInputTypes.Count];
            int j = 0;
            foreach(KeyValuePair<String, List<String>> ruleToInputTypes in rulesToInputTypes)
            {  
                ruleNames[j] = ruleToInputTypes.Key;
                ++j;
            }

			Sequence seq;
            try
            {
                seq = SequenceParser.ParseSequence(xgrsStr, ruleNames, varDecls);
                LGSPSequenceChecker checker = new LGSPSequenceChecker(ruleNames, rulesToInputTypes, rulesToOutputTypes, model);
                checker.Check(seq);
            }
            catch(ParseException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" caused the following error:\n" + ex.Message);
                return false;
            }
            catch(SequenceParserException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" caused the following error:\n");
                if(ex.RuleName == null && ex.Kind != SequenceParserError.TypeMismatch)
                {
                    Console.Error.WriteLine("Unknown rule: \"{0}\"", ex.RuleName);
                    return false;
                }
                switch(ex.Kind)
                {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if(rulesToInputTypes[ex.RuleName].Count != ex.NumGivenInputs && rulesToOutputTypes[ex.RuleName].Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of parameters and return values for action \"" + ex.RuleName + "\"!");
                    else if(rulesToInputTypes[ex.RuleName].Count != ex.NumGivenInputs)
                        Console.Error.WriteLine("Wrong number of parameters for action \"" + ex.RuleName + "\"!");
                    else if(rulesToOutputTypes[ex.RuleName].Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of return values for action \"" + ex.RuleName + "\"!");
                    else
                        goto default;
                    break;

                case SequenceParserError.BadParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". parameter is not valid for action \"" + ex.RuleName + "\"!");
                    break;

                case SequenceParserError.BadReturnParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". return parameter is not valid for action \"" + ex.RuleName + "\"!");
                    break;

                case SequenceParserError.RuleNameUsedByVariable:
                    Console.Error.WriteLine("The name of the variable conflicts with the name of action \"" + ex.RuleName + "\"!");
                    return false;

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    Console.Error.WriteLine("The variable \"" + ex.RuleName + "\" may neither receive parameters nor return values!");
                    return false;

                case SequenceParserError.UnknownAttribute:
                    Console.WriteLine("Unknown attribute \"" + ex.RuleName + "\"!");
                    return false;

                case SequenceParserError.TypeMismatch:
                    Console.Error.WriteLine("The construct \"" + ex.VariableOrFunctionName + "\" expects:" + ex.ExpectedType + " but is / is given " + ex.GivenType + "!");
                    return false;

                default:
                    throw new ArgumentException("Invalid error kind: " + ex.Kind);
                }

                Console.Error.Write("Prototype: {0}", ex.RuleName);
                if(rulesToInputTypes[ex.RuleName].Count != 0)
                {
                    Console.Error.Write("(");
                    bool first = true;
                    foreach(String typeName in rulesToInputTypes[ex.RuleName])
                    {
                        Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                        first = false;
                    }
                    Console.Error.Write(")");
                }
                if(rulesToOutputTypes[ex.RuleName].Count != 0)
                {
                    Console.Error.Write(" : (");
                    bool first = true;
                    foreach(String typeName in rulesToOutputTypes[ex.RuleName])
                    {
                        Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                        first = false;
                    }
                    Console.Error.Write(")");
                }
                Console.Error.WriteLine();

                return false;
            }

            source.AppendFront("public static bool ApplyXGRS_" + xgrsID + "(GRGEN_LGSP.LGSPGraph graph");
			for(int i = 0; i < paramNames.Length; i++)
			{
				source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(paramTypes[i]), model) + " var_");
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
