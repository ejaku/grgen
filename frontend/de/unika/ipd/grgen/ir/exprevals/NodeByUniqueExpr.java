/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class NodeByUniqueExpr extends Expression {
	private final Expression unique;
	private final Expression nodeType;

	public NodeByUniqueExpr(Expression unique, Expression nodeType, Type type) {
		super("node by unique id expression", type);
		this.unique = unique;
		this.nodeType = nodeType;
	}

	public Expression getUniqueExpr() {
		return unique;
	}

	public Expression getNodeTypeExpr() {
		return nodeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		unique.collectNeededEntities(needs);
		nodeType.collectNeededEntities(needs);
	}
}

