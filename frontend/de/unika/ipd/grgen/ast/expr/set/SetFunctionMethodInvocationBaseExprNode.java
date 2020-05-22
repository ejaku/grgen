/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.set;

import de.unika.ipd.grgen.ast.expr.ContainerFunctionMethodInvocationBaseExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.SetTypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class SetFunctionMethodInvocationBaseExprNode extends ContainerFunctionMethodInvocationBaseExprNode
{
	static {
		setName(SetFunctionMethodInvocationBaseExprNode.class,
				"set function method invocation base expression");
	}

	public SetFunctionMethodInvocationBaseExprNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected SetTypeNode getTargetType()
	{
		return (SetTypeNode)super.getTargetType();
	}
}
