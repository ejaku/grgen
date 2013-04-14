/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphRedirectSourceAndTargetComp extends ComputationInvocationBase {
	private Expression edge;
	private Expression newSource;
	private Expression newTarget;
	private Expression oldSourceName; // optional
	private Expression oldTargetName; // optional

	public GraphRedirectSourceAndTargetComp(Expression edge, Expression newSource, Expression newTarget, 
			Expression oldSourceName, Expression oldTargetName) {
		super("graph redirect source and target computation");
		this.edge = edge;
		this.newSource = newSource;
		this.newTarget = newTarget;
		this.oldSourceName = oldSourceName;
		this.oldTargetName = oldTargetName;
	}

	public Expression getEdge() {
		return edge;
	}

	public Expression getNewSource() {
		return newSource;
	}

	public Expression getNewTarget() {
		return newTarget;
	}

	public Expression getOldSourceName() {
		return oldSourceName;
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
		newSource.collectNeededEntities(needs);
		newTarget.collectNeededEntities(needs);
	}
}
