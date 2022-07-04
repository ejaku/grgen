/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.numeric.PowExpr;
import de.unika.ipd.grgen.parser.Coords;

public class PowExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(PowExprNode.class, "pow expr");
	}

	private ExprNode leftExpr;
	private ExprNode rightExpr;

	public PowExprNode(Coords coords, ExprNode leftExpr, ExprNode rightExpr)
	{
		super(coords);

		this.leftExpr = becomeParent(leftExpr);
		this.rightExpr = becomeParent(rightExpr);
	}

	public PowExprNode(Coords coords, ExprNode rightExpr)
	{
		super(coords);

		this.rightExpr = becomeParent(rightExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(leftExpr != null)
			children.add(leftExpr);
		children.add(rightExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		if(leftExpr != null)
			childrenNames.add("left");
		childrenNames.add("right");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!rightExpr.getType().isEqual(BasicTypeNode.doubleType)) {
			reportError("The function pow() expects as 1. argument a value of type double"
					+ " (but is given a value of type " + rightExpr.getType() + ").");
			return false;
		}
		if(leftExpr != null && !leftExpr.getType().isEqual(BasicTypeNode.doubleType)) {
			reportError("The function pow() expects as 2. argument a value of type double"
					+ " (but is given a value of type " + leftExpr.getType() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		rightExpr = rightExpr.evaluate();
		if(leftExpr != null) {
			leftExpr = leftExpr.evaluate();
			return new PowExpr(leftExpr.checkIR(Expression.class), rightExpr.checkIR(Expression.class));
		} else
			return new PowExpr(rightExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.doubleType;
	}
}
