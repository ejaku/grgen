/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

/// <summary>
/// Represents a lock statement in the IR.
/// </summary>
public class LockStatement : BlockNestingStatement
{
	private Expression lockObjectExpr;

	public LockStatement(Expression lockObjectExpr)
		: base("lock statement")
	{
		this.lockObjectExpr = lockObjectExpr;
	}

	public virtual Expression LockObjectExpr
	{
		get
		{
		return lockObjectExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		lockObjectExpr.CollectNeededEntities(needs);
		foreach(EvalStatement lockedStatement in statements)
			lockedStatement.CollectNeededEntities(needs);
	}
}

}
