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

public class ArraySubarrayExpr extends Expression {
	private Expression targetExpr, startExpr, lengthExpr;

	public ArraySubarrayExpr(Expression targetExpr, Expression startExpr, Expression lengthExpr) {
		super("array subarray expr", (ArrayType)targetExpr.getType());
		this.targetExpr = targetExpr;
		this.startExpr = startExpr;
		this.lengthExpr = lengthExpr;
	}

	public Expression getTargetExpr() {
		return targetExpr;
	}

	public Expression getStartExpr() {
		return startExpr;
	}

	public Expression getLengthExpr() {
		return lengthExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		targetExpr.collectNeededEntities(needs);
		startExpr.collectNeededEntities(needs);
		lengthExpr.collectNeededEntities(needs);
	}
}
