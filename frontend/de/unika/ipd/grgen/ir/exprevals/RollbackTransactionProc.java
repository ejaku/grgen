/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class RollbackTransactionProc extends ProcedureInvocationBase {
	private Expression transactionIdExpr;

	public RollbackTransactionProc(Expression transactionIdExpr) {
		super("rollback transaction procedure");
		this.transactionIdExpr = transactionIdExpr;
	}

	public Expression getTransactionId() {
		return transactionIdExpr;
	}
	
	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		transactionIdExpr.collectNeededEntities(needs);
	}
}
