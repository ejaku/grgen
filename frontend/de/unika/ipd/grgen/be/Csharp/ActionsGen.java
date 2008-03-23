/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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

import de.unika.ipd.grgen.ir.*;
import java.util.*;

public class ActionsGen extends CSharpBase {
	public ActionsGen(SearchPlanBackend2 backend) {
		be = backend;
		mg = new ModifyGen();
	}

	/**
	 * Generates the subpatterns and actions sourcecode for this unit.
	 */
	public void genRulesAndSubpatterns() {
		StringBuffer sb = new StringBuffer();
		String filename = be.unit.getUnitName() + "Actions_intermediate.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using System.Text;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("using de.unika.ipd.grGen.lgsp;\n");
		sb.append("using de.unika.ipd.grGen.Model_" + be.unit.getUnitName() + ";\n");
		sb.append("\n");

		sb.append("namespace de.unika.ipd.grGen.Action_" + be.unit.getUnitName() + "\n");
		sb.append("{\n");

		for(Action action : be.patternMap.keySet())
			if(action instanceof MatchingAction)
				genSubpattern(sb, (MatchingAction) action);
			else
				throw new IllegalArgumentException("Unknown Subpattern: " + action);

		for(Action action : be.actionMap.keySet())
			if(action instanceof MatchingAction)
				genRule(sb, (MatchingAction) action);
			else
				throw new IllegalArgumentException("Unknown Action: " + action);

		sb.append("// GrGen insert Actions here\n");
		sb.append("}\n");

		writeFile(be.path, filename, sb);
	}

