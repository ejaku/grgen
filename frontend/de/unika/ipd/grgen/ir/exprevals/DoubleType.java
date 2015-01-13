/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ast.BasicTypeNode;
import de.unika.ipd.grgen.ir.*;

/**
 * A double precision floating point type.
 */
public class DoubleType extends PrimitiveType {

	public DoubleType(Ident ident) {
		super("double type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_DOUBLE;
	}

	public static Type getType() {
		return BasicTypeNode.doubleType.checkIR(Type.class);
	}
}
