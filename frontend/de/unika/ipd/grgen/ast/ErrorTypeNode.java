/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.VoidType;

/**
 * The error basic type. It is compatible to no other type.
 * TODO: Why compatible to no other type? The error node within an compiler
 * should be compatible to every other node, to protect against error avalanches
 */
public class ErrorTypeNode extends TypeNode {
	static {
		setName(ErrorTypeNode.class, "error type");
	}

	private IdentNode id;

	public ErrorTypeNode(IdentNode id) {
		this.id = id;
		setCoords(id.getCoords());
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	@Override
	protected IR constructIR() {
		return new VoidType(id.getIdent());
	}

	public static String getUseStr() {
		return "error type";
	}

	@Override
	public String toString() {
		return "error type";
	}
};
