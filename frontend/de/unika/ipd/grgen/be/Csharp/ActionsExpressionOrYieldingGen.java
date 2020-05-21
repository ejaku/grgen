/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the condition expressions and yield statements for the SearchPlanBackend2 backend.
 * @author Edgar Jakumeit, Moritz Kroll
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.HashMap;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.stmt.AssignmentGraphEntity;
import de.unika.ipd.grgen.ir.stmt.AssignmentIdentical;
import de.unika.ipd.grgen.ir.stmt.AssignmentVar;
import de.unika.ipd.grgen.ir.stmt.AssignmentVarIndexed;
import de.unika.ipd.grgen.ir.stmt.BreakStatement;
import de.unika.ipd.grgen.ir.stmt.CaseStatement;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignment;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVar;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVar;
import de.unika.ipd.grgen.ir.stmt.ConditionStatement;
import de.unika.ipd.grgen.ir.stmt.ContainerAccumulationYield;
import de.unika.ipd.grgen.ir.stmt.ContinueStatement;
import de.unika.ipd.grgen.ir.stmt.DefDeclGraphEntityStatement;
import de.unika.ipd.grgen.ir.stmt.DefDeclVarStatement;
import de.unika.ipd.grgen.ir.stmt.DoWhileStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.IntegerRangeIterationYield;
import de.unika.ipd.grgen.ir.stmt.IteratedAccumulationYield;
import de.unika.ipd.grgen.ir.stmt.MultiStatement;
import de.unika.ipd.grgen.ir.stmt.ReturnAssignment;
import de.unika.ipd.grgen.ir.stmt.SwitchStatement;
import de.unika.ipd.grgen.ir.stmt.WhileStatement;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddItem;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarClear;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarRemoveItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarAddItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarClear;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarRemoveItem;
import de.unika.ipd.grgen.ir.stmt.graph.ForFunction;
import de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessEquality;
import de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessOrdering;
import de.unika.ipd.grgen.ir.stmt.map.MapVarAddItem;
import de.unika.ipd.grgen.ir.stmt.map.MapVarClear;
import de.unika.ipd.grgen.ir.stmt.map.MapVarRemoveItem;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugAddProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugEmitProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHaltProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHighlightProc;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugRemProc;
import de.unika.ipd.grgen.ir.stmt.procenv.EmitProc;
import de.unika.ipd.grgen.ir.stmt.procenv.RecordProc;
import de.unika.ipd.grgen.ir.stmt.set.SetVarAddItem;
import de.unika.ipd.grgen.ir.stmt.set.SetVarClear;
import de.unika.ipd.grgen.ir.stmt.set.SetVarRemoveItem;
import de.unika.ipd.grgen.ir.typedecl.ArrayType;
import de.unika.ipd.grgen.ir.typedecl.DequeType;
import de.unika.ipd.grgen.ir.typedecl.ExternalType;
import de.unika.ipd.grgen.ir.typedecl.IntType;
import de.unika.ipd.grgen.ir.typedecl.MapType;
import de.unika.ipd.grgen.ir.typedecl.ObjectType;
import de.unika.ipd.grgen.ir.typedecl.SetType;
import de.unika.ipd.grgen.ir.typedecl.StringType;
import de.unika.ipd.grgen.util.SourceBuilder;
import de.unika.ipd.grgen.ir.expr.*;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsDequeExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsMapExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsSetExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsString;
import de.unika.ipd.grgen.ir.expr.array.ArrayAvgExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayCopyConstructor;
import de.unika.ipd.grgen.ir.expr.array.ArrayDevExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayEmptyExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayExtract;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfByExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedByExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.expr.array.ArrayItem;
import de.unika.ipd.grgen.ir.expr.array.ArrayKeepOneForEach;
import de.unika.ipd.grgen.ir.expr.array.ArrayKeepOneForEachBy;
import de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfByExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMaxExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMedExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMedUnorderedExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMinExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderAscending;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderAscendingBy;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderDescending;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderDescendingBy;
import de.unika.ipd.grgen.ir.expr.array.ArrayPeekExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayProdExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayReverseExpr;
import de.unika.ipd.grgen.ir.expr.array.ArraySizeExpr;
import de.unika.ipd.grgen.ir.expr.array.ArraySubarrayExpr;
import de.unika.ipd.grgen.ir.expr.array.ArraySumExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayVarExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeAsArrayExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeAsSetExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeCopyConstructor;
import de.unika.ipd.grgen.ir.expr.deque.DequeEmptyExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeInit;
import de.unika.ipd.grgen.ir.expr.deque.DequeItem;
import de.unika.ipd.grgen.ir.expr.deque.DequeLastIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequePeekExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeSizeExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeSubdequeExpr;
import de.unika.ipd.grgen.ir.expr.graph.AdjacentNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeWithRemainingDepthExpr;
import de.unika.ipd.grgen.ir.expr.graph.CanonizeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountAdjacentNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountBoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountBoundedReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountEdgesExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountIncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountNodesExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.DefinedSubgraphExpr;
import de.unika.ipd.grgen.ir.expr.graph.EdgeByNameExpr;
import de.unika.ipd.grgen.ir.expr.graph.EdgeByUniqueExpr;
import de.unika.ipd.grgen.ir.expr.graph.EdgesExpr;
import de.unika.ipd.grgen.ir.expr.graph.EmptyExpr;
import de.unika.ipd.grgen.ir.expr.graph.EqualsAnyExpr;
import de.unika.ipd.grgen.ir.expr.graph.IncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IndexedIncidenceCountIndexAccessExpr;
import de.unika.ipd.grgen.ir.expr.graph.InducedSubgraphExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsAdjacentNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsIncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.Nameof;
import de.unika.ipd.grgen.ir.expr.graph.NodeByNameExpr;
import de.unika.ipd.grgen.ir.expr.graph.NodeByUniqueExpr;
import de.unika.ipd.grgen.ir.expr.graph.NodesExpr;
import de.unika.ipd.grgen.ir.expr.graph.OppositeExpr;
import de.unika.ipd.grgen.ir.expr.graph.ReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.ReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.SizeExpr;
import de.unika.ipd.grgen.ir.expr.graph.SourceExpr;
import de.unika.ipd.grgen.ir.expr.graph.TargetExpr;
import de.unika.ipd.grgen.ir.expr.graph.ThisExpr;
import de.unika.ipd.grgen.ir.expr.graph.Uniqueof;
import de.unika.ipd.grgen.ir.expr.graph.Visited;
import de.unika.ipd.grgen.ir.expr.map.MapAsArrayExpr;
import de.unika.ipd.grgen.ir.expr.map.MapCopyConstructor;
import de.unika.ipd.grgen.ir.expr.map.MapDomainExpr;
import de.unika.ipd.grgen.ir.expr.map.MapEmptyExpr;
import de.unika.ipd.grgen.ir.expr.map.MapInit;
import de.unika.ipd.grgen.ir.expr.map.MapItem;
import de.unika.ipd.grgen.ir.expr.map.MapPeekExpr;
import de.unika.ipd.grgen.ir.expr.map.MapRangeExpr;
import de.unika.ipd.grgen.ir.expr.map.MapSizeExpr;
import de.unika.ipd.grgen.ir.expr.numeric.AbsExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ArcSinCosTanExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ByteMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ByteMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.CeilExpr;
import de.unika.ipd.grgen.ir.expr.numeric.DoubleMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.DoubleMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.EExpr;
import de.unika.ipd.grgen.ir.expr.numeric.FloatMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.FloatMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.FloorExpr;
import de.unika.ipd.grgen.ir.expr.numeric.IntMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.IntMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.LogExpr;
import de.unika.ipd.grgen.ir.expr.numeric.LongMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.LongMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.MaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.MinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.PiExpr;
import de.unika.ipd.grgen.ir.expr.numeric.PowExpr;
import de.unika.ipd.grgen.ir.expr.numeric.RoundExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SgnExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ShortMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ShortMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SinCosTanExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SqrExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SqrtExpr;
import de.unika.ipd.grgen.ir.expr.numeric.TruncateExpr;
import de.unika.ipd.grgen.ir.expr.procenv.ExistsFileExpr;
import de.unika.ipd.grgen.ir.expr.procenv.ImportExpr;
import de.unika.ipd.grgen.ir.expr.procenv.NowExpr;
import de.unika.ipd.grgen.ir.expr.procenv.RandomExpr;
import de.unika.ipd.grgen.ir.expr.set.SetAsArrayExpr;
import de.unika.ipd.grgen.ir.expr.set.SetCopyConstructor;
import de.unika.ipd.grgen.ir.expr.set.SetEmptyExpr;
import de.unika.ipd.grgen.ir.expr.set.SetInit;
import de.unika.ipd.grgen.ir.expr.set.SetItem;
import de.unika.ipd.grgen.ir.expr.set.SetPeekExpr;
import de.unika.ipd.grgen.ir.expr.set.SetSizeExpr;
import de.unika.ipd.grgen.ir.expr.string.StringAsArray;
import de.unika.ipd.grgen.ir.expr.string.StringEndsWith;
import de.unika.ipd.grgen.ir.expr.string.StringIndexOf;
import de.unika.ipd.grgen.ir.expr.string.StringLastIndexOf;
import de.unika.ipd.grgen.ir.expr.string.StringLength;
import de.unika.ipd.grgen.ir.expr.string.StringReplace;
import de.unika.ipd.grgen.ir.expr.string.StringStartsWith;
import de.unika.ipd.grgen.ir.expr.string.StringSubstring;
import de.unika.ipd.grgen.ir.expr.string.StringToLower;
import de.unika.ipd.grgen.ir.expr.string.StringToUpper;

