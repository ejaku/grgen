/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

public class InducedSubgraphExpr extends BuiltinFunctionInvocationExpr
{
	private final Expression setExpr;

	public InducedSubgraphExpr(Expression setExpr, Type type)
	{
		super("induced subgraph expression", type);
		this.setExpr = setExpr;
	}

	public Expression getSetExpr()
	{
		return setExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		setExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}
