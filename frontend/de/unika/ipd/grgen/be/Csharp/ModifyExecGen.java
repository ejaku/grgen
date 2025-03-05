/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the eval statements for the SearchPlanBackend2 backend.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.ArrayList;
import java.util.EnumSet;
import java.util.HashSet;
import java.util.Map;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.NeededEntities.Needs;
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
import de.unika.ipd.grgen.util.Pair;
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
			ModifyGenerationState state, ModifyGenerationStateConst stateConst, NeededEntities needsInput,
			String pathPrefix, String packagePrefixedActionName)
	{
		if(state.emitProfilingInstrumentation() && pathPrefix.equals("")
				&& !task.isSubpattern && task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			genExecProfilingStart(sb);

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

		state.ClearContainerExprs();
		for(ImperativeStmt istmt : task.right.getImperativeStmts()) {
			NeededEntities needs = new NeededEntities(EnumSet.of(Needs.CONTAINER_EXPRS));
			collectContainerExprsNeededByImperativeStatement(istmt, needs);
			state.InitNeeds(needs.containerExprs);

			sb.appendFront("{\n");
			sb.indent();
			genContainerVariablesBeforeImperativeStatement(sb, state);

			state.useVarForResult = true;
			genImperativeStatement(sb, stateConst, task, istmt, pathPrefix);
			state.useVarForResult = false;

			sb.unindent();
			sb.appendFront("}\n");

			state.ClearContainerExprs();
		}

		if(state.emitProfilingInstrumentation() && pathPrefix.equals("")
				&& !task.isSubpattern && task.typeOfTask == ModifyGenerationTask.TYPE_OF_TASK_MODIFY)
			genExecProfilingStop(sb, packagePrefixedActionName);
	}

	private static void genExecProfilingStart(SourceBuilder sb)
	{
		sb.appendFront("long searchStepsAtBeginExec = actionEnv.PerformanceInfo.SearchSteps;\n");
	}

	private static void collectContainerExprsNeededByImperativeStatement(ImperativeStmt istmt, 
			NeededEntities needs)
	{
		if(istmt instanceof Emit) {
			Emit emit = (Emit)istmt;
			for(Expression arg : emit.getArguments()) {
				arg.collectNeededEntities(needs);
			}
		}
	}

	private void genContainerVariablesBeforeImperativeStatement(SourceBuilder sb, ModifyGenerationState state)
	{
		// the container expressions are visited and inserted into the LinkedHashSet from the needs in preorder, and transferred to a LinkedHashMap in iteration order
		ArrayList<Pair<Expression, String>> array = new ArrayList<Pair<Expression, String>>();
		for(Map.Entry<Expression, String> entry : state.mapExprToTempVar().entrySet()) {
			array.add(new Pair<Expression, String>(entry.getKey(), entry.getValue()));
		}

		// iterate in reverse order so that contained container expressions are evaluated before their containing expressions
		for(int i = array.size() - 1; i >= 0; --i) {
			Expression expr = array.get(i).first;
			String varName = array.get(i).second;
			sb.appendFront(formatAttributeType(expr.getType()) + " " + varName + " = ");

			state.switchToVarForResultAfterFirstVarUsage = true;
			genExpression(sb, expr, state);
			state.switchToVarForResultAfterFirstVarUsage = false;
			state.useVarForResult = false;

			sb.append(";\n");
		}
	}

	private void genImperativeStatement(SourceBuilder sb, ModifyGenerationStateConst state,
			ModifyGenerationTask task, ImperativeStmt istmt, String pathPrefix)
	{
		if(istmt instanceof Emit) {
			Emit emit = (Emit)istmt;
			genEmit(sb, state, emit);
		} else if(istmt instanceof Exec) {
			Exec exec = (Exec)istmt;
			genExec(sb, task, pathPrefix, exec);
		} else
			assert false : "unknown ImperativeStmt: " + istmt + " in " + task.left.getNameOfGraph();
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
