/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.set;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.set.SetEmptyExpr;
import de.unika.ipd.grgen.parser.Coords;

public class SetEmptyNode extends SetFunctionMethodInvocationBaseExprNode
{
	static {
		setName(SetEmptyNode.class, "set empty expression");
	}

	public SetEmptyNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.booleanType;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new SetEmptyExpr(targetExpr.checkIR(Expression.class));
	}
}
