/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.numeric
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;

public class ArcSinCosTanExpr : BuiltinFunctionInvocationExpr
{
	public enum ArcusTrigonometryFunctionType
	{
		arcsin,
		arccos,
		arctan
	}

	private ArcusTrigonometryFunctionType which;
	private Expression expr;

	public ArcSinCosTanExpr(ArcusTrigonometryFunctionType which, Expression expr)
		: base("arc sin cos tan expr", expr.Type)
	{
		this.which = which;
		this.expr = expr;
	}

	public virtual ArcusTrigonometryFunctionType Which
	{
		get
		{
			return which;
		}
	}

	public virtual Expression Expr
	{
		get
		{
			return expr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		expr.CollectNeededEntities(needs);
	}
}

}
