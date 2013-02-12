/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;


/**
 * Represents a return statement in the IR.
 */
public class ReturnStatement extends EvalStatement {

	private Expression returnValueExpr;

	public ReturnStatement(Expression returnValueExpr) {
		super("return statement");
		this.returnValueExpr = returnValueExpr;
	}

	public Expression getReturnValueExpr() {
		return returnValueExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		returnValueExpr.collectNeededEntities(needs);
	}
}
