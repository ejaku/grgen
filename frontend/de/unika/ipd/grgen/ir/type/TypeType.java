/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author adam
 */

package de.unika.ipd.grgen.ir.type;

import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.Ident;

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
	public int classify()
	{
		return IS_TYPE;
	}

	public static Type getType()
	{
		return BasicTypeNode.typeType.checkIR(Type.class);
	}
}
