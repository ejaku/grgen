/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.FloatType;
import de.unika.ipd.grgen.ir.IR;

/**
 * The floating point basic type.
 */
public class FloatTypeNode extends BasicTypeNode
{
	static {
		setName(FloatTypeNode.class, "float type");
	}

	protected IR constructIR() {
		return new FloatType(getIdentNode().getIdent());
	}
	public String toString() {
		return "float";
	}
};