	/**
	 * Generates the subpattern actions sourcecode for the given matching-action
	 */
	private void genSubpattern(StringBuffer sb, MatchingAction action) {
		String actionName = formatIdentifiable(action);

		sb.append("\tpublic class Pattern_" + actionName + " : LGSPRulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static Pattern_" + actionName + " instance = null;\n"); //new Rule_" + actionName + "();\n");
		sb.append("\t\tpublic static Pattern_" + actionName + " Instance { get { if (instance==null) { instance = new Pattern_" + actionName + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");
		genTypeConditionsAndEnums(sb, action.getPattern(), action.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, action, true);
		sb.append("\n");
		genActionConditions(sb, action);
		sb.append("\n");

		if(action instanceof Rule) {
			Rule rule = (Rule) action;
			mg.genRuleModify(sb, rule, true, true);
			sb.append("\n");
			mg.genRuleModify(sb, rule, false, true);

			mg.genAddedGraphElementsArray(sb, true);

			genEmit(sb, rule, true);
		}
		else if(action instanceof Test) {
			Test test = (Test) action;
			mg.genTestModify(sb, test, true);
			
			mg.genAddedGraphElementsArray(sb, false);
		}
		else {
			throw new IllegalArgumentException("Unknown action type: " + action);
		}

		sb.append("\t}\n");
		sb.append("\n");
	}

	/**
	 * Generates the actions sourcecode for the given matching-action
	 */
	private void genRule(StringBuffer sb, MatchingAction action) {
		String actionName = formatIdentifiable(action);

		sb.append("\tpublic class Rule_" + actionName + " : LGSPRulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static Rule_" + actionName + " instance = null;\n"); //new Rule_" + actionName + "();\n");
		sb.append("\t\tpublic static Rule_" + actionName + " Instance { get { if (instance==null) { instance = new Rule_" + actionName + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");
		genTypeConditionsAndEnums(sb, action.getPattern(), action.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, action, false);
		sb.append("\n");
		genActionConditions(sb, action);
		sb.append("\n");

		if(action instanceof Rule) {
			Rule rule = (Rule) action;
			mg.genRuleModify(sb, rule, true, false);
			sb.append("\n");
			mg.genRuleModify(sb, rule, false, false);

			mg.genAddedGraphElementsArray(sb, true);

			genEmit(sb, rule, false);
		}
		else if(action instanceof Test) {
			Test test = (Test) action;
			mg.genTestModify(sb, test, false);
			
			mg.genAddedGraphElementsArray(sb, false);
		}
		else {
			throw new IllegalArgumentException("Unknown action type: " + action);
		}

		sb.append("\t}\n");
		sb.append("\n");
	}

	////////////////////////////////
	// Type conditions generation //
	////////////////////////////////

	private void genTypeConditionsAndEnums(StringBuffer sb, PatternGraph pattern,
										   String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);

		int i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genTypeConditionsAndEnums(sb, neg, pathPrefixForElements+"neg_"+i+"_",
									  alreadyDefinedEntityToNameClone);
			++i;
		}

		i = 0;
		for(Alternative alt : pattern.getAlts()) {
			genCaseEnum(sb, alt, pathPrefixForElements+"alt_"+i+"_");
			for(PatternGraph altCase : alt.getAlternativeCases()) {
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
				genTypeConditionsAndEnums(sb, altCase, pathPrefixForElements+"alt_"+i+"_"+altCase.getNameOfGraph()+"_",
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
					for(Type type : be.nodeTypeMap.keySet()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : be.nodeTypeMap.keySet()) {
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
					for(Type type : be.edgeTypeMap.keySet()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : be.edgeTypeMap.keySet()) {
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
		for(PatternGraph altCase : alt.getAlternativeCases()) {
			sb.append("@" + altCase.getNameOfGraph() + ", ");
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
		sb.append("\t\t\tisSubpattern = " + (isSubpattern ? "true" : "false") + ";\n");
		sb.append("\n");
		genRuleParamResult(sb, action);
		sb.append("\t\t}\n");

		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();
		HashMap<Identifiable, String> alreadyDefinedIdentifiableToName = new HashMap<Identifiable, String>();

		double max = computePriosMax(-1, action.getPattern());

		StringBuilder aux = new StringBuilder();
		String patGraphVarName = "pat_" + pattern.getNameOfGraph();
		sb.append("\t\tpublic override void initialize()\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tPatternGraph " + patGraphVarName + ";\n");
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

		sb.append("\t\t\t\tnew Condition[] { ");
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
			for(PatternGraph altCase : alt.getAlternativeCases()) {
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCase.getNameOfGraph();
				sb.append("\t\t\t" + altPatGraphVarName + ".embeddingGraph = " + patGraphVarName + ";\n");
			}
			++i;
		}
		i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			sb.append("\t\t\t" + pathPrefixForElements+negName + ".embeddingGraph = " + patGraphVarName + ";\n");
			++i;
		}
		
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
			Set<Node> nodes = new LinkedHashSet<Node>();
			Set<Edge> edges = new LinkedHashSet<Edge>();
			expr.collectNodesnEdges(nodes, edges);
			sb.append("\t\t\tCondition cond_" + condCnt + " = new Condition(" + condCnt + ", new String[] ");
			genEntitySet(sb, nodes, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new String[] ");
			genEntitySet(sb, edges, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(");\n");
			condCnt++;
		}

		int i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			for(PatternGraph altCase : alt.getAlternativeCases()) {
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCase.getNameOfGraph();
				sb.append("\t\t\tPatternGraph " + altPatGraphVarName + ";\n");
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
				HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
				condCnt = genPatternGraph(sb, aux, altCase,
										  pathPrefixForElements+altName+"_", altCase.getNameOfGraph(),
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
			sb.append("\t\t\tPatternGraph " + pathPrefixForElements+negName + ";\n");
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

	private void genRuleParamResult(StringBuffer sb, MatchingAction action) {
		sb.append("\t\t\tinputs = new GrGenType[] { ");
		for(Entity ent : action.getParameters())
			sb.append(formatTypeClass(ent.getType()) + ".typeVar, ");
		sb.append("};\n");

		sb.append("\t\t\tinputNames = new string[] { ");
		for(Entity ent : action.getParameters())
			sb.append("\"" + formatEntity(ent, action.getPattern().getNameOfGraph()+"_") + "\", ");
		sb.append("};\n");

		sb.append("\t\t\toutputs = new GrGenType[] { ");
		for(Entity ent : action.getReturns())
			sb.append(formatTypeClass(ent.getType()) + ".typeVar, ");
		sb.append("};\n");

		sb.append("\t\t\toutputNames = new string[] { ");
		for(Entity ent : action.getReturns())
			sb.append("\"" + formatEntity(ent, action.getPattern().getNameOfGraph()+"_") + "\", ");
		sb.append("};\n");
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
					if(param instanceof Variable) continue;
					sb.append("\"" + param.getIdent() + "\", ");
				}
				sb.append("},\n");
				sb.append("\t\t\t\"" + exec.getXGRSString() + "\");\n");
				sb.append("\t\tprivate void ApplyXGRS_" + xgrsID++ + "(LGSPGraph graph");
				for(Entity param : exec.getArguments()) {
					if(param instanceof Variable) continue;
					sb.append(", IGraphElement var_");
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
			for(PatternGraph altCase : alt.getAlternativeCases()) {
				condCnt = genPatternConditions(sb, altCase, condCnt);
			}
		}
		for(PatternGraph neg : pattern.getNegs()) {
			condCnt = genPatternConditions(sb, neg, condCnt);
		}
		return condCnt;
	}

	private int genConditions(StringBuffer sb, Collection<Expression> conditions, int condCnt) {
		for(Expression expr : conditions) {
			Set<Node> nodes = new LinkedHashSet<Node>();
			Set<Edge> edges = new LinkedHashSet<Edge>();
			expr.collectNodesnEdges(nodes, edges);
			sb.append("\t\tpublic static bool Condition_" + condCnt + "(");
			genSet(sb, nodes, "LGSPNode node_", "", false);
			if(!nodes.isEmpty() && !edges.isEmpty())
				sb.append(", ");
			genSet(sb, edges, "LGSPEdge edge_", "", false);
			sb.append(")\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn ");
			genExpression(sb, expr);
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
			for(PatternGraph altCase : alt.getAlternativeCases()) {
				max = computePriosMax(max, altCase);
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

	protected void genQualAccess(StringBuffer sb, Qualification qual) {
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		genQualAccess(sb, owner, member);
	}

	protected void genQualAccess(StringBuffer sb, Entity owner, Entity member) {
		sb.append("((I" + (owner instanceof Node ? "Node" : "Edge") + "_" +
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
}

