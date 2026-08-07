/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
using de.unika.ipd.grgen.ir;
using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
using Index = de.unika.ipd.grgen.ir.model.Index;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class MinMaxFromIndexExpr : BuiltinFunctionInvocationExpr
{
	public readonly Index index;
	public readonly bool isMin;

	public MinMaxFromIndexExpr(Index index, bool isMin, Type type)
		: base("min/max node/edge from index expression", type)
	{
		this.index = index;
		this.isMin = isMin;
	}

	public virtual Index Index
	{
		get
		{
		return index;
		}
	}

	public virtual bool IsMin()
	{
		return isMin;
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
	}
}

}
