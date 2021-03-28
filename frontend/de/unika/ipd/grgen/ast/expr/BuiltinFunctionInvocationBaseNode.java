/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.expr.invocation.FunctionOrBuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.parser.Coords;

/** base class for builtin function calls */
public abstract class BuiltinFunctionInvocationBaseNode extends FunctionOrBuiltinFunctionInvocationBaseNode
{
	static {
		setName(BuiltinFunctionInvocationBaseNode.class, "builtin function invocation base");
	}

	public BuiltinFunctionInvocationBaseNode(Coords coords)
	{
		super(coords);
	}
}
