/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BasicTypeNode;

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
