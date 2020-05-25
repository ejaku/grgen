/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * Represents a while statement in the IR.
 */
public class WhileStatement extends EvalStatement
{
	private Expression conditionExpr;
	private Collection<EvalStatement> loopedStatements = new LinkedList<EvalStatement>();

	public WhileStatement(Expression conditionExpr)
	{
		super("while statement");
		this.conditionExpr = conditionExpr;
	}

	public void addLoopedStatement(EvalStatement loopedStatement)
	{
		loopedStatements.add(loopedStatement);
	}

	public Expression getConditionExpr()
	{
		return conditionExpr;
	}

	public Collection<EvalStatement> getLoopedStatements()
	{
		return loopedStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		conditionExpr.collectNeededEntities(needs);
		for(EvalStatement loopedStatement : loopedStatements) {
			loopedStatement.collectNeededEntities(needs);
		}
	}
}