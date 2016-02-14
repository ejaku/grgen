/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class InsertInducedSubgraphProc extends ProcedureInvocationBase {
	private final Expression nodeSetExpr;
	private final Expression nodeExpr;

	public InsertInducedSubgraphProc(Expression nodeSetExpr, Expression nodeExpr) {
		super("insert induced subgraph procedure");
		this.nodeSetExpr = nodeSetExpr;
		this.nodeExpr = nodeExpr;
	}

	public Expression getSetExpr() {
		return nodeSetExpr;
	}

	public Expression getNodeExpr() {
		return nodeExpr;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		nodeSetExpr.collectNeededEntities(needs);
		nodeExpr.collectNeededEntities(needs);
	}
}

