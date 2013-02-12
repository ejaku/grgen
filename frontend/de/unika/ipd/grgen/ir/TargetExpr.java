/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BaseNode;

public class TargetExpr extends Expression {
	private final Edge edge;

	public TargetExpr(Edge edge, Type type) {
		super("target expression", type);
		this.edge = edge;
	}

	public Edge getEdge() {
		return edge;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		if(!isGlobalVariable(edge) && (edge.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(edge);
	}
}

