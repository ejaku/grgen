/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;

public class DefinedSubgraphExpr extends Expression
{
	private final Expression setExpr;

	public DefinedSubgraphExpr(Expression setExpr, Type type)
	{
		super("defined subgraph expression", type);
		this.setExpr = setExpr;
	}

	public Expression getSetExpr()
	{
		return setExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		setExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}
