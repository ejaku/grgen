/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * Represents an assignment statement in the IR.
 */
public class AssignmentVar extends AssignmentBase
{
	/** The lhs of the assignment. */
	private Variable target;

	public AssignmentVar(Variable target, Expression expr)
	{
		super("assignment var");
		this.target = target;
		this.expr = expr;
	}

	protected AssignmentVar(String name, Variable target, Expression expr)
	{
		super(name);
		this.target = target;
		this.expr = expr;
	}

	public Variable getTarget()
	{
		return target;
	}

	@Override
	public String toString()
	{
		return getTarget() + " = " + getExpression();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target))
			needs.add(target);

		getExpression().collectNeededEntities(needs);
	}
}
