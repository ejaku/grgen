/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * Represents a "var" parameter of an action.
 *
 * @author Moritz Kroll
 * @version $Id$
 */

package de.unika.ipd.grgen.ir;

public class Variable extends Entity {
	public Variable(String name, Ident ident, Type type) {
		super(name, ident, type, false);
	}
}

