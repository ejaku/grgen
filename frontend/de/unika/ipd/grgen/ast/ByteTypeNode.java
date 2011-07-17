/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ByteType;

/**
 * The byte basic type.
 */
public class ByteTypeNode extends BasicTypeNode
{
	static {
		setName(ByteTypeNode.class, "byte type");
	}

	@Override
	protected IR constructIR() {
		return new ByteType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "byte";
	}
};
