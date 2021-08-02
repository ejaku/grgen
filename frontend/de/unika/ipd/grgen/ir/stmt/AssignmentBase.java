/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * Gives access to the expression of an assignment statement in the IR.
 */
public abstract class AssignmentBase extends EvalStatement
{
	/** The rhs of the assignment. */
	protected Expression expr;

	public AssignmentBase(String name)
	{
		super(name);
	}

	public Expression getExpression()
	{
		return expr;
	}

	public void setExpression(Expression expr)
	{
		this.expr = expr;
	}
}
