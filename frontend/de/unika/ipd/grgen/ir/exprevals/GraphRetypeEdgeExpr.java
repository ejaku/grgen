/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ast.BaseNode;

public class GraphRetypeEdgeExpr extends Expression {
	private final Edge edge;
	private final EdgeType newEdgeType;

	public GraphRetypeEdgeExpr(Edge edge,
			EdgeType newEdgeType,
			Type type) {
		super("graph retype edge expression", type);
		this.edge = edge;
		this.newEdgeType = newEdgeType;
	}

	public Edge getEdge() {
		return edge;
	}

	public EdgeType getNewEdgeType() {
		return newEdgeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		if(!isGlobalVariable(edge) && (edge.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(edge);
	}
}

