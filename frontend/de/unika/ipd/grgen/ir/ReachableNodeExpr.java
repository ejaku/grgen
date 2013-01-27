/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

public class ReachableNodeExpr extends Expression {
	private final Node node;
	private final EdgeType incidentEdgeType;
	private final int direction;
	private final NodeType adjacentNodeType;

	public static final int ADJACENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;

	public ReachableNodeExpr(Node node,
			EdgeType incidentEdgeType, int direction,
			NodeType adjacentNodeType, Type type) {
		super("reachable node expression", type);
		this.node = node;
		this.incidentEdgeType = incidentEdgeType;
		this.direction = direction;
		this.adjacentNodeType = adjacentNodeType;
	}

	public Node getNode() {
		return node;
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
		if(!isGlobalVariable(node))
			needs.add(node);
	}
}

