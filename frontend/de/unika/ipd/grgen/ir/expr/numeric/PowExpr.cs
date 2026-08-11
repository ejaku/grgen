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

public class PowExpr : BuiltinFunctionInvocationExpr
{
	private Expression leftExpr;
	private Expression rightExpr;

	public PowExpr(Expression leftExpr, Expression rightExpr)
		: base("pow expr", rightExpr.Type)
	{
		this.leftExpr = leftExpr;
		this.rightExpr = rightExpr;
	}

	public PowExpr(Expression rightExpr)
		: base("pow expr", rightExpr.Type)
	{
		this.rightExpr = rightExpr;
	}

	public virtual Expression LeftExpr
	{
		get
		{
			return leftExpr;
		}
	}

	public virtual Expression RightExpr
	{
		get
		{
			return rightExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(leftExpr != null)
			leftExpr.CollectNeededEntities(needs);
		rightExpr.CollectNeededEntities(needs);
	}
}

}
