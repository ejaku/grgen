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

public class EdgeByUniqueExpr extends BuiltinFunctionInvocationExpr
{
	private final Expression unique;
	private final Expression edgeType;

	public EdgeByUniqueExpr(Expression unique, Expression edgeType, Type type)
	{
		super("edge by unique id expression", type);
		this.unique = unique;
		this.edgeType = edgeType;
	}

	public Expression getUniqueExpr()
	{
		return unique;
	}

	public Expression getEdgeTypeExpr()
	{
		return edgeType;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		unique.collectNeededEntities(needs);
		edgeType.collectNeededEntities(needs);
	}
}
