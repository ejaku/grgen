/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the actions file for the SearchPlanBackend2 backend.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.ir.containers.*;


public class ModifyGen extends CSharpBase {
	final int TYPE_OF_TASK_NONE = 0;
	final int TYPE_OF_TASK_MODIFY = 1;
	final int TYPE_OF_TASK_CREATION = 2;
	final int TYPE_OF_TASK_DELETION = 3;

	class ModifyGenerationTask {
		int typeOfTask;
		PatternGraph left;
		PatternGraph right;
		List<Entity> parameters;
		Collection<EvalStatements> evals;
		List<Entity> replParameters;
		List<Expression> returns;
		boolean isSubpattern;
		boolean mightThereBeDeferredExecs;

		public ModifyGenerationTask() {
			typeOfTask = TYPE_OF_TASK_NONE;
			left = null;
			right = null;
			parameters = null;
			evals = null;
			replParameters = null;
			returns = null;
			isSubpattern = false;
			mightThereBeDeferredExecs = false;
		}
	}

	interface ModifyGenerationStateConst extends ExpressionGenerationState {
		String computationName();
		
		Collection<Node> commonNodes();
		Collection<Edge> commonEdges();
		Collection<SubpatternUsage> commonSubpatternUsages();

		Collection<Node> newNodes();
		Collection<Edge> newEdges();
		Collection<SubpatternUsage> newSubpatternUsages();

		Collection<Node> delNodes();
		Collection<Edge> delEdges();
		Collection<SubpatternUsage> delSubpatternUsages();
		
		Collection<Node> yieldedNodes();
		Collection<Edge> yieldedEdges();		
		Collection<Variable> yieldedVariables();

		Collection<Node> newOrRetypedNodes();
		Collection<Edge> newOrRetypedEdges();
		Collection<GraphEntity> accessViaInterface();

		Map<GraphEntity, HashSet<Entity>> neededAttributes();
		Map<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete();

		Collection<Variable> neededVariables();

		Collection<Node> nodesNeededAsElements();
		Collection<Edge> edgesNeededAsElements();
		Collection<Node> nodesNeededAsAttributes();
		Collection<Edge> edgesNeededAsAttributes();
		Collection<Node> nodesNeededAsTypes();
		Collection<Edge> edgesNeededAsTypes();

		Map<GraphEntity, HashSet<Entity>> forceAttributeToVar();
	}

	class ModifyGenerationState implements ModifyGenerationStateConst {
		public String computationName() { return computationName; }

		public Collection<Node> commonNodes() { return Collections.unmodifiableCollection(commonNodes); }
		public Collection<Edge> commonEdges() { return Collections.unmodifiableCollection(commonEdges); }
		public Collection<SubpatternUsage> commonSubpatternUsages() { return Collections.unmodifiableCollection(commonSubpatternUsages); }

		public Collection<Node> newNodes() { return Collections.unmodifiableCollection(newNodes); }
		public Collection<Edge> newEdges() { return Collections.unmodifiableCollection(newEdges); }
		public Collection<SubpatternUsage> newSubpatternUsages() { return Collections.unmodifiableCollection(newSubpatternUsages); }

		public Collection<Node> delNodes() { return Collections.unmodifiableCollection(delNodes); }
		public Collection<Edge> delEdges() { return Collections.unmodifiableCollection(delEdges); }
		public Collection<SubpatternUsage> delSubpatternUsages() { return Collections.unmodifiableCollection(delSubpatternUsages); }

		public Collection<Node> yieldedNodes() { return Collections.unmodifiableCollection(yieldedNodes); }
		public Collection<Edge> yieldedEdges() { return Collections.unmodifiableCollection(yieldedEdges); }
		public Collection<Variable> yieldedVariables() { return Collections.unmodifiableCollection(yieldedVariables); }

		public Collection<Node> newOrRetypedNodes() { return Collections.unmodifiableCollection(newOrRetypedNodes); }
		public Collection<Edge> newOrRetypedEdges() { return Collections.unmodifiableCollection(newOrRetypedEdges); }
		public Collection<GraphEntity> accessViaInterface() { return Collections.unmodifiableCollection(accessViaInterface); }

		public Map<GraphEntity, HashSet<Entity>> neededAttributes() { return Collections.unmodifiableMap(neededAttributes); }
		public Map<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete() { return Collections.unmodifiableMap(attributesStoredBeforeDelete); }

		public Collection<Variable> neededVariables() { return Collections.unmodifiableCollection(neededVariables); }

		public Collection<Node> nodesNeededAsElements() { return Collections.unmodifiableCollection(nodesNeededAsElements); }
		public Collection<Edge> edgesNeededAsElements() { return Collections.unmodifiableCollection(edgesNeededAsElements); }
		public Collection<Node> nodesNeededAsAttributes() { return Collections.unmodifiableCollection(nodesNeededAsAttributes); }
		public Collection<Edge> edgesNeededAsAttributes() { return Collections.unmodifiableCollection(edgesNeededAsAttributes); }
		public Collection<Node> nodesNeededAsTypes() { return Collections.unmodifiableCollection(nodesNeededAsTypes); }
		public Collection<Edge> edgesNeededAsTypes() { return Collections.unmodifiableCollection(edgesNeededAsTypes); }

		public Map<GraphEntity, HashSet<Entity>> forceAttributeToVar() { return Collections.unmodifiableMap(forceAttributeToVar); }

		public Map<Expression, String> mapExprToTempVar() { return Collections.unmodifiableMap(mapExprToTempVar); }
		public boolean useVarForResult() { return useVarForResult; }

		// --------------------

		// if not null this is the generation state of a computation (with all entries empty)
		// otherwise it is the generation state of the modify of an action
		public String computationName;
		
		public HashSet<Node> commonNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> commonEdges = new LinkedHashSet<Edge>();
		public HashSet<SubpatternUsage> commonSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public HashSet<Node> newNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> newEdges = new LinkedHashSet<Edge>();
		public HashSet<SubpatternUsage> newSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public HashSet<Node> delNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> delEdges = new LinkedHashSet<Edge>();
		public HashSet<SubpatternUsage> delSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public HashSet<Node> yieldedNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> yieldedEdges = new LinkedHashSet<Edge>();
		public HashSet<Variable> yieldedVariables = new LinkedHashSet<Variable>();

		public HashSet<Node> newOrRetypedNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> newOrRetypedEdges = new LinkedHashSet<Edge>();
		public HashSet<GraphEntity> accessViaInterface = new LinkedHashSet<GraphEntity>();

		public HashMap<GraphEntity, HashSet<Entity>> neededAttributes;
		public HashMap<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

		public HashSet<Variable> neededVariables;

		public HashSet<Node> nodesNeededAsElements;
		public HashSet<Edge> edgesNeededAsElements;
		public HashSet<Node> nodesNeededAsAttributes;
		public HashSet<Edge> edgesNeededAsAttributes;

		public HashSet<Node> nodesNeededAsTypes = new LinkedHashSet<Node>();
		public HashSet<Edge> edgesNeededAsTypes = new LinkedHashSet<Edge>();

		public HashMap<GraphEntity, HashSet<Entity>> forceAttributeToVar = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

		public HashMap<Expression, String> mapExprToTempVar = new LinkedHashMap<Expression, String>();
		public boolean useVarForResult;


		public void InitNeeds(NeededEntities needs) {
			neededAttributes = needs.attrEntityMap;
			nodesNeededAsElements = needs.nodes;
			edgesNeededAsElements = needs.edges;
			nodesNeededAsAttributes = needs.attrNodes;
			edgesNeededAsAttributes = needs.attrEdges;
			neededVariables = needs.variables;

			int i = 0;
			for(Expression expr : needs.containerExprs) {
				if(expr instanceof MapInit || expr instanceof SetInit 
						|| expr instanceof ArrayInit || expr instanceof DequeInit)
					continue;
				mapExprToTempVar.put(expr, "tempcontainervar_" + i);
				i++;
			}
		}
	}

	final List<Entity> emptyParameters = new LinkedList<Entity>();
	final List<Expression> emptyReturns = new LinkedList<Expression>();
	final Collection<EvalStatements> emptyEvals = new LinkedList<EvalStatements>();

	// eval statement generation state
	int tmpVarID;
	int embeddedComputationXgrsID;

