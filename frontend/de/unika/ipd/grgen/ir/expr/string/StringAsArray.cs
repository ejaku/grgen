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
using Type = de.unika.ipd.grgen.ir.type.Type;

public class StringAsArray : BuiltinFunctionInvocationExpr
{
	private Expression stringExpr;
	private Expression stringToSplitAtExpr;

	public StringAsArray(Expression stringExpr, Expression stringToSplitAtExpr, Type targetType)
		: base("string asArray", targetType)
	{
		this.stringExpr = stringExpr;
		this.stringToSplitAtExpr = stringToSplitAtExpr;
	}

	public virtual Expression StringExpr
	{
		get
		{
			return stringExpr;
		}
	}

	public virtual Expression StringToSplitAtExpr
	{
		get
		{
			return stringToSplitAtExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		stringExpr.CollectNeededEntities(needs);
		stringToSplitAtExpr.CollectNeededEntities(needs);
	}
}

}
