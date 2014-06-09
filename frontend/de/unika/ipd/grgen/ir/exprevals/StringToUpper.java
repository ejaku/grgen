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

public class StringToUpper extends Expression {
	private Expression stringExpr;

	public StringToUpper(Expression stringExpr) {
		super("string toUpper", StringType.getType());
		this.stringExpr = stringExpr;
	}

	public Expression getStringExpr() {
		return stringExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		stringExpr.collectNeededEntities(needs);
	}
}
