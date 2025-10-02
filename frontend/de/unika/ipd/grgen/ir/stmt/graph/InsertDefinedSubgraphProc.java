/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.type.Type;

public class InsertDefinedSubgraphProc extends BuiltinProcedureInvocationBase
{
	private final Expression edgeSetExpr;
	private final Expression edgeExpr;
	
	private final Type returnType;

	public InsertDefinedSubgraphProc(Expression var, Expression edge, Type returnType)
	{
		super("insert defined subgraph procedure");
		this.edgeSetExpr = var;
		this.edgeExpr = edge;
		this.returnType = returnType;
	}

	public Expression getSetExpr()
	{
		return edgeSetExpr;
	}

	public Expression getEdgeExpr()
	{
		return edgeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		edgeSetExpr.collectNeededEntities(needs);
		edgeExpr.collectNeededEntities(needs);
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
