/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphRedirectTargetComp extends ComputationInvocationBase {
	private Expression edge;
	private Expression newTarget;
	private Expression oldTargetName; // optional

	public GraphRedirectTargetComp(Expression edge, Expression newTarget, Expression oldTargetName) {
		super("graph redirect target computation");
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

	public ComputationBase getComputationBase() {
		return null; // dummy needed for interface, not accessed cause real type defines computation
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edge.collectNeededEntities(needs);
		newTarget.collectNeededEntities(needs);
	}
}
