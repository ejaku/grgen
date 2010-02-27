/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

import java.util.Collection;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.LinkedList;

import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Cast;
import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Emit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.ImperativeStmt;
import de.unika.ipd.grgen.ir.MapAccessExpr;
import de.unika.ipd.grgen.ir.MapInit;
import de.unika.ipd.grgen.ir.MapItem;
import de.unika.ipd.grgen.ir.MapDomainExpr;
import de.unika.ipd.grgen.ir.MapRangeExpr;
import de.unika.ipd.grgen.ir.MapSizeExpr;
import de.unika.ipd.grgen.ir.MapType;
import de.unika.ipd.grgen.ir.SetInit;
import de.unika.ipd.grgen.ir.SetItem;
import de.unika.ipd.grgen.ir.SetSizeExpr;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.Nameof;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SetType;
import de.unika.ipd.grgen.ir.StringIndexOf;
import de.unika.ipd.grgen.ir.StringLastIndexOf;
import de.unika.ipd.grgen.ir.StringLength;
import de.unika.ipd.grgen.ir.StringReplace;
import de.unika.ipd.grgen.ir.StringSubstring;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Typeof;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.VariableExpression;
import de.unika.ipd.grgen.ir.Visited;

public class ActionsGen extends CSharpBase {
	// constants encoding different types of match parts
	// must be consecutive, beginning with MATCH_PART_NODES, ending with terminating dummy-element MATCH_PART_END
	final int MATCH_PART_NODES = 0;
	final int MATCH_PART_EDGES = 1;
	final int MATCH_PART_VARIABLES = 2;
	final int MATCH_PART_EMBEDDED_GRAPHS = 3;
	final int MATCH_PART_ALTERNATIVES = 4;
	final int MATCH_PART_ITERATEDS = 5;
	final int MATCH_PART_INDEPENDENTS = 6;
	final int MATCH_PART_END = 7;

