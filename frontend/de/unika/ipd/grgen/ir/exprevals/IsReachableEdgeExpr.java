/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class IsReachableEdgeExpr extends Expression {
	private final Expression startNodeExpr;
	private final Expression endEdgeExpr;
	private final Expression incidentEdgeTypeExpr;
	private final int direction;
	private final Expression adjacentNodeTypeExpr;

	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public IsReachableEdgeExpr(Expression startNodeExpression, Expression endNodeExpression,
			Expression incidentEdgeTypeExpr, int direction,
			Expression adjacentNodeTypeExpr, Type type) {
		super("is reachable edge expression", type);
		this.startNodeExpr = startNodeExpression;
		this.endEdgeExpr = endNodeExpression;
		this.incidentEdgeTypeExpr = incidentEdgeTypeExpr;
		this.direction = direction;
		this.adjacentNodeTypeExpr = adjacentNodeTypeExpr;
	}

	public Expression getStartNodeExpr() {
		return startNodeExpr;
	}

	public Expression getEndEdgeExpr() {
		return endEdgeExpr;
	}

	public Expression getIncidentEdgeTypeExpr() {
		return incidentEdgeTypeExpr;
	}

	public int Direction() {
		return direction;
	}

	public Expression getAdjacentNodeTypeExpr() {
		return adjacentNodeTypeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		startNodeExpr.collectNeededEntities(needs);
		endEdgeExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}

