/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class ImportExpr extends Expression {
	private final Expression pathExpr;

	public ImportExpr(Expression pathExpr, Type type) {
		super("import expression", type);
		this.pathExpr= pathExpr;
	}

	public Expression getPathExpr() {
		return pathExpr;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		pathExpr.collectNeededEntities(needs);
		needs.needsGraph();
	}
}

