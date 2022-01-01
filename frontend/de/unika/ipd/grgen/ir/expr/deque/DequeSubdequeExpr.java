/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.expr.deque;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.DequeType;

public class DequeSubdequeExpr extends DequeFunctionMethodInvocationBaseExpr
{
	private Expression startExpr;
	private Expression lengthExpr;

	public DequeSubdequeExpr(Expression targetExpr, Expression startExpr, Expression lengthExpr)
	{
		super("deque subdeque expr", (DequeType)targetExpr.getType(), targetExpr);
		this.startExpr = startExpr;
		this.lengthExpr = lengthExpr;
	}

	public Expression getStartExpr()
	{
		return startExpr;
	}

	public Expression getLengthExpr()
	{
		return lengthExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		startExpr.collectNeededEntities(needs);
		lengthExpr.collectNeededEntities(needs);
	}
}
