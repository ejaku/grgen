/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IntType;

/**
 * The integer basic type.
 */
public class IntTypeNode extends BasicTypeNode
{
	static {
		setName(IntTypeNode.class, "int type");
	}

	@Override
	protected IR constructIR() {
		return new IntType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "int";
	}
};
