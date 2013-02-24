/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ast.BaseNode;

public class IsReachableNodeExpr extends Expression {
	private final Node startNode;
	private final Node endNode;
	private final EdgeType incidentEdgeType;
	private final int direction;
	private final NodeType adjacentNodeType;

	public static final int ADJACENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public IsReachableNodeExpr(Node startNode, Node endNode,
			EdgeType incidentEdgeType, int direction,
			NodeType adjacentNodeType, Type type) {
		super("reachable node expression", type);
		this.startNode = startNode;
		this.endNode = endNode;
		this.incidentEdgeType = incidentEdgeType;
		this.direction = direction;
		this.adjacentNodeType = adjacentNodeType;
	}

	public Node getStartNode() {
		return startNode;
	}

	public Node getEndNode() {
		return endNode;
	}

	public EdgeType getIncidentEdgeType() {
		return incidentEdgeType;
	}

	public int Direction() {
		return direction;
	}

	public NodeType getAdjacentNodeType() {
		return adjacentNodeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		if(!isGlobalVariable(startNode) && (startNode.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(startNode);
		if(!isGlobalVariable(endNode) && (endNode.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(endNode);
	}
}

