/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.type.basic;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.ShortType;

/**
 * The short basic type.
 */
public class ShortTypeNode extends BasicTypeNode
{
	static {
		setName(ShortTypeNode.class, "short type");
	}

	@Override
	protected IR constructIR()
	{
		return new ShortType(getIdentNode().getIdent());
	}

	@Override
	public String toString()
	{
		return "short";
	}
}
