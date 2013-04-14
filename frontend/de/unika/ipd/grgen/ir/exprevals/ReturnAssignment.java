/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.exprevals;

import java.util.Vector;


/**
 * Represents an assignment of computation invocation return values statement in the IR.
 */
public class ReturnAssignment extends EvalStatement {
	ComputationInvocationBase computationInvocation;
	Vector<AssignmentBase> targets = new Vector<AssignmentBase>();

	public ReturnAssignment(ComputationInvocationBase computationInvocation) {
		super("return assignment");
		
		this.computationInvocation = computationInvocation;
	}
	
	public void addAssignment(AssignmentBase target) {
		targets.add(target);
	}
	
	public ComputationInvocationBase getComputationInvocation() {
		return computationInvocation;
	}
	
	public Vector<AssignmentBase> getTargets() {
		return targets;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement target : targets) {
			target.collectNeededEntities(needs);
		}
		computationInvocation.collectNeededEntities(needs);
	}
}
