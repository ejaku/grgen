/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class ExportProc extends ProcedureInvocationBase {
	private Expression pathExpr;
	private Expression graphExpr;

	public ExportProc(Expression pathExpr, Expression graphExpr) {
		super("export procedure");
		this.pathExpr = pathExpr;
		this.graphExpr = graphExpr;
	}

	public Expression getPathExpr() {
		return pathExpr;
	}

	public Expression getGraphExpr() {
		return graphExpr;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		pathExpr.collectNeededEntities(needs);
		if(graphExpr!=null)
			graphExpr.collectNeededEntities(needs);
	}
}
