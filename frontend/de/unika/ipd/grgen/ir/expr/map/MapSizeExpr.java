/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.map;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class MapSizeExpr extends MapFunctionMethodInvocationBaseExpr
{
	public MapSizeExpr(Expression targetExpr)
	{
		super("map size expression", IntType.getType(), targetExpr);
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
	}
}
