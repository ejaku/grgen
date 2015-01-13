/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

public class StringLastIndexOf extends Expression {
	private Expression stringExpr;
	private Expression stringToSearchForExpr;
	private Expression startIndexExpr;

	public StringLastIndexOf(Expression stringExpr, Expression stringToSearchForExpr) {
		super("string lastIndexOf", IntType.getType());
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
	}

	public StringLastIndexOf(Expression stringExpr, Expression stringToSearchForExpr, Expression startIndexExpr) {
		super("string lastIndexOf", IntType.getType());
		this.stringExpr = stringExpr;
		this.stringToSearchForExpr = stringToSearchForExpr;
		this.startIndexExpr = startIndexExpr;
	}


	public Expression getStringExpr() {
		return stringExpr;
	}

	public Expression getStringToSearchForExpr() {
		return stringToSearchForExpr;
	}

	public Expression getStartIndexExpr() {
		return startIndexExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		stringExpr.collectNeededEntities(needs);
		stringToSearchForExpr.collectNeededEntities(needs);
		if(startIndexExpr!=null)
			startIndexExpr.collectNeededEntities(needs);
	}
}
