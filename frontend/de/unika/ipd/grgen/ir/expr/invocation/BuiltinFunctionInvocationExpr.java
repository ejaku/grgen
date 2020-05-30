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

import de.unika.ipd.grgen.ir.type.Type;

/**
 * Base class for builtin function calls.
 */
public abstract class BuiltinFunctionInvocationExpr extends FunctionOrBuiltinFunctionInvocationExpr
{
	protected BuiltinFunctionInvocationExpr(String name, Type type)
	{
		super(name, type);
	}
}
