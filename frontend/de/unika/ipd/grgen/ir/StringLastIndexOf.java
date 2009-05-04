/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

public class StringLastIndexOf extends Expression {
	private Expression stringExpr, stringToSearchForExpr;
	
	public StringLastIndexOf(Expression stringExpr, Expression stringToSearchForExpr) {
		super("string lastIndexOf", IntType.getType());
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
