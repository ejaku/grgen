/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

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
