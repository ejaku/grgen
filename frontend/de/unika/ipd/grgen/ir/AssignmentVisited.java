/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id: Assignment.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ir;

import java.util.HashSet;


/**
 * Represents an assignment statement in the IR.
 */
public class AssignmentVisited extends EvalStatement {

	/** The lhs of the assignment. */
	private Visited target;

	/** The rhs of the assignment. */
	private Expression expr;

	public AssignmentVisited(Visited target, Expression expr) {
		super("assignment visited");
		this.target = target;
		this.expr = expr;
	}

	public Visited getTarget() {
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
		Entity entity = target.getEntity();
		target.getVisitorID().collectNeededEntities(needs);
		needs.add((GraphEntity) entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getExpression().collectNeededEntities(needs);
	}
}
