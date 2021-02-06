/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
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
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.map.MapRangeExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapRangeNode extends MapFunctionMethodInvocationBaseExprNode
{
	static {
		setName(MapSizeNode.class, "map range expression");
	}

	private SetTypeNode setTypeNode;

	public MapRangeNode(Coords coords, ExprNode targetExpr)
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
		return new MapRangeExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
