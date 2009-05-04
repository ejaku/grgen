/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
}
