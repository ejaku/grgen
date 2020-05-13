/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the exec representation for the SearchPlanBackend2 backend.
 * @author Edgar Jakumeit, Moritz Kroll
 */

package de.unika.ipd.grgen.be.Csharp;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.util.SourceBuilder;

public class ActionsExecGen extends CSharpBase
{
	public ActionsExecGen(String nodeTypePrefix, String edgeTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix);
	}

	//////////////////////////////////////////
	// Imperative statement/exec generation //
	//////////////////////////////////////////

	public void genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName,
			boolean isTopLevel, boolean isSubpattern)
	{
		if(rule.getRight() == null) {
			return;
		}

		if(isTopLevel) {
			sb.append("#if INITIAL_WARMUP\t\t// GrGen imperative statement section: "
					+ getPackagePrefixDoubleColon(rule) + (isSubpattern ? "Pattern_" : "Rule_")
					+ formatIdentifiable(rule) + "\n");
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

	private void genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName)
	{
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

	private int genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName,
			EvalStatement evalStmt, int xgrsID)
	{
		if(evalStmt instanceof ConditionStatement) {
			ConditionStatement condStmt = (ConditionStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : condStmt.getTrueCaseStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			if(condStmt.getFalseCaseStatements() != null) {
				for(EvalStatement nestedEvalStmt : condStmt.getFalseCaseStatements()) {
					xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
				}
			}
		} else if(evalStmt instanceof SwitchStatement) {
			SwitchStatement switchStmt = (SwitchStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : switchStmt.getStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof CaseStatement) {
			CaseStatement caseStmt = (CaseStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : caseStmt.getStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof WhileStatement) {
			WhileStatement whileStmt = (WhileStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : whileStmt.getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof DoWhileStatement) {
			DoWhileStatement doWhileStmt = (DoWhileStatement)evalStmt;
			for(EvalStatement nestedEvalStmt : doWhileStmt.getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof ContainerAccumulationYield) {
			ContainerAccumulationYield containerAccumulationYieldStmt = (ContainerAccumulationYield)evalStmt;
			for(EvalStatement nestedEvalStmt : containerAccumulationYieldStmt.getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof IntegerRangeIterationYield) {
			IntegerRangeIterationYield integerRangeIterationYieldStmt = (IntegerRangeIterationYield)evalStmt;
			for(EvalStatement nestedEvalStmt : integerRangeIterationYieldStmt.getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof MatchesAccumulationYield) {
			MatchesAccumulationYield matchesAccumulationYieldStmt = (MatchesAccumulationYield)evalStmt;
			for(EvalStatement nestedEvalStmt : matchesAccumulationYieldStmt.getAccumulationStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof ForFunction) {
			ForFunction forFunctionStmt = (ForFunction)evalStmt;
			for(EvalStatement nestedEvalStmt : forFunctionStmt.getLoopedStatements()) {
				xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
		} else if(evalStmt instanceof ExecStatement) {
			ExecStatement execStmt = (ExecStatement)evalStmt;
			xgrsID = genImperativeStatements(sb, rule, pathPrefix, packageName, execStmt, xgrsID);
		}
		return xgrsID;
	}

	private int genExec(SourceBuilder sb, String pathPrefix, String packageName, Exec exec, int xgrsID)
	{
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
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ",\n");
		sb.appendFront("\"" + escapeBackslashAndDoubleQuotes(exec.getXGRSString()) + "\",\n");
		sb.appendFront(exec.getLineNr() + "\n");
		sb.unindent();
		sb.appendFront(");\n");

		sb.appendFront("private static bool ApplyXGRS_" + pathPrefix + xgrsID
				+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
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

	private int genImperativeStatements(SourceBuilder sb, Rule rule, String pathPrefix, String packageName,
			ExecStatement execStmt, int xgrsID)
	{
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
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ",\n");
		sb.appendFront("\"" + escapeBackslashAndDoubleQuotes(execStmt.getXGRSString()) + "\",\n");
		sb.appendFront(execStmt.getLineNr() + "\n");
		sb.unindent();
		sb.appendFront(");\n");

		sb.appendFront("private static bool ApplyXGRS_" + pathPrefix + xgrsID
				+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
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

	public void genImperativeStatementClosures(SourceBuilder sb, Rule rule, String pathPrefix,
			boolean isTopLevelRule)
	{
		if(rule.getRight() == null) {
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

	private void genImperativeStatementClosures(SourceBuilder sb, Rule rule, String pathPrefix)
	{
		int xgrsID = 0;
		for(ImperativeStmt istmt : rule.getRight().getImperativeStmts()) {
			if(!(istmt instanceof Exec)) {
				continue;
			}

			Exec exec = (Exec)istmt;
			sb.append("\n");
			sb.appendFront("public class XGRSClosure_" + pathPrefix + xgrsID
					+ " : GRGEN_LGSP.LGSPEmbeddedSequenceClosure\n");
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
			sb.appendFront("}\n");

			++xgrsID;
		}
	}

	public void genImperativeStatements(SourceBuilder sb, Procedure procedure)
	{
		int xgrsID = 0;
		for(EvalStatement evalStmt : procedure.getComputationStatements()) {
			xgrsID = genImperativeStatements(sb, procedure, evalStmt, xgrsID);
		}
	}

	private int genImperativeStatements(SourceBuilder sb, Procedure procedure, EvalStatement evalStmt, int xgrsID)
	{
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
			ExecStatement execStmt, int xgrsID)
	{
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
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ",\n");
		sb.appendFront("\"" + escapeBackslashAndDoubleQuotes(exec.getXGRSString()) + "\",\n");
		sb.appendFront(exec.getLineNr() + "\n");
		sb.unindent();
		sb.appendFront(");\n");

		sb.appendFront("private static bool ApplyXGRS_" + formatIdentifiable(procedure) + "_" + xgrsID
				+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
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
