/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.graph
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using AssignmentBase = de.unika.ipd.grgen.ir.stmt.AssignmentBase;

/// <summary>
/// Represents a nameof assignment statement in the IR.
/// </summary>
public class AssignmentNameof : AssignmentBase
{
	/// <summary>
	/// The lhs of the assignment. </summary>
	private Expression target;

	public AssignmentNameof(Expression target, Expression expr)
		: base("assignment nameof")
	{
		this.target = target;
		this.expr = expr;
	}

	public virtual Expression Target
	{
		get
		{
		return target;
		}
	}

	public override string ToString()
	{
		return "nameof(" + (Target != null ? Target.ToString() : "") + ") = " + Expression;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(target != null)
			target.CollectNeededEntities(needs);
		Expression.CollectNeededEntities(needs);
	}
}

}
