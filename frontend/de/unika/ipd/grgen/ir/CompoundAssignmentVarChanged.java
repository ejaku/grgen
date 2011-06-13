/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.HashSet;


/**
 * Represents a compound assignment var changed statement in the IR.
 */
public class CompoundAssignmentVarChanged extends CompoundAssignmentVar {

	/** The change assignment. */
	private Qualification changedTarget;

	/** The operation of the change assignment */
	private int changedOperation;


	public CompoundAssignmentVarChanged(Variable target,
			int compoundAssignmentType, Expression expr,
			int changedAssignmentType, Qualification changedTarget) {
		super(target, compoundAssignmentType, expr);
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public Qualification getChangedTarget() {
		return changedTarget;
	}

	public int getChangedOperation() {
		return changedOperation;
	}

	public String toString() {
		return super.toString() + (changedOperation==UNION?" |> ":changedOperation==INTERSECTION?" &> ":" => ") + changedTarget.toString();
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);

		Entity entity = changedTarget.getOwner();
		needs.add((GraphEntity) entity);

		// Temporarily do not collect variables for changed target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		changedTarget.collectNeededEntities(needs);
		needs.variables = varSet;
	}
}
