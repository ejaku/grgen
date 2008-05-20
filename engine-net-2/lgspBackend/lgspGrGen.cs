/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

//#define NO_EDGE_LOOKUP

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.lgsp
{
	public class LGSPXGRSInfo
	{
		public LGSPXGRSInfo(String[] parameters, String xgrs)
		{
			Parameters = parameters;
			XGRS = xgrs;
		}

		public String[] Parameters;
		public String XGRS;
	}
    
    public class LGSPGrGen
    {
        private Dictionary<String, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();
        private bool assemblyHandlerInstalled = false;

        /// <summary>
        /// Returns a string where all "wrong" directory separator chars are replaced by the ones used by the system 
        /// </summary>
        /// <param name="path">The original path string potentially with wrong chars</param>
        /// <returns>The corrected path string</returns>
        static String FixDirectorySeparators(String path)
        {
            if(Path.DirectorySeparatorChar != '\\')
                path = path.Replace('\\', Path.DirectorySeparatorChar);
            if(Path.DirectorySeparatorChar != '/')
                path = path.Replace('/', Path.DirectorySeparatorChar);
            return path;
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly;
            loadedAssemblies.TryGetValue(args.Name, out assembly);
            return assembly;
        }

        void AddAssembly(Assembly assembly)
        {
            loadedAssemblies.Add(assembly.FullName, assembly);
            if(!assemblyHandlerInstalled)
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                assemblyHandlerInstalled = true;
            }
        }

        bool ProcessModel(String modelFilename, String modelStubFilename, String destDir, ProcessSpecFlags flags,
            out Assembly modelAssembly, out String modelAssemblyName)
        {
            String modelName = Path.GetFileNameWithoutExtension(modelFilename);
            String modelExtension = Path.GetExtension(modelFilename);

            modelAssembly = null;
            modelAssemblyName = null;

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);

            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug" : "/optimize";
            compParams.OutputAssembly = destDir  + "lgsp-" + modelName + ".dll";

            CompilerResults compResults;
            try
            {
                if(modelStubFilename != null)
                    compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename, modelStubFilename);
                else                   
                    compResults = compiler.CompileAssemblyFromFile(compParams, modelFilename);
                if(compResults.Errors.HasErrors)
                {
                    Console.Error.WriteLine("Illegal model C# source code: " + compResults.Errors.Count + " Errors:");
                    foreach(CompilerError error in compResults.Errors)
                        Console.Error.WriteLine("Line: " + error.Line + " - " + error.ErrorText);
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Unable to compile model: {0}", ex.Message);
                return false;
            }

            modelAssembly = compResults.CompiledAssembly;
            modelAssemblyName = compParams.OutputAssembly;
            AddAssembly(modelAssembly);

            Console.WriteLine(" - Model assembly \"{0}\" generated.", modelAssemblyName);
            return true;
        }

        IGraphModel GetGraphModel(Assembly modelAssembly)
        {
            Type modelType = null;
            try
            {
                foreach(Type type in modelAssembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.GetInterface("IGraphModel") != null && type.GetInterface("IGraph") == null)
                    {
                        if(modelType != null)
                        {
                            Console.Error.WriteLine("The given model contains more than one IGraphModel implementation: '"
                                + modelType + "' and '" + type + "'");
                            return null;
                        }
                        modelType = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                Console.WriteLine(e);
            }
            if(modelType == null)
            {
                Console.Error.WriteLine("The given model does not contain an IGraphModel implementation!");
                return null;
            }

            return (IGraphModel) modelAssembly.CreateInstance(modelType.FullName);
        }

        /// <summary>
        /// Generate plan graph for given pattern graph with costs from initial static schedule handed in with graph elements.
        /// Plan graph contains nodes representing the pattern elements (nodes and edges)
        /// and edges representing the matching operations to get the elements by.
        /// Edges in plan graph are given in the nodes by incoming list, as needed for MSA computation.
        /// </summary>
        PlanGraph GenerateStaticPlanGraph(PatternGraph patternGraph, bool negPatternGraph, bool isSubpattern)
        {
            //
            // If you change this method, chances are high you also want to change GeneratePlanGraph in LGSPMatcherGenerator
            // todo: unify it with GeneratePlanGraph in LGSPMatcherGenerator
            //

            // Create root node
            // Create plan graph nodes for all pattern graph nodes and all pattern graph edges
            // Create "lookup" plan graph edge from root node to each plan graph node
            // Create "implicit source" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the source node of the pattern graph edge
            // Create "implicit target" plan graph edge from each plan graph node originating with a pattern edge 
            //     to the plan graph node created by the target node of the pattern graph edge
            // Create "incoming" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the incoming edges of the pattern node
            // Create "outgoing" plan graph edge from each plan graph node originating with a pattern node
            //     to a plan graph node created by one of the outgoing edges of the pattern node
            // Ensured: there's no plan graph edge with a preset element as target besides the lookup,
            //     so presets are only search operation sources

            PlanNode[] planNodes = new PlanNode[patternGraph.nodes.Length + patternGraph.edges.Length];
            List<PlanEdge> planEdges = new List<PlanEdge>(patternGraph.nodes.Length + 5 * patternGraph.edges.Length);   // upper bound for num of edges

            int nodesIndex = 0;

            // create plan nodes and lookup plan edges for all pattern graph nodes
            PlanNode planRoot = new PlanNode("root");
            for(int i = 0; i < patternGraph.nodes.Length; ++i)
            {
                PatternNode node = patternGraph.nodes[i];

                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if (node.PointOfDefinition == null)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.MaybePreset;
                }
                else if (node.PointOfDefinition != patternGraph)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = negPatternGraph ? SearchOperationType.NegPreset : SearchOperationType.SubPreset;
                }
                else
                {
                    cost = 2 * node.Cost + 10;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }
                planNodes[nodesIndex] = new PlanNode(node, i + 1, isPreset);
                PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                planEdges.Add(rootToNodePlanEdge);
                planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);

                node.TempPlanMapping = planNodes[nodesIndex];

                ++nodesIndex;
            }

            // create plan nodes and necessary plan edges for all pattern graph edges
            for(int i = 0; i < patternGraph.edges.Length; ++i)
            {
                PatternEdge edge = patternGraph.edges[i];

                float cost;
                bool isPreset;
                SearchOperationType searchOperationType;
                if (edge.PointOfDefinition == null)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = isSubpattern ? SearchOperationType.SubPreset : SearchOperationType.MaybePreset;
                }
                else if (edge.PointOfDefinition != patternGraph)
                {
                    cost = 0;
                    isPreset = true;
                    searchOperationType = negPatternGraph ? SearchOperationType.NegPreset : SearchOperationType.SubPreset;
                }
                else
                {
                    cost = 2 * edge.Cost + 10;
                    isPreset = false;
                    searchOperationType = SearchOperationType.Lookup;
                }

                planNodes[nodesIndex] = new PlanNode(edge, i + 1, isPreset,
                    patternGraph.GetSource(edge)!=null ? patternGraph.GetSource(edge).TempPlanMapping : null,
                    patternGraph.GetTarget(edge)!=null ? patternGraph.GetTarget(edge).TempPlanMapping : null);

