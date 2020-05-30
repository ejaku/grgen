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

import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * An external function invocation is an expression.
 */
public class ExternalFunctionInvocationExpr extends FunctionInvocationBaseExpr
{
	/** The function of the function invocation expression. */
	protected ExternalFunction externalFunction;

	public ExternalFunctionInvocationExpr(Type type, ExternalFunction externalFunction)
	{
		super("external function invocation expr", type);

		this.externalFunction = externalFunction;
	}

	public ExternalFunction getExternalFunc()
	{
		return externalFunction;
	}
}
