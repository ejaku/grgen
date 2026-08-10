/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.type.basic;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.VoidType;

/**
 * The void basic type. It is compatible to no other type.
 */
public class VoidTypeNode extends BasicTypeNode
{
	static {
		setClassName(VoidTypeNode.class, "void type");
	}

	@Override
	protected IR constructIR()
	{
		return new VoidType(getIdent().getIRIdent());
	}

	@Override
	public String toString()
	{
		return "void";
	}
}
