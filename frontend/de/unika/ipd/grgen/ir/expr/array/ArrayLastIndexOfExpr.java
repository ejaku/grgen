/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class ArrayLastIndexOfExpr extends ArrayFunctionMethodInvocationBaseExpr
{
	private Expression valueExpr;
	private Expression startIndexExpr;

	public ArrayLastIndexOfExpr(Expression targetExpr, Expression valueExpr)
	{
		super("array lastIndexOf expr", IntType.getType(), targetExpr);
		this.valueExpr = valueExpr;
	}

	public ArrayLastIndexOfExpr(Expression targetExpr, Expression valueExpr, Expression startIndexExpr)
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

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
		if(startIndexExpr != null)
			startIndexExpr.collectNeededEntities(needs);
	}
}
