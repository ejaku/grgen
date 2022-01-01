/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;

/**
 * Represents an assignment statement in the IR.
 */
public class AssignmentGraphEntity extends AssignmentBase
{
	/** The lhs of the assignment. */
	private GraphEntity target;

	public AssignmentGraphEntity(GraphEntity target, Expression expr)
	{
		super("assignment graph entity");
		this.target = target;
		this.expr = expr;
	}

	public GraphEntity getTarget()
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
		if(!isGlobalVariable(target))
			needs.add(target);

		getExpression().collectNeededEntities(needs);
	}
}
