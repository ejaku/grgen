/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.type.basic;

import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * The void type.
 */
public class VoidType extends PrimitiveType
{
	public VoidType(Ident ident)
	{
		super("void type", ident);
	}

	@Override
	public boolean isVoid()
	{
		return true;
	}

	@Override
	public boolean isEqual(Type t)
	{
		return t.isVoid();
	}

	public static Type getType()
	{
		return BasicTypeNode.voidType.checkIR(Type.class);
	}
}
