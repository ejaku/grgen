/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * ActionsGen.java
 *
 * Generates the actions file for the SearchPlanBackend2 backend.
 *
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id: ActionsGen.java 18216 2008-03-22 21:18:36Z eja $
 */

package de.unika.ipd.grgen.be.Csharp;

import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Cast;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Emit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.ImperativeStmt;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.RetypedEdge;
import de.unika.ipd.grgen.ir.RetypedNode;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.VariableExpression;
import de.unika.ipd.grgen.ir.Visited;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

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
		Collection<Assignment> evals;
		List<Entity> replParameters;
		List<Expression> returns;
		boolean isSubpattern;
		boolean reuseNodesAndEdges;

		public ModifyGenerationTask() {
			typeOfTask = TYPE_OF_TASK_NONE;
			left = null;
			right = null;
			parameters = null;
			evals = null;
			replParameters = null;
			returns = null;
			isSubpattern = false;
			reuseNodesAndEdges = false;
		}
	}

	interface ModifyGenerationStateConst {
		Collection<Node> commonNodes();
		Collection<Edge> commonEdges();
		Collection<SubpatternUsage> commonSubpatternUsages();

		Collection<Node> newNodes();
		Collection<Edge> newEdges();
		Collection<SubpatternUsage> newSubpatternUsages();
		Collection<Node> delNodes();
		Collection<Edge> delEdges();
		Collection<SubpatternUsage> delSubpatternUsages();

		Collection<Node> newOrRetypedNodes();
		Collection<Edge> newOrRetypedEdges();
		Collection<GraphEntity> reusedElements();
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
		public Collection<Node> commonNodes() { return Collections.unmodifiableCollection(commonNodes); }
		public Collection<Edge> commonEdges() { return Collections.unmodifiableCollection(commonEdges); }
		public Collection<SubpatternUsage> commonSubpatternUsages() { return Collections.unmodifiableCollection(commonSubpatternUsages); }

		public Collection<Node> newNodes() { return Collections.unmodifiableCollection(newNodes); }
		public Collection<Edge> newEdges() { return Collections.unmodifiableCollection(newEdges); }
		public Collection<SubpatternUsage> newSubpatternUsages() { return Collections.unmodifiableCollection(newSubpatternUsages); }
		public Collection<Node> delNodes() { return Collections.unmodifiableCollection(delNodes); }
		public Collection<Edge> delEdges() { return Collections.unmodifiableCollection(delEdges); }
		public Collection<SubpatternUsage> delSubpatternUsages() { return Collections.unmodifiableCollection(delSubpatternUsages); }

		public Collection<Node> newOrRetypedNodes() { return Collections.unmodifiableCollection(newOrRetypedNodes); }
		public Collection<Edge> newOrRetypedEdges() { return Collections.unmodifiableCollection(newOrRetypedEdges); }
		public Collection<GraphEntity> reusedElements() { return Collections.unmodifiableCollection(reusedElements); }
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

		// --------------------

		public HashSet<Node> commonNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> commonEdges = new LinkedHashSet<Edge>();
		public HashSet<SubpatternUsage> commonSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public HashSet<Node> newNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> newEdges = new LinkedHashSet<Edge>();
		public HashSet<SubpatternUsage> newSubpatternUsages = new LinkedHashSet<SubpatternUsage>();
		public HashSet<Node> delNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> delEdges = new LinkedHashSet<Edge>();
		public HashSet<SubpatternUsage> delSubpatternUsages = new LinkedHashSet<SubpatternUsage>();

		public HashSet<Node> newOrRetypedNodes = new LinkedHashSet<Node>();
		public HashSet<Edge> newOrRetypedEdges = new LinkedHashSet<Edge>();
		public HashSet<GraphEntity> reusedElements = new LinkedHashSet<GraphEntity>();
		public HashSet<GraphEntity> accessViaInterface = new LinkedHashSet<GraphEntity>();

		public HashMap<GraphEntity, HashSet<Entity>> neededAttributes = new LinkedHashMap<GraphEntity, HashSet<Entity>>();
		public HashMap<GraphEntity, HashSet<Entity>> attributesStoredBeforeDelete = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

		public HashSet<Variable> neededVariables = new LinkedHashSet<Variable>();

		public HashSet<Node> nodesNeededAsElements = new LinkedHashSet<Node>();
		public HashSet<Edge> edgesNeededAsElements = new LinkedHashSet<Edge>();
		public HashSet<Node> nodesNeededAsAttributes = new LinkedHashSet<Node>();
		public HashSet<Edge> edgesNeededAsAttributes = new LinkedHashSet<Edge>();
		public HashSet<Node> nodesNeededAsTypes = new LinkedHashSet<Node>();
		public HashSet<Edge> edgesNeededAsTypes = new LinkedHashSet<Edge>();

		public HashMap<GraphEntity, HashSet<Entity>> forceAttributeToVar = new LinkedHashMap<GraphEntity, HashSet<Entity>>();
	}

	final List<Entity> emptyParameters = new LinkedList<Entity>();
	final List<Expression> emptyReturns = new LinkedList<Expression>();
	final Collection<Assignment> emptyEvals = new HashSet<Assignment>();

	public ModifyGen(String nodeTypePrefix, String edgeTypePrefix) {
		super(nodeTypePrefix, edgeTypePrefix);
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
			task.replParameters = rule.getReplParameters();
			task.returns = rule.getReturns();
			task.isSubpattern = isSubpattern;
			task.reuseNodesAndEdges = true;
			genModifyRuleOrSubrule(sb, task, pathPrefix);

			// version without reuse deleted elements for new elements optimization
			task.reuseNodesAndEdges = false;
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
			task.reuseNodesAndEdges = true;
			genModifyRuleOrSubrule(sb, task, pathPrefix);

			// version without reuse deleted elements for new elements optimization
			// that's nonsense but the interface requires it
			task.reuseNodesAndEdges = false;
			genModifyRuleOrSubrule(sb, task, pathPrefix);
		}

		if(isSubpattern) {
			// create subpattern into pattern
			ModifyGenerationTask task = new ModifyGenerationTask();
			task.typeOfTask = TYPE_OF_TASK_CREATION;
			task.left = new PatternGraph(rule.getLeft().getNameOfGraph(), false); // empty graph
			task.right = rule.getLeft();
			task.parameters = rule.getParameters();
			task.evals = emptyEvals;
			task.replParameters = emptyParameters;
			task.returns = emptyReturns;
			task.isSubpattern = true;
			for(Entity entity : task.parameters) { // add connections to empty graph so that they stay unchanged
				if(entity instanceof Node) {
					Node node = (Node)entity;
					task.left.addSingleNode(node);
				} else if(entity instanceof Edge) {
					Edge edge = (Edge)entity;
					task.left.addSingleEdge(edge);
				} else {
					assert false;
				}
			}
			genModifyRuleOrSubrule(sb, task, pathPrefix);

			// delete subpattern from pattern
			task = new ModifyGenerationTask();
			task.typeOfTask = TYPE_OF_TASK_DELETION;
			task.left = rule.getLeft();
			task.right = new PatternGraph(rule.getLeft().getNameOfGraph(), false); // empty graph
			task.parameters = rule.getParameters();
			task.evals = emptyEvals;
			task.replParameters = emptyParameters;
			task.returns = emptyReturns;
			task.isSubpattern = true;
			for(Entity entity : task.parameters) { // add connections to empty graph so that they stay unchanged
				if(entity instanceof Node) {
					Node node = (Node)entity;
					task.right.addSingleNode(node);
				} else if(entity instanceof Edge) {
					Edge edge = (Edge)entity;
					task.right.addSingleEdge(edge);
				} else {
					assert false;
				}
			}
			genModifyRuleOrSubrule(sb, task, pathPrefix);
		}

		int i = 0;
		for(Alternative alt : rule.getLeft().getAlts()) {
			String altName = "alt_" + i;

			genModifyAlternative(sb, rule, alt, pathPrefix+rule.getLeft().getNameOfGraph()+"_", altName, isSubpattern);

			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altCasePatGraphVarName = pathPrefix+rule.getLeft().getNameOfGraph()+"_"+altName+"_"+altCasePattern.getNameOfGraph();
				genModify(sb, altCase, pathPrefix+rule.getLeft().getNameOfGraph()+"_"+altName+"_", altCasePatGraphVarName, isSubpattern);
			}
			++i;
		}
	}

	private void genModifyAlternative(StringBuffer sb, Rule rule, Alternative alt,
			String pathPrefix, String altName, boolean isSubpattern) {
		if(rule.getRight()!=null) { // generate code for dependent modify dispatcher
			genModifyAlternativeModify(sb, alt, pathPrefix, altName, rule.getReplParameters(), isSubpattern, true);
			genModifyAlternativeModify(sb, alt, pathPrefix, altName, rule.getReplParameters(), isSubpattern, false);
		}

		if(isSubpattern) { // generate for delete alternative dispatcher
			genModifyAlternativeDelete(sb, alt, pathPrefix, altName, isSubpattern);
		}
	}

	private void genModifyAlternativeModify(StringBuffer sb, Alternative alt, String pathPrefix, String altName,
			List<Entity> replParameters, boolean isSubpattern, boolean reuseNodesAndEdges) {
		// Emit function header
		sb.append("\n");
		sb.append("\t\tpublic void "
					  + pathPrefix+altName+"_" + (reuseNodesAndEdges ? "Modify" : "ModifyNoReuse")
					  + "(LGSPGraph graph, LGSPMatch curMatch");
		for(Entity entity : replParameters) {
			Node node = (Node)entity;
			sb.append(", LGSPNode " + formatEntity(node));
		}
		sb.append(")\n");
		sb.append("\t\t{\n");

		// Emit dispatcher calling the modify-method of the alternative case which was matched
		boolean firstCase = true;
		for(Rule altCase : alt.getAlternativeCases()) {
			if(firstCase) {
				sb.append("\t\t\tif(curMatch.patternGraph == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
				firstCase = false;
			}
			else
			{
				sb.append("\t\t\telse if(curMatch.patternGraph == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
			}
			sb.append("\t\t\t\t" + pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+"_"
					+ (reuseNodesAndEdges ? "Modify" : "ModifyNoReuse")
					+ "(graph, curMatch");
			for(Entity entity : replParameters) {
				Node node = (Node)entity;
				sb.append(", " + formatEntity(node));
			}
			sb.append(");\n");
			sb.append("\t\t\t\treturn;\n");
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new ApplicationException(); //debug assert\n");

		// Emit end of function
		sb.append("\t\t}\n");
	}

	private void genModifyAlternativeDelete(StringBuffer sb, Alternative alt, String pathPrefix, String altName,
			boolean isSubpattern) {
		// Emit function header
		sb.append("\n");
		sb.append("\t\tpublic void " + pathPrefix+altName+"_"+"Delete(LGSPGraph graph, LGSPMatch curMatch)\n");
		sb.append("\t\t{\n");

		// Emit dispatcher calling the delete-method of the alternative case which was matched
		boolean firstCase = true;
		for(Rule altCase : alt.getAlternativeCases()) {
			if(firstCase) {
				sb.append("\t\t\tif(curMatch.patternGraph == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
				firstCase = false;
			}
			else
			{
				sb.append("\t\t\telse if(curMatch.patternGraph == "
						+ pathPrefix+altName+"_" + altCase.getPattern().getNameOfGraph() + ") {\n");
			}
			sb.append("\t\t\t\t" + pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+"_"
					+ "Delete(graph, curMatch);\n");
			sb.append("\t\t\t\treturn;\n");
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new ApplicationException(); //debug assert\n");

		// Emit end of function
		sb.append("\t\t}\n");
	}

	private void genModifyRuleOrSubrule(StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		StringBuffer sb2 = new StringBuffer();
		StringBuffer sb3 = new StringBuffer();

		boolean useAddedElementNames = task.typeOfTask==TYPE_OF_TASK_CREATION ||
			(task.typeOfTask==TYPE_OF_TASK_MODIFY && task.left!=task.right);
		boolean createAddedElementNames = task.typeOfTask==TYPE_OF_TASK_CREATION ||
			(task.typeOfTask==TYPE_OF_TASK_MODIFY && task.left!=task.right && task.reuseNodesAndEdges);
		String prefix = (task.typeOfTask==TYPE_OF_TASK_CREATION ? "create_" : "")
			+ pathPrefix+task.left.getNameOfGraph()+"_";

		// Emit function header
		sb.append("\n");
		emitMethodHead(sb, task, pathPrefix);
		sb.append("\t\t{\n");

		// The resulting code has the following order:
		// (but this is not the order in which it is computed)
		//  - Extract nodes from match as LGSPNode instances
		//  - Extract nodes from match or from already extracted nodes as interface instances
		//  - Extract edges from match as LGSPEdge instances
		//  - Extract edges from match or from already extracted edges as interface instances
		//  - Extract subpattern submatches from match as LGSPMatch instances
		//  - Extract alternative submatches from match as LGSPMatch instances
		//  - Extract node types
		//  - Extract edge types
		//  - Create variables for used attributes of reusee
		//  - Create new nodes or reuse nodes
		//  - Call modification code of nested subpatterns
		//  - Call modification code of nested alternatives
		//  - Retype nodes
		//  - Create new edges or reuse edges
		//  - Retype edges
		//  - Create subpatterns
		//  - Attribute reevaluation
		//  - Create variables for used attributes of non-reusees needed for imperative statements and returns
		//  - Check deleted elements for retyping due to homomorphy
		//  - Remove edges
		//  - Remove nodes
		//  - Remove subpatterns
		//  - Emit
		//  - Check returned elements for deletion and retyping due to homomorphy
		//  - Return

		ModifyGenerationState state = new ModifyGenerationState();
		ModifyGenerationStateConst stateConst = state;

		collectCommonElements(task, state.commonNodes, state.commonEdges, state.commonSubpatternUsages);

		collectNewElements(task, stateConst, state.newNodes, state.newEdges, state.newSubpatternUsages);

		collectDeletedElements(task, stateConst, state.delNodes, state.delEdges, state.delSubpatternUsages);

		collectNewOrRetypedElements(task, state, state.newOrRetypedNodes, state.newOrRetypedEdges);

		collectElementsAccessedByInterface(task, state.accessViaInterface);

		collectElementsAndAttributesNeededByImperativeStatements(task, state.neededAttributes,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.nodesNeededAsAttributes, state.edgesNeededAsAttributes,
				state.neededVariables);

		collectElementsAndAttributesNeededByReturns(task, state.neededAttributes,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.nodesNeededAsAttributes, state.edgesNeededAsAttributes,
				state.neededVariables);

		// Copy all entries generated by collectNeededAttributes for imperative statements and returns
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : state.neededAttributes.entrySet()) {
			HashSet<Entity> neededAttrs = entry.getValue();
			HashSet<Entity> attributesStoredBeforeDelete = state.attributesStoredBeforeDelete.get(entry.getKey());
			if(attributesStoredBeforeDelete == null) {
				state.attributesStoredBeforeDelete.put(entry.getKey(),
						attributesStoredBeforeDelete = new LinkedHashSet<Entity>());
			}
			attributesStoredBeforeDelete.addAll(neededAttrs);
		}

		collectElementsAndAttributesNeededByEvals(task, state.neededAttributes,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.nodesNeededAsAttributes, state.edgesNeededAsAttributes,
				state.neededVariables);

		genNewNodes(sb2, task.reuseNodesAndEdges, stateConst, useAddedElementNames, prefix,
				state.nodesNeededAsElements, state.nodesNeededAsTypes);

		genSubpatternModificationCalls(sb2, task, pathPrefix, state.nodesNeededAsElements);

		genAlternativeModificationCalls(sb2, task, pathPrefix);

		genTypeChangesNodes(sb2, stateConst, task,
				state.nodesNeededAsElements, state.nodesNeededAsTypes);

		genNewEdges(sb2, stateConst, task, useAddedElementNames, prefix,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.edgesNeededAsTypes, state.reusedElements);

		genTypeChangesEdges(sb2, task, stateConst, state.edgesNeededAsElements, state.edgesNeededAsTypes);

		genNewSubpatternCalls(sb2, stateConst);

		genEvals(sb3, stateConst, task.evals);

		genVariablesForUsedAttributesBeforeDelete(sb3, stateConst, state.forceAttributeToVar);

		genCheckDeletedElementsForRetypingThroughHomomorphy(sb3, stateConst);

		genDelEdges(sb3, stateConst, state.edgesNeededAsElements);

		genDelNodes(sb3, stateConst, state.nodesNeededAsElements);

		genDelSubpatternCalls(sb3, stateConst);

		genImperativeStatements(sb3, stateConst, task);

		genCheckReturnedElementsForDeletionOrRetypingDueToHomomorphy(sb3, task);

		// Emit return (only if top-level rule)
		if(pathPrefix=="" && !task.isSubpattern)
			emitReturnStatement(sb3, stateConst, task.returns);

		// Emit end of function
		sb3.append("\t\t}\n");

		removeAgainFromNeededWhatIsNotReallyNeeded(task, stateConst,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.nodesNeededAsAttributes, state.edgesNeededAsAttributes);

		////////////////////////////////////////////////////////////////////////////////
		// Finalize method using the infos collected and the already generated code

		genExtractElementsFromMatch(sb, stateConst, pathPrefix, task.left.getNameOfGraph());

		genExtractVariablesFromMatch(sb, stateConst, pathPrefix, task.left.getNameOfGraph());

		genExtractSubmatchesFromMatch(sb, pathPrefix, task.left);

		genNeededTypes(sb, stateConst);

		genCreateVariablesForUsedAttributesOfReusedElements(sb, stateConst);

		// New nodes/edges (re-use), retype nodes/edges, call modification code
		sb.append(sb2);

		// Attribute re-calc, attr vars for emit, remove, emit, return
		sb.append(sb3);

		// ----------

		if(createAddedElementNames) {
			genAddedGraphElementsArray(sb, stateConst, prefix, task.typeOfTask);
		}
	}

	private void emitMethodHead(StringBuffer sb, ModifyGenerationTask task, String pathPrefix)
	{
		String noReuse = task.reuseNodesAndEdges ? "" : "NoReuse";
		switch(task.typeOfTask) {
		case TYPE_OF_TASK_MODIFY:
			if(pathPrefix=="" && !task.isSubpattern) {
				sb.append("\t\tpublic override object[] Modify"+noReuse+"(LGSPGraph graph, LGSPMatch curMatch)\n");
			} else {
				sb.append("\t\tpublic void "
						+ pathPrefix+task.left.getNameOfGraph()+"_Modify"+noReuse+"(LGSPGraph graph, LGSPMatch curMatch");
				for(Entity entity : task.replParameters) {
					Node node = (Node)entity;
					sb.append(", LGSPNode " + formatEntity(node));
				}
				sb.append(")\n");
			}
			break;
		case TYPE_OF_TASK_CREATION:
			sb.append("\t\tpublic void "
					+ pathPrefix+task.left.getNameOfGraph()+"_Create(LGSPGraph graph");
			for(Entity entity : task.parameters) {
				sb.append((entity instanceof Node ? ", LGSPNode " : ", LGSPEdge " )+ formatEntity(entity));
			}
			sb.append(")\n");
			break;
		case TYPE_OF_TASK_DELETION:
			sb.append("\t\tpublic void "
					+ pathPrefix+task.left.getNameOfGraph()+"_Delete(LGSPGraph graph, LGSPMatch curMatch)\n");
			break;
		default:
			assert false;
		}
	}

	private void collectNewOrRetypedElements(ModifyGenerationTask task,	ModifyGenerationStateConst state,
			HashSet<Node> newOrRetypedNodes, HashSet<Edge> newOrRetypedEdges)
	{
		newOrRetypedNodes.addAll(state.newNodes());
		for(Node node : task.right.getNodes()) {
			if(node.changesType())
				newOrRetypedNodes.add(node.getRetypedNode());
		}
		newOrRetypedEdges.addAll(state.newEdges());
		for(Edge edge : task.right.getEdges()) {
			if(edge.changesType())
				newOrRetypedEdges.add(edge.getRetypedEdge());
		}
	}

	private void removeAgainFromNeededWhatIsNotReallyNeeded(
			ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes)
	{
		// nodes/edges needed from match, but not the new nodes
		nodesNeededAsElements.removeAll(state.newNodes());
		nodesNeededAsAttributes.removeAll(state.newNodes());
		edgesNeededAsElements.removeAll(state.newEdges());
		edgesNeededAsAttributes.removeAll(state.newEdges());

		// nodes/edges handed in as subpattern connections to create are already available as method parameters
		if(task.typeOfTask==TYPE_OF_TASK_CREATION) {
			nodesNeededAsElements.removeAll(task.parameters);
			//nodesNeededAsAttributes.removeAll(state.newNodes);
			edgesNeededAsElements.removeAll(task.parameters);
			//edgesNeededAsAttributes.removeAll(state.newEdges);
		}

		// nodes handed in as replacement connections to modify are already available as method parameters
		if(task.typeOfTask==TYPE_OF_TASK_MODIFY) {
			nodesNeededAsElements.removeAll(task.replParameters);
			//nodesNeededAsAttributes.removeAll(state.newNodes);
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
		for(SubpatternDependentReplacement subRepl : task.right.getSubpatternDependentReplacements()) {
			delSubpatternUsages.remove(subRepl.getSubpatternUsage());
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

		// and which are not in the replacement parameters of a subpattern
		if(task.isSubpattern) {
			for(Entity entity : task.replParameters) {
				Node node = (Node)entity;
				newNodes.remove(node);
			}
		}
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

		// Elements from outer pattern are not allowed to be modified by inner alternative case pattern
		for(Node node : task.left.getNodes()) {
			if(node.getPointOfDefinition()!=task.left) {
				commonNodes.add(node);
			}
		}
		for(Edge edge : task.left.getEdges()) {
			if(edge.getPointOfDefinition()!=task.left) {
				commonEdges.add(edge);
			}
		}

		// Parameters/connections are not allowed to be modified by subpatterns
		if(task.isSubpattern) {
			for(Entity entity : task.parameters) {
				if(entity instanceof Node) {
					commonNodes.add((Node)entity);
				}
				else {
					commonEdges.add((Edge)entity);
				}
			}
		}
	}

	private void collectElementsAccessedByInterface(ModifyGenerationTask task,
			HashSet<GraphEntity> accessViaInterface)
	{
		accessViaInterface.addAll(task.left.getNodes());
		accessViaInterface.addAll(task.left.getEdges());
		for(Node node : task.right.getNodes()) {
			if(node.inheritsType())
				accessViaInterface.add(node);
			else if(node.changesType())
				accessViaInterface.add(node.getRetypedEntity());
		}
		for(Edge edge : task.right.getEdges()) {
			if(edge.inheritsType())
				accessViaInterface.add(edge);
			else if(edge.changesType())
				accessViaInterface.add(edge.getRetypedEntity());
		}
	}

	private void collectElementsAndAttributesNeededByImperativeStatements(ModifyGenerationTask task,
			HashMap<GraphEntity, HashSet<Entity>> neededAttributes,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
			HashSet<Variable> neededVariables)
	{
		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit) istmt;
				for(Expression arg : emit.getArguments())
					collectNeededAttributes(arg, neededAttributes,
							nodesNeededAsElements, edgesNeededAsElements,
							nodesNeededAsAttributes, edgesNeededAsAttributes, neededVariables);
			}
			else if (istmt instanceof Exec) {
				Exec exec = (Exec) istmt;
				for(Entity param : exec.getArguments()) {
					if(param instanceof Node)
						nodesNeededAsElements.add((Node) param);
					else if(param instanceof Edge)
						edgesNeededAsElements.add((Edge) param);
					else if(param instanceof Variable) {
						if(neededVariables != null)
							neededVariables.add((Variable) param);
					}
					else
						assert false : "XGRS argument of unknown type: " + param.getClass();
				}
			}
			else assert false : "unknown ImperativeStmt: " + istmt + " in " + task.left.getNameOfGraph();
		}
	}

	private void collectElementsAndAttributesNeededByEvals(ModifyGenerationTask task,
			HashMap<GraphEntity, HashSet<Entity>> neededAttributes,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
			HashSet<Variable> neededVariables)
	{
		for(Assignment ass : task.evals) {
			Expression target = ass.getTarget();
			Entity entity;

			if(target instanceof Qualification)
				entity = ((Qualification) target).getOwner();
			else if(target instanceof Visited) {
				Visited visTgt = (Visited) target;
				entity = visTgt.getEntity();
				collectNeededAttributes(visTgt.getVisitorID(), neededAttributes,
						nodesNeededAsElements, edgesNeededAsElements,
						nodesNeededAsAttributes, edgesNeededAsAttributes, neededVariables);
			}
			else
				throw new UnsupportedOperationException("Unsupported assignment target (" + target + ")");

			if(entity instanceof Node)
				nodesNeededAsElements.add((Node) entity);
			else if(entity instanceof Edge)
				edgesNeededAsElements.add((Edge) entity);
			else
				throw new UnsupportedOperationException("Unsupported entity (" + entity + ")");

			collectNeededAttributes(target, neededAttributes,
					nodesNeededAsElements, edgesNeededAsElements,
					nodesNeededAsAttributes, edgesNeededAsAttributes, null);
			collectNeededAttributes(ass.getExpression(), neededAttributes,
					nodesNeededAsElements, edgesNeededAsElements,
					nodesNeededAsAttributes, edgesNeededAsAttributes, neededVariables);
		}
	}

	private void collectElementsAndAttributesNeededByReturns(ModifyGenerationTask task,
			HashMap<GraphEntity, HashSet<Entity>> neededAttributes,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
			HashSet<Variable> neededVariables)
	{
		for(Expression expr : task.returns) {
			if(expr instanceof GraphEntityExpression) {
				GraphEntity entity = ((GraphEntityExpression) expr).getGraphEntity();
				if(entity instanceof Node)
					nodesNeededAsElements.add((Node) entity);
				else if(entity instanceof Edge)
					edgesNeededAsElements.add((Edge) entity);
				else
					throw new UnsupportedOperationException("Unsupported entity (" + entity + ")");
			}
			else
				collectNeededAttributes(expr, neededAttributes,
						nodesNeededAsElements, edgesNeededAsElements,
						nodesNeededAsAttributes, edgesNeededAsAttributes, neededVariables);
		}
	}

	private void genCreateVariablesForUsedAttributesOfReusedElements(StringBuffer sb, ModifyGenerationStateConst state)
	{
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : state.neededAttributes().entrySet()) {
			if(!state.reusedElements().contains(entry.getKey())) continue;

			String grEntName = formatEntity(entry.getKey());
			for(Entity entity : entry.getValue()) {
				String attrName = formatIdentifiable(entity);
				genVariable(sb, grEntName, entity);
				sb.append(" = i" + grEntName + ".@" + attrName + ";\n");
			}
		}
	}

	private void genNeededTypes(StringBuffer sb, ModifyGenerationStateConst state)
	{
		for(Node node : state.nodesNeededAsTypes()) {
			String name = formatEntity(node);
			sb.append("\t\t\tNodeType " + name + "_type = " + name + ".type;\n");
		}
		for(Edge edge : state.edgesNeededAsTypes()) {
			String name = formatEntity(edge);
			sb.append("\t\t\tEdgeType " + name + "_type = " + name + ".type;\n");
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

	private void genImperativeStatements(StringBuffer sb, ModifyGenerationStateConst state, ModifyGenerationTask task)
	{
		int xgrsID = 0;
		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			if(istmt instanceof Emit) {
				Emit emit =(Emit) istmt;
				for(Expression arg : emit.getArguments()) {
					sb.append("\t\t\tgraph.EmitWriter.Write(");
					genExpression(sb, arg, state);
					sb.append(");\n");
				}
			} else if (istmt instanceof Exec) {
				Exec exec = (Exec) istmt;
				sb.append("\t\t\tApplyXGRS_" + xgrsID++ + "(graph");
				for(Entity param : exec.getArguments()) {
					sb.append(", ");
					sb.append(formatEntity(param));
				}
				sb.append(");\n");
			} else assert false : "unknown ImperativeStmt: " + istmt + " in " + task.left.getNameOfGraph();
		}
	}

	private void genVariablesForUsedAttributesBeforeDelete(StringBuffer sb,
			ModifyGenerationStateConst state, HashMap<GraphEntity, HashSet<Entity>> forceAttributeToVar)
	{
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : state.attributesStoredBeforeDelete().entrySet()) {
			GraphEntity owner = entry.getKey();
			if(state.reusedElements().contains(owner)) continue;

			String grEntName = formatEntity(owner);
			for(Entity entity : entry.getValue()) {
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

	private void genDelNodes(StringBuffer sb, ModifyGenerationStateConst state, HashSet<Node> nodesNeededAsElements)
	{
		for(Node node : state.delNodes()) {
			nodesNeededAsElements.add(node);
			sb.append("\t\t\tgraph.RemoveEdges(" + formatEntity(node) + ");\n");
			sb.append("\t\t\tgraph.Remove(" + formatEntity(node) + ");\n");
		}
	}

	private void genDelEdges(StringBuffer sb, ModifyGenerationStateConst state, HashSet<Edge> edgesNeededAsElements)
	{
		for(Edge edge : state.delEdges()) {
			if(state.reusedElements().contains(edge)) continue;
			edgesNeededAsElements.add(edge);
			sb.append("\t\t\tgraph.Remove(" + formatEntity(edge) + ");\n");
		}
	}

	private void genTypeChangesEdges(StringBuffer sb, ModifyGenerationTask task, ModifyGenerationStateConst state,
			HashSet<Edge> edgesNeededAsElements, HashSet<Edge> edgesNeededAsTypes)
	{
		for(Edge edge : task.right.getEdges()) {
			if(!edge.changesType()) continue;
			String new_type;
			RetypedEdge redge = edge.getRetypedEdge();

			if(redge.inheritsType()) {
				Edge typeofElem = (Edge) getConcreteTypeofElem(redge);
				new_type = formatEntity(typeofElem) + "_type";
				edgesNeededAsElements.add(typeofElem);
				edgesNeededAsTypes.add(typeofElem);
			} else {
				new_type = formatTypeClass(redge.getType()) + ".typeVar";
			}

			edgesNeededAsElements.add(edge);
			sb.append("\t\t\tLGSPEdge " + formatEntity(redge) + " = graph.Retype("
						   + formatEntity(edge) + ", " + new_type + ");\n");
			if(state.edgesNeededAsAttributes().contains(redge) && state.accessViaInterface().contains(redge)) {
				sb.append("\t\t\t" + formatVarDeclWithCast(redge.getType(), "I", "i" + formatEntity(redge))
							   + formatEntity(redge) + ";\n");
			}
		}
	}

	private void genTypeChangesNodes(StringBuffer sb, ModifyGenerationStateConst state, ModifyGenerationTask task,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes)
	{
		for(Node node : task.right.getNodes()) {
			if(!node.changesType()) continue;
			String new_type;
			RetypedNode rnode = node.getRetypedNode();

			if(rnode.inheritsType()) {
				Node typeofElem = (Node) getConcreteTypeofElem(rnode);
				new_type = formatEntity(typeofElem) + "_type";
				nodesNeededAsElements.add(typeofElem);
				nodesNeededAsTypes.add(typeofElem);
			} else {
				new_type = formatTypeClass(rnode.getType()) + ".typeVar";
			}

			nodesNeededAsElements.add(node);
			sb.append("\t\t\tLGSPNode " + formatEntity(rnode) + " = graph.Retype("
						   + formatEntity(node) + ", " + new_type + ");\n");
			if(state.nodesNeededAsAttributes().contains(rnode) && state.accessViaInterface().contains(rnode)) {
				sb.append("\t\t\t" + formatVarDeclWithCast(rnode.getType(), "I", "i" + formatEntity(rnode))
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
			for(int i = 0; i < task.left.getAlts().size(); i++) {
				String altName = "alt_" + i;
				sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+altName+"_" +
						(task.reuseNodesAndEdges ? "Modify" : "ModifyNoReuse") + "(graph, alternative_" + altName);
				for(Entity entity : task.replParameters) {
					Node node = (Node)entity;
					sb.append(", " + formatEntity(node));
				}
				sb.append(");\n");
			}
		}
		else if(task.typeOfTask==TYPE_OF_TASK_DELETION) {
			// generate calls to the deletion of the alternatives (nested alternatives are handled in their enclosing alternative)
			for(int i = 0; i < task.left.getAlts().size(); i++) {
				String altName = "alt_" + i;
				sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+altName+"_" +
						"Delete" + "(graph, alternative_"+altName+");\n");
			}
		}
	}

	private void genSubpatternModificationCalls(StringBuffer sb, ModifyGenerationTask task, String pathPrefix,
			HashSet<Node> nodesNeededAsElements) {
		if(task.right==task.left) { // test needs top-level-modify due to interface, but not more
			return;
		}

		// generate calls to the dependent modifications of the subpatterns
		for(SubpatternDependentReplacement subRep : task.right.getSubpatternDependentReplacements()) {
			String subName = formatIdentifiable(subRep);
			sb.append("\t\t\tPattern_" + formatIdentifiable(subRep.getSubpatternUsage().getSubpatternAction())
					+ ".Instance." + formatIdentifiable(subRep.getSubpatternUsage().getSubpatternAction()) +
					"_Modify(graph, subpattern_" + subName);
			for(Entity entity : subRep.getReplConnections()) {
				Node node = (Node)entity;
				sb.append(", " + formatEntity(node));
				nodesNeededAsElements.add(node);
			}
			sb.append(");\n");
		}
	}

	private void genAddedGraphElementsArray(StringBuffer sb, String pathPrefix, boolean isNode, Collection<? extends GraphEntity> set) {
		String NodesOrEdges = isNode?"Node":"Edge";
		sb.append("\t\tprivate static String[] " + pathPrefix + "added" + NodesOrEdges + "Names = new String[] ");
		genSet(sb, set, "\"", "\"", true);
		sb.append(";\n");
	}

	private void emitReturnStatement(StringBuffer sb, ModifyGenerationStateConst state, List<Expression> returns) {
		if(returns.isEmpty())
			sb.append("\t\t\treturn EmptyReturnElements;\n");
		else {
			sb.append("\t\t\treturn new object[] { ");
			for(Expression expr : returns)
			{
				genExpression(sb, expr, state);
				sb.append(", ");
			}
			sb.append("};\n");
		}
	}

	private void genExtractElementsFromMatch(StringBuffer sb, ModifyGenerationStateConst state,
			String pathPrefix, String patternName) {
		for(Node node : state.nodesNeededAsElements()) {
			if(node.isRetyped()) continue;
			sb.append("\t\t\tLGSPNode " + formatEntity(node)
					+ " = curMatch.Nodes[(int)" + pathPrefix+patternName + "_NodeNums.@"
					+ formatIdentifiable(node) + "];\n");
		}
		for(Node node : state.nodesNeededAsAttributes()) {
			if(node.isRetyped()) continue;
			sb.append("\t\t\t" + formatVarDeclWithCast(node.getType(), "I", "i" + formatEntity(node)));
			if(state.nodesNeededAsElements().contains(node))
				sb.append(formatEntity(node) + ";\n");
			else
				sb.append("curMatch.Nodes[(int)" + pathPrefix+patternName + "_NodeNums.@"
						+ formatIdentifiable(node) + "];\n");
		}
		for(Edge edge : state.edgesNeededAsElements()) {
			if(edge.isRetyped()) continue;
			sb.append("\t\t\tLGSPEdge " + formatEntity(edge)
					+ " = curMatch.Edges[(int)" + pathPrefix+patternName + "_EdgeNums.@"
					+ formatIdentifiable(edge) + "];\n");
		}
		for(Edge edge : state.edgesNeededAsAttributes()) {
			if(edge.isRetyped()) continue;
			sb.append("\t\t\t" + formatVarDeclWithCast(edge.getType(), "I", "i" + formatEntity(edge)));
			if(state.edgesNeededAsElements().contains(edge))
				sb.append(formatEntity(edge) + ";\n");
			else
				sb.append("curMatch.Edges[(int)" + pathPrefix+patternName + "_EdgeNums.@"
						+ formatIdentifiable(edge) + "];\n");
		}
	}

	private void genExtractVariablesFromMatch(StringBuffer sb, ModifyGenerationStateConst state,
			String pathPrefix, String patternName) {
		for(Variable var : state.neededVariables()) {
			String type = formatAttributeType(var);
			sb.append("\t\t\t" + type + " " + formatEntity(var) + " = ("
					+ type + ") curMatch.Variables[(int)" + pathPrefix + patternName
					+ "_VariableNums.@" + formatIdentifiable(var) + "];\n");
		}
	}

	private void genExtractSubmatchesFromMatch(StringBuffer sb, String pathPrefix, PatternGraph pattern) {
		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			String subName = formatIdentifiable(sub);
			sb.append("\t\t\tLGSPMatch subpattern_" + subName
					+ " = curMatch.EmbeddedGraphs[(int)" + pathPrefix+pattern.getNameOfGraph() + "_SubNums.@"
					+ formatIdentifiable(sub) + "];\n");
		}
		for(int i = 0; i < pattern.getAlts().size(); i++) {
			String altName = "alt_" + i;
			sb.append("\t\t\tLGSPMatch alternative_" + altName
					+ " = curMatch.EmbeddedGraphs[(int)" + pathPrefix+pattern.getNameOfGraph() + "_AltNums.@"
					+ altName + " + " + pattern.getSubpatternUsages().size() + "];\n");
		}
	}

	/**
	 * Scans an expression for all read attributes and collects
	 * them in the neededAttributes hash map and their owners in
	 * the appropriate <kind>NeededAsAttributes hash set.
	 * @param neededVariables This may be null, if not used.
	 */
	private void collectNeededAttributes(Expression expr, HashMap<GraphEntity, HashSet<Entity>> neededAttributes,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Node> nodesNeededAsAttributes, HashSet<Edge> edgesNeededAsAttributes,
			HashSet<Variable> neededVariables) {
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			for(int i = 0; i < op.arity(); i++)
				collectNeededAttributes(op.getOperand(i), neededAttributes,
						nodesNeededAsElements, edgesNeededAsElements,
						nodesNeededAsAttributes, edgesNeededAsAttributes, neededVariables);
		}
		else if(expr instanceof Qualification) {
			Qualification qual = (Qualification) expr;
			GraphEntity entity = (GraphEntity) qual.getOwner();
			HashSet<Entity> neededAttrs = neededAttributes.get(entity);
			if(neededAttrs == null) {
				neededAttributes.put(entity, neededAttrs = new LinkedHashSet<Entity>());
			}
			neededAttrs.add(qual.getMember());
			if(entity instanceof Node)
				nodesNeededAsAttributes.add((Node) entity);
			else if(entity instanceof Edge)
				edgesNeededAsAttributes.add((Edge) entity);
			else
				throw new UnsupportedOperationException("Unsupported entity (" + entity + ")");
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			collectNeededAttributes(cast.getExpression(), neededAttributes,
					nodesNeededAsElements, edgesNeededAsElements,
					nodesNeededAsAttributes, edgesNeededAsAttributes, neededVariables);
		}
		else if(expr instanceof VariableExpression) {
			if(neededVariables != null)
				neededVariables.add(((VariableExpression) expr).getVariable());
		}
		else if(expr instanceof Visited) {
			Visited vis = (Visited) expr;
			GraphEntity entity = (GraphEntity) vis.getEntity();
			if(entity instanceof Node)
				nodesNeededAsElements.add((Node) entity);
			else if(entity instanceof Edge)
				edgesNeededAsElements.add((Edge) entity);
			else
				throw new UnsupportedOperationException("Unsupported entity (" + entity + ")");
			collectNeededAttributes(vis.getVisitorID(), neededAttributes,
					nodesNeededAsElements, edgesNeededAsElements,
					nodesNeededAsAttributes, edgesNeededAsAttributes, neededVariables);
		}
	}

	////////////////////////////
	// New element generation //
	////////////////////////////

	private void genNewNodes(StringBuffer sb2, boolean reuseNodeAndEdges, ModifyGenerationStateConst state,
			boolean useAddedElementNames, String pathPrefix,
			HashSet<Node> nodesNeededAsElements, HashSet<Node> nodesNeededAsTypes) {
		// call nodes added delegate
		if(useAddedElementNames) sb2.append("\t\t\tgraph.SettingAddedNodeNames( " + pathPrefix + "addedNodeNames );\n");

		//reuseNodeAndEdges = false;							// TODO: reimplement this!!

		LinkedList<Node> tmpNewNodes = new LinkedList<Node>(state.newNodes());

		/* LinkedList<Node> tmpDelNodes = new LinkedList<Node>(delNodes);
		 if(reuseNodeAndEdges) {
		 NN: for(Iterator<Node> i = tmpNewNodes.iterator(); i.hasNext();) {
		 Node node = i.next();
		 // Can we reuse the node
		 for(Iterator<Node> j = tmpDelNodes.iterator(); j.hasNext();) {
		 Node delNode = j.next();
		 if(delNode.getNodeType() == node.getNodeType()) {
		 sb2.append("\t\t\tLGSPNode " + formatEntity(node) + " = " + formatEntity(delNode) + ";\n");
		 sb2.append("\t\t\tgraph.ReuseNode(" + formatEntity(delNode) + ", null);\n");
		 delNodes.remove(delNode);
		 j.remove();
		 i.remove();
		 nodesNeededAsElements.add(delNode);
		 reusedElements.add(delNode);
		 continue NN;
		 }
		 }
		 }
		 NN: for(Iterator<Node> i = tmpNewNodes.iterator(); i.hasNext();) {
		 Node node = i.next();
		 // Can we reuse the node
		 for(Iterator<Node> j = tmpDelNodes.iterator(); j.hasNext();) {
		 Node delNode = j.next();
		 if(!delNode.getNodeType().getAllMembers().isEmpty()) {
		 String type = computeGraphEntityType(node);
		 sb2.append("\t\t\tLGSPNode " + formatEntity(node) + " = " + formatEntity(delNode) + ";\n");
		 sb2.append("\t\t\tgraph.ReuseNode(" + formatEntity(delNode) + ", " + type + ");\n");
		 delNodes.remove(delNode);
		 j.remove();
		 i.remove();
		 nodesNeededAsElements.add(delNode);
		 reusedElements.add(delNode);
		 continue NN;
		 }
		 }
		 }
		 }
		 NN:*/ for(Node node : tmpNewNodes) {
			/*String type = computeGraphEntityType(node);
			 // Can we reuse the node
			 if(reuseNodeAndEdges && !tmpDelNodes.isEmpty()) {
			 Node delNode = tmpDelNodes.getFirst();
			 sb2.append("\t\t\tLGSPNode " + formatEntity(node) + " = " + formatEntity(delNode) + ";\n");
			 sb2.append("\t\t\tgraph.ReuseNode(" + formatEntity(delNode) + ", " + type + ");\n");
			 delNodes.remove(delNode);
			 tmpDelNodes.removeFirst();
			 i.remove();
			 nodesNeededAsElements.add(delNode);
			 reusedElements.add(delNode);
			 continue NN;
			 }*/
			if(node.inheritsType()) {
				Node typeofElem = (Node) getConcreteTypeofElem(node);
				nodesNeededAsElements.add(typeofElem);
				nodesNeededAsTypes.add(typeofElem);
				sb2.append("\t\t\tLGSPNode " + formatEntity(node) + " = (LGSPNode) "
							   + formatEntity(typeofElem) + "_type.CreateNode();\n"
							   + "\t\t\tgraph.AddNode(" + formatEntity(node) + ");\n");
				if(state.nodesNeededAsAttributes().contains(node) && state.accessViaInterface().contains(node)) {
					sb2.append("\t\t\t" + formatVarDeclWithCast(node.getType(), "I", "i" + formatEntity(node))
								   + formatEntity(node) + ";\n");
				}
			}
			else {
				String etype = "@" + formatElementClass(node.getType());
				sb2.append("\t\t\t" + etype + " " + formatEntity(node) + " = " + etype + ".CreateNode(graph);\n");
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

	// TODO use or remove it
	/*private String computeGraphEntityType(Node node) {
	 String type;
	 if(node.inheritsType()) {
	 Node typeofElem = (Node) getConcreteTypeof(node);
	 type = formatEntity(typeofElem) + "_type";
	 nodesNeededAsElements.add(typeofElem);
	 nodesNeededAsTypes.add(typeofElem);
	 } else {
	 type = formatTypeClass(node.getType()) + ".typeVar";
	 }
	 return type;
	 }*/

	private void genNewEdges(StringBuffer sb2, ModifyGenerationStateConst state, ModifyGenerationTask task,
			boolean useAddedElementNames, String pathPrefix,
			HashSet<Node> nodesNeededAsElements, HashSet<Edge> edgesNeededAsElements,
			HashSet<Edge> edgesNeededAsTypes, HashSet<GraphEntity> reusedElements)
	{
		// call edges added delegate
		if(useAddedElementNames) sb2.append("\t\t\tgraph.SettingAddedEdgeNames( " + pathPrefix + "addedEdgeNames );\n");

		NE:	for(Edge edge : state.newEdges()) {
			String etype = "@" + formatElementClass(edge.getType());

			Node src_node = task.right.getSource(edge);
			Node tgt_node = task.right.getTarget(edge);
			if(src_node==null || tgt_node==null) {
				return; // don't create dangling edges    - todo: what's the correct way to handle them?
			}

			if(src_node.changesType()) src_node = src_node.getRetypedNode();
			if(tgt_node.changesType()) tgt_node = tgt_node.getRetypedNode();

			if(state.commonNodes().contains(src_node))
				nodesNeededAsElements.add(src_node);

			if(state.commonNodes().contains(tgt_node))
				nodesNeededAsElements.add(tgt_node);

			if(edge.inheritsType()) {
				Edge typeofElem = (Edge) getConcreteTypeofElem(edge);
				edgesNeededAsElements.add(typeofElem);
				edgesNeededAsTypes.add(typeofElem);

				sb2.append("\t\t\tLGSPEdge " + formatEntity(edge) + " = (LGSPEdge) "
							   + formatEntity(typeofElem) + "_type.CreateEdge("
							   + formatEntity(src_node) + ", " + formatEntity(tgt_node) + ");\n"
							   + "\t\t\tgraph.AddEdge(" + formatEntity(edge) + ");\n");
				if(state.edgesNeededAsAttributes().contains(edge) && state.accessViaInterface().contains(edge)) {
					sb2.append("\t\t\t" + formatVarDeclWithCast(edge.getType(), "I", "i" + formatEntity(edge))
								   + formatEntity(edge) + ";\n");
				}
				continue;
			}
			else if(task.reuseNodesAndEdges) {
				Edge bestDelEdge = null;
				int bestPoints = -1;

				// Can we reuse a deleted edge of the same type?
				for(Edge delEdge : state.delEdges()) {
					if(reusedElements.contains(delEdge)) continue;
					if(delEdge.getType() != edge.getType()) continue;

					int curPoints = 0;
					if(task.left.getSource(delEdge) == src_node)
						curPoints++;
					if(task.left.getTarget(delEdge) == tgt_node)
						curPoints++;
					if(curPoints > bestPoints) {
						bestPoints = curPoints;
						bestDelEdge = delEdge;
						if(curPoints == 2) break;
					}
				}

				if(bestDelEdge != null) {
					// We may be able to reuse the edge instead of deleting it,
					// if the runtime type of the reused edge has the exact type.
					// This is a veritable performance optimization, as object creation is costly.

					String newEdgeName = formatEntity(edge);
					String reusedEdgeName = formatEntity(bestDelEdge);
					String src = formatEntity(src_node);
					String tgt = formatEntity(tgt_node);

					sb2.append("\t\t\t" + etype + " " + newEdgeName + ";\n"
								   + "\t\t\tif(" + reusedEdgeName + ".type == "
								   + formatTypeClass(edge.getType()) + ".typeVar)\n"
								   + "\t\t\t{\n"
								   + "\t\t\t\t// re-using " + reusedEdgeName + " as " + newEdgeName + "\n"
								   + "\t\t\t\t" + newEdgeName + " = (" + etype + ") " + reusedEdgeName + ";\n"
								   + "\t\t\t\tgraph.ReuseEdge(" + reusedEdgeName + ", ");

					if(task.left.getSource(bestDelEdge) != src_node)
						sb2.append(src + ", ");
					else
						sb2.append("null, ");

					if(task.left.getTarget(bestDelEdge) != tgt_node)
						sb2.append(tgt + "");
					else
						sb2.append("null");

					// If the runtime type does not match, delete the edge and create a new one
					sb2.append(");\n"
								   + "\t\t\t}\n"
								   + "\t\t\telse\n"
								   + "\t\t\t{\n"
								   + "\t\t\t\tgraph.Remove(" + reusedEdgeName + ");\n"
								   + "\t\t\t\t" + newEdgeName + " = " + etype
								   + ".CreateEdge(graph, " + src + ", " + tgt + ");\n"
								   + "\t\t\t}\n");

					edgesNeededAsElements.add(bestDelEdge);
					reusedElements.add(bestDelEdge);
					continue NE;
				}
			}

			// Create the edge
			sb2.append("\t\t\t" + etype + " " + formatEntity(edge) + " = " + etype
						   + ".CreateEdge(graph, " + formatEntity(src_node)
						   + ", " + formatEntity(tgt_node) + ");\n");
		}
	}

	private void genNewSubpatternCalls(StringBuffer sb, ModifyGenerationStateConst state)
	{
		for(SubpatternUsage subUsage : state.newSubpatternUsages()) {
			sb.append("\t\t\tPattern_" + formatIdentifiable(subUsage.getSubpatternAction())
					+ ".Instance." + formatIdentifiable(subUsage.getSubpatternAction()) +
					"_Create(graph");
			for(Entity entity : subUsage.getSubpatternConnections()) {
				sb.append(", " + formatEntity(entity));
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
					"_Delete(graph, subpattern_" + subName + ");\n");
		}
	}

	//////////////////////////
	// Eval part generation //
	//////////////////////////

	private void genEvals(StringBuffer sb, ModifyGenerationStateConst state, Collection<Assignment> assignments) {
		boolean def_b = false, def_i = false, def_s = false, def_f = false, def_d = false, def_o = false;
		for(Assignment ass : assignments) {
			Expression target = ass.getTarget();

			if(target instanceof Qualification) {
				Qualification qualTgt = (Qualification) target;
				Entity entity = qualTgt.getOwner();

				String varName, varType;
				switch(ass.getTarget().getType().classify()) {
					case Type.IS_BOOLEAN:
						varName = "tempvar_b";
						varType = def_b?"":"bool ";
						def_b = true;
						break;
					case Type.IS_INTEGER:
						varName = "tempvar_i";
						varType = def_i?"":"int ";
						def_i = true;
						break;
					case Type.IS_FLOAT:
						varName = "tempvar_f";
						varType = def_f?"":"float ";
						def_f = true;
						break;
					case Type.IS_DOUBLE:
						varName = "tempvar_d";
						varType = def_d?"":"double ";
						def_d = true;
						break;
					case Type.IS_STRING:
						varName = "tempvar_s";
						varType = def_s?"":"String ";
						def_s = true;
						break;
					case Type.IS_OBJECT:
						varName = "tempvar_o";
						varType = def_o?"":"Object ";
						def_o = true;
						break;
					default:
						throw new IllegalArgumentException();
				}

				sb.append("\t\t\t" + varType + varName + " = ");
				if(ass.getTarget().getType() instanceof EnumType)
					sb.append("(int) ");
				genExpression(sb, ass.getExpression(), state);
				sb.append(";\n");

				String kindStr = null;
				boolean isDeletedElem = false;
				if(entity instanceof Node) {
					kindStr = "Node";
					isDeletedElem = state.delNodes().contains(entity);
				}
				else if(entity instanceof Edge) {
					kindStr = "Edge";
					isDeletedElem = state.delEdges().contains(entity);
				}
				else assert false : "Entity is neither a node nor an edge (" + entity + ")!";

				if(!isDeletedElem) {
					sb.append("\t\t\tgraph.Changing" + kindStr + "Attribute(" + formatEntity(entity) +
								  ", " + kindStr + "Type_" + formatIdentifiable(qualTgt.getMember().getOwner()) +
								  ".AttributeType_" + formatIdentifiable(qualTgt.getMember()) + ", ");
					genExpression(sb, ass.getTarget(), state);
					sb.append(", " + varName + ");\n");
				}

				sb.append("\t\t\t");
				genExpression(sb, ass.getTarget(), state);
				sb.append(" = ");
				if(ass.getTarget().getType() instanceof EnumType)
					sb.append("(ENUM_" + formatIdentifiable(ass.getTarget().getType()) + ") ");
				sb.append(varName + ";\n");
			}
			else if(target instanceof Visited) {
				Visited visTgt = (Visited) target;

				sb.append("\t\t\tgraph.SetVisited(" + formatEntity(visTgt.getEntity()) + ", ");
				genExpression(sb, visTgt.getVisitorID(), state);
				sb.append(", ");
				genExpression(sb, ass.getTarget(), state);
				sb.append(");\n");
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
		if(state==null) {
			assert false;
			sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
			return;
		}

		if(accessViaVariable(state, (GraphEntity) owner, member)) {
			sb.append("tempvar_" + formatEntity(owner) + "_" + formatIdentifiable(member));
		}
		else {
			if(state.accessViaInterface().contains(owner))
				sb.append("i");

			sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
		}
	}

	protected void genMemberAccess(StringBuffer sb, Entity member) {
		throw new UnsupportedOperationException("Member expressions not allowed in actions!");
	}

	private boolean accessViaVariable(ModifyGenerationStateConst state, GraphEntity elem, Entity attr) {
		if(!state.reusedElements().contains(elem)) {
			HashSet<Entity> forcedAttrs = state.forceAttributeToVar().get(elem);
			return forcedAttrs != null && forcedAttrs.contains(attr);
		}
		HashSet<Entity> neededAttrs = state.neededAttributes().get(elem);
		return neededAttrs != null && neededAttrs.contains(attr);
	}

	private void genVariable(StringBuffer sb, String ownerName, Entity entity) {
		String varTypeName;
		String attrName = formatIdentifiable(entity);
		Type type = entity.getType();
		if(type instanceof EnumType)
			varTypeName = "ENUM_" + formatIdentifiable(type);
		else {
			switch(type.classify()) {
				case Type.IS_BOOLEAN:
					varTypeName = "bool";
					break;
				case Type.IS_INTEGER:
					varTypeName = "int";
					break;
				case Type.IS_FLOAT:
					varTypeName = "float";
					break;
				case Type.IS_DOUBLE:
					varTypeName = "double";
					break;
				case Type.IS_STRING:
					varTypeName = "String";
					break;
				case Type.IS_OBJECT:
				case Type.IS_UNKNOWN:
					varTypeName = "Object";
					break;
				default:
					throw new IllegalArgumentException();
			}
		}

		sb.append("\t\t\t" + varTypeName + " tempvar_" + ownerName + "_" + attrName);
	}
}

