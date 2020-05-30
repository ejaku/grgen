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

public class InsertInducedSubgraphProc extends BuiltinProcedureInvocationBase
{
	private final Expression nodeSetExpr;
	private final Expression nodeExpr;
	
	private final Type returnType;

	public InsertInducedSubgraphProc(Expression nodeSetExpr, Expression nodeExpr, Type returnType)
	{
		super("insert induced subgraph procedure");
		this.nodeSetExpr = nodeSetExpr;
		this.nodeExpr = nodeExpr;
		this.returnType = returnType;
	}

	public Expression getSetExpr()
	{
		return nodeSetExpr;
	}

	public Expression getNodeExpr()
	{
		return nodeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		nodeSetExpr.collectNeededEntities(needs);
		nodeExpr.collectNeededEntities(needs);
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
