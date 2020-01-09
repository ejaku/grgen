/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.ShortType;

/**
 * The short basic type.
 */
public class ShortTypeNode extends BasicTypeNode
{
	static {
		setName(ShortTypeNode.class, "short type");
	}

	@Override
	protected IR constructIR() {
		return new ShortType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "short";
	}
};
