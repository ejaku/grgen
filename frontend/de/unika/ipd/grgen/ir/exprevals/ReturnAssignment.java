/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.exprevals;

import java.util.Vector;


/**
 * Represents an assignment of procedure invocation return values statement in the IR.
 */
public class ReturnAssignment extends EvalStatement {
	ProcedureInvocationBase procedureInvocation;
	Vector<AssignmentBase> targets = new Vector<AssignmentBase>();

	public ReturnAssignment(ProcedureInvocationBase procedureInvocation) {
		super("return assignment");
		
		this.procedureInvocation = procedureInvocation;
	}
	
	public void addAssignment(AssignmentBase target) {
		targets.add(target);
	}
	
	public ProcedureInvocationBase getProcedureInvocation() {
		return procedureInvocation;
	}
	
	public Vector<AssignmentBase> getTargets() {
		return targets;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement target : targets) {
			target.collectNeededEntities(needs);
		}
		procedureInvocation.collectNeededEntities(needs);
	}
}
