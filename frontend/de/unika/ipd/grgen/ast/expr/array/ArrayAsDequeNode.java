/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsDequeExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayAsDequeNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayAsDequeNode.class, "array as deque expression");
	}

	private DequeTypeNode dequeTypeNode;

	public ArrayAsDequeNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean resolveLocal()
	{
		// target type already checked during resolving into this node
		dequeTypeNode = new DequeTypeNode(getTargetType().valueTypeUnresolved);
		return dequeTypeNode.resolve();
	}

	@Override
	public TypeNode getType()
	{
		return dequeTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new ArrayAsDequeExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
