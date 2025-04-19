/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
		if(!leftExpr.getType().isNumericType())
		{
			reportError("The function Math::max() expects as 1. argument a value of type " + TypeNode.getNumericTypesAsString()
					+ " (but is given a value of type " + leftExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!rightExpr.getType().isNumericType())
		{
			reportError("The function Math::max() expects as 2. argument a value of type " + TypeNode.getNumericTypesAsString()
					+ " (but is given a value of type " + rightExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!rightExpr.getType().isEqual(leftExpr.getType()))
		{
			reportError("The function Math::max() expects the 1. and 2. argument to be of the same type"
			+ " (but they are values of type " + leftExpr.getType().getTypeName() + " and " + rightExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		leftExpr = leftExpr.evaluate();
		rightExpr = rightExpr.evaluate();
		return new MaxExpr(leftExpr.checkIR(Expression.class), rightExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return leftExpr.getType();
	}
}
