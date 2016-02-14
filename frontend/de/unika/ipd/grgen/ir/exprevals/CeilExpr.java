/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class CeilExpr extends Expression {
	private Expression expr;

	public CeilExpr(Expression expr) {
		super("ceil expr", expr.getType());
		this.expr = expr;
	}

	public Expression getExpr() {
		return expr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		expr.collectNeededEntities(needs);
	}
}
