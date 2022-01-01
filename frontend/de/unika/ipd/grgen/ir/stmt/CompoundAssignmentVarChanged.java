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

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents a compound assignment var changed statement in the IR.
 */
public class CompoundAssignmentVarChanged extends CompoundAssignmentVar
{
	/** The change assignment. */
	private Qualification changedTarget;

	/** The operation of the change assignment */
	private CompoundAssignmentType changedOperation;

	public CompoundAssignmentVarChanged(Variable target,
			CompoundAssignmentType compoundAssignmentType, Expression expr,
			CompoundAssignmentType changedAssignmentType, Qualification changedTarget)
	{
		super(target, compoundAssignmentType, expr);
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public Qualification getChangedTarget()
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

		Entity entity = changedTarget.getOwner();
		if(!isGlobalVariable(entity))
			needs.add((GraphEntity)entity);

		// Temporarily do not collect variables for changed target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		changedTarget.collectNeededEntities(needs);
		needs.variables = varSet;
	}
}
