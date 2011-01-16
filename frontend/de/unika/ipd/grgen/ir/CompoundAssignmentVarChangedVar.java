/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
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
 * Represents a compound assignment var changed var statement in the IR.
 */
public class CompoundAssignmentVarChangedVar extends CompoundAssignmentVar {

	/** The change assignment. */
	private Variable changedTarget;

	/** The operation of the change assignment */
	private int changedOperation;

	
	public CompoundAssignmentVarChangedVar(Variable target,
			int compoundAssignmentType, Expression expr,
			int changedAssignmentType, Variable changedTarget) {
		super(target, compoundAssignmentType, expr);
		this.changedOperation = changedAssignmentType;
		this.changedTarget = changedTarget;
	}

	public Variable getChangedTarget() {
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
		
		needs.add(changedTarget);
	}
}
