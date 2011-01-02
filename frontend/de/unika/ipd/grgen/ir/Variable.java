/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Represents a "var" parameter of an action.
 *
 * @author Moritz Kroll
 * @version $Id$
 */

package de.unika.ipd.grgen.ir;

public class Variable extends Entity {
	// the pattern graph of the variable
	public PatternGraph directlyNestingLHSGraph;
	
	public Variable(String name, Ident ident, Type type,
			PatternGraph directlyNestingLHSGraph, int context) {
		super(name, ident, type, false, context);
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}
}

