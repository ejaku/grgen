/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.map;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.map.MapEmptyExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapEmptyNode extends MapFunctionMethodInvocationBaseExprNode
{
	static {
		setName(MapEmptyNode.class, "map empty expression");
	}

	public MapEmptyNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
		this.targetExpr = becomeParent(targetExpr);
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
		return new MapEmptyExpr(targetExpr.checkIR(Expression.class));
	}
}
