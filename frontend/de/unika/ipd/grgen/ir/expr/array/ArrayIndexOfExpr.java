/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.expr.array;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class ArrayIndexOfExpr extends ArrayFunctionMethodInvocationBaseExpr
{
	private Expression valueExpr;
	private Expression startIndexExpr;

	public ArrayIndexOfExpr(Expression targetExpr, Expression valueExpr)
	{
		super("array indexOf expr", IntType.getType(), targetExpr);
		this.valueExpr = valueExpr;
	}

	public ArrayIndexOfExpr(Expression targetExpr, Expression valueExpr, Expression startIndexExpr)
	{
		super("array indexOf expr", IntType.getType(), targetExpr);
		this.valueExpr = valueExpr;
		this.startIndexExpr = startIndexExpr;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	public Expression getStartIndexExpr()
	{
		return startIndexExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
		if(startIndexExpr != null)
			startIndexExpr.collectNeededEntities(needs);
	}
}
