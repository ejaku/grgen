/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.type.Type;

public class NodesFromIndexAccessFromToExpr extends NodesFromIndexAccessExpr
{
	private final IndexAccessOrdering indexAccess;
	private final boolean ascending;

	public NodesFromIndexAccessFromToExpr(IndexAccessOrdering indexAccess, Type type)
	{
		super(indexAccess.index, type);
		this.indexAccess = indexAccess;
		this.ascending = true; // dummy assignment, value is not of importance in this constructor for the set type
	}

	public NodesFromIndexAccessFromToExpr(IndexAccessOrdering indexAccess, boolean ascending, Type type)
	{
		super(indexAccess.index, type);
		this.indexAccess = indexAccess;
		this.ascending = ascending;
	}

	public IndexAccessOrdering getIndexAccessOrdering()
	{
		return indexAccess;
	}

	public boolean isAscending()
	{
		return ascending;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		indexAccess.collectNeededEntities(needs);
	}
}
