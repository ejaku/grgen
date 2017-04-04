/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.exprevals.*;;

/**
 * A exec variable expression node.
 */
public class ExecVariableExpression extends Expression {
	private ExecVariable var;

	public ExecVariableExpression(ExecVariable var) {
		super("exec variable", var.getType());
		this.var = var;
	}

	/** Returns the exec variable of this exec variable expression. */
	public ExecVariable getVariable() {
		return var;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
	}

	public boolean equals(Object other) {
		if(!(other instanceof ExecVariableExpression)) return false;
		return var == ((ExecVariableExpression) other).getVariable();
	}

	public int hashCode() {
		return var.hashCode();
	}
}
