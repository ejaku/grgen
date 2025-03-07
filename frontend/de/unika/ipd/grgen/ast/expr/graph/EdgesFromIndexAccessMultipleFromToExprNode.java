/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessMultipleFromToExpr;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the edges from multiple indices (by accessing a range from a certain value to a certain value, each time).
 */
public class EdgesFromIndexAccessMultipleFromToExprNode extends FromIndexAccessMultipleFromToExprNode
{
	static {
		setName(EdgesFromIndexAccessMultipleFromToExprNode.class, "edges from index access multiple from to expr");
	}

	public EdgesFromIndexAccessMultipleFromToExprNode(Coords coords)
	{
		super(coords);
	}

	protected IdentNode getRoot()
	{
		return getEdgeRoot();
	}

	protected String shortSignature()
	{
		return "edgesFromIndexMultipleFromTo" + "(" + argumentsPart() + ")";
	}

	@Override
	protected IR constructIR()
	{
		Vector<IndexAccessOrdering> indexAccesses = new Vector<IndexAccessOrdering>();
		for(FromIndexAccessFromToPartExprNode indexAccessExpr : indexAccessExprs.getChildren())
		{
			indexAccesses.add(indexAccessExpr.constructIRPart());
		}
		return new EdgesFromIndexAccessMultipleFromToExpr(indexAccesses, getType().getType());
	}
}
