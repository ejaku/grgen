/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the eval statements for the SearchPlanBackend2 backend.
 * @author Edgar Jakumeit, Moritz Kroll
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.stmt.Assignment;
import de.unika.ipd.grgen.ir.stmt.AssignmentBase;
import de.unika.ipd.grgen.ir.stmt.AssignmentGraphEntity;
import de.unika.ipd.grgen.ir.stmt.AssignmentIdentical;
import de.unika.ipd.grgen.ir.stmt.AssignmentIndexed;
import de.unika.ipd.grgen.ir.stmt.AssignmentMember;
import de.unika.ipd.grgen.ir.stmt.AssignmentVar;
import de.unika.ipd.grgen.ir.stmt.AssignmentVarIndexed;
import de.unika.ipd.grgen.ir.stmt.BreakStatement;
import de.unika.ipd.grgen.ir.stmt.CaseStatement;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignment;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChanged;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVar;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVisited;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVar;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChanged;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVar;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVisited;
import de.unika.ipd.grgen.ir.stmt.ConditionStatement;
import de.unika.ipd.grgen.ir.stmt.ContainerAccumulationYield;
import de.unika.ipd.grgen.ir.stmt.ContinueStatement;
import de.unika.ipd.grgen.ir.stmt.DefDeclGraphEntityStatement;
import de.unika.ipd.grgen.ir.stmt.DefDeclVarStatement;
import de.unika.ipd.grgen.ir.stmt.DoWhileStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.stmt.ExecStatement;
import de.unika.ipd.grgen.ir.stmt.IntegerRangeIterationYield;
import de.unika.ipd.grgen.ir.stmt.MatchesAccumulationYield;
import de.unika.ipd.grgen.ir.stmt.MultiStatement;
import de.unika.ipd.grgen.ir.stmt.ProcedureInvocationBase;
import de.unika.ipd.grgen.ir.stmt.ReturnAssignment;
import de.unika.ipd.grgen.ir.stmt.ReturnStatement;
import de.unika.ipd.grgen.ir.stmt.ReturnStatementFilter;
import de.unika.ipd.grgen.ir.stmt.ReturnStatementProcedure;
import de.unika.ipd.grgen.ir.stmt.SwitchStatement;
import de.unika.ipd.grgen.ir.stmt.WhileStatement;
import de.unika.ipd.grgen.ir.stmt.array.ArrayAddItem;
import de.unika.ipd.grgen.ir.stmt.array.ArrayClear;
import de.unika.ipd.grgen.ir.stmt.array.ArrayRemoveItem;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddItem;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarClear;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarRemoveItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeAddItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeClear;
import de.unika.ipd.grgen.ir.stmt.deque.DequeRemoveItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarAddItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarClear;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarRemoveItem;
import de.unika.ipd.grgen.ir.stmt.graph.AssignmentNameof;
import de.unika.ipd.grgen.ir.stmt.graph.AssignmentVisited;
import de.unika.ipd.grgen.ir.stmt.graph.ForFunction;
import de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessEquality;
import de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessOrdering;
import de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyEdgeProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyNodeProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphAddEdgeProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphAddNodeProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphClearProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphMergeProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectSourceAndTargetProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectSourceProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectTargetProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRemoveProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeEdgeProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeNodeProc;
import de.unika.ipd.grgen.ir.stmt.graph.InsertCopyProc;
import de.unika.ipd.grgen.ir.stmt.graph.InsertDefinedSubgraphProc;
import de.unika.ipd.grgen.ir.stmt.graph.InsertInducedSubgraphProc;
import de.unika.ipd.grgen.ir.stmt.graph.InsertProc;
import de.unika.ipd.grgen.ir.stmt.graph.VAllocProc;
import de.unika.ipd.grgen.ir.stmt.graph.VFreeNonResetProc;
import de.unika.ipd.grgen.ir.stmt.graph.VFreeProc;
import de.unika.ipd.grgen.ir.stmt.graph.VResetProc;
import de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureInvocation;
import de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureMethodInvocation;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureInvocation;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureMethodInvocation;
import de.unika.ipd.grgen.ir.stmt.map.MapAddItem;
import de.unika.ipd.grgen.ir.stmt.map.MapClear;
import de.unika.ipd.grgen.ir.stmt.map.MapRemoveItem;
import de.unika.ipd.grgen.ir.stmt.map.MapVarAddItem;
import de.unika.ipd.grgen.ir.stmt.map.MapVarClear;
import de.unika.ipd.grgen.ir.stmt.map.MapVarRemoveItem;
import de.unika.ipd.grgen.ir.stmt.procenv.CommitTransactionProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugAddProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugEmitProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHaltProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHighlightProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugRemProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DeleteFileProc;
import de.unika.ipd.grgen.ir.stmt.procenv.EmitProc;
import de.unika.ipd.grgen.ir.stmt.procenv.ExportProc;
import de.unika.ipd.grgen.ir.stmt.procenv.PauseTransactionProc;
import de.unika.ipd.grgen.ir.stmt.procenv.RecordProc;
import de.unika.ipd.grgen.ir.stmt.procenv.ResumeTransactionProc;
import de.unika.ipd.grgen.ir.stmt.procenv.RollbackTransactionProc;
import de.unika.ipd.grgen.ir.stmt.procenv.StartTransactionProc;
import de.unika.ipd.grgen.ir.stmt.set.SetAddItem;
import de.unika.ipd.grgen.ir.stmt.set.SetClear;
import de.unika.ipd.grgen.ir.stmt.set.SetRemoveItem;
import de.unika.ipd.grgen.ir.stmt.set.SetVarAddItem;
import de.unika.ipd.grgen.ir.stmt.set.SetVarClear;
import de.unika.ipd.grgen.ir.stmt.set.SetVarRemoveItem;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.MatchType;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.basic.GraphType;
import de.unika.ipd.grgen.ir.type.basic.IntType;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.ir.type.container.DequeType;
import de.unika.ipd.grgen.ir.type.container.MapType;
import de.unika.ipd.grgen.ir.type.container.SetType;
import de.unika.ipd.grgen.util.Direction;
import de.unika.ipd.grgen.util.SourceBuilder;
import de.unika.ipd.grgen.ir.expr.Cast;
import de.unika.ipd.grgen.ir.expr.Constant;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.Operator;
import de.unika.ipd.grgen.ir.expr.ProjectionExpr;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.graph.AdjacentNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.EdgesExpr;
import de.unika.ipd.grgen.ir.expr.graph.IncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.NodesExpr;
import de.unika.ipd.grgen.ir.expr.graph.ReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.ReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.Visited;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.model.type.EnumType;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.pattern.NameOrAttributeInitialization;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.Variable;

public class ModifyEvalGen extends CSharpBase
{
	Model model;
	SearchPlanBackend2 be;

	int tmpVarID;

	ModifyExecGen execGen;

	public ModifyEvalGen(SearchPlanBackend2 backend, ModifyExecGen execGen,
			String nodeTypePrefix, String edgeTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
		model = be.unit.getActionsGraphModel();

		tmpVarID = 0;

		this.execGen = execGen;
	}

	//////////////////////////
	// Eval part generation //
	//////////////////////////

