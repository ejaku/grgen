/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class LogExpr extends Expression {
	private Expression leftExpr;
	private Expression rightExpr;

	public LogExpr(Expression leftExpr, Expression rightExpr) {
		super("log expr", leftExpr.getType());
		this.leftExpr = leftExpr;
		this.rightExpr = rightExpr;
	}

	public LogExpr(Expression leftExpr) {
		super("log expr", leftExpr.getType());
		this.leftExpr = leftExpr;
	}

	public Expression getLeftExpr() {
		return leftExpr;
	}

	public Expression getRightExpr() {
		return rightExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		leftExpr.collectNeededEntities(needs);
		if(rightExpr!=null) rightExpr.collectNeededEntities(needs);
	}
}
