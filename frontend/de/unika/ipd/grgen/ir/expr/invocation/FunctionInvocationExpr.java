/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.invocation;

import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Real function calls, i.e. calls of user-defined functions.
 */
public class FunctionInvocationExpr extends FunctionInvocationBaseExpr
{
	/** The function of the function invocation expression. */
	protected Function function;

	public FunctionInvocationExpr(Type type, Function function)
	{
		super("function invocation expr", type);

		this.function = function;
	}

	public Function getFunction()
	{
		return function;
	}
}
