/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

public class EqualsAnyExpr extends BuiltinFunctionInvocationExpr
{
	private final Expression subgraphExpr;
	private final Expression setExpr;
	private final boolean includingAttributes;

	public EqualsAnyExpr(Expression subgraphExpr, Expression setExpr, boolean includingAttributes, Type type)
	{
		super("equals any expression", type);
		this.subgraphExpr = subgraphExpr;
		this.setExpr = setExpr;
		this.includingAttributes = includingAttributes;
	}

	public Expression getSubgraphExpr()
	{
		return subgraphExpr;
	}

	public Expression getSetExpr()
	{
		return setExpr;
	}

	public boolean getIncludingAttributes()
	{
		return includingAttributes;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		subgraphExpr.collectNeededEntities(needs);
		setExpr.collectNeededEntities(needs);
	}
}
