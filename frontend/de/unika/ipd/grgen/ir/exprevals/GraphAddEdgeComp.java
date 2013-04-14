/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphAddEdgeComp extends ComputationInvocationBase {
	private final Expression sourceNode;
	private final Expression targetNode;
	private final Expression edgeType;

	public GraphAddEdgeComp(Expression edgeType,
			Expression sourceNode,
			Expression targetNode) {
		super("graph add edge computation");
		this.edgeType = edgeType;
		this.sourceNode = sourceNode;
		this.targetNode = targetNode;
	}

	public Expression getEdgeTypeExpr() {
		return edgeType;
	}

	public Expression getSourceNodeExpr() {
		return sourceNode;
	}

	public Expression getTargetNodeExpr() {
		return targetNode;
	}

	public ComputationBase getComputationBase() {
		return null; // dummy needed for interface, not accessed cause real type defines computation
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edgeType.collectNeededEntities(needs);
		sourceNode.collectNeededEntities(needs);
		targetNode.collectNeededEntities(needs);
	}
}

