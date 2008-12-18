/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.TypeType;

/**
 * The type basic type.
 */
public class TypeTypeNode extends BasicTypeNode
{
	static {
		setName(TypeTypeNode.class, "type type");
	}

	protected IR constructIR() {
		return new TypeType(getIdentNode().getIdent());
	}
};
