/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: SetPeekExpr.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ir;

public class SetPeekExpr extends Expression {
	private Expression targetExpr, numberExpr;

	public SetPeekExpr(Expression targetExpr, Expression numberExpr) {
		super("set peek expr", ((SetType)(targetExpr.getType())).valueType);
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
