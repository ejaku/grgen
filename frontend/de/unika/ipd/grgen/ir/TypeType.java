/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author adam
 * @version $$
 */
package de.unika.ipd.grgen.ir;


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
}
