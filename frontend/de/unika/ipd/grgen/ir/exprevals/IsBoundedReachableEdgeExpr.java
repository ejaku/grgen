/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class IsBoundedReachableEdgeExpr extends Expression {
	private final Expression startNodeExpr;
	private final Expression endEdgeExpr;
	private final Expression depthExpr;
	private final Expression incidentEdgeTypeExpr;
	private final int direction;
	private final Expression adjacentNodeTypeExpr;

	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public IsBoundedReachableEdgeExpr(Expression startNodeExpression, 
			Expression endNodeExpression, Expression depthExpression,
			Expression incidentEdgeTypeExpr, int direction,
			Expression adjacentNodeTypeExpr, Type type) {
		super("is bounded reachable edge expression", type);
		this.startNodeExpr = startNodeExpression;
		this.endEdgeExpr = endNodeExpression;
		this.depthExpr = depthExpression;
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

	public Expression getDepthExpr() {
		return depthExpr;
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
		depthExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}

