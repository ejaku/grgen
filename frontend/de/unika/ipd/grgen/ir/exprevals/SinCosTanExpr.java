/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class SinCosTanExpr extends Expression {
	private int which;
	private Expression expr;

	public static final int SIN = 0;
	public static final int COS = 1;
	public static final int TAN = 2;

	public SinCosTanExpr(int which, Expression expr) {
		super("sin cos tan expr", expr.getType());
		this.which = which;
		this.expr = expr;
	}

	public int getWhich() {
		return which;
	}

	public Expression getExpr() {
		return expr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		expr.collectNeededEntities(needs);
	}
}
