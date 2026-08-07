/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using IR = de.unika.ipd.grgen.ir.IR;
using EdgesFromIndexAccessMultipleFromToExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessMultipleFromToExpr;
using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the edges from multiple indices (by accessing a range from a certain value to a certain value, each time).
/// </summary>
public class EdgesFromIndexAccessMultipleFromToExprNode : FromIndexAccessMultipleFromToExprNode
{
	static EdgesFromIndexAccessMultipleFromToExprNode()
	{
		SetClassName(typeof(EdgesFromIndexAccessMultipleFromToExprNode), "edges from index access multiple from to expr");
	}

	public EdgesFromIndexAccessMultipleFromToExprNode(Coords coords)
		: base(coords)
	{
	}

	protected internal override IdentNode Root
	{
		get
		{
		return EdgeRoot;
		}
	}

	protected internal override string ShortSignature()
	{
		return "edgesFromIndexMultipleFromTo" + "(" + ArgumentsPart() + ")";
	}

	protected internal override IR ConstructIR()
	{
		IList<IndexAccessOrdering> indexAccesses = new List<IndexAccessOrdering>();
		foreach(FromIndexAccessFromToPartExprNode indexAccessExpr in indexAccessExprs.ChildrenExact)
			indexAccesses.Add(indexAccessExpr.ConstructIRPart());
		return new EdgesFromIndexAccessMultipleFromToExpr(indexAccesses, Type.IRType);
	}
}

}
