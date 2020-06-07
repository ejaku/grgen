/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type.container;

import de.unika.ipd.grgen.ir.type.Type;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque 
//should be unified in abstract base classes and algorithms working on them

public class MapType extends ContainerType
{
	public Type keyType;
	public Type valueType;

	public MapType(Type keyType, Type valueType)
	{
		super("map type");
		this.keyType = keyType;
		this.valueType = valueType;
	}

	public Type getKeyType()
	{
		return keyType;
	}

	public Type getValueType()
	{
		return valueType;
	}

	@Override
	public String toString()
	{
		return "map<" + keyType + "," + valueType + ">";
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_MAP;
	}
	
	@Override
	public Type getElementType()
	{
		return keyType;
	}
}
