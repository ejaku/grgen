/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

public class StringReplace extends Expression {
	private Expression stringExpr, startExpr, lengthExpr, replaceStrExpr;

	public StringReplace(Expression stringExpr,
			Expression startExpr, Expression lengthExpr, Expression replaceStrExpr) {
		super("string replace", StringType.getType());
		this.stringExpr = stringExpr;
		this.startExpr = startExpr;
		this.lengthExpr = lengthExpr;
		this.replaceStrExpr = replaceStrExpr;
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

	public Expression getReplaceStrExpr() {
		return replaceStrExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		stringExpr.collectNeededEntities(needs);
		startExpr.collectNeededEntities(needs);
		lengthExpr.collectNeededEntities(needs);
		replaceStrExpr.collectNeededEntities(needs);
	}
}
