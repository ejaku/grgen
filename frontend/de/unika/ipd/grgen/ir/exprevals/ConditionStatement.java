/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;
import java.util.LinkedList;

/**
 * Represents a condition statement in the IR.
 */
public class ConditionStatement extends EvalStatement {

	private Expression conditionExpr;
	private Collection<EvalStatement> trueCaseStatements = new LinkedList<EvalStatement>();
	private Collection<EvalStatement> falseCaseStatements = null;

	public ConditionStatement(Expression conditionExpr) {
		super("condition statement");
		this.conditionExpr = conditionExpr;
	}
	
	public void addTrueCaseStatement(EvalStatement trueCaseStatement) {
		trueCaseStatements.add(trueCaseStatement);
	}

	public void addFalseCaseStatement(EvalStatement falseCaseStatement) {
		if(falseCaseStatements==null)
			falseCaseStatements = new LinkedList<EvalStatement>();
		falseCaseStatements.add(falseCaseStatement);
	}

	public Expression getConditionExpr() {
		return conditionExpr;
	}

	public Collection<EvalStatement> getTrueCaseStatements() {
		return trueCaseStatements;
	}

	public Collection<EvalStatement> getFalseCaseStatements() {
		return falseCaseStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		conditionExpr.collectNeededEntities(needs);
		for(EvalStatement trueCaseStatement : trueCaseStatements)
			trueCaseStatement.collectNeededEntities(needs);
		if(falseCaseStatements!=null)
			for(EvalStatement falseCaseStatement : falseCaseStatements)
				falseCaseStatement.collectNeededEntities(needs);
	}
}
