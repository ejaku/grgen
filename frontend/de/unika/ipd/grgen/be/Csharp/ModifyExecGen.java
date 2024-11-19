/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the eval statements for the SearchPlanBackend2 backend.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.HashSet;
import java.util.Map;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.stmt.ExecStatement;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.MatchType;
import de.unika.ipd.grgen.util.SourceBuilder;

public class ModifyExecGen extends CSharpBase
{
	Model model;
	SearchPlanBackend2 be;

	int xgrsID;

	public ModifyExecGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix, String objectTypePrefix, String transientObjectTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);
		be = backend;
		model = be.unit.getActionsGraphModel();

		xgrsID = 0;
	}

	public void genExecStatement(SourceBuilder sb, ModifyGenerationStateConst state, ExecStatement es)
	{
		Exec exec = es.getExec();
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				if(neededEntity instanceof GraphEntity) {
					sb.appendFront(formatElementInterfaceRef(neededEntity.getType()) + " ");
					sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + " = ");
					sb.append("(" + formatElementInterfaceRef(neededEntity.getType()) + ")");
					sb.append(formatEntity(neededEntity) + ";\n");
				} else { // if(neededEntity instanceof Variable) 
					sb.appendFront(formatAttributeType(neededEntity.getType()) + " ");
					sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + " = ");
					sb.append("(" + formatAttributeType(neededEntity.getType()) + ")");
					sb.append(formatEntity(neededEntity) + ";\n");
				}
			}
		}
		sb.appendFront("ApplyXGRS_" + state.name() + "_" + xgrsID
				+ "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(!neededEntity.isDefToBeYieldedTo()) {
				sb.append(", ");
				if(neededEntity.getType() instanceof InheritanceType) {
					sb.append("(" + formatElementInterfaceRef(neededEntity.getType()) + ")");
				}
				sb.append(formatEntity(neededEntity));
			}
		}
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.append(", ref ");
				sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID);
			}
		}
		sb.append(");\n");
		for(Entity neededEntity : exec.getNeededEntities(true)) {
			if(neededEntity.isDefToBeYieldedTo()) {
				sb.appendFront(formatEntity(neededEntity) + " = ");
				if((neededEntity.getContext() & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION) {
					if(neededEntity instanceof Node) {
						sb.append("(GRGEN_LGSP.LGSPNode)");
					} else if(neededEntity instanceof Edge) {
						sb.append("(GRGEN_LGSP.LGSPEdge)");
					}
				}
				sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + ";\n");
			}
		}

		++xgrsID;
	}

	public void genImperativeStatements(SourceBuilder sb, ModifyGenerationTask task,
			ModifyGenerationState state, ModifyGenerationStateConst stateConst, NeededEntities needs,
			String pathPrefix, String packagePrefixedActionName)
	{
		if(state.emitProfilingInstrumentation() && pathPrefix.equals("")
				&& !task.isSubpattern && task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			genExecProfilingStart(sb);

		collectContainerExprsNeededByImperativeStatements(task, needs);
		state.InitNeeds(needs.containerExprs);
		genContainerVariablesBeforeImperativeStatements(sb, stateConst);

		state.useVarForResult = true;
		genImperativeStatements(sb, stateConst, task, pathPrefix);
		state.useVarForResult = false;

		state.ClearContainerExprs();

		if(state.emitProfilingInstrumentation() && pathPrefix.equals("")
				&& !task.isSubpattern && task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			genExecProfilingStop(sb, packagePrefixedActionName);
	}

	private static void genExecProfilingStart(SourceBuilder sb)
	{
		sb.appendFront("long searchStepsAtBeginExec = actionEnv.PerformanceInfo.SearchSteps;\n");
	}

	private static void collectContainerExprsNeededByImperativeStatements(ModifyGenerationTask task,
			NeededEntities needs)
	{
		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit)istmt;
				for(Expression arg : emit.getArguments()) {
					arg.collectNeededEntities(needs);
				}
			}
		}
	}

	private void genContainerVariablesBeforeImperativeStatements(SourceBuilder sb, ModifyGenerationStateConst state)
	{
		for(Map.Entry<Expression, String> entry : state.mapExprToTempVar().entrySet()) {
			Expression expr = entry.getKey();
			String varName = entry.getValue();
			sb.appendFront(formatAttributeType(expr.getType()) + " " + varName + " = ");
			genExpression(sb, expr, state);
			sb.append(";\n");
		}
	}

	private void genImperativeStatements(SourceBuilder sb, ModifyGenerationStateConst state,
			ModifyGenerationTask task, String pathPrefix)
	{
		if(!task.mightThereBeDeferredExecs) { // procEnv was already emitted in case of deferred execs
			if(!task.right.getImperativeStmts().isEmpty()) { // we need it?
				// see genSubpatternModificationCalls why not simply emitting in case of !task.right.getImperativeStmts().isEmpty()
				if(!task.isEmitHereNeeded()) { // it was not already emitted?
					sb.appendFront("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;\n");
				}
			}
		}

		if(task.mightThereBeDeferredExecs) {
			sb.appendFront("procEnv.sequencesManager.ExecuteDeferredSequencesThenExitRuleModify(procEnv);\n");
		}

		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			if(istmt instanceof Emit) {
				Emit emit = (Emit)istmt;
				genEmit(sb, state, emit);
			} else if(istmt instanceof Exec) {
				Exec exec = (Exec)istmt;
				genExec(sb, task, pathPrefix, exec);
			} else
				assert false : "unknown ImperativeStmt: " + istmt + " in " + task.left.getNameOfGraph();
		}
	}

	private static void genExecProfilingStop(SourceBuilder sb, String packagePrefixedActionName)
	{
		sb.appendFront("actionEnv.PerformanceInfo.ActionProfiles[\"" + packagePrefixedActionName
				+ "\"].searchStepsDuringExecTotal");
		sb.append(" += actionEnv.PerformanceInfo.SearchSteps - searchStepsAtBeginExec;\n");
	}

	public void genEmit(SourceBuilder sb, ModifyGenerationStateConst state, Emit emit)
	{
		String emitWriter = emit.isDebug() ? "EmitWriterDebug" : "EmitWriter";
		for(Expression arg : emit.getArguments()) {
			sb.appendFront("procEnv." + emitWriter + ".Write(");
			sb.append("GRGEN_LIBGR.EmitHelper.ToStringNonNull(");
			genExpression(sb, arg, state);
			sb.append(", graph, false, null, null, null)");
			sb.append(");\n");
		}
	}

	private void genExec(SourceBuilder sb, ModifyGenerationTask task, String pathPrefix, Exec exec)
	{
		if(task.isSubpattern || pathPrefix != "") {
			String closureName = "XGRSClosure_" + pathPrefix + task.left.getNameOfGraph() + "_" + xgrsID;
			sb.appendFront(closureName + " xgrs" + xgrsID + " = "
					+ "new " + closureName + "(");
			boolean first = true;
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				if(first) {
					first = false;
				} else {
					sb.append(", ");
				}
				if(neededEntity.getType() instanceof InheritanceType) {
					sb.append("(" + formatElementInterfaceRef(neededEntity.getType()) + ")");
				}
				sb.append(formatEntity(neededEntity));
			}
			sb.append(");\n");
			sb.appendFront("procEnv.sequencesManager.AddDeferredSequence(xgrs" + xgrsID + ");\n");
		} else {
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				if(neededEntity.isDefToBeYieldedTo()) {
					if(neededEntity instanceof GraphEntity) {
						sb.appendFront(formatElementInterfaceRef(neededEntity.getType()) + " ");
						sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + " = ");
						sb.append("(" + formatElementInterfaceRef(neededEntity.getType()) + ")");
						sb.append(formatEntity(neededEntity) + ";\n");
					} else { // if(neededEntity instanceof Variable) 
						sb.appendFront(formatAttributeType(neededEntity.getType()) + " ");
						sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + " = ");
						sb.append("(" + formatAttributeType(neededEntity.getType()) + ")");
						sb.append(formatEntity(neededEntity) + ";\n");
					}
				}
			}
			sb.appendFront("ApplyXGRS_" + task.left.getNameOfGraph() + "_" + xgrsID + "(procEnv");
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				if(!neededEntity.isDefToBeYieldedTo()) {
					sb.append(", ");
					if(neededEntity.getType() instanceof InheritanceType) {
						sb.append("(" + formatElementInterfaceRef(neededEntity.getType()) + ")");
					}
					sb.append(formatEntity(neededEntity));
				}
			}
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				if(neededEntity.isDefToBeYieldedTo()) {
					sb.append(", ref ");
					sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID);
				}
			}
			sb.append(");\n");
			for(Entity neededEntity : exec.getNeededEntities(false)) {
				if(neededEntity.isDefToBeYieldedTo()) {
					sb.appendFront(formatEntity(neededEntity) + " = ");
					if((neededEntity.getContext() & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION) {
						if(neededEntity instanceof Node) {
							sb.append("(GRGEN_LGSP.LGSPNode)");
						} else if(neededEntity instanceof Edge) {
							sb.append("(GRGEN_LGSP.LGSPEdge)");
						}
					}
					sb.append("tmp_" + formatEntity(neededEntity) + "_" + xgrsID + ";\n");
				}
			}
		}
		/*		for(Expression arg : exec.getArguments()) {
					if(!(arg instanceof GraphEntityExpression)) continue;
					sb.append(", ");
					genExpression(sb, arg, state);
				}*/

		++xgrsID;
	}

	//////////////////////
	// Expression stuff //
	//////////////////////

	@Override
	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState)
	{
		genQualAccess(sb, qual, (ModifyGenerationStateConst)modifyGenerationState);
	}

	private void genQualAccess(SourceBuilder sb, Qualification qual, ModifyGenerationStateConst state)
	{
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		if(owner.getType() instanceof MatchType || owner.getType() instanceof DefinedMatchType) {
			sb.append(formatEntity(owner) + "." + formatEntity(member));
		} else {
			genQualAccess(sb, state, owner, member);
		}
	}

	private void genQualAccess(SourceBuilder sb, ModifyGenerationStateConst state, Entity owner, Entity member)
	{
		if(!Expression.isGlobalVariable(owner)) {
			if(state == null) {
				assert false;
				sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
				return;
			}

			if(accessViaVariable(state, /*(GraphEntity)*/owner, member)) {
				sb.append("tempvar_" + formatEntity(owner) + "_" + formatIdentifiable(member));
			} else {
				if(state.accessViaInterface().contains(owner))
					sb.append("i");

				sb.append(formatEntity(owner) + ".@" + formatIdentifiable(member));
			}
		} else {
			sb.append(formatGlobalVariableRead(owner));
			sb.append(".@" + formatIdentifiable(member));
		}
	}

	@Override
	protected void genMemberAccess(SourceBuilder sb, Entity member)
	{
		// needed in implementing methods
		sb.append("@" + formatIdentifiable(member));
	}

	private static boolean accessViaVariable(ModifyGenerationStateConst state, Entity elem, Entity attr)
	{
		HashSet<Entity> forcedAttrs = state.forceAttributeToVar().get(elem);
		return forcedAttrs != null && forcedAttrs.contains(attr);
	}
}
