/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type.container;

import de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
import de.unika.ipd.grgen.ir.type.Type;

public class DequeType extends ContainerType
{
	public Type valueType;

	public DequeType(Type valueType)
	{
		super("deque type");
		this.valueType = valueType;
	}

	public Type getValueType()
	{
		return valueType;
	}

	@Override
	public String toString()
	{
		return "deque<" + valueType + ">";
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_DEQUE;
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
