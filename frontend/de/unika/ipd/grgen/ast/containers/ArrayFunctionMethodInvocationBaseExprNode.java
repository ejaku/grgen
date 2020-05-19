/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import de.unika.ipd.grgen.ast.TypeNode;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ArrayFunctionMethodInvocationBaseExprNode extends ContainerFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayFunctionMethodInvocationBaseExprNode.class,
				"array function method invocation base expression");
	}

	public ArrayFunctionMethodInvocationBaseExprNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}
	
	@Override
	protected ArrayTypeNode getTargetType()
	{
		TypeNode targetType = targetExpr.getType();
		return (ArrayTypeNode)targetType;
	}
}
