/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
}