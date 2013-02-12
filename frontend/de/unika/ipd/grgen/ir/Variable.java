/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Represents a "var" parameter of an action.
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ir;

public class Variable extends Entity {
	// the pattern graph of the variable
	public PatternGraph directlyNestingLHSGraph;

	// null or an expression used to initialize the variable
	public Expression initialization;

	public Variable(String name, Ident ident, Type type, boolean isDefToBeYieldedTo,
			PatternGraph directlyNestingLHSGraph, int context) {
		super(name, ident, type, false, isDefToBeYieldedTo, context);
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}
	
	public void setInitialization(Expression initialization) {
		this.initialization = initialization;
	}
}

