/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.deque;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.deque.DequeAsSetExpr;
import de.unika.ipd.grgen.parser.Coords;

public class DequeAsSetNode extends DequeFunctionMethodInvocationBaseExprNode
{
	static {
		setName(DequeAsSetNode.class, "deque as set expression");
	}

	private SetTypeNode setTypeNode;

	public DequeAsSetNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean resolveLocal()
	{
		// target type already checked during resolving into this node
		setTypeNode = new SetTypeNode(getTargetType().valueTypeUnresolved);
		return setTypeNode.resolve();
	}

	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new DequeAsSetExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
