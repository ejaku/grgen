/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir;

public class ArrayPeekExpr extends Expression {
	private Expression targetExpr, numberExpr;

	public ArrayPeekExpr(Expression targetExpr, Expression numberExpr) {
		super("array peek expr", ((ArrayType)(targetExpr.getType())).valueType);
		this.targetExpr = targetExpr;
		this.numberExpr = numberExpr;
	}

	public Expression getTargetExpr() {
		return targetExpr;
	}

	public Expression getNumberExpr() {
		return numberExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		targetExpr.collectNeededEntities(needs);
		numberExpr.collectNeededEntities(needs);
	}
}
