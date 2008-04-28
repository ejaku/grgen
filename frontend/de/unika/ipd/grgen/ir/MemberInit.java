/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


public class MemberInit extends IR {

	/** The lhs of the assignment. */
	private Entity member;

	/** The rhs of the assignment. */
	private Expression expr;

	public MemberInit(Entity member, Expression expr) {
		super("memberinit");
		this.member = member;
		this.expr = expr;
	}

	public Entity getMember() {
		return member;
	}

	public Expression getExpression() {
		return expr;
	}

	public String toString() {
		return getMember() + " = " + getExpression();
	}
}
