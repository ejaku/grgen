/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr
{
using de.unika.ipd.grgen.ir;
using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class ScanExpr : BuiltinFunctionInvocationExpr
{
	private readonly Expression stringExpr;

	public ScanExpr(Expression stringExpr, Type type)
		: base("scan expression", type)
	{
		this.stringExpr = stringExpr;
	}

	public virtual Expression StringExpr
	{
		get
		{
			return stringExpr;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		stringExpr.CollectNeededEntities(needs);
		needs.NeedsGraph();
	}
}

}
