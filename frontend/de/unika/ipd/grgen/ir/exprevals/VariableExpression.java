/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

/**
 * A variable expression node.
 */
public class VariableExpression extends Expression {
	private Variable var;

	public VariableExpression(Variable var) {
		super("variable", var.getType());
		this.var = var;
	}

	/** Returns the variable of this variable expression. */
	public Variable getVariable() {
		return var;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		if(!isGlobalVariable(var))
			needs.add(var);
	}

	public boolean equals(Object other) {
		if(!(other instanceof VariableExpression)) return false;
		return var == ((VariableExpression) other).getVariable();
	}

	public int hashCode() {
		return var.hashCode();
	}
}
