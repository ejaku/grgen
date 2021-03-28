/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.expr.deque;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class DequeLastIndexOfExpr extends DequeFunctionMethodInvocationBaseExpr
{
	private Expression valueExpr;
	private Expression startIndexExpr;

	public DequeLastIndexOfExpr(Expression targetExpr, Expression valueExpr)
	{
		super("deque lastIndexOf expr", IntType.getType(), targetExpr);
		this.valueExpr = valueExpr;
	}

	public DequeLastIndexOfExpr(Expression targetExpr, Expression valueExpr, Expression startIndexExpr)
	{
		super("deque lastIndexOf expr", IntType.getType(), targetExpr);
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
