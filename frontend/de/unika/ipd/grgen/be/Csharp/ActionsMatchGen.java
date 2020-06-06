/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the pattern match objects for the SearchPlanBackend2 backend.
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Alternative;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.util.SourceBuilder;

public class ActionsMatchGen extends CSharpBase
{
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

	public ActionsMatchGen(String nodeTypePrefix, String edgeTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix);
	}

	//////////////////////////////
	// Match objects generation //
	//////////////////////////////

	public void genPatternMatchInterface(SourceBuilder sb, PatternGraph pattern, String name,
			String base, String pathPrefixForElements, boolean iterated, boolean alternativeCase,
			boolean matchClass, HashSet<String> elementsAlreadyDeclared)
	{
		genMatchInterface(sb, pattern, name,
				base, pathPrefixForElements, iterated, alternativeCase,
				matchClass, elementsAlreadyDeclared);

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			genPatternMatchInterface(sb, neg, pathPrefixForElements + negName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + negName + "_",
					false, false, false, elementsAlreadyDeclared);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			genPatternMatchInterface(sb, idpt, pathPrefixForElements + idptName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + idptName + "_",
					false, false, false, elementsAlreadyDeclared);
		}

		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			genAlternativeMatchInterface(sb, pathPrefixForElements + altName);
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				genPatternMatchInterface(sb, altCasePattern, altPatName,
						"IMatch_" + pathPrefixForElements + altName,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						false, true, false, elementsAlreadyDeclared);
			}
		}

		for(Rule iter : pattern.getIters()) {
			PatternGraph iterPattern = iter.getLeft();
			String iterName = iterPattern.getNameOfGraph();
			genPatternMatchInterface(sb, iterPattern, pathPrefixForElements + iterName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + iterName + "_",
					true, false, false, elementsAlreadyDeclared);
		}
	}

	public void genPatternMatchImplementation(SourceBuilder sb, PatternGraph pattern, String name,
			String patGraphVarName, String className,
			String pathPrefixForElements,
			boolean iterated, boolean independent, boolean parallelized)
	{
		genMatchImplementation(sb, pattern, name,
				patGraphVarName, className, pathPrefixForElements,
				iterated, independent, parallelized);

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			genPatternMatchImplementation(sb, neg, pathPrefixForElements + negName,
					pathPrefixForElements + negName, className,
					pathPrefixForElements + negName + "_", false, false, false);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			genPatternMatchImplementation(sb, idpt, pathPrefixForElements + idptName,
					pathPrefixForElements + idptName, className,
					pathPrefixForElements + idptName + "_", false, true, false);
		}

		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				genPatternMatchImplementation(sb, altCasePattern, altPatName,
						altPatName, className,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						false, false, false);
			}
		}

		for(Rule iter : pattern.getIters()) {
			PatternGraph iterPattern = iter.getLeft();
			String iterName = iterPattern.getNameOfGraph();
			genPatternMatchImplementation(sb, iterPattern, pathPrefixForElements + iterName,
					pathPrefixForElements + iterName, className,
					pathPrefixForElements + iterName + "_", true, false, false);
		}
	}

	private void genMatchInterface(SourceBuilder sb, PatternGraph pattern,
			String name, String base,
			String pathPrefixForElements, boolean iterated, boolean alternativeCase,
			boolean matchClass, HashSet<String> elementsAlreadyDeclared)
	{
		String interfaceName = "IMatch_" + name;
		sb.appendFront("public interface " + interfaceName + " : " + base + "\n");
		sb.appendFront("{\n");
		sb.indent();

		for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i) {
			genMatchedEntitiesInterface(sb, pattern, elementsAlreadyDeclared,
					name, i, pathPrefixForElements);
		}

		sb.appendFront("// further match object stuff\n");

		if(iterated) {
			sb.appendFront("bool IsNullMatch { get; }\n");
		}

		if(alternativeCase) {
			sb.appendFront("new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");
		} else {
			if(!matchClass) {
				sb.appendFront("void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");
			}
		}

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private static void genAlternativeMatchInterface(SourceBuilder sb, String name)
	{
		String interfaceName = "IMatch_" + name;
		sb.appendFront("public interface " + interfaceName + " : GRGEN_LIBGR.IMatch\n");
		sb.appendFront("{\n");

		sb.appendFrontIndented("void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");

		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genMatchImplementation(SourceBuilder sb, PatternGraph pattern, String name,
			String patGraphVarName, String ruleClassName,
			String pathPrefixForElements,
			boolean iterated, boolean independent, boolean parallelized)
	{
		String interfaceName = "IMatch_" + name;
		String className = "Match_" + name;
		sb.appendFront("public class " + className + " : GRGEN_LGSP.MatchListElement<" + className + ">, "
				+ interfaceName + "\n");
		sb.appendFront("{\n");
		sb.indent();

		for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i) {
			genMatchedEntitiesImplementation(sb, pattern, name,
					i, pathPrefixForElements);
			genMatchEnum(sb, pattern, name,
					i, pathPrefixForElements);
			genIMatchImplementation(sb, pattern, name,
					i, pathPrefixForElements);
			sb.append("\n");
		}

		sb.appendFront("public override GRGEN_LIBGR.IPatternGraph Pattern { get { return " + ruleClassName
				+ ".instance." + patGraphVarName + "; } }\n");
		if(iterated) {
			sb.appendFront("public bool IsNullMatch { get { return _isNullMatch; } }\n");
			sb.appendFront("public bool _isNullMatch;\n");
		}
		sb.appendFront("public override GRGEN_LIBGR.IMatch Clone() { return new " + className + "(this); }\n");
		sb.appendFront("public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) "
				+ "{ _matchOfEnclosingPattern = matchOfEnclosingPattern; }\n");

		sb.appendFront("public " + className + " nextWithSameHash;\n");

		genCleanNextWithSameHash(sb, className);

		if(parallelized)
			sb.appendFront("public int duplicateMatchHash;\n");

		genCopyConstructor(sb, pattern, name, pathPrefixForElements, className);

		sb.appendFront("public " + className + "()\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.append("\n");

		genIsEqualMethod(sb, pattern, name, pathPrefixForElements, className);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private static void genCleanNextWithSameHash(SourceBuilder sb, String className)
	{
		sb.appendFront("public void CleanNextWithSameHash() {\n");
		sb.indent();
		sb.appendFront(className + " cur = this;\n");
		sb.appendFront("while(cur != null) {\n");
		sb.indent();
		sb.appendFront(className + " next = cur.nextWithSameHash;\n");
		sb.appendFront("cur.nextWithSameHash = null;\n");
		sb.appendFront("cur = next;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genCopyConstructor(SourceBuilder sb, PatternGraph pattern, String name, String pathPrefixForElements,
			String className)
	{
		sb.appendFront("public void CopyMatchContent(" + className + " that)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i) {
			genCopyMatchedEntities(sb, pattern, name, i, pathPrefixForElements);
		}
		sb.unindent();
		sb.appendFront("}\n");

		sb.append("\n");

		sb.appendFront("public " + className + "(" + className + " that)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("CopyMatchContent(that);\n");
		sb.appendFront("}\n");
	}

	private void genIsEqualMethod(SourceBuilder sb, PatternGraph pattern, String name, String pathPrefixForElements,
			String className)
	{
		sb.appendFront("public bool IsEqual(" + className + " that)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(that==null) return false;\n");
		for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i) {
			genEqualMatch(sb, pattern, name, i, pathPrefixForElements);
		}
		sb.appendFront("return true;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genMatchedEntitiesInterface(SourceBuilder sb, PatternGraph pattern,
			HashSet<String> elementsAlreadyDeclared,
			String name, int which, String pathPrefixForElements)
	{
		// the getters for the elements
		sb.appendFront("//" + matchedEntitiesNamePlural(which) + "\n");
		switch(which) {
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				String newPrefix = elementsAlreadyDeclared.contains(formatEntity(node)) ? "new " : "";
				sb.appendFront(newPrefix + formatElementInterfaceRef(node.getType()) + " " + formatEntity(node)
						+ " { get; set; }\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				String newPrefix = elementsAlreadyDeclared.contains(formatEntity(edge)) ? "new " : "";
				sb.appendFront(newPrefix + formatElementInterfaceRef(edge.getType()) + " " + formatEntity(edge)
						+ " { get; set; }\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				String newPrefix = elementsAlreadyDeclared.contains(formatEntity(var)) ? "new " : "";
				sb.appendFront(newPrefix + formatAttributeType(var.getType()) + " @" + formatEntity(var)
						+ " { get; set; }\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("@" + matchType(sub.getSubpatternAction().getPattern(), sub.getSubpatternAction(), true, "")
								+ " @" + formatIdentifiable(sub) + " { get; }\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("IMatch_" + pathPrefixForElements + altName + " " + altName + " { get; }\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("GRGEN_LIBGR.IMatchesExact<IMatch_" + pathPrefixForElements + iterName + "> " + iterName
						+ " { get; }\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("IMatch_" + pathPrefixForElements + idptName + " " + idptName + " { get; }\n");
			}
			break;
		default:
			assert(false);
		}
	}

	private void genMatchedEntitiesImplementation(SourceBuilder sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		// the element itself and the getter for it
		switch(which) {
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.appendFront("public " + formatElementInterfaceRef(node.getType()) + " " + formatEntity(node)
						+ " { "
						+ "get { return (" + formatElementInterfaceRef(node.getType()) + ")" + formatEntity(node, "_") + "; } "
						+ "set { " + formatEntity(node, "_") + " = (GRGEN_LGSP.LGSPNode)value; }"
						+ " }\n");
			}
			for(Node node : pattern.getNodes()) {
				sb.appendFront("public GRGEN_LGSP.LGSPNode " + formatEntity(node, "_") + ";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.appendFront("public " + formatElementInterfaceRef(edge.getType()) + " " + formatEntity(edge)
						+ " { "
						+ "get { return (" + formatElementInterfaceRef(edge.getType()) + ")" + formatEntity(edge, "_") + "; } "
						+ "set { " + formatEntity(edge, "_") + " = (GRGEN_LGSP.LGSPEdge)value; }"
						+ " }\n");
			}
			for(Edge edge : pattern.getEdges()) {
				sb.appendFront("public GRGEN_LGSP.LGSPEdge " + formatEntity(edge, "_") + ";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.appendFront("public " + formatAttributeType(var.getType()) + " " + formatEntity(var)
						+ " { "
						+ "get { return " + formatEntity(var, "_") + "; } "
						+ "set { " + formatEntity(var, "_") + " = value; }"
						+ " }\n");
			}
			for(Variable var : pattern.getVars()) {
				sb.appendFront("public " + formatAttributeType(var.getType()) + " " + formatEntity(var, "_") + ";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("public @"
						+ matchType(sub.getSubpatternAction().getPattern(), sub.getSubpatternAction(), true, "") + " @"
						+ formatIdentifiable(sub) + " { get { return @" + formatIdentifiable(sub, "_") + "; } }\n");
			}
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("public @"
						+ matchType(sub.getSubpatternAction().getPattern(), sub.getSubpatternAction(), true, "") + " @"
						+ formatIdentifiable(sub, "_") + ";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("public IMatch_" + pathPrefixForElements + altName + " " + altName
						+ " { get { return _" + altName + "; } }\n");
			}
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("public IMatch_" + pathPrefixForElements + altName + " _" + altName + ";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("public GRGEN_LIBGR.IMatchesExact<IMatch_" + pathPrefixForElements + iterName + "> "
						+ iterName + " { get { return _" + iterName + "; } }\n");
			}
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("public GRGEN_LGSP.LGSPMatchesList<Match_" + pathPrefixForElements + iterName
						+ ", IMatch_" + pathPrefixForElements + iterName + "> _" + iterName + ";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("public IMatch_" + pathPrefixForElements + idptName + " " + idptName
						+ " { get { return _" + idptName + "; } }\n");
			}
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("public IMatch_" + pathPrefixForElements + idptName + " _" + idptName + ";\n");
			}
			break;
		default:
			assert(false);
		}
	}

	private void genCopyMatchedEntities(SourceBuilder sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		switch(which) {
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				String nodeName = formatEntity(node, "_");
				sb.appendFront(nodeName + " = that." + nodeName + ";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				String edgeName = formatEntity(edge, "_");
				sb.appendFront(edgeName + " = that." + edgeName + ";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				String varName = formatEntity(var, "_");
				sb.appendFront(varName + " = that." + varName + ";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				String subName = "@" + formatIdentifiable(sub, "_");
				sb.appendFront(subName + " = that." + subName + ";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = "_" + alt.getNameOfGraph();
				sb.appendFront(altName + " = that." + altName + ";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = "_" + iter.getLeft().getNameOfGraph();
				sb.appendFront(iterName + " = that." + iterName + ";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = "_" + idpt.getNameOfGraph();
				sb.appendFront(idptName + " = that." + idptName + ";\n");
			}
			break;
		default:
			assert(false);
		}
	}

	private void genEqualMatch(SourceBuilder sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		switch(which) {
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				if(node.isDefToBeYieldedTo())
					continue;
				String nodeName = formatEntity(node, "_");
				sb.appendFront("if(" + nodeName + " != that." + nodeName + ") return false;\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				if(edge.isDefToBeYieldedTo())
					continue;
				String edgeName = formatEntity(edge, "_");
				sb.appendFront("if(" + edgeName + " != that." + edgeName + ") return false;\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				if(var.isDefToBeYieldedTo())
					continue;
				String varName = formatEntity(var, "_");
				sb.appendFront("if(" + varName + " != that." + varName + ") return false;\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				String subName = "@" + formatIdentifiable(sub, "_");
				sb.appendFront("if(!" + subName + ".IsEqual(that." + subName + ")) return false;\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = "_" + alt.getNameOfGraph();
				for(Rule altCase : alt.getAlternativeCases()) {
					PatternGraph altCasePattern = altCase.getLeft();
					sb.appendFront("if(" + altName + " is Match_" + name + altName + "_" + altCasePattern.getNameOfGraph()
							+ " && !(" + altName + " as Match_" + name + altName + "_" + altCasePattern.getNameOfGraph()
							+ ").IsEqual(that." + altName + " as Match_" + name + altName + "_" + altCasePattern.getNameOfGraph()
							+ ")) return false;\n");
				}
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = "_" + iter.getLeft().getNameOfGraph();
				sb.appendFront("if(" + iterName + ".Count != that." + iterName + ".Count) return false;\n");
				sb.appendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + iterName + "_thisEnumerator = " + iterName
						+ ".GetEnumerator();\n");
				sb.appendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + iterName + "_thatEnumerator = that." + iterName
						+ ".GetEnumerator();\n");
				sb.appendFront("while(" + iterName + "_thisEnumerator.MoveNext())\n");
				sb.appendFront("{\n");
				sb.indent();
				sb.appendFront(iterName + "_thatEnumerator.MoveNext();\n");
				sb.append("if(!(" + iterName + "_thisEnumerator.Current as Match_" + name + iterName + ").IsEqual("
						+ iterName + "_thatEnumerator.Current as Match_" + name + iterName + ")) return false;\n");
				sb.unindent();
				sb.appendFront("}\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			// for independents, the existence counts, the exact elements are irrelevant
			break;
		default:
			assert(false);
		}
	}

	private void genIMatchImplementation(SourceBuilder sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		// the various match part getters

		String enumerableName = "GRGEN_LGSP." + matchedEntitiesNamePlural(which) + "_Enumerable";
		String enumeratorName = "GRGEN_LGSP." + matchedEntitiesNamePlural(which) + "_Enumerator";
		String typeOfMatchedEntities = typeOfMatchedEntities(which);
		int numberOfMatchedEntities = numOfMatchedEntities(which, pattern);
		String matchedEntitiesNameSingular = matchedEntitiesNameSingular(which);
		String matchedEntitiesNamePlural = matchedEntitiesNamePlural(which);

		sb.appendFront("public override IEnumerable<" + typeOfMatchedEntities + "> " + matchedEntitiesNamePlural
				+ " { get { return new " + enumerableName + "(this); } }\n");
		sb.appendFront("public override IEnumerator<" + typeOfMatchedEntities + "> " + matchedEntitiesNamePlural
				+ "Enumerator { get { return new " + enumeratorName + "(this); } }\n");
		sb.appendFront("public override int NumberOf" + matchedEntitiesNamePlural
				+ " { get { return " + numberOfMatchedEntities + ";} }\n");

		// -----------------------------

		sb.appendFront("public override " + typeOfMatchedEntities
				+ " get" + matchedEntitiesNameSingular + "At(int index)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(index) {\n");

		switch(which) {
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@"
						+ formatIdentifiable(node) + ": return " + formatEntity(node, "_") + ";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@"
						+ formatIdentifiable(edge) + ": return " + formatEntity(edge, "_") + ";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@"
						+ formatIdentifiable(var) + ": return " + formatEntity(var, "_") + ";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@"
						+ formatIdentifiable(sub) + ": return " + formatIdentifiable(sub, "_") + ";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + altName
						+ ": return _" + altName + ";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + iterName
						+ ": return _" + iterName + ";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + idptName
						+ ": return _" + idptName + ";\n");
			}
			break;
		default:
			assert(false);
			break;
		}

		sb.appendFront("default: return null;\n");
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");

		// -----------------------------

		sb.appendFront("public override " + typeOfMatchedEntities
				+ " get" + matchedEntitiesNameSingular + "(string name)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(name) {\n");

		switch(which) {
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.appendFront("case \"" + formatIdentifiable(node) + "\": return " + formatEntity(node, "_") + ";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.appendFront("case \"" + formatIdentifiable(edge) + "\": return " + formatEntity(edge, "_") + ";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.appendFront("case \"" + formatIdentifiable(var) + "\": return " + formatEntity(var, "_") + ";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("case \"" + formatIdentifiable(sub) + "\": return " + formatIdentifiable(sub, "_") + ";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("case \"" + altName + "\": return _" + altName + ";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("case \"" + iterName + "\": return _" + iterName + ";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("case \"" + idptName + "\": return _" + idptName + ";\n");
			}
			break;
		default:
			assert(false);
			break;
		}

		sb.appendFront("default: return null;\n");
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genMatchEnum(SourceBuilder sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		// generate enum mapping entity names to consecutive integers
		sb.appendFront("public enum " + entitiesEnumName(which, pathPrefixForElements) + " { ");
		switch(which) {
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.append("@" + formatIdentifiable(node) + ", ");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.append("@" + formatIdentifiable(edge) + ", ");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.append("@" + formatIdentifiable(var) + ", ");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.append("@" + formatIdentifiable(sub) + ", ");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				sb.append("@" + alt.getNameOfGraph() + ", ");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				sb.append("@" + iter.getLeft().getNameOfGraph() + ", ");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				sb.append("@" + idpt.getNameOfGraph() + ", ");
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
		switch(which) {
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
		return matchedEntitiesNameSingular(which) + "s";
	}

	private String entitiesEnumName(int which, String pathPrefixForElements)
	{
		switch(which) {
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
		switch(which) {
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
		switch(which) {
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

	@Override
	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState)
	{
		// needed because of inheritance, maybe todo: remove
	}

	@Override
	protected void genMemberAccess(SourceBuilder sb, Entity member)
	{
		// needed because of inheritance, maybe todo: remove
	}
}
