/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.basic.DoubleType;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class RandomExpr extends BuiltinFunctionInvocationExpr
{
	private Expression numExpr;

	public RandomExpr(Expression numExpr)
	{
		super("random", numExpr == null ? DoubleType.getType() : IntType.getType());
		this.numExpr = numExpr;
	}

	public Expression getNumExpr()
	{
		return numExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(numExpr != null)
			numExpr.collectNeededEntities(needs);
	}
}
