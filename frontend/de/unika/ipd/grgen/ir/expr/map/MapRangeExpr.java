/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.map;

import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;

public class MapRangeExpr extends MapFunctionMethodInvocationBaseExpr
{
	public MapRangeExpr(Expression targetExpr, Type targetType)
	{
		super("map range expression", targetType, targetExpr);
	}
}
