/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * Represents a compound assignment var statement in the IR.
 */
public class CompoundAssignmentVar extends EvalStatement
{
	public enum CompoundAssignmentType
	{
		NONE,
		UNION,
		INTERSECTION,
		WITHOUT,
		CONCATENATE,
		ASSIGN
	}

	/** The lhs of the assignment. */
	private Variable target;

	/** The operation of the compound assignment */
	private CompoundAssignmentType operation;

	/** The rhs of the assignment. */
	private Expression expr;

	public CompoundAssignmentVar(Variable target, CompoundAssignmentType compoundAssignmentType, Expression expr)
	{
		super("compound assignment var");
		this.target = target;
		this.operation = compoundAssignmentType;
		this.expr = expr;
	}

	public Variable getTarget()
	{
		return target;
	}

	public Expression getExpression()
	{
		return expr;
	}

	public CompoundAssignmentType getOperation()
	{
		return operation;
	}

	@Override
	public String toString()
	{
		return getTarget() + (operation == CompoundAssignmentType.UNION ?
				" |= " : operation == CompoundAssignmentType.INTERSECTION ? " &= " : " \\= ")
				+ getExpression();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target))
			needs.add(target);

		getExpression().collectNeededEntities(needs);
	}
}
