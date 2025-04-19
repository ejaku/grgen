/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.basic.StringType;

public class CanonizeExpr extends BuiltinFunctionInvocationExpr
{
	private Expression graphExpr;

	public CanonizeExpr(Expression graphExpr)
	{
		super("canonize expr", StringType.getType());
		this.graphExpr = graphExpr;
	}

	public Expression getGraphExpr()
	{
		return graphExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		graphExpr.collectNeededEntities(needs);
	}
}
