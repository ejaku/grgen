/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class GraphAddEdgeExpr extends Expression {
	private final Expression sourceNode;
	private final Expression targetNode;
	private final Expression edgeType;

	public GraphAddEdgeExpr(Expression edgeType,
			Expression sourceNode,
			Expression targetNode,
			Type type) {
		super("graph add edge expression", type);
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

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edgeType.collectNeededEntities(needs);
		sourceNode.collectNeededEntities(needs);
		targetNode.collectNeededEntities(needs);
	}
}

