/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.type;

import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.*;

/**
 * A single precision floating point type.
 */
public class FloatType extends PrimitiveType
{
	public FloatType(Ident ident)
	{
		super("float type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	public int classify()
	{
		return IS_FLOAT;
	}

	public static Type getType()
	{
		return BasicTypeNode.floatType.checkIR(Type.class);
	}
}
