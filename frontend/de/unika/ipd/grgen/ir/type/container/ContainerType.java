/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type.container;

import de.unika.ipd.grgen.ir.type.Type;

public abstract class ContainerType extends Type
{
	public ContainerType(String name)
	{
		super(name, null);
	}
	
	// returns value type for array|deque|set and key type for map
	public abstract Type getElementType();
	
	public abstract boolean containsBaseInternalObjectType();
}