#if NO_EDGE_LOOKUP
                if(isPreset)
                {
#endif
                    PlanEdge rootToNodePlanEdge = new PlanEdge(searchOperationType, planRoot, planNodes[nodesIndex], cost);
                    planEdges.Add(rootToNodePlanEdge);
                    planNodes[nodesIndex].IncomingEdges.Add(rootToNodePlanEdge);
#if NO_EDGE_LOOKUP
                }
#endif

                // only add implicit source operation if edge source is needed and the edge source is not a preset node
                if(patternGraph.GetSource(edge) != null && !patternGraph.GetSource(edge).TempPlanMapping.IsPreset)
                {
                    SearchOperationType operation = edge.fixedDirection ? 
                        SearchOperationType.ImplicitSource : SearchOperationType.Implicit;
                    PlanEdge implSrcPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetSource(edge).TempPlanMapping, 0);
                    planEdges.Add(implSrcPlanEdge);
                    patternGraph.GetSource(edge).TempPlanMapping.IncomingEdges.Add(implSrcPlanEdge);
                }
                // only add implicit target operation if edge target is needed and the edge target is not a preset node
                if(patternGraph.GetTarget(edge) != null && !patternGraph.GetTarget(edge).TempPlanMapping.IsPreset)
                {
                    SearchOperationType operation = edge.fixedDirection ?
                        SearchOperationType.ImplicitTarget : SearchOperationType.Implicit;
                    PlanEdge implTgtPlanEdge = new PlanEdge(operation, planNodes[nodesIndex],
                        patternGraph.GetTarget(edge).TempPlanMapping, 0);
                    planEdges.Add(implTgtPlanEdge);
                    patternGraph.GetTarget(edge).TempPlanMapping.IncomingEdges.Add(implTgtPlanEdge);
                }

                // edge must only be reachable from other nodes if it's not a preset
                if(!isPreset)
                {
                    // no outgoing if no source
                    if(patternGraph.GetSource(edge) != null)
                    {
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Outgoing : SearchOperationType.Incident;
                        PlanEdge outPlanEdge = new PlanEdge(operation, patternGraph.GetSource(edge).TempPlanMapping,
                            planNodes[nodesIndex], edge.Cost);
                        planEdges.Add(outPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(outPlanEdge);
                    }
                    // no incoming if no target
                    if(patternGraph.GetTarget(edge) != null)
                    {
                        SearchOperationType operation = edge.fixedDirection ?
                            SearchOperationType.Incoming: SearchOperationType.Incident;
                        PlanEdge inPlanEdge = new PlanEdge(operation, patternGraph.GetTarget(edge).TempPlanMapping,
                            planNodes[nodesIndex], edge.Cost);
                        planEdges.Add(inPlanEdge);
                        planNodes[nodesIndex].IncomingEdges.Add(inPlanEdge);
                    }
                }

                ++nodesIndex;
            }

            return new PlanGraph(planRoot, planNodes, planEdges.ToArray());
        }
       
        /// <summary>
        /// Generates scheduled search plans needed for matcher code generation for action compilation
        /// out of static schedule information given by rulePattern elements, 
        /// utilizing code of the lgsp matcher generator.
        /// The scheduled search plans are added to the main and the nested pattern graphs.
        /// </summary>
        protected void GenerateScheduledSearchPlans(PatternGraph patternGraph, LGSPMatcherGenerator matcherGen,
            bool isSubpattern, bool isNegative)
        {
            PlanGraph planGraph = GenerateStaticPlanGraph(patternGraph, isNegative, isSubpattern);
            matcherGen.MarkMinimumSpanningArborescence(planGraph, patternGraph.name);
            SearchPlanGraph searchPlanGraph = matcherGen.GenerateSearchPlanGraph(planGraph);
            ScheduledSearchPlan scheduledSearchPlan = matcherGen.ScheduleSearchPlan(
                searchPlanGraph, patternGraph, isNegative);
            matcherGen.AppendHomomorphyInformation(scheduledSearchPlan);
            patternGraph.Schedule = scheduledSearchPlan;

            foreach (PatternGraph neg in patternGraph.negativePatternGraphs)
            {
                GenerateScheduledSearchPlans(neg, matcherGen, isSubpattern, true);
            }

            foreach (Alternative alt in patternGraph.alternatives)
            {
                foreach (PatternGraph altCase in alt.alternativeCases)
                {
                    GenerateScheduledSearchPlans(altCase, matcherGen, isSubpattern, false);
                }
            }
        }

		int xgrsNextSequenceID = 0;
		Dictionary<Sequence, int> xgrsSequenceIDs = new Dictionary<Sequence, int>();
		Dictionary<String, object> xgrsVars = new Dictionary<string, object>();
		Dictionary<String, object> xgrsRules = new Dictionary<string, object>();

		void EmitElementVarIfNew(String varName, SourceBuilder source)
		{
			if(!xgrsVars.ContainsKey(varName))
			{
				xgrsVars.Add(varName, null);
				source.AppendFront("object var_" + varName + " = null;\n");
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

		void EmitNeededVars(Sequence seq, SourceBuilder source)
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
				case SequenceType.AssignSequenceResultToVar:
				{
					SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar) seq;
					EmitBoolVarIfNew(seqToVar.DestVar, source);
					EmitNeededVars(seqToVar.Seq, source);
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
						source.AppendFront("LGSPAction rule_");
						source.Append(ruleName);
						source.Append(" = actions.GetAction(\"" + ruleName + "\");\n");
					}
					foreach(String varName in seqRule.RuleObj.ParamVars)
						EmitElementVarIfNew(varName, source);
					foreach(String varName in seqRule.RuleObj.ReturnVars)
						EmitElementVarIfNew(varName, source);
					break;
				}

				default:
					foreach(Sequence childSeq in seq.Children)
						EmitNeededVars(childSeq, source);
					break;
			}
		}

		void EmitLazyOp(Sequence left, Sequence right, bool isOr, int seqID, SourceBuilder source)
		{
			EmitSequence(left, source);
			source.AppendFront("if(" + (isOr ? "res_" : "!res_") + xgrsSequenceIDs[left] + ") res_" + seqID + " = "
				+ (isOr ? "true;\n" : "false;\n"));
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
					source.AppendFront("LGSPMatches mat_" + seqID + " = rule_" + ruleObj.RuleName
						+ ".Match(graph, " + (seq.SequenceType == SequenceType.Rule ? "1" : "actions.MaxMatches"));
                    if(ruleObj.ParamVars.Length != 0)
                    {
                        source.Append(", new object[] {");
                        foreach(String paramName in ruleObj.ParamVars)
                            source.Append("var_" + paramName + ", ");
                        source.Append("}");
                    }
                    else source.Append(", null");
					source.Append(");\n");
                    source.AppendFront("graph.Matched(mat_" + seqID + ", " + specialStr + ");\n");
                    source.AppendFront("if(mat_" + seqID + ".Count == 0)\n");
                    source.AppendFront("\tres_" + seqID + " = false;\n");
                    source.AppendFront("else\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront("graph.Finishing(mat_" + seqID + ", " + specialStr + ");\n");
                    source.AppendFront("object[] ret_" + seqID + " = ");
                    if(seq.SequenceType == SequenceType.Rule)
                        source.Append("rule_" + ruleObj.RuleName + ".Modify(graph, mat_" + seqID + ".matchesList.First);\n");
                    else
                        source.Append("actions.Replace(mat_" + seqID + ", -1, null);\n");
                    source.AppendFront("graph.Finished(mat_" + seqID + ", " + specialStr + ");\n");

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
					source.AppendFront("res_" + seqID + " = ret_" + seqID + " != null;\n");
                    source.Unindent(); ;
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
				{
					SequenceBinary seqBin = (SequenceBinary) seq;
					bool isOr = seq.SequenceType == SequenceType.LazyOr;
					if(seqBin.Randomize)
					{
						source.AppendFront("if(Sequence.randomGenerator.Next(2) == 1)\n");
						source.AppendFront("{\n");
						source.Indent();
						EmitLazyOp(seqBin.Right, seqBin.Left, isOr, seqID, source);
						source.Unindent();
						source.AppendFront("}\n");
						source.AppendFront("else\n");
						source.AppendFront("{\n");
						EmitLazyOp(seqBin.Left, seqBin.Right, isOr, seqID, source);
						source.Unindent();
						source.AppendFront("}\n");
					}
					else
					{
						EmitLazyOp(seqBin.Left, seqBin.Right, isOr, seqID, source);
					}
					break;
				}

				case SequenceType.StrictAnd:
				case SequenceType.StrictOr:
				case SequenceType.Xor:
				{
					SequenceBinary seqBin = (SequenceBinary) seq;
					if(seqBin.Randomize)
					{
						source.AppendFront("if(Sequence.randomGenerator.Next(2) == 1)\n");
						source.AppendFront("{\n");
						source.Indent();
						EmitSequence(seqBin.Right, source);
						EmitSequence(seqBin.Left, source);
						source.Unindent();
						source.AppendFront("}\n");
						source.AppendFront("else\n");
						source.AppendFront("{\n");
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
					SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar) seq;
					int seqSubID = xgrsSequenceIDs[seqToVar.Seq];
					EmitSequence(seqToVar.Seq, source);
					source.AppendFront("res_" + seqID + " = varbool_" + seqToVar.DestVar
						+ " = res_" + seqSubID + ";\n");
					break;
				}

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

		public bool GenerateXGRSCode(String xgrsName, String xgrsStr, String[] paramNames, SourceBuilder source)
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

			source.AppendFront("public static bool ApplyXGRS_" + xgrsName + "(LGSPGraph graph");
			for(int i = 0; i < paramNames.Length; i++)
			{
				source.Append(", IGraphElement var_");
				source.Append(paramNames[i]);
			}
			source.Append(")\n");
			source.AppendFront("{\n");
			source.Indent();

			source.AppendFront("LGSPActions actions = graph.curActions;\n");

			xgrsVars.Clear();
			xgrsNextSequenceID = 0;
			xgrsSequenceIDs.Clear();
			xgrsRules.Clear();

			foreach(String param in paramNames)
				xgrsVars.Add(param, null);
			EmitNeededVars(seq, source);

			EmitSequence(seq, source);
			source.AppendFront("return res_" + xgrsSequenceIDs[seq] + ";\n");
			source.Unindent();
			source.AppendFront("}\n");

			return true;
		}

        public static bool ExecuteGrGenJava(String tmpDir, out List<String> genModelFiles, out List<String> genModelStubFiles,
                out List<String> genActionsFiles, params String[] sourceFiles)
        {
            genModelFiles = new List<string>();
            genModelStubFiles = new List<string>();
            genActionsFiles = new List<string>();

            if(sourceFiles.Length == 0)
            {
                Console.Error.WriteLine("No GrGen.NET source files specified!");
                return false;
            }

            String binPath = FixDirectorySeparators(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    + Path.DirectorySeparatorChar;

            try
            {
                String javaString;
                if(Environment.OSVersion.Platform == PlatformID.Unix) javaString = "java";
                else javaString = "javaw";

                ProcessStartInfo startInfo = new ProcessStartInfo(javaString, "-Xmx1024M -jar \"" + binPath + "grgen.jar\" "
                    + "-b de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2 "
                    + "-c " + tmpDir + Path.DirectorySeparatorChar + "printOutput.txt -o " + tmpDir
                    + " \"" + String.Join("\" \"", sourceFiles) + "\"");
                startInfo.CreateNoWindow = true;
                Process grGenJava = Process.Start(startInfo);
                grGenJava.WaitForExit();
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("Unable to process specification: " + e.Message);
                return false;
            }

            bool noError = true;
            using(StreamReader sr = new StreamReader(tmpDir + Path.DirectorySeparatorChar + "printOutput.txt"))
            {
                String frontStr = "  generating the ";
                String backStr = " file...";
                String frontStubStr = "  writing the ";
                String backStubStr = " stub file...";

                String line;
                while((line = sr.ReadLine()) != null)
                {
                    if(line.Contains("ERROR"))
                    {
                        Console.Error.WriteLine(line);
                        noError = false;
                        continue;
                    }
                    if(line.Contains("WARNING"))
                    {
                        Console.Error.WriteLine(line);
                        continue;
                    }
                    if(line.StartsWith(frontStr) && line.EndsWith(backStr))
                    {
                        String filename = line.Substring(frontStr.Length, line.Length - frontStr.Length - backStr.Length);
                        if(filename.EndsWith("Model.cs"))
                            genModelFiles.Add(tmpDir + Path.DirectorySeparatorChar + filename);
                        else if(filename.EndsWith("Actions_intermediate.cs"))
                            genActionsFiles.Add(tmpDir + Path.DirectorySeparatorChar + filename);
                    }
                    else if(line.StartsWith(frontStubStr) && line.EndsWith(backStubStr))
                    {
                        String filename = line.Substring(frontStubStr.Length, line.Length - frontStubStr.Length - backStubStr.Length);
                        if(filename.EndsWith("ModelStub.cs"))
                            genModelStubFiles.Add(tmpDir + Path.DirectorySeparatorChar + filename);
                    }
                }
            }

            return noError;
        }

        enum ErrorType { NoError, GrGenJavaError, GrGenNetError };

        ErrorType ProcessSpecificationImpl(String specFile, String destDir, String tmpDir, ProcessSpecFlags flags)
        {
            Console.WriteLine("Building libraries...");

            ///////////////////////////////////////////////
            // use java frontend to build the model and intermediate action source files
            ///////////////////////////////////////////////

            String modelFilename = null;
            String modelStubFilename = null;
            String actionsFilename = null;

            if((flags & ProcessSpecFlags.UseExistingMask) == ProcessSpecFlags.UseNoExistingFiles)
            {
                List<String> genModelFiles, genModelStubFiles, genActionsFiles;

                if(!ExecuteGrGenJava(tmpDir, out genModelFiles, out genModelStubFiles,
                        out genActionsFiles, specFile))
                    return ErrorType.GrGenJavaError;

                if(genModelFiles.Count == 1) modelFilename = genModelFiles[0];
                else if(genModelFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple models are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genModelStubFiles.Count == 1) modelStubFilename = genModelStubFiles[0];
                else if(genModelStubFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple model stubs are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }

                if(genActionsFiles.Count == 1) actionsFilename = genActionsFiles[0];
                else if(genActionsFiles.Count > 1)
                {
                    Console.Error.WriteLine("Multiple action sets are not supported by ProcessSpecification, yet!");
                    return ErrorType.GrGenNetError;
                }
            }
            else
            {
                String[] producedFiles = Directory.GetFiles(tmpDir);
                foreach(String file in producedFiles)
                {
                    if(file.EndsWith("Model.cs"))
                    {
                        if(modelFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(modelFilename))
                            modelFilename = file;
                    }
                    else if(file.EndsWith("Actions_intermediate.cs"))
                    {
                        if(actionsFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(actionsFilename))
                            actionsFilename = file;
                    }
                    else if(file.EndsWith("ModelStub.cs"))
                    {
                        if(modelStubFilename == null || File.GetLastWriteTime(file) > File.GetLastWriteTime(modelStubFilename))
                            modelStubFilename = file;
                    }
                }
            }

            if(modelFilename == null || actionsFilename == null)
            {
                Console.Error.WriteLine("Not all required files have been generated!");
                return ErrorType.GrGenJavaError;
            }

            ///////////////////////////////////////////////
            // compile the model and intermediate action files generated by the java frontend
            // to gain access via reflection to their content needed for matcher code generation
            ///////////////////////////////////////////////

            Assembly modelAssembly;
            String modelAssemblyName;
            if(!ProcessModel(modelFilename, modelStubFilename, destDir, flags, out modelAssembly, out modelAssemblyName))
                return ErrorType.GrGenNetError;

            IGraphModel model = GetGraphModel(modelAssembly);
            if(model == null) return ErrorType.GrGenNetError;

            if((flags & ProcessSpecFlags.NoProcessActions) != 0) return ErrorType.NoError;

            String actionsName = Path.GetFileNameWithoutExtension(actionsFilename);
            actionsName = actionsName.Substring(0, actionsName.Length - 13);    // remove "_intermediate" suffix
            String actionsOutputFilename = tmpDir + Path.DirectorySeparatorChar + actionsName + ".cs";

            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters compParams = new CompilerParameters();
            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
            compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);
            compParams.ReferencedAssemblies.Add(modelAssemblyName);

            String actionsOutputSource;
            if((flags & ProcessSpecFlags.UseExistingMask) != ProcessSpecFlags.UseAllGeneratedFiles)
            {
                compParams.GenerateInMemory = true;
                compParams.CompilerOptions = "/optimize /d:INITIAL_WARMUP";

                CompilerResults compResultsWarmup;
                try
                {
                    compResultsWarmup = compiler.CompileAssemblyFromFile(compParams, actionsFilename);
                    if(compResultsWarmup.Errors.HasErrors)
                    {
                        String errorMsg = compResultsWarmup.Errors.Count + " Errors:";
                        foreach(CompilerError error in compResultsWarmup.Errors)
                            errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                        Console.Error.WriteLine("Illegal actions C# input source code: " + errorMsg);
                        return ErrorType.GrGenNetError;
                    }
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine("Unable to compile initial actions: {0}", ex.Message);
                    return ErrorType.GrGenNetError;
                }

                Assembly initialAssembly = compResultsWarmup.CompiledAssembly;

                Dictionary<String, Type> actionTypes = new Dictionary<string, Type>();

                foreach(Type type in initialAssembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.BaseType == typeof(LGSPMatchingPattern) || type.BaseType == typeof(LGSPRulePattern))
                        actionTypes.Add(type.Name, type);
                }

                ///////////////////////////////////////////////
                // take action intermediate file until action insertion point as base for action file 
                ///////////////////////////////////////////////

                SourceBuilder source = new SourceBuilder((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0);
                source.Indent();
                source.Indent();
                bool actionPointFound = false;
                String actionsNamespace = null;
                using(StreamReader sr = new StreamReader(actionsFilename))
                {
                    String line;
                    while(!actionPointFound && (line = sr.ReadLine()) != null)
                    {
                        if(actionsNamespace == null && line.StartsWith("namespace "))
                        {
                            actionsNamespace = line.Substring("namespace ".Length);
                            source.Append(line);
                            source.Append("\n");
                        }
                        else if(line.Length > 0 && line[0] == '#' && line.Contains("// GrGen emit statement section"))
                        {
                            int lastSpace = line.LastIndexOf(' ');
                            String ruleName = line.Substring(lastSpace + 1);
                            Type ruleType = actionTypes[ruleName];
                            int xgrsID = 0;
                            while(true)
                            {
                                FieldInfo fieldInfo = ruleType.GetField("XGRSInfo_" + xgrsID);
                                if(fieldInfo == null) break;
                                LGSPXGRSInfo xgrsInfo = (LGSPXGRSInfo) fieldInfo.GetValue(null);
                                if(!GenerateXGRSCode(xgrsID.ToString(), xgrsInfo.XGRS,
                                        xgrsInfo.Parameters, source))
                                    return ErrorType.GrGenNetError;
                                xgrsID++;
                            }
                            while((line = sr.ReadLine()) != null)
                            {
                                if(line.StartsWith("#"))
                                    break;
                            }
                        }
                        else if(line.Length > 0 && line[0] == '/' && line.StartsWith("// GrGen insert Actions here"))
                        {
                            actionPointFound = true;
                            break;
                        }
                        else
                        {
                            source.Append(line);
                            source.Append("\n");
                        }
                    }
                }

                if(!actionPointFound)
                {
                    Console.Error.WriteLine("Illegal actions C# input source code: Actions insertion point not found!");
                    return ErrorType.GrGenJavaError;
                }

                source.Unindent();
                source.Append("\n");

                ///////////////////////////////////////////////
                // generate and insert the matcher source code into the action file
                // already filled with the content of the action intermediate file until the action insertion point
                ///////////////////////////////////////////////

                LGSPMatcherGenerator matcherGen = new LGSPMatcherGenerator(model);
                if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0) matcherGen.CommentSourceCode = true;

                String unitName;
                int lastDot = actionsNamespace.LastIndexOf(".");
                if(lastDot == -1) unitName = "";
                else unitName = actionsNamespace.Substring(lastDot + 8);  // skip ".Action_"

                SourceBuilder endSource = new SourceBuilder("\n");
                endSource.Indent();
                endSource.AppendFront("public class " + unitName + "Actions : LGSPActions\n");
                endSource.AppendFront("{\n");
                endSource.Indent();
                endSource.AppendFront("public " + unitName + "Actions(LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)\n");
                endSource.AppendFront("    : base(lgspgraph, modelAsmName, actionsAsmName)\n");
                endSource.AppendFront("{\n");
                endSource.AppendFront("    InitActions();\n");
                endSource.AppendFront("}\n\n");
                endSource.AppendFront("public " + unitName + "Actions(LGSPGraph lgspgraph)\n");
                endSource.AppendFront("    : base(lgspgraph)\n");
                endSource.AppendFront("{\n");
                endSource.AppendFront("    InitActions();\n");
                endSource.AppendFront("}\n\n");
                endSource.AppendFront("private void InitActions()\n");
                endSource.AppendFront("{\n");
                endSource.Indent();

                foreach(Type type in initialAssembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.BaseType == typeof(LGSPMatchingPattern) || type.BaseType == typeof(LGSPRulePattern))
                    {
                        LGSPMatchingPattern matchingPattern = (LGSPMatchingPattern) initialAssembly.CreateInstance(type.FullName);
                        matchingPattern.initialize();

                        GenerateScheduledSearchPlans(matchingPattern.patternGraph, matcherGen, !(matchingPattern is LGSPRulePattern), false);

                        matcherGen.MergeNegativeSchedulesIntoPositiveSchedules(matchingPattern.patternGraph);

                        matcherGen.GenerateMatcherSourceCode(source, matchingPattern, true);

                        if(matchingPattern is LGSPRulePattern) // normal rule
                        {
                            endSource.AppendFrontFormat("actions.Add(\"{0}\", (LGSPAction) Action_{0}.Instance);\n", matchingPattern.name);
                        }
                    }
                }
                endSource.Unindent();
                endSource.AppendFront("}\n\n");
                endSource.AppendFront("public override String Name { get { return \"" + actionsName + "\"; } }\n");
                endSource.AppendFront("public override String ModelMD5Hash { get { return \"" + model.MD5Hash + "\"; } }\n");
                endSource.Unindent();
                endSource.AppendFront("}\n");
                source.Append(endSource.ToString());
                source.Append("}");

                actionsOutputSource = source.ToString();

                if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
                {
                    StreamWriter writer = new StreamWriter(actionsOutputFilename);
                    writer.Write(actionsOutputSource);
                    writer.Close();
                }
            }
            else
            {
                try
                {
                    using(StreamReader reader = new StreamReader(actionsOutputFilename))
                        actionsOutputSource = reader.ReadToEnd();
                }
                catch(Exception)
                {
                    Console.Error.WriteLine("Unable to read from file \"" + actionsOutputFilename + "\"!");
                    return ErrorType.GrGenNetError;
                }
            }

            if((flags & ProcessSpecFlags.NoCreateActionsAssembly) != 0) return ErrorType.NoError;

            ///////////////////////////////////////////////
            // finally compile the action source file into action assembly
            ///////////////////////////////////////////////
            // action source file was built this way:
            // the rule pattern code was copied from the action intermediate file, 
            // action code was appended by matcher generation,
            // which needed access to the rule pattern objects, 
            // given via reflection of the compiled action intermediate file)
            ///////////////////////////////////////////////

            compParams.GenerateInMemory = false;
            compParams.IncludeDebugInformation = (flags & ProcessSpecFlags.CompileWithDebug) != 0;
            compParams.CompilerOptions = (flags & ProcessSpecFlags.CompileWithDebug) != 0 ? "/debug" : "/optimize";
            compParams.OutputAssembly = destDir + "lgsp-" + actionsName + ".dll";

            CompilerResults compResults;
            if((flags & ProcessSpecFlags.KeepGeneratedFiles) != 0)
            {
                compResults = compiler.CompileAssemblyFromFile(compParams, actionsOutputFilename);
            }
            else
            {
                compResults = compiler.CompileAssemblyFromSource(compParams, actionsOutputSource);
            }
            if(compResults.Errors.HasErrors)
            {
                String errorMsg = compResults.Errors.Count + " Errors:";
                foreach(CompilerError error in compResults.Errors)
                    errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                Console.Error.WriteLine("Illegal generated actions C# source code: " + errorMsg);
                return ErrorType.GrGenNetError;
            }

            Console.WriteLine(" - Actions assembly \"{0}\" generated.", compParams.OutputAssembly);
            return ErrorType.NoError;
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        /// <param name="destDir">The directory, where the generated libraries are to be placed.</param>
        /// <param name="intermediateDir">A directory, where intermediate files can be placed.</param>
        /// <param name="flags">Specifies how the specification is to be processed.</param>
        /// <exception cref="System.Exception">Thrown, when an error occurred.</exception>
        public static void ProcessSpecification(String specPath, String destDir, String intermediateDir, ProcessSpecFlags flags)
        {
            ErrorType ret;
            try
            {
                ret = new LGSPGrGen().ProcessSpecificationImpl(specPath, destDir, intermediateDir, flags);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            if(ret != ErrorType.NoError)
            {
                if(ret == ErrorType.GrGenJavaError && File.Exists(intermediateDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                {
                    using(StreamReader sr = new StreamReader(intermediateDir + Path.DirectorySeparatorChar + "printOutput.txt"))
                        throw new Exception("Error while processing specification:\n" + sr.ReadToEnd());
                }
                else throw new Exception("Error while processing specification!");
            }
        }

        /// <summary>
        /// Processes the given rule specification file and generates a model and actions library in the same directory as the specification file.
        /// </summary>
        /// <param name="specPath">The path to the rule specification file (.grg).</param>
        public static void ProcessSpecification(String specPath)
        {
            specPath = FixDirectorySeparators(specPath);

            String specDir;
            int index = specPath.LastIndexOf(Path.DirectorySeparatorChar);
            if(index == -1)
                specDir = "";
            else
            {
                specDir = specPath.Substring(0, index + 1);
                if(!Directory.Exists(specDir))
                    throw new Exception("Something is wrong with the directory of the specification file:\n\"" + specDir + "\" does not exist!");
            }

            String dirname;
            int id = 0;
            do
            {
                dirname = specDir + "tmpgrgen" + id + "";
                id++;
            }
            while(Directory.Exists(dirname));
            try
            {
                Directory.CreateDirectory(dirname);
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to create temporary directory \"" + dirname + "\"!", ex);
            }

            try
            {
                ProcessSpecification(specPath, specDir, dirname, ProcessSpecFlags.UseNoExistingFiles);
            }
            finally
            {
                Directory.Delete(dirname, true);
            }
        }
    }
}
