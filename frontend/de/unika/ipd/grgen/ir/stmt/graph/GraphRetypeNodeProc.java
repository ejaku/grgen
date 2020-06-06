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

public class GraphRetypeNodeProc extends BuiltinProcedureInvocationBase
{
	private final Expression node;
	private final Expression newNodeType;
	
	private final Type returnType;

	public GraphRetypeNodeProc(Expression node, Expression newNodeType, Type returnType)
	{
		super("graph retype node procedure");
		this.node = node;
		this.newNodeType = newNodeType;
		this.returnType = returnType;
	}

	public Expression getNodeExpr()
	{
		return node;
	}

	public Expression getNewNodeTypeExpr()
	{
		return newNodeType;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		node.collectNeededEntities(needs);
		newNodeType.collectNeededEntities(needs);
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
