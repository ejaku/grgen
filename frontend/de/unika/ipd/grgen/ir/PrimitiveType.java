/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A Primitive type.
 */
public class PrimitiveType extends Type {

	/**
	 * Make a new primitive type.
	 * @param name Name of the primitive type.
	 */
	public PrimitiveType(String name, Ident ident) {
		super(name, ident);
	}
}
