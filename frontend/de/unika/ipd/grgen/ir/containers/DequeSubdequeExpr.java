/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.exprevals.*;

public class DequeSubdequeExpr extends Expression {
	private Expression targetExpr, startExpr, lengthExpr;

	public DequeSubdequeExpr(Expression targetExpr, Expression startExpr, Expression lengthExpr) {
		super("deque subdeque expr", (DequeType)targetExpr.getType());
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
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
		startExpr.collectNeededEntities(needs);
		lengthExpr.collectNeededEntities(needs);
	}
}
