/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
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
