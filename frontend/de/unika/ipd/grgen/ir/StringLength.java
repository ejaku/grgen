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

public class StringLength extends Expression {
	private Expression stringExpr;

	public StringLength(Expression stringExpr) {
		super("string length", IntType.getType());
		this.stringExpr = stringExpr;
	}

	public Expression getStringExpr() {
		return stringExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		stringExpr.collectNeededEntities(needs);
	}
}
