/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class InsertCopyProc extends ProcedureInvocationBase {
	private final Expression graphExpr;
	private final Expression nodeExpr;

	public InsertCopyProc(Expression graphExpr, Expression nodeExpr) {
		super("insert copy procedure");
		this.graphExpr = graphExpr;
		this.nodeExpr = nodeExpr;
	}

	public Expression getGraphExpr() {
		return graphExpr;
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
		graphExpr.collectNeededEntities(needs);
		nodeExpr.collectNeededEntities(needs);
	}
}

