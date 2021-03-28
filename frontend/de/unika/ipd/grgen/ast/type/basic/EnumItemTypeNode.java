/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.type.basic;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.IntType;

/**
 * The enum member type.
 */
public class EnumItemTypeNode extends BasicTypeNode
{
	static {
		setName(EnumItemTypeNode.class, "enum item type");
	}

	@Override
	protected IR constructIR()
	{
		return new IntType(getIdentNode().getIdent());
	}
}
