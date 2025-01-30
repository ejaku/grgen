/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.type.Type;

public class InsertCopyProc extends BuiltinProcedureInvocationBase
{
	private final Expression graphExpr;
	private final Expression nodeExpr;
	
	private final Type returnType;

	public InsertCopyProc(Expression graphExpr, Expression nodeExpr, Type returnType)
	{
		super("insert copy procedure");
		this.graphExpr = graphExpr;
		this.nodeExpr = nodeExpr;
		this.returnType = returnType;
	}

	public Expression getGraphExpr()
	{
		return graphExpr;
	}

	public Expression getNodeExpr()
	{
		return nodeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		graphExpr.collectNeededEntities(needs);
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
