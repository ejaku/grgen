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

/// <summary>
/// Represents an indexed assignment statement in the IR.
/// </summary>
public class AssignmentIndexed : Assignment
{
	/// <summary>
	/// The index to the lhs. </summary>
	private Expression index;

	public AssignmentIndexed(Qualification target, Expression expr, Expression index)
		: base("assignment indexed", target, expr)
	{
		this.index = index;
	}

	public virtual Expression Index
	{
		get
		{
			return index;
		}
	}

	public override string ToString()
	{
		return Target + "[" + Index + "] = " + Expression;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		base.CollectNeededEntities(needs);
		Index.CollectNeededEntities(needs);
	}
}

}
