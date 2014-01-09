/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphRedirectSourceProc extends ProcedureInvocationBase {
	private Expression edge;
	private Expression newSource;
	private Expression oldSourceName; // optional

	public GraphRedirectSourceProc(Expression edge, Expression newSource, Expression oldSourceName) {
		super("graph redirect source procedure");
		this.edge = edge;
		this.newSource = newSource;
		this.oldSourceName = oldSourceName;
	}

	public Expression getEdge() {
		return edge;
	}

	public Expression getNewSource() {
		return newSource;
	}

	public Expression getOldSourceName() {
		return oldSourceName;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		edge.collectNeededEntities(needs);
		newSource.collectNeededEntities(needs);
	}
}
