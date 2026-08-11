/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.@string
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
using BooleanType = de.unika.ipd.grgen.ir.type.basic.BooleanType;

public class StringStartsWith : BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression stringToSearchForExpr;

	public StringStartsWith(Expression stringExpr, Expression stringToSearchForExpr)
		: base("string startsWith", BooleanType.Type)
	{
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
	}

	public virtual Expression StringExpr
	{
		get
		{
		return stringExpr;
		}
	}

	public virtual Expression StringToSearchForExpr
	{
		get
		{
		return stringToSearchForExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		stringExpr.CollectNeededEntities(needs);
		stringToSearchForExpr.CollectNeededEntities(needs);
	}
}

}
