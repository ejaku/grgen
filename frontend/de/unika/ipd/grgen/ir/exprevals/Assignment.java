/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */
package de.unika.ipd.grgen.ir.exprevals;

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;


/**
 * Represents an assignment statement in the IR.
 */
public class Assignment extends EvalStatement {

	/** The lhs of the assignment. */
	private Qualification target;

	/** The rhs of the assignment. */
	private Expression expr;

	public Assignment(Qualification target, Expression expr) {
		super("assignment");
		this.target = target;
		this.expr = expr;
	}

	protected Assignment(String name, Qualification target, Expression expr) {
		super(name);
		this.target = target;
		this.expr = expr;
	}

	public Qualification getTarget() {
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
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity))
			needs.add((GraphEntity) entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getExpression().collectNeededEntities(needs);
	}
}
