/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class DequePeekExpr extends DequeFunctionMethodInvocationBaseExpr
{
	private Expression numberExpr;

	public DequePeekExpr(Expression targetExpr, Expression numberExpr)
	{
		super("deque peek expr", ((DequeType)(targetExpr.getType())).valueType, targetExpr);
		this.numberExpr = numberExpr;
	}

	public Expression getNumberExpr()
	{
		return numberExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
		if(numberExpr != null)
			numberExpr.collectNeededEntities(needs);
	}
}
