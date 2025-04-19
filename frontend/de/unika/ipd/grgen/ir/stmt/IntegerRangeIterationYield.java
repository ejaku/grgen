/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents an accumulation yielding of a container variable in the IR.
 */
public class IntegerRangeIterationYield extends BlockNestingStatement
{
	private Variable iterationVar;
	private Expression leftExpr;
	private Expression rightExpr;

	public IntegerRangeIterationYield(Variable iterationVar, Expression left, Expression right)
	{
		super("integer range iteration yield");
		this.iterationVar = iterationVar;
		this.leftExpr = left;
		this.rightExpr = right;
	}

	public Variable getIterationVar()
	{
		return iterationVar;
	}

	public Expression getLeftExpr()
	{
		return leftExpr;
	}

	public Expression getRightExpr()
	{
		return rightExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		leftExpr.collectNeededEntities(needs);
		rightExpr.collectNeededEntities(needs);
		for(EvalStatement accumulationStatement : statements) {
			accumulationStatement.collectNeededEntities(needs);
		}
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
