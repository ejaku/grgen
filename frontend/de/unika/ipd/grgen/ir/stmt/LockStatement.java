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

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;

/**
 * Represents a lock statement in the IR.
 */
public class LockStatement extends BlockNestingStatement
{
	private Expression lockObjectExpr;

	public LockStatement(Expression lockObjectExpr)
	{
		super("lock statement");
		this.lockObjectExpr = lockObjectExpr;
	}

	public Expression getLockObjectExpr()
	{
		return lockObjectExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		lockObjectExpr.collectNeededEntities(needs);
		for(EvalStatement lockedStatement : statements) {
			lockedStatement.collectNeededEntities(needs);
		}
	}
}
