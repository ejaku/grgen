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
 * @version $Id$
 */

package de.unika.ipd.grgen.be.Csharp;

import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Emit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.ImperativeStmt;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Variable;
import java.util.Collection;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;

public class ActionsGen extends CSharpBase {
	public ActionsGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix) {
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
		model = be.unit.getActionsGraphModel();
		mg = new ModifyGen(nodeTypePrefix, edgeTypePrefix);
	}

	/**
	 * Generates the subpatterns and actions sourcecode for this unit.
	 */
	public void genActionsAndSubpatterns() {
		StringBuffer sb = new StringBuffer();
		String filename = be.unit.getUnitName() + "Actions_intermediate.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.append("// This file has been generated automatically by GrGen.\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
				+ "using System.Text;\n"
				+ "using de.unika.ipd.grGen.libGr;\n"
				+ "using de.unika.ipd.grGen.lgsp;\n"
				+ "using de.unika.ipd.grGen.Model_" + be.unit.getActionsGraphModelName() + ";\n"
				+ "\n"
				+ "namespace de.unika.ipd.grGen.Action_" + be.unit.getUnitName() + "\n"
				+ "{\n");

		for(Rule subpatternRule : be.unit.getSubpatternRules()) {
			genSubpattern(sb, subpatternRule);
		}

		for(Rule actionRule : be.unit.getActionRules()) {
			genAction(sb, actionRule);
		}

		sb.append("// GrGen insert Actions here\n");
		sb.append("}\n");

		writeFile(be.path, filename, sb);
	}

	/**
	 * Generates the subpattern actions sourcecode for the given subpattern-matching-action
	 */
	private void genSubpattern(StringBuffer sb, Rule subpatternRule) {
		String actionName = formatIdentifiable(subpatternRule);

		sb.append("\tpublic class Pattern_" + actionName + " : LGSPMatchingPattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static Pattern_" + actionName + " instance = null;\n"); //new Rule_" + actionName + "();\n");
		sb.append("\t\tpublic static Pattern_" + actionName + " Instance { get { if (instance==null) { instance = new Pattern_" + actionName + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");
		sb.append("\t\tprivate static object[] ReturnArray = new object[" + subpatternRule.getReturns().size() + "];\n\n");

		String patGraphVarName = "pat_" + subpatternRule.getPattern().getNameOfGraph();
		genTypeConditionsAndEnums(sb, subpatternRule.getPattern(), patGraphVarName,
				subpatternRule.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, subpatternRule, true);
		sb.append("\n");
		genActionConditions(sb, subpatternRule);
		sb.append("\n");

		mg.genModify(sb, subpatternRule, true);

		sb.append("\t}\n");
		sb.append("\n");
	}

	/**
	 * Generates the actions sourcecode for the given matching-action
	 */
	private void genAction(StringBuffer sb, Rule actionRule) {
		String actionName = formatIdentifiable(actionRule);

		sb.append("\tpublic class Rule_" + actionName + " : LGSPRulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static Rule_" + actionName + " instance = null;\n"); //new Rule_" + actionName + "();\n");
		sb.append("\t\tpublic static Rule_" + actionName + " Instance { get { if (instance==null) { instance = new Rule_" + actionName + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");
		sb.append("\t\tprivate static object[] ReturnArray = new object[" + actionRule.getReturns().size() + "];\n\n");

		String patGraphVarName = "pat_" + actionRule.getPattern().getNameOfGraph();
		genTypeConditionsAndEnums(sb, actionRule.getPattern(), patGraphVarName,
				actionRule.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, actionRule, false);
		sb.append("\n");
		genActionConditions(sb, actionRule);
		sb.append("\n");

		mg.genModify(sb, actionRule, false);

		if(actionRule.getRight()!=null) {
			genEmit(sb, actionRule, false);
		}

		sb.append("\t}\n");
		sb.append("\n");
	}

	////////////////////////////////
	// Type conditions generation //
	////////////////////////////////

	private void genTypeConditionsAndEnums(StringBuffer sb, PatternGraph pattern, String patGraphVarName,
										   String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);
		sb.append("\t\tPatternGraph " + patGraphVarName + ";\n");
		sb.append("\n");

		int i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genTypeConditionsAndEnums(sb, neg, pathPrefixForElements+negName,
					pathPrefixForElements + negName + "_",
					alreadyDefinedEntityToNameClone);
			++i;
		}

		i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			genCaseEnum(sb, alt, pathPrefixForElements+altName+"_");
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
				genTypeConditionsAndEnums(sb, altCasePattern, altPatGraphVarName,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						alreadyDefinedEntityToNameClone);
			}
			++i;
		}
	}

	private void genAllowedTypeArrays(StringBuffer sb, PatternGraph pattern,
									  String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		genAllowedNodeTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genAllowedEdgeTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
	}

	private void genAllowedNodeTypeArrays(StringBuffer sb, PatternGraph pattern,
										  String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		StringBuilder aux = new StringBuilder();
		for(Node node : pattern.getNodes()) {
			if(alreadyDefinedEntityToName.get(node)!=null) {
				continue;
			}
			sb.append("\t\tpublic static NodeType[] " + formatEntity(node, pathPrefixForElements) + "_AllowedTypes = ");
			aux.append("\t\tpublic static bool[] " + formatEntity(node, pathPrefixForElements) + "_IsAllowedType = ");
			if( !node.getConstraints().isEmpty() ) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : node.getConstraints())
					for(Type type : model.getNodeTypes()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : model.getNodeTypes()) {
					boolean isAllowed = type.isCastableTo(node.getNodeType()) && !allForbiddenTypes.contains(type);
					// all permitted nodes, aka nodes that are not forbidden
					if( isAllowed )
						sb.append(formatTypeClass(type) + ".typeVar, ");
					aux.append(isAllowed);
					aux.append(", ");
				}
				sb.append("}");
				aux.append("}");
			} else {
				sb.append("null");
				aux.append("null");
			}
			sb.append(";\n");
			aux.append(";\n");
			alreadyDefinedEntityToName.put(node, formatEntity(node, pathPrefixForElements));
		}
		sb.append(aux);
	}

	private void genAllowedEdgeTypeArrays(StringBuffer sb, PatternGraph pattern,
										  String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		StringBuilder aux = new StringBuilder();
		for(Edge edge : pattern.getEdges()) {
			if(alreadyDefinedEntityToName.get(edge)!=null) {
				continue;
			}
			sb.append("\t\tpublic static EdgeType[] " + formatEntity(edge, pathPrefixForElements) + "_AllowedTypes = ");
			aux.append("\t\tpublic static bool[] " + formatEntity(edge, pathPrefixForElements) + "_IsAllowedType = ");
			if( !edge.getConstraints().isEmpty() ) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : edge.getConstraints())
					for(Type type : model.getEdgeTypes()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : model.getEdgeTypes()) {
					boolean isAllowed = type.isCastableTo(edge.getEdgeType()) && !allForbiddenTypes.contains(type);
					// all permitted nodes, aka node that are not forbidden
					if( isAllowed )
						sb.append(formatTypeClass(type) + ".typeVar, ");
					aux.append(isAllowed);
					aux.append(", ");
				}
				sb.append("}");
				aux.append("}");
			} else {
				sb.append("null");
				aux.append("null");
			}
			sb.append(";\n");
			aux.append(";\n");
			alreadyDefinedEntityToName.put(edge, formatEntity(edge, pathPrefixForElements));
		}
		sb.append(aux);
	}

	//////////////////////
	// Enums generation //
	//////////////////////

	private void genEnums(StringBuffer sb, PatternGraph pattern, String pathPrefixForElements) {
		sb.append("\t\tpublic enum " + pathPrefixForElements + "NodeNums { ");
		for(Node node : pattern.getNodes()) {
			sb.append("@" + formatIdentifiable(node) + ", ");
		}
		sb.append("};\n");

		sb.append("\t\tpublic enum " + pathPrefixForElements + "EdgeNums { ");
		for(Edge edge : pattern.getEdges()) {
			sb.append("@" + formatIdentifiable(edge) + ", ");
		}
		sb.append("};\n");

		sb.append("\t\tpublic enum " + pathPrefixForElements + "VariableNums { ");
		for(Variable var : pattern.getVars()) {
			sb.append("@" + formatIdentifiable(var) + ", ");
		}
		sb.append("};\n");

		sb.append("\t\tpublic enum " + pathPrefixForElements + "SubNums { ");
		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			sb.append("@" + formatIdentifiable(sub) + ", ");
		}
		sb.append("};\n");

		sb.append("\t\tpublic enum " + pathPrefixForElements + "AltNums { ");

		for(int i=0; i < pattern.getAlts().size(); i++) {
			sb.append("@" + "alt_" + i + ", ");
		}
		sb.append("};\n");
	}

	private void genCaseEnum(StringBuffer sb, Alternative alt, String pathPrefixForElements) {
		sb.append("\t\tpublic enum " + pathPrefixForElements + "CaseNums { ");
		for(Rule altCase : alt.getAlternativeCases()) {
			PatternGraph altCasePattern = altCase.getLeft();
			sb.append("@" + altCasePattern.getNameOfGraph() + ", ");
		}
		sb.append("};\n");
	}

	/////////////////////////////////////////
	// Rule/Subpattern metadata generation //
	/////////////////////////////////////////

	private void genRuleOrSubpatternInit(StringBuffer sb, MatchingAction action, boolean isSubpattern) {
		PatternGraph pattern = action.getPattern();

		sb.append("#if INITIAL_WARMUP\n");
		sb.append("\t\tpublic " + (isSubpattern ? "Pattern_" : "Rule_") + formatIdentifiable(action) + "()\n");
		sb.append("#else\n");
		sb.append("\t\tprivate " + (isSubpattern ? "Pattern_" : "Rule_") + formatIdentifiable(action) + "()\n");
		sb.append("#endif\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tname = \"" + formatIdentifiable(action) + "\";\n");
		sb.append("\n");
		genRuleParamResult(sb, action, isSubpattern);
		sb.append("\t\t}\n");

		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();
		HashMap<Identifiable, String> alreadyDefinedIdentifiableToName = new HashMap<Identifiable, String>();

		double max = computePriosMax(-1, action.getPattern());

		StringBuilder aux = new StringBuilder();
		String patGraphVarName = "pat_" + pattern.getNameOfGraph();
		sb.append("\t\tpublic override void initialize()\n");
		sb.append("\t\t{\n");

		genPatternGraph(sb, aux, pattern, "", pattern.getNameOfGraph(), patGraphVarName, alreadyDefinedEntityToName,
						alreadyDefinedIdentifiableToName, 0, action.getParameters(), max);
		sb.append(aux);
		sb.append("\n");
		sb.append("\t\t\tpatternGraph = " + patGraphVarName + ";\n");

		sb.append("\t\t}\n");
	}

	private int genPatternGraph(StringBuffer sb, StringBuilder aux, PatternGraph pattern,
								String pathPrefix, String patternName, // negatives without name, have to compute it and hand it in
								String patGraphVarName,
								HashMap<Entity, String> alreadyDefinedEntityToName,
								HashMap<Identifiable, String> alreadyDefinedIdentifiableToName,
								int condCntInit, List<Entity> parameters, double max) {
		genElementsRequiredByPatternGraph(sb, aux, pattern, pathPrefix, patternName, patGraphVarName,
										  alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, condCntInit, parameters, max);

		sb.append("\t\t\t" + patGraphVarName + " = new PatternGraph(\n");
		sb.append("\t\t\t\t\"" + patternName + "\",\n");
		sb.append("\t\t\t\t\"" + pathPrefix + "\",\n");
		sb.append("\t\t\t\t" + (pattern.isIndependent() ? "true" : "false") + ",\n" );

		String pathPrefixForElements = pathPrefix+patternName+"_";

		sb.append("\t\t\t\tnew PatternNode[] ");
		genEntitySet(sb, pattern.getNodes(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew PatternEdge[] ");
		genEntitySet(sb, pattern.getEdges(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew PatternVariable[] ");
		genEntitySet(sb, pattern.getVars(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew PatternGraphEmbedding[] ");
		genSubpatternUsageSet(sb, pattern.getSubpatternUsages(), "", "", true, pathPrefixForElements, alreadyDefinedIdentifiableToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew Alternative[] { ");
		for(int i = 0; i < pattern.getAlts().size(); ++i) {
			sb.append(pathPrefixForElements+"alt_" + i + ", ");
		}
		sb.append(" }, \n");

		sb.append("\t\t\t\tnew PatternGraph[] { ");
		for(int i = 0; i < pattern.getNegs().size(); ++i) {
			sb.append(pathPrefixForElements+"neg_" + i + ", ");
		}
		sb.append(" }, \n");

		sb.append("\t\t\t\tnew PatternCondition[] { ");
		int condCnt = condCntInit;
		for (int i = 0; i < pattern.getConditions().size(); i++){
			sb.append("cond_" + condCnt + ", ");
			condCnt++;
		}
		sb.append(" }, \n");

		sb.append("\t\t\t\tnew bool[" + pattern.getNodes().size() + ", " + pattern.getNodes().size() + "] ");
		if(pattern.getNodes().size() > 0) {
			sb.append("{\n");
			for(Node node1 : pattern.getNodes()) {
				sb.append("\t\t\t\t\t{ ");
				for(Node node2 : pattern.getNodes()) {
					if(pattern.isHomomorphic(node1,node2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.append("\t\t\t\t}");
		}
		sb.append(",\n");

		sb.append("\t\t\t\tnew bool[" + pattern.getEdges().size() + ", " + pattern.getEdges().size() + "] ");
		if(pattern.getEdges().size() > 0) {
			sb.append("{\n");
			for(Edge edge1 : pattern.getEdges()) {
				sb.append("\t\t\t\t\t{ ");
				for(Edge edge2 : pattern.getEdges()) {
					if(pattern.isHomomorphic(edge1,edge2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.append("\t\t\t\t}");
		}
		sb.append(",\n");

		sb.append("\t\t\t\t");
		sb.append(pathPrefixForElements + "isNodeHomomorphicGlobal,\n");

		sb.append("\t\t\t\t");
		sb.append(pathPrefixForElements + "isEdgeHomomorphicGlobal\n");

		sb.append("\t\t\t);\n");

		// link edges to nodes
		for(Edge edge : pattern.getEdges()) {
			String edgeName = alreadyDefinedEntityToName.get(edge)!=null ?
					alreadyDefinedEntityToName.get(edge) : formatEntity(edge, pathPrefixForElements);

			if(pattern.getSource(edge)!=null) {
				String sourceName = formatEntity(pattern.getSource(edge), pathPrefixForElements, alreadyDefinedEntityToName);
				sb.append("\t\t\t" + patGraphVarName + ".edgeToSourceNode.Add("+edgeName+", "+sourceName+");\n");
			}

			if(pattern.getTarget(edge)!=null) {
				String targetName = formatEntity(pattern.getTarget(edge), pathPrefixForElements, alreadyDefinedEntityToName);
				sb.append("\t\t\t" + patGraphVarName + ".edgeToTargetNode.Add("+edgeName+", "+targetName+");\n");
			}
		}

		// set embedding-member of contained graphs
		int i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				sb.append("\t\t\t" + altPatGraphVarName + ".embeddingGraph = " + patGraphVarName + ";\n");
			}
			++i;
		}

		for(int j = 0; j < pattern.getNegs().size(); j++) {
			String negName = "neg_" + j;
			sb.append("\t\t\t" + pathPrefixForElements+negName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		sb.append("\n");

		return condCnt;
	}

	private void genElementsRequiredByPatternGraph(StringBuffer sb, StringBuilder aux, PatternGraph pattern,
												   String pathPrefix, String patternName, String patGraphVarName,
												   HashMap<Entity, String> alreadyDefinedEntityToName,
												   HashMap<Identifiable, String> alreadyDefinedIdentifiableToName,
												   int condCntInit, List<Entity> parameters, double max) {
		String pathPrefixForElements = pathPrefix+patternName+"_";

		sb.append("\t\t\tbool[,] " + pathPrefixForElements + "isNodeHomomorphicGlobal = "
				+ "new bool[" + pattern.getNodes().size() + ", " + pattern.getNodes().size() + "] ");
		if(pattern.getNodes().size() > 0) {
			sb.append("{\n");
			for(Node node1 : pattern.getNodes()) {
				sb.append("\t\t\t\t{ ");
				for(Node node2 : pattern.getNodes()) {
					if(pattern.isHomomorphicGlobal(alreadyDefinedEntityToName, node1, node2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.append("\t\t\t}");
		}
		sb.append(";\n");

		sb.append("\t\t\tbool[,] " + pathPrefixForElements + "isEdgeHomomorphicGlobal = "
				+ "new bool[" + pattern.getEdges().size() + ", " + pattern.getEdges().size() + "] ");
		if(pattern.getEdges().size() > 0) {
			sb.append("{\n");
			for(Edge edge1 : pattern.getEdges()) {
				sb.append("\t\t\t\t{ ");
				for(Edge edge2 : pattern.getEdges()) {
					if(pattern.isHomomorphicGlobal(alreadyDefinedEntityToName, edge1, edge2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.append("\t\t\t}");
		}
		sb.append(";\n");

		for(Node node : pattern.getNodes()) {
			if(alreadyDefinedEntityToName.get(node)!=null) {
				continue;
			}
			String nodeName = formatEntity(node, pathPrefixForElements);
			sb.append("\t\t\tPatternNode " + nodeName + " = new PatternNode(");
			sb.append("(int) NodeTypes.@" + formatIdentifiable(node.getType())
						  + ", \"" + nodeName + "\", \"" + formatIdentifiable(node) + "\", ");
			sb.append(nodeName + "_AllowedTypes, ");
			sb.append(nodeName + "_IsAllowedType, ");
			appendPrio(sb, node, max);
			sb.append(parameters.indexOf(node)+");\n");
			alreadyDefinedEntityToName.put(node, nodeName);
			aux.append("\t\t\t" + nodeName + ".PointOfDefinition = " + (parameters.indexOf(node)==-1 ? patGraphVarName : "null") + ";\n");

			node.setPointOfDefinition(pattern);
		}

		for(Edge edge : pattern.getEdges()) {
			if(alreadyDefinedEntityToName.get(edge)!=null) {
				continue;
			}
			String edgeName = formatEntity(edge, pathPrefixForElements);
			sb.append("\t\t\tPatternEdge " + edgeName + " = new PatternEdge(");
			sb.append((edge.hasFixedDirection() ? "true" : "false") + ", ");
			sb.append("(int) EdgeTypes.@" + formatIdentifiable(edge.getType())
						  + ", \"" + edgeName + "\", \"" + formatIdentifiable(edge) + "\", ");
			sb.append(edgeName + "_AllowedTypes, ");
			sb.append(edgeName + "_IsAllowedType, ");
			appendPrio(sb, edge, max);
			sb.append(parameters.indexOf(edge)+");\n");
			alreadyDefinedEntityToName.put(edge, edgeName);
			aux.append("\t\t\t" + edgeName + ".PointOfDefinition = " + (parameters.indexOf(edge)==-1 ? patGraphVarName : "null") + ";\n");

			edge.setPointOfDefinition(pattern);
		}

		for(Variable var : pattern.getVars()) {
			if(alreadyDefinedEntityToName.get(var)!=null) {
				continue;
			}
			String varName = formatEntity(var, pathPrefixForElements);
			sb.append("\t\t\tPatternVariable " + varName + " = new PatternVariable(");
			sb.append("VarType.GetVarType(typeof(" + formatAttributeType(var)
					+ ")), \"" + varName + "\", \"" + formatIdentifiable(var) + "\", ");
			sb.append(parameters.indexOf(var)+");\n");
			alreadyDefinedEntityToName.put(var, varName);
		}

		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			if(alreadyDefinedIdentifiableToName.get(sub)!=null) {
				continue;
			}
			String subName = formatIdentifiable(sub, pathPrefixForElements);
			sb.append("\t\t\tPatternGraphEmbedding " + subName + " = new PatternGraphEmbedding(");
			sb.append("\"" + formatIdentifiable(sub) + "\", ");
			sb.append("Pattern_"+ sub.getSubpatternAction().getIdent().toString() + ".Instance, ");
			sb.append("new PatternElement[] ");
			genEntitySet(sb, sub.getSubpatternConnections(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(");\n");
			alreadyDefinedIdentifiableToName.put(sub, subName);
			aux.append("\t\t\t" + subName + ".PointOfDefinition = " + patGraphVarName + ";\n");
		}

		int condCnt = condCntInit;
		for(Expression expr : pattern.getConditions()) {
			NeededEntities needs = new NeededEntities(true, true, true);
			expr.collectNeededEntities(needs);
			sb.append("\t\t\tPatternCondition cond_" + condCnt + " = new PatternCondition(" + condCnt +
					", " + (needs.isGraphUsed ? "true" : "false") + ", new String[] ");
			genEntitySet(sb, needs.nodes, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new String[] ");
			genEntitySet(sb, needs.edges, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new String[] ");
			genEntitySet(sb, needs.variables, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(");\n");
			condCnt++;
		}

		int i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
				HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
				condCnt = genPatternGraph(sb, aux, altCasePattern,
										  pathPrefixForElements+altName+"_", altCasePattern.getNameOfGraph(),
										  altPatGraphVarName,
										  alreadyDefinedEntityToNameClone,
										  alreadyDefinedIdentifiableToNameClone,
										  condCnt, parameters, max);
			}
			++i;
		}

		i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			sb.append("\t\t\tAlternative " + pathPrefixForElements+altName + " = new Alternative( ");
			sb.append("\"" + altName + "\", ");
			sb.append("\"" + pathPrefixForElements + "\", ");
			sb.append("new PatternGraph[] ");
			genAlternativesSet(sb, alt.getAlternativeCases(), pathPrefixForElements+altName+"_", "", true);
			sb.append(" );\n\n");
			++i;
		}

		i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			condCnt = genPatternGraph(sb, aux, neg,
									  pathPrefixForElements, negName, pathPrefixForElements+negName,
									  alreadyDefinedEntityToNameClone,
									  alreadyDefinedIdentifiableToNameClone,
									  condCnt, parameters, max);
			++i;
		}
	}

	private void genRuleParamResult(StringBuffer sb, MatchingAction action, boolean isSubpattern) {
		sb.append("\t\t\tinputs = new GrGenType[] { ");
		for(Entity ent : action.getParameters()) {
			if(ent instanceof Variable)
				sb.append("VarType.GetVarType(typeof(" + formatAttributeType(ent) + ")), ");
			else sb.append(formatTypeClass(ent.getType()) + ".typeVar, ");
		}
		sb.append("};\n");

		sb.append("\t\t\tinputNames = new string[] { ");
		for(Entity ent : action.getParameters())
			sb.append("\"" + formatEntity(ent, action.getPattern().getNameOfGraph()+"_") + "\", ");
		sb.append("};\n");

		if(!isSubpattern) {
			sb.append("\t\t\toutputs = new GrGenType[] { ");
			for(Expression expr : action.getReturns()) {
				if(expr instanceof GraphEntityExpression)
					sb.append(formatTypeClass(expr.getType()) + ".typeVar, ");
				else
					sb.append("VarType.GetVarType(typeof(" + formatAttributeType(expr.getType()) + ")), ");
			}
			sb.append("};\n");
		}
	}

	private void genEmit(StringBuffer sb, Rule rule, boolean isSubpattern) {
		String actionName = formatIdentifiable(rule);
		sb.append("#if INITIAL_WARMUP\t\t// GrGen emit statement section: " + (isSubpattern ? "Pattern_" : "Rule_") + actionName + "\n");
		int xgrsID = 0;
		for(ImperativeStmt istmt : rule.getRight().getImperativeStmts()) {
			if(istmt instanceof Emit) {
				// nothing to do
			} else if (istmt instanceof Exec) {
				Exec exec = (Exec) istmt;
				sb.append("\t\tpublic static LGSPXGRSInfo XGRSInfo_" + xgrsID + " = new LGSPXGRSInfo(new String[] {");
				for(Entity param : exec.getArguments()) {
					sb.append("\"" + param.getIdent() + "\", ");
				}
				sb.append("},\n");
				sb.append("\t\t\t\"" + exec.getXGRSString() + "\");\n");
				sb.append("\t\tprivate void ApplyXGRS_" + xgrsID++ + "(LGSPGraph graph");
				for(Entity param : exec.getArguments()) {
					sb.append(", object var_");
					sb.append(param.getIdent());
				}
				sb.append(") {}\n");
			} else assert false : "unknown ImperativeStmt: " + istmt + " in " + rule;
		}
		sb.append("#endif\n");
	}

	//////////////////////////
	// Condition generation //
	//////////////////////////

	private void genActionConditions(StringBuffer sb, MatchingAction action) {
		genPatternConditions(sb, action.getPattern(), 0);
	}

	private int genPatternConditions(StringBuffer sb, PatternGraph pattern, int condCnt) {
		condCnt = genConditions(sb, pattern.getConditions(), condCnt);
		for(Alternative alt : pattern.getAlts()) {
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				condCnt = genPatternConditions(sb, altCasePattern, condCnt);
			}
		}
		for(PatternGraph neg : pattern.getNegs()) {
			condCnt = genPatternConditions(sb, neg, condCnt);
		}
		return condCnt;
	}

	private int genConditions(StringBuffer sb, Collection<Expression> conditions, int condCnt) {
		for(Expression expr : conditions) {
			NeededEntities needs = new NeededEntities(true, true, true);
			expr.collectNeededEntities(needs);
			sb.append("\t\tpublic static bool Condition_" + condCnt + "(");
			boolean first = true;
			if(needs.isGraphUsed) {
				sb.append("LGSPGraph graph");
				first = false;
			}
			if(!needs.nodes.isEmpty())
			{
				if(!first) sb.append(", ");
				genSet(sb, needs.nodes, "LGSPNode node_", "", false);
				first = false;
			}

			if(!needs.edges.isEmpty())
			{
				if(!first) sb.append(", ");
				genSet(sb, needs.edges, "LGSPEdge edge_", "", false);
				first = false;
			}

			if(!needs.variables.isEmpty())
			{
				for(Variable var : needs.variables) {
					if(first) first = false;
					else sb.append(", ");
					sb.append(formatAttributeType(var));
					sb.append(" var_");
					sb.append(formatIdentifiable(var));
				}
			}
			sb.append(")\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn ");
			genExpression(sb, expr, null);
			sb.append(";\n");
			sb.append("\t\t}\n");
			++condCnt;
		}
		return condCnt;
	}

	///////////////////////////////////////
	// Static searchplan cost generation //
	///////////////////////////////////////

	private double computePriosMax(double max, PatternGraph pattern) {
		max = computePriosMax(pattern.getNodes(), max);
		max = computePriosMax(pattern.getEdges(), max);
		for(PatternGraph neg : pattern.getNegs()) {
			max = computePriosMax(max, neg);
		}
		for(Alternative alt : pattern.getAlts()) {
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				max = computePriosMax(max, altCasePattern);
			}
		}
		return max;
	}

	private double computePriosMax(Collection<? extends Entity> nodesOrEdges, double max) {
		for(Entity noe : nodesOrEdges) {
			Object prioO = noe.getAnnotations().get("prio");

			if (prioO != null && prioO instanceof Integer) {
				double val = ((Integer)prioO).doubleValue();
				assert val > 0 : "value of prio attribute of decl is out of range.";

				if(max < val)
					max = val;
			}
		}
		return max;
	}

	private void appendPrio(StringBuffer sb, Entity entity, double max) {
		Object prioO = entity.getAnnotations().get("prio");

		double prio;
		if (prioO != null && prioO instanceof Integer) {
			prio = ((Integer)prioO).doubleValue();
			prio = 10.0 - (prio / max) * 9.0;
		}
		else {
			prio = 5.5;
		}

		sb.append(prio + "F, ");
	}

	//////////////////////
	// Expression stuff //
	//////////////////////

	protected void genQualAccess(StringBuffer sb, Qualification qual, Object modifyGenerationState) {
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		genQualAccess(sb, owner, member);
	}

	protected void genQualAccess(StringBuffer sb, Entity owner, Entity member) {
		sb.append("((I" + getNodeOrEdgeTypePrefix(owner) +
					  formatIdentifiable(owner.getType()) + ") ");
		sb.append(formatEntity(owner) + ").@" + formatIdentifiable(member));
	}

	protected void genMemberAccess(StringBuffer sb, Entity member) {
		throw new UnsupportedOperationException("Member expressions not allowed in actions!");
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private SearchPlanBackend2 be;
	private ModifyGen mg;
	private Model model;
}