	SearchPlanBackend2 be;
	
	
	public ModifyGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix) {
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
	}

	//////////////////////////////////
	// Modification part generation //
	//////////////////////////////////

	public void genModify(StringBuffer sb, Rule rule, boolean isSubpattern) {
		genModify(sb, rule, "", "pat_"+rule.getLeft().getNameOfGraph(), isSubpattern);
	}

	private void genModify(StringBuffer sb, Rule rule, String pathPrefix, String patGraphVarName, boolean isSubpattern)
	{
		if(rule.getRight()!=null) { // rule / subpattern with dependent replacement
			// replace left by right, normal version
			ModifyGenerationTask task = new ModifyGenerationTask();
			task.typeOfTask = TYPE_OF_TASK_MODIFY;
			task.left = rule.getLeft();
			task.right = rule.getRight();
			task.parameters = rule.getParameters();
			task.evals = rule.getEvals();
			task.replParameters = rule.getRight().getReplParameters();
			task.returns = rule.getReturns();
			task.isSubpattern = isSubpattern;
			task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
			genModifyRuleOrSubrule(sb, task, pathPrefix);
		} else if(!isSubpattern){ // test
			// keep left unchanged, normal version
			ModifyGenerationTask task = new ModifyGenerationTask();
			task.typeOfTask = TYPE_OF_TASK_MODIFY;
			task.left = rule.getLeft();
			task.right = rule.getLeft();
			task.parameters = rule.getParameters();
			task.evals = rule.getEvals();
			task.replParameters = emptyParameters;
			task.returns = rule.getReturns();
			task.isSubpattern = false;
			task.mightThereBeDeferredExecs = rule.mightThereBeDeferredExecs;
			genModifyRuleOrSubrule(sb, task, pathPrefix);
		}

		if(isSubpattern) {
			if(pathPrefix.isEmpty() 
					&& !hasAbstractElements(rule.getLeft())
					&& !hasDanglingEdges(rule.getLeft())) {
				// create subpattern into pattern
				ModifyGenerationTask task = new ModifyGenerationTask();
				task.typeOfTask = TYPE_OF_TASK_CREATION;
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
				genModifyRuleOrSubrule(sb, task, pathPrefix);
			}

			// delete subpattern from pattern
			ModifyGenerationTask task = new ModifyGenerationTask();
			task.typeOfTask = TYPE_OF_TASK_DELETION;
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
			genModifyRuleOrSubrule(sb, task, pathPrefix);
		}

		for(Alternative alt : rule.getLeft().getAlts()) {
			String altName = alt.getNameOfGraph();

			genModifyAlternative(sb, rule, alt, pathPrefix+rule.getLeft().getNameOfGraph()+"_", altName, isSubpattern);

			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altCasePatGraphVarName = pathPrefix+rule.getLeft().getNameOfGraph()+"_"+altName+"_"+altCasePattern.getNameOfGraph();
				genModify(sb, altCase, pathPrefix+rule.getLeft().getNameOfGraph()+"_"+altName+"_", altCasePatGraphVarName, isSubpattern);
			}
		}

		for(Rule iter : rule.getLeft().getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			String iterPatGraphVarName = pathPrefix+rule.getLeft().getNameOfGraph()+"_"+iterName;
			genModifyIterated(sb, iter, pathPrefix+rule.getLeft().getNameOfGraph()+"_", iterName, isSubpattern);
			genModify(sb, iter, pathPrefix+rule.getLeft().getNameOfGraph()+"_", iterPatGraphVarName, isSubpattern);
		}
	}

	/**
	 * Checks whether the given pattern contains abstract elements.
	 */
	private boolean hasAbstractElements(PatternGraph left) {
		for(Node node : left.getNodes())
			if(node.getNodeType().isAbstract()) return true;

		for(Edge edge : left.getEdges())
			if(edge.getEdgeType().isAbstract()) return true;

		return false;
	}

	private boolean hasDanglingEdges(PatternGraph left) {
		for(Edge edge : left.getEdges())
			if(left.getSource(edge)==null || left.getTarget(edge)==null)
				return true;

		return false;
	}

	private void genModifyAlternative(StringBuffer sb, Rule rule, Alternative alt,
			String pathPrefix, String altName, boolean isSubpattern) {
		if(rule.getRight()!=null) { // generate code for dependent modify dispatcher
			genModifyAlternativeModify(sb, alt, pathPrefix, altName, isSubpattern);
		}

		if(isSubpattern) { // generate for delete alternative dispatcher
			genModifyAlternativeDelete(sb, alt, pathPrefix, altName, isSubpattern);
		}
	}

	private void genModifyAlternativeModify(StringBuffer sb, Alternative alt, String pathPrefix, String altName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.append("\t\tpublic void "
					  + pathPrefix+altName+"_Modify"
					  + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, IMatch_"+pathPrefix+altName+" curMatch");
		List<Entity> replParameters = new LinkedList<Entity>();
		getUnionOfReplaceParametersOfAlternativeCases(alt, replParameters);
		for(Entity entity : replParameters) {
			if(entity instanceof Node) {
				Node node = (Node)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo()) sb.append("ref ");
				sb.append("GRGEN_LGSP.LGSPNode " + formatEntity(node));
			} else {
				Variable var = (Variable)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo()) sb.append("ref ");
				sb.append(formatAttributeType(var)+ " " + formatEntity(var));
			}
		}
		sb.append(")\n");
		sb.append("\t\t{\n");

		// Emit dispatcher calling the modify-method of the alternative case which was matched
		boolean firstCase = true;
		for(Rule altCase : alt.getAlternativeCases()) {
			if(firstCase) {
				sb.append("\t\t\tif(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
				firstCase = false;
			}
			else
			{
				sb.append("\t\t\telse if(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
			}
			sb.append("\t\t\t\t" + pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+"_Modify"
					+ "(actionEnv, (Match_"+pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+")curMatch");
			replParameters = altCase.getRight().getReplParameters();
			for(Entity entity : replParameters) {
				sb.append(", ");
				if(entity.isDefToBeYieldedTo()) sb.append("ref ");
				sb.append(formatEntity(entity));
			}
			sb.append(");\n");
			sb.append("\t\t\t\treturn;\n");
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new ApplicationException(); //debug assert\n");

		// Emit end of function
		sb.append("\t\t}\n");
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

	private void genModifyAlternativeDelete(StringBuffer sb, Alternative alt, String pathPrefix, String altName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.append("\t\tpublic void " + pathPrefix+altName+"_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
				+ "IMatch_"+pathPrefix+altName+" curMatch)\n");
		sb.append("\t\t{\n");

		// Emit dispatcher calling the delete-method of the alternative case which was matched
		boolean firstCase = true;
		for(Rule altCase : alt.getAlternativeCases()) {
			if(firstCase) {
				sb.append("\t\t\tif(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
				firstCase = false;
			}
			else
			{
				sb.append("\t\t\telse if(curMatch.Pattern == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
			}
			sb.append("\t\t\t\t" + pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+"_"
					+ "Delete(actionEnv, (Match_"+pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+")curMatch);\n");
			sb.append("\t\t\t\treturn;\n");
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new ApplicationException(); //debug assert\n");

		// Emit end of function
		sb.append("\t\t}\n");
	}

	private void genModifyIterated(StringBuffer sb, Rule rule, String pathPrefix, String iterName, boolean isSubpattern) {
		if(rule.getRight()!=null) { // generate code for dependent modify dispatcher
			genModifyIteratedModify(sb, rule, pathPrefix, iterName, isSubpattern);
		}

		if(isSubpattern) { // generate for delete iterated dispatcher
			genModifyIteratedDelete(sb, rule, pathPrefix, iterName, isSubpattern);
		}
	}

	private void genModifyIteratedModify(StringBuffer sb, Rule iter, String pathPrefix, String iterName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.append("\t\tpublic void "+pathPrefix+iterName+"_Modify"
					  + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
					  + "GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefix+iterName
					  + ", IMatch_"+pathPrefix+iterName+"> curMatches");
		List<Entity> replParameters = iter.getRight().getReplParameters();
		for(Entity entity : replParameters) {
			if(entity instanceof Node) {
				Node node = (Node)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo()) sb.append("ref ");
				sb.append("GRGEN_LGSP.LGSPNode " + formatEntity(node));
			} else {
				Variable var = (Variable)entity;
				sb.append(", ");
				if(entity.isDefToBeYieldedTo()) sb.append("ref ");
				sb.append(formatAttributeType(var)+ " " + formatEntity(var));
			}
		}
		sb.append(")\n");
		sb.append("\t\t{\n");

		// Emit dispatcher calling the modify-method of the iterated pattern which was matched
		sb.append("\t\t\tfor(Match_"+pathPrefix+iterName+" curMatch=curMatches.Root;"
				+" curMatch!=null; curMatch=curMatch.next) {\n");
		sb.append("\t\t\t\t" + pathPrefix+iterName+"_Modify"
				+ "(actionEnv, curMatch");
		for(Entity entity : replParameters) {
			sb.append(", ");
			if(entity.isDefToBeYieldedTo()) sb.append("ref ");
			sb.append(formatEntity(entity));
		}
		sb.append(");\n");
		sb.append("\t\t\t}\n");

		// Emit end of function
		sb.append("\t\t}\n");
	}

	private void genModifyIteratedDelete(StringBuffer sb, Rule iter, String pathPrefix, String iterName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.append("\t\tpublic void "+pathPrefix+iterName+"_Delete"
					  + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "
					  + "GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefix+iterName
					  + ", IMatch_"+pathPrefix+iterName+"> curMatches)\n");
		sb.append("\t\t{\n");

		// Emit dispatcher calling the modify-method of the iterated pattern which was matched
		sb.append("\t\t\tfor(Match_"+pathPrefix+iterName+" curMatch=curMatches.Root;"
				+" curMatch!=null; curMatch=curMatch.next) {\n");
		sb.append("\t\t\t\t" + pathPrefix+iterName+"_Delete"
				+ "(actionEnv, curMatch);\n");
		sb.append("\t\t\t}\n");

		// Emit end of function
		sb.append("\t\t}\n");
	}

	private void genModifyRuleOrSubrule(StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		StringBuffer sb2 = new StringBuffer();
		StringBuffer sb3 = new StringBuffer();

		boolean useAddedElementNames = be.system.mayFireEvents()
			&& (task.typeOfTask==TYPE_OF_TASK_CREATION
				|| (task.typeOfTask==TYPE_OF_TASK_MODIFY && task.left!=task.right));
		boolean createAddedElementNames = task.typeOfTask==TYPE_OF_TASK_CREATION ||
			(task.typeOfTask==TYPE_OF_TASK_MODIFY && task.left!=task.right);
		String prefix = (task.typeOfTask==TYPE_OF_TASK_CREATION ? "create_" : "")
			+ pathPrefix+task.left.getNameOfGraph()+"_";

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

		ModifyGenerationState state = new ModifyGenerationState();
		ModifyGenerationStateConst stateConst = state;

		collectYieldedElements(task, stateConst, state.yieldedNodes, state.yieldedEdges, state.yieldedVariables);

		collectCommonElements(task, state.commonNodes, state.commonEdges, state.commonSubpatternUsages);

		collectNewElements(task, stateConst, state.newNodes, state.newEdges, state.newSubpatternUsages);

		collectDeletedElements(task, stateConst, state.delNodes, state.delEdges, state.delSubpatternUsages);
		
		collectNewOrRetypedElements(task, state, state.newOrRetypedNodes, state.newOrRetypedEdges);

		collectElementsAccessedByInterface(task, state.accessViaInterface);

		NeededEntities needs = new NeededEntities(true, true, true, false, true, true, false);
		collectElementsAndAttributesNeededByImperativeStatements(task, needs);
		collectElementsAndAttributesNeededByReturns(task, needs);
		collectElementsNeededBySubpatternCreation(task, needs);

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
		needs.collectContainerExprs = false;
		collectElementsAndAttributesNeededByEvals(task, needs);
		needs.collectContainerExprs = true;

		// Fill state with information gathered in needs
		state.InitNeeds(needs);

		genNewNodes(sb2, stateConst, useAddedElementNames, prefix,
				state.nodesNeededAsElements, state.nodesNeededAsTypes);

		// generates subpattern modification calls, evalhere statements, emithere statements,
		// and alternative/iterated modification calls if specified (if not, they are generated below)
		initEvalGen();
		genSubpatternModificationCalls(sb2, task, pathPrefix, stateConst,
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

		genAllEvals(sb3, stateConst, task.evals);

		genVariablesForUsedAttributesBeforeDelete(sb3, stateConst, state.forceAttributeToVar);

		genCheckDeletedElementsForRetypingThroughHomomorphy(sb3, stateConst);

		genDelEdges(sb3, stateConst, state.edgesNeededAsElements, task.right);

		genDelNodes(sb3, stateConst, state.nodesNeededAsElements, task.right);

		genDelSubpatternCalls(sb3, stateConst);

		genContainerVariablesBeforeImperativeStatements(sb3, stateConst);

		state.useVarForResult = true;
		genImperativeStatements(sb3, stateConst, task, pathPrefix);
		state.useVarForResult = false;

		genCheckReturnedElementsForDeletionOrRetypingDueToHomomorphy(sb3, task);

		// Emit return (only if top-level rule)
		if(pathPrefix=="" && !task.isSubpattern)
			emitReturnStatement(sb3, stateConst, task.returns);

		// Emit end of function
		sb3.append("\t\t}\n");

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
		sb.append(sb2);

		// Attribute re-calc, attr vars for emit, remove, emit, return
		sb.append(sb3);

		// ----------

		if(createAddedElementNames) {
			genAddedGraphElementsArray(sb, stateConst, prefix, task.typeOfTask);
		}
	}

	private void emitMethodHeadAndBegin(StringBuffer sb, ModifyGenerationTask task, String pathPrefix)
	{
		String matchType = "Match_"+pathPrefix+task.left.getNameOfGraph();
		StringBuffer outParameters = new StringBuffer();
		int i=0;
		for(Expression expr : task.returns) {
			outParameters.append(", out ");
			if(expr instanceof GraphEntityExpression)
				outParameters.append(formatElementInterfaceRef(expr.getType()));
			else
				outParameters.append(formatAttributeType(expr.getType()));
			outParameters.append(" output_"+i);
			++i;
		}

		switch(task.typeOfTask) {
		case TYPE_OF_TASK_MODIFY:
			if(pathPrefix=="" && !task.isSubpattern) {
				sb.append("\t\tpublic void "
						+ "Modify"
						+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch"
						+ outParameters + ")\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\tGRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
				sb.append("\t\t\t"+matchType+" curMatch = ("+matchType+")_curMatch;\n");
			} else {
				sb.append("\t\tpublic void "
						+ pathPrefix+task.left.getNameOfGraph() + "_Modify"
						+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch");
				for(Entity entity : task.replParameters) {
					if(entity instanceof Node) {
						Node node = (Node)entity;
						sb.append(", ");
						if(entity.isDefToBeYieldedTo()) sb.append("ref ");
						sb.append("GRGEN_LGSP.LGSPNode " + formatEntity(node));
					} else {
						Variable var = (Variable)entity;
						sb.append(", ");
						if(entity.isDefToBeYieldedTo()) sb.append("ref ");
						sb.append(formatAttributeType(var)+ " " + formatEntity(var));
					}
				}
				sb.append(")\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\tGRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
				sb.append("\t\t\t"+matchType+" curMatch = ("+matchType+")_curMatch;\n");
			}
			break;
		case TYPE_OF_TASK_CREATION:
			sb.append("\t\tpublic void "
					+ pathPrefix+task.left.getNameOfGraph() + "_Create"
					+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv");
			for(Entity entity : task.parameters) {
				if(entity instanceof Node) {
					sb.append(", GRGEN_LGSP.LGSPNode " + formatEntity(entity));					
				} else if (entity instanceof Edge) {
					sb.append(", GRGEN_LGSP.LGSPEdge " + formatEntity(entity));
				} else {
					// var parameters can't be used in creation, so just skip them
					//sb.append(", " + formatAttributeType(entity.getType()) + " " + formatEntity(entity));
				}
			}
			sb.append(")\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\tGRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
			break;
		case TYPE_OF_TASK_DELETION:
			sb.append("\t\tpublic void "
					+ pathPrefix+task.left.getNameOfGraph() + "_Delete"
					+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, "+matchType+" curMatch)\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\tGRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
			break;
		default:
			assert false;
		}
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
		if(task.typeOfTask==TYPE_OF_TASK_CREATION) {
			nodesNeededAsElements.removeAll(task.parameters);
			//nodesNeededAsAttributes.removeAll(state.newNodes);
			edgesNeededAsElements.removeAll(task.parameters);
			//edgesNeededAsAttributes.removeAll(state.newEdges);
			neededVariables.removeAll(task.parameters);
		}

		// nodes handed in as replacement connections to modify are already available as method parameters
		if(task.typeOfTask==TYPE_OF_TASK_MODIFY) {
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
				for(Expression arg : emit.getArguments())
					arg.collectNeededEntities(needs);
			}
			else if (istmt instanceof Exec) {
				Exec exec = (Exec) istmt;
				for(Expression arg : exec.getArguments())
					arg.collectNeededEntities(needs);
			}
			else assert false : "unknown ImperativeStmt: " + istmt + " in " + task.left.getNameOfGraph();
		}

		for(OrderedReplacements orpls : task.right.getOrderedReplacements()) {
			for(OrderedReplacement orpl : orpls.orderedReplacements) {
				if(orpl instanceof Emit) {
					Emit emit = (Emit) orpl;
					for(Expression arg : emit.getArguments())
						arg.collectNeededEntities(needs);
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
		for(Variable var : state.yieldedVariables())
			if(var.initialization!=null)
				var.initialization.collectNeededEntities(needs);
	}

	private void collectElementsAndAttributesNeededByReturns(ModifyGenerationTask task,
			NeededEntities needs)
	{
		for(Expression expr : task.returns)
			expr.collectNeededEntities(needs);
	}

	private void collectElementsNeededBySubpatternCreation(ModifyGenerationTask task,
			NeededEntities needs)
	{
		for(SubpatternUsage subUsage : task.right.getSubpatternUsages())
			for(Expression expr : subUsage.getSubpatternConnections())
				expr.collectNeededEntities(needs);
	}

	private void genNeededTypes(StringBuffer sb, ModifyGenerationStateConst state)
	{
		for(Node node : state.nodesNeededAsTypes()) {
			String name = formatEntity(node);
			sb.append("\t\t\tGRGEN_LIBGR.NodeType " + name + "_type = " + name + ".lgspType;\n");
		}
		for(Edge edge : state.edgesNeededAsTypes()) {
			String name = formatEntity(edge);
			sb.append("\t\t\tGRGEN_LIBGR.EdgeType " + name + "_type = " + name + ".lgspType;\n");
		}
	}

	private void genYieldedElements(StringBuffer sb, ModifyGenerationStateConst state, PatternGraph right)
	{
		for(Node node : state.yieldedNodes()) {
			if(right.getReplParameters().contains(node)) continue;
			sb.append("\t\t\tGRGEN_LGSP.LGSPNode " + formatEntity(node)+ " = null;\n");
		}
		for(Edge edge : state.yieldedEdges()) {
			if(right.getReplParameters().contains(edge)) continue;
			sb.append("\t\t\tGRGEN_LGSP.LGSPEdge " + formatEntity(edge) + " = null;\n");
		}
		for(Variable var : state.yieldedVariables()) {
			if(right.getReplParameters().contains(var)) continue;
			sb.append("\t\t\t" + formatAttributeType(var.getType()) + " " + formatEntity(var) + " = ");
			if(var.initialization!=null) {
				if(var.getType() instanceof EnumType)
					sb.append("(GRGEN_MODEL.ENUM_" + formatIdentifiable(var.getType()) + ") ");
				genExpression(sb, var.initialization, state);
				sb.append(";\n");
			} else {
				sb.append(getInitializationValue(var.getType()) + ";\n");
			}
		}
	}

	private void genCheckReturnedElementsForDeletionOrRetypingDueToHomomorphy(
			StringBuffer sb, ModifyGenerationTask task)
	{
		for(Expression expr : task.returns) {
			if(!(expr instanceof GraphEntityExpression)) continue;

			GraphEntity grEnt = ((GraphEntityExpression) expr).getGraphEntity();
			if(grEnt.isMaybeRetyped()) {
				String elemName = formatEntity(grEnt);
				String kind = formatNodeOrEdge(grEnt);
				sb.append("\t\t\tif(" + elemName + ".ReplacedBy" + kind + " != null) "
					+ elemName + " = " + elemName + ".ReplacedBy" + kind + ";\n");
			}
			if(grEnt.isMaybeDeleted())
				sb.append("\t\t\tif(!" + formatEntity(grEnt) + ".Valid) " + formatEntity(grEnt) + " = null;\n");
		}
	}

	private void genContainerVariablesBeforeImperativeStatements(StringBuffer sb, ModifyGenerationStateConst state) {
		for(Map.Entry<Expression, String> entry : state.mapExprToTempVar().entrySet()) {
			Expression expr = entry.getKey();
			String varName = entry.getValue();
			sb.append("\t\t\t" + formatAttributeType(expr.getType()) + " " + varName + " = ");
			genExpression(sb, expr, state);
			sb.append(";\n");
		}
	}

	private void genImperativeStatements(StringBuffer sb, ModifyGenerationStateConst state, 
			ModifyGenerationTask task, String pathPrefix)
	{
		if(!task.mightThereBeDeferredExecs) { // procEnv was already emitted in case of deferred execs
			if(!task.right.getImperativeStmts().isEmpty()) { // we need it?
				boolean emitHereNeeded = false;
				for(OrderedReplacements orderedReps : task.right.getOrderedReplacements()) {
					for(OrderedReplacement orderedRep : orderedReps.orderedReplacements) {
						if(orderedRep instanceof Emit) { // emithere
							emitHereNeeded = true;
							break;
						}
					}
				}
				
				// see genSubpatternModificationCalls why not simply emitting in case of !task.right.getImperativeStmts().isEmpty()
				if(!emitHereNeeded) { // it was not already emitted?
					sb.append("\t\t\tGRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;\n");
				}
			}
		}

		if(task.mightThereBeDeferredExecs) {
			sb.append("\t\t\tprocEnv.sequencesManager.ExecuteDeferredSequencesThenExitRuleModify(procEnv);\n");
		}

		int xgrsID = 0;
		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit) istmt;
				for(Expression arg : emit.getArguments()) {
					sb.append("\t\t\tprocEnv.EmitWriter.Write(");
					genExpression(sb, arg, state);
					sb.append(");\n");
				}
			} else if (istmt instanceof Exec) {
				Exec exec = (Exec) istmt;

				if(task.isSubpattern || pathPrefix!="") {
					String closureName = "XGRSClosure_" + pathPrefix + task.left.getNameOfGraph() + "_" + xgrsID;
					sb.append("\t\t\t" + closureName + " xgrs"+xgrsID + " = "
							+"new "+ closureName + "(");
					boolean first = true;
					for(Entity neededEntity : exec.getNeededEntities(false)) {
						if(first) {
							first = false;
						} else {
							sb.append(", ");
						}
						if(neededEntity.getType() instanceof InheritanceType) {
							sb.append("("+formatElementInterfaceRef(neededEntity.getType())+")");
						}
						sb.append(formatEntity(neededEntity));
					}
					sb.append(");\n");
					sb.append("\t\t\tprocEnv.sequencesManager.AddDeferredSequence(xgrs"+xgrsID+");\n");
				} else {
					for(Entity neededEntity : exec.getNeededEntities(false)) {
						if(neededEntity.isDefToBeYieldedTo()) {
							if(neededEntity instanceof GraphEntity) {
								sb.append("\t\t\t" + formatElementInterfaceRef(neededEntity.getType()) + " ");
								sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + " = ");
								sb.append("("+formatElementInterfaceRef(neededEntity.getType())+")");
								sb.append(formatEntity(neededEntity) + ";\n");
							}
							else { // if(neededEntity instanceof Variable) 
								sb.append("\t\t\t" + formatAttributeType(neededEntity.getType()) + " ");
								sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + " = ");
								sb.append("("+formatAttributeType(neededEntity.getType())+")");
								sb.append(formatEntity(neededEntity) + ";\n");
							}
						}
					}
					sb.append("\t\t\tApplyXGRS_" + task.left.getNameOfGraph() + "_" + xgrsID + "(procEnv");
					for(Entity neededEntity : exec.getNeededEntities(false)) {
						if(!neededEntity.isDefToBeYieldedTo()) {
							sb.append(", ");
							if(neededEntity.getType() instanceof InheritanceType) {
								sb.append("("+formatElementInterfaceRef(neededEntity.getType())+")");
							}
							sb.append(formatEntity(neededEntity));
						}
					}
					for(Entity neededEntity : exec.getNeededEntities(false)) {
						if(neededEntity.isDefToBeYieldedTo()) {
							sb.append(", ref ");
							sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID);
						}
					}
					sb.append(");\n");
					for(Entity neededEntity : exec.getNeededEntities(false)) {
						if(neededEntity.isDefToBeYieldedTo()) {
							sb.append("\t\t\t" + formatEntity(neededEntity) + " = ");
							if(neededEntity instanceof Node) {
								sb.append("(GRGEN_LGSP.LGSPNode)");
							} else if(neededEntity instanceof Edge) {
								sb.append("(GRGEN_LGSP.LGSPEdge)");
							}
							sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + ";\n");
						}
					}
				}
/*				for(Expression arg : exec.getArguments()) {
					if(!(arg instanceof GraphEntityExpression)) continue;
					sb.append(", ");
					genExpression(sb, arg, state);
				}*/
				
				++xgrsID;
			} else assert false : "unknown ImperativeStmt: " + istmt + " in " + task.left.getNameOfGraph();
		}
	}

	private void genVariablesForUsedAttributesBeforeDelete(StringBuffer sb,
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

	private void genCheckDeletedElementsForRetypingThroughHomomorphy(StringBuffer sb, ModifyGenerationStateConst state)
	{
		for(Edge edge : state.delEdges()) {
			if(!edge.isMaybeRetyped()) continue;

			String edgeName = formatEntity(edge);
			sb.append("\t\t\tif(" + edgeName + ".ReplacedByEdge != null) "
					+ edgeName + " = " + edgeName + ".ReplacedByEdge;\n");
		}
		for(Node node : state.delNodes()) {
			if(!node.isMaybeRetyped()) continue;

			String nodeName = formatEntity(node);
			sb.append("\t\t\tif(" + nodeName + ".ReplacedByNode != null) "
					+ nodeName + " = " + nodeName + ".ReplacedByNode;\n");
		}
	}

	private void genDelNodes(StringBuffer sb, ModifyGenerationStateConst state,
			HashSet<Node> nodesNeededAsElements, PatternGraph right)
	{
		for(Node node : state.delNodes()) {
			nodesNeededAsElements.add(node);
			sb.append("\t\t\tgraph.RemoveEdges(" + formatEntity(node) + ");\n");
			sb.append("\t\t\tgraph.Remove(" + formatEntity(node) + ");\n");
		}
		for(Node node : state.yieldedNodes()) {
			if(node.patternGraphDefYieldedIsToBeDeleted()==right) {
				nodesNeededAsElements.add(node);
				sb.append("\t\t\tgraph.RemoveEdges(" + formatEntity(node) + ");\n");
				sb.append("\t\t\tgraph.Remove(" + formatEntity(node) + ");\n");
			}
		}
	}

	private void genDelEdges(StringBuffer sb, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, PatternGraph right)
	{
		for(Edge edge : state.delEdges()) {
			edgesNeededAsElements.add(edge);
			sb.append("\t\t\tgraph.Remove(" + formatEntity(edge) + ");\n");
		}
		for(Edge edge : state.yieldedEdges()) {
			if(edge.patternGraphDefYieldedIsToBeDeleted()==right) {
				edgesNeededAsElements.add(edge);
				sb.append("\t\t\tgraph.Remove(" + formatEntity(edge) + ");\n");
			}
		}
	}

	private void genRedirectEdges(StringBuffer sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Node> nodesNeededAsElements)
	{
		for(Edge edge : task.right.getEdges()) {
			if(edge.getRedirectedSource(task.right)!=null && edge.getRedirectedTarget(task.right)!=null) {
				Node redirectedSource = edge.getRedirectedSource(task.right);
				Node redirectedTarget = edge.getRedirectedTarget(task.right);
				Node oldSource = task.left.getSource(edge);
				Node oldTarget = task.left.getTarget(edge);
				sb.append("\t\t\tgraph.RedirectSourceAndTarget("
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
			else if(edge.getRedirectedSource(task.right)!=null) {
				Node redirectedSource = edge.getRedirectedSource(task.right);
				Node oldSource = task.left.getSource(edge);
				sb.append("\t\t\tgraph.RedirectSource("
						+ formatEntity(edge) + ", "
						+ formatEntity(redirectedSource) + ", "
						+ "\"" + (oldSource!=null ? formatIdentifiable(oldSource) : "<unknown>") + "\");\n");
				edgesNeededAsElements.add(edge);
				if(!state.newNodes().contains(redirectedSource))
					nodesNeededAsElements.add(redirectedSource);
			}
			else if(edge.getRedirectedTarget(task.right)!=null) {
				Node redirectedTarget = edge.getRedirectedTarget(task.right);
				Node oldTarget = task.left.getTarget(edge);
				sb.append("\t\t\tgraph.RedirectTarget("
						+ formatEntity(edge) + ", "
						+ formatEntity(edge.getRedirectedTarget(task.right)) + ", "
						+ "\"" + (oldTarget!=null ? formatIdentifiable(oldTarget) : "<unknown>") + "\");\n");
				edgesNeededAsElements.add(edge);
				if(!state.newNodes().contains(redirectedTarget))
					nodesNeededAsElements.add(redirectedTarget);
			}
		}
	}
	
	private void genTypeChangesEdges(StringBuffer sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes)
	{
		for(Edge edge : task.right.getEdges()) {
			if(!edge.changesType(task.right)) continue;
			
			String new_type;
			RetypedEdge redge = edge.getRetypedEdge(task.right);

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
			sb.append("\t\t\tGRGEN_LGSP.LGSPEdge " + formatEntity(redge) + " = graph.Retype("
					+ formatEntity(edge) + ", " + new_type + ");\n");
			if(state.edgesNeededAsAttributes().contains(redge) && state.accessViaInterface().contains(redge)) {
				sb.append("\t\t\t"
						+ formatVarDeclWithCast(formatElementInterfaceRef(redge.getType()), "i" + formatEntity(redge))
						+ formatEntity(redge) + ";\n");
			}
		}
	}

	private void genTypeChangesNodesAndMerges(StringBuffer sb, ModifyGenerationStateConst state, ModifyGenerationTask task,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes)
	{
		for(Node node : task.right.getNodes()) {
			if(!node.changesType(task.right)) continue;
			
			String new_type;
			RetypedNode rnode = node.getRetypedNode(task.right);

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
			sb.append("\t\t\tGRGEN_LGSP.LGSPNode " + formatEntity(rnode) + " = graph.Retype("
					+ formatEntity(node) + ", " + new_type + ");\n");
			for(Node mergee : rnode.getMergees()) {
				nodesNeededAsElements.add(mergee);
				sb.append("\t\t\tgraph.Merge("
						+ formatEntity(rnode) + ", " + formatEntity(mergee) + ", \"" + formatIdentifiable(mergee) + "\");\n");
			}
			if(state.nodesNeededAsAttributes().contains(rnode) && state.accessViaInterface().contains(rnode)) {
				sb.append("\t\t\t"
						+ formatVarDeclWithCast(formatElementInterfaceRef(rnode.getType()), "i" + formatEntity(rnode))
						+ formatEntity(rnode) + ";\n");
			}
		}
	}

	private void genAddedGraphElementsArray(StringBuffer sb, ModifyGenerationStateConst state, String pathPrefix, int typeOfTask) {
		if(typeOfTask==TYPE_OF_TASK_MODIFY || typeOfTask==TYPE_OF_TASK_CREATION) {
			genAddedGraphElementsArray(sb, pathPrefix, true, state.newNodes());
			genAddedGraphElementsArray(sb, pathPrefix, false, state.newEdges());
		}
	}

	private void genAlternativeModificationCalls(StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		if(task.right==task.left) { // test needs top-level-modify due to interface, but not more
			return;
		}

		if(task.typeOfTask==TYPE_OF_TASK_MODIFY) {
			// generate calls to the modifications of the alternatives (nested alternatives are handled in their enclosing alternative)
			Collection<Alternative> alts = task.left.getAlts();
			for(Alternative alt : alts) {
				if(alt.wasReplacementAlreadyCalled)
					continue;
				genAlternativeModificationCall(alt, sb, task, pathPrefix);
			}
		}
		else if(task.typeOfTask==TYPE_OF_TASK_DELETION) {
			// generate calls to the deletion of the alternatives (nested alternatives are handled in their enclosing alternative)
			Collection<Alternative> alts = task.left.getAlts();
			for(Alternative alt : alts) {
				String altName = alt.getNameOfGraph();
				sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+altName+"_" +
						"Delete" + "(actionEnv, alternative_"+altName+");\n");
			}
		}
	}

	private void genAlternativeModificationCall(Alternative alt, StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		String altName = alt.getNameOfGraph();
		sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+altName+"_" +
				"Modify(actionEnv, alternative_" + altName);
		List<Entity> replParameters = new LinkedList<Entity>();
		getUnionOfReplaceParametersOfAlternativeCases(alt, replParameters);
		for(Entity entity : replParameters) {
			sb.append(", ");
			if(entity.isDefToBeYieldedTo()) sb.append("ref ");
			sb.append(formatEntity(entity));
		}
		sb.append(");\n");
	}

	private void genIteratedModificationCalls(StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		if(task.right==task.left) { // test needs top-level-modify due to interface, but not more
			return;
		}

		if(task.typeOfTask==TYPE_OF_TASK_MODIFY) {
			// generate calls to the modifications of the iterateds (nested iterateds are handled in their enclosing iterated)
			Collection<Rule> iters = task.left.getIters();
			for(Rule iter : iters) {
				if(iter.wasReplacementAlreadyCalled)
					continue;
				genIteratedModificationCall(iter, sb, task, pathPrefix);
			}
		}
		else if(task.typeOfTask==TYPE_OF_TASK_DELETION) {
			// generate calls to the deletion of the iterateds (nested iterateds are handled in their enclosing iterated)
			Collection<Rule> iters = task.left.getIters();
			for(Rule iter : iters) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+iterName+"_" +
						"Delete" + "(actionEnv, iterated_"+iterName+");\n");
			}
		}
	}

	private void genIteratedModificationCall(Rule iter, StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		String iterName = iter.getLeft().getNameOfGraph();
		sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+iterName+"_" +
				"Modify(actionEnv, iterated_" + iterName);
		List<Entity> replParameters = iter.getRight().getReplParameters();
		for(Entity entity : replParameters) {
			sb.append(", ");
			if(entity.isDefToBeYieldedTo()) sb.append("ref ");
			sb.append(formatEntity(entity));
		}
		sb.append(");\n");
	}

	private void genSubpatternModificationCalls(StringBuffer sb, ModifyGenerationTask task, String pathPrefix,
			ModifyGenerationStateConst state, HashSet<Node> nodesNeededAsElements, HashSet<Variable> neededVariables,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes) {
		if(task.right==task.left) { // test needs top-level-modify due to interface, but not more
			return;
		}

		boolean emitHereNeeded = false;
		for(OrderedReplacements orderedReps : task.right.getOrderedReplacements()) {
			for(OrderedReplacement orderedRep : orderedReps.orderedReplacements) {
				if(orderedRep instanceof Emit) { // emithere
					emitHereNeeded = true;
					break;
				}
			}
		}
		
		if(emitHereNeeded || task.mightThereBeDeferredExecs) {
			sb.append("\t\t\tGRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;\n");
		}

		if(task.mightThereBeDeferredExecs) {
			sb.append("\t\t\tprocEnv.sequencesManager.EnterRuleModifyAddingDeferredSequences();\n");
		}

		// generate calls to the dependent modifications of the subpatterns
		for(OrderedReplacements orderedReps : task.right.getOrderedReplacements()) {
			sb.append("\t\t\t{ // " + orderedReps.getName() + "\n");
			for(OrderedReplacement orderedRep : orderedReps.orderedReplacements) {
				if(orderedRep instanceof SubpatternDependentReplacement) {
					SubpatternDependentReplacement subRep = (SubpatternDependentReplacement)orderedRep;
					Rule subRule = subRep.getSubpatternUsage().getSubpatternAction();
					String subName = formatIdentifiable(subRep);
					sb.append("\t\t\tPattern_" + formatIdentifiable(subRule)
							+ ".Instance." + formatIdentifiable(subRule) +
							"_Modify(actionEnv, subpattern_" + subName);
					NeededEntities needs = new NeededEntities(true, true, true, false, true, true, false);
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
				} else if(orderedRep instanceof Emit) { // emithere
					Emit emit = (Emit)orderedRep;
					for(Expression arg : emit.getArguments()) {
						sb.append("\t\t\tprocEnv.EmitWriter.Write(");
						genExpression(sb, arg, state);
						sb.append(");\n");
					}
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
					genEvalStmt(sb, state, evalStmt);
				}
			}
			sb.append("\t\t\t}\n");
		}
	}

	private void genYieldedElementsInterfaceAccess(StringBuffer sb, ModifyGenerationStateConst state, String pathPrefix) {
		for(Node node : state.yieldedNodes()) {
			sb.append("\t\t\t"
					+ formatVarDeclWithCast(formatElementInterfaceRef(node.getType()), "i" + formatEntity(node))
					+ formatEntity(node) + ";\n");
		}
		for(Edge edge : state.yieldedEdges()) {
			sb.append("\t\t\t"
					+ formatVarDeclWithCast(formatElementInterfaceRef(edge.getType()), "i" + formatEntity(edge))
					+ formatEntity(edge) + ";\n");
		}
	}
	
	private void genAddedGraphElementsArray(StringBuffer sb, String pathPrefix, boolean isNode, Collection<? extends GraphEntity> set) {
		String NodesOrEdges = isNode?"Node":"Edge";
		sb.append("\t\tprivate static string[] " + pathPrefix + "added" + NodesOrEdges + "Names = new string[] ");
		genSet(sb, set, "\"", "\"", true);
		sb.append(";\n");
	}

	private void emitReturnStatement(StringBuffer sb, ModifyGenerationStateConst state, List<Expression> returns) {
		for(int i = 0; i < returns.size(); i++)
		{
			sb.append("\t\t\toutput_" + i + " = ");
			Expression expr = returns.get(i);
			if(expr instanceof GraphEntityExpression)
				sb.append("(" + formatElementInterfaceRef(expr.getType()) + ")(");
			else
				sb.append("(" + formatAttributeType(expr.getType()) + ") (");
			genExpression(sb, expr, state);
			sb.append(");\n");
		}
		sb.append("\t\t\treturn;\n");
	}

	private void genExtractElementsFromMatch(StringBuffer sb, ModifyGenerationTask task,
			ModifyGenerationStateConst state, String pathPrefix, String patternName) {
		for(Node node : state.nodesNeededAsElements()) {
			if(node.isRetyped() && node.isRHSEntity()) continue;
			if(state.yieldedNodes().contains(node)) continue;
			sb.append("\t\t\tGRGEN_LGSP.LGSPNode " + formatEntity(node)
					+ " = curMatch." + formatEntity(node, "_") + ";\n");
		}
		for(Node node : state.nodesNeededAsAttributes()) {
			if(node.isRetyped() && node.isRHSEntity()) continue;
			if(state.yieldedNodes().contains(node)) continue;
			if(task.replParameters.contains(node)) {
				sb.append("\t\t\t" + formatElementInterfaceRef(node.getType()) + " i" + formatEntity(node)
						+ " = (" + formatElementInterfaceRef(node.getType()) + ")" + formatEntity(node) + ";\n");
				continue; // replacement parameters are handed in as parameters
			}
			sb.append("\t\t\t" + formatElementInterfaceRef(node.getType()) + " i" + formatEntity(node)
					+ " = curMatch." + formatEntity(node) + ";\n");
		}
		for(Edge edge : state.edgesNeededAsElements()) {
			if(edge.isRetyped() && edge.isRHSEntity()) continue;
			if(state.yieldedEdges().contains(edge)) continue;
			sb.append("\t\t\tGRGEN_LGSP.LGSPEdge " + formatEntity(edge)
					+ " = curMatch." + formatEntity(edge, "_") + ";\n");
		}
		for(Edge edge : state.edgesNeededAsAttributes()) {
			if(edge.isRetyped() && edge.isRHSEntity()) continue;
			if(state.yieldedEdges().contains(edge)) continue;
			if(task.replParameters.contains(edge)) {
				sb.append("\t\t\t" + formatElementInterfaceRef(edge.getType()) + " i" + formatEntity(edge)
						+ " = (" + formatElementInterfaceRef(edge.getType()) + ")" + formatEntity(edge) + ";\n");
				continue; // replacement parameters are handed in as parameters
			}
			sb.append("\t\t\t" + formatElementInterfaceRef(edge.getType()) + " i" + formatEntity(edge)
					+ " = curMatch." + formatEntity(edge) + ";\n");
		}
	}

	private void genExtractVariablesFromMatch(StringBuffer sb, ModifyGenerationTask task,
			ModifyGenerationStateConst state, String pathPrefix, String patternName) {
		for(Variable var : state.neededVariables()) {
			if(task.replParameters.contains(var)) continue; // skip replacement parameters, they are handed in as parameters
			if(state.yieldedVariables().contains(var)) continue;
			String type = formatAttributeType(var);
			sb.append("\t\t\t" + type + " " + formatEntity(var)
					+ " = curMatch."+formatEntity(var, "_")+";\n");
		}
	}

	private void genExtractSubmatchesFromMatch(StringBuffer sb, String pathPrefix, PatternGraph pattern) {
		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			String subName = formatIdentifiable(sub);
			sb.append("\t\t\t"+matchType(sub.getSubpatternAction().getPattern(), true, "")+" subpattern_" + subName
					+ " = curMatch.@_" + formatIdentifiable(sub) + ";\n");
		}
		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			String iterType = "GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefix+pattern.getNameOfGraph()+"_"+iterName
			  + ", IMatch_"+pathPrefix+pattern.getNameOfGraph()+"_"+iterName+">";
			sb.append("\t\t\t"+iterType+" iterated_" + iterName
					+ " = curMatch._"+iterName+";\n");
		}
		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			String altType = "IMatch_"+pathPrefix+pattern.getNameOfGraph()+"_"+altName;
			sb.append("\t\t\t"+altType+" alternative_" + altName
					+ " = curMatch._"+altName+";\n");
		}
	}

	////////////////////////////
	// New element generation //
	////////////////////////////

	private void genNewNodes(StringBuffer sb2, ModifyGenerationStateConst state,
			boolean useAddedElementNames, String pathPrefix,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes) {
		// call nodes added delegate
		if(useAddedElementNames) sb2.append("\t\t\tgraph.SettingAddedNodeNames( " + pathPrefix + "addedNodeNames );\n");

		LinkedList<Node> tmpNewNodes = new LinkedList<Node>(state.newNodes());

		for(Node node : tmpNewNodes) {
			if(node.inheritsType()) { // typeof or copy
				Node typeofElem = (Node) getConcreteTypeofElem(node);
				nodesNeededAsElements.add(typeofElem);
				
				if(node.isCopy()) { // node:copy<typeofElem>
					sb2.append("\t\t\tGRGEN_LGSP.LGSPNode " + formatEntity(node)
							+ " = (GRGEN_LGSP.LGSPNode) "
							+ formatEntity(typeofElem) + ".Clone();\n"
						+ "\t\t\tgraph.AddNode(" + formatEntity(node) + ");\n");					
				} else { // node:typeof(typeofElem)
					nodesNeededAsTypes.add(typeofElem);
					sb2.append("\t\t\tGRGEN_LGSP.LGSPNode " + formatEntity(node)
								+ " = (GRGEN_LGSP.LGSPNode) "
								+ formatEntity(typeofElem) + "_type.CreateNode();\n"
							+ "\t\t\tgraph.AddNode(" + formatEntity(node) + ");\n");
				}
				
				if(state.nodesNeededAsAttributes().contains(node) && state.accessViaInterface().contains(node)) {
					sb2.append("\t\t\t"
							+ formatVarDeclWithCast(formatElementInterfaceRef(node.getType()), "i" + formatEntity(node))
							+ formatEntity(node) + ";\n");
				}
			} else { // node:type
				String elemref = formatElementClassRef(node.getType());
				sb2.append("\t\t\t" + elemref + " " + formatEntity(node) + " = " + elemref + ".CreateNode(graph);\n");
			}
		}
	}

	/**
	 * Returns the iterated inherited type element for a given element
	 * or null, if the given element does not inherit its type from another element.
	 */
	private GraphEntity getConcreteTypeofElem(GraphEntity elem) {
		GraphEntity typeofElem = elem;
		while(typeofElem.inheritsType())
			typeofElem = typeofElem.getTypeof();
		return typeofElem == elem ? null : typeofElem;
	}

	private void genNewEdges(StringBuffer sb2, ModifyGenerationStateConst state, ModifyGenerationTask task,
			boolean useAddedElementNames, String pathPrefix,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Edge> edgesNeededAsTypes)
	{
		// call edges added delegate
		if(useAddedElementNames) sb2.append("\t\t\tgraph.SettingAddedEdgeNames( " + pathPrefix + "addedEdgeNames );\n");

		for(Edge edge : state.newEdges()) {
			String elemref = formatElementClassRef(edge.getType());

			Node src_node = task.right.getSource(edge);
			Node tgt_node = task.right.getTarget(edge);
			if(src_node==null || tgt_node==null) {
				return; // don't create dangling edges    - todo: what's the correct way to handle them?
			}

			if(src_node.changesType(task.right)) src_node = src_node.getRetypedNode(task.right);
			if(tgt_node.changesType(task.right)) tgt_node = tgt_node.getRetypedNode(task.right);

			if(state.commonNodes().contains(src_node))
				nodesNeededAsElements.add(src_node);

			if(state.commonNodes().contains(tgt_node))
				nodesNeededAsElements.add(tgt_node);

			if(edge.inheritsType()) { // typeof or copy
				Edge typeofElem = (Edge) getConcreteTypeofElem(edge);
				edgesNeededAsElements.add(typeofElem);

				if(edge.isCopy()) { // -edge:copy<typeofElem>->
					sb2.append("\t\t\tGRGEN_LGSP.LGSPEdge " + formatEntity(edge)
							+ " = (GRGEN_LGSP.LGSPEdge) "
							+ formatEntity(typeofElem) + ".Clone("
							+ formatEntity(src_node) + ", " + formatEntity(tgt_node) + ");\n"
						+ "\t\t\tgraph.AddEdge(" + formatEntity(edge) + ");\n");
				} else { // -edge:typeof(typeofElem)->
					edgesNeededAsTypes.add(typeofElem);
					sb2.append("\t\t\tGRGEN_LGSP.LGSPEdge " + formatEntity(edge)
								+ " = (GRGEN_LGSP.LGSPEdge) "
								+ formatEntity(typeofElem) + "_type.CreateEdge("
								+ formatEntity(src_node) + ", " + formatEntity(tgt_node) + ");\n"
							+ "\t\t\tgraph.AddEdge(" + formatEntity(edge) + ");\n");
				}

				if(state.edgesNeededAsAttributes().contains(edge) && state.accessViaInterface().contains(edge)) {
					sb2.append("\t\t\t"
							+ formatVarDeclWithCast(formatElementInterfaceRef(edge.getType()), "i" + formatEntity(edge))
							+ formatEntity(edge) + ";\n");
				}
			} else { // -edge:type->
				sb2.append("\t\t\t" + elemref + " " + formatEntity(edge) + " = " + elemref
					   + ".CreateEdge(graph, " + formatEntity(src_node)
					   + ", " + formatEntity(tgt_node) + ");\n");
			}
		}
	}

	private void genNewSubpatternCalls(StringBuffer sb, ModifyGenerationStateConst state)
	{
		for(SubpatternUsage subUsage : state.newSubpatternUsages()) {
			if(hasAbstractElements(subUsage.getSubpatternAction().getPattern()))
				continue;

			sb.append("\t\t\tPattern_" + formatIdentifiable(subUsage.getSubpatternAction())
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

	private void genDelSubpatternCalls(StringBuffer sb, ModifyGenerationStateConst state)
	{
		for(SubpatternUsage subUsage : state.delSubpatternUsages()) {
			String subName = formatIdentifiable(subUsage);
			sb.append("\t\t\tPattern_" + formatIdentifiable(subUsage.getSubpatternAction())
					+ ".Instance." + formatIdentifiable(subUsage.getSubpatternAction()) +
					"_Delete(actionEnv, subpattern_" + subName + ");\n");
		}
	}

	//////////////////////////
	// Eval part generation //
	//////////////////////////

	private void initEvalGen() {
		// init eval statement generation state
		tmpVarID = 0;
		embeddedComputationXgrsID = 0;
	}

	private void genAllEvals(StringBuffer sb, ModifyGenerationStateConst state, Collection<EvalStatements> evalStatements) {
		for(EvalStatements evalStmts : evalStatements) {
			sb.append("\t\t\t{ // " + evalStmts.getName() + "\n");
			genEvals(sb, state, evalStmts.evalStatements);
			sb.append("\t\t\t}\n");
		}
	}

	private void genEvals(StringBuffer sb, ModifyGenerationStateConst state, Collection<EvalStatement> evalStatements) {
		for(EvalStatement evalStmt : evalStatements) {
			genEvalStmt(sb, state, evalStmt);
		}
	}

	public void genEvalStmt(StringBuffer sb, ModifyGenerationStateConst state, EvalStatement evalStmt) {
		if(evalStmt instanceof Assignment) { // includes evalStmt instanceof AssignmentIndexed
			genAssignment(sb, state, (Assignment) evalStmt);
		}
		else if(evalStmt instanceof AssignmentVar) { // includes evalStmt instanceof AssignmentVarIndexed
			genAssignmentVar(sb, state, (AssignmentVar) evalStmt);
		}
		else if(evalStmt instanceof AssignmentGraphEntity) {
			genAssignmentGraphEntity(sb, state, (AssignmentGraphEntity) evalStmt);
		}
		else if(evalStmt instanceof AssignmentVisited) {
			genAssignmentVisited(sb, state, (AssignmentVisited) evalStmt);
		}
		else if(evalStmt instanceof AssignmentIdentical) {
			//nothing to generate, was assignment . = . optimized away;
		}
		else if(evalStmt instanceof CompoundAssignmentChanged) {
			genCompoundAssignmentChanged(sb, state, (CompoundAssignmentChanged) evalStmt);
		}
		else if(evalStmt instanceof CompoundAssignmentChangedVar) {
			genCompoundAssignmentChangedVar(sb, state, (CompoundAssignmentChangedVar) evalStmt);
		}
		else if(evalStmt instanceof CompoundAssignmentChangedVisited) {
			genCompoundAssignmentChangedVisited(sb, state, (CompoundAssignmentChangedVisited) evalStmt);
		}
		else if(evalStmt instanceof CompoundAssignment) { // must come after the changed versions
			genCompoundAssignment(sb, state, (CompoundAssignment) evalStmt, "\t\t\t", ";\n");
		}
		else if(evalStmt instanceof CompoundAssignmentVarChanged) {
			genCompoundAssignmentVarChanged(sb, state, (CompoundAssignmentVarChanged) evalStmt);
		}
		else if(evalStmt instanceof CompoundAssignmentVarChangedVar) {
			genCompoundAssignmentVarChangedVar(sb, state, (CompoundAssignmentVarChangedVar) evalStmt);
		}
		else if(evalStmt instanceof CompoundAssignmentVarChangedVisited) {
			genCompoundAssignmentVarChangedVisited(sb, state, (CompoundAssignmentVarChangedVisited) evalStmt);
		}
		else if(evalStmt instanceof CompoundAssignmentVar) { // must come after the changed versions
			genCompoundAssignmentVar(sb, state, (CompoundAssignmentVar) evalStmt, "\t\t\t", ";\n");
		}
		else if(evalStmt instanceof MapRemoveItem) {
			genMapRemoveItem(sb, state, (MapRemoveItem) evalStmt);
		} 
		else if(evalStmt instanceof MapClear) {
			genMapClear(sb, state, (MapClear) evalStmt);
		} 
		else if(evalStmt instanceof MapAddItem) {
			genMapAddItem(sb, state, (MapAddItem) evalStmt);
		} 
		else if(evalStmt instanceof SetRemoveItem) {
			genSetRemoveItem(sb, state, (SetRemoveItem) evalStmt);
		} 
		else if(evalStmt instanceof SetClear) {
			genSetClear(sb, state, (SetClear) evalStmt);
		} 
		else if(evalStmt instanceof SetAddItem) {
			genSetAddItem(sb, state, (SetAddItem) evalStmt);
		}
		else if(evalStmt instanceof ArrayRemoveItem) {
			genArrayRemoveItem(sb, state, (ArrayRemoveItem) evalStmt);
		} 
		else if(evalStmt instanceof ArrayClear) {
			genArrayClear(sb, state, (ArrayClear) evalStmt);
		} 
		else if(evalStmt instanceof ArrayAddItem) {
			genArrayAddItem(sb, state, (ArrayAddItem) evalStmt);
		}
		else if(evalStmt instanceof DequeRemoveItem) {
			genDequeRemoveItem(sb, state, (DequeRemoveItem) evalStmt);
		} 
		else if(evalStmt instanceof DequeClear) {
			genDequeClear(sb, state, (DequeClear) evalStmt);
		} 
		else if(evalStmt instanceof DequeAddItem) {
			genDequeAddItem(sb, state, (DequeAddItem) evalStmt);
		}
		else if(evalStmt instanceof MapVarRemoveItem) {
			genMapVarRemoveItem(sb, state, (MapVarRemoveItem) evalStmt);
		} 
		else if(evalStmt instanceof MapVarClear) {
			genMapVarClear(sb, state, (MapVarClear) evalStmt);
		} 
		else if(evalStmt instanceof MapVarAddItem) {
			genMapVarAddItem(sb, state, (MapVarAddItem) evalStmt);
		}
		else if(evalStmt instanceof SetVarRemoveItem) {
			genSetVarRemoveItem(sb, state, (SetVarRemoveItem) evalStmt);
		}
		else if(evalStmt instanceof SetVarClear) {
			genSetVarClear(sb, state, (SetVarClear) evalStmt);
		}
		else if(evalStmt instanceof SetVarAddItem) {
			genSetVarAddItem(sb, state, (SetVarAddItem) evalStmt);
		}
		else if(evalStmt instanceof ArrayVarRemoveItem) {
			genArrayVarRemoveItem(sb, state, (ArrayVarRemoveItem) evalStmt);
		}
		else if(evalStmt instanceof ArrayVarClear) {
			genArrayVarClear(sb, state, (ArrayVarClear) evalStmt);
		}
		else if(evalStmt instanceof ArrayVarAddItem) {
			genArrayVarAddItem(sb, state, (ArrayVarAddItem) evalStmt);
		}
		else if(evalStmt instanceof DequeVarRemoveItem) {
			genDequeVarRemoveItem(sb, state, (DequeVarRemoveItem) evalStmt);
		}
		else if(evalStmt instanceof DequeVarClear) {
			genDequeVarClear(sb, state, (DequeVarClear) evalStmt);
		}
		else if(evalStmt instanceof DequeVarAddItem) {
			genDequeVarAddItem(sb, state, (DequeVarAddItem) evalStmt);
		}
		else if(evalStmt instanceof ReturnStatement) {
			genReturnStatement(sb, state, (ReturnStatement) evalStmt);
		}
		else if(evalStmt instanceof ConditionStatement) {
			genConditionStatement(sb, state, (ConditionStatement) evalStmt);
		}
		else if(evalStmt instanceof WhileStatement) {
			genWhileStatement(sb, state, (WhileStatement) evalStmt);
		}
		else if(evalStmt instanceof DoWhileStatement) {
			genDoWhileStatement(sb, state, (DoWhileStatement) evalStmt);
		}
		else if(evalStmt instanceof DefDeclVarStatement) {
			genDefDeclVarStatement(sb, state, (DefDeclVarStatement) evalStmt);
		}
		else if(evalStmt instanceof DefDeclGraphEntityStatement) {
			genDefDeclGraphEntityStatement(sb, state, (DefDeclGraphEntityStatement) evalStmt);
		}
		else if(evalStmt instanceof ContainerAccumulationYield) {
			genContainerAccumulationYield(sb, state, (ContainerAccumulationYield) evalStmt);
		}
		else if(evalStmt instanceof ForLookup) {
			genForLookup(sb, state, (ForLookup) evalStmt);
		}
		else if(evalStmt instanceof ForFunction) {
			genForFunction(sb, state, (ForFunction) evalStmt);
		}
		else if(evalStmt instanceof BreakStatement) {
			genBreakStatement(sb, state, (BreakStatement) evalStmt);
		}
		else if(evalStmt instanceof ContinueStatement) {
			genContinueStatement(sb, state, (ContinueStatement) evalStmt);
		}
		else if(evalStmt instanceof EmitStatement) {
			genEmitStatement(sb, state, (EmitStatement) evalStmt);
		}
		else if(evalStmt instanceof RecordStatement) {
			genRecordStatement(sb, state, (RecordStatement) evalStmt);
		}
		else if(evalStmt instanceof GraphClear) {
			genGraphClearStatement(sb, state, (GraphClear) evalStmt);
		}
		else if(evalStmt instanceof GraphRemove) {
			genGraphRemoveStatement(sb, state, (GraphRemove) evalStmt);
		}
		else if(evalStmt instanceof GraphMerge) {
			genGraphMergeStatement(sb, state, (GraphMerge) evalStmt);
		}
		else if(evalStmt instanceof GraphRedirectSource) {
			genGraphRedirectSourceStatement(sb, state, (GraphRedirectSource) evalStmt);
		}
		else if(evalStmt instanceof GraphRedirectTarget) {
			genGraphRedirectTargetStatement(sb, state, (GraphRedirectTarget) evalStmt);
		}
		else if(evalStmt instanceof GraphRedirectSourceAndTarget) {
			genGraphRedirectSourceAndTargetStatement(sb, state, (GraphRedirectSourceAndTarget) evalStmt);
		}
		else if(evalStmt instanceof VFreeStatement) {
			genVFreeStatement(sb, state, (VFreeStatement) evalStmt);
		}
		else if(evalStmt instanceof VFreeNonResetStatement) {
			genVFreeNonResetStatement(sb, state, (VFreeNonResetStatement) evalStmt);
		}
		else if(evalStmt instanceof VResetStatement) {
			genVResetStatement(sb, state, (VResetStatement) evalStmt);
		}
		else if(evalStmt instanceof PauseTransaction) {
			genPauseTransactionStatement(sb, state, (PauseTransaction) evalStmt);
		}
		else if(evalStmt instanceof ResumeTransaction) {
			genResumeTransactionStatement(sb, state, (ResumeTransaction) evalStmt);
		}
		else if(evalStmt instanceof CommitTransaction) {
			genCommitTransactionStatement(sb, state, (CommitTransaction) evalStmt);
		}
		else if(evalStmt instanceof RollbackTransaction) {
			genRollbackTransactionStatement(sb, state, (RollbackTransaction) evalStmt);
		}
		else if(evalStmt instanceof CallStatement) {
			genCallStatement(sb, state, (CallStatement) evalStmt);
		}
		else if(evalStmt instanceof ExecStatement) {
			genExecStatement(sb, state, (ExecStatement) evalStmt);
		}
		else {
			throw new UnsupportedOperationException("Unexpected eval statement \"" + evalStmt + "\"");
		}
	}

	private void genAssignment(StringBuffer sb, ModifyGenerationStateConst state, Assignment ass) {
		Qualification target = ass.getTarget();
		Expression expr = ass.getExpression();
		Type targetType = target.getType();

		if((targetType instanceof MapType 
			|| targetType instanceof SetType 
			|| targetType instanceof ArrayType
			|| targetType instanceof DequeType)
				&& !(ass instanceof AssignmentIndexed)) {
			// Check whether we have to make a copy of the right hand side of the assignment
			boolean mustCopy = true;
			if(expr instanceof Operator) {
				Operator op = (Operator) expr;

				// For unions and intersections new maps/sets are already created,
				// so we don't have to copy them again
				if(op.getOpCode() == Operator.BIT_OR || op.getOpCode() == Operator.BIT_AND)
					mustCopy = false;
			}

			String typeName = formatAttributeType(targetType);
			String varName = "tempvar_" + tmpVarID++;
			sb.append("\t\t\t" + typeName + " " + varName + " = ");
			if(mustCopy)
				sb.append("new " + typeName + "(");
			genExpression(sb, expr, state);
			if(mustCopy)
				sb.append(')');
			sb.append(";\n");

			genChangingAttribute(sb, state, target, "Assign", varName , "null");

			sb.append("\t\t\t");
			genExpression(sb, target, state); // global var case handled by genQualAccess
			sb.append(" = " + varName + ";\n");

			return;
		}

		// indexed assignment to array/deque/map, the target type is the array/deque/map value type
		if(ass instanceof AssignmentIndexed && targetType instanceof ArrayType) {
			targetType = ((ArrayType)targetType).getValueType();
		}
		if(ass instanceof AssignmentIndexed && targetType instanceof DequeType) {
			targetType = ((DequeType)targetType).getValueType();
		}
		if(ass instanceof AssignmentIndexed && targetType instanceof MapType) {
			targetType = ((MapType)targetType).getValueType();
		}

		String varName = "tempvar_" + tmpVarID++;
		String varType = getTypeNameForTempVarDecl(targetType) + " ";

		sb.append("\t\t\t" + varType + varName + " = ");
		if(targetType instanceof EnumType)
			sb.append("(int) ");
		genExpression(sb, expr, state);
		sb.append(";\n");

		if(ass instanceof AssignmentIndexed)
		{
			AssignmentIndexed assIdx = (AssignmentIndexed)ass; 

			if(target.getType() instanceof ArrayType
					|| target.getType() instanceof DequeType) {
				String indexType = "int ";
				String indexName = "tempvar_index" + tmpVarID++;
				sb.append("\t\t\t" + indexType + indexName + " = (int)");
				genExpression(sb, assIdx.getIndex(), state);
				sb.append(";\n");

				sb.append("\t\t\tif(" + indexName + " < ");
				genExpression(sb, target, state);
				sb.append(".Count) {\n");
				
				sb.append("\t");
				genChangingAttribute(sb, state, target, "AssignElement", varName, indexName);

				sb.append("\t\t\t\t");
				genExpression(sb, target, state); // global var case handled by genQualAccess
				sb.append("[");
				sb.append(indexName);
				sb.append("]");
			} else { //if(target.getType() instanceof MapType)
				String indexType = formatType(((MapType)target.getType()).getKeyType()) + " ";
				String indexName = "tempvar_index_" + tmpVarID++;
				sb.append("\t\t\t" + indexType + indexName + " = ");
				genExpression(sb, assIdx.getIndex(), state);
				sb.append(";\n");

				sb.append("\t\t\tif(");
				genExpression(sb, target, state);
				sb.append(".ContainsKey(");
				sb.append(indexName);
				sb.append(")) {\n");

				sb.append("\t");
				genChangingAttribute(sb, state, target, "AssignElement", varName, indexName);
				
				sb.append("\t\t\t\t");
				genExpression(sb, target, state); // global var case handled by genQualAccess
				sb.append("[");
				sb.append(indexName);
				sb.append("]");
			}
			
			sb.append(" = ");
			if(targetType instanceof EnumType)
				sb.append("(GRGEN_MODEL.ENUM_" + formatIdentifiable(targetType) + ") ");
			sb.append(varName + ";\n");
			
			sb.append("\t\t\t}\n");
		}
		else
		{
			genChangingAttribute(sb, state, target, "Assign", varName, "null");
	
			sb.append("\t\t\t");
			genExpression(sb, target, state); // global var case handled by genQualAccess
			
			sb.append(" = ");
			if(targetType instanceof EnumType)
				sb.append("(GRGEN_MODEL.ENUM_" + formatIdentifiable(targetType) + ") ");
			sb.append(varName + ";\n");
		}
	}

	private void genAssignmentVar(StringBuffer sb, ModifyGenerationStateConst state, AssignmentVar ass) {
		Variable target = ass.getTarget();
		Expression expr = ass.getExpression();
				
		Type targetType = target.getType();
		if(ass instanceof AssignmentVarIndexed) {
			if(targetType instanceof ArrayType)
				targetType = ((ArrayType)target.getType()).getValueType();
			else if(targetType instanceof DequeType)
				targetType = ((DequeType)target.getType()).getValueType();
			else // targetType instanceof MapType
				targetType = ((MapType)target.getType()).getValueType();
		}

		Type indexType = IntType.getType();
		if(target.getType() instanceof MapType)
			indexType = ((MapType)target.getType()).getKeyType();

		sb.append("\t\t\t");
		if(!Expression.isGlobalVariable(target)) {
			sb.append("var_" + target.getIdent());
			if(ass instanceof AssignmentVarIndexed) {
				AssignmentVarIndexed assIdx = (AssignmentVarIndexed)ass;
				Expression index = assIdx.getIndex();
				sb.append("[(" + formatType(indexType) + ") (");
				genExpression(sb, index, state);
				sb.append(")]");
			}
			
			sb.append(" = ");
			sb.append("(" + formatType(targetType) + ") (");
			genExpression(sb, expr, state);
			sb.append(");\n");
		} else {
			if(ass instanceof AssignmentVarIndexed) {
				AssignmentVarIndexed assIdx = (AssignmentVarIndexed)ass;
				sb.append(formatGlobalVariableRead(target));
				Expression index = assIdx.getIndex();
				sb.append("[(" + formatType(indexType) + ") (");
				genExpression(sb, index, state);
				sb.append(")]");
				
				sb.append(" = ");
				sb.append("(" + formatType(targetType) + ") (");
				genExpression(sb, expr, state);
				sb.append(");\n");
			} else {
				StringBuffer tmp = new StringBuffer();
				tmp.append("(" + formatType(targetType) + ") (");
				genExpression(tmp, expr, state);
				tmp.append(")");
				sb.append(formatGlobalVariableWrite(target, tmp.toString()));
				sb.append(";\n");
			}			
		}
	}

	private void genAssignmentGraphEntity(StringBuffer sb, ModifyGenerationStateConst state, AssignmentGraphEntity ass) {
		GraphEntity target = ass.getTarget();
		Expression expr = ass.getExpression();

		sb.append("\t\t\t");
		if(!Expression.isGlobalVariable(target)) {
			sb.append(formatEntity(target));
			sb.append(" = ");
			genExpression(sb, expr, state);
			sb.append(";\n");
		} else {
			StringBuffer tmp = new StringBuffer();
			genExpression(tmp, expr, state);
			sb.append(formatGlobalVariableWrite(target, tmp.toString()));
			sb.append(";\n");
		}
	}

	private void genAssignmentVisited(StringBuffer sb, ModifyGenerationStateConst state, AssignmentVisited ass) {
		sb.append("\t\t\tgraph.SetVisited(");
		genExpression(sb, ass.getTarget().getEntity(), state);
		sb.append(", ");
		genExpression(sb, ass.getTarget().getVisitorID(), state);
		sb.append(", ");
		genExpression(sb, ass.getExpression(), state);
		sb.append(");\n");
	}
	
	private void genCompoundAssignmentChanged(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentChanged cass)
	{
		Qualification changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation()==CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation()==CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";

		Entity owner = cass.getTarget().getOwner();
		boolean isDeletedElem = owner instanceof Node ? state.delNodes().contains(owner) : state.delEdges().contains(owner);
		if(!isDeletedElem && be.system.mayFireEvents()) {
			owner = changedTarget.getOwner();
			isDeletedElem = owner instanceof Node ? state.delNodes().contains(owner) : state.delEdges().contains(owner);
			if(!isDeletedElem && be.system.mayFireEvents()) {
				String varName = "tempvar_" + tmpVarID++;
				String varType = "bool ";

				sb.append("\t\t\t" + varType + varName + " = ");
				genExpression(sb, changedTarget, state);
				sb.append(";\n");

				String prefix = "\t\t\t" + varName + changedOperation;
				
				genCompoundAssignment(sb, state, cass, prefix, ";\n");

				genChangingAttribute(sb, state, changedTarget, "Assign", varName, "null");	

				sb.append("\t\t\t");				
				genExpression(sb, changedTarget, state);
				sb.append(" = " + varName + ";\n");
			} else {
				genCompoundAssignment(sb, state, cass, "\t\t\t", ";\n");
			}
		}
	}
	
	private void genCompoundAssignmentChangedVar(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentChangedVar cass)
	{
		Variable changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation()==CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation()==CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";
		
		String prefix = "\t\t\t" + "var_" + changedTarget.getIdent() + changedOperation;

		genCompoundAssignment(sb, state, cass, prefix, ";\n");
	}
	
	private void genCompoundAssignmentChangedVisited(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentChangedVisited cass)
	{
		Visited changedTarget = cass.getChangedTarget();

		StringBuffer changedTargetBuffer = new StringBuffer();
		genExpression(changedTargetBuffer, changedTarget.getEntity(), state);
		changedTargetBuffer.append(", ");
		genExpression(changedTargetBuffer, changedTarget.getVisitorID(), state);

		String prefix = "\t\t\t" + "graph.SetVisited("
			+ changedTargetBuffer.toString() + ", ";
		if(cass.getChangedOperation()!=CompoundAssignment.ASSIGN) {
			prefix += "graph.IsVisited(" + changedTargetBuffer.toString() + ")"
				+ (cass.getChangedOperation()==CompoundAssignment.UNION ? " | " : " & ");
		}

		genCompoundAssignment(sb, state, cass, prefix, ");\n");
	}
	
	private void genCompoundAssignment(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignment cass,
			String prefix, String postfix)
	{
		Qualification target = cass.getTarget();
		assert(target.getType() instanceof MapType || target.getType() instanceof SetType 
				|| target.getType() instanceof ArrayType || target.getType() instanceof DequeType);
		Expression expr = cass.getExpression();

		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		boolean isDeletedElem = element instanceof Node ? state.delNodes().contains(element) : state.delEdges().contains(element);
		if(!isDeletedElem && be.system.mayFireEvents()) {
			sb.append(prefix);
			if(cass.getOperation()==CompoundAssignment.UNION)
				sb.append("GRGEN_LIBGR.ContainerHelper.UnionChanged(");
			else if(cass.getOperation()==CompoundAssignment.INTERSECTION)
				sb.append("GRGEN_LIBGR.ContainerHelper.IntersectChanged(");
			else if(cass.getOperation()==CompoundAssignment.WITHOUT)
				sb.append("GRGEN_LIBGR.ContainerHelper.ExceptChanged(");
			else //if(cass.getOperation()==CompoundAssignment.CONCATENATE)
				sb.append("GRGEN_LIBGR.ContainerHelper.ConcatenateChanged(");
			genExpression(sb, target, state);
			sb.append(", ");
			genExpression(sb, expr, state);
			sb.append(", ");
			sb.append("graph, "
					+ formatEntity(element) + ", " 
					+ formatTypeClassRef(elementType) + "." + formatAttributeTypeName(attribute));
			sb.append(")");
			sb.append(postfix);
		}
	}

	private void genCompoundAssignmentVarChanged(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentVarChanged cass)
	{
		Qualification changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation()==CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation()==CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";

		Entity owner = changedTarget.getOwner();
		boolean isDeletedElem = owner instanceof Node ? state.delNodes().contains(owner) : state.delEdges().contains(owner);
		if(!isDeletedElem && be.system.mayFireEvents()) {
			String varName = "tempvar_" + tmpVarID++;
			String varType = "bool ";

			sb.append("\t\t\t" + varType + varName + " = ");
			genExpression(sb, changedTarget, state);
			sb.append(";\n");

			String prefix = "\t\t\t" + varName + changedOperation;
			
			genCompoundAssignmentVar(sb, state, cass, prefix, ";\n");

			genChangingAttribute(sb, state, changedTarget, "Assign", varName, "null");	

			sb.append("\t\t\t");				
			genExpression(sb, changedTarget, state);
			sb.append(" = " + varName + ";\n");
		} else {
			genCompoundAssignmentVar(sb, state, cass, "\t\t\t", ";\n");
		}
	}
	
	private void genCompoundAssignmentVarChangedVar(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentVarChangedVar cass)
	{
		Variable changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation()==CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation()==CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";
		
		String prefix = "\t\t\t" + "var_" + changedTarget.getIdent() + changedOperation;
		
		genCompoundAssignmentVar(sb, state, cass, prefix, ";\n");
	}
	
	private void genCompoundAssignmentVarChangedVisited(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentVarChangedVisited cass)
	{
		Visited changedTarget = cass.getChangedTarget();

		StringBuffer changedTargetBuffer = new StringBuffer();
		genExpression(changedTargetBuffer, changedTarget.getEntity(), state);
		changedTargetBuffer.append(", ");
		genExpression(changedTargetBuffer, changedTarget.getVisitorID(), state);

		String prefix = "\t\t\t" + "graph.SetVisited("
			+ changedTargetBuffer.toString() + ", ";
		if(cass.getChangedOperation()!=CompoundAssignment.ASSIGN) {
			prefix += "graph.IsVisited(" + changedTargetBuffer.toString() + ")"
				+ (cass.getChangedOperation()==CompoundAssignment.UNION ? " | " : " & ");
		}

		genCompoundAssignmentVar(sb, state, cass, prefix, ");\n");
	}

	private void genCompoundAssignmentVar(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentVar cass,
			String prefix, String postfix)
	{
		Variable target = cass.getTarget();
		assert(target.getType() instanceof MapType || target.getType() instanceof SetType 
				|| target.getType() instanceof ArrayType || target.getType() instanceof DequeType);
		Expression expr = cass.getExpression();

		sb.append(prefix);
		if(cass.getOperation()==CompoundAssignment.UNION)
			sb.append("GRGEN_LIBGR.ContainerHelper.UnionChanged(");
		else if(cass.getOperation()==CompoundAssignment.INTERSECTION)
			sb.append("GRGEN_LIBGR.ContainerHelper.IntersectChanged(");
		else if(cass.getOperation()==CompoundAssignment.WITHOUT)
			sb.append("GRGEN_LIBGR.ContainerHelper.ExceptChanged(");
		else //if(cass.getOperation()==CompoundAssignment.CONCATENATE)
			sb.append("GRGEN_LIBGR.ContainerHelper.ConcatenateChanged(");
		sb.append("var_" + target.getIdent());
		sb.append(", ");
		genExpression(sb, expr, state);
		sb.append(")");
		sb.append(postfix);
	}

	private void genMapRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, MapRemoveItem mri) {
		Qualification target = mri.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, mri.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "RemoveElement", "null", keyExprStr);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(".Remove(");
		if(mri.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mri.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append(");\n");

		if(mri.getNext()!=null) {
			genEvalStmt(sb, state, mri.getNext());
		}
	}

	private void genMapClear(StringBuffer sb, ModifyGenerationStateConst state, MapClear mc) {
		Qualification target = mc.getTarget();

		genClearAttribute(sb, state, target);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		if(mc.getNext()!=null) {
			genEvalStmt(sb, state, mc.getNext());
		}
	}

	private void genMapAddItem(StringBuffer sb, ModifyGenerationStateConst state, MapAddItem mai) {
		Qualification target = mai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, mai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();
		sbtmp.delete(0, sbtmp.length());
		genExpression(sbtmp, mai.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, keyExprStr);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append("[");
		if(mai.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mai.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append("] = ");
		if(mai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(";\n");

		if(mai.getNext()!=null) {
			genEvalStmt(sb, state, mai.getNext());
		}
	}

	private void genSetRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, SetRemoveItem sri) {
		Qualification target = sri.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, sri.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "RemoveElement", valueExprStr, "null");

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(".Remove(");
		if(sri.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(sri.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		if(sri.getNext()!=null) {
			genEvalStmt(sb, state, sri.getNext());
		}
	}

	private void genSetClear(StringBuffer sb, ModifyGenerationStateConst state, SetClear sc) {
		Qualification target = sc.getTarget();

		genClearAttribute(sb, state, target);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		if(sc.getNext()!=null) {
			genEvalStmt(sb, state, sc.getNext());
		}
	}

	private void genSetAddItem(StringBuffer sb, ModifyGenerationStateConst state, SetAddItem sai) {
		Qualification target = sai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, sai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, "null");

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append("[");
		if(sai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(sai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append("] = null;\n");

		if(sai.getNext()!=null) {
			genEvalStmt(sb, state, sai.getNext());
		}
	}

	private void genArrayRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, ArrayRemoveItem ari) {
		Qualification target = ari.getTarget();

		String indexStr = "null";
		if(ari.getIndexExpr()!=null) {
			StringBuffer sbtmp = new StringBuffer();
			genExpression(sbtmp, ari.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}
		
		genChangingAttribute(sb, state, target, "RemoveElement", "null", indexStr);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(".RemoveAt(");
		if(ari.getIndexExpr()!=null) {
			sb.append(indexStr);
		} else {
			sb.append("(");
			genExpression(sb, target, state);
			sb.append(").Count - 1");
		}
		sb.append(");\n");

		if(ari.getNext()!=null) {
			genEvalStmt(sb, state, ari.getNext());
		}
	}

	private void genArrayClear(StringBuffer sb, ModifyGenerationStateConst state, ArrayClear ac) {
		Qualification target = ac.getTarget();

		genClearAttribute(sb, state, target);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		if(ac.getNext()!=null) {
			genEvalStmt(sb, state, ac.getNext());
		}
	}

	private void genArrayAddItem(StringBuffer sb, ModifyGenerationStateConst state, ArrayAddItem aai) {
		Qualification target = aai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, aai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();
		
		sbtmp = new StringBuffer();
		String indexExprStr = "null";
		if(aai.getIndexExpr()!=null) {
			genExpression(sbtmp, aai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, indexExprStr);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		if(aai.getIndexExpr()==null) {
			sb.append(".Add(");
		} else {
			sb.append(".Insert(");
			sb.append(indexExprStr);
			sb.append(", ");
		}
		if(aai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(aai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");
		
		if(aai.getNext()!=null) {
			genEvalStmt(sb, state, aai.getNext());
		}
	}

	private void genDequeRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, DequeRemoveItem dri) {
		Qualification target = dri.getTarget();

		String indexStr = "null";
		if(dri.getIndexExpr()!=null) {
			StringBuffer sbtmp = new StringBuffer();
			genExpression(sbtmp, dri.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}

		genChangingAttribute(sb, state, target, "RemoveElement", "null", indexStr);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		if(dri.getIndexExpr()!=null) {
			sb.append(".DequeueAt(" + indexStr + ");\n");
		} else {
			sb.append(".Dequeue();\n");
		}

		if(dri.getNext()!=null) {
			genEvalStmt(sb, state, dri.getNext());
		}
	}

	private void genDequeClear(StringBuffer sb, ModifyGenerationStateConst state, DequeClear dc) {
		Qualification target = dc.getTarget();

		genClearAttribute(sb, state, target);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		if(dc.getNext()!=null) {
			genEvalStmt(sb, state, dc.getNext());
		}
	}

	private void genDequeAddItem(StringBuffer sb, ModifyGenerationStateConst state, DequeAddItem dai) {
		Qualification target = dai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, dai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sbtmp = new StringBuffer();
		String indexExprStr = "null";
		if(dai.getIndexExpr()!=null) {
			genExpression(sbtmp, dai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, indexExprStr);

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		if(dai.getIndexExpr()==null) {
			sb.append(".Enqueue(");
		} else {
			sb.append(".EnqueueAt(");
			sb.append(indexExprStr);
			sb.append(", ");
		}		
		if(dai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(dai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");
		
		if(dai.getNext()!=null) {
			genEvalStmt(sb, state, dai.getNext());
		}
	}

	private void genMapVarRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, MapVarRemoveItem mvri) {
		Variable target = mvri.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, mvri.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append(".Remove(");
		if(mvri.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mvri.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append(");\n");
		
		assert mvri.getNext()==null;
	}

	private void genMapVarClear(StringBuffer sb, ModifyGenerationStateConst state, MapVarClear mvc) {
		Variable target = mvc.getTarget();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append(".Clear();\n");
		
		assert mvc.getNext()==null;
	}

	private void genMapVarAddItem(StringBuffer sb, ModifyGenerationStateConst state, MapVarAddItem mvai) {
		Variable target = mvai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, mvai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();
		sbtmp.delete(0, sbtmp.length());
		genExpression(sbtmp, mvai.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append("[");
		if(mvai.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mvai.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append("] = ");
		if(mvai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mvai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(";\n");

		assert mvai.getNext()==null;
	}

	private void genSetVarRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, SetVarRemoveItem svri) {
		Variable target = svri.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, svri.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append(".Remove(");
		if(svri.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(svri.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");
		
		assert svri.getNext()==null;
	}

	private void genSetVarClear(StringBuffer sb, ModifyGenerationStateConst state, SetVarClear svc) {
		Variable target = svc.getTarget();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append(".Clear();\n");
		
		assert svc.getNext()==null;
	}

	private void genSetVarAddItem(StringBuffer sb, ModifyGenerationStateConst state, SetVarAddItem svai) {
		Variable target = svai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, svai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append("[");
		if(svai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(svai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append("] = null;\n");
		
		assert svai.getNext()==null;
	}

	private void genArrayVarRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, ArrayVarRemoveItem avri) {
		Variable target = avri.getTarget();

		String indexStr = "null";
		if(avri.getIndexExpr()!=null) {
			StringBuffer sbtmp = new StringBuffer();
			genExpression(sbtmp, avri.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append(".RemoveAt(");

		if(avri.getIndexExpr()!=null) {
			sb.append(indexStr);
		} else {
			sb.append("(");
			sb.append("\t\t\tvar_" + target.getIdent());
			sb.append(").Count - 1");
		}
		sb.append(");\n");
		
		assert avri.getNext()==null;
	}

	private void genArrayVarClear(StringBuffer sb, ModifyGenerationStateConst state, ArrayVarClear avc) {
		Variable target = avc.getTarget();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append(".Clear();\n");
		
		assert avc.getNext()==null;
	}

	private void genArrayVarAddItem(StringBuffer sb, ModifyGenerationStateConst state, ArrayVarAddItem avai) {
		Variable target = avai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, avai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sbtmp = new StringBuffer();
		String indexExprStr = "null";
		if(avai.getIndexExpr()!=null) {
			genExpression(sbtmp, avai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		sb.append("\t\t\t");
		sb.append("\t\t\tvar_" + target.getIdent());
		if(avai.getIndexExpr()==null) {
			sb.append(".Add(");
		} else {
			sb.append(".Insert(");
			sb.append(indexExprStr);
			sb.append(", ");
		}
		if(avai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(avai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		assert avai.getNext()==null;
	}

	private void genDequeVarRemoveItem(StringBuffer sb, ModifyGenerationStateConst state, DequeVarRemoveItem dvri) {
		Variable target = dvri.getTarget();

		String indexStr = "null";
		if(dvri.getIndexExpr()!=null) {
			StringBuffer sbtmp = new StringBuffer();
			genExpression(sbtmp, dvri.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}

		sb.append("\t\t\tvar_" + target.getIdent());

		if(dvri.getIndexExpr()!=null) {
			sb.append(".DequeueAt(" + indexStr + ");\n");
		} else {
			sb.append(".Dequeue();\n");
		}

		assert dvri.getNext()==null;
	}

	private void genDequeVarClear(StringBuffer sb, ModifyGenerationStateConst state, DequeVarClear dvc) {
		Variable target = dvc.getTarget();

		sb.append("\t\t\tvar_" + target.getIdent());
		sb.append(".Clear();\n");
		
		assert dvc.getNext()==null;
	}

	private void genDequeVarAddItem(StringBuffer sb, ModifyGenerationStateConst state, DequeVarAddItem dvai) {
		Variable target = dvai.getTarget();

		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, dvai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sbtmp = new StringBuffer();
		String indexExprStr = "null";
		if(dvai.getIndexExpr()!=null) {
			genExpression(sbtmp, dvai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		sb.append("\t\t\t");
		sb.append("\t\t\tvar_" + target.getIdent());
		if(dvai.getIndexExpr()==null) {
			sb.append(".Enqueue(");
		} else {
			sb.append(".EnqueueAt(");
			sb.append(indexExprStr);
			sb.append(", ");
		}		
		if(dvai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(dvai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		assert dvai.getNext()==null;
	}

	private void genReturnStatement(StringBuffer sb, ModifyGenerationStateConst state, ReturnStatement rs) {
		sb.append("\t\t\treturn ");
		genExpression(sb, rs.getReturnValueExpr(), state);
		sb.append(";\n");
	}

	private void genConditionStatement(StringBuffer sb, ModifyGenerationStateConst state, ConditionStatement cs) {
		sb.append("\t\t\tif(");
		genExpression(sb, cs.getConditionExpr(), state);
		sb.append(") {\n");
		genEvals(sb, state, cs.getTrueCaseStatements());
		if(cs.getFalseCaseStatements()!=null) {
			sb.append("\t\t\t} else {\n");			
			genEvals(sb, state, cs.getFalseCaseStatements());
		}
		sb.append("\t\t\t}\n");
	}

	private void genWhileStatement(StringBuffer sb, ModifyGenerationStateConst state, WhileStatement ws) {
		sb.append("\t\t\twhile(");
		genExpression(sb, ws.getConditionExpr(), state);
		sb.append(") {\n");
		genEvals(sb, state, ws.getLoopedStatements());
		sb.append("}\n");
	}

	private void genDoWhileStatement(StringBuffer sb, ModifyGenerationStateConst state, DoWhileStatement dws) {
		sb.append("\t\t\tdo {\n");
		genEvals(sb, state, dws.getLoopedStatements());
		sb.append("\t\t\t} while(");
		genExpression(sb, dws.getConditionExpr(), state);
		sb.append(");\n");
	}

	private void genDefDeclVarStatement(StringBuffer sb, ModifyGenerationStateConst state, DefDeclVarStatement ddvs) {
		Variable var = ddvs.getTarget();
		sb.append("\t\t\t" + formatType(var.getType()) + " " + formatEntity(var));
		if(var.initialization!=null) {
			sb.append(" = ");
			sb.append("(" + formatType(var.getType()) + ")(");
			genExpression(sb, var.initialization, state);		
			sb.append(")");
		}
		sb.append(";\n");
	}

	private void genDefDeclGraphEntityStatement(StringBuffer sb, ModifyGenerationStateConst state, DefDeclGraphEntityStatement ddges) {
		GraphEntity graphEntity = ddges.getTarget();
		sb.append("\t\t\t" + formatType(graphEntity.getType()) + " " + formatEntity(graphEntity));
		if(graphEntity.initialization!=null) {
			sb.append(" = ");
			sb.append("(" + formatType(graphEntity.getType()) + ")(");
			genExpression(sb, graphEntity.initialization, state);		
			sb.append(")");
		}
		sb.append(";\n");
	}

	private void genContainerAccumulationYield(StringBuffer sb, ModifyGenerationStateConst state, ContainerAccumulationYield cay) {
        if(cay.getContainer().getType() instanceof ArrayType)
        {
        	Type arrayValueType = ((ArrayType)cay.getContainer().getType()).getValueType();
        	String arrayValueTypeStr = formatType(arrayValueType);
        	String indexVar = "index_" + tmpVarID++;
        	String entryVar = "entry_" + tmpVarID++; // for the container itself
            sb.append("\t\t\tList<" + arrayValueTypeStr + "> " + entryVar + " = (List<" + arrayValueTypeStr + ">) " + formatEntity(cay.getContainer()) + ";\n");
            sb.append("\t\t\tfor(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
            sb.append("\t\t\t{\n");

            if(cay.getIndexVar() != null)
            {
                if(!Expression.isGlobalVariable(cay.getIndexVar()) || (cay.getIndexVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                    sb.append("\t\t\t" + "int" + " " + formatEntity(cay.getIndexVar()) + " = " + indexVar + ";\n");
        		} else {
        			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIndexVar(), indexVar) + ";\n");
        		}
        		if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                    sb.append("\t\t\t" + arrayValueTypeStr + " " + formatEntity(cay.getIterationVar()) + " = " + entryVar + "[" + indexVar + "];\n");
        		} else {
        			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
        		}
            }
            else
            {
        		if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                    sb.append("\t\t\t" + arrayValueTypeStr + " "  + formatEntity(cay.getIterationVar()) + " = " + entryVar + "[" + indexVar + "];\n");
        		} else {
        			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
        		}
            }

    		genEvals(sb, state, cay.getAccumulationStatements());

            sb.append("\t\t\t}\n");
        }
        else if(cay.getContainer().getType() instanceof DequeType)
        {
        	Type dequeValueType = ((DequeType)cay.getContainer().getType()).getValueType();
        	String dequeValueTypeStr = formatType(dequeValueType);
        	String indexVar = "index_" + tmpVarID++;
        	String entryVar = "entry_" + tmpVarID++; // for the container itself
            sb.append("\t\t\tGRGEN_LIBGR.Deque<" + dequeValueTypeStr + "> " + entryVar + " = (GRGEN_LIBGR.Deque<" + dequeValueTypeStr + ">) " + formatEntity(cay.getContainer()) + ";\n");
            sb.append("\t\t\tfor(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
            sb.append("\t\t\t{\n");

            if(cay.getIndexVar() != null)
            {
                if(!Expression.isGlobalVariable(cay.getIndexVar()) || (cay.getIndexVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                    sb.append("\t\t\t" + "int" + " " + formatEntity(cay.getIndexVar()) + " = " + indexVar + ";\n");
        		} else {
        			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIndexVar(), indexVar) + ";\n");
        		}
        		if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                    sb.append("\t\t\t" + dequeValueTypeStr + " " + formatEntity(cay.getIterationVar()) + " = " + entryVar + "[" + indexVar + "];\n");
        		} else {
        			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
        		}
            }
            else
            {
        		if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                    sb.append("\t\t\t" + dequeValueTypeStr + " " + formatEntity(cay.getIterationVar()) + " = " + entryVar + "[" + indexVar + "];\n");
        		} else {
        			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
        		}
            }

    		genEvals(sb, state, cay.getAccumulationStatements());

            sb.append("\t\t\t}\n");
        }
        else if(cay.getContainer().getType() instanceof SetType)
        {
        	Type setValueType = ((SetType)cay.getContainer().getType()).getValueType();
        	String setValueTypeStr = formatType(setValueType);
        	String entryVar = "entry_" + tmpVarID++;
            sb.append("\t\t\tforeach(KeyValuePair<" + setValueTypeStr + ", GRGEN_LIBGR.SetValueType> " + entryVar + " in " + formatEntity(cay.getContainer()) + ")\n");
            sb.append("\t\t\t{\n");

    		if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                sb.append("\t\t\t" + setValueTypeStr + " " + formatEntity(cay.getIterationVar()) + " = " + entryVar + ".Key;\n");
    		} else {
    			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIterationVar(), entryVar + ".Key") + ";\n");
    		}

    		genEvals(sb, state, cay.getAccumulationStatements());

            sb.append("\t\t\t}\n");
        }
        else //if(cay.getContainer().getType() instanceof MapType)
        {
        	Type mapKeyType = ((MapType)cay.getContainer().getType()).getKeyType();
        	String mapKeyTypeStr = formatType(mapKeyType);
        	Type mapValueType = ((MapType)cay.getContainer().getType()).getValueType();
        	String mapValueTypeStr = formatType(mapValueType);
        	String entryVar = "entry_" + tmpVarID++;
            sb.append("\t\t\tforeach(KeyValuePair<" + mapKeyTypeStr + ", " + mapValueTypeStr + "> " + entryVar + " in " + formatEntity(cay.getContainer()) + ")\n");
            sb.append("\t\t\t{\n");

    		if(!Expression.isGlobalVariable(cay.getIndexVar()) || (cay.getIndexVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                sb.append("\t\t\t" + mapKeyTypeStr + " " + formatEntity(cay.getIndexVar()) + " = " + entryVar + ".Key;\n");
    		} else {
    			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIndexVar(), entryVar + ".Key") + ";\n");
    		}
    		if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()&BaseNode.CONTEXT_COMPUTATION)==BaseNode.CONTEXT_COMPUTATION) {
                sb.append("\t\t\t" + mapValueTypeStr + " " + formatEntity(cay.getIterationVar()) + " = " + entryVar + ".Value;\n");
    		} else {
    			sb.append("\t\t\t" + formatGlobalVariableWrite(cay.getIterationVar(), entryVar + ".Value") + ";\n");
    		}

    		genEvals(sb, state, cay.getAccumulationStatements());

            sb.append("\t\t\t}\n");            
        }
	}

	private void genForLookup(StringBuffer sb, ModifyGenerationStateConst state, ForLookup fl) {
    	String id = Integer.toString(tmpVarID++);

        if(fl.getIterationVar().getType() instanceof NodeType)
        {
        	sb.append("\t\t\tforeach(GRGEN_LIBGR.INode node_" + id + " in graph.GetCompatibleNodes(GRGEN_LIBGR.TypesHelper.GetNodeType(\""+formatIdentifiable(fl.getIterationVar().getType())+"\", graph.Model)))\n");
        	sb.append("\t\t\t{\n");
            sb.append("\t\t\t" + formatElementClassRef(fl.getIterationVar().getType()) + " " + formatEntity(fl.getIterationVar()) + " = " + "(" + formatElementClassRef(fl.getIterationVar().getType()) + ")node_" + id + ";\n");
        }
        else
        {
        	sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id + " in graph.GetCompatibleEdges(GRGEN_LIBGR.TypesHelper.GetEdgeType(\""+formatIdentifiable(fl.getIterationVar().getType())+"\", graph.Model)))\n");
        	sb.append("\t\t\t{\n");
            sb.append("\t\t\t" + formatElementClassRef(fl.getIterationVar().getType()) + " " + formatEntity(fl.getIterationVar()) + " = " + "(" + formatElementClassRef(fl.getIterationVar().getType()) + ")edge_" + id + ";\n");
        }

		genEvals(sb, state, fl.getLoopedStatements());
        
        sb.append("\t\t\t}\n");
	}

	private void genForFunction(StringBuffer sb, ModifyGenerationStateConst state, ForFunction ff) {
    	String id = Integer.toString(tmpVarID++);
    	
		if(ff.getFunction() instanceof AdjacentNodeExpr) {
			AdjacentNodeExpr adjacent = (AdjacentNodeExpr)ff.getFunction();
			if(adjacent.Direction()==AdjacentNodeExpr.ADJACENT) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, adjacent.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
		        		+ ".GetCompatibleIncident(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t{\n");
	
		        sb.append("\t\t\tif(!edge_" + id + ".Opposite(node_" + id + ").InstanceOf(GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, adjacent.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t\tcontinue;\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ".Opposite(node_" + id + ");\n");
			}
			else if(adjacent.Direction()==AdjacentNodeExpr.INCOMING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, adjacent.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
		        		+ ".GetCompatibleIncoming(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t{\n");
	
		        sb.append("\t\t\tif(!edge_" + id + ".Source.InstanceOf(GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, adjacent.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t\tcontinue;\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ".Source;\n");
			}
			else if(adjacent.Direction()==AdjacentNodeExpr.OUTGOING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, adjacent.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
		        		+ ".GetCompatibleOutgoing(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t{\n");
	
		        sb.append("\t\t\tif(!edge_" + id + ".Target.InstanceOf(GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, adjacent.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t\tcontinue;\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ".Target;\n");
			}
		}
		else if(ff.getFunction() instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr incident = (IncidentEdgeExpr)ff.getFunction();
			if(incident.Direction()==IncidentEdgeExpr.INCIDENT) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, incident.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
		        		+ ".GetCompatibleIncident(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t{\n");
	
		        sb.append("\t\t\tif(!edge_" + id + ".Opposite(node_" + id + ").InstanceOf(GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, incident.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t\tcontinue;\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
			else if(incident.Direction()==IncidentEdgeExpr.INCOMING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, incident.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
		        		+ ".GetCompatibleIncoming(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t{\n");
	
		        sb.append("\t\t\tif(!edge_" + id + ".Source.InstanceOf(GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, incident.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t\tcontinue;\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
			else if(incident.Direction()==IncidentEdgeExpr.OUTGOING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, incident.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
		        		+ ".GetCompatibleOutgoing(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t{\n");
	
		        sb.append("\t\t\tif(!edge_" + id + ".Target.InstanceOf(GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, incident.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model)))\n");
		        sb.append("\t\t\t\tcontinue;\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
		}
		else if(ff.getFunction() instanceof ReachableNodeExpr) {
			ReachableNodeExpr reachable = (ReachableNodeExpr)ff.getFunction();
			if(reachable.Direction()==ReachableNodeExpr.ADJACENT) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.INode iter_" + id 
		        		+ " in GRGEN_LIBGR.GraphHelper.Reachable(node_" + id + ",");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model),");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model),"); 
				sb.append("graph))\n");
		        sb.append("\t\t\t{\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			}
			else if(reachable.Direction()==ReachableNodeExpr.INCOMING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.INode iter_" + id 
		        		+ " in GRGEN_LIBGR.GraphHelper.ReachableIncoming(node_" + id + ",");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model),");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model),"); 
				sb.append("graph))\n");
		        sb.append("\t\t\t{\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			}
			else if(reachable.Direction()==ReachableNodeExpr.OUTGOING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.INode iter_" + id 
		        		+ " in GRGEN_LIBGR.GraphHelper.ReachableOutgoing(node_" + id + ",");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model),");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model),"); 
				sb.append("graph))\n");
		        sb.append("\t\t\t{\n");
		        sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			}
		}
		else if(ff.getFunction() instanceof ReachableEdgeExpr) {
			ReachableEdgeExpr reachable = (ReachableEdgeExpr)ff.getFunction();
			if(reachable.Direction()==ReachableEdgeExpr.INCIDENT) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id 
		        		+ " in GRGEN_LIBGR.GraphHelper.Reachable(node_" + id + ",");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model),");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model),"); 
				sb.append("graph))\n");
		        sb.append("\t\t\t{\n");
			    sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
			else if(reachable.Direction()==ReachableEdgeExpr.INCOMING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id 
		        		+ " in GRGEN_LIBGR.GraphHelper.ReachableIncoming(node_" + id + ",");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model),");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model),"); 
				sb.append("graph))\n");
		        sb.append("\t\t\t{\n");
			    sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
			else if(reachable.Direction()==ReachableEdgeExpr.OUTGOING) {
				sb.append("\t\t\tGRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);	        
		        sb.append(";\n");
		        sb.append("\t\t\tforeach(GRGEN_LIBGR.IEdge edge_" + id 
		        		+ " in GRGEN_LIBGR.GraphHelper.ReachableOutgoing(node_" + id + ",");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetEdgeType(\"");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);	        
		        sb.append("\", graph.Model),");
		        sb.append("GRGEN_LIBGR.TypesHelper.GetNodeType(\"");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);	        
				sb.append("\", graph.Model),"); 
				sb.append("graph))\n");
		        sb.append("\t\t\t{\n");
			    sb.append("\t\t\t" + formatElementClassRef(ff.getIterationVar().getType()) + " " + formatEntity(ff.getIterationVar()) + " = (" + formatElementClassRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
		}

		genEvals(sb, state, ff.getLoopedStatements());

        sb.append("\t\t\t}\n");
	}

	private void genBreakStatement(StringBuffer sb, ModifyGenerationStateConst state, BreakStatement bs) {
		sb.append("\t\t\tbreak;\n");
	}

	private void genContinueStatement(StringBuffer sb, ModifyGenerationStateConst state, ContinueStatement cs) {
		sb.append("\t\t\tcontinue;\n");
	}

	private void genEmitStatement(StringBuffer sb, ModifyGenerationStateConst state, EmitStatement es) {
    	String emitVar = "emit_value_" + tmpVarID++;
		sb.append("\t\t\tobject " + emitVar + " = ");
		genExpression(sb, es.getToEmitExpr(), state);
		sb.append(";\n");
		sb.append("\t\t\tif(" + emitVar + " != null)\n");
		sb.append("\t\t\t\t((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).EmitWriter.Write("
				+ "GRGEN_LIBGR.ContainerHelper.ToStringNonNull(" + emitVar + ", graph));\n");
	}

	private void genRecordStatement(StringBuffer sb, ModifyGenerationStateConst state, RecordStatement rs) {
    	String recordVar = "record_value_" + tmpVarID++;
		sb.append("\t\t\tobject " + recordVar + " = ");
		genExpression(sb, rs.getToRecordExpr(), state);
		sb.append(";\n");
		sb.append("\t\t\tif(" + recordVar + " != null)\n");
		sb.append("\t\t\t\t((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).Recorder.Write("
				+ "GRGEN_LIBGR.ContainerHelper.ToStringNonNull(" + recordVar + ", graph));\n");
	}

	private void genGraphClearStatement(StringBuffer sb, ModifyGenerationStateConst state, GraphClear gc) {
		sb.append("\t\t\tgraph.Clear();\n");
	}

	private void genGraphRemoveStatement(StringBuffer sb, ModifyGenerationStateConst state, GraphRemove gr) {
        if(gr.getEntity().getType() instanceof NodeType) {
			sb.append("\t\t\tgraph.RemoveEdges((GRGEN_LIBGR.INode)");
			genExpression(sb, gr.getEntity(), state);
			sb.append(");\n");

			sb.append("\t\t\tgraph.Remove((GRGEN_LIBGR.INode)");
			genExpression(sb, gr.getEntity(), state);
			sb.append(");\n");
		} else {
			sb.append("\t\t\tgraph.Remove((GRGEN_LIBGR.IEdge)");
			genExpression(sb, gr.getEntity(), state);
			sb.append(");\n");
		}
	}

	private void genGraphMergeStatement(StringBuffer sb, ModifyGenerationStateConst state, GraphMerge gm) {
        if(gm.getSourceName() != null) {
			sb.append("\t\t\tgraph.Merge((GRGEN_LIBGR.INode)");
			genExpression(sb, gm.getTarget(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, gm.getSource(), state);
			sb.append(", (String)");
			genExpression(sb, gm.getSourceName(), state);
			sb.append(");\n");
		} else {
			sb.append("\t\t\t((GRGEN_LGSP.LGSPNamedGraph)graph).Merge((GRGEN_LIBGR.INode)");
			genExpression(sb, gm.getTarget(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, gm.getSource(), state);
			sb.append(");\n");
		}
	}

	private void genGraphRedirectSourceStatement(StringBuffer sb, ModifyGenerationStateConst state, GraphRedirectSource grs) {
        if(grs.getOldSourceName() != null) {
			sb.append("\t\t\tgraph.RedirectSource((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grs.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grs.getNewSource(), state);
			sb.append(", (String)");
			genExpression(sb, grs.getOldSourceName(), state);
			sb.append(");\n");
		} else {
			sb.append("\t\t\t((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectSource((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grs.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grs.getNewSource(), state);
			sb.append(");\n");
		}
	}

	private void genGraphRedirectTargetStatement(StringBuffer sb, ModifyGenerationStateConst state, GraphRedirectTarget grt) {
        if(grt.getOldTargetName() != null) {
			sb.append("\t\t\tgraph.RedirectTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grt.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grt.getNewTarget(), state);
			sb.append(", (String)");
			genExpression(sb, grt.getOldTargetName(), state);
			sb.append(");\n");
		} else {
			sb.append("\t\t\t((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grt.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grt.getNewTarget(), state);
			sb.append(");\n");
		}
	}

	private void genGraphRedirectSourceAndTargetStatement(StringBuffer sb, ModifyGenerationStateConst state, GraphRedirectSourceAndTarget grsat) {
        if(grsat.getOldSourceName() != null) {
			sb.append("\t\t\tgraph.RedirectSourceAndTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grsat.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsat.getNewSource(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsat.getNewTarget(), state);
			sb.append(", (String)");
			genExpression(sb, grsat.getOldSourceName(), state);
			sb.append(", (String)");
			genExpression(sb, grsat.getOldTargetName(), state);
			sb.append(");\n");
		} else {
			sb.append("\t\t\t((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectSourceAndTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grsat.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsat.getNewSource(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsat.getNewTarget(), state);
			sb.append(");\n");
		}
	}

	private void genVFreeStatement(StringBuffer sb, ModifyGenerationStateConst state, VFreeStatement vfs) {
		sb.append("\t\t\tgraph.FreeVisitedFlag((int)");
		genExpression(sb, vfs.getVisitedFlagExpr(), state);
		sb.append(");\n");
	}
	
	private void genVFreeNonResetStatement(StringBuffer sb, ModifyGenerationStateConst state, VFreeNonResetStatement vfnrs) {
		sb.append("\t\t\tgraph.FreeVisitedFlagNonReset((int)");
		genExpression(sb, vfnrs.getVisitedFlagExpr(), state);
		sb.append(");\n");
	}
	
	private void genVResetStatement(StringBuffer sb, ModifyGenerationStateConst state, VResetStatement vrs) {
		sb.append("\t\t\tgraph.ResetVisitedFlag((int)");
		genExpression(sb, vrs.getVisitedFlagExpr(), state);
		sb.append(");\n");
	}

	private void genPauseTransactionStatement(StringBuffer sb, ModifyGenerationStateConst state, PauseTransaction pt) {
		sb.append("\t\t\t((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Pause();\n");
	}

	private void genResumeTransactionStatement(StringBuffer sb, ModifyGenerationStateConst state, ResumeTransaction rt) {
		sb.append("\t\t\t((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Resume();\n");
	}

	private void genCommitTransactionStatement(StringBuffer sb, ModifyGenerationStateConst state, CommitTransaction ct) {
		sb.append("\t\t\t((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Commit((int)");
		genExpression(sb, ct.getTransactionId(), state);
		sb.append(");\n");
	}

	private void genRollbackTransactionStatement(StringBuffer sb, ModifyGenerationStateConst state, RollbackTransaction rt) {
		sb.append("\t\t\t((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Rollback((int)");
		genExpression(sb, rt.getTransactionId(), state);
		sb.append(");\n");
	}

	private void genCallStatement(StringBuffer sb, ModifyGenerationStateConst state, CallStatement cs) {
		sb.append("\t\t\tobject dummyvar_" + tmpVarID++ + " = "); // needed cause some function calls come with result cast, which is not allowed as C# statement
		genExpression(sb, cs.getCalledFunction(), state);
		sb.append(";\n");
	}

	private void genExecStatement(StringBuffer sb, ModifyGenerationStateConst state, ExecStatement es) {
		Exec exec = es.getExec();
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof GraphEntity) {
					sb.append("\t\t\t" + formatElementInterfaceRef(neededEntity.getType()) + " ");
					sb.append("tmp_" + formatEntity(neededEntity) + "_" + embeddedComputationXgrsID + " = ");
					sb.append("("+formatElementInterfaceRef(neededEntity.getType())+")");
					sb.append(formatEntity(neededEntity) + ";\n");
				}
				else { // if(neededEntity instanceof Variable) 
					sb.append("\t\t\t" + formatAttributeType(neededEntity.getType()) + " ");
					sb.append("tmp_" + formatEntity(neededEntity) + "_" + embeddedComputationXgrsID + " = ");
					sb.append("("+formatAttributeType(neededEntity.getType())+")");
					sb.append(formatEntity(neededEntity) + ";\n");
				}
			}
		}
		sb.append("\t\t\tApplyXGRS_" + state.computationName() + "_" + embeddedComputationXgrsID + "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append(", ");
				if(neededEntity.getType() instanceof InheritanceType) {
					sb.append("("+formatElementInterfaceRef(neededEntity.getType())+")");
				}
				sb.append(formatEntity(neededEntity));
			}
		}
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append(", ref ");
				sb.append("tmp_" + formatEntity(neededEntity) + "_" + embeddedComputationXgrsID);
			}
		}
		sb.append(");\n");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append("\t\t\t" + formatEntity(neededEntity) + " = ");
				if(neededEntity instanceof Node) {
					sb.append("(GRGEN_LGSP.LGSPNode)");
				} else if(neededEntity instanceof Edge) {
					sb.append("(GRGEN_LGSP.LGSPEdge)");
				}
				sb.append("tmp_" + formatEntity(neededEntity) + "_" + embeddedComputationXgrsID + ";\n");
			}
		}
		
		++embeddedComputationXgrsID;
	}

	//////////////////////
	
	protected void genChangingAttribute(StringBuffer sb, ModifyGenerationStateConst state,
			Qualification target, String attributeChangeType, String newValue, String keyValue)
	{
		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		String kindStr = null;
		boolean isDeletedElem = false;
		if(element instanceof Node) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		}
		else if(element instanceof Edge) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		}
		else assert false : "Entity is neither a node nor an edge (" + element + ")!";

		if(!isDeletedElem && be.system.mayFireEvents()) {
			if(!Expression.isGlobalVariable(element)) {
				sb.append("\t\t\tgraph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) +	", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType." + attributeChangeType + ", " +
						newValue + ", " + keyValue + ");\n");
			} else {
				sb.append("\t\t\tgraph.Changing" + kindStr + "Attribute(" +
						formatGlobalVariableRead(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType." + attributeChangeType + ", " +
						newValue + ", " + keyValue + ");\n");
			}
		}
	}

	protected void genClearAttribute(StringBuffer sb, ModifyGenerationStateConst state, Qualification target)
	{		
		StringBuffer sbtmp = new StringBuffer();
		genExpression(sbtmp, target, state);
		String targetStr = sbtmp.toString();

		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		String kindStr = null;
		boolean isDeletedElem = false;
		if(element instanceof Node) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		}
		else if(element instanceof Edge) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		}
		else assert false : "Entity is neither a node nor an edge (" + element + ")!";

		if(!isDeletedElem && be.system.mayFireEvents()) {
			if(attribute.getType() instanceof MapType) {
				MapType attributeType = (MapType)attribute.getType();
				sb.append("\t\t\tforeach(KeyValuePair<" + formatType(attributeType.getKeyType()) + "," + formatType(attributeType.getValueType()) + "> kvp " +
						"in " + targetStr + ")\n");
				sb.append("\t\t\t\tgraph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) +	", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"null, kvp.Key);\n");
			} else if(attribute.getType() instanceof SetType) {
				SetType attributeType = (SetType)attribute.getType();
				sb.append("\t\t\tforeach(KeyValuePair<" + formatType(attributeType.getValueType()) + ", GRGEN_LIBGR.SetValueType> kvp " +
						"in " + targetStr + ")\n");
				sb.append("\t\t\t\tgraph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) +	", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"kvp.Key, null);\n");
			} else if(attribute.getType() instanceof ArrayType) {
				sb.append("\t\t\tfor(int i = " + targetStr + ".Count; i>=0; --i)\n");
				sb.append("\t\t\t\tgraph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) +	", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"null, i);\n");
			} else if(attribute.getType() instanceof DequeType) {
				sb.append("\t\t\tfor(int i = " + targetStr + ".Count; i>=0; --i)\n");
				sb.append("\t\t\t\tgraph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) +	", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"null, i);\n");
			} else {
				assert(false);
			}
		}
	}

	//////////////////////
	// Expression stuff //
	//////////////////////

	protected void genQualAccess(StringBuffer sb, Qualification qual, Object modifyGenerationState) {
		genQualAccess(sb, qual, (ModifyGenerationState)modifyGenerationState);
	}

	protected void genQualAccess(StringBuffer sb, Qualification qual, ModifyGenerationStateConst state) {
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		genQualAccess(sb, state, owner, member);
	}

	protected void genQualAccess(StringBuffer sb, ModifyGenerationStateConst state, Entity owner, Entity member) {
		if(!Expression.isGlobalVariable(owner)) {
			if(state==null) {
				assert false;
				sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
				return;
			}
	
			if(accessViaVariable(state, (GraphEntity) owner, member)) {
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

	protected void genMemberAccess(StringBuffer sb, Entity member) {
		throw new UnsupportedOperationException("Member expressions not allowed in actions!");
	}

	private boolean accessViaVariable(ModifyGenerationStateConst state, GraphEntity elem, Entity attr) {
		HashSet<Entity> forcedAttrs = state.forceAttributeToVar().get(elem);
		return forcedAttrs != null && forcedAttrs.contains(attr);
	}

	private void genVariable(StringBuffer sb, String ownerName, Entity entity) {
		String varTypeName;
		String attrName = formatIdentifiable(entity);
		Type type = entity.getType();
		if(type instanceof EnumType) {
			varTypeName = "GRGEN_MODEL.ENUM_" + formatIdentifiable(type);
		} else {
			varTypeName = getTypeNameForTempVarDecl(type);
		}

		sb.append("\t\t\t" + varTypeName + " tempvar_" + ownerName + "_" + attrName);
	}
}

