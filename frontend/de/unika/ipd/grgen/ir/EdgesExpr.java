/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

public class EdgesExpr extends Expression {
	private final EdgeType edgeType;

	public EdgesExpr(EdgeType edgeType, Type type) {
		super("edges expression", type);
		this.edgeType = edgeType;
	}

	public EdgeType getEdgeType() {
		return edgeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
	}
}

