/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class StringStartsWith extends Expression {
	private Expression stringExpr, stringToSearchForExpr;

	public StringStartsWith(Expression stringExpr, Expression stringToSearchForExpr) {
		super("string startsWith", BooleanType.getType());
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
	}

	public Expression getStringExpr() {
		return stringExpr;
	}

	public Expression getStringToSearchForExpr() {
		return stringToSearchForExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		stringExpr.collectNeededEntities(needs);
		stringToSearchForExpr.collectNeededEntities(needs);
	}
}
