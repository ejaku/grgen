/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class StringIndexOf extends Expression {
	private Expression stringExpr, stringToSearchForExpr;

	public StringIndexOf(Expression stringExpr, Expression stringToSearchForExpr) {
		super("string indexOf", IntType.getType());
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
