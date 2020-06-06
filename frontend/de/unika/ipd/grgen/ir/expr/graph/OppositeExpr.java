/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

public class OppositeExpr extends BuiltinFunctionInvocationExpr
{
	private final Expression edge;
	private final Expression node;

	public OppositeExpr(Expression edge, Expression node, Type type)
	{
		super("opposite expression", type);
		this.edge = edge;
		this.node = node;
	}

	public Expression getEdgeExpr()
	{
		return edge;
	}

	public Expression getNodeExpr()
	{
		return node;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		edge.collectNeededEntities(needs);
		node.collectNeededEntities(needs);
	}
}
