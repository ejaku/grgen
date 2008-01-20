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
 * @author Moritz Kroll
 * @version $Id$
 */

package de.unika.ipd.grgen.be.Csharp;

import de.unika.ipd.grgen.ir.*;
import java.util.*;

public class ActionsGen extends CSharpBase {
	public ActionsGen(SearchPlanBackend2 backend) {
		be = backend;
	}

	/**
	 * Generates the subpatterns and actions sourcecode for this unit.
	 */
	public void genRulesAndSubpatterns() {
		StringBuffer sb = new StringBuffer();
		String filename = formatIdentifiable(be.unit) + "Actions_intermediate.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using System.Text;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("using de.unika.ipd.grGen.lgsp;\n");
		sb.append("using de.unika.ipd.grGen.models." + formatIdentifiable(be.unit) + ";\n");
		sb.append("\n");
		sb.append("namespace de.unika.ipd.grGen.actions." + formatIdentifiable(be.unit) + "\n");
		sb.append("{\n");

		for(Action action : be.patternMap.keySet())
			if(action instanceof MatchingAction)
				genPattern(sb, (MatchingAction) action);
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
	private void genPattern(StringBuffer sb, MatchingAction action) {
		String actionName = formatIdentifiable(action);

		sb.append("\tpublic class Pattern_" + actionName + " : LGSPRulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static Pattern_" + actionName + " instance = null;\n"); //new Rule_" + actionName + "();\n");
		sb.append("\t\tpublic static Pattern_" + actionName + " Instance { get { if (instance==null) instance = new Pattern_" + actionName + "(); return instance; } }\n");
		sb.append("\n");
		genTypeCondition(sb, action);
		sb.append("\n");
		genRuleOrSubpatternInit(sb, action, false);
		sb.append("\n");
		genActionConditions(sb, action);
		sb.append("\n");
		if(action instanceof Rule) {
			Rule rule = (Rule) action;
			inRewriteModify = true;
			genRuleModify(sb, rule, true);
			sb.append("\n");
			genRuleModify(sb, rule, false);
			inRewriteModify = false;

			genAddedGraphElementsArray(sb, true, newNodes);
			genAddedGraphElementsArray(sb, false, newEdges);

			genEmit(sb, rule, false);
		}
		else
			throw new IllegalArgumentException("Unknown action type: " + action);

		sb.append("\t}\n");
		sb.append("\n");

		genSchedule(sb, action, false);
	}

	/**
	 * Generates the actions sourcecode for the given matching-action
	 */
	private void genRule(StringBuffer sb, MatchingAction action) {
		String actionName = formatIdentifiable(action);

		sb.append("\tpublic class Rule_" + actionName + " : LGSPRulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static Rule_" + actionName + " instance = null;\n"); //new Rule_" + actionName + "();\n");
		sb.append("\t\tpublic static Rule_" + actionName + " Instance { get { if (instance==null) instance = new Rule_" + actionName + "(); return instance; } }\n");
		sb.append("\n");
		genTypeCondition(sb, action);
		sb.append("\n");
		genRuleOrSubpatternInit(sb, action, true);
		sb.append("\n");
		genActionConditions(sb, action);
		sb.append("\n");
		if(action instanceof Rule) {
			Rule rule = (Rule) action;
			inRewriteModify = true;
			genRuleModify(sb, rule, true);
			sb.append("\n");
			genRuleModify(sb, rule, false);
			inRewriteModify = false;

			genAddedGraphElementsArray(sb, true, newNodes);
			genAddedGraphElementsArray(sb, false, newEdges);

			genEmit(sb, rule, true);
		}
		else if(action instanceof Test) {
			sb.append("\t\tpublic override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)\n");
			sb.append("\t\t{  // test does not have modifications\n");
			sb.append("\t\t\treturn EmptyReturnElements;\n");
			sb.append("\t\t}\n");

			sb.append("\t\tpublic override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)\n");
			sb.append("\t\t{  // test does not have modifications\n");
			sb.append("\t\t\treturn EmptyReturnElements;\n");
			sb.append("\t\t}\n");

			sb.append("\t\tprivate static String[] addedNodeNames = new String[] {};\n");
			sb.append("\t\tpublic override String[] AddedNodeNames { get { return addedNodeNames; } }\n");
			sb.append("\t\tprivate static String[] addedEdgeNames = new String[] {};\n");
			sb.append("\t\tpublic override String[] AddedEdgeNames { get { return addedEdgeNames; } }\n");
		}
		else
			throw new IllegalArgumentException("Unknown action type: " + action);

		sb.append("\t}\n");
		sb.append("\n");

		genSchedule(sb, action, true);
	}

	////////////////////////////////
	// Type conditions generation //
	////////////////////////////////

	private void genTypeCondition(StringBuffer sb, MatchingAction action) {
		genAllowedTypeArrays(sb, action.getPattern(), null, -1);
		int i = 0;
		for(PatternGraph neg : action.getNegs())
			genAllowedTypeArrays(sb, neg, action.getPattern(), i++);
	}

	private void genAllowedTypeArrays(StringBuffer sb, PatternGraph pattern, PatternGraph outer, int negCount) {
		genAllowedNodeTypeArrays(sb, pattern, outer, negCount);
		genAllowedEdgeTypeArrays(sb, pattern, outer, negCount);
	}

	private void genAllowedNodeTypeArrays(StringBuffer sb, PatternGraph pattern, PatternGraph outer, int negCount) {
		StringBuilder aux = new StringBuilder();
		for(Node node : pattern.getNodes()) {
			if(outer!=null && outer.getNodes().contains(node))
				continue;
			sb.append("\t\tpublic static NodeType[] " + formatEntity(node, outer, negCount) + "_AllowedTypes = ");
			aux.append("\t\tpublic static bool[] " + formatEntity(node, outer, negCount) + "_IsAllowedType = ");
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
		}
		sb.append(aux);
	}

	private void genAllowedEdgeTypeArrays(StringBuffer sb, PatternGraph pattern, PatternGraph outer, int negCount) {
		StringBuilder aux = new StringBuilder();
		for(Edge edge : pattern.getEdges()) {
			if(outer!=null && outer.getEdges().contains(edge))
				continue;
			sb.append("\t\tpublic static EdgeType[] " + formatEntity(edge, outer, negCount) + "_AllowedTypes = ");
			aux.append("\t\tpublic static bool[] " + formatEntity(edge, outer, negCount) + "_IsAllowedType = ");
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
		}
		sb.append(aux);
	}

	//////////////////////////////
	// Rule/Subpattern metadata generation //
	//////////////////////////////

	private void genRuleOrSubpatternInit(StringBuffer sb, MatchingAction action, boolean isRule) {
		int i = 0;
		sb.append("\t\tpublic enum NodeNums { ");
		for(Node node : action.getPattern().getNodes())
			if(i++ == 0)
				sb.append("@" + formatIdentifiable(node) + "  = 1, ");
			else
				sb.append("@" + formatIdentifiable(node) + ", ");
		sb.append("};\n");

		i = 0;
		sb.append("\t\tpublic enum EdgeNums { ");
		for(Edge edge : action.getPattern().getEdges())
			if(i++ == 0)
				sb.append("@" + formatIdentifiable(edge) + " = 1, ");
			else
				sb.append("@" + formatIdentifiable(edge) + ", ");
		sb.append("};\n");
		sb.append("\n");

		if(isRule) {
			sb.append("\t\tprivate Rule_" + formatIdentifiable(action) + "()\n");
			sb.append("\t\t{\n");
		} else {
			sb.append("\t\tprivate Pattern_" + formatIdentifiable(action) + "()\n");
			sb.append("\t\t{\n");
		}
		
		PatternGraph pattern = action.getPattern();
		List<Entity> parameters = action.getParameters();
		int condCnt = genPatternGraph(sb, null, pattern, "patternGraph", 0, -1, parameters);
		sb.append("\n");

		i = 0;
		for(PatternGraph neg : action.getNegs()) {
			String negName = "negPattern_" + i++;
			sb.append("\t\t\tPatternGraph " + negName + ";\n");
			sb.append("\t\t\t{\n");
			condCnt = genPatternGraph(sb, pattern, neg, negName, condCnt, i-1, parameters);
			sb.append("\t\t\t}\n\n");
		}

		sb.append("\t\t\tnegativePatternGraphs = new PatternGraph[] {");
		for(i = 0; i < action.getNegs().size(); i++)
			sb.append("negPattern_" + i + ", ");
		sb.append("};\n");

		genRuleParamResult(sb, action);

		sb.append("\t\t}\n");
	}

	private int genPatternGraph(StringBuffer sb, PatternGraph outer, PatternGraph pattern, String pattern_name,
			int condCntInit, int negCount, List<Entity> parameters) {
		boolean isNeg = outer != null;
		String additional_parameters = isNeg?"PatternElementType.NegElement":"PatternElementType.Normal";

		for(Node node : pattern.getNodes()) {
			if(outer != null && outer.hasNode(node))
				continue;
			sb.append("\t\t\tPatternNode " + formatEntity(node, outer, negCount) + " = new PatternNode(");
			sb.append("(int) NodeTypes.@" + formatIdentifiable(node.getType()) + ", \"" + formatEntity(node, outer, negCount) + "\"");
			sb.append(", " + formatEntity(node, outer, negCount) + "_AllowedTypes, ");
			sb.append(formatEntity(node, outer, negCount) + "_IsAllowedType, ");
			sb.append(parameters.contains(node)?"PatternElementType.Preset":additional_parameters);
			sb.append(", " + parameters.indexOf(node) + ");\n");
		}
		for(Edge edge : pattern.getEdges()) {
			if(outer != null && outer.hasEdge(edge))
				continue;
			sb.append("\t\t\tPatternEdge " + formatEntity(edge, outer, negCount) + " = new PatternEdge(");
			sb.append(pattern.getSource(edge)!=null?formatEntity(pattern.getSource(edge), outer, negCount):"null");
			sb.append(", ");
			sb.append(pattern.getTarget(edge)!=null?formatEntity(pattern.getTarget(edge), outer, negCount):"null");
			sb.append(", (int) EdgeTypes.@" + formatIdentifiable(edge.getType()) + ", \"" + formatEntity(edge, outer, negCount) + "\"");
			sb.append(", " + formatEntity(edge, outer, negCount) + "_AllowedTypes, ");
			sb.append(formatEntity(edge, outer, negCount) + "_IsAllowedType, ");
			sb.append(parameters.contains(edge)?"PatternElementType.Preset":additional_parameters);
			sb.append(", " + parameters.indexOf(edge) + ");\n");
		}

		int condCnt = condCntInit;
		for(Expression expr : pattern.getConditions()) {
			Set<Node> nodes = new LinkedHashSet<Node>();
			Set<Edge> edges = new LinkedHashSet<Edge>();
			expr.collectNodesnEdges(nodes, edges);
			sb.append("\t\t\tCondition cond_" + condCnt + " = new Condition(" + condCnt + ", new String[] ");
			genEntitySet(sb, nodes, "\"", "\"", true, outer, negCount);
			sb.append(", new String[] ");
			genEntitySet(sb, edges, "\"", "\"", true, outer, negCount);
			sb.append(");\n");
			condCnt++;
		}

		sb.append("\t\t\t" + pattern_name + " = new PatternGraph(\n");

		sb.append("\t\t\t\tnew PatternNode[] ");
		genEntitySet(sb, pattern.getNodes(), "", "", true, outer, negCount);
		sb.append(", \n");

		sb.append("\t\t\t\tnew PatternEdge[] ");
		genEntitySet(sb, pattern.getEdges(), "", "", true, outer, negCount);
		sb.append(", \n");

		sb.append("\t\t\t\tnew Condition[] { ");
		condCnt = condCntInit;
		for(Expression expr : pattern.getConditions()){
			sb.append("cond_" + condCnt++ + ", ");
		}
		sb.append("},\n");

		sb.append("\t\t\t\tnew bool[" + pattern.getNodes().size() + ", " + pattern.getNodes().size() + "] ");
		if(pattern.getNodes().size() > 0) {
			sb.append("{\n");
			for(Node node1 : pattern.getNodes()) {
				sb.append("\t\t\t\t\t{ ");
				for(Node node2 : pattern.getNodes())
					if(pattern.isHomomorphic(node1,node2))
						sb.append("true, ");
					else
						sb.append("false, ");
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
				for(Edge edge2 : pattern.getEdges())
					if(pattern.isHomomorphic(edge1,edge2))
						sb.append("true, ");
					else
						sb.append("false, ");
				sb.append("},\n");
			}
			sb.append("\t\t\t\t}");
		}
		sb.append(",\n");

		sb.append("\t\t\t\tnew bool[] {");
		if(pattern.getNodes().size() > 0) {
			sb.append("\n\t\t\t\t\t");
			for(Node node : pattern.getNodes()) {
				sb.append(pattern.isHomToAll(node));
				sb.append(", ");
			}
		}
		sb.append("},\n");

		sb.append("\t\t\t\tnew bool[] {");
		if(pattern.getEdges().size() > 0) {
			sb.append("\n\t\t\t\t\t");
			for(Edge edge : pattern.getEdges()) {
				sb.append(pattern.isHomToAll(edge));
				sb.append(", ");
			}
		}
		sb.append("},\n");

		sb.append("\t\t\t\tnew bool[] {");
		if(pattern.getNodes().size() > 0) {
			sb.append("\n\t\t\t\t\t");
			for(Node node : pattern.getNodes()) {
				sb.append(pattern.isIsoToAll(node));
				sb.append(", ");
			}
		}
		sb.append("},\n");

		sb.append("\t\t\t\tnew bool[] {");
		if(pattern.getEdges().size() > 0) {
			sb.append("\n\t\t\t\t\t");
			for(Edge edge : pattern.getEdges()) {
				sb.append(pattern.isIsoToAll(edge));
				sb.append(", ");
			}
		}
		sb.append("}\n");

		sb.append("\t\t\t);\n");

		return condCnt;
	}

	private void genRuleParamResult(StringBuffer sb, Action action) {
		sb.append("\t\t\tinputs = new GrGenType[] { ");
		if(action instanceof MatchingAction)
			for(Entity ent : ((MatchingAction)action).getParameters())
				sb.append(formatTypeClass(ent.getType()) + ".typeVar, ");
		sb.append("};\n");

		sb.append("\t\t\toutputs = new GrGenType[] { ");
		if(action instanceof MatchingAction)
			for(Entity ent : ((MatchingAction)action).getReturns())
				sb.append(formatTypeClass(ent.getType()) + ".typeVar, ");
		sb.append("};\n");
	}

	private void genAddedGraphElementsArray(StringBuffer sb, boolean isNode, Collection<? extends GraphEntity> set) {
		String NodesOrEdges = isNode?"Node":"Edge";
		sb.append("\t\tprivate static String[] added" + NodesOrEdges + "Names = new String[] ");
		genSet(sb, set, "\"", "\"", true);
		sb.append(";\n");
		sb.append("\t\tpublic override String[] Added" + NodesOrEdges
				+ "Names { get { return added" + NodesOrEdges + "Names; } }\n");
	}

	private void genEmit(StringBuffer sb, Rule rule, boolean isRule) {
		String actionName = formatIdentifiable(rule);
		if(isRule) {
			sb.append("#if INITIAL_WARMUP\t\t// GrGen emit statement section: Rule_" + actionName + "\n");
		} else {
			sb.append("#if INITIAL_WARMUP\t\t// GrGen emit statement section: Pattern_" + actionName + "\n");			
		}
		int xgrsID = 0;
		for(Emit emit : ((PatternGraph) rule.getRight()).getEmits()) {
			for(IR arg : emit.getArguments()) {
				if(!(arg instanceof XGRS)) continue;
				XGRS xgrs = (XGRS) arg;
				sb.append("\t\tpublic static LGSPXGRSInfo XGRSInfo_" + xgrsID + " = new LGSPXGRSInfo(new String[] {");
				for(GraphEntity param : xgrs.getArguments()) {
					sb.append("\"" + param.getIdent() + "\", ");
				}
				sb.append("},\n");
				sb.append("\t\t\t\"" + xgrs.getXGRSString() + "\");\n");
				sb.append("\t\tprivate void ApplyXGRS_" + xgrsID++ + "(LGSPGraph graph");
				for(GraphEntity param : xgrs.getArguments()) {
					sb.append(", IGraphElement var_");
					sb.append(param.getIdent());
				}
				sb.append(") {}\n");
			}
		}
		sb.append("#endif\n");
	}

	private void genSchedule(StringBuffer sb, MatchingAction action, boolean isRule) {
		String actionName = formatIdentifiable(action);
		sb.append("#if INITIAL_WARMUP\n");
		sb.append("\tpublic class Schedule_" + actionName + " : LGSPStaticScheduleInfo\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic Schedule_" + actionName + "()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tActionName = \"" + actionName + "\";\n");
		if(isRule) {
			sb.append("\t\t\tthis.RulePattern = Rule_" + actionName + ".Instance;\n");
		} else {
			sb.append("\t\t\tthis.RulePattern = Pattern_" + actionName + ".Instance;\n");
		}
		genPrios(action, sb);
		sb.append("\t\t}\n");
		sb.append("\t}\n");
		sb.append("#endif\n");
		sb.append("\n");
	}

	//////////////////////////
	// Condition generation //
	//////////////////////////

	private void genActionConditions(StringBuffer sb, MatchingAction action) {
		int condCnt = genConditions(sb, action.getPattern().getConditions(), 0);
		for(PatternGraph neg : action.getNegs())
			condCnt = genConditions(sb, neg.getConditions(), condCnt);
	}

	private int genConditions(StringBuffer sb, Collection<Expression> conditions, int condCntInit) {
		int i = condCntInit;
		for(Expression expr : conditions) {
			Set<Node> nodes = new LinkedHashSet<Node>();
			Set<Edge> edges = new LinkedHashSet<Edge>();
			expr.collectNodesnEdges(nodes, edges);
			sb.append("\t\tpublic static bool Condition_" + i + "(");
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
			i++;
		}
		return i;
	}

	//////////////////////////////////
	// Modification part generation //
	//////////////////////////////////

	private void genRuleModify(StringBuffer sb, Rule rule, boolean reuseNodeAndEdges) {
		StringBuffer sb2 = new StringBuffer();
		StringBuffer sb3 = new StringBuffer();

		sb.append("\t\tpublic override IGraphElement[] "
				+ (reuseNodeAndEdges ? "Modify" : "ModifyNoReuse")
				+ "(LGSPGraph graph, LGSPMatch match)\n");
		sb.append("\t\t{\n");

		// Generates code in the following order:
		//  - Extract nodes from match as LGSPNode instances
		//  - Extract nodes from match or from already extracted nodes as interface instances
		//  - Extract edges from match as LGSPEdge instances
		//  - Extract edges from match or from already extracted edges as interface instances
		//  - Extract node types
		//  - Extract edge types
		//  - Create variables for used attributes of reusee
		//  - Create new nodes or reuse nodes
		//  - Create new edges or reuse edges
		//  - Retype nodes
		//  - Retype edges
		//  - Create variables for used attributes of non-reusees needed for emits
		//  - Attribute reevaluation
		//  - Remove edges
		//  - Remove nodes
		//  - Emit
		//  - Return

		newNodes = new HashSet<Node>(rule.getRight().getNodes());
		newEdges = new HashSet<Edge>(rule.getRight().getEdges());
		delNodes = new HashSet<Node>(rule.getLeft().getNodes());
		delEdges = new HashSet<Edge>(rule.getLeft().getEdges());
		reusedElements.clear();
		neededAttributes.clear();
		neededAttributesForEmit.clear();
		nodesNeededAsElements.clear();
		edgesNeededAsElements.clear();
		nodesNeededAsTypes.clear();
		edgesNeededAsTypes.clear();
		nodesNeededAsAttributes.clear();
		edgesNeededAsAttributes.clear();
		forceAttributeToVar.clear();

		commonNodes = rule.getCommonNodes();
		commonEdges = rule.getCommonEdges();

		newNodes.removeAll(commonNodes);
		newEdges.removeAll(commonEdges);
		delNodes.removeAll(commonNodes);
		delEdges.removeAll(commonEdges);

		newOrRetypedNodes = new HashSet<Node>(newNodes);
		newOrRetypedEdges = new HashSet<Edge>(newEdges);
		for(Node node : rule.getRight().getNodes()) {
			if(node.changesType())
				newOrRetypedNodes.add(node.getRetypedNode());
		}
		for(Edge edge : rule.getRight().getEdges()) {
			if(edge.changesType())
				newOrRetypedEdges.add(edge.getRetypedEdge());
		}

		for(Emit emit : ((PatternGraph) rule.getRight()).getEmits()) {
			for(IR arg : emit.getArguments()) {
				if(arg instanceof XGRS) {
					XGRS xgrs = (XGRS) arg;
					for(GraphEntity param : xgrs.getArguments()) {
						if(param instanceof Node)
							nodesNeededAsElements.add((Node) param);
						else if(param instanceof Edge)
							edgesNeededAsElements.add((Edge) param);
						else
							assert false : "XGRS argument of unknown type: " + param.getClass();
					}
				}
				else if(arg instanceof Qualification) {
					Qualification qual = (Qualification) arg;
					GraphEntity entity = (GraphEntity) qual.getOwner();
					HashSet<Entity> neededAttrs = neededAttributes.get(entity);
					if(neededAttrs == null) {
						neededAttributes.put(entity, neededAttrs = new LinkedHashSet<Entity>());
					}
					neededAttrs.add(qual.getMember());

					neededAttrs = neededAttributesForEmit.get(entity);
					if(neededAttrs == null) {
						neededAttributesForEmit.put(entity, neededAttrs = new LinkedHashSet<Entity>());
					}
					neededAttrs.add(qual.getMember());
				}
				else if(arg instanceof Expression) {
					collectNeededAttributes((Expression) arg);
				}
			}
		}

		// Copy all entries generated by collectNeededAttributes for emit stuff
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : neededAttributes.entrySet()) {
			HashSet<Entity> neededAttrs = entry.getValue();
			HashSet<Entity> neededAttrsForEmit = neededAttributesForEmit.get(entry.getKey());
			if(neededAttrsForEmit == null) {
				neededAttributesForEmit.put(entry.getKey(), neededAttrsForEmit = new LinkedHashSet<Entity>());
			}
			neededAttrsForEmit.addAll(neededAttrs);
		}

		for(Assignment ass : rule.getEvals()) {
			collectNeededAttributes(ass.getExpression());
		}


		// new nodes
		genRewriteNewNodes(sb2, reuseNodeAndEdges);

		// new edges
		genRewriteNewEdges(sb2, rule, reuseNodeAndEdges);

		// attribute re-calc
		genEvals(sb3, rule);

		// node type changes
		for(Node node : rule.getRight().getNodes()) {
			if(node.changesType()) {
				String new_type;
				RetypedNode rnode = node.getRetypedNode();

				if(rnode.inheritsType()) {
					new_type = formatEntity(rnode.getTypeof()) + "_type";
					nodesNeededAsElements.add(rnode.getTypeof());
					nodesNeededAsTypes.add(rnode.getTypeof());
				} else {
					new_type = formatTypeClass(rnode.getType()) + ".typeVar";
				}

				nodesNeededAsElements.add(node);
				sb2.append("\t\t\t" + formatNodeAssign(rnode, nodesNeededAsAttributes)
						+ "graph.Retype(" + formatEntity(node) + ", " + new_type + ");\n");
			}
		}

		// edge type changes
		for(Edge edge : rule.getRight().getEdges()) {
			if(edge.changesType()) {
				String new_type;
				RetypedEdge redge = edge.getRetypedEdge();

				if(redge.inheritsType()) {
					new_type = formatEntity(redge.getTypeof()) + "_type";
					edgesNeededAsElements.add(redge.getTypeof());
					edgesNeededAsTypes.add(redge.getTypeof());
				} else {
					new_type = formatTypeClass(redge.getType()) + ".typeVar";
				}

				edgesNeededAsElements.add(edge);
				sb2.append("\t\t\t" + formatEdgeAssign(redge, edgesNeededAsAttributes)
						+ "graph.Retype(" + formatEntity(edge) + ", " + new_type + ");\n");
			}
		}

		// remove edges
		for(Edge edge : delEdges) {
			edgesNeededAsElements.add(edge);
			sb3.append("\t\t\tgraph.Remove(" + formatEntity(edge) + ");\n");
		}

		// remove nodes
		for(Node node : delNodes) {
			nodesNeededAsElements.add(node);
			sb3.append("\t\t\tgraph.RemoveEdges(" + formatEntity(node) + ");\n");
			sb3.append("\t\t\tgraph.Remove(" + formatEntity(node) + ");\n");
		}

		// emits
		int xgrsID = 0;
		for(Emit emit : ((PatternGraph) rule.getRight()).getEmits()) {
			for(IR arg : emit.getArguments()) {
				if(arg instanceof Constant) {
					Constant constant = (Constant) arg;
					sb3.append("\t\t\tConsole.Write(\"" + constant.getValue() + "\");\n");
				}
				else if(arg instanceof Qualification) {
					Qualification qual = (Qualification) arg;
					sb3.append("\t\t\tConsole.Write(");
					genQualAccess(sb3, qual);
					sb3.append(");\n");
				}
				else if(arg instanceof XGRS) {
					XGRS xgrs = (XGRS) arg;
					sb3.append("\t\t\tApplyXGRS_" + xgrsID++ + "(graph");
					boolean isFirst = true;
					for(GraphEntity param : xgrs.getArguments()) {
						sb3.append(", ");
						sb3.append(formatEntity(param));
					}
					sb3.append(");\n");
				}
				else if(arg instanceof Expression) {
					sb3.append("\t\t\tConsole.Write(");
					genExpression(sb3, (Expression) arg);
					sb3.append(");\n");
				}
				else assert false : "Invalid emit argument";
			}
		}

		// return parameter (output)
		if(rule.getReturns().isEmpty())
			sb3.append("\t\t\treturn EmptyReturnElements;\n");
		else {
			sb3.append("\t\t\treturn new IGraphElement[] { ");
			for(Entity ent : rule.getReturns()) {
				if(ent instanceof Node)
					nodesNeededAsElements.add((Node)ent);
				else if(ent instanceof Edge)
					edgesNeededAsElements.add((Edge)ent);
				else
					throw new IllegalArgumentException("unknown Entity: " + ent);
				sb3.append(formatEntity(ent) + ", ");
			}
			sb3.append("};\n");
		}

		sb3.append("\t\t}\n");

		// nodes/edges needed from match, but not the new nodes
		nodesNeededAsElements.removeAll(newNodes);
		nodesNeededAsAttributes.removeAll(newNodes);
		edgesNeededAsElements.removeAll(newEdges);
		edgesNeededAsAttributes.removeAll(newEdges);

		// extract nodes/edges from match
		for(Node node : nodesNeededAsElements) {
			if(!node.isRetyped()) {
				sb.append("\t\t\tLGSPNode " + formatEntity(node)
						+ " = match.Nodes[ (int) NodeNums.@"
						+ formatIdentifiable(node) + " - 1 ];\n");
			}
		}
		for(Node node : nodesNeededAsAttributes) {
			if(!node.isRetyped()) {
				sb.append("\t\t\t" + formatCastedAssign(node.getType(), "I", "i" + formatEntity(node)));
				if(nodesNeededAsElements.contains(node))
					sb.append(formatEntity(node) + ";\n");
				else
					sb.append("match.Nodes[ (int) NodeNums.@" + formatIdentifiable(node) + " - 1 ];\n");
			}
		}
		for(Edge edge : edgesNeededAsElements) {
			if(!edge.isRetyped()) {
				sb.append("\t\t\tLGSPEdge " + formatEntity(edge)
						+ " = match.Edges[ (int) EdgeNums.@"
						+ formatIdentifiable(edge) + " - 1 ];\n");
			}
		}
		for(Edge edge : edgesNeededAsAttributes) {
			if(!edge.isRetyped()) {
				sb.append("\t\t\t" + formatCastedAssign(edge.getType(), "I", "i" + formatEntity(edge)));
				if(edgesNeededAsElements.contains(edge))
					sb.append(formatEntity(edge) + ";\n");
				else
					sb.append("match.Edges[ (int) EdgeNums.@" + formatIdentifiable(edge) + " - 1 ];\n");
			}
		}

		for(Node node : nodesNeededAsTypes) {
			String name = formatEntity(node);
			sb.append("\t\t\tNodeType " + name + "_type = " + name + ".type;\n");
		}
		for(Edge edge : edgesNeededAsTypes) {
			String name = formatEntity(edge);
			sb.append("\t\t\tEdgeType " + name + "_type = " + name + ".type;\n");
		}

		// create variables for used attributes of reused elements
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : neededAttributes.entrySet()) {
			if(!reusedElements.contains(entry.getKey())) continue;

			String grEntName = formatEntity(entry.getKey());
			for(Entity entity : entry.getValue()) {
				String attrName = formatIdentifiable(entity);
				genVariable(sb, grEntName, entity);
				sb.append(" = i" + grEntName + ".@" + attrName + ";\n");
			}
		}

