/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.type.basic;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.StringType;

/**
 * The string basic type.
 */
public class StringTypeNode extends BasicTypeNode
{
	static {
		setName(StringTypeNode.class, "string type");
	}

	@Override
	protected IR constructIR()
	{
		return new StringType(getIdentNode().getIdent());
	}

	@Override
	public String toString()
	{
		return "string";
	}
}
