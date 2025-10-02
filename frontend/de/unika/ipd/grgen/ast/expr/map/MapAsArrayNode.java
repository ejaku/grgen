/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.map;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.map.MapAsArrayExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapAsArrayNode extends MapFunctionMethodInvocationBaseExprNode
{
	static {
		setName(MapAsArrayNode.class, "map as array expression");
	}

	private ArrayTypeNode arrayTypeNode;

	public MapAsArrayNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean resolveLocal()
	{
		// target type already checked during resolving into this node
		arrayTypeNode = new ArrayTypeNode(getTargetType().valueTypeUnresolved);
		return arrayTypeNode.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		MapTypeNode targetMapType = getTargetType();
		if(!(targetMapType.keyType instanceof IntTypeNode)) {
			targetExpr.reportError("The map function method asArray can only be employed on an object of type map<int,T>"
					+ " (but is employed on an object of type " + targetMapType.getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return arrayTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new MapAsArrayExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
