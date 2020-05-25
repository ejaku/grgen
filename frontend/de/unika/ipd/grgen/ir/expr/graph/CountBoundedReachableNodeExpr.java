/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.basic.IntType;

public class CountBoundedReachableNodeExpr extends Expression
{
	private final Expression startNodeExpr;
	private final Expression depthExpr;
	private final Expression incidentEdgeTypeExpr;
	private final int direction;
	private final Expression adjacentNodeTypeExpr;

	public static final int ADJACENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public CountBoundedReachableNodeExpr(Expression startNodeExpression, Expression depthExpression,
			Expression incidentEdgeTypeExpr, int direction,
			Expression adjacentNodeTypeExpr)
	{
		super("count bounded reachable node expression", IntType.getType());
		this.startNodeExpr = startNodeExpression;
		this.depthExpr = depthExpression;
		this.incidentEdgeTypeExpr = incidentEdgeTypeExpr;
		this.direction = direction;
		this.adjacentNodeTypeExpr = adjacentNodeTypeExpr;
	}

	public Expression getStartNodeExpr()
	{
		return startNodeExpr;
	}

	public Expression getDepthExpr()
	{
		return depthExpr;
	}

	public Expression getIncidentEdgeTypeExpr()
	{
		return incidentEdgeTypeExpr;
	}

	public int Direction()
	{
		return direction;
	}

	public Expression getAdjacentNodeTypeExpr()
	{
		return adjacentNodeTypeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		startNodeExpr.collectNeededEntities(needs);
		depthExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}