/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.numeric;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.numeric.MaxExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MaxExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(MaxExprNode.class, "max expr");
	}

	private ExprNode leftExpr;
	private ExprNode rightExpr;

	public MaxExprNode(Coords coords, ExprNode leftExpr, ExprNode rightExpr)
	{
		super(coords);

		this.leftExpr = becomeParent(leftExpr);
		this.rightExpr = becomeParent(rightExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(leftExpr);
		children.add(rightExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("left");
		childrenNames.add("right");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(leftExpr.getType().isNumericType() && rightExpr.getType().isEqual(leftExpr.getType()))
			return true;
		reportError("valid types for max(.,.) are: " + TypeNode.getNumericTypesAsString() + " (pairs of same type)");
		return false;
	}

	@Override
	protected IR constructIR()
	{
		return new MaxExpr(leftExpr.checkIR(Expression.class), rightExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return leftExpr.getType();
	}
}
