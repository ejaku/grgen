/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.invocation;

import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A base class for procedure or builtin procedure invocations.
 */
public abstract class ProcedureOrBuiltinProcedureInvocationBase extends EvalStatement
{
	protected ProcedureOrBuiltinProcedureInvocationBase(String name)
	{
		super(name);
	}

	/** @return The number of return arguments. */
	public abstract int returnArity();
	
	/**
	 * Get the ith return type.
	 * @param index The index of the return type
	 * @return The return type, if <code>index</code> was valid, <code>null</code> if not.
	 */
	public abstract Type getReturnType(int index);
}
