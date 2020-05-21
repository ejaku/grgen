/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.parser.Coords;

/** base class for builtin and real function calls */
public abstract class FunctionOrBuiltinFunctionInvocationBaseNode extends ExprNode
{
	static {
		setName(FunctionOrBuiltinFunctionInvocationBaseNode.class, "function or builtin function invocation base");
	}

	public FunctionOrBuiltinFunctionInvocationBaseNode(Coords coords)
	{
		super(coords);
	}
}
