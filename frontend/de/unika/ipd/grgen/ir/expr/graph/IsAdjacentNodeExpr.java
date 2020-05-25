/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;

public class IsAdjacentNodeExpr extends Expression
{
	private final Expression startNodeExpr;
	private final Expression endNodeExpr;
	private final Expression incidentEdgeTypeExpr;
	private final int direction;
	private final Expression adjacentNodeTypeExpr;

	public static final int ADJACENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public IsAdjacentNodeExpr(Expression startNodeExpression, Expression endNodeExpression,
			Expression incidentEdgeTypeExpr, int direction,
			Expression adjacentNodeTypeExpr, Type type)
	{
		super("is adjacent node expression", type);
		this.startNodeExpr = startNodeExpression;
		this.endNodeExpr = endNodeExpression;
		this.incidentEdgeTypeExpr = incidentEdgeTypeExpr;
		this.direction = direction;
		this.adjacentNodeTypeExpr = adjacentNodeTypeExpr;
	}

	public Expression getStartNodeExpr()
	{
		return startNodeExpr;
	}

	public Expression getEndNodeExpr()
	{
		return endNodeExpr;
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
		endNodeExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}