/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

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
