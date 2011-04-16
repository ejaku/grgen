/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir;

public class ArrayLastIndexOfExpr extends Expression {
	private Expression targetExpr, valueExpr;

	public ArrayLastIndexOfExpr(Expression targetExpr, Expression valueExpr) {
		super("array lastIndexOf expr", IntType.getType());
		this.targetExpr = targetExpr;
		this.valueExpr = valueExpr;
	}

	public Expression getTargetExpr() {
		return targetExpr;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		targetExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
	}
}
