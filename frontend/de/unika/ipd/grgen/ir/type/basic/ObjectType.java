/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ObjectType.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.ir.type.basic;

import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.type.Type;

public class ObjectType extends PrimitiveType
{
	/** @param ident The name of the boolean type. */
	public ObjectType(Ident ident)
	{
		super("object type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_OBJECT;
	}

	public static Type getType()
	{
		return BasicTypeNode.objectType.checkIR(Type.class);
	}
}
