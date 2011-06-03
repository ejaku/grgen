/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ir;

public class MapItem extends IR {
	Expression keyExpr;
	Expression valueExpr;

	public MapItem(Expression keyExpr, Expression valueExpr) {
		super("map item");
		this.keyExpr = keyExpr;
		this.valueExpr = valueExpr;
	}

	public Expression getKeyExpr() {
		return keyExpr;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		keyExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
	}
}
