/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.exprevals.*;

public class ArrayIndexOfExpr extends Expression {
	private Expression targetExpr, valueExpr;

	public ArrayIndexOfExpr(Expression targetExpr, Expression valueExpr) {
		super("array indexOf expr", IntType.getType());
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
