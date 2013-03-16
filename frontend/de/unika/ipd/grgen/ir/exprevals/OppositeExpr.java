/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class OppositeExpr extends Expression {
	private final Expression edge;
	private final Expression node;

	public OppositeExpr(Expression edge, Expression node, Type type) {
		super("opposite expression", type);
		this.edge = edge;
		this.node = node;
	}

	public Expression getEdgeExpr() {
		return edge;
	}

	public Expression getNodeExpr() {
		return node;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		edge.collectNeededEntities(needs);
		node.collectNeededEntities(needs);
	}
}

