/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class CanonizeExpr extends Expression {
	private Expression graphExpr;

	public CanonizeExpr(Expression graphExpr) {
		super("canonize expr", StringType.getType());
		this.graphExpr = graphExpr;
	}

	public Expression getGraphExpr() {
		return graphExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		graphExpr.collectNeededEntities(needs);
	}
}
