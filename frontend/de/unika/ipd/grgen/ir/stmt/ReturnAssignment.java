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

import java.util.Vector;

import de.unika.ipd.grgen.ir.NeededEntities;

/**
 * Represents an assignment of procedure invocation return values statement in the IR.
 */
public class ReturnAssignment extends EvalStatement
{
	ProcedureInvocationBase procedureInvocation;
	Vector<AssignmentBase> targets = new Vector<AssignmentBase>();

	public ReturnAssignment(ProcedureInvocationBase procedureInvocation)
	{
		super("return assignment");

		this.procedureInvocation = procedureInvocation;
	}

	public void addAssignment(AssignmentBase target)
	{
		targets.add(target);
	}

	public ProcedureInvocationBase getProcedureInvocation()
	{
		return procedureInvocation;
	}

	public Vector<AssignmentBase> getTargets()
	{
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