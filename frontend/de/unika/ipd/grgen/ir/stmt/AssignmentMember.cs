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
using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

/// <summary>
/// Represents an assignment statement in the IR.
/// </summary>
//currently unused, would be needed for member assignment inside method without "this." prefix
public class AssignmentMember : AssignmentBase
{
	/// <summary>
	/// The lhs of the assignment. </summary>
	private Entity target;

	public AssignmentMember(Entity target, Expression expr)
		: base("assignment member")
	{
		this.target = target;
		this.expr = expr;
	}

	public virtual Entity Target
	{
		get
		{
			return target;
		}
	}

	public override string ToString()
	{
		return Target + " = " + Expression;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		Expression.CollectNeededEntities(needs);
	}
}

}
