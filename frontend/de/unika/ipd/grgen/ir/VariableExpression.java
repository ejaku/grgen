/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
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
	private ExecVariable var;

	public VariableExpression(ExecVariable var) {
		super("variable", var.getType());
		this.var = var;
	}

	/** Returns the variable of this variable expression. */
	public ExecVariable getVariable() {
		return var;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.add(var);
	}
}
