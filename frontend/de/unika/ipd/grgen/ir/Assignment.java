/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


/**
 * Represents an assignment statement in the IR.
 */
public class Assignment extends IR {

	/** The lhs of the assignment. */
	private Qualification target;

	/** The rhs of the assignment. */
	private Expression expr;

	public Assignment(Qualification target, Expression expr) {
		super("assignment");
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
}
