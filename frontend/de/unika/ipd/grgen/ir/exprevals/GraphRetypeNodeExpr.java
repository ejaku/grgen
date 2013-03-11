/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ast.BaseNode;

public class GraphRetypeNodeExpr extends Expression {
	private final Node node;
	private final NodeType newNodeType;

	public GraphRetypeNodeExpr(Node node,
			NodeType newNodeType, Type type) {
		super("graph retype node expression", type);
		this.node = node;
		this.newNodeType = newNodeType;
	}

	public Node getNode() {
		return node;
	}

	public NodeType getNewNodeType() {
		return newNodeType;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		if(!isGlobalVariable(node) && (node.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(node);
	}
}

