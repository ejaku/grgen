/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

	protected IR constructIR() {
		return new StringType(getIdentNode().getIdent());
	}
	public String toString() {
		return "string";
	}
};
