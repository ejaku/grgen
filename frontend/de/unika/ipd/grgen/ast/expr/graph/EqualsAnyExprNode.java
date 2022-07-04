/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.GraphTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EqualsAnyExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node comparing a subgraph against a subgraph set.
 */
public class EqualsAnyExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(EqualsAnyExprNode.class, "equals any expr");
	}

	private ExprNode subgraphExpr;
	private ExprNode subgraphSetExpr;
	private boolean includingAttributes;

	public EqualsAnyExprNode(Coords coords, ExprNode subgraphExpr,
			ExprNode subgraphSetExpr, boolean includingAttributes)
	{
		super(coords);
		this.subgraphExpr = subgraphExpr;
		becomeParent(this.subgraphExpr);
		this.subgraphSetExpr = subgraphSetExpr;
		becomeParent(this.subgraphSetExpr);
		this.includingAttributes = includingAttributes;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(subgraphExpr);
		children.add(subgraphSetExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subgraphExpr");
		childrenNames.add("subgraphSetExpr");
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
		if(!(subgraphExpr.getType() instanceof GraphTypeNode)) {
			subgraphExpr.reportError("The function equalsAny expects as 1. argument (subgraphToCompare) a value of type graph"
					+ " (but is given a value of type " + subgraphExpr.getType() + ").");
			return false;
		}
		if(!(subgraphSetExpr.getType() instanceof SetTypeNode)) {
			subgraphSetExpr.reportError("The function equalsAny expects as 2. argument (setOfSubgraphsToCompareAgainst) a value of type set"
					+ " (but is given a value of type " + subgraphSetExpr.getType() + ").");
			return false;
		}
		SetTypeNode type = (SetTypeNode)subgraphSetExpr.getType();
		if(!(type.valueType instanceof GraphTypeNode)) {
			subgraphSetExpr.reportError("The function equalsAny expects as 2. argument (setOfSubgraphsToCompareAgainst) a value of type set<graph>"
					+ " (but is given a value of type " + subgraphSetExpr.getType() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		subgraphExpr = subgraphExpr.evaluate();
		subgraphSetExpr = subgraphSetExpr.evaluate();
		return new EqualsAnyExpr(subgraphExpr.checkIR(Expression.class),
				subgraphSetExpr.checkIR(Expression.class),
				includingAttributes, getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.booleanType;
	}
}
