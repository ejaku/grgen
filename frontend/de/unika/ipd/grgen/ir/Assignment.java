/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.HashSet;


/**
 * Represents an assignment statement in the IR.
 */
public class Assignment extends EvalStatement {

	/** The lhs of the assignment. */
	private Expression target;

	/** The rhs of the assignment. */
	private Expression expr;

	public Assignment(Expression target, Expression expr) {
		super("assignment");
		this.target = target;
		this.expr = expr;
	}

	public Expression getTarget() {
		return target;
	}

	public Expression getExpression() {
		return expr;
	}

	public String toString() {
		return getTarget() + " = " + getExpression();
	}
	
	public void collectNeededEntities(NeededEntities needs)
	{
		Expression target = getTarget();
		Entity entity;

		if(target instanceof Qualification)
			entity = ((Qualification) target).getOwner();
		else if(target instanceof Visited) {
			Visited visTgt = (Visited) target;
			entity = visTgt.getEntity();
			visTgt.getVisitorID().collectNeededEntities(needs);
		}
		else
			throw new UnsupportedOperationException("Unsupported assignment target (" + target + ")");

		needs.add((GraphEntity) entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getExpression().collectNeededEntities(needs);
	}
}
