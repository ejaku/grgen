/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
public class AssignmentGraphEntity extends AssignmentBase {

	/** The lhs of the assignment. */
	private GraphEntity target;

	public AssignmentGraphEntity(GraphEntity target, Expression expr) {
		super("assignment graph entity");
		this.target = target;
		this.expr = expr;
	}

	public GraphEntity getTarget() {
		return target;
	}

	public String toString() {
		return getTarget() + " = " + getExpression();
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target))
			needs.add(target);

		getExpression().collectNeededEntities(needs);
	}
}
