/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.procenv
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class RollbackTransactionProc : BuiltinProcedureInvocationBase
{
	private Expression transactionIdExpr;

	public RollbackTransactionProc(Expression transactionIdExpr)
		: base("rollback transaction procedure")
	{
		this.transactionIdExpr = transactionIdExpr;
	}

	public virtual Expression TransactionId
	{
		get
		{
		return transactionIdExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
		transactionIdExpr.CollectNeededEntities(needs);
	}
}

}
