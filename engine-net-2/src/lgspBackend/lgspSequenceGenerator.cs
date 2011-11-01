/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.IO;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A closure for an exec statement in an alternative, iterated or subpattern,
    /// containing the entities needed for the exec execution.
    /// These exec are executed at the end of the rule which directly or indirectly used them,
    /// long after the alternative/iterated/subpattern modification containing them has been applied.
    /// The real stuff depends on the xgrs and is generated, implementing this abstract class.
    /// </summary>
    public abstract class LGSPEmbeddedSequenceClosure
    {
        /// <summary>
        /// Executes the embedded sequence closure
        /// </summary>
        /// <param name="graph">the graph on which to apply the sequence</param>
        /// <returns>the result of sequence execution</returns>
        public abstract bool exec(LGSPGraph graph);
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

        // maps sequence names available in the .grg to compile to the list of the input typ names
        Dictionary<String, List<String>> sequencesToInputTypes;
        // maps sequence names available in the .grg to compile to the list of the output typ names
        Dictionary<String, List<String>> sequencesToOutputTypes;

        // the used rules (so that a variable was created for easy acess to them)
		Dictionary<String, object> knownRules = new Dictionary<string, object>();

        // a counter for unique temporary variables needed as dummy variables
        // to receive the return/out values of rules/sequnces in case no assignment is given
        int tmpVarCtr;


        /// <summary>
        /// Constructs the sequence generator
        /// </summary>
        public LGSPSequenceGenerator(LGSPGrGen gen, IGraphModel model,
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes,
            Dictionary<String, List<String>> sequencesToInputTypes, Dictionary<String, List<String>> sequencesToOutputTypes)
        {
            this.gen = gen;
            this.model = model;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.sequencesToInputTypes = sequencesToInputTypes;
            this.sequencesToOutputTypes = sequencesToOutputTypes;
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

        /// <summary>
        /// pre-run for emitting the needed entities before emitting the real code
        /// - emits result variable declarations
        /// - emits xgrs variable declarations (only once for every variable, declaration only possible at assignment targets)
        /// - collects used rules into knownRules, emit local rule declaration (only once for every rule)
        /// </summary>
		void EmitNeededVarAndRuleEntities(Sequence seq, SourceBuilder source)
		{
			source.AppendFront(DeclareResultVar(seq));

			switch(seq.SequenceType)
			{
				case SequenceType.AssignExprToVar:		// TODO: Load from external vars?
				{
					SequenceAssignExprToVar toVar = (SequenceAssignExprToVar) seq;
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
                case SequenceType.AssignUserInputToVar:
                {
                    SequenceAssignUserInputToVar userInputToVar = (SequenceAssignUserInputToVar)seq;
                    EmitVarIfNew(userInputToVar.DestVar, source);
                    break;
                }
                case SequenceType.AssignRandomToVar:
                {
                    SequenceAssignRandomToVar randomToVar = (SequenceAssignRandomToVar)seq;
                    EmitVarIfNew(randomToVar.DestVar, source);
                    break;
                }
                case SequenceType.AssignExprToAttribute:
                {
                    SequenceAssignExprToAttribute varToAttr = (SequenceAssignExprToAttribute)seq;
                    EmitVarIfNew(varToAttr.DestVar, source);
                    break;
                }

				case SequenceType.RuleCall:
				case SequenceType.RuleAllCall:
				{
					SequenceRuleCall seqRule = (SequenceRuleCall) seq;
					String ruleName = seqRule.ParamBindings.Name;
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

				default: // esp. AssignElemToVar
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

        void EmitRuleOrRuleAllCall(SequenceRuleCall seqRule, SourceBuilder source)
        {
            RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
            String specialStr = seqRule.Special ? "true" : "false";
            String parameters = BuildParameters(paramBindings);
            String matchingPatternClassName = "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seqRule.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seqRule.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.Name
                + ".Match(graph, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "graph.MaxMatches")
                + parameters + ");\n");
            if(gen.FireEvents) source.AppendFront("graph.Matched(" + matchesName + ", " + specialStr + ");\n");
            if(seqRule is SequenceRuleAllCall
                && ((SequenceRuleAllCall)seqRule).ChooseRandom
                && ((SequenceRuleAllCall)seqRule).MinSpecified)
            {
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                source.AppendFrontFormat("int minmatchesvar_{0} = (int){1};\n", seqRuleAll.Id, GetVar(seqRuleAll.MinVarChooseRandom));
                source.AppendFrontFormat("if({0}.Count < minmatchesvar_{1}) {{\n", matchesName, seqRuleAll.Id);
            }
            else
            {
                source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            }
            source.Indent();
            source.AppendFront(SetResultVar(seqRule, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seqRule, "true"));
            if(gen.UsePerfInfo)
                source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(gen.FireEvents) source.AppendFront("graph.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            BuildReturnParameters(paramBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

            if(seqRule.SequenceType == SequenceType.RuleCall)
            {
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;\n");
            }
            else if(!((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
            {
                // iterate through matches, use Modify on each, fire the next match event after the first
                String enumeratorName = "enum_" + seqRule.Id;
                source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                source.AppendFront("if(" + matchName + "!=" + matchesName + ".FirstExact) graph.RewritingNextMatch();\n");
                if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
            {
                // as long as a further rewrite has to be selected: randomly choose next match, rewrite it and remove it from available matches; fire the next match event after the first
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                source.AppendFrontFormat("int numchooserandomvar_{0} = (int){1};\n", seqRuleAll.Id, seqRuleAll.MaxVarChooseRandom != null ? GetVar(seqRuleAll.MaxVarChooseRandom) : (seqRuleAll.MinSpecified ? "2147483647" : "1"));
                source.AppendFrontFormat("if({0}.Count < numchooserandomvar_{1}) numchooserandomvar_{1} = {0}.Count;\n", matchesName, seqRule.Id);
                source.AppendFrontFormat("for(int i = 0; i < numchooserandomvar_{0}; ++i)\n", seqRule.Id);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("if(i != 0) graph.RewritingNextMatch();\n");
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                if(gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }

            if(gen.FireEvents) source.AppendFront("graph.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        void EmitSequenceCall(SequenceSequenceCall seqSeq, SourceBuilder source)
        {
            SequenceInvocationParameterBindings paramBindings = seqSeq.ParamBindings;
            String parameters = BuildParameters(paramBindings);
            String outParameterDeclarations;
            String outArguments;
            String outAssignments;
            BuildOutParameters(paramBindings, out outParameterDeclarations, out outArguments, out outAssignments);

            if(outParameterDeclarations.Length != 0)
                source.AppendFront(outParameterDeclarations + "\n");
            source.AppendFront("if(Sequence_"+paramBindings.Name+".ApplyXGRS_" + paramBindings.Name
                                + "(graph" + parameters + outArguments + ")) {\n");
            source.Indent();
            if(outAssignments.Length != 0)
                source.AppendFront(outAssignments + "\n");
            source.AppendFront(SetResultVar(seqSeq, "true"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seqSeq, "false"));
            source.Unindent();
            source.AppendFront("}\n");
        }

		void EmitSequence(Sequence seq, SourceBuilder source)
		{
			switch(seq.SequenceType)
			{
				case SequenceType.RuleCall:
                case SequenceType.RuleAllCall:
                    EmitRuleOrRuleAllCall((SequenceRuleCall)seq, source);
                    break;

                case SequenceType.SequenceCall:
                    EmitSequenceCall((SequenceSequenceCall)seq, source);
                    break;

				case SequenceType.BooleanExpression:
				{
					SequenceBooleanExpression seqPred = (SequenceBooleanExpression) seq;
					source.AppendFront(SetResultVar(seqPred, GetSequenceExpression(seqPred.Expression)));
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
					if(seqBin.Random)
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
					if(seqBin.Random)
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

                    source.AppendFront(SetResultVar(seqFor, "true"));

                    if(seqFor.Container.Type == "")
                    {
                        // type not statically known? -> might be Dictionary or List dynamically, must decide at runtime
                        source.AppendFront("if(" + GetVar(seqFor.Container) + " is IList) {\n");
                        source.Indent();

                        source.AppendFront("IList entry_" + seqFor.Id + " = (IList) " + GetVar(seqFor.Container) + ";\n");
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        source.AppendFront("foreach(DictionaryEntry entry_" + seqFor.Id + " in (IDictionary)" + GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqFor.Container.Type.StartsWith("array"))
                    {
                        String arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        source.AppendFrontFormat("List<{0}> entry_{1} = (List<{0}>) " + GetVar(seqFor.Container) + ";\n", arrayValueType, seqFor.Id);
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();

                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        String srcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        String dstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqFor.Container.Type), model);
                        source.AppendFront("foreach(KeyValuePair<" + srcType + "," + dstType + "> entry_" + seqFor.Id + " in " + GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();

                        source.AppendFront(SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        if(seqFor.VarDst != null)
                            source.AppendFront(SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(SetResultVar(seqFor, GetResultVar(seqFor) + " & " + GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }

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

                case SequenceType.YieldingAssignExprToVar:
                {
                    SequenceYieldingAssignExprToVar seqYield = (SequenceYieldingAssignExprToVar)seq;
                    source.AppendFront(SetVar(seqYield.DestVar, GetSequenceExpression(seqYield.SourceExpression)));
                    source.AppendFront(SetResultVar(seqYield, "true"));
                    break;
                }

                case SequenceType.ContainerAdd:
                {
                    SequenceContainerAdd seqAdd = (SequenceContainerAdd)seq;

                    if(seqAdd.Container.Type == "")
                    {
                        string sourceValue = GetVar(seqAdd.Var);
                        string destinationValue = seqAdd.VarDst == null ? null : destinationValue = GetVar(seqAdd.VarDst);

                        source.AppendFront("if(" + GetVar(seqAdd.Container) + " is IList) {\n");
                        source.Indent();

                        if(destinationValue != null && !TypesHelper.IsSameOrSubtype(seqAdd.VarDst.Type, "int", model))
                            source.AppendFront("throw new Exception(\"Can't add non-int key to array\");\n");
                        else
                        {
                            string array = "((System.Collections.IList)" + GetVar(seqAdd.Container) + ")";
                            if(destinationValue == null)
                                source.AppendFront(array + ".Add(" + sourceValue + ");\n");
                            else
                                source.AppendFront(array + ".Insert((int)" + destinationValue + ", " + sourceValue + ");\n");
                        }

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + GetVar(seqAdd.Container) + ")";
                        source.AppendFront("if(" + dictionary + ".Contains(" + sourceValue + "))\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        if(destinationValue == null)
                            source.AppendFront(dictionary + "[" + sourceValue + "] = null;\n");
                        else
                            source.AppendFront(dictionary + "[" + sourceValue + "] = " + destinationValue + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("else\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqAdd.VarDst == null)
                            source.AppendFront(dictionary + ".Add(" + sourceValue + ", null);\n");
                        else
                            source.AppendFront(dictionary + ".Add(" + sourceValue + ", " + destinationValue + ");\n");
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqAdd.Container.Type.StartsWith("array"))
                    {
                        string array = GetVar(seqAdd.Container);
                        string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.Container.Type), model);
                        string sourceValue = "((" + arrayValueType + ")" + GetVar(seqAdd.Var) + ")";
                        string destinationValue = seqAdd.VarDst == null ? null : destinationValue = "((int)" + GetVar(seqAdd.VarDst) + ")";
                        if(destinationValue == null)
                            source.AppendFront(array + ".Add(" + sourceValue + ");\n");
                        else
                            source.AppendFront(array + ".Insert((int)" + destinationValue + ", " + sourceValue + ");\n");
                    }
                    else
                    {
                        string dictionary = GetVar(seqAdd.Container);
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.Container.Type), model);
                        string sourceValue = "((" + dictSrcType + ")" + GetVar(seqAdd.Var) + ")";
                        string dictDstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqAdd.Container.Type), model);
                        string destinationValue = seqAdd.VarDst == null ? null : destinationValue = "((" + dictDstType + ")" + GetVar(seqAdd.VarDst) + ")";

                        source.AppendFront("if(" + dictionary + ".ContainsKey(" + sourceValue + "))\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqAdd.VarDst == null)
                            source.AppendFront(dictionary + "[" + sourceValue + "] = null;\n");
                        else
                            source.AppendFront(dictionary + "[" + sourceValue + "] = " + destinationValue + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("else\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqAdd.VarDst == null)
                            source.AppendFront(dictionary + ".Add(" + sourceValue + ", null);\n");
                        else
                            source.AppendFront(dictionary + ".Add(" + sourceValue + ", " + destinationValue + ");\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    source.AppendFront(SetResultVar(seqAdd, "true"));
                    break;
                }

                case SequenceType.ContainerRem:
                {
                    SequenceContainerRem seqDel = (SequenceContainerRem)seq;
                    if(seqDel.Container.Type == "")
                    {
                        string sourceValue = seqDel.Var == null ? null : GetVar(seqDel.Var);
                        source.AppendFront("if(" + GetVar(seqDel.Container) + " is IList) {\n");
                        source.Indent();

                        string array = "((System.Collections.IList)" + GetVar(seqDel.Container) + ")";
                        if(sourceValue == null)
                            source.AppendFront(array + ".RemoveAt(" + array + ".Count - 1);\n");
                        else
                        {
                            if(!TypesHelper.IsSameOrSubtype(seqDel.Var.Type, "int", model))
                                source.AppendFront("throw new Exception(\"Can't remove non-int index from array\");\n");
                            else
                                source.AppendFront(array + ".RemoveAt((int)" + sourceValue + ");\n");
                        }

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + GetVar(seqDel.Container) + ")";
                        if(sourceValue == null)
                            source.AppendFront("throw new Exception(\""+seqDel.Container.Name+".rem() only possible on array!\");\n");
                        else
                            source.AppendFront(dictionary + ".Remove(" + sourceValue + ");\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqDel.Container.Type.StartsWith("array"))
                    {
                        string array = GetVar(seqDel.Container);
                        string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqDel.Container.Type), model);
                        string sourceValue = seqDel.Var == null ? null : GetVar(seqDel.Var);
                        if(sourceValue == null)
                            source.AppendFront(array + ".RemoveAt(" + array + ".Count - 1);\n");
                        else
                            source.AppendFront(array + ".RemoveAt((int)" + sourceValue + ");\n");
                    }
                    else
                    {
                        string dictionary = GetVar(seqDel.Container);
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqDel.Container.Type), model);
                        string sourceValue = "((" + dictSrcType + ")" + GetVar(seqDel.Var) + ")";
                        source.AppendFront(dictionary + ".Remove(" + sourceValue + ");\n");
                    }
                    source.AppendFront(SetResultVar(seqDel, "true"));
                    break;
                }

                case SequenceType.ContainerClear:
                {
                    SequenceContainerClear seqClear = (SequenceContainerClear)seq;

                    if(seqClear.Container.Type == "")
                    {
                        source.AppendFront("if(" + GetVar(seqClear.Container) + " is IList) {\n");
                        source.Indent();

                        string array = "((System.Collections.IList)" + GetVar(seqClear.Container) + ")";
                        source.AppendFront(array + ".Clear();\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + GetVar(seqClear.Container) + ")";
                        source.AppendFront(dictionary + ".Clear();\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqClear.Container.Type.StartsWith("array"))
                    {
                        string array = GetVar(seqClear.Container);
                        source.AppendFront(array + ".Clear();\n");
                    }
                    else
                    {
                        string dictionary = GetVar(seqClear.Container);
                        source.AppendFront(dictionary + ".Clear();\n");
                    }

                    source.AppendFront(SetResultVar(seqClear, "true"));
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
                        if(seqEmit.Variable.Type=="" || seqEmit.Variable.Type.StartsWith("set<") || seqEmit.Variable.Type.StartsWith("map<") || seqEmit.Variable.Type.StartsWith("array<"))
                        {
                            source.AppendFront("if(" + GetVar(seqEmit.Variable) + " is IDictionary)\n");
                            source.AppendFront("\tgraph.EmitWriter.Write(GRGEN_LIBGR.DictionaryListHelper.ToString((IDictionary)" + GetVar(seqEmit.Variable) + ", graph));\n");
                            source.AppendFront("else if(" + GetVar(seqEmit.Variable) + " is IList)\n");
                            source.AppendFront("\tgraph.EmitWriter.Write(GRGEN_LIBGR.DictionaryListHelper.ToString((IList)" + GetVar(seqEmit.Variable) + ", graph));\n");
                            source.AppendFront("else\n\t");
                        }
                        source.AppendFront("graph.EmitWriter.Write(GRGEN_LIBGR.DictionaryListHelper.ToString(" + GetVar(seqEmit.Variable) + ", graph));\n");
                    } else {
                        String text = seqEmit.Text.Replace("\n", "\\n");
                        text = text.Replace("\r", "\\r");
                        text = text.Replace("\t", "\\t");
                        source.AppendFront("graph.EmitWriter.Write(\""+text+"\");\n");
                    }
                    source.AppendFront(SetResultVar(seqEmit, "true"));
                    break;
                }

                case SequenceType.Record:
                {
                    SequenceRecord seqRec = (SequenceRecord)seq;
                    if(seqRec.Variable != null) {
                        if(seqRec.Variable.Type=="" || seqRec.Variable.Type.StartsWith("set<") || seqRec.Variable.Type.StartsWith("map<") || seqRec.Variable.Type.StartsWith("array<"))
                        {
                            source.AppendFront("if(" + GetVar(seqRec.Variable) + " is IDictionary)\n");
                            source.AppendFront("\tgraph.Recorder.Write(GRGEN_LIBGR.DictionaryListHelper.ToString((IDictionary)" + GetVar(seqRec.Variable) + ", graph));\n");
                            source.AppendFront("else if(" + GetVar(seqRec.Variable) + " is IList)\n");
                            source.AppendFront("\tgraph.Recorder.Write(GRGEN_LIBGR.DictionaryListHelper.ToString((IList)" + GetVar(seqRec.Variable) + ", graph));\n");
                            source.AppendFront("else\n\t");
                        }
                        source.AppendFront("graph.Recorder.Write(GRGEN_LIBGR.DictionaryListHelper.ToString(" + GetVar(seqRec.Variable) + ", graph));\n");
                    } else {
                        String text = seqRec.Text.Replace("\n", "\\n");
                        text = text.Replace("\r", "\\r");
                        text = text.Replace("\t", "\\t");
                        source.AppendFront("graph.Recorder.Write(\"" + text + "\");\n");
                    }
                    source.AppendFront(SetResultVar(seqRec, "true"));
                    break;
                }

                case SequenceType.AssignExprToIndexedVar:
                {
                    SequenceAssignExprToIndexedVar seqAssignVarToIndexedVar = (SequenceAssignExprToIndexedVar)seq;
                    source.AppendFront(SetResultVar(seqAssignVarToIndexedVar, "false"));

                    if(seqAssignVarToIndexedVar.DestVar.Type == "")
                    {
                        string indexValue = GetVar(seqAssignVarToIndexedVar.KeyVar);
                        source.AppendFront("if(" + GetVar(seqAssignVarToIndexedVar.DestVar) + " is IList) {\n");
                        source.Indent();

                        string array = "((System.Collections.IList)" + GetVar(seqAssignVarToIndexedVar.DestVar) + ")";
                        if(!TypesHelper.IsSameOrSubtype(seqAssignVarToIndexedVar.KeyVar.Type, "int", model))
                        {
                            source.AppendFront("if(true) {\n");
                            source.Indent();
                            source.AppendFront("throw new Exception(\"Can't access non-int index in array\");\n");
                        }
                        else
                        {
                            source.AppendFront("if(" + array + ".Count > (int)" + indexValue + ") {\n");
                            source.Indent();
                            source.AppendFront(array + "[(int)" + indexValue + "] = " + GetSequenceExpression(seqAssignVarToIndexedVar.SourceExpression) + ";");
                            source.AppendFront(SetResultVar(seqAssignVarToIndexedVar, "true"));
                        }
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        string dictionary = "((System.Collections.IDictionary)" + GetVar(seqAssignVarToIndexedVar.DestVar) + ")";
                        source.AppendFront("if(" + dictionary + ".Contains(" + indexValue + ")) {\n");
                        source.Indent();
                        source.AppendFront(dictionary + "[" + indexValue + "] = " + GetSequenceExpression(seqAssignVarToIndexedVar.SourceExpression) + ";");
                        source.AppendFront(SetResultVar(seqAssignVarToIndexedVar, "true"));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqAssignVarToIndexedVar.DestVar.Type.StartsWith("array"))
                    {
                        string array = GetVar(seqAssignVarToIndexedVar.DestVar);
                        string indexValue = "((int)" + GetVar(seqAssignVarToIndexedVar.KeyVar) + ")";
                        source.AppendFront("if(" + array + ".Count > " + indexValue + ") {\n");
                        source.Indent();
                        source.AppendFront(array + "[(int)" + indexValue + "] = " + GetSequenceExpression(seqAssignVarToIndexedVar.SourceExpression) + ";");
                        source.AppendFront(SetResultVar(seqAssignVarToIndexedVar, "true"));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        string dictionary = GetVar(seqAssignVarToIndexedVar.DestVar);
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAssignVarToIndexedVar.DestVar.Type), model);
                        string indexValue = "((" + dictSrcType + ")" + GetVar(seqAssignVarToIndexedVar.KeyVar) + ")";
                        source.AppendFront("if(" + dictionary + ".ContainsKey(" + indexValue + ")) {\n");
                        source.Indent();
                        source.AppendFront(dictionary + "[" + indexValue + "] = " + GetSequenceExpression(seqAssignVarToIndexedVar.SourceExpression) + ";");
                        source.AppendFront(SetResultVar(seqAssignVarToIndexedVar, "true"));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    break;
                }

				case SequenceType.AssignExprToVar:
				{
					SequenceAssignExprToVar seqToVar = (SequenceAssignExprToVar) seq;
                    source.AppendFront(SetVar(seqToVar.DestVar, GetSequenceExpression(seqToVar.SourceExpression)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
					break;
				}

                case SequenceType.AssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(SetVar(seqToVar.DestVar, GetResultVar(seqToVar.Seq)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.OrAssignSequenceResultToVar:
                {
                    SequenceOrAssignSequenceResultToVar seqToVar = (SequenceOrAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(SetVar(seqToVar.DestVar, GetResultVar(seqToVar.Seq) + "|| (bool)" + GetVar(seqToVar.DestVar)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AndAssignSequenceResultToVar:
                {
                    SequenceAndAssignSequenceResultToVar seqToVar = (SequenceAndAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(SetVar(seqToVar.DestVar, GetResultVar(seqToVar.Seq) + "&& (bool)" + GetVar(seqToVar.DestVar)));
                    source.AppendFront(SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignUserInputToVar:
                {
                    throw new Exception("Internal Error: the AssignUserInputToVar is interpreted only (no Debugger available at lgsp level)");
                }

                case SequenceType.AssignRandomToVar:
                {
                    SequenceAssignRandomToVar seqRandomToVar = (SequenceAssignRandomToVar)seq;
                    source.AppendFront(SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.Next(" + seqRandomToVar.Number + ")"));
                    source.AppendFront(SetResultVar(seqRandomToVar, "true"));
                    break;
                }

                case SequenceType.AssignExprToAttribute:
                {
                    SequenceAssignExprToAttribute seqVarToAttr = (SequenceAssignExprToAttribute)seq;
                    source.AppendFront("object value_" + seqVarToAttr.Id + " = " + GetSequenceExpression(seqVarToAttr.SourceExpression) + ";\n");
                    source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqVarToAttr.Id + " = (GRGEN_LIBGR.IGraphElement)" + GetVar(seqVarToAttr.DestVar) + ";\n");
                    source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqVarToAttr.Id + ";\n");
                    source.AppendFront("value_" + seqVarToAttr.Id + " = GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(elem_" + seqVarToAttr.Id + ", \"" + seqVarToAttr.AttributeName + "\", value_" + seqVarToAttr.Id + ", out attrType_" + seqVarToAttr.Id + ");\n");
                    source.AppendFront("GRGEN_LIBGR.AttributeChangeType changeType_" + seqVarToAttr.Id + " = GRGEN_LIBGR.AttributeChangeType.Assign;\n");
                    source.AppendFront("if(elem_" + seqVarToAttr.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqVarToAttr.Id + ", attrType_" + seqVarToAttr.Id + ", changeType_" + seqVarToAttr.Id + ", value_" + seqVarToAttr.Id + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqVarToAttr.Id + ", attrType_" + seqVarToAttr.Id + ", changeType_" + seqVarToAttr.Id + ", value_" + seqVarToAttr.Id + ", null);\n");
                    source.AppendFront("elem_" + seqVarToAttr.Id + ".SetAttribute(\"" + seqVarToAttr.AttributeName + "\", value_" + seqVarToAttr.Id + ");\n");
                    source.AppendFront(SetResultVar(seqVarToAttr, "true"));
                    break;
                }

                case SequenceType.LazyOrAll:
                {
                    SequenceLazyOrAll seqAll = (SequenceLazyOrAll)seq;
                    EmitSequenceAll(seqAll, true, true, source);
                    break;
                }

                case SequenceType.LazyAndAll:
                {
                    SequenceLazyAndAll seqAll = (SequenceLazyAndAll)seq;
                    EmitSequenceAll(seqAll, false, true, source);
                    break;
                }

                case SequenceType.StrictOrAll:
                {
                    SequenceStrictOrAll seqAll = (SequenceStrictOrAll)seq;
                    EmitSequenceAll(seqAll, true, false, source);
                    break;
                }

                case SequenceType.StrictAndAll:
                {
                    SequenceStrictAndAll seqAll = (SequenceStrictAndAll)seq;
                    EmitSequenceAll(seqAll, false, false, source);
                    break;
                }

                case SequenceType.SomeFromSet:
                {
                    SequenceSomeFromSet seqSome = (SequenceSomeFromSet)seq;
                    EmitSequenceSome(seqSome, source);
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

                case SequenceType.Backtrack:
                {
                    SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                    EmitSequenceBacktrack(seqBack, source);
                    break;
                }

				default:
					throw new Exception("Unknown sequence type: " + seq.SequenceType);
			}
		}

        public void EmitSequenceBacktrack(SequenceBacktrack seq, SourceBuilder source)
        {
            RuleInvocationParameterBindings paramBindings = seq.Rule.ParamBindings;
            String specialStr = seq.Rule.Special ? "true" : "false";
            String parameters = BuildParameters(paramBindings);
            String matchingPatternClassName = "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seq.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seq.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.Name
                + ".Match(graph, graph.MaxMatches" + parameters + ");\n");

            source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seq, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(SetResultVar(seq, "true")); // shut up compiler
            if(gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(gen.FireEvents) source.AppendFront("graph.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            BuildReturnParameters(paramBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

            // apply the rule and the following sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            String enumeratorName = "enum_" + seq.Id;
            String matchesTriedName = "matchesTried_" + seq.Id;
            source.AppendFront("int " + matchesTriedName + " = 0;\n");
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            source.AppendFront("++" + matchesTriedName + ";\n");

            // start a transaction
            source.AppendFront("int transID_" + seq.Id + " = graph.TransactionManager.StartTransaction();\n");
            source.AppendFront("int oldRewritesPerformed_" + seq.Id + " = -1;\n");
            source.AppendFront("if(graph.PerformanceInfo!=null) oldRewritesPerformed_"+seq.Id+" = graph.PerformanceInfo.RewritesPerformed;\n");
            if(gen.FireEvents) source.AppendFront("graph.Matched(" + matchesName + ", " + specialStr + ");\n");
            if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
            if(gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
            if(gen.FireEvents) source.AppendFront("graph.Finished(" + matchesName + ", " + specialStr + ");\n");

            // rule applied, now execute the sequence
            EmitSequence(seq.Seq, source);

            // if sequence execution failed, roll the changes back and try the next match of the rule
            source.AppendFront("if(!" + GetResultVar(seq.Seq) + ") {\n");
            source.Indent();
            source.AppendFront("graph.TransactionManager.Rollback(transID_" + seq.Id + ");\n");
            if(gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed = oldRewritesPerformed_"+seq.Id+";\n");

            source.AppendFront("if(" + matchesTriedName + " < " + matchesName + ".Count) {\n"); // further match available -> try it
            source.Indent();
            source.AppendFront("continue;\n");
            source.Unindent();
            source.AppendFront("} else {\n"); // all matches tried, all failed later on -> end in fail
            source.Indent();
            source.AppendFront(SetResultVar(seq, "false"));
            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution succeeded, commit the changes so far and succeed
            source.AppendFront("graph.TransactionManager.Commit(transID_" + seq.Id + ");\n");
            source.AppendFront(SetResultVar(seq, "true"));
            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        public void EmitSequenceAll(SequenceNAry seqAll, bool disjunction, bool lazy, SourceBuilder source)
        {
            source.AppendFront(SetResultVar(seqAll, disjunction ? "false" : "true"));
            source.AppendFrontFormat("bool continue_{0} = true;\n", seqAll.Id);
            source.AppendFrontFormat("List<int> sequencestoexecutevar_{0} = new List<int>({1});\n", seqAll.Id, seqAll.Sequences.Count);
            source.AppendFrontFormat("for(int i = 0; i < {1}; ++i) sequencestoexecutevar_{0}.Add(i);\n", seqAll.Id, seqAll.Sequences.Count);
            source.AppendFrontFormat("while(sequencestoexecutevar_{0}.Count>0 && continue_{0})\n", seqAll.Id);
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("int positionofsequencetoexecute_{0} = GRGEN_LIBGR.Sequence.randomGenerator.Next(sequencestoexecutevar_{0}.Count);\n", seqAll.Id);
            source.AppendFrontFormat("switch(sequencestoexecutevar_{0}[positionofsequencetoexecute_{0}])\n", seqAll.Id);
            source.AppendFront("{\n");
            source.Indent();
            for(int i = 0; i < seqAll.Sequences.Count; ++i)
            {
                source.AppendFrontFormat("case {0}:\n", i);
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqAll.Sequences[i], source);
                source.AppendFrontFormat("sequencestoexecutevar_{0}.Remove({1});\n", seqAll.Id, i);
                source.AppendFront(SetResultVar(seqAll, GetResultVar(seqAll) + (disjunction ? " || " : " && ") + GetResultVar(seqAll.Sequences[i])));
                if(lazy)
                    source.AppendFrontFormat("if(" + (disjunction?"":"!") + GetResultVar(seqAll) + ") continue_{0} = false;\n", seqAll.Id);
                source.AppendFront("break;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        void EmitSequenceSome(SequenceSomeFromSet seqSome, SourceBuilder source)
        {
            source.AppendFront(SetResultVar(seqSome, "false"));

            // emit code for matching all the contained rules
            for (int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
                String specialStr = seqRule.Special ? "true" : "false";
                String parameters = BuildParameters(paramBindings);
                String matchingPatternClassName = "Rule_" + paramBindings.Name;
                String patternName = paramBindings.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;
                source.AppendFront(matchesType + " " + matchesName + " = rule_" + paramBindings.Name
                    + ".Match(graph, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "graph.MaxMatches")
                    + parameters + ");\n");
                if (gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                source.Indent();
                source.AppendFront(SetResultVar(seqSome, "true"));
                source.Unindent();
                source.AppendFront("}\n");
            }

            // emit code for deciding on the match to rewrite
            String totalMatchToApply = "total_match_to_apply_" + seqSome.Id;
            String curTotalMatch = "cur_total_match_" + seqSome.Id;
            if (seqSome.Random)
            {
                source.AppendFront("int " + totalMatchToApply + " = 0;\n");
                for (int i = 0; i < seqSome.Sequences.Count; ++i)
                {
                    SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                    String matchesName = "matches_" + seqRule.Id;
                    if (seqRule.SequenceType == SequenceType.RuleCall)
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                    else if (!((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                        source.AppendFront("if(" + matchesName + ".Count>0) ++" + totalMatchToApply + ";\n");
                    else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                }
                source.AppendFront(totalMatchToApply + " = GRGEN_LIBGR.Sequence.randomGenerator.Next(" + totalMatchToApply + ");\n");
                source.AppendFront("int " + curTotalMatch + " = 0;\n");
            }

            // code to handle the rewrite next match
            String firstRewrite = "first_rewrite_" + seqSome.Id;
            source.AppendFront("bool " + firstRewrite + " = true;\n");

            // emit code for rewriting all the contained rules which got matched
            for (int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
                String specialStr = seqRule.Special ? "true" : "false";
                String matchingPatternClassName = "Rule_" + paramBindings.Name;
                String patternName = paramBindings.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchName = "match_" + seqRule.Id;
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;

                if(seqSome.Random)
                    source.AppendFront("if(" + matchesName + ".Count!=0 && " + curTotalMatch + "<=" + totalMatchToApply + ") {\n");
                else
                    source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                source.Indent();

                String returnParameterDeclarations;
                String returnArguments;
                String returnAssignments;
                BuildReturnParameters(paramBindings, out returnParameterDeclarations, out returnArguments, out returnAssignments);

                if (seqRule.SequenceType == SequenceType.RuleCall)
                {
                    if (seqSome.Random) {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                    if (gen.FireEvents) source.AppendFront("graph.Matched(" + matchesName + ", " + specialStr + ");\n");
                    if (gen.FireEvents) source.AppendFront("graph.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") graph.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
                    if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    if (gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo != null) graph.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");

                    if (seqSome.Random) {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else if (!((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                {
                    if (seqSome.Random)
                    {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    // iterate through matches, use Modify on each, fire the next match event after the first
                    String enumeratorName = "enum_" + seqRule.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    if (gen.FireEvents) source.AppendFront("graph.Matched(" + matchesName + ", " + specialStr + ");\n");
                    if (gen.FireEvents) source.AppendFront("graph.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") graph.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
                    if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    if (gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");
                    source.Unindent();
                    source.AppendFront("}\n");

                    if (seqSome.Random)
                    {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                {
                    if (seqSome.Random)
                    {
                        // for the match selected: rewrite it
                        String enumeratorName = "enum_" + seqRule.Id;
                        source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                        source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                        source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                        if (gen.FireEvents) source.AppendFront("graph.Matched(" + matchesName + ", " + specialStr + ");\n");
                        if (gen.FireEvents) source.AppendFront("graph.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") graph.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        if (gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        // randomly choose match, rewrite it and remove it from available matches
                        source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".GetMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                        if (gen.FireEvents) source.AppendFront("graph.Matched(" + matchesName + ", " + specialStr + ");\n");
                        if (gen.FireEvents) source.AppendFront("graph.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") graph.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + paramBindings.Name + ".Modify(graph, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        if (gen.UsePerfInfo) source.AppendFront("if(graph.PerformanceInfo!=null) graph.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                    }
                }

                if (gen.FireEvents) source.AppendFront("graph.Finished(" + matchesName + ", " + specialStr + ");\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        private String BuildParameters(InvocationParameterBindings paramBindings)
        {
            String parameters = "";
            for (int j = 0; j < paramBindings.ParamVars.Length; j++)
            {
                if (paramBindings.ParamVars[j] != null)
                {
                    String typeName;
                    if(rulesToInputTypes.ContainsKey(paramBindings.Name))
                        typeName = rulesToInputTypes[paramBindings.Name][j];
                    else
                        typeName = sequencesToInputTypes[paramBindings.Name][j];
                    String cast = "(" + TypesHelper.XgrsTypeToCSharpType(typeName, model) + ")";
                    parameters += ", " + cast + GetVar(paramBindings.ParamVars[j]);
                }
                else
                {
                    object arg = paramBindings.Parameters[j];
                    if (arg is bool)
                        parameters += ", " + ((bool)arg ? "true" : "false");
                    else if (arg is string)
                        parameters += ", \"" + (string)arg + "\"";
                    else if (arg is float)
                        parameters += "," + ((float)arg).ToString(System.Globalization.CultureInfo.InvariantCulture) + "f";
                    else if (arg is double)
                        parameters += "," + ((double)arg).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    else if (arg is sbyte)
                        parameters += ", (sbyte)(" + arg.ToString() + ")";
                    else if (arg is short)
                        parameters += ", (short)(" + arg.ToString() + ")";
                    else if (arg is long)
                        parameters += ", (long)(" + arg.ToString() + ")";
                    else // e.g. int
                        parameters += "," + arg.ToString();
                    // TODO: abolish constants as parameters or extend to set/map?
                }
            }
            return parameters;
        }

        private void BuildOutParameters(SequenceInvocationParameterBindings paramBindings, out String outParameterDeclarations, out String outArguments, out String outAssignments)
        {
            outParameterDeclarations = "";
            outArguments = "";
            outAssignments = "";
            for(int j = 0; j < sequencesToOutputTypes[paramBindings.Name].Count; j++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0) {
                    varName = paramBindings.ReturnVars[j].Prefix + paramBindings.ReturnVars[j].Name;
                } else {
                    varName = tmpVarCtr.ToString();
                    ++tmpVarCtr;
                }
                String typeName = sequencesToOutputTypes[paramBindings.Name][j];
                outParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName
                    + " = " + TypesHelper.DefaultValue(typeName, model) + ";";
                outArguments += ", ref tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    outAssignments += SetVar(paramBindings.ReturnVars[j], "tmpvar_" + varName);
            }
        }

        private void BuildReturnParameters(RuleInvocationParameterBindings paramBindings, out String returnParameterDeclarations, out String returnArguments, out String returnAssignments)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            returnParameterDeclarations = "";
            returnArguments = "";
            returnAssignments = "";
            for(int j = 0; j < rulesToOutputTypes[paramBindings.Name].Count; j++)
            {
                String varName;
                if(paramBindings.ReturnVars.Length != 0) {
                    varName = paramBindings.ReturnVars[j].Prefix + paramBindings.ReturnVars[j].Name;
                } else {
                    varName = tmpVarCtr.ToString();
                    ++tmpVarCtr;
                }
                String typeName = rulesToOutputTypes[paramBindings.Name][j];
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName + "; ";
                returnArguments += ", out tmpvar_" + varName;
                if(paramBindings.ReturnVars.Length != 0)
                    returnAssignments += SetVar(paramBindings.ReturnVars[j], "tmpvar_" + varName);
            }
        }

        string GetSequenceExpression(SequenceExpression expr)
        {
            switch(expr.SequenceExpressionType)
            {
                case SequenceExpressionType.Def:
                    {
                        SequenceExpressionDef seqDef = (SequenceExpressionDef)expr;
                        String condition = "";
                        bool isFirst = true;
                        foreach(SequenceVariable var in seqDef.DefVars)
                        {
                            if(isFirst) isFirst = false;
                            else condition += " && ";
                            condition += GetVar(var) + "!=null";
                        }
                        return condition;
                    }

                case SequenceExpressionType.True:
                    return "true";

                case SequenceExpressionType.False:
                    return "false";

                case SequenceExpressionType.InContainer:
                    {
                        SequenceExpressionInContainer seqIn = (SequenceExpressionInContainer)expr;

                        if(seqIn.Container.Type == "")
                        {
                            SourceBuilder source = new SourceBuilder();

                            string sourceValue = GetVar(seqIn.Var);
                            source.AppendFront("(" + GetVar(seqIn.Container) + " is IList ? ");

                            string array = "((System.Collections.IList)" + GetVar(seqIn.Container) + ")";
                            source.AppendFront(array + ".Contains(" + sourceValue + ")");

                            source.AppendFront(" : ");

                            string dictionary = "((System.Collections.IDictionary)" + GetVar(seqIn.Container) + ")";
                            source.AppendFront(dictionary + ".Contains(" + sourceValue + ")");

                            source.AppendFront(")");

                            return source.ToString();
                        }
                        else if(seqIn.Container.Type.StartsWith("array"))
                        {
                            string array = GetVar(seqIn.Container);
                            string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqIn.Container.Type), model);
                            string sourceValue = "((" + arrayValueType + ")" + GetVar(seqIn.Var) + ")";
                            return array + ".Contains(" + sourceValue + ")";
                        }
                        else
                        {
                            string dictionary = GetVar(seqIn.Container);
                            string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqIn.Container.Type), model);
                            string sourceValue = "((" + dictSrcType + ")" + GetVar(seqIn.Var) + ")";
                            return dictionary + ".ContainsKey(" + sourceValue + ")";
                        }
                    }

                case SequenceExpressionType.IsVisited:
                    {
                        SequenceExpressionIsVisited seqIsVisited = (SequenceExpressionIsVisited)expr;
                        return "graph.IsVisited("
                            + "(GRGEN_LIBGR.IGraphElement)" + GetVar(seqIsVisited.GraphElementVar)
                            + ", (int)" + GetVar(seqIsVisited.VisitedFlagVar)
                            + ")";
                    }

                case SequenceExpressionType.VAlloc:
                    return "graph.AllocateVisitedFlag()";

                case SequenceExpressionType.ContainerSize:
                    {
                        SequenceExpressionContainerSize seqContainerSizeToVar = (SequenceExpressionContainerSize)expr;

                        if(seqContainerSizeToVar.Container.Type == "")
                        {
                            SourceBuilder source = new SourceBuilder();

                            source.AppendFront("(" + GetVar(seqContainerSizeToVar.Container) + " is IList ? ");

                            string array = "((System.Collections.IList)" + GetVar(seqContainerSizeToVar.Container) + ")";
                            source.AppendFront(array + ".Count");

                            source.AppendFront(" : ");

                            string dictionary = "((System.Collections.IDictionary)" + GetVar(seqContainerSizeToVar.Container) + ")";
                            source.AppendFront(dictionary + ".Count");

                            source.AppendFront(")");

                            return source.ToString();
                        }
                        else if(seqContainerSizeToVar.Container.Type.StartsWith("array"))
                        {
                            string array = GetVar(seqContainerSizeToVar.Container);
                            return array + ".Count";
                        }
                        else
                        {
                            string dictionary = GetVar(seqContainerSizeToVar.Container);
                            return dictionary + ".Count";
                        }
                    }

                case SequenceExpressionType.ContainerEmpty:
                    {
                        SequenceExpressionContainerEmpty seqContainerEmptyToVar = (SequenceExpressionContainerEmpty)expr;

                        if(seqContainerEmptyToVar.Container.Type == "")
                        {
                            SourceBuilder source = new SourceBuilder();

                            source.AppendFront("(" + GetVar(seqContainerEmptyToVar.Container) + " is IList ?");

                            string array = "((System.Collections.IList)" + GetVar(seqContainerEmptyToVar.Container) + ")";
                            source.AppendFront(array + ".Count==0");

                            source.AppendFront(" : ");

                            string dictionary = "((System.Collections.IDictionary)" + GetVar(seqContainerEmptyToVar.Container) + ")";
                            source.AppendFront(dictionary + ".Count==0");

                            source.AppendFront(")");

                            return source.ToString();
                        }
                        else if(seqContainerEmptyToVar.Container.Type.StartsWith("array"))
                        {
                            string array = GetVar(seqContainerEmptyToVar.Container);
                            return array + ".Count==0";
                        }
                        else
                        {
                            string dictionary = GetVar(seqContainerEmptyToVar.Container);
                            return dictionary + ".Count==0";
                        }
                    }

                case SequenceExpressionType.ContainerAccess:
                    {
                        SequenceExpressionContainerAccess seqContainerAccessToVar = (SequenceExpressionContainerAccess)expr; // todo: dst type unknownTypesHelper.ExtractSrc(seqMapAccessToVar.Setmap.Type)

                        if(seqContainerAccessToVar.Container.Type == "")
                        {
                            SourceBuilder source = new SourceBuilder();

                            string sourceValue = GetVar(seqContainerAccessToVar.KeyVar);
                            source.AppendFront("(" + GetVar(seqContainerAccessToVar.Container) + " is IList ? ");

                            string array = "((System.Collections.IList)" + GetVar(seqContainerAccessToVar.Container) + ")";
                            if(!TypesHelper.IsSameOrSubtype(seqContainerAccessToVar.KeyVar.Type, "int", model))
                            {
                                source.AppendFront(array + "[-1]");
                            }
                            else
                            {
                                source.AppendFront(array + "[(int)" + sourceValue + "]");
                            }

                            source.AppendFront(" : ");

                            string dictionary = "((System.Collections.IDictionary)" + GetVar(seqContainerAccessToVar.Container) + ")";
                            source.AppendFront(dictionary + "[" + sourceValue + "]");

                            source.AppendFront(")");

                            return source.ToString();
                        }
                        else if(seqContainerAccessToVar.Container.Type.StartsWith("array"))
                        {
                            string array = GetVar(seqContainerAccessToVar.Container);
                            string sourceValue = "((int)" + GetVar(seqContainerAccessToVar.KeyVar) + ")";
                            return array + "[" + sourceValue + "]";
                        }
                        else
                        {
                            string dictionary = GetVar(seqContainerAccessToVar.Container);
                            string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqContainerAccessToVar.Container.Type), model);
                            string sourceValue = "((" + dictSrcType + ")" + GetVar(seqContainerAccessToVar.KeyVar) + ")";
                            return dictionary + "[" + sourceValue + "]";
                        }
                    }

                case SequenceExpressionType.Constant:
                    {
                        SequenceExpressionConstant seqConstToVar = (SequenceExpressionConstant)expr;
                        if(seqConstToVar.Constant is bool)
                        {
                            return (bool)seqConstToVar.Constant == true ? "true" : "false";
                        }
                        else if(seqConstToVar.Constant is Enum)
                        {
                            Enum enumConst = (Enum)seqConstToVar.Constant;
                            return enumConst.GetType().ToString() + "." + enumConst.ToString();
                        }
                        else if(seqConstToVar.Constant is IDictionary)
                        {
                            Type keyType;
                            Type valueType;
                            DictionaryListHelper.GetDictionaryTypes(seqConstToVar.Constant, out keyType, out valueType);
                            String srcType = "typeof(" + TypesHelper.PrefixedTypeFromType(keyType) + ")";
                            String dstType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                            return "GRGEN_LIBGR.DictionaryListHelper.NewDictionary(" + srcType + "," + dstType + ")";
                        }
                        else if(seqConstToVar.Constant is IList)
                        {
                            Type valueType;
                            DictionaryListHelper.GetListType(seqConstToVar.Constant, out valueType);
                            String arrayValueType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                            return "GRGEN_LIBGR.DictionaryListHelper.NewList(" + arrayValueType + ")";
                        }
                        else if(seqConstToVar.Constant is string)
                        {
                            return "\"" + seqConstToVar.Constant.ToString() + "\"";
                        }
                        else if(seqConstToVar.Constant is float)
                        {
                            return ((float)seqConstToVar.Constant).ToString(System.Globalization.CultureInfo.InvariantCulture) + "f";
                        }
                        else if(seqConstToVar.Constant is double)
                        {
                            return ((double)seqConstToVar.Constant).ToString(System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if(seqConstToVar.Constant is sbyte)
                        {
                            return "(sbyte)(" + seqConstToVar.Constant.ToString() + ")";
                        }
                        else if(seqConstToVar.Constant is short)
                        {
                            return "(short)(" + seqConstToVar.Constant.ToString() + ")";
                        }
                        else if(seqConstToVar.Constant is long)
                        {
                            return "(long)(" + seqConstToVar.Constant.ToString() + ")";
                        }
                        else
                        {
                            return seqConstToVar.Constant.ToString();
                        }
                    }

                case SequenceExpressionType.GraphElementAttribute:
                    {
                        SequenceExpressionAttribute seqAttrToVar = (SequenceExpressionAttribute)expr;
                        string element = "((GRGEN_LIBGR.IGraphElement)" + GetVar(seqAttrToVar.SourceVar) + ")";
                        string value = element + ".GetAttribute(\"" + seqAttrToVar.AttributeName + "\")";
                        return "GRGEN_LIBGR.DictionaryListHelper.IfAttributeOfElementIsDictionaryOrListThenCloneDictionaryOrListValue(" + element + ", \"" + seqAttrToVar.AttributeName + "\", " + value + ")";
                    }

                case SequenceExpressionType.ElementFromGraph:
                    {
                        throw new Exception("Internal Error: the AssignElemToVar is interpreted only (no NamedGraph available at lgsp level)");
                    }

                case SequenceExpressionType.Variable:
                    {
                        SequenceExpressionVariable seqVar = (SequenceExpressionVariable)expr;
                        return GetVar(seqVar.PredicateVar);
                    }

                default:
                    throw new Exception("Unknown sequence expression type: " + expr.SequenceExpressionType);
            }
        }

		public bool GenerateXGRSCode(string xgrsName, String xgrsStr,
            String[] paramNames, GrGenType[] paramTypes,
            String[] defToBeYieldedToNames, GrGenType[] defToBeYieldedToTypes,
            SourceBuilder source)
		{
			Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for (int i = 0; i < paramNames.Length; i++)
            {
                varDecls.Add(paramNames[i], TypesHelper.DotNetTypeToXgrsType(paramTypes[i]));
            }
            for(int i = 0; i < defToBeYieldedToNames.Length; i++)
            {
                varDecls.Add(defToBeYieldedToNames[i], TypesHelper.DotNetTypeToXgrsType(defToBeYieldedToTypes[i]));
            }
            String[] ruleNames = new String[rulesToInputTypes.Count];
            int j = 0;
            foreach(KeyValuePair<String, List<String>> ruleToInputTypes in rulesToInputTypes)
            {
                ruleNames[j] = ruleToInputTypes.Key;
                ++j;
            }
            String[] sequenceNames = new String[sequencesToInputTypes.Count];
            j = 0;
            foreach(KeyValuePair<String, List<String>> sequenceToInputTypes in sequencesToInputTypes)
            {
                sequenceNames[j] = sequenceToInputTypes.Key;
                ++j;
            }

			Sequence seq;
            try
            {
                seq = SequenceParser.ParseSequence(xgrsStr, ruleNames, sequenceNames, varDecls, model);
                LGSPSequenceChecker checker = new LGSPSequenceChecker(ruleNames, sequenceNames, rulesToInputTypes, rulesToOutputTypes,
                                                    sequencesToInputTypes, sequencesToOutputTypes, model);
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
                HandleSequenceParserException(ex);
                return false;
            }

            source.Append("\n");
            source.AppendFront("public static bool ApplyXGRS_" + xgrsName + "(GRGEN_LGSP.LGSPGraph graph");
			for(int i = 0; i < paramNames.Length; i++)
			{
				source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(paramTypes[i]), model) + " var_");
				source.Append(paramNames[i]);
			}
            for(int i = 0; i < defToBeYieldedToTypes.Length; i++)
            {
                source.Append(", out " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(defToBeYieldedToTypes[i]), model) + " var_");
                source.Append(defToBeYieldedToNames[i]);
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

        public bool GenerateDefinedSequences(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for(int i = 0; i < sequence.Parameters.Length; i++)
            {
                varDecls.Add(sequence.Parameters[i], TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]));
            }
            for(int i = 0; i < sequence.OutParameters.Length; i++)
            {
                varDecls.Add(sequence.OutParameters[i], TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]));
            }
            String[] ruleNames = new String[rulesToInputTypes.Count];
            int j = 0;
            foreach(KeyValuePair<String, List<String>> ruleToInputTypes in rulesToInputTypes)
            {
                ruleNames[j] = ruleToInputTypes.Key;
                ++j;
            }
            String[] sequenceNames = new String[sequencesToInputTypes.Count];
            j = 0;
            foreach(KeyValuePair<String, List<String>> sequenceToInputTypes in sequencesToInputTypes)
            {
                sequenceNames[j] = sequenceToInputTypes.Key;
                ++j;
            }

            Sequence seq;
            try
            {
                seq = SequenceParser.ParseSequence(sequence.XGRS, ruleNames, sequenceNames, varDecls, model);
                LGSPSequenceChecker checker = new LGSPSequenceChecker(ruleNames, sequenceNames, rulesToInputTypes, rulesToOutputTypes,
                                                    sequencesToInputTypes, sequencesToOutputTypes, model);
                checker.Check(seq);
            }
            catch(ParseException ex)
            {
                Console.Error.WriteLine("In the defined sequence " + sequence.Name
                    + " the exec part \"" + sequence.XGRS
                    + "\" caused the following error:\n" + ex.Message);
                return false;
            }
            catch(SequenceParserException ex)
            {
                Console.Error.WriteLine("In the defined sequence " + sequence.Name
                    + " the exec part \"" + sequence.XGRS
                    + "\" caused the following error:\n");
                HandleSequenceParserException(ex);
                return false;
            }

            // exact sequence definition compiled class
            source.Append("\n");
            source.AppendFront("public class Sequence_" + sequence.Name + " : GRGEN_LIBGR.SequenceDefinitionCompiled\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateSequenceDefinedSingleton(source, sequence);

            source.Append("\n");
            GenerateInternalDefinedSequenceApplicationMethod(source, sequence, seq);

            source.Append("\n");
            GenerateExactExternalDefinedSequenceApplicationMethod(source, sequence);

            source.Append("\n");
            GenerateGenericExternalDefinedSequenceApplicationMethod(source, sequence);

            // end of exact sequence definition compiled class
            source.Unindent();
            source.AppendFront("}\n");

            return true;
        }

        private void GenerateSequenceDefinedSingleton(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            String className = "Sequence_" + sequence.Name;
            source.AppendFront("private static "+className+" instance = null;\n");

            source.AppendFront("public static "+className+" Instance { get { if(instance==null) instance = new "+className+"(); return instance; } }\n");

            source.AppendFront("private "+className+"() : base(\""+sequence.Name+"\", SequenceInfo_"+sequence.Name+".Instance) { }\n");
        }

        private void GenerateInternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence, Sequence seq)
        {
            source.AppendFront("public static bool ApplyXGRS_" + sequence.Name + "(GRGEN_LGSP.LGSPGraph graph");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
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
        }

        private void GenerateExactExternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            source.AppendFront("public static bool Apply_" + sequence.Name + "(GRGEN_LIBGR.IGraph graph");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
            }
            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model);
                source.AppendFront(typeName + " vari_" + sequence.OutParameters[i]);
                source.Append(" = " + TypesHelper.DefaultValue(typeName, model) + ";\n");
            }
            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraph)graph");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", var_" + sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref var_" + sequence.OutParameters[i]);
            }
            source.Append(");\n");
            if(sequence.OutParameters.Length > 0)
            {
                source.AppendFront("if(result) {\n");
                source.Indent();
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.AppendFront("var_" + sequence.OutParameters[i]);
                    source.Append(" = vari_" + sequence.OutParameters[i] + ";\n");
                }
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("return result;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateGenericExternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            source.AppendFront("public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.SequenceExecutionEnvironment env)");
            source.AppendFront("{\n");
            source.Indent();

            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.Parameters[i]);
                source.Append(" = (" + typeName + ")sequenceInvocation.ParamVars[" + i + "].GetVariableValue(graph);\n");
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.OutParameters[i]);
                source.Append(" = " + TypesHelper.DefaultValue(typeName, model) + ";\n");
            }

            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraph)graph");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", var_" + sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref var_" + sequence.OutParameters[i]);
            }
            source.Append(");\n");
            if(sequence.OutParameters.Length > 0)
            {
                source.AppendFront("if(result) {\n");
                source.Indent();
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.AppendFront("sequenceInvocation.ReturnVars[" + i + "].SetVariableValue(var_" + sequence.OutParameters[i] + ", graph);\n");
                }
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("return result;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        void HandleSequenceParserException(SequenceParserException ex)
        {
            if(ex.Name == null && ex.Kind != SequenceParserError.TypeMismatch)
            {
                Console.Error.WriteLine("Unknown rule/sequence: \"{0}\"", ex.Name);
                return;
            }

            switch(ex.Kind)
            {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if(rulesToInputTypes[ex.Name].Count != ex.NumGivenInputs && rulesToOutputTypes[ex.Name].Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of parameters and return values for action/sequence \"" + ex.Name + "\"!");
                    else if(rulesToInputTypes[ex.Name].Count != ex.NumGivenInputs)
                        Console.Error.WriteLine("Wrong number of parameters for action/sequence \"" + ex.Name + "\"!");
                    else if(rulesToOutputTypes[ex.Name].Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of return values for action/sequence \"" + ex.Name + "\"!");
                    else
                        goto default;
                    break;

                case SequenceParserError.BadParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". parameter is not valid for action/sequence \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.BadReturnParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". return parameter is not valid for action/sequence \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.RuleNameUsedByVariable:
                    Console.Error.WriteLine("The name of the variable conflicts with the name of action/sequence \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    Console.Error.WriteLine("The variable \"" + ex.Name + "\" may neither receive parameters nor return values!");
                    return;

                case SequenceParserError.UnknownAttribute:
                    Console.WriteLine("Unknown attribute \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.TypeMismatch:
                    Console.Error.WriteLine("The construct \"" + ex.VariableOrFunctionName + "\" expects:" + ex.ExpectedType + " but is / is given " + ex.GivenType + "!");
                    return;

                default:
                    throw new ArgumentException("Invalid error kind: " + ex.Kind);
            }

            // todo: Sequence
            Console.Error.Write("Prototype: {0}", ex.Name);
            if(rulesToInputTypes[ex.Name].Count != 0)
            {
                Console.Error.Write("(");
                bool first = true;
                foreach(String typeName in rulesToInputTypes[ex.Name])
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
            if(rulesToOutputTypes[ex.Name].Count != 0)
            {
                Console.Error.Write(" : (");
                bool first = true;
                foreach(String typeName in rulesToOutputTypes[ex.Name])
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
            Console.Error.WriteLine();
        }
    }
}
