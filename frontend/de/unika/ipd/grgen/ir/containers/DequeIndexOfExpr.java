/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.exprevals.*;

public class DequeIndexOfExpr extends Expression {
	private Expression targetExpr, valueExpr;

	public DequeIndexOfExpr(Expression targetExpr, Expression valueExpr) {
		super("deque indexOf expr", IntType.getType());
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
