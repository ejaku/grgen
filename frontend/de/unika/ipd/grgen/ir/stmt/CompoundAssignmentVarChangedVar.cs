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
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

/// <summary>
/// Represents a compound assignment var changed var statement in the IR.
/// </summary>
public class CompoundAssignmentVarChangedVar : CompoundAssignmentVar
{
	/// <summary>
	/// The change assignment. </summary>
	private Variable changedTarget;

	/// <summary>
	/// The operation of the change assignment </summary>
	private CompoundAssignmentType changedOperation;

	public CompoundAssignmentVarChangedVar(Variable target,
			CompoundAssignmentType compoundAssignmentType, Expression expr,
			CompoundAssignmentType changedAssignmentType, Variable changedTarget)
		: base(target, compoundAssignmentType, expr)
	{
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public virtual Variable ChangedTarget
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

		if(!IsGlobalVariable(changedTarget))
			needs.Add(changedTarget);
	}
}

}
