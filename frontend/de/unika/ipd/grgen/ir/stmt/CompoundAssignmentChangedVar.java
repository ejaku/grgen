/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
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
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents a compound assignment changed var statement in the IR.
 */
public class CompoundAssignmentChangedVar extends CompoundAssignment
{
	/** The change assignment. */
	private Variable changedTarget;

	/** The operation of the change assignment */
	private CompoundAssignmentType changedOperation;

	public CompoundAssignmentChangedVar(Qualification target,
			CompoundAssignmentType compoundAssignmentType, Expression expr,
			CompoundAssignmentType changedAssignmentType, Variable changedTarget)
	{
		super(target, compoundAssignmentType, expr);
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public Variable getChangedTarget()
	{
		return changedTarget;
	}

	public CompoundAssignmentType getChangedOperation()
	{
		return changedOperation;
	}

	@Override
	public String toString()
	{
		return super.toString()
				+ (changedOperation == CompoundAssignmentType.UNION ?
						" |> " : changedOperation == CompoundAssignmentType.INTERSECTION ? " &> " : " => ")
				+ changedTarget.toString();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);

		if(!isGlobalVariable(changedTarget))
			needs.add(changedTarget);
	}
}
