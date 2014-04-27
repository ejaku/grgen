/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class CountNodesExpr extends Expression {
	private final Expression nodeType;

	public CountNodesExpr(Expression nodeType) {
		super("count nodes expression", IntType.getType());
		this.nodeType = nodeType;
	}

	public Expression getNodeTypeExpr() {
		return nodeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		nodeType.collectNeededEntities(needs);
	}
}

