/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type.container;

import de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
import de.unika.ipd.grgen.ir.type.Type;

public class SetType extends ContainerType
{
	public Type valueType;

	public SetType(Type valueType)
	{
		super("set type");
		this.valueType = valueType;
	}

	public Type getValueType()
	{
		return valueType;
	}

	@Override
	public String toString()
	{
		return "set<" + valueType + ">";
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_SET;
	}
	
	@Override
	public Type getElementType()
	{
		return valueType;
	}
	
	@Override
	public boolean containsBaseInternalObjectType()
	{
		return valueType instanceof BaseInternalObjectType;
	}
}
