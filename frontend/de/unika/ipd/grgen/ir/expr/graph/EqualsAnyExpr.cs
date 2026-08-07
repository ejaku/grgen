/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class EqualsAnyExpr : BuiltinFunctionInvocationExpr
{
	private readonly Expression subgraphExpr;
	private readonly Expression setExpr;
	private readonly bool includingAttributes;

	public EqualsAnyExpr(Expression subgraphExpr, Expression setExpr, bool includingAttributes, Type type)
		: base("equals any expression", type)
	{
		this.subgraphExpr = subgraphExpr;
		this.setExpr = setExpr;
		this.includingAttributes = includingAttributes;
	}

	public virtual Expression SubgraphExpr
	{
		get
		{
		return subgraphExpr;
		}
	}

	public virtual Expression SetExpr
	{
		get
		{
		return setExpr;
		}
	}

	public virtual bool IncludingAttributes
	{
		get
		{
		return includingAttributes;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		subgraphExpr.CollectNeededEntities(needs);
		setExpr.CollectNeededEntities(needs);
	}
}

}
