/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.type.basic;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.LongType;

/**
 * The long basic type.
 */
public class LongTypeNode extends BasicTypeNode
{
	static {
		setName(LongTypeNode.class, "long type");
	}

	@Override
	protected IR constructIR()
	{
		return new LongType(getIdentNode().getIdent());
	}

	@Override
	public String toString()
	{
		return "long";
	}
}
