/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class InducedSubgraphExpr extends Expression {
	private final Expression setExpr;

	public InducedSubgraphExpr(Expression setExpr, Type type) {
		super("induced subgraph expression", type);
		this.setExpr= setExpr;
	}

	public Expression getSetExpr() {
		return setExpr;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		setExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}

