/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using AssignmentBase = de.unika.ipd.grgen.ir.stmt.AssignmentBase;

/// <summary>
/// Represents an assignment statement in the IR.
/// </summary>
public class AssignmentVisited : AssignmentBase
{
	/// <summary>
	/// The lhs of the assignment. </summary>
	private Visited target;

	public AssignmentVisited(Visited target, Expression expr)
		: base("assignment visited")
	{
		this.target = target;
		this.expr = expr;
	}

	public virtual Visited Target
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
		target.Entity.CollectNeededEntities(needs);
		target.VisitorID.CollectNeededEntities(needs);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.CollectNeededEntities(needs);
		needs.variables = varSet;

		Expression.CollectNeededEntities(needs);
	}
}

}
