/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.map;

import de.unika.ipd.grgen.ir.expr.ContainerFunctionMethodInvocationBaseExpr;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.MapType;

public abstract class MapFunctionMethodInvocationBaseExpr extends ContainerFunctionMethodInvocationBaseExpr
{
	protected MapFunctionMethodInvocationBaseExpr(String name, Type type, Expression targetExpr)
	{
		super(name, type, targetExpr);
	}
	
	@Override
	public MapType getTargetType()
	{
		return (MapType)super.getTargetType();
	}
}
