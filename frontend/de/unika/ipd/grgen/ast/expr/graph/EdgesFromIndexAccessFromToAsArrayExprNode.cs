/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{
using de.unika.ipd.grgen.ast;
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using EdgesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessFromToExpr;
using Index = de.unika.ipd.grgen.ir.model.Index;
using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the edges from an index as array by accessing a range from a certain value to a certain value (one or both may be optional).
/// </summary>
public class EdgesFromIndexAccessFromToAsArrayExprNode : FromIndexAccessFromToExprNode
{
	static EdgesFromIndexAccessFromToAsArrayExprNode()
	{
		SetClassName(typeof(EdgesFromIndexAccessFromToAsArrayExprNode), "edges from index access from to as array expr");
	}

	private ArrayTypeNode arrayTypeNode;
	private bool ascending;

	public EdgesFromIndexAccessFromToAsArrayExprNode(Coords coords, BaseNode index, bool ascending, ExprNode fromExpr, bool fromExclusive, ExprNode toExpr, bool toExclusive)
		: base(coords, index, fromExpr, fromExclusive, toExpr, toExclusive)
	{
		this.ascending = ascending;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		arrayTypeNode = new ArrayTypeNode(Root);
		successfullyResolved &= arrayTypeNode.Resolve();
		return successfullyResolved;
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
		return "edgesFromIndex" + FromPart() + ToPart() + "AsArray" + (ascending ? "Ascending" : "Descending") + "(" + ArgumentsPart() + ")";
	}

	public override TypeNode Type
	{
		get
		{
		return arrayTypeNode;
		}
	}

	protected internal override Operator FromOperator()
	{
		if(ascending)
			return fromExclusive ? Operator.GT : Operator.GE;
		else
			return fromExclusive ? Operator.LT : Operator.LE;
	}

	protected internal override Operator ToOperator()
	{
		if(ascending)
			return toExclusive ? Operator.LT : Operator.LE;
		else
			return toExclusive ? Operator.GT : Operator.GE;
	}

	protected internal override IR ConstructIR()
	{
		if(fromExpr != null)
			fromExpr = fromExpr.Evaluate();
		if(toExpr != null)
			toExpr = toExpr.Evaluate();
		return new EdgesFromIndexAccessFromToExpr(
				new IndexAccessOrdering(index.CheckIR(typeof(Index)), ascending,
						FromOperator(), fromExpr != null ? fromExpr.CheckIR(typeof(Expression)) : null,
						ToOperator(), toExpr != null ? toExpr.CheckIR(typeof(Expression)) : null),
				Type.IRType);
	}
}

}
