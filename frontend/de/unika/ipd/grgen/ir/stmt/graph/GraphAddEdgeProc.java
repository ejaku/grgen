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

public class GraphAddEdgeProc extends BuiltinProcedureInvocationBase
{
	private final Expression sourceNode;
	private final Expression targetNode;
	private final Expression edgeType;
	
	private final Type returnType;

	public GraphAddEdgeProc(Expression edgeType,
			Expression sourceNode,
			Expression targetNode,
			Type returnType)
	{
		super("graph add edge procedure");
		this.edgeType = edgeType;
		this.sourceNode = sourceNode;
		this.targetNode = targetNode;
		this.returnType = returnType;
	}

	public Expression getEdgeTypeExpr()
	{
		return edgeType;
	}

	public Expression getSourceNodeExpr()
	{
		return sourceNode;
	}

	public Expression getTargetNodeExpr()
	{
		return targetNode;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		edgeType.collectNeededEntities(needs);
		sourceNode.collectNeededEntities(needs);
		targetNode.collectNeededEntities(needs);
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
