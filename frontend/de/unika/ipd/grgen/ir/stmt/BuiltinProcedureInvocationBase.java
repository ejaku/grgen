/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A base class for builtin procedure invocations.
 */
public abstract class BuiltinProcedureInvocationBase extends ProcedureOrBuiltinProcedureInvocationBase
{
	protected BuiltinProcedureInvocationBase(String name)
	{
		super(name);
	}

	@Override
	public int returnArity()
	{
		return 0;
	}
	
	@Override
	public Type getReturnType(int index)
	{
		return null;
	}
}
