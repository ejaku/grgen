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

public class PowExpr extends Expression {
	private Expression leftExpr;
	private Expression rightExpr;

	public PowExpr(Expression leftExpr, Expression rightExpr) {
		super("pow expr", rightExpr.getType());
		this.leftExpr = leftExpr;
		this.rightExpr = rightExpr;
	}

	public PowExpr(Expression rightExpr) {
		super("pow expr", rightExpr.getType());
		this.rightExpr = rightExpr;
	}

	public Expression getLeftExpr() {
		return leftExpr;
	}

	public Expression getRightExpr() {
		return rightExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		if(leftExpr!=null) leftExpr.collectNeededEntities(needs);
		rightExpr.collectNeededEntities(needs);
	}
}
