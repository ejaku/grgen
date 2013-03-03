/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ast.BaseNode;

public class GraphAddEdgeExpr extends Expression {
	private final Node sourceNode;
	private final Node targetNode;
	private final EdgeType edgeType;

	public GraphAddEdgeExpr(EdgeType edgeType,
			Node sourceNode,
			Node targetNode,
			Type type) {
		super("graph add edge expression", type);
		this.edgeType = edgeType;
		this.sourceNode = sourceNode;
		this.targetNode = targetNode;
	}

	public EdgeType getEdgeType() {
		return edgeType;
	}

	public Node getSourceNode() {
		return sourceNode;
	}

	public Node getTargetNode() {
		return targetNode;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		if(!isGlobalVariable(sourceNode) && (sourceNode.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(sourceNode);
		if(!isGlobalVariable(targetNode) && (targetNode.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(targetNode);
	}
}