	public ActionsGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix) {
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
		model = be.unit.getActionsGraphModel();
		mg = new ModifyGen(backend, nodeTypePrefix, edgeTypePrefix);
	}

	/**
	 * Generates the subpatterns and actions sourcecode for this unit.
	 */
	public void genActionsAndSubpatterns() {
		StringBuffer sb = new StringBuffer();
		String filename = be.unit.getUnitName() + "Actions_intermediate.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.append("// This file has been generated automatically by GrGen (www.grgen.net)\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
				+ "using System.Text;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
                + "using GRGEN_EXPR = de.unika.ipd.grGen.expression;\n"
				+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + be.unit.getActionsGraphModelName() + ";\n"
				+ "\n"
				+ "namespace de.unika.ipd.grGen.Action_" + be.unit.getUnitName() + "\n"
				+ "{\n");

		for(Rule subpatternRule : be.unit.getSubpatternRules()) {
			genSubpattern(sb, subpatternRule);
		}

		for(Rule actionRule : be.unit.getActionRules()) {
			genAction(sb, actionRule);
		}

		sb.append("\tpublic class " + be.unit.getUnitName() + "_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic " + be.unit.getUnitName() + "_RuleAndMatchingPatterns()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tsubpatterns = new GRGEN_LGSP.LGSPMatchingPattern["+be.unit.getSubpatternRules().size()+"];\n");
		sb.append("\t\t\trules = new GRGEN_LGSP.LGSPRulePattern["+be.unit.getActionRules().size()+"];\n");
		sb.append("\t\t\trulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern["+
				be.unit.getSubpatternRules().size()+"+"+be.unit.getActionRules().size()+"];\n");
		int i = 0;
		for(Rule subpatternRule : be.unit.getSubpatternRules()) {
			sb.append("\t\t\tsubpatterns["+i+"] = Pattern_"+formatIdentifiable(subpatternRule)+".Instance;\n");
			sb.append("\t\t\trulesAndSubpatterns["+i+"] = Pattern_"+formatIdentifiable(subpatternRule)+".Instance;\n");
			++i;
		}
		int j = 0;
		for(Rule actionRule : be.unit.getActionRules()) {
			sb.append("\t\t\trules["+j+"] = Rule_"+formatIdentifiable(actionRule)+".Instance;\n");
			sb.append("\t\t\trulesAndSubpatterns["+i+"+"+j+"] = Rule_"+formatIdentifiable(actionRule)+".Instance;\n");
			++j;
		}
		sb.append("\t\t}\n");
		sb.append("\t\tpublic override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }\n");
		sb.append("\t\tprivate GRGEN_LGSP.LGSPRulePattern[] rules;\n");
		sb.append("\t\tpublic override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }\n");
		sb.append("\t\tprivate GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;\n");
		sb.append("\t\tpublic override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }\n");
		sb.append("\t\tprivate GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;\n");
		sb.append("\t}\n");
		sb.append("\n");

		sb.append("// GrGen insert Actions here\n");
		sb.append("}\n");

		writeFile(be.path, filename, sb);
	}

	/**
	 * Generates the subpattern actions sourcecode for the given subpattern-matching-action
	 */
	private void genSubpattern(StringBuffer sb, Rule subpatternRule) {
		String actionName = formatIdentifiable(subpatternRule);
		String className = "Pattern_"+actionName;
		List<String> staticInitializers = new LinkedList<String>();

		sb.append("\tpublic class " + className + " : GRGEN_LGSP.LGSPMatchingPattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static " + className + " instance = null;\n");
		sb.append("\t\tpublic static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");

		String patGraphVarName = "pat_" + subpatternRule.getPattern().getNameOfGraph();
		genRuleOrSubpatternClassEntities(sb, subpatternRule, patGraphVarName, staticInitializers,
				subpatternRule.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, subpatternRule, className, true);
		sb.append("\n");

		mg.genModify(sb, subpatternRule, true);

		if(subpatternRule.getRight()!=null) {
			genImperativeStatements(sb, subpatternRule, true);
		}

		genStaticConstructor(sb, className, staticInitializers);

		genMatch(sb, subpatternRule.getPattern(), className);

		sb.append("\t}\n");
		sb.append("\n");
	}

	/**
	 * Generates the actions sourcecode for the given matching-action
	 */
	private void genAction(StringBuffer sb, Rule actionRule) {
		String actionName = formatIdentifiable(actionRule);
		String className = "Rule_"+actionName;
		List<String> staticInitializers = new LinkedList<String>();

		sb.append("\tpublic class " + className + " : GRGEN_LGSP.LGSPRulePattern\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static " + className + " instance = null;\n");
		sb.append("\t\tpublic static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");

		String patGraphVarName = "pat_" + actionRule.getPattern().getNameOfGraph();
		genRuleOrSubpatternClassEntities(sb, actionRule, patGraphVarName, staticInitializers,
				actionRule.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, actionRule, className, false);
		sb.append("\n");

		mg.genModify(sb, actionRule, false);

		if(actionRule.getRight()!=null) {
			genImperativeStatements(sb, actionRule, false);
		}

		genStaticConstructor(sb, className, staticInitializers);

		genMatch(sb, actionRule.getPattern(), className);

		sb.append("\t}\n");
		sb.append("\n");
	}

	/**
	 * Generates the match classes (of pattern and contained patterns)
	 */
	private void genMatch(StringBuffer sb, PatternGraph pattern, String className) {
		// generate getters to contained nodes, edges, variables, embedded graphs, alternatives
		genPatternMatchInterface(sb, pattern, pattern.getNameOfGraph(),
				"GRGEN_LIBGR.IMatch", pattern.getNameOfGraph()+"_",
				false, false);

		// generate contained nodes, edges, variables, embedded graphs, alternatives
		// and the implementation of the various getters from IMatch and the pattern specific match interface
		String patGraphVarName = "pat_" + pattern.getNameOfGraph();
		genPatternMatchImplementation(sb, pattern, pattern.getNameOfGraph(),
				patGraphVarName, className, pattern.getNameOfGraph()+"_", false);
	}

	//////////////////////////////////////////////////
	// rule or subpattern class entities generation //
	//////////////////////////////////////////////////

	private void genRuleOrSubpatternClassEntities(StringBuffer sb, Rule rule,
							String patGraphVarName, List<String> staticInitializers,
							String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		PatternGraph pattern = rule.getPattern();
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);
		genLocalMapsAndSets(sb, rule.getLeft(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		genLocalMapsAndSets(sb, rule.getEvals(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		genLocalMapsAndSetsJavaSucks(sb, rule.getReturns(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		if(rule.getRight()!=null) {
			genLocalMapsAndSets(sb, rule.getRight().getImperativeStmts(), staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}
		sb.append("\t\tGRGEN_LGSP.PatternGraph " + patGraphVarName + ";\n");
		sb.append("\n");

		int i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, neg, pathPrefixForElements+negName, staticInitializers,
					pathPrefixForElements + negName + "_",
					alreadyDefinedEntityToNameClone);
			++i;
		}

		i = 0;
		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = "idpt_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, idpt, pathPrefixForElements+idptName, staticInitializers,
					pathPrefixForElements + idptName + "_",
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
				genRuleOrSubpatternClassEntities(sb, altCase, altPatGraphVarName, staticInitializers,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						alreadyDefinedEntityToNameClone);
			}
			++i;
		}

		i = 0;
		for(Rule iter : pattern.getIters()) {
			String iterName = "iter_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, iter, pathPrefixForElements+iterName, staticInitializers,
					pathPrefixForElements + iterName + "_",
					alreadyDefinedEntityToNameClone);
			++i;
		}
	}

	private void genRuleOrSubpatternClassEntities(StringBuffer sb, PatternGraph pattern,
							String patGraphVarName, List<String> staticInitializers,
							String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);
		genLocalMapsAndSets(sb, pattern, staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append("\t\tGRGEN_LGSP.PatternGraph " + patGraphVarName + ";\n");
		sb.append("\n");

		int i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, neg, pathPrefixForElements+negName, staticInitializers,
					pathPrefixForElements + negName + "_",
					alreadyDefinedEntityToNameClone);
			++i;
		}

		i = 0;
		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = "idpt_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, idpt, pathPrefixForElements+idptName, staticInitializers,
					pathPrefixForElements + idptName + "_",
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
				genRuleOrSubpatternClassEntities(sb, altCase, altPatGraphVarName, staticInitializers,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						alreadyDefinedEntityToNameClone);
				}
			++i;
		}

		i = 0;
		for(Rule iter : pattern.getIters()) {
			String iterName = "iter_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, iter, pathPrefixForElements+iterName, staticInitializers,
					pathPrefixForElements + iterName + "_",
					alreadyDefinedEntityToNameClone);
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
			sb.append("\t\tpublic static GRGEN_LIBGR.NodeType[] "
					+ formatEntity(node, pathPrefixForElements) + "_AllowedTypes = ");
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
						sb.append(formatTypeClassRef(type) + ".typeVar, ");
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
			sb.append("\t\tpublic static GRGEN_LIBGR.EdgeType[] "
					+ formatEntity(edge, pathPrefixForElements) + "_AllowedTypes = ");
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
						sb.append(formatTypeClassRef(type) + ".typeVar, ");
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

		sb.append("\t\tpublic enum " + pathPrefixForElements + "IterNums { ");
		for(int i=0; i < pattern.getIters().size(); i++) {
			sb.append("@" + "iter_" + i + ", ");
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

	private void genLocalMapsAndSets(StringBuffer sb, PatternGraph pattern, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true);
		for(Expression expr : pattern.getConditions()) {
			expr.collectNeededEntities(needs);
		}
		genLocalMapsAndSets(sb, needs, staticInitializers);
	}

	private void genLocalMapsAndSets(StringBuffer sb, Collection<EvalStatement> evals, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true);
		for(EvalStatement eval : evals) {
			if(eval instanceof Assignment) {
				Assignment assignment = (Assignment)eval;
				assignment.getExpression().collectNeededEntities(needs);
			}
		}
		genLocalMapsAndSets(sb, needs, staticInitializers);
	}

	// type collision with the method below cause java can't distinguish List<Expression> from List<ImperativeStmt>
	private void genLocalMapsAndSetsJavaSucks(StringBuffer sb, List<Expression> returns, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true);
		for(Expression expr : returns) {
			expr.collectNeededEntities(needs);
		}
		genLocalMapsAndSets(sb, needs, staticInitializers);
	}
	
	private void genLocalMapsAndSets(StringBuffer sb, List<ImperativeStmt> istmts, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true);
		for(ImperativeStmt istmt : istmts) {
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
			else assert false : "unknown ImperativeStmt: " + istmt;
		}
		genLocalMapsAndSets(sb, needs, staticInitializers);
	}

	private void genLocalMapsAndSets(StringBuffer sb, NeededEntities needs, List<String> staticInitializers) {
		sb.append("\n");
		for(Expression mapSetExpr : needs.mapSetExprs) {
			if(mapSetExpr instanceof MapInit) {
				genLocalMap(sb, (MapInit)mapSetExpr, staticInitializers);
			} else if(mapSetExpr instanceof SetInit) {
				genLocalSet(sb, (SetInit)mapSetExpr, staticInitializers);
			}
		}
	}

	private void genLocalMap(StringBuffer sb, MapInit mapInit, List<String> staticInitializers) {
		String mapName = mapInit.getAnonymnousMapName();
		String attrType = formatAttributeType(mapInit.getType());
		if(mapInit.isConstant()) {
			sb.append("\t\tpublic static readonly " + attrType + " " + mapName + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + mapName);
			sb.append("\t\tstatic void init_" + mapName + "() {\n");
			for(MapItem item : mapInit.getMapItems()) {
				sb.append("\t\t\t");
				sb.append(mapName);
				sb.append("[");
				genExpression(sb, item.getKeyExpr(), null);
				sb.append("] = ");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(";\n");
			}
			sb.append("\t\t}\n");
		} else {
			sb.append("\t\tpublic static " + attrType + " fill_" + mapName + "(");
			int itemCounter = 0;
			boolean first = true;
			for(MapItem item : mapInit.getMapItems()) {
				String itemKeyType = formatAttributeType(item.getKeyExpr().getType());
				String itemValueType = formatAttributeType(item.getValueExpr().getType());
				if(first) {
					sb.append(itemKeyType + " itemkey" + itemCounter);
					sb.append(itemValueType + " itemvalue" + itemCounter);
					first = false;
				} else {
					sb.append(", " + itemKeyType + " itemkey" + itemCounter);
					sb.append(itemValueType + " itemvalue" + itemCounter);
				}
				++itemCounter;
			}
			sb.append(") {\n");
			sb.append("\t\t\t" + attrType + " " + mapName + " = " +
					"new " + attrType + "();\n");

			int itemLength = mapInit.getMapItems().size();
			for(itemCounter = 0; itemCounter < itemLength; ++itemCounter) {
				sb.append("\t\t\t" + mapName);
				sb.append("[" + "itemkey" + itemCounter + "] = itemvalue" + itemCounter + ";\n");
			}
			sb.append("\t\t\treturn " + mapName + ";\n");
			sb.append("\t\t}\n");
		}
	}

	private void genLocalSet(StringBuffer sb, SetInit setInit, List<String> staticInitializers) {
		String setName = setInit.getAnonymnousSetName();
		String attrType = formatAttributeType(setInit.getType());
		if(setInit.isConstant()) {
			sb.append("\t\tpublic static readonly " + attrType + " " + setName + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + setName);
			sb.append("\t\tstatic void init_" + setName + "() {\n");
			for(SetItem item : setInit.getSetItems()) {
				sb.append("\t\t\t");
				sb.append(setName);
				sb.append("[");
				genExpression(sb, item.getValueExpr(), null);
				sb.append("] = null;\n");
			}
			sb.append("\t\t}\n");
		} else {
			sb.append("\t\tpublic static " + attrType + " fill_" + setName + "(");
			int itemCounter = 0;
			boolean first = true;
			for(SetItem item : setInit.getSetItems()) {
				String itemType = formatAttributeType(item.getValueExpr().getType());
				if(first) {
					sb.append(itemType + " item" + itemCounter);
					first = false;
				} else {
					sb.append(", " + itemType + " item" + itemCounter);
				}
				++itemCounter;
			}
			sb.append(") {\n");
			sb.append("\t\t\t" + attrType + " " + setName + " = " +
					"new " + attrType + "();\n");

			int itemLength = setInit.getSetItems().size();
			for(itemCounter = 0; itemCounter < itemLength; ++itemCounter) {
				sb.append("\t\t\t" + setName);
				sb.append("[" + "item" + itemCounter + "] = null;\n");
			}
			sb.append("\t\t\treturn " + setName + ";\n");
			sb.append("\t\t}\n");
		}
	}

	/////////////////////////////////////////
	// Rule/Subpattern metadata generation //
	/////////////////////////////////////////

	private void genRuleOrSubpatternInit(StringBuffer sb, MatchingAction action,
			String className, boolean isSubpattern) {
		PatternGraph pattern = action.getPattern();

		sb.append("\t\tprivate " + className + "()\n");
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
		sb.append("\t\tprivate void initialize()\n");
		sb.append("\t\t{\n");

		genPatternGraph(sb, aux, pattern, "", pattern.getNameOfGraph(), patGraphVarName, className,
				alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, 0, action.getParameters(), max);
		sb.append(aux);
		sb.append("\n");
		sb.append("\t\t\tpatternGraph = " + patGraphVarName + ";\n");

		sb.append("\t\t}\n");
	}

	private int genPatternGraph(StringBuffer sb, StringBuilder aux, PatternGraph pattern,
								String pathPrefix, String patternName, // negatives without name, have to compute it and hand it in
								String patGraphVarName, String className,
								HashMap<Entity, String> alreadyDefinedEntityToName,
								HashMap<Identifiable, String> alreadyDefinedIdentifiableToName,
								int condCntInit, List<Entity> parameters, double max) {
		genElementsRequiredByPatternGraph(sb, aux, pattern, pathPrefix, patternName, patGraphVarName, className,
										  alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, condCntInit, parameters, max);

		sb.append("\t\t\t" + patGraphVarName + " = new GRGEN_LGSP.PatternGraph(\n");
		sb.append("\t\t\t\t\"" + patternName + "\",\n");
		sb.append("\t\t\t\t\"" + pathPrefix + "\",\n");
		sb.append("\t\t\t\t" + (pattern.isPatternpathLocked() ? "true" : "false") + ",\n" );

		String pathPrefixForElements = pathPrefix+patternName+"_";

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternNode[] ");
		genEntitySet(sb, pattern.getNodes(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternEdge[] ");
		genEntitySet(sb, pattern.getEdges(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternVariable[] ");
		genEntitySet(sb, pattern.getVars(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternGraphEmbedding[] ");
		genSubpatternUsageSet(sb, pattern.getSubpatternUsages(), "", "", true, pathPrefixForElements, alreadyDefinedIdentifiableToName);
		sb.append(", \n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.Alternative[] { ");
		for(int i = 0; i < pattern.getAlts().size(); ++i) {
			sb.append(pathPrefixForElements+"alt_" + i + ", ");
		}
		sb.append(" }, \n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternGraph[] { ");
		for(int i = 0; i < pattern.getIters().size(); ++i) {
			sb.append(pathPrefixForElements+"iter_" + i + ", ");
		}
		sb.append(" }, \n");

		sb.append("\t\t\t\t" + pathPrefixForElements + "minMatches,\n");
		sb.append("\t\t\t\t" + pathPrefixForElements + "maxMatches,\n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternGraph[] { ");
		for(int i = 0; i < pattern.getNegs().size(); ++i) {
			sb.append(pathPrefixForElements+"neg_" + i + ", ");
		}
		sb.append(" }, \n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternGraph[] { ");
		for(int i = 0; i < pattern.getIdpts().size(); ++i) {
			sb.append(pathPrefixForElements+"idpt_" + i + ", ");
		}
		sb.append(" }, \n");

		sb.append("\t\t\t\tnew GRGEN_LGSP.PatternCondition[] { ");
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

		sb.append("\t\t\t\t" + pathPrefixForElements + "isNodeHomomorphicGlobal,\n");

		sb.append("\t\t\t\t" + pathPrefixForElements + "isEdgeHomomorphicGlobal\n");

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

		for(int j = 0; j < pattern.getIters().size(); j++) {
			String iterName = "iter_" + j;
			sb.append("\t\t\t" + pathPrefixForElements+iterName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		for(int j = 0; j < pattern.getNegs().size(); j++) {
			String negName = "neg_" + j;
			sb.append("\t\t\t" + pathPrefixForElements+negName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		for(int j = 0; j < pattern.getIdpts().size(); j++) {
			String idptName = "idpt_" + j;
			sb.append("\t\t\t" + pathPrefixForElements+idptName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		sb.append("\n");

		return condCnt;
	}

	private void genElementsRequiredByPatternGraph(StringBuffer sb, StringBuilder aux, PatternGraph pattern,
												   String pathPrefix, String patternName,
												   String patGraphVarName, String className,
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

		sb.append("\t\t\tint[] " + pathPrefixForElements + "minMatches = "
				+ "new int[" + pattern.getIters().size() + "] ");
		if(pattern.getIters().size() > 0) {
			sb.append("{\n\t\t\t\t");
			for(Rule iter : pattern.getIters()) {
				sb.append(iter.getMinMatches() + ", ");
			}
			sb.append("\n\t\t\t}");
		}
		sb.append(";\n");

		sb.append("\t\t\tint[] " + pathPrefixForElements + "maxMatches = "
				+ "new int[" + pattern.getIters().size() + "] ");
		if(pattern.getIters().size() > 0) {
			sb.append("{\n\t\t\t\t");
			for(Rule iter : pattern.getIters()) {
				sb.append(iter.getMaxMatches() + ", ");
			}
			sb.append("\n\t\t\t}");
		}
		sb.append(";\n");

		for(Node node : pattern.getNodes()) {
			if(alreadyDefinedEntityToName.get(node)!=null) {
				continue;
			}
			String nodeName = formatEntity(node, pathPrefixForElements);
			sb.append("\t\t\tGRGEN_LGSP.PatternNode " + nodeName + " = new GRGEN_LGSP.PatternNode(");
			sb.append("(int) GRGEN_MODEL.NodeTypes.@" + formatIdentifiable(node.getType()) + ", \"" + formatElementInterfaceRef(node.getType()) + "\", ");
			sb.append("\"" + nodeName + "\", \"" + formatIdentifiable(node) + "\", ");
			sb.append(nodeName + "_AllowedTypes, ");
			sb.append(nodeName + "_IsAllowedType, ");
			appendPrio(sb, node, max);
			sb.append(parameters.indexOf(node)+", ");
			sb.append(node.getMaybeNull()?"true);\n":"false);\n");
			alreadyDefinedEntityToName.put(node, nodeName);
			aux.append("\t\t\t" + nodeName + ".PointOfDefinition = " + (parameters.indexOf(node)==-1 ? patGraphVarName : "null") + ";\n");

			node.setPointOfDefinition(pattern);
		}

		for(Edge edge : pattern.getEdges()) {
			if(alreadyDefinedEntityToName.get(edge)!=null) {
				continue;
			}
			String edgeName = formatEntity(edge, pathPrefixForElements);
			sb.append("\t\t\tGRGEN_LGSP.PatternEdge " + edgeName + " = new GRGEN_LGSP.PatternEdge(");
			sb.append((edge.hasFixedDirection() ? "true" : "false") + ", ");
			sb.append("(int) GRGEN_MODEL.EdgeTypes.@" + formatIdentifiable(edge.getType()) + ", \"" + formatElementInterfaceRef(edge.getType()) + "\", ");
			sb.append("\"" + edgeName + "\", \"" + formatIdentifiable(edge) + "\", ");
			sb.append(edgeName + "_AllowedTypes, ");
			sb.append(edgeName + "_IsAllowedType, ");
			appendPrio(sb, edge, max);
			sb.append(parameters.indexOf(edge)+", ");
			sb.append(edge.getMaybeNull()?"true);\n":"false);\n");
			alreadyDefinedEntityToName.put(edge, edgeName);
			aux.append("\t\t\t" + edgeName + ".PointOfDefinition = " + (parameters.indexOf(edge)==-1 ? patGraphVarName : "null") + ";\n");

			edge.setPointOfDefinition(pattern);
		}

		for(Variable var : pattern.getVars()) {
			if(alreadyDefinedEntityToName.get(var)!=null) {
				continue;
			}
			String varName = formatEntity(var, pathPrefixForElements);
			sb.append("\t\t\tGRGEN_LGSP.PatternVariable " + varName
					+ " = new GRGEN_LGSP.PatternVariable(");
			sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(var)
					+ ")), \"" + varName + "\", \"" + formatIdentifiable(var) + "\", ");
			sb.append(parameters.indexOf(var)+");\n");
			alreadyDefinedEntityToName.put(var, varName);
		}

		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			if(alreadyDefinedIdentifiableToName.get(sub)!=null) {
				continue;
			}
			String subName = formatIdentifiable(sub, pathPrefixForElements);
			sb.append("\t\t\tGRGEN_LGSP.PatternGraphEmbedding " + subName
					+ " = new GRGEN_LGSP.PatternGraphEmbedding(");
			sb.append("\"" + formatIdentifiable(sub) + "\", ");
			sb.append("Pattern_"+ sub.getSubpatternAction().getIdent().toString() + ".Instance, \n");
			sb.append("\t\t\t\tnew GRGEN_EXPR.Expression[] {\n");
			NeededEntities needs = new NeededEntities(true, true, true, false, false, true);
			for(Expression expr : sub.getSubpatternConnections()) {
				expr.collectNeededEntities(needs);
				sb.append("\t\t\t\t\t");
				genExpressionTree(sb, expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
				sb.append(",\n");
			}
			sb.append("\t\t\t\t}, \n");
			sb.append("\t\t\t\tnew string[] ");
			genEntitySet(sb, needs.nodes, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new string[] ");
			genEntitySet(sb, needs.edges, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new string[] ");
			genEntitySet(sb, needs.variables, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new GRGEN_LIBGR.VarType[] ");
			genVarTypeSet(sb, needs.variables, true);
			sb.append(");\n");
			alreadyDefinedIdentifiableToName.put(sub, subName);
			aux.append("\t\t\t" + subName + ".PointOfDefinition = " + patGraphVarName + ";\n");
		}

		int condCnt = condCntInit;
		for(Expression expr : pattern.getConditions()) {
			NeededEntities needs = new NeededEntities(true, true, true, false, false, true);
			expr.collectNeededEntities(needs);
			sb.append("\t\t\tGRGEN_LGSP.PatternCondition cond_" + condCnt
					+ " = new GRGEN_LGSP.PatternCondition(\n"
					+ "\t\t\t\t");
			genExpressionTree(sb, expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(",\n");
			sb.append("\t\t\t\tnew string[] ");
			genEntitySet(sb, needs.nodes, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new string[] ");
			genEntitySet(sb, needs.edges, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new string[] ");
			genEntitySet(sb, needs.variables, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(", new GRGEN_LIBGR.VarType[] ");
			genVarTypeSet(sb, needs.variables, true);
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
										  altPatGraphVarName, className,
										  alreadyDefinedEntityToNameClone,
										  alreadyDefinedIdentifiableToNameClone,
										  condCnt, parameters, max);
			}
			++i;
		}

		i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			sb.append("\t\t\tGRGEN_LGSP.Alternative " + pathPrefixForElements+altName + " = new GRGEN_LGSP.Alternative( ");
			sb.append("\"" + altName + "\", ");
			sb.append("\"" + pathPrefixForElements + "\", ");
			sb.append("new GRGEN_LGSP.PatternGraph[] ");
			genAlternativesSet(sb, alt.getAlternativeCases(), pathPrefixForElements+altName+"_", "", true);
			sb.append(" );\n\n");
			++i;
		}

		i = 0;
		for(Rule iter : pattern.getIters()) {
			String iterName = "iter_" + i;
			PatternGraph iterPattern = iter.getLeft();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			condCnt = genPatternGraph(sb, aux, iterPattern,
									  pathPrefixForElements, iterName,
									  pathPrefixForElements+iterName, className,
									  alreadyDefinedEntityToNameClone,
									  alreadyDefinedIdentifiableToNameClone,
									  condCnt, parameters, max);
			++i;
		}

		i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			condCnt = genPatternGraph(sb, aux, neg,
									  pathPrefixForElements, negName,
									  pathPrefixForElements+negName, className,
									  alreadyDefinedEntityToNameClone,
									  alreadyDefinedIdentifiableToNameClone,
									  condCnt, parameters, max);
			++i;
		}

		i = 0;
		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = "idpt_" + i;
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			condCnt = genPatternGraph(sb, aux, idpt,
									  pathPrefixForElements, idptName,
									  pathPrefixForElements+idptName, className,
									  alreadyDefinedEntityToNameClone,
									  alreadyDefinedIdentifiableToNameClone,
									  condCnt, parameters, max);
			++i;
		}
	}

	private void genRuleParamResult(StringBuffer sb, MatchingAction action, boolean isSubpattern) {
		sb.append("\t\t\tinputs = new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity ent : action.getParameters()) {
			if(ent instanceof Variable) {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(ent) + ")), ");
			} else {
				GraphEntity gent = (GraphEntity)ent;
				sb.append(formatTypeClassRef(gent.getParameterInterfaceType()!=null ? gent.getParameterInterfaceType() : gent.getType()) + ".typeVar, ");
			}
		}
		sb.append("};\n");

		sb.append("\t\t\tinputNames = new string[] { ");
		for(Entity ent : action.getParameters())
			sb.append("\"" + formatEntity(ent, action.getPattern().getNameOfGraph()+"_") + "\", ");
		sb.append("};\n");

		if(!isSubpattern) {
			sb.append("\t\t\toutputs = new GRGEN_LIBGR.GrGenType[] { ");
			for(Expression expr : action.getReturns()) {
				if(expr instanceof GraphEntityExpression)
					sb.append(formatTypeClassRef(expr.getType()) + ".typeVar, ");
				else
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(expr.getType()) + ")), ");
			}
			sb.append("};\n");
		}
	}

	private void genImperativeStatements(StringBuffer sb, Rule rule, boolean isSubpattern) {
		String actionName = formatIdentifiable(rule);
		sb.append("#if INITIAL_WARMUP\t\t// GrGen imperative statement section: " + (isSubpattern ? "Pattern_" : "Rule_") + actionName + "\n");
		int xgrsID = 0;
		for(ImperativeStmt istmt : rule.getRight().getImperativeStmts()) {
			if(istmt instanceof Emit) {
				// nothing to do
			} else if (istmt instanceof Exec) {
				Exec exec = (Exec) istmt;

				sb.append("\t\tpublic static GRGEN_LGSP.LGSPXGRSInfo XGRSInfo_" + xgrsID
						+ " = new GRGEN_LGSP.LGSPXGRSInfo(new string[] {");
				for(Rule actionRule : be.unit.getActionRules()) {
					sb.append("\"" + actionRule.getIdent() + "\", ");
				}
				sb.append("},\n");
				sb.append("\t\t\tnew string[] {");
				for(Entity neededEntity : exec.getNeededEntities()) {
					sb.append("\"" + neededEntity.getIdent() + "\", ");
				}
				sb.append("},\n");
				sb.append("\t\t\tnew string[] {");
				for(Entity neededEntity : exec.getNeededEntities()) {
					if(neededEntity instanceof GraphEntity && ((GraphEntity)neededEntity).getParameterInterfaceType()!=null) {
						sb.append("\"" + formatType(((GraphEntity)neededEntity).getParameterInterfaceType()) + "\", ");
					} else {
						sb.append("\"" + formatType(neededEntity.getType()) + "\", ");
					}
				}
				sb.append("},\n");
				sb.append("\t\t\t\"" + exec.getXGRSString().replace("\"", "\\\"") + "\");\n");
				sb.append("\t\tprivate void ApplyXGRS_" + xgrsID++ + "(GRGEN_LGSP.LGSPGraph graph");
				for(Entity neededEntity : exec.getNeededEntities()) {
					sb.append(", " + formatType(neededEntity.getType()) + " var_" + neededEntity.getIdent());
				}
				sb.append(") {}\n");
			} else assert false : "unknown ImperativeStmt: " + istmt + " in " + rule;
		}
		sb.append("#endif\n");
	}

	//////////////////////////////////////////
	// Condition expression tree generation //
	//////////////////////////////////////////

	private void genExpressionTree(StringBuffer sb, Expression expr, String className,
			String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			String opNamePrefix = "";
			if(op.getType() instanceof SetType || op.getType() instanceof MapType)
				opNamePrefix = "DICT_";
			if(op.getOpCode()==Operator.EQ || op.getOpCode()==Operator.NE
				|| op.getOpCode()==Operator.GT || op.getOpCode()==Operator.GE
				|| op.getOpCode()==Operator.LT || op.getOpCode()==Operator.LE) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof SetType || opnd.getType() instanceof MapType) {
					opNamePrefix = "DICT_";
				}
			}

			sb.append("new GRGEN_EXPR." + opNamePrefix + Operator.opNames[op.getOpCode()] + "(");
			switch (op.arity()) {
				case 1:
					genExpressionTree(sb, op.getOperand(0), className, pathPrefix, alreadyDefinedEntityToName);
					break;
				case 2:
					genExpressionTree(sb, op.getOperand(0), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					genExpressionTree(sb, op.getOperand(1), className, pathPrefix, alreadyDefinedEntityToName);
					break;
				case 3:
					if(op.getOpCode()==Operator.COND) {
						genExpressionTree(sb, op.getOperand(0), className, pathPrefix, alreadyDefinedEntityToName);
						sb.append(", ");
						genExpressionTree(sb, op.getOperand(1), className, pathPrefix, alreadyDefinedEntityToName);
						sb.append(", ");
						genExpressionTree(sb, op.getOperand(2), className, pathPrefix, alreadyDefinedEntityToName);
						break;
					}
					// FALLTHROUGH
				default:
					throw new UnsupportedOperationException(
						"Unsupported operation arity (" + op.arity() + ")");
			}
			sb.append(")");
		}
		else if(expr instanceof Qualification) {
			Qualification qual = (Qualification) expr;
			Entity owner = qual.getOwner();
			Entity member = qual.getMember();
			sb.append("new GRGEN_EXPR.Qualification(\"" + formatElementInterfaceRef(owner.getType())
				+ "\", \"" + formatEntity(owner, pathPrefix, alreadyDefinedEntityToName) + "\", \"" + formatIdentifiable(member) + "\")");

		}
		else if(expr instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression) expr;
			sb.append("new GRGEN_EXPR.ConstantEnumExpression(\"" + enumExp.getType().getIdent().toString()
					+ "\", \"" + enumExp.getEnumItem().toString() + "\")");
		}
		else if(expr instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) expr;
			sb.append("new GRGEN_EXPR.Constant(\"" + escapeDoubleQuotes(getValueAsCSSharpString(constant)) + "\")");
		}
		else if(expr instanceof Nameof) {
			Nameof no = (Nameof) expr;
			sb.append("new GRGEN_EXPR.Nameof("
					+ (no.getEntity()==null ? "null" : "\""+formatEntity(no.getEntity(), pathPrefix, alreadyDefinedEntityToName)+"\"")
					+ ")");
		}
		else if(expr instanceof Typeof) {
			Typeof to = (Typeof) expr;
			sb.append("new GRGEN_EXPR.Typeof(\"" + formatEntity(to.getEntity(), pathPrefix, alreadyDefinedEntityToName) + "\")");
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			String typeName = getTypeNameForCast(cast);
			if(typeName == "object") {
				// no cast needed
				genExpressionTree(sb, cast.getExpression(), className, pathPrefix, alreadyDefinedEntityToName);
			} else {
				sb.append("new GRGEN_EXPR.Cast(\"" + typeName + "\", ");
				genExpressionTree(sb, cast.getExpression(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(")");
			}
		}
		else if(expr instanceof VariableExpression) {
			Variable var = ((VariableExpression) expr).getVariable();
			sb.append("new GRGEN_EXPR.VariableExpression(\"" + formatEntity(var, pathPrefix, alreadyDefinedEntityToName) + "\")");
		}
		else if(expr instanceof GraphEntityExpression) {
			GraphEntity ent = ((GraphEntityExpression) expr).getGraphEntity();
			sb.append("new GRGEN_EXPR.GraphEntityExpression(\"" + formatEntity(ent, pathPrefix, alreadyDefinedEntityToName) + "\")");
		}
		else if(expr instanceof Visited) {
			Visited vis = (Visited) expr;
			sb.append("new GRGEN_EXPR.Visited(\"" + formatEntity(vis.getEntity(), pathPrefix, alreadyDefinedEntityToName) + "\", ");
			genExpressionTree(sb, vis.getVisitorID(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringLength) {
			StringLength strlen = (StringLength) expr;
			sb.append("new GRGEN_EXPR.StringLength(");
			genExpressionTree(sb, strlen.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringSubstring) {
			StringSubstring strsubstr = (StringSubstring) expr;
			sb.append("new GRGEN_EXPR.StringSubstring(");
			genExpressionTree(sb, strsubstr.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strsubstr.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strsubstr.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringIndexOf) {
			StringIndexOf strio = (StringIndexOf) expr;
			sb.append("new GRGEN_EXPR.StringIndexOf(");
			genExpressionTree(sb, strio.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strio.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringLastIndexOf) {
			StringLastIndexOf strlio = (StringLastIndexOf) expr;
			sb.append("new GRGEN_EXPR.StringLastIndexOf(");
			genExpressionTree(sb, strlio.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strlio.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringReplace) {
			StringReplace strrepl = (StringReplace) expr;
			sb.append("new GRGEN_EXPR.StringReplace(");
			genExpressionTree(sb, strrepl.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strrepl.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strrepl.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strrepl.getReplaceStrExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapAccessExpr) {
			MapAccessExpr ma = (MapAccessExpr)expr;
			sb.append("new GRGEN_EXPR.MapAccess(");
			genExpressionTree(sb, ma.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ma.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapSizeExpr) {
			MapSizeExpr ms = (MapSizeExpr)expr;
			sb.append("new GRGEN_EXPR.MapSize(");
			genExpressionTree(sb, ms.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapDomainExpr) {
			MapDomainExpr md = (MapDomainExpr)expr;
			sb.append("new GRGEN_EXPR.MapDomain(");
			genExpressionTree(sb, md.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapRangeExpr) {
			MapRangeExpr mr = (MapRangeExpr)expr;
			sb.append("new GRGEN_EXPR.MapRange(");
			genExpressionTree(sb, mr.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SetSizeExpr) {
			SetSizeExpr ss = (SetSizeExpr)expr;
			sb.append("new GRGEN_EXPR.SetSize(");
			genExpressionTree(sb, ss.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapInit) {
			MapInit mi = (MapInit)expr;
			if(mi.isConstant()) {
				sb.append("new GRGEN_EXPR.StaticMap(\"" + className + "\", \"" + mi.getAnonymnousMapName() + "\")");
			} else {
				sb.append("new GRGEN_EXPR.MapConstructor(\"" + className + "\", \"" + mi.getAnonymnousMapName() + "\",");
				int openParenthesis = 0;
				for(MapItem item : mi.getMapItems()) {
					sb.append("new GRGEN_EXPR.MapItem(");
					genExpressionTree(sb, item.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					genExpressionTree(sb, item.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					++openParenthesis;
				}
				sb.append("null");
				for(int i=0; i<openParenthesis; ++i) sb.append(")");
				sb.append(")");
			}
		}
		else if (expr instanceof SetInit) {
			SetInit si = (SetInit)expr;
			if(si.isConstant()) {
				sb.append("new GRGEN_EXPR.StaticSet(\"" + className + "\", \"" + si.getAnonymnousSetName() + "\")");
			} else {
				sb.append("new GRGEN_EXPR.SetConstructor(\"" + className + "\", \"" + si.getAnonymnousSetName() + "\", ");
				int openParenthesis = 0;
				for(SetItem item : si.getSetItems()) {
					sb.append("new GRGEN_EXPR.SetItem(");
					genExpressionTree(sb, item.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					++openParenthesis;
				}
				sb.append("null");
				for(int i=0; i<openParenthesis; ++i) sb.append(")");
				sb.append(")");
			}
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + expr + ")");
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
		for(PatternGraph idpt : pattern.getIdpts()) {
			max = computePriosMax(max, idpt);
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

	/////////////////////////////////////////////
	// Static constructor calling static inits //
	/////////////////////////////////////////////

	protected void genStaticConstructor(StringBuffer sb, String className, List<String> staticInitializers)
	{
		sb.append("\n");
		sb.append("\t\tstatic " + className + "() {\n");
		for(String staticInit : staticInitializers) {
			sb.append("\t\t\t" + staticInit + "();\n");
		}
		sb.append("\t\t}\n");
		sb.append("\n");
	}

	//////////////////////////////
	// Match objects generation //
	//////////////////////////////

	private void genPatternMatchInterface(StringBuffer sb, PatternGraph pattern, String name,
			String base, String pathPrefixForElements, boolean iterated, boolean alternativeCase)
	{
		genMatchInterface(sb, pattern, name,
				base, pathPrefixForElements, iterated, alternativeCase);

		int i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			genPatternMatchInterface(sb, neg, pathPrefixForElements+negName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + negName + "_",
					false, false);
			++i;
		}

		i = 0;
		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = "idpt_" + i;
			genPatternMatchInterface(sb, idpt, pathPrefixForElements+idptName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + idptName + "_",
					false, false);
			++i;
		}

		i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			genAlternativeMatchInterface(sb, pathPrefixForElements + altName);
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				genPatternMatchInterface(sb, altCasePattern, altPatName,
						"IMatch_"+pathPrefixForElements+altName,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						false, true);
			}
			++i;
		}

		i = 0;
		for(Rule iter : pattern.getIters()) {
			String iterName = "iter_" + i;
			PatternGraph iterPattern = iter.getLeft();
			genPatternMatchInterface(sb, iterPattern, pathPrefixForElements+iterName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + iterName + "_",
					true, false);
			++i;
		}
	}

	private void genPatternMatchImplementation(StringBuffer sb, PatternGraph pattern, String name,
			String patGraphVarName, String className,
			String pathPrefixForElements, boolean iterated)
	{
		genMatchImplementation(sb, pattern, name,
				patGraphVarName, className, pathPrefixForElements, iterated);

		int i = 0;
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = "neg_" + i;
			genPatternMatchImplementation(sb, neg, pathPrefixForElements+negName,
					pathPrefixForElements+negName, className,
					pathPrefixForElements+negName+"_", false);
			++i;
		}

		i = 0;
		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = "idpt_" + i;
			genPatternMatchImplementation(sb, idpt, pathPrefixForElements+idptName,
					pathPrefixForElements+idptName, className,
					pathPrefixForElements+idptName+"_", false);
			++i;
		}

		i = 0;
		for(Alternative alt : pattern.getAlts()) {
			String altName = "alt_" + i;
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				genPatternMatchImplementation(sb, altCasePattern, altPatName,
						altPatName, className,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_", false);
			}
			++i;
		}

		i = 0;
		for(Rule iter : pattern.getIters()) {
			String iterName = "iter_" + i;
			PatternGraph iterPattern = iter.getLeft();
			genPatternMatchImplementation(sb, iterPattern, pathPrefixForElements+iterName,
					pathPrefixForElements+iterName, className,
					pathPrefixForElements+iterName+"_", true);
			++i;
		}
	}

	private void genMatchInterface(StringBuffer sb, PatternGraph pattern,
			String name, String base,
			String pathPrefixForElements, boolean iterated, boolean alternativeCase)
	{
		String interfaceName = "IMatch_" + name;
		sb.append("\t\tpublic interface "+interfaceName+" : "+base+"\n");
		sb.append("\t\t{\n");

		for(int i=MATCH_PART_NODES; i<MATCH_PART_END; ++i) {
			genMatchedEntitiesInterface(sb, pattern,
					name, i, pathPrefixForElements);
		}

		sb.append("\t\t\t// further match object stuff\n");

		if(iterated) {
			sb.append("\t\t\tbool IsNullMatch { get; }\n");
		}

		if(alternativeCase) {
			sb.append("\t\t\tnew void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");
		} else {
			sb.append("\t\t\tvoid SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");
		}
		sb.append("\t\t}\n");
		sb.append("\n");
	}

	private void genAlternativeMatchInterface(StringBuffer sb, String name)
	{
		String interfaceName = "IMatch_" + name;
		sb.append("\t\tpublic interface "+interfaceName+" : GRGEN_LIBGR.IMatch\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tvoid SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");

		sb.append("\t\t}\n");
		sb.append("\n");
	}

	private void genMatchImplementation(StringBuffer sb, PatternGraph pattern, String name,
			String patGraphVarName, String ruleClassName,
			String pathPrefixForElements, boolean iterated)
	{
		String interfaceName = "IMatch_" + name;
		String className = "Match_" + name;
		sb.append("\t\tpublic class "+className+" : GRGEN_LGSP.ListElement<"+className+">, "+interfaceName+"\n");
		sb.append("\t\t{\n");

		for(int i=MATCH_PART_NODES; i<MATCH_PART_END; ++i) {
			genMatchedEntitiesImplementation(sb, pattern, name,
					i, pathPrefixForElements);
			genMatchEnum(sb, pattern, name,
					i, pathPrefixForElements);
			genIMatchImplementation(sb, pattern, name,
					i, pathPrefixForElements);
			sb.append("\t\t\t\n");
		}

		sb.append("\t\t\tpublic GRGEN_LIBGR.IPatternGraph Pattern { get { return "+ruleClassName+".instance."+patGraphVarName+"; } }\n");
		if(iterated) {
			sb.append("\t\t\tpublic bool IsNullMatch { get { return _isNullMatch; } }\n");
			sb.append("\t\t\tpublic bool _isNullMatch;\n");
		}
		sb.append("\t\t\tpublic GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }\n");
		sb.append("\t\t\tpublic GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;\n");
		sb.append("\t\t\tpublic void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }\n");
		sb.append("\t\t\tpublic override string ToString() { return \"Match of \" + Pattern.Name; }\n");

		sb.append("\t\t}\n");
		sb.append("\n");
	}

	private void genMatchedEntitiesInterface(StringBuffer sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		// the getters for the elements
		sb.append("\t\t\t//"+matchedEntitiesNamePlural(which)+"\n");
		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.append("\t\t\t"+formatElementInterfaceRef(node.getType())+" "+formatEntity(node)+" { get; }\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.append("\t\t\t"+formatElementInterfaceRef(edge.getType())+" "+formatEntity(edge)+" { get; }\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.append("\t\t\t"+formatAttributeType(var.getType())+" @"+formatEntity(var)+" { get; }\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.append("\t\t\t@"+matchType(sub.getSubpatternAction().getPattern(), true, "")+" @"+formatIdentifiable(sub)+" { get; }\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(int i=0; i<pattern.getAlts().size(); ++i) {
				sb.append("\t\t\tIMatch_"+pathPrefixForElements+"alt_"+i+" alt_"+i+" { get; }\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(int i=0; i<pattern.getIters().size(); ++i) {
				sb.append("\t\t\tGRGEN_LIBGR.IMatchesExact<IMatch_"+pathPrefixForElements+"iter_"+i+"> iter_"+i+" { get; }\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(int i=0; i<pattern.getIdpts().size(); ++i) {
				sb.append("\t\t\tIMatch_"+pathPrefixForElements+"idpt_"+i+" idpt_"+i+" { get; }\n");
			}
			break;
		default:
			assert(false);
		}
	}

	private void genMatchedEntitiesImplementation(StringBuffer sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		// the element itself and the getter for it
		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.append("\t\t\tpublic "+formatElementInterfaceRef(node.getType())+" "+formatEntity(node)
						+ " { get { return ("+formatElementInterfaceRef(node.getType())+")"+formatEntity(node, "_")+"; } }\n");
			}
			for(Node node : pattern.getNodes()) {
				sb.append("\t\t\tpublic GRGEN_LGSP.LGSPNode "+formatEntity(node, "_")+";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.append("\t\t\tpublic "+formatElementInterfaceRef(edge.getType())+" "+formatEntity(edge)
						+ " { get { return ("+formatElementInterfaceRef(edge.getType())+")"+formatEntity(edge, "_")+"; } }\n");
			}
			for(Edge edge : pattern.getEdges()) {
				sb.append("\t\t\tpublic GRGEN_LGSP.LGSPEdge "+formatEntity(edge, "_")+";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.append("\t\t\tpublic "+formatAttributeType(var.getType())+" "+formatEntity(var)+" { get { return "+formatEntity(var, "_")+"; } }\n");
			}
			for(Variable var : pattern.getVars()) {
				sb.append("\t\t\tpublic "+formatAttributeType(var.getType())+" "+formatEntity(var, "_")+";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.append("\t\t\tpublic @"+matchType(sub.getSubpatternAction().getPattern(), true, "")+" @"+formatIdentifiable(sub)+" { get { return @"+formatIdentifiable(sub, "_")+"; } }\n");
			}
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.append("\t\t\tpublic @"+matchType(sub.getSubpatternAction().getPattern(), true, "")+" @"+formatIdentifiable(sub, "_")+";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(int i=0; i<pattern.getAlts().size(); ++i) {
				sb.append("\t\t\tpublic IMatch_"+pathPrefixForElements+"alt_"+i+" alt_"+i+" { get { return _alt_"+i+"; } }\n");
			}
			for(int i=0; i<pattern.getAlts().size(); ++i) {
				sb.append("\t\t\tpublic IMatch_"+pathPrefixForElements+"alt_"+i+" _alt_"+i+";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(int i=0; i<pattern.getIters().size(); ++i) {
				sb.append("\t\t\tpublic GRGEN_LIBGR.IMatchesExact<IMatch_"+pathPrefixForElements+"iter_"+i+"> iter_"+i+" { get { return _iter_"+i+"; } }\n");
			}
			for(int i=0; i<pattern.getIters().size(); ++i) {
				sb.append("\t\t\tpublic GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefixForElements+"iter_"+i+", IMatch_"+pathPrefixForElements+"iter_"+i+"> _iter_"+i+";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(int i=0; i<pattern.getIdpts().size(); ++i) {
				sb.append("\t\t\tpublic IMatch_"+pathPrefixForElements+"idpt_"+i+" idpt_"+i+" { get { return _idpt_"+i+"; } }\n");
			}
			for(int i=0; i<pattern.getIdpts().size(); ++i) {
				sb.append("\t\t\tpublic IMatch_"+pathPrefixForElements+"idpt_"+i+" _idpt_"+i+";\n");
			}
			break;
		default:
			assert(false);
		}
	}


	private void genIMatchImplementation(StringBuffer sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		// the various match part getters

		String enumerableName = "GRGEN_LGSP."+matchedEntitiesNamePlural(which)+"_Enumerable";
		String enumeratorName = "GRGEN_LGSP."+matchedEntitiesNamePlural(which)+"_Enumerator";
		String typeOfMatchedEntities = typeOfMatchedEntities(which);
		int numberOfMatchedEntities = numOfMatchedEntities(which, pattern);
		String matchedEntitiesNameSingular = matchedEntitiesNameSingular(which);
		String matchedEntitiesNamePlural = matchedEntitiesNamePlural(which);

		sb.append("\t\t\tpublic IEnumerable<"+typeOfMatchedEntities+"> "+matchedEntitiesNamePlural+" { get { return new "+enumerableName+"(this); } }\n");
		sb.append("\t\t\tpublic IEnumerator<"+typeOfMatchedEntities+"> "+matchedEntitiesNamePlural+"Enumerator { get { return new " + enumeratorName + "(this); } }\n");
		sb.append("\t\t\tpublic int NumberOf"+matchedEntitiesNamePlural+" { get { return " + numberOfMatchedEntities + ";} }\n");
		sb.append("\t\t\tpublic "+typeOfMatchedEntities+" get"+matchedEntitiesNameSingular+"At(int index)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tswitch(index) {\n");

		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.append("\t\t\t\tcase (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(node) + ": return " + formatEntity(node, "_") + ";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.append("\t\t\t\tcase (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(edge) + ": return " + formatEntity(edge, "_") + ";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.append("\t\t\t\tcase (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(var) + ": return " + formatEntity(var, "_") + ";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.append("\t\t\t\tcase (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(sub) + ": return " + formatIdentifiable(sub, "_") + ";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(int i=0; i < pattern.getAlts().size(); ++i) {
				sb.append("\t\t\t\tcase (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + "alt_" + i + ": return _alt_" + i + ";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(int i=0; i < pattern.getIters().size(); ++i) {
				sb.append("\t\t\t\tcase (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + "iter_" + i + ": return _iter_" + i + ";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(int i=0; i < pattern.getIdpts().size(); ++i) {
				sb.append("\t\t\t\tcase (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + "idpt_" + i + ": return _idpt_" + i + ";\n");
			}
			break;
		default:
			assert(false);
			break;
		}

		sb.append("\t\t\t\tdefault: return null;\n");
		sb.append("\t\t\t\t}\n");
	    sb.append("\t\t\t}\n");
	}


	private void genMatchEnum(StringBuffer sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		// generate enum mapping entity names to consecutive integers
		sb.append("\t\t\tpublic enum "+entitiesEnumName(which, pathPrefixForElements)+" { ");
		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.append("@"+formatIdentifiable(node)+", ");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.append("@"+formatIdentifiable(edge)+", ");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.append("@"+formatIdentifiable(var)+", ");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.append("@"+formatIdentifiable(sub)+", ");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(int i=0; i < pattern.getAlts().size(); i++) {
				sb.append("@"+"alt_"+ i +", ");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(int i=0; i < pattern.getIters().size(); i++) {
				sb.append("@"+"iter_"+ i +", ");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(int i=0; i < pattern.getIdpts().size(); i++) {
				sb.append("@"+"idpt_"+ i +", ");
			}
			break;
		default:
			assert(false);
			break;
		}
		sb.append("END_OF_ENUM };\n");
	}


	private String matchedEntitiesNameSingular(int which)
	{
		switch(which)
		{
		case MATCH_PART_NODES:
			return "Node";
		case MATCH_PART_EDGES:
			return "Edge";
		case MATCH_PART_VARIABLES:
			return "Variable";
		case MATCH_PART_EMBEDDED_GRAPHS:
			return "EmbeddedGraph";
		case MATCH_PART_ALTERNATIVES:
			return "Alternative";
		case MATCH_PART_ITERATEDS:
			return "Iterated";
		case MATCH_PART_INDEPENDENTS:
			return "Independent";
		default:
			assert(false);
			return "";
		}
	}

	private String matchedEntitiesNamePlural(int which)
	{
		return matchedEntitiesNameSingular(which)+"s";
	}

	private String entitiesEnumName(int which, String pathPrefixForElements)
	{
		switch(which)
		{
		case MATCH_PART_NODES:
			return pathPrefixForElements + "NodeNums";
		case MATCH_PART_EDGES:
			return pathPrefixForElements + "EdgeNums";
		case MATCH_PART_VARIABLES:
			return pathPrefixForElements + "VariableNums";
		case MATCH_PART_EMBEDDED_GRAPHS:
			return pathPrefixForElements + "SubNums";
		case MATCH_PART_ALTERNATIVES:
			return pathPrefixForElements + "AltNums";
		case MATCH_PART_ITERATEDS:
			return pathPrefixForElements + "IterNums";
		case MATCH_PART_INDEPENDENTS:
			return pathPrefixForElements + "IdptNums";
		default:
			assert(false);
			return "";
		}
	}

	private String typeOfMatchedEntities(int which)
	{
		switch(which)
		{
		case MATCH_PART_NODES:
			return "GRGEN_LIBGR.INode";
		case MATCH_PART_EDGES:
			return "GRGEN_LIBGR.IEdge";
		case MATCH_PART_VARIABLES:
			return "object";
		case MATCH_PART_EMBEDDED_GRAPHS:
			return "GRGEN_LIBGR.IMatch";
		case MATCH_PART_ALTERNATIVES:
			return "GRGEN_LIBGR.IMatch";
		case MATCH_PART_ITERATEDS:
			return "GRGEN_LIBGR.IMatches";
		case MATCH_PART_INDEPENDENTS:
			return "GRGEN_LIBGR.IMatch";
		default:
			assert(false);
			return "";
		}
	}

	private int numOfMatchedEntities(int which, PatternGraph pattern)
	{
		switch(which)
		{
		case MATCH_PART_NODES:
			return pattern.getNodes().size();
		case MATCH_PART_EDGES:
			return pattern.getEdges().size();
		case MATCH_PART_VARIABLES:
			return pattern.getVars().size();
		case MATCH_PART_EMBEDDED_GRAPHS:
			return pattern.getSubpatternUsages().size();
		case MATCH_PART_ALTERNATIVES:
			return pattern.getAlts().size();
		case MATCH_PART_ITERATEDS:
			return pattern.getIters().size();
		case MATCH_PART_INDEPENDENTS:
			return pattern.getIdpts().size();
		default:
			assert(false);
			return 0;
		}
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private SearchPlanBackend2 be;
	private ModifyGen mg;
	private Model model;
}

