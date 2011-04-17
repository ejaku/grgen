/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

public class PowExpr extends Expression {
	private Expression leftExpr;
	private Expression rightExpr;

	public PowExpr(Expression leftExpr, Expression rightExpr) {
		super("pow expr", leftExpr.getType());
		this.leftExpr = leftExpr;
		this.rightExpr = rightExpr;
	}

	public Expression getLeftExpr() {
		return leftExpr;
	}

	public Expression getRightExpr() {
		return rightExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		leftExpr.collectNeededEntities(needs);
		rightExpr.collectNeededEntities(needs);
	}
}
