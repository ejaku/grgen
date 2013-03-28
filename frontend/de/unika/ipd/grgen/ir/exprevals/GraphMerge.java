/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class GraphMerge extends EvalStatement {
	private Expression target;
	private Expression source;
	private Expression sourceName;

	public GraphMerge(Expression target, Expression source, Expression sourceName) {
		super("graph merge");
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

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		target.collectNeededEntities(needs);
		source.collectNeededEntities(needs);
	}
}
