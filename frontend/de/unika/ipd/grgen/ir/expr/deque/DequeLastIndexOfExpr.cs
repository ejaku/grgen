/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ir.expr.deque
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;

public class DequeLastIndexOfExpr : DequeFunctionMethodInvocationBaseExpr
{
	private Expression valueExpr;
	private Expression startIndexExpr;

	public DequeLastIndexOfExpr(Expression targetExpr, Expression valueExpr)
		: base("deque lastIndexOf expr", IntType.Type, targetExpr)
	{
		this.valueExpr = valueExpr;
	}

	public DequeLastIndexOfExpr(Expression targetExpr, Expression valueExpr, Expression startIndexExpr)
		: base("deque lastIndexOf expr", IntType.Type, targetExpr)
	{
		this.valueExpr = valueExpr;
		this.startIndexExpr = startIndexExpr;
	}

	public virtual Expression ValueExpr
	{
		get
		{
			return valueExpr;
		}
	}

	public virtual Expression StartIndexExpr
	{
		get
		{
			return startIndexExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		base.CollectNeededEntities(needs);
		valueExpr.CollectNeededEntities(needs);
		if(startIndexExpr != null)
			startIndexExpr.CollectNeededEntities(needs);
	}
}

}
