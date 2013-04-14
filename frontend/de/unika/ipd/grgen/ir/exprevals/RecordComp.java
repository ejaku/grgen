/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class RecordComp extends ComputationInvocationBase {
	private Expression toRecordExpr;

	public RecordComp(Expression toRecordExpr) {
		super("record computation");
		this.toRecordExpr = toRecordExpr;
	}

	public Expression getToRecordExpr() {
		return toRecordExpr;
	}

	public ComputationBase getComputationBase() {
		return null; // dummy needed for interface, not accessed cause real type defines computation
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		toRecordExpr.collectNeededEntities(needs);
	}
}
