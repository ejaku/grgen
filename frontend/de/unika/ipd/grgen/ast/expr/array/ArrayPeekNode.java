/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayPeekExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayPeekNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayPeekNode.class, "array peek");
	}

	private ExprNode numberExpr;

	public ArrayPeekNode(Coords coords, ExprNode targetExpr, ExprNode numberExpr)
	{
		super(coords, targetExpr);
		this.numberExpr = becomeParent(numberExpr);
	}

	public ArrayPeekNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		if(numberExpr != null)
			children.add(numberExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		if(numberExpr != null)
			childrenNames.add("numberExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		if(numberExpr != null && !numberExpr.getType().isEqual(BasicTypeNode.intType)) {
			numberExpr.reportError("The array function method peek expects as argument (number) a value of type int"
					+ " (but is given a value of type " + numberExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return getTargetType().valueType;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		if(numberExpr != null)
			numberExpr = numberExpr.evaluate();
		return new ArrayPeekExpr(targetExpr.checkIR(Expression.class),
				numberExpr != null ? numberExpr.checkIR(Expression.class) : null);
	}
}
