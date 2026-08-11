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
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;

/// <summary>
/// Represents an assignment statement in the IR.
/// </summary>
public class AssignmentGraphEntity : AssignmentBase
{
	/// <summary>
	/// The lhs of the assignment. </summary>
	private GraphEntity target;

	public AssignmentGraphEntity(GraphEntity target, Expression expr)
		: base("assignment graph entity")
	{
		this.target = target;
		this.expr = expr;
	}

	public virtual GraphEntity Target
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
		if(!IsGlobalVariable(target))
			needs.Add(target);

		Expression.CollectNeededEntities(needs);
	}
}

}
