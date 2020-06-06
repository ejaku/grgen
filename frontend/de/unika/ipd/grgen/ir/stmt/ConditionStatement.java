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
 * Represents a condition statement in the IR.
 */
public class ConditionStatement extends NestingStatement
{
	private Expression conditionExpr;
	private Collection<EvalStatement> falseCaseStatements = null;

	public ConditionStatement(Expression conditionExpr)
	{
		super("condition statement");
		this.conditionExpr = conditionExpr;
	}

	public void addFalseCaseStatement(EvalStatement falseCaseStatement)
	{
		if(falseCaseStatements == null)
			falseCaseStatements = new LinkedList<EvalStatement>();
		falseCaseStatements.add(falseCaseStatement);
	}

	public Expression getConditionExpr()
	{
		return conditionExpr;
	}

	public Collection<EvalStatement> getFalseCaseStatements()
	{
		return falseCaseStatements;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		conditionExpr.collectNeededEntities(needs);
		for(EvalStatement trueCaseStatement : statements) {
			trueCaseStatement.collectNeededEntities(needs);
		}
		if(falseCaseStatements != null) {
			for(EvalStatement falseCaseStatement : falseCaseStatements) {
				falseCaseStatement.collectNeededEntities(needs);
			}
		}
	}
}
