/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.pattern.Variable;

public abstract class ContainerVarProcedureMethodInvocationBase extends BuiltinProcedureInvocationBase
{
	protected Variable target;

	protected ContainerVarProcedureMethodInvocationBase(String name, Variable target)
	{
		super(name);
		this.target = target;
	}

	public Variable getTarget()
	{
		return target;
	}
	
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target))
			needs.add(target);

		if(getNext() != null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
