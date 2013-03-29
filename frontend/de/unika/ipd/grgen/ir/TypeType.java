/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author adam
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BasicTypeNode;

import de.unika.ipd.grgen.ir.exprevals.*;

/**
 * A Type type.
 */
public class TypeType extends PrimitiveType {

	public TypeType(Ident ident) {
		super("type type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_TYPE;
	}

	public static Type getType() {
		return BasicTypeNode.typeType.checkIR(Type.class);
	}
}
