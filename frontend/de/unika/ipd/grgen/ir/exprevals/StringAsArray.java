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

import de.unika.ipd.grgen.ir.Type;

public class StringAsArray extends Expression {
	private Expression stringExpr, stringToSplitAtExpr;

	public StringAsArray(Expression stringExpr, Expression stringToSplitAtExpr, Type targetType) {
		super("string asArray", targetType);
		this.stringExpr = stringExpr;
		this.stringToSplitAtExpr = stringToSplitAtExpr;
	}

	public Expression getStringExpr() {
		return stringExpr;
	}

	public Expression getStringToSplitAtExpr() {
		return stringToSplitAtExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		stringExpr.collectNeededEntities(needs);
		stringToSplitAtExpr.collectNeededEntities(needs);
	}
}
