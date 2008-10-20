/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ir;

public class SetSizeExpr extends Expression {
	Expression targetExpr;
	
	public SetSizeExpr(Expression targetExpr) {
		super("set size expression", IntType.getType());
		this.targetExpr = targetExpr;
	}
	
	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
	}
	
	public Expression getTargetExpr() {
		return targetExpr;
	}	
}
