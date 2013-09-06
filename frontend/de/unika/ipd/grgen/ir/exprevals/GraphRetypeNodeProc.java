/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphRetypeNodeProc extends ProcedureInvocationBase {
	private final Expression node;
	private final Expression newNodeType;

	public GraphRetypeNodeProc(Expression node, Expression newNodeType) {
		super("graph retype node procedure");
		this.node = node;
		this.newNodeType = newNodeType;
	}

	public Expression getNodeExpr() {
		return node;
	}

	public Expression getNewNodeTypeExpr() {
		return newNodeType;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		node.collectNeededEntities(needs);
		newNodeType.collectNeededEntities(needs);
	}
}

