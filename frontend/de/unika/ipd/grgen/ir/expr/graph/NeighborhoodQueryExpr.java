/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;

public abstract class NeighborhoodQueryExpr extends Expression
{
	protected final Expression startNodeExpr;
	protected final Expression incidentEdgeTypeExpr;
	protected final int direction;
	protected final Expression adjacentNodeTypeExpr;

	public static final int ADJACENT = 0;
	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public NeighborhoodQueryExpr(String name, Type type,
			Expression startNodeExpression,
			Expression incidentEdgeTypeExpr, int direction,
			Expression adjacentNodeTypeExpr)
	{
		super(name, type);
		this.startNodeExpr = startNodeExpression;
		this.incidentEdgeTypeExpr = incidentEdgeTypeExpr;
		this.direction = direction;
		this.adjacentNodeTypeExpr = adjacentNodeTypeExpr;
	}

	public Expression getStartNodeExpr()
	{
		return startNodeExpr;
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
}
