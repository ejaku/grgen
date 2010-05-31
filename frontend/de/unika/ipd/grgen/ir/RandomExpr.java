/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id: RandomExpr.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ir;

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
