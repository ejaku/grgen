/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.AlternativeReplacement;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.AssignmentGraphEntity;
import de.unika.ipd.grgen.ir.AssignmentIdentical;
import de.unika.ipd.grgen.ir.AssignmentVar;
import de.unika.ipd.grgen.ir.AssignmentVisited;
import de.unika.ipd.grgen.ir.BooleanType;
import de.unika.ipd.grgen.ir.CompoundAssignment;
import de.unika.ipd.grgen.ir.CompoundAssignmentChanged;
import de.unika.ipd.grgen.ir.CompoundAssignmentChangedVar;
import de.unika.ipd.grgen.ir.CompoundAssignmentChangedVisited;
import de.unika.ipd.grgen.ir.CompoundAssignmentVar;
import de.unika.ipd.grgen.ir.CompoundAssignmentVarChanged;
import de.unika.ipd.grgen.ir.CompoundAssignmentVarChangedVar;
import de.unika.ipd.grgen.ir.CompoundAssignmentVarChangedVisited;
import de.unika.ipd.grgen.ir.DoubleType;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Emit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.ExternalType;
import de.unika.ipd.grgen.ir.FloatType;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.ImperativeStmt;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.IntType;
import de.unika.ipd.grgen.ir.IteratedReplacement;
import de.unika.ipd.grgen.ir.MapAddItem;
import de.unika.ipd.grgen.ir.MapInit;
import de.unika.ipd.grgen.ir.MapRemoveItem;
import de.unika.ipd.grgen.ir.MapVarAddItem;
import de.unika.ipd.grgen.ir.MapVarRemoveItem;
import de.unika.ipd.grgen.ir.ObjectType;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.OrderedReplacement;
import de.unika.ipd.grgen.ir.SetAddItem;
import de.unika.ipd.grgen.ir.SetInit;
import de.unika.ipd.grgen.ir.SetRemoveItem;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.RetypedEdge;
import de.unika.ipd.grgen.ir.RetypedNode;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SetVarAddItem;
import de.unika.ipd.grgen.ir.SetVarRemoveItem;
import de.unika.ipd.grgen.ir.StringType;
import de.unika.ipd.grgen.ir.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.MapType;
import de.unika.ipd.grgen.ir.SetType;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.Visited;
import de.unika.ipd.grgen.ir.VoidType;


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
		Collection<EvalStatement> evals;
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
		public boolean useVarForMapResult() { return useVarForMapResult; }

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
		public boolean useVarForMapResult;


		public void InitNeeds(NeededEntities needs) {
			neededAttributes = needs.attrEntityMap;
			nodesNeededAsElements = needs.nodes;
			edgesNeededAsElements = needs.edges;
			nodesNeededAsAttributes = needs.attrNodes;
			edgesNeededAsAttributes = needs.attrEdges;
			neededVariables = needs.variables;

			int i = 0;
			for(Expression expr : needs.mapSetExprs) {
				if(expr instanceof MapInit || expr instanceof SetInit) continue;
				mapExprToTempVar.put(expr, "tempmapsetvar_" + i);
				i++;
			}
		}
	}

	final List<Entity> emptyParameters = new LinkedList<Entity>();
	final List<Expression> emptyReturns = new LinkedList<Expression>();
	final Collection<EvalStatement> emptyEvals = new HashSet<EvalStatement>();

	// eval statement generation state
	HashSet<String> defined = new HashSet<String>();
	int mapSetVarID;

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
					  + "(GRGEN_LGSP.LGSPGraph graph, IMatch_"+pathPrefix+altName+" curMatch");
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
					+ "(graph, (Match_"+pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+")curMatch");
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
		sb.append("\t\tpublic void " + pathPrefix+altName+"_Delete(GRGEN_LGSP.LGSPGraph graph, "
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
					+ "Delete(graph, (Match_"+pathPrefix+altName+"_"+altCase.getPattern().getNameOfGraph()+")curMatch);\n");
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
					  + "(GRGEN_LGSP.LGSPGraph graph, "
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
				+ "(graph, curMatch");
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
					  + "(GRGEN_LGSP.LGSPGraph graph, "
					  + "GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefix+iterName
					  + ", IMatch_"+pathPrefix+iterName+"> curMatches)\n");
		sb.append("\t\t{\n");

		// Emit dispatcher calling the modify-method of the iterated pattern which was matched
		sb.append("\t\t\tfor(Match_"+pathPrefix+iterName+" curMatch=curMatches.Root;"
				+" curMatch!=null; curMatch=curMatch.next) {\n");
		sb.append("\t\t\t\t" + pathPrefix+iterName+"_Delete"
				+ "(graph, curMatch);\n");
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

		NeededEntities needs = new NeededEntities(true, true, true, false, true, true);
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

		// Do not collect map and set expressions for evals
		needs.collectMapSetExprs = false;
		collectElementsAndAttributesNeededByEvals(task, needs);
		needs.collectMapSetExprs = true;

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

		genTypeChangesNodes(sb2, stateConst, task,
				state.nodesNeededAsElements, state.nodesNeededAsTypes);

		genNewEdges(sb2, stateConst, task, useAddedElementNames, prefix,
				state.nodesNeededAsElements, state.edgesNeededAsElements,
				state.edgesNeededAsTypes);

		genTypeChangesEdges(sb2, task, stateConst, state.edgesNeededAsElements, state.edgesNeededAsTypes);

		genNewSubpatternCalls(sb2, stateConst);

		genEvals(sb3, stateConst, task.evals);

		genVariablesForUsedAttributesBeforeDelete(sb3, stateConst, state.forceAttributeToVar);

		genCheckDeletedElementsForRetypingThroughHomomorphy(sb3, stateConst);

		genDelEdges(sb3, stateConst, state.edgesNeededAsElements, task.right);

		genDelNodes(sb3, stateConst, state.nodesNeededAsElements, task.right);

		genDelSubpatternCalls(sb3, stateConst);

		genMapAndSetVariablesBeforeImperativeStatements(sb3, stateConst);

		state.useVarForMapResult = true;
		genImperativeStatements(sb3, stateConst, task, pathPrefix);
		state.useVarForMapResult = false;

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
						+ "(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch"
						+ outParameters + ")\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\t"+matchType+" curMatch = ("+matchType+")_curMatch;\n");
			} else {
				sb.append("\t\tpublic void "
						+ pathPrefix+task.left.getNameOfGraph() + "_Modify"
						+ "(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch");
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
				sb.append("\t\t\t"+matchType+" curMatch = ("+matchType+")_curMatch;\n");
			}
			break;
		case TYPE_OF_TASK_CREATION:
			sb.append("\t\tpublic void "
					+ pathPrefix+task.left.getNameOfGraph() + "_Create"
					+ "(GRGEN_LGSP.LGSPGraph graph");
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
			break;
		case TYPE_OF_TASK_DELETION:
			sb.append("\t\tpublic void "
					+ pathPrefix+task.left.getNameOfGraph() + "_Delete"
					+ "(GRGEN_LGSP.LGSPGraph graph, "+matchType+" curMatch)\n");
			sb.append("\t\t{\n");
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
		for(OrderedReplacement orderedRepl : task.right.getOrderedReplacements()) {
			if(orderedRepl instanceof SubpatternDependentReplacement) {
				SubpatternDependentReplacement subRepl = (SubpatternDependentReplacement)orderedRepl;
				delSubpatternUsages.remove(subRepl.getSubpatternUsage());
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
		}
		for(Edge edge : task.right.getEdges()) {
			if(edge.inheritsType())
				accessViaInterface.add(edge);
			else if(edge.changesType(task.right))
				accessViaInterface.add(edge.getRetypedEntity(task.right));
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
		
		for(OrderedReplacement orpl : task.right.getOrderedReplacements())
		{
			if(orpl instanceof Emit) {
				Emit emit = (Emit) orpl;
				for(Expression arg : emit.getArguments())
					arg.collectNeededEntities(needs);
			}
			// the other ordered statement is the totally different dependent subpattern replacement
		}
	}

	private void collectElementsAndAttributesNeededByEvals(ModifyGenerationTask task, NeededEntities needs)
	{
		for(EvalStatement evalStmt : task.evals) {
			evalStmt.collectNeededEntities(needs);
		}
		for(OrderedReplacement orderedRep : task.right.getOrderedReplacements()) {
			if(orderedRep instanceof EvalStatement) {
				((EvalStatement)orderedRep).collectNeededEntities(needs);
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
				if(var.getType() instanceof IntType || var.getType() instanceof DoubleType
						|| var.getType() instanceof EnumType) {
					sb.append("0;\n");
				} else if(var.getType() instanceof FloatType) {
					sb.append("0f;\n");
				} else if(var.getType() instanceof BooleanType) {
					sb.append("false;\n");
				} else if(var.getType() instanceof StringType || var.getType() instanceof ObjectType 
						|| var.getType() instanceof VoidType || var.getType() instanceof ExternalType 
						|| var.getType() instanceof MapType || var.getType() instanceof SetType) {
					sb.append("null;\n");
				} else {
					throw new IllegalArgumentException("Unknown type: " + var.getType());
				}			
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

	private void genMapAndSetVariablesBeforeImperativeStatements(StringBuffer sb, ModifyGenerationStateConst state) {
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
		if(task.mightThereBeDeferredExecs) {
			sb.append("\t\t\tgraph.sequencesManager.ExecuteDeferredSequencesThenExitRuleModify(graph);\n");
		}

		int xgrsID = 0;
		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit) istmt;
				for(Expression arg : emit.getArguments()) {
					sb.append("\t\t\tgraph.EmitWriter.Write(");
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
					for(Entity neededEntity : exec.getNeededEntities()) {
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
					sb.append("\t\t\tgraph.sequencesManager.AddDeferredSequence(xgrs"+xgrsID+");\n");
				} else {
					for(Entity neededEntity : exec.getNeededEntities()) {
						if(neededEntity.isDefToBeYieldedTo()) {
							sb.append("\t\t\t" + formatElementInterfaceRef(neededEntity.getType()) + " ");
							sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + " = ");
							sb.append("("+formatElementInterfaceRef(neededEntity.getType())+")");
							sb.append(formatEntity(neededEntity) + ";\n");
						}
					}
					sb.append("\t\t\tApplyXGRS_" + task.left.getNameOfGraph() + "_" + xgrsID + "(graph");
					for(Entity neededEntity : exec.getNeededEntities()) {
						if(!neededEntity.isDefToBeYieldedTo()) {
							sb.append(", ");
							if(neededEntity.getType() instanceof InheritanceType) {
								sb.append("("+formatElementInterfaceRef(neededEntity.getType())+")");
							}
							sb.append(formatEntity(neededEntity));
						}
					}
					for(Entity neededEntity : exec.getNeededEntities()) {
						if(neededEntity.isDefToBeYieldedTo()) {
							sb.append(", out ");
							sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID);
						}
					}
					sb.append(");\n");
					for(Entity neededEntity : exec.getNeededEntities()) {
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
				if(entity.getType() instanceof MapType || entity.getType() instanceof SetType) continue;

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

	private void genTypeChangesNodes(StringBuffer sb, ModifyGenerationStateConst state, ModifyGenerationTask task,
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
						"Delete" + "(graph, alternative_"+altName+");\n");
			}
		}
	}

	private void genAlternativeModificationCall(Alternative alt, StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		String altName = alt.getNameOfGraph();
		sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+altName+"_" +
				"Modify(graph, alternative_" + altName);
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
						"Delete" + "(graph, iterated_"+iterName+");\n");
			}
		}
	}

	private void genIteratedModificationCall(Rule iter, StringBuffer sb, ModifyGenerationTask task, String pathPrefix) {
		String iterName = iter.getLeft().getNameOfGraph();
		sb.append("\t\t\t" + pathPrefix+task.left.getNameOfGraph()+"_"+iterName+"_" +
				"Modify(graph, iterated_" + iterName);
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

		if(task.mightThereBeDeferredExecs) {
			sb.append("\t\t\tgraph.sequencesManager.EnterRuleModifyAddingDeferredSequences();\n");
		}

		// generate calls to the dependent modifications of the subpatterns
		for(OrderedReplacement orderedRep : task.right.getOrderedReplacements()) {
			if(orderedRep instanceof SubpatternDependentReplacement) {
				SubpatternDependentReplacement subRep = (SubpatternDependentReplacement)orderedRep;
				Rule subRule = subRep.getSubpatternUsage().getSubpatternAction();
				String subName = formatIdentifiable(subRep);
				sb.append("\t\t\tPattern_" + formatIdentifiable(subRule)
						+ ".Instance." + formatIdentifiable(subRule) +
						"_Modify(graph, subpattern_" + subName);
				NeededEntities needs = new NeededEntities(true, true, true, false, true, true);
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
					sb.append("\t\t\tgraph.EmitWriter.Write(");
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
			if(node.isRetyped()) continue;
			if(state.yieldedNodes().contains(node)) continue;
			sb.append("\t\t\tGRGEN_LGSP.LGSPNode " + formatEntity(node)
					+ " = curMatch." + formatEntity(node, "_") + ";\n");
		}
		for(Node node : state.nodesNeededAsAttributes()) {
			if(node.isRetyped()) continue;
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
			if(edge.isRetyped()) continue;
			if(state.yieldedEdges().contains(edge)) continue;
			sb.append("\t\t\tGRGEN_LGSP.LGSPEdge " + formatEntity(edge)
					+ " = curMatch." + formatEntity(edge, "_") + ";\n");
		}
		for(Edge edge : state.edgesNeededAsAttributes()) {
			if(edge.isRetyped()) continue;
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
					"_Create(graph");
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
					"_Delete(graph, subpattern_" + subName + ");\n");
		}
	}

	//////////////////////////
	// Eval part generation //
	//////////////////////////

	private void initEvalGen() {
		// init eval statement generation state
		defined.clear();
		mapSetVarID = 0;
	}

	private void genEvals(StringBuffer sb, ModifyGenerationStateConst state, Collection<EvalStatement> evalStatements) {
		for(EvalStatement evalStmt : evalStatements) {
			genEvalStmt(sb, state, evalStmt);
		}
	}

	private void genEvalStmt(StringBuffer sb, ModifyGenerationStateConst state, EvalStatement evalStmt) {
		if(evalStmt instanceof Assignment) {
			genAssignment(sb, state, (Assignment) evalStmt);
		}
		else if(evalStmt instanceof AssignmentVar) {
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
		else if(evalStmt instanceof MapAddItem) {
			genMapAddItem(sb, state, (MapAddItem) evalStmt);
		} 
		else if(evalStmt instanceof SetRemoveItem) {
			genSetRemoveItem(sb, state, (SetRemoveItem) evalStmt);
		} 
		else if(evalStmt instanceof SetAddItem) {
			genSetAddItem(sb, state, (SetAddItem) evalStmt);
		}
		else if(evalStmt instanceof MapVarRemoveItem) {
			genMapVarRemoveItem(sb, state, (MapVarRemoveItem) evalStmt);
		} 
		else if(evalStmt instanceof MapVarAddItem) {
			genMapVarAddItem(sb, state, (MapVarAddItem) evalStmt);
		}
		else if(evalStmt instanceof SetVarRemoveItem) {
			genSetVarRemoveItem(sb, state, (SetVarRemoveItem) evalStmt);
		}
		else if(evalStmt instanceof SetVarAddItem) {
			genSetVarAddItem(sb, state, (SetVarAddItem) evalStmt);
		}
		else {
			throw new UnsupportedOperationException("Unexpected eval statement \"" + evalStmt + "\"");
		}
	}

	private void genAssignment(StringBuffer sb, ModifyGenerationStateConst state, Assignment ass) {
		Qualification target = ass.getTarget();
		Expression expr = ass.getExpression();

		if(target.getType() instanceof MapType || target.getType() instanceof SetType) {
			String typeName = formatAttributeType(target.getType());
			String varName = "tempmapsetvar_" + mapSetVarID++;

			// Check whether we have to make a copy of the right hand side of the assignment
			boolean mustCopy = true;
			if(expr instanceof Operator) {
				Operator op = (Operator) expr;

				// For unions and intersections new maps/sets are already created,
				// so we don't have to copy them again
				if(op.getOpCode() == Operator.BIT_OR || op.getOpCode() == Operator.BIT_AND)
					mustCopy = false;
			}

			sb.append("\t\t\t" + typeName + " " + varName + " = ");
			if(mustCopy)
				sb.append("new " + typeName + "(");
			genExpression(sb, expr, state);
			if(mustCopy)
				sb.append(')');
			sb.append(";\n");

			genChangingAttribute(sb, state, target, "Assign", varName , "null");

			sb.append("\t\t\t");
			genExpression(sb, target, state);
			sb.append(" = " + varName + ";\n");

			return;
		}

		String varName, varType;
		switch(target.getType().classify()) {
			case Type.IS_BOOLEAN:
				varName = "tempvar_bool";
				varType = defined.contains("bool") ? "" : "bool ";
				defined.add("bool");
				break;
			case Type.IS_INTEGER:
				varName = "tempvar_int";
				varType = defined.contains("int") ? "" : "int ";
				defined.add("int");
				break;
			case Type.IS_FLOAT:
				varName = "tempvar_float";
				varType = defined.contains("float") ? "" : "float ";
				defined.add("float");
				break;
			case Type.IS_DOUBLE:
				varName = "tempvar_double";
				varType = defined.contains("double") ? "" : "double ";
				defined.add("double");
				break;
			case Type.IS_STRING:
				varName = "tempvar_string";
				varType = defined.contains("string") ? "" : "string ";
				defined.add("string");
				break;
			case Type.IS_OBJECT:
				varName = "tempvar_object";
				varType = defined.contains("object") ? "" : "Object ";
				defined.add("object");
				break;
			case Type.IS_EXTERNAL_TYPE:
				varName = "tempvar_" + target.getType().getIdent();
				varType = defined.contains(target.getType().getIdent().toString()) ? "" : "GRGEN_MODEL."+target.getType().getIdent()+" ";
				defined.add(target.getType().getIdent().toString());
				break;
			default:
				throw new IllegalArgumentException();
		}

		sb.append("\t\t\t" + varType + varName + " = ");
		if(target.getType() instanceof EnumType)
			sb.append("(int) ");
		genExpression(sb, expr, state);
		sb.append(";\n");

		genChangingAttribute(sb, state, target, "Assign", varName, "null");

		sb.append("\t\t\t");
		genExpression(sb, target, state);
		sb.append(" = ");
		if(target.getType() instanceof EnumType)
			sb.append("(GRGEN_MODEL.ENUM_" + formatIdentifiable(target.getType()) + ") ");
		sb.append(varName + ";\n");
	}

	private void genAssignmentVar(StringBuffer sb, ModifyGenerationStateConst state, AssignmentVar ass) {
		Variable target = ass.getTarget();
		Expression expr = ass.getExpression();

		sb.append("\t\t\t");
		sb.append("var_" + target.getIdent());
		sb.append(" = ");
		if(target.getType() instanceof EnumType)
			sb.append("(GRGEN_MODEL.ENUM_" + formatIdentifiable(target.getType()) + ") ");
		genExpression(sb, expr, state);
		sb.append(";\n");
	}

	private void genAssignmentGraphEntity(StringBuffer sb, ModifyGenerationStateConst state, AssignmentGraphEntity ass) {
		GraphEntity target = ass.getTarget();
		Expression expr = ass.getExpression();

		sb.append("\t\t\t");
		sb.append(formatEntity(target));
		sb.append(" = ");
		genExpression(sb, expr, state);
		sb.append(";\n");
	}

	private void genAssignmentVisited(StringBuffer sb, ModifyGenerationStateConst state, AssignmentVisited ass) {
		sb.append("\t\t\tgraph.SetVisited(" + formatEntity(ass.getTarget().getEntity()) + ", ");
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
				String varName = "tempvar_bool";
				String varType = defined.contains("bool") ? "" : "bool ";
				defined.add("bool");

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
		genExpression(changedTargetBuffer, changedTarget.getVisitorID(), state);

		String prefix = "\t\t\t" + "graph.SetVisited("
			+ formatEntity(changedTarget.getEntity()) + ", "
			+ changedTargetBuffer.toString() + ", ";
		if(cass.getChangedOperation()!=CompoundAssignment.ASSIGN) {
			prefix += "graph.IsVisited(" + formatEntity(changedTarget.getEntity())
				+ ", " + changedTargetBuffer.toString() + ")"
				+ (cass.getChangedOperation()==CompoundAssignment.UNION ? " | " : " & ");
		}

		genCompoundAssignment(sb, state, cass, prefix, ");\n");
	}
	
	private void genCompoundAssignment(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignment cass,
			String prefix, String postfix)
	{
		Qualification target = cass.getTarget();
		assert(target.getType() instanceof MapType || target.getType() instanceof SetType);
		Expression expr = cass.getExpression();

		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		boolean isDeletedElem = element instanceof Node ? state.delNodes().contains(element) : state.delEdges().contains(element);
		if(!isDeletedElem && be.system.mayFireEvents()) {
			sb.append(prefix);
			sb.append("GRGEN_LIBGR.DictionaryHelper.");
			if(cass.getOperation()==CompoundAssignment.UNION)
				sb.append("UnionChanged(");
			else if(cass.getOperation()==CompoundAssignment.INTERSECTION)
				sb.append("IntersectChanged(");
			else //if(cass.getOperation()==CompoundAssignment.WITHOUT)
				sb.append("ExceptChanged(");
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
			String varName = "tempvar_bool";
			String varType = defined.contains("bool") ? "" : "bool ";
			defined.add("bool");

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
		genExpression(changedTargetBuffer, changedTarget.getVisitorID(), state);

		String prefix = "\t\t\t" + "graph.SetVisited("
			+ formatEntity(changedTarget.getEntity()) + ", "
			+ changedTargetBuffer.toString() + ", ";
		if(cass.getChangedOperation()!=CompoundAssignment.ASSIGN) {
			prefix += "graph.IsVisited(" + formatEntity(changedTarget.getEntity())
				+ ", " + changedTargetBuffer.toString() + ")"
				+ (cass.getChangedOperation()==CompoundAssignment.UNION ? " | " : " & ");
		}

		genCompoundAssignmentVar(sb, state, cass, prefix, ");\n");
	}

	private void genCompoundAssignmentVar(StringBuffer sb, ModifyGenerationStateConst state, CompoundAssignmentVar cass,
			String prefix, String postfix)
	{
		Variable target = cass.getTarget();
		assert(target.getType() instanceof MapType || target.getType() instanceof SetType);
		Expression expr = cass.getExpression();

		sb.append(prefix);
		sb.append("GRGEN_LIBGR.DictionaryHelper.");
		if(cass.getOperation()==CompoundAssignment.UNION)
			sb.append("UnionChanged(");
		else if(cass.getOperation()==CompoundAssignment.INTERSECTION)
			sb.append("IntersectChanged(");
		else //if(cass.getOperation()==CompoundAssignment.WITHOUT)
			sb.append("ExceptChanged(");
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
			sb.append("\t\t\tgraph.Changing" + kindStr + "Attribute(" +
					formatEntity(element) +	", " +
					formatTypeClassRef(elementType) + "." +
					formatAttributeTypeName(attribute) + ", " +
					"GRGEN_LIBGR.AttributeChangeType." + attributeChangeType + ", " +
					newValue + ", " + keyValue + ");\n");
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
		HashSet<Entity> forcedAttrs = state.forceAttributeToVar().get(elem);
		return forcedAttrs != null && forcedAttrs.contains(attr);
	}

	private void genVariable(StringBuffer sb, String ownerName, Entity entity) {
		String varTypeName;
		String attrName = formatIdentifiable(entity);
		Type type = entity.getType();
		if(type instanceof EnumType)
			varTypeName = "GRGEN_MODEL.ENUM_" + formatIdentifiable(type);
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
					varTypeName = "string";
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

