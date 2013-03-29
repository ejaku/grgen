/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

/**
 * Represents a call statement (of a function normally returning a value, throwing that value away) in the IR.
 */
public class CallStatement extends EvalStatement {

	private Expression calledFunction;

	public CallStatement(Expression calledFunction) {
		super("call statement");
		this.calledFunction = calledFunction;
	}
	
	public Expression getCalledFunction() {
		return calledFunction;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		calledFunction.collectNeededEntities(needs);
	}
}
