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
using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;

public class DequeSubdequeExpr : DequeFunctionMethodInvocationBaseExpr
{
	private Expression startExpr;
	private Expression lengthExpr;

	public DequeSubdequeExpr(Expression targetExpr, Expression startExpr, Expression lengthExpr)
		: base("deque subdeque expr", (DequeType)targetExpr.Type, targetExpr)
	{
		this.startExpr = startExpr;
		this.lengthExpr = lengthExpr;
	}

	public virtual Expression StartExpr
	{
		get
		{
			return startExpr;
		}
	}

	public virtual Expression LengthExpr
	{
		get
		{
			return lengthExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		base.CollectNeededEntities(needs);
		startExpr.CollectNeededEntities(needs);
		lengthExpr.CollectNeededEntities(needs);
	}
}

}
