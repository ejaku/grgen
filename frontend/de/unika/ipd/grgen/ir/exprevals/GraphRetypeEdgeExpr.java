/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class GraphRetypeEdgeExpr extends Expression {
	private final Expression edge;
	private final Expression newEdgeType;

	public GraphRetypeEdgeExpr(Expression edge,
			Expression newEdgeType,
			Type type) {
		super("graph retype edge expression", type);
		this.edge = edge;
		this.newEdgeType = newEdgeType;
	}

	public Expression getEdgeExpr() {
		return edge;
	}

	public Expression getNewEdgeTypeExpr() {
		return newEdgeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edge.collectNeededEntities(needs);
		newEdgeType.collectNeededEntities(needs);
	}
}

