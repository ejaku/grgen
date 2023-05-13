/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type.container;

import de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
import de.unika.ipd.grgen.ir.type.Type;

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
	
	@Override
	public boolean containsBaseInternalObjectType()
	{
		return keyType instanceof BaseInternalObjectType || valueType instanceof BaseInternalObjectType;
	}
}
