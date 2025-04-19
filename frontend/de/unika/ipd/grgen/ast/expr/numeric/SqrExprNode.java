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
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.numeric.SqrExpr;
import de.unika.ipd.grgen.parser.Coords;

public class SqrExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(SqrExprNode.class, "sqr expr");
	}

	private ExprNode expr;

	public SqrExprNode(Coords coords, ExprNode expr)
	{
		super(coords);

		this.expr = becomeParent(expr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(expr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("expr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(expr.getType().isEqual(BasicTypeNode.doubleType)) {
			return true;
		}
		reportError("The function Math::sqr() expects as argument a value of type double"
				+ " (but is given a value of type " + expr.getType().getTypeName() + ").");
		return false;
	}

	@Override
	protected IR constructIR()
	{
		expr = expr.evaluate();
		return new SqrExpr(expr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.doubleType;
	}
}
