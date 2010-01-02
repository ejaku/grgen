/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BasicTypeNode;

/**
 * A single precision floating point type.
 */
public class FloatType extends PrimitiveType {

	public FloatType(Ident ident) {
		super("float type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_FLOAT;
	}

	public static Type getType() {
		return BasicTypeNode.floatType.checkIR(Type.class);
	}
}
