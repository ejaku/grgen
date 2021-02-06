/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.set;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.SetType;

public class SetPeekExpr extends SetFunctionMethodInvocationBaseExpr
{
	private Expression numberExpr;

	public SetPeekExpr(Expression targetExpr, Expression numberExpr)
	{
		super("set peek expr", ((SetType)(targetExpr.getType())).valueType, targetExpr);
		this.numberExpr = numberExpr;
	}

	public Expression getNumberExpr()
	{
		return numberExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		numberExpr.collectNeededEntities(needs);
	}
}
