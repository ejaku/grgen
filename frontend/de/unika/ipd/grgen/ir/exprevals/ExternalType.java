/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

/**
 * IR class that represents external types.
 */
public class ExternalType extends InheritanceType {
	/**
	 * Make a new external type.
	 * @param ident The identifier that declares this type.
	 */
	public ExternalType(Ident ident) {
		super("node type", ident, 0, null);
	}

	/** Return a classification of a type for the IR. */
	public int classify() {
		return IS_EXTERNAL_TYPE;
	}
}
