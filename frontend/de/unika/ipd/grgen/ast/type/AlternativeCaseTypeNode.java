/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.type;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;

/**
 * Type of alternative case node declaration.
 */
public class AlternativeCaseTypeNode extends TypeNode
{
	static {
		setName(AlternativeCaseTypeNode.class, "alternative case type");
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	protected Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}
}
