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
 * A GrGen backend which generates C# code for a search-plan-based implementation
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.be.Csharp;



import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.util.Util;
import java.io.File;
import java.io.PrintStream;

public class SearchPlanBackend2 extends IDBase implements Backend, BackendFactory {

	/** The unit to generate code for. */
	protected Unit unit;

	/** The output path as handed over by the frontend. */
	private File path;

	/* binary operator symbols of the C-language */
	// ATTENTION: the first two shift operations are signed shifts,
	// 		the second right shift is unsigned. This Backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	// TODO: Check whether this is correct...
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
	 * Starts the C#-code Generation of the SearchPlanBackend2
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
		sb.append("using de.unika.ipd.grGen.lgsp;\n");
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
//		String filename = Util.removePathPrefix(unit.getFilename() + ".cs");
		String filename = formatIdentifiable(unit) + "Actions_intermediate.cs";


		System.out.println("  generating the "+filename+" file...");


		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using System.Text;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("using de.unika.ipd.grGen.lgsp;\n");
		sb.append("using de.unika.ipd.grGen.models." + formatIdentifiable(unit) + ";\n");
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

		sb.append("\tpublic class Rule_" + actionName + " : LGSPRulePattern\n");
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
		if(action instanceof Rule) {
			genRuleModify(sb, (Rule)action, true);
			sb.append("\n");
			genRuleModify(sb, (Rule)action, false);
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
		}
		else
			throw new IllegalArgumentException("Unknown action type: " + action);
		sb.append("\t}\n");
		sb.append("\n");

		sb.append("#if INITIAL_WARMUP\n");
		sb.append("\tpublic class Schedule_" + actionName + " : LGSPStaticScheduleInfo\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic Schedule_" + actionName + "()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tActionName = \"" + actionName + "\";\n");
		sb.append("\t\t\tthis.RulePattern = Rule_" + actionName + ".Instance;\n");
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

		sb.append("\t\tprivate Rule_" + formatIdentifiable(action) + "()\n");
		sb.append("\t\t{\n");

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

