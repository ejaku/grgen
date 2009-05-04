/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.VoidType;

/**
 * The error basic type. It is compatible to no other type.
 * TODO: Why compatible to no other type? The error node within an compiler
 * should be compatible to every other node, to protect against error avalanches
 */
class ErrorTypeNode extends TypeNode {
	static {
		setName(ErrorTypeNode.class, "error type");
	}

	private IdentNode id;

	public ErrorTypeNode(IdentNode id) {
		this.id = id;
		setCoords(id.getCoords());
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	protected IR constructIR() {
		return new VoidType(id.getIdent());
	}

	public static String getUseStr() {
		return "error type";
	}

	public String toString() {
		return "error type";
	}
};
