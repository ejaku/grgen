/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.InducedSubgraphExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the induced subgraph of a node set.
 */
public class InducedSubgraphExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(InducedSubgraphExprNode.class, "induced subgraph expr");
	}

	private ExprNode nodeSetExpr;

	public InducedSubgraphExprNode(Coords coords, ExprNode nodeSetExpr)
	{
		super(coords);
		this.nodeSetExpr = nodeSetExpr;
		becomeParent(this.nodeSetExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(nodeSetExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("nodeSetExpr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(nodeSetExpr.getType() instanceof SetTypeNode)) {
			nodeSetExpr.reportError("set expected as argument to inducedSubgraph");
			return false;
		}
		SetTypeNode type = (SetTypeNode)nodeSetExpr.getType();
		if(!(type.valueType instanceof NodeTypeNode)) {
			nodeSetExpr.reportError("set of nodes expected as argument to inducedSubgraph");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		nodeSetExpr = nodeSetExpr.evaluate();
		return new InducedSubgraphExpr(nodeSetExpr.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.graphType;
	}
}
