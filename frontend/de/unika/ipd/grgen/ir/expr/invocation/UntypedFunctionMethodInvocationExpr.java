/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.invocation;

import de.unika.ipd.grgen.ir.type.Type;

/**
 * An untyped function method invocation is an expression.
 */
public class UntypedFunctionMethodInvocationExpr extends FunctionInvocationBaseExpr
{
	public UntypedFunctionMethodInvocationExpr(Type type)
	{
		super("untyped function method invocation expr", type);
	}
}
