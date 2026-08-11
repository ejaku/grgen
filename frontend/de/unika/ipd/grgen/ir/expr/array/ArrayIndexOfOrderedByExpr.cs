/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ir.expr.array
{
using Entity = de.unika.ipd.grgen.ir.Entity;
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;

public class ArrayIndexOfOrderedByExpr : ArrayFunctionMethodInvocationBaseExpr
{
	private Entity member;
	private Expression valueExpr;

	public ArrayIndexOfOrderedByExpr(Expression targetExpr, Entity member, Expression valueExpr)
		: base("array indexOfOrderedBy expr", IntType.Type, targetExpr)
	{
		this.member = member;
		this.valueExpr = valueExpr;
	}

	public virtual Entity Member
	{
		get
		{
			return member;
		}
	}

	public virtual Expression ValueExpr
	{
		get
		{
			return valueExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		base.CollectNeededEntities(needs);
		valueExpr.CollectNeededEntities(needs);
	}
}

}
