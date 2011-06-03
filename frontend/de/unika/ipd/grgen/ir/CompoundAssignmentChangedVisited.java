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


/**
 * Represents a compound assignment changed visited statement in the IR.
 */
public class CompoundAssignmentChangedVisited extends CompoundAssignment {

	/** The change assignment. */
	private Visited changedTarget;

	/** The operation of the change assignment */
	private int changedOperation;


	public CompoundAssignmentChangedVisited(Qualification target,
			int compoundAssignmentType, Expression expr,
			int changedAssignmentType, Visited changedTarget) {
		super(target, compoundAssignmentType, expr);
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public Visited getChangedTarget() {
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

		changedTarget.collectNeededEntities(needs);
	}
}
