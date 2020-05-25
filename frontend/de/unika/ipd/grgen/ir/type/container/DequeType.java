/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type.container;

import de.unika.ipd.grgen.ir.type.Type;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque 
//should be unified in abstract base classes and algorithms working on them

public class DequeType extends Type
{
	public Type valueType;

	public DequeType(Type valueType)
	{
		super("deque type", null);
		this.valueType = valueType;
	}

	public Type getValueType()
	{
		return valueType;
	}

	public String toString()
	{
		return "deque<" + valueType + ">";
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	public int classify()
	{
		return IS_DEQUE;
	}
}