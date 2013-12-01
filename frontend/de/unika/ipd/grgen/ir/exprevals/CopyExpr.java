/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class CopyExpr extends Expression {
	private final Expression sourceExpr;

	public CopyExpr(Expression sourceExpr, Type type) {
		super("copy expression", type);
		this.sourceExpr = sourceExpr;
	}

	public Expression getSourceExpr() {
		return sourceExpr;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		sourceExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}

