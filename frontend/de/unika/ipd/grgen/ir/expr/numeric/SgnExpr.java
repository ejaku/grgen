/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.numeric;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;

public class SgnExpr extends BuiltinFunctionInvocationExpr
{
	private Expression expr;

	public SgnExpr(Expression expr)
	{
		super("sgn expr", expr.getType());
		this.expr = expr;
	}

	public Expression getExpr()
	{
		return expr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		expr.collectNeededEntities(needs);
	}
}
