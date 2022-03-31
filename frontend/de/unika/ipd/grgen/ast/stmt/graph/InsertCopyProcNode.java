/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.InsertCopyProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for inserting a copy of the subgraph to the given main graph.
 */
public class InsertCopyProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(InsertCopyProcNode.class, "insert copy procedure");
	}

	private ExprNode graphExpr;
	private ExprNode nodeExpr;

	Vector<TypeNode> returnTypes;

	public InsertCopyProcNode(Coords coords, ExprNode nodeSetExpr, ExprNode nodeExpr)
	{
		super(coords);
		this.graphExpr = nodeSetExpr;
		becomeParent(this.graphExpr);
		this.nodeExpr = nodeExpr;
		becomeParent(this.nodeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(graphExpr);
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
		if(!(graphExpr.getType().equals(BasicTypeNode.graphType))) {
			reportError("first argument of insertCopy(.,.) must be of graph type (the subgraph to insert into the current graph)");
			return false;
		}
		if(!(nodeExpr.getType() instanceof NodeTypeNode)) {
			nodeExpr.reportError("node expected as 2nd argument to insertCopy");
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
		graphExpr = graphExpr.evaluate();
		nodeExpr = nodeExpr.evaluate();
		InsertCopyProc insertCopy = new InsertCopyProc(graphExpr.checkIR(Expression.class),
				nodeExpr.checkIR(Expression.class), nodeExpr.getType().getType());
		return insertCopy;
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
