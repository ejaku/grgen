/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Represents a "var" parameter of an action.
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ir.pattern;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;

public class Variable extends Entity
{
	// the pattern graph of the variable
	public PatternGraphLhs directlyNestingLHSGraph;

	// null or an expression used to initialize the variable
	public Expression initialization;

	public Variable(String name, Ident ident, Type type, boolean isDefToBeYieldedTo,
			PatternGraphLhs directlyNestingLHSGraph, int context)
	{
		super(name, ident, type, false, isDefToBeYieldedTo, context);
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}

	public void setInitialization(Expression initialization)
	{
		this.initialization = initialization;
	}
}
