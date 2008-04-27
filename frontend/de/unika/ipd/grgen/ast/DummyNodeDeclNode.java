/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Node;


/**
 * Dummy node needed for dangling edges
 */
public class DummyNodeDeclNode extends NodeDeclNode
{
	static {
		setName(DummyNodeDeclNode.class, "dummy node");
	}

	public DummyNodeDeclNode(IdentNode id, BaseNode type, int context) {
		super(id, type, context, TypeExprNode.getEmpty());
	}

	public Node getNode() {
		return null;
	}

	public boolean isDummy() {
		return true;
	}

	public String toString() {
		return "a dummy node";
	}
};
