/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class InsertDefinedSubgraphComp extends ComputationInvocationBase {
	private final Expression edgeSetExpr;
	private final Expression edgeExpr;

	public InsertDefinedSubgraphComp(Expression var, Expression edge) {
		super("insert defined subgraph computation");
		this.edgeSetExpr = var;
		this.edgeExpr = edge;
	}

	public Expression getSetExpr() {
		return edgeSetExpr;
	}

	public Expression getEdgeExpr() {
		return edgeExpr;
	}

	public ComputationBase getComputationBase() {
		return null; // dummy needed for interface, not accessed cause real type defines computation
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edgeSetExpr.collectNeededEntities(needs);
		edgeExpr.collectNeededEntities(needs);
	}
}

