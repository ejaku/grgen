/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsMapExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayAsMapNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayAsMapNode.class, "array as map expression");
	}

	private MapTypeNode mapTypeNode;

	public ArrayAsMapNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean resolveLocal()
	{
		// target type already checked during resolving into this node
		mapTypeNode = new MapTypeNode(BasicTypeNode.intType.getIdentNode(),
				getTargetType().valueTypeUnresolved);
		return mapTypeNode.resolve();
	}

	@Override
	public TypeNode getType()
	{
		return mapTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new ArrayAsMapExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
