/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * Represents a case statement of a switch statement in the IR.
 */
public class CaseStatement extends EvalStatement {

	private Expression caseConstantExpr; // null for the "else" (aka default) case
	private Collection<EvalStatement> statements = new LinkedList<EvalStatement>();

	public CaseStatement(Expression caseConstExpr) {
		super("case statement");
		this.caseConstantExpr = caseConstExpr;
	}
	
	public void addStatement(EvalStatement statement) {
		statements.add(statement);
	}

	public Expression getCaseConstantExpr() {
		return caseConstantExpr;
	}

	public Collection<EvalStatement> getStatements() {
		return statements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(caseConstantExpr!=null)
			caseConstantExpr.collectNeededEntities(needs);
		for(EvalStatement statement : statements)
			statement.collectNeededEntities(needs);
	}
}
