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

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * Represents a case statement of a switch statement in the IR.
 */
public class CaseStatement extends NestingStatement
{
	private Expression caseConstantExpr; // null for the "else" (aka default) case

	public CaseStatement(Expression caseConstExpr)
	{
		super("case statement");
		this.caseConstantExpr = caseConstExpr;
	}

	public Expression getCaseConstantExpr()
	{
		return caseConstantExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(caseConstantExpr != null)
			caseConstantExpr.collectNeededEntities(needs);
		for(EvalStatement statement : statements) {
			statement.collectNeededEntities(needs);
		}
	}
}
