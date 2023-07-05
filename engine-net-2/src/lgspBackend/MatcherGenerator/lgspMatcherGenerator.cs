/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

#define MONO_MULTIDIMARRAY_WORKAROUND       // must be equally set to the same flag in lgspGraphStatistics.cs!
//#define RANDOM_LOOKUP_LIST_START      // currently broken
//#define DUMP_SCHEDULED_SEARCH_PLAN
//#define DUMP_SEARCHPROGRAMS

using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.expression;
using System.IO;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;


namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Class generating matcher programs out of rules.
    /// A PatternGraphAnalyzer must run before the matcher generator is used,
    /// so that the analysis data is written the pattern graphs of the matching patterns to generate code for.
    /// </summary>
    public class LGSPMatcherGenerator
    {
        /// <summary>
        /// The model for which the matcher functions shall be generated.
        /// </summary>
        private readonly IGraphModel model;

        /// <summary>
        /// If true, the generated matcher functions are commented to improve understanding the source code.
        /// </summary>
        public bool CommentSourceCode = false;

        /// <summary>
        /// If true, validity checks are added at some places, to support debugging/developments.
        /// </summary>
        public bool EmitDebugValidityChecks = false;

        /// <summary>
        /// If true, the source code of dynamically generated matcher functions are dumped to a file in the current directory.
        /// </summary>
        public bool DumpDynSourceCode = false;

        /// <summary>
        /// If true, generated search plans are dumped in VCG and TXT files in the current directory.
        /// </summary>
        public bool DumpSearchPlan = false;

        /// <summary>
        /// If true, the negatives, independents, and evaluations are inserted at the end of the schedule
        /// instead of as early as possible; this is likely less efficient but allows to use checks 
        /// which require that they are only called after a structural match was found
        /// </summary>
        public bool LazyNegativeIndependentConditionEvaluation = false;

        /// <summary>
        /// If true, the independents are to be inlined
        /// </summary>
        public bool InlineIndependents = true;

        /// <summary>
        /// If true, profiling information is to be collected, i.e. some statistics about search steps executed
        /// </summary>
        public bool Profile = false;

        /// <summary>
        /// Instantiates a new instance of LGSPMatcherGenerator with the given graph model.
        /// A PatternGraphAnalyzer must run before the matcher generator is used,
        /// so that the analysis data is written the pattern graphs of the matching patterns to generate code for.
        /// </summary>
        /// <param name="model">The model for which the matcher functions shall be generated.</param>
        public LGSPMatcherGenerator(IGraphModel model)
        {
            this.model = model;
        }

        public IGraphModel GetModel()
        {
            return model;
        }

        public static void SetNeedForProfiling(ExpressionOrYielding expyield)
        {
            expyield.SetNeedForProfiling(true);
            foreach(ExpressionOrYielding child in expyield)
            {
                SetNeedForProfiling(child);
            }
        }

        /// <summary>
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the given rule pattern into the given source builder
        /// </summary>
        public void GenerateActionAndMatcher(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, bool isInitialStatic)
        {
            // generate the search program out of the schedule(s) within the pattern graph of the rule
            SearchProgram searchProgram = GenerateSearchProgram(matchingPattern);

            // generate the parallelized search program out of the parallelized schedule(s) within the pattern graph of the rule
            SearchProgram searchProgramParallelized = GenerateParallelizedSearchProgramAsNeeded(matchingPattern);

            // emit matcher class head, body, tail; body is source code representing search program
            if(matchingPattern is LGSPRulePattern)
            {
                GenerateActionInterface(sb, (LGSPRulePattern)matchingPattern); // generate the exact action interface
                GenerateMatcherClassHeadAction(sb, (LGSPRulePattern)matchingPattern, isInitialStatic, searchProgram);
                searchProgram.Emit(sb);
                if(searchProgramParallelized != null)
                    searchProgramParallelized.Emit(sb);
                GenerateActionImplementation(sb, (LGSPRulePattern)matchingPattern);
                sb.Append(searchProgram.ArrayPerElementMethods);
                if(searchProgramParallelized != null)
                {
                    sb.Append(searchProgramParallelized.ArrayPerElementMethods);
                    sb.Append(((SearchProgram)searchProgramParallelized.Next).ArrayPerElementMethods);
                }
                GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);
            }
            else
            {
                GenerateMatcherClassHeadSubpattern(sb, matchingPattern, isInitialStatic);
                searchProgram.Emit(sb);
                if(searchProgramParallelized != null)
                    searchProgramParallelized.Emit(sb);
                sb.Append(searchProgram.ArrayPerElementMethods);
                if(searchProgramParallelized != null)
                    sb.Append(searchProgramParallelized.ArrayPerElementMethods);
                GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);
            }

            // finally generate matcher source for all the nested alternatives or iterateds of the pattern graph
            // nested inside the alternatives,iterateds,negatives,independents
            foreach(Alternative alt in matchingPattern.patternGraph.alternativesPlusInlined)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach(Iterated iter in matchingPattern.patternGraph.iteratedsPlusInlined)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
            }
            foreach(PatternGraph neg in matchingPattern.patternGraph.negativePatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
            }
            foreach(PatternGraph idpt in matchingPattern.patternGraph.independentPatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, idpt, isInitialStatic);
            }
        }

        /// <summary>
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the given alternative into the given source builder
        /// </summary>
        public void GenerateActionAndMatcherOfAlternative(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, Alternative alt, bool isInitialStatic)
        {
            // generate the search program out of the schedules within the pattern graphs of the alternative cases
            SearchProgram searchProgram = GenerateSearchProgramAlternative(matchingPattern, alt);

            // generate the parallelized search program out of the parallelized schedules within the pattern graphs of the alternative cases
            SearchProgram searchProgramParallelized = GenerateParallelizedSearchProgramAlternativeAsNeeded(matchingPattern, alt);

            // emit matcher class head, body, tail; body is source code representing search program
            GenerateMatcherClassHeadAlternative(sb, matchingPattern, alt, isInitialStatic);
            searchProgram.Emit(sb);
            if(searchProgramParallelized != null)
                searchProgramParallelized.Emit(sb);
            sb.Append(searchProgram.ArrayPerElementMethods);
            if(searchProgramParallelized != null)
                sb.Append(searchProgramParallelized.ArrayPerElementMethods);
            GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);

            // handle alternatives or iterateds nested in the alternative cases
            foreach(PatternGraph altCase in alt.alternativeCases)
            {
                // nested inside the alternatives,iterateds,negatives,independents
                foreach(Alternative nestedAlt in altCase.alternativesPlusInlined)
                {
                    GenerateActionAndMatcherOfAlternative(sb, matchingPattern, nestedAlt, isInitialStatic);
                }
                foreach(Iterated iter in altCase.iteratedsPlusInlined)
                {
                    GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
                }
                foreach(PatternGraph neg in altCase.negativePatternGraphsPlusInlined)
                {
                    GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
                }
                foreach(PatternGraph idpt in altCase.independentPatternGraphsPlusInlined)
                {
                    GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, idpt, isInitialStatic);
                }
            }
        }

        /// <summary>
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the given iterated pattern into the given source builder
        /// </summary>
        public void GenerateActionAndMatcherOfIterated(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, PatternGraph iter, bool isInitialStatic)
        {
            // generate the search program out of the schedule within the pattern graph of the iterated pattern
            SearchProgram searchProgram = GenerateSearchProgramIterated(matchingPattern, iter);

            // generate the parallelized search program out of the parallelized schedule within the pattern graph of the iterated pattern
            SearchProgram searchProgramParallelized = GenerateParallelizedSearchProgramIteratedAsNeeded(matchingPattern, iter);

            // emit matcher class head, body, tail; body is source code representing search program
            GenerateMatcherClassHeadIterated(sb, matchingPattern, iter, isInitialStatic);
            searchProgram.Emit(sb);
            if(searchProgramParallelized != null)
                searchProgramParallelized.Emit(sb);
            sb.Append(searchProgram.ArrayPerElementMethods);
            if(searchProgramParallelized != null)
                sb.Append(searchProgramParallelized.ArrayPerElementMethods);
            GenerateMatcherClassTail(sb, matchingPattern.PatternGraph.Package != null);

            // finally generate matcher source for all the nested alternatives or iterateds of the iterated pattern graph
            // nested inside the alternatives,iterateds,negatives,independents
            foreach(Alternative alt in iter.alternativesPlusInlined)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach(Iterated nestedIter in iter.iteratedsPlusInlined)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, nestedIter.iteratedPattern, isInitialStatic);
            }
            foreach(PatternGraph neg in iter.negativePatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, neg, isInitialStatic);
            }
            foreach(PatternGraph idpt in iter.independentPatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, idpt, isInitialStatic);
            }
        }

        /// <summary>
        /// Generates the action interface plus action implementation including the matcher source code 
        /// for the alternatives/iterateds nested within the given negative/independent pattern graph into the given source builder
        /// </summary>
        public void GenerateActionAndMatcherOfNestedPatterns(SourceBuilder sb,
            LGSPMatchingPattern matchingPattern, PatternGraph negOrIdpt, bool isInitialStatic)
        {
            // nothing to do locally ..

            // .. just move on to the nested alternatives or iterateds
            foreach(Alternative alt in negOrIdpt.alternativesPlusInlined)
            {
                GenerateActionAndMatcherOfAlternative(sb, matchingPattern, alt, isInitialStatic);
            }
            foreach(Iterated iter in negOrIdpt.iteratedsPlusInlined)
            {
                GenerateActionAndMatcherOfIterated(sb, matchingPattern, iter.iteratedPattern, isInitialStatic);
            }
            foreach(PatternGraph nestedNeg in negOrIdpt.negativePatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, nestedNeg, isInitialStatic);
            }
            foreach(PatternGraph nestedIdpt in negOrIdpt.independentPatternGraphsPlusInlined)
            {
                GenerateActionAndMatcherOfNestedPatterns(sb, matchingPattern, nestedIdpt, isInitialStatic);
            }
        }

        /// <summary>
        /// generate the exact action interface
        /// </summary>
        void GenerateActionInterface(SourceBuilder sb, LGSPRulePattern matchingPattern)
        {
            String actionInterfaceName = "IAction_"+matchingPattern.name;
            String outParameters = "";
            String refParameters = "";
            String allParameters = "";
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                outParameters += ", out " + TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_"+i;
                refParameters += ", ref " + TypesHelper.TypeName(matchingPattern.Outputs[i]) + " output_"+i;
                allParameters += ", List<" + TypesHelper.TypeName(matchingPattern.Outputs[i]) + "> output_"+i;
            }
 
            String inParameters = "";
            for(int i=0; i<matchingPattern.Inputs.Length; ++i)
            {
                inParameters += ", " + TypesHelper.TypeName(matchingPattern.Inputs[i]) + " " + matchingPattern.InputNames[i];
            }
            String matchingPatternClassName = matchingPattern.GetType().Name;
            String patternName = matchingPattern.name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">" ;

            if(matchingPattern.PatternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", matchingPattern.PatternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("/// <summary>\n");
            sb.AppendFront("/// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.\n");
            sb.AppendFront("/// </summary>\n");
            sb.AppendFrontFormat("public interface {0}\n", actionInterfaceName);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("/// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("{0} Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches{1});\n", matchesType, inParameters);

            sb.AppendFront("/// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} match{1});\n", matchType, outParameters);

            sb.AppendFront("/// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} matches{1});\n", matchesType, allParameters);

            sb.AppendFront("/// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>\n");
            sb.AppendFrontFormat("bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1});\n", inParameters, refParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>\n");
            sb.AppendFrontFormat("int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1});\n", inParameters, allParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0});\n", inParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0});\n", inParameters);

            sb.AppendFront("/// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>\n");
            sb.AppendFrontFormat("bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max{0});\n", inParameters);
            sb.Unindent();
            sb.AppendFront("}\n");
            
            if(matchingPattern.PatternGraph.Package != null)
            {
                sb.Unindent();
                sb.AppendFront("}\n");
            }

            sb.AppendFront("\n");
        }

        /// <summary>
        /// generate implementation of the exact action interface,
        /// delegate calls of the inexact action interface IAction to the exact action interface
        /// </summary>
        void GenerateActionImplementation(SourceBuilder sb, LGSPRulePattern matchingPattern)
        {
            StringBuilder sbInParameters = new StringBuilder();
            StringBuilder sbInArguments = new StringBuilder();
            StringBuilder sbInArgumentsFromArray = new StringBuilder();
            for(int i = 0; i < matchingPattern.Inputs.Length; ++i)
            {
                sbInParameters.Append(", ");
                sbInParameters.Append(TypesHelper.TypeName(matchingPattern.Inputs[i]));
                sbInParameters.Append(" ");
                sbInParameters.Append(matchingPattern.InputNames[i]);

                sbInArguments.Append(", ");
                sbInArguments.Append(matchingPattern.InputNames[i]);

                String parameterType = TypesHelper.DotNetTypeToXgrsType(matchingPattern.Inputs[i]);
                if(parameterType.StartsWith("array<match<"))
                {
                    String arrayValueType = TypesHelper.ExtractSrc(parameterType);
                    if(parameterType.StartsWith("array<match<class "))
                        sbInArgumentsFromArray.AppendFormat(", {0}.ConvertAsNeeded(", TypesHelper.MatchClassInfoForMatchClassType(arrayValueType));
                    else
                        sbInArgumentsFromArray.AppendFormat(", {0}.ConvertAsNeeded(", TypesHelper.RuleClassForMatchType(arrayValueType));
                    sbInArgumentsFromArray.Append("parameters[");
                    sbInArgumentsFromArray.Append(i);
                    sbInArgumentsFromArray.Append("])");
                }
                else if(parameterType.StartsWith("array<"))
                {
                    String arrayValueType = TypesHelper.ExtractSrc(parameterType);
                    sbInArgumentsFromArray.AppendFormat(", GRGEN_LIBGR.ContainerHelper.ConvertIfEmpty<{0}>(", TypesHelper.XgrsTypeToCSharpType(arrayValueType, model));
                    sbInArgumentsFromArray.Append("parameters[");
                    sbInArgumentsFromArray.Append(i);
                    sbInArgumentsFromArray.Append("])");
                }
                else
                {
                    sbInArgumentsFromArray.Append(", (");
                    sbInArgumentsFromArray.Append(TypesHelper.TypeName(matchingPattern.Inputs[i]));
                    sbInArgumentsFromArray.Append(") parameters[");
                    sbInArgumentsFromArray.Append(i);
                    sbInArgumentsFromArray.Append("]");
                }
            }
            String inParameters = sbInParameters.ToString();
            String inArguments = sbInArguments.ToString();
            String inArgumentsFromArray = sbInArgumentsFromArray.ToString();

            StringBuilder sbOutParameters = new StringBuilder();
            StringBuilder sbRefParameters = new StringBuilder();
            StringBuilder sbOutLocals = new StringBuilder();
            StringBuilder sbRefLocals = new StringBuilder();
            StringBuilder sbOutArguments = new StringBuilder();
            StringBuilder sbRefArguments = new StringBuilder();
            StringBuilder sbOutIntermediateLocalsAll = new StringBuilder();
            StringBuilder sbOutParametersAll = new StringBuilder();
            StringBuilder sbOutArgumentsAll = new StringBuilder();
            StringBuilder sbIntermediateLocalArgumentsAll = new StringBuilder();
            StringBuilder sbOutClassArgumentsAll = new StringBuilder();
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sbOutParameters.Append(", out ");
                sbOutParameters.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sbOutParameters.Append(" output_");
                sbOutParameters.Append(i);

                sbRefParameters.Append(", ref ");
                sbRefParameters.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sbRefParameters.Append(" output_");
                sbRefParameters.Append(i);

                sbOutLocals.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sbOutLocals.Append(" output_");
                sbOutLocals.Append(i);
                sbOutLocals.Append("; ");

                sbRefLocals.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sbRefLocals.Append(" output_");
                sbRefLocals.Append(i);
                sbRefLocals.Append(" = ");
                sbRefLocals.Append(TypesHelper.DefaultValueString(matchingPattern.Outputs[i].PackagePrefixedName, model));
                sbRefLocals.Append("; ");

                sbOutArguments.Append(", out output_");
                sbOutArguments.Append(i);

                sbRefArguments.Append(", ref output_");
                sbRefArguments.Append(i);

                sbOutIntermediateLocalsAll.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sbOutIntermediateLocalsAll.Append(" output_local_");
                sbOutIntermediateLocalsAll.Append(i);
                sbOutIntermediateLocalsAll.Append("; ");

                sbOutParametersAll.Append(", List<");
                sbOutParametersAll.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sbOutParametersAll.Append("> output_");
                sbOutParametersAll.Append(i);

                sbOutArgumentsAll.Append(", output_");
                sbOutArgumentsAll.Append(i);

                sbIntermediateLocalArgumentsAll.Append(", out output_local_");
                sbIntermediateLocalArgumentsAll.Append(i);

                sbOutClassArgumentsAll.Append(", output_list_");
                sbOutClassArgumentsAll.Append(i);
            }
            String outParameters = sbOutParameters.ToString();
            String refParameters = sbRefParameters.ToString();
            String outLocals = sbOutLocals.ToString();
            String refLocals = sbRefLocals.ToString();
            String outArguments = sbOutArguments.ToString();
            String refArguments = sbRefArguments.ToString();
            String outIntermediateLocalsAll = sbOutIntermediateLocalsAll.ToString();
            String outParametersAll = sbOutParametersAll.ToString();
            String outArgumentsAll = sbOutArgumentsAll.ToString();
            String intermediateLocalArgumentsAll = sbIntermediateLocalArgumentsAll.ToString();
            String outClassArgumentsAll = sbOutClassArgumentsAll.ToString();
 
            String matchingPatternClassName = matchingPattern.GetType().Name;
            String patternName = matchingPattern.name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";

            // implementation of exact action interface

            sb.AppendFront("/// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>\n");
            sb.AppendFrontFormat("public delegate {0} MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches{1});\n", matchesType, inParameters);

            sb.AppendFront("/// <summary> A delegate pointing to the current matcher program for this rule. </summary>\n");
            sb.AppendFront("public MatchInvoker DynamicMatch;\n");

            sb.AppendFront("/// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>\n");
            sb.AppendFront("public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }\n");

            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFront("List<");
                sb.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sb.Append("> output_list_");
                sb.Append(i.ToString());
                sb.Append(" = new List<");
                sb.Append(TypesHelper.TypeName(matchingPattern.Outputs[i]));
                sb.Append(">();\n");
            }

            sb.AppendFrontFormat("public {0} Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches{1})\n", matchesType, inParameters);
            sb.AppendFront("{\n");
            sb.Indent();

            if(EmitDebugValidityChecks)
            {
                // throw a source-level early exception instead of an internal NPE in case a parameter is null (that is not explicitly allowed to be null)
                for(int i = 0; i < matchingPattern.Inputs.Length; ++i)
                {
                    String inputName = matchingPattern.InputNames[i];
                    if(Array.IndexOf(matchingPattern.patternGraph.maybeNullElementNames, inputName) != -1)
                        continue;
                    GrGenType inputType = matchingPattern.Inputs[i];
                    if(inputType is NodeType || inputType is EdgeType
                        /*|| inputType is VarType && (inputType as VarType).Type.IsGenericType*/)
                    {
                        sb.AppendFrontFormat("if({0}==null) throw new ArgumentNullException(\"{0}\", \"The graph element handed in is null. Common causes are uninitialized variables, or the automatic null-ing of graph global variables upon graph element removal.\");\n", inputName);
                        sb.AppendFrontFormat("if(!{0}.Valid) throw new ArgumentException(\"The graph element handed in is invalid by now, i.e. not contained in the graph anymore. A common cause is a remaining reference in a local variable or a storage after element removal from the graph.\", \"{0}\");\n", inputName);
                    }
                }
            }

            sb.AppendFrontFormat("return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches{0});\n", inArguments);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} match{1})\n", matchType, outParameters);
            sb.AppendFront("{\n");
            sb.Indent();

            if(EmitDebugValidityChecks)
            {
                bool validityCheckDisabled = false;
                if(matchingPattern.annotations.ContainsAnnotation("validityCheck"))
                    validityCheckDisabled = matchingPattern.annotations["validityCheck"].Equals("false");
                if(!validityCheckDisabled)
                   sb.AppendFront("GRGEN_LIBGR.MatchedElementsValidityChecker.Check(match);\n");
            }

            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match{0});\n", outArguments);
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, {0} matches{1})\n", matchesType, outParametersAll);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("foreach({0} match in matches)\n", matchType);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", outIntermediateLocalsAll);
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match{0});\n", intermediateLocalArgumentsAll);
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFrontFormat("output_{0}.Add(output_local_{0});\n", i);
            }
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1})\n", inParameters, refParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{1});\n", matchesType, inArguments);
            sb.AppendFront("if(matches.Count <= 0) return false;\n");
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.AppendFront("return true;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0}{1})\n", inParameters, outParametersAll);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches{1});\n", matchesType, inArguments);
            sb.AppendFront("if(matches.Count <= 0) return 0;\n");
            sb.AppendFrontFormat("foreach({0} match in matches)\n", matchType);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", outIntermediateLocalsAll);
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match{0});\n", intermediateLocalArgumentsAll);
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFrontFormat("output_{0}.Add(output_local_{0});\n", i);
            }
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return matches.Count;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0})\n", inParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches;\n", matchesType);
            sb.AppendFrontFormat("{0}\n", outLocals);

            sb.AppendFront("while(true)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{0});\n", inArguments);
            sb.AppendFront("if(matches.Count <= 0) return true;\n");
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv{0})\n", inParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{1});\n", matchesType, inArguments);
            sb.AppendFront("if(matches.Count <= 0) return false;\n");
            sb.AppendFrontFormat("{0}\n", outLocals);
            sb.AppendFront("do\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.AppendFrontFormat("matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{0});\n", inArguments);
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("while(matches.Count > 0) ;\n");
            sb.AppendFront("return true;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max{0})\n", inParameters);
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0} matches;\n", matchesType);
            sb.AppendFrontFormat("{0}\n", outLocals);
            sb.AppendFront("for(int i = 0; i < max; i++)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1{0});\n", inArguments);
            sb.AppendFront("if(matches.Count <= 0) return i >= min;\n");
            sb.AppendFrontFormat("_rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First{0});\n", outArguments);
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return true;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            // implementation of inexact action interface by delegation to exact action interface
            sb.AppendFront("// implementation of inexact action interface by delegation to exact action interface\n");

            sb.AppendFront("public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("return Match(actionEnv, maxMatches{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", outLocals);
            sb.AppendFrontFormat("Modify(actionEnv, ({0})match{1});\n", matchType, outArguments);
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
            }
            sb.AppendFront("return ReturnArray;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)\n");
            sb.AppendFront("{\n");
            sb.Indent();

            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFront("output_list_");
                sb.Append(i.ToString());
                sb.Append(".Clear();\n");
            }
            sb.AppendFrontFormat("ModifyAll(actionEnv, ({0})matches{1});\n", matchesType, outClassArgumentsAll);

            sb.AppendFront("while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[" + matchingPattern.Outputs.Length + "]);\n");
            sb.AppendFront("ReturnArrayListForAll.Clear();\n");
            sb.AppendFront("for(int i=0; i<matches.Count; ++i)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("ReturnArrayListForAll.Add(AvailableReturnArrays[i]);\n");
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFrontFormat("ReturnArrayListForAll[i][{0}] = output_list_{0}[i];\n", i);
            }
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("return ReturnArrayListForAll;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if(matchingPattern.Inputs.Length == 0) {
                sb.AppendFrontFormat("{0}\n", refLocals);
                sb.AppendFrontFormat("if(Apply(actionEnv{0})) ", refArguments);
                sb.Append("{\n");
                sb.Indent();
                for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
                {
                    sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
                }
                sb.AppendFront("return ReturnArray;\n");
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.AppendFront("else return null;\n");
            }
            else
                sb.AppendFront("throw new Exception();\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("{0}\n", refLocals);
            sb.AppendFrontFormat("if(Apply(actionEnv{0}{1})) ", inArgumentsFromArray, refArguments);
            sb.Append("{\n");
            sb.Indent();
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFrontFormat("ReturnArray[{0}] = output_{0};\n", i);
            }
            sb.AppendFront("return ReturnArray;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");
            sb.AppendFront("else return null;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("public List<object[]> Reserve(int numReturns)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("if(AvailableReturnArrays == null)\n");
            sb.AppendFrontIndented("AvailableReturnArrays = new List<object[]>();\n");
            sb.AppendFront("while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[" + matchingPattern.Outputs.Length + "]);\n");
            sb.AppendFront("if(ReturnArrayListForAll == null)\n");
            sb.AppendFrontIndented("ReturnArrayListForAll = new List<object[]>();\n");
            sb.AppendFront("ReturnArrayListForAll.Clear();\n");
            sb.AppendFront("for(int i=0; i<numReturns; ++i)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("ReturnArrayListForAll.Add(AvailableReturnArrays[i]);\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return ReturnArrayListForAll;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if(matchingPattern.Inputs.Length == 0)
            {
                for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
                {
                    sb.AppendFront("output_list_");
                    sb.Append(i.ToString());
                    sb.Append(".Clear();\n");
                }
                sb.AppendFrontFormat("int matchesCount = ApplyAll(maxMatches, actionEnv{0});\n", outClassArgumentsAll);
                sb.AppendFront("while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[" + matchingPattern.Outputs.Length + "]);\n");
                sb.AppendFront("ReturnArrayListForAll.Clear();\n");
                sb.AppendFront("for(int i=0; i<matchesCount; ++i)\n");
                sb.AppendFront("{\n");
                sb.Indent();
                sb.AppendFront("ReturnArrayListForAll.Add(AvailableReturnArrays[i]);\n");
                for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
                {
                    sb.AppendFrontFormat("ReturnArrayListForAll[i][{0}] = output_list_{0}[i];\n", i);
                }
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.AppendFront("return ReturnArrayListForAll;\n");
            }
            else
                sb.AppendFront("throw new Exception();\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFront("output_list_");
                sb.Append(i.ToString());
                sb.Append(".Clear();\n");
            }
            sb.AppendFrontFormat("int matchesCount = ApplyAll(maxMatches, actionEnv{0}{1});\n", inArgumentsFromArray, outClassArgumentsAll);
            sb.AppendFront("while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[" + matchingPattern.Outputs.Length + "]);\n");
            sb.AppendFront("ReturnArrayListForAll.Clear();\n");
            sb.AppendFront("for(int i=0; i<matchesCount; ++i)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("ReturnArrayListForAll.Add(AvailableReturnArrays[i]);\n");
            for(int i = 0; i < matchingPattern.Outputs.Length; ++i)
            {
                sb.AppendFrontFormat("ReturnArrayListForAll[i][{0}] = output_list_{0}[i];\n", i);
            }
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return ReturnArrayListForAll;\n");
            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if(matchingPattern.Inputs.Length == 0)
                sb.AppendFront("return ApplyStar(actionEnv);\n");
            else
                sb.AppendFront("throw new Exception(); return false;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("return ApplyStar(actionEnv{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if(matchingPattern.Inputs.Length == 0)
                sb.AppendFront("return ApplyPlus(actionEnv);\n");
            else
                sb.AppendFront("throw new Exception(); return false;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFrontFormat("return ApplyPlus(actionEnv{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            if(matchingPattern.Inputs.Length == 0)
                sb.AppendFront("return ApplyMinMax(actionEnv, min, max);\n");
            else 
                sb.AppendFront("throw new Exception(); return false;\n");
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)\n");
            sb.AppendFront("{\n");
            sb.Indent(); 
            sb.AppendFrontFormat("return ApplyMinMax(actionEnv, min, max{0});\n", inArgumentsFromArray);
            sb.Unindent(); 
            sb.AppendFront("}\n");

            sb.AppendFront("public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCallWithArguments filter)\n");
            sb.AppendFront("{\n");
            sb.Indent();
            sb.AppendFront("switch(filter.PackagePrefixedName) {\n");
            sb.Indent();
            foreach(IFilter filter in matchingPattern.Filters)
            {
                if(filter is IFilterAutoSupplied)
                {
                    IFilterAutoSupplied filterAutoSupplied = (IFilterAutoSupplied)filter;
                    sb.AppendFrontFormat("case \"{0}\": ", filterAutoSupplied.Name);
                    sb.AppendFormat("matches.Filter_{0}(", filterAutoSupplied.Name);
                    bool first = true;
                    for(int i = 0; i < filterAutoSupplied.Inputs.Length; ++i)
                    {
                        if(first)
                            first = false;
                        else
                            sb.Append(", ");
                        sb.AppendFormat("({0})(filter.Arguments[{1}])", TypesHelper.TypeName(filterAutoSupplied.Inputs[i]), i);
                    }
                    sb.Append("); break;\n");
                }
                else if(filter is IFilterAutoGenerated)
                {
                    LGSPFilterAutoGenerated filterAutoGenerated = (LGSPFilterAutoGenerated)filter;
                    sb.AppendFrontFormat("case \"{0}\": ", filterAutoGenerated.Name);
                    sb.AppendFormat("{0}MatchFilters.",
                        "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(filterAutoGenerated.PackageOfApplyee));
                    sb.AppendFormat("Filter_{0}_{1}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({2})matches); break;\n",
                        patternName, filterAutoGenerated.NameWithUnderscoreSuffix, matchesType);
                }
                else if(filter is IFilterFunction)
                {
                    IFilterFunction filterFunction = (IFilterFunction)filter;
                    if(filterFunction.Package == null
                        || (matchingPattern.PatternGraph.Package != null && filterFunction.Package == matchingPattern.PatternGraph.Package && matchingPattern.GetFilter(filterFunction.Name) == null))
                    {
                        sb.AppendFrontFormat("case \"{0}\": {1}MatchFilters.Filter_{2}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({3})matches",
                            filterFunction.Name, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(filterFunction.Package), filterFunction.Name, matchesType);
                        for(int i = 0; i < filterFunction.Inputs.Length; ++i)
                        {
                            sb.AppendFormat(", ({0})(filter.Arguments[{1}])", TypesHelper.TypeName(filterFunction.Inputs[i]), i);
                        }
                        sb.Append("); break;\n");
                    }
                    if(filterFunction.Package != null)
                    {
                        sb.AppendFrontFormat("case \"{0}::{1}\": ", filterFunction.Package, filterFunction.Name);
                        sb.AppendFormat("{0}MatchFilters.Filter_{1}((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv, ({2})matches",
                            "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(filterFunction.Package), filterFunction.Name, matchesType);
                        for(int i = 0; i < filterFunction.Inputs.Length; ++i)
                        {
                            sb.AppendFormat(", ({0})(filter.Arguments[{1}])", TypesHelper.TypeName(filterFunction.Inputs[i]), i);
                        }
                        sb.Append("); break;\n");
                    }
                }
            }
            sb.AppendFront("default: throw new Exception(\"Unknown filter name \" + filter.PackagePrefixedName + \"!\");\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n");
        }

        /// <summary>
        /// Generates the search program(s) for the pattern graph of the given rule
        /// </summary>
        SearchProgram GenerateSearchProgram(LGSPMatchingPattern matchingPattern)
        {
            PatternGraph patternGraph = matchingPattern.patternGraph;
            SearchProgram searchProgramRoot = null;
            SearchProgram searchProgramListEnd = null;

            for(int i=0; i<patternGraph.schedulesIncludingNegativesAndIndependents.Length; ++i)
            {
                ScheduledSearchPlan scheduledSearchPlan = patternGraph.schedulesIncludingNegativesAndIndependents[i];

#if DUMP_SCHEDULED_SEARCH_PLAN
                StreamWriter sspwriter = new StreamWriter(matchingPattern.name + i + "_ssp_dump.txt");
                float prevCostToEnd = scheduledSearchPlan.Operations.Length > 0 ? scheduledSearchPlan.Operations[0].CostToEnd : 0f;
                foreach(SearchOperation so in scheduledSearchPlan.Operations)
                {
                    sspwriter.Write(SearchOpToString(so) + " ; " + so.CostToEnd + " (+" + (prevCostToEnd-so.CostToEnd) + ")" + "\n");
                    prevCostToEnd = so.CostToEnd;
                }
                sspwriter.Close();
#endif

                // build pass: build nested program from scheduled search plan
                if(matchingPattern is LGSPRulePattern)
                {
                    SearchProgram sp = SearchProgramBuilder.BuildSearchProgram(model,
                        (LGSPRulePattern)matchingPattern, i, null, false, Profile);
                    if(i == 0)
                        searchProgramRoot = searchProgramListEnd = sp;
                    else
                        searchProgramListEnd = (SearchProgram)searchProgramListEnd.Append(sp);
                }
                else
                {
                    Debug.Assert(searchProgramRoot==null);
                    searchProgramRoot = searchProgramListEnd = SearchProgramBuilder.BuildSearchProgram(
                        model, matchingPattern, false, Profile);
                }
            }

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgramRoot.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_" + searchProgramRoot.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgramRoot);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgramRoot.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_" + searchProgramRoot.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgramRoot;
        }

        /// <summary>
        /// Generates the parallelized search program(s) for the pattern graph of the given rule
        /// </summary>
        SearchProgram GenerateParallelizedSearchProgramAsNeeded(LGSPMatchingPattern matchingPattern)
        {
            PatternGraph patternGraph = matchingPattern.patternGraph;
            if(patternGraph.parallelizedSchedule == null)
                return null;

            SearchProgram searchProgramRoot = null;
            SearchProgram searchProgramListEnd = null;

            for(int i = 0; i < patternGraph.parallelizedSchedule.Length; ++i) // 2 for actions, 1 for subpatterns
            {
                ScheduledSearchPlan scheduledSearchPlan = patternGraph.parallelizedSchedule[i];

#if DUMP_SCHEDULED_SEARCH_PLAN
                StreamWriter sspwriter = new StreamWriter(matchingPattern.name + (i==1 ? "_parallelized_body" : "_parallelized") + "_ssp_dump.txt");
                float prevCostToEnd = scheduledSearchPlan.Operations.Length > 0 ? scheduledSearchPlan.Operations[0].CostToEnd : 0f;
                foreach(SearchOperation so in scheduledSearchPlan.Operations)
                {
                    sspwriter.Write(SearchOpToString(so) + " ; " + so.CostToEnd + " (+" + (prevCostToEnd-so.CostToEnd) + ")" + "\n");
                    prevCostToEnd = so.CostToEnd;
                }
                sspwriter.Close();
#endif

                // build pass: build nested program from scheduled search plan
                if(matchingPattern is LGSPRulePattern)
                {
                    SearchProgram sp = SearchProgramBuilder.BuildSearchProgram(model, (LGSPRulePattern)matchingPattern, i, null, true, Profile);
                    if(i == 0)
                        searchProgramRoot = searchProgramListEnd = sp;
                    else
                        searchProgramListEnd = (SearchProgram)searchProgramListEnd.Append(sp);
                }
                else
                {
                    Debug.Assert(searchProgramRoot == null);
                    searchProgramRoot = searchProgramListEnd = SearchProgramBuilder.BuildSearchProgram(model, matchingPattern, true, Profile);
                }
            }

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgramRoot.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + (i==1 ? "_parallelized_body" : "_parallelized") + "_" + searchProgramRoot.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgramRoot);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgramRoot.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + (i==1 ? "_parallelized_body" : "_parallelized") + "_" + searchProgramRoot.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgramRoot;
        }

        /// <summary>
        /// Generates the search program for the given alternative 
        /// </summary>
        SearchProgram GenerateSearchProgramAlternative(LGSPMatchingPattern matchingPattern, Alternative alt)
        {
            // build pass: build nested program from scheduled search plans of the alternative cases
            SearchProgram searchProgram = SearchProgramBuilder.BuildSearchProgram(model, matchingPattern, alt, false, Profile);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_" + alt.name + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_" + alt.name + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgram;
        }

        /// <summary>
        /// Generates the parallelized search program for the given alternative 
        /// </summary>
        SearchProgram GenerateParallelizedSearchProgramAlternativeAsNeeded(LGSPMatchingPattern matchingPattern, Alternative alt)
        {
            if(matchingPattern.patternGraph.parallelizedSchedule == null)
                return null;

            // build pass: build nested program from scheduled search plans of the alternative cases
            SearchProgram searchProgram = SearchProgramBuilder.BuildSearchProgram(model, matchingPattern, alt, true, Profile);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_parallelized_" + alt.name + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_parallelized_" + alt.name + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgram;
        }

        /// <summary>
        /// Generates the search program for the given iterated pattern
        /// </summary>
        SearchProgram GenerateSearchProgramIterated(LGSPMatchingPattern matchingPattern, PatternGraph iter)
        {
            // build pass: build nested program from scheduled search plan of the all pattern
            SearchProgram searchProgram = SearchProgramBuilder.BuildSearchProgram(model, matchingPattern, iter, false, Profile);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_" + iter.name + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_" + iter.name + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgram;
        }

        /// <summary>
        /// Generates the parallelized search program for the given iterated pattern
        /// </summary>
        SearchProgram GenerateParallelizedSearchProgramIteratedAsNeeded(LGSPMatchingPattern matchingPattern, PatternGraph iter)
        {
            if(matchingPattern.patternGraph.parallelizedSchedule == null)
                return null;

            // build pass: build nested program from scheduled search plan of the all pattern
            SearchProgram searchProgram = SearchProgramBuilder.BuildSearchProgram(model, matchingPattern, iter, true, Profile);

#if DUMP_SEARCHPROGRAMS
            // dump built search program for debugging
            SourceBuilder builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            StreamWriter writer = new StreamWriter(matchingPattern.name + "_parallelized_" + iter.name + "_" + searchProgram.Name + "_built_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            // complete pass: complete check operations in all search programs
            SearchProgramCompleter.CompleteCheckOperationsInAllSearchPrograms(searchProgram);

#if DUMP_SEARCHPROGRAMS
            // dump completed search program for debugging
            builder = new SourceBuilder(CommentSourceCode);
            searchProgram.Dump(builder);
            writer = new StreamWriter(matchingPattern.name + "_parallelized_" + iter.name + "_" + searchProgram.Name + "_completed_dump.txt");
            writer.Write(builder.ToString());
            writer.Close();
#endif

            return searchProgram;
        }

        /// <summary>
        /// Generates file header for actions file into given source builer
        /// </summary>
        public void GenerateFileHeaderForActionsFile(SourceBuilder sb,
                String namespaceOfModel, String namespaceOfRulePatterns)
        {
            sb.AppendFront("using System;\n"
                + "using System.Collections.Generic;\n"
                + "using System.Collections;\n"
                + "using System.Text;\n"
                + "using System.Threading;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_EXPR = de.unika.ipd.grGen.expression;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
                + "using GRGEN_MODEL = " + namespaceOfModel + ";\n"
                + "using GRGEN_ACTIONS = " + namespaceOfRulePatterns + ";\n"
                + "using " + namespaceOfRulePatterns + ";\n\n");
            sb.AppendFront("namespace de.unika.ipd.grGen.lgspActions\n");
            sb.AppendFront("{\n");
            sb.Indent(); // namespace level
        }

        /// <summary>
        /// Generates matcher class head source code for the pattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        void GenerateMatcherClassHeadAction(SourceBuilder sb, LGSPRulePattern rulePattern, 
            bool isInitialStatic, SearchProgram searchProgram)
        {
            PatternGraph patternGraph = (PatternGraph)rulePattern.PatternGraph;
                
            String namePrefix = (isInitialStatic ? "" : "Dyn") + "Action_";
            String className = namePrefix + rulePattern.name;
            String rulePatternClassName = rulePattern.GetType().Name;
            String matchClassName = rulePatternClassName + "." + "Match_" + rulePattern.name;
            String matchInterfaceName = rulePatternClassName + "." + "IMatch_" + rulePattern.name;
            String actionInterfaceName = "IAction_" + rulePattern.name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPAction, "
                + "GRGEN_LIBGR.IAction, " + actionInterfaceName + "\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("public " + className + "()\n");
            sb.AppendFront("    : base(" + rulePatternClassName + ".Instance.patternGraph)\n");
            sb.AppendFront("{\n");
            sb.Indent(); // method body level
            sb.AppendFront("_rulePattern = " + rulePatternClassName + ".Instance;\n");
            if(rulePattern.patternGraph.branchingFactor < 2)
            {
                sb.AppendFront("DynamicMatch = myMatch;\n");
                if(!isInitialStatic)
                    sb.AppendFrontFormat("GRGEN_ACTIONS.Action_{0}.Instance.DynamicMatch = myMatch;\n", rulePattern.name);
            }
            else
            {
                sb.AppendFront("if(Environment.ProcessorCount == 1)\n");
                sb.AppendFront("{\n");
                sb.AppendFrontIndented("DynamicMatch = myMatch;\n");
                if(!isInitialStatic)
                    sb.AppendFrontIndentedFormat("GRGEN_ACTIONS.Action_{0}.Instance.DynamicMatch = myMatch;\n", rulePattern.name);
                sb.AppendFront("}\n");
                sb.AppendFront("else\n");
                sb.AppendFront("{\n");
                sb.Indent();
                sb.AppendFront("DynamicMatch = myMatch_parallelized;\n");
                if(!isInitialStatic)
                    sb.AppendFrontFormat("GRGEN_ACTIONS.Action_{0}.Instance.DynamicMatch = myMatch_parallelized;\n", rulePattern.name);
                sb.AppendFrontFormat("numWorkerThreads = GRGEN_LGSP.WorkerPool.EnsurePoolSize({0});\n", rulePattern.patternGraph.branchingFactor);
                sb.AppendFrontFormat("parallelTaskMatches = new GRGEN_LGSP.LGSPMatchesList<{0}, {1}>[numWorkerThreads];\n", matchClassName, matchInterfaceName);
                sb.AppendFront("moveHeadAfterNodes = new List<GRGEN_LGSP.LGSPNode>[numWorkerThreads];\n");
                sb.AppendFront("moveHeadAfterEdges = new List<GRGEN_LGSP.LGSPEdge>[numWorkerThreads];\n");
                sb.AppendFront("moveOutHeadAfter = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[numWorkerThreads];\n");
                sb.AppendFront("moveInHeadAfter = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[numWorkerThreads];\n");
                sb.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                sb.AppendFront("{\n");
                sb.Indent();
                sb.AppendFront("moveHeadAfterNodes[i] = new List<GRGEN_LGSP.LGSPNode>();\n");
                sb.AppendFront("moveHeadAfterEdges[i] = new List<GRGEN_LGSP.LGSPEdge>();\n");
                sb.AppendFront("moveOutHeadAfter[i] = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>();\n");
                sb.AppendFront("moveInHeadAfter[i] = new List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>();\n");
                sb.Unindent();
                sb.AppendFront("}\n");

                sb.AppendFront("for(int i=0; i<parallelTaskMatches.Length; ++i)\n");
                sb.AppendFrontIndentedFormat("parallelTaskMatches[i] = new GRGEN_LGSP.LGSPMatchesList<{0}, {1}>(this);\n", matchClassName, matchInterfaceName);
                sb.Unindent();
                sb.AppendFront("}\n");
            }
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            sb.AppendFront("public " + rulePatternClassName + " _rulePattern;\n");
            sb.AppendFront("public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }\n");
            sb.AppendFront("public override string Name { get { return \"" + rulePattern.name + "\"; } }\n");
            sb.AppendFront("[ThreadStatic] private static GRGEN_LGSP.LGSPMatchesList<" + matchClassName + ", " + matchInterfaceName + "> matches;\n\n");

            sb.AppendFront("// Performance optimization: saves us usage of new for the return array or the return arrays. In the object/string-style modify/apply methods of the action interface implementation.\n");
            sb.AppendFront("[ThreadStatic] public static object[] ReturnArray;\n");
            sb.AppendFront("[ThreadStatic] public static List<object[]> ReturnArrayListForAll;\n");
            sb.AppendFront("[ThreadStatic] public static List<object[]> AvailableReturnArrays;\n");

            if(isInitialStatic)
            {
                sb.AppendFront("public static " + className + " Instance { get { return instance; } set { instance = value; } }\n");
                sb.AppendFront("private static " + className + " instance = new " + className + "();\n");
            }

            GenerateIndependentsMatchObjects(sb, rulePattern, patternGraph);

            sb.AppendFront("\n");

            GenerateParallelizationSetupAsNeeded(sb, rulePattern, searchProgram);
        }

        void GenerateParallelizationSetupAsNeeded(SourceBuilder sb, LGSPRulePattern rulePattern, SearchProgram searchProgram)
        {
            if(rulePattern.patternGraph.branchingFactor < 2)
                return;

            foreach(SearchOperation so in rulePattern.patternGraph.parallelizedSchedule[0].Operations)
            {
                switch(so.Type)
                {
                    case SearchOperationType.WriteParallelPreset:
                        if(so.Element is SearchPlanNodeNode)
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0};\n", NamesOfEntities.IterationParallelizationParallelPresetCandidate(((SearchPlanNodeNode)so.Element).PatternElement.Name));
                        else //SearchPlanEdgeNode
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationParallelPresetCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.WriteParallelPresetVar:
                        sb.AppendFrontFormat("{0} {1};\n",
                            TypesHelper.TypeName(((PatternVariable)so.Element).Type),
                            NamesOfEntities.IterationParallelizationParallelPresetCandidate(((PatternVariable)so.Element).Name));
                        break;
                    case SearchOperationType.SetupParallelLookup:
                        if(so.Element is SearchPlanNodeNode)
                        {
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanNodeNode)so.Element).PatternElement.Name));
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanNodeNode)so.Element).PatternElement.Name));
                        }
                        else
                        {
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                            sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        }
                        break;
                    case SearchOperationType.SetupParallelPickFromStorage:
                        if(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.type).StartsWith("set") || TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.type).StartsWith("map"))
                        {
                            sb.AppendFrontFormat("IEnumerator<KeyValuePair<{0},{1}>> {2};\n",
                                TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.Type)), model),
                                TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.Type)), model),
                                NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        else
                        {
                            sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                                TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.DotNetTypeToXgrsType(so.Storage.Variable.Type)), model),
                                NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        break;
                    case SearchOperationType.SetupParallelPickFromStorageDependent:
                        if(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute).StartsWith("set") || TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute).StartsWith("map"))
                        {
                            sb.AppendFrontFormat("IEnumerator<KeyValuePair<{0},{1}>> {2};\n",
                               TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute)), model),
                               TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute)), model),
                               NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        else
                        {
                            sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                               TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(TypesHelper.AttributeTypeToXgrsType(so.Storage.Attribute.Attribute)), model),
                               NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        }
                        break;
                    case SearchOperationType.SetupParallelPickFromIndex:
                        sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                           TypesHelper.TypeName(so.IndexAccess.Index is AttributeIndexDescription ?
                               ((AttributeIndexDescription)so.IndexAccess.Index).GraphElementType :
                               ((IncidenceCountIndexDescription)so.IndexAccess.Index).StartNodeType),
                           NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.SetupParallelPickFromIndexDependent:
                        sb.AppendFrontFormat("IEnumerator<{0}> {1};\n",
                           TypesHelper.TypeName(so.IndexAccess.Index is AttributeIndexDescription ?
                               ((AttributeIndexDescription)so.IndexAccess.Index).GraphElementType :
                               ((IncidenceCountIndexDescription)so.IndexAccess.Index).StartNodeType),
                           NamesOfEntities.IterationParallelizationIterator(((SearchPlanNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.SetupParallelIncoming:
                    case SearchOperationType.SetupParallelOutgoing:
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        break;
                    case SearchOperationType.SetupParallelIncident:
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationListHead(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        sb.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0};\n", NamesOfEntities.IterationParallelizationNextCandidate(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        sb.AppendFrontFormat("int {0};\n", NamesOfEntities.IterationParallelizationDirectionRunCounterVariable(((SearchPlanEdgeNode)so.Element).PatternElement.Name));
                        break;
                }
            }
            sb.AppendFront("\n");

            String rulePatternClassName = rulePattern.GetType().Name;
            String matchClassName = rulePatternClassName + "." + "Match_" + rulePattern.name;
            String matchInterfaceName = rulePatternClassName + "." + "IMatch_" + rulePattern.name;
            sb.AppendFront("private static GRGEN_LGSP.LGSPMatchesList<" + matchClassName + ", " + matchInterfaceName + ">[] parallelTaskMatches;\n");
            sb.AppendFront("private static int numWorkerThreads;\n");
            sb.AppendFront("private static int iterationNumber;\n");
            sb.AppendFront("private static int iterationLock;\n");
            sb.AppendFront("[ThreadStatic] private static int currentIterationNumber;\n");
            sb.AppendFront("[ThreadStatic] private static int threadId;\n");
            sb.AppendFront("private static GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnvParallel;\n");
            sb.AppendFront("private static int maxMatchesParallel;\n");
            sb.AppendFront("private static bool maxMatchesFound = false;\n");

            sb.AppendFront("private static List<GRGEN_LGSP.LGSPNode>[] moveHeadAfterNodes;\n");
            sb.AppendFront("private static List<GRGEN_LGSP.LGSPEdge>[] moveHeadAfterEdges;\n");
            sb.AppendFront("private static List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[] moveOutHeadAfter;\n");
            sb.AppendFront("private static List<KeyValuePair<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPEdge>>[] moveInHeadAfter;\n");
            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the subpattern of the rulePattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadSubpattern(SourceBuilder sb, LGSPMatchingPattern matchingPattern,
            bool isInitialStatic)
        {
            Debug.Assert(!(matchingPattern is LGSPRulePattern));
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "PatternAction_";
            String className = namePrefix + matchingPattern.name;
            String matchingPatternClassName = matchingPattern.GetType().Name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_)\n");
            sb.AppendFrontFormat("    : base(null)\n");
            sb.AppendFront("{\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            sb.AppendFront("patternGraph = " + matchingPatternClassName + ".Instance.patternGraph;\n");
           
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            GenerateTasksMemoryPool(sb, className, false, false, matchingPattern.patternGraph.branchingFactor);

            // generate task variables
            for(int i = 0; i < patternGraph.nodesPlusInlined.Length; ++i)
            {
                PatternNode node = patternGraph.nodesPlusInlined[i];
                if(node.PointOfDefinition == null)
                    sb.AppendFront("public GRGEN_LGSP.LGSPNode " + node.name + ";\n");
            }
            for(int i = 0; i < patternGraph.edgesPlusInlined.Length; ++i)
            {
                PatternEdge edge = patternGraph.edgesPlusInlined[i];
                if(edge.PointOfDefinition == null)
                    sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + edge.name + ";\n");
            }
            for(int i = 0; i < patternGraph.variablesPlusInlined.Length; ++i)
            {
                PatternVariable variable = patternGraph.variablesPlusInlined[i];
                sb.AppendFront("public " +TypesHelper.TypeName(variable.type) + " " + variable.name + ";\n");
            }

            GenerateIndependentsMatchObjects(sb, matchingPattern, patternGraph);

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the given alternative into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadAlternative(SourceBuilder sb, LGSPMatchingPattern matchingPattern, 
            Alternative alternative, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "AlternativeAction_";
            String className = namePrefix + alternative.pathPrefix+alternative.name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level


            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_)\n");
            sb.AppendFrontFormat("    : base(patternGraphs_)\n");
            sb.AppendFront("{\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            
            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            GenerateTasksMemoryPool(sb, className, true, false, matchingPattern.patternGraph.branchingFactor);

            Dictionary<string, PatternNode> neededNodes = new Dictionary<string, PatternNode>();
            Dictionary<string, PatternEdge> neededEdges = new Dictionary<string, PatternEdge>();
            Dictionary<string, PatternVariable> neededVariables = new Dictionary<string, PatternVariable>();
            foreach(PatternGraph altCase in alternative.alternativeCases)
            {
                foreach(KeyValuePair<string, PatternNode> neededNode in altCase.neededNodes)
                {
                    neededNodes[neededNode.Key] = neededNode.Value;
                }
                foreach(KeyValuePair<string, PatternEdge> neededEdge in altCase.neededEdges)
                {
                    neededEdges[neededEdge.Key] = neededEdge.Value;
                }
                foreach(KeyValuePair<string, PatternVariable> neededVariable in altCase.neededVariables)
                {
                    neededVariables[neededVariable.Key] = neededVariable.Value;
                }
            }

            // generate task variables
            foreach(KeyValuePair<string, PatternNode> node in neededNodes)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPNode " + node.Key + ";\n");
            }
            foreach(KeyValuePair<string, PatternEdge> edge in neededEdges)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + edge.Key + ";\n");
            }
            foreach(KeyValuePair<string, PatternVariable> variable in neededVariables)
            {
                sb.AppendFront("public " + TypesHelper.TypeName(variable.Value.Type) + " " + variable.Key + ";\n");
            }
    
            foreach(PatternGraph altCase in alternative.alternativeCases)
            {
                GenerateIndependentsMatchObjects(sb, matchingPattern, altCase);
            }

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates matcher class head source code for the given iterated pattern into given source builder
        /// isInitialStatic tells whether the initial static version or a dynamic version after analyze is to be generated.
        /// </summary>
        public void GenerateMatcherClassHeadIterated(SourceBuilder sb, LGSPMatchingPattern matchingPattern,
            PatternGraph iter, bool isInitialStatic)
        {
            PatternGraph patternGraph = (PatternGraph)matchingPattern.PatternGraph;

            String namePrefix = (isInitialStatic ? "" : "Dyn") + "IteratedAction_";
            String className = namePrefix + iter.pathPrefix + iter.name;
            String matchingPatternClassName = matchingPattern.GetType().Name;

            if(patternGraph.Package != null)
            {
                sb.AppendFrontFormat("namespace {0}\n", patternGraph.Package);
                sb.AppendFront("{\n");
                sb.Indent();
            }

            sb.AppendFront("public class " + className + " : GRGEN_LGSP.LGSPSubpatternAction\n");
            sb.AppendFront("{\n");
            sb.Indent(); // class level

            sb.AppendFront("private " + className + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_)\n");
            sb.AppendFrontFormat("    : base(null)\n");
            sb.AppendFront("{\n");
            sb.Indent(); // method body level
            sb.AppendFront("actionEnv = actionEnv_; openTasks = openTasks_;\n");
            sb.AppendFront("patternGraph = " + matchingPatternClassName + ".Instance.patternGraph;\n");
            int index = -1;
            for(int i=0; i<iter.embeddingGraph.iteratedsPlusInlined.Length; ++i)
            {
                if(iter.embeddingGraph.iteratedsPlusInlined[i].iteratedPattern == iter)
                    index = i;
            }
            sb.AppendFrontFormat("minMatchesIter = {0};\n", iter.embeddingGraph.iteratedsPlusInlined[index].minMatches);
            sb.AppendFrontFormat("maxMatchesIter = {0};\n", iter.embeddingGraph.iteratedsPlusInlined[index].maxMatches);
            sb.AppendFront("numMatchesIter = 0;\n");
            if(iter.isIterationBreaking)
                sb.AppendFront("breakIteration = false;\n");

            sb.Unindent(); // class level
            sb.AppendFront("}\n\n");

            sb.AppendFront("int minMatchesIter;\n");
            sb.AppendFront("int maxMatchesIter;\n");
            sb.AppendFront("int numMatchesIter;\n");
            if(iter.isIterationBreaking)
                sb.AppendFront("bool breakIteration;\n");
            sb.Append("\n");

            GenerateTasksMemoryPool(sb, className, false, iter.isIterationBreaking, matchingPattern.patternGraph.branchingFactor);

            Dictionary<string, PatternNode> neededNodes = new Dictionary<string, PatternNode>();
            Dictionary<string, PatternEdge> neededEdges = new Dictionary<string, PatternEdge>();
            Dictionary<string, PatternVariable> neededVariables = new Dictionary<string, PatternVariable>();
            foreach(KeyValuePair<string, PatternNode> neededNode in iter.neededNodes)
            {
                neededNodes[neededNode.Key] = neededNode.Value;
            }
            foreach(KeyValuePair<string, PatternEdge> neededEdge in iter.neededEdges)
            {
                neededEdges[neededEdge.Key] = neededEdge.Value;
            }
            foreach(KeyValuePair<string, PatternVariable> neededVariable in iter.neededVariables)
            {
                neededVariables[neededVariable.Key] = neededVariable.Value;
            }

            // generate task variables
            foreach(KeyValuePair<string, PatternNode> node in neededNodes)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPNode " + node.Key + ";\n");
            }
            foreach(KeyValuePair<string, PatternEdge> edge in neededEdges)
            {
                sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + edge.Key + ";\n");
            }
            foreach(KeyValuePair<string, PatternVariable> variable in neededVariables)
            {
                sb.AppendFront("public " + TypesHelper.TypeName(variable.Value.Type) + " " + variable.Key + ";\n");
            }

            GenerateIndependentsMatchObjects(sb, matchingPattern, iter);

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates match objects of independents (one pre-allocated is part of action class)
        /// </summary>
        private void GenerateIndependentsMatchObjects(SourceBuilder sb,
            LGSPMatchingPattern matchingPatternClass, PatternGraph patternGraph)
        {
            if(patternGraph.nestedIndependents != null)
            {
                foreach(KeyValuePair<PatternGraph, bool> nestedIndependent in patternGraph.nestedIndependents)
                {
                    if(nestedIndependent.Value == true)
                        continue; // if independent is nested in iterated with potentially more than one match we need stack-based match variables for the multiple matches living at a time

                    if(nestedIndependent.Key.originalPatternGraph != null)
                    {
                        sb.AppendFrontFormat("private {0} {1} = new {0}();",
                            nestedIndependent.Key.originalSubpatternEmbedding.matchingPatternOfEmbeddedGraph.GetType().Name + "." + NamesOfEntities.MatchClassName(nestedIndependent.Key.originalPatternGraph.pathPrefix + nestedIndependent.Key.originalPatternGraph.name),
                            NamesOfEntities.MatchedIndependentVariable(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name));
                    }
                    else
                    {
                        sb.AppendFrontFormat("private {0} {1} = new {0}();",
                            matchingPatternClass.GetType().Name + "." + NamesOfEntities.MatchClassName(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name),
                            NamesOfEntities.MatchedIndependentVariable(nestedIndependent.Key.pathPrefix + nestedIndependent.Key.name));
                    }
                }
            }

            foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                GenerateIndependentsMatchObjects(sb, matchingPatternClass, idpt);
            }
        }

        /// <summary>
        /// Generates memory pooling code for matching tasks of class given by it's name
        /// </summary>
        private void GenerateTasksMemoryPool(SourceBuilder sb, String className, bool isAlternative, bool isIterationBreaking, int branchingFactor)
        {
            // getNewTask method handing out new task from pool or creating task if pool is empty
            if(isAlternative)
            {
                sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                    + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {\n");
            }
            else
            {
                sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                    + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {\n");
            }
            sb.Indent();
            sb.AppendFront(className + " newTask;\n");
            sb.AppendFront("if(numFreeTasks>0) {\n");
            sb.Indent();
            sb.AppendFront("newTask = freeListHead;\n"); 
            sb.AppendFront("newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;\n");
            if(isIterationBreaking)
                sb.AppendFront("newTask.breakIteration = false;\n");
            sb.AppendFront("freeListHead = newTask.next;\n");
            sb.AppendFront("newTask.next = null;\n");
            sb.AppendFront("--numFreeTasks;\n");
            sb.Unindent();
            sb.AppendFront("} else {\n");
            sb.Indent();
            if(isAlternative)
                sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_, patternGraphs_);\n");
            else
                sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_);\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.AppendFront("return newTask;\n");
            sb.Unindent();
            sb.AppendFront("}\n\n");

            // releaseTask method recycling task into pool if pool is not full
            sb.AppendFront("public static void releaseTask(" + className + " oldTask) {\n");
            sb.Indent();
            sb.AppendFront("if(numFreeTasks<MAX_NUM_FREE_TASKS) {\n");
            sb.Indent();
            sb.AppendFront("oldTask.next = freeListHead;\n");
            sb.AppendFront("oldTask.actionEnv = null; oldTask.openTasks = null;\n");
            sb.AppendFront("freeListHead = oldTask;\n");
            sb.AppendFront("++numFreeTasks;\n");
            sb.Unindent();
            sb.AppendFront("}\n");
            sb.Unindent();
            sb.AppendFront("}\n\n");

            // tasks pool administration data
            sb.AppendFront("private static " + className + " freeListHead = null;\n");
            sb.AppendFront("private static int numFreeTasks = 0;\n");
            sb.AppendFront("private const int MAX_NUM_FREE_TASKS = 100;\n\n"); // todo: compute antiproportional to pattern size

            sb.AppendFront("private " + className + " next = null;\n\n");

            // for parallelized subpatterns/alternatives/iterateds we need a freelist per thread
            if(branchingFactor > 1)
            {
                // getNewTask method handing out new task from pool or creating task if pool is empty
                if(isAlternative)
                {
                    sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                        + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_, int threadId) {\n");
                }
                else
                {
                    sb.AppendFront("public static " + className + " getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, "
                        + "Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, int threadId) {\n");
                }
                sb.Indent();
                sb.AppendFront(className + " newTask;\n");
                sb.AppendFront("if(numFreeTasks_perWorker[threadId]>0) {\n");
                sb.Indent();
                sb.AppendFront("newTask = freeListHead_perWorker[threadId];\n");
                sb.AppendFront("newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;\n");
                if(isIterationBreaking)
                    sb.AppendFront("newTask.breakIteration = false;\n");
                sb.AppendFront("freeListHead_perWorker[threadId] = newTask.next;\n");
                sb.AppendFront("newTask.next = null;\n");
                sb.AppendFront("--numFreeTasks_perWorker[threadId];\n");
                sb.Unindent();
                sb.AppendFront("} else {\n");
                sb.Indent();
                if(isAlternative)
                    sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_, patternGraphs_);\n");
                else
                    sb.AppendFront("newTask = new " + className + "(actionEnv_, openTasks_);\n");
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.AppendFront("return newTask;\n");
                sb.Unindent();
                sb.AppendFront("}\n\n");

                // releaseTask method recycling task into pool if pool is not full
                sb.AppendFront("public static void releaseTask(" + className + " oldTask, int threadId) {\n");
                sb.Indent();
                sb.AppendFront("if(numFreeTasks_perWorker[threadId]<MAX_NUM_FREE_TASKS/2) {\n");
                sb.Indent();
                sb.AppendFront("oldTask.next = freeListHead_perWorker[threadId];\n");
                sb.AppendFront("oldTask.actionEnv = null; oldTask.openTasks = null;\n");
                sb.AppendFront("freeListHead_perWorker[threadId] = oldTask;\n");
                sb.AppendFront("++numFreeTasks_perWorker[threadId];\n");
                sb.Unindent();
                sb.AppendFront("}\n");
                sb.Unindent();
                sb.AppendFront("}\n\n");

                // tasks pool administration data
                sb.AppendFront("private static " + className + "[] freeListHead_perWorker = new " + className + "[Math.Min(" + branchingFactor + ", Environment.ProcessorCount)];\n");
                sb.AppendFront("private static int[] numFreeTasks_perWorker = new int[Math.Min(" + branchingFactor + ", Environment.ProcessorCount)];\n");
            }
        }

        /// <summary>
        /// Generates matcher class tail source code
        /// </summary>
        public void GenerateMatcherClassTail(SourceBuilder sb, bool containedInPackage)
        {
            sb.Unindent();
            sb.AppendFront("}\n");

            if(containedInPackage)
            {
                sb.Unindent();
                sb.AppendFront("}\n");
            }

            sb.AppendFront("\n");
        }

        /// <summary>
        /// Generates scheduled search plans needed for matcher code generation for action compilation
        /// out of graph with analyze information, 
        /// The scheduled search plans are added to the main and the nested pattern graphs.
        /// </summary>
        public void GenerateScheduledSearchPlans(PatternGraph patternGraph, LGSPGraph graph,
            bool isSubpatternLike, bool isNegativeOrIndependent, 
            ScheduledSearchPlan nestingScheduledSearchPlan)
        {
            for(int i=0; i<patternGraph.schedules.Length; ++i)
            {
                patternGraph.AdaptToMaybeNull(i);
                if(Profile)
                    SetNeedForProfiling(patternGraph);
                PlanGraph planGraph = PlanGraphGenerator.GeneratePlanGraph(model, graph.statistics, patternGraph,
                    isNegativeOrIndependent, isSubpatternLike, InlineIndependents,
                    ScheduleEnricher.ExtractOwnElements(nestingScheduledSearchPlan, patternGraph));
                PlanGraphGenerator.MarkMinimumSpanningArborescence(planGraph, patternGraph.name, DumpSearchPlan);
                SearchPlanGraph searchPlanGraph = SearchPlanGraphGeneratorAndScheduler.GenerateSearchPlanGraph(planGraph);
                ScheduledSearchPlan scheduledSearchPlan = SearchPlanGraphGeneratorAndScheduler.ScheduleSearchPlan(
                    searchPlanGraph, patternGraph, isNegativeOrIndependent, LazyNegativeIndependentConditionEvaluation);
                ScheduleEnricher.AppendHomomorphyInformation(model, scheduledSearchPlan);
                patternGraph.schedules[i] = scheduledSearchPlan;
                patternGraph.RevertMaybeNullAdaption(i);

                foreach(PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(neg, graph,
                        isSubpatternLike, true, null);
                }

                foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
                {
                    GenerateScheduledSearchPlans(idpt, graph,
                        isSubpatternLike, true, patternGraph.schedules[i]);
                }

                foreach(Alternative alt in patternGraph.alternativesPlusInlined)
                {
                    foreach(PatternGraph altCase in alt.alternativeCases)
                    {
                        GenerateScheduledSearchPlans(altCase, graph,
                            true, false, null);
                    }
                }

                foreach(Iterated iter in patternGraph.iteratedsPlusInlined)
                {
                    GenerateScheduledSearchPlans(iter.iteratedPattern, graph,
                        true, false, null);
                }
            }
        }

        public static void SetNeedForProfiling(PatternGraph patternGraph)
        {
            foreach(PatternCondition condition in patternGraph.Conditions)
            {
                SetNeedForProfiling(condition.ConditionExpression);
            }
            foreach(PatternYielding patternYielding in patternGraph.Yieldings)
            {
                foreach(Yielding yielding in patternYielding.ElementaryYieldings)
                {
                    SetNeedForProfiling(yielding);
                }
            }

            foreach(PatternGraph idpt in patternGraph.independentPatternGraphsPlusInlined)
            {
                SetNeedForProfiling(idpt);
            }
            foreach(PatternGraph neg in patternGraph.negativePatternGraphsPlusInlined)
            {
                SetNeedForProfiling(neg);
            }

            foreach(Alternative alt in patternGraph.alternativesPlusInlined)
            {
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    SetNeedForProfiling(altCase);
                }
            }
            foreach(Iterated iter in patternGraph.iteratedsPlusInlined)
            {
                SetNeedForProfiling(iter.iteratedPattern);
            }
        }

        /// <summary>
        /// Setup of compiler parameters for recompilation of actions at runtime taking care of analyze information
        /// </summary>
        public CompilerParameters GetDynCompilerSetup(String modelAssemblyLocation, String actionAssemblyLocation)
        {
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(BaseGraph)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPAction)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyLocation);
            compParams.ReferencedAssemblies.Add(actionAssemblyLocation);

            compParams.GenerateInMemory = true;
            compParams.CompilerOptions = "/optimize";
            return compParams;
        }

        /// <summary>
        /// Generates an LGSPAction object for the given scheduled search plan.
        /// </summary>
        /// <param name="scheduledSearchPlan">The scheduled search plan</param>
        /// <param name="action">Needed for the rule pattern and the name</param>
        /// <param name="modelAssemblyLocation">The location of the model assembly</param>
        /// <param name="actionAssemblyLocation">The location of the action assembly</param>
        /// <param name="sourceOutputFilename">null if no output file needed</param>
        public LGSPAction GenerateAction(ScheduledSearchPlan scheduledSearchPlan, LGSPAction action,
            String modelAssemblyLocation, String actionAssemblyLocation, String sourceOutputFilename)
        {
            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            GenerateFileHeaderForActionsFile(sourceCode, model.GetType().Namespace, action.rulePattern.GetType().Namespace);

            // can't generate new subpattern matchers due to missing scheduled search plans for them / missing graph analyze data
            Debug.Assert(action.rulePattern.patternGraph.embeddedGraphsPlusInlined.Length == 0);

            GenerateActionAndMatcher(sourceCode, action.rulePattern, false);

            // close namespace
            sourceCode.Append("}");

            // write generated source to file if requested
            if(sourceOutputFilename != null)
            {
                StreamWriter writer = new StreamWriter(sourceOutputFilename);
                writer.Write(sourceCode.ToString());
                writer.Close();
            }

            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = GetDynCompilerSetup(modelAssemblyLocation, actionAssemblyLocation);

//            Stopwatch compilerWatch = Stopwatch.StartNew();

            // compile generated code
            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                {
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                }
                throw new ArgumentException("Illegal dynamic C# source code produced for actions \"" + action.Name + "\": " + errorMsg);
            }

            // create action instance
            object obj = compResults.CompiledAssembly.CreateInstance("de.unika.ipd.grGen.lgspActions.DynAction_" + action.Name);

