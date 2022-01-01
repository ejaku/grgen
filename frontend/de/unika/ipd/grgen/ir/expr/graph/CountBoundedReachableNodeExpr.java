/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.basic.IntType;
import de.unika.ipd.grgen.util.Direction;

public class CountBoundedReachableNodeExpr extends NeighborhoodQueryExpr
{
	private final Expression depthExpr;

	public CountBoundedReachableNodeExpr(Expression startNodeExpression, Expression depthExpression,
			Expression incidentEdgeTypeExpr, Direction direction,
			Expression adjacentNodeTypeExpr)
	{
		super("count bounded reachable node expression", IntType.getType(), startNodeExpression,
				incidentEdgeTypeExpr, direction, adjacentNodeTypeExpr);
		this.depthExpr = depthExpression;
	}

	public Expression getDepthExpr()
	{
		return depthExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		startNodeExpr.collectNeededEntities(needs);
		depthExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}
