/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class InsertInducedSubgraphExpr extends Expression {
	private final Expression nodeSetExpr;
	private final Expression nodeExpr;

	public InsertInducedSubgraphExpr(Expression nodeSetExpr, Expression nodeExpr, Type type) {
		super("insert induced subgraph expression", type);
		this.nodeSetExpr = nodeSetExpr;
		this.nodeExpr = nodeExpr;
	}

	public Expression getSetExpr() {
		return nodeSetExpr;
	}

	public Expression getNodeExpr() {
		return nodeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		nodeSetExpr.collectNeededEntities(needs);
		nodeExpr.collectNeededEntities(needs);
	}
}