	public void genAllEvals(SourceBuilder sb, ModifyGenerationStateConst state,
			Collection<EvalStatements> evalStatements)
	{
		for(Node node : state.newNodes()) {
			if(node.hasAttributeInitialization()) {
				for(NameOrAttributeInitialization nai : node.nameOrAttributeInitialization) {
					if(nai.attribute == null) // skip name initialization
						continue;
					genAssignment(sb, state, new Assignment(new Qualification(nai.owner, nai.attribute), nai.expr));
				}
			}
		}
		for(Edge edge : state.newEdges()) {
			if(edge.hasAttributeInitialization()) {
				for(NameOrAttributeInitialization nai : edge.nameOrAttributeInitialization) {
					if(nai.attribute == null) // skip name initialization
						continue;
					genAssignment(sb, state, new Assignment(new Qualification(nai.owner, nai.attribute), nai.expr));
				}
			}
		}

		for(EvalStatements evalStmts : evalStatements) {
			sb.appendFront("{ // " + evalStmts.getName() + "\n");
			sb.indent();

			//if(be.system.mayFireDebugEvents()) {
			//	sb.append("\t\t\t((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering(");
			//	sb.append("\"" + state.name() + "." + evalStmts.getName() + "\"");
			//	sb.append(");\n");
			//}

			genEvals(sb, state, evalStmts.evalStatements);

			//if(be.system.mayFireDebugEvents()) {
			//	sb.append("\t\t\t((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting(");
			//	sb.append("\"" + state.name() + "." + evalStmts.getName() + "\"");
			//	sb.append(");\n");
			//}

			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genEvals(SourceBuilder sb, ModifyGenerationStateConst state, Collection<EvalStatement> evalStatements)
	{
		for(EvalStatement evalStmt : evalStatements) {
			genEvalStmt(sb, state, evalStmt);
		}
	}

	public void genEvalStmt(SourceBuilder sb, ModifyGenerationStateConst state, EvalStatement evalStmt)
	{
		if(evalStmt instanceof Assignment) { // includes evalStmt instanceof AssignmentIndexed
			genAssignment(sb, state, (Assignment)evalStmt);
		} else if(evalStmt instanceof AssignmentVar) { // includes evalStmt instanceof AssignmentVarIndexed
			genAssignmentVar(sb, state, (AssignmentVar)evalStmt);
		} else if(evalStmt instanceof AssignmentGraphEntity) {
			genAssignmentGraphEntity(sb, state, (AssignmentGraphEntity)evalStmt);
		} else if(evalStmt instanceof AssignmentMember) {
			// currently unused, would be needed for member assignment inside method without "this." prefix
			genAssignmentMember(sb, state, (AssignmentMember)evalStmt);
		} else if(evalStmt instanceof AssignmentVisited) {
			genAssignmentVisited(sb, state, (AssignmentVisited)evalStmt);
		} else if(evalStmt instanceof AssignmentNameof) {
			genAssignmentNameof(sb, state, (AssignmentNameof)evalStmt);
		} else if(evalStmt instanceof AssignmentIdentical) {
			//nothing to generate, was assignment . = . optimized away;
		} else if(evalStmt instanceof CompoundAssignmentChanged) {
			genCompoundAssignmentChanged(sb, state, (CompoundAssignmentChanged)evalStmt);
		} else if(evalStmt instanceof CompoundAssignmentChangedVar) {
			genCompoundAssignmentChangedVar(sb, state, (CompoundAssignmentChangedVar)evalStmt);
		} else if(evalStmt instanceof CompoundAssignmentChangedVisited) {
			genCompoundAssignmentChangedVisited(sb, state, (CompoundAssignmentChangedVisited)evalStmt);
		} else if(evalStmt instanceof CompoundAssignment) { // must come after the changed versions
			genCompoundAssignment(sb, state, (CompoundAssignment)evalStmt, "\t\t\t", ";\n");
		} else if(evalStmt instanceof CompoundAssignmentVarChanged) {
			genCompoundAssignmentVarChanged(sb, state, (CompoundAssignmentVarChanged)evalStmt);
		} else if(evalStmt instanceof CompoundAssignmentVarChangedVar) {
			genCompoundAssignmentVarChangedVar(sb, state, (CompoundAssignmentVarChangedVar)evalStmt);
		} else if(evalStmt instanceof CompoundAssignmentVarChangedVisited) {
			genCompoundAssignmentVarChangedVisited(sb, state, (CompoundAssignmentVarChangedVisited)evalStmt);
		} else if(evalStmt instanceof CompoundAssignmentVar) { // must come after the changed versions
			genCompoundAssignmentVar(sb, state, (CompoundAssignmentVar)evalStmt, "\t\t\t", ";\n");
		} else if(evalStmt instanceof MapRemoveItem) {
			genMapRemoveItem(sb, state, (MapRemoveItem)evalStmt);
		} else if(evalStmt instanceof MapClear) {
			genMapClear(sb, state, (MapClear)evalStmt);
		} else if(evalStmt instanceof MapAddItem) {
			genMapAddItem(sb, state, (MapAddItem)evalStmt);
		} else if(evalStmt instanceof SetRemoveItem) {
			genSetRemoveItem(sb, state, (SetRemoveItem)evalStmt);
		} else if(evalStmt instanceof SetClear) {
			genSetClear(sb, state, (SetClear)evalStmt);
		} else if(evalStmt instanceof SetAddItem) {
			genSetAddItem(sb, state, (SetAddItem)evalStmt);
		} else if(evalStmt instanceof ArrayRemoveItem) {
			genArrayRemoveItem(sb, state, (ArrayRemoveItem)evalStmt);
		} else if(evalStmt instanceof ArrayClear) {
			genArrayClear(sb, state, (ArrayClear)evalStmt);
		} else if(evalStmt instanceof ArrayAddItem) {
			genArrayAddItem(sb, state, (ArrayAddItem)evalStmt);
		} else if(evalStmt instanceof DequeRemoveItem) {
			genDequeRemoveItem(sb, state, (DequeRemoveItem)evalStmt);
		} else if(evalStmt instanceof DequeClear) {
			genDequeClear(sb, state, (DequeClear)evalStmt);
		} else if(evalStmt instanceof DequeAddItem) {
			genDequeAddItem(sb, state, (DequeAddItem)evalStmt);
		} else if(evalStmt instanceof MapVarRemoveItem) {
			genMapVarRemoveItem(sb, state, (MapVarRemoveItem)evalStmt);
		} else if(evalStmt instanceof MapVarClear) {
			genMapVarClear(sb, state, (MapVarClear)evalStmt);
		} else if(evalStmt instanceof MapVarAddItem) {
			genMapVarAddItem(sb, state, (MapVarAddItem)evalStmt);
		} else if(evalStmt instanceof SetVarRemoveItem) {
			genSetVarRemoveItem(sb, state, (SetVarRemoveItem)evalStmt);
		} else if(evalStmt instanceof SetVarClear) {
			genSetVarClear(sb, state, (SetVarClear)evalStmt);
		} else if(evalStmt instanceof SetVarAddItem) {
			genSetVarAddItem(sb, state, (SetVarAddItem)evalStmt);
		} else if(evalStmt instanceof ArrayVarRemoveItem) {
			genArrayVarRemoveItem(sb, state, (ArrayVarRemoveItem)evalStmt);
		} else if(evalStmt instanceof ArrayVarClear) {
			genArrayVarClear(sb, state, (ArrayVarClear)evalStmt);
		} else if(evalStmt instanceof ArrayVarAddItem) {
			genArrayVarAddItem(sb, state, (ArrayVarAddItem)evalStmt);
		} else if(evalStmt instanceof DequeVarRemoveItem) {
			genDequeVarRemoveItem(sb, state, (DequeVarRemoveItem)evalStmt);
		} else if(evalStmt instanceof DequeVarClear) {
			genDequeVarClear(sb, state, (DequeVarClear)evalStmt);
		} else if(evalStmt instanceof DequeVarAddItem) {
			genDequeVarAddItem(sb, state, (DequeVarAddItem)evalStmt);
		} else if(evalStmt instanceof ReturnStatementFilter) {
			genReturnStatementFilter(sb, state, (ReturnStatementFilter)evalStmt);
		} else if(evalStmt instanceof ReturnStatement) {
			genReturnStatement(sb, state, (ReturnStatement)evalStmt);
		} else if(evalStmt instanceof ReturnStatementProcedure) {
			genReturnStatementProcedure(sb, state, (ReturnStatementProcedure)evalStmt);
		} else if(evalStmt instanceof ConditionStatement) {
			genConditionStatement(sb, state, (ConditionStatement)evalStmt);
		} else if(evalStmt instanceof SwitchStatement) {
			genSwitchStatement(sb, state, (SwitchStatement)evalStmt);
		} else if(evalStmt instanceof WhileStatement) {
			genWhileStatement(sb, state, (WhileStatement)evalStmt);
		} else if(evalStmt instanceof DoWhileStatement) {
			genDoWhileStatement(sb, state, (DoWhileStatement)evalStmt);
		} else if(evalStmt instanceof MultiStatement) {
			genMultiStatement(sb, state, (MultiStatement)evalStmt);
		} else if(evalStmt instanceof DefDeclVarStatement) {
			genDefDeclVarStatement(sb, state, (DefDeclVarStatement)evalStmt);
		} else if(evalStmt instanceof DefDeclGraphEntityStatement) {
			genDefDeclGraphEntityStatement(sb, state, (DefDeclGraphEntityStatement)evalStmt);
		} else if(evalStmt instanceof ContainerAccumulationYield) {
			genContainerAccumulationYield(sb, state, (ContainerAccumulationYield)evalStmt);
		} else if(evalStmt instanceof IntegerRangeIterationYield) {
			genIntegerRangeIterationYield(sb, state, (IntegerRangeIterationYield)evalStmt);
		} else if(evalStmt instanceof MatchesAccumulationYield) {
			genMatchesAccumulationYield(sb, state, (MatchesAccumulationYield)evalStmt);
		} else if(evalStmt instanceof ForFunction) {
			genForFunction(sb, state, (ForFunction)evalStmt);
		} else if(evalStmt instanceof ForIndexAccessEquality) {
			genForIndexAccessEquality(sb, state, (ForIndexAccessEquality)evalStmt);
		} else if(evalStmt instanceof ForIndexAccessOrdering) {
			genForIndexAccessOrdering(sb, state, (ForIndexAccessOrdering)evalStmt);
		} else if(evalStmt instanceof BreakStatement) {
			genBreakStatement(sb, state, (BreakStatement)evalStmt);
		} else if(evalStmt instanceof ContinueStatement) {
			genContinueStatement(sb, state, (ContinueStatement)evalStmt);
		} else if(evalStmt instanceof ExecStatement) {
			execGen.genExecStatement(sb, state, (ExecStatement)evalStmt);
		} else if(evalStmt instanceof ReturnAssignment) {
			genReturnAssignment(sb, state, (ReturnAssignment)evalStmt); // contains the procedure and method invocations
		} else {
			throw new UnsupportedOperationException("Unexpected eval statement \"" + evalStmt + "\"");
		}
	}

	private void genAssignment(SourceBuilder sb, ModifyGenerationStateConst state, Assignment ass)
	{
		Qualification target = ass.getTarget();
		Expression expr = ass.getExpression();
		Type targetType = target.getType();

		if((targetType instanceof MapType
				|| targetType instanceof SetType
				|| targetType instanceof ArrayType
				|| targetType instanceof DequeType)
				&& !(ass instanceof AssignmentIndexed)) {
			// Check whether we have to make a copy of the right hand side of the assignment
			boolean mustCopy = true;
			if(expr instanceof Operator) {
				Operator op = (Operator)expr;

				// For unions and intersections new maps/sets are already created,
				// so we don't have to copy them again
				if(op.getOpCode() == Operator.BIT_OR || op.getOpCode() == Operator.BIT_AND)
					mustCopy = false;
			}

			String typeName = formatAttributeType(targetType);
			String varName = "tempvar_" + tmpVarID++;
			sb.appendFront(typeName + " " + varName + " = ");
			if(mustCopy)
				sb.append("new " + typeName + "(");
			genExpression(sb, expr, state);
			if(mustCopy)
				sb.append(")");
			sb.append(";\n");

			genChangingAttribute(sb, state, target, "Assign", varName, "null");

			sb.appendFront("");
			genExpression(sb, target, state); // global var case handled by genQualAccess
			sb.append(" = " + varName + ";\n");

			genChangedAttribute(sb, state, target);

			return;
		}

		// indexed assignment to array/deque/map, the target type is the array/deque/map value type
		if(ass instanceof AssignmentIndexed && targetType instanceof ArrayType) {
			targetType = ((ArrayType)targetType).getValueType();
		}
		if(ass instanceof AssignmentIndexed && targetType instanceof DequeType) {
			targetType = ((DequeType)targetType).getValueType();
		}
		if(ass instanceof AssignmentIndexed && targetType instanceof MapType) {
			targetType = ((MapType)targetType).getValueType();
		}

		String varName = "tempvar_" + tmpVarID++;
		String varType = getTypeNameForTempVarDecl(targetType) + " ";

		sb.appendFront(varType + varName + " = ");
		if(targetType instanceof EnumType)
			sb.append("(int) ");
		else
			sb.append("(" + varType + ")");
		genExpression(sb, expr, state);
		sb.append(";\n");

		if(ass instanceof AssignmentIndexed) {
			AssignmentIndexed assIdx = (AssignmentIndexed)ass;

			if(target.getType() instanceof ArrayType
					|| target.getType() instanceof DequeType) {
				String indexType = "int ";
				String indexName = "tempvar_index" + tmpVarID++;
				sb.appendFront(indexType + indexName + " = (int)");
				genExpression(sb, assIdx.getIndex(), state);
				sb.append(";\n");

				sb.appendFront("if(" + indexName + " < ");
				genExpression(sb, target, state);
				sb.append(".Count) {\n");
				sb.indent();

				sb.appendFront("");
				genChangingAttribute(sb, state, target, "AssignElement", varName, indexName);

				sb.appendFront("");
				genExpression(sb, target, state); // global var case handled by genQualAccess
				sb.append("[");
				sb.append(indexName);
				sb.append("]");
			} else { //if(target.getType() instanceof MapType)
				String indexType = formatType(((MapType)target.getType()).getKeyType()) + " ";
				String indexName = "tempvar_index_" + tmpVarID++;
				sb.appendFront(indexType + indexName + " = ");
				if(targetType instanceof EnumType)
					sb.append("(int) ");
				else
					sb.append("(" + indexType + ")");
				genExpression(sb, assIdx.getIndex(), state);
				sb.append(";\n");

				sb.appendFront("if(");
				genExpression(sb, target, state);
				sb.append(".ContainsKey(");
				sb.append(indexName);
				sb.append(")) {\n");
				sb.indent();

				sb.appendFront("");
				genChangingAttribute(sb, state, target, "AssignElement", varName, indexName);

				sb.appendFront("");
				genExpression(sb, target, state); // global var case handled by genQualAccess
				sb.append("[");
				sb.append(indexName);
				sb.append("]");
			}

			sb.append(" = ");
			if(targetType instanceof EnumType)
				sb.append("(GRGEN_MODEL." + getPackagePrefixDot(targetType) + "ENUM_" + formatIdentifiable(targetType) + ") ");
			sb.append(varName + ";\n");

			genChangedAttribute(sb, state, target);

			sb.unindent();
			sb.appendFront("}\n");
		} else {
			if(!(target.getOwner().getType() instanceof MatchType
					|| target.getOwner().getType() instanceof DefinedMatchType))
				genChangingAttribute(sb, state, target, "Assign", varName, "null");

			sb.appendFront("");
			genExpression(sb, target, state); // global var case handled by genQualAccess

			sb.append(" = ");
			if(targetType instanceof EnumType)
				sb.append("(GRGEN_MODEL." + getPackagePrefixDot(targetType) + "ENUM_" + formatIdentifiable(targetType) + ") ");
			sb.append(varName + ";\n");

			if(!(target.getOwner().getType() instanceof MatchType
					|| target.getOwner().getType() instanceof DefinedMatchType))
				genChangedAttribute(sb, state, target);
		}
	}

	private void genAssignmentVar(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentVar ass)
	{
		Variable target = ass.getTarget();
		Expression expr = ass.getExpression();

		Type targetType = target.getType();
		if(ass instanceof AssignmentVarIndexed) {
			if(targetType instanceof ArrayType)
				targetType = ((ArrayType)target.getType()).getValueType();
			else if(targetType instanceof DequeType)
				targetType = ((DequeType)target.getType()).getValueType();
			else // targetType instanceof MapType
				targetType = ((MapType)target.getType()).getValueType();
		}

		Type indexType = IntType.getType();
		if(target.getType() instanceof MapType)
			indexType = ((MapType)target.getType()).getKeyType();

		sb.appendFront("");
		if(!Expression.isGlobalVariable(target)) {
			sb.append(formatEntity(target));
			if(ass instanceof AssignmentVarIndexed) {
				AssignmentVarIndexed assIdx = (AssignmentVarIndexed)ass;
				Expression index = assIdx.getIndex();
				sb.append("[(" + formatType(indexType) + ") (");
				genExpression(sb, index, state);
				sb.append(")]");
			}

			sb.append(" = ");
			sb.append("(" + formatType(targetType) + ") (");
			genExpression(sb, expr, state);
			sb.append(");\n");
		} else {
			if(ass instanceof AssignmentVarIndexed) {
				AssignmentVarIndexed assIdx = (AssignmentVarIndexed)ass;
				sb.append(formatGlobalVariableRead(target));
				Expression index = assIdx.getIndex();
				sb.append("[(" + formatType(indexType) + ") (");
				genExpression(sb, index, state);
				sb.append(")]");

				sb.append(" = ");
				sb.append("(" + formatType(targetType) + ") (");
				genExpression(sb, expr, state);
				sb.append(");\n");
			} else {
				SourceBuilder tmp = new SourceBuilder();
				tmp.append("(" + formatType(targetType) + ") (");
				genExpression(tmp, expr, state);
				tmp.append(")");
				sb.append(formatGlobalVariableWrite(target, tmp.toString()));
				sb.append(";\n");
			}
		}
	}

	private void genAssignmentGraphEntity(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentGraphEntity ass)
	{
		GraphEntity target = ass.getTarget();
		Expression expr = ass.getExpression();

		sb.appendFront("");
		if(!Expression.isGlobalVariable(target)) {
			sb.append(formatEntity(target));
			sb.append(" = ");
			if((target.getContext() & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION) {
				if(target instanceof Node)
					sb.append("(GRGEN_LGSP.LGSPNode)");
				else
					sb.append("(GRGEN_LGSP.LGSPEdge)");
			}
			genExpression(sb, expr, state);
			sb.append(";\n");
		} else {
			SourceBuilder tmp = new SourceBuilder();
			genExpression(tmp, expr, state);
			sb.append(formatGlobalVariableWrite(target, tmp.toString()));
			sb.append(";\n");
		}
	}

	private void genAssignmentMember(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentMember ass)
	{
		Entity target = ass.getTarget();
		Expression expr = ass.getExpression();

		sb.appendFront("");
		if(!Expression.isGlobalVariable(target)) {
			genMemberAccess(sb, target);
			sb.append(" = ");
			if((target.getContext() & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION) {
				sb.append("(" + formatType(target.getType()) + ")");
			}
			genExpression(sb, expr, state);
			sb.append(";\n");
		} else {
			SourceBuilder tmp = new SourceBuilder();
			genExpression(tmp, expr, state);
			sb.append(formatGlobalVariableWrite(target, tmp.toString()));
			sb.append(";\n");
		}
	}

	private void genAssignmentVisited(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentVisited ass)
	{
		sb.appendFront("graph.SetVisited(");
		genExpression(sb, ass.getTarget().getEntity(), state);
		sb.append(", ");
		genExpression(sb, ass.getTarget().getVisitorID(), state);
		sb.append(", ");
		genExpression(sb, ass.getExpression(), state);
		sb.append(");\n");
	}

	private void genAssignmentNameof(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentNameof ass)
	{
		if(ass.getTarget() == null || ass.getTarget().getType() instanceof GraphType) {
			if(ass.getTarget() == null)
				sb.appendFront("graph.Name = ");
			else {
				sb.appendFront("(");
				genExpression(sb, ass.getTarget(), state);
				sb.append(").Name = ");
			}
			genExpression(sb, ass.getExpression(), state);
			sb.append(";\n");
		} else {
			sb.appendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).SetElementName(");
			genExpression(sb, ass.getTarget(), state);
			sb.append(", ");
			genExpression(sb, ass.getExpression(), state);
			sb.append(");\n");
		}
	}

	private void genCompoundAssignmentChanged(SourceBuilder sb, ModifyGenerationStateConst state,
			CompoundAssignmentChanged cass)
	{
		Qualification changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation() == CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation() == CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";

		Entity owner = cass.getTarget().getOwner();
		boolean isDeletedElem = owner instanceof Node
				? state.delNodes().contains(owner)
				: state.delEdges().contains(owner);
		if(!isDeletedElem && be.system.mayFireEvents()) {
			owner = changedTarget.getOwner();
			isDeletedElem = owner instanceof Node ? state.delNodes().contains(owner) : state.delEdges().contains(owner);
			if(!isDeletedElem && be.system.mayFireEvents()) {
				String varName = "tempvar_" + tmpVarID++;
				String varType = "bool ";

				sb.appendFront(varType + varName + " = ");
				genExpression(sb, changedTarget, state);
				sb.append(";\n");

				String prefix = sb.getIndent() + varName + changedOperation;

				genCompoundAssignment(sb, state, cass, prefix, ";\n");

				genChangingAttribute(sb, state, changedTarget, "Assign", varName, "null");

				sb.appendFront("");
				genExpression(sb, changedTarget, state);
				sb.append(" = " + varName + ";\n");

				genChangedAttribute(sb, state, changedTarget);
			} else {
				genCompoundAssignment(sb, state, cass, sb.getIndent(), ";\n");
			}
		}
	}

	private void genCompoundAssignmentChangedVar(SourceBuilder sb, ModifyGenerationStateConst state,
			CompoundAssignmentChangedVar cass)
	{
		Variable changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation() == CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation() == CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";

		String prefix = sb.getIndent() + formatEntity(changedTarget) + changedOperation;

		genCompoundAssignment(sb, state, cass, prefix, ";\n");
	}

	private void genCompoundAssignmentChangedVisited(SourceBuilder sb, ModifyGenerationStateConst state,
			CompoundAssignmentChangedVisited cass)
	{
		Visited changedTarget = cass.getChangedTarget();

		SourceBuilder changedTargetBuffer = new SourceBuilder();
		genExpression(changedTargetBuffer, changedTarget.getEntity(), state);
		changedTargetBuffer.append(", ");
		genExpression(changedTargetBuffer, changedTarget.getVisitorID(), state);

		String prefix = sb.getIndent() + "graph.SetVisited("
				+ changedTargetBuffer.toString() + ", ";
		if(cass.getChangedOperation() != CompoundAssignment.ASSIGN) {
			prefix += "graph.IsVisited(" + changedTargetBuffer.toString() + ")"
					+ (cass.getChangedOperation() == CompoundAssignment.UNION ? " | " : " & ");
		}

		genCompoundAssignment(sb, state, cass, prefix, ");\n");
	}

	private void genCompoundAssignment(SourceBuilder sb, ModifyGenerationStateConst state, CompoundAssignment cass,
			String prefix, String postfix)
	{
		Qualification target = cass.getTarget();
		assert(target.getType() instanceof MapType || target.getType() instanceof SetType
				|| target.getType() instanceof ArrayType || target.getType() instanceof DequeType);
		Expression expr = cass.getExpression();

		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		boolean isDeletedElem = element instanceof Node
				? state.delNodes().contains(element)
				: state.delEdges().contains(element);
		if(!isDeletedElem && be.system.mayFireEvents()) {
			sb.append(prefix);
			if(cass.getOperation() == CompoundAssignment.UNION)
				sb.append("GRGEN_LIBGR.ContainerHelper.UnionChanged(");
			else if(cass.getOperation() == CompoundAssignment.INTERSECTION)
				sb.append("GRGEN_LIBGR.ContainerHelper.IntersectChanged(");
			else if(cass.getOperation() == CompoundAssignment.WITHOUT)
				sb.append("GRGEN_LIBGR.ContainerHelper.ExceptChanged(");
			else //if(cass.getOperation()==CompoundAssignment.CONCATENATE)
				sb.append("GRGEN_LIBGR.ContainerHelper.ConcatenateChanged(");
			genExpression(sb, target, state);
			sb.append(", ");
			genExpression(sb, expr, state);
			sb.append(", ");
			sb.append("graph, "
					+ formatEntity(element) + ", "
					+ formatTypeClassRef(elementType) + "." + formatAttributeTypeName(attribute));
			sb.append(")");
			sb.append(postfix);
		}
	}

	private void genCompoundAssignmentVarChanged(SourceBuilder sb, ModifyGenerationStateConst state,
			CompoundAssignmentVarChanged cass)
	{
		Qualification changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation() == CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation() == CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";

		Entity owner = changedTarget.getOwner();
		boolean isDeletedElem = owner instanceof Node
				? state.delNodes().contains(owner)
				: state.delEdges().contains(owner);
		if(!isDeletedElem && be.system.mayFireEvents()) {
			String varName = "tempvar_" + tmpVarID++;
			String varType = "bool ";

			sb.appendFront(varType + varName + " = ");
			genExpression(sb, changedTarget, state);
			sb.append(";\n");

			String prefix = sb.getIndent() + varName + changedOperation;

			genCompoundAssignmentVar(sb, state, cass, prefix, ";\n");

			genChangingAttribute(sb, state, changedTarget, "Assign", varName, "null");

			sb.appendFront("");
			genExpression(sb, changedTarget, state);
			sb.append(" = " + varName + ";\n");

			genChangedAttribute(sb, state, changedTarget);
		} else {
			genCompoundAssignmentVar(sb, state, cass, sb.getIndent(), ";\n");
		}
	}

	private void genCompoundAssignmentVarChangedVar(SourceBuilder sb, ModifyGenerationStateConst state,
			CompoundAssignmentVarChangedVar cass)
	{
		Variable changedTarget = cass.getChangedTarget();
		String changedOperation;
		if(cass.getChangedOperation() == CompoundAssignment.UNION)
			changedOperation = " |= ";
		else if(cass.getChangedOperation() == CompoundAssignment.INTERSECTION)
			changedOperation = " &= ";
		else //if(cass.getChangedOperation()==CompoundAssignment.ASSIGN)
			changedOperation = " = ";

		String prefix = sb.getIndent() + formatEntity(changedTarget) + changedOperation;

		genCompoundAssignmentVar(sb, state, cass, prefix, ";\n");
	}

	private void genCompoundAssignmentVarChangedVisited(SourceBuilder sb, ModifyGenerationStateConst state,
			CompoundAssignmentVarChangedVisited cass)
	{
		Visited changedTarget = cass.getChangedTarget();

		SourceBuilder changedTargetBuffer = new SourceBuilder();
		genExpression(changedTargetBuffer, changedTarget.getEntity(), state);
		changedTargetBuffer.append(", ");
		genExpression(changedTargetBuffer, changedTarget.getVisitorID(), state);

		String prefix = sb.getIndent() + "graph.SetVisited("
				+ changedTargetBuffer.toString() + ", ";
		if(cass.getChangedOperation() != CompoundAssignment.ASSIGN) {
			prefix += "graph.IsVisited(" + changedTargetBuffer.toString() + ")"
					+ (cass.getChangedOperation() == CompoundAssignment.UNION ? " | " : " & ");
		}

		genCompoundAssignmentVar(sb, state, cass, prefix, ");\n");
	}

	private void genCompoundAssignmentVar(SourceBuilder sb, ModifyGenerationStateConst state,
			CompoundAssignmentVar cass, String prefix, String postfix)
	{
		Variable target = cass.getTarget();
		assert(target.getType() instanceof MapType || target.getType() instanceof SetType
				|| target.getType() instanceof ArrayType || target.getType() instanceof DequeType);
		Expression expr = cass.getExpression();

		sb.append(prefix);
		if(cass.getOperation() == CompoundAssignment.UNION)
			sb.append("GRGEN_LIBGR.ContainerHelper.UnionChanged(");
		else if(cass.getOperation() == CompoundAssignment.INTERSECTION)
			sb.append("GRGEN_LIBGR.ContainerHelper.IntersectChanged(");
		else if(cass.getOperation() == CompoundAssignment.WITHOUT)
			sb.append("GRGEN_LIBGR.ContainerHelper.ExceptChanged(");
		else //if(cass.getOperation()==CompoundAssignment.CONCATENATE)
			sb.append("GRGEN_LIBGR.ContainerHelper.ConcatenateChanged(");
		sb.append(formatEntity(target));
		sb.append(", ");
		genExpression(sb, expr, state);
		sb.append(")");
		sb.append(postfix);
	}

	private void genMapRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, MapRemoveItem mri)
	{
		Qualification target = mri.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, mri.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "RemoveElement", "null", keyExprStr);

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append(".Remove(");
		if(mri.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mri.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append(");\n");

		genChangedAttribute(sb, state, target);

		if(mri.getNext() != null) {
			genEvalStmt(sb, state, mri.getNext());
		}
	}

	private void genMapClear(SourceBuilder sb, ModifyGenerationStateConst state, MapClear mc)
	{
		Qualification target = mc.getTarget();

		genClearAttribute(sb, state, target);

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		genClearedAttribute(sb, state, target);

		if(mc.getNext() != null) {
			genEvalStmt(sb, state, mc.getNext());
		}
	}

	private void genMapAddItem(SourceBuilder sb, ModifyGenerationStateConst state, MapAddItem mai)
	{
		Qualification target = mai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, mai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();
		sbtmp.delete(0, sbtmp.length());
		genExpression(sbtmp, mai.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, keyExprStr);

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append("[");
		if(mai.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mai.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append("] = ");
		if(mai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(";\n");

		genChangedAttribute(sb, state, target);

		if(mai.getNext() != null) {
			genEvalStmt(sb, state, mai.getNext());
		}
	}

	private void genSetRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, SetRemoveItem sri)
	{
		Qualification target = sri.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, sri.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "RemoveElement", valueExprStr, "null");

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append(".Remove(");
		if(sri.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(sri.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		genChangedAttribute(sb, state, target);

		if(sri.getNext() != null) {
			genEvalStmt(sb, state, sri.getNext());
		}
	}

	private void genSetClear(SourceBuilder sb, ModifyGenerationStateConst state, SetClear sc)
	{
		Qualification target = sc.getTarget();

		genClearAttribute(sb, state, target);

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		genClearedAttribute(sb, state, target);

		if(sc.getNext() != null) {
			genEvalStmt(sb, state, sc.getNext());
		}
	}

	private void genSetAddItem(SourceBuilder sb, ModifyGenerationStateConst state, SetAddItem sai)
	{
		Qualification target = sai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, sai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, "null");

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append("[");
		if(sai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(sai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append("] = null;\n");

		genChangedAttribute(sb, state, target);

		if(sai.getNext() != null) {
			genEvalStmt(sb, state, sai.getNext());
		}
	}

	private void genArrayRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayRemoveItem ari)
	{
		Qualification target = ari.getTarget();

		String indexStr = "null";
		if(ari.getIndexExpr() != null) {
			SourceBuilder sbtmp = new SourceBuilder();
			genExpression(sbtmp, ari.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}

		genChangingAttribute(sb, state, target, "RemoveElement", "null", indexStr);

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append(".RemoveAt(");
		if(ari.getIndexExpr() != null) {
			sb.append(indexStr);
		} else {
			sb.append("(");
			genExpression(sb, target, state);
			sb.append(").Count - 1");
		}
		sb.append(");\n");

		genChangedAttribute(sb, state, target);

		if(ari.getNext() != null) {
			genEvalStmt(sb, state, ari.getNext());
		}
	}

	private void genArrayClear(SourceBuilder sb, ModifyGenerationStateConst state, ArrayClear ac)
	{
		Qualification target = ac.getTarget();

		genClearAttribute(sb, state, target);

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		genClearedAttribute(sb, state, target);

		if(ac.getNext() != null) {
			genEvalStmt(sb, state, ac.getNext());
		}
	}

	private void genArrayAddItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayAddItem aai)
	{
		Qualification target = aai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, aai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sbtmp = new SourceBuilder();
		String indexExprStr = "null";
		if(aai.getIndexExpr() != null) {
			genExpression(sbtmp, aai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, indexExprStr);

		sb.appendFront("");
		genExpression(sb, target, state);
		if(aai.getIndexExpr() == null) {
			sb.append(".Add(");
		} else {
			sb.append(".Insert(");
			sb.append(indexExprStr);
			sb.append(", ");
		}
		if(aai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(aai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		genChangedAttribute(sb, state, target);

		if(aai.getNext() != null) {
			genEvalStmt(sb, state, aai.getNext());
		}
	}

	private void genDequeRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeRemoveItem dri)
	{
		Qualification target = dri.getTarget();

		String indexStr = "null";
		if(dri.getIndexExpr() != null) {
			SourceBuilder sbtmp = new SourceBuilder();
			genExpression(sbtmp, dri.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}

		genChangingAttribute(sb, state, target, "RemoveElement", "null", indexStr);

		sb.appendFront("");
		genExpression(sb, target, state);
		if(dri.getIndexExpr() != null) {
			sb.append(".DequeueAt(" + indexStr + ");\n");
		} else {
			sb.append(".Dequeue();\n");
		}

		genChangedAttribute(sb, state, target);

		if(dri.getNext() != null) {
			genEvalStmt(sb, state, dri.getNext());
		}
	}

	private void genDequeClear(SourceBuilder sb, ModifyGenerationStateConst state, DequeClear dc)
	{
		Qualification target = dc.getTarget();

		genClearAttribute(sb, state, target);

		sb.appendFront("");
		genExpression(sb, target, state);
		sb.append(".Clear();\n");

		genClearedAttribute(sb, state, target);

		if(dc.getNext() != null) {
			genEvalStmt(sb, state, dc.getNext());
		}
	}

	private void genDequeAddItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeAddItem dai)
	{
		Qualification target = dai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, dai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sbtmp = new SourceBuilder();
		String indexExprStr = "null";
		if(dai.getIndexExpr() != null) {
			genExpression(sbtmp, dai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		genChangingAttribute(sb, state, target, "PutElement", valueExprStr, indexExprStr);

		sb.appendFront("");
		genExpression(sb, target, state);
		if(dai.getIndexExpr() == null) {
			sb.append(".Enqueue(");
		} else {
			sb.append(".EnqueueAt(");
			sb.append(indexExprStr);
			sb.append(", ");
		}
		if(dai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(dai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		genChangedAttribute(sb, state, target);

		if(dai.getNext() != null) {
			genEvalStmt(sb, state, dai.getNext());
		}
	}

	private void genMapVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, MapVarRemoveItem mvri)
	{
		Variable target = mvri.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, mvri.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		genVar(sb, target, state);
		sb.append(".Remove(");
		if(mvri.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mvri.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append(");\n");

		assert mvri.getNext() == null;
	}

	private void genMapVarClear(SourceBuilder sb, ModifyGenerationStateConst state, MapVarClear mvc)
	{
		Variable target = mvc.getTarget();

		genVar(sb, target, state);
		sb.append(".Clear();\n");

		assert mvc.getNext() == null;
	}

	private void genMapVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, MapVarAddItem mvai)
	{
		Variable target = mvai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, mvai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();
		sbtmp.delete(0, sbtmp.length());
		genExpression(sbtmp, mvai.getKeyExpr(), state);
		String keyExprStr = sbtmp.toString();

		genVar(sb, target, state);
		sb.append("[");
		if(mvai.getKeyExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mvai.getKeyExpr().getType()) + ")(" + keyExprStr + ")");
		else
			sb.append(keyExprStr);
		sb.append("] = ");
		if(mvai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(mvai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(";\n");

		assert mvai.getNext() == null;
	}

	private void genSetVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, SetVarRemoveItem svri)
	{
		Variable target = svri.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, svri.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		genVar(sb, target, state);
		sb.append(".Remove(");
		if(svri.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(svri.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		assert svri.getNext() == null;
	}

	private void genSetVarClear(SourceBuilder sb, ModifyGenerationStateConst state, SetVarClear svc)
	{
		Variable target = svc.getTarget();

		genVar(sb, target, state);
		sb.append(".Clear();\n");

		assert svc.getNext() == null;
	}

	private void genSetVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, SetVarAddItem svai)
	{
		Variable target = svai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, svai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		genVar(sb, target, state);
		sb.append("[");
		if(svai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(svai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append("] = null;\n");

		assert svai.getNext() == null;
	}

	private void genArrayVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayVarRemoveItem avri)
	{
		Variable target = avri.getTarget();

		String indexStr = "null";
		if(avri.getIndexExpr() != null) {
			SourceBuilder sbtmp = new SourceBuilder();
			genExpression(sbtmp, avri.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}

		genVar(sb, target, state);
		sb.append(".RemoveAt(");

		if(avri.getIndexExpr() != null) {
			sb.append(indexStr);
		} else {
			sb.append("(");
			sb.append(formatEntity(target));
			sb.append(").Count - 1");
		}
		sb.append(");\n");

		assert avri.getNext() == null;
	}

	private void genArrayVarClear(SourceBuilder sb, ModifyGenerationStateConst state, ArrayVarClear avc)
	{
		Variable target = avc.getTarget();

		genVar(sb, target, state);
		sb.append(".Clear();\n");

		assert avc.getNext() == null;
	}

	private void genArrayVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayVarAddItem avai)
	{
		Variable target = avai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, avai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sbtmp = new SourceBuilder();
		String indexExprStr = "null";
		if(avai.getIndexExpr() != null) {
			genExpression(sbtmp, avai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		genVar(sb, target, state);
		if(avai.getIndexExpr() == null) {
			sb.append(".Add(");
		} else {
			sb.append(".Insert(");
			sb.append(indexExprStr);
			sb.append(", ");
		}
		if(avai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(avai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		assert avai.getNext() == null;
	}

	private void genDequeVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeVarRemoveItem dvri)
	{
		Variable target = dvri.getTarget();

		String indexStr = "null";
		if(dvri.getIndexExpr() != null) {
			SourceBuilder sbtmp = new SourceBuilder();
			genExpression(sbtmp, dvri.getIndexExpr(), state);
			indexStr = sbtmp.toString();
		}

		genVar(sb, target, state);
		if(dvri.getIndexExpr() != null) {
			sb.append(".DequeueAt(" + indexStr + ");\n");
		} else {
			sb.append(".Dequeue();\n");
		}

		assert dvri.getNext() == null;
	}

	private void genDequeVarClear(SourceBuilder sb, ModifyGenerationStateConst state, DequeVarClear dvc)
	{
		Variable target = dvc.getTarget();

		genVar(sb, target, state);
		sb.append(".Clear();\n");

		assert dvc.getNext() == null;
	}

	private void genDequeVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeVarAddItem dvai)
	{
		Variable target = dvai.getTarget();

		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, dvai.getValueExpr(), state);
		String valueExprStr = sbtmp.toString();

		sbtmp = new SourceBuilder();
		String indexExprStr = "null";
		if(dvai.getIndexExpr() != null) {
			genExpression(sbtmp, dvai.getIndexExpr(), state);
			indexExprStr = sbtmp.toString();
		}

		genVar(sb, target, state);
		if(dvai.getIndexExpr() == null) {
			sb.append(".Enqueue(");
		} else {
			sb.append(".EnqueueAt(");
			sb.append(indexExprStr);
			sb.append(", ");
		}
		if(dvai.getValueExpr() instanceof GraphEntityExpression)
			sb.append("(" + formatElementInterfaceRef(dvai.getValueExpr().getType()) + ")(" + valueExprStr + ")");
		else
			sb.append(valueExprStr);
		sb.append(");\n");

		assert dvai.getNext() == null;
	}

	private void genVar(SourceBuilder sb, Variable var, ModifyGenerationStateConst state)
	{
		if(!Expression.isGlobalVariable(var)) {
			sb.appendFront(formatEntity(var));
		} else {
			sb.append(formatGlobalVariableRead(var));
		}
	}

	private void genReturnStatementFilter(SourceBuilder sb, ModifyGenerationStateConst state, ReturnStatementFilter rsf)
	{
		if(state.matchClassName() != null)
			sb.appendFront("GRGEN_LIBGR.MatchListHelper.FromList(matches, this_matches);\n");
		else
			sb.appendFront("matches.FromListExact();\n");
		sb.appendFront("return;\n");
	}

	private void genReturnStatement(SourceBuilder sb, ModifyGenerationStateConst state, ReturnStatement rs)
	{
		sb.appendFront("return ");
		genExpression(sb, rs.getReturnValueExpr(), state);
		sb.append(";\n");
	}

	private void genReturnStatementProcedure(SourceBuilder sb, ModifyGenerationStateConst state,
			ReturnStatementProcedure rsp)
	{
		int i = 0;
		for(Expression returnValueExpr : rsp.getReturnValueExpr()) {
			sb.appendFront("_out_param_" + i + " = ");
			genExpression(sb, returnValueExpr, state);
			sb.append(";\n");
			++i;
		}
		if(be.system.mayFireDebugEvents()) {
			sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting(");
			sb.append("\"" + state.name() + "\"");
			for(int j = 0; j < i; ++j) {
				sb.append(", _out_param_" + j);
			}
			sb.append(");\n");
		}
		sb.appendFront("return;\n");
	}

	private void genConditionStatement(SourceBuilder sb, ModifyGenerationStateConst state, ConditionStatement cs)
	{
		sb.appendFront("if(");
		genExpression(sb, cs.getConditionExpr(), state);
		sb.append(") {\n");
		sb.indent();
		genEvals(sb, state, cs.getTrueCaseStatements());
		if(cs.getFalseCaseStatements() != null) {
			sb.unindent();
			sb.appendFront("} else {\n");
			sb.indent();
			genEvals(sb, state, cs.getFalseCaseStatements());
		}
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genSwitchStatement(SourceBuilder sb, ModifyGenerationStateConst state, SwitchStatement ss)
	{
		sb.appendFront("switch(");
		genExpression(sb, ss.getSwitchExpr(), state);
		sb.append(") {\n");
		for(CaseStatement cs : ss.getStatements()) {
			genCaseStatement(sb, state, cs);
		}
		sb.append("}\n");
	}

	private void genCaseStatement(SourceBuilder sb, ModifyGenerationStateConst state, CaseStatement cs)
	{
		if(cs.getCaseConstantExpr() != null) {
			sb.appendFront("case ");
			genExpression(sb, cs.getCaseConstantExpr(), state);
			sb.append(": ");
		} else {
			sb.appendFront("default: ");
		}
		sb.append("{\n");
		sb.indent();
		genEvals(sb, state, cs.getStatements());
		sb.appendFront("break;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genWhileStatement(SourceBuilder sb, ModifyGenerationStateConst state, WhileStatement ws)
	{
		sb.appendFront("while(");
		genExpression(sb, ws.getConditionExpr(), state);
		sb.append(") {\n");
		sb.indent();
		genEvals(sb, state, ws.getLoopedStatements());
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genDoWhileStatement(SourceBuilder sb, ModifyGenerationStateConst state, DoWhileStatement dws)
	{
		sb.appendFront("do {\n");
		sb.indent();
		genEvals(sb, state, dws.getLoopedStatements());
		sb.unindent();
		sb.appendFront("} while(");
		genExpression(sb, dws.getConditionExpr(), state);
		sb.append(");\n");
	}

	private void genMultiStatement(SourceBuilder sb, ModifyGenerationStateConst state, MultiStatement ms)
	{
		genEvals(sb, state, ms.getStatements());
	}

	private void genDefDeclVarStatement(SourceBuilder sb, ModifyGenerationStateConst state, DefDeclVarStatement ddvs)
	{
		Variable var = ddvs.getTarget();
		if(var.getIdent().toString().equals("this") && var.getType() instanceof ArrayType) {
			if(state.matchClassName() != null)
				sb.appendFront(formatType(var.getType()) + " this_matches = GRGEN_LIBGR.MatchListHelper.ToList<"
						+ state.packagePrefix() + "IMatch_" + state.matchClassName() + ">(matches);\n");
			else
				sb.appendFront(formatType(var.getType()) + " this_matches = matches.ToListExact();\n");
			return;
		}
		sb.appendFront(formatType(var.getType()) + " " + formatEntity(var));
		if(var.initialization != null) {
			sb.append(" = ");
			sb.append("(" + formatType(var.getType()) + ")(");
			genExpression(sb, var.initialization, state);
			sb.append(")");
		} else {
			sb.append(" = " + getInitializationValue(var.getType()));
		}
		sb.append(";\n");
	}

	private void genDefDeclGraphEntityStatement(SourceBuilder sb, ModifyGenerationStateConst state,
			DefDeclGraphEntityStatement ddges)
	{
		GraphEntity graphEntity = ddges.getTarget();
		if(graphEntity.getIdent().toString() == "this") {
			return; // don't emit a declaration for the fake "this" entity of a method
		}
		sb.appendFront(formatType(graphEntity.getType()) + " " + formatEntity(graphEntity));
		if(graphEntity.initialization != null) {
			sb.append(" = ");
			sb.append("(" + formatType(graphEntity.getType()) + ")(");
			genExpression(sb, graphEntity.initialization, state);
			sb.append(")");
		} else {
			sb.append(" = " + getInitializationValue(graphEntity.getType()));
		}
		sb.append(";\n");
	}

	private void genContainerAccumulationYield(SourceBuilder sb, ModifyGenerationStateConst state,
			ContainerAccumulationYield cay)
	{
		if(cay.getContainer().getType() instanceof ArrayType) {
			Type arrayValueType = ((ArrayType)cay.getContainer().getType()).getValueType();
			String arrayValueTypeStr = formatType(arrayValueType);
			String entryVarTypeStr = formatType(cay.getIterationVar().getType());
			String indexVar = "index_" + tmpVarID++;
			String entryVar = "entry_" + tmpVarID++; // for the container itself
			sb.appendFront("List<" + arrayValueTypeStr + "> " + entryVar + " = "
					+ "(List<" + arrayValueTypeStr + ">) " + formatEntity(cay.getContainer()) + ";\n");
			sb.appendFront("for(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
			sb.appendFront("{\n");
			sb.indent();

			if(cay.getIndexVar() != null) {
				if(!Expression.isGlobalVariable(cay.getIndexVar()) || (cay.getIndexVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront("int" + " " + formatEntity(cay.getIndexVar()) + " = " + indexVar + ";\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIndexVar(), indexVar) + ";\n");
				}
				if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront(entryVarTypeStr + " " + formatEntity(cay.getIterationVar()) + " = ("
							+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
				}
			} else {
				if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront(entryVarTypeStr + " " + formatEntity(cay.getIterationVar()) + " = ("
							+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
				}
			}

			genEvals(sb, state, cay.getAccumulationStatements());

			sb.unindent();
			sb.appendFront("}\n");
		} else if(cay.getContainer().getType() instanceof DequeType) {
			Type dequeValueType = ((DequeType)cay.getContainer().getType()).getValueType();
			String dequeValueTypeStr = formatType(dequeValueType);
			String entryVarTypeStr = formatType(cay.getIterationVar().getType());
			String indexVar = "index_" + tmpVarID++;
			String entryVar = "entry_" + tmpVarID++; // for the container itself
			sb.appendFront("GRGEN_LIBGR.Deque<" + dequeValueTypeStr + "> " + entryVar + " = "
					+ "(GRGEN_LIBGR.Deque<" + dequeValueTypeStr + ">) " + formatEntity(cay.getContainer()) + ";\n");
			sb.appendFront("for(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
			sb.appendFront("{\n");
			sb.indent();

			if(cay.getIndexVar() != null) {
				if(!Expression.isGlobalVariable(cay.getIndexVar()) || (cay.getIndexVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront("int" + " " + formatEntity(cay.getIndexVar()) + " = " + indexVar + ";\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIndexVar(), indexVar) + ";\n");
				}
				if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront(entryVarTypeStr + " " + formatEntity(cay.getIterationVar()) + " = ("
							+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
				}
			} else {
				if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront(entryVarTypeStr + " " + formatEntity(cay.getIterationVar()) + " = ("
							+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
				}
			}

			genEvals(sb, state, cay.getAccumulationStatements());

			sb.unindent();
			sb.appendFront("}\n");
		} else if(cay.getContainer().getType() instanceof SetType) {
			Type setValueType = ((SetType)cay.getContainer().getType()).getValueType();
			String setValueTypeStr = formatType(setValueType);
			String entryVarTypeStr = formatType(cay.getIterationVar().getType());
			String entryVar = "entry_" + tmpVarID++;
			sb.appendFront("foreach(KeyValuePair<" + setValueTypeStr + ", GRGEN_LIBGR.SetValueType> " + entryVar
					+ " in " + formatEntity(cay.getContainer()) + ")\n");
			sb.appendFront("{\n");
			sb.indent();

			if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()
					& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
				sb.appendFront(entryVarTypeStr + " " + formatEntity(cay.getIterationVar()));
				sb.append(" = (" + entryVarTypeStr + ")" + entryVar + ".Key;\n");
			} else {
				sb.appendFront(formatGlobalVariableWrite(cay.getIterationVar(), entryVar + ".Key") + ";\n");
			}

			genEvals(sb, state, cay.getAccumulationStatements());

			sb.unindent();
			sb.appendFront("}\n");
		} else //if(cay.getContainer().getType() instanceof MapType)
		{
			Type mapKeyType = ((MapType)cay.getContainer().getType()).getKeyType();
			String mapKeyTypeStr = formatType(mapKeyType);
			Type mapValueType = ((MapType)cay.getContainer().getType()).getValueType();
			String mapValueTypeStr = formatType(mapValueType);
			String keyVarTypeStr = cay.getIndexVar() != null
					? formatType(cay.getIndexVar().getType())
					: formatType(cay.getIterationVar().getType());
			String valueVarTypeStr = formatType(cay.getIterationVar().getType());
			String entryVar = "entry_" + tmpVarID++;
			sb.appendFront("foreach(KeyValuePair<" + mapKeyTypeStr + ", " + mapValueTypeStr + "> " + entryVar
					+ " in " + formatEntity(cay.getContainer()) + ")\n");
			sb.appendFront("{\n");
			sb.indent();

			if(cay.getIndexVar() != null) {
				if(!Expression.isGlobalVariable(cay.getIndexVar()) || (cay.getIndexVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront(keyVarTypeStr + " " + formatEntity(cay.getIndexVar()) + " = ("
							+ keyVarTypeStr + ")" + entryVar + ".Key;\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIndexVar(), entryVar + ".Key") + ";\n");
				}
				if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront(valueVarTypeStr + " " + formatEntity(cay.getIterationVar()) + " = ("
							+ valueVarTypeStr + ")" + entryVar + ".Value;\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIterationVar(), entryVar + ".Value") + ";\n");
				}
			} else {
				if(!Expression.isGlobalVariable(cay.getIterationVar()) || (cay.getIterationVar().getContext()
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
					sb.appendFront(keyVarTypeStr + " " + formatEntity(cay.getIterationVar()) + " = ("
							+ keyVarTypeStr + ")" + entryVar + ".Key;\n");
				} else {
					sb.appendFront(formatGlobalVariableWrite(cay.getIterationVar(), entryVar + ".Key") + ";\n");
				}
			}

			genEvals(sb, state, cay.getAccumulationStatements());

			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genIntegerRangeIterationYield(SourceBuilder sb, ModifyGenerationStateConst state,
			IntegerRangeIterationYield iriy)
	{
		String ascendingVar = "ascending_" + tmpVarID++;
		String entryVar = "entry_" + tmpVarID++;
		String limitVar = "limit_" + tmpVarID++;
		sb.appendFront("int " + entryVar + " = ");
		genExpression(sb, iriy.getLeftExpr(), state);
		sb.append(";\n");
		sb.appendFront("int " + limitVar + " = ");
		genExpression(sb, iriy.getRightExpr(), state);
		sb.append(";\n");
		sb.appendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");
		sb.appendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
		sb.appendFront("{\n");
		sb.indent();

		if(!Expression.isGlobalVariable(iriy.getIterationVar()) || (iriy.getIterationVar().getContext()
				& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
			sb.appendFront("int " + formatEntity(iriy.getIterationVar()) + " = " + entryVar + ";\n");
		} else {
			sb.appendFront(formatGlobalVariableWrite(iriy.getIterationVar(), entryVar) + ";\n");
		}

		genEvals(sb, state, iriy.getAccumulationStatements());

		sb.appendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genMatchesAccumulationYield(SourceBuilder sb, ModifyGenerationStateConst state,
			MatchesAccumulationYield may)
	{
		Type arrayValueType = may.getIterationVar().getType();
		String arrayValueTypeStr = formatType(arrayValueType);
		String indexVar = "index_" + tmpVarID++;
		String entryVar = "entry_" + tmpVarID++;
		sb.appendFront("List<" + arrayValueTypeStr + "> " + entryVar + " = "
				+ "(List<" + arrayValueTypeStr + ">) this_matches;\n");
		sb.appendFront("for(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
		sb.appendFront("{\n");
		sb.indent();

		if(!Expression.isGlobalVariable(may.getIterationVar()) || (may.getIterationVar().getContext()
				& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
			sb.appendFront(arrayValueTypeStr + " " + formatEntity(may.getIterationVar()) + " = "
					+ entryVar + "[" + indexVar + "];\n");
		} else {
			sb.appendFront(formatGlobalVariableWrite(may.getIterationVar(), entryVar + "[" + indexVar + "]") + ";\n");
		}

		genEvals(sb, state, may.getAccumulationStatements());

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genForFunction(SourceBuilder sb, ModifyGenerationStateConst state, ForFunction ff)
	{
		String id = Integer.toString(tmpVarID++);

		if(ff.getFunction() instanceof AdjacentNodeExpr) {
			AdjacentNodeExpr adjacent = (AdjacentNodeExpr)ff.getFunction();
			if(adjacent.Direction() == Direction.INCIDENT) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, adjacent.getStartNodeExpr(), state);
				sb.append(";\n");
				if(!state.emitProfilingInstrumentation()) {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".GetCompatibleIncident(");
					genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
				} else {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".Incident)\n");
				}
				sb.appendFront("{\n");
				sb.indent();

				if(state.emitProfilingInstrumentation()) {
					if(state.isToBeParallelizedActionExisting())
						sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
					sb.appendFront("if(!edge_" + id + ".InstanceOf(");
					genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
					sb.appendFrontIndented("continue;\n");
				}

				sb.appendFront("if(!edge_" + id + ".Opposite(node_" + id + ").InstanceOf(");
				genExpression(sb, adjacent.getAdjacentNodeTypeExpr(), state);
				sb.append("))\n");
				sb.appendFrontIndented("continue;\n");
				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id
						+ ".Opposite(node_" + id + ");\n");
			} else if(adjacent.Direction() == Direction.INCOMING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, adjacent.getStartNodeExpr(), state);
				sb.append(";\n");
				if(!state.emitProfilingInstrumentation()) {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".GetCompatibleIncoming(");
					genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
				} else {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".Incoming)\n");
				}
				sb.appendFront("{\n");
				sb.indent();

				if(state.emitProfilingInstrumentation()) {
					if(state.isToBeParallelizedActionExisting())
						sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
					sb.appendFront("if(!edge_" + id + ".InstanceOf(");
					genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
					sb.appendFrontIndented("continue;\n");
				}

				sb.appendFront("if(!edge_" + id + ".Source.InstanceOf(");
				genExpression(sb, adjacent.getAdjacentNodeTypeExpr(), state);
				sb.append("))\n");
				sb.appendFrontIndented("continue;\n");
				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id
						+ ".Source;\n");
			} else if(adjacent.Direction() == Direction.OUTGOING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, adjacent.getStartNodeExpr(), state);
				sb.append(";\n");
				if(!state.emitProfilingInstrumentation()) {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".GetCompatibleOutgoing(");
					genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
				} else {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".Outgoing)\n");
				}
				sb.appendFront("{\n");
				sb.indent();

				if(state.emitProfilingInstrumentation()) {
					if(state.isToBeParallelizedActionExisting())
						sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
					sb.appendFront("if(!edge_" + id + ".InstanceOf(");
					genExpression(sb, adjacent.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
					sb.appendFrontIndented("continue;\n");
				}

				sb.appendFront("if(!edge_" + id + ".Target.InstanceOf(");
				genExpression(sb, adjacent.getAdjacentNodeTypeExpr(), state);
				sb.append("))\n");
				sb.appendFrontIndented("continue;\n");
				sb.append(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id
						+ ".Target;\n");
			}
		} else if(ff.getFunction() instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr incident = (IncidentEdgeExpr)ff.getFunction();
			if(incident.Direction() == Direction.INCIDENT) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, incident.getStartNodeExpr(), state);
				sb.append(";\n");
				if(!state.emitProfilingInstrumentation()) {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".GetCompatibleIncident(");
					genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
				} else {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".Incident)\n");
				}
				sb.appendFront("{\n");
				sb.indent();

				if(state.emitProfilingInstrumentation()) {
					if(state.isToBeParallelizedActionExisting())
						sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
					sb.appendFront("if(!edge_" + id + ".InstanceOf(");
					genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
					sb.appendFrontIndented("continue;\n");
				}

				sb.appendFront("if(!edge_" + id + ".Opposite(node_" + id + ").InstanceOf(");
				genExpression(sb, incident.getAdjacentNodeTypeExpr(), state);
				sb.append("))\n");
				sb.appendFrontIndented("continue;\n");
				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			} else if(incident.Direction() == Direction.INCOMING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, incident.getStartNodeExpr(), state);
				sb.append(";\n");
				if(!state.emitProfilingInstrumentation()) {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".GetCompatibleIncoming(");
					genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
				} else {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".Incoming)\n");
				}
				sb.appendFront("{\n");
				sb.indent();

				if(state.emitProfilingInstrumentation()) {
					if(state.isToBeParallelizedActionExisting())
						sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
					sb.appendFront("if(!edge_" + id + ".InstanceOf(");
					genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
					sb.appendFrontIndented("continue;\n");
				}

				sb.appendFront("if(!edge_" + id + ".Source.InstanceOf(");
				genExpression(sb, incident.getAdjacentNodeTypeExpr(), state);
				sb.append("))\n");
				sb.appendFrontIndented("continue;\n");
				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			} else if(incident.Direction() == Direction.OUTGOING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, incident.getStartNodeExpr(), state);
				sb.append(";\n");
				if(!state.emitProfilingInstrumentation()) {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".GetCompatibleOutgoing(");
					genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
				} else {
					sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
							+ ".Outgoing)\n");
				}
				sb.appendFront("{\n");
				sb.indent();

				if(state.emitProfilingInstrumentation()) {
					if(state.isToBeParallelizedActionExisting())
						sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
					sb.appendFront("if(!edge_" + id + ".InstanceOf(");
					genExpression(sb, incident.getIncidentEdgeTypeExpr(), state);
					sb.append("))\n");
					sb.appendFrontIndented("continue;\n");
				}

				sb.appendFront("if(!edge_" + id + ".Target.InstanceOf(");
				genExpression(sb, incident.getAdjacentNodeTypeExpr(), state);
				sb.append("))\n");
				sb.appendFrontIndented("continue;\n");
				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
		} else if(ff.getFunction() instanceof ReachableNodeExpr) {
			ReachableNodeExpr reachable = (ReachableNodeExpr)ff.getFunction();
			if(reachable.Direction() == Direction.INCIDENT) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.INode iter_" + id
						+ " in GRGEN_LIBGR.GraphHelper.Reachable(node_" + id + ",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			} else if(reachable.Direction() == Direction.INCOMING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.INode iter_" + id
						+ " in GRGEN_LIBGR.GraphHelper.ReachableIncoming(node_" + id + ",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			} else if(reachable.Direction() == Direction.OUTGOING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.INode iter_" + id
						+ " in GRGEN_LIBGR.GraphHelper.ReachableOutgoing(node_" + id + ",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			}
		} else if(ff.getFunction() instanceof ReachableEdgeExpr) {
			ReachableEdgeExpr reachable = (ReachableEdgeExpr)ff.getFunction();
			if(reachable.Direction() == Direction.INCIDENT) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
						+ " in GRGEN_LIBGR.GraphHelper.ReachableEdges(node_" + id + ",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			} else if(reachable.Direction() == Direction.INCOMING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
						+ " in GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming(node_" + id + ",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			} else if(reachable.Direction() == Direction.OUTGOING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
						+ " in GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing(node_" + id + ",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
		} else if(ff.getFunction() instanceof BoundedReachableNodeExpr) {
			BoundedReachableNodeExpr reachable = (BoundedReachableNodeExpr)ff.getFunction();
			if(reachable.Direction() == Direction.INCIDENT) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.INode iter_" + id
						+ " in GRGEN_LIBGR.GraphHelper.BoundedReachable(node_" + id + ",");
				genExpression(sb, reachable.getDepthExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			} else if(reachable.Direction() == Direction.INCOMING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.INode iter_" + id
						+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableIncoming(node_" + id + ",");
				genExpression(sb, reachable.getDepthExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			} else if(reachable.Direction() == Direction.OUTGOING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.INode iter_" + id
						+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableOutgoing(node_" + id + ",");
				genExpression(sb, reachable.getDepthExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")iter_" + id + ";\n");
			}
		} else if(ff.getFunction() instanceof BoundedReachableEdgeExpr) {
			BoundedReachableEdgeExpr reachable = (BoundedReachableEdgeExpr)ff.getFunction();
			if(reachable.Direction() == Direction.INCIDENT) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
						+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableEdges(node_" + id + ",");
				genExpression(sb, reachable.getDepthExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			} else if(reachable.Direction() == Direction.INCOMING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
						+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesIncoming(node_" + id + ",");
				genExpression(sb, reachable.getDepthExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			} else if(reachable.Direction() == Direction.OUTGOING) {
				sb.appendFront("GRGEN_LIBGR.INode node_" + id + " = ");
				genExpression(sb, reachable.getStartNodeExpr(), state);
				sb.append(";\n");
				sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
						+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesOutgoing(node_" + id + ",");
				genExpression(sb, reachable.getDepthExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getIncidentEdgeTypeExpr(), state);
				sb.append(",");
				genExpression(sb, reachable.getAdjacentNodeTypeExpr(), state);
				sb.append(",");
				sb.append("graph");
				if(state.emitProfilingInstrumentation())
					sb.append(", actionEnv");
				if(state.isToBeParallelizedActionExisting())
					sb.append(", threadId");
				sb.append("))\n");
				sb.appendFront("{\n");
				sb.indent();

				sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
						+ formatEntity(ff.getIterationVar()));
				sb.append(" = (" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
			}
		} else if(ff.getFunction() instanceof NodesExpr) {
			NodesExpr nodes = (NodesExpr)ff.getFunction();
			sb.appendFront("foreach(GRGEN_LIBGR.INode node_" + id + " in graph.GetCompatibleNodes(");
			genExpression(sb, nodes.getNodeTypeExpr(), state);
			sb.append("))\n");
			sb.appendFront("{\n");
			sb.indent();

			if(state.emitProfilingInstrumentation()) {
				if(state.isToBeParallelizedActionExisting())
					sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
				else
					sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
			}

			sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
					+ formatEntity(ff.getIterationVar()));
			sb.append(" = " + "(" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")node_" + id + ";\n");
		} else if(ff.getFunction() instanceof EdgesExpr) {
			EdgesExpr edges = (EdgesExpr)ff.getFunction();
			sb.appendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in graph.GetCompatibleEdges(");
			genExpression(sb, edges.getEdgeTypeExpr(), state);
			sb.append("))\n");
			sb.appendFront("{\n");
			sb.indent();

			if(state.emitProfilingInstrumentation()) {
				if(state.isToBeParallelizedActionExisting())
					sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
				else
					sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
			}

			sb.appendFront(formatElementInterfaceRef(ff.getIterationVar().getType()) + " "
					+ formatEntity(ff.getIterationVar()));
			sb.append(" = " + "(" + formatElementInterfaceRef(ff.getIterationVar().getType()) + ")edge_" + id + ";\n");
		}

		genEvals(sb, state, ff.getLoopedStatements());

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genForIndexAccessEquality(SourceBuilder sb, ModifyGenerationStateConst state,
			ForIndexAccessEquality fiae)
	{
		IndexAccessEquality iae = fiae.getIndexAcccessEquality();

		sb.appendFront("foreach( " + formatElementInterfaceRef(fiae.getIterationVar().getType()) +
				" " + formatEntity(fiae.getIterationVar()) + " in ((" +
				"GRGEN_MODEL." + model.getIdent() + "IndexSet" + ")graph.Indices)." + iae.index.getIdent() +
				".Lookup(");
		genExpression(sb, iae.expr, state);
		sb.append(") )");
		sb.appendFront("{\n");
		sb.indent();

		if(state.emitProfilingInstrumentation()) {
			if(state.isToBeParallelizedActionExisting())
				sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
			else
				sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
		}

		genEvals(sb, state, fiae.getLoopedStatements());

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genForIndexAccessOrdering(SourceBuilder sb, ModifyGenerationStateConst state,
			ForIndexAccessOrdering fiao)
	{
		IndexAccessOrdering iao = fiao.getIndexAccessOrdering();

		sb.appendFront("foreach( " + formatElementInterfaceRef(fiao.getIterationVar().getType()) +
				" " + formatEntity(fiao.getIterationVar()) + " in ((" +
				"GRGEN_MODEL." + model.getIdent() + "IndexSet" + ")graph.Indices)." + iao.index.getIdent() +
				".Lookup");
		if(iao.ascending)
			sb.append("Ascending");
		else
			sb.append("Descending");
		if(iao.from() != null && iao.to() != null) {
			sb.append("From");
			if(iao.includingFrom())
				sb.append("Inclusive");
			else
				sb.append("Exclusive");
			sb.append("To");
			if(iao.includingTo())
				sb.append("Inclusive");
			else
				sb.append("Exclusive");
			sb.append("(");
			genExpression(sb, iao.from(), state);
			sb.append(", ");
			genExpression(sb, iao.to(), state);
		} else if(iao.from() != null) {
			sb.append("From");
			if(iao.includingFrom())
				sb.append("Inclusive");
			else
				sb.append("Exclusive");
			sb.append("(");
			genExpression(sb, iao.from(), state);
		} else if(iao.to() != null) {
			sb.append("To");
			if(iao.includingTo())
				sb.append("Inclusive");
			else
				sb.append("Exclusive");
			sb.append("(");
			genExpression(sb, iao.to(), state);
		} else {
			sb.append("(");
		}
		sb.append(") )\n");
		sb.appendFront("{\n");
		sb.indent();

		if(state.emitProfilingInstrumentation()) {
			if(state.isToBeParallelizedActionExisting())
				sb.appendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
			else
				sb.appendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
		}

		genEvals(sb, state, fiao.getLoopedStatements());

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genBreakStatement(SourceBuilder sb, ModifyGenerationStateConst state, BreakStatement bs)
	{
		sb.appendFront("break;\n");
	}

	private void genContinueStatement(SourceBuilder sb, ModifyGenerationStateConst state, ContinueStatement cs)
	{
		sb.appendFront("continue;\n");
	}

	private void genReturnAssignment(SourceBuilder sb, ModifyGenerationStateConst state, ReturnAssignment ra)
	{
		// declare temporary out variables
		ProcedureInvocationBase procedure = ra.getProcedureInvocation();
		Collection<AssignmentBase> targets = ra.getTargets();
		Vector<String> outParams = new Vector<String>();
		for(int i = 0; i < procedure.getNumReturnTypes(); ++i) {
			String outParam = "outvar_" + tmpVarID;
			outParams.add(outParam);
			++tmpVarID;
			sb.appendFront(formatType(procedure.getReturnType(i)) + " " + outParam + ";\n");
		}
		int i = 0;
		for(AssignmentBase assignment : targets) {
			ProjectionExpr proj;
			if(assignment.getExpression() instanceof ProjectionExpr) {
				proj = (ProjectionExpr)assignment.getExpression();
			} else {
				Cast cast = (Cast)assignment.getExpression();
				proj = (ProjectionExpr)cast.getExpression();
			}
			proj.setProjectedValueVarName(outParams.get(i));
			++i;
		}

		// do the call, with out variables, depending on the type of procedure
		if(ra.getProcedureInvocation() instanceof ProcedureInvocation
				|| ra.getProcedureInvocation() instanceof ExternalProcedureInvocation) {
			genReturnAssignmentProcedureOrExternalProcedureInvocation(sb, state,
					ra.getProcedureInvocation(), outParams);
		} else if(ra.getProcedureInvocation() instanceof ProcedureMethodInvocation
				|| ra.getProcedureInvocation() instanceof ExternalProcedureMethodInvocation) {
			genReturnAssignmentProcedureMethodOrExternalProcedureMethodInvocation(sb, state,
					ra.getProcedureInvocation(), outParams);
		} else {
			genReturnAssignmentBuiltinProcedureOrMethodInvocation(sb, state, ra.getProcedureInvocation(), outParams);
		}

		// assign out variables to the real targets
		for(AssignmentBase assignment : ra.getTargets()) {
			genEvalStmt(sb, state, assignment);
		}
	}

	private void genReturnAssignmentProcedureOrExternalProcedureInvocation(SourceBuilder sb,
			ModifyGenerationStateConst state, ProcedureInvocationBase procedure, Vector<String> outParams)
	{
		// call the procedure with out variables  
		if(procedure instanceof ProcedureInvocation) {
			ProcedureInvocation call = (ProcedureInvocation)procedure;
			sb.appendFront("GRGEN_ACTIONS." + getPackagePrefixDot(call.getProcedure()) + "Procedures."
					+ call.getProcedure().getIdent().toString() + "(actionEnv, graph");
		} else {
			ExternalProcedureInvocation call = (ExternalProcedureInvocation)procedure;
			sb.appendFront("GRGEN_EXPR.ExternalProcedures." + call.getExternalProc().getIdent().toString()
					+ "(actionEnv, graph");
		}
		for(int i = 0; i < procedure.arity(); ++i) {
			sb.append(", ");
			Expression argument = procedure.getArgument(i);
			if(argument.getType() instanceof InheritanceType) {
				sb.append("(" + formatElementInterfaceRef(argument.getType()) + ")");
			}
			genExpression(sb, argument, state);
		}
		for(int i = 0; i < procedure.returnArity(); ++i) {
			sb.append(", out " + outParams.get(i));
		}
		sb.append(");\n");
	}

	private void genReturnAssignmentProcedureMethodOrExternalProcedureMethodInvocation(SourceBuilder sb,
			ModifyGenerationStateConst state, ProcedureInvocationBase procedure, Vector<String> outParams)
	{
		// call the procedure method with out variables  
		if(procedure instanceof ProcedureMethodInvocation) {
			ProcedureMethodInvocation call = (ProcedureMethodInvocation)procedure;
			Entity owner = call.getOwner();
			sb.appendFront("((" + formatElementInterfaceRef(owner.getType()) + ") ");
			sb.append(formatEntity(owner) + ").@");
			sb.append(call.getProcedure().getIdent().toString() + "(actionEnv, graph");
		} else {
			ExternalProcedureMethodInvocation call = (ExternalProcedureMethodInvocation)procedure;
			// the graph element is handed in to the external type method if it was called on a graph element attribute, to allow for transaction manager undo item registration
			if(call.getOwnerQual() != null) {
				genQualAccess(sb, call.getOwnerQual(), state);
			} else {
				genVar(sb, call.getOwnerVar(), state);
			}
			sb.append(".@");
			sb.append(call.getExternalProc().getIdent().toString() + "(actionEnv, graph, ");
			if(call.getOwnerQual() != null) {
				sb.append(formatEntity(call.getOwnerQual().getOwner()));
			} else {
				sb.append("null");
			}
		}
		for(int i = 0; i < procedure.arity(); ++i) {
			sb.append(", ");
			Expression argument = procedure.getArgument(i);
			if(argument.getType() instanceof InheritanceType) {
				sb.append("(" + formatElementInterfaceRef(argument.getType()) + ")");
			}
			genExpression(sb, argument, state);
		}
		for(int i = 0; i < procedure.returnArity(); ++i) {
			sb.append(", out " + outParams.get(i));
		}
		sb.append(");\n");
	}

	private void genReturnAssignmentBuiltinProcedureOrMethodInvocation(SourceBuilder sb,
			ModifyGenerationStateConst state, ProcedureInvocationBase procedure, Vector<String> outParams)
	{
		// call the procedure or procedure method, either without return value, or with one return value, more not supported as of now
		if(outParams.size() == 0) {
			genEvalComp(sb, state, procedure);
		} else {
			assert(outParams.size() == 1);
			sb.appendFront(outParams.get(0) + " = ");
			genEvalComp(sb, state, procedure);
			sb.append(";\n");
		}
	}

	///////////////////////////////
	// Procedure call generation //
	///////////////////////////////

	public void genEvalComp(SourceBuilder sb, ModifyGenerationStateConst state, ProcedureInvocationBase evalProc)
	{
		if(evalProc instanceof EmitProc) {
			genEmitProc(sb, state, (EmitProc)evalProc);
		} else if(evalProc instanceof DebugAddProc) {
			genDebugAddProc(sb, state, (DebugAddProc)evalProc);
		} else if(evalProc instanceof DebugRemProc) {
			genDebugRemProc(sb, state, (DebugRemProc)evalProc);
		} else if(evalProc instanceof DebugEmitProc) {
			genDebugEmitProc(sb, state, (DebugEmitProc)evalProc);
		} else if(evalProc instanceof DebugHaltProc) {
			genDebugHaltProc(sb, state, (DebugHaltProc)evalProc);
		} else if(evalProc instanceof DebugHighlightProc) {
			genDebugHighlightProc(sb, state, (DebugHighlightProc)evalProc);
		} else if(evalProc instanceof RecordProc) {
			genRecordProc(sb, state, (RecordProc)evalProc);
		} else if(evalProc instanceof ExportProc) {
			genExportProc(sb, state, (ExportProc)evalProc);
		} else if(evalProc instanceof DeleteFileProc) {
			genDeleteFileProc(sb, state, (DeleteFileProc)evalProc);
		} else if(evalProc instanceof GraphAddNodeProc) {
			genGraphAddNodeProc(sb, state, (GraphAddNodeProc)evalProc);
		} else if(evalProc instanceof GraphAddEdgeProc) {
			genGraphAddEdgeProc(sb, state, (GraphAddEdgeProc)evalProc);
		} else if(evalProc instanceof GraphRetypeNodeProc) {
			genGraphRetypeNodeProc(sb, state, (GraphRetypeNodeProc)evalProc);
		} else if(evalProc instanceof GraphRetypeEdgeProc) {
			genGraphRetypeEdgeProc(sb, state, (GraphRetypeEdgeProc)evalProc);
		} else if(evalProc instanceof GraphClearProc) {
			genGraphClearProc(sb, state, (GraphClearProc)evalProc);
		} else if(evalProc instanceof GraphRemoveProc) {
			genGraphRemoveProc(sb, state, (GraphRemoveProc)evalProc);
		} else if(evalProc instanceof GraphAddCopyNodeProc) {
			genGraphAddCopyNodeProc(sb, state, (GraphAddCopyNodeProc)evalProc);
		} else if(evalProc instanceof GraphAddCopyEdgeProc) {
			genGraphAddCopyEdgeProc(sb, state, (GraphAddCopyEdgeProc)evalProc);
		} else if(evalProc instanceof GraphMergeProc) {
			genGraphMergeProc(sb, state, (GraphMergeProc)evalProc);
		} else if(evalProc instanceof GraphRedirectSourceProc) {
			genGraphRedirectSourceProc(sb, state, (GraphRedirectSourceProc)evalProc);
		} else if(evalProc instanceof GraphRedirectTargetProc) {
			genGraphRedirectTargetProc(sb, state, (GraphRedirectTargetProc)evalProc);
		} else if(evalProc instanceof GraphRedirectSourceAndTargetProc) {
			genGraphRedirectSourceAndTargetProc(sb, state, (GraphRedirectSourceAndTargetProc)evalProc);
		} else if(evalProc instanceof InsertProc) {
			genInsertProc(sb, state, (InsertProc)evalProc);
		} else if(evalProc instanceof InsertCopyProc) {
			genInsertCopyProc(sb, state, (InsertCopyProc)evalProc);
		} else if(evalProc instanceof InsertInducedSubgraphProc) {
			genInsertInducedSubgraphProc(sb, state, (InsertInducedSubgraphProc)evalProc);
		} else if(evalProc instanceof InsertDefinedSubgraphProc) {
			genInsertDefinedSubgraphProc(sb, state, (InsertDefinedSubgraphProc)evalProc);
		} else if(evalProc instanceof VAllocProc) {
			genVAllocProc(sb, state, (VAllocProc)evalProc);
		} else if(evalProc instanceof VFreeProc) {
			genVFreeProc(sb, state, (VFreeProc)evalProc);
		} else if(evalProc instanceof VFreeNonResetProc) {
			genVFreeNonResetProc(sb, state, (VFreeNonResetProc)evalProc);
		} else if(evalProc instanceof VResetProc) {
			genVResetProc(sb, state, (VResetProc)evalProc);
		} else if(evalProc instanceof StartTransactionProc) {
			genStartTransactionProc(sb, state, (StartTransactionProc)evalProc);
		} else if(evalProc instanceof PauseTransactionProc) {
			genPauseTransactionProc(sb, state, (PauseTransactionProc)evalProc);
		} else if(evalProc instanceof ResumeTransactionProc) {
			genResumeTransactionProc(sb, state, (ResumeTransactionProc)evalProc);
		} else if(evalProc instanceof CommitTransactionProc) {
			genCommitTransactionProc(sb, state, (CommitTransactionProc)evalProc);
		} else if(evalProc instanceof RollbackTransactionProc) {
			genRollbackTransactionProc(sb, state, (RollbackTransactionProc)evalProc);
		} else if(evalProc instanceof MapRemoveItem) {
			genMapRemoveItem(sb, state, (MapRemoveItem)evalProc);
		} else if(evalProc instanceof MapClear) {
			genMapClear(sb, state, (MapClear)evalProc);
		} else if(evalProc instanceof MapAddItem) {
			genMapAddItem(sb, state, (MapAddItem)evalProc);
		} else if(evalProc instanceof SetRemoveItem) {
			genSetRemoveItem(sb, state, (SetRemoveItem)evalProc);
		} else if(evalProc instanceof SetClear) {
			genSetClear(sb, state, (SetClear)evalProc);
		} else if(evalProc instanceof SetAddItem) {
			genSetAddItem(sb, state, (SetAddItem)evalProc);
		} else if(evalProc instanceof ArrayRemoveItem) {
			genArrayRemoveItem(sb, state, (ArrayRemoveItem)evalProc);
		} else if(evalProc instanceof ArrayClear) {
			genArrayClear(sb, state, (ArrayClear)evalProc);
		} else if(evalProc instanceof ArrayAddItem) {
			genArrayAddItem(sb, state, (ArrayAddItem)evalProc);
		} else if(evalProc instanceof DequeRemoveItem) {
			genDequeRemoveItem(sb, state, (DequeRemoveItem)evalProc);
		} else if(evalProc instanceof DequeClear) {
			genDequeClear(sb, state, (DequeClear)evalProc);
		} else if(evalProc instanceof DequeAddItem) {
			genDequeAddItem(sb, state, (DequeAddItem)evalProc);
		} else if(evalProc instanceof MapVarRemoveItem) {
			genMapVarRemoveItem(sb, state, (MapVarRemoveItem)evalProc);
		} else if(evalProc instanceof MapVarClear) {
			genMapVarClear(sb, state, (MapVarClear)evalProc);
		} else if(evalProc instanceof MapVarAddItem) {
			genMapVarAddItem(sb, state, (MapVarAddItem)evalProc);
		} else if(evalProc instanceof SetVarRemoveItem) {
			genSetVarRemoveItem(sb, state, (SetVarRemoveItem)evalProc);
		} else if(evalProc instanceof SetVarClear) {
			genSetVarClear(sb, state, (SetVarClear)evalProc);
		} else if(evalProc instanceof SetVarAddItem) {
			genSetVarAddItem(sb, state, (SetVarAddItem)evalProc);
		} else if(evalProc instanceof ArrayVarRemoveItem) {
			genArrayVarRemoveItem(sb, state, (ArrayVarRemoveItem)evalProc);
		} else if(evalProc instanceof ArrayVarClear) {
			genArrayVarClear(sb, state, (ArrayVarClear)evalProc);
		} else if(evalProc instanceof ArrayVarAddItem) {
			genArrayVarAddItem(sb, state, (ArrayVarAddItem)evalProc);
		} else if(evalProc instanceof DequeVarRemoveItem) {
			genDequeVarRemoveItem(sb, state, (DequeVarRemoveItem)evalProc);
		} else if(evalProc instanceof DequeVarClear) {
			genDequeVarClear(sb, state, (DequeVarClear)evalProc);
		} else if(evalProc instanceof DequeVarAddItem) {
			genDequeVarAddItem(sb, state, (DequeVarAddItem)evalProc);
		} else {
			throw new UnsupportedOperationException("Unexpected eval procedure \"" + evalProc + "\"");
		}
	}

	private void genEmitProc(SourceBuilder sb, ModifyGenerationStateConst state, EmitProc ep)
	{
		String emitVar = "emit_value_" + tmpVarID++;
		String emitWriter = ep.isDebug() ? "EmitWriterDebug" : "EmitWriter";
		sb.appendFront("object " + emitVar + ";\n");
		for(Expression expr : ep.getExpressions()) {
			sb.appendFront(emitVar + " = ");
			genExpression(sb, expr, state);
			sb.append(";\n");
			sb.appendFront("if(" + emitVar + " != null)\n");
			sb.appendFrontIndented("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv)." + emitWriter + ".Write("
					+ "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + emitVar + ", graph));\n");
		}
	}

	private void genDebugAddProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugAddProc dap)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering((string)");
		genExpression(sb, dap.getFirstExpression(), state);
		boolean first = true;
		for(Expression expr : dap.getExpressions()) {
			if(!first) {
				sb.append(",");
				genExpression(sb, expr, state);
			}
			first = false;
		}
		sb.append(");\n");
	}

	private void genDebugRemProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugRemProc drp)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting((string)");
		genExpression(sb, drp.getFirstExpression(), state);
		boolean first = true;
		for(Expression expr : drp.getExpressions()) {
			if(!first) {
				sb.append(",");
				genExpression(sb, expr, state);
			}
			first = false;
		}
		sb.append(");\n");
	}

	private void genDebugEmitProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugEmitProc dep)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEmitting((string)");
		genExpression(sb, dep.getFirstExpression(), state);
		boolean first = true;
		for(Expression expr : dep.getExpressions()) {
			if(!first) {
				sb.append(",");
				genExpression(sb, expr, state);
			}
			first = false;
		}
		sb.append(");\n");
	}

	private void genDebugHaltProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugHaltProc dhp)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugHalting((string)");
		genExpression(sb, dhp.getFirstExpression(), state);
		boolean first = true;
		for(Expression expr : dhp.getExpressions()) {
			if(!first) {
				sb.append(",");
				genExpression(sb, expr, state);
			}
			first = false;
		}
		sb.append(");\n");
	}

	private void genDebugHighlightProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugHighlightProc dhp)
	{
		String highlightValuesArray = "highlight_values_" + tmpVarID++;
		sb.appendFront("List<object> " + highlightValuesArray + " = new List<object>();\n");
		String highlightSourceNamesArray = "highlight_source_names_" + tmpVarID++;
		sb.appendFront("List<string> " + highlightSourceNamesArray + " = new List<string>();\n");
		int parameterNum = 0;
		for(Expression expr : dhp.getExpressions()) {
			if(parameterNum == 0) {
				++parameterNum;
				continue;
			}
			if(parameterNum % 2 == 1) {
				sb.appendFront(highlightValuesArray + ".Add(");
				genExpression(sb, expr, state);
				sb.append(");\n");
			} else {
				sb.appendFront(highlightSourceNamesArray + ".Add((string)");
				genExpression(sb, expr, state);
				sb.append(");\n");
			}
			++parameterNum;
		}
		sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugHighlighting((string)");
		genExpression(sb, dhp.getFirstExpression(), state);
		sb.append("," + highlightValuesArray + ", " + highlightSourceNamesArray + ");\n");
	}

	private void genRecordProc(SourceBuilder sb, ModifyGenerationStateConst state, RecordProc rp)
	{
		String recordVar = "record_value_" + tmpVarID++;
		sb.appendFront("object " + recordVar + " = ");
		genExpression(sb, rp.getToRecordExpr(), state);
		sb.append(";\n");
		sb.appendFront("if(" + recordVar + " != null)\n");
		sb.appendFrontIndented("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).Recorder.Write("
				+ "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + recordVar + ", graph));\n");
	}

	private void genExportProc(SourceBuilder sb, ModifyGenerationStateConst state, ExportProc ep)
	{
		if(ep.getGraphExpr() != null) {
			sb.appendFront("GRGEN_LIBGR.GraphHelper.Export(");
			genExpression(sb, ep.getPathExpr(), state);
			sb.append(", ");
			genExpression(sb, ep.getGraphExpr(), state);
			sb.append(");\n");
		} else {
			sb.appendFront("GRGEN_LIBGR.GraphHelper.Export(");
			genExpression(sb, ep.getPathExpr(), state);
			sb.append(", graph);\n");
		}
	}

	private void genDeleteFileProc(SourceBuilder sb, ModifyGenerationStateConst state, DeleteFileProc dfp)
	{
		sb.appendFront("System.IO.File.Delete(");
		genExpression(sb, dfp.getPathExpr(), state);
		sb.append(");\n");
	}

	private void genGraphAddNodeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddNodeProc ganp)
	{
		Constant constant = (Constant)ganp.getNodeTypeExpr();
		sb.append("(" + formatType((Type)constant.getValue()) + ")"
				+ "GRGEN_LIBGR.GraphHelper.AddNodeOfType(");
		genExpression(sb, ganp.getNodeTypeExpr(), state);
		sb.append(", graph)");
	}

	private void genGraphAddEdgeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddEdgeProc gaep)
	{
		Constant constant = (Constant)gaep.getEdgeTypeExpr();
		sb.append("(" + formatType((Type)constant.getValue()) + ")"
				+ "GRGEN_LIBGR.GraphHelper.AddEdgeOfType(");
		genExpression(sb, gaep.getEdgeTypeExpr(), state);
		sb.append(", ");
		genExpression(sb, gaep.getSourceNodeExpr(), state);
		sb.append(", ");
		genExpression(sb, gaep.getTargetNodeExpr(), state);
		sb.append(", graph)");
	}

	private void genGraphRetypeNodeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphRetypeNodeProc grnp)
	{
		Constant constant = (Constant)grnp.getNewNodeTypeExpr();
		sb.append("(" + formatType((Type)constant.getValue()) + ")"
				+ "graph.Retype(");
		genExpression(sb, grnp.getNodeExpr(), state);
		sb.append(", ");
		genExpression(sb, grnp.getNewNodeTypeExpr(), state);
		sb.append(")");
	}

	private void genGraphRetypeEdgeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphRetypeEdgeProc grep)
	{
		Constant constant = (Constant)grep.getNewEdgeTypeExpr();
		sb.append("(" + formatType((Type)constant.getValue()) + ")"
				+ "graph.Retype(");
		genExpression(sb, grep.getEdgeExpr(), state);
		sb.append(", ");
		genExpression(sb, grep.getNewEdgeTypeExpr(), state);
		sb.append(")");
	}

	private void genGraphClearProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphClearProc gcp)
	{
		sb.appendFront("graph.Clear();\n");
	}

	private void genGraphRemoveProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphRemoveProc grp)
	{
		if(grp.getEntity().getType() instanceof NodeType) {
			sb.appendFront("graph.RemoveEdges((GRGEN_LIBGR.INode)");
			genExpression(sb, grp.getEntity(), state);
			sb.append(");\n");

			sb.appendFront("graph.Remove((GRGEN_LIBGR.INode)");
			genExpression(sb, grp.getEntity(), state);
			sb.append(");\n");
		} else {
			sb.appendFront("graph.Remove((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grp.getEntity(), state);
			sb.append(");\n");
		}
	}

	private void genGraphAddCopyNodeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddCopyNodeProc gacnp)
	{
		sb.append("(" + formatType(gacnp.getOldNodeExpr().getType()) + ")");
		sb.append("GRGEN_LIBGR.GraphHelper.AddCopyOfNode(");
		genExpression(sb, gacnp.getOldNodeExpr(), state);
		sb.append(", graph)");
	}

	private void genGraphAddCopyEdgeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddCopyEdgeProc gacep)
	{
		sb.append("(" + formatType(gacep.getOldEdgeExpr().getType()) + ")");
		sb.append("GRGEN_LIBGR.GraphHelper.AddCopyOfEdge(");
		genExpression(sb, gacep.getOldEdgeExpr(), state);
		sb.append(", (GRGEN_LIBGR.INode)");
		genExpression(sb, gacep.getSourceNodeExpr(), state);
		sb.append(", (GRGEN_LIBGR.INode)");
		genExpression(sb, gacep.getTargetNodeExpr(), state);
		sb.append(", graph)");
	}

	private void genGraphMergeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphMergeProc gmp)
	{
		if(gmp.getSourceName() != null) {
			sb.appendFront("graph.Merge((GRGEN_LIBGR.INode)");
			genExpression(sb, gmp.getTarget(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, gmp.getSource(), state);
			sb.append(", (String)");
			genExpression(sb, gmp.getSourceName(), state);
			sb.append(");\n");
		} else {
			sb.appendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).Merge((GRGEN_LIBGR.INode)");
			genExpression(sb, gmp.getTarget(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, gmp.getSource(), state);
			sb.append(");\n");
		}
	}

	private void genGraphRedirectSourceProc(SourceBuilder sb, ModifyGenerationStateConst state,
			GraphRedirectSourceProc grsp)
	{
		if(grsp.getOldSourceName() != null) {
			sb.appendFront("graph.RedirectSource((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grsp.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsp.getNewSource(), state);
			sb.append(", (String)");
			genExpression(sb, grsp.getOldSourceName(), state);
			sb.append(");\n");
		} else {
			sb.appendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectSource((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grsp.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsp.getNewSource(), state);
			sb.append(");\n");
		}
	}

	private void genGraphRedirectTargetProc(SourceBuilder sb, ModifyGenerationStateConst state,
			GraphRedirectTargetProc grtp)
	{
		if(grtp.getOldTargetName() != null) {
			sb.appendFront("graph.RedirectTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grtp.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grtp.getNewTarget(), state);
			sb.append(", (String)");
			genExpression(sb, grtp.getOldTargetName(), state);
			sb.append(");\n");
		} else {
			sb.appendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grtp.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grtp.getNewTarget(), state);
			sb.append(");\n");
		}
	}

	private void genGraphRedirectSourceAndTargetProc(SourceBuilder sb, ModifyGenerationStateConst state,
			GraphRedirectSourceAndTargetProc grsatp)
	{
		if(grsatp.getOldSourceName() != null) {
			sb.appendFront("graph.RedirectSourceAndTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grsatp.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsatp.getNewSource(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsatp.getNewTarget(), state);
			sb.append(", (String)");
			genExpression(sb, grsatp.getOldSourceName(), state);
			sb.append(", (String)");
			genExpression(sb, grsatp.getOldTargetName(), state);
			sb.append(");\n");
		} else {
			sb.appendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectSourceAndTarget((GRGEN_LIBGR.IEdge)");
			genExpression(sb, grsatp.getEdge(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsatp.getNewSource(), state);
			sb.append(", (GRGEN_LIBGR.INode)");
			genExpression(sb, grsatp.getNewTarget(), state);
			sb.append(");\n");
		}
	}

	private void genInsertProc(SourceBuilder sb, ModifyGenerationStateConst state, InsertProc ip)
	{
		sb.appendFront("GRGEN_LIBGR.GraphHelper.Insert((GRGEN_LIBGR.IGraph)");
		genExpression(sb, ip.getGraphExpr(), state);
		sb.append(", graph);\n");
	}

	private void genInsertCopyProc(SourceBuilder sb, ModifyGenerationStateConst state, InsertCopyProc icp)
	{
		sb.append("GRGEN_LIBGR.GraphHelper.InsertCopy((GRGEN_LIBGR.IGraph)");
		genExpression(sb, icp.getGraphExpr(), state);
		sb.append(", (GRGEN_LIBGR.INode)");
		genExpression(sb, icp.getNodeExpr(), state);
		sb.append(", graph)");
	}

	private void genInsertInducedSubgraphProc(SourceBuilder sb, ModifyGenerationStateConst state,
			InsertInducedSubgraphProc iisp)
	{
		sb.append("((");
		sb.append(formatType(iisp.getNodeExpr().getType()));
		sb.append(")GRGEN_LIBGR.GraphHelper.InsertInduced((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)");
		genExpression(sb, iisp.getSetExpr(), state);
		sb.append(", ");
		genExpression(sb, iisp.getNodeExpr(), state);
		sb.append(", graph))");
	}

	private void genInsertDefinedSubgraphProc(SourceBuilder sb, ModifyGenerationStateConst state,
			InsertDefinedSubgraphProc idsp)
	{
		sb.append("((");
		sb.append(formatType(idsp.getEdgeExpr().getType()));
		sb.append(")GRGEN_LIBGR.GraphHelper.InsertDefined");
		switch(getDirectednessSuffix(idsp.getSetExpr().getType())) {
		case "Directed":
			sb.append("Directed(");
			sb.append("(IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)");
			genExpression(sb, idsp.getSetExpr(), state);
			sb.append(",(GRGEN_LIBGR.IDEdge)");
			break;
		case "Undirected":
			sb.append("Undirected(");
			sb.append("(IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)");
			genExpression(sb, idsp.getSetExpr(), state);
			sb.append(",(GRGEN_LIBGR.IUEdge)");
			break;
		default:
			sb.append("(");
			sb.append("(IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)");
			genExpression(sb, idsp.getSetExpr(), state);
			sb.append(",(GRGEN_LIBGR.IEdge)");
			break;
		}
		genExpression(sb, idsp.getEdgeExpr(), state);
		sb.append(", graph))");
	}

	private void genVAllocProc(SourceBuilder sb, ModifyGenerationStateConst state, VAllocProc vap)
	{
		sb.append("graph.AllocateVisitedFlag()");
	}

	private void genVFreeProc(SourceBuilder sb, ModifyGenerationStateConst state, VFreeProc vfp)
	{
		sb.appendFront("graph.FreeVisitedFlag((int)");
		genExpression(sb, vfp.getVisitedFlagExpr(), state);
		sb.append(");\n");
	}

	private void genVFreeNonResetProc(SourceBuilder sb, ModifyGenerationStateConst state, VFreeNonResetProc vfnrp)
	{
		sb.appendFront("graph.FreeVisitedFlagNonReset((int)");
		genExpression(sb, vfnrp.getVisitedFlagExpr(), state);
		sb.append(");\n");
	}

	private void genVResetProc(SourceBuilder sb, ModifyGenerationStateConst state, VResetProc vrp)
	{
		sb.appendFront("graph.ResetVisitedFlag((int)");
		genExpression(sb, vrp.getVisitedFlagExpr(), state);
		sb.append(");\n");
	}

	private void genStartTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, StartTransactionProc stp)
	{
		sb.append("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Start()");
	}

	private void genPauseTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, PauseTransactionProc ptp)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Pause();\n");
	}

	private void genResumeTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, ResumeTransactionProc rtp)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Resume();\n");
	}

	private void genCommitTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, CommitTransactionProc ctp)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Commit((int)");
		genExpression(sb, ctp.getTransactionId(), state);
		sb.append(");\n");
	}

	private void genRollbackTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state,
			RollbackTransactionProc rtp)
	{
		sb.appendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Rollback((int)");
		genExpression(sb, rtp.getTransactionId(), state);
		sb.append(");\n");
	}

	//////////////////////

	protected void genChangingAttribute(SourceBuilder sb, ModifyGenerationStateConst state,
			Qualification target, String attributeChangeType, String newValue, String keyValue)
	{
		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		String kindStr = null;
		boolean isDeletedElem = false;
		if(element instanceof Node) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		} else if(element instanceof Edge) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		} else if(element instanceof Variable && ((Variable)element).getType() instanceof NodeType) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		} else if(element instanceof Variable && ((Variable)element).getType() instanceof EdgeType) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		} else
			assert false : "Entity is neither a node nor an edge (" + element + ")!";

		if(!isDeletedElem && be.system.mayFireEvents()) {
			if(!Expression.isGlobalVariable(element)) {
				sb.appendFront("graph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType." + attributeChangeType + ", " +
						newValue + ", " + keyValue + ");\n");
			} else {
				sb.appendFront("graph.Changing" + kindStr + "Attribute(" +
						formatGlobalVariableRead(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType." + attributeChangeType + ", " +
						newValue + ", " + keyValue + ");\n");
			}
		}
	}

	protected void genChangedAttribute(SourceBuilder sb, ModifyGenerationStateConst state,
			Qualification target)
	{
		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		String kindStr = null;
		boolean isDeletedElem = false;
		if(element instanceof Node) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		} else if(element instanceof Edge) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		} else if(element instanceof Variable && ((Variable)element).getType() instanceof NodeType) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		} else if(element instanceof Variable && ((Variable)element).getType() instanceof EdgeType) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		} else
			assert false : "Entity is neither a node nor an edge (" + element + ")!";

		if(!isDeletedElem && be.system.mayFireDebugEvents()) {
			if(!Expression.isGlobalVariable(element)) {
				sb.appendFront("graph.Changed" + kindStr + "Attribute(" +
						formatEntity(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ");\n");
			} else {
				sb.appendFront("graph.Changed" + kindStr + "Attribute(" +
						formatGlobalVariableRead(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ");\n");
			}
		}
	}

	protected void genClearAttribute(SourceBuilder sb, ModifyGenerationStateConst state, Qualification target)
	{
		SourceBuilder sbtmp = new SourceBuilder();
		genExpression(sbtmp, target, state);
		String targetStr = sbtmp.toString();

		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		String kindStr = null;
		boolean isDeletedElem = false;
		if(element instanceof Node) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		} else if(element instanceof Edge) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		} else
			assert false : "Entity is neither a node nor an edge (" + element + ")!";

		if(!isDeletedElem && be.system.mayFireEvents()) {
			if(attribute.getType() instanceof MapType) {
				MapType attributeType = (MapType)attribute.getType();
				sb.appendFront("foreach(KeyValuePair<" + formatType(attributeType.getKeyType()) + ","
						+ formatType(attributeType.getValueType()) + "> kvp " +
						"in " + targetStr + ")\n");
				sb.appendFrontIndented("graph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"null, kvp.Key);\n");
			} else if(attribute.getType() instanceof SetType) {
				SetType attributeType = (SetType)attribute.getType();
				sb.appendFront("foreach(KeyValuePair<" + formatType(attributeType.getValueType())
						+ ", GRGEN_LIBGR.SetValueType> kvp " +
						"in " + targetStr + ")\n");
				sb.appendFrontIndented("graph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"kvp.Key, null);\n");
			} else if(attribute.getType() instanceof ArrayType) {
				sb.appendFront("for(int i = " + targetStr + ".Count; i>=0; --i)\n");
				sb.appendFrontIndented("graph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"null, i);\n");
			} else if(attribute.getType() instanceof DequeType) {
				sb.appendFront("for(int i = " + targetStr + ".Count; i>=0; --i)\n");
				sb.appendFrontIndented("graph.Changing" + kindStr + "Attribute(" +
						formatEntity(element) + ", " +
						formatTypeClassRef(elementType) + "." +
						formatAttributeTypeName(attribute) + ", " +
						"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
						"null, i);\n");
			} else {
				assert(false);
			}
		}
	}

	protected void genClearedAttribute(SourceBuilder sb, ModifyGenerationStateConst state, Qualification target)
	{
		Entity element = target.getOwner();
		Entity attribute = target.getMember();
		Type elementType = attribute.getOwner();

		String kindStr = null;
		boolean isDeletedElem = false;
		if(element instanceof Node) {
			kindStr = "Node";
			isDeletedElem = state.delNodes().contains(element);
		} else if(element instanceof Edge) {
			kindStr = "Edge";
			isDeletedElem = state.delEdges().contains(element);
		} else
			assert false : "Entity is neither a node nor an edge (" + element + ")!";

		if(!isDeletedElem && be.system.mayFireDebugEvents()) {
			sb.appendFront("graph.Changed" + kindStr + "Attribute(" +
					formatEntity(element) + ", " +
					formatTypeClassRef(elementType) + "." +
					formatAttributeTypeName(attribute) + ");\n");
		}
	}

	//////////////////////
	// Expression stuff //
	//////////////////////

	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState)
	{
		genQualAccess(sb, qual, (ModifyGenerationStateConst)modifyGenerationState);
	}

	protected void genQualAccess(SourceBuilder sb, Qualification qual, ModifyGenerationStateConst state)
	{
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		if(owner.getType() instanceof MatchType || owner.getType() instanceof DefinedMatchType) {
			sb.append(formatEntity(owner) + "." + formatEntity(member));
		} else {
			genQualAccess(sb, state, owner, member);
		}
	}

	protected void genQualAccess(SourceBuilder sb, ModifyGenerationStateConst state, Entity owner, Entity member)
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

	protected void genMemberAccess(SourceBuilder sb, Entity member)
	{
		// needed in implementing methods
		sb.append("@" + formatIdentifiable(member));
	}

	private boolean accessViaVariable(ModifyGenerationStateConst state, Entity elem, Entity attr)
	{
		HashSet<Entity> forcedAttrs = state.forceAttributeToVar().get(elem);
		return forcedAttrs != null && forcedAttrs.contains(attr);
	}
}
