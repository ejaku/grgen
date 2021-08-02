/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * Represents a return statement of a function in the IR.
 */
public class ReturnStatement extends EvalStatement
{
	private Expression returnValueExpr;

	public ReturnStatement(Expression returnValueExpr)
	{
		super("return statement");
		this.returnValueExpr = returnValueExpr;
	}

	public Expression getReturnValueExpr()
	{
		return returnValueExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		returnValueExpr.collectNeededEntities(needs);
	}
}
