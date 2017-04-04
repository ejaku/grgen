/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphMergeProc extends ProcedureInvocationBase {
	private Expression target;
	private Expression source;
	private Expression sourceName;

	public GraphMergeProc(Expression target, Expression source, Expression sourceName) {
		super("graph merge procedure");
		this.target = target;
		this.source = source;
		this.sourceName = sourceName;
	}

	public Expression getTarget() {
		return target;
	}

	public Expression getSource() {
		return source;
	}

	public Expression getSourceName() {
		return sourceName;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		target.collectNeededEntities(needs);
		source.collectNeededEntities(needs);
	}
}
