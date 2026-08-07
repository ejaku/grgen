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

public class InducedSubgraphExpr : BuiltinFunctionInvocationExpr
{
	private readonly Expression setExpr;

	public InducedSubgraphExpr(Expression setExpr, Type type)
		: base("induced subgraph expression", type)
	{
		this.setExpr = setExpr;
	}

	public virtual Expression SetExpr
	{
		get
		{
		return setExpr;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		setExpr.CollectNeededEntities(needs);
		needs.NeedsGraph();
	}
}

}
