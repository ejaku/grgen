/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.InsertInducedSubgraphProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding an inserted node of the insertion of an induced subgraph of a node set.
 */
public class InsertInducedSubgraphProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(InsertInducedSubgraphProcNode.class, "insert induced subgraph procedure");
	}

	private ExprNode nodeSetExpr;
	private ExprNode nodeExpr;

	Vector<TypeNode> returnTypes;

	public InsertInducedSubgraphProcNode(Coords coords, ExprNode nodeSetExpr, ExprNode nodeExpr)
	{
		super(coords);
		this.nodeSetExpr = nodeSetExpr;
		becomeParent(this.nodeSetExpr);
		this.nodeExpr = nodeExpr;
		becomeParent(this.nodeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(nodeSetExpr);
		children.add(nodeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("nodeSetExpr");
		childrenNames.add("nodeExpr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode nodeSetExprType = nodeSetExpr.getType();
		if(!(nodeSetExprType instanceof SetTypeNode)) {
			nodeSetExpr.reportError("The insertInducedSubgraph procedure expects as 1. argument (setOfNodes)"
					+ " a value of type set<Node>"
					+ " (but is given a value of type " + nodeSetExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		SetTypeNode type = (SetTypeNode)nodeSetExprType;
		if(!(type.valueType instanceof NodeTypeNode)) {
			nodeSetExpr.reportError("The insertInducedSubgraph procedure expects as 1. argument (setOfNodes)"
					+ " a value of type set<Node>"
					+ " (but is given a value of type " + nodeSetExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode nodeExprType = nodeExpr.getType();
		if(!(nodeExprType instanceof NodeTypeNode)) {
			nodeExpr.reportError("The insertInducedSubgraph procedure expects as 2. argument (node)"
					+ " a value of type Node"
					+ " (but is given a value of type " + nodeExprType.toStringWithDeclarationCoords() + ").");
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
		nodeSetExpr = nodeSetExpr.evaluate();
		nodeExpr = nodeExpr.evaluate();
		InsertInducedSubgraphProc insertInduced = new InsertInducedSubgraphProc(
				nodeSetExpr.checkIR(Expression.class), nodeExpr.checkIR(Expression.class),
				nodeExpr.getType().getType());
		return insertInduced;
	}

	@Override
	public Vector<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(nodeExpr.getType());
		}
		return returnTypes;
	}
}
