/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import java.util.List;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.type.Type;

public class NodesFromIndexAccessMultipleFromToExpr extends BuiltinFunctionInvocationExpr
{
	private final List<IndexAccessOrdering> indexAccesses;

	public NodesFromIndexAccessMultipleFromToExpr(List<IndexAccessOrdering> indexAccesses, Type type)
	{
		super("nodes from index access multiple expression", type);
		this.indexAccesses = indexAccesses;
	}

	public List<IndexAccessOrdering> getIndexAccesses()
	{
		return indexAccesses;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		for(IndexAccessOrdering indexAccess : indexAccesses) {
			indexAccess.collectNeededEntities(needs);
		}
	}
}
