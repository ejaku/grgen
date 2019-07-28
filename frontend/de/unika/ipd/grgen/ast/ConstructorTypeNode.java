/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

/**
 * Type of constructor declaration nodes.
 */
public class ConstructorTypeNode extends TypeNode {
	static Vector<BaseNode> emptyChildren = new Vector<BaseNode>();
	static Vector<String> emptyChildrenNames = new Vector<String>();
	static {
		setName(ConstructorTypeNode.class, "constructor type");
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		return emptyChildren;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		return emptyChildrenNames;
	}
}
