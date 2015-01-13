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

public class VResetProc extends ProcedureInvocationBase {
	private Expression visFlagExpr;

	public VResetProc(Expression stringExpr) {
		super("vreset procedure");
		this.visFlagExpr = stringExpr;
	}

	public Expression getVisitedFlagExpr() {
		return visFlagExpr;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		visFlagExpr.collectNeededEntities(needs);
	}
}
