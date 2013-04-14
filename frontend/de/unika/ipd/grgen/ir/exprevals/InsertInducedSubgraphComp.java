/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class InsertInducedSubgraphComp extends ComputationInvocationBase {
	private final Expression nodeSetExpr;
	private final Expression nodeExpr;

	public InsertInducedSubgraphComp(Expression nodeSetExpr, Expression nodeExpr) {
		super("insert induced subgraph computation");
		this.nodeSetExpr = nodeSetExpr;
		this.nodeExpr = nodeExpr;
	}

	public Expression getSetExpr() {
		return nodeSetExpr;
	}

	public Expression getNodeExpr() {
		return nodeExpr;
	}

	public ComputationBase getComputationBase() {
		return null; // dummy needed for interface, not accessed cause real type defines computation
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		nodeSetExpr.collectNeededEntities(needs);
		nodeExpr.collectNeededEntities(needs);
	}
}

