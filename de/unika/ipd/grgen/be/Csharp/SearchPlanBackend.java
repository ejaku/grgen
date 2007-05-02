/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

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
 * A GrGen Backend which generates C# code for a search-plan-based
 * graph model impl and a frame based graph matcher
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.be.Csharp;



import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.util.Util;

import java.util.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.be.IDBase;
import java.io.File;
import java.io.PrintStream;

public class SearchPlanBackend extends IDBase implements Backend, BackendFactory {
	
	/** The unit to generate code for. */
	protected Unit unit;
	
	/** The output path as handed over by the frontend. */
	private File path;
	
	/* binary operator symbols of the C-language */
	// ATTENTION: the forst two shift operations are signed shifts
	// 		the second right shift is signed. This Backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	private String[] opSymbols = {
		null, "||", "&&", "|", "^", "&",
			"==", "!=", "<", "<=", ">", ">=", "<<", ">>", ">>", "+",
			"-", "*", "/", "%", "!", "~", "-", "(cast)"
	};
	
	
	/**
	 * Method makeEvals
	 *
	 * @param    ps                  a  PrintStream
	 *
	 */
	protected void makeEvals(PrintStream ps) {
		// TODO
	}
	
	/**
	 * Create a new backend.
	 * @return A new backend.
	 */
	public Backend getBackend() throws BackendException {
		return this;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	public void init(Unit unit, Sys system, File outputPath) {
		this.unit = unit;
		this.path = outputPath;
		path.mkdirs();
		
		makeTypes(unit);
	}
	
	/**
	 * Starts the C-code Genration of the FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {
		// Emit an include file for Makefiles
		
		System.out.println("The " + this.getClass() + " GrGen backend...");
		genModel();
		genRules();
		
		System.out.println("done!");
	}
	
	/**
	 * Write a character sequence to a file using the path set.
	 * @param filename The filename.
	 * @param cs A character sequence.
	 */
	protected final void writeFile(String filename, CharSequence cs) {
		Util.writeFile(new File(path, filename), cs, error);
	}
	
	private void genModel() {
		StringBuffer sb = new StringBuffer();
		String filename = formatIdentifiable(unit) + "Model.cs";
		
		System.out.println("  generating the "+filename+" file...");
		
		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("\n");
		sb.append("namespace de.unika.ipd.grGen.models." + formatIdentifiable(unit) + "\n");
		sb.append("{\n");
		
		System.out.println("    generating enums...");
		genModelEnum(sb);
		
		System.out.println("    generating node types...");
		sb.append("\n");
		genModelTypes(sb, nodeTypeMap.keySet(), true);
		
		System.out.println("    generating node model...");
		sb.append("\n");
		genModelModel(sb, nodeTypeMap.keySet(), true);
		
		System.out.println("    generating edge types...");
		sb.append("\n");
		genModelTypes(sb, edgeTypeMap.keySet(), false);
		
		System.out.println("    generating edge model...");
		sb.append("\n");
		genModelModel(sb, edgeTypeMap.keySet(), false);
		
		System.out.println("    generating graph model...");
		sb.append("\n");
		genModelGraph(sb, edgeTypeMap.keySet(), false);
		
		sb.append("}\n");
		
		writeFile(filename, sb);
	}
	
	private void genRules() {
		StringBuffer sb = new StringBuffer();
		String filename = formatIdentifiable(unit) + "Actions.cs";
		
		System.out.println("  generating the "+filename+" file...");
		
		
		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using System.Text;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("using de.unika.ipd.grGen.lgsp;\n");
		sb.append("using de.unika.ipd.grGen.models." + formatIdentifiable(unit) + ";\n");
		sb.append("\n");
		sb.append("#if INITIAL_WARMUP\n");
		sb.append("using de.unika.ipd.grGen.grGen;\n");
		sb.append("#endif\n");
		sb.append("\n");
		sb.append("namespace de.unika.ipd.grGen.actions." + formatIdentifiable(unit) + "\n");
		sb.append("{\n");
		
		for(Action action : actionMap.keySet())
			if(action instanceof MatchingAction)
				genRule(sb, (MatchingAction)action);
			else
				throw new IllegalArgumentException("Unknown Action: " + action);
		
		sb.append("}\n");
		
		writeFile(filename, sb);
	}
	
	private void genRule(StringBuffer sb, MatchingAction action) {
		String actionName = formatIdentifiable(action);
		
		sb.append("\tpublic class Rule_" + actionName + " : RulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static Rule_" + actionName + " instance = null;\n"); //new Rule_" + actionName + "();\n");
		sb.append("\t\tpublic static Rule_" + actionName + " Instance { get { if (instance==null) instance = new Rule_" + actionName + "(); return instance; } }\n");
		sb.append("\n");
		genTypeCondition(sb, action);
		sb.append("\n");
		genRuleInit(sb, action);
		sb.append("\n");
		genActionConditions(sb, action);
		sb.append("\n");
		if(action instanceof Rule)
			genRuleModify(sb, (Rule)action);
		else
			throw new IllegalArgumentException("NYI. We cannot handle this type upto now! " + action);
		sb.append("\t}\n");
		sb.append("\n");
		
		sb.append("#if INITIAL_WARMUP\n");
		sb.append("\tpublic class Schedule_" + actionName + " : Schedule\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic Schedule_" + actionName + "()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tActionName = \"" + actionName + "\";\n");
		sb.append("\t\t\tRulePattern = Rule_" + actionName + ".Instance;\n");
		genPrios(action, sb);
		sb.append("\t\t}\n");
		sb.append("\t}\n");
		sb.append("#endif\n");
		sb.append("\n");
	}
	
	private void genRuleInit(StringBuffer sb, MatchingAction action) {
		int i = 0;
		sb.append("\t\tpublic enum NodeNums { ");
		for(Node node : action.getPattern().getNodes())
			if(i++ == 0)
				sb.append(formatIdentifiable(node) + "  = 1, ");
			else
				sb.append(formatIdentifiable(node) + ", ");
		sb.append("};\n");
		
		i = 0;
		sb.append("\t\tpublic enum EdgeNums { ");
		for(Edge edge : action.getPattern().getEdges())
			if(i++ == 0)
				sb.append(formatIdentifiable(edge) + " = 1, ");
			else
				sb.append(formatIdentifiable(edge) + ", ");
		sb.append("};\n");
		sb.append("\n");
		
		sb.append("\t\tprivate Rule_" + formatIdentifiable(action) + "()\n");
		sb.append("\t\t{\n");
		
		PatternGraph pattern = action.getPattern();
		List<Entity> parameters = action.getParameters();
		int condCnt = genPatternGraph(sb, null, pattern, "PatternGraph", 0, -1, parameters);
		sb.append("\n");
		
		i = 0;
		for(PatternGraph neg : action.getNegs()) {
			String negName = "negPattern_" + i++;
			sb.append("\t\t\tPatternGraph " + negName + ";\n");
			sb.append("\t\t\t{\n");
			condCnt = genPatternGraph(sb, pattern, neg, negName, condCnt, i-1, parameters);
			sb.append("\t\t\t}\n\n");
		}
		
		sb.append("\t\t\tNegativePatternGraphs = new PatternGraph[] {");
		for(i = 0; i < action.getNegs().size(); i++)
			sb.append("negPattern_" + i + ", ");
		sb.append("};\n");
		
		genRuleParamResult(sb, action);
		
		sb.append("\t\t}\n");
	}
	
	private void genRuleParamResult(StringBuffer sb, Action action) {
		sb.append("\t\t\tInputs = new IType[] { ");
		if(action instanceof MatchingAction)
			for(Entity ent : ((MatchingAction)action).getParameters())
				sb.append(formatType(ent.getType()) + ".typeVar, ");
		sb.append("};\n");
		
		sb.append("\t\t\tOutputs = new IType[] { ");
		if(action instanceof MatchingAction)
			for(Entity ent : ((MatchingAction)action).getReturns())
				sb.append(formatType(ent.getType()) + ".typeVar, ");
		sb.append("};\n");
	}
	
	
	private void genActionConditions(StringBuffer sb, MatchingAction action) {
		int condCnt = genConditions(sb, action.getPattern().getConditions(), 0);
		for(PatternGraph neg : action.getNegs())
			condCnt = genConditions(sb, neg.getConditions(), condCnt);
	}
	
	private int genConditions(StringBuffer sb, Collection<Expression> conditions, int condCntInit) {
		int i = condCntInit;
		for(Expression expr : conditions) {
			Set<Node> nodes = new HashSet<Node>();
			Set<Edge> edges = new HashSet<Edge>();
			collectNodesnEdges(nodes, edges, expr);
			sb.append("\t\tpublic static bool Condition_" + i + "(");
			genSet(sb, nodes, "LGSPNode node_", "", false);
			if(!nodes.isEmpty() && !edges.isEmpty())
				sb.append(", ");
			genSet(sb, edges, "LGSPEdge edge_", "", false);
			sb.append(")\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn ");
			genConditionEval(sb, expr, null, null);
			sb.append(";\n");
			sb.append("\t\t}\n");
			i++;
		}
		return i;
	}
	
	private void genRuleModify(StringBuffer sb, Rule rule) {
		StringBuffer sb2 = new StringBuffer();
		
		sb.append("\t\tpublic override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)\n");
		sb.append("\t\t{\n");
		
		Collection<Node> newNodes = new HashSet<Node>(rule.getRight().getNodes());
		Collection<Edge> newEdges = new HashSet<Edge>(rule.getRight().getEdges());
		Collection<Node> delNodes = new HashSet<Node>(rule.getLeft().getNodes());
		Collection<Edge> delEdges = new HashSet<Edge>(rule.getLeft().getEdges());
		Collection<Node> extractNodeFromMatch = new HashSet<Node>();
		Collection<Edge> extractEdgeFromMatch = new HashSet<Edge>();
		
		newNodes.removeAll(rule.getCommonNodes());
		newEdges.removeAll(rule.getCommonEdges());
		delNodes.removeAll(rule.getCommonNodes());
		delEdges.removeAll(rule.getCommonEdges());
		
		
		
		// new nodes
		for(Node node : newNodes) {
			String type;
			
			if(node.inheritsType()) {
				type = formatEntity(node.getTypeof()) + ".type.typeVar";
				extractNodeFromMatch.add(node.getTypeof());
			} else {
				type = formatType(node.getType()) + ".typeVar";
			}
			sb2.append(
				"\t\t\tLGSPNode " + formatEntity(node) + " = graph.AddNode(" +
					type + ");\n"
			);
		}
		
		// new edges
		NE:	for(Edge edge : newEdges) {
			Node src_node = rule.getRight().getSource(edge);
			Node tgt_node = rule.getRight().getTarget(edge);
			
			if( rule.getCommonNodes().contains(src_node) )
				extractNodeFromMatch.add(src_node);
			
			if( rule.getCommonNodes().contains(tgt_node) )
				extractNodeFromMatch.add(tgt_node);
			
			String type;
			
			if(edge.inheritsType()) {
				type = formatEntity(edge.getTypeof()) + ".type.typeVar";
				extractEdgeFromMatch.add(edge.getTypeof());
			} else {
				type = formatType(edge.getType()) + ".typeVar";
			}
			
			// Can we reuse the edge
			for(Edge delEdge : new HashSet<Edge>(delEdges))
				if(edge.getEdgeType().getAllMembers().isEmpty() && delEdge.getEdgeType().getAllMembers().isEmpty()) {
					// We can reuse the edge instead of deleting it!
					// This is a veritable performance optimization, as object creation is costly
					
					String de = formatEntity(delEdge);
					String src = formatEntity(src_node);
					String tgt = formatEntity(tgt_node);
					
					sb2.append("\t\t\t// re-using " + de + " as " + formatEntity(edge) + "\n");
					
					if(delEdge.getType() != edge.getType())
						sb2.append("\t\t\tgraph.RetypeEdge(" + de + ", " + type + ");\n");
					if(rule.getLeft().getSource(delEdge)!=src_node) {
						if(rule.getLeft().getTarget(delEdge)!=tgt_node)
							sb2.append("\t\t\tgraph.RerouteEdge(" + de + ", " + src + ", " + tgt + ");\n");
						else
							sb2.append("\t\t\tgraph.RerouteSource(" + de + ", " + src + ");\n");
					}
					else if(rule.getLeft().getTarget(delEdge)!=tgt_node)
						sb2.append("\t\t\tgraph.RerouteTarget(" + de + ", " + tgt + ");\n");
					
					delEdges.remove(delEdge); // Do not delete the edge (it is reused)
					extractEdgeFromMatch.add(delEdge);
					continue NE;
				}
			
			// Create the edge
			sb2.append(
				"\t\t\tLGSPEdge " + formatEntity(edge) + " = graph.AddEdge(" +
					type + ", " + formatEntity(src_node) + ", " + formatEntity(tgt_node) + ");\n"
			);
		}
		
		// node type changes
		for(Node node : rule.getRight().getNodes())
			if(node.changesType()) {
				String new_type;
				RetypedNode rnode = node.getRetypedNode();
				
				if(rnode.inheritsType()) {
					new_type = formatEntity(rnode.getTypeof()) + ".type.typeVar";
					extractNodeFromMatch.add(rnode.getTypeof());
				} else {
					new_type = formatType(rnode.getType()) + ".typeVar";
				}
				
				extractNodeFromMatch.add(node);
				sb2.append("\t\t\tINode_" + formatIdentifiable(node.getType()));
				sb2.append(" " + formatEntity(node) + "_attributes = ");
				sb2.append("(INode_" + formatIdentifiable(node.getType()) + ") ");
				sb2.append("graph.SetNodeType(" + formatEntity(node) + ", " + new_type + ");\n");
				// TODO fix attribute access for all nodes in evals
			}
		
		// edge type changes
		for(Edge edge : rule.getRight().getEdges())
			if(edge.changesType()) {
				String new_type;
				RetypedEdge redge = edge.getRetypedEdge();
				
				if(edge.inheritsType()) {
					new_type = formatEntity(redge.getTypeof()) + ".type.typeVar";
					extractEdgeFromMatch.add(redge.getTypeof());
				} else {
					new_type = formatType(redge.getType()) + ".typeVar";
				}
				
				extractEdgeFromMatch.add(edge);
				sb2.append("\t\t\tIEdge_" + formatIdentifiable(edge.getType()));
				sb2.append(" " + formatEntity(edge) + "_attributes = ");
				sb2.append("(IEdge_" + formatIdentifiable(edge.getType()) + ") ");
				sb2.append("graph.SetEdgeType(" + formatEntity(edge) + ", " + new_type + ");\n");
				// TODO fix attribute access for all edges in evals
			}
		
		// attribute re-calc
		genEvals(sb2, rule, extractNodeFromMatch, extractEdgeFromMatch);
		
		// remove edges
		for(Edge edge : delEdges) {
			extractEdgeFromMatch.add(edge);
			sb2.append("\t\t\tgraph.Remove(" + formatEntity(edge) + ");\n");
		}
		
		// remove nodes
		for(Node node : delNodes) {
			extractNodeFromMatch.add(node);
			sb2.append("\t\t\tgraph.RemoveEdges(" + formatEntity(node) + ");\n");
			sb2.append("\t\t\tgraph.Remove(" + formatEntity(node) + ");\n");
		}
		
		// return parameter (output)
		//extractNodeFromMatch.addAll(rule.getReturns());
		if(rule.getReturns().isEmpty())
			sb2.append("\t\t\treturn RulePattern.EmptyReturnElements;\n");
		else {
			sb2.append("\t\t\treturn new IGraphElement[] { ");
			for(Entity ent : rule.getReturns()) {
				if(ent instanceof Node)
					extractNodeFromMatch.add((Node)ent);
				else if(ent instanceof Node)
					extractEdgeFromMatch.add((Edge)ent);
				else
					throw new IllegalArgumentException("unknown Entity: " + ent);
				sb2.append(formatEntity(ent) + ", ");
			}
			sb2.append("};\n");
		}
		
		sb2.append("\t\t}\n");
		
		// nodes needed from match
		extractNodeFromMatch.removeAll(newNodes);
		extractEdgeFromMatch.removeAll(newEdges);
		
		for(Node node : extractNodeFromMatch)
			if(node.isRetyped())
				sb.append("\t\t\tLGSPNode " + formatEntity(node) + " = match.nodes[ (int) NodeNums." + formatIdentifiable(((RetypedNode)node).getOldNode()) + " - 1 ];\n");
			else
				sb.append("\t\t\tLGSPNode " + formatEntity(node) + " = match.nodes[ (int) NodeNums." + formatIdentifiable(node) + " - 1 ];\n");
		for(Edge edge : extractEdgeFromMatch)
			sb.append("\t\t\tLGSPEdge " + formatEntity(edge) + " = match.edges[ (int) EdgeNums." + formatIdentifiable(edge) + " - 1 ];\n");
		
		sb.append(sb2);
	}
	
	private void genEvals(StringBuffer sb, Rule rule, Collection<Node> extractNodeFromMatch, Collection<Edge> extractEdgeFromMatch) {
		boolean def_b = false, def_i = false, def_s = false, def_f = false, def_d = false;
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
				default:
					throw new IllegalArgumentException();
			}
			
			sb.append("\t\t\t" + varType + " " + varName + " = ");
			if(ass.getTarget().getType() instanceof EnumType)
				sb.append("(int) ");
			genConditionEval(sb, ass.getExpression(), extractNodeFromMatch, extractEdgeFromMatch);
			sb.append(";\n");
			
			if(entity instanceof Node) {
				sb.append("\t\t\tgraph.ChangingNodeAttribute(" + formatEntity((Node)entity) +
							  ", NodeType_" + formatIdentifiable(ass.getTarget().getMember().getOwner()) +
							  ".AttributeType_" + formatIdentifiable(ass.getTarget().getMember()) + ", ");
				genConditionEval(sb, ass.getTarget(), extractNodeFromMatch, extractEdgeFromMatch);
				sb.append(", " + varName + ");\n");
			} else if(entity instanceof Edge) {
				sb.append("\t\t\tgraph.ChangingEdgeAttribute(" + formatEntity((Edge)entity) +
							  ", EdgeType_" + formatIdentifiable(ass.getTarget().getMember().getOwner()) +
							  ".AttributeType_" + formatIdentifiable(ass.getTarget().getMember()) + ", ");
				genConditionEval(sb, ass.getTarget(), extractNodeFromMatch, extractEdgeFromMatch);
				sb.append(", " + varName + ");\n");
			}
			
			sb.append("\t\t\t");
			genConditionEval(sb, ass.getTarget(), extractNodeFromMatch, extractEdgeFromMatch);
			sb.append(" = ");
			if(ass.getTarget().getType() instanceof EnumType)
				sb.append("(ENUM_" + formatIdentifiable(ass.getTarget().getType()) + ") ");
			sb.append(varName + ";\n");
		}
	}
	
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
			Object prioO = noe.getAttributes().get("prio");
			
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
			Object prioO = noe.getAttributes().get("prio");
			
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
	
	private int genPatternGraph(StringBuffer sb, PatternGraph outer, PatternGraph pattern, String pattern_name,
								int condCntInit, int negCount, List<Entity> parameters) {
		boolean isNeg = outer != null;
		String additional_parameters = isNeg?"PatternElementType.NegElement":"PatternElementType.Normal";
		
		for(Node node : pattern.getNodes()) {
			if(outer != null && outer.hasNode(node))
				continue;
			sb.append("\t\t\tPatternNode " + formatEntity(node, outer, negCount) + " = new PatternNode(");
			sb.append("(int) NodeTypes." + formatIdentifiable(node.getType()) + ", \"" + formatEntity(node, outer, negCount) + "\"");
			sb.append(", " + formatEntity(node, outer, negCount) + "_AllowedTypes, ");
			sb.append(formatEntity(node, outer, negCount) + "_IsAllowedType, ");
			sb.append(parameters.contains(node)?"PatternElementType.Preset":additional_parameters);
			sb.append(", " + parameters.indexOf(node) + ");\n");
		}
		for(Edge edge : pattern.getEdges()) {
			if(outer != null && outer.hasEdge(edge))
				continue;
			sb.append("\t\t\tPatternEdge " + formatEntity(edge, outer, negCount) + " = new PatternEdge(");
			sb.append(formatEntity(pattern.getSource(edge), outer, negCount) + ", " + formatEntity(pattern.getTarget(edge), outer, negCount));
			sb.append(", (int) EdgeTypes." + formatIdentifiable(edge.getType()) + ", \"" + formatEntity(edge, outer, negCount) + "\"");
			sb.append(", " + formatEntity(edge, outer, negCount) + "_AllowedTypes, ");
			sb.append(formatEntity(edge, outer, negCount) + "_IsAllowedType, ");
			sb.append(parameters.contains(edge)?"PatternElementType.Preset":additional_parameters);
			sb.append(", " + parameters.indexOf(edge) + ");\n");
		}
		int condCnt = condCntInit;
		for(Expression expr : pattern.getConditions()){
			Set<Node> nodes = new HashSet<Node>();
			Set<Edge> edges = new HashSet<Edge>();
			collectNodesnEdges(nodes, edges, expr);
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
		
		sb.append("\n");
		sb.append("\t\t\t);\n");
		
		return condCnt;
	}
	
	/**
	 * Method genTypeCondition
	 *
	 * @param    sb                  a  StringBuffer
	 * @param    node                a  Node
	 *
	 */
	private void genTypeCondition(StringBuffer sb, MatchingAction action) {
		genAllowedTypeArrays(sb, action.getPattern(), null, -1);
		int i = 0;
		for(PatternGraph neg : action.getNegs())
			genAllowedTypeArrays(sb, neg, action.getPattern(), i++);
	}
	
	private void genAllowedTypeArrays(StringBuffer sb, PatternGraph pattern, PatternGraph outer, int negCount) {
		genAllowedTypeArrays1(sb, pattern, outer, negCount);
		genAllowedTypeArrays2(sb, pattern, outer, negCount);
	}
	
	private void genAllowedTypeArrays1(StringBuffer sb, PatternGraph pattern, PatternGraph outer, int negCount) {
		StringBuilder aux = new StringBuilder();
		for(Node node : pattern.getNodes()) {
			if(outer!=null && outer.getNodes().contains(node))
				continue;
			sb.append("\t\tpublic static ITypeFramework[] " + formatEntity(node, outer, negCount) + "_AllowedTypes = ");
			aux.append("\t\tpublic static bool[] " + formatEntity(node, outer, negCount) + "_IsAllowedType = ");
			if( !node.getConstraints().isEmpty() ) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : node.getConstraints())
					for(Type type : nodeTypeMap.keySet()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : nodeTypeMap.keySet()) {
					boolean isAllowed = type.isCastableTo(node.getNodeType()) && !allForbiddenTypes.contains(type);
					// all permitted nodes, aka node that are not forbidden
					if( isAllowed )
						sb.append(formatType(type) + ".typeVar, ");
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
	
	private void genAllowedTypeArrays2(StringBuffer sb, PatternGraph pattern, PatternGraph outer, int negCount) {
		StringBuilder aux = new StringBuilder();
		for(Edge edge : pattern.getEdges()) {
			if(outer!=null && outer.getEdges().contains(edge))
				continue;
			sb.append("\t\tpublic static ITypeFramework[] " + formatEntity(edge, outer, negCount) + "_AllowedTypes = ");
			aux.append("\t\tpublic static bool[] " + formatEntity(edge, outer, negCount) + "_IsAllowedType = ");
			if( !edge.getConstraints().isEmpty() ) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : edge.getConstraints())
					for(Type type : edgeTypeMap.keySet()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : edgeTypeMap.keySet()) {
					boolean isAllowed = type.isCastableTo(edge.getEdgeType()) && !allForbiddenTypes.contains(type);
					// all permitted nodes, aka node that are not forbidden
					if( isAllowed )
						sb.append(formatType(type) + ".typeVar, ");
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
	
	private void genModelEnum(StringBuffer sb) {
		sb.append("\t//\n");
		sb.append("\t// Enums\n");
		sb.append("\t//\n");
		sb.append("\n");
		
		for(EnumType enumt :enumMap.keySet()) {
			sb.append("\tpublic enum ENUM_" + formatIdentifiable(enumt) + " { ");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append(formatIdentifiable(enumi) + " = " + enumi.getValue().getValue() + ", ");
			}
			sb.append("};\n\n");
		}
		
		sb.append("\tpublic class Enums\n");
		sb.append("\t{\n");
		for(EnumType enumt :enumMap.keySet()) {
			sb.append("\t\tpublic static EnumAttributeType " + formatIdentifiable(enumt) +
						  " = new EnumAttributeType(\"ENUM_" + formatIdentifiable(enumt) + "\", new EnumMember[] {\n");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("\t\t\tnew EnumMember(" + enumi.getValue().getValue() + ", \"" + formatIdentifiable(enumi) + "\"),\n");
			}
			sb.append("\t\t});\n");
		}
		sb.append("\t}\n");
	}
	
	private void genModelTypes(StringBuffer sb, Set<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " types\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic enum " + formatNodeOrEdge(isNode) + "Types ");
		genSet(sb, types, "", "", true);
		sb.append(";\n");
		
		for(InheritanceType type : types) {
			genModelType(sb, types, type);
		}
	}
	
	private void genModelModel(StringBuffer sb, Set<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " model\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic sealed class " + formatIdentifiable(unit) + formatNodeOrEdge(isNode) + "Model : ITypeModel\n");
		sb.append("\t{\n");
		
		InheritanceType rootType = genModelModel1(sb, isNode, types);
		
		genModelModel2(sb, isNode, rootType, types);
		
		genModelModel3(sb, types);
		
		sb.append("\t}\n");
	}
	
	private InheritanceType genModelModel1(StringBuffer sb, boolean isNode, Set<? extends InheritanceType> types) {
		InheritanceType rootType = null;
		
		sb.append("\t\tpublic " + formatIdentifiable(unit) + formatNodeOrEdge(isNode) + "Model()\n");
		sb.append("\t\t{\n");
		for(InheritanceType type : types) {
			sb.append("\t\t\t" + formatType(type) + ".typeVar.subOrSameTypes = new ITypeFramework[] {\n");
			sb.append("\t\t\t\t" + formatType(type) + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && otherType.isCastableTo(type))
					sb.append("\t\t\t\t" + formatType(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");
			
			sb.append("\t\t\t" + formatType(type) + ".typeVar.superOrSameTypes = new ITypeFramework[] {\n");
			sb.append("\t\t\t\t" + formatType(type) + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && type.isCastableTo(otherType))
					sb.append("\t\t\t\t" + formatType(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");
			if(type.isRoot())
				rootType = type;
		}
		sb.append("\t\t}\n");
		
		return rootType;
	}
	
	private void genModelModel2(StringBuffer sb, boolean isNode, InheritanceType rootType, Set<? extends InheritanceType> types) {
		sb.append("\t\tpublic bool IsNodeModel { get { return " + (isNode?"true":"false") +"; } }\n");
		sb.append("\t\tpublic IType RootType { get { return " + formatType(rootType) + ".typeVar; } }\n");
		sb.append("\t\tpublic IType GetType(String name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(name)\n");
		sb.append("\t\t\t{\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t\tcase \"" + formatIdentifiable(type) + "\" : return " + formatType(type) + ".typeVar;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
	}
	
	private void genModelModel3(StringBuffer sb, Set<? extends InheritanceType> types) {
		sb.append("\t\tprivate ITypeFramework[] types = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t" + formatType(type) + ".typeVar,\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic IType[] Types { get { return types; } }\n");
		
		sb.append("\t\tprivate Type[] typeTypes = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\ttypeof(" + formatType(type) + "),\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic Type[] TypeTypes { get { return typeTypes; } }\n");
		
		sb.append("\t\tprivate AttributeType[] attributeTypes = {\n");
		for(InheritanceType type : types)
			for(Entity member : type.getMembers())
				sb.append("\t\t\t" + formatType(type) + "." + formatAttributeTypeName(member) + ",\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }\n");
	}
	
	private void genModelGraph(StringBuffer sb, Set<? extends InheritanceType> keySet, boolean p2) {
		sb.append("\t//\n");
		sb.append("\t// IGraphModel implementation\n");
		sb.append("\t//\n");
		sb.append("\n");
		
		sb.append("\tpublic sealed class " + formatIdentifiable(unit) + "GraphModel : IGraphModel\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate " + formatIdentifiable(unit) + "NodeModel nodeModel = new " + formatIdentifiable(unit) + "NodeModel();\n");
		sb.append("\t\tprivate " + formatIdentifiable(unit) + "EdgeModel edgeModel = new " + formatIdentifiable(unit) + "EdgeModel();\n");
		genValidate(sb);
		sb.append("\n");
		
		sb.append("\t\tpublic String Name { get { return \"" + formatIdentifiable(unit) + "\"; } }\n");
		sb.append("\t\tpublic ITypeModel NodeModel { get { return nodeModel; } }\n");
		sb.append("\t\tpublic ITypeModel EdgeModel { get { return edgeModel; } }\n");
		sb.append("\t\tpublic IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }\n");
		sb.append("\t\tpublic String MD5Hash { get { return \"" + unit.getTypeDigest() + "\"; } }\n");
		
		sb.append("\t}\n");
	}
	
	private void genValidate(StringBuffer sb) {
		sb.append("\t\tprivate ValidateInfo[] validateInfos = {\n");
		
		for(EdgeType edgeType : edgeTypeMap.keySet()) {
			for(ConnAssert ca :edgeType.getConnAsserts()) {
				sb.append("\t\t\tnew ValidateInfo(");
				sb.append(formatType(edgeType) + ".typeVar, ");
				sb.append(formatType(ca.getSrcType()) + ".typeVar, ");
				sb.append(formatType(ca.getTgtType()) + ".typeVar, ");
				sb.append(formatInt(ca.getSrcLower()) + ", ");
				sb.append(formatInt(ca.getSrcUpper()) + ", ");
				sb.append(formatInt(ca.getTgtLower()) + ", ");
				sb.append(formatInt(ca.getTgtUpper()));
				sb.append("),\n");
			}
		}
		
		sb.append("\t\t};\n");
	}
	
	private void genModelType(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		String typeName = formatIdentifiable(type);
		String cname = formatNodeOrEdge(type) + "_" + typeName;
		String tname = formatType(type);
		String iname = "I" + cname;
		
		sb.append("\n");
		sb.append("\t// *** " + formatNodeOrEdge(type) + " " + typeName + " ***\n");
		sb.append("\n");
		
		sb.append("\tpublic interface " + iname + " : ");
		genSuperTypeList(sb, type);
		sb.append("\n");
		sb.append("\t{\n");
		genAttributeAccess(sb, type);
		sb.append("\t}\n");
		
		sb.append("\n");
		sb.append("\tpublic sealed class " + cname + " : " + iname + "\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic Object Clone() { return MemberwiseClone(); }\n");
		genAttributeAccessImpl(sb, type);
		sb.append("\t}\n");
		
		sb.append("\n");
		sb.append("\tpublic sealed class " + tname + " : TypeFramework<" + tname + ", " + cname + ", " + iname + ">\n");
		sb.append("\t{\n");
		genAttributeAttributes(sb, type);
		sb.append("\t\tpublic " + tname + "()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\ttypeID   = (int) " + formatNodeOrEdge(type) + "Types." + typeName + ";\n");
		genIsA(sb, types, type);
		genIsMyType(sb, types, type);
		genAttributeInit(sb, type);
		sb.append("\t\t}\n");
		sb.append("\t\tpublic override String Name { get { return \"" + typeName + "\"; } }\n");
		sb.append("\t\tpublic override bool IsNodeType { get { return " + ((type instanceof NodeType) ? "true" : "false") + "; } }\n");
		sb.append("\t\tpublic override IAttributes CreateAttributes() { return "
					  + (type.getAllMembers().size() == 0 ? "null" : "new " + cname + "()") + "; }\n");
		sb.append("\t\tpublic override int NumAttributes { get { return " + type.getAllMembers().size() + "; } }\n");
		genAttributeTypesEnum(sb, type);
		genGetAttributeType(sb, type);
		sb.append("\t}\n");
	}
	
	
	/**
	 * Method genAttributeAccess
	 * @param    sb                  a  StringBuffer
	 * @param    type                an InheritanceType
	 */
	private void genAttributeAccess(StringBuffer sb, InheritanceType type) {
		for(Entity e : type.getMembers()) {
			sb.append("\t\t" + formatAttributeType(e) + " " + formatAttributeName(e) + " { get; set; }\n");
		}
	}
	
	/**
	 * Method genAttributeAccessImpl
	 * @param    sb                  a  StringBuffer
	 * @param    type                an InheritanceType
	 */
	private void genAttributeAccessImpl(StringBuffer sb, InheritanceType type) {
		for(Entity e : type.getAllMembers()) {
			sb.append("\t\tprivate " + formatAttributeType(e) + " _" + formatAttributeName(e) + ";\n");
			sb.append("\t\tpublic " + formatAttributeType(e) + " " + formatAttributeName(e) + "\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\tget { return _" + formatAttributeName(e) + "; }\n");
			sb.append("\t\t\tset { _" + formatAttributeName(e) + " = value; }\n");
			sb.append("\t\t}\n");
			sb.append("\n");
		}
	}
	
	/**
	 * Method genAttributeAccessImpl
	 * @param    sb                  a  StringBuffer
	 * @param    type                an InheritanceType
	 */
	private void genAttributeInit(StringBuffer sb, InheritanceType type) {
		for(Entity e : type.getMembers()) {
			sb.append("\t\t\t" + formatAttributeTypeName(e) + " = new AttributeType(");
			sb.append("\"" + formatAttributeName(e) + "\", this, AttributeKind.");
			Type t = e.getType();
			
			if (t instanceof IntType)
				sb.append("IntegerAttr, null");
			else if (t instanceof FloatType)
				sb.append("FloatAttr, null");
			else if (t instanceof DoubleType)
				sb.append("DoubleAttr, null");
			else if (t instanceof BooleanType)
				sb.append("BooleanAttr, null");
			else if (t instanceof StringType)
				sb.append("StringAttr, null");
			else if (t instanceof EnumType)
				sb.append("EnumAttr, Enums." + formatIdentifiable(t));
			else throw new IllegalArgumentException("Unknown Entity: " + e + "(" + t + ")");
			
			sb.append(");\n");
		}
	}
	
	/**
	 * Generate a list of supertpes of the actual type.
	 */
	private void genSuperTypeList(StringBuffer sb, InheritanceType type) {
		Collection<InheritanceType> superTypes = type.getDirectSuperTypes();
		
		if(superTypes.isEmpty())
			sb.append("IAttributes");
		
		for(Iterator<InheritanceType> i = superTypes.iterator(); i.hasNext(); ) {
			InheritanceType superType = i.next();
			sb.append("I" + formatNodeOrEdge(type) + "_" + formatIdentifiable(superType));
			if(i.hasNext())
				sb.append(", ");
		}
	}
	
	private void genAttributeAttributes(StringBuffer sb, InheritanceType type) {
		for(Entity member : type.getMembers()) // only for locally defined members
			sb.append("\t\tpublic static AttributeType " + formatAttributeTypeName(member) + ";\n");
	}
	
	private void genConditionEval(StringBuffer sb, Expression cond, Collection<Node> extractNodeFromMatch, Collection<Edge> extractEdgeFromMatch) {
		if(cond instanceof Operator) {
			Operator op = (Operator)cond;
			switch (op.arity()) {
				case 1:
					sb.append("(" + opSymbols[op.getOpCode()] + " ");
					genConditionEval(sb, op.getOperand(0), extractNodeFromMatch, extractEdgeFromMatch);
					sb.append(")");
					break;
				case 2:
					genConditionEval(sb, op.getOperand(0), extractNodeFromMatch, extractEdgeFromMatch);
					sb.append(" " + opSymbols[op.getOpCode()] + " ");
					genConditionEval(sb, op.getOperand(1), extractNodeFromMatch, extractEdgeFromMatch);
					break;
				case 3:
					if(op.getOpCode()==Operator.COND) {
						sb.append("(");
						genConditionEval(sb, op.getOperand(0), extractNodeFromMatch, extractEdgeFromMatch);
						sb.append(") ? (");
						genConditionEval(sb, op.getOperand(1), extractNodeFromMatch, extractEdgeFromMatch);
						sb.append(") : (");
						genConditionEval(sb, op.getOperand(2), extractNodeFromMatch, extractEdgeFromMatch);
						sb.append(")");
						break;
					}
				default: throw new UnsupportedOperationException("Unsupported Operation arrity (" + op.arity() + ")");
			}
		}
		else if(cond instanceof Qualification) {
			Qualification qual = (Qualification)cond;
			Entity entity = qual.getOwner();
			
			if(entity instanceof Node)
				genQualAccess((Node)entity, extractNodeFromMatch, sb, qual);
			else if (entity instanceof Edge)
				genQualAccess((Edge)entity, extractEdgeFromMatch, sb, qual);
			else
				throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
		}
		else if (cond instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) cond;
			Type type = constant.getType();
			
			switch (type.classify()) {
				case Type.IS_STRING: //emit C-code for string constants
					sb.append("\"" + constant.getValue() + "\"");
					break;
				case Type.IS_BOOLEAN: //emit C-code for boolean constans
					Boolean bool_const = (Boolean) constant.getValue();
					if ( bool_const.booleanValue() )
						sb.append("1"); /* true-value */
					else
						sb.append("0"); /* false-value */
					break;
				case Type.IS_INTEGER: //emit C-code for integer constants
				case Type.IS_FLOAT: //emit C-code for float constants
				case Type.IS_DOUBLE: //emit C-code for double constants
					sb.append(constant.getValue().toString()); /* this also applys to enum constants */
					break;
				case Type.IS_TYPE: //emit code for type constants
					InheritanceType it = (InheritanceType)constant.getValue();
					sb.append(formatType(it)+".typeVar");
					break;
				default:
					throw new UnsupportedOperationException("unsupported type");
			}
		} else if(cond instanceof Typeof) {
			Typeof to = (Typeof)cond;
			sb.append(formatEntity(to.getEntity())+".type");
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + cond + ")");
	}
	
	private void genQualAccess(GraphEntity entity, Collection extractEntityFromMatch, StringBuffer sb, Qualification qual) {
		if(extractEntityFromMatch != null && !entity.changesType())
			extractEntityFromMatch.add(entity);
		if(entity.changesType()) {
			sb.append(formatEntity(entity));
			sb.append("_attributes." + formatIdentifiable(qual.getMember()));
		} else {
			if(entity instanceof Edge)
				sb.append("((Edge_");
			else if(entity instanceof Node)
				sb.append("((Node_");
			else
				throw new IllegalArgumentException("Unknown Entity: " + entity);
			sb.append(formatIdentifiable(entity.getType()) + ")");
			sb.append(formatEntity(entity));
			sb.append(".attributes)." + formatIdentifiable(qual.getMember()));
		}
	}
	
	private void genIsA(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\t\tisA      = new bool[] { ");
		for(InheritanceType nt : types) {
			if(type.isCastableTo(nt))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}
	
	private void genIsMyType(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\t\tisMyType      = new bool[] { ");
		for(InheritanceType nt : types) {
			if(nt.isCastableTo(type))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}
	
	
	private void genAttributeTypesEnum(StringBuffer sb, InheritanceType type) {
		Collection<Entity> allMembers = type.getAllMembers();
		sb.append("\t\tpublic override IEnumerable<AttributeType> AttributeTypes");
		
		if(allMembers.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tget\n");
			sb.append("\t\t\t{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.append("\t\t\t\tyield return " + formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\t\t\t\tyield return " + formatType(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t}\n");
		}
	}
	
	private void genGetAttributeType(StringBuffer sb, InheritanceType type) {
		Collection<Entity> allMembers = type.getAllMembers();
		sb.append("\t\tpublic override AttributeType GetAttributeType(String name)");
		
		if(allMembers.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tswitch(name)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.append("\t\t\t\tcase \"" + formatIdentifiable(e) + "\" : return " +
								  formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\t\t\t\tcase \"" + formatIdentifiable(e) + "\" : return " +
								  formatType(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t\treturn null;\n");
			sb.append("\t\t}\n");
		}
	}
	
	
	/**
	 * Method genSet dumps C-like Set representaion.
	 *
	 * @param    sb                  a  StringBuffer
	 * @param    get                 a  Collection<Node>
	 *
	 */
	private void genSet(StringBuffer sb, Collection<? extends Identifiable> set, String pre, String post, boolean brackets) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends Identifiable> iter = set.iterator(); iter.hasNext();) {
			Identifiable id = iter.next();
			sb.append(pre + formatIdentifiable(id) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if (brackets)
			sb.append(" }");
	}
	
	
	private void genEntitySet(StringBuffer sb, Collection<? extends Entity> set, String pre, String post, boolean brackets,
							  PatternGraph outer, int negCount) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends Entity> iter = set.iterator(); iter.hasNext();) {
			Entity id = iter.next();
			sb.append(pre + formatEntity(id, outer, negCount) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if (brackets)
			sb.append(" }");
	}
	
	
	/**
	 * Method collectNodesnEdges extracts the nodes and edges occuring in an Expression.
	 *
	 * @param    nodes               a  Set to contain the nodes of cond
	 * @param    edges               a  Set to contain the edges of cond
	 * @param    cond                an Expression
	 *
	 */
	private void collectNodesnEdges(Set<Node> nodes, Set<Edge> edges, Expression cond) {
		if(cond instanceof Qualification) {
			Entity entity = ((Qualification)cond).getOwner();
			if(entity instanceof Node)
				nodes.add((Node)entity);
			else if(entity instanceof Edge)
				edges.add((Edge)entity);
			else
				throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
		}
		else if(cond instanceof Typeof) {
			Entity entity = ((Typeof)cond).getEntity();
			if(entity instanceof Node)
				nodes.add((Node)entity);
			else if(entity instanceof Edge)
				edges.add((Edge)entity);
			else
				throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
		}
		else if(cond instanceof Operator)
			for(Expression child : ((Operator)cond).getWalkableChildren())
				collectNodesnEdges(nodes, edges, child);
	}
	
	private String formatAttributeName(Entity e) {
		return formatIdentifiable(e);
	}
	
	private String formatAttributeType(Entity e) {
		Type t = e.getType();
		if (t instanceof IntType)
			return "int";
		else if (t instanceof BooleanType)
			return "bool";
		else if (t instanceof FloatType)
			return "float";
		else if (t instanceof DoubleType)
			return "double";
		else if (t instanceof StringType)
			return "String";
		else if (t instanceof EnumType)
			return "ENUM_" + formatIdentifiable(e.getType());
		else throw new IllegalArgumentException("Unknown Entity: " + e + "(" + t + ")");
	}
	
	private String formatEnum(Entity e) {
		return "ENUM_" + formatIdentifiable(e);
	}
	
	private String formatAttributeTypeName(Entity e) {
		return "AttributeType_" + formatAttributeName(e);
	}
	
	private String formatIdentifiable(Identifiable id) {
		String res = id.getIdent().toString();
		return res.replace('$', '_');
	}
	
	private String formatEntity(Entity entity, PatternGraph outer, int negCount) {
		if(entity instanceof Node)
			return ( (outer !=null && !outer.getNodes().contains(entity)) ? "neg_" + negCount + "_" : "") + "node_" + formatIdentifiable(entity);
		else if (entity instanceof Edge)
			return ( (outer !=null && !outer.getEdges().contains(entity)) ? "neg_" + negCount + "_" : "") + "edge_" + formatIdentifiable(entity);
		else
			throw new IllegalArgumentException("Unknown entity" + entity + "(" + entity.getClass() + ")");
	}
	
	private String formatEntity(Entity entity) {
		return formatEntity(entity, null, 0);
	}
	
	private String formatInt(int i) {
		return (i==Integer.MAX_VALUE)?"int.MaxValue":new Integer(i).toString();
	}
	
	private String formatType(Type type) {
		return formatNodeOrEdge(type) + "Type_" + formatIdentifiable(type);
	}
	
	private String formatNodeOrEdge(boolean isNode) {
		if(isNode)
			return "Node";
		else
			return "Edge";
	}
	
	private String formatNodeOrEdge(Type type) {
		if (type instanceof NodeType)
			return formatNodeOrEdge(true);
		else if (type instanceof EdgeType)
			return formatNodeOrEdge(false);
		else
			throw new IllegalArgumentException("Unknown type" + type + "(" + type.getClass() + ")");
	}
	
	public void done() {
		// TODO
	}
}








