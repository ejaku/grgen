/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;

public class MapAsArrayExpr extends Expression
{
	Expression targetExpr;

	public MapAsArrayExpr(Expression targetExpr, Type targetType)
	{
		super("map as array expression", targetType);
		this.targetExpr = targetExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
	}

	public Expression getTargetExpr()
	{
		return targetExpr;
	}
}
