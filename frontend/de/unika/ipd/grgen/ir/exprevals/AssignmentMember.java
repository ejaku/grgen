/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;


/**
 * Represents an assignment statement in the IR.
 */
//currently unused, would be needed for member assignment inside method without "this." prefix
public class AssignmentMember extends AssignmentBase {

	/** The lhs of the assignment. */
	private Entity target;

	public AssignmentMember(Entity target, Expression expr) {
		super("assignment member");
		this.target = target;
		this.expr = expr;
	}

	public Entity getTarget() {
		return target;
	}

	public String toString() {
		return getTarget() + " = " + getExpression();
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		getExpression().collectNeededEntities(needs);
	}
}
