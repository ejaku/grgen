/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.type;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

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
		List<BaseNode> children = new ArrayList<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		// no children
		return childrenNames;
	}
}
