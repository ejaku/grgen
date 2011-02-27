/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A variable declared inside an "exec" statement containing nodes, edges or primitive types.
 * (due to being declared in the sequence, it can't be a defToBeYieldedTo variable, only entities from the outside can be)
 */
public class ExecVariable extends Entity {
	public ExecVariable(String name, Ident ident, Type type, int context) {
		super(name, ident, type, false, false, context);
	}
}
