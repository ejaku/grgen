/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class InsertDefinedSubgraphExpr extends Expression {
	private final Expression edgeSetExpr;
	private final Expression edgeExpr;

	public InsertDefinedSubgraphExpr(Expression var, Expression edge, Type type) {
		super("insert defined subgraph expression", type);
		this.edgeSetExpr = var;
		this.edgeExpr = edge;
	}

	public Expression getSetExpr() {
		return edgeSetExpr;
	}

	public Expression getEdgeExpr() {
		return edgeExpr;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edgeSetExpr.collectNeededEntities(needs);
		edgeExpr.collectNeededEntities(needs);
	}
}

