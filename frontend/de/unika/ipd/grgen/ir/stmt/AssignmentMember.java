/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * Represents an assignment statement in the IR.
 */
//currently unused, would be needed for member assignment inside method without "this." prefix
public class AssignmentMember extends AssignmentBase
{
	/** The lhs of the assignment. */
	private Entity target;

	public AssignmentMember(Entity target, Expression expr)
	{
		super("assignment member");
		this.target = target;
		this.expr = expr;
	}

	public Entity getTarget()
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
		getExpression().collectNeededEntities(needs);
	}
}
