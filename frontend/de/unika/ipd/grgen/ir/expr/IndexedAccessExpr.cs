/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Type = de.unika.ipd.grgen.ir.type.Type;
using ContainerType = de.unika.ipd.grgen.ir.type.container.ContainerType;

public class IndexedAccessExpr : Expression
{
	internal Expression targetExpr;
	internal Expression keyExpr;

	public IndexedAccessExpr(Expression targetExpr, Expression keyExpr, Type type)
		: base("indexed access expression", type)
	{
		this.targetExpr = targetExpr;
		this.keyExpr = keyExpr;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		targetExpr.CollectNeededEntities(needs);
		keyExpr.CollectNeededEntities(needs);
	}

	public virtual Expression TargetExpr
	{
		get
		{
			return targetExpr;
		}
	}

	public virtual ContainerType TargetType
	{
		get
		{
			return (ContainerType)targetExpr.Type;
		}
	}

	public virtual Expression KeyExpr
	{
		get
		{
			return keyExpr;
		}
	}
}

}
