/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

public class SourceExpr extends BuiltinFunctionInvocationExpr
{
	private final Expression edge;

	public SourceExpr(Expression edge, Type type)
	{
		super("source expression", type);
		this.edge = edge;
	}

	public Expression getEdgeExpr()
	{
		return edge;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		edge.collectNeededEntities(needs);
	}
}
