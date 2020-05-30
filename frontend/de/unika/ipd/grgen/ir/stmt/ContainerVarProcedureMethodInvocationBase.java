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
}
