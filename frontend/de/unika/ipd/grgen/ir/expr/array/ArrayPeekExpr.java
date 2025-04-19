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
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayPeekExpr extends ArrayFunctionMethodInvocationBaseExpr
{
	private Expression numberExpr;

	public ArrayPeekExpr(Expression targetExpr, Expression numberExpr)
	{
		super("array peek expr", ((ArrayType)(targetExpr.getType())).valueType, targetExpr);
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
		if(numberExpr != null)
			numberExpr.collectNeededEntities(needs);
	}
}
