/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class EmitProc extends ProcedureInvocationBase {
	private Expression toEmitExpr;

	public EmitProc(Expression toEmitExpr) {
		super("emit procedure");
		this.toEmitExpr = toEmitExpr;
	}

	public Expression getToEmitExpr() {
		return toEmitExpr;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		toEmitExpr.collectNeededEntities(needs);
	}
}
