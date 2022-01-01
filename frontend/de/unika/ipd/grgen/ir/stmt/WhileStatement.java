/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * Represents a while statement in the IR.
 */
public class WhileStatement extends BlockNestingStatement
{
	private Expression conditionExpr;

	public WhileStatement(Expression conditionExpr)
	{
		super("while statement");
		this.conditionExpr = conditionExpr;
	}

	public Expression getConditionExpr()
	{
		return conditionExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		conditionExpr.collectNeededEntities(needs);
		for(EvalStatement loopedStatement : statements) {
			loopedStatement.collectNeededEntities(needs);
		}
	}
}
