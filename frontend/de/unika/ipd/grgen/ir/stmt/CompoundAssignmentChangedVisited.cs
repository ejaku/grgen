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
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;

/// <summary>
/// Represents a compound assignment changed visited statement in the IR.
/// </summary>
public class CompoundAssignmentChangedVisited : CompoundAssignment
{
	/// <summary>
	/// The change assignment. </summary>
	private Visited changedTarget;

	/// <summary>
	/// The operation of the change assignment </summary>
	private CompoundAssignmentType changedOperation;

	public CompoundAssignmentChangedVisited(Qualification target,
			CompoundAssignmentType compoundAssignmentType, Expression expr,
			CompoundAssignmentType changedAssignmentType, Visited changedTarget)
		: base(target, compoundAssignmentType, expr)
	{
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public virtual Visited ChangedTarget
	{
		get
		{
		return changedTarget;
		}
	}

	public virtual CompoundAssignmentType ChangedOperation
	{
		get
		{
		return changedOperation;
		}
	}

	public override string ToString()
	{
		return base.ToString()
				+ (changedOperation == CompoundAssignmentType.UNION ?
						" |> " : changedOperation == CompoundAssignmentType.INTERSECTION ? " &> " : " => ")
				+ changedTarget.ToString();
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		base.CollectNeededEntities(needs);

		changedTarget.CollectNeededEntities(needs);
	}
}

}
