/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

public class StringSubstring extends Expression {
	private Expression stringExpr, startExpr, lengthExpr;
	
	public StringSubstring(Expression stringExpr, Expression startExpr, Expression lengthExpr) {
		super("string substring", StringType.getType());
		this.stringExpr = stringExpr;
		this.startExpr = startExpr;
		this.lengthExpr = lengthExpr;
	}
	
	public Expression getStringExpr() {
		return stringExpr;
	}
	
	public Expression getStartExpr() {
		return startExpr;
	}
	
	public Expression getLengthExpr() {
		return lengthExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		stringExpr.collectNeededEntities(needs);
		startExpr.collectNeededEntities(needs);
		lengthExpr.collectNeededEntities(needs);
	}
}
