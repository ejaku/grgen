/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type.container;

import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;

public abstract class ContainerTypeNode extends DeclaredTypeNode
{
	static {
		setName(ContainerTypeNode.class, "container type");
	}
	
	@Override
	public String getName()
	{
		return getTypeName();
	}

	// returns value type for array|deque|set and key type for map
	public abstract TypeNode getElementType();
}
