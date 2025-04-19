/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessMultipleFromToExpr;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the nodes from multiple indices (by accessing a range from a certain value to a certain value, each time).
 */
public class NodesFromIndexAccessMultipleFromToExprNode extends FromIndexAccessMultipleFromToExprNode
{
	static {
		setName(NodesFromIndexAccessMultipleFromToExprNode.class, "nodes from index access multiple from to expr");
	}

	public NodesFromIndexAccessMultipleFromToExprNode(Coords coords)
	{
		super(coords);
	}

	protected IdentNode getRoot()
	{
		return getNodeRoot();
	}

	protected String shortSignature()
	{
		return "nodesFromIndexMultipleFromTo" + "(" + argumentsPart() + ")";
	}

	@Override
	protected IR constructIR()
	{
		Vector<IndexAccessOrdering> indexAccesses = new Vector<IndexAccessOrdering>();
		for(FromIndexAccessFromToPartExprNode indexAccessExpr : indexAccessExprs.getChildren())
		{
			indexAccesses.add(indexAccessExpr.constructIRPart());
		}
		return new NodesFromIndexAccessMultipleFromToExpr(indexAccesses, getType().getType());
	}
}
