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
 * Represents a switch statement in the IR.
 */
public class SwitchStatement extends EvalStatement {

	private Expression switchExpr;
	private Collection<CaseStatement> statements = new LinkedList<CaseStatement>();

	public SwitchStatement(Expression switchExpr) {
		super("switch statement");
		this.switchExpr = switchExpr;
	}
	
	public void addStatement(CaseStatement statement) {
		statements.add(statement);
	}

	public Expression getSwitchExpr() {
		return switchExpr;
	}

	public Collection<CaseStatement> getStatements() {
		return statements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		switchExpr.collectNeededEntities(needs);
		for(EvalStatement statement : statements)
			statement.collectNeededEntities(needs);
	}
}
