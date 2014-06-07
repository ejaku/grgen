/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.containers.*;

public class IndexedAccessExpr extends Expression {
	Expression targetExpr;
	Expression keyExpr;

	public IndexedAccessExpr(Expression targetExpr, Expression keyExpr) {
		super("indexed access expression", targetExpr.getType() instanceof MapType ? ((MapType) targetExpr.getType()).getValueType() : targetExpr.getType() instanceof DequeType ? ((DequeType) targetExpr.getType()).getValueType() : ((ArrayType) targetExpr.getType()).getValueType());
		this.targetExpr = targetExpr;
		this.keyExpr = keyExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		keyExpr.collectNeededEntities(needs);
		targetExpr.collectNeededEntities(needs);
	}

	public Expression getTargetExpr() {
		return targetExpr;
	}

	public Expression getKeyExpr() {
		return keyExpr;
	}
}
