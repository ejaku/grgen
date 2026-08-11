/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{
using System;

using de.unika.ipd.grgen.ast;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Index = de.unika.ipd.grgen.ir.model.Index;
using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the graph elements (nodes or edges) from an index by accessing a range from a certain value to a certain value (part class to be used in a multiple index query).
/// </summary>
public class FromIndexAccessFromToPartExprNode : FromIndexAccessFromToExprNode
{
	static FromIndexAccessFromToPartExprNode()
	{
		SetClassName(typeof(FromIndexAccessFromToPartExprNode), "from index access from to part expr");
	}

	internal int indexShiftCausedByPartNumber;
	internal FromIndexAccessMultipleFromToExprNode wholeExpr;

	public FromIndexAccessFromToPartExprNode(Coords coords, BaseNode index, ExprNode fromExpr, bool fromExclusive, ExprNode toExpr, bool toExclusive, int indexShiftCausedByPartNumber, FromIndexAccessMultipleFromToExprNode wholeExpr)
		: base(coords, index, fromExpr, fromExclusive, toExpr, toExclusive)
	{
		this.indexShiftCausedByPartNumber = indexShiftCausedByPartNumber;
		this.wholeExpr = wholeExpr;
	}

	protected internal override int IndexShift() // the parts in a multiple from to index query are shifted by 3 from each other
	{
		return indexShiftCausedByPartNumber;
	}

	protected internal override IdentNode Root
	{
		get
		{
			return wholeExpr.Root;
		}
	}

	protected internal override string ShortSignature()
	{
		return wholeExpr.ShortSignature();
	}

	public virtual IndexAccessOrdering ConstructIRPart()
	{
		if(fromExpr != null)
			fromExpr = fromExpr.Evaluate();
		if(toExpr != null)
			toExpr = toExpr.Evaluate();
		return new IndexAccessOrdering(index.CheckIR(typeof(Index)), true,
				FromOperator(), fromExpr != null ? fromExpr.CheckIR(typeof(Expression)) : null,
				ToOperator(), toExpr != null ? toExpr.CheckIR(typeof(Expression)) : null);
	}

	protected internal override IR ConstructIR()
	{
		throw new Exception("Not implemented! Only used as part class.");
	}

	public override TypeNode Type
	{
		get
		{
			throw new Exception("Not implemented! Only used as part class.");
		}
	}
}

}
