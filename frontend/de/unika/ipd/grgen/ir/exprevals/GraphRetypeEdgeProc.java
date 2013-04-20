/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphRetypeEdgeProc extends ProcedureInvocationBase {
	private final Expression edge;
	private final Expression newEdgeType;

	public GraphRetypeEdgeProc(Expression edge, Expression newEdgeType) {
		super("graph retype edge procedure");
		this.edge = edge;
		this.newEdgeType = newEdgeType;
	}

	public Expression getEdgeExpr() {
		return edge;
	}

	public Expression getNewEdgeTypeExpr() {
		return newEdgeType;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edge.collectNeededEntities(needs);
		newEdgeType.collectNeededEntities(needs);
	}
}

