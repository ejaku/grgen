/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.Visited;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.AssignmentBase;

/**
 * Represents an assignment statement in the IR.
 */
public class AssignmentVisited extends AssignmentBase
{
	/** The lhs of the assignment. */
	private Visited target;

	public AssignmentVisited(Visited target, Expression expr)
	{
		super("assignment visited");
		this.target = target;
		this.expr = expr;
	}

	public Visited getTarget()
	{
		return target;
	}

	@Override
	public String toString()
	{
		return getTarget() + " = " + getExpression();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		target.getEntity().collectNeededEntities(needs);
		target.getVisitorID().collectNeededEntities(needs);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getExpression().collectNeededEntities(needs);
	}
}
