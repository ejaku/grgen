/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class IsBoundedReachableNodeExpr extends Expression {
	private final Expression startNodeExpr;
	private final Expression endNodeExpr;
	private final Expression depthExpr;
	private final Expression incidentEdgeTypeExpr;
	private final int direction;
	private final Expression adjacentNodeTypeExpr;

	public static final int ADJACENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public IsBoundedReachableNodeExpr(Expression startNodeExpression, 
			Expression endNodeExpression, Expression depthExpression,
			Expression incidentEdgeTypeExpr, int direction,
			Expression adjacentNodeTypeExpr, Type type) {
		super("is bouneded reachable node expression", type);
		this.startNodeExpr = startNodeExpression;
		this.endNodeExpr = endNodeExpression;
		this.depthExpr = depthExpression;
		this.incidentEdgeTypeExpr = incidentEdgeTypeExpr;
		this.direction = direction;
		this.adjacentNodeTypeExpr = adjacentNodeTypeExpr;
	}

	public Expression getStartNodeExpr() {
		return startNodeExpr;
	}

	public Expression getEndNodeExpr() {
		return endNodeExpr;
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
		endNodeExpr.collectNeededEntities(needs);
		depthExpr.collectNeededEntities(needs);
		incidentEdgeTypeExpr.collectNeededEntities(needs);
		adjacentNodeTypeExpr.collectNeededEntities(needs);
	}
}

