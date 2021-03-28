/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;

/**
 * Represents an indexed assignment statement in the IR.
 */
public class AssignmentIndexed extends Assignment
{
	/** The index to the lhs. */
	private Expression index;

	public AssignmentIndexed(Qualification target, Expression expr, Expression index)
	{
		super("assignment indexed", target, expr);
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