		// new nodes/edges (re-use) and retype nodes/edges
		sb.append(sb2);

		// create variables for used attributes of non-reusees needed for emits
		for(Map.Entry<GraphEntity, HashSet<Entity>> entry : neededAttributesForEmit.entrySet()) {
			GraphEntity owner = entry.getKey();
			if(reusedElements.contains(owner)) continue;

			String grEntName = formatEntity(owner);
			for(Entity entity : entry.getValue()) {
				genVariable(sb, grEntName, entity);
				sb.append(" = ");
				genQualAccess(sb, owner, entity);
				sb.append(";\n");

				HashSet<Entity> forcedAttrs = forceAttributeToVar.get(owner);
				if(forcedAttrs == null)
					forceAttributeToVar.put(owner, forcedAttrs = new HashSet<Entity>());
				forcedAttrs.add(entity);
			}
		}

		// attribute re-calc, remove, return
		sb.append(sb3);
	}

	/**
	 * Scans an expression for all read attributes and collects
	 * them in the neededAttributes hash map.
	 */
	private void collectNeededAttributes(Expression expr) {
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			switch(op.arity()) {
				case 3:
					collectNeededAttributes(op.getOperand(2));
					// FALLTHROUGH
				case 2:
					collectNeededAttributes(op.getOperand(1));
					// FALLTHROUGH
				case 1:
					collectNeededAttributes(op.getOperand(0));
					break;
			}
		}
		else if(expr instanceof Qualification) {
			Qualification qual = (Qualification) expr;
			GraphEntity entity = (GraphEntity) qual.getOwner();
			HashSet<Entity> neededAttrs = neededAttributes.get(entity);
			if(neededAttrs == null) {
				neededAttributes.put(entity, neededAttrs = new LinkedHashSet<Entity>());
			}
			neededAttrs.add(qual.getMember());
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			collectNeededAttributes(cast.getExpression());
		}
	}

	////////////////////////////
	// New element generation //
	////////////////////////////

	private void genRewriteNewNodes(StringBuffer sb2, boolean reuseNodeAndEdges) {
		reuseNodeAndEdges = false;							// TODO: reimplement this!!

		LinkedList<Node> tmpNewNodes = new LinkedList<Node>(newNodes);
		LinkedList<Node> tmpDelNodes = new LinkedList<Node>(delNodes);
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
		NN: for(Iterator<Node> i = tmpNewNodes.iterator(); i.hasNext();) {
			Node node = i.next();
			String type = computeGraphEntityType(node);
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
			}
			String etype = formatElementClass(node.getType());
			sb2.append("\t\t\t" + etype + " " + formatEntity(node) + " = " + etype + ".CreateNode(graph);\n");
		}
	}

	private String computeGraphEntityType(Node node) {
		String type;
		if(node.inheritsType()) {
			type = formatEntity(node.getTypeof()) + "_type";
			nodesNeededAsElements.add(node.getTypeof());
			nodesNeededAsTypes.add(node.getTypeof());
		} else {
			type = formatTypeClass(node.getType()) + ".typeVar";
		}
		return type;
	}

	private void genRewriteNewEdges(StringBuffer sb2, Rule rule, boolean reuseNodeAndEdges) {
		PatternGraph leftSide = rule.getLeft();
		Graph rightSide = rule.getRight();

		NE:	for(Edge edge : newEdges) {
			String etype = formatElementClass(edge.getType());

			Node src_node = rightSide.getSource(edge);
			Node tgt_node = rightSide.getTarget(edge);

			if( commonNodes.contains(src_node) )
				nodesNeededAsElements.add(src_node);

			if( commonNodes.contains(tgt_node) )
				nodesNeededAsElements.add(tgt_node);

			if(edge.inheritsType()) {
				edgesNeededAsElements.add(edge.getTypeof());
				edgesNeededAsTypes.add(edge.getTypeof());
			}
			else if(reuseNodeAndEdges) {
				Edge bestDelEdge = null;
				int bestPoints = -1;

				// Can we reuse a deleted edge of the same type?
				for(Edge delEdge : delEdges) {
					if(delEdge.getType() != edge.getType()) continue;

					int curPoints = 0;
					if(leftSide.getSource(delEdge) == src_node)
						curPoints++;
					if(leftSide.getTarget(delEdge) == tgt_node)
						curPoints++;
					if(curPoints > bestPoints) {
						bestPoints = curPoints;
						bestDelEdge = delEdge;
						if(curPoints == 2) break;
					}
				}

				if(bestDelEdge != null) {
					// We may be able to reuse the edge instead of deleting it!
					// This is a veritable performance optimization, as object creation is costly

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

					if(leftSide.getSource(bestDelEdge) != src_node)
						sb2.append(src + ", ");
					else
						sb2.append("null, ");

					if(leftSide.getTarget(bestDelEdge) != tgt_node)
						sb2.append(tgt + "");
					else
						sb2.append("null");

					sb2.append(");\n"
							+ "\t\t\t}\n"
							+ "\t\t\telse\n"
							+ "\t\t\t\t" + formatEntity(edge) + " = " + etype
							+ ".CreateEdge(graph, " + formatEntity(src_node)
							+ ", " + formatEntity(tgt_node) + ");\n");

					delEdges.remove(bestDelEdge); // Do not delete the edge (it is reused)
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

	//////////////////////////
	// Eval part generation //
	//////////////////////////

	private void genEvals(StringBuffer sb, Rule rule) {
		boolean def_b = false, def_i = false, def_s = false, def_f = false, def_d = false, def_o = false;
		for(Assignment ass : rule.getEvals()) {
			String varName, varType;
			Entity entity = ass.getTarget().getOwner();

			switch(ass.getTarget().getType().classify()) {
				case Type.IS_BOOLEAN:
					varName = "var_b";
					varType = def_b?"":"bool ";
					def_b = true;
					break;
				case Type.IS_INTEGER:
					varName = "var_i";
					varType = def_i?"":"int ";
					def_i = true;
					break;
				case Type.IS_FLOAT:
					varName = "var_f";
					varType = def_f?"":"float ";
					def_f = true;
					break;
				case Type.IS_DOUBLE:
					varName = "var_d";
					varType = def_d?"":"double ";
					def_d = true;
					break;
				case Type.IS_STRING:
					varName = "var_s";
					varType = def_s?"":"String ";
					def_s = true;
					break;
				case Type.IS_OBJECT:
					varName = "var_o";
					varType = def_o?"":"Object ";
					def_o = true;
					break;
				default:
					throw new IllegalArgumentException();
			}

			sb.append("\t\t\t" + varType + varName + " = ");
			if(ass.getTarget().getType() instanceof EnumType)
				sb.append("(int) ");
			genExpression(sb, ass.getExpression());
			sb.append(";\n");

			if(entity instanceof Node) {
				Node node = (Node) entity;
				nodesNeededAsElements.add(node);
				sb.append("\t\t\tgraph.ChangingNodeAttribute(" + formatEntity(node) +
						", NodeType_" + formatIdentifiable(ass.getTarget().getMember().getOwner()) +
						".AttributeType_" + formatIdentifiable(ass.getTarget().getMember()) + ", ");
				genExpression(sb, ass.getTarget());
				sb.append(", " + varName + ");\n");
			} else if(entity instanceof Edge) {
				Edge edge = (Edge) entity;
				edgesNeededAsElements.add(edge);
				sb.append("\t\t\tgraph.ChangingEdgeAttribute(" + formatEntity(edge) +
						", EdgeType_" + formatIdentifiable(ass.getTarget().getMember().getOwner()) +
						".AttributeType_" + formatIdentifiable(ass.getTarget().getMember()) + ", ");
				genExpression(sb, ass.getTarget());
				sb.append(", " + varName + ");\n");
			}

			sb.append("\t\t\t");
			genExpression(sb, ass.getTarget());
			sb.append(" = ");
			if(ass.getTarget().getType() instanceof EnumType)
				sb.append("(ENUM_" + formatIdentifiable(ass.getTarget().getType()) + ") ");
			sb.append(varName + ";\n");
		}
	}

	///////////////////////////////////////
	// Static searchplan cost generation //
	///////////////////////////////////////

	private void genPrios(MatchingAction action, StringBuffer sb) {
		double max;

		max = computePriosMax(action.getPattern().getNodes(), -1);
		max = computePriosMax(action.getPattern().getEdges(), max);
		for(PatternGraph neg : action.getNegs())
			max = computePriosMax(neg.getNodes(), max);
		for(PatternGraph neg : action.getNegs())
			max = computePriosMax(neg.getEdges(), max);

		sb.append("\t\t\tNodeCost = new float[] { ");
		genPriosNoE(sb, action.getPattern().getNodes(), max);
		sb.append(" };\n");

		sb.append("\t\t\tEdgeCost = new float[] { ");
		genPriosNoE(sb, action.getPattern().getEdges(), max);
		sb.append(" };\n");

		sb.append("\t\t\tNegNodeCost = new float[][] { ");
		for(PatternGraph neg : action.getNegs()) {
			sb.append("new float[] { ");
			genPriosNoE(sb, neg.getNodes(), max);
			sb.append("}, ");
		}
		sb.append("};\n");

		sb.append("\t\t\tNegEdgeCost = new float[][] { ");
		for(PatternGraph neg : action.getNegs()) {
			sb.append("new float[] { ");
			genPriosNoE(sb, neg.getEdges(), max);
			sb.append("}, ");
		}
		sb.append("};\n");
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

	private void genPriosNoE(StringBuffer sb, Collection<? extends Entity> nodesOrEdges, double max) {
		for(Entity noe : nodesOrEdges) {
			Object prioO = noe.getAnnotations().get("prio");

			double prio;
			if (prioO != null && prioO instanceof Integer) {
				prio = ((Integer)prioO).doubleValue();
				prio = 10.0 - (prio / max) * 9.0;
			}
			else
				prio = 5.5;

			sb.append(prio + "F, ");
		}
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
		if(inRewriteModify) {
			if(accessViaVariable((GraphEntity) owner, member)) {
				if(owner instanceof Node)
					nodesNeededAsAttributes.add((Node) owner);
				else if(owner instanceof Edge)
					edgesNeededAsAttributes.add((Edge) owner);

				sb.append("var_" + formatEntity(owner) + "_" + formatIdentifiable(member));
			}
			else {
				if(owner instanceof Node) {
					nodesNeededAsAttributes.add((Node) owner);
					if(!newOrRetypedNodes.contains(owner))		// element extracted from match?
						sb.append("i");							// yes, attributes only accessible via interface
				}
				else if(owner instanceof Edge) {
					edgesNeededAsAttributes.add((Edge) owner);
					if(!newOrRetypedEdges.contains(owner))		// element extracted from match?
						sb.append("i");							// yes, attributes only accessible via interface
				}
				else
					throw new UnsupportedOperationException("Unsupported Entity (" + owner + ")");

				sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
			}
		}
		else {
			sb.append("((I" + (owner instanceof Node ? "Node" : "Edge") + "_" +
					formatIdentifiable(owner.getType()) + ") ");
			sb.append(formatEntity(owner) + ").@" + formatIdentifiable(member));
		}
	}

	protected void genMemberAccess(StringBuffer sb, Entity member) {
		throw new UnsupportedOperationException("Member expressions not allowed in actions!");
	}

	private boolean accessViaVariable(GraphEntity elem, Entity attr) {
		if(!reusedElements.contains(elem))
		{
			HashSet<Entity> forcedAttrs = forceAttributeToVar.get(elem);
			return forcedAttrs != null && forcedAttrs.contains(attr);
		}
		HashSet<Entity> neededAttrs = neededAttributes.get(elem);
		return neededAttrs != null && neededAttrs.contains(attr);
	}

	private void genVariable(StringBuffer sb, String ownerName, Entity entity) {
		String varTypeName;
		String attrName = formatIdentifiable(entity);
		switch(entity.getType().classify()) {
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
				varTypeName = "Object";
				break;
			default:
				throw new IllegalArgumentException();
		}

		sb.append("\t\t\t" + varTypeName + " var_" + ownerName + "_" + attrName);
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private SearchPlanBackend2 be;

	private boolean inRewriteModify;

	private HashSet<Node> newNodes;
	private HashSet<Edge> newEdges;
	private HashSet<Node> delNodes;
	private HashSet<Edge> delEdges;
	private Collection<Node> commonNodes;
	private Collection<Edge> commonEdges;
	private HashSet<Node> newOrRetypedNodes;
	private HashSet<Edge> newOrRetypedEdges;
	private HashSet<GraphEntity> reusedElements = new HashSet<GraphEntity>();

	private HashMap<GraphEntity, HashSet<Entity>> neededAttributes = new LinkedHashMap<GraphEntity, HashSet<Entity>>();
	private HashMap<GraphEntity, HashSet<Entity>> neededAttributesForEmit = new LinkedHashMap<GraphEntity, HashSet<Entity>>();

	private HashSet<Node> nodesNeededAsElements = new LinkedHashSet<Node>();
	private HashSet<Edge> edgesNeededAsElements = new LinkedHashSet<Edge>();
	private HashSet<Node> nodesNeededAsAttributes = new LinkedHashSet<Node>();
	private HashSet<Edge> edgesNeededAsAttributes = new LinkedHashSet<Edge>();
	private HashSet<Node> nodesNeededAsTypes = new LinkedHashSet<Node>();
	private HashSet<Edge> edgesNeededAsTypes = new LinkedHashSet<Edge>();

	private HashMap<GraphEntity, HashSet<Entity>> forceAttributeToVar = new LinkedHashMap<GraphEntity, HashSet<Entity>>();
}



