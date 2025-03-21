/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.util.Direction;

public class IsAdjacentNodeExpr extends NeighborhoodQueryExpr
{
	private final Expression endNodeExpr;

	public IsAdjacentNodeExpr(Expression startNodeExpression, Expression endNodeExpression,
			Expression incidentEdgeTypeExpr, Direction direction,
			Expression adjacentNodeTypeExpr, Type type)
	{
		super("is adjacent node expression", type, startNodeExpression,
				incidentEdgeTypeExpr, direction, adjacentNodeTypeExpr);
		this.endNodeExpr = endNodeExpression;
	}

	public Expression getEndNodeExpr()
	{
		return endNodeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		startNodeExpr.collectNeededEntities(needs);
		endNodeExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}
