/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.util.Direction;

public class IsBoundedReachableNodeExpr extends NeighborhoodQueryExpr
{
	private final Expression endNodeExpr;
	private final Expression depthExpr;

	public IsBoundedReachableNodeExpr(Expression startNodeExpression,
			Expression endNodeExpression, Expression depthExpression,
			Expression incidentEdgeTypeExpr, Direction direction,
			Expression adjacentNodeTypeExpr, Type type)
	{
		super("is bouneded reachable node expression", type, startNodeExpression,
				incidentEdgeTypeExpr, direction, adjacentNodeTypeExpr);
		this.endNodeExpr = endNodeExpression;
		this.depthExpr = depthExpression;
	}

	public Expression getEndNodeExpr()
	{
		return endNodeExpr;
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
		endNodeExpr.collectNeededEntities(needs);
		depthExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}