	private void genRuleParamResult(StringBuffer sb, Action action) {
		sb.append("\t\t\tinputs = new GrGenType[] { ");
		if(action instanceof MatchingAction)
			for(Entity ent : ((MatchingAction)action).getParameters())
				sb.append(formatType(ent.getType()) + ".typeVar, ");
		sb.append("};\n");

		sb.append("\t\t\toutputs = new GrGenType[] { ");
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
			genConditionEval(sb, expr, false);
			sb.append(";\n");
			sb.append("\t\t}\n");
			i++;
		}
		return i;
	}

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

	private HashSet<Node> nodesNeededAsElements = new LinkedHashSet<Node>();
	private HashSet<Edge> edgesNeededAsElements = new LinkedHashSet<Edge>();
	private HashSet<Node> nodesNeededAsAttributes = new LinkedHashSet<Node>();
	private HashSet<Edge> edgesNeededAsAttributes = new LinkedHashSet<Edge>();
	private HashSet<Node> nodesNeededAsTypes = new LinkedHashSet<Node>();
	private HashSet<Edge> edgesNeededAsTypes = new LinkedHashSet<Edge>();

	private boolean accessViaVariable(GraphEntity elem, Entity attr) {
		if(!reusedElements.contains(elem)) return false;
		HashSet<Entity> neededAttrs = neededAttributes.get(elem);
		return neededAttrs != null && neededAttrs.contains(attr);
	}


	private void genRuleModify(StringBuffer sb, Rule rule, boolean reuseNodeAndEdges) {
		StringBuffer sb2 = new StringBuffer();
		StringBuffer sb3 = new StringBuffer();

		sb.append("\t\tpublic override IGraphElement[] "
				+ (reuseNodeAndEdges ? "Modify" : "ModifyNoReuse")
				+ "(LGSPGraph graph, LGSPMatch match)\n");
		sb.append("\t\t{\n");

		newNodes = new HashSet<Node>(rule.getRight().getNodes());
		newEdges = new HashSet<Edge>(rule.getRight().getEdges());
		delNodes = new HashSet<Node>(rule.getLeft().getNodes());
		delEdges = new HashSet<Edge>(rule.getLeft().getEdges());
		reusedElements.clear();
		neededAttributes.clear();
		nodesNeededAsElements.clear();
		edgesNeededAsElements.clear();
		nodesNeededAsTypes.clear();
		edgesNeededAsTypes.clear();
		nodesNeededAsAttributes.clear();
		edgesNeededAsAttributes.clear();

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
					new_type = formatType(rnode.getType()) + ".typeVar";
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
					new_type = formatType(redge.getType()) + ".typeVar";
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
						+ " = match.nodes[ (int) NodeNums.@"
						+ formatIdentifiable(node) + " - 1 ];\n");
			}
		}
		for(Node node : nodesNeededAsAttributes) {
			if(!node.isRetyped()) {
				sb.append("\t\t\t" + formatCastedAssign(node.getType(), "I", "i" + formatEntity(node)));
				if(nodesNeededAsElements.contains(node))
					sb.append(formatEntity(node) + ";\n");
				else
					sb.append("match.nodes[ (int) NodeNums.@" + formatIdentifiable(node) + " - 1 ];\n");
			}
		}
		for(Edge edge : edgesNeededAsElements) {
			if(!edge.isRetyped()) {
				sb.append("\t\t\tLGSPEdge " + formatEntity(edge)
						+ " = match.edges[ (int) EdgeNums.@"
						+ formatIdentifiable(edge) + " - 1 ];\n");
			}
		}
		for(Edge edge : edgesNeededAsAttributes) {
			if(!edge.isRetyped()) {
				sb.append("\t\t\t" + formatCastedAssign(edge.getType(), "I", "i" + formatEntity(edge)));
				if(edgesNeededAsElements.contains(edge))
					sb.append(formatEntity(edge) + ";\n");
				else
					sb.append("match.edges[ (int) EdgeNums.@" + formatIdentifiable(edge) + " - 1 ];\n");
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

				sb.append("\t\t\t" + varTypeName + " var_" + grEntName + "_" + attrName
						+ " = i" + grEntName + ".@" + attrName + ";\n");
			}
		}

		// new nodes/edges (re-use) and retype nodes/edges
		sb.append(sb2);

		// attribute re-calc, remove, return
		sb.append(sb3);

		if(reuseNodeAndEdges) { // we need this only once
			genAddedGraphElementsArray(sb, true, newNodes);
			genAddedGraphElementsArray(sb, false, newEdges);
		}
	}

	private void genAddedGraphElementsArray(StringBuffer sb, boolean isNode, Collection<? extends GraphEntity> set) {
		String NodesOrEdges = isNode?"Node":"Edge";
		sb.append("\t\tprivate static String[] added" + NodesOrEdges + "Names = new String[] ");
		genSet(sb, set, "\"", "\"", true);
		sb.append(";\n");
		sb.append("\t\tpublic override String[] Added" + NodesOrEdges + "Names { get { return added" + NodesOrEdges + "Names; } }\n");
	}

	private void genRewriteNewEdges(StringBuffer sb2, Rule rule, boolean reuseNodeAndEdges) {
		PatternGraph leftSide = rule.getLeft();
		Graph rightSide = rule.getRight();

		NE:	for(Edge edge : newEdges) {
			String ctype = formatClassType(edge.getType());

			Node src_node = rightSide.getSource(edge);
			Node tgt_node = rightSide.getTarget(edge);

			if( commonNodes.contains(src_node) )
				nodesNeededAsElements.add(src_node);

			if( commonNodes.contains(tgt_node) )
				nodesNeededAsElements.add(tgt_node);

			if(edge.inheritsType()) {
				edgesNeededAsElements.add(edge.getTypeof());
				edgesNeededAsTypes.add(edge.getTypeof());
			} else {
				if(reuseNodeAndEdges) {
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

						sb2.append("\t\t\t" + ctype + " " + newEdgeName + ";\n"
								+ "\t\t\tif(" + reusedEdgeName + ".type == " + formatType(edge.getType()) + ".typeVar)\n"
								+ "\t\t\t{\n"
								+ "\t\t\t\t// re-using " + reusedEdgeName + " as " + newEdgeName + "\n"
								+ "\t\t\t\t" + newEdgeName + " = (" + ctype + ") " + reusedEdgeName + ";\n"
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
								+ "\t\t\t\t" + formatEntity(edge) + " = " + ctype
								+ ".CreateEdge(graph, " + formatEntity(src_node)
								+ ", " + formatEntity(tgt_node) + ");\n");

						delEdges.remove(bestDelEdge); // Do not delete the edge (it is reused)
						edgesNeededAsElements.add(bestDelEdge);
						reusedElements.add(bestDelEdge);
						continue NE;
					}
				}
			}

			// Create the edge
			sb2.append("\t\t\t" + ctype + " " + formatEntity(edge) + " = " + ctype
					+ ".CreateEdge(graph, " + formatEntity(src_node)
					+ ", " + formatEntity(tgt_node) + ");\n");
		}
	}

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
			String ctype = formatClassType(node.getType());
			sb2.append("\t\t\t" + ctype + " " + formatEntity(node) + " = " + ctype + ".CreateNode(graph);\n");
		}
	}

	private String computeGraphEntityType(Node node) {
		String type;
		if(node.inheritsType()) {
			type = formatEntity(node.getTypeof()) + "_type";
			nodesNeededAsElements.add(node.getTypeof());
			nodesNeededAsTypes.add(node.getTypeof());
		} else {
			type = formatType(node.getType()) + ".typeVar";
		}
		return type;
	}

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
			genConditionEval(sb, ass.getExpression(), true);
			sb.append(";\n");

			if(entity instanceof Node) {
				Node node = (Node) entity;
				nodesNeededAsElements.add(node);
				sb.append("\t\t\tgraph.ChangingNodeAttribute(" + formatEntity(node) +
						", NodeType_" + formatIdentifiable(ass.getTarget().getMember().getOwner()) +
						".AttributeType_" + formatIdentifiable(ass.getTarget().getMember()) + ", ");
				genConditionEval(sb, ass.getTarget(), true);
				sb.append(", " + varName + ");\n");
			} else if(entity instanceof Edge) {
				Edge edge = (Edge) entity;
				edgesNeededAsElements.add(edge);
				sb.append("\t\t\tgraph.ChangingEdgeAttribute(" + formatEntity(edge) +
						", EdgeType_" + formatIdentifiable(ass.getTarget().getMember().getOwner()) +
						".AttributeType_" + formatIdentifiable(ass.getTarget().getMember()) + ", ");
				genConditionEval(sb, ass.getTarget(), true);
				sb.append(", " + varName + ");\n");
			}

			sb.append("\t\t\t");
			genConditionEval(sb, ass.getTarget(), true);
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
			sb.append("\t\tpublic static NodeType[] " + formatEntity(node, outer, negCount) + "_AllowedTypes = ");
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
					// all permitted nodes, aka nodes that are not forbidden
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
			sb.append("\t\tpublic static EdgeType[] " + formatEntity(edge, outer, negCount) + "_AllowedTypes = ");
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
				sb.append("@" + formatIdentifiable(enumi) + " = " + enumi.getValue().getValue() + ", ");
			}
			sb.append("};\n\n");
		}

		sb.append("\tpublic class Enums\n");
		sb.append("\t{\n");
		for(EnumType enumt :enumMap.keySet()) {
			sb.append("\t\tpublic static EnumAttributeType @" + formatIdentifiable(enumt) +
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
		genSet(sb, types, "@", "", true);
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
		sb.append("\tpublic sealed class " + formatIdentifiable(unit) + formatNodeOrEdge(isNode)
				+ "Model : I" + (isNode ? "Node" : "Edge") + "Model\n");
		sb.append("\t{\n");

		InheritanceType rootType = genModelModel1(sb, isNode, types);

		genModelModel2(sb, isNode, rootType, types);

		genModelModel3(sb, isNode, types);

		sb.append("\t}\n");
	}

	private InheritanceType genModelModel1(StringBuffer sb, boolean isNode, Set<? extends InheritanceType> types) {
		InheritanceType rootType = null;

		sb.append("\t\tpublic " + formatIdentifiable(unit) + formatNodeOrEdge(isNode) + "Model()\n");
		sb.append("\t\t{\n");
		for(InheritanceType type : types) {
			sb.append("\t\t\t" + formatType(type) + ".typeVar.subOrSameGrGenTypes = "
					+ formatType(type) + ".typeVar.subOrSameTypes = new "
					+ (isNode ? "Node" : "Edge") + "Type[] {\n");
			sb.append("\t\t\t\t" + formatType(type) + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && otherType.isCastableTo(type))
					sb.append("\t\t\t\t" + formatType(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");

			sb.append("\t\t\t" + formatType(type) + ".typeVar.superOrSameGrGenTypes = "
					+ formatType(type) + ".typeVar.superOrSameTypes = new "
					+ (isNode ? "Node" : "Edge") + "Type[] {\n");
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

	private void genModelModel2(StringBuffer sb, boolean isNode,
			InheritanceType rootType, Set<? extends InheritanceType> types) {
		sb.append("\t\tpublic bool IsNodeModel { get { return " + (isNode?"true":"false") +"; } }\n");
		sb.append("\t\tpublic " + (isNode ? "Node" : "Edge") + "Type RootType { get { return "
				+ formatType(rootType) + ".typeVar; } }\n");
		sb.append("\t\tGrGenType ITypeModel.RootType { get { return " + formatType(rootType) + ".typeVar; } }\n");
		sb.append("\t\tpublic " + (isNode ? "Node" : "Edge") + "Type GetType(String name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(name)\n");
		sb.append("\t\t\t{\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t\tcase \"" + formatIdentifiable(type) + "\" : return " + formatType(type) + ".typeVar;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
		sb.append("\t\tGrGenType ITypeModel.GetType(String name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn GetType(name);\n");
		sb.append("\t\t}\n");
	}

	private void genModelModel3(StringBuffer sb, boolean isNode, Set<? extends InheritanceType> types) {
		String elemKind = isNode ? "Node" : "Edge";
		sb.append("\t\tprivate " + elemKind + "Type[] types = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t" + formatType(type) + ".typeVar,\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic " + elemKind + "Type[] Types { get { return types; } }\n");
		sb.append("\t\tGrGenType[] ITypeModel.Types { get { return types; } }\n");

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
		sb.append("\t\tpublic INodeModel NodeModel { get { return nodeModel; } }\n");
		sb.append("\t\tpublic IEdgeModel EdgeModel { get { return edgeModel; } }\n");
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
		boolean isNode = type instanceof NodeType;
		String elemKind = isNode ? "Node" : "Edge";

		sb.append("\n");
		sb.append("\t// *** " + formatNodeOrEdge(type) + " " + typeName + " ***\n");
		sb.append("\n");

		sb.append("\tpublic interface " + iname + " : ");
		genSuperTypeList(sb, type);
		sb.append("\n");
		sb.append("\t{\n");
		genAttributeAccess(sb, type);
		sb.append("\t}\n");

		sb.append("\n"
				+ "\tpublic sealed class " + cname + " : LGSP" + elemKind + ", " + iname + "\n"
				+ "\t{\n"
				+ "\t\tprivate static int poolLevel = 0;\n"
				+ "\t\tprivate static " + cname + "[] pool = new " + cname + "[10];\n");

		if(isNode) {
			sb.append("\t\tpublic " + cname + "() : base("+ tname + ".typeVar) { }\n"
					+ "\t\tprivate " + cname + "(" + cname + " oldElem) : base(" + tname + ".typeVar)\n");
		}
		else {
			sb.append("\t\tpublic " + cname + "(LGSPNode source, LGSPNode target)\n"
					+ "\t\t\t: base("+ tname + ".typeVar, source, target) { }\n"
					+ "\t\tprivate " + cname + "(" + cname + " oldElem, LGSPNode newSource, LGSPNode newTarget)\n"
					+ "\t\t\t: base(" + tname + ".typeVar, newSource, newTarget)\n");
		}
		sb.append("\t\t{\n");
		for(Entity member : type.getAllMembers()) {
			String attrName = formatAttributeName(member);
			sb.append("\t\t\t_" + attrName + " = oldElem._" + attrName + ";\n");
		}
		sb.append("\t\t}\n");

//		sb.append("\t\tpublic Object Clone() { return MemberwiseClone(); }\n\n");
		if(isNode) {
			sb.append("\t\tpublic override INode Clone() { return new " + cname + "(this); }\n"
					+ "\t\tpublic static " + cname + " CreateNode(LGSPGraph graph)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " node;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tnode = new " + cname + "();\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tnode = pool[--poolLevel];\n"
					+ "\t\t\t\tnode.inhead = null;\n"
					+ "\t\t\t\tnode.outhead = null;\n"
					+ "\t\t\t\tnode.hasVariables = false;\n");
			initAllMembers(sb, type, "node", "\t\t\t\t");
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddNode(node);\n"
					+ "\t\t\treturn node;\n"
					+ "\t\t}\n\n"
					+ "\t\tpublic static " + cname + " CreateNode(LGSPGraph graph, String varName)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " node;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tnode = new " + cname + "();\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tnode = pool[--poolLevel];\n"
					+ "\t\t\t\tnode.inhead = null;\n"
					+ "\t\t\t\tnode.outhead = null;\n"
					+ "\t\t\t\tnode.hasVariables = false;\n");
			initAllMembers(sb, type, "node", "\t\t\t\t");
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddNode(node, varName);\n"
					+ "\t\t\treturn node;\n"
					+ "\t\t}\n\n");
		}
		else {
			sb.append("\t\tpublic override IEdge Clone(INode newSource, INode newTarget)\n"
					+ "\t\t{ return new " + cname + "(this, (LGSPNode) newSource, (LGSPNode) newTarget); }\n"
					+ "\t\tpublic static " + cname + " CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " edge;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tedge = new " + cname + "(source, target);\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tedge = pool[--poolLevel];\n"
					+ "\t\t\t\tedge.hasVariables = false;\n"
					+ "\t\t\t\tedge.source = source;\n"
					+ "\t\t\t\tedge.target = target;\n");
			initAllMembers(sb, type, "edge", "\t\t\t\t");
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddEdge(edge);\n"
					+ "\t\t\treturn edge;\n"
					+ "\t\t}\n\n"
					+ "\t\tpublic static " + cname + " CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + cname + " edge;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tedge = new " + cname + "(source, target);\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tedge = pool[--poolLevel];\n"
					+ "\t\t\t\tedge.hasVariables = false;\n"
					+ "\t\t\t\tedge.source = source;\n"
					+ "\t\t\t\tedge.target = target;\n");
			initAllMembers(sb, type, "edge", "\t\t\t\t");
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddEdge(edge, varName);\n"
					+ "\t\t\treturn edge;\n"
					+ "\t\t}\n\n");
		}
		sb.append("\t\tpublic override void Recycle()\n"
				+ "\t\t{\n"
				+ "\t\t\tif(poolLevel < 10)\n"
				+ "\t\t\t\tpool[poolLevel++] = this;\n"
				+ "\t\t}\n\n");
		genAttributeAccessImpl(sb, type);
		sb.append("\t}\n");

		sb.append("\n");
		sb.append("\tpublic sealed class " + tname + " : " + elemKind + "Type\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic static " + tname + " typeVar = new " + tname + "();\n");
		genIsA(sb, types, type);
		genIsMyType(sb, types, type);
		genAttributeAttributes(sb, type);
		sb.append("\t\tpublic " + tname + "() : base((int) " + formatNodeOrEdge(type) + "Types.@" + typeName + ")\n");
		sb.append("\t\t{\n");
		genAttributeInit(sb, type);
		sb.append("\t\t}\n");
		sb.append("\t\tpublic override String Name { get { return \"" + typeName + "\"; } }\n");

		if(isNode) {
			sb.append("\t\tpublic override INode CreateNode() { return new " + cname + "(); }\n");
		}
		else {
			sb.append("\t\tpublic override IEdge CreateEdge(INode source, INode target)\n"
					+ "\t\t{\n"
					+ "\t\t\treturn new " + cname + "((LGSPNode) source, (LGSPNode) target);\n"
					+ "\t\t}\n");
		}

		sb.append("\t\tpublic override int NumAttributes { get { return " + type.getAllMembers().size() + "; } }\n");
		genAttributeTypesEnum(sb, type);
		genGetAttributeType(sb, type);

		sb.append("\t\tpublic override bool IsA(GrGenType other)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn (this == other) || isA[other.TypeID];\n");
		sb.append("\t\t}\n");

		genCreateWithCopyCommons(sb, type);
		sb.append("\t}\n");
	}

	private void initAllMembers(StringBuffer sb, InheritanceType type, String varName, String indentString) {
		for(Entity member : type.getAllMembers()) {
			String attrName = formatAttributeName(member);
			sb.append(indentString + varName + ".@" + attrName + " = ");
			Type t = member.getType();
			if(t instanceof IntType || t instanceof DoubleType || t instanceof EnumType)
				sb.append("0;\n");
			else if(t instanceof FloatType)
				sb.append("0f;\n");
			else if(t instanceof BooleanType)
				sb.append("false;\n");
			else if(t instanceof StringType || t instanceof ObjectType)
				sb.append("null;\n");
			else throw new IllegalArgumentException("Unknown Entity: " + member + "(" + t + ")");
		}
	}

	/**
	 * Method genAttributeAccess
	 * @param    sb                  a  StringBuffer
	 * @param    type                an InheritanceType
	 */
	private void genAttributeAccess(StringBuffer sb, InheritanceType type) {
		for(Entity e : type.getMembers()) {
			sb.append("\t\t" + formatAttributeType(e) + " @" + formatAttributeName(e) + " { get; set; }\n");
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
			sb.append("\t\tpublic " + formatAttributeType(e) + " @" + formatAttributeName(e) + "\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\tget { return _" + formatAttributeName(e) + "; }\n");
			sb.append("\t\t\tset { _" + formatAttributeName(e) + " = value; }\n");
			sb.append("\t\t}\n");
			sb.append("\n");
		}

		sb.append("\t\tpublic override object GetAttribute(string attrName)\n");
		sb.append("\t\t{\n");
		if(type.getAllMembers().size() != 0) {
			sb.append("\t\t\tswitch(attrName)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : type.getAllMembers()) {
				String name = formatAttributeName(e);
				sb.append("\t\t\t\tcase \"" + name + "\": return _" + name + ";\n");
			}
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new NullReferenceException(\n");
		sb.append("\t\t\t\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\" + attrName + \\\"\\\"!\");\n");
		sb.append("\t\t}\n");

		sb.append("\t\tpublic override void SetAttribute(string attrName, object value)\n");
		sb.append("\t\t{\n");
		if(type.getAllMembers().size() != 0) {
			sb.append("\t\t\tswitch(attrName)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : type.getAllMembers()) {
				String name = formatAttributeName(e);
				sb.append("\t\t\t\tcase \"" + name + "\": _" + name + " = ("
						+ formatAttributeType(e) + ") value; return;\n");
			}
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new NullReferenceException(\n");
		sb.append("\t\t\t\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\" + attrName + \\\"\\\"!\");\n");
		sb.append("\t\t}\n");

		sb.append("\t\tpublic override void ResetAllAttributes()\n");
		sb.append("\t\t{\n");
		initAllMembers(sb, type, "this", "\t\t\t");
		sb.append("\t\t}\n");
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
				sb.append("EnumAttr, Enums.@" + formatIdentifiable(t));
			else if (t instanceof ObjectType)
				sb.append("ObjectAttr, null");
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

	private strictfp void genConditionEval(StringBuffer sb, Expression cond, boolean inRewriteModify) {
		if(cond instanceof Operator) {
			Operator op = (Operator)cond;
			switch (op.arity()) {
				case 1:
					sb.append("(" + opSymbols[op.getOpCode()] + " ");
					genConditionEval(sb, op.getOperand(0), inRewriteModify);
					sb.append(")");
					break;
				case 2:
					sb.append("(");
					genConditionEval(sb, op.getOperand(0), inRewriteModify);
					sb.append(" " + opSymbols[op.getOpCode()] + " ");
					genConditionEval(sb, op.getOperand(1), inRewriteModify);
					sb.append(")");
					break;
				case 3:
					if(op.getOpCode()==Operator.COND) {
						sb.append("((");
						genConditionEval(sb, op.getOperand(0), inRewriteModify);
						sb.append(") ? (");
						genConditionEval(sb, op.getOperand(1), inRewriteModify);
						sb.append(") : (");
						genConditionEval(sb, op.getOperand(2), inRewriteModify);
						sb.append("))");
						break;
					}
					// FALLTHROUGH
				default:
					throw new UnsupportedOperationException(
						"Unsupported operation arity (" + op.arity() + ")");
			}
		}
		else if(cond instanceof Qualification) {
			Qualification qual = (Qualification) cond;
			Entity entity = qual.getOwner();
			genQualAccess(entity, sb, qual, inRewriteModify);
		}
		else if(cond instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression) cond;
			sb.append("ENUM_" + enumExp.getType().getIdent().toString() + ".@" + enumExp.getEnumItem().toString());
		}
		else if(cond instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) cond;
			Type type = constant.getType();

			switch (type.classify()) {
				case Type.IS_STRING: //emit C-code for string constants
					sb.append("\"" + constant.getValue() + "\"");
					break;
				case Type.IS_BOOLEAN: //emit C-code for boolean constans
					Boolean bool_const = (Boolean) constant.getValue();
					if(bool_const.booleanValue())
						sb.append("true"); /* true-value */
					else
						sb.append("false"); /* false-value */
					break;
				case Type.IS_INTEGER: //emit C-code for integer constants
				case Type.IS_DOUBLE: //emit C-code for double constants
					sb.append(constant.getValue().toString());
					break;
				case Type.IS_FLOAT: //emit C-code for float constants
					sb.append(constant.getValue().toString()); /* this also applys to enum constants */
					sb.append('f');
					break;
				case Type.IS_TYPE: //emit code for type constants
					InheritanceType it = (InheritanceType) constant.getValue();
					sb.append(formatType(it) + ".typeVar");
					break;
				default:
					throw new UnsupportedOperationException("unsupported type");
			}
		}
		else if(cond instanceof Typeof) {
			Typeof to = (Typeof)cond;
			sb.append(formatEntity(to.getEntity()) + ".type");
		}
		else if(cond instanceof Cast) {
			Cast cast = (Cast) cond;
			Type type = cast.getType();

			if(type.classify() == Type.IS_STRING) {
				genConditionEval(sb, cast.getExpression(), inRewriteModify);
				sb.append(".ToString()");
			}
			else {
				String typeName = "";

				switch(type.classify()) {
					case Type.IS_INTEGER: typeName = "int"; break;
					case Type.IS_FLOAT: typeName = "float"; break;
					case Type.IS_DOUBLE: typeName = "double"; break;
					case Type.IS_BOOLEAN: typeName = "bool"; break;
					default:
						throw new UnsupportedOperationException(
							"This is either a forbidden cast, which should have been " +
							"rejected on building the IR, or an allowed cast, which " +
							"should have been processed by the above code.");
				}

				sb.append("((" + typeName  + ") ");
				genConditionEval(sb, cast.getExpression(), inRewriteModify);
				sb.append(")");
			}
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + cond + ")");
	}

	private void genQualAccess(Entity entity, StringBuffer sb, Qualification qual, boolean inRewriteModify) {
		Entity member = qual.getMember();
		if(inRewriteModify) {
			if(accessViaVariable((GraphEntity) entity, member)) {
				if(entity instanceof Node)
					nodesNeededAsAttributes.add((Node) entity);
				else if(entity instanceof Edge)
					edgesNeededAsAttributes.add((Edge) entity);

				sb.append("var_" + formatEntity(entity) + "_" + formatIdentifiable(member));
			}
			else {
				if(entity instanceof Node) {
					nodesNeededAsAttributes.add((Node) entity);
					if(!newOrRetypedNodes.contains(entity))		// element extracted from match?
						sb.append("i");							// yes, attributes only accessible via interface
				}
				else if(entity instanceof Edge) {
					edgesNeededAsAttributes.add((Edge) entity);
					if(!newOrRetypedEdges.contains(entity))		// element extracted from match?
						sb.append("i");							// yes, attributes only accessible via interface
				}
				else
					throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");

				sb.append(formatEntity(entity) + ".@" + formatIdentifiable(member));
			}
		}
		else {	 // in genConditions()
			sb.append("((I" + (entity instanceof Node ? "Node" : "Edge") + "_" +
					formatIdentifiable(entity.getType()) + ") ");
			sb.append(formatEntity(entity) + ").@" + formatIdentifiable(member));
		}
	}

	private void genIsA(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\tpublic static bool[] isA = new bool[] { ");
		for(InheritanceType nt : types) {
			if(type.isCastableTo(nt))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}

	private void genIsMyType(StringBuffer sb, Set<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\tpublic static bool[] isMyType = new bool[] { ");
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

	private void getFirstCommonAncestors(InheritanceType curType,
			InheritanceType type, Set<InheritanceType> resTypes) {
		if(type.isCastableTo(curType))
			resTypes.add(curType);
		else
			for(InheritanceType superType : curType.getDirectSuperTypes())
				getFirstCommonAncestors(superType, type, resTypes);
	}


	private void genCreateWithCopyCommons(StringBuffer sb, InheritanceType type) {
		boolean isNode = type instanceof NodeType;
		String cname = formatClassType(type);
		String kindName = isNode ? "Node" : "Edge";

		if(isNode) {
			sb.append("\t\tpublic override INode CreateNodeWithCopyCommons(INode oldINode)\n"
					+ "\t\t{\n"
					+ "\t\t\tLGSPNode oldNode = (LGSPNode) oldINode;\n"
					+ "\t\t\t" + cname + " newNode = new " + cname + "();\n");
		}
		else {
			sb.append("\t\tpublic override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)\n"
					+ "\t\t{\n"
					+ "\t\t\tLGSPEdge oldEdge = (LGSPEdge) oldIEdge;\n"
					+ "\t\t\t" + cname + " newEdge = new " + cname + "((LGSPNode) source, (LGSPNode) target);\n");
		}

		Map<BitSet, LinkedList<InheritanceType>> commonGroups = new LinkedHashMap<BitSet, LinkedList<InheritanceType>>();

		Collection<? extends InheritanceType> typeSet =
			isNode ? (Collection<? extends InheritanceType>) nodeTypeMap.keySet()
			: (Collection<? extends InheritanceType>) edgeTypeMap.keySet();
		for(InheritanceType itype : typeSet) {
			Set<InheritanceType> firstCommonAncestors = new LinkedHashSet<InheritanceType>();
			getFirstCommonAncestors(itype, type, firstCommonAncestors);

			TreeSet<InheritanceType> sortedCommonTypes = new TreeSet<InheritanceType>(
				new Comparator<InheritanceType>() {
					public int compare(InheritanceType o1, InheritanceType o2) {
						return o2.getMaxDist() - o1.getMaxDist();
					}
				});

			sortedCommonTypes.addAll(firstCommonAncestors);
			Iterator<InheritanceType> iter = sortedCommonTypes.iterator();
			while(iter.hasNext()) {
				InheritanceType commonType = iter.next();
				if(!firstCommonAncestors.contains(commonType)) continue;
				for(InheritanceType superType : commonType.getAllSuperTypes()) {
					firstCommonAncestors.remove(superType);
				}
			}

			boolean mustCopyAttribs = false;
			for(InheritanceType commonType : firstCommonAncestors) {
				if(commonType.getAllMembers().size() != 0) {
					mustCopyAttribs = true;
					break;
				}
			}

			if(!mustCopyAttribs) continue;

			BitSet commonTypesBitset = new BitSet();
			for(InheritanceType commonType : firstCommonAncestors) {
				commonTypesBitset.set(commonType.getTypeID());
			}
			LinkedList<InheritanceType> commonList = commonGroups.get(commonTypesBitset);
			if(commonList == null) {
				commonList = new LinkedList<InheritanceType>();
				commonGroups.put(commonTypesBitset, commonList);
			}
			commonList.add(itype);
		}

		if(commonGroups.size() != 0) {
			sb.append("\t\t\tswitch(old" + kindName + ".Type.TypeID)\n"
					+ "\t\t\t{\n");
			for(Map.Entry<BitSet, LinkedList<InheritanceType>> entry : commonGroups.entrySet()) {
				for(InheritanceType itype : entry.getValue()) {
					sb.append("\t\t\t\tcase (int) " + kindName + "Types.@"
							+ formatIdentifiable(itype) + ":\n");
				}
				BitSet bitset = entry.getKey();
				HashSet<Entity> copiedAttribs = new HashSet<Entity>();
				for(int i = bitset.nextSetBit(0); i >= 0; i = bitset.nextSetBit(i+1)) {
					InheritanceType commonType = InheritanceType.getByTypeID(i);
					Collection<Entity> members = commonType.getAllMembers();
					if(members.size() != 0) {
						sb.append("\t\t\t\t\t// copy attributes for: "
								+ formatIdentifiable(commonType) + "\n");
						boolean alreadyCasted = false;
						for(Entity member : members) {
							if(copiedAttribs.contains(member)) {
								sb.append("\t\t\t\t\t\t// already copied: " + formatAttributeName(member) + "\n");
								continue;
							}
							if(!alreadyCasted) {
								alreadyCasted = true;
								sb.append("\t\t\t\t\t{\n"
										+ "\t\t\t\t\t\t" + formatCastedAssign(commonType, "I", "old")
										+ "old" + kindName + ";\n");
							}
							copiedAttribs.add(member);
							String memberName = formatAttributeName(member);
							sb.append("\t\t\t\t\t\tnew" + kindName + "." + memberName
									+ " = old." + memberName + ";\n");
						}
						if(alreadyCasted)
							sb.append("\t\t\t\t\t}\n");
					}
				}
				sb.append("\t\t\t\t\tbreak;\n");
			}
			sb.append("\t\t\t}\n");
		}

		sb.append("\t\t\treturn new" + kindName + ";\n"
				+ "\t\t}\n\n");
	}

	/**
	 * Method genSet dumps C-like Set representation.
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
		else if (t instanceof ObjectType)
			return "Object"; //TODO maybe we need another output type
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

	private String formatClassType(Type type) {
		return formatNodeOrEdge(type) + "_" + formatIdentifiable(type);
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

	private String formatCastedAssign(Type type, String typePrefix, String varName) {
		String ctype = typePrefix + formatClassType(type);
		return ctype + " " + varName + " = (" + ctype + ") ";
	}

	private String formatNodeAssign(Node node, Collection<Node> extractNodeAttributeObject) {
		if(extractNodeAttributeObject.contains(node))
			return formatCastedAssign(node.getType(), "", formatEntity(node));
		else
			return "LGSPNode " + formatEntity(node) + " = ";
	}

	private String formatEdgeAssign(Edge edge, Collection<Edge> extractEdgeAttributeObject) {
		if(extractEdgeAttributeObject.contains(edge))
			return formatCastedAssign(edge.getType(), "", formatEntity(edge));
		else
			return "LGSPEdge " + formatEntity(edge) + " = ";
	}

	public void done() {
		// TODO
	}
}

