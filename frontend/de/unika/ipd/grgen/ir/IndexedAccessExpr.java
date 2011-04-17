/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id: IndexedAccessExpr.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

public class IndexedAccessExpr extends Expression {
	Expression targetExpr;
	Expression keyExpr;

	public IndexedAccessExpr(Expression targetExpr, Expression keyExpr) {
		super("indexed access expression", targetExpr.getType() instanceof MapType ? ((MapType) targetExpr.getType()).getValueType() : ((ArrayType) targetExpr.getType()).getValueType());
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
