/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the actions file for the SearchPlanBackend2 backend.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.LinkedList;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.VariableExpression;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.model.type.ExternalType;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.pattern.Alternative;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.pattern.NameLookup;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.RetypedEdge;
import de.unika.ipd.grgen.ir.pattern.RetypedNode;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.UniqueLookup;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
import de.unika.ipd.grgen.ir.stmt.ReturnStatementFilter;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.PackageActionType;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.util.SourceBuilder;

public class ActionsGen extends CSharpBase
{
	enum MemberBearerType
	{
		Action, Subpattern, MatchClass
	}

	public ActionsGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
		model = be.unit.getActionsGraphModel();
		mg = new ModifyGen(backend, nodeTypePrefix, edgeTypePrefix);
		eyGen = new ActionsExpressionOrYieldingGen(backend, nodeTypePrefix, edgeTypePrefix);
	}

	/**
	 * Generates the subpatterns, actions, sequences, functions, procedures, match classes, filters sourcecode for this unit.
	 */
	public void genActionlike()
	{
		SourceBuilder sb = new SourceBuilder();
		String filename = be.unit.getUnitName() + "Actions_intermediate.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.appendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
				+ "using System.Collections;\n"
				+ "using System.Text;\n"
				+ "using System.Threading;\n"
				+ "using System.Diagnostics;\n"
				+ "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
				+ "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
				+ "using GRGEN_EXPR = de.unika.ipd.grGen.expression;\n"
				+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + be.unit.getActionsGraphModelName() + ";\n"
				+ "using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_" + be.unit.getUnitName() + ";\n"
				+ "\n"
				+ "namespace de.unika.ipd.grGen.Action_" + be.unit.getUnitName() + "\n"
				+ "{\n");
		sb.indent();

		/////////////////////////////////////////////////////////

		for(PackageActionType pt : be.unit.getPackages()) {
			System.out.println("    generating package " + pt.getIdent() + "...");

			sb.append("\n");
			sb.appendFront("//-----------------------------------------------------------\n");
			sb.appendFront("namespace ");
			sb.append(formatIdentifiable(pt));
			sb.append("\n");
			sb.appendFront("//-----------------------------------------------------------\n");
			sb.appendFront("{\n");

			sb.indent();
			genBearer(sb, pt, pt.getIdent().toString());
			sb.unindent();

			sb.append("\n");
			sb.appendFront("//-----------------------------------------------------------\n");
			sb.appendFront("}\n");
			sb.appendFront("//-----------------------------------------------------------\n");
		}

		genBearer(sb, be.unit, null);
		genExternalFunctionInfos(sb);
		genExternalProcedureInfos(sb);

		sb.append("\n");
		sb.appendFront("//-----------------------------------------------------------\n\n");

		ActionsBearer bearer = new ComposedActionsBearer(be.unit);

		sb.appendFront("public class " + be.unit.getUnitName()
				+ "_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("public " + be.unit.getUnitName() + "_RuleAndMatchingPatterns()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[" + bearer.getSubpatternRules().size() + "];\n");
		sb.appendFront("rules = new GRGEN_LGSP.LGSPRulePattern[" + bearer.getActionRules().size() + "];\n");
		sb.appendFront("rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[" +
				bearer.getSubpatternRules().size() + "+" + bearer.getActionRules().size() + "];\n");
		sb.appendFront("definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[" + bearer.getSequences().size() + "];\n");
		sb.appendFront("functions = new GRGEN_LIBGR.FunctionInfo[" + bearer.getFunctions().size() + "+"
				+ model.getExternalFunctions().size() + "];\n");
		sb.appendFront("procedures = new GRGEN_LIBGR.ProcedureInfo[" + bearer.getProcedures().size() + "+"
				+ model.getExternalProcedures().size() + "];\n");
		sb.appendFront("matchClasses = new GRGEN_LIBGR.MatchClassInfo[" + bearer.getMatchClasses().size() + "];\n");
		sb.appendFront("packages = new string[" + be.unit.getPackages().size() + "];\n");
		int i = 0;
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			sb.appendFront("subpatterns[" + i + "] = " + getPackagePrefixDot(subpatternRule) + "Pattern_"
					+ formatIdentifiable(subpatternRule) + ".Instance;\n");
			sb.appendFront("rulesAndSubpatterns[" + i + "] = " + getPackagePrefixDot(subpatternRule) + "Pattern_"
					+ formatIdentifiable(subpatternRule) + ".Instance;\n");
			++i;
		}
		int j = 0;
		for(Rule actionRule : bearer.getActionRules()) {
			sb.appendFront("rules[" + j + "] = " + getPackagePrefixDot(actionRule) + "Rule_"
					+ formatIdentifiable(actionRule) + ".Instance;\n");
			sb.appendFront("rulesAndSubpatterns[" + i + "+" + j + "] = " + getPackagePrefixDot(actionRule) + "Rule_"
					+ formatIdentifiable(actionRule) + ".Instance;\n");
			++j;
		}
		i = 0;
		for(Sequence sequence : bearer.getSequences()) {
			sb.appendFront("definedSequences[" + i + "] = " + getPackagePrefixDot(sequence) + "SequenceInfo_"
					+ formatIdentifiable(sequence) + ".Instance;\n");
			++i;
		}
		i = 0;
		for(Function function : bearer.getFunctions()) {
			sb.appendFront("functions[" + i + "] = " + getPackagePrefixDot(function) + "FunctionInfo_"
					+ formatIdentifiable(function) + ".Instance;\n");
			++i;
		}
		for(ExternalFunction function : model.getExternalFunctions()) {
			sb.appendFront("functions[" + i + "] = " + "FunctionInfo_" + formatIdentifiable(function) + ".Instance;\n");
			++i;
		}
		i = 0;
		for(Procedure procedure : bearer.getProcedures()) {
			sb.appendFront("procedures[" + i + "] = " + getPackagePrefixDot(procedure) + "ProcedureInfo_"
					+ formatIdentifiable(procedure) + ".Instance;\n");
			++i;
		}
		for(ExternalProcedure procedure : model.getExternalProcedures()) {
			sb.appendFront("procedures[" + i + "] = " + "ProcedureInfo_" + formatIdentifiable(procedure) + ".Instance;\n");
			++i;
		}
		i = 0;
		for(DefinedMatchType matchClass : bearer.getMatchClasses()) {
			sb.appendFront("matchClasses[" + i + "] = " + getPackagePrefixDot(matchClass) + "MatchClassInfo_"
					+ formatIdentifiable(matchClass) + ".Instance;\n");
			++i;
		}
		i = 0;
		for(PackageActionType pack : be.unit.getPackages()) {
			sb.appendFront("packages[" + i + "] = \"" + pack.getIdent() + "\";\n");
			++i;
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }\n");
		sb.appendFront("private GRGEN_LGSP.LGSPRulePattern[] rules;\n");
		sb.appendFront("public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }\n");
		sb.appendFront("private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;\n");
		sb.appendFront("public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }\n");
		sb.appendFront("private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;\n");
		sb.appendFront("public override GRGEN_LIBGR.DefinedSequenceInfo[] DefinedSequences { get { return definedSequences; } }\n");
		sb.appendFront("private GRGEN_LIBGR.DefinedSequenceInfo[] definedSequences;\n");
		sb.appendFront("public override GRGEN_LIBGR.FunctionInfo[] Functions { get { return functions; } }\n");
		sb.appendFront("private GRGEN_LIBGR.FunctionInfo[] functions;\n");
		sb.appendFront("public override GRGEN_LIBGR.ProcedureInfo[] Procedures { get { return procedures; } }\n");
		sb.appendFront("private GRGEN_LIBGR.ProcedureInfo[] procedures;\n");
		sb.appendFront("public override GRGEN_LIBGR.MatchClassInfo[] MatchClasses { get { return matchClasses; } }\n");
		sb.appendFront("private GRGEN_LIBGR.MatchClassInfo[] matchClasses;\n");
		sb.appendFront("public override string[] Packages { get { return packages; } }\n");
		sb.appendFront("private string[] packages;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.unindent();
		sb.appendFront("// GrGen insert Actions here\n");
		sb.appendFront("}\n");

		System.out.println("    writing to " + be.path + " / " + filename);
		writeFile(be.path, filename, sb.getStringBuilder());
	}

	private void genBearer(SourceBuilder sb, ActionsBearer bearer, String packageName)
	{
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			genSubpattern(sb, subpatternRule, packageName);
		}

		for(Rule actionRule : bearer.getActionRules()) {
			genAction(sb, actionRule, packageName);
		}

		for(Sequence sequence : bearer.getSequences()) {
			genSequence(sb, sequence, packageName);
		}

		genFunctions(sb, bearer, packageName);

		genProcedures(sb, bearer, packageName);

		genFilterFunctions(sb, bearer, packageName);

		genMatchClassFilterFunctions(sb, bearer, packageName);

		genMatchClasses(sb, bearer, packageName);
	}

	private void genExternalFunctionInfos(SourceBuilder sb)
	{
		for(ExternalFunction ef : model.getExternalFunctions()) {
			genExternalFunctionInfo(sb, ef);
		}
	}

	private void genExternalProcedureInfos(SourceBuilder sb)
	{
		for(ExternalProcedure ep : model.getExternalProcedures()) {
			genExternalProcedureInfo(sb, ep);
		}
	}

	private void genExternalFunctionInfo(SourceBuilder sb, ExternalFunction function)
	{
		String functionName = formatIdentifiable(function);
		String className = "FunctionInfo_" + functionName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + functionName + "\",\n");
		sb.appendFront("null" + ", ");
		sb.append("\"" + functionName + "\",\n");
		sb.appendFront("true,\n");
		sb.appendFront("new String[] { ");
		int i = 0;
		for(@SuppressWarnings("unused") Type inType : function.getParameterTypes()) {
			sb.append("\"in_" + i + "\", ");
			++i;
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type inType : function.getParameterTypes()) {
			if(inType instanceof InheritanceType && !(inType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inType) + ")), ");
			}
		}
		sb.append(" },\n");
		Type outType = function.getReturnType();
		if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
			sb.appendFront(formatTypeClassRef(outType) + ".typeVar\n");
		} else {
			sb.appendFront("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + "))\n");
		}
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.indent();
		addAnnotations(sb, function, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.append("return GRGEN_EXPR.ExternalFunctions." + functionName
				+ "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
		i = 0;
		for(Type inType : function.getParameterTypes()) {
			sb.append(", (" + formatType(inType) + ")arguments[" + i + "]");
			++i;
		}
		sb.append(");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genExternalProcedureInfo(SourceBuilder sb, ExternalProcedure procedure)
	{
		String procedureName = formatIdentifiable(procedure);
		String className = "ProcedureInfo_" + procedureName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + procedureName + "\",\n");
		sb.appendFront("null" + ", ");
		sb.append("\"" + procedureName + "\",\n");
		sb.appendFront("true,\n");
		sb.appendFront("new String[] { ");
		int i = 0;
		for(@SuppressWarnings("unused") Type inType : procedure.getParameterTypes()) {
			sb.append("\"in_" + i + "\", ");
			++i;
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type inType : procedure.getParameterTypes()) {
			if(inType instanceof InheritanceType && !(inType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inType) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type outType : procedure.getReturnTypes()) {
			if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(outType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + ")), ");
			}
		}
		sb.append(" }\n");
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.indent();
		addAnnotations(sb, procedure, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();

		i = 0;
		for(Type outType : procedure.getReturnTypes()) {
			sb.append(formatType(outType));
			sb.append(" ");
			sb.append("_out_param_" + i + ";\n");
			++i;
		}

		sb.append("GRGEN_EXPR.ExternalProcedures." + procedureName
				+ "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
		i = 0;
		for(Type inType : procedure.getParameterTypes()) {
			sb.append(", (" + formatType(inType) + ")arguments[" + i + "]");
			++i;
		}
		for(i = 0; i < procedure.getReturnTypes().size(); ++i) {
			sb.append(", out ");
			sb.append("_out_param_" + i);
		}
		sb.append(");\n");

		for(i = 0; i < procedure.getReturnTypes().size(); ++i) {
			sb.appendFront("ReturnArray[" + i + "] = ");
			sb.append("_out_param_" + i + ";\n");
		}

		sb.appendFront("return ReturnArray;\n");

		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the subpattern action representation sourcecode for the given subpattern-matching-action
	 */
	private void genSubpattern(SourceBuilder sb, Rule subpatternRule, String packageName)
	{
		String actionName = formatIdentifiable(subpatternRule);
		String className = "Pattern_" + actionName;
		List<String> staticInitializers = new LinkedList<String>();

		sb.appendFront("public class " + className + " : GRGEN_LGSP.LGSPMatchingPattern\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");

		String patGraphVarName = "pat_" + subpatternRule.getPattern().getNameOfGraph();
		genRuleOrSubpatternClassEntities(sb, subpatternRule, patGraphVarName, staticInitializers,
				subpatternRule.getPattern().getNameOfGraph() + "_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, subpatternRule, className, packageName, true);
		sb.append("\n");

		mg.genModify(sb, subpatternRule, packageName, true);

		ActionsExecGen execGen = new ActionsExecGen(nodeTypePrefix, edgeTypePrefix);
		execGen.genImperativeStatements(sb, subpatternRule, formatIdentifiable(subpatternRule) + "_",
				subpatternRule.getPackageContainedIn(), true, true);
		execGen.genImperativeStatementClosures(sb, subpatternRule, formatIdentifiable(subpatternRule) + "_", false);

		genStaticConstructor(sb, className, staticInitializers);

		genMatch(sb, subpatternRule.getPattern(), null, className, false);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		for(Rule iteratedRule : subpatternRule.getLeft().getIters()) {
			genArraySortBy(sb, subpatternRule, MemberBearerType.Subpattern, iteratedRule);
		}
		sb.append("\n");
	}

	/**
	 * Generates the action representation sourcecode for the given matching-action
	 */
	private void genAction(SourceBuilder sb, Rule actionRule, String packageName)
	{
		String actionName = formatIdentifiable(actionRule);
		String className = "Rule_" + actionName;
		List<String> staticInitializers = new LinkedList<String>();

		sb.appendFront("public class " + className + " : GRGEN_LGSP.LGSPRulePattern\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");

		String patGraphVarName = "pat_" + actionRule.getPattern().getNameOfGraph();
		genRuleOrSubpatternClassEntities(sb, actionRule, patGraphVarName, staticInitializers,
				actionRule.getPattern().getNameOfGraph() + "_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, actionRule, className, packageName, false);
		sb.append("\n");

		mg.genModify(sb, actionRule, packageName, false);

		ActionsExecGen execGen = new ActionsExecGen(nodeTypePrefix, edgeTypePrefix);
		execGen.genImperativeStatements(sb, actionRule, formatIdentifiable(actionRule) + "_",
				actionRule.getPackageContainedIn(), true, false);
		execGen.genImperativeStatementClosures(sb, actionRule, formatIdentifiable(actionRule) + "_", true);

		genStaticConstructor(sb, className, staticInitializers);

		genMatch(sb, actionRule.getPattern(), actionRule.getImplementedMatchClasses(), className,
				actionRule.getAnnotations().containsKey("parallelize"));

		sb.append("\n");
		genExtractor(sb, actionRule, null);
		for(Rule iteratedRule : actionRule.getLeft().getIters()) {
			genExtractor(sb, actionRule, iteratedRule);
		}

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		genArraySortBy(sb, actionRule, MemberBearerType.Action, null);
		for(Rule iteratedRule : actionRule.getLeft().getIters()) {
			genArraySortBy(sb, actionRule, MemberBearerType.Action, iteratedRule);
		}
		sb.append("\n");
	}

	/**
	 * Generates the sequence representation sourcecode for the given sequence
	 */
	private void genSequence(SourceBuilder sb, Sequence sequence, String packageName)
	{
		String sequenceName = formatIdentifiable(sequence);
		String className = "SequenceInfo_" + sequenceName;
		boolean isExternalSequence = sequence.getExec().getXGRSString().length() == 0;
		String baseClass = isExternalSequence
				? "GRGEN_LIBGR.ExternalDefinedSequenceInfo"
				: "GRGEN_LIBGR.DefinedSequenceInfo";

		sb.appendFront("public class " + className + " : " + baseClass + "\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("new String[] { ");
		for(ExecVariable inParam : sequence.getInParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(ExecVariable inParam : sequence.getInParameters()) {
			if(inParam.getType() instanceof InheritanceType) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.appendFront("new String[] { ");
		for(ExecVariable inParam : sequence.getOutParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(ExecVariable outParam : sequence.getOutParameters()) {
			if(outParam.getType() instanceof InheritanceType) {
				sb.append(formatTypeClassRef(outParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.appendFront("\"" + sequenceName + "\",\n");
		if(!isExternalSequence) {
			sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
			sb.append("\"" + (packageName != null ? packageName + "::" + sequenceName : sequenceName) + "\",\n");
			sb.appendFront("\"" + escapeBackslashAndDoubleQuotes(sequence.getExec().getXGRSString()) + "\",\n");
		}
		sb.appendFront(sequence.getExec().getLineNr() + "\n");
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.indent();
		addAnnotations(sb, sequence, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genFunctions(SourceBuilder sb, ActionsBearer bearer, String packageName)
	{
		sb.appendFront("public class Functions\n");
		sb.appendFront("{\n");
		sb.indent();

		for(Function function : bearer.getFunctions()) {
			forceNotConstant(function.getComputationStatements());
			genFunction(sb, function, false, be.system.emitProfilingInstrumentation());
		}
		if(model.areFunctionsParallel()) {
			for(Function function : bearer.getFunctions()) {
				genFunction(sb, function, true, be.system.emitProfilingInstrumentation());
			}
		}

		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();

		for(Function function : bearer.getFunctions()) {
			genLocalContainersEvals(sb, function.getComputationStatements(), staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}

		genStaticConstructor(sb, "Functions", staticInitializers);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		for(Function function : bearer.getFunctions()) {
			genFunctionInfo(sb, function, packageName);
		}
	}

	/**
	 * Generates the function representation sourcecode for the given function
	 */
	private void genFunction(SourceBuilder sb, Function function,
			boolean isToBeParallelizedActionExisting, boolean emitProfilingInstrumentation)
	{
		sb.appendFront("public static " + formatType(function.getReturnType()) + " ");
		sb.append(function.getIdent().toString()
				+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPGraph graph");
		for(Entity inParam : function.getParameters()) {
			sb.append(", ");
			sb.append(formatType(inParam.getType()));
			sb.append(" ");
			sb.append(formatEntity(inParam));
		}
		if(isToBeParallelizedActionExisting)
			sb.append(", int threadId");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		ModifyGenerationState modifyGenState = new ModifyGenerationState(model, null, "",
				isToBeParallelizedActionExisting, emitProfilingInstrumentation);
		ModifyEvalGen evalGen = new ModifyEvalGen(be, null, nodeTypePrefix, edgeTypePrefix);
		for(EvalStatement evalStmt : function.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = function.getIdent().toString();
			evalGen.genEvalStmt(sb, modifyGenState, evalStmt);
		}
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the function info for the given function
	 */
	private void genFunctionInfo(SourceBuilder sb, Function function, String packageName)
	{
		String functionName = formatIdentifiable(function);
		String className = "FunctionInfo_" + functionName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + functionName + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName != null ? packageName + "::" + functionName : functionName) + "\",\n");
		sb.appendFront("false,\n");
		sb.appendFront("new String[] { ");
		for(Entity inParam : function.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : function.getParameters()) {
			if(inParam.getType() instanceof InheritanceType && !(inParam.getType() instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		Type outType = function.getReturnType();
		if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
			sb.appendFront(formatTypeClassRef(outType) + ".typeVar\n");
		} else {
			sb.appendFront("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + "))\n");
		}
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.indent();
		addAnnotations(sb, function, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("return GRGEN_ACTIONS." + getPackagePrefixDot(function) + "Functions." + functionName
				+ "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
		int i = 0;
		for(Entity inParam : function.getParameters()) {
			sb.append(", (" + formatType(inParam.getType()) + ")arguments[" + i + "]");
			++i;
		}
		sb.append(");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the procedure representation sourcecode for the given procedure
	 */
	private void genProcedures(SourceBuilder sb, ActionsBearer bearer, String packageName)
	{
		sb.appendFront("public class Procedures\n");
		sb.appendFront("{\n");
		sb.indent();

		for(Procedure procedure : bearer.getProcedures()) {
			forceNotConstant(procedure.getComputationStatements());
			genProcedure(sb, procedure, be.system.emitProfilingInstrumentation());
		}

		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();

		for(Procedure procedure : bearer.getProcedures()) {
			genLocalContainersEvals(sb, procedure.getComputationStatements(), staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}

		genStaticConstructor(sb, "Procedures", staticInitializers);

		ActionsExecGen execGen = new ActionsExecGen(nodeTypePrefix, edgeTypePrefix);
		sb.append("#if INITIAL_WARMUP\t\t// GrGen procedure exec section: "
				+ (packageName != null ? packageName + "::" + "Procedures\n" : "Procedures\n"));
		for(Procedure procedure : bearer.getProcedures()) {
			execGen.genImperativeStatements(sb, procedure);
		}
		sb.append("#endif\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		for(Procedure procedure : bearer.getProcedures()) {
			genProcedureInfo(sb, procedure, packageName);
		}
	}

	private void genProcedure(SourceBuilder sb, Procedure procedure, boolean emitProfilingInstrumentation)
	{
		sb.appendFront("public static void ");
		sb.append(procedure.getIdent().toString()
				+ "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPGraph graph");
		for(Entity inParam : procedure.getParameters()) {
			sb.append(", ");
			sb.append(formatType(inParam.getType()));
			sb.append(" ");
			sb.append(formatEntity(inParam));
		}
		int i = 0;
		for(Type outType : procedure.getReturnTypes()) {
			sb.append(", out ");
			sb.append(formatType(outType));
			sb.append(" ");
			sb.append("_out_param_" + i);
			++i;
		}
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		ModifyGenerationState modifyGenState = new ModifyGenerationState(model, null, "", false,
				emitProfilingInstrumentation);
		ModifyExecGen execGen = new ModifyExecGen(be, nodeTypePrefix, edgeTypePrefix);
		ModifyEvalGen evalGen = new ModifyEvalGen(be, execGen, nodeTypePrefix, edgeTypePrefix);

		if(be.system.mayFireDebugEvents()) {
			sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering(");
			sb.append("\"" + procedure.getIdent().toString() + "\"");
			for(Entity inParam : procedure.getParameters()) {
				sb.append(", ");
				sb.append(formatEntity(inParam));
			}
			sb.append(");\n");
		}

		for(EvalStatement evalStmt : procedure.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = procedure.getIdent().toString();
			evalGen.genEvalStmt(sb, modifyGenState, evalStmt);
		}
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the procedure info for the given procedure
	 */
	private void genProcedureInfo(SourceBuilder sb, Procedure procedure, String packageName)
	{
		String procedureName = formatIdentifiable(procedure);
		String className = "ProcedureInfo_" + procedureName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + procedureName + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName != null ? packageName + "::" + procedureName : procedureName) + "\",\n");
		sb.appendFront("false,\n");
		sb.appendFront("new String[] { ");
		for(Entity inParam : procedure.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : procedure.getParameters()) {
			if(inParam.getType() instanceof InheritanceType && !(inParam.getType() instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type outType : procedure.getReturnTypes()) {
			if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(outType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + ")), ");
			}
		}
		sb.append("}\n");
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.indent();
		addAnnotations(sb, procedure, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();

		int i = 0;
		for(Type outType : procedure.getReturnTypes()) {
			sb.appendFront(formatType(outType));
			sb.append(" ");
			sb.append("_out_param_" + i + ";\n");
			++i;
		}

		sb.appendFront("GRGEN_ACTIONS." + getPackagePrefixDot(procedure) + "Procedures." + procedureName
				+ "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
		i = 0;
		for(Entity inParam : procedure.getParameters()) {
			sb.append(", (" + formatType(inParam.getType()) + ")arguments[" + i + "]");
			++i;
		}
		for(i = 0; i < procedure.getReturnTypes().size(); ++i) {
			sb.append(", out ");
			sb.append("_out_param_" + i);
		}
		sb.append(");\n");

		for(i = 0; i < procedure.getReturnTypes().size(); ++i) {
			sb.appendFront("ReturnArray[" + i + "] = ");
			sb.append("_out_param_" + i + ";\n");
		}

		sb.appendFront("return ReturnArray;\n");

		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the function representation sourcecode for the given filter function
	 */
	private void genFilterFunctions(SourceBuilder sb, ActionsBearer bearer, String packageName)
	{
		sb.appendFront("public partial class MatchFilters\n");
		sb.appendFront("{\n");
		sb.indent();

		for(FilterFunction filter : bearer.getFilterFunctions()) {
			if(filter instanceof FilterFunctionInternal) {
				FilterFunctionInternal filterFunction = (FilterFunctionInternal)filter;
				forceNotConstant(filterFunction.getComputationStatements());
				genFilterFunction(sb, filterFunction, packageName, be.system.emitProfilingInstrumentation());
			}
		}

		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();

		for(FilterFunction filter : bearer.getFilterFunctions()) {
			if(filter instanceof FilterFunctionInternal) {
				FilterFunctionInternal filterFunction = (FilterFunctionInternal)filter;
				genLocalContainersEvals(sb, filterFunction.getComputationStatements(), staticInitializers,
						pathPrefixForElements, alreadyDefinedEntityToName);
			}
		}

		genStaticConstructor(sb, "MatchFilters", staticInitializers);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genFilterFunction(SourceBuilder sb, FilterFunctionInternal filter, String packageName,
			boolean emitProfilingInstrumentation)
	{
		String actionName = filter.getAction().getIdent().toString();
		String packagePrefixOfAction = "GRGEN_ACTIONS." + getPackagePrefixDot(filter.getAction());
		String matchType = packagePrefixOfAction + "Rule_" + actionName + ".IMatch_" + actionName;
		sb.appendFront("public static void ");
		sb.append("Filter_" + filter.getIdent().toString()
				+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<" + matchType + "> matches");
		for(Entity inParam : filter.getParameters()) {
			sb.append(", ");
			sb.append(formatType(inParam.getType()));
			sb.append(" ");
			sb.append(formatEntity(inParam));
		}
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = procEnv;\n");
		sb.appendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)procEnv.Graph;\n");
		ModifyGenerationState modifyGenState = new ModifyGenerationState(model, null, "", false,
				emitProfilingInstrumentation);
		ModifyEvalGen evalGen = new ModifyEvalGen(be, null, nodeTypePrefix, edgeTypePrefix);
		EvalStatement lastEvalStmt = null;
		for(EvalStatement evalStmt : filter.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = filter.getIdent().toString();
			evalGen.genEvalStmt(sb, modifyGenState, evalStmt);
			lastEvalStmt = evalStmt;
		}
		if(!(lastEvalStmt instanceof ReturnStatementFilter)) {
			// ensure that FromList is called if the user omitted return
			evalGen.genEvalStmt(sb, modifyGenState, new ReturnStatementFilter());
		}

		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the function representation sourcecode for the given match filter function
	 */
	private void genMatchClassFilterFunctions(SourceBuilder sb, ActionsBearer bearer, String packageName)
	{
		sb.appendFront("public partial class MatchClassFilters\n");
		sb.appendFront("{\n");
		sb.indent();

		for(MatchClassFilterFunction matchClassFilter : bearer.getMatchClassFilterFunctions()) {
			if(matchClassFilter instanceof MatchClassFilterFunctionInternal) {
				MatchClassFilterFunctionInternal matchClassFilterFunction = (MatchClassFilterFunctionInternal)matchClassFilter;
				forceNotConstant(matchClassFilterFunction.getComputationStatements());
				genMatchClassFilterFunction(sb, matchClassFilterFunction, be.system.emitProfilingInstrumentation());
			}
		}

		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();

		for(MatchClassFilterFunction matchClassFilter : bearer.getMatchClassFilterFunctions()) {
			if(matchClassFilter instanceof MatchClassFilterFunctionInternal) {
				MatchClassFilterFunctionInternal matchClassFilterFunction = (MatchClassFilterFunctionInternal)matchClassFilter;
				genLocalContainersEvals(sb, matchClassFilterFunction.getComputationStatements(), staticInitializers,
						pathPrefixForElements, alreadyDefinedEntityToName);
			}
		}

		genStaticConstructor(sb, "MatchClassFilters", staticInitializers);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genMatchClassFilterFunction(SourceBuilder sb, MatchClassFilterFunctionInternal matchClassFilter,
			boolean emitProfilingInstrumentation)
	{
		String packagePrefix = getPackagePrefixDot(matchClassFilter.getMatchClass());
		String matchClassName = formatIdentifiable(matchClassFilter.getMatchClass());
		sb.appendFront("public static void ");
		sb.append("Filter_" + matchClassFilter.getIdent().toString()
				+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IList<GRGEN_LIBGR.IMatch> matches");
		for(Entity inParam : matchClassFilter.getParameters()) {
			sb.append(", ");
			sb.append(formatType(inParam.getType()));
			sb.append(" ");
			sb.append(formatEntity(inParam));
		}
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = procEnv;\n");
		sb.appendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)procEnv.Graph;\n");
		ModifyGenerationState modifyGenState = new ModifyGenerationState(model, matchClassName, packagePrefix, false,
				emitProfilingInstrumentation);
		ModifyEvalGen evalGen = new ModifyEvalGen(be, null, nodeTypePrefix, edgeTypePrefix);
		EvalStatement lastEvalStmt = null;
		for(EvalStatement evalStmt : matchClassFilter.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = matchClassFilter.getIdent().toString();
			evalGen.genEvalStmt(sb, modifyGenState, evalStmt);
			lastEvalStmt = evalStmt;
		}
		if(!(lastEvalStmt instanceof ReturnStatementFilter)) {
			// ensure that FromList is called if the user omitted return
			evalGen.genEvalStmt(sb, modifyGenState, new ReturnStatementFilter());
		}
		sb.unindent();
		sb.append("}\n");
	}

	/**
	 * Generates the match classes (of match classes)
	 */
	private void genMatchClasses(SourceBuilder sb, ActionsBearer bearer, String packageName)
	{
		for(DefinedMatchType matchClass : bearer.getMatchClasses()) {
			genMatchClass(sb, matchClass, packageName);
		}

		sb.append("\n");
	}

	private void genMatchClass(SourceBuilder sb, DefinedMatchType matchClass, String packageName)
	{
		// generate getters to contained nodes, edges, variables
		HashSet<String> elementsAlreadyDeclared = new HashSet<String>();
		ActionsMatchGen matchGen = new ActionsMatchGen(nodeTypePrefix, edgeTypePrefix);
		matchGen.genPatternMatchInterface(sb, matchClass.getPatternGraph(),
				matchClass.getPatternGraph().getNameOfGraph(), "GRGEN_LIBGR.IMatch",
				matchClass.getPatternGraph().getNameOfGraph() + "_",
				false, false, true, elementsAlreadyDeclared);

		sb.append("\n");

		genMatchClassInfo(sb, matchClass, packageName);

		sb.append("\n");

		genArraySortBy(sb, matchClass);
		sb.append("\n");
	}

	private void genMatchClassInfo(SourceBuilder sb, DefinedMatchType matchClass, String packageName)
	{
		String matchClassName = formatIdentifiable(matchClass);
		String className = "MatchClassInfo_" + matchClassName;
		String pathPrefix = "";
		String pathPrefixForElements = pathPrefix + matchClass.getPatternGraph().getNameOfGraph() + "_";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();

		SourceBuilder sbElements = new SourceBuilder();

		SourceBuilder aux = new SourceBuilder();
		aux.indent().indent().indent();

		HashMap<Identifiable, String> alreadyDefinedIdentifiableToName = new HashMap<Identifiable, String>();
		double max = computePriosMax(-1, matchClass.getPatternGraph());
		String patGraphVarName = "pat_" + matchClass.getPatternGraph().getNameOfGraph();
		List<Entity> parameters = new LinkedList<Entity>();
		genElementsRequiredByPatternGraph(sbElements, aux, matchClass.getPatternGraph(), pathPrefix, matchClassName,
				packageName, patGraphVarName, className,
				alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, parameters, max, true);

		alreadyDefinedEntityToName = new HashMap<Entity, String>();
		genAllowedTypeArrays(sbElements, matchClass.getPatternGraph(), pathPrefixForElements,
				alreadyDefinedEntityToName);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.MatchClassInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + matchClassName + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName != null ? packageName + "::" + matchClassName : matchClassName) + "\",\n");
		sb.appendFront("new GRGEN_LIBGR.IPatternNode[] ");
		genEntitySet(sb, matchClass.getPatternGraph().getNodes(), "", "", true, pathPrefixForElements,
				alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LIBGR.IPatternEdge[] ");
		genEntitySet(sb, matchClass.getPatternGraph().getEdges(), "", "", true, pathPrefixForElements,
				alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LIBGR.IPatternVariable[] ");
		genEntitySet(sb, matchClass.getPatternGraph().getVars(), "", "", true, pathPrefixForElements,
				alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LIBGR.IFilter[] {\n");
		sb.indent();

		genMatchClassFilterAutoSupplied(sb, "keepFirst", packageName, "int");
		genMatchClassFilterAutoSupplied(sb, "keepLast", packageName, "int");
		genMatchClassFilterAutoSupplied(sb, "keepFirstFraction", packageName, "double");
		genMatchClassFilterAutoSupplied(sb, "keepLastFraction", packageName, "double");
		genMatchClassFilterAutoSupplied(sb, "removeFirst", packageName, "int");
		genMatchClassFilterAutoSupplied(sb, "removeLast", packageName, "int");
		genMatchClassFilterAutoSupplied(sb, "removeFirstFraction", packageName, "double");
		genMatchClassFilterAutoSupplied(sb, "removeLastFraction", packageName, "double");

		for(MatchClassFilter matchClassFilter : matchClass.getMatchClassFilters()) {
			if(matchClassFilter instanceof MatchClassFilterAutoGenerated) {
				genMatchClassFilterAutoGenerated(sb, (MatchClassFilterAutoGenerated)matchClassFilter, packageName);
			} else {
				genMatchClassFilterFunction(sb, (MatchClassFilterFunction)matchClassFilter, packageName);
			}
		}

		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.indent();
		addAnnotations(sb, matchClass, "annotations");

		sb.append(aux.toString());

		sb.unindent();
		sb.appendFront("}\n");

		sb.append("\n");
		sb.append(sbElements.toString());

		sb.append("\n");
		genMatchClassExtractor(sb, matchClass);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the match classes (of pattern and contained patterns)
	 */
	private void genMatch(SourceBuilder sb, PatternGraph pattern, Collection<DefinedMatchType> implementedMatchClasses,
			String className, boolean parallelized)
	{
		HashSet<String> elementsAlreadyDeclared = new HashSet<String>();
		String base = "";
		if(implementedMatchClasses == null || implementedMatchClasses.isEmpty()) {
			base = "GRGEN_LIBGR.IMatch";
		} else {
			boolean first = true;
			for(DefinedMatchType implementedMatchClass : implementedMatchClasses) {
				for(Node node : implementedMatchClass.getNodes()) {
					elementsAlreadyDeclared.add(formatEntity(node));
				}
				for(Edge edge : implementedMatchClass.getEdges()) {
					elementsAlreadyDeclared.add(formatEntity(edge));
				}
				for(Variable var : implementedMatchClass.getVars()) {
					elementsAlreadyDeclared.add(formatEntity(var));
				}
				if(first) {
					first = false;
				} else {
					base += ", ";
				}
				String packagePrefix = implementedMatchClass.getPackageContainedIn() != null
						? implementedMatchClass.getPackageContainedIn() + "."
						: "";
				base += packagePrefix + "IMatch_" + implementedMatchClass.getName();
			}
		}

		// generate getters to contained nodes, edges, variables, embedded graphs, alternatives
		ActionsMatchGen matchGen = new ActionsMatchGen(nodeTypePrefix, edgeTypePrefix);
		matchGen.genPatternMatchInterface(sb, pattern, pattern.getNameOfGraph(),
				base, pattern.getNameOfGraph() + "_",
				false, false, false, elementsAlreadyDeclared);

		// generate contained nodes, edges, variables, embedded graphs, alternatives
		// and the implementation of the various getters from IMatch and the pattern specific match interface
		String patGraphVarName = "pat_" + pattern.getNameOfGraph();
		matchGen.genPatternMatchImplementation(sb, pattern, pattern.getNameOfGraph(),
				patGraphVarName, className, pattern.getNameOfGraph() + "_", false, false, parallelized);
	}

	/**
	 * Generates the Extractor class with the Extract helper functions (returning an array of the extracted match element type from an array of match type)
	 */
	void genExtractor(SourceBuilder sb, Rule actionRule, Rule iteratedRule)
	{
		String iteratedRuleSuffix = iteratedRule != null ? "_" + formatIdentifiable(iteratedRule) : "";

		sb.appendFront("public class Extractor" + iteratedRuleSuffix + "\n");
		sb.appendFront("{\n");
		sb.indent();

		PatternGraph pattern = iteratedRule != null ? iteratedRule.getPattern() : actionRule.getPattern();
		String matchTypeName = "IMatch_" + actionRule.getPattern().getNameOfGraph() + iteratedRuleSuffix;

		for(Node node : pattern.getNodes()) {
			genExtractMethod(sb, matchTypeName, node);
		}
		for(Edge edge : pattern.getEdges()) {
			genExtractMethod(sb, matchTypeName, edge);
		}
		for(Variable var : pattern.getVars()) {
			genExtractMethod(sb, matchTypeName, var);
		}

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genExtractMethod(SourceBuilder sb, String matchTypeName, Entity entity)
	{
		sb.appendFront("public static List<" + formatType(entity.getType()) + "> Extract_" + formatIdentifiable(entity)
				+ "(List<" + matchTypeName + "> matchList)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("List<" + formatType(entity.getType()) + "> resultList = new List<"
				+ formatType(entity.getType()) + ">(matchList.Count);\n");
		sb.appendFront("foreach(" + matchTypeName + " match in matchList)\n");
		sb.indent();
		sb.appendFront("resultList.Add(match." + formatEntity(entity) + ");\n");
		sb.unindent();
		sb.appendFront("return resultList;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the Array_orderAscendingBy_member/Array_orderDescendingBy_member function plus the Comparison helper (shared with the corresponding orderAscendingBy/orderDescendingBy filter)
	 */
	void genArraySortBy(SourceBuilder sb, Rule actionRule, MemberBearerType memberBearerType, Rule iteratedRule)
	{
		sb.appendFront("public partial class MatchFilters\n");
		sb.appendFront("{\n");
		sb.indent();

		Rule rule = iteratedRule != null ? iteratedRule : actionRule;
		for(Variable var : rule.getPattern().getVars()) {
			if(var.getType().isOrderableType()) {
				generateComparerAndArrayOrderBy(sb, actionRule, memberBearerType, iteratedRule, var, true);
				generateComparerAndArrayOrderBy(sb, actionRule, memberBearerType, iteratedRule, var, false);
				generateArrayKeepOneForEach(sb, actionRule, memberBearerType, iteratedRule, var);
			}
		}

		for(Variable var : rule.getPattern().getVars()) {
			if(var.getType().isFilterableType() && !var.getType().isOrderableType()) {
				generateArrayKeepOneForEach(sb, actionRule, memberBearerType, iteratedRule, var);
			}
		}

		for(Node node : rule.getPattern().getNodes()) {
			generateArrayKeepOneForEach(sb, actionRule, memberBearerType, iteratedRule, node);
		}

		for(Edge edge : rule.getPattern().getEdges()) {
			generateArrayKeepOneForEach(sb, actionRule, memberBearerType, iteratedRule, edge);
		}

		sb.unindent();
		sb.appendFront("}\n");
	}

	void generateComparerAndArrayOrderBy(SourceBuilder sb, Identifiable memberBearer,
			MemberBearerType memberBearerType, Rule iteratedRule, Variable var, boolean ascending)
	{
		String name = formatIdentifiable(memberBearer);
		String iteratedNameComponent = iteratedRule != null ? "_" + formatIdentifiable(iteratedRule) : "";
		String memberBearerClass;
		if(memberBearerType == MemberBearerType.Action)
			memberBearerClass = "Rule_" + name + ".";
		else if(memberBearerType == MemberBearerType.Subpattern)
			memberBearerClass = "Pattern_" + name + ".";
		else //if(memberBearerType == MemberBearerType.MatchClass)
			memberBearerClass = "";
		String matchInterfaceName = "GRGEN_ACTIONS." + getPackagePrefixDot(memberBearer)
				+ memberBearerClass + "IMatch_" + name + iteratedNameComponent;
		String functionName = ascending
				? "orderAscendingBy_" + formatIdentifiable(var)
				: "orderDescendingBy_" + formatIdentifiable(var);
		String arrayFunctionName = "Array_" + name + iteratedNameComponent + "_" + functionName;
		String comparerName = "Comparer_" + name + iteratedNameComponent + "_" + functionName;

		sb.appendFront("public static List<" + matchInterfaceName + "> " + arrayFunctionName
				+ "(List<" + matchInterfaceName + "> list)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("List<" + matchInterfaceName + "> newList = new List<" + matchInterfaceName + ">(list);\n");
		sb.appendFront("newList.Sort(new " + comparerName + "());\n");
		sb.appendFront("return newList;\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("class " + comparerName + " : Comparer<" + matchInterfaceName + ">\n");
		sb.appendFront("{\n");
		sb.indent();

		genCompareMethod(sb, matchInterfaceName, formatEntity(var), var.getType(), ascending);

		sb.unindent();
		sb.appendFront("}\n");
	}

	void generateArrayKeepOneForEach(SourceBuilder sb, Identifiable memberBearer,
			MemberBearerType memberBearerType, Rule iteratedRule, Entity entity)
	{
		String name = formatIdentifiable(memberBearer);
		String iteratedNameComponent = iteratedRule != null ? "_" + formatIdentifiable(iteratedRule) : "";
		String memberBearerClass;
		if(memberBearerType == MemberBearerType.Action)
			memberBearerClass = "Rule_" + name + ".";
		else if(memberBearerType == MemberBearerType.Subpattern)
			memberBearerClass = "Pattern_" + name + ".";
		else //if(memberBearerType == MemberBearerType.MatchClass)
			memberBearerClass = "";
		String matchInterfaceName = "GRGEN_ACTIONS." + getPackagePrefixDot(memberBearer)
				+ memberBearerClass + "IMatch_" + name + iteratedNameComponent;
		String functionName = "keepOneForEachBy_" + formatIdentifiable(entity);
		String arrayFunctionName = "Array_" + name + iteratedNameComponent + "_" + functionName;

		generateArrayKeepOneForEach(sb, arrayFunctionName, matchInterfaceName,
				formatEntity(entity), formatType(entity.getType()));
	}

	/**
	 * Generates the Extractor class with the Extract helper functions (returning an array of the extracted match class element type from an array of match class type)
	 */
	void genMatchClassExtractor(SourceBuilder sb, DefinedMatchType matchClass)
	{
		sb.appendFront("public class Extractor\n");
		sb.appendFront("{\n");

		String matchTypeName = "IMatch_" + matchClass.getIdent().toString();

		for(Node node : matchClass.getNodes()) {
			genExtractMethod(sb, matchTypeName, node);
		}
		for(Edge edge : matchClass.getEdges()) {
			genExtractMethod(sb, matchTypeName, edge);
		}
		for(Variable var : matchClass.getVars()) {
			genExtractMethod(sb, matchTypeName, var);
		}

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the Array_sortAscendingBy_member function plus the Comparison helper (shared with the corresponding sortAscendingBy filter)
	 */
	void genArraySortBy(SourceBuilder sb, DefinedMatchType matchClass)
	{
		sb.appendFront("public partial class MatchClassFilters\n");
		sb.appendFront("{\n");
		sb.indent();

		for(Variable var : matchClass.getVars()) {
			if(var.getType().isOrderableType()) {
				generateComparerAndArrayOrderBy(sb, matchClass, MemberBearerType.MatchClass, null, var, true);
				generateComparerAndArrayOrderBy(sb, matchClass, MemberBearerType.MatchClass, null, var, false);
				generateArrayKeepOneForEach(sb, matchClass, MemberBearerType.MatchClass, null, var);
			}
		}

		for(Variable var : matchClass.getVars()) {
			if(var.getType().isFilterableType() && !var.getType().isOrderableType()) {
				generateArrayKeepOneForEach(sb, matchClass, MemberBearerType.MatchClass, null, var);
			}
		}

		for(Node node : matchClass.getNodes()) {
			generateArrayKeepOneForEach(sb, matchClass, MemberBearerType.MatchClass, null, node);
		}

		for(Edge edge : matchClass.getEdges()) {
			generateArrayKeepOneForEach(sb, matchClass, MemberBearerType.MatchClass, null, edge);
		}

		sb.unindent();
		sb.appendFront("}\n");
	}

	//////////////////////////////////////////////////
	// rule or subpattern class entities generation //
	//////////////////////////////////////////////////

	private void genRuleOrSubpatternClassEntities(SourceBuilder sb, Rule rule,
			String patGraphVarName, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		PatternGraph pattern = rule.getPattern();
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);
		genLocalContainers(sb, rule, staticInitializers, pathPrefixForElements,
				alreadyDefinedEntityToName);
		sb.appendFront("public GRGEN_LGSP.PatternGraph " + patGraphVarName + ";\n");
		sb.append("\n");

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
					alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, neg, pathPrefixForElements + negName, staticInitializers,
					pathPrefixForElements + negName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
					alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, idpt, pathPrefixForElements + idptName, staticInitializers,
					pathPrefixForElements + idptName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			genCaseEnum(sb, alt, pathPrefixForElements + altName + "_");
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
						alreadyDefinedEntityToName);
				genRuleOrSubpatternClassEntities(sb, altCase, altPatGraphVarName, staticInitializers,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						alreadyDefinedEntityToNameClone);
			}
		}

		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
					alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, iter, pathPrefixForElements + iterName, staticInitializers,
					pathPrefixForElements + iterName + "_",
					alreadyDefinedEntityToNameClone);
		}
	}

	private void genRuleOrSubpatternClassEntities(SourceBuilder sb, PatternGraph pattern,
			String patGraphVarName, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);
		genLocalContainers(sb, pattern, staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		sb.appendFront("public GRGEN_LGSP.PatternGraph " + patGraphVarName + ";\n");
		sb.append("\n");

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
					alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, neg, pathPrefixForElements + negName, staticInitializers,
					pathPrefixForElements + negName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
					alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, idpt, pathPrefixForElements + idptName, staticInitializers,
					pathPrefixForElements + idptName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			genCaseEnum(sb, alt, pathPrefixForElements + altName + "_");
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
						alreadyDefinedEntityToName);
				genRuleOrSubpatternClassEntities(sb, altCase, altPatGraphVarName, staticInitializers,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						alreadyDefinedEntityToNameClone);
			}
		}

		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(
					alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, iter, pathPrefixForElements + iterName, staticInitializers,
					pathPrefixForElements + iterName + "_",
					alreadyDefinedEntityToNameClone);
		}
	}

	private void genAllowedTypeArrays(SourceBuilder sb, PatternGraph pattern,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		genAllowedNodeTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genAllowedEdgeTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
	}

	private void genAllowedNodeTypeArrays(SourceBuilder sb, PatternGraph pattern,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		SourceBuilder aux = new SourceBuilder();
		aux.indent().indent();

		for(Node node : pattern.getNodes()) {
			if(alreadyDefinedEntityToName.get(node) != null) {
				continue;
			}
			sb.appendFront("public static GRGEN_LIBGR.NodeType[] "
					+ formatEntity(node, pathPrefixForElements) + "_AllowedTypes = ");
			aux.appendFront("public static bool[] " + formatEntity(node, pathPrefixForElements) + "_IsAllowedType = ");
			if(!node.getConstraints().isEmpty()) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : node.getConstraints())
					for(Type type : model.getAllNodeTypes()) {
						if(type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : model.getAllNodeTypes()) {
					boolean isAllowed = type.isCastableTo(node.getNodeType()) && !allForbiddenTypes.contains(type);
					// all permitted nodes, aka nodes that are not forbidden
					if(isAllowed)
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

		sb.append(aux.toString());
	}

	private void genAllowedEdgeTypeArrays(SourceBuilder sb, PatternGraph pattern,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		SourceBuilder aux = new SourceBuilder();
		aux.indent().indent();

		for(Edge edge : pattern.getEdges()) {
			if(alreadyDefinedEntityToName.get(edge) != null) {
				continue;
			}
			sb.appendFront("public static GRGEN_LIBGR.EdgeType[] "
					+ formatEntity(edge, pathPrefixForElements) + "_AllowedTypes = ");
			aux.appendFront("public static bool[] " + formatEntity(edge, pathPrefixForElements) + "_IsAllowedType = ");
			if(!edge.getConstraints().isEmpty()) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : edge.getConstraints())
					for(Type type : model.getAllEdgeTypes()) {
						if(type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : model.getAllEdgeTypes()) {
					boolean isAllowed = type.isCastableTo(edge.getEdgeType()) && !allForbiddenTypes.contains(type);
					// all permitted nodes, aka node that are not forbidden
					if(isAllowed)
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

		sb.append(aux.toString());
	}

	private void genEnums(SourceBuilder sb, PatternGraph pattern, String pathPrefixForElements)
	{
		sb.appendFront("public enum " + pathPrefixForElements + "NodeNums { ");
		for(Node node : pattern.getNodes()) {
			sb.append("@" + formatIdentifiable(node) + ", ");
		}
		sb.append("};\n");

		sb.appendFront("public enum " + pathPrefixForElements + "EdgeNums { ");
		for(Edge edge : pattern.getEdges()) {
			sb.append("@" + formatIdentifiable(edge) + ", ");
		}
		sb.append("};\n");

		sb.appendFront("public enum " + pathPrefixForElements + "VariableNums { ");
		for(Variable var : pattern.getVars()) {
			sb.append("@" + formatIdentifiable(var) + ", ");
		}
		sb.append("};\n");

		sb.appendFront("public enum " + pathPrefixForElements + "SubNums { ");
		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			sb.append("@" + formatIdentifiable(sub) + ", ");
		}
		sb.append("};\n");

		sb.appendFront("public enum " + pathPrefixForElements + "AltNums { ");
		for(Alternative alt : pattern.getAlts()) {
			sb.append("@" + alt.getNameOfGraph() + ", ");
		}
		sb.append("};\n");

		sb.appendFront("public enum " + pathPrefixForElements + "IterNums { ");
		for(Rule iter : pattern.getIters()) {
			sb.append("@" + iter.getLeft().getNameOfGraph() + ", ");
		}
		sb.append("};\n");
	}

	private void genCaseEnum(SourceBuilder sb, Alternative alt, String pathPrefixForElements)
	{
		sb.appendFront("public enum " + pathPrefixForElements + "CaseNums { ");
		for(Rule altCase : alt.getAlternativeCases()) {
			PatternGraph altCasePattern = altCase.getLeft();
			sb.append("@" + altCasePattern.getNameOfGraph() + ", ");
		}
		sb.append("};\n");
	}

	private void genLocalContainers(SourceBuilder sb, Rule rule,
			List<String> staticInitializers, String pathPrefixForElements,
			HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		genLocalContainers(sb, rule.getLeft(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);

		for(EvalStatements evals : rule.getEvals()) {
			genLocalContainersEvals(sb, evals.evalStatements, staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}
		genLocalContainersReturns(sb, rule.getReturns(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		if(rule.getRight() != null) {
			genLocalContainersInitializations(sb, rule.getRight(), rule.getLeft(), staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
			genLocalContainersImperativeStatements(sb, rule.getRight().getImperativeStmts(), staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}
	}

	private void genLocalContainers(SourceBuilder sb, PatternGraph pattern,
			List<String> staticInitializers, String pathPrefixForElements,
			HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		genLocalContainersInitializations(sb, pattern, staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		genLocalContainersConditions(sb, pattern, staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		for(EvalStatements evals : pattern.getYields()) {
			genLocalContainersEvals(sb, evals.evalStatements, staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}
	}

	private void genLocalContainersInitializations(SourceBuilder sb, PatternGraph rhsPattern,
			PatternGraph directlyNestingLHSPattern, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Variable var : rhsPattern.getVars()) {
			if(var.initialization != null) {
				if(var.directlyNestingLHSGraph == directlyNestingLHSPattern
						&& (var.getContext() & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
					var.initialization.collectNeededEntities(needs);
				}
			}
		}
		genLocalContainers(sb, needs, staticInitializers, false);
	}

	private void genLocalContainersInitializations(SourceBuilder sb, PatternGraph pattern,
			List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Variable var : pattern.getVars()) {
			if(var.initialization != null) {
				if(var.directlyNestingLHSGraph == pattern) {
					var.initialization.collectNeededEntities(needs);
				}
			}
		}
		genLocalContainers(sb, needs, staticInitializers, false);
	}

	private void genLocalContainersConditions(SourceBuilder sb, PatternGraph pattern, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Expression expr : pattern.getConditions()) {
			expr.collectNeededEntities(needs);
		}
		genLocalContainers(sb, needs, staticInitializers, true);
	}

	// type collision with the method below cause java can't distinguish List<Expression> from List<ImperativeStmt>
	private void genLocalContainersReturns(SourceBuilder sb, List<Expression> returns, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Expression expr : returns) {
			expr.collectNeededEntities(needs);
		}
		genLocalContainers(sb, needs, staticInitializers, true);
	}

	private void genLocalContainersImperativeStatements(SourceBuilder sb, List<ImperativeStmt> istmts,
			List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(ImperativeStmt istmt : istmts) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit)istmt;
				for(Expression arg : emit.getArguments())
					arg.collectNeededEntities(needs);
			}
		}
		genLocalContainers(sb, needs, staticInitializers, false);
	}

	/////////////////////////////////////////
	// Rule/Subpattern metadata generation //
	/////////////////////////////////////////

	private void genRuleOrSubpatternInit(SourceBuilder sb, MatchingAction action,
			String className, String packageName, boolean isSubpattern)
	{
		PatternGraph pattern = action.getPattern();

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(");
		sb.indent();
		sb.append("\"" + formatIdentifiable(action) + "\",\n");
		genRuleParam(sb, action, packageName); // no closing \n
		if(!isSubpattern) {
			genRuleResult(sb, (Rule)action, packageName);
			genRuleFilter(sb, (Rule)action, packageName);
			genRuleMatchClassInfo(sb, (Rule)action, packageName); // no closing \n
		}
		sb.append("\n");
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.indent();
		addAnnotations(sb, action, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();
		HashMap<Identifiable, String> alreadyDefinedIdentifiableToName = new HashMap<Identifiable, String>();

		double max = computePriosMax(-1, action.getPattern());

		SourceBuilder aux = new SourceBuilder();
		aux.indent().indent().indent();

		String patGraphVarName = "pat_" + pattern.getNameOfGraph();
		sb.appendFront("private void initialize()\n");
		sb.appendFront("{\n");
		sb.indent();

		genPatternGraph(sb, aux, pattern, "", pattern.getNameOfGraph(), packageName, patGraphVarName, className,
				alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, action.getParameters(), max);

		sb.append(aux.toString());

		sb.append("\n");
		sb.appendFront("patternGraph = " + patGraphVarName + ";\n");

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genPatternGraph(SourceBuilder sb, SourceBuilder aux, PatternGraph pattern,
			String pathPrefix, String patternName, String packageName, // negatives without name, have to compute it and hand it in
			String patGraphVarName, String className,
			HashMap<Entity, String> alreadyDefinedEntityToName,
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToName,
			List<Entity> parameters, double max)
	{
		genElementsRequiredByPatternGraph(sb, aux, pattern, pathPrefix, patternName, packageName, patGraphVarName, className,
				alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, parameters, max, false);

		sb.appendFront(patGraphVarName + " = new GRGEN_LGSP.PatternGraph(\n");
		sb.indent();
		sb.appendFront("\"" + patternName + "\",\n");
		sb.appendFront("\"" + pathPrefix + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName != null ? packageName + "::" : "") + patternName + "\",\n");
		sb.appendFront((pattern.isPatternpathLocked() ? "true" : "false") + ", ");
		sb.append((pattern.isIterationBreaking() ? "true" : "false") + ",\n");

		String pathPrefixForElements = pathPrefix + patternName + "_";

		sb.appendFront("new GRGEN_LGSP.PatternNode[] ");
		genEntitySet(sb, pattern.getNodes(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.appendFront("new GRGEN_LGSP.PatternEdge[] ");
		genEntitySet(sb, pattern.getEdges(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.appendFront("new GRGEN_LGSP.PatternVariable[] ");
		genEntitySet(sb, pattern.getVars(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", \n");

		sb.appendFront("new GRGEN_LGSP.PatternGraphEmbedding[] ");
		genSubpatternUsageSet(sb, pattern.getSubpatternUsages(), "", "", true, pathPrefixForElements,
				alreadyDefinedIdentifiableToName);
		sb.append(", \n");

		sb.appendFront("new GRGEN_LGSP.Alternative[] { ");
		for(Alternative alt : pattern.getAlts()) {
			sb.append(pathPrefixForElements + alt.getNameOfGraph() + ", ");
		}
		sb.append(" }, \n");

		sb.appendFront("new GRGEN_LGSP.Iterated[] { ");
		for(Rule iter : pattern.getIters()) {
			sb.append(pathPrefixForElements + iter.getLeft().getNameOfGraph() + "_it" + ", ");
		}
		sb.append(" }, \n");

		sb.appendFront("new GRGEN_LGSP.PatternGraph[] { ");
		for(PatternGraph neg : pattern.getNegs()) {
			sb.append(pathPrefixForElements + neg.getNameOfGraph() + ", ");
		}
		sb.append(" }, \n");

		sb.appendFront("new GRGEN_LGSP.PatternGraph[] { ");
		for(PatternGraph idpt : pattern.getIdpts()) {
			sb.append(pathPrefixForElements + idpt.getNameOfGraph() + ", ");
		}
		sb.append(" }, \n");

		sb.appendFront("new GRGEN_LGSP.PatternCondition[] { ");
		for(int i = 0; i < pattern.getConditions().size(); i++) {
			sb.append(pathPrefixForElements + "cond_" + i + ", ");
		}
		sb.append(" }, \n");

		sb.appendFront("new GRGEN_LGSP.PatternYielding[] { ");
		for(EvalStatements evals : pattern.getYields()) {
			sb.append(pathPrefixForElements + evals.getName() + ", ");
		}
		sb.append(" }, \n");

		sb.appendFront("new bool[" + pattern.getNodes().size() + ", " + pattern.getNodes().size() + "] ");
		genNodeHomMatrix(sb, pattern);
		sb.append(",\n");

		sb.appendFront("new bool[" + pattern.getEdges().size() + ", " + pattern.getEdges().size() + "] ");
		genEdgeHomMatrix(sb, pattern);
		sb.append(",\n");

		sb.appendFront(pathPrefixForElements + "isNodeHomomorphicGlobal,\n");

		sb.appendFront(pathPrefixForElements + "isEdgeHomomorphicGlobal,\n");

		sb.appendFront(pathPrefixForElements + "isNodeTotallyHomomorphic,\n");

		sb.appendFront(pathPrefixForElements + "isEdgeTotallyHomomorphic\n");

		sb.unindent();
		sb.appendFront(");\n");

		linkEdgesToNodes(sb, pattern, patGraphVarName, alreadyDefinedEntityToName, pathPrefixForElements);

		setEmbeddingGraph(sb, pattern, patGraphVarName, pathPrefixForElements);

		sb.append("\n");
	}

	private void genNodeHomMatrix(SourceBuilder sb, PatternGraph pattern)
	{
		if(pattern.getNodes().size() > 0) {
			sb.append("{\n");
			sb.indent();
			for(Node node1 : pattern.getNodes()) {
				sb.appendFront("{ ");
				for(Node node2 : pattern.getNodes()) {
					if(pattern.isHomomorphic(node1, node2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.unindent();
			sb.appendFront("}");
		}
	}

	private void genEdgeHomMatrix(SourceBuilder sb, PatternGraph pattern)
	{
		if(pattern.getEdges().size() > 0) {
			sb.append("{\n");
			sb.indent();
			for(Edge edge1 : pattern.getEdges()) {
				sb.appendFront("{ ");
				for(Edge edge2 : pattern.getEdges()) {
					if(pattern.isHomomorphic(edge1, edge2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.unindent();
			sb.appendFront("}");
		}
	}

	private void linkEdgesToNodes(SourceBuilder sb, PatternGraph pattern, String patGraphVarName,
			HashMap<Entity, String> alreadyDefinedEntityToName, String pathPrefixForElements)
	{
		for(Edge edge : pattern.getEdges()) {
			String edgeName = alreadyDefinedEntityToName.get(edge) != null
					? alreadyDefinedEntityToName.get(edge)
					: formatEntity(edge, pathPrefixForElements);

			if(pattern.getSource(edge) != null) {
				String sourceName = formatEntity(pattern.getSource(edge), pathPrefixForElements,
						alreadyDefinedEntityToName);
				sb.appendFront(patGraphVarName + ".edgeToSourceNode.Add(" + edgeName + ", " + sourceName + ");\n");
			}

			if(pattern.getTarget(edge) != null) {
				String targetName = formatEntity(pattern.getTarget(edge), pathPrefixForElements,
						alreadyDefinedEntityToName);
				sb.appendFront(patGraphVarName + ".edgeToTargetNode.Add(" + edgeName + ", " + targetName + ");\n");
			}
		}
	}

	private void setEmbeddingGraph(SourceBuilder sb, PatternGraph pattern, String patGraphVarName,
			String pathPrefixForElements)
	{
		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				sb.appendFront(altPatGraphVarName + ".embeddingGraph = " + patGraphVarName + ";\n");
			}
		}

		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			sb.appendFront(pathPrefixForElements + iterName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			sb.appendFront(pathPrefixForElements + negName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			sb.appendFront(pathPrefixForElements + idptName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}
	}

	private void genElementsRequiredByPatternGraph(SourceBuilder sb, SourceBuilder aux, PatternGraph pattern,
			String pathPrefix, String patternName, String packageName,
			String patGraphVarName, String className,
			HashMap<Entity, String> alreadyDefinedEntityToName,
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToName,
			List<Entity> parameters, double max, boolean isMatchClass)
	{
		String pathPrefixForElements = pathPrefix + patternName + "_";

		if(!isMatchClass) {
			sb.appendFront("bool[,] " + pathPrefixForElements + "isNodeHomomorphicGlobal = "
					+ "new bool[" + pattern.getNodes().size() + ", " + pattern.getNodes().size() + "]");
			genNodeGlobalHomMatrix(sb, pattern, alreadyDefinedEntityToName);
			sb.append(";\n");

			sb.appendFront("bool[,] " + pathPrefixForElements + "isEdgeHomomorphicGlobal = "
					+ "new bool[" + pattern.getEdges().size() + ", " + pattern.getEdges().size() + "]");
			genEdgeGlobalHomMatrix(sb, pattern, alreadyDefinedEntityToName);
			sb.append(";\n");

			sb.appendFront("bool[] " + pathPrefixForElements + "isNodeTotallyHomomorphic = "
					+ "new bool[" + pattern.getNodes().size() + "]");
			genNodeTotallyHomArray(sb, pattern);
			sb.append(";\n");

			sb.appendFront("bool[] " + pathPrefixForElements + "isEdgeTotallyHomomorphic = "
					+ "new bool[" + pattern.getEdges().size() + "]");
			genEdgeTotallyHomArray(sb, pattern);
			sb.append(";\n");
		}

		for(Variable var : pattern.getVars()) {
			if(alreadyDefinedEntityToName.get(var) != null) {
				continue;
			}

			String varName = formatEntity(var, pathPrefixForElements);
			genPatternVariable(sb, aux, patGraphVarName, className, alreadyDefinedEntityToName,
					parameters, isMatchClass, pathPrefixForElements,
					var, varName);
			alreadyDefinedEntityToName.put(var, varName);
		}

		// Dependencies because an element requires another element (e.g. match by storage access)
		int dependencyLevel = 0;
		boolean somethingSkipped;
		do {
			somethingSkipped = false;

			for(Node node : pattern.getNodes()) {
				if(alreadyDefinedEntityToName.get(node) != null) {
					continue;
				}
				if(node.getDependencyLevel() > dependencyLevel) {
					somethingSkipped = true;
					continue;
				}

				String nodeName = formatEntity(node, pathPrefixForElements);
				genPatternNode(sb, aux, pattern, pathPrefix, patGraphVarName, className,
						alreadyDefinedEntityToName, parameters, max, isMatchClass, pathPrefixForElements,
						node, nodeName);
				alreadyDefinedEntityToName.put(node, nodeName);
			}

			for(Edge edge : pattern.getEdges()) {
				if(alreadyDefinedEntityToName.get(edge) != null) {
					continue;
				}
				if(edge.getDependencyLevel() > dependencyLevel) {
					somethingSkipped = true;
					continue;
				}

				String edgeName = formatEntity(edge, pathPrefixForElements);
				genPatternEdge(sb, aux, pattern, pathPrefix, patGraphVarName, className,
						alreadyDefinedEntityToName, parameters, max, isMatchClass, pathPrefixForElements,
						edge, edgeName);
				alreadyDefinedEntityToName.put(edge, edgeName);
			}

			++dependencyLevel;
		} while(somethingSkipped);

		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			if(alreadyDefinedIdentifiableToName.get(sub) != null) {
				continue;
			}

			String subName = formatIdentifiable(sub, pathPrefixForElements);
			genSubpatternEmbedding(sb, aux, patGraphVarName, className,
					alreadyDefinedEntityToName, pathPrefixForElements,
					sub, subName);
			alreadyDefinedIdentifiableToName.put(sub, subName);
		}

		int i = 0;
		for(Expression expr : pattern.getConditions()) {
			String condName = pathPrefixForElements + "cond_" + i;
			genPatternCondition(sb, className, alreadyDefinedEntityToName, pathPrefixForElements, expr, condName);
			++i;
		}

		for(EvalStatements yields : pattern.getYields()) {
			genPatternYielding(sb, className, alreadyDefinedEntityToName, pathPrefixForElements, yields);
		}

		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				HashMap<Entity, String> alreadyDefinedEntityToNameClone =
						new HashMap<Entity, String>(alreadyDefinedEntityToName);
				HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone =
						new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
				genPatternGraph(sb, aux, altCasePattern,
						pathPrefixForElements + altName + "_", altCasePattern.getNameOfGraph(), packageName,
						altPatGraphVarName, className,
						alreadyDefinedEntityToNameClone,
						alreadyDefinedIdentifiableToNameClone,
						parameters, max);
			}
		}

		for(Alternative alt : pattern.getAlts()) {
			getPatternAlternative(sb, pathPrefixForElements, alt);
		}

		for(Rule iter : pattern.getIters()) {
			PatternGraph iterPattern = iter.getLeft();
			String iterName = iterPattern.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone =
					new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone =
					new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			genPatternGraph(sb, aux, iterPattern,
					pathPrefixForElements, iterName, packageName,
					pathPrefixForElements + iterName, className,
					alreadyDefinedEntityToNameClone,
					alreadyDefinedIdentifiableToNameClone,
					parameters, max);
		}

		for(Rule iter : pattern.getIters()) {
			genPatternIterated(sb, packageName, pathPrefixForElements, iter);
		}

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone =
					new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone =
					new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			genPatternGraph(sb, aux, neg,
					pathPrefixForElements, negName, packageName,
					pathPrefixForElements + negName, className,
					alreadyDefinedEntityToNameClone,
					alreadyDefinedIdentifiableToNameClone,
					parameters, max);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone =
					new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone =
					new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			genPatternGraph(sb, aux, idpt,
					pathPrefixForElements, idptName, packageName,
					pathPrefixForElements + idptName, className,
					alreadyDefinedEntityToNameClone,
					alreadyDefinedIdentifiableToNameClone,
					parameters, max);
		}
	}

	private void genNodeGlobalHomMatrix(SourceBuilder sb, PatternGraph pattern,
			HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(pattern.getNodes().size() > 0) {
			sb.append(" {\n");
			sb.indent();
			for(Node node1 : pattern.getNodes()) {
				sb.appendFront("{ ");
				for(Node node2 : pattern.getNodes()) {
					if(pattern.isHomomorphicGlobal(alreadyDefinedEntityToName, node1, node2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.unindent();
			sb.appendFront("}");
		}
	}

	private void genEdgeGlobalHomMatrix(SourceBuilder sb, PatternGraph pattern,
			HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(pattern.getEdges().size() > 0) {
			sb.append(" {\n");
			sb.indent();
			for(Edge edge1 : pattern.getEdges()) {
				sb.appendFront("{ ");
				for(Edge edge2 : pattern.getEdges()) {
					if(pattern.isHomomorphicGlobal(alreadyDefinedEntityToName, edge1, edge2))
						sb.append("true, ");
					else
						sb.append("false, ");
				}
				sb.append("},\n");
			}
			sb.unindent();
			sb.appendFront("}");
		}
	}

	private void genNodeTotallyHomArray(SourceBuilder sb, PatternGraph pattern)
	{
		if(pattern.getNodes().size() > 0) {
			sb.append(" { ");
			for(Node node : pattern.getNodes()) {
				if(pattern.isTotallyHomomorphic(node))
					sb.append("true, ");
				else
					sb.append("false, ");
			}
			sb.append(" }");
		}
	}

	private void genEdgeTotallyHomArray(SourceBuilder sb, PatternGraph pattern)
	{
		if(pattern.getEdges().size() > 0) {
			sb.append(" { ");
			for(Edge edge : pattern.getEdges()) {
				if(pattern.isTotallyHomomorphic(edge))
					sb.append("true, ");
				else
					sb.append("false, ");
			}
			sb.append(" }");
		}
	}

	private String genPatternVariable(SourceBuilder sb, SourceBuilder aux, String patGraphVarName,
			String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			List<Entity> parameters, boolean isMatchClass, String pathPrefixForElements,
			Variable var, String varName)
	{
		sb.appendFront((isMatchClass ? "static " : "") + "GRGEN_LGSP.PatternVariable " + varName
				+ " = new GRGEN_LGSP.PatternVariable(");
		sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(var)
				+ ")), \"" + varName + "\", \"" + formatIdentifiable(var) + "\", ");
		sb.append(parameters.indexOf(var) + ", ");
		sb.append(var.isDefToBeYieldedTo() ? "true, " : "false, ");
		if(var.initialization != null) {
			eyGen.genExpressionTree(sb, var.initialization, className, pathPrefixForElements,
					alreadyDefinedEntityToName);
			sb.append(");\n");
		} else {
			sb.append("null);\n");
		}
		if(isMatchClass)
			aux.appendFront(varName + ".pointOfDefinition = null;\n");
		else {
			aux.appendFront(varName + ".pointOfDefinition = "
					+ (parameters.indexOf(var) == -1 ? patGraphVarName : "null") + ";\n");
		}
		addAnnotations(aux, var, varName + ".annotations");
		return varName;
	}

	private void genPatternNode(SourceBuilder sb, SourceBuilder aux, PatternGraph pattern, String pathPrefix,
			String patGraphVarName, String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			List<Entity> parameters, double max, boolean isMatchClass, String pathPrefixForElements,
			Node node, String nodeName)
	{
		sb.appendFront((isMatchClass ? "static " : "") + "GRGEN_LGSP.PatternNode " + nodeName
				+ " = new GRGEN_LGSP.PatternNode(");
		sb.append("(int) GRGEN_MODEL." + getPackagePrefixDot(node.getType()) + "NodeTypes.@"
				+ formatIdentifiable(node.getType())
				+ ", " + formatTypeClassRef(node.getType()) + ".typeVar"
				+ ", \"" + formatElementInterfaceRef(node.getType()) + "\", ");
		sb.append("\"" + nodeName + "\", \"" + formatIdentifiable(node) + "\", ");
		sb.append(nodeName + "_AllowedTypes, ");
		sb.append(nodeName + "_IsAllowedType, ");
		appendPrio(sb, node, max);
		sb.append(parameters.indexOf(node) + ", ");
		sb.append(node.getMaybeNull() ? "true, " : "false, ");
		genStorageAccess(sb, pathPrefix, alreadyDefinedEntityToName,
				pathPrefixForElements, node);
		genIndexAccess(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, node, parameters);
		genNameLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, node, parameters);
		genUniqueLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, node, parameters);
		sb.append((node instanceof RetypedNode
				? formatEntity(((RetypedNode)node).getOldNode(), pathPrefixForElements, alreadyDefinedEntityToName)
				: "null") + ", ");
		sb.append(node.isDefToBeYieldedTo() ? "true," : "false,");
		if(node.initialization != null) {
			eyGen.genExpressionTree(sb, node.initialization, className, pathPrefixForElements,
					alreadyDefinedEntityToName);
			sb.append(");\n");
		} else {
			sb.append("null);\n");
		}
		if(isMatchClass)
			aux.appendFront(nodeName + ".pointOfDefinition = null;\n");
		else {
			aux.appendFront(nodeName + ".pointOfDefinition = "
					+ (parameters.indexOf(node) == -1 ? patGraphVarName : "null") + ";\n");
		}
		addAnnotations(aux, node, nodeName + ".annotations");

		node.setPointOfDefinition(pattern);
	}

	private void genPatternEdge(SourceBuilder sb, SourceBuilder aux, PatternGraph pattern, String pathPrefix,
			String patGraphVarName, String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			List<Entity> parameters, double max, boolean isMatchClass, String pathPrefixForElements,
			Edge edge, String edgeName)
	{
		sb.appendFront((isMatchClass ? "static " : "") + "GRGEN_LGSP.PatternEdge " + edgeName
				+ " = new GRGEN_LGSP.PatternEdge(");
		sb.append((edge.hasFixedDirection() ? "true" : "false") + ", ");
		sb.append("(int) GRGEN_MODEL." + getPackagePrefixDot(edge.getType()) + "EdgeTypes.@"
				+ formatIdentifiable(edge.getType())
				+ ", " + formatTypeClassRef(edge.getType()) + ".typeVar"
				+ ", \"" + formatElementInterfaceRef(edge.getType()) + "\", ");
		sb.append("\"" + edgeName + "\", \"" + formatIdentifiable(edge) + "\", ");
		sb.append(edgeName + "_AllowedTypes, ");
		sb.append(edgeName + "_IsAllowedType, ");
		appendPrio(sb, edge, max);
		sb.append(parameters.indexOf(edge) + ", ");
		sb.append(edge.getMaybeNull() ? "true, " : "false, ");
		genStorageAccess(sb, pathPrefix, alreadyDefinedEntityToName,
				pathPrefixForElements, edge);
		genIndexAccess(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, edge, parameters);
		genNameLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, edge, parameters);
		genUniqueLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, edge, parameters);
		sb.append((edge instanceof RetypedEdge
				? formatEntity(((RetypedEdge)edge).getOldEdge(), pathPrefixForElements, alreadyDefinedEntityToName)
				: "null") + ", ");
		sb.append(edge.isDefToBeYieldedTo() ? "true," : "false,");
		if(edge.initialization != null) {
			eyGen.genExpressionTree(sb, edge.initialization, className, pathPrefixForElements,
					alreadyDefinedEntityToName);
			sb.append(");\n");
		} else {
			sb.append("null);\n");
		}
		if(isMatchClass)
			aux.appendFront(edgeName + ".pointOfDefinition = null;\n");
		else {
			aux.appendFront(edgeName + ".pointOfDefinition = "
					+ (parameters.indexOf(edge) == -1 ? patGraphVarName : "null") + ";\n");
		}
		addAnnotations(aux, edge, edgeName + ".annotations");

		edge.setPointOfDefinition(pattern);
	}

	private void genSubpatternEmbedding(SourceBuilder sb, SourceBuilder aux, String patGraphVarName,
			String className, HashMap<Entity, String> alreadyDefinedEntityToName, String pathPrefixForElements,
			SubpatternUsage sub, String subName)
	{
		sb.appendFront("GRGEN_LGSP.PatternGraphEmbedding " + subName
				+ " = new GRGEN_LGSP.PatternGraphEmbedding(");
		sb.append("\"" + formatIdentifiable(sub) + "\", ");
		sb.append(getPackagePrefixDot(sub.getSubpatternAction()) + "Pattern_"
				+ sub.getSubpatternAction().getIdent().toString() + ".Instance, \n");
		sb.indent();

		sb.appendFront("new GRGEN_EXPR.Expression[] {\n");
		NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(Expression expr : sub.getSubpatternConnections()) {
			expr.collectNeededEntities(needs);
			sb.appendFrontIndented("");
			eyGen.genExpressionTree(sb, expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(",\n");
		}
		sb.appendFront("}, \n");

		sb.appendFront("new string[] { ");
		for(Expression expr : sub.getSubpatternYields()) {
			sb.append("\"");
			if(expr instanceof VariableExpression) {
				VariableExpression ve = (VariableExpression)expr;
				sb.append(formatEntity(ve.getVariable(), pathPrefixForElements, alreadyDefinedEntityToName));
			} else {
				GraphEntityExpression ge = (GraphEntityExpression)expr;
				sb.append(formatEntity(ge.getGraphEntity(), pathPrefixForElements, alreadyDefinedEntityToName));
			}
			sb.append("\", ");
		}
		sb.append("}, ");
		sb.append("new GRGEN_LGSP.PatternElement[] { ");
		for(Expression expr : sub.getSubpatternYields()) {
			if(expr instanceof VariableExpression) {
				sb.append("null");
			} else {
				GraphEntityExpression ge = (GraphEntityExpression)expr;
				sb.append(formatEntity(ge.getGraphEntity(), pathPrefixForElements, alreadyDefinedEntityToName));
			}
			sb.append(", ");
		}
		sb.append("}, ");
		sb.append("new GRGEN_LGSP.PatternVariable[] { ");
		for(Expression expr : sub.getSubpatternYields()) {
			if(expr instanceof VariableExpression) {
				VariableExpression ve = (VariableExpression)expr;
				sb.append(formatEntity(ve.getVariable(), pathPrefixForElements, alreadyDefinedEntityToName));
			} else {
				sb.append("null");
			}
			sb.append(", ");
		}
		sb.append("},\n");
		sb.appendFront("new string[] ");
		genEntitySet(sb, needs.nodes, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new string[] ");
		genEntitySet(sb, needs.edges, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new string[] ");
		genEntitySet(sb, needs.variables, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LGSP.PatternNode[] ");
		genEntitySet(sb, needs.nodes, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_LGSP.PatternEdge[] ");
		genEntitySet(sb, needs.edges, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_LGSP.PatternVariable[] ");
		genEntitySet(sb, needs.variables, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);

		sb.unindent();
		sb.append(");\n");

		aux.appendFront(subName + ".PointOfDefinition = " + patGraphVarName + ";\n");

		addAnnotations(aux, sub, subName + ".annotations");
	}

	private void genPatternCondition(SourceBuilder sb, String className,
			HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, Expression expr, String condName)
	{
		NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
		expr.collectNeededEntities(needs);
		sb.appendFront("GRGEN_LGSP.PatternCondition " + condName + " = new GRGEN_LGSP.PatternCondition(\n");
		sb.indent();
		sb.appendFront("");
		eyGen.genExpressionTree(sb, expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new string[] ");
		genEntitySet(sb, needs.nodes, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new string[] ");
		genEntitySet(sb, needs.edges, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new string[] ");
		genEntitySet(sb, needs.variables, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LGSP.PatternNode[] ");
		genEntitySet(sb, needs.nodes, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_LGSP.PatternEdge[] ");
		genEntitySet(sb, needs.edges, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_LGSP.PatternVariable[] ");
		genEntitySet(sb, needs.variables, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(");\n");
		sb.unindent();
	}

	private void genPatternYielding(SourceBuilder sb, String className,
			HashMap<Entity, String> alreadyDefinedEntityToName, String pathPrefixForElements, EvalStatements yields)
	{
		String yieldName = pathPrefixForElements + yields.getName();
		sb.appendFront("GRGEN_LGSP.PatternYielding " + yieldName + " = new GRGEN_LGSP.PatternYielding(");
		sb.append("\"" + yields.getName() + "\",\n ");
		sb.indent();
		sb.appendFront("new GRGEN_EXPR.Yielding[] {\n");
		sb.indent();

		for(EvalStatement yield : yields.evalStatements) {
			eyGen.genYield(sb, yield, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(",\n");
		}

		sb.unindent();
		sb.appendFront("}, \n");

		NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
		yields.collectNeededEntities(needs);
		sb.appendFront("new string[] ");
		genEntitySet(sb, needs.nodes, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new string[] ");
		genEntitySet(sb, needs.edges, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new string[] ");
		genEntitySet(sb, needs.variables, "\"", "\"", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LGSP.PatternNode[] ");
		genEntitySet(sb, needs.nodes, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_LGSP.PatternEdge[] ");
		genEntitySet(sb, needs.edges, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_LGSP.PatternVariable[] ");
		genEntitySet(sb, needs.variables, "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(");\n");
		sb.unindent();
	}

	private void getPatternAlternative(SourceBuilder sb, String pathPrefixForElements, Alternative alt)
	{
		String altName = alt.getNameOfGraph();
		sb.appendFront("GRGEN_LGSP.Alternative " + pathPrefixForElements + altName + " = new GRGEN_LGSP.Alternative( ");
		sb.append("\"" + altName + "\", ");
		sb.append("\"" + pathPrefixForElements + "\", ");
		sb.append("new GRGEN_LGSP.PatternGraph[] ");
		genAlternativesSet(sb, alt.getAlternativeCases(), pathPrefixForElements + altName + "_", "", true);
		sb.append(" );\n\n");
	}

	private void genPatternIterated(SourceBuilder sb, String packageName, String pathPrefixForElements, Rule iter)
	{
		PatternGraph iterPattern = iter.getLeft();
		String iterName = iterPattern.getNameOfGraph();
		sb.appendFront("GRGEN_LGSP.Iterated " + pathPrefixForElements + iterName + "_it"
				+ " = new GRGEN_LGSP.Iterated( ");
		sb.append(pathPrefixForElements + iterName + ", ");
		sb.append(iter.getMinMatches() + ", ");
		sb.append(iter.getMaxMatches() + ", ");

		sb.append("new GRGEN_LGSP.LGSPFilter[] {\n");
		sb.indent();

		genFilterAutoSupplied(sb, "keepFirst", packageName, "int");
		genFilterAutoSupplied(sb, "keepLast", packageName, "int");
		genFilterAutoSupplied(sb, "keepFirstFraction", packageName, "double");
		genFilterAutoSupplied(sb, "keepLastFraction", packageName, "double");
		genFilterAutoSupplied(sb, "removeFirst", packageName, "int");
		genFilterAutoSupplied(sb, "removeLast", packageName, "int");
		genFilterAutoSupplied(sb, "removeFirstFraction", packageName, "double");
		genFilterAutoSupplied(sb, "removeLastFraction", packageName, "double");

		for(Filter filter : iter.getFilters()) {
			if(filter instanceof FilterAutoGenerated)
				genFilterAutoGenerated(sb, (FilterAutoGenerated)filter, packageName);
		}

		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront(");\n");
	}

	private void genStorageAccess(SourceBuilder sb, String pathPrefix,
			HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, GraphEntity entity)
	{
		if(entity.storageAccess != null) {
			if(entity.storageAccess.storageVariable != null) {
				Variable storageVariable = entity.storageAccess.storageVariable;
				sb.append("new GRGEN_LGSP.StorageAccess("
						+ formatEntity(storageVariable, pathPrefixForElements, alreadyDefinedEntityToName) + "), ");
			} else if(entity.storageAccess.storageAttribute != null) {
				Qualification storageAttribute = entity.storageAccess.storageAttribute;
				GraphEntity owner = (GraphEntity)storageAttribute.getOwner();
				Entity member = storageAttribute.getMember();
				sb.append("new GRGEN_LGSP.StorageAccess(new GRGEN_LGSP.QualificationAccess("
						+ formatEntity(owner, pathPrefix, alreadyDefinedEntityToName) + ", ");
				sb.append(formatTypeClassRef(
						owner.getParameterInterfaceType() != null ? owner.getParameterInterfaceType() : owner.getType())
						+ ".typeVar" + ".GetAttributeType(\"" + formatIdentifiable(member) + "\")");
				sb.append(")), ");
			}
		} else {
			sb.append("null, ");
		}
		if(entity.storageAccessIndex != null) {
			if(entity.storageAccessIndex.indexGraphEntity != null) {
				GraphEntity indexGraphEntity = entity.storageAccessIndex.indexGraphEntity;
				sb.append("new GRGEN_LGSP.StorageAccessIndex("
						+ formatEntity(indexGraphEntity, pathPrefixForElements, alreadyDefinedEntityToName) + "), ");
			}
		} else {
			sb.append("null, ");
		}
	}

	private void genIndexAccess(SourceBuilder sb, String pathPrefix,
			String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, GraphEntity entity, List<Entity> parameters)
	{
		if(entity.indexAccess != null) {
			if(entity.indexAccess instanceof IndexAccessEquality) {
				IndexAccessEquality indexAccess = (IndexAccessEquality)entity.indexAccess;
				NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
				indexAccess.expr.collectNeededEntities(needs);
				Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
				sb.append("new GRGEN_LGSP.IndexAccessEquality(");
				sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\""
						+ indexAccess.index.getIdent() + "\"), ");
				sb.append(neededEntity != null
								? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", "
								: "null, ");
				sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
				eyGen.genExpressionTree(sb, indexAccess.expr, className, pathPrefixForElements,
						alreadyDefinedEntityToName);
				sb.append("), ");
			} else if(entity.indexAccess instanceof IndexAccessOrdering) {
				IndexAccessOrdering indexAccess = (IndexAccessOrdering)entity.indexAccess;
				NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
				if(indexAccess.from() != null)
					indexAccess.from().collectNeededEntities(needs);
				if(indexAccess.to() != null)
					indexAccess.to().collectNeededEntities(needs);
				Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
				if(indexAccess.ascending) {
					sb.append("new GRGEN_LGSP.IndexAccessAscending(");
				} else {
					sb.append("new GRGEN_LGSP.IndexAccessDescending(");
				}
				sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\""
						+ indexAccess.index.getIdent() + "\"), ");
				sb.append(neededEntity != null
								? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", "
								: "null, ");
				sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
				if(indexAccess.from() != null) {
					eyGen.genExpressionTree(sb, indexAccess.from(), className, pathPrefixForElements,
							alreadyDefinedEntityToName);
				} else
					sb.append("null");
				sb.append(", " + (indexAccess.includingFrom() ? "true, " : "false, "));
				if(indexAccess.to() != null) {
					eyGen.genExpressionTree(sb, indexAccess.to(), className, pathPrefixForElements,
							alreadyDefinedEntityToName);
				} else
					sb.append("null");
				sb.append(", " + (indexAccess.includingTo() ? "true" : "false"));
				sb.append("), ");
			}
		} else {
			sb.append("null, ");
		}
	}

	private void genNameLookup(SourceBuilder sb, String pathPrefix,
			String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, GraphEntity entity, List<Entity> parameters)
	{
		if(entity.nameMapAccess != null) {
			NameLookup nameMapAccess = entity.nameMapAccess;
			NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
			nameMapAccess.expr.collectNeededEntities(needs);
			Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
			sb.append("new GRGEN_LGSP.NameLookup(");
			sb.append(neededEntity != null
					? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", "
					: "null, ");
			sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
			eyGen.genExpressionTree(sb, nameMapAccess.expr, className, pathPrefixForElements,
					alreadyDefinedEntityToName);
			sb.append("), ");
		} else {
			sb.append("null, ");
		}
	}

	private void genUniqueLookup(SourceBuilder sb, String pathPrefix,
			String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, GraphEntity entity, List<Entity> parameters)
	{
		if(entity.uniqueIndexAccess != null) {
			UniqueLookup uniqueIndexAccess = entity.uniqueIndexAccess;
			NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
			uniqueIndexAccess.expr.collectNeededEntities(needs);
			Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
			sb.append("new GRGEN_LGSP.UniqueLookup(");
			sb.append(neededEntity != null
					? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", "
					: "null, ");
			sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
			eyGen.genExpressionTree(sb, uniqueIndexAccess.expr, className, pathPrefixForElements,
					alreadyDefinedEntityToName);
			sb.append("), ");
		} else {
			sb.append("null, ");
		}
	}

	private void genRuleParam(SourceBuilder sb, MatchingAction action, String packageName)
	{
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity ent : action.getParameters()) {
			if(ent instanceof Variable) {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(ent) + ")), ");
			} else {
				GraphEntity gent = (GraphEntity)ent;
				sb.append(formatTypeClassRef(
						gent.getParameterInterfaceType() != null ? gent.getParameterInterfaceType() : gent.getType())
						+ ".typeVar, ");
			}
		}
		sb.append("},\n");

		sb.appendFront("new string[] { ");
		for(Entity ent : action.getParameters())
			sb.append("\"" + formatEntity(ent, action.getPattern().getNameOfGraph() + "_") + "\", ");
		sb.append("},\n");

		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity ent : action.getDefParameters()) {
			if(ent instanceof Variable) {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(ent) + ")), ");
			} else {
				GraphEntity gent = (GraphEntity)ent;
				sb.append(formatTypeClassRef(
						gent.getParameterInterfaceType() != null ? gent.getParameterInterfaceType() : gent.getType())
						+ ".typeVar, ");
			}
		}
		sb.append("},\n");

		sb.appendFront("new string[] { ");
		for(Entity ent : action.getDefParameters())
			sb.append("\"" + formatIdentifiable(ent) + "\", ");
		sb.append("}");
	}

	private void genRuleResult(SourceBuilder sb, Rule rule, String packageName)
	{
		sb.append(",\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Expression expr : rule.getReturns()) {
			if(expr instanceof GraphEntityExpression)
				sb.append(formatTypeClassRef(expr.getType()) + ".typeVar, ");
			else
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(expr.getType()) + ")), ");
		}
		sb.append("},\n");
	}

	private void genRuleFilter(SourceBuilder sb, Rule rule, String packageName)
	{
		sb.appendFront("new GRGEN_LGSP.LGSPFilter[] {\n");
		sb.indent();

		genFilterAutoSupplied(sb, "keepFirst", packageName, "int");
		genFilterAutoSupplied(sb, "keepLast", packageName, "int");
		genFilterAutoSupplied(sb, "keepFirstFraction", packageName, "double");
		genFilterAutoSupplied(sb, "keepLastFraction", packageName, "double");
		genFilterAutoSupplied(sb, "removeFirst", packageName, "int");
		genFilterAutoSupplied(sb, "removeLast", packageName, "int");
		genFilterAutoSupplied(sb, "removeFirstFraction", packageName, "double");
		genFilterAutoSupplied(sb, "removeLastFraction", packageName, "double");

		for(Filter filter : rule.getFilters()) {
			if(filter instanceof FilterAutoGenerated) {
				genFilterAutoGenerated(sb, (FilterAutoGenerated)filter, packageName);
			} else {
				genFilterFunction(sb, (FilterFunction)filter, packageName);
			}
		}

		sb.unindent();
		sb.appendFront("},\n");
	}

	private void genRuleMatchClassInfo(SourceBuilder sb, Rule rule, String packageName)
	{
		sb.appendFront("new GRGEN_LIBGR.MatchClassInfo[] { ");
		for(DefinedMatchType implementedMatchClass : rule.getImplementedMatchClasses()) {
			sb.append(getPackagePrefixDot(implementedMatchClass) + "MatchClassInfo_"
					+ implementedMatchClass.getIdent().toString() + ".Instance");
			sb.append(", ");
		}
		sb.append("}");
	}

	private void genFilterAutoSupplied(SourceBuilder sb, String filterName, String packageName, String parameterType)
	{
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoSupplied(\"" + filterName + "\", ");
		sb.append("null, ");
		sb.append("\"" + filterName + "\", ");
		sb.append(packageName != null ? "\"" + packageName + "\", " : "null, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] {");
		sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + parameterType + ")), ");
		sb.append("}, ");
		sb.append("new String[] {");
		sb.append("\"param\"");
		sb.append("}");
		sb.append("),\n");
	}

	private void genFilterAutoGenerated(SourceBuilder sb, FilterAutoGenerated fag, String packageName)
	{
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoGenerated(\"" + fag.getFilterName() + fag.getSuffix() + "\", ");
		sb.append("null, ");
		sb.append("\"" + fag.getFilterName() + fag.getSuffix() + "\", ");
		sb.append(packageName != null ? "\"" + packageName + "\", " : "null, ");
		sb.append("\"" + fag.getFilterName() + "\", ");
		sb.append("new String[] { ");
		if(fag.getFilterEntities() != null) {
			boolean first = true;
			for(String filterEntity : fag.getFilterEntities()) {
				if(first)
					first = false;
				else
					sb.append(", ");
				sb.append("\"" + filterEntity + "\"");
			}
		}
		sb.append("}, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] { ");
		if(fag.getFilterEntityTypes() != null) {
			boolean first = true;
			for(Type filterEntityType : fag.getFilterEntityTypes()) {
				if(first)
					first = false;
				else
					sb.append(", ");
				if(filterEntityType instanceof InheritanceType) {
					sb.append(formatTypeClassRef(filterEntityType) + ".typeVar");
				} else {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(filterEntityType) + "))");
				}
			}
		}
		sb.append("}");
		sb.append("),\n");
	}

	private void genFilterFunction(SourceBuilder sb, FilterFunction ff, String packageName)
	{
		String packageNameOfFilterFunction = ff.getPackageContainedIn();
		sb.appendFront("new GRGEN_LGSP.LGSPFilterFunction(\"" + ff.getFilterName() + "\", ");
		sb.append(packageNameOfFilterFunction != null ? "\"" + packageNameOfFilterFunction + "\", " : "null, ");
		sb.append("\"" + (packageNameOfFilterFunction != null
				? packageNameOfFilterFunction + "::" + ff.getFilterName()
				: ff.getFilterName()) + "\", ");
		sb.append((ff instanceof FilterFunctionExternal ? "true" : "false") + ", ");
		sb.append(packageNameOfFilterFunction != null ? "\"" + packageNameOfFilterFunction + "\", " : "null, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] {");
		for(Type paramType : ff.getParameterTypes()) {
			if(paramType instanceof InheritanceType) {
				sb.append(formatTypeClassRef(paramType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(paramType) + ")), ");
			}
		}
		sb.append("}, ");
		sb.append("new String[] {");
		for(Entity entity : ff.getParameters()) {
			sb.append("\"" + entity.getIdent().toString() + "\"");
			sb.append(", ");
		}
		sb.append("}");
		sb.append("),\n ");
	}

	private void genMatchClassFilterAutoSupplied(SourceBuilder sb, String filterName, String packageName,
			String parameterType)
	{
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoSupplied(\"" + filterName + "\", ");
		sb.append("null, ");
		sb.append("\"" + filterName + "\", ");
		sb.append(packageName != null ? "\"" + packageName + "\", " : "null, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] {");
		sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + parameterType + ")), ");
		sb.append("}, ");
		sb.append("new String[] {");
		sb.append("\"param\"");
		sb.append("}");
		sb.append("),\n");
	}

	private void genMatchClassFilterAutoGenerated(SourceBuilder sb, MatchClassFilterAutoGenerated mfag,
			String packageName)
	{
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoGenerated(\"" + mfag.getFilterName() + mfag.getSuffix() + "\", ");
		sb.append("null, ");
		sb.append("\"" + mfag.getFilterName() + mfag.getSuffix() + "\", ");
		sb.append(packageName != null ? "\"" + packageName + "\", " : "null, ");
		sb.append("\"" + mfag.getFilterName() + "\", ");
		sb.append("new String[] { ");
		if(mfag.getFilterEntities() != null) {
			boolean first = true;
			for(String filterEntity : mfag.getFilterEntities()) {
				if(first)
					first = false;
				else
					sb.append(", ");
				sb.append("\"" + filterEntity + "\"");
			}
		}
		sb.append("}, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] { ");
		if(mfag.getFilterEntityTypes() != null) {
			boolean first = true;
			for(Type filterEntityType : mfag.getFilterEntityTypes()) {
				if(first)
					first = false;
				else
					sb.append(", ");
				if(filterEntityType instanceof InheritanceType) {
					sb.append(formatTypeClassRef(filterEntityType) + ".typeVar");
				} else {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(filterEntityType) + "))");
				}
			}
		}
		sb.append("}");
		sb.append("),\n");
	}

	private void genMatchClassFilterFunction(SourceBuilder sb, MatchClassFilterFunction mff, String packageName)
	{
		String packageNameOfFilterFunction = mff.getPackageContainedIn();
		sb.appendFront("new GRGEN_LGSP.LGSPFilterFunction(\"" + mff.getFilterName() + "\", ");
		sb.append(packageNameOfFilterFunction != null ? "\"" + packageNameOfFilterFunction + "\", " : "null, ");
		sb.append("\"" + (packageNameOfFilterFunction != null
				? packageNameOfFilterFunction + "::" + mff.getFilterName()
				: mff.getFilterName()) + "\", ");
		sb.append((mff instanceof MatchClassFilterFunctionExternal ? "true" : "false") + ", ");
		sb.append(packageName != null ? "\"" + packageName + "\", " : "null, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] {");
		for(Type paramType : mff.getParameterTypes()) {
			if(paramType instanceof InheritanceType) {
				sb.append(formatTypeClassRef(paramType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(paramType) + ")), ");
			}
		}
		sb.append("}, ");
		sb.append("new String[] {");
		for(Entity entity : mff.getParameters()) {
			sb.append("\"" + entity.getIdent().toString() + "\"");
			sb.append(", ");
		}
		sb.append("}");
		sb.append("),\n");
	}

	///////////////////////////////////////
	// Static searchplan cost generation //
	///////////////////////////////////////

	private double computePriosMax(double max, PatternGraph pattern)
	{
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
		for(Rule iter : pattern.getIters()) {
			PatternGraph iterPattern = iter.getLeft();
			max = computePriosMax(max, iterPattern);
		}
		return max;
	}

	private double computePriosMax(Collection<? extends Entity> nodesOrEdges, double max)
	{
		for(Entity noe : nodesOrEdges) {
			Object prioO = noe.getAnnotations().get("prio");

			if(prioO != null && prioO instanceof Integer) {
				double val = ((Integer)prioO).doubleValue();
				assert val > 0 : "value of prio attribute of decl is out of range.";

				if(max < val)
					max = val;
			}
		}
		return max;
	}

	private void appendPrio(SourceBuilder sb, Entity entity, double max)
	{
		Object prioO = entity.getAnnotations().get("prio");

		double prio;
		if(prioO != null && prioO instanceof Integer) {
			prio = ((Integer)prioO).doubleValue();
			prio = 10.0 - (prio / max) * 9.0;
		} else {
			prio = 5.5;
		}

		sb.append(prio + "F, ");
	}

	//////////////////////
	// Expression stuff //
	//////////////////////

	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState)
	{
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		genQualAccess(sb, owner, member);
	}

	protected void genQualAccess(SourceBuilder sb, Entity owner, Entity member)
	{
		sb.append("((I" + getNodeOrEdgeTypePrefix(owner) +
				formatIdentifiable(owner.getType()) + ") ");
		sb.append(formatEntity(owner) + ").@" + formatIdentifiable(member));
	}

	protected void genMemberAccess(SourceBuilder sb, Entity member)
	{
		throw new UnsupportedOperationException("Member expressions not allowed in actions!");
	}

	/////////////////////////////////////////////
	// Static constructor calling static inits //
	/////////////////////////////////////////////

	protected void genStaticConstructor(SourceBuilder sb, String className, List<String> staticInitializers)
	{
		sb.append("\n");
		sb.appendFront("static " + className + "() {\n");
		sb.indent();
		for(String staticInit : staticInitializers) {
			sb.appendFront(staticInit + "();\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private SearchPlanBackend2 be;
	private ModifyGen mg;
	private ActionsExpressionOrYieldingGen eyGen;
	private Model model;
}
