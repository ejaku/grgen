/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.LongType;

/**
 * The long basic type.
 */
public class LongTypeNode extends BasicTypeNode
{
	static {
		setName(LongTypeNode.class, "long type");
	}

	@Override
	protected IR constructIR() {
		return new LongType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "long";
	}
};