//            long compSourceTicks = compilerWatch.ElapsedTicks;
//            ConsoleUI.outWriter.WriteLine("GenMatcher: Compile source: {0} us", compSourceTicks / (Stopwatch.Frequency / 1000000));
            return (LGSPAction) obj;
        }

        /// <summary>
        /// Do the static search planning again so we can explain the search plan
        /// </summary>
        public void FillInStaticSearchPlans(LGSPGraphStatistics graphStatistics, bool inlineIndependents, params LGSPAction[] actions)
        {
            if(actions.Length == 0)
                throw new ArgumentException("No actions provided!");

            // use domain of dictionary as set with rulepatterns of the subpatterns of the actions, get them from pattern graph
            Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPatterns 
                = new Dictionary<LGSPMatchingPattern, LGSPMatchingPattern>();
            foreach(LGSPAction action in actions)
            {
                foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpattern 
                    in action.rulePattern.patternGraph.usedSubpatterns)
                {
                    subpatternMatchingPatterns[usedSubpattern.Key] = usedSubpattern.Value;
                }
            }

            // build search plans for the subpatterns
            foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPattern in subpatternMatchingPatterns)
            {
                LGSPMatchingPattern smp = subpatternMatchingPattern.Key;

                LGSPGrGen.GenerateScheduledSearchPlans(smp.patternGraph, graphStatistics, 
                    this, true, false, null);

                ScheduleEnricher.MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(smp.patternGraph, LazyNegativeIndependentConditionEvaluation);

                ScheduleEnricher.ParallelizeAsNeeded(smp);
            }

            // build search plans code for actions
            foreach(LGSPAction action in actions)
            {
                LGSPGrGen.GenerateScheduledSearchPlans(action.rulePattern.patternGraph, graphStatistics, 
                    this, false, false, null);

                ScheduleEnricher.MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(action.rulePattern.patternGraph, LazyNegativeIndependentConditionEvaluation);

                ScheduleEnricher.ParallelizeAsNeeded(action.rulePattern);
            }
        }

        /// <summary>
        /// Generate new actions for the given actions, doing the same work, 
        /// but hopefully faster by taking graph analysis information into account
        /// </summary>
        public LGSPAction[] GenerateActions(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, 
            params LGSPAction[] actions)
        {
            if(actions.Length == 0)
                throw new ArgumentException("No actions provided!");

            SourceBuilder sourceCode = new SourceBuilder(CommentSourceCode);
            GenerateFileHeaderForActionsFile(sourceCode, model.GetType().Namespace, actions[0].rulePattern.GetType().Namespace);

            // use domain of dictionary as set with rulepatterns of the subpatterns of the actions, get them from pattern graph
            Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPatterns 
                = new Dictionary<LGSPMatchingPattern, LGSPMatchingPattern>();
            foreach(LGSPAction action in actions)
            {
                foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpattern 
                    in action.rulePattern.patternGraph.usedSubpatterns)
                {
                    subpatternMatchingPatterns[usedSubpattern.Key] = usedSubpattern.Value;
                }
            }

            // generate code for subpatterns
            foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> subpatternMatchingPattern in subpatternMatchingPatterns)
            {
                LGSPMatchingPattern smp = subpatternMatchingPattern.Key;

                GenerateScheduledSearchPlans(smp.patternGraph, graph, 
                    true, false, null);

                ScheduleEnricher.MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(smp.patternGraph, LazyNegativeIndependentConditionEvaluation);

                ScheduleEnricher.ParallelizeAsNeeded(smp);

                GenerateActionAndMatcher(sourceCode, smp, false);
            }

            // generate code for actions
            foreach(LGSPAction action in actions)
            {
                GenerateScheduledSearchPlans(action.rulePattern.patternGraph, graph,
                    false, false, null);

                ScheduleEnricher.MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules(action.rulePattern.patternGraph, LazyNegativeIndependentConditionEvaluation);

                ScheduleEnricher.ParallelizeAsNeeded(action.rulePattern);

                GenerateActionAndMatcher(sourceCode, action.rulePattern, false);
            }

            // close namespace
            sourceCode.Append("}");

            if(DumpDynSourceCode)
            {
                using(StreamWriter writer = new StreamWriter("dynamic_" + actions[0].Name + ".cs"))
                    writer.Write(sourceCode.ToString());
            }

            // set up compiler
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = GetDynCompilerSetup(modelAssemblyName, actionsAssemblyName);
 
            // compile generated code
            CompilerResults compResults = compiler.CompileAssemblyFromSource(compParams, sourceCode.ToString());
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                {
                    errorMsg += Environment.NewLine + "Line: " + error.Line + " - " + error.ErrorText;
                }
                throw new ArgumentException("Internal error: Illegal dynamic C# source code produced: " + errorMsg);
            }

            // create action instances
            LGSPAction[] newActions = new LGSPAction[actions.Length];
            for(int i = 0; i < actions.Length; i++)
            {
                newActions[i] = (LGSPAction) compResults.CompiledAssembly.CreateInstance(
                    "de.unika.ipd.grGen.lgspActions.DynAction_" + actions[i].Name);
                if(newActions[i] == null)
                {
                    throw new ArgumentException("Internal error: Generated assembly does not contain action '"
                        + actions[i].Name + "'!");
                }
            }
            return newActions;
        }

        /// <summary>
        /// Generate a new action for the given action, doing the same work, 
        /// but hopefully faster by taking graph analysis information into account
        /// </summary>
        public LGSPAction GenerateAction(LGSPGraph graph, String modelAssemblyName, String actionsAssemblyName, LGSPAction action)
        {
            return GenerateActions(graph, modelAssemblyName, actionsAssemblyName, action)[0];
        }
    }
}
