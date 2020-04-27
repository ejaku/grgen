/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the rewrite part of the actions file for the SearchPlanBackend2 backend.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.util.SourceBuilder;
import de.unika.ipd.grgen.ir.containers.*;


public class ModifyGen extends CSharpBase {
	final List<Entity> emptyParameters = new LinkedList<Entity>();
	final List<Expression> emptyReturns = new LinkedList<Expression>();
	final Collection<EvalStatements> emptyEvals = new LinkedList<EvalStatements>();

	Model model;
	SearchPlanBackend2 be;

	
	public ModifyGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix) {
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
		model = be.unit.getActionsGraphModel();
	}

	//////////////////////////////////
	// Modification part generation //
	//////////////////////////////////

	public void genModify(SourceBuilder sb, Rule rule, String packageName, boolean isSubpattern) {
		genModify(sb, rule, packageName, "", "pat_"+rule.getLeft().getNameOfGraph(), isSubpattern);
	}

	private void genModify(SourceBuilder sb, Rule rule, String packageName, String pathPrefix, String patGraphVarName, boolean isSubpattern)
	{
		if(rule.getRight()!=null) { // rule / subpattern with dependent replacement
			// replace left by right, normal version
			ModifyGenerationTask task = new ModifyGenerationTask();
			task.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_MODIFY;
			task.left = rule.getLeft();
			task.right = rule.getRight();
			task.parameters = rule.getParameters();
			task.evals = rule.getEvals();
			task.replParameters = rule.getRight().getReplParameters();
			task.returns = rule.getReturns();
			task.isSubpattern = isSubpattern;
			task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
			genModifyRuleOrSubrule(sb, task, packageName, pathPrefix);
		} else if(!isSubpattern){ // test
			// keep left unchanged, normal version
			ModifyGenerationTask task = new ModifyGenerationTask();
			task.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_MODIFY;
			task.left = rule.getLeft();
			task.right = rule.getLeft();
			task.parameters = rule.getParameters();
			task.evals = rule.getEvals();
			task.replParameters = emptyParameters;
			task.returns = rule.getReturns();
			task.isSubpattern = false;
			task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
			genModifyRuleOrSubrule(sb, task, packageName, pathPrefix);
		}

		if(isSubpattern) {
			if(pathPrefix.isEmpty() 
					&& !hasAbstractElements(rule.getLeft())
					&& !hasDanglingEdges(rule.getLeft())) {
				// create subpattern into pattern
				ModifyGenerationTask task = new ModifyGenerationTask();
				task.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_CREATION;
				task.left = new PatternGraph(rule.getLeft().getNameOfGraph(), 0); // empty graph
				task.left.setDirectlyNestingLHSGraph(task.left);
				task.right = rule.getLeft();
				task.parameters = rule.getParameters();
				task.evals = emptyEvals;
				task.replParameters = emptyParameters;
				task.returns = emptyReturns;
				task.isSubpattern = true;
				task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
				for(Entity entity : task.parameters) { // add connections to empty graph so that they stay unchanged
					if(entity instanceof Node) {
						Node node = (Node)entity;
						task.left.addSingleNode(node);
					} else if(entity instanceof Edge) {
						Edge edge = (Edge)entity;
						task.left.addSingleEdge(edge);
					}
				}
				genModifyRuleOrSubrule(sb, task, packageName, pathPrefix);
			}

			// delete subpattern from pattern
			ModifyGenerationTask task = new ModifyGenerationTask();
			task.typeOfTask = ModifyGenerationTask.TYPE_OF_TASK_DELETION;
			task.left = rule.getLeft();
			task.right = new PatternGraph(rule.getLeft().getNameOfGraph(), 0); // empty graph
			task.right.setDirectlyNestingLHSGraph(task.left);
			task.parameters = rule.getParameters();
			task.evals = emptyEvals;
			task.replParameters = emptyParameters;
			task.returns = emptyReturns;
			task.isSubpattern = true;
			task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
			for(Entity entity : task.parameters) { // add connections to empty graph so that they stay unchanged
				if(entity instanceof Node) {
					Node node = (Node)entity;
					task.right.addSingleNode(node);
				} else if(entity instanceof Edge) {
					Edge edge = (Edge)entity;
					task.right.addSingleEdge(edge);
				}
			}
			genModifyRuleOrSubrule(sb, task, packageName, pathPrefix);
		}

		for(Alternative alt : rule.getLeft().getAlts()) {
			String altName = alt.getNameOfGraph();

			genModifyAlternative(sb, rule, alt, pathPrefix+rule.getLeft().getNameOfGraph()+"_", altName, isSubpattern);

			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altCasePatGraphVarName = pathPrefix+rule.getLeft().getNameOfGraph()+"_"+altName+"_"+altCasePattern.getNameOfGraph();
				genModify(sb, altCase, packageName, pathPrefix+rule.getLeft().getNameOfGraph()+"_"+altName+"_", altCasePatGraphVarName, isSubpattern);
			}
		}

		for(Rule iter : rule.getLeft().getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			String iterPatGraphVarName = pathPrefix+rule.getLeft().getNameOfGraph()+"_"+iterName;
			genModifyIterated(sb, iter, pathPrefix+rule.getLeft().getNameOfGraph()+"_", iterName, isSubpattern);
			genModify(sb, iter, packageName, pathPrefix+rule.getLeft().getNameOfGraph()+"_", iterPatGraphVarName, isSubpattern);
		}
	}

	/**
	 * Checks whether the given pattern contains abstract elements.
	 */
	private boolean hasAbstractElements(PatternGraph left) {
		for(Node node : left.getNodes()) {
			if(node.getNodeType().isAbstract())
				return true;
		}

		for(Edge edge : left.getEdges()) {
			if(edge.getEdgeType().isAbstract())
				return true;
		}

		return false;
	}

	private boolean hasDanglingEdges(PatternGraph left) {
		for(Edge edge : left.getEdges()) {
			if(left.getSource(edge)==null || left.getTarget(edge)==null)
				return true;
		}

		return false;
	}

	private void genModifyAlternative(SourceBuilder sb, Rule rule, Alternative alt,
			String pathPrefix, String altName, boolean isSubpattern) {
		if(rule.getRight()!=null) { // generate code for dependent modify dispatcher
			genModifyAlternativeModify(sb, alt, pathPrefix, altName, isSubpattern);
		}

		if(isSubpattern) { // generate for delete alternative dispatcher
			genModifyAlternativeDelete(sb, alt, pathPrefix, altName, isSubpattern);
		}
	}

	private void genModifyAlternativeModify(SourceBuilder sb, Alternative alt, String pathPrefix, String altName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.appendFront("public void "
					  + pathPrefix+altName+"_Modify"
					  + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, IMatch_"+pathPrefix+altName+" curMatch");
		List<Entity> replParameters = new LinkedList<Entity>();
		getUnionOfReplaceParametersOfAlternativeCases(alt, replParameters);
		for(Entity entity : replParameters) {
			if(entity instanceof Node) {
				Node node = (Node)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo())
					sb.append("ref ");
				sb.append("GRGEN_LGSP.LGSPNode " + formatEntity(node));
			} else {
				Variable var = (Variable)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo())
					sb.append("ref ");
				sb.append(formatAttributeType(var)+ " " + formatEntity(var));
			}
		}
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();

		// Emit dispatcher calling the modify-method of the alternative case which was matched
		boolean firstCase = true;
		for(Rule altCase : alt.getAlternativeCases()) {
			if(firstCase) {
				sb.appendFront("if(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
				firstCase = false;
			} else {
				sb.appendFront("else if(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
			}
			sb.indent();
			sb.appendFront(pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+"_Modify"
					+ "(actionEnv, (Match_"+pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+")curMatch");
			replParameters = altCase.getRight().getReplParameters();
			for(Entity entity : replParameters) {
				sb.append(", ");
				if(entity.isDefToBeYieldedTo())
					sb.append("ref ");
				sb.append(formatEntity(entity));
			}
			sb.append(");\n");
			sb.appendFront("return;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
		sb.appendFront("throw new ApplicationException(); //debug assert\n");

		// Emit end of function
		sb.unindent();
		sb.appendFront("}\n");
	}
	
	private void getUnionOfReplaceParametersOfAlternativeCases(Alternative alt, Collection<Entity> replaceParameters) {
		for(Rule altCase : alt.getAlternativeCases()) {
			List<Entity> replParams = altCase.getRight().getReplParameters();
			for(Entity entity : replParams) {
				if(!replaceParameters.contains(entity)) {
					replaceParameters.add(entity);
				}
			}
		}
	}

	private void genModifyAlternativeDelete(SourceBuilder sb, Alternative alt, String pathPrefix, String altName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.appendFront("public void " + pathPrefix+altName+"_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
				+ "IMatch_"+pathPrefix+altName+" curMatch)\n");
		sb.appendFront("{\n");
		sb.indent();

		// Emit dispatcher calling the delete-method of the alternative case which was matched
		boolean firstCase = true;
		for(Rule altCase : alt.getAlternativeCases()) {
			if(firstCase) {
				sb.appendFront("if(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
				firstCase = false;
			}
			else
			{
				sb.appendFront("else if(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
			}
			sb.indent();
			sb.appendFront(pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+"_"
					+ "Delete(actionEnv, (Match_"+pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+")curMatch);\n");
			sb.appendFront("return;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
		sb.appendFront("throw new ApplicationException(); //debug assert\n");

		// Emit end of function
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genModifyIterated(SourceBuilder sb, Rule rule, String pathPrefix, String iterName, boolean isSubpattern) {
		if(rule.getRight()!=null) { // generate code for dependent modify dispatcher
			genModifyIteratedModify(sb, rule, pathPrefix, iterName, isSubpattern);
		}

		if(isSubpattern) { // generate for delete iterated dispatcher
			genModifyIteratedDelete(sb, rule, pathPrefix, iterName, isSubpattern);
		}
	}

	private void genModifyIteratedModify(SourceBuilder sb, Rule iter, String pathPrefix, String iterName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.appendFront("public void "+pathPrefix+iterName+"_Modify"
					  + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
					  + "GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefix+iterName
					  + ", IMatch_"+pathPrefix+iterName+"> curMatches");
		List<Entity> replParameters = iter.getRight().getReplParameters();
		for(Entity entity : replParameters) {
			if(entity instanceof Node) {
				Node node = (Node)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo())
					sb.append("ref ");
				sb.append("GRGEN_LGSP.LGSPNode " + formatEntity(node));
			} else {
				Variable var = (Variable)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo())
					sb.append("ref ");
				sb.append(formatAttributeType(var)+ " " + formatEntity(var));
			}
		}
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();

		// Emit dispatcher calling the modify-method of the iterated pattern which was matched
		sb.appendFront("for(Match_"+pathPrefix+iterName+" curMatch=curMatches.Root;"
				+" curMatch!=null; curMatch=curMatch.next) {\n");
		sb.indent();
		sb.appendFront(pathPrefix+iterName+"_Modify"
				+ "(actionEnv, curMatch");
		for(Entity entity : replParameters) {
			sb.append(", ");
			if(entity.isDefToBeYieldedTo())
				sb.append("ref ");
			sb.append(formatEntity(entity));
		}
		sb.append(");\n");
		sb.unindent();
		sb.appendFront("}\n");

		// Emit end of function
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genModifyIteratedDelete(SourceBuilder sb, Rule iter, String pathPrefix, String iterName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.appendFront("public void "+pathPrefix+iterName+"_Delete"
					  + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
					  + "GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefix+iterName
					  + ", IMatch_"+pathPrefix+iterName+"> curMatches)\n");
		sb.appendFront("{\n");
		sb.indent();

		// Emit dispatcher calling the modify-method of the iterated pattern which was matched
		sb.appendFront("for(Match_"+pathPrefix+iterName+" curMatch=curMatches.Root;"
				+" curMatch!=null; curMatch=curMatch.next) {\n");
		sb.appendFront("\t" + pathPrefix+iterName+"_Delete"
				+ "(actionEnv, curMatch);\n");
		sb.appendFront("}\n");

		// Emit end of function
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genModifyRuleOrSubrule(SourceBuilder sb, ModifyGenerationTask task, String packageName, String pathPrefix) {
		SourceBuilder sb2 = new SourceBuilder();
		sb2.indent().indent().indent();
		SourceBuilder sb3 = new SourceBuilder();
		sb3.indent().indent().indent();

		boolean useAddedElementNames = be.system.mayFireDebugEvents()
			&& (task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_CREATION
				|| (task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY && task.left!=task.right));
		boolean createAddedElementNames = task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_CREATION ||
			(task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY && task.left!=task.right);
		String prefix = (task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_CREATION ? "create_" : "")
			+ pathPrefix+task.left.getNameOfGraph()+"_";

		ModifyExecGen execGen = new ModifyExecGen(be, nodeTypePrefix, edgeTypePrefix);
		ModifyEvalGen evalGen = new ModifyEvalGen(be, execGen, nodeTypePrefix, edgeTypePrefix);
		
		// Emit function header
		sb.append("\n");
		emitMethodHeadAndBegin(sb, task, pathPrefix);

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

		ModifyGenerationState state = new ModifyGenerationState(model, null, "", false, be.system.emitProfilingInstrumentation());
		state.actionName = task.left.getNameOfGraph();
		String packagePrefixedActionName = packageName==null ? task.left.getNameOfGraph() : packageName + "::" + task.left.getNameOfGraph();
		ModifyGenerationStateConst stateConst = state;

		collectYieldedElements(task, stateConst, state.yieldedNodes, state.yieldedEdges, state.yieldedVariables);

		collectCommonElements(task, state.commonNodes, state.commonEdges, state.commonSubpatternUsages);

		collectNewElements(task, stateConst, state.newNodes, state.newEdges, state.newSubpatternUsages);

		collectDeletedElements(task, stateConst, state.delNodes, state.delEdges, state.delSubpatternUsages);
		
		collectNewOrRetypedElements(task, state, state.newOrRetypedNodes, state.newOrRetypedEdges);

		collectElementsAccessedByInterface(task, state.accessViaInterface);

		NeededEntities needs = new NeededEntities(true, true, true, false, true, true, false, false);
		collectElementsAndAttributesNeededByImperativeStatements(task, needs);
		needs.collectContainerExprs = false;
		collectElementsAndAttributesNeededByReturns(task, needs);
		collectElementsNeededBySubpatternCreation(task, needs);
		collectElementsNeededByNameOrAttributeInitialization(state, needs);
		
		// Copy all entries generated by collectNeededAttributes for imperative statements and returns
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : needs.attrEntityMap.entrySet()) {
			HashSet<Entity> neededAttrs = entry.getValue();
			HashSet<Entity> attributesStoredBeforeDelete = state.attributesStoredBeforeDelete.get(entry.getKey());
			if(attributesStoredBeforeDelete == null) {
				state.attributesStoredBeforeDelete.put(entry.getKey(),
						attributesStoredBeforeDelete = new LinkedHashSet<Entity>());
			}
			attributesStoredBeforeDelete.addAll(neededAttrs);
		}

		collectElementsAndAttributesNeededByDefVarToBeYieldedToInitialization(state, needs);

		// Do not collect container expressions for evals
		collectElementsAndAttributesNeededByEvals(task, needs);
		needs.collectContainerExprs = true;

		// Fill state with information gathered in needs
		state.InitNeeds(needs);

		if(state.emitProfilingInstrumentation() && pathPrefix.equals("")
				&& !task.isSubpattern && task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			genEvalProfilingStart(sb2, true);
		
		genNewNodes(sb2, stateConst, useAddedElementNames, prefix,
				state.nodesNeededAsElements, state.nodesNeededAsTypes);

		// generates subpattern modification calls, evalhere statements, emithere statements,
		// and alternative/iterated modification calls if specified (if not, they are generated below)
		genSubpatternModificationCalls(sb2, task, pathPrefix,
				stateConst, evalGen, execGen,
				state.nodesNeededAsElements, state.neededVariables,
				state.nodesNeededAsAttributes, state.edgesNeededAsAttributes);

		genIteratedModificationCalls(sb2, task, pathPrefix);

		genAlternativeModificationCalls(sb2, task, pathPrefix);
		
		genYieldedElementsInterfaceAccess(sb2, stateConst, pathPrefix);

		genRedirectEdges(sb2, task, stateConst, 
				state.edgesNeededAsElements, state.nodesNeededAsElements);

		genTypeChangesNodesAndMerges(sb2, stateConst, task,
				state.nodesNeededAsElements, state.nodesNeededAsTypes);

		genNewEdges(sb2, stateConst, task, useAddedElementNames, prefix,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.edgesNeededAsTypes);

		genTypeChangesEdges(sb2, task, stateConst, 
				state.edgesNeededAsElements, state.edgesNeededAsTypes);

		genNewSubpatternCalls(sb2, stateConst);

		evalGen.genAllEvals(sb3, stateConst, task.evals);

		genVariablesForUsedAttributesBeforeDelete(sb3, stateConst, state.forceAttributeToVar);

		genCheckDeletedElementsForRetypingThroughHomomorphy(sb3, stateConst);

		genDelEdges(sb3, stateConst, state.edgesNeededAsElements, task.right);

		genDelNodes(sb3, stateConst, state.nodesNeededAsElements, task.right);

		genDelSubpatternCalls(sb3, stateConst);

		if(state.emitProfilingInstrumentation() && pathPrefix.equals("")
				&& !task.isSubpattern && task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			genEvalProfilingStop(sb3, packagePrefixedActionName);

		execGen.genImperativeStatements(sb3, task, state, stateConst, needs, pathPrefix, packagePrefixedActionName);

		genCheckReturnedElementsForDeletionOrRetypingDueToHomomorphy(sb3, task);

		// Emit return (only if top-level rule)
		if(pathPrefix.equals("") && !task.isSubpattern) {
			emitReturnStatement(sb3, stateConst,
				state.emitProfilingInstrumentation() && task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY,
				packagePrefixedActionName, task.returns);
		}

		// Emit end of function
		sb3.unindent();
		sb3.appendFront("}\n");

		removeAgainFromNeededWhatIsNotReallyNeeded(task, stateConst,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.nodesNeededAsAttributes, state.edgesNeededAsAttributes,
				state.neededVariables);

		////////////////////////////////////////////////////////////////////////////////
		// Finalize method using the infos collected and the already generated code

		genExtractElementsFromMatch(sb, task, stateConst, pathPrefix, task.left.getNameOfGraph());

		genExtractVariablesFromMatch(sb, task, stateConst, pathPrefix, task.left.getNameOfGraph());

		genExtractSubmatchesFromMatch(sb, pathPrefix, task.left);

		genNeededTypes(sb, stateConst);
	
		genYieldedElements(sb, stateConst, task.right);

		// New nodes/edges (re-use), retype nodes/edges, call modification code
		sb.append(sb2.toString());

		// Attribute re-calc, attr vars for emit, remove, emit, return
		sb.append(sb3.toString());

		sb.unindent();
		
		// ----------

		if(createAddedElementNames) {
			genAddedGraphElementsArray(sb, stateConst, prefix, task.typeOfTask);
		}
	}

	private void genEvalProfilingStart(SourceBuilder sb, boolean declareVariable) {
		if(declareVariable)
	        sb.appendFront("long searchStepsAtBeginEval = actionEnv.PerformanceInfo.SearchSteps;\n");
		else
			sb.appendFront("searchStepsAtBeginEval = actionEnv.PerformanceInfo.SearchSteps;\n");
	}

	private void genEvalProfilingStop(SourceBuilder sb, String packagePrefixedActionName) {
		sb.appendFront("actionEnv.PerformanceInfo.ActionProfiles[\"" + packagePrefixedActionName + "\"].searchStepsDuringEvalTotal");
		sb.append(" += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBeginEval;\n");
	}

	private void emitMethodHeadAndBegin(SourceBuilder sb, ModifyGenerationTask task, String pathPrefix)
	{
		String matchType = "Match_"+pathPrefix+task.left.getNameOfGraph();

		switch(task.typeOfTask) {
		case ModifyGenerationTask.TYPE_OF_TASK_MODIFY:
			if(pathPrefix=="" && !task.isSubpattern) {
				sb.appendFront("public void "
						+ "Modify"
						+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch"
						+ outParameters(task) + ")\n");
				sb.appendFront("{\n");
				sb.indent();
				sb.appendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
				sb.appendFront(matchType+" curMatch = ("+matchType+")_curMatch;\n");
			} else {
				sb.appendFront("public void "
						+ pathPrefix+task.left.getNameOfGraph() + "_Modify"
						+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch"
						+ replParameters(task) + ")\n");
				sb.appendFront("{\n");
				sb.indent();
				sb.appendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
				sb.appendFront(matchType+" curMatch = ("+matchType+")_curMatch;\n");
			}
			break;
		case ModifyGenerationTask.TYPE_OF_TASK_CREATION:
			sb.appendFront("public void "
					+ pathPrefix+task.left.getNameOfGraph() + "_Create"
					+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv"
					+ parameters(task) + ")\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
			break;
		case ModifyGenerationTask.TYPE_OF_TASK_DELETION:
			sb.appendFront("public void "
					+ pathPrefix+task.left.getNameOfGraph() + "_Delete"
					+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "+matchType+" curMatch)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
			break;
		default:
			assert false;
		}
	}

	private String outParameters(ModifyGenerationTask task) {
		StringBuffer outParametersBuilder = new StringBuffer();
		int i = 0;
		for(Expression expr : task.returns) {
			outParametersBuilder.append(", out ");
			if(expr instanceof GraphEntityExpression)
				outParametersBuilder.append(formatElementInterfaceRef(expr.getType()));
			else
				outParametersBuilder.append(formatAttributeType(expr.getType()));
			outParametersBuilder.append(" output_" + i);
			++i;
		}
		return outParametersBuilder.toString();
	}

	private String replParameters(ModifyGenerationTask task) {
		StringBuffer replParametersBuilder = new StringBuffer();
		for(Entity entity : task.replParameters) {
			if(entity instanceof Node) {
				Node node = (Node)entity;
				replParametersBuilder.append(", ");
				if(entity.isDefToBeYieldedTo())
					replParametersBuilder.append("ref ");
				replParametersBuilder.append("GRGEN_LGSP.LGSPNode " + formatEntity(node));
			} else {
				Variable var = (Variable)entity;
				replParametersBuilder.append(", ");
				if(entity.isDefToBeYieldedTo())
					replParametersBuilder.append("ref ");
				replParametersBuilder.append(formatAttributeType(var)+ " " + formatEntity(var));
			}
		}
		return replParametersBuilder.toString();
	}

	private String parameters(ModifyGenerationTask task) {
		StringBuffer parametersBuilder = new StringBuffer();
		for(Entity entity : task.parameters) {
			if(entity instanceof Node) {
				parametersBuilder.append(", GRGEN_LGSP.LGSPNode " + formatEntity(entity));					
			} else if (entity instanceof Edge) {
				parametersBuilder.append(", GRGEN_LGSP.LGSPEdge " + formatEntity(entity));
			} else {
				// var parameters can't be used in creation, so just skip them
				//parametersBuilder.append(", " + formatAttributeType(entity.getType()) + " " + formatEntity(entity));
			}
		}
		return parametersBuilder.toString();
	}

	private void collectNewOrRetypedElements(ModifyGenerationTask task,	ModifyGenerationStateConst stateConst,
			HashSet<Node> newOrRetypedNodes, HashSet<Edge> newOrRetypedEdges)
	{
		newOrRetypedNodes.addAll(stateConst.newNodes());
		for(Node node : task.right.getNodes()) {
			if(node.changesType(task.right))
				newOrRetypedNodes.add(node.getRetypedNode(task.right));
		}
		newOrRetypedEdges.addAll(stateConst.newEdges());
		for(Edge edge : task.right.getEdges()) {
			if(edge.changesType(task.right))
				newOrRetypedEdges.add(edge.getRetypedEdge(task.right));
		}
		
		// yielded elements are not to be created/retyped
		newOrRetypedNodes.removeAll(stateConst.yieldedNodes());
		newOrRetypedEdges.removeAll(stateConst.yieldedEdges());
	}

	private void removeAgainFromNeededWhatIsNotReallyNeeded(
			ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
			HashSet<Variable> neededVariables)
	{
		// nodes/edges needed from match, but not the new nodes
		nodesNeededAsElements.removeAll(state.newNodes());
		nodesNeededAsAttributes.removeAll(state.newNodes());
		edgesNeededAsElements.removeAll(state.newEdges());
		edgesNeededAsAttributes.removeAll(state.newEdges());
		
		// yielded nodes/edges are handled separately
		nodesNeededAsElements.removeAll(state.yieldedNodes());
		edgesNeededAsElements.removeAll(state.yieldedEdges());

		// nodes/edges/vars handed in as subpattern connections to create are already available as method parameters
		if(task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_CREATION) {
			nodesNeededAsElements.removeAll(task.parameters);
			//nodesNeededAsAttributes.removeAll(state.newNodes);
			edgesNeededAsElements.removeAll(task.parameters);
			//edgesNeededAsAttributes.removeAll(state.newEdges);
			neededVariables.removeAll(task.parameters);
		}

		// nodes handed in as replacement connections to modify are already available as method parameters
		if(task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY) {
			nodesNeededAsElements.removeAll(task.replParameters);
			//nodesNeededAsAttributes.removeAll(state.newNodes);
		}
	}

	private void collectYieldedElements(ModifyGenerationTask task,
			ModifyGenerationStateConst stateConst, HashSet<Node> yieldedNodes, 
			HashSet<Edge> yieldedEdges, HashSet<Variable> yieldedVariables)
	{
		// only RHS yielded elements, the LHS yields are handled by matching,
		// for us they are simply matched elements
		
		for(Node node : task.right.getNodes()) {
			if(node.isDefToBeYieldedTo() && !task.left.getNodes().contains(node)) {
				yieldedNodes.add(node);
			}
		}

		for(Edge edge : task.right.getEdges()) {
			if(edge.isDefToBeYieldedTo() && !task.left.getEdges().contains(edge)) {
				yieldedEdges.add(edge);
			}
		}
		
		for(Variable var : task.right.getVars()) {
			if(var.isDefToBeYieldedTo() && !task.left.getVars().contains(var)) {
				yieldedVariables.add(var);
			}
		}
	}
	
	private void collectDeletedElements(ModifyGenerationTask task,
			ModifyGenerationStateConst stateConst, HashSet<Node> delNodes, HashSet<Edge> delEdges,
			HashSet<SubpatternUsage> delSubpatternUsages)
	{
		// Deleted elements are elements from the LHS which are not common
		delNodes.addAll(task.left.getNodes());
		delNodes.removeAll(stateConst.commonNodes());
		delEdges.addAll(task.left.getEdges());
		delEdges.removeAll(stateConst.commonEdges());
		delSubpatternUsages.addAll(task.left.getSubpatternUsages());
		delSubpatternUsages.removeAll(stateConst.commonSubpatternUsages());

		// subpatterns not appearing on the right side as subpattern usages but as dependent replacements are to be modified by their special method
		for(OrderedReplacements orderedRepls : task.right.getOrderedReplacements()) {
			for(OrderedReplacement orderedRepl : orderedRepls.orderedReplacements) {
				if(orderedRepl instanceof SubpatternDependentReplacement) {
					SubpatternDependentReplacement subRepl = (SubpatternDependentReplacement)orderedRepl;
					delSubpatternUsages.remove(subRepl.getSubpatternUsage());
				}
			}
		}
	}

	private void collectNewElements(ModifyGenerationTask task,
			ModifyGenerationStateConst stateConst, HashSet<Node> newNodes, HashSet<Edge> newEdges,
			HashSet<SubpatternUsage> newSubpatternUsages)
	{
		// New elements are elements from the RHS which are not common
		newNodes.addAll(task.right.getNodes());
		newNodes.removeAll(stateConst.commonNodes());
		newEdges.addAll(task.right.getEdges());
		newEdges.removeAll(stateConst.commonEdges());
		newSubpatternUsages.addAll(task.right.getSubpatternUsages());
		newSubpatternUsages.removeAll(stateConst.commonSubpatternUsages());

		// and which are not in the replacement parameters
		for(Entity entity : task.replParameters) {
			if(entity instanceof Node) {
				Node node = (Node)entity;
				newNodes.remove(node);
			}
		}
		
		// yielded elements are not to be created
		newNodes.removeAll(stateConst.yieldedNodes());
		newEdges.removeAll(stateConst.yieldedEdges());
	}

	private void collectCommonElements(ModifyGenerationTask task,
			HashSet<Node> commonNodes, HashSet<Edge> commonEdges, HashSet<SubpatternUsage> commonSubpatternUsages)
	{
		// Common elements are elements of the LHS which are unmodified by RHS
		commonNodes.addAll(task.left.getNodes());
		commonNodes.retainAll(task.right.getNodes());
		commonEdges.addAll(task.left.getEdges());
		commonEdges.retainAll(task.right.getEdges());
		commonSubpatternUsages.addAll(task.left.getSubpatternUsages());
		commonSubpatternUsages.retainAll(task.right.getSubpatternUsages());
	}

	private void collectElementsAccessedByInterface(ModifyGenerationTask task,
			HashSet<GraphEntity> accessViaInterface)
	{
		accessViaInterface.addAll(task.left.getNodes());
		accessViaInterface.addAll(task.left.getEdges());
		for(Entity replParam : task.replParameters) {
			if(replParam instanceof GraphEntity)
				accessViaInterface.add((GraphEntity)replParam);
		}
		for(Node node : task.right.getNodes()) {
			if(node.inheritsType())
				accessViaInterface.add(node);
			else if(node.changesType(task.right))
				accessViaInterface.add(node.getRetypedEntity(task.right));
			else if(node.isDefToBeYieldedTo())
				accessViaInterface.add(node);
		}
		for(Edge edge : task.right.getEdges()) {
			if(edge.inheritsType())
				accessViaInterface.add(edge);
			else if(edge.changesType(task.right))
				accessViaInterface.add(edge.getRetypedEntity(task.right));
			else if(edge.isDefToBeYieldedTo())
				accessViaInterface.add(edge);
		}
	}

	private void collectElementsAndAttributesNeededByImperativeStatements(ModifyGenerationTask task,
			NeededEntities needs)
	{
		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit) istmt;
				for(Expression arg : emit.getArguments()) {
					arg.collectNeededEntities(needs);
				}
			}
			else if (istmt instanceof Exec) {
				Exec exec = (Exec) istmt;
				boolean collectContainerExprsBackup = needs.collectContainerExprs;
				needs.collectContainerExprs = false;
				for(Expression arg : exec.getArguments()) {
					arg.collectNeededEntities(needs);
				}
				needs.collectContainerExprs = collectContainerExprsBackup;
			}
			else
				assert false : "unknown ImperativeStmt: " + istmt + " in " + task.left.getNameOfGraph();
		}

		for(OrderedReplacements orpls : task.right.getOrderedReplacements()) {
			for(OrderedReplacement orpl : orpls.orderedReplacements) {
				if(orpl instanceof Emit) {
					Emit emit = (Emit) orpl;
					for(Expression arg : emit.getArguments()) {
						arg.collectNeededEntities(needs);
					}
				}
				// the other ordered statement is the totally different dependent subpattern replacement
			}
		}
	}

	private void collectElementsAndAttributesNeededByEvals(ModifyGenerationTask task, NeededEntities needs)
	{
		for(EvalStatements evalStmts : task.evals) {
			evalStmts.collectNeededEntities(needs);
		}
		for(OrderedReplacements orderedReps : task.right.getOrderedReplacements()) {
			for(OrderedReplacement orderedRep : orderedReps.orderedReplacements) {
				if(orderedRep instanceof EvalStatement) {
					((EvalStatement)orderedRep).collectNeededEntities(needs);
				}
			}
		}
	}

	private void collectElementsAndAttributesNeededByDefVarToBeYieldedToInitialization(ModifyGenerationStateConst state,
			NeededEntities needs)
	{
		for(Variable var : state.yieldedVariables()) {
			if(var.initialization!=null)
				var.initialization.collectNeededEntities(needs);
		}
	}

	private void collectElementsAndAttributesNeededByReturns(ModifyGenerationTask task,
			NeededEntities needs)
	{
		for(Expression expr : task.returns) {
			expr.collectNeededEntities(needs);
		}
	}

	private void collectElementsNeededBySubpatternCreation(ModifyGenerationTask task,
			NeededEntities needs)
	{
		for(SubpatternUsage subUsage : task.right.getSubpatternUsages()) {
			for(Expression expr : subUsage.getSubpatternConnections()) {
				expr.collectNeededEntities(needs);
			}
		}
	}

	private void collectElementsNeededByNameOrAttributeInitialization(ModifyGenerationState state,
			NeededEntities needs)
	{
		for(Node node : state.newNodes()) {
			for(NameOrAttributeInitialization nai : node.nameOrAttributeInitialization) {
				nai.expr.collectNeededEntities(needs);
			}
		}
		for(Edge edge : state.newEdges()) {
			for(NameOrAttributeInitialization nai : edge.nameOrAttributeInitialization) {
				nai.expr.collectNeededEntities(needs);
			}
		}
	}

	private void genNeededTypes(SourceBuilder sb, ModifyGenerationStateConst state)
	{
		for(Node node : state.nodesNeededAsTypes()) {
			String name = formatEntity(node);
			sb.appendFront("GRGEN_LIBGR.NodeType " + name + "_type = " + name + ".lgspType;\n");
		}
		for(Edge edge : state.edgesNeededAsTypes()) {
			String name = formatEntity(edge);
			sb.appendFront("GRGEN_LIBGR.EdgeType " + name + "_type = " + name + ".lgspType;\n");
		}
	}

	private void genYieldedElements(SourceBuilder sb, ModifyGenerationStateConst state, PatternGraph right)
	{
		for(Node node : state.yieldedNodes()) {
			if(right.getReplParameters().contains(node))
				continue;
			sb.appendFront("GRGEN_LGSP.LGSPNode " + formatEntity(node)+ " = null;\n");
		}
		for(Edge edge : state.yieldedEdges()) {
			if(right.getReplParameters().contains(edge))
				continue;
			sb.appendFront("GRGEN_LGSP.LGSPEdge " + formatEntity(edge) + " = null;\n");
		}
		for(Variable var : state.yieldedVariables()) {
			if(right.getReplParameters().contains(var))
				continue;
			sb.appendFront(formatAttributeType(var.getType()) + " " + formatEntity(var) + " = ");
			if(var.initialization!=null) {
				if(var.getType() instanceof EnumType)
					sb.append("(GRGEN_MODEL." + getPackagePrefixDot(var.getType()) + "ENUM_" + formatIdentifiable(var.getType()) + ") ");
				genExpression(sb, var.initialization, state);
				sb.append(";\n");
			} else {
				sb.append(getInitializationValue(var.getType()) + ";\n");
			}
		}
	}

	private void genCheckReturnedElementsForDeletionOrRetypingDueToHomomorphy(
			SourceBuilder sb, ModifyGenerationTask task)
	{
		for(Expression expr : task.returns) {
			if(!(expr instanceof GraphEntityExpression))
				continue;

			GraphEntity grEnt = ((GraphEntityExpression) expr).getGraphEntity();
			if(grEnt.isMaybeRetyped()) {
				String elemName = formatEntity(grEnt);
				String kind = formatNodeOrEdge(grEnt);
				sb.appendFront("if(" + elemName + ".ReplacedBy" + kind + " != null) "
					+ elemName + " = " + elemName + ".ReplacedBy" + kind + ";\n");
			}
			if(grEnt.isMaybeDeleted())
				sb.appendFront("if(!" + formatEntity(grEnt) + ".Valid) " + formatEntity(grEnt) + " = null;\n");
		}
	}

	private void genVariablesForUsedAttributesBeforeDelete(SourceBuilder sb,
			ModifyGenerationStateConst state, HashMap<GraphEntity, HashSet<Entity>> forceAttributeToVar)
	{
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : state.attributesStoredBeforeDelete().entrySet()) {
			GraphEntity owner = entry.getKey();

			String grEntName = formatEntity(owner);
			for(Entity entity : entry.getValue()) {
				if(entity.getType() instanceof MapType || entity.getType() instanceof SetType 
						|| entity.getType() instanceof ArrayType || entity.getType() instanceof DequeType)
					continue;

				genVariable(sb, grEntName, entity);
				sb.append(" = ");
				genQualAccess(sb, state, owner, entity);
				sb.append(";\n");

				HashSet<Entity> forcedAttrs = forceAttributeToVar.get(owner);
				if(forcedAttrs == null)
					forceAttributeToVar.put(owner, forcedAttrs = new HashSet<Entity>());
				forcedAttrs.add(entity);
			}
		}
	}

	private void genCheckDeletedElementsForRetypingThroughHomomorphy(SourceBuilder sb, ModifyGenerationStateConst state)
	{
		for(Edge edge : state.delEdges()) {
			if(!edge.isMaybeRetyped())
				continue;

			String edgeName = formatEntity(edge);
			sb.appendFront("if(" + edgeName + ".ReplacedByEdge != null) "
					+ edgeName + " = " + edgeName + ".ReplacedByEdge;\n");
		}
		for(Node node : state.delNodes()) {
			if(!node.isMaybeRetyped())
				continue;

			String nodeName = formatEntity(node);
			sb.appendFront("if(" + nodeName + ".ReplacedByNode != null) "
					+ nodeName + " = " + nodeName + ".ReplacedByNode;\n");
		}
	}

	private void genDelNodes(SourceBuilder sb, ModifyGenerationStateConst state,
			HashSet<Node> nodesNeededAsElements, PatternGraph right)
	{
		for(Node node : state.delNodes()) {
			nodesNeededAsElements.add(node);
			sb.appendFront("graph.RemoveEdges(" + formatEntity(node) + ");\n");
			sb.appendFront("graph.Remove(" + formatEntity(node) + ");\n");
		}
		for(Node node : state.yieldedNodes()) {
			if(node.patternGraphDefYieldedIsToBeDeleted() == right) {
				nodesNeededAsElements.add(node);
				sb.appendFront("graph.RemoveEdges(" + formatEntity(node) + ");\n");
				sb.appendFront("graph.Remove(" + formatEntity(node) + ");\n");
			}
		}
	}

	private void genDelEdges(SourceBuilder sb, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, PatternGraph right)
	{
		for(Edge edge : state.delEdges()) {
			edgesNeededAsElements.add(edge);
			sb.appendFront("graph.Remove(" + formatEntity(edge) + ");\n");
		}
		for(Edge edge : state.yieldedEdges()) {
			if(edge.patternGraphDefYieldedIsToBeDeleted() == right) {
				edgesNeededAsElements.add(edge);
				sb.appendFront("graph.Remove(" + formatEntity(edge) + ");\n");
			}
		}
	}

	private void genRedirectEdges(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements)
	{
		for(Edge edge : task.right.getEdges()) {
			if(edge.getRedirectedSource(task.right) != null && edge.getRedirectedTarget(task.right) != null) {
				genRedirectEdgeSourceAndTarget(sb, task, state, edgesNeededAsElements, nodesNeededAsElements, edge);
			} 
			else if(edge.getRedirectedSource(task.right) != null) {
				genRedirectEdgeSource(sb, task, state, edgesNeededAsElements, nodesNeededAsElements, edge);
			}
			else if(edge.getRedirectedTarget(task.right) != null) {
				genRedirectEdgeTarget(sb, task, state, edgesNeededAsElements, nodesNeededAsElements, edge);
			}
		}
	}

	private void genRedirectEdgeSourceAndTarget(SourceBuilder sb, ModifyGenerationTask task,
			ModifyGenerationStateConst state, HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements,
			Edge edge) {
		Node redirectedSource = edge.getRedirectedSource(task.right);
		Node redirectedTarget = edge.getRedirectedTarget(task.right);
		Node oldSource = task.left.getSource(edge);
		Node oldTarget = task.left.getTarget(edge);
		sb.appendFront("graph.RedirectSourceAndTarget("
				+ formatEntity(edge) + ", "
				+ formatEntity(redirectedSource) + ", "
				+ formatEntity(redirectedTarget) + ", "
				+ "\"" + (oldSource!=null ? formatIdentifiable(oldSource) : "<unknown>") + "\", "
				+ "\"" + (oldTarget!=null ? formatIdentifiable(oldTarget) : "<unknown>") + "\");\n");
		edgesNeededAsElements.add(edge);
		if(!state.newNodes().contains(redirectedSource))
			nodesNeededAsElements.add(redirectedSource);
		if(!state.newNodes().contains(redirectedTarget))
			nodesNeededAsElements.add(redirectedTarget);
	}

	private void genRedirectEdgeSource(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements, Edge edge) {
		Node redirectedSource = edge.getRedirectedSource(task.right);
		Node oldSource = task.left.getSource(edge);
		sb.appendFront("graph.RedirectSource("
				+ formatEntity(edge) + ", "
				+ formatEntity(redirectedSource) + ", "
				+ "\"" + (oldSource!=null ? formatIdentifiable(oldSource) : "<unknown>") + "\");\n");
		edgesNeededAsElements.add(edge);
		if(!state.newNodes().contains(redirectedSource))
			nodesNeededAsElements.add(redirectedSource);
	}

	private void genRedirectEdgeTarget(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements, Edge edge) {
		Node redirectedTarget = edge.getRedirectedTarget(task.right);
		Node oldTarget = task.left.getTarget(edge);
		sb.appendFront("graph.RedirectTarget("
				+ formatEntity(edge) + ", "
				+ formatEntity(edge.getRedirectedTarget(task.right)) + ", "
				+ "\"" + (oldTarget!=null ? formatIdentifiable(oldTarget) : "<unknown>") + "\");\n");
		edgesNeededAsElements.add(edge);
		if(!state.newNodes().contains(redirectedTarget))
			nodesNeededAsElements.add(redirectedTarget);
	}
	
	private void genTypeChangesEdges(SourceBuilder sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes)
	{
		for(Edge edge : task.right.getEdges()) {
			if(!edge.changesType(task.right))
				continue;
			
			RetypedEdge redge = edge.getRetypedEdge(task.right);
			genTypeChangesEdge(sb, state, edgesNeededAsElements, edgesNeededAsTypes, 
					edge, redge);
		}
	}

	private void genTypeChangesEdge(SourceBuilder sb, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes,
			Edge edge, RetypedEdge redge)
	{
		String new_type;

		if(redge.inheritsType()) {
			assert !redge.isCopy();
			Edge typeofElem = (Edge) getConcreteTypeofElem(redge);
			new_type = formatEntity(typeofElem) + "_type";
			edgesNeededAsElements.add(typeofElem);
			edgesNeededAsTypes.add(typeofElem);
		} else {
			new_type = formatTypeClassRef(redge.getType()) + ".typeVar";
		}
		edgesNeededAsElements.add(edge);
		
		sb.appendFront("GRGEN_LGSP.LGSPEdge " + formatEntity(redge) + " = graph.Retype("
				+ formatEntity(edge) + ", " + new_type + ");\n");
		if(state.edgesNeededAsAttributes().contains(redge) && state.accessViaInterface().contains(redge)) {
			sb.appendFront(formatVarDeclWithCast(formatElementInterfaceRef(redge.getType()), "i" + formatEntity(redge))
					+ formatEntity(redge) + ";\n");
		}
	}

	private void genTypeChangesNodesAndMerges(SourceBuilder sb, ModifyGenerationStateConst state, ModifyGenerationTask task,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes)
	{
		for(Node node : task.right.getNodes()) {
			if(!node.changesType(task.right))
				continue;
			
			RetypedNode rnode = node.getRetypedNode(task.right);
			genTypeChangesNodeAndMerges(sb, state, nodesNeededAsElements, nodesNeededAsTypes,
					node, rnode);
		}
	}

	private void genTypeChangesNodeAndMerges(SourceBuilder sb, ModifyGenerationStateConst state,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes,
			Node node, RetypedNode rnode)
	{
		String new_type;

		if(rnode.inheritsType()) {
			assert !rnode.isCopy();
			Node typeofElem = (Node) getConcreteTypeofElem(rnode);
			new_type = formatEntity(typeofElem) + "_type";
			nodesNeededAsElements.add(typeofElem);
			nodesNeededAsTypes.add(typeofElem);
		} else {
			new_type = formatTypeClassRef(rnode.getType()) + ".typeVar";
		}
		nodesNeededAsElements.add(node);
		
		sb.appendFront("GRGEN_LGSP.LGSPNode " + formatEntity(rnode) + " = graph.Retype("
				+ formatEntity(node) + ", " + new_type + ");\n");
		for(Node mergee : rnode.getMergees()) {
			nodesNeededAsElements.add(mergee);
			sb.appendFront("graph.Merge("
					+ formatEntity(rnode) + ", " + formatEntity(mergee) + ", \"" + formatIdentifiable(mergee) + "\");\n");
		}
		if(state.nodesNeededAsAttributes().contains(rnode) && state.accessViaInterface().contains(rnode)) {
			sb.appendFront(formatVarDeclWithCast(formatElementInterfaceRef(rnode.getType()), "i" + formatEntity(rnode))
					+ formatEntity(rnode) + ";\n");
		}
	}

	private void genAddedGraphElementsArray(SourceBuilder sb, ModifyGenerationStateConst state, String pathPrefix, int typeOfTask) {
		if(typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY || typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_CREATION) {
			genAddedGraphElementsArray(sb, pathPrefix, true, state.newNodes());
			genAddedGraphElementsArray(sb, pathPrefix, false, state.newEdges());
		}
	}

	private void genAlternativeModificationCalls(SourceBuilder sb, ModifyGenerationTask task, String pathPrefix) {
		if(task.right==task.left) { // test needs top-level-modify due to interface, but not more
			return;
		}

		if(task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY) {
			// generate calls to the modifications of the alternatives (nested alternatives are handled in their enclosing alternative)
			Collection<Alternative> alts = task.left.getAlts();
			for(Alternative alt : alts) {
				if(alt.wasReplacementAlreadyCalled)
					continue;
				genAlternativeModificationCall(alt, sb, task, pathPrefix);
			}
		}
		else if(task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_DELETION) {
			// generate calls to the deletion of the alternatives (nested alternatives are handled in their enclosing alternative)
			Collection<Alternative> alts = task.left.getAlts();
			for(Alternative alt : alts) {
				String altName = alt.getNameOfGraph();
				sb.appendFront(pathPrefix+task.left.getNameOfGraph()+"_"+altName+"_" +
						"Delete" + "(actionEnv, alternative_"+altName+");\n");
			}
		}
	}

	private void genAlternativeModificationCall(Alternative alt, SourceBuilder sb, ModifyGenerationTask task, String pathPrefix) {
		String altName = alt.getNameOfGraph();
		sb.appendFront(pathPrefix+task.left.getNameOfGraph()+"_"+altName+"_" +
				"Modify(actionEnv, alternative_" + altName);
		List<Entity> replParameters = new LinkedList<Entity>();
		getUnionOfReplaceParametersOfAlternativeCases(alt, replParameters);
		for(Entity entity : replParameters) {
			sb.append(", ");
			if(entity.isDefToBeYieldedTo())
				sb.append("ref ");
			sb.append(formatEntity(entity));
		}
		sb.append(");\n");
	}

	private void genIteratedModificationCalls(SourceBuilder sb, ModifyGenerationTask task, String pathPrefix) {
		if(task.right==task.left) { // test needs top-level-modify due to interface, but not more
			return;
		}

		if(task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_MODIFY) {
			// generate calls to the modifications of the iterateds (nested iterateds are handled in their enclosing iterated)
			Collection<Rule> iters = task.left.getIters();
			for(Rule iter : iters) {
				if(iter.wasReplacementAlreadyCalled)
					continue;
				genIteratedModificationCall(iter, sb, task, pathPrefix);
			}
		}
		else if(task.typeOfTask==ModifyGenerationTask.TYPE_OF_TASK_DELETION) {
			// generate calls to the deletion of the iterateds (nested iterateds are handled in their enclosing iterated)
			Collection<Rule> iters = task.left.getIters();
			for(Rule iter : iters) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront(pathPrefix+task.left.getNameOfGraph()+"_"+iterName+"_" +
						"Delete" + "(actionEnv, iterated_"+iterName+");\n");
			}
		}
	}

	private void genIteratedModificationCall(Rule iter, SourceBuilder sb, ModifyGenerationTask task, String pathPrefix) {
		String iterName = iter.getLeft().getNameOfGraph();
		sb.appendFront(pathPrefix+task.left.getNameOfGraph()+"_"+iterName+"_" +
				"Modify(actionEnv, iterated_" + iterName);
		List<Entity> replParameters = iter.getRight().getReplParameters();
		for(Entity entity : replParameters) {
			sb.append(", ");
			if(entity.isDefToBeYieldedTo())
				sb.append("ref ");
			sb.append(formatEntity(entity));
		}
		sb.append(");\n");
	}

	private void genSubpatternModificationCalls(SourceBuilder sb, ModifyGenerationTask task, String pathPrefix,
			ModifyGenerationStateConst state, ModifyEvalGen evalGen, ModifyExecGen execGen,
			HashSet<Node> nodesNeededAsElements, HashSet<Variable> neededVariables,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes) {
		if(task.right==task.left) { // test needs top-level-modify due to interface, but not more
			return;
		}

		if(execGen.isEmitHereNeeded(task) || task.mightThereBeDeferredExecs) {
			sb.appendFront("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;\n");
		}

		if(task.mightThereBeDeferredExecs) {
			sb.appendFront("procEnv.sequencesManager.EnterRuleModifyAddingDeferredSequences();\n");
		}

		// generate calls to the dependent modifications of the subpatterns
		for(OrderedReplacements orderedReps : task.right.getOrderedReplacements()) {
			sb.appendFront("{ // " + orderedReps.getName() + "\n");
			sb.indent();
			
			for(OrderedReplacement orderedRep : orderedReps.orderedReplacements) {
				if(orderedRep instanceof SubpatternDependentReplacement) {
					SubpatternDependentReplacement subRep = (SubpatternDependentReplacement)orderedRep;
					genSubpatternReplacementModificationCall(sb, state, nodesNeededAsElements, neededVariables,
							nodesNeededAsAttributes, edgesNeededAsAttributes, subRep);
				} else if(orderedRep instanceof Emit) { // emithere
					Emit emit = (Emit)orderedRep;
					execGen.genEmit(sb, state, emit);
				} else if(orderedRep instanceof AlternativeReplacement) {
					AlternativeReplacement altRep = (AlternativeReplacement)orderedRep;
					Alternative alt = altRep.getAlternative();
					genAlternativeModificationCall(alt, sb, task, pathPrefix);
					alt.wasReplacementAlreadyCalled = true;
				} else if(orderedRep instanceof IteratedReplacement) {
					IteratedReplacement iterRep = (IteratedReplacement)orderedRep;
					Rule iter = iterRep.getIterated();
					genIteratedModificationCall(iter, sb, task, pathPrefix);
					iter.wasReplacementAlreadyCalled = true;
				} else if(orderedRep instanceof EvalStatement) { // evalhere
					EvalStatement evalStmt = (EvalStatement)orderedRep;
					evalGen.genEvalStmt(sb, state, evalStmt);
				}
			}
			
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genSubpatternReplacementModificationCall(SourceBuilder sb, ModifyGenerationStateConst state,
			HashSet<Node> nodesNeededAsElements, HashSet<Variable> neededVariables,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
			SubpatternDependentReplacement subRep) {
		Rule subRule = subRep.getSubpatternUsage().getSubpatternAction();
		String subName = formatIdentifiable(subRep);
		sb.appendFront("GRGEN_ACTIONS." + getPackagePrefixDot(subRule) + "Pattern_" + formatIdentifiable(subRule)
				+ ".Instance." + formatIdentifiable(subRule) +
				"_Modify(actionEnv, subpattern_" + subName);
		NeededEntities needs = new NeededEntities(true, true, true, false, true, true, false, false);
		List<Entity> replParameters = subRule.getRight().getReplParameters();
		for(int i=0; i<subRep.getReplConnections().size(); ++i) {
			Expression expr = subRep.getReplConnections().get(i);
			Entity param = replParameters.get(i);
			expr.collectNeededEntities(needs);
			sb.append(", ");
			if(param.isDefToBeYieldedTo()) {
				sb.append("ref (");
			} else {
				if(expr instanceof GraphEntityExpression) {
					sb.append("(GRGEN_LGSP.LGSPNode)(");
				} else {
					sb.append("(" + formatAttributeType(expr.getType()) + ") (");
				}
			}
			genExpression(sb, expr, state);
			sb.append(")");
		}
		for(Node node : needs.nodes) {
			nodesNeededAsElements.add(node);
		}
		for(Node node : needs.attrNodes) {
			nodesNeededAsAttributes.add(node);
		}
		for(Edge edge : needs.attrEdges) {
			edgesNeededAsAttributes.add(edge);
		}
		for(Variable var : needs.variables) {
			neededVariables.add(var);
		}
		sb.append(");\n");
	}

	private void genYieldedElementsInterfaceAccess(SourceBuilder sb, ModifyGenerationStateConst state, String pathPrefix) {
		for(Node node : state.yieldedNodes()) {
			sb.appendFront(formatVarDeclWithCast(formatElementInterfaceRef(node.getType()), "i" + formatEntity(node))
					+ formatEntity(node) + ";\n");
		}
		for(Edge edge : state.yieldedEdges()) {
			sb.appendFront(formatVarDeclWithCast(formatElementInterfaceRef(edge.getType()), "i" + formatEntity(edge))
					+ formatEntity(edge) + ";\n");
		}
	}
	
	private void genAddedGraphElementsArray(SourceBuilder sb, String pathPrefix, boolean isNode, Collection<? extends GraphEntity> set) {
		String NodesOrEdges = isNode?"Node":"Edge";
		sb.appendFront("private static string[] " + pathPrefix + "added" + NodesOrEdges + "Names = new string[] ");
		genSet(sb, set, "\"", "\"", true);
		sb.append(";\n");
	}

	private void emitReturnStatement(SourceBuilder sb, ModifyGenerationStateConst state, boolean emitProfiling, String packagePrefixedactionName, List<Expression> returns) {
		if(emitProfiling && returns.size() > 0)
			genEvalProfilingStart(sb, false);
		for(int i = 0; i < returns.size(); i++)
		{
			sb.appendFront("output_" + i + " = ");
			Expression expr = returns.get(i);
			if(expr instanceof GraphEntityExpression)
				sb.append("(" + formatElementInterfaceRef(expr.getType()) + ")(");
			else
				sb.append("(" + formatAttributeType(expr.getType()) + ") (");
			genExpression(sb, expr, state);
			sb.append(");\n");
		}
		if(emitProfiling && returns.size() > 0)
			genEvalProfilingStop(sb, packagePrefixedactionName);
		sb.appendFront("return;\n");
	}

	private void genExtractElementsFromMatch(SourceBuilder sb, ModifyGenerationTask task,
			ModifyGenerationStateConst state, String pathPrefix, String patternName) {
		for(Node node : state.nodesNeededAsElements()) {
			if(node.isRetyped() && node.isRHSEntity())
				continue;
			if(state.yieldedNodes().contains(node))
				continue;
			sb.appendFront("GRGEN_LGSP.LGSPNode " + formatEntity(node)
					+ " = curMatch." + formatEntity(node, "_") + ";\n");
		}
		for(Node node : state.nodesNeededAsAttributes()) {
			if(node.isRetyped() && node.isRHSEntity())
				continue;
			if(state.yieldedNodes().contains(node))
				continue;
			if(task.replParameters.contains(node)) {
				sb.appendFront(formatElementInterfaceRef(node.getType()) + " i" + formatEntity(node)
						+ " = (" + formatElementInterfaceRef(node.getType()) + ")" + formatEntity(node) + ";\n");
				continue; // replacement parameters are handed in as parameters
			}
			sb.appendFront(formatElementInterfaceRef(node.getType()) + " i" + formatEntity(node)
					+ " = curMatch." + formatEntity(node) + ";\n");
		}
		for(Edge edge : state.edgesNeededAsElements()) {
			if(edge.isRetyped() && edge.isRHSEntity())
				continue;
			if(state.yieldedEdges().contains(edge))
				continue;
			sb.appendFront("GRGEN_LGSP.LGSPEdge " + formatEntity(edge)
					+ " = curMatch." + formatEntity(edge, "_") + ";\n");
		}
		for(Edge edge : state.edgesNeededAsAttributes()) {
			if(edge.isRetyped() && edge.isRHSEntity())
				continue;
			if(state.yieldedEdges().contains(edge))
				continue;
			if(task.replParameters.contains(edge)) {
				sb.appendFront(formatElementInterfaceRef(edge.getType()) + " i" + formatEntity(edge)
						+ " = (" + formatElementInterfaceRef(edge.getType()) + ")" + formatEntity(edge) + ";\n");
				continue; // replacement parameters are handed in as parameters
			}
			sb.appendFront(formatElementInterfaceRef(edge.getType()) + " i" + formatEntity(edge)
					+ " = curMatch." + formatEntity(edge) + ";\n");
		}
	}

	private void genExtractVariablesFromMatch(SourceBuilder sb, ModifyGenerationTask task,
			ModifyGenerationStateConst state, String pathPrefix, String patternName) {
		for(Variable var : state.neededVariables()) {
			if(task.replParameters.contains(var)) 
				continue; // skip replacement parameters, they are handed in as parameters
			if(state.yieldedVariables().contains(var))
				continue;
			String type = formatAttributeType(var);
			sb.appendFront(type + " " + formatEntity(var)
					+ " = curMatch."+formatEntity(var, "_")+";\n");
		}
	}

	private void genExtractSubmatchesFromMatch(SourceBuilder sb, String pathPrefix, PatternGraph pattern) {
		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			String subName = formatIdentifiable(sub);
			sb.appendFront(matchType(sub.getSubpatternAction().getPattern(), sub.getSubpatternAction(), true, "")+" subpattern_" + subName
					+ " = curMatch.@_" + formatIdentifiable(sub) + ";\n");
		}
		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			String iterType = "GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefix+pattern.getNameOfGraph()+"_"+iterName
			  + ", IMatch_"+pathPrefix+pattern.getNameOfGraph()+"_"+iterName+">";
			sb.appendFront(iterType+" iterated_" + iterName
					+ " = curMatch._"+iterName+";\n");
		}
		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			String altType = "IMatch_"+pathPrefix+pattern.getNameOfGraph()+"_"+altName;
			sb.appendFront(altType+" alternative_" + altName
					+ " = curMatch._"+altName+";\n");
		}
	}

	////////////////////////////
	// New element generation //
	////////////////////////////

	private void genNewNodes(SourceBuilder sb2, ModifyGenerationStateConst state,
			boolean useAddedElementNames, String pathPrefix,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes) {
		// call nodes added delegate
		if(useAddedElementNames)
			sb2.appendFront("graph.SettingAddedNodeNames( " + pathPrefix + "addedNodeNames );\n");

		LinkedList<Node> tmpNewNodes = new LinkedList<Node>(state.newNodes());

		for(Node node : tmpNewNodes) {
			genNewNode(sb2, state, pathPrefix, nodesNeededAsElements, nodesNeededAsTypes,
					node);
		}
	}

	private void genNewNode(SourceBuilder sb2, ModifyGenerationStateConst state, String pathPrefix,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes,
			Node node) {
		if(node.inheritsType()) { // typeof or copy
			Node typeofElem = (Node) getConcreteTypeofElem(node);
			nodesNeededAsElements.add(typeofElem);
			
			if(node.isCopy()) { // node:copy<typeofElem>
				sb2.appendFront("GRGEN_LGSP.LGSPNode " + formatEntity(node)
						+ " = (GRGEN_LGSP.LGSPNode) "
						+ formatEntity(typeofElem) + ".Clone();\n");
			} else { // node:typeof(typeofElem)
				nodesNeededAsTypes.add(typeofElem);
				sb2.appendFront("GRGEN_LGSP.LGSPNode " + formatEntity(node)
							+ " = (GRGEN_LGSP.LGSPNode) "
							+ formatEntity(typeofElem) + "_type.CreateNode();\n");
			}
			if(node.hasNameInitialization()) {
				sb2.appendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).AddNode(" + formatEntity(node) + ", ");
				genExpression(sb2, node.getNameInitialization().expr, state);
				sb2.append(");\n");
			} else
				sb2.appendFront("graph.AddNode(" + formatEntity(node) + ");\n");
			
			if(state.nodesNeededAsAttributes().contains(node) && state.accessViaInterface().contains(node)) {
				sb2.appendFront(formatVarDeclWithCast(formatElementInterfaceRef(node.getType()), "i" + formatEntity(node))
						+ formatEntity(node) + ";\n");
			}
		} else { // node:type
			String elemref = formatElementClassRef(node.getType());
			if(node.hasNameInitialization()) {
				sb2.appendFront(elemref + " " + formatEntity(node) + " = " + elemref + ".CreateNode((GRGEN_LGSP.LGSPNamedGraph)graph, ");
				genExpression(sb2, node.getNameInitialization().expr, state);
				sb2.append(");\n");
			} else
				sb2.appendFront(elemref + " " + formatEntity(node) + " = " + elemref + ".CreateNode(graph);\n");
		}
	}

	/**
	 * Returns the iterated inherited type element for a given element
	 * or null, if the given element does not inherit its type from another element.
	 */
	private GraphEntity getConcreteTypeofElem(GraphEntity elem) {
		GraphEntity typeofElem = elem;
		while(typeofElem.inheritsType()) {
			typeofElem = typeofElem.getTypeof();
		}
		return typeofElem == elem ? null : typeofElem;
	}

	private void genNewEdges(SourceBuilder sb2, ModifyGenerationStateConst state, ModifyGenerationTask task,
			boolean useAddedElementNames, String pathPrefix,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Edge> edgesNeededAsTypes)
	{
		// call edges added delegate
		if(useAddedElementNames)
			sb2.appendFront("graph.SettingAddedEdgeNames( " + pathPrefix + "addedEdgeNames );\n");

		for(Edge edge : state.newEdges()) {
			Node src_node = task.right.getSource(edge);
			Node tgt_node = task.right.getTarget(edge);
			if(src_node==null || tgt_node==null) {
				return; // don't create dangling edges    - todo: what's the correct way to handle them?
			}

			if(src_node.changesType(task.right))
				src_node = src_node.getRetypedNode(task.right);
			if(tgt_node.changesType(task.right))
				tgt_node = tgt_node.getRetypedNode(task.right);

			if(state.commonNodes().contains(src_node))
				nodesNeededAsElements.add(src_node);

			if(state.commonNodes().contains(tgt_node))
				nodesNeededAsElements.add(tgt_node);

			genNewEdge(sb2, state, pathPrefix, edgesNeededAsElements, edgesNeededAsTypes,
					edge, src_node, tgt_node);
		}
	}

	private void genNewEdge(SourceBuilder sb2, ModifyGenerationStateConst state, String pathPrefix,
			HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes,
			Edge edge, Node src_node, Node tgt_node)
	{
		if(edge.inheritsType()) { // typeof or copy
			Edge typeofElem = (Edge) getConcreteTypeofElem(edge);
			edgesNeededAsElements.add(typeofElem);

			if(edge.isCopy()) { // -edge:copy<typeofElem>->
				sb2.appendFront("GRGEN_LGSP.LGSPEdge " + formatEntity(edge)
						+ " = (GRGEN_LGSP.LGSPEdge) "
						+ formatEntity(typeofElem) + ".Clone("
						+ formatEntity(src_node) + ", " + formatEntity(tgt_node) + ");\n");
			} else { // -edge:typeof(typeofElem)->
				edgesNeededAsTypes.add(typeofElem);
				sb2.appendFront("GRGEN_LGSP.LGSPEdge " + formatEntity(edge)
							+ " = (GRGEN_LGSP.LGSPEdge) "
							+ formatEntity(typeofElem) + "_type.CreateEdge("
							+ formatEntity(src_node) + ", " + formatEntity(tgt_node) + ");\n");
			}
			if(edge.hasNameInitialization()) {
				sb2.appendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).AddEdge(" + formatEntity(edge) + ", ");
				genExpression(sb2, edge.getNameInitialization().expr, state);
				sb2.append(");\n");
			} else
				sb2.appendFront("graph.AddEdge(" + formatEntity(edge) + ");\n");

			if(state.edgesNeededAsAttributes().contains(edge) && state.accessViaInterface().contains(edge)) {
				sb2.appendFront(formatVarDeclWithCast(formatElementInterfaceRef(edge.getType()), "i" + formatEntity(edge))
						+ formatEntity(edge) + ";\n");
			}
		} else { // -edge:type->
			String elemref = formatElementClassRef(edge.getType());
			if(edge.hasNameInitialization()) {
				sb2.appendFront(elemref + " " + formatEntity(edge) + " = " + elemref
						   + ".CreateEdge((GRGEN_LGSP.LGSPNamedGraph)graph, " + formatEntity(src_node)
						   + ", " + formatEntity(tgt_node) + ", ");
				genExpression(sb2, edge.getNameInitialization().expr, state);
				sb2.append(");\n");
			} else
				sb2.appendFront(elemref + " " + formatEntity(edge) + " = " + elemref
						   + ".CreateEdge(graph, " + formatEntity(src_node)
						   + ", " + formatEntity(tgt_node) + ");\n");
		}
	}

	private void genNewSubpatternCalls(SourceBuilder sb, ModifyGenerationStateConst state)
	{
		for(SubpatternUsage subUsage : state.newSubpatternUsages()) {
			if(hasAbstractElements(subUsage.getSubpatternAction().getPattern()) 
				|| hasDanglingEdges(subUsage.getSubpatternAction().getPattern()))
				continue; // pattern creation code was not generated, can't call it

			sb.appendFront("GRGEN_ACTIONS." + getPackagePrefixDot(subUsage.getSubpatternAction()) + "Pattern_" + formatIdentifiable(subUsage.getSubpatternAction())
					+ ".Instance." + formatIdentifiable(subUsage.getSubpatternAction()) +
					"_Create(actionEnv");
			for(Expression expr: subUsage.getSubpatternConnections()) {
				// var parameters can't be used in creation, so just skip them
				if(expr instanceof GraphEntityExpression) {
					sb.append(", ");
					sb.append("(" + formatElementClassRef(expr.getType()) + ")(");
					genExpression(sb, expr, state);
					sb.append(")");
				}
			}
			sb.append(");\n");
		}
	}

	private void genDelSubpatternCalls(SourceBuilder sb, ModifyGenerationStateConst state)
	{
		for(SubpatternUsage subUsage : state.delSubpatternUsages()) {
			String subName = formatIdentifiable(subUsage);
			sb.appendFront("GRGEN_ACTIONS." + getPackagePrefixDot(subUsage.getSubpatternAction()) + "Pattern_" + formatIdentifiable(subUsage.getSubpatternAction())
					+ ".Instance." + formatIdentifiable(subUsage.getSubpatternAction()) +
					"_Delete(actionEnv, subpattern_" + subName + ");\n");
		}
	}

	//////////////////////
	// Expression stuff //
	//////////////////////

	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState) {
		genQualAccess(sb, qual, (ModifyGenerationState)modifyGenerationState);
	}

	protected void genQualAccess(SourceBuilder sb, Qualification qual, ModifyGenerationStateConst state) {
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		if(owner.getType() instanceof MatchType || owner.getType() instanceof DefinedMatchType) {
			sb.append(formatEntity(owner) + "." + formatEntity(member));
		} else {
			genQualAccess(sb, state, owner, member);
		}
	}

	protected void genQualAccess(SourceBuilder sb, ModifyGenerationStateConst state, Entity owner, Entity member) {
		if(!Expression.isGlobalVariable(owner)) {
			if(state==null) {
				assert false;
				sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
				return;
			}
	
			if(accessViaVariable(state, /*(GraphEntity)*/owner, member)) {
				sb.append("tempvar_" + formatEntity(owner) + "_" + formatIdentifiable(member));
			} else {
				if(state.accessViaInterface().contains(owner))
					sb.append("i");
	
				sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
			}
		} else {
			sb.append(formatGlobalVariableRead(owner));
			sb.append(".@" + formatIdentifiable(member));
		}
	}

	protected void genMemberAccess(SourceBuilder sb, Entity member) {
		// needed in implementing methods
		sb.append("@" + formatIdentifiable(member));
	}

	private boolean accessViaVariable(ModifyGenerationStateConst state, Entity elem, Entity attr) {
		HashSet<Entity> forcedAttrs = state.forceAttributeToVar().get(elem);
		return forcedAttrs != null && forcedAttrs.contains(attr);
	}

	private void genVariable(SourceBuilder sb, String ownerName, Entity entity) {
		String varTypeName;
		String attrName = formatIdentifiable(entity);
		Type type = entity.getType();
		if(type instanceof EnumType) {
			varTypeName = "GRGEN_MODEL." + getPackagePrefixDot(type) + "ENUM_" + formatIdentifiable(type);
		} else {
			varTypeName = getTypeNameForTempVarDecl(type);
		}

		sb.appendFront(varTypeName + " tempvar_" + ownerName + "_" + attrName);
	}
}
