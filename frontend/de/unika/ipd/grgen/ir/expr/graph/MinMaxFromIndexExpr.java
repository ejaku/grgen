/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.type.Type;

public class MinMaxFromIndexExpr extends BuiltinFunctionInvocationExpr
{
	public final Index index;
	public final boolean isMin;

	public MinMaxFromIndexExpr(Index index, boolean isMin, Type type)
	{
		super("min/max node/edge from index expression", type);
		this.index = index;
		this.isMin = isMin;
	}

	public Index getIndex()
	{
		return index;
	}

	public boolean isMin()
	{
		return isMin;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
	}
}
