/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import java.util.Vector;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * Represents a return statement of a procedure in the IR.
 */
public class ReturnStatementProcedure extends EvalStatement
{
	private Vector<Expression> returnValuesExprs = new Vector<Expression>();

	public ReturnStatementProcedure()
	{
		super("return statement (procedure)");
	}

	public void addReturnValueExpr(Expression returnValueExpr)
	{
		returnValuesExprs.add(returnValueExpr);
	}

	public Vector<Expression> getReturnValueExpr()
	{
		return returnValuesExprs;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		for(Expression returnValueExpr : returnValuesExprs) {
			returnValueExpr.collectNeededEntities(needs);
		}
	}
}
