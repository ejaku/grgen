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


/**
 * Represents a nameof assignment statement in the IR.
 */
public class AssignmentNameof extends AssignmentBase {

	/** The lhs of the assignment. */
	private Expression target;

	public AssignmentNameof(Expression target, Expression expr) {
		super("assignment nameof");
		this.target = target;
		this.expr = expr;
	}

	public Expression getTarget() {
		return target;
	}

	public String toString() {
		return "nameof(" + (getTarget()!=null ? getTarget().toString() : "") + ") = " + getExpression();
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(target!=null)
			target.collectNeededEntities(needs);		
		getExpression().collectNeededEntities(needs);
	}
}
