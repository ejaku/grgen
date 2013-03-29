/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class GraphRetypeNodeExpr extends Expression {
	private final Expression node;
	private final Expression newNodeType;

	public GraphRetypeNodeExpr(Expression node,
			Expression newNodeType, Type type) {
		super("graph retype node expression", type);
		this.node = node;
		this.newNodeType = newNodeType;
	}

	public Expression getNodeExpr() {
		return node;
	}

	public Expression getNewNodeTypeExpr() {
		return newNodeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		node.collectNeededEntities(needs);
		newNodeType.collectNeededEntities(needs);
	}
}

