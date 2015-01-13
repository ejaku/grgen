/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

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
