/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.type.basic;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.type.Type;

//import de.unika.ipd.grgen.ast.BasicTypeNode;

public class UntypedExecVarType extends Type
{
	/** @param ident The name of the type. */
	public UntypedExecVarType(Ident ident)
	{
		super("untyped exec var type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_UNTYPED_EXEC_VAR_TYPE;
	}

	public static Type getType()
	{
		return null;
	}
}
