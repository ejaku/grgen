/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.type.Type;

public class GraphRetypeEdgeProc extends BuiltinProcedureInvocationBase
{
	private final Expression edge;
	private final Expression newEdgeType;
	
	private final Type returnType;

	public GraphRetypeEdgeProc(Expression edge, Expression newEdgeType, Type returnType)
	{
		super("graph retype edge procedure");
		this.edge = edge;
		this.newEdgeType = newEdgeType;
		this.returnType = returnType;
	}

	public Expression getEdgeExpr()
	{
		return edge;
	}

	public Expression getNewEdgeTypeExpr()
	{
		return newEdgeType;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		edge.collectNeededEntities(needs);
		newEdgeType.collectNeededEntities(needs);
	}
	
	@Override
	public int returnArity()
	{
		return 1;
	}
	
	@Override
	public Type getReturnType(int index)
	{
		assert(index == 0);
		return returnType;
	}
}
