/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 * @version $Id$
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
