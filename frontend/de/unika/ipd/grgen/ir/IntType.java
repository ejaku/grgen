/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BasicTypeNode;

/**
 * A Integer type.
 */
public class IntType extends PrimitiveType {
	public IntType(Ident ident) {
		super("integer type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_INTEGER;
	}
	
	public static Type getType() {
		return BasicTypeNode.intType.checkIR(Type.class);
	}
}
