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

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.graph.Visited;

/**
 * Represents a compound assignment changed visited statement in the IR.
 */
public class CompoundAssignmentChangedVisited extends CompoundAssignment
{
	/** The change assignment. */
	private Visited changedTarget;

	/** The operation of the change assignment */
	private CompoundAssignmentType changedOperation;

	public CompoundAssignmentChangedVisited(Qualification target,
			CompoundAssignmentType compoundAssignmentType, Expression expr,
			CompoundAssignmentType changedAssignmentType, Visited changedTarget)
	{
		super(target, compoundAssignmentType, expr);
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public Visited getChangedTarget()
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

		changedTarget.collectNeededEntities(needs);
	}
}
