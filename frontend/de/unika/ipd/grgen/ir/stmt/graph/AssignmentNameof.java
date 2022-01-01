/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.AssignmentBase;

/**
 * Represents a nameof assignment statement in the IR.
 */
public class AssignmentNameof extends AssignmentBase
{
	/** The lhs of the assignment. */
	private Expression target;

	public AssignmentNameof(Expression target, Expression expr)
	{
		super("assignment nameof");
		this.target = target;
		this.expr = expr;
	}

	public Expression getTarget()
	{
		return target;
	}

	@Override
	public String toString()
	{
		return "nameof(" + (getTarget() != null ? getTarget().toString() : "") + ") = " + getExpression();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(target != null)
			target.collectNeededEntities(needs);
		getExpression().collectNeededEntities(needs);
	}
}
