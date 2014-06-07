/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class RandomExpr extends Expression {
	private Expression numExpr;

	public RandomExpr(Expression numExpr) {
		super("random", numExpr==null ? DoubleType.getType() : IntType.getType());
		this.numExpr = numExpr;
	}

	public Expression getNumExpr() {
		return numExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		if(numExpr!=null)
			numExpr.collectNeededEntities(needs);
	}
}
