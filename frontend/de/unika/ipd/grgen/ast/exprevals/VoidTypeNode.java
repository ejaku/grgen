/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.VoidType;

/**
 * The void basic type. It is compatible to no other type.
 */
public class VoidTypeNode extends BasicTypeNode
{
	static {
		setName(VoidTypeNode.class, "void type");
	}

	@Override
	protected IR constructIR() {
		return new VoidType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "void";
	}
};
