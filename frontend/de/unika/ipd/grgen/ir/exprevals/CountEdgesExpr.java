/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class CountEdgesExpr extends Expression {
	private final Expression edgeType;

	public CountEdgesExpr(Expression edgeType) {
		super("count edges expression", IntType.getType());
		this.edgeType = edgeType;
	}

	public Expression getEdgeTypeExpr() {
		return edgeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edgeType.collectNeededEntities(needs);
	}
}

