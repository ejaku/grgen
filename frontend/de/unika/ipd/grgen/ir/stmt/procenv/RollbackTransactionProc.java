/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class RollbackTransactionProc extends BuiltinProcedureInvocationBase
{
	private Expression transactionIdExpr;

	public RollbackTransactionProc(Expression transactionIdExpr)
	{
		super("rollback transaction procedure");
		this.transactionIdExpr = transactionIdExpr;
	}

	public Expression getTransactionId()
	{
		return transactionIdExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		transactionIdExpr.collectNeededEntities(needs);
	}
}
