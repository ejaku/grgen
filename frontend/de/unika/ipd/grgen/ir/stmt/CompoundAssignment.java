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

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents a compound assignment statement in the IR.
 */
public class CompoundAssignment extends EvalStatement
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
	private Qualification target;

	/** The operation of the compound assignment */
	private CompoundAssignmentType operation;

	/** The rhs of the assignment. */
	private Expression expr;

	public CompoundAssignment(Qualification target, CompoundAssignmentType compoundAssignmentType, Expression expr)
	{
		super("compound assignment");
		this.target = target;
		this.operation = compoundAssignmentType;
		this.expr = expr;
	}

	public Qualification getTarget()
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
		String res = getTarget().toString();
		if(operation == CompoundAssignmentType.UNION)
			res += " |= ";
		else if(operation == CompoundAssignmentType.INTERSECTION)
			res += " &= ";
		else if(operation == CompoundAssignmentType.WITHOUT)
			res += " \\= ";
		else if(operation == CompoundAssignmentType.CONCATENATE)
			res += " += ";
		else
			res += " = ";
		res += getExpression().toString();
		return res;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity))
			needs.add((GraphEntity)entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getExpression().collectNeededEntities(needs);
	}
}
