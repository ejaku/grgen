/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author adam
 */

package de.unika.ipd.grgen.ir.type.basic;

import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A Type type.
 */
public class TypeType extends PrimitiveType
{
	public TypeType(Ident ident)
	{
		super("type type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_TYPE;
	}

	public static Type getType()
	{
		return BasicTypeNode.typeType.checkIR(Type.class);
	}
}
