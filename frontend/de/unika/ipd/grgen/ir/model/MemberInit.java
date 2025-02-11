/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ir.model;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;

public class MemberInit extends IR
{
	/** The lhs of the assignment. */
	private Entity member;

	/** The rhs of the assignment. */
	private Expression expr;

	public MemberInit(Entity member, Expression expr)
	{
		super("memberinit");
		this.member = member;
		this.expr = expr;
	}

	public Entity getMember()
	{
		return member;
	}

	public Expression getExpression()
	{
		return expr;
	}

	@Override
	public String toString()
	{
		return getMember() + " = " + getExpression();
	}
}
