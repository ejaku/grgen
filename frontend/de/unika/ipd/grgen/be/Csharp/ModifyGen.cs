using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Generates the rewrite part of the actions file for the SearchPlanBackend2 backend.
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using CopyKind = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode.CopyKind;
	using de.unika.ipd.grgen.ir;
	using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using MatchType = de.unika.ipd.grgen.ir.type.MatchType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;
	using SetType = de.unika.ipd.grgen.ir.type.container.SetType;
	using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using GraphEntityExpression = de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using Alternative = de.unika.ipd.grgen.ir.pattern.Alternative;
	using AlternativeReplacement = de.unika.ipd.grgen.ir.pattern.AlternativeReplacement;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using IteratedReplacement = de.unika.ipd.grgen.ir.pattern.IteratedReplacement;
	using NameOrAttributeInitialization = de.unika.ipd.grgen.ir.pattern.NameOrAttributeInitialization;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using OrderedReplacement = de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
	using OrderedReplacements = de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
	using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
	using PatternGraphRhsFromLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhsFromLhs;
	using RetypedEdge = de.unika.ipd.grgen.ir.pattern.RetypedEdge;
	using RetypedNode = de.unika.ipd.grgen.ir.pattern.RetypedNode;
	using SubpatternDependentReplacement = de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
	using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	public class ModifyGen : CSharpBase
	{
		internal readonly IList<Entity> emptyParameters = new List<Entity>();
		internal readonly IList<Expression> emptyReturns = new List<Expression>();
		internal readonly ICollection<EvalStatements> emptyEvals = new List<EvalStatements>();

		internal Model model;
		internal SearchPlanBackend2 be;

		public ModifyGen(SearchPlanBackend2 backend, string nodeTypePrefix, string edgeTypePrefix, string objectTypePrefix, string internalObjectTypePrefix)
			: base(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, internalObjectTypePrefix)
		{
			be = backend;
			model = be.unit.ActionsGraphModel;
		}

		//////////////////////////////////
		// Modification part generation //
		//////////////////////////////////

		public virtual void GenModify(SourceBuilder sb, Rule rule, string packageName, bool isSubpattern)
		{
			GenModify(sb, rule, packageName, "", "pat_" + rule.Left.NameOfGraph, isSubpattern);
		}

		private void GenModify(SourceBuilder sb, Rule rule, string packageName, string pathPrefix, string patGraphVarName,
				bool isSubpattern)
		{
			if(rule.Right != null)
			{ // rule / subpattern with dependent replacement
				// replace left by right, normal version
				ModifyGenerationTask task = new ModifyGenerationTask();
				task.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_MODIFY;
				task.left = rule.Left;
				task.right = rule.Right;
				task.parameters = rule.Parameters;
				task.evals = rule.Evals;
				task.replParameters = rule.Right.ReplParameters;
				task.returns = rule.Returns;
				task.isSubpattern = isSubpattern;
				task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
				GenModifyRuleOrSubrule(sb, task, packageName, pathPrefix);
			}
			else if(!isSubpattern)
			{ // test
				// keep left unchanged, normal version
				ModifyGenerationTask task = new ModifyGenerationTask();
				task.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_MODIFY;
				task.left = rule.Left;
				task.right = new PatternGraphRhsFromLhs(rule.Left);
				task.parameters = rule.Parameters;
				task.evals = rule.Evals;
				task.replParameters = emptyParameters;
				task.returns = rule.Returns;
				task.isSubpattern = false;
				task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
				GenModifyRuleOrSubrule(sb, task, packageName, pathPrefix);
			}

			if(isSubpattern)
			{
				if(pathPrefix.Length == 0
						&& !HasAbstractElements(rule.Left)
						&& !HasDanglingEdges(rule.Left))
				{
					// create subpattern into pattern
					ModifyGenerationTask creationTask = new ModifyGenerationTask();
					creationTask.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_CREATION;
					creationTask.left = new PatternGraphLhs(rule.Left.NameOfGraph, 0); // empty graph
					creationTask.left.DirectlyNestingLHSGraph = creationTask.left;
					creationTask.right = new PatternGraphRhsFromLhs(rule.Left);
					creationTask.parameters = rule.Parameters;
					creationTask.evals = emptyEvals;
					creationTask.replParameters = emptyParameters;
					creationTask.returns = emptyReturns;
					creationTask.isSubpattern = true;
					creationTask.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
					foreach(Entity entity in creationTask.parameters)
					{ // add connections to empty graph so that they stay unchanged
						if(entity is Node)
						{
							Node node = (Node)entity;
							creationTask.left.AddSingleNode(node);
						}
						else if(entity is Edge)
						{
							Edge edge = (Edge)entity;
							creationTask.left.AddSingleEdge(edge);
						}
					}
					GenModifyRuleOrSubrule(sb, creationTask, packageName, pathPrefix);
				}

				// delete subpattern from pattern
				ModifyGenerationTask deletionTask = new ModifyGenerationTask();
				deletionTask.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_DELETION;
				deletionTask.left = rule.Left;
				deletionTask.right = new PatternGraphRhsFromLhs(new PatternGraphLhs(rule.Left.NameOfGraph, 0)); // empty graph
				deletionTask.right.DirectlyNestingLHSGraph = deletionTask.left;
				deletionTask.parameters = rule.Parameters;
				deletionTask.evals = emptyEvals;
				deletionTask.replParameters = emptyParameters;
				deletionTask.returns = emptyReturns;
				deletionTask.isSubpattern = true;
				deletionTask.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
				foreach(Entity entity in deletionTask.parameters)
				{ // add connections to empty graph so that they stay unchanged
					if(entity is Node)
					{
						Node node = (Node)entity;
						deletionTask.right.AddSingleNode(node);
					}
					else if(entity is Edge)
					{
						Edge edge = (Edge)entity;
						deletionTask.right.AddSingleEdge(edge);
					}
				}
				GenModifyRuleOrSubrule(sb, deletionTask, packageName, pathPrefix);
			}

			foreach(Alternative alt in rule.Left.Alts)
			{
				string altName = alt.NameOfGraph;

				GenModifyAlternative(sb, rule, alt, pathPrefix + rule.Left.NameOfGraph + "_", altName,
						isSubpattern);

				foreach(Rule altCase in alt.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					string altCasePatGraphVarName = pathPrefix + rule.Left.NameOfGraph + "_" + altName + "_"
							+ altCasePattern.NameOfGraph;
					GenModify(sb, altCase, packageName, pathPrefix + rule.Left.NameOfGraph + "_" + altName + "_",
							altCasePatGraphVarName, isSubpattern);
				}
			}

			foreach(Rule iter in rule.Left.Iters)
			{
				string iterName = iter.Left.NameOfGraph;
				string iterPatGraphVarName = pathPrefix + rule.Left.NameOfGraph + "_" + iterName;
				GenModifyIterated(sb, iter, pathPrefix + rule.Left.NameOfGraph + "_", iterName, isSubpattern);
				GenModify(sb, iter, packageName, pathPrefix + rule.Left.NameOfGraph + "_", iterPatGraphVarName,
						isSubpattern);
			}
		}

		/// <summary>
		/// Checks whether the given pattern contains abstract elements.
		/// </summary>
		private static bool HasAbstractElements(PatternGraphLhs left)
		{
			foreach(Node node in left.Nodes)
			{
				if(node.NodeType.IsAbstract())
					return true;
			}

			foreach(Edge edge in left.Edges)
			{
				if(edge.EdgeType.IsAbstract())
					return true;
			}

			return false;
		}

		private static bool HasDanglingEdges(PatternGraphLhs left)
		{
			foreach(Edge edge in left.Edges)
			{
				if(left.GetSource(edge) == null || left.GetTarget(edge) == null)
					return true;
			}

			return false;
		}

		private void GenModifyAlternative(SourceBuilder sb, Rule rule, Alternative alt,
				string pathPrefix, string altName, bool isSubpattern)
		{
			if(rule.Right != null) // generate code for dependent modify dispatcher
				GenModifyAlternativeModify(sb, alt, pathPrefix, altName, isSubpattern);

			if(isSubpattern) // generate for delete alternative dispatcher
				GenModifyAlternativeDelete(sb, alt, pathPrefix, altName, isSubpattern);
		}

		private void GenModifyAlternativeModify(SourceBuilder sb, Alternative alt, string pathPrefix, string altName,
				bool isSubpattern)
		{
			// Emit function header
			sb.Append("\n");
			sb.AppendFront("public void "
					+ pathPrefix + altName + "_Modify"
					+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, IMatch_" + pathPrefix + altName + " curMatch");
			IList<Entity> replParameters = new List<Entity>();
			GetUnionOfReplaceParametersOfAlternativeCases(alt, replParameters);
			foreach(Entity entity in replParameters)
			{
				if(entity is Node)
				{
					Node node = (Node)entity;
					sb.Append(", ");
					if(entity.IsDefToBeYieldedTo())
						sb.Append("ref ");
					sb.Append("GRGEN_LGSP.LGSPNode " + FormatEntity(node));
				}
				else
				{
					Variable var = (Variable)entity;
					sb.Append(", ");
					if(entity.IsDefToBeYieldedTo())
						sb.Append("ref ");
					sb.Append(FormatAttributeType(var) + " " + FormatEntity(var));
				}
			}
			sb.Append(")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			// Emit dispatcher calling the modify-method of the alternative case which was matched
			bool firstCase = true;
			foreach(Rule altCase in alt.AlternativeCases)
			{
				PatternGraphLhs pattern = altCase.Pattern;
				if(firstCase)
				{
					sb.AppendFront("if(curMatch.Pattern == "
							+ pathPrefix + altName + "_" + pattern.NameOfGraph + ") {\n");
					firstCase = false;
				}
				else
				{
					sb.AppendFront("else if(curMatch.Pattern == "
							+ pathPrefix + altName + "_" + pattern.NameOfGraph + ") {\n");
				}
				sb.Indent();
				sb.AppendFront(pathPrefix + altName + "_" + pattern.NameOfGraph + "_Modify"
						+ "(actionEnv, (Match_" + pathPrefix + altName + "_" + pattern.NameOfGraph
						+ ")curMatch");
				replParameters = altCase.Right.ReplParameters;
				foreach(Entity entity in replParameters)
				{
					sb.Append(", ");
					if(entity.IsDefToBeYieldedTo())
						sb.Append("ref ");
					sb.Append(FormatEntity(entity));
				}
				sb.Append(");\n");
				sb.AppendFront("return;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			sb.AppendFront("throw new ApplicationException(); //debug assert\n");

			// Emit end of function
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private static void GetUnionOfReplaceParametersOfAlternativeCases(Alternative alt, ICollection<Entity> replaceParameters)
		{
			foreach(Rule altCase in alt.AlternativeCases)
			{
				IList<Entity> replParams = altCase.Right.ReplParameters;
				foreach(Entity entity in replParams)
				{
					if(!replaceParameters.Contains(entity))
						replaceParameters.Add(entity);
				}
			}
		}

		private static void GenModifyAlternativeDelete(SourceBuilder sb, Alternative alt, string pathPrefix, string altName,
				bool isSubpattern)
		{
			// Emit function header
			sb.Append("\n");
			sb.AppendFront("public void " + pathPrefix + altName + "_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
							+ "IMatch_" + pathPrefix + altName + " curMatch)\n");
			sb.AppendFront("{\n");
			sb.Indent();

			// Emit dispatcher calling the delete-method of the alternative case which was matched
			bool firstCase = true;
			foreach(Rule altCase in alt.AlternativeCases)
			{
				PatternGraphLhs pattern = altCase.Pattern;
				if(firstCase)
				{
					sb.AppendFront("if(curMatch.Pattern == "
							+ pathPrefix + altName + "_" + pattern.NameOfGraph + ") {\n");
					firstCase = false;
				}
				else
				{
					sb.AppendFront("else if(curMatch.Pattern == "
							+ pathPrefix + altName + "_" + pattern.NameOfGraph + ") {\n");
				}
				sb.Indent();
				sb.AppendFront(pathPrefix + altName + "_" + pattern.NameOfGraph + "_"
						+ "Delete(actionEnv, (Match_" + pathPrefix + altName + "_" + pattern.NameOfGraph
						+ ")curMatch);\n");
				sb.AppendFront("return;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			sb.AppendFront("throw new ApplicationException(); //debug assert\n");

			// Emit end of function
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenModifyIterated(SourceBuilder sb, Rule rule, string pathPrefix, string iterName,
				bool isSubpattern)
		{
			if(rule.Right != null) // generate code for dependent modify dispatcher
				GenModifyIteratedModify(sb, rule, pathPrefix, iterName, isSubpattern);

			if(isSubpattern) // generate for delete iterated dispatcher
				GenModifyIteratedDelete(sb, rule, pathPrefix, iterName, isSubpattern);
		}

		private void GenModifyIteratedModify(SourceBuilder sb, Rule iter, string pathPrefix, string iterName,
				bool isSubpattern)
		{
			// Emit function header
			sb.Append("\n");
			sb.AppendFront("public void " + pathPrefix + iterName + "_Modify"
					+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
					+ "GRGEN_LGSP.LGSPMatchesList<Match_" + pathPrefix + iterName
					+ ", IMatch_" + pathPrefix + iterName + "> curMatches");
			IList<Entity> replParameters = iter.Right.ReplParameters;
			foreach(Entity entity in replParameters)
			{
				if(entity is Node)
				{
					Node node = (Node)entity;
					sb.Append(", ");
					if(entity.IsDefToBeYieldedTo())
						sb.Append("ref ");
					sb.Append("GRGEN_LGSP.LGSPNode " + FormatEntity(node));
				}
				else
				{
					Variable var = (Variable)entity;
					sb.Append(", ");
					if(entity.IsDefToBeYieldedTo())
						sb.Append("ref ");
					sb.Append(FormatAttributeType(var) + " " + FormatEntity(var));
				}
			}
			sb.Append(")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			// Emit dispatcher calling the modify-method of the iterated pattern which was matched
			sb.AppendFront("for(Match_" + pathPrefix + iterName + " curMatch=curMatches.Root;"
					+ " curMatch!=null; curMatch=curMatch.next) {\n");
			sb.Indent();
			sb.AppendFront(pathPrefix + iterName + "_Modify"
					+ "(actionEnv, curMatch");
			foreach(Entity entity in replParameters)
			{
				sb.Append(", ");
				if(entity.IsDefToBeYieldedTo())
					sb.Append("ref ");
				sb.Append(FormatEntity(entity));
			}
			sb.Append(");\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			// Emit end of function
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private static void GenModifyIteratedDelete(SourceBuilder sb, Rule iter, string pathPrefix, string iterName,
				bool isSubpattern)
		{
			// Emit function header
			sb.Append("\n");
			sb.AppendFront("public void " + pathPrefix + iterName + "_Delete"
					+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
					+ "GRGEN_LGSP.LGSPMatchesList<Match_" + pathPrefix + iterName
					+ ", IMatch_" + pathPrefix + iterName + "> curMatches)\n");
			sb.AppendFront("{\n");
			sb.Indent();

			// Emit dispatcher calling the modify-method of the iterated pattern which was matched
			sb.AppendFront("for(Match_" + pathPrefix + iterName + " curMatch=curMatches.Root;"
					+ " curMatch!=null; curMatch=curMatch.next) {\n");
			sb.AppendFrontIndented("" + pathPrefix + iterName + "_Delete"
					+ "(actionEnv, curMatch);\n");
			sb.AppendFront("}\n");

			// Emit end of function
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenModifyRuleOrSubrule(SourceBuilder sb, ModifyGenerationTask task, string packageName,
				string pathPrefix)
		{
			SourceBuilder sb2 = new SourceBuilder();
			sb2.IndentationLevel = sb.IndentationLevel + 1;
			SourceBuilder sb3 = new SourceBuilder();
			sb3.IndentationLevel = sb.IndentationLevel + 1;

			bool useAddedElementNames = be.sys.MayFireDebugEvents()
					&& (task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_CREATION
							|| (task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY && !(task.right is PatternGraphRhsFromLhs)));
			bool createAddedElementNames = task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_CREATION ||
					(task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY && !(task.right is PatternGraphRhsFromLhs));
			string prefix = (task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_CREATION ? "create_" : "")
					+ pathPrefix + task.left.NameOfGraph + "_";

			ModifyExecGen execGen = new ModifyExecGen(be, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);
			ModifyEvalGen evalGen = new ModifyEvalGen(be, execGen, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);

			// Emit function header
			sb.Append("\n");
			EmitMethodHeadAndBegin(sb, task, pathPrefix);

			// The resulting code has the following order:
			// (but this is not the order in which it is computed)
			//  - Extract nodes from match as LGSPNode instances
			//  - Extract nodes from match or from already extracted nodes as interface instances
			//  - Extract edges from match as LGSPEdge instances
			//  - Extract edges from match or from already extracted edges as interface instances
			//  - Extract subpattern submatches from match
			//  - Extract iterated submatches from match
			//  - Extract alternative submatches from match
			//  - Extract node types
			//  - Extract edge types
			//  - Create new nodes
			//  - Call modification code of nested subpatterns
			//  - Call modification code of nested iterateds
			//  - Call modification code of nested alternatives
			//  - Retype nodes
			//  - Create new edges
			//  - Retype edges
			//  - Create subpatterns
			//  - Attribute reevaluation
			//  - Create variables for used attributes needed for imperative statements and returns
			//  - Check deleted elements for retyping due to homomorphy
			//  - Remove edges
			//  - Remove nodes
			//  - Remove subpatterns
			//  - Emit / Exec
			//  - Check returned elements for deletion and retyping due to homomorphy
			//  - Return

			ModifyGenerationState state = new ModifyGenerationState(model, null, "", false,
					be.sys.EmitProfilingInstrumentation());
			state.actionName = task.left.NameOfGraph;
			string packagePrefixedActionName = string.ReferenceEquals(packageName, null)
					? task.left.NameOfGraph
					: packageName + "::" + task.left.NameOfGraph;
			ModifyGenerationStateConst stateConst = state;

			CollectYieldedElements(task, stateConst, state.yieldedNodes, state.yieldedEdges, state.yieldedVariables);

			CollectCommonElements(task, state.commonNodes, state.commonEdges, state.commonSubpatternUsages);

			CollectNewElements(task, stateConst, state.newNodes, state.newEdges, state.newSubpatternUsages);

			CollectDeletedElements(task, stateConst, state.delNodes, state.delEdges, state.delSubpatternUsages);

			CollectNewOrRetypedElements(task, state, state.newOrRetypedNodes, state.newOrRetypedEdges);

			CollectElementsAccessedByInterface(task, state.accessViaInterface);

			NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.ALL_ATTRIBUTES | Needs.CONTAINER_EXPRS);
			CollectElementsAndAttributesNeededByImperativeStatements(task, needs);
			needs.collectContainerExprs = false;
			CollectElementsAndAttributesNeededByReturns(task, needs);
			CollectElementsNeededBySubpatternCreation(task, needs);
			CollectElementsNeededByNameOrAttributeInitialization(state, needs);

			// Copy all entries generated by collectNeededAttributes for imperative statements and returns
			foreach(KeyValuePair<GraphEntity, HashSet<Entity>> entry in needs.attrEntityMap.SetOfKeyValuePairs())
			{
				HashSet<Entity> neededAttrs = entry.Value;
				HashSet<Entity> attributesStoredBeforeDelete = state.attributesStoredBeforeDelete[entry.Key];
				if(attributesStoredBeforeDelete == null)
					state.attributesStoredBeforeDelete[entry.Key] = attributesStoredBeforeDelete = new LinkedHashSet<Entity>();
				attributesStoredBeforeDelete.AddAll(neededAttrs);
			}

			CollectElementsAndAttributesNeededByDefVarToBeYieldedToInitialization(state, needs);

			// Do not collect container expressions for evals
			CollectElementsAndAttributesNeededByEvals(task, needs);
			needs.collectContainerExprs = true;

			// Fill state with information gathered in needs
			state.InitNeeds(needs);

			if(state.EmitProfilingInstrumentation() && pathPrefix.Equals("")
					&& !task.isSubpattern && task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
				GenEvalProfilingStart(sb2, true);

			GenNewNodes(sb2, stateConst, useAddedElementNames, prefix,
					state.nodesNeededAsElements, state.nodesNeededAsTypes);

			// generates subpattern modification calls, evalhere statements, emithere statements,
			// and alternative/iterated modification calls if specified (if not, they are generated below)
			GenSubpatternModificationCalls(sb2, task, pathPrefix,
					stateConst, evalGen, execGen,
					state.nodesNeededAsElements, state.neededVariables,
					state.nodesNeededAsAttributes, state.edgesNeededAsAttributes);

			GenIteratedModificationCalls(sb2, task, pathPrefix);

			GenAlternativeModificationCalls(sb2, task, pathPrefix);

			GenYieldedElementsInterfaceAccess(sb2, stateConst, pathPrefix);

			GenRedirectEdges(sb2, task, stateConst,
					state.edgesNeededAsElements, state.nodesNeededAsElements);

			GenTypeChangesNodesAndMerges(sb2, stateConst, task,
					state.nodesNeededAsElements, state.nodesNeededAsTypes);

			GenNewEdges(sb2, stateConst, task, useAddedElementNames, prefix,
					state.nodesNeededAsElements, state.edgesNeededAsElements,
					state.edgesNeededAsTypes);

			GenTypeChangesEdges(sb2, task, stateConst,
					state.edgesNeededAsElements, state.edgesNeededAsTypes);

			GenNewSubpatternCalls(sb2, stateConst);

			evalGen.GenAllEvals(sb3, stateConst, task.evals);

			GenVariablesForUsedAttributesBeforeDelete(sb3, stateConst, state.forceAttributeToVar);

			GenCheckDeletedElementsForRetypingThroughHomomorphy(sb3, stateConst);

			GenDelEdges(sb3, stateConst, state.edgesNeededAsElements, task.right);

			GenDelNodes(sb3, stateConst, state.nodesNeededAsElements, task.right);

			GenDelSubpatternCalls(sb3, stateConst);

			if(state.EmitProfilingInstrumentation() && pathPrefix.Equals("")
					&& !task.isSubpattern && task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
				GenEvalProfilingStop(sb3, packagePrefixedActionName);

			// Emit selected match rewritten event firing (only if top-level rule)
			if(pathPrefix.Equals("") && !task.isSubpattern)
			{
				if(be.sys.MayFireDebugEvents())
					sb3.AppendFront("actionEnv.SelectedMatchRewritten();\n");
			}

			execGen.GenImperativeStatements(sb3, task, state, stateConst, needs, pathPrefix, packagePrefixedActionName);

			GenCheckReturnedElementsForDeletionOrRetypingDueToHomomorphy(sb3, task);

			// Emit return (only if top-level rule)
			if(pathPrefix.Equals("") && !task.isSubpattern)
				EmitReturnStatement(sb3, stateConst,
						state.EmitProfilingInstrumentation() && task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY,
						packagePrefixedActionName, task.returns);

			// Emit end of function
			sb3.Unindent();
			sb3.AppendFront("}\n");

			RemoveAgainFromNeededWhatIsNotReallyNeeded(task, stateConst,
					state.nodesNeededAsElements, state.edgesNeededAsElements,
					state.nodesNeededAsAttributes, state.edgesNeededAsAttributes,
					state.neededVariables);

			////////////////////////////////////////////////////////////////////////////////
			// Finalize method using the infos collected and the already generated code

			GenExtractElementsFromMatch(sb, task, stateConst, pathPrefix, task.left.NameOfGraph);

			GenExtractVariablesFromMatch(sb, task, stateConst, pathPrefix, task.left.NameOfGraph);

			GenExtractSubmatchesFromMatch(sb, pathPrefix, task.left);

			GenNeededTypes(sb, stateConst);

			GenYieldedElements(sb, stateConst, task.right);

			// New nodes/edges (re-use), retype nodes/edges, call modification code
			sb.Append(sb2.ToString());

			// Attribute re-calc, attr vars for emit, remove, emit, return
			sb.Append(sb3.ToString());

			sb.Unindent();

			// ----------

			sb.Append(state.PerElementMethodSourceBuilder.ToString());

			if(createAddedElementNames)
				GenAddedGraphElementsArray(sb, stateConst, prefix, task.typeOfTask);
		}

		private static void GenEvalProfilingStart(SourceBuilder sb, bool declareVariable)
		{
			if(declareVariable)
				sb.AppendFront("long searchStepsAtBeginEval = actionEnv.PerformanceInfo.SearchSteps;\n");
			else
				sb.AppendFront("searchStepsAtBeginEval = actionEnv.PerformanceInfo.SearchSteps;\n");
		}

		private static void GenEvalProfilingStop(SourceBuilder sb, string packagePrefixedActionName)
		{
			sb.AppendFront("actionEnv.PerformanceInfo.ActionProfiles[\"" + packagePrefixedActionName
					+ "\"].searchStepsDuringEvalTotal");
			sb.Append(" += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBeginEval;\n");
		}

		private void EmitMethodHeadAndBegin(SourceBuilder sb, ModifyGenerationTask task, string pathPrefix)
		{
			string matchType = "Match_" + pathPrefix + task.left.NameOfGraph;

			switch(task.typeOfTask)
			{
			case ModifyGenerationTask.TYPE_OF_TASK_MODIFY:
				if(string.ReferenceEquals(pathPrefix, "") && !task.isSubpattern)
				{
					sb.AppendFront("public void "
							+ "Modify"
							+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch"
							+ OutParameters(task) + ")\n");
					sb.AppendFront("{\n");
					sb.Indent();
					sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
					sb.AppendFront(matchType + " curMatch = (" + matchType + ")_curMatch;\n");
				}
				else
				{
					sb.AppendFront("public void "
							+ pathPrefix + task.left.NameOfGraph + "_Modify"
							+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch"
							+ ReplParameters(task) + ")\n");
					sb.AppendFront("{\n");
					sb.Indent();
					sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
					sb.AppendFront(matchType + " curMatch = (" + matchType + ")_curMatch;\n");
				}
				break;
			case ModifyGenerationTask.TYPE_OF_TASK_CREATION:
				sb.AppendFront("public void "
						+ pathPrefix + task.left.NameOfGraph + "_Create"
						+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv"
						+ Parameters(task) + ")\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
				break;
			case ModifyGenerationTask.TYPE_OF_TASK_DELETION:
				sb.AppendFront("public void "
						+ pathPrefix + task.left.NameOfGraph + "_Delete"
						+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, " + matchType + " curMatch)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
				break;
			default:
				Debug.Assert(false);
			break;
			}
		}

		private string OutParameters(ModifyGenerationTask task)
		{
			StringBuilder outParametersBuilder = new StringBuilder();
			int i = 0;
			foreach(Expression expr in task.returns)
			{
				outParametersBuilder.Append(", out ");
				if(expr is GraphEntityExpression)
					outParametersBuilder.Append(FormatElementInterfaceRef(expr.Type));
				else
					outParametersBuilder.Append(FormatAttributeType(expr.Type));
				outParametersBuilder.Append(" output_" + i);
				++i;
			}
			return outParametersBuilder.ToString();
		}

		private string ReplParameters(ModifyGenerationTask task)
		{
			StringBuilder replParametersBuilder = new StringBuilder();
			foreach(Entity entity in task.replParameters)
			{
				if(entity is Node)
				{
					Node node = (Node)entity;
					replParametersBuilder.Append(", ");
					if(entity.IsDefToBeYieldedTo())
						replParametersBuilder.Append("ref ");
					replParametersBuilder.Append("GRGEN_LGSP.LGSPNode " + FormatEntity(node));
				}
				else
				{
					Variable var = (Variable)entity;
					replParametersBuilder.Append(", ");
					if(entity.IsDefToBeYieldedTo())
						replParametersBuilder.Append("ref ");
					replParametersBuilder.Append(FormatAttributeType(var) + " " + FormatEntity(var));
				}
			}
			return replParametersBuilder.ToString();
		}

		private static string Parameters(ModifyGenerationTask task)
		{
			StringBuilder parametersBuilder = new StringBuilder();
			foreach(Entity entity in task.parameters)
			{
				if(entity is Node)
					parametersBuilder.Append(", GRGEN_LGSP.LGSPNode " + FormatEntity(entity));
				else if(entity is Edge)
					parametersBuilder.Append(", GRGEN_LGSP.LGSPEdge " + FormatEntity(entity));
				else
				{
					// var parameters can't be used in creation, so just skip them
					//parametersBuilder.append(", " + formatAttributeType(entity.getType()) + " " + formatEntity(entity));
				}
			}
			return parametersBuilder.ToString();
		}

		private static void CollectNewOrRetypedElements(ModifyGenerationTask task, ModifyGenerationStateConst stateConst,
				HashSet<Node> newOrRetypedNodes, HashSet<Edge> newOrRetypedEdges)
		{
			newOrRetypedNodes.AddAll(stateConst.NewNodes);
			foreach(Node node in task.right.Nodes)
			{
				if(node.ChangesType(task.right))
					newOrRetypedNodes.Add(node.GetRetypedNode(task.right));
			}
			newOrRetypedEdges.AddAll(stateConst.NewEdges);
			foreach(Edge edge in task.right.Edges)
			{
				if(edge.ChangesType(task.right))
					newOrRetypedEdges.Add(edge.GetRetypedEdge(task.right));
			}

			// yielded elements are not to be created/retyped
			newOrRetypedNodes.RemoveAll(stateConst.YieldedNodes);
			newOrRetypedEdges.RemoveAll(stateConst.YieldedEdges);
		}

		private static void RemoveAgainFromNeededWhatIsNotReallyNeeded(
				ModifyGenerationTask task, ModifyGenerationStateConst state,
				HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
				HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
				HashSet<Variable> neededVariables)
		{
			// nodes/edges needed from match, but not the new nodes
			nodesNeededAsElements.RemoveAll(state.NewNodes);
			nodesNeededAsAttributes.RemoveAll(state.NewNodes);
			edgesNeededAsElements.RemoveAll(state.NewEdges);
			edgesNeededAsAttributes.RemoveAll(state.NewEdges);

			// yielded nodes/edges are handled separately
			nodesNeededAsElements.RemoveAll(state.YieldedNodes);
			edgesNeededAsElements.RemoveAll(state.YieldedEdges);

			// nodes/edges/vars handed in as subpattern connections to create are already available as method parameters
			if(task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_CREATION)
			{
				nodesNeededAsElements.RemoveAll(task.parameters);
				//nodesNeededAsAttributes.removeAll(state.newNodes);
				edgesNeededAsElements.RemoveAll(task.parameters);
				//edgesNeededAsAttributes.removeAll(state.newEdges);
				neededVariables.RemoveAll(task.parameters);
			}

			// nodes handed in as replacement connections to modify are already available as method parameters
			if(task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
				nodesNeededAsElements.RemoveAll(task.replParameters);
				//nodesNeededAsAttributes.removeAll(state.newNodes);
		}

		private static void CollectYieldedElements(ModifyGenerationTask task,
				ModifyGenerationStateConst stateConst, HashSet<Node> yieldedNodes,
				HashSet<Edge> yieldedEdges, HashSet<Variable> yieldedVariables)
		{
			// only RHS yielded elements, the LHS yields are handled by matching,
			// for us they are simply matched elements

			foreach(Node node in task.right.Nodes)
			{
				if(node.IsDefToBeYieldedTo() && !task.left.Nodes.Contains(node))
					yieldedNodes.Add(node);
			}

			foreach(Edge edge in task.right.Edges)
			{
				if(edge.IsDefToBeYieldedTo() && !task.left.Edges.Contains(edge))
					yieldedEdges.Add(edge);
			}

			foreach(Variable var in task.right.Vars)
			{
				if(var.IsDefToBeYieldedTo() && !task.left.Vars.Contains(var))
					yieldedVariables.Add(var);
			}
		}

		private static void CollectDeletedElements(ModifyGenerationTask task,
				ModifyGenerationStateConst stateConst, HashSet<Node> delNodes, HashSet<Edge> delEdges,
				HashSet<SubpatternUsage> delSubpatternUsages)
		{
			// Deleted elements are elements from the LHS which are not common
			delNodes.AddAll(task.left.Nodes);
			delNodes.RemoveAll(stateConst.CommonNodes);
			delEdges.AddAll(task.left.Edges);
			delEdges.RemoveAll(stateConst.CommonEdges);
			delSubpatternUsages.AddAll(task.left.SubpatternUsages);
			delSubpatternUsages.RemoveAll(stateConst.CommonSubpatternUsages);

			// subpatterns not appearing on the right side as subpattern usages but as dependent replacements are to be modified by their special method
			foreach(OrderedReplacements orderedRepls in task.right.OrderedReplacements)
			{
				foreach(OrderedReplacement orderedRepl in orderedRepls.orderedReplacements)
				{
					if(orderedRepl is SubpatternDependentReplacement)
					{
						SubpatternDependentReplacement subRepl = (SubpatternDependentReplacement)orderedRepl;
						delSubpatternUsages.Remove(subRepl.SubpatternUsage);
					}
				}
			}
		}

		private static void CollectNewElements(ModifyGenerationTask task,
				ModifyGenerationStateConst stateConst, HashSet<Node> newNodes, HashSet<Edge> newEdges,
				HashSet<SubpatternUsage> newSubpatternUsages)
		{
			// New elements are elements from the RHS which are not common
			newNodes.AddAll(task.right.Nodes);
			newNodes.RemoveAll(stateConst.CommonNodes);
			newEdges.AddAll(task.right.Edges);
			newEdges.RemoveAll(stateConst.CommonEdges);
			newSubpatternUsages.AddAll(task.right.SubpatternUsages);
			newSubpatternUsages.RemoveAll(stateConst.CommonSubpatternUsages);

			// and which are not in the replacement parameters
			foreach(Entity entity in task.replParameters)
			{
				if(entity is Node)
				{
					Node node = (Node)entity;
					newNodes.Remove(node);
				}
			}

			// yielded elements are not to be created
			newNodes.RemoveAll(stateConst.YieldedNodes);
			newEdges.RemoveAll(stateConst.YieldedEdges);
		}

		private static void CollectCommonElements(ModifyGenerationTask task,
				HashSet<Node> commonNodes, HashSet<Edge> commonEdges, HashSet<SubpatternUsage> commonSubpatternUsages)
		{
			// Common elements are elements of the LHS which are unmodified by RHS
			commonNodes.AddAll(task.left.Nodes);
			commonNodes.RetainAll(task.right.Nodes);
			commonEdges.AddAll(task.left.Edges);
			commonEdges.RetainAll(task.right.Edges);
			commonSubpatternUsages.AddAll(task.left.SubpatternUsages);
			commonSubpatternUsages.RetainAll(task.right.SubpatternUsages);
		}

		private static void CollectElementsAccessedByInterface(ModifyGenerationTask task,
				HashSet<GraphEntity> accessViaInterface)
		{
			accessViaInterface.AddAll(task.left.Nodes);
			accessViaInterface.AddAll(task.left.Edges);
			foreach(Entity replParam in task.replParameters)
			{
				if(replParam is GraphEntity)
					accessViaInterface.Add((GraphEntity)replParam);
			}
			foreach(Node node in task.right.Nodes)
			{
				if(node.InheritsType())
					accessViaInterface.Add(node);
				else if(node.ChangesType(task.right))
					accessViaInterface.Add(node.GetRetypedEntity(task.right));
				else if(node.IsDefToBeYieldedTo())
					accessViaInterface.Add(node);
			}
			foreach(Edge edge in task.right.Edges)
			{
				if(edge.InheritsType())
					accessViaInterface.Add(edge);
				else if(edge.ChangesType(task.right))
					accessViaInterface.Add(edge.GetRetypedEntity(task.right));
				else if(edge.IsDefToBeYieldedTo())
					accessViaInterface.Add(edge);
			}
		}

		private static void CollectElementsAndAttributesNeededByImperativeStatements(ModifyGenerationTask task,
				NeededEntities needs)
		{
			foreach(ImperativeStmt istmt in task.right.ImperativeStmts)
			{
				if(istmt is Emit)
				{
					Emit emit = (Emit)istmt;
					foreach(Expression arg in emit.Arguments)
						arg.CollectNeededEntities(needs);
				}
				else if(istmt is Exec)
				{
					Exec exec = (Exec)istmt;
					bool collectContainerExprsBackup = needs.collectContainerExprs;
					needs.collectContainerExprs = false;
					foreach(Expression arg in exec.Arguments)
						arg.CollectNeededEntities(needs);
					needs.collectContainerExprs = collectContainerExprsBackup;
				}
				else
					Debug.Assert(false, "unknown ImperativeStmt: " + istmt + " in " + task.left.NameOfGraph);
			}

			foreach(OrderedReplacements orpls in task.right.OrderedReplacements)
			{
				foreach(OrderedReplacement orpl in orpls.orderedReplacements)
				{
					if(orpl is Emit)
					{
						Emit emit = (Emit)orpl;
						foreach(Expression arg in emit.Arguments)
							arg.CollectNeededEntities(needs);
					}
					// the other ordered statement is the totally different dependent subpattern replacement
				}
			}
		}

		private static void CollectElementsAndAttributesNeededByEvals(ModifyGenerationTask task, NeededEntities needs)
		{
			foreach(EvalStatements evalStmts in task.evals)
				evalStmts.CollectNeededEntities(needs);
			foreach(OrderedReplacements orderedReps in task.right.OrderedReplacements)
			{
				foreach(OrderedReplacement orderedRep in orderedReps.orderedReplacements)
				{
					if(orderedRep is EvalStatement)
						((EvalStatement)orderedRep).CollectNeededEntities(needs);
				}
			}
		}

		private static void CollectElementsAndAttributesNeededByDefVarToBeYieldedToInitialization(ModifyGenerationStateConst state,
				NeededEntities needs)
		{
			foreach(Variable var in state.YieldedVariables)
			{
				if(var.initialization != null)
					var.initialization.CollectNeededEntities(needs);
			}
		}

		private static void CollectElementsAndAttributesNeededByReturns(ModifyGenerationTask task,
				NeededEntities needs)
		{
			foreach(Expression expr in task.returns)
				expr.CollectNeededEntities(needs);
		}

		private static void CollectElementsNeededBySubpatternCreation(ModifyGenerationTask task,
				NeededEntities needs)
		{
			foreach(SubpatternUsage subUsage in task.right.SubpatternUsages)
			{
				foreach(Expression expr in subUsage.SubpatternConnections)
					expr.CollectNeededEntities(needs);
			}
		}

		private static void CollectElementsNeededByNameOrAttributeInitialization(ModifyGenerationState state,
				NeededEntities needs)
		{
			foreach(Node node in state.NewNodes)
			{
				foreach(NameOrAttributeInitialization nai in node.nameOrAttributeInitialization)
					nai.expr.CollectNeededEntities(needs);
			}
			foreach(Edge edge in state.NewEdges)
			{
				foreach(NameOrAttributeInitialization nai in edge.nameOrAttributeInitialization)
					nai.expr.CollectNeededEntities(needs);
			}
		}

		private static void GenNeededTypes(SourceBuilder sb, ModifyGenerationStateConst state)
		{
			foreach(Node node in state.NodesNeededAsTypes)
			{
				string name = FormatEntity(node);
				sb.AppendFront("GRGEN_LIBGR.NodeType " + name + "_type = " + name + ".lgspType;\n");
			}
			foreach(Edge edge in state.EdgesNeededAsTypes)
			{
				string name = FormatEntity(edge);
				sb.AppendFront("GRGEN_LIBGR.EdgeType " + name + "_type = " + name + ".lgspType;\n");
			}
		}

		private void GenYieldedElements(SourceBuilder sb, ModifyGenerationStateConst state, PatternGraphRhs right)
		{
			foreach(Node node in state.YieldedNodes)
			{
				if(right.ReplParameters.Contains(node))
					continue;
				sb.AppendFront("GRGEN_LGSP.LGSPNode " + FormatEntity(node) + " = null;\n");
			}
			foreach(Edge edge in state.YieldedEdges)
			{
				if(right.ReplParameters.Contains(edge))
					continue;
				sb.AppendFront("GRGEN_LGSP.LGSPEdge " + FormatEntity(edge) + " = null;\n");
			}
			foreach(Variable var in state.YieldedVariables)
			{
				if(right.ReplParameters.Contains(var))
					continue;
				sb.AppendFront(FormatAttributeType(var.Type) + " " + FormatEntity(var) + " = ");
				if(var.initialization != null)
				{
					if(var.Type is EnumType)
						sb.Append("(GRGEN_MODEL." + GetPackagePrefixDot(var.Type)
								+ "ENUM_" + FormatIdentifiable(var.Type) + ") ");
					GenExpression(sb, var.initialization, state);
					sb.Append(";\n");
				}
				else
					sb.Append(GetInitializationValue(var.Type) + ";\n");
			}
		}

		private static void GenCheckReturnedElementsForDeletionOrRetypingDueToHomomorphy(
				SourceBuilder sb, ModifyGenerationTask task)
		{
			foreach(Expression expr in task.returns)
			{
				if(!(expr is GraphEntityExpression))
					continue;

				GraphEntity grEnt = ((GraphEntityExpression)expr).GraphEntity;
				if(grEnt.IsMaybeRetyped())
				{
					string elemName = FormatEntity(grEnt);
					string kind = FormatGraphElement(grEnt);
					sb.AppendFront("if(" + elemName + ".ReplacedBy" + kind + " != null) "
							+ elemName + " = " + elemName + ".ReplacedBy" + kind + ";\n");
				}
				if(grEnt.IsMaybeDeleted())
					sb.AppendFront("if(!" + FormatEntity(grEnt) + ".Valid) " + FormatEntity(grEnt) + " = null;\n");
			}
		}

		private void GenVariablesForUsedAttributesBeforeDelete(SourceBuilder sb,
				ModifyGenerationStateConst state, Dictionary<GraphEntity, HashSet<Entity>> forceAttributeToVar)
		{
			foreach(KeyValuePair<GraphEntity, HashSet<Entity>> entry in state.AttributesStoredBeforeDelete.SetOfKeyValuePairs())
			{
				GraphEntity owner = entry.Key;

				string grEntName = FormatEntity(owner);
				foreach(Entity entity in entry.Value)
				{
					if(entity.Type is MapType || entity.Type is SetType
							|| entity.Type is ArrayType || entity.Type is DequeType)
						continue;

					GenVariable(sb, grEntName, entity);
					sb.Append(" = ");
					GenQualAccess(sb, state, owner, entity);
					sb.Append(";\n");

					HashSet<Entity> forcedAttrs = forceAttributeToVar[owner];
					if(forcedAttrs == null)
						forceAttributeToVar[owner] = forcedAttrs = new HashSet<Entity>();
					forcedAttrs.Add(entity);
				}
			}
		}

		private static void GenCheckDeletedElementsForRetypingThroughHomomorphy(SourceBuilder sb, ModifyGenerationStateConst state)
		{
			foreach(Edge edge in state.DelEdges)
			{
				if(!edge.IsMaybeRetyped())
					continue;

				string edgeName = FormatEntity(edge);
				sb.AppendFront("if(" + edgeName + ".ReplacedByEdge != null) "
						+ edgeName + " = " + edgeName + ".ReplacedByEdge;\n");
			}
			foreach(Node node in state.DelNodes)
			{
				if(!node.IsMaybeRetyped())
					continue;

				string nodeName = FormatEntity(node);
				sb.AppendFront("if(" + nodeName + ".ReplacedByNode != null) "
						+ nodeName + " = " + nodeName + ".ReplacedByNode;\n");
			}
		}

		private static void GenDelNodes(SourceBuilder sb, ModifyGenerationStateConst state,
				HashSet<Node> nodesNeededAsElements, PatternGraphBase right)
		{
			foreach(Node node in state.DelNodes)
			{
				nodesNeededAsElements.Add(node);
				sb.AppendFront("graph.RemoveEdges(" + FormatEntity(node) + ");\n");
				sb.AppendFront("graph.Remove(" + FormatEntity(node) + ");\n");
			}
			foreach(Node node in state.YieldedNodes)
			{
				if(node.PatternGraphDefYieldedIsToBeDeleted == right)
				{
					nodesNeededAsElements.Add(node);
					sb.AppendFront("graph.RemoveEdges(" + FormatEntity(node) + ");\n");
					sb.AppendFront("graph.Remove(" + FormatEntity(node) + ");\n");
				}
			}
		}

		private static void GenDelEdges(SourceBuilder sb, ModifyGenerationStateConst state,
				HashSet<Edge> edgesNeededAsElements, PatternGraphBase right)
		{
			foreach(Edge edge in state.DelEdges)
			{
				edgesNeededAsElements.Add(edge);
				sb.AppendFront("graph.Remove(" + FormatEntity(edge) + ");\n");
			}
			foreach(Edge edge in state.YieldedEdges)
			{
				if(edge.PatternGraphDefYieldedIsToBeDeleted == right)
				{
					edgesNeededAsElements.Add(edge);
					sb.AppendFront("graph.Remove(" + FormatEntity(edge) + ");\n");
				}
			}
		}

		private static void GenRedirectEdges(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
				HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements)
		{
			foreach(Edge edge in task.right.Edges)
			{
				if(edge.GetRedirectedSource(task.right) != null && edge.GetRedirectedTarget(task.right) != null)
					GenRedirectEdgeSourceAndTarget(sb, task, state, edgesNeededAsElements, nodesNeededAsElements, edge);
				else if(edge.GetRedirectedSource(task.right) != null)
					GenRedirectEdgeSource(sb, task, state, edgesNeededAsElements, nodesNeededAsElements, edge);
				else if(edge.GetRedirectedTarget(task.right) != null)
					GenRedirectEdgeTarget(sb, task, state, edgesNeededAsElements, nodesNeededAsElements, edge);
			}
		}

		private static void GenRedirectEdgeSourceAndTarget(SourceBuilder sb, ModifyGenerationTask task,
				ModifyGenerationStateConst state, HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements,
				Edge edge)
		{
			Node redirectedSource = edge.GetRedirectedSource(task.right);
			Node redirectedTarget = edge.GetRedirectedTarget(task.right);
			Node oldSource = task.left.GetSource(edge);
			Node oldTarget = task.left.GetTarget(edge);
			sb.AppendFront("graph.RedirectSourceAndTarget("
					+ FormatEntity(edge) + ", "
					+ FormatEntity(redirectedSource) + ", "
					+ FormatEntity(redirectedTarget) + ", "
					+ "\"" + (oldSource != null ? FormatIdentifiable(oldSource) : "<unknown>") + "\", "
					+ "\"" + (oldTarget != null ? FormatIdentifiable(oldTarget) : "<unknown>") + "\");\n");
			edgesNeededAsElements.Add(edge);
			if(!state.NewNodes.Contains(redirectedSource))
				nodesNeededAsElements.Add(redirectedSource);
			if(!state.NewNodes.Contains(redirectedTarget))
				nodesNeededAsElements.Add(redirectedTarget);
		}

		private static void GenRedirectEdgeSource(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
				HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements, Edge edge)
		{
			Node redirectedSource = edge.GetRedirectedSource(task.right);
			Node oldSource = task.left.GetSource(edge);
			sb.AppendFront("graph.RedirectSource("
					+ FormatEntity(edge) + ", "
					+ FormatEntity(redirectedSource) + ", "
					+ "\"" + (oldSource != null ? FormatIdentifiable(oldSource) : "<unknown>") + "\");\n");
			edgesNeededAsElements.Add(edge);
			if(!state.NewNodes.Contains(redirectedSource))
				nodesNeededAsElements.Add(redirectedSource);
		}

		private static void GenRedirectEdgeTarget(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
				HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements, Edge edge)
		{
			Node redirectedTarget = edge.GetRedirectedTarget(task.right);
			Node oldTarget = task.left.GetTarget(edge);
			sb.AppendFront("graph.RedirectTarget("
					+ FormatEntity(edge) + ", "
					+ FormatEntity(edge.GetRedirectedTarget(task.right)) + ", "
					+ "\"" + (oldTarget != null ? FormatIdentifiable(oldTarget) : "<unknown>") + "\");\n");
			edgesNeededAsElements.Add(edge);
			if(!state.NewNodes.Contains(redirectedTarget))
				nodesNeededAsElements.Add(redirectedTarget);
		}

		private void GenTypeChangesEdges(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
				HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes)
		{
			foreach(Edge edge in task.right.Edges)
			{
				if(!edge.ChangesType(task.right))
					continue;

				RetypedEdge redge = edge.GetRetypedEdge(task.right);
				GenTypeChangesEdge(sb, state, edgesNeededAsElements, edgesNeededAsTypes,
						edge, redge);
			}
		}

		private void GenTypeChangesEdge(SourceBuilder sb, ModifyGenerationStateConst state,
				HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes,
				Edge edge, RetypedEdge redge)
		{
			string new_type;

			if(redge.InheritsType())
			{
				Debug.Assert(redge.Copy == CopyKind.None);
				Edge typeofElem = (Edge)GetConcreteTypeofElem(redge);
				new_type = FormatEntity(typeofElem) + "_type";
				edgesNeededAsElements.Add(typeofElem);
				edgesNeededAsTypes.Add(typeofElem);
			}
			else
				new_type = FormatTypeClassRef(redge.Type) + ".typeVar";
			edgesNeededAsElements.Add(edge);

			sb.AppendFront("GRGEN_LGSP.LGSPEdge " + FormatEntity(redge) + " = graph.Retype("
					+ FormatEntity(edge) + ", " + new_type + ");\n");
			if(state.EdgesNeededAsAttributes.Contains(redge) && state.AccessViaInterface.Contains(redge))
			{
				sb.AppendFront(FormatVarDeclWithCast(FormatElementInterfaceRef(redge.Type), "i" + FormatEntity(redge))
						+ FormatEntity(redge) + ";\n");
			}
		}

		private void GenTypeChangesNodesAndMerges(SourceBuilder sb, ModifyGenerationStateConst state,
				ModifyGenerationTask task, HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes)
		{
			foreach(Node node in task.right.Nodes)
			{
				if(!node.ChangesType(task.right))
					continue;

				RetypedNode rnode = node.GetRetypedNode(task.right);
				GenTypeChangesNodeAndMerges(sb, state, nodesNeededAsElements, nodesNeededAsTypes,
						node, rnode);
			}
		}

		private void GenTypeChangesNodeAndMerges(SourceBuilder sb, ModifyGenerationStateConst state,
				HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes,
				Node node, RetypedNode rnode)
		{
			string new_type;

			if(rnode.InheritsType())
			{
				Debug.Assert(rnode.Copy == CopyKind.None);
				Node typeofElem = (Node)GetConcreteTypeofElem(rnode);
				new_type = FormatEntity(typeofElem) + "_type";
				nodesNeededAsElements.Add(typeofElem);
				nodesNeededAsTypes.Add(typeofElem);
			}
			else
				new_type = FormatTypeClassRef(rnode.Type) + ".typeVar";
			nodesNeededAsElements.Add(node);

			sb.AppendFront("GRGEN_LGSP.LGSPNode " + FormatEntity(rnode) + " = graph.Retype("
					+ FormatEntity(node) + ", " + new_type + ");\n");
			foreach(Node mergee in rnode.Mergees)
			{
				nodesNeededAsElements.Add(mergee);
				sb.AppendFront("graph.Merge(" + FormatEntity(rnode) + ", " + FormatEntity(mergee)
						+ ", \"" + FormatIdentifiable(mergee) + "\");\n");
			}
			if(state.NodesNeededAsAttributes.Contains(rnode) && state.AccessViaInterface.Contains(rnode))
			{
				sb.AppendFront(FormatVarDeclWithCast(FormatElementInterfaceRef(rnode.Type), "i" + FormatEntity(rnode))
						+ FormatEntity(rnode) + ";\n");
			}
		}

		private static void GenAddedGraphElementsArray(SourceBuilder sb, ModifyGenerationStateConst state, string pathPrefix,
				int typeOfTask)
		{
			if(typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY
					|| typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_CREATION)
			{
				GenAddedGraphElementsArray(sb, pathPrefix, true, state.NewNodes);
				GenAddedGraphElementsArray(sb, pathPrefix, false, state.NewEdges);
			}
		}

		private static void GenAlternativeModificationCalls(SourceBuilder sb, ModifyGenerationTask task, string pathPrefix)
		{
			if(task.right is PatternGraphRhsFromLhs) // test needs top-level-modify due to interface, but not more
				return;

			if(task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			{
				// generate calls to the modifications of the alternatives (nested alternatives are handled in their enclosing alternative)
				ICollection<Alternative> alts = task.left.Alts;
				foreach(Alternative alt in alts)
				{
					if(alt.wasReplacementAlreadyCalled)
						continue;
					GenAlternativeModificationCall(alt, sb, task, pathPrefix);
				}
			}
			else if(task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_DELETION)
			{
				// generate calls to the deletion of the alternatives (nested alternatives are handled in their enclosing alternative)
				ICollection<Alternative> alts = task.left.Alts;
				foreach(Alternative alt in alts)
				{
					string altName = alt.NameOfGraph;
					sb.AppendFront(pathPrefix + task.left.NameOfGraph + "_" + altName + "_" +
							"Delete" + "(actionEnv, alternative_" + altName + ");\n");
				}
			}
		}

		private static void GenAlternativeModificationCall(Alternative alt, SourceBuilder sb, ModifyGenerationTask task,
				string pathPrefix)
		{
			string altName = alt.NameOfGraph;
			sb.AppendFront(pathPrefix + task.left.NameOfGraph + "_" + altName + "_" +
					"Modify(actionEnv, alternative_" + altName);
			IList<Entity> replParameters = new List<Entity>();
			GetUnionOfReplaceParametersOfAlternativeCases(alt, replParameters);
			foreach(Entity entity in replParameters)
			{
				sb.Append(", ");
				if(entity.IsDefToBeYieldedTo())
					sb.Append("ref ");
				sb.Append(FormatEntity(entity));
			}
			sb.Append(");\n");
		}

		private static void GenIteratedModificationCalls(SourceBuilder sb, ModifyGenerationTask task, string pathPrefix)
		{
			if(task.right is PatternGraphRhsFromLhs) // test needs top-level-modify due to interface, but not more
				return;

			if(task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			{
				// generate calls to the modifications of the iterateds (nested iterateds are handled in their enclosing iterated)
				ICollection<Rule> iters = task.left.Iters;
				foreach(Rule iter in iters)
				{
					if(iter.wasReplacementAlreadyCalled)
						continue;
					GenIteratedModificationCall(iter, sb, task, pathPrefix);
				}
			}
			else if(task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_DELETION)
			{
				// generate calls to the deletion of the iterateds (nested iterateds are handled in their enclosing iterated)
				ICollection<Rule> iters = task.left.Iters;
				foreach(Rule iter in iters)
				{
					string iterName = iter.Left.NameOfGraph;
					sb.AppendFront(pathPrefix + task.left.NameOfGraph + "_" + iterName + "_" +
							"Delete" + "(actionEnv, iterated_" + iterName + ");\n");
				}
			}
		}

		private static void GenIteratedModificationCall(Rule iter, SourceBuilder sb, ModifyGenerationTask task, string pathPrefix)
		{
			string iterName = iter.Left.NameOfGraph;
			sb.AppendFront(pathPrefix + task.left.NameOfGraph + "_" + iterName + "_" +
					"Modify(actionEnv, iterated_" + iterName);
			IList<Entity> replParameters = iter.Right.ReplParameters;
			foreach(Entity entity in replParameters)
			{
				sb.Append(", ");
				if(entity.IsDefToBeYieldedTo())
					sb.Append("ref ");
				sb.Append(FormatEntity(entity));
			}
			sb.Append(");\n");
		}

		private void GenSubpatternModificationCalls(SourceBuilder sb, ModifyGenerationTask task, string pathPrefix,
				ModifyGenerationStateConst state, ModifyEvalGen evalGen, ModifyExecGen execGen,
				HashSet<Node> nodesNeededAsElements, HashSet<Variable> neededVariables,
				HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes)
		{
			if(task.right is PatternGraphRhsFromLhs) // test needs top-level-modify due to interface, but not more
				return;

			if(task.IsEmitHereNeeded() || task.mightThereBeDeferredExecs)
				sb.AppendFront("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;\n");

			if(task.mightThereBeDeferredExecs)
				sb.AppendFront("procEnv.sequencesManager.EnterRuleModifyAddingDeferredSequences();\n");

			// generate calls to the dependent modifications of the subpatterns
			foreach(OrderedReplacements orderedReps in task.right.OrderedReplacements)
			{
				sb.AppendFront("{ // " + orderedReps.Name + "\n");
				sb.Indent();

				foreach(OrderedReplacement orderedRep in orderedReps.orderedReplacements)
				{
					if(orderedRep is SubpatternDependentReplacement)
					{
						SubpatternDependentReplacement subRep = (SubpatternDependentReplacement)orderedRep;
						GenSubpatternReplacementModificationCall(sb, state, nodesNeededAsElements, neededVariables,
								nodesNeededAsAttributes, edgesNeededAsAttributes, subRep);
					}
					else if(orderedRep is Emit)
					{ // emithere
						Emit emit = (Emit)orderedRep;
						execGen.GenEmit(sb, state, emit);
					}
					else if(orderedRep is AlternativeReplacement)
					{
						AlternativeReplacement altRep = (AlternativeReplacement)orderedRep;
						Alternative alt = altRep.Alternative;
						GenAlternativeModificationCall(alt, sb, task, pathPrefix);
						alt.wasReplacementAlreadyCalled = true;
					}
					else if(orderedRep is IteratedReplacement)
					{
						IteratedReplacement iterRep = (IteratedReplacement)orderedRep;
						Rule iter = iterRep.Iterated;
						GenIteratedModificationCall(iter, sb, task, pathPrefix);
						iter.wasReplacementAlreadyCalled = true;
					}
					else if(orderedRep is EvalStatement)
					{ // evalhere
						EvalStatement evalStmt = (EvalStatement)orderedRep;
						evalGen.GenEvalStmt(sb, state, evalStmt);
					}
				}

				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenSubpatternReplacementModificationCall(SourceBuilder sb, ModifyGenerationStateConst state,
				HashSet<Node> nodesNeededAsElements, HashSet<Variable> neededVariables,
				HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
				SubpatternDependentReplacement subRep)
		{
			Rule subRule = subRep.SubpatternUsage.SubpatternAction;
			string subName = FormatIdentifiable(subRep);
			sb.AppendFront("GRGEN_ACTIONS." + GetPackagePrefixDot(subRule) + "Pattern_" + FormatIdentifiable(subRule)
					+ ".Instance." + FormatIdentifiable(subRule) +
					"_Modify(actionEnv, subpattern_" + subName);
			NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.ALL_ATTRIBUTES | Needs.CONTAINER_EXPRS);
			IList<Entity> replParameters = subRule.Right.ReplParameters;
			for(int i = 0; i < subRep.ReplConnections.Count; ++i)
			{
				Expression expr = subRep.ReplConnections[i];
				Entity param = replParameters[i];
				expr.CollectNeededEntities(needs);
				sb.Append(", ");
				if(param.IsDefToBeYieldedTo())
					sb.Append("ref (");
				else
				{
					if(expr is GraphEntityExpression)
						sb.Append("(GRGEN_LGSP.LGSPNode)(");
					else
						sb.Append("(" + FormatAttributeType(expr.Type) + ") (");
				}
				GenExpression(sb, expr, state);
				sb.Append(")");
			}
			foreach(Node node in needs.nodes)
				nodesNeededAsElements.Add(node);
			foreach(Node node in needs.attrNodes)
				nodesNeededAsAttributes.Add(node);
			foreach(Edge edge in needs.attrEdges)
				edgesNeededAsAttributes.Add(edge);
			foreach(Variable var in needs.variables)
				neededVariables.Add(var);
			sb.Append(");\n");
		}

		private void GenYieldedElementsInterfaceAccess(SourceBuilder sb, ModifyGenerationStateConst state,
				string pathPrefix)
		{
			foreach(Node node in state.YieldedNodes)
			{
				sb.AppendFront(FormatVarDeclWithCast(FormatElementInterfaceRef(node.Type), "i" + FormatEntity(node))
						+ FormatEntity(node) + ";\n");
			}
			foreach(Edge edge in state.YieldedEdges)
			{
				sb.AppendFront(FormatVarDeclWithCast(FormatElementInterfaceRef(edge.Type), "i" + FormatEntity(edge))
						+ FormatEntity(edge) + ";\n");
			}
		}

		private static void GenAddedGraphElementsArray<T1>(SourceBuilder sb, string pathPrefix, bool isNode,
				ICollection<T1> set) where T1 : de.unika.ipd.grgen.ir.pattern.GraphEntity
		{
			string NodesOrEdges = isNode ? "Node" : "Edge";
			sb.AppendFront("private static string[] " + pathPrefix + "added" + NodesOrEdges + "Names = new string[] ");
			GenSet(sb, set, "\"", "\"", true);
			sb.Append(";\n");
		}

		private void EmitReturnStatement(SourceBuilder sb, ModifyGenerationStateConst state, bool emitProfiling,
				string packagePrefixedactionName, IList<Expression> returns)
		{
			if(emitProfiling && returns.Count > 0)
				GenEvalProfilingStart(sb, false);
			for(int i = 0; i < returns.Count; i++)
			{
				sb.AppendFront("output_" + i + " = ");
				Expression expr = returns[i];
				if(expr is GraphEntityExpression)
					sb.Append("(" + FormatElementInterfaceRef(expr.Type) + ")(");
				else
					sb.Append("(" + FormatAttributeType(expr.Type) + ") (");
				GenExpression(sb, expr, state);
				sb.Append(");\n");
			}
			if(emitProfiling && returns.Count > 0)
				GenEvalProfilingStop(sb, packagePrefixedactionName);
			sb.AppendFront("return;\n");
		}

		private void GenExtractElementsFromMatch(SourceBuilder sb, ModifyGenerationTask task,
				ModifyGenerationStateConst state, string pathPrefix, string patternName)
		{
			foreach(Node node in state.NodesNeededAsElements)
			{
				if(node.IsRetyped() && node.IsRHSEntity())
					continue;
				if(state.YieldedNodes.Contains(node))
					continue;
				sb.AppendFront("GRGEN_LGSP.LGSPNode " + FormatEntity(node)
						+ " = curMatch." + FormatEntity(node, "_") + ";\n");
			}
			foreach(Node node in state.NodesNeededAsAttributes)
			{
				if(node.IsRetyped() && node.IsRHSEntity())
					continue;
				if(state.YieldedNodes.Contains(node))
					continue;
				if(task.replParameters.Contains(node))
				{
					sb.AppendFront(FormatElementInterfaceRef(node.Type) + " i" + FormatEntity(node)
							+ " = (" + FormatElementInterfaceRef(node.Type) + ")" + FormatEntity(node) + ";\n");
					continue; // replacement parameters are handed in as parameters
				}
				sb.AppendFront(FormatElementInterfaceRef(node.Type) + " i" + FormatEntity(node)
						+ " = curMatch." + FormatEntity(node) + ";\n");
			}
			foreach(Edge edge in state.EdgesNeededAsElements)
			{
				if(edge.IsRetyped() && edge.IsRHSEntity())
					continue;
				if(state.YieldedEdges.Contains(edge))
					continue;
				sb.AppendFront("GRGEN_LGSP.LGSPEdge " + FormatEntity(edge)
						+ " = curMatch." + FormatEntity(edge, "_") + ";\n");
			}
			foreach(Edge edge in state.EdgesNeededAsAttributes)
			{
				if(edge.IsRetyped() && edge.IsRHSEntity())
					continue;
				if(state.YieldedEdges.Contains(edge))
					continue;
				if(task.replParameters.Contains(edge))
				{
					sb.AppendFront(FormatElementInterfaceRef(edge.Type) + " i" + FormatEntity(edge)
							+ " = (" + FormatElementInterfaceRef(edge.Type) + ")" + FormatEntity(edge) + ";\n");
					continue; // replacement parameters are handed in as parameters
				}
				sb.AppendFront(FormatElementInterfaceRef(edge.Type) + " i" + FormatEntity(edge)
						+ " = curMatch." + FormatEntity(edge) + ";\n");
			}
		}

		private void GenExtractVariablesFromMatch(SourceBuilder sb, ModifyGenerationTask task,
				ModifyGenerationStateConst state, string pathPrefix, string patternName)
		{
			foreach(Variable var in state.NeededVariables)
			{
				if(task.replParameters.Contains(var))
					continue; // skip replacement parameters, they are handed in as parameters
				if(state.YieldedVariables.Contains(var))
					continue;
				string type = FormatAttributeType(var);
				sb.AppendFront(type + " " + FormatEntity(var)
						+ " = curMatch." + FormatEntity(var, "_") + ";\n");
			}
		}

		private static void GenExtractSubmatchesFromMatch(SourceBuilder sb, string pathPrefix, PatternGraphLhs pattern)
		{
			foreach(SubpatternUsage sub in pattern.SubpatternUsages)
			{
				string subName = FormatIdentifiable(sub);
				sb.AppendFront(MatchType(sub.SubpatternAction.Pattern, sub.SubpatternAction, true, "")
						+ " subpattern_" + subName
						+ " = curMatch.@_" + FormatIdentifiable(sub) + ";\n");
			}
			foreach(Rule iter in pattern.Iters)
			{
				string iterName = iter.Left.NameOfGraph;
				string iterType = "GRGEN_LGSP.LGSPMatchesList<Match_" + pathPrefix + pattern.NameOfGraph + "_"
						+ iterName + ", IMatch_" + pathPrefix + pattern.NameOfGraph + "_" + iterName + ">";
				sb.AppendFront(iterType + " iterated_" + iterName
						+ " = curMatch._" + iterName + ";\n");
			}
			foreach(Alternative alt in pattern.Alts)
			{
				string altName = alt.NameOfGraph;
				string altType = "IMatch_" + pathPrefix + pattern.NameOfGraph + "_" + altName;
				sb.AppendFront(altType + " alternative_" + altName
						+ " = curMatch._" + altName + ";\n");
			}
		}

		////////////////////////////
		// New element generation //
		////////////////////////////

		private void GenNewNodes(SourceBuilder sb2, ModifyGenerationStateConst state,
				bool useAddedElementNames, string pathPrefix,
				HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes)
		{
			// call nodes added delegate
			if(useAddedElementNames)
				sb2.AppendFront("graph.SettingAddedNodeNames( " + pathPrefix + "addedNodeNames );\n");

			IList<Node> tmpNewNodes = new List<Node>(state.NewNodes);

			foreach(Node node in tmpNewNodes)
			{
				GenNewNode(sb2, state, pathPrefix, nodesNeededAsElements, nodesNeededAsTypes,
						node);
			}
		}

		private void GenNewNode(SourceBuilder sb2, ModifyGenerationStateConst state, string pathPrefix,
				HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes,
				Node node)
		{
			if(node.InheritsType())
			{ // typeof or copy
				Node typeofElem = (Node)GetConcreteTypeofElem(node);
				nodesNeededAsElements.Add(typeofElem);

				if(node.Copy == CopyKind.Clone) // node:clone<typeofElem>
				{
					sb2.AppendFront("GRGEN_LGSP.LGSPNode " + FormatEntity(node)
							+ " = (GRGEN_LGSP.LGSPNode) "
							+ FormatEntity(typeofElem) + ".Clone();\n");
				}
				else if(node.Copy == CopyKind.Copy) // node:copy<typeofElem>
				{
					sb2.AppendFront("GRGEN_LGSP.LGSPNode " + FormatEntity(node)
					+ " = (GRGEN_LGSP.LGSPNode) "
					+ FormatEntity(typeofElem) + ".Copy(graph, new Dictionary<object, object>());\n");
				}
				else // node:typeof(typeofElem)
				{
					nodesNeededAsTypes.Add(typeofElem);
					sb2.AppendFront("GRGEN_LGSP.LGSPNode " + FormatEntity(node)
							+ " = (GRGEN_LGSP.LGSPNode) "
							+ FormatEntity(typeofElem) + "_type.CreateNode();\n");
				}
				if(node.HasNameInitialization())
				{
					sb2.AppendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).AddNode(" + FormatEntity(node) + ", ");
					GenExpression(sb2, node.NameInitialization.expr, state);
					sb2.Append(");\n");
				}
				else
					sb2.AppendFront("graph.AddNode(" + FormatEntity(node) + ");\n");

				if(state.NodesNeededAsAttributes.Contains(node) && state.AccessViaInterface.Contains(node))
				{
					sb2.AppendFront(FormatVarDeclWithCast(FormatElementInterfaceRef(node.Type), "i" + FormatEntity(node))
							+ FormatEntity(node) + ";\n");
				}
			}
			else
			{ // node:type
				string elemref = FormatInheritanceClassRef(node.Type);
				if(node.HasNameInitialization())
				{
					sb2.AppendFront(elemref + " " + FormatEntity(node) + " = "
							+ elemref + ".CreateNode((GRGEN_LGSP.LGSPNamedGraph)graph, ");
					GenExpression(sb2, node.NameInitialization.expr, state);
					sb2.Append(");\n");
				}
				else
					sb2.AppendFront(elemref + " " + FormatEntity(node) + " = " + elemref + ".CreateNode(graph);\n");
			}
		}

		/// <summary>
		/// Returns the iterated inherited type element for a given element
		/// or null, if the given element does not inherit its type from another element.
		/// </summary>
		private static GraphEntity GetConcreteTypeofElem(GraphEntity elem)
		{
			GraphEntity typeofElem = elem;
			while(typeofElem.InheritsType())
				typeofElem = typeofElem.Typeof;
			return typeofElem == elem ? null : typeofElem;
		}

		private void GenNewEdges(SourceBuilder sb2, ModifyGenerationStateConst state, ModifyGenerationTask task,
				bool useAddedElementNames, string pathPrefix,
				HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
				HashSet<Edge> edgesNeededAsTypes)
		{
			// call edges added delegate
			if(useAddedElementNames)
				sb2.AppendFront("graph.SettingAddedEdgeNames( " + pathPrefix + "addedEdgeNames );\n");

			foreach(Edge edge in state.NewEdges)
			{
				Node src_node = task.right.GetSource(edge);
				Node tgt_node = task.right.GetTarget(edge);
				if(src_node == null || tgt_node == null)
					return; // don't create dangling edges    - todo: what's the correct way to handle them?

				if(src_node.ChangesType(task.right))
					src_node = src_node.GetRetypedNode(task.right);
				if(tgt_node.ChangesType(task.right))
					tgt_node = tgt_node.GetRetypedNode(task.right);

				if(state.CommonNodes.Contains(src_node))
					nodesNeededAsElements.Add(src_node);

				if(state.CommonNodes.Contains(tgt_node))
					nodesNeededAsElements.Add(tgt_node);

				GenNewEdge(sb2, state, pathPrefix, edgesNeededAsElements, edgesNeededAsTypes,
						edge, src_node, tgt_node);
			}
		}

		private void GenNewEdge(SourceBuilder sb2, ModifyGenerationStateConst state, string pathPrefix,
				HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes,
				Edge edge, Node src_node, Node tgt_node)
		{
			if(edge.InheritsType())
			{ // typeof or copy
				Edge typeofElem = (Edge)GetConcreteTypeofElem(edge);
				edgesNeededAsElements.Add(typeofElem);

				if(edge.Copy == CopyKind.Clone) // -edge:clone<typeofElem>->
				{
					sb2.AppendFront("GRGEN_LGSP.LGSPEdge " + FormatEntity(edge)
							+ " = (GRGEN_LGSP.LGSPEdge) "
							+ FormatEntity(typeofElem) + ".Clone("
							+ FormatEntity(src_node) + ", " + FormatEntity(tgt_node) + ");\n");
				}
				else if(edge.Copy == CopyKind.Copy) // -edge:copy<typeofElem>->
				{
					sb2.AppendFront("GRGEN_LGSP.LGSPEdge " + FormatEntity(edge)
							+ " = (GRGEN_LGSP.LGSPEdge) "
							+ FormatEntity(typeofElem) + ".Copy("
							+ FormatEntity(src_node) + ", " + FormatEntity(tgt_node) + ", graph, new Dictionary<object, object>());\n");
				}
				else
				{ // -edge:typeof(typeofElem)->
					edgesNeededAsTypes.Add(typeofElem);
					sb2.AppendFront("GRGEN_LGSP.LGSPEdge " + FormatEntity(edge) + " = (GRGEN_LGSP.LGSPEdge) "
							+ FormatEntity(typeofElem) + "_type.CreateEdge("
							+ FormatEntity(src_node) + ", " + FormatEntity(tgt_node) + ");\n");
				}
				if(edge.HasNameInitialization())
				{
					sb2.AppendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).AddEdge(" + FormatEntity(edge) + ", ");
					GenExpression(sb2, edge.NameInitialization.expr, state);
					sb2.Append(");\n");
				}
				else
					sb2.AppendFront("graph.AddEdge(" + FormatEntity(edge) + ");\n");

				if(state.EdgesNeededAsAttributes.Contains(edge) && state.AccessViaInterface.Contains(edge))
				{
					sb2.AppendFront(FormatVarDeclWithCast(FormatElementInterfaceRef(edge.Type), "i" + FormatEntity(edge))
							+ FormatEntity(edge) + ";\n");
				}
			}
			else
			{ // -edge:type->
				string elemref = FormatInheritanceClassRef(edge.Type);
				if(edge.HasNameInitialization())
				{
					sb2.AppendFront(elemref + " " + FormatEntity(edge) + " = " + elemref
							+ ".CreateEdge((GRGEN_LGSP.LGSPNamedGraph)graph, " + FormatEntity(src_node)
							+ ", " + FormatEntity(tgt_node) + ", ");
					GenExpression(sb2, edge.NameInitialization.expr, state);
					sb2.Append(");\n");
				}
				else
				{
					sb2.AppendFront(elemref + " " + FormatEntity(edge) + " = " + elemref
							+ ".CreateEdge(graph, " + FormatEntity(src_node)
							+ ", " + FormatEntity(tgt_node) + ");\n");
				}
			}
		}

		private void GenNewSubpatternCalls(SourceBuilder sb, ModifyGenerationStateConst state)
		{
			foreach(SubpatternUsage subUsage in state.NewSubpatternUsages)
			{
				if(HasAbstractElements(subUsage.SubpatternAction.Pattern)
						|| HasDanglingEdges(subUsage.SubpatternAction.Pattern))
					continue; // pattern creation code was not generated, can't call it

				sb.AppendFront("GRGEN_ACTIONS." + GetPackagePrefixDot(subUsage.SubpatternAction)
						+ "Pattern_" + FormatIdentifiable(subUsage.SubpatternAction)
						+ ".Instance." + FormatIdentifiable(subUsage.SubpatternAction)
						+ "_Create(actionEnv");
				foreach(Expression expr in subUsage.SubpatternConnections)
				{
					// var parameters can't be used in creation, so just skip them
					if(expr is GraphEntityExpression)
					{
						sb.Append(", ");
						sb.Append("(" + FormatInheritanceClassRef(expr.Type) + ")(");
						GenExpression(sb, expr, state);
						sb.Append(")");
					}
				}
				sb.Append(");\n");
			}
		}

		private static void GenDelSubpatternCalls(SourceBuilder sb, ModifyGenerationStateConst state)
		{
			foreach(SubpatternUsage subUsage in state.DelSubpatternUsages)
			{
				string subName = FormatIdentifiable(subUsage);
				sb.AppendFront("GRGEN_ACTIONS." + GetPackagePrefixDot(subUsage.SubpatternAction)
						+ "Pattern_" + FormatIdentifiable(subUsage.SubpatternAction)
						+ ".Instance." + FormatIdentifiable(subUsage.SubpatternAction)
						+ "_Delete(actionEnv, subpattern_" + subName + ");\n");
			}
		}

		//////////////////////
		// Expression stuff //
		//////////////////////

		protected internal override void GenQualAccess(SourceBuilder sb, Qualification qual, object modifyGenerationState)
		{
			GenQualAccess(sb, qual, (ModifyGenerationState)modifyGenerationState);
		}

		protected internal virtual void GenQualAccess(SourceBuilder sb, Qualification qual, ModifyGenerationStateConst state)
		{
			Entity owner = qual.Owner;
			Entity member = qual.Member;
			if(owner.Type is MatchType || owner.Type is DefinedMatchType)
				sb.Append(FormatEntity(owner) + "." + FormatEntity(member));
			else
				GenQualAccess(sb, state, owner, member);
		}

		protected internal virtual void GenQualAccess(SourceBuilder sb, ModifyGenerationStateConst state, Entity owner, Entity member)
		{
			if(!Expression.IsGlobalVariable(owner))
			{
				if(state == null)
				{
					Debug.Assert(false);
					sb.Append(FormatEntity(owner) + ".@" + FormatIdentifiable(member));
					return;
				}

				if(AccessViaVariable(state, owner, member))
					sb.Append("tempvar_" + FormatEntity(owner) + "_" + FormatIdentifiable(member));
				else
				{
					if(AccessViaInterface(state, owner))
						sb.Append("i");

					sb.Append(FormatEntity(owner) + ".@" + FormatIdentifiable(member));
				}
			}
			else
			{
				sb.Append(FormatGlobalVariableRead(owner));
				sb.Append(".@" + FormatIdentifiable(member));
			}
		}

		protected internal override void GenMemberAccess(SourceBuilder sb, Entity member)
		{
			// needed in implementing methods
			sb.Append("@" + FormatIdentifiable(member));
		}

		private void GenVariable(SourceBuilder sb, string ownerName, Entity entity)
		{
			string varTypeName;
			string attrName = FormatIdentifiable(entity);
			Type type = entity.Type;
			if(type is EnumType)
				varTypeName = "GRGEN_MODEL." + GetPackagePrefixDot(type) + "ENUM_" + FormatIdentifiable(type);
			else
				varTypeName = GetTypeNameForTempVarDecl(type);

			sb.AppendFront(varTypeName + " tempvar_" + ownerName + "_" + attrName);
		}
	}

}
