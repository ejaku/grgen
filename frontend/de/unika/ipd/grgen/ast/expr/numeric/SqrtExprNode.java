/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
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
import de.unika.ipd.grgen.ir.expr.numeric.SqrtExpr;
import de.unika.ipd.grgen.parser.Coords;

public class SqrtExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(SqrtExprNode.class, "sqrt expr");
	}

	private ExprNode expr;

	public SqrtExprNode(Coords coords, ExprNode expr)
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
		reportError("valid types for sqrt(.) are: double");
		return false;
	}

	@Override
	protected IR constructIR()
	{
		expr = expr.evaluate();
		return new SqrtExpr(expr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.doubleType;
	}
}
