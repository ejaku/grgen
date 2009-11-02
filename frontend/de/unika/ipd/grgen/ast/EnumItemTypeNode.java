/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IntType;

/**
 * The enum member type.
 */
public class EnumItemTypeNode extends BasicTypeNode
{
	static {
		setName(EnumItemTypeNode.class, "enum item type");
	}

	@Override
	protected IR constructIR() {
		return new IntType(getIdentNode().getIdent());
	}
};
