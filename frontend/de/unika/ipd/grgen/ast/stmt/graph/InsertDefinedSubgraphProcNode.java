/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.InsertDefinedSubgraphProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding an inserted edge of the insertion of a defined subgraph of an edge set.
 */
public class InsertDefinedSubgraphProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(InsertDefinedSubgraphProcNode.class, "insert defined subgraph procedure");
	}

	private ExprNode edgeSetExpr;
	private ExprNode edgeExpr;

	Vector<TypeNode> returnTypes;

	public InsertDefinedSubgraphProcNode(Coords coords, ExprNode edgeSetExpr, ExprNode edgeExpr)
	{
		super(coords);
		this.edgeSetExpr = edgeSetExpr;
		becomeParent(this.edgeSetExpr);
		this.edgeExpr = edgeExpr;
		becomeParent(this.edgeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeSetExpr);
		children.add(edgeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edgeSetExpr");
		childrenNames.add("edgeExpr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode edgeSetExprType = edgeSetExpr.getType();
		if(!(edgeSetExprType instanceof SetTypeNode)) {
			edgeSetExpr.reportError("The insertDefinedSubgraph procedure expects as 1. argument (setOfEdges)"
					+ " a value of type set<AEdge> or set<Edge> or set<UEdge>"
					+ " (but is given a value of type " + edgeSetExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		SetTypeNode type = (SetTypeNode)edgeSetExprType;
		if(!(type.valueType instanceof EdgeTypeNode)) {
			edgeSetExpr.reportError("The insertDefinedSubgraph procedure expects as 1. argument (setOfEdges)"
					+ " a value of type set<AEdge> or set<Edge> or set<UEdge>"
					+ " (but is given a value of type " + edgeSetExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		EdgeTypeNode edgeValueType = (EdgeTypeNode)type.valueType;
		if(edgeValueType != EdgeTypeNode.arbitraryEdgeType
				&& edgeValueType != EdgeTypeNode.directedEdgeType
				&& edgeValueType != EdgeTypeNode.undirectedEdgeType) {
			edgeSetExpr.reportError("The insertDefinedSubgraph procedure expects as 1. argument (setOfEdges)"
					+ " a value of type set<AEdge> or set<Edge> or set<UEdge>"
					+ " (but is given a value of type " + edgeSetExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode edgeExprType = edgeExpr.getType();
		if(!(edgeExprType instanceof EdgeTypeNode)) {
			edgeExpr.reportError("The insertDefinedSubgraph procedure expects as 2. argument (edge)"
					+ " a value of type AEdge or Edge or UEdge"
					+ " (but is given a value of type " + edgeExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		edgeSetExpr = edgeSetExpr.evaluate();
		edgeExpr = edgeExpr.evaluate();
		InsertDefinedSubgraphProc insertDefined = new InsertDefinedSubgraphProc(
				edgeSetExpr.checkIR(Expression.class), edgeExpr.checkIR(Expression.class),
				edgeExpr.getType().getType());
		return insertDefined;
	}

	@Override
	public Vector<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(edgeExpr.getType());
		}
		return returnTypes;
	}
}
