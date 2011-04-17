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

public class ArrayItem extends IR {
	Expression valueExpr;

	public ArrayItem(Expression valueExpr) {
		super("array item");
		this.valueExpr = valueExpr;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}
	
	public void collectNeededEntities(NeededEntities needs) {
		valueExpr.collectNeededEntities(needs);
	}
}
