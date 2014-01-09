/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class InsertDefinedSubgraphProc extends ProcedureInvocationBase {
	private final Expression edgeSetExpr;
	private final Expression edgeExpr;

	public InsertDefinedSubgraphProc(Expression var, Expression edge) {
		super("insert defined subgraph procedure");
		this.edgeSetExpr = var;
		this.edgeExpr = edge;
	}

	public Expression getSetExpr() {
		return edgeSetExpr;
	}

	public Expression getEdgeExpr() {
		return edgeExpr;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edgeSetExpr.collectNeededEntities(needs);
		edgeExpr.collectNeededEntities(needs);
	}
}

