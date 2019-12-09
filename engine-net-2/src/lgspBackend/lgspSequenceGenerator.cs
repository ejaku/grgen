/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
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
        /// <param name="procEnv">the processing environment on which to apply the sequence, esp. containing the graph</param>
        /// <returns>the result of sequence execution</returns>
        public abstract bool exec(LGSPGraphProcessingEnvironment procEnv);
    }

    /// <summary>
    /// The C#-part responsible for compiling the XGRSs of the exec statements.
    /// </summary>
    public class LGSPSequenceGenerator
    {
        IGraphModel model;

        SequenceCheckingEnvironmentCompiled env;

        NeededEntitiesEmitter neededEntitiesEmitter;

        SequenceComputationGenerator compGen;

        SequenceExpressionGenerator exprGen;

        SequenceGeneratorHelper helper;

        ActionNames actionNames;

        bool fireDebugEvents;
        bool emitProfiling;


        public LGSPSequenceGenerator(IGraphModel model, ActionsTypeInformation actionsTypeInformation,
            bool fireDebugEvents, bool emitProfiling)
        {
            this.model = model;

            this.actionNames = new ActionNames(actionsTypeInformation);

            this.env = new SequenceCheckingEnvironmentCompiled(actionNames, actionsTypeInformation, model);

            this.helper = new SequenceGeneratorHelper(model, actionsTypeInformation, env);

            this.exprGen = new SequenceExpressionGenerator(model, env, helper);

            this.helper.SetSequenceExpressionGenerator(exprGen);

            this.compGen = new SequenceComputationGenerator(model, env, exprGen, helper, fireDebugEvents);

            this.neededEntitiesEmitter = new NeededEntitiesEmitter(compGen, helper);

            this.fireDebugEvents = fireDebugEvents;
            this.emitProfiling = emitProfiling;
        }

		private void EmitLazyOp(SequenceBinary seq, SourceBuilder source, bool reversed)
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
                source.AppendFront("if(" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "true"));
                source.Unindent();
            } else if(seq.SequenceType == SequenceType.LazyAnd) {
                source.AppendFront("if(!" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "false"));
                source.Unindent();
            } else { //seq.SequenceType==SequenceType.IfThen -- lazy implication
                source.AppendFront("if(!" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "true"));
                source.Unindent();
            }

			source.AppendFront("else\n");
			source.AppendFront("{\n");
			source.Indent();

            EmitSequence(seqRight, source);
            source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqRight)));

            source.Unindent();
			source.AppendFront("}\n");
		}

        private void EmitRuleOrRuleAllCall(SequenceRuleCall seqRule, SourceBuilder source)
        {
            RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
            String specialStr = seqRule.Special ? "true" : "false";
            String parameterDeclarations = null;
            String parameters = null;
            if(paramBindings.Subgraph != null)
                parameters = helper.BuildParametersInDeclarations(paramBindings, out parameterDeclarations);
            else
                parameters = helper.BuildParameters(paramBindings);
            String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seqRule.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seqRule.Id;

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + helper.GetVar(paramBindings.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                + parameters + ");\n");
            for(int i = 0; i < seqRule.Filters.Count; ++i)
            {
                EmitFilterCall(source, seqRule.Filters[i], patternName, matchesName);
            }

            if(fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
            if(seqRule is SequenceRuleCountAllCall)
            {
                SequenceRuleCountAllCall seqRuleCountAll = (SequenceRuleCountAllCall)seqRule;
                source.AppendFront(helper.SetVar(seqRuleCountAll.CountResult, matchesName + ".Count"));
            }

            if(seqRule is SequenceRuleAllCall
                && ((SequenceRuleAllCall)seqRule).ChooseRandom
                && ((SequenceRuleAllCall)seqRule).MinSpecified)
            {
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                source.AppendFrontFormat("int minmatchesvar_{0} = (int){1};\n", seqRuleAll.Id, helper.GetVar(seqRuleAll.MinVarChooseRandom));
                source.AppendFrontFormat("if({0}.Count < minmatchesvar_{1}) {{\n", matchesName, seqRuleAll.Id);
            }
            else
            {
                source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            }
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqRule, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqRule, "true"));
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            helper.BuildReturnParameters(paramBindings,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            if(seqRule.SequenceType == SequenceType.RuleCall)
            {
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            }
            else if(seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
            {
                // iterate through matches, use Modify on each, fire the next match event after the first
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                String enumeratorName = "enum_" + seqRule.Id;
                source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                source.AppendFront("if(" + matchName + "!=" + matchesName + ".FirstExact) procEnv.RewritingNextMatch();\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
            }
            else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
            {
                // as long as a further rewrite has to be selected: randomly choose next match, rewrite it and remove it from available matches; fire the next match event after the first
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                source.AppendFrontFormat("int numchooserandomvar_{0} = (int){1};\n", seqRuleAll.Id, seqRuleAll.MaxVarChooseRandom != null ? helper.GetVar(seqRuleAll.MaxVarChooseRandom) : (seqRuleAll.MinSpecified ? "2147483647" : "1"));
                source.AppendFrontFormat("if({0}.Count < numchooserandomvar_{1}) numchooserandomvar_{1} = {0}.Count;\n", matchesName, seqRule.Id);
                source.AppendFrontFormat("for(int i = 0; i < numchooserandomvar_{0}; ++i)\n", seqRule.Id);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("if(i != 0) procEnv.RewritingNextMatch();\n");
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
            }

            if(fireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.Unindent();
            source.AppendFront("}\n");

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

        private void EmitSequenceCall(SequenceSequenceCall seqSeq, SourceBuilder source)
        {
            SequenceInvocationParameterBindings paramBindings = seqSeq.ParamBindings;
            String parameterDeclarations = null;
            String parameters = null;
            if(paramBindings.Subgraph != null)
                parameters = helper.BuildParametersInDeclarations(paramBindings, out parameterDeclarations);
            else
                parameters = helper.BuildParameters(paramBindings);
            String outParameterDeclarations;
            String outArguments;
            String outAssignments;
            helper.BuildOutParameters(paramBindings, out outParameterDeclarations, out outArguments, out outAssignments);

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + helper.GetVar(paramBindings.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            if(outParameterDeclarations.Length != 0)
                source.AppendFront(outParameterDeclarations + "\n");
            source.AppendFront("if(" + TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Sequence_" + paramBindings.Name + ".ApplyXGRS_" + paramBindings.Name
                                + "(procEnv" + parameters + outArguments + ")) {\n");
            source.Indent();
            if(outAssignments.Length != 0)
                source.AppendFront(outAssignments + "\n");
            source.AppendFront(compGen.SetResultVar(seqSeq, "true"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqSeq, "false"));
            source.Unindent();
            source.AppendFront("}\n");

            if(paramBindings.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

		void EmitSequence(Sequence seq, SourceBuilder source)
		{
			switch(seq.SequenceType)
			{
				case SequenceType.RuleCall:
                case SequenceType.RuleAllCall:
                case SequenceType.RuleCountAllCall:
                    EmitRuleOrRuleAllCall((SequenceRuleCall)seq, source);
                    break;

                case SequenceType.SequenceCall:
                    EmitSequenceCall((SequenceSequenceCall)seq, source);
                    break;

				case SequenceType.Not:
				{
					SequenceNot seqNot = (SequenceNot) seq;
					EmitSequence(seqNot.Seq, source);
					source.AppendFront(compGen.SetResultVar(seqNot, "!"+compGen.GetResultVar(seqNot.Seq)));
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
                        source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqBin.Left)));
                        break;
                    } else if(seq.SequenceType==SequenceType.ThenRight) {
                        source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqBin.Right)));
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
				    source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqBin.Left) + " "+op+" " + compGen.GetResultVar(seqBin.Right)));
					break;
				}

                case SequenceType.IfThenElse:
                {
                    SequenceIfThenElse seqIf = (SequenceIfThenElse)seq;

                    EmitSequence(seqIf.Condition, source);

                    source.AppendFront("if(" + compGen.GetResultVar(seqIf.Condition) + ")");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.TrueCase, source);
                    source.AppendFront(compGen.SetResultVar(seqIf, compGen.GetResultVar(seqIf.TrueCase)));

                    source.Unindent();
                    source.AppendFront("}\n");
                    source.AppendFront("else\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.FalseCase, source);
                    source.AppendFront(compGen.SetResultVar(seqIf, compGen.GetResultVar(seqIf.FalseCase)));

                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForContainer:
                {
                    SequenceForContainer seqFor = (SequenceForContainer)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    if(seqFor.Container.Type == "")
                    {
                        // type not statically known? -> might be Dictionary or List or Deque dynamically, must decide at runtime
                        source.AppendFront("if(" + helper.GetVar(seqFor.Container) + " is IList) {\n");
                        source.Indent();

                        source.AppendFront("IList entry_" + seqFor.Id + " = (IList) " + helper.GetVar(seqFor.Container) + ";\n");
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else if(" + helper.GetVar(seqFor.Container) + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        source.AppendFront("GRGEN_LIBGR.IDeque entry_" + seqFor.Id + " = (GRGEN_LIBGR.IDeque) " + helper.GetVar(seqFor.Container) + ";\n");
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        source.AppendFront("foreach(DictionaryEntry entry_" + seqFor.Id + " in (IDictionary)" + helper.GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqFor.Container.Type.StartsWith("array"))
                    {
                        String arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        source.AppendFrontFormat("List<{0}> entry_{1} = (List<{0}>) " + helper.GetVar(seqFor.Container) + ";\n", arrayValueType, seqFor.Id);
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();

                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqFor.Container.Type.StartsWith("deque"))
                    {
                        String dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        source.AppendFrontFormat("GRGEN_LIBGR.Deque<{0}> entry_{1} = (GRGEN_LIBGR.Deque<{0}>) " + helper.GetVar(seqFor.Container) + ";\n", dequeValueType, seqFor.Id);
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();

                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        String srcTypeXgrs = TypesHelper.ExtractSrc(seqFor.Container.Type);
                        String srcType = TypesHelper.XgrsTypeToCSharpType(srcTypeXgrs, model);
                        String dstTypeXgrs = TypesHelper.ExtractDst(seqFor.Container.Type);
                        String dstType = TypesHelper.XgrsTypeToCSharpType(dstTypeXgrs, model);
                        source.AppendFront("foreach(KeyValuePair<" + srcType + "," + dstType + "> entry_" + seqFor.Id + " in " + helper.GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();

                        if(dstTypeXgrs== "SetValueType")
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        else
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));

                        if (seqFor.VarDst != null)
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }

                    break;
                }

                case SequenceType.ForIntegerRange:
                {
                    SequenceForIntegerRange seqFor = (SequenceForIntegerRange)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    String ascendingVar = "ascending_" + seqFor.Id;
                    String entryVar = "entry_" + seqFor.Id;
                    String limitVar = "limit_" + seqFor.Id;
                    source.AppendFrontFormat("int {0} = (int)({1});\n", entryVar, exprGen.GetSequenceExpression(seqFor.Left, source));
                    source.AppendFrontFormat("int {0} = (int)({1});\n", limitVar, exprGen.GetSequenceExpression(seqFor.Right, source));
                    source.AppendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");

                    source.AppendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));

                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.ForIndexAccessEquality:
                {
                    SequenceForIndexAccessEquality seqFor = (SequenceForIndexAccessEquality)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    String indexVar = "index_" + seqFor.Id;
                    source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
                    String entryVar = "entry_" + seqFor.Id;
                    source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                        entryVar, indexVar);
                    source.Append("(");
                    source.Append(exprGen.GetSequenceExpression(seqFor.Expr, source));
                    source.Append("))\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(emitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.ForIndexAccessOrdering:
                {
                    SequenceForIndexAccessOrdering seqFor = (SequenceForIndexAccessOrdering)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    String indexVar = "index_" + seqFor.Id;
                    source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
                    String entryVar = "entry_" + seqFor.Id;
                    source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                        entryVar, indexVar);

                    if(seqFor.Ascending)
                        source.Append("Ascending");
                    else
                        source.Append("Descending");
                    if(seqFor.From() != null && seqFor.To() != null)
                    {
                        source.Append("From");
                        if(seqFor.IncludingFrom())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("To");
                        if(seqFor.IncludingTo())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(exprGen.GetSequenceExpression(seqFor.From(), source));
                        source.Append(", ");
                        source.Append(exprGen.GetSequenceExpression(seqFor.To(), source));
                    }
                    else if(seqFor.From() != null)
                    {
                        source.Append("From");
                        if(seqFor.IncludingFrom())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(exprGen.GetSequenceExpression(seqFor.From(), source));
                    }
                    else if(seqFor.To() != null)
                    {
                        source.Append("To");
                        if(seqFor.IncludingTo())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(exprGen.GetSequenceExpression(seqFor.To(), source));
                    }
                    else
                    {
                        source.Append("(");
                    }

                    source.Append("))\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(emitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.Unindent();
                    source.AppendFront("}\n");
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
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);

                    SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 2 ? seqFor.ArgExprs[1] : null;
                    string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
                    SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
                    string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);

                    string iterationVariable; // valid for incident/adjacent and reachable
                    string iterationType;
                    string edgeMethod = null; // only valid for incident/adajcent
                    string theOther = null; // only valid for incident/adjacent
                    string reachableMethod = null; // only valid for reachable
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForAdjacentNodes:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForAdjacentNodesViaIncoming:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForAdjacentNodesViaOutgoing:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForIncidentEdges:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForIncomingEdges:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForOutgoingEdges:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableNodes:
                            reachableMethod = "";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableNodesViaIncoming:
                            reachableMethod = "Incoming";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableNodesViaOutgoing:
                            reachableMethod = "Outgoing";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableEdges:
                            reachableMethod = "Edges";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableEdgesViaIncoming:
                            reachableMethod = "EdgesIncoming";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableEdgesViaOutgoing:
                            reachableMethod = "EdgesOutgoing";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        default:
                            edgeMethod = theOther = iterationVariable = iterationType = "INTERNAL ERROR";
                            break;
                    }

                    string profilingArgument = emitProfiling ? ", procEnv" : "";
                    if(seqFor.SequenceType == SequenceType.ForReachableNodes || seqFor.SequenceType == SequenceType.ForReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableNodesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GraphHelper.Reachable{1}(node_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else if(seqFor.SequenceType == SequenceType.ForReachableEdges || seqFor.SequenceType == SequenceType.ForReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableEdgesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GraphHelper.Reachable{1}(node_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else
                    {
                        if(emitProfiling)
                            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.{1})\n",
                                seqFor.Id, edgeMethod);
                        else
                            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatible{1}({2}))\n",
                                seqFor.Id, edgeMethod, incidentEdgeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();

                    if(seqFor.SequenceType != SequenceType.ForReachableNodes && seqFor.SequenceType != SequenceType.ForReachableNodesViaIncoming && seqFor.SequenceType != SequenceType.ForReachableNodesViaOutgoing
                        && seqFor.SequenceType != SequenceType.ForReachableEdges && seqFor.SequenceType != SequenceType.ForReachableEdgesViaIncoming || seqFor.SequenceType != SequenceType.ForReachableEdgesViaOutgoing)
                    {
                        if(emitProfiling)
                        {
                            source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                            source.AppendFrontFormat("if(!edge_{0}.InstanceOf(", seqFor.Id);
                            source.Append(incidentEdgeTypeExpr);
                            source.Append("))\n");
                            source.AppendFront("\tcontinue;\n");
                        }

                        // incident/adjacent needs a check for adjacent node, cause only incident edge can be type constrained in the loop
                        // reachable already allows to iterate exactly the edges of interest
                        source.AppendFrontFormat("if(!{0}.InstanceOf({1}))\n",
                            theOther, adjacentNodeTypeExpr);
                        source.AppendFront("\tcontinue;\n");
                    }

                    source.AppendFront(helper.SetVar(seqFor.Var, iterationVariable));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForBoundedReachableNodes:
                case SequenceType.ForBoundedReachableNodesViaIncoming:
                case SequenceType.ForBoundedReachableNodesViaOutgoing:
                case SequenceType.ForBoundedReachableEdges:
                case SequenceType.ForBoundedReachableEdgesViaIncoming:
                case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);
                    string depthExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[1], source);
                    source.AppendFrontFormat("int depth_{0} = (int)({1});\n", seqFor.Id, depthExpr);

                    SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
                    string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
                    SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 4 ? seqFor.ArgExprs[3] : null;
                    string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);

                    string iterationVariable; // valid for incident/adjacent and reachable
                    string iterationType;
                    string edgeMethod = null; // only valid for incident/adajcent
                    string theOther = null; // only valid for incident/adjacent
                    string reachableMethod = null; // only valid for reachable
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForBoundedReachableNodes:
                            reachableMethod = "";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableNodesViaIncoming:
                            reachableMethod = "Incoming";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableNodesViaOutgoing:
                            reachableMethod = "Outgoing";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableEdges:
                            reachableMethod = "Edges";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForBoundedReachableEdgesViaIncoming:
                            reachableMethod = "EdgesIncoming";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                            reachableMethod = "EdgesOutgoing";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        default:
                            edgeMethod = theOther = iterationVariable = iterationType = "INTERNAL ERROR";
                            break;
                    }

                    string profilingArgument = emitProfiling ? ", procEnv" : "";
                    if(seqFor.SequenceType == SequenceType.ForBoundedReachableNodes || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}(node_{0}, depth_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else if(seqFor.SequenceType == SequenceType.ForBoundedReachableEdges || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}(node_{0}, depth_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront(helper.SetVar(seqFor.Var, iterationVariable));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForNodes:
                case SequenceType.ForEdges:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    if (seqFor.SequenceType == SequenceType.ForNodes)
                    {
                        SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                        string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode elem_{0} in graph.GetCompatibleNodes({1}))\n", seqFor.Id, adjacentNodeTypeExpr);
                    }
                    else
                    {
                        SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                        string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge elem_{0} in graph.GetCompatibleEdges({1}))\n", seqFor.Id, incidentEdgeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();
                    
                    if(emitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, "elem_" + seqFor.Id));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForMatch:
                {
                    SequenceForMatch seqFor = (SequenceForMatch)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    RuleInvocationParameterBindings paramBindings = seqFor.Rule.ParamBindings;
                    String specialStr = seqFor.Rule.Special ? "true" : "false";
                    String parameters = helper.BuildParameters(paramBindings);
                    String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
                    String patternName = paramBindings.Name;
                    String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                    String matchName = "match_" + seqFor.Id;
                    String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                    String matchesName = "matches_" + seqFor.Id;
                    source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                        + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
                    for(int i=0; i<seqFor.Rule.Filters.Count; ++i)
                    {
                        EmitFilterCall(source, seqFor.Rule.Filters[i], patternName, matchesName);
                    }

                    source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                    source.Indent();
                    source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
                    source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                    if(fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

                    String returnParameterDeclarations;
                    String returnArguments;
                    String returnAssignments;
                    String returnParameterDeclarationsAllCall;
                    String intermediateReturnAssignmentsAllCall;
                    String returnAssignmentsAllCall;
                    helper.BuildReturnParameters(paramBindings,
                        out returnParameterDeclarations, out returnArguments, out returnAssignments,
                        out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

                    // apply the sequence for every match found
                    String enumeratorName = "enum_" + seqFor.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, matchName));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

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
					source.AppendFront("if(!" + compGen.GetResultVar(seqMin.Seq) + ") break;\n");
					source.AppendFront("i_" + seqMin.Id + "++;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront(compGen.SetResultVar(seqMin, "i_" + seqMin.Id + " >= " + seqMin.Min));
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
                    source.AppendFront("if(!" + compGen.GetResultVar(seqMinMax.Seq) + ") break;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront(compGen.SetResultVar(seqMinMax, "i_" + seqMinMax.Id + " >= " + seqMinMax.Min));
					break;
				}

                case SequenceType.DeclareVariable:
                {
                    SequenceDeclareVariable seqDeclVar = (SequenceDeclareVariable)seq;
                    source.AppendFront(helper.SetVar(seqDeclVar.DestVar, TypesHelper.DefaultValueString(seqDeclVar.DestVar.Type, env.Model)));
                    source.AppendFront(compGen.SetResultVar(seqDeclVar, "true"));
                    break;
                }

				case SequenceType.AssignConstToVar:
				{
					SequenceAssignConstToVar seqToVar = (SequenceAssignConstToVar) seq;
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, helper.GetConstant(seqToVar.Constant)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
					break;
				}

                case SequenceType.AssignContainerConstructorToVar:
                {
                    SequenceAssignContainerConstructorToVar seqToVar = (SequenceAssignContainerConstructorToVar)seq;
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, exprGen.GetSequenceExpression(seqToVar.Constructor, source)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignVarToVar:
                {
                    SequenceAssignVarToVar seqToVar = (SequenceAssignVarToVar)seq;
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, helper.GetVar(seqToVar.Variable)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.OrAssignSequenceResultToVar:
                {
                    SequenceOrAssignSequenceResultToVar seqToVar = (SequenceOrAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq) + "|| (bool)" + helper.GetVar(seqToVar.DestVar)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AndAssignSequenceResultToVar:
                {
                    SequenceAndAssignSequenceResultToVar seqToVar = (SequenceAndAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq) + "&& (bool)" + helper.GetVar(seqToVar.DestVar)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignUserInputToVar:
                {
                    throw new Exception("Internal Error: the AssignUserInputToVar is interpreted only (no Debugger available at lgsp level)");
                }

                case SequenceType.AssignRandomIntToVar:
                {
                    SequenceAssignRandomIntToVar seqRandomToVar = (SequenceAssignRandomIntToVar)seq;
                    source.AppendFront(helper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.Next(" + seqRandomToVar.Number + ")"));
                    source.AppendFront(compGen.SetResultVar(seqRandomToVar, "true"));
                    break;
                }

                case SequenceType.AssignRandomDoubleToVar:
                {
                    SequenceAssignRandomDoubleToVar seqRandomToVar = (SequenceAssignRandomDoubleToVar)seq;
                    source.AppendFront(helper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()"));
                    source.AppendFront(compGen.SetResultVar(seqRandomToVar, "true"));
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

                case SequenceType.WeightedOne:
                {
                    SequenceWeightedOne seqWeighted = (SequenceWeightedOne)seq;
                    EmitSequenceWeighted(seqWeighted, source);
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
                    source.AppendFront("int transID_" + seqTrans.Id + " = procEnv.TransactionManager.Start();\n");
					EmitSequence(seqTrans.Seq, source);
                    source.AppendFront("if("+ compGen.GetResultVar(seqTrans.Seq) + ") procEnv.TransactionManager.Commit(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront("else procEnv.TransactionManager.Rollback(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront(compGen.SetResultVar(seqTrans, compGen.GetResultVar(seqTrans.Seq)));
					break;
				}

                case SequenceType.Backtrack:
                {
                    SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                    EmitSequenceBacktrack(seqBack, source);
                    break;
                }

                case SequenceType.Pause:
                {
                    SequencePause seqPause = (SequencePause)seq;
                    source.AppendFront("procEnv.TransactionManager.Pause();\n");
                    EmitSequence(seqPause.Seq, source);
                    source.AppendFront("procEnv.TransactionManager.Resume();\n");
                    source.AppendFront(compGen.SetResultVar(seqPause, compGen.GetResultVar(seqPause.Seq)));
                    break;
                }

                case SequenceType.ExecuteInSubgraph:
                {
                    SequenceExecuteInSubgraph seqExecInSub = (SequenceExecuteInSubgraph)seq;
                    string subgraph;
                    if(seqExecInSub.AttributeName == null)
                        subgraph = helper.GetVar(seqExecInSub.SubgraphVar);
                    else
                    {
                        string element = "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqExecInSub.SubgraphVar) + ")";
                        subgraph = element + ".GetAttribute(\"" + seqExecInSub.AttributeName + "\")";
                    }
                    source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + subgraph + ");\n");
                    source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
                    EmitSequence(seqExecInSub.Seq, source);
                    source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                    source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
                    source.AppendFront(compGen.SetResultVar(seqExecInSub, compGen.GetResultVar(seqExecInSub.Seq)));
                    break;
                }

                case SequenceType.BooleanComputation:
                {
                    SequenceBooleanComputation seqComp = (SequenceBooleanComputation)seq;
                    compGen.EmitSequenceComputation(seqComp.Computation, source);
                    if(seqComp.Computation.ReturnsValue)
                        source.AppendFront(compGen.SetResultVar(seqComp, "!GRGEN_LIBGR.TypesHelper.IsDefaultValue(" + compGen.GetResultVar(seqComp.Computation) + ")"));
                    else
                        source.AppendFront(compGen.SetResultVar(seqComp, "true"));
                    break;
                }

				default:
					throw new Exception("Unknown sequence type: " + seq.SequenceType);
			}
		}

        private void EmitSequenceBacktrack(SequenceBacktrack seq, SourceBuilder source)
        {
            RuleInvocationParameterBindings paramBindings = seq.Rule.ParamBindings;
            String specialStr = seq.Rule.Special ? "true" : "false";
            String parameters = helper.BuildParameters(paramBindings);
            String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
            String patternName = paramBindings.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seq.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seq.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            for(int i=0; i<seq.Rule.Filters.Count; ++i)
            {
                EmitFilterCall(source, seq.Rule.Filters[i], patternName, matchesName);
            }

            source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "true")); // shut up compiler
            source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            helper.BuildReturnParameters(paramBindings,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

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
            source.AppendFront("int transID_" + seq.Id + " = procEnv.TransactionManager.Start();\n");
            source.AppendFront("int oldRewritesPerformed_" + seq.Id + " = procEnv.PerformanceInfo.RewritesPerformed;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", " + matchName + ", " + specialStr + ");\n");
            if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            // rule applied, now execute the sequence
            EmitSequence(seq.Seq, source);

            // if sequence execution failed, roll the changes back and try the next match of the rule
            source.AppendFront("if(!" + compGen.GetResultVar(seq.Seq) + ") {\n");
            source.Indent();
            source.AppendFront("procEnv.TransactionManager.Rollback(transID_" + seq.Id + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed_" + seq.Id + ";\n");

            source.AppendFront("if(" + matchesTriedName + " < " + matchesName + ".Count) {\n"); // further match available -> try it
            source.Indent();
            source.AppendFront("continue;\n");
            source.Unindent();
            source.AppendFront("} else {\n"); // all matches tried, all failed later on -> end in fail
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "false"));
            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution succeeded, commit the changes so far and succeed
            source.AppendFront("procEnv.TransactionManager.Commit(transID_" + seq.Id + ");\n");
            source.AppendFront(compGen.SetResultVar(seq, "true"));
            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceAll(SequenceNAry seqAll, bool disjunction, bool lazy, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqAll, disjunction ? "false" : "true"));
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
                source.AppendFront(compGen.SetResultVar(seqAll, compGen.GetResultVar(seqAll) + (disjunction ? " || " : " && ") + compGen.GetResultVar(seqAll.Sequences[i])));
                if(lazy)
                    source.AppendFrontFormat("if(" + (disjunction?"":"!") + compGen.GetResultVar(seqAll) + ") continue_{0} = false;\n", seqAll.Id);
                source.AppendFront("break;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceWeighted(SequenceWeightedOne seqWeighted, SourceBuilder source)
        {
            source.AppendFrontFormat("double pointtoexec_{0} = GRGEN_LIBGR.Sequence.randomGenerator.NextDouble() * {1};\n", seqWeighted.Id, seqWeighted.Numbers[seqWeighted.Numbers.Count - 1].ToString(System.Globalization.CultureInfo.InvariantCulture));
            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
            {
                if(i == 0)
                    source.AppendFrontFormat("if(pointtoexec_{0} <= {1})\n", seqWeighted.Id, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                else if(i == seqWeighted.Sequences.Count - 1)
                    source.AppendFrontFormat("else\n");
                else
                    source.AppendFrontFormat("else if(pointtoexec_{0} <= {1})\n", seqWeighted.Id, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqWeighted.Sequences[i], source);
                source.AppendFront(compGen.SetResultVar(seqWeighted, compGen.GetResultVar(seqWeighted.Sequences[i])));
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        void EmitSequenceSome(SequenceSomeFromSet seqSome, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqSome, "false"));

            // emit code for matching all the contained rules
            for (int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                RuleInvocationParameterBindings paramBindings = seqRule.ParamBindings;
                String specialStr = seqRule.Special ? "true" : "false";
                String parameters = helper.BuildParameters(paramBindings);
                String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(paramBindings.Package) + "Rule_" + paramBindings.Name;
                String patternName = paramBindings.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;
                source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name)
                    + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                    + parameters + ");\n");
                for(int j=0; j<seqRule.Filters.Count; ++j)
                {
                    EmitFilterCall(source, seqRule.Filters[j], patternName, matchesName);
                }
                source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seqSome, "true"));
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
                    else if (seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
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
                String returnParameterDeclarationsAllCall;
                String intermediateReturnAssignmentsAllCall;
                String returnAssignmentsAllCall;
                helper.BuildReturnParameters(paramBindings,
                    out returnParameterDeclarations, out returnArguments, out returnAssignments,
                    out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

                if (seqRule.SequenceType == SequenceType.RuleCall)
                {
                    if (seqSome.Random) {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");

                    if (seqSome.Random) {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else if (seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                {
                    if (seqSome.Random)
                    {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    // iterate through matches, use Modify on each, fire the next match event after the first
                    if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                    String enumeratorName = "enum_" + seqRule.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                    source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                    if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
                    if(seqRule.SequenceType == SequenceType.RuleCountAllCall)
                    {
                        SequenceRuleCountAllCall ruleCountAll = (SequenceRuleCountAllCall)seqRule;
                        source.AppendFront(helper.SetVar(ruleCountAll.CountResult, matchesName + ".Count"));
                    }

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
                        if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
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
                        if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(paramBindings.Package, paramBindings.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                        source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                    }
                }

                if (fireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        private void EmitFilterCall(SourceBuilder source, FilterCall filterCall, string patternName, string matchesName)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                switch(filterCall.Name)
                {
                    case "keepFirst":
                        source.AppendFrontFormat("{0}.FilterKeepFirst((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepLast":
                        source.AppendFrontFormat("{0}.FilterKeepLast((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepFirstFraction":
                        source.AppendFrontFormat("{0}.FilterKeepFirstFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepLastFraction":
                        source.AppendFrontFormat("{0}.FilterKeepLastFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeFirst":
                        source.AppendFrontFormat("{0}.FilterRemoveFirst((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeLast":
                        source.AppendFrontFormat("{0}.FilterRemoveLast((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeFirstFraction":
                        source.AppendFrontFormat("{0}.FilterRemoveFirstFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeLastFraction":
                        source.AppendFrontFormat("{0}.FilterRemoveLastFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                }
            }
            else
            {
                if(filterCall.IsAutoGenerated && filterCall.Name == "auto")
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}(procEnv, {3});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, matchesName);
                else if(filterCall.IsAutoGenerated)
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}_{3}(procEnv, {4});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, filterCall.EntitySuffixForName, matchesName);
                else
                {
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}(procEnv, {2}",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), filterCall.Name, matchesName);
                    for(int i = 0; i < filterCall.ArgumentExpressions.Length; ++i)
                    {
                        source.AppendFormat(", ({0})({1})",
                            TypesHelper.XgrsTypeToCSharpType(helper.actionsTypeInformation.filterFunctionsToInputTypes[filterCall.Name][i], model),
                            exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[i], source));
                    } 
                    source.Append(");\n");
                }
            }
        }

		public bool GenerateXGRSCode(string xgrsName, String package, String xgrsStr,
            String[] paramNames, GrGenType[] paramTypes,
            String[] defToBeYieldedToNames, GrGenType[] defToBeYieldedToTypes,
            SourceBuilder source, int lineNr)
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

			Sequence seq;
            try
            {
                List<string> warnings = new List<string>();
                seq = SequenceParser.ParseSequence(xgrsStr, package,
                    actionNames, varDecls, model, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("The exec statement \"" + xgrsStr
                        + "\" given on line " + lineNr + " reported back:\n" + warning);
                }
                seq.Check(env);
                seq.SetNeedForProfilingRecursive(emitProfiling);
            }
            catch(ParseException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" given on line " + lineNr + " caused the following error:\n" + ex.Message);
                return false;
            }
            catch(SequenceParserException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" given on line " + lineNr + " caused the following error:\n");
                HandleSequenceParserException(ex);
                return false;
            }

            source.Append("\n");
            source.AppendFront("public static bool ApplyXGRS_" + xgrsName + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
			for(int i = 0; i < paramNames.Length; i++)
			{
				source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(paramTypes[i]), model) + " var_");
				source.Append(paramNames[i]);
			}
            for(int i = 0; i < defToBeYieldedToTypes.Length; i++)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(defToBeYieldedToTypes[i]), model) + " var_");
                source.Append(defToBeYieldedToNames[i]);
            }
            source.Append(")\n");
			source.AppendFront("{\n");
			source.Indent();

            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            source.AppendFront("GRGEN_LGSP.LGSPActions actions = procEnv.curActions;\n");

			neededEntitiesEmitter.Reset();

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugEntering(\"{0}\", \"{1}\");\n", InjectExec(xgrsName), xgrsStr.Replace("\\", "\\\\").Replace("\"", "\\\""));
            }

            neededEntitiesEmitter.EmitNeededVarAndRuleEntities(seq, source);

			EmitSequence(seq, source);

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugExiting(\"{0}\");\n", InjectExec(xgrsName));
            }

            source.AppendFront("return " + compGen.GetResultVar(seq) + ";\n");
			source.Unindent();
			source.AppendFront("}\n");

            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            seq.GetLocalVariables(variables, containerConstructors, null);
            foreach(SequenceExpressionContainerConstructor cc in containerConstructors)
            {
                GenerateContainerConstructor(cc, source);
            }

			return true;
		}

        private string InjectExec(string execName)
        {
            string rulePart = execName.Substring(0, execName.LastIndexOf('_'));
            string execNumberPart = execName.Substring(execName.LastIndexOf('_'));
            return rulePart + ".exec" + execNumberPart;
        }

        public bool GenerateDefinedSequence(SourceBuilder source, DefinedSequenceInfo sequence)
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

            Sequence seq;
            try
            {
                List<string> warnings = new List<string>();
                seq = SequenceParser.ParseSequence(sequence.XGRS, sequence.Package,
                    actionNames, varDecls, model, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("In the defined sequence " + sequence.Name
                        + " the exec part \"" + sequence.XGRS
                        + "\" reported back:\n" + warning);
                }
                seq.Check(env);
                seq.SetNeedForProfilingRecursive(emitProfiling);
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

            if(sequence.Package != null)
            {
                source.AppendFrontFormat("namespace {0}\n", sequence.Package);
                source.AppendFront("{\n");
                source.Indent();
            }

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

            if(sequence.Package != null)
            {
                source.Unindent();
                source.AppendFront("}\n");
            }

            return true;
        }

        public bool GenerateExternalDefinedSequence(SourceBuilder source, ExternalDefinedSequenceInfo sequence)
        {
            // exact sequence definition compiled class
            source.Append("\n");
            source.AppendFront("public partial class Sequence_" + sequence.Name + " : GRGEN_LIBGR.SequenceDefinitionCompiled\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateSequenceDefinedSingleton(source, sequence);

            source.Append("\n");
            GenerateExactExternalDefinedSequenceApplicationMethod(source, sequence);

            source.Append("\n");
            GenerateGenericExternalDefinedSequenceApplicationMethod(source, sequence);

            // end of exact sequence definition compiled class
            source.Unindent();
            source.AppendFront("}\n");

            return true;
        }

        public void GenerateExternalDefinedSequencePlaceholder(SourceBuilder source, ExternalDefinedSequenceInfo sequence, String externalActionsExtensionFilename)
        {
            source.Append("\n");
            source.AppendFront("public partial class Sequence_" + sequence.Name + "\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateInternalDefinedSequenceApplicationMethodStub(source, sequence, externalActionsExtensionFilename);

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateSequenceDefinedSingleton(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            String className = "Sequence_" + sequence.Name;
            source.AppendFront("private static "+className+" instance = null;\n");

            source.AppendFront("public static "+className+" Instance { get { if(instance==null) instance = new "+className+"(); return instance; } }\n");

            source.AppendFront("private "+className+"() : base(\""+sequence.PackagePrefixedName+"\", SequenceInfo_"+sequence.Name+".Instance) { }\n");
        }

        private void GenerateInternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence, Sequence seq)
        {
            source.AppendFront("public static bool ApplyXGRS_" + sequence.Name + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
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

            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            source.AppendFront("GRGEN_LGSP.LGSPActions actions = procEnv.curActions;\n");

            neededEntitiesEmitter.Reset();

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugEntering(\"{0}\"", sequence.Name);
                for(int i = 0; i < sequence.Parameters.Length; ++i)
                {
                    source.Append(", var_");
                    source.Append(sequence.Parameters[i]);
                }
                source.Append(");\n");
            }

            neededEntitiesEmitter.EmitNeededVarAndRuleEntities(seq, source);

            EmitSequence(seq, source);

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugExiting(\"{0}\"", sequence.Name);
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.Append(", var_");
                    source.Append(sequence.OutParameters[i]);
                }
                source.Append(");\n");
            }

            source.AppendFront("return " + compGen.GetResultVar(seq) + ";\n");
            source.Unindent();
            source.AppendFront("}\n");

            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            seq.GetLocalVariables(variables, containerConstructors, null);
            foreach(SequenceExpressionContainerConstructor cc in containerConstructors)
            {
                GenerateContainerConstructor(cc, source);
            }
        }

        private void GenerateInternalDefinedSequenceApplicationMethodStub(SourceBuilder source, DefinedSequenceInfo sequence, String externalActionsExtensionFilename)
        {
            source.AppendFrontFormat("// You must implement the following function in the same partial class in ./{0}\n", externalActionsExtensionFilename);

            source.AppendFront("//public static bool ApplyXGRS_" + sequence.Name + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
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
        }

        private void GenerateExactExternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            source.AppendFront("public static bool Apply_" + sequence.Name + "(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv");
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
                source.Append(" = " + TypesHelper.DefaultValueString(typeName, model) + ";\n");
            }
            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv");
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
            source.AppendFront("public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");

            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.Parameters[i]);
                source.Append(" = (" + typeName + ")sequenceInvocation.ArgumentExpressions[" + i + "].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);\n");
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.OutParameters[i]);
                source.Append(" = " + TypesHelper.DefaultValueString(typeName, model) + ";\n");
            }

            source.AppendFront("if(sequenceInvocation.Subgraph!=null)\n");
            source.AppendFront("\t{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }\n");

            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", var_" + sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref var_" + sequence.OutParameters[i]);
            }
            source.Append(");\n");

            source.AppendFront("if(sequenceInvocation.Subgraph!=null)\n");
            source.AppendFront("\t{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }\n");

            if(sequence.OutParameters.Length > 0)
            {
                source.AppendFront("if(result) {\n");
                source.Indent();
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.AppendFront("sequenceInvocation.ReturnVars[" + i + "].SetVariableValue(var_" + sequence.OutParameters[i] + ", procEnv);\n");
                }
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("return result;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        public void GenerateFilterStubs(SourceBuilder source, LGSPRulePattern rulePattern)
        {
            String rulePatternClassName = rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";

            foreach(IFilter filter in rulePattern.Filters)
            {
                if(filter is IFilterAutoGenerated)
                    continue;

                IFilterFunction filterFunction = (IFilterFunction)filter;
                if(!filterFunction.IsExternal)
                    continue;

                if(filter.Package != null)
                {
                    source.AppendFrontFormat("namespace {0}\n", filter.Package);
                    source.AppendFront("{\n");
                    source.Indent();
                }
                source.AppendFront("public partial class MatchFilters\n");
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFrontFormat("//public static void Filter_{0}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {1} matches", filter.Name, matchesListType);
                for(int i = 0; i < filterFunction.Inputs.Length; ++i)
                {
                    source.AppendFormat(", {0} {1}", TypesHelper.TypeName(filterFunction.Inputs[i]), filterFunction.InputNames[i]);
                }
                source.Append(")\n");

                source.Unindent();
                source.AppendFront("}\n");
                if(filter.Package != null)
                {
                    source.Unindent();
                    source.AppendFront("}\n");
                }
            }
        }

        public void GenerateFilters(SourceBuilder source, LGSPRulePattern rulePattern)
        {
            foreach(IFilter f in rulePattern.Filters)
            {
                if(f is IFilterAutoGenerated)
                {
                    IFilterAutoGenerated filter = (IFilterAutoGenerated)f;

                    if(filter.Package != null)
                    {
                        source.AppendFrontFormat("namespace {0}\n", filter.Package);
                        source.AppendFront("{\n");
                        source.Indent();
                    }
                    source.AppendFront("public partial class MatchFilters\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(filter.Name == "auto")
                        GenerateAutomorphyFilter(source, rulePattern);
                    else
                    {
                        if(filter.Name == "orderAscendingBy")
                            GenerateOrderByFilter(source, rulePattern, (LGSPFilterAutoGenerated)filter, true);
                        if(filter.Name == "orderDescendingBy")
                            GenerateOrderByFilter(source, rulePattern, (LGSPFilterAutoGenerated)filter, false);
                        if(filter.Name == "groupBy")
                            GenerateGroupByFilter(source, rulePattern, filter.Entities[0]);
                        if(filter.Name == "keepSameAsFirst")
                            GenerateKeepSameFilter(source, rulePattern, filter.Entities[0], true);
                        if(filter.Name == "keepSameAsLast")
                            GenerateKeepSameFilter(source, rulePattern, filter.Entities[0], false);
                        if(filter.Name == "keepOneForEach")
                            GenerateKeepOneForEachFilter(source, rulePattern, filter.Entities[0]);
                    }

                    source.Unindent();
                    source.AppendFront("}\n");
                    if(filter.Package != null)
                    {
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                }
            }
        }

        private static void GenerateAutomorphyFilter(SourceBuilder source, LGSPRulePattern rulePattern)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchClassName = rulePatternClassName + "." + NamesOfEntities.MatchClassName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = "auto";
            
            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n", 
                rulePattern.name, filterName, matchesListType);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront("if(matches.Count < 2)\n");
            source.AppendFront("\treturn;\n");
            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);

            source.AppendFrontFormat("if(matches.Count < 5 || {0}.Instance.patternGraph.nodes.Length + {0}.Instance.patternGraph.edges.Length < 1)\n", rulePatternClassName);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront("for(int i = 0; i < matchesArray.Count; ++i)\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("if(matchesArray[i] == null)\n");
            source.AppendFront("\tcontinue;\n");
            source.AppendFront("for(int j = i + 1; j < matchesArray.Count; ++j)\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("if(matchesArray[j] == null)\n");
            source.AppendFront("\tcontinue;\n");
            source.AppendFront("if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(matchesArray[i], matchesArray[j], procEnv.graph))\n");
            source.AppendFront("\tmatchesArray[j] = null;\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("Dictionary<int, {0}> foundMatchesOfSameMainPatternHash = new Dictionary<int, {0}>();\n", 
                matchClassName);
            source.AppendFront("for(int i = 0; i < matchesArray.Count; ++i)\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} match = ({0})matchesArray[i];\n", matchClassName);
            source.AppendFront("int duplicateMatchHash = 0;\n");
            source.AppendFront("for(int j = 0; j < match.NumberOfNodes; ++j) duplicateMatchHash ^= match.getNodeAt(j).GetHashCode();\n");
            source.AppendFront("for(int j = 0; j < match.NumberOfEdges; ++j) duplicateMatchHash ^= match.getEdgeAt(j).GetHashCode();\n");
            source.AppendFront("bool contained = foundMatchesOfSameMainPatternHash.ContainsKey(duplicateMatchHash);\n");
            source.AppendFront("if(contained)\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];\n", matchClassName);
            source.AppendFront("do\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("if(GRGEN_LIBGR.SymmetryChecker.AreSymmetric(match, duplicateMatchCandidate, procEnv.graph))\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("matchesArray[i] = null;\n");
            source.AppendFrontFormat("goto label_auto_{0};\n", rulePatternClassName.Replace('.', '_'));
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("while((duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash) != null);\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("if(!contained)\n");
            source.AppendFront("\tfoundMatchesOfSameMainPatternHash[duplicateMatchHash] = match;\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} duplicateMatchCandidate = foundMatchesOfSameMainPatternHash[duplicateMatchHash];\n", matchClassName);
            source.AppendFront("while(duplicateMatchCandidate.nextWithSameHash != null) duplicateMatchCandidate = duplicateMatchCandidate.nextWithSameHash;\n");
            source.AppendFront("duplicateMatchCandidate.nextWithSameHash = match;\n");
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFormat("label_auto_{0}: ;\n", rulePatternClassName.Replace('.', '_'));
            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFrontFormat("foreach({0} toClean in foundMatchesOfSameMainPatternHash.Values) toClean.CleanNextWithSameHash();\n", matchClassName);
            
            source.Unindent();
            source.AppendFront("}\n");

            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private static void GenerateOrderByFilter(SourceBuilder source, LGSPRulePattern rulePattern, LGSPFilterAutoGenerated filter, bool ascending)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = ascending ? "orderAscendingBy_" + filter.EntitySuffixForName : "orderDescendingBy_" + filter.EntitySuffixForName;

            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n", 
                rulePattern.name, filterName, matchesListType);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);
            source.AppendFrontFormat("matchesArray.Sort(new Comparer_{0}_{1}());\n", rulePattern.name, filterName);
            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.AppendFrontFormat("class Comparer_{0}_{1} : Comparer<{2}>\n", rulePattern.name, filterName, matchInterfaceName);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("public override int Compare({0} left, {0} right)\n", matchInterfaceName);
            source.AppendFront("{\n");
            source.Indent();
            GenerateOrderByFilter(source, filter.Entities, ascending, false, 0);
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private static void GenerateOrderByFilter(SourceBuilder source, List<String> filterVariables, bool ascending, bool leaf, int pos)
        {
            String filterVariable = filterVariables[pos];
            if(pos + 1 < filterVariables.Count && !leaf)
            {
                source.AppendFrontFormat("if(left.{0}.CompareTo(right.{0})==0)", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                source.Indent();
                GenerateOrderByFilter(source, filterVariables, ascending, false, pos + 1);
                source.Unindent();
                source.AppendFrontFormat("else");
                source.Indent();
                GenerateOrderByFilter(source, filterVariables, ascending, true, pos);
                source.Unindent();
            }
            else
            {
                if(ascending)
                    source.AppendFrontFormat("return left.{0}.CompareTo(right.{0});\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                else
                    source.AppendFrontFormat("return -left.{0}.CompareTo(right.{0});\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
            }
        }

        private void GenerateGroupByFilter(SourceBuilder source, LGSPRulePattern rulePattern, String filterVariable)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = "groupBy_" + filterVariable;

            if(true) // does the type of the variable to group-by support ordering? then order, is more efficient than equality comparisons
            {
                source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n",
                    rulePattern.name, filterName, matchesListType);
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);
                source.AppendFrontFormat("matchesArray.Sort(new Comparer_{0}_{1}());\n", rulePattern.name, filterName);
                source.AppendFront("matches.FromList();\n");

                source.Unindent();
                source.AppendFront("}\n");

                source.AppendFrontFormat("class Comparer_{0}_{1} : Comparer<{2}>\n", rulePattern.name, filterName, matchInterfaceName);
                source.AppendFront("{\n");
                source.Indent();

                source.AppendFrontFormat("public override int Compare({0} left, {0} right)\n", matchInterfaceName);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("return left.{0}.CompareTo(right.{0});\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                source.Unindent();
                source.AppendFront("}\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
            else
            {
                // ensure that if two elements are equal, they are neighbours or only separated by equal elements
                // not needed yet, as of now we only support numerical types and string, that support ordering in addition to equality comparison
                /*List<T> matchesArray;
                for(int i = 0; i < matchesArray.Count - 1; ++i)
                {
                    for(int j = i + 1; j < matchesArray.Count; ++j)
                    {
                        if(matchesArray[i] == matchesArray[j])
                        {
                            T tmp = matchesArray[i + 1];
                            matchesArray[i + 1] = matchesArray[j];
                            matchesArray[j] = tmp;
                            break;
                        }
                    }
                }*/
            }
        }

        private void GenerateKeepSameFilter(SourceBuilder source, LGSPRulePattern rulePattern, String filterVariable, bool sameAsFirst)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = sameAsFirst ? "keepSameAsFirst_" + filterVariable : "keepSameAsLast_" + filterVariable;

            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n", 
                rulePattern.name, filterName, matchesListType);

            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);

            if(sameAsFirst)
            {
                source.AppendFront("int pos = 0 + 1;\n");
                source.AppendFrontFormat("while(pos < matchesArray.Count && matchesArray[pos].{0} == matchesArray[0].{0})\n", 
                    NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                source.AppendFront("{\n");
                source.AppendFront("\t++pos;\n");
                source.AppendFront("}\n");
                source.AppendFront("for(; pos < matchesArray.Count; ++pos)\n");
                source.AppendFront("{\n");
                source.AppendFront("\tmatchesArray[pos] = null;\n");
                source.AppendFront("}\n");
            }
            else
            {
                source.AppendFront("int pos = matchesArray.Count-1 - 1;\n");
                source.AppendFrontFormat("while(pos >= 0 && matchesArray[pos].{0} == matchesArray[matchesArray.Count-1].{0})\n",
                    NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
                source.AppendFront("{\n");
                source.AppendFront("\t--pos;\n");
                source.AppendFront("}\n");
                source.AppendFront("for(; pos >= 0; --pos)\n");
                source.AppendFront("{\n");
                source.AppendFront("\tmatchesArray[pos] = null;\n");
                source.AppendFront("}\n");
            }

            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateKeepOneForEachFilter(SourceBuilder source, LGSPRulePattern rulePattern, String filterVariable)
        {
            String rulePatternClassName = TypesHelper.GetPackagePrefixDot(rulePattern.PatternGraph.Package) + rulePattern.GetType().Name;
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(rulePattern.name);
            String matchesListType = "GRGEN_LIBGR.IMatchesExact<" + matchInterfaceName + ">";
            String filterName = "keepOneForEach_" + filterVariable;

            source.AppendFrontFormat("public static void Filter_{0}_{1}(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, {2} matches)\n",
                 rulePattern.name, filterName, matchesListType);
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFrontFormat("List<{0}> matchesArray = matches.ToList();\n", matchInterfaceName);

            source.AppendFront("int cmpPos = 0;\n");
            source.AppendFront("int pos = 0 + 1;\n");
            source.AppendFront("for(; pos < matchesArray.Count; ++pos)\n");
            source.AppendFront("{\n");
            source.AppendFrontFormat("\tif(matchesArray[pos].{0} == matchesArray[cmpPos].{0})\n", NamesOfEntities.MatchName(filterVariable, EntityType.Variable));
            source.AppendFront("\t\tmatchesArray[pos] = null;\n");
            source.AppendFront("\telse\n");
            source.AppendFront("\t\tcmpPos = pos;\n");
            source.AppendFront("}\n");

            source.AppendFront("matches.FromList();\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateContainerConstructor(SequenceExpressionContainerConstructor cc, SourceBuilder source)
        {
            string containerType = TypesHelper.XgrsTypeToCSharpType(GetContainerType(cc), model);
            string valueType = TypesHelper.XgrsTypeToCSharpType(cc.ValueType, model);
            string keyType = null;
            if(cc is SequenceExpressionMapConstructor)
                keyType = TypesHelper.XgrsTypeToCSharpType(((SequenceExpressionMapConstructor)cc).KeyType, model);

            source.Append("\n");
            source.AppendFront("public static ");
            source.Append(containerType);
            source.Append(" fillFromSequence_" + cc.Id);
            source.Append("(");
            for(int i = 0; i < cc.ContainerItems.Length; ++i)
            {
                if(i > 0)
                    source.Append(", ");
                if(keyType != null)
                    source.AppendFormat("{0} paramkey{1}, ", keyType, i);
                source.AppendFormat("{0} param{1}", valueType, i);
            }
            source.Append(")\n");
            
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} container = new {0}();\n", containerType);
            for(int i = 0; i < cc.ContainerItems.Length; ++i)
            {
                if(cc is SequenceExpressionSetConstructor)
                    source.AppendFrontFormat("container.Add(param{0}, null);\n", i);
                else if(cc is SequenceExpressionMapConstructor)
                    source.AppendFrontFormat("container.Add(paramkey{0}, param{0});\n", i);
                else if(cc is SequenceExpressionArrayConstructor)
                    source.AppendFrontFormat("container.Add(param{0});\n", i);
                else //if(cc is SequenceExpressionDequeConstructor)
                    source.AppendFrontFormat("container.Enqueue(param{0});\n", i);
            }
            source.AppendFront("return container;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private static string GetContainerType(SequenceExpressionContainerConstructor cc)
        {
            if(cc is SequenceExpressionSetConstructor)
                return "set<" + cc.ValueType + ">";
            else if(cc is SequenceExpressionMapConstructor)
                return "map<" + ((SequenceExpressionMapConstructor)cc).KeyType + "," + cc.ValueType + ">";
            else if(cc is SequenceExpressionArrayConstructor)
                return "array<" + cc.ValueType + ">";
            else //if(cc is SequenceExpressionDequeConstructor)
                return "deque<" + cc.ValueType + ">";
        }

        private void HandleSequenceParserException(SequenceParserException ex)
        {
            if(ex.Name == null 
                && ex.Kind != SequenceParserError.TypeMismatch
                && ex.Kind != SequenceParserError.FilterError
                && ex.Kind != SequenceParserError.FilterParameterError
                && ex.Kind != SequenceParserError.OperatorNotFound)
            {
                Console.Error.WriteLine("Unknown " + ex.DefinitionTypeName + ": \"{0}\"", ex.Name);
                return;
            }

            switch(ex.Kind)
            {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if(helper.actionsTypeInformation.InputTypes(ex.Name).Count != ex.NumGivenInputs && helper.actionsTypeInformation.OutputTypes(ex.Name).Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of parameters and return values for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else if(helper.actionsTypeInformation.InputTypes(ex.Name).Count != ex.NumGivenInputs)
                        Console.Error.WriteLine("Wrong number of parameters for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else if(helper.actionsTypeInformation.OutputTypes(ex.Name).Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of return values for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else
                        goto default;
                    break;

                case SequenceParserError.BadParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". parameter is not valid for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.BadReturnParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". return parameter is not valid for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.FilterError:
                    Console.Error.WriteLine("Can't apply filter " + ex.FilterName + " to rule " + ex.Name + "!");
                    return;

                case SequenceParserError.FilterParameterError:
                    Console.Error.WriteLine("Filter parameter mismatch for filter \"" + ex.FilterName + "\" applied to \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.OperatorNotFound:
                    Console.Error.WriteLine("Operator not found/arguments not of correct type: " + ex.Expression);
                    return;

                case SequenceParserError.RuleNameUsedByVariable:
                    Console.Error.WriteLine("The name of the variable conflicts with the name of action/sequence \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    Console.Error.WriteLine("The variable \"" + ex.Name + "\" may neither receive parameters nor return values!");
                    return;

                case SequenceParserError.UnknownAttribute:
                    Console.WriteLine("Unknown attribute \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.UnknownProcedure:
                    Console.WriteLine("Unknown procedure \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.UnknownFunction:
                    Console.WriteLine("Unknown function \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.TypeMismatch:
                    Console.Error.WriteLine("The construct \"" + ex.VariableOrFunctionName + "\" expects:" + ex.ExpectedType + " but is / is given " + ex.GivenType + "!");
                    return;

                default:
                    throw new ArgumentException("Invalid error kind: " + ex.Kind);
            }

            if(helper.actionsTypeInformation.rulesToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of rule/test: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.rulesToInputTypes[ex.Name]);
                PrintOutputParams(helper.actionsTypeInformation.rulesToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(helper.actionsTypeInformation.sequencesToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of sequence: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.sequencesToInputTypes[ex.Name]);
                PrintOutputParams(helper.actionsTypeInformation.sequencesToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(helper.actionsTypeInformation.proceduresToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature procedure: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.proceduresToInputTypes[ex.Name]);
                PrintOutputParams(helper.actionsTypeInformation.proceduresToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(helper.actionsTypeInformation.functionsToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of function: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.functionsToInputTypes[ex.Name]);
                Console.Error.Write(" : ");
                Console.Error.Write(helper.actionsTypeInformation.functionsToOutputType[ex.Name]);
                Console.Error.WriteLine();
            }
        }

        private void PrintInputParams(List<String> nameToInputTypes)
        {
            if(nameToInputTypes.Count != 0)
            {
                Console.Error.Write("(");
                bool first = true;
                foreach(String typeName in nameToInputTypes)
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
        }

        private void PrintOutputParams(List<String> nameToOutputTypes)
        {
            if(nameToOutputTypes.Count != 0)
            {
                Console.Error.Write(" : (");
                bool first = true;
                foreach(String typeName in nameToOutputTypes)
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
        }
    }
}