public class ActionsExpressionOrYieldingGen extends CSharpBase
{
	public ActionsExpressionOrYieldingGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix);
		model = backend.unit.getActionsGraphModel();
	}

	//////////////////////////////////////////
	// Condition expression tree generation //
	//////////////////////////////////////////

	public void genExpressionTree(SourceBuilder sb, Expression expr, String className,
			String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(expr instanceof Operator) {
			Operator op = (Operator)expr;
			String opNamePrefix = "";
			if(op.getType() instanceof SetType || op.getType() instanceof MapType)
				opNamePrefix = "DICT_";
			if(op.getType() instanceof ArrayType)
				opNamePrefix = "LIST_";
			if(op.getType() instanceof DequeType)
				opNamePrefix = "DEQUE_";
			if(op.getOpCode() == Operator.EQ || op.getOpCode() == Operator.NE
					|| op.getOpCode() == Operator.SE
					|| op.getOpCode() == Operator.GT || op.getOpCode() == Operator.GE
					|| op.getOpCode() == Operator.LT || op.getOpCode() == Operator.LE) {
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
			if(op.getOpCode() == Operator.GT || op.getOpCode() == Operator.GE
					|| op.getOpCode() == Operator.LT || op.getOpCode() == Operator.LE) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof StringType) {
					opNamePrefix = "STRING_";
				}
			}
			if(model.isEqualClassDefined() && (op.getOpCode() == Operator.EQ || op.getOpCode() == Operator.NE)) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof ObjectType || opnd.getType() instanceof ExternalType) {
					opNamePrefix = "EXTERNAL_";
				}
			}
			if(model.isLowerClassDefined() && (op.getOpCode() == Operator.GT || op.getOpCode() == Operator.GE
					|| op.getOpCode() == Operator.LT || op.getOpCode() == Operator.LE)) {
				Expression opnd = op.getOperand(0); // or .getOperand(1), irrelevant
				if(opnd.getType() instanceof ObjectType || opnd.getType() instanceof ExternalType) {
					opNamePrefix = "EXTERNAL_";
				}
			}

			sb.append("new GRGEN_EXPR." + opNamePrefix + Operator.opNames[op.getOpCode()] + "(");
			switch(op.arity()) {
			case 1:
				genExpressionTree(sb, op.getOperand(0), className, pathPrefix, alreadyDefinedEntityToName);
				break;
			case 2:
				genExpressionTree(sb, op.getOperand(0), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
				genExpressionTree(sb, op.getOperand(1), className, pathPrefix, alreadyDefinedEntityToName);
				if(op.getOpCode() == Operator.IN) {
					if(op.getOperand(0) instanceof GraphEntityExpression)
						sb.append(", \"" + formatElementInterfaceRef(op.getOperand(0).getType()) + "\"");
					boolean isDictionary = op.getOperand(1).getType() instanceof SetType
							|| op.getOperand(1).getType() instanceof MapType;
					sb.append(isDictionary ? ", true" : ", false");
				}
				break;
			case 3:
				if(op.getOpCode() == Operator.COND) {
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
		} else if(expr instanceof Qualification) {
			Qualification qual = (Qualification)expr;
			Entity owner = qual.getOwner();
			Entity member = qual.getMember();
			if(Expression.isGlobalVariable(owner)) {
				sb.append("new GRGEN_EXPR.GlobalVariableQualification(\"" + formatType(owner.getType())
						+ "\", \"" + formatIdentifiable(owner, pathPrefix, alreadyDefinedEntityToName) + "\", \""
						+ formatIdentifiable(member) + "\")");
			} else if(owner != null) {
				sb.append("new GRGEN_EXPR.Qualification(\"" + formatElementInterfaceRef(owner.getType())
						+ "\", \"" + formatEntity(owner, pathPrefix, alreadyDefinedEntityToName) + "\", \""
						+ formatIdentifiable(member) + "\")");
			} else {
				sb.append("new GRGEN_EXPR.CastQualification(");
				genExpressionTree(sb, qual.getOwnerExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(member) + "\")");
			}
		} else if(expr instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression)expr;
			sb.append("new GRGEN_EXPR.ConstantEnumExpression(\"" + enumExp.getType().getIdent().toString()
					+ "\", \"" + enumExp.getEnumItem().toString() + "\")");
		} else if(expr instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant)expr;
			sb.append("new GRGEN_EXPR.Constant(\"" + escapeBackslashAndDoubleQuotes(getValueAsCSSharpString(constant))
					+ "\")");
		} else if(expr instanceof Nameof) {
			Nameof no = (Nameof)expr;
			sb.append("new GRGEN_EXPR.Nameof(");
			if(no.getNamedEntity() == null)
				sb.append("null");
			else
				genExpressionTree(sb, no.getNamedEntity(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof Uniqueof) {
			Uniqueof uo = (Uniqueof)expr;
			sb.append("new GRGEN_EXPR.Uniqueof(");
			if(uo.getEntity() != null)
				genExpressionTree(sb, uo.getEntity(), className, pathPrefix, alreadyDefinedEntityToName);
			else
				sb.append("null");
			if(uo.getEntity() != null && uo.getEntity().getType() instanceof NodeType)
				sb.append(", true, false");
			else if(uo.getEntity() != null && uo.getEntity().getType() instanceof EdgeType)
				sb.append(", false, false");
			else
				sb.append(", false, true");
			sb.append(")");
		} else if(expr instanceof ExistsFileExpr) {
			ExistsFileExpr efe = (ExistsFileExpr)expr;
			sb.append("new GRGEN_EXPR.ExistsFileExpression(");
			genExpressionTree(sb, efe.getPathExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ImportExpr) {
			ImportExpr ie = (ImportExpr)expr;
			sb.append("new GRGEN_EXPR.ImportExpression(");
			genExpressionTree(sb, ie.getPathExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof CopyExpr) {
			CopyExpr ce = (CopyExpr)expr;
			Type t = ce.getSourceExpr().getType();
			sb.append("new GRGEN_EXPR.CopyExpression(");
			genExpressionTree(sb, ce.getSourceExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(t instanceof GraphType) {
				sb.append(", null");
			} else { // no match type possible here, can only occur in filter function (-> CSharpBase expression)
				sb.append(", \"" + formatType(t) + "\"");
			}
			sb.append(")");
		} else if(expr instanceof Count) {
			Count count = (Count)expr;
			sb.append("new GRGEN_EXPR.Count(\"" + formatIdentifiable(count.getIterated()) + "\")");
		} else if(expr instanceof Typeof) {
			Typeof to = (Typeof)expr;
			sb.append("new GRGEN_EXPR.Typeof(\"" + formatEntity(to.getEntity(), pathPrefix, alreadyDefinedEntityToName)
					+ "\")");
		} else if(expr instanceof Cast) {
			Cast cast = (Cast)expr;
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
		} else if(expr instanceof VariableExpression) {
			Variable var = ((VariableExpression)expr).getVariable();
			if(!Expression.isGlobalVariable(var)) {
				sb.append("new GRGEN_EXPR.VariableExpression(\""
						+ formatEntity(var, pathPrefix, alreadyDefinedEntityToName) + "\")");
			} else {
				sb.append("new GRGEN_EXPR.GlobalVariableExpression(\"" + formatIdentifiable(var) + "\", \""
						+ formatType(var.getType()) + "\")");
			}
		} else if(expr instanceof GraphEntityExpression) {
			GraphEntity ent = ((GraphEntityExpression)expr).getGraphEntity();
			if(!Expression.isGlobalVariable(ent)) {
				sb.append("new GRGEN_EXPR.GraphEntityExpression(\""
						+ formatEntity(ent, pathPrefix, alreadyDefinedEntityToName) + "\")");
			} else {
				sb.append("new GRGEN_EXPR.GlobalVariableExpression(\"" + formatIdentifiable(ent) + "\", \""
						+ formatType(ent.getType()) + "\")");
			}
		} else if(expr instanceof Visited) {
			Visited vis = (Visited)expr;
			sb.append("new GRGEN_EXPR.Visited(");
			genExpressionTree(sb, vis.getEntity(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, vis.getVisitorID(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof RandomExpr) {
			RandomExpr re = (RandomExpr)expr;
			sb.append("new GRGEN_EXPR.Random(");
			if(re.getNumExpr() != null)
				genExpressionTree(sb, re.getNumExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ThisExpr) {
			sb.append("new GRGEN_EXPR.This()");
		} else if(expr instanceof StringLength) {
			StringLength strlen = (StringLength)expr;
			sb.append("new GRGEN_EXPR.StringLength(");
			genExpressionTree(sb, strlen.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof StringToUpper) {
			StringToUpper strtoup = (StringToUpper)expr;
			sb.append("new GRGEN_EXPR.StringToUpper(");
			genExpressionTree(sb, strtoup.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof StringToLower) {
			StringToLower strtolow = (StringToLower)expr;
			sb.append("new GRGEN_EXPR.StringToLower(");
			genExpressionTree(sb, strtolow.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof StringSubstring) {
			StringSubstring strsubstr = (StringSubstring)expr;
			sb.append("new GRGEN_EXPR.StringSubstring(");
			genExpressionTree(sb, strsubstr.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strsubstr.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(strsubstr.getLengthExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, strsubstr.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof StringIndexOf) {
			StringIndexOf strio = (StringIndexOf)expr;
			sb.append("new GRGEN_EXPR.StringIndexOf(");
			genExpressionTree(sb, strio.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strio.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(strio.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, strio.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof StringLastIndexOf) {
			StringLastIndexOf strlio = (StringLastIndexOf)expr;
			sb.append("new GRGEN_EXPR.StringLastIndexOf(");
			genExpressionTree(sb, strlio.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strlio.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(strlio.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, strlio.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof StringStartsWith) {
			StringStartsWith strsw = (StringStartsWith)expr;
			sb.append("new GRGEN_EXPR.StringStartsWith(");
			genExpressionTree(sb, strsw.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strsw.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof StringEndsWith) {
			StringEndsWith strew = (StringEndsWith)expr;
			sb.append("new GRGEN_EXPR.StringEndsWith(");
			genExpressionTree(sb, strew.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strew.getStringToSearchForExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof StringReplace) {
			StringReplace strrepl = (StringReplace)expr;
			sb.append("new GRGEN_EXPR.StringReplace(");
			genExpressionTree(sb, strrepl.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strrepl.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strrepl.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, strrepl.getReplaceStrExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof StringAsArray) {
			StringAsArray saa = (StringAsArray)expr;
			sb.append("new GRGEN_EXPR.StringAsArray(");
			genExpressionTree(sb, saa.getStringExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, saa.getStringToSplitAtExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof IndexedAccessExpr) {
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
		} else if(expr instanceof IndexedIncidenceCountIndexAccessExpr) {
			IndexedIncidenceCountIndexAccessExpr ia = (IndexedIncidenceCountIndexAccessExpr)expr;
			sb.append("new GRGEN_EXPR.IncidenceCountIndexAccess(");
			sb.append("\"" + ia.getTarget().getIdent() + "\", ");
			genExpressionTree(sb, ia.getKeyExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + formatElementInterfaceRef(ia.getKeyExpr().getType()) + "\"");
			sb.append(")");
		} else if(expr instanceof MapSizeExpr) {
			MapSizeExpr ms = (MapSizeExpr)expr;
			sb.append("new GRGEN_EXPR.MapSize(");
			genExpressionTree(sb, ms.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof MapEmptyExpr) {
			MapEmptyExpr me = (MapEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.MapEmpty(");
			genExpressionTree(sb, me.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof MapDomainExpr) {
			MapDomainExpr md = (MapDomainExpr)expr;
			sb.append("new GRGEN_EXPR.MapDomain(");
			genExpressionTree(sb, md.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof MapRangeExpr) {
			MapRangeExpr mr = (MapRangeExpr)expr;
			sb.append("new GRGEN_EXPR.MapRange(");
			genExpressionTree(sb, mr.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof MapAsArrayExpr) {
			MapAsArrayExpr maa = (MapAsArrayExpr)expr;
			sb.append("new GRGEN_EXPR.MapAsArray(");
			genExpressionTree(sb, maa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof MapPeekExpr) {
			MapPeekExpr mp = (MapPeekExpr)expr;
			sb.append("new GRGEN_EXPR.MapPeek(");
			genExpressionTree(sb, mp.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, mp.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SetSizeExpr) {
			SetSizeExpr ss = (SetSizeExpr)expr;
			sb.append("new GRGEN_EXPR.SetSize(");
			genExpressionTree(sb, ss.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SetEmptyExpr) {
			SetEmptyExpr se = (SetEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.SetEmpty(");
			genExpressionTree(sb, se.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SetPeekExpr) {
			SetPeekExpr sp = (SetPeekExpr)expr;
			sb.append("new GRGEN_EXPR.SetPeek(");
			genExpressionTree(sb, sp.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, sp.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SetAsArrayExpr) {
			SetAsArrayExpr saa = (SetAsArrayExpr)expr;
			sb.append("new GRGEN_EXPR.SetAsArray(");
			genExpressionTree(sb, saa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArraySizeExpr) {
			ArraySizeExpr as = (ArraySizeExpr)expr;
			sb.append("new GRGEN_EXPR.ArraySize(");
			genExpressionTree(sb, as.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayEmptyExpr) {
			ArrayEmptyExpr ae = (ArrayEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayEmpty(");
			genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayPeekExpr) {
			ArrayPeekExpr ap = (ArrayPeekExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayPeek(");
			genExpressionTree(sb, ap.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(ap.getNumberExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, ap.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof ArrayIndexOfExpr) {
			ArrayIndexOfExpr ai = (ArrayIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOf(");
			genExpressionTree(sb, ai.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ai.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(ai.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, ai.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof ArrayIndexOfByExpr) {
			ArrayIndexOfByExpr aib = (ArrayIndexOfByExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOfBy(");
			genExpressionTree(sb, aib.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + ((ArrayType)aib.getTargetExpr().getType()).getValueType().getIdent().toString() + "\"");
			sb.append(", \"" + formatIdentifiable(aib.getMember()) + "\", ");
			genExpressionTree(sb, aib.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(aib.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, aib.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof ArrayIndexOfOrderedExpr) {
			ArrayIndexOfOrderedExpr aio = (ArrayIndexOfOrderedExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOfOrdered(");
			genExpressionTree(sb, aio.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, aio.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayIndexOfOrderedByExpr) {
			ArrayIndexOfOrderedByExpr aiob = (ArrayIndexOfOrderedByExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayIndexOfOrderedBy(");
			genExpressionTree(sb, aiob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + ((ArrayType)aiob.getTargetExpr().getType()).getValueType().getIdent().toString() + "\"");
			sb.append(", \"" + formatIdentifiable(aiob.getMember()) + "\", ");
			genExpressionTree(sb, aiob.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayLastIndexOfExpr) {
			ArrayLastIndexOfExpr ali = (ArrayLastIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayLastIndexOf(");
			genExpressionTree(sb, ali.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ali.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(ali.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, ali.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof ArrayLastIndexOfByExpr) {
			ArrayLastIndexOfByExpr alib = (ArrayLastIndexOfByExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayLastIndexOfBy(");
			genExpressionTree(sb, alib.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + ((ArrayType)alib.getTargetExpr().getType()).getValueType().getIdent().toString() + "\"");
			sb.append(", \"" + formatIdentifiable(alib.getMember()) + "\", ");
			genExpressionTree(sb, alib.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(alib.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, alib.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof ArraySubarrayExpr) {
			ArraySubarrayExpr as = (ArraySubarrayExpr)expr;
			sb.append("new GRGEN_EXPR.ArraySubarray(");
			genExpressionTree(sb, as.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, as.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, as.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayOrderAscending) {
			ArrayOrderAscending aoa = (ArrayOrderAscending)expr;
			sb.append("new GRGEN_EXPR.ArrayOrder(");
			genExpressionTree(sb, aoa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", true");
			sb.append(")");
		} else if(expr instanceof ArrayOrderDescending) {
			ArrayOrderDescending aod = (ArrayOrderDescending)expr;
			sb.append("new GRGEN_EXPR.ArrayOrder(");
			genExpressionTree(sb, aod.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", false");
			sb.append(")");
		} else if(expr instanceof ArrayKeepOneForEach) {
			ArrayKeepOneForEach ako = (ArrayKeepOneForEach)expr;
			sb.append("new GRGEN_EXPR.ArrayKeepOneForEach(");
			genExpressionTree(sb, ako.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayOrderAscendingBy) {
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
			} else if(arrayValueType instanceof MatchTypeIterated) {
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
			} else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayOfMatchTypeOrderBy(");
				genExpressionTree(sb, aoab.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(aoab.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderAscending");
				sb.append(")");
			} else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOfMatchClassTypeOrderBy(");
				genExpressionTree(sb, aoab.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", \"" + formatIdentifiable(aoab.getMember()) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderAscending");
				sb.append(")");
			}
		} else if(expr instanceof ArrayOrderDescendingBy) {
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
			} else if(arrayValueType instanceof MatchTypeIterated) {
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
			} else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayOfMatchTypeOrderBy(");
				genExpressionTree(sb, aodb.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(aodb.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderDescending");
				sb.append(")");
			} else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOfMatchClassTypeOrderBy(");
				genExpressionTree(sb, aodb.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", \"" + formatIdentifiable(aodb.getMember()) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.OrderDescending");
				sb.append(")");
			}
		} else if(expr instanceof ArrayKeepOneForEachBy) {
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
			} else if(arrayValueType instanceof MatchTypeIterated) {
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
			} else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayOfMatchTypeOrderBy(");
				genExpressionTree(sb, akob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", \"" + formatIdentifiable(akob.getMember()) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.KeepOneForEach");
				sb.append(")");
			} else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayOfMatchClassTypeOrderBy(");
				genExpressionTree(sb, akob.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", \"" + formatIdentifiable(akob.getMember()) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(", GRGEN_LIBGR.OrderMethod.KeepOneForEach");
				sb.append(")");
			}
		} else if(expr instanceof ArrayReverseExpr) {
			ArrayReverseExpr ar = (ArrayReverseExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayReverse(");
			genExpressionTree(sb, ar.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayExtract) {
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
			} else if(arrayValueType instanceof MatchTypeIterated) {
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
			} else if(arrayValueType instanceof MatchType) {
				MatchType matchType = (MatchType)arrayValueType;
				Rule rule = matchType.getAction();
				sb.append("new GRGEN_EXPR.ArrayExtract(");
				genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(ae.getMember()) + "\"");
				sb.append(", \"" + formatIdentifiable(rule) + "\"");
				sb.append(", " + (rule.getPackageContainedIn()!=null ? "\"" + rule.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(")");
			} else if(arrayValueType instanceof DefinedMatchType) {
				DefinedMatchType matchType = (DefinedMatchType)arrayValueType;
				sb.append("new GRGEN_EXPR.ArrayExtractMatchClass(");
				genExpressionTree(sb, ae.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", \"" + formatIdentifiable(ae.getMember()) + "\"");
				sb.append(", \"" + formatIdentifiable(matchType) + "\"");
				sb.append(", " + (matchType.getPackageContainedIn()!=null ? "\"" + matchType.getPackageContainedIn() + "\"" : "null") + "");
				sb.append(")");
			}
		} else if(expr instanceof ArrayAsSetExpr) {
			ArrayAsSetExpr aas = (ArrayAsSetExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAsSet(");
			genExpressionTree(sb, aas.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayAsDequeExpr) {
			ArrayAsDequeExpr aad = (ArrayAsDequeExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAsDeque(");
			genExpressionTree(sb, aad.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayAsMapExpr) {
			ArrayAsMapExpr aam = (ArrayAsMapExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAsMap(");
			genExpressionTree(sb, aam.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayAsString) {
			ArrayAsString aas = (ArrayAsString)expr;
			sb.append("new GRGEN_EXPR.ArrayAsString(");
			genExpressionTree(sb, aas.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, aas.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArraySumExpr) {
			ArraySumExpr as = (ArraySumExpr)expr;
			sb.append("new GRGEN_EXPR.ArraySum(");
			genExpressionTree(sb, as.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayProdExpr) {
			ArrayProdExpr ap = (ArrayProdExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayProd(");
			genExpressionTree(sb, ap.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayMinExpr) {
			ArrayMinExpr am = (ArrayMinExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMin(");
			genExpressionTree(sb, am.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayMaxExpr) {
			ArrayMaxExpr am = (ArrayMaxExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMax(");
			genExpressionTree(sb, am.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayAvgExpr) {
			ArrayAvgExpr aa = (ArrayAvgExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayAvg(");
			genExpressionTree(sb, aa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayMedExpr) {
			ArrayMedExpr am = (ArrayMedExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMed(");
			genExpressionTree(sb, am.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayMedUnorderedExpr) {
			ArrayMedUnorderedExpr amu = (ArrayMedUnorderedExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayMedUnordered(");
			genExpressionTree(sb, amu.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayVarExpr) {
			ArrayVarExpr av = (ArrayVarExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayVar(");
			genExpressionTree(sb, av.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayDevExpr) {
			ArrayDevExpr ad = (ArrayDevExpr)expr;
			sb.append("new GRGEN_EXPR.ArrayDev(");
			genExpressionTree(sb, ad.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DequeSizeExpr) {
			DequeSizeExpr ds = (DequeSizeExpr)expr;
			sb.append("new GRGEN_EXPR.DequeSize(");
			genExpressionTree(sb, ds.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DequeEmptyExpr) {
			DequeEmptyExpr de = (DequeEmptyExpr)expr;
			sb.append("new GRGEN_EXPR.DequeEmpty(");
			genExpressionTree(sb, de.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DequePeekExpr) {
			DequePeekExpr dp = (DequePeekExpr)expr;
			sb.append("new GRGEN_EXPR.DequePeek(");
			genExpressionTree(sb, dp.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(dp.getNumberExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, dp.getNumberExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof DequeIndexOfExpr) {
			DequeIndexOfExpr di = (DequeIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.DequeIndexOf(");
			genExpressionTree(sb, di.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, di.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(di.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, di.getStartIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof DequeLastIndexOfExpr) {
			DequeLastIndexOfExpr dli = (DequeLastIndexOfExpr)expr;
			sb.append("new GRGEN_EXPR.DequeLastIndexOf(");
			genExpressionTree(sb, dli.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, dli.getValueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DequeSubdequeExpr) {
			DequeSubdequeExpr dsd = (DequeSubdequeExpr)expr;
			sb.append("new GRGEN_EXPR.DequeSubdeque(");
			genExpressionTree(sb, dsd.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, dsd.getStartExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, dsd.getLengthExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DequeAsSetExpr) {
			DequeAsSetExpr das = (DequeAsSetExpr)expr;
			sb.append("new GRGEN_EXPR.DequeAsSet(");
			genExpressionTree(sb, das.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DequeAsArrayExpr) {
			DequeAsArrayExpr daa = (DequeAsArrayExpr)expr;
			sb.append("new GRGEN_EXPR.DequeAsArray(");
			genExpressionTree(sb, daa.getTargetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof MapInit) {
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
				for(int i = 0; i < openParenthesis; ++i)
					sb.append(")");
				sb.append(")");
			}
		} else if(expr instanceof SetInit) {
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
				for(int i = 0; i < openParenthesis; ++i)
					sb.append(")");
				sb.append(")");
			}
		} else if(expr instanceof ArrayInit) {
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
				for(int i = 0; i < openParenthesis; ++i)
					sb.append(")");
				sb.append(")");
			}
		} else if(expr instanceof DequeInit) {
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
				for(int i = 0; i < openParenthesis; ++i)
					sb.append(")");
				sb.append(")");
			}
		} else if(expr instanceof MapCopyConstructor) {
			MapCopyConstructor mcc = (MapCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.MapCopyConstructor(\"" + formatType(mcc.getMapType()) + "\", ");
			sb.append("\"" + formatSequenceType(mcc.getMapType().getKeyType()) + "\", ");
			sb.append("\"" + formatSequenceType(mcc.getMapType().getValueType()) + "\", ");
			genExpressionTree(sb, mcc.getMapToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SetCopyConstructor) {
			SetCopyConstructor scc = (SetCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.SetCopyConstructor(\"" + formatType(scc.getSetType()) + "\", ");
			sb.append("\"" + formatSequenceType(scc.getSetType().getValueType()) + "\", ");
			genExpressionTree(sb, scc.getSetToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArrayCopyConstructor) {
			ArrayCopyConstructor acc = (ArrayCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.ArrayCopyConstructor(\"" + formatType(acc.getArrayType()) + "\", ");
			sb.append("\"" + formatSequenceType(acc.getArrayType().getValueType()) + "\", ");
			genExpressionTree(sb, acc.getArrayToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DequeCopyConstructor) {
			DequeCopyConstructor dcc = (DequeCopyConstructor)expr;
			sb.append("new GRGEN_EXPR.DequeCopyConstructor(\"" + formatType(dcc.getDequeType()) + "\", ");
			sb.append("\"" + formatSequenceType(dcc.getDequeType().getValueType()) + "\", ");
			genExpressionTree(sb, dcc.getDequeToCopy(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof FunctionInvocationExpr) {
			FunctionInvocationExpr fi = (FunctionInvocationExpr)expr;
			sb.append("new GRGEN_EXPR.FunctionInvocation(\"GRGEN_ACTIONS." + getPackagePrefixDot(fi.getFunction())
					+ "\", \"" + fi.getFunction().getIdent() + "\", new GRGEN_EXPR.Expression[] {");
			for(int i = 0; i < fi.arity(); ++i) {
				Expression argument = fi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i = 0; i < fi.arity(); ++i) {
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
		} else if(expr instanceof ExternalFunctionInvocationExpr) {
			ExternalFunctionInvocationExpr efi = (ExternalFunctionInvocationExpr)expr;
			sb.append("new GRGEN_EXPR.ExternalFunctionInvocation(\"" + efi.getExternalFunc().getIdent()
					+ "\", new GRGEN_EXPR.Expression[] {");
			for(int i = 0; i < efi.arity(); ++i) {
				Expression argument = efi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i = 0; i < efi.arity(); ++i) {
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
		} else if(expr instanceof FunctionMethodInvocationExpr) {
			FunctionMethodInvocationExpr fmi = (FunctionMethodInvocationExpr)expr;
			sb.append("new GRGEN_EXPR.FunctionMethodInvocation(\"" + formatElementInterfaceRef(fmi.getOwner().getType()) + "\","
					+ " \"" + formatEntity(fmi.getOwner(), pathPrefix, alreadyDefinedEntityToName) + "\","
					+ " \"" + fmi.getFunction().getIdent() + "\", new GRGEN_EXPR.Expression[] {");
			for(int i = 0; i < fmi.arity(); ++i) {
				Expression argument = fmi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i = 0; i < fmi.arity(); ++i) {
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
		} else if(expr instanceof ExternalFunctionMethodInvocationExpr) {
			ExternalFunctionMethodInvocationExpr efmi = (ExternalFunctionMethodInvocationExpr)expr;
			sb.append("new GRGEN_EXPR.ExternalFunctionMethodInvocation(");
			genExpressionTree(sb, efmi.getOwner(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", \"" + efmi.getExternalFunc().getIdent() + "\", new GRGEN_EXPR.Expression[] {");
			for(int i = 0; i < efmi.arity(); ++i) {
				Expression argument = efmi.getArgument(i);
				genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			sb.append("}, ");
			sb.append("new String[] {");
			for(int i = 0; i < efmi.arity(); ++i) {
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
		} else if(expr instanceof EdgesExpr) {
			EdgesExpr e = (EdgesExpr)expr;
			sb.append("new GRGEN_EXPR.Edges(");
			genExpressionTree(sb, e.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			sb.append(getDirectedness(e.getType()));
			sb.append(")");
		} else if(expr instanceof NodesExpr) {
			NodesExpr n = (NodesExpr)expr;
			sb.append("new GRGEN_EXPR.Nodes(");
			genExpressionTree(sb, n.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof CountEdgesExpr) {
			CountEdgesExpr ce = (CountEdgesExpr)expr;
			sb.append("new GRGEN_EXPR.CountEdges(");
			genExpressionTree(sb, ce.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof CountNodesExpr) {
			CountNodesExpr cn = (CountNodesExpr)expr;
			sb.append("new GRGEN_EXPR.CountNodes(");
			genExpressionTree(sb, cn.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof NowExpr) {
			//NowExpr n = (NowExpr) expr;
			sb.append("new GRGEN_EXPR.Now(");
			sb.append(")");
		} else if(expr instanceof EmptyExpr) {
			//EmptyExpr e = (EmptyExpr) expr;
			sb.append("new GRGEN_EXPR.Empty(");
			sb.append(")");
		} else if(expr instanceof SizeExpr) {
			//SizeExpr s = (SizeExpr) expr;
			sb.append("new GRGEN_EXPR.Size(");
			sb.append(")");
		} else if(expr instanceof SourceExpr) {
			SourceExpr s = (SourceExpr)expr;
			sb.append("new GRGEN_EXPR.Source(");
			genExpressionTree(sb, s.getEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof TargetExpr) {
			TargetExpr t = (TargetExpr)expr;
			sb.append("new GRGEN_EXPR.Target(");
			genExpressionTree(sb, t.getEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof OppositeExpr) {
			OppositeExpr o = (OppositeExpr)expr;
			sb.append("new GRGEN_EXPR.Opposite(");
			genExpressionTree(sb, o.getEdgeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			genExpressionTree(sb, o.getNodeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof NodeByNameExpr) {
			NodeByNameExpr nbn = (NodeByNameExpr)expr;
			sb.append("new GRGEN_EXPR.NodeByName(");
			genExpressionTree(sb, nbn.getNameExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!nbn.getNodeTypeExpr().getType().getIdent().equals("Node")) {
				sb.append(", ");
				genExpressionTree(sb, nbn.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof EdgeByNameExpr) {
			EdgeByNameExpr ebn = (EdgeByNameExpr)expr;
			sb.append("new GRGEN_EXPR.EdgeByName(");
			genExpressionTree(sb, ebn.getNameExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!ebn.getEdgeTypeExpr().getType().getIdent().equals("AEdge")) {
				sb.append(", ");
				genExpressionTree(sb, ebn.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof NodeByUniqueExpr) {
			NodeByUniqueExpr nbu = (NodeByUniqueExpr)expr;
			sb.append("new GRGEN_EXPR.NodeByUnique(");
			genExpressionTree(sb, nbu.getUniqueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!nbu.getNodeTypeExpr().getType().getIdent().equals("Node")) {
				sb.append(", ");
				genExpressionTree(sb, nbu.getNodeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof EdgeByUniqueExpr) {
			EdgeByUniqueExpr ebu = (EdgeByUniqueExpr)expr;
			sb.append("new GRGEN_EXPR.EdgeByUnique(");
			genExpressionTree(sb, ebu.getUniqueExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(!ebu.getEdgeTypeExpr().getType().getIdent().equals("AEdge")) {
				sb.append(", ");
				genExpressionTree(sb, ebu.getEdgeTypeExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr ie = (IncidentEdgeExpr)expr;
			if(ie.Direction() == IncidentEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.Outgoing(");
			} else if(ie.Direction() == IncidentEdgeExpr.INCOMING) {
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
		} else if(expr instanceof AdjacentNodeExpr) {
			AdjacentNodeExpr an = (AdjacentNodeExpr)expr;
			if(an.Direction() == AdjacentNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.AdjacentOutgoing(");
			} else if(an.Direction() == AdjacentNodeExpr.INCOMING) {
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
		} else if(expr instanceof CountIncidentEdgeExpr) {
			CountIncidentEdgeExpr cie = (CountIncidentEdgeExpr)expr;
			if(cie.Direction() == CountIncidentEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountOutgoing(");
			} else if(cie.Direction() == CountIncidentEdgeExpr.INCOMING) {
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
		} else if(expr instanceof CountAdjacentNodeExpr) {
			CountAdjacentNodeExpr can = (CountAdjacentNodeExpr)expr;
			if(can.Direction() == CountAdjacentNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountAdjacentOutgoing(");
			} else if(can.Direction() == CountAdjacentNodeExpr.INCOMING) {
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
		} else if(expr instanceof IsAdjacentNodeExpr) {
			IsAdjacentNodeExpr ian = (IsAdjacentNodeExpr)expr;
			if(ian.Direction() == IsAdjacentNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsAdjacentOutgoing(");
			} else if(ian.Direction() == IsAdjacentNodeExpr.INCOMING) {
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
		} else if(expr instanceof IsIncidentEdgeExpr) {
			IsIncidentEdgeExpr iie = (IsIncidentEdgeExpr)expr;
			if(iie.Direction() == IsIncidentEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsOutgoing(");
			} else if(iie.Direction() == IsIncidentEdgeExpr.INCOMING) {
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
		} else if(expr instanceof ReachableEdgeExpr) {
			ReachableEdgeExpr re = (ReachableEdgeExpr)expr;
			if(re.Direction() == ReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.ReachableEdgesOutgoing(");
			} else if(re.Direction() == ReachableEdgeExpr.INCOMING) {
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
		} else if(expr instanceof ReachableNodeExpr) {
			ReachableNodeExpr rn = (ReachableNodeExpr)expr;
			if(rn.Direction() == ReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.ReachableOutgoing(");
			} else if(rn.Direction() == ReachableNodeExpr.INCOMING) {
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
		} else if(expr instanceof CountReachableEdgeExpr) {
			CountReachableEdgeExpr cre = (CountReachableEdgeExpr)expr;
			if(cre.Direction() == CountReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountReachableEdgesOutgoing(");
			} else if(cre.Direction() == CountReachableEdgeExpr.INCOMING) {
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
		} else if(expr instanceof CountReachableNodeExpr) {
			CountReachableNodeExpr crn = (CountReachableNodeExpr)expr;
			if(crn.Direction() == CountReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountReachableOutgoing(");
			} else if(crn.Direction() == CountReachableNodeExpr.INCOMING) {
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
		} else if(expr instanceof IsReachableNodeExpr) {
			IsReachableNodeExpr irn = (IsReachableNodeExpr)expr;
			if(irn.Direction() == IsReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsReachableOutgoing(");
			} else if(irn.Direction() == IsReachableNodeExpr.INCOMING) {
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
		} else if(expr instanceof IsReachableEdgeExpr) {
			IsReachableEdgeExpr ire = (IsReachableEdgeExpr)expr;
			if(ire.Direction() == IsReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsReachableEdgesOutgoing(");
			} else if(ire.Direction() == IsReachableEdgeExpr.INCOMING) {
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
		} else if(expr instanceof BoundedReachableEdgeExpr) {
			BoundedReachableEdgeExpr bre = (BoundedReachableEdgeExpr)expr;
			if(bre.Direction() == BoundedReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.BoundedReachableEdgesOutgoing(");
			} else if(bre.Direction() == BoundedReachableEdgeExpr.INCOMING) {
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
		} else if(expr instanceof BoundedReachableNodeExpr) {
			BoundedReachableNodeExpr brn = (BoundedReachableNodeExpr)expr;
			if(brn.Direction() == BoundedReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.BoundedReachableOutgoing(");
			} else if(brn.Direction() == BoundedReachableNodeExpr.INCOMING) {
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
		} else if(expr instanceof BoundedReachableNodeWithRemainingDepthExpr) {
			BoundedReachableNodeWithRemainingDepthExpr brnwrd = (BoundedReachableNodeWithRemainingDepthExpr)expr;
			if(brnwrd.Direction() == BoundedReachableNodeWithRemainingDepthExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.BoundedReachableWithRemainingDepthOutgoing(");
			} else if(brnwrd.Direction() == BoundedReachableNodeWithRemainingDepthExpr.INCOMING) {
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
		} else if(expr instanceof CountBoundedReachableEdgeExpr) {
			CountBoundedReachableEdgeExpr cbre = (CountBoundedReachableEdgeExpr)expr;
			if(cbre.Direction() == CountBoundedReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountBoundedReachableEdgesOutgoing(");
			} else if(cbre.Direction() == CountBoundedReachableEdgeExpr.INCOMING) {
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
		} else if(expr instanceof CountBoundedReachableNodeExpr) {
			CountBoundedReachableNodeExpr cbrn = (CountBoundedReachableNodeExpr)expr;
			if(cbrn.Direction() == CountBoundedReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.CountBoundedReachableOutgoing(");
			} else if(cbrn.Direction() == CountBoundedReachableNodeExpr.INCOMING) {
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
		} else if(expr instanceof IsBoundedReachableNodeExpr) {
			IsBoundedReachableNodeExpr ibrn = (IsBoundedReachableNodeExpr)expr;
			if(ibrn.Direction() == IsBoundedReachableNodeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsBoundedReachableOutgoing(");
			} else if(ibrn.Direction() == IsBoundedReachableNodeExpr.INCOMING) {
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
		} else if(expr instanceof IsBoundedReachableEdgeExpr) {
			IsBoundedReachableEdgeExpr ibre = (IsBoundedReachableEdgeExpr)expr;
			if(ibre.Direction() == IsBoundedReachableEdgeExpr.OUTGOING) {
				sb.append("new GRGEN_EXPR.IsBoundedReachableEdgesOutgoing(");
			} else if(ibre.Direction() == IsBoundedReachableEdgeExpr.INCOMING) {
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
		} else if(expr instanceof InducedSubgraphExpr) {
			InducedSubgraphExpr is = (InducedSubgraphExpr)expr;
			sb.append("new GRGEN_EXPR.InducedSubgraph(");
			genExpressionTree(sb, is.getSetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof DefinedSubgraphExpr) {
			DefinedSubgraphExpr ds = (DefinedSubgraphExpr)expr;
			sb.append("new GRGEN_EXPR.DefinedSubgraph(");
			genExpressionTree(sb, ds.getSetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(",");
			sb.append(getDirectedness(ds.getSetExpr().getType()));
			sb.append(")");
		} else if(expr instanceof EqualsAnyExpr) {
			EqualsAnyExpr ea = (EqualsAnyExpr)expr;
			sb.append("new GRGEN_EXPR.EqualsAny(");
			genExpressionTree(sb, ea.getSubgraphExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, ea.getSetExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			sb.append(ea.getIncludingAttributes() ? "true" : "false");
			sb.append(")");
		} else if(expr instanceof MaxExpr) {
			MaxExpr m = (MaxExpr)expr;
			sb.append("new GRGEN_EXPR.Max(");
			genExpressionTree(sb, m.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, m.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof MinExpr) {
			MinExpr m = (MinExpr)expr;
			sb.append("new GRGEN_EXPR.Min(");
			genExpressionTree(sb, m.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
			genExpressionTree(sb, m.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof AbsExpr) {
			AbsExpr a = (AbsExpr)expr;
			sb.append("new GRGEN_EXPR.Abs(");
			genExpressionTree(sb, a.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SgnExpr) {
			SgnExpr s = (SgnExpr)expr;
			sb.append("new GRGEN_EXPR.Sgn(");
			genExpressionTree(sb, s.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof PiExpr) {
			//PiExpr pi = (PiExpr) expr;
			sb.append("new GRGEN_EXPR.Pi(");
			sb.append(")");
		} else if(expr instanceof EExpr) {
			//EExpr e = (EExpr) expr;
			sb.append("new GRGEN_EXPR.E(");
			sb.append(")");
		} else if(expr instanceof ByteMinExpr) {
			sb.append("new GRGEN_EXPR.ByteMin(");
			sb.append(")");
		} else if(expr instanceof ByteMaxExpr) {
			sb.append("new GRGEN_EXPR.ByteMax(");
			sb.append(")");
		} else if(expr instanceof ShortMinExpr) {
			sb.append("new GRGEN_EXPR.ShortMin(");
			sb.append(")");
		} else if(expr instanceof ShortMaxExpr) {
			sb.append("new GRGEN_EXPR.ShortMax(");
			sb.append(")");
		} else if(expr instanceof IntMinExpr) {
			sb.append("new GRGEN_EXPR.IntMin(");
			sb.append(")");
		} else if(expr instanceof IntMaxExpr) {
			sb.append("new GRGEN_EXPR.IntMax(");
			sb.append(")");
		} else if(expr instanceof LongMinExpr) {
			sb.append("new GRGEN_EXPR.LongMin(");
			sb.append(")");
		} else if(expr instanceof LongMaxExpr) {
			sb.append("new GRGEN_EXPR.LongMax(");
			sb.append(")");
		} else if(expr instanceof FloatMinExpr) {
			sb.append("new GRGEN_EXPR.FloatMin(");
			sb.append(")");
		} else if(expr instanceof FloatMaxExpr) {
			sb.append("new GRGEN_EXPR.FloatMax(");
			sb.append(")");
		} else if(expr instanceof DoubleMinExpr) {
			sb.append("new GRGEN_EXPR.DoubleMin(");
			sb.append(")");
		} else if(expr instanceof DoubleMaxExpr) {
			sb.append("new GRGEN_EXPR.DoubleMax(");
			sb.append(")");
		} else if(expr instanceof CeilExpr) {
			CeilExpr c = (CeilExpr)expr;
			sb.append("new GRGEN_EXPR.Ceil(");
			genExpressionTree(sb, c.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof FloorExpr) {
			FloorExpr f = (FloorExpr)expr;
			sb.append("new GRGEN_EXPR.Floor(");
			genExpressionTree(sb, f.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof RoundExpr) {
			RoundExpr r = (RoundExpr)expr;
			sb.append("new GRGEN_EXPR.Round(");
			genExpressionTree(sb, r.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof TruncateExpr) {
			TruncateExpr t = (TruncateExpr)expr;
			sb.append("new GRGEN_EXPR.Truncate(");
			genExpressionTree(sb, t.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SinCosTanExpr) {
			SinCosTanExpr sct = (SinCosTanExpr)expr;
			switch(sct.getWhich()) {
			case sin:
				sb.append("new GRGEN_EXPR.Sin(");
				break;
			case cos:
				sb.append("new GRGEN_EXPR.Cos(");
				break;
			case tan:
				sb.append("new GRGEN_EXPR.Tan(");
				break;
			}
			genExpressionTree(sb, sct.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof ArcSinCosTanExpr) {
			ArcSinCosTanExpr asct = (ArcSinCosTanExpr)expr;
			switch(asct.getWhich()) {
			case arcsin:
				sb.append("new GRGEN_EXPR.ArcSin(");
				break;
			case arccos:
				sb.append("new GRGEN_EXPR.ArcCos(");
				break;
			case arctan:
				sb.append("new GRGEN_EXPR.ArcTan(");
				break;
			}
			genExpressionTree(sb, asct.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof CanonizeExpr) {
			CanonizeExpr c = (CanonizeExpr)expr;
			sb.append("new GRGEN_EXPR.Canonize(");
			genExpressionTree(sb, c.getGraphExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SqrExpr) {
			SqrExpr s = (SqrExpr)expr;
			sb.append("new GRGEN_EXPR.Sqr(");
			genExpressionTree(sb, s.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof SqrtExpr) {
			SqrtExpr s = (SqrtExpr)expr;
			sb.append("new GRGEN_EXPR.Sqrt(");
			genExpressionTree(sb, s.getExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof PowExpr) {
			PowExpr p = (PowExpr)expr;
			sb.append("new GRGEN_EXPR.Pow(");
			if(p.getLeftExpr() != null) {
				genExpressionTree(sb, p.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
				sb.append(", ");
			}
			genExpressionTree(sb, p.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(")");
		} else if(expr instanceof LogExpr) {
			LogExpr l = (LogExpr)expr;
			sb.append("new GRGEN_EXPR.Log(");
			genExpressionTree(sb, l.getLeftExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			if(l.getRightExpr() != null) {
				sb.append(", ");
				genExpressionTree(sb, l.getRightExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			}
			sb.append(")");
		} else if(expr instanceof IteratedQueryExpr) {
			IteratedQueryExpr iq = (IteratedQueryExpr)expr;
			sb.append("new GRGEN_EXPR.IteratedQuery(");
			sb.append("\"" + iq.getIteratedName().toString() + "\"");
			sb.append(")");
		} else
			throw new UnsupportedOperationException("Unsupported expression type (" + expr + ")");
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

	////////////////////////////////////
	// Yielding assignment generation //
	////////////////////////////////////

	public void genYield(SourceBuilder sb, EvalStatement evalStmt, String className,
			String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(evalStmt instanceof AssignmentVarIndexed) { // must come before AssignmentVar
			genAssignmentVarIndexed(sb, (AssignmentVarIndexed)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof AssignmentVar) {
			genAssignmentVar(sb, (AssignmentVar)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof AssignmentGraphEntity) {
			genAssignmentGraphEntity(sb, (AssignmentGraphEntity)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof AssignmentIdentical) {
			//nothing to generate, was assignment . = . optimized away;
		} else if(evalStmt instanceof CompoundAssignmentVarChangedVar) {
			genCompoundAssignmentVarChangedVar(sb, (CompoundAssignmentVarChangedVar)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof CompoundAssignmentVar) { // must come after the changed versions
			genCompoundAssignmentVar(sb, (CompoundAssignmentVar)evalStmt, "\t\t\t\t",
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof MapVarRemoveItem) {
			genMapVarRemoveItem(sb, (MapVarRemoveItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof MapVarClear) {
			genMapVarClear(sb, (MapVarClear)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof MapVarAddItem) {
			genMapVarAddItem(sb, (MapVarAddItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof SetVarRemoveItem) {
			genSetVarRemoveItem(sb, (SetVarRemoveItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof SetVarClear) {
			genSetVarClear(sb, (SetVarClear)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof SetVarAddItem) {
			genSetVarAddItem(sb, (SetVarAddItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ArrayVarRemoveItem) {
			genArrayVarRemoveItem(sb, (ArrayVarRemoveItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ArrayVarClear) {
			genArrayVarClear(sb, (ArrayVarClear)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ArrayVarAddItem) {
			genArrayVarAddItem(sb, (ArrayVarAddItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DequeVarRemoveItem) {
			genDequeVarRemoveItem(sb, (DequeVarRemoveItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DequeVarClear) {
			genDequeVarClear(sb, (DequeVarClear)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DequeVarAddItem) {
			genDequeVarAddItem(sb, (DequeVarAddItem)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof IteratedAccumulationYield) {
			genIteratedAccumulationYield(sb, (IteratedAccumulationYield)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ContainerAccumulationYield) {
			genContainerAccumulationYield(sb, (ContainerAccumulationYield)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof IntegerRangeIterationYield) {
			genIntegerRangeIterationYield(sb, (IntegerRangeIterationYield)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ForFunction) {
			genForFunction(sb, (ForFunction)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ForIndexAccessEquality) {
			genForIndexAccessEquality(sb, (ForIndexAccessEquality)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ForIndexAccessOrdering) {
			genForIndexAccessOrdering(sb, (ForIndexAccessOrdering)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ConditionStatement) {
			genConditionStatement(sb, (ConditionStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof SwitchStatement) {
			genSwitchStatement(sb, (SwitchStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof WhileStatement) {
			genWhileStatement(sb, (WhileStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DoWhileStatement) {
			genDoWhileStatement(sb, (DoWhileStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof MultiStatement) {
			genMultiStatement(sb, (MultiStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DefDeclVarStatement) {
			genDefDeclVarStatement(sb, (DefDeclVarStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DefDeclGraphEntityStatement) {
			genDefDeclGraphEntityStatement(sb, (DefDeclGraphEntityStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof BreakStatement) {
			genBreakStatement(sb, (BreakStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ContinueStatement) {
			genContinueStatement(sb, (ContinueStatement)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof EmitProc) {
			genEmitProc(sb, (EmitProc)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DebugAddProc) {
			genDebugAddProc(sb, (DebugAddProc)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DebugRemProc) {
			genDebugRemProc(sb, (DebugRemProc)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DebugEmitProc) {
			genDebugEmitProc(sb, (DebugEmitProc)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DebugHaltProc) {
			genDebugHaltProc(sb, (DebugHaltProc)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof DebugHighlightProc) {
			genDebugHighlightProc(sb, (DebugHighlightProc)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof RecordProc) {
			genRecordProc(sb, (RecordProc)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof ReturnAssignment) {
			genYield(sb, ((ReturnAssignment)evalStmt).getProcedureInvocation(),
					className, pathPrefix, alreadyDefinedEntityToName);
		} else if(evalStmt instanceof IteratedFiltering) {
			genIteratedFiltering(sb, (IteratedFiltering)evalStmt,
					className, pathPrefix, alreadyDefinedEntityToName);
		} else {
			throw new UnsupportedOperationException("Unexpected yield statement \"" + evalStmt + "\"");
		}
	}

	private void genAssignmentVar(SourceBuilder sb, AssignmentVar ass,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
		if(cass.getChangedOperation() == CompoundAssignment.UNION)
			changedOperation = "GRGEN_EXPR.YieldChangeDisjunctionAssignment";
		else if(cass.getChangedOperation() == CompoundAssignment.INTERSECTION)
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
		if(cass.getOperation() == CompoundAssignment.UNION)
			sb.append("SetMapUnion(");
		else if(cass.getOperation() == CompoundAssignment.INTERSECTION)
			sb.append("SetMapIntersect(");
		else //if(cass.getOperation()==CompoundAssignment.WITHOUT)
			sb.append("SetMapExcept(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(", ");
		genExpressionTree(sb, expr, className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}

	private void genMapVarRemoveItem(SourceBuilder sb, MapVarRemoveItem mvri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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

		assert mvri.getNext() == null;
	}

	private void genMapVarClear(SourceBuilder sb, MapVarClear mvc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable target = mvc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");

		assert mvc.getNext() == null;
	}

	private void genMapVarAddItem(SourceBuilder sb, MapVarAddItem mvai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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

		assert mvai.getNext() == null;
	}

	private void genSetVarRemoveItem(SourceBuilder sb, SetVarRemoveItem svri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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

		assert svri.getNext() == null;
	}

	private void genSetVarClear(SourceBuilder sb, SetVarClear svc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable target = svc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");

		assert svc.getNext() == null;
	}

	private void genSetVarAddItem(SourceBuilder sb, SetVarAddItem svai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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

		assert svai.getNext() == null;
	}

	private void genArrayVarRemoveItem(SourceBuilder sb, ArrayVarRemoveItem avri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable target = avri.getTarget();

		sb.appendFront("new GRGEN_EXPR.ArrayRemove(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		if(avri.getIndexExpr() != null) {
			sb.append(", ");
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, avri.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(indexExprStr);
		}
		sb.append(")");

		assert avri.getNext() == null;
	}

	private void genArrayVarClear(SourceBuilder sb, ArrayVarClear avc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable target = avc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");

		assert avc.getNext() == null;
	}

	private void genArrayVarAddItem(SourceBuilder sb, ArrayVarAddItem avai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
		if(avai.getIndexExpr() != null) {
			sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, avai.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(", ");
			sb.append(indexExprStr);
		}
		sb.append(")");

		assert avai.getNext() == null;
	}

	private void genDequeVarRemoveItem(SourceBuilder sb, DequeVarRemoveItem dvri,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable target = dvri.getTarget();

		sb.appendFront("new GRGEN_EXPR.DequeRemove(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		if(dvri.getIndexExpr() != null) {
			sb.append(", ");
			SourceBuilder sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, dvri.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(indexExprStr);
		}
		sb.append(")");

		assert dvri.getNext() == null;
	}

	private void genDequeVarClear(SourceBuilder sb, DequeVarClear dvc,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable target = dvc.getTarget();

		sb.appendFront("new GRGEN_EXPR.Clear(");
		sb.append("\"" + formatEntity(target, pathPrefix, alreadyDefinedEntityToName) + "\"");
		sb.append(")");

		assert dvc.getNext() == null;
	}

	private void genDequeVarAddItem(SourceBuilder sb, DequeVarAddItem dvai,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
		if(dvai.getIndexExpr() != null) {
			sbtmp = new SourceBuilder();
			genExpressionTree(sbtmp, dvai.getIndexExpr(), className, pathPrefix, alreadyDefinedEntityToName);
			String indexExprStr = sbtmp.toString();
			sb.append(", ");
			sb.append(indexExprStr);
		}
		sb.append(")");

		assert dvai.getNext() == null;
	}

	private void genIteratedAccumulationYield(SourceBuilder sb, IteratedAccumulationYield iay,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
		if(indexVar != null) {
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable iterationVar = fiae.getIterationVar();
		Type iterationVarType = iterationVar.getType();

		sb.appendFront("new GRGEN_EXPR.ForIndexAccessEquality(");
		sb.append("\"GRGEN_MODEL." + model.getIdent() + "IndexSet\", ");
		sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\""
				+ fiae.getIndexAcccessEquality().index.getIdent() + "\"), ");

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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable iterationVar = fiao.getIterationVar();
		Type iterationVarType = iterationVar.getType();

		sb.appendFront("new GRGEN_EXPR.ForIndexAccessOrdering(");
		sb.append("\"GRGEN_MODEL." + model.getIdent() + "IndexSet\", ");
		sb.append("GRGEN_MODEL." + model.getIdent() + "GraphModel.GetIndexDescription(\""
				+ fiao.getIndexAccessOrdering().index.getIdent() + "\"), ");

		sb.append("\"" + formatEntity(iterationVar, pathPrefix, alreadyDefinedEntityToName) + "\", ");
		sb.append("\"" + formatIdentifiable(iterationVar) + "\", ");
		sb.append("\"" + formatElementInterfaceRef(iterationVarType) + "\", ");

		sb.append(fiao.getIndexAccessOrdering().ascending ? "true, " : "false, ");
		sb.append(fiao.getIndexAccessOrdering().includingFrom() ? "true, " : "false, ");
		sb.append(fiao.getIndexAccessOrdering().includingTo() ? "true, " : "false, ");
		if(fiao.getIndexAccessOrdering().from() != null)
			genExpressionTree(sb, fiao.getIndexAccessOrdering().from(), className, pathPrefix,
					alreadyDefinedEntityToName);
		else
			sb.append("null");
		sb.append(", ");
		if(fiao.getIndexAccessOrdering().to() != null)
			genExpressionTree(sb, fiao.getIndexAccessOrdering().to(), className, pathPrefix,
					alreadyDefinedEntityToName);
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		sb.appendFront("new GRGEN_EXPR.ConditionStatement(");
		genExpressionTree(sb, cs.getConditionExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(",");
		sb.append("new GRGEN_EXPR.Yielding[] { ");
		for(EvalStatement statement : cs.getTrueCaseStatements()) {
			genYield(sb, statement, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}, ");
		if(cs.getFalseCaseStatements() != null) {
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		Variable var = ddvs.getTarget();
		sb.appendFront("new GRGEN_EXPR.DefDeclaration(");
		sb.append("\"" + formatEntity(var, pathPrefix, alreadyDefinedEntityToName) + "\",");
		sb.append("\"" + formatType(var.getType()) + "\",");
		if(var.initialization != null) {
			genExpressionTree(sb, var.initialization, className, pathPrefix, alreadyDefinedEntityToName);
		} else {
			sb.append("null");
		}
		sb.append(")");
	}

	private void genDefDeclGraphEntityStatement(SourceBuilder sb, DefDeclGraphEntityStatement ddges,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		GraphEntity graphEntity = ddges.getTarget();
		sb.appendFront("new GRGEN_EXPR.DefDeclaration(");
		sb.append("\"" + formatEntity(graphEntity, pathPrefix, alreadyDefinedEntityToName) + "\",");
		sb.append("\"" + formatType(graphEntity.getType()) + "\",");
		if(graphEntity.initialization != null) {
			genExpressionTree(sb, graphEntity.initialization, className, pathPrefix, alreadyDefinedEntityToName);
		} else {
			sb.append("null");
		}
		sb.append(")");
	}

	private void genBreakStatement(SourceBuilder sb, BreakStatement bs,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		sb.appendFront("new GRGEN_EXPR.BreakStatement()");
	}

	private void genContinueStatement(SourceBuilder sb, ContinueStatement cs,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		sb.appendFront("new GRGEN_EXPR.ContinueStatement()");
	}

	private void genEmitProc(SourceBuilder sb, EmitProc ep,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
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
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		sb.appendFront("new GRGEN_EXPR.RecordStatement(");
		genExpressionTree(sb, rp.getToRecordExpr(), className, pathPrefix, alreadyDefinedEntityToName);
		sb.append(")");
	}

	private void genIteratedFiltering(SourceBuilder sb, IteratedFiltering itf,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		sb.appendFront("new GRGEN_EXPR.IteratedFiltering(");
		sb.append("\"" + itf.getActionOrSubpattern().getIdent() + "\", ");
		sb.append("\"" + itf.getIterated().getIdent() + "\", ");
		sb.append("new GRGEN_EXPR.FilterInvocation[] {");
		for(int i = 0; i < itf.getFilterInvocations().size(); ++i) {
			FilterInvocation filterInvocation = itf.getFilterInvocation(i);
			genFilterInvocation(sb, filterInvocation, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("} ");
		sb.append(")");
	}

	private void genFilterInvocation(SourceBuilder sb, FilterInvocation fi,
			String className, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		FilterAutoSupplied fas = fi.getFilterAutoSupplied();
		FilterAutoGenerated fag = fi.getFilterAutoGenerated();
		sb.append("new GRGEN_EXPR.FilterInvocation(");
		sb.append("\"" + (fas != null ? fas.getFilterName() : fag.getFilterName() + fag.getUnderscoreSuffix()) + "\", ");
		sb.append((fas != null ? "true" : "false") + ", ");
		sb.append("new GRGEN_EXPR.Expression[] {");
		for(int i = 0; i < fi.getFilterArguments().size(); ++i) {
			Expression argument = fi.getFilterArgument(i);
			genExpressionTree(sb, argument, className, pathPrefix, alreadyDefinedEntityToName);
			sb.append(", ");
		}
		sb.append("}, ");
		sb.append("new String[] {");
		for(int i = 0; i < fi.getFilterArguments().size(); ++i) {
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

	private Model model;
}
