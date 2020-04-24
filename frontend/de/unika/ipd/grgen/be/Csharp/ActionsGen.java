/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
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
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.util.SourceBuilder;
import de.unika.ipd.grgen.ir.containers.*;

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

	enum MemberBearerType
	{
		Action, Subpattern, MatchClass
	}

	public ActionsGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix) {
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
		model = be.unit.getActionsGraphModel();
		mg = new ModifyGen(backend, nodeTypePrefix, edgeTypePrefix);
		mgFuncComp = new ModifyGen(backend, nodeTypePrefix, edgeTypePrefix);
	}

	/**
	 * Generates the subpatterns, actions, sequences, functions, procedures, match classes, filters sourcecode for this unit.
	 */
	public void genActionlike() {
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

		sb.appendFront("public class " + be.unit.getUnitName() + "_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("public " + be.unit.getUnitName() + "_RuleAndMatchingPatterns()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("subpatterns = new GRGEN_LGSP.LGSPMatchingPattern["+bearer.getSubpatternRules().size()+"];\n");
		sb.appendFront("rules = new GRGEN_LGSP.LGSPRulePattern["+bearer.getActionRules().size()+"];\n");
		sb.appendFront("rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern["+
				bearer.getSubpatternRules().size()+"+"+bearer.getActionRules().size()+"];\n");
		sb.appendFront("definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo["+bearer.getSequences().size()+"];\n");
		sb.appendFront("functions = new GRGEN_LIBGR.FunctionInfo["+bearer.getFunctions().size()+"+"+model.getExternalFunctions().size()+"];\n");
		sb.appendFront("procedures = new GRGEN_LIBGR.ProcedureInfo["+bearer.getProcedures().size()+"+"+model.getExternalProcedures().size()+"];\n");
		sb.appendFront("matchClasses = new GRGEN_LIBGR.MatchClassInfo["+bearer.getMatchClasses().size()+"];\n");
		sb.appendFront("packages = new string["+be.unit.getPackages().size()+"];\n");
		int i = 0;
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			sb.appendFront("subpatterns["+i+"] = " + getPackagePrefixDot(subpatternRule) + "Pattern_"+formatIdentifiable(subpatternRule)+".Instance;\n");
			sb.appendFront("rulesAndSubpatterns["+i+"] = " + getPackagePrefixDot(subpatternRule) + "Pattern_"+formatIdentifiable(subpatternRule)+".Instance;\n");
			++i;
		}
		int j = 0;
		for(Rule actionRule : bearer.getActionRules()) {
			sb.appendFront("rules["+j+"] = " + getPackagePrefixDot(actionRule) + "Rule_"+formatIdentifiable(actionRule)+".Instance;\n");
			sb.appendFront("rulesAndSubpatterns["+i+"+"+j+"] = " + getPackagePrefixDot(actionRule) + "Rule_"+formatIdentifiable(actionRule)+".Instance;\n");
			++j;
		}
		i = 0;
		for(Sequence sequence : bearer.getSequences()) {
			sb.appendFront("definedSequences["+i+"] = " + getPackagePrefixDot(sequence) + "SequenceInfo_"+formatIdentifiable(sequence)+".Instance;\n");
			++i;
		}
		i = 0;
		for(Function function : bearer.getFunctions()) {
			sb.appendFront("functions["+i+"] = " + getPackagePrefixDot(function) + "FunctionInfo_"+formatIdentifiable(function)+".Instance;\n");
			++i;
		}
		for(ExternalFunction function : model.getExternalFunctions()) {
			sb.appendFront("functions["+i+"] = " + "FunctionInfo_"+formatIdentifiable(function)+".Instance;\n");
			++i;
		}
		i = 0;
		for(Procedure procedure : bearer.getProcedures()) {
			sb.appendFront("procedures["+i+"] = " + getPackagePrefixDot(procedure) + "ProcedureInfo_"+formatIdentifiable(procedure)+".Instance;\n");
			++i;
		}
		for(ExternalProcedure procedure : model.getExternalProcedures()) {
			sb.appendFront("procedures["+i+"] = " + "ProcedureInfo_"+formatIdentifiable(procedure)+".Instance;\n");
			++i;
		}
		i = 0;
		for(DefinedMatchType matchClass : bearer.getMatchClasses()) {
			sb.appendFront("matchClasses["+i+"] = " + getPackagePrefixDot(matchClass) + "MatchClassInfo_"+formatIdentifiable(matchClass)+".Instance;\n");
			++i;
		}
		i = 0;
		for(PackageActionType pack : be.unit.getPackages()) {
			sb.appendFront("packages["+i+"] = \"" + pack.getIdent() +"\";\n");
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

	private void genBearer(SourceBuilder sb, ActionsBearer bearer, String packageName) {
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

	private void genExternalFunctionInfos(SourceBuilder sb) {
		for(ExternalFunction ef : model.getExternalFunctions()) {
			genExternalFunctionInfo(sb, ef);
		}
	}

	private void genExternalProcedureInfos(SourceBuilder sb) {
		for(ExternalProcedure ep : model.getExternalProcedures()) {
			genExternalProcedureInfo(sb, ep);
		}
	}

	private void genExternalFunctionInfo(SourceBuilder sb, ExternalFunction function) {
		String functionName = formatIdentifiable(function);
		String className = "FunctionInfo_"+functionName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
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
		sb.append("return GRGEN_EXPR.ExternalFunctions." + functionName + "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
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

	private void genExternalProcedureInfo(SourceBuilder sb, ExternalProcedure procedure) {
		String procedureName = formatIdentifiable(procedure);
		String className = "ProcedureInfo_"+procedureName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
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

		sb.append("GRGEN_EXPR.ExternalProcedures." + procedureName + "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
		i = 0;
		for(Type inType : procedure.getParameterTypes()) {
			sb.append(", (" + formatType(inType) + ")arguments[" + i + "]");
			++i;
		}
		for(i=0; i<procedure.getReturnTypes().size(); ++i) {
			sb.append(", out ");
			sb.append("_out_param_" + i);
		}
		sb.append(");\n");

		for(i=0; i<procedure.getReturnTypes().size(); ++i) {
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
	private void genSubpattern(SourceBuilder sb, Rule subpatternRule, String packageName) {
		String actionName = formatIdentifiable(subpatternRule);
		String className = "Pattern_"+actionName;
		List<String> staticInitializers = new LinkedList<String>();

		sb.appendFront("public class " + className + " : GRGEN_LGSP.LGSPMatchingPattern\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");

		String patGraphVarName = "pat_" + subpatternRule.getPattern().getNameOfGraph();
		genRuleOrSubpatternClassEntities(sb, subpatternRule, patGraphVarName, staticInitializers,
				subpatternRule.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, subpatternRule, className, packageName, true);
		sb.append("\n");

		mg.genModify(sb, subpatternRule, packageName, true);

		genImperativeStatements(sb, subpatternRule, formatIdentifiable(subpatternRule) + "_", subpatternRule.getPackageContainedIn(), true, true);
		genImperativeStatementClosures(sb, subpatternRule, formatIdentifiable(subpatternRule) + "_", false);

		genStaticConstructor(sb, className, staticInitializers);

		genMatch(sb, subpatternRule.getPattern(), null, className, false);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		for(Rule iteratedRule : subpatternRule.getLeft().getIters())
		{
			genArraySortBy(sb, subpatternRule, MemberBearerType.Subpattern, iteratedRule);
		}
		sb.append("\n");
	}

	/**
	 * Generates the action representation sourcecode for the given matching-action
	 */
	private void genAction(SourceBuilder sb, Rule actionRule, String packageName) {
		String actionName = formatIdentifiable(actionRule);
		String className = "Rule_"+actionName;
		List<String> staticInitializers = new LinkedList<String>();

		sb.appendFront("public class " + className + " : GRGEN_LGSP.LGSPRulePattern\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); instance.initialize(); } return instance; } }\n");
		sb.append("\n");

		String patGraphVarName = "pat_" + actionRule.getPattern().getNameOfGraph();
		genRuleOrSubpatternClassEntities(sb, actionRule, patGraphVarName, staticInitializers,
				actionRule.getPattern().getNameOfGraph()+"_", new HashMap<Entity, String>());
		sb.append("\n");
		genRuleOrSubpatternInit(sb, actionRule, className, packageName, false);
		sb.append("\n");

		mg.genModify(sb, actionRule, packageName, false);

		genImperativeStatements(sb, actionRule, formatIdentifiable(actionRule) + "_", actionRule.getPackageContainedIn(), true, false);
		genImperativeStatementClosures(sb, actionRule, formatIdentifiable(actionRule) + "_", true);

		genStaticConstructor(sb, className, staticInitializers);

		genMatch(sb, actionRule.getPattern(), actionRule.getImplementedMatchClasses(), className, actionRule.getAnnotations().containsKey("parallelize"));

		sb.append("\n");
		genExtractor(sb, actionRule, null);
		for(Rule iteratedRule : actionRule.getLeft().getIters())
		{
			genExtractor(sb, actionRule, iteratedRule);
		}

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		genArraySortBy(sb, actionRule, MemberBearerType.Action, null);
		for(Rule iteratedRule : actionRule.getLeft().getIters())
		{
			genArraySortBy(sb, actionRule, MemberBearerType.Action, iteratedRule);
		}
		sb.append("\n");
	}

	/**
	 * Generates the sequence representation sourcecode for the given sequence
	 */
	private void genSequence(SourceBuilder sb, Sequence sequence, String packageName) {
		String sequenceName = formatIdentifiable(sequence);
		String className = "SequenceInfo_"+sequenceName;
		boolean isExternalSequence = sequence.getExec().getXGRSString().length()==0;
		String baseClass = isExternalSequence ? "GRGEN_LIBGR.ExternalDefinedSequenceInfo" : "GRGEN_LIBGR.DefinedSequenceInfo";

		sb.appendFront("public class " + className + " : " + baseClass + "\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
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
			sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
			sb.append("\"" + (packageName!=null ? packageName + "::" + sequenceName : sequenceName) + "\",\n");
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

	private void genFunctions(SourceBuilder sb, ActionsBearer bearer, String packageName) {
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
			boolean isToBeParallelizedActionExisting, boolean emitProfilingInstrumentation) {
		sb.appendFront("public static " + formatType(function.getReturnType()) + " ");
		sb.append(function.getIdent().toString() + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPGraph graph");
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
		ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, null, "", isToBeParallelizedActionExisting, emitProfilingInstrumentation);
		for(EvalStatement evalStmt : function.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = function.getIdent().toString();
			mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
		}
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the function info for the given function
	 */
	private void genFunctionInfo(SourceBuilder sb, Function function, String packageName) {
		String functionName = formatIdentifiable(function);
		String className = "FunctionInfo_"+functionName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + functionName + "\",\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + functionName : functionName) + "\",\n");
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
		sb.appendFront("return GRGEN_ACTIONS." + getPackagePrefixDot(function) + "Functions." + functionName + "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
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
	private void genProcedures(SourceBuilder sb, ActionsBearer bearer, String packageName) {
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

		sb.append("#if INITIAL_WARMUP\t\t// GrGen procedure exec section: "
			+ (packageName!=null ? packageName + "::" + "Procedures\n" : "Procedures\n"));
		for(Procedure procedure : bearer.getProcedures()) {
			genImperativeStatements(sb, procedure);
		}
		sb.append("#endif\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		
		for(Procedure procedure : bearer.getProcedures()) {
			genProcedureInfo(sb, procedure, packageName);
		}
	}

	private void genProcedure(SourceBuilder sb, Procedure procedure, boolean emitProfilingInstrumentation) {
		sb.appendFront("public static void ");
		sb.append(procedure.getIdent().toString() + "(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPGraph graph");
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
		ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, null, "", false, emitProfilingInstrumentation);
		mgFuncComp.initEvalGen();

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
			mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
		}
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the procedure info for the given procedure
	 */
	private void genProcedureInfo(SourceBuilder sb, Procedure procedure, String packageName) {
		String procedureName = formatIdentifiable(procedure);
		String className = "ProcedureInfo_"+procedureName;

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + procedureName + "\",\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + procedureName : procedureName) + "\",\n");
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

		sb.appendFront("GRGEN_ACTIONS." + getPackagePrefixDot(procedure) + "Procedures." + procedureName + "((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph");
		i = 0;
		for(Entity inParam : procedure.getParameters()) {
			sb.append(", (" + formatType(inParam.getType()) + ")arguments[" + i + "]");
			++i;
		}
		for(i=0; i<procedure.getReturnTypes().size(); ++i) {
			sb.append(", out ");
			sb.append("_out_param_" + i);
		}
		sb.append(");\n");

		for(i=0; i<procedure.getReturnTypes().size(); ++i) {
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
	private void genFilterFunctions(SourceBuilder sb, ActionsBearer bearer, String packageName) {
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

	private void genFilterFunction(SourceBuilder sb, FilterFunctionInternal filter, String packageName, boolean emitProfilingInstrumentation) {
		String actionName = filter.getAction().getIdent().toString();
		String packagePrefixOfAction = "GRGEN_ACTIONS." + getPackagePrefixDot(filter.getAction());
		String matchType = packagePrefixOfAction + "Rule_" + actionName + ".IMatch_" + actionName;
		sb.appendFront("public static void ");
		sb.append("Filter_" + filter.getIdent().toString() + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_LIBGR.IMatchesExact<" + matchType + "> matches");
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
		ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, null, "", false, emitProfilingInstrumentation);
		EvalStatement lastEvalStmt = null;
		for(EvalStatement evalStmt : filter.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = filter.getIdent().toString();
			mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
			lastEvalStmt = evalStmt;
		}
		if(!(lastEvalStmt instanceof ReturnStatementFilter)) {
			// ensure that FromList is called if the user omitted return
			mgFuncComp.genEvalStmt(sb, modifyGenState, new ReturnStatementFilter());
		}
		
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the function representation sourcecode for the given match filter function
	 */
	private void genMatchClassFilterFunctions(SourceBuilder sb, ActionsBearer bearer, String packageName) {
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

	private void genMatchClassFilterFunction(SourceBuilder sb, MatchClassFilterFunctionInternal matchClassFilter, boolean emitProfilingInstrumentation) {
		String packagePrefix = getPackagePrefixDot(matchClassFilter.getMatchClass());
		String matchClassName = formatIdentifiable(matchClassFilter.getMatchClass());
		sb.appendFront("public static void ");
		sb.append("Filter_" + matchClassFilter.getIdent().toString() + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, IList<GRGEN_LIBGR.IMatch> matches");
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
		ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, matchClassName, packagePrefix, false, emitProfilingInstrumentation);
		EvalStatement lastEvalStmt = null;
		for(EvalStatement evalStmt : matchClassFilter.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = matchClassFilter.getIdent().toString();
			mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
			lastEvalStmt = evalStmt;
		}
		if(!(lastEvalStmt instanceof ReturnStatementFilter)) {
			// ensure that FromList is called if the user omitted return
			mgFuncComp.genEvalStmt(sb, modifyGenState, new ReturnStatementFilter());
		}
		sb.unindent();
		sb.append("}\n");
	}

	/**
	 * Generates the match classes (of match classes)
	 */
	private void genMatchClasses(SourceBuilder sb, ActionsBearer bearer, String packageName) {
		for(DefinedMatchType matchClass : bearer.getMatchClasses()) {
			genMatchClass(sb, matchClass, packageName);
		}
		
		sb.append("\n");
	}

	private void genMatchClass(SourceBuilder sb, DefinedMatchType matchClass, String packageName) {
		// generate getters to contained nodes, edges, variables
		HashSet<String> elementsAlreadyDeclared = new HashSet<String>();
		genPatternMatchInterface(sb, matchClass.getPatternGraph(), matchClass.getPatternGraph().getNameOfGraph(),
				"GRGEN_LIBGR.IMatch", matchClass.getPatternGraph().getNameOfGraph()+"_",
				false, false, true, elementsAlreadyDeclared);

		sb.append("\n");

		genMatchClassInfo(sb, matchClass, packageName);
		
		sb.append("\n");

		genArraySortBy(sb, matchClass);
		sb.append("\n");
	}

	private void genMatchClassInfo(SourceBuilder sb, DefinedMatchType matchClass, String packageName) {
		String matchClassName = formatIdentifiable(matchClass);
		String className = "MatchClassInfo_"+matchClassName;
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
		genElementsRequiredByPatternGraph(sbElements, aux, matchClass.getPatternGraph(), pathPrefix, matchClassName, packageName, patGraphVarName, className,
				alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, parameters, max, true);

		alreadyDefinedEntityToName = new HashMap<Entity, String>();
		genAllowedTypeArrays(sbElements, matchClass.getPatternGraph(), pathPrefixForElements, alreadyDefinedEntityToName);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.MatchClassInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + matchClassName + "\",\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + matchClassName : matchClassName) + "\",\n");
		sb.appendFront("new GRGEN_LIBGR.IPatternNode[] ");
		genEntitySet(sb, matchClass.getPatternGraph().getNodes(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LIBGR.IPatternEdge[] ");
		genEntitySet(sb, matchClass.getPatternGraph().getEdges(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
		sb.append(",\n");
		sb.appendFront("new GRGEN_LIBGR.IPatternVariable[] ");
		genEntitySet(sb, matchClass.getPatternGraph().getVars(), "", "", true, pathPrefixForElements, alreadyDefinedEntityToName);
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
	private void genMatch(SourceBuilder sb, PatternGraph pattern, Collection<DefinedMatchType> implementedMatchClasses, String className, boolean parallelized) {
		HashSet<String> elementsAlreadyDeclared = new HashSet<String>();
		String base = "";
		if(implementedMatchClasses==null || implementedMatchClasses.isEmpty()) {
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
				String packagePrefix = implementedMatchClass.getPackageContainedIn() != null ? implementedMatchClass.getPackageContainedIn() + "." : "";
				base += packagePrefix + "IMatch_" + implementedMatchClass.getName();
			}
		}

		// generate getters to contained nodes, edges, variables, embedded graphs, alternatives
		genPatternMatchInterface(sb, pattern, pattern.getNameOfGraph(),
				base, pattern.getNameOfGraph()+"_",
				false, false, false, elementsAlreadyDeclared);

		// generate contained nodes, edges, variables, embedded graphs, alternatives
		// and the implementation of the various getters from IMatch and the pattern specific match interface
		String patGraphVarName = "pat_" + pattern.getNameOfGraph();
		genPatternMatchImplementation(sb, pattern, pattern.getNameOfGraph(),
				patGraphVarName, className, pattern.getNameOfGraph()+"_", false, false, parallelized);
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

		PatternGraph pattern = iteratedRule!=null ? iteratedRule.getPattern() : actionRule.getPattern();
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
		sb.appendFront("public static List<" + formatType(entity.getType()) + "> Extract_" + formatIdentifiable(entity) + "(List<" + matchTypeName + "> matchList)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("List<" + formatType(entity.getType()) + "> resultList = new List<" + formatType(entity.getType()) + ">(matchList.Count);\n");
		sb.appendFront("foreach(" + matchTypeName + " match in matchList)\n");
		sb.indent();
		sb.appendFront("resultList.Add(match." + formatEntity(entity) + ");\n");
		sb.unindent();
		sb.appendFront("return resultList;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates the Array_sortAscendingBy_member/Array_sortDescendingBy_member function plus the Comparison helper (shared with the corresponding sortAscendingBy/sortDescendingBy filter)
	 */
	void genArraySortBy(SourceBuilder sb, Rule actionRule, MemberBearerType memberBearerType, Rule iteratedRule)
	{
		sb.appendFront("public partial class MatchFilters\n");
		sb.appendFront("{\n");
		sb.indent();

		Rule rule = iteratedRule != null ? iteratedRule : actionRule;
		for(Variable var : rule.getPattern().getVars())
		{
			if(var.getType().isFilterableType()) {
				generateComparerAndArrayOrderBy(sb, actionRule, memberBearerType, iteratedRule, var, true);
				generateComparerAndArrayOrderBy(sb, actionRule, memberBearerType, iteratedRule, var, false);
				generateArrayKeepOneForEach(sb, actionRule, memberBearerType, iteratedRule, var);
			}
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
		String functionName = ascending ? "orderAscendingBy_" + formatIdentifiable(var) : "orderDescendingBy_" + formatIdentifiable(var);
		String arrayFunctionName = "Array_" + name + iteratedNameComponent + "_" + functionName;
		String comparerName = "Comparer_" + name + iteratedNameComponent + "_" + functionName;

		sb.appendFront("public static List<" + matchInterfaceName + "> " + arrayFunctionName + "(List<" + matchInterfaceName + "> list)\n");
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

		sb.appendFront("public override int Compare(" + matchInterfaceName + " left, " + matchInterfaceName + " right)\n");
		sb.appendFront("{\n");
		sb.indent();
		if(ascending)
			sb.appendFront("return left." + formatEntity(var) + ".CompareTo(right." + formatEntity(var) + ");\n");
		else
			sb.appendFront("return -left." + formatEntity(var) + ".CompareTo(right." + formatEntity(var) + ");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
	}

	void generateArrayKeepOneForEach(SourceBuilder sb, Identifiable memberBearer,
			MemberBearerType memberBearerType, Rule iteratedRule, Variable var)
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
		String functionName = "keepOneForEachBy_" + formatIdentifiable(var);
		String arrayFunctionName = "Array_" + name + iteratedNameComponent + "_" + functionName;

		generateArrayKeepOneForEach(sb, arrayFunctionName, matchInterfaceName, formatEntity(var), formatType(var.getType()));
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

		for(Variable var : matchClass.getVars())
		{
			if(var.getType().isFilterableType()) {
				generateComparerAndArrayOrderBy(sb, matchClass, MemberBearerType.MatchClass, null, var, true);
				generateComparerAndArrayOrderBy(sb, matchClass, MemberBearerType.MatchClass, null, var, false);
				generateArrayKeepOneForEach(sb, matchClass, MemberBearerType.MatchClass, null, var);
			}
		}

		sb.unindent();
		sb.appendFront("}\n");
	}

	//////////////////////////////////////////////////
	// rule or subpattern class entities generation //
	//////////////////////////////////////////////////

	private void genRuleOrSubpatternClassEntities(SourceBuilder sb, Rule rule,
							String patGraphVarName, List<String> staticInitializers,
							String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		PatternGraph pattern = rule.getPattern();
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);
		genLocalContainers(sb, rule, staticInitializers, pathPrefixForElements,
				alreadyDefinedEntityToName);
		sb.append("\t\tpublic GRGEN_LGSP.PatternGraph " + patGraphVarName + ";\n");
		sb.append("\n");

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, neg, pathPrefixForElements+negName, staticInitializers,
					pathPrefixForElements + negName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, idpt, pathPrefixForElements+idptName, staticInitializers,
					pathPrefixForElements + idptName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			genCaseEnum(sb, alt, pathPrefixForElements+altName+"_");
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
				genRuleOrSubpatternClassEntities(sb, altCase, altPatGraphVarName, staticInitializers,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						alreadyDefinedEntityToNameClone);
			}
		}

		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, iter, pathPrefixForElements+iterName, staticInitializers,
					pathPrefixForElements + iterName + "_",
					alreadyDefinedEntityToNameClone);
		}
	}

	private void genRuleOrSubpatternClassEntities(SourceBuilder sb, PatternGraph pattern,
							String patGraphVarName, List<String> staticInitializers,
							String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		genAllowedTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genEnums(sb, pattern, pathPrefixForElements);
		genLocalContainers(sb, pattern, staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		sb.appendFront("public GRGEN_LGSP.PatternGraph " + patGraphVarName + ";\n");
		sb.append("\n");

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, neg, pathPrefixForElements+negName, staticInitializers,
					pathPrefixForElements + negName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, idpt, pathPrefixForElements+idptName, staticInitializers,
					pathPrefixForElements + idptName + "_",
					alreadyDefinedEntityToNameClone);
		}

		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			genCaseEnum(sb, alt, pathPrefixForElements+altName+"_");
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				String altPatGraphVarName = pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph();
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
				genRuleOrSubpatternClassEntities(sb, altCase, altPatGraphVarName, staticInitializers,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						alreadyDefinedEntityToNameClone);
				}
		}

		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			genRuleOrSubpatternClassEntities(sb, iter, pathPrefixForElements+iterName, staticInitializers,
					pathPrefixForElements + iterName + "_",
					alreadyDefinedEntityToNameClone);
		}
	}

	private void genAllowedTypeArrays(SourceBuilder sb, PatternGraph pattern,
									  String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		genAllowedNodeTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
		genAllowedEdgeTypeArrays(sb, pattern, pathPrefixForElements, alreadyDefinedEntityToName);
	}

	private void genAllowedNodeTypeArrays(SourceBuilder sb, PatternGraph pattern,
										  String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		SourceBuilder aux = new SourceBuilder();
		aux.indent().indent();
		
		for(Node node : pattern.getNodes()) {
			if(alreadyDefinedEntityToName.get(node)!=null) {
				continue;
			}
			sb.appendFront("public static GRGEN_LIBGR.NodeType[] "
					+ formatEntity(node, pathPrefixForElements) + "_AllowedTypes = ");
			aux.appendFront("public static bool[] " + formatEntity(node, pathPrefixForElements) + "_IsAllowedType = ");
			if( !node.getConstraints().isEmpty() ) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : node.getConstraints())
					for(Type type : model.getAllNodeTypes()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : model.getAllNodeTypes()) {
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
		
		sb.append(aux.toString());
	}

	private void genAllowedEdgeTypeArrays(SourceBuilder sb, PatternGraph pattern,
										  String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		SourceBuilder aux = new SourceBuilder();
		aux.indent().indent();
		
		for(Edge edge : pattern.getEdges()) {
			if(alreadyDefinedEntityToName.get(edge)!=null) {
				continue;
			}
			sb.appendFront("public static GRGEN_LIBGR.EdgeType[] "
					+ formatEntity(edge, pathPrefixForElements) + "_AllowedTypes = ");
			aux.appendFront("public static bool[] " + formatEntity(edge, pathPrefixForElements) + "_IsAllowedType = ");
			if( !edge.getConstraints().isEmpty() ) {
				// alle verbotenen Typen und deren Untertypen
				HashSet<Type> allForbiddenTypes = new HashSet<Type>();
				for(Type forbiddenType : edge.getConstraints())
					for(Type type : model.getAllEdgeTypes()) {
						if (type.isCastableTo(forbiddenType))
							allForbiddenTypes.add(type);
					}
				sb.append("{ ");
				aux.append("{ ");
				for(Type type : model.getAllEdgeTypes()) {
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
		
		sb.append(aux.toString());
	}

	private void genEnums(SourceBuilder sb, PatternGraph pattern, String pathPrefixForElements) {
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

	private void genCaseEnum(SourceBuilder sb, Alternative alt, String pathPrefixForElements) {
		sb.appendFront("public enum " + pathPrefixForElements + "CaseNums { ");
		for(Rule altCase : alt.getAlternativeCases()) {
			PatternGraph altCasePattern = altCase.getLeft();
			sb.append("@" + altCasePattern.getNameOfGraph() + ", ");
		}
		sb.append("};\n");
	}

	private void genLocalContainers(SourceBuilder sb, Rule rule,
			List<String> staticInitializers, String pathPrefixForElements,
			HashMap<Entity, String> alreadyDefinedEntityToName) {
		genLocalContainers(sb, rule.getLeft(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		
		for(EvalStatements evals : rule.getEvals()) {
			genLocalContainersEvals(sb, evals.evalStatements, staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}
		genLocalContainersReturns(sb, rule.getReturns(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		if(rule.getRight()!=null) {
			genLocalContainersInitializations(sb, rule.getRight(), rule.getLeft(), staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
			genLocalContainersImperativeStatements(sb, rule.getRight().getImperativeStmts(), staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}
	}

	private void genLocalContainers(SourceBuilder sb, PatternGraph pattern,
			List<String> staticInitializers, String pathPrefixForElements,
			HashMap<Entity, String> alreadyDefinedEntityToName) {
		genLocalContainersInitializations(sb, pattern, staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		genLocalContainersConditions(sb, pattern, staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);
		for(EvalStatements evals: pattern.getYields()) {
			genLocalContainersEvals(sb, evals.evalStatements, staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);
		}
	}

	private void genLocalContainersInitializations(SourceBuilder sb, PatternGraph rhsPattern, PatternGraph directlyNestingLHSPattern, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Variable var : rhsPattern.getVars()) {
			if(var.initialization!=null) {
				if(var.directlyNestingLHSGraph==directlyNestingLHSPattern 
						&& (var.getContext()&BaseNode.CONTEXT_LHS_OR_RHS)==BaseNode.CONTEXT_RHS) {
					var.initialization.collectNeededEntities(needs);
				}
			}
		}
		genLocalContainers(sb, needs, staticInitializers, false);
	}

	private void genLocalContainersInitializations(SourceBuilder sb, PatternGraph pattern, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Variable var : pattern.getVars()) {
			if(var.initialization!=null) {
				if(var.directlyNestingLHSGraph==pattern) {
					var.initialization.collectNeededEntities(needs);
				}
			}
		}
		genLocalContainers(sb, needs, staticInitializers, false);
	}

	private void genLocalContainersConditions(SourceBuilder sb, PatternGraph pattern, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Expression expr : pattern.getConditions()) {
			expr.collectNeededEntities(needs);
		}
		genLocalContainers(sb, needs, staticInitializers, true);
	}

	// type collision with the method below cause java can't distinguish List<Expression> from List<ImperativeStmt>
	private void genLocalContainersReturns(SourceBuilder sb, List<Expression> returns, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(Expression expr : returns) {
			expr.collectNeededEntities(needs);
		}
		genLocalContainers(sb, needs, staticInitializers, true);
	}
	
	private void genLocalContainersImperativeStatements(SourceBuilder sb, List<ImperativeStmt> istmts, List<String> staticInitializers,
			String pathPrefixForElements, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(ImperativeStmt istmt : istmts) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit) istmt;
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
			String className, String packageName, boolean isSubpattern) {
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
								List<Entity> parameters, double max) {
		genElementsRequiredByPatternGraph(sb, aux, pattern, pathPrefix, patternName, packageName, patGraphVarName, className,
										  alreadyDefinedEntityToName, alreadyDefinedIdentifiableToName, parameters, max, false);

		sb.appendFront(patGraphVarName + " = new GRGEN_LGSP.PatternGraph(\n");
		sb.indent();
		sb.appendFront("\"" + patternName + "\",\n");
		sb.appendFront("\"" + pathPrefix + "\",\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName+"::" : "") + patternName + "\",\n");
		sb.appendFront((pattern.isPatternpathLocked() ? "true" : "false") + ", " );
		sb.append((pattern.isIterationBreaking() ? "true" : "false") + ",\n" );

		String pathPrefixForElements = pathPrefix+patternName+"_";

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
		genSubpatternUsageSet(sb, pattern.getSubpatternUsages(), "", "", true, pathPrefixForElements, alreadyDefinedIdentifiableToName);
		sb.append(", \n");

		sb.appendFront("new GRGEN_LGSP.Alternative[] { ");
		for(Alternative alt : pattern.getAlts()) {
			sb.append(pathPrefixForElements + alt.getNameOfGraph() + ", ");
		}
		sb.append(" }, \n");

		sb.appendFront("new GRGEN_LGSP.Iterated[] { ");
		for(Rule iter : pattern.getIters()) {
			sb.append(pathPrefixForElements + iter.getLeft().getNameOfGraph()+"_it" + ", ");
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
		for(int i = 0; i < pattern.getConditions().size(); i++){
			sb.append(pathPrefixForElements+"cond_" + i + ", ");
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

	private void genNodeHomMatrix(SourceBuilder sb, PatternGraph pattern) {
		if(pattern.getNodes().size() > 0) {
			sb.append("{\n");
			sb.indent();
			for(Node node1 : pattern.getNodes()) {
				sb.appendFront("{ ");
				for(Node node2 : pattern.getNodes()) {
					if(pattern.isHomomorphic(node1,node2))
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

	private void genEdgeHomMatrix(SourceBuilder sb, PatternGraph pattern) {
		if(pattern.getEdges().size() > 0) {
			sb.append("{\n");
			sb.indent();
			for(Edge edge1 : pattern.getEdges()) {
				sb.appendFront("{ ");
				for(Edge edge2 : pattern.getEdges()) {
					if(pattern.isHomomorphic(edge1,edge2))
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
			HashMap<Entity, String> alreadyDefinedEntityToName, String pathPrefixForElements) {
		for(Edge edge : pattern.getEdges()) {
			String edgeName = alreadyDefinedEntityToName.get(edge)!=null ?
					alreadyDefinedEntityToName.get(edge) : formatEntity(edge, pathPrefixForElements);

			if(pattern.getSource(edge)!=null) {
				String sourceName = formatEntity(pattern.getSource(edge), pathPrefixForElements, alreadyDefinedEntityToName);
				sb.appendFront(patGraphVarName + ".edgeToSourceNode.Add("+edgeName+", "+sourceName+");\n");
			}

			if(pattern.getTarget(edge)!=null) {
				String targetName = formatEntity(pattern.getTarget(edge), pathPrefixForElements, alreadyDefinedEntityToName);
				sb.appendFront(patGraphVarName + ".edgeToTargetNode.Add("+edgeName+", "+targetName+");\n");
			}
		}
	}

	private void setEmbeddingGraph(SourceBuilder sb, PatternGraph pattern, String patGraphVarName,
			String pathPrefixForElements) {
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
			sb.appendFront(pathPrefixForElements+iterName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			sb.appendFront(pathPrefixForElements+negName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			sb.appendFront(pathPrefixForElements+idptName + ".embeddingGraph = " + patGraphVarName + ";\n");
		}
	}

	private void genElementsRequiredByPatternGraph(SourceBuilder sb, SourceBuilder aux, PatternGraph pattern,
												   String pathPrefix, String patternName, String packageName,
												   String patGraphVarName, String className,
												   HashMap<Entity, String> alreadyDefinedEntityToName,
												   HashMap<Identifiable, String> alreadyDefinedIdentifiableToName,
												   List<Entity> parameters, double max, boolean isMatchClass) {
		String pathPrefixForElements = pathPrefix+patternName+"_";

		if(!isMatchClass)
		{
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
			if(alreadyDefinedIdentifiableToName.get(sub)!=null) {
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
				HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
				HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
				genPatternGraph(sb, aux, altCasePattern,
								  pathPrefixForElements+altName+"_", altCasePattern.getNameOfGraph(), packageName,
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
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			genPatternGraph(sb, aux, iterPattern,
							  pathPrefixForElements, iterName, packageName,
							  pathPrefixForElements+iterName, className,
							  alreadyDefinedEntityToNameClone,
							  alreadyDefinedIdentifiableToNameClone,
							  parameters, max);
		}

		for(Rule iter : pattern.getIters()) {
			genPatternIterated(sb, packageName, pathPrefixForElements, iter);
		}
		
		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			genPatternGraph(sb, aux, neg,
							  pathPrefixForElements, negName, packageName,
							  pathPrefixForElements+negName, className,
							  alreadyDefinedEntityToNameClone,
							  alreadyDefinedIdentifiableToNameClone,
							  parameters, max);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			HashMap<Entity, String> alreadyDefinedEntityToNameClone = new HashMap<Entity, String>(alreadyDefinedEntityToName);
			HashMap<Identifiable, String> alreadyDefinedIdentifiableToNameClone = new HashMap<Identifiable, String>(alreadyDefinedIdentifiableToName);
			genPatternGraph(sb, aux, idpt,
							  pathPrefixForElements, idptName, packageName,
							  pathPrefixForElements+idptName, className,
							  alreadyDefinedEntityToNameClone,
							  alreadyDefinedIdentifiableToNameClone,
							  parameters, max);
		}
	}

	private void genNodeGlobalHomMatrix(SourceBuilder sb, PatternGraph pattern,
			HashMap<Entity, String> alreadyDefinedEntityToName) {
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
			HashMap<Entity, String> alreadyDefinedEntityToName) {
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

	private void genNodeTotallyHomArray(SourceBuilder sb, PatternGraph pattern) {
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

	private void genEdgeTotallyHomArray(SourceBuilder sb, PatternGraph pattern) {
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
			Variable var, String varName) {
		sb.appendFront((isMatchClass ? "static " : "") + "GRGEN_LGSP.PatternVariable " + varName
				+ " = new GRGEN_LGSP.PatternVariable(");
		sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(var)
				+ ")), \"" + varName + "\", \"" + formatIdentifiable(var) + "\", ");
		sb.append(parameters.indexOf(var)+", ");
		sb.append(var.isDefToBeYieldedTo() ? "true, " : "false, ");
		if(var.initialization!=null) {
			genExpressionTree(sb, var.initialization, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(");\n");
		} else {
			sb.append("null);\n");
		}
		if(isMatchClass)
			aux.appendFront(varName + ".pointOfDefinition = null;\n");
		else
			aux.appendFront(varName + ".pointOfDefinition = " + (parameters.indexOf(var)==-1 ? patGraphVarName : "null") + ";\n");
		addAnnotations(aux, var, varName+".annotations");
		return varName;
	}

	private void genPatternNode(SourceBuilder sb, SourceBuilder aux, PatternGraph pattern, String pathPrefix,
			String patGraphVarName, String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			List<Entity> parameters, double max, boolean isMatchClass, String pathPrefixForElements, 
			Node node, String nodeName) {
		sb.appendFront((isMatchClass ? "static " : "") + "GRGEN_LGSP.PatternNode " + nodeName + " = new GRGEN_LGSP.PatternNode(");
		sb.append("(int) GRGEN_MODEL." + getPackagePrefixDot(node.getType()) + "NodeTypes.@" + formatIdentifiable(node.getType()) 
				+ ", " + formatTypeClassRef(node.getType()) + ".typeVar"
				+ ", \"" + formatElementInterfaceRef(node.getType()) + "\", ");
		sb.append("\"" + nodeName + "\", \"" + formatIdentifiable(node) + "\", ");
		sb.append(nodeName + "_AllowedTypes, ");
		sb.append(nodeName + "_IsAllowedType, ");
		appendPrio(sb, node, max);
		sb.append(parameters.indexOf(node)+", ");
		sb.append(node.getMaybeNull() ? "true, " : "false, ");
		genStorageAccess(sb, pathPrefix, alreadyDefinedEntityToName,
				pathPrefixForElements, node);
		genIndexAccess(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, node, parameters);
		genNameLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, node, parameters);
		genUniqueLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, node, parameters);
		sb.append((node instanceof RetypedNode ? formatEntity(((RetypedNode)node).getOldNode(), pathPrefixForElements, alreadyDefinedEntityToName) : "null")+", ");
		sb.append(node.isDefToBeYieldedTo() ? "true," : "false,");
		if(node.initialization!=null) {
			genExpressionTree(sb, node.initialization, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(");\n");
		} else {
			sb.append("null);\n");
		}
		if(isMatchClass)
			aux.appendFront(nodeName + ".pointOfDefinition = null;\n");
		else
			aux.appendFront(nodeName + ".pointOfDefinition = " + (parameters.indexOf(node)==-1 ? patGraphVarName : "null") + ";\n");
		addAnnotations(aux, node, nodeName+".annotations");

		node.setPointOfDefinition(pattern);
	}

	private void genPatternEdge(SourceBuilder sb, SourceBuilder aux, PatternGraph pattern, String pathPrefix,
			String patGraphVarName, String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			List<Entity> parameters, double max, boolean isMatchClass, String pathPrefixForElements, 
			Edge edge, String edgeName) {
		sb.appendFront((isMatchClass ? "static " : "") + "GRGEN_LGSP.PatternEdge " + edgeName + " = new GRGEN_LGSP.PatternEdge(");
		sb.append((edge.hasFixedDirection() ? "true" : "false") + ", ");
		sb.append("(int) GRGEN_MODEL." + getPackagePrefixDot(edge.getType()) + "EdgeTypes.@" + formatIdentifiable(edge.getType()) 
				+ ", " + formatTypeClassRef(edge.getType()) + ".typeVar"
				+ ", \"" + formatElementInterfaceRef(edge.getType()) + "\", ");
		sb.append("\"" + edgeName + "\", \"" + formatIdentifiable(edge) + "\", ");
		sb.append(edgeName + "_AllowedTypes, ");
		sb.append(edgeName + "_IsAllowedType, ");
		appendPrio(sb, edge, max);
		sb.append(parameters.indexOf(edge)+", ");
		sb.append(edge.getMaybeNull()?"true, ":"false, ");
		genStorageAccess(sb, pathPrefix, alreadyDefinedEntityToName,
				pathPrefixForElements, edge);
		genIndexAccess(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, edge, parameters);
		genNameLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, edge, parameters);
		genUniqueLookup(sb, pathPrefix, className, alreadyDefinedEntityToName,
				pathPrefixForElements, edge, parameters);
		sb.append((edge instanceof RetypedEdge ? formatEntity(((RetypedEdge)edge).getOldEdge(), pathPrefixForElements, alreadyDefinedEntityToName) : "null")+", ");
		sb.append(edge.isDefToBeYieldedTo() ? "true," : "false,");
		if(edge.initialization!=null) {
			genExpressionTree(sb, edge.initialization, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append(");\n");
		} else {
			sb.append("null);\n");
		}
		if(isMatchClass)
			aux.appendFront(edgeName + ".pointOfDefinition = null;\n");
		else
			aux.appendFront(edgeName + ".pointOfDefinition = " + (parameters.indexOf(edge)==-1 ? patGraphVarName : "null") + ";\n");
		addAnnotations(aux, edge, edgeName+".annotations");

		edge.setPointOfDefinition(pattern);
	}

	private void genSubpatternEmbedding(SourceBuilder sb, SourceBuilder aux, String patGraphVarName,
			String className, HashMap<Entity, String> alreadyDefinedEntityToName, String pathPrefixForElements,
			SubpatternUsage sub, String subName) {
		sb.appendFront("GRGEN_LGSP.PatternGraphEmbedding " + subName
				+ " = new GRGEN_LGSP.PatternGraphEmbedding(");
		sb.append("\"" + formatIdentifiable(sub) + "\", ");
		sb.append(getPackagePrefixDot(sub.getSubpatternAction()) + "Pattern_" + sub.getSubpatternAction().getIdent().toString() + ".Instance, \n");
		sb.indent();
		
		sb.appendFront("new GRGEN_EXPR.Expression[] {\n");
		NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
		for(Expression expr : sub.getSubpatternConnections()) {
			expr.collectNeededEntities(needs);
			sb.appendFront("\t");
			genExpressionTree(sb, expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
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
		
		addAnnotations(aux, sub, subName+".annotations");
	}

	private void genPatternCondition(SourceBuilder sb, String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, Expression expr, String condName) {
		NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
		expr.collectNeededEntities(needs);
		sb.appendFront("GRGEN_LGSP.PatternCondition " + condName + " = new GRGEN_LGSP.PatternCondition(\n");
		sb.indent();
		sb.appendFront("");
		genExpressionTree(sb, expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
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
			HashMap<Entity, String> alreadyDefinedEntityToName, String pathPrefixForElements, EvalStatements yields) {
		String yieldName = pathPrefixForElements + yields.getName();
		sb.appendFront("GRGEN_LGSP.PatternYielding " + yieldName + " = new GRGEN_LGSP.PatternYielding(");
		sb.append("\"" + yields.getName() + "\",\n ");
		sb.indent();
		sb.appendFront("new GRGEN_EXPR.Yielding[] {\n");
		sb.indent();

		for(EvalStatement yield : yields.evalStatements) {
			genYield(sb, yield, className, pathPrefixForElements, alreadyDefinedEntityToName);
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

	private void getPatternAlternative(SourceBuilder sb, String pathPrefixForElements, Alternative alt) {
		String altName = alt.getNameOfGraph();
		sb.appendFront("GRGEN_LGSP.Alternative " + pathPrefixForElements+altName + " = new GRGEN_LGSP.Alternative( ");
		sb.append("\"" + altName + "\", ");
		sb.append("\"" + pathPrefixForElements + "\", ");
		sb.append("new GRGEN_LGSP.PatternGraph[] ");
		genAlternativesSet(sb, alt.getAlternativeCases(), pathPrefixForElements+altName+"_", "", true);
		sb.append(" );\n\n");
	}

	private void genPatternIterated(SourceBuilder sb, String packageName, String pathPrefixForElements, Rule iter) {
		PatternGraph iterPattern = iter.getLeft();
		String iterName = iterPattern.getNameOfGraph();
		sb.appendFront("GRGEN_LGSP.Iterated " + pathPrefixForElements+iterName+"_it" + " = new GRGEN_LGSP.Iterated( ");
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
			String pathPrefixForElements, GraphEntity entity) {
		if(entity.storageAccess!=null) {
			if(entity.storageAccess.storageVariable!=null) {
				Variable storageVariable = entity.storageAccess.storageVariable;
				sb.append("new GRGEN_LGSP.StorageAccess(" + formatEntity(storageVariable, pathPrefixForElements, alreadyDefinedEntityToName) + "), ");
			} else if(entity.storageAccess.storageAttribute!=null) {
				Qualification storageAttribute = entity.storageAccess.storageAttribute;
				GraphEntity owner = (GraphEntity)storageAttribute.getOwner();
				Entity member = storageAttribute.getMember();
				sb.append("new GRGEN_LGSP.StorageAccess(new GRGEN_LGSP.QualificationAccess(" + formatEntity(owner, pathPrefix, alreadyDefinedEntityToName) + ", ");
				sb.append(formatTypeClassRef(owner.getParameterInterfaceType()!=null ? owner.getParameterInterfaceType() : owner.getType()) + ".typeVar" + ".GetAttributeType(\"" + formatIdentifiable(member) + "\")");
				sb.append(")), ");
			}
		} else {
			sb.append("null, ");
		}
		if(entity.storageAccessIndex!=null) {
			if(entity.storageAccessIndex.indexGraphEntity!=null) {
				GraphEntity indexGraphEntity = entity.storageAccessIndex.indexGraphEntity;
				sb.append("new GRGEN_LGSP.StorageAccessIndex(" + formatEntity(indexGraphEntity, pathPrefixForElements, alreadyDefinedEntityToName) + "), ");
			}
		} else {
			sb.append("null, ");
		}
	}

	private void genIndexAccess(SourceBuilder sb, String pathPrefix, 
			String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, GraphEntity entity, List<Entity> parameters) {
		if(entity.indexAccess != null) {
			if(entity.indexAccess instanceof IndexAccessEquality) {
				IndexAccessEquality indexAccess = (IndexAccessEquality)entity.indexAccess;
				NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
				indexAccess.expr.collectNeededEntities(needs);
				Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
				sb.append("new GRGEN_LGSP.IndexAccessEquality(");
				sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\"" + indexAccess.index.getIdent() + "\"), ");
				sb.append(neededEntity!=null ? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", " : "null, ");
				sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
				genExpressionTree(sb, indexAccess.expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
				sb.append("), ");
			} else if(entity.indexAccess instanceof IndexAccessOrdering) {
				IndexAccessOrdering indexAccess = (IndexAccessOrdering)entity.indexAccess;
				NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
				if(indexAccess.from()!=null)
					indexAccess.from().collectNeededEntities(needs);
				if(indexAccess.to()!=null)
					indexAccess.to().collectNeededEntities(needs);
				Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
				if(indexAccess.ascending) {
					sb.append("new GRGEN_LGSP.IndexAccessAscending(");
				} else {
					sb.append("new GRGEN_LGSP.IndexAccessDescending(");
				}
				sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\"" + indexAccess.index.getIdent() + "\"), ");
				sb.append(neededEntity!=null ? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", " : "null, ");
				sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
				if(indexAccess.from()!=null)
					genExpressionTree(sb, indexAccess.from(), className, pathPrefixForElements, alreadyDefinedEntityToName);
				else
					sb.append("null");
				sb.append(", " + (indexAccess.includingFrom() ? "true, " : "false, "));
				if(indexAccess.to()!=null)
					genExpressionTree(sb, indexAccess.to(), className, pathPrefixForElements, alreadyDefinedEntityToName);
				else
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
			String pathPrefixForElements, GraphEntity entity, List<Entity> parameters) {
		if(entity.nameMapAccess != null) {
			NameLookup nameMapAccess = entity.nameMapAccess;
			NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
			nameMapAccess.expr.collectNeededEntities(needs);
			Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
			sb.append("new GRGEN_LGSP.NameLookup(");
			sb.append(neededEntity!=null ? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", " : "null, ");
			sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
			genExpressionTree(sb, nameMapAccess.expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append("), ");
		} else {
			sb.append("null, ");
		}		
	}

	private void genUniqueLookup(SourceBuilder sb, String pathPrefix, 
			String className, HashMap<Entity, String> alreadyDefinedEntityToName,
			String pathPrefixForElements, GraphEntity entity, List<Entity> parameters) {
		if(entity.uniqueIndexAccess != null) {
			UniqueLookup uniqueIndexAccess = entity.uniqueIndexAccess;
			NeededEntities needs = new NeededEntities(true, true, true, false, false, true, false, false);
			uniqueIndexAccess.expr.collectNeededEntities(needs);
			Entity neededEntity = getAtMostOneNeededNodeOrEdge(needs, parameters);
			sb.append("new GRGEN_LGSP.UniqueLookup(");
			sb.append(neededEntity!=null ? formatEntity(neededEntity, pathPrefix, alreadyDefinedEntityToName) + ", " : "null, ");
			sb.append(!needs.variables.isEmpty() ? "true, " : "false, ");
			genExpressionTree(sb, uniqueIndexAccess.expr, className, pathPrefixForElements, alreadyDefinedEntityToName);
			sb.append("), ");
		} else {
			sb.append("null, ");
		}		
	}

	private void genRuleParam(SourceBuilder sb, MatchingAction action, String packageName) {
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity ent : action.getParameters()) {
			if(ent instanceof Variable) {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(ent) + ")), ");
			} else {
				GraphEntity gent = (GraphEntity)ent;
				sb.append(formatTypeClassRef(gent.getParameterInterfaceType()!=null ? gent.getParameterInterfaceType() : gent.getType()) + ".typeVar, ");
			}
		}
		sb.append("},\n");

		sb.appendFront("new string[] { ");
		for(Entity ent : action.getParameters())
			sb.append("\"" + formatEntity(ent, action.getPattern().getNameOfGraph()+"_") + "\", ");
		sb.append("},\n");

		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity ent : action.getDefParameters()) {
			if(ent instanceof Variable) {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(ent) + ")), ");
			} else {
				GraphEntity gent = (GraphEntity)ent;
				sb.append(formatTypeClassRef(gent.getParameterInterfaceType()!=null ? gent.getParameterInterfaceType() : gent.getType()) + ".typeVar, ");
			}
		}
		sb.append("},\n");

		sb.appendFront("new string[] { ");
		for(Entity ent : action.getDefParameters())
			sb.append("\"" + formatIdentifiable(ent) + "\", ");
		sb.append("}");
	}

	private void genRuleResult(SourceBuilder sb, Rule rule, String packageName) {
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

	private void genRuleFilter(SourceBuilder sb, Rule rule, String packageName) {		
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

	private void genRuleMatchClassInfo(SourceBuilder sb, Rule rule, String packageName) {			
		sb.appendFront("new GRGEN_LIBGR.MatchClassInfo[] { ");
		for(DefinedMatchType implementedMatchClass : rule.getImplementedMatchClasses()) {
			sb.append(getPackagePrefixDot(implementedMatchClass) + "MatchClassInfo_" + implementedMatchClass.getIdent().toString() + ".Instance");
			sb.append(", ");
		}
		sb.append("}");
	}

	private void genFilterAutoSupplied(SourceBuilder sb, String filterName, String packageName, String parameterType) {
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoSupplied(\"" + filterName + "\", "); 
		sb.append("null, ");
		sb.append("\"" + filterName + "\", ");
		sb.append(packageName!=null ? "\"" + packageName + "\", " : "null, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] {");
		sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + parameterType + ")), ");
		sb.append("}, "); 
		sb.append("new String[] {");
		sb.append("\"param\"");
		sb.append("}");
		sb.append("),\n ");
	}

	private void genFilterAutoGenerated(SourceBuilder sb, FilterAutoGenerated fag, String packageName) {
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoGenerated(\"" + fag.getFilterName() + fag.getSuffix() + "\", ");
		sb.append("null, ");
		sb.append("\"" + fag.getFilterName() + fag.getSuffix() + "\", ");
		sb.append(packageName!=null ? "\"" + packageName + "\", " : "null, ");
		sb.append("\"" + fag.getFilterName() + "\", ");
		sb.append("new String[] { ");
		if(fag.getFilterEntities()!=null) {
			boolean first = true;
			for(String filterEntity : fag.getFilterEntities()) {
				if(first)
					first = false;
				else
					sb.append(", ");
				sb.append("\"" + filterEntity + "\"");
			}
		} 
		sb.append("} ");
		sb.append("),\n ");
	}

	private void genFilterFunction(SourceBuilder sb, FilterFunction ff, String packageName) {
		String packageNameOfFilterFunction = ff.getPackageContainedIn();
		sb.appendFront("new GRGEN_LGSP.LGSPFilterFunction(\"" + ff.getFilterName() + "\", "); 
		sb.append(packageNameOfFilterFunction!=null ? "\"" + packageNameOfFilterFunction + "\", " : "null, ");
		sb.append("\"" + (packageNameOfFilterFunction!=null ? packageNameOfFilterFunction + "::" + ff.getFilterName() : ff.getFilterName()) + "\", ");
		sb.append((ff instanceof FilterFunctionExternal ? "true" : "false") + ", "); 
		sb.append(packageNameOfFilterFunction!=null ? "\"" + packageNameOfFilterFunction + "\", " : "null, ");
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

	private void genMatchClassFilterAutoSupplied(SourceBuilder sb, String filterName, String packageName, String parameterType) {
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoSupplied(\"" + filterName + "\", "); 
		sb.append("null, ");
		sb.append("\"" + filterName + "\", ");
		sb.append(packageName!=null ? "\"" + packageName + "\", " : "null, ");
		sb.append("new GRGEN_LIBGR.GrGenType[] {");
		sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + parameterType + ")), ");
		sb.append("}, "); 
		sb.append("new String[] {");
		sb.append("\"param\"");
		sb.append("}");
		sb.append("),\n");
	}

	private void genMatchClassFilterAutoGenerated(SourceBuilder sb, MatchClassFilterAutoGenerated mfag, String packageName) {
		sb.appendFront("new GRGEN_LGSP.LGSPFilterAutoGenerated(\"" + mfag.getFilterName() + mfag.getSuffix() + "\", ");
		sb.append("null, ");
		sb.append("\"" + mfag.getFilterName() + mfag.getSuffix() + "\", ");
		sb.append(packageName!=null ? "\"" + packageName + "\", " : "null, ");
		sb.append("\"" + mfag.getFilterName() + "\", ");
		sb.append("new String[] { ");
		if(mfag.getFilterEntities()!=null) {
			boolean first = true;
			for(String filterEntity : mfag.getFilterEntities()) {
				if(first)
					first = false;
				else
					sb.append(", ");
				sb.append("\"" + filterEntity + "\"");
			}
		} 
		sb.append("}");
		sb.append("),\n");
	}

	private void genMatchClassFilterFunction(SourceBuilder sb, MatchClassFilterFunction mff, String packageName) {
		String packageNameOfFilterFunction = mff.getPackageContainedIn();
		sb.appendFront("new GRGEN_LGSP.LGSPFilterFunction(\"" + mff.getFilterName() + "\", "); 
		sb.append(packageNameOfFilterFunction!=null ? "\"" + packageNameOfFilterFunction + "\", " : "null, ");
		sb.append("\"" + (packageNameOfFilterFunction!=null ? packageNameOfFilterFunction + "::" + mff.getFilterName() : mff.getFilterName()) + "\", ");
		sb.append((mff instanceof MatchClassFilterFunctionExternal ? "true" : "false") + ", "); 
		sb.append(packageName!=null ? "\"" + packageName + "\", " : "null, ");
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

	//////////////////////////////////////////
	// Imperative statement/exec generation //
	//////////////////////////////////////////

	private void genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName,
			boolean isTopLevel, boolean isSubpattern) {
		if(rule.getRight()==null) {
			return;
		}
		
		if(isTopLevel) {
			sb.append("#if INITIAL_WARMUP\t\t// GrGen imperative statement section: " 
					+ getPackagePrefixDoubleColon(rule) + (isSubpattern ? "Pattern_" : "Rule_") + formatIdentifiable(rule) + "\n");
		}
		
		genImperativeStatements(sb, rule, pathPrefix, packageName);
				
		PatternGraph pattern = rule.getPattern();
		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				genImperativeStatements(sb, altCase,
						pathPrefix + altName + "_" + altCasePattern.getNameOfGraph() + "_", packageName,
						false, isSubpattern);
			}
		}
		
		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			genImperativeStatements(sb, iter,
					pathPrefix + iterName + "_", packageName,
					false, isSubpattern);
		}
		
		if(isTopLevel) {
			sb.append("#endif\n");
		}
	}

	private void genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName) {
		int xgrsID = 0;
		for(EvalStatements evals : rule.getEvals()) {
			for(EvalStatement eval : evals.evalStatements) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, eval, xgrsID);
			}
		}
		for(ImperativeStmt istmt : rule.getRight().getImperativeStmts()) {
			if(istmt instanceof Exec) {
				xgrsID = genExec(sb, pathPrefix, packageName, (Exec)istmt, xgrsID);
			} else if(istmt instanceof Emit) {
				// nothing to do
			} else {
				assert false : "unknown ImperativeStmt: " + istmt + " in " + rule;
			}
		}
	}

	private int genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName, EvalStatement evalStmt, int xgrsID) {
		if(evalStmt instanceof ConditionStatement) {
			ConditionStatement condStmt = (ConditionStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : condStmt.getTrueCaseStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			if(condStmt.getFalseCaseStatements()!=null) {
				for(EvalStatement nestedEvalStmt : condStmt.getFalseCaseStatements()) {
					xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
				}
			}
		}
		else if(evalStmt instanceof SwitchStatement) {
			SwitchStatement switchStmt = (SwitchStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : switchStmt.getStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof CaseStatement) {
			CaseStatement caseStmt = (CaseStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : caseStmt.getStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof WhileStatement) {
			WhileStatement whileStmt = (WhileStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : whileStmt.getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof DoWhileStatement) {
			DoWhileStatement doWhileStmt = (DoWhileStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : doWhileStmt.getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof ContainerAccumulationYield) {
			ContainerAccumulationYield containerAccumulationYieldStmt = (ContainerAccumulationYield)evalStmt;
			for(EvalStatement nestedEvalStmt : containerAccumulationYieldStmt.getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof IntegerRangeIterationYield) {
			IntegerRangeIterationYield integerRangeIterationYieldStmt = (IntegerRangeIterationYield)evalStmt;
			for(EvalStatement nestedEvalStmt : integerRangeIterationYieldStmt.getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof MatchesAccumulationYield) {
			MatchesAccumulationYield matchesAccumulationYieldStmt = (MatchesAccumulationYield)evalStmt;
			for(EvalStatement nestedEvalStmt : matchesAccumulationYieldStmt.getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof ForFunction) {
			ForFunction forFunctionStmt = (ForFunction)evalStmt;
			for(EvalStatement nestedEvalStmt : forFunctionStmt.getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		}
		else if(evalStmt instanceof ExecStatement) {
			ExecStatement execStmt = (ExecStatement)evalStmt;
			xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, execStmt, xgrsID);
		}
		return xgrsID;
	}

	private int genExec(SourceBuilder sb, String pathPrefix, String packageName, Exec exec, int xgrsID) {
		sb.appendFront("public static GRGEN_LIBGR.EmbeddedSequenceInfo XGRSInfo_" + pathPrefix + xgrsID
				+ " = new GRGEN_LIBGR.EmbeddedSequenceInfo(\n");
		sb.indent();
		sb.appendFront("new string[] {");
		for(Entity neededEntity : exec.getNeededEntities(false)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append("\"" + neededEntity.getIdent() + "\", ");
			}
		}
		sb.append("},\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity neededEntity : exec.getNeededEntities(false)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof Variable) {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(neededEntity) + ")), ");
				} else {
					GraphEntity gent = (GraphEntity)neededEntity;
					sb.append(formatTypeClassRef(gent.getType()) + ".typeVar, ");
				}
			}
		}
		sb.append("},\n");
		sb.appendFront("new string[] {");
		for(Entity neededEntity : exec.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append("\"" + neededEntity.getIdent() + "\", ");
			}
		}
		sb.append("},\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity neededEntity : exec.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof Variable) {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(neededEntity) + ")), ");
				} else {
					GraphEntity gent = (GraphEntity)neededEntity;
					sb.append(formatTypeClassRef(gent.getType()) + ".typeVar, ");
				}
			}
		}
		sb.append("},\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ",\n");
		sb.appendFront("\"" + escapeBackslashAndDoubleQuotes(exec.getXGRSString()) + "\",\n");
		sb.appendFront(exec.getLineNr() + "\n");
		sb.unindent();
		sb.appendFront(");\n");
		
		sb.appendFront("private static bool ApplyXGRS_" + pathPrefix + xgrsID + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
		for(Entity neededEntity : exec.getNeededEntities(false)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append(", " + formatType(neededEntity.getType()) + " " + formatEntity(neededEntity));
			}
		}
		for(Entity neededEntity : exec.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append(", ref " + formatType(neededEntity.getType()) + " " + formatEntity(neededEntity));
			}
		}
		sb.append(") {\n");
		sb.indent();
		for(Entity neededEntity : exec.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.appendFront(formatEntity(neededEntity) + " = ");
				sb.append(getInitializationValue(neededEntity.getType()) + ";\n");
				sb.append(";\n");
			}
		}
		sb.appendFront("return true;\n");
		sb.unindent();
		sb.appendFront("}\n");
		
		++xgrsID;
		return xgrsID;
	}

	private int genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName, ExecStatement execStmt, int xgrsID) {
		sb.appendFront("public static GRGEN_LIBGR.EmbeddedSequenceInfo XGRSInfo_" + pathPrefix + xgrsID
				+ " = new GRGEN_LIBGR.EmbeddedSequenceInfo(\n");
		sb.indent();
		sb.appendFront("new string[] {");
		for(Entity neededEntity : execStmt.getNeededEntities(false)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append("\"" + neededEntity.getIdent() + "\", ");
			}
		}
		sb.append("},\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity neededEntity : execStmt.getNeededEntities(false)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof Variable) {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(neededEntity) + ")), ");
				} else {
					GraphEntity gent = (GraphEntity)neededEntity;
					sb.append(formatTypeClassRef(gent.getType()) + ".typeVar, ");
				}
			}
		}
		sb.append("},\n");
		sb.appendFront("new string[] {");
		for(Entity neededEntity : execStmt.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append("\"" + neededEntity.getIdent() + "\", ");
			}
		}
		sb.append("},\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity neededEntity : execStmt.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof Variable) {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(neededEntity) + ")), ");
				} else {
					GraphEntity gent = (GraphEntity)neededEntity;
					sb.append(formatTypeClassRef(gent.getType()) + ".typeVar, ");
				}
			}
		}
		sb.append("},\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ",\n");
		sb.appendFront("\"" + escapeBackslashAndDoubleQuotes(execStmt.getXGRSString()) + "\",\n");
		sb.appendFront(execStmt.getLineNr() + "\n");
		sb.unindent();
		sb.appendFront(");\n");
		
		sb.appendFront("private static bool ApplyXGRS_" + pathPrefix + xgrsID + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
		for(Entity neededEntity : execStmt.getNeededEntities(false)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append(", " + formatType(neededEntity.getType()) + " " + formatEntity(neededEntity));
			}
		}
		for(Entity neededEntity : execStmt.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append(", ref " + formatType(neededEntity.getType()) + " " + formatEntity(neededEntity));
			}
		}
		sb.append(") {\n");
		sb.indent();
		for(Entity neededEntity : execStmt.getNeededEntities(false)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.appendFront(formatEntity(neededEntity) + " = ");
				sb.append(getInitializationValue(neededEntity.getType()) + ";\n");
				sb.append(";\n");
			}
		}
		sb.appendFront("return true;\n");
		sb.unindent();
		sb.appendFront("}\n");
		
		++xgrsID;
		return xgrsID;
	}

	private void genImperativeStatementClosures(SourceBuilder sb, Rule rule, String pathPrefix,
			boolean isTopLevelRule) {
		if(rule.getRight()==null) {
			return;
		}

		if(!isTopLevelRule) {
			genImperativeStatementClosures(sb, rule, pathPrefix);
		}
						
		PatternGraph pattern = rule.getPattern();
		for(Alternative alt : pattern.getAlts()) {
			String altName = alt.getNameOfGraph();
			for(Rule altCase : alt.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				genImperativeStatementClosures(sb, altCase,
						pathPrefix + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						false);
			}
		}
		
		for(Rule iter : pattern.getIters()) {
			String iterName = iter.getLeft().getNameOfGraph();
			genImperativeStatementClosures(sb, iter,
					pathPrefix + iterName + "_",
					false);
		}
	}

	private void genImperativeStatementClosures(SourceBuilder sb, Rule rule, String pathPrefix) {
		int xgrsID = 0;
		for(ImperativeStmt istmt : rule.getRight().getImperativeStmts()) {
			if(!(istmt instanceof Exec)) {
				continue;
			}
			
			Exec exec = (Exec) istmt;
			sb.append("\n");
			sb.appendFront("public class XGRSClosure_" + pathPrefix + xgrsID + " : GRGEN_LGSP.LGSPEmbeddedSequenceClosure\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("public XGRSClosure_" + pathPrefix + xgrsID + "(");
			boolean first = true;
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				if(first) {
					first = false;
				} else {
					sb.append(", ");
				}
				sb.append(formatType(neededEntity.getType()) + " " + formatEntity(neededEntity));
			}
			sb.append(") {\n");
			sb.indent();
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				sb.appendFront("this." + formatEntity(neededEntity) + " = " + formatEntity(neededEntity) + ";\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			
			sb.appendFront("public override bool exec(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv) {\n");
			sb.appendFront("\treturn ApplyXGRS_" + pathPrefix + xgrsID + "(procEnv");
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				sb.append(", " + formatEntity(neededEntity));
			}
			
			sb.append(");\n"); 
			sb.appendFront("}\n");
			
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				sb.appendFront(formatType(neededEntity.getType()) + " " + formatEntity(neededEntity) + ";\n");
			}

			//sb.append("\n");
			//sb.append("\t\t\tpublic static int numFreeClosures = 0;\n");
			//sb.append("\t\t\tpublic static LGSPEmbeddedSequenceClosure rootOfFreeClosures = null;\n");

			sb.unindent();
			sb.append("}\n");
			
			++xgrsID;
		}
	}

	private void genImperativeStatements(SourceBuilder sb, Procedure procedure) {
		int xgrsID = 0;
		for(EvalStatement evalStmt : procedure.getComputationStatements()) {
			xgrsID = genImperativeStatements(sb, procedure, evalStmt, xgrsID);
		}
	}

	private int genImperativeStatements(SourceBuilder sb, Procedure procedure, EvalStatement evalStmt, int xgrsID) {
		if(evalStmt instanceof ExecStatement) {
			genImperativeStatement(sb, procedure, procedure.getPackageContainedIn(), (ExecStatement)evalStmt, xgrsID);
			++xgrsID;
		} else if(evalStmt instanceof ConditionStatement) {
			ConditionStatement condStmt = (ConditionStatement)evalStmt;
			for(EvalStatement childEvalStmt : condStmt.getTrueCaseStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			if(condStmt.getFalseCaseStatements() != null) {
				for(EvalStatement childEvalStmt : condStmt.getFalseCaseStatements()) {
					xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
				}
			}
		} else if(evalStmt instanceof SwitchStatement) {
			SwitchStatement switchStmt = (SwitchStatement)evalStmt;
			for(EvalStatement childEvalStmt : switchStmt.getStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof CaseStatement) {
			CaseStatement caseStmt = (CaseStatement)evalStmt;
			for(EvalStatement childEvalStmt : caseStmt.getStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof ContainerAccumulationYield) {
			for(EvalStatement childEvalStmt : ((ContainerAccumulationYield)evalStmt).getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof IntegerRangeIterationYield) {
			for(EvalStatement childEvalStmt : ((IntegerRangeIterationYield)evalStmt).getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof ForFunction) {
			for(EvalStatement childEvalStmt : ((ForFunction)evalStmt).getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof DoWhileStatement) {
			for(EvalStatement childEvalStmt : ((DoWhileStatement)evalStmt).getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof WhileStatement) {
			for(EvalStatement childEvalStmt : ((WhileStatement)evalStmt).getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof MultiStatement) {
			for(EvalStatement childEvalStmt : ((MultiStatement)evalStmt).getStatements()) {
				xgrsID = genImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
		}
		return xgrsID;
	}

	private void genImperativeStatement(SourceBuilder sb, Identifiable procedure, String packageName,
			ExecStatement execStmt, int xgrsID) {
		Exec exec = execStmt.getExec();
		
		sb.appendFront("public static GRGEN_LIBGR.EmbeddedSequenceInfo XGRSInfo_" + formatIdentifiable(procedure) + "_" + xgrsID
				+ " = new GRGEN_LIBGR.EmbeddedSequenceInfo(\n");
		sb.indent();
		sb.appendFront("new string[] {");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append("\"" + neededEntity.getIdent() + "\", ");
			}
		}
		sb.append("},\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof Variable) {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(neededEntity) + ")), ");
				} else {
					GraphEntity gent = (GraphEntity)neededEntity;
					sb.append(formatTypeClassRef(gent.getType()) + ".typeVar, ");
				}
			}
		}
		sb.append("},\n");
		sb.appendFront("new string[] {");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append("\"" + neededEntity.getIdent() + "\", ");
			}
		}
		sb.append("},\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof Variable) {
					sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(neededEntity) + ")), ");
				} else {
					GraphEntity gent = (GraphEntity)neededEntity;
					sb.append(formatTypeClassRef(gent.getType()) + ".typeVar, ");
				}
			}
		}
		sb.append("},\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ",\n");
		sb.appendFront("\"" + escapeBackslashAndDoubleQuotes(exec.getXGRSString()) + "\",\n");
		sb.appendFront(exec.getLineNr() + "\n");
		sb.unindent();
		sb.appendFront(");\n");
		
		sb.appendFront("private static bool ApplyXGRS_" + formatIdentifiable(procedure) + "_" + xgrsID + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append(", " + formatType(neededEntity.getType()) + " " + formatEntity(neededEntity));
			}
		}
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append(", ref " + formatType(neededEntity.getType()) + " " + formatEntity(neededEntity));
			}
		}
		sb.append(") {\n");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.appendFront(formatEntity(neededEntity) + " = ");
				sb.append(getInitializationValue(neededEntity.getType()) + ";\n");
				sb.append(";\n");
			}
		}
		sb.appendFront("return true;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	//////////////////////////////////////////
	// Condition expression tree generation //
	//////////////////////////////////////////

	private void genExpressionTree(SourceBuilder sb, Expression expr, String className,
			String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			String opNamePrefix = "";
			if(op.getType() instanceof SetType || op.getType() instanceof MapType)
				opNamePrefix = "DICT_";
			if(op.getType() instanceof ArrayType)
				opNamePrefix = "LIST_";
			if(op.getType() instanceof DequeType)
				opNamePrefix = "DEQUE_";
			if(op.getOpCode()==Operator.EQ || op.getOpCode()==Operator.NE 
				|| op.getOpCode()==Operator.SE
				|| op.getOpCode()==Operator.GT || op.getOpCode()==Operator.GE
				|| op.getOpCode()==Operator.LT || op.getOpCode()==Operator.LE) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof SetType || opnd.getType() instanceof MapType) {
					opNamePrefix = "DICT_";
				}
				if(opnd.getType() instanceof ArrayType) {
					opNamePrefix = "LIST_";
				}
				if(opnd.getType() instanceof DequeType) {
					opNamePrefix = "DEQUE_";
				}
				if(opnd.getType() instanceof GraphType) {
					opNamePrefix = "GRAPH_";
				}
			}
			if(op.getOpCode()==Operator.GT || op.getOpCode()==Operator.GE
				|| op.getOpCode()==Operator.LT || op.getOpCode()==Operator.LE) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof StringType) {
					opNamePrefix = "STRING_";
				}
			}
			if(model.isEqualClassDefined() && (op.getOpCode()==Operator.EQ || op.getOpCode()==Operator.NE)) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof ObjectType || opnd.getType() instanceof ExternalType) {
					opNamePrefix = "EXTERNAL_";
				}
			}
			if(model.isLowerClassDefined() && (op.getOpCode()==Operator.GT || op.getOpCode()==Operator.GE || op.getOpCode()==Operator.LT || op.getOpCode()==Operator.LE)) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof ObjectType || opnd.getType() instanceof ExternalType) {
					opNamePrefix = "EXTERNAL_";
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
					if(op.getOpCode()==Operator.IN) {
						if(op.getOperand(0) instanceof GraphEntityExpression)
							sb.append(", \"" + formatElementInterfaceRef(op.getOperand(0).getType()) + "\"");
						boolean isDictionary = op.getOperand(1).getType() instanceof SetType || op.getOperand(1).getType() instanceof MapType;
						sb.append(isDictionary ? ", true" : ", false");
					}
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
			if(Expression.isGlobalVariable(owner)) {
				sb.append("new GRGEN_EXPR.GlobalVariableQualification(\"" + formatType(owner.getType())
						+ "\", \"" + formatIdentifiable(owner, pathPrefix, alreadyDefinedEntityToName) + "\", \"" + formatIdentifiable(member) + "\")");
			} else if(owner!=null) {
				sb.append("new GRGEN_EXPR.Qualification(\"" + formatElementInterfaceRef(owner.getType())
						+ "\", \"" + formatEntity(owner, pathPrefix, alreadyDefinedEntityToName) + "\", \"" + formatIdentifiable(member) + "\")");
			} else {
				sb.append("new GRGEN_EXPR.CastQualification(");
				genExpressionTree(sb, qual.getOwnerExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(member) + "\")");
			}
		}
		else if(expr instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression) expr;
			sb.append("new GRGEN_EXPR.ConstantEnumExpression(\"" + enumExp.getType().getIdent().toString()
					+ "\", \"" + enumExp.getEnumItem().toString() + "\")");
		}
		else if(expr instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) expr;
			sb.append("new GRGEN_EXPR.Constant(\"" + escapeBackslashAndDoubleQuotes(getValueAsCSSharpString(constant)) + "\")");
		}
		else if(expr instanceof Nameof) {
			Nameof no = (Nameof) expr;
			sb.append("new GRGEN_EXPR.Nameof(");
			if(no.getNamedEntity()==null)
				sb.append("null");
			else
				genExpressionTree(sb, no.getNamedEntity(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if(expr instanceof Uniqueof) {
			Uniqueof uo = (Uniqueof) expr;
			sb.append("new GRGEN_EXPR.Uniqueof(");
			if(uo.getEntity()!=null)
				genExpressionTree(sb, uo.getEntity(), className, pathPrefix, alreadyDefinedEntityToName);
			else
				sb.append("null");
			if(uo.getEntity()!=null && uo.getEntity().getType() instanceof NodeType)
				sb.append(", true, false");
			else if(uo.getEntity()!=null && uo.getEntity().getType() instanceof EdgeType)
				sb.append(", false, false");	
			else
				sb.append(", false, true");					
			sb.append(")");
		}
		else if(expr instanceof ExistsFileExpr) {
			ExistsFileExpr efe = (ExistsFileExpr) expr;
			sb.append("new GRGEN_EXPR.ExistsFileExpression(");
			genExpressionTree(sb, efe.getPathExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if(expr instanceof ImportExpr) {
			ImportExpr ie = (ImportExpr) expr;
			sb.append("new GRGEN_EXPR.ImportExpression(");
			genExpressionTree(sb, ie.getPathExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if(expr instanceof CopyExpr) {
			CopyExpr ce = (CopyExpr) expr;
			Type t = ce.getSourceExpr().getType();
			sb.append("new GRGEN_EXPR.CopyExpression(");
			genExpressionTree(sb, ce.getSourceExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(t instanceof GraphType) {
				sb.append(", null");
			} else { // no match type possible here, can only occur in filter function (-> CSharpBase expression)
				sb.append(", \"" + formatType(t) + "\"");
			}
			sb.append(")");
		}
		else if(expr instanceof Count) {
			Count count = (Count) expr;
			sb.append("new GRGEN_EXPR.Count(\"" + formatIdentifiable(count.getIterated()) + "\")");
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
				if(cast.getExpression().getType() instanceof SetType 
					|| cast.getExpression().getType() instanceof MapType
					|| cast.getExpression().getType() instanceof ArrayType 
					|| cast.getExpression().getType() instanceof DequeType) {
					sb.append(", true");
				} else {
					sb.append(", false");
				}
				sb.append(")");
			}
		}
		else if(expr instanceof VariableExpression) {
			Variable var = ((VariableExpression) expr).getVariable();
			if(!Expression.isGlobalVariable(var)) {
				sb.append("new GRGEN_EXPR.VariableExpression(\"" + formatEntity(var, pathPrefix, alreadyDefinedEntityToName) + "\")");
			} else {
				sb.append("new GRGEN_EXPR.GlobalVariableExpression(\"" + formatIdentifiable(var) + "\", \"" + formatType(var.getType()) + "\")");
			}
		}
		else if(expr instanceof GraphEntityExpression) {
			GraphEntity ent = ((GraphEntityExpression) expr).getGraphEntity();
			if(!Expression.isGlobalVariable(ent)) {
				sb.append("new GRGEN_EXPR.GraphEntityExpression(\"" + formatEntity(ent, pathPrefix, alreadyDefinedEntityToName) + "\")");
			} else {
				sb.append("new GRGEN_EXPR.GlobalVariableExpression(\"" + formatIdentifiable(ent) + "\", \"" + formatType(ent.getType()) + "\")");
			}
		}
		else if(expr instanceof Visited) {
			Visited vis = (Visited) expr;
			sb.append("new GRGEN_EXPR.Visited(");
			genExpressionTree(sb, vis.getEntity(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, vis.getVisitorID(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if(expr instanceof RandomExpr) {
			RandomExpr re = (RandomExpr) expr;
			sb.append("new GRGEN_EXPR.Random(");
			if(re.getNumExpr()!=null)
				genExpressionTree(sb, re.getNumExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if(expr instanceof ThisExpr) {
			sb.append("new GRGEN_EXPR.This()");
		}
		else if (expr instanceof StringLength) {
			StringLength strlen = (StringLength) expr;
			sb.append("new GRGEN_EXPR.StringLength(");
			genExpressionTree(sb, strlen.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringToUpper) {
			StringToUpper strtoup = (StringToUpper) expr;
			sb.append("new GRGEN_EXPR.StringToUpper(");
			genExpressionTree(sb, strtoup.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringToLower) {
			StringToLower strtolow = (StringToLower) expr;
			sb.append("new GRGEN_EXPR.StringToLower(");
			genExpressionTree(sb, strtolow.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringSubstring) {
			StringSubstring strsubstr = (StringSubstring) expr;
			sb.append("new GRGEN_EXPR.StringSubstring(");
			genExpressionTree(sb, strsubstr.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strsubstr.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(strsubstr.getLengthExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, strsubstr.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof StringIndexOf) {
			StringIndexOf strio = (StringIndexOf) expr;
			sb.append("new GRGEN_EXPR.StringIndexOf(");
			genExpressionTree(sb, strio.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strio.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(strio.getStartIndexExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, strio.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);				
			}
			sb.append(")");
		}
		else if (expr instanceof StringLastIndexOf) {
			StringLastIndexOf strlio = (StringLastIndexOf) expr;
			sb.append("new GRGEN_EXPR.StringLastIndexOf(");
			genExpressionTree(sb, strlio.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strlio.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(strlio.getStartIndexExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, strlio.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);				
			}
			sb.append(")");
		}
		else if (expr instanceof StringStartsWith) {
			StringStartsWith strsw = (StringStartsWith) expr;
			sb.append("new GRGEN_EXPR.StringStartsWith(");
			genExpressionTree(sb, strsw.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strsw.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof StringEndsWith) {
			StringEndsWith strew = (StringEndsWith) expr;
			sb.append("new GRGEN_EXPR.StringEndsWith(");
			genExpressionTree(sb, strew.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strew.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
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
		else if (expr instanceof StringAsArray) {
			StringAsArray saa = (StringAsArray) expr;
			sb.append("new GRGEN_EXPR.StringAsArray(");
			genExpressionTree(sb, saa.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, saa.getStringToSplitAtExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof IndexedAccessExpr) {
			IndexedAccessExpr ia = (IndexedAccessExpr)expr;
			if(ia.getTargetExpr().getType() instanceof MapType)
				sb.append("new GRGEN_EXPR.MapAccess(");
			else if(ia.getTargetExpr().getType() instanceof ArrayType)
				sb.append("new GRGEN_EXPR.ArrayAccess(");
			else
				sb.append("new GRGEN_EXPR.DequeAccess(");
			genExpressionTree(sb, ia.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ia.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(ia.getKeyExpr() instanceof GraphEntityExpression)
				sb.append(", \"" + formatElementInterfaceRef(ia.getKeyExpr().getType()) + "\"");
			sb.append(")");
		}
		else if (expr instanceof IndexedIncidenceCountIndexAccessExpr) {
			IndexedIncidenceCountIndexAccessExpr ia = (IndexedIncidenceCountIndexAccessExpr)expr;
			sb.append("new GRGEN_EXPR.IncidenceCountIndexAccess(");
			sb.append("\"" + ia.getTarget().getIdent() + "\", ");
			genExpressionTree(sb, ia.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + formatElementInterfaceRef(ia.getKeyExpr().getType()) + "\"");
			sb.append(")");
		}
		else if (expr instanceof MapSizeExpr) {
			MapSizeExpr ms = (MapSizeExpr)expr;
			sb.append("new GRGEN_EXPR.MapSize(");
			genExpressionTree(sb, ms.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapEmptyExpr) {
			MapEmptyExpr me = (MapEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.MapEmpty(");
			genExpressionTree(sb, me.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
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
		else if (expr instanceof MapAsArrayExpr) {
			MapAsArrayExpr maa = (MapAsArrayExpr)expr;
			sb.append("new GRGEN_EXPR.MapAsArray(");
			genExpressionTree(sb, maa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapPeekExpr) {
			MapPeekExpr mp = (MapPeekExpr)expr;
			sb.append("new GRGEN_EXPR.MapPeek(");
			genExpressionTree(sb, mp.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, mp.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SetSizeExpr) {
			SetSizeExpr ss = (SetSizeExpr)expr;
			sb.append("new GRGEN_EXPR.SetSize(");
			genExpressionTree(sb, ss.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SetEmptyExpr) {
			SetEmptyExpr se = (SetEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.SetEmpty(");
			genExpressionTree(sb, se.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SetPeekExpr) {
			SetPeekExpr sp = (SetPeekExpr)expr;
			sb.append("new GRGEN_EXPR.SetPeek(");
			genExpressionTree(sb, sp.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, sp.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SetAsArrayExpr) {
			SetAsArrayExpr saa = (SetAsArrayExpr)expr;
			sb.append("new GRGEN_EXPR.SetAsArray(");
			genExpressionTree(sb, saa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArraySizeExpr) {
			ArraySizeExpr as = (ArraySizeExpr)expr;
			sb.append("new GRGEN_EXPR.ArraySize(");
			genExpressionTree(sb, as.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayEmptyExpr) {
			ArrayEmptyExpr ae = (ArrayEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayEmpty(");
			genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayPeekExpr) {
			ArrayPeekExpr ap = (ArrayPeekExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayPeek(");
			genExpressionTree(sb, ap.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(ap.getNumberExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, ap.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof ArrayIndexOfExpr) {
			ArrayIndexOfExpr ai = (ArrayIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOf(");
			genExpressionTree(sb, ai.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ai.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(ai.getStartIndexExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, ai.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);				
			}
			sb.append(")");
		}
		else if (expr instanceof ArrayIndexOfByExpr) {
			ArrayIndexOfByExpr aib = (ArrayIndexOfByExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOfBy(");
			genExpressionTree(sb, aib.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + ((ArrayType)aib.getTargetExpr().getType()).getValueType().getIdent().toString() + "\"");
			sb.append(", \"" + formatIdentifiable(aib.getMember()) + "\", ");
			genExpressionTree(sb, aib.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(aib.getStartIndexExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, aib.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);				
			}
			sb.append(")");
		}
		else if (expr instanceof ArrayIndexOfOrderedExpr) {
			ArrayIndexOfOrderedExpr aio = (ArrayIndexOfOrderedExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOfOrdered(");
			genExpressionTree(sb, aio.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, aio.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayIndexOfOrderedByExpr) {
			ArrayIndexOfOrderedByExpr aiob = (ArrayIndexOfOrderedByExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOfOrderedBy(");
			genExpressionTree(sb, aiob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + ((ArrayType)aiob.getTargetExpr().getType()).getValueType().getIdent().toString() + "\"");
			sb.append(", \"" + formatIdentifiable(aiob.getMember()) + "\", ");
			genExpressionTree(sb, aiob.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayLastIndexOfExpr) {
			ArrayLastIndexOfExpr ali = (ArrayLastIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayLastIndexOf(");
			genExpressionTree(sb, ali.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ali.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(ali.getStartIndexExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, ali.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof ArrayLastIndexOfByExpr) {
			ArrayLastIndexOfByExpr alib = (ArrayLastIndexOfByExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayLastIndexOfBy(");
			genExpressionTree(sb, alib.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + ((ArrayType)alib.getTargetExpr().getType()).getValueType().getIdent().toString() + "\"");
			sb.append(", \"" + formatIdentifiable(alib.getMember()) + "\", ");
			genExpressionTree(sb, alib.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(alib.getStartIndexExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, alib.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof ArraySubarrayExpr) {
			ArraySubarrayExpr as = (ArraySubarrayExpr)expr;
			sb.append("new GRGEN_EXPR.ArraySubarray(");
			genExpressionTree(sb, as.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, as.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, as.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayOrderAscending) {
			ArrayOrderAscending aoa = (ArrayOrderAscending)expr;
			sb.append("new GRGEN_EXPR.ArrayOrder(");
			genExpressionTree(sb, aoa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", true");
			sb.append(")");
		}
		else if (expr instanceof ArrayOrderDescending) {
			ArrayOrderDescending aod = (ArrayOrderDescending)expr;
			sb.append("new GRGEN_EXPR.ArrayOrder(");
			genExpressionTree(sb, aod.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", false");
			sb.append(")");
		}
		else if (expr instanceof ArrayKeepOneForEach) {
			ArrayKeepOneForEach ako = (ArrayKeepOneForEach)expr;
			sb.append("new GRGEN_EXPR.ArrayKeepOneForEach(");
			genExpressionTree(sb, ako.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayOrderAscendingBy) {
			ArrayOrderAscendingBy aoab = (ArrayOrderAscendingBy)expr;
			Type arrayValueType = ((ArrayType)aoab.getTargetExpr().getType()).getValueType();
			if(arrayValueType instanceof InheritanceType) {
				InheritanceType graphElementType = (InheritanceType)arrayValueType;
				ContainedInPackage cip = (ContainedInPackage)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOrderBy(");
				genExpressionTree(sb, aoab.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + graphElementType.getIdent().toString() + "\"");
				sb.append(", \"" + formatIdentifiable(aoab.getMember()) + "\"");
				sb.append(", " + (cip.getPackageContainedIn()!=null ? "\"" + cip.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderAscending");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchTypeIterated) {
				MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
				Rule rule = matchType.getAction();
				Rule iterated = matchType.getIterated();
				sb.append("new GRGEN_EXPR.ArrayOfIteratedMatchTypeOrderBy(");
				genExpressionTree(sb, aoab.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(iterated) + "\"");
				sb.append(", \"" + formatIdentifiable(aoab.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderAscending");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayOfMatchTypeOrderBy(");
				genExpressionTree(sb, aoab.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(aoab.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderAscending");
				sb.append(")");
			}
			else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOfMatchClassTypeOrderBy(");
				genExpressionTree(sb, aoab.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", \"" + formatIdentifiable(aoab.getMember()) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderAscending");
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayOrderDescendingBy) {
			ArrayOrderDescendingBy aodb = (ArrayOrderDescendingBy)expr;
			Type arrayValueType = ((ArrayType)aodb.getTargetExpr().getType()).getValueType();
			if(arrayValueType instanceof InheritanceType) {
				InheritanceType graphElementType = (InheritanceType)arrayValueType;
				ContainedInPackage cip = (ContainedInPackage)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOrderBy(");
				genExpressionTree(sb, aodb.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + graphElementType.getIdent().toString() + "\"");
				sb.append(", \"" + formatIdentifiable(aodb.getMember()) + "\"");
				sb.append(", " + (cip.getPackageContainedIn()!=null ? "\"" + cip.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderDescending");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchTypeIterated) {
				MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
				Rule rule = matchType.getAction();
				Rule iterated = matchType.getIterated();
				sb.append("new GRGEN_EXPR.ArrayOfIteratedMatchTypeOrderBy(");
				genExpressionTree(sb, aodb.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(iterated) + "\"");
				sb.append(", \"" + formatIdentifiable(aodb.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderDescending");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayOfMatchTypeOrderBy(");
				genExpressionTree(sb, aodb.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(aodb.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderDescending");
				sb.append(")");
			}
			else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOfMatchClassTypeOrderBy(");
				genExpressionTree(sb, aodb.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", \"" + formatIdentifiable(aodb.getMember()) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderDescending");
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayKeepOneForEachBy) {
			ArrayKeepOneForEachBy akob = (ArrayKeepOneForEachBy)expr;
			Type arrayValueType = ((ArrayType)akob.getTargetExpr().getType()).getValueType();
			if(arrayValueType instanceof InheritanceType) {
				InheritanceType graphElementType = (InheritanceType)arrayValueType;
				ContainedInPackage cip = (ContainedInPackage)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOrderBy(");
				genExpressionTree(sb, akob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + graphElementType.getIdent().toString() + "\"");
				sb.append(", \"" + formatIdentifiable(akob.getMember()) + "\"");
				sb.append(", " + (cip.getPackageContainedIn()!=null ? "\"" + cip.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.KeepOneForEach");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchTypeIterated) {
				MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
				Rule rule = matchType.getAction();
				Rule iterated = matchType.getIterated();
				sb.append("new GRGEN_EXPR.ArrayOfIteratedMatchTypeOrderBy(");
				genExpressionTree(sb, akob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(iterated) + "\"");
				sb.append(", \"" + formatIdentifiable(akob.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.KeepOneForEach");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayOfMatchTypeOrderBy(");
				genExpressionTree(sb, akob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(akob.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.KeepOneForEach");
				sb.append(")");
			}
			else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOfMatchClassTypeOrderBy(");
				genExpressionTree(sb, akob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", \"" + formatIdentifiable(akob.getMember()) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.KeepOneForEach");
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayReverseExpr) {
			ArrayReverseExpr ar = (ArrayReverseExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayReverse(");
			genExpressionTree(sb, ar.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayExtract) {
			ArrayExtract ae = (ArrayExtract)expr;
			Type arrayValueType = ((ArrayType)ae.getTargetExpr().getType()).getValueType();
			if(arrayValueType instanceof InheritanceType) {
				InheritanceType graphElementType = (InheritanceType)arrayValueType;
				ContainedInPackage cip = (ContainedInPackage)graphElementType;
				sb.append("new GRGEN_EXPR.ArrayExtractGraphElementType(");
				genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(ae.getMember()) + "\"");
				sb.append(", \"" + formatIdentifiable(graphElementType) + "\"");
				sb.append(", " + (cip.getPackageContainedIn()!=null ? "\"" + cip.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchTypeIterated) {
				MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
				Rule rule = matchType.getAction();
				Rule iterated = matchType.getIterated();
				sb.append("new GRGEN_EXPR.ArrayExtractIterated(");
				genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(ae.getMember()) + "\"");
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(iterated) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(")");
			}
			else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayExtract(");
				genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(ae.getMember()) + "\"");
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(")");
			}
			else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayExtractMatchClass(");
				genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(ae.getMember()) + "\"");
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayAsSetExpr) {
			ArrayAsSetExpr aas = (ArrayAsSetExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAsSet(");
			genExpressionTree(sb, aas.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayAsDequeExpr) {
			ArrayAsDequeExpr aad = (ArrayAsDequeExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAsDeque(");
			genExpressionTree(sb, aad.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayAsMapExpr) {
			ArrayAsMapExpr aam = (ArrayAsMapExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAsMap(");
			genExpressionTree(sb, aam.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayAsString) {
			ArrayAsString aas = (ArrayAsString)expr;
			sb.append("new GRGEN_EXPR.ArrayAsString(");
			genExpressionTree(sb, aas.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, aas.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArraySumExpr) {
			ArraySumExpr as = (ArraySumExpr)expr;
			sb.append("new GRGEN_EXPR.ArraySum(");
			genExpressionTree(sb, as.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayProdExpr) {
			ArrayProdExpr ap = (ArrayProdExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayProd(");
			genExpressionTree(sb, ap.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayMinExpr) {
			ArrayMinExpr am = (ArrayMinExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMin(");
			genExpressionTree(sb, am.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayMaxExpr) {
			ArrayMaxExpr am = (ArrayMaxExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMax(");
			genExpressionTree(sb, am.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayAvgExpr) {
			ArrayAvgExpr aa = (ArrayAvgExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAvg(");
			genExpressionTree(sb, aa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayMedExpr) {
			ArrayMedExpr am = (ArrayMedExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMed(");
			genExpressionTree(sb, am.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayMedUnsortedExpr) {
			ArrayMedUnsortedExpr amu = (ArrayMedUnsortedExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMedUnsorted(");
			genExpressionTree(sb, amu.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayVarExpr) {
			ArrayVarExpr av = (ArrayVarExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayVar(");
			genExpressionTree(sb, av.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayDevExpr) {
			ArrayDevExpr ad = (ArrayDevExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayDev(");
			genExpressionTree(sb, ad.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DequeSizeExpr) {
			DequeSizeExpr ds = (DequeSizeExpr)expr;
			sb.append("new GRGEN_EXPR.DequeSize(");
			genExpressionTree(sb, ds.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DequeEmptyExpr) {
			DequeEmptyExpr de = (DequeEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.DequeEmpty(");
			genExpressionTree(sb, de.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DequePeekExpr) {
			DequePeekExpr dp = (DequePeekExpr)expr;
			sb.append("new GRGEN_EXPR.DequePeek(");
			genExpressionTree(sb, dp.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(dp.getNumberExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, dp.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof DequeIndexOfExpr) {
			DequeIndexOfExpr di = (DequeIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.DequeIndexOf(");
			genExpressionTree(sb, di.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, di.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(di.getStartIndexExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, di.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof DequeLastIndexOfExpr) {
			DequeLastIndexOfExpr dli = (DequeLastIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.DequeLastIndexOf(");
			genExpressionTree(sb, dli.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, dli.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DequeSubdequeExpr) {
			DequeSubdequeExpr dsd = (DequeSubdequeExpr)expr;
			sb.append("new GRGEN_EXPR.DequeSubdeque(");
			genExpressionTree(sb, dsd.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, dsd.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, dsd.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DequeAsSetExpr) {
			DequeAsSetExpr das = (DequeAsSetExpr)expr;
			sb.append("new GRGEN_EXPR.DequeAsSet(");
			genExpressionTree(sb, das.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DequeAsArrayExpr) {
			DequeAsArrayExpr daa = (DequeAsArrayExpr)expr;
			sb.append("new GRGEN_EXPR.DequeAsArray(");
			genExpressionTree(sb, daa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MapInit) {
			MapInit mi = (MapInit)expr;
			if(mi.isConstant()) {
				sb.append("new GRGEN_EXPR.StaticMap(\"" + className + "\", \"" + mi.getAnonymousMapName() + "\")");
			} else {
				sb.append("new GRGEN_EXPR.MapConstructor(\"" + className + "\", \"" + mi.getAnonymousMapName() + "\",");
				int openParenthesis = 0;
				for(MapItem item : mi.getMapItems()) {
					sb.append("new GRGEN_EXPR.MapItem(");
					genExpressionTree(sb, item.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					if(item.getKeyExpr() instanceof GraphEntityExpression)
						sb.append("\"" + formatElementInterfaceRef(item.getKeyExpr().getType()) + "\", ");
					else
						sb.append("null, ");
					genExpressionTree(sb, item.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append("\"" + formatElementInterfaceRef(item.getValueExpr().getType()) + "\", ");
					else
						sb.append("null, ");
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
				sb.append("new GRGEN_EXPR.StaticSet(\"" + className + "\", \"" + si.getAnonymousSetName() + "\")");
			} else {
				sb.append("new GRGEN_EXPR.SetConstructor(\"" + className + "\", \"" + si.getAnonymousSetName() + "\", ");
				int openParenthesis = 0;
				for(SetItem item : si.getSetItems()) {
					sb.append("new GRGEN_EXPR.SetItem(");
					genExpressionTree(sb, item.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append("\"" + formatElementInterfaceRef(item.getValueExpr().getType()) + "\", ");
					else
						sb.append("null, ");
					++openParenthesis;
				}
				sb.append("null");
				for(int i=0; i<openParenthesis; ++i) sb.append(")");
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayInit) {
			ArrayInit ai = (ArrayInit)expr;
			if(ai.isConstant()) {
				sb.append("new GRGEN_EXPR.StaticArray(\"" + className + "\", \"" + ai.getAnonymousArrayName() + "\")");
			} else {
				sb.append("new GRGEN_EXPR.ArrayConstructor(\"" + className + "\", \"" + ai.getAnonymousArrayName() + "\", ");
				int openParenthesis = 0;
				for(ArrayItem item : ai.getArrayItems()) {
					sb.append("new GRGEN_EXPR.ArrayItem(");
					genExpressionTree(sb, item.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append("\"" + formatElementInterfaceRef(item.getValueExpr().getType()) + "\", ");
					else
						sb.append("null, ");
					++openParenthesis;
				}
				sb.append("null");
				for(int i=0; i<openParenthesis; ++i) sb.append(")");
				sb.append(")");
			}
		}
		else if (expr instanceof DequeInit) {
			DequeInit di = (DequeInit)expr;
			if(di.isConstant()) {
				sb.append("new GRGEN_EXPR.StaticDeque(\"" + className + "\", \"" + di.getAnonymousDequeName() + "\")");
			} else {
				sb.append("new GRGEN_EXPR.DequeConstructor(\"" + className + "\", \"" + di.getAnonymousDequeName() + "\", ");
				int openParenthesis = 0;
				for(DequeItem item : di.getDequeItems()) {
					sb.append("new GRGEN_EXPR.DequeItem(");
					genExpressionTree(sb, item.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
					sb.append(", ");
					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append("\"" + formatElementInterfaceRef(item.getValueExpr().getType()) + "\", ");
					else
						sb.append("null, ");
					++openParenthesis;
				}
				sb.append("null");
				for(int i=0; i<openParenthesis; ++i) sb.append(")");
				sb.append(")");
			}
		}
		else if (expr instanceof MapCopyConstructor) {
			MapCopyConstructor mcc = (MapCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.MapCopyConstructor(\"" + formatType(mcc.getMapType()) + "\", ");
			sb.append("\"" + formatSequenceType(mcc.getMapType().getKeyType()) + "\", ");
			sb.append("\"" + formatSequenceType(mcc.getMapType().getValueType()) + "\", ");
			genExpressionTree(sb, mcc.getMapToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SetCopyConstructor) {
			SetCopyConstructor scc = (SetCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.SetCopyConstructor(\"" + formatType(scc.getSetType()) + "\", ");
			sb.append("\"" + formatSequenceType(scc.getSetType().getValueType()) + "\", ");
			genExpressionTree(sb, scc.getSetToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArrayCopyConstructor) {
			ArrayCopyConstructor acc = (ArrayCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.ArrayCopyConstructor(\"" + formatType(acc.getArrayType()) + "\", ");
			sb.append("\"" + formatSequenceType(acc.getArrayType().getValueType()) + "\", ");
			genExpressionTree(sb, acc.getArrayToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DequeCopyConstructor) {
			DequeCopyConstructor dcc = (DequeCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.DequeCopyConstructor(\"" + formatType(dcc.getDequeType()) + "\", ");
			sb.append("\"" + formatSequenceType(dcc.getDequeType().getValueType()) + "\", ");
			genExpressionTree(sb, dcc.getDequeToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof FunctionInvocationExpr) {
			FunctionInvocationExpr fi = (FunctionInvocationExpr) expr;
			sb.append("new GRGEN_EXPR.FunctionInvocation(\"GRGEN_ACTIONS." + getPackagePrefixDot(fi.getFunction()) + "\", \"" + fi.getFunction().getIdent() + "\", new GRGEN_EXPR.Expression[] {");
			for(int i=0; i<fi.arity(); ++i) {
				Expression argument = fi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i=0; i<fi.arity(); ++i) {
				Expression argument = fi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("\"" + formatElementInterfaceRef(argument.getType()) + "\"");
				} else {
					sb.append("null");
				}
				sb.append(", ");
			}
			sb.append("}");
			sb.append(")");
		}
		else if (expr instanceof ExternalFunctionInvocationExpr) {
			ExternalFunctionInvocationExpr efi = (ExternalFunctionInvocationExpr)expr;
			sb.append("new GRGEN_EXPR.ExternalFunctionInvocation(\"" + efi.getExternalFunc().getIdent() + "\", new GRGEN_EXPR.Expression[] {");
			for(int i=0; i<efi.arity(); ++i) {
				Expression argument = efi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i=0; i<efi.arity(); ++i) {
				Expression argument = efi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("\"" + formatElementInterfaceRef(argument.getType()) + "\"");
				} else {
					sb.append("null");
				}
				sb.append(", ");
			}
			sb.append("}");
			sb.append(")");
		}
		else if (expr instanceof FunctionMethodInvocationExpr) {
			FunctionMethodInvocationExpr fmi = (FunctionMethodInvocationExpr) expr;
			sb.append("new GRGEN_EXPR.FunctionMethodInvocation(\"" + formatElementInterfaceRef(fmi.getOwner().getType()) + "\","
					+ " \"" + formatEntity(fmi.getOwner(), pathPrefix, alreadyDefinedEntityToName) + "\","
					+ " \"" + fmi.getFunction().getIdent() + "\", new GRGEN_EXPR.Expression[] {");
			for(int i=0; i<fmi.arity(); ++i) {
				Expression argument = fmi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i=0; i<fmi.arity(); ++i) {
				Expression argument = fmi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("\"" + formatElementInterfaceRef(argument.getType()) + "\"");
				} else {
					sb.append("null");
				}
				sb.append(", ");
			}
			sb.append("}");
			sb.append(")");
		}
		else if (expr instanceof ExternalFunctionMethodInvocationExpr) {
			ExternalFunctionMethodInvocationExpr efmi = (ExternalFunctionMethodInvocationExpr)expr;
			sb.append("new GRGEN_EXPR.ExternalFunctionMethodInvocation(");
			genExpressionTree(sb, efmi.getOwner(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + efmi.getExternalFunc().getIdent() + "\", new GRGEN_EXPR.Expression[] {");
			for(int i=0; i<efmi.arity(); ++i) {
				Expression argument = efmi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i=0; i<efmi.arity(); ++i) {
				Expression argument = efmi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("\"" + formatElementInterfaceRef(argument.getType()) + "\"");
				} else {
					sb.append("null");
				}
				sb.append(", ");
			}
			sb.append("}");
			sb.append(")");
		}
		else if (expr instanceof EdgesExpr) {
			EdgesExpr e = (EdgesExpr) expr;
			sb.append("new GRGEN_EXPR.Edges(");
			genExpressionTree(sb, e.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			sb.append(getDirectedness(e.getType()));
			sb.append(")");
		}
		else if (expr instanceof NodesExpr) {
			NodesExpr n = (NodesExpr) expr;
			sb.append("new GRGEN_EXPR.Nodes(");
			genExpressionTree(sb, n.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountEdgesExpr) {
			CountEdgesExpr ce = (CountEdgesExpr) expr;
			sb.append("new GRGEN_EXPR.CountEdges(");
			genExpressionTree(sb, ce.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountNodesExpr) {
			CountNodesExpr cn = (CountNodesExpr) expr;
			sb.append("new GRGEN_EXPR.CountNodes(");
			genExpressionTree(sb, cn.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof NowExpr) {
			//NowExpr n = (NowExpr) expr;
			sb.append("new GRGEN_EXPR.Now(");
			sb.append(")");
		}
		else if (expr instanceof EmptyExpr) {
			//EmptyExpr e = (EmptyExpr) expr;
			sb.append("new GRGEN_EXPR.Empty(");
			sb.append(")");
		}
		else if (expr instanceof SizeExpr) {
			//SizeExpr s = (SizeExpr) expr;
			sb.append("new GRGEN_EXPR.Size(");
			sb.append(")");
		}
		else if (expr instanceof SourceExpr) {
			SourceExpr s = (SourceExpr) expr;
			sb.append("new GRGEN_EXPR.Source(");
			genExpressionTree(sb, s.getEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof TargetExpr) {
			TargetExpr t = (TargetExpr) expr;
			sb.append("new GRGEN_EXPR.Target(");
			genExpressionTree(sb, t.getEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof OppositeExpr) {
			OppositeExpr o = (OppositeExpr) expr;
			sb.append("new GRGEN_EXPR.Opposite(");
			genExpressionTree(sb, o.getEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			genExpressionTree(sb, o.getNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof NodeByNameExpr) {
			NodeByNameExpr nbn = (NodeByNameExpr) expr;
			sb.append("new GRGEN_EXPR.NodeByName(");
			genExpressionTree(sb, nbn.getNameExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!nbn.getNodeTypeExpr().getType().getIdent().equals("Node")) {
				sb.append(", ");
				genExpressionTree(sb, nbn.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof EdgeByNameExpr) {
			EdgeByNameExpr ebn = (EdgeByNameExpr) expr;
			sb.append("new GRGEN_EXPR.EdgeByName(");
			genExpressionTree(sb, ebn.getNameExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!ebn.getEdgeTypeExpr().getType().getIdent().equals("AEdge")) {
				sb.append(", ");
				genExpressionTree(sb, ebn.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof NodeByUniqueExpr) {
			NodeByUniqueExpr nbu = (NodeByUniqueExpr) expr;
			sb.append("new GRGEN_EXPR.NodeByUnique(");
			genExpressionTree(sb, nbu.getUniqueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!nbu.getNodeTypeExpr().getType().getIdent().equals("Node")) {
				sb.append(", ");
				genExpressionTree(sb, nbu.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof EdgeByUniqueExpr) {
			EdgeByUniqueExpr ebu = (EdgeByUniqueExpr) expr;
			sb.append("new GRGEN_EXPR.EdgeByUnique(");
			genExpressionTree(sb, ebu.getUniqueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!ebu.getEdgeTypeExpr().getType().getIdent().equals("AEdge")) {
				sb.append(", ");
				genExpressionTree(sb, ebu.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr ie = (IncidentEdgeExpr) expr;
			if(ie.Direction()==IncidentEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.Outgoing(");
			} else if(ie.Direction()==IncidentEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.Incoming(");
			} else {
				sb.append("new GRGEN_EXPR.Incident(");
			}
			genExpressionTree(sb, ie.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ie.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ie.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			sb.append(getDirectedness(ie.getType()));
			sb.append(")");
		}
		else if (expr instanceof AdjacentNodeExpr) {
			AdjacentNodeExpr an = (AdjacentNodeExpr) expr;
			if(an.Direction()==AdjacentNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.AdjacentOutgoing(");
			} else if(an.Direction()==AdjacentNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.AdjacentIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.Adjacent(");
			}
			genExpressionTree(sb, an.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, an.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, an.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountIncidentEdgeExpr) {
			CountIncidentEdgeExpr cie = (CountIncidentEdgeExpr) expr;
			if(cie.Direction()==CountIncidentEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountOutgoing(");
			} else if(cie.Direction()==CountIncidentEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.CountIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.CountIncident(");
			}
			genExpressionTree(sb, cie.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cie.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cie.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountAdjacentNodeExpr) {
			CountAdjacentNodeExpr can = (CountAdjacentNodeExpr) expr;
			if(can.Direction()==CountAdjacentNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountAdjacentOutgoing(");
			} else if(can.Direction()==CountAdjacentNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.CountAdjacentIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.CountAdjacent(");
			}
			genExpressionTree(sb, can.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, can.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, can.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof IsAdjacentNodeExpr) {
			IsAdjacentNodeExpr ian = (IsAdjacentNodeExpr) expr;
			if(ian.Direction()==IsAdjacentNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsAdjacentOutgoing(");
			} else if(ian.Direction()==IsAdjacentNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.IsAdjacentIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.IsAdjacent(");
			}
			genExpressionTree(sb, ian.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ian.getEndNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ian.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ian.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof IsIncidentEdgeExpr) {
			IsIncidentEdgeExpr iie = (IsIncidentEdgeExpr) expr;
			if(iie.Direction()==IsIncidentEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsOutgoing(");
			} else if(iie.Direction()==IsIncidentEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.IsIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.IsIncident(");
			}
			genExpressionTree(sb, iie.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, iie.getEndEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, iie.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, iie.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ReachableEdgeExpr) {
			ReachableEdgeExpr re = (ReachableEdgeExpr) expr;
			if(re.Direction()==ReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.ReachableEdgesOutgoing(");
			} else if(re.Direction()==ReachableEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.ReachableEdgesIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.ReachableEdges(");
			}
			genExpressionTree(sb, re.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, re.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, re.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			sb.append(getDirectedness(re.getType()));
			sb.append(")");
		}
		else if (expr instanceof ReachableNodeExpr) {
			ReachableNodeExpr rn = (ReachableNodeExpr) expr;
			if(rn.Direction()==ReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.ReachableOutgoing(");
			} else if(rn.Direction()==ReachableNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.ReachableIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.Reachable(");
			}
			genExpressionTree(sb, rn.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, rn.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, rn.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountReachableEdgeExpr) {
			CountReachableEdgeExpr cre = (CountReachableEdgeExpr) expr;
			if(cre.Direction()==CountReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountReachableEdgesOutgoing(");
			} else if(cre.Direction()==CountReachableEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.CountReachableEdgesIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.CountReachableEdges(");
			}
			genExpressionTree(sb, cre.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cre.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cre.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountReachableNodeExpr) {
			CountReachableNodeExpr crn = (CountReachableNodeExpr) expr;
			if(crn.Direction()==CountReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountReachableOutgoing(");
			} else if(crn.Direction()==CountReachableNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.CountReachableIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.CountReachable(");
			}
			genExpressionTree(sb, crn.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, crn.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, crn.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof IsReachableNodeExpr) {
			IsReachableNodeExpr irn = (IsReachableNodeExpr) expr;
			if(irn.Direction()==IsReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsReachableOutgoing(");
			} else if(irn.Direction()==IsReachableNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.IsReachableIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.IsReachable(");
			}
			genExpressionTree(sb, irn.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, irn.getEndNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, irn.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, irn.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof IsReachableEdgeExpr) {
			IsReachableEdgeExpr ire = (IsReachableEdgeExpr) expr;
			if(ire.Direction()==IsReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsReachableEdgesOutgoing(");
			} else if(ire.Direction()==IsReachableEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.IsReachableEdgesIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.IsReachableEdges(");
			}
			genExpressionTree(sb, ire.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ire.getEndEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ire.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ire.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof BoundedReachableEdgeExpr) {
			BoundedReachableEdgeExpr bre = (BoundedReachableEdgeExpr) expr;
			if(bre.Direction()==BoundedReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.BoundedReachableEdgesOutgoing(");
			} else if(bre.Direction()==BoundedReachableEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.BoundedReachableEdgesIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.BoundedReachableEdges(");
			}
			genExpressionTree(sb, bre.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, bre.getDepthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, bre.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, bre.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			sb.append(getDirectedness(bre.getType()));
			sb.append(")");
		}
		else if (expr instanceof BoundedReachableNodeExpr) {
			BoundedReachableNodeExpr brn = (BoundedReachableNodeExpr) expr;
			if(brn.Direction()==BoundedReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.BoundedReachableOutgoing(");
			} else if(brn.Direction()==BoundedReachableNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.BoundedReachableIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.BoundedReachable(");
			}
			genExpressionTree(sb, brn.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, brn.getDepthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, brn.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, brn.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof BoundedReachableNodeWithRemainingDepthExpr) {
			BoundedReachableNodeWithRemainingDepthExpr brnwrd = (BoundedReachableNodeWithRemainingDepthExpr) expr;
			if(brnwrd.Direction()==BoundedReachableNodeWithRemainingDepthExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.BoundedReachableWithRemainingDepthOutgoing(");
			} else if(brnwrd.Direction()==BoundedReachableNodeWithRemainingDepthExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.BoundedReachableWithRemainingDepthIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.BoundedReachableWithRemainingDepth(");
			}
			genExpressionTree(sb, brnwrd.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, brnwrd.getDepthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, brnwrd.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, brnwrd.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountBoundedReachableEdgeExpr) {
			CountBoundedReachableEdgeExpr cbre = (CountBoundedReachableEdgeExpr) expr;
			if(cbre.Direction()==CountBoundedReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountBoundedReachableEdgesOutgoing(");
			} else if(cbre.Direction()==CountBoundedReachableEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.CountBoundedReachableEdgesIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.CountBoundedReachableEdges(");
			}
			genExpressionTree(sb, cbre.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cbre.getDepthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cbre.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cbre.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CountBoundedReachableNodeExpr) {
			CountBoundedReachableNodeExpr cbrn = (CountBoundedReachableNodeExpr) expr;
			if(cbrn.Direction()==CountBoundedReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountBoundedReachableOutgoing(");
			} else if(cbrn.Direction()==CountBoundedReachableNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.CountBoundedReachableIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.CountBoundedReachable(");
			}
			genExpressionTree(sb, cbrn.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cbrn.getDepthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cbrn.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, cbrn.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof IsBoundedReachableNodeExpr) {
			IsBoundedReachableNodeExpr ibrn = (IsBoundedReachableNodeExpr) expr;
			if(ibrn.Direction()==IsBoundedReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsBoundedReachableOutgoing(");
			} else if(ibrn.Direction()==IsBoundedReachableNodeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.IsBoundedReachableIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.IsBoundedReachable(");
			}
			genExpressionTree(sb, ibrn.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibrn.getEndNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibrn.getDepthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibrn.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibrn.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof IsBoundedReachableEdgeExpr) {
			IsBoundedReachableEdgeExpr ibre = (IsBoundedReachableEdgeExpr) expr;
			if(ibre.Direction()==IsBoundedReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsBoundedReachableEdgesOutgoing(");
			} else if(ibre.Direction()==IsBoundedReachableEdgeExpr.INCOMING) {
				sb.append("new GRGEN_EXPR.IsBoundedReachableEdgesIncoming(");
			} else {
				sb.append("new GRGEN_EXPR.IsBoundedReachableEdges(");
			}
			genExpressionTree(sb, ibre.getStartNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibre.getEndEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibre.getDepthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibre.getIncidentEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ibre.getAdjacentNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof InducedSubgraphExpr) {
			InducedSubgraphExpr is = (InducedSubgraphExpr) expr;
			sb.append("new GRGEN_EXPR.InducedSubgraph(");
			genExpressionTree(sb, is.getSetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof DefinedSubgraphExpr) {
			DefinedSubgraphExpr ds = (DefinedSubgraphExpr) expr;
			sb.append("new GRGEN_EXPR.DefinedSubgraph(");
			genExpressionTree(sb, ds.getSetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			sb.append(getDirectedness(ds.getSetExpr().getType()));
			sb.append(")");
		}
		else if (expr instanceof EqualsAnyExpr) {
			EqualsAnyExpr ea = (EqualsAnyExpr) expr;
			sb.append("new GRGEN_EXPR.EqualsAny(");
			genExpressionTree(sb, ea.getSubgraphExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ea.getSetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			sb.append(ea.getIncludingAttributes() ? "true" : "false");
			sb.append(")");
		}
		else if (expr instanceof MaxExpr) {
			MaxExpr m = (MaxExpr) expr;
			sb.append("new GRGEN_EXPR.Max(");
			genExpressionTree(sb, m.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, m.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof MinExpr) {
			MinExpr m = (MinExpr) expr;
			sb.append("new GRGEN_EXPR.Min(");
			genExpressionTree(sb, m.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, m.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof AbsExpr) {
			AbsExpr a = (AbsExpr) expr;
			sb.append("new GRGEN_EXPR.Abs(");
			genExpressionTree(sb, a.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SgnExpr) {
			SgnExpr s = (SgnExpr) expr;
			sb.append("new GRGEN_EXPR.Sgn(");
			genExpressionTree(sb, s.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof PiExpr) {
			//PiExpr pi = (PiExpr) expr;
			sb.append("new GRGEN_EXPR.Pi(");
			sb.append(")");
		}
		else if (expr instanceof EExpr) {
			//EExpr e = (EExpr) expr;
			sb.append("new GRGEN_EXPR.E(");
			sb.append(")");
		}
		else if (expr instanceof ByteMinExpr) {
			sb.append("new GRGEN_EXPR.ByteMin(");
			sb.append(")");
		}
		else if (expr instanceof ByteMaxExpr) {
			sb.append("new GRGEN_EXPR.ByteMax(");
			sb.append(")");
		}
		else if (expr instanceof ShortMinExpr) {
			sb.append("new GRGEN_EXPR.ShortMin(");
			sb.append(")");
		}
		else if (expr instanceof ShortMaxExpr) {
			sb.append("new GRGEN_EXPR.ShortMax(");
			sb.append(")");
		}
		else if (expr instanceof IntMinExpr) {
			sb.append("new GRGEN_EXPR.IntMin(");
			sb.append(")");
		}
		else if (expr instanceof IntMaxExpr) {
			sb.append("new GRGEN_EXPR.IntMax(");
			sb.append(")");
		}
		else if (expr instanceof LongMinExpr) {
			sb.append("new GRGEN_EXPR.LongMin(");
			sb.append(")");
		}
		else if (expr instanceof LongMaxExpr) {
			sb.append("new GRGEN_EXPR.LongMax(");
			sb.append(")");
		}
		else if (expr instanceof FloatMinExpr) {
			sb.append("new GRGEN_EXPR.FloatMin(");
			sb.append(")");
		}
		else if (expr instanceof FloatMaxExpr) {
			sb.append("new GRGEN_EXPR.FloatMax(");
			sb.append(")");
		}
		else if (expr instanceof DoubleMinExpr) {
			sb.append("new GRGEN_EXPR.DoubleMin(");
			sb.append(")");
		}
		else if (expr instanceof DoubleMaxExpr) {
			sb.append("new GRGEN_EXPR.DoubleMax(");
			sb.append(")");
		}
		else if (expr instanceof CeilExpr) {
			CeilExpr c = (CeilExpr) expr;
			sb.append("new GRGEN_EXPR.Ceil(");
			genExpressionTree(sb, c.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof FloorExpr) {
			FloorExpr f = (FloorExpr) expr;
			sb.append("new GRGEN_EXPR.Floor(");
			genExpressionTree(sb, f.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof RoundExpr) {
			RoundExpr r = (RoundExpr) expr;
			sb.append("new GRGEN_EXPR.Round(");
			genExpressionTree(sb, r.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof TruncateExpr) {
			TruncateExpr t = (TruncateExpr) expr;
			sb.append("new GRGEN_EXPR.Truncate(");
			genExpressionTree(sb, t.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SinCosTanExpr) {
			SinCosTanExpr sct = (SinCosTanExpr)expr;
			switch(sct.getWhich()) {
			case SinCosTanExpr.SIN:
				sb.append("new GRGEN_EXPR.Sin(");
				break;
			case SinCosTanExpr.COS:
				sb.append("new GRGEN_EXPR.Cos(");
				break;
			case SinCosTanExpr.TAN:
				sb.append("new GRGEN_EXPR.Tan(");
				break;
			}
			genExpressionTree(sb, sct.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof ArcSinCosTanExpr) {
			ArcSinCosTanExpr asct = (ArcSinCosTanExpr)expr;
			switch(asct.getWhich()) {
			case ArcSinCosTanExpr.ARC_SIN:
				sb.append("new GRGEN_EXPR.ArcSin(");
				break;
			case ArcSinCosTanExpr.ARC_COS:
				sb.append("new GRGEN_EXPR.ArcCos(");
				break;
			case ArcSinCosTanExpr.ARC_TAN:
				sb.append("new GRGEN_EXPR.ArcTan(");
				break;
			}
			genExpressionTree(sb, asct.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof CanonizeExpr) {
			CanonizeExpr c = (CanonizeExpr) expr;
			sb.append("new GRGEN_EXPR.Canonize(");
			genExpressionTree(sb, c.getGraphExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SqrExpr) {
			SqrExpr s = (SqrExpr) expr;
			sb.append("new GRGEN_EXPR.Sqr(");
			genExpressionTree(sb, s.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof SqrtExpr) {
			SqrtExpr s = (SqrtExpr) expr;
			sb.append("new GRGEN_EXPR.Sqrt(");
			genExpressionTree(sb, s.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof PowExpr) {
			PowExpr p = (PowExpr) expr;
			sb.append("new GRGEN_EXPR.Pow(");
			if(p.getLeftExpr()!=null) {
				genExpressionTree(sb, p.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			genExpressionTree(sb, p.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		}
		else if (expr instanceof LogExpr) {
			LogExpr l = (LogExpr) expr;
			sb.append("new GRGEN_EXPR.Log(");
			genExpressionTree(sb, l.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(l.getRightExpr()!=null) {
				sb.append(", ");
				genExpressionTree(sb, l.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		}
		else if (expr instanceof IteratedQueryExpr) {
			IteratedQueryExpr iq = (IteratedQueryExpr) expr;
			sb.append("new GRGEN_EXPR.IteratedQuery(");
			sb.append("\"" + iq.getIteratedName().toString() + "\"");
			sb.append(")");
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
		for(Rule iter : pattern.getIters()) {
			PatternGraph iterPattern = iter.getLeft();
			max = computePriosMax(max, iterPattern);
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

	private void appendPrio(SourceBuilder sb, Entity entity, double max) {
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

	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState) {
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		genQualAccess(sb, owner, member);
	}

	protected void genQualAccess(SourceBuilder sb, Entity owner, Entity member) {
		sb.append("((I" + getNodeOrEdgeTypePrefix(owner) +
					  formatIdentifiable(owner.getType()) + ") ");
		sb.append(formatEntity(owner) + ").@" + formatIdentifiable(member));
	}

	protected void genMemberAccess(SourceBuilder sb, Entity member) {
		throw new UnsupportedOperationException("Member expressions not allowed in actions!");
	}

	/////////////////////////////////////////////
	// Static constructor calling static inits //
	/////////////////////////////////////////////

	protected void genStaticConstructor(SourceBuilder sb, String className, List<String> staticInitializers)
	{
		sb.append("\n");
		sb.appendFront("static " + className + "() {\n");
		for(String staticInit : staticInitializers) {
			sb.appendFront("\t" + staticInit + "();\n");
		}
		sb.appendFront("}\n");
		sb.append("\n");
	}

	//////////////////////////////
	// Match objects generation //
	//////////////////////////////

	private void genPatternMatchInterface(SourceBuilder sb, PatternGraph pattern, String name,
			String base, String pathPrefixForElements, boolean iterated, boolean alternativeCase,
			boolean matchClass, HashSet<String> elementsAlreadyDeclared)
	{
		genMatchInterface(sb, pattern, name,
				base, pathPrefixForElements, iterated, alternativeCase,
				matchClass, elementsAlreadyDeclared);

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			genPatternMatchInterface(sb, neg, pathPrefixForElements+negName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + negName + "_",
					false, false, false, elementsAlreadyDeclared);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			genPatternMatchInterface(sb, idpt, pathPrefixForElements+idptName,
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
						"IMatch_"+pathPrefixForElements+altName,
						pathPrefixForElements + altName + "_" + altCasePattern.getNameOfGraph() + "_",
						false, true, false, elementsAlreadyDeclared);
			}
		}

		for(Rule iter : pattern.getIters()) {
			PatternGraph iterPattern = iter.getLeft();
			String iterName = iterPattern.getNameOfGraph();
			genPatternMatchInterface(sb, iterPattern, pathPrefixForElements+iterName,
					"GRGEN_LIBGR.IMatch", pathPrefixForElements + iterName + "_",
					true, false, false, elementsAlreadyDeclared);
		}
	}

	private void genPatternMatchImplementation(SourceBuilder sb, PatternGraph pattern, String name,
			String patGraphVarName, String className,
			String pathPrefixForElements, 
			boolean iterated, boolean independent, boolean parallelized)
	{
		genMatchImplementation(sb, pattern, name,
				patGraphVarName, className, pathPrefixForElements, 
				iterated, independent, parallelized);

		for(PatternGraph neg : pattern.getNegs()) {
			String negName = neg.getNameOfGraph();
			genPatternMatchImplementation(sb, neg, pathPrefixForElements+negName,
					pathPrefixForElements+negName, className,
					pathPrefixForElements+negName+"_", false, false, false);
		}

		for(PatternGraph idpt : pattern.getIdpts()) {
			String idptName = idpt.getNameOfGraph();
			genPatternMatchImplementation(sb, idpt, pathPrefixForElements+idptName,
					pathPrefixForElements+idptName, className,
					pathPrefixForElements+idptName+"_", false, true, false);
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
			genPatternMatchImplementation(sb, iterPattern, pathPrefixForElements+iterName,
					pathPrefixForElements+iterName, className,
					pathPrefixForElements+iterName+"_", true, false, false);
		}
	}

	private void genMatchInterface(SourceBuilder sb, PatternGraph pattern,
			String name, String base,
			String pathPrefixForElements, boolean iterated, boolean alternativeCase,
			boolean matchClass, HashSet<String> elementsAlreadyDeclared)
	{
		String interfaceName = "IMatch_" + name;
		sb.appendFront("public interface "+interfaceName+" : "+base+"\n");
		sb.appendFront("{\n");
		sb.indent();

		for(int i=MATCH_PART_NODES; i<MATCH_PART_END; ++i) {
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

	private void genAlternativeMatchInterface(SourceBuilder sb, String name)
	{
		String interfaceName = "IMatch_" + name;
		sb.appendFront("public interface "+interfaceName+" : GRGEN_LIBGR.IMatch\n");
		sb.appendFront("{\n");

		sb.appendFront("\tvoid SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");

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
		sb.appendFront("public class "+className+" : GRGEN_LGSP.MatchListElement<"+className+">, "+interfaceName+"\n");
		sb.appendFront("{\n");
		sb.indent();

		for(int i=MATCH_PART_NODES; i<MATCH_PART_END; ++i) {
			genMatchedEntitiesImplementation(sb, pattern, name,
					i, pathPrefixForElements);
			genMatchEnum(sb, pattern, name,
					i, pathPrefixForElements);
			genIMatchImplementation(sb, pattern, name,
					i, pathPrefixForElements);
			sb.append("\n");
		}

		sb.appendFront("public override GRGEN_LIBGR.IPatternGraph Pattern { get { return "+ruleClassName+".instance."+patGraphVarName+"; } }\n");
		if(iterated) {
			sb.appendFront("public bool IsNullMatch { get { return _isNullMatch; } }\n");
			sb.appendFront("public bool _isNullMatch;\n");
		}
		sb.appendFront("public override GRGEN_LIBGR.IMatch Clone() { return new "+className+"(this); }\n");
		sb.appendFront("public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }\n");

		sb.appendFront("public "+className+" nextWithSameHash;\n");
		
		genCleanNextWithSameHash(sb, className);

		if(parallelized)
			sb.appendFront("public int duplicateMatchHash;\n");
		
		genCopyConstructor(sb, pattern, name, pathPrefixForElements, className);
		
		sb.appendFront("public "+className+"()\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.append("\n");
		
		genIsEqualMethod(sb, pattern, name, pathPrefixForElements, className);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genCleanNextWithSameHash(SourceBuilder sb, String className) {
		sb.appendFront("public void CleanNextWithSameHash() {\n");
		sb.indent();
		sb.appendFront(className+" cur = this;\n");
		sb.appendFront("while(cur != null) {\n");
		sb.indent();
		sb.appendFront(className+" next = cur.nextWithSameHash;\n");
		sb.appendFront("cur.nextWithSameHash = null;\n");
		sb.appendFront("cur = next;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genCopyConstructor(SourceBuilder sb, PatternGraph pattern, String name, String pathPrefixForElements,
			String className) {
		sb.appendFront("public void CopyMatchContent("+className +" that)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(int i=MATCH_PART_NODES; i<MATCH_PART_END; ++i) {
			genCopyMatchedEntities(sb, pattern, name, i, pathPrefixForElements);
		}
		sb.unindent();
		sb.appendFront("}\n");

		sb.append("\n");

		sb.appendFront("public "+className+"("+className +" that)\n");
		sb.appendFront("{\n");
		sb.appendFront("\tCopyMatchContent(that);\n");
		sb.appendFront("}\n");
	}

	private void genIsEqualMethod(SourceBuilder sb, PatternGraph pattern, String name, String pathPrefixForElements,
			String className) {
		sb.appendFront("public bool IsEqual("+className +" that)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(that==null) return false;\n");
		for(int i=MATCH_PART_NODES; i<MATCH_PART_END; ++i) {
			genEqualMatch(sb, pattern, name, i, pathPrefixForElements);
		}
		sb.appendFront("return true;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genMatchedEntitiesInterface(SourceBuilder sb, PatternGraph pattern, HashSet<String> elementsAlreadyDeclared,
			String name, int which, String pathPrefixForElements)
	{
		// the getters for the elements
		sb.appendFront("//"+matchedEntitiesNamePlural(which)+"\n");
		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				String newPrefix = elementsAlreadyDeclared.contains(formatEntity(node)) ? "new " : "";
				sb.appendFront(newPrefix+formatElementInterfaceRef(node.getType())+" "+formatEntity(node)+" { get; set; }\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				String newPrefix = elementsAlreadyDeclared.contains(formatEntity(edge)) ? "new " : "";
				sb.appendFront(newPrefix+formatElementInterfaceRef(edge.getType())+" "+formatEntity(edge)+" { get; set; }\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				String newPrefix = elementsAlreadyDeclared.contains(formatEntity(var)) ? "new " : "";
				sb.appendFront(newPrefix+formatAttributeType(var.getType())+" @"+formatEntity(var)+" { get; set; }\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("@"+matchType(sub.getSubpatternAction().getPattern(), sub.getSubpatternAction(), true, "")+" @"+formatIdentifiable(sub)+" { get; }\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("IMatch_"+pathPrefixForElements+altName+" "+altName+" { get; }\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("GRGEN_LIBGR.IMatchesExact<IMatch_"+pathPrefixForElements+iterName+"> "+iterName+" { get; }\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("IMatch_"+pathPrefixForElements+idptName+" "+idptName+" { get; }\n");
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
		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.appendFront("public "+formatElementInterfaceRef(node.getType())+" "+formatEntity(node)
						+ " { " 
						+ "get { return ("+formatElementInterfaceRef(node.getType())+")"+formatEntity(node, "_")+"; } "
						+ "set { "+formatEntity(node, "_")+" = (GRGEN_LGSP.LGSPNode)value; }"
						+ " }\n");
			}
			for(Node node : pattern.getNodes()) {
				sb.appendFront("public GRGEN_LGSP.LGSPNode "+formatEntity(node, "_")+";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.appendFront("public "+formatElementInterfaceRef(edge.getType())+" "+formatEntity(edge)
						+ " { "
						+ "get { return ("+formatElementInterfaceRef(edge.getType())+")"+formatEntity(edge, "_")+"; } "
						+ "set { "+formatEntity(edge, "_")+" = (GRGEN_LGSP.LGSPEdge)value; }"
						+ " }\n");
			}
			for(Edge edge : pattern.getEdges()) {
				sb.appendFront("public GRGEN_LGSP.LGSPEdge "+formatEntity(edge, "_")+";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.appendFront("public "+formatAttributeType(var.getType())+" "+formatEntity(var)
						+ " { "
						+ "get { return "+formatEntity(var, "_")+"; } "
						+ "set { "+formatEntity(var, "_")+" = value; }"
						+ " }\n");
			}
			for(Variable var : pattern.getVars()) {
				sb.appendFront("public "+formatAttributeType(var.getType())+" "+formatEntity(var, "_")+";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("public @"+matchType(sub.getSubpatternAction().getPattern(), sub.getSubpatternAction(), true, "")+" @"+formatIdentifiable(sub)+" { get { return @"+formatIdentifiable(sub, "_")+"; } }\n");
			}
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("public @"+matchType(sub.getSubpatternAction().getPattern(), sub.getSubpatternAction(), true, "")+" @"+formatIdentifiable(sub, "_")+";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("public IMatch_"+pathPrefixForElements+altName+" "+altName+" { get { return _"+altName+"; } }\n");
			}
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("public IMatch_"+pathPrefixForElements+altName+" _"+altName+";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("public GRGEN_LIBGR.IMatchesExact<IMatch_"+pathPrefixForElements+iterName+"> "+iterName+" { get { return _"+iterName+"; } }\n");
			}
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("public GRGEN_LGSP.LGSPMatchesList<Match_"+pathPrefixForElements+iterName+", IMatch_"+pathPrefixForElements+iterName+"> _"+iterName+";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("public IMatch_"+pathPrefixForElements+idptName+" "+idptName+" { get { return _"+idptName+"; } }\n");
			}
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("public IMatch_"+pathPrefixForElements+idptName+" _"+idptName+";\n");
			}
			break;
		default:
			assert(false);
		}
	}

	private void genCopyMatchedEntities(SourceBuilder sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				String nodeName = formatEntity(node, "_");
				sb.appendFront(nodeName+" = that."+nodeName+";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				String edgeName = formatEntity(edge, "_");
				sb.appendFront(edgeName+" = that."+edgeName+";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				String varName = formatEntity(var, "_");
				sb.appendFront(varName+" = that."+varName+";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				String subName = "@" + formatIdentifiable(sub, "_");
				sb.appendFront(subName+" = that."+subName+";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = "_" + alt.getNameOfGraph();
				sb.appendFront(altName+" = that."+altName+";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = "_" + iter.getLeft().getNameOfGraph();
				sb.appendFront(iterName+" = that."+iterName+";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = "_" + idpt.getNameOfGraph();
				sb.appendFront(idptName+" = that."+idptName+";\n");
			}
			break;
		default:
			assert(false);
		}
	}

	private void genEqualMatch(SourceBuilder sb, PatternGraph pattern,
			String name, int which, String pathPrefixForElements)
	{
		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				if(node.isDefToBeYieldedTo())
					continue;
				String nodeName = formatEntity(node, "_");
				sb.appendFront("if("+nodeName+" != that."+nodeName+") return false;\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				if(edge.isDefToBeYieldedTo())
					continue;
				String edgeName = formatEntity(edge, "_");
				sb.appendFront("if("+edgeName+" != that."+edgeName+") return false;\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				if(var.isDefToBeYieldedTo())
					continue;
				String varName = formatEntity(var, "_");
				sb.appendFront("if("+varName+" != that."+varName+") return false;\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				String subName = "@" + formatIdentifiable(sub, "_");
				sb.appendFront("if(!"+subName+".IsEqual(that."+subName+")) return false;\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = "_" + alt.getNameOfGraph();
				for(Rule altCase : alt.getAlternativeCases()) {
					PatternGraph altCasePattern = altCase.getLeft();
					sb.appendFront("if("+altName+" is Match_"+name+altName+"_"+altCasePattern.getNameOfGraph()+" && !("+altName+" as Match_"+name+altName+"_"+altCasePattern.getNameOfGraph()+").IsEqual(that."+altName+" as Match_"+name+altName+"_"+altCasePattern.getNameOfGraph()+")) return false;\n");
				}
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = "_" + iter.getLeft().getNameOfGraph();
				sb.appendFront("if("+iterName+".Count != that."+iterName+".Count) return false;\n");
				sb.appendFront("IEnumerator<GRGEN_LIBGR.IMatch> "+iterName+"_thisEnumerator = "+iterName+".GetEnumerator();\n");
				sb.appendFront("IEnumerator<GRGEN_LIBGR.IMatch> "+iterName+"_thatEnumerator = that."+iterName+".GetEnumerator();\n");
				sb.appendFront("while("+iterName+"_thisEnumerator.MoveNext())\n");
				sb.appendFront("{\n");
				sb.indent();
				sb.appendFront(iterName+"_thatEnumerator.MoveNext();\n");
				sb.append("if(!("+iterName+"_thisEnumerator.Current as Match_"+name+iterName+").IsEqual("+iterName+"_thatEnumerator.Current as Match_"+name+iterName+")) return false;\n");
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

		String enumerableName = "GRGEN_LGSP."+matchedEntitiesNamePlural(which)+"_Enumerable";
		String enumeratorName = "GRGEN_LGSP."+matchedEntitiesNamePlural(which)+"_Enumerator";
		String typeOfMatchedEntities = typeOfMatchedEntities(which);
		int numberOfMatchedEntities = numOfMatchedEntities(which, pattern);
		String matchedEntitiesNameSingular = matchedEntitiesNameSingular(which);
		String matchedEntitiesNamePlural = matchedEntitiesNamePlural(which);

		sb.appendFront("public override IEnumerable<"+typeOfMatchedEntities+"> "+matchedEntitiesNamePlural+" { get { return new "+enumerableName+"(this); } }\n");
		sb.appendFront("public override IEnumerator<"+typeOfMatchedEntities+"> "+matchedEntitiesNamePlural+"Enumerator { get { return new " + enumeratorName + "(this); } }\n");
		sb.appendFront("public override int NumberOf"+matchedEntitiesNamePlural+" { get { return " + numberOfMatchedEntities + ";} }\n");

	    // -----------------------------

		sb.appendFront("public override "+typeOfMatchedEntities+" get"+matchedEntitiesNameSingular+"At(int index)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(index) {\n");

		switch(which)
		{
		case MATCH_PART_NODES:
			for(Node node : pattern.getNodes()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(node) + ": return " + formatEntity(node, "_") + ";\n");
			}
			break;
		case MATCH_PART_EDGES:
			for(Edge edge : pattern.getEdges()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(edge) + ": return " + formatEntity(edge, "_") + ";\n");
			}
			break;
		case MATCH_PART_VARIABLES:
			for(Variable var : pattern.getVars()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(var) + ": return " + formatEntity(var, "_") + ";\n");
			}
			break;
		case MATCH_PART_EMBEDDED_GRAPHS:
			for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + formatIdentifiable(sub) + ": return " + formatIdentifiable(sub, "_") + ";\n");
			}
			break;
		case MATCH_PART_ALTERNATIVES:
			for(Alternative alt : pattern.getAlts()) {
				String altName = alt.getNameOfGraph();
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + altName + ": return _" + altName+ ";\n");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				String iterName = iter.getLeft().getNameOfGraph();
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + iterName + ": return _" + iterName + ";\n");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				String idptName = idpt.getNameOfGraph();
				sb.appendFront("case (int)" + entitiesEnumName(which, pathPrefixForElements) + ".@" + idptName + ": return _" + idptName + ";\n");
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
	    
		sb.appendFront("public override "+typeOfMatchedEntities+" get"+matchedEntitiesNameSingular+"(string name)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(name) {\n");

		switch(which)
		{
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
				sb.appendFront("case \"" + altName + "\": return _" + altName+ ";\n");
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
		sb.appendFront("public enum "+entitiesEnumName(which, pathPrefixForElements)+" { ");
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
			for(Alternative alt : pattern.getAlts()) {
				sb.append("@"+alt.getNameOfGraph()+", ");
			}
			break;
		case MATCH_PART_ITERATEDS:
			for(Rule iter : pattern.getIters()) {
				sb.append("@"+iter.getLeft().getNameOfGraph()+", ");
			}
			break;
		case MATCH_PART_INDEPENDENTS:
			for(PatternGraph idpt : pattern.getIdpts()) {
				sb.append("@"+idpt.getNameOfGraph()+", ");
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

	////////////////////////////////////
	// Yielding assignment generation //
	////////////////////////////////////

	private void genYield(SourceBuilder sb, EvalStatement evalStmt, String className,
			String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		if(evalStmt instanceof AssignmentVarIndexed) { // must come before AssignmentVar
			genAssignmentVarIndexed(sb, (AssignmentVarIndexed) evalStmt, 
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof AssignmentVar) {
			genAssignmentVar(sb, (AssignmentVar) evalStmt, 
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof AssignmentGraphEntity) {
			genAssignmentGraphEntity(sb, (AssignmentGraphEntity) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof AssignmentIdentical) {
			//nothing to generate, was assignment . = . optimized away;
		}
		else if(evalStmt instanceof CompoundAssignmentVarChangedVar) {
			genCompoundAssignmentVarChangedVar(sb, (CompoundAssignmentVarChangedVar) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof CompoundAssignmentVar) { // must come after the changed versions
			genCompoundAssignmentVar(sb, (CompoundAssignmentVar) evalStmt, "\t\t\t\t",
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof MapVarRemoveItem) {
			genMapVarRemoveItem(sb, (MapVarRemoveItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} 
		else if(evalStmt instanceof MapVarClear) {
			genMapVarClear(sb, (MapVarClear) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} 
		else if(evalStmt instanceof MapVarAddItem) {
			genMapVarAddItem(sb, (MapVarAddItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof SetVarRemoveItem) {
			genSetVarRemoveItem(sb, (SetVarRemoveItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof SetVarClear) {
			genSetVarClear(sb, (SetVarClear) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof SetVarAddItem) {
			genSetVarAddItem(sb, (SetVarAddItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ArrayVarRemoveItem) {
			genArrayVarRemoveItem(sb, (ArrayVarRemoveItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ArrayVarClear) {
			genArrayVarClear(sb, (ArrayVarClear) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ArrayVarAddItem) {
			genArrayVarAddItem(sb, (ArrayVarAddItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DequeVarRemoveItem) {
			genDequeVarRemoveItem(sb, (DequeVarRemoveItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DequeVarClear) {
			genDequeVarClear(sb, (DequeVarClear) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DequeVarAddItem) {
			genDequeVarAddItem(sb, (DequeVarAddItem) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof IteratedAccumulationYield) {
			genIteratedAccumulationYield(sb, (IteratedAccumulationYield) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ContainerAccumulationYield) {
			genContainerAccumulationYield(sb, (ContainerAccumulationYield) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof IntegerRangeIterationYield) {
			genIntegerRangeIterationYield(sb, (IntegerRangeIterationYield) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ForFunction) {
			genForFunction(sb, (ForFunction) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ForIndexAccessEquality) {
			genForIndexAccessEquality(sb, (ForIndexAccessEquality) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ForIndexAccessOrdering) {
			genForIndexAccessOrdering(sb, (ForIndexAccessOrdering) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ConditionStatement) {
			genConditionStatement(sb, (ConditionStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof SwitchStatement) {
			genSwitchStatement(sb, (SwitchStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof WhileStatement) {
			genWhileStatement(sb, (WhileStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DoWhileStatement) {
			genDoWhileStatement(sb, (DoWhileStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof MultiStatement) {
			genMultiStatement(sb, (MultiStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DefDeclVarStatement) {
			genDefDeclVarStatement(sb, (DefDeclVarStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DefDeclGraphEntityStatement) {
			genDefDeclGraphEntityStatement(sb, (DefDeclGraphEntityStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof BreakStatement) {
			genBreakStatement(sb, (BreakStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ContinueStatement) {
			genContinueStatement(sb, (ContinueStatement) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof EmitProc) {
			genEmitProc(sb, (EmitProc) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DebugAddProc) {
			genDebugAddProc(sb, (DebugAddProc) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DebugRemProc) {
			genDebugRemProc(sb, (DebugRemProc) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DebugEmitProc) {
			genDebugEmitProc(sb, (DebugEmitProc) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DebugHaltProc) {
			genDebugHaltProc(sb, (DebugHaltProc) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof DebugHighlightProc) {
			genDebugHighlightProc(sb, (DebugHighlightProc) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof RecordProc) {
			genRecordProc(sb, (RecordProc) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof ReturnAssignment) {
			genYield(sb, ((ReturnAssignment) evalStmt).getProcedureInvocation(),
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else if(evalStmt instanceof IteratedFiltering) {
			genIteratedFiltering(sb, (IteratedFiltering) evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		}
		else {
			throw new UnsupportedOperationException("Unexpected yield statement \"" + evalStmt + "\"");
		}
	}

	private void genAssignmentVar(SourceBuilder sb, AssignmentVar ass,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = ass.getTarget();
		Expression expr = ass.getExpression();

		sb.appendFront("new GRGEN_EXPR.YieldAssignment(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", true, ");
		sb.append("\"" + formatType(target.getType()) + "\", ");
		genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}

	private void genAssignmentVarIndexed(SourceBuilder sb, AssignmentVarIndexed ass,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = ass.getTarget();
		Expression expr = ass.getExpression();
		Expression index = ass.getIndex();

		sb.appendFront("new GRGEN_EXPR.YieldAssignmentIndexed(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", ");
		genExpressionTree(sb, index, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", ");
		sb.append("\"" + formatType(expr.getType()) + "\"");
		sb.append(", ");
		sb.append("\"" + formatType(index.getType()) + "\"");
		sb.append(")");
	}

	private void genAssignmentGraphEntity(SourceBuilder sb, AssignmentGraphEntity ass,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		GraphEntity target = ass.getTarget();
		Expression expr = ass.getExpression();

		sb.appendFront("new GRGEN_EXPR.YieldAssignment(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", false, ");
		sb.append("\"" + (target instanceof Node ? "GRGEN_LGSP.LGSPNode" : "GRGEN_LGSP.LGSPEdge") + "\", ");
		genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}

	private void genCompoundAssignmentVarChangedVar(SourceBuilder sb, CompoundAssignmentVarChangedVar cass,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		String changedOperation;
		if(cass.getChangedOperation()==CompoundAssignment.UNION)
			changedOperation = "GRGEN_EXPR.YieldChangeDisjunctionAssignment";
		else if(cass.getChangedOperation()==CompoundAssignment.INTERSECTION)
			changedOperation = "GRGEN_EXPR.YieldChangeConjunctionAssignment";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = "GRGEN_EXPR.YieldChangeAssignment";

		Variable changedTarget = cass.getChangedTarget();	
		sb.appendFront("new " + changedOperation + "(");
		sb.append("\"" + formatEntity(changedTarget, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		genCompoundAssignmentVar(sb, cass, "", className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}

	private void genCompoundAssignmentVar(SourceBuilder sb, CompoundAssignmentVar cass, String prefix,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable target = cass.getTarget();
		assert(target.getType() instanceof MapType || target.getType() instanceof SetType);
		Expression expr = cass.getExpression();

		sb.append(prefix + "new GRGEN_EXPR.");
		if(cass.getOperation()==CompoundAssignment.UNION)
			sb.append("SetMapUnion(");
		else if(cass.getOperation()==CompoundAssignment.INTERSECTION)
			sb.append("SetMapIntersect(");
		else //if(cass.getOperation()==CompoundAssignment.WITHOUT)
			sb.append("SetMapExcept(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}

	private void genMapVarRemoveItem(SourceBuilder sb, MapVarRemoveItem mvri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = mvri.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpressionTree(sbtmp, mvri.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		String keyExprStr = sbtmp.toString();

		sb.appendFront("new GRGEN_EXPR.SetMapRemove(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		sb.append(keyExprStr);
		sb.append(", ");
		sb.append("\"" + formatType(mvri.getKeyExpr().getType()) + "\"");
		sb.append(")");
		
		assert mvri.getNext()==null;
	}

	private void genMapVarClear(SourceBuilder sb, MapVarClear mvc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = mvc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");
		
		assert mvc.getNext()==null;
	}

	private void genMapVarAddItem(SourceBuilder sb, MapVarAddItem mvai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = mvai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpressionTree(sbtmp, mvai.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		String valueExprStr = sbtmp.toString();
		sbtmp.delete(0, sbtmp.length());
		genExpressionTree(sbtmp, mvai.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		String keyExprStr = sbtmp.toString();

		sb.appendFront("new GRGEN_EXPR.MapAdd(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		sb.append(keyExprStr);
		sb.append(", ");
		sb.append(valueExprStr);
		sb.append(", ");
		sb.append("\"" + formatType(mvai.getKeyExpr().getType()) + "\"");
		sb.append(", ");
		sb.append("\"" + formatType(mvai.getValueExpr().getType()) + "\"");
		sb.append(")");

		assert mvai.getNext()==null;
	}

	private void genSetVarRemoveItem(SourceBuilder sb, SetVarRemoveItem svri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = svri.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpressionTree(sbtmp, svri.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		String valueExprStr = sbtmp.toString();

		sb.appendFront("new GRGEN_EXPR.SetMapRemove(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		sb.append(valueExprStr);
		sb.append(", ");
		sb.append("\"" + formatType(svri.getValueExpr().getType()) + "\"");
		sb.append(")");
		
		assert svri.getNext()==null;
	}

	private void genSetVarClear(SourceBuilder sb, SetVarClear svc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = svc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");
		
		assert svc.getNext()==null;
	}

	private void genSetVarAddItem(SourceBuilder sb, SetVarAddItem svai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = svai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpressionTree(sbtmp, svai.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		String valueExprStr = sbtmp.toString();

		sb.appendFront("new GRGEN_EXPR.SetAdd(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		sb.append(valueExprStr);
		sb.append(", ");
		sb.append("\"" + formatType(svai.getValueExpr().getType()) + "\"");
		sb.append(")");
		
		assert svai.getNext()==null;
	}

	private void genArrayVarRemoveItem(SourceBuilder sb, ArrayVarRemoveItem avri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = avri.getTarget();

		sb.appendFront("new GRGEN_EXPR.ArrayRemove(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		if(avri.getIndexExpr()!=null) {
			sb.append(", ");
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, avri.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(indexExprStr);
		}
		sb.append(")");
		
		assert avri.getNext()==null;
	}

	private void genArrayVarClear(SourceBuilder sb, ArrayVarClear avc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = avc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");
		
		assert avc.getNext()==null;
	}

	private void genArrayVarAddItem(SourceBuilder sb, ArrayVarAddItem avai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = avai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpressionTree(sbtmp, avai.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		String valueExprStr = sbtmp.toString();

		sb.appendFront("new GRGEN_EXPR.ArrayAdd(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		sb.append(valueExprStr);
		sb.append(", ");
		sb.append("\"" + formatType(avai.getValueExpr().getType()) + "\"");
		if(avai.getIndexExpr()!=null) {
			sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, avai.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(", ");
			sb.append(indexExprStr);
		}
		sb.append(")");
		
		assert avai.getNext()==null;
	}

	private void genDequeVarRemoveItem(SourceBuilder sb, DequeVarRemoveItem dvri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = dvri.getTarget();

		sb.appendFront("new GRGEN_EXPR.DequeRemove(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		if(dvri.getIndexExpr()!=null) {
			sb.append(", ");
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, dvri.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(indexExprStr);
		}
		sb.append(")");
		
		assert dvri.getNext()==null;
	}

	private void genDequeVarClear(SourceBuilder sb, DequeVarClear dvc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = dvc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");
		
		assert dvc.getNext()==null;
	}

	private void genDequeVarAddItem(SourceBuilder sb, DequeVarAddItem dvai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable target = dvai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpressionTree(sbtmp, dvai.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		String valueExprStr = sbtmp.toString();

		sb.appendFront("new GRGEN_EXPR.DequeAdd(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		sb.append(valueExprStr);
		sb.append(", ");
		sb.append("\"" + formatType(dvai.getValueExpr().getType()) + "\"");
		if(dvai.getIndexExpr()!=null) {
			sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, dvai.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(", ");
			sb.append(indexExprStr);
		}
		sb.append(")");
		
		assert dvai.getNext()==null;
	}

	private void genIteratedAccumulationYield(SourceBuilder sb, IteratedAccumulationYield iay,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable iterationVar = iay.getIterationVar();
		Rule iterated = iay.getIterated();

		sb.appendFront("new GRGEN_EXPR.IteratedAccumulationYield(");
		sb.append("\"" + formatEntity(iterationVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(iterationVar) + "\", ");
		sb.append("\"" + formatIdentifiable(iterated) + "\", ");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : iay.getAccumulationStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genContainerAccumulationYield(SourceBuilder sb, ContainerAccumulationYield cay,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable iterationVar = cay.getIterationVar();
		Variable indexVar = cay.getIndexVar();
		Variable container = cay.getContainer();

		sb.appendFront("new GRGEN_EXPR.ContainerAccumulationYield(");
		sb.append("\"" + formatEntity(iterationVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(iterationVar) + "\", ");
		Type valueType;
		Type indexType = null;
		if(container.getType() instanceof SetType) {
			valueType = ((SetType)container.getType()).getValueType();
		} else if(container.getType() instanceof MapType) {
			valueType = ((MapType)container.getType()).getValueType();
			indexType = ((MapType)container.getType()).getKeyType();
		} else if(container.getType() instanceof ArrayType) {
			valueType = ((ArrayType)container.getType()).getValueType();
			indexType = IntType.getType();
		} else {
			valueType = ((DequeType)container.getType()).getValueType();
			indexType = IntType.getType();
		}
		sb.append("\"" + formatType(valueType) + "\", ");
		if(indexVar!=null)
		{
			sb.append("\"" + formatEntity(indexVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
			sb.append("\"" + formatIdentifiable(indexVar) + "\", ");
			sb.append("\"" + formatType(indexType) + "\", ");
		}
		sb.append("\"" + formatEntity(container, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(container) + "\", ");
		sb.append("\"" + formatType(container.getType()) + "\", ");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : cay.getAccumulationStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genIntegerRangeIterationYield(SourceBuilder sb, IntegerRangeIterationYield iriy,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable iterationVar = iriy.getIterationVar();
		Expression left = iriy.getLeftExpr();
		Expression right = iriy.getRightExpr();

		sb.appendFront("new GRGEN_EXPR.IntegerRangeIterationYield(");
		sb.append("\"" + formatEntity(iterationVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(iterationVar) + "\", ");
		sb.append("\"" + formatType(iterationVar.getType()) + "\", ");
		genExpressionTree(sb, left, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", ");
		genExpressionTree(sb, right, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", ");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : iriy.getAccumulationStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genForFunction(SourceBuilder sb, ForFunction ff,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable iterationVar = ff.getIterationVar();
		Type iterationVarType = iterationVar.getType();

		sb.appendFront("new GRGEN_EXPR.ForFunction(");
		sb.append("\"" + formatEntity(iterationVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(iterationVar) + "\", ");
		sb.append("\"" + formatElementInterfaceRef(iterationVarType) + "\", ");
		if(ff.getFunction() instanceof AdjacentNodeExpr) {
			AdjacentNodeExpr adjacentExpr = (AdjacentNodeExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, adjacentExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		} else if(ff.getFunction() instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr incidentExpr = (IncidentEdgeExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, incidentExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		} else if(ff.getFunction() instanceof ReachableNodeExpr) {
			ReachableNodeExpr reachableExpr = (ReachableNodeExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, reachableExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		} else if(ff.getFunction() instanceof ReachableEdgeExpr) {
			ReachableEdgeExpr reachableExpr = (ReachableEdgeExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, reachableExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		} else if(ff.getFunction() instanceof BoundedReachableNodeExpr) {
			BoundedReachableNodeExpr boundedReachableExpr = (BoundedReachableNodeExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, boundedReachableExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		} else if(ff.getFunction() instanceof BoundedReachableEdgeExpr) {
			BoundedReachableEdgeExpr boundedReachableExpr = (BoundedReachableEdgeExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, boundedReachableExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		} else if(ff.getFunction() instanceof NodesExpr) {
			NodesExpr nodesExpr = (NodesExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, nodesExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		} else if(ff.getFunction() instanceof EdgesExpr) {
			EdgesExpr edgesExpr = (EdgesExpr)ff.getFunction();
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, edgesExpr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(sbtmp.toString() + ", ");
		}
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : ff.getLoopedStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genForIndexAccessEquality(SourceBuilder sb, ForIndexAccessEquality fiae,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable iterationVar = fiae.getIterationVar();
		Type iterationVarType = iterationVar.getType();

		sb.appendFront("new GRGEN_EXPR.ForIndexAccessEquality(");
		sb.append("\"GRGEN_MODEL." + model.getIdent() + "IndexSet\", ");
		sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\"" + fiae.getIndexAcccessEquality().index.getIdent() + "\"), ");

		sb.append("\"" + formatEntity(iterationVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(iterationVar) + "\", ");
		sb.append("\"" + formatElementInterfaceRef(iterationVarType) + "\", ");

		genExpressionTree(sb, fiae.getIndexAcccessEquality().expr, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", ");
			
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : fiae.getLoopedStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genForIndexAccessOrdering(SourceBuilder sb, ForIndexAccessOrdering fiao,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable iterationVar = fiao.getIterationVar();
		Type iterationVarType = iterationVar.getType();

		sb.appendFront("new GRGEN_EXPR.ForIndexAccessOrdering(");
		sb.append("\"GRGEN_MODEL." + model.getIdent() + "IndexSet\", ");
		sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\"" + fiao.getIndexAccessOrdering().index.getIdent() + "\"), ");

		sb.append("\"" + formatEntity(iterationVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(iterationVar) + "\", ");
		sb.append("\"" + formatElementInterfaceRef(iterationVarType) + "\", ");

		sb.append(fiao.getIndexAccessOrdering().ascending ? "true, " : "false, ");
		sb.append(fiao.getIndexAccessOrdering().includingFrom() ? "true, " : "false, ");
		sb.append(fiao.getIndexAccessOrdering().includingTo() ? "true, " : "false, ");
		if(fiao.getIndexAccessOrdering().from() != null)
			genExpressionTree(sb, fiao.getIndexAccessOrdering().from(), className, pathPrefix, alreadyDefinedEntityToName);
		else
			sb.append("null");
		sb.append(", ");
		if(fiao.getIndexAccessOrdering().to() != null)
			genExpressionTree(sb, fiao.getIndexAccessOrdering().to(), className, pathPrefix, alreadyDefinedEntityToName);
		else
			sb.append("null");
		sb.append(", ");
			
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : fiao.getLoopedStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genConditionStatement(SourceBuilder sb, ConditionStatement cs,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.ConditionStatement(");
		genExpressionTree(sb, cs.getConditionExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(",");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : cs.getTrueCaseStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}, ");
		if(cs.getFalseCaseStatements()!=null) {
			sb.append("new GRGEN_EXPR.Yielding[] { ");
			for(EvalStatement statement : cs.getFalseCaseStatements()) {
				genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}");
		} else {
			sb.append("null");
		}
		sb.append(")");
	}

	private void genSwitchStatement(SourceBuilder sb, SwitchStatement ss,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.SwitchStatement(");
		genExpressionTree(sb, ss.getSwitchExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(",");
		sb.append("new GRGEN_EXPR.CaseStatement[] { ");
		for(CaseStatement statement : ss.getStatements()) {
			genCaseStatement(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("} ");
		sb.append(")");
	}

	private void genCaseStatement(SourceBuilder sb, CaseStatement cs,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.CaseStatement(");
		if(cs.getCaseConstantExpr() != null)
			genExpressionTree(sb, cs.getCaseConstantExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		else
			sb.append("null");
		sb.append(",");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : cs.getStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("} ");
		sb.append(")");
	}

	private void genWhileStatement(SourceBuilder sb, WhileStatement ws,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.WhileStatement(");
		genExpressionTree(sb, ws.getConditionExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(",");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : ws.getLoopedStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genDoWhileStatement(SourceBuilder sb, DoWhileStatement dws,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.DoWhileStatement(");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : dws.getLoopedStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(",");
		genExpressionTree(sb, dws.getConditionExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}

	private void genMultiStatement(SourceBuilder sb, MultiStatement ms,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.MultiStatement(");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : ms.getStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	private void genDefDeclVarStatement(SourceBuilder sb, DefDeclVarStatement ddvs,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		Variable var = ddvs.getTarget();
		sb.appendFront("new GRGEN_EXPR.DefDeclaration(");
		sb.append("\"" + formatEntity(var, pathPrefix, alreadyDefinedEntityToName) + "\",");
		sb.append("\"" + formatType(var.getType()) + "\",");
		if(var.initialization!=null) {
			genExpressionTree(sb, var.initialization, className, pathPrefix, alreadyDefinedEntityToName);		
		} else {
			sb.append("null");
		}
		sb.append(")");
	}

	private void genDefDeclGraphEntityStatement(SourceBuilder sb, DefDeclGraphEntityStatement ddges,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		GraphEntity graphEntity = ddges.getTarget();
		sb.appendFront("new GRGEN_EXPR.DefDeclaration(");
		sb.append("\"" + formatEntity(graphEntity, pathPrefix, alreadyDefinedEntityToName) + "\",");
		sb.append("\"" + formatType(graphEntity.getType()) + "\",");
		if(graphEntity.initialization!=null) {
			genExpressionTree(sb, graphEntity.initialization, className, pathPrefix, alreadyDefinedEntityToName);		
		} else {
			sb.append("null");
		}
		sb.append(")");
	}

	private void genBreakStatement(SourceBuilder sb, BreakStatement bs,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.BreakStatement()");
	}

	private void genContinueStatement(SourceBuilder sb, ContinueStatement cs,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.ContinueStatement()");
	}

	private void genEmitProc(SourceBuilder sb, EmitProc ep,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.EmitStatement(");
		sb.append("new GRGEN_EXPR.Expression[] { ");
		for(Expression expr : ep.getExpressions()) {
			genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}");
		sb.append(", ");
		sb.append(ep.isDebug());
		sb.append(")");
	}

	private void genDebugAddProc(SourceBuilder sb, DebugAddProc dap,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.DebugAddStatement(");		
		genExpressionTree(sb, dap.getFirstExpression(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_EXPR.Expression[] { ");
		boolean first = true;
		for(Expression expr : dap.getExpressions()) {
			if(!first) {
				genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			first = false;
		}
		sb.append("}");
		sb.append(")");
	}

	private void genDebugRemProc(SourceBuilder sb, DebugRemProc drp,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.DebugRemStatement(");		
		genExpressionTree(sb, drp.getFirstExpression(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_EXPR.Expression[] { ");
		boolean first = true;
		for(Expression expr : drp.getExpressions()) {
			if(!first) {
				genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			first = false;
		}
		sb.append("}");
		sb.append(")");
	}

	private void genDebugEmitProc(SourceBuilder sb, DebugEmitProc dep,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.DebugEmitStatement(");		
		genExpressionTree(sb, dep.getFirstExpression(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_EXPR.Expression[] { ");
		boolean first = true;
		for(Expression expr : dep.getExpressions()) {
			if(!first) {
				genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			first = false;
		}
		sb.append("}");
		sb.append(")");
	}

	private void genDebugHaltProc(SourceBuilder sb, DebugHaltProc dhp,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.DebugHaltStatement(");		
		genExpressionTree(sb, dhp.getFirstExpression(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_EXPR.Expression[] { ");
		boolean first = true;
		for(Expression expr : dhp.getExpressions()) {
			if(!first) {
				genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			first = false;
		}
		sb.append("}");
		sb.append(")");
	}

	private void genDebugHighlightProc(SourceBuilder sb, DebugHighlightProc dhp,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.DebugHighlightStatement(");		
		genExpressionTree(sb, dhp.getFirstExpression(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(", new GRGEN_EXPR.Expression[] { ");
		int parameterNum = 0;
		for(Expression expr : dhp.getExpressions()) {
			if(parameterNum != 0 && parameterNum % 2 == 1) {
				genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			++parameterNum;
		}
		sb.append("} ");
		sb.append(", new GRGEN_EXPR.Expression[] { ");
		parameterNum = 0;
		for(Expression expr : dhp.getExpressions()) {
			if(parameterNum != 0 && parameterNum % 2 == 0) {
				genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			++parameterNum;
		}
		sb.append("}");
		sb.append(")");
	}

	private void genRecordProc(SourceBuilder sb, RecordProc rp,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.RecordStatement(");
		genExpressionTree(sb, rp.getToRecordExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}
	
	private void genIteratedFiltering(SourceBuilder sb, IteratedFiltering itf,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		sb.appendFront("new GRGEN_EXPR.IteratedFiltering(");
		sb.append("\"" + itf.getActionOrSubpattern().getIdent() + "\", ");
		sb.append("\"" + itf.getIterated().getIdent() + "\", ");
		sb.append("new GRGEN_EXPR.FilterInvocation[] {");
		for(int i=0; i<itf.getFilterInvocations().size(); ++i) {
			FilterInvocation filterInvocation = itf.getFilterInvocation(i);
			genFilterInvocation(sb, filterInvocation, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("} ");
		sb.append(")");
	}

	private void genFilterInvocation(SourceBuilder sb, FilterInvocation fi,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		FilterAutoSupplied fas = fi.getFilterAutoSupplied();
		FilterAutoGenerated fag = fi.getFilterAutoGenerated();
		sb.append("new GRGEN_EXPR.FilterInvocation(");
		sb.append("\"" + (fas != null ? fas.getFilterName() : fag.getFilterName() + fag.getUnderscoreSuffix()) + "\", ");
		sb.append((fas != null ? "true" : "false") + ", ");
		sb.append("new GRGEN_EXPR.Expression[] {");
		for(int i=0; i<fi.getFilterArguments().size(); ++i) {
			Expression argument = fi.getFilterArgument(i);
			genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}, ");
		sb.append("new String[] {");
		for(int i=0; i<fi.getFilterArguments().size(); ++i) {
			Expression argument = fi.getFilterArgument(i);
			if(argument.getType() instanceof InheritanceType) {
				sb.append("\"" + formatElementInterfaceRef(argument.getType()) + "\"");
			} else {
				sb.append("null");
			}
			sb.append(", ");
		}
		sb.append("}");
		sb.append(")");
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private SearchPlanBackend2 be;
	private ModifyGen mg;
	private ModifyGen mgFuncComp;
	private Model model;
}
