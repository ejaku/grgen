/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents an indexed assignment statement in the IR.
 */
public class AssignmentVarIndexed extends AssignmentVar
{
	/** The index to the lhs. */
	private Expression index;

	public AssignmentVarIndexed(Variable target, Expression expr, Expression index)
	{
		super("assignment var indexed", target, expr);
		this.index = index;
	}

	public Expression getIndex()
	{
		return index;
	}

	@Override
	public String toString()
	{
		return getTarget() + "[" + getIndex() + "] = " + getExpression();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		getIndex().collectNeededEntities(needs);
	}
}
