using System;
using System.Collections.Generic;
using System.Diagnostics;

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Generates the eval statements for the SearchPlanBackend2 backend.
/// @author Edgar Jakumeit, Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ir;
	using Assignment = de.unika.ipd.grgen.ir.stmt.Assignment;
	using AssignmentBase = de.unika.ipd.grgen.ir.stmt.AssignmentBase;
	using AssignmentGraphEntity = de.unika.ipd.grgen.ir.stmt.AssignmentGraphEntity;
	using AssignmentIdentical = de.unika.ipd.grgen.ir.stmt.AssignmentIdentical;
	using AssignmentIndexed = de.unika.ipd.grgen.ir.stmt.AssignmentIndexed;
	using AssignmentMember = de.unika.ipd.grgen.ir.stmt.AssignmentMember;
	using AssignmentVar = de.unika.ipd.grgen.ir.stmt.AssignmentVar;
	using AssignmentVarIndexed = de.unika.ipd.grgen.ir.stmt.AssignmentVarIndexed;
	using BreakStatement = de.unika.ipd.grgen.ir.stmt.BreakStatement;
	using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
	using CaseStatement = de.unika.ipd.grgen.ir.stmt.CaseStatement;
	using CompoundAssignment = de.unika.ipd.grgen.ir.stmt.CompoundAssignment;
	using CompoundAssignmentChanged = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChanged;
	using CompoundAssignmentChangedVar = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVar;
	using CompoundAssignmentChangedVisited = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVisited;
	using CompoundAssignmentVar = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVar;
	using CompoundAssignmentVarChanged = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChanged;
	using CompoundAssignmentVarChangedVar = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVar;
	using CompoundAssignmentVarChangedVisited = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVisited;
	using ConditionStatement = de.unika.ipd.grgen.ir.stmt.ConditionStatement;
	using ContainerAccumulationYield = de.unika.ipd.grgen.ir.stmt.ContainerAccumulationYield;
	using ContinueStatement = de.unika.ipd.grgen.ir.stmt.ContinueStatement;
	using DefDeclGraphEntityStatement = de.unika.ipd.grgen.ir.stmt.DefDeclGraphEntityStatement;
	using DefDeclVarStatement = de.unika.ipd.grgen.ir.stmt.DefDeclVarStatement;
	using DoWhileStatement = de.unika.ipd.grgen.ir.stmt.DoWhileStatement;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using ExecStatement = de.unika.ipd.grgen.ir.stmt.ExecStatement;
	using FunctionAutoKeepOneForEachAccumulateBy = de.unika.ipd.grgen.ir.stmt.FunctionAutoKeepOneForEachAccumulateBy;
	using IntegerRangeIterationYield = de.unika.ipd.grgen.ir.stmt.IntegerRangeIterationYield;
	using LockStatement = de.unika.ipd.grgen.ir.stmt.LockStatement;
	using MatchesAccumulationYield = de.unika.ipd.grgen.ir.stmt.MatchesAccumulationYield;
	using MultiStatement = de.unika.ipd.grgen.ir.stmt.MultiStatement;
	using ReturnAssignment = de.unika.ipd.grgen.ir.stmt.ReturnAssignment;
	using ReturnStatement = de.unika.ipd.grgen.ir.stmt.ReturnStatement;
	using ReturnStatementFilter = de.unika.ipd.grgen.ir.stmt.ReturnStatementFilter;
	using ReturnStatementProcedure = de.unika.ipd.grgen.ir.stmt.ReturnStatementProcedure;
	using SwitchStatement = de.unika.ipd.grgen.ir.stmt.SwitchStatement;
	using WhileStatement = de.unika.ipd.grgen.ir.stmt.WhileStatement;
	using ArrayAddItem = de.unika.ipd.grgen.ir.stmt.array.ArrayAddItem;
	using ArrayClear = de.unika.ipd.grgen.ir.stmt.array.ArrayClear;
	using ArrayRemoveItem = de.unika.ipd.grgen.ir.stmt.array.ArrayRemoveItem;
	using ArrayVarAddAll = de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddAll;
	using ArrayVarAddItem = de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddItem;
	using ArrayVarClear = de.unika.ipd.grgen.ir.stmt.array.ArrayVarClear;
	using ArrayVarRemoveItem = de.unika.ipd.grgen.ir.stmt.array.ArrayVarRemoveItem;
	using DequeAddItem = de.unika.ipd.grgen.ir.stmt.deque.DequeAddItem;
	using DequeClear = de.unika.ipd.grgen.ir.stmt.deque.DequeClear;
	using DequeRemoveItem = de.unika.ipd.grgen.ir.stmt.deque.DequeRemoveItem;
	using DequeVarAddItem = de.unika.ipd.grgen.ir.stmt.deque.DequeVarAddItem;
	using DequeVarClear = de.unika.ipd.grgen.ir.stmt.deque.DequeVarClear;
	using DequeVarRemoveItem = de.unika.ipd.grgen.ir.stmt.deque.DequeVarRemoveItem;
	using AssignmentNameof = de.unika.ipd.grgen.ir.stmt.graph.AssignmentNameof;
	using AssignmentVisited = de.unika.ipd.grgen.ir.stmt.graph.AssignmentVisited;
	using ForFunction = de.unika.ipd.grgen.ir.stmt.graph.ForFunction;
	using ForIndexAccessEquality = de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessEquality;
	using ForIndexAccessOrdering = de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessOrdering;
	using GraphAddCopyEdgeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyEdgeProc;
	using GraphAddCopyNodeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyNodeProc;
	using GraphAddEdgeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphAddEdgeProc;
	using GraphAddNodeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphAddNodeProc;
	using GraphClearProc = de.unika.ipd.grgen.ir.stmt.graph.GraphClearProc;
	using GraphMergeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphMergeProc;
	using GraphRedirectSourceAndTargetProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectSourceAndTargetProc;
	using GraphRedirectSourceProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectSourceProc;
	using GraphRedirectTargetProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectTargetProc;
	using GraphRemoveProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRemoveProc;
	using GraphRetypeEdgeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeEdgeProc;
	using GraphRetypeNodeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeNodeProc;
	using InsertCopyProc = de.unika.ipd.grgen.ir.stmt.graph.InsertCopyProc;
	using InsertDefinedSubgraphProc = de.unika.ipd.grgen.ir.stmt.graph.InsertDefinedSubgraphProc;
	using InsertInducedSubgraphProc = de.unika.ipd.grgen.ir.stmt.graph.InsertInducedSubgraphProc;
	using InsertProc = de.unika.ipd.grgen.ir.stmt.graph.InsertProc;
	using VAllocProc = de.unika.ipd.grgen.ir.stmt.graph.VAllocProc;
	using VFreeNonResetProc = de.unika.ipd.grgen.ir.stmt.graph.VFreeNonResetProc;
	using VFreeProc = de.unika.ipd.grgen.ir.stmt.graph.VFreeProc;
	using VResetProc = de.unika.ipd.grgen.ir.stmt.graph.VResetProc;
	using ExternalProcedureInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureInvocation;
	using ExternalProcedureMethodInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureMethodInvocation;
	using ProcedureInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureInvocation;
	using ProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureInvocationBase;
	using ProcedureOrBuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;
	using ProcedureMethodInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureMethodInvocation;
	using MapAddItem = de.unika.ipd.grgen.ir.stmt.map.MapAddItem;
	using MapClear = de.unika.ipd.grgen.ir.stmt.map.MapClear;
	using MapRemoveItem = de.unika.ipd.grgen.ir.stmt.map.MapRemoveItem;
	using MapVarAddItem = de.unika.ipd.grgen.ir.stmt.map.MapVarAddItem;
	using MapVarClear = de.unika.ipd.grgen.ir.stmt.map.MapVarClear;
	using MapVarRemoveItem = de.unika.ipd.grgen.ir.stmt.map.MapVarRemoveItem;
	using AssertProc = de.unika.ipd.grgen.ir.stmt.procenv.AssertProc;
	using CommitTransactionProc = de.unika.ipd.grgen.ir.stmt.procenv.CommitTransactionProc;
	using DebugAddProc = de.unika.ipd.grgen.ir.stmt.procenv.DebugAddProc;
	using DebugEmitProc = de.unika.ipd.grgen.ir.stmt.procenv.DebugEmitProc;
	using DebugHaltProc = de.unika.ipd.grgen.ir.stmt.procenv.DebugHaltProc;
	using DebugHighlightProc = de.unika.ipd.grgen.ir.stmt.procenv.DebugHighlightProc;
	using DebugRemProc = de.unika.ipd.grgen.ir.stmt.procenv.DebugRemProc;
	using DeleteFileProc = de.unika.ipd.grgen.ir.stmt.procenv.DeleteFileProc;
	using EmitProc = de.unika.ipd.grgen.ir.stmt.procenv.EmitProc;
	using ExportProc = de.unika.ipd.grgen.ir.stmt.procenv.ExportProc;
	using GetEquivalentOrAddProc = de.unika.ipd.grgen.ir.stmt.procenv.GetEquivalentOrAddProc;
	using PauseTransactionProc = de.unika.ipd.grgen.ir.stmt.procenv.PauseTransactionProc;
	using RecordProc = de.unika.ipd.grgen.ir.stmt.procenv.RecordProc;
	using ResumeTransactionProc = de.unika.ipd.grgen.ir.stmt.procenv.ResumeTransactionProc;
	using RollbackTransactionProc = de.unika.ipd.grgen.ir.stmt.procenv.RollbackTransactionProc;
	using StartTransactionProc = de.unika.ipd.grgen.ir.stmt.procenv.StartTransactionProc;
	using SynchronizationEnterProc = de.unika.ipd.grgen.ir.stmt.procenv.SynchronizationEnterProc;
	using SynchronizationExitProc = de.unika.ipd.grgen.ir.stmt.procenv.SynchronizationExitProc;
	using SynchronizationTryEnterProc = de.unika.ipd.grgen.ir.stmt.procenv.SynchronizationTryEnterProc;
	using SetAddItem = de.unika.ipd.grgen.ir.stmt.set.SetAddItem;
	using SetClear = de.unika.ipd.grgen.ir.stmt.set.SetClear;
	using SetRemoveItem = de.unika.ipd.grgen.ir.stmt.set.SetRemoveItem;
	using SetVarAddAll = de.unika.ipd.grgen.ir.stmt.set.SetVarAddAll;
	using SetVarAddItem = de.unika.ipd.grgen.ir.stmt.set.SetVarAddItem;
	using SetVarClear = de.unika.ipd.grgen.ir.stmt.set.SetVarClear;
	using SetVarRemoveItem = de.unika.ipd.grgen.ir.stmt.set.SetVarRemoveItem;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using MatchType = de.unika.ipd.grgen.ir.type.MatchType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using GraphType = de.unika.ipd.grgen.ir.type.basic.GraphType;
	using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;
	using SetType = de.unika.ipd.grgen.ir.type.container.SetType;
	using Direction = de.unika.ipd.grgen.util.Direction;
	using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;
	using Cast = de.unika.ipd.grgen.ir.expr.Cast;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using GraphEntityExpression = de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
	using Operator = de.unika.ipd.grgen.ir.expr.Operator;
	using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
	using ProjectionExpr = de.unika.ipd.grgen.ir.expr.ProjectionExpr;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using AdjacentNodeExpr = de.unika.ipd.grgen.ir.expr.graph.AdjacentNodeExpr;
	using BoundedReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.BoundedReachableEdgeExpr;
	using BoundedReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeExpr;
	using EdgesExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesExpr;
	using EdgesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessFromToExpr;
	using EdgesFromIndexAccessMultipleFromToExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessMultipleFromToExpr;
	using EdgesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessSameExpr;
	using IncidentEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.IncidentEdgeExpr;
	using NodesExpr = de.unika.ipd.grgen.ir.expr.graph.NodesExpr;
	using NodesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessFromToExpr;
	using NodesFromIndexAccessMultipleFromToExpr = de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessMultipleFromToExpr;
	using NodesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessSameExpr;
	using ReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.ReachableEdgeExpr;
	using ReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.ReachableNodeExpr;
	using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using InternalObjectType = de.unika.ipd.grgen.ir.model.type.InternalObjectType;
	using InternalTransientObjectType = de.unika.ipd.grgen.ir.model.type.InternalTransientObjectType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
	using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
	using NameOrAttributeInitialization = de.unika.ipd.grgen.ir.pattern.NameOrAttributeInitialization;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	public class ModifyEvalGen : CSharpBase
	{
		internal Model model;
		internal SearchPlanBackend2 be;

		internal int tmpVarID;

		internal ModifyExecGen execGen;

		public ModifyEvalGen(SearchPlanBackend2 backend, ModifyExecGen execGen,
				string nodeTypePrefix, string edgeTypePrefix, string objectTypePrefix, string transientObjectTypePrefix)
			: base(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix)
		{
			be = backend;
			model = be.unit.ActionsGraphModel;

			tmpVarID = 0;

			this.execGen = execGen;
		}

		//////////////////////////
		// Eval part generation //
		//////////////////////////

		public virtual void GenAllEvals(SourceBuilder sb, ModifyGenerationStateConst state,
				ICollection<EvalStatements> evalStatements)
		{
			foreach(Node node in state.NewNodes)
			{
				if(node.HasAttributeInitialization())
				{
					foreach(NameOrAttributeInitialization nai in node.nameOrAttributeInitialization)
					{
						if(nai.attribute == null) // skip name initialization
							continue;
						GenAssignment(sb, state, new Assignment(new Qualification(nai.owner, nai.attribute), nai.expr));
					}
				}
			}
			foreach(Edge edge in state.NewEdges)
			{
				if(edge.HasAttributeInitialization())
				{
					foreach(NameOrAttributeInitialization nai in edge.nameOrAttributeInitialization)
					{
						if(nai.attribute == null) // skip name initialization
							continue;
						GenAssignment(sb, state, new Assignment(new Qualification(nai.owner, nai.attribute), nai.expr));
					}
				}
			}

			foreach(EvalStatements evalStmts in evalStatements)
			{
				sb.AppendFront("{ // " + evalStmts.Name + "\n");
				sb.Indent();

				//if(be.sys.mayFireDebugEvents()) {
				//	sb.append("\t\t\t((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering(");
				//	sb.append("\"" + state.name() + "." + evalStmts.getName() + "\"");
				//	sb.append(");\n");
				//}

				GenEvals(sb, state, evalStmts.evalStatements);

				//if(be.sys.mayFireDebugEvents()) {
				//	sb.append("\t\t\t((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting(");
				//	sb.append("\"" + state.name() + "." + evalStmts.getName() + "\"");
				//	sb.append(");\n");
				//}

				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenEvals(SourceBuilder sb, ModifyGenerationStateConst state, ICollection<EvalStatement> evalStatements)
		{
			foreach(EvalStatement evalStmt in evalStatements)
				GenEvalStmt(sb, state, evalStmt);
		}

		public virtual void GenEvalStmt(SourceBuilder sb, ModifyGenerationStateConst state, EvalStatement evalStmt)
		{
			if(evalStmt is Assignment) // includes evalStmt instanceof AssignmentIndexed
				GenAssignment(sb, state, (Assignment)evalStmt);
			else if(evalStmt is AssignmentVar) // includes evalStmt instanceof AssignmentVarIndexed
				GenAssignmentVar(sb, state, (AssignmentVar)evalStmt);
			else if(evalStmt is AssignmentGraphEntity)
				GenAssignmentGraphEntity(sb, state, (AssignmentGraphEntity)evalStmt);
			else if(evalStmt is AssignmentMember)
			{
				// currently unused, would be needed for member assignment inside method without "this." prefix
				GenAssignmentMember(sb, state, (AssignmentMember)evalStmt);
			}
			else if(evalStmt is AssignmentVisited)
				GenAssignmentVisited(sb, state, (AssignmentVisited)evalStmt);
			else if(evalStmt is AssignmentNameof)
				GenAssignmentNameof(sb, state, (AssignmentNameof)evalStmt);
			else if(evalStmt is AssignmentIdentical)
			{
				//nothing to generate, was assignment . = . optimized away;
			}
			else if(evalStmt is CompoundAssignmentChanged)
				GenCompoundAssignmentChanged(sb, state, (CompoundAssignmentChanged)evalStmt);
			else if(evalStmt is CompoundAssignmentChangedVar)
				GenCompoundAssignmentChangedVar(sb, state, (CompoundAssignmentChangedVar)evalStmt);
			else if(evalStmt is CompoundAssignmentChangedVisited)
				GenCompoundAssignmentChangedVisited(sb, state, (CompoundAssignmentChangedVisited)evalStmt);
			else if(evalStmt is CompoundAssignment)
				GenCompoundAssignment(sb, state, (CompoundAssignment)evalStmt, "\t\t\t", ";\n");
			else if(evalStmt is CompoundAssignmentVarChanged)
				GenCompoundAssignmentVarChanged(sb, state, (CompoundAssignmentVarChanged)evalStmt);
			else if(evalStmt is CompoundAssignmentVarChangedVar)
				GenCompoundAssignmentVarChangedVar(sb, state, (CompoundAssignmentVarChangedVar)evalStmt);
			else if(evalStmt is CompoundAssignmentVarChangedVisited)
				GenCompoundAssignmentVarChangedVisited(sb, state, (CompoundAssignmentVarChangedVisited)evalStmt);
			else if(evalStmt is CompoundAssignmentVar)
				GenCompoundAssignmentVar(sb, state, (CompoundAssignmentVar)evalStmt, "\t\t\t", ";\n");
			else if(evalStmt is MapRemoveItem)
				GenMapRemoveItem(sb, state, (MapRemoveItem)evalStmt);
			else if(evalStmt is MapClear)
				GenMapClear(sb, state, (MapClear)evalStmt);
			else if(evalStmt is MapAddItem)
				GenMapAddItem(sb, state, (MapAddItem)evalStmt);
			else if(evalStmt is SetRemoveItem)
				GenSetRemoveItem(sb, state, (SetRemoveItem)evalStmt);
			else if(evalStmt is SetClear)
				GenSetClear(sb, state, (SetClear)evalStmt);
			else if(evalStmt is SetAddItem)
				GenSetAddItem(sb, state, (SetAddItem)evalStmt);
			else if(evalStmt is ArrayRemoveItem)
				GenArrayRemoveItem(sb, state, (ArrayRemoveItem)evalStmt);
			else if(evalStmt is ArrayClear)
				GenArrayClear(sb, state, (ArrayClear)evalStmt);
			else if(evalStmt is ArrayAddItem)
				GenArrayAddItem(sb, state, (ArrayAddItem)evalStmt);
			else if(evalStmt is ArrayVarAddAll)
				GenArrayVarAddAll(sb, state, (ArrayVarAddAll)evalStmt);
			else if(evalStmt is DequeRemoveItem)
				GenDequeRemoveItem(sb, state, (DequeRemoveItem)evalStmt);
			else if(evalStmt is DequeClear)
				GenDequeClear(sb, state, (DequeClear)evalStmt);
			else if(evalStmt is DequeAddItem)
				GenDequeAddItem(sb, state, (DequeAddItem)evalStmt);
			else if(evalStmt is MapVarRemoveItem)
				GenMapVarRemoveItem(sb, state, (MapVarRemoveItem)evalStmt);
			else if(evalStmt is MapVarClear)
				GenMapVarClear(sb, state, (MapVarClear)evalStmt);
			else if(evalStmt is MapVarAddItem)
				GenMapVarAddItem(sb, state, (MapVarAddItem)evalStmt);
			else if(evalStmt is SetVarRemoveItem)
				GenSetVarRemoveItem(sb, state, (SetVarRemoveItem)evalStmt);
			else if(evalStmt is SetVarClear)
				GenSetVarClear(sb, state, (SetVarClear)evalStmt);
			else if(evalStmt is SetVarAddItem)
				GenSetVarAddItem(sb, state, (SetVarAddItem)evalStmt);
			else if(evalStmt is SetVarAddAll)
				GenSetVarAddAll(sb, state, (SetVarAddAll)evalStmt);
			else if(evalStmt is ArrayVarRemoveItem)
				GenArrayVarRemoveItem(sb, state, (ArrayVarRemoveItem)evalStmt);
			else if(evalStmt is ArrayVarClear)
				GenArrayVarClear(sb, state, (ArrayVarClear)evalStmt);
			else if(evalStmt is ArrayVarAddItem)
				GenArrayVarAddItem(sb, state, (ArrayVarAddItem)evalStmt);
			else if(evalStmt is DequeVarRemoveItem)
				GenDequeVarRemoveItem(sb, state, (DequeVarRemoveItem)evalStmt);
			else if(evalStmt is DequeVarClear)
				GenDequeVarClear(sb, state, (DequeVarClear)evalStmt);
			else if(evalStmt is DequeVarAddItem)
				GenDequeVarAddItem(sb, state, (DequeVarAddItem)evalStmt);
			else if(evalStmt is ReturnStatementFilter)
				GenReturnStatementFilter(sb, state, (ReturnStatementFilter)evalStmt);
			else if(evalStmt is ReturnStatement)
				GenReturnStatement(sb, state, (ReturnStatement)evalStmt);
			else if(evalStmt is ReturnStatementProcedure)
				GenReturnStatementProcedure(sb, state, (ReturnStatementProcedure)evalStmt);
			else if(evalStmt is ConditionStatement)
				GenConditionStatement(sb, state, (ConditionStatement)evalStmt);
			else if(evalStmt is SwitchStatement)
				GenSwitchStatement(sb, state, (SwitchStatement)evalStmt);
			else if(evalStmt is WhileStatement)
				GenWhileStatement(sb, state, (WhileStatement)evalStmt);
			else if(evalStmt is DoWhileStatement)
				GenDoWhileStatement(sb, state, (DoWhileStatement)evalStmt);
			else if(evalStmt is MultiStatement)
				GenMultiStatement(sb, state, (MultiStatement)evalStmt);
			else if(evalStmt is DefDeclVarStatement)
				GenDefDeclVarStatement(sb, state, (DefDeclVarStatement)evalStmt);
			else if(evalStmt is DefDeclGraphEntityStatement)
				GenDefDeclGraphEntityStatement(sb, state, (DefDeclGraphEntityStatement)evalStmt);
			else if(evalStmt is ContainerAccumulationYield)
				GenContainerAccumulationYield(sb, state, (ContainerAccumulationYield)evalStmt);
			else if(evalStmt is IntegerRangeIterationYield)
				GenIntegerRangeIterationYield(sb, state, (IntegerRangeIterationYield)evalStmt);
			else if(evalStmt is MatchesAccumulationYield)
				GenMatchesAccumulationYield(sb, state, (MatchesAccumulationYield)evalStmt);
			else if(evalStmt is ForFunction)
				GenForFunction(sb, state, (ForFunction)evalStmt);
			else if(evalStmt is ForIndexAccessEquality)
				GenForIndexAccessEquality(sb, state, (ForIndexAccessEquality)evalStmt);
			else if(evalStmt is ForIndexAccessOrdering)
				GenForIndexAccessOrdering(sb, state, (ForIndexAccessOrdering)evalStmt);
			else if(evalStmt is BreakStatement)
				GenBreakStatement(sb, state, (BreakStatement)evalStmt);
			else if(evalStmt is ContinueStatement)
				GenContinueStatement(sb, state, (ContinueStatement)evalStmt);
			else if(evalStmt is ExecStatement)
				execGen.GenExecStatement(sb, state, (ExecStatement)evalStmt);
			else if(evalStmt is ReturnAssignment)
				GenReturnAssignment(sb, state, (ReturnAssignment)evalStmt); // contains the procedure and method invocations
			else if(evalStmt is FunctionAutoKeepOneForEachAccumulateBy)
				GenFunctionAutoKeepOneForEachAccumulateBy(sb, state, (FunctionAutoKeepOneForEachAccumulateBy)evalStmt);
			else if(evalStmt is LockStatement)
				GenLockStatement(sb, state, (LockStatement)evalStmt);
			else
				throw new System.NotSupportedException("Unexpected eval statement \"" + evalStmt + "\"");
		}

		private string GenTempVarFromExpression(SourceBuilder sb, ModifyGenerationStateConst state, /*Type type,*/ Expression expr, string varPrefix)
		{
			Type type = expr.Type;
			string varName = (!string.ReferenceEquals(varPrefix, null) ? varPrefix : "tempvar_") + tmpVarID++;
			string varType = FormatType(type); //getTypeNameForTempVarDecl(type); //TODO: formatType(type)?
			sb.AppendFront(varType + " " + varName + " = ");
			/*if(type instanceof EnumType)
				sb.append("(int) ");
			else
				*/
				sb.Append("(" + varType + ")");
			GenExpression(sb, expr, state);
			sb.Append(";\n");
			return varName;
		}

		private void GenAssignment(SourceBuilder sb, ModifyGenerationStateConst state, Assignment ass)
		{
			Qualification target = ass.Target;
			Expression expr = ass.Expression;
			Type targetType = target.Type;

			if((targetType is MapType
					|| targetType is SetType
					|| targetType is ArrayType
					|| targetType is DequeType)
					&& !(ass is AssignmentIndexed))
			{

				GenAssignmentContainer(sb, state, target, expr, targetType);

				return;
			}

			// indexed assignment to array/deque/map, the target type is the array/deque/map value type
			if(ass is AssignmentIndexed && targetType is ArrayType)
				targetType = ((ArrayType)targetType).ValueType;
			if(ass is AssignmentIndexed && targetType is DequeType)
				targetType = ((DequeType)targetType).ValueType;
			if(ass is AssignmentIndexed && targetType is MapType)
				targetType = ((MapType)targetType).ValueType;

			string varName = "tempvar_" + tmpVarID++;
			string varType = GetTypeNameForTempVarDecl(targetType) + " ";

			sb.AppendFront(varType + varName + " = ");
			if(targetType is EnumType)
				sb.Append("(int) ");
			else
				sb.Append("(" + varType + ")");
			GenExpression(sb, expr, state);
			sb.Append(";\n");

			if(ass is AssignmentIndexed)
			{
				AssignmentIndexed assIdx = (AssignmentIndexed)ass;

				if(target.Type is ArrayType
						|| target.Type is DequeType)
				{
					string indexType = "int ";
					string indexVarName = "tempvar_index" + tmpVarID++;
					sb.AppendFront(indexType + indexVarName + " = (int)");
					GenExpression(sb, assIdx.Index, state);
					sb.Append(";\n");

					sb.AppendFront("if(" + indexVarName + " < ");
					GenExpression(sb, target, state);
					sb.Append(".Count) {\n");
					sb.Indent();

					sb.AppendFront("");
					GenChangingAttribute(sb, state, target, "AssignElement", varName, indexVarName);

					sb.AppendFront("");
					GenExpression(sb, target, state); // global var case handled by genQualAccess
					sb.Append("[");
					sb.Append(indexVarName);
					sb.Append("]");
				}
				else
				{ //if(target.getType() instanceof MapType)
					string indexType = FormatType(((MapType)target.Type).KeyType) + " ";
					string indexVarName = "tempvar_index_" + tmpVarID++;
					sb.AppendFront(indexType + indexVarName + " = ");
					if(targetType is EnumType)
						sb.Append("(int) ");
					else
						sb.Append("(" + indexType + ")");
					GenExpression(sb, assIdx.Index, state);
					sb.Append(";\n");

					sb.AppendFront("if(");
					GenExpression(sb, target, state);
					sb.Append(".ContainsKey(");
					sb.Append(indexVarName);
					sb.Append(")) {\n");
					sb.Indent();

					sb.AppendFront("");
					GenChangingAttribute(sb, state, target, "AssignElement", varName, indexVarName);

					sb.AppendFront("");
					GenExpression(sb, target, state); // global var case handled by genQualAccess
					sb.Append("[");
					sb.Append(indexVarName);
					sb.Append("]");
				}

				sb.Append(" = ");
				if(targetType is EnumType)
					sb.Append("(GRGEN_MODEL." + GetPackagePrefixDot(targetType) + "ENUM_" + FormatIdentifiable(targetType) + ") ");
				sb.Append(varName + ";\n");

				GenChangedAttribute(sb, state, target);

				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				if(!(target.Owner.Type is MatchType
					|| target.Owner.Type is DefinedMatchType
					|| target.Owner.Type is InternalTransientObjectType))
				{
					GenChangingAttribute(sb, state, target, "Assign", varName, "null");
				}

				sb.AppendFront("");
				GenExpression(sb, target, state); // global var case handled by genQualAccess

				sb.Append(" = ");
				if(targetType is EnumType)
					sb.Append("(GRGEN_MODEL." + GetPackagePrefixDot(targetType) + "ENUM_" + FormatIdentifiable(targetType) + ") ");
				sb.Append(varName + ";\n");

				if(!(target.Owner.Type is MatchType
						|| target.Owner.Type is DefinedMatchType
						|| target.Owner.Type is InternalObjectType
						|| target.Owner.Type is InternalTransientObjectType))
				{
					GenChangedAttribute(sb, state, target);
				}
			}
		}

		private void GenAssignmentContainer(SourceBuilder sb, ModifyGenerationStateConst state,
				Qualification target, Expression expr, Type targetType)
		{
			// Check whether we have to make a copy of the right hand side of the assignment
			bool mustCopy = true;
			if(expr is Operator)
			{
				Operator op = (Operator)expr;

				// For unions and intersections new maps/sets are already created,
				// so we don't have to copy them again
				if(op.OpCode == OperatorCode.BIT_OR || op.OpCode == OperatorCode.BIT_AND)
					mustCopy = false;
			}

			string typeName = FormatAttributeType(targetType);
			string varName = "tempvar_" + tmpVarID++;
			sb.AppendFront(typeName + " " + varName + " = ");
			if(mustCopy && !(expr is Constant)) // only null supported as constant
				sb.Append("new " + typeName + "(");
			GenExpression(sb, expr, state);
			if(mustCopy && !(expr is Constant))
				sb.Append(")");
			sb.Append(";\n");

			GenChangingAttribute(sb, state, target, "Assign", varName, "null");

			sb.AppendFront("");
			GenExpression(sb, target, state); // global var case handled by genQualAccess
			sb.Append(" = " + varName + ";\n");

			GenChangedAttribute(sb, state, target);
		}

		private void GenAssignmentVar(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentVar ass)
		{
			Variable target = ass.Target;
			Expression expr = ass.Expression;

			Type targetType = target.Type;
			if(ass is AssignmentVarIndexed)
			{
				if(targetType is ArrayType)
					targetType = ((ArrayType)target.Type).ValueType;
				else if(targetType is DequeType)
					targetType = ((DequeType)target.Type).ValueType;
				else // targetType instanceof MapType
					targetType = ((MapType)target.Type).ValueType;
			}

			Type indexType = IntType.Type;
			if(target.Type is MapType)
				indexType = ((MapType)target.Type).KeyType;

			sb.AppendFront("");
			if(!Expression.IsGlobalVariable(target))
			{
				sb.Append(FormatEntity(target));
				if(ass is AssignmentVarIndexed)
				{
					AssignmentVarIndexed assIdx = (AssignmentVarIndexed)ass;
					Expression index = assIdx.Index;
					sb.Append("[(" + FormatType(indexType) + ") (");
					GenExpression(sb, index, state);
					sb.Append(")]");
				}

				sb.Append(" = ");
				sb.Append("(" + FormatType(targetType) + ") (");
				GenExpression(sb, expr, state);
				sb.Append(");\n");
			}
			else
			{
				if(ass is AssignmentVarIndexed)
				{
					AssignmentVarIndexed assIdx = (AssignmentVarIndexed)ass;
					sb.Append(FormatGlobalVariableRead(target));
					Expression index = assIdx.Index;
					sb.Append("[(" + FormatType(indexType) + ") (");
					GenExpression(sb, index, state);
					sb.Append(")]");

					sb.Append(" = ");
					sb.Append("(" + FormatType(targetType) + ") (");
					GenExpression(sb, expr, state);
					sb.Append(");\n");
				}
				else
				{
					SourceBuilder tmp = new SourceBuilder();
					tmp.Append("(" + FormatType(targetType) + ") (");
					GenExpression(tmp, expr, state);
					tmp.Append(")");
					sb.Append(FormatGlobalVariableWrite(target, tmp.ToString()));
					sb.Append(";\n");
				}
			}
		}

		private void GenAssignmentGraphEntity(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentGraphEntity ass)
		{
			GraphEntity target = ass.Target;
			Expression expr = ass.Expression;

			sb.AppendFront("");
			if(!Expression.IsGlobalVariable(target))
			{
				sb.Append(FormatEntity(target));
				sb.Append(" = ");
				if((target.Context & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION)
				{
					if(target is Node)
						sb.Append("(GRGEN_LGSP.LGSPNode)");
					else
						sb.Append("(GRGEN_LGSP.LGSPEdge)");
				}
				GenExpression(sb, expr, state);
				sb.Append(";\n");
			}
			else
			{
				SourceBuilder tmp = new SourceBuilder();
				GenExpression(tmp, expr, state);
				sb.Append(FormatGlobalVariableWrite(target, tmp.ToString()));
				sb.Append(";\n");
			}
		}

		private void GenAssignmentMember(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentMember ass)
		{
			Entity target = ass.Target;
			Expression expr = ass.Expression;

			sb.AppendFront("");
			if(!Expression.IsGlobalVariable(target))
			{
				GenMemberAccess(sb, target);
				sb.Append(" = ");
				if((target.Context & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION)
					sb.Append("(" + FormatType(target.Type) + ")");
				GenExpression(sb, expr, state);
				sb.Append(";\n");
			}
			else
			{
				SourceBuilder tmp = new SourceBuilder();
				GenExpression(tmp, expr, state);
				sb.Append(FormatGlobalVariableWrite(target, tmp.ToString()));
				sb.Append(";\n");
			}
		}

		private void GenAssignmentVisited(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentVisited ass)
		{
			sb.AppendFront("graph.SetVisited(");
			GenExpression(sb, ass.Target.Entity, state);
			sb.Append(", ");
			GenExpression(sb, ass.Target.VisitorID, state);
			sb.Append(", ");
			GenExpression(sb, ass.Expression, state);
			sb.Append(");\n");
		}

		private void GenAssignmentNameof(SourceBuilder sb, ModifyGenerationStateConst state, AssignmentNameof ass)
		{
			if(ass.Target == null || ass.Target.Type is GraphType)
			{
				if(ass.Target == null)
					sb.AppendFront("graph.Name = ");
				else
				{
					sb.AppendFront("(");
					GenExpression(sb, ass.Target, state);
					sb.Append(").Name = ");
				}
				GenExpression(sb, ass.Expression, state);
				sb.Append(";\n");
			}
			else
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).SetElementName(");
				GenExpression(sb, ass.Target, state);
				sb.Append(", ");
				GenExpression(sb, ass.Expression, state);
				sb.Append(");\n");
			}
		}

		private void GenCompoundAssignmentChanged(SourceBuilder sb, ModifyGenerationStateConst state,
				CompoundAssignmentChanged cass)
		{
			Qualification changedTarget = cass.ChangedTarget;
			string changedOperation;
			if(cass.ChangedOperation == CompoundAssignment.CompoundAssignmentType.UNION)
				changedOperation = " |= ";
			else if(cass.ChangedOperation == CompoundAssignment.CompoundAssignmentType.INTERSECTION)
				changedOperation = " &= ";
			else //if(cass.getChangedOperation()==CompoundAssignment.CompoundAssignmentType.ASSIGN)
				changedOperation = " = ";

			Entity owner = cass.Target.Owner;
			bool isDeletedElem = state.IsDeleted(owner);
			if(!isDeletedElem && be.sys.MayFireEvents())
			{
				owner = changedTarget.Owner;
				isDeletedElem = state.IsDeleted(owner);
				if(!isDeletedElem && be.sys.MayFireEvents())
				{
					string varName = "tempvar_" + tmpVarID++;
					string varType = "bool ";

					sb.AppendFront(varType + varName + " = ");
					GenExpression(sb, changedTarget, state);
					sb.Append(";\n");

					string prefix = sb.Indentation + varName + changedOperation;

					GenCompoundAssignment(sb, state, cass, prefix, ";\n");

					GenChangingAttribute(sb, state, changedTarget, "Assign", varName, "null");

					sb.AppendFront("");
					GenExpression(sb, changedTarget, state);
					sb.Append(" = " + varName + ";\n");

					GenChangedAttribute(sb, state, changedTarget);
				}
				else
					GenCompoundAssignment(sb, state, cass, sb.Indentation, ";\n");
			}
		}

		private void GenCompoundAssignmentChangedVar(SourceBuilder sb, ModifyGenerationStateConst state,
				CompoundAssignmentChangedVar cass)
		{
			Variable changedTarget = cass.ChangedTarget;
			string changedOperation;
			if(cass.ChangedOperation == CompoundAssignment.CompoundAssignmentType.UNION)
				changedOperation = " |= ";
			else if(cass.ChangedOperation == CompoundAssignment.CompoundAssignmentType.INTERSECTION)
				changedOperation = " &= ";
			else //if(cass.getChangedOperation()==CompoundAssignment.CompoundAssignmentType.ASSIGN)
				changedOperation = " = ";

			string prefix = sb.Indentation + FormatEntity(changedTarget) + changedOperation;

			GenCompoundAssignment(sb, state, cass, prefix, ";\n");
		}

		private void GenCompoundAssignmentChangedVisited(SourceBuilder sb, ModifyGenerationStateConst state,
				CompoundAssignmentChangedVisited cass)
		{
			Visited changedTarget = cass.ChangedTarget;

			SourceBuilder changedTargetBuffer = new SourceBuilder();
			GenExpression(changedTargetBuffer, changedTarget.Entity, state);
			changedTargetBuffer.Append(", ");
			GenExpression(changedTargetBuffer, changedTarget.VisitorID, state);

			string prefix = sb.Indentation + "graph.SetVisited("
					+ changedTargetBuffer.ToString() + ", ";
			if(cass.ChangedOperation != CompoundAssignment.CompoundAssignmentType.ASSIGN)
			{
				prefix += "graph.IsVisited(" + changedTargetBuffer.ToString() + ")"
						+ (cass.ChangedOperation == CompoundAssignment.CompoundAssignmentType.UNION ? " | " : " & ");
			}

			GenCompoundAssignment(sb, state, cass, prefix, ");\n");
		}

		private void GenCompoundAssignment(SourceBuilder sb, ModifyGenerationStateConst state, CompoundAssignment cass,
				string prefix, string postfix)
		{
			Qualification target = cass.Target;
			Debug.Assert((target.Type is MapType || target.Type is SetType
					|| target.Type is ArrayType || target.Type is DequeType));
			Expression expr = cass.Expression;

			Entity element = target.Owner;
			Entity attribute = target.Member;
			Type elementType = attribute.Owner;

			if(!state.IsDeleted(element) && be.sys.MayFireEvents())
			{
				sb.Append(prefix);
				if(cass.Operation == CompoundAssignment.CompoundAssignmentType.UNION)
					sb.Append("GRGEN_LIBGR.ContainerHelper.UnionChanged(");
				else if(cass.Operation == CompoundAssignment.CompoundAssignmentType.INTERSECTION)
					sb.Append("GRGEN_LIBGR.ContainerHelper.IntersectChanged(");
				else if(cass.Operation == CompoundAssignment.CompoundAssignmentType.WITHOUT)
					sb.Append("GRGEN_LIBGR.ContainerHelper.ExceptChanged(");
				else //if(cass.getOperation()==CompoundAssignment.CompoundAssignmentType.CONCATENATE)
					sb.Append("GRGEN_LIBGR.ContainerHelper.ConcatenateChanged(");
				GenExpression(sb, target, state);
				sb.Append(", ");
				GenExpression(sb, expr, state);
				sb.Append(", ");
				sb.Append("graph, "
						+ FormatEntity(element) + ", "
						+ FormatTypeClassRef(elementType) + "." + FormatAttributeTypeName(attribute));
				sb.Append(")");
				sb.Append(postfix);
			}
		}

		private void GenCompoundAssignmentVarChanged(SourceBuilder sb, ModifyGenerationStateConst state,
				CompoundAssignmentVarChanged cass)
		{
			Qualification changedTarget = cass.ChangedTarget;
			string changedOperation;
			if(cass.ChangedOperation == CompoundAssignmentVar.CompoundAssignmentType.UNION)
				changedOperation = " |= ";
			else if(cass.ChangedOperation == CompoundAssignmentVar.CompoundAssignmentType.INTERSECTION)
				changedOperation = " &= ";
			else //if(cass.getChangedOperation()==CompoundAssignmentVar.CompoundAssignmentType.ASSIGN)
				changedOperation = " = ";

			Entity owner = changedTarget.Owner;
			if(!state.IsDeleted(owner) && be.sys.MayFireEvents())
			{
				string varName = "tempvar_" + tmpVarID++;
				string varType = "bool ";

				sb.AppendFront(varType + varName + " = ");
				GenExpression(sb, changedTarget, state);
				sb.Append(";\n");

				string prefix = sb.Indentation + varName + changedOperation;

				GenCompoundAssignmentVar(sb, state, cass, prefix, ";\n");

				GenChangingAttribute(sb, state, changedTarget, "Assign", varName, "null");

				sb.AppendFront("");
				GenExpression(sb, changedTarget, state);
				sb.Append(" = " + varName + ";\n");

				GenChangedAttribute(sb, state, changedTarget);
			}
			else
				GenCompoundAssignmentVar(sb, state, cass, sb.Indentation, ";\n");
		}

		private void GenCompoundAssignmentVarChangedVar(SourceBuilder sb, ModifyGenerationStateConst state,
				CompoundAssignmentVarChangedVar cass)
		{
			Variable changedTarget = cass.ChangedTarget;
			string changedOperation;
			if(cass.ChangedOperation == CompoundAssignmentVar.CompoundAssignmentType.UNION)
				changedOperation = " |= ";
			else if(cass.ChangedOperation == CompoundAssignmentVar.CompoundAssignmentType.INTERSECTION)
				changedOperation = " &= ";
			else //if(cass.getChangedOperation()==CompoundAssignmentVar.CompoundAssignmentType.ASSIGN)
				changedOperation = " = ";

			string prefix = sb.Indentation + FormatEntity(changedTarget) + changedOperation;

			GenCompoundAssignmentVar(sb, state, cass, prefix, ";\n");
		}

		private void GenCompoundAssignmentVarChangedVisited(SourceBuilder sb, ModifyGenerationStateConst state,
				CompoundAssignmentVarChangedVisited cass)
		{
			Visited changedTarget = cass.ChangedTarget;

			SourceBuilder changedTargetBuffer = new SourceBuilder();
			GenExpression(changedTargetBuffer, changedTarget.Entity, state);
			changedTargetBuffer.Append(", ");
			GenExpression(changedTargetBuffer, changedTarget.VisitorID, state);

			string prefix = sb.Indentation + "graph.SetVisited("
					+ changedTargetBuffer.ToString() + ", ";
			if(cass.ChangedOperation != CompoundAssignmentVar.CompoundAssignmentType.ASSIGN)
			{
				prefix += "graph.IsVisited(" + changedTargetBuffer.ToString() + ")"
						+ (cass.ChangedOperation == CompoundAssignmentVar.CompoundAssignmentType.UNION ? " | " : " & ");
			}

			GenCompoundAssignmentVar(sb, state, cass, prefix, ");\n");
		}

		private void GenCompoundAssignmentVar(SourceBuilder sb, ModifyGenerationStateConst state,
				CompoundAssignmentVar cass, string prefix, string postfix)
		{
			Variable target = cass.Target;
			Debug.Assert((target.Type is MapType || target.Type is SetType
					|| target.Type is ArrayType || target.Type is DequeType));
			Expression expr = cass.Expression;

			sb.Append(prefix);
			if(cass.Operation == CompoundAssignmentVar.CompoundAssignmentType.UNION)
				sb.Append("GRGEN_LIBGR.ContainerHelper.UnionChanged(");
			else if(cass.Operation == CompoundAssignmentVar.CompoundAssignmentType.INTERSECTION)
				sb.Append("GRGEN_LIBGR.ContainerHelper.IntersectChanged(");
			else if(cass.Operation == CompoundAssignmentVar.CompoundAssignmentType.WITHOUT)
				sb.Append("GRGEN_LIBGR.ContainerHelper.ExceptChanged(");
			else //if(cass.getOperation()==CompoundAssignmentVar.CompoundAssignmentType.CONCATENATE)
				sb.Append("GRGEN_LIBGR.ContainerHelper.ConcatenateChanged(");
			sb.Append(FormatEntity(target));
			sb.Append(", ");
			GenExpression(sb, expr, state);
			sb.Append(")");
			sb.Append(postfix);
		}

		private void GenMapRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, MapRemoveItem mri)
		{
			Qualification target = mri.Target;

			string varName = GenTempVarFromExpression(sb, state, mri.KeyExpr, null);

			GenChangingAttribute(sb, state, target, "RemoveElement", "null", varName);

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append(".Remove(");
			if(mri.KeyExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(mri.KeyExpr.Type) + ")(" + varName + ")");
			else
				sb.Append(varName);
			sb.Append(");\n");

			GenChangedAttribute(sb, state, target);

			if(mri.Next != null)
				GenEvalStmt(sb, state, mri.Next);
		}

		private void GenMapClear(SourceBuilder sb, ModifyGenerationStateConst state, MapClear mc)
		{
			Qualification target = mc.Target;

			GenClearAttribute(sb, state, target);

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append(".Clear();\n");

			GenClearedAttribute(sb, state, target);

			if(mc.Next != null)
				GenEvalStmt(sb, state, mc.Next);
		}

		private void GenMapAddItem(SourceBuilder sb, ModifyGenerationStateConst state, MapAddItem mai)
		{
			Qualification target = mai.Target;

			string keyVarName = GenTempVarFromExpression(sb, state, mai.KeyExpr, null);
			string valueVarName = GenTempVarFromExpression(sb, state, mai.ValueExpr, null);

			GenChangingAttribute(sb, state, target, "PutElement", valueVarName, keyVarName);

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append("[");
			if(mai.KeyExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(mai.KeyExpr.Type) + ")(" + keyVarName + ")");
			else
				sb.Append(keyVarName);
			sb.Append("] = ");
			if(mai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(mai.ValueExpr.Type) + ")(" + valueVarName + ")");
			else
				sb.Append(valueVarName);
			sb.Append(";\n");

			GenChangedAttribute(sb, state, target);

			if(mai.Next != null)
				GenEvalStmt(sb, state, mai.Next);
		}

		private void GenSetRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, SetRemoveItem sri)
		{
			Qualification target = sri.Target;

			string valueVarName = GenTempVarFromExpression(sb, state, sri.ValueExpr, null);

			GenChangingAttribute(sb, state, target, "RemoveElement", valueVarName, "null");

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append(".Remove(");
			if(sri.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(sri.ValueExpr.Type) + ")(" + valueVarName + ")");
			else
				sb.Append(valueVarName);
			sb.Append(");\n");

			GenChangedAttribute(sb, state, target);

			if(sri.Next != null)
				GenEvalStmt(sb, state, sri.Next);
		}

		private void GenSetClear(SourceBuilder sb, ModifyGenerationStateConst state, SetClear sc)
		{
			Qualification target = sc.Target;

			GenClearAttribute(sb, state, target);

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append(".Clear();\n");

			GenClearedAttribute(sb, state, target);

			if(sc.Next != null)
				GenEvalStmt(sb, state, sc.Next);
		}

		private void GenSetAddItem(SourceBuilder sb, ModifyGenerationStateConst state, SetAddItem sai)
		{
			Qualification target = sai.Target;

			string valueVarName = GenTempVarFromExpression(sb, state, sai.ValueExpr, null);

			GenChangingAttribute(sb, state, target, "PutElement", valueVarName, "null");

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append("[");
			if(sai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(sai.ValueExpr.Type) + ")(" + valueVarName + ")");
			else
				sb.Append(valueVarName);
			sb.Append("] = null;\n");

			GenChangedAttribute(sb, state, target);

			if(sai.Next != null)
				GenEvalStmt(sb, state, sai.Next);
		}

		private void GenArrayRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayRemoveItem ari)
		{
			Qualification target = ari.Target;

			string indexVarName = null;
			if(ari.IndexExpr != null)
				indexVarName = GenTempVarFromExpression(sb, state, ari.IndexExpr, null);

			GenChangingAttribute(sb, state, target, "RemoveElement", "null", ari.IndexExpr != null ? indexVarName : "null");

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append(".RemoveAt(");
			if(ari.IndexExpr != null)
				sb.Append(indexVarName);
			else
			{
				sb.Append("(");
				GenExpression(sb, target, state);
				sb.Append(").Count - 1");
			}
			sb.Append(");\n");

			GenChangedAttribute(sb, state, target);

			if(ari.Next != null)
				GenEvalStmt(sb, state, ari.Next);
		}

		private void GenArrayClear(SourceBuilder sb, ModifyGenerationStateConst state, ArrayClear ac)
		{
			Qualification target = ac.Target;

			GenClearAttribute(sb, state, target);

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append(".Clear();\n");

			GenClearedAttribute(sb, state, target);

			if(ac.Next != null)
				GenEvalStmt(sb, state, ac.Next);
		}

		private void GenArrayAddItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayAddItem aai)
		{
			Qualification target = aai.Target;

			string valueVarName = GenTempVarFromExpression(sb, state, aai.ValueExpr, null);

			string indexVarName = null;
			if(aai.IndexExpr != null)
				indexVarName = GenTempVarFromExpression(sb, state, aai.IndexExpr, null);

			GenChangingAttribute(sb, state, target, "PutElement", valueVarName, aai.IndexExpr != null ? indexVarName : "null");

			sb.AppendFront("");
			GenExpression(sb, target, state);
			if(aai.IndexExpr == null)
				sb.Append(".Add(");
			else
			{
				sb.Append(".Insert(");
				sb.Append(indexVarName);
				sb.Append(", ");
			}
			if(aai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(aai.ValueExpr.Type) + ")(" + valueVarName + ")");
			else
				sb.Append(valueVarName);
			sb.Append(");\n");

			GenChangedAttribute(sb, state, target);

			if(aai.Next != null)
				GenEvalStmt(sb, state, aai.Next);
		}

		private void GenDequeRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeRemoveItem dri)
		{
			Qualification target = dri.Target;

			string indexVarName = null;
			if(dri.IndexExpr != null)
				indexVarName = GenTempVarFromExpression(sb, state, dri.IndexExpr, null);

			GenChangingAttribute(sb, state, target, "RemoveElement", "null", dri.IndexExpr != null ? indexVarName : "null");

			sb.AppendFront("");
			GenExpression(sb, target, state);
			if(dri.IndexExpr != null)
				sb.Append(".DequeueAt(" + indexVarName + ");\n");
			else
				sb.Append(".Dequeue();\n");

			GenChangedAttribute(sb, state, target);

			if(dri.Next != null)
				GenEvalStmt(sb, state, dri.Next);
		}

		private void GenDequeClear(SourceBuilder sb, ModifyGenerationStateConst state, DequeClear dc)
		{
			Qualification target = dc.Target;

			GenClearAttribute(sb, state, target);

			sb.AppendFront("");
			GenExpression(sb, target, state);
			sb.Append(".Clear();\n");

			GenClearedAttribute(sb, state, target);

			if(dc.Next != null)
				GenEvalStmt(sb, state, dc.Next);
		}

		private void GenDequeAddItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeAddItem dai)
		{
			Qualification target = dai.Target;

			string valueVarName = GenTempVarFromExpression(sb, state, dai.ValueExpr, null);

			string indexVarName = null;
			if(dai.IndexExpr != null)
				indexVarName = GenTempVarFromExpression(sb, state, dai.IndexExpr, null);

			GenChangingAttribute(sb, state, target, "PutElement", valueVarName, dai.IndexExpr != null ? indexVarName : "null");

			sb.AppendFront("");
			GenExpression(sb, target, state);
			if(dai.IndexExpr == null)
				sb.Append(".Enqueue(");
			else
			{
				sb.Append(".EnqueueAt(");
				sb.Append(indexVarName);
				sb.Append(", ");
			}
			if(dai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(dai.ValueExpr.Type) + ")(" + valueVarName + ")");
			else
				sb.Append(valueVarName);
			sb.Append(");\n");

			GenChangedAttribute(sb, state, target);

			if(dai.Next != null)
				GenEvalStmt(sb, state, dai.Next);
		}

		private void GenMapVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, MapVarRemoveItem mvri)
		{
			Variable target = mvri.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, mvri.KeyExpr, state);
			string keyExprStr = sbtmp.ToString();

			GenVar(sb, target, state);
			sb.Append(".Remove(");
			if(mvri.KeyExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(mvri.KeyExpr.Type) + ")(" + keyExprStr + ")");
			else
				sb.Append(keyExprStr);
			sb.Append(");\n");

			Debug.Assert(mvri.Next == null);
		}

		private void GenMapVarClear(SourceBuilder sb, ModifyGenerationStateConst state, MapVarClear mvc)
		{
			Variable target = mvc.Target;

			GenVar(sb, target, state);
			sb.Append(".Clear();\n");

			Debug.Assert(mvc.Next == null);
		}

		private void GenMapVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, MapVarAddItem mvai)
		{
			Variable target = mvai.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, mvai.ValueExpr, state);
			string valueExprStr = sbtmp.ToString();
			sbtmp.Delete(0, sbtmp.Length());
			GenExpression(sbtmp, mvai.KeyExpr, state);
			string keyExprStr = sbtmp.ToString();

			GenVar(sb, target, state);
			sb.Append("[");
			if(mvai.KeyExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(mvai.KeyExpr.Type) + ")(" + keyExprStr + ")");
			else
				sb.Append(keyExprStr);
			sb.Append("] = ");
			if(mvai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(mvai.ValueExpr.Type) + ")(" + valueExprStr + ")");
			else
				sb.Append(valueExprStr);
			sb.Append(";\n");

			Debug.Assert(mvai.Next == null);
		}

		private void GenSetVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, SetVarRemoveItem svri)
		{
			Variable target = svri.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, svri.ValueExpr, state);
			string valueExprStr = sbtmp.ToString();

			GenVar(sb, target, state);
			sb.Append(".Remove(");
			if(svri.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(svri.ValueExpr.Type) + ")(" + valueExprStr + ")");
			else
				sb.Append(valueExprStr);
			sb.Append(");\n");

			Debug.Assert(svri.Next == null);
		}

		private void GenSetVarClear(SourceBuilder sb, ModifyGenerationStateConst state, SetVarClear svc)
		{
			Variable target = svc.Target;

			GenVar(sb, target, state);
			sb.Append(".Clear();\n");

			Debug.Assert(svc.Next == null);
		}

		private void GenSetVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, SetVarAddItem svai)
		{
			Variable target = svai.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, svai.ValueExpr, state);
			string valueExprStr = sbtmp.ToString();

			GenVar(sb, target, state);
			sb.Append("[");
			if(svai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(svai.ValueExpr.Type) + ")(" + valueExprStr + ")");
			else
				sb.Append(valueExprStr);
			sb.Append("] = null;\n");

			Debug.Assert(svai.Next == null);
		}

		private void GenSetVarAddAll(SourceBuilder sb, ModifyGenerationStateConst state, SetVarAddAll svaa)
		{
			Variable target = svaa.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, svaa.ValueExpr, state);
			string valueExprStr = sbtmp.ToString();

			SetType setType = (SetType)svaa.ValueExpr.Type;
			string setValueName = "value_" + svaa.Id;
			sb.Append("foreach(" + FormatType(setType.valueType) + " " + setValueName + " in (" + valueExprStr + ").Keys)\n");
			sb.Append("{\n");
			sb.Indent();
			GenVar(sb, target, state);
			sb.Append(".Add(" + setValueName + ", null);\n");
			sb.Unindent();
			sb.Append("}\n");

			Debug.Assert(svaa.Next == null);
		}

		private void GenArrayVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayVarRemoveItem avri)
		{
			Variable target = avri.Target;

			string indexStr = "null";
			if(avri.IndexExpr != null)
			{
				SourceBuilder sbtmp = new SourceBuilder();
				GenExpression(sbtmp, avri.IndexExpr, state);
				indexStr = sbtmp.ToString();
			}

			GenVar(sb, target, state);
			sb.Append(".RemoveAt(");

			if(avri.IndexExpr != null)
				sb.Append(indexStr);
			else
			{
				sb.Append("(");
				sb.Append(FormatEntity(target));
				sb.Append(").Count - 1");
			}
			sb.Append(");\n");

			Debug.Assert(avri.Next == null);
		}

		private void GenArrayVarClear(SourceBuilder sb, ModifyGenerationStateConst state, ArrayVarClear avc)
		{
			Variable target = avc.Target;

			GenVar(sb, target, state);
			sb.Append(".Clear();\n");

			Debug.Assert(avc.Next == null);
		}

		private void GenArrayVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, ArrayVarAddItem avai)
		{
			Variable target = avai.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, avai.ValueExpr, state);
			string valueExprStr = sbtmp.ToString();

			sbtmp = new SourceBuilder();
			string indexExprStr = "null";
			if(avai.IndexExpr != null)
			{
				GenExpression(sbtmp, avai.IndexExpr, state);
				indexExprStr = sbtmp.ToString();
			}

			GenVar(sb, target, state);
			if(avai.IndexExpr == null)
				sb.Append(".Add(");
			else
			{
				sb.Append(".Insert(");
				sb.Append(indexExprStr);
				sb.Append(", ");
			}
			if(avai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(avai.ValueExpr.Type) + ")(" + valueExprStr + ")");
			else
				sb.Append(valueExprStr);
			sb.Append(");\n");

			Debug.Assert(avai.Next == null);
		}

		private void GenArrayVarAddAll(SourceBuilder sb, ModifyGenerationStateConst state, ArrayVarAddAll avaa)
		{
			Variable target = avaa.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, avaa.ValueExpr, state);
			string valueExprStr = sbtmp.ToString();

			GenVar(sb, target, state);
			sb.Append(".AddRange(");
			sb.Append(valueExprStr);
			sb.Append(");\n");

			Debug.Assert(avaa.Next == null);
		}

		private void GenDequeVarRemoveItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeVarRemoveItem dvri)
		{
			Variable target = dvri.Target;

			string indexStr = "null";
			if(dvri.IndexExpr != null)
			{
				SourceBuilder sbtmp = new SourceBuilder();
				GenExpression(sbtmp, dvri.IndexExpr, state);
				indexStr = sbtmp.ToString();
			}

			GenVar(sb, target, state);
			if(dvri.IndexExpr != null)
				sb.Append(".DequeueAt(" + indexStr + ");\n");
			else
				sb.Append(".Dequeue();\n");

			Debug.Assert(dvri.Next == null);
		}

		private void GenDequeVarClear(SourceBuilder sb, ModifyGenerationStateConst state, DequeVarClear dvc)
		{
			Variable target = dvc.Target;

			GenVar(sb, target, state);
			sb.Append(".Clear();\n");

			Debug.Assert(dvc.Next == null);
		}

		private void GenDequeVarAddItem(SourceBuilder sb, ModifyGenerationStateConst state, DequeVarAddItem dvai)
		{
			Variable target = dvai.Target;

			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, dvai.ValueExpr, state);
			string valueExprStr = sbtmp.ToString();

			sbtmp = new SourceBuilder();
			string indexExprStr = "null";
			if(dvai.IndexExpr != null)
			{
				GenExpression(sbtmp, dvai.IndexExpr, state);
				indexExprStr = sbtmp.ToString();
			}

			GenVar(sb, target, state);
			if(dvai.IndexExpr == null)
				sb.Append(".Enqueue(");
			else
			{
				sb.Append(".EnqueueAt(");
				sb.Append(indexExprStr);
				sb.Append(", ");
			}
			if(dvai.ValueExpr is GraphEntityExpression)
				sb.Append("(" + FormatElementInterfaceRef(dvai.ValueExpr.Type) + ")(" + valueExprStr + ")");
			else
				sb.Append(valueExprStr);
			sb.Append(");\n");

			Debug.Assert(dvai.Next == null);
		}

		private void GenVar(SourceBuilder sb, Variable var, ModifyGenerationStateConst state)
		{
			if(!Expression.IsGlobalVariable(var))
				sb.AppendFront(FormatEntity(var));
			else
				sb.Append(FormatGlobalVariableRead(var));
		}

		private static void GenReturnStatementFilter(SourceBuilder sb, ModifyGenerationStateConst state, ReturnStatementFilter rsf)
		{
			if(!string.ReferenceEquals(state.MatchClassName, null))
				sb.AppendFront("GRGEN_LIBGR.MatchListHelper.FromList(matches, this_matches);\n");
			else
				sb.AppendFront("matches.FromListExact();\n");
			sb.AppendFront("return;\n");
		}

		private void GenReturnStatement(SourceBuilder sb, ModifyGenerationStateConst state, ReturnStatement rs)
		{
			sb.AppendFront("return ");
			GenExpression(sb, rs.ReturnValueExpr, state);
			sb.Append(";\n");
		}

		private void GenReturnStatementProcedure(SourceBuilder sb, ModifyGenerationStateConst state,
				ReturnStatementProcedure rsp)
		{
			int i = 0;
			foreach(Expression returnValueExpr in rsp.ReturnValueExpr)
			{
				sb.AppendFront("_out_param_" + i + " = ");
				GenExpression(sb, returnValueExpr, state);
				sb.Append(";\n");
				++i;
			}
			if(be.sys.MayFireDebugEvents())
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting(");
				sb.Append("\"" + state.Name + "\"");
				for(int j = 0; j < i; ++j)
					sb.Append(", _out_param_" + j);
				sb.Append(");\n");
			}
			sb.AppendFront("return;\n");
		}

		private void GenConditionStatement(SourceBuilder sb, ModifyGenerationStateConst state, ConditionStatement cs)
		{
			sb.AppendFront("if(");
			GenExpression(sb, cs.ConditionExpr, state);
			sb.Append(") {\n");
			sb.Indent();
			GenEvals(sb, state, cs.Statements);
			if(cs.FalseCaseStatements != null)
			{
				sb.Unindent();
				sb.AppendFront("} else {\n");
				sb.Indent();
				GenEvals(sb, state, cs.FalseCaseStatements);
			}
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenSwitchStatement(SourceBuilder sb, ModifyGenerationStateConst state, SwitchStatement ss)
		{
			sb.AppendFront("switch(");
			GenExpression(sb, ss.SwitchExpr, state);
			sb.Append(") {\n");
			foreach(CaseStatement cs in ss.Statements)
				GenCaseStatement(sb, state, cs);
			sb.Append("}\n");
		}

		private void GenCaseStatement(SourceBuilder sb, ModifyGenerationStateConst state, CaseStatement cs)
		{
			if(cs.CaseConstantExpr != null)
			{
				sb.AppendFront("case ");
				GenExpression(sb, cs.CaseConstantExpr, state);
				sb.Append(": ");
			}
			else
				sb.AppendFront("default: ");
			sb.Append("{\n");
			sb.Indent();
			GenEvals(sb, state, cs.Statements);
			sb.AppendFront("break;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenWhileStatement(SourceBuilder sb, ModifyGenerationStateConst state, WhileStatement ws)
		{
			sb.AppendFront("while(");
			GenExpression(sb, ws.ConditionExpr, state);
			sb.Append(") {\n");
			sb.Indent();
			GenEvals(sb, state, ws.Statements);
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenDoWhileStatement(SourceBuilder sb, ModifyGenerationStateConst state, DoWhileStatement dws)
		{
			sb.AppendFront("do {\n");
			sb.Indent();
			GenEvals(sb, state, dws.Statements);
			sb.Unindent();
			sb.AppendFront("} while(");
			GenExpression(sb, dws.ConditionExpr, state);
			sb.Append(");\n");
		}

		private void GenMultiStatement(SourceBuilder sb, ModifyGenerationStateConst state, MultiStatement ms)
		{
			GenEvals(sb, state, ms.Statements);
		}

		private void GenDefDeclVarStatement(SourceBuilder sb, ModifyGenerationStateConst state, DefDeclVarStatement ddvs)
		{
			Variable var = ddvs.Target;
			if(var.Ident.ToString().Equals("this"))
			{
				if(var.Type is ArrayType)
				{
					if(!string.ReferenceEquals(state.MatchClassName, null))
					{
						sb.AppendFront(FormatType(var.Type) + " this_matches = GRGEN_LIBGR.MatchListHelper.ToList<"
						+ state.PackagePrefix + "IMatch_" + state.MatchClassName + ">(matches);\n");
					}
					else
						sb.AppendFront(FormatType(var.Type) + " this_matches = matches.ToListExact();\n");
				}
				return; // don't emit a declaration for the fake "this" entity of a method / emit this_matches in case of a this of array type (of match value type, appears in filters)
			}
			sb.AppendFront(FormatType(var.Type) + " " + FormatEntity(var));
			if(var.initialization != null)
			{
				sb.Append(" = ");
				sb.Append("(" + FormatType(var.Type) + ")(");
				GenExpression(sb, var.initialization, state);
				sb.Append(")");
			}
			else
				sb.Append(" = " + GetInitializationValue(var.Type));
			sb.Append(";\n");
		}

		private void GenDefDeclGraphEntityStatement(SourceBuilder sb, ModifyGenerationStateConst state,
				DefDeclGraphEntityStatement ddges)
		{
			GraphEntity graphEntity = ddges.Target;
			if(graphEntity.Ident.ToString().Equals("this"))
				return; // don't emit a declaration for the fake "this" entity of a method
			sb.AppendFront(FormatType(graphEntity.Type) + " " + FormatEntity(graphEntity));
			if(graphEntity.initialization != null)
			{
				sb.Append(" = ");
				sb.Append("(" + FormatType(graphEntity.Type) + ")(");
				GenExpression(sb, graphEntity.initialization, state);
				sb.Append(")");
			}
			else
				sb.Append(" = " + GetInitializationValue(graphEntity.Type));
			sb.Append(";\n");
		}

		private void GenContainerAccumulationYield(SourceBuilder sb, ModifyGenerationStateConst state,
				ContainerAccumulationYield cay)
		{
			if(cay.Container.Type is ArrayType)
			{
				Type arrayValueType = ((ArrayType)cay.Container.Type).ValueType;
				string arrayValueTypeStr = FormatType(arrayValueType);
				string entryVarTypeStr = FormatType(cay.IterationVar.Type);
				string indexVar = "index_" + tmpVarID++;
				string entryVar = "entry_" + tmpVarID++; // for the container itself
				sb.AppendFront("List<" + arrayValueTypeStr + "> " + entryVar + " = "
						+ "(List<" + arrayValueTypeStr + ">) " + FormatEntity(cay.Container) + ";\n");
				sb.AppendFront("for(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
				sb.AppendFront("{\n");
				sb.Indent();

				if(cay.IndexVar != null)
				{
					if(!Expression.IsGlobalVariable(cay.IndexVar) || (cay.IndexVar.Context
						 & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront("int" + " " + FormatEntity(cay.IndexVar) + " = " + indexVar + ";\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IndexVar, indexVar) + ";\n");
					if(!Expression.IsGlobalVariable(cay.IterationVar) || (cay.IterationVar.Context
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront(entryVarTypeStr + " " + FormatEntity(cay.IterationVar) + " = ("
								+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IterationVar, entryVar + "[" + indexVar + "]") + ";\n");
				}
				else
				{
					if(!Expression.IsGlobalVariable(cay.IterationVar) || (cay.IterationVar.Context
							& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront(entryVarTypeStr + " " + FormatEntity(cay.IterationVar) + " = ("
								+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IterationVar, entryVar + "[" + indexVar + "]") + ";\n");
				}

				GenEvals(sb, state, cay.Statements);

				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else if(cay.Container.Type is DequeType)
			{
				Type dequeValueType = ((DequeType)cay.Container.Type).ValueType;
				string dequeValueTypeStr = FormatType(dequeValueType);
				string entryVarTypeStr = FormatType(cay.IterationVar.Type);
				string indexVar = "index_" + tmpVarID++;
				string entryVar = "entry_" + tmpVarID++; // for the container itself
				sb.AppendFront("GRGEN_LIBGR.Deque<" + dequeValueTypeStr + "> " + entryVar + " = "
						+ "(GRGEN_LIBGR.Deque<" + dequeValueTypeStr + ">) " + FormatEntity(cay.Container) + ";\n");
				sb.AppendFront("for(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
				sb.AppendFront("{\n");
				sb.Indent();

				if(cay.IndexVar != null)
				{
					if(!Expression.IsGlobalVariable(cay.IndexVar) || (cay.IndexVar.Context
							& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront("int" + " " + FormatEntity(cay.IndexVar) + " = " + indexVar + ";\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IndexVar, indexVar) + ";\n");
					if(!Expression.IsGlobalVariable(cay.IterationVar) || (cay.IterationVar.Context
							& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront(entryVarTypeStr + " " + FormatEntity(cay.IterationVar) + " = ("
								+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IterationVar, entryVar + "[" + indexVar + "]") + ";\n");
				}
				else
				{
					if(!Expression.IsGlobalVariable(cay.IterationVar) || (cay.IterationVar.Context
							& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront(entryVarTypeStr + " " + FormatEntity(cay.IterationVar) + " = ("
								+ entryVarTypeStr + ")" + entryVar + "[" + indexVar + "];\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IterationVar, entryVar + "[" + indexVar + "]") + ";\n");
				}

				GenEvals(sb, state, cay.Statements);

				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else if(cay.Container.Type is SetType)
			{
				Type setValueType = ((SetType)cay.Container.Type).ValueType;
				string setValueTypeStr = FormatType(setValueType);
				string entryVarTypeStr = FormatType(cay.IterationVar.Type);
				string entryVar = "entry_" + tmpVarID++;
				sb.AppendFront("foreach(KeyValuePair<" + setValueTypeStr + ", GRGEN_LIBGR.SetValueType> " + entryVar
						+ " in " + FormatEntity(cay.Container) + ")\n");
				sb.AppendFront("{\n");
				sb.Indent();

				if(!Expression.IsGlobalVariable(cay.IterationVar) || (cay.IterationVar.Context
						& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
				{
					sb.AppendFront(entryVarTypeStr + " " + FormatEntity(cay.IterationVar));
					sb.Append(" = (" + entryVarTypeStr + ")" + entryVar + ".Key;\n");
				}
				else
					sb.AppendFront(FormatGlobalVariableWrite(cay.IterationVar, entryVar + ".Key") + ";\n");

				GenEvals(sb, state, cay.Statements);

				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else //if(cay.getContainer().getType() instanceof MapType)
			{
				Type mapKeyType = ((MapType)cay.Container.Type).KeyType;
				string mapKeyTypeStr = FormatType(mapKeyType);
				Type mapValueType = ((MapType)cay.Container.Type).ValueType;
				string mapValueTypeStr = FormatType(mapValueType);
				string keyVarTypeStr = cay.IndexVar != null
						? FormatType(cay.IndexVar.Type)
						: FormatType(cay.IterationVar.Type);
				string valueVarTypeStr = FormatType(cay.IterationVar.Type);
				string entryVar = "entry_" + tmpVarID++;
				sb.AppendFront("foreach(KeyValuePair<" + mapKeyTypeStr + ", " + mapValueTypeStr + "> " + entryVar
						+ " in " + FormatEntity(cay.Container) + ")\n");
				sb.AppendFront("{\n");
				sb.Indent();

				if(cay.IndexVar != null)
				{
					if(!Expression.IsGlobalVariable(cay.IndexVar) || (cay.IndexVar.Context
							& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront(keyVarTypeStr + " " + FormatEntity(cay.IndexVar) + " = ("
								+ keyVarTypeStr + ")" + entryVar + ".Key;\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IndexVar, entryVar + ".Key") + ";\n");
					if(!Expression.IsGlobalVariable(cay.IterationVar) || (cay.IterationVar.Context
							& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront(valueVarTypeStr + " " + FormatEntity(cay.IterationVar) + " = ("
								+ valueVarTypeStr + ")" + entryVar + ".Value;\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IterationVar, entryVar + ".Value") + ";\n");
				}
				else
				{
					if(!Expression.IsGlobalVariable(cay.IterationVar) || (cay.IterationVar.Context
							& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
					{
						sb.AppendFront(keyVarTypeStr + " " + FormatEntity(cay.IterationVar) + " = ("
								+ keyVarTypeStr + ")" + entryVar + ".Key;\n");
					}
					else
						sb.AppendFront(FormatGlobalVariableWrite(cay.IterationVar, entryVar + ".Key") + ";\n");
				}

				GenEvals(sb, state, cay.Statements);

				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenIntegerRangeIterationYield(SourceBuilder sb, ModifyGenerationStateConst state,
				IntegerRangeIterationYield iriy)
		{
			string ascendingVar = "ascending_" + tmpVarID++;
			string entryVar = "entry_" + tmpVarID++;
			string limitVar = "limit_" + tmpVarID++;
			sb.AppendFront("int " + entryVar + " = ");
			GenExpression(sb, iriy.LeftExpr, state);
			sb.Append(";\n");
			sb.AppendFront("int " + limitVar + " = ");
			GenExpression(sb, iriy.RightExpr, state);
			sb.Append(";\n");
			sb.AppendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");
			sb.AppendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			if(!Expression.IsGlobalVariable(iriy.IterationVar) || (iriy.IterationVar.Context
					& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
			{
				sb.AppendFront("int " + FormatEntity(iriy.IterationVar) + " = " + entryVar + ";\n");
			}
			else
				sb.AppendFront(FormatGlobalVariableWrite(iriy.IterationVar, entryVar) + ";\n");

			GenEvals(sb, state, iriy.Statements);

			sb.AppendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenMatchesAccumulationYield(SourceBuilder sb, ModifyGenerationStateConst state,
				MatchesAccumulationYield may)
		{
			Type arrayValueType = may.IterationVar.Type;
			string arrayValueTypeStr = FormatType(arrayValueType);
			string indexVar = "index_" + tmpVarID++;
			string entryVar = "entry_" + tmpVarID++;
			sb.AppendFront("List<" + arrayValueTypeStr + "> " + entryVar + " = "
					+ "(List<" + arrayValueTypeStr + ">) " + FormatEntity(may.MatchesVar) + ";\n");
			sb.AppendFront("for(int " + indexVar + "=0; " + indexVar + "<" + entryVar + ".Count; ++" + indexVar + ")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			if(!Expression.IsGlobalVariable(may.IterationVar) || (may.IterationVar.Context
					& BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
			{
				sb.AppendFront(arrayValueTypeStr + " " + FormatEntity(may.IterationVar) + " = "
						+ entryVar + "[" + indexVar + "];\n");
			}
			else
				sb.AppendFront(FormatGlobalVariableWrite(may.IterationVar, entryVar + "[" + indexVar + "]") + ";\n");

			GenEvals(sb, state, may.Statements);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenForFunction(SourceBuilder sb, ModifyGenerationStateConst state, ForFunction ff)
		{
			string id = Convert.ToString(tmpVarID++);

			if(ff.Function is AdjacentNodeExpr)
			{
				AdjacentNodeExpr adjacent = (AdjacentNodeExpr)ff.Function;
				if(adjacent.Direction() == Direction.INCIDENT)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, adjacent.StartNodeExpr, state);
					sb.Append(";\n");
					if(!state.EmitProfilingInstrumentation())
					{
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
								+ ".GetCompatibleIncident(");
						GenExpression(sb, adjacent.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
					}
					else
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id + ".Incident)\n");
					sb.AppendFront("{\n");
					sb.Indent();

					if(state.EmitProfilingInstrumentation())
					{
						if(state.IsToBeParallelizedActionExisting())
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
						else
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
						sb.AppendFront("if(!edge_" + id + ".InstanceOf(");
						GenExpression(sb, adjacent.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
						sb.AppendFrontIndented("continue;\n");
					}

					sb.AppendFront("if(!edge_" + id + ".Opposite(node_" + id + ").InstanceOf(");
					GenExpression(sb, adjacent.AdjacentNodeTypeExpr, state);
					sb.Append("))\n");
					sb.AppendFrontIndented("continue;\n");
					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id
							+ ".Opposite(node_" + id + ");\n");
				}
				else if(adjacent.Direction() == Direction.INCOMING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, adjacent.StartNodeExpr, state);
					sb.Append(";\n");
					if(!state.EmitProfilingInstrumentation())
					{
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
								+ ".GetCompatibleIncoming(");
						GenExpression(sb, adjacent.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
					}
					else
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id + ".Incoming)\n");
					sb.AppendFront("{\n");
					sb.Indent();

					if(state.EmitProfilingInstrumentation())
					{
						if(state.IsToBeParallelizedActionExisting())
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
						else
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
						sb.AppendFront("if(!edge_" + id + ".InstanceOf(");
						GenExpression(sb, adjacent.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
						sb.AppendFrontIndented("continue;\n");
					}

					sb.AppendFront("if(!edge_" + id + ".Source.InstanceOf(");
					GenExpression(sb, adjacent.AdjacentNodeTypeExpr, state);
					sb.Append("))\n");
					sb.AppendFrontIndented("continue;\n");
					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id
							+ ".Source;\n");
				}
				else if(adjacent.Direction() == Direction.OUTGOING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, adjacent.StartNodeExpr, state);
					sb.Append(";\n");
					if(!state.EmitProfilingInstrumentation())
					{
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
								+ ".GetCompatibleOutgoing(");
						GenExpression(sb, adjacent.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
					}
					else
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id + ".Outgoing)\n");
					sb.AppendFront("{\n");
					sb.Indent();

					if(state.EmitProfilingInstrumentation())
					{
						if(state.IsToBeParallelizedActionExisting())
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
						else
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
						sb.AppendFront("if(!edge_" + id + ".InstanceOf(");
						GenExpression(sb, adjacent.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
						sb.AppendFrontIndented("continue;\n");
					}

					sb.AppendFront("if(!edge_" + id + ".Target.InstanceOf(");
					GenExpression(sb, adjacent.AdjacentNodeTypeExpr, state);
					sb.Append("))\n");
					sb.AppendFrontIndented("continue;\n");
					sb.Append(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id
							+ ".Target;\n");
				}
			}
			else if(ff.Function is IncidentEdgeExpr)
			{
				IncidentEdgeExpr incident = (IncidentEdgeExpr)ff.Function;
				if(incident.Direction() == Direction.INCIDENT)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, incident.StartNodeExpr, state);
					sb.Append(";\n");
					if(!state.EmitProfilingInstrumentation())
					{
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
								+ ".GetCompatibleIncident(");
						GenExpression(sb, incident.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
					}
					else
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id + ".Incident)\n");
					sb.AppendFront("{\n");
					sb.Indent();

					if(state.EmitProfilingInstrumentation())
					{
						if(state.IsToBeParallelizedActionExisting())
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
						else
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
						sb.AppendFront("if(!edge_" + id + ".InstanceOf(");
						GenExpression(sb, incident.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
						sb.AppendFrontIndented("continue;\n");
					}

					sb.AppendFront("if(!edge_" + id + ".Opposite(node_" + id + ").InstanceOf(");
					GenExpression(sb, incident.AdjacentNodeTypeExpr, state);
					sb.Append("))\n");
					sb.AppendFrontIndented("continue;\n");
					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type)
							+ " " + FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
				else if(incident.Direction() == Direction.INCOMING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, incident.StartNodeExpr, state);
					sb.Append(";\n");
					if(!state.EmitProfilingInstrumentation())
					{
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
								+ ".GetCompatibleIncoming(");
						GenExpression(sb, incident.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
					}
					else
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id + ".Incoming)\n");
					sb.AppendFront("{\n");
					sb.Indent();

					if(state.EmitProfilingInstrumentation())
					{
						if(state.IsToBeParallelizedActionExisting())
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
						else
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
						sb.AppendFront("if(!edge_" + id + ".InstanceOf(");
						GenExpression(sb, incident.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
						sb.AppendFrontIndented("continue;\n");
					}

					sb.AppendFront("if(!edge_" + id + ".Source.InstanceOf(");
					GenExpression(sb, incident.AdjacentNodeTypeExpr, state);
					sb.Append("))\n");
					sb.AppendFrontIndented("continue;\n");
					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
				else if(incident.Direction() == Direction.OUTGOING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, incident.StartNodeExpr, state);
					sb.Append(";\n");
					if(!state.EmitProfilingInstrumentation())
					{
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id
								+ ".GetCompatibleOutgoing(");
						GenExpression(sb, incident.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
					}
					else
						sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in node_" + id + ".Outgoing)\n");
					sb.AppendFront("{\n");
					sb.Indent();

					if(state.EmitProfilingInstrumentation())
					{
						if(state.IsToBeParallelizedActionExisting())
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
						else
							sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
						sb.AppendFront("if(!edge_" + id + ".InstanceOf(");
						GenExpression(sb, incident.IncidentEdgeTypeExpr, state);
						sb.Append("))\n");
						sb.AppendFrontIndented("continue;\n");
					}

					sb.AppendFront("if(!edge_" + id + ".Target.InstanceOf(");
					GenExpression(sb, incident.AdjacentNodeTypeExpr, state);
					sb.Append("))\n");
					sb.AppendFrontIndented("continue;\n");
					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
			}
			else if(ff.Function is ReachableNodeExpr)
			{
				ReachableNodeExpr reachable = (ReachableNodeExpr)ff.Function;
				if(reachable.Direction() == Direction.INCIDENT)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.INode iter_" + id
							+ " in GRGEN_LIBGR.GraphHelper.Reachable(node_" + id + ",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")iter_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.INCOMING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.INode iter_" + id
							+ " in GRGEN_LIBGR.GraphHelper.ReachableIncoming(node_" + id + ",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")iter_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.OUTGOING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.INode iter_" + id
							+ " in GRGEN_LIBGR.GraphHelper.ReachableOutgoing(node_" + id + ",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")iter_" + id + ";\n");
				}
			}
			else if(ff.Function is ReachableEdgeExpr)
			{
				ReachableEdgeExpr reachable = (ReachableEdgeExpr)ff.Function;
				if(reachable.Direction() == Direction.INCIDENT)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
							+ " in GRGEN_LIBGR.GraphHelper.ReachableEdges(node_" + id + ",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.INCOMING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
							+ " in GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming(node_" + id + ",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.OUTGOING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
							+ " in GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing(node_" + id + ",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
			}
			else if(ff.Function is BoundedReachableNodeExpr)
			{
				BoundedReachableNodeExpr reachable = (BoundedReachableNodeExpr)ff.Function;
				if(reachable.Direction() == Direction.INCIDENT)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.INode iter_" + id
							+ " in GRGEN_LIBGR.GraphHelper.BoundedReachable(node_" + id + ",");
					GenExpression(sb, reachable.DepthExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")iter_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.INCOMING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.INode iter_" + id
							+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableIncoming(node_" + id + ",");
					GenExpression(sb, reachable.DepthExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")iter_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.OUTGOING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.INode iter_" + id
							+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableOutgoing(node_" + id + ",");
					GenExpression(sb, reachable.DepthExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")iter_" + id + ";\n");
				}
			}
			else if(ff.Function is BoundedReachableEdgeExpr)
			{
				BoundedReachableEdgeExpr reachable = (BoundedReachableEdgeExpr)ff.Function;
				if(reachable.Direction() == Direction.INCIDENT)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
							+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableEdges(node_" + id + ",");
					GenExpression(sb, reachable.DepthExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.INCOMING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
							+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesIncoming(node_" + id + ",");
					GenExpression(sb, reachable.DepthExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
				else if(reachable.Direction() == Direction.OUTGOING)
				{
					sb.AppendFront("GRGEN_LIBGR.INode node_" + id + " = ");
					GenExpression(sb, reachable.StartNodeExpr, state);
					sb.Append(";\n");
					sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id
							+ " in GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesOutgoing(node_" + id + ",");
					GenExpression(sb, reachable.DepthExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.IncidentEdgeTypeExpr, state);
					sb.Append(",");
					GenExpression(sb, reachable.AdjacentNodeTypeExpr, state);
					sb.Append(",");
					sb.Append("graph");
					if(state.EmitProfilingInstrumentation())
						sb.Append(", actionEnv");
					if(state.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append("))\n");
					sb.AppendFront("{\n");
					sb.Indent();

					sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
							+ FormatEntity(ff.IterationVar));
					sb.Append(" = (" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
				}
			}
			else if(ff.Function is NodesExpr)
			{
				NodesExpr nodes = (NodesExpr)ff.Function;
				sb.AppendFront("foreach(GRGEN_LIBGR.INode node_" + id + " in graph.GetCompatibleNodes(");
				GenExpression(sb, nodes.NodeTypeExpr, state);
				sb.Append("))\n");
				sb.AppendFront("{\n");
				sb.Indent();

				if(state.EmitProfilingInstrumentation())
				{
					if(state.IsToBeParallelizedActionExisting())
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
				}

				sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
						+ FormatEntity(ff.IterationVar));
				sb.Append(" = " + "(" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")node_" + id + ";\n");
			}
			else if(ff.Function is EdgesExpr)
			{
				EdgesExpr edges = (EdgesExpr)ff.Function;
				sb.AppendFront("foreach(GRGEN_LIBGR.IEdge edge_" + id + " in graph.GetCompatibleEdges(");
				GenExpression(sb, edges.EdgeTypeExpr, state);
				sb.Append("))\n");
				sb.AppendFront("{\n");
				sb.Indent();

				if(state.EmitProfilingInstrumentation())
				{
					if(state.IsToBeParallelizedActionExisting())
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
				}

				sb.AppendFront(FormatElementInterfaceRef(ff.IterationVar.Type) + " "
						+ FormatEntity(ff.IterationVar));
				sb.Append(" = " + "(" + FormatElementInterfaceRef(ff.IterationVar.Type) + ")edge_" + id + ";\n");
			}
			else if(ff.Function is NodesFromIndexAccessSameExpr || ff.Function is EdgesFromIndexAccessSameExpr)
			{
				IndexAccessEquality iae;
				if(ff.Function is NodesFromIndexAccessSameExpr)
					iae = ((NodesFromIndexAccessSameExpr)ff.Function).IndexAccessEquality;
				else
					iae = ((EdgesFromIndexAccessSameExpr)ff.Function).IndexAccessEquality;

				sb.AppendFront("foreach( " + FormatElementInterfaceRef(ff.IterationVar.Type) +
						" " + FormatEntity(ff.IterationVar) + " in ((" +
						"GRGEN_MODEL." + model.Ident + "IndexSet" + ")graph.Indices)." + iae.index.Ident +
						".Lookup(");
				GenExpression(sb, iae.expr, state);
				sb.Append(") )");
				sb.AppendFront("{\n");
				sb.Indent();

				if(state.EmitProfilingInstrumentation())
				{
					if(state.IsToBeParallelizedActionExisting())
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
				}
			}
			else if(ff.Function is NodesFromIndexAccessFromToExpr || ff.Function is EdgesFromIndexAccessFromToExpr)
			{
				IndexAccessOrdering iao;
				if(ff.Function is NodesFromIndexAccessFromToExpr)
					iao = ((NodesFromIndexAccessFromToExpr)ff.Function).IndexAccessOrdering;
				else
					iao = ((EdgesFromIndexAccessFromToExpr)ff.Function).IndexAccessOrdering;

				sb.AppendFront("foreach( " + FormatElementInterfaceRef(ff.IterationVar.Type) +
						" " + FormatEntity(ff.IterationVar) + " in ((" +
						"GRGEN_MODEL." + model.Ident + "IndexSet" + ")graph.Indices)." + iao.index.Ident +
						".Lookup");
				if(iao.ascending)
					sb.Append("Ascending");
				else
					sb.Append("Descending");
				if(iao.From() != null && iao.To() != null)
				{
					sb.Append("From");
					if(iao.IncludingFrom())
						sb.Append("Inclusive");
					else
						sb.Append("Exclusive");
					sb.Append("To");
					if(iao.IncludingTo())
						sb.Append("Inclusive");
					else
						sb.Append("Exclusive");
					sb.Append("(");
					GenExpression(sb, iao.From(), state);
					sb.Append(", ");
					GenExpression(sb, iao.To(), state);
				}
				else if(iao.From() != null)
				{
					sb.Append("From");
					if(iao.IncludingFrom())
						sb.Append("Inclusive");
					else
						sb.Append("Exclusive");
					sb.Append("(");
					GenExpression(sb, iao.From(), state);
				}
				else if(iao.To() != null)
				{
					sb.Append("To");
					if(iao.IncludingTo())
						sb.Append("Inclusive");
					else
						sb.Append("Exclusive");
					sb.Append("(");
					GenExpression(sb, iao.To(), state);
				}
				else
					sb.Append("(");
				sb.Append(") )\n");
				sb.AppendFront("{\n");
				sb.Indent();

				if(state.EmitProfilingInstrumentation())
				{
					if(state.IsToBeParallelizedActionExisting())
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
					else
						sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
				}
			}
			else if(ff.Function is NodesFromIndexAccessMultipleFromToExpr || ff.Function is EdgesFromIndexAccessMultipleFromToExpr)
			{
				sb.AppendFront("foreach( " + FormatElementInterfaceRef(ff.IterationVar.Type)
						+ " " + FormatEntity(ff.IterationVar) + " in ");

				IList<IndexAccessOrdering> iaos;
				if(ff.Function is NodesFromIndexAccessMultipleFromToExpr)
				{
					NodesFromIndexAccessMultipleFromToExpr nfiamft = (NodesFromIndexAccessMultipleFromToExpr)ff.Function;
					sb.Append("GRGEN_LIBGR.IndexHelper.NodesFromIndexMultipleFromTo(");
					iaos = nfiamft.IndexAccesses;
				}
				else
				{
					EdgesFromIndexAccessMultipleFromToExpr efiamft = (EdgesFromIndexAccessMultipleFromToExpr)ff.Function;
					sb.Append("GRGEN_LIBGR.IndexHelper.EdgesFromIndexMultipleFromTo(");
					iaos = efiamft.IndexAccesses;
				}

				if(state.EmitProfilingInstrumentation())
					sb.Append("actionEnv, ");
				if(state.IsToBeParallelizedActionExisting())
					sb.Append("threadId, ");
				bool first = true;
				foreach(IndexAccessOrdering iao in iaos)
				{
					if(first)
						first = false;
					else
						sb.Append(",");
					sb.Append("new GRGEN_LIBGR.IndexHelper.IndexAccess(");
					GenIndexAccessOrdering(sb, iao, state);
					sb.Append(")");
				}
				sb.Append(").Keys)\n");
				sb.AppendFront("{\n");
				sb.Indent();
			}

			GenEvals(sb, state, ff.LoopedStatements);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenForIndexAccessEquality(SourceBuilder sb, ModifyGenerationStateConst state,
				ForIndexAccessEquality fiae)
		{
			IndexAccessEquality iae = fiae.IndexAcccessEquality;

			sb.AppendFront("foreach( " + FormatElementInterfaceRef(fiae.IterationVar.Type) +
					" " + FormatEntity(fiae.IterationVar) + " in ((" +
					"GRGEN_MODEL." + model.Ident + "IndexSet" + ")graph.Indices)." + iae.index.Ident +
					".Lookup(");
			GenExpression(sb, iae.expr, state);
			sb.Append(") )");
			sb.AppendFront("{\n");
			sb.Indent();

			if(state.EmitProfilingInstrumentation())
			{
				if(state.IsToBeParallelizedActionExisting())
					sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
				else
					sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
			}

			GenEvals(sb, state, fiae.LoopedStatements);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenForIndexAccessOrdering(SourceBuilder sb, ModifyGenerationStateConst state,
				ForIndexAccessOrdering fiao)
		{
			IndexAccessOrdering iao = fiao.IndexAccessOrdering;

			sb.AppendFront("foreach( " + FormatElementInterfaceRef(fiao.IterationVar.Type) +
					" " + FormatEntity(fiao.IterationVar) + " in ((" +
					"GRGEN_MODEL." + model.Ident + "IndexSet" + ")graph.Indices)." + iao.index.Ident +
					".Lookup");
			if(iao.ascending)
				sb.Append("Ascending");
			else
				sb.Append("Descending");
			if(iao.From() != null && iao.To() != null)
			{
				sb.Append("From");
				if(iao.IncludingFrom())
					sb.Append("Inclusive");
				else
					sb.Append("Exclusive");
				sb.Append("To");
				if(iao.IncludingTo())
					sb.Append("Inclusive");
				else
					sb.Append("Exclusive");
				sb.Append("(");
				GenExpression(sb, iao.From(), state);
				sb.Append(", ");
				GenExpression(sb, iao.To(), state);
			}
			else if(iao.From() != null)
			{
				sb.Append("From");
				if(iao.IncludingFrom())
					sb.Append("Inclusive");
				else
					sb.Append("Exclusive");
				sb.Append("(");
				GenExpression(sb, iao.From(), state);
			}
			else if(iao.To() != null)
			{
				sb.Append("To");
				if(iao.IncludingTo())
					sb.Append("Inclusive");
				else
					sb.Append("Exclusive");
				sb.Append("(");
				GenExpression(sb, iao.To(), state);
			}
			else
				sb.Append("(");
			sb.Append(") )\n");
			sb.AppendFront("{\n");
			sb.Indent();

			if(state.EmitProfilingInstrumentation())
			{
				if(state.IsToBeParallelizedActionExisting())
					sb.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
				else
					sb.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
			}

			GenEvals(sb, state, fiao.LoopedStatements);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private static void GenBreakStatement(SourceBuilder sb, ModifyGenerationStateConst state, BreakStatement bs)
		{
			sb.AppendFront("break;\n");
		}

		private static void GenContinueStatement(SourceBuilder sb, ModifyGenerationStateConst state, ContinueStatement cs)
		{
			sb.AppendFront("continue;\n");
		}

		private void GenReturnAssignment(SourceBuilder sb, ModifyGenerationStateConst state, ReturnAssignment ra)
		{
			// declare temporary out variables
			ProcedureOrBuiltinProcedureInvocationBase procedure = ra.ProcedureInvocation;
			ICollection<AssignmentBase> targets = ra.Targets;
			IList<string> outParams = new List<string>();
			for(int i = 0; i < procedure.ReturnArity(); ++i)
			{
				string outParam = "outvar_" + tmpVarID;
				outParams.Add(outParam);
				++tmpVarID;
				sb.AppendFront(FormatType(procedure.GetReturnType(i)) + " " + outParam + ";\n");
			}
			int outParamNumber = 0;
			foreach(AssignmentBase assignment in targets)
			{
				ProjectionExpr proj;
				if(assignment.Expression is ProjectionExpr)
					proj = (ProjectionExpr)assignment.Expression;
				else
				{
					Cast cast = (Cast)assignment.Expression;
					proj = (ProjectionExpr)cast.Expression;
				}
				proj.ProjectedValueVarName = outParams[outParamNumber];
				++outParamNumber;
			}

			// do the call, with out variables, depending on the type of procedure
			if(ra.ProcedureInvocation is ProcedureInvocation
					|| ra.ProcedureInvocation is ExternalProcedureInvocation)
			{
				GenReturnAssignmentProcedureOrExternalProcedureInvocation(sb, state,
						(ProcedureInvocationBase)ra.ProcedureInvocation, outParams);
			}
			else if(ra.ProcedureInvocation is ProcedureMethodInvocation
					|| ra.ProcedureInvocation is ExternalProcedureMethodInvocation)
			{
				GenReturnAssignmentProcedureMethodOrExternalProcedureMethodInvocation(sb, state,
						(ProcedureInvocationBase)ra.ProcedureInvocation, outParams);
			}
			else
			{
				GenReturnAssignmentBuiltinProcedureOrMethodInvocation(sb, state,
						(BuiltinProcedureInvocationBase)ra.ProcedureInvocation, outParams);
			}

			// assign out variables to the real targets
			foreach(AssignmentBase assignment in ra.Targets)
				GenEvalStmt(sb, state, assignment);
		}

		private void GenReturnAssignmentProcedureOrExternalProcedureInvocation(SourceBuilder sb,
				ModifyGenerationStateConst state, ProcedureInvocationBase procedure, IList<string> outParams)
		{
			// call the procedure with out variables  
			if(procedure is ProcedureInvocation)
			{
				ProcedureInvocation call = (ProcedureInvocation)procedure;
				sb.AppendFront("GRGEN_ACTIONS." + GetPackagePrefixDot(call.Procedure) + "Procedures."
						+ call.Procedure.Ident.ToString() + "(actionEnv, graph");
			}
			else
			{
				ExternalProcedureInvocation call = (ExternalProcedureInvocation)procedure;
				sb.AppendFront("GRGEN_EXPR.ExternalProcedures." + call.ExternalProc.Ident.ToString()
						+ "(actionEnv, graph");
			}
			for(int i = 0; i < procedure.Arity(); ++i)
			{
				sb.Append(", ");
				Expression argument = procedure.GetArgument(i);
				if(argument.Type is InheritanceType)
					sb.Append("(" + FormatElementInterfaceRef(argument.Type) + ")");
				GenExpression(sb, argument, state);
			}
			for(int i = 0; i < procedure.ReturnArity(); ++i)
				sb.Append(", out " + outParams[i]);
			sb.Append(");\n");
		}

		private void GenReturnAssignmentProcedureMethodOrExternalProcedureMethodInvocation(SourceBuilder sb,
				ModifyGenerationStateConst state, ProcedureInvocationBase procedure, IList<string> outParams)
		{
			// call the procedure method with out variables  
			if(procedure is ProcedureMethodInvocation)
			{
				ProcedureMethodInvocation call = (ProcedureMethodInvocation)procedure;
				Entity owner = call.Owner;
				sb.AppendFront("((" + FormatElementInterfaceRef(owner.Type) + ") ");
				sb.Append(FormatEntity(owner) + ").@");
				sb.Append(call.Procedure.Ident.ToString() + "(actionEnv, graph");
			}
			else
			{
				ExternalProcedureMethodInvocation call = (ExternalProcedureMethodInvocation)procedure;
				// the graph element is handed in to the external type method if it was called on a graph element attribute, to allow for transaction manager undo item registration
				if(call.OwnerQual != null)
					GenQualAccess(sb, call.OwnerQual, state);
				else
					GenVar(sb, call.OwnerVar, state);
				sb.Append(".@");
				sb.Append(call.ExternalProc.Ident.ToString() + "(actionEnv, graph, ");
				if(call.OwnerQual != null)
					sb.Append(FormatEntity(call.OwnerQual.Owner));
				else
					sb.Append("null");
			}
			for(int i = 0; i < procedure.Arity(); ++i)
			{
				sb.Append(", ");
				Expression argument = procedure.GetArgument(i);
				if(argument.Type is InheritanceType)
					sb.Append("(" + FormatElementInterfaceRef(argument.Type) + ")");
				GenExpression(sb, argument, state);
			}
			for(int i = 0; i < procedure.ReturnArity(); ++i)
				sb.Append(", out " + outParams[i]);
			sb.Append(");\n");
		}

		private void GenReturnAssignmentBuiltinProcedureOrMethodInvocation(SourceBuilder sb,
				ModifyGenerationStateConst state, BuiltinProcedureInvocationBase procedure, IList<string> outParams)
		{
			// call the procedure or procedure method, either without return value, or with one return value, more not supported as of now
			if(outParams.Count == 0)
				GenEvalComp(sb, state, procedure);
			else
			{
				Debug.Assert((outParams.Count == 1));
				sb.AppendFront(outParams[0] + " = ");
				GenEvalComp(sb, state, procedure);
				sb.Append(";\n");
			}
		}

		private void GenFunctionAutoKeepOneForEachAccumulateBy(SourceBuilder sb,
				ModifyGenerationStateConst state, FunctionAutoKeepOneForEachAccumulateBy functionAuto)
		{
			ArrayType arrayOfMatch = functionAuto.TargetType;
			Type matchType = arrayOfMatch.ElementType;
			string matchInterfaceName = FormatType(matchType);
			Variable inputVariable = functionAuto.TargetVar;
			string inputVar = FormatEntity(inputVariable);
			Entity member = functionAuto.Member;
			string matchEntity = FormatEntity(member);
			string typeOfEntity = FormatType(member.Type);
			Variable accumulationMember = functionAuto.AccumulationMember;
			string accumulationVariable = FormatEntity(accumulationMember);
			string typeOfAccumulationVariable = FormatType(accumulationMember.Type);
			string accumulationMethod = GetArrayAccumulationMethodImplementation(functionAuto.AccumulationMethod);

			sb.AppendFront("List<" + matchInterfaceName + "> newList = new List<" + matchInterfaceName + ">();\n");
			sb.AppendFront("Dictionary<" + typeOfEntity + ", List<" + typeOfAccumulationVariable + ">> seenValues"
					+ " = new Dictionary<" + typeOfEntity + ", List<" + typeOfAccumulationVariable + ">>();\n");
			sb.AppendFront("for(int pos = 0; pos < " + inputVar + ".Count; ++pos)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("if(seenValues.ContainsKey(" + inputVar + "[pos].@" + matchEntity + "))\n");
			sb.AppendFrontIndented("seenValues[" + inputVar + "[pos].@" + matchEntity +
					"].Add(" + inputVar + "[pos].@" + accumulationVariable + ");\n");
			sb.AppendFront("else {\n");
			sb.Indent();
			sb.AppendFront("List<" + typeOfAccumulationVariable + "> tempList"
					+ " = new List<" + typeOfAccumulationVariable + ">();\n");
			sb.AppendFront("tempList.Add(" + inputVar + "[pos].@" + accumulationVariable + ");\n");
			sb.AppendFront("seenValues.Add(" + inputVar + "[pos].@" + matchEntity + ", tempList);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.AppendFront("for(int pos = 0; pos < " + inputVar + ".Count; ++pos)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("if(seenValues.ContainsKey(" + inputVar + "[pos].@" + matchEntity + "))");
			sb.Append(" {\n");
			sb.Indent();
			sb.AppendFront(inputVar + "[pos].@" + accumulationVariable
					+ " = " + accumulationMethod + "(seenValues[" + inputVar + "[pos].@" + matchEntity + "]);\n");
			sb.AppendFront("seenValues.Remove(" + inputVar + "[pos].@" + matchEntity + ");\n");
			sb.AppendFront("newList.Add(" + inputVar + "[pos]);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("return newList;\n");
		}

		private static string GetArrayAccumulationMethodImplementation(string arrayAccumulationMethod)
		{
			switch(arrayAccumulationMethod)
			{
			case "sum":
				return "GRGEN_LIBGR.ContainerHelper.Sum";
			case "prod":
				return "GRGEN_LIBGR.ContainerHelper.Prod";
			case "min":
				return "GRGEN_LIBGR.ContainerHelper.Min";
			case "max":
				return "GRGEN_LIBGR.ContainerHelper.Max";
			case "avg":
				return "GRGEN_LIBGR.ContainerHelper.Avg";
			case "med":
				return "GRGEN_LIBGR.ContainerHelper.Med";
			case "medUnordered":
				return "GRGEN_LIBGR.ContainerHelper.MedUnordered";
			case "var":
				return "GRGEN_LIBGR.ContainerHelper.Var";
			case "dev":
				return "GRGEN_LIBGR.ContainerHelper.Dev";
			default:
				return "INTERNAL ERROR";
			}
		}

		private void GenLockStatement(SourceBuilder sb, ModifyGenerationStateConst state, LockStatement ls)
		{
			sb.AppendFront("lock(");
			GenExpression(sb, ls.LockObjectExpr, state);
			sb.Append(") {\n");
			sb.Indent();
			GenEvals(sb, state, ls.Statements);
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		///////////////////////////////
		// Procedure call generation //
		///////////////////////////////

		public virtual void GenEvalComp(SourceBuilder sb, ModifyGenerationStateConst state, ProcedureOrBuiltinProcedureInvocationBase evalProc)
		{
			if(evalProc is EmitProc)
				GenEmitProc(sb, state, (EmitProc)evalProc);
			else if(evalProc is DebugAddProc)
				GenDebugAddProc(sb, state, (DebugAddProc)evalProc);
			else if(evalProc is DebugRemProc)
				GenDebugRemProc(sb, state, (DebugRemProc)evalProc);
			else if(evalProc is DebugEmitProc)
				GenDebugEmitProc(sb, state, (DebugEmitProc)evalProc);
			else if(evalProc is DebugHaltProc)
				GenDebugHaltProc(sb, state, (DebugHaltProc)evalProc);
			else if(evalProc is DebugHighlightProc)
				GenDebugHighlightProc(sb, state, (DebugHighlightProc)evalProc);
			else if(evalProc is AssertProc)
				GenAssertProc(sb, state, (AssertProc)evalProc);
			else if(evalProc is RecordProc)
				GenRecordProc(sb, state, (RecordProc)evalProc);
			else if(evalProc is ExportProc)
				GenExportProc(sb, state, (ExportProc)evalProc);
			else if(evalProc is DeleteFileProc)
				GenDeleteFileProc(sb, state, (DeleteFileProc)evalProc);
			else if(evalProc is GraphAddNodeProc)
				GenGraphAddNodeProc(sb, state, (GraphAddNodeProc)evalProc);
			else if(evalProc is GraphAddEdgeProc)
				GenGraphAddEdgeProc(sb, state, (GraphAddEdgeProc)evalProc);
			else if(evalProc is GraphRetypeNodeProc)
				GenGraphRetypeNodeProc(sb, state, (GraphRetypeNodeProc)evalProc);
			else if(evalProc is GraphRetypeEdgeProc)
				GenGraphRetypeEdgeProc(sb, state, (GraphRetypeEdgeProc)evalProc);
			else if(evalProc is GraphClearProc)
				GenGraphClearProc(sb, state, (GraphClearProc)evalProc);
			else if(evalProc is GraphRemoveProc)
				GenGraphRemoveProc(sb, state, (GraphRemoveProc)evalProc);
			else if(evalProc is GraphAddCopyNodeProc)
				GenGraphAddCopyNodeProc(sb, state, (GraphAddCopyNodeProc)evalProc);
			else if(evalProc is GraphAddCopyEdgeProc)
				GenGraphAddCopyEdgeProc(sb, state, (GraphAddCopyEdgeProc)evalProc);
			else if(evalProc is GraphMergeProc)
				GenGraphMergeProc(sb, state, (GraphMergeProc)evalProc);
			else if(evalProc is GraphRedirectSourceProc)
				GenGraphRedirectSourceProc(sb, state, (GraphRedirectSourceProc)evalProc);
			else if(evalProc is GraphRedirectTargetProc)
				GenGraphRedirectTargetProc(sb, state, (GraphRedirectTargetProc)evalProc);
			else if(evalProc is GraphRedirectSourceAndTargetProc)
				GenGraphRedirectSourceAndTargetProc(sb, state, (GraphRedirectSourceAndTargetProc)evalProc);
			else if(evalProc is InsertProc)
				GenInsertProc(sb, state, (InsertProc)evalProc);
			else if(evalProc is InsertCopyProc)
				GenInsertCopyProc(sb, state, (InsertCopyProc)evalProc);
			else if(evalProc is InsertInducedSubgraphProc)
				GenInsertInducedSubgraphProc(sb, state, (InsertInducedSubgraphProc)evalProc);
			else if(evalProc is InsertDefinedSubgraphProc)
				GenInsertDefinedSubgraphProc(sb, state, (InsertDefinedSubgraphProc)evalProc);
			else if(evalProc is VAllocProc)
				GenVAllocProc(sb, state, (VAllocProc)evalProc);
			else if(evalProc is VFreeProc)
				GenVFreeProc(sb, state, (VFreeProc)evalProc);
			else if(evalProc is VFreeNonResetProc)
				GenVFreeNonResetProc(sb, state, (VFreeNonResetProc)evalProc);
			else if(evalProc is VResetProc)
				GenVResetProc(sb, state, (VResetProc)evalProc);
			else if(evalProc is StartTransactionProc)
				GenStartTransactionProc(sb, state, (StartTransactionProc)evalProc);
			else if(evalProc is PauseTransactionProc)
				GenPauseTransactionProc(sb, state, (PauseTransactionProc)evalProc);
			else if(evalProc is ResumeTransactionProc)
				GenResumeTransactionProc(sb, state, (ResumeTransactionProc)evalProc);
			else if(evalProc is CommitTransactionProc)
				GenCommitTransactionProc(sb, state, (CommitTransactionProc)evalProc);
			else if(evalProc is RollbackTransactionProc)
				GenRollbackTransactionProc(sb, state, (RollbackTransactionProc)evalProc);
			else if(evalProc is MapRemoveItem)
				GenMapRemoveItem(sb, state, (MapRemoveItem)evalProc);
			else if(evalProc is MapClear)
				GenMapClear(sb, state, (MapClear)evalProc);
			else if(evalProc is MapAddItem)
				GenMapAddItem(sb, state, (MapAddItem)evalProc);
			else if(evalProc is SetRemoveItem)
				GenSetRemoveItem(sb, state, (SetRemoveItem)evalProc);
			else if(evalProc is SetClear)
				GenSetClear(sb, state, (SetClear)evalProc);
			else if(evalProc is SetAddItem)
				GenSetAddItem(sb, state, (SetAddItem)evalProc);
			else if(evalProc is ArrayRemoveItem)
				GenArrayRemoveItem(sb, state, (ArrayRemoveItem)evalProc);
			else if(evalProc is ArrayClear)
				GenArrayClear(sb, state, (ArrayClear)evalProc);
			else if(evalProc is ArrayAddItem)
				GenArrayAddItem(sb, state, (ArrayAddItem)evalProc);
			else if(evalProc is ArrayVarAddAll)
				GenArrayVarAddAll(sb, state, (ArrayVarAddAll)evalProc);
			else if(evalProc is DequeRemoveItem)
				GenDequeRemoveItem(sb, state, (DequeRemoveItem)evalProc);
			else if(evalProc is DequeClear)
				GenDequeClear(sb, state, (DequeClear)evalProc);
			else if(evalProc is DequeAddItem)
				GenDequeAddItem(sb, state, (DequeAddItem)evalProc);
			else if(evalProc is MapVarRemoveItem)
				GenMapVarRemoveItem(sb, state, (MapVarRemoveItem)evalProc);
			else if(evalProc is MapVarClear)
				GenMapVarClear(sb, state, (MapVarClear)evalProc);
			else if(evalProc is MapVarAddItem)
				GenMapVarAddItem(sb, state, (MapVarAddItem)evalProc);
			else if(evalProc is SetVarRemoveItem)
				GenSetVarRemoveItem(sb, state, (SetVarRemoveItem)evalProc);
			else if(evalProc is SetVarClear)
				GenSetVarClear(sb, state, (SetVarClear)evalProc);
			else if(evalProc is SetVarAddItem)
				GenSetVarAddItem(sb, state, (SetVarAddItem)evalProc);
			else if(evalProc is SetVarAddAll)
				GenSetVarAddAll(sb, state, (SetVarAddAll)evalProc);
			else if(evalProc is ArrayVarRemoveItem)
				GenArrayVarRemoveItem(sb, state, (ArrayVarRemoveItem)evalProc);
			else if(evalProc is ArrayVarClear)
				GenArrayVarClear(sb, state, (ArrayVarClear)evalProc);
			else if(evalProc is ArrayVarAddItem)
				GenArrayVarAddItem(sb, state, (ArrayVarAddItem)evalProc);
			else if(evalProc is DequeVarRemoveItem)
				GenDequeVarRemoveItem(sb, state, (DequeVarRemoveItem)evalProc);
			else if(evalProc is DequeVarClear)
				GenDequeVarClear(sb, state, (DequeVarClear)evalProc);
			else if(evalProc is DequeVarAddItem)
				GenDequeVarAddItem(sb, state, (DequeVarAddItem)evalProc);
			else if(evalProc is SynchronizationEnterProc)
				GenSynchronizationEnterProc(sb, state, (SynchronizationEnterProc)evalProc);
			else if(evalProc is SynchronizationTryEnterProc)
				GenSynchronizationTryEnterProc(sb, state, (SynchronizationTryEnterProc)evalProc);
			else if(evalProc is SynchronizationExitProc)
				GenSynchronizationExitProc(sb, state, (SynchronizationExitProc)evalProc);
			else if(evalProc is GetEquivalentOrAddProc)
				GenGetEquivalentOrAddProc(sb, state, (GetEquivalentOrAddProc)evalProc);
			else
				throw new System.NotSupportedException("Unexpected eval procedure \"" + evalProc + "\"");
		}

		private void GenEmitProc(SourceBuilder sb, ModifyGenerationStateConst state, EmitProc ep)
		{
			string emitVar = "emit_value_" + tmpVarID++;
			string emitWriter = ep.IsDebug() ? "EmitWriterDebug" : "EmitWriter";
			sb.AppendFront("object " + emitVar + ";\n");
			foreach(Expression expr in ep.Expressions)
			{
				sb.AppendFront(emitVar + " = ");
				GenExpression(sb, expr, state);
				sb.Append(";\n");
				sb.AppendFront("if(" + emitVar + " != null)\n");
				sb.AppendFrontIndented("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv)." + emitWriter + ".Write("
						+ "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + emitVar + ", graph, null, null, null));\n");
			}
		}

		private void GenDebugAddProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugAddProc dap)
		{
			if(!be.sys.MayFireDebugEvents())
				return;

			sb.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering((string)");
			GenExpression(sb, dap.FirstExpression, state);
			bool first = true;
			foreach(Expression expr in dap.Expressions)
			{
				if(!first)
				{
					sb.Append(",");
					GenExpression(sb, expr, state);
				}
				first = false;
			}
			sb.Append(");\n");
		}

		private void GenDebugRemProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugRemProc drp)
		{
			if(!be.sys.MayFireDebugEvents())
				return;

			sb.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting((string)");
			GenExpression(sb, drp.FirstExpression, state);
			bool first = true;
			foreach(Expression expr in drp.Expressions)
			{
				if(!first)
				{
					sb.Append(",");
					GenExpression(sb, expr, state);
				}
				first = false;
			}
			sb.Append(");\n");
		}

		private void GenDebugEmitProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugEmitProc dep)
		{
			if(!be.sys.MayFireDebugEvents())
				return;

			sb.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEmitting((string)");
			GenExpression(sb, dep.FirstExpression, state);
			bool first = true;
			foreach(Expression expr in dep.Expressions)
			{
				if(!first)
				{
					sb.Append(",");
					GenExpression(sb, expr, state);
				}
				first = false;
			}
			sb.Append(");\n");
		}

		private void GenDebugHaltProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugHaltProc dhp)
		{
			if(!be.sys.MayFireDebugEvents())
				return;

			sb.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugHalting((string)");
			GenExpression(sb, dhp.FirstExpression, state);
			bool first = true;
			foreach(Expression expr in dhp.Expressions)
			{
				if(!first)
				{
					sb.Append(",");
					GenExpression(sb, expr, state);
				}
				first = false;
			}
			sb.Append(");\n");
		}

		private void GenDebugHighlightProc(SourceBuilder sb, ModifyGenerationStateConst state, DebugHighlightProc dhp)
		{
			if(!be.sys.MayFireDebugEvents())
				return;

			string highlightValuesArray = "highlight_values_" + tmpVarID++;
			sb.AppendFront("List<object> " + highlightValuesArray + " = new List<object>();\n");
			string highlightSourceNamesArray = "highlight_source_names_" + tmpVarID++;
			sb.AppendFront("List<string> " + highlightSourceNamesArray + " = new List<string>();\n");
			int parameterNum = 0;
			foreach(Expression expr in dhp.Expressions)
			{
				if(parameterNum == 0)
				{
					++parameterNum;
					continue;
				}
				if(parameterNum % 2 == 1)
				{
					sb.AppendFront(highlightValuesArray + ".Add(");
					GenExpression(sb, expr, state);
					sb.Append(");\n");
				}
				else
				{
					sb.AppendFront(highlightSourceNamesArray + ".Add((string)");
					GenExpression(sb, expr, state);
					sb.Append(");\n");
				}
				++parameterNum;
			}
			sb.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugHighlighting((string)");
			GenExpression(sb, dhp.FirstExpression, state);
			sb.Append("," + highlightValuesArray + ", " + highlightSourceNamesArray + ");\n");
		}

		private void GenAssertProc(SourceBuilder sb, ModifyGenerationStateConst state, AssertProc ap)
		{
			sb.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).UserProxy.HandleAssert(");
			sb.Append(ap.IsAlways() ? "true" : "false");
			foreach(Expression expr in ap.Expressions)
			{
				sb.Append(", () => ");
				GenExpression(sb, expr, state);
			}
			sb.Append(");\n");
		}

		private void GenRecordProc(SourceBuilder sb, ModifyGenerationStateConst state, RecordProc rp)
		{
			string recordVar = "record_value_" + tmpVarID++;
			sb.AppendFront("object " + recordVar + " = ");
			GenExpression(sb, rp.ToRecordExpr, state);
			sb.Append(";\n");
			sb.AppendFront("if(" + recordVar + " != null)\n");
			sb.AppendFrontIndented("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).Recorder.Write("
					+ "GRGEN_LIBGR.EmitHelper.ToStringNonNull(" + recordVar + ", graph, null, null, null));\n");
		}

		private void GenExportProc(SourceBuilder sb, ModifyGenerationStateConst state, ExportProc ep)
		{
			if(ep.GraphExpr != null)
			{
				sb.AppendFront("GRGEN_LIBGR.GraphHelper.Export(");
				GenExpression(sb, ep.PathExpr, state);
				sb.Append(", ");
				GenExpression(sb, ep.GraphExpr, state);
				sb.Append(");\n");
			}
			else
			{
				sb.AppendFront("GRGEN_LIBGR.GraphHelper.Export(");
				GenExpression(sb, ep.PathExpr, state);
				sb.Append(", graph);\n");
			}
		}

		private void GenDeleteFileProc(SourceBuilder sb, ModifyGenerationStateConst state, DeleteFileProc dfp)
		{
			sb.AppendFront("System.IO.File.Delete(");
			GenExpression(sb, dfp.PathExpr, state);
			sb.Append(");\n");
		}

		private void GenGraphAddNodeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddNodeProc ganp)
		{
			Constant constant = (Constant)ganp.NodeTypeExpr;
			sb.Append("(" + FormatType((Type)constant.Value) + ")"
					+ "GRGEN_LIBGR.GraphHelper.AddNodeOfType(");
			GenExpression(sb, ganp.NodeTypeExpr, state);
			sb.Append(", graph)");
		}

		private void GenGraphAddEdgeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddEdgeProc gaep)
		{
			Constant constant = (Constant)gaep.EdgeTypeExpr;
			sb.Append("(" + FormatType((Type)constant.Value) + ")"
					+ "GRGEN_LIBGR.GraphHelper.AddEdgeOfType(");
			GenExpression(sb, gaep.EdgeTypeExpr, state);
			sb.Append(", ");
			GenExpression(sb, gaep.SourceNodeExpr, state);
			sb.Append(", ");
			GenExpression(sb, gaep.TargetNodeExpr, state);
			sb.Append(", graph)");
		}

		private void GenGraphRetypeNodeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphRetypeNodeProc grnp)
		{
			Constant constant = (Constant)grnp.NewNodeTypeExpr;
			sb.Append("(" + FormatType((Type)constant.Value) + ")"
					+ "graph.Retype(");
			GenExpression(sb, grnp.NodeExpr, state);
			sb.Append(", ");
			GenExpression(sb, grnp.NewNodeTypeExpr, state);
			sb.Append(")");
		}

		private void GenGraphRetypeEdgeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphRetypeEdgeProc grep)
		{
			Constant constant = (Constant)grep.NewEdgeTypeExpr;
			sb.Append("(" + FormatType((Type)constant.Value) + ")"
					+ "graph.Retype(");
			GenExpression(sb, grep.EdgeExpr, state);
			sb.Append(", ");
			GenExpression(sb, grep.NewEdgeTypeExpr, state);
			sb.Append(")");
		}

		private static void GenGraphClearProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphClearProc gcp)
		{
			sb.AppendFront("graph.Clear();\n");
		}

		private void GenGraphRemoveProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphRemoveProc grp)
		{
			if(grp.Entity.Type is NodeType)
			{
				sb.AppendFront("graph.RemoveEdges((GRGEN_LIBGR.INode)");
				GenExpression(sb, grp.Entity, state);
				sb.Append(");\n");

				sb.AppendFront("graph.Remove((GRGEN_LIBGR.INode)");
				GenExpression(sb, grp.Entity, state);
				sb.Append(");\n");
			}
			else
			{
				sb.AppendFront("graph.Remove((GRGEN_LIBGR.IEdge)");
				GenExpression(sb, grp.Entity, state);
				sb.Append(");\n");
			}
		}

		private void GenGraphAddCopyNodeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddCopyNodeProc gacnp)
		{
			sb.Append("(" + FormatType(gacnp.OldNodeExpr.Type) + ")");
			string functionName = gacnp.Deep ? "AddCopyOfNode" : "AddCloneOfNode";
			sb.Append("GRGEN_LIBGR.GraphHelper." + functionName + "(");
			GenExpression(sb, gacnp.OldNodeExpr, state);
			sb.Append(", graph)");
		}

		private void GenGraphAddCopyEdgeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphAddCopyEdgeProc gacep)
		{
			sb.Append("(" + FormatType(gacep.OldEdgeExpr.Type) + ")");
			string functionName = gacep.Deep ? "AddCopyOfEdge" : "AddCloneOfEdge";
			sb.Append("GRGEN_LIBGR.GraphHelper." + functionName + "(");
			GenExpression(sb, gacep.OldEdgeExpr, state);
			sb.Append(", (GRGEN_LIBGR.INode)");
			GenExpression(sb, gacep.SourceNodeExpr, state);
			sb.Append(", (GRGEN_LIBGR.INode)");
			GenExpression(sb, gacep.TargetNodeExpr, state);
			sb.Append(", graph)");
		}

		private void GenGraphMergeProc(SourceBuilder sb, ModifyGenerationStateConst state, GraphMergeProc gmp)
		{
			if(gmp.SourceName != null)
			{
				sb.AppendFront("graph.Merge((GRGEN_LIBGR.INode)");
				GenExpression(sb, gmp.Target, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, gmp.Source, state);
				sb.Append(", (String)");
				GenExpression(sb, gmp.SourceName, state);
				sb.Append(");\n");
			}
			else
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).Merge((GRGEN_LIBGR.INode)");
				GenExpression(sb, gmp.Target, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, gmp.Source, state);
				sb.Append(");\n");
			}
		}

		private void GenGraphRedirectSourceProc(SourceBuilder sb, ModifyGenerationStateConst state,
				GraphRedirectSourceProc grsp)
		{
			if(grsp.OldSourceName != null)
			{
				sb.AppendFront("graph.RedirectSource((GRGEN_LIBGR.IEdge)");
				GenExpression(sb, grsp.Edge, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grsp.NewSource, state);
				sb.Append(", (String)");
				GenExpression(sb, grsp.OldSourceName, state);
				sb.Append(");\n");
			}
			else
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectSource((GRGEN_LIBGR.IEdge)");
				GenExpression(sb, grsp.Edge, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grsp.NewSource, state);
				sb.Append(");\n");
			}
		}

		private void GenGraphRedirectTargetProc(SourceBuilder sb, ModifyGenerationStateConst state,
				GraphRedirectTargetProc grtp)
		{
			if(grtp.OldTargetName != null)
			{
				sb.AppendFront("graph.RedirectTarget((GRGEN_LIBGR.IEdge)");
				GenExpression(sb, grtp.Edge, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grtp.NewTarget, state);
				sb.Append(", (String)");
				GenExpression(sb, grtp.OldTargetName, state);
				sb.Append(");\n");
			}
			else
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectTarget((GRGEN_LIBGR.IEdge)");
				GenExpression(sb, grtp.Edge, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grtp.NewTarget, state);
				sb.Append(");\n");
			}
		}

		private void GenGraphRedirectSourceAndTargetProc(SourceBuilder sb, ModifyGenerationStateConst state,
				GraphRedirectSourceAndTargetProc grsatp)
		{
			if(grsatp.OldSourceName != null)
			{
				sb.AppendFront("graph.RedirectSourceAndTarget((GRGEN_LIBGR.IEdge)");
				GenExpression(sb, grsatp.Edge, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grsatp.NewSource, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grsatp.NewTarget, state);
				sb.Append(", (String)");
				GenExpression(sb, grsatp.OldSourceName, state);
				sb.Append(", (String)");
				GenExpression(sb, grsatp.OldTargetName, state);
				sb.Append(");\n");
			}
			else
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPNamedGraph)graph).RedirectSourceAndTarget((GRGEN_LIBGR.IEdge)");
				GenExpression(sb, grsatp.Edge, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grsatp.NewSource, state);
				sb.Append(", (GRGEN_LIBGR.INode)");
				GenExpression(sb, grsatp.NewTarget, state);
				sb.Append(");\n");
			}
		}

		private void GenInsertProc(SourceBuilder sb, ModifyGenerationStateConst state, InsertProc ip)
		{
			sb.AppendFront("GRGEN_LIBGR.GraphHelper.Insert((GRGEN_LIBGR.IGraph)");
			GenExpression(sb, ip.GraphExpr, state);
			sb.Append(", graph);\n");
		}

		private void GenInsertCopyProc(SourceBuilder sb, ModifyGenerationStateConst state, InsertCopyProc icp)
		{
			sb.Append("GRGEN_LIBGR.GraphHelper.InsertCopy((GRGEN_LIBGR.IGraph)");
			GenExpression(sb, icp.GraphExpr, state);
			sb.Append(", (GRGEN_LIBGR.INode)");
			GenExpression(sb, icp.NodeExpr, state);
			sb.Append(", graph)");
		}

		private void GenInsertInducedSubgraphProc(SourceBuilder sb, ModifyGenerationStateConst state,
				InsertInducedSubgraphProc iisp)
		{
			sb.Append("((");
			sb.Append(FormatType(iisp.NodeExpr.Type));
			sb.Append(")GRGEN_LIBGR.GraphHelper.InsertInduced((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)");
			GenExpression(sb, iisp.SetExpr, state);
			sb.Append(", ");
			GenExpression(sb, iisp.NodeExpr, state);
			sb.Append(", graph))");
		}

		private void GenInsertDefinedSubgraphProc(SourceBuilder sb, ModifyGenerationStateConst state,
				InsertDefinedSubgraphProc idsp)
		{
			sb.Append("((");
			sb.Append(FormatType(idsp.EdgeExpr.Type));
			sb.Append(")GRGEN_LIBGR.GraphHelper.InsertDefined");
			switch(GetDirectednessSuffix(idsp.SetExpr.Type))
			{
			case "Directed":
				sb.Append("Directed(");
				sb.Append("(IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)");
				GenExpression(sb, idsp.SetExpr, state);
				sb.Append(",(GRGEN_LIBGR.IDEdge)");
				break;
			case "Undirected":
				sb.Append("Undirected(");
				sb.Append("(IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)");
				GenExpression(sb, idsp.SetExpr, state);
				sb.Append(",(GRGEN_LIBGR.IUEdge)");
				break;
			default:
				sb.Append("(");
				sb.Append("(IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)");
				GenExpression(sb, idsp.SetExpr, state);
				sb.Append(",(GRGEN_LIBGR.IEdge)");
				break;
			}
			GenExpression(sb, idsp.EdgeExpr, state);
			sb.Append(", graph))");
		}

		private static void GenVAllocProc(SourceBuilder sb, ModifyGenerationStateConst state, VAllocProc vap)
		{
			sb.Append("graph.AllocateVisitedFlag()");
		}

		private void GenVFreeProc(SourceBuilder sb, ModifyGenerationStateConst state, VFreeProc vfp)
		{
			sb.AppendFront("graph.FreeVisitedFlag((int)");
			GenExpression(sb, vfp.VisitedFlagExpr, state);
			sb.Append(");\n");
		}

		private void GenVFreeNonResetProc(SourceBuilder sb, ModifyGenerationStateConst state, VFreeNonResetProc vfnrp)
		{
			sb.AppendFront("graph.FreeVisitedFlagNonReset((int)");
			GenExpression(sb, vfnrp.VisitedFlagExpr, state);
			sb.Append(");\n");
		}

		private void GenVResetProc(SourceBuilder sb, ModifyGenerationStateConst state, VResetProc vrp)
		{
			sb.AppendFront("graph.ResetVisitedFlag((int)");
			GenExpression(sb, vrp.VisitedFlagExpr, state);
			sb.Append(");\n");
		}

		private static void GenStartTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, StartTransactionProc stp)
		{
			sb.Append("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Start()");
		}

		private static void GenPauseTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, PauseTransactionProc ptp)
		{
			sb.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Pause();\n");
		}

		private static void GenResumeTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, ResumeTransactionProc rtp)
		{
			sb.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Resume();\n");
		}

		private void GenCommitTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state, CommitTransactionProc ctp)
		{
			sb.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Commit((int)");
			GenExpression(sb, ctp.TransactionId, state);
			sb.Append(");\n");
		}

		private void GenRollbackTransactionProc(SourceBuilder sb, ModifyGenerationStateConst state,
				RollbackTransactionProc rtp)
		{
			sb.AppendFront("((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).TransactionManager.Rollback((int)");
			GenExpression(sb, rtp.TransactionId, state);
			sb.Append(");\n");
		}

		private void GenSynchronizationEnterProc(SourceBuilder sb, ModifyGenerationStateConst state, SynchronizationEnterProc sep)
		{
			sb.Append("Monitor.Enter(");
			GenExpression(sb, sep.CriticalSectionObject, state);
			sb.Append(");\n");
		}

		private void GenSynchronizationTryEnterProc(SourceBuilder sb, ModifyGenerationStateConst state, SynchronizationTryEnterProc sep)
		{
			sb.Append("Monitor.TryEnter(");
			GenExpression(sb, sep.CriticalSectionObject, state);
			sb.Append(")");
		}

		private void GenSynchronizationExitProc(SourceBuilder sb, ModifyGenerationStateConst state, SynchronizationExitProc sep)
		{
			sb.Append("Monitor.Exit(");
			GenExpression(sb, sep.CriticalSectionObject, state);
			sb.Append(");\n");
		}

		private void GenGetEquivalentOrAddProc(SourceBuilder sb, ModifyGenerationStateConst state, GetEquivalentOrAddProc geoa)
		{
			sb.Append("GRGEN_LIBGR.GraphHelper.GetEquivalentOrAdd((GRGEN_LIBGR.IGraph)");
			GenExpression(sb, geoa.SubgraphExpr, state);
			sb.Append(", (IList<GRGEN_LIBGR.IGraph>)");
			GenExpression(sb, geoa.ArrayExpr, state);
			sb.Append(", ");
			sb.Append(geoa.IncludingAttributes ? "true" : "false");
			sb.Append(")");
		}

		//////////////////////

		protected internal virtual void GenChangingAttribute(SourceBuilder sb, ModifyGenerationStateConst state,
				Qualification target, string attributeChangeType, string newValue, string keyValue)
		{
			Entity element = target.Owner;
			Entity attribute = target.Member;
			Type elementType = attribute.Owner;

			string kindStr = null;
			bool isDeletedElem = state.IsDeleted(element);
			if(element is Node)
				kindStr = "Node";
			else if(element is Edge)
				kindStr = "Edge";
			else if(element is Variable)
			{
				Variable var = (Variable)element;
				if(var.Type is NodeType)
					kindStr = "Node";
				else if(var.Type is EdgeType)
					kindStr = "Edge";
				else if(var.Type is InternalObjectType)
					kindStr = "Object";
				else
				{
					Debug.Assert(false, "Entity is neither a node nor an edge nor an object (" + element + ")!");
					return;
				}
			}
			else
			{
				Debug.Assert(false, "Entity is neither a node nor an edge nor an object (" + element + ")!");
				return;
			}

			if(!isDeletedElem && be.sys.MayFireEvents())
			{
				if(!Expression.IsGlobalVariable(element))
				{
					sb.AppendFront("graph.Changing" + kindStr + "Attribute(" +
							FormatEntity(element) + ", " +
							FormatTypeClassRef(elementType) + "." +
							FormatAttributeTypeName(attribute) + ", " +
							"GRGEN_LIBGR.AttributeChangeType." + attributeChangeType + ", " +
							newValue + ", " + keyValue + ");\n");
				}
				else
				{
					sb.AppendFront("graph.Changing" + kindStr + "Attribute(" +
							FormatGlobalVariableRead(element) + ", " +
							FormatTypeClassRef(elementType) + "." +
							FormatAttributeTypeName(attribute) + ", " +
							"GRGEN_LIBGR.AttributeChangeType." + attributeChangeType + ", " +
							newValue + ", " + keyValue + ");\n");
				}
			}
		}

		protected internal virtual void GenChangedAttribute(SourceBuilder sb, ModifyGenerationStateConst state,
				Qualification target)
		{
			Entity element = target.Owner;
			Entity attribute = target.Member;
			Type elementType = attribute.Owner;

			string kindStr = null;
			bool isDeletedElem = state.IsDeleted(element);
			if(element is Node)
				kindStr = "Node";
			else if(element is Edge)
				kindStr = "Edge";
			else if(element is Variable)
			{
				Variable var = (Variable)element;
				if(var.Type is NodeType)
					kindStr = "Node";
				else if(var.Type is EdgeType)
					kindStr = "Edge";
				else if(var.Type is InternalObjectType)
					return;
				else
				{
					Debug.Assert(false, "Entity is neither a node nor an edge nor an object (" + element + ")!");
					return;
				}
			}
			else
			{
				Debug.Assert(false, "Entity is neither a node nor an edge nor an object (" + element + ")!");
				return;
			}

			if(!isDeletedElem && be.sys.MayFireDebugEvents())
			{
				if(!Expression.IsGlobalVariable(element))
				{
					sb.AppendFront("graph.Changed" + kindStr + "Attribute(" +
							FormatEntity(element) + ", " +
							FormatTypeClassRef(elementType) + "." +
							FormatAttributeTypeName(attribute) + ");\n");
				}
				else
				{
					sb.AppendFront("graph.Changed" + kindStr + "Attribute(" +
							FormatGlobalVariableRead(element) + ", " +
							FormatTypeClassRef(elementType) + "." +
							FormatAttributeTypeName(attribute) + ");\n");
				}
			}
		}

		protected internal virtual void GenClearAttribute(SourceBuilder sb, ModifyGenerationStateConst state, Qualification target)
		{
			SourceBuilder sbtmp = new SourceBuilder();
			GenExpression(sbtmp, target, state);
			string targetStr = sbtmp.ToString();

			Entity element = target.Owner;
			Entity attribute = target.Member;
			Type elementType = attribute.Owner;

			string kindStr = null;
			bool isDeletedElem = state.IsDeleted(element);
			if(element is Node)
				kindStr = "Node";
			else if(element is Edge)
				kindStr = "Edge";
			else if(element is Variable)
			{
				Variable var = (Variable)element;
				if(var.Type is NodeType)
					kindStr = "Node";
				else if(var.Type is EdgeType)
					kindStr = "Edge";
				else if(var.Type is InternalObjectType)
					kindStr = "Object";
				else
				{
					Debug.Assert(false, "Entity is neither a node nor an edge nor an object (" + element + ")!");
					return;
				}
			}
			else
			{
				Debug.Assert(false, "Entity is neither a node nor an edge nor an object (" + element + ")!");
				return;
			}

			if(!isDeletedElem && be.sys.MayFireEvents())
			{
				if(attribute.Type is MapType)
				{
					MapType attributeType = (MapType)attribute.Type;
					sb.AppendFront("foreach(KeyValuePair<" + FormatType(attributeType.KeyType) + ","
							+ FormatType(attributeType.ValueType) + "> kvp " +
							"in " + targetStr + ")\n");
					sb.AppendFrontIndented("graph.Changing" + kindStr + "Attribute(" +
							FormatEntity(element) + ", " + 
							FormatTypeClassRef(elementType) + "." + 
							FormatAttributeTypeName(attribute) + ", " + 
							"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
							"null, kvp.Key);\n");
				}
				else if(attribute.Type is SetType)
				{
					SetType attributeType = (SetType)attribute.Type;
					sb.AppendFront("foreach(KeyValuePair<" + FormatType(attributeType.ValueType)
							+ ", GRGEN_LIBGR.SetValueType> kvp " +
							"in " + targetStr + ")\n");
					sb.AppendFrontIndented("graph.Changing" + kindStr + "Attribute(" +
							FormatEntity(element) + ", " + 
							FormatTypeClassRef(elementType) + "." + 
							FormatAttributeTypeName(attribute) + ", " + 
							"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " +
							"kvp.Key, null);\n");
				}
				else if(attribute.Type is ArrayType)
				{
					sb.AppendFront("for(int i = " + targetStr + ".Count - 1; i>=0; --i)\n");
					sb.AppendFrontIndented("graph.Changing" + kindStr + "Attribute(" +
							FormatEntity(element) + ", " + 
							FormatTypeClassRef(elementType) + "." + 
							FormatAttributeTypeName(attribute) + ", " + 
							"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + 
							"null, i);\n");
				}
				else if(attribute.Type is DequeType)
				{
					sb.AppendFront("for(int i = " + targetStr + ".Count - 1; i>=0; --i)\n");
					sb.AppendFrontIndented("graph.Changing" + kindStr + "Attribute(" + 
							FormatEntity(element) + ", " + 
							FormatTypeClassRef(elementType) + "." + 
							FormatAttributeTypeName(attribute) + ", " + 
							"GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + 
							"null, i);\n");
				}
				else
					Debug.Assert((false));
			}
		}

		protected internal virtual void GenClearedAttribute(SourceBuilder sb, ModifyGenerationStateConst state, Qualification target)
		{
			Entity element = target.Owner;
			Entity attribute = target.Member;
			Type elementType = attribute.Owner;

			string kindStr = null;
			bool isDeletedElem = state.IsDeleted(element);
			if(element is Node)
				kindStr = "Node";
			else if(element is Edge)
				kindStr = "Edge";
			else if(element is Variable)
			{
				Variable var = (Variable)element;
				if(var.Type is NodeType)
					kindStr = "Node";
				else if(var.Type is EdgeType)
					kindStr = "Edge";
				else
				{
					Debug.Assert(false, "Entity is neither a node nor an edge (" + element + ")!");
					return;
				}
			}
			else
			{
				Debug.Assert(false, "Entity is neither a node nor an edge (" + element + ")!");
				return;
			}

			if(!isDeletedElem && be.sys.MayFireDebugEvents())
				sb.AppendFront("graph.Changed" + kindStr + "Attribute(" +
						FormatEntity(element) + ", " + 
						FormatTypeClassRef(elementType) + "." + 
						FormatAttributeTypeName(attribute) + ");\n");
		}

		//////////////////////
		// Expression stuff //
		//////////////////////

		protected internal override void GenQualAccess(SourceBuilder sb, Qualification qual, object modifyGenerationState)
		{
			GenQualAccess(sb, qual, (ModifyGenerationStateConst)modifyGenerationState);
		}

		protected internal virtual void GenQualAccess(SourceBuilder sb, Qualification qual, ModifyGenerationStateConst state)
		{
			Entity owner = qual.Owner;
			Entity member = qual.Member;
			if(owner.Type is MatchType || owner.Type is DefinedMatchType)
				sb.Append(FormatEntity(owner) + "." + FormatEntity(member));
			else
				GenQualAccess(sb, state, owner, member);
		}

		protected internal virtual void GenQualAccess(SourceBuilder sb, ModifyGenerationStateConst state, Entity owner, Entity member)
		{
			if(!Expression.IsGlobalVariable(owner))
			{
				if(state == null)
				{
					Debug.Assert(false);
					sb.Append(FormatEntity(owner) + ".@" + FormatIdentifiable(member));
					return;
				}

				if(AccessViaVariable(state, owner, member))
					sb.Append("tempvar_" + FormatEntity(owner) + "_" + FormatIdentifiable(member));
				else
				{
					if(AccessViaInterface(state, owner))
						sb.Append("i");

					sb.Append(FormatEntity(owner) + ".@" + FormatIdentifiable(member));
				}
			}
			else
			{
				sb.Append(FormatGlobalVariableRead(owner));
				sb.Append(".@" + FormatIdentifiable(member));
			}
		}

		protected internal override void GenMemberAccess(SourceBuilder sb, Entity member)
		{
			// needed in implementing methods
			sb.Append("@" + FormatIdentifiable(member));
		}
	}

}
