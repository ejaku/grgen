/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class ReachableEdgeExpr extends Expression {
	private final Expression startNodeExpr;
	private final Expression incidentEdgeTypeExpr;
	private final int direction;
	private final Expression adjacentNodeTypeExpr;

	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public ReachableEdgeExpr(Expression startNodeExpression,
			Expression incidentEdgeTypeExpr, int direction,
			Expression adjacentNodeTypeExpr, Type type) {
		super("reachable edge expression", type);
		this.startNodeExpr = startNodeExpression;
		this.incidentEdgeTypeExpr = incidentEdgeTypeExpr;
		this.direction = direction;
		this.adjacentNodeTypeExpr = adjacentNodeTypeExpr;
	}

	public Expression getStartNodeExpr() {
		return startNodeExpr;
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
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}

