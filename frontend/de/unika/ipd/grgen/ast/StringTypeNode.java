/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.StringType;

/**
 * The string basic type.
 */
public class StringTypeNode extends BasicTypeNode
{
	static {
		setName(StringTypeNode.class, "string type");
	}

	@Override
	protected IR constructIR() {
		return new StringType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "string";
	}
};
