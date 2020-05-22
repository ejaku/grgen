/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ContainerFunctionMethodInvocationBaseExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(ContainerFunctionMethodInvocationBaseExprNode.class,
				"container function method invocation base expression");
	}

	protected ExprNode targetExpr;

	public ContainerFunctionMethodInvocationBaseExprNode(Coords coords, ExprNode targetExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
	}
	
	protected ContainerTypeNode getTargetType()
	{
		TypeNode targetType = targetExpr.getType();
		return (ContainerTypeNode)targetType;
	}
}
