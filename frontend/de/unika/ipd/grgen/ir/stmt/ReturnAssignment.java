/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.stmt;

import java.util.ArrayList;
import java.util.List;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;

/**
 * Represents an assignment of procedure invocation return values statement in the IR.
 */
public class ReturnAssignment extends EvalStatement
{
	ProcedureOrBuiltinProcedureInvocationBase procedureInvocation;
	List<AssignmentBase> targets = new ArrayList<AssignmentBase>();

	public ReturnAssignment(ProcedureOrBuiltinProcedureInvocationBase procedureInvocation)
	{
		super("return assignment");

		this.procedureInvocation = procedureInvocation;
	}

	public void addAssignment(AssignmentBase target)
	{
		targets.add(target);
	}

	public ProcedureOrBuiltinProcedureInvocationBase getProcedureInvocation()
	{
		return procedureInvocation;
	}

	public List<AssignmentBase> getTargets()
	{
		return targets;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement target : targets) {
			target.collectNeededEntities(needs);
		}
		procedureInvocation.collectNeededEntities(needs);
	}
}
