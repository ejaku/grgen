/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class DeleteFileProc extends ProcedureInvocationBase {
	private Expression pathExpr;

	public DeleteFileProc(Expression pathExpr) {
		super("deleteFile procedure");
		this.pathExpr = pathExpr;
	}

	public Expression getPathExpr() {
		return pathExpr;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		pathExpr.collectNeededEntities(needs);
	}
}
