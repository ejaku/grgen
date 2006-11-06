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

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.C.LibGrSearchPlanBackend;
import java.io.File;
import java.io.PrintStream;
import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

public class SearchPlanBackend extends LibGrSearchPlanBackend {
	
	private final int OUT = 0;
	private final int IN = 1;
	
	private final String MODE_EDGE_NAME = "has_mode";
	
	protected final boolean emit_subgraph_info = false;
	
	/* binary operator symbols of the C-language */
	// ATTENTION: the forst two shift operations are signed shifts
	// 		the second right shift is signed. This Backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	private String[] opSymbols = {
		null, "||", "&&", "|", "^", "&",
			"==", "!=", "<", "<=", ">", ">=", "<<", ">>", ">>", "+",
			"-", "*", "/", "%", null, null, null, null
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
	
//	// The unit to generate code for.
//	protected Unit unit;
//	// keine Ahnung wozu das gut sein soll
//	protected Sys system;
//	// The output path as handed over by the frontend.
//	private File path;
	
	/**
	 * Create a new backend.
	 * @return A new backend.
	 */
	public Backend getBackend() throws BackendException {
		return this;
	}
	
	/**
	 * Initializes the FrameBasedBackend
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.Sys, java.io.File)
	 */
	public void init(Unit unit, Sys system, File outputPath) {
		super.init(unit, system, outputPath);
//		this.unit = unit;
//		this.path = outputPath;
//		this.system = system;
//		path.mkdirs();
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
	
	private void genModel() {
		StringBuffer sb = new StringBuffer();
		String filename = formatIdent(unit.getIdent()) + "Model.cs";
		
		System.out.println("  generating the "+filename+" file...");
		
		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("\n");
		sb.append("namespace de.unika.ipd.grGen.models\n");
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
		String filename = formatIdent(unit.getIdent()) + "Actions.cs";
		
		System.out.println("  generating the "+filename+" file...");
		
		
		sb.append("using System;\n");
		sb.append("using System.Collections.Generic;\n");
		sb.append("using System.Text;\n");
		sb.append("using de.unika.ipd.grGen.libGr;\n");
		sb.append("using de.unika.ipd.grGen.lgsp;\n");
		sb.append("using de.unika.ipd.grGen.models;\n");
		sb.append("\n");
		sb.append("#if INITIAL_WARMUP\n");
		sb.append("using de.unika.ipd.grGen.grGenCookerHelper;\n");
		sb.append("#endif\n");
		sb.append("\n");
		sb.append("namespace de.unika.ipd.grGen.models\n");
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
		String actionName = formatIdent(action.getIdent());
		
		sb.append("\tpublic class Rule_" + actionName + " : RulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic static Rule_" + actionName + " Instance = new Rule_" + actionName + "();\n");
		sb.append("\n");
		genRuleInit(sb, action);
		sb.append("\n");
		genRuleReplace(sb, (Rule)action);
		sb.append("\t}\n");
		sb.append("\n");
		
		sb.append("#if INITIAL_WARMUP\n");
		sb.append("\tpublic class Schedule_" + actionName + " : Schedule\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic Schedule_" + actionName + "()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tActionName = \"" + actionName + "\";\n");
		sb.append("\t\t\tRulePattern = Rule_" + actionName + ".Instance;\n");
		// TODO
		
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
				sb.append(formatIdent(node.getIdent()) + "  = 1, ");
			else
				sb.append(formatIdent(node.getIdent()) + ", ");
		sb.append("};\n");
		
		i = 0;
		sb.append("\t\tpublic enum EdgeNums { ");
		for(Edge edge : action.getPattern().getEdges())
			if(i++ == 0)
				sb.append(formatIdent(edge.getIdent()) + " = 1, ");
			else
				sb.append(formatIdent(edge.getIdent()) + ", ");
		sb.append("};\n");
		sb.append("\n");
		
		sb.append("\t\tpublic Rule_" + formatIdent(action.getIdent()) + "()\n");
		sb.append("\t\t{\n");
		
		PatternGraph pattern = action.getPattern();
		genPatternGraph(sb, null, pattern, "PatternGraph", "");
		sb.append("\n");
		
		i = 0;
		for(PatternGraph neg : action.getNegs()) {
			String negName = "negPattern_" + i++;
			sb.append("\t\t\tPatternGraph " + negName + ";\n");
			sb.append("\t\t\t{\n");
			genPatternGraph(sb, pattern, neg, negName, ", PatternElementType.NegElement");
			sb.append("\t\t\t}\n\n");
		}
		
		sb.append("\t\t\tNegativePatternGraphs = new PatternGraph[] {");
		for(i = 0; i < action.getNegs().size(); i++)
			sb.append("negPattern_" + i + ", ");
		sb.append("};\n");
		sb.append("\t\t\tInputs = new IType[0];\n");
		sb.append("\t\t\tOutputs = new IType[0];\n");
		
		sb.append("\t\t}\n");
	}
	
	
	
	private void genRuleReplace(StringBuffer sb, Rule rule) {
		sb.append("\t\tpublic override void Replace(IGraph graph, Match match, IGraphElement[] parameters, String[] returnVars)\n");
		sb.append("\t\t{\n");
		
		Collection<Node> newNodes = new HashSet<Node>(rule.getRight().getNodes());
		Collection<Edge> newEdges = new HashSet<Edge>(rule.getRight().getEdges());
		Collection<Node> delNodes = new HashSet<Node>(rule.getLeft().getNodes());
		Collection<Edge> delEdges = new HashSet<Edge>(rule.getLeft().getEdges());
		
		newNodes.removeAll(rule.getCommonNodes());
		newEdges.removeAll(rule.getCommonEdges());
		delNodes.removeAll(rule.getCommonNodes());
		delEdges.removeAll(rule.getCommonEdges());
		
		for(Node node : newNodes)
			sb.append(
				"\t\t\tINode node_" + formatIdent(node.getIdent()) + " = graph.AddNode(" +
					"NodeType_" + formatIdent(node.getType().getIdent()) + ".typeVar);\n"
			);
		
		for(Edge edge : newEdges) {
			String src, tgt;
			
			if(rule.getCommonNodes().contains(rule.getRight().getSource(edge)) )
				src = "match.Nodes[(int) NodeNums." + formatIdent(rule.getRight().getSource(edge).getIdent())  + " - 1]";
			else
				src = "node_" + formatIdent(rule.getRight().getSource(edge).getIdent());
			
			if(rule.getCommonNodes().contains(rule.getRight().getTarget(edge)) )
				tgt = "match.Nodes[(int) NodeNums." + formatIdent(rule.getRight().getTarget(edge).getIdent()) + " - 1]";
			else
				tgt = "node_" + formatIdent(rule.getRight().getTarget(edge).getIdent());
			
			sb.append(
				"\t\t\tIEdge edge_" + formatIdent(edge.getIdent()) + " = graph.AddEdge(" +
					"EdgeType_" + formatIdent(edge.getType().getIdent()) + ".typeVar, " +
					src + ", " + tgt + ");\n"
			);
		}
		
		for(Assignment ass : rule.getEvals())
			sb.append("// TODO " + ass); // TODO
		
		for(Node node : delNodes)
			sb.append("\t\t\tgraph.Remove(match.Nodes[(int) NodeNums." + formatIdent(node.getIdent()) + " - 1]);\n");
		
		for(Edge edge : delEdges)
			sb.append("\t\t\tgraph.Remove(match.Edges[(int) EdgeNums." + formatIdent(edge.getIdent()) + " - 1]);\n");
		
		sb.append("\t\t}\n");
	}
	
	private void genPrios(MatchingAction action, StringBuffer sb) {
		double max;
		max = computePriosMax(action.getPattern().getNodes(), -1);
		max = computePriosMax(action.getPattern().getEdges(), max);
		// TODO compute max of NACs
		
		sb.append("\t\t\tNodeCost = new float[] { ");
		genPriosNoE(sb, action.getPattern().getNodes(), max);
		sb.append(" };\n");
		
		sb.append("\t\t\tEdgeCost = new float[] { ");
		genPriosNoE(sb, action.getPattern().getEdges(), max);
		sb.append(" };\n");
		
		sb.append("\t\t\tNegNodeCost = new float[][] { ");
		for(PatternGraph neg : action.getNegs()){
			sb.append("new float[] { ");
			genPriosNoE(sb, neg.getNodes(), max);
			sb.append("}, ");
		}
		sb.append("};\n");
		
		sb.append("\t\t\tNegEdgeCost = new float[][] { ");
		for(PatternGraph neg : action.getNegs()){
			sb.append("new float[] { ");
			genPriosNoE(sb, neg.getEdges(), max);
			sb.append("}, ");
		}
		sb.append("};\n");
	}
	
	private double computePriosMax(Collection<? extends ConstraintEntity> nodesOrEdges, double max) {
		for(ConstraintEntity noe : nodesOrEdges) {
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
	
	private void genPriosNoE(StringBuffer sb, Collection<? extends ConstraintEntity> nodesOrEdges, double max) {
		for(ConstraintEntity noe : nodesOrEdges) {
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
	
	private void genPatternGraph(StringBuffer sb, PatternGraph outer, PatternGraph pattern, String pattern_name, String additional_parameters) {
		for(Node node : pattern.getNodes()) {
			if(outer != null && outer.hasNode(node))
				continue;
			sb.append("\t\t\tPatternNode node_"  + formatIdent(node.getIdent()) + " = new PatternNode(");
			sb.append("(int) NodeTypes." + formatIdent(node.getType().getIdent()) + ", \"" + formatIdent(node.getIdent()) + "\"" + additional_parameters + ");\n");
		}
		for(Edge edge : pattern.getEdges()) {
			if(outer != null && outer.hasEdge(edge))
				continue;
			sb.append("\t\t\tPatternEdge edge_"  + formatIdent(edge.getIdent()) + " = new PatternEdge(");
			sb.append("node_" + formatIdent(pattern.getSource(edge).getIdent()) + ", node_"+ formatIdent(pattern.getTarget(edge).getIdent()));
			sb.append(", (int) EdgeTypes." + formatIdent(edge.getType().getIdent()) + ", \"" + formatIdent(edge.getIdent()) + "\"" + additional_parameters + ");\n");
		}
		
		sb.append("\t\t\t" + pattern_name + " = new PatternGraph(\n");
		
		sb.append("\t\t\t\tnew PatternNode[] { ");
		for(Node node : pattern.getNodes())
			sb.append("node_" + formatIdent(node.getIdent()) + ", ");
		sb.append("}, \n");
		
		sb.append("\t\t\t\tnew PatternEdge[] { ");
		for(Edge edge : pattern.getEdges())
			sb.append("edge_" + formatIdent(edge.getIdent()) + ", ");
		sb.append("}, \n");
		
		sb.append("\t\t\t\tnew Condition[] { ");
		int i = 0;
		for(Expression expr : pattern.getConditions())
			sb.append("cond_" + i++ + ", // TODO " + expr); // TODO
		sb.append("}, \n");
		
		sb.append("\t\t\t\tnew bool[" + pattern.getNodes().size() + ", " + pattern.getNodes().size() + "] {\n");
		for(Node node1 : pattern.getNodes()) {
			sb.append("\t\t\t\t\t{ ");
			for(Node node2 : pattern.getNodes())
				if(node1.isHomomorphic(node2))
					sb.append("true, ");
				else
					sb.append("false, ");
			sb.append("},\n");
		}
		sb.append("\t\t\t\t}\n");
		
		sb.append("\t\t\t);\n");
	}
	
	private void genModelEnum(StringBuffer sb) {
		sb.append("\t//\n");
		sb.append("\t// Enums\n");
		sb.append("\t//\n");
		sb.append("\n");
		
		for(EnumType enumt :enumMap.keySet()) {
			sb.append("\tpublic enum ENUM_" + formatIdent(enumt.getIdent()) + " { ");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append(formatIdent(enumi.getIdent()) + " = " + enumi.getValue().getValue() + ", ");
			}
			sb.append("};\n\n");
		}
		
		sb.append("\tpublic class Enums\n");
		sb.append("\t{\n");
		for(EnumType enumt :enumMap.keySet()) {
			sb.append("\t\tpublic static EnumAttributeType " + formatIdent(enumt.getIdent()) +
						  " = new EnumAttributeType(\"ENUM_" + formatIdent(enumt.getIdent()) + "\", new EnumMember[] {\n");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("\t\t\tnew EnumMember(" + enumi.getValue().getValue() + ", \"" + formatIdent(enumi.getIdent()) + "\"),\n");
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
		sb.append("\tpublic enum " + formatNodeOrEdge(isNode) + "Types { ");
		for(Identifiable id : types) {
			String type = formatIdent(id.getIdent());
			sb.append(formatId(type) + ", ");
		}
		sb.append("};\n");
		
		for(InheritanceType type : types) {
			genModelType(sb, types, type);
		}
	}
	
	private void genModelModel(StringBuffer sb, Set<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " model\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic sealed class " + formatNodeOrEdge(isNode) + "Model : ITypeModel\n");
		sb.append("\t{\n");
		
		InheritanceType rootType = genModelModel1(sb, isNode, types);
		
		genModelModel2(sb, isNode, rootType, types);
		
		genModelModel3(sb, types);
		
		sb.append("\t}\n");
	}
	
	private InheritanceType genModelModel1(StringBuffer sb, boolean isNode, Set<? extends InheritanceType> types) {
		InheritanceType rootType = null;
		
		sb.append("\t\tpublic " + formatNodeOrEdge(isNode) + "Model()\n");
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
			sb.append("\t\t\t\tcase \"" + formatIdent(type.getIdent()) + "\" : return " + formatType(type) + ".typeVar;\n");
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
		
		sb.append("\tpublic sealed class GraphModel : IGraphModel\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate NodeModel nodeModel = new NodeModel();\n");
		sb.append("\t\tprivate EdgeModel edgeModel = new EdgeModel();\n");
		genValidate(sb);
		sb.append("\n");
		
		sb.append("\t\tpublic String Name { get { return \"" + formatIdent(unit.getIdent()) + "\"; } }\n");
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
		String typeName = formatIdent(type.getIdent());
		String cname = formatNodeOrEdge(type) + "_" + formatId(typeName);
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
		// TODO compute cost of node by prio
		
		sb.append("\t\tpublic override String Name { get { return \"" + typeName + "\"; } }\n");
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
			else if (t instanceof BooleanType)
				sb.append("BooleanAttr, null");
			else if (t instanceof StringType)
				sb.append("StringAttr, null");
			else if (t instanceof EnumType)
				sb.append("EnumAttr, Enums." + formatIdent(t.getIdent()));
			else throw new IllegalArgumentException("Unknown Entity: " + e + "(" + t + ")");
			
			sb.append(");\n");
		}
	}
	
	/**
	 * Generate a list of supertpes of the actual type.
	 */
	private void genSuperTypeList(StringBuffer sb, InheritanceType type) {
		Collection<InheritanceType> superTypes = type.getSuperTypes();
		
		if(superTypes.isEmpty())
			sb.append("IAttributes");
		
		for(Iterator<InheritanceType> i = superTypes.iterator(); i.hasNext(); ) {
			InheritanceType superType = i.next();
			sb.append("I" + formatNodeOrEdge(type) + "_" + formatIdent(superType.getIdent()));
			if(i.hasNext())
				sb.append(", ");
		}
	}
	
	private void genAttributeAttributes(StringBuffer sb, InheritanceType type) {
		for(Entity member : type.getMembers()) // only for locally defined members
			sb.append("\t\tpublic static AttributeType " + formatAttributeTypeName(member) + ";\n");
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
					sb.append("\t\t\t\tcase \"" + formatIdent(e.getIdent()) + "\" : return " +
								  formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\t\t\t\tcase \"" + formatIdent(e.getIdent()) + "\" : return " +
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
	 * @param    set                 a  Set
	 *
	 */
	private void genSet(StringBuffer sb, Set<? extends Entity> set) {
		sb.append('{');
		for(Iterator<? extends Entity> i = set.iterator(); i.hasNext(); ) {
			Entity e = i.next();
			if(e instanceof Node)
				sb.append("n_" + formatIdent(e.getIdent()));
			else if (e instanceof Edge)
				sb.append("e_" + formatIdent(e.getIdent()));
			else
				sb.append(formatIdent(e.getIdent()));
			if(i.hasNext())
				sb.append(", ");
		}
		sb.append('}');
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
		else if(cond instanceof Operator)
			for(Expression child : ((Operator)cond).getWalkableChildren())
				collectNodesnEdges(nodes, edges, child);
	}
	
	private String formatAttributeName(Entity e) {
		return formatIdent(e.getIdent());
	}
	
	private String formatAttributeType(Entity e) {
		Type t = e.getType();
		if (t instanceof IntType)
			return "int";
		else if (t instanceof BooleanType)
			return "bool";
		else if (t instanceof StringType)
			return "String";
		else if (t instanceof EnumType)
			return "ENUM_" + formatIdent(e.getType().getIdent());
		else throw new IllegalArgumentException("Unknown Entity: " + e + "(" + t + ")");
	}
	
	private String formatEnum(Entity e) {
		return "ENUM_" + formatIdent(e.getIdent());
	}
	
	private String formatAttributeTypeName(Entity e) {
		return "AttributeType_" + formatAttributeName(e);
	}
	
	private String formatIdent(Ident id) {
		String res = id.toString();
		return res.replace('$', '_');
	}
	
	private String formatInt(int i) {
		return (i==Integer.MAX_VALUE)?"int.MaxValue":new Integer(i).toString();
	}
	
	private String formatType(Type type) {
		return formatNodeOrEdge(type) + "Type_" + formatIdent(type.getIdent());
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
}



