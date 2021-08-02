/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * A exec variable expression node.
 */
public class ExecVariableExpression extends Expression
{
	private ExecVariable var;

	public ExecVariableExpression(ExecVariable var)
	{
		super("exec variable", var.getType());
		this.var = var;
	}

	/** Returns the exec variable of this exec variable expression. */
	public ExecVariable getVariable()
	{
		return var;
	}

	@Override
	public boolean equals(Object other)
	{
		if(!(other instanceof ExecVariableExpression))
			return false;
		return var == ((ExecVariableExpression)other).getVariable();
	}

	@Override
	public int hashCode()
	{
		return var.hashCode();
	}
}
