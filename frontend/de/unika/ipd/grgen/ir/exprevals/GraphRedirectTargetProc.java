/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphRedirectTargetProc extends ProcedureInvocationBase {
	private Expression edge;
	private Expression newTarget;
	private Expression oldTargetName; // optional

	public GraphRedirectTargetProc(Expression edge, Expression newTarget, Expression oldTargetName) {
		super("graph redirect target procedure");
		this.edge = edge;
		this.newTarget = newTarget;
		this.oldTargetName = oldTargetName;
	}

	public Expression getEdge() {
		return edge;
	}

	public Expression getNewTarget() {
		return newTarget;
	}

	public Expression getOldTargetName() {
		return oldTargetName;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edge.collectNeededEntities(needs);
		newTarget.collectNeededEntities(needs);
	}
}
